using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.PURC;

public partial class Purchase_Provision_Type : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    public UserAccess objUA = new UserAccess();
    BLL_PURC_Provision objProvision = new BLL_PURC_Provision();
    public string OperationMode = "";
    protected void Page_Load(object sender, EventArgs e)
    {


        UserAccessValidation();
        if (!IsPostBack)
        {
            BindProvisionType();
            FillProvisionType();
        }

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
    public void BindProvisionType()
    {
        DataSet ds = BLL_PURC_Provision.PURC_GET_SUBSYSTEMCODE_PROVISIONTYPE(Convert.ToInt32(Session["USERID"]));

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvProvision_Type.DataSource = ds.Tables[0];
            gvProvision_Type.DataBind();
        }
        else
        {
            gvProvision_Type.DataSource = ds.Tables[0];
            gvProvision_Type.DataBind();
        }
    }
    protected void gvProvision_Type_RowEditing(object sender, GridViewEditEventArgs de)
    {
        GridView _gridView = (GridView)sender;
        _gridView.EditIndex = de.NewEditIndex;
        _gridView.SelectedIndex = de.NewEditIndex;
        BindProvisionType();
    }
    protected void gvProvision_Type_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        _gridView.EditIndex = -1;
        BindProvisionType();
    }
    protected void gvProvision_Type_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int nCurrentRow = e.RowIndex;
        _gridView.EditIndex = -1;
        BindProvisionType();
    }
    protected void gvProvision_Type_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        int nCurrentRow = Int32.Parse(e.CommandArgument.ToString());

        if (e.CommandName.ToUpper().Equals("UPDATE"))
        {

            Label lblPROVIID = (Label)_gridView.Rows[nCurrentRow].FindControl("lblProviID");
            DropDownList ddlProvisionType = (DropDownList)_gridView.Rows[nCurrentRow].FindControl("ddlProvisionType");

            ProvisionTypeUpdate(lblPROVIID.Text, ddlProvisionType.SelectedValue);
        }
        else if (e.CommandName.ToUpper().Equals("DELETE"))
        {
            Label lblPROVIID = (Label)_gridView.Rows[nCurrentRow].FindControl("lblProviID");
            ProvisionTypeDelete(lblPROVIID.Text, null);

        }
        FillProvisionType();
        BindProvisionType();

    }
    private void ProvisionTypeInsert(int SubSystemCode, string ProvisionType)
    {
        BLL_PURC_Provision.PURC_INS_SUBSYSTEMCODE_PROVISIONTYPE(SubSystemCode, ProvisionType, Convert.ToInt32(Session["USERID"]));
    }
    private void ProvisionTypeUpdate(string PROVIID, string ProvisionType)
    {
        BLL_PURC_Provision.PURC_UPDATE_SUBSYSTEMCODE_PROVISIONTYPE(Convert.ToInt32(PROVIID), ProvisionType, Convert.ToInt32(Session["USERID"]));
    }
    private void ProvisionTypeDelete(string PROVIID, string ProvisionType)
    {
        BLL_PURC_Provision.PURC_DELETE_SUBSYSTEMCODE_PROVISIONTYPE(Convert.ToInt32(PROVIID), ProvisionType, Convert.ToInt32(Session["USERID"]));
    }
    protected void ImgAdd_Click(object sender, ImageClickEventArgs e)
    {

        HiddenFlag.Value = "Add";

        OperationMode = "Add Provision SubSystem";
        lblError.Text = "";
        ddlProviType.SelectedIndex = 0;
        ddlSubSystem.SelectedIndex = 0;

        string AddSubModal = String.Format("showModal('divAddItem',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSubModal", AddSubModal, true);

    }
    private void FillProvisionType()
    {
        DataSet ds = BLL_PURC_Provision.PURC_GET_PROVISIONTYPE();

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlSubSystem.DataSource = ds.Tables[0];
            ddlSubSystem.DataTextField = Convert.ToString(ds.Tables[0].Columns["Subsystem_Description"]);//Subsystem_Description
            ddlSubSystem.DataValueField = Convert.ToString(ds.Tables[0].Columns["ID"]);//ID
            ddlSubSystem.DataBind();
        }
        ddlSubSystem.Items.Insert(0, "--SELECT--");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ProvisionTypeInsert(Convert.ToInt32(ddlSubSystem.SelectedValue), Convert.ToString(ddlProviType.SelectedValue));
        string hideSubModal = String.Format("hideModal('divAddItem',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hideSubModal", hideSubModal, true);
        ddlSubSystem.Items.Clear();
        FillProvisionType();
        BindProvisionType();
    }
    protected void gvProvision_Type_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}