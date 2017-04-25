<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Crew_Retention_Category.aspx.cs" Inherits="KPI_Crew_Retantion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/jscript"></script>
    <link href="../Styles/tooltipsytle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <link href="../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/SMSPopup.js" type="text/javascript"></script>
    <link href="~/Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
   
  
    <style type="text/css">
        .style1
        {
            width: 1%;
        }
    </style>
   
  
</head>
<body style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 95%;">
     


    <form id="form1" runat="server">

    <div id="dvContent" style="text-align: center; border: 1px solid #5588BB; "  >
        <center>
        <asp:ScriptManager ID="ScriptManager1" runat="server">        </asp:ScriptManager>
            <asp:UpdateProgress ID="upUpdateProgress" runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                    <img src="../Images/loaderbar.gif" alt="Please Wait" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <center>
        <div id="dialog" style="display: none"></div>
           <div style="border: 1px solid #cccccc" class="page-title">
                <asp:Literal ID="ltTitle" runat="server"></asp:Literal>
            </div>
            <table border="0" cellpadding="2" cellspacing="2" width="90%">
            <tr>
            <td align="center">
             <asp:Label ID="lblformula" Font-Bold="true" ForeColor="Blue" runat="server"></asp:Label>
            </td>
            </tr>

            <tr>
            <td >
                <asp:GridView ID="gvCategory" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                   CellPadding="1" CellSpacing="0" Width="100%"  DataKeyNames="ID"
                   CssClass="gridmain-css" AllowSorting="true" >
                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                            <PagerStyle CssClass="PMSPagerStyle-css" />
                            <SelectedRowStyle BackColor="#FFFFCC" />
                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                <Columns>

                                <asp:BoundField HeaderText="Quarter" DataField="Qtr" ItemStyle-Width="10%"  HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField HeaderText="Employed Crew(PI06)" DataField="AvgAvailable"  ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField HeaderText="NTBR(PI16)" DataField="NTBR"  ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField HeaderText="Total Left(PI41)" DataField="LeftAll" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Retention Rate" DataField="KPI_Value"  ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>

                                </Columns>
                            </asp:GridView>
            
            
            </td>

                </tr>


            </table>

        </center>
    </div>


    </form>
</body>
</html>
