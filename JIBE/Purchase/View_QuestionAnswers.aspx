<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View_QuestionAnswers.aspx.cs" Inherits="Purchase_View_QuestionAnswers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>   
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/Common_Functions.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scm" runat="server" ClientIDMode="Static"></asp:ScriptManager>
    <div id="divPopupQuestion" title="Question & Answers" style="display:; width:800px" >
          <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                    <asp:GridView ID="grdQuestion" runat="server" ClientIDMode="Static" 
                        Width="100%" AutoGenerateColumns="false"  BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px" CellPadding="2" CellSpacing="0"
                        onrowdatabound="grdQuestion_RowDataBound">
                         <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                <RowStyle Font-Size="12px" CssClass="PMSGridRowStyle-css" />
                                <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                <PagerStyle CssClass="PMSPagerStyle-css" />
                                <SelectedRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                    <Columns>
                    <asp:TemplateField HeaderText="Questions">
                        <ItemTemplate>
                            <asp:Label ID="lblQuestID" style="display:none" ClientIDMode="Static" runat="server" Text='<%#Eval("Question_ID") %>'></asp:Label>
                            <asp:Label ID="lblQuestion" ClientIDMode="Static" runat="server" Text='<%#Eval("Question") %>'></asp:Label>
                            <asp:Label ID="lblGradeType" style="display:none"  ClientIDMode="Static" runat="server" Text='<%#Eval("Grade_Type") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Answers">
                    <ItemTemplate>

                    <asp:TextBox ID="txtDescriptive" ClientIDMode="Static" runat="server" TextMode="MultiLine" Text='<%#Eval("Remark")%>'> </asp:TextBox>
                    <asp:DropDownList ID="ddlAnswers" ClientIDMode="Static" runat="server"  ></asp:DropDownList>
                    <asp:Label ID="lblAns"  style="display:none"  runat="server" ClientIDMode="Static" Text='<%#Eval("Option_ID") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
                    <br />
                    <table align="center">
                    <tr>
                    <td><asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" 
                            onclick="btnSave_Click"/></td>
                    </tr>
                    </table>
                     <br />
            </ContentTemplate>
            </asp:UpdatePanel>
           
            </div>
    </form>
</body>
</html>
