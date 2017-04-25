<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Rating.ascx.cs" Inherits="UserControl_Rating" %>
<asp:Panel ID="Panel1" runat="server" Height="30px" Width="130px">
    <table cellpadding ="0" cellspacing="0">
        <tr >
            <td >
                <asp:Image ID="Image1" ImageUrl="~/Images/Smiley_5.png" Height="12px"  Width="12px" runat="server" Style="vertical-align: bottom;" ToolTip="Outstanding"/>
            </td>
            <td>
                <asp:Image ID="Image2" ImageUrl="~/Images/Smiley_4.png" Height="12px"  Width="12px" runat="server" Style="vertical-align: bottom;"  ToolTip="Above Average"/>
              </td>
            <td>  
             <asp:Image ID="Image3" ImageUrl="~/Images/Smiley_3.png" Height="12px"  Width="12px" runat="server" Style="vertical-align: bottom;" ToolTip="Average"/>
             </td>
            <td>   
             <asp:Image ID="Image4" ImageUrl="~/Images/Smiley_2.png" Height="12px"  Width="12px" runat="server" Style="vertical-align: bottom;" ToolTip="Below Average"/>
              </td>
            <td> 
              <asp:Image ID="Image5" ImageUrl="~/Images/Smiley_1.png" Height="12px"  Width="12px" runat="server" Style="vertical-align: bottom;" ToolTip="Unacceptable"/>
            </td>
        </tr>
        <tr  >
            <td >
                <asp:RadioButton ID="Radio5" runat="server" Enabled="false" />
                 </td>
            <td>
                <asp:RadioButton ID="Radio4" runat="server" Enabled="false" />
                 </td>
            <td>
                <asp:RadioButton ID="Radio3" runat="server" Enabled="false" />
                 </td>
            <td>
                <asp:RadioButton ID="Radio2" runat="server" Enabled="false" />
                 </td>
            <td>
                <asp:RadioButton ID="Radio1" runat="server" Enabled="false" />
            </td>
        </tr>
    </table>
</asp:Panel>