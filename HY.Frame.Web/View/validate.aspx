<%@ Page Title="" Language="C#" MasterPageFile="~/BS.Master" AutoEventWireup="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>验证</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="span12">
            <h3>验证 jQuery Validate</h3>
        </div>
        <div class="span9 offset2">
            <form id="op-form" class="form-horizontal">
                <div class="control-group">
                    <label class="control-label">Email</label>
                    <div class="controls">
                        <input type="text" name="f_email" placeholder="Email">
                        <span class="help-inline"></span>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">姓名</label>
                    <div class="controls">
                        <input type="text" name="f_name" placeholder="name">
                        <span class="help-inline"></span>
                    </div>
                </div>
                <div class="control-group">
                    <label class="control-label">字母</label>
                    <div class="controls">
                        <input type="text" name="f_letter" placeholder="Password">
                        <span class="help-inline"></span>
                    </div>
                </div>
                <div class="control-group">
                    <div class="controls">
                        <label class="checkbox">
                            <input type="checkbox">
                            Remember me
                        </label>
                        <span class="help-inline">Remember</span>
                    </div>
                </div>
                <div class="form-actions">
                    <button type="button" id="op-" class="btn btn-primary">Save changes</button>
                    <button type="button" class="btn">Cancel</button>
                </div>
            </form>
        </div>
        <div class="span12">
            <div id="op-pager"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
    <script src="/Core/BS.js"></script>
    <script src="/Core/BS.pager.js"></script>
    <script src="/Scripts/jquery.validate.min.js"></script>
    <script src="/Scripts/jquery.validate.messages_zh.js"></script>
    <script src="/Core/jquery.validate.ext.js"></script>
    <script>
        $.validator.setDefaults({
            debug: true
        });
        $(function () {
            $('#op-form').validate({
                rules: {
                    f_email: {
                        required: true,
                        email: true
                    },
                    f_name: 'required',
                    f_letter: 'lettersonly'
                }
            });
            $('#op-').click(function () {
                alert($('#op-form').valid());
            });
        });
    </script>
</asp:Content>
