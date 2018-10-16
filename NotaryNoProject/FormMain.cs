using NotaryNoProject.GloBal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WindowsForms.appCode;

namespace NotaryNoProject
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private int btnType = 0;
        private string lastCode = "";
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string[] listInfo = IDCardReader.ReaderListInfo();
                string getCardID = listInfo[2];
                string curCardID = textReadCard.Text.Trim();

                if (curCardID == "" && (getCardID == null || getCardID == ""))
                {
                    MessageBox.Show("读取失败,请填写身份证号!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (getCardID != "" && getCardID != null)
                {
                    btnType = 1;
                    btnUpCustomer.Visible = true;//判断是否更新资料 阅读器获取身份证信息
                    textReadCard.Text = getCardID;
                    labelc01.Text = listInfo[0];
                    labelc02.Text = listInfo[1];
                    labelc03.Text = listInfo[2];
                    labelc04.Text = listInfo[3];
                    labelc05.Text = listInfo[4];
                    CardSearch(textReadCard.Text.Trim());
                }
                else if (textReadCard.Text.Trim() != "")
                {
                    btnType = 1;
                    NoCardRead();
                    CardSearch(textReadCard.Text.Trim());
                }


            }
            catch (Exception ex) { }

        }
        public void NoCardRead()
        {
            labelc01.Text = "";
            labelc02.Text = "";
            labelc03.Text = "";
            labelc04.Text = "";
            labelc05.Text = "";
            btnUpCustomer.Visible = false;//判断是否更新资料
        }
        public void CardSearch(string cardID)
        {
            try
            {
                string command = string.Format(@"select * from CustomerInfos where DelStaus=0 and (CustomerFirstCard='{0}' or CustomerSecondCard='{0}') ", cardID);
                DataTable dt = HelpSQL.Search(command.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    labeld01.Text = Helper.ToStr(dt.Rows[0]["CustomerFirstName"]);
                    labeld02.Text = Helper.ToStr(dt.Rows[0]["CustomerFirstSex"]);
                    labeld03.Text = Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]);
                    if (Helper.IsCard(Helper.ToStr(dt.Rows[0]["CustomerFirstCard"])))
                        labeld04.Text = Helper.ToDateFirst(Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]).Substring(6, 4) + "-" + Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]).Substring(10, 2) + "-" + Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]).Substring(12, 2));

                    if (Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Length > 0)
                    {
                        labeld01.Text = labeld01.Text + "、" + Helper.ToStr(dt.Rows[0]["CustomerSecondName"]);
                        if (Helper.ToStr(dt.Rows[0]["CustomerSecondSex"]).Length > 0)
                            labeld02.Text = labeld02.Text + "、" + Helper.ToStr(dt.Rows[0]["CustomerSecondSex"]);
                        labeld032.Text = Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]);
                        if (Helper.IsCard(Helper.ToStr(dt.Rows[0]["CustomerSecondCard"])))
                            labeld04.Text = labeld04.Text + "、" + Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Substring(6, 4) + "-" + Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Substring(10, 2) + "-" + Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Substring(12, 2);
                    }
                    else
                    {
                        labeld032.Text = "";
                    }
                    labeld05.Text = Helper.ToStr(dt.Rows[0]["CustomerAddress"]);
                    labeld06.Text = Helper.ToStr(dt.Rows[0]["NotaryNo"]);
                    labeld07.Text = Helper.ToStr(dt.Rows[0]["SignNo"]);
                    labeld08.Text = Helper.ToFirstBollen(dt.Rows[0]["SignStaus"]);
                    labeld09.Text = Helper.ToDateThree(dt.Rows[0]["SignDate"]);
                    IsSign(Helper.ToStr(dt.Rows[0]["SignStaus"]));
                    lastCode = textReadCard.Text.Trim();
                    textReadCard.Text = "";
                }
                else
                    MessageBox.Show("未找到相应信息!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lastCode == "")
            {
                MessageBox.Show("请填写信息!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (button2.Text == "取消签到")
            {
                if (btnType == 1)
                {
                    string command = string.Format(@"Update CustomerInfos set SignStaus=0,SignDate=null where DelStaus=0 and (CustomerFirstCard='{0}' or CustomerSecondCard='{0}') ", lastCode);
                    int upNum = HelpSQL.Update(command.ToString());
                    if (upNum > 0)
                    {
                        labeld08.Text = "否";
                        labeld09.Text = "";
                        button2.Text = Helper.SignText(_isSignAndPrint);
                    }
                    else
                        MessageBox.Show("取消签到操作失败!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btnType == 2)
                {
                    string command = string.Format(@"Update CustomerInfos set SignStaus=0,SignDate=null where DelStaus=0 and NotaryNo='{0}' ", lastCode);
                    int upNum = HelpSQL.Update(command.ToString());
                    if (upNum > 0)
                    {
                        labeld08.Text = "否";
                        labeld09.Text = "";
                        button2.Text = Helper.SignText(_isSignAndPrint);
                    }
                    else
                        MessageBox.Show("取消签到操作失败!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btnType == 3)
                {
                    string command = string.Format(@"Update CustomerInfos set SignStaus=0,SignDate=null where DelStaus=0 and (CustomerFirstPhone='{0}' or CustomerSecondPhone='{0}') ", lastCode);
                    int upNum = HelpSQL.Update(command.ToString());
                    if (upNum > 0)
                    {
                        labeld08.Text = "否";
                        labeld09.Text = "";
                        button2.Text = Helper.SignText(_isSignAndPrint);
                    }
                    else
                        MessageBox.Show("取消签到操作失败!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                string curDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (btnType == 1)
                {
                    string command = string.Format(@"Update CustomerInfos set SignStaus=1,SignDate='{1}' where DelStaus=0 and (CustomerFirstCard='{0}' or CustomerSecondCard='{0}') ", lastCode, curDate);
                    int upNum = HelpSQL.Update(command.ToString());
                    if (upNum > 0)
                    {
                        labeld08.Text = "是";
                        labeld09.Text = curDate;
                        button2.Text = "取消签到";
                        SignAndPrint();
                    }
                    else
                        MessageBox.Show("签到操作失败!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btnType == 2)
                {
                    string command = string.Format(@"Update CustomerInfos set SignStaus=1,SignDate='{1}' where DelStaus=0 and NotaryNo='{0}' ", lastCode, curDate);
                    int upNum = HelpSQL.Update(command.ToString());
                    if (upNum > 0)
                    {
                        labeld08.Text = "是";
                        labeld09.Text = curDate;
                        button2.Text = "取消签到";
                        SignAndPrint();
                    }
                    else
                        MessageBox.Show("签到操作失败!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (btnType == 3)
                {
                    string command = string.Format(@"Update CustomerInfos set SignStaus=1,SignDate='{1}' where DelStaus=0 and (CustomerFirstPhone='{0}' or CustomerSecondPhone='{0}') ", lastCode, curDate);
                    int upNum = HelpSQL.Update(command.ToString());
                    if (upNum > 0)
                    {
                        labeld08.Text = "是";
                        labeld09.Text = curDate;
                        button2.Text = "取消签到";
                        SignAndPrint();
                    }
                    else
                        MessageBox.Show("签到操作失败!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }


        }

        private static bool _isSignAndPrint;
        public void SignAndPrint()
        {
            if (_isSignAndPrint)
            {
                LabelPrint();
            }
        }

        public void LabelPrint()
        {
            string[] infoList = new string[6];
            if (btnType == 1)
            {
                string command = string.Format(@"select * from CustomerInfos where DelStaus=0 and (CustomerFirstCard='{0}' or CustomerSecondCard='{0}') ", lastCode);
                DataTable dt = HelpSQL.Search(command.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    infoList[0] = Helper.ToStr(dt.Rows[0]["ProjectName"]);
                    infoList[1] = Helper.ToStr(dt.Rows[0]["NotaryNo"]);
                    infoList[2] = Helper.ToStr(dt.Rows[0]["SignNo"]);
                    infoList[3] = Helper.ToStr(dt.Rows[0]["CustomerFirstName"]);
                    if (Helper.ToStr(dt.Rows[0]["CustomerSecondName"]) != "")
                        infoList[3] = infoList[3] + "、" + Helper.ToStr(dt.Rows[0]["CustomerSecondName"]);

                    if (Helper.IsCard(Helper.ToStr(dt.Rows[0]["CustomerFirstCard"])))
                        infoList[4] = Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]).Substring(0, 8) + "******" + Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]).Substring(14);
                    else
                        infoList[4] = Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]);
                    if (Helper.IsCard(Helper.ToStr(dt.Rows[0]["CustomerSecondCard"])))
                        infoList[5] = Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Substring(0, 8) + "******" + Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Substring(14);
                    else
                        infoList[5] = Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]);

                    BarcodePrint.PrintInfo(infoList);
                }
                else
                    MessageBox.Show("打印操作失败!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (btnType == 2)
            {
                string command = string.Format(@"select * from CustomerInfos where DelStaus=0 and NotaryNo='{0}' ", lastCode);
                DataTable dt = HelpSQL.Search(command.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    infoList[0] = Helper.ToStr(dt.Rows[0]["ProjectName"]);
                    infoList[1] = Helper.ToStr(dt.Rows[0]["NotaryNo"]);
                    infoList[2] = Helper.ToStr(dt.Rows[0]["SignNo"]);
                    infoList[3] = Helper.ToStr(dt.Rows[0]["CustomerFirstName"]);
                    if (Helper.ToStr(dt.Rows[0]["CustomerSecondName"]) != "")
                        infoList[3] = infoList[3] + "、" + Helper.ToStr(dt.Rows[0]["CustomerSecondName"]);
                    if (Helper.IsCard(Helper.ToStr(dt.Rows[0]["CustomerFirstCard"])))
                        infoList[4] = Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]).Substring(0, 8) + "******" + Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]).Substring(14);
                    else
                        infoList[4] = Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]);
                    if (Helper.IsCard(Helper.ToStr(dt.Rows[0]["CustomerSecondCard"])))
                        infoList[5] = Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Substring(0, 8) + "******" + Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Substring(14);
                    else
                        infoList[5] = Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]);

                    BarcodePrint.PrintInfo(infoList);
                }
                else
                    MessageBox.Show("打印操作失败!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (btnType == 3)
            {
                string command = string.Format(@"select * from CustomerInfos where DelStaus=0 and (CustomerFirstPhone='{0}' or CustomerSecondPhone='{0}') ", lastCode);
                DataTable dt = HelpSQL.Search(command.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    infoList[0] = Helper.ToStr(dt.Rows[0]["ProjectName"]);
                    infoList[1] = Helper.ToStr(dt.Rows[0]["NotaryNo"]);
                    infoList[2] = Helper.ToStr(dt.Rows[0]["SignNo"]);
                    infoList[3] = Helper.ToStr(dt.Rows[0]["CustomerFirstName"]);
                    if (Helper.ToStr(dt.Rows[0]["CustomerSecondName"]) != "")
                        infoList[3] = infoList[3] + "、" + Helper.ToStr(dt.Rows[0]["CustomerSecondName"]);
                    if (Helper.IsCard(Helper.ToStr(dt.Rows[0]["CustomerFirstCard"])))
                        infoList[4] = Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]).Substring(0, 8) + "******" + Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]).Substring(14);
                    else
                        infoList[4] = Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]);
                    if (Helper.IsCard(Helper.ToStr(dt.Rows[0]["CustomerSecondCard"])))
                        infoList[5] = Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Substring(0, 8) + "******" + Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Substring(14);
                    else
                        infoList[5] = Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]);

                    BarcodePrint.PrintInfo(infoList);
                }
                else
                    MessageBox.Show("打印操作失败!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (lastCode == "")
            {
                MessageBox.Show("请填写信息!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                LabelPrint();
            }
        }

        public void IsSign(string signStaus)
        {
            if (signStaus == "1")
            {
                button2.Text = "取消签到";
            }
            else
            {
                button2.Text = Helper.SignText(_isSignAndPrint);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string txtNotaryNo = textReadCard.Text.Trim();
            if (txtNotaryNo == "")
                MessageBox.Show("请填写信息!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                btnType = 2;
                NoCardRead();//判断阅读器资料
                NotaryNoSearch(txtNotaryNo);
            }
        }
        public void NotaryNoSearch(string notaryNo)
        {
            string command = string.Format(@"select * from CustomerInfos where DelStaus=0 and NotaryNo='{0}' ", notaryNo);
            DataTable dt = HelpSQL.Search(command.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                labeld01.Text = Helper.ToStr(dt.Rows[0]["CustomerFirstName"]);
                labeld02.Text = Helper.ToStr(dt.Rows[0]["CustomerFirstSex"]);
                labeld03.Text = Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]);
                if (Helper.IsCard(Helper.ToStr(dt.Rows[0]["CustomerFirstCard"])))
                    labeld04.Text = Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]).Substring(6, 4) + "-" + Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]).Substring(10, 2) + "-" + Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]).Substring(12, 2);
                if (Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Length > 0)
                {
                    labeld01.Text = labeld01.Text + "、" + Helper.ToStr(dt.Rows[0]["CustomerSecondName"]);
                    if (Helper.ToStr(dt.Rows[0]["CustomerSecondSex"]).Length > 0)
                        labeld02.Text = labeld02.Text + "、" + Helper.ToStr(dt.Rows[0]["CustomerSecondSex"]);
                    labeld032.Text = Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]);
                    if (Helper.IsCard(Helper.ToStr(dt.Rows[0]["CustomerSecondCard"])))
                        labeld04.Text = labeld04.Text + "、" + Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Substring(6, 4) + "-" + Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Substring(10, 2) + "-" + Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Substring(12, 2);
                }
                else
                {
                    labeld032.Text = "";
                }
                labeld05.Text = Helper.ToStr(dt.Rows[0]["CustomerAddress"]);
                labeld06.Text = Helper.ToStr(dt.Rows[0]["NotaryNo"]);
                labeld07.Text = Helper.ToStr(dt.Rows[0]["SignNo"]);
                labeld08.Text = Helper.ToFirstBollen(dt.Rows[0]["SignStaus"]);
                labeld09.Text = Helper.ToDateThree(dt.Rows[0]["SignDate"]);
                IsSign(Helper.ToStr(dt.Rows[0]["SignStaus"]));
                lastCode = textReadCard.Text.Trim();
                textReadCard.Text = "";
            }
            else
                MessageBox.Show("未找到相应信息!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string txtPhone = textReadCard.Text.Trim();
            if (txtPhone == "")
                MessageBox.Show("请填写信息!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                btnType = 3;
                NoCardRead();//判断阅读器资料
                PhoneSearch(txtPhone);
            }
        }
        public void PhoneSearch(string phone)
        {
            string command = string.Format(@"select * from CustomerInfos where DelStaus=0 and (CustomerFirstPhone='{0}' or CustomerSecondPhone='{0}') ", phone);
            DataTable dt = HelpSQL.Search(command.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                labeld01.Text = Helper.ToStr(dt.Rows[0]["CustomerFirstName"]);
                labeld02.Text = Helper.ToStr(dt.Rows[0]["CustomerFirstSex"]);
                labeld03.Text = Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]);
                if (Helper.IsCard(Helper.ToStr(dt.Rows[0]["CustomerFirstCard"])))
                    labeld04.Text = Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]).Substring(6, 4) + "-" + Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]).Substring(10, 2) + "-" + Helper.ToStr(dt.Rows[0]["CustomerFirstCard"]).Substring(12, 2);
                if (Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Length > 0)
                {
                    labeld01.Text = labeld01.Text + "、" + Helper.ToStr(dt.Rows[0]["CustomerSecondName"]);
                    if (Helper.ToStr(dt.Rows[0]["CustomerSecondSex"]).Length > 0)
                        labeld02.Text = labeld02.Text + "、" + Helper.ToStr(dt.Rows[0]["CustomerSecondSex"]);
                    labeld032.Text = Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]);
                    if (Helper.IsCard(Helper.ToStr(dt.Rows[0]["CustomerSecondCard"])))
                        labeld04.Text = labeld04.Text + "、" + Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Substring(6, 4) + "-" + Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Substring(10, 2) + "-" + Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]).Substring(12, 2);
                }
                else
                {
                    labeld032.Text = "";
                }
                labeld05.Text = Helper.ToStr(dt.Rows[0]["CustomerAddress"]);
                labeld06.Text = Helper.ToStr(dt.Rows[0]["NotaryNo"]);
                labeld07.Text = Helper.ToStr(dt.Rows[0]["SignNo"]);
                labeld08.Text = Helper.ToFirstBollen(dt.Rows[0]["SignStaus"]);
                labeld09.Text = Helper.ToDateThree(dt.Rows[0]["SignDate"]);
                IsSign(Helper.ToStr(dt.Rows[0]["SignStaus"]));
                lastCode = textReadCard.Text.Trim();
                textReadCard.Text = "";
            }
            else
                MessageBox.Show("未找到相应信息!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        static string strConn = "";
        public void CustonersImport()
        {
            //清空数据集
            DataTable dtExcel = new DataTable();
            //打开一个文件选择框
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Excel文件";
            ofd.FileName = "";
            ofd.Filter = "Excel文件(*.xls)|*";
            try
            {
                //选中文件
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    //获取选中文件的路径
                    string filePath = ofd.FileName;
                    string sheetName = "";
                    //获取文件后缀名 
                    if (System.IO.Path.GetExtension(ofd.FileName).ToLower() == (".xls"))
                    {
                        //如果是07以下（.xls）的版本的Excel文件就使用这条连接字符串
                        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ofd.FileName + ";Extended Properties=Excel 8.0;";
                    }
                    else
                    {
                        //如果是07以上(.xlsx)的版本的Excel文件就使用这条连接字符串
                        strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + ofd.FileName + ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'"; //此連接可以操作.xls與.xlsx文件
                    }
                    if (System.IO.Path.GetExtension(ofd.FileName).ToLower().Contains(".xls"))
                    {
                        //打开Excel的连接，设置连接对象
                        OleDbConnection conn = new OleDbConnection(strConn);
                        conn.Open();
                        DataTable sheetNames = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                        //遍历Excel文件获取Excel工作表，获取第一个工作表名称 
                        sheetName = sheetNames.Rows[0][2].ToString();

                        //当前选中的工作表前几行数据，获取数据列
                        OleDbDataAdapter oada = new OleDbDataAdapter("select * from [" + sheetName + "]", strConn);
                        oada.Fill(dtExcel);
                        conn.Close();

                        MegBox megBox = new MegBox(dtExcel);
                        megBox.Owner = this;
                        megBox.Show();
                    }
                    else
                    {
                        MessageBox.Show("excel格式不正确！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导入文件时出错,文件可能正被打开\r\n" + ex.Message.ToString(), "提示");
            }
        }

        private void btnUpCustomer_Click(object sender, EventArgs e)
        {
            string customerName = labelc01.Text.Trim();
            string customerSex = labelc02.Text.Trim();
            string customerCard = labelc03.Text.Trim();
            string customerAddress = labelc05.Text.Trim();

            if (customerCard.Length >= 16)
            {
                string command = string.Format(@"select * from CustomerInfos where DelStaus=0 and (CustomerFirstCard='{0}' or CustomerSecondCard='{0}') ", customerCard);
                DataTable dt = HelpSQL.Search(command.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    string commandUp = "";
                    if (Helper.ToStr(dt.Rows[0]["CustomerSecondCard"]) == customerCard)
                    {
                        commandUp = string.Format("update CustomerInfos set CustomerSecondName='{1}',CustomerSecondSex='{2}',CustomerAddress='{3}' where CustomerSecondCard='{0}' "
                            , customerCard, customerName, customerSex, customerAddress);
                    }
                    else
                    {
                        commandUp = string.Format("update CustomerInfos set CustomerFirstName='{1}',CustomerFirstSex='{2}',CustomerAddress='{3}' where CustomerFirstCard='{0}' "
                            , customerCard, customerName, customerSex, customerAddress);
                    }
                    int upNum = HelpSQL.Update(commandUp.ToString());
                    if (upNum > 0)
                    {
                        MessageBox.Show("系统信息更新成功!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("系统信息更新失败!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                btnUpCustomer.Visible = false;
            }
        }

        private void ItemMenuImport_Click(object sender, EventArgs e)
        {
            CustonersImport();
        }

        private void ItemMenuExport1_Click(object sender, EventArgs e)
        {
            string command = string.Format(@"select ProjectName as '项目名称',NotaryNo as '认筹号',SignNo as '登记号',(case when SignStaus=1 then '已签到' else '未签到' end) as '签到状态',CONVERT(varchar(16),SignDate,120) as '签到时间'
              ,CustomerFirstName as '客户姓名',CustomerFirstCard as '证件号码',CustomerFirstPhone as '手机号码',CustomerSecondName as '客户姓名2',CustomerSecondCard as '证件号码2',CustomerSecondPhone as '手机号码2'
              from CustomerInfos where SignStaus=1
              order by NotaryNo asc,SignNo asc");
            DataTable dt = HelpSQL.Search(command.ToString());
            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型 
            sfd.Filter = "Excel表格（*.xls）|*.xls";
            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;
            sfd.FileName = "已签到客户" + DateTime.Now.ToString("yyyyMMddHH") + ".xls";
            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = sfd.FileName.ToString(); //获得文件路径 
                PublicBase.ConvertDataTableToExcel(dt, localFilePath, "已签到客户列表", 22);
                MessageBox.Show("已成功导出文件!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void ItemMenuExport2_Click(object sender, EventArgs e)
        {
            string command = string.Format(@"select ProjectName as '项目名称',NotaryNo as '认筹号',SignNo as '登记号',(case when SignStaus=1 then '已签到' else '未签到' end) as '签到状态','' as '签到时间'
              ,CustomerFirstName as '客户姓名',CustomerFirstCard as '证件号码',CustomerFirstPhone as '手机号码',CustomerSecondName as '客户姓名2',CustomerSecondCard as '证件号码2',CustomerSecondPhone as '手机号码2'
              from CustomerInfos where SignStaus=0
              order by NotaryNo asc,SignNo asc");
            DataTable dt = HelpSQL.Search(command.ToString());
            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型 
            sfd.Filter = "Excel表格（*.xls）|*.xls";
            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;
            sfd.FileName = "未签到客户" + DateTime.Now.ToString("yyyyMMddHH") + ".xls";
            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = sfd.FileName.ToString(); //获得文件路径 
                PublicBase.ConvertDataTableToExcel(dt, localFilePath, "未签到客户列表", 22);
                MessageBox.Show("已成功导出文件!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //设置签到＆打印标识
            _isSignAndPrint = Helper.IsSignAndPrint();
            button2.Text = Helper.SignText(_isSignAndPrint);

            ConnectLabel.Text = "请稍后，正在连接数据库中...！";
            textReadCard.Enabled = false;
            ThreadPool.QueueUserWorkItem(state => ConnectionTest());
        }

        public void ConnectionTest()
        {
            if (!HelpSQL.ConnectionTest())
            {
                MessageBox.Show("连接数据库失败！请重新设置！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Invoke(new Action(() => ConnectLabel.Text = ""));
            this.Invoke(new Action(() => textReadCard.Enabled = true));
        }

        private void SignTimer_Tick(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(state => SetCurSign());
        }

        public static int SignAllNum = 0;
        public void SetCurSign()
        {
            if (textReadCard.Enabled == false)
            {
                return;
            }
            if (SignAllNum > 0)
            {
                SignAllSum.Text = SignAllNum.ToString();
            }
            else
            {
                string command1 = string.Format(@"select count(1) from CustomerInfos where DelStaus=0 ");
                object oSum1 = HelpSQL.GetFirstField(command1.ToString());
                this.Invoke(new Action(() => SignAllSum.Text = oSum1.ToString()));
                int.TryParse(oSum1.ToString(), out SignAllNum);
            }
            string command2 = string.Format(@"select count(1) from CustomerInfos where DelStaus=0 and SignStaus=1 ");
            object oSum2 = HelpSQL.GetFirstField(command2.ToString());
            this.Invoke(new Action(() => SignYesSum.Text = oSum2.ToString()));
            
            string command3 = string.Format(@"select count(1) from CustomerInfos where DelStaus=0 and SignStaus=0 ");
            object oSum3 = HelpSQL.GetFirstField(command3.ToString());
            this.Invoke(new Action(() => SignNoSum.Text = oSum3.ToString()));
        }

        private void TemplateUsers_Click(object sender, EventArgs e)
        {
            string filePath = Application.StartupPath.Replace("\\bin\\Debug", "").Replace("\\obj\\Debug", "")
                + "\\Template\\客户信息导入模板.xls";
            if (!File.Exists(filePath))
            {
                MessageBox.Show("客户信息导入模板不存在！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型 
            sfd.Filter = "Excel表格（*.xls）|*.xls";
            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;
            sfd.FileName = "客户信息导入模板"+DateTime.Now.ToString("yyyyMMddHH")+".xls";
            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = sfd.FileName.ToString(); //获得文件路径 
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名
                File.Copy(filePath, localFilePath);
                MessageBox.Show("客户信息导入模板已下载成功！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void ItemMenuCustomerExport_Click(object sender, EventArgs e)
        {
            string command = string.Format(@"select ProjectName as '项目名称',NotaryNo as '认筹号',SignNo as '登记号',PurchaseManager as '置业顾问'
              ,(case when ISNULL(CustomerSecondCard,'')='' then CustomerFirstName else CustomerFirstName+'|'+CustomerSecondName end) as '客户姓名'
              ,(case when ISNULL(CustomerSecondCard,'')='' then CustomerFirstCard else CustomerFirstCard+'|'+CustomerSecondCard end) as '证件号码'
              ,(case when ISNULL(CustomerSecondCard,'')='' then CustomerFirstPhone else CustomerFirstPhone+'|'+CustomerSecondPhone end) as '手机号码'
              ,CustomerAddress as '通信地址'
              from CustomerInfos
              order by NotaryNo asc,SignNo asc");
            DataTable dt = HelpSQL.Search(command.ToString());

            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型 
            sfd.Filter = "Excel表格（*.xls）|*.xls";
            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;
            sfd.FileName = "客户信息列表" + DateTime.Now.ToString("yyyyMMddHH") + ".xls";
            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = sfd.FileName.ToString(); //获得文件路径 
                PublicBase.ConvertDataTableToExcel(dt, localFilePath, "客户信息列表", 22);
                MessageBox.Show("已成功导出文件!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
