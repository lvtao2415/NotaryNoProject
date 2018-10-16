using System;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace WindowsForms.appCode
{
    public class PublicBase
    {
        public static string[] Colorstr
        {
            get
            {
                string[] colorstr1 = new string[] { "8BBA00", "F6BD0F", "FF8E46", "AFD8F8", "008E8E", "D64646", "F6BD0F", "588526", "B3AA00", "008ED6", "9D080D", "A186BE", "99FFFF", "CCCCFF", "FFCCFF", "99CCCC", "CCCC99", "FFCC99", "99CC99", "FFCC66", "CCCC66", "99CC66", "FF99FF", "CC99FF", "9999FF", "FF0000", "CC9966", "999966", "FF9933", "FF66FF", "990033", "660099", "666FF", "000CC", "9933CC", "FFCC00", "CC6699", "663300", "FF6699", "FF6600", "FF3366", "993366", "9966CC", "3300CC", "00CCFF", "FF0000", "FFCC66", "FF9900", "33CCCC", "00CC99", "FF6633", "33FF33", "336600", "33FFFF", "3366CC", "FF6666", "CC6600", "FF9933", "00CC99", "6633FF", "999933", "CC66FF", "99300", "9966FF", "663366", "333366", "003300", "0000CC", "6600CC", "660099", "666600", "666666", "66CCFF", "003333", "CCFF00", "333333", "FF9933", "FFFFCC", "669966", "33ffff", "660000", "99FF00", "99CC66", "CCFFCC", "FFCCFF", "FF6633", "F6FAFF" };
                return colorstr1;
            }
        }

        #region 导出Excel
        public static void show(string message, DataGridView DbG1)
        {
            //====加入报表导出路径值======
            char c = (char)30;
            XmlDocument xd = new XmlDocument();
            string strpath = Application.StartupPath + "\\配置文件\\Profile.xml";
            xd.Load(strpath);
            XmlNode xn = xd.DocumentElement;
            string path = xn.ChildNodes[1]["Path"].InnerText;


            if (!File.Exists(path))//Directory.Exists
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                StreamWriter sw = new StreamWriter(path + "\\" + message + DateTime.Now.ToString("yyyyMMdd") + ".xls", false, System.Text.Encoding.Unicode);
                StringBuilder sb = new StringBuilder();
                sb.Append("<table border=\"1\" cellspacing=\"0\" style=\"border-collapse:collapse;\">");
                sb.Append("<tr>");
                for (int i = 0; i < DbG1.Columns.Count; i++)
                {
                    sb.Append("<td>" + DbG1.Columns[i].HeaderText + "</td>");
                }
                sb.Append("</tr>");

                for (int j = 0; j < DbG1.Rows.Count; j++)
                {
                    sb.Append("<tr>");
                    for (int k = 0; k < DbG1.Columns.Count; k++)
                    {
                        sb.Append("<td>" + c + DbG1.Rows[j].Cells[k].Value.ToString() + "</td>");

                    }
                    sb.Append("</tr>");

                }
                sw.WriteLine(sb);
                sw.Close();
                MessageBox.Show(message + "已经成功以Excel文件格式保存到指定的目录下！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("文件正被另一个人或程序使用，关闭任何使用这个文件的程序，请重试！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 将Datatable导出为excel
        /// </summary>
        /// <param name="dt">datatable数据源</param>
        /// <param name="xlsSaveFileName">保存路径。包括文件名</param>
        /// <param name="sheetName">excel sheet名称 </param>
        /// <returns>导出成功与失败</returns>
        public static bool ConvertDataTableToExcel(System.Data.DataTable dt, string xlsSaveFileName, string sheetName)
        {
            FileStream fs = new FileStream(xlsSaveFileName, FileMode.Create);
            try
            {
                HSSFWorkbook newBook = new HSSFWorkbook();
                HSSFSheet newSheet = (HSSFSheet)newBook.CreateSheet(sheetName);//新建工作簿

                HSSFRow headerRow = (HSSFRow)newSheet.CreateRow(0);   // handling header.       
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    headerRow.CreateCell(i).SetCellValue(dt.Columns[i].ToString());        // handling value. 
                }
                int rowIndex = 1;
                foreach (DataRow row in dt.Rows)
                {
                    HSSFRow dataRow = (HSSFRow)newSheet.CreateRow(rowIndex);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        dataRow.CreateCell(i).SetCellValue(row[i].ToString());
                    }
                    rowIndex++;
                }

                newBook.Write(fs);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                fs.Close();
            }
        }
        
        /// <summary>
        /// 将Datatable导出为excel
        /// </summary>
        /// <param name="dt">datatable数据源</param>
        /// <param name="xlsSaveFileName">保存路径。包括文件名</param>
        /// <param name="sheetName">excel sheet名称 </param>
        /// <returns>导出成功与失败</returns>
        public static bool ConvertDataTableToExcel(System.Data.DataTable dt, string xlsSaveFileName, string sheetName,int width)
        {
            FileStream fs = new FileStream(xlsSaveFileName, FileMode.Create);
            try
            {
                HSSFWorkbook newBook = new HSSFWorkbook();
                ISheet newSheet = newBook.CreateSheet(sheetName);//新建工作簿
                newSheet.DefaultColumnWidth = width;

                IRow headerRow = newSheet.CreateRow(0);

                ICellStyle cellStyle = CellStyleFirst(newBook);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    var cell = (HSSFCell)headerRow.CreateCell(i);
                    cell.SetCellValue(dt.Columns[i].ToString());
                    cell.CellStyle = cellStyle;
                }
                
                int rowIndex = 1;
                ICellStyle cellTxtStyle = CellStyleSecond(newBook);
                foreach (DataRow row in dt.Rows)
                {
                    IRow dataRow = newSheet.CreateRow(rowIndex);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        ICell cell = dataRow.CreateCell(i);
                        cell.SetCellValue(row[i].ToString());
                        cell.CellStyle = cellTxtStyle;
                        //cell.SetCellType(NPOI.SS.UserModel.CellType.STRING);
                    }
                    rowIndex++;
                }

                newBook.Write(fs);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                fs.Close();
            }
        }
        /// <summary>
        /// 将Datatable导出为excel
        /// </summary>
        /// <param name="dt">datatable数据源</param>
        /// <param name="xlsSaveFileName">保存路径。包括文件名</param>
        /// <param name="sheetName">excel sheet名称 </param>
        /// <returns>导出成功与失败</returns>
        public static bool ConvertDataGridViewToExcel(DataGridView dg, string xlsSaveFileName, string sheetName)
        {
            FileStream fs = new FileStream(xlsSaveFileName, FileMode.Create);
            try
            {
                HSSFWorkbook newBook = new HSSFWorkbook();
                HSSFSheet newSheet = (HSSFSheet)newBook.CreateSheet(sheetName);//新建工作簿

                HSSFRow headerRow = (HSSFRow)newSheet.CreateRow(0);   // handling header.       
                for (int i = 0; i < dg.Columns.Count; i++)
                {
                    if (dg.Columns[i].Visible == false) //被隐藏的列不导出
                    {
                        //if (i !=0)
                        //headerRow.CreateCell(i-1).SetCellValue(dg.Columns[i].HeaderText.ToString());   
                    }
                    else headerRow.CreateCell(i).SetCellValue(dg.Columns[i].HeaderText.ToString());        // handling value. 
                }
                int rowIndex = 1;
                foreach (DataGridViewRow row in dg.Rows)
                {
                    HSSFRow dataRow = (HSSFRow)newSheet.CreateRow(rowIndex);
                    for (int i = 0; i < dg.Columns.Count; i++)
                    {
                        string val = row.Cells[i].Value == null ? "" : row.Cells[i].Value.ToString();
                        if (dg.Columns[i].Visible == false)
                        {
                            //if (i != 0)
                            //{
                            //    dataRow.CreateCell(i - 1).SetCellValue(val);
                            //}
                        }
                        else
                        {
                            dataRow.CreateCell(i).SetCellValue(val);
                        }
                    }
                    rowIndex++;
                }

                newBook.Write(fs);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                fs.Close();
            }
        }
        /// <summary>
        /// 将listview数据导出成Excel文件
        /// </summary>
        /// <param name="lv">listview</param>
        /// <param name="xlsSaveFileName">导出文件名称</param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static bool ConvertListViewToExcel(ListView lv, string xlsSaveFileName, string sheetName)
        {
            FileStream fs = new FileStream(xlsSaveFileName, FileMode.Create);
            try
            {
                HSSFWorkbook newBook = new HSSFWorkbook();
                HSSFSheet newSheet = (HSSFSheet)newBook.CreateSheet(sheetName);//新建工作簿
                newSheet.DefaultColumnWidth = 15;
                HSSFRow headerRow = (HSSFRow)newSheet.CreateRow(0);   // handling header.       
                for (int i = 0; i < lv.Columns.Count; i++)
                {
                    headerRow.CreateCell(i).SetCellValue(lv.Columns[i].Text);        // handling value. 
                }
                int rowIndex = 1;
                foreach (ListViewItem row in lv.Items)
                {
                    HSSFRow dataRow = (HSSFRow)newSheet.CreateRow(rowIndex);

                    for (int i = 0; i < lv.Columns.Count; i++)
                    {
                        dataRow.CreateCell(i).SetCellValue(row.SubItems[i].Text);
                    }
                    rowIndex++;
                }

                newBook.Write(fs);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                fs.Close();
            }
        }

        #endregion

        #region
        /// <summary>
        /// 设置单元格样式（字体白色，背景灰色）
        /// </summary>
        /// <param name="workbook">表格对象</param>
        /// <returns>导出成功与失败</returns>
        public static ICellStyle CellStyleFirst(IWorkbook workbook)
        {
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 11;//设置字体大小   
            font.Boldweight = 500;//粗体显示   
            font.Color = IndexedColors.White.Index;//白色字体

            ICellStyle cellStyle = workbook.CreateCellStyle();
            cellStyle.FillForegroundColor = IndexedColors.Grey40Percent.Index;// 设置背景色  
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.SetFont(font);//选择创建的字体格式 

            return cellStyle;
        }

        /// <summary>
        /// 设置单元格样式（字体白色，背景灰色）
        /// </summary>
        /// <param name="workbook">表格对象</param>
        /// <returns>导出成功与失败</returns>
        public static ICellStyle CellStyleSecond(IWorkbook workbook)
        {
                ICellStyle cellStyle = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyle.DataFormat = format.GetFormat("@");
                 
                return cellStyle;
        }
        #endregion

        #region 数据量大导出excel（数据量>=65536）
        #region 创建excel表头
        //ListView
        public static void CreateHeader(ListView lv, HSSFSheet newSheet)
        {
            newSheet.DefaultColumnWidth = 15;
            HSSFRow headerRow = (HSSFRow)newSheet.CreateRow(0);   // handling header.       
            for (int i = 0; i < lv.Columns.Count; i++)
            {
                headerRow.CreateCell(i).SetCellValue(lv.Columns[i].Text);        // handling value. 
            }

        }
        //DataGridView
        public static void CreateHeader(DataGridView datagridview, HSSFSheet newSheet)
        {
            newSheet.DefaultColumnWidth = 15;
            HSSFRow headerRow = (HSSFRow)newSheet.CreateRow(0);   // handling header.       
            for (int i = 0; i < datagridview.Columns.Count; i++)
            {
                headerRow.CreateCell(i).SetCellValue(datagridview.Columns[i].HeaderText);        // handling value. 
            }
        }
        #endregion

        #region  插入数据行
        //ListView导出
        public static bool InsertRow(ListView lv, string sheetName, string xlsSaveFileName)
        {
            FileStream fs = null;
            int rowIndex = 1;
            try
            {

                int sheetCount = 1;
                string tempname = string.Empty;
                if (lv.Items.Count < 65536) tempname = xlsSaveFileName + ".xls";
                else tempname = xlsSaveFileName + "(" + sheetCount + ")" + ".xls";
                HSSFWorkbook newBook = new HSSFWorkbook();
                HSSFSheet newsheet = null;
                newsheet = (HSSFSheet)newBook.CreateSheet(sheetName + sheetCount);
                CreateHeader(lv, newsheet);
                foreach (ListViewItem row in lv.Items)
                {
                    HSSFRow dataRow = (HSSFRow)newsheet.CreateRow(rowIndex);
                    for (int i = 0; i < lv.Columns.Count; i++)
                    {
                        dataRow.CreateCell(i).SetCellValue(row.SubItems[i].Text);
                    }
                    rowIndex++;
                    if (rowIndex == 65536)
                    {
                        fs = new FileStream(tempname, FileMode.Create);
                        newBook.Write(fs);
                        newBook.Dispose();
                        fs.Close();
                        newBook = new HSSFWorkbook();
                        rowIndex = 1;
                        sheetCount++;
                        newsheet = (HSSFSheet)newBook.CreateSheet(sheetName + sheetCount);
                        CreateHeader(lv, newsheet);
                        tempname = xlsSaveFileName + "(" + sheetCount + ")" + ".xls";
                    }
                }
                if (rowIndex < 65536)
                {
                    fs = new FileStream(tempname, FileMode.Create);
                    newBook.Write(fs);
                    newBook.Dispose();
                    fs.Close();
                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "--" + rowIndex + "--" + e.StackTrace);
                return false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return true;
        }
        //DataGridView导出
        public static bool InsertRow(DataGridView datagridview, string sheetName, string xlsSaveFileName)
        {
            FileStream fs = null;
            int rowIndex = 1;
            try
            {

                int sheetCount = 1;
                string tempname = string.Empty;
                if (datagridview.Rows.Count < 65536) tempname = xlsSaveFileName + ".xls";
                else tempname = xlsSaveFileName + "(" + sheetCount + ")" + ".xls";
                HSSFWorkbook newBook = new HSSFWorkbook();
                HSSFSheet newsheet = null;
                newsheet = (HSSFSheet)newBook.CreateSheet(sheetName + sheetCount);

                CreateHeader(datagridview, newsheet);

                foreach (DataGridViewRow row in datagridview.Rows)
                {
                    HSSFRow dataRow = (HSSFRow)newsheet.CreateRow(rowIndex);
                    for (int i = 0; i < dataRow.Cells.Count; i++)
                    {
                        string val = row.Cells[i].Value == null ? "" : row.Cells[i].Value.ToString();
                        dataRow.CreateCell(i).SetCellValue(val);
                    }
                    rowIndex++;
                    if (rowIndex == 65536)
                    {
                        fs = new FileStream(tempname, FileMode.Create);
                        newBook.Write(fs);
                        newBook.Dispose();
                        fs.Close();
                        newBook = new HSSFWorkbook();
                        rowIndex = 1;
                        sheetCount++;
                        newsheet = (HSSFSheet)newBook.CreateSheet(sheetName + sheetCount);
                        CreateHeader(datagridview, newsheet);
                        tempname = xlsSaveFileName + "(" + sheetCount + ")" + ".xls";
                    }
                }
                if (rowIndex < 65536)
                {
                    fs = new FileStream(tempname, FileMode.Create);
                    newBook.Write(fs);
                    newBook.Dispose();
                    fs.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "--" + rowIndex + "--" + e.StackTrace);
                return false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return true;
        }
        #endregion

        #endregion

        #region
        /// <summary>
        /// 字符串反转
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Reverse(string data)
        {

            int i, LenData;
            string datastr;
            if (string.IsNullOrEmpty(data))
            {
                return "";
            }
            datastr = "";
            //data = Evestr(data);
            LenData = data.Trim().Length / 2;
            for (i = LenData - 1; i >= 0; i--)
            {
                datastr = datastr + data.Trim().Substring(2 * i, 2);
            }
            return datastr;

        }

        public static string show(string str)
        {
            char[] c = str.ToCharArray();
            System.Array.Reverse(c);
            return new string(c);
        }

        public static string TostrN(string data, int len)//将字符串格式化成想要的长度
        {

            if (string.IsNullOrEmpty(data))
            {
                return "";
            }
            int i;
            int lendata = len - data.Trim().Length;
            for (i = 1; i <= lendata; i++)
            {
                data = "0" + data;
            }
            return data;
        }

        public static void writeXML(string fileName, string userName)
        {
            XmlDocument myDoc = new XmlDocument();
            myDoc.Load(fileName);

            //搜索是否存在
            System.Xml.XmlNodeList nodes = myDoc.SelectNodes("//User");
            int flag = 0; //是否存在改用户名，0 不存在，1 存在

            if (nodes != null)
            {
                foreach (System.Xml.XmlNode xn in nodes)
                {
                    if (xn.SelectSingleNode("UserName").InnerText == userName)
                        flag = 1;
                }
            }
            if (flag == 0)
            {
                XmlElement ele = myDoc.CreateElement("UserName");
                XmlText text = myDoc.CreateTextNode(userName);
                XmlNode newEle = myDoc.CreateNode("element", "User", "");
                newEle.AppendChild(ele);
                newEle.LastChild.AppendChild(text);

                XmlElement root = myDoc.DocumentElement;
                root.AppendChild(newEle);
                myDoc.Save(fileName);
            }
        }
        public static string[] readXML(string fileName)
        {
            XmlDocument myDoc = new XmlDocument();
            myDoc.Load(fileName);

            //搜索是否存在
            System.Xml.XmlNodeList nodes = myDoc.SelectNodes("//User");
            string[] list = new string[nodes.Count];
            if (nodes != null)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    list[i] = nodes[i].SelectSingleNode("UserName").InnerText;
                }
            }
            return list;
        }
        #endregion
    }
}
