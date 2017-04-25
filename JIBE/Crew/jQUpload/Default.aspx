<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Demo Page</title>
    <script type="text/javascript" src="Scripts/jquery-1.2.6.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.ajax_upload.0.6.min.js"></script>
<script type= "text/javascript">/*<![CDATA[*/
$(document).ready(function(){
	/* example 1 */
	var button = $('#button1'), interval;
	$.ajax_upload(button,{
		action: 'FileHandler.ashx',
		name: 'myfile',
		onSubmit : function(file, ext){
			// change button text, when user selects file			
			button.text('Uploading');
			
			// If you want to allow uploading only 1 file at time,
			// you can disable upload button
			this.disable();
			
			// Uploding -> Uploading. -> Uploading...
			interval = window.setInterval(function(){
				var text = button.text();
				if (button.text().length < 13){
					button.text(button.text() + '.');					
				} else {
					button.text('Uploading');				
				}
			}, 200);
		},
		onComplete: function(file, response){
			button.text('Upload');
			
			// Although plugins emulates hover effect automatically,
			// it doens't work when button is disabled
			button.removeClass('hover');
			
			window.clearInterval(interval);
						
			// enable upload button
			this.enable();
			
			// add file to the list
			$('<li></li>').appendTo('#example1 .files').text(file);						
		}
	});
	
	
});/*]]>*/</script>
		
<style type="text/css">
body {font-family: verdana, arial, helvetica, sans-serif;font-size: 12px;background: #373A32;color: #D0D0D0;}
h1 {color: #C7D92C;	font-size: 18px; font-weight: 400;}
a {	color: white;}
#text {	margin: 25px; }
ul { list-style: none; }

.example {	
	padding: 0 20px;
	float: left;		
	width: 230px;
	height:200px;
}

.wrapper {
	width: 133px;
	/* Centering button will not work, so we need to use additional div */
	margin: 0 auto;
}

div.button {
	height: 29px;	
	width: 133px;
	background: url(button.png) 0 0;
	
	font-size: 14px;
	color: #C7D92C;
	text-align: center;
	padding-top: 15px;
}
/* 
We can't use ":hover" preudo-class because we have
invisible file input above, so we have to simulate
hover effect with javascript. 
 */
div.button.hover {
	background: url(button.png) 0 56px;
	color: #95A226;	
}
</style>		
</head>
<body>
    <form id="form1" runat="server">
<ul style="height:200px;">
	<li id="example1" class="example">
		<p>You can style button as you want</p>
		<div class="wrapper">
			<div id="button1" class="button">Upload</div>
		</div>
		<p>Uploaded files:</p>
		<ol class="files"></ol>
	</li>
</ul>
    </form>
</body>
</html>
