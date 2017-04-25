using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.PURC;
using SMS.Business.Crew;
using SMS.Business.PMS;
using System.Text;
using SMS.Properties;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using AjaxControlToolkit4;
using System.Configuration;
using System.Text.RegularExpressions;
  
public partial class Technical_PMS_PMS_Manage_System_SubSystem : System.Web.UI.Page
{
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");
        if (!IsPostBack)
        {
            ucAsyncPager1.BindMethodName = "BindNextPageJobs";
            ucAsyncPager2.BindMethodName = "BindNextPageItems";
          //  ucAsyncLocPager.BindMethodName = "onGetLocation";
            // UserAccessValidation();
            BLL_Infra_VesselLib objBLLVessel = new BLL_Infra_VesselLib();
            DataTable dtvsl = objBLLVessel.Get_VesselList(0, 0, UDFLib.ConvertToInteger(Session["USERCOMPANYID"]), "", UDFLib.ConvertToInteger(Session["USERCOMPANYID"]));
            DDlVessel_List.DataSource = dtvsl;
            DDlVessel_List.DataBind();
            DDlVessel_List.Items.Insert(0, new ListItem("--SELECT VESSEL--", "0"));

           // BindLocationGrid();
           hdnUserID.Value= Session["userid"].ToString();
           hdnCompanyID.Value = Session["USERCOMPANYID"].ToString();
           hdnAppName.Value = System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString();
           BindDeptOptList();
           Session["Image"] = "";
           Session["DetailsImage"] = "";
           Session["ItemOperationMode"] = "";
           Session["AppAttach_" + hdnItemID.Value] = "";
        }

       
   
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void BindDeptOptList()
    {
        try
        {
            using (BLL_PURC_Purchase objBLLPUR = new BLL_PURC_Purchase())
            {
                DataTable DeptDt = objBLLPUR.GetDeptType();
                rblDeptType.DataSource = DeptDt;
                rblDeptType.DataTextField = "Description";
                rblDeptType.DataValueField = "Short_Code";
                rblDeptType.DataBind();
                rblDeptType.SelectedValue = "SP";
                rblDeptType.Items.Remove("ALL");
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void BindLocationGrid()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("LocationID");
        dt.Columns.Add("LocationShortCode");
        dt.Columns.Add("LocationName");
        dt.Columns.Add("System_Description");
        dt.Columns.Add("Subsystem_Description");
        dt.Columns.Add("LocAssignFlag");
        dt.Columns.Add("Category_Code");
        dt.Rows.Add();
       // gvLocation.DataSource = dt;
       // gvLocation.DataBind();
    }
     
    protected void imgDeleteAssignLoc_click(object sender, ImageClickEventArgs e)
    {
        //string AssignLocation = String.Format("onDeleteAssignLocation();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "DeleteLocation", AssignLocation, true);

        if (hdnCatalogOperationMode.Value == "ADD")
        {
            string AssginLocmodal = String.Format("onAddSystem();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalUser", AssginLocmodal, true);

        }
        else if (hdnCatalogOperationMode.Value == "EDIT")
        {
            string AssginLocmodal = String.Format("onEditSystem();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalUser", AssginLocmodal, true);
        }
    }
    //protected void btnSaveItem_Click(object sender, EventArgs e)
    //{
        
    //    DataTable dt = new DataTable();
    //    dt = objUploadFilesize.Get_Module_FileUpload("PURC_");
    //    try
    //    {
    //        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();


    //        string image_url = "";
    //        string product_dtl_url = "";
    //        if (dt.Rows.Count > 0)
    //        {
    //            string datasize = dt.Rows[0]["Size_KB"].ToString();
    //            if (ImageUploader.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
    //            {
    //                string strImagePath = ImageUploader.PostedFile != null ? ImageUploader.PostedFile.FileName : "";

    //                if (DetailsImageUploader.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
    //                {
    //                    string strProductDtlPath = DetailsImageUploader.PostedFile != null ? DetailsImageUploader.PostedFile.FileName : "";

    //                    if ((String)hdnItemOperationMode.Value == "EDIT")
    //                    {

    //                        if (strImagePath != "")
    //                        {
    //                            image_url = "itm_" + System.Guid.NewGuid() + Path.GetExtension(strImagePath);
    //                            ImageUploader.PostedFile.SaveAs(Server.MapPath("~/Uploads/PURC_Items/" + image_url));
    //                        }
    //                        else
    //                        {
    //                            image_url = hdnImageURL.Value;
    //                        }

    //                        if (strProductDtlPath != "")
    //                        {
    //                            product_dtl_url = "itm_" + System.Guid.NewGuid() + Path.GetExtension(strProductDtlPath);
    //                            DetailsImageUploader.PostedFile.SaveAs(Server.MapPath("~/Uploads/PURC_Items/" + product_dtl_url));

    //                        }
    //                        else
    //                        {
    //                            product_dtl_url = hdnProductURL.Value;
    //                        }

    //                        string AssginLocmodal = String.Format("onEditSpare("+ hdnItemID.Value +");");
    //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowSpare", AssginLocmodal, true);
    //                    }

    //                    if ((String)hdnItemOperationMode.Value == "ADD")
    //                    {
    //                        if (strImagePath != "")
    //                            image_url = "itm_" + System.Guid.NewGuid() + Path.GetExtension(strImagePath);

    //                        if (strProductDtlPath != "")
    //                            product_dtl_url = "itm_" + System.Guid.NewGuid() + Path.GetExtension(strProductDtlPath);
                         
    //                        if (ImageUploader.PostedFile.FileName != "")
    //                        {
    //                            ImageUploader.PostedFile.SaveAs(Server.MapPath("~/Uploads/PURC_Items/" + image_url));
    //                        }
    //                        if (DetailsImageUploader.PostedFile.FileName != "")
    //                        {
    //                            DetailsImageUploader.PostedFile.SaveAs(Server.MapPath("~/Uploads/PURC_Items/" + product_dtl_url));
    //                        }
                            
    //                    }

    //                    hdnImageURL.Value = image_url;
    //                    hdnProductURL.Value = product_dtl_url;

                     
    //                    string SaveSpare = String.Format("onSaveSpare();");
    //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SaveSpare", SaveSpare, true);

    //                }

    //                else
    //                {
    //                    lblItemError.Text = " KB File size exceeds maximum limit";
    //                }
    //            }
    //            else
    //            {
    //                lblItemError.Text = " KB File size exceeds maximum limit";
    //            }
           
    //        }
    //        else
    //        {

    //            string js2 = "alert('Upload size not set!');";
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
    //        }

           
    //    }
    //    catch (Exception ex)
    //    {
    //        lblItemErrorMsg.Text = ex.ToString();

    //    }

    
    //}
    //protected void ImageUploader_UploadedComplete(object sender, AjaxControlToolkit4.AsyncFileUploadEventArgs e)
    //{
  
    //    Session["Image"] = ImageUploader.PostedFile;
    //    Session["ItemOperationMode"] = hdnItemOperationMode;

    //}
    //protected void DetailsImageUploader_UploadedComplete(object sender, AjaxControlToolkit4.AsyncFileUploadEventArgs e)
    //{

    //    Session["DetailsImage"] = DetailsImageUploader.PostedFile;
    //    Session["ItemOperationMode"] = hdnItemOperationMode;


    //}
    protected void ImageUploader_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {

            Session["Image"] = file.GetContents();
            Session["ItemOperationMode"] = hdnItemOperationMode;
        }
        catch (Exception ex)
        {
        }
    }
    protected void DetailsImageUploader_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {
            Session["DetailsImage"] = file.GetContents();
            Session["ItemOperationMode"] = hdnItemOperationMode;
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnCatalogueSave_Click(object sender, EventArgs e)
    {
    }

    
[System.Web.Services.WebMethod]
public static string ImageUpload()
{
  Technical_PMS_PMS_Manage_System_SubSystem obj=new Technical_PMS_PMS_Manage_System_SubSystem();
    

  return obj.ImgUpload();
}


[System.Web.Services.WebMethod]
public static string GetMovingData(string table)
{
    Technical_PMS_PMS_Manage_System_SubSystem obj = new Technical_PMS_PMS_Manage_System_SubSystem();
     return obj.HtmlToDataset(table);
}

public string HtmlToDataset(string table)
{
    DataSet ds = ConvertHTMLTablesToDataSet(table);
 
    Session["dtMoveJob"] = ds.Tables[0];

    
    if (ds.Tables[0].Rows.Count > 0)
    {
      //  string msg3 = string.Format("document.getElementById('iFrmCopyJobs').src ='../PMS/PMSMoveJobs.aspx?VesselCode=" + VesselID + "&SystemCode=" + SystemCode + "&SystemID=" + SystemID + "&SubSystemID=" + SubSystemID + "&SubSystemCode=" + SubSystemCode + "&DeptCode=" + DeptCode + "';showModal('dvCopyJobsPopUp');");
      //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg3", msg3, true);
        return "true";
    }
    else
    {
       // string jsAlert = "alert('Please select job/s to move.');";
       // ScriptManager.RegisterStartupScript(this, this.GetType(), "jsAlert", jsAlert, true);
        return "false";
    }

}
protected void BtnTemp_Click(object sender, EventArgs e)
{
    hdnImageURL.Value = Session["AppAttach_" + hdnItemID.Value].ToString().Split(',')[0] ;
    if (hdnImageURL.Value == "")
    {
        string js2 = "alert('"+Session["AppAttach_" + hdnItemID.Value].ToString().Split(',')[1]+"');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
    }
    Session["AppAttach_" + hdnItemID.Value] = ",";

    string jsAttachImg = "onAttachSpareImage();";
    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsAttachImg", jsAttachImg, true);

}
    protected void BtnTempDetails_Click(object sender, EventArgs e)
{
    hdnProductURL.Value = Session["AppAttach_" + hdnItemID.Value].ToString().Split(',')[0];
    if (hdnProductURL.Value == "")
    {
        string js2 = "alert('" + Session["AppAttach_" + hdnItemID.Value].ToString().Split(',')[1] + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert10", js2, true);
    }
    Session["AppAttach_" + hdnItemID.Value] = "";
    string jsAttachPrdImg = "onAttachSpareImage();";
    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsAttachPrdImg", jsAttachPrdImg, true);
}
    protected string GetSessionValue()
    {
         return Session["AppAttach_" + hdnItemID.Value].ToString();
       
    }
 public string ImgUpload()
    {
        string image_url = "";
        string product_dtl_url = "";
            DataTable dt = new DataTable();
        dt = objUploadFilesize.Get_Module_FileUpload("PURC_");

        //Session["Image"] = ImageUploader.PostedFile;
        //Session["DetailsImage"] = DetailsImageUploader.PostedFile;
        //Session["ItemOperationMode"] = hdnItemOperationMode;
        
        
        try
        {
            if (Session["Image"] != "" && Session["DetailsImage"] != "" && Session["ItemOperationMode"] != "")
            {
                BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
                HiddenField itemOperationMode = (HiddenField)Session["ItemOperationMode"];
                HttpPostedFile ImgPostedFile = (HttpPostedFile)Session["Image"];
                HttpPostedFile DetailsImgPostedFile = (HttpPostedFile)Session["DetailsImage"];

                if (dt.Rows.Count > 0)
                {
                    string datasize = dt.Rows[0]["Size_KB"].ToString();
                    if (ImgPostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                    {
                        string strImagePath = ImgPostedFile != null ? ImgPostedFile.FileName : "";

                        if (DetailsImgPostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                        {
                            string strProductDtlPath = DetailsImgPostedFile != null ? DetailsImgPostedFile.FileName : "";

                            if ((String)itemOperationMode.Value == "EDIT")
                            {

                                if (strImagePath != "")
                                {
                                    image_url = "itm_" + System.Guid.NewGuid() + Path.GetExtension(strImagePath);
                                    ImgPostedFile.SaveAs(Server.MapPath("~/Uploads/PURC_Items/" + image_url));
                                }
                                else
                                {
                                    image_url = hdnImageURL.Value;
                                }

                                if (strProductDtlPath != "")
                                {
                                    product_dtl_url = "itm_" + System.Guid.NewGuid() + Path.GetExtension(strProductDtlPath);
                                    DetailsImgPostedFile.SaveAs(Server.MapPath("~/Uploads/PURC_Items/" + product_dtl_url));

                                }
                                else
                                {
                                    product_dtl_url = hdnProductURL.Value;
                                }

                                //  string AssginLocmodal = String.Format("onEditSpare(" + hdnItemID.Value + ");");
                                //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowSpare", AssginLocmodal, true);
                            }

                            if ((String)itemOperationMode.Value == "ADD")
                            {
                                if (strImagePath != "")
                                    image_url = "itm_" + System.Guid.NewGuid() + Path.GetExtension(strImagePath);

                                if (strProductDtlPath != "")
                                    product_dtl_url = "itm_" + System.Guid.NewGuid() + Path.GetExtension(strProductDtlPath);

                                if (ImgPostedFile.FileName != "")
                                {
                                    ImgPostedFile.SaveAs(Server.MapPath("~/Uploads/PURC_Items/" + image_url));
                                }
                                if (DetailsImgPostedFile.FileName != "")
                                {
                                    DetailsImgPostedFile.SaveAs(Server.MapPath("~/Uploads/PURC_Items/" + product_dtl_url));
                                }

                            }

                            //  hdnImageURL.Value = image_url;
                            // hdnProductURL.Value = product_dtl_url;


                            //   string SaveSpare = String.Format("onSaveSpare();");
                            //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SaveSpare", SaveSpare, true);



                        }

                        else
                        {
                            lblItemError.Text = " KB File size exceeds maximum limit";
                        }
                    }
                    else
                    {
                        lblItemError.Text = " KB File size exceeds maximum limit";
                    }

                }
                else
                {

                    string js2 = "alert('Upload size not set!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                }

            }
        }
        catch (Exception ex)
        {
            lblItemErrorMsg.Text = ex.ToString();
        }
        return image_url + "," + product_dtl_url;
    }


private DataSet ConvertHTMLTablesToDataSet(string HTML)
    {
        // Declarations 
        DataSet ds = new DataSet();
        DataTable dt = null;
        DataRow dr = null;
       // DataColumn dc = null;
        string TableExpression = "<table[^>]*>(.*?)</table>";
        string HeaderExpression = "<th[^>]*>(.*?)</th>";
        string RowExpression = "<tr[^>]*>(.*?)</tr>";
        string ColumnExpression = "<td[^>]*>(.*?)</td>";
        bool HeadersExist = false;
        int iCurrentColumn = 0;
        int iCurrentRow = 0;

        // Get a match for all the tables in the HTML 
        MatchCollection Tables = Regex.Matches(HTML, TableExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

        // Loop through each table element 
        foreach (Match Table in Tables)
        {
            // Reset the current row counter and the header flag 
            iCurrentRow = 0;
            HeadersExist = false;

            // Add a new table to the DataSet 
            dt = new DataTable();

            //Create the relevant amount of columns for this table (use the headers if they exist, otherwise use default names) 
            if (Table.Value.Contains("<th"))
            {
                // Set the HeadersExist flag 
                HeadersExist = true;

                // Get a match for all the rows in the table 
                MatchCollection Headers = Regex.Matches(Table.Value, HeaderExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

                // Loop through each header element 
                foreach (Match Header in Headers)
                {
                    dt.Columns.Add(Header.Groups[1].ToString());
                }
            }
            else
            {
                for (int iColumns = 1; iColumns <= Regex.Matches(Regex.Matches(Regex.Matches(Table.Value, TableExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase)[0].ToString(), RowExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase)[0].ToString(), ColumnExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase).Count; iColumns++)
                {
                    dt.Columns.Add("Column " + iColumns);
                }
            }


            //Get a match for all the rows in the table 

            MatchCollection Rows = Regex.Matches(Table.Value, RowExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

            // Loop through each row element 
            foreach (Match Row in Rows)
            {
                // Only loop through the row if it isn't a header row 
                if (!(iCurrentRow == 0 && HeadersExist))
                {
                    // Create a new row and reset the current column counter 
                    dr = dt.NewRow();
                    iCurrentColumn = 0;

                    // Get a match for all the columns in the row 
                    MatchCollection Columns = Regex.Matches(Row.Value, ColumnExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    // Loop through each column element 
                    foreach (Match Column in Columns)
                    {

                   
                            // Add the value to the DataRow 
                            dr[iCurrentColumn] = Column.Groups[1].ToString();

                            // Increase the current column  
                            iCurrentColumn++;
                      
                    }

                    // Add the DataRow to the DataTable 
                    dt.Rows.Add(dr);

                }

                // Increase the current row counter 
                iCurrentRow++;
            }


            // Add the DataTable to the DataSet 
            if (dt.Columns.Count >= 1)
            {
                ds.Merge(dt);
            }

        }

        return ds;

    }



protected void btnAttImage_Click(object sender, EventArgs e)
{

}
protected void btnAttDetailsImg_Click(object sender, EventArgs e)
{

}

}