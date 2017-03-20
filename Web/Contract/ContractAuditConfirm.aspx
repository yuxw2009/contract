<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractAuditConfirm.aspx.cs" Inherits="Contract_ContractAuditConfirm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>采购经理合同审核确认</title>
    <!-- Bootstrap -->
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/css/styles.css" rel="stylesheet" />
    <link href="/css/layui.css" rel="stylesheet" />
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
                        <a class="navbar-brand" href="#">合同审核确认</a>
                    </div>
                </div>
            </nav>
            <div class="fr navbar">
                <asp:HiddenField ID="hdfid" runat="server" />
                <asp:HiddenField ID="hdfSRMNo" runat="server" />
                <asp:HiddenField ID="hdfStatus" runat="server" />

                <button type="button" class="btn btn-default" onclick="PrintShow()">打印预览</button>
                <asp:Button ID="btnCom" runat="server" CssClass="btn btn-default" Text="同意" OnClick="btnCom_Click" />
                <input runat="server" id="btnBacks" type="button" class="btn btn-default" onclick="MyBack()" value="回退" />
                <input type="button" id="btnEdit" runat="server" class="btn btn-default" onclick="EnditTo()" value="修改" />
                <button type="button" class="btn btn-default" onclick="javascript:window.history.go(-1);">退出</button>
            </div>
            <div style="display:none">
                <asp:Button ID="btnBack" runat="server" CssClass="btn btn-default" Text="回退" OnClick="btnBack_Click" />
                <asp:TextBox ID="txtBackRemark" runat="server"></asp:TextBox>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <table class="table table-bordered">
                        <tbody>
                            <tr>
                                <th>采购组织</th>
                                <td>
                                    <asp:Label ID="lbBuyOrg" runat="server"></asp:Label></td>
                                <th>创建时间</th>
                                <td>
                                    <asp:Label ID="lbTime" runat="server"></asp:Label></td>
                                <th>SRM合同号</th>
                                <td>
                                    <asp:Label ID="lbSrmNo" runat="server"></asp:Label></td>
                                <td><asp:Label ID="lbGrantNo" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>付款方式</th>
                                <td>
                                    <asp:Label ID="lbPlay" runat="server"></asp:Label>
                                </td>
                                <th>合同模板</th>
                                <td>
                                    <asp:Label ID="lbTemple" runat="server"></asp:Label>
                                </td>
                                <th>ERP合同号</th>
                                <td>
                                    <asp:Label ID="lbERPNo" runat="server"></asp:Label></td>
                                <td></td>
                            </tr>
                            <tr>
                                <th>供应商</th>
                                <td>
                                    <asp:Label ID="lbDistributorName" runat="server"></asp:Label></td>
                                <th>地点</th>
                                <td>
                                    <asp:Label ID="lbDistributorAddr" runat="server"></asp:Label></td>
                                <th>联系人</th>
                                <td colspan="2">
                                    <asp:Label ID="lbContactName" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>收货方</th>
                                <td>
                                    <asp:Label ID="lbReceivingParty" runat="server"></asp:Label></td>
                                <th>收单方</th>
                                <td>
                                    <asp:Label ID="lbAcquiringParty" runat="server"></asp:Label></td>
                                <th>币种</th>
                                <td colspan="2">
                                    <asp:Label ID="lbCurrency" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>采购员</th>
                                <td>
                                    <asp:Label ID="lbBuyerAccount" runat="server"></asp:Label></td>
                                <th>合计</th>
                                <td>
                                    <asp:Label ID="lbTotal" runat="server"></asp:Label></td>
                                <th>状态</th>
                                <td colspan="2">
                                    <asp:Label ID="lbWorkFlowStatus" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>摘要</th>
                                <td colspan="6">
                                    <asp:Label ID="txtRemark" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="bs-docs-example">
                        <ul id="myTab" class="nav nav-tabs">
                            <li class="active"><a href="#home" data-toggle="tab">行</a></li>
                            <li class=""><a href="#profile" data-toggle="tab">合同条款</a></li>
                        </ul>
                        <div id="myTabContent" class="tab-content">
                            <div class="tab-pane fade active in" id="home">

                                <asp:Repeater ID="Repeater1" runat="server">
                                    <HeaderTemplate>
                                        <table class="table table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>编号</th>
                                                    <th>类型</th>
                                                    <th>物料编码</th>
                                                    <th>版本</th>
                                                    <th>类别</th>
                                                    <th>物料名称</th>
                                                    <th>单位</th>
                                                    <th>数量</th>
                                                    <th>价格</th>
                                                    <th>已承诺日期</th>
                                                    <th>需求日期</th>
                                                    <th>备注</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr onclick="CkRow('<%#Eval("ItemCode")%>','<%#Eval("ItemName")%>')">
                                            <td><%#Eval("rowno")%></td>
                                            <td></td>
                                            <td><asp:Label ID="lbCode" runat="server" Text='<%#Eval("ItemCode")%>'></asp:Label></td>
                                            <td></td>
                                            <td><asp:Label ID="lbClassificatiion" runat="server" Text='<%#Eval("Classificatiion")%>'></asp:Label></td>
                                            <td><asp:Label ID="lbItemName" runat="server" Text='<%#Eval("ItemName")%>'></asp:Label></td>
                                            <td><asp:Label ID="lbUnit" runat="server" Text='<%#Eval("Unit")%>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="txtQuantity" runat="server" Text='<%#Eval("Quantity")%>' Width="80px" ></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbPrice" runat="server" Text='<%#Eval("UnitPrice")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtPromiseDate" runat="server" Text='<%# string.IsNullOrEmpty(Eval("PromiseDate").ToString()) ? "" : Convert.ToDateTime(Eval("PromiseDate")).ToString("yyyy-MM-dd")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbRequireDate2" runat="server" Text='<%# string.IsNullOrEmpty(Eval("RequireDate").ToString()) ? "" : Convert.ToDateTime(Eval("RequireDate")).ToString("yyyy-MM-dd")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtRemark" runat="server" Width="100px" Text='<%#Eval("CRemark")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <tr>
                                            <th colspan="2">物料编码</th>
                                            <td><span id="lbNo"></span></td>
                                            <th colspan="2">物料名称</th>
                                            <td colspan="7"><span id="lbName"></span></td>
                                        </tr>
                                        </table>

                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="tab-pane fade" id="profile">
                                <div class="row">
                                    <div class="col-md-6">
                                        <table class="table table-bordered">
                                            <tbody>
                                                <tr>
                                                    <th>付款方式</th>
                                                    <td>
                                                        <asp:Label ID="lbPlay2" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>价格条款</th>
                                                    <td>
                                                        <asp:Label ID="lbPriceClause" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>协议有效开始日期</th>
                                                    <td>
                                                        <asp:Label ID="txtBeginDate" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>协议有效结束日期</th>
                                                    <td>
                                                        <asp:Label ID="txtEndDate" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>币种</th>
                                                    <td><asp:Label ID="lbCurrency2" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <th>汇率类型</th>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <th>汇率日期</th>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <th>汇率</th>
                                                    <td></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>

    <script src="/script/jquery.min.js"></script>
    <script src="/script/bootstrap.min.js"></script>
    <script src="/script/layui.js"></script>
    <script type="text/javascript">
        function CkRow(id, v) {
            $("#lbNo").text(id);
            $("#lbName").text(v);
        }

        function PlaySelect(v, id) {
            $("#" + id).val(v);
        }

        layui.use(['layer'], function () {
            var layer = layui.layer;
        });

        function MyBack() {
            layer.prompt({
                formType: 2,
                //value: '初始值',
                title: '请输入回退原因',
                area: ['400px', '180px'] //自定义文本域宽高
            }, function (value, index, elem) {
                //得到value
                $("#txtBackRemark").val(value);
                layer.close(index);

                document.getElementById("btnBack").click();
            });
        }

        function EnditTo() {
            var status = $("#hdfStatus").val();
            if (status == "7") {
                location.href = "ContractEdit.aspx?id=" + $("#hdfid").val();
            }
        }
        function PrintShow() {
            location.href = "ContractPrint.aspx?id=" + $("#hdfid").val();
        }
    </script>
</body>
</html>
