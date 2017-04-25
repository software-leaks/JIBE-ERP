using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using System.Net;
using System.Web.Script.Serialization;
using SMS.Properties;
using System.Threading;
using System.Text;
using System.IO;
using EO.Pdf;
using System.Drawing;
using SMS.Business.Inspection;
public partial class InspectionReport_Checklist : System.Web.UI.Page
{
    public class CatRating
    {
        public string RNO { get; set; }
        public string Description { get; set; }
        public string LastReport { get; set; }
        public string ThisReport { get; set; }
        public string Rating { get; set; }
    }
    BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
    DataTable dt = new DataTable();
    DataSet dscon = new DataSet();

    DataSet dsCat = new DataSet();
    BLL_Infra_InspectionReportConfig objCon = new BLL_Infra_InspectionReportConfig();
    BLL_INSP_Checklist objChecklist = new BLL_INSP_Checklist();
    CatRating item = new CatRating();
    public List<CatRating> lstRate = new List<CatRating>();
    public UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    public static int CounterRow = 0;

    int newtab = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            if (Request.QueryString["InspID"] != null && Request.QueryString["InspectorID"] != null && Request.QueryString["ScheduleID"] != null)
            {
                if (Request.QueryString["InspID"].ToString().Trim() != "" && Request.QueryString["InspectorID"].ToString() != "" && Request.QueryString["ScheduleID"] != "")
                {
                    int ScheduleID = UDFLib.ConvertToInteger(Request.QueryString["ScheduleID"].ToString());
                    ViewState["ScheduleID"] = Request.QueryString["ScheduleID"].ToString();
                    ViewState["InspectorID"] = Request.QueryString["InspectorID"].ToString();
                    ViewState["InspID"] = Request.QueryString["InspID"].ToString();
                    string CheckListIDS = GetChecklist(ScheduleID);

                    GetVesselAttendance(Convert.ToInt32(Request.QueryString["InspectorID"].ToString()), UDFLib.ConvertToInteger(Request.QueryString["InspID"].ToString()));

                    GetCHCategoryRating(Convert.ToInt32(Request.QueryString["InspID"].ToString()));
                   
                }
            }

        }
    }

    
    public void GetCHCategoryRating(int InspectionID)
    {
        string strTable = "";

        DataTable dtchgroup = objChecklist.Get_WorklistReportWithAllGrouping(InspectionID);



        DataTable dtLocation = new DataTable();
        if (dtchgroup.Rows.Count > 0)
        {
            dtLocation = objChecklist.Get_CheckListLocations(int.Parse(dtchgroup.Rows[0]["VesselType"].ToString()));
        }

        strTable = CreateHtmlTableChecklistWithRatings(dtchgroup, dtLocation, InspectionID.ToString(),// objChecklist.Get_GradesWithOption(), dtLocation,
              new string[] { "", "", "" },
             new string[] { "Description", "NodeType", "CheckList_Type_name" },
             new string[] { "left", "left", "left", "left", "left" },
              "tbl-common-Css",
                "hdr-common-Css-Vertical",
             "row-common-Css-Vertical",
             "row-common-Css-Vertical");

        dvCatWiseJobs.InnerHtml = strTable;

    }



    public string CreateHtmlTableChecklistWithRatings(DataTable dtTable, DataTable dtLocation, string Inspection_ID, string[] HeaderCaptions, string[] DataColumnsName, string[] DataColumnsAlignment, string TableCss, string HeaderCss, string RowStyleCss, string AlternateRowStyleCss)//DataTable dtTableOFGrades, DataTable dtTableLocations,
    {
        StringBuilder strTable = new StringBuilder();
        try
        {
            if (dtTable.Rows.Count > 0)
            {
                strTable.Append("<table id='__tbl_ChecklistRatings' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse; width: 100%;' ");
                if (!string.IsNullOrWhiteSpace(TableCss))
                    strTable.Append(" class='" + TableCss + "' ");
                strTable.Append(" > ");

                int icol = 0;
                int rowCount = 0;
                int itemCount = 0;
                string vesseltype_ID = "";
                bool trflag = false;

                if (dtTable.Rows.Count > 0)
                {
                    int parentPadding = 1;

                    dtTable.DefaultView.RowFilter = "NodeType IS NULL AND Parent_ID IS NULL AND CheckListName IS NOT NULL AND checklistType IS NOT NULL";
                    dtTable.DefaultView.Sort = "Checklist_ID asc";
                    DataTable dtCheckListDiff = dtTable.DefaultView.ToTable();

                    if (dtCheckListDiff.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCheckListDiff.Rows.Count; i++)
                        {
                            //strTable.Append("<tr><td class='insp-Chk-Checklist-td'>");
                            strTable.Append("<tr><td>");
                            strTable.Append("<div class='insp-Chk-Checklist-td'>");
                            strTable.Append("<div class='insp-Chk-Checklist'  id=dv" + dtCheckListDiff.Rows[i][0].ToString() + ">");
                            //strTable.Append("<div id=dv" + dtCheckListDiff.Rows[i][0].ToString() + ">");
                            strTable.Append(dtCheckListDiff.Rows[i][0].ToString().Replace("\n", "<br>"));
                            strTable.Append("</div>");

                            strTable.Append("<div class='insp-Chk-Checklist' id=dv" + dtCheckListDiff.Rows[i][1].ToString() + ">");
                            strTable.Append(dtCheckListDiff.Rows[i][1].ToString().Replace("\n", "<br>"));
                            strTable.Append("</div>");

                            strTable.Append("<div class='insp-Chk-Checklist' >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                            strTable.Append("</div>");

                            strTable.Append("</div>");
                            
                            strTable.Append("</td></tr>");

                            dtTable.DefaultView.RowFilter = "NodeType= 'Group' AND Parent_ID IS NULL AND Checklist_ID =" + dtCheckListDiff.Rows[i][5].ToString() + "";
                            dtTable.DefaultView.Sort = "Index_No asc";
                            DataTable dtgroupDiff = dtTable.DefaultView.ToTable();
                            int groupcnt = dtgroupDiff.Rows.Count;
                            if (dtgroupDiff.Rows.Count > 0)
                            {
                                strTable.Append(getrecurtionWithRatings(dtgroupDiff, dtTable, dtLocation, Inspection_ID, null, "", rowCount, parentPadding, DataColumnsName, groupcnt));//dtTableOFGrades, dtTableLocations,
                            }
                            dtTable.DefaultView.RowFilter = "NodeType= 'Location' AND Parent_ID IS NULL AND Checklist_ID= " + dtCheckListDiff.Rows[i][5].ToString() + "";
                            dtTable.DefaultView.Sort = "Index_No asc";
                            DataTable dtLocationDiff = dtTable.DefaultView.ToTable();
                            if (dtLocationDiff.Rows.Count > 0)
                            {
                                strTable.Append(getrecurtionWithRatings(dtLocationDiff, dtTable, dtLocation, Inspection_ID, null, "", CounterRow, parentPadding, DataColumnsName, groupcnt));//dtTableOFGrades, dtTableLocations,
                            }
                        }
                    }

                }
                strTable.Append("</table>");
            }
           
            return strTable.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }
    /// <summary>
    /// Modified on 26-07-2016
    /// Method returns HTML string  which display details of checklist assigned in inspection with ratings.
    /// </summary>
    /// <param name="dtdiffTable">Group / Location / Item Table </param>
    /// <param name="dtTable">Current CheckList Ratings</param>
    /// <param name="dtLocation">CheckList Locations </param>
    /// <param name="Inspection_ID">Inspection ID for which repost is to be displayed.</param>
    /// <param name="ParentID">ChecklistItem ID / NULL</param>
    /// <param name="index"></param>
    /// <param name="rowCount"> Row Count</param>
    /// <param name="Padding"></param>
    /// <param name="DataColumnsName">Column Name :CheckList_Name", "NodeType", "CheckList_Type_name"</param>
    /// <param name="groupcnt"></param>
    /// <returns>HTML string </returns>
    public string getrecurtionWithRatings(DataTable dtdiffTable, DataTable dtTable, DataTable dtLocation, string Inspection_ID, string ParentID, string index, int rowCount, int Padding, string[] DataColumnsName, int groupcnt)// DataTable dtTableOFGrades, DataTable dtTableLocations,
    {
        StringBuilder strTable = new StringBuilder();
        strTable.Append("<tr><td>");
        int GroupCount = 0;
        int LocCount = 0;
        int QueCount = 0;
        int tdtrCount = 0;

        if (ParentID == null)
        {
            foreach (DataRow dr in dtdiffTable.Rows)
            {
                rowCount++;

                tdtrCount += 1;
                rowCount += 1;
                if (dr["Parent_ID"] == DBNull.Value)
                {
                    if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Group")
                    {
                        GroupCount++;
                        if (GroupCount > 1)
                        {
                            strTable.Append("<tr><td>");
                        }
                        strTable.Append("<div class='insp-Chk-Group insp-Item-Pos" + Padding.ToString() + "' id=dvGrp" + (rowCount).ToString() + "> <table style='width:100%'><tr><td style='text-align:left'>");
                        strTable.Append("<span class='insp-IndexNo' > " + GroupCount.ToString().Replace("\n", "<br>") + ".&nbsp</span>");
                        strTable.Append(dr["Description"].ToString().Replace("\n", "<br>"));
                        strTable.Append("</td><td style='text-align:right'>");

                        if ((dr["Node_Value"].ToString()) != "")
                            strTable.Append("<span class='insp-OptionText' > " + dr["Node_Value"].ToString().Replace("\n", "<br>") + "</span>");

                        strTable.Append("</td></tr></table>");
                        strTable.Append("</div>");
                        strTable.Append("</td></tr>");

                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Group'";
                        dtTable.DefaultView.Sort = "Index_No asc";
                        DataTable dtGroupDiff = dtTable.DefaultView.ToTable();
                        if (dtGroupDiff.Rows.Count > 0)
                        {
                            CounterRow = rowCount;
                            strTable.Append(getrecurtionWithRatings(dtGroupDiff, dtTable, dtLocation, Inspection_ID, dr["ChecklistItem_ID"].ToString(), GroupCount.ToString(), CounterRow, Padding + 1, DataColumnsName, groupcnt));//dtTableOFGrades, dtTableLocations,
                        }

                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Location'";
                        dtTable.DefaultView.Sort = "Index_No asc";
                        DataTable dtLocationDiff = dtTable.DefaultView.ToTable();

                        if (dtLocationDiff.Rows.Count > 0)
                        {
                            CounterRow = rowCount;
                            strTable.Append(getrecurtionWithRatings(dtLocationDiff, dtTable, dtLocation, Inspection_ID, dr["ChecklistItem_ID"].ToString(), GroupCount.ToString(), CounterRow, Padding + 1, DataColumnsName, groupcnt));// dtTableOFGrades, dtTableLocations, 
                        }
                    }
                    else if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Location" && dr["Parent_ID"] == DBNull.Value)
                    {
                        LocCount++;
                        int indxNO = groupcnt + LocCount;
                        if (LocCount > 1)
                        {
                            strTable.Append("<tr><td>");
                        }

                        strTable.Append("<div  class='insp-Chk-Location insp-Item-Pos" + Padding.ToString() + "' id=dvLocation" + (rowCount).ToString() + "> <table style='width:100%'><tr><td style='text-align:left'>");
                        strTable.Append("<span class='insp-IndexNo' > " + indxNO.ToString().Replace("\n", "<br>") + ".&nbsp</span>");
                        strTable.Append(dr["Description"].ToString().Replace("\n", "<br>"));

                        if (dr["Location_ID"].ToString() != "")
                        {
                            dtLocation.DefaultView.RowFilter = "ID= '" + dr["Location_ID"].ToString() + "'";
                            DataTable dtLocationDiff = dtLocation.DefaultView.ToTable();
                            if (dtLocationDiff.Rows.Count > 0)
                                strTable.Append("<label class='insp-Chk-Location-Text' >&nbsp;" + dtLocationDiff.Rows[0]["LocationName"].ToString().Replace("\n", "<br>") + "</label>");
                        }

                        strTable.Append("</td><td style='text-align:right'>");
                        if ((dr["Node_Value"].ToString()) != "")
                            strTable.Append("<span > " + dr["Node_Value"].ToString().Replace("\n", "<br>") + "</span>");

                        strTable.Append("</td></tr></table>");
                        strTable.Append("</div>");

                        string strItemID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                        string indexNo = index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>");

                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "'";
                        dtTable.DefaultView.Sort = "Index_No asc";
                        DataTable dtItemDiffCount = dtTable.DefaultView.ToTable();
                        if (dtItemDiffCount.Rows.Count == 0)
                        {
                            strTable.Append("<div style='width: 99%;padding-left: 10px;'>");
                            strTable.Append(Search_Worklist(dr["Location_ID"].ToString(), dr["ChecklistItem_ID"].ToString(), ViewState["InspID"].ToString()));
                            strTable.Append("</div>");
                        }

                        strTable.Append("</td></tr>");
                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "'";
                        dtTable.DefaultView.Sort = "Index_No asc";
                        DataTable dtItemDiff = dtTable.DefaultView.ToTable();
                        if (dtItemDiff.Rows.Count > 0)
                        {
                            CounterRow = rowCount;
                            strTable.Append(getrecurtionWithRatings(dtItemDiff, dtTable, dtLocation, Inspection_ID, dr["ChecklistItem_ID"].ToString(), indexNo, CounterRow, Padding + 1, DataColumnsName, indxNO));
                        }                        
                    }
                }
            }
        }
        else
        {
            foreach (DataRow dr in dtdiffTable.Rows)
            {
                rowCount++;
                tdtrCount += 1;
                if (dr["Parent_ID"] != DBNull.Value)
                {
                    if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Group")
                    {
                        GroupCount++;
                        strTable.Append("<div class='insp-Chk-Group insp-Item-Pos" + Padding.ToString() + "' id=dvGrp" + (rowCount).ToString() + "> <table style='width:100%'><tr><td style='text-align:left'>");
                        strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + GroupCount.ToString().Replace("\n", "<br>") + "&nbsp</span>");


                        strTable.Append(dr["Description"].ToString().Replace("\n", "<br>"));

                        strTable.Append("</td><td style='text-align:right'>");

                        if ((dr["Node_Value"].ToString()) != "")
                            strTable.Append("<span class='insp-OptionText' > " + dr["Node_Value"].ToString().Replace("\n", "<br>") + "</span>");

                        strTable.Append("</td></tr></table>");

                        strTable.Append("</div>");
                        strTable.Append("</td></tr>");

                        string indexNoval = index.ToString() + "." + GroupCount.ToString().Replace("\n", "<br>");

                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Group'";
                        dtTable.DefaultView.Sort = "Index_No asc";
                        DataTable dtGroupDiff = dtTable.DefaultView.ToTable();
                        if (dtGroupDiff.Rows.Count > 0)
                        {
                            CounterRow = rowCount;
                            strTable.Append(getrecurtionWithRatings(dtGroupDiff, dtTable, dtLocation, Inspection_ID, dr["ChecklistItem_ID"].ToString(), indexNoval, CounterRow, Padding + 1, DataColumnsName, groupcnt));
                        }

                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Location'";
                        dtTable.DefaultView.Sort = "Index_No asc";
                        DataTable dtLocationDiff = dtTable.DefaultView.ToTable();

                        if (dtLocationDiff.Rows.Count > 0)
                        {
                            CounterRow = rowCount;
                            strTable.Append(getrecurtionWithRatings(dtLocationDiff, dtTable, dtLocation, Inspection_ID, dr["ChecklistItem_ID"].ToString(), indexNoval, CounterRow, Padding + 1, DataColumnsName, groupcnt));
                        }

                    }
                    else if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Location")
                    {
                        LocCount++;
                        if (LocCount > 1)
                        {
                            strTable.Append("<tr><td>");
                        }

                        strTable.Append("<div  class='insp-Chk-Location insp-Item-Pos" + Padding.ToString() + "' id=dvLocation" + (rowCount).ToString() + "> <table style='width:100%'><tr><td style='text-align:left'>");
                        strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>") + "&nbsp</span>");

                        strTable.Append(dr["Description"].ToString().Replace("\n", "<br>"));

                        if (dr["Location_ID"].ToString() != "")
                        {
                            dtLocation.DefaultView.RowFilter = "ID= '" + dr["Location_ID"].ToString() + "'";
                            DataTable dtLocationDiff = dtLocation.DefaultView.ToTable();
                            if (dtLocationDiff.Rows.Count > 0)
                                strTable.Append("<label class='insp-Chk-Location-Text' >&nbsp;" + dtLocationDiff.Rows[0]["LocationName"].ToString().Replace("\n", "<br>") + "</label>");
                        }

                        strTable.Append("</td><td style='text-align:right'>");

                        if ((dr["Node_Value"].ToString()) != "")
                        {
                            decimal nodeval = Convert.ToDecimal(dr["Node_Value"].ToString());//Modified on :26-07-2016|| To display star ratings in float values .
                            strTable.Append("<div  > " + nodeval.ToString() + "</div>");
                        }

                        strTable.Append("</td></tr></table>");

                        strTable.Append("</div>");

                        string strItemID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                        string indexNo = index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>");

                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "'";
                        dtTable.DefaultView.Sort = "Index_No asc";
                        DataTable dtItemDiffCount = dtTable.DefaultView.ToTable();
                        if (dtItemDiffCount.Rows.Count == 0)
                        {
                            strTable.Append("<div style='width: 99%;padding-left: 10px;'>");
                            strTable.Append(Search_Worklist(dr["Location_ID"].ToString(), dr["ChecklistItem_ID"].ToString(), ViewState["InspID"].ToString()));
                            strTable.Append("</div>");
                        }

                        strTable.Append("</td></tr>");
                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "'";
                        dtTable.DefaultView.Sort = "Index_No asc";
                        DataTable dtItemDiff = dtTable.DefaultView.ToTable();
                        if (dtItemDiff.Rows.Count > 0)
                        {
                            CounterRow = rowCount;
                            strTable.Append(getrecurtionWithRatings(dtItemDiff, dtTable, dtLocation, Inspection_ID, dr["ChecklistItem_ID"].ToString(), indexNo, CounterRow, Padding + 1, DataColumnsName, groupcnt));
                        }
                    }
                    else if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Item")
                    {
                        QueCount++;
                        if (QueCount > 1)
                        {
                            strTable.Append("<tr><td>");
                        }
                        string strItemID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                        strTable.Append("<div class='insp-Chk-Item-Container insp-Item-Pos" + Padding.ToString() + "'  id=dvItem-" + (rowCount).ToString() + "> <table style='width:100%'><tr><td style='text-align:left'>");
                        strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + QueCount.ToString() + "&nbsp</span>");

                        strTable.Append(dr["Description"].ToString().Replace("\n", "<br>"));
                        strTable.Append("</td><td style='text-align:right'>");

                        if ((dr["Node_Value"].ToString()) != "")
                            strTable.Append("<span class='insp-OptionText1' > " + (dr["OptionText"].ToString()) + "</span>");
                        strTable.Append("</td></tr></table>");
                        strTable.Append("</div>");
                        
                        strTable.Append("<div style='width: 99%;padding-left: 10px;'>");
                        strTable.Append(Search_Worklist(dr["Location_ID"].ToString(), dr["ChecklistItem_ID"].ToString(), ViewState["InspID"].ToString()));
                        strTable.Append("</div>");

                        strTable.Append("</td></tr>");
                    }
                }
            }
        }
        return strTable.ToString();
    }


    protected string Search_Worklist(string LocationID, string LocationNodeID, string InspID)
    {
        try
        {
            string response = "";
            BLL_Tec_Inspection objInsp1 = new BLL_Tec_Inspection();
            DataTable dtFilter = new DataTable();
            dtFilter.Columns.Add("PRM_NAME", typeof(string));
            dtFilter.Columns.Add("PRM_VALUE", typeof(object));

            dtFilter.Rows.Add(new object[] { "@FLEET_ID", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@VESSEL_ID", UDFLib.ConvertIntegerToNull(ViewState["Vessel_ID"]) });
            dtFilter.Rows.Add(new object[] { "@ASSIGNOR", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DEPT_SHIP", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DEPT_OFFICE", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@PRIORITY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_NATURE", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_PRIMARY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_SECONDARY", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_MINOR", null, });
            dtFilter.Rows.Add(new object[] { "@JOB_DESCRIPTION", UDFLib.ConvertStringToNull(null) });
            dtFilter.Rows.Add(new object[] { "@JOB_STATUS", UDFLib.ConvertStringToNull(null) });
            dtFilter.Rows.Add(new object[] { "@JOB_TYPE", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@PIC", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@JOB_MODIFIED_IN", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DATE_RAISED_FROM", UDFLib.ConvertDateToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DATE_RAISED_TO", UDFLib.ConvertDateToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DATE_CMPLTN_FROM", UDFLib.ConvertDateToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DATE_CMPLTN_TO", UDFLib.ConvertDateToNull(null) });
            dtFilter.Rows.Add(new object[] { "@DEFER_TO_DD", (null) });
            dtFilter.Rows.Add(new object[] { "@SENT_TO_SHIP", (null) });
            dtFilter.Rows.Add(new object[] { "@HAVING_REQ_NO", (null) });
            dtFilter.Rows.Add(new object[] { "@FLAGGED_FOR_MEETING", (null) });
            dtFilter.Rows.Add(new object[] { "@INSPECTOR", UDFLib.ConvertIntegerToNull(null) });
            dtFilter.Rows.Add(new object[] { "@PAGE_INDEX", (null) });
            dtFilter.Rows.Add(new object[] { "@PAGE_SIZE", (null) });
            dtFilter.Rows.Add(new object[] { "@LOCATION_ID", UDFLib.ConvertIntegerToNull(LocationID) });
            dtFilter.Rows.Add(new object[] { "@NODE_ID", UDFLib.ConvertIntegerToNull(LocationNodeID) });
            dtFilter.Rows.Add(new object[] { "@InspectionID", UDFLib.ConvertIntegerToNull(InspID) });

            int Record_Count = 0;


            DataSet taskTable = objInsp1.INSP_Get_Worklist(dtFilter, ref Record_Count);
            if (taskTable.Tables.Count > 0)
            {
                if (taskTable.Tables[0].Rows.Count > 0)
                {
                     string strTable = CreateHtmlTableFromDataTable(taskTable.Tables[0], new string[] { "Job Code", "DESCRIPTION", "DATE RAISED", "Dept. On Ship", "TYPE", "PRIORITY", "STATUS" }, new string[] { "WLID_DISPLAY", "JOB_DESCRIPTION", "DATE_RAISED", "ONSHIP_DEPT", "Type", "PRIORITY", "WORKLIST_STATUS" }, new string[] { "50px", "300px", "70px", "100px", "120px", "100px", "100px" }, "");
					response = strTable;
                }
            }
            return response;
        }
        catch (Exception ex)
        {
            string js = "alert('Error in loading data!! Error: " + UDFLib.ReplaceSpecialCharacter(ex.Message) + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
            return "";
        }
    }

    public string CreateHtmlTableFromDataTable(DataTable dtTable, string[] HeaderCaptions, string[] DataColumnsName, string[] ColumnsWidth, string PageHeader)
    {
        StringBuilder strTable = new StringBuilder();

        try
        {

            if (dtTable.Rows.Count > 0)
            {
                strTable.Append("<table id='__tbl_remark' class='CreateHtmlTableFromDataTable-table' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse'  >");
                if (PageHeader.Length > 1)
                    strTable.Append("<tr> <td class='CreateHtmlTableFromDataTable-PageHeader'  colspan='" + dtTable.Columns.Count + "' > <b>" + PageHeader + "</b> </td></tr>");

                if (HeaderCaptions.Length > 0)
                {
                    strTable.Append("<tr >");
                    for (int i = 0; i < HeaderCaptions.Length; i++)
                    {
                        strTable.Append("<td class='CreateHtmlTableFromDataTable-DataHedaer' width='" + ColumnsWidth[i] + "'>");
                        strTable.Append("<b>" + HeaderCaptions[i] + "</b>");
                        strTable.Append("</td>");
                    }
                    strTable.Append("</tr>");
                }
                foreach (DataRow dr in dtTable.Rows)
                {
                    strTable.Append("<tr>");
                    for (int i = 0; i < DataColumnsName.Length; i++)
                    {
                        if (i == 0 || i == 4 || i == 6 || i == 5)
                        {
                            strTable.Append("<td class='CreateHtmlTableFromDataTable-Data' style='White-space:normal;width:" + ColumnsWidth[i] + ";text-align:center;'>");
                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                            strTable.Append("</td>");
                        }
                        else
                        {
                            strTable.Append("<td class='CreateHtmlTableFromDataTable-Data' style='White-space:normal;width:" + ColumnsWidth[i] + "'>");
                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                            strTable.Append("</td>");
                        }
                    }
                    strTable.Append("</tr>");

                    // Attachment//
                    BLL_Tec_Inspection objBLL = new BLL_Tec_Inspection();
                    DataTable dt = objBLL.Get_Worklist_Attachments(Convert.ToInt32(dr["Vessel_ID"]), Convert.ToInt32(dr["Worklist_id"]), Convert.ToInt32(dr["OFFICE_ID"]), 0);
                    DataView dvImage = dt.DefaultView;
                    dvImage.RowFilter = "Is_Image='1' ";

                    strTable.Append("<tr>");
                    strTable.Append("<td colspan='" + DataColumnsName.Length.ToString() + "' class='CreateHtmlTableFromDataTable-Data' style='background-color:#F0F0F0; '>");
                    int imgRun = 1;
                    foreach (DataRow drimg in dvImage.ToTable().Rows)
                    {
                        strTable.Append("<a href='" + Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" + "uploads/technical/" + drimg["Image_Path"].ToString() + "'><img src='" + Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/" + "uploads/technical/" + drimg["Image_Path"].ToString() + "' alt='' height='250' width='250px'  style='margin:5px;'   ></a>");


                        if (imgRun == 3)
                        {
                            strTable.Append("</td>");
                            strTable.Append("</tr>");
                            strTable.Append("<tr>");
                            strTable.Append("<td colspan='" + DataColumnsName.Length.ToString() + "' class='CreateHtmlTableFromDataTable-Data'>");
                            imgRun = 1;
                        }


                        imgRun++;

                    }

                    strTable.Append("</td>");
                    strTable.Append("</tr>");

                    //attachment/
                }
                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'> No record found !</span>");

            return strTable.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }


    public string GetChecklist(int ScheduleID)
    {
        DataTable ds = objChecklist.Get_CheckListName(ScheduleID);
        string ChecklistID = string.Empty;
        if (ds.Rows.Count > 0)
        {

            for (int i = 0; i < ds.Rows.Count; i++)
            {
                if (ChecklistID != "")
                {
                    ChecklistID = ChecklistID + "," + ds.Rows[i]["ChecklistID"].ToString();
                }
                else
                {
                    ChecklistID = ds.Rows[i]["ChecklistID"].ToString();
                }


            }
        }

        return ChecklistID;
    }
    public void PullRatingFromChecklistRating(int InspectionID, string CheckListIDs)
    {
        try
        {

            objInsp.INSP_Get_RatingsFromChecklistRating(InspectionID, CheckListIDs);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void GetConfiguration()
    {
        dscon = objCon.INSP_Get_ReportConfig();
        if (dscon.Tables[0].Rows.Count > 0)
        {
            grdReportConfig.DataSource = dscon.Tables[0];
            grdReportConfig.DataBind();

            updReportConfig.Update();


        }
    }
    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUA.View == 0)
        {
            Response.Redirect("~/crew/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {

        }
    }
    public void GetExecutiveSummary()
    {
        string js2 = "GetExecutiveSummary();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ExeSummary", js2, true);
    }
    public void GetCategoryRating()
    {
        string js3 = "GetCategoryRating();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CatRating", js3, true);
    }
    public void GetWorklist()
    {
        string js3 = "GetWorklist();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "worklist", js3, true);
    }
    public void ReplaceDropDownWithLabel()
    {
        string js3 = "ReplaceDropDownWithLabel();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "worklist", js3, true);
    }
    public void GetSubCategoryRating()
    {
      

        if (dsCat.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsCat.Tables[0].Rows.Count; i++)
            {
                string SystemCode = dsCat.Tables[0].Rows[i][1].ToString();
                string js3 = "GetSubCategoryRating(" + SystemCode + ");";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SubCatRating" + i, js3, true);
            }
        }

    }

    public void BindCategoryWiseJobs(int InspectionID, int ShowImages, string ReportType, string ChecklistIDs)
    {

        char[] c = ReportType.ToCharArray();
        string RptType = c[1].ToString();
        DataTable dt = objInsp.INSP_Get_WorklistReportWithCategoryGrouping(InspectionID, ShowImages, RptType, ChecklistIDs);
        dvCatWiseJobs.InnerHtml = dt.Rows[0][0].ToString();

    }
    public void BindCategoryRating(string InspectionID, string CheckListIDs)
    {
        try
        {
            JavaScriptSerializer j = new JavaScriptSerializer();
            dsCat = objInsp.GetCategoryRating(InspectionID, CheckListIDs);

            if (dsCat.Tables[0].Rows.Count > 0)
            {
                // dsCat.Tables[0].

                for (int i = 0; i < dsCat.Tables[0].Rows.Count; i++)
                {
                    item = new CatRating();
                    item.RNO = dsCat.Tables[0].Rows[i][0].ToString();
                    item.Description = dsCat.Tables[0].Rows[i][2].ToString();
                    item.LastReport = dsCat.Tables[0].Rows[i][3].ToString();
                    item.ThisReport = dsCat.Tables[0].Rows[i][4].ToString();
                    item.Rating = dsCat.Tables[0].Rows[i][5].ToString();

                    lstRate.Add(item);
                }

                string js4 = "drawChartSummaryRating(" + j.Serialize(lstRate) + ");";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CatRatingChart", js4, true);


            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public void GetVesselAttendance(int InspectorID, int InspectionID)
    {
        dt = objInsp.Get_VesselAttendence(InspectorID, InspectionID);

        if (dt.Rows.Count > 0)
        {
            ViewState["Vessel_ID"] = dt.Rows[0][0].ToString();
            lblShipName.Text = dt.Rows[0][1].ToString();
            lblAttendendBy.Text = dt.Rows[0][7].ToString();
            lblFrom.Text = Convert.ToDateTime(dt.Rows[0][2].ToString()).ToString("dd MMM yyyy");
            lblPorts.Text = dt.Rows[0][5].ToString();

           
        }


    }

    protected void BtnConfig_Click(object sender, ImageClickEventArgs e)
    {
        GetConfiguration();

        string js4 = "ShowModalPopup('dvConfig');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "rptConfig", js4, true);

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int KeyNo, ActiveStatus;

        for (int i = 0; i < grdReportConfig.Rows.Count; i++)
        {
            KeyNo = Convert.ToInt32(grdReportConfig.Rows[i].Cells[0].Text);
            ActiveStatus = Convert.ToInt32(((CheckBox)grdReportConfig.Rows[i].Cells[3].FindControl("chkActive")).Checked);
            objCon.INSP_Update_InspectionReportConfigStaus(KeyNo, ActiveStatus, Convert.ToInt32(Session["USERID"]), DateTime.Now);
        }
        string js4 = "hideModal('dvConfig');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "rptConfigHide", js4, true);
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        string js4 = "hideModal('dvConfig');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "rptConfigHide", js4, true);
    }


    protected void BtnPrintPDF_Click(object sender, ImageClickEventArgs e)
    {

        EO.Pdf.HtmlToPdf.Options.PageSize = new SizeF(11.69f, 8.27f);

        PdfDocument doc = new PdfDocument();

        string GUID = Guid.NewGuid().ToString();
        string filePath = Server.MapPath("~/Uploads/Reports/" + GUID + ".pdf");
        string FileName = "~/Uploads/Reports/" + GUID + ".pdf";
       
        EO.Pdf.Runtime.AddLicense("p+R2mbbA3bNoqbTC4KFZ7ekDHuio5cGz4aFZpsKetZ9Zl6TNHuig5eUFIPGe" +
      "tcznH+du5PflEuCG49jjIfewwO/o9dB2tMDAHuig5eUFIPGetZGb566l4Of2" +
      "GfKetZGbdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW6yuCwb6y9xtyxdabw" +
      "+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzr" +
      "peb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mkBxDxrODz/+ihb6W0" +
      "s8uud4SOscufWbOz8hfrqO7CnrWfWZekzRrxndz22hnlqJfo8h8=");
        HtmlToPdf.ConvertHtml(hdnContent.Value, filePath);

        newtab = UDFLib.ConvertToInteger(ViewState["newtabnumber"]);
        newtab++;
        ViewState["newtabnumber"] = newtab;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideText", "window.open('../../Uploads/Reports/" + GUID + ".pdf','INSPRPT" + newtab + "');", true);  // (  this.GetType(), "OpenWindow", "window.open('../../Uploads/InspectionReport.pdf','_newtab');", true);
       
    }
}