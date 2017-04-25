using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using Telerik.Web.UI;
using ClsBLLTechnical;
using System.IO;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using System.Web;



public partial class Purchase_Quotation_Evaluation_Gridview : System.Web.UI.Page
{
    //    private string ButtonClickStatus;
    protected CheckBox boolValue;
    protected string strColoumnName = "";
    protected bool btnActive;
    int count = 0;
    public string EvalOpt = "";

    public static string HiddenArgument = "";
    public static string DynamicHeaderCSS = "";
    public static int ColumnCount_Supp = 7;
    public DataTable dtItemsTypes = new DataTable();

    public static Dictionary<string, string> dicTotalAmount = new Dictionary<string, string>();
    DataTable dtQuatationInfo = new DataTable();
    MergeGridviewHeader_Info objApprovalHist = new MergeGridviewHeader_Info();
    protected void Page_Init(object source, System.EventArgs e)
    {
        ViewState["RetriveBtnPress"] = false;
    }


    protected void Page_Load(object sender, EventArgs e)
    {


        UserAccessValidation();
        hdf_firstTime_clicked.Value = "1";
        hdf_QtnCode_FinalAmount.Value = "";
        optEvalCSupp.Checked = false;
        optEvalCItm.Checked = false;
        btnApprove.Attributes.Add("onclick", "Async_Get_Reqsn_Validity('" + Request.QueryString["Requisitioncode"] + "');");
        hdReqnCode.Value = Request.QueryString["Requisitioncode"].ToString();
        hdDocumentCode.Value = Request.QueryString["Document_Code"].ToString();
        hdVesselCode.Value = Request.QueryString["Vessel_Code"].ToString();
        lbtnPurchaserRemark.Attributes.Add("onmouseover", "javascript:GetRemark_ByRemarkType('302','" + Request.QueryString["Document_Code"] + "','1');return false;");
        lbtnPurchaserRemark.Attributes.Add("onmouseout", "javascript:CloseRemarkToolTip();return false;");

        lbtnAllremarks.Attributes.Add("onmouseover", "javascript:GetRemarkToolTip('" + Request.QueryString["Document_Code"] + "','1',event);return false;");
        lbtnAllremarks.Attributes.Add("onmouseout", "javascript:CloseRemarkToolTip();return false;");
        lbtnAllremarks.Attributes.Add("onclick", "javascript:GetRemarkAll('" + Request.QueryString["Document_Code"] + "','" + Session["userid"].ToString() + "',null,event);return false;");

        hdfUserIDSaveEval.Value = Session["userid"].ToString();
        btnActive = (bool)ViewState["RetriveBtnPress"];

        rgdSupplierInfo.Attributes.Add("bordercolor", "#D8D8D8");
        rgdQuatationInfo.Attributes.Add("border-color", "#D8D8D8");


        //dvReworktoSuppler.Visible = false;
        //string s = Request.QueryString["Vessel_Code"].ToString();
        dvSendForApproval.Visible = false;
        dvSendTosuppdt.Visible = false;
        if (!IsPostBack)
        {
            btnSaveEvaln.Enabled = false;
            btnFinalizeEval.Enabled = false;
            btnReq_Ord.Enabled = false;
            ViewState["HiddenArgument"] = "";
            ViewState["DynamicHeaderCSS"] = "";
            ViewState["ColumnCount_Supp"] = 7;

            BindRequisitionInfo();
            BindQuatationSendBySupplier();


            HiddenbtnRetrive.Value = "0";
            HiddenDocumentCode.Value = Request.QueryString["Document_Code"].ToString();



            Session["SqlString"] = Request.QueryString["Requisitioncode"].ToString() + "," + Request.QueryString["Vessel_Code"].ToString() + "," + Request.QueryString["Document_Code"].ToString();

            BLL_PURC_Purchase objHistory = new BLL_PURC_Purchase();


            gvApprovalHistory.DataSource = objHistory.Get_Approver_History(Request.QueryString["Requisitioncode"].ToString().Trim());
            gvApprovalHistory.DataBind();


            rpAttachment.DataSource = objHistory.Purc_Get_Reqsn_Attachments(Request.QueryString["Requisitioncode"].ToString(), Convert.ToInt32(Request.QueryString["Vessel_Code"]));
            rpAttachment.DataBind();

        }



        if (HiddenbtnRetrive.Value == "1")
        {
            BindGridOnRetriveButtonClick();

        }

        Int16 isSentToSuppdt = BLL_PURC_Common.GET_Is_SentToSuppdt(Request.QueryString["Requisitioncode"].ToString());
        if (isSentToSuppdt == 0)
        {
            btnFinalizeEval.Visible = false;
            btnSaveEvaln.Visible = true;
            btnReq_Ord.Enabled = true;
            lbtnPurchaserRemark.Visible = false;
            btnApprovalHistory.Visible = false;
            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
            DataSet ds = objTechService.SelectBudgetCode();

            ddlBGTCodeToSuppdt.DataSource = ds.Tables[0];
            ddlBGTCodeToSuppdt.DataTextField = "Budget_Name";
            ddlBGTCodeToSuppdt.DataValueField = "Budget_Code";
            ddlBGTCodeToSuppdt.DataBind();
            if (rgdSupplierInfo.HeaderRow != null)
                ((LinkButton)(rgdSupplierInfo.HeaderRow.FindControl("btnReworkPurclink"))).Visible = false;

        }
        else
        {
            lbtnSendForApproval.Visible = false;
        }

        lblActionmsg.Text = "";
        lblActionmsg.Visible = false;

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


        }
        if (objUA.Edit == 0)
        {
            btnSaveEvaln.Visible = false;

        }
        if (objUA.Approve == 0)
        {
            btnFinalizeEval.Visible = false;
        }
        if (objUA.Delete == 0)
        {


        }


    }
    private void BindRequisitionInfo()
    {

        try
        {
            DataTable dtReqInfo = new DataTable();
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                dtReqInfo = objTechService.SelectRequistionToSupplier(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString());
                lblReqNo.Text = dtReqInfo.DefaultView[0]["REQUISITION_CODE"].ToString();
                lblVessel.Text = dtReqInfo.DefaultView[0]["Vessel_Name"].ToString();
                lblVesselCode.Value = dtReqInfo.DefaultView[0]["Vessel_Code"].ToString();
                lblCatalog.Text = dtReqInfo.DefaultView[0]["SYSTEM_Description"].ToString();
                lblToDate.Text = dtReqInfo.DefaultView[0]["requestion_Date"].ToString();
                lblTotalItem.Text = dtReqInfo.DefaultView[0]["TOTAL_ITEMS"].ToString();
                lblReqDate.Text = dtReqInfo.DefaultView[0]["RFQ_Date"].ToString();
                lblITEMSYSTEMCODE.Value = dtReqInfo.DefaultView[0]["ITEM_SYSTEM_CODE"].ToString();
                lblReqsnType.Text = Convert.ToString(dtReqInfo.DefaultView[0]["Reqsn_Type"]);
                //lbtnPurchaserRemark.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[" + dtReqInfo.DefaultView[0]["SentToSupdt_Remark"].ToString() + "]");
                lblReqNo.NavigateUrl = "RequisitionSummary.aspx?REQUISITION_CODE=" + Request.QueryString["Requisitioncode"].ToString() + "&Document_Code=" + Request.QueryString["Document_Code"].ToString() + "&Vessel_Code=" + Request.QueryString["Vessel_Code"].ToString() + "&Dept_Code=" + dtReqInfo.DefaultView[0]["DEPARTMENT"].ToString() + "&" + 1.ToString();
                ViewState["SavedReqsnType"] = Convert.ToString(dtReqInfo.DefaultView[0]["Reqsn_Type_Code"]);
                ViewState["Dept_Code"] = dtReqInfo.DefaultView[0]["DEPARTMENT"].ToString();

            }

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

    }

    private void BindQuatationSendBySupplier()
    {

        try
        {
            //int QuotRecCount = 0;
            DataTable dtsuppInfo = new DataTable();
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                dtsuppInfo = objTechService.SelectQuatationSendBySupplier(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString());
                rgdSupplierInfo.DataSource = dtsuppInfo;
                rgdSupplierInfo.DataBind();
                if (dtsuppInfo.Rows.Count > 0)
                    lblQuotDueDate.Text = dtsuppInfo.DefaultView[0]["Quotation_Due_Date"].ToString();

            }



            // for Not quotated Supplier are read Only 

            foreach (GridViewRow item in rgdSupplierInfo.Rows)
            {
                CheckBox chkQuaEvaluated = (CheckBox)(item.FindControl("chkQuaEvaluated") as CheckBox);
                Button btnRework = (Button)(item.FindControl("btnRework") as Button);
                if (item.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkbox = (CheckBox)item.FindControl("chkStatus");
                    if (!chkbox.Checked)
                    {
                        chkQuaEvaluated.Checked = false;
                        item.Enabled = false;
                        btnRework.Enabled = false;
                    }
                }
            }

            //Colouring of the row if re send this RFQ to supplier

            ColourForReSendforQtn();

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }

    }

    private void ColourForReSendforQtn()
    {
        foreach (GridViewRow dataItem in rgdSupplierInfo.Rows)
        {
            Button btnRework = (Button)(dataItem.FindControl("btnRework") as Button);

            if (rgdSupplierInfo.DataKeys[dataItem.RowIndex].Values["Req_Qut_Status"].ToString() == "R")//Req_Qut_Status
            {
                dataItem.BackColor = System.Drawing.Color.LightGreen;
                btnRework.Enabled = false;
            }
        }

    }

    protected void rgdEvalItems_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {

    }

    protected void chkQuaEvaluted_Click(object sender, EventArgs e)
    {

    }

    protected void Fill_Budget()
    {



        //DataTable dtBgtCode = BLL_PURC_Common.Get_BudgetCode_ByReqsnType(Convert.ToInt32(ddlReqsnType.SelectedValue));
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        DataSet dtBgtCode = objTechService.SelectBudgetCode(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Convert.ToInt32(ddlReqsnType.SelectedValue));

        if (dtBgtCode.Tables[0].Rows.Count > 0)
        {
            ddlBudgetCode.DataSource = dtBgtCode.Tables[0];
            ddlBudgetCode.DataTextField = "Budget_Name";
            ddlBudgetCode.DataValueField = "Linked_Budget_Code";
            ddlBudgetCode.DataBind();
            ddlBudgetCode.Items.Insert(0, new ListItem("Select", "0"));
            //Session["Budget_Code"] = "1";
            hdnBudgetCode.Value = "1";
            lblBudgetMsg.Text = "";
            btnRequestAmount.Visible = true; 
        }
        else
        {
            ddlBudgetCode.DataSource = dtBgtCode.Tables[1];
            ddlBudgetCode.DataTextField = "Budget_Name";
            ddlBudgetCode.DataValueField = "Budget_Code";
            ddlBudgetCode.DataBind();
            ddlBudgetCode.Items.Insert(0, new ListItem("Select", "0"));
            //Session["Budget_Code"] = "0";
            hdnBudgetCode.Value = "0";
            lblBudgetMsg.Text = "Budget limit not defined for this vessel.";
            btnRequestAmount.Visible = false; 
        }
        if (!string.IsNullOrWhiteSpace(Convert.ToString(ViewState["BGTCODE"])))
        {
            ddlBudgetCode.ClearSelection();
            ListItem libgt = ddlBudgetCode.Items.FindByValue(ViewState["BGTCODE"].ToString());
            if (libgt != null)
                libgt.Selected = true;
        }
        else
        {
            ddlBudgetCode.SelectedIndex = 0;
        }


        txtComment.Text = "";



        //divApprove.Visible = true;

    }
    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        try
        {

            ViewState["RetriveBtnPress"] = true;
            info.MergedColumns.Clear();
            info.StartColumns.Clear();
            info.Titles.Clear();



            bool SChecked = false;
            if (rgdSupplierInfo.Rows.Count > 0)
            {
                foreach (GridViewRow item in rgdSupplierInfo.Rows)
                {
                    if (((CheckBox)item.FindControl("chkQuaEvaluated")).Checked == true)
                    {
                        SChecked = true;
                        break;
                    }

                }

                if (SChecked == true)
                {
                    BindGridOnRetriveButtonClick();
                    String msgretv = String.Format("setTimeout(calculate,1000);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);

                    //disable on 20130809 - enable for provision limit validation
                    //if (lblITEMSYSTEMCODE.Value.Equals("PROVI"))
                    //{
                    //    String msgretvProvision = String.Format("HighlightItemsForProvisionLimit('" + Request.QueryString["Document_Code"] + "');");
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6vmsgretvProvision", msgretvProvision, true);
                    //}

                }
                else
                {
                    String msg = String.Format("alert('Please select Supplier from above list.');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

                }
            }

        }
        catch
        {

        }
        finally
        {

        }

    }

    private void BindGridOnRetriveButtonClick()
    {
        try
        {

            btnSaveEvaln.Enabled = true;
            btnFinalizeEval.Enabled = true;
            btnReq_Ord.Enabled = true;

            int suppSel = 0;
            string[] arrySuppName = new string[] { "", "", "", "", "", "", "", "", "", "" };
            string[] arrSuppNameInShort = new string[10];
            ViewState["suppliercode"] = "";
            ViewState["quotationcode"] = "";
            ViewState["suppcurrency"] = "";
            ViewState["Portname"] = "";
            string SuppQtnCodesItemTypes = "";
            hdfquotation_codes_compare.Value = "";
            hdfquotation_codes_RowNum_compare.Value = "";


            if (rgdQuatationInfo.Columns.Count > 12)
            {
                //Remove all the programatically generated colomn from grid and 
                for (int i = rgdQuatationInfo.Columns.Count - 1; i >= 12; i--)
                {
                    rgdQuatationInfo.Columns.RemoveAt(i);
                }
            }
            if (Request.QueryString["Requisitioncode"].ToString().Substring(0, 2) == "ST" || Request.QueryString["Requisitioncode"].ToString().Substring(0, 3) == "OST")
            {
                rgdQuatationInfo.Columns[3].Visible = false;
            }
            StringBuilder strSql = new StringBuilder();
            StringBuilder strTables = new StringBuilder();
            StringBuilder strCTP = new StringBuilder();
            decimal MinValues = 0;
            int col_id = 11;
            int sel_Id = 0;
            string supplist = "";
            //  HiddenChepSupp.Value = 0;
            // strSql.Append("select distinct a.ITEM_SERIAL_NO,'False' chechstatus, a.QUOTATION_CODE,a.ITEM_REF_CODE, a.ITEM_SHORT_DESC,a.ITEM_FULL_DESC,a.QUOTED_UNIT_ID,a.QUOTED_QTY");
            strSql.Append(@"select distinct M.ITEM_SERIAL_NO
                                        ,'False' chechstatus
                                        , a.QUOTATION_CODE
                                        ,a.ITEM_REF_CODE
                                        , a.ITEM_SHORT_DESC
                                        ,a.ITEM_FULL_DESC
                                        ,a.QUOTED_QTY
                                        ,case when item.Drawing_Number='0' then '' else item.Drawing_Number end Drawing_Number
                                        ,Item.Part_Number
                                        ,isnull(Item.Long_Description,'') Long_Description
                                        , M.ITEM_COMMENT 
                                        ,M.ORDER_UNIT_ID
                                        ,item.Unit_and_Packings
                                        ,M.ROB_Qty
                                        ,M.REQUESTED_QTY
                                        ,M.ORDER_QTY
                                        ,isnull(M.ITEM_INTERN_REF,0) as ITEM_INTERN_REF
                                        ,a.Vessel_Code
                                        ,PURC_LIB_SUBSYSTEMS.Subsystem_Description
                                        ,isnull(notdeldItems.ITEM_REF_CODE,'0') as NotDeliverd
                                        ,isnull((select active_status from purc_dtl_reqsn where order_code=m.order_code and line_type='O'),0) as Active_PO   
                                        ,item.Critical_Flag 
                                        ,'" + lblCatalog.Text.Trim().Replace("'", " ") + "' as Catalogue, '" + lblReqNo.Text.Trim() + "' as Reqsnno");

            strTables.Append(@"  
                                from  (select * from  PURC_Dtl_Quoted_Prices where DOCUMENT_CODE ='" + Request.QueryString["Document_Code"].ToString() + @"')  a 
                                inner  join PURC_Lib_Items item 
                                    On item.Item_Intern_Ref=A.Item_Ref_Code 
                                inner join PURC_Dtl_Supply_Items M 
                                    on M.item_ref_code=A.item_ref_code and M.Document_Code=a.Document_Code and a.Vessel_Code=M.Vessel_Code 
                                inner join PURC_LIB_SUBSYSTEMS 
                                    on PURC_LIB_SUBSYSTEMS.Subsystem_Code  =M.ITEM_SUBSYSTEM_CODE and PURC_LIB_SUBSYSTEMS.System_Code=M.ITEM_SYSTEM_CODE 
                                 left join  ( select  ITEM_REF_CODE from  PURC_DTL_SUPPLY_ITEMS inner join PURC_DTL_REQSN on PURC_DTL_SUPPLY_ITEMS.ORDER_CODE=PURC_DTL_REQSN.ORDER_CODE    where PURC_DTL_SUPPLY_ITEMS.DOCUMENT_CODE<>'" + Request.QueryString["Document_Code"].ToString() + "' and PURC_DTL_SUPPLY_ITEMS.DELIVERD_QTY is null and PURC_DTL_SUPPLY_ITEMS.Vessel_Code=" + Request.QueryString["Vessel_Code"].ToString() + @"  and PURC_DTL_REQSN.Active_Status=1 and PURC_DTL_REQSN.LINE_TYPE='O') notdeldItems
                                    on notdeldItems.ITEM_REF_CODE=m.ITEM_REF_CODE     
                                    ");

            string BackColor = "";
            int suppCountBG = 2;
            foreach (GridViewRow dataItem in rgdSupplierInfo.Rows)
            {
                string str = "";
                if (UDFLib.ConvertToDecimal(rgdSupplierInfo.DataKeys[dataItem.RowIndex].Values["ORDER_AMOUNT"].ToString()) < 1)
                {
                    TextBox txtgrdItemReqQty = (TextBox)(dataItem.FindControl("txtgrdItemReqstdQty") as TextBox);
                    CheckBox chk = (CheckBox)(dataItem.FindControl("chkQuaEvaluated") as CheckBox);

                    string PortName = rgdSupplierInfo.DataKeys[dataItem.RowIndex].Values["PortName"].ToString();
                    string suppcurrency = rgdSupplierInfo.DataKeys[dataItem.RowIndex].Values["Currency"].ToString();
                    if ((chk.Checked))
                    {
                        if (suppCountBG % 2 == 0)// assigne the different color to suppliers 
                        {
                            BackColor = "QtnEval-ItemStyle-css";

                        }
                        else
                        {
                            BackColor = "QtnEval-AltItemStyle-css";
                        }
                        suppCountBG++;

                        col_id = col_id + Convert.ToInt32(ViewState["ColumnCount_Supp"].ToString());

                        string supplier_code = rgdSupplierInfo.DataKeys[dataItem.RowIndex].Values["SUPPLIER"].ToString();
                        string QUOTATION_CODE = rgdSupplierInfo.DataKeys[dataItem.RowIndex].Values["QUOTATION_CODE"].ToString();
                        string Col_supp = supplier_code.Replace('-', '_') + QUOTATION_CODE.Replace('-', '_');

                        if (hdfquotation_codes_compare.Value.Length > 0)
                            hdfquotation_codes_compare.Value += ",";

                        hdfquotation_codes_compare.Value += Col_supp;

                        if (hdfquotation_codes_RowNum_compare.Value.Length > 0)
                            hdfquotation_codes_RowNum_compare.Value += ",";

                        hdfquotation_codes_RowNum_compare.Value += Col_supp + "!" + dataItem.RowIndex.ToString();


                        string Col_supp_Alias = "Supp" + supplier_code.Trim() + dataItem.RowIndex.ToString();
                        string Col_supp_where = supplier_code;

                        string Col_supp_Short = (dataItem.FindControl("lblsupplier_shortname") as Label).Text;
                        string exchange_rate = (dataItem.FindControl("hdfEXCHANGE_RATE") as HiddenField).Value;

                        string strColSupp = "";
                        if (Col_supp_Short.Length > Convert.ToInt32(ViewState["ColumnCount_Supp"].ToString()))
                        {
                            for (int i = 0; i < Convert.ToInt32(ViewState["ColumnCount_Supp"].ToString()); i++)
                            {
                                strColSupp = strColSupp + Col_supp_Short[i];
                            }
                        }
                        else
                        {
                            strColSupp = Col_supp_Short;
                        }



                        //strSql.Append(",");
                        strTables.Append(@" 
                                    
                            inner Join ");

                        str = @"(select supl.ITEM_REF_CODE
                                ,( QUOTED_RATE * " + exchange_rate + ") " + Col_supp_Alias + @"_Rate
                                ,QUOTED_PRICE " + Col_supp_Alias + @"_Price
                                ,QUOTED_DISCOUNT " + Col_supp_Alias + @"_Discount
                                , QUOTATION_REMARKS " + Col_supp_Alias + @"_Remark
                                , case when isnull(EVALUATION_OPTION,0)=1 then 'True' else 'False' end as " + Col_supp_Alias + @"_Status
                                ,QUOTATION_CODE
                                ,(((cast(QUOTED_RATE*" + exchange_rate + " as decimal(18,2))*supl.ORDER_QTY)-(cast(QUOTED_RATE*" + exchange_rate + " as decimal(18,2))*supl.ORDER_QTY*cast(QUOTED_DISCOUNT as decimal(18,2))/100))) " + Col_supp_Alias + @"_Amount
                                , isnull(Lead_Time,'') " + Col_supp_Alias + @"_Lead_Time
                                , [Description]" + Col_supp_Alias + @"_ItemType
                                ,PURC_Dtl_Quoted_Prices.QUOTED_RATE  
                                , PURC_Dtl_Quoted_Prices.QTN_Item_ID  
                                                          
                                from PURC_Dtl_Quoted_Prices 
                                    inner join PURC_DTL_SUPPLY_ITEMS supl 
                                          on supl.DOCUMENT_CODE=PURC_Dtl_Quoted_Prices.DOCUMENT_CODE and supl.ITEM_REF_CODE=PURC_Dtl_Quoted_Prices.ITEM_REF_CODE  
                                    inner join PURC_LIB_SYSTEM_PARAMETERS 
                                           on Code=Item_Type
                                    
                                    where supplier_code='" + Col_supp_where
                            + "' and QUOTATION_CODE ='" + QUOTATION_CODE + "')  " + Col_supp_Alias + " on " + Col_supp_Alias + ".ITEM_REF_CODE = a.ITEM_REF_CODE ";
                        if (col_id == 18)
                        {
                            str += " and " + Col_supp_Alias + ".QUOTATION_CODE=a.QUOTATION_CODE ";
                        }


                        strTables.Append(str);
                        strSql.Append(Environment.NewLine +" ,"+Col_supp_Alias + "." + "ITEM_REF_CODE");
                        strSql.Append(Environment.NewLine + " ," + Col_supp_Alias + "." + Col_supp_Alias + "_Rate ");
                        strSql.Append(Environment.NewLine + " ," + Col_supp_Alias + "." + Col_supp_Alias + "_Price ");
                        strSql.Append(Environment.NewLine + " ," + Col_supp_Alias + "." + Col_supp_Alias + "_Discount ");
                        strSql.Append(Environment.NewLine + " ," + Col_supp_Alias + "." + Col_supp_Alias + "_Remark ");
                        strSql.Append(Environment.NewLine + " ," + Col_supp_Alias + "." + Col_supp_Alias + "_Status ");
                        strSql.Append(Environment.NewLine + " ," + Col_supp_Alias + "." + "QUOTATION_CODE ");
                        strSql.Append(Environment.NewLine + " ," + Col_supp_Alias + "." + Col_supp_Alias + "_Amount ");
                        strSql.Append(Environment.NewLine + " ," + Col_supp_Alias + "." + Col_supp_Alias + "_Lead_Time ");
                        strSql.Append(Environment.NewLine + " ," + Col_supp_Alias + "." + Col_supp_Alias + "_ItemType ");




                        strSql.Append(Environment.NewLine + ",CASE WHEN CAST(ISNULL(" + Col_supp_Alias + ".QUOTED_RATE,0) AS DECIMAL(18,2))!= CAST(ISNULL(" + Col_supp_Alias + "CTP.Rate,ISNULL(" + Col_supp_Alias + ".QUOTED_RATE,0)) AS DECIMAL(18,2)) THEN 1 ELSE 0 END  " + Col_supp_Alias + "_CTP_rate_change");
                        strSql.Append(Environment.NewLine + ", cast( " + exchange_rate + " *  " + Col_supp_Alias + "CTP.rate as decimal(18,2))  " + Col_supp_Alias + @"_CTP_rate");

                        strCTP.Append(Environment.NewLine + " LEFT JOIN  PURC_DTL_CTP_Quotation_Item as " + Col_supp_Alias + "CTP ON " + Col_supp_Alias + "CTP.QTN_Item_ID=" + Col_supp_Alias + ".QTN_Item_ID");


                        TemplateField templateColumn;


                        templateColumn = new TemplateField();
                        templateColumn.HeaderTemplate = new DataGridTemplateLabel(ListItemType.Header, Col_supp, "Unit Price", "");
                        templateColumn.ItemTemplate = new DataGridTemplateLabel(ListItemType.Item, Col_supp + "_UnitPrice", Col_supp, Col_supp_Alias + "_Rate");
                        templateColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                        templateColumn.ItemStyle.CssClass = BackColor;
                        templateColumn.HeaderStyle.Width = 70;
                        templateColumn.ItemStyle.Width = 70;
                        rgdQuatationInfo.Columns.Add(templateColumn);

                        templateColumn = new TemplateField();
                        templateColumn.HeaderTemplate = new DataGridTemplateLabel(ListItemType.Header, Col_supp, "Discount", "");
                        templateColumn.ItemTemplate = new DataGridTemplateLabel(ListItemType.Item, Col_supp + "_Discount", Col_supp, Col_supp_Alias + "_Discount");
                        templateColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                        templateColumn.ItemStyle.CssClass = BackColor;
                        templateColumn.HeaderStyle.Width = 70;
                        templateColumn.ItemStyle.Width = 70;
                        rgdQuatationInfo.Columns.Add(templateColumn);

                        templateColumn = new TemplateField();
                        templateColumn.HeaderTemplate = new DataGridTemplateLabel(ListItemType.Header, Col_supp, "Amount", "");
                        templateColumn.ItemTemplate = new DataGridTemplateLabel(ListItemType.Item, Col_supp + "_Amount", Col_supp, Col_supp_Alias + "_Amount");
                        templateColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                        templateColumn.ItemStyle.CssClass = BackColor;
                        templateColumn.HeaderStyle.Width = 70;
                        templateColumn.ItemStyle.Width = 70;
                        rgdQuatationInfo.Columns.Add(templateColumn);

                        templateColumn = new TemplateField();
                        templateColumn.HeaderTemplate = new DataGridTemplateLabel(ListItemType.Header, Col_supp, "Lead days", "");
                        templateColumn.ItemTemplate = new DataGridTemplateLabel(ListItemType.Item, Col_supp + "_Lead_Time", Col_supp, Col_supp_Alias + "_Lead_Time");
                        templateColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        templateColumn.ItemStyle.CssClass = BackColor;
                        templateColumn.HeaderStyle.Width = 70;
                        templateColumn.ItemStyle.Width = 70;
                        rgdQuatationInfo.Columns.Add(templateColumn);

                        TemplateField templateColumnItemType = new TemplateField();
                        templateColumnItemType.HeaderText = "ItemType";
                        templateColumnItemType.ItemTemplate = new DataGridTemplateDropDown(ListItemType.Item, Col_supp, Col_supp + "_ItemType");
                        templateColumnItemType.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        templateColumnItemType.ItemStyle.CssClass = BackColor;
                        templateColumnItemType.HeaderStyle.Width = 70;
                        templateColumnItemType.ItemStyle.Width = 70;
                        rgdQuatationInfo.Columns.Add(templateColumnItemType);

                        TemplateField templateColumnRemark = new TemplateField();
                        templateColumnRemark.HeaderText = "Remark";
                        templateColumnRemark.ItemTemplate = new DataGridTemplateImage(ListItemType.Item, Col_supp, Col_supp_Alias + "_Remark", Col_supp + "_Remark");
                        templateColumnRemark.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        templateColumnRemark.ItemStyle.CssClass = BackColor;
                        templateColumnRemark.ItemStyle.Width = 70;
                        templateColumnRemark.HeaderStyle.Width = 70;
                        rgdQuatationInfo.Columns.Add(templateColumnRemark);

                        templateColumn = new TemplateField();
                        templateColumn.HeaderTemplate = new DataGridTempla(ListItemType.Header, Col_supp, "", "", "");
                        templateColumn.ItemTemplate = new DataGridTempla(ListItemType.Item, Col_supp, Col_supp, Col_supp_Alias + "_Status", "Active_PO");
                        templateColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        templateColumn.ItemStyle.CssClass = BackColor;
                        templateColumn.HeaderStyle.Width = 70;
                        templateColumn.ItemStyle.Width = 70;
                        rgdQuatationInfo.Columns.Add(templateColumn);


                        ViewState["Col_supp"] = Col_supp;
                        ViewState["Portname"] += PortName + ",";
                        ViewState["suppcurrency"] += suppcurrency + ",";
                        ViewState["suppliercode"] = ViewState["suppliercode"] + Col_supp_Alias + ",";
                        ViewState["quotationcode"] = ViewState["quotationcode"] + QUOTATION_CODE + ",";
                        SuppQtnCodesItemTypes += " select '" + QUOTATION_CODE + "'  ";

                        arrySuppName[suppSel] = Col_supp;
                        supplist = Col_supp + "," + supplist;
                        arrSuppNameInShort[suppSel] = strColSupp;


                        suppSel += 1;
                        //if (MinValues == 0 || MinValues > Convert.ToDecimal(dataItem["Supp_Tot_Amt"].Text.ToString()))
                        //{
                        //    //HiddenChepSupp.Value = Col_supp + "_chk";
                        //    HiddenChepSupp.Value = Col_supp;
                        //    MinValues = Convert.ToDecimal(dataItem["Supp_Tot_Amt"].Text.ToString());
                        //    sel_Id = col_id;
                        //}

                    }
                }
            }

            strSql.Append(" ");
            strSql.Append(strTables.ToString() +"  "+ strCTP.ToString());
            Session["SubQuerry"] = strSql.ToString();
            strSql.Append(Environment.NewLine + " where  --a.Document_code ='" + Request.QueryString["Document_Code"].ToString() + @"' and 
         a.active_status=1 order by M.ITEM_SERIAL_NO");

            SuppQtnCodesItemTypes = SuppQtnCodesItemTypes.Remove(SuppQtnCodesItemTypes.Length - 1, 1);
            dtItemsTypes = BLL_PURC_Common.GET_ItemTypeAll(SuppQtnCodesItemTypes);

            DataTable dt = new DataTable();
            // DataTable dtQuatationInfo = new DataTable();
            ViewState["SuppSelforEval"] = suppSel;
            ViewState["supplierList"] = arrySuppName;
            ViewState["supplierListInShort"] = arrSuppNameInShort;


            count = 0;



            // optEvalCSupp.Attributes.Add("Onclick", "javascript:check_changesOnUI('CalculateByEvalOpt('0','0')','0');");
            //optEvalCItm.Attributes.Add("Onclick", "check_changesOnUI('CalculateByEvalOpt(1, 0)','0');");

            Session["supplist"] = supplist.ToString();

            rgdQuatationInfo.DataSource = null;
            rgdQuatationInfo.DataBind();
            TechnicalBAL objtechBAL = new TechnicalBAL();
            string FinalQuery = strSql.ToString();
            dtQuatationInfo = objtechBAL.GetTable(FinalQuery);
            rgdQuatationInfo.DataSource = dtQuatationInfo;
            rgdQuatationInfo.DataBind();
            Session["QuatationInfo"] = dtQuatationInfo;
            Session["GeneratedQuery"] = FinalQuery;

            foreach (GridViewRow Item in rgdQuatationInfo.Rows)
            {
                int suppCount = (int)ViewState["SuppSelforEval"];
                string[] arrSupp = (string[])ViewState["supplierList"];

                int j = 11;
                for (int i = 0; i < suppCount; i++)
                {
                    j = j + Convert.ToInt32(ViewState["ColumnCount_Supp"].ToString());
                    //Find the checkbox control in header and add an attribute
                    ((CheckBox)Item.FindControl(arrSupp[i].ToString())).Attributes.Add("onclick", "javascript:SelectRowsClick('" +
                           ((CheckBox)Item.FindControl(arrSupp[i].ToString())).ClientID + "'," + count + "," + j + ")");

                }
                count++;

                Label longDescpt = (Label)Item.FindControl("lblLongDesc");
                if (longDescpt.ToolTip == "1")
                {
                    ((HyperLink)Item.FindControl("lblItemDesc")).CssClass = "NewItem";
                    Item.Cells[8].BackColor = System.Drawing.Color.Yellow;

                }

            }

            // ViewState["EvaluateTable"] = dtQuatationInfo;
            int column = 12;
            int PortCount = 0;
            foreach (string supp in arrSuppNameInShort)
            {


                if (supp != "" && supp != null)
                {

                    info.AddMergedColumns(new int[] { column, column + 1, column + 2, column + 3, column + 4, column + 5, column + 6 }, supp + "  (Port : " + ViewState["Portname"].ToString().Split(new char[] { ',' })[PortCount].ToString() + " ,Quoted Currency : " + ViewState["suppcurrency"].ToString().Split(new char[] { ',' })[PortCount].ToString() + ")");
                    column += Convert.ToInt32(ViewState["ColumnCount_Supp"].ToString());
                }
                PortCount++;
            }

        }
        catch (Exception ex)
        {
            lblActionmsg.Visible = true;
            lblActionmsg.Text = ex.Message;
        }

        UserAccessValidation();


    }

    protected RadGrid grid;


    public void InstantiateIn(Control container)
    {
        boolValue = new CheckBox();
        boolValue.ID = "boolValue";
        boolValue.AutoPostBack = true;

        //boolValue.CheckedChanged += new EventHandler(boolValue_DataBinding);
        container.Controls.Add(boolValue);
    }

    public void boolValue_DataBinding(object sender, EventArgs e)
    {

        //        int i = 0;

        CheckBox cBox = (CheckBox)sender;
        GridDataItem container = (GridDataItem)cBox.NamingContainer;

    }

    public DataTable GetDataTable(string query)
    {
        String ConnString = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        SqlConnection conn = new SqlConnection(ConnString);
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = new SqlCommand(query, conn);

        DataTable myDataTable = new DataTable();

        conn.Open();
        try
        {
            adapter.Fill(myDataTable);
        }
        finally
        {
            conn.Close();
        }

        return myDataTable;
    }

    protected void rgdQuatationInfo_ItemDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {

            string[] arrSupp = (string[])ViewState["supplierList"];
            string[] arrSuppCode = ViewState["suppliercode"].ToString().Split(',');
            string[] arrQtnCode = ViewState["quotationcode"].ToString().Split(',');
            if (e.Row.RowType == DataControlRowType.Header)
            {

                int suppCount = (int)ViewState["SuppSelforEval"];

                int j = 11;
                for (int i = 0; i < suppCount; i++)
                {
                    j = j + Convert.ToInt32(ViewState["ColumnCount_Supp"].ToString());
                    //Find the checkbox control in header and add an attribute
                    ((CheckBox)e.Row.FindControl(arrSupp[i].ToString())).Attributes.Add("onclick", "javascript:SelectAll('" +
                            ((CheckBox)e.Row.FindControl(arrSupp[i].ToString())).ClientID + "'," + j + ")");

                }
                //count++;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow item = e.Row;
                int RemarkID = 29;
                int RateID = 12;
                int suppno = 0;
                DataTable dtItemType = new DataTable();
                DataTable dtfilteredItem = new DataTable();
                DataTable dtSource = new DataTable();
                foreach (string supp in arrSupp)
                {
                    if (supp != "" && supp != null)
                    {
                        ///remark
                        //string strremark = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[RemarkID].ToString(); ;
                        //((Image)(e.Row.FindControl(supp + "_img"))).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[" + strremark + "]");
                        //((Image)(e.Row.FindControl(supp + "_img"))).AlternateText = strremark;
                        //if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[RemarkID].ToString() == "")
                        //{
                        //    ((Image)(e.Row.FindControl(supp + "_img"))).ImageUrl = "~/PURCHASE/Image/remarknone.png";

                        //}
                        ///item type
                        dtSource = dtQuatationInfo;
                        string columnName = arrSuppCode[suppno] + "_ItemType";

                        dtSource.DefaultView.RowFilter = "ITEM_REF_CODE='" + rgdQuatationInfo.DataKeys[item.RowIndex].Values["ITEM_REF_CODE"].ToString() + "'";

                        string lblItemType = "";
                        if (dtSource.DefaultView.ToTable().Rows.Count > 0)
                        {

                            lblItemType = dtSource.DefaultView.ToTable().Rows[0][columnName].ToString();
                        }
                        dtSource.DefaultView.RowFilter = "";
                        dtItemType = dtItemsTypes;
                        // DataTable dtItemType = BLL_PURC_Common.GET_ItemType(arrQtnCode[suppno], item["ITEM_REF_CODE"].Text);//get from public table 
                        dtItemType.DefaultView.RowFilter = "Quotation_Code='" + arrQtnCode[suppno] + "' and  Item_Ref_Code='" + rgdQuatationInfo.DataKeys[item.RowIndex].Values["ITEM_REF_CODE"].ToString() + "'";

                        dtfilteredItem = dtItemType.DefaultView.ToTable();
                        int dtItemTypeCount = dtfilteredItem.Rows.Count;
                        DropDownList ddlItemType = ((DropDownList)e.Row.FindControl(supp + "_ddl"));
                        if (dtItemTypeCount > 0)
                        {
                            ddlItemType.DataTextField = "Description";
                            ddlItemType.DataValueField = "Quoted_Rate_Code";
                            ddlItemType.DataSource = dtfilteredItem;
                            ddlItemType.DataBind();
                            // if item type more than one then only java sccript method  and item selected are required
                            if (dtItemTypeCount != 1)
                            {
                                string txtORqtyClientID = ((TextBox)e.Row.FindControl("txtORqty")).ClientID;
                                ddlItemType.Attributes.Add("onChange", "ItemTypeChanged('" + RateID + "',this," + txtORqtyClientID + ")");

                                if (lblItemType != "")
                                {
                                    ddlItemType.Items.FindByText(lblItemType).Selected = true;
                                }
                            }
                            else
                            {
                                ddlItemType.Enabled = false;
                            }

                        }

                        else
                        {
                            ddlItemType.Enabled = false;
                        }


                    }
                    RemarkID += 10;
                    RateID += 7;
                    suppno++;
                }

                if (((HyperLink)(e.Row.FindControl("lblItemDesc"))).Text.Length > 30)
                {

                    ((HyperLink)(e.Row.FindControl("lblItemDesc"))).Text = ((HyperLink)(e.Row.FindControl("lblItemDesc"))).Text.Substring(0, 35) + "..";
                }

                if (DataBinder.Eval(e.Row.DataItem, "Critical_Flag").ToString() == "1")
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Wheat;
                }



                Label longDescpt = (Label)e.Row.FindControl("lblLongDesc");
                // ((HyperLink)(e.Row.FindControl("lblItemDesc"))).Attributes.Add("onmousemove", "js_ShowToolTip('Short Description: " + ((Label)e.Row.FindControl("lblshortDesc")).Text + "<hr>" + "Long Description: " + longDescpt.Text + "<hr>" + "Sub Catalogue: " + ((Label)e.Row.FindControl("lblshortDesc")).ToolTip + " <hr>Comment: " + ((Label)e.Row.FindControl("lblComments")).Text + "Drw no.:" + e.Row.Cells[6].Text + " Part no.:" + e.Row.Cells[7].Text + "',event,this)");
                ((HyperLink)(e.Row.FindControl("lblItemDesc"))).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + "Sub Catalogue: " + ((Label)e.Row.FindControl("lblshortDesc")).ToolTip + "<hr>Short Description: " + ((Label)e.Row.FindControl("lblshortDesc")).Text + "<hr>" + "Long Description: " + longDescpt.Text + " <hr>Comment: " + ((Label)e.Row.FindControl("lblComments")).Text + "] body=[" + "Drw no.:" + e.Row.Cells[4].Text + " Part no.:" + e.Row.Cells[5].Text + "]");



            }





        }
        catch //(Exception ex)
        {

        }


    }



    protected void rgdSupplierInfo_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        BindQuatationSendBySupplier();

    }



    protected void btnFinalizeEval_Click(object sender, EventArgs e)
    {
        try
        {
            info.MergedColumns.Clear();
            info.StartColumns.Clear();
            info.Titles.Clear();
            lblErrorMsg.Text = "";
            decimal maxQuotaed_Amount = 0;
            hdfMaxQuotedAmount.Value = "0";
            hdfOrderAmounts.Value = "0";

            // check the validity of suppliers

            string SElectedSupplierToApprove = hdfSupplierBeingApproved.Value;
            string suppliers = "";
            foreach (GridViewRow item in rgdSupplierInfo.Rows)
            {
                string supplier_code = rgdSupplierInfo.DataKeys[item.RowIndex].Values["SUPPLIER"].ToString();
                decimal Supp_Tot_Amt = UDFLib.ConvertToDecimal(rgdSupplierInfo.DataKeys[item.RowIndex].Values["Supp_Tot_Amt"].ToString());
                suppliers += " select '" + supplier_code + "' union"; //get the ASL_Status_Valid_till date for all supplier 

                if (SElectedSupplierToApprove.Contains(supplier_code)) // get the max quotation amount among quoted suppliers and those are under comparison section
                {
                    if (Supp_Tot_Amt > maxQuotaed_Amount)
                    {
                        maxQuotaed_Amount = Supp_Tot_Amt;
                    }
                }

                //approved amount will includes existing POs's amount and current quotation's final amount(based on items selection) 
                if (Supp_Tot_Amt > 0)
                {
                    hdfOrderAmounts.Value = (UDFLib.ConvertToDecimal(hdfOrderAmounts.Value) + UDFLib.ConvertToDecimal(rgdSupplierInfo.DataKeys[item.RowIndex].Values["ORDER_AMOUNT"].ToString())).ToString();
                }


            }
            hdfMaxQuotedAmount.Value = maxQuotaed_Amount.ToString();
            suppliers += " select '0'";
            DataTable dtSuppDate = BLL_PURC_Common.Get_Supplier_ValidDate(suppliers);
            string supplierNameNotValid = "";
            if (dtSuppDate.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSuppDate.Rows)
                {
                    if (Convert.ToDateTime(dr["ASL_Status_Valid_till"]) < DateTime.Now)
                    {
                        if (SElectedSupplierToApprove.Contains(dr["SUPPLIER"].ToString()))// if supplier is in comaparison section 
                        {
                            supplierNameNotValid += dr["Full_NAME"].ToString() + ", ";
                        }
                    }
                }
            }

            if (supplierNameNotValid.Length > 0)
            {

                String msg = String.Format("alert('Supplier(s) Expired:  " + supplierNameNotValid + "');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg45", msg, true);
                BindGridOnRetriveButtonClick();

            }
            else
            {

                string FinalQuery = HiddenQuery.Value;

                if (FinalQuery != "")
                {

                    TechnicalBAL objtechBAL = new TechnicalBAL();

                    int retVal = objtechBAL.ExecuteQuery(FinalQuery);
                    ViewState["BGTCODE"] = BLL_PURC_Common.Get_BGTCode_Reqsn(Request.QueryString["Requisitioncode"].ToString());

                    ddlReqsnType.ClearSelection();
                    ListItem listreqstype = ddlReqsnType.Items.FindByValue(Convert.ToString(ViewState["SavedReqsnType"]));
                    if (listreqstype != null)
                        listreqstype.Selected = true;


                    Fill_Budget();


                    BindGridOnRetriveButtonClick();
                    String msgretv = String.Format("setTimeout(calculate,1000);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinal", msgretv, true);

                    if (isProvisionLimitExceeding())
                    {
                        String msgmodal = String.Format("showModal('divReasonForProvisionLimit');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msReasonLimit", msgmodal, true);
                    }
                    else
                    {

                        String msgmodal = String.Format("showModal('divApprove');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodal", msgmodal, true);
                       
                    }


                }
                else
                {
                    String msg = String.Format("alert('Please select items to finalize the Quotation.');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

                }
            }

        }
        catch (Exception ex)
        {
            lblErrorMsg.Text = ex.Message + ex.StackTrace;
        }

    }

    protected void btnReasonForProvisionLimit_Click(object s, EventArgs e)
    {
        BLL_PURC_Common.INS_Remarks(Request.QueryString["Document_Code"], Convert.ToInt32(Session["userid"].ToString()), "Reason for approving provision's items  :  " + txtReasonForProvisionLimit.Text.Trim(), 303);

        BindGridOnRetriveButtonClick();
        String msgretv = String.Format("setTimeout(calculate,1000);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinal", msgretv, true);

        String msgmodal = String.Format("showModal('divApprove');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodal", msgmodal, true);
    }

    protected void btnSaveEvaln_Click(object s, EventArgs e)
    {
        info.MergedColumns.Clear();
        info.StartColumns.Clear();
        info.Titles.Clear();
        lblErrorMsg.Text = "";
        string FinalQuery = HiddenQuery.Value;

        if (FinalQuery != "")
        {

            TechnicalBAL objtechBAL = new TechnicalBAL();
            int retVal = objtechBAL.ExecuteQuery(FinalQuery);
            BindGridOnRetriveButtonClick();
            String msgretv = String.Format("setTimeout(calculate,1000);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonsave", msgretv, true);
            lblActionmsg.Visible = true;
            lblActionmsg.Text = "Saved successfully.";



        }
        else
        {
            String msg = String.Format("alert('Please select items to Save the Quotation.');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

            // lblErrorMsg.Text = "Please Select Items to finalize the Quotation.";
        }



    }

    private void SavePurchasedOrder(DataTable dtReqInfo)
    {

        string strSupplier = dtReqInfo.Rows[0]["SUPPLIER"].ToString();

        if (strSupplier.Length > 0)
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                //qtnbased
                // at the time of approval sentto suppdt will become 1 so that it will not come under RFQ/quotation stage and
                // on roll back of a PO this column will be set to 0 (zero) for that quotation only

                int RetVal = objTechService.InsertDataForPO(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), strSupplier, dtReqInfo.Rows[0]["QUOTATION_CODE"].ToString(), Session["userid"].ToString(), Request.QueryString["Document_Code"].ToString());

            }
        }




    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            info.MergedColumns.Clear();
            info.StartColumns.Clear();
            info.Titles.Clear();

            BindGridOnRetriveButtonClick();

            GridViewExportUtil objex = new GridViewExportUtil();
            objex.ExportGridviewToExcel(rgdQuatationInfo);

        }
        catch (Exception ex)
        {

        }
        finally
        {


        }
    }

    protected void ddlReqsnType_SelectedIndexChanged(object s, EventArgs e)
    {
        BindGridOnRetriveButtonClick();
        Fill_Budget();

    }


    protected void onRework(object source, CommandEventArgs e)
    {
        GridViewRow gridrow = (GridViewRow)((Button)source).Parent.Parent;
        hdfQTNCode.Value = rgdSupplierInfo.DataKeys[gridrow.RowIndex].Values["QUOTATION_CODE"].ToString();
        hdfSuppCode.Value = e.CommandArgument.ToString();
        //dvReworktoSuppler.Visible = true;

        info.MergedColumns.Clear();
        info.StartColumns.Clear();
        info.Titles.Clear();

        String msg1 = String.Format("DivReworkSuppShow();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg1, true);

    }

    protected void SendEmailToSupplier(DataSet dsEmailInfo, string strSuppCode, string strServerIPAdd, string Attachment)
    {
        try
        {
            string UploadFilePath = ConfigurationManager.AppSettings["PURC_UPLOAD_PATH"];
            String URL = "";
            DataTable dtSuppDetails = new DataTable();
            string strFormatSubject = "", strFormatBody = "", sToEmailAddress = "";

            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            DataTable dtUser = objUser.Get_UserDetails(Convert.ToInt32(Session["USERID"]));
            string strEmailAddCc = dtUser.Rows[0]["MailID"].ToString();

            BLL_Crew_CrewDetails objMail = new BLL_Crew_CrewDetails();
            int MailID = 0;

            FormateEmailOnRework(dsEmailInfo, false, strServerIPAdd, out sToEmailAddress, out strFormatSubject, out strFormatBody);

            MailID = objMail.Send_CrewNotification(0, 0, 0, 0, sToEmailAddress, strEmailAddCc, "", strFormatSubject, strFormatBody, "", "MAIL", "", UDFLib.ConvertToInteger(Session["USERID"].ToString()), "DRAFT");
            URL = String.Format("window.open('../crew/EmailEditor.aspx?ID=+" + MailID.ToString() + @"&FILEPATH=" + UploadFilePath + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "k" + MailID.ToString(), URL, true);

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }

    }
    /// <summary>
    /// Add <br> fro break line in Email signature.Mobile No should come in new Line not in same line with address.
    /// Modified by Alok
    /// </summary>
    /// <param name="dsEmailInfo"></param>
    /// <param name="IsExcelBaseRFQ"></param>
    /// <param name="strServerIPAdd"></param>
    /// <param name="sEmailAddress"></param>
    /// <param name="strSubject"></param>
    /// <param name="strBody"></param>
    protected void FormateEmailOnRework(DataSet dsEmailInfo, bool IsExcelBaseRFQ, string strServerIPAdd, out string sEmailAddress, out string strSubject, out string strBody)
    {
        BLL_PURC_Purchase objpurch = new BLL_PURC_Purchase();
        string Legalterm = objpurch.Get_LegalTerm(282);
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        DataTable dtUser = objUser.Get_UserDetails(Convert.ToInt32(Session["USERID"]));

        StringBuilder sbBody = new StringBuilder();
        sEmailAddress = "";
        strSubject = "";
        strBody = "";
        //Need filter Line type ='Q' because it's in RFQ stage.
        dsEmailInfo.Tables[0].DefaultView.RowFilter = "Line_type ='Q'";
        // If the supplier is not  Listed


        string WebQuotSite = ConfigurationManager.AppSettings["WebQuotSite"].ToString();
        strBody = @"Dear " + dsEmailInfo.Tables[0].DefaultView[0]["SHORT_NAME"].ToString() + @"<br><br>
                      " + txtReworkToSupplier.Text + @"<br>  
                    Please click the below link to <b>rework.</b><br>
                   <a href='" + strServerIPAdd.Trim() + "'>'" + strServerIPAdd.Trim() + @"'</a> <br>
                    User Name&nbsp;:  " + dsEmailInfo.Tables[0].Rows[0]["User_name"].ToString() + @"<br> 
                    Password&nbsp;&nbsp; :  " + dsEmailInfo.Tables[0].Rows[0]["Password"].ToString() + @"<br>" + @" <br>
                                                                                       
                    THIS IS AN ENQUIRY <b>FOR RE-WORK</b> ONLY. IT IS NOT AN ORDER FOR PURCHASE.<br>  <br>
                    All quotes must be submitted via this format. We will not accept quotes in any other format. <br>
                    Upon receipt of this request  Kindly complete all highlighted cells and return via  this mode of transmission. <br><br>
                    Please review quantity, unit of measure, part number, lead time(in days), delivery date  and mark changes <br>
                    as required prior sending out the quote. Failure to use this form may result in disqualification.<br><br>
                    Special Instruction: If you haven`t received a response from us in 20 working days, Please consider this request to be closed. <br><br>
                    We will require an estimate of total shipment weight if possible, with the quote.  <br>    
                    In the event you are awarded this requisition, Payment will only be made to the name and address listed on the Quotation. <br><br> <br>   

                    <b>IMPORTANT:</b> <br><br>
                    Terms Subject to " + Session["Company_Name_GL"].ToString() + @" standard terms and conditions of purchase.<br><br>
                    *          Please submit your quote urgently by return / within 3 days of receipt of this RFQ. <br><br>
                   <b>Note:</b><br><br>
                    1.	Insurance should be covered by the vendor’s policy until the goods have been <br>
                        received at the agreed delivery point specified in the order and/or our separate dispatch instructions.
                    2.	No additional goods to this should be supplied without our approval.<br>
                    3.	Please provide appropriate certificate from class maker and/or supplier for the above items. <br><br>

                    Thank you & best regards,<br>
                    " + Session["USERFULLNAME"].ToString() + @"<br>
                    " + dtUser.Rows[0]["Designation"].ToString() + @"<br>
                   " + Convert.ToString(Session["Company_Address_GL"]).Replace("\n", "<br>") + @"<br>
                    Tel: " + dtUser.Rows[0]["Mobile_Number"].ToString() + @"<br>
                    Email: " + dtUser.Rows[0]["MailID"].ToString() + @"<br>
                                 
                                 <br><br>
                    ";

        strSubject = "Please find the Invitation to re-tender for M.T. : " + dsEmailInfo.Tables[0].DefaultView[0]["REQUISITION_CODE"].ToString();
        //To Get Email Address of the supplier 
        sEmailAddress = dsEmailInfo.Tables[0].DefaultView[0]["SuppEmailIDs"].ToString();

     



    }


    protected void OnSpliting(object source, CommandEventArgs e)
    {

    }

    protected void btndivSave_Click(object sender, EventArgs e)
    {
        string ItemIDs = HiddenItemIDs.Value.ToString();
        TechnicalBAL objtechBAL = new TechnicalBAL();
        int retVal = objtechBAL.RequisitionItemsOnSplit(Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString(), Request.QueryString["QUOTATION_CODE"].ToString(), ItemIDs, Session["userid"].ToString());


        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DataTable dtQuotationList = new DataTable();
            dtQuotationList.Columns.Add("Qtncode");
            dtQuotationList.Columns.Add("amount");

            //Requisition stage status update
            objTechService.InsertRequisitionStageStatus(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), "QEV", " ", Convert.ToInt32(Session["userid"]), dtQuotationList);

        }



        String msg = String.Format("alert('Items has been splited sucessfully.'); RefreshPendingDetails(); window.close();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        divOnSplit.Visible = false;

    }
    protected void btndivCancel_Click(object sender, EventArgs e)
    {
        divOnSplit.Visible = false;
    }
    protected bool isProvisionLimitExceeding()
    {
        bool isProvisionLimitExceeding = false;
        // disable on 20130809 - enable for provision limit validation
        //if (lblITEMSYSTEMCODE.Value.Equals("PROVI"))
        //{
        //    DataTable dtProvLimits = BLL_PURC_Common.Get_Check_Provision_Limit(Request.QueryString["Document_Code"]);
        //    if (dtProvLimits.Rows.Count > 0)
        //        isProvisionLimitExceeding = true;
        //}

        return isProvisionLimitExceeding;
    }
    /// <summary>
    /// PO Approving Budget Amount should be greater than Approval Amount 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            info.MergedColumns.Clear();
            info.StartColumns.Clear();
            info.Titles.Clear();

            //string[] strArgs = HiddenArgument.Split(',');
            string QuotationCode = "";
            string SupplierCode = "";
            bool IsFinalAproved = false;
            DataTable dtQuotationList_ForTopApprover = new DataTable();
            dtQuotationList_ForTopApprover.Columns.Add("Qtncode");
            dtQuotationList_ForTopApprover.Columns.Add("amount");

            DataTable dtQuotationList = new DataTable();
            dtQuotationList.Columns.Add("Qtncode");
            dtQuotationList.Columns.Add("amount");

            DataTable dtBudgetCode = new DataTable();
            dtBudgetCode.Columns.Add("Qtncode");
            dtBudgetCode.Columns.Add("amount");
            TechnicalBAL objTechBAL = new TechnicalBAL();

            string[] Attchment = new string[10];

            // check for provision's approval limit for items
            bool isProvisionLimitsts = isProvisionLimitExceeding();


            //Get Approval Amount 

            BLL_PURC_Purchase objApproval = new BLL_PURC_Purchase();
            DataTable dtApproval = objApproval.Get_Approval_Limit(Convert.ToInt32(Session["USERID"]), ViewState["Dept_Code"].ToString());
            if (dtApproval.Rows.Count < 1)
            {
                String msgApp = String.Format("alert('Approval limit does not exist for you.Please contact admin.');RefreshPendingDetails();window.close();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg99011a", msgApp, true);
                //divApprove.Visible = false;
                String msgmodal = String.Format("hideModal('divApprove');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodalhide", msgmodal, true);
                return;
            }

            decimal dblPOAprLimitAmt = decimal.Parse(dtApproval.Rows[0]["Approval_Limit"].ToString());
            if (dblPOAprLimitAmt < 1)
            {
                String msgApp = String.Format("alert('Approval limit does not exist for you.Please contact admin.');RefreshPendingDetails() ;window.close();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg660081g", msgApp, true);
                // divApprove.Visible = false;
                String msgmodal = String.Format("hideModal('divApprove');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodalhide", msgmodal, true);
                return;
            }

            TechnicalBAL objtechBAL = new TechnicalBAL();
            objtechBAL.Update_ReqsnType(Request.QueryString["Requisitioncode"].ToString(), UDFLib.ConvertToInteger(ddlReqsnType.SelectedValue), "", UDFLib.ConvertToInteger(Session["USERID"].ToString()));

            decimal Supp_Total_Amount = 0;
            decimal approvedAmount = 0;
            dicTotalAmount.Clear();
            string[] AprTotalAmount = HiddenFieldTotalAmountApproved.Value.Split(new char[] { '@' });
            foreach (string item in AprTotalAmount)
            {
                if (item != "")
                {
                    dicTotalAmount.Add(item.Split(new char[] { '&' })[1], item.Split(new char[] { '&' })[0]);
                    approvedAmount += Convert.ToDecimal(item.Split(new char[] { '&' })[0]);
                }
            }

            // add the orderedamount from existing POs in approved amount(amount going to be approved)
            approvedAmount = approvedAmount + Convert.ToDecimal(hdfOrderAmounts.Value);

            Supp_Total_Amount = decimal.Parse(hdfMaxQuotedAmount.Value);

            foreach (GridViewRow item in rgdSupplierInfo.Rows)
            {
                CheckBox chkQuaEvaluated = (CheckBox)(item.FindControl("chkQuaEvaluated") as CheckBox);

                SupplierCode = rgdSupplierInfo.DataKeys[item.RowIndex].Values["SUPPLIER"].ToString();
                QuotationCode = rgdSupplierInfo.DataKeys[item.RowIndex].Values["QUOTATION_CODE"].ToString();

                if (chkQuaEvaluated.Checked && dicTotalAmount.ContainsKey(QuotationCode))
                {


                    //approvedAmount += decimal.Parse(dicTotalAmount[QuotationCode]);
                    BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();

                    //objTechBAL.InsertUserApprovalEntries(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Session["userid"].ToString(), Session["userid"].ToString(), Supp_Total_Amount, approvedAmount, SupplierCode, txtComment.Text.Trim());
                    string retval = "TRUE";
                    if (hdnBudgetCode.Value.ToString() == "1")
                    {
                        DataRow dtrow = dtBudgetCode.NewRow();
                        dtrow[0] = QuotationCode;
                        dtrow[1] = dicTotalAmount[QuotationCode].ToString();
                        dtBudgetCode.Rows.Add(dtrow);
                        retval = objTechService.Check_Update_BudgetCode(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), ddlBudgetCode.SelectedValue, dtBudgetCode);
                    }

                    if (retval == "TRUE")
                    {
                        if (Supp_Total_Amount > dblPOAprLimitAmt || approvedAmount > dblPOAprLimitAmt)// supplier code is zero in this case
                        {
                            objApproval.POApproving(Request.QueryString["Requisitioncode"].ToString(), QuotationCode, "0", Session["userid"].ToString(), "", Request.QueryString["Vessel_Code"].ToString(), ddlBudgetCode.SelectedValue.ToString());

                            // store the qtn code and supp amount for top approver
                            DataRow dtrow = dtQuotationList_ForTopApprover.NewRow();
                            dtrow[0] = QuotationCode;
                            dtrow[1] = dicTotalAmount[QuotationCode].ToString();
                            dtQuotationList_ForTopApprover.Rows.Add(dtrow);
                        }

                        else if (Supp_Total_Amount <= dblPOAprLimitAmt && approvedAmount <= dblPOAprLimitAmt && (isProvisionLimitsts == false))//The actual approval 
                        {
                            //BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();

                            //qtnbased

                            DataRow dtrow = dtQuotationList.NewRow();
                            dtrow[0] = QuotationCode;
                            dtrow[1] = dicTotalAmount[QuotationCode].ToString();
                            dtQuotationList.Rows.Add(dtrow);

                            /// begin insert the records for final PO
                            DataTable dtReqInfo = new DataTable();
                            dtReqInfo = objTechService.SelectSupplierToSendOrderEval(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), QuotationCode);
                            SavePurchasedOrder(dtReqInfo.DefaultView.ToTable());

                            objTechService.POApproving(Request.QueryString["Requisitioncode"].ToString(), QuotationCode, SupplierCode, Session["userid"].ToString(), txtComment.Text, Request.QueryString["Vessel_Code"].ToString(), ddlBudgetCode.SelectedValue.ToString());


                            IsFinalAproved = true;

                        }
                    }
                    else
                    {
                        //btnRequestAmount.Visible = true;
                        String msg1 = String.Format("alert('A.Total Approval amount is greater than Budget limit,Please request for increase Budget limit.');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg1, true);
                        String msgmodal = String.Format("showModal('divApprove');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodal", msgmodal, true);
                    }
                }
            }

            if (IsFinalAproved)
            {
                //Requisition stage status update
                BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
                //Check Budget Code and Update
                string retval = "TRUE";
                if (hdnBudgetCode.Value.ToString() == "1")
                {
                    retval = objTechService.Check_Update_BudgetCode(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), ddlBudgetCode.SelectedValue, dtBudgetCode);
                }

                if (retval == "TRUE")
                {
                    //btnRequestAmount.Visible = false;
                    // SAVE APPROVAL
                    objTechBAL.InsertUserApprovalEntries(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Session["userid"].ToString(), Session["userid"].ToString(), Supp_Total_Amount, approvedAmount, "0", txtComment.Text.Trim(), dtQuotationList);

                    BLL_PURC_Common.INS_Remarks(Request.QueryString["Document_Code"], Convert.ToInt32(Session["userid"].ToString()), txtComment.Text.Trim(), 303);
                    objTechService.InsertRequisitionStageStatus(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), "RPO", " ", Convert.ToInt32(Session["USERID"]), dtQuotationList);

                    String msg1 = String.Format("alert('Approved successfully.'); RefreshPendingDetails(); window.close();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg1, true);
                    return;
                }
                else
                {
                    //btnRequestAmount.Visible = true;
                    String msg1 = String.Format("alert('B.Total Approval amount is greater than Budget limit,Please request for increase Budget limit.');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg1, true);
                    String msgmodal = String.Format("showModal('divApprove');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodal", msgmodal, true);
                }
            }

            else
            {

                if (!isProvisionLimitsts)
                {

                    //check if only one approver is left then send him directly instead of prompting the current user to send and save the current approver's approval.
                    BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
                    string retval = "TRUE";
                    if (hdnBudgetCode.Value.ToString() == "1")
                    {
                        retval = objTechService.Check_Update_BudgetCode(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), ddlBudgetCode.SelectedValue, dtBudgetCode);
                    }

                    if (retval == "TRUE")
                    {
                        //btnRequestAmount.Visible = false;
                        int ApproverCount = BLL_PURC_Common.CheckHierarchy_SendForApproval(Request.QueryString["Requisitioncode"], Request.QueryString["Document_Code"], Convert.ToInt32(Request.QueryString["Vessel_Code"]), Session["userid"].ToString(), Supp_Total_Amount, dblPOAprLimitAmt, dtQuotationList_ForTopApprover);
                        if (ApproverCount == 1)
                        {
                            BLL_PURC_Common.INS_Remarks(Request.QueryString["Document_Code"], Convert.ToInt32(Session["userid"].ToString()), txtComment.Text.Trim(), 303);
                            String msg = String.Format("alert('Approved successfully but  total Approval amount is greater than your approval limit ,this Requisition is now being sent to your supirior for his approval.'); RefreshPendingDetails();window.close();");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg16", msg, true);
                        }
                        else if (ApproverCount > 1)
                        {
                            BLL_PURC_Common.INS_Remarks(Request.QueryString["Document_Code"], Convert.ToInt32(Session["userid"].ToString()), txtComment.Text.Trim(), 303);
                            String msg1 = String.Format("alert('Approved successfully but  total Approval amount is greater than your approval limit ,this Requisition is now being sent to your supirior for his approval ');");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg1, true);
                            ucApprovalUser1.ReqsnCode = Request.QueryString["Requisitioncode"];


                            ucApprovalUser1.FillUser();
                            dvSendForApproval.Visible = true;
                        }
                        else if (ApproverCount == 0)
                        {
                            BLL_PURC_Common.INS_Remarks(Request.QueryString["Document_Code"], Convert.ToInt32(Session["userid"].ToString()), txtComment.Text.Trim(), 303);
                            String msg1 = String.Format("alert('Total Approval amount is greater than your approval limit and no approver found for the amount " + Supp_Total_Amount + " . Please contact your manager.' );");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgCt0", msg1, true);

                        }
                        String msgmoda12l = String.Format("hideModal('divApprove');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodalhide", msgmoda12l, true);
                    }
                    else
                    {
                        //btnRequestAmount.Visible = true;
                        String msg1 = String.Format("alert('C.Total Approval amount is greater than Budget limit,Please request for increase Budget limit.');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg1, true);
                        String msgmodal = String.Format("showModal('divApprove');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinalmodal", msgmodal, true);
                    }
                }
            }



            //divApprove.Visible = false;
            return;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }




    }
    //if approver's limit is less than qtn amount then he has to forward this reqsn to his supirior,in this case if approver will not send to supirior then it will not save his/her approval.
    // here it save after sending to supirior.
    public void OnStsSaved(object s, EventArgs e)
    {
        TechnicalBAL objTechBAL = new TechnicalBAL();
        string QuotationCode = "";
        string SupplierCode = "";

        decimal Supp_Total_Amount = 0;
        decimal approvedAmount = 0;
        dicTotalAmount.Clear();

        DataTable dtQuotationList = new DataTable();
        dtQuotationList.Columns.Add("Qtncode");
        dtQuotationList.Columns.Add("amount");

        string[] AprTotalAmount = HiddenFieldTotalAmountApproved.Value.Split(new char[] { '@' });
        foreach (string item in AprTotalAmount)
        {
            if (item != "")
            {
                dicTotalAmount.Add(item.Split(new char[] { '&' })[1], item.Split(new char[] { '&' })[0]);
                approvedAmount += Convert.ToDecimal(item.Split(new char[] { '&' })[0]);
            }
        }

        foreach (GridViewRow item in rgdSupplierInfo.Rows)
        {
            CheckBox chkQuaEvaluated = (CheckBox)(item.FindControl("chkQuaEvaluated") as CheckBox);

            SupplierCode = item.Cells[2].Text;
            QuotationCode = rgdSupplierInfo.DataKeys[item.RowIndex].Values["QUOTATION_CODE"].ToString();

            if (chkQuaEvaluated.Checked && dicTotalAmount.ContainsKey(QuotationCode))
            {

                Supp_Total_Amount = decimal.Parse(rgdSupplierInfo.DataKeys[item.RowIndex].Values["Supp_Tot_Amt"].ToString());

                DataRow dtrow = dtQuotationList.NewRow();
                dtrow[0] = QuotationCode;
                dtrow[1] = dicTotalAmount[QuotationCode].ToString();
                dtQuotationList.Rows.Add(dtrow);
            }
        }

        // first send for approval 
        int stsEntry = objTechBAL.InsertUserApprovalEntries(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Session["userid"].ToString(), ucApprovalUser1.ApproverID, 0, 0, "", ucApprovalUser1.Remark, dtQuotationList);

        // save the approval
        objTechBAL.InsertUserApprovalEntries(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Session["userid"].ToString(), Session["userid"].ToString(), Supp_Total_Amount, approvedAmount, "0", txtComment.Text.Trim(), dtQuotationList);
        String msg1 = String.Format("alert('Sent successfully.'); RefreshPendingDetails();window.close();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgSENT", msg1, true);

    }

    protected void gvApprovalHistory_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //call the method for custom rendering the columns headers	on row created event



        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objApprovalHist);

            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);

            ViewState["DynamicHeaderCSS"] = "HeaderStyle-css";
        }

    }
    //method for rendering the columns headers	

    private void RenderHeader(HtmlTextWriter output, Control container)
    {
        int suppCount = 2;
        string HeadCss = "";
        for (int i = 0; i < container.Controls.Count; i++)
        {
            TableCell cell = (TableCell)container.Controls[i];
            //stretch non merged columns for two rows
            if (!info.MergedColumns.Contains(i))
            {
                cell.Attributes["rowspan"] = "2";

                cell.RenderControl(output);
            }
            else //render merged columns common title
                if (info.StartColumns.Contains(i))
                {
                    if (suppCount % 2 == 0)
                    {
                        HeadCss = "QtnEval-HeaderStyle-css";
                    }
                    else
                    {
                        HeadCss = "QtnEval-AltHeaderStyle-css";
                    }


                    output.Write(string.Format("<th align='center' class='" + HeadCss + "' colspan='{0}'>{1}</th>",
                             info.StartColumns[i], info.Titles[i]));
                    suppCount++;

                }
        }

        //close the first row	
        output.RenderEndTag();
        //set attributes for the second row
        //grid.HeaderStyle.AddAttributesToRender(output);
        //start the second row
        output.RenderBeginTag("tr");


        int j = 1;
        int GroupCount = info.MergedColumns.Count / Convert.ToInt32(ViewState["ColumnCount_Supp"].ToString());
        int StartGroup = 1;
        bool Alternategroup = false;
        //render the second row (only the merged columns)
        for (int i = 0; i < info.MergedColumns.Count; i++)
        {
            //if qtn eval 

            TableCell cell = (TableCell)container.Controls[info.MergedColumns[i]];


            if (i < (StartGroup * Convert.ToInt32(ViewState["ColumnCount_Supp"].ToString())))
            {
                cell.CssClass = "QtnEval-HeaderStyle-css";
                cell.RenderControl(output);
                if (i == ((StartGroup * Convert.ToInt32(ViewState["ColumnCount_Supp"].ToString())) - 1))
                {
                    Alternategroup = true;
                    continue;
                }
            }
            if (Alternategroup)
            {
                cell.CssClass = "QtnEval-AltHeaderStyle-css";
                cell.RenderControl(output);

                if (j == Convert.ToInt32(ViewState["ColumnCount_Supp"].ToString()))
                {
                    j = 1;
                    Alternategroup = false;
                    if (StartGroup <= GroupCount)
                    {
                        StartGroup = StartGroup + 2;
                    }
                }
                j++;

            }

        }

        info.MergedColumns.Clear();
        info.StartColumns.Clear();
        info.Titles.Clear();
    }


    private void RenderHeaderApprHistory(HtmlTextWriter output, Control container)
    {

        for (int i = 0; i < container.Controls.Count; i++)
        {
            TableCell cell = (TableCell)container.Controls[i];
            //stretch non merged columns for two rows
            if (!info.MergedColumns.Contains(i))
            {
                cell.Attributes["rowspan"] = "2";

                cell.RenderControl(output);
            }
            else //render merged columns common title
                if (info.StartColumns.Contains(i))
                {


                    output.Write(string.Format("<th align='center'  colspan='{0}'>{1}</th>",
                             info.StartColumns[i], info.Titles[i]));


                }
        }

        //close the first row	
        output.RenderEndTag();
        //set attributes for the second row
        //grid.HeaderStyle.AddAttributesToRender(output);
        //start the second row
        output.RenderBeginTag("tr");



        //render the second row (only the merged columns)
        for (int i = 0; i < info.MergedColumns.Count; i++)
        {
            //if qtn eval 

            TableCell cell = (TableCell)container.Controls[info.MergedColumns[i]];

            cell.CssClass = "HeaderStyle-css";
            cell.RenderControl(output);

        }

        info.MergedColumns.Clear();
        info.StartColumns.Clear();
        info.Titles.Clear();
    }

    [Serializable]
    private class MergedColumnsInfo
    {
        // indexes of merged columns
        public List<int> MergedColumns = new List<int>();
        // key-value pairs: key = the first column index, value = number of the merged columns
        public Hashtable StartColumns = new Hashtable();
        // key-value pairs: key = the first column index, value = common title of the merged columns 
        public Hashtable Titles = new Hashtable();

        //parameters: the merged columns indexes, common title of the merged columns 
        public void AddMergedColumns(int[] columnsIndexes, string title)
        {
            MergedColumns.AddRange(columnsIndexes);
            StartColumns.Add(columnsIndexes[0], columnsIndexes.Length);
            Titles.Add(columnsIndexes[0], title);
        }
    }

    private MergedColumnsInfo info
    {
        get
        {
            if (ViewState["info"] == null)
                ViewState["info"] = new MergedColumnsInfo();
            return (MergedColumnsInfo)ViewState["info"];
        }
    }


    protected void gvApprovalHistory_DataBound(object sender, EventArgs e)
    {
        string HeaderToHide = "";
        string HeaderSupplier = "";

        var grh = gvApprovalHistory.HeaderRow;
        for (int i = 0; i < gvApprovalHistory.HeaderRow.Cells.Count; i++)
        {

            string Suppname = grh.Cells[i].Text.ToLower();
            if (Suppname.Contains("_hide"))
            {
                HeaderToHide += i.ToString() + ",";
                HeaderSupplier += grh.Cells[i].Text.Split(new char[] { '_' })[0] + ",";

                gvApprovalHistory.HeaderRow.Cells[i].Visible = false;
                foreach (GridViewRow gr in gvApprovalHistory.Rows)
                {
                    gr.Cells[i].Visible = false;
                }


            }
            if (Suppname.Contains("_amount"))
            {
                gvApprovalHistory.HeaderRow.Cells[i].Text = "Amount";
                gvApprovalHistory.HeaderRow.Cells[i].CssClass = "HeaderStyle-css";
                foreach (GridViewRow gr in gvApprovalHistory.Rows)
                {
                    gr.Cells[i].CssClass = "amount-css";
                    gr.Cells[0].CssClass = "text-css";
                    gr.Cells[gr.Cells.Count - 1].CssClass = "text-css";
                }

            }
            if (Suppname.Contains("_currency"))
            {
                gvApprovalHistory.HeaderRow.Cells[i].Text = "Currency";
                gvApprovalHistory.HeaderRow.Cells[i].CssClass = "HeaderStyle-css";
            }
        }




        string[] strColumnIndex = HeaderToHide.Split(new char[] { ',' });
        string[] strHeaderName = HeaderSupplier.Split(new char[] { ',' });
        int index = 0;
        foreach (string item in strColumnIndex)
        {
            if (item != "")
            {

                objApprovalHist.AddMergedColumns(new int[] { int.Parse(item) + 1, int.Parse(item) + 2 }, strHeaderName[index]);
            }
            index++;
        }

    }


    protected void imgclose_Click(object sender, ImageClickEventArgs e)
    {
        //divApprove.Visible = false;
    }

    protected void rgdQuatationInfo_ItemCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.SetRenderMethodDelegate(RenderHeader);

        }

    }
    protected void btnReworktopurc_Click(object s, EventArgs e)
    {
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        // if stage from is  "QEV-FOR~PURCHASER" means rework for purchaser
        int iReturn = objTechService.CancelRequisitionStages(Convert.ToInt32(Request.QueryString["Vessel_Code"].ToString()), Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString(), "RFQ", txtRemarkToPurc.Text, Convert.ToInt32(Session["userid"].ToString()), "QEV-FOR~PURCHASER");
        BLL_PURC_Common.INS_Remarks(Request.QueryString["Document_Code"], Convert.ToInt32(Session["userid"].ToString()), txtRemarkToPurc.Text, 301);

        String msg1 = String.Format("alert('Requisition has been sent to purchaser'); RefreshPendingDetails();window.close();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg1, true);
    }

    protected void rgdSupplierInfo_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow item = (GridViewRow)e.Row;
            ((Image)item.FindControl("imgQuRemark")).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[" + ((Image)item.FindControl("imgQuRemark")).AlternateText + "]");
            ((Label)item.FindControl("lblPkg")).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[" + ((Label)item.FindControl("lblPkgRs")).Text + "]");
            ((Label)item.FindControl("lblOtherCost")).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[" + ((Label)item.FindControl("lblOtherCostRs")).Text + "]");
            ((Label)item.FindControl("txtVat")).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[" + DataBinder.Eval(e.Row.DataItem, "VAT").ToString() + "]");
            //((CheckBox)item.FindControl("chkQuaEvaluated")).Attributes.Add("onclick", "javascript:GetSupplierStatus('" + rgdSupplierInfo.DataKeys[item.RowIndex].Values["SUPPLIER"].ToString() + "')");
            ((CheckBox)item.FindControl("chkQuaEvaluated")).Attributes.Add("onclick", "javascript:GetSupplierStatus('" + rgdSupplierInfo.DataKeys[item.RowIndex].Values["SUPPLIER"].ToString() + "','" + ((CheckBox)item.FindControl("chkQuaEvaluated")).ClientID + "')");
            Label txtVat = (Label)item.FindControl("txtVat");
            if (DataBinder.Eval(e.Row.DataItem, "CONTRACT_STS").ToString() == "0")
            {
                DataControlFieldCell suppcell = ((DataControlFieldCell)(((Label)e.Row.FindControl("lblsupplier_shortname")).Parent));

                suppcell.BackColor = System.Drawing.Color.Red;
                suppcell.ForeColor = System.Drawing.Color.White;
                suppcell.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header[] body=[ Contract " + DataBinder.Eval(e.Row.DataItem, "QTN_Contract_Code").ToString() + " has expired !]");

            }
            //TextBox Vatcell = ((DataControlFieldCell)(((TextBox)e.Row.FindControl("txtVat")).Parent));
            if (DataBinder.Eval(e.Row.DataItem, "Applicable_Flag").ToString() == "Yes")
            {
                txtVat.ForeColor = System.Drawing.Color.White;
                e.Row.Cells[9].BackColor = System.Drawing.Color.Green;
                //Vatcell.BackColor = System.Drawing.Color.Green;
            }
           
        }
    }

    protected void btnReworkToSupplier_Click(object s, EventArgs e)
    {
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        DataSet dsEmailInfo = new DataSet();
        string Supplier_code = hdfSuppCode.Value;
        dsEmailInfo = objTechService.GetRFQsuppInfoSendEmail(Supplier_code, hdfQTNCode.Value, Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), Session["userid"].ToString());

        int RetVal = objTechService.UpdateQuotForRework(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), Supplier_code, hdfQTNCode.Value);
        if (RetVal != 0)
        {
            BLL_PURC_Common.INS_Remarks(Request.QueryString["Document_Code"], Convert.ToInt32(Session["userid"].ToString()), txtReworkToSupplier.Text, 300);
            btnSaveEvaln.Enabled = false;
            btnFinalizeEval.Enabled = false;
            string ServerIPAdd = ConfigurationManager.AppSettings["WebQuotSite"].ToString();
            SendEmailToSupplier(dsEmailInfo, Supplier_code, ServerIPAdd, "attch");
            BindQuatationSendBySupplier();
        }

    }
    // for purchaser 
    protected void lbtnSendForApproval_Click(object sender, EventArgs e)
    {
        if (rgdSupplierInfo.Rows.Count > 0)
        {
            ucApprovalUserToSuppdt.ReqsnCode = Request.QueryString["Requisitioncode"];

            ucApprovalUserToSuppdt.FillUser();
            gvQuotationList.DataSource = BLL_PURC_Common.PURC_GET_Quotation_ByReqsnCode(Request.QueryString["Requisitioncode"]);
            gvQuotationList.DataBind();
            dvSendTosuppdt.Visible = true;
        }
        else
        {
            String msg = String.Format("alert('You have not received any quotation for this requisition !');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }

    protected void OnStsSavedSentToApprover(object s, EventArgs e)
    {
        try
        {

            TechnicalBAL objpurc = new TechnicalBAL();
            TechnicalBAL objTechBAL = new TechnicalBAL();

            //qtnbased
            DataTable dtQuotations = BLL_PURC_Common.PURC_GET_Quotation_ByReqsnCode(Request.QueryString["Requisitioncode"].Trim());
            DataTable dtQuotationList = new DataTable();
            dtQuotationList.Columns.Add("Qtncode");
            dtQuotationList.Columns.Add("amount");

            foreach (DataRow dr in dtQuotations.Rows)
            {
                if (dr["active_PO"].ToString() == "0") // for those POs have been cancelled(means active_PO is zero)
                {
                    DataRow dtrow = dtQuotationList.NewRow();
                    dtrow[0] = dr["QUOTATION_CODE"].ToString();
                    dtrow[1] = "0";
                    dtQuotationList.Rows.Add(dtrow);
                }
            }

            // save the requested qty into order qty and order qty column on grid will be bibnded to order qty (change so store the updated qty by supp at the time of eval.) { this functionality has been implemented at rfq send stage}
            // code to update the order qty have been commented ,this is used to save bgt code only.

            BLL_PURC_Common.Update_OrderQty_From_ReqstQty(Request.QueryString["Requisitioncode"].Trim(), Request.QueryString["Document_Code"].Trim(), ddlBGTCodeToSuppdt.SelectedValue);

            int stsEntry = objTechBAL.InsertUserApprovalEntries(Request.QueryString["Requisitioncode"].Trim(), Request.QueryString["Document_Code"], Request.QueryString["Vessel_Code"], Session["USERID"].ToString(), ucApprovalUserToSuppdt.ApproverID, 0, 0, "", ucApprovalUserToSuppdt.Remark, dtQuotationList);
            if (stsEntry > 0)
            {
                // dtQuotationList is passing but not using in sp ,all quotation will be set as senttosuppdt as true for this reqsn
                int res = objpurc.PURC_Update_SentToSupdt(0, 0, Request.QueryString["Requisitioncode"].Trim(), int.Parse(Session["USERID"].ToString()), ucApprovalUserToSuppdt.Remark, dtQuotationList);

                if (res > 0)
                {
                    BLL_PURC_Purchase objPurc = new BLL_PURC_Purchase();
                    //Requisition stage status update
                    objPurc.InsertRequisitionStageStatus(Request.QueryString["Requisitioncode"].Trim(), Request.QueryString["Vessel_Code"], Request.QueryString["Document_Code"], "QEV", " ", Convert.ToInt32(Session["USERID"]), dtQuotationList);
                    BLL_PURC_Common.INS_Remarks(Request.QueryString["Document_Code"], Convert.ToInt32(Session["userid"].ToString()), ucApprovalUserToSuppdt.Remark, 302);

                    String msg = String.Format("alert('Sent successfully'); RefreshPendingDetails();window.close();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    protected class DataGridTempla : System.Web.UI.Page, ITemplate
    {
        ListItemType templateType;
        string columnName;
        string Header;
        string DataFieldName;
        string DataFieldActive_PO;

        public DataGridTempla(ListItemType type, string colname, string HeaderText, string DataField, string DFActive_PO = "")
        {
            templateType = type;
            columnName = colname;
            Header = HeaderText;
            DataFieldName = DataField;
            DataFieldActive_PO = DFActive_PO;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            Literal lc = new Literal();
            CheckBox chkb = new CheckBox();
            //chkb.AutoPostBack = true;

            switch (templateType)
            {
                case ListItemType.Header:
                    lc.Text = Header;

                    //chkb.Text = "Select"; 
                    //lb.CommandName = "EditButton"; 
                    chkb.ID = columnName;

                    //chkb.ID = "Select";

                    //chkb.CheckedChanged += new EventHandler(this.chkb_Header);   
                    container.Controls.Add(chkb);
                    container.Controls.Add(lc);
                    break;

                case ListItemType.Item:
                    //lc.Text=columnName;
                    //lc.Text = "Select " + columnName;
                    //chkb.Text = "Select"; 
                    chkb.ID = columnName;// "Select";
                    chkb.DataBinding += new EventHandler(ChK_DataBinding);
                    // chkb.UniqueID = columnName; // +"_Chk";
                    //chkb.Height = 14;
                    //chkb.Width = 14;
                    //chkb.CssClass = "chk";
                    //chkb.CheckedChanged += new EventHandler(this.chkb_Items);
                    container.Controls.Add(chkb);
                    container.Controls.Add(lc);
                    break;

                case ListItemType.EditItem:

                    TextBox tb = new TextBox();
                    tb.Text = "";
                    container.Controls.Add(tb);
                    break;

                case ListItemType.Footer:

                    lc.Text = "<I>" + Header + "</I>";
                    container.Controls.Add(lc);
                    break;

            }

        }

        void ChK_DataBinding(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow container = (GridViewRow)chk.NamingContainer;
            chk.Checked = ((DataRowView)container.DataItem)[DataFieldName].ToString() == "True" ? true : false;
            chk.Enabled = ((DataRowView)container.DataItem)[DataFieldActive_PO].ToString() == "0" ? true : false;
        }


    }


    protected class DataGridTemplateImage : System.Web.UI.Page, ITemplate
    {
        ListItemType templateType;
        string columnName;
        string strRemark;
        string DataFieldName;

        public DataGridTemplateImage(ListItemType type, string colname, string strDataFieldName, string strRemarkFrom)
        {
            templateType = type;
            columnName = colname;
            strRemark = strRemarkFrom;
            DataFieldName = strDataFieldName;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            Literal lc = new Literal();
            Image img = new Image();

            switch (templateType)
            {
                case ListItemType.Header:
                    lc.Text = "<B>" + columnName + "</B>";
                    container.Controls.Add(lc);
                    break;

                case ListItemType.Item:
                    img.ID = columnName + "_img";
                    img.Height = 12;
                    img.Width = 12;
                    img.ImageUrl = "~/PURCHASE/Image/view1.gif";
                    img.DataBinding += new EventHandler(img_DataBinding);

                    container.Controls.Add(img);
                    container.Controls.Add(lc);
                    break;

            }

        }


        protected void img_DataBinding(object s, EventArgs e)
        {
            Image img = (Image)s;
            GridViewRow container = (GridViewRow)img.NamingContainer;
            ///remark
            string strremark = ((DataRowView)container.DataItem)[DataFieldName].ToString();

            if (strremark != "")
            {
                img.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[" + strremark + "]");
                img.AlternateText = strremark;
            }
            else
            {
                img.ImageUrl = "~/PURCHASE/Image/remarknone.png";
            }

        }


    }



    protected class DataGridTemplateDropDown : System.Web.UI.Page, ITemplate
    {
        ListItemType templateType;
        string columnName;
        string strRemark;

        public DataGridTemplateDropDown(ListItemType type, string colname, string strRemarkFrom)
        {
            templateType = type;
            columnName = colname;

        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            Literal lc = new Literal();
            DropDownList ddlItem = new DropDownList();

            switch (templateType)
            {
                case ListItemType.Header:
                    lc.Text = "<B>" + columnName + "</B>";
                    container.Controls.Add(lc);
                    break;

                case ListItemType.Item:
                    ddlItem.ID = columnName + "_ddl";
                    ddlItem.Width = 65;
                    ddlItem.Font.Size = FontUnit.XSmall;
                    container.Controls.Add(ddlItem);
                    container.Controls.Add(lc);
                    break;

            }

        }




    }


    protected class DataGridTemplateLabel : System.Web.UI.Page, ITemplate
    {
        ListItemType templateType;
        string columnName;
        string Header;
        string DataFieldName;

        public DataGridTemplateLabel(ListItemType type, string colname, string HeaderText, string DataField)
        {
            templateType = type;
            columnName = colname;
            Header = HeaderText;
            DataFieldName = DataField;

        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            Literal lc = new Literal();
            Label lbl_control = new Label();

            switch (templateType)
            {
                case ListItemType.Header:
                    lc.Text = "<B>" + Header + "</B>";
                    container.Controls.Add(lc);
                    break;

                case ListItemType.Item:
                    lbl_control.ID = columnName;
                    container.Controls.Add(lbl_control);
                    lbl_control.DataBinding += new EventHandler(Lbl_DataBinding);
                    container.Controls.Add(lc);
                    container.Controls.Add(lbl_control);


                    break;

            }

        }


        void Lbl_DataBinding(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            GridViewRow container = (GridViewRow)lbl.NamingContainer;

            decimal outRes = 0;

            string strvalue = ((DataRowView)container.DataItem)[DataFieldName].ToString();
            string dataFiled_CTP_Price_change = "";
            string dataFiled_CTP_Price = "";

            if (decimal.TryParse(strvalue, out outRes))
            {
                lbl.Text = outRes.ToString("0.##");
            }
            else
            {
                lbl.Text = ((DataRowView)container.DataItem)[DataFieldName].ToString();
            }


            if (DataFieldName.Contains("_Rate"))
            {
                dataFiled_CTP_Price_change = DataFieldName.Split('_')[0] + "_CTP_rate_change";
                dataFiled_CTP_Price = DataFieldName.Split('_')[0] + "_CTP_rate";

                if (UDFLib.ConvertToDecimal(((DataRowView)container.DataItem)[dataFiled_CTP_Price_change]) == 1)
                {
                    ((DataControlFieldCell)lbl.Parent).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[ Contract Unit Price : " + ((DataRowView)container.DataItem)[dataFiled_CTP_Price].ToString() + "]");
                    ((DataControlFieldCell)lbl.Parent).BackColor = System.Drawing.Color.LightPink;

                }
            }


        }
       



    }

    protected void btnRequestAmount_Click(object sender, EventArgs e)
    {
        string OCA_URL = null;
        if (ddlBudgetCode.SelectedIndex > 0)
        {
            if (!Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["OCA_APP_URL"]))
            {
                OCA_URL = ConfigurationManager.AppSettings["OCA_APP_URL"];
            }
            //string PassString = txtPassString.Text.ToString().ToString();
            string OCA_URL1 = OCA_URL + "/Acct/Vessel_Budget_Utilization_Report.asp?Search_budget_Code=" + ddlBudgetCode.SelectedValue + "";

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + OCA_URL1 + "');", true);
        }
        else
        {
            String msg = String.Format("alert('Please select Budget Code !');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }
}


