using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

public partial class CharterParty_CP_Preview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Fileid"] != null)
        {
            try
            {

                //string sPath = "../Uploads/Infobase/";
                //string crewDocPath = Request.QueryString["Fileid"];
                //// string crewDocPath = Session["crewDocPath"].ToString();
                //string F1 = Mid(crewDocPath, 0, 2);
                //string F2 = Mid(crewDocPath, 2, 2);
                //string F3 = Mid(crewDocPath, 4, 2);
                ////string filename = Session["filename"].ToString();
                //string filename = Request.QueryString["Filename"];
                //string extension = Path.GetExtension(sPath + F1 + "//" + F2 + "//" + F3 + "//" + filename);
                //string js = "previewDocument('" + sPath + F1 + "/" + F2 + "/" + F3 + "/" + crewDocPath + extension + "');";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "loadfile", js, true);

                string CP_Infobase_Loc = null;
                if (!Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["CP_Infobase_Loc"]))
                {
                    CP_Infobase_Loc = ConfigurationManager.AppSettings["CP_Infobase_Loc"];
                }
                //string OCA_URL1 = OCA_URL + @"\Files_Uploaded";
                string sPath = (CP_Infobase_Loc);
                //SavePath = ("../Files_Uploaded");
                string File_ID = Request.QueryString["Fileid"].ToString();
                File_ID = File_ID.PadLeft(8, '0');
                string F1 = Mid(File_ID, 0, 2);
                string F2 = Mid(File_ID, 2, 2);
                string F3 = Mid(File_ID, 4, 2);
                string filePath = sPath + @"\" + F1 + @"\" + F2 + @"\" + F3 + @"\" + Request.QueryString["Fileid"].ToString();

                System.Diagnostics.Process.Start(filePath);
            }
            catch { }
        }
    }


    public static string Mid(string param, int startIndex, int length)
    {
        string result = param.Substring(startIndex, length);
        return result;
    }
}