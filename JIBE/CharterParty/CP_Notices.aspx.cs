using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using System.Text;

using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.CP;


public partial class CP_Notices : System.Web.UI.Page
{

    BLL_CP_CharterParty oBLL_CP = new BLL_CP_CharterParty();
    BLL_CP_Openings oBLL_Openings = new BLL_CP_Openings();
    UserAccess objUA = new UserAccess();
    public string Type = null;
    private int VesselId = 0;
    public string OperationMode = "";
    public string CurrStatus = null;
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();


        if (!IsPostBack)
        {
            BindVessels();

            BindGrid();
        }

    }


    protected void BindVessels()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        DataTable dt = oBLL_Openings.GetVesselListAll(UserCompanyID);

        ddlvessel.DataSource = dt;
        ddlvessel.DataTextField = "VESSEL_NAME";
        ddlvessel.DataValueField = "VESSEL_ID";
        ddlvessel.DataBind();
        ddlvessel.Items.Insert(0, new ListItem("-Select-", "0"));
    }


    public void BindGrid()
    {
        try
        {

            int rowcount = ucCustomPager1.isCountRecord;
            //string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            //int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            VesselId = Convert.ToInt32(ddlvessel.SelectedValue);

            DataTable dt = oBLL_CP.Redelivery_GetDetail(VesselId, ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount);


            if (ucCustomPager1.isCountRecord == 1)
            {
                ucCustomPager1.CountTotalRec = rowcount.ToString();
                ucCustomPager1.BuildPager();
            }

            if (dt.Rows.Count > 0)
            {
                gvNotices.DataSource = dt;
                gvNotices.DataBind();
            }
        }
        catch { }
    }



    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            // btnsave.Visible = false;

            if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void gvNotices_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindGrid();

    }
    protected void gvNotices_Rowdatabound(object sender, GridViewRowEventArgs e)
    {
            if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            Label lblRedelivery = (Label)e.Row.FindControl("lblRedelivery");

            Button btnOpenings = (Button)e.Row.FindControl("btnOpenings");
                
            HtmlTableRow minmaxnotice = (HtmlTableRow)e.Row.FindControl("minmaxnotice"); 
            bool ReDeliveryExceeds = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "ExceedRedelivery"));
            if (ReDeliveryExceeds)
            {
                lblRedelivery.ForeColor = System.Drawing.Color.Red;

            }

            bool CurrentRedelivery = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "CurrentRedelivery"));
            int OpeningCount = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Opening_Count"));
            if (OpeningCount > 0)
            {
                btnOpenings.Text =  OpeningCount.ToString() ;
                btnOpenings.Visible = true;
            }
            else
            {
                btnOpenings.Visible = false;
            }
            if (CurrentRedelivery)
            {
                if ((DataBinder.Eval(e.Row.DataItem, "Est_ReDelivery") == null || DataBinder.Eval(e.Row.DataItem, "Est_ReDelivery").ToString() == "") && DataBinder.Eval(e.Row.DataItem, "Extention_Period") == null || DataBinder.Eval(e.Row.DataItem, "Extention_Period").ToString() == "")
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                    minmaxnotice.Attributes.Add("style", "color: #FF0000;");
                }
                else if(DataBinder.Eval(e.Row.DataItem, "Est_ReDelivery")!=null && DataBinder.Eval(e.Row.DataItem, "Est_ReDelivery").ToString() !="")
                {
                     lblStatus.Text = "Redelivery Notice received";
                    
                }
                else
                    lblStatus.Text = "Extended";
            }

        }

        
  }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPager1.isCountRecord;
        VesselId = Convert.ToInt32(ddlvessel.SelectedValue);

        DataTable dt = oBLL_CP.Redelivery_GetDetail(VesselId, ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount);
        string[] HeaderCaptions = { "Vessel Name", "Charterer", "Redelivery Period From", "TO", "Notice Days", "Notice Period From", "To","Earliest Delivery","Redelivery Date" };
        string[] DataColumnsName = { "Vessel_Name", "Charterer", "Redelivery_Min_Date", "Redelivery_Max_Date", "Notice_Days", "MinReDeliveryNotice", "MaxReDeliveryNotice", "EarliestRedelivery", "Est_ReDelivery" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "RedeliveryNotice.xls", "Redelivery Notice");

    }

    protected void ibtnOpenings_Click(object sender, EventArgs e)
    {


            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            int  Vessel_Id = Convert.ToInt32(gvNotices.DataKeys[row.RowIndex].Value);
            int rowcount = 0;

            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("Status");
            dtStatus.Rows.Add("Enquiry");
            dtStatus.Rows.Add("Response");
            dtStatus.Rows.Add("Negotiation");
            dtStatus.Rows.Add("Approval");
            dtStatus.Rows.Add("Suspend");

            
          

            DataTable dt = oBLL_Openings.GetOpeningList(dtStatus, ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, Vessel_Id, ref  rowcount);


            if (ucCustomPager2.isCountRecord == 1)
            {
                ucCustomPager2.CountTotalRec = rowcount.ToString();
                ucCustomPager2.BuildPager();
            }

            gvNotices.SelectedIndex = row.RowIndex;

            gvOpenings.DataSource = dt;
            gvOpenings.DataBind();
            gvOpeningHistory.Visible = false;
            gvOpenings.Visible = true;
            ucCustomPager2.Visible = true;
        }


    protected void ImgView_click(object sender, EventArgs e)
    {


        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        HiddenField hdnVessel_Id = (HiddenField)gvr.FindControl("hdnVesselId");
        HiddenField hdnOpening_Id = (HiddenField)gvr.FindControl("hdnOpeningId");
        gvOpenings.SelectedIndex = gvr.RowIndex;
        Session["Vessel_Id"] = hdnVessel_Id.Value;
        Session["Opening_Id"] = hdnOpening_Id.Value;
        BindGridHistory();
    }

    protected void BindGridHistory()
    {
        int Opening_Id = UDFLib.ConvertToInteger(Session["Opening_Id"] );
        int Vessel_Id = UDFLib.ConvertToInteger(Session["Vessel_Id"]);
        DataTable dtHistory = oBLL_Openings.Get_OpeningDetails(Opening_Id, Vessel_Id).Tables[1];

        gvOpeningHistory.DataSource = dtHistory;
        gvOpeningHistory.DataBind();

        gvOpeningHistory.Visible = true;
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
      
        BindGrid();
    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ddlvessel.SelectedValue = "0";
        BindGrid();

    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        BindGrid();
    }
    protected void gvOpeningHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvOpeningHistory.PageIndex = e.NewPageIndex;
        BindGridHistory();
    }
}