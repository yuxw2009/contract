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
/// 采购经理合同审核确认
/// </summary>
public partial class Contract_ContractAuditConfirm : System.Web.UI.Page
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
        btnEdit.Visible = false;
        DataTable dt = bll.GetContactDataByID(id.Trim());
        if (dt != null && dt.Rows.Count > 0)
        {
            string workFlowStatus = dt.Rows[0]["WorkFlowStatus"].ToString().Trim();

            //状态为7（合同生效）的可查看信息
            if (Request.QueryString["ty"] != null)
            {
                if (Request.QueryString["ty"].ToString() != "7")
                {
                    if (workFlowStatus != "5")//经理审批中
                    {
                        ShareCode.AlertMsg(this, "play", "确认信息出错，请重新选择。", "Distributor.aspx", false);
                        return;
                    }
                }
                else
                {
                    btnCom.Visible = false;
                    btnBacks.Visible = false;
                    btnEdit.Visible = true;
                }
            }
            else
            {
                if (workFlowStatus != "5")//经理审批中
                {
                    ShareCode.AlertMsg(this, "play", "确认信息出错，请重新选择。", "Distributor.aspx", false);
                    return;
                }
            }

            lbTime.Text = dt.Rows[0]["CreateTime"].ToString();
            lbBuyOrg.Text = dt.Rows[0]["BuyOrg"].ToString();

            string srmNo = dt.Rows[0]["SRM_Contract_NO"].ToString();
            lbSrmNo.Text = srmNo;
            hdfid.Value = dt.Rows[0]["id"].ToString();
            hdfStatus.Value = dt.Rows[0]["WorkFlowStatus"].ToString();


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

            lbTemple.Text = dt.Rows[0]["TemplateName"].ToString();//合同模板TemplateId
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
    /// 审核确认
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCom_Click(object sender, EventArgs e)
    {
        f_SRM_Result model = new f_SRM_Result();
        model.id = hdfid.Value;
        model.WorkFlowStatus = "7";//合同生效
        model.SRM_Contract_NO = lbSrmNo.Text.Trim();

        int relt = bll.UpdateContactInfo(model);
        if (relt > 0)
        {
            ShareCode.AlertMsg(this, "play", "审核成功", "ContractAuditList.aspx", false);

            //LoadData(hdfid.Value);
        }
        else
        {
            ShareCode.AlertMsg(this, "play", "审核失败", "", false);
        }
    }

    /// <summary>
    /// 回退
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        model.WorkFlowStatus = "6";//经理回退
        model.SRM_Contract_NO = lbSrmNo.Text.Trim();
        model.AuditRemark = remark;

        int relt = bll.UpdateContactInfo(model);
        if (relt > 0)
        {
            ShareCode.AlertMsg(this, "play", "回退操作完成", "ContractAuditList.aspx", false);
        }
    }
}