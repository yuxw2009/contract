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
/// 采购员提交审批合同清单
/// </summary>
public partial class Contract_ContractApproval : System.Web.UI.Page
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
        model.WorkFlowStatus = "4";
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

    public string GetWorkFlowStatusText(string WorkFlowStatus)
    {
        return CommonCode.GetWorkFlowStatusText(WorkFlowStatus);
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
    /// <summary>
    /// 提交审核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAudit_Click(object sender, EventArgs e)
    {
        List<f_SRM_Result> modelList = new List<f_SRM_Result>();
        f_SRM_Result model = new f_SRM_Result();
        for (int i = 0; i < this.Repeater1.Items.Count; i++)
        {
            CheckBox ck = (CheckBox)this.Repeater1.Items[i].FindControl("ckAudit");
            if (ck.Checked)
            {
                model = new f_SRM_Result();
                model.WorkFlowStatus = "5";//经理审批中

                HiddenField hdf = (HiddenField)Repeater1.Items[i].FindControl("hdfCID");
                model.id = hdf.Value.Trim();//合同ID

                hdf = (HiddenField)Repeater1.Items[i].FindControl("hdfSRMNO");
                model.SRM_Contract_NO = hdf.Value.Trim();//合同号

                modelList.Add(model);
            }
        }
        if (modelList.Count > 0)
        {
           int relt= bll.UpdateContactWorkFlowStatus(modelList);
           if (relt > 0)
           {
               ShareCode.AlertMsg(this, "play", "提交审核成功", "", false);
               bindData();
           }
           else
           {
               ShareCode.AlertMsg(this, "play", "提交审核失败", "", false);
               bindData();
           }
        }
        else
        {
            ShareCode.AlertMsg(this, "play", "请选择需要提交审核的合同", "", false);
        }
    }
}