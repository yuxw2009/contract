using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// 供应商合同确认
/// </summary>
public partial class Contact_DistributorConfirm : System.Web.UI.Page
{
    private readonly ContactBLL bll = new ContactBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                LoadData(Request.QueryString["id"].ToString());
            }
        }
    }

    private void LoadData(string id)
    {
        //bool isD = false;//查询哪个表的产品，false没有生成合同时，根据询价单ID查询，true已生成合同，根据合同ID查询
        DataTable dt = bll.GetContactDataByID(id.Trim());
        if (dt != null && dt.Rows.Count > 0)
        {
            string workFlowStatus = dt.Rows[0]["WorkFlowStatus"].ToString().Trim();
            if (workFlowStatus != "2")//供应商确认状态
            {
                ShareCode.AlertMsg(this, "play", "确认信息出错，请重新选择。", "Distributor.aspx", false);
                return;
            }

            lbTime.Text = dt.Rows[0]["CreateTime"].ToString();
            lbBuyOrg.Text = dt.Rows[0]["BuyOrg"].ToString();

            string srmNo = dt.Rows[0]["SRM_Contract_NO"].ToString();
            lbSrmNo.Text = srmNo;
            hdfid.Value = dt.Rows[0]["id"].ToString();

            lbERPNo.Text = dt.Rows[0]["ERP_Contract_NO"].ToString();
            lbGrantNo.Text = dt.Rows[0]["GrantNo"].ToString();
            lbDistributorName.Text = dt.Rows[0]["DistributorName"].ToString();
            lbDistributorAddr.Text = dt.Rows[0]["DistributorAddr"].ToString();
            lbContactName.Text = dt.Rows[0]["ContactName"].ToString();
            lbReceivingParty.Text = dt.Rows[0]["ReceivingParty"].ToString();
            lbAcquiringParty.Text = dt.Rows[0]["AcquiringParty"].ToString();
            lbCurrency.Text = dt.Rows[0]["Currency"].ToString();
            lbCurrency2.Text = lbCurrency.Text;
            lbBuyerAccount.Text = dt.Rows[0]["BuyerAccount"].ToString();

            lbWorkFlowStatus.Text = CommonCode.GetWorkFlowStatusText(workFlowStatus);

            lbPlay.Text = dt.Rows[0]["PlayType"].ToString();//付款方式
            lbPlay2.Text = lbPlay.Text;
            lbTemple.Text = dt.Rows[0]["TemplateName"].ToString();//合同模板 TemplateId
            lbPriceClause.Text = dt.Rows[0]["PriceClause"].ToString();//价格条款
            txtRemark.Text = dt.Rows[0]["Remark"].ToString();//摘要

            string bDate = dt.Rows[0]["AgreementBDate"].ToString();
            txtBeginDate.Text = bDate;

            bDate = dt.Rows[0]["AgreementEDate"].ToString();
            txtEndDate.Text = bDate;

            DataTable dtp = null;
            dtp = bll.GetContactProductByCID(dt.Rows[0]["id"].ToString());
            Repeater1.DataSource = dtp;
            Repeater1.DataBind();

            //金额合计
            double amount = 0;
            if (dtp != null && dtp.Rows.Count > 0)
            {
                foreach (DataRow r in dtp.Rows)
                {
                    amount += string.IsNullOrEmpty(r["Amount"].ToString()) ? 0 : double.Parse(r["Amount"].ToString());
                }
            }
            lbTotal.Text = amount.ToString();
        }
        else
        {
            ShareCode.AlertMsg(this, "play", "确认信息出错，请重新选择。", "Distributor.aspx", false);
        }
    }

    /// <summary>
    /// 供应商确认
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCom_Click(object sender, EventArgs e)
    {
        f_SRM_Result model = new f_SRM_Result();
        model.id = hdfid.Value;
        model.WorkFlowStatus = "4";//供应商确认
        model.SRM_Contract_NO = lbSrmNo.Text.Trim();

        int relt = bll.UpdateContactInfo(model);
        if (relt > 0)
        {
            ShareCode.AlertMsg(this, "play", "确认成功", "Distributor.aspx", false);

            //LoadData(hdfid.Value);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string remark = txtBackRemark.Text;
        if (string.IsNullOrEmpty(remark))
        {
            ShareCode.AlertMsg(this, "play", "请输入回退原因", "", false);
            return;
        }

        f_SRM_Result model = new f_SRM_Result();
        model.id = hdfid.Value;
        model.WorkFlowStatus = "3";//供应商回退
        model.SRM_Contract_NO = lbSrmNo.Text.Trim();
        model.DistributorRemark = remark;

        int relt = bll.UpdateContactInfo(model);
        if (relt > 0)
        {
            ShareCode.AlertMsg(this, "play", "回退操作完成", "Distributor.aspx", false);
        }
    }
}