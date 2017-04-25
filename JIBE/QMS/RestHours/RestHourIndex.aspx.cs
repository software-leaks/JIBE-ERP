using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.QMSDB;
using System.Diagnostics;
using System.Collections;

public partial class RestHourIndex : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFleetDLL();
            DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            BindVesselDDL();
            Load_RankList();
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            txtfrom.Text = DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString("dd-MM-yyyy");
            txtto.Text = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day).ToString("dd-MM-yyyy");
            BindGrid();
        }
        //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
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

    public void Load_RankList()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_RankList();

        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "Rank_Short_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlRank.SelectedIndex = 0;
    }

    public void BindGrid()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = BLL_QMS_RestHours.Get_RestHours_Index(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(ddlvessel.SelectedValue), UDFLib.ConvertStringToNull(txtCrew.Text), UDFLib.ConvertStringToNull(ddlRank.SelectedValue)
            , UDFLib.ConvertDateToNull(txtfrom.Text), UDFLib.ConvertDateToNull(txtto.Text), sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            
            gvDeckLogBook.DataSource = dt;
            gvDeckLogBook.DataBind();
        }
        else
        {
            gvDeckLogBook.DataSource = dt;
            gvDeckLogBook.DataBind();
        }
    }


    protected void txtfrom_TextChanged(object sender, EventArgs e)
    {
        //BindGrid();
         
    }

    protected void txtCrew_TextChanged(object sender, EventArgs e)
    {
        
       // BindGrid();

    }
    protected void ddlRank_SelectedIndexChanged(object sender, EventArgs e)
    {
       
      //  BindGrid();

    }
    protected void txtto_TextChanged(object sender, EventArgs e)
    {
       // BindGrid();
    }

    protected void ddlvessel_SelectedIndexChanged(object sender, EventArgs e)
    {

       // BindGrid();
       
    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselDDL();
      //  BindGrid();
    }

    protected void gvDeckLogBook_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindGrid();
    }

    protected void gvDeckLogBook_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvDeckLogBook_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';this.style.font='bold';";
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.font='bold';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
          //  e.Row.Attributes["onclick"] = ClientScript.GetPostBackEventReference(this.gvDeckLogBook, "Select$" + e.Row.RowIndex);

        }

    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_QMS_RestHours.Get_RestHours_Index(UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(ddlvessel.SelectedValue), UDFLib.ConvertStringToNull(txtCrew.Text), UDFLib.ConvertStringToNull(ddlRank.SelectedValue),
            UDFLib.ConvertDateToNull(txtfrom.Text), UDFLib.ConvertDateToNull(txtto.Text), sortbycoloumn, sortdirection
           , 1, null, ref  rowcount);


        string[] HeaderCaptions = { "Vessel", "Date", "Code", "Crew Name", "Rank", "Ship's Clocked Hours", "Working Hours", "Rest Hours", "Rest Hours Any 24", "Over Time", "Seafarer's Remarks", "Verifier's Remarks" };
        string[] DataColumnsName = { "Vessel_Name", "REST_HOURS_DATE", "Staff_Code", "Staff_Name", "Staff_rank_Code", "SHIPS_CLOCKED_HOURS", "WORKING_HOURS", "REST_HOURS", "REST_HOURS_ANY24", "OverTime_HOURS", "Seafarer_Remarks", "Verifier_Remarks" };

         GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Rest Hour", "Rest_Hours_Index", "");

        //ExcelDataExport objexp = new ExcelDataExport();
        //objexp.WriteExcell(dt);

    
    }

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        DDLFleet.SelectedValue = "0";
        ddlvessel.SelectedValue = "0";
        txtfrom.Text = "";
        txtto.Text = "";
        ddlRank.SelectedIndex = 0;
        txtCrew.Text = "";
        //txtfrom.Text = DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToString("dd-MM-yyyy");
        //txtto.Text = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day).ToString("dd-MM-yyyy");
        BindGrid();
    }
     

    protected void gvDeckLogBook_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {
        Label lblVesselID = (Label)gvDeckLogBook.Rows[se.NewSelectedIndex].FindControl("lblVesselID");
        Label lblDeckLogBookID = (Label)gvDeckLogBook.Rows[se.NewSelectedIndex].FindControl("lblDeckLogBookID");
        ResponseHelper.Redirect("../RestHours/RestHourDetails.aspx?ID=" + lblDeckLogBookID.Text.Trim() + "&Vessel_ID=" + lblVesselID.Text, "Blank", "");

    }
}

public class ExcelDataExport : Page
{
    Hashtable myHashtable;
    public static object Opt = Type.Missing;
    public static Microsoft.Office.Interop.Excel.Application ExlApp;
    public static Microsoft.Office.Interop.Excel.Workbook ExlWrkBook;
    public static Microsoft.Office.Interop.Excel.Worksheet ExlWrkSheet;
    //private Microsoft.Office.Interop.Excel.Range range;
    public ExcelDataExport()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void WriteExcell(DataTable dtReport)
    {
        CheckExcellProcesses();

        string path = "";
        path = Server.MapPath("RestHours_Export.xls");


        ExlApp = new Microsoft.Office.Interop.Excel.Application();
        try
        {
            ExlWrkBook = ExlApp.Workbooks.Open(path, 0,
                                                      true,
                                                      5,
                                                      "",
                                                      "",
                                                      true,
                                                      Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                                                      "\t",
                                                      false,
                                                      false,
                                                      0,
                                                      true,
                                                      1,
                                                      0);
            ExlWrkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExlWrkBook.ActiveSheet;

            int i = 2;
            foreach (DataRow dr in dtReport.Rows)
            {
                ExlWrkSheet.Cells[i, 1] = Convert.ToString(dr["Vessel_Name"]);
                ExlWrkSheet.Cells[i, 2] = Convert.ToString(dr["REST_HOURS_DATE"]);
                ExlWrkSheet.Cells[i, 3] = Convert.ToString(dr["Staff_Code"]);
                ExlWrkSheet.Cells[i, 4] = Convert.ToString(dr["Staff_Name"]);
                ExlWrkSheet.Cells[i, 5] = Convert.ToString(dr["Staff_rank_Code"]);
                ExlWrkSheet.Cells[i, 6] = Convert.ToString(dr["SHIPS_CLOCKED_HOURS"]);
                ExlWrkSheet.Cells[i, 7] = Convert.ToString(dr["WORKING_HOURS"]);
                ExlWrkSheet.Cells[i, 8] = Convert.ToString(dr["REST_HOURS"]);
                ExlWrkSheet.Cells[i, 9] = Convert.ToString(dr["Seafarer_Remarks"]);
                ExlWrkSheet.Cells[i, 10] = Convert.ToString(dr["Verifier_Remarks"]);
                ExlWrkSheet.Cells[i, 61] = Convert.ToString(dr["REST_HOURS_ANY24"]);
                if (dr["WH_0000_0030"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range  range =  ExlWrkSheet.get_Range("M"+i.ToString(),"M"+i.ToString());
                   range.Cells.Interior.Color = System.Drawing.Color.Orange;
                   
                }
 
                

               
                if (dr["WH_0030_0100"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("N" + i.ToString(), "N" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }
 
              

                if (dr["WH_0100_0130"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("O" + i.ToString(), "O" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }
 

                if (dr["WH_0130_0200"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("P" + i.ToString(), "P" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }
 
                if (dr["WH_0200_0230"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("Q" + i.ToString(), "Q" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }
 
                if (dr["WH_0230_0300"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("R" + i.ToString(), "R" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }
 

                if (dr["WH_0300_0330"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("S" + i.ToString(), "S" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }
 

                if (dr["WH_0330_0400"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("T" + i.ToString(), "T" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }
 

                if (dr["WH_0400_0430"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("U" + i.ToString(), "U" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }

                if (dr["WH_0430_0500"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("V" + i.ToString(), "V" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }

                if (dr["WH_0500_0530"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("W" + i.ToString(), "W" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_0530_0600"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("X" + i.ToString(), "X" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_0600_0630"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("Y" + i.ToString(), "Y" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_0630_0700"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("Z" + i.ToString(), "Z" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_0700_0730"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AA" + i.ToString(), "AA" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_0730_0800"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AB" + i.ToString(), "AB" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_0800_0830"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AC" + i.ToString(), "AC" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_0830_0900"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AD" + i.ToString(), "AD" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_0900_0930"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AE" + i.ToString(), "AE" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_0930_1000"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AF" + i.ToString(), "AF" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }

                if (dr["WH_1000_1030"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AG" + i.ToString(), "AG" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_1030_1100"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AH" + i.ToString(), "AH" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_1100_1130"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AI" + i.ToString(), "AI" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_1130_1200"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AJ" + i.ToString(), "AJ" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_1200_1230"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AK" + i.ToString(), "AK" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_1230_1300"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AL" + i.ToString(), "AL" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_1300_1330"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AM" + i.ToString(), "AM" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_1330_1400"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AN" + i.ToString(), "AN" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }
 


                ////--------------

                if (dr["WH_1400_1430"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AO" + i.ToString(), "AO" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_1430_1500"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AP" + i.ToString(), "AP" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }



                if (dr["WH_1500_1530"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AQ" + i.ToString(), "AQ" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_1530_1600"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AR" + i.ToString(), "AR" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }

                if (dr["WH_1600_1630"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AS" + i.ToString(), "AS" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }

                if (dr["WH_1630_1700"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AT" + i.ToString(), "AT" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_1700_1730"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AU" + i.ToString(), "AU" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_1730_1800"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AV" + i.ToString(), "AV" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_1800_1830"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AW" + i.ToString(), "AW" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_1830_1900"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AX" + i.ToString(), "AX" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_1900_1930"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AY" + i.ToString(), "AY" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_1930_2000"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("AZ" + i.ToString(), "AZ" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_2000_2030"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("BA" + i.ToString(), "BA" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_2030_2100"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("BB" + i.ToString(), "BB" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_2100_2130"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("BC" + i.ToString(), "BC" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_2130_2200"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("BD" + i.ToString(), "BD" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_2200_2230"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("BE" + i.ToString(), "BE" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_2230_2300"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("BF" + i.ToString(), "BF" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }


                if (dr["WH_2300_2330"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("BG" + i.ToString(), "BG" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange; //System.Drawing.Color.Orange;

                }

                if (dr["WH_2330_2400"].ToString() == "1")
                {
                    Microsoft.Office.Interop.Excel.Range range = ExlWrkSheet.get_Range("BH" + i.ToString(), "BH" + i.ToString());
                    range.Cells.Interior.Color = System.Drawing.Color.Orange;

                }
 

              

                i++;

            }

            string FileNameToSave = "RestHours_" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + " " + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss") + ".xls";

            ExlWrkBook.SaveAs(Server.MapPath("~/uploads/Purchase/") + FileNameToSave, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, true);
            ResponseHelper.Redirect("~/uploads/Purchase/" + FileNameToSave, "blank", "");
          
        }
        catch
        {

        }
        finally
        {
            ExlWrkBook.Close(null, null, null);
            ExlApp.Workbooks.Close();
            ExlApp.Quit();
            KillExcel();



        }



    }

    private void CheckExcellProcesses()
    {
        Process[] AllProcesses = Process.GetProcessesByName("excel");
        myHashtable = new Hashtable();
        int iCount = 0;

        foreach (Process ExcelProcess in AllProcesses)
        {
            myHashtable.Add(ExcelProcess.Id, iCount);
            iCount = iCount + 1;
        }
    }

    private void KillExcel()
    {
        Process[] AllProcesses = Process.GetProcessesByName("excel");

        // check to kill the right process
        foreach (Process ExcelProcess in AllProcesses)
        {
            if (myHashtable.ContainsKey(ExcelProcess.Id) == false)
                ExcelProcess.Kill();
        }

        AllProcesses = null;
    }


}