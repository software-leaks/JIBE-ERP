using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using SMS.Properties;
using System.Data;
using AjaxControlToolkit4;
using System.IO;

public partial class Crew_CrewMedicalHistory : System.Web.UI.Page
{
    decimal TotalUSDAmt = 0;
    decimal TotalLocalAmt = 0;
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    public string DFormat = "";
    public string TodayDateFormat = ""; 
    protected void Page_Load(object sender, EventArgs e)
    {
        DFormat = CalendarExtender1.Format = CalendarExtender2.Format = CalendarExtender5.Format = UDFLib.GetDateFormat();
        TodayDateFormat = UDFLib.DateFormatMessage();
        if (!CostItemUploader.IsInFileUploadPostBack)
        {

            if (GetSessionUserID() == 0)
            {
                lblMsg.Text = "Session Expired!! Log-out and log-in again.";
                pnlAddMedHistory.Visible = false;
            }
            else
                UserAccessValidation();

            TotalLocalAmt = 0;
            TotalUSDAmt = 0;


            if (!IsPostBack)
            {
                Session["MedicalCostItemAttachments"] = null;

                int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
                int Case_ID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);
                int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"]);
                int Office_ID = UDFLib.ConvertToInteger(Request.QueryString["Office_ID"]);

                Load_Crew_Info(CrewID);
                if (objUA.Edit > 0)
                {
                    Load_Med_History_Types();
                    Load_Med_History_CostItem_Types();
                    Load_Currency_List();
                }

                if (Case_ID > 0)
                {
                    hdnCaseID.Value = Case_ID.ToString();
                    hdnVesselId.Value = Vessel_ID.ToString();
                    hdnOfficeId.Value = Office_ID.ToString();

                    pnlCostItems.Visible = true;
                    pnlFollowups.Visible = true;
                    MultiView_MedHistory.ActiveViewIndex = 1;

                    Load_Crew_MedHistory_Details(Case_ID, Vessel_ID, Office_ID);
                }
                else
                {
                    hdnCaseID.Value = "";
                    hdnVesselId.Value = "";
                    hdnOfficeId.Value = "";
                    pnlCostItems.Visible = false;
                    pnlFollowups.Visible = false;
                    MultiView_MedHistory.ActiveViewIndex = 0;
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

    protected void UserAccessValidation()
    {

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";
            pnlAddMedHistory.Visible = false;
        }
        if (objUA.Add == 0)
        {
            lnkAddCostItem.Visible = false;
            lnkAddFollowUp.Visible = false;
            pnlFileUpload.Visible = false;
            btnSave.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            lnkEditDetails.Visible = false;
            lnkAddCostItem.Visible = false;
            lnkAddFollowUp.Visible = false;
            pnlFileUpload.Visible = false;
            btnSave.Visible = false;
        }
        if (objUA.Delete == 0)
        {
        }
        if (objUA.Approve == 0)
        {
        }
    }

    protected void Load_Crew_MedHistory_Details(int Case_ID, int Vessel_ID, int Office_ID)
    {

        DataSet ds = BLL_Crew_MedHistory.Get_Crew_MedHistory_Details(Case_ID, Vessel_ID, Office_ID, GetSessionUserID());
        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDate_Of_Creation.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(ds.Tables[0].Rows[0]["Case_Date"]));
                ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["StatusID"].ToString();
                ddlType.SelectedValue = ds.Tables[0].Rows[0]["CASE_TYPE_ID"].ToString();
                txtDetails.Text = ds.Tables[0].Rows[0]["CASE_DETAIL"].ToString();
                ddlVoyages.SelectedValue = ds.Tables[0].Rows[0]["VoyageID"].ToString();


                lblDate_Of_Creation.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(ds.Tables[0].Rows[0]["CASE_DATE"]));
                lblToDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(ds.Tables[0].Rows[0]["CASE_TO_DATE"]));
                lblStatus.Text = ds.Tables[0].Rows[0]["CASE_STATUS"].ToString();
                lblType.Text = ds.Tables[0].Rows[0]["CASE_TYPE"].ToString();
                lblDetails.Text = ds.Tables[0].Rows[0]["CASE_DETAIL"].ToString();
                lblVoyageName.Text = ds.Tables[0].Rows[0]["Voyage_Name"].ToString();

                pnlCostItems.Visible = true;
                pnlFollowups.Visible = true;

            }
        }
        catch { }

        rptCostItems.DataSource = ds.Tables[1];
        rptCostItems.DataBind();

        rptFollowUps.DataSource = ds.Tables[2];
        rptFollowUps.DataBind();

        rptAttachments.DataSource = ds.Tables[3];
        rptAttachments.DataBind();

    }

    protected void Load_Med_History_Types()
    {
        DataTable dtType = BLL_Crew_MedHistory.Get_Crew_MedHistory_Types(GetSessionUserID());
        ddlType.DataSource = dtType;
        ddlType.DataTextField = "Name";
        ddlType.DataValueField = "Code";
        ddlType.DataBind();
        ddlType.Items.Insert(0, new ListItem("- SELECT -", "0"));

    }

    protected void Load_Med_History_CostItem_Types()
    {
        DataTable dtType = BLL_Crew_MedHistory.Get_Crew_MedHistory_CostItem_Types(GetSessionUserID());
        ddlExpType.DataSource = dtType;
        ddlExpType.DataTextField = "COST_TYPE";
        ddlExpType.DataValueField = "COST_TYPE_ID";
        ddlExpType.DataBind();
        ddlExpType.Items.Insert(0, new ListItem("- SELECT -", "0"));

    }

    protected void Load_Currency_List()
    {
        BLL_Infra_Currency objCurr = new BLL_Infra_Currency();

        DataTable dtCurr = objCurr.Get_CurrencyList();
        ddlLocalCurr.DataSource = dtCurr;
        ddlLocalCurr.DataTextField = "Currency_Code";
        ddlLocalCurr.DataValueField = "Currency_ID";
        ddlLocalCurr.DataBind();
        ddlLocalCurr.Items.Insert(0, new ListItem("- SELECT -", "0"));

    }

    protected void Load_Crew_Info(int CrewID)
    {
        try
        {
            BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
            DataTable dt = objCrew.Get_CrewPersonalDetailsByID(CrewID);

            if (dt.Rows.Count > 0)
            {
                lblStaff_FullName.Text = dt.Rows[0]["Staff_FullName"].ToString();
                lnkStaff_Code.Text = dt.Rows[0]["Staff_Code"].ToString();
                lnkStaff_Code.NavigateUrl = "CrewDetails.aspx?ID=" + dt.Rows[0]["ID"].ToString();

                lblStaff_FullName_View.Text = dt.Rows[0]["Staff_FullName"].ToString();
                lnkStaff_Code_View.Text = dt.Rows[0]["Staff_Code"].ToString();
                lnkStaff_Code_View.NavigateUrl = "CrewDetails.aspx?ID=" + dt.Rows[0]["ID"].ToString();
            }

            DataTable dtVoy = objCrew.Get_CrewVoyages(CrewID);
            for (int i = 0; i < dtVoy.Rows.Count; i++)
            {
                ListItem lst = new ListItem();
                string textField = Convert.ToString(dtVoy.Rows[i]["Vessel_Short_Name"]) == "" ? "" : Convert.ToString(dtVoy.Rows[i]["Vessel_Short_Name"]) + " : ";
                textField += Convert.ToString(dtVoy.Rows[i]["Sign_On_Date"]) == "" ? " " : UDFLib.ConvertUserDateFormat(Convert.ToString(dtVoy.Rows[i]["Sign_On_Date"])) + " ";
                textField += Convert.ToString(dtVoy.Rows[i]["Sign_Off_Date"]) == "" ? Convert.ToString(dtVoy.Rows[i]["Sign_On_Date"]) == "" ? "Current" : "- Current " : " - " + UDFLib.ConvertUserDateFormat(Convert.ToString(dtVoy.Rows[i]["Sign_Off_Date"]));

                lst.Text = textField;
                lst.Value = dtVoy.Rows[i]["ID"].ToString();
                ddlVoyages.Items.Add(lst);
                ddlVoyages.DataBind();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void lnkEditDetails_Click(object sender, EventArgs e)
    {
        MultiView_MedHistory.ActiveViewIndex = 0;
    }

    protected void lnkCancelEditDetails_Click(object sender, EventArgs e)
    {
        MultiView_MedHistory.ActiveViewIndex = 1;
    }

    protected void lnkSaveDetails_Click(object sender, EventArgs e)
    {
        btnSave_Click(null, null);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int Res = 0;
            int Case_ID = UDFLib.ConvertToInteger(hdnCaseID.Value);
            int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
            int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"]);
            int Office_ID = UDFLib.ConvertToInteger(Request.QueryString["Office_ID"]);

            if (Case_ID > 0)
            {
                if (txtDetails.Text.Trim() == "")
                {
                    string js1 = "alert('Please enter medical history details!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "js1", js1, true);
                }
                else if (txtDate_Of_Creation.Text.Trim() == "")
                {
                    string js2 = "alert('Please enter medical history date!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "js2", js2, true);
                    UpdatePanel1.Update();
                }
                else
                {
                    Res = BLL_Crew_MedHistory.UPDATE_Crew_MedHistory(Case_ID, UDFLib.ConvertToInteger(ddlVoyages.SelectedValue), CrewID, UDFLib.ConvertToInteger(ddlType.SelectedValue), UDFLib.ConvertToInteger(ddlStatus.SelectedValue), txtDetails.Text, UDFLib.ConvertToDefaultDt(txtDate_Of_Creation.Text), GetSessionUserID(), UDFLib.ConvertToDefaultDt(txtToDate.Text), Office_ID);
                    if (Res > 0)
                    {
                        string js = "alert('Medical History updated!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "updated", js, true);

                        txtFollowUp.Text = "";
                        Load_Crew_MedHistory_Details(Case_ID, Vessel_ID, Office_ID);
                    }
                    MultiView_MedHistory.ActiveViewIndex = 1;
                }
            }
            else
            {
                if (txtDetails.Text.Trim() == "")
                {
                    string js1 = "alert('Please enter medical history details!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "js1", js1, true);
                }
                else if (txtDate_Of_Creation.Text.Trim() == "")
                {
                    string js2 = "alert('Please enter medical history date!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "js2", js2, false);
                    UpdatePanel1.Update();
                }
                else
                {
                    DataTable dtI = BLL_Crew_MedHistory.INSERT_Crew_MedHistory(UDFLib.ConvertToInteger(ddlVoyages.SelectedValue), CrewID, UDFLib.ConvertToInteger(ddlType.SelectedValue), UDFLib.ConvertToInteger(ddlStatus.SelectedValue), txtDetails.Text, UDFLib.ConvertToDefaultDt(txtDate_Of_Creation.Text), GetSessionUserID(), UDFLib.ConvertToDefaultDt(txtToDate.Text));
                    if (dtI.Rows.Count > 0)
                    {
                        int caseID = Convert.ToInt32(dtI.Rows[0][0].ToString());
                        int VesselID = Convert.ToInt32(dtI.Rows[0][1].ToString());
                        hdnCaseID.Value = Res.ToString();
                        Load_Crew_MedHistory_Details(caseID, VesselID, Office_ID);
                        string js = "alert('Medical History added!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "inserted", js, true);
                        txtFollowUp.Text = "";

                        MultiView_MedHistory.ActiveViewIndex = 1;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnSaveFollowUp_Click(object sender, EventArgs e)
    {
        try
        {
            int Case_ID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);
            int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"]);
            int Office_ID = UDFLib.ConvertToInteger(Request.QueryString["Office_ID"]);

            int Res = BLL_Crew_MedHistory.INSERT_Crew_MedHistory_FollowUp(Case_ID, Vessel_ID, Office_ID, txtFollowUp.Text, GetSessionUserID());
            if (Res > 0)
            {
                string js = "alert('FollowUp added!!)";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript1", js, true);

                txtFollowUp.Text = "";
                Load_Crew_MedHistory_Details(Case_ID, Vessel_ID, Office_ID);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void rptCostItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType.ToString() == "Item" || e.Item.ItemType.ToString() == "AlternatingItem")
        {
            TotalUSDAmt += UDFLib.ConvertToDecimal(DataBinder.Eval(e.Item.DataItem, "USD_Amount").ToString());

            TotalLocalAmt += UDFLib.ConvertToDecimal(DataBinder.Eval(e.Item.DataItem, "Local_Amount").ToString());
        }
        if (e.Item.ItemType.ToString() == "Footer")
        {
            ((Label)e.Item.FindControl("lblTotal")).Text = TotalLocalAmt.ToString();
            ((Label)e.Item.FindControl("lblTotalUSD")).Text = TotalUSDAmt.ToString();
        }
    }

    protected void rptCostItems_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int ID = UDFLib.ConvertToInteger(e.CommandArgument);
            int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"]);
            int Office_ID = UDFLib.ConvertToInteger(Request.QueryString["Office_ID"]);

            BLL_Crew_MedHistory.DELETE_Med_Cost_Item(ID, UDFLib.ConvertToInteger(hdnCaseID.Value), Vessel_ID, Office_ID, GetSessionUserID());

            Load_Crew_MedHistory_Details(UDFLib.ConvertToInteger(hdnCaseID.Value), Vessel_ID, Office_ID);
        }
    }

    protected void btnSaveCostItem_Click(object sender, EventArgs e)
    {
        try
        {
            string js;
            if (txtExp_Date.Text.Trim().Length == 0)
            {

                js = "alert('Date is not Valid!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                js = "showModal('dvPopupCostItem');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "updated1", js, true);
                UpdatePanel2.Update();
                return;
            }
            else
            {
                try
                {
                    DateTime dt = DateTime.Parse(UDFLib.ConvertToDefaultDt(txtExp_Date.Text));

                }
                catch
                {

                    js = "alert('Enter valid  Date" + TodayDateFormat + "')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "scripts", js, true);
                    js = "showModal('dvPopupCostItem');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
                    UpdatePanel2.Update();
                    return;
                }
            }
            if (txtDesc.Text.ToString().Length == 0 || ddlExpType.SelectedIndex <= 0 || ddlLocalCurr.SelectedIndex == 0 || txtLocalAmt.Text.Trim().Length == 0)
            {
                js = "showModal('dvPopupCostItem');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "updated1", js, true);
                UpdatePanel2.Update();
                return;
            }

            int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"]);
            int Office_ID = UDFLib.ConvertToInteger(Request.QueryString["Office_ID"]);
            int Cost_Item_ID = BLL_Crew_MedHistory.INSERT_Med_Cost_Item(UDFLib.ConvertToInteger(hdnCaseID.Value), Vessel_ID, Office_ID, UDFLib.ConvertToDefaultDt(Convert.ToString(txtExp_Date.Text)), txtDesc.Text, UDFLib.ConvertToInteger(ddlExpType.SelectedValue), UDFLib.ConvertToInteger(ddlLocalCurr.SelectedValue), UDFLib.ConvertDecimalToNull(txtLocalAmt.Text), UDFLib.ConvertDecimalToNull(txtUSDAmt.Text), GetSessionUserID());

            Upload_CostItemAttachments(Cost_Item_ID);

            Load_Crew_MedHistory_Details(UDFLib.ConvertToInteger(hdnCaseID.Value), Vessel_ID, Office_ID);

            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        finally
        {
            Session["MedicalCostItemAttachments"] = null;
        }
    }

    public void OpenFileExternal(string url)
    {
        try
        {
            string filepath = Server.MapPath(url);
            System.IO.FileInfo file = new System.IO.FileInfo(filepath);
            if (file.Exists)
            {
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Disposition", "inline; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = ReturnExtension(file.Extension.ToLower());
                Response.TransmitFile(file.FullName);
                Response.End();
            }
            else
            {
                string js = "alert('File not found at the specied location:  " + file.Name + "');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript2", js, true);

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }

    protected void btnUploadAttachments_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        try
        {
            int Case_ID = UDFLib.ConvertToInteger(hdnCaseID.Value);
            int Vessel_ID = UDFLib.ConvertToInteger(hdnVesselId.Value);
            int Office_ID = UDFLib.ConvertToInteger(hdnOfficeId.Value);

            DataTable dt = new DataTable();
            dt = objUploadFilesize.Get_Module_FileUpload("CWF_");
            if (dt.Rows.Count > 0)
            {
                string datasize = dt.Rows[0]["Size_KB"].ToString();
                if (UploadAttachments.HasFile)
                {
                    if (UploadAttachments.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                    {
                        //if (UploadAttachments.FileName.Length > 0)
                        //{

                        Guid GUID = Guid.NewGuid();

                        long SIZE_BYTES = UploadAttachments.PostedFile.ContentLength;

                        if (SIZE_BYTES > 0 && Case_ID > 0)
                        {
                            string File_Path = GUID + System.IO.Path.GetExtension(UploadAttachments.FileName);

                            UploadAttachments.PostedFile.SaveAs(Server.MapPath("~/Uploads/MedHistory/" + File_Path));

                            int FileID = BLL_Crew_MedHistory.INSERT_Crew_MedHistory_Attachment(Case_ID, Vessel_ID, Office_ID, UploadAttachments.FileName.Replace("&", "_"), File_Path, SIZE_BYTES, GetSessionUserID());


                        }

                        DataTable dtAtt = BLL_Crew_MedHistory.Get_Crew_MedHistory_Attachments(Case_ID, Vessel_ID, Office_ID, GetSessionUserID());
                        rptAttachments.DataSource = dtAtt;
                        rptAttachments.DataBind();
                    }
                    else
                    {
                        lblMsg.Text = datasize + " KB File size exceeds maximum limit";

                    }
                }
            }
            else
            {
                string js2 = "alert('Upload size not set!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }



    protected void rptAttachments_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int ID = UDFLib.ConvertToInteger(e.CommandArgument);
            int Case_ID = UDFLib.ConvertToInteger(hdnCaseID.Value);
            int Vessel_ID = UDFLib.ConvertToInteger(hdnVesselId.Value);
            int Office_ID = UDFLib.ConvertToInteger(hdnOfficeId.Value);

            BLL_Crew_MedHistory.DELETE_Crew_MedHistory_Attachment(ID, Case_ID, Vessel_ID, Office_ID, GetSessionUserID());

            DataTable dtAtt = BLL_Crew_MedHistory.Get_Crew_MedHistory_Attachments(Case_ID, Vessel_ID, Office_ID, GetSessionUserID());
            rptAttachments.DataSource = dtAtt;
            rptAttachments.DataBind();
        }
    }


    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        // User can save file to File System, database or in session state
        Dictionary<string, Byte[]> Files = new Dictionary<string, byte[]>();
        if (file != null)
        {
            if (Session["MedicalCostItemAttachments"] != null)
                Files = Session["MedicalCostItemAttachments"] as Dictionary<string, Byte[]>;

            Files.Add(file.FileName, file.GetContents());
            Session["MedicalCostItemAttachments"] = Files;
        }
    }

    protected void Upload_CostItemAttachments(int Cost_Item_ID)
    {
        try
        {
            // User can save file to File System, database or in session state
            if (Session["MedicalCostItemAttachments"] != null)
            {
                Dictionary<string, Byte[]> Files = Session["MedicalCostItemAttachments"] as Dictionary<string, Byte[]>;
                int Case_ID = UDFLib.ConvertToInteger(hdnCaseID.Value);
                int Vessel_ID = UDFLib.ConvertToInteger(hdnVesselId.Value);
                int Office_ID = UDFLib.ConvertToInteger(hdnOfficeId.Value);

                foreach (KeyValuePair<string, Byte[]> file in Files)
                {
                    Byte[] fileBytes = file.Value;
                    string FileName = file.Key;
                    int SIZE_BYTES = fileBytes.Length;

                    Guid GUID = Guid.NewGuid();
                    string FilPath = Path.Combine(Server.MapPath("~/Uploads/MedHistory/"), GUID.ToString() + Path.GetExtension(FileName));
                    string Attach_Name = Path.GetFileName(FileName);
                    string Attach_Path = GUID.ToString() + Path.GetExtension(FileName);

                    FileStream fileStream = new FileStream(FilPath, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Write(fileBytes, 0, fileBytes.Length);
                    fileStream.Close();

                    int FileID = BLL_Crew_MedHistory.INSERT_Med_Cost_Item_Attachment(Case_ID, @Cost_Item_ID, Vessel_ID, Office_ID, Attach_Name.Replace("&", "_"), Attach_Path, SIZE_BYTES, GetSessionUserID());

                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void lnkAddCostItem_Click(object sender, EventArgs e)
    {
        txtExp_Date.Text = "";
        txtDesc.Text = "";
        ddlExpType.SelectedIndex = 0;
        ddlLocalCurr.SelectedIndex = 0;
        txtLocalAmt.Text = "";
        txtUSDAmt.Text = "";
        string js = "showModal('dvPopupCostItem');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "updated1", js, true);
        UpdatePanel2.Update();
        UpdatePanel_CostItem.Update();
    }
}