jQuery(function ($) {
    var
    cookieName = 'glimpsePolicy',
    $glimpse = $('#glimpseControl'),
    glimpseCookie = dnn.dom.getCookie(cookieName);
    if (glimpseCookie) {
        $glimpse.text('Disable Glimpse');
    }
    else {
        $glimpse.text('Enable Glimpse');
    };

    $glimpse.click(function (e) {
        e.preventDefault();
        if (glimpseCookie) {
            //document.cookie = 'glimpsePolicy=; path=/; expires=Sat, 01 Jan 2050 12:00:00 GMT;';
            dnn.dom.deleteCookie(cookieName, '/');
        }
        else {
            dnn.dom.setCookie(cookieName, 'On', 30, '/')
        };
        window.location.reload();
    });
});