using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Telerik.Web.UI;
using SMS.Business.Infrastructure;
using AjaxControlToolkit4;
using BLLQuotation;
using ClsBLLTechnical;

public partial class Purchase_ReqsnAttachment : System.Web.UI.Page
{
    clsQuotationBLL objQuoBLL = new clsQuotationBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
       

            if (!IsPostBack)
            {

                if (Request.QueryString["Requisition_code"] != null)
                {
                    BindMechDetails();
                    BindSubCatalogueMechDetails();
                }
               
            }
            //lblErrorMsg.Text = "";
       
    }
    public void BindMechDetails()
    {
        DataSet DsMech = new DataSet();
        DsMech = objQuoBLL.GetMechDetails(Request.QueryString["Requisition_code"].ToString());

        DataTable dtVesselDtl = BLL_PURC_Purchase.Get_VID_VesselDetails(UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"].ToString()));

        if (DsMech.Tables[0].Rows.Count > 0)
        {
            lblMachinesrno.Text = DsMech.Tables[0].Rows[0]["System_Serial_Number"].ToString();
            lblMakerCity.Text = DsMech.Tables[0].Rows[0]["MakerCity"].ToString();
            lblMakerContact.Text = DsMech.Tables[0].Rows[0]["MakerCONTACT"].ToString();
            lblMakerEmail.Text = DsMech.Tables[0].Rows[0]["MakerEmail"].ToString();
            lblMakerName.Text = DsMech.Tables[0].Rows[0]["MakerName"].ToString();
            lblMakerPh.Text = DsMech.Tables[0].Rows[0]["MakerPhone"].ToString();
            lblModel.Text = DsMech.Tables[0].Rows[0]["Model_Type"].ToString();
            txtAddress.Text = DsMech.Tables[0].Rows[0]["MakerAddress"].ToString();
            lblParticulars.Text = DsMech.Tables[0].Rows[0]["MechInfo"].ToString();
            lblmachinaryname.Text = DsMech.Tables[0].Rows[0]["MechName"].ToString();
            //lblSetInstalled.Text = DsMech.Tables[0].Rows[0]["Set_Instaled"].ToString();
            lblVessel.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Name"]);
        }

        if (dtVesselDtl.Rows.Count > 0)
        {
            lblVesselExName1.Text = Convert.ToString(dtVesselDtl.Rows[0]["VesselExNames"]);
            lblVessel.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Name"]);
            //lblVesselExName2.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["Vessel_Ex_Name2"]);
            lblVesselHullNo.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Hull_No"]);
            lblVesselType.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Type"]);
            lblVesselYard.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Yard"]);
            lblVesselDelvDate.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Delvry_Date"]);
            lblIMOno.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_IMO_No"]);
        }
    }
   
    public void BindSubCatalogueMechDetails()
    {
        TechnicalBAL objtechBAL = new TechnicalBAL();
        DataSet dsReqSumm = new DataSet();

        dsReqSumm = objtechBAL.GetSubCatalogueDetails(Request.QueryString["Requisition_code"].ToString(), Request.QueryString["Catalogue_Code"].ToString(), Request.QueryString["Vessel_ID"].ToString());
        if (dsReqSumm.Tables[0].Rows.Count > 0)
        {


            gvSubCatalogueDetails.DataSource = dsReqSumm.Tables[0];
            gvSubCatalogueDetails.DataBind();


            //gvSubcatelogDetails.DataSource = dsReqSumm.Tables[2];
            //rpAttachment.DataBind();
        }
        else
        {
            String msg = String.Format("alert('This Requisition is not valid !'); window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg156", msg, true);
        }
    }
}