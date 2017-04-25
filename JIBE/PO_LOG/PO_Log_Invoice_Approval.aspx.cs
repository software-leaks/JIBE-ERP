using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.POLOG;


public partial class PO_LOG_PO_Log_Invoice_Approval : System.Web.UI.Page
{
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    UserAccess objUA = new UserAccess();
    public string Type = null;
    public string CurrStatus = null;
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();
    MergeGridviewHeader_Info objChangeReqstMerge1 = new MergeGridviewHeader_Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            chkVerified.Checked = true;
            BindType();

            Load_dropdownlist();
            //hdfSelectedStageValue.Value = "Verified";
            //hdfSelectedStage.Value = lnkMenu1.ClientID;
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "selectNode", "selMe('" + hdfSelectedStage.Value + "');", true);
            BindGrid();
            //BindApprovedGrid();
        }

    }
    public void Load_dropdownlist()
    {
        string SearchType = "APPROVAL";
        DataSet ds = BLL_POLOG_Register.POLOG_Get_Supplier_InvoiceWise(UDFLib.ConvertIntegerToNull(GetSessionUserID()), SearchType);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlSupplier.DataSource = ds.Tables[0];
            ddlSupplier.DataTextField = "Supplier_Name";
            ddlSupplier.DataValueField = "Supplier_Code";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new ListItem("-All-", "0"));
        }
        else
        {
            ddlSupplier.Items.Insert(0, new ListItem("-All-", "0"));
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            ddlVessel.DataSource = ds.Tables[1];
            ddlVessel.DataTextField = "Vessel_Name";
            ddlVessel.DataValueField = "Vessel_ID";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem("-All-", "0"));
        }
        else
        {
            ddlVessel.Items.Insert(0, new ListItem("-All-", "0"));
        }
    }
    protected void BindGrid()
    {
        try
        {
            objChangeReqstMerge.AddMergedColumns(new int[] { 2, 3, 4 }, "PO", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 5, 6,7, 8, 9, 10, 11, 12, 13 }, "Invoice", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 14, 15 }, "Verified", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 16, 17 }, "Approved", "HeaderStyle-center");
            int rowcount = ucCustomPagerItems.isCountRecord;
            DataTable dtType = ChkType();
            string urgent = null;
            if (chkUrgent.Checked == true)
            {
                urgent = "YES";
            }

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            ChkStatus();
            //string  ReqsnStatus = hdfSelectedStageValue.Value;
            DataSet ds = BLL_POLOG_Register.POLOG_Get_Pending_Invoice_Search(UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                                     UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), CurrStatus, urgent, dtType, UDFLib.ConvertIntegerToNull(GetSessionUserID()), sortbycoloumn, sortdirection
                             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                //btnApprove.Visible = true;
                gvPendinginvoice.DataSource = ds.Tables[0];
                gvPendinginvoice.DataBind();
            }
            else
            {
               // btnApprove.Visible = false;
                gvPendinginvoice.DataSource = ds.Tables[0];
                gvPendinginvoice.DataBind();
                gvPendinginvoice.EmptyDataText = "NO RECORDS FOUND";
            }
          
        }
        catch { }
        {
        }
    }
    protected void ChkStatus()
    {
        if (chkVerified.Checked == true)
        {
            CurrStatus = "Verified";
        }
        if (chkRework.Checked == true)
        {
            if (CurrStatus == "")
            {
                CurrStatus = "Rework";
            }
            else
            {
                CurrStatus = CurrStatus + "," + "Rework";
            }
        }
        if (chkHold.Checked == true)
        {
            if (CurrStatus == "")
            {
                CurrStatus = "Hold";
            }
            else
            {
                CurrStatus = CurrStatus + "," + "Hold";
            }
        }
        if (chkDispute.Checked == true)
        {
            if (CurrStatus == "")
            {
                CurrStatus = "Dispute";
            }
            else
            {
                CurrStatus = CurrStatus + "," + "Dispute";
            }
        }
        if (chkApproved.Checked == true)
        {
            if (CurrStatus == "")
            {
                CurrStatus = "Approved";
            }
            else
            {
                CurrStatus = CurrStatus + "," + "Approved";
            }
        }

    }
    protected DataTable ChkType()
    {

        DataTable dtType = new DataTable();
        dtType.Columns.Add("PKID");
        dtType.Columns.Add("FKID");
        dtType.Columns.Add("Value");
        foreach (ListItem chkitem in chkType.Items)
        {

            DataRow dr = dtType.NewRow();
            if (chkitem.Selected == true)
            {
                dr["FKID"] = chkitem.Selected == true ? 1 : 0;
                dr["Value"] = chkitem.Value;
                dtType.Rows.Add(dr);
            }

        }

        return dtType;
    }

    protected void BindType()
    {
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(Session["UserID"].ToString()), "PO_TYPE");
            chkType.DataSource = ds.Tables[0];
            chkType.DataTextField = "VARIABLE_NAME";
            chkType.DataValueField = "VARIABLE_CODE";
            chkType.DataBind();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                chkType.Items[i].Selected = true;
                string color = ds.Tables[0].Rows[i]["COLOR_CODE"].ToString();
                chkType.Items[i].Attributes.Add("style", "background-color: " + color + ";");
                //chkType.Attributes.Add("style", "background-color: " + color + ";");
            }

        }
        catch { }
        {
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {

        }
        //if (objUA.Edit == 1)
        //uaEditFlag = true;
        //else
        // btnsave.Visible = false;

        //if (objUA.Delete == 1) //uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        BindGrid();
        //BindApprovedGrid();
    }
    protected void gvPendinginvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objChangeReqstMerge);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string ColorCode = DataBinder.Eval(e.Row.DataItem, "COLOR_CODE").ToString();
            System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(ColorCode);
            DateTime Invoice_Date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Invoice_Date").ToString());
            DateTime PO_Date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Created_Date").ToString());
            DateTime Payment_Due_Date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Payment_Due_Date").ToString());
            Button ViewRemarks = (Button)e.Row.FindControl("btnViewRemarks");
            Label lblUrgent = (Label)e.Row.FindControl("lblUrgent");
            Label lblUrgentFlag = (Label)e.Row.FindControl("lblUrgentFlag");
            LinkButton lblSupplier = (LinkButton)e.Row.FindControl("lbl_SupplierName");
            LinkButton lblPO = (LinkButton)e.Row.FindControl("lblPOCode");
            Label lblInvoiceStatus = (Label)e.Row.FindControl("lblInvoice_Status");
            Button btnApprove = (Button)e.Row.FindControl("btnAprove");
            Button btnunApprove = (Button)e.Row.FindControl("btnUnApprove");
            Label lblApproveMsg = (Label)e.Row.FindControl("lblApproveMsg");
            ImageButton btnonHold = (ImageButton)e.Row.FindControl("btnOnHold");
            ImageButton btnHold = (ImageButton)e.Row.FindControl("btnhold");
            Label lblDays = (Label)e.Row.FindControl("lblDays");
            Label lblPaymentDate = (Label)e.Row.FindControl("lblPaymentDate");
            Label lblInvoice_Value = (Label)e.Row.FindControl("lblInvoice_Value");
            Label lblInvoice_Currency = (Label)e.Row.FindControl("lblInvoice_Currency");
            if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Invoice_Value")) > Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PO_Amount")))
            {
                lblInvoice_Value.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblInvoice_Value.ForeColor = System.Drawing.Color.Black;
            }
            if (DataBinder.Eval(e.Row.DataItem, "CURRENCY").ToString() != DataBinder.Eval(e.Row.DataItem, "Invoice_Currency").ToString())
            {
                e.Row.Cells[13].BackColor = System.Drawing.Color.Violet;
            }
            else
            {
                e.Row.Cells[13].BackColor = System.Drawing.Color.White;
            }
            if (DataBinder.Eval(e.Row.DataItem, "Supplier_Currency").ToString() != DataBinder.Eval(e.Row.DataItem, "CURRENCY").ToString())
            {
                if (DataBinder.Eval(e.Row.DataItem, "Supplier_Currency") != null)
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Violet;
                }
            }
            else
            {
                e.Row.Cells[4].BackColor = System.Drawing.Color.White;
            }
            if (DataBinder.Eval(e.Row.DataItem, "Urgent_Flag").ToString() == "URGENT")
            {
                lblUrgent.Visible = true;
                lblUrgentFlag.Visible = true;
            }
            else
            {
                lblUrgent.Visible = false;
                lblUrgentFlag.Visible = false;
            }
            int result = DateTime.Compare(PO_Date, Invoice_Date);
            if (result > 0)
            {
                e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
                lblPO.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[1].BackColor = col;
                //lblSupplier.ForeColor = System.Drawing.Color.White;
            }
            DateTime TDate = System.DateTime.Now;
            int Diff = DateTime.Compare(Payment_Due_Date, TDate);
            if (Diff > 0)
            {
                System.TimeSpan diffResult = Payment_Due_Date - TDate;
                int differenceInDays = diffResult.Days;
                lblDays.Text = differenceInDays + " " + "Days";
                lblDays.ForeColor = System.Drawing.Color.Black;
                lblPaymentDate.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                lblPaymentDate.ForeColor = System.Drawing.Color.Red;
            }
            if (DataBinder.Eval(e.Row.DataItem, "Invoice_Status").ToString() == "Approved")
            {
                e.Row.Cells[11].BackColor = System.Drawing.Color.Green;
                lblInvoiceStatus.ForeColor = System.Drawing.Color.White;
                btnunApprove.Visible = true;
                btnApprove.Visible = false;
                lblApproveMsg.Text = "No Dispute.";
                lblApproveMsg.ForeColor = System.Drawing.Color.Green;
                btnHold.Visible = false;
                btnonHold.Visible = false;
            }
            else if (DataBinder.Eval(e.Row.DataItem, "Dispute_Flag").ToString() == "YES")
            {
                e.Row.Cells[11].BackColor = System.Drawing.Color.White;
                lblInvoiceStatus.ForeColor = System.Drawing.Color.Black;
                btnApprove.Visible = true;
                btnApprove.Enabled = false;
                btnunApprove.Visible = false;
                lblApproveMsg.Text = "Cannot approve until Dispute status is removed.";
                lblApproveMsg.ForeColor = System.Drawing.Color.Red;
            }
            else if (DataBinder.Eval(e.Row.DataItem, "Invoice_Status").ToString() == "Hold")
            {
                e.Row.Cells[11].BackColor = System.Drawing.Color.White;
                lblInvoiceStatus.ForeColor = System.Drawing.Color.Black;
                btnApprove.Visible = true;
                btnApprove.Enabled = false;
                btnunApprove.Visible = false;
                lblApproveMsg.Text = "Cannot approve until Hold status is removed.";
                btnHold.Visible = true;
                btnonHold.Visible = false;
                lblApproveMsg.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                e.Row.Cells[11].BackColor = System.Drawing.Color.White;
                lblInvoiceStatus.ForeColor = System.Drawing.Color.Black;
                btnApprove.Visible = true;
                btnApprove.Enabled = true;
                btnunApprove.Visible = false;
                lblApproveMsg.Text = "No Dispute.";
                
                lblApproveMsg.ForeColor = System.Drawing.Color.Green;
                btnHold.Visible = false;
                btnonHold.Visible = true;
            }

            //e.Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
            //string ID = UDFLib.ConvertStringToNull(gvPendinginvoice.DataKeys[e.Row.RowIndex].Value);

            //GridView gv = (GridView)e.Row.FindControl("gvRemarks");
            //DataSet ds = BLL_POLOG_Register.POLOG_Get_Remarks_ByInvoiceID(ID);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    //ViewRemarks.Visible = true;
            //    gv.DataSource = ds.Tables[0];
            //    gv.DataBind();
            //}

        }
    }


    protected void gvPendinginvoice_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;
        BindGrid();
    }

    protected void btnApprove_Click(object sender, CommandEventArgs e)
    {
        string msg2 = null;
        string[] arg = e.CommandArgument.ToString().Split(',');
        string Invoice_ID= UDFLib.ConvertStringToNull(arg[0]);
        string Status = "Approved";
        string InvStatus = "Approved";
         int Retval =   Update_Invoice(Invoice_ID,Status, InvStatus);
         if (Retval == 1)
         {
             int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(Invoice_ID), "Approved Invoice", "ApprovedInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
              msg2 = String.Format("alert('Invoice Approved.')");
             
             BindGrid();
         }
         else
         {
              msg2 = String.Format("alert('Disputed Invoice Cant be approved.')");
             
         }
         ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
    }
    protected void btnUnApprove_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string text = e.CommandName.ToString();
        string Invoice_ID = UDFLib.ConvertStringToNull(arg[0]);
        string Status = "UNAPPROVED";
        string InvStatus = "UNAPPROVED";
        Update_Invoice(Invoice_ID, Status, InvStatus);
        int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(Invoice_ID), "UNApproved Invoice", "UNApprovedInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
        string msg2 = String.Format("alert('Invoice Un-Approved.')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        BindGrid();
    }
    protected void btnOnHold_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string text = e.CommandName.ToString();
        string Invoice_ID = UDFLib.ConvertStringToNull(arg[0]);
        string Status = "Hold";
        string InvStatus = "Hold";
        Update_Invoice(Invoice_ID, Status, InvStatus);
        int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(Invoice_ID), "Hold Invoice", "HoldInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
        string msg2 = String.Format("alert('Invoice Put on Hold.')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        BindGrid();
    }
    protected void btnHold_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        string text = e.CommandName.ToString();
        string Invoice_ID = UDFLib.ConvertStringToNull(arg[0]);
        string Status = "UnHold";
        string InvStatus = "UnHold";
        Update_Invoice(Invoice_ID, Status, InvStatus);
        int RetAuditVal = BLL_POLOG_Register.POLOG_Insert_Transactionlog(UDFLib.ConvertStringToNull(Invoice_ID), "UnHold Invoice", "UnHoldInvoice", UDFLib.ConvertToInteger(GetSessionUserID()));
        string msg2 = String.Format("alert('Invoice remove from on Hold.')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        BindGrid();
    }
    protected int Update_Invoice(string Invoice_ID,string Status, string InvStatus)
    {
        try
        {
            int retval = BLL_POLOG_Register.POLog_Update_Invoice(UDFLib.ConvertStringToNull(Invoice_ID), 0, 0, InvStatus,
                                                                          Status, UDFLib.ConvertToInteger(GetSessionUserID()));
            return retval;
            //DataTable dt = new DataTable();
            //dt.Columns.Add("PKID");
            //dt.Columns.Add("ITEMREF");
            //dt.Columns.Add("VESSELID");
            //dt.Columns.Add("ChkValue");
            //int i = 0;
            //foreach (GridViewRow row in gvPendinginvoice.Rows)
            //{

            //    bool result = ((CheckBox)row.FindControl("chkInvoice")).Checked;
            //    if (result)
            //    {
            //        DataRow dr = dt.NewRow();
            //        dr["PKID"] = i + 1;
            //        dr["Invoice_ID"] = gvPendinginvoice.DataKeys[row.RowIndex].Value.ToString();
            //        dr["ChkValue"] = 1;
            //        dt.Rows.Add(dr);
            //    }

            //}
            //int retval = BLL_POLOG_Register.POLog_Update_Invoice(dt, InvStatus, Status, UDFLib.ConvertToInteger(GetSessionUserID()));
        }
        catch { }
        {
            return 0;
        }

    }
    
}
    //protected void ChkStatus()
    //{
    //    CurrStatus = null;
    //    if (chkVerified.Checked == true)
    //    {
    //        CurrStatus = "VERIFIED";
    //    }
    //    if (chkRework.Checked == true)
    //    {
    //        if (CurrStatus == "")
    //        {
    //            CurrStatus = "REWORK";
    //        }
    //        else
    //        {
    //            CurrStatus = CurrStatus + "," + "REWORK";
    //        }
    //    }
    //    if (chkHold.Checked == true)
    //    {
    //        if (CurrStatus == "")
    //        {
    //            CurrStatus = "HOLD";
    //        }
    //        else
    //        {
    //            CurrStatus = CurrStatus + "," + "HOLD";
    //        }
    //    }
    //    if (chkDispute.Checked == true)
    //    {
    //        if (CurrStatus == "")
    //        {
    //            CurrStatus = "DISPUTE";
    //        }
    //        else
    //        {
    //            CurrStatus = CurrStatus + "," + "DISPUTE";
    //        }
    //    }
    //    if (chkApproved.Checked == true)
    //    {
    //        if (CurrStatus == "")
    //        {
    //            CurrStatus = "APPROVED";
    //        }
    //        else
    //        {
    //            CurrStatus = CurrStatus + "," + "APPROVED";
    //        }
    //    }
    //    if (chkUrgent.Checked == true)
    //    {
    //        if (CurrStatus == "")
    //        {
    //            CurrStatus = "URGENT";
    //        }
    //        else
    //        {
    //            CurrStatus = CurrStatus + "," + "URGENT";
    //        }
    //    }
    //}
//protected void BindApprovedGrid()
//{
//    try
//    {
//        objChangeReqstMerge1.AddMergedColumns(new int[] { 2, 3, 4 }, "PO", "HeaderStyle-center");
//        objChangeReqstMerge1.AddMergedColumns(new int[] { 5, 6, 7, 8, 9, 10 }, "Invoice", "HeaderStyle-center");
//        objChangeReqstMerge1.AddMergedColumns(new int[] { 11, 12 }, "Invoice Verified", "HeaderStyle-center");

//        int rowcount = ucCustomPager1.isCountRecord;
//        DataTable dtType = ChkType();
//        string urgent = null;
//        if (chkUrgent.Checked == true)
//        {
//            urgent = "Yes";
//        }


//        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
//        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
//        ChkStatus();

//        DataTable dt = BLL_POLOG_Register.POLOG_Get_Approved_Invoice_Search(UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
//                                 UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), CurrStatus, dtType, UDFLib.ConvertIntegerToNull(GetSessionUserID()), sortbycoloumn, sortdirection
//                         , ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount);


//        if (ucCustomPager1.isCountRecord == 1)
//        {
//            ucCustomPager1.CountTotalRec = rowcount.ToString();
//            ucCustomPager1.BuildPager();
//        }

//        if (dt.Rows.Count > 0)
//        {
//            divApprovedInvoice.Visible = true;
//            gvApprovedInvoice.DataSource = dt;
//            gvApprovedInvoice.DataBind();
//        }
//        else
//        {
//            divApprovedInvoice.Visible = false;
//            gvApprovedInvoice.DataSource = dt;
//            gvApprovedInvoice.DataBind();
//        }
//    }
//    catch { }
//    {
//    }
//}
//protected void gvApprovedInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
//{
//    if (e.Row.RowType == DataControlRowType.Header)
//    {
//        MergeGridviewHeader.SetProperty(objChangeReqstMerge1);
//        e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
//    }
//    if (e.Row.RowType == DataControlRowType.DataRow)
//    {
//        string ColorCode = DataBinder.Eval(e.Row.DataItem, "COLOR_CODE").ToString();
//        System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(ColorCode);
//        DateTime Invoice_Date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Invoice_Date").ToString());
//        DateTime PO_Date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Created_Date").ToString());
//        Button ViewRemarks = (Button)e.Row.FindControl("btnViewRemarks");
//        Label lblUrgent = (Label)e.Row.FindControl("lblUrgent");
//        Label lblUrgentFlag = (Label)e.Row.FindControl("lblUrgentFlag");
//        LinkButton lblSupplier = (LinkButton)e.Row.FindControl("lbl_SupplierName");
//        LinkButton lblPO = (LinkButton)e.Row.FindControl("lblPOCode");
//        if (DataBinder.Eval(e.Row.DataItem, "Urgent_Flag").ToString() == "Yes")
//        {
//            lblUrgent.Visible = true;
//            lblUrgentFlag.Visible = true;
//        }
//        else
//        {
//            lblUrgent.Visible = false;
//            lblUrgentFlag.Visible = false;
//        }
//        int result = DateTime.Compare(PO_Date, Invoice_Date);
//        if (result > 0)
//        {
//            e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
//            lblPO.ForeColor = System.Drawing.Color.White;
//        }
//        else
//        {
//            e.Row.Cells[1].BackColor = col;
//            e.Row.Cells[1].ForeColor = System.Drawing.Color.White;
//        }

//    }
//}
//protected void gvApprovedInvoice_Sorting(object sender, GridViewSortEventArgs se)
//{
//    ViewState["SORTBYCOLOUMN"] = se.SortExpression;

//    if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
//        ViewState["SORTDIRECTION"] = 1;
//    else
//        ViewState["SORTDIRECTION"] = 0;
//    BindApprovedGrid();
//}
    