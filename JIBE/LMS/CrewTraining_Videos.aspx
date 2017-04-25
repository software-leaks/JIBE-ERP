<%@ Page Title="Crew Training Videos" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CrewTraining_Videos.aspx.cs" Inherits="LMS_CrewTraining_Videos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
   <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
   <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <center>
<asp:HiddenField ID="hdnEdit" runat="server" />
     <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 800px;
            height: 100%;">
            <div style="height: 650px; width: 800px; color: Black;">
                <div class="page-title">
                   &nbsp;Crew Training Videos
                </div>
           <div>
       <p> JB - VID089 - ONBD Engine Log Book</p>
        <iframe width="500" 
                height="255" 
                src="https://player.vimeo.com/video/159797036" 
                frameborder="0"></iframe>
                </div>

                <div>
                <p> JB-VID093-ONBD Deck Log Book</p>
        <iframe width="500" 
                height="255" 
                src="https://player.vimeo.com/video/159797038"
                frameborder="0"></iframe>
                </div>

    
    </div>
        </div>
    </center>
</asp:Content>

