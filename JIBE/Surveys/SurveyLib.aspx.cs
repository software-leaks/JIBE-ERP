using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.Survey;

public partial class Surveys_SurveyLib : System.Web.UI.Page
{
    BLL_SURV_Survey objBLL = new BLL_SURV_Survey();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            Load_CategoryList();
            Load_CertificateList();
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            lnkAddNewCategory.Visible = false;
            lnkAddNewCertificate.Visible = false;
        }

        if (objUA.Edit == 0)
        {
            GridView_Certificate.Columns[GridView_Certificate.Columns.Count - 2].Visible = false;
            GridView_Category.Columns[GridView_Category.Columns.Count - 2].Visible = false;

        }
        if (objUA.Delete == 0)
        {
            GridView_Certificate.Columns[GridView_Certificate.Columns.Count - 1].Visible = false;
            GridView_Category.Columns[GridView_Category.Columns.Count - 2].Visible = false;

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
    
    protected void Load_CategoryList()
    {
        string SearchText = txtCategory.Text;
        DataTable dt = objBLL.Get_Survay_CategoryList(SearchText);
        GridView_Category.DataSource = dt;
        GridView_Category.DataBind();
                
    }
    protected void ddlCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_CertificateList();
    }
    protected void Load_CertificateList()
    {
        int CatID = UDFLib.ConvertToInteger(ddlCategoryFilter.SelectedValue);
         string SearchText = txtCertificate.Text;

        DataTable dt = objBLL.Get_SurvayCertificate_List(CatID, SearchText);
        GridView_Certificate.DataSource = dt;
        GridView_Certificate.DataBind();
    }

    protected void txtCategory_TextChanged(object sender, EventArgs e)
    {
        Load_CategoryList();
    }
    protected void txtCertificate_TextChanged(object sender, EventArgs e)
    {
        Load_CertificateList();
    }

    protected void GridView_Category_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Category.DataKeys[e.RowIndex].Value.ToString());

        objBLL.DELETE_Survey_Category(ID, GetSessionUserID());
        Load_CategoryList();
    }
    protected void GridView_Category_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Category.DataKeys[e.RowIndex].Value.ToString());
        string Category_Name = e.NewValues["Survey_Category"].ToString();

        objBLL.UPDATE_Survey_Category(0,ID, Category_Name, GetSessionUserID());
        GridView_Category.EditIndex = -1;
        Load_CategoryList();
    }
    protected void GridView_Category_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Category.EditIndex = e.NewEditIndex;
            Load_CategoryList();
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
            Load_CategoryList();

        }
        catch
        {
        }


    }

    protected void GridView_Certificate_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Certificate.DataKeys[e.RowIndex].Value.ToString());

        objBLL.DELETE_Survey_Certificate(ID, GetSessionUserID());
        Load_CertificateList();
    }
    protected void GridView_Certificate_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_Certificate.DataKeys[e.RowIndex].Value.ToString());

        string Survey_Cert_Name = (e.NewValues["Survey_Cert_Name1"] != null) ? e.NewValues["Survey_Cert_Name1"].ToString() : "";
        string Survey_Cert_remarks = (e.NewValues["Survey_Cert_remarks"]!=null)?e.NewValues["Survey_Cert_remarks"].ToString():"";
        int Survey_Category_ID = UDFLib.ConvertToInteger(e.NewValues["Survey_Category_ID"].ToString());
        int Term = (e.NewValues["Term"]!=null)?UDFLib.ConvertToInteger(e.NewValues["Term"].ToString()):0;

        objBLL.UPDATE_Survey_Certificate(ID, Survey_Cert_Name,Survey_Category_ID, GetSessionUserID(),Term,Survey_Cert_remarks,null,null);
        GridView_Certificate.EditIndex = -1;
        Load_CertificateList();
    }
    protected void GridView_Certificate_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_Certificate.EditIndex = e.NewEditIndex;
            Load_CertificateList();

        }
        catch
        {
        }
    }
    protected void GridView_Certificate_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_Certificate.EditIndex = -1;
            Load_CertificateList();

        }
        catch
        {
        }


    }
    
    protected void btnSaveAndAddCat_Click(object sender, EventArgs e)
    {
        int ID = objBLL.INSERT_Survey_Category(0,txtCatName.Text.Trim(), GetSessionUserID());
        txtCatName.Text = "";
        txtCategory.Text = "";
        Load_CategoryList();
        ddlCategoryFilter.DataBind();
    }
    protected void btnSaveAndCloseCat_Click(object sender, EventArgs e)
    {
        int ID = objBLL.INSERT_Survey_Category(0,txtCatName.Text.Trim(), GetSessionUserID());
        txtCatName.Text = "";
        txtCategory.Text = "";
        Load_CategoryList();
        ddlCategoryFilter.DataBind();
        
        string js = "closeDivAddNewCategory();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

    }

    protected void btnSaveAndAddCertificate_Click(object sender, EventArgs e)
    {
       // int ID = objBLL.INSERT_Survey_Certificate(txtCertificateName.Text.Trim(), UDFLib.ConvertToInteger(ddlSurvey_Category.SelectedValue), GetSessionUserID(), UDFLib.ConvertToInteger(txtTerm.Text), txtSurvey_Cert_remarks.Text,null);
        txtCertificateName.Text = "";
        txtCertificate.Text = "";
        Load_CertificateList();
    }
    protected void btnSaveAndCloseCertificate_Click(object sender, EventArgs e)
    {
      //  int ID = objBLL.INSERT_Survey_Certificate(txtCertificateName.Text.Trim(), UDFLib.ConvertToInteger(ddlSurvey_Category.SelectedValue), GetSessionUserID(), UDFLib.ConvertToInteger(txtTerm.Text), txtSurvey_Cert_remarks.Text,null);
        txtCertificateName.Text = "";
        txtCertificate.Text = "";
        Load_CertificateList();

        string js = "closeDivAddNewCertificate();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

    }
}