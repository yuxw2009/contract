<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testMenus.aspx.cs" Inherits="Contract_testMenus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ul>
        <li>
            <a href="ContractList.aspx" target="_blank">合同清单</a>
        </li>
        <li>
            <a href="Distributor.aspx" target="_blank">供应商合同清单</a>
        </li>
        <li>
            <a href="ContractApproval.aspx" target="_blank">采购员提交审批合同清单</a>
        </li>
        <li>
            <a href="ContractAuditList.aspx" target="_blank">采购经理审批合同清单</a>
        </li>
        <li>
            <a href="ContractEffectiveList.aspx" target="_blank">生效合同清单</a>
        </li>
        <li>
            <a href="../ContractTemplete/ContractTempletList.aspx" target="_blank">合同模版列表</a>
        </li>
    </ul>
    </div>
    </form>
</body>
</html>
