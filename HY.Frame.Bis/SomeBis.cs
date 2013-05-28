using HY.Frame.Core.Toolkit;
using HY.Frame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alx.ORM.Core;
using System.Data;

namespace HY.Frame.Bis
{
    public class SomeBis
    {
        public DataTable List1Init1(string name, string key)
        {
            var q = new SomeBisQuery { SearchText = name, KeyId = key.ToInt32() };
            return List1(q);
        }

        public DataTable List1Init2(string name, string key, string Sex, string birthday)
        {
            var q = new SomeBisQuery { SearchText = name, KeyId = key.ToInt32(), Birthday = birthday, Sex = Sex };
            return List1(q);
        }

        protected DataTable List1(SomeBisQuery q)
        {
            var db = Qdb.Current;
            var pars = new List<object>();
            var sql = "select person_id,name,key_id, birthday, mobile from table where 1=1";

            if (q.KeyId > 0)
            {
                sql += " and key_id = ?";
                pars.Add(new Parameter(System.Data.DbType.Int32, q.KeyId));
            }
            if (!string.IsNullOrWhiteSpace(q.SearchText))
            {
                sql += " and name like ?";
                pars.Add(q.SearchText.Trim() + "%");
            }

            if (!string.IsNullOrWhiteSpace(q.Sex))
            {
                sql += " and sex = ?";
                pars.Add(q.Sex);
            }

            if (!string.IsNullOrWhiteSpace(q.Birthday))
            {
                sql += " and birthday = ?";
                pars.Add(q.Birthday.ToDateTime());
            }

            var dt = db.Query(sql, pars).Tables[0];
            return dt;
        }
    }

    public class SomeBisQuery : Query
    {
        private string _searchText;

        public override string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; }
        }
        public string Sex { get; set; }
        public int KeyId { get; set; }
        public string Birthday { get; set; }

        public string mobile { get; set; }
    }
}
