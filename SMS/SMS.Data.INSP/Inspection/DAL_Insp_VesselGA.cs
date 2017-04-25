using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace SMS.Data.Inspection
{
    public class DAL_Insp_VesselGA
    {
          IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private string connection = "";
        public DAL_Insp_VesselGA(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Insp_VesselGA()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }



        public DataTable Get_VesselGASearch_DL(string searchtext, int? Path_ID, int? Object_ID, int? Fleet_ID, 
                 string sortby, int? sortdirection, int? pagenumber, int? pagesize,string CompanyID, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Path_ID", Path_ID),
                   new System.Data.SqlClient.SqlParameter("@Object_ID", Object_ID), 
                   new System.Data.SqlClient.SqlParameter("@Fleet_ID", Fleet_ID), 
                  new System.Data.SqlClient.SqlParameter("@CompanyID", CompanyID), 

                    
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INSP_Get_VesselGA_Search", obj);           
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }

       
        public DataSet Get_ParentID_DL(int Parent_Company_ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VesselID", Parent_Company_ID),
                  
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INSP_Get_ParentID", obj);
        }

        public DataSet Get_ParentID_VT_DL(int VesselType)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@VesselType_ID", VesselType),
                  
            };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INSP_Get_ParentID_By_Vessel_Type", obj);
        }

        public int INS_VesselGA_DL(string Path_ID, int Object_ID, string Image_Path, string SVG_Path, int Is_GA, string Parent_Path_ID, int? Vessel_TypeID, string Path_Name)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Path_ID",Path_ID),
                                            new SqlParameter("@Object_ID",Object_ID),
                                            new SqlParameter("@Image_Path",Image_Path),
                                            new SqlParameter("@SVG_Path",SVG_Path),
                                            new SqlParameter("@Is_GA",Is_GA),
                                            new SqlParameter("@Parent_Path_ID",Parent_Path_ID),
                                            new SqlParameter("@Path_Name",Path_Name),     
                                             new SqlParameter("@Vessel_TypeID",Vessel_TypeID),     

                                            new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INSP_INS_VesselGA", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int INS_IMPORT_VesselGA_DL(DataTable tableGA)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@GAtable",tableGA),
                                            
                                            //new SqlParameter("return",SqlDbType.Int)
            };
            //sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            sqlprm[sqlprm.Length - 1].SqlDbType = SqlDbType.Structured;
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INSP_INS_Import_VesselGA", sqlprm);
            
            //return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        public DataTable Get_VesselGA_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INSP_Get_VesselGA").Tables[0];

        }


        public int EditVesselGA_DL(string Path_ID, int Object_ID, string Image_Path, string SVG_Path, int? Is_GA, string Parent_Path_ID, int? Vessel_TypeID, string Path_Name)
        {
           
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Path_ID",Path_ID),
                                            new SqlParameter("@Object_ID",Object_ID),
                                            new SqlParameter("@Image_Path",Image_Path),
                                            new SqlParameter("@SVG_Path",SVG_Path),
                                            new SqlParameter("@Is_GA",Is_GA),
                                            new SqlParameter("@Parent_Path_ID",Parent_Path_ID),
                                            new SqlParameter("@Vessel_TypeID",Vessel_TypeID),
                                            new SqlParameter("@Path_Name",Path_Name),
                                            //new SqlParameter("@Base_Curr",Base_Curr),
                                            //new SqlParameter("@Address",Address),
                                            //new SqlParameter("@Country",Country),
                                            //new SqlParameter("@Email1",Email1),
                                            //new SqlParameter("@Phone1",Phone1),
                                            //new SqlParameter("@Short_Name","")

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INSP_Update_VesselGA", sqlprm);

        }

        public int DeleteVesselGA_DL(string imgORsvg, string Path_ID)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                             new SqlParameter("@imgORsvg",imgORsvg),
                                            new SqlParameter("@Path_ID",Path_ID)
                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INSP_Delete_VesselGA", sqlprm);

        }

        //public int DeleteVesselGA_DL(string imgORsvg, string Path_ID)
        //{

        //    SqlParameter[] sqlprm = new SqlParameter[]
        //                                { 
        //                                     new SqlParameter("@imgORsvg",imgORsvg),
        //                                    new SqlParameter("@Path_ID",Path_ID)
        //                                 };

        //    return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INSP_DeleteVesselGA", sqlprm);

        //}
  
    }
}
