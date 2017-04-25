using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SMS.Business.PURC;
using Telerik.Web.UI;
using AjaxControlToolkit4;
using CrystalDecisions.Shared;
using System.IO;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Technical_INV_ItemPreview : System.Web.UI.Page
{
    public int OFFICE_ID = 0;
    public int WORKLIST_ID = 0;
    public int VESSEL_ID = 0;
    public string ReqsnCode = "";
    public string DocCode = "";
    public string FormType = "";
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    DataView dv = new DataView();

    protected void Page_Load(object sender, EventArgs e)
    {
        ReqsnCode = Convert.ToString(Request.QueryString["Requisitioncode"]);
        DocCode = Convert.ToString(Request.QueryString["Document_Code"]);

        if (!IsPostBack)
        {

            BindItmPreviewRpt();
            BindPurcQuestion();

            btnLoadFiles.Attributes.Add("style", "visibility:hidden");
            Session["VesselID"] = Request.QueryString["Vessel_Code"].ToString();
            LoadFiles(null, null);
            VESSEL_ID = Convert.ToInt32(Session["VesselID"]);

        }
    }


    // Events
    /// <summary>
    /// Change the Header SubTitle Row Back Color
    /// </summary>
    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)                                  // Change SubCatalogue Backcolor
    {
        if (groupName == "SubCatalog")
        {
            row.BackColor = System.Drawing.Color.SkyBlue;

            row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
            row.Cells[0].ForeColor = System.Drawing.Color.Black;
            row.Cells[0].Font.Bold = true;

        }

    }

    /// <summary>
    /// Delete The Requisition on Delete Button Click
    /// </summary>
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        try
        {
            int ReturenID;

            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                ReturenID = objTechService.DeleteRequisitionItem(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString());

            }

            String msg = String.Format("alert('Requisition has been deleted sucessfully.'); ");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            Response.Redirect("PendingRequisitionDetails.aspx");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }                                                        // delete Requisition

    /// <summary>
    /// Finalise the Requisition and gives the New Requisition Number Generated on Finalise Button Click
    /// </summary>
    protected void btnFinalize_Click(object sender, EventArgs e)                                                         // Finalise Requisition
    {

        string ReturnReq = "";
        int RetVal;
        DataTable dtReqDocCode = new DataTable();
        if (ValidateQuestions() == true)
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                ReturnReq = objTechService.FinalizeRequisitionItem(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString(), Request.QueryString["Vessel_Code"].ToString(), "", null, "");

                DataTable dtQuotationList = new DataTable();
                dtQuotationList.Columns.Add("Qtncode");
                dtQuotationList.Columns.Add("amount");

                RetVal = objTechService.InsertRequisitionStageStatus(ReturnReq, Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), "NRQ", "Requistion has been finalize", Convert.ToInt32(Session["USERID"]), dtQuotationList);
                SaveQuestionnaire(ReturnReq);
                stssave.Value = "1";

            }

            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Requisition has been Finalized.\\n Please Note Requisition Number:" + ReturnReq + "');window.location='PendingRequisitionDetails.aspx';", true);

        }
        else
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Please complete the Purchase Questionnaire to proceed with the purchase process');", true);
            //BindItmPreviewRpt();

        }
    }

    /// <summary>
    /// Go Back to Item Preview Page
    /// </summary>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ItemPreview.aspx");
    }

    /// <summary>
    /// Binds the Attachment Grid with the List of Attachment Previously Uploaded
    /// </summary>
    public void LoadFiles(object s, EventArgs e)                                                                        // Bind Attachment Grid
    {
        try
        {
            BLL_PURC_Purchase objAttch = new BLL_PURC_Purchase();
            gvAttachment.DataSource = objAttch.Purc_Get_Reqsn_Attachments_New(Convert.ToString(Request.QueryString["Document_Code"]), int.Parse(Request.QueryString["Vessel_Code"].ToString()));
            gvAttachment.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    /// <summary>
    ///  on attachment Delete Image  Click Deleates the File attachment uploaded
    /// </summary>
    public void imgbtnDelete_Click(object s, EventArgs e)                                                               // Delete attachment file Uploaded
    {
        try
        {

            BLL_PURC_Purchase objAttch = new BLL_PURC_Purchase();

            string[] arg = (((ImageButton)s).CommandArgument.Split(new char[] { ',' }));
            int res = objAttch.Purc_Delete_Reqsn_Attachments(int.Parse(arg[0]), int.Parse(arg[2]), int.Parse(arg[3]));
            if (res > 0)
            {
                File.Delete(Server.MapPath(((ImageButton)s).CommandArgument.Split(new char[] { ',' })[1]));
            }
            LoadFiles(null, null);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Ajax file browse and Uploade
    /// </summary>
    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)                        // Ajax File Upload
    {
        try
        {

            DataTable dta = new DataTable();
            dta = objUploadFilesize.Get_Module_FileUpload("PURC_");

            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();

            Byte[] fileBytes = file.GetContents();
            string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Purchase");
            Guid GUID = Guid.NewGuid();

            string Flag_Attach = GUID.ToString() + Path.GetExtension(file.FileName);



            int sts = objTechService.SaveAttachedFileInfo_New(Convert.ToString(Request.QueryString["Vessel_Code"]), Request.QueryString["Document_Code"], "0", Path.GetExtension(file.FileName), UDFLib.Remove_Special_Characters(Path.GetFileName(file.FileName)), "../Uploads/Purchase/" + Flag_Attach, Session["USERID"].ToString(), 0);

            string FullFilename = Path.Combine(sPath, GUID.ToString() + Path.GetExtension(file.FileName));

            FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    /// <summary>
    /// Load Files
    /// </summary>
    protected void btnLoadFiles_Click(object sender, EventArgs e)
    {
        LoadFiles(null, null);
        //BindItmPreviewRpt();
    }

    /// <summary>
    /// On Cancle Button Click Go back to PendingRequisition Page
    /// </summary>
    protected void btnCancel_Click(object sender, EventArgs e)                                                         // Go Back
    {
        Response.Redirect("PendingRequisitionDetails.aspx");
    }

    /// <summary>
    /// On Update image Click Updates the changed Items Quantity 
    /// </summary>
    protected void UpdateItemClick(object sender, EventArgs e)                                                         // Update Requisition
    {
        ImageButton ibtn = (ImageButton)sender;
        var item = (RepeaterItem)ibtn.NamingContainer;
        TextBox txtQnty = (TextBox)item.FindControl("txtQnty");
        BLL_PURC_Purchase objinvntryitem = new BLL_PURC_Purchase();
        int result = objinvntryitem.UpdateSupplyQnty(itemid.Value, Request.QueryString["Document_Code"], Convert.ToDecimal(txtQnty.Text));

        BindItmPreviewRpt();

    }

    /// <summary>
    /// On Update Image click Allow quantiy to Edit and enables the Update Button to Update
    /// </summary>
    protected void updateClick(object sender, EventArgs e)                                                            // Update Image Click
    {
        ImageButton imbtn = (ImageButton)sender;

        var item = (RepeaterItem)imbtn.NamingContainer;
        ImageButton btnupdate = (ImageButton)item.FindControl("btnUpdate");
        ImageButton btnCancel = (ImageButton)item.FindControl("ImgBtnCancel");
        ImageButton ImgUpdate = (ImageButton)item.FindControl("ImgUpdate");
        TextBox txtQnty = (TextBox)item.FindControl("txtQnty");
        txtQnty.Enabled = true;
        itemid.Value = imbtn.CommandArgument.ToString();
        btnupdate.Visible = true;
        imbtn.Visible = false;
        ImgUpdate.Visible = false;
        btnCancel.Visible = true;
    }

    /// <summary>
    /// On Cancle Image Click disables the update button and disables 
    /// quantity field with no change in quantity  // Cancle Image Click  
    /// </summary>
    protected void CancelClick(object sender, EventArgs e)
    {
        ImageButton imbtn = (ImageButton)sender;

        var item = (RepeaterItem)imbtn.NamingContainer;
        ImageButton btnupdate = (ImageButton)item.FindControl("btnUpdate");
        TextBox txtQnty = (TextBox)item.FindControl("txtQnty");
        ImageButton ImgUpdate = (ImageButton)item.FindControl("ImgUpdate");
        txtQnty.Enabled = false;
        itemid.Value = imbtn.CommandArgument.ToString();
        btnupdate.Visible = false;
        imbtn.Visible = false;
        ImgUpdate.Visible = true;


    }



    // Functions
    /// <summary>
    /// Bind the Repeater with the Requisition Items 
    /// </summary>
    private void BindItmPreviewRpt()                                                                                      // bind repeater
    {
        try
        {
            DataSet dataforDisplay = new DataSet();
            DataTable dt = (DataTable)Session["InventoryData"];
            DataTable distinctValues;
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {

                dataforDisplay = objTechService.GetReqItemsPreview(ReqsnCode, Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString());
                DataView view = new DataView(dataforDisplay.Tables[0]);
                distinctValues = view.ToTable(true, "SubCatalog", "SubCatalogID");

            }

            if (distinctValues.Rows.Count <= 0)
            {
                String msg = String.Format("alert('This Requisition has been finalized or Deleted.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            }
            else
            {
                rptMain.DataSource = distinctValues;
                rptMain.DataBind();

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }


    #region Purchase Question and Answers
    /// <summary>
    ///REturns Form Type 
    /// </summary>
    private string GetFormType()
    {
        try
        {
            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
            DataSet ds = objTechService.GET_PURC_DEP_ON_DOCCODE(Request.QueryString["Document_Code"]);
            string DeptType = Convert.ToString(ds.Tables[0].Rows[0]["DEPARTMENT"]);//Convert.ToString(Request.QueryString["DocumentCode"]).Split('-')[1];
            DataTable dtDept = objTechService.SelectDepartment();
            var q = from a in dtDept.AsEnumerable()
                    where a.Field<string>("code") == DeptType
                    select new { Form_Type = a.Field<string>("Form_Type") };
            return q.Select(a => a.Form_Type).Single();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            return "";
        }

    }

    /// <summary>
    /// bind Questions Grid
    /// </summary>
    private void BindPurcQuestion()                                                                                   // Bind Questions Grid
    {
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        try
        {
            string FormType = GetFormType();
            DataSet dt = objTechService.Get_Purc_Questions(Request.QueryString["Document_Code"], FormType);
            if (dt.Tables[0].Rows.Count > 0)
            {
                grdQuestion.DataSource = dt;
                grdQuestion.DataBind();
            }
            // else { tbl.Rows[1].Cells[0].Attributes.Add("style", "display:none"); }



        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Bind the Answers Dropdown list on row Data Bound
    /// </summary>
    protected void grdQuestion_RowDataBound(object sender, GridViewRowEventArgs e)                                    // Bind Answers Dropdownlist
    {
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblQuestID = (Label)e.Row.FindControl("lblQuestID");
            Label lblQuestion = (Label)e.Row.FindControl("lblQuestion");
            Label lblGradeType = (Label)e.Row.FindControl("lblGradeType");
            TextBox txtDescriptive = (TextBox)e.Row.FindControl("txtDescriptive");
            DropDownList ddlAnswers = (DropDownList)e.Row.FindControl("ddlAnswers");
            Label lblAns = (Label)e.Row.FindControl("lblAns");
            switch (lblGradeType.Text)
            {
                case "1":
                    ddlAnswers.Style.Add("display", "");
                    txtDescriptive.Style.Add("display", "none");

                    break;
                case "2":

                    ddlAnswers.Style.Add("display", "none");
                    txtDescriptive.Style.Add("display", "");

                    break;
            }

            DataSet ds = objTechService.Get_Purc_Questions_Options(Convert.ToInt32(lblQuestID.Text));


            ddlAnswers.DataSource = ds.Tables[0];
            ddlAnswers.DataTextField = "OptionText";
            ddlAnswers.DataValueField = "OPTIONS_ID";
            ddlAnswers.DataBind();
            ddlAnswers.Items.Insert(0, "--SELECT--");
            if (lblAns.Text != "0")
            {
                ddlAnswers.Items.FindByValue((e.Row.FindControl("lblAns") as Label).Text).Selected = true;
            }

        }
    }

    /// <summary>
    /// Bind Child repeater with then items for specific Subcatalogue
    /// </summary>
    protected void OnItemDataBound(object sender, RepeaterItemEventArgs e)                                            // Bind Items Repeater(Inner Repeater)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            DataSet dataforDisplay = new DataSet();
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                dataforDisplay = objTechService.GetReqItemsPreview(ReqsnCode, Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString());

            }

            string SubcatlogId = (e.Item.FindControl("hfCatlogId") as HiddenField).Value;

            Repeater rptOrders = e.Item.FindControl("rptChild") as Repeater;

            DataTable resulttbl = dataforDisplay.Tables[0].Select("SubCatalogID =" + SubcatlogId).CopyToDataTable();
            rptOrders.DataSource = resulttbl;
            rptOrders.DataBind();
        }
    }


    /// <summary>
    /// Delete Supply Items on Delete image Click
    /// </summary>
    protected void deleteClick(object sender, ImageClickEventArgs e)                                                 // Delete Supply Items
    {
        try
        {
            ImageButton ibtn = (ImageButton)sender;
            var item = (RepeaterItem)ibtn.NamingContainer;
            ImageButton btnupdate = (ImageButton)item.FindControl("ImgDelete");
            BLL_PURC_Purchase objinvntryitem = new BLL_PURC_Purchase();
            string a = ibtn.CommandArgument.ToString();
            string b = btnupdate.CommandArgument.ToString();

            int result = objinvntryitem.DeleteSupplyItem(btnupdate.CommandArgument.ToString(), Request.QueryString["Document_Code"].ToString());
            BindItmPreviewRpt();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// On Edit Button Click Redirect to requisition Items Page
    /// </summary>
    protected void btnEdit_Click(object sender, EventArgs e)                                                         // Edit Button Click redirect to Items page  
    {
        Session["DocumentCode"] = DocCode;
        Response.Redirect("PURC_Reqn_Items.aspx?Requisitioncode=" + Request.QueryString["Requisitioncode"] + "&Vessel_Code=" + Session["VesselID"] + "&Document_Code=" + Request.QueryString["Document_Code"]);
    }

    /// <summary>
    ///  Save Questionnaire 
    /// </summary>
    private void SaveQuestionnaire(string ReqsnCode)                                                                 // Save Questioners
    {
        try
        {
            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
            DataTable dtQuest = new DataTable();
            dtQuest.Columns.Add("QUESTION_ID");
            dtQuest.Columns.Add("OPTIONS_ID");
            dtQuest.Columns.Add("REMARK");
            DataRow dr = null;
            if (grdQuestion.Rows.Count > 0)
            {
                foreach (GridViewRow gridRow in grdQuestion.Rows)
                {
                    Label lblQID = (Label)gridRow.FindControl("lblQuestID");
                    DropDownList ddl = (DropDownList)gridRow.FindControl("ddlAnswers");
                    TextBox txtc = (TextBox)gridRow.FindControl("txtDescriptive");
                    if (ddl.SelectedValue != "")
                    {
                        dr = dtQuest.NewRow();
                        dr["QUESTION_ID"] = UDFLib.ConvertIntegerToNull(lblQID.Text);
                        dr["OPTIONS_ID"] = UDFLib.ConvertIntegerToNull(ddl.SelectedValue);
                        dr["REMARK"] = UDFLib.ConvertStringToNull(txtc.Text);
                        dtQuest.Rows.Add(dr);
                    }
                }
                int retval = objTechService.Insert_Purc_Question(ReqsnCode, Convert.ToString(Request.QueryString["Document_Code"]), Convert.ToInt32(Session["USERID"]), dtQuest, 1, UDFLib.ConvertIntegerToNull(Request.QueryString["Vessel_Code"]));

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }



    private Boolean ValidateQuestions()
    {
        bool Flag = true;
        try
        {
            foreach (GridViewRow gridRow in grdQuestion.Rows)
            {
                DropDownList ddl = new DropDownList();
                TextBox txt = new TextBox();
                ddl = (DropDownList)gridRow.FindControl("ddlAnswers");
                txt = (TextBox)gridRow.FindControl("txtDescriptive");
                Label lblOBJE = (Label)gridRow.FindControl("lblOBJE");
                if (lblOBJE.Text == "True")
                {
                    if (ddl.SelectedValue == "0" || ddl.SelectedValue == "--SELECT--")
                    {
                        Flag = false;
                        break;
                    }
                }
                else
                {
                    if (txt.Text.Trim() == string.Empty)
                    {
                        Flag = false;
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        return Flag;
    }

}
    #endregion



