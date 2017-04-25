using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.Configuration;
using System.IO;
using SMS.Business.QMSDB;
using AjaxControlToolkit4;
using System.Web;

public partial class ProcedureBuilder : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtprocedure = BLL_QMSDB_Procedures.QMSDBProcedures_Edit(Convert.ToInt32(Request.QueryString["PROCEDURE_ID"]));
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
        BindUserList();
        btnLoadUploadedFiles_Click(null, null);
    }


    int Save_Section_Details()
    {
        return BLL_QMSDB_ProcedureSection.Upd_Section_Details(Convert.ToInt32(ViewState["Selected_Section_Details_ID"]), txtProcedureSectionDetails.Content, Convert.ToInt32(Session["userid"]));
    }

    protected void imgbtnViewSectionDetails_Command(object sender, CommandEventArgs e)
    {
        txtProcedureSectionDetails.Visible = true;
        gvSectionList.SelectedIndex = -1;
        gvSectionList.SelectedIndex = ((sender as ImageButton).Parent.Parent as GridViewRow).RowIndex;

        if (UDFLib.ConvertIntegerToNull(e.CommandArgument) != null)
        {
            if (UDFLib.ConvertIntegerToNull(ViewState["Selected_Section_Details_ID"]) != null)
            {
                if (Is_Selected_Section_Details_Changed())
                    Save_Section_Details();
            }

            DataTable dt = BLL_QMSDB_ProcedureSection.Get_Section_Details(Convert.ToInt32(e.CommandArgument));
            if (dt.Rows.Count > 0)
            {
                txtProcedureSectionDetails.Content = dt.Rows[0]["CHECKOUTDETAILS"].ToString();
                ViewState["Selected_Section_Details_Text"] = txtProcedureSectionDetails.Content;
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

                    txtProcedureSectionDetails.Content += "<img style='" + size.ToString() + "'  src='/" + ConfigurationManager.AppSettings["QMSDBPath"] + "/" + Fnmae + "'>";
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
        return Convert.ToString(ViewState["Selected_Section_Details_Text"]).Equals(txtProcedureSectionDetails.Content) ? false : true;

    }

    void Load_Procedure_Preview()
    {
        DataTable dtSections = BLL_QMSDB_ProcedureSection.Get_All_Sections(Convert.ToInt32(Request.QueryString["PROCEDURE_ID"]));
        txtProcedureSectionDetails.Content = "";
        foreach (DataRow drSec in dtSections.Rows)
        {
            txtProcedureSectionDetails.Content += Convert.ToString(drSec["CHECKOUTDETAILS"]);
        }
        //updPreview.Update();
        txtProcedureSectionDetails.PrevMode = true; 
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

        BLL_QMSDB_ProcedureSection.Upd_All_Sections(Convert.ToInt32(Request.QueryString["PROCEDURE_ID"]), dtSection, Convert.ToInt32(Session["USERID"]), txtProcedureCode.Text.Trim(), txtProcedureName.Text.Trim());

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
    }


    protected void BtnSaveApproved_Click(object s, EventArgs e)
    {

        if (Is_Selected_Section_Details_Changed())
            Save_Section_Details();

        btnSectionSave_Command(null, null);
        int sts = 0;
        DataTable dtAttach = BLL_QMSDB_ProcedureSection.Upd_Publish_Procedure(Convert.ToInt32(Request.QueryString["PROCEDURE_ID"]), Convert.ToInt32(Session["USERID"]), txtApprovalComments.Text, ref sts);
        if (sts == 1)
        {
            // delete the attachment from folder
            foreach (DataRow drAttach in dtAttach.Rows)
            {
                File.Delete(Server.MapPath("/" + ConfigurationManager.AppSettings["QMSDBPath"] + "/") + drAttach["ATTACHMENT_NAME"].ToString());
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "close", "hideModal('DivApproval');alert('Published successfully.');window.open('','_self');window.close();", true);
        }

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

        btnSectionSave_Command(null, null);

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

    protected void btnInsertPic_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.Files.Count > 0 && txtProcedureSectionDetails.ActiveMode == AjaxControlToolkit.HTMLEditor.ActiveModeType.Design)
            {
                //string sPath = Server.MapPath("/" + ConfigurationManager.AppSettings["QMSDBPath"] + "/");
                

                try
                {
                    int Max_Height = 400;
                    int Max_Width = 400;

                    System.Text.StringBuilder size = new System.Text.StringBuilder();

                    string sPath = Server.MapPath("/" + ConfigurationManager.AppSettings["QMSDBPath"] + "/");
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                          HttpPostedFile postedFile = Request.Files[i];
                          string FileName = "QMSDB_" + Guid.NewGuid().ToString() + Path.GetExtension(postedFile.FileName);
                         string FullFilename = Path.Combine(sPath, FileName);
                         postedFile.SaveAs(FullFilename); 
                         //FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
                         //fileStream.Write(fileBytes, 0, fileBytes.Length);
                         //fileStream.Close();
                        BLL_QMSDB_ProcedureSection.Ins_Procedure_Attachment(Convert.ToInt32(Request.QueryString["PROCEDURE_ID"]), FileName, Convert.ToInt32(Session["userid"]));

                        if (postedFile.ContentLength > 0)
                        {
                            if (!string.IsNullOrWhiteSpace(postedFile.FileName))
                            {
                                System.Drawing.Image objUpdImg = System.Drawing.Image.FromFile(Path.Combine(sPath, FileName));

                                if (objUpdImg.Height > Max_Height)
                                    size.Append("height:" + Max_Height.ToString() + "px;");

                                if (objUpdImg.Width > Max_Width)
                                    size.Append("width:" + Max_Width.ToString() + "px;");

                                txtProcedureSectionDetails.Content += "<img style='" + size.ToString() + "'  src='/" + ConfigurationManager.AppSettings["QMSDBPath"] + "/" + FileName + "'>";
                                size.Clear();
                            }
                        }
                    }

                }
                catch { }
                finally
                {
                    //updInserImage.Update();
                    Session["UploadedFiles_Name"] = "";
                }

                //Byte[] fileBytes = file.GetContents();

                //string sPath = Server.MapPath("/" + ConfigurationManager.AppSettings["QMSDBPath"] + "/");
                //string FileName = "QMSDB_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                //string FullFilename = Path.Combine(sPath, FileName);

                //FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
                //fileStream.Write(fileBytes, 0, fileBytes.Length);
                //fileStream.Close();
                //BLL_QMSDB_ProcedureSection.Ins_Procedure_Attachment(Convert.ToInt32(Request.QueryString["PROCEDURE_ID"]), FileName, Convert.ToInt32(Session["userid"]));
                //Session["UploadedFiles_Name"] += FileName + ",";

                //Guid gFileName = PhoenixCommonFileAttachment.InsertAttachment(Request.Files, new Guid(ViewState["dtkey"].ToString()), PhoenixModule.PURCHASE, null, ".jpg,.png,.gif");
                //DataTable dt = PhoenixCommonFileAttachment.AttachmentList(new Guid(ViewState["dtkey"].ToString()));
                //DataRow[] dr = dt.Select("FLDDTKEY = '" + gFileName.ToString() + "'");
                //if (dr.Length > 0)
                //    txtItemDetails.Content = txtItemDetails.Content + "<img src=\"" + HttpContext.Current.Session["sitepath"] + "/attachments/" + dr[0]["FLDFILEPATH"].ToString() + "\" />";
            }
            else
            {
                //ucError.Text = Request.Files.Count > 0 ? "You are not in design mode" : "No Picture selected.";
                //ucError.Visible = true;
            }
        }
        catch (Exception ex)
        {
            //ucError.ErrorMessage = ex.Message;
            //ucError.Visible = true;
        }
    }
}
