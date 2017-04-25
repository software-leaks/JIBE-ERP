using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;

using System.Data;
using AjaxControlToolkit4;
using System.IO;
using SMS.Business.Inspection;
public partial class Technical_Worklist_InspAttachment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (AjaxFileUpload1.IsInFileUploadPostBack)
        {

        }
        else
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["InspectionDetailId"] != null)
                {
                    lblInspectionDetailId.Text =   Request.QueryString["InspectionDetailId"].ToString();

                    if (Request.QueryString["Vessel_Id"] != null)
                    {
                        ViewState["Vessel_Id"]= Request.QueryString["Vessel_Id"];
                    }
                    if (Request.QueryString["Vessel_Name"] != null)
                    {
                        lblVesselName.Text = Request.QueryString["Vessel_Name"];
                    }
                    if (Request.QueryString["Schedule_Date"] != null)
                    {
                        lblInspectionDate.Text = UDFLib.ConvertDateToNull(Request.QueryString["Schedule_Date"]).Value.ToString("dd/MMM/yyyy");
                    }


                    BindInspAttchment();
                }

            }
            lblErrorMsg.Text = "";
        }
    }
    public void BindInspAttchment()
    {


        BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
        DataTable dtAttachment = objInsp.INSP_Get_Attachments_DL(Request.QueryString["InspectionDetailId"].ToString(), Convert.ToInt32(Request.QueryString["Vessel_Id"].ToString()));

        gvInspectionAttachment.DataSource = dtAttachment;
        gvInspectionAttachment.DataBind(); 

    }


    public void imgbtnDelete_Click(object s, EventArgs e)
    {
        try
        {

            BLL_Tec_Inspection objAttch = new BLL_Tec_Inspection();

            string[] arg = (((ImageButton)s).CommandArgument.Split(new char[] { ',' }));
            int res = objAttch.INSP_Delete_Attachments_DL(int.Parse(arg[0]), int.Parse(arg[1]), UDFLib.ConvertToInteger(Session["USERID"]));

            BindInspAttchment();


        }
        catch
        { }
    }

    

    

    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {
            BLL_Tec_Inspection objTechService = new BLL_Tec_Inspection();

            Byte[] fileBytes = file.GetContents();
            string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Technical");
            Guid GUID = Guid.NewGuid();
            string Flag_Attach = GUID.ToString() + Path.GetExtension(file.FileName);


            

            string FullFilename = Path.Combine(sPath, GUID.ToString() + Path.GetExtension(file.FileName));
            int sts = objTechService.SaveAttachedFileInfo(Request.QueryString["VESSEL_ID"].ToString(), Request.QueryString["InspectionDetailId"].ToString(), Path.GetFileName(file.FileName), UDFLib.Remove_Special_Characters(Path.GetFileName(FullFilename)), Session["USERID"].ToString());
            FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();
             
           // ScriptManager.RegisterStartupScript(this, typeof(Page), "refresh", "fn_OnClose(a,b)", true);
         
        }
        catch (Exception ex)
        {

        }

    }

    protected void btnLoadFiles_Click(object sender, EventArgs e)
    {

        BindInspAttchment();
    }
}