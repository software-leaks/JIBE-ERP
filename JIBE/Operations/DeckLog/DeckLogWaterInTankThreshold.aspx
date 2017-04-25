<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeckLogWaterInTankThreshold.aspx.cs" Inherits="DeckLogWaterInTankThreshold" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Styles/smscss_blue.css" rel="stylesheet" type="text/css" />
    <link href="../../Purchase/styles/Purchase.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .CellClass1
        {
            background-color: Red;
            color: White;
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        .CellClass0
        {
            border: 1px solid #cccccc;
            border-right: 1px solid #cccccc;
        }
        
        .HeaderCellColor
        {
            background-color: #3399CC;
            color: White;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; width: 100%;
        height: 100%;">
        <table cellspacing="1" cellpadding="1" rules="all" style="border: 1px solid #cccccc"
            width="100%">
            <tr>
                <td rowspan="2" align="center" class="HeaderCellColor">
                    Threshold
                </td>
                <td align="center" class="HeaderCellColor">
                    Water in Tank Threshold
                </td>
            </tr>
            <tr align="center">
                <td class="HeaderCellColor">
                    Standard
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color: #BCF5A9">
                    MIN
                </td>
                <td align="center">
                    <asp:TextBox ID="txtSoundingMin" CssClass="txtInput" runat="server" Width="60px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" style="background-color: #F78181">
                    MAX
                </td>
                <td align="center">
                    <asp:TextBox ID="txtSoundingMax" CssClass="txtInput" runat="server" Width="60px"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 30px">
                <td align="right" colspan="2">
                  <asp:Button ID="btnSave" Text="Save" runat="server" Width="100px" OnClick="btnSave_Click" />

                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
