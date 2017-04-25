using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace SMS.Data.QMS 
{
    public class DAL_BookingCalendar
    {

        static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;



        public static DataTable BookingCalenderList(int? bookingid,int? userid)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@BOOKINGID", bookingid) 
                   ,new System.Data.SqlClient.SqlParameter("@USERID", userid) 
            };
            
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CAL_PR_GET_BOOKING_LIST", obj).Tables[0];
            
        }
        public static SqlDataReader BookingCalenderListForDate(DateTime? BookingDate, int? userid)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@BOOKING_DATE", BookingDate) 
                   ,new System.Data.SqlClient.SqlParameter("@USERID", userid) 
            };

            return SqlHelper.ExecuteReader(_internalConnection, CommandType.StoredProcedure, "CAL_PR_GET_BOOKING_LIST_FORDATE", obj);

        }

        public static int BookingCalenderSave(int? userid ,int? bookingid,DateTime? bookingdate ,string  timefrom , string timeto ,string roomid, string roomname , string remaks,ref int bookingidout)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@USERID", userid ),  
                 new System.Data.SqlClient.SqlParameter("@BOOKINGDATE", bookingdate ),
                 new System.Data.SqlClient.SqlParameter("@BOOKINGID", bookingid ),  
                 new System.Data.SqlClient.SqlParameter("@TIME_FROM", timefrom ),  
                 new System.Data.SqlClient.SqlParameter("@TIME_TO", timeto ),  
                 new System.Data.SqlClient.SqlParameter("@ROOMID", roomid ),  
                 new System.Data.SqlClient.SqlParameter("@ROOMNAME", roomname ),  
                 new System.Data.SqlClient.SqlParameter("@REMARK", remaks ), 
                 new System.Data.SqlClient.SqlParameter("@BOOKING_ID", SqlDbType.Int ), 
            };
            obj[obj.Length - 1].Direction = ParameterDirection.Output;
            int retval = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "CAL_PR_BOOKING_SAVE", obj);
            bookingidout = Convert.ToInt32(obj[obj.Length - 1].Value);
            return retval;
        }


        public static int BookingCalenderDelete(int? userid, int? bookingid, ref int bookingidout)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@USERID", userid ),  
                 new System.Data.SqlClient.SqlParameter("@BOOKINGID", bookingid ),  
                 new System.Data.SqlClient.SqlParameter("@BOOKING_ID_OUT", SqlDbType.Int ), 
                
            };

            obj[obj.Length - 1].Direction = ParameterDirection.Output;
            int retval = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "CAL_PR_BOOKING_DELETE", obj);
            bookingidout = Convert.ToInt32(obj[obj.Length - 1].Value);
            return retval;
        }



        public static DataTable BookingCalenderInviteUserList(int? userid)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@CURRENTUSER", userid) 
            };
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CAL_PR_GET_INVITE_USER_LIST", obj);
            dt = ds.Tables[0];
            return dt;
        }


        public static int BookingCalenderAttendeesSave(int userid, int bookingid, int ismeetingtimechanged, DataTable attendeesid)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@USERID", userid ),  
                 new System.Data.SqlClient.SqlParameter("@BOOKINGID", bookingid ),  
                 new System.Data.SqlClient.SqlParameter("@ISMEETINGTIMECHANGED", ismeetingtimechanged ),  
                 new System.Data.SqlClient.SqlParameter("@ATTENDEESID", attendeesid ),  
                
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "CAL_PR_BOOKING_ATTENDEES_SAVE", obj);
        
        }


        public static int BookingCalenderAttendeesDelete(int userid, int calenderattendesid)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@USERID", userid ),  
                 new System.Data.SqlClient.SqlParameter("@CALENDERATTENDESID", calenderattendesid),  
                
            };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "CAL_PR_BOOKING_ATTENDEES_DELETE", obj);

        }




        public static DataTable BooingCalenderGetAttendeesInfo(int bookingid)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 new System.Data.SqlClient.SqlParameter("@BOOKINGID", bookingid ),  
            };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "CAL_PR_GET_ATTENDEES_INFO" , obj).Tables[0];
        }


        public static int BookingCalenderInviteCrewMailSave(int userid, string subject, string mailto, string cc, string body,DateTime meetingdate, ref int crewfbmidout)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@USERID", userid),
                   new System.Data.SqlClient.SqlParameter("@SUBJECT", subject),
                   new System.Data.SqlClient.SqlParameter("@MAILTO", mailto),
                   new System.Data.SqlClient.SqlParameter("@CC", cc),
                   new System.Data.SqlClient.SqlParameter("@BODY", body),
                   new System.Data.SqlClient.SqlParameter("@MEETINGTIME", meetingdate),
                   new System.Data.SqlClient.SqlParameter("@CREW_MAX_MAILE_ID",SqlDbType.Int),
            };
            obj[obj.Length - 1].Direction = ParameterDirection.Output;
            int retval = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "CAL_PR_CREW_MAIL_SAVE", obj);
            crewfbmidout = Convert.ToInt32(obj[obj.Length - 1].Value);
            return retval;
        }


        public static string BookingCalenderCheckValidBokingDateTime(DateTime BookingDate, string timefrom, string timeto)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@BOOKINGDATE", BookingDate),
                   new System.Data.SqlClient.SqlParameter("@TIME_FROM", timefrom),
                   new System.Data.SqlClient.SqlParameter("@TIME_TO", timeto),
                   new System.Data.SqlClient.SqlParameter("@Return", SqlDbType.Int),
            };

            obj[obj.Length - 1].Direction = ParameterDirection.ReturnValue;
             SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "CAL_PR_CHECK_VALID_BOOKING_DATE_TIME", obj);
             return obj[obj.Length - 1].Value.ToString();

        }

    }
}
