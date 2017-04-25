using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using AjaxControlToolkit4;
using SMS.Business.PURC;
using System.Data;
using SMS.Business.Infrastructure;
public partial class Technical_PMS_FileUploader : System.Web.UI.Page
{
    BLL_PURC_Purchase objPurchase = new BLL_PURC_Purchase();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }


    protected void ImageUploader_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = objUploadFilesize.Get_Module_FileUpload("PURC_");


            //dtattachment = (DataTable)Session["Attachment"];

            Byte[] fileBytes = file.GetContents();

            string sPath = Server.MapPath(Request.QueryString["Path"]); 
            Guid GUID = Guid.NewGuid();

            if (dt.Rows.Count > 0)
            {
                string datasize = dt.Rows[0]["Size_KB"].ToString();
                if (fileBytes.Length < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                {



                    string Flag_Attach = "itm_" + GUID.ToString() + Path.GetExtension(file.FileName);


                    string FullFilename = Path.Combine(sPath, "itm_" + GUID.ToString() + Path.GetExtension(file.FileName));

                    FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Write(fileBytes, 0, fileBytes.Length);
                    fileStream.Close();


                    Session["AppAttach_" + Request.QueryString["ItemID"].ToString()] = Flag_Attach + "," + file.FileName;


                }
                else
                {
                    Session["AppAttach_" + Request.QueryString["ItemID"].ToString()] = ",KB File size exceeds maximum limit";
                }
            }
            else
            {
                Session["AppAttach_" + Request.QueryString["ItemID"].ToString()] = ",Upload Size Not Set";
            }

            //if (Request.QueryString["ImageType"].ToString() == "Image")
            //{
            //    objPurchase.PURC_Update_ItemImageURL(Flag_Attach, null, Request.QueryString["ItemID"].ToString());
            //}
            //else if (Request.QueryString["ImageType"].ToString() == "ProductDetailImage")
            //{
            //    objPurchase.PURC_Update_ItemImageURL(null, Flag_Attach, Request.QueryString["ItemID"].ToString());
            //}
         //   string js2 = "UpdatePage();";
           // ScriptManager.RegisterStartupScript(this, this.GetType(), "hidePopup", js2, true);

            //hdnFileInfo.Value = Flag_Attach + "," + file.FileName;
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "reply", "top.$get('hdnFileInfo').innerHTML= " + Flag_Attach + "," + file.FileName + ";", true);


        }
        catch (Exception ex)
        {

        }

    }
}