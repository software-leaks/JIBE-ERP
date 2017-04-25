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


public partial class CrewQuery_CrewQuery_Details : System.Web.UI.Page
{
    decimal TotalRequestedUSD = 0;
    decimal TotalApprovedUSD = 0;
    int PendingCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");
        else
            UserAccessValidation();

        TotalRequestedUSD = 0;
        TotalApprovedUSD = 0;
        PendingCount = 0;

        if (!IsPostBack)
        {
            Load_Crew_Query_Details();

        }
        //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
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
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        UserAccess objUA = new UserAccess();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

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
        }
    }

    protected void Load_Crew_Query_Details()
    {
        int QueryID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);
        int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"]);

        DataSet ds = BLL_Crew_Queries.Get_CrewQuery_Details(QueryID, Vessel_ID, GetSessionUserID());

        frmDetails.DataSource = ds.Tables[0];
        frmDetails.DataBind();

        ((Repeater)frmDetails.FindControl("rptClaims")).DataSource = ds.Tables[1];
        ((Repeater)frmDetails.FindControl("rptClaims")).DataBind();

        ((Repeater)frmDetails.FindControl("rptFollowUps")).DataSource = ds.Tables[2];
        ((Repeater)frmDetails.FindControl("rptFollowUps")).DataBind();

        ((Repeater)frmDetails.FindControl("rptAttachments")).DataSource = ds.Tables[3];
        ((Repeater)frmDetails.FindControl("rptAttachments")).DataBind();

    }

    protected void btnSaveFollowUp_Click(object sender, EventArgs e)
    {
        int QueryID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);
        int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"]);
        //int Send_To_Ship = chkSendToShip.Checked == true?1:0;

        int Res = BLL_Crew_Queries.INSERT_CrewQuery_FollowUp(QueryID, Vessel_ID, txtFollowUp.Text, 0, GetSessionUserID());
        if (Res > 0)
        {
            string js = "alert('FollowUp added!!)";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript1", js, true);

            txtFollowUp.Text = "";
            Load_Crew_Query_Details();
        }
    }

    protected void rptClaims_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType.ToString() == "Item" || e.Item.ItemType.ToString() == "AlternatingItem")
        {
            TotalRequestedUSD += UDFLib.ConvertToDecimal(DataBinder.Eval(e.Item.DataItem, "Requested_US_Amount").ToString());
            TotalApprovedUSD += UDFLib.ConvertToDecimal(DataBinder.Eval(e.Item.DataItem, "Approved_US_Amount").ToString());
            PendingCount += UDFLib.ConvertToInteger(DataBinder.Eval(e.Item.DataItem, "Claim_Status").ToString()) == 0 ? 1 : 0; ;
        }
        if (e.Item.ItemType.ToString() == "Footer")
        {
            ((Label)e.Item.FindControl("lblTotalRequestedUSD")).Text = TotalRequestedUSD.ToString();
            ((Label)e.Item.FindControl("lblTotalApprovedUSD")).Text = TotalApprovedUSD.ToString();

            Button btnApproveClaims = (Button)e.Item.FindControl("btnApproveClaims");
            Button btnRejectClaims = (Button)e.Item.FindControl("btnRejectClaims");

            if (PendingCount > 0)
            {
                btnApproveClaims.Visible = true;
                btnRejectClaims.Visible = true;
            }
            else
            {
                btnApproveClaims.Visible = false;
                btnRejectClaims.Visible = false;
            }
        }
    }

    protected void btnApproveClaims_Click(object sender, EventArgs e)
    {
        int QueryID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);
        int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"]);

        Repeater rptClaims = ((Repeater)frmDetails.FindControl("rptClaims"));
        decimal Req_USD = 0;
        decimal Apr_USD = 0;

        DataTable Claims = new DataTable();
        Claims.Columns.Add("PID", typeof(int));
        Claims.Columns.Add("VALUE", typeof(string));

        foreach (RepeaterItem oItem in rptClaims.Items)
        {
            if (oItem.ItemType.ToString() == "Item" || oItem.ItemType.ToString() == "AlternatingItem")
            {
                CheckBox chkApproval = (CheckBox)oItem.FindControl("chkApproval");
                Label lblRequestedUSD = (Label)oItem.FindControl("lblRequestedUSD");
                TextBox txtApprovedUSD = (TextBox)oItem.FindControl("txtApprovedUSD");
                HiddenField hdnClaimID = (HiddenField)oItem.FindControl("hdnClaimID");

                if (chkApproval != null)
                {
                    if (chkApproval.Checked == true)
                    {
                        if (lblRequestedUSD != null)
                            Req_USD = UDFLib.ConvertToDecimal(lblRequestedUSD.Text);

                        if (lblRequestedUSD != null)
                            Apr_USD = UDFLib.ConvertToDecimal(txtApprovedUSD.Text);

                        if (hdnClaimID != null)
                            Claims.Rows.Add(UDFLib.ConvertToInteger(hdnClaimID.Value), Apr_USD);
                    }
                }
            }
        }

        if (Claims.Rows.Count > 0)
        {
            int Res = BLL_Crew_Queries.Approve_CrewQuery_Claims(QueryID, Vessel_ID, Claims, GetSessionUserID());
            Load_Crew_Query_Details();
            if (Res > 0)
            {

                string js = "alert('Selected claims approved !!)";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript1", js, true);
            }
        }
    }

    protected void btnRejectClaims_Click(object sender, EventArgs e)
    {
        int QueryID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);
        int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"]);

        Repeater rptClaims = ((Repeater)frmDetails.FindControl("rptClaims"));
        decimal Apr_USD = 0;

        DataTable Claims = new DataTable();
        Claims.Columns.Add("PID", typeof(int));
        Claims.Columns.Add("VALUE", typeof(string));

        foreach (RepeaterItem oItem in rptClaims.Items)
        {
            if (oItem.ItemType.ToString() == "Item" || oItem.ItemType.ToString() == "AlternatingItem")
            {
                CheckBox chkApproval = (CheckBox)oItem.FindControl("chkApproval");
                HiddenField hdnClaimID = (HiddenField)oItem.FindControl("hdnClaimID");

                if (chkApproval != null)
                {
                    if (chkApproval.Checked == true)
                    {
                        if (hdnClaimID != null)
                            Claims.Rows.Add(UDFLib.ConvertToInteger(hdnClaimID.Value), Apr_USD);
                    }
                }
            }
        }

        if (Claims.Rows.Count > 0)
        {
            int Res = BLL_Crew_Queries.Reject_CrewQuery_Claims(QueryID, Vessel_ID, Claims, GetSessionUserID());
            Load_Crew_Query_Details();
            if (Res > 0)
            {

                string js = "alert('Selected claims rejected !!)";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript1", js, true);
            }
        }
    }

    protected void rptClaims_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        //int QueryID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);
        //int Vessel_ID = UDFLib.ConvertToInteger(Request.QueryString["VID"]);
        //int ClaimID = UDFLib.ConvertToInteger(e.CommandArgument);

        //DataTable dt = BLL_Crew_Queries.Get_Claim_Attachments(QueryID, Vessel_ID, ClaimID, GetSessionUserID());
        //if (dt.Rows.Count == 1)
        //{
        //    OpenFileExternal("../Uploads/CrewQuery/" + dt.Rows[0]["Attachment_Path"].ToString());
        //}
        //else{
        //    rptClaimAttachments.DataSource = dt;
        //    rptClaimAttachments.DataBind();

        //    string js = "showModal('#dvPopupAttachments');";
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript3", js, true);
        //}
    }

    public void OpenFileExternal(string url)
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
}