using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using SMS.Business.Crew;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Crew_ProposeCrewCard : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();

        if (Session["UserID"] == null)
        {
            Response.Write("Session Expired!! Please logout and login again.");
            Response.End();
        }

        if (!IsPostBack)
        {
            Load_CrewCardStatus();
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());


        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            Response.Write("You don't have sufficient privilege to access the requested page.");
            Response.End();
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
    
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    
    protected void Load_CrewCardStatus()
    {
        int CrewID = GetCrewID();
        int CardApprover = 0;
        if (Session["UTYPE"].ToString() == "OFFICE USER" || Session["UTYPE"].ToString() == "ADMIN")
        {
            DataSet ds = objBLLCrew.Get_CrewCardStatus(CrewID, int.Parse(Session["USERID"].ToString()));

            ds.Tables[0].TableName = "Cards";
            ds.Tables[1].TableName = "Attachments";
            ds.AcceptChanges();

            DataRelation Rel = new DataRelation("CardAttachments", ds.Tables["Cards"].Columns["CardID"], ds.Tables["Attachments"].Columns["CardID"]);
            ds.Relations.Add(Rel);
            ds.AcceptChanges();

            
            rptCardDetails.DataSource = ds;
            rptCardDetails.DataMember = "Cards";
            rptCardDetails.DataBind();

            if (ds.Tables["Cards"].Rows.Count > 0)
            {
                hdnCardID.Value = ds.Tables["Cards"].Rows[0]["CardID"].ToString();

                if (ds.Tables["Cards"].Rows[0]["cardstatus"].ToString() == "PROPOSED")
                {
                    if (ds.Tables["Cards"].Rows[0]["cardtype"].ToString() == "YELLOW CARD")
                    {
                        CardApprover = objUser.CRW_CHECK_Card_Approval("Yellow_Card",int.Parse(Session["USERID"].ToString()));
                        if (CardApprover == 1)
                        {
                            pnlCardEntry.Visible = false;
                            pnlCardApprove.Visible = true;
                        }
                        else
                        {
                            pnlCardEntry.Visible = false;
                            pnlCardApprove.Visible = false;
                            lblMessage.Text = "There is a pending proposal for the crew.<br><br>You can not propose a new card unless the previous proposal is accepted or rejected by the authority";
                        }
                    }
                    else
                    {
                        CardApprover = objUser.CRW_CHECK_Card_Approval("Red_Card",int.Parse(Session["USERID"].ToString()));
                        if (CardApprover == 1)
                        {
                            pnlCardEntry.Visible = false;
                            pnlCardApprove.Visible = true;
                        }
                        else
                        {
                            pnlCardEntry.Visible = false;
                            pnlCardApprove.Visible = false;
                            lblMessage.Text = "There is a pending proposal for the crew.<br><br>You can not propose a new card unless the previous proposal is accepted or rejected by the authority";
                        }
                    }
                }
                if (ds.Tables["Cards"].Rows[0]["cardstatus"].ToString() == "ISSUED")
                {
                    if (ds.Tables["Cards"].Rows[0]["cardtype"].ToString() == "YELLOW CARD")
                    {
                        CardApprover = objUser.CRW_CHECK_Card_Approval("Red_Card", int.Parse(Session["USERID"].ToString()));
                        if (CardApprover == 1)
                        {
                            pnlCardEntry.Visible = true;
                            btnSaveAndApprove.Visible = true;
                        }
                        else
                        {
                            btnSaveAndApprove.Visible = false;
                        }
                    }
                    else
                        pnlCardEntry.Visible = false;
                }
            }
            else
            { 
                string CardType ;
                if (int.Parse(ddlCrewCardType.SelectedValue.ToString()) == 1)
                    CardType = "Yellow_Card";
                else
                    CardType = "Red_Card";
              
                CardApprover = objUser.CRW_CHECK_Card_Approval(CardType, int.Parse(Session["USERID"].ToString()));
                if (CardApprover == 1)
                    btnSaveAndApprove.Visible = true;
                else
                    btnSaveAndApprove.Visible = false;
                pnlCardDetails.Visible = false;
            }
        }
        else
        {
            lblMessage.Text = "You are not authorised to access this page.!!";
            pnlCardEntry.Visible = false;
            pnlCardApprove.Visible = false;
            pnlCardDetails.Visible = false;
        }

      
        
    }
    
    protected void btnProposeCard_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = objUploadFilesize.Get_Module_FileUpload("CWF_");
        
        try
        {
            if (txtRemarks.Text != "")
            {
                if (dt.Rows.Count > 0)
                {
                    string datasize = dt.Rows[0]["Size_KB"].ToString();
                    string FileName = "";
                    string DocName = "";
                    string FileExt = "";
                    int CardID = objBLLCrew.INS_CrewYellow_RedCard(GetCrewID(), UDFLib.ConvertToInteger(ddlCrewCardType.SelectedValue), txtRemarks.Text, GetSessionUserID());
 
                    if (FileUpload_LW.HasFile)
                    {
                        if (FileUpload_LW.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                        {
                            FileName = Path.GetFileName(FileUpload_LW.FileName);
                            
                            Guid GUID = Guid.NewGuid();
                            FileExt = Path.GetExtension(FileName).ToLower();
                            DocName = FileName.Replace(FileExt, "");
                            FileName = GUID.ToString() + FileExt;

                            string strPath = MapPath("~/Uploads/CrewDocuments/") + FileName;
                            FileUpload_LW.SaveAs(strPath);

                            objBLLCrew.INS_CrewCard_Attachment(CardID, 1, DocName, FileName, GetSessionUserID());
                            //int Ret1 = objBLLCrew.INS_CrewCard_Attachment(CardID, 1, FileName.Replace(Path.GetExtension(FileName), ""), FileName, GetSessionUserID());
                        }
                        else
                        {
                            lblMessage.Text = datasize + " KB File size exceeds maximum limit";
                            return;
                        }
                    }
                    if (FileUpload_LB.HasFile)
                    {
                        if (FileUpload_LB.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                        {
                            FileName = Path.GetFileName(FileUpload_LB.FileName);

                            Guid GUID = Guid.NewGuid();
                            FileExt = Path.GetExtension(FileName).ToLower();
                            DocName = FileName.Replace(FileExt, "");
                            FileName = GUID.ToString() + FileExt;

                            string strPath = MapPath("~/Uploads/CrewDocuments/") + FileName;
                            FileUpload_LB.SaveAs(strPath);
                            objBLLCrew.INS_CrewCard_Attachment(CardID, 2, DocName, FileName, GetSessionUserID());
                            //int Ret2 = objBLLCrew.INS_CrewCard_Attachment(CardID, 2, FileName.Replace(Path.GetExtension(FileName), ""), FileName, GetSessionUserID());
                        }
                        else
                        {
                            lblMessage.Text = datasize + " KB File size exceeds maximum limit";
                            return;
                        }
                    }
                    if (FileUpload_P.HasFile)
                    {
                        if (FileUpload_P.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                        {
                            FileName = Path.GetFileName(FileUpload_P.FileName);

                            Guid GUID = Guid.NewGuid();
                            FileExt = Path.GetExtension(FileName).ToLower();
                            DocName = FileName.Replace(FileExt, "");
                            FileName = GUID.ToString() + FileExt;

                            string strPath = MapPath("~/Uploads/CrewDocuments/") + FileName;
                            FileUpload_P.SaveAs(strPath);
                            objBLLCrew.INS_CrewCard_Attachment(CardID,3, DocName, FileName, GetSessionUserID());
                            //int Ret3 = objBLLCrew.INS_CrewCard_Attachment(CardID, 3, FileName.Replace(Path.GetExtension(FileName), ""), FileName, GetSessionUserID());
                        }
                        else
                        {
                            lblMessage.Text = datasize + " KB File size exceeds maximum limit";
                            return;
                        }
                    }
                    txtRemarks.Text = "";
                    string js = "";


                    if (CardID == -1)
                    {
                        js = "alert('A " + ddlCrewCardType.SelectedItem.Text + " is already proposed for the crew.');";
                    }
                    else if (CardID > 0)
                    {
                         js = String.Format("alert('" + ddlCrewCardType.SelectedItem.Text + " proposed for the crew.');parent.document.getElementById('ctl00_MainContent_btnRefreshCrewCardStatus').click();");
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);                           
                }
                else
                {
                    string js2 = "alert('Upload size not set!');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                }
            }
            else

                lblMessage.Text = "Please enter your Feedback and Save";
           
            }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {

        Save_CardApproval(1, "Card Proposal");
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        Save_CardApproval(-1, "Card Proposal");
    }
    //upload uncomplete
    protected void Save_CardApproval(int ApprovalStatus, string CardType)
    {
        
        try
        {
            if (txtApprovalRemark.Text != "")
            {
                string FileName = "";
                int CardID = UDFLib.ConvertToInteger(hdnCardID.Value);
                string DocName = "";
                string FileExt = "";

                if (FileUpload_A_LW.HasFile)
                {
                    FileName = Path.GetFileName(FileUpload_A_LW.FileName);

                    Guid GUID = Guid.NewGuid();
                    FileExt = Path.GetExtension(FileName).ToLower();                    
                    DocName = FileName.Replace(FileExt, "");
                    FileName = GUID.ToString() + FileExt;

                    string strPath = MapPath("~/Uploads/CrewDocuments/") + FileName;
                    FileUpload_A_LW.SaveAs(strPath);
                    int Ret1 = objBLLCrew.INS_CrewCard_Attachment(CardID, 1, DocName, FileName, GetSessionUserID());
                }
                if (FileUpload_A_LB.HasFile)
                {
                    FileName = Path.GetFileName(FileUpload_A_LB.FileName);

                    Guid GUID = Guid.NewGuid();
                    FileExt = Path.GetExtension(FileName).ToLower();
                    DocName = FileName.Replace(FileExt, "");
                    FileName = GUID.ToString() + FileExt;

                    string strPath = MapPath("~/Uploads/CrewDocuments/") + FileName;
                    FileUpload_A_LB.SaveAs(strPath);
                    int Ret2 = objBLLCrew.INS_CrewCard_Attachment(CardID, 2, DocName, FileName, GetSessionUserID());
                }
                if (FileUpload_A.HasFile)
                {
                    FileName = Path.GetFileName(FileUpload_A.FileName);

                    Guid GUID = Guid.NewGuid();
                    FileExt = Path.GetExtension(FileName).ToLower();
                    DocName = FileName.Replace(FileExt, "");
                    FileName = GUID.ToString() + FileExt;

                    string strPath = MapPath("~/Uploads/CrewDocuments/") + FileName;
                    FileUpload_A.SaveAs(strPath);
                    int Ret3 = objBLLCrew.INS_CrewCard_Attachment(CardID, 4, DocName, FileName, GetSessionUserID());
                }

                int Ret = objBLLCrew.Approve_CrewYellow_RedCard(ApprovalStatus, CardID, txtApprovalRemark.Text, GetSessionUserID());

                txtRemarks.Text = "";
                string js = "";

                if (Ret == 1)
                {
                    js = "alert('" + CardType + ((ApprovalStatus == 1) ? " Approved !!" : " Rejected !!") + "');  window.parent.document.getElementById('ctl00_MainContent_btnRefreshCrewCardStatus').click();";
                }
                if (Ret == -1)
                {
                    js = "alert('Unable to approve/reject the card proposal.');";
                }
                if (Ret == -2)
                {
                    js = "alert('Can not approve the card. Please upload Letter of Warning!!.');";
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", js, true);
            }
            else
                lblMessage.Text = "Please enter your remark and Save";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void lnkAddAttachment_Click(object sender, EventArgs e)
    {
        string FileName = "";
        int CardID = UDFLib.ConvertToInteger(hdnCardID.Value);
        DataTable dt = new DataTable();
        dt = objUploadFilesize.Get_Module_FileUpload("CWF_");
        if (dt.Rows.Count > 0)
        {
            string datasize = dt.Rows[0]["Size_KB"].ToString();
            if (Add_FileUpload_LW.HasFile)
            {
                if (Add_FileUpload_LW.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                {
                    FileName = Path.GetFileName(Add_FileUpload_LW.FileName);
                    string strPath = MapPath("~/Uploads/CrewDocuments/") + FileName;
                    Add_FileUpload_LW.SaveAs(strPath);
                    int Ret1 = objBLLCrew.INS_CrewCard_Attachment(CardID, 1, FileName.Replace(Path.GetExtension(FileName), ""), FileName, GetSessionUserID());
                }
                else
                {
                    lblMessage.Text = datasize + " KB File size exceeds maximum limit";
                    return;
                }
            }
            if (Add_FileUpload_LB.HasFile)
            {
                if (Add_FileUpload_LB.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                {
                    FileName = Path.GetFileName(Add_FileUpload_LB.FileName);
                    string strPath = MapPath("~/Uploads/CrewDocuments/") + FileName;
                    Add_FileUpload_LB.SaveAs(strPath);
                    int Ret2 = objBLLCrew.INS_CrewCard_Attachment(CardID, 2, FileName.Replace(Path.GetExtension(FileName), ""), FileName, GetSessionUserID());
                }
                else
                {
                    lblMessage.Text = datasize + " KB File size exceeds maximum limit";
                    return;
                }
            }
            if (Add_FileUpload_P.HasFile) 
            {
                if (Add_FileUpload_P.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                {
                    FileName = Path.GetFileName(Add_FileUpload_P.FileName);
                    string strPath = MapPath("~/Uploads/CrewDocuments/") + FileName;
                    Add_FileUpload_P.SaveAs(strPath);
                    int Ret3 = objBLLCrew.INS_CrewCard_Attachment(CardID, 4, FileName.Replace(Path.GetExtension(FileName), ""), FileName, GetSessionUserID());
                }
                else
                {
                    lblMessage.Text = datasize + " KB File size exceeds maximum limit";
                    return;
                }
            }
            Load_CrewCardStatus();
        }
        else
        {
            string js2 = "alert('Upload size not set!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
        }     
    }
    //upload uncomplete
    protected void btnSaveAndApprove_Click(object sender, EventArgs e)
    {
         DataTable dt = new DataTable();
        dt = objUploadFilesize.Get_Module_FileUpload("CWF_");
        try
        {
            if (txtRemarks.Text != "")
            {

                string FileName = "";
                string DocName = "";
                string FileExt = "";
                int CardID = objBLLCrew.INS_CrewYellow_RedCard(GetCrewID(), UDFLib.ConvertToInteger(ddlCrewCardType.SelectedValue), txtRemarks.Text, GetSessionUserID());


                if (FileUpload_LW.HasFile)
                {
                    FileName = Path.GetFileName(FileUpload_LW.FileName);

                    Guid GUID = Guid.NewGuid();
                    FileExt = Path.GetExtension(FileName).ToLower();
                    DocName = FileName.Replace(FileExt, "");
                    FileName = GUID.ToString() + FileExt;

                    string strPath = MapPath("~/Uploads/CrewDocuments/") + FileName;
                    FileUpload_LW.SaveAs(strPath);
                    int Ret1 = objBLLCrew.INS_CrewCard_Attachment(CardID, 1, DocName, FileName, GetSessionUserID());
                }
                if (FileUpload_LB.HasFile)
                {
                    FileName = Path.GetFileName(FileUpload_LB.FileName);
                    Guid GUID = Guid.NewGuid();
                    FileExt = Path.GetExtension(FileName).ToLower();
                    DocName = FileName.Replace(FileExt, "");
                    FileName = GUID.ToString() + FileExt;

                    string strPath = MapPath("~/Uploads/CrewDocuments/") + FileName;
                    FileUpload_LB.SaveAs(strPath);
                    int Ret2 = objBLLCrew.INS_CrewCard_Attachment(CardID, 2, DocName, FileName, GetSessionUserID());
                }
                if (FileUpload_P.HasFile)
                {
                    FileName = Path.GetFileName(FileUpload_P.FileName);
                    Guid GUID = Guid.NewGuid();
                    FileExt = Path.GetExtension(FileName).ToLower();
                    DocName = FileName.Replace(FileExt, "");
                    FileName = GUID.ToString() + FileExt;

                    string strPath = MapPath("~/Uploads/CrewDocuments/") + FileName;
                    FileUpload_P.SaveAs(strPath);
                    int Ret3 = objBLLCrew.INS_CrewCard_Attachment(CardID, 3, DocName, FileName, GetSessionUserID());
                }


                txtRemarks.Text = "";
                string js = "";


                if (CardID == -1)
                {
                    js = "alert('Unable to save the card proposal.');"; 
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                }
                else if (CardID > 0)
                {
                    int Ret = objBLLCrew.Approve_CrewYellow_RedCard(1, CardID, txtRemarks.Text, GetSessionUserID());

                    if (Ret == 1)
                    {
                        js = "alert('Crew card approved'); window.parent.document.getElementById('ctl00_MainContent_btnRefreshCrewCardStatus').click();";
                    }
                    if (Ret == -1)
                    {
                        js = "alert('Unable to approve/reject the crew card.');";
                    }
                    if (Ret == -2)
                    {
                        js = "alert('Crew card PROPOSED, but NOT APPROVED. Please upload Letter of Warning and approve again.');window.reload();";
                    }
                                        
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg2", js, true);
                }
            }
            else
                lblMessage.Text = "Please enter your remark and Save";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void btnSaveRemarks_Click(object sender, EventArgs e)
    {
        if (txtAddRemarks.Text != "")
        {
            int CardID = UDFLib.ConvertToInteger(hdnCardID.Value);
            objBLLCrew.INS_CrewCard_Remarks(CardID, txtAddRemarks.Text,"FOLLOWUP1" ,GetSessionUserID());
            txtAddRemarks.Text = "";
            string js = "alert('Remarks saved.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgremarks", js, true);
        }
    }

    protected void lnkAddRemarks_Click(object sender, EventArgs e)
    {
        pnlRemarks.Visible = true;
    }
}