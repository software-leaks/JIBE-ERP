using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.PURC;
using Telerik.Web.UI;
using System.IO;

public partial class Purchase_CancelledLog : System.Web.UI.Page
{
    public string sRequiPendingType = "CAN";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["NeedDataSource"] = "0";

        if (!IsPostBack)
            BindData();
    }



    public void BindData()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = (ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_PURC_Common.Get_CancelReqsn_DL_New(
                                UDFLib.ConvertStringToNull(Session["sFleet"]), UDFLib.ConvertStringToNull(Session["sVesselCode"])
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
                                , UDFLib.ConvertStringToNull(Session["sUrgency"])
                                , UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))
                                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));

        //DataTable dt = BLL_PURC_Common.Get_CancelReqsn((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
        //                        , UDFLib.ConvertStringToNull(Session["sDeptType"]), (DataTable)Session["sDeptCode"]
        //                        , UDFLib.ConvertIntegerToNull(Session["ReqsnType"].ToString()), UDFLib.ConvertStringToNull(Session["REQNUM"].ToString())
        //                        , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, sortbycoloumn, sortdirection);

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        rgdCancelledLog.DataSource = dt;

        if (ViewState["NeedDataSource"] == "0")
            rgdCancelledLog.DataBind();


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

    protected void rgdCancelledLog_ItemDataBound(object sender, GridItemEventArgs e)
    {
       
        try
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                ImageButton ImgPriority = (ImageButton)(item.FindControl("ImgPriority") as ImageButton);
                #region To View Cancel PO
                HyperLink lnkCancelPO = (HyperLink)item.FindControl("lnkCancelPO");
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                if (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "File_Path")) != "")
                {
                    lnkCancelPO.NavigateUrl = baseUrl + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "File_Path"));
                }
                #endregion
                if ((item["URGENCY_CODE"].Text != " ") && (item["URGENCY_CODE"].Text != "&nbsp"))
                {
                    if (item["URGENCY_CODE"].Text == "Urgent")
                    {
                        ImgPriority.ImageUrl = "~/Images/exclamation.png";
                        ImgPriority.ToolTip = item["URGENCY_CODE"].Text;
                    }
                    if (item["URGENCY_CODE"].Text == "Immediate")
                    {
                        ImgPriority.ImageUrl = "~/Images/double_Exclamation.png";
                        ImgPriority.ToolTip = item["URGENCY_CODE"].Text;
                    }
                    //if (item["URGENCY_CODE"].Text == "Urgent")
                    //{
                    //    ImgPriority.ImageUrl = "~/Images/exclamation.png";
                    //    ImgPriority.ToolTip = "Urgent";
                    //}
                    //{
                    //    ImgPriority.ImageUrl = "~/Images/double_Exclamation.jpg";
                    //    ImgPriority.ToolTip = item["URGENCY_CODE"].Text;
                    //}

                }
                else
                {
                    ImgPriority.ImageUrl = "~/purchase/Image/transparent.gif";

                }
                string[] Remarks = ((Label)item["CalcelationType"].FindControl("lblclremark")).Text.Split('`');
                item["CalcelationType"].Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[<table><tr><td style='text-align:right;font-weight:bold;width:90px' >Cancelled By:</td> <td >" + Remarks[0] + "</td></tr>  <tr><td  style='text-align:right;font-weight:bold'>Cancelled On:</td> <td>" + Remarks[1] + "</td></tr>  <tr><td  style='text-align:right;font-weight:bold'> Remark: </td> <td>" + Remarks[2] + "</td></tr> </table> ]");
            }
        }
        catch
        {
        }
    }

    protected void btnActivateReqsn_Click(object s, EventArgs e)
    {
        
       
            int sts = BLL_PURC_Common.Activate_Cancelled_Reqsn(((Button)s).CommandArgument, Convert.ToInt32(Session["userid"].ToString()));
            if (sts > 0)
            {
                String msg = String.Format("alert('This requisition has been re-activated successfully.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgactivatedReqsn", msg, true);
                ucCustomPagerItems.isCountRecord = 1;
                BindData();
            }
            else
            {
                String msg = String.Format("alert('Fail to activate this requisition.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgactivatedReqsn", msg, true);
                ucCustomPagerItems.isCountRecord = 1;
                BindData();
            }
        

    }
    protected void btnActivatePO_Click(object s, EventArgs e)
    {
        int Active_PO_Count = BLL_PURC_Common.Get_Active_PO_Count(((Button)s).CommandArgument);
        if (Active_PO_Count == 0)
        {
            int sts = BLL_PURC_Common.Activate_Cancelled_PO_DL(((Button)s).CommandArgument, Convert.ToInt32(Session["userid"].ToString()));
            if (sts > 0)
            {
                String msg = String.Format("alert('This requisition has been re-activated successfully.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgactivatedPO", msg, true);
                ucCustomPagerItems.isCountRecord = 1;
                BindData();
            }
            else
            {
                String msg = String.Format("alert('Fail to activate this PO.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgactivatedReqsn", msg, true);
                ucCustomPagerItems.isCountRecord = 1;
                BindData();
            }
        }
        else
        {
            String msg = String.Format("alert('Active PO already exists for this Supplier.');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgactiveReqsn", msg, true);
            ucCustomPagerItems.isCountRecord = 1;
            BindData();
        }
    }
    protected void rgdCancelledLog_SortCommand(object sender, GridSortCommandEventArgs e)
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
   

}