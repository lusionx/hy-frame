using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq.Expressions;
using System.IO;
using HY.Frame.Core.Toolkit;

namespace HY.Frame.Core
{
    internal class PathPair
    {
        public string Path { get; set; }
        public object Instance { get; set; }
        public MethodInfo MethodInfo { get; set; }
        public Func<object, object[], object> Delegate { get; set; }
    }

    internal delegate object Execute(object instance, object[] pars);

    /// <summary>
    /// 方法代理工厂
    /// </summary>
    public class MAgentFactory : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        protected HttpRequest Request { get; set; }
        protected HttpResponse Response { get; set; }

        protected log4net.ILog Logger { get; set; }

        private void Init(HttpContext context)
        {
            Logger = Log.Get(this.GetType());
            Request = context.Request;
            Logger.InfoFormat("url:{0}", Request.Url.OriginalString);
            Response = context.Response;
        }

        public void ProcessRequest(HttpContext context)
        {
            Init(context);
            var key = "HY.Frame.Core.MAgentFactory.PathPairs";
            object instance = null;
            Func<object, object[], object> func = null;
            MethodInfo methodInf = null;
            if (CacheData.Exist(key) && CacheData.Get<List<PathPair>>(key).Any(a => a.Path == Request.Url.AbsolutePath))
            {
                #region 从缓存中取得
                var pp = CacheData.Get<List<PathPair>>(key).First(a => a.Path == Request.Url.AbsolutePath);
                instance = pp.Instance;
                methodInf = pp.MethodInfo;
                func = pp.Delegate;
                #endregion
            }
            else
            {
                #region 获得 实例 委托
                var url = FixUrl();
                var path = url.Split(new string[] { ".", "/" }, StringSplitOptions.RemoveEmptyEntries);
                var mothod = path[path.Length - 1];//去掉末尾的 .c
                var clsName = string.Join(".", path.Take(path.Length - 1).ToArray());

                //得到正确的程序集
                Assembly assem = GetAssembly(context.Request.PhysicalApplicationPath, path);
                if (assem == null)
                {
                    Response.Write("没有找到程序集");
                    return;
                }

                instance = GetInstance(assem, clsName);
                if (instance == null)
                {
                    Response.Write("没有找到要访问的类");
                    return;
                }

                methodInf = instance.GetType().GetMethod(mothod, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (methodInf == null)
                {
                    Response.Write("没有找到要访问的方法");
                    return;
                }

                if (!methodInf.GetCustomAttributes(false).Any(a => a is WebApiAttribute)
                    && methodInf.ReturnType != typeof(ResResult))
                {
                    Response.Write("拒绝访问方法");
                    return;
                }

                func = GetDelegate(methodInf);
                var pp = new PathPair { Path = Request.Url.AbsolutePath, Delegate = func, Instance = instance, MethodInfo = methodInf };
                if (!CacheData.Exist(key))
                {
                    CacheData.Add(key, new List<PathPair> { pp });
                }
                else
                {
                    CacheData.Get<List<PathPair>>(key).Add(pp);
                }
                #endregion
            }

            object[] parVs = GetParameterValue(methodInf).ToArray();
            object result = null;
            try
            {
                result = func(instance, parVs);
            }
            catch (Exception e)
            {
                Log.Get(instance.GetType()).Error("Error.c", e);
                result = new ResResult { error = true, msg = e.Message };
            }

            Response.AppendHeader("Cache-Control", "no-cache");
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Expires", "0");

            ResponseResult(result);
        }

        protected void ResponseResult(object result)
        {
            Func<object, string> js = (e) => ObjectExtensions.ToJson(e);
            if (result is ResStringResult)
            {
                Response.Write((result as ResStringResult).data.ToString());
            }
            else if (result is ResJsonResult)
            {
                Response.Write(js((result as ResJsonResult).data));
            }
            else if (result is ResResult)
            {
                Response.Write(js((result as ResResult)));
            }
            else if (result is string)
            {
                Response.Write(result as string);
            }
            else if (result is Stream)
            {
                Response.BinaryWrite(Deflate.StreamToBytes(result as Stream));
            }
            else
            {
                var ijson = Registration.FindIJSON(result);
                Response.Write(ijson.ToJSON(result));
            }
        }

        /// <summary>
        /// 得到参数列表
        /// </summary>
        /// <param name="methodInf"></param>
        /// <returns></returns>
        protected IEnumerable<object> GetParameterValue(MethodInfo methodInf)
        {
            var parsVal = new List<object>();
            foreach (var par in methodInf.GetParameters())
            {
                var str = Request[par.Name] ?? string.Empty;
                try
                {
                    parsVal.Add(Convert.ChangeType(str.Trim(), par.ParameterType));
                }
                catch
                {
                    parsVal.Add(null);
                }
            }
            return parsVal;
        }


        /// <summary>
        /// 打算使用IL生成委托，但是搞不定啊
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="mothod"></param>
        /// <returns></returns>
        [Obsolete("陷入无解")]
        internal Execute GetDelegateIL(MethodInfo methodInfo)
        {
            //http://www.cnblogs.com/daizhj/archive/2008/06/20/1224818.html
            //http://www.cnblogs.com/xuanhun/archive/2012/06/22/2558698.html
            // 生成 方法
            /*
            public T invoke( Class2 cls, object[] par)
        {
            return cls.mothd(par[0], par[1]);
        }
    IL_0000: nop
    IL_0001: ldarg.1
    IL_0002: ldarg.2
    IL_0003: ldc.i4.0
    IL_0004: ldelem.ref
    IL_0005: ldarg.2
    IL_0006: ldc.i4.1
    IL_0007: ldelem.ref
    IL_0008: callvirt instance object HY.Frame.Bis.Class2::mothd(string,  string)
    IL_000d: stloc.0
    IL_000e: br.s IL_0010

    IL_0010: ldloc.0
    IL_0011: ret
             */
            //Type[] methodArgs = methodInfo.GetParameters().Select(a => a.ParameterType).ToArray();


            Type[] methodArgs = { typeof(object), typeof(object[]) };
            System.Reflection.Emit.DynamicMethod invoke = new System.Reflection.Emit.DynamicMethod("Invoke", typeof(object), methodArgs);
            ILGenerator il = invoke.GetILGenerator();
            il.Emit(OpCodes.Nop);
            il.Emit(OpCodes.Ldarg_1);
            for (int i = 0; i < methodArgs.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_2);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldelem_Ref);
            }
            il.Emit(OpCodes.Callvirt, methodInfo);
            il.Emit(OpCodes.Ret);

            var exec = (Execute)invoke.CreateDelegate(typeof(Execute));
            /*
           var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(, AssemblyBuilderAccess.RunAndSave);
           var moduleBuilder = assemblyBuilder.DefineDynamicModule("MvcAdviceProviderModule", "test.dll", true);
           TypeBuilder typeBuilder = moduleBuilder.DefineType("MvcAdviceProvider.MvcAdviceProviderType", 
               TypeAttributes.Public, typeof(object), new Type[] { typeof(IAssessmentAopAdviceProvider) });
           assemblyBuilder.Save("runass");
            */
            return exec;

        }

        /// <summary>
        /// 得到一个 非void方法的 委托，我也看不懂 为啥
        /// 参考 http://blog.sina.com.cn/s/blog_638ea1960100ptsp.html
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="mothod"></param>
        public Func<object, object[], object> GetDelegate(MethodInfo methodInfo)
        {
            ParameterExpression instanceParameter = Expression.Parameter(typeof(object), "instance");
            ParameterExpression parametersParameter = Expression.Parameter(typeof(object[]), "parameters");
            List<Expression> parameterExpressions = new List<Expression>();
            ParameterInfo[] paramInfos = methodInfo.GetParameters();
            for (int i = 0; i < paramInfos.Length; i++)
            {
                BinaryExpression valueObj = Expression.ArrayIndex(parametersParameter, Expression.Constant(i));
                UnaryExpression valueCast = Expression.Convert(valueObj, paramInfos[i].ParameterType);
                parameterExpressions.Add(valueCast);
            }
            UnaryExpression instanceCast = Expression.Convert(instanceParameter, methodInfo.ReflectedType);
            MethodCallExpression methodCall = Expression.Call(instanceCast, methodInfo, parameterExpressions);
            Expression<Func<object, object[], object>> lambda = Expression.Lambda<Func<object, object[], object>>(methodCall, instanceParameter, parametersParameter);

            //表达式 类似  T（call( instance, (t)p1,(t)p2, t(p3) ))  t 并不相同
            Func<object, object[], object> execute = lambda.Compile();
            return execute;
        }

        protected object GetInstance(Assembly ass, string clsName)
        {
            object instance = null;
            try
            {
                instance = ass.CreateInstance(clsName, true);
            }
            catch (Exception ex)
            {
                Logger.Error("No Instance", ex);
            }
            return instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root">bin的物理路径</param>
        /// <param name="path"></param>
        /// <returns></returns>
        protected Assembly GetAssembly(string root, string[] path)
        {
            var dlls = System.IO.Directory.GetFiles(root + "bin", "*.dll")
                .Select(a => System.IO.Path.GetFileName(a)).ToList(); // 去掉目录

            Assembly assem = null;
            for (int i = 0; i < path.Length; i++)
            {
                var pathj = string.Join(".", path.Take(i + 1).ToArray()) + ".dll";
                if (pathj.In(dlls))
                {
                    assem = Assembly.LoadFrom(root + "bin\\" + pathj);
                    break;
                }
            }
            return assem;
        }

        private string FixUrl()
        {
            var url = Request.Url.AbsolutePath;//  /name.space.class.method.cls  or /name/space/class/method.cls
            if (Request.ApplicationPath.Length == 1)
            {
                url = url.Substring(1);// 去掉开头 ‘/’
            }
            else
            {
                url = url.Substring(Request.ApplicationPath.Length + 1);
            }
            url = url.Substring(0, url.Length - 1);// 去掉 扩展 *.c cls = name.space.class.method
            return url;
        }
    }
}
