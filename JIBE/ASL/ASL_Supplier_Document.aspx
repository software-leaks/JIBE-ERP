<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ASL_Supplier_Document.aspx.cs"
    Inherits="ASL_ASL_Supplier_Document" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Supplier Document</title>
    <link href="../Styles/crew_css.css" rel="stylesheet" type="text/css" />
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
    <script language="javascript" type="text/javascript">

        function DocOpen(FILEPATH) {

            //var filepath = "../uploads/ASL/";
            //alert(filepath + filename);
            //alert(FILEPATH);
            window.open(FILEPATH);
        }

        function showdetails(path) {
            window.open(path);
            return false;
        }
        function previewDocument(docPath) {
            document.getElementById("ifrmDocPreview").src = docPath;

        }
    </script>
      <script type="text/javascript">
          /*The Following Code added To adjust height of the popup when open after entering search criteria.*/
          $(document).ready(function () {
              window.parent.$("#ASL_Evalution").css("height", (parseInt($("#pnlDocs").height()) + 50) + "px");
              window.parent.$(".xfCon").css("height", (parseInt($("#pnlDocs").height()) + 50) + "px").css("top", "50px");
          });
    </script>
</head>
<body style="border: 0px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 99%;">
<table  style="border: 0px solid #cccccc; font-family: Tahoma; overflow: Auto; font-size: 12px; width: 100%;">
                                 <tr><td>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="printablediv" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
        color: Black; height: 100%;">
        <div class="error-message" onclick="javascript:this.style.display='none';">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <asp:Panel ID="pnlDocs" runat="server" Visible="true">
            <asp:UpdatePanel ID="panel1" runat="server">
                <ContentTemplate>
                     <div id="Div1" style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px;
                color: Black; height: 100%;">
                <div id="Div2" class="page-title">
                                    Supplier Document
                                </div>
                        <table width="100%" cellpadding="2" cellspacing="0">
                        <tr>
                            <td style="text-align: left">
                                Attachments and Documents (Click on the download button to see the document)
                                <br />
                            </td>
                        </tr>
                        <tr id="TR1" runat="server" visible="false">
                            <td style="text-align: left">
                                <div style="background: #cccccc; color: Blue; padding: 3px; font-weight: 600">
                                    Purchasing Contract Prices</div>
                                <br />
                            </td>
                        </tr>
                        <tr id="TR2" runat="server" visible="false">
                            <td>
                                <div>
                                    <asp:GridView ID="gvPurchaseAttachment" runat="server" AutoGenerateColumns="False"
                                        Width="100%" GridLines="Both">
                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                        <RowStyle CssClass="PMSGridRowStyle-css" />
                                        <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="false" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Uploaded Date">
                                                <HeaderTemplate>
                                                    Uploaded Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPurchase" runat="server" Text='<%#Eval("CreatedDate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attachments">
                                                <HeaderTemplate>
                                                    Attached File(s)
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPurchasefileName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILENAME") %>'></asp:Label>
                                                    <asp:Label ID="lblPurchasefilePath" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILEPATH") %>'></asp:Label>
                                                    <asp:Label ID="lblPurchaseFileId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="left" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <table cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td>
                                                                <img style="border: 0; width: 14px; height: 14px" alt="Open in new window" onclick="DocOpen('<%#Eval("FILEPATH") + "," +Eval("File_Status") + "," + Eval("FileFullName")%>')"
                                                                    src="../Images/Download-icon.png" title="Click to View file" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <div id="divRegisteredData" runat="server"  >
                        <tr>
                            <td>
                                <div style="background: #cccccc; color: Blue; padding: 3px; font-weight: 600">
                                    Supplier Data Form</div>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <asp:GridView ID="gvDataAttachment" runat="server" AutoGenerateColumns="False" Width="100%"
                                        GridLines="Both" onrowdatabound="gvDataAttachment_RowDataBound"  EmptyDataText="NO RECORDS FOUND" >
                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                        <RowStyle CssClass="PMSGridRowStyle-css" />
                                        <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="false" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Uploaded Date">
                                                <HeaderTemplate>
                                                    Uploaded Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupplier" runat="server" Text='<%#Eval("CreatedDate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attachments">
                                                <HeaderTemplate>
                                                    Attached File(s)
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDatafileName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILENAME") %>'></asp:Label>
                                                    <asp:Label ID="lblDatafilePath" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILEPATH") %>'></asp:Label>
                                                    <asp:Label ID="lblDataFileId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <table cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td>
                                                               <%-- <asp:Button ID="btnView" runat="server" ToolTip="View" Text="View Document" OnClientClick='<%#"DocOpen(&#39;" + Eval("[FilePath]") +"&#39;);return false;"%>' />--%>
                                                               <asp:ImageButton ID="imgDownload" runat="server"  style="width: 20px; height: 20px" CommandArgument='<%#Eval("FilePath") + "," +Eval("File_Status") + "," + Eval("FileFullName")%>' OnCommand="ImgDownload_Click" 
                                                    ForeColor="Black" ToolTip="Click to View file" ImageUrl="~/Images/Download-icon.png"></asp:ImageButton>
                                                                 <%-- <img style="border: 0; width: 20px; height: 20px" alt="Open in new window" onclick="DocOpen('<%#Eval("FILEPATH")%>')" OnClientClick='<%#"DocOpen(&#39;" + Eval("[FilePath]") +"&#39;);return false;"%>' 
                                                                    src="../Images/Download-icon.png" title="Click to View file" />--%>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td>
                                                           <%-- <asp:Button ID="btnDataDelete" runat="server" Visible='<%# uaDeleteFlage %>' ToolTip="Delete"  
                                                            OnClientClick="return confirm('Are you sure want to delete?')" OnCommand="btnDataDelete_Click" Text="Delete Document"
                                                             CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") + "," + DataBinder.Eval(Container,"DataItem.FILEPATH")  %>' />--%>
                                                               <asp:ImageButton ID="btnDataDelete" style="border: 0; width: 20px; height: 20px" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") + "," + DataBinder.Eval(Container,"DataItem.FILEPATH") + "," + Eval("File_Status")  %>'
                                                                    Visible='<%# uaDeleteFlage %>' ImageUrl="~/images/delete.png" OnCommand="btnDataDelete_Click"
                                                                    OnClientClick="return confirm('Are you sure want to delete?')" ToolTip="Delete">
                                                                </asp:ImageButton>
                                                              
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <br />
                                <br />
                            </td>
                        </tr>
                        </div>
                        <tr id="TR3" runat="server" visible="false">
                            <td>
                                <div style="background: #cccccc; color: Blue; padding: 3px; font-weight: 600">
                                    Supplier Alerts</div>
                                <br />
                            </td>
                        </tr>
                        <tr id="TR4" runat="server" visible="false">
                            <td>
                                <div>
                                    <asp:GridView ID="gvSupplierAlerts" runat="server" AutoGenerateColumns="False" Width="100%"
                                        GridLines="Both">
                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                        <RowStyle CssClass="PMSGridRowStyle-css" />
                                        <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="false" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Uploaded Date">
                                                <HeaderTemplate>
                                                    Uploaded Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblalert" runat="server" Text='<%#Eval("CreatedDate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attachments">
                                                <HeaderTemplate>
                                                    Attached File(s)
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblalertfileName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILENAME") %>'></asp:Label>
                                                    <asp:Label ID="lblalertfilePath" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILEPATH") %>'></asp:Label>
                                                    <asp:Label ID="lblalertFileId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <table cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td>
                                                                <img style="border: 0; width: 14px; height: 14px" alt="Open in new window" onclick="DocOpen('<%#Eval("FILEPATH") + "," + Eval("File_Status") + "," + Eval("FileFullName")%>')"
                                                                    src="../Images/Download-icon.png" title="Click to View file" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <div id="divCompdocs" runat="server"  >
                        <tr>
                            <td>
                                <div style="background: #cccccc; color: Blue; padding: 3px; font-weight: 600">
                                    Company Registration</div>
                                <br />
                            </td>
                        </tr>
                        </div>
                        <tr>
                            <td>
                                <div>
                                    <asp:GridView ID="gvCompanyAttachment" runat="server" AutoGenerateColumns="False"
                                        Width="100%" GridLines="Both" 
                                        onrowdatabound="gvCompanyAttachment_RowDataBound"  EmptyDataText="NO RECORDS FOUND">
                                        <HeaderStyle CssClass="PMSGridHeaderStyle-css" />
                                        <RowStyle CssClass="PMSGridRowStyle-css" />
                                        <AlternatingRowStyle CssClass="PMSGridAlternatingRowStyle-css" />
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="false" Font-Size="12px" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Uploaded Date">
                                                <HeaderTemplate>
                                                    Uploaded Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompany" runat="server" Text='<%#Eval("CreatedDate","{0:dd/MM/yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attachments">
                                                <HeaderTemplate>
                                                    Attached File(s)
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfileName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILENAME") %>'></asp:Label>
                                                    <asp:Label ID="lblfilePath" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILEPATH") %>'></asp:Label>
                                                    <asp:Label ID="lblFileId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <table cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td>
                                                           <%-- <asp:Button ID="btnCompView" runat="server" ToolTip="View" Text="View Document" OnClientClick='<%#"DocOpen(&#39;" + Eval("[FILEPATH]") +"&#39;);return false;"%>' />--%>
                                                                 <asp:ImageButton ID="imgDownload" runat="server" OnCommand="ImgDownload_Click" style="width: 20px; height: 20px" CommandArgument='<%#Eval("[FilePath]") + "," + Eval("File_Status") + "," + Eval("FileFullName")%>'
                                                    ForeColor="Black" ToolTip="Click to View file" ImageUrl="~/Images/Download-icon.png"></asp:ImageButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td>
                                                             <%-- <asp:Button ID="btnCompDelete" runat="server" Visible='<%# uaDeleteFlage %>' ToolTip="Delete"  
                                                            OnClientClick="return confirm('Are you sure want to delete?')" OnCommand="btnCompanyDelete_Click" Text="Delete Document"
                                                             CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") + "," + DataBinder.Eval(Container,"DataItem.FILEPATH")  %>' />--%>
                                                                <asp:ImageButton ID="btnCompanyDelete" style="border: 0; width: 20px; height: 20px" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") + "," + DataBinder.Eval(Container,"DataItem.FILEPATH") + "," + Eval("File_Status")  %>'
                                                                    Visible='<%# uaDeleteFlage %>' ImageUrl="~/images/delete.png" OnCommand="btnCompanyDelete_Click"
                                                                    OnClientClick="return confirm('Are you sure want to delete?')" ToolTip="Delete">
                                                                </asp:ImageButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <br />
                            </td>
                        </tr>
                        <div id="divotherdocs" runat="server"  >
                        <tr>
                            <td style="width: 8%; color: Blue;">
                                Other Document uploaded by Suppliers.
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="margin-left: auto; margin-right: auto; text-align: center;">
                                    <asp:GridView ID="gvAllAtttachment" runat="server" AutoGenerateColumns="False" Width="100%"
                                        GridLines="Both" onrowdatabound="gvAllAtttachment_RowDataBound">
                                        <HeaderStyle CssClass="HeaderStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                                        <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                        <EmptyDataRowStyle CssClass="EmptyDataRowStyle-css" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Uploaded ID">
                                                <HeaderTemplate>
                                                    Uploaded ID
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Uploaded Date">
                                                <HeaderTemplate>
                                                    Uploaded Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedDate" runat="server" Width="80px" Text='<%#Eval("CreatedDate","{0:dd-MMM-yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="80px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attachments">
                                                <HeaderTemplate>
                                                    Attached File(s)
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAllfileName" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILENAME") %>'></asp:Label>
                                                    <asp:Label ID="lblAllfilePath" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FILEPATH") %>'></asp:Label>
                                                    <asp:Label ID="lblAllFileId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attachments Type">
                                                <HeaderTemplate>
                                                    Attachments Type
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoice_Type" runat="server" Text='<%#Eval("Invoice_Type")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="true" HorizontalAlign="Center" Width="100px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                                <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <table cellpadding="2" cellspacing="2">
                                                        <tr>
                                                            <td>
                                                              <asp:ImageButton ID="imgDownload" runat="server" OnCommand="ImgDownload_Click" style="width: 20px; height: 20px" CommandArgument='<%#Eval("[FilePath]") + "," + Eval("File_Status") + "," + Eval("FileFullName")%>'
                                                    ForeColor="Black" ToolTip="Click to View file" ImageUrl="~/Images/Download-icon.png"></asp:ImageButton>
                                                             <%--<asp:Button ID="btnOtherView" runat="server" ToolTip="View" Text="View Document" OnClientClick='<%#"DocOpen(&#39;" + Eval("[FILEPATH]") +"&#39;);return false;"%>' />--%>
                                                          <%--      <img style="border: 0; width: 20px; height: 20px" alt="Open in new window" onclick="DocOpen('<%#Eval("FILEPATH")%>')"
                                                                    src="../Images/Download-icon.png" title="Click to View file" />--%>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td>
                                                           <%-- <asp:Button ID="btnOtherDelete" runat="server" Visible='<%# uaDeleteFlage %>' ToolTip="Delete"  
                                                            OnClientClick="return confirm('Are you sure want to delete?')" OnCommand="btnOtherDelete_Click" Text="Delete Document"
                                                             CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") + "," + DataBinder.Eval(Container,"DataItem.FILEPATH")  %>' />--%>
                                                                <asp:ImageButton ID="btnOtherDelete" style="border: 0; width: 20px; height: 20px" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") + "," + DataBinder.Eval(Container,"DataItem.FILEPATH") + "," + Eval("File_Status")  %>'
                                                                    Visible='<%# uaDeleteFlage %>' ImageUrl="~/images/delete.png" OnCommand="btnOtherDelete_Click"
                                                                    OnClientClick="return confirm('Are you sure want to delete?')" ToolTip="Delete">
                                                                </asp:ImageButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="50px" CssClass="PMSGridItemStyle-css">
                                                </ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                       </div>
                    </table>
                      </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <%--   <iframe id="ifrmDocPreview"  marginheight="0px" runat="server"
                                                    src="../Images/previewAttach.png" 
                                                    style="vertical-align: middle; height: 500px; text-align: center;" width="100%"></iframe>--%>
    </form>
    </td></tr></table>
</body>
</html>
