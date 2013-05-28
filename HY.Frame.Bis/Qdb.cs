using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HY.Frame.Bis
{
    public class Qdb
    {
        public static Alx.ORM.Core.ObjectContext Current
        {
            get
            {
                var sqlstr = "data source=10.0.6.150;initial catalog=hy_common;user id=sa;password=444444;";
                //var fac = "System.Data,System.Data.SqlClient.SqlClientFactory,Sql";
                    //Oracle.DataAccess, Oracle.DataAccess.Client.OracleClientFactory,Oracle
                var fac = System.Data.SqlClient.SqlClientFactory.Instance;
                return new Alx.ORM.Core.ObjectContext(sqlstr, fac,"SQL");
            }
        }
    }
}
