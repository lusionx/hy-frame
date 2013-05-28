/*
扩展
 */

!function ($) {
	$.validator.addMethod("lettersonly", function (value, element) {
		return this.optional(element) || /^[a-z]+$/i.test(value);
	}, "只允许字母");
}(window.jQuery);