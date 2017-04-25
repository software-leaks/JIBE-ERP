<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignWorklist.aspx.cs" Inherits="Purchase_AssignWorklist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
      <script language="javascript" type="text/javascript">

       
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
                        if (inputList[i].disabled == false) {
                            inputList[i].checked = false;
                        }
                        else 
                        {
                            inputList[i].checked = true;
                        }
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
        function ConfirmDelete() {
            if (confirm("Are you sure to want to Delete?") == true)
                return true;
            else
                return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Script1" runat="server" >
    </asp:ScriptManager>
    <asp:UpdateProgress ID="upUpdateProgress" runat="server">
    <ProgressTemplate>
   <div id="divProgress" style="background-color: transparent; position: absolute; left: 49%;
    top: 20px; z-index: 2; color: black">
    <img src="../images/loaderbar.gif" alt="Please Wait" />
   </div>
  </ProgressTemplate>
 </asp:UpdateProgress>
    <asp:UpdatePanel ID="upfd" runat="server">
    <ContentTemplate>
   
    <center>
            <div style="font-family: Tahoma; font-size: 12px;height:720px; overflow-x: hidden; overflow-y: scroll">
       
                <table style="width:800px">
                <tr>
                <td style="font-weight:bold;text-align:left"><asp:Label ID="lblhdr1" runat="server" ClientIDMode="Static"  Text="Search (Worklist ID,Job Description) :"></asp:Label></td>
                <td style="text-align:left"><asp:TextBox ID="txtSearch" runat="server"/> </td>
              
                <td style="text-align:right">
                <asp:Button id="Search" Text="Search" runat="server" onclick="Search_Click"   BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                Height="24px" BackColor="#81DAF5" />
                </td>
                </tr>
                <tr>
                <td colspan="3"></td>
                </tr>
                <tr>
                <td colspan="3" align="left">
                <asp:Label ID="Label1" style="font-weight:bold" runat="server" Text="UnAssigned Worklist" ClientIDMode="Static"></asp:Label>
                </td>
                </tr>                </table>
        
         <div id="dvWorkListCrewInvolved" style="font-family: Tahoma; font-size: 12px;width:800px;overflow:auto">

        <asp:GridView ID="grdAddWorklistInvolved" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    CellSpacing="1" EmptyDataText="No Record Found!" GridLines="both" 
                 Width="800px" DataKeyNames="WORKLIST_ID"
                    AllowSorting="false" 
                 onrowdatabound="grdAddWorklistInvolved_RowDataBound">
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
                        
                        <asp:TemplateField HeaderText="Worklist ID" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblWrID" style="display:none" runat="server" Text='<%#Eval("WORKLIST_ID") %>'></asp:Label>
                                <asp:Label ID="lblOFFICE_ID" style="display:none" runat="server" Text='<%#Eval("OFFICE_ID") %>'></asp:Label>
                                <asp:HyperLink ID="lblWorklistID" runat="server" NavigateUrl='<%#"../Technical/Worklist/ViewJob.aspx?WLID=" + Eval("WORKLIST_ID") +"&OFFID="+Eval("OFFICE_ID")+"&VID="+Eval("VESSEL_ID")%>'
                                    Target="_blank" Text='<%# Eval("WLID_DISPLAY")%>'></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Wrap="false" HorizontalAlign="Left" Width="50px"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Job Description">
                            <ItemTemplate>
                                <asp:Label ID="lblJobDesc" runat="server" Text='<%#Eval("JOB_DESCRIPTION").ToString()%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Wrap="false" HorizontalAlign="Left" Width="100px"/>
                        </asp:TemplateField>
                       
                        <asp:TemplateField>
                            <HeaderTemplate >
                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="checkRow" runat="server" onclick="Check_Click(this)"  Checked='<%# SelectCheckbox(Eval("ID").ToString()) %>'  />
                               
                            </ItemTemplate>
                                <ItemStyle Wrap="false" HorizontalAlign="Center" Width="40px"/>
                                <HeaderStyle Wrap="false" HorizontalAlign="Center" Width="40px"/>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <table width="100%"><tr id="tdPager" runat="server"><td colspan="2"><auc:CustomPager ID="ucCustomPagerctp" 
                        OnBindDataItem="BindWorklist" AlwaysGetRecordsCount="true" PageSize="10" 
                                        RecordCountCaption="Total Worklist" runat="server" /></td></tr>
                                        <tr>
                                        <td align="left"><asp:Label ID="lblhdr" style="font-weight:bold" runat="server" Text="Assigned Worklist" ClientIDMode="Static"></asp:Label></td>
                                        <td align="right">
                                         <asp:Button id="btnAdd" Text="Add Selected" runat="server" onclick="btnAdd_Click"   BorderStyle="Solid" BorderColor="#0489B1" BorderWidth="1px" Font-Names="Tahoma"
                                Height="24px" BackColor="#81DAF5"   />   
                                        </td></tr>
                                        </table>
                
                                        </div>
                   
         <div style="overflow:auto">
              
                <asp:GridView ID="grdWorklistInvolved" runat="server" AutoGenerateColumns="False" CellPadding="4"  
                    CellSpacing="1" EmptyDataText="No Record Found!" GridLines="both" Width="800px"
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
                    
                        <asp:TemplateField HeaderText="Woklist ID" ItemStyle-HorizontalAlign="Center">
                         <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblOFFICE_ID" style="display:none" runat="server" Text='<%#Eval("OFFICE_ID") %>'></asp:Label>
                                <asp:HyperLink ID="lblWORKLIST_ID" runat="server" NavigateUrl='<%#"../Technical/Worklist/ViewJob.aspx?WLID=" + Eval("WORKLIST_ID") +"&OFFID="+Eval("OFFICE_ID")+"&VID="+Eval("VESSEL_ID")%>'
                                    Target="_blank" Text='<%# Eval("WLID_DISPLAY")%>'></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Wrap="false" HorizontalAlign="Left" Width="50px"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Job Description">
                        <ItemStyle Width="200px" />
                            <ItemTemplate>
                                <asp:Label ID="lblJobDescription" runat="server" Text='<%#Eval("JOB_DESCRIPTION").ToString()%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            <ItemStyle Wrap="false" HorizontalAlign="Left" Width="100px"/>
                        </asp:TemplateField>
                   
                          <asp:TemplateField HeaderText="Action">
                           <ItemStyle Width="100px" />
                          <ItemTemplate>
                             <asp:ImageButton ImageUrl="~/Images/Delete-icon.png" ID="btnDelete" Text="Delete" runat="server"  CommandArgument='<%#Eval("ID")+ "," +Eval("WORKLIST_ID")+ ","  +Eval("OFFICE_ID") %>' CommandName="Delete" OnClientClick="return confirm('Do you want to delete?');"  OnClick="onDelete"   ToolTip="Remove" />
                           </ItemTemplate>
                           <ItemStyle Wrap="true" HorizontalAlign="Center" Width="40px"/>
                           <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                          </asp:TemplateField>
                    </Columns>
                </asp:GridView>
           
                </div>
                </div>
    </center>
    </ContentTemplate></asp:UpdatePanel>
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
