using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Configuration;


public partial class Crew_CrewApproval : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (GetSessionUserID() == 0)
                Response.Redirect("~/account/login.aspx");

            if (!IsPostBack)
            {
                BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();

                UserAccessValidation();
                

                
                if (getQueryString("ID") != "")
                {
                    BindVesselTypes(int.Parse(getQueryString("ID")));
                    DataTable dt = objCrewBLL.Get_CrewPersonalDetailsByID(int.Parse(getQueryString("ID")));
                    if (dt.Rows.Count > 0)
                    {
                        txtCrewName.Text = dt.Rows[0]["staff_fullname"].ToString();
                        txtManningOffice.Text = dt.Rows[0]["company_name"].ToString();
                        txtAppliedRank.Text = dt.Rows[0]["rank_applied_name"].ToString();
                        
                        if (dt.Rows[0]["CrewManagerApproval"].ToString() == "1" || dt.Rows[0]["CrewManagerApproval"].ToString() == "-1")
                        {

                            txtApprover.Text = dt.Rows[0]["ApproverName"].ToString();

                            txtApprovalDate.Text = UDFLib.ConvertUserDateFormat(dt.Rows[0]["ApprovalDate"].ToString());


                            txtResultText.Text = dt.Rows[0]["CrewManagerRemark"].ToString();
                            txtOther.Text = dt.Rows[0]["OtherRemark"].ToString();

                            rdoSelected.SelectedValue = dt.Rows[0]["CrewManagerApproval"].ToString();

                            if (dt.Rows[0]["staff_rank"].ToString() != "")
                                ddlApprovedRank.Text = dt.Rows[0]["staff_rank"].ToString();

                            ddlApprovedRank.Enabled = false;
                            txtResultText.Enabled = false;
                            txtOther.Enabled = false;
                            btnSaveInterviewResult.Enabled = false;
                            rdoSelected.Enabled = false;

                        }
                        else
                        {
                            if (dt.Rows[0]["Active_Status"].ToString() == "0")
                            {
                                btnSaveInterviewResult.Enabled = false;
                                string js = "alert('Crew is INACTIVE , cannot be Approved');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                            }
                            else if (dt.Rows[0]["Crew_Status"].ToString() == "NTBR")
                            {
                                btnSaveInterviewResult.Enabled = false;
                                string js = "alert('Crew is marked as NTBR , cannot be Approved');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                            }
                            else
                            {
                                txtApprover.Text = getSessionString("USERFULLNAME");
                                txtApprovalDate.Text = DateTime.Today.ToString(Convert.ToString(Session["User_DateFormat"]));

                                txtResultText.Enabled = true;
                                txtOther.Enabled = true;
                                btnSaveInterviewResult.Enabled = true;
                                rdoSelected.Enabled = true;
                            }
                        }

                    }
                    else
                    {
                        txtApprover.Text = getSessionString("USERFULLNAME");
                        txtApprovalDate.Text = DateTime.Today.ToString(Convert.ToString(Session["User_DateFormat"]));

                    }

                    if (getQueryString("Quick") != "")
                    {
                        txtResultText.BackColor = System.Drawing.Color.Yellow;

                    }
                }
            }
        }
        catch { }
    }
    private int GetSessionUserID()
    {
        try
        {
            if (Session["USERID"] != null)
                return int.Parse(Session["USERID"].ToString());
            else
                return 0;
        }
        catch
        {
            return 0;
        }
    }
    private string getQueryString(string QueryField)
    {
        try
        {
            if (Request.QueryString[QueryField] != null && Request.QueryString[QueryField].ToString() != "")
            {
                return Request.QueryString[QueryField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }
    private string getSessionString(string SessionField)
    {
        try
        {
            if (Session[SessionField] != null && Session[SessionField].ToString() != "")
            {
                return Session[SessionField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }
    /// <summary>
    /// Get all Vessel Types
    /// </summary>
    /// 
    public void BindVesselTypes(int CrewId)
    {
        try
        {
            DataTable dtVesselType = objCrewBLL.GET_VesselTypeForCrew(CrewId);
            ddlVesselType.DataSource = dtVesselType;
            ddlVesselType.DataTextField = "VesselTypes";
            ddlVesselType.DataValueField = "ID";
            ddlVesselType.DataBind();
            dtVesselType.PrimaryKey = new DataColumn[] { dtVesselType.Columns["VesselTypes"] };
            CheckBoxList chk = ddlVesselType.FindControl("CheckBoxListItems") as CheckBoxList;
            foreach (ListItem chkitem in chk.Items)
            {
                DataRow dr = dtVesselType.Rows.Find(chkitem);
                if (dr["Selected"].ToString() == "1")
                {
                    chkitem.Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnSaveInterviewResult_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdoSelected.SelectedValue == "")
            {
                string js = "alert('Please Select Status of the Crew');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            }
            else
            {
                if (getQueryString("ID") != "" && getSessionString("USERID") != "")
                {
                    if (txtResultText.Text == "")
                    {
                        string js = "alert('Please enter your REMARK.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                    }
                    else if (ddlApprovedRank.SelectedValue == "0")
                    {
                        string js = "alert('Please select approved RANK.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                    }
                    else
                    {
                        int QuickApproval = 0;
                        int CrewID = UDFLib.ConvertToInteger(Request.QueryString["ID"].ToString());

                        if (getQueryString("Quick") != "")
                            QuickApproval = 1;

                        int Approved_Rank = UDFLib.ConvertToInteger(ddlApprovedRank.SelectedValue);

                        int Approval = UDFLib.ConvertToInteger(rdoSelected.SelectedValue);

                        //Get selected Vessel Types 
                        int i = 1;
                        DataTable dtVesselTypes = new DataTable();
                        dtVesselTypes.Columns.Add("PID");
                        dtVesselTypes.Columns.Add("VALUE"); 

                        foreach (DataRow dr in ddlVesselType.SelectedValues.Rows)
                        {
                            DataRow dr1 = dtVesselTypes.NewRow();
                            dr1["PID"] = i;
                            dr1["VALUE"] = dr[0];
                            dtVesselTypes.Rows.Add(dr1);
                            i++;
                        }

                        if (Approval == 1)
                        {
                            if (dtVesselTypes.Rows.Count > 0)
                            {
                                int validate = objCrewBLL.Validate_InterviewConfig(CrewID);
                                if (validate == 0)
                                {
                                    string js = "alert('Crew can not be approved as there is no interview taken by office!!');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertNo", js, true);
                                }
                                else if (validate == 1)
                                {
                                    string js = "alert('Crew can not be APPROVED as one or more interview is REJECTED for the crew!!');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertNo", js, true);
                                }
                                else if (validate == 2)
                                {
                                    string js = "alert('Crew interview is scheduled but not yet finalized!!');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertNo", js, true);
                                }
                                else
                                {
                                    //Depending on crew detail configuration mandatory field check is done...
                                    DataTable dt = objCrewBLL.CheckCrewMandatoryFields(CrewID);
                                    if ( dt.Rows.Count > 0 && dt.Rows[0][0].ToString() == "" )
                                    {
                                        objCrewBLL.UPDATE_CrewApprovalByHeadOffice(CrewID, GetSessionUserID(), txtApprovalDate.Text, Approval, txtOther.Text, txtResultText.Text, QuickApproval, Approved_Rank, dtVesselTypes);
                                        lblMessage.Text = "Crew approved successfully!!";
                                        btnSaveInterviewResult.Enabled = false;
                                        SendMail_CrewApproval(UDFLib.ConvertToInteger(rdoSelected.SelectedValue));
                                    }
                                    else
                                    {
                                        string js = "alert('Crews mandatory data "+ dt.Rows[0][0].ToString() + " missing ');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertNo", js, true);
                                    }
                                }
                            }
                            else
                            {
                                string js = "alert('Select at least one Vessel Type!!');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertNo", js, true);
                            }
                        }
                        else
                        {
                            objCrewBLL.UPDATE_CrewApprovalByHeadOffice(CrewID, GetSessionUserID(), txtApprovalDate.Text, Approval, txtOther.Text, txtResultText.Text, QuickApproval, Approved_Rank, dtVesselTypes);
                            if ( QuickApproval == 1 )
                                lblMessage.Text = "Crew approval saved successfully!!";
                            btnSaveInterviewResult.Enabled = false;
                            SendMail_CrewApproval(UDFLib.ConvertToInteger(rdoSelected.SelectedValue));
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CrewDetails.aspx?ID=" + getQueryString("ID"));
    }

    protected void SendMail_CrewApproval(int Approval)
    {
        string ManningOffice = "";
        string Staff_Name = "";
        string Applied_Rank = "";
        string ReadyToJoin = "";
        string ManningOfficeMailID = "";
        string Staff_Code = "";

        try
        {
            DataTable dt = objCrewBLL.Get_CrewPersonalDetailsByID(int.Parse(getQueryString("ID")));
            if (dt.Rows.Count > 0)
            {
                ManningOffice = dt.Rows[0]["Company_Name"].ToString();
                Staff_Name = dt.Rows[0]["Staff_FullName"].ToString();
                Applied_Rank = dt.Rows[0]["rank_applied_name"].ToString();
                ReadyToJoin = dt.Rows[0]["available_from_date"].ToString();
                ManningOfficeMailID = dt.Rows[0]["ManningOfficeMailID"].ToString();
                Staff_Code = dt.Rows[0]["Staff_Code"].ToString();
            }

            string msgTo = ManningOfficeMailID;
            string msgCC = "";
            string msgBCC = "";
            string msgSubject = "";
            string msgBody = "";

            int Approved_RankID = int.Parse(ddlApprovedRank.SelectedValue);
            string Approved_Rank = ddlApprovedRank.SelectedItem.Text;

            if (Approval == 1)
            {
                //-- APPROVED --
                msgSubject = "Application is APPROVED by the Crew Team for " + Staff_Name;

                msgBody = "System Notification: Please do not reply to this mail.<br><br>";

                msgBody += "Attn: " + ManningOffice + "<br><br>";

                msgBody += "The application for " + Staff_Name + " rank " + Approved_Rank + " and has been APPROVED.<br><br>";
                msgBody += "The staff is assigned with a staff code: " + Staff_Code + " in the system.<br><br>";


                msgBody += "As this personnel is APPROVED and is planned to join a vessel, please arrange to update the following particulars in the jibe immediately and confirm:";
                msgBody += "<br>";

                DataTable dtMissingData = objCrewBLL.Get_Crew_MissingData(int.Parse(getQueryString("ID")));

                int iCounter = 0;

                foreach (DataRow dr in dtMissingData.Rows)
                {
                    if (dr[0].ToString() != "")
                    {
                        iCounter++;
                        msgBody += iCounter + ")  " + dr[0].ToString() + " <br>";
                    }
                }

                msgBody += "<br><br>";
                msgBody += "<b>*** IMPORTANT ***<br>";
                msgBody += "Without the above data updated in our crew module, this personnel will not be taken onboard our vessels.</b><br><br>";

                //msgBody += "Kindly update all required details in the system, Upload scanned copy of the staff certificates and update the document checklist.<br><br>";

                string querystring = UDFLib.Encrypt("id=" + getQueryString("ID"));

                msgBody += "<a href='http://" + Request.ServerVariables["SERVER_NAME"].ToString() + "/" + ConfigurationManager.AppSettings["APP_NAME"].ToUpper() + "/crew/crewdetails.aspx" + querystring + "'>--- Click to view the crew details ---</a><br>";
            }
            else if (Approval == -1)
            {
                //-- REJECTED --

                msgSubject = "Application is NOT APPROVED by the Crew Team for " + Staff_Name;

                msgBody = "System Notification: Please do not reply to this mail.<br><br>";

                msgBody += "Attn: " + ManningOffice + "<br><br>";

                msgBody += "The application for " + Staff_Name + " rank " + Applied_Rank + " is NOT APPROVED .<br><br>";

                //msgBody += "Kindly update all required details in the system, Upload scanned copy of the staff certificates and update the document checklist.<br><br>";

                string querystring = UDFLib.Encrypt("id=" + getQueryString("ID"));

                msgBody += "<a href='http://" + Request.ServerVariables["SERVER_NAME"].ToString() + "/" + ConfigurationManager.AppSettings["APP_NAME"].ToUpper() + "/crew/crewdetails.aspx" + querystring + "'>--- Click to view the crew details ---</a><br>";

            }
            else
            {
                //-- PENDING --

                msgSubject = "Application is ON HOLD by the Crew Team for " + Staff_Name;

                msgBody = "System Notification: Please do not reply to this mail.<br><br>";

                msgBody += "Attn: " + ManningOffice + "<br><br>";

                msgBody += "The application for " + Staff_Name + " rank " + Applied_Rank + " is ON HOLD .<br><br>";

                //msgBody += "Kindly update all required details in the system, Upload scanned copy of the staff certificates and update the document checklist.<br><br>";

                string querystring = UDFLib.Encrypt("id=" + getQueryString("ID"));

                msgBody += "<a href='http://" + Request.ServerVariables["SERVER_NAME"].ToString() + "/" + ConfigurationManager.AppSettings["APP_NAME"].ToUpper() + "/crew/crewdetails.aspx" + querystring + "'>--- Click to view the crew details ---</a><br>";

            }

            objCrewBLL.Send_CrewNotification(int.Parse(getQueryString("ID")), 0, 0, 3, msgTo, msgCC, msgBCC, msgSubject, msgBody, "", "MAIL", "", GetSessionUserID(), "READY");
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }


    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        UserAccess objUA = new UserAccess();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
        }
        if (objUA.Edit == 0)
        {
        }
        if (objUA.Delete == 0)
        {
        }

        if (objUA.Approve == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");
        }

        //-- MANNING OFFICE LOGIN --
       
    }

 

    

}