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
using System.Text;
public partial class Technical_INV_NewRequisition : System.Web.UI.Page
{
    DataTable dtRequistion = new DataTable();
    public string sVesselCode = "0";
    public string sCatalog = "0";

    public string sRequiPendingType = "NRQ";
    int CriticalCount = 0;
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
        UserAccessValidation();
        ViewState["NeedDataSource"] = "0";
        dvCancelReq.Visible = false;
        if (!IsPostBack)
        {
            //BindRequisitionGrid(sVesselCode, sRequiPendingType, sCatalog, sDeptCode, sREQNUM, sDeptType, sFleet);

            BindData();
            divOnHold.Visible = false;
            ViewState["callResize"] = "0";
         
        }

    }
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/useraccess.htm");

        if (objUA.Add == 0)
        {


        }
        if (objUA.Edit == 0)
        {


        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {


        }


    }

    public void BindData()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        BLL_PURC_Purchase objPurcBLL = new BLL_PURC_Purchase();
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        string sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = (ViewState["SORTDIRECTION"].ToString());

        //DataTable dt = objPurcBLL.SelectNewRequisitionList((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"]
        //                        , UDFLib.ConvertStringToNull(Session["sDeptType"]), (DataTable)Session["sDeptCode"]
        //                        , UDFLib.ConvertIntegerToNull(Session["ReqsnType"].ToString()), UDFLib.ConvertStringToNull(Session["REQNUM"].ToString())
        //                        , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount,sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));

        DataTable dt = objPurcBLL.SelectNewRequisitionList_New
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
                                ,UDFLib.ConvertStringToNull(Session["sUrgency"])
                                ,UDFLib.ConvertStringToNull((Session["sReqsnStatus"]))
                                ,ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, sortbycoloumn, UDFLib.ConvertStringToNull(sortdirection));

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        rgdNewREQ.DataSource = dt;

        if (ViewState["NeedDataSource"] == "0")
            rgdNewREQ.DataBind();


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
        divOnHold.Visible = false;


        String msg = "CheckFileAndOpen('" + ((ImageButton)source).ValidationGroup + "')";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), DateTime.Now.ToString(), msg, true);
        if (((ImageButton)source).ValidationGroup == "")
        {
            ResponseHelper.Redirect("FileAttachmentInfo.aspx?Requisitioncode=" + e.CommandArgument.ToString(), "Blank", "");
        }
    }

    protected void onSelect(object sender, CommandEventArgs e)
    {
        divOnHold.Visible = false;

        HiddenArgument.Value = e.CommandArgument.ToString();
        string[] strArgs = HiddenArgument.Value.Split('&');

        string sOnHold = strArgs[4];


        if (sOnHold.ToString() == "False")
        {
            ResponseHelper.Redirect("SelectSuppliers.aspx?Requisitioncode=" + e.CommandArgument.ToString(), "Blank", "");
        }
        else
        {
            String msg = String.Format("alert('This requisition has been marked as On Hold.'); window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }

    protected void onItemPreview(object sender, CommandEventArgs e)
    {
        ResponseHelper.Redirect("ReqPoolReportShow.aspx?Requisitioncode=" + e.CommandArgument.ToString(), "Blank", "");
    }

    protected void onAddNewItem(object sender, CommandEventArgs e)
    {
        ResponseHelper.Redirect("AddNotListItems.aspx?ReqCode=" + e.CommandArgument.ToString(), "Blank", "");

    }

    protected void btnOnHold_Click(object sender, EventArgs e)
    {
        try
        {
            HoldUnHold.Remarks = "";
            divOnHold.Visible = true;

            GridDataItem dataItem = (GridDataItem)((ImageButton)sender).Parent.Parent;
            ImageButton btnOnHold = (ImageButton)(dataItem.FindControl("btnOnHold") as ImageButton);

            string sOnHoldFlag = dataItem["OnHold"].Text.ToString();
            if (sOnHoldFlag.ToString() == "False")
            {
                // btnOnHold.Text = "Hold";
                btnOnHold.ImageUrl = "~/purchase/Image/OnHold.png";
                HoldUnHold.lblHeader = "Hold the Requisition";
            }
            else
            {
                // btnOnHold.Text = "Un Hold";
                btnOnHold.ImageUrl = "~/purchase/Image/release.png";
                HoldUnHold.lblHeader = "Un Hold the Requisition";
            }





        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }

    }

    protected void OnHold(object sender, CommandEventArgs e)
    {
        HiddenArgument.Value = e.CommandArgument.ToString();

        string[] strArgs = HiddenArgument.Value.Split(',');
        BLL_PURC_Purchase objhold = new BLL_PURC_Purchase();
        HoldUnHold.DTLog = objhold.GetRequisitionOnHoldLogHistory_ByReqsn(strArgs[0]);
        HoldUnHold.BindLog();
    }

    protected void btndivSave_Click(object sender, EventArgs e)
    {
        if (HoldUnHold.Remarks != "")
        {
            string[] strArgs = HiddenArgument.Value.Split(',');
            string sRequisitionCode = strArgs[0];
            string sDocumentCode = strArgs[1];
            string sVessel_Code = strArgs[2];

            string sREQNUM = (string)Session["REQNUM"];
            string sDeptType = (string)Session["sDeptType"];

            string sLinkType = sRequiPendingType;
            string sOnHoldName = "";
            string sOnHold = strArgs[4];

            string sRemarks = HoldUnHold.Remarks;
            string sSupplierID = "";
            if (sOnHold.ToString() == "False")
            {
                sOnHold = "1";
            }
            else
            {
                sOnHold = "0";
            }
            if (sOnHold == "1")
                sOnHoldName = "hold";
            else
                sOnHoldName = "un hold";
            try
            {
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    int iReturn = objTechService.InsRequisitionOnHoldLogHistory(sRequisitionCode, sVessel_Code, sDocumentCode, sLinkType, sOnHold, sRemarks, sSupplierID, Convert.ToInt32(Session["userid"].ToString()));
                    if (iReturn == 1)
                    {
                        DataTable dtQuotationList = new DataTable();
                        dtQuotationList.Columns.Add("Qtncode");
                        dtQuotationList.Columns.Add("amount");

                        BLL_PURC_Common.INS_Remarks(sDocumentCode, Convert.ToInt32(Session["userid"].ToString()), sRemarks, 306);
                        objTechService.InsertRequisitionStageStatus(sRequisitionCode, sVessel_Code, sDocumentCode, sOnHold, sRemarks, Convert.ToInt32(Session["USERID"]), dtQuotationList);
                        divOnHold.Visible = false;
                        String msg = String.Format("alert('Requisition " + sRequisitionCode + " has been marked as " + sOnHoldName + "'); window.close();");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

                        BindData();
                    }

                }
            }
            catch (Exception ex)
            {
                //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            }
        }
        else
        {
            String msg = String.Format("alert('Remark is mandatory field.'); window.close();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }

    protected void btndivCancel_Click(object sender, EventArgs e)
    {
        divOnHold.Visible = false;
    }

    protected void rgdNewREQ_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            BLL_PURC_Purchase objPurcBLL = new BLL_PURC_Purchase();

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                ImageButton ImgPriority = (ImageButton)(item.FindControl("ImgPriority") as ImageButton);
                Label lblSubsystem_Description = (Label)(item.FindControl("lblsubsystem") as Label);
                DataTable dtSubsystem = objPurcBLL.Get_Subsytem_Requisitionwise(Convert.ToString(item["REQUISITION_CODE"].Text), Convert.ToString(item["document_code"].Text));
                
                if (!string.IsNullOrWhiteSpace(item["URGENCY_CODE"].Text))
                {
                    if (item["URGENCY_CODE"].Text == "Urgent")
                    {
                        ImgPriority.ImageUrl = "~/Images/exclamation.gif";
                        ImgPriority.ToolTip = item["URGENCY_CODE"].Text;
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

                ImageButton btnOnHold = (ImageButton)(item.FindControl("btnOnHold") as ImageButton);

                string sOnHoldFlag = item["OnHold"].Text.ToString();
                if (sOnHoldFlag.ToString() == "False")
                {
                    btnOnHold.ImageUrl = "~/purchase/Image/OnHold.png";
                    btnOnHold.ToolTip = "Put on Hold";
                }
                else
                {

                    btnOnHold.ImageUrl = "~/purchase/Image/release.png";
                    btnOnHold.ToolTip = "Cancel Hold";
                }

                ImageButton ImgAttach = (ImageButton)(item.FindControl("ImgAttachment") as ImageButton);
                PlaceHolder DataPlaceHolder =new PlaceHolder();

                string strAttFlag = item["Attach_Status"].Text.ToString();

                if (strAttFlag == "0")
                {
                    ImgAttach.ImageUrl = "~/images/Attach.png";
                    //ImgAttach.Enabled = false;
                    ImgAttach.ToolTip = "Add Attachments";
                    ImgAttach.Attributes.Add("onmouseover", "DisplayActionInHeader('Add Attachments' ,'rgdNewREQ');"); 

                }
                else
                {
                    ImgAttach.ImageUrl = "~/images/attachment32.png";
                    ImgAttach.ToolTip = "View Attachments";
                    ImgAttach.Attributes.Add("onmouseover", "DisplayActionInHeader('View Attachments' ,'rgdNewREQ');");

                }
                #region Requisition Subsystem

                if (dtSubsystem.Rows.Count > 1)
                {
                    StringBuilder htmlTable = new StringBuilder();
                    htmlTable.Append("<table border='0' cellpadding='4' style='background-color:white;min-width:150px;' width='100%' >");
                    htmlTable.Append("<tr ><th style='background-color:gray;text-align:left;color:#FFF'>Sub-Systems</th></tr>");
                    foreach (DataRow dr in dtSubsystem.Rows)
                    {
                        htmlTable.Append("<tr><td>" + Convert.ToString(dr["Subsystem_Description"]) + "</td></tr>");
                    }
                    htmlTable.Append("</table>");
                    lblSubsystem_Description.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[] body=[" + htmlTable.ToString() + "]");
                    lblSubsystem_Description.Text = Convert.ToString(dtSubsystem.Rows[0]["Subsystem_Description"]) + "...";
                    //if(e.Item.CssClass)
                    lblSubsystem_Description.Attributes.Add("onmouseover", "this.style.backgroundColor='#FDCB0A'");
                    lblSubsystem_Description.Attributes.Add("onmouseout","this.style.backgroundColor='#fff'");
                    
                }
                else
                {
                    lblSubsystem_Description.ToolTip = Convert.ToString(dtSubsystem.Rows[0]["Subsystem_Description"]);
                    lblSubsystem_Description.Text = Convert.ToString(dtSubsystem.Rows[0]["Subsystem_Description"]);
                }

                #endregion

                #region Requisition containing critical Items requisition code should display in red color.

                Label lblCriticalFlag = (Label)(item.FindControl("lblCriticalFlag"));
                HyperLink hlinkReq = new HyperLink();
                if (lblCriticalFlag.Text == "1")
                {
                    hlinkReq = (HyperLink)item.FindControl("hlinkReq");
                    hlinkReq.ForeColor = System.Drawing.Color.Red;
                }

                #endregion

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void rgdNewREQ_DataBound(object sender, EventArgs e)
    {
        RadGrid grid = sender as RadGrid;
        int gridItems = grid.MasterTableView.Items.Count;
        if (gridItems < 7)
        {
            grid.ClientSettings.Scrolling.ScrollHeight = Unit.Pixel(gridItems * 24);
        }
    }

    protected void imgbtnCancel_Click(object s, EventArgs e)
    {
        string[] CommParam = ((ImageButton)s).CommandArgument.Split(new char[] { ',' });
        dvCancelReq.Visible = true;
        ucPurcCancelReqsnNew.ReqsnNumber = CommParam[0];
        ucPurcCancelReqsnNew.DocCode = CommParam[1];
        ucPurcCancelReqsnNew.VesselCode = CommParam[2];

    }
    protected void rgdNewREQ_SortCommand(object sender, GridSortCommandEventArgs e)
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
