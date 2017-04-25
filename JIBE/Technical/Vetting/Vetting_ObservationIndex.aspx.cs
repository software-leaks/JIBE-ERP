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

public partial class Technical_Vetting_Vetting_ObservationIndex : System.Web.UI.Page
{
    public Boolean uaEditFlag = true;
    public Boolean uaDeleteFlage = true;


    protected void Page_Load(object sender, EventArgs e)
 {
     try
     {
         if (GetSessionUserID() == 0)
             Response.Redirect("~/account/login.aspx");

         String msgretv = String.Format("setTimeout(getOperatingSystem,500);");
         ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);
         UserAccessValidation();
         setDateFormat();

         if (!IsPostBack)
         {

             ViewState["Status"] = "";
             VET_GET_QuestionnireList();
             BindFleetDLL();
             if (Session["USERFLEETID"] == null)
             {
                 DDLFleet.SelectedValue = "0";
             }
             else
             {
                 DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
             }

             if (Request.QueryString["Question_ID"] != null && Request.QueryString["FleetCode"] != null && Request.QueryString["Vetting_Type_ID"] != null && Request.QueryString["Status"] != null)
             {
                 int Question_ID = UDFLib.ConvertToInteger(Request.QueryString["Question_ID"].ToString());
                 ViewState["Question_ID"] = Request.QueryString["Question_ID"].ToString();

                 int FleetCode = UDFLib.ConvertToInteger(Request.QueryString["FleetCode"].ToString());
                 ViewState["FleetCode"] = Request.QueryString["FleetCode"].ToString();

                 int Vetting_Type_ID = UDFLib.ConvertToInteger(Request.QueryString["Vetting_Type_ID"].ToString());
                 ViewState["Vetting_Type_ID"] = Request.QueryString["Vetting_Type_ID"].ToString();

                 string Status = Request.QueryString["Status"].ToString();
                 ViewState["Status"] = Request.QueryString["Status"].ToString();

                 VET_Get_QuestionnaireIdByQuestionId();
                 VET_Get_SectionByQuestionnireId();
                 VET_Get_QuestionNoByQuestionnireId();
                 DDLQuestion.Select(ViewState["Question_ID"].ToString());

                 DDLFleet.SelectedValue = ViewState["FleetCode"].ToString();

                 Bind_ObservationIndex();

             }

             VET_Get_OilMajorListObs();
             VET_Get_InspectorListObs();
             Bind_RiskLevel();
             VET_Get_ObservationCategories();
             BindVesselDDLByFleet();
             Bind_ObservationIndex();
         }
     
         }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
   
    /// <summary>
    /// Set date format
    /// </summary>
    public void setDateFormat()
    {        
        cexLObsFromDate.Format = UDFLib.GetDateFormat();
        cexLObsToDate.Format = UDFLib.GetDateFormat();        
    }

    /// <summary>
    /// To check access rights of user for requested page
    /// </summary>
    protected void UserAccessValidation()
    {
        try
        {
            UserAccess objUA = new UserAccess();
            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

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
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
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

    /// <summary>
    /// Method is used to get Questionnire details of particular question
    /// </summary>
    public void VET_Get_QuestionnaireIdByQuestionId()
    {
        try
        {
            BLL_VET_Index objBLLIndx = new BLL_VET_Index();
            DataTable dtQuestionnire = objBLLIndx.VET_Get_QuestionnaireIdByQuestionId(UDFLib.ConvertToInteger(ViewState["Question_ID"].ToString()));
            if (dtQuestionnire.Rows.Count > 0)
            {
                DDLQuestionnaire.Select(Convert.ToString(dtQuestionnire.Rows[0]["Questionnaire_ID"]));
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
    /// Bind Questionnire list
    /// </summary>
    public void VET_GET_QuestionnireList()
    {
        try
        {
            BLL_VET_Index objBLLIndx = new BLL_VET_Index();
            DataTable dtQuestionnire = objBLLIndx.VET_GET_QuestionnireList();
            DDLQuestionnaire.DataSource = dtQuestionnire;
            DDLQuestionnaire.DataTextField = "Name";
            DDLQuestionnaire.DataValueField = "Questionnaire_ID";
            DDLQuestionnaire.DataBind();
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
    public void VET_Get_SectionByQuestionnireId()
    {
        try
        {
            
            if (DDLQuestionnaire.SelectedValues.Rows.Count > 0)
            {
                BLL_VET_Index objBLLIndx = new BLL_VET_Index();
                DataTable dtSectionNo = objBLLIndx.VET_Get_SectionByQuestionnireId(DDLQuestionnaire.SelectedValues);
                DDLSection.DataSource = dtSectionNo;
                DDLSection.DataTextField = "Section_No";
                DDLSection.DataValueField = "Section_No";
                DDLSection.DataBind();
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
    /// Bind Question number by questionnire id
    /// </summary>
    public void VET_Get_QuestionNoByQuestionnireId()
    {
        try
        {
            if (DDLQuestionnaire.SelectedValues.Rows.Count > 0)
            {
                BLL_VET_Index objBLLIndx = new BLL_VET_Index();
                DataTable dtQuestionNo = objBLLIndx.VET_Get_QuestionNoByQuestionnireId(DDLQuestionnaire.SelectedValues,DDLSection.SelectedValues);
                DDLQuestion.DataSource = dtQuestionNo;
                DDLQuestion.DataTextField = "Question_No";
                DDLQuestion.DataValueField = "ID";
                DDLQuestion.DataBind();
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
    /// Method is used to bind section and question no. dropdown
    /// </summary>
    public void DDLQuestionnaireApplySearch()
    {
        try
        {
            VET_Get_SectionByQuestionnireId();
            VET_Get_QuestionNoByQuestionnireId();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Bind fleet 
    /// </summary>
    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            
                DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
                DDLFleet.Items.Clear();
                DDLFleet.DataSource = FleetDT;
                DDLFleet.DataTextField = "Name";
                DDLFleet.DataValueField = "code";
                DDLFleet.DataBind();                
                ListItem li = new ListItem("-Select-", "0");
                DDLFleet.Items.Insert(0, li);           
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    
  
    /// <summary>
    /// Bind Vessel list as per fleet selection
    /// </summary>
    public void BindVesselDDLByFleet()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            if (chkVesselAssign.Checked == true)
            {
                DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

                DDLVesselObs.DataTextField = "Vessel_name";
                DDLVesselObs.DataValueField = "Vessel_id";
                DDLVesselObs.DataSource = dtVessel;
                DDLVesselObs.DataBind();
                BLL_VET_VettingLib objBLLVetLib = new BLL_VET_VettingLib();
                DataTable dtUserVessel = objBLLVetLib.VET_Get_UserVesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID());
                if (dtUserVessel.Rows.Count > 0)
                {
                    CheckBoxList chk = (CheckBoxList)DDLVesselObs.Controls[0].Controls[0].FindControl("CheckBoxListItems");
                    for (int j = 0; j < chk.Items.Count; j++)
                    {
                        for (int i = 0; i < dtUserVessel.Rows.Count; i++)
                        {
                            if (chk.Items[j].Value == dtUserVessel.Rows[i]["Vessel_ID"].ToString())
                            {
                                ((CheckBoxList)DDLVesselObs.Controls[0].Controls[0].FindControl("CheckBoxListItems")).Items[j].Selected = true;
                            }
                        }
                    }
                }
            }
            else
            {
                DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

                DDLVesselObs.DataTextField = "Vessel_name";
                DDLVesselObs.DataValueField = "Vessel_id";
                DDLVesselObs.DataSource = dtVessel;
                DDLVesselObs.DataBind();
            }
        }
            
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindVesselDDLByFleet();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Bind oil major list
    /// </summary>
    public void VET_Get_OilMajorListObs()
    {
        try
        {
            BLL_VET_VettingLib objBLLVetLib = new BLL_VET_VettingLib();
            DataTable dtOilMajorNamesObs = objBLLVetLib.VET_Get_OilMajorList();
            DDLOilMajorObs.DataSource = dtOilMajorNamesObs;
            DDLOilMajorObs.DataTextField = "Display_Name";
            DDLOilMajorObs.DataValueField = "ID";
            DDLOilMajorObs.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// Bind Inspector list
    /// </summary>
    public void VET_Get_InspectorListObs()
    {
        try
        {
            BLL_VET_VettingLib objBLLVetLib = new BLL_VET_VettingLib();
            DataTable dt = objBLLVetLib.VET_Get_InspectorList();
            DDLInspectorObs.DataSource = dt;
            DDLInspectorObs.DataTextField = "NAME";
            DDLInspectorObs.DataValueField = "UserID";
            DDLInspectorObs.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Bind risk level
    /// </summary>
    public void Bind_RiskLevel()
    {
        try
        {
            DataTable dt = new DataTable();            
            dt.Columns.Add("RiskLevel");

            dt.Rows.Add("1");
            dt.Rows.Add("2");
            dt.Rows.Add("3");
            dt.Rows.Add("4");
            dt.Rows.Add("5");

            DDLRiskLevel.DataSource = dt;
            DDLRiskLevel.DataTextField = "RiskLevel";
            DDLRiskLevel.DataValueField = "RiskLevel";
            DDLRiskLevel.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Bind observation categories
    /// </summary>
    public void VET_Get_ObservationCategories()
    {
        try
        {
            BLL_VET_Index objBLLIndx = new BLL_VET_Index();
            string Mode = "Edit";
            DataTable dt = objBLLIndx.VET_Get_ObservationCategories(Mode);
            DDLCategories.DataSource = dt;
            DDLCategories.DataTextField = "OBSCategory_Name";
            DDLCategories.DataValueField = "OBSCategory_ID";
            DDLCategories.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Bind all records of observation index
    /// </summary>
    public void Bind_ObservationIndex()
    {
        try
        {
            BLL_VET_Index objBLLIndx = new BLL_VET_Index();
            DataSet ds = new DataSet();
            int rowcount = ucCustomPagerItemsObs.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            
            DataTable dtInspector = new DataTable();
            dtInspector.Columns.Add("ID");
            DataTable dtEXInspector = new DataTable();
            dtEXInspector.Columns.Add("ID");          
                        
           foreach (DataRow dr in DDLInspectorObs.SelectedValues.Rows)
            {
                if (dr[0].ToString().Split('_')[1] == "In")
                {
                    dtInspector.Rows.Add(dr[0].ToString().Split('_')[0]);
                }
                if (dr[0].ToString().Split('_')[1] == "Ex")
                {
                    dtEXInspector.Rows.Add(dr[0].ToString().Split('_')[0]);
                }
            }

           DateTime? LObsFromDate, LObsToDate;
            LObsFromDate = txtLObsFromDate.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtLObsFromDate.Text));
            LObsToDate = txtLObsToDate.Text.Trim() == "" ? null : UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtLObsToDate.Text));
            ds = objBLLIndx.VET_Get_ObservationIndex(DDLQuestionnaire.SelectedValues, DDLSection.SelectedValues, DDLQuestion.SelectedValues, UDFLib.ConvertToInteger(rbtnType.SelectedValue), ViewState["Status"].ToString(), UDFLib.ConvertToInteger(DDLFleet.SelectedValue), DDLVesselObs.SelectedValues, DDLOilMajorObs.SelectedValues, dtInspector, dtEXInspector, DDLCategories.SelectedValues, DDLRiskLevel.SelectedValues, txtObservationVessel.Text != "" ? txtObservationVessel.Text.Trim() : null, LObsFromDate, LObsToDate, sortbycoloumn, sortdirection, ucCustomPagerItemsObs.CurrentPageIndex, ucCustomPagerItemsObs.PageSize, ref rowcount);

            if (ds.Tables.Count > 0)
            {  
                ViewState["dtObsResponse"] = ds.Tables[1];
                ViewState["dtObsJobsCount"] = ds.Tables[2];
                ViewState["dtObsvationCount"] = ds.Tables[3];
            }

           if (ucCustomPagerItemsObs.isCountRecord == 1)
            {
                ucCustomPagerItemsObs.CountTotalRec = rowcount.ToString();
                ucCustomPagerItemsObs.BuildPager();
            }
           gvObservationIndex.DataSource = ds.Tables[0];
           gvObservationIndex.DataBind();
           if (ds.Tables[0].Rows.Count>0)
           {
               btnObsExport.Enabled = true;
           }
           else
           {
               btnObsExport.Enabled = false;
           }

            UpdPnlGridObs.Update();
            UpdPnlFilterObs.Update();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void btnRetrieveData_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ucCustomPagerItemsObs.CurrentPageIndex = 1;
            Bind_ObservationIndex();
            if (hfAdv.Value == "o")
            {
                String tgladvsearch = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearch", tgladvsearch, true);
            }
            else
            {
                String tgladvsearch1 = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearch1", tgladvsearch1, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void btnClearAllFilter_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DDLQuestionnaire.ClearSelection();
            DDLSection.ClearSelection();
            DDLQuestion.ClearSelection();
            rbtnType.SelectedValue = "0";
            DDLFleet.SelectedValue = "0";
            DDLVesselObs.ClearSelection();
            DDLOilMajorObs.ClearSelection();
            DDLInspectorObs.ClearSelection();
            txtObservationVessel.Text = "";
            DDLCategories.ClearSelection();
            DDLRiskLevel.ClearSelection();
            txtLObsFromDate.Text = "";
            txtLObsToDate.Text = "";
            chkVesselAssign.Checked = true;
            BindVesselDDLByFleet();
            UpdAdvFltrObs.Update();
            Bind_ObservationIndex();

            if (hfAdv.Value == "o")
            {
                String tgladvsearchClr = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearchClr", tgladvsearchClr, true);
            }
            else
            {
                String tgladvsearchClr1 = String.Format("toggleOnSearchClearFilter(advText,'" + hfAdv.Value + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tgladvsearchClr1", tgladvsearchClr1, true);
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
    /// To Export gridview data to excel
    /// </summary>
    /// <param name="GridViewexp"> Grid view name </param>
    /// <param name="ExportHeader"> Dislay Header in excel </param>
    public void ExportGridviewToExcel(GridView GridViewexp, string ExportHeader)
    {

        try
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

            grh.Cells[11].Visible = false;
            foreach (TableCell cl in grh.Cells)
            {
                cl.HorizontalAlign = HorizontalAlign.Left;
                cl.Attributes.Add("class", "text");
                PrepareControlForExport_GridView(cl);

            }
            foreach (GridViewRow gr in GridViewexp.Rows)
            {
                gr.Cells[11].Visible = false;
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
            form.Controls.Add(GridViewexp);
            form.RenderControl(htmlWriter);
            string style = @"<style> .text { mso-number-format:\@; } </style> ";
            HttpContext.Current.Response.Write(style);
            HttpContext.Current.Response.Write(stringWriter.ToString());
            HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public static void PrepareControlForExport_GridView(Control control)
    {
        try
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
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    protected void btnObsExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Bind_ObservationIndex();

            ExportGridviewToExcel(gvObservationIndex, "Observation Index");
           
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void gvObservationIndex_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = e.SortExpression;

            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;

            Bind_ObservationIndex();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void gvObservationIndex_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblObsQuestionnire_ID = (Label)e.Row.FindControl("lblQuestionnire_ID");
                Label lblObsQuestion_ID = (Label)e.Row.FindControl("lblQuestion_ID");
                Label lblObservationID = (Label)e.Row.FindControl("lblObservation_ID");
                Label lblVesselID = (Label)e.Row.FindControl("lblVeseelID");
                Label lblVettingTypID = (Label)e.Row.FindControl("lblVetTypeID");
                Label lblVesselName = (Label)e.Row.FindControl("lblVessel");
                Label lblVettingId = (Label)e.Row.FindControl("lblVettingID");
                Label lblVetTypeName = (Label)e.Row.FindControl("lblVetType");

                Label lblObsVettingDate = (Label)e.Row.FindControl("lblVettingDate");

                HyperLink hplnkRelatedJobsCount = (HyperLink)e.Row.FindControl("hplnkRelatedJobs");
                HyperLink hplnkResponsesCount = (HyperLink)e.Row.FindControl("hplnkResponses");
                HiddenField hdnObCount = (HiddenField)e.Row.FindControl("hdnObCount");
                HiddenField hdnFleetCode = (HiddenField)e.Row.FindControl("hdnFleetCode");
                ImageButton ImgDetails = (ImageButton)e.Row.FindControl("ImgDetails");

               for (int i = 9; i < e.Row.Controls.Count; i++)
                {
                    string RelatedJobsCount, ResponseCount, ObsCount;

                    DataTable dtObsJobsCount = (DataTable)ViewState["dtObsJobsCount"];
                    string filterobsreljobexpression = " Question_ID=" + lblObsQuestion_ID.Text + " and Observation_ID=" + lblObservationID.Text;
                    DataRow[] drObsRelJobCount = dtObsJobsCount.Select(filterobsreljobexpression);
                    if (drObsRelJobCount.Length <= 0)
                    {
                        RelatedJobsCount = "0";
                        hplnkRelatedJobsCount.Text = RelatedJobsCount;
                    }
                    else
                    {
                        RelatedJobsCount = drObsRelJobCount[0][0].ToString();
                        hplnkRelatedJobsCount.Text = RelatedJobsCount;
                    }

                    DataTable dtObsResponse = (DataTable)ViewState["dtObsResponse"];
                    string filterresponseexpression = " Question_ID=" + lblObsQuestion_ID.Text + " and Observation_ID=" + lblObservationID.Text;
                    DataRow[] drResponseCount = dtObsResponse.Select(filterresponseexpression);
                    if (drResponseCount.Length <= 0)
                    {
                        ResponseCount = "0";
                        hplnkResponsesCount.Text = ResponseCount;
                    }
                    else
                    {
                        ResponseCount = drResponseCount[0][0].ToString();
                        hplnkResponsesCount.Text = ResponseCount;
                    }

                    DataTable dtObsvationCount = (DataTable)ViewState["dtObsvationCount"];
                    string filterrobsexpression = " Question_ID=" + lblObsQuestion_ID.Text + " and FleetCode=" + hdnFleetCode.Value;
                    DataRow[] drObsCount = dtObsvationCount.Select(filterrobsexpression);
                    if (drObsCount.Length <= 0)
                    {
                        ObsCount = "0";
                        hdnObCount.Value = ObsCount;
                    }
                    else
                    {
                        ObsCount = drObsCount[0][0].ToString();
                        hdnObCount.Value = ObsCount;
                    }     
                }             

               if (lblObsVettingDate.Text != "")
               {
                   lblObsVettingDate.Text = UDFLib.ConvertUserDateFormat(lblObsVettingDate.Text, UDFLib.GetDateFormat());
               }

               ImgDetails.Attributes.Add("onclick", "document.getElementById('IframeAddNote_Observation').src ='Vetting_AddObservationNotes.aspx?FleetCode=" + hdnFleetCode.Value + "&Vessel_ID=" + lblVesselID.Text + "&Vessel_Name=" + lblVesselName.Text + "&Observation_ID=" + lblObservationID.Text + "&Question_ID=" + lblObsQuestion_ID.Text + "&Vetting_ID=" + lblVettingId.Text + "&Vetting_Type_ID=" + lblVettingTypID.Text + "&Vetting_Type_Name=" + lblVetTypeName.Text + "&Opn_Obs_Count=" + hdnObCount.Value + "&Mode=Edit'; $('#dvAddNote_Observation').prop('title', 'Add Note/Observation');showModal('dvAddNote_Observation',true,CloseNote_Observation);return false;");
              
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
    /// Event is use to call function that retrive forms details according to vessels assigned to login user.
    /// </summary>
    protected void chkVesselAssign_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            BindVesselDDLByFleet();
            UpdPnlFilterObs.Update();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }
    
}