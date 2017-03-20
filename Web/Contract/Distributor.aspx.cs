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
/// 供应商合同清单
/// </summary>
public partial class Contact_Distributor : System.Web.UI.Page
{
    private readonly ContactBLL bll = new ContactBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindData();
        }
    }

    void bindData()
    {
        f_SRM_Result model = new f_SRM_Result();
        model.WorkFlowStatus = "2";//供应商确认中
        model.SRM_Contract_NO = txtNo.Text.Trim();

        int total = 0;
        DataTable dt = null;
        if (string.IsNullOrEmpty(model.SRM_Contract_NO))//一般查询
        {
            total = 0;
            dt = bll.GetContactList(model, AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, ref total);
        }
        else//合同清单列表【根据单号，模糊查询】
        {
            total = 0;
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

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        bindData();
    }

    /// <summary>
    /// 刷新
    /// </summary>
    /// <param name="send"></param>
    /// <param name="e"></param>
    protected void btnRef_OnClick(object sender, EventArgs e)
    {
        bindData();
    }

    public string GetWorkFlowStatusText(string WorkFlowStatus)
    {
        return CommonCode.GetWorkFlowStatusText(WorkFlowStatus);
    }

    /// <summary>
    /// TextBox输入事件
    /// </summary>
    /// <param name="src"></param>
    /// <param name="e"></param>
    protected void txtNo_OnTextChanged(object sender, EventArgs e)
    {
        //string qV = ((TextBox)sender).Text;
        //Response.Write("<script>alert('"+qV+"');</script>");
        bindData();
    }

    protected void Repeater1_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            string id = e.CommandArgument.ToString();

            Response.Write("<script>alert('" + id + "');</script>");
            Response.Redirect("DistributorConfirm.aspx?id=" + id);
        }
    }
}