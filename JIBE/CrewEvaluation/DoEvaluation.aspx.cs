using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Business.Dashboard;
using SMS.Data.Infrastructure;
using System.IO;

public partial class CrewEvaluation_DoEvaluation : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
    public string dvTitle = "";
    JibeWebService JibeWS = new JibeWebService();
    JibeDashboardService JibeDbWs = new JibeDashboardService();

    string EvalLibRank = "";
    string Evaluator_CrewDtlID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        calEvaDate.Format = UDFLib.GetDateFormat();

        if (Session["USERFULLNAME"] == null)
            Response.Redirect("~/Account/Login.aspx");

        if (!IsPostBack)
        {

            int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"].ToString());
            int Evaluation_ID = UDFLib.ConvertToInteger(Request.QueryString["EID"].ToString());

            try
            {
                lblMonth.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(Request.QueryString["M"]));
            }
            catch
            {
                lblMonth.Text = Request.QueryString["M"];
            }


            Load_CrewPersonalDetails(CrewID);

            DataTable dtEval = BLL_Crew_Evaluation.Get_Evaluations(Evaluation_ID);
            if (dtEval.Rows.Count > 0)
            {
                lblEvalName.Text = dtEval.Rows[0]["Evaluation_Name"].ToString();
            }
            DataTable dt1 = objUser.Get_UserDetails(Convert.ToInt32(Session["USERID"].ToString()));
            if (dt1.Rows.Count > 0)
            {
                int LoggedInUserCrewId = UDFLib.ConvertToInteger(dt1.Rows[0]["CrewId"].ToString());
                DataTable dt = objBLLCrew.Get_CrewPersonalDetailsByID(LoggedInUserCrewId);
                if (dt.Rows.Count > 0)
                {
                    string rank = dt.Rows[0]["Rank_Short_Name"].ToString() == "" ? "" : dt.Rows[0]["Rank_Short_Name"].ToString() + "-";
                    string staffCode = dt.Rows[0]["Staff_Code"].ToString() == "" ? "" : dt.Rows[0]["Staff_Code"].ToString() + "-";
                    lnkEvaluator.Text = rank + staffCode + Session["USERFULLNAME"].ToString();
                    lnkEvaluator.NavigateUrl = "~/Crew/CrewDetails.aspx?ID=" + dt.Rows[0]["ID"].ToString();
                    EvalLibRank = rank;
                    Evaluator_CrewDtlID = dt.Rows[0]["ID"].ToString();
                    //Evaluator_CrewDtlID = dt.Rows[0]["Evaluator_CrewDtlID"].ToString();
                }
            }
            if (Request.QueryString["DtlID"] != null)
            {
                string Dtl_Evaluation_ID = Request.QueryString["DtlID"].ToString();

                Bind_EvaluationResult();
                string Office_ID = hdnOffice_ID.Value.ToString();
                string Vessel_ID = hdnVessel_ID.Value.ToString();


                btnSaveEvaluation.Enabled = false;

                if (Dtl_Evaluation_ID != "" && Office_ID != "" && Vessel_ID != "")
                {
                    if (BLL_Crew_Evaluation.Get_CrewEvaluation_FeedbackCount(GetSessionUserID(), Convert.ToInt32(Dtl_Evaluation_ID), Convert.ToInt32(Office_ID), Convert.ToInt32(Vessel_ID)) > 0)
                    {
                        lnkReqFeedBk.BackColor = System.Drawing.Color.Yellow;
                    }
                    DataSet ds = BLL_Crew_Evaluation.Get_CrewEvaluation_Verification(UDFLib.ConvertToInteger(CrewID), UDFLib.ConvertToInteger(Dtl_Evaluation_ID), UDFLib.ConvertToInteger(Office_ID), UDFLib.ConvertToInteger(Vessel_ID));
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["VerifiedBy"].ToString() != "")
                            {

                                btnSaveFollowUpAndClose.Visible = false;
                                txtMessage.Text = ds.Tables[0].Rows[0]["VerificationComment"].ToString();
                                txtMessage.ReadOnly = true;
                            }
                            else
                            {
                                btnSaveFollowUpAndClose.Enabled = true;
                                txtMessage.ReadOnly = false;
                            }
                        }
                        else
                        {
                            btnSaveFollowUpAndClose.Visible = false;
                            txtMessage.Visible = false;
                            txtVerificationComment.Visible = false;
                        }

                    }
                    else
                    {
                        btnSaveFollowUpAndClose.Visible = false;
                        txtMessage.Visible = false;
                        txtVerificationComment.Visible = false;
                    }


                    string Show_Dashboard = String.Format("AsyncFeedbackHistory();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", Show_Dashboard, true);

                    dvEvalutionFooter.Visible = true;
                    dvEvalutionFooter.InnerHtml = GetEvaluationSignatureDetails(UDFLib.ConvertToInteger(Request.QueryString["SchID"].ToString()), Evaluation_ID);
                    if (Request.QueryString.ToString().Contains("DshBrd"))
                    {
                        if (Request.QueryString["DshBrd"].ToString() != null)
                        {
                            InsertDel_ActionDetails(CrewID, UDFLib.ConvertToInteger(Request.QueryString["SchID"].ToString()), 0);
                        }
                    }

                    return;

                }

            }
            else
            {
                Bind_AssignedCriteria();
                txtEvaDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(DateTime.Today));
                lnkReqFeedBk.Visible = false;
                lnkAddFeedBk.Visible = false;
                lnkHide.Visible = false;
                lnkShow.Visible = false;
                btnSaveFollowUpAndClose.Visible = false;
                txtMessage.Visible = false;
                txtVerificationComment.Visible = false;
            }

            if (Request.QueryString.ToString().Contains("DshBrd"))
            {
                if (Request.QueryString["DshBrd"].ToString() != null)
                {
                    InsertDel_ActionDetails(CrewID, UDFLib.ConvertToInteger(Request.QueryString["SchID"].ToString()), 0);
                }
            }

        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void GridView_AssignedCriteria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Grading_ID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Grading_Type").ToString());
                int Grade_Type = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Grade_Type").ToString());

                int UserAnswer = 0;
                string TextAnswer = "";
                string Remarks = "";


                RadioButtonList rdo = (RadioButtonList)e.Row.FindControl("rdoOptions");
                TextBox txtAnswer = (TextBox)e.Row.FindControl("txtAnswer");
                TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");

                DataTable dt = BLL_Crew_Evaluation.Get_GradingOptions(Grading_ID);
                rdo.DataSource = dt;
                rdo.DataBind();

                int Evaluation_ID = UDFLib.ConvertToInteger(Request.QueryString["EID"].ToString());
                string Remark = "";
                foreach (DataRow row in dt.Rows)
                {
                    DataTable dtremark = BLL_Crew_Evaluation.Get_MandatoryRemark(UDFLib.ConvertToInteger(row["ID"].ToString()), Evaluation_ID, UDFLib.ConvertToInteger(GridView_AssignedCriteria.DataKeys[e.Row.RowIndex].Value.ToString()));
                    if (dtremark.Rows.Count > 0)
                    {
                        if (dtremark.Rows[0]["OptionText"].ToString() == row["OptionText"].ToString())
                        {
                            Remark = Remark + row["ID"].ToString() + ";";
                        }
                    }
                }
                HiddenField hdnRemark = (HiddenField)e.Row.FindControl("hdnRemark");
                hdnRemark.Value = Remark;
                if (Grade_Type == 2)
                {
                    txtAnswer.Visible = true;
                }

                if (Request.QueryString["DtlID"] != null)
                {
                    UserAnswer = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "UserAnswer").ToString());
                    TextAnswer = DataBinder.Eval(e.Row.DataItem, "TextAnswer").ToString();
                    Remarks = DataBinder.Eval(e.Row.DataItem, "Remarks").ToString();


                    rdo.SelectedValue = UserAnswer.ToString();
                    rdo.Enabled = false;
                    txtAnswer.Text = TextAnswer;

                    if (TextAnswer.Length > 100)
                        txtAnswer.Rows = (TextAnswer.Length / 40);

                    txtAnswer.ReadOnly = true;
                    txtRemarks.Text = Remarks;
                    txtRemarks.ReadOnly = true;
                    ((CheckBox)e.Row.FindControl("chkNA")).Enabled = false;
                }
            }
        }
        catch
        {
        }
    }

    protected void Load_CrewPersonalDetails(int ID)
    {
        DataTable dt = objBLLCrew.Get_CrewPersonalDetailsByID(ID);
        if (dt.Rows.Count > 0)
        {
            lblStaffName.Text = dt.Rows[0]["STAFF_FULLNAME"].ToString();

            lnkStaffCode.Text = dt.Rows[0]["STAFF_CODE"].ToString();
            lnkStaffCode.NavigateUrl = "~/Crew/CrewDetails.aspx?ID=" + dt.Rows[0]["ID"].ToString();


            lblRank.Text = dt.Rows[0]["RANK_NAME"].ToString();
            hdnCrewrank.Value = dt.Rows[0]["CurrentRankID"].ToString();
            if (dt.Rows[0]["EST_SING_OFF_DATE"].ToString() != "")
                lblCOC.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["EST_SING_OFF_DATE"]));
            // lblCOC.Text = DateTime.Parse(dt.Rows[0]["EST_SING_OFF_DATE"].ToString()).ToString("dd/MM/yyyy");

        }
    }

    protected void btnSaveEvaluation_Click(object sender, EventArgs e)
    {
        try
        {
            int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"].ToString());
            //int CrewRank = UDFLib.ConvertToInteger(hdnCrewrank.Value);
            int EID = UDFLib.ConvertToInteger(Request.QueryString["EID"].ToString());
            string DueDate = UDFLib.ConvertStringToNull(Request.QueryString["DueDate"]);

            if (!string.IsNullOrEmpty(DueDate))
            {
                //  DueDate = DueDate.ToString().Replace('-', '/');
            }
            else
                if (txtEvaDate.Text != "")
                {
                    DueDate = txtEvaDate.Text;
                }

            int Schedule_ID = 0;
            if (Request.QueryString["SchID"] != null)
                Schedule_ID = UDFLib.ConvertToInteger(Request.QueryString["SchID"].ToString());

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Criteria_ID", typeof(int)));
            dt.Columns.Add(new DataColumn("UserAnswer", typeof(int)));
            dt.Columns.Add(new DataColumn("TextAnswer", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dt.Columns.Add(new DataColumn("NotApplicable", typeof(int)));

            int QustionAnswered = 0;
            foreach (GridViewRow row in GridView_AssignedCriteria.Rows)
            {
                QustionAnswered = 0;
                int UserAnswer = 0;
                string TextAnswer = "";

                HiddenField hdnCriteria_ID = (HiddenField)row.FindControl("hdnCriteria_ID");
                int Criteria_ID = UDFLib.ConvertToInteger(hdnCriteria_ID.Value);

                RadioButtonList rdoOptions = (RadioButtonList)row.FindControl("rdoOptions");
                if (rdoOptions != null)
                {
                    UserAnswer = UDFLib.ConvertToInteger(rdoOptions.SelectedValue);
                }
                string Remarks = ((TextBox)(row.FindControl("txtRemarks"))).Text;
                ((TextBox)(row.FindControl("txtRemarks"))).BackColor = System.Drawing.Color.White;
                TextBox txtAns = (TextBox)row.FindControl("txtAnswer");
                if (txtAns != null)
                {
                    TextAnswer = txtAns.Text;
                }

                CheckBox chkNA = (CheckBox)row.FindControl("chkNA");

                if (chkNA.Checked == true)
                {
                    UserAnswer = 0;
                    TextAnswer = "";
                }
                if (chkNA.Checked == true || UserAnswer > 0 || TextAnswer.Length > 0)
                {
                    QustionAnswered = 1;
                }
                if (QustionAnswered == 0)
                {
                    QustionAnswered = -1;
                    break;
                }

                DataRow dr = dt.NewRow();
                dr[0] = Criteria_ID;
                dr[1] = UserAnswer;
                dr[2] = TextAnswer;
                dr[3] = Remarks;
                dr[4] = (chkNA.Checked == true ? 1 : 0);
                dt.Rows.Add(dr);

            }

            string js = "";
            if (QustionAnswered > 0)
            {
                int C_EID = BLL_Crew_Evaluation.INSERT_Crew_Evaluation(CrewID, EID, GetSessionUserID(), UDFLib.ConvertToDate(txtEvaDate.Text).ToShortDateString(), GetSessionUserID(), UDFLib.ConvertToDate(DueDate).ToShortDateString(), Schedule_ID);
                if (C_EID > 0)
                {
                    BLL_Crew_Evaluation.INSERT_Crew_Evaluation_Answer(C_EID, dt, GetSessionUserID());
                }
                js = "alert('Evaluation Saved');window.open('','_self','');window.close();";
                btnSaveEvaluation.Enabled = false;
            }
            else if (QustionAnswered == -1)
            {
                js = "alert('All evalution question are mandatory');chkNA_Checked();";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        
        }
    }

    protected void Bind_AssignedCriteria()
    {
        int Evaluation_ID = UDFLib.ConvertToInteger(Request.QueryString["EID"].ToString());

        DataTable dt = BLL_Crew_Evaluation.Get_Assigned_CriteriaList(Evaluation_ID, "", 0);

        DataView dataView = new DataView(dt);

        if (ViewState["sortExpression"] != null)
            dataView.Sort = ViewState["sortExpression"].ToString();

        GridView_AssignedCriteria.DataSource = dataView;
        GridView_AssignedCriteria.DataBind();

    }

    protected void Bind_EvaluationResult()
    {
        int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"].ToString());
        int Evaluation_ID = UDFLib.ConvertToInteger(Request.QueryString["EID"].ToString());
        int Month = UDFLib.ConvertToInteger(Request.QueryString["M"].ToString());
        int Dtl_Evaluation_ID = UDFLib.ConvertToInteger(Request.QueryString["DtlID"].ToString());

        DataTable dt = BLL_Crew_Evaluation.Get_CrewEvaluation_Details(CrewID, Dtl_Evaluation_ID); ;

        DataView dataView = new DataView(dt);

        if (ViewState["sortExpression"] != null)
            dataView.Sort = ViewState["sortExpression"].ToString();

        GridView_AssignedCriteria.DataSource = dataView;
        GridView_AssignedCriteria.DataBind();

        if (dt.Rows.Count > 0)
        {
            hdnOffice_ID.Value = dt.Rows[0]["Office_ID"].ToString();
            hdnVessel_ID.Value = dt.Rows[0]["Vessel_ID"].ToString();
            string rank = dt.Rows[0]["Rank_Short_Name"].ToString() == "" ? "" : dt.Rows[0]["Rank_Short_Name"].ToString() + "-";
            string staffCode = dt.Rows[0]["Staff_Code"].ToString() == "" ? "" : dt.Rows[0]["Staff_Code"].ToString() + "-";
            lnkEvaluator.Text = rank + staffCode + dt.Rows[0]["Evaluator"].ToString();
            lnkEvaluator.NavigateUrl = "~/Crew/CrewDetails.aspx?ID=" + dt.Rows[0]["Evaluator_CrewDtlID"].ToString();
            txtEvaDate.Text = Convert.ToDateTime(dt.Rows[0]["Evaluation_Date"].ToString()).ToString(Convert.ToString(Session["User_DateFormat"]));
            txtEvaDate.Enabled = false;
            EvalLibRank = rank;
            Evaluator_CrewDtlID = dt.Rows[0]["Evaluator_CrewDtlID"].ToString();
        }
    }

    protected void btnReqFeedBk_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["DtlID"] != null && Request.QueryString["CrewID"] != null && Request.QueryString["DtlID"] != "" && Request.QueryString["CrewID"] != "")
        {
            //string Show_Dashboard1 = String.Format("AsyncFeedbackHistory();");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", Show_Dashboard1, true);

            string CrewID = Request.QueryString["CrewID"].ToString();
            string Dtl_Evaluation_ID = Request.QueryString["DtlID"].ToString();
            string Office_ID = hdnOffice_ID.Value.ToString();
            string Vessel_ID = hdnVessel_ID.Value.ToString();
            if (((LinkButton)sender).ID.ToString() == "lnkReqFeedBk")
                dvTitle = "Crew Evaluation Feedback Request";
            else
                dvTitle = "Add Feedback";
            string Evaluation_ID = Request.QueryString["EID"].ToString();
            string Month = Request.QueryString["M"].ToString();
            string Schedule_ID = Request.QueryString["SchID"].ToString();

            string src = "FeedbackRequest.aspx?ID=" + Dtl_Evaluation_ID + "&Vessel_ID=" + Vessel_ID + "&Office_ID=" + Office_ID + "&Crew_ID=" + CrewID + "&Evaluation_ID=" + Evaluation_ID + "&Month=" + Month + "&Schedule_ID=" + Schedule_ID + "&btnID=" + ((LinkButton)sender).ID.ToString();
            ifrmFeedbackRequest.Attributes.Add("src", src);
            UpdatePanel_Frame.Update();
            string js = "showModal('dvCrewEvalFeedbackReq',false);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CrewExists", js, true);
        }
    }
    protected void brnVerifySave(object sender, EventArgs e)
    {
        string js;
        try
        {
            int Office_ID = UDFLib.ConvertToInteger(hdnOffice_ID.Value.ToString());
            int Vessel_ID = UDFLib.ConvertToInteger(Convert.ToInt32(hdnVessel_ID.Value.ToString()));
            if (txtMessage.Text.Trim().Length == 0)
            {
                js = "alert('Verification comment is mandatory!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "js", js, true);
                return;
            }

            txtMessage.ReadOnly = true;
            int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"].ToString());
            int Dtl_Evaluation_ID = UDFLib.ConvertToInteger(Request.QueryString["DtlID"].ToString());
            int VerifiedBy = int.Parse(Session["USERID"].ToString());
            BLL_Crew_Evaluation.Update_CrewEvaluation_Verification(CrewID, Dtl_Evaluation_ID, Office_ID, Vessel_ID, VerifiedBy, txtMessage.Text);
            //btnVerify.Enabled = false;

            txtMessage.Enabled = false;
            btnSaveFollowUpAndClose.Enabled = false;
            //LoadFollowUps(iJob_OfficeID, VESSEL_ID, Worklist_ID);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);        
        }

    }

    /// <summary>
    /// display created by and signed by evaluation footer
    /// </summary>
    /// <param name="iSchedule_ID"></param>
    /// <param name="iEvaluation_ID"></param>
    /// <returns></returns>
    public string GetEvaluationSignatureDetails(int iSchedule_ID, int iEvaluation_ID)
    {
        System.Text.StringBuilder info = new System.Text.StringBuilder();
        try
        {
            Boolean blnIsEvalByVesselUser = false;
            Boolean blnIsSignByVesselUser = false;

            DataTable dtEvlSignedBy = BLL_Crew_Evaluation.Get_EvaluatedCrewDetails(iSchedule_ID);
            
           // Evaluator_CrewDtlID = Evaluator_CrewDtlID == "" ? "1" : Evaluator_CrewDtlID;
            
            DataTable dtEvlCreatedByPersonalDetails = objBLLCrew.Get_CrewPersonalDetailsByID(UDFLib.ConvertToInteger(Evaluator_CrewDtlID));
                        
            DataTable dtEvlCreatedByinfo =  DAL_Infra_Common.Get_Crew_Information(UDFLib.ConvertToInteger(Evaluator_CrewDtlID), UDFLib.ConvertDateToNull(DateTime.Now.ToString()));

            if (dtEvlCreatedByinfo.Rows.Count > 0)
            {
                if (dtEvlCreatedByinfo.Rows[0]["USERTYPE"].ToString() != "OFFICE USER")
                    blnIsEvalByVesselUser = true;
            }



            string CrewID = Request.QueryString["CrewID"].ToString();
            DataTable dtEvlSignedByPersonalDetails = objBLLCrew.Get_CrewPersonalDetailsByID(UDFLib.ConvertToInteger(CrewID));
            DataTable dtEvlSignedByinfo = DAL_Infra_Common.Get_Crew_Information(UDFLib.ConvertToInteger(CrewID), UDFLib.ConvertDateToNull(DateTime.Now.ToString()));
            if (dtEvlSignedByinfo.Rows.Count > 0)
            {
                if (dtEvlSignedByinfo.Rows[0]["USERTYPE"].ToString() != "OFFICE USER")
                    blnIsSignByVesselUser = true;
            }

            info.Append("<table cellpadding='0' id='dvEvalutionFooter' cellspacing='0' style='color:#000;'>");
            info.Append("<tr>");
            info.Append("<td rowspan='2' width='60px' >");
            if ((dtEvlCreatedByPersonalDetails.Rows[0]["PhotoURL"] != null) && (dtEvlCreatedByPersonalDetails.Rows[0]["PhotoURL"].ToString() != ""))
            {
                if (File.Exists(Server.MapPath("Uploads/CrewImages/" + dtEvlCreatedByPersonalDetails.Rows[0]["PhotoURL"])))
                    info.Append("<img id='imgCreatedBy' width='35' height='35'  alt='' src='/jibe/Uploads/CrewImages/" + dtEvlCreatedByPersonalDetails.Rows[0]["PhotoURL"].ToString() + "'>");
                else
                    info.Append("<img id='imgCreatedBy' width='35' height='35'  alt=''  src='/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/Images/NoPic.png'>");
            }
            else
            {
                info.Append("<img id='imgCreatedBy' width='35' height='35'  alt=''  src='/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/Images/NoPic.png'>");
            }
            
            info.Append("</td>");
            info.Append("<td width='70px' style='text-align:left;'>Created By :");
            info.Append("</td>");

            info.Append("<td style='padding-left:10px;text-align:left;'>");
            info.Append("<a ID='lnkCreatedBy' style='text-decoration:none; float:left;' href='../Crew/CrewDetails.aspx?ID=" + UDFLib.ConvertToInteger(Evaluator_CrewDtlID) + "' runat='server' Target='_blank'>" + EvalLibRank + dtEvlCreatedByPersonalDetails.Rows[0]["staff_fullname"].ToString() + "</a>");
            info.Append("</td>");

            if ((dtEvlSignedBy.Rows[0]["EvaluationSigned"].ToString().ToLower() == "1") && (blnIsSignByVesselUser == true)) //displays data if evaluation is signed
            {
                info.Append("<td rowspan='2' width='60px' style='padding-left:20px;'>");
                if ((dtEvlSignedByPersonalDetails.Rows[0]["PhotoURL"] != null) && (dtEvlSignedByPersonalDetails.Rows[0]["PhotoURL"].ToString() != ""))
                {
                    if (File.Exists(Server.MapPath("Uploads/CrewImages/" + dtEvlSignedByPersonalDetails.Rows[0]["PhotoURL"])))
                        info.Append("<img id='imgCreatedBy' width='35' height='35'  alt='' src='/jibe/Uploads/CrewImages/" + dtEvlSignedByPersonalDetails.Rows[0]["PhotoURL"].ToString() + "'>");
                    else
                        info.Append("<img id='imgCreatedBy' width='35' height='35'  alt=''  src='/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/Images/NoPic.png'>");
                }
                else
                {
                    info.Append("<img id='imgCreatedBy' width='35' height='35'  alt=''  src='/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/Images/NoPic.png'>");
                }
                //info.Append("<img id='imgSignedBy' width='35' height='35' alt='' src='/jibe/Uploads/CrewImages/" + dtEvlSignedByPersonalDetails.Rows[0]["PhotoURL"].ToString() + "'>");
                info.Append("</td>");
                info.Append("<td width='70px' style='text-align:left;'>Signed By :</td>");
                info.Append("<td  style='padding-left:10px;' style='text-align:left;'>");
                info.Append("<a ID='lnkSignedBy' style='text-decoration:none; float:left;' href='../Crew/CrewDetails.aspx?ID=" + dtEvlSignedByPersonalDetails.Rows[0]["ID"].ToString() + "' runat='server' Target='_blank'>" + dtEvlSignedByPersonalDetails.Rows[0]["STAFF_FULLNAME"].ToString() + "</a>");
                info.Append("</td>");
            }
            info.Append("</tr>");

            info.Append("<tr>");
            info.Append("<td width='auto' style='text-align:left;'><label ID='lblCreatedDt' runat='server'>" + UDFLib.ConvertUserDateFormat(Convert.ToString(dtEvlSignedBy.Rows[0]["Date_of_Creation"])) + "</label></td>");
            info.Append("<td><div id='' style='text-align: left; width: 100px; border: 0px solid gray; '>");
            info.Append(JibeWS.asyncGet_Crew_Information(Convert.ToString(Evaluator_CrewDtlID)));
            info.Append("</div></td>");

            if ((dtEvlSignedBy.Rows[0]["EvaluationSigned"].ToString().ToLower() == "1") && (blnIsSignByVesselUser == true)) //displays data if evaluation is signed
            {
                info.Append("<td width='auto' style='text-align:left;'>");
                info.Append("<label ID='lblSignedByDt' runat='server'>" + UDFLib.ConvertUserDateFormat(Convert.ToString(dtEvlSignedBy.Rows[0]["Date_Of_EvalSigned"])) + "</label>");
                info.Append("</td>");

                info.Append("<td>");
                info.Append("<div id='dvCrewInformation' style='text-align: left; width: 100px; border: 0px solid gray;'>");
                info.Append(JibeWS.asyncGet_Crew_Information(CrewID));
                info.Append("</div>");
                info.Append("</td>");
            }
            info.Append("</tr>");
            info.Append("</table>");

            //if (blnIsSignByVesselUser == true)
            //{
            if (dtEvlSignedBy.Rows[0]["EvaluationSigned"].ToString().ToLower() == "0")
            {
                LblDigitalSign.Text = "Refused to Sign";
                LblDigitalSign.ForeColor = System.Drawing.Color.Red;
            }
            else if (dtEvlSignedBy.Rows[0]["EvaluationSigned"].ToString().ToLower() == "1")
            {
                LblDigitalSign.Text = "Signed by the evaluated staff";
                LblDigitalSign.ForeColor = System.Drawing.Color.Green;
            }
            else if (dtEvlSignedBy.Rows[0]["EvaluationSigned"].ToString().ToLower() == "-1")
            {
                LblDigitalSign.Text = "Not signed. Crew sign-off";
                LblDigitalSign.ForeColor = System.Drawing.Color.Red;
            }
            //}
            return info.ToString();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            return info.ToString();
        }

    }

    public static void InsertDel_ActionDetails(int CrewId, int ScheduleID, int Del)
    {
        System.Text.StringBuilder info = new System.Text.StringBuilder();
        BLL_Dashboard.Dash_InsertDel_CrwAction_RefusedToSign(CrewId, ScheduleID, 0);
    }
}