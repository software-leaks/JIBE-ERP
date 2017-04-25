using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.QMS;
using SMS.Business.Crew;
using SMS.Business.QMSDB;

public partial class DataMigrationIndex : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindFleetDLL();
            DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            BindVesselDDL();
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
          
            BindGrid();
        }
    }


    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.Items.Clear();
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLFleet.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }

    public void BindVesselDDL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            ddlvessel.Items.Clear();
            ddlvessel.DataSource = dtVessel;
            ddlvessel.DataTextField = "Vessel_name";
            ddlvessel.DataValueField = "Vessel_id";
            ddlvessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            ddlvessel.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }

   
    public void BindGrid()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = BLL_Infra_Common.SearchDMN_UpdatesErrors( UDFLib.ConvertIntegerToNull(ddlvessel.SelectedValue), sortbycoloumn, sortdirection , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            
            gvDmErrorList.DataSource = dt;
            gvDmErrorList.DataBind();
        }
        else
        {
            gvDmErrorList.DataSource = dt;
            gvDmErrorList.DataBind();
        }
    }


    protected void txtfrom_TextChanged(object sender, EventArgs e)
    {
        BindGrid();
         
    }

    protected void txtCrew_TextChanged(object sender, EventArgs e)
    {
        
        BindGrid();

    }
    protected void ddlRank_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        BindGrid();

    }
    protected void txtto_TextChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void ddlvessel_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindGrid();
       
    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselDDL();
        BindGrid();
    }

    protected void gvDmErrorList_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindGrid();
    }

    protected void gvDmErrorList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowUp.png";
                    else
                        img.Src = "~/purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }
    }

    protected void gvDmErrorList_RowCreated(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    //e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';this.style.font='bold';";
        //    e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.font='bold';";
        //    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
        //    e.Row.Attributes["onclick"] = ClientScript.GetPostBackEventReference(this.gvDmErrorList, "Select$" + e.Row.RowIndex);

        //}

    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_QMS_RestHours.Get_RestHours_Index(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(ddlvessel.SelectedValue), UDFLib.ConvertStringToNull(null), UDFLib.ConvertStringToNull(null),
            UDFLib.ConvertDateToNull(null), UDFLib.ConvertDateToNull(null), sortbycoloumn, sortdirection
           , 1, UDFLib.ConvertIntegerToNull(ucCustomPagerItems.CountTotalRec), ref  rowcount);


        string[] HeaderCaptions = { "Vessel", "Date", "Code", "Crew Name","Rank","Ship's Clocked Hours","Working Hours","Rest Hours","Verifier's Comments" };
        string[] DataColumnsName = { "Vessel_Name", "REST_HOURS_DATE", "Staff_Code", "Staff_Name", "Staff_rank_Code", "SHIPS_CLOCKED_HOURS", "WORKING_HOURS", "REST_HOURS", "Verifier_Remarks"};

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Rest Hour", "Rest Hours", "");
    
    }

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        DDLFleet.SelectedValue = "0";
        ddlvessel.SelectedValue = "0";
        
        BindGrid();
    }
     

    protected void gvDmErrorList_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {
        Label lblVesselID = (Label)gvDmErrorList.Rows[se.NewSelectedIndex].FindControl("lblVesselID");
        Label lblDeckLogBookID = (Label)gvDmErrorList.Rows[se.NewSelectedIndex].FindControl("lblDeckLogBookID");
        ResponseHelper.Redirect("../RestHours/RestHourDetails.aspx?ID=" + lblDeckLogBookID.Text.Trim() + "&Vessel_ID=" + lblVesselID.Text, "Blank", "");

    }
}