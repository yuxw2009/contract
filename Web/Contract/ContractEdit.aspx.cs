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
/// 合同数据页
/// </summary>
public partial class Contract_ContractEdit : System.Web.UI.Page
{
    private readonly ContactBLL bll = new ContactBLL();
    private readonly ContactTempletBLL tmpBll = new ContactTempletBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //下拉列表数据绑定
            //付款方式
            sltPlay.DataSource = CommonCode.GetOptionDataSource(CommonCode.PlayAr);
            sltPlay.DataTextField = "key";
            sltPlay.DataValueField = "value";
            sltPlay.DataBind();

            sltPlay2.DataSource = CommonCode.GetOptionDataSource(CommonCode.PlayAr);
            sltPlay2.DataTextField = "key";
            sltPlay2.DataValueField = "value";
            sltPlay2.DataBind();

            //价格条款
            sltPriceClause.DataSource = CommonCode.GetOptionDataSource(CommonCode.PriceClauseAr);
            sltPriceClause.DataTextField = "key";
            sltPriceClause.DataValueField = "value";
            sltPriceClause.DataBind();

            //币种
            sltCurrency.DataSource = CommonCode.GetOptionDataSource(CommonCode.CurrencyAr);
            sltCurrency.DataTextField = "key";
            sltCurrency.DataValueField = "value";
            sltCurrency.DataBind();

            //模板
            sltTemple.DataSource = tmpBll.GetContactTempletOption(null);
            sltTemple.DataTextField = "CTName";
            sltTemple.DataValueField = "ID";
            sltTemple.DataBind();

            if (Request.QueryString["id"] != null)
            {
                LoadData(Request.QueryString["id"].ToString());
            }
        }
    }

    private void LoadData(string id)
    {
        bool isD = false;//查询哪个表的产品，false没有生成合同时，根据询价单ID查询，true已生成合同，根据合同ID查询
        DataTable dt = bll.GetContactDataByID(id.Trim());
        if (dt != null && dt.Rows.Count > 0)
        {
            if (string.IsNullOrEmpty(dt.Rows[0]["CreateTime"].ToString()))
            {
                lbTime.Text = DateTime.Now.ToString();
            }
            else
            {
                lbTime.Text = dt.Rows[0]["CreateTime"].ToString();
            }

            lbBuyOrg.Text = dt.Rows[0]["BuyOrg"].ToString();

            string srmNo = dt.Rows[0]["SRM_Contract_NO"].ToString();
            if (string.IsNullOrEmpty(srmNo))
            {
                //srmNo = "90000001";
                srmNo = bll.GetSRMNo();//取得合同号

                hdfSRMNo.Value = "";
                isD = false;
            }
            else
            {
                hdfSRMNo.Value = srmNo;
                isD = true;
            }
            lbSrmNo.Text = srmNo;
            hdfid.Value = dt.Rows[0]["id"].ToString();

            lbERPNo.Text = dt.Rows[0]["ERP_Contract_NO"].ToString();
            lbGrantNo.Text = dt.Rows[0]["GrantNo"].ToString();
            lbDistributorName.Text = dt.Rows[0]["DistributorName"].ToString();
            lbDistributorAddr.Text = dt.Rows[0]["DistributorAddr"].ToString();
            lbContactName.Text = dt.Rows[0]["ContactName"].ToString();
            lbReceivingParty.Text = dt.Rows[0]["ReceivingParty"].ToString();
            lbAcquiringParty.Text = dt.Rows[0]["AcquiringParty"].ToString();

            string currency = dt.Rows[0]["Currency"].ToString();
            if (string.IsNullOrEmpty(currency))
            {
                currency = dt.Rows[0]["AbCurrency"].ToString();
            }
            if (string.IsNullOrEmpty(currency))
            {
                currency = "CNY";
            }
            lbCurrency.Text = currency;
            sltCurrency.Value = currency;

            lbBuyerAccount.Text = dt.Rows[0]["BuyerAccount"].ToString();

            string workFlowStatus = dt.Rows[0]["WorkFlowStatus"].ToString().Trim();
            lbWorkFlowStatus.Text = CommonCode.GetWorkFlowStatusText(workFlowStatus);

            //判断显示功能按钮
            switch (workFlowStatus)
            {
                case "1"://明细维护
                    btnSave.Visible = true;
                    btnCom.Visible = true;
                    btnAud.Visible = false;
                    hdfStatus.Value = "1";
                    hdfBackStatus.Value = "1";
                    break;
                case "2"://供应商确认中
                    btnSave.Visible = true;
                    btnCom.Visible = false;
                    btnAud.Visible = false;
                    hdfStatus.Value = "2";
                    hdfBackStatus.Value = "0";
                    break;
                case "3"://供应商回退
                    btnSave.Visible = true;
                    btnCom.Visible = true;
                    btnAud.Visible = false;
                    hdfStatus.Value = "1";
                    hdfBackStatus.Value = "1";
                    aStatus.Title = dt.Rows[0]["DistributorRemark"].ToString();
                    break;
                case "4"://供应商确认
                    btnSave.Visible = true;
                    btnCom.Visible = false;
                    btnAud.Visible = true;
                    hdfStatus.Value = "4";
                    hdfBackStatus.Value = "0";
                    break;
                case "5"://经理审批中
                    btnSave.Visible = true;
                    btnCom.Visible = false;
                    btnAud.Visible = false;
                    hdfStatus.Value = "5";
                    hdfBackStatus.Value = "0";
                    break;
                case "6"://经理回退
                    btnSave.Visible = true;
                    btnCom.Visible = false;
                    btnAud.Visible = true;
                    hdfStatus.Value = "6";
                    hdfBackStatus.Value = "0";
                    aStatus.Title = dt.Rows[0]["AuditRemark"].ToString();
                    break;
                case "7"://合同生效
                    btnSave.Visible = true;
                    btnCom.Visible = false;
                    btnAud.Visible = false;
                    hdfStatus.Value = "7";
                    hdfBackStatus.Value = "0";
                    hdfUrl.Value = "ContractEffectiveList.aspx";
                    break;
                default://未创建
                    btnSave.Visible = true;
                    btnCom.Visible = false;
                    btnAud.Visible = false;
                    hdfStatus.Value = "1";
                    hdfBackStatus.Value = "1";
                    break;
            }


            sltPlay.Value = dt.Rows[0]["PlayType"].ToString();//付款方式
            sltPlay2.Value = dt.Rows[0]["PlayType"].ToString();//付款方式

            sltTemple.Value = dt.Rows[0]["TemplateId"].ToString();//合同模板
            sltPriceClause.Value = dt.Rows[0]["PriceClause"].ToString();//价格条款
            txtRemark.Text = dt.Rows[0]["Remark"].ToString();//摘要

            string bDate = dt.Rows[0]["AgreementBDate"].ToString();
            if (string.IsNullOrEmpty(bDate))
            {
                bDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                bDate = Convert.ToDateTime(bDate).ToString("yyyy-MM-dd");
            }
            txtBeginDate.Value = bDate;

            bDate = dt.Rows[0]["AgreementEDate"].ToString();
            if (string.IsNullOrEmpty(bDate))
            {
                bDate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
            }
            else
            {
                bDate = Convert.ToDateTime(bDate).ToString("yyyy-MM-dd");
            }
            txtEndDate.Value = bDate;

            DataTable dtp = null;
            //isD = false;//查询哪个表的产品，false没有生成合同时，根据询价单ID查询，true已生成合同，根据合同ID查询
            if (isD)
            {
                dtp = bll.GetContactProductByCID(dt.Rows[0]["id"].ToString());
            }
            else
            {
                string xjNo = dt.Rows[0]["XunJiaWorkflowID"].ToString();
                dtp = bll.GetContactProduct(xjNo);//Amount
            }

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
    }

    /// <summary>
    /// 保存
    /// <param name="url">保存后，跳转的页面，为空，则为当前页</param>
    /// </summary>
    private void SaveContact(string url)
    {
        string playType = sltPlay.Value.Trim();//付款方式
        string temple = sltTemple.Value.Trim();//合同模板
        string priceClause = sltPriceClause.Value.Trim();//价格条款
        string currency = sltCurrency.Value.Trim();//币种
        string remark = txtRemark.Text.Trim();//摘要

        string beginDate = txtBeginDate.Value.Trim();//协议有效开始日期
        string endDate = txtEndDate.Value.Trim();//协议有效结束日期


        f_SRM_Result model = new f_SRM_Result();
        model.id = hdfid.Value;
        model.SRM_Contract_NO = hdfSRMNo.Value;
        model.WorkFlowStatus = "1";
        model.TemplateId = temple;
        model.Remark = remark;
        model.PriceClause = priceClause;
        model.Currency = currency;
        model.AgreementBDate = beginDate;
        model.AgreementEDate = endDate;
        model.CreateTime = DateTime.Now;
        model.PlayType = playType;

        decimal totalAmount = 0;
        string amount = lbTotal.Text.Trim();//合同金额
        if (string.IsNullOrEmpty(amount))
        {
            amount = "0";
        }
        try
        {
            totalAmount = decimal.Parse(amount);
        }
        catch
        {
            ShareCode.AlertMsg(this, "play", "合计金额出错，请检查后再保存。", url, false);
            return;
        }
        model.Amount = totalAmount;

        int relt = bll.UpdateContactInfo(model);
        if (relt > 0)
        {
            //添加产品表
            ContactProduct modelp = new ContactProduct();
            IList<ContactProduct> modelList = new List<ContactProduct>();

            for (int i = 0; i < this.Repeater1.Items.Count; i++)
            {
                modelp = new ContactProduct();

                modelp.CID = hdfid.Value;//合同主表ID

                modelp.CType = "";//类型
                Label lb = (Label)this.Repeater1.Items[i].FindControl("lbCode");//物料编码
                modelp.ItemCode = lb.Text.Trim();
                modelp.CVersion = "";//版本
                lb = (Label)this.Repeater1.Items[i].FindControl("lbClassificatiion");//类别
                modelp.Classificatiion = lb.Text.Trim();
                lb = (Label)this.Repeater1.Items[i].FindControl("lbItemName");//物料名称
                modelp.ItemName = lb.Text.Trim();
                lb = (Label)this.Repeater1.Items[i].FindControl("lbUnit");//单位
                modelp.Unit = lb.Text.Trim();
                TextBox txt = (TextBox)this.Repeater1.Items[i].FindControl("txtQuantity");//数量
                modelp.Quantity = txt.Text.Trim();
                lb = (Label)this.Repeater1.Items[i].FindControl("lbPrice");//价格
                modelp.UnitPrice = lb.Text.Trim();
                txt = (TextBox)this.Repeater1.Items[i].FindControl("txtPromiseDate");//已承诺日期
                if (!string.IsNullOrEmpty(txt.Text))
                {
                    modelp.PromiseDate = Convert.ToDateTime(txt.Text.Trim());
                }
                lb = (Label)this.Repeater1.Items[i].FindControl("lbRequireDate2");//需求日期
                if (!string.IsNullOrEmpty(lb.Text))
                {
                    modelp.RequireDate = Convert.ToDateTime(lb.Text.Trim());
                }
                txt = (TextBox)this.Repeater1.Items[i].FindControl("txtRemark");//备注
                modelp.CRemark = txt.Text.Trim();

                modelList.Add(modelp);
            }
            bll.AddContactProduct(modelList);

            ShareCode.AlertMsg(this, "play", "保存成功", url, false);

            LoadData(hdfid.Value);
        }
        else
        {
            ShareCode.AlertMsg(this, "play", "保存失败", "", false);
        }
    }

    /// <summary>
    /// 保存合同（生成合同）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string url = "";
        string status = hdfStatus.Value.Trim();
        if (status == "7")
        {
            url = hdfUrl.Value.Trim();
        }
        SaveContact(url);
    }

    /// <summary>
    /// 金额合计（重新输入数量时）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        //金额合计
        double amount = 0;
        int quantity = 0;
        double price = 0;
        for (int i = 0; i < this.Repeater1.Items.Count; i++)
        {
            TextBox txt = (TextBox)this.Repeater1.Items[i].FindControl("txtQuantity");
            Label lb = (Label)this.Repeater1.Items[i].FindControl("lbPrice");

            if (!ShareCode.VldInteger(txt.Text))
            {
                ShareCode.AlertMsg(this, "shopName", "数量,请输入正整数", "", false);
                return;
            }

            quantity = string.IsNullOrEmpty(txt.Text) ? 0 : int.Parse(txt.Text);
            price = string.IsNullOrEmpty(lb.Text) ? 0 : double.Parse(lb.Text);

            amount += quantity * price;
        }
        lbTotal.Text = amount.ToString();
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
        model.WorkFlowStatus = "2";//供应商确认中
        model.SRM_Contract_NO = hdfSRMNo.Value;

        int relt = bll.UpdateContactInfo(model);
        if (relt > 0)
        {
            ShareCode.AlertMsg(this, "play", "提交供应商确认成功", "", false);

            LoadData(hdfid.Value);
        }
    }

    /// <summary>
    /// 提交审核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAud_Click(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SaveContact("ContractList.aspx");
    }
}