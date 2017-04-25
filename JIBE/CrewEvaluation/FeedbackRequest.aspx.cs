using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;

public partial class CrewEvaluation_FeedbackRequest : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
    public string TodayDateFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CalendarExtendertxtDueDate.Format = Convert.ToString(Session["User_DateFormat"]);
       
        TodayDateFormat = UDFLib.DateFormatMessage();
        if (Session["USERFULLNAME"] == null)
            Response.Redirect("~/Account/Login.aspx");

        if (!IsPostBack)
        {
            DataTable dt = objUser.Get_UserList(Convert.ToInt32(Session["USERCOMPANYID"]));

            ddlFeedbackFrom.DataSource = dt;
            ddlFeedbackFrom.DataTextField = "USERNAME";
            ddlFeedbackFrom.DataValueField = "UserID";
            ddlFeedbackFrom.DataBind();
            ddlFeedbackFrom.Items.Insert(0, new ListItem("-Select-", "0"));


            DataTable dt1 = BLL_Crew_Evaluation.Get_FeedbackCategories();

            ddlFeedbackCategory_Add.DataSource = dt1;
            ddlFeedbackCategory_Add.DataTextField = "CATEGORY";
            ddlFeedbackCategory_Add.DataValueField = "ID";
            ddlFeedbackCategory_Add.DataBind();

            lblName.Text = Session["USERFULLNAME"].ToString();
            lblDate.Text = DateTime.Now.ToString(Convert.ToString(Session["User_DateFormat"]));

            Load_CrewEvaluationFeedbackRequest();

        }
    }

    protected void GridView_AssignedCriteria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField ReqestedFrom = (HiddenField)e.Row.FindControl("hdnReqestedFrom");
                TextBox txtRemark = (TextBox)e.Row.FindControl("txtRemark");
                ImageButton ImgSaveEvalFeedback = (ImageButton)e.Row.FindControl("ImgSaveEvalFeedback");
                HiddenField hdnStaff_RemarkID = (HiddenField)e.Row.FindControl("hdnStaff_RemarkID");

                if (UDFLib.ConvertIntegerToNull(ReqestedFrom.Value) == GetSessionUserID() && hdnStaff_RemarkID.Value.ToString() == "")
                {
                    txtRemark.Enabled = true;
                    ImgSaveEvalFeedback.Visible = true;
                }
                else
                {
                    txtRemark.Enabled = false;
                    ImgSaveEvalFeedback.Visible = false;
                }

            }
        }
        catch
        {
        }
    }

    protected void Load_CrewEvaluationFeedbackRequest()
    {
        try
        {
            string btnColor = "";
            int ID = UDFLib.ConvertToInteger(Request.QueryString["ID"].ToString());
            int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"].ToString());
            int Office_ID = UDFLib.ConvertToInteger(Request.QueryString["Office_ID"].ToString());

            DataTable dt = BLL_Crew_Evaluation.Get_CrewEvaluationFeedbackRequest_Details(ID, Vessel_ID, Office_ID);

            GridView_CrewEvaluationFeedbackRequest.DataSource = dt;
            GridView_CrewEvaluationFeedbackRequest.DataBind();

            if (BLL_Crew_Evaluation.Get_CrewEvaluation_FeedbackCount(GetSessionUserID(), Convert.ToInt32(ID), Convert.ToInt32(Office_ID), Convert.ToInt32(Vessel_ID)) > 0)
            {
                btnColor = "yellow";
            }

            string Show_Dashboard1 = String.Format("try{{OpenFeedback();parent.AsyncFeedbackHistory();parent.ChangeBtnColor('" + btnColor + "');}}catch(exp){{}}");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", Show_Dashboard1, true);
        }
        catch (Exception ex)
        {
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void btnSendForFeedback_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlFeedbackFrom.SelectedValue.ToString() != "0")
            {
                if (UDFLib.ConvertDateToNull(UDFLib.ConvertToDefaultDt(txtDueDate.Text)) >= DateTime.Now.Date && UDFLib.ConvertDateToNull(UDFLib.ConvertToDefaultDt(txtDueDate.Text)) != null)
                {
                    if (!UDFLib.DateCheck(txtDueDate.Text))
                    {
                        lblMsg.Text = "**Enter Valid Due Date"+TodayDateFormat;
                        return;
                    }

                    if (txtReqComment.Text.Trim() != "")
                    {
                        int ID = UDFLib.ConvertToInteger(Request.QueryString["ID"].ToString());
                        int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"].ToString());
                        int Office_ID = UDFLib.ConvertToInteger(Request.QueryString["Office_ID"].ToString());
                        int Requested_From = Convert.ToInt32(ddlFeedbackFrom.SelectedValue);
                        int? FeedbackCategory_ID = null;
                        string Evaluation_ID = Request.QueryString["Evaluation_ID"].ToString();
                        string Month = Request.QueryString["Month"].ToString();
                        string Schedule_ID = Request.QueryString["Schedule_ID"].ToString();
                        int Crew_ID = UDFLib.ConvertToInteger(Request.QueryString["Crew_ID"].ToString());

                        BLL_Crew_Evaluation.INS_CrewEvaluationFeedbackRequest_Details(ID, Vessel_ID, Office_ID, GetSessionUserID(), Requested_From, UDFLib.ConvertDateToNull(txtDueDate.Text), Evaluation_ID, Month, Schedule_ID, Crew_ID, txtReqComment.Text.Trim(), FeedbackCategory_ID);
                        Load_CrewEvaluationFeedbackRequest();
                        ddlFeedbackFrom.SelectedValue = "0";
                        txtDueDate.Text = "";
                        lblMsg.Text = "";
                        txtReqComment.Text = "";
                    }
                    else
                    {
                        lblMsg.Text = "**Requestor's comment cannot be empty.";
                        return;
                    }
                }
                else
                {
                    if (!UDFLib.DateCheck(txtDueDate.Text))
                    {
                        lblMsg.Text = "**Enter Valid Due Date"+TodayDateFormat;
                        return;
                    }
                    lblMsg.Text = "**Due Date cannot be empty and less than today's date.";
                    return;
                }
            }
            else
            {
                lblMsg.Text = "**Feedback From cannot be empty.";
                return;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void onSave(object source, CommandEventArgs e)
    {
        if (e.CommandArgument.ToString() != "")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            int ID = Convert.ToInt32(commandArgs[0]);
            int index = Convert.ToInt32(commandArgs[1]);
            string Remarks = (GridView_CrewEvaluationFeedbackRequest.Rows[index].Cells[5].FindControl("txtRemark") as TextBox).Text.Trim();
            int Feedbackcategory = int.Parse((GridView_CrewEvaluationFeedbackRequest.Rows[index].Cells[6].FindControl("ddlFeedbackCategory_Feedback") as DropDownList).SelectedValue);
            if (Remarks != "" && Feedbackcategory > 0)
            {
                int Crew_ID = UDFLib.ConvertToInteger(Request.QueryString["Crew_ID"].ToString());

                string Evaluation_ID = Request.QueryString["Evaluation_ID"].ToString();
                string Month = Request.QueryString["Month"].ToString();
                string Schedule_ID = Request.QueryString["Schedule_ID"].ToString();

                BLL_Crew_Evaluation.INS_CrewEvaluationFeedback(ID, Crew_ID, Remarks, GetSessionUserID(), Evaluation_ID, Month, Schedule_ID, null, null, null, Feedbackcategory);
                Load_CrewEvaluationFeedbackRequest();
            }
            else
            {
                string js1;
                if (Remarks == "")
                    js1 = "alert('Feedback cannot be blank!');";
                else
                    js1 = "alert('Please select Feedback Action!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js1, true);
            }
        }
    }
    protected void btnAddFeedback_Click(object sender, EventArgs e)
    {
        lblerrmsg.Text = "";
        int FeedbackCategory_ID = Convert.ToInt32(ddlFeedbackCategory_Add.SelectedValue);
        if (txtFeedback.Text.Trim() != "" && FeedbackCategory_ID > 0)
        {
            int Crew_ID = UDFLib.ConvertToInteger(Request.QueryString["Crew_ID"].ToString());

            string Evaluation_ID = Request.QueryString["Evaluation_ID"].ToString();
            string Month = Request.QueryString["Month"].ToString();
            string Schedule_ID = Request.QueryString["Schedule_ID"].ToString();
            int Dtl_Evaluation_ID = UDFLib.ConvertToInteger(Request.QueryString["ID"].ToString());
            int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"].ToString());
            int Office_ID = UDFLib.ConvertToInteger(Request.QueryString["Office_ID"].ToString());

            BLL_Crew_Evaluation.INS_CrewEvaluationFeedback(null, Crew_ID, txtFeedback.Text.Trim(), GetSessionUserID(), Evaluation_ID, Month, Schedule_ID, Dtl_Evaluation_ID, Vessel_ID, Office_ID, FeedbackCategory_ID);
            txtFeedback.Text = "";

            string js = "alert('Feedback Saved');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
        }
        else
        {
            if (txtFeedback.Text.Trim() == "")
                lblerrmsg.Text = "**Feedback cannot be empty.";
            else
                lblerrmsg.Text = "**Feedback Action is mandatory.";
        }
        Load_CrewEvaluationFeedbackRequest();
    }



   
}