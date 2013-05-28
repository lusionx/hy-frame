<%@ Page Title="" Language="C#" MasterPageFile="~/BS.Master" AutoEventWireup="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="span8">
            <table id="result" class="table table-condensed table-hover table-bordered table-striped">
                <thead>
                    <tr>
                        <th>com_person_id</th>
                        <th>person_no</th>
                        <th>person_name</th>
                        <th>sex</th>
                        <th>#</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div class="span4">
            <form>
                <fieldset>
                    <legend>编辑</legend>
                    <label></label>
                    <input type="text" name="person_name" placeholder="placeholder">
                    <span class="help-block">姓名</span>
                    <label class="checkbox">
                        <input type="checkbox">
                        Check me out
                    </label>
                    <button key="" id="submit" class="btn btn-primary">确认</button>
                </fieldset>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
    <script src="/Scripts/JayDataContext.js"></script>
    <script>
        var db = HY.Frame.Bis.context;
        var now = null;
        $(function () {
            var q = db.com_person;
            q.where(function (e) {
                return e.sex == '男';
            }).map(function (e) {//微软并没有实现 此约定, 返回还是 所有字段
                return {
                    person_no: e.person_no,
                    person_name: e.person_name,
                    sex: e.sex
                };
            });
            //q = q.orderBy("it.person_name"); //支持
            q = q.skip(20).take(10).toArray(function (objs) {
                var a = result
                var html = '';
                for (var i = 0; i < objs.length; i++) {
                    var e = objs[i];
                    html += '<tr>';
                    html += '<td>' + e.com_person_id + '</td>';
                    html += '<td>' + e.person_no + '</td>';
                    html += '<td>' + e.person_name + '</td>';
                    html += '<td>' + e.sex + '</td>';
                    html += '<td><a key="' + e.com_person_id + '" class="btn op-edit">编辑</a></td>';
                    html += '</tr>';
                }
                $('#result tbody').html(html);
            });

            $('a.op-edit').live('click', function () {
                var me = $(this);
                db.com_person.where(function (e) {
                    return e.com_person_id == this.key;
                }, { key: $data.parseGuid(me.attr('key')) }).toArray(function (e) {
                    var e = e[0];
                    now = e;
                    $('input[name="person_name"]').val(e.person_name);
                    var a = 1;
                });
            });

            $('#submit').click(function () {
                db.attach(now);
                now.person_name = $('input[name="person_name"]').val();
                db.saveChanges(function (e) {
                    //alert(arguments);
                });
                return false;
            });
        });
    </script>
</asp:Content>
