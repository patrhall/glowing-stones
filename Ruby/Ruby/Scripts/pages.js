var Pages = {};

(function (Pages) {
    'use strict';

    Pages.initPage = function () {
        $(document).on('click', '.delete', Pages.delete);
    };
    Pages.delete = function () {
        if (confirm('Are you sure you want to delete this page (and child pages)?')===true) {
            var url = $('.delete').data('href');

            $.ajax({
                type: 'POST',
                url: url
            }).done(function() {
                location.reload();
            });
        }
    };
    $(document).ready(function () {
        Pages.initPage();
    });

}(Pages));