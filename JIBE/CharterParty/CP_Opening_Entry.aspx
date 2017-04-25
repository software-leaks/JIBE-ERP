<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CP_Opening_Entry.aspx.cs" Inherits="CP_Opening_Entry" %>

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
     
    <script language="javascript" type="text/javascript">

        function validation() {

            if (document.getElementById("txtName").value == "") {
                  alert("Please Enter Opening Title.");
                document.getElementById("txtName").focus();
                    return false;
                
            }
            if (document.getElementById("txtDescription").value == "") {
                alert("Please Enter Opening Description.");
                document.getElementById("txtDescription").focus();
                return false;

            }


            if (document.getElementById("<%= chkVessel.ClientID %>") == null && document.getElementById("ddlVessel").value == "0") {
                    alert("Please check atleast one vessel");
                    return false

                }



            return true

        }



        function validateddlVessel() {
            if (document.getElementById("ddlVessel").value == "0") {
                alert("Please select a Vessel to add.");
                return false;
            }
            return true;
        }

    </script>

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
                Opening Details
            </div>
            <table border="0" cellpadding="2" cellspacing="2" width="800px">

                <tr>
                    <td align="right" style="width: 39%">
                        Opening Name &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left" style="width: 50%">
                        <asp:TextBox ID="txtName" runat="server" Width="250px" MaxLength="250" CssClass="txtInput"> </asp:TextBox>
                    </td>
                    <td align="right" style="width: 10%">
                        &nbsp;
                    </td>
                </tr>

                   <tr>
                    <td align="right" style="width: 39%">
                        Charterer &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
  
                    </td>
                    <td align="left" style="width: 50%">
                        <asp:TextBox ID="txtCharter" runat="server" Width="250px" MaxLength="250" CssClass="txtInput"> </asp:TextBox>
                    </td>
                    <td align="right" style="width: 10%">
                        &nbsp;
                    </td>
                </tr>

                 <tr>
                    <td align="right" style="width: 39%">
                        Broker &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
  
                    </td>
                    <td align="left" style="width: 50%">
                        <asp:TextBox ID="txtBroker" runat="server" Width="250px" MaxLength="250" CssClass="txtInput"> </asp:TextBox>
                    </td>
                    <td align="right" style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
             <tr>
                    <td align="right" style="width: 39%">
                        Contact Name &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
  
                    </td>
                    <td align="left" style="width: 50%">
                        <asp:TextBox ID="txtContactName" runat="server" Width="250px" MaxLength="250" CssClass="txtInput"> </asp:TextBox>
                    </td>
                    <td align="right" style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
             <tr>
                    <td align="right" style="width: 39%">
                        Contact Email/Skype &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
  
                    </td>
                    <td align="left" style="width: 50%">
                        <asp:TextBox ID="txtContactEmail" runat="server" Width="250px" MaxLength="250" CssClass="txtInput"> </asp:TextBox>
                    </td>
                    <td align="right" style="width: 10%">
                        &nbsp;
                    </td>
                </tr>



             <tr>
                    <td align="right" style="width: 39%">
                        Contact Phone &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
  
                    </td>
                    <td align="left" style="width: 50%">
                        <asp:TextBox ID="txtContactPhone" runat="server" Width="250px" MaxLength="250" CssClass="txtInput"> </asp:TextBox>
                    </td>
                    <td align="right" style="width: 10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 39%">
                        Progress &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left" style="width: 50%">
                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" Width="300px" MaxLength="2000" runat="server"></asp:TextBox>
                         </td>
                    <td  style="width: 10%"></td>
                </tr>

                <tr>
                    <td align="right" style="width: 39%">
                        Status &nbsp;:&nbsp;
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                    </td>
                    <td align="left" style="width: 50%">
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="100px" CssClass="txtInput">
                        <asp:ListItem Text="Enquiry" Value="Enquiry"></asp:ListItem>
                        <asp:ListItem Text="Talking Stage" Value="Response" ></asp:ListItem>
                        <asp:ListItem Text="Working Firm" Value="Negotiation"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approval"></asp:ListItem>
                        <asp:ListItem Text="Fixed" Value="Fixed"></asp:ListItem>
                        <asp:ListItem Text="Suspend" Value="Suspend"></asp:ListItem>
                        <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 10%">
                    </td>
                </tr>

                  <tr>

                    <td style="text-align: Right;">
                      Identify Vessel :
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                        *
                    </td>
                    <td align="left">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div>
                        <asp:DropDownList ID="ddlVessel" runat="server" Width="200px" CssClass="txtInput">
                        </asp:DropDownList>
                        <asp:Button ID="btnVesselAdd" runat="server" Text="Add Vessel" OnClientClick="return validateddlVessel();"
                                        onclick="btnVesselAdd_Click" />

                        <br />
                                             
                                <div id="dvVesselList" runat="server"  style="float: left; text-align: left; width: 400px; height: 40px;
                                    border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; overflow: Auto;
                                    background-color: #ffffff;">
                                    <asp:CheckBoxList ID="chkVessel"  RepeatLayout="Flow" RepeatDirection="Horizontal"
                                        runat="server">
                                    </asp:CheckBoxList>
                                </div>
                                                   
                        </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td></td>
                  
                  </tr>

                <tr>
                    <td align="right" style="width: 39%">
                    </td>
                    <td style="color: #FF0000; " align="right" class="style1">
                    
                    </td>
                    <td align="left">
                            <asp:HiddenField ID="hdProgressID" runat="server" />
                    </td>
                    <td align="right">
                    </td>
                </tr>
                <tr>
               
                    <td colspan="4" align="left" style="color: #FF0000; font-size: small;">
                        * Mandatory fields &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                <td colspan="4" align="center" style="color: #FF0000; font-size: small;">
                        <asp:Label ID="lbl1" runat="server" Text=""></asp:Label>
                    </td>
                   
                </tr>
            </table>
                    <div style="background-color: #d8d8d8; text-align: center">
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnsave" runat="server" OnClientClick="return validation();"
                            Text="Save" OnClick="btnsave_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Label ID="lblMessage" Style="color: #FF0000;" runat="server"></asp:Label>
        </div>
        <div>
        <asp:GridView ID="gvOpenings" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False" AllowPaging="true" PageSize="5"
                                CellPadding="1" CellSpacing="0" Width="100%" GridLines="both" 
                                CssClass="gridmain-css" AllowSorting="true"  >
                                <HeaderStyle CssClass="HeaderStyle-css" />
                                <RowStyle CssClass="RowStyle-css" />
                                <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                <Columns>

                                <asp:BoundField HeaderText="Status" DataField="Status" ItemStyle-Width="8%" />
                                <asp:BoundField HeaderText="Opening" DataField="Opening"  ItemStyle-Width="8%"/>
                                <asp:BoundField HeaderText="Charterer" DataField="Charterer_Name" ItemStyle-Width="10%" />
                                <asp:BoundField HeaderText="Broker" DataField="Broker_Name"  ItemStyle-Width="8%"/>
                                <asp:BoundField HeaderText="Contact" DataField="Contact_Name"  ItemStyle-Width="10%"/>
                                <asp:BoundField HeaderText="Remark(Progress)" DataField="Progress_Remarks"  ItemStyle-Width="30%"/>
                                <asp:BoundField HeaderText="Updated On" DataField="Remarks_Date" ItemStyle-Width="10%"/>
                                <asp:BoundField HeaderText="Updated By" DataField="Updated_By" ItemStyle-Width="10%"/>




                                    <asp:TemplateField >
                                        <HeaderTemplate>
                                         Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td>
                                                            <asp:ImageButton ID="ibtnDelete" runat="server" CommandArgument='<%#Eval("Progress_ID") %>'
                                                                    Visible="true" ImageUrl="~/images/delete.png" OnCommand="ibtnDelete_click"
                                                                    OnClientClick="return confirm('Are you sure want to delete?')" ToolTip="ibtnDelete_click">
                                                                </asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="true" HorizontalAlign="Center" Width="4%" CssClass="PMSGridItemStyle-css">
                                        </ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="False" />
                        </div>

        </center>
    </div>


    </form>
</body>
</html>
