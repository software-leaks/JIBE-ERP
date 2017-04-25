using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Data;
using SMS.Properties;
using SMS.Business.JRA;
using System.Drawing;

public partial class JRA_Libraries_JRAType : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            txtDesc.Attributes.Add("maxlength", "2000");
            populatColor();
            Search_Type();
            ApplyDivColor(ddlColors);
            ddlColors.Enabled = false;
            tdcolor.Text = "";
            ucCustomPagerItems.PageSize = 10;
        }

    }
    #endregion

    #region General Functions
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void UserAccessValidation()
    {
        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            
            lnkAddNewType.Visible = false;
            //lnkAddNewGrade.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            GridView_Type.Columns[GridView_Type.Columns.Count - 2].Visible = false;
            //GridView_Grading.Columns[GridView_Grading.Columns.Count - 2].Visible = false;

        }
        if (objUA.Delete == 0)
        {
            GridView_Type.Columns[GridView_Type.Columns.Count - 1].Visible = false;
            //GridView_Grading.Columns[GridView_Grading.Columns.Count - 1].Visible = false;
        }
        if (objUA.Approve == 0)
        {
        }

    }
    protected void Search_Type()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        int SearchRows = 0;
        int TotalRows = 0;

        JRA_Lib JRALib_Data = new JRA_Lib();
        JRALib_Data.SearchText = txtfilter.Text.Trim();
        JRALib_Data.SearchType = ddlFiter.SelectedItem.Text == "--Select--" ? null : ddlFiter.SelectedItem.Text;
        JRALib_Data.PageNumber= ucCustomPagerItems.CurrentPageIndex;
        JRALib_Data.PageSize = ucCustomPagerItems.PageSize;
        DataTable dt = BLL_JRA_Work_Category.JRA_GET_TYPE_SEARCH(JRALib_Data);
        
        GridView_Type.DataSource = dt;
        GridView_Type.DataBind();

        if (dt.Rows.Count > 0)
        {
            SearchRows = Convert.ToInt32(dt.Rows[0]["SearchRows"]);
            TotalRows = Convert.ToInt32(dt.Rows[0]["TotalRows"]);
        }

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            rowcount = SearchRows;
            ucCustomPagerItems.CountTotalRec = Convert.ToString(TotalRows); 
            ucCustomPagerItems.BuildPager();
        }
    }
    private void SaveType(string DB_Mode)
    {
        JRA_Lib JRALib_Data = new JRA_Lib();
        JRALib_Data.Type_ID = 0;
        JRALib_Data.Type = ddlType.SelectedItem.Text;
        JRALib_Data.Type_Value = txtTypeValue.Text.Trim();
        JRALib_Data.Type_Display_Text = txtDisplayText.Text.Trim();
        JRALib_Data.Type_Description = txtDesc.Text.Trim();
        JRALib_Data.Type_Color = ddlColors.SelectedItem.Text;
        JRALib_Data.UserID = Convert.ToInt32(Session["USERID"]);
        JRALib_Data.DB_Mode = DB_Mode;
        int result = BLL_JRA_Work_Category.JRA_INS_TYPE(JRALib_Data);

    }

    protected void Load_TypeList()
    {
        JRA_Lib JRALib_Data = new JRA_Lib();
        JRALib_Data.Type_ID = 0;
        DataTable dt = BLL_JRA_Work_Category.JRA_GET_TYPE_LIST(JRALib_Data);
        GridView_Type.DataSource = dt;
        GridView_Type.DataBind();
    }
    private void ClearType()
    {
        ddlType.SelectedIndex = 0;
        txtDesc.Text = string.Empty;
        txtTypeValue.Text = string.Empty;
        txtDisplayText.Text = string.Empty;
        ddlColors.SelectedIndex = 0;
        ddlColors.BackColor = System.Drawing.Color.White;
        divColor.Style.Add("background-color", "White");
    }
    #endregion

    #region Gridview Events
    protected void GridView_Type_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        JRA_Lib JRALib_Data = new JRA_Lib();
        int ID = UDFLib.ConvertToInteger(GridView_Type.DataKeys[e.RowIndex].Value.ToString());
        JRALib_Data.Type_ID = ID;
        JRALib_Data.UserID = Convert.ToInt32(Session["USERID"]);
        JRALib_Data.DB_Mode = "D";
        int result = BLL_JRA_Work_Category.JRA_INS_TYPE(JRALib_Data);
        Search_Type();

    }
    protected void GridView_Type_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        JRA_Lib JRALib_Data = new JRA_Lib();
        try
        {

            int ID = UDFLib.ConvertToInteger(GridView_Type.DataKeys[e.RowIndex].Values[0].ToString());
            GridViewRow row = (GridViewRow)GridView_Type.Rows[e.RowIndex];
            JRALib_Data.Type_ID = ID;
            JRALib_Data.Type = ((DropDownList)row.FindControl("ddlType")).SelectedValue;
            JRALib_Data.Type_Value = ((TextBox)row.FindControl("txtType_Value")).Text;
            JRALib_Data.Type_Display_Text = ((TextBox)row.FindControl("txtType_Display_Text")).Text;
            JRALib_Data.Type_Description = ((TextBox)row.FindControl("txtType_Desc")).Text;
            JRALib_Data.Type_Color = ((DropDownList)row.FindControl("ddlGrdType_Color")).SelectedItem.Text;
            JRALib_Data.UserID = Convert.ToInt32(Session["USERID"]);
            JRALib_Data.DB_Mode = "U";
            if (ValidateType(JRALib_Data.Type, JRALib_Data.Type_Value, JRALib_Data.Type_Description, JRALib_Data.Type_Display_Text, JRALib_Data.Type_Color) == true)
            {
                int result = BLL_JRA_Work_Category.JRA_INS_TYPE(JRALib_Data);
                GridView_Type.EditIndex = -1;
                Search_Type();
            }
        }
        catch
        {
        }
    }
    protected void GridView_Type_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Type.EditIndex = e.NewEditIndex;
            GridViewRow row = GridView_Type.Rows[e.NewEditIndex];
            Search_Type();
        }
        catch
        {
        }
    }
    protected void GridView_Type_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_Type.EditIndex = -1;
            Search_Type();
        }
        catch
        {
        }
    }
    protected void GridView_Type_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ID = UDFLib.ConvertToInteger(GridView_Type.DataKeys[e.Row.RowIndex].Value.ToString());
                Label lblType_Color = (Label)(e.Row.FindControl("lblType_Color"));
                //lblType_Color.Text = lblType_Color.Text == "-Select-" ? "" : lblType_Color.Text;
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddList = (DropDownList)e.Row.FindControl("ddlGrdType_Color");
                    DropDownList ddlType = (DropDownList)e.Row.FindControl("ddlType");
                    ddList.DataSource = ColorList();
                    ddList.DataBind();
                    ddList.Items.Insert(0, new ListItem("--Select--", "0"));
                    DataRowView drType = e.Row.DataItem as DataRowView;

                    if (ddlType.Items.FindByText(Convert.ToString(drType["Type"])) != null)
                    {
                        ddlType.Items.FindByText(Convert.ToString(drType["Type"])).Selected = true;
                    }

                    DataRowView dr = e.Row.DataItem as DataRowView;
                    //ddList.SelectedItem.Text = dr["Type_Color"].ToString();
                    ddList.SelectedValue = dr["Type_Color"].ToString();
                    ApplyDivColor(ddList);
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region Control Events
    protected void txtfilter_TextChanged(object sender, EventArgs e)
    {
        Search_Type();
    }
    protected void btnSaveType_Click(object sender, EventArgs e)
    {
        if (ValidateType(ddlType.SelectedValue, txtTypeValue.Text, txtDesc.Text, txtDisplayText.Text, ddlColors.SelectedValue) == true)
        {
            SaveType("A");
            ClearType();
            Search_Type();
        }
    }
    protected void btnSaveAndCloseType_Click(object sender, EventArgs e)
    {
        if (ValidateType(ddlType.SelectedValue, txtTypeValue.Text, txtDesc.Text, txtDisplayText.Text, ddlColors.SelectedValue) == true)
        {
            SaveType("A");
            ClearType();
            string hidemodal = String.Format("hideModal('dvAddNewType')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
            Search_Type();
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        string hidemodal = String.Format("hideModal('dvAddNewType')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        Search_Type();
    }
    protected void ddlColors_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlColors.BackColor = Color.FromName(ddlColors.SelectedItem.Text);
        ApplyDivColor(ddlColors);
        ddlColors.Items.FindByValue(ddlColors.SelectedValue).Selected =
            true;
        divColor.Attributes.Add("style", "background:" +
            ddlColors.SelectedItem.Value + ";width:30px;height:21px;");
    }
    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        Search_Type();
    }
    #endregion

    #region Colour Dropdown

    private bool ValidateType(string Type, string TypeValue, string Desc, string DisplayText, string Color)
    {
        string js = "";
        bool Validate = true;

        if (Type == "0")
        {
            js = "alert('Select Type');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            Validate = false;
        }
        if (Type == "1" || Type == "2")
        {
            if (TypeValue.Trim() == "")
            {
                js = "alert('Enter Type Value');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                Validate = false;
            }
        }
        if (Desc.Trim() == "")
        {
            js = "alert('Enter Description');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            Validate = false;
        }
        if (DisplayText.Trim() == "")
        {
            js = "alert('Enter Display Text');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            Validate = false;
        }
        if (Type == "3")
        {
            if (Color == "--Select--" || Color == "0")
            {
                js = "alert('Select Color');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                Validate = false;
            }
        }
        return Validate;

    }
    private void populatColor()
    {
        ddlColors.DataSource = ColorList();
        ddlColors.DataBind();
        ddlColors.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private List<string> ColorList()
    {
        string[] allColors = Enum.GetNames(typeof(System.Drawing.KnownColor));
        string[] systemEnvironmentColors =
            new string[(
            typeof(System.Drawing.SystemColors)).GetProperties().Length];
        int index = 0;
        foreach (MemberInfo member in (
            typeof(System.Drawing.SystemColors)).GetProperties())
        {
            systemEnvironmentColors[index++] = member.Name;
        }
        List<string> finalColorList = new List<string>();

        foreach (string color in allColors)
        {
            if (Array.IndexOf(systemEnvironmentColors, color) < 0)
            {
                finalColorList.Add(color);
            }
        }
        return finalColorList.OrderBy(q => q).ToList();
    }
    private void ApplyDivColor(DropDownList ddlColors)
    {
        int row;
        for (row = 0; row < ddlColors.Items.Count - 1; row++)
        {
            ddlColors.Items[row].Attributes.Add("style",
                "background-color:" + ddlColors.Items[row].Value);
        }
        ddlColors.BackColor =
            Color.FromName(ddlColors.SelectedItem.Text);
    }
    protected void ddlGrdType_Color_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlGrdType_Color = (DropDownList)sender;
        //GridViewRow GridRow=(GridViewRow)ddlGrdType_Color.Parent.Parent;
        ddlGrdType_Color.BackColor = Color.FromName(ddlColors.SelectedItem.Text);
        ApplyDivColor(ddlGrdType_Color);
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (ddlType.SelectedValue)
        {
            case "1": //Severity
                ddlColors.Enabled = false;
                tdDesc.Text = "*";
                tdDisText.Text = "*";
                tdvalue.Text = "*";
                tdcolor.Text = "";
                break;
            case "2"://Likelihood
                ddlColors.Enabled = false;
                tdDesc.Text = "*";
                tdDisText.Text = "*";
                tdvalue.Text = "*";
                tdcolor.Text = "";
                break;
            case "3"://Risk
                ddlColors.Enabled = true;
                tdDesc.Text = "*";
                tdDisText.Text = "*";
                tdvalue.Text = "";
                tdcolor.Text = "*";
                break;
            case "4": //Consequences
                ddlColors.Enabled = false;
                tdDesc.Text = "*";
                tdDisText.Text = "*";
                tdvalue.Text = "";
                tdcolor.Text = "";
                break;

        }
    }
    #endregion

    
}