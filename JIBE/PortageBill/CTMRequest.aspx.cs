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
using Telerik.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Text;
using SMS.Business.PortageBill;
using SMS.Properties;
using SMS.Business.Infrastructure;

public partial class PortageBill_CTMRequest : System.Web.UI.Page
{
    # region . variables
    double ReqTotal = 0;
    decimal CTMTotal = 0;
    int NoOfNotes_by_Capt = 0;
    int NoOfNotes_by_Office = 0;
    decimal Total_AmtCapt = 0;
    decimal Total_AmtOffice = 0;
    decimal Total_AmtVessel = 0;
    decimal GrandTotal_AmtOffice = 0;

    public bool IsApproved = false;

    public int Vessel_ID
    {
        get { return UDFLib.ConvertToInteger(ViewState["Vessel_ID"]); }
        set { ViewState["Vessel_ID"] = value.ToString(); }
    }

    public int CTM_ID
    {
        get { return UDFLib.ConvertToInteger(ViewState["ID"]); }
        set { ViewState["ID"] = value.ToString(); }
    }

    public int Office_ID
    {
        get { return UDFLib.ConvertToInteger(ViewState["Office_ID"]); }
        set { ViewState["Office_ID"] = value.ToString(); }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        lblPortcallMessage.Text = "";
        if (!Page.IsPostBack)
        {
            UserAccessValidation();

            ViewState["Vessel_ID"] = UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"]);
            ViewState["ID"] = UDFLib.ConvertToInteger(Request.QueryString["ID"]);
            ViewState["Office_ID"] = UDFLib.ConvertToInteger(Request.QueryString["Office_ID"]);

            if (CTM_ID == 0)
            {
                lblCTMCreatedBy.Text = Session["USERFULLNAME"].ToString();
                txtRequestedOn.Text = UDFLib.ConvertUserDateFormat(DateTime.Now.ToShortDateString());
            }
            else
            {
                DDLVessel.Enabled = false;
            }

            BindVesselDDL();
            Load_CTM_Details();
            ucPortCallsCtm.BindPortCalls(Vessel_ID);
        }
        //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }

    public void BindVesselDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            ListItem li = new ListItem("-- SELECT --", "0");
            DDLVessel.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void UserAccessValidation()
    {

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
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
        if (objUA.Approve == 1 || objUA.Admin == 1)
        {
            pnlApprove.Visible = true;
            btnRework.Visible = true;

        }
        else
        {
            pnlApprove.Visible = false;
            btnRework.Visible = false;
        }
    }

    public string GetTotal(string Denomination, string NoOfNotes)
    {
        decimal Res = (UDFLib.ConvertToDecimal(Denomination) * UDFLib.ConvertToDecimal(NoOfNotes));
        return Res.ToString("$ ###,##0.00");
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
        {
            Session.Abandon();
            Response.Redirect("~/Account/Login.aspx");
            return 0;
        }
    }

    protected void gvLastCTM_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDate = (Label)e.Row.FindControl("lblDate");
                if (DataBinder.Eval(e.Row.DataItem, "DateReceived") != null)
                    lblDate.Text = UDFLib.ConvertToDate(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DateReceived"))).ToShortDateString();
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    protected void Load_CTM_Details()
    {


        DataSet CTMDs = BLL_PB_PortageBill.Get_CTM_Details(Vessel_ID, CTM_ID, Office_ID);
        Load_CTM_Offsigners();
        gvDenominations_BindData();
        //-- CTM Details --
        if (CTMDs.Tables[0].Rows.Count > 0)
        {
            gvLastCTM.DataSource = CTMDs.Tables[3];
            gvLastCTM.DataBind();


            DDLVessel.Items.FindByValue(CTMDs.Tables[0].Rows[0]["Vessel_ID"].ToString()).Selected = true;


            txtCTM_Supply_Date.Text = (CTMDs.Tables[0].Rows[0]["CTM_Date"].ToString() == "") ? "" : DateTime.Parse(CTMDs.Tables[0].Rows[0]["CTM_Date"].ToString()).ToString(UDFLib.GetDateFormat());

            lblCTMCreatedBy.Text = CTMDs.Tables[0].Rows[0]["RequestedBy"].ToString();
            txtCTMRemark.Text = CTMDs.Tables[0].Rows[0]["RequestDetails"].ToString();
            txtRequestedOn.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(CTMDs.Tables[0].Rows[0]["CreatedOn"]));

            if (CTMDs.Tables[0].Rows[0]["ctm_port"].ToString() != "")
            {
                hdfPort_ID.Value = CTMDs.Tables[0].Rows[0]["ctm_port"].ToString();
                lblportname.Text = CTMDs.Tables[0].Rows[0]["PORT_NAME"].ToString();
                hdfPortCall_ID.Value = Convert.ToString(CTMDs.Tables[0].Rows[0]["PortCall_ID"]);

            }

            lblDeliveryDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(CTMDs.Tables[0].Rows[0]["Delivery_Date"]));
            lblOrderDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(CTMDs.Tables[0].Rows[0]["ORDER_DATE"]));
            lblOrderCode.Text = Convert.ToString(CTMDs.Tables[0].Rows[0]["ORDER_CODE"]);
            lblDeliveryCode.Text = Convert.ToString(CTMDs.Tables[0].Rows[0]["DELIVERY_CODE"]);
            uc_SupplierListCTM.SelectedValue = Convert.ToString(CTMDs.Tables[0].Rows[0]["Supplier_Code"]);
            txtBOWCalculated.Text = UDFLib.ConvertToDecimal(CTMDs.Tables[0].Rows[0]["BalanceOfWagesTotalAmt"]).ToString(" ###,##0.00");

            if (CTMDs.Tables[0].Rows[0]["CTMStatus"].ToString() == "CANCELLED" || CTMDs.Tables[0].Rows[0]["CTMStatus"].ToString() == "ACKVESSEL" || CTMDs.Tables[0].Rows[0]["CTMStatus"].ToString() == "APPROVED")
                txtCashOnBoard.Text = UDFLib.ConvertToDecimal(CTMDs.Tables[0].Rows[0]["CashOnBoardAmt"]).ToString(" ###,##0.00");
            else
                txtCashOnBoard.Text = CTMDs.Tables[5].Rows.Count > 0 ? UDFLib.ConvertToDecimal(CTMDs.Tables[5].Rows[0]["CashOnBoardAmt"]).ToString(" ###,##0.00") : ""; ;
            txtCTMRequested.Text = UDFLib.ConvertToDecimal(CTMDs.Tables[0].Rows[0]["CTMRequestedAmt"]).ToString(" ###,##0.00");

            CTMTotal = UDFLib.ConvertToDecimal(CTMDs.Tables[0].Rows[0]["CTMRequestedRndOffAmt"]);
            txtSupplierCommission.Text = UDFLib.ConvertStringToNull(CTMDs.Tables[0].Rows[0]["Supplier_Commission"]);
            txtApprovalRemark.Text = Convert.ToString(CTMDs.Tables[0].Rows[0]["OfficeRemark"]);
            if (CTMDs.Tables[4].Rows.Count > 0)
            {
                foreach (DataRow dr in CTMDs.Tables[4].Rows)
                {
                    if (!string.IsNullOrEmpty(dr["Sent On"].ToString()))
                    {
                        dr["Sent On"] = UDFLib.ConvertUserDateFormatTime(Convert.ToDateTime(dr["Sent On"].ToString()).ToString("dd/MM/yyyy HH:mm:ss"), UDFLib.GetDateFormat());
                    }
                }
            }
            gvApprovals.DataSource = CTMDs.Tables[4];
            gvApprovals.DataBind();
            //---- CHECK APPROVALS --//

            //-- NOT APPROVED BY STAGE - 1 APPROVER
            if (CTMDs.Tables[0].Rows[0]["CTMStatus"].ToString() == "SENTTOOFFICE")
            {
                btnApprv.Enabled = true;
                btnCancel.Enabled = true;
                btnRework.Enabled = false;
                //btnRework.Text = "Rollback";
                pnlDenominationApprove.Visible = true;

                uc_SupplierListCTM.Enable = true;
                txtSupplierCommission.Enabled = true;
                txtBOWCalculated.Text = TotalBOW.ToString("0.00");
            }
            else if (CTMDs.Tables[0].Rows[0]["CTMStatus"].ToString() != "CANCELLED" && CTMDs.Tables[0].Rows[0]["CTMStatus"].ToString() != "ACKVESSEL")
            {
                pnlApprove.Enabled = false;

                IsApproved = true;
                btnRework.Enabled = true;
                // btnRework.Text = "Rollback";
                uc_SupplierListCTM.Enable = false;
                txtSupplierCommission.Enabled = false;
            }
            else
            {
                IsApproved = true;
                pnlDenominationApprove.Enabled = false;
                pnlApprove.Enabled = false;
                btnRework.Enabled = CTMDs.Tables[0].Rows[0]["CTMStatus"].ToString() == "ACKVESSEL" ? false : true;
                //  btnRework.Text = "Rollback";
                uc_SupplierListCTM.Enable = false;
                txtSupplierCommission.Enabled = false;
            }
        }
    }

    protected void Load_CTM_Offsigners()
    {
        gvCTM_OffSigners.DataSource = BLL_PB_PortageBill.Get_CTM_OffSignersList(CTM_ID, Vessel_ID);
        gvCTM_OffSigners.DataBind();
    }

    protected void gvDenominations_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            int fieldValue = int.Parse(dataItem["ReqTotal"].Text);
            ReqTotal += fieldValue;
        }
        if (e.Item is GridFooterItem)
        {
            GridFooterItem footerItem = e.Item as GridFooterItem;
            footerItem["ReqTotal"].Text = "total: " + ReqTotal.ToString();

        }


    }
    protected void txtPect_TextChanged(object sender, EventArgs e)
    {

    }


    private void gvDenominations_BindData()
    {


        DataTable dt = BLL_PB_PortageBill.Get_CTM_Denominations(Vessel_ID, CTM_ID, Office_ID);


        if (dt.Rows.Count == 0)
        {
            int[] arrDen = new int[] { 100, 50, 20, 10, 5, 1 };
            for (int i = 0; i < 6; i++)
            {
                DataRow drden = dt.NewRow();
                drden["id"] = "0";
                drden["Denomination"] = arrDen[i];

                dt.Rows.Add(drden);
            }
        }

        gvDenominations.DataSource = dt;
        gvDenominations.DataBind();

        ViewState["Denominations"] = dt.Clone();
    }

    protected void btnAddNewItem_Click(object s, EventArgs e)
    {
        DataTable dtGridItems = ((DataTable)ViewState["Denominations"]).Clone();

        DataRow dr = null;
        foreach (GridViewRow grItem in gvDenominations.Rows)
        {
            dr = dtGridItems.NewRow();
            dr["id"] = gvDenominations.DataKeys[grItem.RowIndex].Values["id"];
            dr["Denomination"] = UDFLib.ConvertToInteger(((TextBox)grItem.FindControl("txtDenomination")).Text);
            dr["NoOfNotes_by_Office"] = UDFLib.ConvertToDecimal(((TextBox)grItem.FindControl("txtNoOfNotes_by_Office")).Text);

            dtGridItems.Rows.Add(dr);

        }

        dr = dtGridItems.NewRow();
        dr["id"] = 0;
        dr["Denomination"] = 0;
        dr["NoOfNotes_by_Office"] = 0;

        dtGridItems.Rows.Add(dr);
        gvDenominations.DataSource = dtGridItems;
        gvDenominations.DataBind();

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "calTotaldCTM", "CalculateTotal();", true);



    }
    protected void gvDenominations_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Denomination = 0;

                Denomination = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Denomination").ToString());
                int NotesByCapt = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "NoOfNotes_by_Capt").ToString());
                int NotesByOffice = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "NoOfNotes_by_Office").ToString());

                NoOfNotes_by_Capt = NoOfNotes_by_Capt + NotesByCapt;
                NoOfNotes_by_Office = NoOfNotes_by_Office + NotesByOffice;

                Total_AmtCapt = Total_AmtCapt + (Denomination * NotesByCapt);
                Total_AmtOffice = Total_AmtOffice + (Denomination * NotesByOffice);

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ViewState["Total_AmtOffice"] = Total_AmtOffice;
                ((Label)e.Row.FindControl("lblTotal_AmtOffice")).Text = Total_AmtOffice.ToString("$ ###,##0.00");
                ViewState["Total_AmtCapt"] = Total_AmtOffice;
                ((Label)e.Row.FindControl("lblTotal_AmtCapt")).Text = Total_AmtCapt.ToString("$ ###,##0.00");

                txtCTMRequested.Text = Total_AmtOffice.ToString(" ###,##0.00");
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    protected void Save_Denominations()
    {
        try
        {


            foreach (GridViewRow grDen in gvDenominations.Rows)
            {
                int ID = UDFLib.ConvertToInteger(gvDenominations.DataKeys[grDen.RowIndex].Value.ToString());
                int Denomination = 0;//UDFLib.ConvertToInteger((grDen.FindControl("ddlDenomination") as DropDownList).SelectedValue);
                int Notes_Office = UDFLib.ConvertToInteger((grDen.FindControl("txtNoOfNotes_by_Office") as TextBox).Text);

                int Res = BLL_PB_PortageBill.UPDATE_CTM_Denominations(Vessel_ID, CTM_ID, Office_ID, ID, Notes_Office, Denomination, GetSessionUserID());

            }

            gvDenominations_BindData();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void gvCTMCalculations_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal rowTotal = UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "CashCategory_Amt").ToString());
            CTMTotal = CTMTotal + rowTotal;
        }

    }

    protected void btnApprv_Click(object sender, EventArgs e)
    {
        try
        {
            if (DDLVessel.SelectedValue == "0")
            {
                string js = "alert('Please Select Vessel.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
            }
            else
            {
                int ApprovalStatus = 0;
                ApprovalStatus = UDFLib.ConvertToInteger((sender as Button).CommandArgument);

                if (ApprovalStatus > 0) // approval
                {
                    CTM_ID = Save_CTM_Details();

                    if (CTM_ID>0)
                    {
                        if (uc_SupplierListCTM.SelectedValue != "0")
                        {
                            if (UDFLib.ConvertToDecimal(ViewState["Total_AmtOffice"]) > 0)
                            {
                                int Res = BLL_PB_PortageBill.UPDATE_CTM_Approval(Vessel_ID, CTM_ID, Office_ID, UDFLib.ConvertToDecimal(ViewState["Total_AmtOffice"]), GetSessionUserID(), txtApprovalRemark.Text, ApprovalStatus, uc_SupplierListCTM.SelectedValue, UDFLib.ConvertToDecimal(txtSupplierCommission.Text));
                                String msg = String.Format("alert('Approved successfully.');window.opener.location.reload();self.close();");
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "approved", msg, true);
                            }
                            else
                            {
                                string js = "alert('Please enter a non zero value into CTM to be arranged field.');";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
                            }
                        }
                        else
                        {
                            string js = "alert('Please select supplier !.');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert212", js, true);
                        }  
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert212", "alert('" + UDFLib.GetException("SystemError/GeneralMessage") + "');", true);
                   
                }
                else // cancellation 
                {
                    int Res = BLL_PB_PortageBill.UPDATE_CTM_Approval(Vessel_ID, CTM_ID, Office_ID, UDFLib.ConvertToDecimal(ViewState["Total_AmtOffice"]), GetSessionUserID(), txtApprovalRemark.Text, ApprovalStatus, uc_SupplierListCTM.SelectedValue, UDFLib.ConvertToDecimal(txtSupplierCommission.Text));
                    if (Res == 1)
                    {
                        String msg = String.Format("alert('Cancelled successfully.');window.opener.location.reload();self.close();");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cancelled", msg, true);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnRework_Click(object s, EventArgs e)
    {
        if (BLL_PB_PortageBill.Upd_CTM_Rework(CTM_ID, Vessel_ID, txtApprovalRemark.Text, Convert.ToInt32(Session["USERID"])) == 1)
        {
            String msg = String.Format("alert('Rollback successfully.');window.opener.location.reload();self.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgclosesucesc", msg, true);
        }
        else
        {
            String msg = String.Format("alert('Error while processing your request !.');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgclosesucesc", msg, true);
        }
    }

    protected void ucPortCallsCtm_Selected(PortCalls objP)
    {
        string js = "hideModal('dvShowPortCalls');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);

        hdfPort_ID.Value = objP.Port_ID.ToString();
        lblportname.Text = objP.Port_Name;
        hdfPortCall_ID.Value = objP.Port_Call_ID.ToString();
        txtCTM_Supply_Date.Text = Convert.ToDateTime(objP.Arrival_Date).ToString("dd/MM/yyyy");

        if (CTM_ID > 0)
        {
            try
            {
                int Res = BLL_PB_PortageBill.UPDATE_CTM_Port(Vessel_ID, CTM_ID, Office_ID, txtCTM_Supply_Date.Text, UDFLib.ConvertToInteger(hdfPort_ID.Value), GetSessionUserID(), UDFLib.ConvertToInteger(hdfPortCall_ID.Value));

                Load_CTM_Details();
                lblPortcallMessage.Text = "Port has been updated successfully.";
            }
            catch (Exception ex) { lblPortcallMessage.Text = ex.Message; }
        }
    }

    protected int Save_CTM_Details()
    {
        int sts = 0;
        try
        {
            if (uc_SupplierListCTM.SelectedValue.ToString() != "" && txtSupplierCommission.Text.Trim() == "")
            {
                string js = "alert('Please enter Supplier Commission.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert2", js, true);
            }
            else
            {
                DataTable dtDenominations = new DataTable();
                dtDenominations.Columns.Add("PID");
                dtDenominations.Columns.Add("ID");
                dtDenominations.Columns.Add("Denomination");
                dtDenominations.Columns.Add("NoOfNotes_By_Office");
                dtDenominations.Columns.Add("NoOfNotes_By_Capt");
                int i = 1;
                decimal Total_AmtOffice = 0;
                foreach (GridViewRow grDen in gvDenominations.Rows)
                {
                    DataRow drDen = dtDenominations.NewRow();
                    drDen["PID"] = i;
                    drDen["ID"] = UDFLib.ConvertToInteger(gvDenominations.DataKeys[grDen.RowIndex].Value.ToString());
                    drDen["Denomination"] = UDFLib.ConvertToInteger((grDen.FindControl("txtDenomination") as TextBox).Text);
                    drDen["NoOfNotes_By_Office"] = UDFLib.ConvertToInteger((grDen.FindControl("txtNoOfNotes_by_Office") as TextBox).Text);
                    drDen["NoOfNotes_By_Capt"] = UDFLib.ConvertToInteger((grDen.FindControl("lblNoOfNotes_by_Capt") as Label).Text);
                    Total_AmtOffice += UDFLib.ConvertToInteger((grDen.FindControl("txtDenomination") as TextBox).Text) * UDFLib.ConvertToInteger((grDen.FindControl("txtNoOfNotes_by_Office") as TextBox).Text);

                    dtDenominations.Rows.Add(drDen);
                    if (UDFLib.ConvertToInteger(gvDenominations.DataKeys[grDen.RowIndex].Value.ToString()) == 0)
                        i++;
                }
                ViewState["Total_AmtOffice"] = Total_AmtOffice;
                DataTable dtOffSigners = new DataTable();
                dtOffSigners.Columns.Add("PID");
                dtOffSigners.Columns.Add("ID");
                dtOffSigners.Columns.Add("CrewID");
                dtOffSigners.Columns.Add("OffSignerName");
                dtOffSigners.Columns.Add("OffSignerRank");
                dtOffSigners.Columns.Add("DateOfSignOff");
                dtOffSigners.Columns.Add("BOWAmt");

                i = 1;
                foreach (GridViewRow grSn in gvCTM_OffSigners.Rows)
                {
                    if (grSn.RowType != DataControlRowType.Footer)
                    {
                        DataRow drSn = dtOffSigners.NewRow();
                        drSn["PID"] = i;
                        drSn["ID"] = UDFLib.ConvertToInteger(gvCTM_OffSigners.DataKeys[grSn.RowIndex].Value.ToString());
                        drSn["CrewID"] = UDFLib.ConvertToInteger((grSn.FindControl("hdfCrewid") as HiddenField).Value);
                        drSn["OffSignerName"] = Convert.ToString((grSn.FindControl("lblStaff_FullName") as Label).Text);
                        drSn["OffSignerRank"] = UDFLib.ConvertToInteger((grSn.FindControl("hdfRankID") as HiddenField).Value);
                        drSn["DateOfSignOff"] = UDFLib.ConvertToDate((grSn.FindControl("lblDateOfSignOff") as Label).Text).ToString("yyyy-MM-dd");
                        drSn["BOWAmt"] = UDFLib.ConvertToDecimal((grSn.FindControl("lblBOWAmt") as Label).Text);

                        dtOffSigners.Rows.Add(drSn);

                        if (UDFLib.ConvertToInteger(gvCTM_OffSigners.DataKeys[grSn.RowIndex].Value.ToString()) == 0)
                            i++;
                    }
                }


                CTM_Deatils prCTM = new CTM_Deatils();
                prCTM.BOW_Calculated_Amt = UDFLib.ConvertToDecimal(txtBOWCalculated.Text);
                prCTM.Cash_OnBoard = UDFLib.ConvertToDecimal(txtCashOnBoard.Text);
                
                if (txtCTM_Supply_Date.Text.Trim()!="")
                    prCTM.CTM_Date = UDFLib.ConvertDateToNull(UDFLib.ConvertToDate(txtCTM_Supply_Date.Text).ToShortDateString());    

                prCTM.CTM_Port = UDFLib.ConvertToInteger(hdfPort_ID.Value);
                prCTM.CTM_Requested_Amt = UDFLib.ConvertToDecimal(Total_AmtOffice);
                prCTM.Office_ID = Office_ID;
                prCTM.Vessel_ID = Vessel_ID;
                prCTM.CTM_ID = CTM_ID;
                prCTM.Denomination = dtDenominations;
                prCTM.OffSigners = dtOffSigners;
                prCTM.CTM_Remark = txtCTMRemark.Text;

                sts = BLL_PB_PortageBill.UPD_Save_CTM_Details(Convert.ToInt32(Session["userid"]), prCTM);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            sts = 0;
        }
        return sts;


    }

    decimal TotalBOWDeleted = 0;
    decimal TotalBOW = 0;
    protected void gvCTM_OffSigners_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.FindControl("lblActiveStatus") as Label).Text.Equals("deleted"))
                TotalBOWDeleted += UDFLib.ConvertToDecimal((e.Row.FindControl("lblBOWAmt") as Label).Text);
            else
                TotalBOW += UDFLib.ConvertToDecimal((e.Row.FindControl("lblBOWAmt") as Label).Text);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            (e.Row.FindControl("lblOffSignerBowTotal") as Label).Text = (TotalBOW + TotalBOWDeleted).ToString("0.00");
        }

    }
    protected void DDLVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Vessel_ID = UDFLib.ConvertToInteger(DDLVessel.SelectedValue);
        Load_CTM_Offsigners();

        ucPortCallsCtm.BindPortCalls(Vessel_ID);

        txtBOWCalculated.Text = TotalBOW.ToString(" ###,##0.00");

        DataTable dtCashonBoard = BLL_PB_PortageBill.Get_Cash_On_Board(Vessel_ID);
        if (dtCashonBoard.Rows.Count > 0)
            txtCashOnBoard.Text = UDFLib.ConvertToDecimal(dtCashonBoard.Rows[0]["CashOnBoardAmt"]).ToString(" ###,##0.00");
    }
}

