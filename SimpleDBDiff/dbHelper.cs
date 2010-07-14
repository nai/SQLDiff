using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SimpleDBDiff
{
    class dbHelper
    {
        private string connectionstring;
        private SqlConnection conn;
        private bool connteststatus;
        public bool IsValid
        {
            get { return conn != null && connteststatus; }
        }
        public dbHelper()
        {
            connteststatus = false;
        }

        private bool testconnection()
        {
            try
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                    return false;
                conn.Close();
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return true;
        }
        public bool SetupConnection(string serverIP, string username, string password)
        {
            connectionstring = "Data Source="+serverIP+
                ";User Id=" + username + ";Password=" + password +";";
            conn = new SqlConnection(connectionstring);
            connteststatus = testconnection();
            return connteststatus;
        }
        public bool SetupConnection(string serverIP )
        {
            connectionstring = "Data Source=" + serverIP +
                    ";Integrated Security=SSPI;"  ;
            conn = new SqlConnection(connectionstring);
            connteststatus = testconnection();
            return connteststatus;
        }

        public string[] GetDatabases()
        {
            try
            {
                string qry = "exec sp_databases";
                return GetStringColumnFromQuery(qry, "DATABASE_NAME");

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string[] GetStringColumnFromQuery(string query, string columnHeader)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                List<string> ret = new List<string>();
                while (reader.Read())
                {
                    ret.Add((string)reader[columnHeader]);
                }

                return ret.ToArray();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                try
                {
                    conn.Close();
                }
                catch (Exception)
                {

                    //    throw;
                }

            }
        }

        public string[] GetStringColumnFromQuery(string query )
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                List<string> ret = new List<string>();
                while (reader.Read())
                {
                    ret.Add((string)reader[0]);
                }

                return ret.ToArray();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                try
                {
                    conn.Close();
                }
                catch (Exception)
                {

                    //    throw;
                }

            }
        }
        public string[] GetTables(string dbName)
        {
            try
            {
                string qry = "use "+dbName+";SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG='" + dbName + "'";
                return GetStringColumnFromQuery(qry );
                 
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public DataTable CompareTables(string dbA, string dbB, string table)
        {
            string[] colA, colB;
            colA = GetColumns(dbA, table);
            //colB = GetColumns(dbB, table);
            string strColA;
            strColA = string.Join(",", colA);
            

            string qrtstr = GetQueryString(dbA,dbB,table, strColA,colA[0]);
            DataTable dt = GetQueryTable(qrtstr);
            return dt;
        }

        private DataTable GetQueryTable(string query)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable tbl = new DataTable();
                adp.Fill(tbl);
                return tbl;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                try
                {
                    conn.Close();
                }
                catch (Exception)
                {

                    //    throw;
                }

            }
        }

        private string GetQueryString(string dbA, string dbB, string table, string strColA, string orderby)
        {
            
            string str =
                string.Format(
                    @" SELECT MIN(TableName) as TableName, {0} 
FROM
(
  SELECT 'Table A' as TableName, {0}  FROM {2}.dbo.{1} A
  UNION ALL
  SELECT 'Table B' as TableName,  {0}  FROM {3}.dbo.{1} A
  
) tmp
GROUP BY {0}
HAVING COUNT(*) = 1
ORDER BY {4}",
                    strColA, table ,dbA, dbB, orderby);
            return str;
        }

        public string[] GetColumns(string dbname, string table)
        {
            string qry = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.columns WHERE TABLE_CATALOG = '" + dbname +
                         "' AND TABLE_NAME = '" + table + "'  ORDER BY ORDINAL_POSITION";

            try
            {
                
                return GetStringColumnFromQuery(qry);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
