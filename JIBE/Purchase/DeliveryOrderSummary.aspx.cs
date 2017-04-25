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

public partial class Technical_INV_DeliveryOrderSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDeliveryOrderSummary();

        }
    }


    public void BindDeliveryOrderSummary()
    {
        TechnicalBAL objtechBAL = new TechnicalBAL();
        DataSet dsReqSumm = new DataSet();
        dsReqSumm = objtechBAL.GetDeliveryOrderSummary(Request.QueryString["REQUISITION_CODE"].ToString(), Request.QueryString["document_code"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["DELIVERY_CODE"].ToString());

        lblCatalog.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["Catalog"]);
        lblReqNo.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["RequistionCode"]);
        lblTotalItem.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["TotalItems"]);
        lblToDate.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["ToDate"]);
        lblVessel.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["VesselName"]);
        txtComments.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["ReqComents"]);
        lblDelDate.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["DELIVERY_DATE"]);
        lblDelPort.Text = Convert.ToString(dsReqSumm.Tables[0].Rows[0]["PORT_NAME"]);

        rgdItems.DataSource = dsReqSumm.Tables[2];
        rgdItems.DataBind();

        rpAttachment.DataSource = dsReqSumm.Tables[1];
        rpAttachment.DataBind();

        gvItemsSupp.DataSource = dsReqSumm.Tables[3];
        gvItemsSupp.DataBind();
    }


    protected void rpAttachment_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //string strPath = ConfigurationManager.AppSettings["INVFolderPath"].ToString();
        //((HyperLink)e.Item.FindControl("lnkAtt")).NavigateUrl = strPath + ((HyperLink)e.Item.FindControl("lnkAtt")).NavigateUrl;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ResponseHelper.Redirect("SummaryReportsShow.aspx?REQUISITION_CODE=" + Request.QueryString["REQUISITION_CODE"].ToString() + "&document_code=" + Request.QueryString["document_code"].ToString() + "&Vessel_Code=" + Request.QueryString["Vessel_Code"].ToString() + "&DELIVERY_CODE=" + Request.QueryString["DELIVERY_CODE"].ToString() + "&RptType=DelvSumry", "Blank", "");
    }
    protected void rgdItems_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            GridDataItem gr = e.Item as GridDataItem;
            if (gr["REQUESTED_QTY"].Text != gr["ORDER_QTY"].Text)
            {
                gr["ORDER_QTY"].BackColor = System.Drawing.Color.Red;
                gr["ORDER_QTY"].ForeColor = System.Drawing.Color.White;
            }
            if (gr["ORDER_QTY"].Text != gr["DELIVERD_QTY"].Text)
            {
                gr["DELIVERD_QTY"].BackColor = System.Drawing.Color.Red;
                gr["DELIVERD_QTY"].ForeColor = System.Drawing.Color.White;
            }
        }
    }
}
