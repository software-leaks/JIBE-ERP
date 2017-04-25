using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using Telerik.Web.UI;
using System.Data;
using System.Text;
using SMS.Business.Infrastructure;


public partial class Purchase_Delivered_Requisition : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.Page.Header.Controls.Add(SetUserStyle.AddThemeInHeader());
            base.OnInit(e);
        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["NeedDataSource"] = "0";

        if (!IsPostBack)
        {
           
            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("Statusvalue");
            dtStatus.Columns.Add("statustext");
            DataRow drdf = dtStatus.NewRow();
            drdf["Statusvalue"] = "0";
            drdf["statustext"] = "Difference in Ordered Qty and Delivered Qty";
            dtStatus.Rows.Add(drdf);
            ucCustomDropDownListstatus.DataSource = dtStatus;
            ucCustomDropDownListstatus.DataTextField = "statustext";
            ucCustomDropDownListstatus.DataValueField = "Statusvalue";
            ucCustomDropDownListstatus.DataBind();
            BindSupplier();
           
            BindData();
        }

    }

    public void BindData()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        int? stsQtyDiff= ucCustomDropDownListstatus.SelectedValues.Rows.Count>0?UDFLib.ConvertIntegerToNull(1) :null;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = (ViewState["SORTDIRECTION"].ToString());

        //DataTable dt = BLL_PURC_Common.Get_Delivered_Requisition_Stage((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
        //    , UDFLib.ConvertStringToNull(Session["sDeptType"]), (DataTable)Session["sDeptCode"]
        //    , UDFLib.ConvertIntegerToNull(Session["ReqsnType"].ToString())s
        //    , UDFLib.ConvertStringToNull(Session["REQNUM"].ToString())
        //    , stsQtyDiff
        //    , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref rowcount, sortbycoloumn, sortdirection);
        DataTable dt = BLL_PURC_Common.Get_Delivered_Requisition_Stage_DL_New(UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
                               , UDFLib.ConvertStringToNull(Session["sDeptType"])
                               , UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                               , UDFLib.ConvertStringToNull(Session["REQNUM"])
                               , UDFLib.ConvertStringToNull(Session["sPOType"])
                               , UDFLib.ConvertStringToNull(Session["sAccType"])
                               , UDFLib.ConvertStringToNull(Session["ReqsnType"])
                               , UDFLib.ConvertStringToNull(Session["sCatalogue"])
                               , UDFLib.ConvertStringToNull(Session["sFrom"])
                               , UDFLib.ConvertStringToNull(Session["sTO"])
                               , UDFLib.ConvertStringToNull(Session["sAccClass"])
                               , UDFLib.ConvertStringToNull(Session["dturgrcy"])
                               , UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))
                               , UDFLib.ConvertStringToNull((Session["DeliveryNo"]))
                               , UDFLib.ConvertStringToNull((Session["Supplier"]))
                               , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));
        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        rgdAllReqStatus.DataSource = dt;
        BindDeliveryNumber(dt);

        if (ViewState["NeedDataSource"] == "0")
            rgdAllReqStatus.DataBind();

        string script = " var height = document.body.scrollHeight;parent.ResizeFromChild(height,'1');";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "resize" + DateTime.Now.Millisecond.ToString(), script, true);
    }

    protected void rgd_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            ViewState["NeedDataSource"] = "1";

            BindData();

        }
    }



    protected void onSelectAttachment(object source, CommandEventArgs e)
    {

        String msg = "CheckFileAndOpen('" + ((ImageButton)source).ValidationGroup + "')";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString(), msg, true);
        if (((ImageButton)source).ValidationGroup == "")
        {
            ResponseHelper.Redirect("FileAttachmentInfo.aspx?Requisitioncode=" + e.CommandArgument.ToString(), "Blank", "");
        }
    }

    protected void rgdAllReqStatus_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                ImageButton ImgPriority = (ImageButton)(item.FindControl("ImgPriority") as ImageButton);
                if (!string.IsNullOrWhiteSpace(item["URGENCY_CODE"].Text))
                {
                    if (item["URGENCY_CODE"].Text == "Urgent")
                    {
                        ImgPriority.ImageUrl = "~/Images/exclamation.png";
                        ImgPriority.ToolTip = "Urgent";
                    }
                    if (item["URGENCY_CODE"].Text == "Immediate")
                    {
                        ImgPriority.ImageUrl = "~/Images/double_Exclamation.png";
                        ImgPriority.ToolTip = item["URGENCY_CODE"].Text;
                    }
                }
                else
                {
                    ImgPriority.ImageUrl = "~/purchase/Image/transparent.gif";

                }
                /* This part is commented  due to: control imgStatus and lblStatus not found at design level causing error for further code execution.
                 * 20/02/2016 by Pranali
                string sts = ((Label)item["Status"].FindControl("lblStatus")).Text;
                Image imgStatus = (Image)item["Status"].FindControl("imgStatus");
                if (sts.Contains("Cancelled"))
                {
                    string[] Remarks = ((Label)item["Status"].FindControl("lblclremark")).Text.Split('`');

                    item["Status"].Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[<table><tr><td style='text-align:right;font-weight:bold;width:90px' >Cancelled By:</td> <td >" + Remarks[0] + "</td></tr>  <tr><td  style='text-align:right;font-weight:bold'>Cancelled On:</td> <td>" + Remarks[1] + "</td></tr>  <tr><td  style='text-align:right;font-weight:bold'> Remark: </td> <td>" + Remarks[2] + "</td></tr> </table> ]");
                    imgStatus.ImageUrl = "~/Purchase/Image/view1.gif";
                }
                else
                {
                    imgStatus.ImageUrl = "~/Purchase/Image/remarknone.png";
                }
                */

                ImageButton ImgAttach = (ImageButton)(item.FindControl("ImgAttachment") as ImageButton);
                string strAttFlag = "0";
                strAttFlag = item["Attach_Status"].Text.ToString();
                if (strAttFlag == "0")
                {
                    ImgAttach.ImageUrl = "~/images/Attach.png";
                    ImgAttach.Attributes.Add("onmouseover", "DisplayActionInHeader('Add Attachments' ,'rgdDeliveryStatus');");
                    
                }
                else
                {
                    ImgAttach.ImageUrl = "~/images/attachment32.png";
                    
                    ImgAttach.ToolTip = "View Attachments";
                    ImgAttach.Attributes.Add("onmouseover", "DisplayActionInHeader('View Attachments' ,'rgdDeliveryStatus');");
                }

            }
        }
        catch
        {
        }
    }
    
    protected void rgdAllReqStatus_SortCommand(object sender, GridSortCommandEventArgs e)
    {
        ViewState["SORTBYCOLOUMN"] = e.CommandArgument.ToString();
        switch (e.NewSortOrder)
        {
            case GridSortOrder.Ascending:
                ViewState["SORTDIRECTION"] = "ASC";
                break;
            case GridSortOrder.Descending:
                ViewState["SORTDIRECTION"] = "DESC";
                break;
        }
        switch (e.CommandArgument.ToString())
        {
            case "requestion_Date":
                ViewState["SORTBYCOLOUMN"] = null;
                break;
        }

        BindData();

    }
    protected void ddlSupplier_SelectedIndexChanged()
    {
        StringBuilder sbFilterFlt = new StringBuilder();
        foreach (DataRow dr in ddlSupplier.SelectedValues.Rows)
        {
            sbFilterFlt.Append(dr[0]);
            sbFilterFlt.Append(",");
        }
        Session["Supplier"] = ddlSupplier.SelectedValues;
        BindData();
    }
    protected void DDLPort_SelectedIndexChanged()
    {
        try
        {
            StringBuilder sbFilterFlt = new StringBuilder();
            foreach (DataRow dr in DDLPort.SelectedValues.Rows)
            {
                sbFilterFlt.Append(dr[0]);
                sbFilterFlt.Append(",");
            }
            Session["DeliveryNo"] = Convert.ToString(sbFilterFlt);
            BindData();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void BindSupplier()
    {

        DataTable dtSupplier = new DataTable();

        dtSupplier = BLL_PURC_Common.Get_SupplierList(null, "");

        ddlSupplier.DataSource = dtSupplier;
        ddlSupplier.DataTextField = "fullname";
        ddlSupplier.DataValueField = "SUPPLIER";
        ddlSupplier.DataBind();

    }
    private void BindDeliveryNumber(DataTable dts)
    {
        try
        {

            var r = from d in dts.AsEnumerable()
                    select d;
            DataTable dt = r.CopyToDataTable();

            BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
            //DataTable dt = objBLLPort.Get_PortList_Mini();
            DDLPort.DataTextField = "DELIVERY_CODE";
            DDLPort.DataValueField = "DELIVERY_CODE";
            DDLPort.DataSource = dt;
            DDLPort.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

}