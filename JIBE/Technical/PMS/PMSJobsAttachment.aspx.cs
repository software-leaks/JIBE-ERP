using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using SMS.Business.PMS;
using System.Text;
using System.Data;
using AjaxControlToolkit4;
using System.IO;

public partial class Technical_PMS_PMSJobsAttachment : System.Web.UI.Page
{
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    protected void Page_Load(object sender, EventArgs e)
    {
        //DataTable dt = new DataTable();
        //dt = objUploadFilesize.Get_Module_FileUpload("PMS_");
        //AjaxFileUpload1.MaximumSizeOfFile = Int32.Parse(dt.Rows[0]["Size_KB"].ToString());
        //if (!IsPostBack)
        //{

        //}

          

    }


    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {

            BLL_PMS_Library_Jobs objjobs = new BLL_PMS_Library_Jobs();

            Byte[] fileBytes = file.GetContents();
            string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\PmsJobs");
            Guid GUID = Guid.NewGuid();
            string AttachPath = "PMS_" + GUID.ToString() + Path.GetExtension(file.FileName);




            objjobs.LibrarySaveJobInstructionAttachment(Convert.ToInt32(Request.QueryString["VesselCode"].ToString()), Convert.ToInt32(Request.QueryString["JobId"].ToString()),Path.GetFileName(file.FileName), AttachPath
                , Convert.ToInt32(Session["userid"].ToString()), null);

            string FullFilename = Path.Combine(sPath, AttachPath);
            FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();

        }
        catch (Exception ex)
        {

        }

    }




}