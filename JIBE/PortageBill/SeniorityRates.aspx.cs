using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Business.PortageBill;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Properties;

public partial class PortageBill_SeniorityRates : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Enabled = false;
            btnUpdateCompanySeniorityRates.Enabled = false;
            btnAddCompanySeniority.Enabled = false;
            Load_RankList();
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

        string PageURL = UDFLib.GetPageURL(Request.Path);

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnSave.Visible = false;            
        }
        if (objUA.Edit == 0)
        {
            btnSave.Visible = false;
        }
        if (objUA.Delete == 0)
        {
        }
        if (objUA.Approve == 0)
        {
        }

        //-- MANNING OFFICE LOGIN --
        if (Session["UTYPE"].ToString() == "MANNING AGENT")
        {

        }
        //-- VESSEL OWNER -- //
        else if (Session["UTYPE"].ToString() == "VESSEL OWNER")
        {

        }
        else//--- CREW TEAM LOGIN--
        {
        }
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

        lstRank.DataSource = dt;
        lstRank.DataTextField = "Rank_Name";
        lstRank.DataValueField = "ID";
        lstRank.DataBind();
        lstRank.Items.Insert(0, new ListItem("-SELECT-", "0"));
        lstRank.SelectedIndex = 0;
    }
    protected void lstRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsg1.Text = "";
        lblSelectedRank.Text = lstRank.SelectedItem.Text;
        lblSelectedRank1.Text = lstRank.SelectedItem.Text;
        Load_RankSeniorityRates(UDFLib.ConvertToInteger(lstRank.SelectedValue));
        Load_CompanySeniorityRates(UDFLib.ConvertToInteger(lstRank.SelectedValue));
        
        ddlRank.SelectedIndex = 0;
        ddlRank1.SelectedIndex = 0;
        if (UDFLib.ConvertToInteger(lstRank.SelectedValue) > 0)
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
        if (UDFLib.ConvertToInteger(lstRank.SelectedValue) > 0)
        {
            DataTable dtRates = new DataTable();
            dtRates.Columns.Add("PID", typeof(int));
            dtRates.Columns.Add("Value", typeof(int));

            foreach (GridViewRow gr in gvNewSeniority.Rows)
            {
                double amt = 0;
                int PID = 0;

                int.TryParse(((Label)gr.Cells[0].FindControl("lblSeniorityYear")).Text, out PID);
                double.TryParse(((TextBox)gr.Cells[1].FindControl("txtAmount")).Text, out amt);

                dtRates.Rows.Add(PID, amt);
            }

            int Res = BLL_PortageBill.Update_Seniority_Rates(dtRates, UDFLib.ConvertToInteger(lstRank.SelectedValue), GetSessionUserID());

            if(Res==1)
                lblMsg.Text = "Rates Updated !!";
            else
                lblMsg.Text = "Unable to Update !!";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        ddlRank.SelectedIndex = 0;
        Load_RankSeniorityRates(UDFLib.ConvertToInteger(lstRank.SelectedValue));
    }

    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        if (UDFLib.ConvertToInteger(lstRank.SelectedValue) > 0)
        {
            int CompanySeniorityYear = int.Parse(txtCompanySeniorityYear.Text.ToString());
            decimal CompanySeniorityAmount = decimal.Parse(txtCompanySeniorityAmount.Text.ToString());
            int Res = BLL_PortageBill.Save_Company_Seniority_Rates(UDFLib.ConvertToInteger(lstRank.SelectedValue), CompanySeniorityYear, CompanySeniorityAmount, GetSessionUserID());

            if (Res == 1)
                lblMsg1.Text = "Rates Updated !!";
            else
                lblMsg1.Text = "Unable to Update !!";

            string msgdivResponseShow = string.Format("hideModal('dvAddNewCompanySeniority');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            pnlCompanySeniority.Update();

            Load_CompanySeniorityRates(UDFLib.ConvertToInteger(lstRank.SelectedValue));
        }
    }
    protected void btnAddCompanySeniority_Click(object sender, EventArgs e)
    {
        txtCompanySeniorityAmount.Text = "";
        txtCompanySeniorityYear.Text = "";
        string msgdivResponseShow = string.Format("showModal('dvAddNewCompanySeniority',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
        pnlCompanySeniority.Update();
    }
    protected void btnCancelCompanySeniorityRates_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        ddlRank1.SelectedIndex = 0;
        Load_CompanySeniorityRates(UDFLib.ConvertToInteger(lstRank.SelectedValue));
    }

    protected void btnUpdateCompanySeniorityRates_Click(object sender, EventArgs e)
    {
        if (UDFLib.ConvertToInteger(lstRank.SelectedValue) > 0)
        {
            DataTable dtRates = new DataTable();
            dtRates.Columns.Add("PID", typeof(int));
            dtRates.Columns.Add("Value", typeof(int));

            foreach (GridViewRow gr in gvCompanySeniority.Rows)
            {
                double amt = 0;
                int PID = 0;

                int.TryParse(((Label)gr.Cells[0].FindControl("lblSeniorityYear")).Text, out PID);
                double.TryParse(((TextBox)gr.Cells[1].FindControl("txtAmount")).Text, out amt);

                dtRates.Rows.Add(PID, amt);
            }

            int Res = BLL_PortageBill.Update_Company_Seniority_Rates(dtRates, UDFLib.ConvertToInteger(lstRank.SelectedValue), GetSessionUserID());

            if (Res == 1)
                lblMsg1.Text = "Rates Updated !!";
            else
                lblMsg1.Text = "Unable to Update !!";
        }
    }
}