using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
/// <summary>
///RegexValid 的摘要说明
/// </summary>
public class RegexValid
{

    /// <summary>
    ///  判断输入的字符串只包含数字（可带正负号整数）
    /// </summary>
    /// <param name="input"></param>
    /// <returns>只能输入数字！</returns>
    public static bool IsNumber(string input)
    {
        string pattern = "^-?\\d+$|^(-?\\d+)(file://.//d+)?$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(input);
    }


    /// <summary>
    /// 匹配非负整数(>=0)
    /// </summary>
    /// <param name="input"></param>
    /// <returns>只能输入非负整数！</returns>
    public static bool IsNotNagtive(string input)
    {
        Regex regex = new Regex(@"^\d+$");
        return regex.IsMatch(input);
    }


    /// <summary>
    /// 匹配非负整数且最大长度范围
    /// </summary>
    /// <param name="input"></param>
    /// <returns>只能输入非负整数且最大长度为X！</returns>
    public static bool IsNotNagtive(string input, int maxlength)
    {
        Regex regex = new Regex(@"^\d+$");
        bool isbool = regex.IsMatch(input);
        if (isbool)
        {
            if (input.Trim().Length > maxlength)
            {
                return false;
            }
            else return true;
        }
        else return false;
    }

    /// <summary>
    ///  匹配正整数(>0)
    /// </summary>
    /// <param name="input"></param>
    /// <returns>只能输入大于0的数字！</returns>
    public static bool IsUint(string input)
    {
        Regex regex = new Regex("^[0-9]*[1-9][0-9]*$");
        return regex.IsMatch(input);
    }

    /// <summary>
    /// 是否是浮点数（可以是整数和浮点型 例如 -1， -1.1， 0 ，1， 1.1 ）
    /// </summary>
    /// <param name="inputData">输入字符串</param>
    /// <returns>数据类型只能是整型或者浮点型！</returns>
    public static bool IsDecimal(string inputData)
    {
        //Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        Regex RegDecimal = new Regex("^-?\\d+$|^(-?\\d+)(\\.\\d+)?$");//^(|[+-]?(0|([1-9]\\d*)|((0|([1-9]\\d*))?\\.\\d{1,2})){1,1})$ 效果一样
        Match m = RegDecimal.Match(inputData);
        return m.Success;
    }



    /// <summary>
    /// 是否是浮点数 可带正负号(例如 -0.9 ，0 ，0.9)
    /// </summary>
    /// <param name="inputData">输入字符串</param>
    /// <returns>数据类型只能是浮点型！</returns>
    public static bool IsDecimalSign(string inputData)
    {
        Regex RegDecimalSign = new Regex("^-?([1-9]\\d*\\.\\d*|0\\.\\d*[1-9]\\d*|0?\\.0+|0)$");
        Match m = RegDecimalSign.Match(inputData);
        return m.Success;
    }

    /// <summary>
    /// 非负浮点数 (>=0的浮点数 例如 0 ， 1 ， 1.1)
    /// </summary>
    /// <param name="inputData">输入字符串</param>
    /// <returns>只能输入非负浮点数！</returns>
    public static bool IsBaseDecimal(string inputData)
    {
        Regex RegDecimalSign = new Regex("^\\d+(\\.\\d+)?$");
        Match m = RegDecimalSign.Match(inputData);
        return m.Success;
    }

    /// <summary>
    /// 非负浮点数且最大长度范围 (>=0的浮点数 例如 0 ， 1 ， 1.1)
    /// </summary>
    /// <param name="inputData">输入字符串</param>
    /// <returns>只能输入非负浮点型数字且最大长度为X！</returns>
    public static bool IsBaseDecimal(string inputData, int maxlength)
    {
        Regex RegDecimalSign = new Regex("^\\d+(\\.\\d+)?$");
        Match m = RegDecimalSign.Match(inputData);
        if (m.Success)
        {
            if (inputData.Length > maxlength) return false; else return true;
        }
        else return false;
    }

    /// <summary>
    ///  判断输入的字符串是否是一个合法的Email地址
    /// </summary>
    /// <param name="input"></param>
    /// <returns>请输入正确的Email地址！</returns>
    public static bool IsEmail(string input)
    {
        string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(input);
    }

    /// <summary>
    /// 检测是否有中文字符
    /// </summary>
    /// <param name="inputData"></param>
    /// <returns></returns>
    public static bool IsHasCHZN(string inputData)
    {
        Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
        Match m = RegCHZN.Match(inputData);
        return m.Success;
    }


    /// <summary>
    /// 判断输入的字符串是否只包含数字和英文字母
    /// </summary>
    /// <param name="input"></param>
    /// <returns>输入的字符串只包含数字和英文字母！</returns>
    public static bool IsNumAndEnCh(string input)
    {
        string pattern = @"^[A-Za-z0-9]+$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(input);
    }

    /// <summary>
    /// 判断输入的字符串是否只包含数字和英文字母
    /// </summary>
    /// <param name="input"></param>
    /// <returns>输入的字符串只包含数字和英文字母！</returns>
    public static bool IsNumorEnCh(string input)
    {
        string pattern = @"^[A-Fa-f0-9]+$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(input);
    }

    /// <summary>
    ///  判断输入的字符串字包含英文字母 
    /// </summary>
    /// <param name="input"></param>
    /// <returns>输入的字符串字只包含英文字母！ </returns>
    public static bool IsEnglisCh(string input)
    {
        Regex regex = new Regex("^[A-Za-z]+$");
        return regex.IsMatch(input);
    }

    /// <summary>
    ///  判断输入的字符串是否是一个合法的手机号^13\\d{9}$
    /// </summary>
    /// <param name="input"></param>
    /// <returns>请输入正确的手机号！</returns>
    public static bool IsMobilePhone(string input)
    {
        Regex regex = new Regex(@"(13[0-9]|15[0123456789]|18[0123456789]|14[57])[0-9]{8}$");
        return regex.IsMatch(input);
    }

    /// <summary>
    ///  判断输入的字符串是否是一个合法的电话号码(正确格式为：XXXX-XXXXXXX，XXXX-XXXXXXXX，XXX-XXXXXXX，XXX-XXXXXXXX，XXXXXXX，XXXXXXXX)
    /// </summary>
    /// <param name="input"></param>
    /// <returns>请输入正确的电话号码</returns>

    public static bool IsMobile(string input)
    {
        Regex regex = new Regex("^(\\(\\d{3,4}\\)|\\d{3,4}-)?\\d{7,8}$");
        return regex.IsMatch(input);

    }

    /// <summary>
    ///  日期格式字符串判断
    /// </summary>
    /// <param name="str"></param>
    /// <returns>日期格式错误！</returns>
    public static bool IsDateTime(string str)
    {
        try
        {
            if (!string.IsNullOrEmpty(str))
            {
                DateTime.Parse(str);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    ///  日期格式字符串判断(HH:mm:ss)
    /// </summary>
    /// <param name="str"></param>
    /// <returns>日期格式错误！</returns>
    public static bool IsDateTimeHMS(string str)
    {
        Regex regexdate = new Regex("([0-1][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])");
        return regexdate.IsMatch(str);
    }

    /// <summary>
    /// 检查字符串最大长度，返回指定长度的串
    /// </summary>
    /// <param name="sqlInput">输入字符串</param>
    /// <param name="maxLength">最大长度</param>
    /// <returns></returns>			
    public static bool SqlText(string sqlInput, int maxLength)
    {
        if (sqlInput != null && sqlInput != string.Empty)
        {
            sqlInput = sqlInput.Trim();
            if (sqlInput.Length > maxLength)//按最大长度截取字符串
                return false;
            else return true;

        }
        else return false;

    }

    /// <summary>
    /// 检查时间格式
    /// </summary>
    /// <param name="inputData"></param>
    /// <returns></returns>
    public static bool IsHasFormat(string inputData)
    {
        Regex RegFormat = new Regex("([0-1][0-9]|2[0-3]):([0-5][0-9])");
        Match m = RegFormat.Match(inputData);
        return m.Success;
    }

    #region 过滤字符串的关键字
    public static string NoHtml(string HtmlStr)
    {
        if (HtmlStr == null)
        {
            return "";
        }
        string tmpStr = HtmlStr;
        tmpStr = ReplaceHtml("&#[^>]*;", tmpStr, "");
        tmpStr = ReplaceHtml("</?marquee[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?object[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?param[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?embed[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?table[^>]*>", tmpStr, "");
        //tmpStr = ReplaceHtml(" ", tmpStr, "");
        tmpStr = ReplaceHtml("</?tr[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?th[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?p[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?a[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?img[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?tbody[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?li[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?span[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?div[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?th[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?td[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?script[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("(javascript|jscript|vbscript|vbs):", tmpStr, "");
        tmpStr = ReplaceHtml("on(mouse|exit|error|click|key)", tmpStr, "");
        tmpStr = ReplaceHtml("<\\?xml[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("<\\/?[a-z]+:[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?font[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?b[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?u[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?i[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("</?strong[^>]*>", tmpStr, "");
        tmpStr = ReplaceHtml("<script[^>]*?>.*?</script>", tmpStr, "");
        //删除HTML
        tmpStr = ReplaceHtml("<(.[^>]*)>", tmpStr, "");
        tmpStr = ReplaceHtml("--> ", tmpStr, "");
        tmpStr = ReplaceHtml(" <!--.*", tmpStr, "");
        tmpStr = ReplaceHtml("&(quot|#34);", tmpStr, "");
        tmpStr = ReplaceHtml("&(amp|#38);", tmpStr, "");
        tmpStr = ReplaceHtml("&(lt|#60);", tmpStr, "");
        tmpStr = ReplaceHtml("&(gt|#62);", tmpStr, "");
        tmpStr = ReplaceHtml("&(nbsp|#160);", tmpStr, "");
        tmpStr = ReplaceHtml("&(iexcl|#161);", tmpStr, "");
        tmpStr = ReplaceHtml("&(cent|#162);", tmpStr, "");
        tmpStr = ReplaceHtml("&(pound|#163);", tmpStr, "");
        tmpStr = ReplaceHtml("&(copy|#169);", tmpStr, "");
        //删除与数据库相关的词
        tmpStr = ReplaceHtml(" select ", tmpStr, "");
        tmpStr = ReplaceHtml(" insert ", tmpStr, "");
        tmpStr = ReplaceHtml(" delete from ", tmpStr, "");
        tmpStr = ReplaceHtml(" count ''", tmpStr, "");
        tmpStr = ReplaceHtml(" drop table ", tmpStr, "");
        tmpStr = ReplaceHtml(" truncate ", tmpStr, "");
        tmpStr = ReplaceHtml(" asc ", tmpStr, "");
        tmpStr = ReplaceHtml(" mid ", tmpStr, "");
        tmpStr = ReplaceHtml(" char ", tmpStr, "");
        tmpStr = ReplaceHtml(" xp_cmdshell ", tmpStr, "");
        tmpStr = ReplaceHtml(" exec master ", tmpStr, "");
        tmpStr = ReplaceHtml(" net localgroup administrators ", tmpStr, "");
        tmpStr = ReplaceHtml(" and ", tmpStr, "");
        tmpStr = ReplaceHtml(" net user ", tmpStr, "");
        tmpStr = ReplaceHtml(" or ", tmpStr, "");
        tmpStr = ReplaceHtml("'or ", tmpStr, "");
        tmpStr = ReplaceHtml(" net ", tmpStr, "");
        //tmpStr = ReplaceHtml("*",tmpStr,"");
        tmpStr = ReplaceHtml("-", tmpStr, "");
        tmpStr = ReplaceHtml(" delete ", tmpStr, "");
        tmpStr = ReplaceHtml(" drop ", tmpStr, "");
        tmpStr = ReplaceHtml(" script ", tmpStr, "");
        //特殊的字符
        tmpStr = ReplaceHtml(" <", tmpStr, "");
        tmpStr = ReplaceHtml("> ", tmpStr, "");
        tmpStr = ReplaceHtml(" - ", tmpStr, "");
        //tmpStr = ReplaceHtml("?", tmpStr, "");
        tmpStr = ReplaceHtml("'", tmpStr, "");
        //tmpStr = ReplaceHtml(",", tmpStr, "");
        //tmpStr = ReplaceHtml("/", tmpStr, "");
        tmpStr = ReplaceHtml(";", tmpStr, "");
        //tmpStr = ReplaceHtml("*/", tmpStr, "");

        return tmpStr;
    }

    public static string ReplaceHtml(string Pattern, string HtmlStr, string replacement)
    {
        System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex(Pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        if (rx.IsMatch(HtmlStr))
            return rx.Replace(HtmlStr, replacement);
        return HtmlStr;
    }

    public static bool FHtml(string HtmlStr)
    {
        if (NoHtml(HtmlStr).Contains(HtmlStr))
        {
            return false;
        }
        return true;
    }
    #endregion

}
