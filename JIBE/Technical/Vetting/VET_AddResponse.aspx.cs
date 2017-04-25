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

public partial class Technical_Vetting_VET_AddResponse : System.Web.UI.Page
{
    BLL_VET_Index objBLLIndx = new BLL_VET_Index();
    string Observation_ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack && !AjaxFileUpload1.IsInFileUploadPostBack)
            {
                if (Request.QueryString["Observation_ID"] != null)
                {
                    Observation_ID = Request.QueryString["Observation_ID"].ToString();
                    ViewState["Observation_ID"] = Observation_ID;

                    Session["Response_ID"] = "";
                    hdnResponseId.Value = Session["Response_ID"].ToString();

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
            if (Session["Response_ID"].ToString() != "")
            {                

                Byte[] fileBytes = file.GetContents();
                string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\VetRAtt");
                Guid GUID = Guid.NewGuid();
                string AttachPath = "VET_" + GUID.ToString() + Path.GetExtension(file.FileName);
                

                objBLLIndx.VET_Ins_ResponseAttachment(UDFLib.ConvertToInteger(Session["Response_ID"].ToString()), Path.GetFileName(file.FileName), AttachPath, Convert.ToInt32(Session["userid"].ToString()));
               
                string FullFilename = Path.Combine(sPath, AttachPath);
                FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Write(fileBytes, 0, fileBytes.Length);
                fileStream.Close();
                
               
            }
            else
            {
                string jsSqlError2 = "alert('Please save the Response and add attachment');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError44", jsSqlError2, true);
            }


        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtResponse = new DataTable();
            dtResponse = objBLLIndx.VET_Ins_Response(UDFLib.ConvertToInteger(ViewState["Observation_ID"].ToString()), txtResponse.Text.Trim(), Convert.ToInt32(GetSessionUserID()));
           if (dtResponse.Rows.Count > 0)
           {
               hdnResponseId.Value = dtResponse.Rows[0]["Response_ID"].ToString();
              Session["Response_ID"] = hdnResponseId.Value;
              
               string jsSqlError2 = "alert('Response saved successfully.');";
               ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);        

              
           }
           
          
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Method is used to get login user id
    /// </summary>
    /// <returns>retrun user id</returns>
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;

    }
}