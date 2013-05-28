/// <reference path="../Scripts/jquery-1.8.3.js" />
/// <reference path="../Scripts/jaydata-vsdoc.js" />

$(function () {
    var db = HY.Frame.Bis.context;
    var q = db.com_person;
    q.where(function (e) {
        return e.sex == '男';
    }).map(function (e) {
        return {
            person_no: e.person_no,
            person_name: e.person_name
        };
    }).orderBy("it.person_name").skip(20).take(10).forEach(function (e) {
        $('#result').append(e.person_name);
    });
    
});




(function () {
    if (!window.jQuery) {
        var ga = document.createElement('script');
        ga.type = 'text/javascript';
        ga.async = true;
        //ga.src = 'http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js';
        (document.getElementsByTagName('head')[0] || document.getElementsByTagName('body')[0]).appendChild(ga);
    }
})();
