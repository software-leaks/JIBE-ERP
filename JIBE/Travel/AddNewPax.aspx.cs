//system libararies
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

//custom libaries
using SMS.Business.Crew;
using SMS.Properties;
using SMS.Business;
using SMS.Business.TRAV;
using SMS.Business.Infrastructure;


public partial class AddNewPax : System.Web.UI.Page
{
    int RequestID;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            RequestID = Convert.ToInt32(Request.QueryString["requestid"].ToString());
        }
        catch { }
    }

    protected void cmdGet_Click(object sender, EventArgs e)
    {
        BLL_TRV_TravelRequest objTrv = new BLL_TRV_TravelRequest();

        DataTable dt = objTrv.Get_SearchCrew(0, 0, 0, UDFLib.ConvertToInteger(Session["USERID"].ToString()), txtSearch.Text).Tables[0];

        GrdCrew.DataSource = dt;

        GrdCrew.DataBind();
    }

    protected void GrdCrew_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        UserAccess UA = new UserAccess();
        try
        {
            if (e.CommandName == "AddPax")
            {
                BLL_TRV_TravelRequest Req = new BLL_TRV_TravelRequest();

                Req.AddPaxToTravelRequest(RequestID, Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"].ToString()), 0, 0);

                string js = "alert('Pax has been added successfully.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ALR", js, true);

                //Response.Write("<script type='text/javascript'>alert('Pax has been added successfully.');</script>");
                Req = null;
            }
        }
        catch { }
        finally { UA = null; }
    }

}