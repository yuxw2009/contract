using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public static class CommonCode
    {
        /// <summary>
        /// 币种编码
        /// </summary>
        public static string[] CurrencyAr = { "AUD", "CAD", "CHF", "CNY", "EUR", "GBP", "HKD", "JPY", "KRM", "RUB", "SEK", "USD" };
        /// <summary>
        /// 付款方式
        /// </summary>
        public static string[] PlayAr ={"invoice 30", "invoice 40", "invoice 45", "invoice 60", "invoice 70", "invoice 90", "其它（可变）", "IMM", "pre-paid", "credit card",
                                          "30% prepayment，70% down payment", "30,60,10", "50%/50%", "40%55%5%", "net 10", "net 120", "net 15", "net 20", "due upon receipt", "1%,net 30", "2%,10,net 30", "2.5/30,2/45,1.5/60,net 90"};
        /// <summary>
        /// 价格条款
        /// </summary>
        public static string[] PriceClauseAr = { "到库价", "出厂价", "CIP", "DAT", "DDU", "DDP", "DAP", "CIF", "FOB", "EXW", "CFR"};

        /// <summary>
        /// 返回绑定下拉表数据
        /// </summary>
        /// <param name="ar">绑定数据的数组</param>
        /// <returns></returns>
        public static DataTable GetOptionDataSource(string[] ar)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("key", Type.GetType("System.String"));
            dt.Columns.Add("value", Type.GetType("System.String"));

            //循环表数据，每次取几次
            for (int i = 0; i < ar.Length; i++)
            {
                dt.Rows.Add(ar[i], ar[i]); 
            }

            return dt;
        }

        /// <summary>
        /// 返回币种下拉选项
        /// </summary>
        /// <param name="optV">下拉选项的值</param>
        /// <param name="txtV">下拉显示字段名</param>
        /// <param name="defV">默认选中荐的值</param>
        /// <returns></returns>
        public static string GetCurrencyOptionHtml(string optV, string txtV, string defV)
        {
            //if (string.IsNullOrEmpty(optV)) { return ""; }
            //if (string.IsNullOrEmpty(txtV)) { return ""; }
            StringBuilder strSlt = new StringBuilder();
            for (int i = 0; i < CurrencyAr.Length; i++)
            {
                string v = CurrencyAr[i];
                if (v == defV)
                {
                    strSlt.Append("<option  value=\"" + v + "\" selected=\"selected\">" + v + "</option>");
                }
                else
                {
                    strSlt.Append("<option  value=\"" + v + "\">" + v + "</option>");
                }
            }
            return strSlt.ToString();
        }

        /// <summary>
        /// 合同状态
        /// </summary>
        /// <param name="WorkFlowStatus">状态编号</param>
        /// <returns></returns>
        public static string GetWorkFlowStatusText(string WorkFlowStatus)
        {
            string statusStr = "未创建";
            switch (WorkFlowStatus)
            {
                case "1":
                    statusStr = "明细维护";
                    break;
                case "2":
                    statusStr = "供应商确认中";
                    break;
                case "3":
                    statusStr = "供应商回退";
                    break;
                case "4":
                    statusStr = "供应商确认";
                    break;
                case "5":
                    statusStr = "经理审批中";
                    break;
                case "6":
                    statusStr = "经理回退";
                    break;
                case "7":
                    statusStr = "合同生效";
                    break;
                default:
                    statusStr = "未创建";
                    break;
            }
            return statusStr;
        }

        // <summary>  
        /// 替换模版内容    
        /// </summary>  
        /// <param name="tempStr">模版</param>
        /// <param name="oldStr">被替换内容</param>
        /// <param name="newStr">替换内容</param>
        public static string TemplateReplace(string tempStr, string oldStr, string newStr)
        {
            newStr = tempStr.Replace(oldStr, newStr);//替换
            return newStr;
        }

        /// <summary>
        /// 提取替换模板参数的正则
        /// </summary>
        /// <param name="htmlImgStr">HTML字符串</param>
        public static List<string> TempParamRegex(string htmlImgStr)
        {
            if (string.IsNullOrEmpty(htmlImgStr)) { return null; }
            List<string> imgList = new List<string>();
            //Regex regExp = new Regex(@"(?is)(?<=<\w+[\s\S]*?src=(['""]?))(https?)?[^'"">]+(?:jpg|png|gif)(?=\1)");
            Regex regExp = new Regex(@"<span style=(['""]?)color:red(['""]?) class=(['""]?)#\w+#(['""]?)>[\w|\u4e00-\u9fa5]+</span>");
            MatchCollection mc = regExp.Matches(htmlImgStr);
            foreach (Match m in mc)
            {
                imgList.Add(m.Value.Replace("&#34;", ""));
            }
            return imgList;
        }

        /// <summary>
        /// 提取表头替换模板参数的正则
        /// </summary>
        /// <param name="htmlImgStr">HTML字符串</param>
        public static List<string> TempTableParamRegex(string htmlImgStr)
        {
            if (string.IsNullOrEmpty(htmlImgStr)) { return null; }
            List<string> imgList = new List<string>();
            Regex regExp = new Regex(@"<span style=(['""]?)color:red(['""]?) class=(['""]?)@#\w+#(['""]?)>[\w|\u4e00-\u9fa5]+</span>");
            MatchCollection mc = regExp.Matches(htmlImgStr);
            foreach (Match m in mc)
            {
                imgList.Add(m.Value.Replace("&#34;", ""));
            }
            return imgList;
        }

        /// <summary>
        /// 提取表头单个变量参数的正则
        /// </summary>
        /// <param name="htmlImgStr">HTML字符串</param>
        public static List<string> TempTableTitleRegex(string htmlImgStr)
        {
            if (string.IsNullOrEmpty(htmlImgStr)) { return null; }
            List<string> imgList = new List<string>();
            Regex regExp = new Regex(@"@#\w+#");
            MatchCollection mc = regExp.Matches(htmlImgStr);
            foreach (Match m in mc)
            {
                imgList.Add(m.Value.Replace("&#34;", ""));
            }
            return imgList;
        }

        /// <summary>
        /// 提取表头单个变量显示字段的正则
        /// </summary>
        /// <param name="htmlImgStr">HTML字符串</param>
        public static List<string> TempTableTitleTextRegex(string htmlImgStr)
        {
            if (string.IsNullOrEmpty(htmlImgStr)) { return null; }
            List<string> imgList = new List<string>();
            Regex regExp = new Regex(@">[\w|\u4e00-\u9fa5]+<");
            MatchCollection mc = regExp.Matches(htmlImgStr);
            foreach (Match m in mc)
            {
                imgList.Add(m.Value.Replace("&#34;", ""));
            }
            return imgList;
        }
    }
}
