<%@ Page Title="" Language="C#" MasterPageFile="~/BS.Master" AutoEventWireup="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="span12">
            <form class="form-inline">
                <input type="text" class="input-small" placeholder="姓名">
                <input type="text" class="input-small" placeholder="电话">
                <label class="checkbox">
                    <input type="checkbox">正常
                </label>
                <div class="input-prepend">
                    <span class="add-on">状态</span>
                    <select class="input-small">
                        <option value=""></option>
                        <option value="1">正常</option>
                        <option value="2">取消</option>
                    </select>
                </div>
                <a id="op-q" class="btn">确定</a>
            </form>
        </div>
    </div>
    <div class="row">
        <div class="span12">
            <table class="table table-condensed table-hover table-bordered table-striped">
                <thead>
                    <tr>
                        <th>com_person_id</th>
                        <th>person_no</th>
                        <th>person_name</th>
                        <th>sex</th>
                        <th>#</th>
                    </tr>
                </thead>
                <tbody id="result">
                    <tr>
                        <td colspan="5"><strong class="pull-right">无数据</strong></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
    <script>
        regScript('/Scripts/JayDataContext.js');
        regScript('/Core/jquery.form.js', '/Core/index.js');
        $(function () {
            $('#op-q').click(function () {
                var pars = {
                    sql: 'select top 2 person_id,name,key_id, birthday, mobile from com_personnel where name like ? and sex = ?',
                    p1: '郑%',
                    p2: '女'
                };
                $.post(AppPath + '/HY.Frame.Bis.BisAXXXX.Query.c', pars, function (obj) {
                    //obj = evalJson(obj);
                    alert(typeof obj);
                    obj = evalJson(obj);
                    alert(obj[1].name);
                });
                //$.ajax({
                //    type: "POST",
                //    url: AppPath + '/HY.Frame.Bis.BisAXXXX.Query.c',
                //    data: pars,
                //    dataType: "json",
                //    success: function (obj) {
                //        alert(typeof obj);
                //    }
                //});
            });
        });
    </script>
</asp:Content>
