using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;


namespace NotaryNoProject.GloBal
{
    public static class HelpACE
    {
        private static string txtConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\\..\\APP_Data\\NotaryNoData.accdb;Persist Security Info=True;";
        public static DataSet Search(string command, string name)
        {
            DataSet ds = new DataSet();
            OleDbConnection conn = new OleDbConnection(txtConn);
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter(command, conn);
                da.Fill(ds, name);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public static DataTable Search(string command)
        {
            DataTable dt = new DataTable();
            OleDbConnection conn = new OleDbConnection(txtConn);
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter(command, conn);
                da.Fill(dt);
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        public static int Add(string command)
        {
            int num = 0;
            OleDbConnection conn = new OleDbConnection(txtConn);
            try
            {
                conn.Open();
                OleDbCommand da = new OleDbCommand(command, conn);
                num = da.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
            return num;
        }
        public static int Update(string command)
        {
            int num = 0;
            OleDbConnection conn = new OleDbConnection(txtConn);
            try
            {
                conn.Open();
                OleDbCommand da = new OleDbCommand(command, conn);
                num = da.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
            return num;
        }
        public static int Delete(string command)
        {
            int num = 0;
            OleDbConnection conn = new OleDbConnection(txtConn);
            try
            {
                conn.Open();
                OleDbCommand da = new OleDbCommand(command, conn);
                num = da.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
            return num;
        }
        public static int GetCount(string command)
        {
            DataTable dt = new DataTable();
            OleDbConnection conn = new OleDbConnection(txtConn);
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter(command, conn);
                da.Fill(dt);
            }
            finally
            {
                conn.Close();
            }
            return dt.Rows.Count;
        }
        public static Object GetFirstField(string command)
        {
            DataTable dt = new DataTable();
            OleDbConnection conn = new OleDbConnection(txtConn);
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter(command, conn);
                da.Fill(dt);
            }
            finally
            {
                conn.Close();
            }
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0];
            else
                return "";
        }
    }
}
