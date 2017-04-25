using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.QMS;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Data;


public partial class QMS_SCM_SCM_Office_Dept_Representative : System.Web.UI.Page
{

    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    public UserAccess objUA = new UserAccess();
   

    protected void Page_Load(object sender, EventArgs e)
    {


        UserAccessValidation();
        if (!IsPostBack)
        {
            BindSCMOfficeRespresentativeSearch();
        }
       
    }


    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUA.View == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {
        }
        


    }



    public void BindSCMOfficeRespresentativeSearch()
    {

        DataSet ds = BLL_SCM_Report.SCMOfficeRepresentativeSearch(null, null, null, null);

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvOffRespresentative.DataSource = ds.Tables[0];
            gvOffRespresentative.DataBind();
        }
    }


    protected void gvOffRespresentative_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvOffRespresentative_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvOffRespresentative_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {


    }
    protected void gvOffRespresentative_Sorting(object sender, GridViewSortEventArgs  e)
    {


     

    }
    protected void gvOffRespresentative_RowEditing(object sender, GridViewEditEventArgs de)
    {


        GridView _gridView = (GridView)sender;

        _gridView.EditIndex = de.NewEditIndex;
        _gridView.SelectedIndex = de.NewEditIndex;


        BindSCMOfficeRespresentativeSearch();
         


    }
    protected void gvOffRespresentative_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        _gridView.EditIndex = -1;
        BindSCMOfficeRespresentativeSearch();
    }
    protected void gvOffRespresentative_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        GridView _gridView = (GridView)sender;
        int nCurrentRow = e.RowIndex;



        _gridView.EditIndex = -1;
        BindSCMOfficeRespresentativeSearch();
    }

    protected void gvOffRespresentative_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {



    }


    protected void gvOffRespresentative_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        GridView _gridView = (GridView)sender;
        int nCurrentRow = Int32.Parse(e.CommandArgument.ToString());

        if (e.CommandName.ToUpper().Equals("UPDATE"))
        {

            Label lblDeptID = (Label)_gridView.Rows[nCurrentRow].FindControl("lblDeptID");
            TextBox txtResEmailEdit = (TextBox)_gridView.Rows[nCurrentRow].FindControl("txtResEmailEdit");

            OfficeRepresentativeSave(lblDeptID.Text, txtResEmailEdit.Text);
            
            //OfficeRepresentativeSave(
            //            Int32.Parse(((Label)_gridView.Rows[nCurrentRow].FindControl("lblFleetidEdit")).Text),
            //            ((TextBox)_gridView.Rows[nCurrentRow].FindControl("txtFleetCodeEdit")).Text
            //         )



        }
        else if (e.CommandName.ToUpper().Equals("DELETE"))
        {

            Label lblDeptID = (Label)_gridView.Rows[nCurrentRow].FindControl("lblDeptID");
            Label lblScmResID = (Label)_gridView.Rows[nCurrentRow].FindControl("lblScmResID");

            OfficeRepresentativeDelete(lblDeptID.Text, lblScmResID.Text);

        }


        BindSCMOfficeRespresentativeSearch();



    }



    private void OfficeRepresentativeSave(string deptid, string emailAddress)
    {

        BLL_SCM_Report.SCMOfficeRepresentativeSave(Convert.ToInt32(deptid), emailAddress);

    }


    private void OfficeRepresentativeDelete(string deptid, string scmrespid)
    {

        BLL_SCM_Report.SCMOfficeRepresentativeDelete(Convert.ToInt32(deptid), UDFLib.ConvertIntegerToNull(scmrespid));

    }


}