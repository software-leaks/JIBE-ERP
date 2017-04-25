<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContextMenu.aspx.cs" Inherits="ContextMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Context Menu</title>
    <script type="text/javascript" src="JS/ContextMenu.js"></script>
    <script type="text/javascript">
        var oCustomContextMenu = null;
        var oBase = null;
        window.onload = function () {
            oBase = document.getElementById('div');

            var Arguments = {
                Base: oBase,
                Width: 200,
                FontColor: null,
                HoverFontColor: null,
                HoverBackgroundColor: null,
                HoverBorderColor: null,
                ClickEventListener: OnClick
            };

            oCustomContextMenu = new CustomContextMenu(Arguments);

            oCustomContextMenu.AddItem('Images/ei0019-48.gif', 'Add', false, 'Add');
            oCustomContextMenu.AddItem('Images/save.png', 'Save', true, 'Save');
            oCustomContextMenu.AddSeparatorItem();
            oCustomContextMenu.AddItem('Images/ei0020-48.gif', 'Update', false, 'Update');
            oCustomContextMenu.AddSeparatorItem();
            oCustomContextMenu.AddItem(null, 'Cancel', false, 'Cancel');
        }

        var OnClick = function (Sender, EventArgs) {
            switch (EventArgs.CommandName) {
                case 'Add':
                    alert('Text: ' + EventArgs.Text);
                    alert('IsDisabled: ' + EventArgs.IsDisabled);
                    alert('ImageUrl: ' + EventArgs.ImageUrl);
                    break;
                case 'Save':
                    alert('Text: ' + EventArgs.Text);
                    alert('IsDisabled: ' + EventArgs.IsDisabled);
                    alert('ImageUrl: ' + EventArgs.ImageUrl);
                    break;
                case 'Update':
                    alert('Text: ' + EventArgs.Text);
                    alert('IsDisabled: ' + EventArgs.IsDisabled);
                    alert('ImageUrl: ' + EventArgs.ImageUrl);
                    break;
                case 'Cancel':
                    alert('Text: ' + EventArgs.Text);
                    alert('IsDisabled: ' + EventArgs.IsDisabled);
                    alert('ImageUrl: ' + EventArgs.ImageUrl);
                    break;
            }

            oCustomContextMenu.Hide();
        }

        window.onunload = function () { oCustomContextMenu.Dispose(); }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="div" oncontextmenu="javascript:return oCustomContextMenu.Display(event);"
        style="width: 925px; height: 300px; background-color: silver;">
    </div>
    </form>
</body>
</html>
