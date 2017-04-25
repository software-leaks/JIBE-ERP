var __app_name = location.pathname.split('/')[1];

var isModalOpen = 0;
var ModalPopUpID = "";

(function ($) {
    $.fn.center = function () { this.css("position", "absolute"); this.css("top", ($(window).height() - this.height()) / 2 + $(window).scrollTop() + "px"); this.css("left", ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + "px"); return this; }
})(jQuery);

function showModal(dvPopUpID, isDraggable, callbackFunction, callback_Help_Function) {
    try {
        if (dvPopUpID) {
            var dvPopUp = document.getElementById(dvPopUpID);

            $(dvPopUp).show();
            $(dvPopUp).css({ 'border': '1px solid #aabbee', 'background-color': 'white' });

            if (!document.getElementById('overlay')) {
                var dvOverlay = document.createElement('div');
                dvOverlay.id = 'overlay';
                document.body.appendChild(dvOverlay);
            }

            //Remove the already added header (if exists)
            //-------------------------------------------
            if (document.getElementById(dvPopUpID + '_dvModalPopupHeader'))
                dvPopUp.removeChild(document.getElementById(dvPopUpID + '_dvModalPopupHeader'));

            //create new header
            //-------------------------------------------            
            var dvModalPopupHeader = document.createElement('div');
            dvModalPopupHeader.id = dvPopUpID + '_dvModalPopupHeader';

            isModalOpen = 1;
            ModalPopUpID = dvPopUpID;
            var title = $(dvPopUp).attr('title') == undefined ? '' : $(dvPopUp).attr('title');

            dvModalPopupHeader.innerHTML = "<span id='" + dvPopUpID + '_dvModalPopupTitle' + "' >" + title + "</span>";
            $(dvModalPopupHeader).css({ 'width': '100%', 'height': 22, backgroundColor: "transparent", color: 'Black', 'background-color': '#A9D0F5', 'font-weight': 'Bold', 'cursor': 'move' });

            $(dvPopUp).attr('title', '');

            var dvModalPopupControlBox = document.createElement('div');
            $(dvModalPopupControlBox).css({ 'height': 20, backgroundColor: "transparent", 'right': 2, 'top': 2, 'position': 'absolute' });

            var dvModalPopupCloseButton = document.createElement('div');
            dvModalPopupCloseButton.id = dvPopUpID + '_dvModalPopupCloseButton';
            dvModalPopupCloseButton.innerHTML = '<img id="closePopupbutton" src="/' + __app_name + '/images/close.png" style="cursor:pointer;" alt="Press ESC to Close" id="imgbtnPopupClose">';
            $(dvModalPopupCloseButton).css({ 'height': 20, backgroundColor: "transparent", 'top': 0, 'float': 'right' });
            $(dvModalPopupCloseButton).click(function () { $('#overlay').hide(); $(dvPopUp).hide(); try { setTimeout(callbackFunction, 1); } catch (ex) { } });
            dvModalPopupControlBox.appendChild(dvModalPopupCloseButton);

            if (callback_Help_Function) {
                var dvModalPopupHelpButton = document.createElement('div');
                dvModalPopupHelpButton.id = dvPopUpID + '_dvModalPopupHelpButton';
                dvModalPopupHelpButton.innerHTML = '<img  src="/' + __app_name + '/images/help16.png" style="cursor:pointer;">';
                $(dvModalPopupHelpButton).css({ 'height': 20, backgroundColor: "transparent", 'padding-right': 5, 'top': 0, 'float': 'right' });
                $(dvModalPopupHelpButton).click(function () { try { setTimeout(callback_Help_Function, 1); } catch (ex) { } });
                dvModalPopupControlBox.appendChild(dvModalPopupHelpButton);
            }

            dvModalPopupHeader.appendChild(dvModalPopupControlBox);
            dvPopUp.insertBefore(dvModalPopupHeader, dvPopUp.firstChild);

            //$(dvModalPopupHeader).bind('mousedown', function () { $(dvPopUp.firstChild).next().hide(); });
            //$(dvModalPopupHeader).bind('mouseup', function () { $(dvPopUp.firstChild).next().show(); });

            var maskHeight = $(document).height();
            var maskWidth = $(document).width();
            var h = dvPopUp.clientHeight;
            var w = dvPopUp.clientWidth;

            var t = ($(window).height() / 2 - h / 2) / 2;
            var l = $(window).width() / 2 - w / 2;

            //-- Kiran: Changes done to fix the window resize issue --//

//            $('#overlay').css({ 'width': maskWidth, 'height': maskHeight, backgroundColor: "black", 'position': "absolute", 'top': 0, 'left': 0, 'z-index': 999 });

//            $(dvPopUp).css({ 'top': t, 'left': l, 'z-index': 1000, 'position': "absolute", 'border': '4px solid #A9D0F5', 'background-color': 'white', 'padding': 0 });


            $('#overlay').css({ backgroundColor: "black", 'position': "fixed", 'top': 0, 'left': 0, 'right': 0, 'bottom': 0, 'z-index': 999 });

            $(dvPopUp).css({ 'z-index': 1000, 'position': "absolute", 'border': '4px solid #A9D0F5', 'background-color': 'white', 'padding': '0', 'top': '15%', 'left': '30%' });


            //--  code changes by kiran lad end --//


            isDraggable = typeof (isDraggable) == 'undefined' ? true : isDraggable;

            $('#overlay').fadeTo("fast", 0.5);
            //$(dvPopUp).center();
            moveToCenter(dvPopUp);

            if (isDraggable)
                $(dvPopUp).draggable();
        }
    }
    catch (e) { }

}
function hideModal(dvPopUpID) {
    if (dvPopUpID) {
        $('#overlay').hide();
        $('#' + dvPopUpID).hide();

    }
    return true;
}


//--  code changes by kiran lad start --//
function moveToCenter(obj) {
    if (obj) {
        var topY = ($(window).height() - $(obj).height()) / 2 + $(window).scrollTop();
        if (topY < 0)
            topY = 5;


        $(obj).css("position", "absolute"); $(obj).css("top", topY + "px"); $(obj).css("left", ($(window).width() - $(obj).width()) / 2 + $(window).scrollLeft() + "px");
        //$(obj).css("position", "absolute");

    }
    return true;
}
//--  code changes by kiran lad end --//


//jQuery.fn.center = function () { this.css("position", "absolute"); this.css("top", ($(window).height() - this.height()) / 2 + $(window).scrollTop() + "px"); this.css("left", ($(window).width() - this.width()) / 2 + $(window).scrollLeft() + "px"); return this; }


$(document).keydown(function (e) {
    // ESCAPE key pressed 
    try {
        if (e.keyCode == 27) {
            if (isModalOpen == 1) {
                hideModal(ModalPopUpID);
            }
        }
    }
    catch (ex) { }
});


//--------------------------------------------//
//          NORMAL POPUP
//-------------------------------------------//

function showPopup(dvPopUpID) {
    if (dvPopUpID) {

        var dvPopUp = document.getElementById(dvPopUpID);

        $(dvPopUp).show();
        $(dvPopUp).css({ 'border': '1px solid #aabbee', 'background-color': 'white' });


        if (!document.getElementById('dvModalPopupHeader')) {
            var dvModalPopupHeader = document.createElement('div');
            dvModalPopupHeader.id = 'dvModalPopupHeader';
            dvModalPopupHeader.innerHTML = $(dvPopUp).attr('title') == undefined ? '' : $(dvPopUp).attr('title');
            $(dvModalPopupHeader).css({ 'width': '100%', 'height': 20, backgroundColor: "transparent", color: 'Black', 'background-color': '#A9D0F5', 'font-weight': 'Bold' });

            if (!document.getElementById('dvModalPopupCloseButton')) {
                var dvModalPopupCloseButton = document.createElement('div');
                dvModalPopupCloseButton.id = 'dvModalPopupCloseButton';
                dvModalPopupCloseButton.innerHTML = '<img src="/' + __app_name + '/images/close.png">';

                $(dvModalPopupCloseButton).css({ 'height': 20, backgroundColor: "transparent", 'right': 2, 'top': 2, 'position': "absolute" });

                $(dvModalPopupCloseButton).click(function () { $('#overlay').hide(); $(dvPopUp).hide(); });
            }
            dvModalPopupHeader.appendChild(dvModalPopupCloseButton);
            dvPopUp.insertBefore(dvModalPopupHeader, dvPopUp.firstChild);

        }

        var maskHeight = $(document).height();
        var maskWidth = $(document).width();
        var h = dvPopUp.clientHeight;
        var w = dvPopUp.clientWidth;

        var t = ($(window).height() / 2 - h / 2) / 2;
        var l = $(window).width() / 2 - w / 2;

        $(dvPopUp).css({ 'top': t, 'left': l, 'z-index': 1000, 'position': "absolute", 'border': '4px solid #A9D0F5', 'background-color': 'white', 'padding': 0 });

        $(dvPopUp).draggable();

    }
}
function hidePopup(dvPopUpID) {
    if (dvPopUpID) {
        $('#' + dvPopUpID).hide();
    }
    return true;
}