using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Technical;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Properties;
public partial class Technical_PMS_PMSCategoryLibrary : System.Web.UI.Page
{
    BLL_Tec_Worklist objWL = new BLL_Tec_Worklist();
    UserAccess objUA = new UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCategory();
            ViewState["Mode"] = "ADD";
          
        }
        UserAccessValidation();
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);


        if (objUA.View == 0)
        {
            //  Response.Redirect("~/default.aspx?msgid=1");
            lblError.Text = "You don't have sufficient privilege to view this page";
            lblError.Visible = true;
            grdCategories.Visible = false;
            lblCatName.Visible = false;
            lblCatType.Visible = false;
            txtCatName.Visible = false;
            ddlType.Visible = false;
            btnCategorySave.Visible = false;
        }
        if (objUA.Add == 0)
        {
           // Response.Redirect("~/default.aspx?msgid=2");
            //pnlAddAttachment.Enabled = false;
            btnCategorySave.Enabled = false;
        }
        if (objUA.Edit == 0)
        {
            //Response.Redirect("~/default.aspx?msgid=3");


            for (int i = 0; i < grdCategories.Rows.Count; i++)
            {
                ImageButton imgb=((ImageButton)grdCategories.Rows[i].Cells[2].FindControl("ImgEditCategory"));
                imgb.Visible = false;
            } 
            btnCategorySave.Enabled = false;

        }
        if (objUA.Delete == 0)
        {
            for (int i = 0; i < grdCategories.Rows.Count; i++)
            {
                ImageButton imgb = ((ImageButton)grdCategories.Rows[i].Cells[2].FindControl("ImgCategoryDelete"));
                imgb.Visible = false;
            }
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
    protected void BindCategory()
    {
        DataSet ds=objWL.TEC_Get_Category();
        if(ds.Tables.Count>0)
        {
            grdCategories.DataSource=ds.Tables[0];
            grdCategories.DataBind();
        }
    }

    protected void btnCategorySave_Click(object sender, EventArgs e)
    {
        string CategoryName, CategoryType;

        CategoryName = txtCatName.Text;
        CategoryType = ddlType.SelectedValue;
        if (ViewState["Mode"].ToString() == "ADD")
        {
            objWL.TEC_Insert_Category(CategoryName, CategoryType, UDFLib.ConvertToInteger(Session["userid"].ToString()));
            string js2 = "alert('Category Saved Successfully');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert10", js2, true);
        }
        else if (ViewState["Mode"].ToString() == "EDIT")
        {
               int ID = Convert.ToInt32(ViewState["CategoryID"].ToString());
               objWL.TEC_Update_Category(ID, CategoryName, CategoryType, UDFLib.ConvertToInteger(Session["userid"].ToString()));
               string js2 = "alert('Category Updated Successfully');";
               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert10", js2, true);
        }

        BindCategory();
        ViewState["CategoryID"] = "";
        ViewState["Mode"] = "ADD";
        txtCatName.Text = "";
        updCat.Update();
      //  btnCategorySave.Enabled = false;

    }

    protected void btnCategoryAdd_Click(object sender, EventArgs e)
    {
        txtCatName.Text = "";
        ddlType.SelectedValue = "--SELECT--";
        ViewState["Mode"] = "ADD";
       // btnCategorySave.Enabled = true;

    }
    protected void ImgCategoryRestore_Click(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument.ToString());
        objWL.TEC_Restore_Category(ID,UDFLib.ConvertToInteger(Session["userid"].ToString()));
        BindCategory();
    }
    protected void ImgCategoryDelete_Click(object sender, CommandEventArgs e)
    {

        int ID = Convert.ToInt32(e.CommandArgument.ToString());
        objWL.TEC_Delete_Category(ID,UDFLib.ConvertToInteger(Session["userid"].ToString()));
        BindCategory();

    }
    protected void ImgEditCategory_Click(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument.ToString());

        DataSet ds = new DataSet();
        ds = objWL.TEC_Get_CategoryByID(ID);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtCatName.Text = ds.Tables[0].Rows[0][1].ToString();
            ddlType.SelectedValue = ds.Tables[0].Rows[0][2].ToString();
       
        }

        ViewState["Mode"] = "EDIT";
        ViewState["CategoryID"] = ID;
     //   btnCategorySave.Enabled = true;
    }
    protected void lnkCategory_Click(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument.ToString());

        DataSet ds = new DataSet();
        ds = objWL.TEC_Get_CategoryByID(ID);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtCatName.Text = ds.Tables[0].Rows[0][1].ToString();
        
            ddlType.SelectedValue = ds.Tables[0].Rows[0][2].ToString();
        }


    }
}