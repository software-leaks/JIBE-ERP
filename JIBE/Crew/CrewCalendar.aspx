<%@ Page Title="Crew Calendar" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CrewCalendar.aspx.cs" Inherits="Crew_CrewCalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <style type="text/css">
        #pageTitle
        {
            background-color: gray;
            color: White;
            font-size: 12px;
            text-align: center;
            padding: 2px;
            font-weight: bold;
        }
        #page-content
        {
            font-size: 12px;
        }
        .header
        {
            margin: 0 0 0 0;
            padding: 6px 2 6px 2;
            color: #FFF;
          
        }
        h4
        {
            font-size: 1.2em;
            color: #ffffff;
            font-weight: bold;
            margin: 0 0 0 5px;
        }
        .content
        {
            background: white;
            padding: 5px;
            margin: 5px;
        }
        .dataTable
        {
            background-color: #ffffff;
            border: 1px solid #efefef;
            border-collapse: collapse;
            color: Teal;
        }
        .dataTable td
        {
            background-color: #ffffff;
            border: 1px solid #efefef;
            border-collapse: collapse;
        }
        .dataTable .data
        {
            vertical-align: top;
            background-color: #ffffff;
            color: Black;
        }
        .inline-edit
        {
            font-size: 10px;
            text-decoration: none;
            font-weight: normal;
            
            color: Blue;
        }
        .up
        {
            background-image: url(../Images/up.png);
            background-repeat: no-repeat;
        }
        .down
        {
            background-image: url(../Images/down.png);
            background-repeat: no-repeat;
        }
        .class-doc-list
        {
            font-size: 11px;
        }
        .class-doc-edit
        {
            font-size: 11px;
        }
        .control-edit
        {
            font-size: 12px;
            padding: 0px;
            width: 150px;
        }
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            padding: 2px;
        }
        .error-message span
        {
            background-color: Yellow;
            text-align: center;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="pageTitle" style="background-color: gray; color: White; font-size: 12px;
        text-align: center; padding: 2px; font-weight: bold;">
        <asp:Label ID="lblPageTitle" runat="server" Text="Crew Calendar"></asp:Label>
    </div>
    <div class="error-message">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </div>
    <div id="page-content" style="height: 620px; border: 1px solid gray; z-index: -2;
        overflow: auto; text-align: center;">
        <div id="dvCrewCal">
        </div>
        <iframe width="100%" height="100%" marginheight="0px" frameborder="0" src="../Scripts/wdCalendar/wdCalendar/Index.htm">
        
        </iframe>
        </div>
</asp:Content>
