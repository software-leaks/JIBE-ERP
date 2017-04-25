using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Text;
using SMS.Business.PortageBill;


public partial class PortageBill_NewCTMRequest : System.Web.UI.Page
{
    double ReqTotal = 0;
    decimal CTMTotal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UserAccessValidation();
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
            Response.Redirect("~/default.aspx?msgid=1");
        }

        if (objUA.Edit == 0)
        {

        }
        if (objUA.Delete == 0)
        {

        }
        if (objUA.Approve == 0)
        {

        }
    }

    public string GetTotal(string Denomination, string NoOfNotes)
    {
        decimal Res = (UDFLib.ConvertToDecimal(Denomination) * UDFLib.ConvertToDecimal(NoOfNotes));
        return Res.ToString("$ ###,##0.00");
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
        {
            Session.Abandon();
            Response.Redirect("~/Account/Login.aspx");
            return 0;
        }
    }

    protected void gvDenominations_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            int fieldValue = int.Parse(dataItem["ReqTotal"].Text);
            ReqTotal += fieldValue;
        }
        if (e.Item is GridFooterItem)
        {
            GridFooterItem footerItem = e.Item as GridFooterItem;
            footerItem["ReqTotal"].Text = "total: " + ReqTotal.ToString();

        }


    }
    protected void txtPect_TextChanged(object sender, EventArgs e)
    {

    }


    private void gvDenominations_BindData()
    {
        int Vessel_ID = 0;
        int CTM_ID = 0;
        int Office_ID = 0;

        if (Request.QueryString["Vessel_ID"] != null)
            Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"].ToString());

        if (Request.QueryString["ID"] != null)
            CTM_ID = UDFLib.ConvertToInteger(Request.QueryString["ID"].ToString());

        if (Request.QueryString["Office_ID"] != null)
            Office_ID = UDFLib.ConvertToInteger(Request.QueryString["Office_ID"].ToString());

        DataTable dt = BLL_PB_PortageBill.Get_CTM_Denominations(Vessel_ID, CTM_ID, Office_ID);

        gvDenominations.DataSource = dt;
        gvDenominations.DataBind();
    }

    protected void gvDenominations_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvDenominations.EditIndex = e.NewEditIndex;
            gvDenominations_BindData();

        }
        catch
        {
        }
    }
    protected void gvDenominations_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvDenominations.EditIndex = -1;
            gvDenominations_BindData();

        }
        catch
        {
        }


    }
    protected void gvDenominations_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int Vessel_ID = 0;
            int CTM_ID = 0;
            int Office_ID = 0;

            if (Request.QueryString["Vessel_ID"] != null)
                Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"].ToString());

            if (Request.QueryString["ID"] != null)
                CTM_ID = UDFLib.ConvertToInteger(Request.QueryString["ID"].ToString());

            if (Request.QueryString["Office_ID"] != null)
                Office_ID = UDFLib.ConvertToInteger(Request.QueryString["Office_ID"].ToString());

            int ID = UDFLib.ConvertToInteger(gvDenominations.DataKeys[e.RowIndex].Value.ToString());
            int Denomination = UDFLib.ConvertToInteger(e.NewValues["Denomination"].ToString());
            int Notes_Office = UDFLib.ConvertToInteger(e.NewValues["NoOfNotes_by_Office"].ToString());

            int Res = BLL_PB_PortageBill.UPDATE_CTM_Denominations(Vessel_ID, CTM_ID, Office_ID, ID, Notes_Office,Denomination, GetSessionUserID());

            gvDenominations.EditIndex = -1;

            gvDenominations_BindData();
        }
        catch
        {

        }

    }

    protected void gvCTMCalculations_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal rowTotal = UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "CashCategory_Amt").ToString());
            CTMTotal = CTMTotal + rowTotal;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //Label lbl = (Label)e.Row.FindControl("lblTotal");
            lblCTMTotal.Text = CTMTotal.ToString("$ ###,##0.00");
        }
    }

   

    

    


    
}

