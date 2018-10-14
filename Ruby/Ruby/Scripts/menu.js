var Menu = {};

(function (Menu) {
    'use strict';

    Menu.initPage = function () {
        $(document).on('click', '.curr', Menu.setTicker);
    };
    Menu.setTicker = function () {
        var url = $('.sessCurr').data('url'),
            ticker = $(this).data('curr');

        $.ajax({
            type: 'POST',
            url: url + '/' + ticker
        }).done(function (){
             location.reload();
        });
    };
    $(document).ready(function () {
        Menu.initPage();
    });

}(Menu));