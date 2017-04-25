var __app_name = location.pathname.split('/')[1];

(function ($) {
    //
    // plugin definition
    //

    var lastExecutor_WebServiceProxy;
    var objthis
    var evt

    $.fn.SailingInfo = function (options) {
        return this.each(function () {
            var cid_ = $(this).attr("crewid");
            //var rid_ = $(this).attr("rankid");            
                                                 
            $(this).attr('rankid', '0');
            var rid_ = $(this).attr("rankid");            
                                    
            //decorate the link on which event happens
            $(this).css({ "color": "Purple", "text-decoration": "underline", "cursor": "pointer" });
            $(this).bind("mouseenter", { crewid: cid_, rankid: rid_}, $.fn.SailingInfo.fnGetSailingInfo);
                       
           

        });
    };

    $.fn.SailingInfo.fnGetSailingInfo = function (event) {
        try {                                
            var crewid_ = event.data.crewid;
            var rankid_ = event.data.rankid;            

            evt = event;
            objthis = this;

            if ($('#dvSailingInfoBox').length == 0) {
                var oDv = document.createElement("div");
                oDv.id = "dvSailingInfoBox";
                document.body.appendChild(oDv);
            }
            var dvSailingInfoBox = $('#dvSailingInfoBox');


            if (crewid_ > 0) {
            //if (crewid_ > 0 && rankid_ > 0) {
                if (lastExecutor_WebServiceProxy != null)
                    lastExecutor_WebServiceProxy.abort();
                  
                  var service = Sys.Net.WebServiceProxy.invoke("/"+__app_name+'/JibeWebService.asmx', 'Get_StaffSailingInfo', false, { "CrewID": crewid_, "RankID": rankid_}, $.fn.SailingInfo.Get_StaffSailingInfo_onSuccess, $.fn.SailingInfo.Get_StaffSailingInfo_onFail);
                  //var service = Sys.Net.WebServiceProxy.invoke('../JibeWebService.asmx', 'Get_StaffSailingInfo', false, { "CrewID": crewid_, "RankID": rankid_ }, $.fn.SailingInfo.Get_StaffSailingInfo_onSuccess, $.fn.SailingInfo.Get_StaffSailingInfo_onFail);

                lastExecutor_WebServiceProxy = service.get_executor();
            }


            $(dvSailingInfoBox).show();

            var pos = $(this).offset();
            var width = $(this).width();

            $(dvSailingInfoBox).css({ "left": (pos.left + width) + "px", "top": pos.top + "px", "width": 600, "position": "absolute", "background-color": "transparent" });
            $(dvSailingInfoBox).bind('mouseleave', function () { $(this).hide(); })
            //$(dvSailingInfoBox).bind("click", $.fn.SailingInfo.fnSelectText);

        }
        catch (ex) { }
    };

    $.fn.SailingInfo.Get_StaffSailingInfo_onSuccess = function (retval) {
        try {
            js_ShowToolTip(retval, evt, objthis)
        }
        catch (ex) { }
    };
    $.fn.SailingInfo.Get_StaffSailingInfo_onFail = function (err_) {
        //alert(err_._message);
    };


})(jQuery);

$(document).ready(function () {
    $('.sailingInfo').SailingInfo();
});


