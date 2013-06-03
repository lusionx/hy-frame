

!function () {
    var bis = function () {
        $(function () {
            //alert($('div').size());
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


