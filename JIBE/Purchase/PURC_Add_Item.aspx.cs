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
using AjaxControlToolkit4;
using System.IO;

public partial class Purchase_PURC_Add_Item : System.Web.UI.Page
{
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    BLL_PURC_Purchase objPurchase = new BLL_PURC_Purchase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (AjaxFileUpload1.IsInFileUploadPostBack)
        {

        }
        else
        {
            if (!IsPostBack)
            {
                BindData();
                BindUnitPakageDDL();
                BindItemCatagory();
                 //string ItemID = "20170105102136687001011";
                 //BindItem(ItemID);
                if (Request.QueryString["Item_Type"].ToString() == "Free_Item")
                {
                    lblHeader.Text = "Add a Free Text Item";
                    ViewState["Item_Type"] = "Free_Item";
                    trSubCatalogue.Visible = false;
                    trCatalogue.Visible = false;
                }
                if (Request.QueryString["Item_Type"].ToString() == "Catalogue_Item")
                {
                    lblHeader.Text = "Add an Item To Catalogue";
                    ViewState["Item_Type"] = "Catalogue_Item";
                    trSubCatalogue.Visible = true;
                    trCatalogue.Visible = true;
                }
            }
        }
    }

    protected void BindData()
    {
        try
        {
            DataSet ds = BLL_PURC_Common.PURC_Get_RequisitionDeatils(UDFLib.ConvertToInteger(GetSessionUserID()), GetDocumentCode());
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblAccClassification.Text = ds.Tables[0].Rows[0]["Account_Classification"].ToString();
                lblSubCatalogue.Text = Session["SUBCATALOGUENAME"].ToString();
                lblCatalogue.Text = ds.Tables[0].Rows[0]["System_Description"].ToString();
                ViewState["VesselID"] = ds.Tables[0].Rows[0]["Vessel_Code"].ToString();
                ViewState["Reqsn_Type"] = ds.Tables[0].Rows[0]["Reqsn_Type"].ToString();
                if (ds.Tables[0].Rows[0]["Reqsn_Type"].ToString() == "SP")
                {
                    trCritical.Visible = true;
                }
                else if (ds.Tables[0].Rows[0]["Reqsn_Type"].ToString() == "RP")
                {
                    trCritical.Visible = true;
                }
                else
                {
                    trCritical.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            
        }
        finally
        {

        }
    }
    private string GetDocumentCode()
    {
        if (Session["DocumentCode"] != null)
            return Session["DocumentCode"].ToString();
        else
            return "0";
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
    private string GetCatalogue()
    {
        if (Session["CatalogID"] != null)
            return Session["CatalogID"].ToString();
        else
            return "0";
    }
    private string GetSubCatalogue()
    {
        if (Session["SubCatalg"] != null)
            return Session["SubCatalg"].ToString();
        else
            return "0";
    }
    private void BindItemCatagory()
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dt = new DataTable();
                dt = objTechService.LibraryGetSystemParameterList("469", "");
                ddlItemCategory.DataTextField = "DESCRIPTION";
                ddlItemCategory.DataValueField = "Code";
                ddlItemCategory.DataSource = dt;
                ddlItemCategory.DataBind();

                DataTable dtCritical = objTechService.LibraryGetSystemParameterList("7598", "");
                ddlCritical.DataTextField = "DESCRIPTION";
                ddlCritical.DataValueField = "Code";
                ddlCritical.DataSource = dtCritical;
                ddlCritical.DataBind();
                ddlCritical.Items.Insert(0, new ListItem("-SELECT-", "0"));
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        finally
        {

        }

    }
    private void BindUnitPakageDDL()
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtUnitnPack = new DataTable();
                dtUnitnPack = objTechService.SelectUnitnPackage();
                ddlUnit.DataTextField = "Main_Pack";
                ddlUnit.DataValueField = "Main_Pack";
                ddlUnit.DataSource = dtUnitnPack;
                ddlUnit.DataBind();

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        finally
        {

        }
    }
    protected void btnSaveItem_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder insetItem = new StringBuilder();
            string strCrtical = null;
            Boolean Catalogue_Item = true;
            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();

            if (ViewState["Reqsn_Type"].ToString() == "SP")
            {
                strCrtical = ddlCritical.SelectedValue;
            }
            else if (ViewState["Reqsn_Type"].ToString() == "RP")
            {
                strCrtical = ddlCritical.SelectedValue;
            }
            if(ViewState["Item_Type"].ToString() == "Free_Item")
            {
                Catalogue_Item = false;
            }
            else{
                Catalogue_Item = true;
            }
            string val = objTechService.LibraryItemSave(Convert.ToInt32(GetSessionUserID()), Convert.ToString(GetCatalogue()), Convert.ToString(GetSubCatalogue()),
                 txtItemPartNumber.Text.Trim(), txtItemName.Text.Trim(), txtItemDescription.Text.Trim(), txtItemDrawingNumber.Text.Trim(), ddlUnit.SelectedItem.Text, UDFLib.ConvertDecimalToNull(txtMinQty.Text.Trim()), UDFLib.ConvertDecimalToNull(txtMaxQty.Text.Trim()), ViewState["VesselID"].ToString(),
                 UDFLib.ConvertIntegerToNull(ddlItemCategory.SelectedValue), hdnImageURL.Value, hdnProductURL.Value, UDFLib.ConvertIntegerToNull(strCrtical), Catalogue_Item);


           
            if (val == "0")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Item Name with same part no already exists..');", true);
            }
            else
            {
                if (ViewState["Item_Type"].ToString() == "Free_Item")
                {

                    SaveInventryItem(val);
                    
                }
               
                BindItem(val);
                string message = "alert('Item Added successfully.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
               
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        finally
        {

        }
    }
    protected void SaveInventryItem(string ItemID)
    {
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        IventoryItemData objDoInventory1 = new IventoryItemData();

        objDoInventory1.VesselCode = ViewState["VesselID"].ToString();
        objDoInventory1.RequisitionCode = null;

        objDoInventory1.ItemRefCode = ItemID.ToString();
        objDoInventory1.ItemInternRef = ItemID.ToString();
        objDoInventory1.SystemCode = Convert.ToString(GetCatalogue());
        objDoInventory1.SubSystemCode = Convert.ToString(GetSubCatalogue());



        if (txtItemDescription.Text != "")
            objDoInventory1.itemFullDesc =  txtItemDescription.Text.Trim();
        else
            objDoInventory1.itemFullDesc = "0";

        objDoInventory1.itemShortDesc = txtItemName.Text.Trim();
        objDoInventory1.SavedLine = "5";
        objDoInventory1.RequisitionComment = "0";
        if (txtItemDrawingNumber.Text != "")
            objDoInventory1.Drawing_Number = txtItemDrawingNumber.Text.Trim();
        else
            objDoInventory1.Drawing_Number = "0";

        objDoInventory1.DrawingLink = "1";

        objDoInventory1.CreatedBy = Session["userid"].ToString();

        objDoInventory1.DocumentCode = GetDocumentCode().ToString();
        objDoInventory1.reqestedQty = "0";
        objDoInventory1.ItemComment = null;
        objDoInventory1.ROB = "0";

       int  retval = objTechService.SaveInventroySupplyItem(objDoInventory1);
    }
    protected void BindItem(string ItemID)
    {
        try
        {
            BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
            DataSet ds = objTechService.LibraryItemList(ItemID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hdnItemID.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                Session["ItemID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                lblSpareCreatedBy.Text = ds.Tables[0].Rows[0]["CREATEDBY"].ToString();
                lblSpareModifiedBy.Text = ds.Tables[0].Rows[0]["MODIFIEDBY"].ToString();
                lblSpareDeletedBy.Text = ds.Tables[0].Rows[0]["DELETEDBY"].ToString();
                txtItemDrawingNumber.Text = ds.Tables[0].Rows[0]["Drawing_Number"].ToString();
                txtItemPartNumber.Text = ds.Tables[0].Rows[0]["PART_NUMBER"].ToString();

                txtItemName.Text = ds.Tables[0].Rows[0]["SHORT_DESCRIPTION"].ToString();
                txtItemDescription.Text = ds.Tables[0].Rows[0]["LONG_DESCRIPTION"].ToString();
                ddlUnit.SelectedValue = ds.Tables[0].Rows[0]["UNIT_AND_PACKINGS"].ToString();
                txtMaxQty.Text = ds.Tables[0].Rows[0]["INVENTORY_MAX"].ToString();
                txtMinQty.Text = ds.Tables[0].Rows[0]["INVENTORY_MIN"].ToString();


                ddlItemCategory.SelectedValue = ds.Tables[0].Rows[0]["Item_Category"].ToString();
                ddlCritical.SelectedValue = ds.Tables[0].Rows[0]["CriticalFlag"].ToString();
                string destinationPath = Server.MapPath("../uploads/PURC_Items/");
                lnkImageUploadName.NavigateUrl = "../uploads/PURC_Items/" + ds.Tables[0].Rows[0]["Image_Url"].ToString();

                lnkProductDetailUploadName.NavigateUrl = "../uploads/PURC_Items/" + ds.Tables[0].Rows[0]["Product_Details"].ToString();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

       
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
         try
        {
            string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnLoadFiles_Click(object sender, EventArgs e)
    {
        BindItem(Session["ItemID"].ToString());
    }
    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = objUploadFilesize.Get_Module_FileUpload("PURC_");
            Byte[] fileBytes = file.GetContents();

            string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\PURC_Items");
            Guid GUID = Guid.NewGuid();
            if (Session["ItemID"] != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string datasize = dt.Rows[0]["Size_KB"].ToString();
                    if (fileBytes.Length < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                    {

                        string Flag_Attach = "itm_" + GUID.ToString() + Path.GetExtension(file.FileName);
                        string FullFilename = Path.Combine(sPath, "itm_" + GUID.ToString() + Path.GetExtension(file.FileName));
                        StringBuilder insetItem = new StringBuilder();
                        if (Session["ImageType"] != null)
                        {
                            if (Session["ImageType"].ToString() == "Image")
                            {
                                hdnImageURL.Value = Flag_Attach.ToString();
                            }
                            else
                            {
                                hdnProductURL.Value = Flag_Attach.ToString();
                            }
                        }
                        else
                        {
                            hdnImageURL.Value = Flag_Attach.ToString();
                        }

                        int val = BLL_PURC_Common.ItemImageUpdate(Convert.ToInt32(GetSessionUserID()), Session["ItemID"].ToString(), Convert.ToString(GetCatalogue()), Convert.ToString(GetSubCatalogue()), hdnImageURL.Value, hdnProductURL.Value);

                        FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
                        fileStream.Write(fileBytes, 0, fileBytes.Length);
                        fileStream.Close();

                      
                        // Session["AppAttach_" + Request.QueryString["ItemID"].ToString()] = Flag_Attach + "," + file.FileName;


                    }
                    else
                    {
                        string message = "alert('KB File size exceeds maximum limit.')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    }
                }
                else
                {
                    string message = "alert('Upload size not set!')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
            }
            else
            {
                string message = "alert('Please First Save Item!')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
           
        }
         catch(Exception ex)
         {
             UDFLib.WriteExceptionLog(ex);
         }
    }

    

    protected void imgAttach_Click(object sender, ImageClickEventArgs e)
    {
        Session["ImageType"] = "Image";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowUploader()", true);
    }
    protected void btnProductAttImage_Click(object sender, ImageClickEventArgs e)
    {
        Session["ImageType"] = "ProductImage";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowDetailsUploader()", true);
    }
}