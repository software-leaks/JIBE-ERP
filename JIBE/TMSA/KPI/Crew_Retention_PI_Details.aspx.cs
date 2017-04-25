using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SMS.Business.Infrastructure;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;
using System.Text;

using SMS.Business.Crew;
using SMS.Business.TMSA;

public partial class Crew_Retention_PI_Details : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    protected DataTable dtGridItems;
    protected int PI_ID= 0;
    protected int Detail_ID = 0;
    protected string QtrMonth= null ;
    int IsExist = 0;
    public Boolean uaEditFlag = true;//Test default true
    public Boolean uaDeleteFlage = true;
    BLL_TMSA_PI objPI = new BLL_TMSA_PI();

    protected void Page_Load(object sender, EventArgs e)
    {
      // UserAccessValidation();
        if (!IsPostBack)
        {
            txtSearchFrom.Text = DateTime.Now.AddDays(-30).ToString("dd-MM-yyyy");
            txtSearchTo.Text = DateTime.Now.ToString("dd-MM-yyyy");
            BindRanks();
            LoadSearchResults();

        }
    }


   
    /// <summary>
    /// Method to load cre  ranks drop down
    /// </summary>
    protected void BindRanks()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_RankList();

        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "Rank_Short_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();

        ddlRank.Items.Insert(0, new ListItem("--Select--", "0"));
        
    }

    /// <summary>
    /// Method to check authorize user
    /// </summary>
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");


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

  

    protected void btnportfilter_Click(object sender, ImageClickEventArgs e)
    {
        LoadSearchResults();
    }
    /// <summary>
    /// Method to search retention PI Values by Rank
    /// </summary>
    protected void LoadSearchResults()
    {

        int rowcount = ucCustomPager1.isCountRecord;
        PI_ID = Convert.ToInt32(Request.QueryString["PI_ID"]);
        DateTime? dtWEF = null;
        DateTime? dtWET = null;
        if (txtSearchFrom.Text != "")

            dtWEF = Convert.ToDateTime(txtSearchFrom.Text);
        if (txtSearchTo.Text != "")
            dtWET = Convert.ToDateTime(txtSearchTo.Text);
        //DataTable dt = BLL_TMSA_PI.Get_Vessel_Values(Convert.ToInt32(ddlVessel.SelectedValue), dtWEF, dtWET).Tables[0];

        if (txtSearchFrom.Text != "" && txtSearchTo.Text != "")
        {
            if (dtWEF <= dtWET)
            {
                DataSet ds = BLL_TMSA_PI.Get_RankWise_Values(Convert.ToInt32(ddlRank.SelectedValue), dtWEF, dtWET, PI_ID, ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount);

                DataTable dt = ds.Tables[0];
                string PI_Code = ds.Tables[1].Rows[0]["code"].ToString();
                if (ltPageHeader.Text.Contains(PI_Code))
                    ltPageHeader.Text = ltPageHeader.Text;
                else
                    ltPageHeader.Text=ltPageHeader.Text + "[" + PI_Code +"]";
                if (ucCustomPager1.isCountRecord == 1)
                {
                    ucCustomPager1.CountTotalRec = rowcount.ToString();
                    ucCustomPager1.BuildPager();
                }
                dt.DefaultView.Sort = "Effective_From DESC";
                gvPIDetails.DataSource = dt;
                gvPIDetails.DataBind();
            }
            else
            {
                string msg2 = String.Format("alert('From Date  should not be greater than to date')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            }
        }
        else
        {
            DataTable dt = new DataTable();
            if (ddlRank.SelectedValue != "0" && ddlRank.SelectedValue !="")
                dt = BLL_TMSA_PI.Get_RankWise_Values(Convert.ToInt32(ddlRank.SelectedValue), dtWEF, dtWET, PI_ID, ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount).Tables[0];
            else
                dt = BLL_TMSA_PI.Get_RankWise_Values(null, dtWEF, dtWET, PI_ID, ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount).Tables[0];
            if (ucCustomPager1.isCountRecord == 1)
            {
                dt.DefaultView.Sort = "Effective_From DESC";
                ucCustomPager1.CountTotalRec = rowcount.ToString();
                ucCustomPager1.BuildPager();
            }

            gvPIDetails.DataSource = dt;
            gvPIDetails.DataBind();
        }


    }
    /// <summary>
    /// Method to export data to excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        int rowcount = ucCustomPager1.isCountRecord;
        string PIName="PI Name: ";
        PI_ID = Convert.ToInt32(Request.QueryString["PI_ID"]);
        DateTime? dtWEF = null;
        DateTime? dtWET = null;
        if (txtSearchFrom.Text != "")

            dtWEF = Convert.ToDateTime(txtSearchFrom.Text);
        if (txtSearchTo.Text != "")
            dtWET = Convert.ToDateTime(txtSearchTo.Text);

        DataSet ds = BLL_TMSA_PI.Get_RankWise_Values(Convert.ToInt32(ddlRank.SelectedValue), dtWEF, dtWET, PI_ID, null, null, ref  rowcount);
        DataTable dt = ds.Tables[0];
        PIName = "PI Rankwise : " + ds.Tables[1].Rows[0]["Name"];

        string[] HeaderCaptions = { "Rank", "Effect From", "Effect To", "PI Value", "Created On" , "Last Modified"};
        string[] DataColumnsName = { "Rank", "Effective_From_Str", "Effective_To", "Value", "Date_Of_Creation", "Date_Of_Modification" };
        GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, PIName, PIName);

    }


    protected void gvPIDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try{
        int ID = UDFLib.ConvertToInteger(gvPIDetails.DataKeys[e.RowIndex].Value.ToString());

        TextBox txtValue = (TextBox)gvPIDetails.Rows[e.RowIndex].FindControl("txtValue");
        int Value = Convert.ToInt32(txtValue.Text);

        objPI.CrewRetention_UpdateRankValue(ID, Value, GetSessionUserID());
        gvPIDetails.EditIndex = -1;
        LoadSearchResults();
        }
        catch
        {
        }
    }

    protected void gvPIDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvPIDetails.EditIndex = e.NewEditIndex;
            LoadSearchResults();

        }
        catch
        {
        }
    }
    protected void gvPIDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvPIDetails.EditIndex = -1;
            LoadSearchResults();

        }
        catch
        {
        }


    }
}
   
