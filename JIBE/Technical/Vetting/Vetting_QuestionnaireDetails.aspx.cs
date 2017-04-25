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

public partial class Technical_Vetting_Vetting_QuestionnaireDetails : System.Web.UI.Page
{
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (GetSessionUserID() == 0)
                Response.Redirect("~/account/login.aspx");
            UserAccessValidation();
            hfQuestionnaireID.Value = "";
            if (!IsPostBack)
            {
                if (Request.QueryString["Questionnaire_ID"].ToString() != "" && Request.QueryString["Questionnaire_ID"] != null)
                {
                    int Questionnaire_Id = UDFLib.ConvertToInteger(Request.QueryString["Questionnaire_ID"].ToString());
                    ViewState["Questionnaire_ID"] = Questionnaire_Id;
                    hfQuestionnaireID.Value = ViewState["Questionnaire_ID"].ToString();
                   
                }
                if (Request.QueryString["Status"].ToString() != "" && Request.QueryString["Status"] != null)
                {
                    string Status = Request.QueryString["Status"].ToString();
                    if (Status == "Published")
                    {
                        btnPublish.Enabled = false;
                    }
                    else
                    {
                        btnPublish.Enabled = true;
                    }
                }
                 if (Request.QueryString["Mode"] != null)
                {
                    string Mode = Request.QueryString["Mode"].ToString();
                    if (Mode == "A")
                    {
                        ImgAdd.Enabled = true;
                    }
                }

                VET_Get_QuestionList();
                VET_Get_SectionList();
                VET_Get_QuestionnaireDetailsByID();
                VET_Get_QuestionnaireDetails();
                hfVersion.Value = txtVersion.Text;
            
              
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// To check access rights of user for requested page
    /// </summary>
    protected void UserAccessValidation()
    {
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        UserAccess objUA = new UserAccess();
        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUA.View == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");
        }
        if (objUA.Add == 0)
        {

        }
        if (objUA.Edit == 1)
            uaEditFlag = true;

        if (objUA.Delete == 1) uaDeleteFlage = true;
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
         

    
    /// <summary>
    /// Bind Question number by questionnire id
    /// </summary>
    public void VET_Get_QuestionList()
    {
        try
        {
            BLL_VET_Questionnaire objBLLQuest = new BLL_VET_Questionnaire();
            DataTable dtQuestionNo = objBLLQuest.VET_Get_QuestionList(UDFLib.ConvertToInteger(ViewState["Questionnaire_ID"].ToString()));
            DDLQuestion.DataSource = dtQuestionNo;
            DDLQuestion.DataTextField = "Question_No";
            DDLQuestion.DataValueField = "ID";
            DDLQuestion.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// Bind section number by questionnire id
    /// </summary>
    public void VET_Get_SectionList()
    {
        try
        {
            BLL_VET_Questionnaire objBLLQuest = new BLL_VET_Questionnaire();
            DataTable dtSectionNo = objBLLQuest.VET_Get_SectionList(UDFLib.ConvertToInteger(ViewState["Questionnaire_ID"].ToString()));
            DDLSection.DataSource = dtSectionNo;
            DDLSection.DataTextField = "Section_No";
            DDLSection.DataValueField = "Section_No";
            DDLSection.DataBind();        
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Get question no by section no
    /// </summary>
    public void VET_Get_QuestionNoBySectionNo()
    {
        try
        {
            BLL_VET_Questionnaire objBLLQuest = new BLL_VET_Questionnaire();
            DDLQuestion.DataSource = objBLLQuest.VET_Get_QuestionNoBySectionNo(UDFLib.ConvertToInteger(ViewState["Questionnaire_ID"].ToString()), DDLSection.SelectedValues);
                DDLQuestion.DataTextField = "Question_No";
                DDLQuestion.DataValueField = "ID";
                DDLQuestion.DataBind();
           
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
   
    /// <summary>
    /// To bind Questionnaire details by ID
    /// </summary>
    public void VET_Get_QuestionnaireDetailsByID()
    {
        try 
        {
            BLL_VET_Questionnaire objBLLQuest = new BLL_VET_Questionnaire();
            DataTable dtAllFields = objBLLQuest.VET_Get_QuestionnaireDetailsByID(UDFLib.ConvertToInteger(ViewState["Questionnaire_ID"].ToString()));

        
            if (Convert.ToString(dtAllFields.Rows[0]["Module"]) != "")
            {
               
                txtModule.Text = dtAllFields.Rows[0]["Module"].ToString();
                
            }
            if (Convert.ToString(dtAllFields.Rows[0]["VesselTypes"]) != "")
            {
               
                txtVesselType.Text = dtAllFields.Rows[0]["VesselTypes"].ToString();
            }

            if (Convert.ToString(dtAllFields.Rows[0]["Name"]) != "")
            {
                txtQuestionnaire.Text = dtAllFields.Rows[0]["Name"].ToString();
            }
            if (Convert.ToString(dtAllFields.Rows[0]["Vetting_Type_Name"]) != "")
            {
               
                txtVettingType.Text = dtAllFields.Rows[0]["Vetting_Type_Name"].ToString();
            }
            if (Convert.ToString(dtAllFields.Rows[0]["Number"]) != "")
            {
                txtNumber.Text = dtAllFields.Rows[0]["Number"].ToString();
                ViewState["Number"] = dtAllFields.Rows[0]["Number"].ToString();
            }
            if (Convert.ToString(dtAllFields.Rows[0]["Version"]) != "")
            {
                txtVersion.Text = dtAllFields.Rows[0]["Version"].ToString();
                ViewState["Version"] = dtAllFields.Rows[0]["Version"].ToString();
              
            }
            if (Convert.ToString(dtAllFields.Rows[0]["Status"]) != "")
            {             
                ViewState["Status"] = dtAllFields.Rows[0]["Status"].ToString();
              
            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }    
    }

    /// <summary>
    /// To bind Questionniare details as requested by Questionnaire page
    /// </summary>
    public void VET_Get_QuestionnaireDetails()
    {
        try
        {
            BLL_VET_Questionnaire objBLLQuest = new BLL_VET_Questionnaire();
            DataSet ds = new DataSet();
            int rowcount = ucCustomPagerItems.isCountRecord;           
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            ds = objBLLQuest.VET_Get_QuestionnaireDetails(UDFLib.ConvertToInteger(ViewState["Questionnaire_ID"].ToString()),DDLSection.SelectedValues,DDLQuestion.SelectedValues, txtQuestion.Text != "" ? txtQuestion.Text.Trim() : null, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref rowcount);

            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }
            gvQuestionnaire.DataSource = ds.Tables[0];
            gvQuestionnaire.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {               
                btnExport.Enabled = true;
            }
            else
            {
                btnExport.Enabled = false;
            }
            UpdPnlGrid.Update();
           
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }
    protected void btnRetrieve_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VET_Get_QuestionnaireDetails();
            //VET_Get_QuestionList();
            //VET_Get_SectionList();
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
            BLL_VET_Questionnaire objBLLQuest = new BLL_VET_Questionnaire();
            DataSet ds = new DataSet();
            ds = objBLLQuest.VET_Get_VersionNumber(txtNumber.Text.Trim().ToString(), txtVersion.Text.Trim().ToString());
           
            if (ds.Tables[0].Rows.Count > 0)
            {
                string jsSqlError2 = "alert('Version already exist.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
            }
            else
            {
                DataTable dtQuestionnaire = new DataTable();
                dtQuestionnaire = objBLLQuest.VET_Ins_NewVersionQuestionDetails(txtNumber.Text.Trim(), ViewState["Version"].ToString(), txtVersion.Text.Trim(), UDFLib.ConvertToInteger(ViewState["Questionnaire_ID"].ToString()), ViewState["Status"].ToString(), Convert.ToInt32(Session["USERID"]));
                if (dtQuestionnaire.Rows.Count > 0)
                {
                    hfQuestionnaireID.Value = dtQuestionnaire.Rows[0]["Questionnaire_ID"].ToString();
                    ViewState["Questionnaire_ID"] = dtQuestionnaire.Rows[0]["Questionnaire_ID"].ToString();
                    UpdatePanel1.Update();
                }
                string jsSqlError3 = "alert('Data saved successfully.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError3", jsSqlError3, true);

                VET_Get_QuestionnaireDetails();

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
      
    }
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        
        try
        {
            VET_Get_QuestionnaireDetails();
            ExportGridviewToExcel(gvQuestionnaire, "Vetting Questionnaire Details");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    public void ExportGridviewToExcel(GridView GridViewexp, string ExportHeader)
    {

        string filename = String.Format("Vetting_{0}_{1}_{2}.xls", DateTime.Today.Day.ToString(),
            DateTime.Today.Month.ToString(), DateTime.Today.Year.ToString());

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
        HttpContext.Current.Response.Charset = "";

        HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

        HttpContext.Current.Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWriter = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

        // Replace all gridview controls with literals

        GridViewRow grh = GridViewexp.HeaderRow;
        grh.ForeColor = System.Drawing.Color.Black;
        grh.HorizontalAlign = HorizontalAlign.Left;
        GridViewexp.GridLines = GridLines.Both;

        grh.Cells[4].Visible = false;
        foreach (TableCell cl in grh.Cells)
        {
            cl.HorizontalAlign = HorizontalAlign.Left;
            cl.Attributes.Add("class", "text");
            PrepareControlForExport_GridView(cl);

        }
        foreach (GridViewRow gr in GridViewexp.Rows)
        {
            gr.Cells[4].Visible = false;
            foreach (TableCell cl in gr.Cells)
            {
                cl.HorizontalAlign = HorizontalAlign.Left;
                cl.Attributes.Add("class", "text");
                PrepareControlForExport_GridView(cl);

            }
        }

        System.Web.UI.HtmlControls.HtmlForm form
            = new System.Web.UI.HtmlControls.HtmlForm();
        Controls.Add(form);

        Label lbl = new Label();
        lbl.Text = ExportHeader;
        lbl.Font.Size = 14;
        lbl.Font.Bold = true;
        form.Controls.Add(lbl);

        Table tbl = new Table();
        tbl.Attributes.Add("Style", "border:1px solid #A9A9A9");
        //tbl.BorderColor = System.Drawing.Color.Black;
        TableRow tr1 = new TableRow();
        TableRow tr2 = new TableRow();

        tr1.Attributes.Add("Style", "border:1px solid #A9A9A9");
        tr2.Attributes.Add("Style", "border:1px solid #A9A9A9");
      

        TableCell tdModule = new TableCell();
        tdModule.Font.Bold = true;
        tdModule.Text = "Module";
        TableCell tdModuleValue = new TableCell();
        tdModuleValue.Text = txtModule.Text;
        TableCell tdVesTyp = new TableCell();
        tdVesTyp.Font.Bold = true;
        tdVesTyp.Text = "Vessel Type";
        TableCell tdVesTypValue = new TableCell();
        tdVesTypValue.Text = txtVesselType.Text;
        TableCell tdQuestionnire = new TableCell();
        tdQuestionnire.Font.Bold = true;
        tdQuestionnire.Text = "Questionnaire Name";
        TableCell tdQuestionnirValue = new TableCell();
        tdQuestionnirValue.Text = txtQuestionnaire.Text;
        TableCell tdVetTyp = new TableCell();
        tdVetTyp.Font.Bold = true;
        tdVetTyp.Text = "Vetting Type";
        TableCell tdVetTypValue = new TableCell();
        tdVetTypValue.Text = txtVettingType.Text;
        TableCell tdNumber = new TableCell();
        tdNumber.Font.Bold = true;
        tdNumber.Text = "Number";
        TableCell tdNumberValue = new TableCell();
        tdNumberValue.Text = txtNumber.Text;
        TableCell tdVersion = new TableCell();
        tdVersion.Font.Bold = true;
        tdVersion.Text = "Version";
        TableCell tdVersionValue = new TableCell();
        tdVersionValue.Text = txtVersion.Text;

        tr1.Cells.Add(tdModule);
        tr1.Cells.Add(tdModuleValue);
        tr1.Cells.Add(tdVesTyp);
        tr1.Cells.Add(tdVesTypValue);
        tr1.Cells.Add(tdQuestionnire);
        tr1.Cells.Add(tdQuestionnirValue);
        tr2.Cells.Add(tdVetTyp);
        tr2.Cells.Add(tdVetTypValue);
        tr2.Cells.Add(tdNumber);
        tr2.Cells.Add(tdNumberValue);
        tr2.Cells.Add(tdVersion);
        tr2.Cells.Add(tdVersionValue);

        tbl.Rows.Add(tr1);
        tbl.Rows.Add(tr2);

       
        form.Controls.Add(tbl);
        Table tbl1 = new Table();
        TableRow tr3 = new TableRow();
        TableCell td1 = new TableCell();
       
        tr3.Cells.Add(td1);

        tbl1.Rows.Add(tr3);
        form.Controls.Add(tbl1);

        form.Controls.Add(GridViewexp);
        form.RenderControl(htmlWriter);
        string style = @"<style> .text { mso-number-format:\@; } </style> ";
        HttpContext.Current.Response.Write(style);
        HttpContext.Current.Response.Write(stringWriter.ToString());
        HttpContext.Current.Response.End();

    }

    public static void PrepareControlForExport_GridView(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];

            if (current is LinkButton)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as LinkButton).Text;

            }
            else if (current is ImageButton)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as ImageButton).ToolTip;
            }
            else if (current is HyperLink)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as HyperLink).Text;
            }
            else if (current is DropDownList)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as DropDownList).Items.Count > 0 ? (current as DropDownList).SelectedItem.Text : ""; ;
            }
            else if (current is CheckBox)
            {

                current.Visible = false;
            }
            else if (current is TextBox)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as TextBox).Text;

            }
            else if (current is Image)
            {
                TableCell cl = control as TableCell;

                cl.Text = (current as Image).AlternateText;

            }
            else if (current is Label)
            {
                TableCell cl = control as TableCell;
                cl.Text = (current as Label).Text;

            }

        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    protected void onDelete(object source, CommandEventArgs e)
    {
       int  Question_ID = Convert.ToInt32(e.CommandArgument);
        try
        {
         BLL_VET_Questionnaire objBLLQuest = new BLL_VET_Questionnaire();
          int res=  objBLLQuest.VET_Del_Question(Question_ID, Convert.ToInt32(Session["USERID"]));
          if (res > 0)
          {
              VET_Get_QuestionnaireDetails();
              VET_Get_QuestionList();
              VET_Get_SectionList();
              UpdPnlEdit.Update();
          }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }       
    }

    protected void gvQuestionnaire_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblQuestionnaire_id = (Label)e.Row.FindControl("lblQuestionnaire_ID");
                Label lblQuestId = (Label)e.Row.FindControl("lblQuestionId");
                Label lblSection = (Label)e.Row.FindControl("lblSection");
                Label lblLevel1 = (Label)e.Row.FindControl("lblLevel_1");
                Label lblLevel2 = (Label)e.Row.FindControl("lblLevel_2");
                Label lblLevel3 = (Label)e.Row.FindControl("lblLevel_3");
                Label lblQuestion = (Label)e.Row.FindControl("lblQuestion");
                Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");               

                ImageButton ImgEdit = (ImageButton)e.Row.FindControl("ImgUpdate");
                ImgEdit.Attributes.Add("onclick", "document.getElementById('IframeAddQuestion').src ='../Vetting/Vetting_AddQuestion.aspx?Question_ID=" + lblQuestId.Text.Trim() + "&Questionnaire_ID=" + lblQuestionnaire_id.Text.Trim() + "&Section=" + lblSection.Text.Trim() + "&Level_1=" + lblLevel1.Text.Trim() + "&Level_2=" + lblLevel2.Text.Trim() + "&Level_3=" + lblLevel3.Text.Trim() + "&Question=" + lblQuestion.Text.Trim() + "&Remarks=" + lblRemarks.Text.Trim() + "&Addmode=Edit';$('#dvAddQuestion').prop('title', 'Add/Edit Question');showModal('dvAddQuestion');return false;");
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void btnPublish_Click(object sender, EventArgs e)
    {        
        try
        {
            BLL_VET_Questionnaire objBLLQuest = new BLL_VET_Questionnaire();
            DataSet ds = new DataSet();
            ds = objBLLQuest.VET_Get_ExistQuestionForQuestionnaire(UDFLib.ConvertToInteger(ViewState["Questionnaire_ID"].ToString()));

            if (ds.Tables[0].Rows.Count == 0)
            {
                string jsSqlError2 = "alert('Add Question for this Questionnaire.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
            }
            else
            {
                int res= objBLLQuest.VET_Upd_QuestionnaireStatus(ViewState["Version"].ToString(), ViewState["Number"].ToString(), Convert.ToInt32(Session["USERID"].ToString()),UDFLib.ConvertToInteger(ViewState["Questionnaire_ID"].ToString()));
                if (res > 0)
                {
                    string jsSqlError3 = "alert('Questionnaire published.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError3", jsSqlError3, true);
                    VET_Get_QuestionnaireDetails();
                    btnPublish.Enabled = false;
                    btnSave.Enabled = false;
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
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {          
            txtVersion.Enabled = true;
            ImgAdd.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void btnClearFilter_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtQuestion.Text = "";
           
            DDLSection.ClearSelection();
            DDLQuestion.ClearSelection();
            VET_Get_QuestionList();
            VET_Get_QuestionnaireDetails();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }    
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Version"].ToString() != txtVersion.Text.ToString())
            {
                txtVersion.Text = ViewState["Version"].ToString();
            }
           
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void hdnRetrive_Click(object sender, EventArgs e)
    {
        try
        {           
            VET_Get_QuestionnaireDetails();
            VET_Get_QuestionList();
            VET_Get_SectionList();
            UpdPnlEdit.Update();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
}