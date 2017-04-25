using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using System.Data;
using SMS.Business.Infrastructure;
using System.Data.SqlTypes;
using SMS.Business.Crew;
using System.Text;
using System.Configuration;
using SMS.Properties;


public partial class Purchase_CTP_Contract_Details : System.Web.UI.Page
{
    int isfinalizing = 0;
    public UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {

        UserAccessValidation();

        if (!IsPostBack)
        {


            // first check for supplier's log-in
            if (!string.IsNullOrWhiteSpace(Convert.ToString(Session["SUPPCODE"])))
            {
                btnReworkToSupplier.Visible = false;
                btnRecallContract.Visible = false;
                
            }

            BLL_Infra_Currency objCurr = new BLL_Infra_Currency();
            DDLCurrency.DataTextField = "Currency_Code";
            DDLCurrency.DataValueField = "Currency_ID";
            DDLCurrency.DataSource = objCurr.Get_CurrencyList();
            DDLCurrency.DataBind();
            ListItem lis = new ListItem("Select", "0");
            DDLCurrency.Items.Insert(0, lis);
            DDLCurrency.SelectedIndex = 0;

            BindContractInfo();
            BindDataItems();
            BindFieldsAfterSave();



        }



    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            hlnkAddItem.Visible = false;

        }
        if (objUA.Edit == 0)
        {
            btnReworkToSupplier.Visible = false;
            btnRecallContract.Visible = false;

            btnSaveAsDraft.Visible = false;
            btnSubmittoseach.Visible = false;

        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {

            // You don't have sufficient previlege to access the requested page.
        }


    }

    private void BindContractInfo()
    {
        DataTable dtInfo = BLL_PURC_CTP.Get_Ctp_Contract_Info(Convert.ToInt32(Request.QueryString["Quotation_ID"]), Convert.ToInt32(Request.QueryString["Contract_ID"]));
        if (dtInfo.Rows.Count > 0 && Convert.ToInt32(Request.QueryString["Quotation_ID"]) > 0)
        {
            lblApprovedBy.Text = dtInfo.Rows[0]["ApprovedBy"].ToString();
            lblApprovedDT.Text = dtInfo.Rows[0]["Approved_Date"].ToString();
            lblCatalogue.Text = dtInfo.Rows[0]["System_Description"].ToString();
            lblCurrentSts.Text = dtInfo.Rows[0]["QTN_STS"].ToString();
            lblDepartment.Text = dtInfo.Rows[0]["Dept_Name"].ToString();
            lblEffectiveDT.Text = dtInfo.Rows[0]["Effective_Date"].ToString();
            lblPort.Text = dtInfo.Rows[0]["PORT_NAME"].ToString();
            //lblRejectedDT.Text = dtInfo.Rows[0]["ApprovedBy"].ToString();
            lblSeachangeRef.Text = dtInfo.Rows[0]["QTN_Contract_Code"].ToString();
            lblSentToSuppDT.Text = dtInfo.Rows[0]["RFQ_Sent_Date"].ToString();
            lblSubmittedBySupp.Text = dtInfo.Rows[0]["QTN_Received_Date"].ToString(); ;
            lblSupplierName.Text = dtInfo.Rows[0]["Full_NAME"].ToString();
            lblSupplierRef.Text = dtInfo.Rows[0]["Supplier_Ref_Number"].ToString();
            imgApprovedByRmk.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[ ] body=[" + dtInfo.Rows[0]["approver_remark"].ToString() + "]");

            hlnkAddItem.Attributes.Add("onclick", "OpenPopupWindowBtnID('POP__IDAddItem', 'Add/modify contract item', 'CTP_Quotation_Items_PopUp.aspx?Dept_ID=" + dtInfo.Rows[0]["Dept_ID"].ToString() + "&Catalogue_code=" + dtInfo.Rows[0]["Catalogue"].ToString() + "&Catalogue_Name=" + dtInfo.Rows[0]["System_Description"].ToString() + "&DepartmentName=" + dtInfo.Rows[0]["Dept_Name"].ToString() + "&Quotation_ID=" + Request.QueryString["Quotation_ID"] + " ','popup',800,1500,40,40,true,true,true,false,'" + btnSearch.ClientID + "'); return false;");

            BindSubCatalogue(dtInfo.Rows[0]["Catalogue"].ToString());

        }
        else
        {
            hlnkAddItem.Attributes.Add("onclick", "OpenPopupWindowBtnID('POP__IDAddItem', 'Add/modify contract item', 'CTP_Quotation_Items_PopUp.aspx?Dept_ID=" + dtInfo.Rows[0]["Dept_ID"].ToString() + "&Catalogue_code=" + dtInfo.Rows[0]["Catalogue"].ToString() + "&Catalogue_Name=" + dtInfo.Rows[0]["System_Description"].ToString() + "&DepartmentName=" + dtInfo.Rows[0]["Dept_Name"].ToString() + "&Contract_ID=" + Request.QueryString["Contract_ID"] + " ','popup',800,1500,40,40,true,true,true,false,'" + btnSearch.ClientID + "'); return false;");
        }
    }

    private void BindFieldsAfterSave()
    {
        DataTable dtInfo = BLL_PURC_CTP.Get_Ctp_Contract_Info(Convert.ToInt32(Request.QueryString["Quotation_ID"]));
        if (dtInfo.Rows.Count > 0)
        {
            ListItem liCurrency = DDLCurrency.Items.FindByText(dtInfo.Rows[0]["currency"].ToString());
            if (liCurrency != null)
            {
                DDLCurrency.ClearSelection();
                liCurrency.Selected = true;
            }
            txtBargeCharge.Text = dtInfo.Rows[0]["Barge_Charge"].ToString();
            txtDiscount.Text = dtInfo.Rows[0]["Discount"].ToString();
            txtFrieghtCharge.Text = dtInfo.Rows[0]["Freight_Charge"].ToString();
            txtOtherCharge.Text = dtInfo.Rows[0]["Other_Charge"].ToString();
            txtPkgCharge.Text = dtInfo.Rows[0]["Pkg_Hld_Charge"].ToString();
            txtTruckCharge.Text = dtInfo.Rows[0]["Truck_Charge"].ToString();
            txtVat.Text = dtInfo.Rows[0]["Vat"].ToString();


            lblApprovedItem_count.Text = "Item Count :-&nbsp;&nbsp; Approved :&nbsp;" + dtInfo.Rows[0]["APPROVED_ITEM_COUNT"].ToString() + ",&nbsp;&nbsp;&nbsp;Un Approved :&nbsp;" + dtInfo.Rows[0]["NOT_APPROVED_ITEM_COUNT"].ToString();

            if (dtInfo.Rows[0]["Quotation_Status"].ToString() == "FZ" || dtInfo.Rows[0]["Quotation_Status"].ToString() == "AP")
            {
                btnSaveAsDraft.Enabled = false;
                btnSubmittoseach.Enabled = false;
                hdf_Quotation_Save_Status.Value = "1";
                hlnkAddItem.Visible = false;
            }
            else
            {
                btnSaveAsDraft.Enabled = true;
                btnSubmittoseach.Enabled = true;
                hdf_Quotation_Save_Status.Value = "0";
                hlnkAddItem.Visible = true;
            }


        }
    }


    protected void BindDataItems()
    {
        if (IsPostBack && hdf_Quotation_Save_Status.Value == "0")
            SaveItemPrice();

        int is_Fetch_Count = ucCustomPagerctp.isCountRecord;
        gvContractDetails.DataSource = BLL_PURC_CTP.Get_Ctp_Contract_Details(Convert.ToInt32(Request.QueryString["Quotation_ID"]),
                                                                           UDFLib.ConvertToInteger(rbtnApprStatus.SelectedValue),
                                                                           UDFLib.ConvertStringToNull(txtItemsDesc.Text),
                                                                           UDFLib.ConvertStringToNull(ddlSubCatalogue.SelectedValue),
                                                                           ucCustomPagerctp.CurrentPageIndex,
                                                                           ucCustomPagerctp.PageSize,
                                                                          ref is_Fetch_Count
                                                                           );
        gvContractDetails.DataBind();
        if (ucCustomPagerctp.isCountRecord == 1)
        {
            ucCustomPagerctp.CountTotalRec = is_Fetch_Count.ToString();
            ucCustomPagerctp.BuildPager();
        }


    }
    /// <summary>
    /// Line No 202:Commented due to it takes time to load data . It used only to add ALL in subsytem.
    /// so ALL item externally added with value 0. some time if ALL Value not pesent in dropdown item it will throw Null refernce .
    /// </summary>
    /// <param name="Catalogue_code"></param>
    public void BindSubCatalogue(string Catalogue_code)
    {
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DataTable dtSubSystem = new DataTable();
            string CatalogId = Catalogue_code;
            dtSubSystem = objTechService.SelectSubCatalogs();
            //dtSubSystem.DefaultView.RowFilter = "System_Code ='" + CatalogId + "' or SubSystem_code='0'";
            
            dtSubSystem.DefaultView.RowFilter = "System_Code ='" + CatalogId + "'";
            DataTable dt = dtSubSystem.DefaultView.ToTable();

            DataRow dr = dt.NewRow();
            dr["SUBSYSTEM_CODE"] = "0";
            dr["Subsystem_Description"] = "ALL";
            dt.Rows.InsertAt(dr, 0);

            ddlSubCatalogue.DataTextField = "Subsystem_Description";
            ddlSubCatalogue.DataValueField = "SUBSYSTEM_CODE";

            ddlSubCatalogue.DataSource = dt;
            ddlSubCatalogue.DataBind();

            ddlSubCatalogue.Items.FindByText("ALL").Selected = true;

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindDataItems();
        BindFieldsAfterSave();
    }
    protected void gvContractDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow item = (GridViewRow)e.Row;
            Image IMGSupp = (Image)item.FindControl("imgSupplierRemark");
            Image IMGPurch = (Image)item.FindControl("imgPurchaserRemark");



            if (IMGSupp.AlternateText.Trim() != "")
            {
                IMGSupp.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[" + IMGSupp.AlternateText + "]");
                IMGSupp.ImageUrl = "~/Images/remark.gif";
            }
            else
                IMGSupp.ImageUrl = "~/Images/remark_new.gif";

            if (IMGPurch.AlternateText.Trim() != "")
            {
                IMGPurch.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[" + IMGPurch.AlternateText + "]");
                IMGPurch.ImageUrl = "~/Images/remark.gif";
            }
            else
                IMGPurch.ImageUrl = "~/Images/remark_new.gif";



        }
    }

    protected void btnDeleteItem_Click(object s, CommandEventArgs e)
    {
        int sts = BLL_PURC_CTP.Update_Ctp_Remove_QtnItem(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Request.QueryString["quotation_id"]), Convert.ToInt32(Session["userid"].ToString()));
        if (sts > 0)
        {
            BindDataItems();
            BindFieldsAfterSave();
        }

    }

    protected void SaveItemPrice()
    {

        DataTable dtItemPrice = new DataTable();
        dtItemPrice.Columns.Add("pkid", typeof(int));
        dtItemPrice.Columns.Add("rate", typeof(decimal));
        dtItemPrice.Columns.Add("discount", typeof(decimal));
        dtItemPrice.Columns.Add("unit", typeof(string));
        dtItemPrice.Columns.Add("supp_remark");

        DataRow dr;
        foreach (GridViewRow gr in gvContractDetails.Rows)
        {
            dr = dtItemPrice.NewRow();
            dr["pkid"] = Convert.ToInt32(((TextBox)gr.FindControl("txtrate")).ToolTip.Trim());
            dr["rate"] = UDFLib.ConvertToDecimal(((TextBox)gr.FindControl("txtrate")).Text.Trim());
            dr["discount"] = UDFLib.ConvertToDecimal(((TextBox)gr.FindControl("txtDiscount")).Text.Trim());
            dr["unit"] = ((TextBox)gr.FindControl("lbtnUnitsPKg")).Text.Trim();
            dtItemPrice.Rows.Add(dr);
        }

        DataTable dtCharges = new DataTable();
        dtCharges.Columns.Add("Currency");
        dtCharges.Columns.Add("Truck_Charge");
        dtCharges.Columns.Add("Barge_Charge");
        dtCharges.Columns.Add("Freight_Charge");
        dtCharges.Columns.Add("Pkg_Hld_Charge");
        dtCharges.Columns.Add("Other_Charge");
        dtCharges.Columns.Add("Vat");
        dtCharges.Columns.Add("Discount");


        DataRow drCharges = dtCharges.NewRow();
        drCharges["Currency"] = UDFLib.ConvertStringToNull(DDLCurrency.SelectedItem.Text.Trim());
        drCharges["Truck_Charge"] = UDFLib.ConvertDecimalToNull(txtTruckCharge.Text);
        drCharges["Barge_Charge"] = UDFLib.ConvertDecimalToNull(txtBargeCharge.Text);
        drCharges["Freight_Charge"] = UDFLib.ConvertDecimalToNull(txtFrieghtCharge.Text);
        drCharges["Pkg_Hld_Charge"] = UDFLib.ConvertDecimalToNull(txtPkgCharge.Text);
        drCharges["Other_Charge"] = UDFLib.ConvertDecimalToNull(txtOtherCharge.Text);
        drCharges["Vat"] = UDFLib.ConvertDecimalToNull(txtVat.Text);
        drCharges["Discount"] = UDFLib.ConvertDecimalToNull(txtDiscount.Text);

        dtCharges.Rows.Add(drCharges);

        if (dtItemPrice.Rows.Count > 0)
            BLL_PURC_CTP.Upd_Ctp_Items_Price(Convert.ToInt32(Request.QueryString["Quotation_ID"]), dtItemPrice, Convert.ToInt32(Session["userid"].ToString()), isfinalizing, dtCharges);


    }
    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {

        BindDataItems();
        BindFieldsAfterSave();
    }

    protected void btnSubmittoseach_Click(object sender, EventArgs e)
    {

        isfinalizing = 1;
        BindDataItems();
        BindFieldsAfterSave();

        String msg = String.Format("alert(' saved and submitted successfully.');window.open('','_self','');window.close();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgclosesucesc", msg, true);

    }

    protected void imgRemark_Click(object sender, CommandEventArgs e)
    {
        try
        {

            GridViewRow gr = (GridViewRow)((ImageButton)sender).Parent.Parent.Parent.Parent;
            ViewState["itemCommentRow"] = gr.RowIndex;

            DivRemarks.Visible = true;
            updDivRemarks.Update();

            ViewState["QTN_ITEM_ID"] = e.CommandArgument;
            ViewState["USER_TYPE"] = e.CommandName;

            txtRemarks.Text = ((ImageButton)sender).AlternateText;

            String msg = String.Format("SetPositionDvRemark('" + ((ImageButton)sender).ClientID + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
        catch (Exception ex)
        {

        }

    }
    protected void btndivSave_Click(object sender, EventArgs e)
    {
        try
        {
            int user_type = Convert.ToInt32(ViewState["USER_TYPE"].ToString());

            BLL_PURC_CTP.Update_Ctp_QtnItem_Remark(user_type, txtRemarks.Text, Convert.ToInt32(Session["USERID"].ToString()), Convert.ToInt32(ViewState["QTN_ITEM_ID"].ToString()));

            GridViewRow gr = (GridViewRow)gvContractDetails.Rows[Convert.ToInt32(ViewState["itemCommentRow"].ToString())];
            ImageButton imgbtn = new ImageButton();
            if (user_type == 1)
            {
                imgbtn = (ImageButton)gr.FindControl("imgSupplierRemark");


            }
            else if (user_type == 2)
            {
                imgbtn = (ImageButton)gr.FindControl("imgPurchaserRemark");

            }

            if (txtRemarks.Text.Trim() == "")
                imgbtn.ImageUrl = "~/Images/remark_new.gif";
            else
                imgbtn.ImageUrl = "~/Images/remark.gif";


            imgbtn.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[ ] body=[" + txtRemarks.Text + "]");
            imgbtn.AlternateText = txtRemarks.Text;

            DivRemarks.Visible = false;

            txtRemarks.Text = "";
            ViewState["USER_TYPE"] = "0";
            ViewState["QTN_ITEM_ID"] = "-1";
            updDivRemarks.Update();


        }
        catch (Exception ex)
        {

        }

    }

    protected void btnReworkToSupplier_Click(object sender, EventArgs e)
    {
        int sts = BLL_PURC_CTP.Update_Ctp_ReworkToSupplier(Convert.ToInt32(Session["USERID"].ToString()), Convert.ToInt32(Request.QueryString["Quotation_ID"]));
        if (sts > 0)
        {
            //BindFieldsAfterSave();
            string ServerIPAdd = ConfigurationManager.AppSettings["WebQuotSite"].ToString();
            DataSet dsSendMailInfo = BLL_PURC_CTP.Get_Ctp_Supplier_Mail(Convert.ToInt32(Request.QueryString["Quotation_ID"]));

            CTP_RFQ_Mail objmail = new CTP_RFQ_Mail(this);
            objmail.SendEmailToSupplier(dsSendMailInfo, dsSendMailInfo.Tables[0].Rows[0]["supplier_code"].ToString(), ServerIPAdd, "", true, "3", true);
            String msg = String.Format("window.open('','_self','');window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }


    protected void btnExport_Click(object s, EventArgs e)
    {
        int is_Fetch_Count = 0;
        DataTable dtexportdata = BLL_PURC_CTP.Get_Ctp_Contract_Details(Convert.ToInt32(Request.QueryString["Quotation_ID"]),
                                                                           UDFLib.ConvertToInteger(rbtnApprStatus.SelectedValue),
                                                                           UDFLib.ConvertStringToNull(txtItemsDesc.Text),
                                                                           UDFLib.ConvertStringToNull(ddlSubCatalogue.SelectedValue),
                                                                           null,
                                                                           null,
                                                                          ref is_Fetch_Count
                                                                           );

        string[] HeaderCaptions = new string[] { "Sub Catalogue", "Part No.", "Short Desc.", "Long Desc.", "Unit", "Offer Unit", "Unit Price", "Discount", "Final Unit Price" };
        string[] DataColumnsName = new string[] { "Subsystem_Description", "Part_Number", "Short_Description", "Long_Description", "Unit_and_Packings", "unit", "Rate", "Discount", "net_price" };
        string FileHeaderName = @"<table  border='1' cellpadding='3' style='border-collapse:collapse;background-color:#F2F2F2;margin-left:10px;' >
                               
                                <tr><td  style='font-weight:bold;text-align:right'>Dept Name :</td><td >" + lblDepartment.Text + " </td><td style='font-weight:bold;text-align:right' >Catalogue :</td> <td>" + lblCatalogue.Text + " </td><td style='font-weight:bold;text-align:right'>Contract Code :</td> <td>" + lblSeachangeRef.Text + " </td>  </tr>" +
                                "<tr> <td style='font-weight:bold;text-align:right'>Supplie Name :</td><td>" + lblSupplierName.Text + " </td> <td style='font-weight:bold;text-align:right'>Port :</td><td>" + lblPort.Text + " </td><td style='font-weight:bold;text-align:right'>Supplier Ref No.: </td><td> " + lblSupplierRef.Text + " </td> </tr></table>";
        string FileName = lblSeachangeRef.Text;

        GridViewExportUtil.ShowExcel(dtexportdata, HeaderCaptions, DataColumnsName, FileName, FileHeaderName);
    }

    protected void btnRecallContract_Click(object s, EventArgs e)
    {
        int sts = BLL_PURC_CTP.UPD_CTP_Recall_Approved_Contract(Convert.ToInt32(Request.QueryString["Quotation_ID"]), Convert.ToInt32(Session["userid"]), "-");
        if (sts > 0)
        {
            String msg = String.Format("alert(' Recalled successfully.');window.opener.location.reload();window.open('','_self','');window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgclosesucesc", msg, true);
        }
    }






}

