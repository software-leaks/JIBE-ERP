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
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Collections.Generic;

public partial class InvertyItems : System.Web.UI.Page
{

    //string _constring = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
    public string CurrentUser;
    public int ItemCount;


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (rgdSubCatalog.SelectedIndexes.Count == 0)
        {
            rgdSubCatalog.SelectedIndexes.Add(0);
        }
        rgdSubCatalog.EnableAjaxSkinRendering = true;
        rgdItems.EnableAjaxSkinRendering = true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (rgdSubCatalog.SelectedIndexes.Count == 0)
        {
            rgdSubCatalog.SelectedIndexes.Add(0);
        }
        if (!IsPostBack)
        {
            ViewState["flt_CatalogID"] = "";
            ViewState["flt_SubCatalg"] = "0";
            ViewState["flt_ItemDesc"] = "0";
            ViewState["flt_PartNo"] = "0";
            ViewState["flt_DrawNo"] = "0";
            ViewState["flt_ReqsnCode"] = "0";
            ViewState["flt_FormType"] = "ALL";
            ViewState["flt_PageIndex"] = "1";
            ViewState["flt_PageSize"] = "15";

            ViewState["countPerRenderPage"] = "0";
            ViewState["countTotalRec"] = "0";
            ViewState["CurrentPageSection"] = "1";
            ViewState["flt_DocumentCode"] = "0";
            divaddItem.Visible = false;
            BindUnitPakageDDL();
            BindDeatils();
        }
        lblError.Text = "";
    }
    
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    private string GetSessionUserName()
    {
        if (Session["USERNAME"] != null)
            return Session["USERNAME"].ToString();
        else
            return "0";
    }
    private string GetDocumentCode()
    {
        if (Session["DocumentCode"] != null)
            return Session["DocumentCode"].ToString();
        else
            return "0";
    }
    /// <summary>
    /// Bind Page details
    /// </summary>
    public void BindDeatils()
    {
        try
        {
            DataSet ds = BLL_PURC_Common.PURC_Get_RequisitionDeatils(UDFLib.ConvertToInteger(GetSessionUserID()), GetDocumentCode());
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["flt_DocumentCode"] = GetDocumentCode();
                ViewState["flt_ReqsnCode"] = ds.Tables[0].Rows[0]["REQUISITION_CODE"].ToString();
                lblfleet.Text = ds.Tables[0].Rows[0]["FleetName"].ToString();
                //lblAccType.Text = ds.Tables[0].Rows[0]["Account_Name"].ToString();
                lblDepartmentName.Text = ds.Tables[0].Rows[0]["DEPARTMENT"].ToString();
                lblRequisitionType.Text = ds.Tables[0].Rows[0]["Reqn_Name"].ToString();
                lblCatalouge.Text = ds.Tables[0].Rows[0]["System_Description"].ToString();
                lblVessel.Text = ds.Tables[0].Rows[0]["Vessel_Name"].ToString();
                lblUrgency.Text = ds.Tables[0].Rows[0]["URGENCY_Type"].ToString();
                hdnVesselID.Value = ds.Tables[0].Rows[0]["Vessel_Code"].ToString();
                hdnPOType.Value = ds.Tables[0].Rows[0]["PO_Type"].ToString();
                BindSubCatalogue(ds.Tables[0].Rows[0]["Requested_Catalouge"].ToString());
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Bind subcatalogue depend on catalogue
    /// </summary>
    /// <param name="CatalogId"></param>
    private void BindSubCataLogs(string CatalogId)
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtSubSystem = new DataTable();
                ViewState["flt_CatalogID"] = CatalogId;
                dtSubSystem = objTechService.SelectSubCatalogs();
                dtSubSystem.DefaultView.RowFilter = "System_Code ='" + CatalogId + "'";
                DataTable dt = dtSubSystem.DefaultView.ToTable();
                DataRow dr = dt.NewRow();
                dr["subsystem_code"] = "0";
                dr["subsystem_description"] = "ALL";
                dt.Rows.InsertAt(dr, 0);
                rgdSubCatalog.DataSource = dt;
                rgdSubCatalog.DataBind();
                rgdSubCatalog_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void BindItems()
    {
        try
        {
            if (rgdItems.Columns[21].Visible == true && rgdItems.Columns[21].Visible == true)
            {
                SaveValue();
            }
            int RecordCount = 1;
            DataTable dtItems = BLL_PURC_Purchase.Get_Create_New_Requisition(ViewState["flt_CatalogID"].ToString(), ViewState["flt_SubCatalg"].ToString(), ViewState["flt_ItemDesc"].ToString(), ViewState["flt_PartNo"].ToString(), ViewState["flt_DrawNo"].ToString(), ViewState["flt_DocumentCode"].ToString(), ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref RecordCount);
            rgdItems.DataSource = dtItems;
            rgdItems.DataBind();
            ucCustomPager1.CountTotalRec = RecordCount.ToString();
            ucCustomPager1.BuildPager();
            ViewState["countTotalRec"] = RecordCount;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    private int BindItems_Count()
    {
        return BLL_PURC_Purchase.SelectItemForInventory_Count(ViewState["flt_CatalogID"].ToString(), ViewState["flt_SubCatalg"].ToString(), ViewState["flt_ItemDesc"].ToString(), ViewState["flt_PartNo"].ToString(), ViewState["flt_DrawNo"].ToString(), ViewState["flt_ReqsnCode"].ToString());
    }
 
    private void BindUnitPakageDDL()
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtUnitnPack = new DataTable();
                dtUnitnPack = objTechService.SelectUnitnPackage();
                cmbUnitnPackage.DataTextField = "Main_Pack";
                cmbUnitnPackage.DataValueField = "Main_Pack";
                cmbUnitnPackage.DataSource = dtUnitnPack;
                cmbUnitnPackage.DataBind();
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Add Items from office side in requisition.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DivItembtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder insetItem = new StringBuilder();
            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
            string val = objTechService.LibraryItemSave(Convert.ToInt32(Session["userid"]), Convert.ToString(ViewState["flt_CatalogID"]), Convert.ToString(ViewState["flt_SubCatalg"]),
                 txtPartnumber.Text.Trim(), txtShortDescription.Text.Trim(), txtLongDescription.Text.Trim(), txtDrawingNumber.Text.Trim(), cmbUnitnPackage.SelectedItem.Text, null, null, hdnVesselID.Value,
                 null, null, "", null);
            if (val == "0")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Item Name with same part no already exists..');", true);
            }
            //again bind the item grid..
            ViewState["CurrentPageSection"] = "1";
            ViewState["flt_PageIndex"] = "1";
            ucCustomPager1.CurrentPageIndex = 1;
            BindItems();
            ViewState["flt_DrawNo"] = "0";
            ViewState["flt_ItemDesc"] = "0";
            ViewState["flt_PartNo"] = "0";
            //clear the grid text box.

            //close the services.
            if (((Button)sender).ID.ToLower() == "btnsaveadd")
            {
                txtPartnumber.Text = "";
                txtShortDescription.Text = "";
                txtLongDescription.Text = "";
                //txtItemAddress.Text = "";
                txtDrawingNumber.Text = "";
                cmbUnitnPackage.SelectedIndex = 0;
            }
            else
            {
                divaddItem.Visible = false;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Clear controls after adding items.
    /// </summary>
    protected void ClearText()
    {
        try
        {
            txtDrawingNumber.Text = "";
            txtPartnumber.Text = "";
            txtShortDescription.Text = "";
            txtLongDescription.Text = "";
            txtUnitPackage.Text = "";
            //txtItemAddress.Text = "";
            txtDrawingNumber.Text = "";
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Add Item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
        string SubCatalogId = ViewState["flt_SubCatalg"].ToString();
        if (SubCatalogId != "0")
        {
            
            lblSubCatalogueIT.Text = ViewState["SUBCATALOGUENAME"].ToString();
            this.SetFocus("ctl00_MainContent_txtPartnumber");
            cmbUnitnPackage.SelectedIndex = 0;
            txtDrawingNumber.Enabled = true;
            txtPartnumber.Text = "";
            txtShortDescription.Text = "";
            txtLongDescription.Text = "";
            //txtItemAddress.Text = "";
            txtDrawingNumber.Text = "";
            divaddItem.Visible = true;

        }
        else
        {
            string message1 = "alert('Please Select the Subcatalog and then Add the item.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Subcatalog", message1, true);
        }
        
            HiddenValues.Value = "";
            lblError.Text = "";
        }
      catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);

        }
    }
    /// <summary>
    /// Requisition Preview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        try
        {


            if (Convert.ToString(ViewState["flt_DocumentCode"]) != "0")
            {
                SaveValue();
                //saveval();
            }

            if (Convert.ToString(ViewState["flt_DocumentCode"]) != "0")//ViewState["flt_ReqsnCode"]) != "0"
            {
                DataSet dataforDisplay;
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {

                    dataforDisplay = objTechService.GetReqItemsPreview(Convert.ToString(ViewState["flt_ReqsnCode"]), hdnVesselID.Value, ViewState["flt_DocumentCode"].ToString());

                }

                if (dataforDisplay.Tables[0].Rows.Count > 0)
                {
                    if (dataforDisplay.Tables[1].Rows.Count > 0)
                    {
                        if (dataforDisplay.Tables[1].Rows[0]["Direct_Quotation"].ToString() == "True")
                        {

                            ResponseHelper.Redirect("ItemPreview.aspx?Requisitioncode=" + "" + "&Vessel_Code=" + hdnVesselID.Value.ToString() + "&Document_Code=" + ViewState["flt_DocumentCode"].ToString(), "_self", "");
                        }
                        else
                        {
                            ResponseHelper.Redirect("RequisitionPreview.aspx?Requisitioncode=" + "" + "&Vessel_Code=" + hdnVesselID.Value.ToString() + "&Document_Code=" + ViewState["flt_DocumentCode"].ToString(), "_self", ""); 
                           
                        }
                       
                    }
                     else
                    {
                        // String msg = String.Format("alert('Requisition has deleted'); window.close();");
                        String msg = String.Format("alert('No Preview configured for this PO Type.');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                    }
                }
                else
                {
                    // String msg = String.Format("alert('Requisition has deleted'); window.close();");
                    String msg = String.Format("alert('No items in this Requisition.');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                }

                //ResponseHelper.Redirect("ItemPreview.aspx?Requisitioncode=" + Convert.ToString(ViewState["flt_ReqsnCode"]) + "&Vessel_Code=" + DDLVessel.SelectedValue + "&Document_Code=" + ViewState["flt_DocumentCode"].ToString(), "_self", "");



            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        finally
        {
            //cmbDept_OnSelectedIndexChanged(null, null);
            //UpdatePane11.Update();
        }


    }
    protected void rgdSubCatalog_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string SubCatalogId;
            rgdItems.Visible = true;
            GridDataItem dataItem = (GridDataItem)rgdSubCatalog.SelectedItems[0];
            SubCatalogId = dataItem["SUBSYSTEM_CODE"].Text;
            ViewState["flt_SubCatalg"] = dataItem["SUBSYSTEM_CODE"].Text;
            ViewState["SUBCATALOGUENAME"] = dataItem["SUBSYSTEM_DESCRIPTION"].Text;
            ViewState["CurrentPageSection"] = "1";
            ViewState["flt_PageIndex"] = "1";
            ucCustomPager1.CurrentPageIndex = 1;
            BindItems();

            ViewState["flt_DrawNo"] = "0";
            ViewState["flt_ItemDesc"] = "0";
            ViewState["flt_PartNo"] = "0";
            btnAdd.Enabled = true;
            divaddItem.Visible = false;
        }

        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void EnableGridtext(bool flag)
    {
        int i = 0;

        foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
        {
            TextBox txtgrdItemReqQty = (TextBox)(dataItem.FindControl("txtgrdItemReqstdQty") as TextBox);
            TextBox txtgrdItemComent = (TextBox)(dataItem.FindControl("txtgrdItemComments") as TextBox);
            TextBox txtROB = (TextBox)(dataItem.FindControl("txtROB") as TextBox);
            //TextBox txtComm = e.Item.FindControl("txtgrdItemComments") as TextBox;
            //txtgrdItemReqQty.Attributes.Add("onblur", "return calculate('" + txtgrdItemReqQty.ClientID + "','" + i.ToString() + "')");
            //txtgrdItemComent.Attributes.Add("onblur", "return calculate('" + txtgrdItemComent.ClientID + "','" + i.ToString() + "')");
            //txtROB.Attributes.Add("onblur", "return UpdatedROBIndex('" + txtgrdItemComent.ClientID + "','" + i.ToString() + "')");
            txtgrdItemReqQty.Enabled = flag;
            txtgrdItemComent.Enabled = flag;
            txtROB.Enabled = flag;

            i++;
        }

    }

    public int TotalRequestedItemCount()
    {

        ItemCount = 0;
        foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
        {
            TextBox txtgrdItemReqQty = (TextBox)(dataItem.FindControl("txtgrdItemReqstdQty") as TextBox);

            if (txtgrdItemReqQty.Text != "")
            {
                ItemCount += 1;
            }

        }
        return ItemCount;
    }
    /// <summary>
    /// Save reqsn
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet dataforDisplay;
        divaddItem.Visible = false;
        try
        {
            SaveValue();
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                dataforDisplay = objTechService.GetReqItemsPreview(Convert.ToString(ViewState["flt_ReqsnCode"]), hdnVesselID.Value, ViewState["flt_DocumentCode"].ToString());
            }
            if (dataforDisplay.Tables[0].Rows.Count > 0)
            {
                ConfirmValue.Value = "0";
                String msg = String.Format("alert('Requested Qty. Saved Successfully...');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            }
            else
            {
                // String msg = String.Format("alert('Requisition has deleted'); window.close();");
                String msg = String.Format("alert('No items in this Requisition.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Save reqsn function
    /// </summary>
    private void SaveValue()
    {
        int retval = 0;
        List<GridDataItem> RequestedItem = (from GridDataItem grdItem in rgdItems.MasterTableView.Items.Cast<GridDataItem>()
                                            where ((TextBox)grdItem.FindControl("txtgrdItemReqstdQty")).Text != "0.00" && ((TextBox)grdItem.FindControl("txtgrdItemReqstdQty")).Text != ""
                                                    && ((TextBox)grdItem.FindControl("txtgrdItemReqstdQty")).Text != "0"
                                            select grdItem).ToList();

        IventoryItemData objDoInventory1 = new IventoryItemData();
        objDoInventory1.VesselCode = hdnVesselID.Value;
        objDoInventory1.RequisitionCode = ViewState["flt_ReqsnCode"].ToString();
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        if (RequestedItem.Count > 0)
        {
            foreach (GridDataItem reqitem in RequestedItem)
            {
                objDoInventory1.ItemRefCode = reqitem["ID"].Text;
                objDoInventory1.ItemInternRef = "0";
                objDoInventory1.SystemCode = reqitem["System_Code"].Text;
                objDoInventory1.SubSystemCode = reqitem["Subsystem_Code"].Text;



                if (reqitem["Long_Description"].Text != "")
                    objDoInventory1.itemFullDesc = reqitem["Long_Description"].Text;
                else
                    objDoInventory1.itemFullDesc = "0";

                objDoInventory1.itemShortDesc = reqitem["Short_Description"].Text;
                objDoInventory1.SavedLine = "5";
                objDoInventory1.RequisitionComment = "0";
                if (reqitem["Drawing_Number"].Text != "")
                    objDoInventory1.Drawing_Number = reqitem["Drawing_Number"].Text;
                else
                    objDoInventory1.Drawing_Number = "0";

                objDoInventory1.DrawingLink = "1";

                objDoInventory1.CreatedBy = Session["userid"].ToString();

                objDoInventory1.DocumentCode = ViewState["flt_DocumentCode"].ToString();
                objDoInventory1.reqestedQty = ((TextBox)(reqitem.FindControl("txtgrdItemReqstdQty"))).Text;
                objDoInventory1.ItemComment = ((TextBox)reqitem.FindControl("txtgrdItemComments")).Text;
                objDoInventory1.ROB = (((TextBox)(reqitem.FindControl("txtROB"))).Text == "" ? null : ((TextBox)(reqitem.FindControl("txtROB"))).Text);

                retval = objTechService.SaveInventroySupplyItem(objDoInventory1);
            }

        }

    }

    protected void btnItemSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSrchDrawingNo.Text != "")
            {
                ViewState["flt_DrawNo"] = txtSrchDrawingNo.Text;

            }
            if (txtSrchDesc.Text != "")
            {
                ViewState["flt_ItemDesc"] = txtSrchDesc.Text;
            }
            if (txtSrchPartNo.Text != "")
            {
                ViewState["flt_PartNo"] = txtSrchPartNo.Text;
            }
            //ViewState["countTotalRec"] = BindItems_Count().ToString();

            ViewState["CurrentPageSection"] = "1";
            ucCustomPager1.CurrentPageIndex = 1;
            ViewState["flt_PageIndex"] = "1";
            BindItems();
            lblError.Text = "";

            divaddItem.Visible = false;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void BindSubCatalogue(string CatalogId)
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                BindSubCataLogs(CatalogId);

                ViewState["countPerRenderPage"] = "0";
                ViewState["countTotalRec"] = "0";
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void rgdSubCatalog_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            foreach (TableCell cell in e.Item.Cells)
            {
                cell.ToolTip = cell.Text != "&nbsp;" ? cell.Text : null;
            }
        }
    }

}

