using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.VET;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using SMS.Properties;
using AjaxControlToolkit4;

public partial class Technical_Vetting_VET_ResponseAttachment : System.Web.UI.Page
{
    BLL_VET_Index objBLLIndx = new BLL_VET_Index();
    string Response_ID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Response_ID"] != null)
                {
                    Response_ID = Request.QueryString["Response_ID"].ToString();
                    ViewState["Response_ID"] = Response_ID;
                    LoadAttachment(Response_ID);
                }


            }
        }     
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {           

                Byte[] fileBytes = file.GetContents();
                string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Vetting\\VetRAtt");
                Guid GUID = Guid.NewGuid();
                string AttachPath = "VET_" + GUID.ToString() + Path.GetExtension(file.FileName);



                objBLLIndx.VET_Ins_ResponseAttachment(UDFLib.ConvertToInteger(Response_ID), Path.GetFileName(file.FileName), AttachPath, Convert.ToInt32(Session["userid"].ToString()));
                
                string FullFilename = Path.Combine(sPath, AttachPath);
                FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Write(fileBytes, 0, fileBytes.Length);
                fileStream.Close();
                LoadAttachment(ViewState["Response_ID"].ToString());
                string jsSqlError2 = "alert('Attachment saved successfully.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);

             
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    /// <summary>
    /// Method is used to bind attachment according to the particular response.
    /// </summary>
    /// <param name="Response_ID">selected Response id</param>
    public void LoadAttachment(string Response_ID)
    {
        try
        {
            DataTable dt = objBLLIndx.VET_Get_ResponseAttachment(UDFLib.ConvertToInteger(Response_ID));
            DataView dvImage = dt.DefaultView;

           
                gvAttachment.DataSource = dt.DefaultView;
                gvAttachment.DataBind();
          
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void btnRetrive_click(object sender, EventArgs e)
    {
        LoadAttachment(ViewState["Response_ID"].ToString());

    }
}