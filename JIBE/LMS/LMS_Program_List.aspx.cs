using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.LMS;
using SMS.Business.Crew;
using System.Text;
using SMS.Properties;
using SMS.Business.Infrastructure;

public partial class LMS_Program_List : System.Web.UI.Page
{
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    public Boolean uaScheduleFlag = false;
    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            bindProgramcategory();
            BindProgramItemInGrid();
            //  ddlDays.DataSource = UDFLib.MonthDays();
            //  ddlDays.DataBind();
            //   ddlDays.Items.Insert(0, "--SELECT--");

        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnAddNewProgram.Enabled = false;

        }
        ViewState["edit"] = objUA.Edit;
        ViewState["del"] = objUA.Delete;
        ViewState["admin"] = objUA.Admin;
    }
    protected bool GetAccessInfo(string s, string d, string c)
    {
        try
        {
            if (s == "d")
            {
                if (ViewState["del"].ToString() == "0")
                {
                    return false;
                }
                else
                {
                    if (c == "1")
                    {
                        if (d.ToString().Length > 0)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }

            return true;
        }
        catch (Exception)
        {

            return true;
        }

    }
    protected void onUpdate(object source, CommandEventArgs e)
    {


    }

    protected void ProgramDelete(object source, CommandEventArgs e)
    {

        int count = 0;
        BLL_LMS_Training.Del_TrainingProgram_Chk(Convert.ToInt32(e.CommandArgument.ToString()), ref count);
        if (count > 0)
        {
            string js = "alert('System cannot delete already scheduled program.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
            return;
        }
        else
        {

            int Result = BLL_LMS_Training.Del_TrainingProgram(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        }

        BindProgramItemInGrid();
    }

    protected void onSchedule(object source, CommandEventArgs e)
    {


    }

    protected void BindProgramItemInGrid()
    {

        string SearchProgramName = UDFLib.ConvertStringToNull(txtProgramName.Text);
        int is_Fetch_Count = ucCustomPagerProgram_List.isCountRecord;
        DataSet ds_ProgramList_Scheduled = BLL_LMS_Training.GET_Program_List(SearchProgramName, UDFLib.ConvertIntegerToNull(ddlProgramCategory.SelectedValue), ucCustomPagerProgram_List.CurrentPageIndex, ucCustomPagerProgram_List.PageSize, ref is_Fetch_Count);
        gvProgram_ListDetails.DataSource = ds_ProgramList_Scheduled;
        gvProgram_ListDetails.DataBind();
        ucCustomPagerProgram_List.CountTotalRec = is_Fetch_Count.ToString();
        ucCustomPagerProgram_List.BuildPager();
    }

    protected void ddlProgramStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindProgramItemInGrid();
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        ddlProgramCategory.SelectedIndex = 0;
        txtProgramName.Text = "";
        BindProgramItemInGrid();
    }
    public void bindProgramcategory()
    {
        DataTable dt = BLL_LMS_Training.Get_Program_Category();
        ddlProgramCategory.DataSource = dt;
        ddlProgramCategory.DataTextField = "Prg_Cat_Name";
        ddlProgramCategory.DataValueField = "Prg_Cat_Id";
        ddlProgramCategory.DataBind();
        ddlProgramCategory.Items.Insert(0, new ListItem("--SELECT--", null));
        ddlProgramCategory.SelectedIndex = 0;

    }


    protected void btnScheduling_Click(object sender, EventArgs e)
    {

        try
        {
            ddlDays.SelectedIndex = 0;
            txtEffectiveStartDate.Text = "";
            txtEffectiveEndDate.Text = "";
            rdbMontPanel.Text = "";
            string js;
            string[] arg = new string[5];
            arg = ((Button)sender).CommandArgument.ToString().Split(';');
            string PROGRAM_NAME;
            Int32 Program_Id;
            Program_Id = Convert.ToInt32(arg[0]);
            PROGRAM_NAME = Convert.ToString(arg[1]);
            string Frequency_Type = UDFLib.ConvertStringToNull(arg[2]);
            DateTime? EffectuveStartDate = UDFLib.ConvertDateToNull(arg[3]);
            DateTime? EffectuveEndDate = UDFLib.ConvertDateToNull(arg[4]);
            if (Frequency_Type == "D")
            {
                rdbDaysPanel.Checked = true;
                rdbMontPanel.Checked = false;

            }
            else
            {
                rdbMontPanel.Checked = true;
                rdbDaysPanel.Checked = false;
            }

            if (EffectuveStartDate != null)
                txtEffectiveStartDate.Text = EffectuveStartDate.Value.ToString("dd/MM/yyyy");
            if (EffectuveEndDate != null)
                txtEffectiveEndDate.Text = EffectuveEndDate.Value.ToString("dd/MM/yyyy");
            ValidatesStates();
            BindProgramItemInGrid();

            foreach (ListItem li in lstSelectedMonth.Items)
            {
                li.Selected = false;
            }
            //foreach (ListItem li in lstSelectedRules.Items)
            //{
            //    li.Selected = false;
            //}
            //ddlDays.SelectedIndex = -1;




            ViewState["Program_Id"] = Convert.ToInt32(Program_Id);

            foreach (ListItem li in lstSelectedMonth.Items)
            {
                li.Selected = false;
            }
            //foreach (ListItem li in lstSelectedRules.Items)
            //{
            //    li.Selected = false;
            //}

            if (PROGRAM_NAME != "")
            {
                lblProgramName.Text = PROGRAM_NAME;
                Bind_Vessel_List();
            }

            js = "showModal('dvTrainingScheduling');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", js, true);
        }
        catch { }

    }

    protected void Bind_Vessel_List()
    {
        DataTable dtVessel = BLL_LMS_Training.Get_VesselList(Convert.ToInt32(ViewState["Program_Id"]), 0, 0, Convert.ToInt32(Session["USERCOMPANYID"]), "", Convert.ToInt32(Session["USERCOMPANYID"]));

        Dictionary<string, bool> VesselSelectedStates = new Dictionary<string, bool>();

        foreach (DataRow row in dtVessel.Rows)
        {
            VesselSelectedStates[row["Vessel_ID"].ToString()] = Convert.ToBoolean(row["Selected"]);
        }
        ViewState["VesselSelectedStates"] = VesselSelectedStates;

        chkLstSelectVessel.DataSource = dtVessel;
        chkLstSelectVessel.DataBind();
        chkLstSelectVessel.Items.Insert(0, new ListItem("SELECT ALL", null));

        string js = "LoadAfterCheckBox();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);

    }



    protected void Load_MonthsAndRules_ForVessel(int Program_Id, int Vessel_Id)
    {
        try
        {

            DataTable dtMonths = BLL_LMS_Training.GET_EVALUATIONMONTHS(Convert.ToInt32(ViewState["Program_Id"]), Vessel_Id);
            DataTable dtRules = BLL_LMS_Training.GET_EVALUATIONRULES(Convert.ToInt32(ViewState["Program_Id"]), Vessel_Id);

            foreach (ListItem li in lstSelectedMonth.Items)
            {
                li.Selected = false;
            }
            //foreach (ListItem li in lstSelectedRules.Items)
            //{
            //    li.Selected = false;
            //}

            foreach (DataRow dr in dtMonths.Rows)
            {
                foreach (ListItem li in lstSelectedMonth.Items)
                {
                    if (li.Value == dr["monthno"].ToString() && dr["active_status"].ToString() == "1")
                        li.Selected = true;
                }

            }

            foreach (DataRow dr in dtRules.Rows)
            {
                //foreach (ListItem li in lstSelectedRules.Items)
                //{
                //    if (li.Value == dr["Days"].ToString() && dr["active_status"].ToString() == "1")
                //        li.Selected = true;
                //}

                if (UDFLib.ConvertIntegerToNull(dr["Days"].ToString()) <= 92)
                {
                    ddlDays.SelectedValue = dr["Days"].ToString();
                }

            }

            if (dtRules.Rows.Count == 0)
            {
                ddlDays.SelectedIndex = -1;
            }

        }
        catch { }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }




    protected void btnSaveScheduleAddNew_Click(object sender, EventArgs e)
    {


        string js;
        if (Convert.ToInt32(ViewState["Program_Id"]) > 0)
        {
            if (UpdateSchedule(Convert.ToInt32(ViewState["Program_Id"])))
            {
                BindProgramItemInGrid();
                js = "hideModal('dvTrainingScheduling');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert3", js, true);
            }
            else
            {
                js = "showModal('dvTrainingScheduling');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showdiv", js, true);
            }



        }
    }

    protected void btnSaveAndCloseSchedule_Click(object sender, EventArgs e)
    {
        string js;
        if (Convert.ToInt32(ViewState["Program_Id"]) > 0)
        {
            if (UpdateSchedule(Convert.ToInt32(ViewState["Program_Id"])))
            {
                BindProgramItemInGrid();
                js = "hideModal('dvTrainingScheduling');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert3", js, true);
            }
            else
            {
                js = "showModal('dvTrainingScheduling');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Showdiv", js, true);
            }



        }



    }


    protected bool UpdateSchedule(int Program_Id)
    {

        Dictionary<string, bool> VesselSelectedStates = (Dictionary<string, bool>)ViewState["VesselSelectedStates"];
        DataTable tablePIDS = new DataTable();
        tablePIDS.Columns.Add("ID", typeof(int));
        tablePIDS.Columns.Add("VALUE", typeof(string));
        string js;

        if (UDFLib.ConvertDateToNull(txtEffectiveStartDate.Text) == null)
        {
            js = "alert('Effective Start Date is required!.');LoadAfterCheckBox();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
            return false;
        }



        if (UDFLib.ConvertDateToNull(txtEffectiveEndDate.Text) != null)
        {
            if (UDFLib.ConvertDateToNull(txtEffectiveEndDate.Text) < UDFLib.ConvertDateToNull(txtEffectiveStartDate.Text))
            {
                js = "alert('Effective End Date should not be lesser than Effective Start Date!.');LoadAfterCheckBox();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true); 
                return false;
            }
            if (UDFLib.ConvertDateToNull(txtEffectiveEndDate.Text).Value.Date < DateTime.Now.Date || UDFLib.ConvertDateToNull(txtEffectiveStartDate.Text).Value.Date < DateTime.Now.Date)
            {
                js = "alert('Effective Start Date or Effective End Date should not be Past Dates!');LoadAfterCheckBox();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);  
                return false;
            }
            if (UDFLib.ConvertDateToNull(txtEffectiveEndDate.Text).Value.Date == DateTime.Now.Date)
            {
                js = "alert('Effective End should not be the current Date!');LoadAfterCheckBox();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
                return false;
            }

        }
        if (UDFLib.ConvertDateToNull(txtEffectiveStartDate.Text).Value.Date < DateTime.Now.Date)
        {
            js = "alert('Effective Start Date should not be Past Dates!');LoadAfterCheckBox();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
            return false;
        }
        if (rdbDaysPanel.Checked == false && rdbMontPanel.Checked == false)
        {
            js = "alert('Rule option not selected!.');LoadAfterCheckBox();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
            return false;
        }



        int ChekedVCount = 0;
        for (int i = 1; i < chkLstSelectVessel.Items.Count; i++)
        {

            if (chkLstSelectVessel.Items[i].Selected)
            {
                tablePIDS.Rows.Add(Convert.ToInt32(chkLstSelectVessel.Items[i].Value), "1");
                ChekedVCount++;


            }
            else
            {
                if (VesselSelectedStates[chkLstSelectVessel.Items[i].Value])
                {
                    tablePIDS.Rows.Add(Convert.ToInt32(chkLstSelectVessel.Items[i].Value), "0");

                }

            }
        }
        if (ChekedVCount == 0)
        {
            js = "alert('Please Select at least one Vessel!.');LoadAfterCheckBox();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
            return false;

        }

        int ChekedMCount = 0;
        foreach (ListItem liMon in lstSelectedMonth.Items)
        {
            if (liMon.Selected == true)
            {

                ChekedMCount++;
            }

        }
        if (rdbMontPanel.Checked)
            if (ChekedMCount == 0)
            {
                js = "alert('Please Select at least one Month!.');LoadAfterCheckBox();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
                return false;

            }


        int ChekedRCount = 0;
        //foreach (ListItem liR in lstSelectedRules.Items)
        //{
        //    if (liR.Selected == true)
        //    {

        //        ChekedRCount++;
        //    }

        //}
        if (ddlDays.SelectedIndex != -1 && ddlDays.SelectedIndex != 0)
        {
            ChekedRCount++;

        }
        //if (ChekedRCount == 0)
        //{
        //    js = "alert('Please Select at least one Rule!.');";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
        //    return false;

        //}

        if (rdbMontPanel.Checked)
            foreach (ListItem liMon in lstSelectedMonth.Items)
            {
                if (liMon.Selected == true)
                {
                    BLL_LMS_Training.Update_EvaluationMonths(Program_Id, tablePIDS, UDFLib.ConvertToInteger(liMon.Value), 1, GetSessionUserID());
                    ChekedMCount++;
                }
                else
                {
                    BLL_LMS_Training.Update_EvaluationMonths(Program_Id, tablePIDS, UDFLib.ConvertToInteger(liMon.Value), 0, GetSessionUserID());
                }
            }




        //foreach (ListItem liRule in lstSelectedRules.Items)
        //{
        //    if (liRule.Selected == true)
        //    {
        //        BLL_LMS_Training.UPDATE_EvaluationRules(Program_Id, tablePIDS, UDFLib.ConvertToInteger(liRule.Value), 1, GetSessionUserID());
        //    }
        //    else
        //    {
        //        BLL_LMS_Training.UPDATE_EvaluationRules(Program_Id, tablePIDS, UDFLib.ConvertToInteger(liRule.Value), 0, GetSessionUserID());
        //    }
        //}

        if (rdbDaysPanel.Checked)
            if (ddlDays.SelectedIndex != -1)
            {
                BLL_LMS_Training.UPDATE_EvaluationRules(Program_Id, tablePIDS, UDFLib.ConvertToInteger(ddlDays.SelectedValue), 1, GetSessionUserID());
            }
        BLL_LMS_Training.Update_Program_Details(Program_Id, rdbMontPanel.Checked ? "M" : "D", UDFLib.ConvertDateToNull(txtEffectiveStartDate.Text), UDFLib.ConvertDateToNull(txtEffectiveEndDate.Text), GetSessionUserID());

        js = "alert('Schedule Planned.');LoadAfterCheckBox();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
        Bind_Vessel_List();

        foreach (ListItem li in lstSelectedMonth.Items)
        {
            li.Selected = false;
        }
        //foreach (ListItem li in lstSelectedRules.Items)
        //{
        //    li.Selected = false;
        //}
        ddlDays.SelectedIndex = -1;
        BindProgramItemInGrid();
        return true;
    }



    protected void lstSelectedRulesIndex_Changed(Object sender, EventArgs e)
    {
        //string js;
        //int countRules = 0;
        //foreach (ListItem li in lstSelectedRules.Items)
        //{
        //    if (li.Selected)
        //    {
        //        countRules = countRules + 1;
        //        if (countRules > 1 || ddlDays.SelectedIndex != 0)
        //        {
        //            li.Selected = false;
        //            js = "alert('Can not select more than one rule.');";
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
        //        }

        //    }

        //}

    }

    protected void ddlDaysIndex_Changed(Object sender, EventArgs e)
    {
        //string js;
        //int countRules = 0;
        //foreach (ListItem li in lstSelectedRules.Items)
        //{
        //    if (li.Selected)
        //    {
        //        countRules = countRules + 1;
        //        if (countRules > 1 || ddlDays.SelectedIndex != 0)
        //        {
        //            ddlDays.SelectedIndex = 0;
        //            js = "alert('Can not select more than one rule.');";
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
        //        }

        //    }

        //}
    }



    protected void chkLstSelectVessel_DataBound(object sender, EventArgs e)
    {

        Dictionary<string, bool> VesselSelectedStates = (Dictionary<string, bool>)ViewState["VesselSelectedStates"];
        int lVessel_ID = 0;
        for (int i = 0; i < chkLstSelectVessel.Items.Count; i++)
        {
            chkLstSelectVessel.Items[i].Selected = VesselSelectedStates[chkLstSelectVessel.Items[i].Value];
            if (lVessel_ID == 0)
            {
                if (VesselSelectedStates[chkLstSelectVessel.Items[i].Value])
                {
                    lVessel_ID = Convert.ToInt32(chkLstSelectVessel.Items[i].Value);
                }

            }
        }


        if (lVessel_ID != 0)
        {
            DataTable dtMonths = BLL_LMS_Training.GET_EVALUATIONMONTHS(Convert.ToInt32(ViewState["Program_Id"]), lVessel_ID);
            DataTable dtRules = BLL_LMS_Training.GET_EVALUATIONRULES(Convert.ToInt32(ViewState["Program_Id"]), lVessel_ID);

            foreach (ListItem li in lstSelectedMonth.Items)
            {
                li.Selected = false;
            }
            //foreach (ListItem li in lstSelectedRules.Items)
            //{
            //    li.Selected = false;
            //}

            foreach (DataRow dr in dtMonths.Rows)
            {
                foreach (ListItem li in lstSelectedMonth.Items)
                {
                    if (li.Value == dr["monthno"].ToString() && dr["active_status"].ToString() == "1")
                        li.Selected = true;
                }

            }
            ddlDays.SelectedIndex = -1;
            foreach (DataRow dr in dtRules.Rows)
            {
                //foreach (ListItem li in lstSelectedRules.Items)
                //{
                //    if (li.Value == dr["Days"].ToString() && dr["active_status"].ToString() == "1")
                //        li.Selected = true;
                //}

                if (UDFLib.ConvertIntegerToNull(dr["Days"].ToString()) <= 92 && dr["active_status"].ToString() == "1")
                {
                    ddlDays.SelectedValue = dr["Days"].ToString();
                }

            }


        }


    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        string js = "hideModal('dvTrainingScheduling');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert3", js, true);
    }
    protected void rdbMontPanel_CheckedChanged(Object sender, EventArgs e)
    {
        ValidatesStates();
    }
    protected void rdbDaysPanel_CheckedChanged(Object sender, EventArgs e)
    {
        ValidatesStates();
    }
    protected void ValidatesStates()
    {
        if (rdbDaysPanel.Checked)
        {
            lstSelectedMonth.Enabled = false;
            ddlDays.Enabled = true;


        }
        else
        {
            lstSelectedMonth.Enabled = true;
            ddlDays.Enabled = false;
        }

         string js = "LoadAfterCheckBox();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
    }
    protected void gvProgram_ListDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ImgDelete = (e.Row.FindControl("ImgDelete") as ImageButton);
            Label lblFreq = (e.Row.FindControl("lblFreq") as Label);
            Label lblEff = (e.Row.FindControl("lblEff") as Label);
            if (lblFreq.Text == "&nbsp;")
                lblFreq.Text = "";
            if (lblEff.Text == "&nbsp;")
                lblEff.Text = "";
            if (string.IsNullOrEmpty(lblFreq.Text) && string.IsNullOrEmpty(lblEff.Text))
            {
                ImgDelete.Visible = true;
            }
            else
            {
                ImgDelete.Visible = false;
            }
        }
    }
}