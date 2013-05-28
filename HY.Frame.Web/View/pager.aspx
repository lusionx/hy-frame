<%@ Page Title="" Language="C#" MasterPageFile="~/BS.Master" AutoEventWireup="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="span12">
            <table class="table table-striped table-condensed" id="op-ls">
                <tbody></tbody>
            </table>
        </div>
        <div class="span12">
            <div id="op-pager"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
    <script src="/Core/BS.js"></script>
    <script src="/Core/BS.pager.js"></script>
    <script>
        $(function () {
            $('#op-pager').pager({
                url: 'data.ashx',
                par: function () {
                    return { Action: "aa" }
                },
                pageSize: 20,
                totalPrototype: 'total'
            });
            $('#op-pager').on('change', function (event, obj) {
                var row = obj.data;
                var htm = [];
                for (var i = 0; i < row.length; i++) {
                    htm.push('<tr>')
                    htm.push('<td>' + row[i].a + '</td>');
                    htm.push('<td>' + row[i].b + '</td>');
                    htm.push('</tr>')
                }
                $('#op-ls tbody').html(htm.join(''));
            });
            $('#op-pager').pager('go', 1);
        });
    </script>
</asp:Content>
