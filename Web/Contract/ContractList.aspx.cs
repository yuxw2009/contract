using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using BLL;
using Common;
using Model;

/// <summary>
/// 合同清单
/// </summary>
public partial class Contract_ContractList : System.Web.UI.Page
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
        model.WorkFlowStatus = "0,1,2,3,4,5,6";
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
        //string statusStr = "未创建";
        //switch (WorkFlowStatus)
        //{
        //    case "1":
        //        statusStr = "明细维护";
        //        break;
        //    case "2":
        //        statusStr = "供应商确认中";
        //        break;
        //    case "3":
        //        statusStr = "供应商回退";
        //        break;
        //    case "4":
        //        statusStr = "经理审批中";
        //        break;
        //    case "5":
        //        statusStr = "经理回退";
        //        break;
        //    case "6":
        //        statusStr = "合同生效";
        //        break;
        //    default:
        //        statusStr = "未创建";
        //        break;
        //}
        //return statusStr;
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
            Response.Redirect("ContractEdit.aspx?id=" + id);
        }
    }
}