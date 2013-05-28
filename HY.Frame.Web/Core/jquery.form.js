
/*
收集表单数据
$().gform() -> object 以表单的name为属性名称
$().sform(obj) -> null 查找和obj属性等同name值得表单,并赋值
$.pform(url,obj) -> null post obj数据 到一个新打开的页面
 */
(function ($) {
    $.fn.extend({
        gform: function () {
            var vals = [];
            $(':text[name],:hidden[name],:password[name],select[name]', this).filter(function () {
                return this.name.length > 0;
            }).each(function () {
                vals.push({ key: this.name, val: this.value });
            });
            $(':checked[name]', this).filter(function () {
                return this.name.length > 0;
            }).each(function () {
                vals.push({ key: this.name, val: this.value });
            });

            $('textarea[name]', this).filter(function () {
                return this.name.length > 0;
            }).each(function () {
                vals.push({ key: this.name, val: this.value });
            });


            var vals2 = {};
            $.each(vals, function (i, e) {
                if (vals2[e.key] == undefined) {
                    vals2[e.key] = [];
                }
                vals2[e.key].push(e.val);
            });
            for (var n in vals2) {
                if (vals2[n].length == 1) {
                    vals2[n] = vals2[n][0];
                }
            }
            return vals2;
        },
        sform: function (data) {
            $(':text[name],:hidden[name]', this).filter(function () {
                return this.name.length > 0;
            }).each(function () {
                if (data[this.name] != undefined) {
                    $(this).val(data[this.name]);
                }
            });
            $(':radio[name]', this).filter(function () {
                return this.name.length > 0;
            }).each(function () {
                if (data[this.name] != undefined && $(this).val() == data[this.name]) {
                    $(this).attr('checked', 'checked');
                }
            });

            $('textarea[name]', this).filter(function () {
                return this.name.length > 0;
            }).each(function () {
                if (data[this.name] != undefined) {
                    $(this).val(data[this.name]);
                }
            });

            $('select[name]', this).filter(function () {
                return this.name.length > 0;
            }).each(function () {
                if (data[this.name] != undefined) {
                    $(this).val(data[this.name]);
                }
            });

        }
    });
})(jQuery);

(function ($) {
    $.extend({
        formP: function (url, obj) {
            var ss = [];
            ss.push('<form target="_blank" action="' + url + '" method="post">');
            for (var name in obj) {
                ss.push('<input type="hidden" name="' + name + '" value=\'' + obj[name] + '\' />');
            }
            ss.push('</form>');
            var f = $(ss.join(''));
            $('body').append(f);
            f.submit();
            f.remove();
        }
    });
})(jQuery);
