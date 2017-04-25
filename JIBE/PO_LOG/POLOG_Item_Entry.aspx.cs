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
using SMS.Business.POLOG;
using ClsBLLTechnical;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;

public partial class PO_LOG_POLOG_Item_Entry : System.Web.UI.Page
{
    public string CurrentUser;
    public int ItemCount;
    Int16 IsApproved = 0;
    decimal total_Qty = 0;
    decimal total_Price = 0;
    decimal total_Discount = 0;
    string clientID;
    protected DataTable dtGridItems;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                BindItems();
            }
            catch { }

        }
        lblError.Text = "";
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
            DataSet ds = BLL_POLOG_Register.POLOG_Get_Item_List(UDFLib.ConvertIntegerToNull(Request.QueryString["POID"].ToString()));
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtGridItems = ds.Tables[0];
                rgdItems.DataSource = ds;
                rgdItems.DataBind();
                rgdItems.MasterTableView.Columns[7].Visible = true;
            }
            else
            {
                dtGridItems = GetAddTable();
                rgdItems.DataSource = dtGridItems;
                rgdItems.DataBind();
                rgdItems.MasterTableView.Columns[7].Visible = false;
                //rgdItems.MasterTableView.Columns[9].Visible = false;
                
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            saveval();
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
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

        DataTable dtExtraItems = new DataTable();
        dtExtraItems.Columns.Add("pkid");
        dtExtraItems.Columns.Add("Item_Code");
        dtExtraItems.Columns.Add("Item_Short_Desc");
        dtExtraItems.Columns.Add("ORDER_QTY");
        dtExtraItems.Columns.Add("ORDER_PRICE");
        dtExtraItems.Columns.Add("Item_Discount");
        dtExtraItems.Columns.Add("Unit");
        dtExtraItems.Columns.Add("Item_Long_Desc");
        
       
       
       // dtExtraItems.Columns.Add("BGT_CODE");

        int inc = 1;
        foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
        {
            HiddenField lblgrdID = (dataItem.FindControl("lblID") as HiddenField);
            TextBox txtgrdItem_Code = (dataItem.FindControl("txtItem_Code") as TextBox);
            TextBox txtgrdItemReqQty = (dataItem.FindControl("txtRequest_Qty") as TextBox);
            TextBox txtgrdItemComent = (dataItem.FindControl("txtItem_Comments") as TextBox);
            TextBox txtItemDescription = (dataItem.FindControl("txtItem_Description") as TextBox);
            TextBox txtUnit = (dataItem.FindControl("cmbUnitnPackage") as TextBox);
            TextBox txtDiscount = (dataItem.FindControl("txtDiscount") as TextBox);
            TextBox txtUnitPrice = (dataItem.FindControl("txtUnitPrice") as TextBox);
            //string BgtCode = (dataItem.FindControl("ddlBudgetCode") as DropDownList).SelectedValue;

            if (txtgrdItemReqQty.Text.Length > 0 && txtItemDescription.Text.Length > 0)
            {
                DataRow dritem = dtExtraItems.NewRow();
                dritem["pkid"] = lblgrdID.Value;
                dritem["Item_Code"] = (txtgrdItem_Code.Text.Replace(",", " "));
                dritem["Item_Short_Desc"] = (txtItemDescription.Text.Replace(",", " "));
                dritem["ORDER_QTY"] = (txtgrdItemReqQty.Text.Trim() == "" ? "0" : txtgrdItemReqQty.Text.Trim());
                dritem["ORDER_PRICE"] = (txtUnitPrice.Text.Trim() == "" ? "0" : txtUnitPrice.Text.Trim());
                dritem["Item_Discount"] = (txtDiscount.Text.Trim() == "" ? "0" : txtDiscount.Text.Trim());
                dritem["Unit"] = (txtUnit.Text.Trim() == "" ? "" : txtUnit.Text.Trim());
                dritem["Item_Long_Desc"] = (txtgrdItemComent.Text.Replace(",", " "));
                dtExtraItems.Rows.Add(dritem);
                inc++;

            }
        }
     
        int retval = 0;

        if (dtExtraItems.Rows.Count > 0)
        {
                retval = BLL_POLOG_Register.POLOG_Insert_Update_POItem(UDFLib.ConvertIntegerToNull(Request.QueryString["POID"].ToString()), dtExtraItems,UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
                BindItems();
                //string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
                //lblError.Text = "Item(s) saved successfully.";
                //BindItems();
                //UpdGrid.Update();
        }
        else
        {
            lblError.Text = "Please provide Item description.";
        }


    }

    protected void rgdItems_ItemDataBound(object sender, GridItemEventArgs e)
    {
        foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
        {
            TextBox txtLT = (TextBox)(dataItem.FindControl("txtRequest_Qty") as TextBox);
            TextBox txtUNt = (TextBox)(dataItem.FindControl("txtUnitPrice") as TextBox);
            TextBox txtDis = (TextBox)(dataItem.FindControl("txtDiscount") as TextBox);
            HiddenField lblID = (HiddenField)(dataItem.FindControl("lblID") as HiddenField);
            ImageButton btnDelete = (ImageButton)(dataItem.FindControl("ImgDelete") as ImageButton);
            txtLT.Attributes.Add("onKeydown", "return MaskMoney(event)");
            txtUNt.Attributes.Add("onKeydown", "return MaskMoney(event)");
            txtDis.Attributes.Add("onKeydown", "return MaskMoney(event)");
            if (lblID.Value == "0")
            {
                btnDelete.Visible = false;
            }

        }
       
        if (e.Item is GridDataItem)
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            if ((dataItem["Request_Qty"].FindControl("txtRequest_Qty") as TextBox).Text != "")
            {
                total_Qty += decimal.Parse((dataItem["Request_Qty"].FindControl("txtRequest_Qty") as TextBox).Text);
            }
            if ((dataItem["UnitPrice"].FindControl("txtUnitPrice") as TextBox).Text != "")
            {
                total_Price += decimal.Parse((dataItem["UnitPrice"].FindControl("txtUnitPrice") as TextBox).Text);
            }
            if ((dataItem["Discount"].FindControl("txtDiscount") as TextBox).Text != "")
            {
                total_Discount += decimal.Parse((dataItem["Discount"].FindControl("txtDiscount") as TextBox).Text);
            }
        }
        else if (e.Item is GridFooterItem)
        {
            GridFooterItem footer = (GridFooterItem)e.Item;
            (footer["Request_Qty"].FindControl("lblQty") as Label).Text = total_Qty.ToString();
            clientID = (footer["Request_Qty"].FindControl("lblQty") as Label).ClientID;

            (footer["UnitPrice"].FindControl("lblUnitPrice") as Label).Text = total_Price.ToString();
            clientID = (footer["UnitPrice"].FindControl("lblUnitPrice") as Label).ClientID;

            (footer["Discount"].FindControl("lblDiscount") as Label).Text = total_Discount.ToString();
            clientID = (footer["Discount"].FindControl("lblDiscount") as Label).ClientID;
        }
    }

    private DataTable GetAddTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(Int32));
        dt.Columns.Add("Srno", typeof(Int32));
        dt.Columns.Add("Item_Code", typeof(string));
        dt.Columns.Add("Item_Short_Desc", typeof(string));
        dt.Columns.Add("Unit", typeof(string));
        dt.Columns.Add("ORDER_QTY", typeof(Double));
        dt.Columns.Add("ORDER_PRICE", typeof(Double));
        dt.Columns.Add("Item_Discount", typeof(Double));
        dt.Columns.Add("Item_Long_Desc", typeof(string));
        dt.AcceptChanges();
        for (int i = 1; i <= 1; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = i;
            dr[1] = i;
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
            dtGridItems.Rows[RowID]["Srno"] = ((Label)item.FindControl("lblSrno")).Text;
            dtGridItems.Rows[RowID]["ID"] = ((HiddenField)item.FindControl("lblID")).Value;
            dtGridItems.Rows[RowID]["Unit"] = ((TextBox)item.FindControl("cmbUnitnPackage")).Text;
            dtGridItems.Rows[RowID]["Item_Code"] = ((TextBox)item.FindControl("txtItem_Code")).Text;
            dtGridItems.Rows[RowID]["Item_Short_Desc"] = ((TextBox)item.FindControl("txtItem_Description")).Text;
            dtGridItems.Rows[RowID]["ORDER_QTY"] = ((TextBox)item.FindControl("txtRequest_Qty")).Text == "" ? Convert.DBNull : Convert.ToDecimal(((TextBox)item.FindControl("txtRequest_Qty")).Text);

            dtGridItems.Rows[RowID]["ORDER_PRICE"] = ((TextBox)item.FindControl("txtUnitPrice")).Text == "" ? Convert.DBNull : Convert.ToDecimal(((TextBox)item.FindControl("txtUnitPrice")).Text);
            dtGridItems.Rows[RowID]["Item_Discount"] = ((TextBox)item.FindControl("txtDiscount")).Text == "" ? Convert.DBNull : Convert.ToDecimal(((TextBox)item.FindControl("txtDiscount")).Text);

            dtGridItems.Rows[RowID]["Item_Long_Desc"] = ((TextBox)item.FindControl("txtItem_Comments")).Text;

            RowID++;
        }

        DataRow dr = dtGridItems.NewRow();
        if (rgdItems.Items.Count == 0)
        {
            dr[0] = 1;
            dr[1] = 0;
        }
        else
        {
            dr[0] = UDFLib.ConvertToInteger(((Label)rgdItems.Items[rgdItems.Items.Count - 1].FindControl("lblSrno")).Text) + 1; 
            dr[1] = 0;
        }
        dtGridItems.Rows.Add(dr); 
        rgdItems.DataSource = dtGridItems;
        rgdItems.DataBind();
        //rgdItems.MasterTableView.Columns[8].Visible = false;
        ViewState["dtGridItems"] = dtGridItems;


    }
    protected void onDelete(object source, CommandEventArgs e)
    {
        HiddenField lblgrdID = (rgdItems.FindControl("lblID") as HiddenField);
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? ItemID = UDFLib.ConvertIntegerToNull(arg[0]);
        //dtGridItems.Rows[RowID]["ID"] = ((HiddenField)item.FindControl("lblID")).Value;
        int retval = BLL_POLOG_Register.POLOG_Delete_Item(Convert.ToInt32(ItemID), Convert.ToInt32(Session["USERID"].ToString()));
        BindItems();
    }
    protected void btnSaveClose_Click(object sender, EventArgs e)
    {
        try
        {
            saveval();
            string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
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