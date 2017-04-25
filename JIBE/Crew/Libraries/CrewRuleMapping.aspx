<%@ Page Title="Crew Rule Mapping" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="CrewRuleMapping.aspx.cs" Inherits="Crew_Libraries_CrewRuleMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../../Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/crew_css.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/SMSPopup.css" rel="stylesheet" type="text/css" />
    <script>
        var previousRow;
        function ChangeRowColor(row) {
            if (previousRow == row)
                return;
            else if (previousRow != null)
                document.getElementById(previousRow).style.backgroundColor = "#ffffff"; document.getElementById(row).style.backgroundColor = "#ffffda";
            previousRow = row;
        }
        
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            document.getElementById("error").style.display = ret ? "none" : "inline";
            return ret;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div id="page-content" style="border: 1px solid #cccccc; z-index: -2; overflow: auto;">

        <div style="min-height: 600px; color: Black; margin-bottom: 25px;">
        <div class="page-title">
            Crew Rule Mapping
        </div>
        <div align="center">
            <table id="tblRule" runat="server">
                <tr>
                    <td>
                        Parameters Count:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNo" AutoPostBack="true" runat="server"  onkeypress='return event.charCode >= 48 && event.charCode <= 57'  ontextchanged="txtNo_TextChanged"></asp:TextBox>
                        <asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click" />
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        Rule Description
                    </td>
                    <td>
                        <asp:TextBox ID="txtRule" runat="server" Width="200px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
             
                <tr>
                    <td  align="center" colspan="2">
                    <table>
                    <tr>
                    <td>
                      <asp:Button ID="btnRule" runat="server" Text="Create Rule" OnClick="btnRule_Click" />
                    
                    
                        <asp:Button ID="btnUpdate" runat="server" Text="Update Rule" Visible="false" 
                             onclick="btnUpdate_Click"  />
                    </td>
                    <td>
                      <asp:ImageButton ID="lnlDelete" runat="server"
                                                ForeColor="Black"  ToolTip="Refresh"
                                                ImageUrl="~/Images/Refresh.png" 
                            onclick="lnlDelete_Click"  />
                    </td>
                    </tr>
                    </table>
                      
                           
                    </td>
                  
                </tr>
            </table>
         

         <asp:GridView runat="server" DataKeyNames="Rule,Parameters,ID,Rulename"  
                   AutoGenerateColumns="false" EmptyDataText="No Records Found" runat="server" HeaderStyle-CssClass="gvheader" RowStyle-CssClass="gvRows"
                                                                    GridLines="Both" HeaderStyle-BackColor="#99ccff" Font-Size="11px" ShowHeaderWhenEmpty="false"  ID="gridRule" 
                onrowdatabound="gridRule_RowDataBound">


              
         <Columns>
          <asp:TemplateField HeaderText="Rule Name">
         <ItemTemplate>
         <asp:Label runat="server" ID="lblrulename" Text='<%#Eval("Rulename") %>' >
         </asp:Label>
         </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Rule">
         <ItemTemplate>
         <asp:Label runat="server" ID="lblrule" Text='<%#Eval("Rule") %>' >
         </asp:Label>
         </ItemTemplate>
         </asp:TemplateField>
          <asp:TemplateField  HeaderText="Parameters">
         <ItemTemplate>
         <asp:Label runat="server" ID="lblParameters"  Text='<%#Eval("Parameters") %>'>
         </asp:Label>
         </ItemTemplate>

         </asp:TemplateField>
            <asp:TemplateField  HeaderText="Action">
         <ItemTemplate>
          <asp:ImageButton ID="lblEdit" runat="server"
                                                ForeColor="Black" Height="20px"  OnClick="lblEdit_Click"
                                                ImageUrl="~/Images/Edit.gif"  />
       
            <asp:ImageButton ID="lnlDelete" runat="server"
                                                ForeColor="Black" Height="20px" OnClientClick="return confirm('Are you sure want to delete?')"  OnClick="lblDelete_Click"
                                                ImageUrl="~/Images/Delete.png"  />
    
         </ItemTemplate>

         </asp:TemplateField>
         </Columns>
         </asp:GridView>
        

            <br />


           

           
        </div>
    </div>
    </div>
</asp:Content>
