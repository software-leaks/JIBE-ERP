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
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Telerik.Web.UI;
using System.Web.Caching;
using SMS.Business.PURC;
using System.Text;

using ClsBLLTechnical;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;

public partial class AddNotListItems : System.Web.UI.Page
{

    /// <summary>
    ///  -1) if RFQ has been sent and finalized by supplier then user will select the supplier from add item screen 
    ///	     and item will be inserted into quoted price and supply item.unit price and discount will be keen by purchaser.
    ///	 -2) if RFQ has not been sent  to  supplier then it will be inserted into supply item only .unit price and discount will be intered by supplier.
    ///	 -3) if RFQ has been sent but not finalized by supplier then item will be insered into quoted price and supply item both tables.
    ///	     unit price and discount amount need to set zero based on QuotationFinalization status for suppliers
    ///	 -4) add after evaluation (order code has been generated)
    ///	 -5) Additional items will be inserted in PURC_LIB_ITEMS with the vessel code and will be sync to that vessel only 
    ///	 -6) Items added before the quotation evaluation and approval will not require seperate approval process(will be approved with all items as noramal during evaluation)
    ///	 -7) if the value of  ORDER_QTY in supply item is null for addition items it means it is not approved 
    ///	 note: system will not allow add item if po has been approved.in this case purchaser need to rollback the reqsn at rfq stage and then add item./20-11-2013.this was givento add 
    ///	 
    /// </summary>
    public string CurrentUser;
    public int ItemCount;
    Int16 IsApproved = 0;
    protected DataTable dtGridItems;

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //if (rgdCatalog.SelectedIndexes.Count == 0)
        //{
        //    rgdCatalog.SelectedIndexes.Add(0);
        //}
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["IsApproved"] = "0";

        btnSave.Attributes.Add("onclick", "Async_Get_Reqsn_Validity('" + Request.QueryString["ReqCode"] + "')" + ";return VallidateGrid()");
        ViewState["IsApproved"] = BLL_PURC_Common.Get_IsApproved_ReqsnItem(Request.QueryString["ReqCode"].ToString());


        if (!IsPostBack)
        {
            try
            {
                ViewState["Item_Ref_Codes"] = "";
                BindItems();

                BindHeader();

                chkListSupplier.DataSource = BLL_PURC_Common.Get_RFQ_Supplier(Request.QueryString["ReqCode"].ToString());
                chkListSupplier.DataBind();

                tblfilter.Visible = false;
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    DataTable dtSubSystem = new DataTable();
                    dtSubSystem = objTechService.SelectSubCatalogs();
                    dtSubSystem.DefaultView.RowFilter = "System_Code ='" + Session["sSystemCode"].ToString() + "' or SubSystem_code='0'";

                    ddlSubCatalogue.DataTextField = "subsystem_description";
                    ddlSubCatalogue.DataValueField = "subsystem_code";
                    ddlSubCatalogue.DataSource = dtSubSystem.DefaultView;
                    ddlSubCatalogue.DataBind();


                }
            }
            catch { }

        }



        lblError.Text = "";
    }

    private void BindHeader()
    {
        TechnicalBAL objtechBAL = new TechnicalBAL();
        DataTable dts = new DataTable();
        dts = objtechBAL.GetRequsitionDetails(Request.QueryString["VCode"].ToString(), Request.QueryString["ReqCode"].ToString(), Request.QueryString["ReqStage"].ToString());
        if (Request.QueryString["ReqStage"].ToString() == "APR" || Request.QueryString["ReqStage"].ToString() == "ORD" || Request.QueryString["ReqStage"].ToString() == "DEO")
            dts.DefaultView.RowFilter = "Order_Code='" + Request.QueryString["sOrderCode"].ToString() + "'";
        else if (Request.QueryString["ReqStage"].ToString() == "INV")
            dts.DefaultView.RowFilter = "Delivery_Code='" + Request.QueryString["sDelivery"].ToString() + "'";
        if (dts.DefaultView.Count > 0)
        {
            lblFleet.Text = dts.DefaultView[0]["Tech_Manager"].ToString();
            lblVessel.Text = dts.DefaultView[0]["Vessel_Name"].ToString();
            lblDepartment.Text = dts.DefaultView[0]["Name_Dept"].ToString();

            lblRequsition.Text = dts.DefaultView[0]["REQUISITION_CODE"].ToString();
            lblCatalogue.Text = dts.DefaultView[0]["System_Description"].ToString();
            Session["sDocCode"] = dts.DefaultView[0]["DOCUMENT_CODE"].ToString();
            Session["sSystemCode"] = dts.DefaultView[0]["System_Code"].ToString();

        }

    }

    private void BindDepartmentByST_SP()
    {

        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtDepartment = new DataTable();
                dtDepartment = objTechService.SelectDepartment();
                rgdItems.MasterTableView.Columns[1].Visible = false;
                rgdItems.MasterTableView.Columns[1].Visible = true;
                rgdItems.MasterTableView.Columns[1].Visible = true;
                rgdItems.MasterTableView.Columns[1].Visible = true;

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

    private void BindItems()
    {
        try
        {

            if (Convert.ToInt32(ViewState["IsApproved"].ToString()) != 0)
            {
                dtGridItems = BLL_PURC_Common.Get_ReqsnItemToApprove(Request.QueryString["ReqCode"].ToString());
                rgdItems.DataSource = dtGridItems;
                rgdItems.DataBind();
                rgdItems.ShowFooter = false;
            }
            else
            {
                dtGridItems = GetAddTable();
                rgdItems.DataSource = GetAddTable();
                rgdItems.DataBind();
            }

            ViewState["dtGridItems"] = dtGridItems;

        }
        catch (Exception ex)
        {
            lblError.Text = ex.ToString();
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

    }

    protected void optList_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            BindDepartmentByST_SP();




        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

           saveval();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        finally
        {

        }

    }
    private void saveval()
    {
        StringBuilder strIDVals = new StringBuilder();
        StringBuilder strItemdesciption = new StringBuilder();
        StringBuilder stritemUnits = new StringBuilder();
        StringBuilder strItemRequestQty = new StringBuilder();
        StringBuilder strItemComments = new StringBuilder();
        StringBuilder strUnitPrice = new StringBuilder();
        StringBuilder strDiscount = new StringBuilder();
        StringBuilder strSuppCode = new StringBuilder();
        StringBuilder strBgtCode = new StringBuilder();
        StringBuilder ItemRefCode = new StringBuilder();

        int i = 0;
        if (chkListSupplier.Items.Count > 0)
        {
            foreach (ListItem item in chkListSupplier.Items)
            {
                if (item.Selected == true)
                {
                    strSuppCode.Append(item.Value);
                    strSuppCode.Append(",");
                    i++;
                }
            }
            strSuppCode.Append("@");
            strSuppCode.Append(i.ToString());
        }

        DataTable dtExtraItems = new DataTable();
        dtExtraItems.Columns.Add("pkid");
        dtExtraItems.Columns.Add("Item_Ref_Code");
        dtExtraItems.Columns.Add("Sub_System_Code");
        dtExtraItems.Columns.Add("Item_Description");
        dtExtraItems.Columns.Add("Part_Number");
        dtExtraItems.Columns.Add("Drawing_Number");
        dtExtraItems.Columns.Add("Unit_and_Packings");
        dtExtraItems.Columns.Add("Req_Qty");
        dtExtraItems.Columns.Add("Comment");
        dtExtraItems.Columns.Add("Unit_Price");
        dtExtraItems.Columns.Add("Discount");
        dtExtraItems.Columns.Add("BGT_CODE");

        int inc = 1;
        foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
        {

            TextBox txtgrdItemReqQty = (dataItem.FindControl("txtRequest_Qty") as TextBox);
            TextBox txtgrdItemComent = (dataItem.FindControl("txtItem_Comments") as TextBox);
            TextBox txtItemDescription = (dataItem.FindControl("txtItem_Description") as TextBox);
            DropDownList txtUnit = (dataItem.FindControl("cmbUnitnPackage") as DropDownList);
            TextBox txtDiscount = (dataItem.FindControl("txtDiscount") as TextBox);
            TextBox txtUnitPrice = (dataItem.FindControl("txtUnitPrice") as TextBox);
            string BgtCode = (dataItem.FindControl("ddlBudgetCode") as DropDownList).SelectedValue;

            if (txtgrdItemReqQty.Text.Length > 0 && txtItemDescription.Text.Length > 0)
            {
                DataRow dritem = dtExtraItems.NewRow();
                dritem["pkid"] = inc;
                dritem["Item_Description"] = (txtItemDescription.Text.Replace(",", " "));
                dritem["Req_Qty"] = (txtgrdItemReqQty.Text.Trim() == "" ? "0" : txtgrdItemReqQty.Text.Trim());
                dritem["Unit_Price"] = (txtUnitPrice.Text.Trim() == "" ? "0" : txtUnitPrice.Text.Trim());
                dritem["Discount"] = (txtDiscount.Text.Trim() == "" ? "0" : txtDiscount.Text.Trim());
                dritem["Unit_and_Packings"] = (txtUnit.SelectedValue);
                dritem["Comment"] = (txtgrdItemComent.Text.Replace(",", " "));
                dritem["BGT_CODE"] = (BgtCode);
                dritem["Part_Number"] = (dataItem.FindControl("txtPartNumber") as TextBox).Text;
                dritem["Drawing_Number"] = (dataItem.FindControl("txtDrawingNumber") as TextBox).Text;
                dritem["Item_Ref_Code"] = (((Label)dataItem.FindControl("lblRefCode")).Text);

                dtExtraItems.Rows.Add(dritem);
                inc++;

            }
        }
        string sOrderCode = "", sDelivery = "", sInvoice = "";
        if (Request.QueryString["ReqStage"].ToString() == "APR" || Request.QueryString["ReqStage"].ToString() == "ORD" || Request.QueryString["ReqStage"].ToString() == "DEO")
            sOrderCode = Request.QueryString["sOrderCode"].ToString();
        else if (Request.QueryString["ReqStage"].ToString() == "INV")
            sDelivery = Request.QueryString["sDelivery"].ToString();
        int retval = 0;

        if (string.IsNullOrWhiteSpace(Request.QueryString["sOrderCode"]))
        {
            if (dtExtraItems.Rows.Count > 0)
            {
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    retval = objTechService.INS_Add_Extra_Items(Request.QueryString["ReqCode"].ToString(), Request.QueryString["VCode"].ToString(), Session["sDocCode"].ToString(), sOrderCode, sDelivery, sInvoice, Request.QueryString["ReqStage"].ToString(), Session["USERID"].ToString(), strSuppCode.ToString(), Session["sSystemCode"].ToString(), dtExtraItems);

                }
                if (retval != 0)
                {
                    lblError.Text = "Item(s) saved successfully.";
                    ViewState["IsApproved"] = BLL_PURC_Common.Get_IsApproved_ReqsnItem(Request.QueryString["ReqCode"].ToString());
                    ViewState["Item_Ref_Codes"] = "";
                    rbtlst_OnSelectedIndexChanged(null, null);
                 

                    UpdGrid.Update();

                }
                else
                    lblError.Text = "Item(s) not saved.";
            }
            else
                lblError.Text = "Please provide Item description.";
        }
        else
        {
            lblError.Text = "Items can not be added because PO has been issued.";
        }




    }

    protected void rgdItems_ItemDataBound(object sender, GridItemEventArgs e)
    {


        foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
        {
            TextBox txtLT = (TextBox)(dataItem.FindControl("txtRequest_Qty") as TextBox);
            TextBox txtUNt = (TextBox)(dataItem.FindControl("txtUnitPrice") as TextBox);
            TextBox txtDis = (TextBox)(dataItem.FindControl("txtDiscount") as TextBox);
            txtLT.Attributes.Add("onKeydown", "return MaskMoney(event)");
            txtUNt.Attributes.Add("onKeydown", "return MaskMoney(event)");
            txtDis.Attributes.Add("onKeydown", "return MaskMoney(event)");

        }



        if (e.Item is GridDataItem)
        {
            foreach (TableCell cell in e.Item.Cells)
            {
                cell.ToolTip = cell.Text != "&nbsp;" ? cell.Text : null;
            }

        }
    }

    private DataTable GetAddTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(Int32));
        dt.Columns.Add("Drawing_Number", typeof(string));
        dt.Columns.Add("Part_Number", typeof(string));
        dt.Columns.Add("Item_Description", typeof(string));
        dt.Columns.Add("Unit", typeof(string));
        dt.Columns.Add("Request_Qty", typeof(Double));
        dt.Columns.Add("Unit_price", typeof(Double));
        dt.Columns.Add("Discount", typeof(Double));
        dt.Columns.Add("Item_Comments", typeof(string));
        dt.Columns.Add("chkApprove", typeof(bool));
        dt.Columns.Add("PKID", typeof(int));
        dt.Columns.Add("Office_ID", typeof(int));
        dt.Columns.Add("BudgetCode", typeof(int));
        dt.Columns.Add("Item_Intern_Ref", typeof(string));


        dt.AcceptChanges();
        for (int i = 1; i <= 10; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = i;
            dr["chkApprove"] = false;
            dr["Unit"] = "0";
            dr["BudgetCode"] = "0";
            dr["Item_Intern_Ref"] = "";
            dt.Rows.Add(dr);

        }
        dt.AcceptChanges();
        return dt;
    }

    protected void btnAddNewItem_Click(object s, EventArgs e)
    {
        dtGridItems = (DataTable)ViewState["dtGridItems"];
        int RowID = 0;
        foreach (GridDataItem item in rgdItems.MasterTableView.Items)
        {
            dtGridItems.Rows[RowID]["ID"] = ((Label)item.FindControl("lblID")).Text;
            dtGridItems.Rows[RowID]["Unit"] = ((DropDownList)item.FindControl("cmbUnitnPackage")).SelectedValue;
            dtGridItems.Rows[RowID]["Item_Description"] = ((TextBox)item.FindControl("txtItem_Description")).Text;
            dtGridItems.Rows[RowID]["Request_Qty"] = ((TextBox)item.FindControl("txtRequest_Qty")).Text == "" ? Convert.DBNull : Convert.ToDecimal(((TextBox)item.FindControl("txtRequest_Qty")).Text);

            dtGridItems.Rows[RowID]["Unit_price"] = ((TextBox)item.FindControl("txtUnitPrice")).Text == "" ? Convert.DBNull : Convert.ToDecimal(((TextBox)item.FindControl("txtUnitPrice")).Text);
            dtGridItems.Rows[RowID]["Discount"] = ((TextBox)item.FindControl("txtDiscount")).Text == "" ? Convert.DBNull : Convert.ToDecimal(((TextBox)item.FindControl("txtDiscount")).Text);

            dtGridItems.Rows[RowID]["Part_Number"] = ((TextBox)item.FindControl("txtPartNumber")).Text;
            dtGridItems.Rows[RowID]["Drawing_Number"] = ((TextBox)item.FindControl("txtDrawingNumber")).Text;

            dtGridItems.Rows[RowID]["Item_Comments"] = ((TextBox)item.FindControl("txtItem_Comments")).Text;
            dtGridItems.Rows[RowID]["PKID"] = UDFLib.ConvertToInteger(((CheckBox)item.FindControl("chkApprove")).ValidationGroup.Split(new char[] { ',' })[0].ToString());
            dtGridItems.Rows[RowID]["Office_ID"] = UDFLib.ConvertToInteger(((CheckBox)item.FindControl("chkApprove")).ValidationGroup.Split(new char[] { ',' })[1].ToString());

            dtGridItems.Rows[RowID]["BudgetCode"] = ((DropDownList)item.FindControl("ddlBudgetCode")).SelectedValue;
            dtGridItems.Rows[RowID]["Item_Intern_Ref"] = ((Label)item.FindControl("lblRefCode")).Text;
            RowID++;
        }

        DataRow dr = dtGridItems.NewRow();
        if (rgdItems.Items.Count == 0)
        {
            dr[0] = 1;
        }
        else
        {
            dr[0] = UDFLib.ConvertToInteger(((Label)rgdItems.Items[rgdItems.Items.Count - 1].FindControl("lblID")).Text) + 1;
        }
        dr["Unit"] = "0";
        dr["BudgetCode"] = "0";
        if (Convert.ToInt32(ViewState["IsApproved"].ToString()) != 0)
        {
            dr["chkApprove"] = true;
        }
        else
        {

            dr["chkApprove"] = false;
        }
        dtGridItems.Rows.Add(dr);
        rgdItems.DataSource = dtGridItems;
        rgdItems.DataBind();

        ViewState["dtGridItems"] = dtGridItems;


    }

    protected void rbtlst_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtlst.SelectedValue == "AddNewItem")
            {
                tblfilter.Visible = false;
                //bind the main grid will null(delete the library items  from grid (rgdItems)) 
                dtGridItems = GetAddTable();
                //(DataTable)ViewState["dtGridItems"];
                //dtGridItems.Rows.Clear();
                //dtGridItems.AcceptChanges();
                rgdItems.DataSource = dtGridItems;
                rgdItems.DataBind();
                rgdItems.ShowFooter = true;

                ViewState["dtGridItems"] = dtGridItems;
            }
            else
            {
                ddlSubCatalogue.SelectedIndex = -1;
                DataTable dt = new DataTable();
                gvItemsTemp.DataSource = dt;
                gvItemsTemp.DataBind();
                dtGridItems = (DataTable)ViewState["dtGridItems"];
                tblfilter.Visible = true;
                dtGridItems.Rows.Clear();
                dtGridItems.AcceptChanges();
                rgdItems.DataSource = dtGridItems;
                rgdItems.DataBind();
                rgdItems.ShowFooter = false;

                ViewState["dtGridItems"] = dtGridItems;
                
            }
        }
        catch (Exception)
        {

        }
    }

    
    protected void btnSearchLib_Click(object sender, EventArgs e)
    {
        gvItemsTemp.DataSource = BLL_PURC_Common.Get_Items_ForAddAdditional(Session["sSystemCode"].ToString(), ddlSubCatalogue.SelectedValue, txtpartno.Text.Trim(), txtdrawNo.Text.Trim(), txtDescpt.Text.Trim());
    
        gvItemsTemp.DataBind();

    }
    /// <summary>
    /// To Add Items from Existing Library.
    /// Modified Due to:Adding same Items which are already present in requisition (#ref :11806).
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    protected void btnAddToList_Click(object s, EventArgs e)
    {
        dtGridItems = (DataTable)ViewState["dtGridItems"];
        #region To check  Items reqsuitions wise

        DataTable dtReqsnItems = BLL_PURC_Common.Get_Purc_Items(Convert.ToString(Request.QueryString["ReqCode"]));
        // to check requisition items are alredy exists or not while adding items from library. 
        var results = from myRow in dtReqsnItems.AsEnumerable()
                      where myRow.Field<string>("ITEM_REF_CODE") == ((Button)s).ToolTip.Trim()
                      select myRow;
        //It will return count  if items already exists in requisition.
        int Existcnt = results.Count();

        //
        #endregion
        if (Existcnt < 1) //To check count of existing items.
        {
            if (!ViewState["Item_Ref_Codes"].ToString().Contains(((Button)s).ToolTip.Trim()))
            {

                int RowID = 0;
                foreach (GridDataItem item in rgdItems.MasterTableView.Items)
                {
                    dtGridItems.Rows[RowID]["ID"] = ((Label)item.FindControl("lblID")).Text;
                    dtGridItems.Rows[RowID]["Unit"] = ((DropDownList)item.FindControl("cmbUnitnPackage")).SelectedValue;
                    dtGridItems.Rows[RowID]["Item_Description"] = ((TextBox)item.FindControl("txtItem_Description")).Text;
                    dtGridItems.Rows[RowID]["Request_Qty"] = ((TextBox)item.FindControl("txtRequest_Qty")).Text == "" ? Convert.DBNull : Convert.ToDecimal(((TextBox)item.FindControl("txtRequest_Qty")).Text);

                    dtGridItems.Rows[RowID]["Unit_price"] = ((TextBox)item.FindControl("txtUnitPrice")).Text == "" ? Convert.DBNull : Convert.ToDecimal(((TextBox)item.FindControl("txtUnitPrice")).Text);
                    dtGridItems.Rows[RowID]["Discount"] = ((TextBox)item.FindControl("txtDiscount")).Text == "" ? Convert.DBNull : Convert.ToDecimal(((TextBox)item.FindControl("txtDiscount")).Text);

                    dtGridItems.Rows[RowID]["Item_Comments"] = ((TextBox)item.FindControl("txtItem_Comments")).Text;

                    dtGridItems.Rows[RowID]["PKID"] = UDFLib.ConvertToInteger(((CheckBox)item.FindControl("chkApprove")).ValidationGroup.Split(new char[] { ',' })[0].ToString());
                    dtGridItems.Rows[RowID]["Office_ID"] = UDFLib.ConvertToInteger(((CheckBox)item.FindControl("chkApprove")).ValidationGroup.Split(new char[] { ',' })[1].ToString());

                    dtGridItems.Rows[RowID]["BudgetCode"] = ((DropDownList)item.FindControl("ddlBudgetCode")).SelectedValue;
                    dtGridItems.Rows[RowID]["Item_Intern_Ref"] = ((Label)item.FindControl("lblRefCode")).Text; ;
                    RowID++;
                }

                DataRow dr = dtGridItems.NewRow();
                GridViewRow row = ((GridViewRow)(((Button)s).Parent.Parent));
                if (rgdItems.Items.Count == 0)
                {
                    dr[0] = 1;
                }
                else
                {
                    dr[0] = UDFLib.ConvertToInteger(((Label)rgdItems.Items[rgdItems.Items.Count - 1].FindControl("lblID")).Text) + 1;
                }

                dr["Unit"] = row.Cells[2].Text;
                dr["Item_Description"] = ((Label)gvItemsTemp.Rows[row.RowIndex].FindControl("lblDescription")).Text.Trim();
                dr["Item_Intern_Ref"] = ((Button)s).ToolTip.Trim();
                dr["BudgetCode"] = "0";
                dr["Part_Number"] = row.Cells[3].Text.Trim();
                dr["Drawing_Number"] = ((Label)gvItemsTemp.Rows[row.RowIndex].FindControl("lblDrawNo")).Text.Trim();

                if (Convert.ToInt32(ViewState["IsApproved"].ToString()) != 0)
                {
                    dr["chkApprove"] = true;
                }
                else
                {

                    dr["chkApprove"] = false;
                }
                dtGridItems.Rows.Add(dr);
                rgdItems.DataSource = dtGridItems;
                rgdItems.DataBind();
                rgdItems.ShowFooter = false;
                ViewState["Item_Ref_Codes"] += ((Button)s).ToolTip.Trim() + ",";

                ViewState["dtGridItems"] = dtGridItems;

            }
            else
            {
                lblError.Text = "One item can be added only one time ";
            }
         
        }
        else
        {
            String msg = String.Format("alert('Same Item already exists in requisition.');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            lblError.Text = "Same Item already exists in requisition.";
        }
    }
}

