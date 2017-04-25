<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CPEntry.aspx.cs"
    Inherits="Operation_CPEntry" Title="CP Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 <%@ Register Src="~/UserControl/ucCustomPager.ascx"TagName="ucCustomPager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <link href="../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript">
         function isNumberKey(evt) {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if ((charCode > 31 && (charCode < 48 || charCode > 57)) && charCode != 46)
                 return false;

             return true;
         }
         function validation() {
             if (document.getElementById("ctl00_MainContent_ddlvesselCP").value == "0") {
                 alert("Please select vessel.");
                 document.getElementById("ctl00_MainContent_ddlvesselCP").focus();
                 return false;
             }
             if (document.getElementById("ctl00_MainContent_ddldatatypeCP").value.trim() == "0") {
                 alert("Please select data type.");
                 document.getElementById("ctl00_MainContent_ddldatatypeCP").focus();
                 return false;
             }
             if (document.getElementById("ctl00_MainContent_txtdatavalue").value.trim() == "") {
                 alert("Please enter data value.");
                 document.getElementById("ctl00_MainContent_txtdatavalue").focus();
                 return false;
             }
           

             
             return true;
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdateProgress ID="UpdateProgress1"  runat="server">
            <ProgressTemplate>
                <div id="blur-on-updateprogress">
                    &nbsp;</div>
                <div id="divProgress" style="position: absolute; left: 49%; top: 30px; z-index: 201;
                    color: black">
                 
                   <img src="../Images/loaderbar.gif"alt="Please Wait" />
                </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <%--  <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>--%>
         <ContentTemplate>
         <div style="font-family: Tahoma; font-size: 12px">
             <center>
        <div style="border: 1px solid  #5588BB;">
            <div class="page-title">
       
              CP Entries
              </div>

              
                  <div style="vertical-align: middle; padding-top: 6px">
               
                   
                  
                            <div style="padding-top: 2px; padding-bottom: 5px; width: 100%">
                               <table width="100%" cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td align="right">
                                            Fleet :&nbsp;&nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="DDLFleet" runat="server"  Width="150px"
                                                AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DDLFleet_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">SELECT ALL</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" colspan="2">
                                            <div style="border: 1px solid #cccccc; text-align: center;">
                                               <%--  <asp:RadioButton ID="rbtnLatest" runat="server" Text="Latest" GroupName="type" AutoPostBack="true"
                                                    OnCheckedChanged="rbtnLatest_CheckedChanged" />
                                                     
                                                &nbsp;
                                                <asp:RadioButton ID="rbtnHistory" runat="server" Text="History" GroupName="type"
                                                    AutoPostBack="true" OnCheckedChanged="rbtnHistory_CheckedChanged" />--%>

                                            <asp:RadioButtonList ID="rdoCPEntryFlag" runat="server" RepeatDirection="Horizontal"
                                            CellPadding="0">
                                            <asp:ListItem Text="Latest" Value="1"></asp:ListItem>

                                            <asp:ListItem Text="History" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                                   
                                            </div>
                                        </td>
                                        <td>
                                        </td>
                                        <td align="left">
                                            <asp:ImageButton ID="lbtnaddentry" ImageUrl="~/Images/AddCP.png" runat="server" OnClick="lbtnaddentry_Click">
                                            </asp:ImageButton>
                                        </td>
                                           <td style="text-align: right">
                                             <asp:Button ID="btnSearch" runat="server" Width="100px"  Text="Search"  CssClass="btnCSS" 
                                                   onclick="btnSearch_Click">
                                          </asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Vessel :&nbsp;&nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlvessel" Width="150px" runat="server"
                                                AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            Data Type :&nbsp;&nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlDatatype"  Width="130px" runat="server">
                                               
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            User :&nbsp;&nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlUser"  Width="110px" runat="server">
                                               
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right">
                                           <asp:Button ID="BtnClearFilter"  runat="server" OnClick="BtnClearFilter_Click" Width="100px" Text="Clear Filter"
                                        CssClass="btnCSS" />
                                </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                               <div style="border: 1px solid gray; margin-top: 2px">
                                <asp:GridView ID="gvCPEntry" runat="server" EmptyDataText="NO RECORDS FOUND!" AutoGenerateColumns="False"
                                   OnRowDataBound="gvCPEntry_RowDataBound" GridLines="Both" AllowSorting="true" DataKeyNames="ID"
                                    CssClass="GridView-css"  Width="100%"  PageSize="20"
                                    CellPadding="4"  OnSorted="gvCPEntry_Sorted" OnSorting="gvCPEntry_Sorting">
                                  
                                    <HeaderStyle CssClass="HeaderStyle-css" />
                                    <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <RowStyle CssClass="RowStyle-css" />
                                    <PagerStyle CssClass="PagerStyle-css" />
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                                    <%--<Columns>
                                        <asp:BoundField DataField="id" HeaderText="id" Visible="false" />
                                        <asp:BoundField DataField="Vessel_Name" HeaderStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Left" HeaderText="Vessel"
                                            ItemStyle-HorizontalAlign="Left" SortExpression="Vessel_Name">
                                         
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Datatype" HeaderStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Left" HeaderText="Data Type" HeaderStyle-Width="100px"
                                           ItemStyle-HorizontalAlign="Left"  SortExpression="Datatype">
                                         
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Data_value" HeaderStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" SortExpression="Data_value"
                                            HeaderText="Data Value" />
                                        <asp:BoundField DataField="username" HeaderStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Left" HeaderText="User Name" 
                                         ItemStyle-HorizontalAlign="Left"  SortExpression="username">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="created_date" HeaderStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Left" HeaderText="Date" 
                                         ItemStyle-HorizontalAlign="Left" SortExpression="strdate">
                                        
                                        </asp:BoundField>
                                    </Columns>--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lblFleetCodeHeader" runat="server" CommandName="Sort" CommandArgument="id"
                                                    ForeColor="Black">id&nbsp;</asp:LinkButton>
                                                <img id="FLEETCODE" runat="server" visible="false" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblFleetCode" runat="server" Text='<%#Eval("id")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="150px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                      <%--  <asp:TemplateField HeaderText="Vessel">
                                            <HeaderTemplate>
                                             Vessel Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVesselManager" runat="server" Text='<%#Eval("Vessel_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>--%>
                                     
                                       <asp:TemplateField HeaderText="Vessel">
                                    <HeaderTemplate>
                                        <asp:LinkButton ID="lblVesseleNameHeader" runat="server" ForeColor="Black" CommandName="Sort"
                                            CommandArgument="Vessel_Name">
                                            Vessel Name &nbsp;
                                        </asp:LinkButton>
                                        <img id="Vessel_Name" runat="server" visible="false" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblVesselName" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Name") %>'></asp:Label>
                                       <%-- <asp:Label ID="lblVesselCode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Vessel_Code") %>'></asp:Label>--%>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="true" HorizontalAlign="Left" Width="120px" CssClass="PMSGridItemStyle-css">
                                    </ItemStyle>
                                </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Data Type">
                                            <HeaderTemplate>
                                                Data Type
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSuptdEmail" runat="server" Text='<%#Eval("Datatype")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Data_value">
                                            <HeaderTemplate>
                                                Data value
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTechTeamEmail" runat="server" Text='<%#Eval("Data_value")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <HeaderTemplate>
                                              User Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVesselManager" runat="server" Text='<%#Eval("username")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <HeaderTemplate>
                                              Date
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVesselManager1" runat="server" Text='<%#Eval("created_date")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                            <HeaderStyle Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Action">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="ImgUpdate" runat="server" Text="Update" OnCommand="onUpdate"
                                                                Visible='<%# uaEditFlag %>' CommandArgument='<%#Eval("[FLEETCODE]")%>' ForeColor="Black"
                                                                ToolTip="Edit" ImageUrl="~/Images/Edit.gif" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="ImgDelete" runat="server" Text="Delete" OnCommand="onDelete"
                                                                Visible='<%# uaDeleteFlage %>' OnClientClick="return confirm('Are you sure want to delete?')"
                                                                CommandArgument='<%#Eval("[FLEETCODE]")%>' ForeColor="Black" ToolTip="Delete"
                                                                ImageUrl="~/Images/delete.png" Height="16px"></asp:ImageButton>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="imgRecordInfo" ImageAlign="AbsMiddle" ImageUrl="~/Images/RecordInformation.png"
                                                                Height="16px" Width="16px" runat="server" onclick='<%# "Get_Record_Information(&#39;LIB_FLEETS&#39;,&#39;FleetCode="+Eval("FLEETCODE").ToString()+"&#39;,event,this)" %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                                            </ItemStyle>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                   
                                </asp:GridView>
                                  <uc1:ucCustomPager ID="ucCustomPagerItems" runat="server" PageSize="30" OnBindDataItem="BindCPEntries" />
                                <asp:HiddenField ID="HiddenFlag" runat="server" EnableViewState="False" /> 
                               
      
    </div>
                            </div>
                            <div id="divadd" title="<%= OperationMode %>" style="display: none; font-family: Tahoma;
                                text-align: left; font-size: 12px; color: Black; width: 25%">
                                <table width="100%" cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td align="right">
                                            Select Vessel :<asp:Label ID="lbl1" runat="server" ForeColor="#FF0000"
                                                    Text="*"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlvesselCP" CssClass="dropdown-css" Width="150px" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Select Data Type :<asp:Label ID="Label1" runat="server" ForeColor="#FF0000"
                                                    Text="*"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddldatatypeCP" CssClass="dropdown-css" Width="150px" runat="server" />                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            Enter Data Value :<asp:Label ID="lbl2" runat="server" ForeColor="#FF0000"
                                                    Text="*"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtdatavalue" CssClass="textbox-css" onkeypress="return isNumberKey(evt)"  Width="145px" runat="server"></asp:TextBox>
                              
                                           <%--  <asp:CompareValidator ID="cmpvdatavalue" SetFocusOnError="true" runat="server" ControlToValidate="txtdatavalue"
                                                Type="Double" Operator="DataTypeCheck" ErrorMessage="only numbers allowed !"></asp:CompareValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblmsg" Font-Size="11px" ForeColor="White" runat="server"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="btnSave" runat="server" Height="30px" Width="70px" OnClick="btnSave_Click"  OnClientClick="return validation();"
                                                Text="Save" CausesValidation="true" />
                                        </td>
                                    </tr>
                                </table>
                                 </center>
                            </div>
                    
                    </ContentTemplate>
                </asp:UpdatePanel>
       
</asp:Content>
