using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using SMS.Business.Crew;
using SMS.Business.PMS;

public partial class PMSAssignLocationCatalogue : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucCustomPagerItems.PageSize = 14;
            BindCatalogueAssignLocation();
        }
    }


    public void BindCatalogueAssignLocation()
    {
        BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["ITEMSORTBYCOLOUMN"] == null) ? null : (ViewState["ITEMSORTBYCOLOUMN"].ToString());

        int? sortdirection = null;
        if (ViewState["ITEMSORTDIRECTION"] != null)
            sortdirection = Int32.Parse(ViewState["ITEMSORTDIRECTION"].ToString());

        string systemcode = ((Request.QueryString["SystemCode"].ToString() != "") || (Request.QueryString["SystemCode"] != null)) ? Request.QueryString["SystemCode"].ToString() : null;

        //if ((Request.QueryString["SystemCode"].ToString() != "") || (Request.QueryString["SystemCode"] !=null))
        //{
        
        //}


        DataSet ds = objJobs.LibraryCatalogueLocationAssignSearch(systemcode,Convert.ToString(ViewState["SubSystemId"]), txtSearchLocation.Text, Convert.ToInt32(19), sortbycoloumn
            , sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvLocation.DataSource = ds.Tables[0];
            gvLocation.DataBind();
        }
        else
        {
            gvLocation.DataSource = ds.Tables[0];
            gvLocation.DataBind();
        }
    }

    protected void imgLocationSearch_Click(object sender, ImageClickEventArgs e)
    {
        //BindCatalogueAssignLocation();
    }


    public void chkAssignLoc_Select(object sender , EventArgs  e)
    {
        //CheckBox chkAssignLoc = (CheckBox)sender;
        //int nCurrentRow = Int32.Parse(chkAssignLoc.Text);

        //string systemcode = ((Label)gvLocation.Rows[nCurrentRow].FindControl("lblSystemCode")).Text;
        //string Locationcode = ((Label)gvLocation.Rows[nCurrentRow].FindControl("lblLocationCode")).Text;
        //string VesselCode = ((Label)gvLocation.Rows[nCurrentRow].FindControl("lblVesselID")).Text;

        //BLL_Tec_Library_Jobs objJobs = new BLL_Tec_Library_Jobs();
        //int retval = objJobs.LibraryCatalogueLocationAssignSave(Convert.ToInt32(Session["userid"].ToString()), systemcode,Convert.ToInt32(Locationcode), Convert.ToInt32(VesselCode));

  }

   


    protected void gvLocation_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void gvLocation_RowDataBound(object sender, GridViewRowEventArgs e)
    {


    }

    protected void gvLocation_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void gvLocation_Sorting(object sender, GridViewSortEventArgs e)
    {


    }

    protected void btnDivApprove_Click(object sender, EventArgs e)
    {

    }
    protected void btnDivReject_Click(object sender, EventArgs e)
    {

    }
    public void btnDivSave_click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("LocationID");
        dt.Columns.Add("LocationName");


        string systemcode = ViewState["SystemCode"].ToString();

        foreach (GridViewRow gr in gvLocation.Rows)
        {
            CheckBox chkAssignLoc = (CheckBox)gr.FindControl("chkDivAssingLoc");

            if (chkAssignLoc.Checked == true)
            {
                string Locationcode = ((Label)gr.FindControl("lblDivLocationCode")).Text;
                string LocationName = ((Label)gr.FindControl("lblDivLocationName")).Text;

                //BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
                //int retval = objJobs.LibraryCatalogueLocationAssignSave(Convert.ToInt32(Session["userid"].ToString())
                //    , systemcode, Convert.ToInt32(Locationcode), Convert.ToInt32(ViewState["VesselCodeEditMode"].ToString()));

                AddDataTempLocation(Locationcode, LocationName, dt); 

            }
        }
      
    }

    public void AddDataTempLocation(string LocationID, string LocationName, DataTable dt)
    {

        DataRow dr;
        dr = dt.NewRow();

        dr["LocationID"] = LocationID;
        dr["LocationName"] = LocationName;

        dt.Rows.Add(dr);

        ViewState["TempDtLocation"] = dt;

    }

}