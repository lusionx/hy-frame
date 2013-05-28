/// <reference path="/Scripts/jquery-1.8.3.js" />
/*
 *  $('#div').pager(options)
 *  options
 *      url         string              请求地址
 *      par         object|function     参数
 *      type        string:post|get     请求方式(默认get)
 *      pageSize    Number             页大小(默认20)
 *      totalPrototype string       表示数据数的属性(默认total)
 *  Methods
 *      go      $().pager('go',i)   跳转第i页, i>=1
 *  Events
 *      change  $().on('change',function(event,obj){})  跳转后触发,obj=完整的ajax数据
 *  
 */

!function ($) {

    "use strict"; // jshint ;_;

    var Pager = function (element, options) {
        this.$element = $(element);
        this.options = $.extend({}, $.fn.pager.defaults, options);
    }
    Pager.prototype.Constructor = Pager;

    Pager.prototype.go = function (i) {
        var me = this;
        var opt = me.options;
        var par = $.isFunction(opt.par) ? opt.par() : opt.par;
        $[opt.type](opt.url, par, function (obj) {
            var row = obj[opt.totalPrototype];
            var page = Math.ceil(row / opt.pageSize);
            me.render(page, i);
            me.$element.trigger('change', [obj]);
        }, 'json');
    };

    Pager.prototype.render = function (page, index) {
        this.$element.addClass('pagination');
        var html = '<ul>';
        if (index == 1) {//首页
            html += '<li class="disabled"><a href="#1">&laquo;</a></li>';
        } else {
            html += '<li class=""><a href="#1">&laquo;</a></li>';
        }
        for (var i = 1; i < page + 1; i++) {
            if (i == index) {
                html += '<li class="active"><a href="#' + i + '">' + i + '</a></li>';
            } else {
                html += '<li class=""><a href="#' + i + '">' + i + '</a></li>';
            }
        }
        if (index == page) {//末页
            html += '<li class="disabled"><a href="#' + page + '">&raquo;</a></li>';
        } else {
            html += '<li class=""><a href="#' + page + '">&raquo;</a></li>';
        }
        html += '</ul>';
        this.$element.html(html);
        this.$element.on('click', 'a', this, function (event) {//跳转
            var index = $(this).attr('href').split('#')[1];
            var me = event.data;
            me.go(index);
        });
    }

    $.fn.pager = function (option, p1, p2) {// init {..}, method('xxxx',i,j)
        return this.each(function () {
            var $this = $(this);
            var pager = $this.data('pager');
            //var options = $.extend({}, $.fn.pager.defaults, $this.data(), typeof option == 'object' && option)
            //var options = typeof option == 'object' && option
            if (typeof option == 'object') {// init {..}
                $this.data('pager', (pager = new Pager(this, option)))
            }
            if (option == 'go') {//method('xxx',....)
                pager.go(p1);
            }
        })
    };

    $.fn.pager.defaults = {
        type: 'get',// post or get
        url:'',
        par:{},
        pageSize: 20,
        totalPrototype: 'total'
    }

    $.fn.pager.Constructor = Pager;

}(window.jQuery);