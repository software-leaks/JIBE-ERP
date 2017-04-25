using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using System.Data;
public partial class Purchase_PURC_Split_Reqsn_BulkPurchase : System.Web.UI.Page
{
    int lastFixedColumnID = 7;
    int remainingQtyColumnID = 4;
    int remainingPriceColumnID = 7;
    int totalQtyColumnID = 3;
    int unitPriceColumnID = 5;


    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        //btnSaveAsDraft.Enabled = false;
        //btnSaveFinalize.Enabled = false;
        lblmsg.Text = "";

        if (!IsPostBack)
        {
            hlkViewFinalizedOrders.NavigateUrl = "~/purchase/PURC_Split_Reqsn_BulkPurchase_Report.aspx?ReqsnCode=" + Request.QueryString["ReqsnCode"] + "&document_code=" + Request.QueryString["document_code"];

            BLL_Infra_VesselLib objVSL = new BLL_Infra_VesselLib();
            DataTable dtVessel = objVSL.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            chkVesselList.DataSource = dtVessel;
            chkVesselList.DataBind();

            DataTable dtFinalizedVSL = BLL_PURC_Common.Get_Split_SavedVessel(Request.QueryString["ReqsnCode"], 1);
            dtFinalizedVSL.PrimaryKey = new DataColumn[] { dtFinalizedVSL.Columns[0]};

            ViewState["dtFinalizedVSL"] = dtFinalizedVSL;
         
            BindRequisitionInfo();

            chkVesselList.Items.FindByValue(ViewState["vessel_code"].ToString()).Enabled = false;

            SelectSavedVessels();

            BindSplittedItems();
        }

        if (IsPostBack)
        {
            #region to retain the data on post back

            GridViewRow gvheader = gvItemsSplit.HeaderRow;
            int hi = 0;
            foreach (TableCell hcell in gvheader.Cells)
            {
                if (hi > lastFixedColumnID)
                {
                    HiddenField hdf = new HiddenField();
                    Label lblvsl = new Label();
                    lblvsl.ID = "lblvsl" + hi.ToString();
                    hdf.ID = "hdf" + hi.ToString();
                    hdf.Value = hcell.Text.Split('_')[1];
                    hcell.Text = hcell.Text.Split('_')[0];
                    hcell.Controls.Add(hdf);
                    hcell.Controls.Add(lblvsl);
                }

                hi++;
            }


            foreach (GridViewRow gr in gvItemsSplit.Rows)
            {
                int i = 0;
                foreach (TableCell cell in gr.Cells)
                {

                    if (i == totalQtyColumnID)
                    {

                        Label lbl = new Label();
                        lbl.ID = "totqty" + i.ToString();
                        lbl.Text = cell.Text;
                        cell.Controls.Add(lbl);

                    }

                    if (i == unitPriceColumnID)
                    {

                        Label lbl = new Label();
                        lbl.ID = "unitprice" + i.ToString();
                        lbl.Text = cell.Text;
                        cell.Controls.Add(lbl);
                    }

                    if (i == remainingQtyColumnID)
                    {
                        Label lbl = new Label();
                        lbl.ID = "qty" + i.ToString();
                        lbl.Text = cell.Text;
                        cell.Controls.Add(lbl);
                        cell.CssClass = "remainingQty";
                    }
                    if (i == remainingPriceColumnID)
                    {
                        Label lbl = new Label();
                        lbl.ID = "price" + i.ToString();
                        lbl.Text = cell.Text;
                        cell.Controls.Add(lbl);
                        cell.CssClass = "remainingPrice";
                    }

                    if (i > lastFixedColumnID)
                    {
                        TextBox txt = new TextBox();

                        txt.ID = i.ToString();
                        txt.CssClass = "txtord";

                        cell.Controls.Add(txt);
                    }

                    i++;
                }

                //TableCell POcell = new TableCell();
                //Button btnSendPO = new Button();
                //btnSendPO.ID = "btnSendPO" + i.ToString();
                //btnSendPO.CommandArgument = i.ToString();
                //btnSendPO.Click += new EventHandler(btnSendPO_Click);
                //btnSendPO.Text = "Send PO";
                //POcell.Controls.Add(btnSendPO);

                //gr.Cells.Add(POcell);
            }


            #endregion
        }

        DataTable dtFinalizedVessels =(DataTable)ViewState["dtFinalizedVSL"];
        foreach (ListItem livsl in chkVesselList.Items)
        {
            if (dtFinalizedVessels.Rows.Contains(livsl.Value))
            {
                livsl.Attributes.Add("Style", "background-color :green;color:white;font-weight:bold;padding:3px");
                livsl.Attributes.Add("title", "Order has been finalized for this vessel");
            }

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
            btnSplitItems.Visible = false;

        }
        if (objUA.Edit == 0)
        {
            btnSaveAsDraft.Visible = false;

        }
        if (objUA.Approve == 0)
        {
            btnSaveFinalize.Visible = false;
        }
        if (objUA.Delete == 0)
        {


        }


    }

    protected void btnSplitItems_Click(object s, EventArgs e)
    {

        DataTable dtVessls = (DataTable)ViewState["vessellist"];

        foreach (ListItem item in chkVesselList.Items)
        {
            if (item.Selected == true)
            {
                //if (dtVessls.AsEnumerable().Where(r => r["vessel_id"].ToString() == item.Value).AsDataView().ToTable().Rows.Count == 0)
                DataRow dtRow = dtVessls.Rows.Find(item.Value);
                if (dtRow == null)
                {
                    dtRow = dtVessls.NewRow();
                    dtRow[0] = item.Value;
                    dtRow[1] = item.Text;
                    dtVessls.Rows.Add(dtRow);

                }
            }
            else
            {
                DataRow drdel = dtVessls.Rows.Find(item.Value);
                if (drdel != null)
                    dtVessls.Rows.Remove(drdel);

            }
        }
        ViewState["vessellist"] = dtVessls;
        BindSplittedItems();

        //btnSaveAsDraft.Enabled = true;
        //btnSaveFinalize.Enabled = true;




    }

    protected void BindSplittedItems()
    {
        DataSet dsitems = BLL_PURC_Common.Get_ReqsnItems_Split_ToVessel(Request.QueryString["ReqsnCode"], (DataTable)ViewState["vessellist"]);
        gvItemsSplit.DataSource = dsitems.Tables[0];
        gvItemsSplit.DataBind();

        dsitems.Tables[1].PrimaryKey = new DataColumn[] { dsitems.Tables[1].Columns["vessel_id"] };
        ViewState["vessellist"] = dsitems.Tables[1];
        //String msgretv = String.Format("setTimeout(calculatePriceAndQty_Onload,500);");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgonFinal", msgretv, true);
    }

    public void SelectSavedVessels()
    {
        DataTable dtVessls = (DataTable)ViewState["vessellist"];
        if (dtVessls == null)
        {
            dtVessls = new DataTable();
            dtVessls.Columns.Add("VESSEL_ID");
            dtVessls.Columns.Add("VESSEL_SHORTNAME");
            dtVessls.PrimaryKey = new DataColumn[] { dtVessls.Columns["VESSEL_ID"] };
        }


        dtVessls.Rows.Clear();
        chkVesselList.ClearSelection();
        DataTable dtSavedVSL = BLL_PURC_Common.Get_Split_SavedVessel(Request.QueryString["ReqsnCode"], 0);
        foreach (DataRow dr in dtSavedVSL.Rows)
        {
            DataRow dtRow = dtVessls.NewRow();
            dtRow[0] = dr["vessel_id"].ToString();
            dtRow[1] = dr["Vessel_Short_Name"];
            dtVessls.Rows.Add(dtRow);
        }
        ViewState["vessellist"] = dtVessls;
        foreach (ListItem li in chkVesselList.Items)
        {
            foreach (DataRow dr in dtSavedVSL.Rows)
            {
                if (dr["vessel_id"].ToString() == li.Value)
                {
                    chkVesselList.Items.FindByValue(li.Value).Selected = true;
                }
            }

        }

    }

    protected DataTable Save_Orders()
    {
        DataTable dtItems = new DataTable();
        dtItems.Columns.Add("VESSEL_ID");
        dtItems.Columns.Add("ITEM_REF_CODE");
        dtItems.Columns.Add("ORD_QTY");

        DataTable dtvesselList = (DataTable)ViewState["vessellist"];
        foreach (GridViewRow gr in gvItemsSplit.Rows)
        {
            int i = 0;
            string item_ref_code = gr.Cells[0].Text;
            foreach (TableCell cell in gr.Cells)
            {
                DataRow dr = dtItems.NewRow();

                decimal? ord_qty = 0;
                if (i > lastFixedColumnID)
                {

                    ord_qty = UDFLib.ConvertDecimalToNull(((TextBox)cell.FindControl(i.ToString())).Text);

                    dr["VESSEL_ID"] = ((HiddenField)gvItemsSplit.HeaderRow.Cells[i].FindControl("hdf" + i.ToString())).Value;
                    dr["ITEM_REF_CODE"] = item_ref_code;
                    dr["ORD_QTY"] = ord_qty;

                    dtItems.Rows.Add(dr);

                }

                i++;
            }
        }

        return BLL_PURC_Common.UPD_Reqsn_Split_IntoVessel(Request.QueryString["ReqsnCode"], dtItems, UDFLib.ConvertToInteger(Session["userid"].ToString()));
    }

    protected void btnSaveAsDraft_Click(object s, EventArgs e)
    {
        //if (Validate_Ordered_Quantity())
        //{

            Save_Orders();

            //SelectSavedVessels();

            BindSplittedItems();

            lblmsg.Text = "Saved successfully.";
        //}
        //else
        //{
        //    btnSaveAsDraft.Enabled = true;
        //    btnSaveFinalize.Enabled = true;
        //    lblmsg.Text = "splited order qty can not be grater than actual order qty !";
        //}
    }

    protected void btnLoadDraftOrder_Click(object s, EventArgs e)
    {
        SelectSavedVessels();

        BindSplittedItems();
    }

    protected void gvItemsSplit_DataBound(object sender, EventArgs e)
    {
        gvItemsSplit.HeaderRow.Cells[0].Visible = false;

        GridViewRow gvheader = gvItemsSplit.HeaderRow;

        int hi = 0;
        foreach (TableCell hcell in gvheader.Cells)
        {
            if (hi > lastFixedColumnID)
            {
                HiddenField hdf = new HiddenField();
                hdf.ID = "hdf" + hi.ToString();
                Label lblvsl = new Label();
                lblvsl.ID = "lblvsl" + hi.ToString();
                hdf.Value = hcell.Text.Split('_')[1];
                lblvsl.Text = hcell.Text.Split('_')[0];
                hcell.Controls.Add(hdf);
                hcell.Controls.Add(lblvsl);
            }

            hi++;
        }




        #region datarow

        foreach (GridViewRow gr in gvItemsSplit.Rows)
        {
            int i = 0;
            string totalQty = "0";
            string unitPrice = "0";
            string RemQty = "0";

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
                    RemQty = cell.Text;
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
                    TextBox txt = new TextBox();
                    txt.Text = cell.Text.Replace("&nbsp;", "");
                    txt.ID = i.ToString();
                    cell.Controls.Add(txt);
                    txt.CssClass = "txtord";
                    txt.Attributes.Add("onchange", "calculatePriceAndQty(" + totalQty + "," + unitPrice + ",'" + lblqty.ClientID + "','" + lblprice.ClientID + "','" + txt.ClientID + "')");
                    txt.Attributes.Add("onfocus", "Store_Old_Value('" + txt.ClientID + "')");

                    

                }



                i++;
            }

        }

        #endregion
    }

    protected void btnSaveFinalize_Click(object s, EventArgs e)
    {
        //if (Validate_Ordered_Quantity())
        //{

            DataTable dtVslToFinalize = Save_Orders();
            int sts = BLL_PURC_Common.UPD_Reqsn_Split_IntoVessel_Finalize(Request.QueryString["ReqsnCode"], dtVslToFinalize, UDFLib.ConvertToInteger(Session["userid"].ToString()));

            if (sts > 0)
            {
                lblmsg.Text = "Finalized successfully.";
                String msgApp = String.Format("alert('Finalized successfully.');RefreshPendingDetails();window.open('','_self');window.close();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg99011a", msgApp, true);
            }
            else
            {
                lblmsg.Text = "Unable to finalize !";
                BindSplittedItems();
            }
        //}
        //else
        //{
        //    lblmsg.Text = "splited order qty can not be grater than actual order qty !";
        //}

    }

    private bool Validate_Ordered_Quantity()
    {
        bool isvalid = true;
        foreach (GridViewRow gr in gvItemsSplit.Rows)
        {
            int i = 0;
            decimal total_ord_qty = 0;

            decimal ord_qty = UDFLib.ConvertToDecimal((gr.Cells[remainingQtyColumnID].FindControl("qty" + remainingQtyColumnID.ToString()) as Label).Text);
            foreach (TableCell cell in gr.Cells)
            {
                if (i > lastFixedColumnID)
                {
                    total_ord_qty += UDFLib.ConvertToDecimal(((TextBox)cell.FindControl(i.ToString())).Text);

                }

                i++;
            }

            if (total_ord_qty > ord_qty)
            {
                isvalid = false;
                gr.BackColor = System.Drawing.Color.Yellow;
                gr.ForeColor = System.Drawing.Color.Black;
            }
        }
        return isvalid;
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