<%@ WebHandler Language="C#" Class="HY.Frame.Web.Api.SQL" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using HY.Frame.Core;
using System.Text;

namespace HY.Frame.Web.Api
{
    /// <summary>
    /// Log 的摘要说明
    /// </summary>
    public class SQL : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var req = context.Request;
            ResponseFrom(context);
            var sql = req["sql"];
            if (!string.IsNullOrEmpty(sql))
            {
                var cn = GetConnection();
                var cmd = cn.CreateCommand();
                cmd.CommandText = "select count(*) from (" + sql + ") a";
                try
                {
                    cn.Open();
                    var cc = cmd.ExecuteScalar().ToString().ToInt32();
                    if (cc > 20)
                    {
                        context.Response.Write("数据量太大");
                    }
                    else
                    {
                        cmd.CommandText = sql;
                        var reader = cmd.ExecuteReader();
                        var sb = new StringBuilder("<table>");
                        var schema = reader.GetSchemaTable();
                        ResponseHeader(sb, schema);
                        ResponseBody(sb, schema, reader);
                        sb.Append("</table>");
                        reader.Close();
                        context.Response.Write(sb.ToString());
                    }
                }
                catch (Exception ex)
                {
                    context.Response.Write(ex.Message);
                }
                finally
                {
                    cn.Close();
                }

            }
        }

        protected System.Data.Common.DbConnection GetConnection()
        {
            return null;
        }


        protected void ResponseBody(StringBuilder sb, DataTable schema, System.Data.Common.DbDataReader dr)
        {
            var fcc = dr.FieldCount;
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr>");
                for (int i = 0; i < fcc; i++)
                {
                    sb.Append("<td>");
                    sb.Append(dr.GetValue(i));
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
        }

        protected void ResponseHeader(StringBuilder sb, DataTable schema)
        {
            sb.Append("<thead><tr>");
            foreach (var dr in schema.AsEnumerable())
            {
                sb.Append("<th>");
                sb.Append(dr.Field<string>("ColumnName"));
                sb.Append("</th>");
            }
            sb.Append("</tr></thead>");
        }

        protected void ResponseFrom(HttpContext context)
        {
            context.Response.Write(string.Format(@" <style>
        table th, table td {{
            border-bottom:1px solid #dddddd; 
            border-right:1px solid #dddddd; 
            padding:2px 4px 2px 8px ;
        }} </style>
<form >
<textarea name='sql' rows='10' style='width:100%'>{0}</textarea>
<input type='submit' value='查询'/>
<form>
", context.Request["sql"]));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
