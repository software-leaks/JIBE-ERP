using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.ASL;
using System.IO;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Configuration;

public partial class ASL_ASL_Supplier_Document : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    UserAccess objUAType = new UserAccess();
    public string Type = null;
    public string OperationMode = "";
    public string CurrStatus = null;
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
           
            BindGrid();
        }
    }
    private string GetSessionSupplierType()
    {
        if (Session["Supplier_Type"] != null)
            return Session["Supplier_Type"].ToString();
        else
            return null;
    }
    protected void UserAccessTypeValidation()
    {
        int CurrentUserID = GetSessionUserID();
        //string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_TypeManagement objType = new BLL_TypeManagement();
        string Variable_Type = "Supplier_Type";
        string Approver_Type = null;
        objUAType = objType.Get_UserTypeAccess(CurrentUserID, Variable_Type, GetSessionSupplierType(), Approver_Type);
        if (objUAType.Delete == 1) uaDeleteFlage = true;

    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            pnlDocs.Visible = false;
            lblMsg.Text = "You don't have sufficient previlege to access the requested information.";
        }
        else
        {
            pnlDocs.Visible = true;
        }

        if (objUA.Add == 0)
        {

        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        //else
           // btnsave.Visible = false;

        if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void BindGrid()
    {
        string SupplierID = GetSuppID();
       // int? SuppID = UDFLib.ConvertIntegerToNull(Request.QueryString["Supp_ID"]);
        DataSet ds = BLL_ASL_Supplier.Get_Supplier_Attachment(UDFLib.ConvertStringToNull(SupplierID));
        gvDataAttachment.DataSource = ds.Tables[0];
        gvDataAttachment.DataBind();
        gvCompanyAttachment.DataSource = ds.Tables[1];
        gvCompanyAttachment.DataBind();
        gvAllAtttachment.DataSource = ds.Tables[2];
        gvAllAtttachment.DataBind();
    }
    public string GetSuppID()
    {
        try
        {
            if (Request.QueryString["Supp_ID"] != null)
            {
                return Request.QueryString["Supp_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    protected void btnDataDelete_Click(object s, CommandEventArgs e)
    {
       
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? FileID = UDFLib.ConvertIntegerToNull(arg[0]);
        string fileName = arg[1].ToString();
        DeleteFiles(FileID, fileName);

    }
    protected void btnCompanyDelete_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? FileID = UDFLib.ConvertIntegerToNull(arg[0]);
        string fileName = arg[1].ToString();
        DeleteFiles(FileID, fileName);
      

    }
    protected void btnOtherDelete_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? FileID = UDFLib.ConvertIntegerToNull(arg[0]);
        string fileName = arg[1].ToString();
        DeleteFiles(FileID, fileName);
       

    }
    protected void DeleteFiles(int? FileID,string FilePath)
    {
        try
        {

            int RetValue = BLL_ASL_Supplier.Delete_Uploaded_Files(FileID, UDFLib.ConvertIntegerToNull(Session["UserID"].ToString()));
            if (File.Exists(Server.MapPath(FilePath)))
                File.Delete(Server.MapPath(FilePath));
            BindGrid();
        }
        catch { }
        {
        }
    }
    /// <summary>
    /// Check filepath exist or not
    /// Hiding Delete button for OCA attachment.(JIT/11989)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCompanyAttachment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string SavePath = null;
                ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnCompanyDelete");
                ImageButton imgDownload = (ImageButton)e.Row.FindControl("imgDownload");
                if (DataBinder.Eval(e.Row.DataItem, "File_Status").ToString() == "FILEUPLOAD")
                {
                    string OCA_URL = null;
                    if (!Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["OCA_Folder_NAME"]))
                    {
                        OCA_URL = ConfigurationManager.AppSettings["OCA_Folder_NAME"];
                    }
                    string OCA_URL1 = OCA_URL + @"\Files_Uploaded";
                    btnDelete.Visible = false;

                    SavePath = (OCA_URL1);
                    string File_ID = DataBinder.Eval(e.Row.DataItem, "Id").ToString();
                    File_ID = File_ID.PadLeft(8, '0');
                    string F1 = Mid(File_ID, 0, 2);
                    string F2 = Mid(File_ID, 2, 2);
                    string F3 = Mid(File_ID, 4, 2);
                    string filePath = SavePath + @"\" + F1 + @"\" + F2 + @"\" + F3 + @"\" + DataBinder.Eval(e.Row.DataItem, "FilePath").ToString();
                    imgDownload.CommandArgument = filePath + "," + DataBinder.Eval(e.Row.DataItem, "File_Status").ToString() + "," + DataBinder.Eval(e.Row.DataItem, "FileFullName").ToString();

                    string destinationPath = Server.MapPath("../Uploads/ASL/") + F1 + @"\" + F2 + @"\" + F3 + @"\";
                    if (!Directory.Exists(destinationPath))
                    {
                        Directory.CreateDirectory(destinationPath);
                    }
                    if (File.Exists(filePath))
                    {
                        File.Copy(filePath, destinationPath + DataBinder.Eval(e.Row.DataItem, "FilePath").ToString(), true);
                    }
                }
                else
                {
                    btnDelete.Visible = true;
                    SavePath = ("../uploads/ASL/");
                    string filePath = SavePath + DataBinder.Eval(e.Row.DataItem, "FilePath").ToString() + "," + DataBinder.Eval(e.Row.DataItem, "File_Status").ToString() + "," + DataBinder.Eval(e.Row.DataItem, "FileFullName").ToString();
                    imgDownload.CommandArgument = filePath;
                }
            }

        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }
    /// <summary>
    /// Check filepath exist or not
    /// Hiding Delete button for OCA attachment.(JIT/11989)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDataAttachment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         try
        {
          
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string SavePath = null;
            ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDataDelete");
            ImageButton imgDownload = (ImageButton)e.Row.FindControl("imgDownload");
            if (DataBinder.Eval(e.Row.DataItem, "File_Status").ToString() == "FILEUPLOAD")
            {
                string OCA_URL = null;
                if (!Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["OCA_Folder_NAME"]))
                {
                    OCA_URL = ConfigurationManager.AppSettings["OCA_Folder_NAME"];
                }
                btnDelete.Visible = false;
                string OCA_URL1 = OCA_URL + @"\Files_Uploaded";
                SavePath = (OCA_URL1);
                string File_ID = DataBinder.Eval(e.Row.DataItem, "Id").ToString();
                File_ID = File_ID.PadLeft(8, '0');
                string F1 = Mid(File_ID, 0, 2);
                string F2 = Mid(File_ID, 2, 2);
                string F3 = Mid(File_ID, 4, 2);
                string filePath = SavePath + @"\" + F1 + @"\" + F2 + @"\" + F3 + @"\" + DataBinder.Eval(e.Row.DataItem, "FilePath").ToString();
                imgDownload.CommandArgument = filePath + "," + DataBinder.Eval(e.Row.DataItem, "File_Status").ToString() + "," + DataBinder.Eval(e.Row.DataItem, "FileFullName").ToString();
                string destinationPath=Server.MapPath("../Uploads/ASL/")+F1 + @"\" + F2 + @"\" + F3 + @"\";
                if(!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(destinationPath);
                }
                if (File.Exists(filePath))
                {
                File.Copy(filePath, destinationPath + DataBinder.Eval(e.Row.DataItem, "FilePath").ToString(), true);
                }
            }
            else
            {
                btnDelete.Visible = true;
                 SavePath = ("../uploads/ASL/");
                 string filePath = SavePath + DataBinder.Eval(e.Row.DataItem, "FilePath").ToString() + "," + DataBinder.Eval(e.Row.DataItem, "File_Status").ToString() + "," + DataBinder.Eval(e.Row.DataItem, "FileFullName").ToString();
                 imgDownload.CommandArgument = filePath;
            }
           

        }
             }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }
    /// <summary>
    /// Check filepath exist or not
    /// Hiding Delete button for OCA attachment.(JIT/11989)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAllAtttachment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         try
        {
          
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string SavePath = null;
            ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnOtherDelete");
            ImageButton imgDownload = (ImageButton)e.Row.FindControl("imgDownload");
            if (DataBinder.Eval(e.Row.DataItem, "File_Status").ToString() == "FILEUPLOAD")
            {
                string OCA_URL = null;
                if (!Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["OCA_Folder_NAME"]))
                {
                    OCA_URL = ConfigurationManager.AppSettings["OCA_Folder_NAME"];
                }
                string OCA_URL1 = OCA_URL + @"\Files_Uploaded";
                btnDelete.Visible = false;
                SavePath = (OCA_URL1);
                //SavePath = ("../Files_Uploaded");
                string File_ID = DataBinder.Eval(e.Row.DataItem, "Id").ToString();
                File_ID = File_ID.PadLeft(8, '0');
                string F1 = Mid(File_ID, 0, 2);
                string F2 = Mid(File_ID, 2, 2);
                string F3 = Mid(File_ID, 4, 2);
                string filePath = SavePath + @"\" + F1 + @"\" + F2 + @"\" + F3 + @"\" + DataBinder.Eval(e.Row.DataItem, "FilePath").ToString();

                imgDownload.CommandArgument = filePath + "," + DataBinder.Eval(e.Row.DataItem, "File_Status").ToString() + "," + DataBinder.Eval(e.Row.DataItem, "FileFullName").ToString();
                string destinationPath = Server.MapPath("../Uploads/ASL/") + F1 + @"\" + F2 + @"\" + F3 + @"\";
                if (!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(destinationPath);
                }
                if (File.Exists(filePath))
                {
                    File.Copy(filePath, destinationPath + DataBinder.Eval(e.Row.DataItem, "FilePath").ToString(), true);
                }
            }
            else
            {
                btnDelete.Visible = true;
                SavePath = ("../uploads/ASL/");
                string filePath = SavePath + DataBinder.Eval(e.Row.DataItem, "FilePath").ToString() + "," + DataBinder.Eval(e.Row.DataItem, "File_Status").ToString() + "," + DataBinder.Eval(e.Row.DataItem, "FileFullName").ToString();
                imgDownload.CommandArgument = filePath;
            }
        }
            }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
       
    }

    public static string Mid(string param, int startIndex, int length)
    {
        //start at the specified index in the string ang get N number of
        //characters depending on the lenght and assign it to a variable
        string result = param.Substring(startIndex, length);
        //return the result of the operation
        return result;
    }
    /// <summary>
    /// Checking path is correct or not.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImgDownload_Click(object sender, CommandEventArgs e)
    {
        try
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            //string FilePath = UDFLib.ConvertStringToNull(arg[0]);
            string FileStatus = UDFLib.ConvertStringToNull(arg[1]);
            string FilePath = UDFLib.ConvertStringToNull(arg[2]);
            string sPath = "../Uploads/ASL/";
            if (FileStatus == "FILEUPLOAD")
            {
               
                string crewDocPath = ((ImageButton)sender).CommandArgument.Replace(ConfigurationManager.AppSettings["OCA_Folder_NAME"] + @"\Files_Uploaded", "").Replace("\\", "").Replace(",FILEUPLOAD", "");
                string F1 = Mid(crewDocPath, 0, 2);
                string F2 = Mid(crewDocPath, 2, 2);
                string F3 = Mid(crewDocPath, 4, 2);
                //string filename = crewDocPath.Replace(F1, "").Replace(F2, "").Replace(F3, "");
                string filename = FilePath;
                //  string filename = ((ImageButton)sender).Text;
                string extension = Path.GetExtension(sPath + F1 + "//" + F2 + "//" + F3 + "//" + filename);

                //string js = "previewDocument('" + sPath + F1 + "/" + F2 + "/" + F3 + "/" + filename + "');";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "loadfile", js, true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + sPath + F1 + "/" + F2 + "/" + F3 + "/" + filename + "');", true);
            }
            else
            {
                FilePath = UDFLib.ConvertStringToNull(arg[2]);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + sPath  + FilePath + "');", true);
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    
}