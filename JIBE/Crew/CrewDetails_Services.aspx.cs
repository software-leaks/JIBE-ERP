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

public partial class Crew_CrewDetail_Services : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.Page.Header.Controls.Add(SetUserStyle.AddThemeInHeader());
            base.OnInit(e);
        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USERID"] == null)
        {
            lblMsg.Text = "Session Expired!! Log-out and log-in again.";
        }
        else
        {
            
            if (!IsPostBack)
            {
                UserAccessValidation();

                int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
                int VoyID = UDFLib.ConvertToInteger(Request.QueryString["VoyID"]);
                string Mode = Request.QueryString["Mode"];
                int CrewStatus = objBLLCrew.Get_CrewStatus(CrewID);
                if (CrewStatus == -1 && Mode == "ADD")
                {
                    btnsave.Enabled = false;
                    string js = "parent.ShowNotification('Alert','Service cannot be created for NTBR Crew',false);";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                }
                if (CrewStatus == 0 && Mode == "ADD")
                {
                    btnsave.Enabled = false;
                    string js = "parent.ShowNotification('Alert','Service cannot be created for Inactive Crew',false);";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                }
                else
                {
                    btnsave.Enabled = true;
                }

                DataTable dt = objBLLCrew.Get_CrewPersonalDetailsByID(CrewID);
                int Staff_Nationality = int.Parse(dt.Rows[0]["Staff_Nationality"].ToString());
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToString(dt.Rows[0]["Staff_Code"]) == "")
                    {
                        lblMsg.Text = "Staff is not yet APPROVED. Please approve the staff to proceed with voyage creation.";
                        ImgAddService.Visible = false;
                        pnlView_Voyages.Visible = false;
                    }
                    else
                    {
                        if (Mode == "EDIT")
                        {
                            if (objUA.Edit == 1)
                            {
                                BindRank();
                                Get_ServiceType();
                                Get_OtherServicesDtl(Convert.ToInt32(Request.QueryString["ID"]));
                                pnlAdd_Voyages.Visible = true;
                                

                            }
                        }
                        else if (Mode == "ADD")
                        {
                            if (objUA.Add == 1)
                            {
                                BindRank();
                                Get_ServiceType();
                                pnlAdd_Voyages.Visible = true;
                                
                            }
                        }
                        else
                        {
                            if (objUA.View == 1)
                            {
                                pnlView_Voyages.Visible = true;
                                Load_GridView_Voyages(CrewID); 
                            }
                        }
                       

                    }
                }
                else
                {
                    lblMsg.Text = "Staff's profile details are still not entered.";
                }
                
            }
        }

    }
    protected void UserAccessValidation()
    {
        if (Session["USERCOMPANYID"] == null)
        {
            Response.Write("Session expired!! Please log out and login again");
            Response.End();
        }
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";
        }
        if (objUA.Add == 0)
        {
            
            ImgAddService.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            
            GridView_Voyages.Columns[GridView_Voyages.Columns.Count - 3].Visible = false;
        }
        if (objUA.Delete == 0)
        {
            GridView_Voyages.Columns[GridView_Voyages.Columns.Count - 2].Visible = false;
        }
        if (objUA.Approve == 0)
        {
           
        }
        //-- MANNING OFFICE LOGIN --
        if (Session["UTYPE"] != null && Session["UTYPE"].Equals("MANNING AGENT"))
        // if (Session["USERCOMPANYID"].ToString() != "89")
        {
            GridView_Voyages.Columns[GridView_Voyages.Columns.Count - 6].Visible = false; // salary instruction
        }
        else//--- CREW TEAM LOGIN--
        {
        }


    }
    public int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void Load_GridView_Voyages(int CrewID)
    {
        DataTable dt = objBLLCrew.Get_CrewOtherServices(CrewID);
        GridView_Voyages.DataSource = dt;
        GridView_Voyages.DataBind();
    }
    protected void AddService(object sender, EventArgs e)
    {
        Get_ServiceType();
    }
    protected void Get_ServiceType()
    {
        DataTable dt = new DataTable();
        dt = objCrewAdmin.Get_ServiceType_List(null);
        ddlServiceType.DataSource = dt;
        ddlServiceType.DataTextField = "Service_Type";
        ddlServiceType.DataValueField = "SCode";
        ddlServiceType.DataBind();
        ddlServiceType.Items.Insert(0, new ListItem("-Select-", "0"));
        ddlServiceType.SelectedIndex = 0;

    
    }
    protected void Get_OtherServicesDtl(int ID)
    {
        DataTable dt = objBLLCrew.Get_CrewOtherService_Dtl(ID);
        if (dt.Rows.Count > 0)
        {

            ddlServiceType.SelectedIndex = 0;


            if (ddlServiceType.Items.FindByValue(dt.Rows[0]["SCode"].ToString()) != null)
                ddlServiceType.SelectedValue = dt.Rows[0]["SCode"].ToString() != "" ? dt.DefaultView[0]["SCode"].ToString() : "0";
            else
                ddlServiceType.SelectedValue = "0";

            if (DDLJoiningRank.Items.FindByValue(dt.Rows[0]["Rank_ID"].ToString()) != null)
                DDLJoiningRank.SelectedValue = dt.Rows[0]["Rank_ID"].ToString() != "" ? dt.DefaultView[0]["Rank_ID"].ToString() : "0";
            else
                DDLJoiningRank.SelectedValue = "0";
            if (!dt.Rows[0]["Date_From"].ToString().Equals(""))
                txtDateFrom.Text = Convert.ToDateTime(dt.Rows[0]["Date_From"]).ToString("dd/MM/yyyy");
            if (!dt.Rows[0]["Date_To"].ToString().Equals(""))
                txtDateTo.Text = Convert.ToDateTime(dt.Rows[0]["Date_To"]).ToString("dd/MM/yyyy");
            txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
            hdnServiceID.Value = dt.Rows[0]["ID"].ToString();
        }
    }
    protected void BindRank()
    {
        DataTable dtRank = new DataTable();
        dtRank = objCrewAdmin.Get_RankList();
        DDLJoiningRank.DataSource = dtRank;
        DDLJoiningRank.DataTextField = "Rank_Short_Name";
        DDLJoiningRank.DataValueField = "ID";
        DDLJoiningRank.DataBind();
        DDLJoiningRank.Items.Insert(0, new ListItem("-Select-", "0"));
        DDLJoiningRank.SelectedIndex = 0;
    }
    protected void btnsave_Click(object sender, EventArgs e)
    { 
        try
        {
            bool iValidate = true;
            string js_notify = "";
            DateTime dFrom = Convert.ToDateTime(txtDateFrom.Text);
            DateTime dTo = Convert.ToDateTime(txtDateTo.Text);
            if (dFrom > dTo)
            {
                iValidate=false;
                lblMsg.Text = "From date cannot be less than To date !!";
                
            }
            int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
            if (iValidate == true)
            {
                DataTable dt = objBLLCrew.Get_CrewOtherServices(CrewID);
                foreach (DataRow row in dt.Rows)
                {
                    if (hdnServiceID.Value != row["ID"].ToString())
                    {
                        DateTime LastToDate = Convert.ToDateTime(row["Date_To"].ToString());
                        if (dFrom < LastToDate)
                        {
                            DateTime LastFromDate = Convert.ToDateTime(row["Date_From"].ToString());
                            if (dFrom >= LastFromDate)
                            {
                                lblMsg.Text = "Two different services cannot be on same date";
                                iValidate = false;

                                break;
                            }
                            if (dTo >= LastFromDate)
                            {
                                lblMsg.Text = "Two different services cannot be on same date";
                                iValidate = false;
                                break;
                            }
                        }
                    }
                }
            }
            if (iValidate == true)
            {
                DataTable dt = objBLLCrew.Get_CrewVoyages(CrewID, 0, GetSessionUserID());
                foreach (DataRow row in dt.Rows)
                {
                    DateTime LastToDate = row["DOA_HomePort"].ToString() == "" ? row["Sign_Off_Date"].ToString() == "" ? DateTime.Today.Date : Convert.ToDateTime(row["Sign_Off_Date"].ToString()) :  Convert.ToDateTime(row["DOA_HomePort"].ToString()) ;
                    if (dFrom < LastToDate)
                    {
                        DateTime LastFromDate = Convert.ToDateTime(row["Joining_Date"].ToString());
                        if (dFrom >= LastFromDate)
                        {
                            lblMsg.Text = "Services duration cannot coincide with Voyage Duration!";
                            iValidate = false;

                            break;
                        }
                        if (dTo >= LastFromDate)
                        {
                            lblMsg.Text = "Services duration cannot coincide with Voyage Duration!";
                            iValidate = false;
                            break;
                        }
                    }
                }
            }
            if (iValidate == true)
            {                
                if (hdnServiceID.Value == "")
                {
                    int result = objBLLCrew.Insert_CrewOtherServices(UDFLib.ConvertToInteger(Request.QueryString["CrewID"]),
                                                            dFrom, dTo, Convert.ToInt32(DDLJoiningRank.SelectedValue),
                                                            ddlServiceType.SelectedValue,
                                                            GetSessionUserID(), txtRemarks.Text.Trim()
                                                            );
                    if (result == 1)
                    {
                        lblMsg.Text = "Service created successfully !!";
                        js_notify = "parent.ShowNotification('Alert','Service created successfully !!',true);";
                    }
                    js_notify = js_notify + "parent.GetOtherServices(" + Request.QueryString["CrewID"].ToString() + ");parent.hideModal('dvPopupFrame');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "InsertServices", js_notify, true);
               }
               else
               {
                    int result = objBLLCrew.Update_CrewOtherServices(Convert.ToInt32(hdnServiceID.Value),
                                                        UDFLib.ConvertToInteger(Request.QueryString["CrewID"]),
                                                        dFrom, dTo, Convert.ToInt32(DDLJoiningRank.SelectedValue),
                                                        ddlServiceType.SelectedValue,
                                                        GetSessionUserID(), txtRemarks.Text.Trim());
                    if (result == 1)
                    {
                        lblMsg.Text = "Service updated successfully !!";
                        js_notify = "parent.ShowNotification('Alert','Service updated successfully !!',true);";
                    }

                    js_notify = js_notify + "parent.GetOtherServices(" + Request.QueryString["CrewID"].ToString() + ");parent.hideModal('dvPopupFrame');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdateServices", js_notify, true);
                }

            }
            else
            {
                js_notify = "parent.ShowNotification('Alert','" + lblMsg.Text + "',true);";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "InsertVoyage", js_notify, true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to add new Service !! " + ex.Message;
        }
    }
}