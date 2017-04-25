using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using SMS.Business.INFRA;
using System.Web.Services;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.Technical;
using SMS.Properties;
using System.IO;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Xml;
using System.Text;
using SMS.Business.Inspection;
using System.Xml.Serialization;
using System.Web.Services.Protocols;

public partial class JibeWebServiceInspection
{

    public static int CounterRow = 0;
    public static int Finalise = 0;
    public static Dictionary<int, Color> InspColor;



    [WebMethod]
    public string Get_CheckList_Table(string CheckList_ID)
    {
        //SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();

        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        Dictionary<string, UDCToolTip> dictoolTip = new Dictionary<string, UDCToolTip>();
        //Dictionary<string, UDCAction> dicAction = new Dictionary<string, UDCAction>();
        List<UDCAction> dicAction = new List<UDCAction>();


        Dictionary<string, string> dicNode = new Dictionary<string, string>();
        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();
        // dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "ShowAddDiv", "onclick" } }, new string[] { "Checklist_ID", "ChecklistItem_ID", "CheckList_Name", "NodeType", "Vessel_Type", "checklistType", "Grading_Type" }, "CheckList_Name"));

        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "ShowAddDiv", "onclick" } }, new string[] { "Checklist_ID", "ChecklistItem_ID", "CheckList_Name", "NodeType", "Vessel_Type", "checklistType", "Grading_Type", "Checklist_IDCopy", "Parent_ID", "Location_ID" }, "CheckList_Name"));


        DataTable dtTable = objBl.Get_CurrentCheckList(int.Parse(CheckList_ID));
        DataTable dtLocation = new DataTable();
        if (dtTable.Rows.Count > 0)
        {
            dtLocation = objBl.Get_CheckListLocations(int.Parse(dtTable.Rows[0]["Vessel_Type"].ToString()));
        }

        return CreateHtmlTableChecklist(dtTable, objBl.Get_GradesWithOption(), dtLocation,
            //new string[] { "CheckList Name", "Node Type", "Description" },
            new string[] { "", "", "" },
           new string[] { "CheckList_Name", "NodeType", "CheckList_Type_name" },
           dicLink, dictoolTip,
           new string[] { "left", "left", "left", "left", "left" },
            "tbl-common-Css",
              "hdr-common-Css-Vertical",
           "row-common-Css-Vertical",
           "row-common-Css-Vertical",
           dicAction,
           dicJSEvent,
           dicNode);
    }

    public static string CreateHtmlTableChecklist(DataTable dtTable, DataTable dtTableOFGrades, DataTable dtTableLocations, string[] HeaderCaptions, string[] DataColumnsName, Dictionary<string, UDCHyperLink> DicLink, Dictionary<string, UDCToolTip> DicToolTip, string[] DataColumnsAlignment, string TableCss, string HeaderCss, string RowStyleCss, string AlternateRowStyleCss, List<UDCAction> dicAction, List<UDCJSEvent> dicJSEvent, Dictionary<string, string> NodeType)
    {
        StringBuilder strTable = new StringBuilder();
        try
        {
            if (dtTable.Rows.Count > 0)
            {
                strTable.Append("<table id='__tbl_remark' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
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
                    bool tdFlag = false;
                    if (dtTable.Rows[0]["NodeType"].ToString().Replace("\n", "<br>") != "Group" && dtTable.Rows[0]["NodeType"].ToString().Replace("\n", "<br>") != "Location" && dtTable.Rows[0]["NodeType"].ToString().Replace("\n", "<br>") != "Item")
                    {
                        if (dtTable.Rows[0]["Vessel_Type"].ToString() != "" && dtTable.Rows[0]["Vessel_Type"] != DBNull.Value)
                        {
                            vesseltype_ID = dtTable.Rows[0]["Vessel_Type"].ToString();
                        }
                        strTable.Append("<tr style='background-color: #efefef;'>"); /// Added by Anjali DT:17-May-2016 JIT:9319 || 
                        strTable.Append("<td ");
                        #region . tooltip and  javascript event

                        for (int i = 0; i < DataColumnsName.Length; i++)
                        {

                            UDCToolTip objToolTip;
                            if (DicToolTip.TryGetValue(DataColumnsName[i], out objToolTip))// check for tool tip
                            {
                                if (objToolTip.UseControlToolTip)
                                    strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dtTable.Rows[0][objToolTip.ToolTipSourceColumn])) + "'");
                                else
                                {
                                    if (Convert.ToString(dtTable.Rows[0][objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                                    {
                                        string s = "";
                                    }
                                    strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dtTable.Rows[0][objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                                    strTable.Append(" style='cursor:default' ");
                                }
                            }



                            if (dicJSEvent.Count > 0)
                            {
                                foreach (UDCJSEvent ievt in dicJSEvent)
                                {
                                    if (ievt.EventColumnName.ToUpper() == DataColumnsName[i].ToUpper())
                                    {
                                        string funparam = "";

                                        foreach (string iprm in ievt.ParamDataColumn)
                                        {

                                            if (funparam.Length > 0)
                                                funparam += ",";
                                            if ("CheckList_Name" == iprm)
                                            {
                                                string[] arr = (dtTable.Rows[0][iprm].ToString()).Split('(');
                                                if (arr.Length >= 1)
                                                    funparam += "\"" + arr[0] + "\"";
                                                else
                                                    funparam += "\"" + dtTable.Rows[0][iprm] + "\"";
                                            }
                                            else
                                            {
                                                funparam += "\"" + dtTable.Rows[0][iprm] + "\"";
                                            }
                                        }

                                        funparam += ",event,this";

                                        foreach (string[] fun in ievt.FunctionName)
                                        {
                                            strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                                        }

                                        strTable.Append(" style='cursor:default' ");
                                    }
                                }
                            }

                        #endregion
                            if (tdFlag == false)
                            {
                                strTable.Append(" > ");
                            }
                            #region . column data

                            UDCHyperLink objA;
                            if (DicLink.TryGetValue(DataColumnsName[i], out objA)) //check for hyperlink
                            {


                                if (string.IsNullOrWhiteSpace(objA.HyperLinkColumnName))//link is fixed
                                    strTable.Append("<a href='" + objA.NaviagteURL);
                                else
                                    strTable.Append("<a  href='../" + Convert.ToString(dtTable.Rows[0][objA.HyperLinkColumnName]));// link can be different for each row

                                for (int iQs = 0; iQs < objA.QueryStringDataColumn.Length; iQs++)// create querystring
                                {
                                    if (iQs == 0)
                                        strTable.Append("?");

                                    strTable.Append(objA.QueryStringText[iQs] + "=" + dtTable.Rows[0][objA.QueryStringDataColumn[iQs]].ToString());

                                    if (iQs < objA.QueryStringDataColumn.Length - 1)
                                        strTable.Append("&");
                                }
                                strTable.Append("'");

                                if (!string.IsNullOrWhiteSpace(objA.Target))
                                    strTable.Append(" target='" + objA.Target + "' ");
                                else
                                    strTable.Append(" target='_blank'");


                                strTable.Append(" > ");

                                strTable.Append(dtTable.Rows[0][DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                                strTable.Append("</a>");

                            }
                            else
                            {

                                if (i == 0)
                                {
                                    strTable.Append("<div class='insp-Chk-Checklist'  id=dv" + DataColumnsName[i] + (rowCount).ToString() + ">");
                                    strTable.Append(dtTable.Rows[0][DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                                    strTable.Append("<input type='hidden' name='hdnStat' ID='hdnStat' value=" + dtTable.Rows[0]["Status"].ToString().Replace("\n", "<br>") + ">");
                                    strTable.Append("<input type='hidden' name='hdnChildFlag' ID='hdnChildFlag' value=" + dtTable.Rows[0]["ChildFlag"].ToString().Replace("\n", "<br>") + ">");
                                    strTable.Append("</div>");
                                }
                                else
                                {
                                    strTable.Append("<div class='insp-Chk-Checklist'  id=dv" + DataColumnsName[i] + (rowCount).ToString() + ">");
                                    strTable.Append(dtTable.Rows[0][DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                                    strTable.Append("</div>");
                                }

                                tdFlag = true;

                            }
                        }

                        strTable.Append("</td>");
                        tdFlag = false;
                        strTable.Append("</tr>");
                            #endregion
                    }

                    int parentPadding = 1;
                    dtTable.DefaultView.RowFilter = "NodeType= 'Group' AND Parent_ID IS NULL";
                    dtTable.DefaultView.Sort = "Index_No asc";
                    DataTable dtgroupDiff = dtTable.DefaultView.ToTable();
                    if (dtgroupDiff.Rows.Count > 0)
                    {
                        strTable.Append(getrecurtion(dtgroupDiff, dtTable, dtTableOFGrades, dtTableLocations, null, parentPadding, "", DataColumnsName, dicJSEvent, DicToolTip));
                    }
                    dtTable.DefaultView.RowFilter = "NodeType= 'Location' AND Parent_ID IS NULL";
                    dtTable.DefaultView.Sort = "Index_No asc";
                    DataTable dtLocationDiff = dtTable.DefaultView.ToTable();
                    if (dtLocationDiff.Rows.Count > 0)
                    {
                        strTable.Append(getrecurtion(dtLocationDiff, dtTable, dtTableOFGrades, dtTableLocations, null, parentPadding, "", DataColumnsName, dicJSEvent, DicToolTip));
                    }
                }

                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'> No record found !</span>");

            return strTable.ToString();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            return "";
        }
        finally
        {

        }
    }

    public static string getrecurtion(DataTable dtdiffTable, DataTable dtTable, DataTable dtTableOFGrades, DataTable dtTableLocations, string ParentID, int Padding, string index, string[] DataColumnsName, List<UDCJSEvent> dicJSEvent, Dictionary<string, UDCToolTip> DicToolTip)
    {
        StringBuilder strTable = new StringBuilder();
        try
        {

            int rowCount = 0;
            int GroupCount = 0;
            int LocCount = 0;
            int QueCount = 0;

            strTable.Append("<tr><td");

            int tdtrCount = 0;

            if (ParentID == null)
            {
                foreach (DataRow dr in dtdiffTable.Rows)
                {
                    if (tdtrCount == 0)
                    {

                        #region . tooltip and  javascript event

                        //for (int i = 0; i < DataColumnsName.Length; i++)
                        //{

                        UDCToolTip objToolTip;
                        if (DicToolTip.TryGetValue(DataColumnsName[0], out objToolTip))// check for tool tip
                        {
                            if (objToolTip.UseControlToolTip)
                                strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                            else
                            {
                                if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                                {
                                    string s = "";
                                }
                                strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                                strTable.Append(" style='cursor:default' ");
                            }
                        }



                        if (dicJSEvent.Count > 0)
                        {
                            foreach (UDCJSEvent ievt in dicJSEvent)
                            {
                                if (ievt.EventColumnName.ToUpper() == DataColumnsName[0].ToUpper())
                                {
                                    string funparam = "";
                                    foreach (string iprm in ievt.ParamDataColumn)
                                    {
                                        if (funparam.Length > 0)
                                            funparam += ",";
                                        // Handled ' quote.
                                        string strQuote = dr[iprm].ToString();

                                        if (strQuote.Contains("'"))
                                            funparam += "\"" + strQuote.Replace("'", "&apos;") + "\"";
                                        else
                                            funparam += "\"" + dr[iprm] + "\"";

                                    }

                                    funparam += ",event,this";

                                    foreach (string[] fun in ievt.FunctionName)
                                    {
                                        strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                                    }

                                    strTable.Append(" style='cursor:default' ");
                                }
                            }
                        }
                        strTable.Append(">");
                        //}
                        #endregion

                    }

                    if (tdtrCount != 0)
                    {
                        strTable.Append("<tr><td  ");

                        #region . tooltip and  javascript event

                        //for (int i = 0; i < DataColumnsName.Length; i++)
                        //{

                        UDCToolTip objToolTip;
                        if (DicToolTip.TryGetValue(DataColumnsName[0], out objToolTip))// check for tool tip
                        {
                            if (objToolTip.UseControlToolTip)
                                strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                            else
                            {
                                if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                                {
                                    string s = "";
                                }
                                strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                                strTable.Append(" style='cursor:default' ");
                            }
                        }



                        if (dicJSEvent.Count > 0)
                        {
                            foreach (UDCJSEvent ievt in dicJSEvent)
                            {
                                if (ievt.EventColumnName.ToUpper() == DataColumnsName[0].ToUpper())
                                {
                                    string funparam = "";
                                    foreach (string iprm in ievt.ParamDataColumn)
                                    {
                                        if (funparam.Length > 0)
                                            funparam += ",";
                                        // Handled ' quote.
                                        string strQuote = dr[iprm].ToString();

                                        if (strQuote.Contains("'"))
                                            funparam += "\"" + strQuote.Replace("'", "&apos;") + "\"";
                                        else
                                            funparam += "\"" + dr[iprm] + "\"";
                                    }

                                    funparam += ",event,this";

                                    foreach (string[] fun in ievt.FunctionName)
                                    {
                                        strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                                    }

                                    strTable.Append(" style='cursor:default' ");
                                }
                            }
                        }
                        strTable.Append(">");
                        //}
                        #endregion

                    }
                    tdtrCount += 1;
                    rowCount += 1;
                    if (dr["Parent_ID"] == DBNull.Value)
                    {
                        if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Group")
                        {
                            GroupCount++;
                            strTable.Append("<div class='insp-Chk-Group insp-Item-Pos" + Padding.ToString() + "' id=dvGrp" + (rowCount).ToString() + ">");
                            strTable.Append("<span class='insp-IndexNo' > " + GroupCount.ToString() + ".&nbsp</span>");
                            strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
                            strTable.Append("</div>");
                            strTable.Append("</td></tr>");

                            dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Group' ";
                            dtTable.DefaultView.Sort = "Index_No asc";
                            DataTable dtGroupDiff = dtTable.DefaultView.ToTable();
                            if (dtGroupDiff.Rows.Count > 0)
                            {
                                strTable.Append(getrecurtion(dtGroupDiff, dtTable, dtTableOFGrades, dtTableLocations, dr["ChecklistItem_ID"].ToString(), Padding + 1, GroupCount.ToString(), DataColumnsName, dicJSEvent, DicToolTip));
                            }

                            dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Location' ";
                            dtTable.DefaultView.Sort = "Index_No asc";
                            DataTable dtLocationDiff = dtTable.DefaultView.ToTable();

                            if (dtLocationDiff.Rows.Count > 0)
                                strTable.Append(getrecurtion(dtLocationDiff, dtTable, dtTableOFGrades, dtTableLocations, dr["ChecklistItem_ID"].ToString(), Padding + 1, GroupCount.ToString(), DataColumnsName, dicJSEvent, DicToolTip));
                        }
                        else if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Location" && dr["Parent_ID"] == DBNull.Value)
                        {
                            LocCount++;
                            if (dtTableLocations.Rows.Count > 0)
                            {
                                if (dr["Location_ID"].ToString() != "" && dr["Location_ID"] != DBNull.Value)
                                {
                                    dtTableLocations.DefaultView.RowFilter = "ID= '" + dr["Location_ID"].ToString() + "' ";
                                    DataTable dtLocationDiff = dtTableLocations.DefaultView.ToTable();
                                    if (dtLocationDiff.Rows.Count > 0)
                                    {
                                        strTable.Append("<div  class='insp-Chk-Location insp-Item-Pos" + Padding.ToString() + "' id=dvLocation" + (rowCount).ToString() + ">");
                                        strTable.Append("<span class='insp-IndexNo' > " + LocCount.ToString() + ".&nbsp</span>");
                                        strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
                                        strTable.Append("<label class='insp-Chk-Location-Text' >&nbsp;" + dtLocationDiff.Rows[0]["LocationName"].ToString().Replace("\n", "<br>") + "</label>");
                                        strTable.Append("</div>");
                                    }
                                }
                                else
                                {
                                    strTable.Append("<div  class='insp-Chk-Location insp-Item-Pos" + Padding.ToString() + "' id=dvLocation" + (rowCount).ToString() + ">");
                                    strTable.Append("<span class='insp-IndexNo' > " + LocCount.ToString() + ".&nbsp</span>");
                                    strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
                                    strTable.Append("</div>");
                                }
                            }
                            else
                            {

                                strTable.Append("<div  class='insp-Chk-Location insp-Item-Pos" + Padding.ToString() + "' id=dvLocation" + (rowCount).ToString() + ">");
                                strTable.Append("<span class='insp-IndexNo' > " + LocCount.ToString() + ".&nbsp</span>");
                                strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
                                strTable.Append("</div>");

                            }

                            strTable.Append("</td></tr>");
                            dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' ";
                            dtTable.DefaultView.Sort = "Index_No asc";
                            DataTable dtItemDiff = dtTable.DefaultView.ToTable();
                            if (dtItemDiff.Rows.Count > 0)
                            {
                                strTable.Append(getrecurtion(dtItemDiff, dtTable, dtTableOFGrades, dtTableLocations, dr["ChecklistItem_ID"].ToString(), Padding + 1, LocCount.ToString(), DataColumnsName, dicJSEvent, DicToolTip));
                            }


                        }
                    }

                }
            }
            else
            {
                foreach (DataRow dr in dtdiffTable.Rows)
                {
                    if (tdtrCount == 0)
                    {

                        #region . tooltip and  javascript event

                        UDCToolTip objToolTip;
                        if (DicToolTip.TryGetValue(DataColumnsName[0], out objToolTip))// check for tool tip
                        {
                            if (objToolTip.UseControlToolTip)
                                strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                            else
                            {
                                if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                                {
                                    string s = "";
                                }
                                strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                                strTable.Append(" style='cursor:default' ");
                            }
                        }



                        if (dicJSEvent.Count > 0)
                        {
                            foreach (UDCJSEvent ievt in dicJSEvent)
                            {
                                if (ievt.EventColumnName.ToUpper() == DataColumnsName[0].ToUpper())
                                {
                                    string funparam = "";
                                    foreach (string iprm in ievt.ParamDataColumn)
                                    {
                                        if (funparam.Length > 0)
                                            funparam += ",";

                                        string strQuote = dr[iprm].ToString();

                                        if (strQuote.Contains("'"))
                                            funparam += "\"" + strQuote.Replace("'", "&apos;") + "\"";
                                        else
                                            funparam += "\"" + dr[iprm] + "\"";
                                    }

                                    funparam += ",event,this";

                                    foreach (string[] fun in ievt.FunctionName)
                                    {
                                        strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                                    }

                                    strTable.Append(" style='cursor:default' ");
                                }
                            }
                        }
                        strTable.Append(">");
                        //}
                        #endregion

                    }
                    if (tdtrCount != 0)
                    {
                        strTable.Append("<tr><td  ");

                        #region . tooltip and  javascript event

                        //for (int i = 0; i < DataColumnsName.Length; i++)
                        //{

                        UDCToolTip objToolTip;
                        if (DicToolTip.TryGetValue(DataColumnsName[0], out objToolTip))// check for tool tip
                        {
                            if (objToolTip.UseControlToolTip)
                                strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                            else
                            {
                                if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                                {
                                    string s = "";
                                }
                                strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                                strTable.Append(" style='cursor:default' ");
                            }
                        }



                        if (dicJSEvent.Count > 0)
                        {
                            foreach (UDCJSEvent ievt in dicJSEvent)
                            {
                                if (ievt.EventColumnName.ToUpper() == DataColumnsName[0].ToUpper())
                                {
                                    string funparam = "";
                                    foreach (string iprm in ievt.ParamDataColumn)
                                    {
                                        if (funparam.Length > 0)
                                            funparam += ",";

                                        // Handled ' quote.
                                        string strQuote = dr[iprm].ToString();

                                        if (strQuote.Contains("'"))
                                            funparam += "\"" + strQuote.Replace("'", "&apos;") + "\"";
                                        else
                                            funparam += "\"" + dr[iprm] + "\"";
                                    }

                                    funparam += ",event,this";

                                    foreach (string[] fun in ievt.FunctionName)
                                    {
                                        strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                                    }

                                    strTable.Append(" style='cursor:default' ");
                                }
                            }
                        }
                        strTable.Append(">");
                        //}
                        #endregion
                    }
                    tdtrCount += 1;
                    if (dr["Parent_ID"] != DBNull.Value)
                    {
                        if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Group")
                        {
                            GroupCount++;
                            strTable.Append("<div class='insp-Chk-Group insp-Item-Pos" + Padding.ToString() + "' id=dvGrp" + (rowCount).ToString() + ">");
                            strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + GroupCount.ToString() + "&nbsp</span>");
                            strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
                            strTable.Append("</div>");
                            strTable.Append("</td></tr>");

                            string indexcounts = index.ToString() + "." + GroupCount.ToString();

                            dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Group' ";
                            dtTable.DefaultView.Sort = "Index_No asc";
                            DataTable dtGroupDiff = dtTable.DefaultView.ToTable();
                            if (dtGroupDiff.Rows.Count > 0)
                            {
                                strTable.Append(getrecurtion(dtGroupDiff, dtTable, dtTableOFGrades, dtTableLocations, dr["ChecklistItem_ID"].ToString(), Padding + 1, indexcounts, DataColumnsName, dicJSEvent, DicToolTip));
                            }

                            dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Location' ";
                            dtTable.DefaultView.Sort = "Index_No asc";
                            DataTable dtLocationDiff = dtTable.DefaultView.ToTable();

                            if (dtLocationDiff.Rows.Count > 0)
                            {
                                strTable.Append(getrecurtion(dtLocationDiff, dtTable, dtTableOFGrades, dtTableLocations, dr["ChecklistItem_ID"].ToString(), Padding + 1, indexcounts, DataColumnsName, dicJSEvent, DicToolTip));
                            }
                        }
                        else if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Location")
                        {
                            LocCount++;
                            string indexcounts = "";
                            if (dtTableLocations.Rows.Count > 0)
                            {
                                if (dr["Location_ID"].ToString() != "" && dr["Location_ID"] != DBNull.Value)
                                {
                                    dtTableLocations.DefaultView.RowFilter = "ID= '" + dr["Location_ID"].ToString() + "'";

                                    DataTable dtLocationDiff = dtTableLocations.DefaultView.ToTable();
                                    if (dtLocationDiff.Rows.Count > 0)
                                    {

                                        strTable.Append("<div  class='insp-Chk-Location insp-Item-Pos" + Padding.ToString() + "' id=dvLocation" + (rowCount).ToString() + ">");
                                        strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + LocCount + "&nbsp</span>");
                                        strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
                                        strTable.Append("<label class='insp-Chk-Location-Text' >&nbsp;" + dtLocationDiff.Rows[0]["LocationName"].ToString().Replace("\n", "<br>") + "</label>");
                                        strTable.Append("</div>");

                                        indexcounts = index.ToString() + "." + LocCount.ToString();

                                    }
                                }
                                else
                                {
                                    strTable.Append("<div  class='insp-Chk-Location insp-Item-Pos" + Padding.ToString() + "' id=dvLocation" + (rowCount).ToString() + ">");
                                    strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + LocCount + "&nbsp</span>");
                                    strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
                                    strTable.Append("</div>");
                                    indexcounts = index.ToString() + "." + LocCount.ToString();
                                }
                            }
                            else
                            {

                                strTable.Append("<div  class='insp-Chk-Location insp-Item-Pos" + Padding.ToString() + "' id=dvLocation" + (rowCount).ToString() + ">");
                                strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + LocCount + "&nbsp</span>");
                                strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
                                strTable.Append("</div>");

                                indexcounts = index.ToString() + "." + LocCount.ToString();

                            }



                            strTable.Append("</td></tr>");

                            dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "'";
                            dtTable.DefaultView.Sort = "Index_No asc";
                            DataTable dtItemDiff = dtTable.DefaultView.ToTable();
                            if (dtItemDiff.Rows.Count > 0)
                            {
                                strTable.Append(getrecurtion(dtItemDiff, dtTable, dtTableOFGrades, dtTableLocations, dr["ChecklistItem_ID"].ToString(), Padding + 1, indexcounts, DataColumnsName, dicJSEvent, DicToolTip));
                            }
                        }
                        else if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Item")
                        {
                            QueCount++;
                            strTable.Append("<div class='insp-Chk-Item-Container insp-Item-Pos" + Padding.ToString() + "'  id=dvItem" + (rowCount).ToString() + ">");
                            strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + QueCount.ToString() + "&nbsp</span>");
                            strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
                            strTable.Append("</div>");
                            strTable.Append("</td></tr>");

                            string indexcounts = index.ToString() + "." + QueCount.ToString();

                            if (dtTableOFGrades.Rows.Count > 0)
                            {
                                dtTableOFGrades.DefaultView.RowFilter = "ID= '" + dr["ItemGrading_Type"].ToString() + "'";

                                DataTable dtitemDiff = dtTableOFGrades.DefaultView.ToTable();
                                if (dtitemDiff.Rows.Count > 0)
                                {
                                    int itemCount = 0;
                                    strTable.Append("<tr><td>");
                                    strTable.Append("<div class='insp-Item-Pos" + Padding.ToString() + "'  id=dvItemOptions" + (rowCount).ToString() + ">");
                                    foreach (DataRow dr1 in dtitemDiff.Rows)
                                    {
                                        itemCount += 1;
                                        strTable.Append("<input type='radio' name='" + dr1["Grade_Name"].ToString().Replace("\n", "<br>") + "' value='" + dr1["optionID"].ToString().Replace("\n", "<br>") + "' disabled='true' >" + dr1["OptionText"].ToString().Replace("\n", "<br>"));
                                    }
                                    strTable.Append("</div>");
                                    strTable.Append("</td></tr>");
                                }
                            }
                        }

                    }

                }
            }
        }
        catch (Exception ex)
        {
            strTable.Append("");
            UDFLib.WriteExceptionLog(ex);
        }
        return strTable.ToString();


    }



    [WebMethod]
    public string Get_Location(string VesselTypeID, string Location_ID)
    {
        StringBuilder strDopdown = new StringBuilder();
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();

        DataTable dtLocation = new DataTable();
        dtLocation = objBl.Get_CheckListLocations(int.Parse(VesselTypeID));

        if (dtLocation.Rows.Count > 0)
        {
            //strDopdown += "<select>";
            strDopdown.Append("<select id='txtLocation' style='Width:150px;' >");//
            strDopdown.Append("<option value='0' selected>--Select--</option>");
            for (int k = 0; k < dtLocation.Rows.Count; k++)
            {
                if (Location_ID == "")
                {
                    strDopdown.Append("<option value='" + dtLocation.Rows[k]["ID"].ToString() + "'>" + dtLocation.Rows[k]["LocationName"].ToString() + "</option>");
                }
                else
                {
                    if (Location_ID == dtLocation.Rows[k]["ID"].ToString())
                    {
                        strDopdown.Append("<option value='" + dtLocation.Rows[k]["ID"].ToString() + "' selected>" + dtLocation.Rows[k]["LocationName"].ToString() + "</option>");
                    }
                    else
                    {
                        strDopdown.Append("<option value='" + dtLocation.Rows[k]["ID"].ToString() + "'>" + dtLocation.Rows[k]["LocationName"].ToString() + "</option>");
                    }
                }

            }
            strDopdown.Append("</select>");//
        }
        else
        {
            strDopdown.Append("<select id='txtLocation' style='Width:120px;' >");//
            strDopdown.Append("<option value='0' selected>--Select--</option>");
            strDopdown.Append("</select>");//
        }

        return strDopdown.ToString();
    }

    [WebMethod]
    public string Get_LocationTree(string VesselTypeID, string Location_ID)
    {

        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();

        DataTable dtLocation = new DataTable();
        dtLocation = objBl.Get_CheckListLocations(int.Parse(VesselTypeID));


        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dtLocation.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dtLocation.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);



    }

    [WebMethod]
    public string Get_CheckListItetemsQB_Table(string CheckList_ID, string Parent_ID)
    {
        //SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();
        Dictionary<string, UDCHyperLinkImage> dicLink = new Dictionary<string, UDCHyperLinkImage>();
        Dictionary<string, UDCToolTip> dictoolTip = new Dictionary<string, UDCToolTip>();
        //Dictionary<string, UDCAction> dicAction = new Dictionary<string, UDCAction>();
        List<UDCActionNew> dicAction = new List<UDCActionNew>();

        Dictionary<string, UDCCheckBox> dicCheckBox = new Dictionary<string, UDCCheckBox>();

        Dictionary<string, string> dicNode = new Dictionary<string, string>();
        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();
        //dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "ShowAddDiv", "onclick" } }, new string[] { "Checklist_ID", "ChecklistItem_ID", "CheckList_Name", "NodeType", "Vessel_Type", "checklistType", "Grading_Type" }, "CheckList_Name"));

        UDCCheckBox chk = new UDCCheckBox("ActiveStatus", "Active_Status");

        dicCheckBox.Add("Active_Status", chk);

        return CreateHtmlTableFromDataTableWithCheckBox(objBl.Get_CheckListItemsQB(int.Parse(CheckList_ID), int.Parse(Parent_ID)),
           new string[] { "NO", "Description", "Active" },
           new string[] { "ID", "ROWNUM", "Description", "Active_Status" },
           dicLink,
            dictoolTip,
           new string[] { "left", "left" }, "tblChkListQB",
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css", "row-common-Css", dicAction, dicJSEvent, dicCheckBox);



    }

    public static string CreateHtmlTableFromDataTableWithCheckBox(DataTable dtTable, string[] HeaderCaptions, string[] DataColumnsName, Dictionary<string, UDCHyperLinkImage> DicLink, Dictionary<string, UDCToolTip> DicToolTip, string[] DataColumnsAlignment, string TableID, string TableCss, string HeaderCss, string RowStyleCss, string AlternateRowStyleCss, List<UDCActionNew> dicAction, List<UDCJSEvent> dicJSEvent, Dictionary<string, UDCCheckBox> dicCheckBox)
    {
        StringBuilder strTable = new StringBuilder();

        try
        {

            if (dtTable.Rows.Count > 0)
            {

                strTable.Append("<table id='" + TableID + "' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
                if (!string.IsNullOrWhiteSpace(TableCss))
                    strTable.Append(" class='" + TableCss + "' ");

                strTable.Append(" > ");

                if (HeaderCaptions.Length > 0)
                {
                    strTable.Append("<tr >");
                    for (int i = 0; i < HeaderCaptions.Length; i++)
                    {
                        //strTable.Append("<th  width='auto' ");
                        if (HeaderCaptions[i] == "No")
                        {
                            strTable.Append("<th  style = 'width: 5%;' ");
                        }
                        else if (HeaderCaptions[i] == "Description")
                        {
                            strTable.Append("<th  style = 'width: 90%;' ");
                        }
                        else if (HeaderCaptions[i] == "Active")
                        {
                            strTable.Append("<th  style = 'width: 5%;' ");
                        }
                        else
                        {
                            strTable.Append("<th  width='auto' ");
                        }

                        if (!string.IsNullOrWhiteSpace(HeaderCss))
                            strTable.Append(" class='" + HeaderCss + "' ");

                        if (HeaderCaptions[i].Split('=').Length == 2)
                        {
                            strTable.Append(" onclick='AsyncLoadDataOnSort(&#39;" + HeaderCaptions[i].Split('=')[1] + "&#39;)'");
                            strTable.Append(" style='cursor:default;color:yellow;text-align:left' ");
                        }
                        if (DataColumnsAlignment.Length > i)
                            strTable.Append("  align='" + DataColumnsAlignment[i] + "' ");

                        strTable.Append(" > ");

                        if (HeaderCaptions[i].Split('=').Length == 2)
                            strTable.Append(HeaderCaptions[i].Split('=')[0]);
                        else
                            strTable.Append(HeaderCaptions[i]);

                        strTable.Append("</th>");
                    }

                    if (dicAction.Count > 0)
                    {
                        strTable.Append("<th  width='auto' style='text-align:left'  class='" + HeaderCss + "' >");
                        strTable.Append("Action");
                        strTable.Append("</th>");
                    }

                    strTable.Append("</tr>");
                }

                int icol = 0;
                int rownum = 1;
                Boolean columnadded = false;
                foreach (DataRow dr in dtTable.Rows)
                {
                    strTable.Append("<tr>");

                    for (int i = 0; i < DataColumnsName.Length; i++)
                    {
                        columnadded = false;
                        if (dtTable.Columns.Contains("Active_Status"))
                        {
                            if (dr["Active_Status"].ToString() == "0")
                            {
                                strTable.Append("<td style=color:red;");
                                columnadded = true;
                            }

                        }

                        if (dtTable.Columns.Contains("Critical"))
                        {
                            if (DataColumnsName[i].ToString() == "Critical")
                            {
                                if (dr["Critical"].ToString() == "Y")
                                {

                                    strTable.Append("<td style=background-color:red;color:white;font-weight:bold;text-align:" + DataColumnsAlignment[i] + ";");
                                    columnadded = true;
                                }

                            }
                        }
                        if (dtTable.Columns.Contains("CMS"))
                        {
                            if (DataColumnsName[i].ToString() == "CMS")
                            {
                                if (dr["CMS"].ToString() == "Y")
                                {


                                    strTable.Append("<td style=background-color:green;color:white;font-weight:bold;text-align:" + DataColumnsAlignment[i] + ";");
                                    columnadded = true;
                                }

                            }
                        }
                        if (columnadded == false)
                        {
                            strTable.Append("<td");
                        }
                        if (!string.IsNullOrWhiteSpace(RowStyleCss) && (icol % 2) == 0)
                            strTable.Append(" class='" + RowStyleCss + "' ");
                        else if (!string.IsNullOrWhiteSpace(AlternateRowStyleCss))
                            strTable.Append(" class='" + AlternateRowStyleCss + "' ");

                        if (DataColumnsAlignment.Length > i)
                            strTable.Append("  align='" + DataColumnsAlignment[i] + "' ");

                        #region . tooltip and  javascript event

                        UDCToolTip objToolTip;
                        if (DicToolTip.TryGetValue(DataColumnsName[i], out objToolTip))// check for tool tip
                        {
                            if (objToolTip.UseControlToolTip)
                                strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                            else
                            {
                                if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                                {
                                    string s = "";
                                }
                                strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                                strTable.Append(" style='cursor:default' ");
                            }
                        }



                        if (dicJSEvent.Count > 0)
                        {
                            foreach (UDCJSEvent ievt in dicJSEvent)
                            {
                                if (ievt.EventColumnName.ToUpper() == DataColumnsName[i].ToUpper())
                                {
                                    string funparam = "";
                                    foreach (string iprm in ievt.ParamDataColumn)
                                    {
                                        if (funparam.Length > 0)
                                            funparam += ",";

                                        funparam += dr[iprm];
                                    }

                                    funparam += ",event,this";

                                    foreach (string[] fun in ievt.FunctionName)
                                    {
                                        strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                                    }

                                    strTable.Append(" style='cursor:default' ");
                                }
                            }
                        }

                        #endregion

                        strTable.Append(" > ");

                        #region . column data

                        UDCHyperLinkImage objA;
                        UDCCheckBox objChk;
                        if (DicLink.TryGetValue(DataColumnsName[i], out objA)) //check for hyperlink
                        {


                            if (string.IsNullOrWhiteSpace(objA.HyperLinkColumnName))//link is fixed
                                strTable.Append("<a href='" + objA.NaviagteURL);
                            else
                                strTable.Append("<a  href='../" + objA.NaviagteURL + Convert.ToString(dr[objA.QueryStringDataColumn[0]]));// link can be different for each row

                            for (int iQs = 0; iQs < objA.QueryStringDataColumn.Length; iQs++)// create querystring
                            {
                                if (iQs == 0)
                                    strTable.Append("?");

                                strTable.Append(objA.QueryStringText[iQs] + "=" + dr[objA.QueryStringDataColumn[iQs]].ToString());

                                if (iQs < objA.QueryStringDataColumn.Length - 1)
                                    strTable.Append("&");
                            }
                            strTable.Append("'");

                            if (!string.IsNullOrWhiteSpace(objA.Target))
                                strTable.Append(" target='" + objA.Target + "' ");
                            else
                                strTable.Append(" target='_blank'");


                            strTable.Append(" > ");
                            //  if (!string.IsNullOrWhiteSpace(objA.HyperLinkColumnName))
                            // {
                            if (dr[objA.HyperLinkColumnName].ToString() != "")
                            {
                                strTable.Append("<img src='" + objA.ImageURL + "' width='18' height='18' />");
                            }

                            //  }
                            // strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                            strTable.Append("</a>");

                        }
                        else if (dicCheckBox.TryGetValue(DataColumnsName[i], out objChk)) //check for CheckBox
                        {
                            if (dr[DataColumnsName[i]].ToString() == "1")
                            {
                                strTable.Append("<input type='checkbox' id=" + TableID + "_" + DataColumnsName[i] + rownum + " checked=true disabled=true onchange='QuestionValueChange(\"" + dr[DataColumnsName[0]].ToString() + "\",0)' />");
                            }
                            else
                            {
                                strTable.Append("<input type='checkbox' id=" + TableID + "_" + DataColumnsName[i] + rownum + " onchange='QuestionValueChange(\"" + dr[DataColumnsName[0]].ToString() + "\",1)' />");
                            }
                        }

                        else
                        {
                            strTable.Append(dr[DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                        }

                        #endregion
                        strTable.Append("</td>");

                        // }
                    }




                    //add action columns
                    if (dicAction.Count > 0)
                    {
                        strTable.Append("<td width='auto'");

                        if (!string.IsNullOrWhiteSpace(RowStyleCss) && (icol % 2) == 0)
                            strTable.Append(" class='" + RowStyleCss + "' ");
                        else if (!string.IsNullOrWhiteSpace(AlternateRowStyleCss))
                            strTable.Append(" class='" + AlternateRowStyleCss + "' ");
                        strTable.Append(">");

                        strTable.Append("<table>");
                        strTable.Append("<tr>");
                        foreach (UDCActionNew iAction in dicAction)
                        {
                            if (dr["Active_Status"].ToString() == "0")
                            {
                                if (iAction.ActionName == "Restore")
                                {
                                    strTable.Append("<td style='cursor:pointer;' onmouseover='js_ShowToolTip(&#39;" + iAction.ActionName + "&#39;,event,this)'> <img src='" + iAction.ImageURL + "' width='18' height='18' ");
                                    string funparam = "";
                                    foreach (string iprm in iAction.ParamDataColumn)
                                    {
                                        if (funparam.Length > 0)
                                            funparam += ",";

                                        funparam += "'" + dr[iprm] + "'";
                                    }

                                    funparam += ",event,this";

                                    foreach (string[] fun in iAction.FunctionName)
                                    {
                                        strTable.Append(fun[1] + "=\"" + fun[0] + "(" + funparam + ")\"");
                                    }

                                    strTable.Append("/>");
                                    strTable.Append("</td>");
                                }
                            }
                            else
                            {
                                if (iAction.ActionName != "Restore")
                                {
                                    strTable.Append("<td style='cursor:pointer;' onmouseover='js_ShowToolTip(&#39;" + iAction.ActionName + "&#39;,event,this)'> <img src='" + iAction.ImageURL + "' width='18' height='18' ");
                                    string funparam = "";
                                    foreach (string iprm in iAction.ParamDataColumn)
                                    {
                                        if (funparam.Length > 0)
                                            funparam += ",";

                                        funparam += "'" + dr[iprm] + "'";
                                    }

                                    funparam += ",event,this";

                                    foreach (string[] fun in iAction.FunctionName)
                                    {
                                        strTable.Append(fun[1] + "=\"" + fun[0] + "(" + funparam + ")\"");
                                    }

                                    strTable.Append("/>");
                                    strTable.Append("</td>");
                                }
                            }


                        }
                        strTable.Append("</tr>");
                        strTable.Append("</table>");
                        strTable.Append("</td>");
                    }


                    strTable.Append("</tr>");

                    icol++;
                    rownum += 1;
                }
                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'> No record found !</span>");

            return strTable.ToString();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            return "";
        }
        finally
        {

        }
    }


    [WebMethod]
    public string Get_CheckListItetemsQB_filter_Table(string CheckList_ID, string Parent_ID, string description)
    {
        //SMS.Business.PMS.BLL_PMS_Library_Jobs objBl = new SMS.Business.PMS.BLL_PMS_Library_Jobs();
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();
        Dictionary<string, UDCHyperLinkImage> dicLink = new Dictionary<string, UDCHyperLinkImage>();
        Dictionary<string, UDCToolTip> dictoolTip = new Dictionary<string, UDCToolTip>();
        //Dictionary<string, UDCAction> dicAction = new Dictionary<string, UDCAction>();
        List<UDCActionNew> dicAction = new List<UDCActionNew>();

        Dictionary<string, UDCCheckBox> dicCheckBox = new Dictionary<string, UDCCheckBox>();

        Dictionary<string, string> dicNode = new Dictionary<string, string>();
        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();
        //dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "ShowAddDiv", "onclick" } }, new string[] { "Checklist_ID", "ChecklistItem_ID", "CheckList_Name", "NodeType", "Vessel_Type", "checklistType", "Grading_Type" }, "CheckList_Name"));

        UDCCheckBox chk = new UDCCheckBox("ActiveStatus", "Active_Status");

        dicCheckBox.Add("Active_Status", chk);

        return CreateHtmlTableFromDataTableWithCheckBox(objBl.Get_CheckListItemsQB(int.Parse(CheckList_ID), int.Parse(Parent_ID), description),
           new string[] { "No", "Description", "Active" },
           new string[] { "ID", "ROWNUM", "Description", "Active_Status" },
           dicLink,
            dictoolTip,
           new string[] { "left", "left" }, "tblChkListQB",
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css", "row-common-Css", dicAction, dicJSEvent, dicCheckBox);



    }



    //Added By Pranav Sakpal On 21/01/2015 For checlist table creation
    [WebMethod]
    public string INS_CheckList_Table(string Checklist_ID, string Parent_ID, string Version, string CheckList_Name, string Vessel_Type, string checklist_Type, string Location_Grade, string Created_By, string Modified_By, string Status)
    {
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();

        //int chkID = objBl.Insert_CheckList(UDFLib.ConvertIntegerToNull(Checklist_ID), CheckList_Name, int.Parse(Vessel_Type), int.Parse(checklist_Type), int.Parse(Location_Grade), UDFLib.ConvertIntegerToNull(Created_By), UDFLib.ConvertIntegerToNull(Modified_By));
        int chkID = objBl.Insert_CheckList(UDFLib.ConvertIntegerToNull(Checklist_ID), UDFLib.ConvertIntegerToNull(Parent_ID), Version, CheckList_Name, int.Parse(Vessel_Type), int.Parse(checklist_Type), int.Parse(Location_Grade),
            UDFLib.ConvertIntegerToNull(Created_By), UDFLib.ConvertIntegerToNull(Modified_By), Convert.ToInt32(Status));

        return Get_CheckList_Table(Convert.ToString(chkID));
    }

    [WebMethod]
    public string INS_Publish_CheckList(string Checklist_ID, string Created_By, string Status)
    {
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();

        int chkID = objBl.Insert_PublishCheckList(UDFLib.ConvertIntegerToNull(Checklist_ID), UDFLib.ConvertIntegerToNull(Created_By), Convert.ToInt32(Status));

        if (chkID == 0)
        {
            return Get_CheckList_Table(Convert.ToString(Checklist_ID)) + "Alert";

        }

        return Get_CheckList_Table(Convert.ToString(chkID));

    }

    [WebMethod]
    public string INS_Get_CheckList(string Checklist_ID)
    {
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();

        //  int chkID = objBl.Insert_EditCheckList(UDFLib.ConvertIntegerToNull(Checklist_ID));

        return Get_CheckList_Table(Convert.ToString(Checklist_ID));

    }

    [WebMethod]
    public int INS_Edit_CheckList(string Checklist_ID)
    {
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();

        int chkID = objBl.Insert_EditCheckList(UDFLib.ConvertIntegerToNull(Checklist_ID));

        return chkID;

    }


    //Added By Pranav Sakpal On 21/01/2015 For checlist table creation
    [WebMethod]
    public string INS_CHKGroupANDLocation(string ID, string ParentID, string Checklist_ID, string LocationId, string NodeType, string Description, string Created_By, string Modified_By, int? ActiveStatus)
    {
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();

        DataTable dt = new DataTable("LIB_UDTT_ID_VALUE");
        dt.Columns.Add("ID", typeof(string));
        dt.Columns.Add("VALUE", typeof(string));

        if (NodeType == "Item")
        {
            if (Description.Contains('{') && Description.Contains('}'))
            {
                string[] strsplitBrace = Description.Split('{', '}');

                for (int i = 0; i < strsplitBrace.Count(); i++)
                {
                    if ((strsplitBrace[i] != "") && (strsplitBrace[i] != ","))
                    {

                        string[] eachRow = strsplitBrace[i].Split(':', ',');

                        if (eachRow.Count() > 0)
                        {
                            dt.Rows.Add(eachRow[0] == "ID" ? eachRow[1] : null, eachRow[2] == "Active_Status" ? eachRow[3] : null);
                        }

                        // dt.Rows.Add(
                    }

                }
            }
            Description = "";
        }



        int chkID = objBl.Insert_CheckListItem(UDFLib.ConvertIntegerToNull(ID), UDFLib.ConvertIntegerToNull(ParentID), UDFLib.ConvertToInteger(Checklist_ID), UDFLib.ConvertIntegerToNull(LocationId), dt, NodeType, Description, UDFLib.ConvertIntegerToNull(Created_By), UDFLib.ConvertIntegerToNull(Modified_By), ActiveStatus);

        return Get_CheckList_Table(Checklist_ID);
    }

    [WebMethod]
    public string Get_CheckListName_Table(string ScheduleID)
    {
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();

        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        Dictionary<string, UDCToolTip> dictoolTip = new Dictionary<string, UDCToolTip>();
        List<UDCAction> dicAction = new List<UDCAction>();
        Dictionary<string, string> dicNode = new Dictionary<string, string>();
        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();

        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "ShowSelectedChecklist", "onclick" } }, new string[] { "ChecklistID" }, "CheckList_Name"));


        DataTable dtTable = objBl.Get_CheckListName(int.Parse(ScheduleID));

        return UDFLib.CreateHtmlTableFromDataTable(dtTable, new string[] { "CheckList Name" }, new string[] { "CheckList_Name" }, dicLink, dictoolTip, new string[] { "Left" },
            "tbl-common-Css", "hdr-common-Css-Vertical", "row-common-Css-Vertical", "row-common-Css-Vertical", dicAction, dicJSEvent);


    }

    [WebMethod]
    public string Get_DirectBindChecklist_Table(string ScheduleID, string Inspection_ID)
    {
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();

        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        Dictionary<string, UDCToolTip> dictoolTip = new Dictionary<string, UDCToolTip>();
        List<UDCAction> dicAction = new List<UDCAction>();
        Dictionary<string, string> dicNode = new Dictionary<string, string>();
        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();

        dicJSEvent.Add(new UDCJSEvent(new string[][] { new string[] { "ShowSelectedChecklist", "onclick" } }, new string[] { "ChecklistID" }, "CheckList_Name"));

        string strOP = "";
        DataTable dtchkTable = objBl.Get_CheckListName(int.Parse(ScheduleID));

        if (dtchkTable.Rows.Count > 0 && dtchkTable.Rows.Count < 2)
        {

            strOP = Get_CheckListTable_Ratings(dtchkTable.Rows[0][0].ToString(), Inspection_ID);
        }
        return strOP;
    }

    [WebMethod]
    public string Get_CheckListTable_Ratings(string CheckList_ID, string Inspection_ID)
    {
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        Dictionary<string, UDCToolTip> dictoolTip = new Dictionary<string, UDCToolTip>();
        List<UDCAction> dicAction = new List<UDCAction>();

        Dictionary<string, string> dicNode = new Dictionary<string, string>();
        List<UDCJSEvent> dicJSEvent = new List<UDCJSEvent>();

        DataTable dtTable = objBl.Get_CurrentCheckListRatings(int.Parse(CheckList_ID), int.Parse(Inspection_ID));
        DataTable dtLocation = new DataTable();
        string LocGradeType = "";
        if (dtTable.Rows.Count > 0)
        {
            dtLocation = objBl.Get_CheckListLocations(int.Parse(dtTable.Rows[0]["Vessel_Type"].ToString()));
            LocGradeType = dtTable.Rows[0]["Grading_Type"].ToString();
        }

        return CreateHtmlTableChecklistWithRatings(dtTable, objBl.Get_GradesWithOption(), dtLocation, LocGradeType, Inspection_ID,
            new string[] { "", "", "" },
           new string[] { "CheckList_Name", "NodeType", "CheckList_Type_name" },
           dicLink, dictoolTip,
           new string[] { "left", "left", "left", "left", "left" },
            "tbl-common-Css",
              "hdr-common-Css-Vertical",
           "row-common-Css-Vertical",
           "row-common-Css-Vertical",
           dicAction,
           dicJSEvent,
           dicNode) + "<div><input type='hidden' name='hdnchkID' ID='hdnchkID' value=" + CheckList_ID + "></div>"; ;
    }

    public static string CreateHtmlTableChecklistWithRatings(DataTable dtTable, DataTable dtTableOFGrades, DataTable dtTableLocations, string LocGradeType, string Inspection_ID, string[] HeaderCaptions, string[] DataColumnsName, Dictionary<string, UDCHyperLink> DicLink, Dictionary<string, UDCToolTip> DicToolTip, string[] DataColumnsAlignment, string TableCss, string HeaderCss, string RowStyleCss, string AlternateRowStyleCss, List<UDCAction> dicAction, List<UDCJSEvent> dicJSEvent, Dictionary<string, string> NodeType)
    {
        StringBuilder strTable = new StringBuilder();
        try
        {
            if (dtTable.Rows.Count > 0)
            {
                strTable.Append("<table id='__tbl_ChecklistRatings' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
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
                    bool tdFlag = false;
                    if (dtTable.Rows[0]["NodeType"].ToString().Replace("\n", "<br>") != "Group" && dtTable.Rows[0]["NodeType"].ToString().Replace("\n", "<br>") != "Location" && dtTable.Rows[0]["NodeType"].ToString().Replace("\n", "<br>") != "Item")
                    {
                        if (dtTable.Rows[0]["Vessel_Type"].ToString() != "" && dtTable.Rows[0]["Vessel_Type"] != DBNull.Value)
                        {
                            vesseltype_ID = dtTable.Rows[0]["Vessel_Type"].ToString();
                        }
                        strTable.Append("<tr style='background-color: #efefef;'>");/// Added by Anjali DT:17-May-2016 JIT:9319 ||
                        strTable.Append("<td ");
                        #region . tooltip and  javascript event

                        for (int i = 0; i < DataColumnsName.Length; i++)
                        {

                            UDCToolTip objToolTip;
                            if (DicToolTip.TryGetValue(DataColumnsName[i], out objToolTip))// check for tool tip
                            {
                                if (objToolTip.UseControlToolTip)
                                    strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dtTable.Rows[0][objToolTip.ToolTipSourceColumn])) + "'");
                                else
                                {
                                    if (Convert.ToString(dtTable.Rows[0][objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                                    {
                                        string s = "";
                                    }
                                    strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dtTable.Rows[0][objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                                    strTable.Append(" style='cursor:default' ");
                                }
                            }



                            if (dicJSEvent.Count > 0)
                            {
                                foreach (UDCJSEvent ievt in dicJSEvent)
                                {
                                    if (ievt.EventColumnName.ToUpper() == DataColumnsName[i].ToUpper())
                                    {
                                        string funparam = "";
                                        foreach (string iprm in ievt.ParamDataColumn)
                                        {
                                            if (funparam.Length > 0)
                                                funparam += ",";

                                            funparam += "\"" + dtTable.Rows[0][iprm] + "\"";
                                        }

                                        funparam += ",event,this";

                                        foreach (string[] fun in ievt.FunctionName)
                                        {
                                            strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                                        }

                                        strTable.Append(" style='cursor:default' ");
                                    }
                                }
                            }

                        #endregion
                            if (tdFlag == false)
                            {
                                strTable.Append(" > ");
                            }
                            #region . column data

                            UDCHyperLink objA;
                            if (DicLink.TryGetValue(DataColumnsName[i], out objA)) //check for hyperlink
                            {


                                if (string.IsNullOrWhiteSpace(objA.HyperLinkColumnName))//link is fixed
                                    strTable.Append("<a href='" + objA.NaviagteURL);
                                else
                                    strTable.Append("<a  href='../" + Convert.ToString(dtTable.Rows[0][objA.HyperLinkColumnName]));// link can be different for each row

                                for (int iQs = 0; iQs < objA.QueryStringDataColumn.Length; iQs++)// create querystring
                                {
                                    if (iQs == 0)
                                        strTable.Append("?");

                                    strTable.Append(objA.QueryStringText[iQs] + "=" + dtTable.Rows[0][objA.QueryStringDataColumn[iQs]].ToString());

                                    if (iQs < objA.QueryStringDataColumn.Length - 1)
                                        strTable.Append("&");
                                }
                                strTable.Append("'");

                                if (!string.IsNullOrWhiteSpace(objA.Target))
                                    strTable.Append(" target='" + objA.Target + "' ");
                                else
                                    strTable.Append(" target='_blank'");


                                strTable.Append(" > ");

                                strTable.Append(dtTable.Rows[0][DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                                strTable.Append("</a>");

                            }
                            else
                            {

                                if (i == 0)
                                {
                                    strTable.Append("<div class='insp-Chk-Checklist'  id=dv" + DataColumnsName[i] + (rowCount).ToString() + ">");
                                    strTable.Append(dtTable.Rows[0][DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                                    strTable.Append("</div>");
                                }
                                else
                                {
                                    strTable.Append("<div class='insp-Chk-Checklist'  id=dv" + DataColumnsName[i] + (rowCount).ToString() + ">");
                                    strTable.Append(dtTable.Rows[0][DataColumnsName[i]].ToString().Replace("\n", "<br>"));
                                    strTable.Append("</div>");
                                }

                                tdFlag = true;

                            }
                        }

                        strTable.Append("</td>");
                        tdFlag = false;
                        strTable.Append("</tr>");
                            #endregion
                    }

                    int parentPadding = 1;
                    dtTable.DefaultView.RowFilter = "NodeType= 'Group' AND Parent_ID IS NULL";
                    dtTable.DefaultView.Sort = "Index_No asc";
                    DataTable dtgroupDiff = dtTable.DefaultView.ToTable();
                    if (dtgroupDiff.Rows.Count > 0)
                    {
                        strTable.Append(getrecurtionWithRatings(dtgroupDiff, dtTable, dtTableOFGrades, dtTableLocations, LocGradeType, Inspection_ID, null, "", rowCount, parentPadding, DataColumnsName, dicJSEvent, DicToolTip));
                    }
                    dtTable.DefaultView.RowFilter = "NodeType= 'Location' AND Parent_ID IS NULL";
                    dtTable.DefaultView.Sort = "Index_No asc";
                    DataTable dtLocationDiff = dtTable.DefaultView.ToTable();
                    if (dtLocationDiff.Rows.Count > 0)
                    {
                        strTable.Append(getrecurtionWithRatings(dtLocationDiff, dtTable, dtTableOFGrades, dtTableLocations, LocGradeType, Inspection_ID, null, "", CounterRow, parentPadding, DataColumnsName, dicJSEvent, DicToolTip));
                    }

                    // strTable.Append("<tr><td><input type='hidden' name='hdnFinalise' value="++">")
                    //Finalise
                    strTable.Append("<tr><td><input type='hidden' name='hdnFinalise' ID='hdnFinalise1' value=" + Finalise.ToString() + "></td></tr>");
                }
                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'> No record found !</span>");

            return strTable.ToString();
        }
        catch (Exception ex)
        {

            UDFLib.WriteExceptionLog(ex);
            return "";

        }
        finally
        {

        }
    }

    /// <summary>
    /// Method returns HTML string  which display details of checklist assigned in inspection with ratings.
    /// Modified by Anjali DT:2-Jun-2016 JIT:9099
    /// </summary>
    /// <param name="dtdiffTable">Group / Location / Item Table </param>
    /// <param name="dtTable">Current CheckList Ratings</param>
    /// <param name="dtTableOFGrades">Grades With Option</param>
    /// <param name="dtTableLocations">CheckList Locations </param>
    /// <param name="LocGradeType"> Grading Type</param>
    /// <param name="Inspection_ID">Inspection ID for which repost is to be displayed.</param>
    /// <param name="ParentID">ChecklistItem ID / NULL</param>
    /// <param name="index"></param>
    /// <param name="rowCount"> Row Count</param>
    /// <param name="Padding"></param>
    /// <param name="DataColumnsName">Column Name :CheckList_Name", "NodeType", "CheckList_Type_name"</param>
    /// <param name="dicJSEvent"></param>
    /// <param name="DicToolTip"> tool tip </param>
    /// <returns>HTML string </returns>
    public static string getrecurtionWithRatings(DataTable dtdiffTable, DataTable dtTable, DataTable dtTableOFGrades, DataTable dtTableLocations, string LocGradeType, string Inspection_ID, string ParentID, string index, int rowCount, int Padding, string[] DataColumnsName, List<UDCJSEvent> dicJSEvent, Dictionary<string, UDCToolTip> DicToolTip)
    {
        StringBuilder strTable = new StringBuilder();
        strTable.Append("<tr><td");
        int GroupCount = 0;
        int LocCount = 0;
        int QueCount = 0;
        int tdtrCount = 0;

        if (ParentID == null)
        {
            foreach (DataRow dr in dtdiffTable.Rows)
            {
                rowCount++;
                if (tdtrCount == 0)
                {
                    #region . tooltip and  javascript event



                    UDCToolTip objToolTip;
                    if (DicToolTip.TryGetValue(DataColumnsName[0], out objToolTip))// check for tool tip
                    {
                        if (objToolTip.UseControlToolTip)
                            strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                        else
                        {
                            if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                            {
                                string s = "";
                            }
                            strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                            strTable.Append(" style='cursor:default' ");
                        }
                    }



                    if (dicJSEvent.Count > 0)
                    {
                        foreach (UDCJSEvent ievt in dicJSEvent)
                        {
                            if (ievt.EventColumnName.ToUpper() == DataColumnsName[0].ToUpper())
                            {
                                string funparam = "";
                                foreach (string iprm in ievt.ParamDataColumn)
                                {
                                    if (funparam.Length > 0)
                                        funparam += ",";

                                    funparam += "\"" + dr[iprm] + "\"";
                                }

                                funparam += ",event,this";

                                foreach (string[] fun in ievt.FunctionName)
                                {
                                    strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                                }

                                strTable.Append(" style='cursor:default' ");
                            }
                        }
                    }
                    strTable.Append(">");
                    //}
                    #endregion
                }
                if (tdtrCount != 0)
                {
                    strTable.Append("<tr><td  ");

                    #region . tooltip and  javascript event

                    //for (int i = 0; i < DataColumnsName.Length; i++)
                    //{

                    UDCToolTip objToolTip;
                    if (DicToolTip.TryGetValue(DataColumnsName[0], out objToolTip))// check for tool tip
                    {
                        if (objToolTip.UseControlToolTip)
                            strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                        else
                        {
                            if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                            {
                                string s = "";
                            }
                            strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                            strTable.Append(" style='cursor:default' ");
                        }
                    }



                    if (dicJSEvent.Count > 0)
                    {
                        foreach (UDCJSEvent ievt in dicJSEvent)
                        {
                            if (ievt.EventColumnName.ToUpper() == DataColumnsName[0].ToUpper())
                            {
                                string funparam = "";
                                foreach (string iprm in ievt.ParamDataColumn)
                                {
                                    if (funparam.Length > 0)
                                        funparam += ",";

                                    funparam += "\"" + dr[iprm] + "\"";
                                }

                                funparam += ",event,this";

                                foreach (string[] fun in ievt.FunctionName)
                                {
                                    strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                                }

                                strTable.Append(" style='cursor:default' ");
                            }
                        }
                    }
                    strTable.Append(">");
                    //}
                    #endregion
                }
                tdtrCount += 1;
                rowCount += 1;
                if (dr["Parent_ID"] == DBNull.Value)
                {
                    if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Group")
                    {
                        GroupCount++;
                        strTable.Append("<div class='insp-Chk-Group insp-Item-Pos" + Padding.ToString() + "' id=dvGrp" + (rowCount).ToString() + ">");
                        strTable.Append("<span class='insp-IndexNo' > " + GroupCount.ToString().Replace("\n", "<br>") + ".&nbsp</span>");
                        strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
                        strTable.Append("</div>");
                        strTable.Append("</td></tr>");

                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Group'";
                        dtTable.DefaultView.Sort = "Index_No asc";
                        DataTable dtGroupDiff = dtTable.DefaultView.ToTable();
                        if (dtGroupDiff.Rows.Count > 0)
                        {
                            CounterRow = rowCount;
                            strTable.Append(getrecurtionWithRatings(dtGroupDiff, dtTable, dtTableOFGrades, dtTableLocations, LocGradeType, Inspection_ID, dr["ChecklistItem_ID"].ToString(), GroupCount.ToString(), CounterRow, Padding + 1, DataColumnsName, dicJSEvent, DicToolTip));
                        }

                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Location'";
                        dtTable.DefaultView.Sort = "Index_No asc";
                        DataTable dtLocationDiff = dtTable.DefaultView.ToTable();

                        if (dtLocationDiff.Rows.Count > 0)
                        {
                            CounterRow = rowCount;
                            strTable.Append(getrecurtionWithRatings(dtLocationDiff, dtTable, dtTableOFGrades, dtTableLocations, LocGradeType, Inspection_ID, dr["ChecklistItem_ID"].ToString(), GroupCount.ToString(), CounterRow, Padding + 1, DataColumnsName, dicJSEvent, DicToolTip));
                        }
                        Finalise = Convert.ToInt32(dr["Final_Stat"].ToString());
                    }
                    else if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Location" && dr["Parent_ID"] == DBNull.Value)
                    {
                        LocCount++;
                        if (dtTableLocations.Rows.Count > 0)
                        {
                            dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "'";
                            DataTable conterrows = dtTable.DefaultView.ToTable();
                            int countofItems = conterrows.Rows.Count;
                            if (dr["Location_ID"].ToString() != "" && dr["Location_ID"] != DBNull.Value)
                            {
                                dtTableLocations.DefaultView.RowFilter = "ID= '" + dr["Location_ID"].ToString() + "'";
                                DataTable dtLocationDiff = dtTableLocations.DefaultView.ToTable();
                                if (dtLocationDiff.Rows.Count > 0)
                                {
                                    strTable.Append("<div  class='insp-Chk-Location insp-Item-Pos" + Padding.ToString() + "' id=dvLocation" + (rowCount).ToString() + ">");
                                    strTable.Append("<span class='insp-IndexNo' > " + LocCount.ToString().Replace("\n", "<br>") + ".&nbsp</span>");
                                    strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
                                    strTable.Append("<label class='insp-Chk-Location-Text' >&nbsp;" + dtLocationDiff.Rows[0]["LocationName"].ToString().Replace("\n", "<br>") + "</label>");

                                    dtTableOFGrades.DefaultView.RowFilter = "ID= '" + LocGradeType + "'";
                                    DataTable dtLocationRatingDiff = dtTableOFGrades.DefaultView.ToTable();
                                    string strItemID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");

                                    BLL_Tec_Worklist objWl = new BLL_Tec_Worklist();
                                    BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
                                    int JobCount = 0;
                                    if (dr["Location_ID"].ToString() != "")
                                        JobCount = objInsp.INSP_Get_WorklistJobsCountByLocationID(Convert.ToInt32(Inspection_ID), UDFLib.ConvertIntegerToNull(dr["Location_ID"].ToString()));
                                    else
                                        JobCount = objInsp.INSP_Get_WorklistJobsCountByLocationNodeID(Convert.ToInt32(Inspection_ID), Convert.ToInt32(dr["ChecklistItem_ID"].ToString()));

                                    if (dtLocationRatingDiff.Rows.Count > 0)
                                    {
                                        if (countofItems == 0)
                                        {
                                            strTable.Append("<table class='insp-table'><tr><td>");
                                        }
                                        strTable.Append("<span class='rating'>");

                                        // Added on :28-07-2016||To uncheck all stars on click of newly added uncheckstar button..
                                        string strUn_NodeID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                        string strUn_ChecklistID = dr["Checklist_IDCopy"].ToString().Replace("\n", "<br>");
                                        string strun_ID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                        strTable.Append("<img style='width:19px;' src='../../Images/UncheckStars.png'  onclick='UncheckAllStars(\"rating-input-" + strun_ID + "-0\",\"" + strUn_NodeID + "\",\"" + strUn_ChecklistID + "\");'> </img>");

                                        string strLocationID = "";
                                        for (int i = dtLocationRatingDiff.Rows.Count - 1; i >= 0; i--)
                                        {
                                            string strOptionText = dtLocationRatingDiff.Rows[i]["OptionText"].ToString();
                                            string strChecklistID = dr["Checklist_IDCopy"].ToString().Replace("\n", "<br>");
                                            string optionID = (rowCount).ToString() + "-" + (i + 1).ToString();
                                            string strID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                            strLocationID = dr["Location_ID"].ToString().Replace("\n", "<br>");

                                            if (dr["LocationRates"].ToString().Replace("\n", "<br>") != "")
                                            {
                                                if (dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() == "" || dtLocationRatingDiff.Rows[i]["OptionValue"] == DBNull.Value)
                                                {
                                                    strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='0' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                                }
                                                else
                                                {
                                                    if (Convert.ToDecimal(dr["LocationRates"].ToString().Replace("\n", "<br>")) == Convert.ToDecimal(dtLocationRatingDiff.Rows[i]["OptionValue"].ToString()))
                                                    {
                                                        strTable.Append("<input type='radio' checked=true class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (LocCount).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                                    }
                                                    else
                                                    {
                                                        strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (LocCount).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (LocCount).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                            }
                                        }
                                        strTable.Append("</span>");
                                        if (countofItems == 0)
                                        {
                                            strTable.Append("</td><td>&nbsp</td><td>");
                                            strTable.Append("<div class='badge2' title='Attach Job' ><div class='badge1' > <a class='attachJob' badge href='#' onclick='Get_JobsAtteched(\"" + strItemID + "\",\"" + strLocationID + "\");'>" + JobCount.ToString() + "</a> </div></div>");
                                            strTable.Append("</td><td>&nbsp</td><td>");
                                            strTable.Append("<div class='Addshedule' title='Add Job' ><img  class='addJob' src='../../Images/add.GIF' onclick='AddDefect(\"" + strItemID + "\",\"" + strLocationID + "\");' ></div>");
                                            strTable.Append("</td></tr></table>");
                                        }
                                        strTable.Append("</div>");
                                    }
                                    Finalise = Convert.ToInt32(dr["Final_Stat"].ToString());
                                }
                                else
                                {
                                    strTable.Append("<div  class='insp-Chk-Location insp-Item-Pos" + Padding.ToString() + "' id=dvLocation" + (rowCount).ToString() + ">");
                                    strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>") + "&nbsp</span>");
                                    strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));

                                    string strItemID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                    BLL_Tec_Worklist objWl = new BLL_Tec_Worklist();
                                    BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
                                    int JobCount = 0;
                                    if (dr["Location_ID"].ToString() != "")
                                        JobCount = objInsp.INSP_Get_WorklistJobsCountByLocationID(Convert.ToInt32(Inspection_ID), UDFLib.ConvertIntegerToNull(dr["Location_ID"].ToString()));
                                    else
                                        JobCount = objInsp.INSP_Get_WorklistJobsCountByLocationNodeID(Convert.ToInt32(Inspection_ID), Convert.ToInt32(dr["ChecklistItem_ID"].ToString()));

                                    dtTableOFGrades.DefaultView.RowFilter = "ID= '" + LocGradeType + "'";
                                    DataTable dtLocationRatingDiff = dtTableOFGrades.DefaultView.ToTable();
                                    if (countofItems == 0)
                                    {
                                        strTable.Append("<table class='insp-table'><tr><td>");
                                    }
                                    strTable.Append("<span class='rating'>");

                                    // Added on :28-07-2016||To uncheck all stars on click of newly added uncheckstar button..
                                    string strUn_NodeID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                    string strUn_ChecklistID = dr["Checklist_IDCopy"].ToString().Replace("\n", "<br>");
                                    string strun_ID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                    strTable.Append("<img style='width:19px;' src='../../Images/UncheckStars.png'  onclick='UncheckAllStars(\"rating-input-" + strun_ID + "-0\",\"" + strUn_NodeID + "\",\"" + strUn_ChecklistID + "\");'> </img>");

                                    string strLocationID = "";
                                    for (int i = dtLocationRatingDiff.Rows.Count - 1; i >= 0; i--)
                                    {
                                        string strOptionText = dtLocationRatingDiff.Rows[i]["OptionText"].ToString();
                                        string strChecklistID = dr["Checklist_IDCopy"].ToString().Replace("\n", "<br>");
                                        string optionID = (rowCount).ToString() + "-" + (i + 1).ToString();
                                        string strID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                        strLocationID = dr["Location_ID"].ToString().Replace("\n", "<br>");

                                        if (dr["LocationRates"].ToString().Replace("\n", "<br>") != "")
                                        {
                                            if (dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() == "" || dtLocationRatingDiff.Rows[i]["OptionValue"] == DBNull.Value)
                                            {
                                                strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='0' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                            }
                                            else
                                            {
                                                if (Convert.ToDecimal(dr["LocationRates"].ToString().Replace("\n", "<br>")) == Convert.ToDecimal(dtLocationRatingDiff.Rows[i]["OptionValue"].ToString()))
                                                {
                                                    strTable.Append("<input type='radio' checked=true class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                                }
                                                else
                                                {
                                                    strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                        }
                                    }
                                    strTable.Append("</span>");
                                    if (countofItems == 0)
                                    {
                                        strTable.Append("</td><td>&nbsp</td><td>");
                                        strTable.Append("<div class='badge2' title='Attach Job' ><div class='badge1' > <a class='attachJob' badge href='#' onclick='Get_JobsAtteched(\"" + strItemID + "\",\"" + strLocationID + "\");'>" + JobCount.ToString() + "</a> </div></div>");
                                        strTable.Append("</td><td>&nbsp</td><td>");
                                        strTable.Append("<div class='Addshedule' title='Add Job' ><img  class='addJob' src='../../Images/add.GIF' onclick='AddDefect(\"" + strItemID + "\",\"" + strLocationID + "\");' ></div>");
                                        strTable.Append("</td></tr></table>");
                                    }
                                    strTable.Append("</div>");
                                    Finalise = Convert.ToInt32(dr["Final_Stat"].ToString());
                                }
                            }
                            else
                            {
                                strTable.Append("<div  class='insp-Chk-Location insp-Item-Pos" + Padding.ToString() + "' id=dvLocation" + (rowCount).ToString() + ">");
                                strTable.Append("<span class='insp-IndexNo' > " + LocCount.ToString().Replace("\n", "<br>") + ".&nbsp</span>");
                                strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
                                string strItemID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                BLL_Tec_Worklist objWl = new BLL_Tec_Worklist();
                                BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
                                int JobCount = 0;
                                if (dr["Location_ID"].ToString() != "")
                                    JobCount = objInsp.INSP_Get_WorklistJobsCountByLocationID(Convert.ToInt32(Inspection_ID), UDFLib.ConvertIntegerToNull(dr["Location_ID"].ToString()));
                                else
                                    JobCount = objInsp.INSP_Get_WorklistJobsCountByLocationNodeID(Convert.ToInt32(Inspection_ID), Convert.ToInt32(dr["ChecklistItem_ID"].ToString()));

                                dtTableOFGrades.DefaultView.RowFilter = "ID= '" + LocGradeType + "'";
                                DataTable dtLocationRatingDiff = dtTableOFGrades.DefaultView.ToTable();
                                if (countofItems == 0)
                                {
                                    strTable.Append("<table class='insp-table'><tr><td>");
                                }
                                strTable.Append("<span class='rating'>");

                                // Added on :28-07-2016||To uncheck all stars on click of newly added uncheckstar button..
                                string strUn_NodeID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                string strUn_ChecklistID = dr["Checklist_IDCopy"].ToString().Replace("\n", "<br>");
                                string strun_ID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                strTable.Append("<img style='width:19px;' src='../../Images/UncheckStars.png'  onclick='UncheckAllStars(\"rating-input-" + strun_ID + "-0\",\"" + strUn_NodeID + "\",\"" + strUn_ChecklistID + "\");'> </img>");

                                string strLocationID = "";
                                for (int i = dtLocationRatingDiff.Rows.Count - 1; i >= 0; i--)
                                {
                                    string strOptionText = dtLocationRatingDiff.Rows[i]["OptionText"].ToString();
                                    string strChecklistID = dr["Checklist_IDCopy"].ToString().Replace("\n", "<br>");
                                    string optionID = (rowCount).ToString() + "-" + (i + 1).ToString();
                                    string strID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                    strLocationID = dr["Location_ID"].ToString().Replace("\n", "<br>");

                                    if (dr["LocationRates"].ToString().Replace("\n", "<br>") != "")
                                    {
                                        if (dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() == "" || dtLocationRatingDiff.Rows[i]["OptionValue"] == DBNull.Value)
                                        {
                                            strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='0' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                        }
                                        else
                                        {
                                            if (Convert.ToDecimal(dr["LocationRates"].ToString().Replace("\n", "<br>")) == Convert.ToDecimal(dtLocationRatingDiff.Rows[i]["OptionValue"].ToString()))
                                            {
                                                strTable.Append("<input type='radio' checked=true class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                            }
                                            else
                                            {

                                                strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                            }
                                        }
                                    }
                                    else
                                    {

                                        strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                    }
                                }
                                strTable.Append("</span>");
                                if (countofItems == 0)
                                {
                                    strTable.Append("</td><td>&nbsp</td><td>");
                                    strTable.Append("<div class='badge2' title='Attach Job' ><div class='badge1' > <a class='attachJob' badge href='#' onclick='Get_JobsAtteched(\"" + strItemID + "\",\"" + strLocationID + "\");'>" + JobCount.ToString() + "</a> </div></div>");
                                    strTable.Append("</td><td>&nbsp</td><td>");
                                    strTable.Append("<div class='Addshedule' title='Add Job' ><img  class='addJob' src='../../Images/add.GIF' onclick='AddDefect(\"" + strItemID + "\",\"" + strLocationID + "\");' ></div>");
                                    strTable.Append("</td></tr></table>");
                                }
                                strTable.Append("</div>");
                                Finalise = Convert.ToInt32(dr["Final_Stat"].ToString());
                            }
                        }

                        strTable.Append("</td></tr>");
                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "'";
                        dtTable.DefaultView.Sort = "Index_No asc";
                        DataTable dtItemDiff = dtTable.DefaultView.ToTable();
                        if (dtItemDiff.Rows.Count > 0)
                        {
                            CounterRow = rowCount;
                            strTable.Append(getrecurtionWithRatings(dtItemDiff, dtTable, dtTableOFGrades, dtTableLocations, LocGradeType, Inspection_ID, dr["ChecklistItem_ID"].ToString(), LocCount.ToString(), CounterRow, Padding + 1, DataColumnsName, dicJSEvent, DicToolTip));
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
                if (tdtrCount == 0)
                {

                    #region . tooltip and  javascript event



                    UDCToolTip objToolTip;
                    if (DicToolTip.TryGetValue(DataColumnsName[0], out objToolTip))// check for tool tip
                    {
                        if (objToolTip.UseControlToolTip)
                            strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                        else
                        {
                            if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                            {
                                string s = "";
                            }
                            strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                            strTable.Append(" style='cursor:default' ");
                        }
                    }



                    if (dicJSEvent.Count > 0)
                    {
                        foreach (UDCJSEvent ievt in dicJSEvent)
                        {
                            if (ievt.EventColumnName.ToUpper() == DataColumnsName[0].ToUpper())
                            {
                                string funparam = "";
                                foreach (string iprm in ievt.ParamDataColumn)
                                {
                                    if (funparam.Length > 0)
                                        funparam += ",";

                                    funparam += "\"" + dr[iprm] + "\"";
                                }

                                funparam += ",event,this";

                                foreach (string[] fun in ievt.FunctionName)
                                {
                                    strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                                }

                                strTable.Append(" style='cursor:default' ");
                            }
                        }
                    }
                    strTable.Append(">");
                    //}
                    #endregion

                }
                if (tdtrCount != 0)
                {
                    strTable.Append("<tr><td  ");

                    #region . tooltip and  javascript event

                    //for (int i = 0; i < DataColumnsName.Length; i++)
                    //{

                    UDCToolTip objToolTip;
                    if (DicToolTip.TryGetValue(DataColumnsName[0], out objToolTip))// check for tool tip
                    {
                        if (objToolTip.UseControlToolTip)
                            strTable.Append(" title='" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "'");
                        else
                        {
                            if (Convert.ToString(dr[objToolTip.ToolTipSourceColumn]).Contains("To Attend TAROKO this call Singapore for"))
                            {
                                string s = "";
                            }
                            strTable.Append(" onmouseover='js_ShowToolTip(&#39;" + ReplaceSpecialCharacter(Convert.ToString(dr[objToolTip.ToolTipSourceColumn])) + "&#39;,event,this)'");
                            strTable.Append(" style='cursor:default' ");
                        }
                    }



                    if (dicJSEvent.Count > 0)
                    {
                        foreach (UDCJSEvent ievt in dicJSEvent)
                        {
                            if (ievt.EventColumnName.ToUpper() == DataColumnsName[0].ToUpper())
                            {
                                string funparam = "";
                                foreach (string iprm in ievt.ParamDataColumn)
                                {
                                    if (funparam.Length > 0)
                                        funparam += ",";

                                    funparam += "\"" + dr[iprm] + "\"";
                                }

                                funparam += ",event,this";

                                foreach (string[] fun in ievt.FunctionName)
                                {
                                    strTable.Append("  " + fun[1] + "='" + fun[0] + "(" + funparam + ")'");
                                }

                                strTable.Append(" style='cursor:default' ");
                            }
                        }
                    }
                    strTable.Append(">");
                    //}
                    #endregion
                }
                tdtrCount += 1;
                if (dr["Parent_ID"] != DBNull.Value)
                {
                    if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Group")
                    {
                        GroupCount++;
                        strTable.Append("<div class='insp-Chk-Group insp-Item-Pos" + Padding.ToString() + "' id=dvGrp" + (rowCount).ToString() + ">");
                        strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + GroupCount.ToString().Replace("\n", "<br>") + "&nbsp</span>");
                        strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
                        strTable.Append("</div>");
                        strTable.Append("</td></tr>");

                        string indexNoval = index.ToString() + "." + GroupCount.ToString().Replace("\n", "<br>");
                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Group'";
                        dtTable.DefaultView.Sort = "Index_No asc";
                        DataTable dtGroupDiff = dtTable.DefaultView.ToTable();
                        if (dtGroupDiff.Rows.Count > 0)
                        {
                            CounterRow = rowCount;
                            strTable.Append(getrecurtionWithRatings(dtGroupDiff, dtTable, dtTableOFGrades, dtTableLocations, LocGradeType, Inspection_ID, dr["ChecklistItem_ID"].ToString(), indexNoval, CounterRow, Padding + 1, DataColumnsName, dicJSEvent, DicToolTip));
                        }

                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "' AND Nodetype= 'Location'";
                        dtTable.DefaultView.Sort = "Index_No asc";
                        DataTable dtLocationDiff = dtTable.DefaultView.ToTable();

                        if (dtLocationDiff.Rows.Count > 0)
                        {
                            CounterRow = rowCount;
                            strTable.Append(getrecurtionWithRatings(dtLocationDiff, dtTable, dtTableOFGrades, dtTableLocations, LocGradeType, Inspection_ID, dr["ChecklistItem_ID"].ToString(), indexNoval, CounterRow, Padding + 1, DataColumnsName, dicJSEvent, DicToolTip));
                        }
                        Finalise = Convert.ToInt32(dr["Final_Stat"].ToString());
                    }
                    else if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Location")
                    {
                        LocCount++;

                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "'";
                        DataTable conterrows = dtTable.DefaultView.ToTable();
                        int countofItems = conterrows.Rows.Count;

                        if (dtTableLocations.Rows.Count > 0)
                        {
                            BLL_Tec_Worklist objWl = new BLL_Tec_Worklist();
                            BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
                            int JobCount = 0;
                            if (dr["Location_ID"].ToString() != "")
                                JobCount = objInsp.INSP_Get_WorklistJobsCountByLocationID(Convert.ToInt32(Inspection_ID), UDFLib.ConvertIntegerToNull(dr["Location_ID"].ToString()));
                            else
                                JobCount = objInsp.INSP_Get_WorklistJobsCountByLocationNodeID(Convert.ToInt32(Inspection_ID), Convert.ToInt32(dr["ChecklistItem_ID"].ToString()));

                            string strItemID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");

                            if (dr["Location_ID"].ToString() != "" && dr["Location_ID"] != DBNull.Value)
                            {
                                dtTableLocations.DefaultView.RowFilter = "ID= '" + dr["Location_ID"].ToString() + "'";

                                DataTable dtLocationDiff = dtTableLocations.DefaultView.ToTable();
                                if (dtLocationDiff.Rows.Count > 0)
                                {
                                    strTable.Append("<div  class='insp-Chk-Location insp-Item-Pos" + Padding.ToString() + "' id=dvLocation" + (rowCount).ToString() + ">");
                                    strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>") + "&nbsp</span>");
                                    strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
                                    strTable.Append("<label class='insp-Chk-Location-Text' >&nbsp;" + dtLocationDiff.Rows[0]["LocationName"].ToString().Replace("\n", "<br>") + "</label>");

                                    dtTableOFGrades.DefaultView.RowFilter = "ID= '" + LocGradeType + "'";
                                    DataTable dtLocationRatingDiff = dtTableOFGrades.DefaultView.ToTable();
                                    if (countofItems == 0)
                                    {
                                        strTable.Append("<table class='insp-table'><tr><td>");
                                    }
                                    strTable.Append("<span class='rating'>");

                                    // Added on :28-07-2016||To uncheck all stars on click of newly added uncheckstar button..
                                    string strUn_NodeID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                    string strUn_ChecklistID = dr["Checklist_IDCopy"].ToString().Replace("\n", "<br>");
                                    string strun_ID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                    strTable.Append("<img style='width:19px;' src='../../Images/UncheckStars.png'  onclick='UncheckAllStars(\"rating-input-" + strun_ID + "-0\",\"" + strUn_NodeID + "\",\"" + strUn_ChecklistID + "\");'> </img>");

                                    string strLocationID = "";
                                    for (int i = dtLocationRatingDiff.Rows.Count - 1; i >= 0; i--)
                                    {
                                        string strOptionText = dtLocationRatingDiff.Rows[i]["OptionText"].ToString();
                                        string strChecklistID = dr["Checklist_IDCopy"].ToString().Replace("\n", "<br>");

                                        string optionID = (rowCount).ToString() + "-" + (i + 1).ToString();

                                        string strID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                        strLocationID = dr["Location_ID"].ToString().Replace("\n", "<br>");

                                        if (dr["LocationRates"].ToString().Replace("\n", "<br>") != "")
                                        {
                                            if (dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() == "" || dtLocationRatingDiff.Rows[i]["OptionValue"] == DBNull.Value)
                                            {
                                                strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='0' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                            }
                                            else
                                            {
                                                if (Convert.ToDecimal(dr["LocationRates"].ToString().Replace("\n", "<br>")) == Convert.ToDecimal(dtLocationRatingDiff.Rows[i]["OptionValue"].ToString()))
                                                {
                                                    strTable.Append("<input type='radio' checked=true class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                                }
                                                else
                                                {
                                                    strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                        }
                                    }
                                    strTable.Append("</span>");

                                    if (countofItems == 0)
                                    {
                                        strTable.Append("</td><td>&nbsp</td><td>");
                                        strTable.Append("<div class='badge2' title='Attach Job' ><div class='badge1' > <a class='attachJob' badge href='#' onclick='Get_JobsAtteched(\"" + strItemID + "\",\"" + strLocationID + "\");'>" + JobCount.ToString() + "</a> </div></div>");
                                        strTable.Append("</td><td>&nbsp</td><td>");
                                        strTable.Append("<div class='Addshedule' title='Add Job'><img  class='addJob' src='../../Images/add.GIF' onclick='AddDefect(\"" + strItemID + "\",\"" + strLocationID + "\");' ></div>");
                                        strTable.Append("</td></tr></table>");
                                    }
                                    strTable.Append("</div>");
                                    Finalise = Convert.ToInt32(dr["Final_Stat"].ToString());
                                }
                            }
                            else
                            {
                                strTable.Append("<div  class='insp-Chk-Location insp-Item-Pos" + Padding.ToString() + "' id=dvLocation" + (rowCount).ToString() + ">");
                                strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>") + "&nbsp</span>");
                                strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));

                                dtTableOFGrades.DefaultView.RowFilter = "ID= '" + LocGradeType + "'";
                                DataTable dtLocationRatingDiff = dtTableOFGrades.DefaultView.ToTable();
                                if (countofItems == 0)
                                {
                                    strTable.Append("<table class='insp-table'><tr><td>");
                                }

                                strTable.Append("<span class='rating'>");


                                // Added on :28-07-2016||To uncheck all stars on click of newly added uncheckstar button..
                                string strUn_NodeID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                string strUn_ChecklistID = dr["Checklist_IDCopy"].ToString().Replace("\n", "<br>");
                                string strun_ID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                strTable.Append("<img style='width:19px;' src='../../Images/UncheckStars.png'  onclick='UncheckAllStars(\"rating-input-" + strun_ID + "-0\",\"" + strUn_NodeID + "\",\"" + strUn_ChecklistID + "\");'> </img>");

                                string strLocationID = "";
                                for (int i = dtLocationRatingDiff.Rows.Count - 1; i >= 0; i--)
                                {
                                    string strOptionText = dtLocationRatingDiff.Rows[i]["OptionText"].ToString();
                                    string strChecklistID = dr["Checklist_IDCopy"].ToString().Replace("\n", "<br>");
                                    string optionID = (rowCount).ToString() + "-" + (i + 1).ToString();
                                    string strID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                    strLocationID = dr["Location_ID"].ToString().Replace("\n", "<br>");

                                    if (dr["LocationRates"].ToString().Replace("\n", "<br>") != "")
                                    {
                                        if (dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() == "" || dtLocationRatingDiff.Rows[i]["OptionValue"] == DBNull.Value)
                                        {
                                            strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='0' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                        }
                                        else
                                        {
                                            if (Convert.ToDecimal(dr["LocationRates"].ToString().Replace("\n", "<br>")) == Convert.ToDecimal(dtLocationRatingDiff.Rows[i]["OptionValue"].ToString()))
                                            {
                                                strTable.Append("<input type='radio' checked=true class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                            }
                                            else
                                            {
                                                strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                    }
                                }
                                strTable.Append("</span>");
                                if (countofItems == 0)
                                {
                                    strTable.Append("</td><td>&nbsp</td><td>");
                                    strTable.Append("<div class='badge2' title='Attach Job'><div class='badge1' > <a class='attachJob' badge href='#' onclick='Get_JobsAtteched(\"" + strItemID + "\",\"" + strLocationID + "\");'>" + JobCount.ToString() + "</a> </div></div>");
                                    strTable.Append("</td><td>&nbsp</td><td>");
                                    strTable.Append("<div class='Addshedule' title='Add Job' ><img  class='addJob' src='../../Images/add.GIF' onclick='AddDefect(\"" + strItemID + "\",\"" + strLocationID + "\");' ></div>");
                                    strTable.Append("</td></tr></table>");
                                }
                                strTable.Append("</div>");
                                Finalise = Convert.ToInt32(dr["Final_Stat"].ToString());
                            }
                        }
                        else
                        {
                            strTable.Append("<div  class='insp-Chk-Location insp-Item-Pos" + Padding.ToString() + "' id=dvLocation" + (rowCount).ToString() + ">");
                            strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>") + "&nbsp</span>");
                            strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
                            string strItemID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                            BLL_Tec_Worklist objWl = new BLL_Tec_Worklist();
                            BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
                            int JobCount = 0;
                            if (dr["Location_ID"].ToString() != "")
                                JobCount = objInsp.INSP_Get_WorklistJobsCountByLocationID(Convert.ToInt32(Inspection_ID), UDFLib.ConvertIntegerToNull(dr["Location_ID"].ToString()));
                            else
                                JobCount = objInsp.INSP_Get_WorklistJobsCountByLocationNodeID(Convert.ToInt32(Inspection_ID), Convert.ToInt32(dr["ChecklistItem_ID"].ToString()));

                            dtTableOFGrades.DefaultView.RowFilter = "ID= '" + LocGradeType + "'";
                            DataTable dtLocationRatingDiff = dtTableOFGrades.DefaultView.ToTable();
                            if (countofItems == 0)
                            {
                                strTable.Append("<table class='insp-table'><tr><td>");
                            }
                            strTable.Append("<span class='rating'>");

                            // Added on :28-07-2016||To uncheck all stars on click of newly added uncheckstar button..
                            string strUn_NodeID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                            string strUn_ChecklistID = dr["Checklist_IDCopy"].ToString().Replace("\n", "<br>");
                            string strun_ID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                            strTable.Append("<img style='width:19px;' src='../../Images/UncheckStars.png'  onclick='UncheckAllStars(\"rating-input-" + strun_ID + "-0\",\"" + strUn_NodeID + "\",\"" + strUn_ChecklistID + "\");'> </img>");

                            string strLocationID = "";
                            for (int i = dtLocationRatingDiff.Rows.Count - 1; i >= 0; i--)
                            {
                                string strOptionText = dtLocationRatingDiff.Rows[i]["OptionText"].ToString();
                                string strChecklistID = dr["Checklist_IDCopy"].ToString().Replace("\n", "<br>");
                                string optionID = (rowCount).ToString() + "-" + (i + 1).ToString();
                                string strID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                strLocationID = dr["Location_ID"].ToString().Replace("\n", "<br>");

                                if (dr["LocationRates"].ToString().Replace("\n", "<br>") != "")
                                {
                                    if (dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() == "" || dtLocationRatingDiff.Rows[i]["OptionValue"] == DBNull.Value)
                                    {
                                        strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='0' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                    }
                                    else
                                    {
                                        if (Convert.ToDecimal(dr["LocationRates"].ToString().Replace("\n", "<br>")) == Convert.ToDecimal(dtLocationRatingDiff.Rows[i]["OptionValue"].ToString()))
                                        {
                                            strTable.Append("<input type='radio' checked=true class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");

                                        }
                                        else
                                        {
                                            strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                        }
                                    }
                                }
                                else
                                {
                                    strTable.Append("<input type='radio' class='rating-input' id='rating-input-" + strID + "-" + i.ToString() + "' name='rating-input-" + (index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>")).ToString() + "' value='" + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + "' > <label for='rating-input-" + strID + "-" + i.ToString() + "' class='rating-star' onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dtLocationRatingDiff.Rows[i]["OptionValue"].ToString() + ", \"" + strOptionText + "\",\"Location\",\"" + strChecklistID + "\");' ></label>");
                                }
                            }
                            strTable.Append("</span>");
                            if (countofItems == 0)
                            {
                                strTable.Append("</td><td>&nbsp</td><td>");
                                strTable.Append("<div class='badge2' title='Attach Job' ><div class='badge1' > <a class='attachJob' badge href='#' onclick='Get_JobsAtteched(\"" + strItemID + "\",\"" + strLocationID + "\");'>" + JobCount.ToString() + "</a> </div></div>");
                                strTable.Append("</td><td>&nbsp</td><td>");
                                strTable.Append("<div class='Addshedule' title='Add Job' ><img  class='addJob' src='../../Images/add.GIF' onclick='AddDefect(\"" + strItemID + "\",\"" + strLocationID + "\");' ></div>");
                                strTable.Append("</td></tr></table>");
                            }
                            strTable.Append("</div>");
                            Finalise = Convert.ToInt32(dr["Final_Stat"].ToString());
                        }
                        strTable.Append("</td></tr>");
                        string indexNoval = index.ToString() + "." + LocCount.ToString().Replace("\n", "<br>");

                        dtTable.DefaultView.RowFilter = "Parent_ID= '" + dr["ChecklistItem_ID"].ToString() + "'";
                        dtTable.DefaultView.Sort = "Index_No asc";
                        DataTable dtItemDiff = dtTable.DefaultView.ToTable();
                        if (dtItemDiff.Rows.Count > 0)
                        {
                            CounterRow = rowCount;
                            strTable.Append(getrecurtionWithRatings(dtItemDiff, dtTable, dtTableOFGrades, dtTableLocations, LocGradeType, Inspection_ID, dr["ChecklistItem_ID"].ToString(), indexNoval, CounterRow, Padding + 1, DataColumnsName, dicJSEvent, DicToolTip));
                        }
                    }
                    else if (dr["NodeType"].ToString().Replace("\n", "<br>") == "Item")
                    {
                        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();
                        DataTable dtLOCNodeAndLoc = objBl.Get_LocationNodeANDLocationID(Convert.ToInt32(dr["Checklist_IDCopy"].ToString()), Convert.ToInt32(Inspection_ID), Convert.ToInt32(dr["ChecklistItem_ID"].ToString()));
                        string LocNodeID = "";
                        string LocID = "";

                        if (dtLOCNodeAndLoc.Rows.Count > 0)
                        {
                            LocNodeID = dr["ChecklistItem_ID"].ToString();//dtLOCNodeAndLoc.Rows[0][0].ToString();
                            LocID = dtLOCNodeAndLoc.Rows[0][1].ToString();
                        }

                        BLL_Tec_Worklist objWl = new BLL_Tec_Worklist();
                        BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
                        int JobCount = 0;
                        JobCount = objInsp.INSP_Get_WorklistJobsCountByLocationNodeID(Convert.ToInt32(Inspection_ID), Convert.ToInt32(dr["ChecklistItem_ID"].ToString()));
                        QueCount++;
                        string strItemID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                        strTable.Append("<div class='insp-Chk-Item-Container insp-Item-Pos" + Padding.ToString() + "'  id=dvItem-" + (rowCount).ToString() + ">");

                        strTable.Append("<span class='insp-IndexNo' > " + index.ToString() + "." + QueCount.ToString() + "&nbsp</span>");
                        strTable.Append(dr["CheckList_Name"].ToString().Replace("\n", "<br>"));
                        strTable.Append("<table class='insp-table'><tr><td>");
                        strTable.Append("<div class='badge2' title='Attach Job'><div class='badge1' > <a class='attachJob' badge href='#' onclick='Get_JobsAtteched(\"" + LocNodeID + "\",\"" + LocID + "\");'>" + JobCount.ToString() + "</a> </div></div>");
                        strTable.Append("</td><td><td>&nbsp</td>");
                        strTable.Append("<div class='Addshedule' title='Add Job' ><img  class='addJob' src='../../Images/add.GIF' onclick='AddDefect(\"" + LocNodeID + "\",\"" + LocID + "\");' ></div>");
                        strTable.Append("</td></tr></table>");
                        strTable.Append("</div>");
                        strTable.Append("</td></tr>");

                        if (dtTableOFGrades.Rows.Count > 0)
                        {
                            dtTableOFGrades.DefaultView.RowFilter = "ID= '" + dr["ItemGrading_Type"].ToString() + "'";
                            string strLocationID = "0";
                            DataTable dtitemDiff = dtTableOFGrades.DefaultView.ToTable();
                            if (dtitemDiff.Rows.Count > 0)
                            {
                                int itemCount = 0;
                                string optionID = (rowCount).ToString();
                                strTable.Append("<tr><td>");
                                strTable.Append("<div class='insp-Item-Pos" + (Padding + 1).ToString() + "'  id=dvItemOptions" + (rowCount).ToString() + ">");
                                foreach (DataRow dr1 in dtitemDiff.Rows)
                                {
                                    string stroptiontext = dr1["OptionText"].ToString().Replace("\n", "<br>");
                                    string strChecklistID = dr["Checklist_IDCopy"].ToString().Replace("\n", "<br>");
                                    string optionName = index.ToString() + "-" + QueCount.ToString();
                                    string strID = dr["ChecklistItem_ID"].ToString().Replace("\n", "<br>");
                                    itemCount += 1;
                                    if (dr["ItemRates"].ToString().Replace("\n", "<br>") != "")
                                    {
                                        if (Convert.ToDecimal(dr["ItemRates"].ToString().Replace("\n", "<br>")) == Convert.ToDecimal(dr1["OptionValue"].ToString()))
                                        {
                                            strTable.Append("<input type='radio' checked=true class='Item-Option' name='dvItem-" + optionName + "' value='" + dr1["optionID"].ToString().Replace("\n", "<br>") + "'  onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dr1["OptionValue"].ToString().Replace("\n", "<br>") + ", \"" + stroptiontext + "\",\"Item\",\"" + strChecklistID + "\");'>" + dr1["OptionText"].ToString().Replace("\n", "<br>"));
                                        }
                                        else
                                        {
                                            strTable.Append("<input type='radio' class='Item-Option'  name='dvItem-" + optionName + "' value='" + dr1["optionID"].ToString().Replace("\n", "<br>") + "'  onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dr1["OptionValue"].ToString().Replace("\n", "<br>") + ", \"" + stroptiontext + "\",\"Item\",\"" + strChecklistID + "\");'>" + dr1["OptionText"].ToString().Replace("\n", "<br>"));
                                        }
                                    }
                                    else
                                    {
                                        strTable.Append("<input type='radio' class='Item-Option' name='dvItem-" + optionName + "' value='" + dr1["optionID"].ToString().Replace("\n", "<br>") + "'  onclick='Get_Ratings(\"" + strLocationID + "\",\"" + strID + "\", " + dr1["OptionValue"].ToString().Replace("\n", "<br>") + ", \"" + stroptiontext + "\",\"Item\",\"" + strChecklistID + "\");'>" + dr1["OptionText"].ToString().Replace("\n", "<br>"));
                                    }
                                    rowCount++;
                                }
                                strTable.Append("</div>");
                                strTable.Append("</td></tr>");
                            }
                        }
                        Finalise = Convert.ToInt32(dr["Final_Stat"].ToString());
                    }
                }

            }
        }

        return strTable.ToString();
    }

    [WebMethod]
    public string INS_LocationRatings(string Location_ID, string Node_ID, string System_Current_Report, string Location_Rating, string Schedule_ID, string Inspection_ID, string Additional_Remarks, string Created_By, string cheklistID)
    {
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();

        int chkID = objBl.Insert_LocationRatings(UDFLib.ConvertIntegerToNull(Location_ID), Convert.ToInt32(Node_ID), Convert.ToDecimal(System_Current_Report), Location_Rating, UDFLib.ConvertIntegerToNull(Schedule_ID), Convert.ToInt32(Inspection_ID), Additional_Remarks, UDFLib.ConvertIntegerToNull(Created_By));//UDFLib.ConvertIntegerToNull(ID), UDFLib.ConvertIntegerToNull(ParentID), UDFLib.ConvertToInteger(Checklist_ID), UDFLib.ConvertIntegerToNull(LocationId),  NodeType, Description, UDFLib.ConvertIntegerToNull(Created_By), UDFLib.ConvertIntegerToNull(Modified_By), ActiveStatus);

        return Get_CheckListTable_Ratings(cheklistID, Inspection_ID);
        //return chkID.ToString();
    }

    [WebMethod]
    public string INS_ItemRatings(string Item_ID, string System_Current_Report, string Item_Rating, string Schedule_ID, string Inspection_ID, string Additional_Remarks, string Created_By, string cheklistID)
    {
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();

        int chkID = objBl.Insert_ItemRatings(Convert.ToInt32(Item_ID), Convert.ToDecimal(System_Current_Report), Item_Rating, UDFLib.ConvertIntegerToNull(Schedule_ID), Convert.ToInt32(Inspection_ID), Additional_Remarks, UDFLib.ConvertIntegerToNull(Created_By));//UDFLib.ConvertIntegerToNull(ID), UDFLib.ConvertIntegerToNull(ParentID), UDFLib.ConvertToInteger(Checklist_ID), UDFLib.ConvertIntegerToNull(LocationId),  NodeType, Description, UDFLib.ConvertIntegerToNull(Created_By), UDFLib.ConvertIntegerToNull(Modified_By), ActiveStatus);

        return Get_CheckListTable_Ratings(cheklistID, Inspection_ID);
        //return chkID.ToString();
    }

    [WebMethod]
    //public string INS_Checklist_Ratings( string Node_ID, string System_Current_Report, string Inspection_ID, string Created_By, string cheklistID)
    public string INS_Checklist_Ratings(string arr)
    {
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();
        string op = "";

        DataTable dt = new DataTable();
        dt.Columns.Add("Node_ID", typeof(string));
        dt.Columns.Add("System_Current_Report", typeof(string));
        dt.Columns.Add("Inspection_ID", typeof(string));
        dt.Columns.Add("Created_By", typeof(string));
        dt.Columns.Add("cheklistID", typeof(string));


        if (arr.Contains('{') && arr.Contains('}'))
        {
            string[] strsplitBrace = arr.Split('{', '}');

            for (int i = 0; i < strsplitBrace.Count(); i++)
            {
                if ((strsplitBrace[i] != "") && (strsplitBrace[i] != ","))
                {

                    string[] eachRow = strsplitBrace[i].Split(':', ',');

                    if (eachRow.Count() > 0)
                    {
                        dt.Rows.Add(eachRow[0] == "Node_ID" ? eachRow[1] : null, eachRow[2] == "System_Current_Report" ? eachRow[3] : null, eachRow[4] == "Inspection_ID" ? eachRow[5] : null, eachRow[6] == "Created_By" ? eachRow[7] : null, eachRow[8] == "cheklistID" ? eachRow[9] : null);
                        //eachRow[0] == "Node_ID" ? eachRow[1] : null, eachRow[2] == "System_Current_Report" ? eachRow[3] : null, "Inspection_ID" ? eachRow[4] : null, eachRow[2] == "Created_By" ? eachRow[3] : null);
                    }

                    // dt.Rows.Add(
                }

            }
            //int chkID = objBl.Insert_ChecklistRatings(dt);
            DataTable dtOP = objBl.Insert_ChecklistRatings(dt);
            if (dtOP.Rows.Count > 0)
            {
                op = Get_CheckListTable_Ratings(dtOP.Rows[0][0].ToString(), dtOP.Rows[0][1].ToString());
            }

        }
        else
        {
            //if(dt.Rows.Count>0)
            if (arr != "")
            {
                string[] arrsplit = arr.Split(',');
                op = Get_CheckListTable_Ratings(arrsplit[0], arrsplit[1]);
            }

        }

        return op;

        //int chkID = objBl.Insert_ChecklistRatings( Convert.ToInt32(Node_ID), Convert.ToDecimal(System_Current_Report),  Convert.ToInt32(Inspection_ID), UDFLib.ConvertIntegerToNull(Created_By));//UDFLib.ConvertIntegerToNull(ID), UDFLib.ConvertIntegerToNull(ParentID), UDFLib.ConvertToInteger(Checklist_ID), UDFLib.ConvertIntegerToNull(LocationId),  NodeType, Description, UDFLib.ConvertIntegerToNull(Created_By), UDFLib.ConvertIntegerToNull(Modified_By), ActiveStatus);

        //return Get_CheckListTable_Ratings("6", "49");
        //return Get_CheckListTable_Ratings(cheklistID, Inspection_ID);
        //return chkID.ToString();
    }


    [WebMethod]
    public string INS_ExportToExcel(string CheckList_ID, string Inspection_ID)
    {
        string HtmlBody = Get_CheckListTable_Ratings(CheckList_ID, Inspection_ID);

        System.IO.File.WriteAllText(@"c:\test\abc.xls", HtmlBody);

        return "Exported....";
    }

    [WebMethod]
    public string Finalize(string CheckList_ID, string Inspection_ID, string Shedule_ID, string Created_By)
    {
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();
        objBl.Insert_Final_ChecklistRatings(Convert.ToInt32(CheckList_ID), Convert.ToInt32(Inspection_ID), UDFLib.ConvertIntegerToNull(Created_By));
        return Get_CheckListTable_Ratings(CheckList_ID, Inspection_ID);

    }

    [WebMethod]
    public string BindRadioButtons(string Grade_ID)
    {
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();


        DataTable dt = objBl.Get_GradingOptions(Convert.ToInt32(Grade_ID));

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);

        //return dt;

    }


    [WebMethod]
    public string SaveChecklistQuestion(string Criteria, string catID, string GradingType, string createdBy)
    {
        BLL_INSP_Checklist objBl = new BLL_INSP_Checklist();


        int i = objBl.INSERT_Question(Criteria, int.Parse(catID), int.Parse(GradingType), int.Parse(createdBy));

        if (i == 0)
            return "Fail";
        else
            return "Success";

        //return serializer.Serialize(rows);

        //return dt;
    }


    #region InspCalendar
    [WebMethod]
    public string asyncGet_InspInfo(int UserId, string Schedule_Date, string VESSEL_NAME, string lEndDate)
    {
        System.Text.StringBuilder info = new System.Text.StringBuilder();
        BLL_Tec_Worklist lObj = new BLL_Tec_Worklist();
        BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
        DataTable dtinfo = objInsp.Get_InspInfo(UserId).Tables[0];
        if (dtinfo.Rows.Count > 0)
        {
            info.Append("<table cellpadding='2px' style='font-size:11px;color:#0D3E6E;' >");
            if (File.Exists(Server.MapPath("~/Uploads/CrewImages/") + dtinfo.Rows[0]["PhotoURL"]))
            {
                info.Append("<tr>");
                info.Append("<td  colspan='3'>");
                info.Append("<img src='/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/Uploads/CrewImages/" + dtinfo.Rows[0]["PhotoURL"] + "' style='float:left;' class='CrewImage' />");
                info.Append("</td>");
                info.Append("</tr>");
            }
            info.Append("<tr><td>Inspector Name</td><td>:</td><td>" + dtinfo.Rows[0]["Name"] + "</td></tr>");
            info.Append("<tr><td>From Date</td><td>:</td><td>" + Schedule_Date + "</td></tr>");
            info.Append("<tr><td>To Date</td><td>:</td><td>" + lEndDate + "</td></tr>");
            info.Append("<tr><td>Vessel Name</td><td>:</td><td>" + VESSEL_NAME + "</td></tr>");
            info.Append("</table>");
        }


        return info.ToString();
    }

    [WebMethod]
    public string asyncGet_SurveyCertificateToolTip(int Surv_Details_ID, int Surv_Vessel_ID, int Vessel_ID, int OfficeID, char Type)
    {
        StringBuilder returnStr = new StringBuilder();
        try
        {
            BLL_Tec_Inspection lObj = new BLL_Tec_Inspection();
            DataSet ds = lObj.Get_SurveyCertificateToolTip(Surv_Details_ID, Surv_Vessel_ID, Vessel_ID, OfficeID, Type);
            if (ds != null)
            {
                ds.Tables[0].Columns.Add("DateFormat", typeof(string));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    if (Convert.ToString(ds.Tables[0].Rows[i]["CalculatedExpiryDate"]) != "")
                        ds.Tables[0].Rows[i]["DateFormat"] = UDFLib.ConvertDateToNull(ds.Tables[0].Rows[i]["CalculatedExpiryDate"]).Value.ToString("dd/MMM/yyyy");
            }
            returnStr.Append(UDFLib.DataTableToJsonObj(ds.Tables[0]));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            returnStr.Append("");
        }
        return returnStr.ToString();
    }


    [WebMethod]
    public string[] asncLoadCalendar(string pUserCompanyId, string pStartDate)
    {
        try
        {
            bool flagfirstrow = true;

            DateTime lStartDate = Convert.ToDateTime(pStartDate);
            lStartDate = new DateTime(lStartDate.Year, lStartDate.Month, 1);
            DateTime lEndDate = lStartDate.AddMonths(2);
            lEndDate = new DateTime(lEndDate.Year, lEndDate.Month, DateTime.DaysInMonth(lEndDate.Year, lEndDate.Month));
            try
            {
                List<string> MonthName = new List<string>();
                List<int> MonthDays = new List<int>();
                for (int i = 1; i <= 12; i++)
                {
                    DateTime lDate = new DateTime(lEndDate.Year, i, DateTime.DaysInMonth(lEndDate.Year, i));
                    MonthName.Add(lDate.ToString("MMMM", new CultureInfo("en-GB")));
                    MonthDays.Add(DateTime.DaysInMonth(lEndDate.Year, i));
                }
                DataTable tbDD = new DataTable();
                tbDD.Columns.Add("DateInt", typeof(int));
                tbDD.Columns.Add("DayOfWeek", typeof(string));
                tbDD.Columns.Add("tDate", typeof(DateTime));
                string selectionText = lStartDate.ToString("MMM/yyyy") + " to " + lEndDate.ToString("MMM/yyyy");
                #region data


                BLL_Tec_Inspection lObj = new BLL_Tec_Inspection();
                DataSet ds = lObj.Get_CaledndarData(lStartDate, lEndDate, UDFLib.ConvertIntegerToNull(pUserCompanyId));

                foreach (DataRow itemC in ds.Tables[0].Rows)
                {
                    if (itemC["ActualDate"].ToString() == "")
                    {
                        itemC["ActualInspectorId"] = itemC["InspectorId"];
                        itemC["ActualDate"] = UDFLib.ConvertDateToNull(itemC["Schedule_Date"]).Value.AddDays(UDFLib.ConvertToInteger(itemC["DurJobs"]) - 1);
                    }
                }

                #endregion

                int? UserCompanyId = UDFLib.ConvertIntegerToNull(pUserCompanyId);




                DateTime tempdate = lStartDate;
                while (true)
                {

                    tbDD.Rows.Add(tempdate.Day, tempdate.DayOfWeek.ToString().ToCharArray()[0], tempdate);
                    if (tempdate == lEndDate)
                        break;
                    tempdate = tempdate.AddDays(1);
                }
                Table InspTable = new Table();
                InspTable.CellPadding = 0;
                InspTable.CellSpacing = 0;
                Table InspTable0 = new Table();
                InspTable0.CellPadding = 0;
                InspTable0.CellSpacing = 0;
                TableRow dateRow;
                TableRow dayRow;
                TableRow VesslAttentRow = null;
                TableCell dateCell;
                TableCell dayCell;
                TableRow monthRow = new TableRow();
                monthRow.TableSection = TableRowSection.TableHeader;

                TableCell monthCell;
                TableCell tcSuperintendents = new TableCell();

                tcSuperintendents.CssClass = "SupStyle";
                tcSuperintendents.RowSpan = 3;
                monthRow.Cells.Add(tcSuperintendents);


                DateTime l = lStartDate;

                while (true)
                {
                    bool tobreak = false; ;
                    if (l.Month == lEndDate.Month)
                    {
                        tobreak = true;
                    }
                    monthCell = new TableCell();
                    monthCell.Text = MonthName[l.Month - 1].ToString() + " " + l.Year;
                    monthCell.ColumnSpan = DateTime.DaysInMonth(l.Year, l.Month);
                    monthCell.CssClass = "MonthStyle";
                    monthRow.Cells.Add(monthCell);
                    l = l.AddMonths(1);
                    if (tobreak)
                        break;
                }




                dateRow = new TableRow();
                dayRow = new TableRow();

                dateRow.TableSection = TableRowSection.TableHeader;
                dayRow.TableSection = TableRowSection.TableHeader;
                foreach (DataRow dataRow in tbDD.Rows)
                {

                    dateCell = new TableCell();
                    dateCell.CssClass = "DateStyle";
                    dateCell.Text = dataRow["DateInt"].ToString();
                    dateCell.Attributes.Add("rel", UDFLib.ConvertDateToNull(dataRow["tDate"]).Value.ToString("dd/MMM/yyyy"));

                    dayCell = new TableCell();
                    dayCell.Text = dataRow["DayOfWeek"].ToString();
                    dayCell.CssClass = "DayStyle";
                    dayCell.Attributes.Add("rel", UDFLib.ConvertDateToNull(dataRow["tDate"]).Value.ToString("dd/MMM/yyyy"));

                    dateRow.Cells.Add(dateCell);
                    dayRow.Cells.Add(dayCell);

                }

                InspTable0.Rows.Add(monthRow);
                InspTable0.Rows.Add(dateRow);
                InspTable0.Rows.Add(dayRow);


                if (InspColor == null)
                {
                    InspColor = new Dictionary<int, Color>();
                }

                InspColor = new Dictionary<int, Color>();
                BLL_Infra_VesselLib lObjVessel = new BLL_Infra_VesselLib();
                DataTable dtVessel = lObjVessel.Get_VesselList(0, 0, 0, "", UDFLib.ConvertToInteger(UserCompanyId));

                foreach (DataRow item in dtVessel.Rows)
                {
                    if (UserCompanyId != null)
                        if (item["Vessel_Manager"].ToString() != UserCompanyId.ToString())
                            continue;
                    TableCell VesselCell = new TableCell();


                    List<DateTime> lDateList = new List<DateTime>();
                    DataTable dx = null;
                    DataTable dy = null;
                    DataRow[] dtCount = ds.Tables[0].Select(" Vessel_Id=" + item["Vessel_Id"].ToString());
                    if (dtCount.Length > 0)
                    {
                        dx = dtCount.CopyToDataTable().DefaultView.ToTable(true, "ActualInspectorId");

                    }
                    if (dtCount.Length > 0)
                    {
                        dy = dtCount.CopyToDataTable().DefaultView.ToTable(true, "InspectorId");
                    }

                    List<string> lb = new List<string>();
                    if (dy != null)
                        foreach (DataRow dyr in dy.Rows)
                        {
                            if (!lb.Contains(dyr[0].ToString()))
                            {
                                lb.Add(dyr[0].ToString());
                            }
                        }
                    if (dx != null)
                    {
                        foreach (DataRow dxr in dx.Rows)
                        {
                            if (!lb.Contains(dxr[0].ToString()))
                            {
                                lb.Add(dxr[0].ToString());
                            }
                        }

                        dx.Rows.Clear();
                    }

                    foreach (string lbr in lb)
                    {
                        dx.Rows.Add(lbr);
                    }

                    ds.Tables[1].DefaultView.RowFilter = "";
                    ds.Tables[1].DefaultView.RowFilter = "Vessel_Id=" + item["Vessel_Id"];

                    if (dtCount.Length > 0 || ds.Tables[1].DefaultView.Count > 0)
                    {
                        Dictionary<string, TableRow> lTRow = new Dictionary<string, TableRow>();

                        List<TableRow> lRowsTable = new List<TableRow>();

                        int TotalNoofInspector = 0;
                        if (dx != null)
                            TotalNoofInspector = dx.Rows.Count;
                        int LoopCnt = 0, CellCount = 0;

                        if (dx != null)
                        {
                            foreach (DataRow insprow in dx.Rows)
                            {
                                TableRow VesslAttentRowD = new TableRow();
                                foreach (DataRow dataRow in tbDD.Rows)
                                {

                                    TableCell VesselAttCell = new TableCell();
                                    VesselAttCell.Text = "";
                                    VesselAttCell.CssClass = "NormStyle";
                                    VesslAttentRowD.Cells.Add(VesselAttCell);
                                }

                                lRowsTable.Add(VesslAttentRowD);

                                CellCount = 0;

                                foreach (DataRow dataRow in tbDD.Rows)
                                {
                                    DataRow[] dtRows = ds.Tables[0].Select("Schedule_Date <= '" + dataRow["tDate"].ToString() + "' and ActualDate >='" + dataRow["tDate"].ToString() + "' and Vessel_Id=" + item["Vessel_Id"] + "  and Inspctor = " + insprow["ActualInspectorId"].ToString());
                                    if (dtRows.Length > 0)
                                    {
                                        int UserId = UDFLib.ConvertToInteger(dtRows[0]["Inspctor"]);

                                        lRowsTable[LoopCnt].Cells[CellCount].BackColor = System.Drawing.ColorTranslator.FromHtml("#" + dtRows[0]["ColorCodeS"]);// InspColor[UDFLib.ConvertToInteger(insprow["ActualInspectorId"])];
                                        lRowsTable[LoopCnt].Cells[CellCount].Text = @"<div rel='" + UDFLib.ConvertDateToNull(dataRow["tDate"]).Value.ToString("dd/MMM/yyyy") + "' onclick='Get_InspInfo(&#34;" + UserId + "&#34;,&#34;" + UDFLib.ConvertDateToNull(dtRows[0]["Schedule_Date"]).Value.ToString("dd/MMM/yyyy") + "&#34;,&#34;" + dtRows[0]["VESSEL_NAME"].ToString() + "&#34;,&#34;" + UDFLib.ConvertDateToNull(dtRows[0]["ActualDate"]).Value.ToString("dd/MMM/yyyy") + "&#34;,event,this)' style='cursor:pointer'>&nbsp;&nbsp;</div>";
                                        lRowsTable[LoopCnt].Cells[CellCount].ForeColor = System.Drawing.ColorTranslator.FromHtml("#" + dtRows[0]["ColorCodeS"]);
                                    }
                                    CellCount++;
                                }
                                LoopCnt++;
                            }
                        }


                        /// Get Maximum no Rows count from Survey Certificate Expiry list
                        int MaxRowCount = 1;
                        int MaxExpiry = 0, MaxReminder = 0;

                        foreach (DataRow dataRow in tbDD.Rows)
                        {
                            ds.Tables[1].DefaultView.RowFilter = "";
                            if (ds.Tables[1].DefaultView.Count > 0)
                            {
                                DataRow[] dtSurveyExpire = ds.Tables[1].Select("CalculatedExpiryDate <= '" + dataRow["tDate"].ToString() + "' and CalculatedExpiryDate >='" + dataRow["tDate"].ToString() + "' and Vessel_Id=" + item["Vessel_Id"] + " and Type='E'");

                                DataRow[] dtSurveyReminder = ds.Tables[1].Select("FollowupReminderDt <= '" + dataRow["tDate"].ToString() + "' and FollowupReminderDt >='" + dataRow["tDate"].ToString() + "' and Vessel_Id=" + item["Vessel_Id"] + " and Type='R'");

                                if (dtSurveyExpire.Count() > 0 || dtSurveyReminder.Count() > 0)
                                {
                                    if ((dtSurveyExpire.Count() + dtSurveyReminder.Count()) > MaxRowCount)
                                        MaxRowCount = dtSurveyExpire.Count() + dtSurveyReminder.Count();
                                }
                            }
                        }

                        ///Bind Empty rows for survey certificate expiry and reminder
                        for (int i = 0; i < MaxRowCount; i++)
                        {
                            TableRow SurveyCertificateRowD = new TableRow();
                            foreach (DataRow dataRow in tbDD.Rows)
                            {
                                TableCell VesselAttCell = new TableCell();
                                VesselAttCell.Text = "";
                                VesselAttCell.CssClass = "NormStyle";
                                SurveyCertificateRowD.Cells.Add(VesselAttCell);
                            }
                            lRowsTable.Add(SurveyCertificateRowD);
                        }

                        int count = 0;
                        int innerloop = LoopCnt;
                        foreach (DataRow dataRow in tbDD.Rows)
                        {
                            ds.Tables[1].DefaultView.RowFilter = "";
                            ds.Tables[1].DefaultView.RowFilter = "Type='E'";

                            if (ds.Tables[1].DefaultView.Count > 0)
                            {
                                DataRow[] dtSurveyExpire = ds.Tables[1].Select("CalculatedExpiryDate <= '" + dataRow["tDate"].ToString() + "' and CalculatedExpiryDate >='" + dataRow["tDate"].ToString() + "' and Vessel_Id=" + item["Vessel_Id"] + " and Type='E'");

                                if (dtSurveyExpire.Count() > 0)
                                {
                                    for (int j = 0; j < dtSurveyExpire.Count(); j++)
                                    {
                                        if (j == 0)
                                            innerloop = LoopCnt;
                                        for (int i = innerloop; i < MaxRowCount + LoopCnt; i++)
                                        {
                                            if (lRowsTable[i].Cells[count].Text.Length == 0)
                                            {
                                                string Surv_Details_ID = Convert.ToString(dtSurveyExpire[j]["Surv_Details_ID"]);
                                                string Surv_Vessel_ID = Convert.ToString(dtSurveyExpire[j]["Surv_Vessel_ID"]);
                                                string Vessel_ID = Convert.ToString(dtSurveyExpire[j]["Vessel_ID"]);
                                                string OfficeID = Convert.ToString(dtSurveyExpire[j]["OfficeID"]);

                                                lRowsTable[i].Cells[count].Style.Add("text-align", "center");
                                                lRowsTable[i].Cells[count].Text = @"<div  rel='" + UDFLib.ConvertDateToNull(dataRow["tDate"].ToString()).Value.ToString("dd/MMM/yyyy") + "' style='cursor:pointer' onclick='SurveyCertificateExpiry(&#34;" + Surv_Details_ID + "&#34;,&#34;" + Surv_Vessel_ID + "&#34;,&#34;" + Vessel_ID + "&#34;,&#34;" + OfficeID + "&#34;,event,this);'><img title='Survey Certificate' src='../../Images/RedIconInspection.png' alt='S' /></div>";
                                                lRowsTable[i].Cells[count].CssClass = "SurveyCertificateExpiry";
                                                innerloop++;
                                                break;
                                            }
                                            else
                                                innerloop++;
                                        }
                                    }
                                }
                            }
                            count++;
                        }

                        count = 0;
                        int Reminderinnerloop = LoopCnt;
                        foreach (DataRow dataRow in tbDD.Rows)
                        {
                            ds.Tables[1].DefaultView.RowFilter = "";
                            ds.Tables[1].DefaultView.RowFilter = "Type='R'";

                            if (ds.Tables[1].DefaultView.Count > 0)
                            {
                                DataRow[] dtSurveyReminder = ds.Tables[1].Select("FollowupReminderDt <= '" + dataRow["tDate"].ToString() + "' and FollowupReminderDt >='" + dataRow["tDate"].ToString() + "' and Vessel_Id=" + item["Vessel_Id"] + " and Type='R'");

                                if (dtSurveyReminder.Count() > 0)
                                {
                                    for (int j = 0; j < dtSurveyReminder.Count(); j++)
                                    {
                                        if (j == 0)
                                            Reminderinnerloop = LoopCnt;
                                        for (int i = Reminderinnerloop; i < MaxRowCount + LoopCnt; i++)
                                        {
                                            if (lRowsTable[i].Cells[count].Text.Length == 0)
                                            {
                                                string Surv_Details_ID = Convert.ToString(dtSurveyReminder[j]["Surv_Details_ID"]);
                                                string Surv_Vessel_ID = Convert.ToString(dtSurveyReminder[j]["Surv_Vessel_ID"]);
                                                string Vessel_ID = Convert.ToString(dtSurveyReminder[j]["Vessel_ID"]);
                                                string OfficeID = Convert.ToString(dtSurveyReminder[j]["OfficeID"]);

                                                lRowsTable[i].Cells[count].Style.Add("text-align", "center");
                                                lRowsTable[i].Cells[count].Text = @"<div rel='" + UDFLib.ConvertDateToNull(dataRow["tDate"].ToString()).Value.ToString("dd/MMM/yyyy") + "' style='cursor:pointer' onclick='SurveyCertificateReminder(&#34;" + Surv_Details_ID + "&#34;,&#34;" + Surv_Vessel_ID + "&#34;,&#34;" + Vessel_ID + "&#34;,&#34;" + OfficeID + "&#34;,event,this);'><img title='Survey Certificate' src='../../Images/YellowIconInspection.png' alt='S' /></div>";
                                                lRowsTable[i].Cells[count].CssClass = "SurveyCertificateReminder";
                                                Reminderinnerloop++;
                                                break;
                                            }
                                            else
                                                Reminderinnerloop++;
                                        }
                                    }
                                }
                            }
                            count++;
                        }

                        LoopCnt = LoopCnt + MaxRowCount;

                        int ik = 0;
                        foreach (TableRow itemTableRow in lRowsTable)
                        {
                            if (ik == 0)
                            {
                                VesslAttentRow = new TableRow();
                                VesselCell = new TableCell();
                                VesselCell.Text = item["Vessel_Name"].ToString();
                                VesselCell.RowSpan = lRowsTable.Count;
                                VesselCell.CssClass = "VesselStyle";
                                VesslAttentRow.Cells.Add(VesselCell);
                                VesselCell.Style.Add("height", (15 * lRowsTable.Count) + "px");

                                foreach (TableCell itemDetails in itemTableRow.Cells)
                                {
                                    TableCell dtC = new TableCell();

                                    dtC.ToolTip = itemDetails.ToolTip;
                                    dtC.Text = itemDetails.Text;
                                    dtC.BackColor = itemDetails.BackColor;
                                    dtC.ForeColor = itemDetails.ForeColor;
                                    dtC.CssClass = "NormStyle";
                                    VesslAttentRow.Cells.Add(dtC);

                                }
                                InspTable.Rows.Add(VesslAttentRow);
                            }
                            else
                            {

                                InspTable.Rows.Add(itemTableRow);
                            }

                            ik++;
                        }

                    }
                    else
                    {
                        VesselCell.Text = item["Vessel_Name"].ToString();
                        VesslAttentRow = new TableRow();
                        VesselCell.CssClass = "VesselStyle";
                        VesslAttentRow.Cells.Add(VesselCell);

                        foreach (DataRow dataRow in tbDD.Rows)
                        {
                            TableCell VesselAttCell = new TableCell();
                            if (flagfirstrow)
                                VesselAttCell.Text = "";
                            VesselAttCell.CssClass = "NormStyle";
                            VesslAttentRow.Cells.Add(VesselAttCell);
                        }
                        InspTable.Rows.Add(VesslAttentRow);
                        flagfirstrow = false;
                    }
                }

                string newt = "";
                string newt0 = "";

                InspTable.ID = "t01";
                using (StringWriter sw = new StringWriter())
                {

                    InspTable.RenderControl(new HtmlTextWriter(sw));
                    newt = sw.ToString();
                }
                InspTable0.ID = "t00";
                using (StringWriter swq = new StringWriter())
                {

                    InspTable0.RenderControl(new HtmlTextWriter(swq));
                    newt0 = swq.ToString();
                }


                String[] result = { newt, selectionText, newt0 };
                return result;
            }
            catch (Exception ex)
            {
                return new string[] { "", "", ex.Message };

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            return new string[] { "", "", ex.Message };
        }

    }
    #endregion


    [WebMethod]
    public string[] asncLoadCalendarBySupt(string pUserCompanyId, string pStartDate)
    {
        bool flagfirstrow = true;
        BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
        DateTime lStartDate = Convert.ToDateTime(pStartDate);
        lStartDate = new DateTime(lStartDate.Year, lStartDate.Month, 1);
        DateTime lEndDate = lStartDate.AddMonths(0);

        lEndDate = new DateTime(lEndDate.Year, lEndDate.Month, DateTime.DaysInMonth(lEndDate.Year, lEndDate.Month));
        try
        {
            List<string> MonthName = new List<string>();
            List<int> MonthDays = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                DateTime lDate = new DateTime(lEndDate.Year, i, DateTime.DaysInMonth(lEndDate.Year, i));
                MonthName.Add(lDate.ToString("MMMM", new CultureInfo("en-GB")));
                MonthDays.Add(DateTime.DaysInMonth(lEndDate.Year, i));
            }
            DataTable tbDD = new DataTable();
            tbDD.Columns.Add("DateInt", typeof(int));
            tbDD.Columns.Add("DayOfWeek", typeof(string));
            tbDD.Columns.Add("tDate", typeof(DateTime));

            DataTable tbDDPrev = new DataTable();
            tbDDPrev.Columns.Add("DateInt", typeof(int));
            tbDDPrev.Columns.Add("DayOfWeek", typeof(string));
            tbDDPrev.Columns.Add("tDate", typeof(DateTime));

            string selectionText = lStartDate.ToString("MMM/yyyy") + " to " + lEndDate.ToString("MMM/yyyy");
            #region data


            BLL_Tec_Inspection lObj = new BLL_Tec_Inspection();
            DataSet ds = lObj.Get_CaledndarDataBySupt(lStartDate, lEndDate, UDFLib.ConvertIntegerToNull(pUserCompanyId));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (UDFLib.ConvertDateToNull(ds.Tables[0].Rows[i]["Schedule_Date"].ToString()).Value < lStartDate)
                {
                    ds.Tables[0].Rows[i]["Schedule_Date"] = lStartDate;


                }
            }
            #endregion
            //if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow itemC in ds.Tables[0].Rows)
                {
                    if (itemC["ActualDate"].ToString() == "")
                    {
                        itemC["ActualInspectorId"] = itemC["InspectorId"];
                        itemC["ActualDate"] = UDFLib.ConvertDateToNull(itemC["Schedule_Date"]).Value.AddDays(UDFLib.ConvertToInteger(itemC["DurJobs"]) - 1);
                    }
                }



                int? UserCompanyId = UDFLib.ConvertIntegerToNull(pUserCompanyId);




                DateTime tempdate = lStartDate;
                //   DateTime PrevTempDate = lStartDate.AddMonths(-1);
                //  DateTime PrevlEndDate = new DateTime(PrevTempDate.Year, PrevTempDate.Month, DateTime.DaysInMonth(PrevTempDate.Year, PrevTempDate.Month)); 
                while (true)
                {

                    tbDD.Rows.Add(tempdate.Day, tempdate.DayOfWeek.ToString().ToCharArray()[0], tempdate);
                    if (tempdate == lEndDate)
                        break;
                    tempdate = tempdate.AddDays(1);
                }
                //while (true)
                //{

                //    tbDDPrev.Rows.Add(PrevTempDate.Day, PrevTempDate.DayOfWeek.ToString().ToCharArray()[0], PrevTempDate);
                //    if (PrevTempDate == PrevlEndDate)
                //        break;
                //    PrevTempDate = PrevTempDate.AddDays(1);
                //}

                Table InspTable = new Table();
                InspTable.CellPadding = 0;
                InspTable.CellSpacing = 0;
                Table InspTable0 = new Table();
                InspTable0.CellPadding = 0;
                InspTable0.CellSpacing = 0;
                TableRow dateRow;
                TableRow dayRow;
                TableRow VesslAttentRow = null;
                TableCell dateCell;
                TableCell dayCell;
                TableRow monthRow = new TableRow();
                monthRow.TableSection = TableRowSection.TableHeader;

                TableCell monthCell;
                TableCell tcSuperintendents = new TableCell();



                tcSuperintendents.Text = "Inspectors";
                tcSuperintendents.CssClass = "SupStyle";
                tcSuperintendents.RowSpan = 3;
                monthRow.Cells.Add(tcSuperintendents);




                DateTime l = lStartDate;

                while (true)
                {
                    bool tobreak = false; ;
                    if (l.Month == lEndDate.Month)
                    {
                        tobreak = true;
                    }
                    monthCell = new TableCell();
                    monthCell.Text = MonthName[l.Month - 1].ToString() + " " + l.Year;
                    monthCell.ColumnSpan = DateTime.DaysInMonth(l.Year, l.Month);
                    monthCell.CssClass = "MonthStyle";
                    monthRow.Cells.Add(monthCell);
                    l = l.AddMonths(1);
                    if (tobreak)
                        break;
                }






                dateRow = new TableRow();
                dayRow = new TableRow();

                dateRow.TableSection = TableRowSection.TableHeader;
                dayRow.TableSection = TableRowSection.TableHeader;
                foreach (DataRow dataRow in tbDD.Rows)
                {

                    dateCell = new TableCell();
                    dateCell.CssClass = "DateStyle";
                    dateCell.Text = dataRow["DateInt"].ToString();
                    dayCell = new TableCell();
                    dayCell.Text = dataRow["DayOfWeek"].ToString();
                    dayCell.CssClass = "DayStyle";
                    dateRow.Cells.Add(dateCell);
                    dayRow.Cells.Add(dayCell);

                }

                InspTable0.Rows.Add(monthRow);
                InspTable0.Rows.Add(dateRow);
                InspTable0.Rows.Add(dayRow);





                if (InspColor == null)
                {
                    InspColor = new Dictionary<int, Color>();
                }
                //else
                //{
                //    InspColor = (Dictionary<int, Color>)Session["InspColor"];
                //}


                InspColor = new Dictionary<int, Color>();
                // BLL_Infra_VesselLib lObjVessel = new BLL_Infra_VesselLib();
                BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
                //DataTable dtVessel = lObjVessel.Get_VesselList(0, 0, 0, "", 0);
                DataTable dtUsers = objUser.INSP_Get_InspectorList();

                //  for (int i = 0; i < 10; i++)
                //  {
                foreach (DataRow item in dtUsers.Rows)
                {
                    if (UserCompanyId != null)
                        if (item["Vessel_Manager"].ToString() != UserCompanyId.ToString())
                            continue;
                    TableCell VesselCell = new TableCell();


                    List<DateTime> lDateList = new List<DateTime>();
                    DataTable dx = null;
                    DataTable dy = null;
                    DataRow[] dtCount = ds.Tables[0].Select(" Inspctor=" + item["InspectorID"].ToString());

                    if (dtCount.Length > 0)
                    {
                        string[] ColumnNames = new string[1];
                        //ColumnNames[0] = "InspectionDetailId";
                        ColumnNames[0] = "VESSEL_ID";
                        //    ColumnNames[1] = "Schedule_date";
                        dx = dtCount.CopyToDataTable().DefaultView.ToTable(true, ColumnNames);

                    }


                    List<string[]> lb = new List<string[]>();

                    if (dx != null)
                    {
                        foreach (DataRow dxr in dx.Rows)
                        {
                            string[] Columnval = new string[1];
                            Columnval[0] = dxr[0].ToString();
                            // Columnval[1] = dxr[1].ToString();
                            if (!lb.Contains(Columnval))
                            {
                                lb.Add(Columnval);
                            }
                        }

                        dx.Rows.Clear();
                    }

                    foreach (string[] lbr in lb)
                    {
                        dx.Rows.Add(lbr);
                    }

                    bool flag = true;

                    if (dtCount.Length > 0)
                    {
                        Dictionary<string, TableRow> lTRow = new Dictionary<string, TableRow>();


                        TableRow VesslAttentRowD = new TableRow(); ;
                        foreach (DataRow dataRow in tbDD.Rows)
                        {

                            TableCell VesselAttCell = new TableCell();
                            VesselAttCell.Text = "";
                            VesselAttCell.CssClass = "NormStyle";
                            VesslAttentRowD.Cells.Add(VesselAttCell);
                        }

                        List<TableRow> lRowsTable = new List<TableRow>();
                        bool fmk = true;
                        lRowsTable.Add(VesslAttentRowD);
                        int st = 0;
                        foreach (DataRow insprow in dx.Rows)
                        {
                            if (!InspColor.ContainsKey(UDFLib.ConvertToInteger(item["InspectorID"].ToString())))
                            {
                                while (true)
                                {
                                    Random randomGen = new Random();
                                    KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
                                    KnownColor randomColorName = names[randomGen.Next(names.Length)];
                                    Color randomColor = Color.FromKnownColor(randomColorName);
                                    bool flagcolr = true;
                                    foreach (Color itemcolor in InspColor.Values)
                                    {
                                        if (randomColor == itemcolor || randomColor == Color.White || randomColor.ToString().Contains("white") || randomColor.A < 5 || randomColor.R > 252 || randomColor.G > 252 || randomColor.B > 252 || randomColor.ToString().Contains("highlight"))
                                            flagcolr = false;

                                    }
                                    if (flagcolr)
                                    {
                                        InspColor[UDFLib.ConvertToInteger(item["InspectorID"].ToString())] = randomColor;
                                        break;
                                    }


                                }


                            }
                            int xk = 0;
                            Dictionary<int, bool> flist = new Dictionary<int, bool>();
                            foreach (DataRow dataRow in tbDD.Rows)
                            {

                                DataRow[] dtRows = ds.Tables[0].Select("Schedule_Date = '" + dataRow["tDate"].ToString() + "'   and VESSEL_ID=" + insprow["VESSEL_ID"] + "and ActualInspectorId=" + item["InspectorID"].ToString());
                                int iko = 0;
                                if (dtRows.Length > 0 && dtRows.Length <= 1)
                                {


                                    foreach (TableRow itemlRowsTable in lRowsTable)
                                    {
                                        if (itemlRowsTable.Cells[xk].Text.Trim().Length > 0 || itemlRowsTable.Cells[xk].Visible == false)
                                        {

                                            flist[iko] = false;

                                            fmk = false;


                                        }

                                        else
                                        {
                                            if (!flist.ContainsKey(iko))
                                            {
                                                flist[iko] = true;
                                            }
                                        }
                                        iko++;
                                    }


                                }

                                xk++;
                            }
                            try
                            {
                                if (flist.Values.Contains(true))
                                {
                                    int trucnt = 0;
                                    foreach (var itemx in flist.Values)
                                    {
                                        if (itemx)
                                        {
                                            break;
                                        }
                                        trucnt++;
                                    }

                                    int nk = 0;
                                    int Dur = 1;






                                    nk = 0;
                                    foreach (DataRow dataRow in tbDD.Rows)
                                    {
                                        Dur = 1;
                                        DataRow[] dtRows = ds.Tables[0].Select("Schedule_Date = '" + dataRow["tDate"].ToString() + "'  and VESSEL_ID=" + insprow["VESSEL_ID"] + " and ActualInspectorId=" + item["InspectorID"]);
                                        if (dtRows.Length > 0)
                                        {



                                            int UserId = UDFLib.ConvertToInteger(dtRows[0]["Inspctor"]);


                                            lRowsTable[trucnt].Cells[nk].BackColor = System.Drawing.ColorTranslator.FromHtml("#" + dtRows[0]["ColorCodeS"]);

                                            Dur = UDFLib.ConvertToInteger(dtRows[0]["DurJobs"].ToString());

                                            DataTable dtSch = objInsp.TEC_Get_SupInspectionDetailsByDate(UDFLib.ConvertDateToNull(dtRows[0]["TempScheduleDate"]).Value, Convert.ToInt32(UserId), UDFLib.ConvertToInteger(pUserCompanyId));

                                            if (dtSch.Rows.Count > 0)
                                            {

                                                if (dtSch.Rows[0]["StartDate"].ToString() != dtSch.Rows[0]["EndDate"].ToString())
                                                {
                                                    lRowsTable[trucnt].Cells[nk].Text = dtSch.Rows[0]["LocName"].ToString() + "<br/>" + Convert.ToDateTime(dtSch.Rows[0]["StartDate"].ToString()).Date.ToString("dd/MM") + " - " + Convert.ToDateTime(dtSch.Rows[0]["EndDate"].ToString()).Date.ToString("dd/MM");
                                                }
                                                else
                                                {
                                                    lRowsTable[trucnt].Cells[nk].Text = dtSch.Rows[0]["LocName"].ToString() + "<br/>" + Convert.ToDateTime(dtSch.Rows[0]["StartDate"].ToString()).Date.ToString("dd/MM");
                                                }
                                            }




                                            int ColSpan = 0, endDay = 0;
                                            double Diff = 0;
                                            Diff = (UDFLib.ConvertDateToNull(dataRow["tDate"]).Value - UDFLib.ConvertDateToNull(dtRows[0]["TempScheduleDate"]).Value).TotalDays;
                                            if ((nk + Dur) > tbDD.Rows.Count)
                                            {
                                                endDay = (UDFLib.ConvertToInteger(Diff) + nk + 1);
                                                Dur = tbDD.Rows.Count - (nk);
                                            }
                                            else
                                            {
                                                endDay = (Dur + nk);
                                                Dur = Dur - UDFLib.ConvertToInteger(Diff);
                                            }

                                            lRowsTable[trucnt].Cells[nk].ColumnSpan = Dur;
                                            for (int i = nk + 1; i < endDay; i++)
                                            {
                                                lRowsTable[trucnt].Cells[i].Visible = false;
                                            }



                                        }
                                        nk++;

                                    }





                                }
                                else
                                {
                                    TableRow VesslAttentRowNew = new TableRow(); ;
                                    foreach (DataRow dataRow in tbDD.Rows)
                                    {

                                        TableCell VesselAttCell = new TableCell();
                                        VesselAttCell.CssClass = "NormStyle";
                                        VesselAttCell.Text = "";

                                        VesslAttentRowNew.Cells.Add(VesselAttCell);
                                    }
                                    int nk = 0;
                                    int Dur = 1;
                                    foreach (DataRow dataRow in tbDD.Rows)
                                    {
                                        Dur = 1;
                                        DataRow[] dtRows = ds.Tables[0].Select("Schedule_Date = '" + dataRow["tDate"].ToString() + "' and VESSEL_ID=" + insprow["VESSEL_ID"] + "  and ActualInspectorId=" + item["InspectorID"]);
                                        if (dtRows.Length > 0)
                                        {



                                            int UserId1 = UDFLib.ConvertToInteger(dtRows[0]["Inspctor"]);

                                            VesslAttentRowNew.Cells[nk].BackColor = System.Drawing.ColorTranslator.FromHtml("#" + dtRows[0]["ColorCodeS"]);

                                            VesslAttentRowNew.CssClass = "NormStyle";
                                            DataTable dtSch = objInsp.TEC_Get_SupInspectionDetailsByDate(UDFLib.ConvertDateToNull(dtRows[0]["TempScheduleDate"]).Value, Convert.ToInt32(UserId1), UDFLib.ConvertToInteger(pUserCompanyId));

                                            if (dtSch.Rows.Count > 0)
                                            {

                                                if (dtSch.Rows[0]["StartDate"].ToString() != dtSch.Rows[0]["EndDate"].ToString())
                                                {
                                                    VesslAttentRowNew.Cells[nk].Text = dtSch.Rows[0]["LocName"].ToString() + "<br/>" + Convert.ToDateTime(dtSch.Rows[0]["StartDate"].ToString()).Date.ToString("dd/MM") + " - " + Convert.ToDateTime(dtSch.Rows[0]["EndDate"].ToString()).Date.ToString("dd/MM");

                                                }
                                                else
                                                {
                                                    VesslAttentRowNew.Cells[nk].Text = dtSch.Rows[0]["LocName"].ToString() + "<br/>" + Convert.ToDateTime(dtSch.Rows[0]["StartDate"].ToString()).Date.ToString("dd/MM");
                                                }
                                            }

                                            Dur = UDFLib.ConvertToInteger(dtRows[0]["DurJobs"].ToString());
                                            int ColSpan = 0, endDay = 0;
                                            double Diff = 0;
                                            Diff = (UDFLib.ConvertDateToNull(dataRow["tDate"]).Value - UDFLib.ConvertDateToNull(dtRows[0]["TempScheduleDate"]).Value).TotalDays;
                                            if ((nk + Dur) > tbDD.Rows.Count)
                                            {
                                                // Diff = (nk + Dur) - tbDD.Rows.Count;
                                                endDay = (UDFLib.ConvertToInteger(Diff) + nk + 1);
                                                //Dur = Dur - Diff;

                                                Dur = tbDD.Rows.Count - (nk);
                                            }
                                            else
                                            {
                                                endDay = (Dur + nk);
                                                Dur = Dur - UDFLib.ConvertToInteger(Diff);
                                            }

                                            VesslAttentRowNew.Cells[nk].ColumnSpan = Dur;

                                            for (int i = nk + 1; i < endDay; i++)
                                            {

                                                VesslAttentRowNew.Cells[i].Visible = false;

                                            }

                                        }
                                        nk++;
                                        // nk += Dur;
                                    }
                                    lRowsTable.Add(VesslAttentRowNew);
                                }
                            }
                            catch (Exception ex)
                            {
                                UDFLib.WriteExceptionLog(ex);
                            }

                        }

                        int ik = 0;
                        foreach (TableRow itemTableRow in lRowsTable)
                        {
                            if (ik == 0)
                            {
                                VesslAttentRow = new TableRow();
                                VesselCell = new TableCell();
                                VesselCell.Text = item["Inspector"].ToString();

                                VesselCell.RowSpan = lRowsTable.Count;
                                VesselCell.CssClass = "VesselStyle";
                                VesslAttentRow.Cells.Add(VesselCell);
                                VesselCell.Style.Add("height", (15 * lRowsTable.Count) + "px");
                                int cellskip = 0;
                                foreach (TableCell itemDetails in itemTableRow.Cells)
                                {

                                    TableCell dtC = new TableCell();

                                    dtC.ToolTip = itemDetails.ToolTip;
                                    dtC.Text = itemDetails.Text;
                                    dtC.BackColor = itemDetails.BackColor;
                                    dtC.ForeColor = itemDetails.ForeColor;
                                    dtC.ColumnSpan = itemDetails.ColumnSpan;

                                    dtC.CssClass = "NormStyle";
                                    if (dtC.ColumnSpan > 0)
                                    {
                                        cellskip = 0;
                                    }
                                    if (cellskip <= 0)
                                    {
                                        cellskip = dtC.ColumnSpan - 1;
                                        if (dtC.Visible == true)
                                        {
                                            VesslAttentRow.Cells.Add(dtC);
                                        }
                                    }
                                    else
                                    {
                                        cellskip--;
                                    }



                                }
                                InspTable.Rows.Add(VesslAttentRow);
                            }
                            else
                            {

                                InspTable.Rows.Add(itemTableRow);
                            }

                            ik++;
                        }

                    }
                    else
                    {
                        VesselCell.Text = item["Inspector"].ToString();
                        VesslAttentRow = new TableRow();
                        VesselCell.CssClass = "VesselStyle";
                        VesslAttentRow.Cells.Add(VesselCell);

                        foreach (DataRow dataRow in tbDD.Rows)
                        {
                            TableCell VesselAttCell = new TableCell();
                            if (flagfirstrow)
                                VesselAttCell.Text = "";
                            VesselAttCell.CssClass = "NormStyle";
                            VesslAttentRow.Cells.Add(VesselAttCell);
                        }
                        InspTable.Rows.Add(VesslAttentRow);
                        flagfirstrow = false;
                    }


                }
                //    }





                string newt = "";
                string newt0 = "";

                InspTable.ID = "t01";
                using (StringWriter sw = new StringWriter())
                {

                    InspTable.RenderControl(new HtmlTextWriter(sw));
                    newt = sw.ToString();
                }
                InspTable0.ID = "t00";
                using (StringWriter swq = new StringWriter())
                {

                    InspTable0.RenderControl(new HtmlTextWriter(swq));
                    newt0 = swq.ToString();
                }


                String[] result = { newt, selectionText, newt0 };
                return result;
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            return new string[] { "", "", ex.Message };

        }

        String[] res = { "", "", "" };
        return res;


    }

    public static string ReplaceSpecialCharacter(string str)
    {
        //return str.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;").Replace("\"", "&quot;");
        string ret = str.Replace(@"\", @"\\");
        return ret;
    }

}
