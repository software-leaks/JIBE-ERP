        function displayCalendar(cal,dt) {	        
            var now = new Date();

            var MonthNames = new Array("January","February","March","April","May","June","July","August","September","October","November","December");
            var MonthLengths = new Array(31,28,31,30,31,30,31,31,30,31,30,31);
            var DisablePast		= false;
            var HighlightToday	= true;
	        var wDay = 1;
            var y = dt.getFullYear(); // Working Date
            var m = dt.getMonth();
            var d = dt.getDate();	        
            var ny = now.getFullYear(); // Today's Date
            var nm = now.getMonth();
            var nd = now.getDate();
            
	        var wDate = new Date(y,m,wDay);
            
	        var sCal = "<table cellspacing=1 cellpading=0 class='calTable'>"+ 
	            "<tr class='cellMonth'><td colspan='6'>" + MonthNames[m] + " "+y+"</td>"+ 
	            "<td style='text-align:right'><img src='images/leftarrow.png' onclick='prevMonth()' style='cursor:hand;'><img src='images/refresh.png' onclick='thisMonth()' style='cursor:hand;'><img src='images/rightarrow.png' onclick='nextMonth()' style='cursor:hand;'></td></tr>"+
		        "<tr class='cellDHeader'><td>Sun</td><td>Mon</td><td>Tue</td><td>Wed</td><td>Thu</td><td>Fri</td><td>Sat</td></tr>";

	        
	        if(isLeapYear1(wDate)) {
		        MonthLengths[1] = 29;
	        } else {
		        MonthLengths[1] = 28;
	        }
	        
	        var dayclass = "";
	        var wkclass = "";
	        var isToday = false;
	        
	        var wSt = new Date(now.getFullYear(),now.getMonth(),now.getDate());
	        wSt.setDate(now.getDate()-now.getDay());
	        var wEd = new Date(wSt.getFullYear(),wSt.getMonth(),wSt.getDate());
	        wEd.setDate(wSt.getDate()+6);
	        
	        for(var r=1; r<7; r++) {
		        sCal = sCal + "<tr>";
		        for(var c=0; c<7; c++) {

			        var wDate = new Date(y,m,wDay);
			        
			        if(wDate.getDay() == c && wDay<=MonthLengths[m]) {
				        if(wDate.getDate()==nd && wDate.getMonth()==nm && wDate.getFullYear()==ny && HighlightToday) {
					        dayclass = "cellToday";
					        isToday = true;
					        
				        } 
//				        else  if(wDate.getDate()==d && wDate.getMonth()==m && wDate.getFullYear()==y) {
//					        dayclass = "cellSelected";
//					        isToday = true;  // only matters if the selected day IS today, otherwise ignored.
//				        }
				        else {
					        dayclass = "cellDay";
					        isToday = false;
				        }
				        if (wDate >= wSt && wDate <= wEd){
				            wkclass = "selWeek";
				        }else {
				            wkclass = "";
				        }
				        
				        if(((now > wDate) && !DisablePast) || (now <= wDate) || isToday) { // >
					        // user wants past dates selectable
					        //week end
					        
					        if(isToday==true)				        
                                sCal = sCal + "<td class='" +dayclass+ " " +wkclass+ "' id="+ m +"_"+wDay+"><div><font color='#c0c0c0'>TODAY</font></div></td>";
                            else
                                sCal = sCal + "<td class='" +dayclass+ " " +wkclass+ "' id="+ m +"_"+wDay+"><div><font color='#c0c0c0'>"+wDay+"</font></div></td>";
                            
				        } else {
					        // user wants past dates to be read only
					        sCal = sCal + "<td class='" +dayclass+ " " +wkclass+ "' id="+ m +"_"+wDay+">**"+wDay+"</td>";
				        }
				        wDay++;
			        } else {
				        sCal = sCal + "<td class='unused'></td>";
			        }

		        }
		        sCal = sCal + "</tr>";
	        }
	        sCal = sCal + "</table>"
	        
	        cal.innerHTML = sCal; // works in FireFox, opera
        }
       function isLeapYear1(dTest) {
	        var y = dTest.getYear();
	        var bReturn = false;
        	
	        if(y % 4 == 0) {
		        if(y % 100 != 0) {
			        bReturn = true;
		        } else {
			        if (y % 400 == 0) {
				        bReturn = true;
			        }
		        }
	        }
        	
	        return bReturn;
        }	
