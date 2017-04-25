<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
  "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ import Namespace="System.Xml.XPath" %>
<%@ import Namespace="System.Xml.Xsl" %>
<%@ import Namespace="System.IO" %>
<script language="C#" runat="server" Debug="true">

/* 

(c) COPYRIGHT 2002 SiteCLX
ALL RIGHTS RESERVED

author:         P.A. Huisman
filename:       transform.aspx
discription:    C# Sample, transform xml data with xslt to xhtml treeview.

modification log:
 version        date            by      notes
 1.00           16/01/2003      PAH     first version.

*/

void Page_Load(Object sender, EventArgs e) {

   XPathDocument treeDoc = new XPathDocument(Server.MapPath("sampletree.xml"));
   XslTransform treeView = new XslTransform();
   treeView.Load(Server.MapPath("treeview.xslt"));
   
   StringWriter sw = new StringWriter();
   treeView.Transform(treeDoc, null, sw);
   string result = sw.ToString();
   result = result.Replace("xmlns:asp=\"remove\"", "");
  
   Control ctrl = Page.ParseControl(result);
   myTree.Controls.Add(ctrl);

}
</script> 

<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
  <head>
    <title>SiteCLX title</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta http-equiv="imagetoolbar" content="no" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="author" content="P.A. Huisman" />
    <meta name="owner" content="SiteCLX" /> 
    <meta name="copyright" content="copyright 2002-2003 SiteCLX. http://www.siteclx.com"  />
    <meta name="keyphrases" content="xhtml treeview sample" /> 
    <meta name="resource-type" content="document" /> 
    <meta name="rating" content="general" /> 
    <meta name="keywords" content="xhtml, treeview" />
    <meta name="description" content="xhtml treeview sample" />
    <meta name="abstract" content="xhtml treeview sample" />
    <meta name="robots" content="index,follow" />
    <meta name="revisit-after" content="30 days" />
    <style type="text/css" media="screen">
    /*<![CDATA[*/
    
    .folder,
    .folder:hover,
    .leaf:hover,
    .leaf {
      font-family : Tahoma;
      padding:0px;
      cursor: hand;
      font-size : 8pt;
      color: #000000;
      text-decoration:none;                             
    }
    .folder:hover,
    .leaf:hover {text-decoration:undeline; cursor: hand;}
    
    /*]]>*/
    </style>
    <script type="text/javascript" language="javascript">
    /*<![CDATA[*/
    function toggle(node) {
    
      var nextDIV = node.nextSibling;
    
      while(nextDIV.nodeName != "DIV") {
        nextDIV = nextDIV.nextSibling;
      }
    
      if (nextDIV.style.display == 'none') {
    
        if (node.childNodes.length > 0) {
    
          if (node.childNodes.item(0).nodeName == "IMG") {
            node.childNodes.item(0).src = getImgDirectory(node.childNodes.item(0).src) + "minus.gif";
          }
        }
        nextDIV.style.display = 'block';
      }
      else {
    
        if (node.childNodes.length > 0) {
          if (node.childNodes.item(0).nodeName == "IMG") {
              node.childNodes.item(0).src = getImgDirectory(node.childNodes.item(0).src) + "plus.gif";
          }
        }
        nextDIV.style.display = 'none';
      }
    }
    
    function getImgDirectory(source) {
        return source.substring(0, source.lastIndexOf('/') + 1);
    }
    
    function selectLeaf(title, url) {
      alert("You just clicked on title = " + title + ":: url = " + url);
    }
    /*]]>*/
    </script>
  </head>
  <body>
    <asp:label id ="myTree" runat="server" />
  </body>
</html>