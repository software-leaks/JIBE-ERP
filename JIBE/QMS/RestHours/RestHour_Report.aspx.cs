using System;
using System.Data;
using SMS.Business.QMSDB;
using SMS.Business.Infrastructure;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections.Generic;
public partial class RestHourDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                BindVesselDDL();
                //if (Request.QueryString["ID"] != null)
                //{
                // hdnVesselID.Value = Request.QueryString["Vessel_ID"].ToString();
                //hdnLogBookID.Value = Request.QueryString["ID"].ToString();              
                BindRestHoursDetails();
                //}

                #region QueryString


                if (Request.QueryString["ID"] != null && Request.QueryString["Vessel_ID"] != null && Request.QueryString["CREWID"] != null & Request.QueryString["RestHourDate"] != null)
                {
                    txtTo.Text = Convert.ToDateTime(Request.QueryString["RestHourDate"]).ToString("dd-MM-yyyy"); // To Date Equals to Exception Date
                    txtFrom.Text = Convert.ToDateTime(Request.QueryString["RestHourDate"]).AddMonths(-1).ToString("dd-MM-yyyy"); // To Date Minus 1 Month
                    ddlVesselList.SelectedValue = UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"]).ToString(); //Vessel Auto Select
                    ddlVesselList_SelectedIndexChanged(sender, e); //vessellistchange event
                    ddlCrewList.SelectedValue = UDFLib.ConvertToInteger(Request.QueryString["CREWID"]).ToString(); // Crew Auto Select
                    txtSearch_Click(sender, e); //Data Populate 
                }

                #endregion
            }
            catch(Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
        }
    }

    private void BindRestHoursDetails()
    {
        DataTable dt = BLL_QMS_RestHours.Get_RestHours_Report(UDFLib.ConvertDateToNull(txtFrom.Text), UDFLib.ConvertDateToNull(txtTo.Text), UDFLib.ConvertToInteger(ddlCrewList.SelectedValue), UDFLib.ConvertToInteger(ddlVesselList.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            lblStaffCode.Text = dt.Rows[0]["Staff_Code"].ToString();
            lblStaffName.Text = dt.Rows[0]["Staff_Name"].ToString();
            lblStaffRank.Text = dt.Rows[0]["Staff_rank_Code"].ToString();
            
            txtSeafarerRemarks.Text = dt.Rows[0]["Seafarer_Remarks"].ToString();
            txtVerifierRemarks.Text = dt.Rows[0]["Verifier_Remarks"].ToString();
            lblManage.Text = dt.Rows[0]["Manager_Code"].ToString() + "-" + dt.Rows[0]["Manager_Rank"].ToString() + "-" + dt.Rows[0]["Manager_Name"].ToString();
            lblDateofjoing.Text = dt.Rows[0]["Joining_Date"].ToString() != "" ? Convert.ToDateTime(dt.Rows[0]["Joining_Date"]).ToString("dd/MMM/yyyy") : "";
            lblDateofsignoff.Text = dt.Rows[0]["Sign_Off_Date"].ToString() != "" ? Convert.ToDateTime(dt.Rows[0]["Sign_Off_Date"]).ToString("dd/MMM/yyyy") : "";
            lblSignOn.Text = dt.Rows[0]["Sign_On_Date"].ToString() != "" ? Convert.ToDateTime(dt.Rows[0]["Sign_On_Date"]).ToString("dd/MMM/yyyy") + " " + Convert.ToDateTime(dt.Rows[0]["Sign_On_Date"]).ToString("HH:MM") : "";
            // lblManagerRank.Text = dt.Rows[0]["Manager_Rank"].ToString();
            lblRestHourDate.Text = "    Date :   " + dt.Rows[0]["REST_HOURS_DATE"].ToString();
            if (dt.Rows[0]["IsEmergency"].ToString() == "1")
                chkEmergency.Checked = true;
            if (dt.Rows[0]["IsArrival"].ToString() == "1")
                chkArrival.Checked = true;
            if (dt.Rows[0]["IsDeparture"].ToString() == "1")
                chkDeparture.Checked = true;
            if (dt.Rows[0]["IsDrill"].ToString() == "1")
                chkDrill.Checked = true;
            if (dt.Rows[0]["IsEmergencyVerify"].ToString() == "1")
                chkEmergencyVerify.Checked = true;

            

        }
        else
        {
            lblStaffCode.Text = "";
            lblStaffName.Text = "";
            lblStaffRank.Text = "";
            lblDateofjoing.Text = "";
            lblDateofsignoff.Text = "";
            txtSeafarerRemarks.Text = "";
            txtVerifierRemarks.Text = "";
            lblManage.Text = "";
            lblSignOn.Text = "";
            // lblManagerRank.Text = dt.Rows[0]["Manager_Rank"].ToString();
            lblRestHourDate.Text = "";
           

        }
        rpDeckLogBook01.DataSource = dt;
        rpDeckLogBook01.DataBind();
    }

    protected void btnHiddenSubmit_Click(object sender, EventArgs e)
    {
        BindRestHoursDetails();
    }
    protected void ddlVesselList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCrewList.DataSource = BLL_QMS_RestHours.CrewList_by_Date(UDFLib.ConvertDateToNull(txtFrom.Text), UDFLib.ConvertDateToNull(txtTo.Text), UDFLib.ConvertToInteger(ddlVesselList.SelectedValue));
        ddlCrewList.DataTextField = "staff_name";
        ddlCrewList.DataValueField = "CrewID";

        ddlCrewList.DataBind();
    }

    public void BindVesselDDL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            ddlVesselList.Items.Clear();
            ddlVesselList.DataSource = dtVessel;
            ddlVesselList.DataTextField = "Vessel_name";
            ddlVesselList.DataValueField = "Vessel_id";
            ddlVesselList.DataBind();
            ListItem li = new ListItem("--SELECT--", "0");
            ddlVesselList.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }
    protected void txtSearch_Click(object sender, EventArgs e)
    {
       
        if (Convert.ToString(txtFrom.Text).Trim() != "" && Convert.ToString(txtTo.Text).Trim() != "")
        {
            if (ddlVesselList.SelectedIndex <= 0)
            {
                string msg = String.Format("alert('Vessel is mandatory!')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                return;
            }
            BindRestHoursDetails();
        }
        else
        {
            string Alert = String.Format("alert('Select Proper From and To Date !');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", Alert, true);
        }
    }
    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {






        DataTable dt = BLL_QMS_RestHours.Get_RestHours_Report(UDFLib.ConvertDateToNull(txtFrom.Text), UDFLib.ConvertDateToNull(txtTo.Text), UDFLib.ConvertToInteger(ddlCrewList.SelectedValue), UDFLib.ConvertToInteger(ddlVesselList.SelectedValue));



        string[] HeaderCaptions = { "Date",   "Ship's Clocked Hours",
                                           "0","0", "1", "0", "2", 
                                             "0", "3", "0", "4", 
                                             "0" , "5", "0","6", 
                                             "0","7", "0", "8",  
                                             "0", "9", "1", "0", 
                                             "1", "1", "1", "2",
                                             "1", "3", "1", "4", 
                                             "1", "5", "1", "6", 
                                             "1","7", "1", "8", 
                                             "1","9", "2", "0", 
                                             "2", "1", "2", "2", 
                                             "2","3", "",  
                                           "Work Hours", "Rest Hours", "Rest Hours in Any 24 Hours", "Over Time" ,"Rest Hour In 7-Day Period","Seafarer's Remark","Verifier's Remark"   };
        string[] DataColumnsName = { "REST_HOURS_DATE", "SHIPS_CLOCKED_HOURS",
                                             "WH_0000_0030", "WH_0030_0100", "WH_0100_0130", "WH_0130_0200", 
                                             "WH_0200_0230", "WH_0230_0300", "WH_0300_0330", "WH_0330_0400", 
                                             "WH_0400_0430" , "WH_0430_0500", "WH_0500_0530","WH_0530_0600", 
                                             "WH_0600_0630","WH_0630_0700", "WH_0700_0730", "WH_0730_0800",  
                                             "WH_0800_0830", "WH_0830_0900", "WH_0900_0930", "WH_0930_1000", 
                                             "WH_1000_1030", "WH_1030_1100", "WH_1100_1130", "WH_1130_1200",
                                             "WH_1200_1230", "WH_1230_1300", "WH_1300_1330", "WH_1330_1400", 
                                             "WH_1400_1430", "WH_1430_1500", "WH_1500_1530", "WH_1530_1600", 
                                             "WH_1600_1630","WH_1630_1700", "WH_1700_1730", "WH_1730_1800", 
                                             "WH_1800_1830","WH_1830_1900", "WH_1900_1930", "WH_1930_2000", 
                                             "WH_2000_2030", "WH_2030_2100", "WH_2100_2130", "WH_2130_2200", 
                                             "WH_2200_2230","WH_2230_2300", "WH_2300_2330", "WH_2330_2400" , 
                                             
                                             "WORKING_HOURS", "REST_HOURS","REST_HOURS_ANY24","OverTime_HOURS"  ,"RestHrs7Day","Seafarer_Remarks","Verifier_Remarks"   };




        List<string> lColName = new List<string>();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            string lCoumnName = dt.Columns[i].ToString();
            if (lCoumnName.StartsWith("WH_"))
                lColName.Add(lCoumnName);
        }

        foreach (string itemc in lColName)
        {
            ChangeColumnDataType(dt, itemc, typeof(string));
        }

        foreach (DataRow item in dt.Rows)
        {
            foreach (DataColumn ColumnName in dt.Columns)
            {
                if (ColumnName.ToString().StartsWith("WH_"))
                    if (item[ColumnName.ToString()].ToString() == "0")
                    {
                        item[ColumnName.ToString()] = DBNull.Value;
                    }
                    else
                    {
                        item[ColumnName.ToString()] = "&#10003;";
                    }
            }
        }





        //GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, "CrewHandOver.xls", "Crew Handover");
        if (dt.Rows.Count > 0)
            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "CrewRestHoursReport.xls", "Rest/Work Hour Report for " + dt.Rows[0]["Staff_rank_Code"] + "-" + dt.Rows[0]["Staff_Code"] + "-" + dt.Rows[0]["Staff_Name"]);
        else
        {
            string msg = String.Format("alert('Records not found!');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }

    }
    public static bool ChangeColumnDataType(DataTable table, string columnname, Type newtype)
    {
        if (table.Columns.Contains(columnname) == false)
            return false;

        DataColumn column = table.Columns[columnname];
        if (column.DataType == newtype)
            return true;

        try
        {
            DataColumn newcolumn = new DataColumn("temperary", newtype);
            table.Columns.Add(newcolumn);
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    row["temperary"] = Convert.ChangeType(row[columnname], newtype);
                }
                catch
                {
                }
            }
            table.Columns.Remove(columnname);
            newcolumn.ColumnName = columnname;
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
    protected void txtFrom_TextChanged(object sender, EventArgs e)
    {
        ddlVesselList.SelectedIndex = -1;
    }
    protected void txtTo_TextChanged(object sender, EventArgs e)
    {
        ddlVesselList.SelectedIndex = -1;

    }
}