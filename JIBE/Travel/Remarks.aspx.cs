//system libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//custom libraries
using SMS.Business;
using SMS.Business.TRAV;

public partial class Remarks : System.Web.UI.Page
{
    int requestID, UserID;
    protected void Page_Load(object sender, EventArgs e)
    {
        requestID = Convert.ToInt32(Request.QueryString["requestid"].ToString());
        UserID = Convert.ToInt32(Session["USERID"].ToString());

        if (!IsPostBack)
        {
            try
            {
                ltRequestid.Text = Request.QueryString["requestid"].ToString();
                objRemarks.SelectParameters["requestID"].DefaultValue = requestID.ToString();
                objRemarks.SelectParameters["AgentID"].DefaultValue = UserID.ToString();
                grdRemarks.DataBind();
            }
            catch { }
        }
    }

    protected void cmdSaveRemark_Click(object sender, EventArgs e)
    {
        BLL_TRV_TravelRequest tr = new BLL_TRV_TravelRequest();
        try
        {
            tr.AddRemarks(requestID, txtRemarks.Text, Convert.ToInt32(Session["USERID"].ToString()), UserID);
            grdRemarks.DataBind();
        }
        catch { }
        finally { tr = null; }
    }

    protected void GrdRemarks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "PrevColor=this.style.backgroundColor; this.style.color='red'; this.style.backgroundColor='#AFCFEE'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=PrevColor; this.style.color='#4A3C8C'");
            }
        }
        catch { }
    }
}