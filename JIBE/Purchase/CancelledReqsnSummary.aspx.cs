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
using SMS.Business.PURC;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Text;
using ClsBLLTechnical;

public partial class Purchase_CancelledReqsnSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            objsrcReqsnType.Select();
            ddlReqsnType.DataBind();
            BindRequisitionSummary();
            TechnicalBAL objpurch = new TechnicalBAL();
         

            gvCrewList.DataSource = objpurch.Get_CrewList(UDFLib.ConvertToInteger(Request.QueryString["Vessel_Code"].ToString()));
            gvCrewList.DataBind();
            BindReqsTypeLog();
        }

    }



    public void BindRequisitionSummary()
    {
        TechnicalBAL objtechBAL = new TechnicalBAL();
        DataSet dsReqSumm = new DataSet();
        dsReqSumm = objtechBAL.CancelledGetRequisitionSummary(Request.QueryString["REQUISITION_CODE"].ToString(), Request.QueryString["document_code"].ToString(), Request.QueryString["Vessel_Code"].ToString());
        lblDepartment.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["DepartmentType"]);

        lblCatalog.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["Catalog"]);
        lblReqNo.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["RequistionCode"]);
        lblTotalItem.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["TotalItems"]);
        lblToDate.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["ToDate"]);
        lblVessel.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["VesselName"]);
        txtComments.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["ReqComents"]);

        //-------Maker Details------
        //lblMarkerAdd.Text = dsReqSumm.Tables[0].Rows[0]["MakerAddress"].ToString();
        txtAddress.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["MakerAddress"]);
        lblMakerPh.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["MakerPhone"]);
        lblMakerName.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["MakerName"]);
        lblMakerExtension.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["MakerTELEX"]);
        lblMakerEmail.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["MakerEmail"]);
        lblMakerContact.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["MakerCONTACT"]);
        lblMakerCity.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["MakerCity"]);

        lblModel.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["Model"]);
        lblParticulars.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["Particulars"]);
        if (dsReqSumm.Tables[0].Rows[0]["Reqsn_Type"].ToString() != "0")
        {
            ddlReqsnType.Items.FindByValue(dsReqSumm.Tables[0].Rows[0]["Reqsn_Type"].ToString()).Selected = true;
        }
        if (dsReqSumm.Tables[0].Rows[0]["REQUISITION_ETA"].ToString() != "")//should be enable untill deliveru update
        {
           
            ddlReqsnType.Enabled = false;
        }
        //-------Vessel Details---------
        DataTable dtVesselDtl = objtechBAL.Get_VID_VesselDetails(UDFLib.ConvertToInteger(Request.QueryString["Vessel_Code"].ToString()));
        if (dtVesselDtl.Rows.Count > 0)
        {
            lblVesselExName1.Text = Convert.ToString(dtVesselDtl.Rows[0]["VesselExNames"]);
            //lblVesselExName2.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["Vessel_Ex_Name2"]);
            lblVesselHullNo.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Hull_No"]);
            lblVesselType.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Type"]);
            lblVesselYard.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Yard"]);
            lblVesselDelvDate.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_Delvry_Date"]);
            lblIMOno.Text = Convert.ToString(dtVesselDtl.Rows[0]["Vessel_IMO_No"]);
        }

        rgdItems.DataSource = dsReqSumm.Tables[1];
        rgdItems.DataBind();

        rpAttachment.DataSource = dsReqSumm.Tables[2];
        rpAttachment.DataBind();
    }

  
    protected void rpAttachment_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        // maneesh string strPath = ConfigurationManager.AppSettings["INVFolderPath"].ToString();
        //mkt  ((HyperLink)e.Item.FindControl("lnkAtt")).NavigateUrl = strPath + ((HyperLink)e.Item.FindControl("lnkAtt")).NavigateUrl;
    }
   
   
    protected void rgdItems_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType.ToString() == "Item" || e.Item.ItemType.ToString() == "AlternatingItem")
        {

            ((Label)(e.Item.FindControl("lblItemDesc"))).Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[" + "Short Description: " + ((Label)e.Item.FindControl("lblshortDesc")).Text + "] body=[" + "Long Description: " + ((Label)e.Item.FindControl("lblLongDesc")).Text + "<hr>" + "Comment: " + ((Label)e.Item.FindControl("lblComments")).Text + "]");
        }


    }

    protected void BindReqsTypeLog()
    {
        gvReqsnTypeLog.DataSource = BLL_PURC_Common.Get_ReqsnType_Log(Request.QueryString["REQUISITION_CODE"].ToString());
        gvReqsnTypeLog.DataBind();

    }
}