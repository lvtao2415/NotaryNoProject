using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NotaryNoProject.GloBal
{
    static class Helper
    {

        public static string ToStr(object obj)
        {
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }
        public static decimal ToDecimal(object obj)
        {
            decimal val = 0;
            if (obj != null)
                decimal.TryParse(obj.ToString(), out val);

            return val;
        }
        public static string ToDateStr(object obj, string form)
        {
            if (obj == null)
                return "";
            else
            {
                DateTime curDate;
                if (DateTime.TryParse(obj.ToString(), out curDate))
                {
                    return curDate.ToString(form);
                }
                return "";
            }
        }
        public static string ToDateFirst(object obj)
        {
            if (obj == null)
                return "";
            else
            {
                DateTime curDate;
                if (DateTime.TryParse(obj.ToString(), out curDate))
                {
                    return curDate.ToString("yyyy年MM月dd日");
                }
                return "";
            }
        }
        public static string ToDateSecond(object obj)
        {
            if (obj == null)
                return "";
            else
            {
                DateTime curDate;
                if (DateTime.TryParse(obj.ToString(), out curDate))
                {
                    return curDate.ToString("yyyy-MM-dd HH");
                }
                return "";
            }
        }
        public static string ToDateThree(object obj)
        {
            if (obj == null)
                return "";
            else
            {
                DateTime curDate;
                if (DateTime.TryParse(obj.ToString(), out curDate))
                {
                    return curDate.ToString("yyyy-MM-dd HH:mm");
                }
                return "";
            }
        }
        public static string ToDateThour(object obj)
        {
            if (obj == null)
                return "";
            else
            {
                DateTime curDate;
                if (DateTime.TryParse(obj.ToString(), out curDate))
                {
                    return curDate.ToString("yyyyMMdd HH:mm");
                }
                return "";
            }
        }
        public static string ToFirstBollen(object obj)
        {
            string curStr = ToStr(obj);
            if (curStr == "1")
                return "是";
            else
                return "否";
        }
        public static string ToSex(object obj)
        {
            string curStr = ToStr(obj);
            if (curStr == "1")
                return "男";
            else
                return "女";
        }
        public static string ToFirstStaus(object obj)
        {
            string curStr = ToStr(obj);
            if (curStr == "1")
                return "正常";
            else
                return "关闭";
        }
        public static Boolean ToBooleanStaus(object obj)
        {
            string curStr = ToStr(obj);
            if (curStr == "1")
                return true;
            else
                return false;
        }
        public static string ToSexStaus(object obj)
        {
            string curStr = ToStr(obj);
            if (curStr == "男")
                return "1";
            else
                return "0";
        }

        public static bool IsLicense(string card)
        {
            if (string.IsNullOrEmpty(card))
            {
                return false;
            }
            if (Regex.IsMatch(card.Substring(0, 1), @"[a-zA-Z]") && card.Length <= 36)
            {
                return true;
            }
            return false;
        }

        public static bool IsCard(string card)
        {
            if (string.IsNullOrEmpty(card))
            {
                return false;
            }
            if (Regex.IsMatch(card, @"^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }

        public static int GetByteSum(string obj)
        {
            return Encoding.Default.GetBytes(obj).Length;
        }

        public static string[] GetByteList(int num, string obj)
        {
            string[] byteList = new string[20];
            int listNum = 0, curNum = 0;
            for (int i = 0; i < obj.Length; i++)
            {
                curNum = curNum + GetByteSum(obj[i].ToString());

                if (curNum <= num)
                {
                    byteList[listNum] += obj[i];
                }
                else
                {
                    curNum = GetByteSum(obj[i].ToString());
                    listNum++;
                    byteList[listNum] += obj[i];
                }
            }

            string[] byteNewList = new string[listNum + 1];
            for (int j = 0; j < listNum + 1; j++)
            {
                byteNewList[j] = byteList[j];
            }

            return byteNewList;
        }






        public static bool IsSignAndPrint()
        {
            string signMark = ConfigurationManager.AppSettings.Get("signandprint");
            if (!string.IsNullOrEmpty(signMark) && signMark.ToLower() == "true")
            {
                return true;
            }
            return false;
        }

        public static string SignText(bool sign)
        {
            if (sign)
            {
                return "签到＆打印";
            }
            else
            {
                return "签 到";
            }
        }


    }
}
