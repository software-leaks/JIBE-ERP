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

public partial class Technical_INV_ALLRequisitioinStatusGrid : System.Web.UI.Page
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
            Button2.Visible = false;

            Session["PoolSelection"] = Request.QueryString["type"].ToString();

            BindALLRequistionsDetails();

        }
    }
    public void BindALLRequistionsDetails()
    {

        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DataTable dtAllRequisition = new DataTable();
            switch (Request.QueryString["Type"])
            {
                case "ALL":

                    int Fetch_Count = ucCustomPagerAllStatus.isCountRecord;
                    string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
                     string sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = (ViewState["SORTDIRECTION"].ToString());
                     dtAllRequisition = objTechService.SelectAllRequisitionStages_New
                                            (UDFLib.ConvertStringToNull(Session["sFleet"]),UDFLib.ConvertStringToNull(Session["sVesselCode"])
                                                                                ,UDFLib.ConvertStringToNull(Session["sDeptType"])
                                                                                ,UDFLib.ConvertStringToNull(Session["sPurc_Dept"])
                                                                                ,UDFLib.ConvertStringToNull(Session["REQNUM"])
                                                                                ,UDFLib.ConvertStringToNull(Session["sPOType"])
                                                                                ,UDFLib.ConvertStringToNull(Session["sAccType"])
                                                                                ,UDFLib.ConvertStringToNull(Session["ReqsnType"])
                                                                                ,UDFLib.ConvertStringToNull(Session["sCatalogue"])
                                                                                ,UDFLib.ConvertDateToNull(Session["sFrom"])
                                                                                ,UDFLib.ConvertDateToNull(Session["sTO"])
                                                                                ,UDFLib.ConvertStringToNull(Session["sAccClass"])
                                                                                ,UDFLib.ConvertStringToNull(Session["dturgrcy"])
                                                                                ,UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))

                                                                               ,ucCustomPagerAllStatus.CurrentPageIndex,
                                                                               ucCustomPagerAllStatus.PageSize,
                                                                               ref Fetch_Count, sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));

                    //dtAllRequisition = objTechService.SelectAllRequisitionStages((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"],
                    //                                                             (DataTable)Session["sDeptCode"],
                    //                                                             UDFLib.ConvertStringToNull(Session["sDeptType"]),
                    //                                                             UDFLib.ConvertStringToNull(Session["REQNUM"]),
                    //                                                             UDFLib.ConvertIntegerToNull(Session["ReqsnType"]),
                    //                                                             UDFLib.ConvertStringToNull(ddlReqsnStatus.SelectedValue),
                    //                                                             ucCustomPagerAllStatus.CurrentPageIndex,
                    //                                                             ucCustomPagerAllStatus.PageSize,
                    //                                                             ref Fetch_Count, sortbycoloumn, sortdirection);

                    if (ucCustomPagerAllStatus.isCountRecord == 1)
                    {
                        ucCustomPagerAllStatus.CountTotalRec = Fetch_Count.ToString();
                        ucCustomPagerAllStatus.BuildPager();
                    }

                    break;
            }


            if (ddlReqsnStatus.SelectedValue != "")
            {
                Session["PURC_ReqsnStatusATAll"] = ddlReqsnStatus.SelectedValue;

            }
            Session["PURC_ReqsnStatusATAll"] = "";

            rgdAllReqStatus.DataSource = dtAllRequisition;

            if (ViewState["NeedDataSource"].ToString() == "0")
                rgdAllReqStatus.DataBind();

            string script = " var height = document.body.scrollHeight;parent.ResizeFromChild(height,'1');";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "resize" + DateTime.Now.Millisecond.ToString(), script, true);
        }



    }
    protected void rgdAllReqStatus_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            ViewState["NeedDataSource"] = "1";

            BindALLRequistionsDetails();

        }


    }
    protected void onSendPO(object sender, CommandEventArgs e)
    {

    }
    protected void onSelect(object sender, CommandEventArgs e)
    {

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
        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
        try
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                ImageButton ImgPriority = (ImageButton)(item.FindControl("ImgPriority") as ImageButton);
                ImageButton btnSendPOToVessel = (ImageButton)(item.FindControl("btnSendPOToVessel"));
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

                string sts = ((Label)item["Status"].FindControl("lblStatus")).Text;
                Image imgStatus = (Image)item["Status"].FindControl("imgStatus");

                #region to View Cancel Po Details
                HyperLink hlnkOrderNo = (HyperLink)item.FindControl("lnkOrderNo");
                if (sts.Contains("Cancelled"))//For Cancelled PO
                {
                    string[] Remarks = ((Label)item["Status"].FindControl("lblclremark")).Text.Split('`');

                    item["Status"].Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[<table><tr><td style='text-align:right;font-weight:bold;width:90px' >Cancelled By:</td> <td >" + Remarks[0] + "</td></tr>  <tr><td  style='text-align:right;font-weight:bold'>Cancelled On:</td> <td>" + Remarks[1] + "</td></tr>  <tr><td  style='text-align:right;font-weight:bold'> Remark: </td> <td>" + Remarks[2] + "</td></tr> </table> ]");
                    imgStatus.ImageUrl = "~/Purchase/Image/view1.gif";
                    if(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CANCEL_PATH"))!=string.Empty)
                    {
                        hlnkOrderNo.NavigateUrl = baseUrl + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "CANCEL_PATH"));
                    }
                    

                }
                else //For Po
                {
                    imgStatus.ImageUrl = "~/Purchase/Image/remarknone.png";
                    hlnkOrderNo.NavigateUrl="POPreview.aspx?RFQCODE=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem,"REQUISITION_CODE"))+"&Vessel_Code="+  Convert.ToString(DataBinder.Eval(e.Item.DataItem,("Vessel_CODE")))+"&Order_Code="+  Convert.ToString(DataBinder.Eval(e.Item.DataItem,("ORDER_CODE")));
                    
                }
                #endregion

                ImageButton ImgAttach = (ImageButton)(item.FindControl("ImgAttachment") as ImageButton);
                string strAttFlag = "0";
                strAttFlag = item["Attach_Status"].Text.ToString();
                if (strAttFlag == "0")
                {
                    ImgAttach.ImageUrl = "~/images/Attach.png";
                    ImgAttach.ToolTip = "Add Attachments";
                    ImgAttach.Attributes.Add("onmouseover", "DisplayActionInHeader('Add Attachments' ,'rgdAllReqStatus');");
                }
                else
                {
                    ImgAttach.ImageUrl = "~/images/attachment32.png";
                    ImgAttach.ToolTip = "View Attachments";
                    ImgAttach.Attributes.Add("onmouseover", "DisplayActionInHeader('View Attachments' ,'rgdAllReqStatus');");
                }

                if (sts.Contains("Cancelled"))
                {
                    btnSendPOToVessel.Visible = false;
                }
                else if (sts.Contains("Raise PO") || sts.Contains("Pending Supplier Confirmation") || sts.Contains("Update Delivery"))
                {
                    btnSendPOToVessel.Visible = true;
                }
            }
        }
        catch
        {
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Session["sType"] = "ARQ";
        ResponseHelper.Redirect("ReqPoolReportShow.aspx?Requisitioncode=OMRK-10-00022" + "&Vessel_Code=301" + "&Document_Code=301100621093137" + "&OrderNo=OMRK-10-00022" + "&SuppCode=S0213", "Blank", "");
    }

    protected void btnSendPOToVessel_Click(object s, EventArgs e)
    {
        try
        {
            
            BLL_PURC_Common.INS_SYNC_PO((s as ImageButton).CommandArgument);

            String msg = "alert('sent successfully.')";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "mssg", msg, true);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    protected void ddlReqsnStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        ucCustomPagerAllStatus.isCountRecord = 1;
        BindALLRequistionsDetails();
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

        BindALLRequistionsDetails();

    }
}
