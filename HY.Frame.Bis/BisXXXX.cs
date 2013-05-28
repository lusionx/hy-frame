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

        public string test2(string name, string age)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("aa");
            dt.Columns.Add(new DataColumn("ii", typeof(int)));
            dt.Rows.Add("a1", 11);
            dt.Rows.Add("a2", 12);
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
