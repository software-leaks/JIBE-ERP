using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.IO;


public partial class Crew_CrewDetails_RefererDetails : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    public string DFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["USERID"] == null)
            {
                lblMsg.Text = "Session Expired!! Log-out and log-in again.";
            }
            else
            {
                DFormat = UDFLib.GetDateFormat();
                CalendarExtender5.Format = DFormat;

                if (!IsPostBack)
                {
                    UserAccessValidation();
                    txtPersonQuieredName.Text = Session["USERFULLNAME"].ToString();
                    int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
                    string Mode = Request.QueryString["Mode"];
                    if (Mode == "0") //Add
                    {
                        if (objUA.Add == 1)
                        {
                            pnlAdd_RefererDetails.Visible = true;
                        }
                    }
                    else
                    {
                        if (objUA.View == 1)
                        {
                            pnlView_RefererDetails.Visible = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    public int GetCrewID()
    {
        try
        {
            if (Request.QueryString["CrewID"] != null)
            {
                return int.Parse(Request.QueryString["CrewID"].ToString());
            }
            else
                return 0;
        }
        catch { return 0; }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";

        }
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
        }
        //-- MANNING OFFICE LOGIN --
        if (Session["USERCOMPANYID"].ToString() != "1")
        {
            //Response.Redirect("~/default.aspx?msgid=2");
        }
        else//--- CREW TEAM LOGIN--
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
    protected void lnkEditRefererDetails_Click(object sender, EventArgs e)
    {
        pnlView_RefererDetails.Visible = false;
        txtReferenceDate.Text = "";
        txtPersonQuieredTitle.Text = "";
        txtRefererName.Text = "";
        txtRefererPhoneNo.Text = "";
        pnlAdd_RefererDetails.Visible = true;
    }
    protected void btnSaveRefererDetail_Click(object sender, EventArgs e)
    {
        string msg = "";
        try
        {
            string js;
            int CrewID = GetCrewID();
            DateTime Dt_ReferenceDate = DateTime.Parse(UDFLib.ConvertUserDateFormat(Convert.ToString("1900/01/01")));

            try
            {
                if (txtReferenceDate.Text != "")
                    if (!UDFLib.DateCheck(txtReferenceDate.Text))
                    {
                        js = "alert('Enter valid Reference Check Date" + UDFLib.DateFormatMessage() + "');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                        return;
                    }
                Dt_ReferenceDate = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtReferenceDate.Text.Trim()));
            }
            catch (Exception)
            {
                msg = "Enter valid Reference Check Date" + UDFLib.DateFormatMessage();
                js = "alert('" + msg + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
                return;
            }

            if ((Dt_ReferenceDate > DateTime.Today))
            {
                js = "alert('Reference Date cannot be greater than todays date.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
            }
            else if (Dt_ReferenceDate == Convert.ToDateTime("01/01/1900 "))
            {
                js = "alert('Invalid Date entry in REFERENCE CHECK DATE field.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js, true);
            }
            else
            {
                objBLLCrew.Save_Crew_Reference_Details(0, CrewID, txtRefererName.Text.Trim(), txtRefererPhoneNo.Text.Trim(), Dt_ReferenceDate.ToShortDateString(), txtPersonQuieredName.Text.Trim(), txtPersonQuieredTitle.Text.Trim(), GetSessionUserID());

                objDS_ReferenceDetail.SelectParameters["CrewID"].DefaultValue = CrewID.ToString();
                objDS_ReferenceDetail.Select();
                gdRefererDetails.DataBind();

                pnlView_RefererDetails.Visible = true;
                pnlAdd_RefererDetails.Visible = false;

                js = "parent.GetInterviewResult(" + Request.QueryString["CrewID"].ToString() + ");";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            }

        }
        catch (Exception)
        {


        }
    }
    protected void btnCancelRefererDetail_Click(object sender, EventArgs e)
    {
        pnlView_RefererDetails.Visible = true;
        pnlAdd_RefererDetails.Visible = false;
    }

    protected void gdRefererDetails_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        string js = "parent.GetInterviewResult(" + Request.QueryString["CrewID"].ToString() + ");";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
    }

    protected void gdRefererDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                TextBox txt = (TextBox)e.Row.FindControl("txtREFERENCE_DATE");

                DataRow dr = ((DataRowView)e.Row.DataItem).Row;

                if (dr["REFERENCE_DATE"].ToString() != "" && txt != null)
                {
                    txt.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dr["REFERENCE_DATE"].ToString()));

                }

            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }

        }
    }

    protected void ObjectDataSource2_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            e.InputParameters["REFERENCE_DATE"] = UDFLib.ConvertToDate(e.InputParameters["REFERENCE_DATE"].ToString()).ToShortDateString();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        
    }

}