var __app_name = location.pathname.split('/')[1];

(function ($) {
    //
    // plugin definition
    //
    $.fn.CrewIcon = function (options) {
        return this.each(function () {
            var image_ = $(this).attr("icon");
            $(this).css({ "background-image": "url(../Uploads/CrewImages/" + image_ + ")"});
        });
    };
    
})(jQuery);


