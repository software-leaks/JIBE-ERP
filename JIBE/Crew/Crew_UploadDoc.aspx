<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Crew_UploadDoc.aspx.cs" Inherits="Crew_Crew_UploadDoc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .error-message
        {
            font-size: 12px;
            font-weight: bold;
            color: Red;
            text-align: center;
            background-color: Yellow;
        }
        body
        {
            font-family:Tahoma;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <table style="width: 100%;">
            <tr>
                <th>
                    Document Type
                </th>
                <th>
                    Browse File
                </th>
                <th>
                    Doc No.
                </th>
                <th>
                    Place of Issue
                </th>
                <th>
                    Issue Date
                </th>
                <th>
                    Expiry Date
                </th>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownList1" DataValueField="DocTypeID" DataTextField="DocTypeName"
                        runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" />
                </td>
                <td>
                    <asp:TextBox ID="txtDocNo1" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtIssuePlace1" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtIssueDate1" runat="server" Width="100px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtIssueDate1">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                    <asp:TextBox ID="txtExpDate1" runat="server" Width="100px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtExpDate1">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                    <div class="error-message" onclick="javascript:this.style.display='none';">
                      <asp:Label ID="lblmsg1" runat="server"></asp:Label>
              </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownList2" DataValueField="DocTypeID" DataTextField="DocTypeName"
                        runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload2" runat="server" Width="300px" />
                </td>
                <td>
                    <asp:TextBox ID="txtDocNo2" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtIssuePlace2" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtIssueDate2" runat="server" Width="100px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtIssueDate2"
                        Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                    <asp:TextBox ID="txtExpDate2" runat="server" Width="100px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtExpDate2"
                        Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                 <div class="error-message" onclick="javascript:this.style.display='none';">
                     <asp:Label ID="lblmsg2" runat="server"></asp:Label>
        </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownList3" DataValueField="DocTypeID" DataTextField="DocTypeName"
                        runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload3" runat="server" Width="300px" />
                </td>
                <td>
                    <asp:TextBox ID="txtDocNo3" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtIssuePlace3" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtIssueDate3" runat="server" Width="100px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtIssueDate3"
                        Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                    <asp:TextBox ID="txtExpDate3" runat="server" Width="100px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtExpDate3"
                        Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                 <div class="error-message" onclick="javascript:this.style.display='none';">
                  <asp:Label ID="lblmsg3" runat="server"></asp:Label>
             </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownList4" DataValueField="DocTypeID" DataTextField="DocTypeName"
                        runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload4" runat="server" Width="300px" />
                </td>
                <td>
                    <asp:TextBox ID="txtDocNo4" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtIssuePlace4" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtIssueDate4" runat="server" Width="100px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtIssueDate4"
                        Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                    <asp:TextBox ID="txtExpDate4" runat="server" Width="100px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtExpDate4"
                        Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                 <div class="error-message" onclick="javascript:this.style.display='none';">
                  <asp:Label ID="lblmsg4" runat="server"></asp:Label>
           </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownList5" DataValueField="DocTypeID" DataTextField="DocTypeName"
                        runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload5" runat="server" Width="300px" />
                </td>
                <td>
                    <asp:TextBox ID="txtDocNo5" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtIssuePlace5" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtIssueDate5" runat="server" Width="100px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txtIssueDate5"
                        Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                    <asp:TextBox ID="txtExpDate5" runat="server" Width="100px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="txtExpDate5"
                        Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td>
                 <div class="error-message" onclick="javascript:this.style.display='none';">
                 <asp:Label ID="lblmsg5" runat="server"></asp:Label>
        </div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="6" style="text-align: center;">
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click">
                    </asp:Button>
                    <input type="button" value="Close" onclick="window.parent.hideModal('dvPopupFrame');" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
