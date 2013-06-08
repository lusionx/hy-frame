using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HY.Frame.Core;


namespace HY.Frame.Bis
{
    public class BisAXXXX
    {
        public object Test()
        {
            //System.Data.Objects.ObjectContext
            //System.Data.Entity.DbContext            
            return test2("222", "dddd");
        }

        [WebApi]
        public object Test1()
        {
            var a = 1;
            var b = 0;

            return a / b;
        }

        public ResResult ResResult()
        {
            return new ResResult { error = false, data = "wwww", msg = "msg" };
        }

        [WebApi]
        public string test2(string name, string age)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("aa", typeof(string));
            dt.Columns.Add("ii", typeof(int));
            dt.Columns.Add("dec", typeof(decimal));
            dt.Columns.Add("dt", typeof(DateTime));
            dt.Rows.Add("a1", 11, 11.3, DateTime.Now);
            dt.Rows.Add("a2", 12, 12.12, DateTime.Now);
            return dt.ToJson();
        }

        public string AA { get; set; }

        public object Test3(string name, string v, string v2)
        {
            AA = v2;
            return Test();
        }

        public object invoke(BisBXXXX cls, string[] par)
        {
            var db = new DBEntities();
            //HY.Frame
            return cls.mothd(par[0], par[1]);
        }

        public string Query(string sql, string p1, string p2)
        {
            var db = Qdb.Current;
            var pars = new List<object>();
            pars.Add(p1);
            pars.Add(p2);
            var ds = db.Query(sql, pars);
            return ds.Tables[0].ConverColDateToString().ToJson();
        }
    }

    public class BisBXXXX
    {
        public object mothd(string aa, string bb)
        {
            return "";
        }
    }
}
