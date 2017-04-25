<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileLoader.aspx.cs" Inherits="FileLoader" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Document</title>
    <link href="css/Main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/AsyncResponse.js"></script>
    <script src="../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="JS/common.js" type="text/javascript"></script>
     <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
       <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            font-family: Tahoma;
            font-size: 11px;
        }
        historyBox
        {
            background-color: #efefef;
            border: 1px solid inset;
        }
    </style>
    <script type="text/javascript">
        function OpenDoc(url) {
        var docId= <%=GetDocID() %>;
            url=url+docId;
            document.getElementById("ifrmDocument").src = url;

            return false;
        }

        function OpenCheckOutDoc(url) {

        var docId= <%=GetDocID() %>;
            url=url+docId;
            var checkStatus = confirm("You are about to Check Out the document.\n\nPlease save the document to your local system,edit and Check In from the same location.\n\nDo you want to continue?");
            if (checkStatus) {
                window.location.href = url;
            }
            return false;
        }
        function ViewHistory() {

        var docId= <%=GetDocID() %>;
            //Async_getHistory(docId);
            //document.getElementById("dvHistory").style.display = 'block';
             $("#dvDocHistory").load("docHistory.aspx", { FileID: docId }, function (content) {
                $("#dvHistory").show('slow');
                
                return false;
            });
            return false;
        }
       

        function closeHistory() {
            document.getElementById("dvHistory").style.display = 'none';
            return false;
        }

        function Async_getHistory() {
         var docId= <%=GetDocID() %>;
            var url = "docHistory.aspx?FileID=" + docId;
            var params = "";

            obj = new AsyncResponse(url, params, response_getHistory)
            obj.getResponse();
            return false;
        }
        function response_getHistory(retval) {
            var ar, arS
            
            if (retval.indexOf('Working') >= 0) { return; }
            try {

                retval = clearXMLTags(retval);

                //var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
                //xmlDoc.async = "false";
                //xmlDoc.loadXML(retval);

                //var frm = xmlDoc.getElementsByTagName("form")[0];
                //dv = frm.childNodes[0];

                //frm.removeChild(dv);

//                var xmlOut = "";
//                var x = frm.childNodes;
//                for (var i = 0; i < x.length; i++) {
//                    xmlOut += x[i].xml;
//                }
                document.getElementById("dvDocHistory").innerHTML = retval;
            }
            catch (ex) { alert(ex.message) }
        }

        function clearXMLTags(retval) {
            try {
                retval = retval.replace('<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">', '');
            }
            catch (ex) { }
            return retval;
        }

        function print() {
            try {
                var selNodeUrl = document.getElementById("lblDocName").innerHTML;
                if (selNodeUrl == undefined || selNodeUrl == "") {
                    myMessage("No Document selected for printing!")
                }
                else {


                    if (selNodeUrl.indexOf('_blank') > 0) { myMessage("Please select a file"); return false; }
                    if (selNodeUrl.indexOf(".pdf") > 0) {
                        myMessage("Please use print button available inside pdf document.")
                    }
                    else {
                        //  frames[0].focus();
                        //  frames[0].print();
                        window.frames['ifrmDocument'].focus();
                        window.frames['ifrmDocument'].print();

                    }
                }
            }
            catch (ex)
        { alert(ex) }
        }


    
    </script>
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
    <div id="divMessage" align="center">
        <asp:Label ID="dvMessage" runat="server" ForeColor="Blue" Font-Size="Medium"></asp:Label>
    </div>
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="dvMain" runat="server">
        <div style="overflow: hidden; background-color: #cfcfff; margin-bottom: 1px; border: 1px solid outset;"
            id="Div2">
            <table id="docProperties" style="width: 100%;">
                <tr>
                    <td>
                        <span style="color: #666">File Name:</span>
                        <asp:Label ID="lblDocName" runat="server" Text=""></asp:Label>
                        &nbsp;|&nbsp; <span style="color: #666">Version:</span>
                        <asp:Label ID="lblCurrentVersion" runat="server" Text=""></asp:Label>
                        <br />
                        <span style="color: #666">Status:</span>
                        <asp:Label ID="lblOppStatus" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="text-align: right;">
                    <asp:HiddenField ID="hfDocName" runat="server" />
                        <%--<img src="images/print.gif" alt="Print" onclick="print()" style="cursor: hand;" tooltip="Print"/>--%>
                          <asp:ImageButton ID="ImgViewIsRead" runat="server" 
                            ImageUrl="~/Images/view-detail-qmsprocedure.png" 
                            ToolTip="Read Receipt Log" onclick="ImgViewIsRead_Click"  />
                        <asp:ImageButton ID="imgPrint" runat="server" ImageUrl="images/print.gif" OnClientClick="return print();" ToolTip="Print"  />
                        <%if (GetStatus() == "CHECKED OUT")
                          {
                              if (Session["USERID"].ToString() == CheckOutUserID())
                              {
                        %>
                      <%--  <img src="images/checkin.png" alt="Check In" onclick="OpenDoc('CheckInForm.aspx?FileID=<%=GetDocID()%>');"
                            style="cursor: hand;" />--%>

                         <asp:ImageButton ID="ImgSubCheckIn" runat="server" ImageUrl="images/checkin.png" OnClientClick="return OpenDoc('CheckInForm.aspx?FileID=');" ToolTip="Check In"  />

                        <%
                            }
                          }
                          else
                          {
                        %>
                        <%--<img src="images/checkout.png" alt="Check Out" onclick="OpenCheckOutDoc('CheckOutForm.aspx?FileID=<%=GetDocID()%>')"
                            style="cursor: hand;" />--%>
                        <asp:ImageButton ID="ImgCheckOut" runat="server" ImageUrl="images/checkout.png" OnClientClick="return OpenCheckOutDoc('CheckOutForm.aspx?FileID=');" ToolTip="Check Out"  />
                        <%--<img src="images/checkin.png" alt="Check In" onclick="OpenDoc('CheckInForm.aspx?FileID=<%=GetDocID()%>');"
                            style="cursor: hand;" />--%>
                        <asp:ImageButton ID="ImgCheckIn" runat="server" ImageUrl="images/checkin.png" OnClientClick="return OpenDoc('CheckInForm.aspx?FileID=');" ToolTip="Check In"  />
                        <%
                            }
                        %>
                       <%-- <img src="images/history.png" alt="View History" onclick="ViewHistory('<%=GetDocID()%>')"
                            style="cursor: hand;" />--%>
                            <asp:ImageButton ID="ImgViewHistory" runat="server" ImageUrl="images/history.png" OnClientClick="return ViewHistory();" ToolTip="View History"  />
              <%--          <img src="images/getLatest.png" alt="Get Latest File" onclick="OpenDoc('GetLatest.aspx?FileID=<%=GetDocID()%>')"
                            style="cursor: hand;" />--%>
                            <asp:ImageButton ID="ImgGetLatest" runat="server" ImageUrl="images/getLatest.png" OnClientClick="return OpenDoc('GetLatest.aspx?FileID=');" ToolTip="Get Latest File"  />
                            <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete File"
                            ImageUrl="~/images/deleteicon.png" OnClick="btnDeleteE_Click" Visible="false"
                            ToolTip="Delete File"  OnClientClick="return confirm('Are you sure want to delete?')"/>
                    
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvFrame" style="overflow: hidden; background-color: #cfcfff; border: 2px solid #cfcfff;
            padding: 1px;">
            <div id="dvHistory" class="historyBox" style="display: none; border: 1px solid inset;
                background-color: #efddef; padding: 2px;text-align: left;">
                <div id="dvDocHistory" style="width: 100%;">
                </div>
                <table cellspacing="0" cellpadding="0" style="width: 100%;">
                    <tr>
                        <td colspan="2" style="text-align: center;">
                           <%-- <img src="images/close.gif" alt="Close" onclick="closeHistory();" style="cursor: hand;" />--%>
                            <asp:ImageButton ID="imgClose" runat="server" ImageUrl="images/close.gif" OnClientClick="return closeHistory();" ToolTip="Close"  />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="border: 1px solid inset;
                background-color: #efddef;  text-align: left;">
                <iframe id="ifrmDocument" name="ifrmDocument" src="<%=GetDocPath()%>" style="height: 557px;
                    width: 100%; border: 1px; padding: 0; margin: 0;" frameborder="0"></iframe>
            </div>
        </div>
    </div>
    <div id="divApprovalMessage" align="center" runat="server" style="overflow: hidden;
        background-color: #cfcfff; margin-bottom: 1px; border: 1px solid outset;">
        <table id="Table1" style="width: 100%;">
            <tr>
                <td>
                
                    <span style="color: #666">File Name:</span>
                    <asp:Label ID="lblDocName1" runat="server" Text=""></asp:Label>
                    &nbsp;|&nbsp; <span style="color: #666">Version:</span>
                    <asp:Label ID="lblCurrentVersion1" runat="server" Text=""></asp:Label>
                    <br />
                    <span style="color: #666">Status:</span>
                    <asp:Label ID="lblOppStatus1" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="lblApprovalPendingBy" runat="server" Font-Bold="True" ForeColor="#003366"
                        Text="Approval Pending By: "></asp:Label>
                    <asp:Label ID="lblUser" runat="server" Font-Bold="True" ForeColor="#003366"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
     <div id="divViewIsRead" title="Read Receipt Log"  style="display: none; border: 1px solid Gray;
                            font-family: Tahoma; text-align: left; font-size: 12px; color: Black; width: 80%;">

                            <table cellpadding="2" cellspacing="2" width="100%" style="padding: 10px 10px 10px 10px">                             
                                                               <caption>
                                                                   File Name:
                                                                   <asp:Label ID="lblRedDocName" runat="server" Font-Bold="true"></asp:Label>
                                                                   </tr>
                                                                   <tr>
                                                                       <td>
                                                                           <asp:GridView ID="gvViewIsRead" runat="server" AllowSorting="true" 
                                                                               AutoGenerateColumns="False" CellPadding="1" CellSpacing="0" 
                                                                               EmptyDataText="NO RECORDS FOUND" GridLines="both" Width="100%">
                                                                               <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                                               <RowStyle CssClass="RowStyle-css" />
                                                                               <HeaderStyle CssClass="HeaderStyle-css" Height="25px" />
                                                                               <PagerStyle CssClass="PMSPagerStyle-css" />
                                                                               <SelectedRowStyle BackColor="#FFFFCC" />
                                                                               <EmptyDataRowStyle Font-Bold="true" Font-Size="12px" ForeColor="Red" 
                                                                                   HorizontalAlign="Center" />
                                                                               <Columns>
                                                                                   <%--   <asp:TemplateField HeaderText="File Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("Key_Value")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField> --%>
                                                                                   <asp:TemplateField HeaderText="Read Date">
                                                                                       <ItemTemplate>
                                                                                           <asp:Label ID="lblDate" runat="server" Text='<%# Eval("CREATED_DATE")%>'></asp:Label>
                                                                                       </ItemTemplate>
                                                                                       <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Left" Width="40px" 
                                                                                           Wrap="true" />
                                                                                       <HeaderStyle HorizontalAlign="Left" Wrap="true" />
                                                                                   </asp:TemplateField>
                                                                                   <asp:TemplateField HeaderText="Version">
                                                                                       <ItemTemplate>
                                                                                           <asp:Label ID="lblVersion" runat="server" Text='<%# Eval("Version")%>'></asp:Label>
                                                                                       </ItemTemplate>
                                                                                       <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Left" Width="40px" 
                                                                                           Wrap="true" />
                                                                                       <HeaderStyle HorizontalAlign="Left" Wrap="true" />
                                                                                   </asp:TemplateField>
                                                                                   <asp:TemplateField HeaderText="Crew Name">
                                                                                       <ItemTemplate>
                                                                                           <asp:Label ID="lblCrewName" runat="server" Text='<%# Eval("Staff_Name")%>'></asp:Label>
                                                                                       </ItemTemplate>
                                                                                       <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Left" Width="40px" 
                                                                                           Wrap="true" />
                                                                                       <HeaderStyle HorizontalAlign="Left" Wrap="true" />
                                                                                   </asp:TemplateField>
                                                                                   <asp:TemplateField HeaderText="Vessel Name">
                                                                                       <ItemTemplate>
                                                                                           <asp:Label ID="lblVesselName" runat="server" Text='<%# Eval("Vessel_Name")%>'></asp:Label>
                                                                                       </ItemTemplate>
                                                                                       <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Left" Width="40px" 
                                                                                           Wrap="true" />
                                                                                       <HeaderStyle HorizontalAlign="Left" Wrap="true" />
                                                                                   </asp:TemplateField>
                                                                               </Columns>
                                                                           </asp:GridView>
                                                                       </td>
                                                                   </tr>
                                                               </caption>
                            </table>
                        </div>
                   </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnDelete" />
                    </Triggers>
                </asp:UpdatePanel>
    </form>
</body>
</html>
