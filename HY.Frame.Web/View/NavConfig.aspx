<%@ Page Title="" Language="C#" MasterPageFile="~/BS.Master" %>

<%@ Register Assembly="HY.Auth" TagPrefix="uc" Namespace="HY.Auth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="//ext.lxing.me/resources/css/ext-all.css">
    <script src="//ext.lxing.me/bootstrap.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="span12">
            <uc:NavConfigControl ID="NavConfigControl1" runat="server" CssClass="navcfg" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
    
</asp:Content>
