<%@ Page Title="Exchange Rate" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Exch_Rate_Update.aspx.cs" Inherits="Exch_Rate_Update" %>
<%@ Register TagPrefix="RJS" Namespace="RJS.Web.WebControl" Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" %>

<asp:Content ID="contenthead" ContentPlaceHolderID="HeadContent" runat="server">
  <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
    function Divaddnewlink() {
        document.getElementById("divadd").style.display = "block";
    }



    function Validate(id) {

        var str = id.substring(0, 44);

        var pathrate = str + "_txtexchrate";
        var pathdate = str + "_txtdate";
        var exrate = document.getElementById(pathrate).value;
        var date = document.getElementById(pathdate).value;
        if (exrate == "") {
            inlineMsg(pathrate, '<strong></strong><img src="../images/warng.gif" style="filter:alpha(opacity=100);opacity:0;" />Please enter exch rate.', 2);
            return false;
        }
        if (date == "") {
            inlineMsg(pathdate, '<strong>Error</strong>  <img  src="../images/warng.gif"  /> <br />Please enter date.', 2);
            return false;
        }
        return true;

    }
    function chechlink() {

        var lname = document.getElementById("ctl00_ContentPlaceHolder1_DropDownListcurrency").value;
        var lurl = document.getElementById("ctl00_ContentPlaceHolder1_txtexchrate").value;
        var txtdate = document.getElementById("ctl00_ContentPlaceHolder1_txtdate").value;
        if (lname == "") {
            inlineMsg('ctl00_ContentPlaceHolder1_DropDownListcurrency', '<strong></strong><br />Please select.', 2);
            return false;
        }

        if (lurl == "") {
            inlineMsg('ctl00_ContentPlaceHolder1_txtexchrate', '<strong>Error</strong><br />Please enter Exch rate.', 2);
            return false;
        }
        if (txtdate == "") {
            inlineMsg('ctl00_ContentPlaceHolder1_txtdate', '<strong>Error</strong><br />Please enter date', 2);
            return false;
        }
        return true;
    }
</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">



<div id="page-header">
Update Exchange Rate
</div>
<div id="page-content" >
   
    
   
  
     
<div style="position:relative;text-align:center;margin-left:auto;margin-right:auto;">
<asp:UpdatePanel ID="updgrid"  runat="server">
<ContentTemplate>

<table><tr><td> 
    <asp:DropDownList ID="DropDownListfilter" CssClass="dropdown-css" runat="server">
        <asp:ListItem Value="Currency_Type">Currency Code</asp:ListItem>
       
     </asp:DropDownList>
<asp:TextBox ID="txtfilter" CssClass="textbox-css" runat="server"></asp:TextBox>
    <asp:Button ID="btnfilter" CssClass="button-css"  runat="server" Text="Filter" OnClick="btnfilter_Click"  /> 
    <asp:CheckBox ID="chkshowall" runat="server" Text="Show All" style="background-color:White;border-color:Wheat;border-style:solid;border-width:1px;color:black;font-weight:bold;font-family:Calibri"
        AutoPostBack="true" oncheckedchanged="chkshowall_CheckedChanged" />&nbsp;
 <a id="addnew" style="background-color:White;border-color:Wheat;border-style:solid;border-width:1px;color:black;font-weight:bold;font-family:Calibri;margin-top:10px;margin-bottom:20px" href="javascript:Divaddnewlink()">Add New</a>
    </td> </tr> </table> 
     
   
    <asp:GridView ID="GridViewExch" 
                   runat="server" 
                   AutoGenerateColumns="False" 
                 
                   DataSourceID="ObjectDataSource1" 
                   DataKeyNames="Curr_code,bcurr" 
                 
                   EmptyDataText="No matching found!" >
     <HeaderStyle CssClass="HeaderStyle-css" />
     <AlternatingRowStyle CssClass="AlternatingRowStyle-css" />
     <RowStyle CssClass="RowStyle-css" />
     <EditRowStyle CssClass="RowStyle-css" />
     <PagerStyle CssClass="PagerStyle-css"  />
     <FooterStyle CssClass="FooterStyle-css" />
        <Columns>
            <asp:TemplateField HeaderText="Currency Code" SortExpression="Currency_Type">
                <ItemTemplate>
                <asp:Label ID="lblcurcode"  runat="server"  Text='<%#Eval("Currency_Type")%>' ></asp:Label>
                   
                </ItemTemplate>
                
                <HeaderStyle ForeColor="White" />
                <ControlStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Exch Rate"  SortExpression="exch_rate" >
                 <ItemTemplate>
                    <asp:TextBox ID="txtrate"  runat="server" Text='<%#Bind("exch_rate")%>'></asp:TextBox>
                </ItemTemplate>
                
                <ControlStyle Width="150px" />
                <HeaderStyle ForeColor="White" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Base Currency" SortExpression="base_curr" >
             
                <ItemTemplate>
                    <asp:Label ID="lblbasecr" runat="server" Text='<%# Bind("base_curr") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="HeaderStyle-css" ForeColor="White" />
                  <ControlStyle Width="100px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Date" SortExpression="date" >
              
                <ItemTemplate>
                    <asp:Label ID="lbldate" runat="server" Text='<%# Bind("date") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle ForeColor="White" />
                  <ControlStyle Width="150px" />
               
            </asp:TemplateField>
                      
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnupd" Text="Update" OnClientClick="return confirm('Are you sure to update ?')" runat="server" onclick="btnupd_Click" />
  </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:ObjectDataSource ID="ObjectDataSource1" 
                           runat="server" 
                           SelectMethod="OCAP_Exch_Rate_fill" 
                           TypeName="SMS.Business.Infrastructure.BLL_Infra_ExchangeRate"  
                          onselecting="ObjectDataSource1_Selecting">
        <UpdateParameters>
            <asp:ControlParameter ControlID="GridViewExch" Name="id" PropertyName="id"/>
               
                <asp:Parameter Name="exch_rate" />
                <asp:Parameter Name="base_curr"/>
                <asp:Parameter Name="date" />
        </UpdateParameters>
        <DeleteParameters>
            <asp:ControlParameter ControlID="GridViewExch" Name="id" PropertyName="id"/>
        </DeleteParameters>
     </asp:ObjectDataSource>
     
   
    
    
    
     </div>
    <asp:UpdatePanel ID="upd1" runat="server">
      <ContentTemplate>
      
     
      <div id="divadd" style="display:none;background-color:#E0E0E0;border-color:Black;border-style:solid;border-width:1px;height:170px;width:300px;position:absolute;left:69%;top:7%;z-index:2;color:black" >
          <asp:Label ID="Label1" runat="server"  ForeColor="Black" Font-Bold="true" Text="Add New"></asp:Label>
      <br/>
      
      <table style="border-style:solid;border-color:Silver;border-width:1px">
      <tr >
      <td style="font-size:11px;font-family:Calibri;text-align:left;border-style:solid;border-color:Silver;border-width:1px;font-weight:bold"> Currency Code :</td>
      <td style="border-style:solid;border-color:Silver;border-width:1px"> 
          <asp:DropDownList ID="DropDownListcurrency" DataSourceID="ObjectDataSource2" Width="118px" CssClass="dropdown-css"  DataTextField="Currency_Type" DataValueField="Currency_Code"  runat="server">
          </asp:DropDownList>
          
       </td>
      </tr>
      <tr >
      <td style="font-size:11px;font-family:Calibri;text-align:left;border-style:solid;border-color:Silver;border-width:1px;font-weight:bold"> Exch. Rate: </td>
      <td style="border-style:solid;border-color:Silver;border-width:1px"> 
          <asp:TextBox ID="txtexchrate"  CssClass="textbox-css" runat="server"></asp:TextBox> </td>
      </tr>
       <tr >
      <td style="font-size:11px;font-family:Calibri;text-align:left;border-style:solid;border-color:Silver;border-width:1px;font-weight:bold"> Base Currency: </td>
      <td style="border-style:solid;border-color:Silver;border-width:1px"> 
           <asp:DropDownList ID="DropDownListbase" Width="118px" CssClass="dropdown-css"  DataSourceID="ObjectDataSource3" DataTextField="Currency_Type" DataValueField="Currency_Code"   runat="server">
                    </asp:DropDownList>
       </td>
      </tr>
      
       <tr >
      <td style="font-size:11px;font-family:Calibri;text-align:left;border-style:solid;border-color:Silver;border-width:1px;font-weight:bold"> Date: </td>
      <td style="border-style:solid;border-color:Silver;border-width:1px"> 
          <asp:TextBox ID="txtdate"  CssClass="textbox-css" runat="server"></asp:TextBox><RJS:PopCalendar ID="cal2" runat="server" Control="txtdate"/> </td>
      </tr>
      
       <tr >
      <td colspan="2" style="font-size:11px;font-family:Calibri;text-align:center;border-style:solid;border-color:Silver;border-width:1px"> 
      <asp:Button ID="Button1" CssClass="button-css" runat="server" OnClientClick="return chechlink()" Text="Save" OnClick="btnsave_Click" />
          <asp:Button ID="btncancel" CssClass="button-css" runat="server" Text="Cancel" />
       </td>
      </tr>
      </table>
    
      </div>
        </ContentTemplate> 
      </asp:UpdatePanel>
 <asp:ObjectDataSource ID="ObjectDataSource3" 
                          runat="server"
                          TypeName="SMS.Business.Infrastructure.BLL_Infra_ExchangeRate" 
                          SelectMethod="OCAP_Exch_Rate_fill_dropdown">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" 
                          runat="server"
                          TypeName="SMS.Business.Infrastructure.BLL_Infra_ExchangeRate" 
                          SelectMethod="OCAP_Exch_Rate_fill_dropdown">
    </asp:ObjectDataSource>

</div>
</asp:Content>

