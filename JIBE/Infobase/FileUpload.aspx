<%@ Page Title="File Upload" Language="C#" MasterPageFile="~/Site.master"   AutoEventWireup="true" CodeFile="FileUpload.aspx.cs" Inherits="FileUpload" %>

<%@ Register Src="../UserControl/ucCustomDropDownList.ascx" TagName="ucCustomDropDownList"
    TagPrefix="ucDDL" %>
<%@ Register Src="~/UserControl/ucCustomPager.ascx" TagName="ucCustomPager" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/iframe.js" type="text/javascript"></script>
    <script src="../Scripts/JSCustomFilterControls.js" type="text/javascript"></script>
    <script src="../Scripts/ucCustomDropDownList.js" type="text/javascript"></script>
      <script src="../Scripts/ModalPopUp.js?v=1" type="text/javascript"></script>
    <link href="../Styles/CssCustomFilterControls.css" rel="stylesheet" type="text/css" />
        <script language="javascript" type="text/javascript">


            function DocOpen(docpath) {
                window.open(docpath);
            }

            function previewDocument(docPath) {
                document.getElementById("ifrmDocPreview").src = docPath;
            }
            function ValidationOnRetrieve() {
                //         debugger; 
                var cmdFleet = document.getElementById("ctl00_MainContent_DDLFleet").value;
                var cmdVessels = document.getElementById("ctl00_MainContent_DDLVessel").value;

                if (cmdFleet == "0" || cmdFleet == null) {
                    alert("Select fleet, vessel and click Retrieve button.");
                    return false;
                }

                if (cmdVessels == "ALL" || cmdVessels == null) {
                    alert("Select vessel and click Retrieve button.");
                    return false;
                }
                return true
            }

            function getImageopen(str) {
                window.open(str, "file", "menubar=0,resizable=0,width=750,height=550,resizeable=yes");
            }


            function OnAddAttachment() {
                var cmdFleet = document.getElementById("ctl00_MainContent_DDLFleet").value;
                var cmdVessels = document.getElementById("ctl00_MainContent_DDLVessel").value;



                if (cmdFleet == "0" || cmdFleet == null) {
                    alert("Select fleet, vessel and click Attach button.");
                    return false;
                }

                if (cmdVessels == "ALL" || cmdVessels == null) {
                    alert("Select vessel and click Attach button.");
                    return false;
                }


                return true;
            }

            function fn_OnClose() {
                $('[id$=btnLoadFiles]').trigger('click');
                //__doPostBack('ctl00_MainContent_btnLoadFiles', true);            
            }
    </script>
    <style type="text/css">
        .listbox
        {
            border: 0px;
        }
        .SelectedNodeStyle
        {
            background: url(../Images/bg.png) left -1672px repeat-x;
        }
        .pager span
        {
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
            background: url(../Images/bg.png) left -1672px repeat-x;
            font-size: 14px;
        }
        .pager a
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:link
        {
            color: black;
            background-color: transparent;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:visited
        {
            color: blue;
            background-color: white;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        .pager a:hover
        {
            color: blue;
            background-color: #efefef;
            text-decoration: none;
            border: 1px solid gray;
            padding: 0px 3px 0px 3px;
        }
        </style>

    <style type="text/css">

.tooltip {

 position: relative;
 
}
        .style2
        {
            width: 278px;
        }
        .style3
        {
            height: 34px;
        }
        .style4
        {
            height: 18px;
        }
        .style5
        {
            height: 67px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdateProgress ID="upUpdateProgress" DisplayAfter="1" runat="server">
        <ProgressTemplate>
            <div id="blur-on-updateprogress">
                &nbsp;</div>
            <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                color: black">
                <img src="../Images/loaderbar.gif" alt="Please Wait" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table style="width: 100%; margin-top: 5px;">
                <tr>
                    <td style="vertical-align: top; border: 1px solid #cccccc;width:20%" class="style2">
                        <div style="height: 600px; overflow: auto;">
                            
                            <asp:TreeView ID="treeDeptFolders" runat="server"  NodeIndent="15"
                                Width="100%" BorderColor="#cccccc" 
                              
                                onselectednodechanged="TreeView1_SelectedNodeChanged" ImageSet="XPFileExplorer">
                                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="2px"
                                    NodeSpacing="0px" VerticalPadding="2px" />
                                <ParentNodeStyle Font-Bold="False" />
                                <HoverNodeStyle Font-Underline="False" ForeColor="#6666AA" BackColor="#99FF66" />
                                <SelectedNodeStyle Font-Underline="False" ForeColor="#6666AA" BackColor="#99FF66" />
                            </asp:TreeView>
                            
                        </div>
                       
                    </td>
                       <td style="vertical-align: top; border: 1px solid #cccccc; width:80%">
                       <table style="width: 100%; margin-top: 5px;">
                                  <tr>
                                 <td style="vertical-align: top; border: 1px solid #cccccc;">
                                    <div id="Div1" title="Add Attachments">
                                       
                                    <table  style="width:100%">
                                        <caption>
                                            <table>
                                            <tr>
                                            <td>Search</td>
                                            <td>
                                            <asp:TextBox ID="txtSearchText" runat="server"
                                                MaxLength="20"></asp:TextBox>
                                           
                                            </td>
                                            <td>
                                             <asp:ImageButton ID="btnSearch" runat="server"  
                                                ImageUrl="~/Images/SearchButton.png" Text="Search" OnClick="btnSearch_Click" CausesValidation="false"
                                                ToolTip="Search" />
                                            </td>
                                            </tr>
                                            </table>
                                           
                                          
                                        </caption>
                                      </tr>
                                      <tr>
                                          <td style="width:50%">
                                              <div style="height:100%;">
                                                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                                      EmptyDataText="NO RECORDS FOUND" GridLines="Both" 
                                                      PagerStyle-Mode="NextPrevAndNumeric" Width="100%">
                                                      <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                                      <RowStyle CssClass="RowStyle-css" />
                                                      <HeaderStyle CssClass="HeaderStyle-css" Height="35px" />
                                                      <SelectedRowStyle BackColor="#FFFFCC" />
                                                      <EmptyDataRowStyle Font-Bold="true" Font-Size="12px" ForeColor="Red" 
                                                          HorizontalAlign="Center" />
                                                      <%--  <RowStyle Wrap="False" />--%>
                                                      <Columns>
                                                          <asp:TemplateField HeaderText="File Name">
                                                              <ItemTemplate>
                                                                  <asp:LinkButton ID="lbtnPreview" runat="server" 
                                                                      CommandArgument='<%#Eval("File_id_seq")%>' OnClick="lbtnPreview_Click" 
                                                                      Text='<%#Eval("File_Name") %>' ToolTip="Preview" Width="148px" CausesValidation="false" > </asp:LinkButton>
                                                              </ItemTemplate>
                                                              <ItemStyle CssClass="PMSGridItemStyle-css" HorizontalAlign="Left" Width="350px" 
                                                                  Wrap="true" />
                                                              <HeaderStyle HorizontalAlign="Left" Wrap="true" />
                                                          </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="File_id_sequence" Visible="false">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="LabelId" runat="server" Text='<%#Eval("File_id_seq")%>' 
                                                                      Visible="false"> </asp:Label>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Created By">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblName" runat="server" Text='<%#Eval("Created_By_Name")%>'> </asp:Label>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Date Of Creation">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblCreation" runat="server" Text='<%#Eval("Date_Of_Creation")%>'> </asp:Label>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Title" Visible="true">
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Info_Title")%>' 
                                                                      Visible="true"> </asp:Label>
                                                              </ItemTemplate>
                                                          </asp:TemplateField>
                                                      </Columns>
                                                  </asp:GridView>
                                                  <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" 
                                                      OnBindDataItem="bindGrid" PageSize="10" />
                                              </div>
                                          </td>
                                  </tr>
                                  </caption>
                                      </table>
                                      </td>
                                      </tr>
                                        </table> 
                                    <table style="width: 100%;height:500px; margin-top: 5px;">
                                    <tr>
                                    <td>
                                        <asp:Panel ID="pnlDDL" runat="server" Visible="False">
                                     
                                    <table style="width: 100%;">
                                    <tr>
                                    <td style="width:32.2%">
                                                           <asp:Label ID="lblPrimary" runat="server" Text="Primary Text"></asp:Label>
                                                       </td>
                                    <td style="width:32.2%">
                                        <asp:DropDownList ID="primaryDDL" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="***Required" ControlToValidate="primaryDDL" ></asp:RequiredFieldValidator>

                                    </td>
                                     </tr>
                                  
                                    </table>
                                       </asp:Panel>
                                    </td>
                                    
                                    </tr>
                                        <tr>
                                            <td align="left" style="width: 50%; vertical-align:top">
                                         
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:Panel ID="pnlTitle" runat="server" Visible="False">
                                               
                                               <table style="width:100%">
                                                   <tr>
                                                       <td style="width:32.2%">
                                                           <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
                                                       </td>
                                                       <td style="width:32.2%">
                                                           <asp:TextBox ID="txtTitle" runat="server" Width="150px"></asp:TextBox>
                                                       </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="RFVTitle" runat="server" ErrorMessage="***Required" ControlToValidate="txtTitle" ></asp:RequiredFieldValidator>
                                                       </td>
                                                   </tr>
                                                    <tr>
                                                       <td >
                                                       <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                                                       </td>
                                                       <td >
                                                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="150px"></asp:TextBox>
                                                       </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="RFVDesc" runat="server" ErrorMessage="***Required" ControlToValidate="txtDescription" ></asp:RequiredFieldValidator>

                                                       </td>
                                                   </tr>
                                               </table>
                                               <table>
                                               <tr>
                                               <td>
                                               </td>
                                               <td style="width:100%">
                                               <center>
                                               <tlk4:AjaxFileUpload ID="AjaxFileUpload2" runat="server" Padding-Bottom="2" Padding-Left="2" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                                Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber"  MaximumNumberOfFiles="1"  />
                                </center>
                                               </td>
                                               </tr>
                                               </table>
                                                </asp:Panel>
                                            </td>
                                            <td style="width: 50%; height: 100%; vertical-align: top; overflow: hidden; border: 1px solid #cccccc">
                                                <iframe ID="ifrmDocPreview" height="100%" marginheight="0px" 
                                                    src="../Images/previewAttach.png" 
                                                    style="vertical-align: middle; text-align: center;" width="100%"></iframe>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="style3" colspan="2">
                                                <asp:Panel ID="Panel1" runat="server" Width="90%">
                                                    <table>
                                                        <tr>
                                                           <%-- <td>
                                                                  <img src="../Images/AddAttachment.png" onclick="showModal('dvPopupAddAttachment',true,fn_OnClose);" />

                                                                 
                                                            </td>--%>
                                                            <td>
                                                              
                                                                <asp:Button ID="Button1" runat="server" BackColor="#3498DB" Height="22px" 
                                                                    onclick="Button1_Click" Text="Save" Visible="false" Width="106px" />
                                                            </td>
                                                           
                                                        </tr>
                                                       
                                                    </table>
                                                    
                                                    <table>
                                                     <tr>
                                                        <td> <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
            </table>
            </div>
            </div>
            </td>
                                    </tr>
                   
                          
                  </table>
            </tr>
                   
                          
                  </td>
               
               
                 
                </tr>
            </table>
      
            
                                            
                                            
                               
            </td>
            </tr>
            </table>
      
           
                     <div id="dvPopupAddAttachment" title="Add Attachments" style="display: none; width:500px ">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left">
                            <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="uploading.gif"/></asp:Label>
                            <tlk4:AjaxFileUpload ID="AjaxFileUpload1" runat="server" Padding-Bottom="2" Padding-Left="2" OnUploadComplete="AjaxFileUpload1_OnUploadComplete"
                                Padding-Right="1" Padding-Top="2" ThrobberID="myThrobber"  MaximumNumberOfFiles="1"  />
                        </td>
                    </tr>
                </table>
    </div>                            
                                            
                               
        </ContentTemplate>

    </asp:UpdatePanel>
  
   
                                   
</asp:Content>