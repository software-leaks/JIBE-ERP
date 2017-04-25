<%@ Page Title="CheckList Rating" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CheckListRating.aspx.cs" Inherits="Technical_Inspection_CheckListRating" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/ChecklistRatings.js?v1" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {

            var hdnfeild = $('[id$=hdnQuerystring]')
            var val = document.getElementById(hdnfeild.attr('id')).value;
            var hdnfeildInspID = $('[id$=hdnQuerystringInspID]')
            var val1 = document.getElementById(hdnfeildInspID.attr('id')).value;
            if (val != "") {
                GetCheckListName(val, val1);
            }
        });
    </script>
    <style type="text/css">
        .insp-Chk-Checklist
        {
            color: Black;
            font-size: 14px;
            float: left;
            min-width: 300px;
            border: 0px solid gray;
            background-color: #efefef;
            padding: 5px;
            font-weight: bold;
        }
        .insp-Chk-Group
        {
            background-color: Yellow;
            color: Black;
            font-size: 14px;
            border: 1px solid gray;
            background-color: #efefef;
            padding: 5px;
            font-weight: bold;
            cursor: pointer;
        }
        
        /*padding:3px;*/
        .insp-Chk-Location
        {
            border: 1px solid gray;
            background-color: Gray;
            color: Black;
            font-size: 12px;
            padding: 5px;
            font-weight: bold;
            cursor: pointer;
        }
        .insp-Chk-Location-Text
        {
            border: 2px solid black;
            color: Black;
            font-size: 12px;
            padding: 5px;
            font-weight: bold;
            cursor: pointer;
        }
        
        .insp-Chk-Item-Container
        {
            color: Black;
            font-size: 11px;
            cursor: pointer;
        }
        
        .insp-Item-Pos1
        {
            margin-left: 10px;
        }
        .insp-Item-Pos2
        {
            margin-left: 50px;
        }
        .insp-Item-Pos3
        {
            margin-left: 100px;
        }
        .insp-Item-Pos4
        {
            margin-left: 150px;
        }
        .insp-Item-Pos5
        {
            margin-left: 200px;
        }
        .insp-Item-Pos6
        {
            margin-left: 250px;
        }
        .insp-Item-Pos7
        {
            margin-left: 300px;
            float: left;
        }
        
        .Addshedule
        {
            overflow: hidden;
            display: inline-block;
            float: right;
            padding-right: 7px;
            padding-top: 0px;
        }
        
        .rating
        {
            overflow: hidden;
            display: inline-block;
            float: right;
        }
        .rating-input
        {
            position: absolute;
            left: 0;
            top: -50px;
        }
        .rating-star
        {
            display: block;
            float: right;
            width: 16px;
            height: 16px;
            background: url('../../Images/star.png') 0 -16px;
        }
        .rating-star:hover, .rating-star:hover ~ .rating-star, .rating-input:checked ~ .rating-star
        {
            background-position: 0 0;
        }
        
        .badge
        {
            color: white;
        }
        
        .badge2
        {
            color: white;
            float: left;
        }
        
        a:link
        {
            color: white;
        }
        
        a:visited
        {
            color: white;
        }
        
        .badge1
        {
            overflow: hidden;
            display: inline-block;
            float: right;
            background: green;
            color: white;
            width: 19px;
            height: 19px;
            text-align: center;
            text-decoration: none;
            line-height: 17px;
            border-radius: 50%;
            box-shadow: 0 0 1px #333;
            padding-right: 0px;
        }
        .badge1[data-badge]:after
        {
            content: attr(data-badge);
            position: absolute;
            top: -12px;
            right: -10px;
            font-size: 1.0em;
            background: green;
            color: white;
            width: 25px;
            height: 25px;
            text-align: center;
            text-decoration: none;
            line-height: 25px;
            border-radius: 50%;
            box-shadow: 0 0 1px #333;
        }
        
        .insp-table
        {
            float: right;
            margin-top: -4px;
        }
        
        /* Just for the demo */
        body
        {
            margin: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <div class="page-title">
            Checklist Rating
        </div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <div style="border: 1px solid gray; border-radius: 5px; min-width: 100px; max-height: 600px;">
                    <div id="dvchecklistHeads" style="padding-top: 3px">
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="border: 1px solid gray; border-radius: 5px; width: 100%; min-width: 250px;
            max-height: 800px; overflow: hidden;">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="FunctionalTree" style="min-width: 250px; height: 750px; max-height: 750px;
                        overflow: scroll; padding-top: 3px">
                    </div>
                    <div id="dvPrint" style="margin-left: 40%; padding-top: 5px; padding-bottom: 5px;
                        display: none">
                        <asp:Button ID="btnSave" Width="100px" runat="server" Text="Save" OnClientClick="return SaveAll();" />&nbsp;
                        <asp:Button ID="btnFinalize" Width="100px" runat="server" Text="Finalize" OnClientClick="return Finalize();" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="dvJob" style="display: none; width: 800px;" title="Defect/Condition Report">
            <iframe id="IframeJob" src="" frameborder="0" style="height: 500px; width: 100%">
            </iframe>
        </div>
        <div id="dvEditJob" style="display: none; width: 1400px;" title="Assign Job">
            <iframe id="IframeEditJob" src="" frameborder="0" style="height: 500px; width: 100%">
            </iframe>
        </div>
        <asp:HiddenField ID="hdnfldUserID" runat="server" />
        <asp:HiddenField ID="hdnfldVesselType" runat="server" />
        <asp:HiddenField ID="hdnQuerystring" runat="server" />
        <asp:HiddenField ID="hdnQuerystringInspID" runat="server" />
        <asp:HiddenField ID="hdnParentID" runat="server" />
        <asp:HiddenField ID="hdnVesselID" runat="server" />
        <asp:HiddenField ID="hdnChecklistID" runat="server" />
    </div>
</asp:Content>
