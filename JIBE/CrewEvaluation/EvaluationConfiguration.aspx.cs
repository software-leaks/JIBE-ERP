using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class CrewEvaluation_EvaluationConfiguration : System.Web.UI.Page
{
    BLL_Crew_Admin cObjCrewAdmin = new BLL_Crew_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                UserAccessValidation();
                rdbEval.Checked = true;
                lstParentRanks.SelectedIndex = -1;
                Bind_Evaluations_Ranks();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void UserAccessValidation()
    {

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        UserAccess objUA = new UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");



        if (objUA.Admin == 0)
        {
         //   btnAssign.Visible = false; 

        }
        


 




    }
    protected void Bind_Evaluations_Ranks()
    {
        DataTable dt = cObjCrewAdmin.Get_RankList();
        lstParentRanks.DataSource = dt;
        lstParentRanks.DataBind();
        lstParentRanks.Items.Insert(0, new ListItem("-  SELECT  -", null));
        chkLstSelectRank.DataSource = dt;
        chkLstSelectRank.DataBind();
        chkLstSelectRank.Items.Insert(0, new ListItem("SELECT ALL", null));
        chkVerifiersRank.DataSource = dt;
        chkVerifiersRank.DataBind();
        chkVerifiersRank.Items.Insert(0, new ListItem("SELECT ALL", null));


        string js = "LoadAfterCheckBox();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
        btnAssign.Enabled = false;
        chkLstSelectRank.Enabled = false;

    }
    protected void lstParentRanks_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < chkLstSelectRank.Items.Count; i++)
        {
            chkLstSelectRank.Items[i].Selected = false;
        }
        if (lstParentRanks.SelectedIndex > 0)
        {
            btnAssign.Enabled = true;
            chkLstSelectRank.Enabled = true;
            chkVerifiersRank.Enabled = true;
             
        }
        else
        {
            btnAssign.Enabled = false;
            chkLstSelectRank.Enabled = false;
            chkVerifiersRank.Enabled = false;
            return;
        }
        DataTable dt;
        if (rdbEval.Checked || rdbHandover.Checked)
        {
            if (rdbEval.Checked)
                dt = BLL_Crew_Evaluation.Get_EvaluationRanks(UDFLib.ConvertToInteger(lstParentRanks.SelectedValue), "EVAL").Tables[0];
            else
                dt = BLL_Crew_Evaluation.Get_EvaluationRanks(UDFLib.ConvertToInteger(lstParentRanks.SelectedValue), "HAND").Tables[0];
            for (int i = 1; i < chkLstSelectRank.Items.Count; i++)
            {

                DataRow[] drs = dt.Select("ChildRank = " + chkLstSelectRank.Items[i].Value);
                if (drs.Length > 0)
                {
                    chkLstSelectRank.Items[i].Selected = true;
                }
                else
                {
                    chkLstSelectRank.Items[i].Selected = false;
                }

            }
        }
        else
        {
            dt = BLL_Crew_Evaluation.Get_EvaluationRanks(UDFLib.ConvertToInteger(lstParentRanks.SelectedValue),"RESTS").Tables[0];
            for (int i = 1; i < chkLstSelectRank.Items.Count; i++)
            {

                DataRow[] drs = dt.Select("ChildRank = " + chkLstSelectRank.Items[i].Value);
                if (drs.Length > 0)
                {
                    chkLstSelectRank.Items[i].Selected = true;
                }
                else
                {
                    chkLstSelectRank.Items[i].Selected = false;
                }

            }

            DataTable dt1 = BLL_Crew_Evaluation.Get_EvaluationRanks(UDFLib.ConvertToInteger(lstParentRanks.SelectedValue), "RESTV").Tables[0];
            for (int i = 1; i < chkLstSelectRank.Items.Count; i++)
            {

                DataRow[] drs = dt1.Select("ChildRank = " + chkLstSelectRank.Items[i].Value);
                if (drs.Length > 0)
                {
                    chkVerifiersRank.Items[i].Selected = true;
                }
                else
                {
                    chkVerifiersRank.Items[i].Selected = false;
                }

            }
        }
        string js = "LoadAfterCheckBox();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable table = new DataTable();
            table.Columns.Add("PID");
            for (int i = 1; i < chkLstSelectRank.Items.Count; i++)
            {

                if (chkLstSelectRank.Items[i].Selected)
                {
                    table.Rows.Add(chkLstSelectRank.Items[i].Value);
                }

            }

            DataTable table1 = new DataTable();
            table1.Columns.Add("PID");
            for (int i = 1; i < chkVerifiersRank.Items.Count; i++)
            {

                if (chkVerifiersRank.Items[i].Selected)
                {
                    table1.Rows.Add(chkVerifiersRank.Items[i].Value);
                }

            }
            string Configuration_Type = "";
            if (rdbEval.Checked)
            {
                Configuration_Type = "EVAL";
            }
            else if (rdbRest.Checked)
            {
                Configuration_Type = "REST";
            }
            else if (rdbHandover.Checked)
            {
                Configuration_Type = "HAND";
            }
             BLL_Crew_Evaluation.Update_EvaluationRanks(UDFLib.ConvertToInteger(lstParentRanks.SelectedValue), table,table1,Configuration_Type, UDFLib.ConvertToInteger(Session["USERID"]));
             lstParentRanks_SelectedIndexChanged(null, null);
            string js = "alertmsg(1);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg1", js, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void rdbEval_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbEval.Checked)
        {
            lblTitle.Text = "configuration for showing evaluations to evaluator based on the crew rank";
            col3.Visible = false;
            chkVerifiersRank.Visible = false;
            ver.Visible = false;

            col1.Text = "Evaluator's Ranks";
            col2.Text = "Evaluation Ranks"; ;
            btnAssign.Text = "Save Evaluation Configuration";
        }
        if (rdbHandover.Checked)
        {
            lblTitle.Text = "configuration for showing handover report based on the crew rank";
            col3.Visible = false;
            chkVerifiersRank.Visible = false;
            ver.Visible = false;

            col1.Text = "Handover's Ranks";
            col2.Text = "Viewable Ranks"; ;
            btnAssign.Text = "Save HandOver Configuration";
        }
        else if (rdbRest.Checked)
        {
            lblTitle.Text = "configuration for showing-verifying the Resthour details based on the crew rank";
            col3.Visible = true;
            chkVerifiersRank.Visible = true;
            ver.Visible = true;
            col1.Text = "RestHours Ranks";
            col2.Text = "Viewable Ranks";
            btnAssign.Text = "Save Resthours Configuration";
            
        }
        lstParentRanks_SelectedIndexChanged(null, null);
    }
}