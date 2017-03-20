<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractEdit.aspx.cs" Inherits="Contract_ContractEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>合同数据页</title>
    <!-- Bootstrap -->
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/css/styles.css" rel="stylesheet" />

    <!--[if lt IE 9]>
      <script src="https://cdn.bootcss.com/html5shiv/3.7.3/html5shiv.min.js"></script>
      <script src="https://cdn.bootcss.com/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script type="text/javascript" src="/Script/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <nav class="navbar navbar-default">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <a class="navbar-brand" href="#">合同明细维护</a>
                    </div>
                </div>
            </nav>
            <div class="fr navbar">
                <asp:HiddenField ID="hdfid" runat="server" />
                <asp:HiddenField ID="hdfSRMNo" runat="server" />
                <asp:HiddenField ID="hdfStatus" runat="server" />
                <asp:HiddenField ID="hdfBackStatus" runat="server" />
                <asp:HiddenField ID="hdfUrl" runat="server" />

                <button type="button" class="btn btn-default" onclick="PrintShow()">打印预览</button>

                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-default" Text="保存" OnClick="btnSave_Click" OnClientClick="javascript:return SaveConfirm()" />
                <asp:Button ID="btnCom" runat="server" CssClass="btn btn-default" Text="供应商确认" OnClick="btnCom_Click" />
                <asp:Button ID="btnAud" runat="server" CssClass="btn btn-default" Text="提交审批" OnClick="btnAud_Click" />
                <asp:Button ID="btnBack" runat="server" CssClass="btn btn-default" Text="退出" OnClick="Button1_Click" OnClientClick="javascript:return MyConfirm()" />
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
                                <td>
                                    <asp:Label ID="lbGrantNo" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>付款方式</th>
                                <td>
                                    <select id="sltPlay" runat="server" class="form-control" onchange="PlaySelect(this.value,'sltPlay2')">
                                    </select>
                                </td>
                                <th>合同模板</th>
                                <td>
                                    <select id="sltTemple" runat="server" class="form-control">
                                    </select>
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
                                    <a id="aStatus" runat="server" href="javascript:;">
                                        <asp:Label ID="lbWorkFlowStatus" runat="server"></asp:Label>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <th>摘要</th>
                                <td colspan="6">
                                    <asp:TextBox ID="txtRemark" CssClass="form-control" runat="server" MaxLength="500"></asp:TextBox>
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
                                            <td>
                                                <asp:Label ID="lbCode" runat="server" Text='<%#Eval("ItemCode")%>'></asp:Label></td>
                                            <td></td>
                                            <td>
                                                <asp:Label ID="lbClassificatiion" runat="server" Text='<%#Eval("Classificatiion")%>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbItemName" runat="server" Text='<%#Eval("ItemName")%>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbUnit" runat="server" Text='<%#Eval("Unit")%>'></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Quantity")%>' Width="80px" MaxLength="9" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                            <td>
                                                <asp:Label ID="lbPrice" runat="server" Text='<%#Eval("UnitPrice")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPromiseDate" runat="server" class="Wdate" Width="100px" Text='<%# string.IsNullOrEmpty(Eval("PromiseDate").ToString()) ? "" : Convert.ToDateTime(Eval("PromiseDate")).ToString("yyyy-MM-dd")%>'
                                                    onFocus="WdatePicker({ dateFmt: 'yyyy-MM-dd'})"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbRequireDate2" runat="server" Text='<%# string.IsNullOrEmpty(Eval("RequireDate").ToString()) ? "" : Convert.ToDateTime(Eval("RequireDate")).ToString("yyyy-MM-dd")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRemark" runat="server" Width="100px" Text='<%#Eval("CRemark")%>'></asp:TextBox>
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

                                <table id="tbProductList" class="table table-bordered" style="display: none">
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
                                        <tr onclick="CkRow('1','物料主数据带出')">
                                            <td>1</td>
                                            <td>物料主数据带出</td>
                                            <td></td>
                                            <td>物料主数据带出</td>
                                            <td>物料主数据带出</td>
                                            <td>按设定的长度显示，不分行</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td>必填</td>
                                            <td>非必填</td>
                                            <td>必填</td>
                                            <td>必填</td>
                                            <td>必填</td>
                                            <td>必填</td>
                                            <td>必填</td>
                                            <td>必填</td>
                                            <td>必填</td>
                                            <td>必填</td>
                                            <td>非必填</td>
                                        </tr>
                                        <tr>
                                            <td>3</td>
                                            <td>物料主数据带出</td>
                                            <td></td>
                                            <td>物料主数据带出</td>
                                            <td>物料主数据带出</td>
                                            <td>按设定的长度显示，不分行</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <th colspan="2">物料编码</th>
                                            <td><span id="lbNo1"></span></td>
                                            <th colspan="2">物料名称</th>
                                            <td colspan="7"><span id="lbName1"></span></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="tab-pane fade" id="profile">
                                <div class="row">
                                    <div class="col-md-6">
                                        <table class="table table-bordered">
                                            <tbody>
                                                <tr>
                                                    <th>付款方式</th>
                                                    <td>
                                                        <select id="sltPlay2" runat="server" class="form-control" onchange="PlaySelect(this.value,'sltPlay')">
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>价格条款</th>
                                                    <td>
                                                        <select id="sltPriceClause" runat="server" class="form-control">
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>协议有效开始日期</th>
                                                    <td>
                                                        <input type="text" runat="server" id="txtBeginDate" name="txtBeginDate" readonly="readonly" class="Wdate" onfocus="WdatePicker({ dateFmt: 'yyyy-MM-dd', maxDate: '#F{$dp.$D(\'txtEndDate\')}' })" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>协议有效结束日期</th>
                                                    <td>
                                                        <input type="text" runat="server" id="txtEndDate" name="txtEndDate" readonly="readonly" class="Wdate" onfocus="WdatePicker({ dateFmt: 'yyyy-MM-dd', minDate: '#F{$dp.$D(\'txtBeginDate\')}' })" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>币种</th>
                                                    <td>
                                                        <select id="sltCurrency" runat="server" class="form-control" onchange="OnChangeCurrency(this.value)">
                                                        </select>
                                                    </td>
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
    <script type="text/javascript">
        function CkRow(id, v) {
            $("#lbNo").text(id);
            $("#lbName").text(v);
        }

        function PlaySelect(v, id) {
            $("#" + id).val(v);
        }

        function OnChangeCurrency(v) {
            $("#lbCurrency").text(v);
        }

        function PrintShow() {
            var tid = $("#sltTemple").val();
            location.href = "ContractPrint.aspx?id=" + $("#hdfid").val() + "&tId=" + tid;
        }

        //Js监听Ctrl+S命令，保存内容 
        function keyDown(e) {
            e.preventDefault();
            var currKey = 0, e = e || event || window.event;
            currKey = e.keyCode || e.which || e.charCode;
            if (currKey == 83 && (e.ctrlKey || e.metaKey)) {
                document.getElementById("btnSave").click();
                return false;
            }
        }
        document.onkeydown = keyDown;

        function SaveConfirm() {
            var isC = $("#hdfStatus").val();
            if (parseInt(isC) > 1) {//保存提示
                if (confirm("合同确认审核中，保存后将重新确认审核。是否保存？") == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return true;
            }
        }

        function MyConfirm() {
            var isC = $("#hdfBackStatus").val();
            if (isC == "1") {//为1时退出保存提示
                if (confirm("退出前是否保存信息?") == true) {
                    return true;
                }
                else {
                    location.href = "ContractList.aspx";
                    return false;
                }
            }
            else {
                if ($("#hdfStatus").val() == "7") {
                    location.href = $("#hdfUrl").val();
                    return false;
                }
                location.href = "ContractList.aspx";
                return false;
            }
        }
    </script>
</body>
</html>
