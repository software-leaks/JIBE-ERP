<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MooringPlan.aspx.cs" Inherits="Operations_MooringPlan" %>

<%@ Register Src="../UserControl/ctlRecordNavigation.ascx" TagName="ctlRecordNavigation"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script type="text/javascript">
        function openFile(filepath) {
            window.open(filepath, "_blank");
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmgr1" runat="server">
    </asp:ScriptManager>
    <div id="dvArrivalReport">
        <style type="text/css">
            .leafTR
            {
                border-bottom-style: solid;
                border-bottom-color: White;
                border-bottom-width: 1px;
            }
            .leafTD-header
            {
                width: 120px;
                height: 18px;
                padding: 0px 0px 0px 10px;
                text-align: left;
                font-size: 13px;
            }
            .leafTD-data
            {
                width: 140px;
                height: 20px;
                padding: 0px 0px 0px 3px;
                background-color: #cce499;
                text-align: left;
                font-size: 13px;
            }
            .leafTD-data-left
            {
                width: 140px;
                height: 20px;
                padding: 0px 0px 0px 2px;
                background-color: #cce499;
                text-align: center;
                font-size: 13px;
            }
            
            .TbCellCon
            {
                width: 60px;
                height: 20px;
                padding: 0px 0px 0px 0px;
                background-color: #cce499;
                text-align: left;
            }
            .TbCellConD
            {
                width: 140px;
                height: 20px;
                padding: 0px 0px 0px 0px;
                background-color: #cce499;
                text-align: left;
            }
            #pageTitle
            {
                background-color: gray;
                color: White;
                font-size: 12px;
                text-align: center;
                padding: 2px;
                font-weight: bold;
            }
        </style>
        <div>
            <div style="text-align: right">
                <%--<div id="divViewAttachments" style="font-family: Tahoma; color: black; width: 100%;">
                    <center>
                             <div style="padding: 0px; padding: 2px; border-top: 0; background-color: #5588BB;
                                color: #FFFFFF; text-align: center;">
                                <b>View Attachments</b>
                            </div>
                     </center>
                    <asp:DataList ID="grdPTR_Attachment" runat="server" AllowSorting="False" AutoGenerateColumns="false"
                        RepeatDirection="Horizontal" RepeatColumns="5" GridLines="None" CellPadding="3" Visible="false"
                        CellSpacing="1" Width="100%" CssClass="GridView-css">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkAttachment" runat="server" Text='<%# Eval("File_Name") %>'
                                NavigateUrl='<%# "~/Uploads/PTR/" + Eval("File_Path").ToString()%>' Target="_blank"
                                Visible='<%#Eval("File_Path").ToString()==""?false:true%>' />
                        </ItemTemplate>
                        <ItemStyle Width="10px" HorizontalAlign="Left" Wrap="false" />
                    </asp:DataList>
                </div>
            </div>--%>
                <center>
                             <div style="padding: 0px; padding: 2px; border-top: 0; background-color: #5588BB;font-family: Tahoma; color: black;  
                                color: #FFFFFF; text-align: center;">
                                <b>Mooring Plan Diagram</b>
                            </div>
                            <table style="width:100%;border-color:Black;border-style:solid;border-width:1px">
                            <tr>
                            <td style="vertical-align:top;width:270px;overflow:auto">
                            <div style="height: 560px; width: 270px; overflow: Auto">
             <asp:DataList ID="imgListLeft" runat="server" AllowSorting="False" AutoGenerateColumns="false"
                        RepeatDirection="Vertical" RepeatColumns="1" GridLines="None" CellPadding="3"
                        CellSpacing="1" Width="250px" CssClass="GridView-css">
                        <ItemTemplate>
                            <asp:Image ID="lnkAttachment" runat="server" Text='<%# Eval("File_Name") %>' 
                            onclick='<%#"openFile(&#39;"+"../Uploads/PTR/"+Eval("File_Path").ToString()+"&#39;);"  %>' 
                            style="cursor:pointer"
                                NavigateUrl='<%# "~/Uploads/PTR/" + Eval("File_Path").ToString()%>' Target="_blank" ImageUrl='<%# GetImage("~/Uploads/PTR/" + Eval("File_Path").ToString()) %>'
                                Visible='<%#Eval("File_Path").ToString()==""?false:true%>'  Width='240px' />
                        </ItemTemplate>
                        <ItemStyle Width="10px" HorizontalAlign="Center" Wrap="false" VerticalAlign="Middle" />
                    </asp:DataList>
        </div>
                           
                            </td>
                            <td style="vertical-align:top">
                              <asp:Image ID="imgMooringPlan" runat="server"     BorderStyle="Solid" BorderWidth="1px"  Width="600px" Height="550px"/>
                            </td>
                              <td style="vertical-align:top;width:270px;overflow:auto">
                            <div style="height: 560px; width: 270px; overflow: Auto">
               <asp:DataList ID="imgListRight" runat="server" AllowSorting="False" AutoGenerateColumns="false"
                        RepeatDirection="Vertical" RepeatColumns="1" GridLines="None" CellPadding="3"
                        CellSpacing="1" Width="250px" CssClass="GridView-css">
                        <ItemTemplate>
                            <asp:Image ID="lnkAttachment" runat="server" Text='<%# Eval("File_Name") %>'
                            style="cursor:pointer"
                             onclick='<%#"openFile(&#39;"+"../Uploads/PTR/"+Eval("File_Path").ToString()+"&#39;);"  %>' 
                                NavigateUrl='<%# "~/Uploads/PTR/" + Eval("File_Path").ToString()%>' Target="_blank" ImageUrl='<%# "~/Uploads/PTR/" + Eval("File_Path").ToString()%>'
                                Visible='<%#Eval("File_Path").ToString()==""?false:true%>'    Width='240px' />
                        </ItemTemplate>
                        <ItemStyle Width="10px" HorizontalAlign="Center" Wrap="false" VerticalAlign="Middle" />
                    </asp:DataList>
        </div>

                           
                            </tr>
                            </table>
                               
                                 <br />
                               <table style="width:100%">
                                 <tr  class='leafTR'>
                <td class='leafTD-header' colspan="3" style="background-color: #99ccff;">Vessel Remarks</td>
            </tr>
            <tr  class='leafTR'>
                <td style="width: 100%; height: 120px; background-color: #cce499" colspan="3" >
                <asp:TextBox ID="txtRemarks" runat="server" BackColor="#cce499" BorderStyle="None"
                                                            ForeColor="Black" Height="100%" TextMode="MultiLine"
                                                            Width="100%" Enabled="false"> </asp:TextBox>
                </td>
            </tr>
                               </table>
                     </center>
            </div>
            <asp:UpdatePanel ID="UpdatePnl" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                </ContentTemplate>
            </asp:UpdatePanel>
    </form>
</body>
</html>
