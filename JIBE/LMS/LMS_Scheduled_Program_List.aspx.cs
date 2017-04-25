using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.LMS;
using SMS.Business.Crew;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class LMS_Scheduled_Program_List : System.Web.UI.Page
{

    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    public Boolean uaScheduleFlag = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            bindProgramcategory();
            BindFleetDLL();
            BindVesselDDL();
            BindProgramItemInGrid();

        }
    }


    protected void onUpdate(object source, CommandEventArgs e)
    {


    }

    protected void ProgramDelete(object source, CommandEventArgs e)
    {
        int Result = BLL_LMS_Training.Del_TrainingProgram(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindProgramItemInGrid();
    }

    protected void onSchedule(object source, CommandEventArgs e)
    {


    }

    protected void gvProgram_ListDetails_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "1")
            ViewState["SORTDIRECTION"] = 0;
        else
            ViewState["SORTDIRECTION"] = 1;

        BindProgramItemInGrid();
    }

    protected void BindProgramItemInGrid()
    {
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = null;
        if (ViewState["SORTDIRECTION"] != null)
            sortdirection = ViewState["SORTDIRECTION"].ToString();
        int is_Fetch_Count = ucCustomPagerProgram_List.isCountRecord;
        DataSet ds_ProgramList_Scheduled = BLL_LMS_Training.Get_Scheduled_Program_List((DataTable)DDLFleet.SelectedValues, (DataTable)DDLVessel.SelectedValues, UDFLib.ConvertStringToNull(txtProgramName.Text.Trim()), UDFLib.ConvertDateToNull(txtDuedateFrom.Text.Trim()), UDFLib.ConvertDateToNull(txtDuedateTo.Text), UDFLib.ConvertIntegerToNull(ddlProgramCategory.SelectedValue), ucCustomPagerProgram_List.CurrentPageIndex, ucCustomPagerProgram_List.PageSize, sortbycoloumn,
            sortdirection, ref is_Fetch_Count);
        gvProgram_ListDetails.DataSource = ds_ProgramList_Scheduled;
        gvProgram_ListDetails.DataBind();
        if (ucCustomPagerProgram_List.isCountRecord == 1)
        {
            ucCustomPagerProgram_List.CountTotalRec = is_Fetch_Count.ToString();
            ucCustomPagerProgram_List.BuildPager();
        }
    }

    protected void ddlProgramStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindProgramItemInGrid();
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        DDLFleet.ClearSelection();
        DDLVessel.ClearSelection();
        BindFleetDLL();
        BindVesselDDL();
        BindProgramItemInGrid();
        txtDuedateFrom.Text = "";
        txtDuedateTo.Text = "";
        txtProgramName.Text = "";
        ddlProgramCategory.SelectedIndex = 0;
        BindProgramItemInGrid();
    }

    public void BindFleetDLL()
    {
        try
        {

            Int32 UserCompanyID = Convert.ToInt32(Session["USERCOMPANYID"].ToString());
            Int32 VesselManager = UDFLib.ConvertToInteger(Session["VesselManager"]);
            DataTable FleetDT = BLL_LMS_Training.GetFleetList(UserCompanyID, VesselManager);
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    public void BindVesselDDL()
    {
        try
        {

            StringBuilder sbFilterFlt = new StringBuilder();
            string VslFilter = "";
            foreach (DataRow dr in DDLFleet.SelectedValues.Rows)
            {
                sbFilterFlt.Append(dr[0]);
                sbFilterFlt.Append(",");
            }


            DataTable dtVessel = BLL_LMS_Training.Get_VesselList(0, 0, 0, Convert.ToInt32(Session["USERCOMPANYID"]), "", Convert.ToInt32(Session["USERCOMPANYID"]));

            if (sbFilterFlt.Length > 1)
            {
                sbFilterFlt.Remove(sbFilterFlt.Length - 1, 1);
                VslFilter = string.Format("fleetCode in (" + sbFilterFlt.ToString() + ")");
                dtVessel.DefaultView.RowFilter = VslFilter;
            }

            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();


        }
        catch (Exception ex)
        {

        }
    }


    protected void DDLFleet_SelectedIndexChanged()
    {
        BindVesselDDL();

        // BindProgramItemInGrid();

    }

    protected void DDLVessel_SelectedIndexChanged()
    {


        // BindProgramItemInGrid();
    }



    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    protected void gvProgram_ListDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "1")
                        img.Src = "~/purchase/Image/arrowDown.png";
                    else
                        img.Src = "~/purchase/Image/arrowUp.png";

                    img.Visible = true;
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (e.Row.Cells[5].Text.Trim().Length == 0 || e.Row.Cells[5].Text.Trim() == "&nbsp;")
            //{
            //    if (DateTime.Now > Convert.ToDateTime(e.Row.Cells[4].Text))
            //    {
            //        e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
            //        e.Row.Cells[4].ForeColor = System.Drawing.Color.White;
            //        e.Row.Cells[4].Font.Bold = true;
            //    }

            //    if (DateTime.Now < Convert.ToDateTime(e.Row.Cells[4].Text))
            //    {
            //        if ((Convert.ToDateTime(e.Row.Cells[4].Text) - DateTime.Now).Days <= 7)
            //        {
            //            e.Row.Cells[4].BackColor = System.Drawing.Color.Yellow;
            //        }
            //    }
            //}


        }


    }
    public void bindProgramcategory()
    {
        DataTable dt = BLL_LMS_Training.Get_Program_Category();
        ddlProgramCategory.DataSource = dt;
        ddlProgramCategory.DataTextField = "Prg_Cat_Name";
        ddlProgramCategory.DataValueField = "Prg_Cat_Id";
        ddlProgramCategory.DataBind();
        ddlProgramCategory.Items.Insert(0, new ListItem("--SELECT--", null));
        ddlProgramCategory.SelectedIndex = 0;

    }
}