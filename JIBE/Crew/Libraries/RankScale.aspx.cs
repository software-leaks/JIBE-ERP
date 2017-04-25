using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;

public partial class Crew_Libraries_RankScale : System.Web.UI.Page
{
    BLL_Crew_Admin objAdmin = new BLL_Crew_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_RankList();
            Load_RankScaleList(0);
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
            lnkAddNewGroup.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            gvRankScale.Columns[gvRankScale.Columns.Count - 2].Visible = false;

        }
        if (objUA.Delete == 0)
        {
            gvRankScale.Columns[gvRankScale.Columns.Count - 1].Visible = false;
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
        if (ddlRank.Items.Count == 0)
        {
            BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
            DataTable dt = objCrewAdmin.Get_RankList();

            ddlRank.DataSource = dt;
            ddlRank.DataTextField = "Rank_Short_Name";
            ddlRank.DataValueField = "ID";
            ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("-SELECT-", "0"));

            ddlRank1.DataSource = dt;
            ddlRank1.DataTextField = "Rank_Short_Name";
            ddlRank1.DataValueField = "ID";
            ddlRank1.DataBind();
            ddlRank1.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
    }
    protected void Load_RankScaleList(int RankId)
    {
        DataTable dt = objAdmin.Get_RankScaleList(RankId);
        gvRankScale.DataSource = dt;
        gvRankScale.DataBind();
    }
    protected void gvRankScale_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(gvRankScale.DataKeys[e.RowIndex].Value.ToString());

        objAdmin.DELETE_RankScale(ID, GetSessionUserID());
        Load_RankScaleList(0);
    }
    protected void gvRankScale_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(gvRankScale.DataKeys[e.RowIndex].Value.ToString());
        int RankId = 0;
        string RankScaleName = e.NewValues["RankScaleName"].ToString();

        ID = objAdmin.Ins_Update_RankScale(ID, RankId, RankScaleName, GetSessionUserID());
        if (ID > 0)
        {
            gvRankScale.EditIndex = -1;
            Load_RankScaleList(0);
        }
        else
        {
            string js = "Rank Scale Name already exsist!";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js + "');", true);
        }
    }
    protected void gvRankScale_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvRankScale.EditIndex = e.NewEditIndex;
            Load_RankScaleList(0);

        }
        catch
        {
        }
    }
    protected void gvRankScale_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvRankScale.EditIndex = -1;
            Load_RankScaleList(0);

        }
        catch
        {
        }


    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (int.Parse(ddlRank.SelectedValue) == 0)
        {
            lblMsg.Text = "Select Rank to add Rank Scale";
            lblMsg.Visible = true;
            string js = String.Format("showModal('dvAddNewRankScale',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "js", js, true);
        }
        else
        {
            lblMsg.Text = "";
            lblMsg.Visible = false;
            int ID = objAdmin.Ins_Update_RankScale(0, int.Parse(ddlRank.SelectedValue), txtRankScaleName.Text.Trim(), GetSessionUserID());
            if (ID > 0)
            {
                txtRankScaleName.Text = "";
                ddlRank.SelectedIndex = 0;
                Load_RankScaleList(0);
                string js = String.Format("showModal('dvAddNewRankScale',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "js", js, true);
            }
            else
            {
                string js = "Rank Scale Name already exsist!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js + "');", true);
            }
        }
    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        if (int.Parse(ddlRank.SelectedValue) == 0)
        {
            lblMsg.Text = "Select Rank to add Rank Scale";
            lblMsg.Visible = true;
            string js = String.Format("showModal('dvAddNewRankScale',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "js", js, true);
        }
        else
        {
            lblMsg.Text = "";
            lblMsg.Visible = false;
            int ID = objAdmin.Ins_Update_RankScale(0, int.Parse(ddlRank.SelectedValue), txtRankScaleName.Text.Trim(), GetSessionUserID());
            if (ID > 0)
            {
                txtRankScaleName.Text = "";
                ddlRank.SelectedIndex = 0;
                Load_RankScaleList(0);

                string js = "closeDiv('dvAddNewRankScale');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            }
            else
            {
                string js = "Rank Scale Name already exsist!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js + "');", true);
            }
        }
    }
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {

        ddlRank1.SelectedIndex = 0;
        Load_RankScaleList(0);

    }

    protected void ddlRank1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_RankScaleList(int.Parse(ddlRank1.SelectedValue));
    }
}