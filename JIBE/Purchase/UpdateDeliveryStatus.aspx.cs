using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using System.Data;
using SMS.Business.Crew;

public partial class UpdateDeliveryStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAgentDetails();
            BindSupplierDetails();
            BindDeliveryStageMovement();
           
        }
      

    }

    protected void BindDeliveryStageMovement()
    {
        using (BLL_PURC_Purchase objsupplier = new BLL_PURC_Purchase())
        {
            DataSet ds = objsupplier.BindRequisitionDeliveryStagesMovement(Request.QueryString["SupplierCode"].ToString(), Request.QueryString["VesselCode"].ToString()
                                                                            , Request.QueryString["sOrderCode"].ToString());
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblsupplier.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                    txtsupplierAddress.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                    txtSupplierReadiness.Text = ds.Tables[0].Rows[0]["ORDER_READINESS"].ToString();
                    txtsupplierRemarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
                    ddlSupplierCurrentCity.SelectedValue = ds.Tables[0].Rows[0]["CURRENT_CITY"].ToString();
                    //ddlSupplierPort.SelectedValue = ds.Tables[0].Rows[0]["CURRENT_PORT"].ToString();
                    ddlSupplierDeliveryPort.SelectedValue = ds.Tables[0].Rows[0]["DELIVERY_PORT"].ToString();
                    txtSupplierEstimateDate.Text = ds.Tables[0].Rows[0]["DELIVERY_DATE"].ToString();

                    if (ds.Tables[0].Rows[0]["CURRENT_ACTIVE_STAGE"].ToString() == "0" || ds.Tables[0].Rows[0]["CURRENT_ACTIVE_STAGE"].ToString() == null)   
                        chkSupplier.Checked = false ;
                    else
                        chkSupplier.Checked = true;    
  
                }

            }

            if (ds.Tables.Count > 1)
            {

                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (ds.Tables[1].Rows[0]["STAGE_HOLDER_CODE"].ToString() != "0")
                    ddlforwarder.SelectedValue = ds.Tables[1].Rows[0]["STAGE_HOLDER_CODE"].ToString();
                    txtForwarderAddress.Text = ds.Tables[1].Rows[0]["ADDRESS"].ToString();

                    optForwarderDeliveryType.SelectedValue = ds.Tables[1].Rows[0]["DELIVERY_TYPE"].ToString();
                    txtForwarderDeliveryRemarks.Text = ds.Tables[1].Rows[0]["DELIVERY_REMARK"].ToString();
                    ddlforwarderCurrentCity.SelectedValue = ds.Tables[1].Rows[0]["CURRENT_CITY"].ToString();
                    //ddlForwarderPort.SelectedValue = ds.Tables[1].Rows[0]["CURRENT_PORT"].ToString();
                    ddlForwarderDeliveryPort.SelectedValue = ds.Tables[1].Rows[0]["DELIVERY_PORT"].ToString();
                    txtForwarderEstimateDate.Text = ds.Tables[1].Rows[0]["DELIVERY_DATE"].ToString();
                    txtForwarderRemarks.Text = ds.Tables[1].Rows[0]["REMARKS"].ToString();

                    if (ds.Tables[1].Rows[0]["CURRENT_ACTIVE_STAGE"].ToString() == "0" || ds.Tables[1].Rows[0]["CURRENT_ACTIVE_STAGE"].ToString() == "")
                        chkForwarder.Checked = false;
                    else
                        chkForwarder.Checked = true;    
                }



                if (ds.Tables[2].Rows.Count > 0)
                {
                    
                    if (ds.Tables[2].Rows[0]["STAGE_HOLDER_CODE"].ToString() != "0")
                    ddlAgent.SelectedValue = ds.Tables[2].Rows[0]["STAGE_HOLDER_CODE"].ToString();
                    txtAgentdetails.Text = ds.Tables[2].Rows[0]["ADDRESS"].ToString();

                    optAgentDeliveryType.SelectedValue = ds.Tables[2].Rows[0]["DELIVERY_TYPE"].ToString();
                    txtAgentDeliveryRemarks.Text = ds.Tables[2].Rows[0]["DELIVERY_REMARK"].ToString();
                    ddlAgentCurrentCity.SelectedValue = ds.Tables[2].Rows[0]["CURRENT_CITY"].ToString();
                    //ddlAgentPort.SelectedValue = ds.Tables[2].Rows[0]["CURRENT_PORT"].ToString();
                    ddlAgentPortDeliveryOnbaord.SelectedValue = ds.Tables[2].Rows[0]["DELIVERY_PORT"].ToString();
                    txtAgentEstimateDate.Text = ds.Tables[2].Rows[0]["DELIVERY_DATE"].ToString();
                    txtAgentRemarks.Text = ds.Tables[2].Rows[0]["REMARKS"].ToString();

                    if (ds.Tables[2].Rows[0]["CURRENT_ACTIVE_STAGE"].ToString() == "0" || ds.Tables[2].Rows[0]["CURRENT_ACTIVE_STAGE"].ToString() == null)
                        chkAgent.Checked = false;
                    else
                        chkAgent.Checked = true;    
                }

                if (ds.Tables[3].Rows.Count > 0)
                {
                    txtDeliveryDate.Text = ds.Tables[3].Rows[0]["DELIVERY_DATE"].ToString();
                    txtDeliveredRemarks.Text = ds.Tables[3].Rows[0]["REMARKS"].ToString();
                    ddlDeliveredPort.SelectedValue = ds.Tables[3].Rows[0]["DELIVERY_PORT"].ToString();

                    if (ds.Tables[3].Rows[0]["CURRENT_ACTIVE_STAGE"].ToString() == "0" || ds.Tables[3].Rows[0]["CURRENT_ACTIVE_STAGE"].ToString() == null)
                        chkDelivered.Checked = false;
                    else
                        chkDelivered.Checked = true;    
                }

                if (ds.Tables[4].Rows.Count > 0)
                {
                    txtVesselAcknowlegedDate.Text = ds.Tables[4].Rows[0]["DELIVERY_DATE"].ToString();
                    txtVesselAcknowledgeRemarks.Text = ds.Tables[4].Rows[0]["REMARKS"].ToString();
                    ddlVesselAcknowledgedPort.SelectedValue = ds.Tables[4].Rows[0]["DELIVERY_PORT"].ToString();

                    if (ds.Tables[4].Rows[0]["CURRENT_ACTIVE_STAGE"].ToString() == "0" || ds.Tables[4].Rows[0]["CURRENT_ACTIVE_STAGE"].ToString() == null)
                        chkVesselAcknowledged.Checked = false;
                    else
                        chkVesselAcknowledged.Checked = true; 
                }
        }
      }

    }


    protected void BindSupplierDetails()
    {
        using (BLL_PURC_Purchase objsupplier = new BLL_PURC_Purchase())
        {
            DataTable dt = objsupplier.SelectSupplier();
            dt.DefaultView.RowFilter = "SUPPLIER_CATEGORY='S'";
            ddlforwarder.DataTextField = "SUPPLIER_NAME";
            ddlforwarder.DataValueField = "SUPPLIER";
            ddlforwarder.DataSource = dt.DefaultView.ToTable();
            ddlforwarder.DataBind();
            ddlforwarder.Items.Insert(0, new ListItem("SELECT", "0"));
        }
    }


    protected void BindAgentDetails()
    {
        using (BLL_PURC_Purchase objsupplier = new BLL_PURC_Purchase())
        {

            DataTable dt = objsupplier.SelectSupplier();
            dt.DefaultView.RowFilter = "SUPPLIER_CATEGORY='A'";
            ddlAgent.DataTextField = "SUPPLIER_NAME";
            ddlAgent.DataValueField = "SUPPLIER";
            ddlAgent.DataSource = dt.DefaultView.ToTable();
            ddlAgent.DataBind();
            ddlAgent.Items.Insert(0, new ListItem("SELECT", "0"));

        }

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            using (BLL_PURC_Purchase objPurchase = new BLL_PURC_Purchase())
            {

                int SuppFlage = chkSupplier.Checked == true ? 1 : 0;
                int ForwarderFlage = chkForwarder.Checked == true ? 1 : 0;
                int AgentFlage = chkAgent.Checked == true ? 1 : 0;
                int DeliveredFlage = chkDelivered.Checked == true ? 1 : 0;
                int VslAckFlage = chkVesselAcknowledged.Checked == true ? 1 : 0;


                int retval = objPurchase.InsertRequisitionDeliveryStagesMovement(Request.QueryString["sOrderCode"].ToString()
                                                        , "SUPPLIER", Request.QueryString["SupplierCode"].ToString(), "", txtSupplierReadiness.Text
                                                        , "", ddlSupplierCurrentCity.SelectedValue.ToString()
                                                        , ""
                                                        , txtSupplierEstimateDate.Text 
                                                        , ddlSupplierDeliveryPort.SelectedValue.ToString()
                                                        , txtsupplierRemarks.Text
                                                        , Session["userid"].ToString(), SuppFlage);
                
                int retvalForwarder = objPurchase.InsertRequisitionDeliveryStagesMovement(Request.QueryString["sOrderCode"].ToString()
                                     , "FORWARDER", ddlforwarder.SelectedValue.ToString(), optForwarderDeliveryType.SelectedItem.Text ,""
                                     , txtForwarderDeliveryRemarks.Text, ddlforwarderCurrentCity.SelectedValue.ToString()
                                     , ddlForwarderDeliveryPort.SelectedValue.ToString()
                                     , txtForwarderEstimateDate.Text
                                     , ddlForwarderDeliveryPort.SelectedValue.ToString()
                                     , txtForwarderRemarks.Text
                                     , Session["userid"].ToString(),ForwarderFlage);


                int retvalAgent = objPurchase.InsertRequisitionDeliveryStagesMovement(Request.QueryString["sOrderCode"].ToString()
                                        , "AGENT", ddlAgent.SelectedValue.ToString(), optAgentDeliveryType.SelectedItem.Text, ""
                                        , txtAgentDeliveryRemarks.Text, ddlAgentCurrentCity.SelectedValue.ToString()
                                        , ""
                                        , txtAgentEstimateDate.Text
                                        , ddlAgentPortDeliveryOnbaord.SelectedValue.ToString()
                                        , txtAgentRemarks.Text
                                       , Session["userid"].ToString(),AgentFlage);


                int retvalDelivered = objPurchase.InsertRequisitionDeliveryStagesMovement(Request.QueryString["sOrderCode"].ToString()
                                   , "DELIVERED", "", "", ""
                                   , "", "", ""
                                   , txtDeliveryDate.Text
                                   , ddlDeliveredPort.SelectedValue.ToString()
                                   , txtDeliveredRemarks.Text
                                  , Session["userid"].ToString(),DeliveredFlage);



                int retvalVAck = objPurchase.InsertRequisitionDeliveryStagesMovement(Request.QueryString["sOrderCode"].ToString()
                               , "VESSELACKNOWLEDGED", "", "",""
                               , "", "", ""
                               , txtVesselAcknowlegedDate.Text
                               , ddlSupplierDeliveryPort.SelectedValue.ToString()
                               , txtVesselAcknowledgeRemarks.Text
                              , Session["userid"].ToString(),VslAckFlage);
 
            }


            BindDeliveryStageMovement();

            String msg = "alert('Saved Successfully.');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
           
  
        }
        catch (Exception ex)
        {
            String msg = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }

    }


    protected void chkSupplier_CheckedChanged(object sender, EventArgs e)
    {
        chkSupplier.Checked = true ; 
        chkAgent.Checked = false;
        chkForwarder.Checked = false;
        chkDelivered.Checked = false;
        chkVesselAcknowledged.Checked = false;  

    }
    protected void chkForwarder_CheckedChanged(object sender, EventArgs e)
    {
        chkSupplier.Checked = false;
        chkForwarder.Checked = true;
        chkAgent.Checked = false;
        chkDelivered.Checked = false;
        chkVesselAcknowledged.Checked = false;  

    }
    protected void chkAgent_CheckedChanged(object sender, EventArgs e)
    {
        chkSupplier.Checked = false;
        chkForwarder.Checked = false;
        chkAgent.Checked = true;
        chkDelivered.Checked = false;
        chkVesselAcknowledged.Checked = false;  
    }
    protected void chkDelivered_CheckedChanged(object sender, EventArgs e)
    {
        chkSupplier.Checked = false;
        chkAgent.Checked = false;
        chkForwarder.Checked = false;
        chkDelivered.Checked = true;
        chkVesselAcknowledged.Checked = false;  
    }
    protected void chkVesselAcknowledged_CheckedChanged(object sender, EventArgs e)
    {
        chkSupplier.Checked = false;
        chkAgent.Checked = false;
        chkForwarder.Checked = false;
        chkDelivered.Checked = false;
        chkVesselAcknowledged.Checked = true;  

    }
}