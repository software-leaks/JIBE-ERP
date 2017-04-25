<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DisplaymodeUserControl.ascx.cs" Inherits="CustomControl_DisplaymodeUserControl" %>
<div id="divper"  style="display:none;float:left"> 
    
  <asp:Panel ID="Panel1" runat="server" 
    Borderwidth="1" 
    Width="130px" 
    BackColor="lightgray"
    Font-Names="Verdana, Arial, Sans Serif" >
    <div style="float:left">
      <asp:Label ID="Label1" runat="server" 
      Text="&nbsp;Display Mode" 
      Font-Bold="true"
      Font-Size="8"
      Width="100" />
      </div>
    <div style="float:right">
     <asp:ImageButton ID="clos" runat="server"  ImageUrl="~/images/close1.png" OnClientClick="return cross();" ToolTip="Close" />
   </div>  
   
    <div>
    <asp:DropDownList ID="DisplayModeDropdown" runat="server"  
      AutoPostBack="true" 
      Width="120"
      OnSelectedIndexChanged="DisplayModeDropdown_SelectedIndexChanged" /><br />
    <asp:LinkButton ID="LinkButton1" runat="server"
      Text="Reset User State" 
      ToolTip="Reset the current user's personalization data for 
      the page."
      Font-Size="8" 
      OnClick="LinkButton1_Click" />
    </div>
    <asp:Panel ID="Panel2" runat="server" 
      GroupingText="Personalization Scope"
      Font-Bold="true"
      Font-Size="8" 
      Visible="false" >
      <asp:RadioButton ID="RadioButton1" runat="server" 
        Text="User" 
        AutoPostBack="true"
        GroupName="Scope" 
        OnCheckedChanged="RadioButton1_CheckedChanged" />
      <asp:RadioButton ID="RadioButton2" runat="server" 
        Text="Shared" 
        AutoPostBack="true"
        GroupName="Scope" 
        OnCheckedChanged="RadioButton2_CheckedChanged" />
    </asp:Panel>
  </asp:Panel>
  </div>