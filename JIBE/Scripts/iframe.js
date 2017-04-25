$(document).ready(IframeResize);


function IframeResize() {
    // Set specific variable to represent all iframe tags.
    var iFrames = document.getElementsByTagName('iframe');

    // Resize heights.
    function iResize() {
        // Iterate through all iframes in the page.
        for (var i = 0, j = iFrames.length; i < j; i++) {
            // Set inline style to equal the body height of the iframed content.
            iFrames[i].style.height = iFrames[i].contentWindow.document.body.offsetHeight + 'px';
        }
    }

    // Check if browser is Safari or Opera.
    if ($.browser.safari || $.browser.opera) {
        // Start timer when loaded.
        $('iframe').load(function () {
            setTimeout(iResize, 0);
        }
			);

        // Safari and Opera need a kick-start.
        for (var i = 0, j = iFrames.length; i < j; i++) {
            var iSource = iFrames[i].src;
            iFrames[i].src = '';
            iFrames[i].src = iSource;
        }
    }
    else {
        // For other good browsers.
        $('iframe').load(function () {
            // Set inline style to equal the body height of the iframed content.

            this.style.height = this.contentWindow.document.body.offsetHeight + 'px';

        }
			);
    }


}


function ResizeFromChild(height_,call_) {
    
    if(call_=='1')
        document.getElementById("ctl00_MainContent_mainFrame").style.height = height_+5 + 'px';
  
}

