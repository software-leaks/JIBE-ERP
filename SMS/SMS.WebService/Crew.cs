using System;
using System.Web;
using System.Data;
using System.Web.Services;
using SMS.Data.PortageBill;
using SMS.Data.Crew;
using SMS.Business.Crew;
using System.Collections.Generic;
using SMS.Business.LMS;
using SMS.Properties;
using System.Text;
using System.Linq;
using SMS.Business.FAQ;
using SMS.Business.Infrastructure;
using System.Configuration;

public partial class JibeWebService
{
    [WebMethod]
    public int Del_Interview(int InterviewID, int CrewID, int Deleted_By)
    {
        return DAL_Crew_Interview.Delete_CrewInterview_DL(InterviewID, CrewID, Deleted_By);
    }
    [WebMethod]
    public int Del_BankAccount(int ID, int CrewID, int Deleted_By)
    {
        return DAL_PortageBill.ACC_Del_BankAccounts_DL(ID, CrewID, Deleted_By);
    }
    [WebMethod]
    public int Del_CrewDependent(int ID, int Deleted_By)
    {
        DAL_Crew_CrewDetails objCrew = new DAL_Crew_CrewDetails();
        return objCrew.DEL_Crew_DependentDetails_DL(ID, Deleted_By);
    }
    [WebMethod]
    public int Del_CrewVoyage(int VoyID, int Deleted_By)
    {
        DAL_Crew_CrewDetails objCrew = new DAL_Crew_CrewDetails();
        return objCrew.DEL_CrewVoyages_DL(VoyID, Deleted_By);
    }
    [WebMethod]
    public int Del_CrewTraining(int TrainingID, int Deleted_By)
    {
        return DAL_Crew_Training.DELETE_Training_DL(TrainingID, Deleted_By);
    }
    [WebMethod]
    public string Get_StaffSailingInfo(int CrewID, int RankID)
    {
        DAL_Crew_CrewDetails objCrew = new DAL_Crew_CrewDetails();

        return UDFLib.CreateHtmlTableFromDataTable(objCrew.Get_StaffSailingInfo_DL(CrewID, RankID, 0),
            new string[] { "", "" },
            new string[] { "Caption", "Value" }, "");
    }
    [WebMethod]
    public string Get_StaffInfo(int StaffCode, string DateFormat = "dd/MM/yyyy")
    {
        DAL_Crew_CrewDetails objCrew = new DAL_Crew_CrewDetails();
        DataTable dt = new DataTable();
        string[] str_etr = { };

        dt = objCrew.Get_StaffInfo_DL(StaffCode);
        foreach (DataRow dr in dt.Rows)
        {
            
                if (dr["Value"].ToString().Contains("On"))
                {
                     dr["Value"] = dr["Value"].ToString().Replace("On", "|");
                    str_etr = Convert.ToString(dr["Value"]).Split('|');
                    if (str_etr.Length > 0)
                    {
                        dr["Value"] = str_etr[0] + "On " + UDFLib.ConvertUserDateFormat(Convert.ToString(str_etr[1]), DateFormat);
                    }
                }

        }

        return UDFLib.CreateHtmlTableFromDataTable(dt,
            new string[] { "", "" },
            new string[] { "Caption", "Value" }, "");
    }
    [WebMethod]
    public string Get_CrewQuery_Attachments(int QueryID, int VesselID, int UserID)
    {
        DataTable dt = BLL_Crew_Queries.Get_CrewQuery_Attachments(QueryID, VesselID, UserID);

        UDCHyperLink aLink = new UDCHyperLink("DocURL", "", new string[] { }, new string[] { }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("Attachment_Name", aLink);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
           new string[] { "Attachments" },
           new string[] { "Attachment_Name" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left" },
           "CrewQuery-Attachments-table",
           "CrewQuery-Attachments-DataHeder",
           "CrewQuery-Attachments-Data");

    }
    [WebMethod]
    public string Get_CrewQuery_Followups(int QueryID, int VesselID, int UserID)
    {
        DataTable dt = BLL_Crew_Queries.Get_CrewQuery_Followups(QueryID, VesselID, UserID);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
            new string[] { "Date", "From", "Message" },
            new string[] { "Date_Of_Creation", "CreatedBy", "Followup_Text" },

            new string[] { },
            "CrewQuery-Followup-table",
            "CrewQuery-Followup-DataHeder",
            "CrewQuery-Followup-Data");

    }
    [WebMethod]
    public string Get_Claim_Attachments(int QueryID, int VesselID, int ClaimID, int UserID)
    {
        DataTable dt = BLL_Crew_Queries.Get_Claim_Attachments(QueryID, VesselID, ClaimID, UserID);

        UDCHyperLink aLink = new UDCHyperLink("DocURL", "", new string[] { }, new string[] { }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("Attachment_Name", aLink);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
           new string[] { "Attachments" },
           new string[] { "Attachment_Name" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left" },
           "CrewQuery-Attachments-table",
           "CrewQuery-Attachments-DataHeder",
           "CrewQuery-Attachments-Data");

    }

    [WebMethod]
    public string Get_MedHistory_Attachments(int Case_ID, int Vessel_ID, int Office_ID, int UserID)
    {
        DataTable dt = BLL_Crew_MedHistory.Get_Crew_MedHistory_Attachments(Case_ID, Vessel_ID, Office_ID, UserID);

        UDCHyperLink aLink = new UDCHyperLink("DocURL", "", new string[] { }, new string[] { }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("Attachment_Name", aLink);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
           new string[] { "Attachments" },
           new string[] { "Attachment_Name" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left" },
           "CrewQuery-Attachments-table",
           "CrewQuery-Attachments-DataHeder",
           "CrewQuery-Attachments-Data");

    }
    [WebMethod]
    public string Get_MedHistory_Followups(int Case_ID, int Vessel_ID, int Office_ID, int UserID)
    {
        DataTable dt = BLL_Crew_MedHistory.Get_Crew_MedHistory_Followups(Case_ID, Vessel_ID, Office_ID, UserID);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
            new string[] { "Date", "From", "Message" },
            new string[] { "Date_Of_Creation", "CreatedBy", "Followup_Text" },

            new string[] { },
            "CrewQuery-Followup-table",
            "CrewQuery-Followup-DataHeder",
            "CrewQuery-Followup-Data");

    }
    [WebMethod]
    public string Get_Med_CostItem_Attachments(int Cost_Item_ID, int Case_ID, int Vessel_ID, int Office_ID, int UserID)
    {
        DataTable dt = BLL_Crew_MedHistory.Get_Med_CostItem_Attachments(Cost_Item_ID, Case_ID, Vessel_ID, Office_ID, UserID);

        UDCHyperLink aLink = new UDCHyperLink("DocURL", "", new string[] { }, new string[] { }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("Attachment_Name", aLink);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
           new string[] { "Attachments" },
           new string[] { "Attachment_Name" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left" },
           "CrewQuery-Attachments-table",
           "CrewQuery-Attachments-DataHeder",
           "CrewQuery-Attachments-Data");

    }
    [WebMethod]
    public string Get_Seniority_Log(int CrewID, int VoyageID, int UserID)
    {
        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

        DataTable dt = objCrewBLL.Get_Seniority_Log(CrewID, VoyageID, UserID);

        return UDFLib.CreateHtmlTableFromDataTable(dt,
            new string[] { "Seniority Log" },
            new string[] { "SeniorityLog" },

            new string[] { },
            "Seniority-Log-table",
            "Seniority-Log-DataHeder",
            "Seniority-Log-Data");

    }

    [WebMethod]
    public string Get_Training_Attachments(int TrainingID, int UserID)
    {
        DataTable dt = BLL_Crew_Training.Get_Training_Attachments(TrainingID, UserID);

        UDCHyperLink aLink = new UDCHyperLink("DocURL", "", new string[] { }, new string[] { }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("DisplayName", aLink);


        return UDFLib.CreateHtmlTableFromDataTable(dt,
           new string[] { "Attachments" },
           new string[] { "DisplayName" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left" },
           "CrewQuery-Attachments-table",
           "CrewQuery-Attachments-DataHeder",
           "CrewQuery-Attachments-Data");

    }

    [WebMethod]
    public string Get_CrewMail_PacketItems(int PacketID, string DateFormat = "dd/MM/yyyy")
    {
        DataTable dt = BLL_Crew_CrewMail.Get_PacketItems_Async(PacketID);
        foreach (DataRow dr in dt.Rows)
        {
            if (!string.IsNullOrEmpty(dr["Date_Placed"].ToString()))
                dr["Date_Placed"] = UDFLib.ConvertUserDateFormat(Convert.ToString(dr["Date_Placed"]), DateFormat);
        }

        return UDFLib.CreateHtmlTableFromDataTable(dt,
            new string[] { "Item ID", "Item Description", "Item Ref", "Quantity", "Placed By", "Dept", "Date Placed", "Item Remarks", "Received ONBD", "Vessel Remarks" },
            new string[] { "ID", "Item_Desc", "Item_Ref", "Item_Qty", "Created_By_Name", "Dept", "Date_Placed", "Item_Remarks", "Received_ONBD", "VesselRemarks" },
            new string[] { "center", "left", "left", "center", "left", "left", "left", "left", "center", "left" },
            "CrewMail Items");

    }

    [WebMethod]
    public string Get_FAQ_List(string SearchFaq, int? Page_Index, int? Page_Size, string TableStyleCSS, string HeaderStyleCSS, string QuestionCSS, string AnswerCSS, int Edit)
    {
        int is_Fetch_Count = 1;
        DataTable dt = BLL_LMS_FAQ.Get_FAQ_List(UDFLib.ConvertStringToNull(SearchFaq), UDFLib.ConvertIntegerToNull(Page_Index), UDFLib.ConvertIntegerToNull(Page_Size), ref is_Fetch_Count).Tables[0];

        string result = CreateFAQListFromDataTable(dt, TableStyleCSS, HeaderStyleCSS, QuestionCSS, AnswerCSS, Edit);

        return result + "~totalrecordfound~" + is_Fetch_Count.ToString();
    }



    public static string CreateFAQListFromDataTable(DataTable dtTable, string TableCss, string HeaderCss, string QuestionCSS, string AnswerCSS, int Edit)
    {
        System.Text.StringBuilder strTable = new System.Text.StringBuilder();

        try
        {

            if (dtTable.Rows.Count > 0)
            {
                int icol = 0;

                strTable.Append("<table id='__tbl_remark' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
                if (!string.IsNullOrWhiteSpace(TableCss))
                    strTable.Append(" class='" + TableCss + "' ");
                strTable.Append(" > ");

                foreach (DataRow dr in dtTable.Rows)
                {
                    //Start Question  TR
                    strTable.Append("<tr class='" + QuestionCSS + "'>");
                    strTable.Append("<td class=\"" + QuestionCSS + "-Icon\"><img src=\"..\\Images\\faq.png\"/></td>");
                    strTable.Append("<td class=\"" + QuestionCSS + "-FAQ\">");
                    strTable.Append("<a href=\"javascript:toggle('" + dr["FAQ_ID"].ToString() + "') \">" + dr["Question"].ToString() + "</a>");

                    strTable.Append("</td><td class=\"" + QuestionCSS + "-Edit\">");

                    if (Edit == 1)
                        strTable.Append("<a href=\"javascript:EditFAQDetails('" + dr["FAQ_ID"].ToString() + "') \"><img src=\"..\\Images\\edit.gif\"/></a>");

                    strTable.Append("</td></tr>");

                    //Start Answer TR
                    strTable.Append("<tr id=" + dr["FAQ_ID"].ToString() + " class='" + AnswerCSS + "'>");
                    strTable.Append("<td></td><td colspan='3'>");
                    strTable.Append("<div class=\"QuestionCSS-RecordInfo\"><img src=\"..\\Images\\RecordInformation.png\" ID=\"imgRecordInfo\"   onclick=\"Get_Record_Information('LMS_DTL_FAQ','FAQ_ID=" + dr["FAQ_ID"].ToString() + "',event,this)\" /></div>");
                    strTable.Append("<div class=\"AnsDiv\">" + dr["Answer"].ToString() + "</div>");
                    strTable.Append("</td></tr>");
                    strTable.Append("&nbsp;");
                    icol++;
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

    [WebMethod]
    public string Get_BelowAverageEvaluationDetails(int CrewID, int EvaluationId)
    {
        return UDFLib.CreateHtmlTableFromDataTable(DAL_Crew_Evaluation.Get_BelowAverageEvaluation_Details_DL(CrewID, EvaluationId),
            new string[] { "Question", "Answer", "Remarks" },
            new string[] { "Criteria", "OptionText", "Remarks" },
            new string[] { },
            "CrewQuery-Followup-table",
            "CrewQuery-Followup-DataHeder",
            "CrewQuery-Followup-Data");
    }
    [WebMethod]
    public string asyncGetCrewEvaluation_FeedbackCompleted(int CrewEvaluation_ID, int Vessel_ID, int Office_ID)
    {
        DataTable dt = BLL_Crew_Evaluation.Get_FeedbackCompleted(CrewEvaluation_ID, Vessel_ID, Office_ID);

        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();

        return UDFLib.CreateHtmlTableFromDataTable(dt,
            new string[] { "Requested By", "Requested On", "Comment", "Requested From", "Submitted On", "Feedback", "Action" },
           new string[] { "createdBy", "creationDate", "Req_Remark", "requestedFrom", "CompletionDate", "Remark", "Category" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left", "center", "left", "center", "left" },
           "tbl-common-Css",
           "hdr-common-Css",
           "row-common-Css");

    }
    //************HELP CENTER*******************************//
    [WebMethod]
    public string Get_FAQ_ModuleList(string TableStyleCSS, string ModuleCSS, string TopicListCSS)
    {

        int i = 0;
        System.Text.StringBuilder strTable = new System.Text.StringBuilder();
        DataTable dt = BLL_FAQ_Item.Get_FAQModule_List().Tables[0];

        strTable.Append("<table id='__tbl_remark' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
        if (!string.IsNullOrWhiteSpace(TableStyleCSS))
            strTable.Append(" class='" + TableStyleCSS + "' ");
        strTable.Append(" > ");

        strTable.Append("<tr>");
        foreach (DataRow row in dt.Rows)
        {
            if (i < 3)
            {
                i = i + 1;
                DataTable dtTopic = BLL_FAQ_Item.Get_FAQTopic_List(Convert.ToInt32(row["Module_ID"].ToString())).Tables[0];
                strTable.Append("<td style='vertical-align:top; margin:1%; width:33.33%'>");
                strTable.Append("<h2 class='" + ModuleCSS + "'>" + row["Module_Description"].ToString() + "</h2>");
                strTable.Append("<ul class=\"" + TopicListCSS + "\">");
                foreach (DataRow row1 in dtTopic.Rows)
                {
                    strTable.Append("<li ><a  style='color:#627A62;font-size:medium;text-decoration: none !important;' target=" + "_blank" + " href=\"LMS_Topic_List.aspx?Topic_ID=" + row1["Topic_ID"].ToString() + "\">" + row1["Description"].ToString() + "</a></li>");
                }
                strTable.Append("</ul></td>");
                if (i == 3)
                {
                    strTable.Append("</tr><tr>");
                    i = 0;
                }
            }
        }
        strTable.Append("</tr></table>");

        return strTable.ToString();
    }

    [WebMethod]
    public string Get_Topic_FAQList(int Topic_ID, int FAQ_ID, string Description, string TableStyleCSS, string QuestionCSS, string AnswerCSS, string TopicListCSS, string CategoryCss, int? Page_Index, int? Page_Size, int UserID)
    {
        int is_Fetch_Count = 1;
        string result = "";
        string mode = "";
        int TopicID = 0;
        int FAQID = 0;
        string description = "";
        if (Topic_ID != 0)
        {
            TopicID = Topic_ID;
            mode = "Topic";
        }
        else if (FAQ_ID != 0)
        {
            FAQID = FAQ_ID;
            mode = "FAQ";
        }
        else
        {
            description = Description;
            mode = "Desc";
        }

        DataTable dtFAQ = BLL_FAQ_Item.Get_Topic_FAQList(UDFLib.ConvertIntegerToNull(TopicID), UDFLib.ConvertIntegerToNull(FAQID), description, UDFLib.ConvertIntegerToNull(Page_Index), UDFLib.ConvertIntegerToNull(Page_Size), ref is_Fetch_Count, UserID).Tables[0];
        result = CreateFAQFromDataTable(mode, dtFAQ, TableStyleCSS, QuestionCSS, AnswerCSS, TopicListCSS, CategoryCss, Description, TopicID);
        return result + "~totalrecordfound~" + is_Fetch_Count.ToString();
    }

    public static string CreateFAQFromDataTable(string mode, DataTable dtTable, string TableCss, string QuestionCSS, string AnswerCSS, string TopicListCSS, string CategoryCss, string Description, int TopicID)
    {
        System.Text.StringBuilder strTable = new System.Text.StringBuilder();
        string title = "";
        try
        {
            switch (mode.ToUpper())
            {
                case "TOPIC":
                    if (dtTable.Rows.Count > 0)
                    {
                        int icol = 0;
                        title = "Help/FAQ/" + dtTable.Rows[0]["Description"].ToString();
                        strTable.Append("<table id='__tbl_remark' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
                        if (!string.IsNullOrWhiteSpace(TableCss))
                            strTable.Append(" class='" + TableCss + "' ");
                        strTable.Append(" > ");

                        strTable.Append("<tr class='" + QuestionCSS + "'><td>" + dtTable.Rows[0]["Module_Description"].ToString() + "/" + dtTable.Rows[0]["Description"].ToString() + "</td></tr>");


                        //Start Question  TR
                        strTable.Append("<tr >");
                        strTable.Append("<td class=\"" + QuestionCSS + "-FAQ\">");
                        strTable.Append("<ul class=\"" + TopicListCSS + "\">");
                        foreach (DataRow dr in dtTable.Rows)
                        {
                            strTable.Append("<li id='ID" + dr["FAQ_ID"].ToString() + "' tabindex='1' onclick=\"javascript:Description('" + dr["FAQ_ID"].ToString() + "') \"><span style='color:#627A62;font-size:medium;'>" + dr["Question"].ToString() + "</span></li>");
                            //strTable.Append("<li><a style='color:#627A62;font-size:medium;text-decoration: none !important;' href=\"javascript:Description('" + dr["FAQ_ID"].ToString() + "') \">" + dr["Question"].ToString() + "</a></li>");
                            icol++;
                        }
                        strTable.Append("</ul></td></tr>");
                        strTable.Append("</table>");
                    }
                    else
                    {
                        DataTable dtFAQ = BLL_FAQ_Item.Get_TopicModule(TopicID);
                        title = "Help/FAQ/" + dtFAQ.Rows[0]["Description"].ToString();
                        strTable.Append("<table id='__tbl_remark' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
                        if (!string.IsNullOrWhiteSpace(TableCss))
                            strTable.Append(" class='" + TableCss + "' ");
                        strTable.Append(" > ");

                        strTable.Append("<tr class='" + QuestionCSS + "'><td>" + dtFAQ.Rows[0]["Module_Description"].ToString() + "/" + dtFAQ.Rows[0]["Description"].ToString() + "</td></tr>");

                        //Start Question  TR
                        strTable.Append("<tr >");
                        strTable.Append("<td class=\"" + QuestionCSS + "-FAQ\">");
                        strTable.Append("<span style='color:maroon;padding:2px'> No record found !</span>");
                        strTable.Append("</td></tr>");
                        strTable.Append("</table>");

                    }
                    return title + "~totalrecordfound~" + strTable.ToString();


                case "FAQ":
                    if (dtTable.Rows.Count > 0)
                    {
                        strTable.Append("<table id='__tbl_remark1' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
                        if (!string.IsNullOrWhiteSpace(TableCss))
                            strTable.Append(" class='" + TableCss + "' ");
                        strTable.Append(" > ");

                        strTable.Append("<tr><td align='left'><h2><b>" + dtTable.Rows[0]["Question"].ToString() + "</b></h2></td>");
                        strTable.Append("<td style='width:250px;' ><div id='dvVideo' style='position:absolute;'>");
                        DataTable dt = BLL_FAQ_Item.Get_FAQ_Link(Convert.ToInt32(dtTable.Rows[0]["FAQ_ID"].ToString())).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            strTable.Append("<span class='FAQ-Video'>Related Videos</span><ul class=\"" + TopicListCSS + "-show\">");

                            foreach (DataRow dr in dt.Rows)
                            {
                                strTable.Append("<li><a style='color:#627A62;text-align:left; ' href=\"javascript:Play('" + dr["ITEM_PATH"].ToString() + "','" + dr["Item_name"].ToString() + "') \">" + dr["Item_name"].ToString() + "</a></li>");
                            }
                            strTable.Append("</ul><div></td></tr>");
                        }

                        //Start Question  TR
                        strTable.Append("<tr >");
                        strTable.Append("<td class='" + AnswerCSS + "-desc'>" + dtTable.Rows[0]["Answer"].ToString());
                        strTable.Append("</td><td class=\"" + AnswerCSS + "-FAQ\"></td>");

                        strTable.Append("</tr>");
                        if (dtTable.Rows[0]["Status"].ToString() == "")
                        {
                            strTable.Append("<tr>");
                            strTable.Append("<td  class='" + AnswerCSS + "' style='font-size:small'>Was this helpful?<a style='text-decoration:none !important;color: #86868D; font-size:x-small' href=\"javascript:IsHelpful('Yes','" + dtTable.Rows[0]["FAQ_ID"].ToString() + "') \">Yes</a>/<a style='text-decoration:none !important;color: #86868D;font-size:x-small' href=\"javascript:IsHelpful('No','" + dtTable.Rows[0]["FAQ_ID"].ToString() + "') \">No</a>");
                            strTable.Append("</td><td class=\"" + AnswerCSS + "-FAQ\"></td></tr>");
                        }

                        strTable.Append("</table><br/>");
                    }
                    else
                        strTable.Append("<span style='color:maroon;padding:2px'> No record found !</span>");

                    return strTable.ToString();


                case "DESC":
                    // string Solution;




                    strTable.Append("<table id='__tbl_remark' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
                    if (!string.IsNullOrWhiteSpace(TableCss))
                        strTable.Append(" class='" + TableCss + "' ");
                    strTable.Append(" > ");
                    strTable.Append("<tr><td class=\"" + QuestionCSS + "-FAQ\">Search results for <span style='background-color:#FFFFCC'>" + Description + "</span><br/><br/></td></tr>");

                    strTable.Append("<tr>");
                    strTable.Append("<td align='left' style='vertical-align:top;'><div class='SpanCss'>?</div><div style='float:left; color:Gray; font-weight: bold;font-size: large;'> Frequently Asked Questions:</div></td></tr><tr><td><br/></td></tr>");

                    if (dtTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTable.Rows)
                        {

                            strTable.Append("<tr style=' background-color:#E0EBEB' >");
                            strTable.Append("<td align='left' class=\"" + QuestionCSS + "-FAQ\">");
                            strTable.Append("<a href=\"javascript:Expand('" + dr["FAQ_ID"].ToString() + "') \">" + dr["Question"].ToString() + "</a>");
                            strTable.Append("</td></tr>");
                            strTable.Append("<tr><td align='left' class='" + CategoryCss + "'>Category:<b style='color:black'>" + dr["Module_Description"].ToString() + "/" + dr["Description"].ToString() + "</b></td></tr>");

                            strTable.Append("<tr >");
                            //if (dtTable.Rows[0]["Answer"].ToString().Length > 50)
                            //{
                            //    int iNextSpace = dtTable.Rows[0]["Answer"].ToString().LastIndexOf(" ", 20);
                            //    Solution=string.Format("{0}...", dtTable.Rows[0]["Answer"].ToString().Substring(0, (iNextSpace > 0) ? iNextSpace : 180).Trim());
                            //} 
                            //else
                            //{
                            //    Solution =  dtTable.Rows[0]["Answer"].ToString();
                            //}
                            strTable.Append("<td class='" + AnswerCSS + "'><div style='height:30px;overflow: hidden;line-height:30px;'><div>" + dr["Answer"].ToString());
                            strTable.Append("</div><div></td></tr><tr><td class='" + AnswerCSS + "'><hr></td></tr>");

                        }
                        strTable.Append("<tr><td><br/></td></tr></table>");
                    }
                    else
                        strTable.Append("<tr><td><span class='" + AnswerCSS + "' style='color:maroon;padding:2px'> No record found !</span></td></tr></table>");


                    return strTable.ToString();


            }
            return "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    [WebMethod]
    public int UpdateIsHelpful(int FAQ_ID, int IsHelpful, int UserID)
    {
        int i = BLL_FAQ_Item.Update_FAQ_List(FAQ_ID, IsHelpful, UserID);
        return FAQ_ID;
    }

    [WebMethod]
    public string[] GetFAQDescList(string prefixText, int count)
    {
        BLL_FAQ_Item al = new BLL_FAQ_Item();
        DataTable dt;
        List<string> RetVal = new List<string>();
        string strReturn;
        try
        {
            dt = al.GetFAQDescList(prefixText).Tables[0];

            dt.Rows.Cast<System.Data.DataRow>().Take(count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((dt.Rows[i]["Answer"].ToString().ToUpper()).Contains(prefixText.ToUpper()))
                {
                    string strAnswer = dt.Rows[i]["Answer"].ToString().ToUpper();
                    int l = dt.Rows[i]["Answer"].ToString().IndexOf("<");
                    if (l > 0)
                    {
                        strReturn = dt.Rows[i]["Answer"].ToString().Substring(0, l);
                        if (!RetVal.Contains(strReturn))
                            RetVal.Add(strReturn);
                    }
                    else if ((dt.Rows[i]["Answer"].ToString()).Contains("<"))
                    {
                        strReturn = dt.Rows[i]["Answer"].ToString().Substring(strAnswer.IndexOf(prefixText.ToUpper()), strAnswer.IndexOf("<", strAnswer.IndexOf(prefixText.ToUpper())) - strAnswer.IndexOf(prefixText.ToUpper()));
                        if (!RetVal.Contains(strReturn))
                            RetVal.Add(strReturn);
                    }
                    else
                    {
                        strReturn = dt.Rows[i]["Answer"].ToString();
                        if (!RetVal.Contains(strReturn))
                            RetVal.Add(strReturn);
                    }
                }
                else if ((dt.Rows[i]["Question"].ToString().ToUpper()).Contains(prefixText.ToUpper()))
                {
                    int l = dt.Rows[i]["Question"].ToString().IndexOf("<");
                    if (l > 0)
                    {
                        strReturn = dt.Rows[i]["Question"].ToString().Substring(0, l);
                        if (!RetVal.Contains(strReturn))
                            RetVal.Add(strReturn);
                    }
                    else
                    {
                        strReturn = dt.Rows[i]["Question"].ToString();
                        if (!RetVal.Contains(strReturn))
                            RetVal.Add(strReturn);
                    }
                }

            }
            return RetVal.ToArray();
        }
        catch { throw; }
        finally { al = null; }
    }

    //[WebMethod]
    //public string Get_HelpPage(string TableStyleCSS, string HeaderStyleCSS, string QuestionCSS, string AnswerCSS, string Keywords, int CompanyID, string VideoCSS)
    //{
    //    int is_Fetch_Count = 1;
    //    DataSet ds = BLL_FAQ_Item.Get_HelpPage_List(null, ref is_Fetch_Count, null, null, Keywords, CompanyID);

    //    string result = CreateHelpPageListFromDataTable(ds, TableStyleCSS, HeaderStyleCSS, QuestionCSS, AnswerCSS, VideoCSS);

    //    return result + "~totalrecordfound~" + is_Fetch_Count.ToString();
    //}
    [WebMethod]
    public string Get_HelpPage(string TableStyleCSS, string HeaderStyleCSS, string QuestionCSS, string AnswerCSS, string Keywords, int CompanyID, string VideoCSS, string SearchUrl)
    {
        int is_Fetch_Count = 1;
        DataSet ds = BLL_FAQ_Item.Get_HelpPage_List(null, ref is_Fetch_Count, null, null, Keywords, CompanyID, SearchUrl);

        string result = CreateHelpPageListFromDataTable(ds, TableStyleCSS, HeaderStyleCSS, QuestionCSS, AnswerCSS, VideoCSS);

        return result + "~totalrecordfound~" + is_Fetch_Count.ToString();
    }
    public static string CreateHelpPageListFromDataTable(DataSet dtTable, string TableCss, string HeaderCss, string QuestionCSS, string AnswerCSS, string VideoCSS)
    {
        System.Text.StringBuilder strTable = new System.Text.StringBuilder();

        try
        {
            string APP_NAME = ConfigurationManager.AppSettings["APP_NAME"].ToString();
            if (dtTable.Tables[0].Rows.Count > 0 || dtTable.Tables[1].Rows.Count > 0)
            {
                int icol = 0;

                strTable.Append("<table id='__tbl_remark' CELLPADDING='2' CELLSPACING='0'  style='border-collapse:collapse' ");
                if (!string.IsNullOrWhiteSpace(TableCss))
                    strTable.Append(" class='" + TableCss + "' ");
                strTable.Append(" > ");
                //strTable.Append("<li><a style='color:#627A62;text-align:left; ' href=\"javascript:Play('" + dr["ITEM_PATH"].ToString() + "','" + dr["Item_name"].ToString() + "') \">" + dr["Item_name"].ToString() + "</a></li>");
                foreach (DataRow dr in dtTable.Tables[1].Rows)
                {
                    strTable.Append("<tr class='" + VideoCSS + "'>");
                    strTable.Append("<td class=\"" + VideoCSS + "-Icon\"><img src=\"/" + APP_NAME + "/Images/LMS_Video_Play.png\" height=\"20px\" width=\"20px\" title='Play' onclick=\"Play('" + dr["ITEM_PATH"].ToString() + "','" + dr["Item_name"].ToString() + "','" + dr["ITEM_Description"].ToString() + "','" + dr["DURATION"].ToString() + "')\"/></td>");
                    strTable.Append("<td class=\"" + VideoCSS + "-FAQ\">");
                    strTable.Append("<a style='color:#627A62;text-align:left; ' href=\"javascript:OpenVideo('" + dr["ITEM_PATH"].ToString() + "','" + dr["Item_name"].ToString() + "','" + dr["ITEM_Description"].ToString() + "','" + dr["DURATION"].ToString() + "') \">" + dr["Item_name"].ToString() + "</a>");

                    strTable.Append("</td><td class=\"" + VideoCSS + "-Edit\">");
                    strTable.Append("</td></tr>");

                }

                foreach (DataRow dr in dtTable.Tables[0].Rows)
                {
                    //Start Question  TR
                    strTable.Append("<tr class='" + QuestionCSS + "'>");
                    strTable.Append("<td class=\"" + QuestionCSS + "-Icon\"><img src=\"/" + APP_NAME + "/Images/FAQ_Help.png\" title='Browse' onclick=\"BrowseFAQ(" + dr["FAQ_ID"].ToString() + ",event,this)\"/></td>");
                    strTable.Append("<td class=\"" + QuestionCSS + "-FAQ\">");
                    strTable.Append("<a style='color:#627A62;text-align:left; ' href=\"javascript:BrowseFAQ(" + dr["FAQ_ID"].ToString() + ",event,this) \">" + dr["Question"].ToString() + "</a>");

                    strTable.Append("</td><td class=\"" + QuestionCSS + "-Edit\">");




                    strTable.Append("</td></tr>");

                    ////Start Answer TR
                    //strTable.Append("<tr id=" + dr["FAQ_ID"].ToString() + " class='" + AnswerCSS + "'>");
                    //strTable.Append("<td></td><td colspan='2'>");
                    //// strTable.Append("<div class=\"QuestionCSS-RecordInfo\"><img src=\"../Images/RecordInformation.png\" ID=\"imgRecordInfo\"   onclick=\"Get_Record_Information('LMS_DTL_FAQ','FAQ_ID=" + dr["FAQ_ID"].ToString() + "',event,this)\" /></div>");
                    //strTable.Append("<div class=\"AnsDiv\">" + dr["Answer"].ToString() + "</div>");
                    //strTable.Append("</td></tr>");
                    //strTable.Append("&nbsp;");
                    icol++;
                }
                strTable.Append("</table>");
            }
            else
                strTable.Append("<span style='color:maroon;padding:2px'> Help not found !</span>");

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

    [WebMethod]
    public int UpdateVideo_IsHelpful(int Video_ID, int IsHelpful, int UserID)
    {
        int i = BLL_FAQ_Item.Update_TrainingItem_IsHelpful(Video_ID, IsHelpful, UserID);
        return Video_ID;
    }
    [WebMethod]
    public int referenceCheckForCrew(string CrewID)
    {
        BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
        return objCrewBLL.Check_Crew_Refererce(int.Parse(CrewID));
    }
    [WebMethod]
    public int Del_CrewEvaluation(int ID, int CrewId, int VoyageId, int Deleted_By)
    {
        DAL_Crew_CrewDetails objCrew = new DAL_Crew_CrewDetails();
        return objCrew.DEL_CrewEvalaution_DL(ID, CrewId, VoyageId, Deleted_By);
    }
    [WebMethod]
    public int Del_CrewOtherServices(int CrewID, int ID, int Deleted_By)
    {
        DAL_Crew_CrewDetails objCrew = new DAL_Crew_CrewDetails();
        return objCrew.Delete_CrewOtherServices_DL(ID, CrewID, Deleted_By);
    }
    [WebMethod]
    public int CheckCrewSeniority(int CrewId)
    {
        DAL_Crew_CrewDetails objCrew = new DAL_Crew_CrewDetails();
        return objCrew.CheckCrewSeniority(CrewId);
    }
    [WebMethod]
    public string Get_RJBFormula(string Description)
    {
        DAL_Crew_CrewDetails objCrew = new DAL_Crew_CrewDetails();
        return objCrew.Get_RJBFormula(Description);
    }
    [WebMethod]
    public string asyncGet_Record_Info(int CrewId, string DocType,string DateFormat)
    {
        System.Text.StringBuilder info = new System.Text.StringBuilder();
        DAL_Crew_CrewDetails objCrew = new DAL_Crew_CrewDetails();
        DataTable dtinfo = objCrew.Get_Record_Info(CrewId, DocType);
        if (dtinfo.Rows.Count > 0)
        {
            info.Append("<div style='margin:5px;border-radius:5px'>");
            info.Append("<table cellpadding='3px' >");

            info.Append("<tr>");
            info.Append("<td style='font-weight:bold;font-size:12px;color:#092B4C;text-align:left'>");
            info.Append("Updated: ");
            info.Append("</td>");
            info.Append("<td >" + dtinfo.Rows[0]["IsAutoUpdate"]);
            info.Append("</td>");
            info.Append("<td style='width:50px'>");
            info.Append("&nbsp;");
            info.Append("</td>");
            info.Append("</tr>");

            if (dtinfo.Rows[0]["Modified_By"].ToString().Equals(""))
            {
                info.Append("<tr>");
                info.Append("<td style='font-weight:bold;font-size:12px;color:#092B4C;text-align:left'>");
                info.Append("Current Value: ");
                info.Append("</td>");
                info.Append("<td >" + dtinfo.Rows[0]["CurrentValue"]);
                info.Append("</td>");
                info.Append("<td style='width:50px'>");
                info.Append("&nbsp;");
                info.Append("</td>");
                info.Append("</tr>");

                info.Append("<tr>");
                info.Append("<td style='font-weight:bold;font-size:12px;color:#092B4C;text-align:left'>");
                info.Append("Created By: ");
                info.Append("</td>");
                info.Append("<td >" + dtinfo.Rows[0]["Created_By"]);
                info.Append("</td>");
                info.Append("<td style='width:50px'>");
                info.Append("&nbsp;");
                info.Append("</td>");
                info.Append("</tr>");

                info.Append("<tr>");
                info.Append("<td style='font-weight:bold;font-size:12px;color:#092B4C;text-align:left'>");
                info.Append("Created On: ");
                info.Append("</td>");
                info.Append("<td >" + UDFLib.ConvertUserDateFormat(Convert.ToString(dtinfo.Rows[0]["Date_Of_Creatation"]), DateFormat));
                info.Append("</td>");
                info.Append("<td style='width:50px'>");
                info.Append("&nbsp;");
                info.Append("</td>");
                info.Append("</tr>");
            }
            else
            {
                info.Append("<tr>");
                info.Append("<td style='font-weight:bold;font-size:12px;color:#092B4C;text-align:left'>");
                info.Append("Previous Value: ");
                info.Append("</td>");
                info.Append("<td >" + dtinfo.Rows[0]["PreviousValue"]);
                info.Append("</td>");
                info.Append("<td style='width:50px'>");
                info.Append("&nbsp;");
                info.Append("</td>");
                info.Append("</tr>");

                info.Append("<tr>");
                info.Append("<td style='font-weight:bold;font-size:12px;color:#092B4C;text-align:left'>");
                info.Append("Current Value: ");
                info.Append("</td>");
                info.Append("<td >" + dtinfo.Rows[0]["CurrentValue"]);
                info.Append("</td>");
                info.Append("<td style='width:50px'>");
                info.Append("&nbsp;");
                info.Append("</td>");
                info.Append("</tr>");

                info.Append("<tr>");
                info.Append("<td style='font-weight:bold;font-size:12px;color:#092B4C;text-align:left'>");
                info.Append("Modified By: ");
                info.Append("</td>");
                info.Append("<td >" + dtinfo.Rows[0]["Modified_By"]);
                info.Append("</td>");
                info.Append("<td style='width:50px'>");
                info.Append("&nbsp;");
                info.Append("</td>");
                info.Append("</tr>");

                info.Append("<tr>");
                info.Append("<td style='font-weight:bold;font-size:12px;color:#092B4C;text-align:left'>");
                info.Append("Modified On: ");
                info.Append("</td>");
                info.Append("<td >" + UDFLib.ConvertUserDateFormat(Convert.ToString(dtinfo.Rows[0]["Date_Of_Modification"]),DateFormat));
                info.Append("</td>");
                info.Append("<td style='width:50px'>");
                info.Append("&nbsp;");
                info.Append("</td>");
                info.Append("</tr>");
            }


            info.Append("</table>");
            info.Append("</div>");
        }


        return info.ToString();
    }
}
