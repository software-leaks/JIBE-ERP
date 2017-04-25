
	/* ---------------------------------------------------------------------- */
	/*	Setup Worklist Dashboard Data
        
        INDEX:
    ---------------------------------------------------------------------- ----
       - isMobile
       - Reset widgets script
       - setup_JSONData_JobCreationTrend_PieChart
       - setup_JSONData_JobsUpdated_InLast24Hours
       - setup_JSONData_Jobs_Super_Inspection_Last6M
       - setup_JSONData_NCR_Raised_InLast3M
       - setup_JSONData_HighPriority_Jobs_InLast3M
       - setup_JSONData_NCR_Trend_BarChart

       - NOT USED:       
       - setup_JSONData_Super_Inspected_BarChart
	/* ---------------------------------------------------------------------- */
    
    // Document Ready

	$(document).ready( function() {   
    	/* DETECT IF MOBILE AND WIDGET CONFIG  */
		isMobile() 

        setup_JSONData_JobCreationTrend_PieChart();
        setup_JSONData_JobsUpdated_InLast24Hours();
        setup_JSONData_JobsCreated_InLast24Hours();
        setup_JSONData_Jobs_Super_Inspection_Last6M();
        setup_JSONData_NCR_Raised_InLast3M();
        setup_JSONData_HighPriority_Jobs_InLast3M();
        //setup_JSONData_NCR_Trend_BarChart();

        //setup_JSONData_Super_Inspected_BarChart();

        /* reset widget */
		$('#reset-widget').click(function(){
			resetWidget();
			return false;
		});
	}); 

    //START - JARVIS WIDGET SETTINGS -------------------------------------------/
    /* chart colors default */
	var $chrt_border_color = "#efefef";
	var $chrt_grid_color = "#DDD"
	var $chrt_main = "#E24913";			/* red       */
	var $chrt_second = "#4b99cb";		/* blue      */
	var $chrt_third = "#FF9F01";		/* orange    */
	var $chrt_fourth = "#87BA17";		/* green     */
	var $chrt_fifth = "#BD362F";		/* dark red  */
	var $chrt_mono = "#000";

    /* ---------------------------------------------------------------------- */
	/*	isMobile
	/* ---------------------------------------------------------------------- */
	
	/** NOTE: Notice we have seperated funtion calls based on user platform. 
	 		  This significantly cuts down on memory usage and prolongs a healthy 
	 		  user experience. 
	**/
		
	function isMobile() {
		/* so far this is covering most hand held devices */
		var ismobile = (/iphone|ipad|ipod|android|blackberry|mini|windows\sce|palm/i.test(navigator.userAgent.toLowerCase()));	
	    if(!ismobile){
		    /* widgets for desktop */
		    setup_widgets_desktop();
			
		} else {
			/* widgets for desktop */
			setup_widgets_mobile();
		}
	}	
	/* end isMobile */

    /* ---------------------------------------------------------------------- */
	/*	Widgets Desktop
	/* ---------------------------------------------------------------------- */	
	
	function setup_widgets_desktop() {
		
		if ($('#widget-grid').length){
			
			$('#widget-grid').jarvisWidgets({	
							
				grid: 'article',
				widgets: '.jarviswidget',
				localStorage: true,
				deleteSettingsKey: '#deletesettingskey-options',
				settingsKeyLabel: 'Reset settings?',
				deletePositionKey: '#deletepositionkey-options',
				positionKeyLabel: 'Reset position?',
				sortable: true,
				buttonsHidden: false,
				toggleButton: true,
				toggleClass: 'min-10 | plus-10',
				toggleSpeed: 200,
				onToggle: function(){},
				deleteButton: false,
				deleteClass: 'trashcan-10',
				deleteSpeed: 200,
				onDelete: function(){},
				editButton: true,
				editPlaceholder: '.jarviswidget-editbox',
				editClass: 'pencil-10 | edit-clicked',
				editSpeed: 200,
				onEdit: function(){},
				fullscreenButton: false,
				fullscreenClass: 'fullscreen-10 | normalscreen-10',	
				fullscreenDiff: 3,		
				onFullscreen: function(){},
				customButton: false,
				customClass: 'folder-10 | next-10',
				customStart: function(){ alert('Hello you, this is a custom button...') },
				customEnd: function(){ alert('bye, till next time...') },
				buttonOrder: '%refresh% %delete% %custom% %edit% %fullscreen% %toggle%',
				opacity: 1.0,
				dragHandle: '> header',
				placeholderClass: 'jarviswidget-placeholder',
				indicator: true,
				indicatorTime: 600,
				ajax: true,
				timestampPlaceholder:'.jarviswidget-timestamp',
				timestampFormat: 'Last update: %m%/%d%/%y% %h%:%i%:%s%',
			    refreshButton: true,
			    refreshButtonClass: 'refresh-10',
				labelError:'Sorry but there was a error:',
				labelUpdated: 'Last Update:',
				labelRefresh: 'Refresh',
				labelDelete: 'Delete widget:',
				afterLoad: function(){},
				rtl: false
				
			});
			
		} // end if
		
	}	
	/* end widgets desktop */   
    /* ---------------------------------------------------------------------- */
	/*	Widgets Mobile
	/* ---------------------------------------------------------------------- */
	
	function setup_widgets_mobile() {	
		
		if ($('#widget-grid').length){
			
			$('#widget-grid').jarvisWidgets({	
							
				grid: 'article',
				widgets: '.jarviswidget',
				localStorage: true,
				deleteSettingsKey: '#deletesettingskey-options',
				settingsKeyLabel: 'Reset settings?',
				deletePositionKey: '#deletepositionkey-options',
				positionKeyLabel: 'Reset position?',
				sortable: false, // sorting disabled for mobile
				buttonsHidden: false,
				toggleButton: true,
				toggleClass: 'min-10 | plus-10',
				toggleSpeed: 200,
				onToggle: function(){},
				deleteButton: false,
				deleteClass: 'trashcan-10',
				deleteSpeed: 200,
				onDelete: function(){},
				editButton: true,
				editPlaceholder: '.jarviswidget-editbox',
				editClass: 'pencil-10 | edit-clicked',
				editSpeed: 200,
				onEdit: function(){},
				fullscreenButton: false,
				fullscreenClass: 'fullscreen-10 | normalscreen-10',	
				fullscreenDiff: 3,		
				onFullscreen: function(){},
				customButton: false, // custom button disabled for mobile
				customClass: 'folder-10 | next-10',
				customStart: function(){ alert('Hello you, this is a custom button...') },
				customEnd: function(){ alert('bye, till next time...') },
				buttonOrder: '%refresh% %delete% %custom% %edit% %fullscreen% %toggle%',
				opacity: 1.0,
				dragHandle: '> header',
				placeholderClass: 'jarviswidget-placeholder',
				indicator: true,
				indicatorTime: 600,
				ajax: true,
				timestampPlaceholder:'.jarviswidget-timestamp',
				timestampFormat: 'Last update: %m%/%d%/%y% %h%:%i%:%s%',
			    refreshButton: true,
			    refreshButtonClass: 'refresh-10',
				labelError:'Sorry but there was a error:',
				labelUpdated: 'Last Update:',
				labelRefresh: 'Refresh',
				labelDelete: 'Delete widget:',
				afterLoad: function(){},
				rtl: false
				
			});
			
		}// end if
		
	}		
	/* end widgets Mobile */ 
    
    /* ---------------------------------------------------------------------- */
	/*	Reset widgets script
	/* ---------------------------------------------------------------------- */
		
	function resetWidget() {
		var cls = confirm("Would you like to RESET all your saved widget settings and clear LocalStorage?");
		if(cls && localStorage){
			localStorage.clear();
			//alert('Local storage has been cleared! Refreshing page...');
			location.reload();
		}

	}
    //END - JARVIS WIDGET SETTINGS ----------------------------------------//



    //START  -- Pie Chart: Job Creation Trend
    function setup_JSONData_JobCreationTrend_PieChart()
    {
        var d = new Date();
        var i = d.getTime();

        var chartError = function(req, status, err) {
            alert('An error occurred: '+ err);
	    };
            
        $.ajax({
            url: '../../UserControl/Worklist_DB_JobCreationTrend.ashx?v='+i,
            method: 'GET',
            dataType: 'json',
            success: DrawChart_JobCreationTrend,
            error: chartError
        });
    }
        
    function DrawChart_JobCreationTrend(data_pie)
    {
        if ($('#pie-chart-JobCreationTrend').length) {
	
			$.plot($("#pie-chart-JobCreationTrend"), data_pie, {
				series : {
					pie : {
						show : true,
						/*innerRadius : 0.5,*/
						radius : 1,
						label : {
							show : true,
							radius : 2 / 3,
							formatter : function(label, series) {
								return '<div style="font-size:11px;text-align:center;padding:4px;color:white;">' + label + '<br/>' + Math.round(series.percent) + '%</div>';
							},
							threshold : 0.0
						}
					}
				},
				legend : {
					show : true,
					noColumns : 1, // number of colums in legend table
					labelFormatter : null, // fn: string -> string
					labelBoxBorderColor : "#000", // border color for the little label boxes
					container : null, // container (as jQuery object) to put legend in, null means default on top of graph
					position : "ne", // position of default legend container within plot
					margin : [5, 10], // distance from grid edge to default legend container within plot
					backgroundColor : "#efefef", // null means auto-detect
					backgroundOpacity : 1 // set to 0 to avoid background
				},
				grid : {
					hoverable : true,
					clickable : true
				},
			});
	
		}
    }
    //END  -- Pie Chart: Job Creation Trend

    //START  -- Data Table: Jobs updated in last 24 H
    function setup_JSONData_JobsUpdated_InLast24Hours()
    {
        var d = new Date();
        var i = d.getTime();

        var chartError = function(req, status, err) {
            alert('An error occurred: '+ err);
	    };
            
        
        var oTable = $('#dt_jobs_updated_24H').dataTable( {
					"bProcessing": false,
					"sAjaxSource": '../../UserControl/Worklist_DB_JobsUpdated_InLast24Hours.ashx?v='+i
				} );


    }
    //END  -- Data Table: Jobs updated in last 24 H

    //START  -- Data Table: Jobs Created in last 24 H
    function setup_JSONData_JobsCreated_InLast24Hours()
    {
        var d = new Date();
        var i = d.getTime();

        var chartError = function(req, status, err) {
            alert('An error occurred: '+ err);
	    };           
        
        var oTable = $('#dt_jobs_created_24H').dataTable( {
					"bProcessing": false,
                    "bJQueryUI": true,
					"sAjaxSource": '../../UserControl/Worklist_DB_JobsCreated_InLast24Hours.ashx?v='+i
				} );
    }
    //END  -- Data Table: Jobs Created in last 24 H

    //START  -- Data Table: Superintendent inspection items in last 6 months
    function setup_JSONData_Jobs_Super_Inspection_Last6M()
    {
        var d = new Date();
        var i = d.getTime();

        var chartError = function(req, status, err) {
            alert('An error occurred: '+ err);
	    };
            
        
        var oTable = $('#dt_jobs_super_6M').dataTable( {
                "bPaginate": false,
                "bLengthChange": false,
                "bFilter": false,
                "bSort": true,
                "bInfo": false,
                "bAutoWidth": false,                
                "sAjaxSource": '../../UserControl/Worklist_DB_Jobs_Super_Inspected.ashx?v='+i
				} );


    }
    //END  -- Data Table: Superintendent inspection items in last 6 months

    //START  -- Data Table: NCR raised/closed in last 3 months
    function setup_JSONData_NCR_Raised_InLast3M()
    {
        var d = new Date();
        var i = d.getTime();

        var chartError = function(req, status, err) {
            alert('An error occurred: '+ err);
	    };
        
        var oTable = $('#dt_NCRs_3M').dataTable( {
                "bPaginate": false,
                "bLengthChange": false,
                "bFilter": false,
                "bSort": true,
                "bInfo": false,
                "bAutoWidth": false,                
                "sAjaxSource": '../../UserControl/Worklist_DB_NCR_Raised.ashx?v='+i
				} );
    }
    //END  -- Data Table: NCR raised/closed in last 3 months

    //START setup_JSONData_HighPriority_Jobs_InLast3M
    function setup_JSONData_HighPriority_Jobs_InLast3M()
    {
        var d = new Date();
        var i = d.getTime();

        var chartError = function(req, status, err) {
            alert('An error occurred: '+ err);
	    };
            
        
        var oTable = $('#dt_HighPriority_Jobs').dataTable( {
                "bPaginate": false,
                "bLengthChange": false,
                "bFilter": false,
                "bSort": true,
                "bInfo": false,
                "bAutoWidth": false,                
                "sAjaxSource": '../../UserControl/Worklist_DB_HighPriority_Jobs.ashx?v='+i
				} );
    }
    //END setup_JSONData_HighPriority_Jobs_InLast3M

    //START  -- Data Table: NCR trend in last 3 months
    function setup_JSONData_NCR_Trend_BarChart()
    {
        var d = new Date();
        var i = d.getTime();

        var chartError = function(req, status, err) {
            alert('An error occurred: '+ err);
	    };
        
        //var data = [ ["January", 10], ["February", 8], ["March", 4], ["April", 13], ["May", 17], ["June", 9] ];            
        $.ajax({
            url: '../../UserControl/Worklist_DB_NCR_Trend_BarChart.ashx?v='+i,
            method: 'GET',
            dataType: 'json',
            success: DrawChart_NCR_Trend,
            error: chartError
        });
    }
    //END  -- Data Table: NCR raised/closed in last 3 months

    //START  -- Draw Bar Chart
    function DrawChart_NCR_Trend(ds)
    {    
    
        if ($("#bar-chart-ncr-trend").length) {
            $.plot("#bar-chart-ncr-trend", ds, {
			    bars: {
                    show: true,
                    barWidth: 0.6
                },
                xaxis: {
                    mode: "categories"
                }
		    });
		}
    }

    //END  -- Draw Bar Chart
    

    //START  -- Get Data for : Super inspected for last 12 months
    function setup_JSONData_Super_Inspected_BarChart()
    {
        var d = new Date();
        var i = d.getTime();

        var chartError = function(req, status, err) {
            alert('An error occurred: '+ err);
	    };
        

        //var data = [ ["January", 10], ["February", 8], ["March", 4], ["April", 13], ["May", 17], ["June", 9] ];            
        $.ajax({
            url: '../../UserControl/Worklist_DB_Super_Inspected_BarChart.ashx?v='+i,
            method: 'GET',
            dataType: 'json',
            success: DrawChart_Super_Inspected_BarChart,
            error: chartError
        });
    }
    //END  -- Data Table: NCR raised/closed in last 3 months

    //START  -- Draw Bar Chart
    function DrawChart_Super_Inspected_BarChart(ds)
    {    
        if ($("#bar_chart_jobs_inspected_super").length) {
            $.plot("#bar_chart_jobs_inspected_super", [ds], {
			    series: {
				    bars: {
					    show: true,
					    barWidth: 0.6,
					    align: "center"
				    }
			    },
			    xaxis: {
				    mode: "categories",
				    tickLength: 0
			    }
		    });		
	
		}
    }

    //END  -- Draw Bar Chart
    