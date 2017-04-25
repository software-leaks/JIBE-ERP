using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.FMS;
using System.Data;
public partial class FMS_FMSFormTypeLibrary : System.Web.UI.Page
{
    public UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_FMS_Document objFMS = new BLL_FMS_Document();
    DataSet dsFromType = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFormType();
          
            ViewState["Mode"] = "ADD";
           // btnFormTypeSave.Enabled = false;
        }
        UserAccessValidation();
    }


    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUA.View == 0)
        {
            Response.Redirect("~/crew/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {

        }
    }
    public void BindFormType()
    {
        dsFromType.Clear();
        dsFromType = objFMS.FMS_Get_FormTypeList();


        if (dsFromType.Tables[0].Rows.Count > 0)
        {
            grdFormType.DataSource = dsFromType.Tables[0];
            grdFormType.DataBind();
        }
    }

    protected void grdFormType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnActiveStatus = (HiddenField)e.Row.FindControl("hdnActiveStatus");
            if (hdnActiveStatus.Value == "1")
            {
                ImageButton ImgRestore = (ImageButton)e.Row.FindControl("ImgRestoreFormType");
                ImageButton ImgEdit = (ImageButton)e.Row.FindControl("ImgEditFormType");
                ImgEdit.Visible = true;
                ImgRestore.Visible = false;
                LinkButton lnk = (LinkButton)e.Row.FindControl("lnkFormType");
                lnk.Enabled = true;

             //   e.Row.Cells[1].BackColor = Color.FromName(e.Row.Cells[2].Text);


            }
            if (hdnActiveStatus.Value == "0")
            {
                ImageButton ImgDelete = (ImageButton)e.Row.FindControl("ImgDeleteFormType");
                ImageButton ImgEdit = (ImageButton)e.Row.FindControl("ImgEditFormType");
                ImgDelete.Visible = false;
                ImgEdit.Visible = false;
                LinkButton lnk = (LinkButton)e.Row.FindControl("lnkFormType");
                lnk.Enabled = false;

            }
        }
    }
  
    protected void btnFormTypeSave_Click(object sender, EventArgs e)
    {
        string FormType, CreatedBy, ModifiedBy;
        int ActiveStatus;
        DateTime DateOfCreation, DateOfModification;
        FormType = txtFormType.Text;
            ActiveStatus = 1;

        if (ViewState["Mode"].ToString() == "ADD")
        {
            DateOfCreation = DateTime.Now;
            CreatedBy = Session["userid"].ToString();
            objFMS.FMS_Insert_FormType(FormType,UDFLib.ConvertToInteger(CreatedBy));
            string js2 = "alert('Form Type Saved Successfully');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
        }
        else if (ViewState["Mode"].ToString() == "EDIT")
        {
            int ID = Convert.ToInt32(ViewState["FormTypeID"].ToString());
            DateOfModification = DateTime.Now;
            ModifiedBy = Session["userid"].ToString();
            objFMS.FMS_Update_FormType(ID,FormType, UDFLib.ConvertToInteger(ModifiedBy));
            string js2 = "alert('Form Type Updated Successfully');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
        }

        BindFormType();
        ViewState["FormTypeID"] = "";
        ViewState["Mode"] = "";
        txtFormType.Text = "";
       

        // ddlColor.SelectedItem.Text = "--SELECT--";
     
        updCat.Update();
        ViewState["Mode"] = "ADD";
    }
    protected void ImgEditFormType_Click(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument.ToString());

        DataSet ds = new DataSet();
        ds = objFMS.FMS_Get_FormTypeByID(ID);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtFormType.Text = ds.Tables[0].Rows[0]["FormType"].ToString();
          
          
          
        }

        ViewState["Mode"] = "EDIT";
        ViewState["FormTypeID"] = ID;
       
    }
    protected void ImgDeleteFormType_Click(object sender, CommandEventArgs e)
    {

        int ID = Convert.ToInt32(e.CommandArgument.ToString());
        objFMS.FMS_Delete_FormType(ID,UDFLib.ConvertToInteger(Session["userid"].ToString()));
        BindFormType();
    }

    protected void ImgRestoreFormType_Click(object sender, CommandEventArgs e)
    {


        int ID = Convert.ToInt32(e.CommandArgument.ToString());
        objFMS.FMS_Restore_FormType(ID, UDFLib.ConvertToInteger(Session["userid"].ToString()));
        BindFormType();
    }


    protected void lnkFormType_Click(object sender, CommandEventArgs e)
    {

        int ID = Convert.ToInt32(e.CommandArgument.ToString());

        DataSet ds = new DataSet();
        ds = objFMS.FMS_Get_FormTypeByID(ID);

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtFormType.Text = ds.Tables[0].Rows[0][1].ToString();
        }
    }
}