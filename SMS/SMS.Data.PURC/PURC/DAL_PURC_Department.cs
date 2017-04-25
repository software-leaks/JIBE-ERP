using System;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using SMS.Data;
using SMS.Properties;

/// <summary>
/// Summary description for DALDepartment
/// </summary>
namespace SMS.Data.PURC
{
    public class DAL_PURC_Department
    {

        private string _internalConnection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;


        public DAL_PURC_Department()
        {
        }


        public DataTable Department_Search(string searchtext, string Form_Type, string Ac_Classi_Code
         , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   
                   new System.Data.SqlClient.SqlParameter("@SearchText", searchtext),
                   new System.Data.SqlClient.SqlParameter("@Form_Type", Form_Type),
                   new System.Data.SqlClient.SqlParameter("@Ac_Classi_Code", Ac_Classi_Code),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Department_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }


        public int SaveDepartment(DepartmentData objDODepartment)
        {

            try
            {

                System.Data.SqlClient.SqlParameter[] sqlprm = new System.Data.SqlClient.SqlParameter[] 
                { 
                   new System.Data.SqlClient.SqlParameter("@NAME_DEPT",objDODepartment.Dept_name),
                   new System.Data.SqlClient.SqlParameter("@CODE", objDODepartment.Dept_code),
                   new System.Data.SqlClient.SqlParameter("@FORM_TYPE", objDODepartment.FormType),
                   new System.Data.SqlClient.SqlParameter("@Link", objDODepartment.Link),
                   new System.Data.SqlClient.SqlParameter("@Vessel_Code", objDODepartment.Vessel_Code),
                   new System.Data.SqlClient.SqlParameter("@Created_By", objDODepartment.CurrentUser),
                   new System.Data.SqlClient.SqlParameter("@AcClassiCode", objDODepartment.Ac_Clssification_Code),
                   new System.Data.SqlClient.SqlParameter("@Approval_Group_Code ", objDODepartment.Approval_Group_Code)
                };

                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_Ins_Lib_DEPARTMENTS]", sqlprm);

                //=====================Return Code=======================
                //sqlprm[6].Direction = ParameterDirection.ReturnValue; 
                //  SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Ins_Lib_DEPARTMENTS", sqlprm);
                //  return Convert.ToInt32(sqlprm[6].Value); 
                //=======================================================

                return 0;

            }
            catch (Exception ex)
            {

                throw ex;


            }

        }

        public int DeleteDepartment(DepartmentData objDODepartment)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] sqlpram = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", objDODepartment.DeptID),
                   new System.Data.SqlClient.SqlParameter("@Deleted_By", objDODepartment.CurrentUser)
            };

                //obj[0].Value = DeptID;
                //obj[1].Value = UserId;

                // System.Data.SqlClient.SqlParameter obj = new System.Data.SqlClient.SqlParameter("@StringQuery", SqlDbType.VarChar);
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_del_Lib_Departments]", sqlpram);
                return 0;
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        public int EditDepartment(DepartmentData objDoDepartment)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                   new System.Data.SqlClient.SqlParameter("@ID", objDoDepartment.DeptID),
                   new System.Data.SqlClient.SqlParameter("@NAME_DEPT", objDoDepartment.Dept_name),
                   new System.Data.SqlClient.SqlParameter("@CODE", objDoDepartment.Dept_code),
                   new System.Data.SqlClient.SqlParameter("@FORM_TYPE", objDoDepartment.FormType),
                   new System.Data.SqlClient.SqlParameter("@Link", objDoDepartment.Link),
                   new System.Data.SqlClient.SqlParameter("@Vessel_Code", objDoDepartment.Vessel_Code),
                   new System.Data.SqlClient.SqlParameter("@Modified_By", objDoDepartment.CurrentUser),
                   new System.Data.SqlClient.SqlParameter("@AcClassiCode", objDoDepartment.Ac_Clssification_Code),
                   new System.Data.SqlClient.SqlParameter("@Approval_Group_Code ", objDoDepartment.Approval_Group_Code)
            };
                //obj[0].Value = DeptID;
                //obj[1].Value = Dept_name;
                //obj[2].Value = FormType;
                //obj[3].Value = Link;
                //obj[4].Value = Vessel_Code;
                //obj[5].Value = UserId;

                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_upd_Lib_DEPARTMENTS]", obj);
                return 0;
            }
            catch (Exception ex)
            {

                throw ex;

            }


        }

        public DataTable SelectDepartment()
        {
            try
            {
                DataTable dtDept = new System.Data.DataTable();
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Lib_DEPARTMENTS");
                dtDept = ds.Tables[1];
                return dtDept;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable GetDepartmentMaster()
        {
            try
            {
                DataTable dtDept = new System.Data.DataTable();
                //System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "select dept.[ID],dept.[Name_Dept],dept.[Code] ,case when dept.[Form_Type]='ST' then 'Stores' else 'Spares' end Form_Type ,dept.[Link],dept.[Vessel_Code] FROM PURC_LIB_DEPARTMENTS dept where dept.[Active_Status] =1 order by dept.[Name_Dept]");
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "select dept.[ID],dept.Dept_Name as name_dept,dept.dept_short_code as code ,case dept.[Form_Type] when 'SP' then 'SPARES' when 'ST' then 'STORES' when 'RP' then 'REPAIRS'end [Form_Type],Account_Classification as Ac_Classi_Code,Approval_Group_Code fROM PURC_LIB_DEPARTMENTS dept where dept.[Active_Status] =1 order by dept.Dept_Name");
                dtDept = ds.Tables[0];
                return dtDept;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable SelectDepartmentType()
        {
            try
            {
                DataTable dtDept = new System.Data.DataTable();
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "select  distinct Form_Type from dbo.PURC_LIB_DEPARTMENTS  where Form_Type is not null and Active_Status ='1' and Form_Type in ('ST','SP')");
                dtDept = ds.Tables[0];
                return dtDept;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable GetAccount()
        {
            try
            {
                DataTable dtAcc = new System.Data.DataTable();
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "select Acc_Code,Acc_Name from dbo.Acc_Lib_COA");
                dtAcc = ds.Tables[0];
                return dtAcc;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable GetAccountDetails()
        {
            try
            {
                DataTable dtAcc = new System.Data.DataTable();
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_GETACCOUNTINFO");
                dtAcc = ds.Tables[0];
                return dtAcc;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public DataTable GetAccountClassification()
        {
            try
            {
                DataTable dtAcc = new System.Data.DataTable();
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_Account_Classification");
                dtAcc = ds.Tables[0];
                return dtAcc;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public int AssingAccountDept(string DeptCode, string CatlgCode, string SubCatlgCode, string AccCode)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                   new System.Data.SqlClient.SqlParameter("@DeptCode", DeptCode),
                   new System.Data.SqlClient.SqlParameter("@CatlgCode", CatlgCode),
                   new System.Data.SqlClient.SqlParameter("@SubCatlgCode", SubCatlgCode),
                   new System.Data.SqlClient.SqlParameter("@AccCode", AccCode),
                  
            };
                int res = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_AssingAccountDept]", obj);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;

            }


        }

        public int UpdateAccount(string ID, string AccCode)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
                   new System.Data.SqlClient.SqlParameter("@AccCode", AccCode)
                 
                  
            };
                int res = SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_UpdateAccount]", obj);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;

            }


        }

        public DataTable CheckExist(string DeptCode, string CatlgCode, string SubCatlgCode)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] { 
                   new System.Data.SqlClient.SqlParameter("@DeptCode", DeptCode),
                   new System.Data.SqlClient.SqlParameter("@CatlgCode", CatlgCode),
                   new System.Data.SqlClient.SqlParameter("@SubCatlgCode", SubCatlgCode)
                  
                  
            };
                DataSet res = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "[PURC_SP_CheckExist]", obj);
                return res.Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;

            }
        }

        public DataTable GetDeptType()
        {
            try
            {
                DataSet res = SqlHelper.ExecuteDataset(_internalConnection, CommandType.Text, "select Short_Code,Description from dbo.PURC_LIB_SYSTEM_PARAMETERS where Parent_Type='157'");
                return res.Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;

            }
        }

        public DataTable GetApprovalCode()
        {
            try
            {
                DataSet res = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_GET_Approval_Group");
                return res.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Req Type PO Type Mapping ------------------------
        public DataTable POType_Search(int? Req_Type, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Req_Type", Req_Type),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Req_POType_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public DataSet GetPOType()
        {
            try
            {
                //DataTable dtAcc = new System.Data.DataTable();
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Get_PO_Type");
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public int SavePOType(int? Req_Type, int? BudgetCode, string PO_Type, string Account_Type, int UserID)
        {

            try
            {

                System.Data.SqlClient.SqlParameter[] sqlprm = new System.Data.SqlClient.SqlParameter[] 
                { 
                   new System.Data.SqlClient.SqlParameter("@Req_Type", Req_Type),
                   new System.Data.SqlClient.SqlParameter("@BudgetCode", BudgetCode),
                   new System.Data.SqlClient.SqlParameter("@PO_Type", PO_Type),
                   new System.Data.SqlClient.SqlParameter("@Account_Type", Account_Type),
                   new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                };

                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_INS_Req_POType]", sqlprm);

                //=====================Return Code=======================
                //sqlprm[6].Direction = ParameterDirection.ReturnValue; 
                //  SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Ins_Lib_DEPARTMENTS", sqlprm);
                //  return Convert.ToInt32(sqlprm[6].Value); 
                //=======================================================

                return 0;

            }
            catch (Exception ex)
            {

                throw ex;


            }

        }
        public int UpdatePOType(int? ID, int? Req_Type, int? BudgetCode, string PO_Type, string Account_Type, int UserID)
        {

            try
            {

                System.Data.SqlClient.SqlParameter[] sqlprm = new System.Data.SqlClient.SqlParameter[] 
                { 
                   new System.Data.SqlClient.SqlParameter("@ID",ID),
                   new System.Data.SqlClient.SqlParameter("@Req_Type", Req_Type),
                   new System.Data.SqlClient.SqlParameter("@BudgetCode", BudgetCode),
                   new System.Data.SqlClient.SqlParameter("@PO_Type", PO_Type),
                   new System.Data.SqlClient.SqlParameter("@Account_Type", Account_Type),
                   new System.Data.SqlClient.SqlParameter("@UserID", UserID),
                };

                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_UPD_Req_POType]", sqlprm);

                //=====================Return Code=======================
                //sqlprm[6].Direction = ParameterDirection.ReturnValue; 
                //  SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_SP_Ins_Lib_DEPARTMENTS", sqlprm);
                //  return Convert.ToInt32(sqlprm[6].Value); 
                //=======================================================

                return 0;

            }
            catch (Exception ex)
            {

                throw ex;


            }

        }

        public int DeletePOType(int? ID,int UserID)
        {
            try
            {

                System.Data.SqlClient.SqlParameter[] sqlpram = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@ID", ID),
                   new System.Data.SqlClient.SqlParameter("@Deleted_By", UserID)
            };
                SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "[PURC_DEL_Req_POType]", sqlpram);
                return 0;
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }
        public DataTable Get_Req_POType(int? ID)
        {
            try
            {
                DataTable dtDept = new System.Data.DataTable();
                System.Data.SqlClient.SqlParameter[] sqlprm = new System.Data.SqlClient.SqlParameter[] 
                { 
                   new System.Data.SqlClient.SqlParameter("@ID",ID),
                };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Req_POType", sqlprm);
                dtDept = ds.Tables[0];
                return dtDept;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        // Email Template -------------------------
       
        public DataSet Get_Email_System_Parameter(string Parent_Short_Code, int UserID)
        {
            try
            {
                DataSet dsDept = new DataSet();
                System.Data.SqlClient.SqlParameter[] sqlprm = new System.Data.SqlClient.SqlParameter[] 
                { 
                   new System.Data.SqlClient.SqlParameter("@Parent_Short_Code",Parent_Short_Code),
                   new System.Data.SqlClient.SqlParameter("@UserID",UserID),
                };
                System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Email_Parameter", sqlprm);
                //dtDept = ds.Tables[0];
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public DataTable Search_Email_Template_List(string Email_Type, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Email_Type", Email_Type),
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Search_Email_Template", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }
        public int PURC_Ins_Email_Template(string Email_Type, string EmailBody, string EmailSubject, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        {  new SqlParameter("@Email_Type",Email_Type),
                                           new SqlParameter("@Email_Body",EmailBody),
                                           new SqlParameter("@Email_Subject",EmailSubject),
                                           new SqlParameter("@CreatedBy",CreatedBy),
                                         };
            return (int)SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PURC_Ins_Email_Template", sqlprm);
        }
        public int PURC_UPD_Email_Template(int ID, string Email_Type, string EmailBody, string EmailSubject, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@ID",ID),
                                        new SqlParameter("@Email_Type",Email_Type),
                                        new SqlParameter("@Email_Body",EmailBody),
                                        new SqlParameter("@Email_Subject",EmailSubject),
                                        new SqlParameter("@CreatedBy",CreatedBy),

                                         };
            return (int)SqlHelper.ExecuteScalar(_internalConnection, CommandType.StoredProcedure, "PURC_UPD_Email_Template", sqlprm);
        }
        public DataTable PURC_Get_Email_Template(int? ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { new SqlParameter("@ID", ID) };
            return SqlHelper.ExecuteDataset(_internalConnection, CommandType.StoredProcedure, "PURC_Get_Email_Template", sqlprm).Tables[0];
        }

        public int PURC_Del_Email_Template(int ID, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@ID",ID),
                                          new SqlParameter("@CreatedBy",CreatedBy),

                                         };

            return SqlHelper.ExecuteNonQuery(_internalConnection, CommandType.StoredProcedure, "PURC_Del_Email_Template", sqlprm);
        }
    }
}