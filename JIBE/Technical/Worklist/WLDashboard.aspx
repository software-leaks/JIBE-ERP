<%@ Page Title="Worklist Dashboard" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeFile="WLDashboard.aspx.cs" Inherits="Technical_Worklist_WLDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <!-- // DO NOT REMOVE OR CHANGE ORDER OF THE FOLLOWING // -->
    <!-- bootstrap default css (DO NOT REMOVE) -->
    <link rel="stylesheet" href="../../css/bootstrap.min.css?v=1" />
    <link rel="stylesheet" href="../../css/bootstrap-responsive.min.css?v=1" />
    <!-- font awsome and custom icons -->
    <link rel="stylesheet" href="../../css/font-awesome.min.css?v=1" />
    
    <!-- jarvis widget css -->
    <link rel="stylesheet" href="../../css/jarvis-widgets.css?v=1" />
    
    <!-- main theme files - required for chart-->
    <link rel="stylesheet" href="../../css/theme.css?v=1" />
    <link rel="stylesheet" href="../../css/theme-responsive.css?v=1" />
    
    <!-- To switch to full width -->
    <link rel="stylesheet" id="switch-width" href="../../css/full-width.css?v=1" />
    <link rel="stylesheet" type="text/css" href="../../css/DT_bootstrap.css" />
 
    <script type="text/javascript" src="../../js/libs/jquery.min.js"></script>
    <script type="text/javascript" src="../../js/libs/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../../js/include/selectnav.min.js"></script>
    <script type="text/javascript" src="../../js/include/jquery.accordion.min.js"></script>
    <script type="text/javascript" src="../../js/include/toastr.min.js"></script>
    <script type="text/javascript" src="../../js/include/slimScroll.min.js"></script>
    <script type="text/javascript" src="../../js/include/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../../js/include/DT_bootstrap.min.js"></script>
    <script type="text/javascript" src="../../js/include/jquery.uniform.min.js"></script>
    <script type="text/javascript" src="../../js/include/jarvis.widget.min.js"></script>
    <script type="text/javascript" src="../../js/include/mobiledevices.min.js"></script>
    <!-- REQUIRED: Flot Chart Engine -->
    <script type="text/javascript" src="../../js/include/jquery.flot.js"></script>
    <script type="text/javascript" src="../../js/include/jquery.flot.cust.min.js"></script>
    <script type="text/javascript" src="../../js/include/jquery.flot.resize.min.js"></script>
    <script type="text/javascript" src="../../js/include/jquery.flot.tooltip.min.js"></script>
    <script type="text/javascript" src="../../js/include/jquery.flot.orderBar.min.js"></script>
    <script type="text/javascript" src="../../js/include/jquery.flot.fillbetween.min.js"></script>
    <script type="text/javascript" src="../../js/include/jquery.flot.pie.min.js"></script>
    <script type="text/javascript" src="../../js/include/jquery.flot.categories.js"></script>
    <!-- REQUIRED: Bootstrap engine -->
    <script src="../../js/include/bootstrap.js"></script>
    <!-- DO NOT REMOVE: Theme Config file -->
    <script type="text/javascript" src="../../Scripts/Worklist_Dashboard.js"></script>

    <style type="text/css">
        .table th
        {
            padding: 0px;
            text-align: center;
        }
        .table td
        {
            padding: 0px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="fluid-container">
        <!-- widget grid -->
        <section id="widget-grid" class="">	
		    <!-- row-fluid -->							
            <div class="row-fluid" id="row1">								
			    <article class="span6">
                	<!-- Jobs updated in last 24 H -->
				    <div class="jarviswidget" id="widget-id-jobs-updated">
					    <header>
						    <h2>Jobs Updated in Last 24 Hours</h2>                           
					    </header>
					    <div>									    
						    <div class="jarviswidget-editbox">
							    <div>
								    <label>Title:</label>
								    <input type="text" />
							    </div>
							    <div>
								    <label>Styles:</label>
								    <span data-widget-setstyle="purple" class="purple-btn"></span>
								    <span data-widget-setstyle="navyblue" class="navyblue-btn"></span>
								    <span data-widget-setstyle="green" class="green-btn"></span>
								    <span data-widget-setstyle="yellow" class="yellow-btn"></span>
								    <span data-widget-setstyle="orange" class="orange-btn"></span>
								    <span data-widget-setstyle="pink" class="pink-btn"></span>
								    <span data-widget-setstyle="red" class="red-btn"></span>
								    <span data-widget-setstyle="darkgrey" class="darkgrey-btn"></span>
								    <span data-widget-setstyle="black" class="black-btn"></span>
							    </div>
						    </div>
            
						    <div class="inner-spacer"> 
						    <!-- content -->	
 							    <!-- bar chart -->
							    <%--<div id="bar-chart" class="chart"></div>--%>
                                <table class="table table-striped table-bordered responsive" id="dt_jobs_updated_24H">
								    <thead>
									    <tr>
										    <th>Vessel</th>
										    <th>Code</th>
										    <th>Assignor</th>
										    <th>Date Raised</th>
										    <th>Expected Compln</th>
                                            <th>Completed</th>
                                            <th>Type</th>
									    </tr>
								    </thead>
								    <tbody>														
														
														
								    </tbody>
							    </table>
						    <!-- end content -->	
						    </div>
						    <!-- end inner spacer -->
					    </div>
				    </div>
				    <!-- end widget -->

                </article>
			    <article class="span6">
                	<!-- Jobs created in last 24 H -->
				    <div class="jarviswidget" id="widget-id-jobs-created">
					    <header>
						    <h2>Jobs Created in Last 24 Hours</h2>                           
					    </header>
					    <div>
									    
						    <div class="jarviswidget-editbox">
							    <div>
								    <label>Title:</label>
								    <input type="text" />
							    </div>
							    <div>
								    <label>Styles:</label>
								    <span data-widget-setstyle="purple" class="purple-btn"></span>
								    <span data-widget-setstyle="navyblue" class="navyblue-btn"></span>
								    <span data-widget-setstyle="green" class="green-btn"></span>
								    <span data-widget-setstyle="yellow" class="yellow-btn"></span>
								    <span data-widget-setstyle="orange" class="orange-btn"></span>
								    <span data-widget-setstyle="pink" class="pink-btn"></span>
								    <span data-widget-setstyle="red" class="red-btn"></span>
								    <span data-widget-setstyle="darkgrey" class="darkgrey-btn"></span>
								    <span data-widget-setstyle="black" class="black-btn"></span>
							    </div>
						    </div>
            
						    <div class="inner-spacer"> 
						    <!-- content -->	
 							    <!-- bar chart -->
							    <%--<div id="bar-chart" class="chart"></div>--%>
                                <table class="table table-striped table-bordered responsive" id="dt_jobs_created_24H">
								    <thead>
									    <tr>
										    <th>Vessel</th>
										    <th>Code</th>
										    <th>Assignor</th>
										    <th>Date Raised</th>
										    <th>Expected Compln</th>
                                            <th>Completed</th>
                                            <th>Type</th>
									    </tr>
								    </thead>
								    <tbody>														
														
														
								    </tbody>
							    </table>
						    <!-- end content -->	
						    </div>
						    <!-- end inner spacer -->
					    </div>
				    </div>
				    <!-- end Jobs created in last 24 H -->
                </article>
            </div>        						
			<!-- end row-fluid -->
		    <!-- row-fluid -->							
		    <div class="row-fluid" id="row2">								
			    <article class="span4">                    
                    <!-- NCR raised but NOT closed in last 3 months -->
					<div class="jarviswidget" id="widget-id-ncr-3months">
						<header>
							<h2>NCR raised but NOT closed in last 3 months</h2>                           
						</header>
						<div>
                            <table class="table text-center" id="dt_NCRs_3M">
								<thead>
									<tr>
										<th>Vessel</th>
										<th>WL Count</th>											
									</tr>
								</thead>
								<tbody>			
								</tbody>
							</table>

						</div>
					</div>
					<!-- end widget -->
			    </article>
                <article class="span4">
                    <!-- High Priority Jobs in last 3 months-->
					<div class="jarviswidget" id="widget-id-highpriority">
						<header>
							<h2>High priority jobs in last 3 months</h2>                           
						</header>
						<div>
                            <table class="table text-center" id="dt_HighPriority_Jobs">
								<thead>
									<tr>
										<th>Vessel</th>
										<th>WL Count</th>											
									</tr>
								</thead>
								<tbody>			
								</tbody>
							</table>
						</div>
					</div>
					<!-- end widget -->                
                </article> 
                             
			    <article class="span4">
                    <!-- Superintendent inspection items in last 3 months -->
					<div class="jarviswidget" id="widget-id-super-inspection">
						<header>
							<h2>Superintendent inspection items in last 6 months</h2>                           
						</header>
						<div>							    		
                            <table class="table text-center" id="dt_jobs_super_6M">
								<thead>
									<tr>
										<th>Vessel</th>
										<th>WL Count</th>											
									</tr>
								</thead>
								<tbody>			
								</tbody>
							</table>							
							
						</div>
					</div>
					<!-- end Superintendent inspection items in last 3 months -->
			    </article>
		    </div>
		    <div class="row-fluid" id="Div1">								
			    <article class="span4">
                    <!-- Pie Chart: Job creation trend in last 3 months -->
				    <div class="jarviswidget" id="widget-id-chart-priority">
					    <header>
						    <h2>Job creation trend in last 3 months</h2>                           
					    </header>
					    <div>
									    
						    <div class="jarviswidget-editbox">
							    <div>
								    <label>Title:</label>
								    <input type="text" />
							    </div>
							    <div>
								    <label>Styles:</label>
								    <span data-widget-setstyle="purple" class="purple-btn"></span>
								    <span data-widget-setstyle="navyblue" class="navyblue-btn"></span>
								    <span data-widget-setstyle="green" class="green-btn"></span>
								    <span data-widget-setstyle="yellow" class="yellow-btn"></span>
								    <span data-widget-setstyle="orange" class="orange-btn"></span>
								    <span data-widget-setstyle="pink" class="pink-btn"></span>
								    <span data-widget-setstyle="red" class="red-btn"></span>
								    <span data-widget-setstyle="darkgrey" class="darkgrey-btn"></span>
								    <span data-widget-setstyle="black" class="black-btn"></span>
							    </div>
						    </div>
            
						    <div class="inner-spacer"> 
						    <!-- content goes here -->
							    <!-- bar chart -->
							    <div id="pie-chart-JobCreationTrend" class="chart has-legend"></div>
						    <!-- end content -->
						    </div>
									        
									        
					    </div>
				    </div>
				    <!-- end widget -->
                </article>
                <article class="span8">
                    <div class="jarviswidget" id="widget-id-bar-chart-ncr-trend">
					    <header>
						    <h2>NCR Trend in last 12 months</h2>                           
					    </header>
					    <div>
									    
						    <div class="jarviswidget-editbox">
							    <div>
								    <label>Title:</label>
								    <input type="text" />
							    </div>
							    <div>
								    <label>Styles:</label>
								    <span data-widget-setstyle="purple" class="purple-btn"></span>
								    <span data-widget-setstyle="navyblue" class="navyblue-btn"></span>
								    <span data-widget-setstyle="green" class="green-btn"></span>
								    <span data-widget-setstyle="yellow" class="yellow-btn"></span>
								    <span data-widget-setstyle="orange" class="orange-btn"></span>
								    <span data-widget-setstyle="pink" class="pink-btn"></span>
								    <span data-widget-setstyle="red" class="red-btn"></span>
								    <span data-widget-setstyle="darkgrey" class="darkgrey-btn"></span>
								    <span data-widget-setstyle="black" class="black-btn"></span>
							    </div>
						    </div>
            
						    <div class="inner-spacer"> 
						    <!-- content goes here -->
							    <!-- bar chart NCR trend-->
							    <div id="bar-chart-ncr-trend" class="chart has-legend"></div>
						    <!-- end content -->
						    </div>
									        
									        
					    </div>
				    </div>
                </article>
            </div>            					
			<!-- end row-fluid -->
	    </section>
        <!-- end widget grid -->
        <a id="reset-widget" href="javascript:void(0)">Clear local storage and Refresh</a>
    </div>
    
</asp:Content>
