<%@ Page Language="C#" %>
function GetBrowser()
{
	return 'IE6';
}

var SitePath = '<% =Session["sitepath"] %>'+'/';
var ajxLoadingText = "";

function KeepComma(str)
{
	return str.replace(/,/g,'¿');
}