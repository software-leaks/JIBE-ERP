<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Worklist_Crew_Involved.aspx.cs"
    Inherits="Worklist_Crew_Involved" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Scripts/ModalPopUp.js" type="text/javascript"></script>
    <script src="../../Scripts/uploadify/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>
    <script src="../../Scripts/uploadify/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
    <script src="../../Scripts/uploadify/swfobject.js" type="text/javascript"></script>
    <link href="../../Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/common_functions.js" type="text/javascript"></script>     
    <script src="../../Scripts/StaffInfo.js" type="text/javascript"></script>     
    <script language="javascript" type="text/javascript">

        function AddNewMaintenanceFeedback(crewid, vid, jid, wlid, offid, voygeid) {

            $('#dvPopupFrame').attr("Title", "Add New Maintenance Feedback");
            $('#dvPopupFrame').css({ "width": "500px", "height": "400px", "text-allign": "center" });
            $('#frPopupFrame').load(function () { this.style.height = (this.contentWindow.document.body.offsetHeight + 20) + 'px'; });

            var URL = "../../Crew/CrewDetails_MaintenanceFeedback.aspx?CrewID=" + crewid + "&VID=" + vid + "&WLID=" + wlid + "&OFFID=" + offid + "&JID=" + jid + "&voygeid=" + voygeid + "&Mode=ADD&rnd=" + Math.random();

            document.getElementById("frPopupFrame").src = URL;
            showModal('dvPopupFrame', true);

        }
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows

                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original

                        inputList[i].checked = false;
                    }
                }
            }
        }



        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;


            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Script1" runat="server">
    </asp:ScriptManager>
    <center>
      <div align="center" id="tstdiv">
        <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
    </div>
        <div id="dvWorkListCrewInvolved" style="font-family: Tahoma; font-size: 12px;" runat="server">
            <div style="height:720px; overflow-x: hidden; overflow-y: scroll">
                
                <table style="width:100%">
                <tr>
                <td colspan="5"  style="text-align:left">
                  
                  
                                <b>All crew members associated with vessel <asp:Label ID="lblVesselName" runat="server"></asp:Label></b>
                          
                       
            
                </td>
                </tr>
                <tr>
                <td style="font-weight:bold;text-align:right">Search (Name,Code) :</td>
                <td style="text-align:left"><asp:TextBox ID="txtSearch" runat="server"  /> </td>
                <td style="text-align:right"> <table>
                <tr>
                <td style="font-weight:bold">
                Status :
                </td>
                <td>
                    <asp:RadioButtonList ID="rdbStatus" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="ALL" Selected="True"  Value="3"></asp:ListItem>
                 <asp:ListItem Text="Current"    Value="2"></asp:ListItem>
                  <asp:ListItem Text="Signed Off"    Value="1"></asp:ListItem>
                </asp:RadioButtonList>
                </td>
                </tr>
                </table>
            
                </td>
                <td style="text-align:left">
                <asp:Button id="Search" Text="Search" runat="server" onclick="Search_Click"   BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                Height="24px" BackColor="#81DAF5" />
                </td>
                </tr>
                <tr>
                <td colspan="4" style="white-space:nowrap"><asp:GridView ID="grdAddCrewInvolved" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    CellSpacing="1" EmptyDataText="No Record Found!" GridLines="both" Width="100%" DataKeyNames="Voyage_ID"
                    AllowSorting="false">
                      <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                        <PagerStyle CssClass="PagerStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                               <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                                        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                                        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                                        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                    <Columns>
                        <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblDATE_CREATED" runat="server" Text='<%#Eval("CREATED_DATE","{0:dd/MM/yyyy}").ToString() == "01/01/1900" ? "" : Eval("CREATED_DATE","{0:dd/MMM/yy}")   %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Staff Code" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="lblStaff_NAME" runat="server" NavigateUrl='<%# "../../Crew/CrewDetails.aspx?ID=" + Eval("CrewID")%>'
                                    Target="_blank" Text='<%# Eval("Staff_Code")%>' CssClass="staffInfo pin-it"></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Staff Name">
                            <ItemTemplate>
                                <asp:Label ID="lblStaff_FullName" runat="server" Text='<%#Eval("Staff_FullName").ToString()%>' ></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rank">
                            <ItemTemplate>
                                <asp:Label ID="lblRank" runat="server" Text='<%#Eval("Rank_Short_Name").ToString()%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css" />
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="Joining Date">
                            <ItemTemplate>
                                <asp:Label ID="lblJoining" runat="server" Text='<%#Eval("JOINING_DATE").ToString()%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css" />
                        </asp:TemplateField>

                        
                        <asp:TemplateField>
                                            <HeaderTemplate >
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="checkRow" runat="server" onclick="Check_Click(this)"  Checked='<%# SelectCheckbox(Eval("Voyage_ID").ToString()) %>'  />
                                            </ItemTemplate>
                                              <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css" />
                                              <HeaderStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css" />
                                        </asp:TemplateField>

                    </Columns>
                </asp:GridView></td>
                </tr>
                <tr>
                <td colspan="3">   <auc:CustomPager ID="ucCustomPagerctp" OnBindDataItem="BindCew" AlwaysGetRecordsCount="true" PageSize="10"
                                        RecordCountCaption="Total Crew" runat="server" /></td><td> 
                        <asp:Button id="btnAdd" Text="Add Selected" runat="server" onclick="btnAdd_Click"   BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                Height="24px" BackColor="#81DAF5"   /></td>
                </tr>
                <tr>
                <td colspan=""4 style="text-align:left">
                  
                  
                                <b> All crew members  involved in this job </b>
                          
                       
            
                </td>
                </tr>
                <tr>
                <td colspan="4"><asp:GridView ID="grdCrewInvolved" runat="server" AutoGenerateColumns="False" CellPadding="4"  
                    CellSpacing="1" EmptyDataText="No Record Found!" GridLines="both" Width="100%"
                    AllowSorting="false">
                      <HeaderStyle CssClass="HeaderStyle-css Crew-HeaderStyle-css" />
                                        <PagerStyle CssClass="PagerStyle-css" />
                                        <RowStyle CssClass="RowStyle-css" />
                               <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle-css" />
                                        <SortedAscendingCellStyle CssClass="SortedAscendingCellStyle-css" />
                                        <SortedAscendingHeaderStyle CssClass="SortedAscendingHeaderStyle-css" />
                                        <SortedDescendingCellStyle CssClass="SortedDescendingCellStyle-css" />
                                        <SortedDescendingHeaderStyle CssClass="SortedDescendingHeaderStyle" />
                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" Font-Bold="true" Font-Size="12px" />
                    <Columns>
                        <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblDATE_CREATED" runat="server" Text='<%#Eval("CREATED_DATE","{0:dd/MM/yyyy}").ToString() == "01/01/1900" ? "" : Eval("CREATED_DATE","{0:dd/MMM/yy}")   %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="30px" CssClass="PMSGridItemStyle-css" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Staff Code" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="lblStaff_NAME" runat="server" NavigateUrl='<%# "../../Crew/CrewDetails.aspx?ID=" + Eval("CrewID")%>'
                                    Target="_blank" Text='<%# Eval("Staff_Code")%>' CssClass="staffInfo pin-it"></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="50px" CssClass="PMSGridItemStyle-css" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Staff Name">
                            <ItemTemplate>
                                <asp:Label ID="lblStaff_FullName" runat="server" Text='<%#Eval("Staff_FullName").ToString()%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="100px" CssClass="PMSGridItemStyle-css" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rank">
                            <ItemTemplate>
                                <asp:Label ID="lblRank" runat="server" Text='<%#Eval("Rank_Short_Name").ToString()%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css" />
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Joining Date">
                            <ItemTemplate>
                                <asp:Label ID="lblJoining" runat="server" Text='<%#Eval("JOINING_DATE").ToString()%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="40px" CssClass="PMSGridItemStyle-css" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Feed back">
                            <HeaderTemplate>
                                Feed back
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Button ID="btnFeedback" Text="FeedBack" runat="server" OnClientClick='<%# "AddNewMaintenanceFeedback("+ Eval("CrewID").ToString() + ","+ Eval("VESSEL_ID").ToString() +","+Eval("Job_ID").ToString() +","+ Eval("WORKLIST_ID").ToString() +"," + Eval("OFFICE_ID").ToString() +","+ Eval("VOYAGE_ID").ToString() + ");return false;" %>' />
                            </ItemTemplate>
                            <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css">
                            </ItemStyle>
                             <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Action">
                          <ItemTemplate>
                                                            <asp:ImageButton ImageUrl="~/Images/Delete-icon.png" ID="btnDelete" Text="Delete" runat="server"  CommandArgument='<%#Eval("ID")%>' CommandName="Delete" OnClick="onDelete"  Enabled='<%# ViewState["del"].ToString()=="1"?true:false %>' ToolTip="Delete" />
 
                          </ItemTemplate>
                           <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px" CssClass="PMSGridItemStyle-css" />
                           <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                          </asp:TemplateField>
                    </Columns>
                </asp:GridView></td>
                </tr>
                </table>
                
              
                                       
                
               
            </div>
        </div>
    </center>
    <div id="dvPopupFrame" class="draggable" style="display: none; background-color: #CBE1EF;
        border-color: #5C87B2; border-style: solid; border-width: 1px; position: absolute;
        left: 0.5%; top: 15%; width: 900px; z-index: 1; color: black" title=''>
        <div class="content">
            <iframe id="frPopupFrame" src="" frameborder="0" height="100%" width="100%"></iframe>
        </div>
    </div>
    </form>
</body>
</html>
