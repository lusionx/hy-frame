

!function () {
    var bis = function () {
        $(function () {
            $('#' + NavConfig.id).on('change', ':checkbox', function () {
                var key = NavConfig.key;
                var ms = (function () {
                    var rows = $('#' + NavConfig.id + ' tbody tr').map(function () {
                        var td = $('td[title]', this);

                        var roles = $(':checked', this).map(function () {
                            return $(this).parent().attr('title');
                        }).get();

                        return {
                            Url: td.text(),
                            Title: td.attr('title'),
                            Roles: roles
                        };

                    }).get();
                    return JSON.stringify(rows);
                })();
                $('#' + NavConfig.id).data(key, ms);
            });
            $('#' + NavConfig.id + ' :checkbox').eq(0).trigger('change');
        });
    };

    var check = function () {
        return $ ? true : false;
    };

    var wait = function () {
        setTimeout(function () {
            if (check()) {
                bis();
            } else {
                wait();
            }
        }, 1000);
    }
    wait();

}(window);


