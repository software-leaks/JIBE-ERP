using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SMS.Business.Crew;


public partial class Crew_Add_PreJoiningExp : System.Web.UI.Page
{

    BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int CrewID = 0;
        if (Request.QueryString["CrewID"] != null)
        {
            btnSave.Enabled = false;
            if (txtCompanyName.Text.Trim() == "" || txtMonths.Text.Trim() == "" || txtDays.Text.Trim() == "")
            {
                lblMessageText.Text = "Enter all details correctly";
            }
            else
            {
                CrewID = int.Parse(Request.QueryString["CrewID"].ToString());
                objCrewBLL.INS_CrewPreJoiningExp(CrewID, txtCompanyName.Text, int.Parse(ddlRank.SelectedValue), int.Parse(txtMonths.Text), int.Parse(txtDays.Text), int.Parse(Session["USERID"].ToString()));

                lblMessageText.Text = "Pre-Joining Experience saved.";

                string js = "closeDialog('#dialog')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
                ViewState.Clear();
            }
        }
        else
        {
            lblMessageText.Text = "Crew information not valid !!";
        }
    }

    protected void ClearForm()
    {
        lblMessageText.Text = "";
        txtCompanyName.Text = "";
        txtMonths.Text = "";
        txtDays.Text = "";

        btnSave.Enabled = true;
    }



}