using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.IO;
using AjaxControlToolkit4;

public partial class Purchase_LOG_Logistic_PO_Details : System.Web.UI.Page
{
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (AjaxFileUpload1.IsInFileUploadPostBack)
        {

        }
        else
        {
            UserAccessValidation();
            // lblLogisticId.Attributes.Add("style", "visibility:hidden");
            lblMessage.Visible = false;
            ViewState["deleted"] = "false";
            uc_SupplierList1.Web_Method_URL = "/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/JibeWebService.asmx/asyncGet_Supplier_List";
            ctlPortList1.Web_Method_URL = "/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/JibeWebService.asmx/asyncGet_Port_List";

            btnLoadFiles.Attributes.Add("style", "visibility:hidden");
            if (!IsPostBack)
            {
                FillDDLs();

                imgbtnPurchaserRemark.Attributes.Add("onmouseover", "ASync_Get_Log_Remark(" + Request.QueryString["LOG_ID"] + ",event,this,0,3)");
                DataSet dsLog = BLL_PURC_LOG.Get_Log_Logistic_PO_Details(UDFLib.ConvertToInteger(Request.QueryString["LOG_ID"]));


                ViewState["Dept_Code"] = dsLog.Tables[1].Rows[0]["DEPARTMENT"].ToString();

                ViewState["deleted"] = Convert.ToString(dsLog.Tables[0].Rows[0]["active_status"]) == "0" ? true : false;

                // lblVesselName.Text = dsLog.Tables[0].Rows[0]["Vessel_Name"].ToString();
                lblLpoCode.Text = Request.QueryString["LOG_ID"];
                string wh = "LOG_ID=" + Request.QueryString["LOG_ID"];
                string msgmodal = String.Format(" Get_Record_Information_Details('PURC_LIB_LOG_PO','" + wh + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Get_Record", msgmodal, true);
                Session["LOG_ID"] = Request.QueryString["LOG_ID"].ToString();

                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Get_records", "Get_records();", true);
                try
                {
                    DDLCurrency.ClearSelection();

                    if (DDLCurrency.Items.FindByText(dsLog.Tables[0].Rows[0]["Currency"].ToString()) != null)
                    {
                        DDLCurrency.Items.FindByText(dsLog.Tables[0].Rows[0]["Currency"].ToString()).Selected = true;
                        if(dsLog.Tables[3].Rows.Count>0)
                            hdfExchrate.Value = dsLog.Tables[3].Rows[0]["ExchRate"].ToString();
                    }

                    ddlHub.ClearSelection();

                    if (ddlHub.Items.FindByValue(dsLog.Tables[0].Rows[0]["Hub"].ToString()) != null)
                        ddlHub.Items.FindByValue(dsLog.Tables[0].Rows[0]["Hub"].ToString()).Selected = true;

                    ddlAgentFord.ClearSelection();

                    if (ddlAgentFord.Items.FindByValue(dsLog.Tables[0].Rows[0]["Supplier_Code"].ToString()) != null)
                        ddlAgentFord.Items.FindByValue(dsLog.Tables[0].Rows[0]["Supplier_Code"].ToString()).Selected = true;


                    rbtnlistPOType.ClearSelection();

                    if (rbtnlistPOType.Items.FindByValue(dsLog.Tables[0].Rows[0]["PO_Type"].ToString()) != null)
                        rbtnlistPOType.Items.FindByValue(dsLog.Tables[0].Rows[0]["PO_Type"].ToString()).Selected = true;

                    rbtnlistCostType.ClearSelection();

                    if (rbtnlistCostType.Items.FindByValue(dsLog.Tables[0].Rows[0]["Cost_Type"].ToString()) != null)
                        rbtnlistCostType.Items.FindByValue(dsLog.Tables[0].Rows[0]["Cost_Type"].ToString()).Selected = true;

                    uc_SupplierList1.SelectedValue = dsLog.Tables[0].Rows[0]["PO_Supplier"].ToString();


                    ctlPortList1.SelectedValue = Convert.ToString(dsLog.Tables[0].Rows[0]["Port"]);


                    //ctlPortList1.SelectedValue = dsLog.Tables[0].Rows[0]["Port"].ToString();

                    gvAttachment.DataSource = BLL_PURC_LOG.Get_Log_Attachment(Request.QueryString["LOG_ID"]);
                    gvAttachment.DataBind();



                }
                catch { }

                dlReqsnPOs.DataSource = dsLog.Tables[1];
                dlReqsnPOs.DataBind();

                if (dsLog.Tables[2].Rows.Count == 0) // creating new PO
                {
                    ViewState["IsCreatingNewPO"] = true;
                    // create  rows based on vessels selected for logistic po
                    DataRow dr;
                    DataTable dtVesselsInLPO = BLL_PURC_LOG.Get_VesselInLogisticPO(UDFLib.ConvertToInteger(Request.QueryString["LOG_ID"]));
                    if (dtVesselsInLPO.Rows.Count == 1)
                    {
                        dr = dsLog.Tables[2].NewRow();
                        dr["item_id"] = 0;
                        dr["vessel_id"] = 0;
                        dsLog.Tables[2].Rows.Add(dr);
                    }
                    else
                    {
                        foreach (DataRow drvsl in dtVesselsInLPO.Rows)
                        {
                            dr = dsLog.Tables[2].NewRow();
                            dr["item_id"] = 0;
                            dr["vessel_id"] = drvsl["vessel_id"];
                            dsLog.Tables[2].Rows.Add(dr);
                        }
                    }

                    gvItemList.DataSource = dsLog.Tables[2];
                    gvItemList.DataBind();


                    if (dsLog.Tables[1].Rows.Count == 1)// select single po option 
                        rbtnlistPOType.Items.FindByValue("SPO").Selected = true;
                    else
                        rbtnlistPOType.Items.FindByValue("CPO").Selected = true;

                }
                else
                {
                    ViewState["IsCreatingNewPO"] = false;
                    gvItemList.DataSource = dsLog.Tables[2];
                    gvItemList.DataBind();

                    string CalculateTotal = String.Format("CalculateTotal();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CalculateTotal", CalculateTotal, true);

                }

                ViewState["dtGridItems"] = dsLog.Tables[2];


                lstUserList.DataSource = BLL_PURC_LOG.Get_Log_Logistic_Approver();
                lstUserList.DataBind();
                lstUserList.Items.Insert(0, new ListItem("SELECT", "0"));
                lstUserList.SelectedIndex = 0;
                ListItem itemrmv = lstUserList.Items.FindByValue(Session["userid"].ToString());
                lstUserList.Items.Remove(itemrmv);



                //ucPurcAttachment1.ReqsnNumber = Request.QueryString["LOG_ID"].ToString();
                //ucPurcAttachment1.UserID = Session["USERID"].ToString();
                //ucPurcAttachment1.FileUploadPath = Server.MapPath("../Uploads/Purchase");
                //ucPurcAttachment1.VesselID = "0";

                ASP.global_asax.AttachedFile = "";
                Session["PURCATTACHEDFILES"] = "";


                DataTable dtDeletedPOs = BLL_PURC_LOG.Get_Log_Deleted_LPO(Request.QueryString["LOG_ID"]);
                if (dtDeletedPOs.Rows.Count > 0)
                {
                    gvDeletedPOs.DataSource = dtDeletedPOs;
                    gvDeletedPOs.DataBind();
                }
                else
                {
                    lbllblexpand.Visible = false;
                    pnldeletedpo.Visible = false;
                }
            }


            if (IsPostBack)
            {
                // ucPurcAttachment1.Register_JS_Attach();
                Session["PURCATTACHEDFILES"] = ASP.global_asax.AttachedFile;

            }

            btnShowCnacelLPO.Visible = false;

            if (UDFLib.ConvertToInteger(Request.QueryString["IsApproving"]) == 0)
            {
                pnlApprove.Visible = false;

            }
            else
            {
                btnSavePODetails.Visible = false;
                btnDeleteLO.Enabled = false;
                btnSendForApproval.Visible = false;
                foreach (DataListItem item in dlReqsnPOs.Items)
                {
                    (item.FindControl("imgbtnDelete") as ImageButton).Visible = false;
                }

                foreach (GridViewRow gr in gvItemList.Rows)
                {
                    (gr.FindControl("imgbtnDeleteitem") as ImageButton).Visible = false;
                }
                (gvItemList.FooterRow.FindControl("btnAddNewItem") as Button).Visible = false;


            }

            if (UDFLib.ConvertToInteger(Request.QueryString["IsApproved"]) == 1)
            {
                pnlApprove.Visible = false;
                btnSavePODetails.Visible = false;
                btnDeleteLO.Enabled = false;
                btnSendForApproval.Visible = false;
                foreach (DataListItem item in dlReqsnPOs.Items)
                {
                    (item.FindControl("imgbtnDelete") as ImageButton).Visible = false;

                }

                foreach (GridViewRow gr in gvItemList.Rows)
                {
                    (gr.FindControl("imgbtnDeleteitem") as ImageButton).Visible = false;
                    (gr.FindControl("ddlpovessels") as DropDownList).Enabled = false;
                }
                (gvItemList.FooterRow.FindControl("btnAddNewItem") as Button).Visible = false;

                btnShowCnacelLPO.Visible = true;

            }


            DisableOnDeleted();
        }

    }

    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            imgAttach.Visible = false; //  ucPurcAttachment1.Visible = false;

        }
        if (objUA.Edit == 0)
        {
            btnSavePODetails.Visible = false;

        }
        if (objUA.Approve == 0)
        {
            btnApprove.Visible = false;
        }
        if (objUA.Delete == 0)
        {
            btnDeleteLO.Visible = false;
            btnCancelLPO.Visible = false;

        }


    }

    protected void FillDDLs()
    {
        BLL_Infra_Currency objCurr = new BLL_Infra_Currency();
        DDLCurrency.DataTextField = "Currency_Code";
        DDLCurrency.DataValueField = "Currency_ID";
        DDLCurrency.DataSource = objCurr.Get_CurrencyList();
        DDLCurrency.DataBind();
        ListItem lis = new ListItem("Select", "0");
        DDLCurrency.Items.Insert(0, lis);
        DDLCurrency.SelectedIndex = 0;


        ddlHub.DataTextField = "Description";
        ddlHub.DataValueField = "code";
        ddlHub.DataSource = BLL_PURC_LOG.Get_Log_Hub_List();
        ddlHub.DataBind();
        ListItem lisHub = new ListItem("Select", "0");
        ddlHub.Items.Insert(0, lisHub);
        ddlHub.SelectedIndex = 0;

        BLL_PURC_Purchase objport = new BLL_PURC_Purchase();
        DataTable dt = objport.SelectSupplier();
        dt.DefaultView.RowFilter = " SUPPLIER_CATEGORY in ('A','S') and ASL_Status in ('Approve','Conditional') ";

        ddlAgentFord.DataTextField = "SUPPLIER_NAME";
        ddlAgentFord.DataValueField = "SUPPLIER";
        ddlAgentFord.DataSource = dt.DefaultView.ToTable();
        ddlAgentFord.DataBind();
        ddlAgentFord.Items.Insert(0, new ListItem("SELECT", "0"));
        ddlAgentFord.SelectedIndex = 0;

        //BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
        //ctlPortList1.DataTextField = "Port_Name";
        //ctlPortList1.DataValueField = "Port_ID";
        //ctlPortList1.DataSource = objBLLPort.Get_PortList_Mini();
        //ctlPortList1.DataBind();

        //ctlPortList1.Items.Insert(0, new ListItem("SELECT", "0"));
        //ctlPortList1.SelectedIndex = 0;


    }

    protected void btnAddNewItem_Click(object s, EventArgs e)
    {
        DataTable dtGridItems = new DataTable();
        dtGridItems.Columns.Add("Item_ID");
        dtGridItems.Columns.Add("item");
        dtGridItems.Columns.Add("amount");
        dtGridItems.Columns.Add("remark");
        dtGridItems.Columns.Add("vessel_id");
        dtGridItems.Columns.Add("Item_PO");



        DataRow dr = null;
        foreach (GridViewRow grItem in gvItemList.Rows)
        {
            dr = dtGridItems.NewRow();
            dr["Item_ID"] = ((HiddenField)grItem.FindControl("hdfID")).Value;
            dr["item"] = ((TextBox)grItem.FindControl("txtItem")).Text;
            dr["amount"] = UDFLib.ConvertToDecimal(((TextBox)grItem.FindControl("txtAmount")).Text);
            dr["remark"] = ((TextBox)grItem.FindControl("txtRemark")).Text;
            dr["vessel_id"] = (grItem.FindControl("ddlpovessels") as DropDownList).SelectedValue;
            dtGridItems.Rows.Add(dr);

        }

        dr = dtGridItems.NewRow();
        dr["Item_ID"] = 0;
        dr["vessel_id"] = 0;
        dtGridItems.Rows.Add(dr);
        gvItemList.DataSource = dtGridItems;
        gvItemList.DataBind();

        string CalculateTotal = String.Format("CalculateTotal();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CalculateTotal", CalculateTotal, true);


    }

    protected void imgbtnDelete_Click(object s, EventArgs e)
    {
        if (BLL_PURC_LOG.Upd_Log_Delete_Logistic_ReqsnPO(Convert.ToInt32((s as ImageButton).CommandArgument), Convert.ToInt32(Session["userid"])) > 0)
        {

            DataSet dsLog = BLL_PURC_LOG.Get_Log_Logistic_PO_Details(UDFLib.ConvertToInteger(Request.QueryString["LOG_ID"]));
            dlReqsnPOs.DataSource = dsLog.Tables[1];
            dlReqsnPOs.DataBind();

            gvItemList.DataSource = dsLog.Tables[2];
            gvItemList.DataBind();

            upditemlist.Update();
        }
    }
    protected void btnDeleteLPO_Click(object s, EventArgs e)
    {
        BLL_PURC_LOG.Ins_Log_Remark(Convert.ToInt32(Request.QueryString["LOG_ID"].ToString()), txtremarkDeleteLPO.Text, Convert.ToInt32(Session["USERID"]), 4);

        if (BLL_PURC_LOG.Upd_Log_Delete_LogisticPO(Convert.ToInt32(Request.QueryString["LOG_ID"]), Convert.ToInt32(Session["userid"])) > 0)
        {
            string deleted = String.Format("parent.ReloadParent_ByButtonID();alert('PO deleted successfully .');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "deletedPO", deleted, true);

        }
    }

    protected void btnSavePODetails_Click(object sender, EventArgs e)
    {
        btnSavePODetails.Enabled = false;

        int sts = SaveLPODetails();
        string CalculateTotal = "";
        if (sts > 0)
        {
            if (!Boolean.Parse(ViewState["IsCreatingNewPO"].ToString()))
            {
                CalculateTotal = String.Format("parent.ReloadParent_ByButtonID();");
            }
            else
            {
                CalculateTotal = String.Format("parent.redirecttoindexpage();");
            }
        }
        else
        {
            btnSavePODetails.Enabled = true;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CalculateTotal", CalculateTotal, true);


    }

    protected int SaveLPODetails()
    {
        DataTable dtGridItems = new DataTable();
        dtGridItems.Columns.Add("PID");
        dtGridItems.Columns.Add("Item_ID");
        dtGridItems.Columns.Add("item");
        dtGridItems.Columns.Add("amount");
        dtGridItems.Columns.Add("remark");
        dtGridItems.Columns.Add("vessel_id");

        dtGridItems.PrimaryKey = new DataColumn[] { dtGridItems.Columns["PID"] };

        DataRow dr = null;
        int RowID = 1;
        foreach (GridViewRow grItem in gvItemList.Rows)
        {
            dr = dtGridItems.NewRow();
            dr["PID"] = RowID++;
            dr["Item_ID"] = ((HiddenField)grItem.FindControl("hdfID")).Value;
            dr["item"] = ((TextBox)grItem.FindControl("txtItem")).Text;
            dr["amount"] = UDFLib.ConvertToDecimal(((TextBox)grItem.FindControl("txtAmount")).Text);
            dr["remark"] = ((TextBox)grItem.FindControl("txtRemark")).Text;
            dr["vessel_id"] = (grItem.FindControl("ddlpovessels") as DropDownList).SelectedValue;

            //if (dtGridItems.Rows.Contains(dr["vessel_id"].ToString()) || dr["vessel_id"].ToString() == "0")
            //{
            //    lblMessage.Visible = true;
            //    lblMessage.Text = "Same vessel can not be selected more than one time . Please select different vessel for each row.";
            //    return 0;
            //}
            if (dr["item"].ToString().Trim() == "")
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please enter item details .";
                return 0;
            }
            if (Convert.ToDecimal(dr["amount"].ToString()) < 1)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Amount should be greater than zero !";
                return 0;
            }

            dtGridItems.Rows.Add(dr);
        }

        return BLL_PURC_LOG.Ins_Log_LogisticPO_Details(UDFLib.ConvertToInteger(Request.QueryString["LOG_ID"]), DDLCurrency.SelectedItem.Text, rbtnlistPOType.SelectedValue,
                                                    rbtnlistCostType.SelectedValue, ddlHub.SelectedValue, ddlAgentFord.SelectedValue, UDFLib.ConvertToInteger(ctlPortList1.SelectedValue), "", dtGridItems, Convert.ToInt32(Session["USERID"].ToString()), uc_SupplierList1.SelectedValue);

    }

    protected void imgbtnDeleteitem_Click(object sender, EventArgs e)
    {
        if (gvItemList.Rows.Count > 1)
        {
            GridViewRow dritem = (GridViewRow)(sender as ImageButton).Parent.Parent;

            DataTable dtGridItems = new DataTable();
            dtGridItems.Columns.Add("Item_ID");
            dtGridItems.Columns.Add("item");
            dtGridItems.Columns.Add("amount");
            dtGridItems.Columns.Add("remark");
            dtGridItems.Columns.Add("vessel_id");
            dtGridItems.Columns.Add("Item_PO");

            int RowID = 0;
            DataRow dr = null;
            foreach (GridViewRow grItem in gvItemList.Rows)
            {
                if (dritem.RowIndex != RowID)
                {

                    dr = dtGridItems.NewRow();
                    dr["Item_ID"] = ((HiddenField)grItem.FindControl("hdfID")).Value;
                    dr["item"] = ((TextBox)grItem.FindControl("txtItem")).Text;
                    dr["amount"] = UDFLib.ConvertToDecimal(((TextBox)grItem.FindControl("txtAmount")).Text);
                    dr["remark"] = ((TextBox)grItem.FindControl("txtRemark")).Text;
                    dr["vessel_id"] = (grItem.FindControl("ddlpovessels") as DropDownList).SelectedValue;
                    dtGridItems.Rows.Add(dr);
                }

                RowID++;
            }

            //delete from database
            if (!Convert.ToBoolean(ViewState["IsCreatingNewPO"]))
            {
                BLL_PURC_LOG.Upd_Log_LogisticPO_Item(Convert.ToInt32((dritem.FindControl("hdfID") as HiddenField).Value), Convert.ToInt32(Session["USERID"]));

            }
            gvItemList.DataSource = dtGridItems;
            gvItemList.DataBind();
            ViewState["vsItemList"] = dtGridItems;
        }
        string CalculateTotal = String.Format("CalculateTotal();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CalculateTotal", CalculateTotal, true);
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        BLL_PURC_Purchase objApproval = new BLL_PURC_Purchase();
        
        
        BLL_PURC_LOG.Ins_Log_Remark(Convert.ToInt32(Request.QueryString["LOG_ID"].ToString()), txtApproverRemark.Text, Convert.ToInt32(Session["USERID"]), 2);
        btnApprove.Enabled = false;
        SaveLPODetails();

        DataTable dtSuppDate = BLL_PURC_Common.Get_Supplier_ValidDate("'" + uc_SupplierList1.SelectedValue + "'");
        if (dtSuppDate.Rows.Count > 0)
        {
            if (Convert.ToDateTime(dtSuppDate.Rows[0]["ASL_Status_Valid_till"]) < DateTime.Now)
            {
                String msg = String.Format("alert('Selected Supplier has been expired/blacklisted');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg451", msg, true);

            }
            else
            {
                decimal TotalAmountToApproved = UDFLib.ConvertToDecimal(hdf_TotalAmount_USD.Value);

                ViewState["TotalAmountToApproved"] = TotalAmountToApproved;

                /*Below code is commented due to this JIT_8823 as below method checks for department as it doesnt required for Logistic Po.*/
                //DataTable dtApproval = objApproval.Get_Approval_Limit(Convert.ToInt32(Session["USERID"]), ViewState["Dept_Code"].ToString());
                
                decimal Applimit = BLL_PURC_LOG.Get_Log_Logistic_Approval_Limit(Convert.ToInt32(Session["USERID"]));

                //decimal Applimit = decimal.Parse(dtApproval.Rows[0]["Approval_Limit"].ToString());

             

                if (Applimit < 1)
                {
                    string msgmodal = String.Format("alert('Approval limit not found !')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Apprmodal", msgmodal, true);
                }
                else if (Applimit >= TotalAmountToApproved)
                {

                    BLL_PURC_LOG.Ins_Log_Logistic_Approval_Entry(Convert.ToInt32(Session["USERID"].ToString()),
                                                        Convert.ToInt32(Session["USERID"].ToString()),
                                                        TotalAmountToApproved,
                                                        Convert.ToInt32(Request.QueryString["LOG_ID"].ToString()), "", txtApproverRemark.Text, 1);

                    string msgmodal = String.Format("alert('Approved successfully');parent.ReloadParent_ByButtonID();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Apprmodala", msgmodal, true);
                }
                else
                {
                    ViewState["islimit<Total"] = 1;
                    string msgmodal = String.Format("showModal('divSendForApproval');alert('The total PO amount is beyond your approval limit.Please forward to one of the following for approval')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Apprmodal", msgmodal, true);


                }
            }
        }
    }

    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {
        BLL_PURC_LOG.Ins_Log_Remark(Convert.ToInt32(Request.QueryString["LOG_ID"].ToString()), txtRemark.Text, Convert.ToInt32(Session["USERID"]), 3);

        if (SaveLPODetails() > 0)
        {


            if (UDFLib.ConvertToInteger(ViewState["islimit<Total"]) == 1)//save the approval
            {
                BLL_PURC_LOG.Ins_Log_Logistic_Approval_Entry(Convert.ToInt32(Session["USERID"].ToString()),
                                                    Convert.ToInt32(Session["USERID"].ToString()),
                                                    UDFLib.ConvertToDecimal(ViewState["TotalAmountToApproved"]),
                                                    Convert.ToInt32(Request.QueryString["LOG_ID"].ToString()), "", txtApproverRemark.Text);
            }

            //  send for appr
            BLL_PURC_LOG.Ins_Log_Logistic_Approval_Entry(Convert.ToInt32(Session["USERID"].ToString()),
                                                 Int32.Parse(lstUserList.SelectedValue),
                                                   0,
                                                   Convert.ToInt32(Request.QueryString["LOG_ID"].ToString()), txtRemark.Text, "");
            string msgmodal = String.Format("alert('Sent successfully');parent.ReloadParent_ByButtonID();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Apprmodal", msgmodal, true);
        }
    }

    protected void gvItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.FindControl("ddlpovessels") as DropDownList).Items.Count == 2)
            {
                (e.Row.FindControl("ddlpovessels") as DropDownList).SelectedIndex = 1;
                (e.Row.FindControl("ddlpovessels") as DropDownList).Enabled = false;
            }

            if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vessel_id")) != "0") // will be 0 for first time 
            {

                ListItem litems = (e.Row.FindControl("ddlpovessels") as DropDownList).Items.FindByValue(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vessel_id")));
                if (litems != null)
                {
                    (e.Row.FindControl("ddlpovessels") as DropDownList).ClearSelection();
                    litems.Selected = true;
                }
                else
                {
                    (e.Row.FindControl("ddlpovessels") as DropDownList).ClearSelection();
                    (e.Row.FindControl("ddlpovessels") as DropDownList).Items.FindByValue("0").Selected = true;
                }

            }
        }
    }

    protected void btnReworkToPurchaser_Click(object s, EventArgs e)
    {
        try
        {
            BLL_PURC_LOG.Ins_Log_Remark(Convert.ToInt32(Request.QueryString["LOG_ID"].ToString()), txtApproverRemark.Text, Convert.ToInt32(Session["USERID"]), 6);
            int sts = BLL_PURC_LOG.Upd_Log_ReworkToPurchaser(Convert.ToInt32(Request.QueryString["LOG_ID"].ToString()), Convert.ToInt32(Session["USERID"]));

            if (sts > 0)
            {
                string msgmodal = String.Format("parent.ReloadParent_ByButtonID();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "rework", msgmodal, true);
            }

        }
        catch (Exception ex)
        {
            string msgmodal = String.Format("alert('" + ex.Message + "'); parent.ReloadParent_ByButtonID();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "rework", msgmodal, true);
        }
    }


    protected void btnCnacelLPO_Click(object s, EventArgs e)
    {
        try
        {
            BLL_PURC_LOG.Ins_Log_Remark(Convert.ToInt32(Request.QueryString["LOG_ID"].ToString()), txtCancelLPORemark.Text, Convert.ToInt32(Session["USERID"]), 7);
            int sts = BLL_PURC_LOG.Upd_Log_Cancel_LPO(Convert.ToInt32(Request.QueryString["LOG_ID"].ToString()), Convert.ToInt32(Session["USERID"]));
            if (sts > 0)
            {
                string msgmodal = String.Format("parent.ReloadParent_ByButtonID();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cancellpo", msgmodal, true);
            }
            else if (sts == -1001)
            {
                string msgmodal = String.Format("alert('Invoice has been uploaded !')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cancellpoinvoce", msgmodal, true);
            }

        }
        catch (Exception ex)
        {
            string msgmodal = String.Format("alert('" + ex.Message + "'); parent.ReloadParent_ByButtonID();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cancellpo", msgmodal, true);
        }

    }

    public void imgbtnDeleteAttachment_Click(object s, EventArgs e)
    {
        try
        {

            BLL_PURC_Purchase objAttch = new BLL_PURC_Purchase();

            int ID = int.Parse(((ImageButton)s).CommandArgument.Split(new char[] { ',' })[0]);
            int res = objAttch.Purc_Delete_Reqsn_Attachments(ID);
            if (res > 0)
            {
                File.Delete(Server.MapPath(((ImageButton)s).CommandArgument.Split(new char[] { ',' })[1]));
            }

            LoadFiles(null, null);


        }
        catch
        { }
    }

    public void LoadFiles(object s, EventArgs e)
    {
        try
        {

            DataTable dtAttachedFile = BLL_PURC_LOG.Get_Log_Attachment(Request.QueryString["LOG_ID"]);

            gvAttachment.DataSource = dtAttachedFile;
            gvAttachment.DataBind();
            ASP.global_asax.AttachedFile = "";
            Session["PURCATTACHEDFILES"] = "";
        }
        catch { }

    }
    protected void gvAttachment_DataBound(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["PURCATTACHEDFILES"]).Length > 0)
        {
            string[] arrID = Convert.ToString(Session["PURCATTACHEDFILES"]).Remove(Convert.ToString(Session["PURCATTACHEDFILES"]).Length - 1, 1).Split(',');

            foreach (GridViewRow gr in gvAttachment.Rows)
            {
                foreach (string sid in arrID)
                {
                    if (sid == gvAttachment.DataKeys[gr.RowIndex].Value.ToString())
                    {
                        (gr.FindControl("imgbtnDelete") as ImageButton).Visible = true;
                    }
                }
            }
        }
    }
    protected void DDLCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL_Infra_ExchangeRate objexch = new BLL_Infra_ExchangeRate();
        hdfExchrate.Value = objexch.ACC_Get_ExchRate_By_Code(DDLCurrency.SelectedItem.Text).ToString();
        string CalculateTotal = String.Format("CalculateTotal();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CalculateTotal", CalculateTotal, true);
    }


    protected void DisableOnDeleted()
    {
        if (Convert.ToBoolean(ViewState["deleted"]))
        {
            imgAttach.Visible = false;
            btnDeleteLO.Visible = false;
            btnCancelLPO.Visible = false;
            btnApprove.Visible = false;
            btnSavePODetails.Visible = false;
            this.Form.Disabled = true;
            foreach (DataListItem item in dlReqsnPOs.Items)
            {
                (item.FindControl("imgbtnDelete") as ImageButton).Visible = false;
            }

            foreach (GridViewRow gr in gvItemList.Rows)
            {
                (gr.FindControl("imgbtnDeleteitem") as ImageButton).Visible = false;
            }
            (gvItemList.FooterRow.FindControl("btnAddNewItem") as Button).Visible = false;
        }
    }
    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {


            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();

            Byte[] fileBytes = file.GetContents();
            string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Purchase");
            Guid GUID = Guid.NewGuid();
            string Flag_Attach = GUID.ToString() + Path.GetExtension(file.FileName);

            int sts = objTechService.SaveAttachedFileInfo("0", Session["LOG_ID"].ToString(), uc_SupplierList1.SelectedValue.ToString(), Path.GetExtension(file.FileName), UDFLib.Remove_Special_Characters(file.FileName), "../Uploads/Purchase/" + Flag_Attach, Session["USERID"].ToString(), 0);

            string FullFilename = Path.Combine(sPath, GUID.ToString() + Path.GetExtension(file.FileName));
            FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();

        }
        catch (Exception ex)
        {

        }

    }
    protected void btnLoadFiles_Click(object sender, EventArgs e)
    {
        LoadFiles(null, null);
    }


}