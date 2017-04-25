using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Properties;
using SMS.Business.JRA;

public partial class JRA_Libraries_RatingType : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadRisk_Type();
            SearchRiskType();
        }

    }
    #endregion

    #region General Function
    private void LoadRisk_Type()
    {
        JRA_Lib JRALib_Data = new JRA_Lib();
        DataTable dt=BLL_JRA_Work_Category.JRA_GET_RISK_TYPES(JRALib_Data);
        ddlRiskType.DataSource = dt;
        ddlRiskType.DataTextField = Convert.ToString(dt.Columns["Type_Display_Text"]);
        ddlRiskType.DataValueField = Convert.ToString(dt.Columns["Type_ID"]);
        ddlRiskType.DataBind();
        ddlRiskType.Items.Insert(0, new ListItem("-Select-", "0"));
        ViewState["vsRiskType"] = dt;
        

    }
    private void SearchRiskType()
    {
        JRA_Lib JRALib_Data = new JRA_Lib();
        DataTable dt = BLL_JRA_Work_Category.JRA_GET_RATINGS_SEARCH(JRALib_Data);
        GridView_Ratings.DataSource = dt;
        GridView_Ratings.DataBind();
    }
    private void SaveRisk_Type(string DB_Mode)
    {
        JRA_Lib JRALib_Data = new JRA_Lib();

        JRALib_Data.RiskType = ddlRiskType.SelectedValue;
        JRALib_Data.RatingValue = Convert.ToInt32(txtRatingVal.Text);
        JRALib_Data.UserID = Convert.ToInt32(Session["USERID"]);
        JRALib_Data.DB_Mode = DB_Mode;

        int result = BLL_JRA_Work_Category.JRA_INS_RatingType(JRALib_Data);

    }
    private void ClearRatings()
    {
        txtRatingVal.Text = string.Empty;
        ddlRiskType.SelectedIndex = 0;
    }
    private bool ValidateControls()
    {
        bool Validate = true;
        string js = "";
        if (ddlRiskType.SelectedIndex == 0)
        {
            js = "alert('Select Risk Type');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            Validate = false;
        }
        if(txtRatingVal.Text.Trim()=="")
        {
            js = "alert('Enter Rating Value');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            Validate = false;
        }
        return Validate;
    }

    #endregion

    #region Control Events

    protected void ddlDisplayText_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlDisplayText = (DropDownList)sender;
        GridViewRow GridRow = (GridViewRow)ddlDisplayText.Parent.Parent;
        DataTable dv = (DataTable)ViewState["vsRiskType"];
        var vType_Color = from p in dv.AsEnumerable()
                          where p.Field<Int32>("Type_ID") == Convert.ToInt32(ddlDisplayText.SelectedValue)
                          select new { Type_Color = p.Field<string>("Type_Color") };
        Label lblColor = ((Label)GridRow.FindControl("lblColor"));
        foreach (var c in vType_Color)
        {
            lblColor.Text = Convert.ToString(c.Type_Color);
        }
        HiddenField hdnSelectedValue =((HiddenField)GridRow.FindControl("hdnSelectedValue"));
        //hdnSelectedValue.Value = ddlDisplayText.Items.FindByText("Cards").Value;
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (ValidateControls() == true)
        {
            SaveRisk_Type("A");
            ClearRatings();
        }
    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        if (ValidateControls() == true)
        {
            SaveRisk_Type("A");
            string hidemodal = String.Format("hideModal('dvAddNewRiskType')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
            SearchRiskType();
        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        string hidemodal = String.Format("hideModal('dvAddNewRiskType')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        SearchRiskType();
    }

    #endregion

    #region Gridview Events
    protected void GridView_Ratings_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        JRA_Lib JRALib_Data = new JRA_Lib();
        int ID = UDFLib.ConvertToInteger(GridView_Ratings.DataKeys[e.RowIndex].Value.ToString());
        JRALib_Data.Rating_ID = ID;
        JRALib_Data.UserID = Convert.ToInt32(Session["USERID"]);
        JRALib_Data.DB_Mode = "D";
        int result = BLL_JRA_Work_Category.JRA_INS_RatingType(JRALib_Data);
        SearchRiskType();

    }
    protected void GridView_Ratings_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        JRA_Lib JRALib_Data = new JRA_Lib();
        try
        {
            int ID = UDFLib.ConvertToInteger(GridView_Ratings.DataKeys[e.RowIndex].Values[0].ToString());
            GridViewRow row = (GridViewRow)GridView_Ratings.Rows[e.RowIndex];
            JRALib_Data.Rating_ID = ID;
            JRALib_Data.RiskType = ((DropDownList)row.FindControl("ddlDisplayText")).SelectedValue;
            JRALib_Data.RatingValue = Convert.ToInt32(((TextBox)row.FindControl("txtRisk")).Text);
            JRALib_Data.UserID = Convert.ToInt32(Session["USERID"]);
            JRALib_Data.DB_Mode = "U";
            int result = BLL_JRA_Work_Category.JRA_INS_RatingType(JRALib_Data);
            GridView_Ratings.EditIndex = -1;
            SearchRiskType();
        }
        catch
        {
        }
    }
    protected void GridView_Ratings_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Ratings.EditIndex = e.NewEditIndex;
            GridViewRow row = GridView_Ratings.Rows[e.NewEditIndex];
            SearchRiskType();
        }
        catch
        {
        }
    }
    protected void GridView_Ratings_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_Ratings.EditIndex = -1;
            SearchRiskType();
        }
        catch
        {
        }
    }
    protected void GridView_Ratings_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            JRA_Lib JRALib_Data = new JRA_Lib();
            DataTable dt = new DataTable();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ID = UDFLib.ConvertToInteger(GridView_Ratings.DataKeys[e.Row.RowIndex].Value.ToString());

                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddList = (DropDownList)e.Row.FindControl("ddlDisplayText");
                    dt= BLL_JRA_Work_Category.JRA_GET_RISK_TYPES(JRALib_Data);
                    ddList.DataSource = dt;
                    ddList.DataTextField = Convert.ToString(dt.Columns["Type_Display_Text"]);
                    ddList.DataValueField = Convert.ToString(dt.Columns["Type_ID"]);
                    ddList.DataBind();
                    ddList.Items.Insert(0, new ListItem("-Select-", "0"));
                    DataRowView dr = e.Row.DataItem as DataRowView;
                    //ddList.SelectedValue = dr["Risk_TYPE"].ToString();
                    ddList.Items.FindByValue(Convert.ToString(dr["Risk_Type"])).Selected = true;
                }
            }
        }
        catch
        {
        }
    }
    #endregion  

    
}