using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NotaryNoProject.GloBal
{
    public class HelpSQL
    {
        private static string txtConn = System.Configuration.ConfigurationSettings.AppSettings["connectionstring"];
        public static DataSet Search(string command, string name)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(txtConn))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter dataRead = new SqlDataAdapter(command, connection);
                        dataRead.Fill(ds, "ds");
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        ErrorMsg();
                    }
                }
            }
            catch (SqlException e)
            {
                ErrorMsg();
            }
            return ds;
        }

        public static DataTable Search(string command)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(txtConn))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter dataRead = new SqlDataAdapter(command, connection);
                        dataRead.Fill(dt);
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        ErrorMsg();
                    }
                }
            }
            catch (SqlException e)
            {
                ErrorMsg();
            }
            return dt;
        }

        public static int Add(string command)
        {
            int num = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(txtConn))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cm = new SqlCommand(command, connection);
                        num = cm.ExecuteNonQuery();
                        return num;
                    }
                    catch
                    {
                        return num;
                    }
                }
            }
            catch (SqlException e)
            {
                return num;
            }
        }
        public static int Update(string command)
        {
            int num = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(txtConn))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cm = new SqlCommand(command, connection);
                        num = cm.ExecuteNonQuery();
                        return num;
                    }
                    catch
                    {
                        return num;
                    }
                }
            }
            catch (SqlException e)
            {
                return num;
            }
        }
        public static int Delete(string command)
        {
            int num = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(txtConn))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cm = new SqlCommand(command, connection);
                        num =  cm.ExecuteNonQuery();
                        return num;
                    }
                    catch
                    {
                        return num;
                    }
                }
            }
            catch (SqlException e)
            {
                return num;
            }
        }
        public static int GetCount(string command)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(txtConn))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter dataRead = new SqlDataAdapter(command, connection);
                        dataRead.Fill(dt);
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            catch (SqlException e)
            {
                ErrorMsg();
            }
            return dt.Rows.Count;
        }
        public static Object GetFirstField(string command)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(txtConn))
                {
                    using (SqlCommand cmd = new SqlCommand(command, connection))
                    {
                        try
                        {
                            connection.Open();
                            object obj = cmd.ExecuteScalar();
                            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                            {
                                return null;
                            }
                            else
                            {
                                return obj;
                            }
                        }
                        catch (System.Data.SqlClient.SqlException e)
                        {
                            connection.Close();
                            ErrorMsg();
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                ErrorMsg();
            }
            return null;
        }

        public static bool ConnectionTest()
        {
            Boolean IsConnection = false;
            using (SqlConnection connection = new SqlConnection(txtConn))
            {
                try
                {
                    connection.Open();
                    IsConnection = true;
                }
                catch
                {
                    IsConnection = false;
                }
                finally
                {
                    connection.Close();
                }
            }

            return IsConnection;
        }

        #region 

        public static void ErrorMsg(string msg)
        {
            MessageBox.Show(msg, "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void ErrorMsg()
        {
            MessageBox.Show("数据库连接失败！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
    }
}
