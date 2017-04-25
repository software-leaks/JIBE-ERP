<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogDetails.aspx.cs" Inherits="Web_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">

a {
	font-style: normal;
	font-size: 8pt;
	color: black;
	cursor:pointer;
	text-decoration:none;
	}
td {
	font-style: normal;
	font-weight: normal;
	font-size: 10pt;
	color: black;
	}

        .style3
        {
            width: 394px;
        }
        .style4
        {
            width: 394px;
            height: 28px;
        }
        </style>
</head>
<body>

<form id="FirstPage" runat="server">
    <a href="" target="_blank"
        style="display: none">Test</a>
    <span id="lblLoading0" style="position: absolute; top: 0; right: 0; visibility: hidden;
        color: red; font-size: 12px; font-weight: bold;">Loading...</span>
    <table width="100%" id="tabLogo0" border="0" 
        style="border-width: thin; border-style: none;">
        <tr>
            <td nowrap 
                style="vertical-align: bottom; background-color: #FFFFFF; " 
                class="style2">
                <img src="Images/Logo.gif" height="53" width="70" />
                <span style="vertical-align: bottom; font-size: medium; font-weight: bold; font-family: Tahoma;">
              QMS</span></td>
                </tr>
        </table>
    
    <div id="content0" style="overflow:auto;">
    
    
   <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 43px;">
     <tr>
      <td align="left" style="border:0;" class="style4">
                    &nbsp;&nbsp;
        &nbsp;&nbsp;<table border="0" cellpadding="0" cellspacing="0" 
              style="width: 146%; height: 17px; margin-left: 145px; font-size: medium; font-weight: bold;">
     <tr>
      <td align="left" style="border:0; font-size: small; font-weight: bold;" class="style3">
          &nbsp;File Summary</td> 
 </tr>
</table>
                &nbsp;</td> 
 </tr>
</table>
    
    
    </div>
    <div>
    
    <table id="tabLogo1" border="0" 
        
            
            style="border-width: thin; border-style: none; height: 46px; width: 72%; margin-left: 140px;">
        <tr style="background-color: #669999;font-size: small;" 
            align="justify">
            <td nowrap 
                style="vertical-align: bottom; background-color: #AFCAE4; color: #FFFFFF;" 
                class="style2">
   
   <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 43px;">
     <tr>
      <td align="left" style="border:0;" class="style3">
        <asp:Button ID="btnExecute" runat="server" Text="Email"  
             Height="22px" Width="105px" Font-Bold="True" Font-Italic="False" />&nbsp;&nbsp;
        <asp:Button ID="btnClear" runat="server" Text="  Print   " Height="22px" 
              Font-Bold="True" />&nbsp;&nbsp;
        <asp:Button ID="btnClear0" runat="server" Text="   Close   " Height="22px" 
              Font-Bold="True" />
     </td> 
 </tr>
</table>
                </td>
                </tr>
    </table>
    
    </div>
    
    
    <div id="Div1" style="overflow:auto;">
    <table id="Table1" border="0" 
        
            style="border-width: thin; border-style: none; height: 103px; width: 72%; margin-left: 140px;">
        <tr style="background-color: #669999; font-size: small;" 
            align="justify">
            <td nowrap style="vertical-align: bottom; background-color: #FFFFFF; color: #FFFFFF;" 
                class="style2">
               <asp:DataGrid ID="resultsDataGrid" runat="server" Width="100%"  
                AutoGenerateColumns="False"  BorderWidth="1px" 
                 CellPadding="3" 
                GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" AllowPaging="True" PageSize="20" Height="466px" > <FooterStyle BackColor="#CCCCCC" ForeColor="Black" /> 
                <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" 
                    HorizontalAlign="Left" /><PagerStyle VerticalAlign="Bottom" BackColor="#999999" ForeColor="Black"   HorizontalAlign="Center" Mode="NumericPages" />
                <AlternatingItemStyle BackColor="#DCDCDC" /> <ItemStyle BackColor="#EEEEEE"  
                    ForeColor="Black" HorizontalAlign="Left" />
                <Columns>
                    
                      <asp:TemplateColumn HeaderText="Manuals" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true" >
                        <ItemTemplate>
                            <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("Manual") %>' CssClass="" ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" ForeColor="White"></HeaderStyle>
                    </asp:TemplateColumn>
                    
                                       
                     <asp:TemplateColumn HeaderText="FileName" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true">                   
                        <ItemTemplate>
                            <asp:Label ID="lblSize" runat="server" Text='<%# Eval("FileName") %>'></asp:Label>
                        </ItemTemplate>   
                        <HeaderStyle Font-Bold="True" ForeColor="White"></HeaderStyle>
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Last Accesed" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true">
                        <ItemTemplate>
                            <asp:Label ID="lblwrite" runat="server" Text= '<%# Eval("Accesed") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" ForeColor="White"></HeaderStyle>
                    </asp:TemplateColumn>

                 </Columns>
            <HeaderStyle BackColor="#3F658B" Font-Bold="True" ForeColor="White" />
        </asp:DataGrid>
   
      </td>
    </tr>
     </td>
      </tr>
    </table>
    </div>
    <div>
    
    </div>
    </form>
    
    </body>
</html>
