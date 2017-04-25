
(function ($) {
    //
    // plugin definition
    //
    $.fn.clock = function (options) {
        debug(this);
        // build main options before element iteration
        var opts = $.extend({}, $.fn.clock.defaults, options);
        // iterate and reformat each matched element
        return this.each(function () {
            $this = $(this);
            // build element specific options
            var o = $.metadata ? $.extend({}, opts, $this.metadata()) : opts;
            // update element styles
            
            $this.css({
                backgroundColor: o.background,
                color: o.foreground
            });

            //Call the WorldTime function           
            var newFn = new function () {
                var sTime = worldClock(o.timeZone, o.region, o.city);
                $this.html(sTime);
            };
            //var timer = setInterval(newFn, 1000);

            var markup = $this.html();
            // call our format function
            //markup = $.fn.clock.format(markup);
            $this.html(markup);

        });
    };
    //
    // private function for debugging
    //
    function debug($obj) {
        if (window.console && window.console.log)
            window.console.log('hilight selection count: ' + $obj.size());
    };
    //
    // define and expose our format function
    //
    $.fn.clock.format = function (txt) {
        return '<strong>' + txt + '</strong>';
    };
    //
    // plugin defaults
    //
    $.fn.clock.defaults = {
        foreground: '#a4a4a4',
        background: '#efefef',
        timeZone: 0
    };

    //
    //public data TimeZones
    //
    $.fn.clock.TimeZones = {
        LocalTime: 0,
        LondonGMT: 0,
        Rome: 1,
        Bangkok: 7,
        HongKong: 8,
        Tokyo: 9,
        Sydney: 10,
        Fiji: 12,
        Hawaii: -10,
        SanFrancisco: -8,
        LosAngeles: -8,
        Houston: -6,
        Miami: -5
    };

  
})(jQuery);


