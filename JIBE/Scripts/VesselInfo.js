var __app_name = location.pathname.split('/')[1];



(function ($) {
    //
    // plugin definition
    //
    $.fn.InfoBox = function (options) {
        return this.each(function () {
            var vid_ = $(this).attr("vid");
            var vname_ = $(this).attr("vname");
            var vcode_ = $(this).text();
            
            //decorate the link on which event happens
            $(this).css({ "color": "Blue", "text-decoration": "underline", "cursor": "pointer" });
            $(this).bind("click", { vid: vid_, vname: vname_, vcode: vcode_ }, $.fn.InfoBox.fnGetVesselInfo);
            
        });
    };

    $.fn.InfoBox.fnGetVesselInfo = function (event) {
        try {
            var vid = event.data.vid;
            var vname = event.data.vname;
            var vcode = event.data.vcode;

            if ($('#dvInfoBox').length == 0) {
                var oDv = document.createElement("div");
                oDv.id = "dvInfoBox";
                document.body.appendChild(oDv);
            }
            var dvInfoBox = $('#dvInfoBox');

            if (vid)
                $(dvInfoBox).load("/"+__app_name+"/infrastructure/vesselinfo.aspx?vid=" + vid + "&rnd=" + Math.random() + ' #dvVesselInfo');
            else if (vcode)
                $(dvInfoBox).load("/"+__app_name+"/infrastructure/vesselinfo.aspx?vcode=" + vcode + "&rnd=" + Math.random() + ' #dvVesselInfo');

            $(dvInfoBox).show();

            var pos = $(this).offset();
            var width = $(this).width();

            $(dvInfoBox).css({ "left": (pos.left + width) + "px", "top": pos.top + "px", "width": 600, "position": "absolute", "background-color": "transparent" });

            $(dvInfoBox).bind('mouseleave', function () { $(this).hide(); })

            //$(dvInfoBox).bind("click", $.fn.InfoBox.fnSelectText);

            
        }
        catch (ex) { }
    };

    $.fn.InfoBox.fnSelectText = function (event) {
        try {
            var doc = document;
            if (doc.body.createTextRange) {
                var range = document.body.createTextRange();
                range.moveToElementText(this);
                range.select();
            } else if (window.getSelection) {
                var selection = window.getSelection();
                var range = document.createRange();
                range.selectNodeContents(this);
                selection.removeAllRanges();
                selection.addRange(range);
            }
        }
        catch (ex) { }
    };
})(jQuery);

$(document).ready(function () {
    $('.vesselinfo').InfoBox();
});


