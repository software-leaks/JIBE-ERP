using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.POLOG;
using System.IO;
using AjaxControlToolkit4;
using System.Text;

public partial class PO_LOG_PO_Log_Transfer_Cost : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
           

            Parent_Supply_id.Value = GetSUPPLY_ID();
            Parent_invoice_id.Value = GetInvoice_ID();
            hiddenTransfer.Value = GetTransfer_Id();
            Load_VesselList();
            BindPODetails();
            BindDropDownList();
            btnSave.Visible = true;
           
            BindGrid();
            BindReqsnAttchment();
            Status();

        }
        registerpostback();

    }
    public string GetSUPPLY_ID()
    {
        try
        {
            if (Request.QueryString["Supply_ID"] != null)
            {
                return Request.QueryString["Supply_ID"].ToString();
            }
            else
                return "0";
        }
        catch { return "0"; }
    }
    public string GetInvoice_ID()
    {
        try
        {
            if (Request.QueryString["Invoice_ID"] != null)
            {
                return Request.QueryString["Invoice_ID"].ToString();
            }
            else
                return "0";
        }
        catch { return "0"; }
    }
    public string GetTransfer_Id()
    {
        try
        {
            if (Request.QueryString["Transfer_Id"] != null)
            {
                return Request.QueryString["Transfer_Id"].ToString();
            }
            else
                return "0";
        }
        catch { return "0"; }
    }
    public void Status()
    {
        if (hiddenTransferStatus.Value.ToString() == "CONFIRM")
        {
            btnUnconfirm.Visible = true;
            btnApprove.Visible = true;
            btnDelete.Visible = true;
            btnConfirm.Visible = false;
            FileUpload1.Visible = true;
            btnView.Visible = true;
        }
        if (hiddenTransferStatus.Value.ToString() == "OPEN" || hiddenTransferStatus.Value.ToString() == "UNCONFIRM")
        {
            btnConfirm.Visible = true;
            btnUnconfirm.Visible = false;
            btnApprove.Visible = false;
            FileUpload1.Visible = true;
            btnView.Visible = true;
            btnDelete.Visible = true;
        }
        if (hiddenTransferStatus.Value.ToString() == "APPROVED")
        {
            btnDelete.Visible = false;
            btnConfirm.Visible = false;
            btnUnconfirm.Visible = false;
            FileUpload1.Visible = false;
            btnView.Visible = false;
            btnApprove.Visible = false;
            btnSave.Visible = false;
            //BindAmount();
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {

        if (FileUpload1.HasFile)
        {

            string DocType = "Invoice";
            string savePath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\Uploads\\Files_Uploaded");
            Guid GUID = Guid.NewGuid();

            string Flag_Attach = GUID.ToString() + Path.GetExtension(FileUpload1.FileName);



            string FileID = BLL_POLOG_Register.POLOG_Insert_AttachedFile(UDFLib.ConvertStringToNull(hiddenTransfer.Value), Path.GetExtension(FileUpload1.FileName),
                UDFLib.Remove_Special_Characters(Path.GetFileName(FileUpload1.FileName)), Flag_Attach, DocType, Convert.ToInt16(GetSessionUserID()));

            FileID = FileID.PadLeft(8, '0');
            string F1 = Mid(FileID, 0, 2);
            string F2 = Mid(FileID, 2, 2);
            string F3 = Mid(FileID, 4, 2);
            if (!Directory.Exists(savePath + "\\" + F1 + "\\" + F2 + "\\" + F3))
            {
                Directory.CreateDirectory(savePath + "\\" + F1 + "\\" + F2 + "\\" + F3);
            }

            string filePath = savePath + "\\" + F1 + "\\" + F2 + "\\" + F3 + "\\" + FileID + "" + Path.GetExtension(FileUpload1.FileName);
            FileUpload1.SaveAs(filePath);

            BindReqsnAttchment();
        }
        BindGrid();
    }

    public void imgbtnDelete_Click(object s, EventArgs e)
    {
        try
        {
            string[] arg = (((ImageButton)s).CommandArgument.Split(new char[] { ',' }));
            int res = BLL_POLOG_Register.POLOG_Delete_Attachments(arg[0].ToString(), arg[1].ToString(), Convert.ToInt16(GetSessionUserID()));
            BindReqsnAttchment();
            ifrmDocPreview.Attributes["src"] = "";

        }
        catch
        { }
    }
    protected void ImgView_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string FilePath = UDFLib.ConvertStringToNull(arg[0]);
        ifrmDocPreview.Attributes["src"] = FilePath;
      //  ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + FilePath + "');", true);

        BindGrid();
        //string js = "previewDocument('" + FilePath + "');";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "loadfile", js, true);
    }
    
    protected void gvReqsnAttachment_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnImg = (ImageButton)e.Row.FindControl("ImgView");
            ImageButton imgDownload = (ImageButton)e.Row.FindControl("imgDownload");

            string SavePath = ("../Uploads/Files_Uploaded");
            string File_ID = DataBinder.Eval(e.Row.DataItem, "Id").ToString();
            File_ID = File_ID.PadLeft(8, '0');
            string F1 = Mid(File_ID, 0, 2);
            string F2 = Mid(File_ID, 2, 2);
            string F3 = Mid(File_ID, 4, 2);
            string filePath = SavePath + "/" + F1 + "/" + F2 + "/" + F3 + "/" + DataBinder.Eval(e.Row.DataItem, "File_Path").ToString();
            btnImg.CommandArgument = filePath;
            imgDownload.CommandArgument = filePath;

        }
    }
    protected void ImgDownload_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string FilePath = UDFLib.ConvertStringToNull(arg[0]);

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + FilePath + "');", true);
    }
    public void BindReqsnAttchment()
    {
        //Parent_invoice_id.Value = "0000";
        string DocType = "Invoice";
        DataTable dtAttachment = BLL_POLOG_Register.POLOG_Get_Attachments(UDFLib.ConvertStringToNull(hiddenTransfer.Value), DocType);

        if (dtAttachment.Rows.Count > 0)
        {
            string FilePath = dtAttachment.Rows[0]["File_Path"].ToString();
            gvReqsnAttachment.DataSource = dtAttachment;
            gvReqsnAttachment.DataBind();
            gvReqsnAttachment.Visible = true;
            divAttachment.Visible = true;
        }
        else
        {
            gvReqsnAttachment.DataSource = dtAttachment;
            gvReqsnAttachment.DataBind();
    
            divAttachment.Visible = false;
        
        }
    }

    public static string Mid(string param, int startIndex, int length)
    {
        string result = param.Substring(startIndex, length);
        return result;
    }

    protected void BindDropDownList()
    {
        DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(GetSessionUserID()), "PO_TYPE");

        if (ds.Tables[14].Rows.Count > 0)
        {
            ddlPOType.DataSource = ds.Tables[14];
            ddlPOType.DataTextField = "VARIABLE_NAME";
            ddlPOType.DataValueField = "VARIABLE_CODE";
            ddlPOType.DataBind();
        }

        ddlOwnerCode.DataSource = ds.Tables[7];
        ddlOwnerCode.DataTextField = "Supplier_Name";
        ddlOwnerCode.DataValueField = "Supplier_Code";
        ddlOwnerCode.DataBind();
        ddlOwnerCode.Items.Insert(0, new ListItem("-Select-", "0"));


        ddlAccClassification.DataSource = ds.Tables[3];
        ddlAccClassification.DataTextField = "VARIABLE_NAME";
        ddlAccClassification.DataValueField = "VARIABLE_CODE";
        ddlAccClassification.DataBind();
        ddlAccClassification.Items.Insert(0, new ListItem("-Select-", "0"));



    }
    public void Load_VesselList()
    {
        DataTable dt = objBLL.Get_VesselList(0, 0, 0, "", Convert.ToInt32(GetCompanyID()));


        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-Select-", "0"));

    }

    private int GetCompanyID()
    {
        if (Session["USERCOMPANYID"] != null)
            return int.Parse(Session["USERCOMPANYID"].ToString());
        else
            return 0;
    }
    protected void BindPODetails()
    {
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_PO_Deatils(UDFLib.ConvertIntegerToNull(Parent_Supply_id.Value), null, UDFLib.ConvertIntegerToNull(GetSessionUserID()));

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];


                if (ddlVessel.Items.FindByValue(dr["Vessel_ID"].ToString()) != null)
                {
                    ddlVessel.SelectedValue = dr["Vessel_ID"].ToString();
                }


            }
        }
        catch { }
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            string transfer_id = Save("Add");
            hiddenTransfer.Value = transfer_id.ToString();
            if (transfer_id != "0")
            {
                string msg2 = String.Format("alert('Transfer Cost Saved.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
                BindGrid();
                divGrid.Visible = true;
                divAttachment.Visible = true;
                hiddenTransferStatus.Value = "OPEN";
                Status();
            }
            else
            {
                string msg2 = String.Format("alert('Transfer Cost is more than Invoice Amount.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            }

        

        }
        catch
        {
        }
    }
    protected string Save(string Status)
    {
        try
        {
            string transfer_id = BLL_POLOG_Invoice.POLOG_INS_Transfer_Cost(UDFLib.ConvertToInteger(Parent_Supply_id.Value), Parent_invoice_id.Value, UDFLib.ConvertToInteger(GetSessionUserID()), hiddenTransfer.Value, Status, Convert.ToDouble(txtAmount.Text),
                        Convert.ToInt32(ddlVessel.SelectedValue), txtRemarks.Text, ddlAccClassification.SelectedValue, ddlOwnerCode.SelectedValue);
            return transfer_id;
         
        }
        catch
        {
            throw;
        }
    }
    public void BindGrid()
    {
        DataSet ds = BLL_POLOG_Invoice.POLOG_Get_Transfer_Cost(hiddenTransfer.Value, Convert.ToInt32(GetSessionUserID()));
        gvTransferCost.DataSource = ds.Tables[0];
        gvTransferCost.DataBind();
        if (ds.Tables[0].Rows.Count > 0)
        {
            string Amount = ds.Tables[0].Rows[0]["Transfer_Amount"].ToString();
            Parent_invoice_id.Value = ds.Tables[0].Rows[0]["Parent_Invoice_ID"].ToString();
            Parent_Supply_id.Value = ds.Tables[0].Rows[0]["Parent_Supply_ID"].ToString();
            hiddenTransferStatus.Value = ds.Tables[0].Rows[0]["Transfer_status"].ToString();
            txtAmount.Text = (Math.Round(Convert.ToDouble(Amount))).ToString();
            ddlVessel.SelectedValue = ds.Tables[0].Rows[0]["New_Vessel_ID"].ToString();
            ddlOwnerCode.SelectedValue = ds.Tables[0].Rows[0]["New_Owner_Code"].ToString();
            ddlAccClassification.SelectedValue = ds.Tables[0].Rows[0]["New_Account_Classification"].ToString();
            txtRemarks.Text = ds.Tables[0].Rows[0]["Transfer_Description"].ToString();
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            BindPlaceHolder(ds.Tables[1]);
        }
        else
        {
            divGrid.Visible = false;
        }
        if (ds.Tables[2].Rows.Count > 0)
        {
            BindAmount(ds.Tables[2]);
        }
        
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int transfer_id = Convert.ToInt32(hiddenTransfer.Value);
        BLL_POLOG_Invoice.POLOG_Del_Transfer_Cost(transfer_id, Convert.ToInt32(GetSessionUserID()));
        string msgDraft = String.Format("window.parent.location.reload(true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);

    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {

            string transfer_id = Save("CONFIRM");
            hiddenTransfer.Value = transfer_id.ToString();
            if (transfer_id != "0")
            {
                string msg2 = String.Format("alert('Transfer Cost Confirmed.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
                BindGrid();
                divGrid.Visible = true;
                divAttachment.Visible = true;
                divAttachment.Visible = true;
                hiddenTransferStatus.Value = "CONFIRM";
                Status();
            }
            else
            {
                string msg2 = String.Format("alert('Transfer Cost is more than Invoice Amount.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            }

           
        }
        catch
        {
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string transfer_id = Save("APPROVED");
            hiddenTransfer.Value = transfer_id.ToString();
            if (transfer_id != "0")
            {
                string msg2 = String.Format("alert('Transfer Cost Approved.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
                BindGrid();
                divGrid.Visible = true;
                divAttachment.Visible = true;
                hiddenTransferStatus.Value = "APPROVED";
                Status();
            }
            else
            {
                string msg2 = String.Format("alert('Transfer Cost is more than Invoice Amount.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            }
         
        }
        catch
        {
        }
    }
    protected void btnUnconfirm_Click(object sender, EventArgs e)
    {
        try
        {

            string transfer_id = Save("UNCONFIRM");
            hiddenTransfer.Value = transfer_id.ToString();
            if (transfer_id != "0")
            {
                string msg2 = String.Format("alert('Transfer Cost Unconfirmed.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
                BindGrid();
                divGrid.Visible = true;
                divAttachment.Visible = true;
                hiddenTransferStatus.Value = "UNCONFIRM";
                Status();
            }
            else
            {
                string msg2 = String.Format("alert('Transfer Cost is more than Invoice Amount.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            }

           
        }
        catch
        {
        }
    }

    public void POLOG_UPD_Transfer_Cost()
    {
        try
        {
            BLL_POLOG_Invoice.POLOG_UPD_Transfer_Cost(hiddenTransfer.Value.ToString(), Parent_invoice_id.Value.ToString(), Convert.ToInt32(Parent_Supply_id.Value), Convert.ToInt32(GetSessionUserID()));
        }
        catch
        {
        }

    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        string msgDraft = String.Format("window.parent.location.reload(true);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        //System.Web.HttpContext.Current.Response.Write("<script>self.close();</script>");
    }

    public void BindPlaceHolder(DataTable dt)
    {
        //DataTable dt = BLL_POLOG_Invoice.POLOG_Get_Transfer_Cost(Convert.ToInt32(hiddenTransfer.Value), Convert.ToInt32(GetSessionUserID())).Tables[0];
        if (dt.Rows.Count > 0)
        {

            StringBuilder htmlTable = new StringBuilder();
            htmlTable.Append("<table border='1' >");
            htmlTable.Append("<tr><th bgcolor='DEDEDE'>Links</th><th bgcolor='DEDEDE'>Parent</th><th bgcolor='DEDEDE'>Child</th><th bgcolor='DEDEDE'>New</th></tr>");


            {

                {
                    htmlTable.Append("<tr>");
                    htmlTable.Append("<td Style='Width:100px; Text-Align:Center;'>" + "Supply Id" + "</td>");
                    htmlTable.Append("<td Style='Width:100px; Text-Align:Center;'>" + dt.Rows[0]["Parent_Supply_Id"].ToString() + "</td>");
                    htmlTable.Append("<td Style='Width:100px; Text-Align:Center;'>" + dt.Rows[0]["Child_Supply_Id"].ToString() + "</td>");
                    htmlTable.Append("<td Style='Width:100px; Text-Align:Center;'>" + dt.Rows[0]["New_Supply_Id"].ToString() + "</td>");
                    htmlTable.Append("</tr>");
                    htmlTable.Append("<tr>");
                    htmlTable.Append("<td Style='Width:100px; Text-Align:Center;'>" + "Invoice Id" + "</td>");
                    htmlTable.Append("<td Style='Width:100px; Text-Align:Center;'>" + dt.Rows[0]["Parent_Invoice_Id"].ToString() + "</td>");
                    htmlTable.Append("<td Style='Width:100px; Text-Align:Center;'>" + dt.Rows[0]["Child_Invoice_Id"].ToString() + "</td>");
                    htmlTable.Append("<td Style='Width:100px; Text-Align:Center;'>" + dt.Rows[0]["New_Invoice_Id"].ToString() + "</td>");
                    htmlTable.Append("</tr>");
                    htmlTable.Append("<tr>");
                    htmlTable.Append("<td Style='Width:100px; Text-Align:Center;'>" + "Budget" + "</td>");
                    htmlTable.Append("<td Style='Width:100px; Text-Align:Center;'>" + dt.Rows[0]["Parent_Account_Classification"].ToString() + "</td>");
                    htmlTable.Append("<td Style='Width:100px; Text-Align:Center;'>" + dt.Rows[0]["Parent_Account_Classification"].ToString() + "</td>");
                    htmlTable.Append("<td Style='Width:100px; Text-Align:Center;'>" + dt.Rows[0]["New_Account_Classification"].ToString() + "</td>");
                    htmlTable.Append("</tr>");
                }
                htmlTable.Append("</table>");

                PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
            }
        }

    }

    public void BindAmount(DataTable dt)
    {
        try
        {

            if (!string.IsNullOrEmpty(dt.Rows[0]["Invoice_Amount"].ToString()))
            {
                StringBuilder htmlTable = new StringBuilder();
                htmlTable.Append("<table border='1' >");
                htmlTable.Append("<tr><td Rowspan=2 bgcolor='DEDEDE'  Style='Width:70px;Text-Align:Center;font-weight: bolder;'>Summary</td><th bgcolor='DEDEDE'>Invoice Amount</th><th bgcolor='DEDEDE'>Total Allocated</th><th bgcolor='DEDEDE'>Available</th></tr>");
                {
                    {
                        htmlTable.Append("<tr>");
                        htmlTable.Append("<td  Style='Width:100px; Text-Align:Center;'>" + dt.Rows[0]["Invoice_Amount"].ToString() + "</td>");
                        htmlTable.Append("<td Style='Width:100px; Text-Align:Center;'>" + dt.Rows[0]["Transfer_Amount"].ToString() + "</td>");
                        htmlTable.Append("<td  Style='Width:100px; Text-Align:Center;'>" + dt.Rows[0]["Balance_Amount"].ToString() + "</td>");
                        htmlTable.Append("</tr>");
                        htmlTable.Append("</table>");
                        PlaceHolder2.Controls.Add(new Literal { Text = htmlTable.ToString() });
                    }

                }
            }
        }
        catch
        {
        }
    }
    protected void registerpostback()
    {
        foreach (GridViewRow row in gvReqsnAttachment.Rows)
        {
            ImageButton lnkFull = row.FindControl("ImgView") as ImageButton;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkFull);


        }
    }
}


