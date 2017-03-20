using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;


/// <summary>
/// 常用代码类
/// </summary>
public static class ShareCode
{

    /// <summary>
    /// 返回下拉选项
    /// </summary>
    /// <param name="dt">数据</param>
    /// <param name="optV">下拉选项的值</param>
    /// <param name="txtV">下拉显示字段名</param>
    /// <param name="defV">默认选中荐的值</param>
    /// <returns></returns>
    public static string GetSelectOptionHtml(DataTable dt, string optV, string txtV, string defV)
    {
        if (dt == null && dt.Rows.Count == 0) { return ""; }
        if (string.IsNullOrEmpty(optV)) { return ""; }
        if (string.IsNullOrEmpty(txtV)) { return ""; }
        StringBuilder strSlt = new StringBuilder();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string v = dt.Rows[i][optV].ToString();
            if (v == defV)
            {
                strSlt.Append("<option  value=\"" + v + "\" selected=\"selected\">" + dt.Rows[i][txtV].ToString() + "</option>");
            }
            else
            {
                strSlt.Append("<option  value=\"" + v + "\">" + dt.Rows[i][txtV].ToString() + "</option>");
            }
        }
        return strSlt.ToString();
    }

    #region 验证
    /// <summary>
    /// 验证是否为非零正整数
    /// </summary>
    /// <param name="num">数字</param>
    /// <returns></returns>
    public static bool VldInteger(string num)
    {
        string sPattern = @"^\+?[1-9][0-9]*$";
        return Regex.IsMatch(num, sPattern);
    }

    /// <summary>
    /// 验证是否为0和正整数
    /// </summary>
    /// <param name="num">数字</param>
    /// <returns></returns>
    public static bool VldIntegerZ(string num)
    {
        string sPattern = @"^\d+$";
        return Regex.IsMatch(num, sPattern);
    }

    /// <summary>
    /// 验证手机号
    /// </summary>
    /// <param name="mobile">手机号</param>
    /// <returns></returns>
    public static bool VldMobile(string mobile)
    {
        string expMobile = @"^(13[0-9]|14[0-9]|15[0-9]|18[0-9])\d{8}$";
        return Regex.IsMatch(mobile, expMobile);
    }

    /// <summary>
    /// 验证电话号码
    /// </summary>
    /// <param name="phone">电话号码</param>
    /// <returns></returns>
    public static bool VldPhone(string phone)
    {
        string expPhone = @"^((0\d{2,3})-)?(0\d{2,3})?(\d{7,8})(-(\d{3,}))?$";
        return Regex.IsMatch(phone, expPhone);
    }

    /// <summary>
    /// 验证手机和电话
    /// </summary>
    /// <param name="phone">手机或电话号码</param>
    /// <returns></returns>
    public static bool VldPhoneAndMobile(string phone)
    {
        var expPM = @"(^((0\d{2,3})-)?(0\d{2,3})?(\d{7,8})(-(\d{3,}))?$)|(^(13[0-9]|14[0-9]|15[0-9]|18[0-9])\d{8}$)";
        return Regex.IsMatch(phone, expPM);
    }

    /// <summary>
    /// 验证传真
    /// </summary>
    /// <param name="fax">传真号码</param>
    /// <returns></returns>
    public static bool VldFax(string fax)
    {
        string expFax = @"^[+]?((0\d{2,3})-)?(0\d{2,3})?(\d{7,8})?$";
        return Regex.IsMatch(fax, expFax);
    }

    /// <summary>
    /// 验证Email地址
    /// </summary>
    /// <param name="email">邮箱</param>
    /// <returns></returns>
    public static bool VldEmail(string email)
    {
        string expEmail = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        return Regex.IsMatch(email, expEmail);
    }

    /// <summary>
    /// 验证Url地址
    /// </summary>
    /// <param name="url">网址</param>
    /// <returns></returns>
    public static bool VldUrl(string url)
    {
        if (url.Length >= 7)
        {
            if (url.Substring(0, 7) != "http://")
            {
                url = "http://" + url;
            }
        }
        string expUrl = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
        return Regex.IsMatch(url, expUrl);
    }

    /// <summary>
    /// 验证身份证号码（位数）
    /// </summary>
    /// <param name="cardId">身份证号码</param>
    /// <returns></returns>
    public static bool VldCard(string cardId)
    {
        string expCardId = @"(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)";
        return Regex.IsMatch(cardId, expCardId);
    }

    private static string[] aCity = new string[]{null,null,null,null,null,null,null,null,null,null,null,"北京","天津","河北","山西","内蒙古",null,null,null,null,null,"辽宁","吉林","黑龙江",null,null,null,null,null,null,null,
    "上海","江苏","浙江","安微","福建","江西","山东",null,null,null,"河南","湖北","湖南","广东","广西","海南",null,null,null,"重庆","四川","贵州","云南","西藏",null,null,null,null,null,null,"陕西",
    "甘肃","青海","宁夏","新疆",null,null,null,null,null,"台湾",null,null,null,null,null,null,null,null,null,"香港","澳门",null,null,null,null,null,null,null,null,"国外"};

    /// <summary>
    /// 验证身份证(返回是否正确：true/false)
    /// </summary>
    /// <param name="cardId">身份证号码</param>
    /// <returns>返回:正确true/错误false</returns>
    public static bool VldIsCarIdInfo(string cardId)
    {
        double iSum = 0;
        Regex rg = new System.Text.RegularExpressions.Regex(@"^\d{17}(\d|x)$");
        Match mc = rg.Match(cardId);
        if (!mc.Success)
        {
            return false;
        }
        cardId = cardId.ToLower();
        cardId = cardId.Replace("x", "a");
        if (aCity[int.Parse(cardId.Substring(0, 2))] == null)
        {
            return false;//非法地区
        }
        try
        {
            DateTime.Parse(cardId.Substring(6, 4) + "-" + cardId.Substring(10, 2) + "-" + cardId.Substring(12, 2));
        }
        catch
        {
            return false;//非法生日
        }
        for (int i = 17; i >= 0; i--)
        {
            iSum += (System.Math.Pow(2, i) % 11) * int.Parse(cardId[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);
        }
        if (iSum % 11 != 1)
        {
            return false;//非法证号
        }
        //return (aCity[int.Parse(cardId.Substring(0, 2))] + "," + cardId.Substring(6, 4) + "-" + cardId.Substring(10, 2) + "-" + cardId.Substring(12, 2) + "," + (int.Parse(cardId.Substring(16, 1)) % 2 == 1 ? "男" : "女"));
        return true;
    }

    /// <summary>
    /// 验证身份证(返回验证信息)
    /// 返回：0:身份证号码出错,1:非法地区,2:非法生日,3:非法证号,正确返回:地区+生日+性别
    /// </summary>
    /// <param name="cardId">身份证号码</param>
    /// <returns>返回：0:身份证号码出错,1:非法地区,2:非法生日,3:非法证号,正确返回:地区+生日+性别</returns>
    public static string VldCheckCarIdInfo(string cardId)
    {
        double iSum = 0;
        Regex rg = new System.Text.RegularExpressions.Regex(@"^\d{17}(\d|x)$");
        Match mc = rg.Match(cardId);
        if (!mc.Success)
        {
            return "0";//身份证号码出错
        }
        cardId = cardId.ToLower();
        cardId = cardId.Replace("x", "a");
        if (aCity[int.Parse(cardId.Substring(0, 2))] == null)
        {
            return "1";//非法地区
        }
        try
        {
            DateTime.Parse(cardId.Substring(6, 4) + "-" + cardId.Substring(10, 2) + "-" + cardId.Substring(12, 2));
        }
        catch
        {
            return "2";//非法生日
        }
        for (int i = 17; i >= 0; i--)
        {
            iSum += (System.Math.Pow(2, i) % 11) * int.Parse(cardId[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);
        }
        if (iSum % 11 != 1)
        {
            return "3";//非法证号
        }
        return (aCity[int.Parse(cardId.Substring(0, 2))] + "," + cardId.Substring(6, 4) + "-" + cardId.Substring(10, 2) + "-" + cardId.Substring(12, 2) + "," + (int.Parse(cardId.Substring(16, 1)) % 2 == 1 ? "男" : "女"));
    }

    /// <summary>
    /// 验证贷币
    /// </summary>
    /// <param name="price">价格</param>
    /// <returns></returns>
    public static bool VldPrice(string price)
    {
        string expPrice = @"^([0-9]+|[0-9]{1,3}(,[0-9]{3})*)(.[0-9]{1,2})?$";
        return Regex.IsMatch(price, expPrice);
    }

    /// <summary>
    /// 验证字符串长度
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="length">长度</param>
    /// <returns></returns>
    public static bool VldLength(string str, int length)
    {
        if (str.Length > length) { return false; }
        else { return true; }
    }

    /// <summary>
    /// 验证面积，保留小数点后四位
    /// </summary>
    /// <param name="area">面积</param>
    /// <returns></returns>
    public static bool VldArea(string area)
    {
        string expArea = @"^(\d+\.\d{1,4}|\d+)$";
        return Regex.IsMatch(area, expArea);
    }

    /// <summary>
    /// 验证字母
    /// </summary>
    /// <param name="letter">字母</param>
    /// <returns></returns>
    public static bool VldLetter(string letter)
    {
        string expLetter = @"^[A-Za-z]+$";
        return Regex.IsMatch(letter, expLetter);
    }

    /// <summary>
    /// 验证只能输入字母或数字
    /// </summary>
    /// <param name="numLetter">字母或数字</param>
    /// <returns></returns>
    public static bool VldNumAndLetter(string numLetter)
    {
        string expNumLe = @"^[A-Za-z0-9]+$";
        return Regex.IsMatch(numLetter, expNumLe);
    }

    /// <summary>
    /// 验证输入中文
    /// </summary>
    /// <param name="chinese">中文</param>
    /// <returns></returns>
    public static bool VldChinese(string chinese)
    {
        string expChinese = @"^[\u4e00-\u9fa5]+$";
        return Regex.IsMatch(chinese, expChinese);
    }

    /// <summary>
    /// 验证只能输入由数字、26个英文字母或者下划线组成的字符串
    /// </summary>
    /// <param name="ic">数字、26个英文字母或者下划线</param>
    /// <returns></returns>
    public static bool VldIllegalCharacter(string ic)
    {
        string expIc = @"^\w+$";
        return Regex.IsMatch(ic, expIc);
    }

    /// <summary>
    /// 密码验证(5-18位,区分大小写,只能使用字母、数字、特殊字符)
    /// </summary>
    /// <param name="ic">验证字符</param>
    /// <returns></returns>
    public static bool VldPassword(string ic)
    {
        string expIc = @"^[\@A-Za-z0-9\!\#\$\%\^\&\*\.\~]{5,18}$";
        return Regex.IsMatch(ic, expIc);
    }

    /// <summary>
    /// QQ验证
    /// </summary>
    /// <param name="qqNum">QQ号码</param>
    /// <returns></returns>
    public static bool VldQQNum(string qqNum)
    {
        string expQQNum = @"^[1-9][0-9]{4,10}$";
        return Regex.IsMatch(qqNum, expQQNum);
    }
    #endregion

    /// <summary>
    /// 截取货币整数
    /// </summary>
    /// <param name="price">价格</param>
    /// <returns></returns>
    public static string SubPrice(decimal price)
    {
        try
        {
            return price.ToString().Split(new char[] { '.' })[0];
        }
        catch
        {
            return price.ToString();
        }
    }

    /// <summary>
    /// 获取字符串中的数字
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns></returns>
    public static int GetNumberByStr(string str)
    {
        string strTempContent = str;
        strTempContent = Regex.Replace(strTempContent, @"[^\d]*", "");
        return Convert.ToInt32(strTempContent);
    }

    #region 日期对比
    /// <summary>
    /// 时间比较（返回状态【-255传参出错,0相等,1结束时间大于开始时间,-1结束时间小于开始时间】）
    /// </summary>
    /// <param name="sd">开始时间</param>
    /// <param name="ed">结束时间</param>
    /// <returns></returns>
    public static int ReturnCompareDateStatus(string sd, string ed)
    {
        if (string.IsNullOrEmpty(sd) && string.IsNullOrEmpty(ed))
        {
            return -255;
        }
        int c = Convert.ToDateTime(ed).CompareTo(Convert.ToDateTime(sd));
        return c;
    }

    /// <summary>
    /// 计算两个时间差值的函数，返回时间差的绝对值
    /// </summary>
    /// <param name="sd">开始时间</param>
    /// <param name="ed">结束时间</param>
    /// <returns></returns>
    public static string DateDiff(DateTime sd, DateTime ed)
    {
        string dateDiff = null;
        try
        {
            TimeSpan ts1 = new TimeSpan(sd.Ticks);
            TimeSpan ts2 = new TimeSpan(ed.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Days.ToString() + "天"
            + ts.Hours.ToString() + "小时"
            + ts.Minutes.ToString() + "分钟"
            + ts.Seconds.ToString() + "秒";
        }
        catch
        {

        }
        return dateDiff;
    }

    /// <summary>
    /// 计算两个时间差值的函数
    /// </summary>
    /// <param name="sd">开始时间</param>
    /// <param name="ed">结束时间</param>
    /// <param name="ty">0:天，1:时，2:分，3:秒</param>
    /// <returns>返回时间差的绝对值</returns>
    public static double DateDiff(DateTime sd, DateTime ed, int ty)
    {
        double dateDiff = 0;
        try
        {
            TimeSpan ts1 = new TimeSpan(sd.Ticks);
            TimeSpan ts2 = new TimeSpan(ed.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();

            switch (ty)
            {
                case 0:
                    //dateDiff = ts.Days.ToString();
                    dateDiff = ts.TotalDays;
                    break;
                case 1:
                    //dateDiff = ts.Hours.ToString();
                    dateDiff = ts.TotalHours;
                    break;
                case 2:
                    //dateDiff = ts.Minutes.ToString();
                    dateDiff = ts.TotalMinutes;
                    break;
                case 3:
                    //dateDiff = ts.Seconds.ToString();
                    dateDiff = ts.TotalSeconds;
                    break;
            }
        }
        catch { }
        return dateDiff;
    }

    /// <summary>
    /// 时间比较（返回相差天数）
    /// </summary>
    /// <param name="sd">开始日期</param>
    /// <param name="ed">结束日期</param>
    /// <returns></returns>
    public static int CompareDate(string sd, string ed)
    {
        int day = 0;//天数
        int c = Convert.ToDateTime(sd).CompareTo(Convert.ToDateTime(ed));

        TimeSpan ts1 = new TimeSpan(Convert.ToDateTime(sd).Ticks);
        TimeSpan ts2 = new TimeSpan(Convert.ToDateTime(ed).Ticks);

        if (c == 0)//两时间相等
        {
            day = 1;
        }
        if (c < 0)//开始时间早于结束时间
        {
            TimeSpan ts = ts2.Subtract(ts1).Duration();//秒
            int milliseconds = Convert.ToInt32(ts.TotalSeconds);
            day = ((milliseconds / 60) / 60) / 24;
            day = day + 1;
        }
        if (c > 0)//开始时间晚于结束单
        {
            TimeSpan ts = ts1.Subtract(ts2).Duration();//秒
            int milliseconds = Convert.ToInt32(ts.TotalSeconds);
            day = ((milliseconds / 60) / 60) / 24;
            day = day + 1;
        }
        return day;
    }

    /// <summary>
    /// 根据日期返回星期
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static string GetWeek(DateTime date)
    {
        if (date == null) { date = DateTime.Now; }
        string[] x = new string[7] { "日", "一", "二", "三", "四", "五", "六" };
        int n = int.Parse(date.DayOfWeek.ToString("D"));
        return x[n];
    }

    /// <summary>
    /// 跟据星期返回这周的开始和结束日期
    /// Xiaoyang.Wei
    /// 2010.08.16
    /// </summary>
    /// <param name="wkStr">英文星期（小写）</param>
    /// <param name="sDate">开始日期</param>
    /// <param name="eDate">结束日期</param>
    /// <param name="type">0数字星期，1中文星期</param>
    /// <returns>返回数字或中文星期(0没相关匹配数据)</returns>
    public static string GetWeekDate(string wkStr, ref string sDate, ref string eDate, int type)
    {
        int wks = 0;
        string wksC = "0";
        DateTime dt = DateTime.Now;

        switch (wkStr.ToLower())
        {
            case "sunday":
                wks = 7;
                wksC = "日";
                sDate = dt.Date.ToShortDateString();
                eDate = dt.Date.AddDays(6).ToShortDateString();
                break;
            case "monday":
                wks = 1;
                wksC = "一";
                sDate = dt.Date.AddDays(-1).ToShortDateString();
                eDate = dt.Date.AddDays(5).ToShortDateString();
                break;
            case "tuesday":
                wks = 2;
                wksC = "二";
                sDate = dt.Date.AddDays(-2).ToShortDateString();
                eDate = dt.Date.AddDays(4).ToShortDateString();
                break;
            case "wednesday":
                wks = 3;
                wksC = "三";
                sDate = dt.Date.AddDays(-3).ToShortDateString();
                eDate = dt.Date.AddDays(3).ToShortDateString();
                break;
            case "thursday":
                wks = 4;
                wksC = "四";
                sDate = dt.Date.AddDays(-4).ToShortDateString();
                eDate = dt.Date.AddDays(2).ToShortDateString();
                break;
            case "friday":
                wks = 5;
                wksC = "五";
                sDate = dt.Date.AddDays(-5).ToShortDateString();
                eDate = dt.Date.AddDays(1).ToShortDateString();
                break;
            case "saturday":
                wks = 6;
                wksC = "六";
                sDate = dt.Date.AddDays(-6).ToShortDateString();
                eDate = dt.ToShortDateString();
                break;
        }
        if (type != 1) { return wks.ToString(); }
        else { return wksC; }
    }
    #endregion

    /// <summary>
    /// 提示窗口(使用Javascript的alert)
    /// </summary>
    /// <param name="msg">提示信息</param>
    /// <param name="url">跳转地址</param>
    /// <param name="back">是否后退</param>
    /// <returns></returns>
    public static string AlertWindow(string msg, string url, bool back)
    {
        msg = msg.Replace("'", " ");
        msg = msg.Replace("\"", " ");
        msg = msg.Replace("\n", " ");
        if (back)
            return "<script type=\"text/javascript\">alert('" + msg + "');window.history.go(-1);</script>";
        else if (url != string.Empty)
            return "<script type=\"text/javascript\">alert('" + msg + "');window.location.href='" + url + "';</script>";
        else return "<script type=\"text/javascript\">alert('" + msg + "');</script>";
    }

    /// <summary>
    /// 提示窗口(调用layer 2.0)
    /// </summary>
    /// <param name="page"></param>
    /// <param name="key"></param>
    /// <param name="icoC">icoC:显示图标</param>
    /// <param name="msg">提示信息</param>
    /// <param name="isLoad">isLoad:重新加载页面【0加载，1跳转URL，其它不操作】</param>
    /// <param name="url">url:跳转URL</param>
    public static void AlertlayerMsg(System.Web.UI.Page page, string key, string icoC, string msg, string isLoad, string url)
    {
        page.ClientScript.RegisterClientScriptBlock(typeof(Object), key, "<script type=\"text/javascript\">ShowAlter('" + icoC + "','" + msg + "','" + isLoad + "','" + url + "');</script>");
    }

    /// <summary>
    /// 提示窗口(使用Javascript的alert)
    /// </summary>
    /// <param name="page"></param>
    /// <param name="key"></param>
    /// <param name="msg">提示信息</param>
    /// <param name="redirectUrl">跳转地址</param>
    /// <param name="isBack">是否后退</param>
    public static void AlertMsg(System.Web.UI.Page page, string key, string msg, string redirectUrl, bool isBack)
    {
        page.ClientScript.RegisterClientScriptBlock(typeof(Object), key, AlertWindow(msg, redirectUrl, isBack));
    }

    /// <summary>
    /// 提示窗口(使用Javascript的alert)
    /// </summary>
    /// <param name="page"></param>
    /// <param name="msg">提示信息</param>
    /// <param name="redirectUrl">跳转地址</param>
    /// <param name="isBack">是否后退</param>
    public static void AlertExceptionMsg(System.Web.UI.Page page, string msg, string redirectUrl, bool isBack)
    {
        msg = msg.Replace("\n", " ").Replace("\r", "");
        AlertMsg(page, "ExceptionMsg", msg, redirectUrl, isBack);
    }

    public static string ListToString<T>(System.Collections.Generic.List<T> ids)
    {
        string stringIds = "";
        if (ids.Count <= 0) return "";
        else if (ids.Count == 1) stringIds = ids[0].ToString();
        else
        {
            foreach (T id in ids)
            {
                stringIds += id.ToString() + ",";
            }
            stringIds = stringIds.Substring(0, stringIds.Length - 1);
        }
        return stringIds;
    }

    public static string ToShortTime(DateTime time)
    {
        if (DateTime.Now.Add(new TimeSpan(24, 0, 0)) > time)
        {
            return time.ToShortTimeString();
        }
        else return time.ToShortDateString();
    }

    public static string ToShortestTime(DateTime time)
    {
        if (DateTime.Now.Add(new TimeSpan(24, 0, 0)) > time)
        {
            return time.ToShortTimeString();
        }
        else return time.Month.ToString() + "月" + time.Day.ToString() + "日";
    }

    /// <summary>
    /// 在弹出窗口的父窗口中关闭本窗口
    /// </summary>
    /// <param name="page"></param>
    public static void ParentCloseMe(Page page)
    {
        page.ClientScript.RegisterClientScriptBlock(typeof(Object), "CloseMe",
            "<script type=\"text/javascript\">CloseMe()</script>");
    }

    public static void AddScriptBlock(Page page, string javascript)
    {
        page.ClientScript.RegisterClientScriptBlock(typeof(Object), "SomeScript" + Guid.NewGuid().ToString(),
            "<script type=\"text/javascript\">" + javascript + "</script>");
    }
}
