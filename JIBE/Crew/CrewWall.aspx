<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewWall.aspx.cs" Inherits="Crew_CrewWall" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/swfobject/2.2/swfobject.js">
    </script>
    <script type="text/javascript">
        var vid = '<%=getVID()%>';

        var Var1 = '<%=ConfigurationManager.AppSettings["APP_URL"].ToString() %>'
        var flashvars = { feed: Var1+"crew/CrewList_RSSFeed.aspx?vid=" + vid, showEmbed: false, showItemEmbed: false, showDescription:true };
        var params = {
            allowFullScreen: "true",
            allowscriptaccess: "always"
        };
        swfobject.embedSWF("http://apps.cooliris.com/embed/cooliris.swf?t=1307582197", "wall", "100%", "700", "9.0.0", "", flashvars, params);
    </script>

    <script type="text/javascript">
          function onItemSelected(item) {
              if (item != null) {
                  var t = document.getElementById("titleField");
                  if(t)
                  t.innerHTML = 'Title';
              }
          }

          var cooliris = {
              onEmbedInitialized: function () {
                  cooliris.embed.setCallbacks({ select: onItemSelected });
              }
          };
      </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="wall" style="height: 100%">
            <!-- 3D Wall Goes Here -->
        </div>
    </div>
    </form>
</body>
</html>
