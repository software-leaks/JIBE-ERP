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

public partial class Purchase_Quotation_Evaluation_Report : System.Web.UI.Page
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

    protected void Page_Init(object source, System.EventArgs e)
    {
        ViewState["RetriveBtnPress"] = false;
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        btnActive = (bool)ViewState["RetriveBtnPress"];
        //string s = Request.QueryString["Vessel_Code"].ToString();

        if (!IsPostBack)
        {

            ViewState["HiddenArgument"] = "";
            ViewState["DynamicHeaderCSS"] = "";
            ViewState["ColumnCount_Supp"] = 7;

            BindRequisitionInfo();
            BindQuatationSendBySupplier();


            HiddenbtnRetrive.Value = "0";
            HiddenDocumentCode.Value = Request.QueryString["Document_Code"].ToString();

            lblVesselCode.Value = Request.QueryString["Vessel_Code"].ToString();

            Session["SqlString"] = Request.QueryString["Requisitioncode"].ToString() + "," + Request.QueryString["Vessel_Code"].ToString() + "," + Request.QueryString["Document_Code"].ToString();

            BLL_PURC_Purchase objHistory = new BLL_PURC_Purchase();


            gvApprovalHistory.DataSource = objHistory.Get_Approver_History(Request.QueryString["Requisitioncode"].ToString().Trim());
            gvApprovalHistory.DataBind();

            info.MergedColumns.Clear();
            info.StartColumns.Clear();
            info.Titles.Clear();

            BindGridOnRetriveButtonClick();

            dlistPONumber.DataSource = BLL_PURC_Common.Get_PONumbers(Request.QueryString["Requisitioncode"]);
            dlistPONumber.DataBind();




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
                lblCatalog.Text = dtReqInfo.DefaultView[0]["SYSTEM_Description"].ToString();
                lblToDate.Text = dtReqInfo.DefaultView[0]["requestion_Date"].ToString();
                lblTotalItem.Text = dtReqInfo.DefaultView[0]["TOTAL_ITEMS"].ToString();
                lblReqDate.Text = dtReqInfo.DefaultView[0]["RFQ_Date"].ToString();
                lblITEMSYSTEMCODE.Value = dtReqInfo.DefaultView[0]["ITEM_SYSTEM_CODE"].ToString();
                lbtnPurchaserRemark.Attributes.Add("onmouseover", "js_ShowToolTip('" + dtReqInfo.DefaultView[0]["SentToSupdt_Remark"].ToString() + "',event,this)");
                lblReqNo.NavigateUrl = "RequisitionSummary.aspx?REQUISITION_CODE=" + Request.QueryString["Requisitioncode"].ToString() + "&Document_Code=" + Request.QueryString["Document_Code"].ToString() + "&Vessel_Code=" + Request.QueryString["Vessel_Code"].ToString() + "&Dept_Code=" + dtReqInfo.DefaultView[0]["DEPARTMENT"].ToString() + "&" + 1.ToString();


            }
            ViewState["dtRequistion"] = dtReqInfo;
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
                dtsuppInfo = objTechService.SelectQuatationSendBySupplier_Report(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString());
                rgdSupplierInfo.DataSource = dtsuppInfo;
                rgdSupplierInfo.DataBind();
                if (dtsuppInfo.Rows.Count > 0)
                    lblQuotDueDate.Text = dtsuppInfo.DefaultView[0]["Quotation_Due_Date"].ToString();

            }

            ViewState["dtSupplier"] = dtsuppInfo;





        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }

    }

    protected void rgdEvalItems_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {

    }

    private void BindGridOnRetriveButtonClick()
    {


        int suppSel = 0;
        string[] arrySuppName = new string[] { "", "", "", "", "", "", "", "", "", "" };
        string[] arrSuppNameInShort = new string[10];
        ViewState["suppliercode"] = "";
        ViewState["quotationcode"] = "";
        ViewState["suppcurrency"] = "";
        string SuppQtnCodesItemTypes = "";


        if (rgdQuatationInfo.MasterTableView.Columns.Count > 12)
        {
            //Remove all the programatically generated colomn from grid and 
            for (int i = rgdQuatationInfo.MasterTableView.Columns.Count - 1; i >= 12; i--)
            {
                rgdQuatationInfo.MasterTableView.Columns.RemoveAt(i);
            }
        }
        if (Request.QueryString["Requisitioncode"].ToString().Substring(0, 2) == "ST" || Request.QueryString["Requisitioncode"].ToString().Substring(0, 3) == "OST")
        {
            rgdQuatationInfo.MasterTableView.Columns[3].Display = false;
        }
        StringBuilder strSql = new StringBuilder();
        StringBuilder strTables = new StringBuilder();
        decimal MinValues = 0;
        int col_id = 11;
        int sel_Id = 0;
        string supplist = "";
        //  HiddenChepSupp.Value = 0;
        // strSql.Append("select distinct a.ITEM_SERIAL_NO,'False' chechstatus, a.QUOTATION_CODE,a.ITEM_REF_CODE, a.ITEM_SHORT_DESC,a.ITEM_FULL_DESC,a.QUOTED_UNIT_ID,a.QUOTED_QTY");
        strSql.Append(@"select distinct a.ITEM_SERIAL_NO
                        ,'False' chechstatus
                        , a.QUOTATION_CODE
                        ,a.ITEM_REF_CODE
                        , a.ITEM_SHORT_DESC
                        ,a.ITEM_FULL_DESC
                        ,a.QUOTED_QTY
                        ,case when item.Drawing_Number='0' then '' else item.Drawing_Number end Drawing_Number
                        ,Item.Part_Number,isnull(Item.Long_Description,'') Long_Description
                        , M.ITEM_COMMENT 
                        ,item.Unit_and_Packings
                        ,M.ROB_Qty
                        ,M.REQUESTED_QTY
                        ,M.ORDER_QTY
                        ,isnull(M.ITEM_INTERN_REF,0) as ITEM_INTERN_REF
                        ,a.Vessel_Code
                        ,PURC_LIB_SUBSYSTEMS.Subsystem_Description
                        ,'" + lblCatalog.Text.Trim() + @"' as Catalogue
                        , '" + lblReqNo.Text.Trim() + "' as Reqsnno");
        // strTables.Append(" from PURC_Dtl_Quoted_Prices a ");
        strTables.Append(@"  from PURC_Dtl_Quoted_Prices a 
                            inner  join PURC_Lib_Items item On item.Item_Intern_Ref=A.Item_Ref_Code 
                            inner join PURC_Dtl_Supply_Items M on M.item_ref_code=A.item_ref_code and M.Document_Code=a.Document_Code and a.Vessel_Code=M.Vessel_Code 
                            inner join PURC_LIB_SUBSYSTEMS on PURC_LIB_SUBSYSTEMS.Subsystem_Code  =M.ITEM_SUBSYSTEM_CODE and PURC_LIB_SUBSYSTEMS.System_Code=M.ITEM_SYSTEM_CODE ");

        string BackColor = "";
        int suppCountBG = 2;
        foreach (GridDataItem dataItem in rgdSupplierInfo.MasterTableView.Items)
        {
            string str = "";

            TextBox txtgrdItemReqQty = (TextBox)(dataItem.FindControl("txtgrdItemReqstdQty") as TextBox);
            CheckBox chk = (CheckBox)(dataItem.FindControl("chkQuaEvaluated") as CheckBox);

            string PortName = rgdSupplierInfo.MasterTableView.DataKeyValues[dataItem.ItemIndex]["PortName"].ToString();
            string suppcurrency = rgdSupplierInfo.MasterTableView.DataKeyValues[dataItem.ItemIndex]["Currency"].ToString();
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

                string QUOTATION_CODE = dataItem["QUOTATION_CODE"].Text.ToString();
                string Col_supp = dataItem["SUPPLIER"].Text.ToString().Replace('-', '_') + QUOTATION_CODE.Replace('-', '_');
                string Col_supp_Alias = "Supp" + dataItem["SUPPLIER"].Text.ToString().Trim() + dataItem.ItemIndex.ToString();
                string Col_supp_where = dataItem["SUPPLIER"].Text;

                string Col_supp_Short = dataItem["SHORT_NAME"].Text.ToString();
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



                strSql.Append(",");
                strTables.Append(" Inner Join ");

                str = @"(select supl.ITEM_REF_CODE
                        ,( QUOTED_RATE * " + dataItem["EXCHANGE_RATE"].Text.ToString() + ") " + Col_supp_Alias + @"_Rate
                        ,QUOTED_PRICE " + Col_supp_Alias + @"_Price
                        ,QUOTED_DISCOUNT " + Col_supp_Alias + @"_Discount
                        , QUOTATION_REMARKS " + Col_supp_Alias + @"_Remark
                        , case when isnull(EVALUATION_OPTION,0)=1 then 'True' else 'False' end as " + Col_supp_Alias + @"_Status
                        ,QUOTATION_CODE
                        ,(((cast(QUOTED_RATE*" + dataItem["EXCHANGE_RATE"].Text.Trim() + " as decimal(18,2))*supl.ORDER_QTY)-(cast(QUOTED_RATE*" + dataItem["EXCHANGE_RATE"].Text.Trim() + " as decimal(18,2))*supl.ORDER_QTY*cast(QUOTED_DISCOUNT* " + dataItem["EXCHANGE_RATE"].Text.Trim() + " as decimal(18,2))/100))) " + Col_supp_Alias + @"_Amount
                        , isnull(Lead_Time,'') " + Col_supp_Alias + @"_Lead_Time
                        , [Description]" + Col_supp_Alias + @"_ItemType    
                        from PURC_Dtl_Quoted_Prices 
                        inner join PURC_DTL_SUPPLY_ITEMS supl on supl.DOCUMENT_CODE=PURC_Dtl_Quoted_Prices.DOCUMENT_CODE and supl.ITEM_REF_CODE=PURC_Dtl_Quoted_Prices.ITEM_REF_CODE  
                        inner join PURC_LIB_SYSTEM_PARAMETERS on Code=Item_Type 
                        where supplier_code='" + Col_supp_where
                    + "' and QUOTATION_CODE ='" + QUOTATION_CODE + "')  " + Col_supp_Alias + " on " + Col_supp_Alias + ".ITEM_REF_CODE = a.ITEM_REF_CODE ";
                if (col_id == 18)
                {
                    str += " and " + Col_supp_Alias + ".QUOTATION_CODE=a.QUOTATION_CODE ";
                }





                strTables.Append(str);
                strSql.Append(Col_supp_Alias);
                strSql.Append(".* ");
                // strSql.Append(" (select isnull(Description,'Original')as  Description from PURC_LIB_SYSTEM_PARAMETERS where Code=isnull(a.Item_Type,154) ) as ItemType ");

                GridBoundColumn boundColumn;
                boundColumn = new GridBoundColumn();
                boundColumn.HeaderText = "Unit Price";
                boundColumn.DataField = Col_supp_Alias + "_Rate";
                boundColumn.UniqueName = Col_supp + "_Rate";
                boundColumn.DataFormatString = "{0:F2}";
                boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                boundColumn.ItemStyle.CssClass = BackColor;
                boundColumn.HeaderStyle.Width = 70;
                boundColumn.ItemStyle.Width = 70;

                rgdQuatationInfo.MasterTableView.Columns.Add(boundColumn);

                boundColumn = new GridBoundColumn();
                boundColumn.HeaderText = "Discount";
                boundColumn.DataField = Col_supp_Alias + "_Discount";
                boundColumn.UniqueName = Col_supp + "_Discount";
                boundColumn.DataFormatString = "{0:F2}";

                boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                boundColumn.ItemStyle.CssClass = BackColor;
                boundColumn.HeaderStyle.Width = 70;
                boundColumn.ItemStyle.Width = 70;
                rgdQuatationInfo.MasterTableView.Columns.Add(boundColumn);

                boundColumn = new GridBoundColumn();
                boundColumn.HeaderText = "Amount";
                boundColumn.DataField = Col_supp_Alias + "_Amount";
                boundColumn.UniqueName = Col_supp + "_Amount";
                boundColumn.DataFormatString = "{0:F2}";

                boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                boundColumn.ItemStyle.CssClass = BackColor;
                boundColumn.HeaderStyle.Width = 70;
                boundColumn.ItemStyle.Width = 70;
                rgdQuatationInfo.MasterTableView.Columns.Add(boundColumn);

                boundColumn = new GridBoundColumn();
                boundColumn.HeaderText = "Lead days";
                boundColumn.DataField = Col_supp_Alias + "_Lead_Time";
                boundColumn.UniqueName = Col_supp + "_Lead_Time";
                //  boundColumn.DataFormatString = "{0:F2}";

                boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                boundColumn.ItemStyle.CssClass = BackColor;
                boundColumn.HeaderStyle.Width = 70;
                boundColumn.ItemStyle.Width = 70;
                rgdQuatationInfo.MasterTableView.Columns.Add(boundColumn);


                boundColumn = new GridBoundColumn();
                boundColumn.HeaderText = "ItemType";
                boundColumn.DataField = Col_supp_Alias + "_ItemType";
                boundColumn.UniqueName = Col_supp + "_ItemType";
                boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                boundColumn.ItemStyle.CssClass = BackColor;
                boundColumn.HeaderStyle.Width = 70;
                boundColumn.ItemStyle.Width = 70;
                rgdQuatationInfo.MasterTableView.Columns.Add(boundColumn);



                GridTemplateColumn templateColumnRemark = new GridTemplateColumn();
                templateColumnRemark.HeaderText = "Remark";
                templateColumnRemark.DataField = Col_supp_Alias + "_Remark";
                templateColumnRemark.ItemTemplate = new DataGridTemplateImage(ListItemType.Item, Col_supp, Col_supp + "_Remark");
                templateColumnRemark.UniqueName = Col_supp + "_img";
                templateColumnRemark.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                templateColumnRemark.ItemStyle.CssClass = BackColor;
                templateColumnRemark.ItemStyle.Width = 70;
                templateColumnRemark.HeaderStyle.Width = 70;
                rgdQuatationInfo.MasterTableView.Columns.Add(templateColumnRemark);

                GridTemplateColumn templateColumn = new GridTemplateColumn();
                templateColumn.UniqueName = "TempChk";
                templateColumn.HeaderTemplate = new DataGridTempla(ListItemType.Header, Col_supp, "Select", "");
                templateColumn.ItemTemplate = new DataGridTempla(ListItemType.Item, Col_supp, Col_supp, Col_supp_Alias + "_Status");
                templateColumn.UniqueName = Col_supp + "_chk";
                templateColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                templateColumn.ItemStyle.CssClass = BackColor;
                templateColumn.HeaderStyle.Width = 70;
                templateColumn.ItemStyle.Width = 70;
                rgdQuatationInfo.MasterTableView.Columns.Add(templateColumn);


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
                if (MinValues == 0 || MinValues > Convert.ToDecimal(dataItem["Supp_Tot_Amt"].Text.ToString()))
                {
                    //HiddenChepSupp.Value = Col_supp + "_chk";
                    HiddenChepSupp.Value = Col_supp;
                    MinValues = Convert.ToDecimal(dataItem["Supp_Tot_Amt"].Text.ToString());
                    sel_Id = col_id;
                }

            }
        }
        strSql.Append(" ");
        strSql.Append(strTables);
        Session["SubQuerry"] = strSql.ToString();
        strSql.Append("where  a.Document_code ='" + Request.QueryString["Document_Code"].ToString() + "' and a.active_status=1 order by a.ITEM_SERIAL_NO");

        SuppQtnCodesItemTypes = SuppQtnCodesItemTypes.Remove(SuppQtnCodesItemTypes.Length - 1, 1);
        dtItemsTypes = BLL_PURC_Common.GET_ItemTypeAll(SuppQtnCodesItemTypes);

        DataTable dt = new DataTable();
        DataTable dtQuatationInfo = new DataTable();
        ViewState["SuppSelforEval"] = suppSel;
        ViewState["supplierList"] = arrySuppName;
        ViewState["supplierListInShort"] = arrSuppNameInShort;
        ViewState["EvaluateTable"] = dtQuatationInfo;

        count = 0;
        // optEval.Attributes.Add("Onclick", "return CalculateByEvalOpt('" + EvalOpt + "'," +sel_Id+");");



        Session["supplist"] = supplist.ToString();



        TechnicalBAL objtechBAL = new TechnicalBAL();
        string FinalQuery = strSql.ToString();
        dtQuatationInfo = objtechBAL.GetTable(FinalQuery);
        rgdQuatationInfo.DataSource = dtQuatationInfo;
        rgdQuatationInfo.DataBind();
        Session["QuatationInfo"] = dtQuatationInfo;
        Session["GeneratedQuery"] = FinalQuery;

        foreach (GridDataItem Item in rgdQuatationInfo.MasterTableView.Items)
        {
            int suppCount = (int)ViewState["SuppSelforEval"];
            string[] arrSupp = (string[])ViewState["supplierList"];



            count++;

            Label longDescpt = (Label)Item.FindControl("lblLongDesc");
            if (longDescpt.ToolTip == "1")
            {
                ((HyperLink)Item.FindControl("lblItemDesc")).CssClass = "NewItem";
                Item.Cells[8].BackColor = System.Drawing.Color.Yellow;

            }

        }

        ViewState["EvaluateTable"] = dtQuatationInfo;
        int column = 14;
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

    protected RadGrid grid;

    [System.Web.Services.WebMethod]
    public void SaveQuotedItems(string bln)
    {
        try
        {
            string id = bln.ToString();

        }
        catch //(Exception ex)
        {

        }
        finally
        {

        }
    }

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

    protected void rgdQuatationInfo_ItemDataBound(object sender, GridItemEventArgs e)
    {

        try
        {

            string[] arrSupp = (string[])ViewState["supplierList"];
            string[] arrSuppCode = ViewState["suppliercode"].ToString().Split(',');
            string[] arrQtnCode = ViewState["quotationcode"].ToString().Split(',');


            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                int RemarkID = 24;
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
                        ((Image)(e.Item.FindControl(supp + "_img"))).Attributes.Add("onmouseover", "js_ShowToolTip('" + ((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray[RemarkID] + "',event,this)");

                        if (((System.Data.DataRowView)(e.Item.DataItem)).Row.ItemArray[RemarkID].ToString() == "")
                        {
                            ((Image)(e.Item.FindControl(supp + "_img"))).ImageUrl = "~/PURCHASE/Image/remarknone.png";

                        }

                    }
                    RemarkID += 10;
                    RateID += 7;
                    suppno++;
                }

                //if (((HyperLink)(e.Item.FindControl("lblItemDesc"))).Text.Length > 20)
                //{

                //    ((HyperLink)(e.Item.FindControl("lblItemDesc"))).Text = ((HyperLink)(e.Item.FindControl("lblItemDesc"))).Text.Substring(0, 30) + "..";
                //}

                Label longDescpt = (Label)e.Item.FindControl("lblLongDesc");
                ((HyperLink)(e.Item.FindControl("lblItemDesc"))).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Short Description: " + ((Label)e.Item.FindControl("lblshortDesc")).Text + "<hr>" + "Long Description: " + longDescpt.Text + "<hr>" + "Comment: " + ((Label)e.Item.FindControl("lblComments")).Text + "] body=[" + "Drw no.:" + e.Item.Cells[6].Text + " Part no.:" + e.Item.Cells[7].Text + "]");



            }





        }
        catch //(Exception ex)
        {

        }


    }

    protected void rgdQuatationInfo_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {

    }

    protected void rgdSupplierInfo_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        BindQuatationSendBySupplier();

    }


    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            info.MergedColumns.Clear();
            info.StartColumns.Clear();
            info.Titles.Clear();

            BindGridOnRetriveButtonClick();
            rgdQuatationInfo.GridLines = GridLines.Both;

            //rgdQuatationInfo.ExportSettings.ExportOnlyData = true;    
            //rgdQuatationInfo.MasterTableView.ExportToExcel();
            // GridViewExportUtil.Export("aaa.xls", rgdQuatationInfo, "");


            RadGrid rgdExp = rgdQuatationInfo;
            string[] arrSupp = (string[])ViewState["supplierList"];
            foreach (GridDataItem item in rgdExp.Items)
            {

                int ColumnIDToHide = 18;// 18,19,20 for the first supler and same for other supplier (+7)
                int i = 0;

                foreach (string supp in arrSupp)
                {
                    if (supp.Trim() != "")
                    {

                        item.Cells[ColumnIDToHide + 1].Text = "";
                        item.Cells[ColumnIDToHide + 2].Text = "";
                        ColumnIDToHide = ColumnIDToHide + 7;
                    }
                    i++;
                }


            }

            rgdExp.MasterTableView.ExportToExcel();

        }
        catch
        {

        }
        finally
        {
        }
    }
    public string GetPageText(string url)
    {
        string htmlText = string.Empty;
        string FILE_NAME = Server.MapPath("Image\\test.xml"); //"c:\\test.xml";

        try
        {

            HttpWebRequest requestIP = (HttpWebRequest)WebRequest.Create(url);
            requestIP.Timeout = 10000;
            using (HttpWebResponse responseIP = (HttpWebResponse)requestIP.GetResponse())
            {
                using (Stream streamIP = responseIP.GetResponseStream())
                {
                    using (StreamReader readerText = new StreamReader(streamIP))
                    {
                        htmlText = readerText.ReadToEnd();
                        string text = htmlText;

                        StreamWriter writer = new StreamWriter(FILE_NAME);
                        writer.Write(text);
                        writer.Close();
                    }
                }
            }
        }
        finally
        {
        }
        return htmlText;
    }


    protected void gvApprovalHistory_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //call the method for custom rendering the columns headers	on row created event

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.SetRenderMethodDelegate(RenderHeaderApprHistory);
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
            if (!infoAppr.MergedColumnsAppr.Contains(i))
            {
                cell.Attributes["rowspan"] = "2";

                cell.RenderControl(output);
            }
            else //render merged columns common title
                if (infoAppr.StartColumnsAppr.Contains(i))
                {


                    output.Write(string.Format("<th align='center'  colspan='{0}'>{1}</th>",
                             infoAppr.StartColumnsAppr[i], infoAppr.TitlesAppr[i]));


                }
        }

        //close the first row	
        output.RenderEndTag();
        //set attributes for the second row
        //grid.HeaderStyle.AddAttributesToRender(output);
        //start the second row
        output.RenderBeginTag("tr");



        //render the second row (only the merged columns)
        for (int i = 0; i < infoAppr.MergedColumnsAppr.Count; i++)
        {
            //if qtn eval 

            TableCell cell = (TableCell)container.Controls[infoAppr.MergedColumnsAppr[i]];

            cell.CssClass = "HeaderStyle-css";
            cell.RenderControl(output);

        }

        infoAppr.MergedColumnsAppr.Clear();
        infoAppr.StartColumnsAppr.Clear();
        infoAppr.TitlesAppr.Clear();
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

    [Serializable]
    private class MergedColumnsInfoAppr
    {
        // indexes of merged columns
        public List<int> MergedColumnsAppr = new List<int>();
        // key-value pairs: key = the first column index, value = number of the merged columns
        public Hashtable StartColumnsAppr = new Hashtable();
        // key-value pairs: key = the first column index, value = common title of the merged columns 
        public Hashtable TitlesAppr = new Hashtable();

        //parameters: the merged columns indexes, common title of the merged columns 
        public void AddMergedColumnsAppr(int[] columnsIndexes, string title)
        {
            MergedColumnsAppr.AddRange(columnsIndexes);
            StartColumnsAppr.Add(columnsIndexes[0], columnsIndexes.Length);
            TitlesAppr.Add(columnsIndexes[0], title);
        }
    }

    private MergedColumnsInfoAppr infoAppr
    {
        get
        {
            if (ViewState["infoAppr"] == null)
                ViewState["infoAppr"] = new MergedColumnsInfoAppr();
            return (MergedColumnsInfoAppr)ViewState["infoAppr"];
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
               

            }
        }

        if (gvApprovalHistory.Columns.Count > 0)
        {
            foreach (GridViewRow gr in gvApprovalHistory.Rows)
            {

                gr.Cells[1].Width = 140;

            }
        }

        string[] strColumnIndex = HeaderToHide.Split(new char[] { ',' });
        string[] strHeaderName = HeaderSupplier.Split(new char[] { ',' });
        int index = 0;
        foreach (string item in strColumnIndex)
        {
            if (item != "")
            {

                infoAppr.AddMergedColumnsAppr(new int[] { int.Parse(item) + 1, int.Parse(item) + 2 }, strHeaderName[index]);
            }
            index++;
        }

    }


    protected void rgdQuatationInfo_ItemCreated(object sender, GridItemEventArgs e)
    {

        if (e.Item.ItemType == GridItemType.Header)
        {
            e.Item.SetRenderMethodDelegate(RenderHeader);

        }

    }


    protected void rgdSupplierInfo_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            ((Image)item.FindControl("imgQuRemark")).Attributes.Add("onmouseover", "js_ShowToolTip('" + ((Image)item.FindControl("imgQuRemark")).AlternateText.Replace("'","") + "',event,this)");
            ((Label)item.FindControl("lblPkg")).Attributes.Add("onmouseover", "js_ShowToolTip('" + ((Label)item.FindControl("lblPkgRs")).Text.Replace("'", "") + "',event,this)");
            ((Label)item.FindControl("lblOtherCost")).Attributes.Add("onmouseover", "js_ShowToolTip('" + ((Label)item.FindControl("lblOtherCostRs")).Text.Replace("'", "") + "',event,this)");
        }
    }

    public class DataGridTempla : System.Web.UI.Page, ITemplate
    {
        ListItemType templateType;
        string columnName;
        string Header;
        string DataFieldName;

        public DataGridTempla(ListItemType type, string colname, string HeaderText, string DataField)
        {
            templateType = type;
            columnName = colname;
            Header = HeaderText;
            DataFieldName = DataField;
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
                    chkb.Enabled = false;

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
                    chkb.Enabled = false;
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
            GridDataItem container = (GridDataItem)chk.NamingContainer;
            chk.Checked = ((DataRowView)container.DataItem)[DataFieldName].ToString() == "True" ? true : false;
        }


        public void chkb_Header(object sender, EventArgs e)
        {


            CheckBox cb = (CheckBox)sender;
            //string va = cb.ID;
            //cb.ID = "tempID";
            //string value = cb.Parent.Parent.Parent.Parent.Parent.Parent.ID;

            RadGrid RadGridQua = (RadGrid)cb.Parent.Parent.Parent.Parent.Parent.Parent;

            RadGridQua.DataSource = (DataTable)Session["QuatationInfo"];

            if (cb.Checked)
            {

                foreach (GridItem dataItem in RadGridQua.MasterTableView.Items)
                {
                    if (dataItem is GridDataItem)
                    {
                        GridDataItem dataItem1 = dataItem as GridDataItem;
                        CheckBox chk = (CheckBox)(dataItem1.FindControl("Select") as CheckBox);

                        chk.Checked = true;

                    }
                }
            }
            else
            {
                foreach (GridDataItem dataItem in RadGridQua.MasterTableView.Items)
                {
                    CheckBox chk = (CheckBox)(dataItem.FindControl("Select") as CheckBox);
                    chk.Checked = false;
                }
            }
        }


        public void chkb_Items(object sender, EventArgs e)
        {

        }




    }
    public class DataGridTemplateImage : System.Web.UI.Page, ITemplate
    {
        ListItemType templateType;
        string columnName;
        string strRemark;

        public DataGridTemplateImage(ListItemType type, string colname, string strRemarkFrom)
        {
            templateType = type;
            columnName = colname;
            strRemark = strRemarkFrom;
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

                    container.Controls.Add(img);
                    container.Controls.Add(lc);
                    break;

            }

        }

    }
    public class DataGridTemplateDropDown : System.Web.UI.Page, ITemplate
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

}


