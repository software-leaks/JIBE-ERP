using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.PortageBill;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Crew_Libraries_Seniority : System.Web.UI.Page
{
    BLL_Crew_Admin objBLLCrewAdmin = new BLL_Crew_Admin();
    int RankID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Enabled = false;
            btnUpdateCompanySeniorityRates.Enabled = false;
            btnAddCompanySeniority.Enabled = false;
            UserAccessValidation();
            Load_RankList();
            Load_SeniorityGrid(0);
        }
    }
    protected void UserAccessValidation()
    {
        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            dvPageContent.Visible = false;
        }
        if (objUA.Add == 0)
        {
            
        }
        if (objUA.Edit == 0)
        {
           // gvContract.Columns[gvContract.Columns.Count - 2].Visible = false;

        }
        if (objUA.Delete == 0)
        {
            
        }
        if (objUA.Approve == 0)
        {
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public void Load_RankList()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_RankList();

        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "Rank_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddlRank.SelectedIndex = 0;

        ddlRank1.DataSource = dt;
        ddlRank1.DataTextField = "Rank_Name";
        ddlRank1.DataValueField = "ID";
        ddlRank1.DataBind();
        ddlRank1.Items.Insert(0, new ListItem("-SELECT-", "0"));
        ddlRank1.SelectedIndex = 0;
     }
    protected void Load_SeniorityGrid(int RankId)
    {
        DataTable dt = objBLLCrewAdmin.Get_RankSeniorityList(RankId);
        gvSenority.DataSource = dt;
        gvSenority.DataBind();
    }

    protected void gvSenority_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int RankId = UDFLib.ConvertToInteger(gvSenority.DataKeys[e.RowIndex].Value.ToString());
        bool CompanySeniority = bool.Parse(e.NewValues["CompanySeniorityConsidered"].ToString());
        bool RankSeniority = bool.Parse(e.NewValues["RankSeniorityConsidered"].ToString());
 
        int ID = objBLLCrewAdmin.Ins_Update_RankSeniority(RankId, CompanySeniority, RankSeniority, GetSessionUserID());
        if (ID > 0)
        {
            gvSenority.EditIndex = -1;
            Load_SeniorityGrid(0);
        }
        else
        {
            string js = "Already exsist!";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js + "');", true);
        }
    }
    protected void gvSenority_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvSenority.EditIndex = e.NewEditIndex;
            Load_SeniorityGrid(0);
        }
        catch
        {
        }
    }
    protected void gvSenority_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvSenority.EditIndex = -1;
            Load_SeniorityGrid(0);
        }
        catch
        {
        }


    }
    public void RankSelected(object sender, CommandEventArgs e)
    {
        string[] cmdargs = e.CommandArgument.ToString().Split(',');

        RankID = int.Parse(cmdargs[0].ToString());       
        string RankName = cmdargs[1].ToString();
        int CompanySeniorityConsidered = cmdargs[2].ToString() == "true" ? 1 : 0;
        int RankSeniorityConsidered = cmdargs[3].ToString() == "true" ? 1 : 0;
        int RowIndex = int.Parse(cmdargs[4].ToString());

        for ( int i= 0 ; i < gvSenority.Rows.Count ; i++ )
        {
            if ( i == RowIndex )
                gvSenority.Rows[i].BackColor = System.Drawing.Color.Yellow;
            else
                gvSenority.Rows[i].BackColor = System.Drawing.Color.White;
        }
 
        ViewState["RankID"] = RankID.ToString();
        lblMsg.Text = "";
        lblMsg1.Text = "";
        lblSelectedRank.Text = RankName;
        lblSelectedRank1.Text = RankName;
        if (CompanySeniorityConsidered == 1)
        {
            btnAddCompanySeniority.Visible = true;
            gvCompanySeniority.Visible = true;
            btnUpdateCompanySeniorityRates.Visible = true;
            btnCancelCompanySeniorityRates.Visible = true;
            Load_CompanySeniorityRates(RankID);
        }
        else
        {
            btnAddCompanySeniority.Visible = false;
            gvCompanySeniority.Visible = false;
            btnUpdateCompanySeniorityRates.Visible = false;
                btnCancelCompanySeniorityRates.Visible = false;
        }

        if (RankSeniorityConsidered == 1)
        {
            gvNewSeniority.Visible = true;
            Load_RankSeniorityRates(RankID);
            btnSave.Visible = true;
            btnCancel.Visible = true;
        }
        else
        {
            gvNewSeniority.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
        }

        ddlRank.SelectedIndex = 0;
        ddlRank1.SelectedIndex = 0;
        if (RankID > 0)
        {
            btnSave.Enabled = true;
            btnUpdateCompanySeniorityRates.Enabled = true;
            btnAddCompanySeniority.Enabled = true;
        }
        else
        {
            btnSave.Enabled = false;
            btnUpdateCompanySeniorityRates.Enabled = false;
            btnAddCompanySeniority.Enabled = false;
        }
    }
    public void Load_RankSeniorityRates(int RankID)
    {
        DataTable dt = BLL_PortageBill.Get_Seniority_Rates(RankID, GetSessionUserID());

        gvNewSeniority.DataSource = dt;
        gvNewSeniority.DataBind();
    }
    public void Load_CompanySeniorityRates(int RankID)
    {
        DataTable dt = BLL_PortageBill.Get_CompanySeniority_Rates(RankID);

        gvCompanySeniority.DataSource = dt;
        gvCompanySeniority.DataBind();
    }
    protected void ddlRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsg1.Text = "";
        Load_RankSeniorityRates(UDFLib.ConvertToInteger(ddlRank.SelectedValue));
    }
    protected void ddlRank1_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsg1.Text = "";
        Load_CompanySeniorityRates(UDFLib.ConvertToInteger(ddlRank1.SelectedValue));
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ViewState["RankID"] != null)
            RankID = int.Parse(ViewState["RankID"].ToString());
        if (RankID > 0)
        {
            DataTable dtRates = new DataTable();
            dtRates.Columns.Add("PID", typeof(int));
            dtRates.Columns.Add("Value", typeof(double));

            foreach (GridViewRow gr in gvNewSeniority.Rows)
            {
                double amt = 0;
                int PID = 0;

                int.TryParse(((Label)gr.Cells[0].FindControl("lblSeniorityYear")).Text, out PID);
                double.TryParse(((TextBox)gr.Cells[1].FindControl("txtAmount")).Text, out amt);

                dtRates.Rows.Add(PID, amt);
            }

            int Res = BLL_PortageBill.Update_Seniority_Rates(dtRates, RankID, GetSessionUserID());

            if (Res == 1)
                lblMsg.Text = "Rates Updated !!";
            else
                lblMsg.Text = "Unable to Update !!";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        ddlRank.SelectedIndex = 0;
        Load_RankSeniorityRates(RankID);
    }

    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        if (ViewState["RankID"] != null)
            RankID = int.Parse(ViewState["RankID"].ToString());
        if (RankID > 0)
        {
            int CompanySeniorityYear = int.Parse(txtCompanySeniorityYear.Text.ToString());
            decimal CompanySeniorityAmount = decimal.Parse(txtCompanySeniorityAmount.Text.ToString());
            int Res = BLL_PortageBill.Save_Company_Seniority_Rates(RankID, CompanySeniorityYear, CompanySeniorityAmount, GetSessionUserID());

            if (Res == 1)
                lblMsg1.Text = "Rates Updated !!";
            else
                lblMsg1.Text = "Unable to Update !!";

            string msgdivResponseShow = string.Format("hideModal('dvAddNewCompanySeniority');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            Load_CompanySeniorityRates(RankID);
        }
    }
    protected void btnAddCompanySeniority_Click(object sender, EventArgs e)
    {
        txtCompanySeniorityAmount.Text = "";
        txtCompanySeniorityYear.Text = "";
        string msgdivResponseShow = string.Format("showModal('dvAddNewCompanySeniority',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
    }
    protected void btnCancelCompanySeniorityRates_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        ddlRank1.SelectedIndex = 0;
        Load_CompanySeniorityRates(RankID);
    }

    protected void btnUpdateCompanySeniorityRates_Click(object sender, EventArgs e)
    {
        if (ViewState["RankID"] != null)
            RankID = int.Parse(ViewState["RankID"].ToString());
        if (RankID > 0)
        {
            DataTable dtRates = new DataTable();
            dtRates.Columns.Add("PID", typeof(int));
            dtRates.Columns.Add("Value", typeof(double));

            foreach (GridViewRow gr in gvCompanySeniority.Rows)
            {
                double amt = 0;
                int PID = 0;

                int.TryParse(((Label)gr.Cells[0].FindControl("lblSeniorityYear")).Text, out PID);
                double.TryParse(((TextBox)gr.Cells[1].FindControl("txtAmount")).Text, out amt);

                dtRates.Rows.Add(PID, amt);
            }

            int Res = BLL_PortageBill.Update_Company_Seniority_Rates(dtRates, RankID, GetSessionUserID());

            if (Res == 1)
                lblMsg1.Text = "Rates Updated !!";
            else
                lblMsg1.Text = "Unable to Update !!";
        }
    }
    protected void btnAddCompanySeniority_Click1(object sender, EventArgs e)
    {
        txtCompanySeniorityAmount.Text = "";
        txtCompanySeniorityYear.Text = "";
        string msgdivResponseShow = string.Format("showModal('dvAddNewCompanySeniority',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
    }
}