using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class downloadfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["filename"]))
        {
            string sFileName;
            sFileName = Request.QueryString["filename"];
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + sFileName);
            string filePath = "../"+ConfigurationManager.AppSettings["TRV_UPLOAD_PATH"].ToString() + sFileName;
            try
            {
                Response.TransmitFile(filePath);
            }
            catch (Exception)
            {

                Response.Redirect("~/FileNotFound.aspx");
            }
           
            Response.End();
        }
    }
}

//Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
//       Dim objGr As New Griffin.GMTFiles
//       Try
//           If Trim(Request.QueryString("fname")) <> "" Then
//               Dim sFileName As String, sFiles() As String
//               Response.ContentType = "application/octet-stream"
//               sFileName = objGr.getFileName(Trim(Request.QueryString("fname")))
//               sFiles = Split(sFileName, "//") 'index zero filename as it was uploaded, index 1 has physical filename on the disk which need to be downloaded
//               Response.AppendHeader("Content-Disposition", "attachment; filename=" & sFiles(0))
//               If Request.QueryString("trType") = "AP" Then
//                   Response.TransmitFile(Server.MapPath("~/UploadedFiles/AP/" & sFiles(1)))
//               Else
//                   Response.TransmitFile(Server.MapPath("~/UploadedFiles/AR/" & sFiles(1)))
//               End If
//               Response.End()
//           End If
//       Catch ex As Exception

//       Finally
//           objGr = Nothing
//       End Try
//   End Sub
