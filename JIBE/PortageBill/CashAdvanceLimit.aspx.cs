using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PortageBill;
using System.Data;
using SMS.Business.Crew;

public partial class PortageBill_CashAdvanceLimit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCashLimit();
            LoadRankCategory();
        }
    }
    private void LoadRankCategory()
    {
        
        BLL_PB_Admin ObjBLLPBAdmin = new BLL_PB_Admin();
        DataTable dt = ObjBLLPBAdmin.GetRankCashAdvanceLimit();
        ddlRnkCat.DataSource = dt;
        if (dt.Rows.Count > 0)
        {
            ddlRnkCat.DataTextField = Convert.ToString(dt.Columns["Category_Name"]);
            ddlRnkCat.DataValueField = Convert.ToString(dt.Columns["ID"]);
            ddlRnkCat.DataBind();
            ddlRnkCat.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        else
        {
            ddlRnkCat.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlRnkCat.SelectedIndex = 0;
        }
        
    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateInput(ddlRnkCat.SelectedValue, txtPercent.Text) == true)
        {
            SaveCashLimit();
            LoadRankCategory();
            ClearInput();
            
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    private void SaveCashLimit()
    {
        BLL_PB_Admin ObjBLLPBAdmin = new BLL_PB_Admin();
        int ID=0 ;
        int RankCatID=Convert.ToInt32(ddlRnkCat.SelectedValue);
        int Percent=Convert.ToInt32(txtPercent.Text ==""?"0":txtPercent.Text.Trim());
        ObjBLLPBAdmin.INS_CASH_ADV_LIMIT(0, RankCatID, Percent, (GetSessionUserID()), "A");
        
    }
    private void LoadCashLimit()
    {
        BLL_PB_Admin ObjBLLPBAdmin = new BLL_PB_Admin();
        DataTable dtCashLimit=ObjBLLPBAdmin.GetCashAdvanceLimit();
        GridView_CashLimit.DataSource = dtCashLimit;
        GridView_CashLimit.DataBind();
        
    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        if (ValidateInput(ddlRnkCat.SelectedValue, txtPercent.Text) == true)
        {
            SaveCashLimit();
            
            string hidemodal = String.Format("hideModal('dvAddNew')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
            LoadRankCategory();
            LoadCashLimit();
            ClearInput();
        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        string hidemodal = String.Format("hideModal('dvAddNew')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        LoadRankCategory();
        
    }
    protected void GridView_CashLimit_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)GridView_CashLimit.Rows[e.RowIndex];
        int ID = UDFLib.ConvertToInteger(GridView_CashLimit.DataKeys[e.RowIndex].Values[0].ToString());
        BLL_PB_Admin ObjBLLPBAdmin = new BLL_PB_Admin();
        int result = ObjBLLPBAdmin.INS_CASH_ADV_LIMIT(ID, null,null,GetSessionUserID(), "D");
        LoadCashLimit();
        LoadRankCategory();

    }
    protected void GridView_CashLimit_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        BLL_PB_Admin ObjBLLPBAdmin = new BLL_PB_Admin();
        try
        {
            int ID = UDFLib.ConvertToInteger(GridView_CashLimit.DataKeys[e.RowIndex].Values[0].ToString());
            GridViewRow row = (GridViewRow)GridView_CashLimit.Rows[e.RowIndex];

            if (ValidateInput(((DropDownList)row.FindControl("ddlRankCat")).SelectedValue, ((TextBox)row.FindControl("txtPercentage")).Text) == true)
            {
                int result = ObjBLLPBAdmin.INS_CASH_ADV_LIMIT(ID, Convert.ToInt32(((DropDownList)row.FindControl("ddlRankCat")).SelectedValue), 
                        Convert.ToInt32(((TextBox)row.FindControl("txtPercentage")).Text),GetSessionUserID(),"U");
                GridView_CashLimit.EditIndex = -1;
                LoadCashLimit();
            }
        }
        catch
        {
        }
    }
    private void ClearInput()
    {
        txtPercent.Text = string.Empty;
       // ddlRnkCat.Items.Clear();
        //ddlRnkCat.SelectedIndex = -1;
    }
    private bool ValidateInput(string RankCategory,string Percent)
    {
        string js = "";
        bool Validate = true;
        if (RankCategory == "0")
        {
            js = "alert('Select Rank Category');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            Validate = false;
        }
        if (Percent == "")
        {
            js = "alert('Enter Percentage');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            Validate = false;
        }
        return Validate;

    }
    protected void GridView_CashLimit_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_CashLimit.EditIndex = e.NewEditIndex;
            GridViewRow row = GridView_CashLimit.Rows[e.NewEditIndex];
            LoadCashLimit();
        }
        catch
        {
        }
    }
    protected void GridView_CashLimit_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_CashLimit.EditIndex = -1;
            LoadCashLimit();
        }
        catch
        {
        }
    }
    protected void GridView_CashLimit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        BLL_Crew_Admin ObjBLL_Crew_Admin = new BLL_Crew_Admin();
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ID = UDFLib.ConvertToInteger(GridView_CashLimit.DataKeys[e.Row.RowIndex].Value.ToString());
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddList = (DropDownList)e.Row.FindControl("ddlRankCat");
                    
                    DataTable dt = ObjBLL_Crew_Admin.Get_RankCategories();
                    ddList.DataSource = dt;
                    ddList.DataTextField = Convert.ToString(dt.Columns["Category_Name"]);
                    ddList.DataValueField = Convert.ToString(dt.Columns["ID"]);
                    ddList.DataBind();
                    ddList.Items.Insert(0, new ListItem("--Select--", "0"));
                    DataRowView drType = e.Row.DataItem as DataRowView;
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    ddList.SelectedValue = dr["Rank_Category_ID"].ToString();
                    
                }
            }
        }
        catch
        {
        }
    }
}