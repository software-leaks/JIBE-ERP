using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.PURC;
using AjaxControlToolkit4;
using System.IO;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class Purchase_RequisitionPreview : System.Web.UI.Page
{
    public int OFFICE_ID = 0;
    public int WORKLIST_ID = 0;
    public int VESSEL_ID = 0;
    public string ReqsnCode = "";
    public string DocCode = "";
    public string FormType = "";
    public string VesselCode = "";
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    DataView dv = new DataView();
    DataTable dtvat = new DataTable();
    DataTable dtWithHold = new DataTable();
    DataSet dataforDisplay = new DataSet();
    string DFormat = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        string aa = HD_SelectedSupplier.Value;
        if (!IsPostBack)
        {
            DFormat = UDFLib.GetDateFormat();
            CE_SuppDate.Format = DFormat;
            ReqsnCode = Convert.ToString(Request.QueryString["Requisitioncode"]);
            DocCode = Convert.ToString(Request.QueryString["Document_Code"]);
            Session["DocumentCode"] = Convert.ToString(Request.QueryString["Document_Code"]);
            VesselCode = Convert.ToString(Request.QueryString["Vessel_Code"]);
            dtvat = BLL_PURC_Common.PURC_Get_Sys_Variable(UDFLib.ConvertToInteger(GetSessionUserID()), "VAT");
            dtWithHold = BLL_PURC_Common.PURC_Get_Sys_Variable(UDFLib.ConvertToInteger(GetSessionUserID()), "WIthHoldTax");

            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                ViewState["CurrencyRates"] = objTechService.getCurrentRates().Tables[0];
            }

            DataTable dt = (DataTable)ViewState["CurrencyRates"];

            dt.DefaultView.RowFilter = "Curr_Code='USD'";

            if (Session["CurrentCurr"] == null)
            {
                Session["PreviousCurr"] = Session["CurrentCurr"] = dt.DefaultView[0]["Exch_rate"].ToString();
                Session["CurrencyCode"] = dt.DefaultView[0]["Curr_Code"].ToString();
            }

            BindSupplierView();                 // Bind Suppliers Grid
            BindItmPreviewRpt();                // Bind Main Grid
            BindPurcQuestion();                 // Bind Purchase Questions Grid
            bindSupp_port();                    // Bind Supplier Port DropDown

            btnLoadFiles.Attributes.Add("style", "visibility:hidden");
            Session["VesselID"] = VesselCode;
            Session["Requisitioncode"] = ReqsnCode;
            LoadFiles(null, null);
            VESSEL_ID = Convert.ToInt32(Session["VesselID"]);
        }

    }



    /// <summary>
    /// SAVE,EDIT,DELETE ,CANCLE Button Events
    /// </summary>
    /// 
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PendingRequisitionDetails.aspx");
    }
    /// <summary>
    /// Redirects to the Requisition Items page (Previous Page) 
    /// </summary>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Session["DocumentCode"] = DocCode;
        Response.Redirect("PURC_Reqn_Items.aspx?Requisitioncode=" + ReqsnCode + "&Vessel_Code=" + Session["VesselID"] + "&Document_Code=" + DocCode);
    }

    /// <summary>
    /// Fill all the Items Detail in Datatable and sets the "IventoryItemData" properties .
    /// saves the Requisition  
    /// </summary>
    protected void btnSave_Click(object sender, EventArgs e)                                             // Save Requisition
    {
        if (ISvalid())
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("ITEM_REF_CODE");
            dt.Columns.Add("ORDER_PRICE");
            dt.Columns.Add("ORDER_DISCOUNT");
            dt.Columns.Add("ORDER_RATE");
            dt.Columns.Add("REQUESTED_QTY");
            dt.Columns.Add("VAT");
            dt.Columns.Add("WithHold");

            for (int i = 0; i < rptMain.Items.Count; i++)
            {
                Repeater rptChild = (Repeater)rptMain.Items[i].FindControl("rptChild");
                for (int j = 0; j < rptChild.Items.Count; j++)
                {
                    TextBox txtPPU = (TextBox)rptChild.Items[j].FindControl("txt_PPU");
                    TextBox txtTP = (TextBox)rptChild.Items[j].FindControl("txtTP");
                    TextBox txtDiscount = (TextBox)rptChild.Items[j].FindControl("txt_Discount");
                    TextBox txtQnty = (TextBox)rptChild.Items[j].FindControl("txtQnty");
                    ImageButton btn = (ImageButton)rptChild.Items[j].FindControl("btnupdate");
                    DropDownList ddlItemvat = (DropDownList)rptChild.Items[j].FindControl("ddl_ItemVAT");
                    DropDownList ddlItemWithhod = (DropDownList)rptChild.Items[j].FindControl("ddl_item_Withhold");
                    DataRow dr = dt.NewRow();
                    dr["ITEM_REF_CODE"] = btn.CommandArgument.ToString();
                    dr["ORDER_PRICE"] = Convert.ToDecimal(txtPPU.Text.Length == 0 ? "0" : txtTP.Text);
                    dr["ORDER_DISCOUNT"] = Convert.ToDecimal(txtDiscount.Text.Length == 0 ? "0" : txtDiscount.Text);
                    dr["ORDER_RATE"] = Convert.ToDecimal(txtPPU.Text.Length == 0 ? "0" : txtPPU.Text);
                    dr["REQUESTED_QTY"] = Convert.ToDecimal(txtPPU.Text.Length == 0 ? "0" : txtQnty.Text);
                    dr["VAT"] = Convert.ToDecimal(Convert.ToDecimal(ddlItemvat.SelectedValue) == 0 ? "0" : ddlItemvat.SelectedValue);
                    dr["WithHold"] = Convert.ToDecimal(Convert.ToDecimal(ddlItemWithhod.SelectedValue) == 0 ? "0" : ddlItemWithhod.SelectedValue);

                    dt.Rows.Add(dr);

                }
            }

            IventoryItemData ItemData = new IventoryItemData();
            ItemData.DocumentCode = Session["DocumentCode"].ToString();
            ItemData.RequisitionCode = Session["Requisitioncode"].ToString();
            ItemData.Delivery_Port = Convert.ToInt32(ddlSupp_Port.SelectedValue);
            ItemData.Discount = Convert.ToInt32(txt_Discount.Text);
            ItemData.SupplierID = HD_SelectedSupplier.Value.ToString();
            ItemData.WithHoldTax = ddlWithHoldTax.SelectedValue.ToString();
            ItemData.VAT = ddlVAt.SelectedValue.ToString();
            ItemData.Advance = Convert.ToInt32(txt_Advance.Text);
            ItemData.Currency = Session["CurrencyCode"].ToString();
            ItemData.Delivery_Date = UDFLib.ConvertToDate(txt_SuppDate.Text);
           
            //bindprice(Convert.ToDecimal(Session["PreviousCurr"]));
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                objTechService.SaveUpdate_Quotation(ItemData, Convert.ToInt32(Session["userid"]), Convert.ToDecimal(HD_TP.Value.Length < 1 ? "0" : HD_TP.Value), dt);
            }

            String msg = String.Format("alert('Requisition has been Saved sucessfully.'); ");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
        else
        {
            string msgtxt = string.Empty;
            if (HD_SelectedSupplier.Value.ToString() == string.Empty) { msgtxt = "Please Select The Supplier"; } else { msgtxt = "Please Fill all the Mandatory Fields"; }
            String msg = String.Format("alert('" + msgtxt + "'); ");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int ReturenID;
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                ReturenID = objTechService.DeleteRequisitionItem(ReqsnCode, DocCode);

            }
            String msg = String.Format("alert('Requisition has been deleted sucessfully.'); ");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            Response.Redirect("PendingRequisitionDetails.aspx");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnFinalize_Click(object sender, EventArgs e)
    {
        if (ISvalid())
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("ITEM_REF_CODE");
            dt.Columns.Add("ORDER_PRICE");
            dt.Columns.Add("ORDER_DISCOUNT");
            dt.Columns.Add("ORDER_RATE");
            dt.Columns.Add("REQUESTED_QTY");
            dt.Columns.Add("VAT");
            dt.Columns.Add("WithHold");

            for (int i = 0; i < rptMain.Items.Count; i++)
            {
                Repeater rptChild = (Repeater)rptMain.Items[i].FindControl("rptChild");
                for (int j = 0; j < rptChild.Items.Count; j++)
                {
                    TextBox txtPPU = (TextBox)rptChild.Items[j].FindControl("txt_PPU");
                    TextBox txtTP = (TextBox)rptChild.Items[j].FindControl("txtTP");
                    TextBox txtDiscount = (TextBox)rptChild.Items[j].FindControl("txt_Discount");
                    TextBox txtQnty = (TextBox)rptChild.Items[j].FindControl("txtQnty");
                    ImageButton btn = (ImageButton)rptChild.Items[j].FindControl("btnupdate");
                    DropDownList ddlItemvat = (DropDownList)rptChild.Items[j].FindControl("ddl_ItemVAT");
                    DropDownList ddlItemWithhod = (DropDownList)rptChild.Items[j].FindControl("ddl_item_Withhold");
                    DataRow dr = dt.NewRow();
                    dr["ITEM_REF_CODE"] = btn.CommandArgument.ToString();
                    dr["ORDER_PRICE"] = Convert.ToDecimal(txtPPU.Text.Length == 0 ? "0" : txtTP.Text);
                    dr["ORDER_DISCOUNT"] = Convert.ToDecimal(txtDiscount.Text.Length == 0 ? "0" : txtDiscount.Text);
                    dr["ORDER_RATE"] = Convert.ToDecimal(txtPPU.Text.Length == 0 ? "0" : txtPPU.Text);
                    dr["REQUESTED_QTY"] = Convert.ToDecimal(txtPPU.Text.Length == 0 ? "0" : txtQnty.Text);
                    dr["VAT"] = Convert.ToDecimal(Convert.ToDecimal(ddlItemvat.SelectedValue) == 0 ? "0" : ddlItemvat.SelectedValue);
                    dr["WithHold"] = Convert.ToDecimal(Convert.ToDecimal(ddlItemWithhod.SelectedValue) == 0 ? "0" : ddlItemWithhod.SelectedValue);

                    dt.Rows.Add(dr);

                }
            }
            //string DateFormat = UDFLib.GetDateFormat();//Get User date format
            //UDFLib.ConvertUserDateFormat(txtDeliveryDate.Text, DateFormat);



            string ReturnReq = string.Empty;
            IventoryItemData ItemData = new IventoryItemData();
            ItemData.DocumentCode = DocCode;
            ItemData.Delivery_Date =  UDFLib.ConvertToDate(txt_SuppDate.Text); ;
            ItemData.Delivery_Port = Convert.ToInt32(ddlSupp_Port.SelectedValue);
            ItemData.Discount = Convert.ToInt32(txt_Discount.Text);
            ItemData.SupplierID = HD_SelectedSupplier.Value.ToString();
            ItemData.WithHoldTax = ddlWithHoldTax.SelectedValue.ToString();
            ItemData.VAT = ddlVAt.SelectedValue.ToString();
            ItemData.Advance = Convert.ToInt32(txt_Advance.Text);
            ItemData.Currency = Session["CurrencyCode"].ToString();
            if (ValidateQuestions() == true)
            {
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    objTechService.SaveUpdate_Quotation(ItemData, Convert.ToInt32(Session["userid"]), Convert.ToDecimal(HD_TP.Value.Length < 1 ? "0" : HD_TP.Value), dt);
                }

                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    ReturnReq = objTechService.SaveUpdate_FinalQuotation(ItemData, Convert.ToInt32(Session["userid"]));
                }
                SaveQuestionnaire(ReturnReq);
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Requisition has been Finalized.\\n Please Note Requisition Number:" + ReturnReq + "');window.location='PendingRequisitionDetails.aspx?NOQUOTE=1';", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Please complete the Purchase Questionnaire to proceed with the purchase process');", true);
                BindItmPreviewRpt();

            }
        }
        else
        {
            string msgtxt = string.Empty;
            if (HD_SelectedSupplier.Value.ToString() == string.Empty) { msgtxt = "Please Select The Supplier"; } else { msgtxt = "Please Fill all the Mandatory Fields"; }
            String msg = String.Format("alert('" + msgtxt + "'); ");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }

    }



    //-- Events

    /// <summary>
    /// Binds the Gridview with the Uploaded Attachments
    /// </summary>
    public void LoadFiles(object s, EventArgs e)
    {
        try
        {
            BLL_PURC_Purchase objAttch = new BLL_PURC_Purchase();
            gvAttachment.DataSource = objAttch.Purc_Get_Reqsn_Attachments_New(Convert.ToString(Session["DocumentCode"]), int.Parse(Request.QueryString["Vessel_Code"]));
            gvAttachment.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// On Delete Button Click Delets the Attachment 
    /// </summary>
    public void imgbtnDelete_Click(object s, EventArgs e)
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
    /// Ajax File Upload
    /// </summary>
    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
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

            int sts = objTechService.SaveAttachedFileInfo_New(Convert.ToString(VesselCode), DocCode, "0", Path.GetExtension(file.FileName), UDFLib.Remove_Special_Characters(Path.GetFileName(file.FileName)), "../Uploads/Purchase/" + Flag_Attach, Session["USERID"].ToString(), 0);

            string FullFilename = Path.Combine(sPath, GUID.ToString() + Path.GetExtension(file.FileName));

            FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }     //-- Attachment Upload
    protected void btnLoadFiles_Click(object sender, EventArgs e)
    {
        LoadFiles(null, null);
    }
    /// <summary>
    /// Gets the saved Requisitions 
    /// Sets the Currency Button Value in Header
    /// and Binds the Gridview AlternatingItem Values by Calculating and setting the Controls and binding Withhold Tax and VAT Dropdownlist
    /// </summary>
    protected void OnItemDataBound(object sender, RepeaterItemEventArgs e)                                // Bind requisition Items Repeater (Inner Grid)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataTable dsproperty = new DataTable();
            if (HD_SelectedSupplier.Value == null)
            {
                if (dataforDisplay.Tables[3].Rows.Count != 0)
                {
                    for (int i = 0; i < dataforDisplay.Tables[3].Rows.Count; i++)
                    {
                        if (dataforDisplay.Tables[3].Rows[i]["Property_ID"].ToString() == "1")
                        {
                            ddlVAt.Enabled = true;
                        }
                        else { ddlVAt.Enabled = false; }
                        if (dataforDisplay.Tables[3].Rows[i]["Property_ID"].ToString() == "2")
                        {
                            ddlWithHoldTax.Enabled = true;
                        }
                        else
                        {
                            ddlWithHoldTax.Enabled = false;
                        }
                    }
                }
            }
            else
            {
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    dsproperty = objTechService.getSupplierProperty(HD_SelectedSupplier.Value.ToString()).Tables[0];
                }
            }

            string SubcatlogId = (e.Item.FindControl("hfCatlogId") as HiddenField).Value;
            Repeater rptChild = e.Item.FindControl("rptChild") as Repeater;


            DataTable resulttbl = dataforDisplay.Tables[0].Select("SubCatalogID =" + SubcatlogId).CopyToDataTable();

            for (int i = 0; i < resulttbl.Rows.Count; i++)
            {
                string rate = resulttbl.Rows[i]["ORDER_RATE"].ToString().Length > 0 ? resulttbl.Rows[i]["ORDER_RATE"].ToString() : "0";
                decimal PricePerunit = System.Math.Round(Convert.ToDecimal(rate), 2);
                resulttbl.Rows[i]["ORDER_RATE"] = PricePerunit.ToString();


            }
            rptChild.DataSource = resulttbl;
            rptChild.DataBind();

            for (int i = 0; i < rptChild.Items.Count; i++)
            {

                DropDownList dll_vat = (DropDownList)rptChild.Items[i].FindControl("ddl_ItemVAT");
                DropDownList dll_withhold = (DropDownList)rptChild.Items[i].FindControl("ddl_item_Withhold");
                Item_bindddl(dll_vat, dtvat, System.Math.Round(Convert.ToDecimal(resulttbl.Rows[i]["ORDER_VAT"]), 2).ToString());

                Item_bindddl(dll_withhold, dtWithHold, System.Math.Round(Convert.ToDecimal(resulttbl.Rows[i]["ORDER_WITHHOLD_TAX"] == "" ? 0 : resulttbl.Rows[i]["ORDER_WITHHOLD_TAX"]), 2).ToString());
                for (int j = 0; j < dsproperty.Rows.Count; i++)
                {
                    if (dsproperty.Rows[i]["Property_ID"].ToString() == "1")
                    {
                        ddlVAt.Enabled = true;
                    }
                    else { ddlVAt.Enabled = false; }
                    if (dsproperty.Rows[i]["Property_ID"].ToString() == "2")
                    {
                        ddlWithHoldTax.Enabled = true;
                    }
                    else
                    {
                        ddlWithHoldTax.Enabled = false;
                    }

                }
            }
        }
        else if (e.Item.ItemType == ListItemType.Header)
        {
            Button btn = (Button)e.Item.FindControl("BtnCurrency");
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                string currency = objTechService.ConfiguredSupplierPreview(DocCode, VesselCode, txt_searchCodeName.Text).Tables[1].Rows[0]["CURRENCY"].ToString();
                btn.Text = currency.Length == 0 ? "USD" : currency;
            }

        }
    }
    protected void OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)             //-- OnPage Change change pager index
    {
        (GV_SupplierList.FindControl("DataPager1") as DataPager).SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
        this.BindSupplierView();
    }
    /// <summary>
    /// On Currency Change in Dropdown Changes the Text on Currency Button 
    /// and Sets the Current Selected Currency in Session["CurrentCurr"] 
    /// and Previous Session in Session["PreviousCurr"]
    /// </summary>
    protected void CurrencyChange(object sender, EventArgs e)                                            //-- Show Currency List 
    {

        DropDownList ddlCurrency = (DropDownList)sender;
        var item = (RepeaterItem)ddlCurrency.NamingContainer;
        Button BtnCurrency = (Button)item.FindControl("BtnCurrency");
        BtnCurrency.Text = ddlCurrency.SelectedItem.Text;


        Session["CurrentCurr"] = ddlCurrency.SelectedValue;
        Session["CurrencyCode"] = ddlCurrency.SelectedItem.Text;

        ddlCurrency.Visible = false;
        BtnCurrency.Visible = true;
        Session["PreviousCurr"] = Session["CurrentCurr"];


    }
    /// <summary>
    /// Updates the Requisition Item changed in In the Gridview 
    /// </summary>
    protected void UpdateItemClick(object sender, EventArgs e)                                          //-- Update Requisition Items
    {
        ImageButton ibtn = (ImageButton)sender;
        var item = (RepeaterItem)ibtn.NamingContainer;
        TextBox txtQnty = (TextBox)item.FindControl("txtQnty");
        TextBox txt_Discount = (TextBox)item.FindControl("txt_Discount");
        TextBox txt_PPU = (TextBox)item.FindControl("txt_PPU");
        ImageButton imbtn = (ImageButton)item.FindControl("ImgUpdate");
        ImageButton btnCancel = (ImageButton)item.FindControl("ImgBtnCancel");
        DropDownList ddl_ItemVat = (DropDownList)item.FindControl("ddl_ItemVAT");
        DropDownList ddl_ItemWthHld = (DropDownList)item.FindControl("ddl_item_Withhold");

        BLL_PURC_Purchase objinvntryitem = new BLL_PURC_Purchase();
        int result = objinvntryitem.PURC_UPD_Reqsn_supplyitems(ibtn.CommandArgument.ToString(), DocCode, Convert.ToDecimal(txtQnty.Text), Convert.ToDecimal(txt_Discount.Text), Convert.ToDecimal(txt_PPU.Text), Convert.ToDecimal(ddl_ItemVat.SelectedValue), Convert.ToDecimal(ddl_ItemWthHld.SelectedValue));
        txt_Discount.Enabled = txt_PPU.Enabled = txtQnty.Enabled = ibtn.Visible = false;
        imbtn.Visible = true;
        btnCancel.Visible = false;
    }
    /// <summary>
    /// Do Calculation for the Total Price
    /// </summary>
    protected void Calculate(object sender, EventArgs e)                                                //-- On Items DataChange Call BindPrice to Calculate Discounte,Total Price and Quantity on Items
    {
        bindprice(Convert.ToDecimal(Session["PreviousCurr"]));
    }
    /// <summary>
    /// on Update Icon click, the Row Is Enabled To edit the Requisiton Items
    /// 
    /// </summary>
    protected void updateClick(object sender, EventArgs e)                                              // Onclick Update image enable Update 
    {
        ImageButton imbtn = (ImageButton)sender;

        var item = (RepeaterItem)imbtn.NamingContainer;
        TextBox txtQnty = (TextBox)item.FindControl("txtQnty");
        txtQnty.Text = txtQnty.Text.Split('.')[0];
        ImageButton btnupdate = (ImageButton)item.FindControl("btnUpdate");
        TextBox txt_Discount = (TextBox)item.FindControl("txt_Discount");
        ImageButton btnCancel = (ImageButton)item.FindControl("ImgBtnCancel");
        txt_Discount.Text = txt_Discount.Text.Split('.')[0];
        TextBox txt_PPU = (TextBox)item.FindControl("txt_PPU");
        txt_Discount.Enabled = true;
        txtQnty.Enabled = true;
        txt_PPU.Enabled = true;
        imbtn.Visible = false;
        btnupdate.Visible = true;
        btnCancel.Visible = true;


    }
    /// <summary>
    /// On Cancle clik The Row is disabled to do Editing the Requisition Items
    /// </summary>
    protected void CancelClick(object sender, EventArgs e)
    {
        ImageButton imbtn = (ImageButton)sender;

        var item = (RepeaterItem)imbtn.NamingContainer;
        ImageButton btnupdate = (ImageButton)item.FindControl("btnUpdate");
        TextBox txtQnty = (TextBox)item.FindControl("txtQnty");
        ImageButton ImgUpdate = (ImageButton)item.FindControl("ImgUpdate");
        TextBox txt_Discount = (TextBox)item.FindControl("txt_Discount");
        TextBox txt_PPU = (TextBox)item.FindControl("txt_PPU");
        txtQnty.Enabled = false;
        itemid.Value = imbtn.CommandArgument.ToString();
        btnupdate.Visible = false;
        imbtn.Visible = false;
        ImgUpdate.Visible = true;
        txt_Discount.Enabled = false;
        txt_PPU.Enabled = false;


    }
    /// <summary>
    /// Remove the Requisition Item 
    /// </summary>
    protected void deleteClick(object sender, ImageClickEventArgs e)                                    //-- Remove  Item From the Requisition
    {
        try
        {
            ImageButton ibtn = (ImageButton)sender;
            var item = (RepeaterItem)ibtn.NamingContainer;
            ImageButton btnupdate = (ImageButton)item.FindControl("ImgDelete");
            BLL_PURC_Purchase objinvntryitem = new BLL_PURC_Purchase();
            string a = ibtn.CommandArgument.ToString();
            string b = btnupdate.CommandArgument.ToString();

            int result = objinvntryitem.DeleteSupplyItem(btnupdate.CommandArgument.ToString(), DocCode);
            BindItmPreviewRpt();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Onbutton Currency click Bind the Currency Dropdown List  and Enable it to Select the Currency 
    /// </summary>
    protected void BtnCurrency_Click(object sender, EventArgs e)                                        //-- Bind Currenct Dropdownlist
    {
        DataSet dataforCrncy = new DataSet();
        Button BtnCurrency = (Button)sender;
        var item = (RepeaterItem)BtnCurrency.NamingContainer;
        DropDownList ddlCurrency = (DropDownList)item.FindControl("ddlCurrency");
        if (ddlCurrency.Items.Count < 1)
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                dataforCrncy = objTechService.getCurrentRates();

            }
            ddlCurrency.DataSource = dataforCrncy.Tables[0];
            ddlCurrency.DataValueField = "Exch_rate";
            ddlCurrency.DataTextField = "Curr_Code";
            ddlCurrency.DataBind();
            if (BtnCurrency.Text != "USD") { ddlCurrency.SelectedItem.Text = BtnCurrency.Text; } else { ddlCurrency.SelectedIndex = 28; }

        }
        ddlCurrency.Visible = true;
        BtnCurrency.Visible = false;



    }
    /// <summary>
    /// Search the supplier in the Grid and bind the Grid 
    /// </summary>
    protected void searchimg_Click(object sender, ImageClickEventArgs e)
    {
        BindSupplierView();
        GV_SupplierList.Visible = true;
        HD_SelectedSupplier.Value = string.Empty;
        //SelectID.Visible = true;
    }
    /// <summary>
    /// Checking the Supplier Status on Selecting the Supplier From the grid and Changing the Row color
    /// if Supplie is not Approved can't select the supplier with the alert message to "approve the supplier First" 
    /// </summary>
    //protected void selected_suppchange(object sender, EventArgs e)
    //{
    //    CheckBox rb = (CheckBox)sender;
    //    GridViewRow gvrow = (GridViewRow)rb.NamingContainer;
    //    HiddenField HF_SupStatus = (HiddenField)gvrow.FindControl("HD_status");
    //    if (HF_SupStatus.Value == "Approved")
    //    {
    //        for (int i = 0; i < GV_SupplierList.Rows.Count; i++)
    //        {
    //            CheckBox rbtn = (CheckBox)GV_SupplierList.Rows[i].FindControl("rbtn_suppselect");
    //            if (gvrow.RowIndex != i)
    //            {
    //                rbtn.Checked = false;
    //                GV_SupplierList.Rows[i].BackColor = System.Drawing.Color.White;
    //            }
    //            else if (GV_SupplierList.Rows.Count == 1)
    //            {
    //                if (rbtn.Checked == false) { GV_SupplierList.Rows[i].BackColor = System.Drawing.Color.White; }
    //                else { HD_SelectedSupplier.Value = GV_SupplierList.Rows[i].Cells[0].Text; GV_SupplierList.Rows[i].BackColor = System.Drawing.Color.SkyBlue; }
    //            }
    //            else
    //            {
    //                HD_SelectedSupplier.Value = GV_SupplierList.Rows[i].Cells[0].Text;
    //                GV_SupplierList.Rows[i].BackColor = System.Drawing.Color.SkyBlue;
    //                rbtn.Checked = true;
    //                HD_SelectedSupplier.Value = GV_SupplierList.Rows[i].Cells[0].Text;

    //            }
    //        }
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg45", "alert('The supplier you have selected is not approved. Please approve it in the ASL before proceeding');", true);
    //        rb.Checked = false;

    //    }
    //}


    protected void SelectID_Click(object sender, EventArgs e)
    {
        //bindSupp_port();

    }


    //-- Functions
    /// <summary>
    /// bind the Supplier Gridview and all the detalis initially saved against supplier if the supplier's status is "Approved"
    /// </summary>
    private void BindSupplierView()                                                                   //-- Bind Supplier List
    {
        try
        {
            DataSet dataforDisplay = new DataSet();
            DataTable dtvat = BLL_PURC_Common.PURC_Get_Sys_Variable(UDFLib.ConvertToInteger(GetSessionUserID()), "VAT");
            DataTable dtWithHold = BLL_PURC_Common.PURC_Get_Sys_Variable(UDFLib.ConvertToInteger(GetSessionUserID()), "WIthHoldTax");
            Item_bindddl(ddlVAt, dtvat, null);
            Item_bindddl(ddlWithHoldTax, dtWithHold, null);
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                dataforDisplay = objTechService.ConfiguredSupplierPreview(DocCode, VesselCode, txt_searchCodeName.Text);

            }
            if ((dataforDisplay.Tables[1].Rows.Count > 0 && txt_searchCodeName.Text.Length > 0) || (dataforDisplay.Tables[1].Rows.Count > 0 && dataforDisplay.Tables[1].Rows[0][0].ToString().Length < 3) || (dataforDisplay.Tables[1].Rows.Count < 1))
            {
                GV_SupplierList.DataSource = dataforDisplay.Tables[0];
                GV_SupplierList.DataBind();
                HD_SelectedSupplier.Value = dataforDisplay.Tables[1].Rows[0]["Supplier_Code"].ToString();
                bindSupp_port();
                ddlSupp_Port.SelectedValue = dataforDisplay.Tables[1].Rows[0]["DELIVERY_PORT"].ToString();
                txt_SuppDate.Text = UDFLib.ConvertUserDateFormat(dataforDisplay.Tables[1].Rows[0]["DELIVERY_DATE"].ToString().Length > 8 ? dataforDisplay.Tables[1].Rows[0]["DELIVERY_DATE"].ToString().Remove(10) : "");
                txt_Discount.Text = dataforDisplay.Tables[1].Rows[0]["DISCOUNT"].ToString().Split('.')[0];
                txt_Advance.Text = dataforDisplay.Tables[1].Rows[0]["Advance_Payment"].ToString().Split('.')[0];

            }

            else
            {
                GV_SupplierList.DataSource = dataforDisplay.Tables[1];
                GV_SupplierList.DataBind();
                //CheckBox rbtn = (CheckBox)GV_SupplierList.Rows[0].FindControl("rbtn_suppselect");
                //rbtn.Checked = true;
                GV_SupplierList.Rows[0].BackColor = System.Drawing.Color.SkyBlue;
                HD_SelectedSupplier.Value = dataforDisplay.Tables[1].Rows[0]["Supplier_Code"].ToString();
                bindSupp_port();
                ddlSupp_Port.SelectedValue = dataforDisplay.Tables[1].Rows[0]["DELIVERY_PORT"].ToString();
                txt_SuppDate.Text = UDFLib.ConvertUserDateFormat(dataforDisplay.Tables[1].Rows[0]["DELIVERY_DATE"].ToString().Length > 8 ? dataforDisplay.Tables[1].Rows[0]["DELIVERY_DATE"].ToString().Remove(10) : "");
                txt_Discount.Text = dataforDisplay.Tables[1].Rows[0]["DISCOUNT"].ToString().Split('.')[0];
                txt_Advance.Text = dataforDisplay.Tables[1].Rows[0]["Advance_Payment"].ToString().Split('.')[0];
                ddlVAt.SelectedValue = dataforDisplay.Tables[1].Rows[0]["VAT_Rate"].ToString().Length != null ? dataforDisplay.Tables[1].Rows[0]["VAT_Rate"].ToString() : "0";
                ddlWithHoldTax.SelectedValue = dataforDisplay.Tables[1].Rows[0]["Withholding_Tax_Rate"].ToString() != null ? dataforDisplay.Tables[1].Rows[0]["Withholding_Tax_Rate"].ToString() : "0";

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    /// <summary>
    /// Bind the requisition Items list
    /// Disable Withhod Tax and VAT Dropdown if It is nonConfigurable ()
    /// </summary>
    private void BindItmPreviewRpt()                                                                  // Bind requisition Subcatalogue Repeater (Main Grid)
    {
        try
        {
            DataTable distinctValues;
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                dataforDisplay = objTechService.GetReqItemsPreview(ReqsnCode, VesselCode, DocCode);
            }
            if (dataforDisplay.Tables[0].Rows.Count <= 0)
            {
                String msg = String.Format("alert('This Requisition has been finalized or Deleted.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            }
            else
            {
                DataView view = new DataView(dataforDisplay.Tables[0]);
                distinctValues = view.ToTable(true, "SubCatalog", "SubCatalogID");
                rptMain.DataSource = distinctValues;
                rptMain.DataBind();
            }

            for (int i = 0; i < dataforDisplay.Tables[3].Rows.Count; i++)
            {
                if (dataforDisplay.Tables[3].Rows[i]["Property_ID"].ToString() == "1")
                {
                    ddlVAt.Enabled = true;
                }
                else { ddlVAt.Enabled = false; }
                if (dataforDisplay.Tables[3].Rows[i]["Property_ID"].ToString() == "2")
                {
                    ddlWithHoldTax.Enabled = true;
                }
                else
                {
                    ddlWithHoldTax.Enabled = false;
                }

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Calculate Discount,Total Price and Quantity on Items
    /// </summary>
    protected void bindprice(decimal previousrate)                                                    //-- Calculate Discount,Total Price and Quantity on Items
    {
        decimal tp = 0;

        for (int i = 0; i < rptMain.Items.Count; i++)
        {
            Repeater rptChild = (Repeater)rptMain.Items[i].FindControl("rptChild");

            for (int j = 0; j < rptChild.Items.Count; j++)
            {
                TextBox txtPPU = (TextBox)rptChild.Items[j].FindControl("txt_PPU");
                TextBox txtTP = (TextBox)rptChild.Items[j].FindControl("txtTP");
                TextBox txtDiscount = (TextBox)rptChild.Items[j].FindControl("txt_Discount");
                TextBox txtQnty = (TextBox)rptChild.Items[j].FindControl("txtQnty");
                DropDownList ddl_Itemvat = (DropDownList)rptChild.Items[j].FindControl("ddl_ItemVAT");
                DropDownList ddl_ItemWithHold = (DropDownList)rptChild.Items[j].FindControl("ddl_item_Withhold");
                decimal PPU;
                if (Convert.ToDecimal(Session["CurrentCurr"]) != 0)
                {
                    PPU = System.Math.Round((Convert.ToDecimal(txtPPU.Text) * Convert.ToDecimal(Session["CurrentCurr"]) / previousrate), 4);

                }
                if (Convert.ToDecimal(Session["CurrentCurr"]) != 0)
                {
                    decimal TPBeforeD = Convert.ToDecimal(txtQnty.Text) * Convert.ToDecimal(txtPPU.Text);
                    decimal tpAfterD = System.Math.Round(TPBeforeD - (TPBeforeD * Convert.ToDecimal(txtDiscount.Text) / 100), 4);
                    decimal VatAmount = System.Math.Round((tpAfterD * Convert.ToDecimal(ddl_Itemvat.SelectedValue) / 100), 4);
                    decimal WitHldAmount = System.Math.Round((tpAfterD * Convert.ToDecimal(ddl_ItemWithHold.SelectedValue) / 100), 4);
                    decimal ActualTP = System.Math.Round(tpAfterD + VatAmount - WitHldAmount, 2);
                    txtTP.Text = ActualTP.ToString();
                    tp = tp + ActualTP;
                }
            }

        }
        HD_TP.Value = tp.ToString();

    }
    /// <summary>
    /// Do Validation if all Mandatory Fields are Filled
    /// </summary>                                               
    public bool ISvalid()                                                                             //-- Do Validation
    {
        bool check = false;
        try
        {

            int count = 0;
            for (int i = 0; i < rptMain.Items.Count; i++)
            {

                Repeater rptChild = (Repeater)rptMain.Items[i].FindControl("rptChild");
                for (int j = 0; j < rptChild.Items.Count; j++)
                {
                    TextBox txtPPU = (TextBox)rptChild.Items[j].FindControl("txt_PPU");
                    TextBox txtDiscount = (TextBox)rptChild.Items[j].FindControl("txt_Discount");
                    TextBox txtQnty = (TextBox)rptChild.Items[j].FindControl("txtQnty");
                    DropDownList ddlvat = (DropDownList)rptChild.Items[j].FindControl("ddl_ItemVAT");
                    DropDownList ddlWithhod = (DropDownList)rptChild.Items[j].FindControl("ddl_item_Withhold");
                    if (txtPPU.Text.Length < 1 || txtDiscount.Text.Length < 1 || txtQnty.Text.Length < 1 || ddlvat.SelectedIndex == 0 || ddlWithhod.SelectedIndex == 0)
                    { check = true; }
                }

                if (count == 0) { check = true; } else { check = false; }
                if (ddlVAt.SelectedIndex == 0 && ddlVAt.Enabled == true) { check = false; }
                if (ddlWithHoldTax.SelectedIndex == 0 && ddlWithHoldTax.Enabled == true) { check = false; }
                if (ddlSupp_Port.SelectedIndex == 0 && ddlSupp_Port.Enabled == true) { check = false; }
                if (txt_Discount.Text.Length == 0 && txt_Discount.Enabled == true) { check = false; }
                if (txt_SuppDate.Text.Length == 0 && txt_SuppDate.Enabled == true) { check = false; }
                if (txt_Advance.Text.Length == 0 && txt_Advance.Enabled == true) { check = false; }
                if (HD_SelectedSupplier.Value == string.Empty) { check = false; }

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        return Convert.ToBoolean(check);

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    /// <summary>
    /// Bind Supplier Port DropdownList
    /// </summary>
    protected void bindSupp_port()                                                              //-- Bind Supplier Port Dropdown list
    {
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            try
            {
                DataTable dtPort = objTechService.getDeliveryPort();//objTechService.Get_SelectedPort().Tables[0];
                ddlSupp_Port.DataSource = dtPort;
                ddlSupp_Port.DataTextField = "Port_Name";
                ddlSupp_Port.DataValueField = "Id";
                ddlSupp_Port.DataBind();
                ddlSupp_Port.Items.Insert(0, new ListItem("-SELECT-", "0"));
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }

        }
    }                                                                
    /// <summary>
    /// Bind DropDownList 
    /// </summary>
    public void Item_bindddl(DropDownList ddl, DataTable dt, string selecttext)
    {
        try
        {
            ddl.DataSource = dt;
            ddl.DataTextField = "VARIABLE_NAME";
            ddl.DataValueField = "VARIABLE_VALUE";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("-SELECT-", "0"));
            if (selecttext != "" || selecttext != "0.00" || selecttext != null) { ddl.SelectedValue = selecttext; }
            else if (selecttext == null) { ddl.SelectedValue = "0"; }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    #region Purchase Question and Answers
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
    private string GetFormType()
    {
        try
        {
            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
            DataSet ds = objTechService.GET_PURC_DEP_ON_DOCCODE(DocCode);
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
    private void BindPurcQuestion()
    {
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        try
        {
            string FormType = GetFormType();
            DataSet dt = objTechService.Get_Purc_Questions(DocCode, FormType);
            if (dt.Tables[0].Rows.Count > 0)
            {
                grdQuestion.DataSource = dt;
                grdQuestion.DataBind();
            }
            // else { tbl.Rows[0].Cells[0].Attributes.Add("style", "display:none"); }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void grdQuestion_RowDataBound(object sender, GridViewRowEventArgs e)
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
    private void SaveQuestionnaire(string ReqsnCode)
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
                int retval = objTechService.Insert_Purc_Question(ReqsnCode, Convert.ToString(DocCode), Convert.ToInt32(Session["USERID"]), dtQuest, 1, UDFLib.ConvertIntegerToNull(VesselCode));

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #endregion


}
