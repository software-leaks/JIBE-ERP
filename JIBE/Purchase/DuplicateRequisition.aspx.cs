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

public partial class Purchase_DuplicateRequisition : System.Web.UI.Page
{
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    BLL_PURC_Purchase objPurchase = new BLL_PURC_Purchase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL();
            hdnReqsnCode.Value = Request.QueryString["REQUISITION_CODE"].ToString();
            hdnDocumentCode.Value = Request.QueryString["Document_Code"].ToString();
        }
        
    }
    /// <summary>
    /// Fill Dropdown of Delivery Port and Vessel.
    /// </summary>
    protected void FillDDL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable dtVessel = objVsl.Get_UserVesselList_DL(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()), UDFLib.ConvertToInteger(GetSessionUserID()));
            ddlVessel.DataSource = dtVessel;
            ddlVessel.DataTextField = "Vessel_name";
            ddlVessel.DataValueField = "Vessel_id";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem("-SELECT-", "0"));

            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
              
                DataTable dtPort = objTechService.getDeliveryPort();
                ddlDeliveryPort.DataSource = dtPort;
                ddlDeliveryPort.DataTextField = "Port_Name";
                ddlDeliveryPort.DataValueField = "Id";
                ddlDeliveryPort.DataBind();
                ddlDeliveryPort.Items.Insert(0, new ListItem("-SELECT-", "0"));

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
   
    /// <summary>
    /// Cancel event
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
    /// <summary>
    /// Insert Duplicate reqs of selected reqsn.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //TechnicalBAL objtechBAL = new TechnicalBAL();

            DataSet ds = BLL_PURC_Common.PURC_INS_Duplicate_RequisitionDeatils(hdnDocumentCode.Value, UDFLib.ConvertToInteger(ddlVessel.SelectedValue), UDFLib.ConvertToInteger(ddlDeliveryPort.SelectedValue), txtDeliveryDate.Text, GetSessionUserID(), GetSessionUserName());

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dataforDisplay;
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    dataforDisplay = objTechService.GetReqItemsPreview(null, ddlVessel.SelectedValue, ds.Tables[0].Rows[0]["DOCUMENT_CODE"].ToString());
                }
                if (dataforDisplay.Tables[0].Rows.Count > 0)
                {
                    if (dataforDisplay.Tables[1].Rows.Count > 0)
                    {
                        if (dataforDisplay.Tables[1].Rows[0]["Direct_Quotation"].ToString() == "True")
                        {

                            ResponseHelper.Redirect("ItemPreview.aspx?Requisitioncode=" + "" + "&Vessel_Code=" + ddlVessel.SelectedValue.ToString() + "&Document_Code=" + ds.Tables[0].Rows[0]["DOCUMENT_CODE"].ToString(), "_self", "");
                            string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
                        }
                        else
                        {

                            ResponseHelper.Redirect("RequisitionPreview.aspx?Requisitioncode=" + "" + "&Vessel_Code=" + ddlVessel.SelectedValue.ToString() + "&Document_Code=" + ds.Tables[0].Rows[0]["DOCUMENT_CODE"].ToString(), "_self", "");
                            string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
                        }

                    }
                    else
                    {
                        // String msg = String.Format("alert('Requisition has deleted'); window.close();");
                        String msg = String.Format("alert('No Preview configured for this PO Type.');");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
                    }

                }
                else
                {
                    String msg = String.Format("alert('Error While Saving Record.');");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                    string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
                }
            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

        }
   
}