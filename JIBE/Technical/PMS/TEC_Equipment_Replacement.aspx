<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TEC_Equipment_Replacement.aspx.cs"
    Inherits="Technical_PMS_TEC_Equipment_Replacement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: White">
    <form id="form1" runat="server">
    <div>
        <table style="margin: 2px">
            <tr>
                <td style="text-align: left; font-weight: bold">
                    Reason for replacement :
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" Height="50px" Width="700px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table style="width: 100%; border-collapse: collapse" border="1">
            <tr>
                <td style="padding: 5px; font-weight: bold; text-align: center; color: Navy">
                    Selected Equipment
                </td>
                <td>
                </td>
                <td style="padding: 5px; font-weight: bold; color: Navy; text-align: center">
                    Spare Equipment
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; width: 30%">
                    <asp:DetailsView ID="DViewActive" runat="server" AutoGenerateRows="false" DataKeyNames="ID"
                        CellPadding="4" Width="250px">
                        <FieldHeaderStyle Font-Size="11px" Font-Bold="true" Font-Names="tahoma" Width="50" />
                        <RowStyle Font-Size="11px" Font-Names="tahoma" />
                        <Fields>
                            <asp:BoundField HeaderText="Description" DataField="Description" />
                            <asp:BoundField HeaderText="Maker" DataField="Maker" />
                            <asp:BoundField HeaderText="Model" DataField="Module_Type" />
                            <asp:BoundField HeaderText="Serial Number" DataField="Serial_Number" />
                            <asp:BoundField HeaderText="Particulars" DataField="Particulars" />
                            <asp:BoundField HeaderText="Critical" DataField="Critical" />
                        </Fields>
                    </asp:DetailsView>
                </td>
                <td style="vertical-align: middle; text-align: center; width: 10%">
                    <asp:ImageButton ID="btnReplaceEquipment" runat="server" ImageUrl="~/Images/Actions-go-previous-icon.png"
                        OnClientClick="return ValidateEQPReplacement()" Text="Replace Equipment" OnClick="btnReplaceEquipment_Click" />
                </td>
                <td style="vertical-align: top">
                    <asp:GridView ID="gvSpare" runat="server" EmptyDataText="NO RECORDS FOUND" AutoGenerateColumns="False"
                        DataKeyNames="ID" CellPadding="4" GridLines="None" CellSpacing="0" Width="100%"
                        Font-Size="12px" CssClass="GridView-css">
                        <HeaderStyle CssClass="HeaderStyle-css" />
                        <PagerStyle CssClass="PagerStyle-css" />
                        <RowStyle CssClass="RowStyle-css" />
                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:RadioButton ID="rbtnDescription" runat="server" GroupName="spare" onclick='<%#"SelectedSpareLocation("+  Eval("ID") +",this)" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Description" DataField="Description" ItemStyle-Width="200px" />
                            <asp:BoundField HeaderText="Maker" DataField="Maker" />
                            <asp:BoundField HeaderText="Model" DataField="Module_Type" />
                            <asp:BoundField HeaderText="Serial Number" DataField="Serial_Number" />
                            <asp:BoundField HeaderText="Particulars" DataField="Particulars" ItemStyle-Width="250px"
                                ItemStyle-Wrap="true" />
                            <asp:BoundField HeaderText="Critical" DataField="Critical" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                    <asp:HiddenField ID="hdfSpareEQPID" Value="0" runat="server" />
                </td>
            </tr>
        </table>
        <script type="text/javascript">

            var _SelectedSpareLocationctl = null;

            function SelectedSpareLocation(SelectedSpareLocationID, objselected) {
                document.getElementById('hdfSpareEQPID').value = SelectedSpareLocationID;


                if (_SelectedSpareLocationctl != null) {
                    _SelectedSpareLocationctl.checked = false;
                }
                _SelectedSpareLocationctl = objselected;

            }

            function ValidateEQPReplacement() {
                if (document.getElementById('hdfSpareEQPID').value.toString() == "0") {
                    alert('Please selected spare equipment to replace !')
                    return false;
                }
                else if (document.getElementById('txtRemark').value.toString().trim() == "") {
                    alert('Please enter reason !')
                    return false;
                }
                else {
                    a = confirm('Are you sure to replace the location? ');
                    if (a)
                        return true;
                    else
                        return false;
                }


            }
        </script>
    </div>
    </form>
</body>
</html>
