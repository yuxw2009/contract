<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContractPrint.aspx.cs" Inherits="Contract_ContractPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>合同数据预览</title>
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
                        <a class="navbar-brand" href="#">合同数据预览</a>
                    </div>
                </div>
            </nav>
            <div class="fr navbar">
                <input type="button" class="btn btn-default" value="退出" onclick="onBack()" />
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lbHtml" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </form>

    <script src="/script/jquery.min.js"></script>
    <script src="/script/bootstrap.min.js"></script>
    <script type="text/javascript">
        function onBack() {
            history.go(-1);
        }
    </script>
</body>
</html>
