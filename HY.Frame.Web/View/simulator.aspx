<%@ Page Title="" Language="C#" MasterPageFile="~/BS.Master" AutoEventWireup="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="span12">
            <table id="tbl" class="table table-condensed table-hover table-bordered table-striped">
                <thead>
                    <tr>
                        <th class="">部位
                        </th>
                        <th>原始
                        </th>
                        <th>宝石,附魔
                        </th>
                        <th>重铸<a href="#" id="op-sum" class="btn-small btn btn-primary">计算</a>
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
                <tfoot>
                </tfoot>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="span6">
            <p id="op-rsl-op"></p>
        </div>
        <div class="span6">
            <p id="op-rsl-val"></p>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
    <script>
        var keys = ['命中', '精准', '暴击', '精通', '急速'];
        var initDps = [
        { key: '头部', val: { item: 85984, value: [{ '暴击': 755 }, { '急速': 575 }], ext: [{ '命中': 160 }] } },//25N
        { key: '颈部', val: { item: 86177, value: [{ '暴击': 518 }, { '精通': 345 }] } },
        { key: '肩部', val: { item: 89345, value: [{ '命中': 418 }, { '暴击': 592 }] } },
        { key: '背部', val: { item: 86154, value: [{ '命中': 421 }, { '急速': 474 }] } },
        { key: '胸甲', val: { item: 85332, value: [{ '暴击': 795 }, { '急速': 635 }] } },
        { key: '手腕', val: { item: 86230, value: [{ '命中': 372 }, { '精通': 558 }] } },//25N

        { key: '手', val: { item: 85331, value: [{ '暴击': 576 }, { '精准': 624 }] } },
        { key: '腰部', val: { item: 89826, value: [{ '暴击': 437 }, { '精准': 578 }], ext: [{ '精准': 120 }, { '命中': 160 }] } },
        { key: '腿部', val: { item: 86670, value: [{ '精准': 594 }, { '精通': 735 }] } },
        { key: '脚', val: { item: 85925, value: [{ '暴击': 569 }, { '精通': 452 }], ext: [{ '命中': 175 }] } },

        { key: '手指1', val: { item: 86162, value: [{ '急速': 512 }, { '精准': 356 }] } },
        { key: '手指2', val: { item: 86880, value: [{ '命中': 343 }, { '暴击': 437 }] } },
        { key: '饰品1', val: { item: 0, value: [{ '': '' }, { '': '' }] } },
        { key: '饰品2', val: { item: 0, value: [{ '': '' }, { '': '' }] } },

        { key: '武器1', val: { item: 86140, value: [{ '暴击': 841 }, { '精准': 634 }] } },
        { key: '武器2', val: { item: 86799, value: [{ '暴击': 745 }, { '精准': 561 }] } }];
        $(function () {
            var slthtml = '<select class="span2 op-s">' + $.map(keys, function (e) {
                return '<option value="' + e + '">' + e + '</option>';
            }).join('') + '</select>';
            var converKeyVal = function (obj) {
                var jstr = ''
                for (var e in obj) {
                    jstr += '{ key :"' + e + '",val:"' + obj[e] + '"}';
                    break;
                }
                return eval('(' + jstr + ')');
            };
            var trs = $.map(initDps, function (e) {
                if (e.val.item == 0) {
                    return null;
                }
                //部位
                var tr = '<tr>'
                tr += '<td>'
                tr += '<a href="http://db.178.com/wow/cn/item/' + e.val.item + '.html" target="_blank">' + e.key + '</a>';
                tr += '</td>'
                //属性
                tr += '<td>'
                var vs = e.val.value;
                var v = converKeyVal(vs[0]);
                tr += slthtml;
                tr += '<input class="span1 op-v" disabled name="' + v.key + '" type="text" value="' + v.val + '" /><br/>';
                v = converKeyVal(vs[1]);
                tr += slthtml;
                tr += '<input class="span1 op-v" disabled name="' + v.key + '"type="text" value="' + v.val + '" />';
                tr += '</td>';
                //扩展
                tr += '<td>'
                if (e.val.ext) {
                    for (var i = 0; i < e.val.ext.length; i++) {
                        v = converKeyVal(e.val.ext[i]);
                        tr += '' + v.key + '<input disabled class="span1 op-v-ext" name="' + v.key + '"type="text" value="' + v.val + '" />';
                        if (i <= e.val.ext.length - 1) {
                            tr += '<br/>';
                        }
                    }
                }
                tr += '</td>'
                //重铸
                tr += '<td><p class="op-tip"/><p class="op-3v"/>'
                tr += '</td>'
                tr += '</tr>';
                return tr;
            });
            $('#tbl tbody').html(trs.join(''));
            $('.op-v').map(function (i, e) {
                $(e).prev().val(e.name);
            });
            $('.op-s').change(function () {
                var me = $(this);
                var td = me.parent().next().next();
                var from = me.next()[0].name;
                var to = me.val();
                me.val(from);
                if ($('.op-s', me.parent()).first().val() == to || $('op-s', me.parent()).last().val() == to) {
                    return;
                }
                var html = from + '->' + to + '<a class="btn op-cancel" href="#">还原</a>';
                $('.op-tip', td).html(html);
                //计算 重铸后的
                $('.op-3v', td).empty();
                $('.op-3v', td).append($('.op-v', me.parent()).clone());
                var change = $('input[name="' + from + '"]', td);
                var v = parseInt(change.val());
                change.val(v * 0.6 + '');
                change = change.clone();
                change.val(v * 0.4 + '');
                change[0].name = to;
                $('.op-3v', td).append(change);
                $('.op-3v input', td).map(function (i, e) {
                    $(e).before('<br/>' + e.name);
                });
                $('.op-3v br', td).eq(0).remove();
            });

            $('a.op-cancel').live('click', function () {
                var td = $(this).parent().parent();
                $('.op-tip', td).empty();
                $('.op-3v', td).empty();
            });

            var allsum = function () {
                var jstr = '{';
                jstr += $.map(keys, function (e) {
                    return '"' + e + '":0';
                }).join(',');
                jstr += '}';
                var obj = eval('(' + jstr + ')');
                var ops = $.map($('.op-3v'), function (e, k) {
                    var puts = $('.op-v', e);
                    var tr = $(e).parent().parent();
                    if (puts.length == 0) {
                        puts = tr.find('.op-s').next();
                    }
                    for (var i = 0; i < puts.length; i++) {
                        obj[puts[i].name] += parseInt(puts[i].value);
                    }
                    puts = $('.op-v-ext', tr);
                    for (var i = 0; i < puts.length; i++) {
                        obj[puts[i].name] += parseInt(puts[i].value);
                    }
                    if ($('.op-tip a', tr).length) {
                        return $('td', tr).first().text() + ':' + $('.op-tip', tr).text().substr(0, 6);
                    } else {
                        return null;
                    }
                });
                $('#op-rsl-val').html($.map(obj, function (val, key) {
                    return key + ":" + val;
                }).join('<br/>'));

                $('#op-rsl-op').html(ops.join('<br/>'));
                return false;
            };
            $('#op-sum').click(allsum);
        });
    </script>
</asp:Content>
