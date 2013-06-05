<%@ Page Title="" Language="C#" MasterPageFile="~/BS.Master" %>

<%@ Register Assembly="HY.Auth" TagPrefix="uc" Namespace="HY.Auth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <uc:ModConfigControl runat="server" CssClass="modcfg" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
    <script>
        $(function () { 
            $('body').on('dblclick', function () {
                var RECLS = './UpdateAuth.ashx';
                var vv = $('#' + ModConfig.id).data(ModConfig.key);
                $.post(RECLS, { mod: vv }, function (obj) {

                }, 'json');
            });
        });
    </script>
</asp:Content>
