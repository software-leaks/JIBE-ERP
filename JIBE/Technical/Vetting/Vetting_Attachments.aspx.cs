using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Web.UI.HtmlControls;
using SMS.Business.PMS;
using SMS.Business.Crew;
using SMS.Business.VET;
using SMS.Properties;
using AjaxControlToolkit4;
using System.IO;

public partial class Technical_Vetting_Vetting_Attachments : System.Web.UI.Page
{
    BLL_VET_Index objBLLIndx = new BLL_VET_Index();   
    string Vetting_ID;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
    
        if (Request.QueryString["Vetting_ID"] != null)
        {
            Vetting_ID = Request.QueryString["Vetting_ID"].ToString();
        }
       
        if (!IsPostBack)
        {
            VET_Get_VettingAttachmentType();
        }       
         
        LoadAttachment();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
     
    }

    /// <summary>
    /// Method is used to bind vetting attachement type
    /// </summary>
    public void VET_Get_VettingAttachmentType()
    {
        try
        {
            DataTable dtAttachmentType = objBLLIndx.VET_Get_VettingAttachmentType();
            DDLAttachment.DataSource = dtAttachmentType;
            DDLAttachment.DataTextField = "Vetting_Attachmt_Type_Name";
            DDLAttachment.DataValueField = "Vetting_Attachmt_Type_ID";
            DDLAttachment.DataBind();
            DDLAttachment.Items.Insert(0, new ListItem("-Select-", "0"));
            
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    
    }
   
    /// <summary>
    /// Method is used to load attachment list of particular vetting
    /// </summary>
    public void LoadAttachment()
    {
        try
        {
            DataTable dt = objBLLIndx.VET_Get_VettingAttachment(Convert.ToInt32(Vetting_ID));
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
    

    protected void DDLAttachment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Session["DDLAttachment_Value"] = DDLAttachment.SelectedItem.Value;

            DataSet ds = new DataSet();
            if (Convert.ToInt32(Session["DDLAttachment_Value"].ToString()) != 4)
            {
                ds = objBLLIndx.VET_Get_VettingExistAttachment(Convert.ToInt32(Vetting_ID), Convert.ToInt32(Session["DDLAttachment_Value"].ToString()));

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "callJSFunction", "ConfirmMsg();", true);

                    }
                }
            }
        }
          
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
   
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {

            if (FileUpload1.HasFile)
            {
                var FileExtension = "." + Path.GetExtension(FileUpload1.PostedFile.FileName).Substring(1);
                string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Vetting\\VetAtt\\");
                Guid GUID = Guid.NewGuid();
                string AttachPath = "VET_" + GUID.ToString() + FileExtension;
                FileUpload1.SaveAs(sPath + AttachPath);

                if (Convert.ToInt32(Session["DDLAttachment_Value"].ToString()) != 4)
                {
                    string confirmValue = Request.Form["confirm_value"];
                    if (confirmValue == "Yes")
                    {
                        objBLLIndx.VET_Upd_VettingAttachments(Convert.ToInt32(Vetting_ID), Convert.ToInt32(Session["DDLAttachment_Value"].ToString()), Path.GetFileName(FileUpload1.PostedFile.FileName), AttachPath, Convert.ToInt32(Session["userid"].ToString()));
                        LoadAttachment();
                        DDLAttachment.SelectedValue = "0";
                    }
                    else if(confirmValue==null)
                    {
                        objBLLIndx.VET_Ins_VettingAttachments(Convert.ToInt32(Vetting_ID), Convert.ToInt32(Session["DDLAttachment_Value"].ToString()), Path.GetFileName(FileUpload1.PostedFile.FileName), AttachPath, Convert.ToInt32(Session["userid"].ToString()));
                        LoadAttachment();
                        DDLAttachment.SelectedValue = "0";
                    }
                    else if (confirmValue == "No")
                    {
                        string jsSqlError2 = "alert('Attachment for selected type already exists.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
                        DDLAttachment.SelectedValue = "0";
                    }

                }
                else                
                {
                    objBLLIndx.VET_Ins_VettingAttachments(Convert.ToInt32(Vetting_ID), Convert.ToInt32(Session["DDLAttachment_Value"].ToString()), Path.GetFileName(FileUpload1.PostedFile.FileName), AttachPath, Convert.ToInt32(Session["userid"].ToString()));
                    LoadAttachment();
                    DDLAttachment.SelectedValue = "0";
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        int Vetting_Attachmt_ID = Convert.ToInt32(e.CommandArgument);
        try
        {

          int res= objBLLIndx.VET_Del_VettingAttachment(Vetting_Attachmt_ID, Convert.ToInt32(Session["USERID"]));
          if (res < 0)
          {
              LoadAttachment();
          }


        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
       
            string js2 = "parent.UpdatePage(); ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePopup", js2, true);
       
    }
}