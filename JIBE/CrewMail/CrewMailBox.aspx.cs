using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;

public partial class CrewMail_CrewMailBox : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        calFrom.Format = UDFLib.GetDateFormat();
        CalendarExtender1.Format = UDFLib.GetDateFormat();

        UserAccessValidation();
        if (!IsPostBack)
        {
            Load_FleetList_Filter();
            Load_VesselList_Filter();

            Load_PendingItems();
            Load_PacketItemReport();

            pnlAddToPacket.Visible = false;
            txtDatePlaced.Text = DateTime.Today.ToString(UDFLib.GetDateFormat());
            //txtDateSent.Text = DateTime.Today.ToString("dd/MM/yyyy");

            txtCoverLetter.config.toolbar = new object[]
            {                
                new object[] { "Print"},   
                new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
                new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
                new object[] { "Styles", "Format", "Font", "FontSize" },
                new object[] { "TextColor", "BGColor" }                
            };

            txtCoverLetter.Height = 610;
            txtCoverLetter.Width = 1190;

            txtCoverLetter.ResizeEnabled = false;
        }
        string js = "$('.vesselinfo').InfoBox();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript", js, true);

    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnNewItemItem.Visible = false;
            btnSaveItemAndAdd.Enabled = false;
            btnSaveItemAndClose.Enabled = false;

            btnSavePacket.Enabled = false;
            btnSaveAndSend.Enabled = false;
            btnAddToPacket.Enabled = false;
        }
        if (objUA.Edit == 0)
        {

        }
        if (objUA.Delete == 0)
        {
            GridView_MailItems.Columns[GridView_MailItems.Columns.Count - 1].Visible = false;
            btnDiscardPacket.Enabled = false;
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
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        DataTable dt = objVessel.GetFleetList(UserCompanyID);
        ddlFleetFilter.DataSource = dt;
        ddlFleetFilter.DataTextField = "NAME";
        ddlFleetFilter.DataValueField = "CODE";
        ddlFleetFilter.DataBind();
        ddlFleetFilter.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

    }
    public void Load_VesselList_Filter()
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
        ddlVesselFilter.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }

    protected void ddlFleetFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList_Filter();
    }
    protected void ddlVesselFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_PendingItems();
        Load_VesselOpenPacket();
        Load_PacketItemReport();

        if (ddlVesselFilter.SelectedValue == "0")
        {
            pnlAddToPacket.Visible = false;
            pnlNewPacket.Visible = false;
            //pnlSavePacket.Visible = false;
        }
        else
        {
            pnlAddToPacket.Visible = true;
            pnlNewPacket.Visible = true;
            //pnlSavePacket.Visible = true;
        }
    }
    protected void txtSearchText_TextChanged(object sender, EventArgs e)
    {
        Load_PendingItems();
    }

    protected void Load_PendingItems()
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
    protected void Load_VesselOpenPacket()
    {
        int Vessel_ID = UDFLib.ConvertToInteger(ddlVesselFilter.SelectedValue);
        ddlVesselPackets.Items.Clear();
        //hdnPacketID.Value = "0";

        DataSet ds = BLL_Crew_CrewMail.Get_VesselOpenPacket(Vessel_ID, GetSessionUserID());

        ddlVesselPackets.DataSource = ds.Tables[0];
        ddlVesselPackets.DataBind();


        if (ds.Tables[0].Rows.Count > 0)
        {
            //ddlVesselPackets.Items.Add(new ListItem(ds.Tables[0].Rows[0]["PacketRef"].ToString(), ds.Tables[0].Rows[0]["PacketID"].ToString()));
            //hdnPacketID.Value = ds.Tables[0].Rows[0]["PacketID"].ToString();

            txtDateSent.Text = ds.Tables[0].Rows[0]["Expected_Date_Dispatch"].ToString() == "" ? "" : DateTime.Parse(ds.Tables[0].Rows[0]["Expected_Date_Dispatch"].ToString()).ToString(UDFLib.GetDateFormat());
            txtSentUsing.Text = ds.Tables[0].Rows[0]["SentUsing"].ToString();
            txtAirwayBill.Text = ds.Tables[0].Rows[0]["AirwayBill"].ToString();

            //ctlDelPort.SelectedValue = ds.Tables[0].Rows[0]["Port"].ToString();
            hdnPortID.Value = ds.Tables[0].Rows[0]["Port"].ToString();
            txtPort.Text = ds.Tables[0].Rows[0]["Port_Name"].ToString();
            if (Convert.ToString(ds.Tables[0].Rows[0]["ETA"]) != "")
            {
                txtArrival.Text = UDFLib.ConvertUserDateFormatTime(ds.Tables[0].Rows[0]["ETA"].ToString());
            }
            else
                txtArrival.Text = "";

            txtApproverRemarks.Text = ds.Tables[0].Rows[0]["VERIFIED_REMARKS"].ToString();

        }

        GridView_SelectedItems.DataSource = ds.Tables[1];
        GridView_SelectedItems.DataBind();

        if (ds.Tables[1].Rows.Count > 0)
        {
            btnDiscardPacket.Enabled = true;
            btnSaveAndSend.Enabled = true;
            btnSaveAndSend.Enabled = true;
        }
        else
        {
            btnDiscardPacket.Enabled = false;
            btnSaveAndSend.Enabled = false;
            btnSaveAndSend.Enabled = false;
        }
    }

    protected void Load_MailPacket(int PacketID)
    {
        DataSet ds = BLL_Crew_CrewMail.Get_MailPacket(PacketID, GetSessionUserID());

        pnlNewPacket.Visible = true;

        if (ddlVesselPackets.Items.FindByValue(PacketID.ToString()) != null)
        {
            ddlVesselPackets.Text = ds.Tables[0].Rows[0]["PacketID"].ToString();
        }
        else
        {
            ddlVesselPackets.DataSource = ds.Tables[0];
            ddlVesselPackets.DataBind();
        }

        GridView_SelectedItems.DataSource = ds.Tables[1];
        GridView_SelectedItems.DataBind();

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtDateSent.Text = ds.Tables[0].Rows[0]["Expected_Date_Dispatch"].ToString() == "" ? "" : DateTime.Parse(ds.Tables[0].Rows[0]["Expected_Date_Dispatch"].ToString()).ToString("dd/MM/yyyy");
            txtSentUsing.Text = ds.Tables[0].Rows[0]["SentUsing"].ToString();
            txtAirwayBill.Text = ds.Tables[0].Rows[0]["AirwayBill"].ToString();

            hdnPortID.Value = ds.Tables[0].Rows[0]["Port"].ToString();
            txtPort.Text = ds.Tables[0].Rows[0]["Port_Name"].ToString();
            txtArrival.Text = ds.Tables[0].Rows[0]["ETA"].ToString();
            txtApproverRemarks.Text = ds.Tables[0].Rows[0]["VERIFIED_REMARKS"].ToString();

            if (ds.Tables[0].Rows[0]["Status"].ToString() == "0" || ds.Tables[0].Rows[0]["Status"].ToString() == "")
            {
                btnSavePacket.Enabled = true;
                btnSaveAndSend.Enabled = true;
                btnDiscardPacket.Enabled = true;

                txtDateSent.Enabled = true;
                txtAirwayBill.Enabled = true;
                txtSentUsing.Enabled = true;
                txtApproverRemarks.Enabled = true;
                lnkPortCall.Visible = true;

                GridView_SelectedItems.Columns[GridView_SelectedItems.Columns.Count - 1].Visible = true;
            }
            else if (ds.Tables[0].Rows[0]["Status"].ToString() == "1")
            {
                btnSavePacket.Enabled = true;
                btnSaveAndSend.Enabled = false;
                btnDiscardPacket.Enabled = false;

                txtDateSent.Enabled = false;
                txtAirwayBill.Enabled = false;
                txtSentUsing.Enabled = false;
                txtApproverRemarks.Enabled = false;

                lnkPortCall.Visible = true;

                GridView_SelectedItems.Columns[GridView_SelectedItems.Columns.Count - 1].Visible = false;
            }

            else
            {
                btnSavePacket.Enabled = false;
                btnSaveAndSend.Enabled = false;
                btnDiscardPacket.Enabled = false;

                txtDateSent.Enabled = false;
                txtAirwayBill.Enabled = false;
                txtSentUsing.Enabled = false;
                txtApproverRemarks.Enabled = false;
                lnkPortCall.Visible = false;

                GridView_SelectedItems.Columns[GridView_SelectedItems.Columns.Count - 1].Visible = false;
            }
        }
        else
        {
            btnDiscardPacket.Enabled = false;
            btnSavePacket.Enabled = false;
            btnSaveAndSend.Enabled = false;
        }

        UserAccessValidation();
    }

    protected bool ValidateEntry()
    {
        bool Ret = true;
        string js = "";

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
        if (!UDFLib.DateCheck(txtDatePlaced.Text))
        {
            Ret = false;
            js = "alert('Enter valid Date Placed" + UDFLib.DateFormatMessage() + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
        }
        return Ret;
    }

    protected void btnAddNewItem_Click(object sender, EventArgs e)
    {
        pnlAddItem.Visible = true;
    }
    protected void SaveItem()
    {
        int Vessel_ID = UDFLib.ConvertToInteger(ddlVesselFilter.SelectedValue);
        int PacketID = UDFLib.ConvertToInteger(ddlVesselPackets.SelectedValue);

        decimal Item_Qty = UDFLib.ConvertToDecimal(txtQty.Text);

        int ItemID = BLL_Crew_CrewMail.INSERT_CrewMailItem(Vessel_ID, txtRefNo.Text, txtDesc.Text, Item_Qty, txtRemarks.Text, UDFLib.ConvertUserDateFormat(txtDatePlaced.Text.ToString()), GetSessionUserID());

        if (chkAddToPacket.Checked == true)
        {
            BLL_Crew_CrewMail.AddItemTo_Packet(Vessel_ID, PacketID, ItemID, GetSessionUserID());
        }

        txtRefNo.Text = "";
        txtDesc.Text = "";
        txtRemarks.Text = "";
        txtQty.Text = "";

    }
    protected void btnSaveItemAndClose_Click(object sender, EventArgs e)
    {
        if (ValidateEntry())
        {
            SaveItem();
            Load_VesselOpenPacket();
            Load_PacketItemReport();
            Load_PendingItems();
            pnlAddItem.Visible = false;
        }
    }
    protected void btnSaveItemAndAdd_Click(object sender, EventArgs e)
    {
        if (ValidateEntry())
        {
            SaveItem();
            Load_VesselOpenPacket();
            Load_PacketItemReport();
            Load_PendingItems();
        }
    }
    protected void btnCloseAddItem_Click(object sender, EventArgs e)
    {
        txtDatePlaced.Text = DateTime.Today.ToString(UDFLib.GetDateFormat());
        txtRefNo.Text = "";
        txtDesc.Text = "";
        txtRemarks.Text = "";
        txtQty.Text = "";
        pnlAddItem.Visible = false;
    }

    protected void GridView_MailItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_MailItems.DataKeys[e.RowIndex].Value.ToString());
        int Ret = BLL_Crew_CrewMail.DELETE_CrewMailItem(ID, GetSessionUserID());
        Load_PendingItems();

    }
    protected void GridView_MailItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Image ImgRemarks = (Image)(e.Row.FindControl("ImgRemarks"));
        if (ImgRemarks != null)
        {
            ImgRemarks.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Remarks] body=[" + DataBinder.Eval(e.Row.DataItem, "Item_Remarks").ToString() + "]");
        }
    }
    protected void GridView_SelectedItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridView_SelectedItems.DataKeys[e.RowIndex].Value.ToString());
        int Ret = BLL_Crew_CrewMail.RemoveItem_FromPacket(ID, GetSessionUserID());
        Load_PendingItems();
        Load_VesselOpenPacket();
        Load_PacketItemReport();
    }
    protected void GridView_SelectedItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Image ImgRemarks = (Image)(e.Row.FindControl("ImgRemarks"));
        if (ImgRemarks != null)
        {
            ImgRemarks.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Information] body=[Placed By: " + DataBinder.Eval(e.Row.DataItem, "Placed_By").ToString() + "<br>Dept: " + DataBinder.Eval(e.Row.DataItem, "Dept").ToString() + "<br>Date Placed: " + UDFLib.ConvertUserDateFormat(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Date_Placed"))) + "<br>Remarks: " + DataBinder.Eval(e.Row.DataItem, "Item_Remarks").ToString() + "]");
        }
    }

    protected void btnAddToPacket_Click(object sender, EventArgs e)
    {
        int Vessel_ID = UDFLib.ConvertToInteger(ddlVesselFilter.SelectedValue);
        int PacketID = UDFLib.ConvertToInteger(ddlVesselPackets.SelectedValue);

        if (PacketID > 0)
        {
            foreach (GridViewRow gr in GridView_MailItems.Rows)
            {
                if (gr.FindControl("chkSelect") != null)
                {
                    if (((CheckBox)gr.FindControl("chkSelect")).Checked == true)
                    {
                        if (gr.FindControl("hdnItemID") != null)
                        {
                            int ID = UDFLib.ConvertToInteger(((HiddenField)gr.FindControl("hdnItemID")).Value);
                            BLL_Crew_CrewMail.AddItemTo_Packet(Vessel_ID, PacketID, ID, GetSessionUserID());
                        }

                    }
                }
            }

        }
        Load_PendingItems();
        Load_VesselOpenPacket();
        Load_PacketItemReport();
    }

    protected void btnSavePacket_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridView_SelectedItems.Rows.Count > 0)
            {
                int PortID = UDFLib.ConvertToInteger(hdnPortID.Value);
                string ArrivalDate = txtArrival.Text;

                int Res = BLL_Crew_CrewMail.Save_MailPacket(UDFLib.ConvertToInteger(ddlVesselPackets.SelectedValue), UDFLib.ConvertUserDateFormat(txtDateSent.Text), txtSentUsing.Text, txtAirwayBill.Text, PortID, ArrivalDate, GetSessionUserID(), txtApproverRemarks.Text);
                if (Res == 1)
                {
                    Load_PacketItemReport();
                    if (ddlVesselPackets.SelectedItem != null)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "saved", "alert('Packet(" + ddlVesselPackets.SelectedItem.Text + ") Saved');", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "saved", "alert('No item selected for the Packet!!');", true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void btnSaveAndSend_Click(object sender, EventArgs e)
    {
        bool valid = true;


        int PortID = UDFLib.ConvertToInteger(hdnPortID.Value);
        string ArrivalDate = UDFLib.ConvertUserDateFormat(txtArrival.Text);

        if (GridView_SelectedItems.Rows.Count > 0)
        {
            if (valid == true)
            {
                int Res = BLL_Crew_CrewMail.SaveAndSend_MailPacket(UDFLib.ConvertToInteger(ddlVesselPackets.SelectedValue), UDFLib.ConvertToDefaultDt(txtDateSent.Text), txtSentUsing.Text, txtAirwayBill.Text, PortID, ArrivalDate, GetSessionUserID(), txtApproverRemarks.Text);
                if (Res == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "saved", "alert('Packet(" + ddlVesselPackets.SelectedItem.Text + ") sent to Vessel');", true);

                    Load_VesselOpenPacket();
                    Load_PacketItemReport();
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "saved", "alert('No item selected for the Packet!!');", true);
        }
    }
    protected void btnDiscardPacket_Click(object sender, EventArgs e)
    {
        int Res = BLL_Crew_CrewMail.Discard_MailPacket(UDFLib.ConvertToInteger(ddlVesselPackets.SelectedValue), GetSessionUserID());
        if (Res == 1)
        {
            Load_VesselOpenPacket();
            Load_PacketItemReport();
            Load_PendingItems();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "saved", "alert('Packet discarded');", true);
        }

    }
    protected void btnCoverLetter_Click(object sender, EventArgs e)
    {

    }

    protected void Load_PortCalls()
    {
        string js = "";

        int Vessel_ID = UDFLib.ConvertToInteger(ddlVesselFilter.SelectedValue);
        if (Vessel_ID > 0)
        {
            gvPortCalls.DataSource = objCrew.Get_PortCall_List(Vessel_ID);
            gvPortCalls.DataBind();
            UpdatePanel_PortCalls.Update();

            js = "overlay()";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertpc", js, true);
        }
        else
        {
            js = "alert('Select vessel to view Port Calls');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertpc", js, true);
        }

    }

    protected void lnkPortCall_Click(object sender, EventArgs e)
    {
        Load_PortCalls();
    }
    protected void ddlVesselPackets_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_MailPacket(UDFLib.ConvertToInteger(ddlVesselPackets.SelectedValue));
    }

    protected void btnSavePortCall_Click(object sender, EventArgs e)
    {
        Label lblPortName = (Label)gvPortCalls.SelectedRow.FindControl("lblPort_Name");
        if (lblPortName != null)
        {
            txtPort.Text = lblPortName.Text;
        }
        HiddenField hdnSelPort = (HiddenField)gvPortCalls.SelectedRow.FindControl("hdnPortID");
        if (hdnSelPort != null)
        {
            hdnPortID.Value = hdnSelPort.Value;
        }
        Label lblArrivalDate = (Label)gvPortCalls.SelectedRow.FindControl("lblArrival");
        if (lblArrivalDate != null)
            txtArrival.Text = lblArrivalDate.Text;
    }

    protected void Load_PacketItemReport()
    {
        int SelectRecordCount = ucCustomPager_PacketItems.isCountRecord;
        int? PAGE_SIZE = ucCustomPager_PacketItems.PageSize;
        int? PAGE_INDEX = ucCustomPager_PacketItems.CurrentPageIndex;

        int FleetCode = UDFLib.ConvertToInteger(ddlFleetFilter.SelectedValue);
        int Vessel_ID = UDFLib.ConvertToInteger(ddlVesselFilter.SelectedValue);
        int Status = 0;
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



}