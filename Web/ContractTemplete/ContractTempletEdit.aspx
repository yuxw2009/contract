<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="ContractTempletEdit.aspx.cs" Inherits="ContractTemplete_ContractTempletEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>合同模板编辑</title>
    <!-- Bootstrap -->
    <link href="/css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="/css/layui.css" rel="stylesheet" />--%>
    <link href="/css/styles.css" rel="stylesheet" />

    <!--[if lt IE 9]>
      <script src="https://cdn.bootcss.com/html5shiv/3.7.3/html5shiv.min.js"></script>
      <script src="https://cdn.bootcss.com/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>

    <form class="form-inline" id="form1" runat="server">

        <div class="container">

            <!-- Static navbar -->
            <nav class="navbar navbar-default">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <a class="navbar-brand" href="#">合同模板编辑</a>
                    </div>
                </div>
                <!--/.container-fluid -->
            </nav>
            <div class="fr">
                <asp:HiddenField ID="hdfid" runat="server" />
                <asp:HiddenField ID="hdfParam" runat="server" />
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-default" Text="保存" OnClick="btnSave_Click" OnClientClick="javascript:return SaveConfirm()" />
                <button type="button" id="btnBack" class="btn btn-default" onclick="JavaScript:location.href='ContractTempletList.aspx'">返回</button>
                <input type="button" class="btn btn-default" onclick="OnTest()" value="测试提取替换参数" />
            </div>
            <div class="clr"></div>
            <div class="page-header">
                <h1>基本信息</h1>
            </div>

            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="exampleInputName2">编号</label>
                        <input type="text" runat="server" class="form-control" id="txtCTCode" placeholder="编号" disabled="disabled" />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="exampleInputName2">版本号</label>
                        <input type="text" runat="server" class="form-control" id="txtCTVersion" placeholder="版本号" disabled="disabled" />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="exampleInputName2">状态</label>
                        <select class="form-control" id="sltCTStatus" runat="server">
                            <option value="0">编辑</option>
                            <option value="1">启用</option>
                            <option value="2">停用</option>
                        </select>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="exampleInputName2">名称</label>
                        <input type="text" class="form-control" id="txtCTName" runat="server" placeholder="名称" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="exampleInputName2">适用业务</label>
                        <select class="form-control" id="sltCTBusiness" runat="server">
                            <option value="A类业务">A类业务</option>
                            <option value="B类业务">B类业务</option>
                            <option value="C类业务">C类业务</option>
                        </select>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="exampleInputName2">采购组织</label>
                        <select class="form-control" id="sltCTPurchaseOrg" runat="server" onchange="OnChangePur(this.id)">
                        </select>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="exampleInputName2">采购员</label>
                        <select class="form-control" id="sltCTBuyer" runat="server">
                            <option value="A采购员">A采购员</option>
                            <option value="B采购员">B采购员</option>
                            <option value="C采购员">C采购员</option>
                        </select>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="exampleInputName2">供应商</label>
                        <select class="form-control" id="sltCTSupplier" runat="server">
                            <option value="A供应商">A供应商</option>
                            <option value="B供应商">B供应商</option>
                            <option value="C供应商">C供应商</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="row navbar">
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="exampleInputName2">创建人：</label>
                        <input type="text" id="txtCTCreate" runat="server" class="form-control" disabled="disabled" />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="exampleInputName2">创建日期：</label>
                        <input type="text" id="txtCTCreateTime" runat="server" class="form-control" disabled="disabled" />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="checkbox">
                        <label>
                            <input type="checkbox" id="ckDefault" runat="server" />
                            默认版本
                        </label>
                    </div>
                </div>
            </div>
            <div class="page-header">
                <h1>
                    <div class="fl">模板设计</div>
                    <a href="#" class="textfr" data-toggle="modal" data-target=".bs-example-modal-lg">插入数</a>
                    <div class="clr"></div>
                </h1>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="layui-form-item layui-form-text">
                        <%--<label class="layui-form-label">编辑器</label>--%>
                        <%--<div class="layui-input-block">--%>
                        <div style="display: none">
                            <asp:TextBox ID="txtEditor" runat="server"></asp:TextBox>
                        </div>
                        <script id="editor" type="text/plain" style="width: 98%; height: 500px;"></script>
                        <%--</div>--%>
                    </div>
                </div>
            </div>

        </div>

    </form>

    <!-- Large modal -->
    <div id="myModal" class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
        <div class="modal-dialog modal-lg" role="document">

            <div class="modal-content">
                <div class="modal-header">
                    <%-- <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>--%>
                    <h4 class="modal-title" id="myLargeModalLabel">插入数据选择
                        <div class="fr">
                            <input type="button" class="btn btn-default" onclick="GetTitleFiled()" value="保存" />
                            <input type="button" class="btn btn-default" onclick="javascript: $('#myModal').modal('hide');" value="返回" />
                        </div>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="edit-tan">
                        <form>
                            <div class="page-header">
                                <h3>
                                    <label>
                                        <input type="radio" name="rdTitle" value="0" checked="checked" />
                                    采购订单表头数据：
                                        </label>
                                </h3>
                            </div>
                            <div id="divTitle">
                                <asp:Label ID="lbTitle" runat="server"></asp:Label>
                            </div>

                            <div class="page-header">
                                <h3>
                                     <label>
                                        <input type="radio" name="rdTitle" value="1"/>
                                    采购订单行数据：
                                        </label>
                                </h3>
                            </div>
                            <div id="divTBTitle" >
                                <asp:Label ID="lbTbTitle" runat="server"></asp:Label>
                                </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="/script/jquery.min.js"></script>
    <script src="/script/bootstrap.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="/script/ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/script/ueditor/ueditor.all.min.js"></script>
    <!--建议手动加在语言，避免在ie下有时因为加载语言失败导致编辑器加载失败-->
    <!--这里加载的语言文件会覆盖你在配置项目里添加的语言类型，比如你在配置项目里配置的是英文，这里加载的中文，那最后就是中文-->
    <script type="text/javascript" charset="utf-8" src="/script/ueditor/lang/zh-cn/zh-cn.js"></script>
    <script type="text/javascript" charset="utf-8" src="/script/ueditor/addTestCombox.js"></script>
    <script type="text/javascript" charset="utf-8" src="/script/ueditor/addCustomizeButton.js"></script>
    <script type="text/javascript">
        //实例化编辑器
        //建议使用工厂方法getEditor创建和引用编辑器实例，如果在某个闭包下引用该编辑器，直接调用UE.getEditor('editor')就能拿到相关的实例
        var ue = UE.getEditor('editor');

        //对编辑器的操作最好在编辑器ready之后再做
        ue.ready(function () {
            //设置编辑器的内容
            ue.setContent($("#txtEditor").val());
            ////获取html内容，返回: <p>hello</p>
            //var html = ue.getContent();
            ////获取纯文本内容，返回: hello
            //var txt = ue.getContentTxt();
        });

        $(document).ready(function () {
            $("input[type='radio'][name='rdTitle']").click(function () {
                var v = $(this).val();
                if (v == "1") {
                    $("#divTBTitle").show();
                    $("#divTitle").hide();
                }
                else {
                    $("#divTBTitle").hide();
                    $("#divTitle").show();
                }
            });
        });

        //参数字段设置
        function GetTitleFiled() {
            var tyV = $("input[type='radio'][name='rdTitle']:checked").val();
            var newHtml = "";
            if (tyV == "0") {//表头参数
                var rdoV = $("input[type='radio'][name='rdoTitle']:checked").val();
                var rdoText = $("input[type='radio'][name='rdoTitle']:checked").attr("title");
                newHtml = "<span style='color:red' class='" + rdoV + "'>" + rdoText + "</span>";
            }
            else {//行参数
                newHtml+="<table class=\"table table-bordered\">";
                newHtml += "<thead>";
                newHtml += "<tr class=\"firstRow\">";

                var tdStr="";
                $("input[type='checkbox'][name='ckTbTitle']:checked").each(function () {
                    var ckV = $(this).val();
                    var ckText = $(this).attr("title");
                    tdStr = "<span style='color:red' class='@" + ckV + "'>" + ckText + "</span>";

                    newHtml += "<td>" + tdStr + "</td>";
                });

                newHtml+="</tr>";
                newHtml += "</thead>";
                newHtml += "<tbody class='@RowData'></tbody>";
                newHtml+="</table>";
            }

            UE.getEditor('editor').execCommand('insertHtml', newHtml);
            $('#myModal').modal('hide');
        }

        function OnChangePur(id) {
            //var v = $("#" + id).val()+"-01";
            //$("#txtCTCode").val(v);
        }

        function SaveConfirm() {
            //var CTCode = $("#txtCTCode").val();
            //if ($.trim(CTCode) == "") {
            //    alert("请输入编号");
            //    return false;
            //}
            //var CTVersion = $("#txtCTVersion").val();
            //if ($.trim(CTVersion) == "") {
            //    alert("请输入版本号");
            //    return false;
            //}
            var CTName = $("#txtCTName").val();
            if ($.trim(CTName) == "") {
                alert("请输入名称");
                return false;
            }
            var nContent = ue.getContent();
            $("#txtEditor").val(nContent);

            var jsonStr = "";
            //取替换字段
            $(nContent).find("span[class^='#']").each(function () {
                //var prm = $(this).attr("class") + $(this).text();
                //alert(prm);
                jsonStr += "{" + $(this).text() + ":" + $(this).attr("class") + "},";
            });
            jsonStr = jsonStr.substring(0, jsonStr.length - 1);

            //表参数
            var titleStr = "";
            $(nContent).find("span[class^='@']").each(function () {
                titleStr += "{" + $(this).text() + ":" + $(this).attr("class").replace("@", "") + "},";
            });
            titleStr = titleStr.substring(0, titleStr.length - 1);

            jsonStr += ";" + titleStr;
            $("#hdfParam").val(jsonStr);

            return true;
        }

        function OnTest() {
            var nContent = ue.getContent();
            //var le = $(nContent).find("span[class^='#']").length;
            //alert(le);
            var jsonStr = "";
            //取替换字段
            $(nContent).find("span[class^='#']").each(function () {
                //var prm = $(this).attr("class") + $(this).text();
                //alert(prm);
                jsonStr += "{" + $(this).text() + ":" + $(this).attr("class") + "},";
            });
            jsonStr = jsonStr.substring(0, jsonStr.length - 1);

            var titleStr = "";
            $(nContent).find("span[class^='@']").each(function () {
                titleStr += "{" + $(this).text() + ":" + $(this).attr("class").replace("@", "") + "},";
            });
            titleStr = titleStr.substring(0, titleStr.length - 1);

            jsonStr += ";" + titleStr;
            alert(jsonStr);
            $("#hdfParam").val(jsonStr);
        }
    </script>

</body>
</html>
