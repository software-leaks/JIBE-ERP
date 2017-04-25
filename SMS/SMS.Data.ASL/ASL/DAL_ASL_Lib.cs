using System;
using System.Collections.Generic;

using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Data.ASL.ASL
{
    public class DAL_ASL_Lib
    {
        private string connection = "";

        public DAL_ASL_Lib(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_ASL_Lib()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }
        public int INS_ASL_Group_Item(DataTable dt, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@dt",dt),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return Convert.ToInt16(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ASL_Insert_Group_Fields", sqlprm));
        }

        public DataTable SupplierScope_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Supplier_Scope_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }
        public DataSet ASL_Supplier_ColumnGroup_List(int UserID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@UserID", UserID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_Group_List", obj);

            return ds;
        }
        public DataTable Get_Supplier_Group_List(int Group_ID,int UserID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Group_ID", Group_ID),
                    new System.Data.SqlClient.SqlParameter("@UserID", UserID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_Group", obj);

            return ds.Tables[0];
        }
    
        public DataTable Get_SupplierScope_List(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_Scope_List", obj);

            return ds.Tables[0];
        }

        public int InsertSupplierScope(string SupplierScope, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SCOPE_NAME",SupplierScope),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return Convert.ToInt16(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ASL_Insert_Supplier_Scope", sqlprm));
        }

        public int EditSupplierScope(int ID, string SupplierScope, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@SupplierScope",SupplierScope),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return Convert.ToInt16(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ASL_Update_Supplier_Scope", sqlprm));
        }

        public int DeleteSupplierScope(int ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Delete_Supplier_Scope", sqlprm);
        }
        public DataTable SupplierService_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),

                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Supplier_Service_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }

        public DataTable Get_SupplierService_List(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_Service_List", obj);

            return ds.Tables[0];
        }

        public int InsertSupplierService(string SupplierService,  int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SupplierService",SupplierService),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return Convert.ToInt16(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ASL_Insert_Supplier_Service", sqlprm));
        }

        public int EditSupplierService(int ID, string SupplierService, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@SupplierService",SupplierService),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return Convert.ToInt16(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ASL_Update_Supplier_Service", sqlprm));
        }

        public int DeleteSupplierService(int ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "ASL_Delete_Supplier_Service", sqlprm);
        }

        public DataTable SupplierApprover_Search(string SearchText,string Group_Name,string Approver_Type, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", SearchText),
                   new System.Data.SqlClient.SqlParameter("@Group_Name", Group_Name),
                   new System.Data.SqlClient.SqlParameter("@Approver_Type", Approver_Type),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Supplier_Approver_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];


        }

        public DataTable Get_SupplierApprover_List(int? ID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_Approver_List", obj);

            return ds.Tables[0];
        }

        public string InsertSupplierApprover(int? ApproveID, int Approver, int FinalApprover, int CreatedBy, string ApproverType, string Group_Name)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ApproveID",ApproveID),
                                            new SqlParameter("@Approver",Approver),
                                            new SqlParameter("@FinalApprover",FinalApprover),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            new SqlParameter("@ApproverType",ApproverType),
                                            new SqlParameter("@Group_Name",Group_Name),
                                            
                                         };
            return SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ASL_Insert_Supplier_Approver", sqlprm).ToString();
        }

        public string EditSupplierApprover(int? ID, int? ApproveID, int Approver, int FinalApprover, int CreatedBy, string ApproverType, string Group_Name)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@ApproveID",ApproveID),
                                            new SqlParameter("@Approver",Approver),
                                            new SqlParameter("@FinalApprover",FinalApprover),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            new SqlParameter("@ApproverType",ApproverType),
                                            new SqlParameter("@Group_Name",Group_Name),
                                            
                                         };
            return SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ASL_Update_Supplier_Approver", sqlprm).ToString();
        }

        public int DeleteSupplierApprover(int ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",ID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ASL_Delete_Supplier_Approver", sqlprm);
        }
        public DataSet Get_Supplier_ApproverUserList(int? UserID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_ApproverUser_List", obj);

            return ds;
        }
        public DataTable SearchEmailTemplate(string searchtext, 
        string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SerchText", searchtext),                             
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_ASL_EmailTemplate_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public int InsertASL_EmailTemplate_DL(string EmailStatus, string EmailBody, string EmailSubject, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {  new SqlParameter("@Email_Status",EmailStatus),
                                           new SqlParameter("@Email_Body",EmailBody),
                                           new SqlParameter("@Email_Subject",EmailSubject),
                                           new SqlParameter("@CreatedBy",CreatedBy),
                                         };
            return   (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_ASL_Insert_EmailTemplate", sqlprm);
        }
        public int EditASL_EmailTemplate_DL(int ID, string EmailStatus, string EmailBody, string EmailSubject, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@ID",ID),
                                        new SqlParameter("@Email_Status",EmailStatus),
                                        new SqlParameter("@Email_Body",EmailBody),
                                        new SqlParameter("@Email_Subject",EmailSubject),
                                        new SqlParameter("@CreatedBy",CreatedBy),

                                         };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_ASL_Update_EmailTemplate", sqlprm);
        }
        public DataTable Get_ASL_EmailTemplateList_DL(int? EmailTemplateId)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@EmailTemplateId", EmailTemplateId) };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_ASL_Get_EmailTemplateList",sqlprm).Tables[0];
        }
        public int DeleteVesselType_DL(int EmailTemplateID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@ID",EmailTemplateID),
                                          new SqlParameter("@CreatedBy",CreatedBy),

                                         };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_ASL_Del_EmailTemplate", sqlprm);
        }
        public DataSet Get_SupplierColumnGroupDetails(int UserID)
        {
            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                    new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                
            };

            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "ASL_Get_Supplier_ColumnGroup_List", obj);

            return ds;
        }

        public int Supplier_Group_Insert(int? GroupID, string Group_Name, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@GroupID",GroupID),
                                            new SqlParameter("@Group_Name",Group_Name),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return Convert.ToInt16(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ASL_Insert_Supplier_Group", sqlprm));
        }
        public int Supplier_Group_Delete(int? GroupID, int CreatedBy)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@GroupID",GroupID),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            
                                         };
            return Convert.ToInt16(SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "ASL_Delete_Supplier_Group", sqlprm));
        }
    }
}
