using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SMS.Data.QMS;

namespace SMS.Business.QMS 
{
    public class BLL_BookingCalendar
    {

        public static DataTable BookingCalenderList(int? bookingid , int? userid)
        {
            return DAL_BookingCalendar.BookingCalenderList(bookingid, userid);
        }
       
        public static SqlDataReader BookingCalenderListForDate(DateTime? BookingDate, int? userid)
        {
            return DAL_BookingCalendar.BookingCalenderListForDate(BookingDate, userid);
        }

        public static int BookingCalenderSave(int? userid, int? bookingid, DateTime? bookingdate, string timefrom, string timeto, string roomid, string roomname, string remaks, ref int bookingidout)
        {
            return DAL_BookingCalendar.BookingCalenderSave(userid, bookingid, bookingdate, timefrom, timeto, roomid, roomname, remaks, ref bookingidout);
        }


        public static int BookingCalenderDelete(int? userid, int? bookingid, ref int bookingidout)
        {
            return DAL_BookingCalendar.BookingCalenderDelete(userid, bookingid, ref bookingidout);
        }


        public static DataTable BookingCalenderInviteUserList(int? userid)
        {
            return DAL_BookingCalendar.BookingCalenderInviteUserList(userid);
        }


        public static int BookingCalenderAttendeesSave(int userid, int bookingid, int ismeetingtimechanged , DataTable attendeesid)
        {
            return DAL_BookingCalendar.BookingCalenderAttendeesSave(userid, bookingid, ismeetingtimechanged, attendeesid);

        }

        public static int BookingCalenderAttendeesDelete(int userid, int calenderattendesid)
        {
            return DAL_BookingCalendar.BookingCalenderAttendeesDelete(userid, calenderattendesid);
        }



        public static DataTable BooingCalenderGetAttendeesInfo(int bookingid)
        {
            return DAL_BookingCalendar.BooingCalenderGetAttendeesInfo(bookingid);
        }


        public static int BookingCalenderInviteCrewMailSave(int userid, string subject, string mailto, string cc, string body,DateTime meetingdate, ref int crewfbmidout)
        {
            return DAL_BookingCalendar.BookingCalenderInviteCrewMailSave(userid, subject, mailto, cc, body, meetingdate, ref crewfbmidout);
        }


        public static string BookingCalenderCheckValidBokingDateTime(DateTime BookingDate, string timefrom, string timeto)
        {

            return DAL_BookingCalendar.BookingCalenderCheckValidBokingDateTime(BookingDate, timefrom, timeto);
        
        }


     }
}
