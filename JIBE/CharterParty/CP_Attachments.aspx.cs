using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit4;
using SMS.Business.Infrastructure;
using SMS.Business.CP;
using SMS.Properties;
using System.Configuration;
using System.Diagnostics;
using SMS.Business.OCAAdmin;

public partial class CP_Attachments : System.Web.UI.Page
{
    protected DataTable dtGridItems;
    UserAccess objUA = new UserAccess();
    public int CPID = 0;
    public int PortId = 0;
    public string PortName = "";
    public string OType = "";
    string Charter_ID = "";
    public Boolean uaEditFlag = true;//Test default true
    public Boolean uaDeleteFlage = true;
    BLL_CP_CharterParty objCP = new BLL_CP_CharterParty();
    BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();
    BLL_Infobase objINFO = new BLL_Infobase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            BindAttachment();
            if (Session["CPID"] != null)
            {
                UserAccessValidation();
                if (Session["ddl"] == null || ddlDepartment.SelectedValue != "0")
                   Session["ddl"] = ddlDepartment.SelectedValue;
                LoadTree();
                if (Session["ddl"] != null )
                ddlDepartment.SelectedValue =  Session["ddl"].ToString();
                AjaxFileUpload2.Visible = true;
            }
            

        }

     
    }


    protected void BindAttachment()
    {

        int? ID = UDFLib.ConvertIntegerToNull(Session["CPID"]);
        DataTable dt1 = objCP.GET_Charter_Party_Details(UDFLib.ConvertIntegerToNull(Session["CPID"]));
        if (dt1.Rows[0]["Charter_Id"] != null)
            Charter_ID = dt1.Rows[0]["Charter_Id"].ToString();
        Session["Charter_ID"] = Charter_ID;
        DataTable dt = new DataTable();
        dt = objCP.GET_CharterFiles(Charter_ID);
        gvAttachment.DataSource = dt;
        gvAttachment.DataBind();
        //Repeater1.DataSource = dt;
        //Repeater1.DataBind();
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);
        //if (objUA.View == 0)
        //    Response.Redirect("~/default.aspx?msgid=1");

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    private void LoadTree()
    {
        try
        {
            string DeptCode = "CHG";
            DataTable dtDepartment = objCP.GetFolderId(DeptCode, GetSessionUserID());
            if (dtDepartment.Rows.Count > 0)
            {
                btnAdd.Visible = true;
                ddlDepartment.DataSource = dtDepartment;
                ddlDepartment.DataTextField = dtDepartment.Columns[1].ToString();
                ddlDepartment.DataValueField = dtDepartment.Columns[0].ToString();
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, "--Select--");
            }
            else
            {
                btnAdd.Visible = false;
            }
        }
        catch
        {
        }


    }
    protected void gvAttachment_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string SavePath = null;
            ImageButton imgAttachment = (ImageButton)e.Row.FindControl("imgAttachment");
            SavePath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["Info_Path"].ToString());
            string File_ID = DataBinder.Eval(e.Row.DataItem, "File_Id").ToString();
            File_ID = File_ID.PadLeft(8, '0');
            string F1 = Mid(File_ID, 0, 2);
            string F2 = Mid(File_ID, 2, 2);
            string F3 = Mid(File_ID, 4, 2);
            string filePath = SavePath  + F1 + @"\" + F2 + @"\" + F3 + @"\" + DataBinder.Eval(e.Row.DataItem, "File_Path").ToString();
            imgAttachment.CommandArgument = filePath;

           
              string Folder_name = gvAttachment.DataKeys[e.Row.RowIndex].Values[0].ToString();
              DataTable dt = objCP.Get_OwnerAccess(GetSessionUserID(), Folder_name).Tables[0];
              DataTable dt1 = objCP.Get_OwnerAccess(GetSessionUserID(), Folder_name).Tables[1];
            ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
            DataControlField dataControlField = gvAttachment.Columns.Cast<DataControlField>().SingleOrDefault(x => x.HeaderText == "Edit");
            if (dt.Rows.Count > 0)
            {
                //dataControlField.Visible = true;
                imgEdit.Visible = true;
            }
            else
            {
               // dataControlField.Visible = false;
                imgEdit.Visible = false;
            }
        }


    }

    protected void ImgAttachment_Click(object sender, CommandEventArgs e)
    {
        try
        {
            string smPath = System.Configuration.ConfigurationManager.AppSettings["OCA_Folder_NAME"].ToString();
           
            string sPath = "..";
            string crewDocPath = ((ImageButton)sender).CommandArgument;
            int x = crewDocPath.IndexOf(@"\uploads\Infobase\");
            string part = crewDocPath.Substring(0, x);
            crewDocPath = crewDocPath.Replace(part, "").Replace("\\","/");
            string filename = Path.GetFileName(crewDocPath);
            if (!File.Exists(Server.MapPath("~") + crewDocPath))
            {
                File.Copy(smPath +"/"+ crewDocPath.Replace("/uploads/Infobase/", ""), Server.MapPath("~") + crewDocPath);
                //UploadFile(Server.MapPath("~") + crewDocPath);
            }
           
                string js = "previewDocument('" + sPath + crewDocPath + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "loadfile", js, true);
            

        }
        catch { }
        {
        }
    }

    protected void ImgEdit_Click(object s, CommandEventArgs e)
    {
        AjaxFileUpload2.Visible = false;
        string[] arg = e.CommandArgument.ToString().Split(',');
        txtContent.Text = arg[1].ToString();
        txtTitle.Text = arg[0].ToString();
        Session["arg"] = arg[2].ToString();
        ddlDepartment.Visible = false;
        lblPrimary.Visible = false;
        Session["Operation"] = "1";
       // string folderid = ddlDepartment.SelectedValue;
        string showmodal = String.Format(" showModal('dvAddFolder', true, dvAddFolder_onClose);");
    
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", showmodal, true);
    }


    public static string Mid(string param, int startIndex, int length)
    {
        string result = param.Substring(startIndex, length);
        return result;
    }

    protected void ImgAttDelete_Click(object sender, CommandEventArgs e)
    {

        string[] cmdargs = e.CommandArgument.ToString().Split(',');

        string fileid = cmdargs[0].ToString();
        string fileName = cmdargs[1].ToString();
        string VesselName = UDFLib.ConvertStringToNull(Session["VesselName"]);
        //string filepath = "../uploads/CP/" + VesselName +"/"+ fileName;

        int retval = objCP.DEL_Attachments(UDFLib.ConvertIntegerToNull(Session["CPID"]), UDFLib.ConvertIntegerToNull(fileid), UDFLib.ConvertIntegerToNull(Session["userid"]));


        //if (File.Exists(Server.MapPath(filepath)))
        //    File.Delete(Server.MapPath(filepath));

        BindAttachment();



    }

    private void UploadFile(string filename)
    {
        try
        {

            CPService offOS = new CPService();
           
            String strFile = System.IO.Path.GetFileName(filename);

           // Uploader.FileUploader srv = new Uploader.FileUploader();
            FileInfo fInfo = new FileInfo(filename);
            long numBytes = fInfo.Length;
            double dLen = Convert.ToDouble(fInfo.Length / 1000000);
            if (dLen < 4)
            {
                FileStream fStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                byte[] data = br.ReadBytes((int)numBytes);
                br.Close();
                string sTmp = offOS.UploadFile(data, strFile);
                fStream.Close();
                fStream.Dispose();
            }
            else
            {
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {
            Byte[] fileBytes = file.GetContents();
            string sPath = System.Configuration.ConfigurationManager.AppSettings["OCA_Folder_NAME"].ToString();
          //  string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Infobase");

           Guid GUID = Guid.NewGuid();
           string Flag_Attach = GUID.ToString() + Path.GetExtension(file.FileName);
           string FullFilename = Path.Combine(sPath, GUID.ToString() + Path.GetExtension(file.FileName));
           FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
           fileStream.Write(fileBytes, 0, fileBytes.Length);
           fileStream.Close();
           Session["Filename"] = Flag_Attach;
           Session["extension"] = Path.GetExtension(file.FileName);
           Session["OriginalFile"] = Path.GetFileName(file.FileName);
          
            
           

            //FileInfo fi = new FileInfo(sPath+"\\" + Session["Filename"]);
            //double size = fi.Length;
            //string folderid = ddlDepartment.SelectedValue;
            //string fun = null;
            //DataTable dt = objCP.Insert_UploadFiles(Session["OriginalFile"].ToString(), Session["extension"].ToString().Replace(".", ""), size, Convert.ToInt32(GetSessionUserID()), folderid, txtTitle.Text, txtContent.Text, Session["Charter_ID"].ToString(), "ADD", fun);
            //string res = dt.Rows[0]["File_Id"].ToString();
            //Session["res"]=res;
            //string F1 = Mid(res, 0, 2);
            //string F2 = Mid(res, 2, 2);
            //string F3 = Mid(res, 4, 2);
            //if (!Directory.Exists(sPath+"\\" + F1 + "\\" + F2 + "\\" + F3))
            //{
            //    Directory.CreateDirectory(sPath + "\\" + F1 + "\\" + F2 + "\\" + F3);
            //}
           
         //   File.Move(sPath+"\\" + Session["Filename"], Path.Combine(sPath + "\\" + F1 + "\\" + F2 + "\\" + F3, GUID.ToString() + Path.GetExtension(file.FileName)));
            
            //UploadFile(sPath + "\\" + F1 + "\\" + F2 + "\\" + F3 + "\\" + Session["Filename"]);
            UploadFile(sPath + "\\" + Session["Filename"]);

        }
        catch (Exception ex)
        {

        }

    }


  
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue != "--Select--")
        {
            string sPath = System.Configuration.ConfigurationManager.AppSettings["OCA_Folder_NAME"].ToString();
            if (HttpContext.Current.Session["Operation"] == null)
            {
                if (HttpContext.Current.Session["Filename"] != null)
                {
                    string savePath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["Info_Path"].ToString());
                    string fileName = Session["Filename"].ToString();
                    FileInfo fi = new FileInfo(savePath + fileName);
                    double size = fi.Length;
                    string folderid = ddlDepartment.SelectedValue;
                    string fun = null;

                    DataTable dt = objCP.Insert_UploadFiles(Session["OriginalFile"].ToString(), Session["extension"].ToString().Replace(".", ""), size, Convert.ToInt32(GetSessionUserID()), folderid, txtTitle.Text, txtContent.Text, Session["Charter_ID"].ToString(), "ADD", fun);
                    string res = dt.Rows[0]["File_Id"].ToString();
                    // string res = Session["res"].ToString();
                    string F1 = Mid(res, 0, 2);
                    string F2 = Mid(res, 2, 2);
                    string F3 = Mid(res, 4, 2);
                    if (!Directory.Exists(savePath + F1 + "\\" + F2 + "\\" + F3))
                    {
                        Directory.CreateDirectory(savePath + F1 + "\\" + F2 + "\\" + F3);
                    }
                    File.Move(savePath + fileName, savePath + F1 + "\\" + F2 + "\\" + F3 + "\\" + res + Session["extension"].ToString());
                    if (!Directory.Exists(sPath + "\\" + F1 + "\\" + F2 + "\\" + F3))
                    {
                        Directory.CreateDirectory(sPath + "\\" + F1 + "\\" + F2 + "\\" + F3);
                    }
                    File.Move(sPath + "\\" + Session["Filename"], sPath + "\\" + F1 + "\\" + F2 + "\\" + F3 + "\\" + res + Session["extension"].ToString());
                    Session["Filename"] = null;
                    BindAttachment();
                    txtContent.Text = "";
                    txtTitle.Text = "";
                    ddlDepartment.SelectedIndex = 0;
                    Session["Operation"] = null;
                    string hidemodal = String.Format("hideModal('dvAddFolder')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
                }
                else
                {
                    string showmodal = String.Format(" showModal('dvAddFolder', true, dvAddFolder_onClose);");

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", showmodal, true);
                }
            }
            else
            {
                objCP.Insert_UploadFiles(null, null, null, Convert.ToInt32(GetSessionUserID()), ddlDepartment.SelectedValue, txtTitle.Text, txtContent.Text, Session["Charter_ID"].ToString(), "EDIT", Session["arg"].ToString());
                BindAttachment();
                txtContent.Text = "";
                txtTitle.Text = "";
                Session["Operation"] = null;
                string hidemodal = String.Format("hideModal('dvAddFolder')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

            }
        }
        else
        {
            string showmodal = String.Format(" showModal('dvAddFolder', true, dvAddFolder_onClose);");

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", showmodal, true);
        }
    }

    public void lbtnPreview_Click(object sender, EventArgs e)
    {
        string sPath = "../Uploads/Infobase/";
        string crewDocPath = ((LinkButton)sender).CommandArgument;
        string F1 = Mid(crewDocPath, 0, 2);
        string F2 = Mid(crewDocPath, 2, 2);
        string F3 = Mid(crewDocPath, 4, 2);
        string filename = ((LinkButton)sender).Text;
        string extension = Path.GetExtension(sPath + F1 + "//" + F2 + "//" + F3 + "//" + filename);
       
        string js = "previewDocument('" + sPath + F1 + "/" + F2 + "/" + F3 + "/" + crewDocPath + extension + "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "loadfile", js, true);

    }


    protected void gvAttachment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAttachment.PageIndex = e.NewPageIndex;

        BindAttachment();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        lblPrimary.Visible = true;
        ddlDepartment.Visible = true;
        AjaxFileUpload2.Visible = true;
        
        Session["Operation"] = null;
        txtContent.Text = "";
        txtTitle.Text = "";
        string showmodal = String.Format(" showModal('dvAddFolder', true, dvAddFolder_onClose);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", showmodal, true);
    }
    


}