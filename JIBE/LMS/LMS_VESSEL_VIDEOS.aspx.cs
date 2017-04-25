using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.LMS;
using AjaxControlToolkit4;
using System.IO;
using System.Data;
public partial class LMS_LMS_VESSEL_VIDEOS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string js = "load_FunctionalTree('1');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "load_FunctionalTree", js, true);
        hfUrl.Value = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port  + HttpContext.Current.Request.ApplicationPath + "/uploads/VesselVideos/";
        if(!this.IsPostBack)
        {
          
        }
    }
    protected void btnAddCatrgory_Click(object sender, EventArgs e)
    {
      
        
        string FileName = null;
        int Parent = Convert.ToInt32(hfNode.Value);
        int Type = 2;
        int Id=0;
        if (txtCategoryName.Text.Trim() == "")
        {
            string js1 = "alert('Name is mandatory.');showModal('dvCategory');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalJS", js1, true);
            return;
        }

            
        if (Session["Mode"].ToString() == "NEW")
        {
            Id = 0;
        }
        if (Session["Mode"].ToString() == "EDIT")
        {
            Id = Convert.ToInt32(hfNode.Value);
        }
        if (Session["Mode"] != null)
        {

            BLL_LMS_Training.INSUPD_VESSEL_VIDEOS(Id, Type, txtCategoryName.Text,null, FileName, Parent, Convert.ToInt32(Session["USERID"]));
            string js = "hideModal('dvCategory');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
            Session["FileName"] = null;
            Session["Mode"] = null;
        }

      
    }
  
    protected void btnAddItem_Click(object sender, EventArgs e)
    {
       
        if (Session["Mode"].ToString().ToUpper() == "NEW".ToUpper())
        {
            hfFileName.Value = null;
        }
        string FileName = null;
        string OriginalFileName = null;
        int Parent = Convert.ToInt32(hfNode.Value);
        int Type = 3;
        int Id = 0; 
        if (hfPar.Value != "#" )
        {
            if (Session["FileName"] == null)
            {
                FileName = hfFileName.Value;
            }
            else
            {
                FileName = Session["FileName"].ToString();
            }
          
            Type = 3;
        }
        if (txtItemName.Text.Trim() == "")
        {
            string js1 = "alert('Name is mandatory.');showModal('dvItem');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalJS", js1, true);
            return;
        }
        if (FileName.Trim() == "" )
        {
            string js1 = "alert('File is mandatory.');showModal('dvItem');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalJS", js1, true);
            return;
        }
        if (Session["OriginalFileName"] == null)
            OriginalFileName = "";
        else
            OriginalFileName = Session["OriginalFileName"].ToString();
        if (Session["Mode"].ToString() == "NEW")
        {
            Id = 0;
        }
        if (Session["Mode"].ToString() == "EDIT")
        {
            Id = Convert.ToInt32(hfNode.Value);
        }
        if (Session["Mode"] != null)
        {

            BLL_LMS_Training.INSUPD_VESSEL_VIDEOS(Id, Type, txtItemName.Text,OriginalFileName, FileName, Parent, Convert.ToInt32(Session["USERID"]));
            string js = "hideModal('dvItem');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
            Session["FileName"] = null;
            Session["Mode"] = null;
        }
    }

    protected void btnDeleteCatrgory_Click(object sender, EventArgs e)
    {

       int Id = Convert.ToInt32(hfNode.Value);
        BLL_LMS_Training.DEL_VESSEL_VIDEOS(Id,  Convert.ToInt32(Session["USERID"]));
        string js = "hideModal('dvCategory');";
         ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
    }
    protected void btnDeleteItem_Click(object sender, EventArgs e)
    {
        int Id = Convert.ToInt32(hfNode.Value);
        BLL_LMS_Training.DEL_VESSEL_VIDEOS(Id, Convert.ToInt32(Session["USERID"]));
        string js = "hideModal('dvItem');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
    }
   
    protected void btnAdd_Click(object sender, EventArgs e)
    {


        if (hfPar.Value == "")
        {
            string msg = String.Format("alert('Node is not selected!')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            return;
        }
            
        if (hfPar.Value == "1" || hfNode.Value == "1")
        {
            Session["Mode"] = "NEW";
            btnDeleteCatrgory.Visible = false;
            btnDeleteItem.Visible = false;
            if (hfPar.Value == "#")
            {
                txtCategoryName.Text = "";
                string js = "showModal('dvCategory');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalJS", js, true);


            }
            else
            {
                txtItemName.Text = "";
                hfFileName.Value = null;
                Session["FileName"] = null;
                string js = "showModal('dvItem');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalJS", js, true);


            }
        }
        else
        {
            string msg = String.Format("alert('Node is not selected!')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            return;
        }

      
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
       
       
        if (hfNode.Value == "1")
        {
            string   js1 = "alert('Select category or video to edit!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalJS", js1, true);
            return;
        }
        if (hfPar.Value == "")
            return;
        Session["Mode"] = "EDIT";
        Session["FileName"] = null;
        if (hfPar.Value == "1")
        {
            btnDeleteItem.Visible = false;
            btnDeleteCatrgory.Visible = true;
            txtCategoryName.Text = hfText.Value;
            string js = "showModal('dvCategory');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalJS", js, true);
        }
        else
        {
            btnDeleteItem.Visible = true;
            btnDeleteCatrgory.Visible = false;
            txtItemName.Text = hfText.Value;
            string js = "showModal('dvItem');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalJS", js, true);
        }
    }

  
    protected void fuVideo_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        // User can save file to File System, database or in session state
       
        if (file != null)
        {


            string path = Server.MapPath("~/Uploads/VesselVideos");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

                Byte[] fileBytes = file.GetContents();
                string FileName =  Path.GetFileName(file.FileName);

                Session["OriginalFileName"] = FileName; 
                Guid GUID = Guid.NewGuid();
                FileName = "VSLV_"+GUID.ToString() + Path.GetExtension(file.FileName); 

                Session["FileName"] = FileName;
                int SIZE_BYTES = fileBytes.Length;
                string FilPath = Path.Combine(Server.MapPath("~/Uploads/VesselVideos/"), FileName);
                if (File.Exists(FilPath))
                    File.Delete(FilPath);
            
                FileStream fileStream = new FileStream(FilPath, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Write(fileBytes, 0, fileBytes.Length);
                fileStream.Close();





 
        }
    }
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt = BLL_LMS_Training.GET_VESSEL_VIDEOS(null);
            dt.DefaultView.RowFilter = "FileName IS NOT NULL";
            DataView dv = dt.DefaultView;

            List<String> lFileList = new List<string>();

            foreach (DataRow item in dv.ToTable().Rows)
            {
                lFileList.Add(item["FileName"].ToString());
            }

            if (lFileList.Count > 0)
            {
                string lDownlodFileName = BLL_LMS_Training.RAR(Server.MapPath("~/Uploads/VesselVideos"), lFileList); 
                ResponseHelper.Redirect("~/Uploads/VesselVideos/" + lDownlodFileName, "blank", "");
            }
        
        }
        catch (Exception)
        {

            string js1 = "alert('Videos Not Found.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModalJS", js1, true);
            return;
        }
      
        //string[] HeaderCaptions = { "Module Name", "Original File Name", "New File Name" };
        //string[] DataColumnsName = { "text", "OriginalFileName", "FileName", };

        //GridViewExportUtil.ShowExcel(dv.ToTable(), HeaderCaptions, DataColumnsName, "Vessel_Videos_File_List", "Rename your local files with file names as given below", "");
    }
}