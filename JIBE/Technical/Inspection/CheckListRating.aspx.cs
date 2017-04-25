using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.Text;
using SMS.Properties;
using SMS.Business.Inspection;
public partial class Technical_Inspection_CheckListRating : System.Web.UI.Page
{
    BLL_Infra_VesselLib objBLLVesslType = new BLL_Infra_VesselLib();
    BLL_INSP_Checklist objBLLCheckList = new BLL_INSP_Checklist();

    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdnfldUserID.Value = Convert.ToString(Session["USERID"]);
            //FillDDLVesselTYPE();
            //FillDDLChecklistTYPE();
            //FillDDLGrades();
            UserAccessValidation();
            
            string id = "";
            ////int id = 0;
            if (Request.QueryString["ScheduleID"] != null)
            {
                id = Request.QueryString["ScheduleID"];
                //id = Convert.ToInt32(Request.QueryString["CHKID"]);
            }

            if (id != "")
            {
                hdnQuerystring.Value = id;
            }
            
            string Inspid = "";
            if(Request.QueryString["InspID"]!= null)
            {
                Inspid = Request.QueryString["InspID"];
                //id = Convert.ToInt32(Request.QueryString["CHKID"]);
            }
            if (Inspid != "")
            {
                hdnQuerystringInspID.Value = Inspid;
            }
            string vesselID = "";
            if (Request.QueryString["VesselID"] != null)
            {
                vesselID = Request.QueryString["VesselID"];
            }
            if (vesselID != "")
            {
                hdnVesselID.Value = vesselID;
            }
        }

        //getchecklistrates();
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
            //btnScheduleInspection.Enabled = false;

        }
        ViewState["del"] = objUA.Delete;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    //public void getchecklistrates()
    //{
    //    BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();
    //    DataTable dt= objBl.Get_CurrentCheckListRatings(1);
    //     DataTable dtLocation =new DataTable();
    //     DataTable dtGradesOption = new DataTable();
    //    if (dt.Rows.Count > 0)
    //    {
    //       dtLocation = objBl.Get_CheckListLocations(int.Parse(dt.Rows[0]["Vessel_Type"].ToString()));
    //    }

    //   dtGradesOption= objBl.Get_GradesWithOption();
    //}

    //public static string getrecurtionWithRatings(DataTable dtdiffTable, DataTable dtTable, DataTable dtTableOFGrades, DataTable dtTableLocations, DataTable dtaffect, string ParentID)
    //{
    //    //int rowCount = 0;
    //    StringBuilder strTable = new StringBuilder();
    //    strTable.Append("<tr><td");
       
    //    if (ParentID == null)
    //    {
    //        foreach (DataRow dr in dtdiffTable.Rows)
    //        {                
    //            if (dr["Parent_ID"] == DBNull.Value)
    //            {
    //                if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Group")
    //                {
    //                    dtaffect.Rows.Add(dr);
                        
    //                    dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Group'";
    //                    dtTable.DefaultView.Sort = "Index_No asc";
    //                    DataTable dtGroupDiff = dtTable.DefaultView.ToTable();
    //                    if (dtGroupDiff.Rows.Count > 0)
    //                    {
    //                        getrecurtionWithRatings(dtGroupDiff, dtTable, dtTableOFGrades, dtTableLocations, dtaffect, dr["ChecklistItem_ID"].ToString());
    //                    }

    //                    dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Location'";
    //                    dtTable.DefaultView.Sort = "Index_No asc";
    //                    DataTable dtLocationDiff = dtTable.DefaultView.ToTable();

    //                    if (dtLocationDiff.Rows.Count > 0)
    //                    {
    //                        getrecurtionWithRatings(dtGroupDiff, dtTable, dtTableOFGrades, dtTableLocations, dtaffect, dr["ChecklistItem_ID"].ToString());
    //                    }
    //                }
    //                else if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Location" && dr["Parent_ID"] == DBNull.Value)
    //                {
                        
    //                    //strTable.Append("parent Location   ");

    //                    dtaffect.Rows.Add(dr);

    //                    //if (dtTableLocations.Rows.Count > 0)
    //                    //{
    //                    //    dtTableLocations.DefaultView.RowFilter = "ID= '" + dr["Location_ID"].ToString() + "'";

    //                    //    DataTable dtLocationDiff = dtTableLocations.DefaultView.ToTable();
    //                    //    if (dtLocationDiff.Rows.Count > 0)
    //                    //    {
    //                    //        strTable.Append("<div  class='insp-Chk-Location insp-Item-Pos" + Padding.ToString() + "' id=dvLocation" + (rowCount).ToString() + ">");
    //                    //        strTable.Append("<span class='insp-IndexNo' > " + LocCount.ToString().Replace("\n", "<br>") + ".&nbsp</span>");
    //                    //        //strTable.Append("<span class='insp-IndexNo' > " + dr["Index_No"].ToString().Replace("\n", "<br>") + ".&nbsp</span>");
    //                    //        strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
    //                    //        strTable.Append("<label class='insp-Chk-Location-Text' >&nbsp;" + dtLocationDiff.Rows[0]["LocationName"].ToString().Replace("\n", "<br>") + "</label>");
    //                    //        strTable.Append("</div>");

    //                    //        dtTableOFGrades.DefaultView.RowFilter = "ID= '" + LocGradeType + "'";
    //                    //        DataTable dtLocationRatingDiff = dtTableOFGrades.DefaultView.ToTable();

    //                    //        if (dtLocationRatingDiff.Rows.Count > 0)
    //                    //        {
    //                    //            strTable.Append("<span class='rating'>");
    //                    //            for (int i = dtLocationRatingDiff.Rows.Count - 1; i >= 0; i--)
    //                    //            {
    //                    //                string strOptionText = dtLocationRatingDiff.Rows[i]["OptionText"].ToString();
    //                    //                string strChecklistID = dr["Checklist_IDCopy"].ToString().Replace("\n", "<br>");
    //                    //                string optionID = (rowCount).ToString() + "-" + (i + 1).ToString();

    //                    //                string strID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
    //                    //                //ratingstarCount++;
    //                    //                if (dr["LocationRates"].ToString().Replace("\n", "<br>") != "")
    //                    //                {

    //                    //                    if (Convert.ToDecimal(dr["LocationRates"].ToString().Replace("\n", "<br>")) == Convert.ToDecimal(dtLocationRatingDiff.Rows[i]["OptionValue"].ToString()))
    //                    //                    {
    //                    //                        strTable.Append("<input type='radio' checked=true class='rating-input' id='rating-input-" + optionID + "' name='rating-input-" + (rowCount).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' onclick='Get_Ratings(\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ",\"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");'> <label for='rating-input-1-" + (i + 1).ToString() + "' class='rating-star'></label>");
    //                    //                    }
    //                    //                    else
    //                    //                    {
    //                    //                        strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + optionID + "' name='rating-input-" + (rowCount).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' onclick='Get_Ratings(\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ",\"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");'> <label for='rating-input-1-" + (i + 1).ToString() + "' class='rating-star'></label>");
    //                    //                    }
    //                    //                }
    //                    //                else
    //                    //                {
    //                    //                    strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + optionID + "' name='rating-input-" + (rowCount).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' onclick='Get_Ratings(\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ",\"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");'> <label for='rating-input-1-" + (i + 1).ToString() + "' class='rating-star'></label>");
    //                    //                }
    //                    //            }
    //                    //            strTable.Append("</span>");
    //                    //        }
    //                    //    }
    //                    //}

    //                    strTable.Append("</td></tr>");
    //                    dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "'";
    //                    dtTable.DefaultView.Sort = "Index_No asc";
    //                    DataTable dtItemDiff = dtTable.DefaultView.ToTable();
    //                    if (dtItemDiff.Rows.Count > 0)
    //                    {
    //                        strTable.Append(getrecurtionWithRatings(dtItemDiff, dtTable, dtTableOFGrades, dtTableLocations, LocGradeType, Inspection_ID, dr["ChecklistItem_ID"].ToString(), LocCount.ToString(), rowCount, Padding + 1, DataColumnsName, dicJSEvent, DicToolTip));
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        foreach (DataRow dr in dtdiffTable.Rows)
    //        {
              
    //            if (dr["Parent_ID"] != DBNull.Value)
    //            {
    //                if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Group")
    //                {
                       
    //                    //strTable.Append("Sub parent Group   ");
    //                    strTable.Append("<div class='insp-Chk-Group insp-Item-Pos" + Padding.ToString() + "' id=dvGrp" + (rowCount).ToString() + ">");
    //                    //strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + dr["Index_No"].ToString().Replace("\n", "<br>") + "&nbsp</span>");
    //                    strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + GroupCount.ToString().Replace("\n", "<br>") + "&nbsp</span>");
    //                    strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
    //                    strTable.Append("</div>");
    //                    strTable.Append("</td></tr>");

    //                    string indexNoval = index.ToString() + "." + GroupCount.ToString().Replace("\n", "<br>");

    //                    dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Group'";
    //                    dtTable.DefaultView.Sort = "Index_No asc";
    //                    DataTable dtGroupDiff = dtTable.DefaultView.ToTable();
    //                    if (dtGroupDiff.Rows.Count > 0)
    //                    {
    //                        strTable.Append(getrecurtionWithRatings(dtGroupDiff, dtTable, dtTableOFGrades, dtTableLocations, LocGradeType, Inspection_ID, dr["ChecklistItem_ID"].ToString(), indexNoval, rowCount, Padding + 1, DataColumnsName, dicJSEvent, DicToolTip));
    //                    }

    //                    dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Location'";
    //                    dtTable.DefaultView.Sort = "Index_No asc";
    //                    DataTable dtLocationDiff = dtTable.DefaultView.ToTable();

    //                    if (dtLocationDiff.Rows.Count > 0)
    //                    {
    //                        strTable.Append(getrecurtionWithRatings(dtLocationDiff, dtTable, dtTableOFGrades, dtTableLocations, LocGradeType, Inspection_ID, dr["ChecklistItem_ID"].ToString(), indexNoval, rowCount, Padding + 1, DataColumnsName, dicJSEvent, DicToolTip));
    //                    }
    //                }
    //                else if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Location")
    //                {
    //                    LocCount++;
    //                    if (dtTableLocations.Rows.Count > 0)
    //                    {
    //                        dtTableLocations.DefaultView.RowFilter = "ID= '" + dr["Location_ID"].ToString() + "'";

    //                        DataTable dtLocationDiff = dtTableLocations.DefaultView.ToTable();
    //                        if (dtLocationDiff.Rows.Count > 0)
    //                        {
    //                            strTable.Append("<div  class='insp-Chk-Location insp-Item-Pos" + Padding.ToString() + "' id=dvLocation" + (rowCount).ToString() + ">");
    //                            //strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + dr["Index_No"].ToString().Replace("\n", "<br>") + "&nbsp</span>");
    //                            strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>") + "&nbsp</span>");
    //                            strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
    //                            strTable.Append("<label class='insp-Chk-Location-Text' >&nbsp;" + dtLocationDiff.Rows[0]["LocationName"].ToString().Replace("\n", "<br>") + "</label>");

    //                            dtTableOFGrades.DefaultView.RowFilter = "ID= '" + LocGradeType + "'";
    //                            DataTable dtLocationRatingDiff = dtTableOFGrades.DefaultView.ToTable();

    //                            strTable.Append("<span class='rating'>");
    //                            for (int i = dtLocationRatingDiff.Rows.Count - 1; i >= 0; i--)
    //                            {
    //                                string strOptionText = dtLocationRatingDiff.Rows[i]["OptionText"].ToString();
    //                                string strChecklistID = dr["Checklist_IDCopy"].ToString().Replace("\n", "<br>");

    //                                string optionID = (rowCount).ToString() + "-" + (i + 1).ToString();

    //                                string strID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");

    //                                if (dr["LocationRates"].ToString().Replace("\n", "<br>") != "")
    //                                {

    //                                    if (Convert.ToDecimal(dr["LocationRates"].ToString().Replace("\n", "<br>")) == Convert.ToDecimal(dtLocationRatingDiff.Rows[i]["OptionValue"].ToString()))
    //                                    {
    //                                        strTable.Append("<input type='radio' checked=true class='rating-input' id='rating-input-" + optionID + "' name='rating-input-" + (rowCount).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-1-" + (i + 1).ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
    //                                    }
    //                                    else
    //                                    {
    //                                        strTable.Append("<input type='radio'  class='rating-input' id='rating-input-" + optionID + "' name='rating-input-" + (rowCount).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-1-" + (i + 1).ToString() + "' class='rating-star'  onclick='Get_Ratings(\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");'></label>");
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    strTable.Append("<input type='radio'  class='rating-input' id='rating-input-" + optionID + "' name='rating-input-" + (rowCount).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-1-" + (i + 1).ToString() + "' class='rating-star'  onclick='Get_Ratings(\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");'></label>");
    //                                }


    //                            }
    //                            strTable.Append("</span>");

    //                            strTable.Append("</div>");

    //                        }
    //                    }

    //                    strTable.Append("</td></tr>");

    //                    string indexNoval = index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>");

    //                    dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "'";
    //                    dtTable.DefaultView.Sort = "Index_No asc";
    //                    DataTable dtItemDiff = dtTable.DefaultView.ToTable();
    //                    if (dtItemDiff.Rows.Count > 0)
    //                    {
    //                        strTable.Append(getrecurtionWithRatings(dtItemDiff, dtTable, dtTableOFGrades, dtTableLocations, LocGradeType, Inspection_ID, dr["ChecklistItem_ID"].ToString(), indexNoval, rowCount, Padding + 1, DataColumnsName, dicJSEvent, DicToolTip));
    //                    }
    //                }
    //                else if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Item")
    //                {
    //                    BLL_Tec_Worklist objWl = new BLL_Tec_Worklist();
    //                    int JobCount = objWl.INSP_Get_WorklistJobsCountByLocationID(Convert.ToInt32(Inspection_ID), Convert.ToInt32(dr["ChecklistItem_ID"].ToString()));

    //                   QueCount++;
    //                    string strItemID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
    //                    strTable.Append("<div class='insp-Chk-Item-Container insp-Item-Pos" + Padding.ToString() + "'  id=dvItem-" + (rowCount).ToString() + ">");

    //                    //strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + QueCount.ToString() + "&nbsp</span>");
    //                    strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + QueCount.ToString() + "&nbsp</span>");
    //                    strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
    //                    //strTable.Append("</td><td>");
    //                    strTable.Append("<table class='insp-table'><tr><td>");
    //                    strTable.Append("<div class='badge2'><div class='badge1' > <a badge href='#' onclick='Get_JobsAtteched(\"" + strItemID + "\");'>" + JobCount.ToString() + "</a> </div></div>");//<div class='badge' ></div> <label data-badge=" + JobCount.ToString() + " onclick='Get_JobsAtteched(\"" + strItemID + "\");' >" + JobCount.ToString() + "</label>
    //                    strTable.Append("</td><td><td>&nbsp</td>");
    //                    strTable.Append("<div class='Addshedule' ><img src='../../Images/add.GIF' onclick='AddDefect(\"" + strItemID + "\");' ></div>");
    //                    strTable.Append("</td></tr></table>");
    //                    strTable.Append("</div>");
    //                    strTable.Append("</td></tr>"); ;

    //                    if (dtTableOFGrades.Rows.Count > 0)
    //                    {
    //                        dtTableOFGrades.DefaultView.RowFilter = "ID= '" + dr["ItemGrading_Type"].ToString() + "'";

    //                        DataTable dtitemDiff = dtTableOFGrades.DefaultView.ToTable();
    //                        if (dtitemDiff.Rows.Count > 0)
    //                        {
    //                            int itemCount = 0;
    //                            string optionID = (rowCount).ToString();
    //                            strTable.Append("<tr><td>");
    //                            strTable.Append("<div class='insp-Item-Pos" + (Padding + 1).ToString() + "'  id=dvItemOptions" + (rowCount).ToString() + ">");
    //                            foreach (DataRow dr1 in dtitemDiff.Rows)
    //                            {

    //                                string stroptiontext = dr1["OptionText"].ToString().Replace("\n", "<br>");
    //                                string strChecklistID = dr["Checklist_IDCopy"].ToString().Replace("\n", "<br>");
    //                                string optionName = index.ToString() + "-" + QueCount.ToString();
    //                                string strID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
    //                                itemCount += 1;
    //                                if (dr["ItemRates"].ToString().Replace("\n", "<br>") != "")
    //                                {
    //                                    if (Convert.ToDecimal(dr["ItemRates"].ToString().Replace("\n", "<br>")) == Convert.ToDecimal(dr1["OptionValue"].ToString()))
    //                                    {
    //                                        strTable.Append("<input type='radio' checked=true name='dvItem-" + optionName + "' value='" + dr1["optionID"].ToString().Replace("\n", "<br>") + "'  onclick='Get_Ratings(\"" + strID + "\", " + dr1["OptionValue"].ToString().Replace("\n", "<br>") + ", \"" + stroptiontext + "\",\"Item\",\"" + strChecklistID + "\");'>" + dr1["OptionText"].ToString().Replace("\n", "<br>"));
    //                                    }
    //                                    else
    //                                    {
    //                                        strTable.Append("<input type='radio'  name='dvItem-" + optionName + "' value='" + dr1["optionID"].ToString().Replace("\n", "<br>") + "'  onclick='Get_Ratings(\"" + strID + "\", " + dr1["OptionValue"].ToString().Replace("\n", "<br>") + ", \"" + stroptiontext + "\",\"Item\",\"" + strChecklistID + "\");'>" + dr1["OptionText"].ToString().Replace("\n", "<br>"));
    //                                    }
    //                                }
    //                                else
    //                                {
    //                                    strTable.Append("<input type='radio'  name='dvItem-" + optionName + "' value='" + dr1["optionID"].ToString().Replace("\n", "<br>") + "'  onclick='Get_Ratings(\"" + strID + "\", " + dr1["OptionValue"].ToString().Replace("\n", "<br>") + ", \"" + stroptiontext + "\",\"Item\",\"" + strChecklistID + "\");'>" + dr1["OptionText"].ToString().Replace("\n", "<br>"));
                                      
    //                                }
    //                                rowCount++;
    //                            }
    //                            strTable.Append("</div>");
    //                            strTable.Append("</td></tr>");
    //                        }
    //                    }
    //                }
    //            }

    //        }
    //    }

    //    return strTable.ToString();
    //}

    //public static string ReplaceSpecialCharacter(string str)
    //{
    //    //return str.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;").Replace("\"", "&quot;");
    //    string ret = str.Replace(@"\", @"\\");
    //    return ret;
    //}



    

      
}