using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Technical;
using System.Data;
public partial class Technical_Worklist_WorklistTypeLib : System.Web.UI.Page
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            
            Search_Worklist_Type();


        }
    }
    #endregion

    #region General Functions
     SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
       

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);
        ViewState["Admin"] = objUA.Add;
        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

      


    }
    protected bool GetValidation()
    {
        if (ViewState["Admin"] == null)
            ViewState["Admin"] = 1;
        if (ViewState["Admin"].ToString() == "1")
            return true;
        else
            return false;
    }

    private int GetSessionUserID()
    {

        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    protected void Search_Worklist_Type()
    {




        DataTable dt = BLL_Tec_WorklistType.TEC_GET_WORKLISTYPE().Tables[0];
        GridView_Category.DataSource = dt;
        GridView_Category.DataBind();




    }






   



    #endregion

     
     

    protected void GridView_Category_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        try
        {
            GridViewRow row = (GridViewRow)GridView_Category.Rows[e.RowIndex];
            int ID = UDFLib.ConvertToInteger(GridView_Category.DataKeys[e.RowIndex].Values[0].ToString());
         



            string Worklist_Type = (((HiddenField)row.FindControl("hdnWorklist_Type")).Value);
            string Worklist_Type_Display = (((TextBox)row.FindControl("txtWorklist_Type_Display")).Text.Trim());

            if (Worklist_Type_Display.Trim().Length == 0)
            {
                string hidemodal = String.Format("alert('Worklist Type Display is mandatory field')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);  
                return;
            }
            GridView_Category.EditIndex = -1;
            BLL_Tec_WorklistType.TEC_UPD_WORKLISTTYPE(Worklist_Type, Worklist_Type_Display);
            Search_Worklist_Type();


        }
        catch
        {
        }

    }
    protected void GridView_Category_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

            GridView_Category.EditIndex = e.NewEditIndex;
            //GroupGridviewHeader();
            Search_Worklist_Type();
        }
        catch
        {
        }
    }
    protected void GridView_Category_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_Category.EditIndex = -1;
            //GroupGridviewHeader();
            Search_Worklist_Type();
        }
        catch
        {
        }
    }
 











}