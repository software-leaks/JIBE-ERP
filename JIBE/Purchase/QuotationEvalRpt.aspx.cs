/*Author:Pranali
  Purpose:To get Quotation details of supplier with Chepaest Supplier Details
  Date:27April2015
 */


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
using System.Linq;

public partial class Purchase_QuotationEvalRpt : System.Web.UI.Page
{
    double selectedItemAmt = 0;
    int selectedItemCount = 0;
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            
            BindSupplier();
            BindRequisitionInfo();
            BindQuatationSendBySupplier();
        }
    }
    #endregion

    #region General Functions

    #region Approval History Functions
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
    #endregion 

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
                //lblToDate.Text = dtReqInfo.DefaultView[0]["requestion_Date"].ToString();
                //lblTotalItem.Text = dtReqInfo.DefaultView[0]["TOTAL_ITEMS"].ToString();
                //lblReqDate.Text = dtReqInfo.DefaultView[0]["RFQ_Date"].ToString();
                lbtnPurchaserRemark.Attributes.Add("onmouseover", "js_ShowToolTip('" + dtReqInfo.DefaultView[0]["SentToSupdt_Remark"].ToString() + "',event,this)");
                lblReqNo.NavigateUrl = "RequisitionSummary.aspx?REQUISITION_CODE=" + Request.QueryString["Requisitioncode"].ToString() + "&Document_Code=" + Request.QueryString["Document_Code"].ToString() + "&Vessel_Code=" + Request.QueryString["Vessel_Code"].ToString() + "&Dept_Code=" + dtReqInfo.DefaultView[0]["DEPARTMENT"].ToString() + "&" + 1.ToString();
                lblITEMSYSTEMCODE.Value = dtReqInfo.DefaultView[0]["ITEM_SYSTEM_CODE"].ToString();
                lbtnPurchaserRemark.Attributes.Add("onmouseover", "js_ShowToolTip('" + dtReqInfo.DefaultView[0]["SentToSupdt_Remark"].ToString() + "',event,this)");
                lblReqNo.NavigateUrl = "RequisitionSummary.aspx?REQUISITION_CODE=" + Request.QueryString["Requisitioncode"].ToString() + "&Document_Code=" + Request.QueryString["Document_Code"].ToString() + "&Vessel_Code=" + Request.QueryString["Vessel_Code"].ToString() + "&Dept_Code=" + dtReqInfo.DefaultView[0]["DEPARTMENT"].ToString() + "&" + 1.ToString();


            }
            ViewState["dtRequistion"] = dtReqInfo;
            
            lblVesselCode.Value = Request.QueryString["Vessel_Code"].ToString();
            BLL_PURC_Purchase objHistory = new BLL_PURC_Purchase();
            gvApprovalHistory.DataSource = objHistory.Get_Approver_History(Request.QueryString["Requisitioncode"].ToString().Trim());
            gvApprovalHistory.DataBind();

            info.MergedColumns.Clear();
            info.StartColumns.Clear();
            info.Titles.Clear();

            

            dlistPONumber.DataSource = BLL_PURC_Common.Get_PONumbers(Request.QueryString["Requisitioncode"]);
            dlistPONumber.DataBind();

            
           
        }
        catch (Exception ex)
        {
        }
        finally
        {

        }

    }
    private void BindSupplier()
    {
        BLL_PURC_Purchase ObjQuote = new BLL_PURC_Purchase();
        
        try
        {
            string Document_Code = Request.QueryString["Document_Code"].ToString();
            string Requisitioncode = Request.QueryString["Requisitioncode"].ToString();
            string Vessel_Code = Request.QueryString["Vessel_Code"].ToString();
            DataSet dsQuote =ObjQuote.GetSupplierQuote(Requisitioncode,Vessel_Code,Document_Code);
            grdQuoteSupp.DataSource = dsQuote;
            ViewState["Quote"] = dsQuote.Tables[1];
            grdQuoteSupp.DataBind();
            for (int i = 0; i < grdQuoteSupp.Rows.Count; i++)
            {
                /*Item Description Link to ItemHistory*/
                HyperLink lnkItemDesc = new HyperLink();
                lnkItemDesc.NavigateUrl = "~/Purchase/Item_History.aspx?vessel_code=" + Convert.ToString(Request.QueryString["Vessel_Code"]) + "&item_ref_code=" + grdQuoteSupp.Rows[i].Cells[2].Text;
                lnkItemDesc.ToolTip = grdQuoteSupp.Rows[i].Cells[6].Text;
                lnkItemDesc.Target = "_blank";
                lnkItemDesc.Text = grdQuoteSupp.Rows[i].Cells[5].Text;
                grdQuoteSupp.Rows[i].Cells[5].Controls.Add(lnkItemDesc);
                
                
            }
        }
        catch
        {
        }
    }
    private void BindQuatationSendBySupplier()
    {
        try
        {
            DataTable dtsuppInfo = new DataTable();
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                dtsuppInfo = objTechService.SelectQuatationSendBySupplier_Report(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString());
                rgdSupplierInfo.DataSource = dtsuppInfo;
                rgdSupplierInfo.DataBind();
                //if (dtsuppInfo.Rows.Count > 0)
                //    lblQuotDueDate.Text = dtsuppInfo.DefaultView[0]["Quotation_Due_Date"].ToString();

            }
        }
        catch (Exception ex)
        {
        }

    }
    private int GetColumnIndexByName(GridView grid, string name)
    {
        if (name != string.Empty && name != "&nbsp;")
        {
            for (int i = 0; i < grid.HeaderRow.Cells.Count; i++)
            {
                if (grid.HeaderRow.Cells[i].Text.ToLower().Trim() == name.ToLower().Trim())
                {
                    return i;
                }
            }
        }
        return -1;
    }


    
    #endregion

    #region Gridview Functions
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
    
    protected void grdQuoteSupp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ///<Summarry>>
            /// Note:Gridview Displays Supplier Price / Lead Time [10.00/2]
            ///</Summarry>>
            int ic = 0;
            DataTable dtQuote = (DataTable)ViewState["Quote"];
            var SuplierCodeName = dtQuote.AsEnumerable()
                           .Select(row => new
                           {
                               Supp_Code = row.Field<string>("SUPPLIER_CODE"),
                               Supp_Name = row.Field<string>("SUPPLIER_NAME")
                           })
                           .Distinct();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int k = 0; k < grdQuoteSupp.HeaderRow.Cells.Count; k++)
                {
                    string RowsupplierName = grdQuoteSupp.HeaderRow.Cells[(grdQuoteSupp.HeaderRow.Cells.Count - 2)].Text;
                    string ColsupplierName = grdQuoteSupp.HeaderRow.Cells[k].Text;
                    string Orderedsupplier = (e.Row.Cells[grdQuoteSupp.HeaderRow.Cells.Count - 2].Text);
                    string CheapSupplier = (e.Row.Cells[grdQuoteSupp.HeaderRow.Cells.Count - 4].Text);

                    //string CheapSupplierCode = e.Row.Cells[e.Row.Controls.Count - 2].Text;
                    //string OrderSupllierCode = e.Row.Cells[e.Row.Controls.Count - 4].Text;

                    foreach (var sup in SuplierCodeName)
                    {
                        if (sup.Supp_Code == ColsupplierName)
                        {
                            grdQuoteSupp.HeaderRow.Cells[k].ToolTip = sup.Supp_Name;
                        }
                    }
                    if (Orderedsupplier == CheapSupplier)
                    {
                        if (k >= 10)
                        {
                            if (ColsupplierName == Orderedsupplier)
                            {
                                //e.Row.Cells[k].ForeColor = System.Drawing.Color.OrangeRed;
                                e.Row.Cells[k].ForeColor = System.Drawing.Color.Green;
                                if (e.Row.Cells[k].Text != "" && e.Row.Cells[k].Text != "&nbsp;")
                                {
                                    selectedItemAmt += Convert.ToDouble(e.Row.Cells[k].Text.Split('/')[0]);
                                    selectedItemCount += 1;
                                }
                            }
                            //e.Row.Cells[grdQuoteSupp.HeaderRow.Cells.Count - 4].ForeColor = System.Drawing.Color.OrangeRed;
                            //e.Row.Cells[grdQuoteSupp.HeaderRow.Cells.Count - 2].ForeColor = System.Drawing.Color.OrangeRed;
                        }


                    }
                    else
                    {
                        if (k >= 10)
                        {
                            if (ColsupplierName == Orderedsupplier)
                            {
                                ic = Convert.ToInt32(GetColumnIndexByName(grdQuoteSupp, ColsupplierName));
                                string abc = e.Row.Cells[ic].Text;
                                e.Row.Cells[ic].ForeColor = System.Drawing.Color.Purple;
                                
                                //e.Row.Cells[grdQuoteSupp.HeaderRow.Cells.Count - 4].ForeColor = System.Drawing.Color.Blue;

                            }
                            if (ColsupplierName == CheapSupplier)
                            {
                                ic = Convert.ToInt32(GetColumnIndexByName(grdQuoteSupp, ColsupplierName));
                                string abc = e.Row.Cells[ic].Text;
                                e.Row.Cells[ic].ForeColor = System.Drawing.Color.CornflowerBlue;
                                //e.Row.Cells[ic].ForeColor = System.Drawing.Color.Blue;
                                //e.Row.Cells[grdQuoteSupp.HeaderRow.Cells.Count - 2].ForeColor = System.Drawing.Color.Purple;//FromName("#373737");
                            }
                        }
                    }

                    if (e.Row.Cells[10].Text != "" && e.Row.Cells[10].Text != "&nbsp;")
                    {
                        if (k > 10 && k < (grdQuoteSupp.HeaderRow.Cells.Count - 4))
                        {
                            List<string> lstRemarks = e.Row.Cells[10].Text.Split(new[] { "##$" }, StringSplitOptions.None).ToList();
                            foreach (var supp in lstRemarks)
                            {
                                if (ColsupplierName == Convert.ToString(supp.Split(new[] { "@@$" }, StringSplitOptions.None)[1]))
                                {
                                    Image ImgRmrk = new Image();
                                    Label lbl = new Label();
                                    //ImgRmrk.ToolTip = "Quotation Remarks";
                                    ImgRmrk.ImageUrl = "~/purchase/Image/view1.gif";
                                    ImgRmrk.ImageAlign = ImageAlign.Right;
                                    ImgRmrk.Attributes.Add("onclick", "this.enabled=false;");
                                    ImgRmrk.AlternateText = supp.Split(new[] { "@@$" }, StringSplitOptions.None)[0];
                                    ImgRmrk.Attributes.Add("onmouseover", "js_ShowToolTip('" + ImgRmrk.AlternateText.Replace("'", "") + "',event,this)");

                                    lbl.Text = e.Row.Cells[k].Text;
                                    lbl.ForeColor = e.Row.Cells[k].ForeColor;
                                    lbl.Attributes.Add("onmouseover", "js_ShowToolTip('" + supp.Split(new[] { "@@$" }, StringSplitOptions.None)[0].Replace("'", "") + "',event,this)");
                                    e.Row.Cells[k].Controls.Add(lbl);
                                    e.Row.Cells[k].Controls.Add(new LiteralControl("&nbsp;"));
                                    
                                    e.Row.Cells[k].Controls.Add(ImgRmrk);
                                    break;
                                }
                            }

                        }
                    }
                    if (k >= 11)
                    {

                        e.Row.Cells[k].HorizontalAlign = HorizontalAlign.Right;
                    }
                    if ((e.Row.Cells[e.Row.Controls.Count - 1].Text) != "" && (e.Row.Cells[e.Row.Controls.Count - 1].Text) != "&nbsp;")
                    {
                        e.Row.Cells[e.Row.Controls.Count - 2].ToolTip = Convert.ToString(e.Row.Cells[e.Row.Controls.Count - 1].Text);
                    }
                    if (e.Row.Cells[e.Row.Controls.Count - 3].Text != "" && (e.Row.Cells[e.Row.Controls.Count - 1].Text) != "&nbsp;")
                    {
                        e.Row.Cells[e.Row.Controls.Count - 4].ToolTip = Convert.ToString(e.Row.Cells[e.Row.Controls.Count - 3].Text);
                    }
                    e.Row.Cells[k].Font.Name = "Tahoma";
                    e.Row.Cells[k].Font.Size = 7;
                    e.Row.Cells[k].Font.Bold = true;
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
    protected void grdQuoteSupp_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            #region Hide Comlumns
            /*Hide Unwanted columns*/
            e.Row.Controls[1].Visible = false;
            e.Row.Controls[2].Visible = false;
            e.Row.Controls[6].Visible = false;
            e.Row.Controls[10].Visible = false;
            e.Row.Controls[e.Row.Controls.Count - 1].Visible = false;
            e.Row.Controls[e.Row.Controls.Count - 3].Visible = false;
            #endregion
        }
    }

    #endregion
    
    #region RadGrid Functions
    protected void rgdSupplierInfo_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        BindQuatationSendBySupplier();

    }
    #region rgdSupplierInfo_ItemDataBound
    protected void rgdSupplierInfo_ItemDataBound(object sender, GridItemEventArgs e)
    {
        BLL_PURC_Purchase objPurch = new BLL_PURC_Purchase();
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            ((Image)item.FindControl("imgQuRemark")).Attributes.Add("onmouseover", "js_ShowToolTip('" + ((Image)item.FindControl("imgQuRemark")).AlternateText.Replace("'", "") + "',event,this)");
            ((Label)item.FindControl("lblPkg")).Attributes.Add("onmouseover", "js_ShowToolTip('" + ((Label)item.FindControl("lblPkgRs")).Text.Replace("'", "") + "',event,this)");
            ((Label)item.FindControl("lblOtherCost")).Attributes.Add("onmouseover", "js_ShowToolTip('" + ((Label)item.FindControl("lblOtherCostRs")).Text.Replace("'", "") + "',event,this)");
            String strTemp = (item["SUPPLIER"]).Text;

            if (((CheckBox)item.FindControl("chkQuaEvaluated")).Checked)
            {
                selectedItemAmt = Convert.ToDouble(item["Supp_Tot_Amt"].Text) - Convert.ToDouble(((Label)item.FindControl("lblPkg")).Text) - Convert.ToDouble(item["Freight_Cost"].Text);
                ((Label)item.FindControl("txtAmount")).Text = Math.Round(selectedItemAmt).ToString("f2");
            }
            else
            {
                ((Label)item.FindControl("txtTotalItem")).Text ="0";
            }
            DataTable dtAttachment = objPurch.Purc_Get_Reqsn_Attachments_Supplier(Request.QueryString["Requisitioncode"].ToString(), Convert.ToInt32(Request.QueryString["Vessel_Code"].ToString()), strTemp);
            if (dtAttachment.Rows.Count > 0)
            {
                ((ImageButton)item.FindControl("ImgAttachment")).Visible = true;
            }
            else 
            {
                ((ImageButton)item.FindControl("ImgAttachment")).Visible = false;
            }

        }
    }
    #endregion

    #endregion

    

}