/*global jQuery, dnn */
jQuery(function ($) {
    'use strict';
    var cookieUtility = dnn.dom,
        cookieName = 'glimpsePolicy',
        $glimpse = $('#glimpseControl'),
        glimpseCookie = cookieUtility.getCookie(cookieName);
    if (glimpseCookie) {
        $glimpse.text('Disable Glimpse');
    } else {
        $glimpse.text('Enable Glimpse');
    }

    $glimpse.click(function (e) {
        e.preventDefault();
        if (glimpseCookie) {
            //document.cookie = 'glimpsePolicy=; path=/; expires=Sat, 01 Jan 2050 12:00:00 GMT;';
            cookieUtility.deleteCookie(cookieName, '/');
        } else {
            cookieUtility.setCookie(cookieName, 'On', 30, '/');
        }
        window.location.reload();
    });
});