(function ($) {
    //
    // plugin definition
    //

    $.fn.wizard = function (options) {
        debug(this);

        var _listPages = [];
        var _listSteps = [];

        var _currentPageIndex = 0;
        $(this).attr('current-page-index', _currentPageIndex);

        var opts = $.extend({}, $.fn.wizard.defaults, options);
        this.find('#steps').selectable({ selecting: opts.selecting, selected: opts.selected });

        // get pages and steps
        _listPages = this.find('.wizard-pages').find('.wizard-page')
        _listSteps = this.find('.wizard-steps').find('li')

        $('#steps li').each(function () {
            var pg = $(this).find('a').attr('href');

            $(this).click(function () {
                navigatePage($(this).index());
            });
        });

        function navigatePage(pageindex) {
            if (pageindex < _listPages.length && pageindex >= 0) {
                var pg = _listPages[pageindex];
                $('.wizard-page').hide();
                $(pg).show('slide', 500);

                var stp = _listSteps[pageindex];
                $('.wizard-steps #steps li').removeClass('ui-selected');
                $(stp).addClass('ui-selected');

                _currentPageIndex = pageindex;
            }
            //show hide buttons
            if (_currentPageIndex == 0)
                $('#_wizardButtonBack').attr("disabled", true);
            else
                $('#_wizardButtonBack').attr("disabled", false);

            if (_currentPageIndex == _listPages.length - 1)
                $('#_wizardButtonNext').attr("disabled", true);
            else
                $('#_wizardButtonNext').attr("disabled", false);
        }

        //create new control box;
        //-------------------------------------------            
        // Remove already added control box;
        if (document.getElementById('_wizardControls'))
            this.removeChild(document.getElementById('_wizardControls'));

        var _wizardControls = document.createElement('div');
        _wizardControls.id = '_wizardControls';
        $(_wizardControls).attr({ "class": "wizard-controls" });


        var btnBack = document.createElement("input");
        $(btnBack).attr({ "id": "_wizardButtonBack", "type": "button", "value": "< Back" });
        $(btnBack).click(function () { navigatePage(_currentPageIndex - 1); });

        var btnNext = document.createElement("input");
        $(btnNext).attr({ "id": "_wizardButtonNext", "type": "button", "value": "Next >" });
        $(btnNext).click(function () { navigatePage(_currentPageIndex + 1); });

        $(_wizardControls).append(btnBack);
        $(_wizardControls).append(btnNext);
        $(this).append(_wizardControls);

        //Buttons
        $("input:button", "#_wizardControls").button();
        //$("a", ".wizard-controls").click(function () { return false; });

        navigatePage(_currentPageIndex);
    };

    //
    // plugin defaults
    //
    $.fn.wizard.defaults = {
        selecting: function (event, ui) { },
        selected: function (event, ui) { }
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
    $.fn.wizard.format = function (txt) {
        return '<strong>' + txt + '</strong>';
    };
})(jQuery);


