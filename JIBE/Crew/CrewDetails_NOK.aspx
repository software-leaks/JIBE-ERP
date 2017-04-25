<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewDetails_NOK.aspx.cs"
    Inherits="Crew_CrewDetails_NOK" %>

<%@ Register Assembly="AjaxControlToolkit4" Namespace="AjaxControlToolkit4" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/CrewDetails.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.balloon.js" type="text/javascript"></script>
    <script src="../Scripts/boxover.js" type="text/javascript"></script>
    <script src="../Scripts/notifier.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="src1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hdnNOKID" runat="server" />
    <div id="dvCrewNOKAndDependents">
        <asp:UpdatePanel ID="upd1" runat="server">
            <ContentTemplate>
                <div class="error-message" onclick="javascript:this.style.display='none';">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </div>
                <asp:Panel ID="pnlView_NextOfKin" runat="server" Visible="false">
                    <fieldset>
                        <legend>&nbsp;&nbsp;Next of Kin&nbsp;<asp:LinkButton ID="lnkEditNOK" runat="server"
                            CssClass="inline-edit"><font color="blue">[Edit]</font></asp:LinkButton></legend>
                        <table id="tdU" class="dataTable" runat="server" style="vertical-align: top; width: 63%;">
                            <tr>
                                <td style="width: 100px">
                                    First Name
                                </td>
                                <td class="data" style="width: 300px">
                                    <asp:Label ID="lblNOKName" runat="server" Width="200px" CssClass="toolTipText"></asp:Label>
                                </td>
                                <td style="width: 10px">
                                </td>
                                <td style="width: 50px">
                                    Surname
                                </td>
                                <td class="data" style="width: 50px">
                                    <asp:Label ID="lblSurname" CssClass="toolTipText" runat="server" Width="200px">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px">
                                    Relationship
                                </td>
                                <td class="data" style="width: 100px">
                                    <asp:Label ID="lblNOKrelationship" runat="server" Width="200px"></asp:Label>
                                </td>
                                <td style="width: 10px">
                                </td>
                                <td style="width: 50px">
                                    Phone Number
                                </td>
                                <td class="data" style="width: 100px">
                                    <asp:Label ID="lblNOKPhone" runat="server" Width="200px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px" runat="server" id="tdlblSSN1">
                                    <asp:Label Text="SSN" runat="server" ID="lblUSSSN"></asp:Label>
                                </td>
                                <td class="data" style="width: 100px" runat="server" id="tdlblSSN2">
                                    <asp:Label ID="lblSSN" runat="server" Width="200px"></asp:Label>
                                </td>
                                <td id="tdblank" runat="server" style="width: 10px">
                                </td>
                                <td style="width: 50px">
                                    Date Of Birth
                                </td>
                                <td class="data" style="width: 100px">
                                    <asp:Label ID="lblDOB" runat="server" Width="200px"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height: 20px;">
                                <td colspan="5">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px">
                                    Address Line 1
                                </td>
                                <td class="data" style="width: 300px">
                                    <asp:Label ID="lblNOKAddress1" CssClass="toolTipText" TextMode="MultiLine" runat="server"
                                        Width="200px"></asp:Label>
                                </td>
                                <td style="width: 10px">
                                </td>
                                <td id="Td1" runat="server" style="width: 50px">
                                    Address Line 2
                                </td>
                                <td id="Td2" class="data" runat="server" style="width: 300px">
                                    <asp:Label ID="lblNOKAddress2" class="toolTipText" TextMode="MultiLine" runat="server"
                                        Width="200px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px">
                                    City
                                </td>
                                <td class="data" style="width: 100px">
                                    <asp:Label ID="lblCity" class="toolTipText" runat="server" Width="200px"></asp:Label>
                                </td>
                                <td style="width: 10px">
                                </td>
                                <td style="width: 150px">
                                    State / Province / Region
                                </td>
                                <td class="data" style="width: 50px">
                                    <asp:Label ID="lblState" class="toolTipText" runat="server" Width="200px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px">
                                    Country
                                </td>
                                <td class="data" style="width: 100px">
                                    <asp:Label ID="lblCountry" runat="server" Width="200px"></asp:Label>
                                </td>
                                <td style="width: 10px">
                                </td>
                                <td style="width: 50px">
                                    Zip Code
                                </td>
                                <td class="data" style="width: 100px">
                                    <asp:Label ID="lblZipCode" class="toolTipText" runat="server" Width="200px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table id="tdI" class="dataTable" runat="server" style="vertical-align: top; width: 65%;">
                            <tr>
                                <td style="width: 100px">
                                    First Name
                                </td>
                                <td class="data" style="width: 300px">
                                    <asp:Label ID="lblNOKName1" class="toolTipText" runat="server" Width="200px"></asp:Label>
                                </td>
                                <td style="width: 10px">
                                </td>
                                <td style="width: 100px">
                                    Surname
                                </td>
                                <td class="data" style="width: 100px">
                                    <asp:Label ID="lblSurname1" class="toolTipText" runat="server" Width="200px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Relationship
                                </td>
                                <td class="data" style="width: 100px">
                                    <asp:Label ID="lblRelationship1" runat="server" Width="200px"></asp:Label>
                                </td>
                                <td style="width: 10px">
                                </td>
                                <td style="width: 100px">
                                    Phone Number
                                </td>
                                <td class="data" style="width: 100px">
                                    <asp:Label ID="lblPhone1" runat="server" Width="200px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    Date Of Birth
                                </td>
                                <td class="data" style="width: 100px">
                                    <asp:Label ID="lblDoB1" runat="server" Width="200px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 100px">
                                    Address
                                </td>
                                <td class="data" style="width: 300px">
                                    <asp:Label ID="lblAddress" class="toolTipText" TextMode="MultiLine" runat="server"
                                        Width="200px"></asp:Label>
                                </td>
                                <td id="tdblank2" runat="server" style="width: 10px">
                                </td>
                                <td valign="top" style="width: 100px" runat="server" id="tdlblSSNI1">
                                    SSN
                                </td>
                                <td valign="top" class="data" style="width: 300px" runat="server" id="tdlblSSNI2">
                                    <asp:Label ID="lblSSNI" TextMode="MultiLine" runat="server" Width="200px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset style="margin-top: 10px;">
                        <legend>&nbsp;&nbsp;Dependents/Beneficiaries </legend>
                        <asp:GridView ID="GridView_Dependents" Style="margin-top: 5px;" runat="server" DataKeyNames="ID"
                            AutoGenerateColumns="False" GridLines="None" Width="63%" CssClass="GridView-css"
                            EmptyDataText="No Dependents/Benefeciaries Records Found" OnRowDataBound="GridView_Dependents_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Name" SortExpression="FirstName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFullName" runat="server" Text='<%#Bind("Fullname")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="200px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Relationship" HeaderStyle-HorizontalAlign="Center"
                                    SortExpression="Relationship">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRelationship" runat="server" Text='<%#Bind("Relationship")%>'></asp:Label>
                                        <asp:Label ID="lblIsNOK" runat="server" Visible="false" Text='<%#Bind("IsNOK")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Beneficiary?" HeaderStyle-HorizontalAlign="Center"
                                    SortExpression="Relationship">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBeneficiary" runat="server" Text='<%# Convert.ToBoolean(Eval("IsBeneficiary"))?"Yes":"No" %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False" HeaderText="Action" HeaderStyle-HorizontalAlign="Center">
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="LinkButton1" runat="server" ImageUrl="~/images/accept.png" CausesValidation="True"
                                            CommandName="Update" AlternateText="Update" ValidationGroup="noofdays"></asp:ImageButton>
                                        <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/images/reject.png" CausesValidation="False"
                                            CommandName="Cancel" AlternateText="Cancel"></asp:ImageButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="LinkButton2" runat="server" ImageUrl="~/images/edit.gif" CausesValidation="False"
                                            AlternateText="Edit" OnClientClick='<%#"EditDependent("+ Eval("CrewID").ToString() + "," + Eval("ID").ToString() +"); return false;" %>'>
                                        </asp:ImageButton>
                                        <asp:ImageButton ID="LinkButton1del" runat="server" ImageUrl="~/images/delete.png"
                                            CausesValidation="False" OnClientClick='<%#"DeleteCrewDependent(" + Eval("ID").ToString() + "," + Eval("CrewID").ToString() + "," + Session["UserID"].ToString() +"); return false;" %>'
                                            AlternateText="Delete"></asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle-css" />
                            <PagerStyle CssClass="PagerStyle-css" />
                            <RowStyle CssClass="RowStyle-css" />
                            <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                            <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                            <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                            <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                            <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                            <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                        </asp:GridView>
                    </fieldset>
                </asp:Panel>
                <asp:Panel ID="pnlAddEdit_NextOfKin" runat="server" Visible="false">
                    <table cellpadding="3" class="dataTable" id="tblUS" runat="server" width="100%">
                        <tr>
                            <td style="width: 80px">
                                First Name
                                <asp:Label ID="lbl1" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:TextBox ID="txtFirstName" runat="server" Width="200px" MaxLength="100" CssClass="required"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqFV1" runat="server" ErrorMessage="First Name is required"
                                    Display="None" ForeColor="Red" ControlToValidate="txtFirstName" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="fltName" runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom"
                                    TargetControlID="txtFirstName" ValidChars=" ">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                            <td style="width: 130px">
                                Surname
                                <asp:Label ID="lbl2" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td style="width: 200px">
                                <asp:TextBox ID="txtSurname" runat="server" Width="200px" MaxLength="100" CssClass="required"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Surname is required"
                                    Display="None" ForeColor="Red" ControlToValidate="txtSurname" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                    FilterType="UppercaseLetters,LowercaseLetters,Custom" TargetControlID="txtSurname"
                                    ValidChars=" ">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Relationship
                                <asp:Label ID="lbl3" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlNOKRelationship" runat="server" Width="205px" CssClass="required">
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="FATHER" Value="FATHER"></asp:ListItem>
                                    <asp:ListItem Text="MOTHER" Value="MOTHER"></asp:ListItem>
                                    <asp:ListItem Text="WIFE" Value="WIFE"></asp:ListItem>
                                    <asp:ListItem Text="SON" Value="SON"></asp:ListItem>
                                    <asp:ListItem Text="DAUGHTER" Value="DAUGHTER"></asp:ListItem>
                                    <asp:ListItem Text="BROTHER" Value="BROTHER"></asp:ListItem>
                                    <asp:ListItem Text="SISTER" Value="SISTER"></asp:ListItem>
                                    <asp:ListItem Text="BROTHER-IN-LAW" Value="BROTHER-IN-LAW"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="0" ID="Req_ID" ValidationGroup="vgSubmit"
                                    runat="server" ControlToValidate="ddlNOKRelationship" Display="None" ErrorMessage="Relationship is required"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Phone Number
                                <asp:Label ID="lbl4" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPhone" runat="server" Width="200px" CssClass="required" MaxLength="50"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender2"
                                    TargetControlID="txtPhone" FilterType="Numbers,Custom" ValidChars="+,-">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Phone number is required"
                                    Display="None" ForeColor="Red" ControlToValidate="txtPhone" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td runat="server" id="tdtxtSSN1">
                                <asp:Label Text="SSN" runat="server" ID="lblSSNUS"></asp:Label>
                                <asp:Label ID="Label3" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td runat="server" id="tdtxtSSN2">
                                <asp:TextBox ID="txtSSN" ClientIDMode="Static" runat="server" Width="200px" ToolTip="The Social Security number is a nine-digit number in the format 'AAA-GG-SSSS'"
                                    MaxLength="11" CssClass="required"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtSSN"
                                    ErrorMessage="Enter Valid  SSN" ValidationExpression="^\d{3}-?\d{2}-?\d{4}$"
                                    ValidationGroup="vgSubmit" Display="None" Width="200px"></asp:RegularExpressionValidator>
                                <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender7"
                                    TargetControlID="txtSSN" FilterType="Numbers,Custom" ValidChars="-">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                            <td>
                                Date Of Birth
                                <asp:Label ID="lblDOBstar" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDOB" ClientIDMode="Static" runat="server" Width="200px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtDOB">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAdd1" runat="server" Text="Address Line 1"></asp:Label><asp:Label
                                    ID="lbl6" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress1" runat="server" Width="200px" MaxLength="100" CssClass="required"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Address Line 1 is required"
                                    Display="None" ForeColor="Red" ControlToValidate="txtAddress1" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                            <td id="Td3" runat="server">
                                <asp:Label ID="lblAdd2" runat="server" Text="Address Line 2"></asp:Label>
                            </td>
                            <td id="Td4" runat="server">
                                <asp:TextBox ID="txtAddress2" runat="server" Width="200px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                City
                                <asp:Label ID="lbl7" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCity" runat="server" Width="200px" MaxLength="100" CssClass="required"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="City is required"
                                    Display="None" ForeColor="Red" ControlToValidate="txtCity" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                State / Province / Region
                                <asp:Label ID="lbl11" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtState" runat="server" Width="200px" MaxLength="100" CssClass="required"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="State is required"
                                    Display="None" ForeColor="Red" ControlToValidate="txtState" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Country
                                <asp:Label ID="lbl8" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlCountry" Width="205px" CssClass="required">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator1" ValidationGroup="vgSubmit"
                                    runat="server" ControlToValidate="ddlCountry" Display="None" ErrorMessage="Country is required"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Zip Code
                                <asp:Label ID="lbl12" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtZip" runat="server" Width="200px" MaxLength="50" CssClass="required"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Zip code is required"
                                    Display="None" ForeColor="Red" ControlToValidate="txtZip" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <center>
                                    <table>
                                        <tr>
                                            <td>
                                                Mark as Beneficiary?
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdbBeneficiary" ClientIDMode="Static" runat="server" RepeatDirection="Horizontal"
                                                    RepeatLayout="Table">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </center>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: Center">
                                <asp:Button ID="btnSaveNOKDetails" runat="server" Text=" Save " OnClick="btnSaveNOKDetails_Click"
                                    ClientIDMode="Static" ValidationGroup="vgSubmit" />
                                <asp:Button ID="btnCloseNOKDetails" runat="server" OnClick="btnSaveAndCloseNOKDetails_Click"
                                    Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                    <table class="dataTable" cellpadding="3" id="tblInternational" runat="server" width="100%">
                        <tr>
                            <td style="width: 100px">
                                First Name
                                <asp:Label ID="lbl13" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtFirstName1" runat="server" Width="200px" MaxLength="100" CssClass="required"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="First Name is required"
                                    Display="None" ForeColor="Red" ControlToValidate="txtFirstName1" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                    FilterType="UppercaseLetters,LowercaseLetters,Custom" TargetControlID="txtFirstName1"
                                    ValidChars=" ">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                            <td style="width: 100px">
                                Surname
                                <asp:Label ID="lbl17" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtSurname1" runat="server" Width="200px" MaxLength="100" CssClass="required"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Surname is required"
                                    Display="None" ForeColor="Red" ControlToValidate="txtSurname1" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                    FilterType="UppercaseLetters,LowercaseLetters,Custom" TargetControlID="txtSurname1"
                                    ValidChars=" ">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Relationship
                                <asp:Label ID="lbl14" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRelationShip1" runat="server" Width="200px" CssClass="required">
                                    <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="FATHER" Value="FATHER"></asp:ListItem>
                                    <asp:ListItem Text="MOTHER" Value="MOTHER"></asp:ListItem>
                                    <asp:ListItem Text="WIFE" Value="WIFE"></asp:ListItem>
                                    <asp:ListItem Text="SON" Value="SON"></asp:ListItem>
                                    <asp:ListItem Text="DAUGHTER" Value="DAUGHTER"></asp:ListItem>
                                    <asp:ListItem Text="BROTHER" Value="BROTHER"></asp:ListItem>
                                    <asp:ListItem Text="SISTER" Value="SISTER"></asp:ListItem>
                                    <asp:ListItem Text="BROTHER-IN-LAW" Value="BROTHER-IN-LAW"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator12" ValidationGroup="vgSubmit"
                                    runat="server" ControlToValidate="ddlRelationShip1" Display="None" ErrorMessage="RelationShip is required"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                Phone Number
                                <asp:Label ID="lbl18" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPhone1" runat="server" Width="200px" CssClass="required" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Phone number is required"
                                    Display="None" ForeColor="Red" ControlToValidate="txtPhone1" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender3"
                                    TargetControlID="txtPhone1" FilterType="Numbers,Custom" ValidChars="+,-">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Date Of Birth<asp:Label ID="lblDobStar2" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDOB1" runat="server" Width="200px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtDOB1">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Address
                                <asp:Label ID="lbl19" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddressInternational" runat="server" TextMode="MultiLine" Width="200px"
                                    MaxLength="250" Height="70px" CssClass="required"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Address is required"
                                    Display="None" ForeColor="Red" ControlToValidate="txtAddressInternational" ValidationGroup="vgSubmit"></asp:RequiredFieldValidator>
                            </td>
                            <td runat="server" id="tdSSNI1">
                                SSN
                                <asp:Label ID="Label4" runat="server" CssClass="mandatory">*</asp:Label>
                            </td>
                            <td runat="server" id="tdSSNI2">
                                <asp:TextBox ID="txtSSNI" ClientIDMode="Static" runat="server" Width="200px" ToolTip="The Social Security number is a nine-digit number in the format 'AAA-GG-SSSS'"
                                    MaxLength="11" CssClass="required"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSSNI"
                                    ErrorMessage="Enter Valid  SSN" ValidationExpression="^\d{3}-?\d{2}-?\d{4}$"
                                    ValidationGroup="vgSubmit" Display="None" Width="200px"></asp:RegularExpressionValidator>
                                <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender8"
                                    TargetControlID="txtSSNI" FilterType="Numbers,Custom" ValidChars="-">
                                </ajaxToolkit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <center>
                                    <table>
                                        <tr>
                                            <td>
                                                Mark as Beneficiary?
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rdbBeneficiary1" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </center>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: Center">
                                <asp:Button ID="Button3" runat="server" Text=" Save " OnClick="btnSaveNOKDetails_Click"
                                    ValidationGroup="vgSubmit" ClientIDMode="Static" />
                                <asp:Button ID="Button4" runat="server" OnClick="btnSaveAndCloseNOKDetails_Click"
                                    Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PanelEditNOK" runat="server" Visible="false">
                    <fieldset style="text-align: left; margin: 0px; padding: 2px; font-family: Tahoma ,Tahoma, Sans-Serif,vrdana;
                        font-size: 12px;">
                        <legend>Next of Kin Details</legend>
                    </fieldset>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlAdd_Dependents" runat="server" Visible="false">
                    <fieldset>
                        <legend>Dependents Details</legend>
                        <table cellpadding="3" class="dataTable" runat="server" id="tblDepUS" width="100%">
                            <tr>
                                <td style="width: 100px">
                                    First Name <span class="mandatory">*</span>
                                </td>
                                <td style="width: 160px">
                                    <asp:TextBox ID="txtDepFirstName" runat="server" Width="200px" MaxLength="100" CssClass="required"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Enter First name"
                                        Display="None" ForeColor="Red" ControlToValidate="txtDepFirstName" ValidationGroup="vgSubmit">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server"
                                        FilterType="UppercaseLetters,LowercaseLetters,Custom" TargetControlID="txtDepFirstName"
                                        ValidChars=" ">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td style="width: 125px">
                                    Surname <span class="mandatory">*</span>
                                </td>
                                <td style="width: 100px">
                                    <asp:TextBox ID="txtDepSurname" runat="server" Width="200px" MaxLength="100" CssClass="required"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="Enter Surname"
                                        Display="None" ForeColor="Red" ControlToValidate="txtDepSurname" ValidationGroup="vgSubmit">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server"
                                        FilterType="UppercaseLetters,LowercaseLetters,Custom" TargetControlID="txtDepSurname"
                                        ValidChars=" ">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Relationship <span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDepRelationship" runat="server" Width="205px" CssClass="required">
                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="FATHER" Value="FATHER"></asp:ListItem>
                                        <asp:ListItem Text="MOTHER" Value="MOTHER"></asp:ListItem>
                                        <asp:ListItem Text="WIFE" Value="WIFE"></asp:ListItem>
                                        <asp:ListItem Text="SON" Value="SON"></asp:ListItem>
                                        <asp:ListItem Text="DAUGHTER" Value="DAUGHTER"></asp:ListItem>
                                        <asp:ListItem Text="BROTHER" Value="BROTHER"></asp:ListItem>
                                        <asp:ListItem Text="SISTER" Value="SISTER"></asp:ListItem>
                                        <asp:ListItem Text="BROTHER-IN-LAW" Value="BROTHER-IN-LAW"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator19" ValidationGroup="vgSubmit"
                                        runat="server" ControlToValidate="ddlDepRelationship" Display="None" ErrorMessage="Select Relationship"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    Phone Number<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDepPhone" runat="server" Width="200px" CssClass="required" MaxLength="50"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1"
                                        TargetControlID="txtDepPhone" FilterType="Numbers,Custom" ValidChars="+,-">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Enter Phone Number"
                                        Display="None" ForeColor="Red" ControlToValidate="txtDepPhone" ValidationGroup="vgSubmit">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Date Of Birth
                                    <asp:Label ID="lblDOBStar3" runat="server" CssClass="mandatory">*</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDepDOB" runat="server" Width="200px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtDepDOB">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td id="tdDepSSN1" runat="server">
                                    <asp:Label ID="lblSSNV" Text="SSN" runat="server"></asp:Label>
                                    <asp:Label ID="Label6" runat="server" CssClass="mandatory">*</asp:Label>
                                </td>
                                <td id="tdDepSSN2" runat="server">
                                    <asp:TextBox ID="txtDepSSN" ToolTip="The Social Security number is a nine-digit number in the format 'AAA-GG-SSSS'"
                                        runat="server" Width="200px" MaxLength="11" CssClass="required"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender9"
                                        TargetControlID="txtDepSSN" FilterMode="ValidChars" FilterType="Numbers,Custom"
                                        ValidChars="-">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDepSSN"
                                        Width="200px" ValidationGroup="vgSubmit" Display="None" ErrorMessage="Enter Valid SSN "
                                        ValidationExpression="^\d{3}-?\d{2}-?\d{4}$">
                                    </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr style="height: 20px;">
                                <td colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address Line 1<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDepAdd1" runat="server" Width="200px" MaxLength="100" CssClass="required"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="Enter Address Line 1"
                                        Display="None" ForeColor="Red" ControlToValidate="txtDepAdd1" ValidationGroup="vgSubmit">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td id="Td5" runat="server">
                                    Address Line 2
                                </td>
                                <td id="Td6" runat="server">
                                    <asp:TextBox ID="txtDepAdd2" runat="server" Width="200px" MaxLength="250"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    City<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDepCity" runat="server" Width="200px" MaxLength="100" CssClass="required"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="Enter City"
                                        Display="None" ForeColor="Red" ControlToValidate="txtDepCity" ValidationGroup="vgSubmit">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    State/ Province/ Region<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDepState" runat="server" Width="200px" MaxLength="100" CssClass="required"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="Enter State/ Province/ Region"
                                        Display="None" ForeColor="Red" ControlToValidate="txtDepState" ValidationGroup="vgSubmit">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Country<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlDepCountry" Width="205px" CssClass="required">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator20" ValidationGroup="vgSubmit"
                                        runat="server" ControlToValidate="ddlDepCountry" Display="None" ErrorMessage="Select Country"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    Zip Code<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDepZip" runat="server" Width="200px" MaxLength="50" CssClass="required"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ErrorMessage="Enter Zipcode"
                                        Display="None" ForeColor="Red" ControlToValidate="txtDepZip" ValidationGroup="vgSubmit">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <center>
                                        <table border="0">
                                            <tr>
                                                <td>
                                                    Mark as Beneficiary?
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdbDepBeneficiary" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: Center">
                                    <asp:Button ID="btnSaveDependent" runat="server" Text=" Save " OnClick="btnSaveDependents_Click"
                                        ValidationGroup="vgSubmit" ClientIDMode="Static" />
                                    <asp:Button ID="Button2" runat="server" OnClick="btnSaveAndCloseDependents_Click"
                                        Text="Cancel" />
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="3" class="dataTable" id="tblDepInternational" runat="server"
                            width="100%">
                            <tr>
                                <td style="width: 100px">
                                    First Name<span class="mandatory">*</span>
                                </td>
                                <td style="width: 160px">
                                    <asp:TextBox ID="txtDepFirstName1" CssClass="required" runat="server" Width="200px"
                                        MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="First name is required"
                                        Display="None" ForeColor="Red" ControlToValidate="txtDepFirstName1" ValidationGroup="vgSubmit">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server"
                                        FilterType="UppercaseLetters,LowercaseLetters,Custom" TargetControlID="txtDepFirstName1"
                                        ValidChars=" ">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td style="width: 125px">
                                    Surname<span class="mandatory">*</span>
                                </td>
                                <td style="width: 100px">
                                    <asp:TextBox ID="txtDepSurname1" CssClass="required" runat="server" Width="200px"
                                        MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ErrorMessage="Surname is required"
                                        Display="None" ForeColor="Red" ControlToValidate="txtDepSurname1" ValidationGroup="vgSubmit">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server"
                                        FilterType="UppercaseLetters,LowercaseLetters,Custom" TargetControlID="txtDepSurname1"
                                        ValidChars=" ">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Relationship<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDepRelationShip1" CssClass="required" runat="server" Width="200px">
                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="FATHER" Value="FATHER"></asp:ListItem>
                                        <asp:ListItem Text="MOTHER" Value="MOTHER"></asp:ListItem>
                                        <asp:ListItem Text="WIFE" Value="WIFE"></asp:ListItem>
                                        <asp:ListItem Text="SON" Value="SON"></asp:ListItem>
                                        <asp:ListItem Text="DAUGHTER" Value="DAUGHTER"></asp:ListItem>
                                        <asp:ListItem Text="BROTHER" Value="BROTHER"></asp:ListItem>
                                        <asp:ListItem Text="SISTER" Value="SISTER"></asp:ListItem>
                                        <asp:ListItem Text="BROTHER-IN-LAW" Value="BROTHER-IN-LAW"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldValidator30" ValidationGroup="vgSubmit"
                                        runat="server" ControlToValidate="ddlDepRelationShip1" Display="None" ErrorMessage="Relationship is required"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    Phone Number<span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDepPhone1" CssClass="required" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtDepPhone1" Width="200px"
                                                    ErrorMessage="Invalid Phone Number" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>--%>
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender4"
                                        TargetControlID="txtDepPhone1" FilterType="Numbers,Custom" ValidChars="+,-">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ErrorMessage="Phone number is required"
                                        Display="None" ForeColor="Red" ControlToValidate="txtDepPhone1" ValidationGroup="vgSubmit">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Date Of Birth
                                    <asp:Label ID="lblDOBStar4" runat="server" CssClass="mandatory">*</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDepDoB1" runat="server" Width="200px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtDepDoB1">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Address"></asp:Label><span class="mandatory">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDepInternational" runat="server" TextMode="MultiLine" Width="200px"
                                        MaxLength="250" CssClass="required" Style="height: 55px; width: 205px;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ErrorMessage="Address is required"
                                        Display="None" ForeColor="Red" ControlToValidate="txtDepInternational" ValidationGroup="vgSubmit">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td id="tdDepSSN3" runat="server">
                                    <asp:Label ID="Label1" Text="SSN" runat="server"></asp:Label>
                                    <asp:Label ID="Label5" runat="server" CssClass="mandatory">*</asp:Label>
                                </td>
                                <td id="tdDepSSN4" runat="server">
                                    <asp:TextBox ID="txtDepSSNI" ToolTip="The Social Security number is a nine-digit number in the format 'AAA-GG-SSSS'"
                                        runat="server" Width="200px" MaxLength="11" CssClass="required"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender10"
                                        TargetControlID="txtDepSSNI" FilterMode="ValidChars" FilterType="Numbers,Custom"
                                        ValidChars="-">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDepSSNI"
                                        Width="200px" ValidationGroup="vgSubmit" Display="None" ErrorMessage="Enter Valid SSN "
                                        ValidationExpression="^\d{3}-?\d{2}-?\d{4}$">
                                    </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <center>
                                        <table>
                                            <tr>
                                                <td>
                                                    Mark as Beneficiary?
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rdbDepBenefeciary1" runat="server" RepeatDirection="Horizontal"
                                                        ClientIDMode="Static">
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                        <asp:ListItem Selected="True">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: Center">
                                    <asp:Button ID="Button5" runat="server" Text=" Save " OnClick="btnSaveDependents_Click"
                                        ValidationGroup="vgSubmit" ClientIDMode="Static" />
                                    <asp:Button ID="Button6" runat="server" OnClick="btnSaveAndCloseDependents_Click"
                                        Text="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:Panel>
                <asp:HiddenField ClientIDMode="Static" runat="server" ID="hdnSession" />
                <asp:HiddenField ClientIDMode="Static" runat="server" ID="hdnUSCountryID" />
                <asp:ValidationSummary ID="Validate" runat="server" DisplayMode="List" ShowMessageBox="true"
                    ShowSummary="false" ValidationGroup="vgSubmit" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("body").on("mouseover", ".toolTipText", function () {
                var data = $(this).attr("rel");
                js_ShowToolTip(data, evt, objthis);
            });

            $("body").on("mouseout", ".toolTipText", function () {
                $("#__divMsgTooltip").hide();
                $("#__divMsgTooltip").html('');
            });

            $("body").on("click", "#btnSaveDependent", function () {
                var ReturnText = "";
                if (parseInt($("#ddlDepCountry option:selected").val()) == parseInt($("#<%=hdnUSCountryID.ClientID %>").val())) {////If selected country is US
                    ReturnText = USAddressValidation($("#txtDepAdd1").val(), $("#txtDepAdd2").val(), $("#txtDepCity").val(), $("#txtDepState").val(), $("#txtDepZip").val(), $("#ddlDepCountry option:selected").text(), "Dependent", "", "", "", "", "", "");
                    if (ReturnText != "") {
                        if (ReturnText == "Error") {
                            alert("Address was not verified in USPS");
                        }
                        else {
                            alert(ReturnText);
                            return false;
                        }
                    }
                }
            });


            $("body").on("click", "#btnSaveNOKDetails", function () {
                var ReturnText = "";

                if (parseInt($("#ddlCountry option:selected").val()) == parseInt($("#<%=hdnUSCountryID.ClientID %>").val())) {////If selected country is US

                    ReturnText = USAddressValidation($("#txtAddress1").val(), $("#txtAddress2").val(), $("#txtCity").val(), $("#txtState").val(), $("#txtZip").val(), $("#ddlCountry option:selected").text(), "NOK", "", "", "", "", "", "");
                    if (ReturnText != "") {
                        if (ReturnText == "Error") {
                            alert("Address was not verified in USPS");
                        }
                        else {
                            alert(ReturnText);
                            return false;
                        }
                    }
                }
            });
        });
       
    </script>
    </form>
</body>
</html>
