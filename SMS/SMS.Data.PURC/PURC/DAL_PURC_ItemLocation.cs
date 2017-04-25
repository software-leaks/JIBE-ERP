using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;

/// <summary>
/// Summary description for DALLocation
/// </summary>
namespace SMS.Data.PURC
{
    public class DAL_PURC_ItemLocation
    {

        private static string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        public static DataTable GetLocation_Search(int? parent_id, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@PARENT_ID", parent_id),

                   new System.Data.SqlClient.SqlParameter("@SEARCHTEXT", searchtext),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_ITEM_LOCATIONLIST", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }


        public static int InsertLocation(int ParentId, string Location_Name, int Userid)
        {

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                   new System.Data.SqlClient.SqlParameter("@Parent_ID", ParentId),
                   new System.Data.SqlClient.SqlParameter("@Location_Name", Location_Name),
                   new System.Data.SqlClient.SqlParameter("@USERID", Userid)
                  
              };
                
                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_INS_ITEM_LOCATION]", obj);
          

        }


        public static int DeleteLocation(int LocationID,  int Userid)
        {
          
                System.Data.SqlClient.SqlParameter[] sqlpram = new System.Data.SqlClient.SqlParameter[] 
                { 
                   new System.Data.SqlClient.SqlParameter("@Location_ID", LocationID),                  
                   new System.Data.SqlClient.SqlParameter("@USERID", Userid)
                };             
            return   SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_DELETE_ITEM_LOCATION", sqlpram);

        }


        public static int UpdateLocation(int LocationID, int ParentId, string Location_Name, int Userid)
        {

                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
                { 
                   new System.Data.SqlClient.SqlParameter("@Location_ID", LocationID),
                   new System.Data.SqlClient.SqlParameter("@Parent_ID", ParentId),
                   new System.Data.SqlClient.SqlParameter("@Location_Name", Location_Name),
                   new System.Data.SqlClient.SqlParameter("@USERID", Userid),
              };

                return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_UPDATE_ITEM_LOCATION", obj);
           

        }
        public static DataTable  GetParentLocation()
        {
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_ITEM_LOCATIONPARENLIST").Tables[0];         

        }
        public static DataTable EditLocation(int LocationID)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[]
                { 
                   new System.Data.SqlClient.SqlParameter("@Location_ID", LocationID),
                  
              };

            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_ITEM_LOCATIONEDIT", obj).Tables[0]; 


        }

      


    }
}