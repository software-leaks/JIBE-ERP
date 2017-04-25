(function ($) {
    //
    // plugin definition
    //

    var lastExecutor_WebServiceProxy;
    var objthis
    var evt

    $.fn.Info = function (options) {
        return this.each(function () {
            var vid_ = $(this).attr("vessel_id");

            $(this).bind("mouseenter", { vid: vid_ }, $.fn.Info.fnGetInfo);

        });
    };

    $.fn.Info.fnGetInfo = function (event) {
        try {
            var vid_ = event.data.vid;

            evt = event;
            objthis = this;

            if ($('#dvInfoBox').length == 0) {
                var oDv = document.createElement("div");
                oDv.id = "dvInfoBox";
                document.body.appendChild(oDv);
            }
            var dvInfoBox = $('#dvInfoBox');


            if (vid_ > 0) {
                if (lastExecutor_WebServiceProxy != null)
                    lastExecutor_WebServiceProxy.abort();
                
                var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_Piracy_Alarm_Change_Log', false, { "Vessel_ID": vid_ }, $.fn.Info.Get_Info_onSuccess, $.fn.Info.Get_Info_onFail);

                lastExecutor_WebServiceProxy = service.get_executor();
            }


            $(dvInfoBox).show();

            var pos = $(this).offset();
            var width = $(this).width();

            $(dvInfoBox).css({ "left": (pos.left + width) + "px", "top": pos.top + "px", "width": 600, "position": "absolute", "background-color": "transparent" });
            $(dvInfoBox).bind('mouseleave', function () { $(this).hide(); })


        }
        catch (ex) { }
    };

    $.fn.Info.Get_Info_onSuccess = function (retval) {
        try {            
            js_ShowToolTip(retval, evt, objthis)
        }
        catch (ex) { }
    };
    $.fn.Info.Get_Info_onFail = function (err_) {
        alert(err_._message);
    };


})(jQuery);

$(document).ready(function () {
    $('.InfoBox').Info();
});

