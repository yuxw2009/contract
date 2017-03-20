using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ContractTemplete_ContractTempletEdit : System.Web.UI.Page
{
    private readonly ContactTempletBLL bll = new ContactTempletBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //采购组织
            sltCTPurchaseOrg.DataSource = bll.GetPRProject();
            sltCTPurchaseOrg.DataTextField = "Description";
            sltCTPurchaseOrg.DataValueField = "ProjectNr";
            sltCTPurchaseOrg.DataBind();

            txtCTCreate.Value = "admin";
            txtCTCreateTime.Value = DateTime.Now.ToString();
            txtCTVersion.Value = "00";

            GetTitleHtml();
            GetTbTitleHtml();

            if (Request.QueryString["id"] != null)
            {
                LoadData(Request.QueryString["id"].ToString());
            }
        }
    }

    /// <summary>
    /// 加载设置参数
    /// </summary>
    private void GetTitleHtml()
    {
        StringBuilder htmlStr = new StringBuilder();
        string value = "";
        string text = "";

        DataTable dt = GetTitleParamData();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            value = dt.Rows[i]["key"].ToString();
            text = dt.Rows[i]["value"].ToString();

            if (i == 0)
            {
                htmlStr.Append("<div class=\"row\">");
            }
            else if (i % 6 == 0)
            {
                htmlStr.Append("</div>");
                htmlStr.Append("<div class=\"row\">");
            }

            htmlStr.Append("<div class=\"col-sm-2 col-xs-6\">");
            htmlStr.Append("<div class=\"checkbox\">");
            htmlStr.Append("<label>");
            htmlStr.Append("<input type=\"radio\" name=\"rdoTitle\" value=\"#" + value + "#\" title=\"" + text + "\" />" + text);
            htmlStr.Append("</label>");
            htmlStr.Append("</div></div>");
        }
        htmlStr.Append("</div>");

        lbTitle.Text = htmlStr.ToString();
    }

    /// <summary>
    /// 加载表格设置参数
    /// </summary>
    private void GetTbTitleHtml()
    {
        StringBuilder htmlStr = new StringBuilder();
        string value = "";
        string text = "";

        DataTable dt = GetTbTitleParamData();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            value = dt.Rows[i]["key"].ToString();
            text = dt.Rows[i]["value"].ToString();

            if (i == 0)
            {
                htmlStr.Append("<div class=\"row\">");
            }
            else if (i % 6 == 0)
            {
                htmlStr.Append("</div>");
                htmlStr.Append("<div class=\"row\">");
            }

            htmlStr.Append("<div class=\"col-sm-2 col-xs-6\">");
            htmlStr.Append("<div class=\"checkbox\">");
            htmlStr.Append("<label>");
            htmlStr.Append("<input type=\"checkbox\" name=\"ckTbTitle\" value=\"#" + value + "#\" title=\"" + text + "\" />" + text);
            htmlStr.Append("</label>");
            htmlStr.Append("</div></div>");
        }
        htmlStr.Append("</div>");

        lbTbTitle.Text = htmlStr.ToString();
    }

    /// <summary>
    /// 表头变量数据
    /// </summary>
    /// <returns></returns>
    private DataTable GetTitleParamData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("key", Type.GetType("System.String"));
        dt.Columns.Add("value", Type.GetType("System.String"));

        dt.Rows.Add("SRM_Contract_NO", "SRM号");
        dt.Rows.Add("BuyOrg", "采购组织");
        dt.Rows.Add("CpzDate", "PO日期");
        dt.Rows.Add("CAmount", "总金额");
        dt.Rows.Add("Play", "付款方式");
        dt.Rows.Add("BeginDate", "到货日期");
        dt.Rows.Add("PriceClause", "价格条款");
        dt.Rows.Add("DistributorAddr", "收货地点");
        dt.Rows.Add("DistributorName", "供应商");

        return dt;
    }

    /// <summary>
    /// 表格变量数据
    /// </summary>
    /// <returns></returns>
    private DataTable GetTbTitleParamData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("key", Type.GetType("System.String"));
        dt.Columns.Add("value", Type.GetType("System.String"));

        dt.Rows.Add("ItemCode", "物料编码");
        dt.Rows.Add("ItemName", "物料名称");
        dt.Rows.Add("Unit", "单位");
        dt.Rows.Add("Quantity", "数量");
        dt.Rows.Add("UnitPrice", "单价");
        dt.Rows.Add("Amount", "金额");
        dt.Rows.Add("PromiseDate", "承诺日期");

        return dt;
    }

    private void LoadData(string id)
    {
        DataTable dt = bll.GetGetContactTemplet(id.Trim());
        if (dt != null && dt.Rows.Count > 0)
        {
            hdfid.Value = dt.Rows[0]["id"].ToString();
            txtCTCode.Value = dt.Rows[0]["ProjectNr"].ToString() + "-" + dt.Rows[0]["CTCode"].ToString();
            txtCTVersion.Value = dt.Rows[0]["CTVersion"].ToString();
            txtCTName.Value = dt.Rows[0]["CTName"].ToString();
            sltCTBusiness.Value = dt.Rows[0]["CTBusiness"].ToString();
            sltCTPurchaseOrg.Value = dt.Rows[0]["ProjectNr"].ToString();
            sltCTBuyer.Value = dt.Rows[0]["CTBuyer"].ToString();
            sltCTSupplier.Value = dt.Rows[0]["CTSupplier"].ToString();
            sltCTStatus.Value = dt.Rows[0]["CTStatus"].ToString();
            ckDefault.Checked = dt.Rows[0]["isDefault"].ToString() == "1" ? true : false;
            txtEditor.Text = dt.Rows[0]["CTRemark"].ToString();

            txtCTCreate.Value = dt.Rows[0]["CTCreate"].ToString();
            txtCTCreateTime.Value = dt.Rows[0]["CTCreateTime"].ToString();
        }
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = hdfid.Value;//ID号
        string CTCode = txtCTCode.Value;

        //判断编号是否存在
        //bool isEx = bll.ExistsCTCode(CTCode, id);
        //if (isEx)
        //{
        //    ShareCode.AlertMsg(this, "play", "编号已存在，请重新输入", "", false);
        //    return;
        //}
        //检查名称是否存在
        string CTName = txtCTName.Value;
        //isEx = bll.ExistsCTName(CTName, id);
        //if (isEx)
        //{
        //    ShareCode.AlertMsg(this, "play", "名称已存在，请重新输入", "", false);
        //    return;
        //}

        string CTVersion = txtCTVersion.Value;
        string CTBusiness = sltCTBusiness.Value;
        string ProjectNr = sltCTPurchaseOrg.Value;
        string CTPurchaseOrg = sltCTPurchaseOrg.Items[sltCTPurchaseOrg.SelectedIndex].Text;
        string CTBuyer = sltCTBuyer.Value;
        string CTSupplier = sltCTSupplier.Value;
        string CTStatus = sltCTStatus.Value;
        string isDefault = ckDefault.Checked ? "1" : "0";

        string CTRemark = txtEditor.Text;
        string param = "";// hdfParam.Value;
        List<string> listParam = CommonCode.TempParamRegex(CTRemark);//提取替换的标识
        if (listParam.Count > 0)
        {
            for (int i = 0; i < listParam.Count; i++)
            {
                param += listParam[i] + ",";
            }
            param = param.Substring(0, param.Length - 1);
        }

        string tbParam = "";
        List<string> listtbParam = CommonCode.TempTableParamRegex(CTRemark);//提取表头替换的标识
        if (listtbParam.Count > 0)
        {
            for (int i = 0; i < listtbParam.Count; i++)
            {
                tbParam += listtbParam[i] + ",";
            }
            tbParam += ";" + tbParam.Substring(0, tbParam.Length - 1);
        }
        param += ";" + tbParam;

        ContactTemplet model = new ContactTemplet();
        model.CTCode = CTCode;
        model.CTVersion = CTVersion;
        model.CTName = CTName;
        model.CTBusiness = CTBusiness;
        model.CTPurchaseOrg = CTPurchaseOrg;
        model.CTBuyer = CTBuyer;
        model.CTSupplier = CTSupplier;
        model.CTRemark = CTRemark;
        model.CTStatus = CTStatus;
        model.IsDefault = isDefault;
        model.ProjectNr = ProjectNr;
        model.Parameter = param;

        model.CTCreate = "admin";
        model.CTCreateTime = DateTime.Now;

        //model.ID = id;//ID号

        if (string.IsNullOrEmpty(model.ID))
        {
            int relt = bll.AddContactTemplet(model);
            if (relt > 0)
            {
                ShareCode.AlertMsg(this, "play", "保存成功", "ContractTempletList.aspx", false);
                return;
            }
            else
            {
                ShareCode.AlertMsg(this, "play", "保存失败", "", false);
                return;
            }
        }
        //else
        //{
        //    int relt = bll.UpdateContactTemplet(model);
        //    if (relt > 0)
        //    {
        //        ShareCode.AlertMsg(this, "play", "保存成功", "ContractTempletList.aspx", false);
        //        return;
        //    }
        //    else
        //    {
        //        ShareCode.AlertMsg(this, "play", "保存失败", "", false);
        //        return;
        //    }
        //}
    }
}