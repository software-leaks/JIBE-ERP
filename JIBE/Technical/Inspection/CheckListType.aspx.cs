using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.Inspection;

public partial class Technical_Inspection_CheckListType : System.Web.UI.Page
{
    BLL_INSP_Checklist objBLL = new BLL_INSP_Checklist();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            LoadCategoryList();
            Load_TypeList();
            Load_GradingList();
            BindMin();
            BindMax();
        }
    }

    protected void BindMin()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("OptionText");
        dt.Columns.Add("OptionValue");


        for (int i = 0; i <= 7; i++)
        {
            dt.Rows.Add(i, i);
        }

        ddlMin.DataSource = dt;
        ddlMin.DataTextField = "OptionText";
        ddlMin.DataValueField = "OptionValue";
        ddlMin.DataBind();
    }

    protected void BindMax()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("OptionText");
        dt.Columns.Add("OptionValue");


        for (int i = 0; i <= 7; i++)
        {
            dt.Rows.Add(i, i);
        }

        ddlMax.DataSource = dt;
        ddlMax.DataTextField = "OptionText";
        ddlMax.DataValueField = "OptionValue";
        ddlMax.DataBind();
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
            //lnkAddNewCategory.Visible = false;
            lnkAddNewType.Visible = false;
            lnkAddNewGrade.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            //GridView_Category.Columns[GridView_Category.Columns.Count - 2].Visible = false;
            GridView_EvaluationType.Columns[GridView_EvaluationType.Columns.Count - 2].Visible = false;
            GridView_Grading.Columns[GridView_Grading.Columns.Count - 2].Visible = false;

        }
        if (objUA.Delete == 0)
        {
            //GridView_Category.Columns[GridView_Category.Columns.Count - 1].Visible = false;
            GridView_EvaluationType.Columns[GridView_EvaluationType.Columns.Count - 1].Visible = false;
            GridView_Grading.Columns[GridView_Grading.Columns.Count - 1].Visible = false;
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

    /// <summary>
    /// Modified by Anjali DT:21-06-2016
    /// To fill gridview after save /update.
    /// </summary>
    protected void Load_GradingList()
    {
        try
        {
            DataTable dt = objBLL.Get_Grades(null);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    GridView_Grading.DataSource = dt;
                    GridView_Grading.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }

    protected void GridView_Grading_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Grading.DataKeys[e.RowIndex].Value.ToString());

        objBLL.DELETE_Grading(ID, GetSessionUserID());

        Load_GradingList();
    }

    protected void GridView_Grading_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            int ID = UDFLib.ConvertToInteger(GridView_Grading.DataKeys[e.RowIndex].Value.ToString());
            string Grade_Name = e.NewValues["Grade_Name"].ToString();
            int Grade_Type = UDFLib.ConvertToInteger(e.NewValues["Grade_Type"].ToString());
            int Min = UDFLib.ConvertToInteger(e.NewValues["Min"].ToString());
            int Max = UDFLib.ConvertToInteger(e.NewValues["Max"].ToString());
            if (Min > Max)
            {
                string js = "alert('Max. value should be smaller than min. value.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                return;
            }
            int Divisions = UDFLib.ConvertToInteger(e.NewValues["Divisions"].ToString());

            objBLL.UPDATE_Grading(ID, Grade_Name, Grade_Type, Min, Max, Divisions, GetSessionUserID());

            GridView_Grading.EditIndex = -1;
            Load_GradingList();

        }
        catch (Exception ex)
        {
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }

    protected void GridView_Grading_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Grading.EditIndex = e.NewEditIndex;
            Load_GradingList();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void GridView_Grading_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_Grading.EditIndex = -1;
            Load_GradingList();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void GridView_Grading_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ID = UDFLib.ConvertToInteger(GridView_Grading.DataKeys[e.Row.RowIndex].Value.ToString());

                RadioButtonList rdo = (RadioButtonList)e.Row.FindControl("rdoOptions");
                DataTable dt = objBLL.Get_GradingOptions(ID);
                //BLL_Crew_Evaluation.Get_GradingOptions(ID);

                rdo.DataSource = dt;
                rdo.DataBind();
            }

        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }

    /// <summary>
    /// Added by Anjali DT:04-Jun-2016
    /// To Validate selected values of min point ,max point and options.
    /// Remarks: 1.Min and Max point can't be same.
    ///          2.Max point should be greater than min point.
    ///          3. Min point , Max point and options can not be zero.
    /// </summary>
    /// <returns>True/False|| False:If Validation fail || True :Validation Successed. </returns>
    private bool ValidateData(bool IsSaveClicked)
    {
        try
        {
            decimal min = UDFLib.ConvertToDecimal(ddlMin.SelectedValue);
            decimal max = UDFLib.ConvertToDecimal(ddlMax.SelectedValue);
            int Division = UDFLib.ConvertToInteger(ddlDivisions.SelectedValue) - 1;

            if (IsSaveClicked == false)
            {
                if (min == 0)
                {
                    string jsSqlError = "alert('Min. point can not be zero.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError", jsSqlError, true);
                    ddlMin.Focus();
                    ddlDivisions.SelectedIndex = 0;
                    return false;
                }
                if (max == 0)
                {
                    string jsSqlError = "alert('Max. point can not be zero.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError", jsSqlError, true);
                    ddlMax.Focus();
                    ddlDivisions.SelectedIndex = 0;
                    return false;
                }
                if (min == max)
                {
                    string jsSqlError = "alert('Min. and max. point can not be same.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError", jsSqlError, true);
                    ddlMin.SelectedIndex = 0;
                    ddlMax.SelectedIndex = 0;
                    return false;
                }
                if (max < min)
                {
                    string jsSqlError = "alert('Max. point should be greater than min. point.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError", jsSqlError, true);
                    ddlMax.SelectedIndex = 0;
                    return false;
                }
                if (Division < 0)
                {
                    return false;
                }
              

            }
            if (IsSaveClicked == true)
            {
                if (UDFLib.ConvertToInteger(ddlDivisions.SelectedValue) == 0)
                {
                    string jsSqlError = "alert('Select no. of options.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError", jsSqlError, true);
                    ddlDivisions.Focus();
                    return false;
                }
            }

        }

        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        return true;
    }

    /// <summary>
    /// Modified by Anjali DT:4-jun-2016|| JIT:9718
    /// To display options/Values on selection of Min point ,Max point and Options.
    /// </summary>
    /// <remarks>
    /// e.g Min point = 1 
    ///     Max point = 2
    ///     No of options = 3
    ///  As per input Grade will be created as followes:
    ///  1. Grade will be divided in 3 options.
    ///  2. Min point will be 1.
    ///  3. Min  point  will be incremented by : Math.Round((max - min) / (Division-1), 4) .i.e  Math.Round((2 - 1) / (3-1), 4) = 0.5
    ///  4. Second value will be 1.5, as '0.5' value will be added to Min point i.e 1 + 0.5 = 1.5.
    ///  5. For third value 0.5 will be added to 1.5 + 0.5 = 2.0 is third value.
    ///  6. Again Min  will incremented by 0.5 ie. 2.0 + 0.5 =2.5.
    ///  6. Now loop will break as Min is greater than Max value.
    ///  7.O/P will be as follows as per selection .
    ///  Value      Text
    ///  1          1
    ///  1.5        1.5
    ///  2.0        2.0
    /// </summary>
    private void ShowOptions()
    {
        decimal min = UDFLib.ConvertToDecimal(ddlMin.SelectedValue);
        decimal max = UDFLib.ConvertToDecimal(ddlMax.SelectedValue);
        int Division = UDFLib.ConvertToInteger(ddlDivisions.SelectedValue) - 1;

        try
        {

            DataTable dt = new DataTable("Options");
            dt.Columns.Add("OptionText", typeof(string));
            dt.Columns.Add("OptionValue", typeof(string));

            if (ValidateData(false)) // Added by anjali DT:04-Jun-2016
            {
                decimal increment = Math.Round((max - min) / Division, 4);

                while (min <= max)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1[0] = min.ToString();
                    dr1[1] = min.ToString();
                    dt.Rows.Add(dr1);

                    min += increment;
                }
                if (dt.Rows.Count < Convert.ToInt32(ddlDivisions.SelectedValue))
                {
                    DataRow dr1 = dt.NewRow();
                    dr1[0] = max.ToString();
                    dr1[1] = max.ToString();
                    dt.Rows.Add(dr1);
                }
                rptOptions.DataSource = dt;
                rptOptions.DataBind();
                dvrpt.Style.Add("display", "block");

            }
            else
            {
                dvrpt.Style.Add("display", "none");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// This event gets called on Max. point , Min. point and No. of options selection and calls a method to display options on selection.
    /// 
    /// <remarks>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDivisions_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ShowOptions();
        }
        catch (Exception ex)
        {
            string jsSqlError = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError", jsSqlError, true);
        }

    }

    /// <summary>
    /// Added by Anjali DT:16-06-2016
    /// To save grades.
    /// </summary>
    private void SaveGrades()
    {
        try
        {
           

                int ID = objBLL.INSERT_Grading(txtGrade.Text.Trim(), UDFLib.ConvertToInteger(rdoGradeType.SelectedValue), UDFLib.ConvertToInteger(ddlMin.SelectedValue), UDFLib.ConvertToInteger(ddlMax.SelectedValue), UDFLib.ConvertToInteger(ddlDivisions.SelectedValue), GetSessionUserID());
                if (ID > 0)
                {
                    txtGrade.Text = "";
                    ddlMin.SelectedIndex = 0;
                    ddlMax.SelectedIndex = 0;
                    ddlDivisions.SelectedIndex = 0;
                    dvrpt.Style.Add("display", "none");
                    for (int i = 0; i < rptOptions.Items.Count; i++)
                    {
                        TextBox txtValue = (TextBox)rptOptions.Items[i].FindControl("txtValue");
                        TextBox txtText = (TextBox)rptOptions.Items[i].FindControl("txtText");

                        if (txtText != null && txtValue != null)
                        {
                            objBLL.INSERT_GradingOption(ID, txtText.Text.Trim(), UDFLib.ConvertToDecimal(txtValue.Text), GetSessionUserID());
                        }
                    }
                    Load_GradingList();

                    BindMin();
                    BindMax();

                    string jsPrompt1 = "alert('Grade saved successfully.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsPrompt1", jsPrompt1, true);
                }
                else
                {
                    string jsPrompt1 = "alert('Grade name already exists.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsPrompt1", jsPrompt1, true);
                }
            

        }
        catch (SqlException sqlEx)
        {
            UDFLib.WriteExceptionLog(sqlEx);
        }
        catch (Exception Ex)
        {
            UDFLib.WriteExceptionLog(Ex);
        }
     
    }

    /// <summary>
    /// Modified by Anjali DT:16-06-2016 JIT:10047
    /// In this event, If all validation successed then grade details will saved and popup remain open to add new grade details.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveGrade_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateData(true)) 
            {
                if (ValidateAll() == true)
                {
                    SaveGrades();
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }
    /// <summary>
    /// Method is used to validate empty Text and value field.
    /// </summary>
    /// <returns>retrun true if both fields is not blank else return false</returns>
    protected bool ValidateAll()
    {
        try
        {
            for (int i = 0; i < rptOptions.Items.Count; i++)
            {
                TextBox txtValue = (TextBox)rptOptions.Items[i].FindControl("txtValue");
                TextBox txtText = (TextBox)rptOptions.Items[i].FindControl("txtText");

                if (txtText.Text == "")
                {
                    string jsPrompt1 = "alert('Text field can not be blank.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsPrompt1", jsPrompt1, true);
                    return false;

                }
                else if (txtValue.Text == "")
                {
                    string jsPrompt1 = "alert('Value field can not be blank.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsPrompt1", jsPrompt1, true);
                    return false;
                }

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
         
        }
        return true;
    }
    /// <summary>
    /// Modified by Anjali DT:16-06-2016
    /// To close 'Add new Grade' Popup.
    /// Clear controls or set default values to controls.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCloseGrade_Click(object sender, EventArgs e)
    {
        rdoGradeType.SelectedValue = "1";
        pnlSubjective.Visible = false;
        rptOptions.Visible = true;
        tr1.Visible = true;
        tr2.Visible = true;
        tr3.Visible = true;
        txtGrade.Text = "";
        ddlMin.SelectedIndex = 0;
        ddlMax.SelectedIndex = 0;
        ddlDivisions.SelectedIndex = 0;
        dvrpt.Style.Add("display", "none");
    }

    /// <summary>
    /// Modified by Anjali DT:16-06-2016 JIT:10047
    /// In this event  If all validation successed then grade details will saved and popup close.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveAndCloseGrade_Click(object sender, EventArgs e)
    {
        if (ValidateData(true))
        {
            if (ValidateAll() == true)
            {
                SaveGrades();

                rdoGradeType.SelectedValue = "1";
                pnlSubjective.Visible = false;
                rptOptions.Visible = true;
                tr1.Visible = true;
                tr2.Visible = true;
                tr3.Visible = true;
                string js = "closeDiv('dvAddNewGrade');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            }
        }


    }

    protected void Load_TypeList()
    {
        DataTable dt = objBLL.Get_CheckListType(null);// BLL_Crew_Evaluation.Get_EvaluationTypeList();
        GridView_EvaluationType.DataSource = dt;
        GridView_EvaluationType.DataBind();
    }

    protected void GridView_EvaluationType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_EvaluationType.DataKeys[e.RowIndex].Value.ToString());

        objBLL.DELETE_EvaluationType(ID, GetSessionUserID());

        Load_TypeList();
    }

    protected void GridView_EvaluationType_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            int ID = UDFLib.ConvertToInteger(GridView_EvaluationType.DataKeys[e.RowIndex].Value.ToString());

            string Evaluation_Type = e.NewValues["Category_Name"].ToString();

            objBLL.UPDATE_EvaluationType(ID, Evaluation_Type, GetSessionUserID());

            GridView_EvaluationType.EditIndex = -1;
            Load_TypeList();
        }
        catch (Exception Ex)
        {
            string jsSqlError = "alert('" + Ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError", jsSqlError, true);
        }

    }

    protected void GridView_EvaluationType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_EvaluationType.EditIndex = e.NewEditIndex;
            Load_TypeList();

        }
        catch
        {
        }
    }

    protected void GridView_EvaluationType_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_EvaluationType.EditIndex = -1;
            Load_TypeList();

        }
        catch
        {
        }


    }

    protected void btnSaveEvaType_Click(object sender, EventArgs e)
    {
        try
        {

            int ID = objBLL.INSERT_CHecklistType(txtEvaluationType.Text.Trim(), GetSessionUserID());

            if (ID > 0)
            {
                txtEvaluationType.Text = "";
                string jsPrompt11 = "alert('Checklist type saved successfully.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsPrompt11", jsPrompt11, true);
                Load_TypeList();
            }
            else
            {
                string jsPrompt11 = "alert('Checklist type already exists.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsPrompt11", jsPrompt11, true);
            }
        }
        catch (SqlException sqlEx)
        {
            string jsSqlError2 = "alert('" + sqlEx.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
        catch (Exception Ex)
        {
            string jsSqlError2 = "alert('" + Ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }

    protected void btnSaveAndCloseEvaType_Click(object sender, EventArgs e)
    {

        btnSaveEvaType_Click(null, null);
        string js = "closeDiv('dvAddNewType');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

    }

    protected void rdoGradeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoGradeType.SelectedValue == "1")
        {
            pnlSubjective.Visible = false;
            rptOptions.Visible = true;
            rptOptions.DataSource = null;
            rptOptions.DataBind();
            ddlMax.SelectedIndex = 0;
            ddlMin.SelectedIndex = 0;
            ddlDivisions.SelectedIndex = 0;

            tr1.Visible = true;
            tr2.Visible = true;
            tr3.Visible = true;

        }
        else
        {
            pnlSubjective.Visible = true;
            rptOptions.Visible = false;
            rptOptions.DataSource = null;
            rptOptions.DataBind();
            ddlMax.SelectedIndex = 0;

            ddlMin.SelectedIndex = 0;
            ddlDivisions.SelectedIndex = 0;

            tr1.Visible = false;
            tr2.Visible = false;
            tr3.Visible = false;
        }
    }

    protected void lnkAddNewType_Click(object sender, EventArgs e)
    {
        txtEvaluationType.Text = "";
    }

    protected void lnkAddNewGrade_Click(object sender, EventArgs e)
    {
        rdoGradeType.SelectedValue = "1";
        pnlSubjective.Visible = false;
        rptOptions.Visible = true;
        tr1.Visible = true;
        tr2.Visible = true;
        tr3.Visible = true;


    }

    // Added by Anjali dt :09-05-2016 JIT:9484
    # region Category

    private void LoadCategoryList()
    {
        try
        {
            DataTable _categoryTable = objBLL.Get_CategoryList();
            gvCategory.DataSource = _categoryTable;
            gvCategory.DataBind();
        }
        catch (Exception ex)
        {
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }

    protected void btnSaveCategory_Click(object sender, EventArgs e)
    {
        SaveCategory();
    }

    protected void btnSaveandCloseCategory_Click(object sender, EventArgs e)
    {
        SaveCategory();
        string js = "closeDiv('dvAddNewCategory');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
    }

    private void SaveCategory()
    {
        try
        {
            //int _id = objBLL.InsertCategory(txtCategory.Text.Trim(), GetSessionUserID());
            int _id = objBLL.INSERT_UPDATE_DELETE_Category(0, txtCategory.Text.Trim(), GetSessionUserID(), 'A');//'A'-ADD,
            if (_id > 0)
            {
                txtCategory.Text = "";
                string jsPrompt2 = "alert('Category saved successfully.')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "jsPrompt2", jsPrompt2, true);
                LoadCategoryList();
            }

        }
        catch (Exception ex)
        {
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }

    }

    protected void gvCategory_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvCategory.EditIndex = -1;
            LoadCategoryList();

        }
        catch (Exception ex)
        {
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }
    }

    protected void gvCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int ID = UDFLib.ConvertToInteger(gvCategory.DataKeys[e.RowIndex].Value.ToString());

            objBLL.INSERT_UPDATE_DELETE_Category(ID, string.Empty, GetSessionUserID(), 'D');//'D'-DELETE

            LoadCategoryList();
        }
        catch (Exception ex)
        {
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }


    }

    protected void gvCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvCategory.EditIndex = e.NewEditIndex;
            LoadCategoryList();
        }
        catch (Exception ex)
        {
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }

    }

    protected void gvCategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int _category_Id = UDFLib.ConvertToInteger(gvCategory.DataKeys[e.RowIndex].Value.ToString());
            string _categoryName = e.NewValues["Category_Name"].ToString();
            objBLL.INSERT_UPDATE_DELETE_Category(_category_Id, _categoryName, GetSessionUserID(), 'U'); //'U'-UPDATE
            gvCategory.EditIndex = -1;
            LoadCategoryList();
        }
        catch (Exception ex)
        {
            string jsSqlError2 = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
        }

    }

    // Added by Anjali dt :09-05-2016 JIT:9484
    protected void lnkAddNewCategory_Click(object sender, EventArgs e)
    {
        string js;
        txtCategory.Text = "";
        js = " $('#dvAddNewCategory').prop('title', 'Add New Category');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", js, true);
    }
    #endregion
}