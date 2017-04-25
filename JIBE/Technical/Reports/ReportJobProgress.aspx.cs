using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Technical;
using System.Data;


public partial class ReportJobProgress : System.Web.UI.Page
{

    BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string strCommand = "";

            if (Session["strQuery"] != null)
            {
                strCommand = Session["strQuery"].ToString();

                DataTable dtSearchResult = objBLL.Get_FilterWorklist(strCommand).Tables[0];

                rpt1.DataSource = dtSearchResult;
                rpt1.DataBind();

                if (Request.QueryString["Export"] != null)
                {
                    pnlAddFollowUp.Visible = false;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=JobProgress.xls;");
                }
            }
            lblDt.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }
        catch (Exception ex)
        {
            string js = "alert('Error in loading data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void rpt1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        try
        {
            RepeaterItem item = e.Item;
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater childRepeater = (Repeater)item.FindControl("rpt2");
                int OFFICE_ID = int.Parse(DataBinder.Eval(e.Item.DataItem, "OFFICE_ID").ToString());
                int WORKLIST_ID = int.Parse(DataBinder.Eval(e.Item.DataItem, "WORKLIST_ID").ToString());
                int VESSEL_ID = int.Parse(DataBinder.Eval(e.Item.DataItem, "VESSEL_ID").ToString());

                DataTable dtFollowUps = objBLL.Get_FollowupList(OFFICE_ID, VESSEL_ID, WORKLIST_ID);

                childRepeater.DataSource = dtFollowUps;
                childRepeater.DataBind();
            }
        }
        catch { }
    }

    protected void rpt1_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument.ToString() != "")
            {
                string[] arg = e.CommandArgument.ToString().Split(',');

                if (e.CommandName == "Add_FollowUp")
                {
                    hdnVesselID.Value = arg[0];
                    hdnWorklistlID.Value = arg[1];
                    hdnOfficeID.Value = arg[2];

                    string js = "OpenFollowupDiv();";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenFollowup", js, true);

                }
            }


        }
        catch
        {
        }
    }

    protected void rpt2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (Request.QueryString["Export"] != null)
        {
            LinkButton lnkAddFollowUp = (LinkButton)e.Item.FindControl("lnkAddFollowUp");
            if (lnkAddFollowUp != null)
                lnkAddFollowUp.Visible = false;

        }

    }


    protected void btnSaveFollowUpAndClose_OnClick(object sender, EventArgs e)
    {
        try
        {
            int iJob_OfficeID = int.Parse(hdnOfficeID.Value);
            int Worklist_ID = int.Parse(hdnWorklistlID.Value);
            int VESSEL_ID = int.Parse(hdnVesselID.Value);

            string FOLLOWUP = txtMessage.Text;
            int CREATED_BY = int.Parse(Session["USERID"].ToString());
            int TOSYNC = 1;

            int newFollowupID = objBLL.Insert_Followup(iJob_OfficeID, Worklist_ID, VESSEL_ID, FOLLOWUP, CREATED_BY, TOSYNC);

            string strCommand = "";
            strCommand = Session["strQuery"].ToString();

            DataTable dtSearchResult = objBLL.Get_FilterWorklist(strCommand).Tables[0];

            rpt1.DataSource = dtSearchResult;
            rpt1.DataBind();
        }
        catch (Exception ex)
        {
            string js = "alert('Error in saving data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorsaving", js, true);
        }

    }

}