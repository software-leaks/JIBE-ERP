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

public partial class Crew_CrewDetails_Feedback : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
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
                string Mode = Request.QueryString["Mode"];
                if (Mode == "ADD")
                {
                    if (objUA.Add == 1)
                    {
                        pnlAddFeedback.Visible = true;
                        Load_CrewVoyages(CrewID);
                    }
                }
                else
                {
                    if (objUA.View == 1)
                    {
                        pnlViewFeedbacks.Visible = true;
                        Load_CrewFeedbacks(CrewID);
                    }
                }
            }
        }
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

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void Load_CrewVoyages(int CrewID)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = objBLLCrew.Get_CrewVoyages(CrewID);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListItem lst = new ListItem();
                        string textField = Convert.ToString(dt.Rows[i]["Vessel_Short_Name"]) == "" ? "" : Convert.ToString(dt.Rows[i]["Vessel_Short_Name"]) + " : ";
                        textField += Convert.ToString(dt.Rows[i]["Sign_On_Date"]) == "" ? " " : UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[i]["Sign_On_Date"])) + " ";
                        textField += Convert.ToString(dt.Rows[i]["Sign_Off_Date"]) == "" ? Convert.ToString(dt.Rows[i]["Sign_On_Date"]) == "" ? "Current" : "- Current " : " - " + UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[i]["Sign_Off_Date"]));

                        lst.Text = textField;
                        lst.Value = dt.Rows[i]["ID"].ToString();
                        ddlVoyages.Items.Add(lst);
                    }
                }
                else
                    ddlVoyages.DataSource = null;
            }
            else
                ddlVoyages.DataSource = null;

            ddlVoyages.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void Load_CrewFeedbacks(int CrewID)
    {
        try
        {
            int SelectRecordCount = 0;
            DataTable dt = objBLLCrew.Get_CrewRemarks(CrewID, int.Parse(Session["USERID"].ToString()), 100, 1, ref SelectRecordCount);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    GridView_CrewRemarks.DataSource = dt;

                }
                else
                    GridView_CrewRemarks.DataSource = null;
            }
            else
                GridView_CrewRemarks.DataSource = null;

            GridView_CrewRemarks.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected int Save()
    {
        lblMsg.Text = "";
        try
        {
            if (txtCrewRemarks.Text != "")
            {
                string FileName = "";
                DataTable dt = new DataTable();
                dt = objUploadFilesize.Get_Module_FileUpload("CWF_");
                if (dt.Rows.Count > 0)
                {
                    string datasize = dt.Rows[0]["Size_KB"].ToString();
                    if (CrewRemarks_FileUploader.HasFile)
                    {
                        if (CrewRemarks_FileUploader.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                        {
                            FileName = Path.GetFileName(CrewRemarks_FileUploader.FileName);
                            string strPath = MapPath("~/Uploads/CrewDocuments/") + FileName;
                            CrewRemarks_FileUploader.SaveAs(strPath);
                        }
                        else
                        {
                            lblMsg.Text = datasize + " KB File size exceeds maximum limit";
                            return 0;
                        }
                    }
                    lblMsg.Text = "";
                    int VoyageID = UDFLib.ConvertToInteger(ddlVoyages.SelectedValue);
                    objBLLCrew.INS_CrewRemarks(UDFLib.ConvertToInteger(Request.QueryString["CrewID"]), txtCrewRemarks.Text, FileName, GetSessionUserID(), VoyageID);
                    return 1;
                }
                else
                {
                    string js2 = "alert('Upload size not set!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                    return 0;
                }
            }
            else

                lblMsg.Text = "Please enter your Feedback and Save";
            return 0;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            return 0;
        }
    }
    protected void btnSaveRemarks_Click(object sender, EventArgs e)
    {
        int saveStatus = Save();
        if (saveStatus == 1)
        {
            lblMsg.Text = "Feedback Saved.";
            txtCrewRemarks.Text = "";
            string js = "parent.GetCrewFeedback(" + Request.QueryString["CrewID"].ToString() + ");";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
    }
    protected void btnSaveAndCloseRemarks_Click(object sender, EventArgs e)
    {
        int saveStatus = Save();
        if (saveStatus == 1)
        {
            string js = "parent.GetCrewFeedback(" + Request.QueryString["CrewID"].ToString() + ");parent.hideModal('dvPopupFrame');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
    }
}