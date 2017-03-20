using BLL;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contract_ContractPrint : System.Web.UI.Page
{
    private readonly ContactBLL bll = new ContactBLL();
    private readonly ContactTempletBLL tempbll = new ContactTempletBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                string tempId = "";
                if (Request.QueryString["tId"] != null)
                {
                    tempId = Request.QueryString["tId"].ToString();
                }

                LoadData(Request.QueryString["id"].ToString(), tempId);
            }
        }
    }


    private void LoadData(string id, string tempId)
    {
        DataTable dt = bll.GetContactDataByID(id.Trim());
        if (dt != null && dt.Rows.Count > 0)
        {
            string TemplateId = dt.Rows[0]["TemplateId"].ToString();//合同模板
            if (!string.IsNullOrEmpty(tempId))
            {
                TemplateId = tempId;
            }

            string tempStr = "";
            //取模板信息
            DataTable tempDt = tempbll.GetGetContactTemplet(TemplateId);
            if (tempDt != null && tempDt.Rows.Count > 0)
            {
                tempStr = tempDt.Rows[0]["CTRemark"].ToString();//模板内容信息
                string param = tempDt.Rows[0]["Parameter"].ToString();//替换参数

                if (!string.IsNullOrEmpty(param))
                {
                    id = dt.Rows[0]["id"].ToString();
                    string workFlowStatus = dt.Rows[0]["WorkFlowStatus"].ToString().Trim();
                    string WorkFlowStatusStr = CommonCode.GetWorkFlowStatusText(workFlowStatus);

                    string CreateTime = dt.Rows[0]["CreateTime"].ToString();
                    string BuyOrg = dt.Rows[0]["BuyOrg"].ToString();
                    string srmNo = dt.Rows[0]["SRM_Contract_NO"].ToString();
                    string ERPNo = dt.Rows[0]["ERP_Contract_NO"].ToString();
                    string GrantNo = dt.Rows[0]["GrantNo"].ToString();
                    string DistributorName = dt.Rows[0]["DistributorName"].ToString();
                    string DistributorAddr = dt.Rows[0]["DistributorAddr"].ToString();
                    string ContactName = dt.Rows[0]["ContactName"].ToString();
                    string ReceivingParty = dt.Rows[0]["ReceivingParty"].ToString();
                    string AcquiringParty = dt.Rows[0]["AcquiringParty"].ToString();
                    string Currency = dt.Rows[0]["Currency"].ToString();
                    string BuyerAccount = dt.Rows[0]["BuyerAccount"].ToString();
                    string Play = dt.Rows[0]["PlayType"].ToString();//付款方式
                    string PriceClause = dt.Rows[0]["PriceClause"].ToString();//价格条款
                    string Remark = dt.Rows[0]["Remark"].ToString();//摘要
                    string bDate = dt.Rows[0]["AgreementBDate"].ToString();
                    string eDate = dt.Rows[0]["AgreementEDate"].ToString();

                    //添加替换值到列表
                    ArrayList arValue = new ArrayList();
                    arValue.Add(dt.Rows[0]["BuyOrg"].ToString());
                    arValue.Add(dt.Rows[0]["DistributorName"].ToString());
                    arValue.Add(dt.Rows[0]["DistributorAddr"].ToString());
                    arValue.Add(dt.Rows[0]["ContactName"].ToString());
                    arValue.Add(dt.Rows[0]["ReceivingParty"].ToString());
                    arValue.Add(dt.Rows[0]["AcquiringParty"].ToString());
                    arValue.Add(dt.Rows[0]["BuyerAccount"].ToString());

                    //拆分替换参数
                    string[] arParam = param.Split(';');
                    if (arParam.Length > 0)
                    {
                        //替换一般变量
                        if (!string.IsNullOrEmpty(arParam[0]))
                        {
                            string[] arTitle = arParam[0].Split(',');
                            for (int i = 0; i < arTitle.Length; i++)
                            {
                                if (string.IsNullOrEmpty(arTitle[i])) { continue; }//如果参数为空，则继续下一个
                                if (arValue.Count < i) { break; }//如果值列表为空，则退出

                                tempStr = CommonCode.TemplateReplace(tempStr, arTitle[i], arValue[i].ToString());
                            }
                        }

                        //表格变量
                        if (!string.IsNullOrEmpty(arParam[1]))
                        {
                            //提取表头单个变量显示字段的正则
                            List<string> titleTextStr = CommonCode.TempTableTitleTextRegex(arParam[1]);
                            //替换表格标题
                            string[] arTbTitle = arParam[1].Split(',');
                            for (int i = 0; i < arTbTitle.Length; i++)
                            {
                                if (string.IsNullOrEmpty(arTbTitle[i])) { continue; }//如果参数为空，则继续下一个

                                tempStr = CommonCode.TemplateReplace(tempStr, arTbTitle[i], titleTextStr[i].Replace(">", "").Replace("<", "").ToString());
                            }

                            //表数据操作
                            DataTable dtNew = new DataTable();
                            string colName = "";
                            ArrayList arColNames = new ArrayList();

                            //提取表头单个变量参数
                            List<string> titleStr = CommonCode.TempTableTitleRegex(arParam[1]);
                            for (int k = 0; k < titleStr.Count; k++)
                            {
                                colName = titleStr[k].Replace("@", "").Replace("#", "");
                                dtNew.Columns.Add(colName, Type.GetType("System.String"));

                                arColNames.Add(colName);
                            }

                            string[] arTBTitle = arParam[1].Split(',');//表格参数
                            if (arTBTitle.Length > 0)
                            {
                                DataTable dtp = null;
                                dtp = bll.GetContactProductByCID(id);//查询产品信息
                                if (dtp != null && dtp.Rows.Count > 0)
                                {
                                    DataRow nRow = null;//新行
                                    for (int j = 0; j < dtp.Rows.Count; j++)
                                    {
                                        nRow = dtNew.NewRow();//新行

                                        foreach (DataColumn c in dtp.Columns)
                                        {
                                            if (arColNames.Contains(c.ColumnName))
                                            {
                                                nRow[c.ColumnName] = dtp.Rows[j][c.ColumnName].ToString();//如果字段一至，则加入对应的数据
                                            }
                                        }
                                        dtNew.Rows.Add(nRow);
                                    }
                                }
                            }

                            //拼成数据行
                            StringBuilder tbHtml = new StringBuilder();
                            tbHtml.Append("<tbody>");
                            for (int n = 0; n < dtNew.Rows.Count; n++)
                            {
                                tbHtml.Append("<tr>");

                                foreach (DataColumn c in dtNew.Columns)
                                {
                                    tbHtml.Append("<td>");
                                    tbHtml.Append(dtNew.Rows[n][c.ColumnName].ToString());
                                    tbHtml.Append("</td>");
                                }
                                tbHtml.Append("</tr>");
                            }
                            tbHtml.Append("</tbody>");

                            tempStr = CommonCode.TemplateReplace(tempStr, "<tbody class=\"@RowData\"></tbody>", tbHtml.ToString());
                        }
                    }
                }
            }
            //Response.Write(tempStr);
            lbHtml.Text = tempStr;


            ////金额合计
            //double amount = 0;
            //if (dtp != null && dtp.Rows.Count > 0)
            //{
            //    foreach (DataRow r in dtp.Rows)
            //    {
            //        amount += string.IsNullOrEmpty(r["Amount"].ToString()) ? 0 : double.Parse(r["Amount"].ToString());
            //    }
            //}
            //string Total = amount.ToString();
        }
        else
        {
            ShareCode.AlertMsg(this, "play", "预览出错", "", false);
        }
    }
}