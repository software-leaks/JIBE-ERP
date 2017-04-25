using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.TRAV;

public partial class Travel_TravelRequest_Approve_Mobile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BLL_TRV_TravelRequest obj=new BLL_TRV_TravelRequest();
            DataSet ds=obj.Get_QuoateForApprovals(Convert.ToInt32(Request.QueryString["RequestID"]));
            gvQuotations.DataSource = ds.Tables[1];
            gvQuotations.DataBind();
            lblVesselName.Text = ds.Tables[0].Rows[0]["vesselName"].ToString();
            lblPaxName.Text = ds.Tables[0].Rows[0]["Pax"].ToString();
        }
    }

    protected void btnApprove_Click(object s, EventArgs e)
    {
        BLL_TRV_TravelRequest obj = new BLL_TRV_TravelRequest();
        obj.Upd_Approve_TravelPO_Mob(1);
        gvQuotations.Visible = false;
        string msgmodal = String.Format(" alert('Quotation has been approved successfully');window.open('','_self','');self.close();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalFinalApproved", msgmodal, true);
    }
    protected void gvQuotations_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "Sent_For_Approval").ToString() == "1")
            {
                //e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
        }
    }
}