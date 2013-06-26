<%@ Page Title="" Language="C#" MasterPageFile="~/BS.Master" %>

<%@ Register Assembly="HY.Auth" TagPrefix="uc" Namespace="HY.Auth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="//ext.150.me/resources/css/ext-all.css">
    <script src="//ext.150.me/bootstrap.js"></script>
    <style>
        .x-body {
             font-size:14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="span12">
            <uc:NavConfigControl ID="NavConfigControl1" runat="server" CssClass="navcfg" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
    <script>
        
    </script>
</asp:Content>
