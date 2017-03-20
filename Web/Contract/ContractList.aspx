<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractList.aspx.cs" Inherits="Contract_ContractList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>合同清单</title>
    <!-- Bootstrap -->
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/css/styles.css" rel="stylesheet" />

    <!--[if lt IE 9]>
      <script src="https://cdn.bootcss.com/html5shiv/3.7.3/html5shiv.min.js"></script>
      <script src="https://cdn.bootcss.com/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <form id="form1" runat="server">

        <div class="lbl-main">
            <nav class="navbar navbar-default">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <a class="navbar-brand" href="#">可创建合同清单</a>
                    </div>
                </div>
            </nav>
            <div class="fr navbar">
                <asp:Button ID="btnRef" CssClass="btn btn-default" runat="server" OnClick="btnRef_OnClick" Text="刷新" />
            </div>

            <div class="row">
                <div class="col-md-12">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>操作</th>
                                <th>序号</th>
                                <th>批准日期</th>
                                <th>比价单号</th>
                                <th>SRM合同号</th>
                                <th>发放号</th>
                                <th>ERP合同号</th>
                                <th>发放号</th>
                                <th>供应商</th>
                                <th>币种</th>
                                <th>流程状态</th>
                                <th>合同模板</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td colspan="12">
                                    <asp:TextBox ID="txtNo" CssClass="soso" placeholder="搜索框" runat="server" OnTextChanged="txtNo_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                </td>
                            </tr>
                        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_OnItemCommand">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnInfo" runat="server" CssClass="btn btn-default" CommandName="Insert" CommandArgument='<%#Eval("id") %>' Text="明细维护" />
                                    </td>
                                    <td><%#Eval("rowno")%></td>
                                    <td><%#Eval("OAAuditDate")%></td>
                                    <td><%#Eval("XunJiaWorkflowID")%></td>
                                    <td>
                                        <a href='ContractEdit.aspx?id=<%#Eval("id") %>'><%#Eval("SRM_Contract_NO")%></a>
                                    </td>
                                    <td><%#Eval("GrantNO")%></td>
                                    <td><%#Eval("ERP_Contract_NO")%></td>
                                    <td></td>
                                    <td><%#Eval("DistributorName")%></td>
                                    <td><%# string.IsNullOrEmpty(Eval("Currency").ToString())?Eval("AbCurrency"):Eval("Currency")%></td>
                                    <td>
                                        <%#Eval("WorkFlowStatus").ToString()=="3"?"<a href='javascript:;' title='原因："+Eval("DistributorRemark")+"'>"+GetWorkFlowStatusText(Eval("WorkFlowStatus").ToString())+"<a>":
                                        Eval("WorkFlowStatus").ToString()=="6"?"<a href='javascript:;' title='原因："+Eval("AuditRemark")+"'>"+GetWorkFlowStatusText(Eval("WorkFlowStatus").ToString())+"<a>":
                                        GetWorkFlowStatusText(Eval("WorkFlowStatus").ToString())%>
                                    </td>
                                    <td><%#Eval("TemplateName")%></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                        <%
                            string sm = "";
                            if (Repeater1.Items.Count == 0)
                            {
                                sm = "<tr><td colspan=\"12\" style='text-align:center'>";
                                sm += "<span style='color:red'>暂无数据！</span>";
                                sm += "</td></tr>";
                            } %>
                        <%=sm %>
                            </tbody>
                    </table>

                    <div class="pull-right">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pagination" Width="100%" HorizontalAlign="right" PageSize="10"
                            OnPageChanged="AspNetPager1_PageChanged" ShowMoreButtons="true" FirstPageText="首页" LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页">
                        </webdiyer:AspNetPager>
                    </div>

                </div>
            </div>

        </div>
    </form>

    <script src="/script/jquery.min.js"></script>
    <script src="/script/bootstrap.min.js"></script>
</body>
</html>
