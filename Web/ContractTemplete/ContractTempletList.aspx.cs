using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ContractTemplete_ContractTempletList : System.Web.UI.Page
{
    private readonly ContactTempletBLL bll = new ContactTempletBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                BindData();
            }
            catch (Exception ex)
            {
            }
        }

    }
    void BindData()
    {
        string queryStr=txtNo.Text.Trim();
        
        int total = 0;
        DataTable dt = null;
        if (string.IsNullOrEmpty(queryStr))//一般查询
        {
            total = 0;
            dt = bll.GetContactTemplet(null, AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, ref total);
        }
        else//合同清单列表【根据单号，模糊查询】
        {
            total = 0;
            ContactTemplet model = new ContactTemplet();
            model.CTCode = queryStr;
            model.CTVersion = queryStr;
            model.CTName = queryStr;
            model.CTBusiness = queryStr;
            model.CTPurchaseOrg = queryStr;
            model.CTBuyer = queryStr;
            model.CTSupplier = queryStr;
            model.ProjectNr = queryStr;
            dt = bll.GetFuzzyQuery(model, AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, ref total);
        }

        AspNetPager1.RecordCount = total;

        if (dt != null && dt.Rows.Count > 0)
        {
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        else
        {
            AspNetPager1.CurrentPageIndex = 1;
            Repeater1.DataSource = null;
            Repeater1.DataBind();
        }
    }

    /// <summary>
    /// TextBox输入事件
    /// </summary>
    /// <param name="src"></param>
    /// <param name="e"></param>
    protected void txtNo_OnTextChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void Repeater1_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //复制模板
        if (e.CommandName == "Copy")
        {
            string id = e.CommandArgument.ToString();

            int relt = bll.CopyContractTempleet(id);
            if (relt > 0)
            {
                ShareCode.AlertMsg(this, "play", "复制成功", "", false);
                BindData();
            }
            else
            {
                ShareCode.AlertMsg(this, "play", "复制失败", "", false);
            }
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData();
    }

    public string GetCTStatusText(string status)
    {
        string statusStr = "编辑";
        switch (status)
        {
            case "0":
                statusStr = "编辑";
                break;
            case "1":
                statusStr = "启用";
                break;
            case "2":
                statusStr = "停用";
                break;
            default:
                statusStr = "编辑";
                break;
        }
        return statusStr;
    }

    /// <summary>
    /// 状态
    /// </summary>
    public string[] statusAr = { "编辑", "启用", "停用" };
    /// <summary>
    /// 返回状态下拉选项
    /// </summary>
    /// <param name="defV">默认选中荐的值</param>
    /// <returns></returns>
    public string GetCTStatusOption(string defV)
    {
        StringBuilder strSlt = new StringBuilder();
        for (int i = 0; i < statusAr.Length; i++)
        {
            string v = statusAr[i];
            if (i.ToString() == defV)
            {
                strSlt.Append("<option  value=\"" + i + "\" selected=\"selected\">" + v + "</option>");
            }
            else
            {
                strSlt.Append("<option  value=\"" + i + "\">" + v + "</option>");
            }
        }
        return strSlt.ToString();
    }

    /// <summary>
    /// 适用业务
    /// </summary>
    public string[] businessAr = { "A类业务", "B类业务", "C类业务" };
    /// <summary>
    /// 返回适用业务下拉选项
    /// </summary>
    /// <param name="defV">默认选中荐的值</param>
    /// <returns></returns>
    public string GetCTBusinessOption(string defV)
    {
        StringBuilder strSlt = new StringBuilder();
        for (int i = 0; i < businessAr.Length; i++)
        {
            string v = businessAr[i];
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
    /// 采购员
    /// </summary>
    public string[] buyerAr = { "A采购员", "B采购员", "C采购员" };
    /// <summary>
    /// 返回采购员下拉选项
    /// </summary>
    /// <param name="defV">默认选中荐的值</param>
    /// <returns></returns>
    public string GetCTBuyerOption(string defV)
    {
        StringBuilder strSlt = new StringBuilder();
        for (int i = 0; i < buyerAr.Length; i++)
        {
            string v = buyerAr[i];
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
    /// 供应商
    /// </summary>
    public string[] supplierAr = { "A供应商", "B供应商", "C供应商" };
    /// <summary>
    /// 返回供应商下拉选项
    /// </summary>
    /// <param name="defV">默认选中荐的值</param>
    /// <returns></returns>
    public string GetCTSupplierOption(string defV)
    {
        StringBuilder strSlt = new StringBuilder();
        for (int i = 0; i < supplierAr.Length; i++)
        {
            string v = supplierAr[i];
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

    private static DataTable dtPRp = null;
    public string GetPRProjectOption(string defV)
    {
        //采购组织
        if (dtPRp == null || dtPRp.Rows.Count <= 0)
        {
            dtPRp = bll.GetPRProject();
        }
        return ShareCode.GetSelectOptionHtml(dtPRp, "ProjectNr", "Description", defV);
    }
}