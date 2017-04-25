using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Data.PortageBill
{
   public class DAL_PB_Admin
   {

       
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private string connection = "";

        public DAL_PB_Admin(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_PB_Admin()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }

        public DataSet SearchSalaryStructure(int? Parent_Code, string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, int? ParentCode)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Parent_Code", Parent_Code),
                   
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                      new System.Data.SqlClient.SqlParameter("@ParentCode",ParentCode),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_SalaryStructure_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds ;
        }

        public DataTable Get_SalaryStructureList_DL(int? Code)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                          new SqlParameter("@Code",Code) 
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_SalaryStructure_List" ,sqlprm).Tables[0];
        }



        public DataTable Get_SalaryStructureParentList_DL()
        {
           return SqlHelper.ExecuteDataset(connection,CommandType.StoredProcedure,"ACC_SP_Get_SalaryStructure_Parent_List").Tables[0];
        }


        public int InsertSalaryStructure_DL(int? Parent_Code, string Name, string Description, string KeyValue, int? SalaryType, int? Sort_Order, string Flage, int CreatedBy, int AutoPopulate, int VesselSpecific, int? Payableat) // ref string Error
        {
           
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Parent_Code",Parent_Code),
                                            new SqlParameter("@Name",Name),
                                            new SqlParameter("@Description",Description),
                                            new SqlParameter("@Key_Value",KeyValue),
                                            new SqlParameter("@Salary_Type",SalaryType),
                                            new SqlParameter("@Sort_Order",Sort_Order),
                                            new SqlParameter("@Flag_id",Flage),                                           
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            new SqlParameter("@AutoPopulate", AutoPopulate),
                                            new SqlParameter("@VesselSpecific",VesselSpecific),
                                             new SqlParameter("@PayableAt",Payableat)
                                         };
     
           return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_Insert_SalaryStructure", sqlprm);           
        }


        public int EditSalaryStructure_DL(int Code, int? Parent_Code, string Name, string Description, string KeyValue, int? SalaryType, int? Sort_Order, string Flage, int ModifiedBy, int AutoPopulate, int VesselSpecific, int? Payableat)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Code",Code),
                                            new SqlParameter("@Parent_Code",Parent_Code),
                                            new SqlParameter("@Name",Name),
                                            new SqlParameter("@Description",Description),
                                            new SqlParameter("@Key_Value",KeyValue),
                                            new SqlParameter("@Salary_Type",SalaryType),
                                            new SqlParameter("@Sort_Order",Sort_Order),
                                            new SqlParameter("@Flag_id",Flage),                                            
                                            new SqlParameter("@ModifiedBy",ModifiedBy),
                                            new SqlParameter("@AutoPopulate", AutoPopulate),
                                            new SqlParameter("@VesselSpecific",VesselSpecific),
                                             new SqlParameter("@PayableAt",Payableat)
                                        
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_Update_SalaryStructure", sqlprm);

        }

      

        public int DeleteSalaryStructure_DL(int Code, int DeletedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Code",Code),
                                            new SqlParameter("@DeletedBy",DeletedBy)
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_Delete_SalaryStructure", sqlprm);
        }


        public int Swap_Sort_Order_DL(int Code,int Parent_Code, int MoveUpDown, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Code",Code),
                                            new SqlParameter("@Parent_Code",Parent_Code),
                                            new SqlParameter("@MoveUpDown",MoveUpDown),
                                            new SqlParameter("@Modified_By",Modified_By)
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_SP_Swap_SalaryStructure_Sort_Order", sqlprm);
        }
        public DataTable Get_KeyValueList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_SalaryStructure_KeyValue_List").Tables[0];
        }
        public int INS_CASH_ADV_LIMIT(int? ID, int? RankCat, int? Percent, int UserId, string Mode)
        {
            SqlParameter[] sqlPrm = new SqlParameter[]
            {
                new SqlParameter("@ID",ID),
                new SqlParameter("@Rank_Category_ID",RankCat),
                new SqlParameter("@Percentage",Percent),
                new SqlParameter("@UserId",UserId),
                new SqlParameter("@Mode",Mode)
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ACC_INS_LIB_Cash_Advance_Limit", sqlPrm);
        }

        public DataTable GET_CASH_ADV_LIMIT()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_LIB_Cash_Advance_Limit").Tables[0];

        }

        public DataTable GetRankCashAdvanceLimit()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_GET_RANK_CASH_ADVANCE_LIMIT").Tables[0];
        }
        public DataTable Get_SalaryTypeList_DL(int? Code)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                          new SqlParameter("@Code",Code) 
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ACC_SP_Get_SalaryType_List", sqlprm).Tables[0];
        }

   }
}
