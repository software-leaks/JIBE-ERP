using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.Configuration;
using System.IO;
using SMS.Business.QMSDB;
using AjaxControlToolkit4;

public partial class QMSDBProcedureBuilder : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {


        if (AjaxFileUploadInsertImage.IsInFileUploadPostBack)
        {

        }
        else
        {

            if (!IsPostBack)
            {
              DataTable dtprocedure=  BLL_QMSDB_Procedures.QMSDBProcedures_Edit(Convert.ToInt32(Request.QueryString["PROCEDURE_ID"]));
              txtProcedureName.Text = Convert.ToString(dtprocedure.Rows[0]["PROCEDURES_NAME"]);
              txtProcedureCode.Text = Convert.ToString(dtprocedure.Rows[0]["PROCEDURE_CODE"]);

                Session["UploadedFiles_Name"] = "";
              
                Load_Sections();
                if (Request.QueryString["CheckOut"] != null)
                {
                    CheckCheckOut(Request.QueryString["PROCEDURE_ID"]);
                }
                imgbtnPreviewProdecure_Click(null, null); 
            }
            string js = "initScripts();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);


            txtProcedureSectionDetails.config.toolbar = new object[]
              { 
                  new object[] {   "Templates" },
                new object[] { "Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-", "Print", "SpellChecker", "Scayt","Image" },
                new object[] { "Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat" },
                new object[] { "Checkbox", "Radio", "TextField", "Textarea", "Select", "Button", "ImageButton" },
                 new object[] { "InsertImage", "Table", "HorizontalRule", "Smiley", "SpecialChar", "PageBreak"  },
               
                new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
                new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote"  },
                new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
                new object[] { "BidiLtr", "BidiRtl" },
                new object[] { "Link", "Unlink", "Anchor" }, 
             "/",
                             
                new object[] { "Styles", "Format", "Font", "FontSize" },
                new object[] { "TextColor", "BGColor" },
              
            };


            BindUserList();
            btnLoadUploadedFiles_Click(null, null);


        }
    }
   

    int Save_Section_Details()
    {
        return BLL_QMSDB_ProcedureSection.Upd_Section_Details(Convert.ToInt32(ViewState["Selected_Section_Details_ID"]), txtProcedureSectionDetails.Text, Convert.ToInt32(Session["userid"]));
    }

    protected void imgbtnViewSectionDetails_Command(object sender,  CommandEventArgs e)
    {
		 txtProcedureSectionDetails.Visible = true;
        gvSectionList.SelectedIndex = -1;
        gvSectionList.SelectedIndex = ((sender as ImageButton).Parent.Parent as GridViewRow).RowIndex;

        if ( UDFLib.ConvertIntegerToNull(e.CommandArgument) != null)
        {
            if (UDFLib.ConvertIntegerToNull(ViewState["Selected_Section_Details_ID"]) != null)
            {
                if (Is_Selected_Section_Details_Changed())
                    Save_Section_Details();
            }

            DataTable dt = BLL_QMSDB_ProcedureSection.Get_Section_Details(Convert.ToInt32(e.CommandArgument));
            if (dt.Rows.Count > 0)
            {
                txtProcedureSectionDetails.Text = dt.Rows[0]["CHECKOUTDETAILS"].ToString();
                ViewState["Selected_Section_Details_Text"] = txtProcedureSectionDetails.Text;
                ViewState["Selected_Section_Details_ID"] = e.CommandArgument;                 

            } 

        }
    
    }
          
    protected void AjaxFileUploadInsertImage_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {


        Byte[] fileBytes = file.GetContents();

        string sPath = Server.MapPath("/" + ConfigurationManager.AppSettings["QMSDBPath"] + "/");
        string FileName = "QMSDB_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        string FullFilename = Path.Combine(sPath, FileName);

        FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
        fileStream.Write(fileBytes, 0, fileBytes.Length);
        fileStream.Close();
        BLL_QMSDB_ProcedureSection.Ins_Procedure_Attachment(Convert.ToInt32(Request.QueryString["PROCEDURE_ID"]), FileName, Convert.ToInt32(Session["userid"]));
        Session["UploadedFiles_Name"] += FileName + ",";


    }

    protected void btnLoadUploadedFiles_Click(object s, EventArgs e)
    {
        try
        {
            int Max_Height = 400;
            int Max_Width = 400;

            System.Text.StringBuilder size = new System.Text.StringBuilder();

            string sPath = Server.MapPath("/" + ConfigurationManager.AppSettings["QMSDBPath"] + "/");
            foreach (string Fnmae in Convert.ToString(Session["UploadedFiles_Name"]).Split(','))
            {
                if (!string.IsNullOrWhiteSpace(Fnmae))
                {
                    System.Drawing.Image objUpdImg = System.Drawing.Image.FromFile(Path.Combine(sPath, Fnmae));

                    if (objUpdImg.Height > Max_Height)
                        size.Append("height:" + Max_Height.ToString() + "px;");

                    if (objUpdImg.Width > Max_Width)
                        size.Append("width:" + Max_Width.ToString() + "px;");

                    txtProcedureSectionDetails.Text += "<img style='" + size.ToString() + "'  src='/" + ConfigurationManager.AppSettings["QMSDBPath"] + "/" + Fnmae + "'>";
                    size.Clear();
                }
            }

        }
        catch { }
        finally
        {
            //updInserImage.Update();
            Session["UploadedFiles_Name"] = "";
        }
    }
    
    bool Is_Selected_Section_Details_Changed()
    {
        return Convert.ToString(ViewState["Selected_Section_Details_Text"]).Equals(txtProcedureSectionDetails.Text) ? false : true;

    }
    
    void Load_Procedure_Preview()
    {
        DataTable dtSections = BLL_QMSDB_ProcedureSection.Get_All_Sections(Convert.ToInt32(Request.QueryString["PROCEDURE_ID"]));
        lblPreviewProcedure.Text = "";
        foreach (DataRow drSec in dtSections.Rows)
        {
            lblPreviewProcedure.Text += Convert.ToString(drSec["CHECKOUTDETAILS"]);
        }
        updPreview.Update();
    }

    protected void Load_Sections()
    {
        DataTable dt = BLL_QMSDB_ProcedureSection.Get_All_Sections(Convert.ToInt32(Request.QueryString["PROCEDURE_ID"]));
        gvSectionList.DataSource = dt;
        gvSectionList.DataBind();

        ViewState["dtSectionClone"] = dt.Clone();
        UpdatePanelEditSection.Update();

        ScriptManager.RegisterStartupScript(this, this.GetType(), "showeditsection", "showModal('EditSection')", true);

    }

    protected void btnNewSection_Click(object s, EventArgs e)
    {
        DataTable dtSections = ((DataTable)ViewState["dtSectionClone"]).Clone();


        DataRow dr = null;
        foreach (GridViewRow grItem in gvSectionList.Rows)
        {
            dr = dtSections.NewRow();
            dr["Section_ID"] = gvSectionList.DataKeys[grItem.RowIndex].Values["Section_ID"].ToString();
            
            dr["SECTION_HEADER"] = (grItem.FindControl("txtSection_Header") as TextBox).Text;

            dtSections.Rows.Add(dr);

        }

        dr = dtSections.NewRow();
        dr["Section_ID"] = "0";

        dtSections.Rows.Add(dr);
        gvSectionList.DataSource = dtSections;
        gvSectionList.DataBind();
        ViewState["dtSectionClone"] = dtSections.Clone();


    }

    protected void btnChangeSectionPosition_Command(object s, CommandEventArgs e)
    {
        DataTable dtSections = ((DataTable)ViewState["dtSectionClone"]).Clone();
        int SelectedRowIndex = ((s as ImageButton).Parent.Parent as GridViewRow).RowIndex;
        string Action = e.CommandArgument.ToString();

        int AddedRowIndex = -1;
        int Next_Prev_Index = -1;

        DataRow dr = null;
        foreach (GridViewRow grItem in gvSectionList.Rows)
        {



            if (grItem.RowIndex == SelectedRowIndex && string.Equals(Action, "DOWN") && grItem.RowIndex != AddedRowIndex && gvSectionList.Rows.Count != (SelectedRowIndex + 1))
            {
                Next_Prev_Index = SelectedRowIndex + 1;

                dr = dtSections.NewRow();
                dr["Section_ID"] = gvSectionList.DataKeys[Next_Prev_Index].Values["Section_ID"].ToString();
              
                dr["SECTION_HEADER"] = (gvSectionList.Rows[Next_Prev_Index].FindControl("txtSection_Header") as TextBox).Text;
                dtSections.Rows.Add(dr);

                dr = dtSections.NewRow();
                dr["Section_ID"] = gvSectionList.DataKeys[SelectedRowIndex].Values["Section_ID"].ToString();
                
                dr["SECTION_HEADER"] = (gvSectionList.Rows[SelectedRowIndex].FindControl("txtSection_Header") as TextBox).Text;
                dtSections.Rows.Add(dr);

                AddedRowIndex = Next_Prev_Index;

            }
            else if (grItem.RowIndex + 1 == SelectedRowIndex && string.Equals(Action, "UP") && grItem.RowIndex != AddedRowIndex && SelectedRowIndex != 0)
            {


                dr = dtSections.NewRow();
                dr["Section_ID"] = gvSectionList.DataKeys[SelectedRowIndex].Values["Section_ID"].ToString();
               
                dr["SECTION_HEADER"] = (gvSectionList.Rows[SelectedRowIndex].FindControl("txtSection_Header") as TextBox).Text;
                dtSections.Rows.Add(dr);

                dr = dtSections.NewRow();
                dr["Section_ID"] = gvSectionList.DataKeys[grItem.RowIndex].Values["Section_ID"].ToString();
               
                dr["SECTION_HEADER"] = (gvSectionList.Rows[grItem.RowIndex].FindControl("txtSection_Header") as TextBox).Text;
                dtSections.Rows.Add(dr);

                AddedRowIndex = SelectedRowIndex;

            }

            else if (grItem.RowIndex != AddedRowIndex)
            {
                dr = dtSections.NewRow();
                dr["Section_ID"] = gvSectionList.DataKeys[grItem.RowIndex].Values["Section_ID"].ToString();
               
                dr["SECTION_HEADER"] = (grItem.FindControl("txtSection_Header") as TextBox).Text;
                dtSections.Rows.Add(dr);

            }

        }


        gvSectionList.DataSource = dtSections;
        gvSectionList.DataBind();
        ViewState["dtSectionClone"] = dtSections.Clone();



    }
    
    protected void btnSectionSave_Command(object s, CommandEventArgs e)
    {
        DataTable dtSection = new DataTable();
        dtSection.Columns.Add("PKID");
        dtSection.Columns.Add("SECTION_ID");
        dtSection.Columns.Add("SECTION_NAME");
        dtSection.Columns.Add("SECTION_HEADER");
        dtSection.Columns.Add("SECTION_ORDER");

        int pkid = 1;

        foreach (GridViewRow gr in gvSectionList.Rows)
        {
            if (!string.IsNullOrWhiteSpace((gr.FindControl("txtSection_Header") as TextBox).Text))
            {

                DataRow dr = dtSection.NewRow();
                if (gvSectionList.DataKeys[gr.RowIndex].Values["Section_ID"].ToString() == "0")
                {
                    dr["PKID"] = pkid.ToString();
                    dr["SECTION_ID"] = "0";
                    pkid++;
                }
                else
                {
                    dr["PKID"] = "0";
                    dr["SECTION_ID"] = gvSectionList.DataKeys[gr.RowIndex].Values["Section_ID"].ToString();
                }

                
                dr["SECTION_HEADER"] = (gr.FindControl("txtSection_Header") as TextBox).Text;
                dr["SECTION_ORDER"] = gr.RowIndex + 1;

                dtSection.Rows.Add(dr);
            }
        }

        BLL_QMSDB_ProcedureSection.Upd_All_Sections(Convert.ToInt32(Request.QueryString["PROCEDURE_ID"]), dtSection, Convert.ToInt32(Session["USERID"]),txtProcedureCode.Text.Trim(),txtProcedureName.Text.Trim());
        
        if (Is_Selected_Section_Details_Changed())
            Save_Section_Details();

        Load_Sections();
       
        
    }


    protected void btnSectionDelete_Command(object s, CommandEventArgs e)
    {
        btnSectionSave_Command(null, null);
        if (Convert.ToInt32(e.CommandArgument) > 0)
        {
            BLL_QMSDB_ProcedureSection.Upd_Delete_Section(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session["userid"]));
            Load_Sections();
        }
        else
        {
            DataTable dtSections = ((DataTable)ViewState["dtSectionClone"]).Clone();


            DataRow dr = null;
            foreach (GridViewRow grItem in gvSectionList.Rows)
            {
                if (((s as ImageButton).Parent.Parent as GridViewRow).RowIndex != grItem.RowIndex)
                {
                    dr = dtSections.NewRow();
                    dr["Section_ID"] = gvSectionList.DataKeys[grItem.RowIndex].Values["Section_ID"].ToString();
                   
                    dr["SECTION_HEADER"] = (grItem.FindControl("txtSection_Header") as TextBox).Text;

                    dtSections.Rows.Add(dr);
                }
            }

            gvSectionList.DataSource = dtSections;
            gvSectionList.DataBind();
            ViewState["dtSectionClone"] = dtSections.Clone();
                      
        }

        
    }

    protected void btnPublishProcedure_Click(object s, EventArgs e)
    {
        string js = "showModal('DivApproval');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);

       // if (Is_Selected_Section_Details_Changed())
       //     Save_Section_Details();

       // btnSectionSave_Command(null, null);
       // int sts = 0;
       //DataTable dtAttach= BLL_QMSDB_ProcedureSection.Upd_Publish_Procedure(Convert.ToInt32(Request.QueryString["PROCEDURE_ID"]), Convert.ToInt32(Session["USERID"]),ref sts);
       //if (sts == 1)
       //{
       //    // delete the attachment from folder
       //    foreach (DataRow drAttach in dtAttach.Rows)
       //    {
       //        File.Delete(Server.MapPath("/" + ConfigurationManager.AppSettings["QMSDBPath"] + "/") + drAttach["ATTACHMENT_NAME"].ToString());
       //    }
       //    ScriptManager.RegisterStartupScript(this, this.GetType(), "close", "alert('Published successfully.');window.open('','_self');window.close();", true);
       //}

    }


    protected void BtnSaveApproved_Click(object s, EventArgs e)
    {
        
        if (Is_Selected_Section_Details_Changed())
            Save_Section_Details();

        btnSectionSave_Command(null, null);
        int sts = 0;
        DataTable dtAttach = BLL_QMSDB_ProcedureSection.Upd_Publish_Procedure(Convert.ToInt32(Request.QueryString["PROCEDURE_ID"]), Convert.ToInt32(Session["USERID"]),txtApprovalComments.Text, ref sts);
        if (sts == 1)
        {
            // delete the attachment from folder
            foreach (DataRow drAttach in dtAttach.Rows)
            {
                File.Delete(Server.MapPath("/" + ConfigurationManager.AppSettings["QMSDBPath"] + "/") + drAttach["ATTACHMENT_NAME"].ToString());
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "close", "hideModal('DivApproval');alert('Published successfully.');window.open('','_self');window.close();", true);
        }

        //string js = "hideModal('DivApproval');";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);

    }
    protected void btnCancelPublish_Click(object sender, EventArgs e)
    {
        string js = "hideModal('DivApproval');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
    }
    protected void imgbtnPreviewProdecure_Click(object s, EventArgs e)
    {

        if (Is_Selected_Section_Details_Changed())
            Save_Section_Details();

        btnSectionSave_Command(null,null);

        Load_Procedure_Preview();
    }
    public void CheckCheckOut(string procedureid)
    {

        DataTable dsOperationInfo = BLL_QMSDB_Procedures.QMSDBProcedures_Edit(int.Parse(procedureid));
        if (dsOperationInfo.Rows.Count > 0)
        {
            //get the last row of the table to know the current status of the file

            string FileStatus = dsOperationInfo.Rows[0]["CHECK_INOUT_STATUS"].ToString();
            string User = dsOperationInfo.Rows[0]["MODIFIED_BY"].ToString();

            if (FileStatus == "1" && (Convert.ToInt32(Session["USERID"]) != Convert.ToInt32(dsOperationInfo.Rows[0]["MODIFIED_BY"].ToString())))
            {
                string Message = "The file is already Checked Out by " + dsOperationInfo.Rows[0]["MODIFIED_BY"].ToString();
                String msg = String.Format("alert('" + Message + "');window.close();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

            }
            else
                showSaveDialog(procedureid);

        }

    }
    public void showSaveDialog(string procedureid)
    {
        //checking for avoid the multiple Check Out the file
        DataTable dsOperationInfo = BLL_QMSDB_Procedures.QMSDBProcedures_Edit(int.Parse(procedureid));

        if (dsOperationInfo.Rows.Count > 0)
        {
            if (dsOperationInfo.Rows[0]["CHECK_INOUT_STATUS"].ToString() == "1")
            {
                String msg = String.Format("alert('You have already Checked Out the file.');window.close();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

            }
            else
            {
                int i = BLL_QMSDB_Procedures.QMSDBProcedure_CheckOUT(int.Parse(procedureid), Convert.ToInt32(Session["USERID"]));
            }
        }
    }
    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {

        int i = BLL_QMSDB_Procedures.QMSDBProcedures_SendApprovel(int.Parse(Request.QueryString["PROCEDURE_ID"].ToString()), txtComments.Text, UDFLib.ConvertIntegerToNull(lstUser.SelectedValue), ddlStatus.SelectedValue, int.Parse(Session["USERID"].ToString()));
        string js = "alert('Query Saved !!');hideModal('DivSendForApp');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);

    }
    protected void BindUserList()
    {
        BLL_Infra_UserCredentials objInfra = new BLL_Infra_UserCredentials();
        int UserCompanyID = int.Parse(Session["USERCOMPANYID"].ToString());
        DataTable dtUsers = objInfra.Get_UserList(UserCompanyID);
        lstUser.DataSource = dtUsers;
        lstUser.DataBind();
        lstUser.Items.Insert(0, new ListItem("- ALL -", "0"));

    }
    protected void btnSendTo_Click(object sender, EventArgs e)
    {
        string js = "showModal('DivSendForApp');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);

    }
    protected void btnCancelApp_Click(object sender, EventArgs e)
    {
        string js = "hideModal('DivSendForApp');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
    }

    protected void previewbutton_Click(object sender, EventArgs e)
    {
        //if (fileupload.PostedFile != null && fileupload.PostedFile.ContentLength > 0)
        //{
        //    using (var reader = new StreamReader(fileupload.PostedFile.InputStream))
        //    {
        //        txtProcedureSectionDetails.Text = reader.ReadToEnd();
        //    }
        //}
  
    
    // byte[] data = new byte[fileupload.PostedFile.ContentLength];
    ////get file extension
    // string extensioin = fileupload.FileName.Substring(fileupload.FileName.LastIndexOf(".") + 1);
    // string fileType = null;
    ////set the file type based on File Extension
    // switch (extensioin)
    // {
    //     case "doc":
    //         fileType = "application/vnd.ms-word";
    //         break;
    //     case "docx":
    //         fileType = "application/vnd.ms-word";
    //         break;
    //     case "xls":
    //         fileType = "application/vnd.ms-excel";
    //         break;
    //     case "xlsx":
    //         fileType = "application/vnd.ms-excel";
    //         break;
    //     case "jpg":
    //         fileType = "image/jpg";
    //         break;
    //     case "png":
    //         fileType = "image/png";
    //         break;
    //     case "gif":
    //         fileType = "image/gif";
    //         break;
    //     case "pdf":
    //         fileType = "application/pdf";
    //         break;
    // }

       
    // using (Stream stream = fileupload.Poste.OpenStream())
    // {
    //    //read the file as stream

    //     stream.Read(data, 0, data.Length);
    //      int i=  BLL_QMSDB_ProcedureSection.Ins_Procedure_File(1,args.FileName,fileType,data);
        // SqlConnection con = new SqlConnection(connectionString);
        // SqlCommand com = new SqlCommand();
        // com.Connection = con;
        ////set parameters
        // SqlParameter p1 = new SqlParameter("@Name", SqlDbType.VarChar);
        // SqlParameter p2 = new SqlParameter("@FileType", SqlDbType.VarChar);
        // SqlParameter p3 = new SqlParameter("@Data", SqlDbType.VarBinary);
        // p1.Value = args.FileName;
        // p2.Value = fileType;
        // p3.Value = data;
        // com.Parameters.Add(p1);
        // com.Parameters.Add(p2);
        // com.Parameters.Add(p3);
        // com.CommandText = "Insert into Files (Name,FileType,Data) VALUES (@Name,@FileType,@Data)";
        // con.Open();
        ////insert the file into database
        // com.ExecuteNonQuery();
        // con.Close();

     //}
 //}



    }
   
}
