/*global jQuery, dnn */

/* code should execute at bottom of doc to ensure dom has loaded */
(function ($) {
    'use strict';
    var cookieUtility = dnn.dom,
        cookieName = 'glimpsePolicy',
        $glimpse = $('#enableGlimpse'),
        enabled = cookieUtility.getCookie(cookieName) == 'On';

    $glimpse.prop('checked', enabled);

    $glimpse.click(function (e) {
        e.preventDefault();
        if (enabled) {
            //document.cookie = 'glimpsePolicy=; path=/; expires=Sat, 01 Jan 2050 12:00:00 GMT;';
            cookieUtility.deleteCookie(cookieName, '/');
            $glimpse.prop('checked', !(enabled));
        } else {
            cookieUtility.setCookie(cookieName, 'On', 30, '/');
            $glimpse.prop('checked', !(enabled));
        }
        window.location.reload();
    });
})(jQuery);