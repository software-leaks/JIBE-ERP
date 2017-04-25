using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.DMS;
using System.IO;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;

public partial class Crew_Crew_UploadPhoto : System.Web.UI.Page
{
    BLL_DMS_Document objDMSBLL = new BLL_DMS_Document();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }


    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        DataTable dt = new DataTable();
        dt = objUploadFilesize.Get_Module_FileUpload("CWF_");
        //lblhdn.Text = dt.DefaultView[0]["Size_KB"].ToString();
        if (dt.Rows.Count > 0)
        {


            int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
            string ParentPage = UDFLib.ConvertStringToNull(Request.QueryString["ParentPage"]);
            int DocID = 0;
            //if (ContentLength< convtert(bll.methodname('').t.row[0]["filesixe"].tostring())) * 1024
            //int bitss = Int32.Parse(lblhdn.Text) * 1024;
            string DocName = "";
            string FileName = "";
            string FileExt = "";
            string datasize = dt.Rows[0]["Size_KB"].ToString();
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                {
                    try
                    {
                        Guid GUID = Guid.NewGuid();
                        FileName = FileUpload1.FileName;
                        FileExt = Path.GetExtension(FileName).ToLower();
                        DocName = FileName.Replace(FileExt, "");
                        FileName = "CIMG_" + GUID.ToString() + FileExt;
                        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Uploads/CrewImages/" + FileName));

                        DocID = objCrew.UPDATE_CrewPhotoURL(CrewID, FileName);
                        if (DocID > 0)
                        {
                            lblMessage.Text = "Photo Uploaded.";
                            string js = "window.parent.CrewPhotoCrop('" + ParentPage + "');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "cropPhoto", js, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "ERROR: " + ex.Message.ToString();
                    }
                }
                else
                {
                    //lblMessage.Text = lblhdn.Text + " KB File size exceeds maximum limit";
                    lblMessage.Text = datasize + " KB File size exceeds maximum limit";
                }

            }
        }
        else
        {

            string js2 = "alert('Upload size not set!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
            //string js2 = "Upload size not set";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg2", js2, true);

        }

    }
}


