<%@ Page Title="" Language="C#" MasterPageFile="~/BS.Master" AutoEventWireup="true" CodeBehind="NavConfig.aspx.cs" Inherits="HY.Frame.Web.View.NavConfig" %>

<%@ Register Assembly="HY.Auth" TagPrefix="uc" Namespace="HY.Auth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <uc:NavConfigControl runat="server" CssClass="navcfg" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
    <script>
        $(function () {
            $('body').on('dblclick', function () {
                // test
                alert($('#' + NavConfig.id).data(NavConfig.key));
            });
        });
    </script>
</asp:Content>
