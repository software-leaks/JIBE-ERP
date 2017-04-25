using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Properties.PURC;
using SMS.Business.PURC;
using System.Data;
using SMS.Business.ASL;

public partial class Purchase_PURC_Config_View : System.Web.UI.Page
{
    POConfig POconfig = new POConfig();

    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            filldata();
            submit.Text = Request.QueryString["OperationMode"].ToString();
            if (Request.QueryString["OperationMode"].ToString() == "UPDATE")
            {
                Update();
                LBpotype.Enabled = false;
                 if (ddl_Quote_required.SelectedValue != "0")
                {
                    txt_QuoteNo.Enabled = true;
                }

            }
            else { LBpotype.Enabled = true; }
        }
    }
    BLL_PURC_Config_PO objBLLconfig = new BLL_PURC_Config_PO();
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void ddl_Quote_required_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Quote_required.SelectedValue == "0")
        {
            txt_QuoteNo.Text = "0";
            txt_QuoteNo.Enabled = false;
        }
        else { txt_QuoteNo.Text = ""; txt_QuoteNo.Enabled = true; }
    }

    public void filldata()
    {
        DataSet ddllist = objBLLconfig.Get_ddlListBLL();

        LbSupplierType.DataSource = ddllist.Tables[2];
        LbSupplierType.DataTextField = "VARIABLE_NAME";
        LbSupplierType.DataValueField = "ID";
        LbSupplierType.DataBind();
        

        LBReqsntype.DataSource = ddllist.Tables[1];
        LBReqsntype.DataTextField = "Description";
        LBReqsntype.DataValueField = "Code";
        LBReqsntype.DataBind();

        LBpotype.DataSource = ddllist.Tables[0];
        LBpotype.DataTextField = "VARIABLE_NAME";
        LBpotype.DataValueField = "ID";
        LBpotype.DataBind();

        LBpotype.Items.Insert(0, new ListItem("Select", string.Empty));
    }

    protected string PreventUnlistedValueError(DropDownList li, string val)
    {
        if (li.Items.FindByValue(val) == null)
        {
            li.SelectedValue = "0";
            val = "0";
        }
        return val;
    }
    string POtypeid = "";
    protected void Update()
    {

        DataSet dsConfig = new DataSet();
        dsConfig = objBLLconfig.PURC_Get_Config_BLL(LBpotype.SelectedValue);

        LBpotype.SelectedValue = PreventUnlistedValueError(LBpotype, Convert.ToString(dsConfig.Tables[0].Rows[0]["PO_Type"]));
        ddl_Owner.SelectedValue = PreventUnlistedValueError(ddl_Owner, Convert.ToString(dsConfig.Tables[0].Rows[0]["Auto_Owner_Selection"]));
        ddl_Delivery_Port.SelectedValue = PreventUnlistedValueError(ddl_Delivery_Port, Convert.ToString(dsConfig.Tables[0].Rows[0]["Delivery_Port"]));
        dd_Delivery_Port_date.SelectedValue = PreventUnlistedValueError(dd_Delivery_Port_date, Convert.ToString(dsConfig.Tables[0].Rows[0]["Delivery_Date"]));
        ddl_Vessel_movement_Date.SelectedValue = PreventUnlistedValueError(ddl_Vessel_movement_Date, Convert.ToString(dsConfig.Tables[0].Rows[0]["Vessel_Movement_Date"]));
        ddl_Quote_required.SelectedValue = PreventUnlistedValueError(ddl_Quote_required, Convert.ToString(dsConfig.Tables[0].Rows[0]["Direct_Quotation"]));
        txt_QuoteNo.Text = dsConfig.Tables[0].Rows[0]["Min_QTN_Required"].ToString();
        ddl_Item_Category.SelectedValue = PreventUnlistedValueError(ddl_Item_Category, Convert.ToString(dsConfig.Tables[0].Rows[0]["Item_Category"]));
        ddl_Vessel_Processing_PO.SelectedValue = PreventUnlistedValueError(ddl_Vessel_Processing_PO, Convert.ToString(dsConfig.Tables[0].Rows[0]["Vessel_Processing_PO"]));
        ddl_Enable_Free_text.SelectedValue = PreventUnlistedValueError(ddl_Enable_Free_text, Convert.ToString(dsConfig.Tables[0].Rows[0]["Free_Text_Items_Addition"]));
        ddl_copy_to_vessel.SelectedValue = PreventUnlistedValueError(ddl_copy_to_vessel, Convert.ToString(dsConfig.Tables[0].Rows[0]["Copy_To_Vessel"]));
        ddl_Sup_Po_Confirmation.SelectedValue = PreventUnlistedValueError(ddl_Sup_Po_Confirmation, Convert.ToString(dsConfig.Tables[0].Rows[0]["Supplier_PO_Confirmation"]));
        ddl_Delivery_Confirm_Onbor.SelectedValue = PreventUnlistedValueError(ddl_Delivery_Confirm_Onbor, Convert.ToString(dsConfig.Tables[0].Rows[0]["Office_Delivery_Confirmation"]));
        ddl_Vessel_Delivery_Confirm.SelectedValue = PreventUnlistedValueError(ddl_Vessel_Delivery_Confirm, Convert.ToString(dsConfig.Tables[0].Rows[0]["Vessel_Delivery_Confirmation"]));
        ddl_Withhold_tax.SelectedValue = PreventUnlistedValueError(ddl_Withhold_tax, Convert.ToString(dsConfig.Tables[0].Rows[0]["Witholding_Tax"]));
        ddl_Vat_Config_Purc.SelectedValue = PreventUnlistedValueError(ddl_Vat_Config_Purc, Convert.ToString(dsConfig.Tables[0].Rows[0]["VAT_Config_By_Purchaser"]));
        ddl_require_verify.SelectedValue = PreventUnlistedValueError(ddl_require_verify, Convert.ToString(dsConfig.Tables[0].Rows[0]["Verification_Required"]));
        ddl_Auto_POClosing.SelectedValue = PreventUnlistedValueError(ddl_Auto_POClosing, Convert.ToString(dsConfig.Tables[0].Rows[0]["Auto_PO_Closing"]));

        string res = "";
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

    public void get_config()
    {
        dt.Clear();
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

                DataRow dr = dt.NewRow();
                dr["Key_Value"] = li.Value;
                dr["Key_VAlue_Type"] = "Supplier";
                dt.Rows.Add(dr);

            }
        }
        if (Request.QueryString["OperationMode"].ToString() == "UPDATE")
        {
            POconfig.ID = Request.QueryString["id"].ToString();
        }
        POconfig.POType = LBpotype.SelectedValue;
        POconfig.Owner = ddl_Owner.SelectedValue;
        POconfig.Delivery_Port = ddl_Delivery_Port.SelectedValue;
        POconfig.Delivery_Port_date = dd_Delivery_Port_date.SelectedValue;
        POconfig.Vessel_movement_Date = ddl_Vessel_movement_Date.SelectedValue;
        POconfig.Vessel_Processing_PO = ddl_Vessel_Processing_PO.SelectedValue;
        POconfig.Item_Category = ddl_Item_Category.SelectedValue;
        POconfig.Quote_required = ddl_Quote_required.SelectedValue;
        POconfig.QuoteNo = txt_QuoteNo.Text;
        POconfig.Vessel_Delivery_Confirm = ddl_Vessel_Delivery_Confirm.SelectedValue;
        POconfig.Enable_Free_text = ddl_Enable_Free_text.SelectedValue;
        POconfig.copy_to_vessel = ddl_copy_to_vessel.SelectedValue;
        POconfig.Sup_Po_Confirmation = ddl_Sup_Po_Confirmation.SelectedValue;
        POconfig.Vessel_Delivery_Confirm = ddl_Vessel_Delivery_Confirm.SelectedValue;
        POconfig.Office_Delivery_Confirmation = ddl_Vessel_Delivery_Confirm.SelectedValue;
        POconfig.Withhold_tax = ddl_Withhold_tax.SelectedValue;
        POconfig.Vat_Config_Purc = ddl_Vat_Config_Purc.SelectedValue;
        POconfig.require_verify = ddl_require_verify.SelectedValue;
        POconfig.Auto_POClosing = ddl_Auto_POClosing.SelectedValue;
        POconfig.Currentuser = Session["userid"].ToString();
    }
    protected void submit_Click(object sender, EventArgs e)
    {
        string script = "";
        if (LBReqsntype.SelectedValue.Count() > 0)
        {
            if (LbSupplierType.SelectedValue.Count() > 0)
            {
                get_config();
                string result = "";
                if (submit.Text == "ADD")
                {
                    result = objBLLconfig.PURC_Save_Config__BLL(POconfig, dt);
                    script = "<script type=\"text/javascript\">alert('" + LBpotype.SelectedItem.Text + " Configurations Added !');</script>";

                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "<script type='text/JavaScript'>window.close();</script>");
                }
                else
                {
                   // result = objBLLconfig.UpdatePOConfig_BLL(POconfig, dt);
                    script = "<script type=\"text/javascript\">alert('" + LBpotype.SelectedItem.Text + " Configurations Updated !');</script>";

                    ClientScript.RegisterStartupScript(typeof(Page), "closePage", "<script type='text/JavaScript'>window.close();</script>");
                }
            }
            else
            {
                script = "<script type=\"text/javascript\">alert('Select atleast one Supplier Type');</script>";
            }
        }
        else
        {
            script = "<script type=\"text/javascript\">alert('Select atleast one Requisition Type');</script>";
        }
        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
    }
}