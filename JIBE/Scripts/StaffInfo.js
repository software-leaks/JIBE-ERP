
var __app_name = location.pathname.split('/')[1];
 
    var lastExecutor_WebServiceProxy;
    var objthis;
    var evt;

    function StaffInfo() {
        var list = document.getElementsByClassName("staffInfo");
        var itm;
        var myEl;
//         alert(list);
        for (var i = 0; i < list.length; i++) {
            //            alert(list[i]);
            //            txt = list.item(i).innerHTML;
            if (list.item(i) != null && list.item(i) != undefined) {
                //                $(this).css({ "color": "Purple", "text-decoration": "underline", "cursor": "pointer" });
                myEl = list.item(i);
                //                myEl.addEventListener('mouseover', test('HI'), false);
//                alert(list[i].innerHTML);
                myEl.onmouseover = createHandlerFor(list[i]);
            }
        }
    }

    function createHandlerFor(ele) {
        return function (event) {
            var scode_ = ele.innerHTML;

            evt = event;
            objthis = this;

//            if (document.getElementById('dvSailingInfoBox') == undefined) {
//                var oDv = document.createElement("div");
//                oDv.id = "dvSailingInfoBox";
//                document.body.appendChild(oDv);
//            }
//            var dvSailingInfoBox = document.getElementById('dvSailingInfoBox');
//            alert(ele.innerHTML);

            if (scode_ > 0) {
                if (lastExecutor_WebServiceProxy != null)
                    lastExecutor_WebServiceProxy.abort();
                var DateFormat = $('[id$=hdnDateFromatMasterPage]').val();
                var service = Sys.Net.WebServiceProxy.invoke("/" + __app_name + '/JibeWebService.asmx', 'Get_StaffInfo', false, { "StaffCode": scode_, "DateFormat": DateFormat.toString() }, Get_StaffInfo_onSuccess, Get_StaffInfo_onFail);                
                lastExecutor_WebServiceProxy = service.get_executor();
            }

//            dvSailingInfoBox.show();
        };
    }

    function Get_StaffInfo_onSuccess(retval) {        
        try {
            js_ShowToolTip(retval, evt, objthis)
        }
        catch (ex) { }
        }

        function Get_StaffInfo_onFail(err_) {
//         alert(err_._message);
    }      