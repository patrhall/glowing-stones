var Products = {};

(function (Products) {
    'use strict';

    Products.initPage = function () {
        $(document).on('click', '.product-type', Products.showCategory);
        $(document).on('click', '.buy', Products.addToShop);
        $(document).on('click', '.remove', Products.removeFromShop);

        if ($('#cart').length) {
            Products.getCart();
        }

        if ($('.thumb').length) {
            Products.setModal();
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
    Products.removeFromShop = function () {
        var id = $(this).data('id'),
            exist = localStorage.getItem('shop-' + id);
        if (exist) {
            localStorage.removeItem('shop-' + id);
            Products.getCart();
        }
    };
    Products.getCart = function () {
        var url = $('#cart').data('url'),
            list = Object.values(localStorage),
            jsonText = JSON.stringify({ ids: list });

        $.ajax({
            type: 'POST',
            url: url,
            data: jsonText,
            contentType: 'application/json; charset=utf-8'
        }).done(function (html) {
            $('#cart-products').html(html);
        });
    };
    Products.setModal = function () {

        $('.thumb').click(function () {
            var img = $(this);
            $('#modalImage').show();
            $('#img01').attr('src', $(img).data('src'));
            $('#caption').html($(img).attr('alt'));
        });

        // When the user clicks on <span> (x), close the modal
        $('.close').click(function () {
            $('#modalImage').hide();
        });
    };
    $(document).ready(function () {
        Products.initPage();
    });

}(Products));