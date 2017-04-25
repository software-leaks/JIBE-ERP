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
using SMS.Properties;
public partial class PO_LOG_Delivery_Listing : System.Web.UI.Page
{
    public string CurrStatus = null;
    public string OperationMode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserAccessValidation();
        if (!IsPostBack)
        {
            chkOpen.Checked = true;
            ChkConfirmed.Checked = true;
            txtPOCode.Text = GetSupplyID();
            BindDeliveryDetails();
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public string GetSupplyID()
    {
        try
        {
            if (Request.QueryString["SUPPLY_ID"] != null)
            {
                return Request.QueryString["SUPPLY_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    protected void ChkStatus()
    {
        if (chkOpen.Checked == true)
        {
            CurrStatus = "OPEN";
        }
        if (ChkConfirmed.Checked == true)
        {
            if (CurrStatus == "")
            {
                CurrStatus = "CONFIRMED";
            }
            else
            {
                CurrStatus = CurrStatus + "," + "CONFIRMED";
            }
        }
        if (chkDeleted.Checked == true)
        {
            if (CurrStatus == "")
            {
                CurrStatus = "DELETED";
            }
            else
            {
                CurrStatus = CurrStatus + "," + "DELETED";
            }
        }
    }
    protected void BindDeliveryDetails()
    {
        try
        {
            ChkStatus();
            DataSet ds = BLL_POLOG_Delivery.POLOG_Get_Delivery_List(UDFLib.ConvertToInteger(txtPOCode.Text), CurrStatus, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
            if (ds.Tables[0].Rows.Count > 0)
            {
                divVesselDelivery.Visible = true;
                gvVesselDelivery.DataSource = ds.Tables[0];
                gvVesselDelivery.DataBind();
            }
            else
            {
                divVesselDelivery.Visible = false;
                gvVesselDelivery.DataSource = ds.Tables[0];
                gvVesselDelivery.DataBind();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                divDelivery.Visible = true;
                gvDelivery.DataSource = ds.Tables[1];
                gvDelivery.DataBind();
            }
            else
            {
                divDelivery.Visible = false;
                gvDelivery.DataSource = ds.Tables[1];
                gvDelivery.DataBind();
            }
        }
        catch { }
        {
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindDeliveryDetails();
    }

    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    this.SetFocus("txtLocation");
    //    txtDeliveryID.Text = null;
    //    string str = "Add";
    //    txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
    //    btnSave.Enabled = true;
    //    BindPortCall();
    //    BindDeliveryItemDetails(str);
    //    OperationMode = "Delivery Deatils";
    //    string AddUserTypemodal = String.Format("showModal('divadd',false);");
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    //}
    //protected void ImgUpdate_Click(object s, CommandEventArgs e)
    //{
    //    string[] arg = e.CommandArgument.ToString().Split(',');
    //    txtDeliveryID.Text = UDFLib.ConvertStringToNull(arg[0]);
    //    txtDeliveryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

    //    string str = "Edit";
    //    BindPortCall();
    //    BindDeliveryItemDetails(str);
    //    OperationMode = "Delivery Deatils";
    //    string AddUserTypemodal = String.Format("showModal('divadd',false);");
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    //}
    protected void ImgDelete_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? RemarksID = UDFLib.ConvertIntegerToNull(arg[0]);
        txtDeliveryID.Text = UDFLib.ConvertStringToNull(arg[0]);
        string Action_By_Button = "DELETED";
        string DeliveryStatus = "DELETED";
        int retval = BLL_POLOG_Delivery.POLOG_Delete_Delivery_Details(UDFLib.ConvertStringToNull(txtDeliveryID.Text.ToString()), UDFLib.ConvertIntegerToNull(txtPOCode.Text.ToString()), Action_By_Button, DeliveryStatus, UDFLib.ConvertToInteger(GetSessionUserID()));
        // InsertAuditTrail("Delete Delivery", "DeleteDelivery");
        int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(txtDeliveryID.Text), "Delete Delivery", "DeleteDelivery", UDFLib.ConvertToInteger(GetSessionUserID()));
        BindDeliveryDetails();
    }
    //private void BindDeliveryItemDetails(string Type)
    //{

    //    DataSet ds = BLL_POLOG_Register.POLOG_Get_Delivery_Details(UDFLib.ConvertStringToNull(txtDeliveryID.Text.ToString()), UDFLib.ConvertToInteger(txtPOCode.Text.ToString()), Type, UDFLib.ConvertToInteger(GetSessionUserID()));

    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        txtDeliveryID.Text = ds.Tables[0].Rows[0]["Delivery_ID"].ToString();
    //        txtDeliveryDate.Text = ds.Tables[0].Rows[0]["Delivery_Date"].ToString();
    //        txtLocation.Text = ds.Tables[0].Rows[0]["Delivery_Location"].ToString();
    //        ddlPortCall.SelectedValue = ds.Tables[0].Rows[0]["Port_Call_ID"].ToString();
    //        txtRemarks.Text = ds.Tables[0].Rows[0]["Delivery_Remarks"].ToString();

    //        lblCreatedBy.Text = ds.Tables[0].Rows[0]["Created_By"].ToString();
    //        lblCreateddate.Text = ds.Tables[0].Rows[0]["Created_Date"].ToString();
    //        lblDelveryID.Text = ds.Tables[0].Rows[0]["Delivery_ID"].ToString();
    //        if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "")
    //        {
    //            btnConfirm.Enabled = false;
    //            btnDelete.Enabled = false;
    //            btnAddNonPO.Enabled = false;
    //        }
    //        else
    //        {
    //            btnConfirm.Enabled = true;
    //            btnDelete.Enabled = true;
    //            btnAddNonPO.Enabled = true;
    //        }
    //        if (ds.Tables[0].Rows[0]["Delivery_Status"].ToString() == "CONFIRMED")
    //        {
    //            btnUnlock.Visible = true;
    //            btnSave.Enabled = false;
    //            btnAddNonPO.Enabled = false;
    //            btnConfirm.Enabled = false;
    //            btnDelete.Enabled = false;
    //            gvDeliveryItem.Columns[7].Visible = false;
    //        }
    //        else
    //        {
    //            btnUnlock.Visible = false;
    //            gvDeliveryItem.Columns[7].Visible = true;
    //        }

    //        lblCreatedBy.Visible = true;
    //        lblCreateddate.Visible = true;
    //        lblDelveryID.Visible = true;
    //        Label1.Visible = true;
    //        lblCreated.Visible = true;
    //        Label2.Visible = true;
    //    }
    //    else
    //    {
    //        btnAddNonPO.Enabled = false;
    //    }
    //    if (ds.Tables[1].Rows.Count > 0)
    //    {
    //        divDeliveryItem.Visible = true;
    //        gvDeliveryItem.DataSource = ds.Tables[1];
    //        gvDeliveryItem.DataBind();
    //    }
    //    else
    //    {
    //        divDeliveryItem.Visible = false;
    //        gvDeliveryItem.DataSource = ds.Tables[1];
    //        gvDeliveryItem.DataBind();
    //    }

    //}
    //protected void BindPortCall()
    //{
    //    DataTable dt = BLL_POLOG_Register.POLOG_Get_Port_Call(UDFLib.ConvertStringToNull(txtDeliveryID.Text.ToString()), UDFLib.ConvertToInteger(txtPOCode.Text.ToString()),0,1);

    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlPortCall.DataSource = dt;
    //        ddlPortCall.DataTextField = "PORT_NAME";
    //        ddlPortCall.DataValueField = "Port_Call_ID";
    //        ddlPortCall.DataBind();
    //        ddlPortCall.Items.Insert(0, new ListItem("-Select-", "0"));
    //    }
    //}
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    string Action_By_Button = "OPEN";
    //    string DeliveryStatus = "OPEN";
    //    //Save(Action_By_Button, DeliveryStatus);
    //    string retval = BLL_POLOG_Register.POLOG_Insert_Delivery_Details(UDFLib.ConvertStringToNull(txtDeliveryID.Text.ToString()), UDFLib.ConvertIntegerToNull(txtPOCode.Text.ToString()),
    //     Convert.ToDateTime(txtDeliveryDate.Text.Trim()), txtLocation.Text.Trim(), UDFLib.ConvertIntegerToNull(ddlPortCall.SelectedValue), txtRemarks.Text.Trim(), Action_By_Button, DeliveryStatus, UDFLib.ConvertToInteger(GetSessionUserID()));
    //    string str = "Edit";
    //    txtDeliveryID.Text = Convert.ToString(retval);
    //    BindDeliveryItemDetails(str);
    //    string AddUserTypemodal = String.Format("showModal('divadd',false);");
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    //}

    //protected void btnConfirm_Click(object sender, EventArgs e)
    //{

    //    string Action_By_Button = "CONFIRMED";
    //    string DeliveryStatus = "CONFIRMED";

    //    string retval = BLL_POLOG_Register.POLOG_Insert_Delivery_Details(UDFLib.ConvertStringToNull(txtDeliveryID.Text.ToString()), UDFLib.ConvertIntegerToNull(txtPOCode.Text.ToString()),
    //    Convert.ToDateTime(txtDeliveryDate.Text.Trim()), txtLocation.Text.Trim(), UDFLib.ConvertIntegerToNull(ddlPortCall.SelectedValue), txtRemarks.Text.Trim(), Action_By_Button, DeliveryStatus, UDFLib.ConvertToInteger(GetSessionUserID()));
    //    string str = "Edit";
    //    txtDeliveryID.Text = Convert.ToString(retval);
    //    //BindDeliveryDetails();
    //    BindDeliveryItemDetails(str);
    //    string AddUserTypemodal = String.Format("showModal('divadd',false);");
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    //}
    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    string Action_By_Button = "DELETED";
    //    string DeliveryStatus = "DELETED";
    //    int retval = BLL_POLOG_Register.POLOG_Delete_Delivery_Details(UDFLib.ConvertStringToNull(txtDeliveryID.Text.ToString()), UDFLib.ConvertIntegerToNull(txtPOCode.Text.ToString()), Action_By_Button, DeliveryStatus, UDFLib.ConvertToInteger(GetSessionUserID()));
    //    BindDeliveryDetails();
    //    string hidemodal = String.Format("hideModal('divadd')");
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
    //}
    //protected void onDelete(object source, CommandEventArgs e)
    //{
    //    int retval = BLL_POLOG_Register.POLOG_Delete_Delivery_Item(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(GetSessionUserID()));
    //    string str = "Edit";
    //    BindDeliveryItemDetails(str);
    //}


    //protected void gvDeliveryItem_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        ImageButton UpdateButton = (ImageButton)e.Row.FindControl("ImgUpdate");
    //        ImageButton DeleteButton = (ImageButton)e.Row.FindControl("ImgDelete");
    //        if (DataBinder.Eval(e.Row.DataItem, "Delivered_Item_Descriptionx").ToString() == "Non PO Item")
    //        {
    //            UpdateButton.Visible = true;
    //            DeleteButton.Visible = true;
    //        }
    //        else
    //        {
    //            UpdateButton.Visible = false;
    //            DeleteButton.Visible = false;
    //        }
    //    }
    //}
    //protected void BindDeliveryItem(string Type)
    //{
    //    DataSet ds = BLL_POLOG_Register.POLOG_Get_Delivery_Details(UDFLib.ConvertStringToNull(txtDeliveryID.Text.ToString()), UDFLib.ConvertToInteger(txtPOCode.Text.ToString()), Type, UDFLib.ConvertToInteger(GetSessionUserID()));
    //    if (ds.Tables[1].Rows.Count > 0)
    //    {
    //        gvDeliveryItem.DataSource = ds.Tables[1];
    //        gvDeliveryItem.DataBind();
    //    }
    //}

    //protected void btnUnlock_Click(object sender, EventArgs e)
    //{
    //    string Action_By_Button = "OPEN";
    //    string DeliveryStatus = "OPEN";
    //    //Save(Action_By_Button, DeliveryStatus);
    //    string retval = BLL_POLOG_Register.POLOG_Insert_Delivery_Details(UDFLib.ConvertStringToNull(txtDeliveryID.Text.ToString()), UDFLib.ConvertIntegerToNull(txtPOCode.Text.ToString()),
    //     Convert.ToDateTime(txtDeliveryDate.Text.Trim()), txtLocation.Text.Trim(), UDFLib.ConvertIntegerToNull(ddlPortCall.SelectedValue), txtRemarks.Text.Trim(), Action_By_Button, DeliveryStatus, UDFLib.ConvertToInteger(GetSessionUserID()));
    //    string str = "Edit";
    //    txtDeliveryID.Text = Convert.ToString(retval);
    //    BindDeliveryItemDetails(str);
    //    string AddUserTypemodal = String.Format("showModal('divadd',false);");
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    //}

    //protected void btnClose_Click(object sender, EventArgs e)
    //{
    //    BindDeliveryDetails();
    //    string hidemodal = String.Format("hideModal('divadd')");
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
    //}
    protected void imgfilter_Click(object sender, ImageClickEventArgs e)
    {
        BindDeliveryDetails();
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        chkDeleted.Checked = false;
        BindDeliveryDetails();
    }
}