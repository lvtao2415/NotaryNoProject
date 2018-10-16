using NotaryNoProject.GloBal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NotaryNoProject
{
    public partial class MegBox : Form
    {
        public MegBox()
        {
            InitializeComponent();
        }
        private DataTable _dtExcel = new DataTable();
        public MegBox(DataTable dtExcel)
        {
            _dtExcel = dtExcel;
            InitializeComponent();
        }
        private void MegBox_Load(object sender, EventArgs e)
        {
            labelMsg.Text = "正在导入数据中...";
            Dictionary<string, string> dicName = new Dictionary<string, string>();
            ArrayList listName = new ArrayList() { "项目名称", "认筹号", "登记号", "客户姓名", "证件号码", "手机号码", "客户姓名2", "证件号码2", "手机号码2", "通信地址", "置业顾问" };
            for (int j = 0; j < _dtExcel.Columns.Count; j++)
            {
                string curName = _dtExcel.Columns[j].ColumnName.Trim();
                if (listName.Contains(curName))
                {
                    dicName.Add(curName, _dtExcel.Columns[j].ColumnName.ToString());
                    listName.Remove(curName);
                }
            }
            Boolean hasSignNo = true;
            string strError = "";
            int missNum = 1;
            foreach (string str in listName)
            {
                if (str == "登记号")
                {
                    hasSignNo = false;
                }
                else
                {
                    strError += str + ",";
                    if (missNum % 3 == 0)
                    {
                        strError += "\r\n";
                    }
                    missNum++;
                }
            }
            if (strError.Length > 0)
            {
                labelMsg.Text = "缺少信息项：\r\n" + strError.TrimEnd(',') + "！\r\n请重新下载导入模板！";
            }
            else
            {
                string commandCustomer = string.Format(@"select * from CustomerInfos where DelStaus=0 and CustomerStaus=1 ");
                DataTable dtCustomer = HelpSQL.Search(commandCustomer.ToString());
                //"项目名称", "认筹号", "登记号", "客户姓名", "证件号码", "手机号码", "客户姓名2", "证件号码2", "手机号码2", "通信地址", "置业顾问"
                ArrayList listCustomerCard = new ArrayList();
                StringBuilder listError = new StringBuilder();
                StringBuilder listCommand = new StringBuilder();
                string bachID = Guid.NewGuid().ToString();
                string curDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                int startNum = 1;
                listCommand.Append("insert into CustomerInfos(ProjectName,NotaryNo,SignNo,CustomerFirstName,CustomerFirstCard,CustomerFirstPhone,CustomerSecondName,CustomerSecondCard,CustomerSecondPhone,CustomerAddress,CustomerStaus,CreateDate,DelStaus,PurchaseManager) ");
                for (int i = 1; i < _dtExcel.Rows.Count; i++)
                {
                    string projectName = Helper.ToStr(_dtExcel.Rows[i][dicName["项目名称"]]).Trim();
                    if (projectName == "本行留空")
                    {
                        startNum = i + 1;
                    }
                    else
                    {
                        string notaryNo = Helper.ToStr(_dtExcel.Rows[i][dicName["认筹号"]]).Trim();
                        string signNo = Helper.ToStr(_dtExcel.Rows[i][dicName["认筹号"]]).Trim();
                        if (hasSignNo)
                            signNo = Helper.ToStr(_dtExcel.Rows[i][dicName["登记号"]]).Trim();
                        string customerFirstName = Helper.ToStr(_dtExcel.Rows[i][dicName["客户姓名"]]).Trim();
                        string customerFirstCard = Helper.ToStr(_dtExcel.Rows[i][dicName["证件号码"]]).Trim();
                        string customerFirstPhone = Helper.ToStr(_dtExcel.Rows[i][dicName["手机号码"]]).Trim();
                        string customerSecondName = Helper.ToStr(_dtExcel.Rows[i][dicName["客户姓名2"]]).Trim();
                        string customerSecondCard = Helper.ToStr(_dtExcel.Rows[i][dicName["证件号码2"]]).Trim();
                        string customerSecondPhone = Helper.ToStr(_dtExcel.Rows[i][dicName["手机号码2"]]).Trim();
                        string customerAddress = Helper.ToStr(_dtExcel.Rows[i][dicName["通信地址"]]).Trim();
                        string purchaseManager = Helper.ToStr(_dtExcel.Rows[i][dicName["置业顾问"]]).Trim();

                        if (projectName.Length == 0)
                            listError.Append("第" + (i) + "行的[项目名称]信息不能为空；\r\n");
                        if (signNo.Length == 0)
                            listError.Append("第" + (i) + "行的[认筹号]信息不能为空；\r\n");
                        if (customerFirstName.Length == 0 || customerFirstName.Length > 50)
                            listError.Append("第" + (i) + "行的[客户名称]信息必须1到50文字内；\r\n");
                        if (customerFirstCard.Length == 0)
                            listError.Append("第" + (i) + "行的[证件号码]不能为空；\r\n");
                        else if (!Helper.IsLicense(customerFirstCard) && !Helper.IsCard(customerFirstCard))
                            listError.Append("第" + (i) + "行的[证件号码]信息格式有误；\r\n");
                        if (dtCustomer.Select("CustomerFirstCard='" + customerFirstCard + "' or CustomerSecondCard='" + customerFirstCard + "'").Length > 0)
                            listError.Append("第" + (i) + "行的[证件号码]信息系统中已存在该号码；\r\n");
                        //if (customerFirstPhone.Length == 0)
                        //    listError.Append("第" + (i) + "行的[手机号码]信息不能为空；\r\n");

                        if (customerSecondCard != "")
                        {
                            if (customerSecondName.Length == 0 || customerSecondName.Length > 50)
                                listError.Append("第" + (i) + "行的[客户名称2]信息必须1到50文字内；\r\n");
                            if (!Helper.IsLicense(customerSecondCard) && !Helper.IsCard(customerSecondCard))
                                listError.Append("第" + (i) + "行的[证件号码2]信息格式有误；\r\n");
                            if (dtCustomer.Select("CustomerFirstCard='" + customerSecondCard + "' or CustomerSecondCard='" + customerSecondCard + "'").Length > 0)
                                listError.Append("第" + (i) + "行的[证件号码2]信息系统中已存在该号码；\r\n");
                            //if (customerSecondPhone.Length == 0)
                            //    listError.Append("第" + (i) + "行的[手机号码2]信息不能为空；\r\n");
                        }

                        listCommand.AppendFormat(" {0} select '{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}'",
                                                  (i == startNum ? "" : "union all"), projectName, notaryNo, signNo, customerFirstName, customerFirstCard, customerFirstPhone,
                                                  customerSecondName, customerSecondCard, customerSecondPhone, customerAddress, 1, curDate, 0, purchaseManager);
                    }
                }
                if (listError.Length > 0)
                {
                    textBoxError.Text = listError.ToString();
                    textBoxError.Visible = true;
                }
                else
                {
                    int importNum = HelpSQL.Add(listCommand.ToString());
                    if (importNum > 0)
                    {
                        labelMsg.Text = "信息导入成功！";
                        labelMsg.ForeColor = Color.Green;
                    }
                }
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
