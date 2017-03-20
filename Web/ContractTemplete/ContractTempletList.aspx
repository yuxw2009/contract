<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractTempletList.aspx.cs" Inherits="ContractTemplete_ContractTempletList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <title>合同模版列表</title>
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
        <div class="container">
            <nav class="navbar navbar-default">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <a class="navbar-brand" href="#">合同模板浏览</a>
                    </div>
                </div>
            </nav>
            <div class="fr navbar">
                <button type="button" class="btn btn-default" onclick="javascript:location.href='ContractTempletEdit.aspx'">新建</button>
                <button type="button" class="btn btn-default" onclick="SaveData()">保存</button>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <table id="tbDataList" class="table table-bordered">
                        <thead>
                            <tr>
                                <th>编号</th>
                                <th>版本号</th>
                                <th>名称</th>
                                <th>适用业务</th>
                                <th>采购组织</th>
                                <th>采购员</th>
                                <th>供应商</th>
                                <th>创建日期</th>
                                <th>创建人</th>
                                <th>状态</th>
                                <th>操作</th>
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
                                <tr id='tr<%#Eval("id")%>'>
                                    <td>
                                        <input type="hidden" id="hdfId<%#Eval("id")%>" value='<%#Eval("id")%>' />
                                        <input type="hidden" id="hdfst<%#Eval("id")%>" value="0" />
                                        <%#Eval("Code")%>
                                    </td>
                                    <td><%#Eval("CTVersion")%></td>
                                    <td>
                                        <a href='ContractTempletEdit.aspx?id=<%#Eval("ID") %>'><%#Eval("CTName") %></a>
                                    </td>
                                    <td>
                                        <%--<%#Eval("CTBusiness")%>--%>
                                         <select id="sltCTBusiness<%#Eval("id")%>" name="sltStatus">
                                            <%#GetCTBusinessOption(Eval("CTBusiness").ToString())%>
                                        </select>
                                    </td>
                                    <td>
                                        <%-- <%#Eval("CTPurchaseOrg")%>--%>
                                        <select id="sltCTPurchaseOrg<%#Eval("id")%>" name="sltStatus">
                                            <%#GetPRProjectOption(Eval("ProjectNr").ToString())%>
                                        </select>
                                    </td>
                                    <td>
                                        <%--<%#Eval("CTBuyer")%>--%>
                                        <select id="sltCTBuyer<%#Eval("id")%>" name="sltStatus">
                                            <%#GetCTBuyerOption(Eval("CTBuyer").ToString())%>
                                        </select>
                                    </td>
                                    <td>
                                        <%--<%#Eval("CTSupplier")%>--%>
                                        <select id="sltCTSupplier<%#Eval("id")%>" name="sltStatus">
                                            <%#GetCTSupplierOption(Eval("CTSupplier").ToString())%>
                                        </select>
                                    </td>
                                    <td><%#Eval("CTCreateTime")%></td>
                                    <td><%#Eval("CTCreate")%></td>
                                    <td>
                                        <%--<%# GetCTStatusText(Eval("CTStatus").ToString())%>--%>
                                        <select id="sltStatus<%#Eval("id")%>" name="sltStatus">
                                            <%#GetCTStatusOption(Eval("CTStatus").ToString())%>
                                        </select>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnInfo" runat="server" CssClass="btn btn-default" CommandName="Copy" CommandArgument='<%#Eval("id") %>' Text="复制" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                        </tbody>
                        <%
                            string sm = "";
                            if (Repeater1.Items.Count == 0)
                            {
                                sm = "<tr><td colspan=\"12\" style='text-align:center'>";
                                sm += "<span style='color:red'>没有查询到相关记录！</span>";
                                sm += "</td></tr>";
                            } %>
                        <%=sm %>
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
    <script type="text/javascript">
        function SaveData() {
            var jsonStr = "[";
            var jsonStrV = "";
            $("#tbDataList tbody tr").each(function () {
                var trid = $(this).attr("id");
                if (trid != undefined) {
                    trid = trid.substring(2, trid.length);

                    var id = $("#hdfId" + trid).val();
                    var st = $("#hdfst" + trid).val();
                    var projectNr = $(this).find("select[id^='sltCTPurchaseOrg']").val();
                    var ctPurchaseOrg = $(this).find("select[id^='sltCTPurchaseOrg']").find("option:selected").text();
                    var ctBusiness = $(this).find("select[id^='sltCTBusiness']").val();
                    var ctBuyer = $(this).find("select[id^='sltCTBuyer']").val();
                    var ctSupplier = $(this).find("select[id^='sltCTSupplier']").val();
                    var statusV = $(this).find("select[id^='sltStatus']").val();

                    jsonStrV += "{";
                    jsonStrV += "ID:'" + id + "'";
                    jsonStrV += ",CTStatus:'" + statusV + "'";
                    jsonStrV += ",ProjectNr:'" + projectNr + "'"
                    jsonStrV += ",CTPurchaseOrg:'" + ctPurchaseOrg + "'"
                    jsonStrV += ",CTBusiness:'" + ctBusiness + "'"
                    jsonStrV += ",CTBuyer:'" + ctBuyer + "'"
                    jsonStrV += ",CTSupplier:'"+ctSupplier+"'"
                    
                    jsonStrV += "},";
                }
            });
            if (jsonStrV.length <= 0) {
                alert("没有修改信息可保存");
                return;
            }
            jsonStrV = jsonStrV.substring(0, jsonStrV.length - 1);
            jsonStr += jsonStrV;
            jsonStr += "]";

            //alert(jsonStr); return;

            $.ajax({
                type: "Post",
                url: "/ContractTemplete/ContractTempHandler.ashx",
                data: { ty: "0", jsonStr: jsonStr },
                beforeSend: function () {
                    //$("#btnyue").val("提交中,请稍候...");
                    //$("#btnyue").unbind("click");
                },
                success: function (relt) {
                    if (relt == "1") {
                        alert("保存成功");
                        location.href = location.href;
                    }
                    else {
                        alert("保存失败");
                    }
                },
                error: function () {

                }
            });
        }
    </script>
</body>
</html>
