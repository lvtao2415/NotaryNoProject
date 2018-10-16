using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NotaryNoProject.GloBal
{
    public class PrintDriver
    {
        public string printDriver { get; set; }
        public string printNum { get; set; }
        public string[][] printLable { get; set; }
        public string printWidth { get; set; }
        public string printHeight { get; set; }
        public string printSpeed { get; set; }
        public string printDensity { get; set; }
        public string printSensor { get; set; }
        public string printVertical { get; set; }
        public string printOffset { get; set; }

        public PrintDriver()
        {
            printDriver = ConfigurationManager.AppSettings.Get("printdriver");
            printNum = ConfigurationManager.AppSettings.Get("printnum");
            int lableNum = 0;
            DataTable labelDt = HelpData.GetProjectLables("1");
            printLable = new string[20][];
            foreach (DataRow row in labelDt.Rows)
            {
                string lableStr = Helper.ToStr(row["LableInfo"]);
                int lableSum = Helper.GetByteSum(lableStr);
                if (Helper.ToStr(row["IsBottom"]) == "1")
                {
                    printLable[lableNum] = new string[3] { "36", ((450 - 12 * lableSum) / 2).ToString(), lableStr };
                    lableNum++;
                }
                else if (lableSum > 38)
                {
                    string[] lableList = Helper.GetByteList(38, lableStr);
                    for (int j = 0; j < lableList.Length; j++)
                    {
                        printLable[lableNum] = new string[3] { (320 - 40 * lableNum).ToString(), (j == 0 ? "4" : "28"), lableList[j] };
                        lableNum++;
                    }
                }
                else
                {
                    printLable[lableNum] = new string[3] { (320 - 40 * lableNum).ToString(), "4", lableStr };
                    lableNum++;
                }

            }
        }
    }
   public static  class BarcodePrint
    {
        public static void PrintInfo(string driver)
        {
            // int tt = 1000;
            // TSCLIB_DLL.about();                                                                 //Show the DLL version
            //// MessageBox.Show((tt++).ToString());
            // TSCLIB_DLL.openport(textBox1.Text.ToString());                                           //Open specified printer driver
            // //MessageBox.Show((tt++).ToString());
            // TSCLIB_DLL.setup("100", "60", "4", "8", "0", "0", "0");                           //Setup the media size and sensor type info
            // //MessageBox.Show((tt++).ToString());
            // TSCLIB_DLL.clearbuffer();                                                           //Clear image buffer
            // //MessageBox.Show((tt++).ToString());
            // //TSCLIB_DLL.barcode("100", "100", "128", "100", "1", "0", "2", "2", "Barcode Test"); //Drawing barcode
            //// MessageBox.Show((tt++).ToString());
            // TSCLIB_DLL.printerfont("50", "450", "3", "90", "1", "1", "Print Font Test");        //Drawing printer font
            // //TSCLIB_DLL.windowsfont(100, 300, 24, 0, 0, 0, "ARIAL", "Windows Arial Font Test");  //Draw windows font
            // //TSCLIB_DLL.downloadpcx("UL.PCX", "UL.PCX");                                         //Download PCX file into printer
            // //TSCLIB_DLL.sendcommand("PUTPCX 100,400,\"UL.PCX\"");                                //Drawing PCX graphic
            // TSCLIB_DLL.printlabel("1", "1");                                                    //Print labels
            // TSCLIB_DLL.closeport();                                                             //Close specified printer driver 

            TSCLIB_DLL.about();
            TSCLIB_DLL.openport(driver);
            TSCLIB_DLL.setup("100", "62.5", "2", "10", "0", "0", "0");
            TSCLIB_DLL.clearbuffer();
            
            //TSCLIB_DLL.printerfont("50", "50", "TSS24.BF2", "0", "1", "2", "融信.永兴首府");
            //TSCLIB_DLL.printerfont("50", "150", "TSS24.BF2", "0", "1", "1", "公正顺序号： 934");
            //TSCLIB_DLL.printerfont("50", "250", "TSS24.BF2", "0", "1", "1", "客户：丁家详、李丽");
            ////TSCLIB_DLL.printerfont("50", "350", "TSS24.BF2", "0", "1", "1", "身份证：321322198310096815");
            ////TSCLIB_DLL.printerfont("50", "450", "TSS24.BF2", "0", "1", "1", "321322198310096000");
            ////TSCLIB_DLL.printerfont("50", "550", "TSS24.BF2", "0", "1", "1", "登记编号：GT00001");
            ////TSCLIB_DLL.printerfont("50", "650", "TSS24.BF2", "0", "1", "1", "温馨提示：");
            //TSCLIB_DLL.printerfont("50", "400", "TSS24.BF2", "0", "1", "2", "1、请保管好此入场票，进入选房区需要验证");


            TSCLIB_DLL.printerfont("745", "82", "TSS24.BF2", "90", "2", "1", "公正顺序号：934");
            TSCLIB_DLL.printerfont("650", "12", "TSS24.BF2", "90", "2", "1", "融信.永兴首府");
            TSCLIB_DLL.printerfont("590", "12", "TSS24.BF2", "90", "2", "1", "客户：丁家详、李丽");
            TSCLIB_DLL.printerfont("530", "12", "TSS24.BF2", "90", "2", "1", "身份证：321322198310096815");
            TSCLIB_DLL.printerfont("470", "110", "TSS24.BF2", "90", "2", "1", "321322198310096000");
            TSCLIB_DLL.printerfont("410", "12", "TSS24.BF2", "90", "2", "1", "登记编号：GT00001");
            TSCLIB_DLL.printerfont("360", "42", "TSS24.BF2", "90", "1", "1", "温馨提示：");
            TSCLIB_DLL.printerfont("320", "6", "TSS24.BF2", "90", "1", "1", "1、请保管好此入场票,进入选房区需要验证");
            TSCLIB_DLL.printerfont("280", "6", "TSS24.BF2", "90", "1", "1", "2、领取入场卷后,请跟随现场指示尽快入座");
            TSCLIB_DLL.printerfont("240", "6", "TSS24.BF2", "90", "1", "1", "3、提前准备好认购申请表回执,身份证和其");
            TSCLIB_DLL.printerfont("200", "40", "TSS24.BF2", "90", "1", "1", "他相关材料");
            TSCLIB_DLL.printerfont("160", "6", "TSS24.BF2", "90", "1", "1", "4、本入场劵仅2018年5月29号当天有效");
            TSCLIB_DLL.printerfont("120", "6", "TSS24.BF2", "90", "1", "1", "5、如果有其他问题请及时联系您的职业顾");
            TSCLIB_DLL.printerfont("80", "40", "TSS24.BF2", "90", "1", "1", "问或现场工作人员");
            TSCLIB_DLL.printerfont("30", "80", "TSS24.BF2", "90", "1", "1", "新视窗-电子开盘提供技术支持");

            TSCLIB_DLL.printlabel("1", "1");
            TSCLIB_DLL.closeport();


        }

        public static void PrintInfo(string[][] list)
        {
            try
            {
                TSCLIB_DLL.openport(list[0][0]);
                //TSCLIB_DLL.setup(list[0][1], list[0][2], list[0][3], list[0][4], "0", "0", "0");
                TSCLIB_DLL.clearbuffer();

                TSCLIB_DLL.printerfont("745", "82", "TSS24.BF2", "90", "2", "1", list[1][0]);
                TSCLIB_DLL.printerfont("650", "12", "TSS24.BF2", "90", "2", "1", "公证号：" + list[1][3]);
                TSCLIB_DLL.printerfont("590", "12", "TSS24.BF2", "90", "2", "1", "客户：" + list[1][5]);
                TSCLIB_DLL.printerfont("530", "12", "TSS24.BF2", "90", "2", "1", "身份证：" + list[1][6]);
                TSCLIB_DLL.printerfont("470", "110", "TSS24.BF2", "90", "2", "1", list[1][7]);
                TSCLIB_DLL.printerfont("410", "12", "TSS24.BF2", "90", "2", "1", "登记编号：" + list[1][4]);
                TSCLIB_DLL.printerfont("360", "42", "TSS24.BF2", "90", "1", "1", "温馨提示：");
                TSCLIB_DLL.printerfont("320", "6", "TSS24.BF2", "90", "1", "1", "1、请保管好此入场票,进入选房区需要验证");
                TSCLIB_DLL.printerfont("280", "6", "TSS24.BF2", "90", "1", "1", "2、领取入场卷后,请跟随现场指示尽快入座");
                TSCLIB_DLL.printerfont("240", "6", "TSS24.BF2", "90", "1", "1", "3、提前准备好认购申请表回执,身份证和其");
                TSCLIB_DLL.printerfont("200", "44", "TSS24.BF2", "90", "1", "1", "他相关材料");
                TSCLIB_DLL.printerfont("160", "6", "TSS24.BF2", "90", "1", "1", "4、本入场劵仅" + list[1][1] + "当天有效");
                TSCLIB_DLL.printerfont("120", "6", "TSS24.BF2", "90", "1", "1", "5、如果有其他问题请及时联系您的职业顾");
                TSCLIB_DLL.printerfont("80", "44", "TSS24.BF2", "90", "1", "1", "问或现场工作人员");
                TSCLIB_DLL.printerfont("30", "90", "TSS24.BF2", "90", "1", "1", list[1][2]);

                TSCLIB_DLL.printlabel("1", "1");
                TSCLIB_DLL.closeport();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static PrintDriver objDriver = null;
        public static void PrintInfo(string[] list)
        {
            try
            {
                if (objDriver == null)
                {
                    objDriver = new PrintDriver();
                    TSCLIB_DLL.openport(objDriver.printDriver);
                    System.Threading.Thread.Sleep(500);
                }

                //TSCLIB_DLL.setup(objDriver.printWidth, objDriver.printHeight, objDriver.printSpeed, objDriver.printDensity, objDriver.printSensor, objDriver.printVertical, objDriver.printOffset);
                TSCLIB_DLL.clearbuffer();
                System.Threading.Thread.Sleep(100);
                TSCLIB_DLL.printerfont("735", ((450 - 24 * list[0].Length) / 2).ToString(), "TSS24.BF2", "90", "2", "1", list[0]);
                TSCLIB_DLL.printerfont("650", "8", "TSS24.BF2", "90", "2", "1", "公证顺序号：" + list[1]);
                TSCLIB_DLL.printerfont("590", "8", "TSS24.BF2", "90", "2", "1", "客户：" + list[3]);
                TSCLIB_DLL.printerfont("530", "8", "TSS24.BF2", "90", "2", "1", "身份证：" + list[4]);
                TSCLIB_DLL.printerfont("470", "106", "TSS24.BF2", "90", "2", "1", list[5]);
                TSCLIB_DLL.printerfont("410", "8", "TSS24.BF2", "90", "2", "1", "登记编号：" + list[2]);
                TSCLIB_DLL.printerfont("356", "28", "TSS24.BF2", "90", "1", "1", "温馨提示：");
                for (int i = 0; i < objDriver.printLable.Length; i++)
                {
                    if (objDriver.printLable[i] != null)
                        TSCLIB_DLL.printerfont(objDriver.printLable[i][0], objDriver.printLable[i][1], "TSS24.BF2", "90", "1", "1", objDriver.printLable[i][2]);
                }

                TSCLIB_DLL.printlabel("1", objDriver.printNum);
                TSCLIB_DLL.clearbuffer();
                //TSCLIB_DLL.closeport();
            }
            catch(Exception ex)
            {
                MessageBox.Show("打印操作失败!请查看打印机设备。", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void PrintInfoNew(string[] list)
        {
            if (objDriver == null)
                objDriver = new PrintDriver();

            TSCLIB_DLL.openport(objDriver.printDriver);
            //TSCLIB_DLL.setup(objDriver.printWidth, objDriver.printHeight, objDriver.printSpeed, objDriver.printDensity, objDriver.printSensor, objDriver.printVertical, objDriver.printOffset);
            TSCLIB_DLL.clearbuffer();

            TSCLIB_DLL.windowsfont(745, 122, 12, 90, 0, 0, "标楷体", "ddddddddsss");
            TSCLIB_DLL.windowsfont(650, 12, 12, 90, 0, 0, "标楷体", "公证顺序号：" );
            TSCLIB_DLL.windowsfont(590, 12, 12, 90, 0, 0, "标楷体", "客户："  );
            TSCLIB_DLL.windowsfont(530, 12, 12, 90, 0, 0, "标楷体", "身份证："  );
            TSCLIB_DLL.windowsfont(470, 110, 12, 90, 0, 0, "标楷体",  "aaaaaaaa");
            TSCLIB_DLL.windowsfont(410, 12, 12, 90, 0, 0, "标楷体", "登记编号：" );
            TSCLIB_DLL.windowsfont(360, 42, 12, 90, 0, 0, "标楷体", "温馨提示：");
            TSCLIB_DLL.windowsfont(320, 6, 12, 90, 0, 0, "标楷体", "1.请保管好此入场票,进入选房区需要验证");
            TSCLIB_DLL.windowsfont(280, 6, 12, 90, 0, 0, "标楷体", "2.领取入场票后,请跟随现场指示尽快入座");
            TSCLIB_DLL.windowsfont(240, 6, 12, 90, 0, 0, "标楷体", "3.提前准备好认购申请表回执,身份证和其");

            TSCLIB_DLL.printlabel("1", "2");
            TSCLIB_DLL.closeport();

        }

        public static void FormFeed()
        {
            if (objDriver == null)
                objDriver = new PrintDriver();

            TSCLIB_DLL.openport(objDriver.printDriver);
            TSCLIB_DLL.clearbuffer();

            TSCLIB_DLL.formfeed();
            TSCLIB_DLL.closeport();
        }


    }
}
