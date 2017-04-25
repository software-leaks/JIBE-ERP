using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.Crew;
using System.Data;

public partial class Crew_Libraries_CrewMatrixConfiguration : System.Web.UI.Page
{

    #region BusinessLayerAccess
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    UserAccess objUA = new UserAccess();
    public bool trAddNewTanker = true, trAddNewSTWC =true,editDeleteAccess=true;
    #endregion

    #region PageLoadEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                UserAccessValidation();
                BindData();
                BindRank();
                listGroup.SelectedIndex = 0;
                listGroup_SelectedIndexChanged(sender, e);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    #endregion
    #region AccessValidation & UserDetails
    private int GetSessionUserID()
    {
        try
        {
            if (Session["USERID"] != null)
                return int.Parse(Session["USERID"].ToString());
            else
                return 0;
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");
        if (objUA.Admin == 0)
        {
            trAddNewSTWC = trAddNewTanker = false;
            editDeleteAccess = false;
            rt2.Visible = false;
        }

        if (objUA.Add == 1)
        {
            tdSave.Visible = true;
            btnSave.Visible = true;
            btnGroupSave.Visible = true;
        }
        else
        {
            tdSave.Visible = false;
            btnSave.Visible = false;
            btnGroupSave.Visible = false;
        }
    }
    #endregion
    #region BindData

    public void BindData()
    {
        try
        {
            DataSet ds = objCrewAdmin.Get_Crew_Matrix_Configuration();
            if (ds != null)
            {
                rptTanker.DataSource = ds.Tables[0];
                rptTanker.DataBind();

                rptSTCW.DataSource = ds.Tables[1];
                rptSTCW.DataBind();

                txtDate.Text = ds.Tables[2].Rows[0]["DefaultValue"].ToString() == "" ? "0" : ds.Tables[2].Rows[0]["DefaultValue"].ToString();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    #endregion


    #region ButtonEvents

    protected void btnSaveMatrix_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (RepeaterItem item in rptTanker.Items)
            {
                CheckBox chkTanker = (CheckBox)item.FindControl("chkTanker");
                if (chkTanker.Checked)
                    objCrewAdmin.UpdateCrewMatrixConfiguration("Update", "Tanker Certification", chkTanker.Text, 1, GetSessionUserID());
                else
                    objCrewAdmin.UpdateCrewMatrixConfiguration("Update", "Tanker Certification", chkTanker.Text, 0, GetSessionUserID());
            }
            foreach (RepeaterItem item in rptSTCW.Items)
            {
                CheckBox chkTanker = (CheckBox)item.FindControl("chkTanker");
                if (chkTanker.Checked)
                    objCrewAdmin.UpdateCrewMatrixConfiguration("Update", "STCW", chkTanker.Text, 1, GetSessionUserID());
                else
                    objCrewAdmin.UpdateCrewMatrixConfiguration("Update", "STCW", chkTanker.Text, 0, GetSessionUserID());
            }

            objCrewAdmin.UpdateCrewMatrixConfiguration("", "DateRestriction", null, UDFLib.ConvertToInteger(txtDate.Text), GetSessionUserID());
            BindData();
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser1", "alert('" + UDFLib.GetException("SuccessMessage/SaveMessage") + "');", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnSaveTank_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(hdnTankerName.Value.Trim()))
            {
                int i = objCrewAdmin.InsertCrewMatrixConfiguration("Tanker Certification", hdnTankerName.Value.Trim(), hdnOrignalTankerName.Value.Trim(), GetSessionUserID());
                BindData();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void btnSaveNewTank_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(hdnTankerName.Value.Trim()))
            {
                int i = objCrewAdmin.InsertCrewMatrixConfiguration("Tanker Certification", hdnTankerName.Value.Trim(), hdnTankerName.Value.Trim(), GetSessionUserID());
                i = objCrewAdmin.UpdateCrewMatrixConfiguration("Update", "Tanker Certification", hdnTankerName.Value.Trim(), 0, GetSessionUserID());
                BindData();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void btnSaveNewSTWC_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(hdnTankerName.Value.Trim()))
            {
                int i = objCrewAdmin.InsertCrewMatrixConfiguration("STCW", hdnTankerName.Value.Trim(), hdnTankerName.Value.Trim(), GetSessionUserID());
                i = objCrewAdmin.UpdateCrewMatrixConfiguration("Update", "STCW", hdnTankerName.Value.Trim(), 0, GetSessionUserID());
                BindData();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void btnSaveSTCW_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(hdnTankerName.Value.Trim()))
            {
                int i = objCrewAdmin.InsertCrewMatrixConfiguration("STCW", hdnTankerName.Value.Trim(), hdnOrignalTankerName.Value.Trim(), GetSessionUserID());
                BindData();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    #endregion
    protected void btnDeleteTank_Click(object sender, EventArgs e)
    {
        try
        {
            objCrewAdmin.UpdateCrewMatrixConfiguration("Delete", "Tanker Certification", Convert.ToString(hdnOrignalTankerName.Value), 1, GetSessionUserID());
            BindData();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser6", "alert('" + UDFLib.GetException("SuccessMessage/DeleteMessage") + "');", true);

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnDeleteSTCW_Click(object sender, EventArgs e)
    {
        try
        {
            objCrewAdmin.UpdateCrewMatrixConfiguration("Delete", "STCW", Convert.ToString(hdnOrignalTankerName.Value), 1, GetSessionUserID());
            BindData();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser6", "alert('" + UDFLib.GetException("SuccessMessage/DeleteMessage") + "');", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public void BindRank()
    {
        try
        {
            BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
            DataTable dt = objCrewAdmin.Get_RankList();

            chkRank.DataSource = dt;
            chkRank.DataTextField = "Rank_Name";
            chkRank.DataValueField = "ID";
            chkRank.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int count = 0;
        string rank = string.Empty;
        string urank = string.Empty;
        for (int i = 0; i < chkRank.Items.Count; i++)
        {

            if (chkRank.Items[i].Selected)
            {
                rank += chkRank.Items[i].Value.ToString() + ",";
                count++;
            }
            else
            {
                urank += chkRank.Items[i].Value.ToString() + ",";

            }

        }

        if (count != 0)
        {
            string[] Ranks = rank.TrimEnd(',').Split(',');
            string[] URanks = urank.TrimEnd(',').Split(',');

            for (int i = 0; i < Ranks.Length; i++)
            {
                objCrewAdmin.InsertCrewMatrixGroup(UDFLib.ConvertToInteger(listGroup.SelectedValue), UDFLib.ConvertToInteger(Ranks[i]), GetSessionUserID(), 1);
            }
            for (int i = 0; i < URanks.Length; i++)
            {
                objCrewAdmin.InsertCrewMatrixGroup(UDFLib.ConvertToInteger(listGroup.SelectedValue), UDFLib.ConvertToInteger(URanks[i]), GetSessionUserID(), 0);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "HideRanks();alert('" + UDFLib.GetException("SuccessMessage/SaveMessage") + "');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser6", "HideRanks();alert('Please select ranks');", true);
        }
    }
    protected void listGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindRank();
            DataSet ds = objCrewAdmin.GetCrewMatrixGroup(UDFLib.ConvertToInteger(listGroup.SelectedValue));

            for (int i = 0; i < chkRank.Items.Count; i++)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (chkRank.Items[i].Value == dr["rankid"].ToString())
                    {
                        chkRank.Items[i].Selected = true;
                    }

                }
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    if (chkRank.Items[i].Value == dr["rankid"].ToString())
                    {
                        chkRank.Items[i].Enabled = false;
                        //chkRank.Items[i].Attributes.Add("style", "Display:none;");
                    }

                }

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideRow", "HideRanks();", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

}