using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using System.Data;
using SMS.Properties.PURC;
using SMS.Business.Crew;
using SMS.Properties;
using SMS.Business.Infrastructure;

public partial class Purc_Config_Settings : System.Web.UI.Page
{
    public string titlename = "";
    int rowindex;
    public bool check = false;
    POConfig POconfig = new POConfig();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    DataTable dt = new DataTable();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    UserAccess objUserAcess = new UserAccess();
    DataSet ds_MandatDta;
    BLL_PURC_ReqsnMandatory objMandatdata = new BLL_PURC_ReqsnMandatory();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();                   // User Validation
            Get_Auto_Req();                           // Bind Automatic requisition 
            BindDropdownPurchaseCrewRank();           // Bind Purchase Crew DropDown List    
            BindPurchaseRank();                       // Bind Purchase Crew Rank gridview
            filldata();                               // Bind requisitionType ,SupplierType and POType Listbox for Purchase Configuration setting    
            MandatFill();                             // Bind Mandatory Fields Checkbox List
            FillPOConfig();
        }
    }

    /// <summary>
    /// Tab1 - (Auto Requisition)
    /// Tab2 - (New Rank)
    /// Tab3 - (Configuration Setting)
    /// Tab4 - (Mandatory Fields)
    /// </summary>

    //-- Tab 1
    #region AutoRequisition
    private void SaveAutoRequsition()
    {
        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
        try
        {
            int Company_ID = Convert.ToInt32(Session["USERCOMPANYID"]);
            int result = objBLLPurc.INSERT_AUTOMATIC_REQUISTION(Company_ID, chkAutoReq.Checked, chkSuppConf.Checked);
            string js = "alert('Purchase Configuration Settings Saved Successfully..');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
        }
        catch (Exception ex)
        {
            string js = "alert('Error Occurred..');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //Saving details.
            SaveAutoRequsition();
            Get_Auto_Req();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    private void Get_Auto_Req()
    {
        //Bind Details.
        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
        try
        {
            int Company_ID = Convert.ToInt32(Session["USERCOMPANYID"]);
            DataTable DT = objBLLPurc.GET_AUTOMATIC_REQUISTION(Company_ID);
            if (DT.Rows.Count > 0)
            {
                chkAutoReq.Checked = Convert.ToBoolean(DT.Rows[0]["Is_Auto_Requisition"]);
                chkSuppConf.Checked = Convert.ToBoolean(DT.Rows[0]["Is_Req_Supplier_Confirm"]);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #endregion


    //-- Tab 2
    #region Purchase Crew Settings
    private void BindDropdownPurchaseCrewRank()
    {
        try
        {
            DataTable dtRanks = objCrewAdmin.Get_RankList();
            BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();

            var r = from dr in dtRanks.AsEnumerable()
                    select dr.Field<Int32>("ID");
            var p = from dr in objBLLPurc.Get_Purc_Rank().AsEnumerable()
                    select dr.Field<Int32>("RankID");
            var iDr = r.Except(p);

            var missing = from table1 in dtRanks.AsEnumerable()
                          where iDr.Contains(table1.Field<Int32>("ID"))
                          select table1;

            DataTable dt = missing.CopyToDataTable();
            if (dt.Rows.Count > 0)
            {
                ddlPurc_CrewRank.DataSource = dt;
                ddlPurc_CrewRank.DataTextField = "Rank_Short_Name";
                ddlPurc_CrewRank.DataValueField = "id";
                ddlPurc_CrewRank.DataBind();
                ddlPurc_CrewRank.Items.Insert(0, "-Select-");

            }
            ddlPurc_CrewRank.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnPurcAddRank_Click(object sender, EventArgs e)
    {
        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();

        if (ddlPurc_CrewRank.SelectedValue != "-Select-")
        {
            int RankID = Convert.ToInt32(ddlPurc_CrewRank.SelectedValue == "-Select-" ? "0" : ddlPurc_CrewRank.SelectedValue);
            int responseid = objBLLPurc.INS_Rank_Purc(RankID, "A", GetSessionUserID());
            string js = "alert('Data Added Successfully..');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
            BindPurchaseRank();
            BindDropdownPurchaseCrewRank();
        }
        else
        {
            string js = "alert('Select Rank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
            ddlPurc_CrewRank.Focus();
        }

    }
    private void BindPurchaseRank()
    {
        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
        try
        {
            DataTable dt = objBLLPurc.Get_Purc_Rank();
            grdPurc_Crew_Rank.DataSource = dt;
            grdPurc_Crew_Rank.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void onDelete(object sender, EventArgs e)
    {
        BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();

        ImageButton objImage = (ImageButton)sender;

        string[] commandArgs = objImage.CommandArgument.ToString().Split(new char[] { ',' });
        Int32 RankID = Convert.ToInt32(commandArgs[0]);
        int responseid = objBLLPurc.INS_Rank_Purc(RankID, "D", GetSessionUserID());
        BindPurchaseRank();
        BindDropdownPurchaseCrewRank();


    }
    #endregion


    //-- Tab 3
    #region PurchaseConfiguration
    BLL_PURC_Config_PO objBLLconfig = new BLL_PURC_Config_PO();

    //--Events

    /// <summary>
    /// If "Quotation Required" is checked then Enables "No Of Quotaion" to Enter the "No of Quotations"
    /// If "Quotation Required" is unchecked then Disables the "No Of Quotaion" with 0 Quantity
    /// </summary>
    protected void Quote_required_onChanged(object sender, EventArgs e)
    {
        if (RBtn_Quote_required.Checked == false)
        {
            txt_QuoteNo.Text = "0";
            txt_QuoteNo.Enabled = false;
        }
        else { txt_QuoteNo.Text = ""; txt_QuoteNo.Enabled = true; }
    }

    /// <summary>
    /// On POType selection Change Updates the Configuration initially save on the Screen
    /// </summary>
    protected void potype_change(object sender, EventArgs e)
    {
        Update();
    }
    /// <summary>
    /// if Supplier type ALL is Checked then check All the Supplier in the List
    /// if Supplier type ALL is unchecked then Uncheck All the Supplier in the List
    /// </summary>
    protected void SupplierTypeChange(object sender, EventArgs e)
    {
        if (LbSupplierType.Items[0].Selected == true)
        {
            for (int i = 1; i < LbSupplierType.Items.Count; i++)
            {
                LbSupplierType.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 1; i < LbSupplierType.Items.Count; i++)
            {
                LbSupplierType.Items[i].Selected = false;
            }
        }
    }

    /// <summary>
    /// if POType already Saved then Updates the PO Configuration else Inserts the new configuration for the selected POType
    /// </summary>
    protected void submit_Click(object sender, EventArgs e)
    {
        string scriptmsg = "";
        if (LBpotype.SelectedValue.ToString() != "0")
        {
            if (LBReqsntype.SelectedValue.Count() > 0)
            {
                if (LbSupplierType.SelectedValue.Count() > 0)
                {
                    get_config(); // SET the PO_CONFIG Properties
                    string result = "";

                    result = objBLLconfig.PURC_Save_Config__BLL(POconfig, dt); // SAVE the PO Configurations for the Selected PO
                    string msg = submit.Text == "Add" ? "Added" : "Updated";
                    scriptmsg = "Purchase Configurations Setting " + msg + " Successfully ";
                }
                else
                {
                    scriptmsg = "Select atleast one Supplier Type";
                }
            }
            else
            {
                scriptmsg = "Select atleast one Requisition Type";
            }
        }
        else { scriptmsg = "Please select The POType"; }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "alert('" + scriptmsg + "');", true);
    }

    // -- Functions

    /// <summary>
    /// Gets the list of POType,requisition Type and SupplierType to bind the List box Controls
    /// </summary>
    public void filldata()
    {
        try
        {
            DataSet ddllist = objBLLconfig.Get_ddlListBLL();

            LbSupplierType.DataSource = ddllist.Tables[2];
            LbSupplierType.DataTextField = "VARIABLE_NAME";
            LbSupplierType.DataValueField = "ID";
            LbSupplierType.DataBind();
            LbSupplierType.Items.Insert(0, new ListItem("ALL", "0"));

            ChkBxList_SuppType.DataSource = ddllist.Tables[2];
            ChkBxList_SuppType.DataTextField = "VARIABLE_NAME";
            ChkBxList_SuppType.DataValueField = "ID";
            ChkBxList_SuppType.DataBind();
            ChkBxList_SuppType.Items.Insert(0, new ListItem("ALL", "0"));


            ChkBxList_ReqsnType.DataSource = ddllist.Tables[1];
            ChkBxList_ReqsnType.DataTextField = "Description";
            ChkBxList_ReqsnType.DataValueField = "Code";
            ChkBxList_ReqsnType.DataBind();

            LBReqsntype.DataSource = ddllist.Tables[1];
            LBReqsntype.DataTextField = "Description";
            LBReqsntype.DataValueField = "Code";
            LBReqsntype.DataBind();

            LBpotype.DataSource = ddllist.Tables[0];
            LBpotype.DataTextField = "VARIABLE_NAME";
            LBpotype.DataValueField = "ID";
            LBpotype.DataBind();

            LBpotype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    string POtypeid = "";
    /// <summary>
    /// if POType already Saved then Updates the PO Configuration else Inserts the new configuration for the selected POType
    /// </summary>
    protected void Update()
    {
        try
        {

            DataSet dsConfig = new DataSet();
            dsConfig = objBLLconfig.PURC_Get_Config_BLL(LBpotype.SelectedValue == "" ? "0" : LBpotype.SelectedValue); // Get the Configured Fields already Saved

            int rowcount = dsConfig.Tables[0].Rows.Count;
            if (rowcount > 0) { submit.Text = "Update"; } else { submit.Text = "Add"; }
            RBtn_Owner.Checked = Convert.ToBoolean(Convert.ToInt32(rowcount > 0 ? dsConfig.Tables[0].Rows[0]["Auto_Owner_Selection"] : 0));
            Rbtn_DeliveryPort.Checked = Convert.ToBoolean(Convert.ToInt32(rowcount > 0 ? dsConfig.Tables[0].Rows[0]["Delivery_Port"] : 0));
            RBtn_Delivery_Port_date.Checked = Convert.ToBoolean(Convert.ToInt32(rowcount > 0 ? dsConfig.Tables[0].Rows[0]["Delivery_Date"] : 0));
            RBtn_VesselMovement_Date.Checked = Convert.ToBoolean(Convert.ToInt32(rowcount > 0 ? dsConfig.Tables[0].Rows[0]["Vessel_Movement_Date"] : 0));
            RBtn_Quote_required.Checked = Convert.ToBoolean(Convert.ToInt32(rowcount > 0 ? dsConfig.Tables[0].Rows[0]["Direct_Quotation"] : 0));
            txt_QuoteNo.Text = rowcount > 0 ? dsConfig.Tables[0].Rows[0]["Min_QTN_Required"].ToString() : "";
            RBtn_Item_Category.Checked = Convert.ToBoolean(Convert.ToInt32(rowcount > 0 ? dsConfig.Tables[0].Rows[0]["Item_Category"] : 0));
            RBtn_Vessel_Processing_PO.Checked = Convert.ToBoolean(Convert.ToInt32(rowcount > 0 ? dsConfig.Tables[0].Rows[0]["Vessel_Processing_PO"] : 0));
            RBtn_Enable_Free_text.Checked = Convert.ToBoolean(Convert.ToInt32(rowcount > 0 ? dsConfig.Tables[0].Rows[0]["Free_Text_Items_Addition"] : 0));
            RBtn_Copy_to_Vessel.Checked = Convert.ToBoolean(Convert.ToInt32(rowcount > 0 ? dsConfig.Tables[0].Rows[0]["Copy_To_Vessel"] : 0));
            RBtn_Sup_Po_Confirmation.Checked = Convert.ToBoolean(Convert.ToInt32(rowcount > 0 ? dsConfig.Tables[0].Rows[0]["Supplier_PO_Confirmation"] : 0));
            RBtn_Delivery_Confirm_Onbor.Checked = Convert.ToBoolean(Convert.ToInt32(rowcount > 0 ? dsConfig.Tables[0].Rows[0]["Office_Delivery_Confirmation"] : 0));
            Rbtn_Vessel_Delivery_Confirm.Checked = Convert.ToBoolean(Convert.ToInt32(rowcount > 0 ? dsConfig.Tables[0].Rows[0]["Vessel_Delivery_Confirmation"] : 0));
            RBtn_Withhold_tax.Checked = Convert.ToBoolean(Convert.ToInt32(rowcount > 0 ? dsConfig.Tables[0].Rows[0]["Witholding_Tax"] : 0));
            RBtn_Vat_Config_Purc.Checked = Convert.ToBoolean(Convert.ToInt32(rowcount > 0 ? dsConfig.Tables[0].Rows[0]["VAT_Config_By_Purchaser"] : 0));
            RBtn_require_verify.Checked = Convert.ToBoolean(Convert.ToInt32(rowcount > 0 ? dsConfig.Tables[0].Rows[0]["Verification_Required"] : 0));
            txt_AutoPoClosing.Text = rowcount > 0 ? dsConfig.Tables[0].Rows[0]["Auto_PO_Closing"].ToString() : "";


            for (int i = 0; i < LBReqsntype.Items.Count; i++)
            {
                LBReqsntype.Items[i].Selected = false;
            }
            for (int i = 0; i < LbSupplierType.Items.Count; i++)
            {
                LbSupplierType.Items[i].Selected = false;
            }
            for (int i = 0; i < LBReqsntype.Items.Count; i++)
            {
                DataRow[] foundRows = dsConfig.Tables[1].Select("Key_Value ='" + LBReqsntype.Items[i].Value + "' and Key_Value_Type = 'Requisition'");

                foreach (DataRow rows in foundRows)
                {
                    LBReqsntype.Items[i].Selected = true;
                }
            }

            for (int i = 0; i < LbSupplierType.Items.Count; i++)
            {
                DataRow[] foundRows = dsConfig.Tables[1].Select("Key_Value ='" + LbSupplierType.Items[i].Value + "' and Key_Value_Type = 'Supplier'");
                foreach (DataRow rows in foundRows)
                {
                    LbSupplierType.Items[i].Selected = true;
                }

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Get Configuration to save or update
    /// fill the datatable with the selected lists of Requisition and Supplier
    /// set the POconfig property
    /// </summary>
    public void get_config()
    {
        try
        {
            dt = new DataTable();
            dt.Columns.Add("Key_Value");
            dt.Columns.Add("Key_VAlue_Type");
            foreach (ListItem li in LBReqsntype.Items)
            {
                if (li.Selected)
                {

                    DataRow dr = dt.NewRow();
                    dr["Key_Value"] = li.Value;
                    dr["Key_VAlue_Type"] = "Requisition";
                    dt.Rows.Add(dr);

                }
            }

            foreach (ListItem li in LbSupplierType.Items)
            {
                if (li.Selected)
                {
                    if (LbSupplierType.Items[0] != li)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Key_Value"] = li.Value;
                        dr["Key_VAlue_Type"] = "Supplier";
                        dt.Rows.Add(dr);
                    }

                }
            }
            POconfig.POType = LBpotype.SelectedValue;
            POconfig.Owner = RBtn_Owner.Checked == true ? "1" : "0";
            POconfig.Delivery_Port = Rbtn_DeliveryPort.Checked == true ? "1" : "0";
            POconfig.Delivery_Port_date = RBtn_Delivery_Port_date.Checked == true ? "1" : "0";
            POconfig.Vessel_movement_Date = RBtn_VesselMovement_Date.Checked == true ? "1" : "0";
            POconfig.Vessel_Processing_PO = RBtn_Vessel_Processing_PO.Checked == true ? "1" : "0";
            POconfig.Item_Category = RBtn_Item_Category.Checked == true ? "1" : "0";
            POconfig.Quote_required = RBtn_Quote_required.Checked == true ? "1" : "0";
            POconfig.QuoteNo = txt_QuoteNo.Text;
            POconfig.Vessel_Delivery_Confirm = Rbtn_Vessel_Delivery_Confirm.Checked == true ? "1" : "0";
            POconfig.Enable_Free_text = RBtn_Enable_Free_text.Checked == true ? "1" : "0";
            POconfig.copy_to_vessel = RBtn_Copy_to_Vessel.Checked == true ? "1" : "0";
            POconfig.Sup_Po_Confirmation = RBtn_Sup_Po_Confirmation.Checked == true ? "1" : "0";
            POconfig.Vessel_Delivery_Confirm = Rbtn_Vessel_Delivery_Confirm.Checked == true ? "1" : "0";
            POconfig.Office_Delivery_Confirmation = Rbtn_Vessel_Delivery_Confirm.Checked == true ? "1" : "0";
            POconfig.Withhold_tax = RBtn_Withhold_tax.Checked == true ? "1" : "0";
            POconfig.Vat_Config_Purc = RBtn_Vat_Config_Purc.Checked == true ? "1" : "0";
            POconfig.require_verify = RBtn_require_verify.Checked == true ? "1" : "0";
            POconfig.Auto_POClosing = txt_AutoPoClosing.Text;
            POconfig.Currentuser = Session["userid"].ToString();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    #endregion


    //-- Tab 4
    #region Requisition Mandatory Fields
    /// <summary>
    /// update the Changet Mandatory Fields configuration
    /// </summary>
    protected void Update_Click(object sender, EventArgs e)
    {
        try
        {
            dt.Clear();
            dt.Columns.Add("Key_Value");
            dt.Columns.Add("Key_VAlue_Type");

            DataRow dr = dt.NewRow();
            dr["Key_Value"] = 10;
            dr["Key_VAlue_Type"] = CBLMandatoryFields.Items[9].Selected == true ? 1 : 0;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key_Value"] = 11;
            dr["Key_VAlue_Type"] = CBLMandatoryFields.Items[10].Selected == true ? 1 : 0;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key_Value"] = 12;
            dr["Key_VAlue_Type"] = CBLMandatoryFields.Items[11].Selected == true ? 1 : 0;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key_Value"] = 13;
            dr["Key_VAlue_Type"] = CBLMandatoryFields.Items[12].Selected == true ? 1 : 0;
            dt.Rows.Add(dr);


            objMandatdata.PURC_UPD_Mandatory_BLL(dt); // UPDATE the Mandatory Fields
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "alert('Mandatory Field Updated Successfully');", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    #endregion


    // -- Tab 5

    /// <summary>
    /// Fill the PO Configuration Gridview 
    /// </summary>
    public void FillPOConfig()
    {
        try
        {
            DataSet dsConfig = new DataSet();
            dsConfig = objBLLconfig.PURC_Get_Config_BLL("");
            gvPOConfig.DataSource = dsConfig.Tables[2];
            gvPOConfig.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }



    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUserAcess = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUserAcess.View == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");
        }
        if (objUserAcess.Admin == 0)
        {
            btnSave.Enabled = false;
            btnPurcAddRank.Enabled = false;
        }
    }

    /// <summary>
    /// Get Current Users ID from the Session
    /// </summary>
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    /// <summary>
    /// get and bind the Mandatory Fields check box list  save initially
    /// </summary>
    private void MandatFill()
    {
        try
        {
            ds_MandatDta = objMandatdata.Get_MandatoryData_BLL();
            CBLMandatoryFields.DataSource = ds_MandatDta.Tables[0];
            CBLMandatoryFields.DataTextField = "text";
            CBLMandatoryFields.DataValueField = "value";
            CBLMandatoryFields.DataBind();

            for (int i = 0; i < CBLMandatoryFields.Items.Count; i++)
            {
                CBLMandatoryFields.Items[i].Selected = Convert.ToBoolean(ds_MandatDta.Tables[0].Rows[i]["Mandatory"].ToString());
                if (i < 9) { CBLMandatoryFields.Items[i].Enabled = false; }


            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    /// <summary>
    /// requisitonType List Link Button clik on POConfig Grid , Shows the POpup of the Requisitiontypes list to select 
    /// </summary>
    protected void lnkReqsnClick(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string[] rqsn = btn.CommandArgument.ToString().Split(',');
        titlename = "RequisitionType";
        var rowitem = (GridViewRow)btn.NamingContainer;
        rowindex = rowitem.RowIndex;
        rowclicked.Text = rowindex.ToString();
        foreach (ListItem itm in ChkBxList_ReqsnType.Items)
        {
            if (rqsn.Contains(itm.Value))
            {
                itm.Selected = true;
            }
            else { itm.Selected = false; }
        }
        string reqsnmodal = String.Format("showModal('dvPopupreqsn',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Functions", reqsnmodal, true);
    }

    /// <summary>
    /// Opens popup of Requisition List to select
    /// </summary>
    protected void imgReqsnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        string[] rqsn = btn.CommandArgument.ToString().Split(',');
        titlename = "RequisitionType";
        var rowitem = (GridViewRow)btn.NamingContainer;
        rowindex = rowitem.RowIndex;
        rowclicked.Text = rowindex.ToString();

        foreach (ListItem itm in ChkBxList_ReqsnType.Items)
        {
            if (rqsn.Contains(itm.Value))
            {
                itm.Selected = true;
            }
            else { itm.Selected = false; }
        }
        string reqsnmodal = String.Format("showModal('dvPopupreqsn',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Functions", reqsnmodal, true);
    }

    /// <summary>
    /// SupplierTypes list linkbutton click, shoes the popup of the SuppliersType List to select
    /// </summary>
    protected void lnkSuppClick(object sender, EventArgs e)
    {
        titlename = "SupplierType";
        LinkButton btn = (LinkButton)sender;
        string[] rqsn = btn.CommandArgument.ToString().Split(',');
        var rowitem = (GridViewRow)btn.NamingContainer;
        rowindex = rowitem.RowIndex;
        rowclicked.Text = rowindex.ToString();
        foreach (ListItem itm in ChkBxList_SuppType.Items)
        {
            if (rqsn.Contains(itm.Value))
            {
                itm.Selected = true;
            }
            else { itm.Selected = false; }
        }
        string reqsnmodal = String.Format("showModal('dvPopupSupp',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Functions", reqsnmodal, true);
    }

    /// <summary>
    /// opens Popup of Suppliertype List to Select
    /// </summary>
    protected void imgSuppClick(object sender, EventArgs e)
    {

        ImageButton btn = (ImageButton)sender;
        string[] rqsn = btn.CommandArgument.ToString().Split(',');
        var rowitem = (GridViewRow)btn.NamingContainer;
        rowindex = rowitem.RowIndex;
        rowclicked.Text = rowindex.ToString();
        foreach (ListItem itm in ChkBxList_SuppType.Items)
        {
            if (rqsn.Contains(itm.Value))
            {
                itm.Selected = true;
            }
            else { itm.Selected = false; }
        }
        rowclicked.Text = rowindex.ToString();
        string reqsnmodal = String.Format("showModal('dvPopupSupp',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Functions", reqsnmodal, true);
    }

    /// <summary>
    /// Enables the PO configuration Row to Edit on Update Image Button Click
    /// </summary>
    protected void updateClick(object sender, EventArgs e)                                              // Onclick Update image enable Update 
    {
        ImageButton imbtn = (ImageButton)sender;
        int ReqQuoteIndex = 0;
        var item = (GridViewRow)imbtn.NamingContainer;
        for (int i = 3; i < gvPOConfig.Columns.Count - 2; i++)
        {
            string aa = gvPOConfig.HeaderRow.Cells[i].Text;
            if (gvPOConfig.HeaderRow.Cells[i].Text.Trim() == "AutoClosing of PO" || gvPOConfig.HeaderRow.Cells[i].Text.Trim() == "Minimum quotations number")
            {
                TextBox txt = (TextBox)gvPOConfig.Rows[item.RowIndex].Cells[i].Controls[1];
                if (gvPOConfig.HeaderRow.Cells[i].Text.Trim() == "Minimum quotations number")
                {
                    CheckBox chk = (CheckBox)gvPOConfig.Rows[item.RowIndex].Cells[ReqQuoteIndex].Controls[1];
                    if (chk.Checked)
                    {
                        txt.Enabled = true;
                    }
                    else { txt.Enabled = false; txt.Text = "0"; }
                }

            }
            else if (gvPOConfig.HeaderRow.Cells[i].Text.Trim() == "Quotation Required")
            {
                ReqQuoteIndex = i;
                CheckBox chk = (CheckBox)gvPOConfig.Rows[item.RowIndex].Cells[i].Controls[1];
                chk.Enabled = true;
            }
            else
            {

                CheckBox chk = (CheckBox)gvPOConfig.Rows[item.RowIndex].Cells[i].Controls[1];
                chk.Enabled = true;
            }
        }
        rowclicked.Text = item.RowIndex.ToString();
        ImageButton btnupdate = (ImageButton)item.FindControl("btnUpdate");
        ImageButton btnCancel = (ImageButton)item.FindControl("ImgBtnCancel");
        imbtn.Visible = false;
        btnupdate.Visible = true;
        btnCancel.Visible = true;
    }
    protected void grd_RowDeleting(object sender, GridViewUpdateEventArgs e)
    {
    }

    /// <summary>
    /// Cancles with out saving the Changed Configuration
    /// </summary>
    protected void Cancel_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imbtn = (ImageButton)sender;

        var item = (GridViewRow)imbtn.NamingContainer;
        for (int i = 3; i < gvPOConfig.Columns.Count - 2; i++)
        {
            if (gvPOConfig.HeaderRow.Cells[i].Text == "Auto Closing of PO" || gvPOConfig.HeaderRow.Cells[i].Text == " Minimum quotations number")
            {
                TextBox txt = (TextBox)gvPOConfig.Rows[item.RowIndex].Cells[i].Controls[1];
                txt.Enabled = false;
            }
            else
            {
                CheckBox chk = (CheckBox)gvPOConfig.Rows[item.RowIndex].Cells[i].Controls[1];
                chk.Enabled = false;
            }
        }

        ImageButton btnupdate = (ImageButton)item.FindControl("btnUpdate");
        ImageButton ImgUpdate = (ImageButton)item.FindControl("ImgUpdate");
        imbtn.Visible = false;
        btnupdate.Visible = false;
        ImgUpdate.Visible = true;

    }

    /// <summary>
    /// Saves the changed Configuration and disables the Editable row
    /// </summary>
    protected void SaveClick(object sender, ImageClickEventArgs e)                                              // Onclick Save image
    {
        ImageButton imbtn = (ImageButton)sender;

        bool isMinQuoteValid = false;
        var item = (GridViewRow)imbtn.NamingContainer;

        string minquote = ""; bool ReqQuote =false;
        for (int i = 3; i < gvPOConfig.Columns.Count - 2; i++)
        {
            if (gvPOConfig.HeaderRow.Cells[i].Text.Trim() == "Minimum quotations number" )
            {
                TextBox txt = (TextBox)gvPOConfig.Rows[item.RowIndex].Cells[i].Controls[1];
                minquote = txt.Text;
            }
            if (gvPOConfig.HeaderRow.Cells[i].Text.Trim() == "Quotation Required")
            {
                CheckBox chk = (CheckBox)gvPOConfig.Rows[item.RowIndex].Cells[i].Controls[1];
                ReqQuote = chk.Checked;
            }

        }
        if (ReqQuote == true && Convert.ToInt32(minquote == "" ? "0" : minquote) > 0)
        { isMinQuoteValid = true; }
        else if (ReqQuote == false && Convert.ToInt32(minquote == "" ? "0" : minquote) == 0)
        { isMinQuoteValid = true; }
        else { isMinQuoteValid = false;  }

        if (isMinQuoteValid)
        {
            for (int i = 3; i < gvPOConfig.Columns.Count - 2; i++)
            {
                if (gvPOConfig.HeaderRow.Cells[i].Text.Trim() == "AutoClosing of PO" || gvPOConfig.HeaderRow.Cells[i].Text.Trim() == "Minimum quotations number")
                {
                    TextBox txt = (TextBox)gvPOConfig.Rows[item.RowIndex].Cells[i].Controls[1];
                    txt.Enabled = false;
                }
                else
                {
                    CheckBox chk = (CheckBox)gvPOConfig.Rows[item.RowIndex].Cells[i].Controls[1];
                    chk.Enabled = false;
                }
            }
            get_configData(item.RowIndex);
            string result = "";

            result = objBLLconfig.PURC_Save_Config__BLL(POconfig, dt); // SAVE the PO Configurations for the Selected PO
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "alert(' Configuration Setting Updated Successfully');", true); ;
            ImageButton btnupdate = (ImageButton)item.FindControl("ImgUpdate");
            ImageButton btnCancel = (ImageButton)item.FindControl("ImgBtnCancel");
            imbtn.Visible = false;
            btnupdate.Visible = true;
            btnCancel.Visible = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "alert('Number of Quotation Is Required');", true);
 
        }
    }

    /// <summary>
    /// Sets the POconfig property , selected requisitionTypeList and SupplytypesList Datatable to save 
    /// </summary>
    public void get_configData(int index)
    {
        try
        {
            dt = new DataTable();
            dt.Columns.Add("Key_Value");
            dt.Columns.Add("Key_VAlue_Type");
            LinkButton lnk_ReqType = (LinkButton)gvPOConfig.Rows[index].Cells[1].Controls[1];
            LinkButton lnk_SuppType = (LinkButton)gvPOConfig.Rows[index].Cells[2].Controls[1];
            LinkButton lnk_potype = (LinkButton)gvPOConfig.Rows[index].Cells[0].Controls[1];
            string[] ReqtypeVal = lnk_ReqType.CommandArgument.ToString().Split(',');
            string[] SuppType = lnk_SuppType.CommandArgument.ToString().Split(',');
            //if( lnk_ReqType.Text != "-" && lnk_SuppType.Text !="-")
            // {

            for (int i = 0; i < ReqtypeVal.Length; i++)
            {
                if (ReqtypeVal[i] != "-")
                {
                    DataRow dr = dt.NewRow();
                    dr["Key_Value"] = ReqtypeVal[i];
                    dr["Key_VAlue_Type"] = "Requisition";
                    dt.Rows.Add(dr);
                }
            }
            for (int i = 0; i < SuppType.Length; i++)
            {
                if (SuppType[i] != "-")
                {
                    DataRow dr = dt.NewRow();
                    dr["Key_Value"] = SuppType[i];
                    dr["Key_VAlue_Type"] = "Supplier";
                    dt.Rows.Add(dr);
                }
            }

            POconfig.POType = lnk_potype.CommandArgument.ToString();
            POconfig.Owner = getvalue(null, (CheckBox)gvPOConfig.Rows[index].Cells[3].Controls[1]);
            POconfig.Delivery_Port = getvalue(null, (CheckBox)gvPOConfig.Rows[index].Cells[4].Controls[1]);
            POconfig.Delivery_Port_date = getvalue(null, (CheckBox)gvPOConfig.Rows[index].Cells[5].Controls[1]);
            POconfig.Vessel_movement_Date = getvalue(null, (CheckBox)gvPOConfig.Rows[index].Cells[6].Controls[1]);
            POconfig.Item_Category = getvalue(null, (CheckBox)gvPOConfig.Rows[index].Cells[7].Controls[1]);
            POconfig.Quote_required = getvalue(null, (CheckBox)gvPOConfig.Rows[index].Cells[8].Controls[1]);
            POconfig.QuoteNo = getvalue((TextBox)gvPOConfig.Rows[index].Cells[9].Controls[1], null);
            POconfig.Vessel_Processing_PO = getvalue(null, (CheckBox)gvPOConfig.Rows[index].Cells[10].Controls[1]);
            POconfig.Enable_Free_text = getvalue(null, (CheckBox)gvPOConfig.Rows[index].Cells[11].Controls[1]);
            POconfig.copy_to_vessel = getvalue(null, (CheckBox)gvPOConfig.Rows[index].Cells[12].Controls[1]);
            POconfig.Sup_Po_Confirmation = getvalue(null, (CheckBox)gvPOConfig.Rows[index].Cells[13].Controls[1]);
            POconfig.Vessel_Delivery_Confirm = getvalue(null, (CheckBox)gvPOConfig.Rows[index].Cells[14].Controls[1]);
            POconfig.Office_Delivery_Confirmation = getvalue(null, (CheckBox)gvPOConfig.Rows[index].Cells[15].Controls[1]);
            POconfig.Withhold_tax = getvalue(null, (CheckBox)gvPOConfig.Rows[index].Cells[16].Controls[1]);
            POconfig.Vat_Config_Purc = getvalue(null, (CheckBox)gvPOConfig.Rows[index].Cells[17].Controls[1]);
            POconfig.require_verify = getvalue(null, (CheckBox)gvPOConfig.Rows[index].Cells[18].Controls[1]);
            POconfig.Auto_POClosing = getvalue((TextBox)gvPOConfig.Rows[index].Cells[19].Controls[1], null);
            POconfig.Currentuser = Session["userid"].ToString();


            //}
            //else { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "alert(' Please select the RequisitionType And SupplierType');", true); }




        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// returns if the Checkbox is seleted  or Textbox is empty or not
    /// </summary>
    public string getvalue(TextBox txt, CheckBox chk)
    {
        if (txt == null)
            return Convert.ToInt32(chk.Checked).ToString();
        else
            return Convert.ToInt32(txt.Text).ToString();
    }

    /// <summary>
    /// Saves the Suppliertype Selected in the PopUp
    /// </summary>
    protected void onSave(object sender, EventArgs e)
    {
        string txtSupptype = "", ValSupptype = "";

        for (int i = 0; i < ChkBxList_SuppType.Items.Count; i++)
        {
            if (ChkBxList_SuppType.Items[i].Selected)
            {
                txtSupptype += ChkBxList_SuppType.Items[i].Text + ",";
                ValSupptype += ChkBxList_SuppType.Items[i].Value + ",";
            }
        }
        rowindex = Convert.ToInt32(rowclicked.Text);
        LinkButton lnk = (LinkButton)gvPOConfig.Rows[rowindex].Cells[2].Controls[1];
        ImageButton imbtn = (ImageButton)gvPOConfig.Rows[rowindex].Cells[2].Controls[3];
        imbtn.Visible = false;
        lnk.Visible = true;
        lnk.Text = txtSupptype.Remove(txtSupptype.Length - 1);
        lnk.CommandArgument = ValSupptype.Remove(ValSupptype.Length - 1);
        get_configData(rowindex);
        objBLLconfig.PURC_Save_Config__BLL(POconfig, dt); // SAVE the PO Configurations for the Selected PO
        String msgmodal = String.Format("hideModal('dvPopupSupp');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodalhide", msgmodal, true);
    }

    /// <summary>
    /// Saves the Selected Requisition Type Selected in the PopUp
    /// </summary>
    protected void BtnReq_Save(object sender, EventArgs e)
    {
        string txtReqtype = "", ValReqtype = "";

        for (int i = 0; i < ChkBxList_ReqsnType.Items.Count; i++)
        {
            if (ChkBxList_ReqsnType.Items[i].Selected)
            {
                txtReqtype += ChkBxList_ReqsnType.Items[i].Text + ",";
                ValReqtype += ChkBxList_ReqsnType.Items[i].Value + ",";
            }
        }
        rowindex = Convert.ToInt32(rowclicked.Text);
        LinkButton lnk = (LinkButton)gvPOConfig.Rows[rowindex].Cells[1].Controls[1];
        int a = gvPOConfig.Rows[rowindex].Cells[1].Controls.Count;
        ImageButton imbtn = (ImageButton)gvPOConfig.Rows[rowindex].Cells[1].Controls[3];
        imbtn.Visible = false;
        lnk.Visible = true;
        lnk.Text = txtReqtype.Remove(txtReqtype.Length - 1);
        lnk.CommandArgument = ValReqtype.Remove(ValReqtype.Length - 1);
        get_configData(rowindex);
        objBLLconfig.PURC_Save_Config__BLL(POconfig, dt); // SAVE the PO Configurations for the Selected PO
        String msgmodal = String.Format("hideModal('dvPopupreqsn');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodalhide", msgmodal, true);
    }

    /// <summary>
    /// Check If quotation is requsired is no set the no of quotaion as 0 and disable NoOFQuatation Textbox else Enable the text box
    /// </summary>
    protected void OnQuoteRequireChange(object sender, EventArgs e)
    {
        rowindex = Convert.ToInt32(rowclicked.Text);
        CheckBox chk = (CheckBox)sender;
        TextBox txtQuoteNo = (TextBox)gvPOConfig.Rows[rowindex].Cells[9].Controls[1];

        if (chk.Checked == false)
        {
            txtQuoteNo.Text = "0";
            txtQuoteNo.Enabled = false;
        }
        else { txtQuoteNo.Text = ""; txtQuoteNo.Enabled = true; }
    }
    protected void grd_RowDeleting(object sender, GridViewCancelEditEventArgs e)
    {

    }
}