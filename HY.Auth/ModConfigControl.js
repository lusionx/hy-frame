

!function () {
    var bis = function () {/*
        $(function () {
            $('#' + ModConfig.id).on('change', ':checkbox', function () {
                var key = ModConfig.key;
                var ms = (function () {
                    var rows = $('#' + ModConfig.id + ' tbody tr').map(function () {
                        var td = $('td[title]', this);
                        var roles = $(':checked', this).map(function () {
                            return $(this).parent().attr('title');
                        }).get();

                        return {
                            Url: td.attr('title'),
                            Title: td.text(),
                            Action: $('td:eq(1)', this).text(),
                            Roles: roles
                        };

                    }).get();
                    return JSON.stringify(rows);
                })();
                $('#' + ModConfig.id).data(key, ms);
            });
            $('#' + ModConfig.id + ' :checkbox').eq(0).trigger('change');
        });*/
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


