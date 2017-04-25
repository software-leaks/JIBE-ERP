using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Web.UI.HtmlControls;
using SMS.Business.PMS;
using SMS.Business.Crew;
using SMS.Business.FMS;
using System.Data.SqlClient;
using SMS.Properties;

/// <summary>
/// Created by Reshma :DT:27-07-2016
/// </summary>

public partial class Technical_PMS_PMSFunctionsLibraryPage : System.Web.UI.Page
{
    BLL_PMS_Library_Jobs objBLL = new BLL_PMS_Library_Jobs();
    UserAccess objUA = new UserAccess();
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserAccessValidation();
            if (!IsPostBack)
            {
                ViewState["SORTDIRECTION"] = 0;
                ViewState["SORTBYCOLOUMN"] = "Description";

                ucCustomPagerItems.PageSize = 20;
                PMS_Get_Function();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// To check acces for logged in user.
    /// like Add,Edit ,delete access for requested page.
    /// </summary>
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Admin == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;

        if (objUA.Delete == 1) uaDeleteFlage = true;
    }
    /// <summary>
    /// Fill the function list whent page will be load
    /// </summary>
    /// <param name="ParentType">Parent Code for function</param>
    /// <param name="sortby">For Sorting</param>
    /// <param name="sortdirection">sortdirection</param>
    /// <param name="pagenumber">Give the page no</param>
    /// <param name="pagesize">Give the page size</param>
    /// <param name="isfetchcount">Total fetch count</param>
    /// <returns>Display the Function List using Parent Type</returns>

    public void PMS_Get_Function()
    {
        try
        {
            int ParentType = 0;
            int rowcount = ucCustomPagerItems.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = objBLL.PMS_Get_Function(ParentType, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
            gvPMSFunctionLib.DataSource = dt;
            gvPMSFunctionLib.DataBind();

            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Fill the function list using search keyword
    /// </summary>
    /// <param name="Search">Search keyword</param>
    /// <param name="ParentType">Parent Code for function</param>
    /// <param name="sortby">For Sorting</param>
    /// <param name="sortdirection">sortdirection</param>
    /// <param name="pagenumber">Give the page no</param>
    /// <param name="pagesize">Give the page size</param>
    /// <param name="isfetchcount">Total fetch count</param>
    /// <returns>Display the Function List using search keyword</returns>
    public void PMS_Get_FunctionBySearch()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;
            int ParentType = 0;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            DataTable dt = objBLL.PMS_Get_FunctionBySearch(txtSearch.Text.Trim(), UDFLib.ConvertToInteger(ParentType), sortbycoloumn, sortdirection
                 , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

            gvPMSFunctionLib.DataSource = dt;
            gvPMSFunctionLib.DataBind();

            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        try
        {
            this.SetFocus("ctl00_MainContent_txtFunction");
            HiddenFlag.Value = "Add";

            OperationMode = "Add Function";
            lblMsg.Text = "";
            ClearFields();
            string AddFunction = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddFunction", AddFunction, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        try
        {
            lblMsg.Text = string.Empty;
            if (ValidateControls())
            {
                SaveUpdateFunction();
            }
            else if (HiddenFlag.Value == "Add")
            {
                OperationMode = "Add Function";
                string ErorAddFunction = String.Format("showModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ErorAddFunction", ErorAddFunction, true);
            }
            else
            {
                OperationMode = "Edit Function";
                string ErorEditFunction = String.Format("showModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ErorEditFunction", ErorEditFunction, true);
            }
        }
        catch (SqlException ex)
        {

            lblMsg.Text = ex.Message.ToString();
            string AddFunction = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddFunction", AddFunction, true);
           
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// For Add and Update Function Name
    /// </summary>
    /// <param name="Code">Primary Key</param>
    /// <param name="ParentType">Parent Code for Function</param>
    /// <param name="ShortCode">ShortCode for function</param>
    /// <param name="FunctionName">Description</param>
    /// <param name="userID">UserID</param>
    /// <returns>Insert the function in database</returns>
    private void SaveUpdateFunction()
    {
        try
        {
            string strShortCode = txtShortCode.Text.Trim();
            string strFunction = txtFunction.Text.Trim();
           // if (strShortCode !="" || strFunction != "")
           // {
                int ParentType = 0;
                if (HiddenFlag.Value == "Add")
                {
                    OperationMode = "Add Function";

                    objBLL.PMS_Insert_Function(UDFLib.ConvertIntegerToNull(txtID.Text), UDFLib.ConvertToInteger(ParentType), strShortCode, strFunction, Convert.ToInt32(Session["USERID"]));

                    string jsSqlError2 = "alert('Function saved successfully.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
                    ClearFields();
                }
                else
                {
                    OperationMode = "Edit Function";
                    objBLL.PMS_Update_Function(UDFLib.ConvertIntegerToNull(txtID.Text), UDFLib.ConvertToInteger(ParentType), strShortCode, strFunction, Convert.ToInt32(Session["USERID"]));

                    string jsSqlError3 = "alert('Function updated successfully.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError3", jsSqlError3, true);
                    ClearFields();
                }

                PMS_Get_Function();
                string hidemodal = String.Format("hideModal('divadd')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
            //}        
         //else
         // {
         //    string jsEmpty = "alert('Function name & short code can not be empty.');";
         //    ScriptManager.RegisterStartupScript(this, this.GetType(), "jsEmpty", jsEmpty, true);             
         // }
        }
        catch (Exception ex)
        {
            throw ex;

        }

    }

    /// <summary>
    /// Bind Function name against Code
    /// </summary>
    /// <param name="Code">Primary key</param>
    /// <returns>Details of specific function for update.</returns>
    protected void onUpdate(object source, CommandEventArgs e)
    {
        try
        {
            this.SetFocus("ctl00_MainContent_txtFunction");
            HiddenFlag.Value = "Edit";
            OperationMode = "Edit Function";
            lblMsg.Text = "";
            ClearFields();
            DataSet ds = new DataSet();

            ds = objBLL.PMS_Get_BindFunctionName(Convert.ToInt32(e.CommandArgument.ToString()));
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtID.Text = e.CommandArgument.ToString();
                    txtShortCode.Text = ds.Tables[0].Rows[0]["Short_Code"].ToString();
                    txtFunction.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                    string EditFunction = String.Format("showModal('divadd',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditFunction", EditFunction, true);
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// Delete the function
    /// </summary>
    /// <param name="Code">Primary Key</param>
    /// <param name="userID">UserID</param>
    /// <returns>Delete the function</returns>
    protected void onDelete(object source, CommandEventArgs e)
    {
        // int Res= 0;
        //int JOBID = 0;
        try
        {

          int  Res=objBLL.PMS_Delete_Function(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
          if (Res > 0)
          {
              string js = "alert('Function deleted successfully.');";
              ScriptManager.RegisterStartupScript(this, this.GetType(), "alertDelete", js, true);

              PMS_Get_Function();
          }
          else
          {
              string js = "alert('This function contains active Systems and cannot be deleted');";
              ScriptManager.RegisterStartupScript(this, this.GetType(), "alertDelete", js, true);
          }
            
        }        
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    /// <summary>
    /// To validate controls.
    /// </summary>
    /// <returns>Check for Function Name and Short code should not be empty</returns>
    private bool ValidateControls()
    {
        try
        {
            string strShortCode = txtShortCode.Text.Trim();
            string strFunction = txtFunction.Text.Trim();
            if (strFunction == "")
            {
                string js = "alert('Please Enter Function Name.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertFun", js, true);
                return false;
            }
            if (strShortCode == "")
            {
                string js = "alert('Please Enter Short Code.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertCode", js, true);
                return false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return true;
    }

    /// <summary>
    /// For clear the fields.
    /// </summary>
    protected void ClearFields()
    {
        txtFunction.Text = "";
        txtShortCode.Text = "";
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            string strSearch = txtSearch.Text.Trim();
            ucCustomPagerItems.CurrentPageIndex = 1;
            PMS_Get_FunctionBySearch();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            txtSearch.Text = "";
            PMS_Get_Function();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;
            int ParentType = 0;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = objBLL.PMS_Get_FunctionBySearch(txtSearch.Text.Trim(), ParentType, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string[] HeaderCaptions = { "Function Name" };
                    string[] DataColumnsName = { "Description" };

                    GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "FunctionName", "Function Library");
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
}