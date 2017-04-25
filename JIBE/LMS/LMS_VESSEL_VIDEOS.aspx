<%@ Page Title="Vessel Videos" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="LMS_VESSEL_VIDEOS.aspx.cs" Inherits="LMS_LMS_VESSEL_VIDEOS"  %>
 <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
	<link href="../Scripts/JsTree/themes/default/style.min.css" rel="stylesheet" type="text/css" />
	<script src="../Scripts/JsTree/libs/jquery.js" type="text/javascript"></script>
	<script src="../Scripts/JsTree/jstree.min.js" type="text/javascript"></script>
	<script src="../Scripts/jquery-ui.min1.11.0.js" type="text/javascript"></script>
	<link href="../Styles/jquery-ui1.11.0.css" rel="stylesheet" type="text/css" />
	<script src="../Scripts/LMS_VESSEL_VIDEOS.js" type="text/javascript"></script>
	<link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
	<script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
		<link href="../Styles/video-js.css" rel="stylesheet" type="text/css" />
	<script src="../Scripts/video.js" type="text/javascript"></script>
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
<script type="text/javascript">

	function LMS_Play_video(src, Item_name) {
		try {


			var $vid_obj = _V_("vdPlayControl");
			// hide the current loaded poster
			$("img.vjs-poster").hide();

			$vid_obj.ready(function () {
				// hide the video UI
				$("#div_video_html5_api").hide();
				// and stop it from playing
				$vid_obj.pause();
				// assign the targeted videos to the source nodes
				$("video:nth-child(1)").attr("src", src);

				// reset the UI states
				$(".vjs-big-play-button").hide();
				$("#vdPlayControl").removeClass("vjs-playing").addClass("vjs-paused");
				// load the new sources
				$vid_obj.load();
				$("#div_video_html5_api").show();
				$vid_obj.play();
			});


			document.getElementById('dvVideoPlayer').style.display = 'block';
			document.getElementById('spnPlayerTitle').innerHTML = Item_name;

		}
		catch (ex) {
			alert(ex.Message);
		}

	}

	function LMS_Disable_Right_Click() {

		return false;

	}





	function NodeClick() {


		urf = $("#ctl00_MainContent_hfUrl").val() + $("#ctl00_MainContent_hfFileName").val();
		LMS_Play_video(urf, $("#ctl00_MainContent_hfText").val());
		

	}
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<div class="page-title" style="margin-bottom: 3px;">
	   Vessel Videos
	</div>
	<asp:UpdatePanel ID="udpMain" runat="server" >
		
	<ContentTemplate>
 
	<div style="float: left; border: 1px solid gray; border-radius: 5px; width: 20%;
			min-width: 250px; max-height: 600px; overflow: hidden;">
			<div>
			<table style="width:100%">
			<tr>
			<td>
			<asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
			</td>
			<td>
			  <asp:Button ID="btnEdit" runat="server" Text="Edit" onclick="btnEdit_Click" />
			</td>
            <td>
			      <asp:ImageButton ID="ImgExpExcel" runat="server" ToolTip="Download All Videos" OnClick="btnExport_Click"
                                        Width="16px" ImageUrl="~/Images/Download-icon.png" 
                      style="height: 16px" />
			</td>
			</tr>
			
			</table>
			
			</div>
			 <div id="FunctionalTree" style="min-width: 250px; max-height: 600px; overflow-y: scroll;
				padding-top: 3px">
			</div>
			</div>
			<div  style="float: left; border: 1px solid gray; border-radius: 5px;">
								<div id="dvVideoPlayer" style="border: 1px solid gray; display: none;">
									<video id="vdPlayControl" class="video-js vjs-default-skin" controls style="border: 1px solid gray"
										height="700px" data-setup="{}" preload="none"  oncontextmenu="return LMS_Disable_Right_Click()"
										width="905px">
										<source src="" type="video/mp4">
									</video><br />
									<span id="spnPlayerTitle" style="color: Navy; font-size: 16px; margin: 10px"></span>
								</div>
			</div>
	
	</ContentTemplate>
	<Triggers>
            <asp:PostBackTrigger ControlID="ImgExpExcel" />
            
        </Triggers>
	</asp:UpdatePanel>

			

	  <div id="dvCategory" style="display: none;" title="Category">
		<asp:UpdatePanel ID="UpdatePanel3" runat="server">
			<ContentTemplate>
				<table style="margin:10px">
					<tr>
						<td>
							Category Name
						</td>
						<td>
							<asp:TextBox ID="txtCategoryName" CssClass="input" runat="server" Width="120px"
								></asp:TextBox>
							 
						</td>
					</tr>
					 
					<tr>
						<td>
							<asp:Button ID="btnAddCatrgory" runat="server" Text="Save" 
								onclick="btnAddCatrgory_Click"   />
						</td>
						<td>
							<asp:Button ID="btnDeleteCatrgory" runat="server" Text="Delete" 
								onclick="btnDeleteCatrgory_Click" style="height: 26px" 
								 />
						</td>
					</tr>
				</table>
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>


	<div id="dvItem" style="display: none;" title="Items">
		<asp:UpdatePanel ID="UpdatePanel1" runat="server">
			<ContentTemplate>
				<table style="margin:10px">
					<tr>
						<td>
							Item Name
						</td>
						<td>
							<asp:TextBox ID="txtItemName" CssClass="input" runat="server" Width="120px"></asp:TextBox>
							 
						</td>

					</tr>

					<tr>
					<td>
					Video File
					</td>
					<td>
						<asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="../Images/update.gif"/></asp:Label>
					<tlk4:AjaxFileUpload ID ="fuVideo" runat="server" Padding-Bottom="2" Padding-Left="2"
												 Padding-Right="1" 
												Padding-Top="2" ThrobberID="myThrobber" OnUploadComplete="fuVideo_OnUploadComplete"
												MaximumNumberOfFiles="1" MaximumSizeOfFile="20480" />
					</td>
					
					</tr>
					 
					<tr>
						<td>
							<asp:Button ID="btnAddItem" runat="server" Text="Save" onclick="btnAddItem_Click" 
								 />
						</td>
						<td>
							<asp:Button ID="btnDeleteItem" runat="server" Text="Delete" onclick="btnDeleteItem_Click" 
								   />
						</td>
					</tr>
				</table>
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>

	   <asp:HiddenField ID="hfPar" runat="server" Value="" />
		 <asp:HiddenField ID="hfNode" runat="server" Value="" />
			<asp:HiddenField ID="hfText" runat="server" Value="" />
			  <asp:HiddenField ID="hfUrl" runat="server" Value="" />
		   <asp:HiddenField ID="hfFileName" runat="server" Value="" />

		 
</asp:Content>