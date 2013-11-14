/*global jQuery, dnn */

/* code should execute at bottom of doc to ensure dom has loaded */
(function ($) {
    'use strict';
    var cookieUtility = dnn.dom,
        cookieName = 'glimpsePolicy',
        $glimpse = $('#enableGlimpse'),
        $update = $('a[id$=cmdUpdate]'),
        enabled = cookieUtility.getCookie(cookieName) == 'On';

    $glimpse.prop('checked', enabled);

    $glimpse.next('span').click(function (e) {
        e.preventDefault();
        $glimpse.prop('checked', !($glimpse.prop('checked')));
        enabled = !enabled;
    });

    $update.click(function (e) {
        e.preventDefault();
        if (enabled) { cookieUtility.setCookie(cookieName, 'On', 30, '/'); }
        else { cookieUtility.deleteCookie(cookieName, '/'); }
        window.location.reload();
    });
})(jQuery);