using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.QMS;
using System.Data;
using SMS.Properties;


public partial class QMS_Calendar_CalendarBookingEntry : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {

        DivUserList.Visible = false;
        lblError.Text = "";

        txtBookingDate.Enabled = false;

        if (!IsPostBack)
        {

            DataTable dtAttendees = new DataTable();

            DataColumn dtclm = new DataColumn("UserId");
            dtAttendees.Columns.Add(dtclm);
            dtAttendees.Columns.Add("UserName");
            dtAttendees.PrimaryKey = new DataColumn[] { dtclm };
            ViewState["vdtAttendees"] = dtAttendees;

            ViewState["SelectedDate"] = null;
            ViewState["ISMEETINGTIMECHANGED"] = "0";
            btnDelete.Enabled = false;


            if (Request.QueryString["bookingid"] != null)
            {
                DataTable dt = BLL_BookingCalendar.BookingCalenderList(Convert.ToInt32(Request.QueryString["bookingid"].ToString()), Convert.ToInt32(Session["userid"].ToString()));
                DataTable dtAttendeesinfo = BLL_BookingCalendar.BooingCalenderGetAttendeesInfo(Convert.ToInt32(Request.QueryString["bookingid"]));

                if (dtAttendeesinfo.Rows.Count > 0)
                {
                    dtAttendeesinfo.PrimaryKey = new DataColumn[] { dtAttendeesinfo.Columns["UserID"] };
                    ViewState["vdtAttendees"] = dtAttendeesinfo;
                    lstAttendees.DataTextField = "username";
                    lstAttendees.DataValueField = "userid";
                    lstAttendees.DataSource = dtAttendeesinfo;
                    lstAttendees.DataBind();
                }

                if (dt.Rows.Count > 0)
                {
                    ddltimefrom.ClearSelection();
                    ddltimeto.ClearSelection();


                    DataRow dr = dt.Rows[0];
                    txtBookingDate.Text = dr["BOOKING_ON"].ToString();
                    txtRemarks.Text = dr["REMARKS"].ToString();
                    txtRoom.Text = dr["ROOM"].ToString();
                    ddltimefrom.Items.FindByValue(dr["TIME_FROM"].ToString()).Selected = true;
                    ddltimeto.Items.FindByValue(dr["TIME_TO"].ToString()).Selected = true;

                    ViewState["OldTimeFrom"] = dr["TIME_FROM"].ToString();
                    ViewState["OldTimeTo"] = dr["TIME_TO"].ToString();

                    hdfRoomid.Value = dr["ROOMID"].ToString();
                    lblBookedby.Text = dr["First_Name"].ToString();
                    lblBookedOn.Text = dr["CREATED_ON"].ToString();

                    ViewState["SelectedDate"] = dr["BOOKING_ON"].ToString();
                    ViewState["BOOKING_BY"] = dr["BOOKING_BY"].ToString();

                    /* User can edit his own booking  ::  for other button will be disable */

                    if (ViewState["BOOKING_BY"].ToString() == Session["userid"].ToString())
                    {
                        btnDelete.Enabled = true;
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        btnDelete.Enabled = false;
                        btnSave.Enabled = false;
                    }

                    ButtonEnableDisable(Convert.ToDateTime(ViewState["SelectedDate"]));

                    //RemovePastTimeToDropDown();
                }

            }
            else
            {

                string TimeFrom = Request.QueryString["TimeFrom"].ToString();
                string RoomID = Request.QueryString["RoomID"].ToString();
                txtBookingDate.Text = Request.QueryString["SelDate"].ToString();

                ViewState["SelectedDate"] = Convert.ToDateTime(txtBookingDate.Text);

                ButtonEnableDisable(Convert.ToDateTime(ViewState["SelectedDate"]));


                if (RoomID == "R1")
                {
                    txtRoom.Text = "Board Room";
                    hdfRoomid.Value = "1";
                }
                else if (RoomID == "R2")
                {
                    txtRoom.Text = "Crew Briefing Room";
                    hdfRoomid.Value = "2";
                }
                else if (RoomID == "R3")
                {
                    txtRoom.Text = "Mgmt Meeting Room";
                    hdfRoomid.Value = "3";
                }

                ddltimefrom.ClearSelection();
                ddltimefrom.Items.FindByValue(TimeFrom).Selected = true;

               

                ddltimeto.ClearSelection();
                ddltimeto.SelectedIndex = ddltimefrom.SelectedIndex + 1;

                RemovePastTimeToDropDown();

                ddltimeto.SelectedIndex = 0;

            }

            BindInviteUserList();
        }


        UserAccessValidation();


    }

    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUA.View == 0)
        {

             Response.Write("You don't have sufficient previlege to access the requested page");
             Response.End();
        }

        if (objUA.Add == 0)
        {
            btnSave.Enabled = false;
            btnInvite.Enabled = false;

        }
        if (objUA.Edit == 0)
        {

            
        }
        if (objUA.Delete == 0)
        {
            btnDelete.Enabled = false;

        }




    }




    protected void RemovePastTimeToDropDown()
    {

        for (int i = ddltimefrom.SelectedIndex-1 ; i >=0; i--)
        {
            ddltimeto.Items.RemoveAt(i); 
        
        }
    
    }



    protected void ButtonEnableDisable(DateTime selecteddate)
    {

        if (selecteddate < DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy")))
        {
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnDelete.Enabled = false;
            btnInvite.Enabled = false;

        }


    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (ValidationOnSave())
        {

            int? bookingid = null;
            int crewfbmidout = 0;


            if (Request.QueryString["bookingid"] != null)
            {
                bookingid = Convert.ToInt32(Request.QueryString["bookingid"]);

                if ((ddltimefrom.SelectedItem.Text != ViewState["OldTimeFrom"].ToString()) || (ddltimeto.SelectedItem.Text != ViewState["OldTimeTo"].ToString()))
                {
                    /* if previous time (timefrom OR timeTo) is differ from current selected time then Email should go to attendees  */
                    ViewState["ISMEETINGTIMECHANGED"] = "1";
                }
            }

            string SelectedDate = "";

            if (ViewState["SelectedDate"] != null)
                SelectedDate = ViewState["SelectedDate"].ToString();
            else
                SelectedDate = System.DateTime.Now.ToString("dd/MM/yyyy");

            int bookingid_out = 0;


            int ret = BLL_BookingCalendar.BookingCalenderSave(Convert.ToInt32(Session["userid"].ToString()), bookingid, Convert.ToDateTime(SelectedDate)
                , ddltimefrom.SelectedValue, ddltimeto.SelectedValue, hdfRoomid.Value, txtRoom.Text, txtRemarks.Text, ref bookingid_out);


            if (bookingid_out != 0)
            {

                int retval = BLL_BookingCalendar.BookingCalenderAttendeesSave(Convert.ToInt32(Session["userid"].ToString()), bookingid_out, Convert.ToInt32(ViewState["ISMEETINGTIMECHANGED"]), (DataTable)ViewState["vdtAttendees"]);

                String script = String.Format("parent.hideModal('dvBookingPopUp');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);

                String Reload = String.Format("parent.RefreshFromchild();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg1", Reload, true);
            }
            else
            {
                lblError.Text = "The selected time slot is Conflicting with an existing booking ! Please select a different time slot.";
            }

        }

    }


    public bool ValidationOnSave()
    {

        if (Request.QueryString["bookingid"] != null) 
        {

            if (ddltimefrom.SelectedIndex == 0)
            {
                lblError.Text = "From Time is required.";
                return false;
            }
            else if (ddltimeto.SelectedIndex == 0)
            {
                lblError.Text = "Time To is required.";
                return false;
            }
            else if (ddltimefrom.SelectedValue == ddltimeto.SelectedValue)
            {
                lblError.Text = "Time from and  time To cannot be equal";
                return false;
            }
            else if (Convert.ToInt32(ddltimefrom.SelectedValue) > Convert.ToInt32(ddltimeto.SelectedValue))
            {
                lblError.Text = "Time from cannot be less then Time to";
                return false;
            }
            else if (txtRoom.Text.Trim() == "")
            {
                lblError.Text = "Booking Room is required.";
                return false;
            }
            else if (txtRemarks.Text.Trim() == "")
            {
                lblError.Text = "Purpose is required.";
                return false;
            }

            return true;
        }
        else 
        {
            if (ddltimefrom.SelectedIndex == 0)
            {
                lblError.Text = "From Time is required.";
                return false;
            }
            else if (txtRoom.Text.Trim() == "")
            {
                lblError.Text = "Booking Room is required.";
                return false;
            }
            else if (txtRemarks.Text.Trim() == "")
            {
                lblError.Text = "Purpose is required.";
                return false;
            }

            return true;
        }

    }


    protected void btnClose_Click(object sender, EventArgs e)
    {

        String script = String.Format("parent.hideModal('dvBookingPopUp');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "close", script, true);

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int bookingid_out = 0;

        
        int retval = BLL_BookingCalendar.BookingCalenderDelete(Convert.ToInt32(Session["userid"].ToString()), Convert.ToInt32(Request.QueryString["bookingid"].ToString()) , ref bookingid_out );


        if (bookingid_out != 0)
        {
            String Reload = String.Format("parent.RefreshFromchild();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdelete", Reload, true);
        }
        else
        {
            lblError.Text = "Booking made for the past, cannot be deleted.";
        }

    }

    protected void btnInvite_Click(object sender, EventArgs e)
    {

        DataTable dtAttendees = (DataTable)ViewState["vdtAttendees"];
        DivUserList.Visible = true;

        foreach (ListItem chkitem in chklistInviteUser.Items)
        {

            if (dtAttendees.Rows.Contains(chkitem.Value))
            {
                chkitem.Selected = true;
            }
            else
            {
                chkitem.Selected = false;
            }

        }
    }


    protected void btnSendInvite_click(object sender, EventArgs e)
    {

        DataTable dtAttendees = (DataTable)ViewState["vdtAttendees"];
        DataRow dr;

        foreach (ListItem lst in chklistInviteUser.Items)
        {
            dr = dtAttendees.NewRow();
            if (lst.Selected)
            {
                if (!dtAttendees.Rows.Contains(lst.Value))
                {
                    dr["userid"] = lst.Value;
                    dr["username"] = lst.Text;
                    dtAttendees.Rows.Add(dr);
                }
            }
            else
            {
                DataRow dritem = dtAttendees.Rows.Find(lst.Value);
                if (dritem != null)
                {

                    dtAttendees.Rows.Remove(dritem);
                }
            }
        }


        ViewState["vdtAttendees"] = dtAttendees;

        lstAttendees.Items.Clear();
        lstAttendees.DataTextField = "username";
        lstAttendees.DataValueField = "userid";
        lstAttendees.DataSource = dtAttendees;
        lstAttendees.DataBind();

        DivUserList.Visible = false;

        //String script = String.Format("parent.hideModal('dvBookingPopUp');");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SendInvite", script, true);

    }

    protected void btnCancel_click(object sender, EventArgs e)
    {

        DivUserList.Visible = false;
    }


    public void BindInviteUserList()
    {
        try
        {

            DataTable dt = BLL_BookingCalendar.BookingCalenderInviteUserList(Convert.ToInt32(Session["userid"].ToString()));

            chklistInviteUser.DataSource = dt;
            chklistInviteUser.DataTextField = "UserName";
            chklistInviteUser.DataValueField = "UserID";
            chklistInviteUser.DataBind();

        }
        catch (Exception ex)
        {


        }
    }


}