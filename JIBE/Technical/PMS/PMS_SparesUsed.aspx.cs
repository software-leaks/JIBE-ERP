using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PMS;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class PMS_SparesUsed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            BindSparesItemUsed(); 
        }

    }


    //private void BindJobIndividualDetails()
    //{

    //    BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
    //    DataSet ds = objJobs.LibraryJobIndividualDetails(Convert.ToInt32(Request.QueryString["JobID"].ToString()));



    //    //if (ds.Tables[0].Rows.Count > 0)
    //    //{

    //    //    DataRow dr = ds.Tables[0].Rows[0];

    //    //    txtJobCode.Text = dr["JobCode"].ToString();
    //    //    txtVessel.Text = dr["Vessel_Name"].ToString();
    //    //    txtMachinery.Text = dr["Machinery"].ToString();

    //    //    txtSubsystem.Text = dr["Vessel_Name"].ToString();
    //    //    txtJobTitle.Text = dr["Job_Title"].ToString();
    //    //    txtJobDescription.Text = dr["Job_Description"].ToString();
    //    //    txtDepartment.Text = dr["DepartmentName"].ToString();
    //    //    txtRank.Text = dr["RankShortName"].ToString();
    //    //    txtcms.Text = dr["CMS"].ToString();

    //    //    if (dr["CMS"].ToString() == "Y")
    //    //        txtcms.BackColor = System.Drawing.Color.RosyBrown;

    //    //    if (dr["CRITICAL"].ToString() == "Y")
    //    //        txtCritical.BackColor = System.Drawing.Color.RosyBrown;


    //    //    txtCritical.Text = dr["CRITICAL"].ToString();
    //    //    txtFrequencyName.Text = dr["FrequencyName"].ToString();
    //    //    txtFrequencyType.Text = dr["Frequency"].ToString();

    //    //}

    //    //if (ds.Tables[1].Rows.Count > 0)
    //    //{

    //    //    DataRow dr = ds.Tables[1].Rows[0];
    //    //    txtLocation.Text = dr["Location"].ToString();
    //    //    txtDateOriginallyDue.Text = dr["DateOriginallyDue"].ToString();
    //    //    txtDateNextDue.Text = dr["DateNextDue"].ToString();
    //    //    txtDateJobDone.Text = dr["DateJobDone"].ToString();
    //    //    txtRhrsWhenJobDone.Text = dr["RhrsWhenJobDone"].ToString();
    //    //    txtJobRemark.Text = dr["History"].ToString();

    //    //    lbnCreatedBy.Text = dr["Created_By_Name"].ToString();
    //    //    txtCreatedOn.Text = dr["Created_On"].ToString();
    //    //    lbnVerifiedBy.Text = dr["Verified_By_Name"].ToString();
    //    //    txtVerifiedOn.Text = dr["Verified_On"].ToString();
    //    //    ViewState["CreatedByID"] = dr["CreatedByID"].ToString();
    //    //    ViewState["VerifiedByID"] = dr["VerifierID"].ToString();

    //    //}


    //}


    public void BindSparesItemUsed()
    {
        BLL_PMS_Job_Status objJobStatus = new BLL_PMS_Job_Status();

        int rowcount = ucCustomPagerItems.isCountRecord;


        int? vesselid = null;
        if (!string.IsNullOrEmpty(Request.QueryString["Vessel_ID"]))
            vesselid = Convert.ToInt32(Request.QueryString["Vessel_ID"].ToString());

        int? jobid = null; 
        if (!string.IsNullOrEmpty(Request.QueryString["JobID"])) 
            jobid = Convert.ToInt32(Request.QueryString["JobID"].ToString());

        int? jobhistoryid = null; 
        if (!string.IsNullOrEmpty(Request.QueryString["JobHistoryID"]))
            jobhistoryid = Convert.ToInt32(Request.QueryString["JobHistoryID"].ToString());


        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataSet ds = objJobStatus.TecJobsSparesItemUsedSearch(vesselid, jobid, jobhistoryid, null, null, txtSearchtext.Text.Trim(), sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvSpareItemUsed.DataSource = ds.Tables[0];
            gvSpareItemUsed.DataBind();
        }
        else
        {
            gvSpareItemUsed.DataSource = ds.Tables[0];
            gvSpareItemUsed.DataBind();
        }



    }

    protected void gvSpareItemUsed_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvSpareItemUsed_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "../../purchase/Image/arrowUp.png";

                    else
                        img.Src = "../../purchase/Image/arrowDown.png";
                    img.Visible = true;
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblItemName = (Label)e.Row.FindControl("lblItemName");
            Label lblFullDesc = (Label)e.Row.FindControl("lblFullDesc");

            Label lblItemUsedRemaks = (Label)e.Row.FindControl("lblItemUsedRemaks");
            Label lblItemUsedFullRemaks = (Label)e.Row.FindControl("lblItemUsedFullRemaks");

            if (lblItemUsedFullRemaks.Text.Length > 20)
            {
                lblItemUsedRemaks.Text = lblItemUsedRemaks.Text + "..";
                lblItemUsedRemaks.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Remarks] body=[" + lblItemUsedFullRemaks.Text + "]");
            }

            if (lblFullDesc.Text != "")
                lblItemName.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Description] body=[" + lblFullDesc.Text + "]");

        }

    }
    protected void gvSpareItemUsed_Sorting(object sender, GridViewSortEventArgs se)
    {


        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindSparesItemUsed();



    }
    protected void btnRetrieve_Click(object sender, EventArgs e)
    {

        BindSparesItemUsed();

    }
    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        txtSearchtext.Text = "";
        BindSparesItemUsed();
    }
}