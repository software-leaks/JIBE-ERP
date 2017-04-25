using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using System.Data;

public partial class Infrastructure_Libraries_ReqsnTypeBudgetAssignment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            BindItems();

    }

    protected void btnSrach_Click(object s, EventArgs e)
    {
        BindItems();
    }
    protected void gvReqsnType_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        BLL_PURC_Common.Upd_Reqsn_Type_Budget("",Convert.ToInt32((gvReqsnType.Rows[e.RowIndex].FindControl("ddlBudgetCode") as DropDownList).SelectedValue),
            Convert.ToInt32(gvReqsnType.DataKeys[e.RowIndex].Values["Reqsn_Type_Code"]),
            UDFLib.ConvertIntegerToNull(gvReqsnType.DataKeys[e.RowIndex].Values["ID"]),
           Convert.ToInt32(Session["userid"]));
        gvReqsnType.EditIndex = -1;
        BindItems();

    }
    protected void gvReqsnType_RowEditing(object sender, GridViewEditEventArgs e)
    {
       

        gvReqsnType.EditIndex = e.NewEditIndex;
        BindItems();
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        DataTable dtBgtCode = objTechService.SelectBudgetCode().Tables[0];

        DropDownList ddlBudgetCode = (gvReqsnType.Rows[e.NewEditIndex].FindControl("ddlBudgetCode") as DropDownList);

        ddlBudgetCode.DataSource = dtBgtCode;
        ddlBudgetCode.DataTextField = "Budget_Name";
        ddlBudgetCode.DataValueField = "Budget_Code";
        ddlBudgetCode.DataBind();
        ddlBudgetCode.Items.Insert(0, new ListItem("Select", "0"));

        if (!string.IsNullOrWhiteSpace(Convert.ToString(gvReqsnType.DataKeys[e.NewEditIndex].Values["Budget_Code"])))
        {
            ddlBudgetCode.ClearSelection();
            ListItem libgt = ddlBudgetCode.Items.FindByValue(Convert.ToString(gvReqsnType.DataKeys[e.NewEditIndex].Values["Budget_Code"]));
            if (libgt != null)
                libgt.Selected = true;
        }
        else
        {
            ddlBudgetCode.SelectedIndex = 0;
        }
    }
    protected void gvReqsnType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvReqsnType.EditIndex = -1;
        BindItems();
    }

    protected void gvReqsnType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        BLL_PURC_Common.Upd_Reqsn_Type_Budget("DELETE", 0, 0, UDFLib.ConvertIntegerToNull(gvReqsnType.DataKeys[e.RowIndex].Values["ID"]), Convert.ToInt32(Session["userid"]));
        BindItems();
    }

    protected void BindItems()
    {
        gvReqsnType.DataSource = BLL_PURC_Common.Get_Reqsn_Type_Budget(UDFLib.ConvertStringToNull(txtSearch.Text));
        gvReqsnType.DataBind();
    }



}