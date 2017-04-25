using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;

public partial class CrewMail_CrewMailIndex : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CalendarExtender1.Format = UDFLib.GetDateFormat();
            if (!IsPostBack)
            {
                UserAccessValidation();
                Load_PendingItems();
                Load_FleetList_Filter();
                Load_VesselList_Filter();

                Load_PacketItemReport();
                txtDatePlaced.Text = UDFLib.ConvertUserDateFormat(DateTime.Today.ToShortDateString());
            }
            string js = "$('.vesselinfo').InfoBox();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript", js, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnNewItem.Visible = false;
        }
        if (objUA.Edit == 0)
        {

        }
        if (objUA.Delete == 0)
        {
            GridView_MailItems.Columns[GridView_MailItems.Columns.Count - 1].Visible = false;
        }
        if (objUA.Approve == 0)
        {
        }

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void Load_FleetList_Filter()
    {
        try
        {
            int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

            DataTable dt = objVessel.GetFleetList(UserCompanyID);
            ddlFleetFilter.DataSource = dt;
            ddlFleetFilter.DataTextField = "NAME";
            ddlFleetFilter.DataValueField = "CODE";
            ddlFleetFilter.DataBind();
            ddlFleetFilter.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    public void Load_VesselList_Filter()
    {
        try
        {
            int Fleet_ID = int.Parse(ddlFleetFilter.SelectedValue);
            int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
            int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

            if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
                Vessel_Manager = UserCompanyID;

            ddlVesselFilter.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);
            ddlVesselFilter.DataTextField = "VESSEL_NAME";
            ddlVesselFilter.DataValueField = "VESSEL_ID";
            ddlVesselFilter.DataBind();
            ddlVesselFilter.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ddlFleetFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList_Filter();
    }
    protected void ddlVesselFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_PendingItems();
        Load_PacketItemReport();
    }
    protected void ddlPacketStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_PendingItems();
        Load_PacketItemReport();
    }

    protected void txtSearchText_TextChanged(object sender, EventArgs e)
    {
        Load_PendingItems();
        Load_PacketItemReport();
    }
    protected void Load_PendingItems()
    {
        try
        {
            int SelectRecordCount = ucCustomPager_PendingItems.isCountRecord;
            int? PAGE_SIZE = ucCustomPager_PendingItems.PageSize;
            int? PAGE_INDEX = ucCustomPager_PendingItems.CurrentPageIndex;

            int FleetCode = UDFLib.ConvertToInteger(ddlFleetFilter.SelectedValue);
            int Vessel_ID = UDFLib.ConvertToInteger(ddlVesselFilter.SelectedValue);
            DataTable dt = BLL_Crew_CrewMail.Get_PendingItems(FleetCode, Vessel_ID, GetSessionUserID(), txtSearchText.Text, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);

            GridView_MailItems.DataSource = dt;
            GridView_MailItems.DataBind();

            ucCustomPager_PendingItems.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager_PendingItems.BuildPager();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected bool ValidateEntry()
    {
        bool Ret = true;
        string js = "";

        if (txtDatePlaced.Text.Trim() == "")
        {
            Ret = false;
            js = "alert('Enter Date Placed');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
        }
        if (!UDFLib.DateCheck(txtDatePlaced.Text.Trim()))
        {
            Ret = false;
            js = "alert('Enter valid Date Placed"+UDFLib.DateFormatMessage()+"');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
        }
        if (ddlVesselFilter.SelectedValue == "0")
        {
            Ret = false;
            js = "alert('Select Vessel');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
        }
        if (txtDesc.Text.Trim() == "")
        {
            Ret = false;
            js = "alert('Enter item description');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
        }
        return Ret;
    }

    protected void SaveItem()
    {
        try
        {
            int Vessel_ID = UDFLib.ConvertToInteger(ddlVesselFilter.SelectedValue);
            decimal Item_Qty = UDFLib.ConvertToDecimal(txtQty.Text);
            string DatePlaced = UDFLib.ConvertUserDateFormat(DateTime.Today.ToShortDateString());

            int ItemID = BLL_Crew_CrewMail.INSERT_CrewMailItem(Vessel_ID, txtRefNo.Text, txtDesc.Text, Item_Qty, txtRemarks.Text, DatePlaced, GetSessionUserID());
            txtRefNo.Text = "";
            txtDesc.Text = "";
            txtQty.Text = "";
            txtRemarks.Text = "";
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateEntry())
            {
                SaveItem();
                Load_PendingItems();
                string js = "alert('Item Added to Pigeon Hole');closeDiv('dvAddItem');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "close", js, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnSaveAndAdd_Click(object sender, EventArgs e)
    {
        if (ValidateEntry())
        {
            SaveItem();
            Load_PendingItems();
            string js = "alert('Item Added to Pigeon Hole');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
        }

    }

    protected void GridView_MailItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_MailItems.DataKeys[e.RowIndex].Value.ToString());
        int Ret = BLL_Crew_CrewMail.DELETE_CrewMailItem(ID, GetSessionUserID());
        Load_PendingItems();

    }

    protected void btnAddToPacket_Click(object sender, EventArgs e)
    {
    }

    protected void Load_PacketItemReport()
    {
        try
        {
            int SelectRecordCount = ucCustomPager_PacketItems.isCountRecord;
            int? PAGE_SIZE = ucCustomPager_PacketItems.PageSize;
            int? PAGE_INDEX = ucCustomPager_PacketItems.CurrentPageIndex;

            int FleetCode = UDFLib.ConvertToInteger(ddlFleetFilter.SelectedValue);
            int Vessel_ID = UDFLib.ConvertToInteger(ddlVesselFilter.SelectedValue);
            int Status = UDFLib.ConvertToInteger(ddlPacketStatus.SelectedValue);
            string SearchText = txtSearchText.Text;

            DataSet ds = BLL_Crew_CrewMail.Get_PacketItems(FleetCode, Vessel_ID, Status, GetSessionUserID(), SearchText, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);

            //UDFLib.AddParentTable(ds.Tables[0], "Packets", new string[] { "PacketID" },
            //new string[] { "Vessel_Short_Name", "PacketRef", "AirwayBill", "ETA", "PORT_NAME", "SentUsing", "PacketStatus", "Expected_Date_Dispatch", "Verified_By", "Verified_Remarks", "DateReceived_ONBD", "VesselPIC", "VesselPICRemarks" }, "PacketItems");

            //rpt1.DataSource = ds;
            //rpt1.DataMember = "Packets";
            //rpt1.DataBind();

            GridView_Packets.DataSource = ds.Tables[0];
            GridView_Packets.DataBind();

            ucCustomPager_PacketItems.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager_PacketItems.BuildPager();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected void GridView_Packets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Image ImgVerifiedRemarks = (Image)(e.Row.FindControl("ImgVerifiedRemarks"));
            if (ImgVerifiedRemarks != null)
            {
                if (DataBinder.Eval(e.Row.DataItem, "Verified_Remarks").ToString() != "")
                    ImgVerifiedRemarks.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Remarks] body=[" + DataBinder.Eval(e.Row.DataItem, "Verified_Remarks").ToString() + "]");
                else
                    ImgVerifiedRemarks.Visible = false;
            }

            Image ImgVesselPICRemarks = (Image)(e.Row.FindControl("ImgVesselPICRemarks"));
            if (ImgVesselPICRemarks != null)
            {
                if (DataBinder.Eval(e.Row.DataItem, "VesselPICRemarks").ToString() != "")
                    ImgVesselPICRemarks.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Remarks] body=[" + DataBinder.Eval(e.Row.DataItem, "VesselPICRemarks").ToString() + "]");
                else
                    ImgVesselPICRemarks.Visible = false;
            }

        }
    }



    //protected void rpt1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        Image ImgVerifiedRemarks = (Image)(e.Item.FindControl("ImgVerifiedRemarks"));
    //        if (ImgVerifiedRemarks != null)
    //        {
    //            if (DataBinder.Eval(e.Item.DataItem, "Verified_Remarks").ToString() != "")
    //                ImgVerifiedRemarks.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Remarks] body=[" + DataBinder.Eval(e.Item.DataItem, "Verified_Remarks").ToString() + "]");
    //            else
    //                ImgVerifiedRemarks.Visible = false;
    //        }

    //        Image ImgVesselPICRemarks = (Image)(e.Item.FindControl("ImgVesselPICRemarks"));
    //        if (ImgVesselPICRemarks != null)
    //        {
    //            if (DataBinder.Eval(e.Item.DataItem, "VesselPICRemarks").ToString() != "")
    //                ImgVesselPICRemarks.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Remarks] body=[" + DataBinder.Eval(e.Item.DataItem, "VesselPICRemarks").ToString() + "]");
    //            else
    //                ImgVesselPICRemarks.Visible = false;
    //        }
    //    }
    //}
    //protected void rpt1_ItemCommand(Object Sender, RepeaterCommandEventArgs e)
    //{
    //    string js = "";
    //    int PacketID = 0;
    //    try
    //    {

    //        if (e.CommandArgument.ToString() != "")
    //        {
    //            if (e.CommandName == "SendMail_Vessel")
    //            {
    //                PacketID = UDFLib.ConvertToInteger(e.CommandArgument.ToString());

    //            }
    //        }
    //    }
    //    catch
    //    { }
    //}

}