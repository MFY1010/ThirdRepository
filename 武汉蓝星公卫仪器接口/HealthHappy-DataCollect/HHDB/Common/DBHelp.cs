using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HHDB.Common
{
    public static class DBHelp
    {
        public static SqlConnectionStringBuilder SqlConnectionBulider { get; set; }
        /// <summary>
        /// 获取体检数据库链接
        /// </summary>
        /// <returns></returns>
        public static DbConnection GetPeisDB(string conn)
        {
            if (SqlConnectionBulider == null)
            {
                SqlConnectionBulider = new SqlConnectionStringBuilder();
                SqlConnectionBulider.ConnectionString = conn;
                SqlConnectionBulider.PersistSecurityInfo = true;
                SqlConnectionBulider.ConnectTimeout = 10;
            }
            return new SqlConnection(SqlConnectionBulider.ToString());
        }
    }
}
