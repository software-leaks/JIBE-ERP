using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using System.Data;

public partial class Purchase_PURC_Bulk_Purchase_Reqsn : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.Page.Header.Controls.Add(SetUserStyle.AddThemeInHeader());
            base.OnInit(e);
        }
        catch { }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {

            BindData();

            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("Statusvalue");
            dtStatus.Columns.Add("statustext");
            DataRow drdf = dtStatus.NewRow();
            drdf["Statusvalue"] = "0";
            drdf["statustext"] = "Draft";
            DataRow drfn = dtStatus.NewRow();
            drfn["Statusvalue"] = "1";
            drfn["statustext"] = "Closed";
            dtStatus.Rows.Add(drfn);
            dtStatus.Rows.Add(drdf);

            ucCustomDropDownListstatus.DataSource = dtStatus;
            ucCustomDropDownListstatus.DataTextField = "statustext";
            ucCustomDropDownListstatus.DataValueField = "Statusvalue";
            ucCustomDropDownListstatus.DataBind();
        }



    }

    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {


        }
        if (objUA.Edit == 0)
        {


        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {


        }


    }

    protected void BindData()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        int? Finalized = null;
        if (ucCustomDropDownListstatus.SelectedValues.Rows.Count == 1)
        {
            Finalized = Convert.ToInt32(ucCustomDropDownListstatus.SelectedValues.Rows[0][0]);
        }

        DataTable dt = BLL_PURC_Common.Get_Bulk_Purchase_Reqsn((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
            , UDFLib.ConvertStringToNull(Session["sDeptType"]), (DataTable)Session["sDeptCode"]
            , UDFLib.ConvertIntegerToNull(Session["ReqsnType"].ToString()), UDFLib.ConvertStringToNull(Session["REQNUM"].ToString()), Finalized
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        gvBulkPurchase.DataSource = dt;
        gvBulkPurchase.DataBind();


        if (ucCustomDropDownListstatus.SelectedValues.Rows.Count > 0)
        {

            (gvBulkPurchase.HeaderRow.FindControl("imgStatusFilter") as Image).ImageUrl = "../Images/filter-add-icon-green.png";
        }
        string script = " var height = document.body.scrollHeight;parent.ResizeFromChild(height,'1');";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "resize" + DateTime.Now.Millisecond.ToString(), script, true);

    }


    protected void btnRollbackAtDrfat_Click(object s, EventArgs e)
    {
        int sts = BLL_PURC_Common.UPD_RollBack_Bulk_Reqsn((s as ImageButton).CommandArgument, Convert.ToInt32(Session["userid"]));
        if (sts > 0)
        {
            BindData();
        }
        else
        {
            string script = " alert('failed to roll back !')";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg" + DateTime.Now.Millisecond.ToString(), script, true);
        }
    }
}