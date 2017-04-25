//System Libarary
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//Custom Library
using SMS.Business.Crew;
using SMS.Properties;
using SMS.Business;
using SMS.Business.TRAV;
using SMS.Business.Infrastructure;

public partial class RequestListAgent : System.Web.UI.Page
{
    public UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    protected string CurrentStatus, status = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserAccessValidation();



            hdf_UserID.Value = Session["USERID"].ToString();
            if (!IsPostBack)
            {
                BLL_Infra_VesselLib objInfra = new BLL_Infra_VesselLib();
                ddlVessel_Manager.DataSource = objInfra.Get_VesselManagerList();
                ddlVessel_Manager.DataTextField = "COMPANY_NAME";
                ddlVessel_Manager.DataValueField = "ID";
                ddlVessel_Manager.DataBind();
                ddlVessel_Manager.Items.Insert(0, new ListItem("-Select All-", "0"));
                ViewState["Status"] = "RFQ RECEIVED";
                BindTravelAgentRequests();

                txtSelMenu.Value = LinkmenuNew.ClientID;
                BLL_TRV_QuoteRequest objtrequser = new BLL_TRV_QuoteRequest();
                ListItem liSlt = new ListItem();
                liSlt.Text = "--ALL--";
                liSlt.Value = "0";
                liSlt.Selected = true;

                ddlQuotedBy.DataSource = objtrequser.Get_Quoted_By_Agent(Convert.ToInt32(Session["USERID"]));
                ddlQuotedBy.DataBind();
                ddlQuotedBy.Items.Insert(0, liSlt);
                ddlRequestedBy.DataSource = objtrequser.Get_Requestor_By_Agent(Convert.ToInt32(Session["USERID"]));
                ddlRequestedBy.DataBind();
                ddlRequestedBy.Items.Insert(0, liSlt);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "selectNode", "selMe('" + txtSelMenu.Value + "');", true);
            }


            string js1 = "bindPaxsName();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "bindPaxsName", js1, true);
        }
        catch { throw; }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "selectNode", "selMe('" + txtSelMenu.Value + "');", true);
    }


    protected void BindTravelAgentRequests()
    {

        BLL_TRV_TravelRequest treq = new BLL_TRV_TravelRequest();
        DataSet ds = new DataSet();
        int rowcount = ucCustomPagerItems.isCountRecord;


        string status = ViewState["Status"].ToString();

        int VCode = 0;
        if (!String.IsNullOrEmpty(cmbVessel.SelectedValue))
            VCode = Convert.ToInt32(cmbVessel.SelectedValue);


        ds = treq.Get_TravelRequests_Agent(Convert.ToInt32(Session["USERID"].ToString()),
                    Convert.ToInt32(cmbFleet.SelectedValue), VCode,
                txtSectorFrom.Text, txtSectorTo.Text, txtTrvDateFrom.Text,
                                txtTrvDateTo.Text, txtPaxName.Text, status, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, UDFLib.ConvertIntegerToNull(ddlVessel_Manager.SelectedValue), UDFLib.ConvertIntegerToNull(ddlQuotedBy.SelectedValue), UDFLib.ConvertIntegerToNull(ddlRequestedBy.SelectedValue));


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        rptParent.DataSource = ds;
        rptParent.DataBind();

    }


    protected void rptParent_OnItemDataBound(object source, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            System.Data.DataRowView drv = (System.Data.DataRowView)e.Item.DataItem;
            CurrentStatus = drv["CurrentStatus"].ToString().ToUpper();
            string current_Status = drv["current_Status"].ToString();

            Label lblReqstID = (Label)e.Item.FindControl("lblReqstID");
            ImageButton imgMarkTraveled = (ImageButton)e.Item.FindControl("imgMarkTraveled");

            imgMarkTraveled.Attributes.Add("onmousemove", "return GetPaxsName('" + lblReqstID.Text.Trim() + "');");

            imgMarkTraveled.Attributes.Add("onclick", "return OnTravelledFlagUpdate('" + imgMarkTraveled.ClientID + "' , '" + lblReqstID.Text.Trim() + "');");

            if (current_Status == "APPROVED" || current_Status == "ISSUED" || current_Status=="TRAVELLED" || current_Status == "REFUND RECEIVED" || current_Status == "REFUND CLOSED" || current_Status == "REFUND")
            {
                (e.Item.FindControl("imgPODetails") as Image).Visible = true;
            }
            else
            {
                (e.Item.FindControl("imgPODetails") as Image).Visible = false;
            }

            //imgMarkTraveled.Attributes.Add("onclick", "GetPaxsName_OnConfirm('" + lblReqstID.Text.Trim() + "' ,'" + imgMarkTraveled.ClientID + "');return false;"); 

            //imgMarkTraveled.Attributes.Add("onclick", "GetPaxsName('" + lblReqstID.Text.Trim() + "'); var funname='OnTravelledFlagUpdate(&#39;" + imgMarkTraveled.ClientID + "&#39; , &#39;" + lblReqstID.Text.Trim() + "&#39;)';setTimeout(funname,100);return false;");


        }
    }

    protected void cmbFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            BindTravelAgentRequests();

        }
        catch { }
    }
    protected void cmbVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindTravelAgentRequests();
        }
        catch { }
    }

    protected void cmbVessel_OnDataBound(object source, EventArgs e)
    {
        cmbVessel.Items.Insert(0, new ListItem("-Select All-", "0"));
    }



    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

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

    protected void NavMenu_Click(object sender, EventArgs e)
    {
        string Selection = ((LinkButton)sender).CommandArgument;
        ViewState["Selection"] = Selection;
        ucCustomPagerItems.CurrentPageIndex = 1;
        txtSelMenu.Value = ((LinkButton)sender).ClientID;

        switch (Selection)
        {
            case "NEW":
                status = "RFQ RECEIVED";

                ViewState["Status"] = status;
                BindTravelAgentRequests();


                break;
            case "RFQ":
                status = "QUOTE SENT";

                ViewState["Status"] = status;
                BindTravelAgentRequests();


                break;
            case "APP":
                status = "APPROVED";

                ViewState["Status"] = status;
                BindTravelAgentRequests();

                break;
            case "RCL":
                status = "CLOSED";

                ViewState["Status"] = status;
                BindTravelAgentRequests();


                break;
            case "TKT":
                status = "ISSUED";

                ViewState["Status"] = status;
                BindTravelAgentRequests();

                break;

            case "TRV":
                status = "TRAVELLED";

                ViewState["Status"] = status;
                BindTravelAgentRequests();

                break;

            case "REF":
                status = "REFUND RECEIVED";

                ViewState["Status"] = status;
                BindTravelAgentRequests();

                break;

            case "REC":
                status = "REFUND CLOSED";

                ViewState["Status"] = status;
                BindTravelAgentRequests();
                break;


            case "CEN":
                status = "CANCELLED";

                ViewState["Status"] = status;
                BindTravelAgentRequests();

                break;




            case "ALL":
                status = "";

                ViewState["Status"] = status;
                BindTravelAgentRequests();
                break;
        }

    }



    protected void imgMarkTraveled_click(object sender, CommandEventArgs e)
    {

        BLL_TRV_TravelRequest treq = new BLL_TRV_TravelRequest();
        int retval = treq.Update_Travel_Flag(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"].ToString()));

        BindTravelAgentRequests();

    }

    protected void ddlVessel_Manager_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTravelAgentRequests();
    }


    protected void objFleet_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        cmbFleet.Items.Clear();
        ListItem li = new ListItem("-Select All-", "0");
        cmbFleet.Items.Add(li);

    }

    protected void btnSearchRequest_Click(object s, EventArgs e)
    {
        BindTravelAgentRequests();
    }



}