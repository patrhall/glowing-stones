var Products = {};

(function (Products) {
    'use strict';

    Products.initPage = function () {
        $(document).on('click', '.product-type', Products.showCategory);
        $(document).on('click', '.buy', Products.addToShop);

        if ($('#cart')) {
            Products.getCart();
        }

        var isMobile = window.matchMedia('only screen and (max-width: 760px)');

        if (!isMobile.matches) {
            $('.products').addClass('in');
        }
    };
    Products.showCategory = function () {
        var link = $(this).find('.product-text').attr('href');
        location.href = link;
    };
    Products.addToShop = function () {
        var id = $(this).data('id'),
            exist = localStorage.getItem('shop-' + id);
        if (!exist) {
            localStorage.setItem('shop-' + id, id);
        }
    };
    Products.getCart = function () {
        var url = $('#cart').data('url');
        const items = { ...localStorage };
        $.get(url, items, function (data, status) {
            var test = data;
        });
    };
    $(document).ready(function () {
        Products.initPage();
    });

}(Products));