using System;
using System.Collections.Generic;
using System.IO;
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
            string[] colA, colA_type;
            colA = GetColumns(dbA, table);
            colA_type = GetColumns_Types(dbA, table);
            if( colA.Length == 0 || colA_type.Length == 0 ) return new DataTable(table);
            //colB = GetColumns(dbB, table);
            string strColA, strColA_Sel;
            strColA = "";// string.Join(",", colA);
            strColA_Sel = "";
            for (int i = 0; i < colA_type.Length; i++ )
            {
                if (strColA_Sel.Length > 0)
                {
                    strColA_Sel += ", ";
                    strColA += ", ";
                }
                if (colA_type[i] == "text")
                {
                    strColA_Sel += "CAST([" + colA[i] + "] AS varchar(MAX)) as [" + colA[i] + "]";
                }
                else strColA_Sel += "[" + colA[i] + "]";
                strColA += "["+ colA[i] +"]";
            }

            string qrtstr = GetQueryString(dbA, dbB, table, strColA, "[" + colA[0] + "]", strColA_Sel);
            File.AppendAllText(@"c:\query.sql","--"+table+Environment.NewLine+ qrtstr+Environment.NewLine);
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
                
                adp.Dispose();
                cmd.Dispose();

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

        private string GetQueryString(string dbA, string dbB, string table, string strColA, string orderby, string strColA_sel)
        {
            
            string str =
                string.Format(
                    @" SELECT MIN(DB) as DB, {0} 
FROM
(
  SELECT 'A' as DB, {5}  FROM {2}.dbo.[{1}]  
  UNION ALL
  SELECT 'B' as DB,  {5}  FROM {3}.dbo.[{1}]  
  
) tmp
GROUP BY {0}
HAVING COUNT(*) = 1
ORDER BY {4}",
                    strColA, table, dbA, dbB, orderby, strColA_sel);
            return str;
        }

        public string[] GetColumns(string dbname, string table)
        {
            string qry = "USE " + dbname + ";";
            qry += "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.columns WHERE TABLE_CATALOG = '" + dbname +
                   "' AND TABLE_NAME = '" + (table.Contains("'")? table.Replace("'", "''"): table)
                   + "'  ORDER BY ORDINAL_POSITION ; ";

            try
            {             
                return GetStringColumnFromQuery(qry);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string[] GetColumns_Types(string dbname, string table)
        {
            string qry = "USE " + dbname + ";";
            qry += "SELECT data_type FROM INFORMATION_SCHEMA.columns WHERE TABLE_CATALOG = '" + dbname +
                   "' AND TABLE_NAME = '" + (table.Contains("'") ? table.Replace("'", "''") : table) 
                   + "'  ORDER BY ORDINAL_POSITION";
            try
            {
                return GetStringColumnFromQuery(qry);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TableDiffResult CompareTablesExt(string dbA, string dbB, string tbA)
        {
            string param = "";
            param += " -sourceserver " + Properties.Settings.Default.Server;
            param += " -destinationserver " + Properties.Settings.Default.Server;
            param += " -sourcedatabase " + dbA;
            param += " -destinationdatabase " + dbB;
            param += " -sourcetable " + tbA;
            param += " -destinationtable " + tbA;

            if( !Properties.Settings.Default.UseWndAuth )
            {
                param += " -sourceuser " + Properties.Settings.Default.Username;
                param += " -sourcepassword " + Properties.Settings.Default.Password;

                param += " -destinationuser " + Properties.Settings.Default.Username;
                param += " -destinationpassword " + Properties.Settings.Default.Password;
            }
 
            string ret = Common.RunExtCmd( Properties.Settings.Default.ExternalDiff,param);
            return ExtractDiff(ret);
        }

        private TableDiffResult ExtractDiff(string data)
        {
            TableDiffResult tbl = new TableDiffResult();
            string[] lines = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            bool errfound = false;
            foreach (string line in lines)
            {
                if(!errfound)
                {
                    if(!line.StartsWith("Err"))
                    {
                       
                        continue;
                    }
                    errfound = true;
                    string[] col1s = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    tbl.Key = col1s[1];
                    continue;
                }
                string[] cols = line.Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
                if (cols.Length > 0)
                {
                    if (cols[0] == "Mismatch")
                    {
                        tbl.Diff.Add( cols[1] );
                    }
                    else
                     if (cols[0] == "Src. Only")
                    {
                        tbl.Src.Add(cols[1]);
                    }
                    else
                     if (cols[0] == "Dest. Only")
                    {
                        tbl.Dest.Add(cols[1]);
                    }
                     
                }
            }
            return tbl;
        }


        public DataTable GetItems(string[] items, string key, string name)
        {
            string data = string.Join(",", items);
            string str = "select * from " + name + " where " + key + " in (" + data + ")";
            return GetQueryTable(str);
        }
    }

    public class TableDiffResult
    {
        public string Key;
        public List<string> Diff
        {
            get; set;
        }
        public List<string> Src
        {
            get;
            set;
        }
        public List<string> Dest
        {
            get;
            set;
        }

        public TableDiffResult()
        {
            Diff = new  List<string>();
            Src = new List<string>();
            Dest = new List<string>();
        }
    }
}
