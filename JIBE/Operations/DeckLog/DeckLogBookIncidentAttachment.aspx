<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeckLogBookIncidentAttachment.aspx.cs"
    Inherits="DeckLogBookIncidentAttachment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/SMSPopup.js" type="text/javascript"></script>
    <script src="../../Scripts/Deck_Engine_LogBook.js" type="text/javascript"></script>
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">



    </script>
    <style type="text/css">
        .CellClass1
        {
            background-color: #FA5858;
            color: White;
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        .CellClass0
        {
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        
        .CellClassChangeFlage1
        {
            background-color: Yellow;
            color: Black;
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        
        .CellClassChangeFlage0
        {
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        
        .HeaderCellColor
        {
            background-color: #3399CC;
            color: White;
        }
        
        .FieldDottedLine
        {
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            border-bottom: 1px dashed;
        }
        .CreateHtmlTableFromDataTable-table
        {
            background-color: #FFFFFF;
            border: 0px solid #FFB733;
        }
        
        .CreateHtmlTableFromDataTable-PageHeader
        {
            background-color: #F6B680;
        }
        
        .CreateHtmlTableFromDataTable-DataHedaer
        {
            background-color: #CCCCCC;
            border: 1px solid gray;
            text-align: center;
        }
        .CreateHtmlTableFromDataTable-Data
        {
            border: 1px solid gray;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

     <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 710;
                color: black">
                <img src="../../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div style="font-family: Tahoma; font-size: 12px; border: 1px solid gray; height: 700px;
        width: 100%; vertical-align: middle;">
        <table cellspacing="0" cellpadding="0" rules="all" width="100%">
            <tr>
                <td class="HeaderCellColor" align="center" colspan="5">
                    <div style="background-color: #5588BB; color: #FFFFCC; text-align: center; height: 20px;">
                        <b>Incident Attachments</b>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="5" class="CellClass0" style="height: 8px;">
                </td>
            </tr>
            <tr>
                <td align="right" class="CellClass0">
                    Incident Date :&nbsp;
                </td>
                <td class="CellClass0">
                    <asp:Label ID="lblIncidentDate" Width="303" CssClass="txtReadOnly" runat="server"></asp:Label>
                </td>
                <td align="right" class="CellClass0">
                    Incident Type :&nbsp;
                </td>
                <td class="CellClass0">
                    <asp:Label ID="lblIncidentType" Width="303" CssClass="txtReadOnly" runat="server"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right" class="CellClass0">
                    Details :&nbsp;
                </td>
                <td class="CellClass0">
                    <asp:TextBox ID="txtIncidentDetails" runat="server" TextMode="MultiLine" CssClass="txtReadOnly"
                        ReadOnly="true" Height="60" Width="300"></asp:TextBox>
                </td>
                <td align="right" class="CellClass0">
                    Action taken :&nbsp;
                </td>
                <td class="CellClass0">
                    <asp:TextBox ID="txtActionTaken" runat="server" TextMode="MultiLine" ReadOnly="true"
                        CssClass="txtReadOnly" Height="60" Width="300"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5" class="CellClass0" style="height: 12px;">
                </td>
            </tr>
        </table>
        <div style="float: left; border: 0px solid gray; width: 99%; margin-top: 10px; margin-left: 2px;
            font-size: 10px; background-color: #ffffff;">
            <table style="width: 100%">
                <tr>
                    <td style='width: 300px; border: 1px solid #cccccc; background-color: white; vertical-align: top;'>
                        <asp:UpdatePanel ID="UpdatePanel_Left" runat="server">
                            <ContentTemplate>
                                <div>
                                    <asp:Repeater runat="server" ID="rptDrillImages" OnItemCommand="rptDrillImages_ItemCommand"
                                        OnItemDataBound="rptDrillImages_ItemDataBound">
                                        <HeaderTemplate>
                                            <table style="width: 100%" cellpadding="2" cellspacing="0">
                                                <tr style="color: Black; background-color: #0B4C5F">
                                                    <td colspan="4" style="font-weight: bold; color: White;">
                                                        Attachments:
                                                    </td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr style="color: Black">
                                                <td style="width: 20px">
                                                    <asp:Image ID="imgDocIcon" ImageUrl="~/Images/DocTree/TXT.gif" runat="server" Height="18px" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDateCr" runat="server" Text='<%#Eval("DATE_OF_CREATION") %>'></asp:Label>
                                                </td>
                                                <td style="padding-left: 2px; width: 150px; text-align: left;">
                                                    <asp:LinkButton ID="lnkAttachment" runat="server" Text='<%#Eval("ATTACHMENT_NAME") %>'
                                                        CommandName="ViewDocument" CommandArgument='<%#Eval("PhotoUrl") %>'></asp:LinkButton>
                                                </td>
                                                <td style="padding-left: 5px; width: 60px; text-align: right;">
                                                    <asp:Label ID="lblSize" runat="server" Text='<%# Eval("FileSize","{0:0.00}").ToString() + " KB" %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr style="color: Black; background-color: #E0ECF8">
                                                <td style="width: 20px">
                                                    <asp:Image ID="imgDocIcon" ImageUrl="~/Images/DocTree/TXT.gif" runat="server" Height="18px" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDateCr" runat="server" Text='<%#Eval("DATE_OF_CREATION") %>'></asp:Label>
                                                </td>
                                                <td style="padding-left: 2px; width: 150px; text-align: left;">
                                                    <asp:LinkButton ID="lnkAttachment" runat="server" Text='<%#Eval("ATTACHMENT_NAME") %>'
                                                        CommandName="ViewDocument" CommandArgument='<%#Eval("PhotoUrl") %>'></asp:LinkButton>
                                                </td>
                                                <td style="padding-left: 5px; width: 60px; text-align: right;">
                                                    <asp:Label ID="lblSize" runat="server" Text='<%# Eval("FileSize","{0:0.00}").ToString() + " KB" %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                        <FooterTemplate>
                                            </table></FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style='border: 1px solid #cccccc; background-color: white; vertical-align: top;'>
                        <asp:UpdatePanel ID="UpdatePanel_Frame" runat="server">
                            <ContentTemplate>
                                <iframe id="frmContract" src="" runat="server" style="width: 100%; height: 500px;
                                    border: 0;"></iframe>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
