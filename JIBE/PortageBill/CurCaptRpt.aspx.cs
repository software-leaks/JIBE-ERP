using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PortageBill;
using System.Data;

public partial class PortageBill_CurCaptRpt : System.Web.UI.Page
{

    MergeGridviewHeader_Info objItemColumn = new MergeGridviewHeader_Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!string.IsNullOrEmpty(Request.QueryString["Vessel_ID"]))
        {
            DataSet dsCapt = BLL_PB_PortageBill.ReportDetailsByID_Cur(Request.QueryString["ID"], Request.QueryString["Vessel_ID"]);
            if (dsCapt.Tables[0].Rows.Count > 0)
            {
                lblVessel.Text = dsCapt.Tables[0].Rows[0]["Vessel_Name"].ToString();
                lblFrom.Text = dsCapt.Tables[0].Rows[0]["from_date"].ToString();
                lblTo.Text = dsCapt.Tables[0].Rows[0]["To_date"].ToString();
                lblOpnBL.Text = dsCapt.Tables[0].Rows[0]["Opening_Bal"].ToString();
                //  lblClosing.Text = dsCapt.Tables[0].Rows[0]["Closing_Bal"].ToString();
                lblClosing.Text = (Convert.ToDouble(dsCapt.Tables[0].Rows[0]["Opening_Bal"].ToString()) + Convert.ToDouble(dsCapt.Tables[0].Rows[0]["Total_Recp_Amount"].ToString()) - Convert.ToDouble(dsCapt.Tables[0].Rows[0]["Total_Payment_Amount"].ToString())).ToString();

                objItemColumn.AddMergedColumns(new int[] { 5, 6 }, "Amount", "HeaderStyle-css");

                gvCastCash.DataSource = dsCapt.Tables[0];
                gvCastCash.DataBind();


                gvCastCash.FooterRow.Cells[4].Text = "USD";
                gvCastCash.FooterRow.Cells[3].Text = "Total :-";
                gvCastCash.FooterRow.Cells[4].Style.Add("text-align", "center");
                gvCastCash.FooterRow.Cells[3].Style.Add("text-align", "center");
                gvCastCash.FooterRow.Cells[3].Style.Add("font-weight", "bold");
                gvCastCash.FooterRow.Cells[4].Style.Add("font-weight", "bold");
                gvCastCash.FooterRow.Cells[5].Style.Add("font-weight", "bold");
                gvCastCash.FooterRow.Cells[6].Style.Add("font-weight", "bold");



                (gvCastCash.FooterRow.FindControl("lblRcvdTotal") as Label).Text = dsCapt.Tables[0].Rows[0]["Total_Recp_Amount"].ToString();
                (gvCastCash.FooterRow.FindControl("lblPaidTotal") as Label).Text = dsCapt.Tables[0].Rows[0]["Total_Payment_Amount"].ToString();
            }
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

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void gvCastCash_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objItemColumn);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);

        }
    }
}