var Products = {};

(function (Products) {
    'use strict';

    Products.initPage = function () {
        $(document).on('click', '#JobWithDestination', Products.showLocations);

        var isMobile = window.matchMedia('only screen and (max-width: 760px)');

        if (!isMobile.matches) {
            $('.products').addClass('in');
        };
    };
    Products.showLocations = function () {
        $('.AACMultiselectBox [name*="JobTypeLocation"]').parents('li').toggle();
    };

    $(document).ready(function () {
        Products.initPage();
    });

}(Products));