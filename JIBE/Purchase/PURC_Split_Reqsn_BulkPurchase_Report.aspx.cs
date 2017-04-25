using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using System.Collections.Generic;
using System.Collections;


public partial class Purchase_PURC_Split_Reqsn_BulkPurchase_Report : System.Web.UI.Page
{
    int lastFixedColumnID = 7;
    int remainingQtyColumnID = 4;
    int remainingPriceColumnID = 7;
    int totalQtyColumnID = 3;
    int unitPriceColumnID = 5;
    protected void Page_Load(object sender, EventArgs e)
    {
        gvItemsSplit.DataSource = BLL_PURC_Common.Get_ReqsnItems_Split_ToVessel_Report(Request.QueryString["ReqsnCode"]);
        gvItemsSplit.DataBind();
        BindRequisitionInfo();
        String msgretv = String.Format("setTimeout(calculatePriceAndQty_Onload,1000);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinal", msgretv, true);

    }



    private void RenderHeader(HtmlTextWriter output, Control container)
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


                    output.Write(string.Format("<th align='center' style='border:1px solid #cccccc;border-collapse:collapse;background:url(../Images/gridheaderbg-image.png) left 0px repeat-x'  colspan='{0}'>{1}</th>",
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

            cell.CssClass = "HeaderStyle-css-bulkreport";
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
    protected void gvItemsSplit_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.SetRenderMethodDelegate(RenderHeader);

        }
    }
    protected void gvItemsSplit_DataBound(object sender, EventArgs e)
    {
        int igr = 0;
        foreach (TableCell cl in gvItemsSplit.HeaderRow.Cells)
        {
            if (igr > 7)
            {
                if ((igr % 2) == 0)
                {
                    string headername = cl.Text.Split('_')[1];
                    info.AddMergedColumns(new int[] { igr, igr + 1 }, headername);
                }
                cl.Text = cl.Text.Split('_')[0];

            }
            igr++;
        }


        gvItemsSplit.HeaderRow.Cells[0].Visible = false;
        foreach (GridViewRow gr in gvItemsSplit.Rows)
        {
            int i = 0;

            string totalQty = "0";
            string unitPrice = "0";

            gr.Cells[0].Visible = false;
            gr.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            gr.Cells[2].Width = 200;
            gr.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            gvItemsSplit.HeaderRow.Cells[remainingPriceColumnID].Width = 50;
            gvItemsSplit.HeaderRow.Cells[remainingQtyColumnID].Width = 50;
            Label lblqty = new Label();
            Label lblprice = new Label();

            foreach (TableCell cell in gr.Cells)
            {
                if (i == totalQtyColumnID)
                {
                    totalQty = cell.Text;
                    Label lbl = new Label();
                    lbl.ID = "totqty" + i.ToString();
                    lbl.Text = cell.Text;
                    cell.Controls.Add(lbl);
                }

                if (i == unitPriceColumnID)
                {
                    unitPrice = cell.Text;
                    Label lbl = new Label();
                    lbl.ID = "unitprice" + i.ToString();
                    lbl.Text = cell.Text;
                    cell.Controls.Add(lbl);
                }


                if (i == remainingQtyColumnID)
                {
                    lblqty.ID = "qty" + i.ToString();
                    lblqty.Text = cell.Text;
                    cell.Controls.Add(lblqty);
                    cell.CssClass = "remainingQty";
                }

                if (i == remainingPriceColumnID)
                {

                    lblprice.ID = "price" + i.ToString();
                    lblprice.Text = cell.Text;
                    cell.Controls.Add(lblprice);
                    cell.CssClass = "remainingPrice";
                }



                if (i > lastFixedColumnID)
                {


                    Label txt = new Label();
                    txt.Text = cell.Text.Replace("&nbsp;", "");
                    txt.ID = i.ToString();

                    cell.Controls.Add(txt);
                    cell.CssClass = "txtord";


                }

                i++;

            }
        }
    }

    private void BindRequisitionInfo()
    {

        try
        {
            System.Data.DataTable dtReqInfo = new System.Data.DataTable();
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                dtReqInfo = objTechService.SelectRequistionToSupplier(Request.QueryString["ReqsnCode"].ToString(), Request.QueryString["Document_Code"].ToString());
                lblReqNo.Text = dtReqInfo.DefaultView[0]["REQUISITION_CODE"].ToString();

                //lblReqNo.NavigateUrl = "RequisitionSummary.aspx?REQUISITION_CODE=" + Request.QueryString["Requisitioncode"].ToString() + "&Document_Code=" + Request.QueryString["Document_Code"].ToString() + "&Vessel_Code=" + Request.QueryString["Vessel_Code"].ToString() + "&Dept_Code=" + dtReqInfo.DefaultView[0]["DEPARTMENT"].ToString() + "&" + 1.ToString();
                lblVessel.Text = dtReqInfo.DefaultView[0]["Vessel_Name"].ToString();
                lblCatalog.Text = dtReqInfo.DefaultView[0]["Name_Dept"].ToString();
                lblToDate.Text = dtReqInfo.DefaultView[0]["requestion_Date"].ToString();
                lblTotalItem.Text = dtReqInfo.DefaultView[0]["TOTAL_ITEMS"].ToString();
                ViewState["vessel_code"] = dtReqInfo.Rows[0]["vessel_code"].ToString();

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
}
