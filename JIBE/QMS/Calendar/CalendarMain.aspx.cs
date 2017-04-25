using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.QMS;


public partial class QMS_Calendar_CalendarMain : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            //sign out
        }
        MeetingCal.SelectedDate = System.DateTime.Now;
        MeetingCal.SelectedDayStyle.BackColor = System.Drawing.Color.Green;
        lbTodayDtDisplay.Text = System.DateTime.Now.ToString("dd/MM/yyyy");


        if (!IsPostBack)
        {

            MeetingCal_SelectionChanged(null, null);
        
            string st = MeetingCal.SelectedDate.ToString();

            string currdate = System.DateTime.Now.ToString("yyyy/MM/dd");



            string js = "getBookings(" + Session["userid"].ToString() + ",'" + hdfDate.Value + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "getBookings", js, true);



            //string js = "CheckValidBokingDateTime(" + hdfDate.Value + ",'" +  + "');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "getBookings", js, true);

            updBooking.Update();
        }
    
    }

    public void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        
        if (e.Day.Date.DayOfWeek != DayOfWeek.Sunday) return;

        e.Cell.ApplyStyle(new Style { BackColor = System.Drawing.Color.LightGray});
        e.Day.IsSelectable = false; 

    }
    
    protected void MeetingCal_SelectionChanged(object sender, EventArgs e)
    {
        
        DateTime dtcalselDate = MeetingCal.SelectedDate;

        string js = "getBookings(" + Session["userid"].ToString() + ",'" + dtcalselDate.ToString("yyyy/MM/dd") + "');";
         ScriptManager.RegisterStartupScript(this, this.GetType(), "getBookings", js, true);

       
         hdfDate.Value = dtcalselDate.ToString("dd/MM/yyyy");

         MeetingCal.SelectedDayStyle.BackColor = System.Drawing.Color.Green;
         MeetingCal.TodayDayStyle.BackColor = System.Drawing.Color.LightGreen;
         updCalender.Update();
         updBooking.Update();
          
    }


    protected void Reload()
    {
        MeetingCal_SelectionChanged(null, null);
    
    }
    protected void lbTodayDtDisplay_Click(object sender, EventArgs e)
    {
        MeetingCal.SelectedDate = System.DateTime.Now;
      

        MeetingCal_SelectionChanged(null, null);
        MeetingCal.SelectedDayStyle.BackColor = System.Drawing.Color.Green;
        MeetingCal.TodayDayStyle.BackColor = System.Drawing.Color.Green;

        updCalender.Update();
        updBooking.Update();
    }
}