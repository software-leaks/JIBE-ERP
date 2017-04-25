<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewFiles.aspx.cs" Inherits="ViewFiles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> View Files</title>
    <style type="text/css">
         ul#navlist { margin-left: 0; padding-left: 0; white-space: nowrap; }
         #navlist li { display: inline; list-style-type: none; } 
         #navlist a { padding: 3px 10px; }
         #navlist a:link, #navlist a:visited { color: #fff; background-color: #036; text-decoration: none; } 
         #navlist a:hover { color: #fff; background-color: #369; text-decoration: none; }
         div.example { padding: 20px; margin: 15px 0px; background: #ffe; clear:left; border: 1px dashed #ccc; text-align: left }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="example">
        <asp:Repeater ID="rptFiles" runat="server" DataSourceID="SqlDataSource1">
            <HeaderTemplate>
             <div id="navcontainer">
                <ul id="navlist">
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:HyperLink id="hlEdit" runat="server"
                     NavigateUrl='<%# "FileHandler.ashx?FileName="+ DataBinder.Eval(Container.DataItem, "File_Name")%>'
                     Text='<%# DataBinder.Eval(Container.DataItem, "File_Name") %>'>
                    </asp:HyperLink>
                </li>
             </ItemTemplate>
            <FooterTemplate>
                </ul>
               </div>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Repeater id="myRepeaterImage" runat="server" DataSourceID="SqlDataSource2">
          <ItemTemplate>
            <a href='<%# "FileHandler.ashx?FileName="+ DataBinder.Eval(Container.DataItem, "File_Name")%>'>
              <img src='<%# "FileHandler.ashx?FileName="+ DataBinder.Eval(Container.DataItem, "File_Name")%>'
              border="0"
              alt="<%# DataBinder.Eval(Container.DataItem, "File_Name") %>"/></a>
          </ItemTemplate>
        </asp:Repeater>
                 <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:DbCon %>" 
                     SelectCommand="SELECT [FileID], [File_Name], [File_MIME_Type] FROM [tbl_File]">
                 </asp:SqlDataSource>
                 <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:DbCon %>" 
                     SelectCommand="SELECT [FileID], [File_Name], [File_MIME_Type] FROM [tbl_File] WHERE ([File_MIME_Type] = @File_MIME_Type)">
                     <SelectParameters>
                         <asp:Parameter DefaultValue="image/gif" Name="File_MIME_Type" 
                             Type="String" />
                     </SelectParameters>
                 </asp:SqlDataSource>                 
    </div>
    </form>
</body>
</html>
