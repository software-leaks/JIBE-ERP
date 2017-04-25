using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

using SMS.Properties;

namespace SMS.Data.Infrastructure
{
    public class DAL_Infra_Supplier
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";

        public DAL_Infra_Supplier(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_Infra_Supplier()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        public DataTable Get_SupplierList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_SupplierList").Tables[0];
        }

        public DataTable Get_SupplierList_Mini_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INF_Get_SupplierList_Mini").Tables[0];
        }

        public int UPDATE_Supplier_DL(Supplier objSup)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { new SqlParameter("@ID",objSup.ID),
                                          new SqlParameter("@ORG_NAME",objSup.ORG_NAME),
                                          new SqlParameter("@ORG_SHORT_NAME",objSup.ORG_SHORT_NAME),
                                          new SqlParameter("@ADDRESS_1",objSup.ADDRESS_1),
                                          new SqlParameter("@ADDRESS_2",objSup.ADDRESS_2),
                                          new SqlParameter("@EMAIL1",objSup.EMAIL1),
                                          new SqlParameter("@EMAIL2",objSup.EMAIL2),
                                          new SqlParameter("@PIC",objSup.PIC),
                                          new SqlParameter("@PIC_POSITION",objSup.PIC_POSITION),
                                          new SqlParameter("@PIC_SALUTATION",objSup.PIC_SALUTATION),
                                          new SqlParameter("@PHONE1",objSup.PHONE1),
                                          new SqlParameter("@PHONE2",objSup.PHONE2),
                                          new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Update_Supplier", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int INS_Supplier_DL(Supplier objSup)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                          new SqlParameter("@ORG_NAME",objSup.ORG_NAME),
                                          new SqlParameter("@ORG_SHORT_NAME",objSup.ORG_SHORT_NAME),
                                          new SqlParameter("@ADDRESS_1",objSup.ADDRESS_1),
                                          new SqlParameter("@ADDRESS_2",objSup.ADDRESS_2),
                                          new SqlParameter("@EMAIL1",objSup.EMAIL1),
                                          new SqlParameter("@EMAIL2",objSup.EMAIL2),
                                          new SqlParameter("@PIC",objSup.PIC),
                                          new SqlParameter("@PIC_POSITION",objSup.PIC_POSITION),
                                          new SqlParameter("@PIC_SALUTATION",objSup.PIC_SALUTATION),
                                          new SqlParameter("@PHONE1",objSup.PHONE1),
                                          new SqlParameter("@PHONE2",objSup.PHONE2),
                                          new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Insert_Supplier", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int DEL_Supplier_DL(int ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("ID", ID),
                                        new SqlParameter("return",SqlDbType.Int)
                                    };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INF_Del_Supplier", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        // new methods for maker
        public DataSet Get_Suppliers_List_DL(string MakerName, string Address, string MakerCode, string Country)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@Supplier_Name",MakerName),
                                            new SqlParameter("@Address",Address),
                                            new SqlParameter("@Country",Country),
                                            new SqlParameter("@Supplier_Code",MakerCode)
                                          
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "PURC_Get_Suppliers_List", sqlprm);
        }



        public DataTable Get_Suppliers_List_Search(string searchText, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                 
                new System.Data.SqlClient.SqlParameter("@SearchText", searchText),
                  
                new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount),
                    
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "PURC_Get_Suppliers_List_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        
        }





        public DataTable Get_Suppliers_Details_DL(int Supplier_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@Supplier_ID",Supplier_ID)
                                           
                                          
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "PURC_GET_SUPPLIERS_DETAILS", sqlprm).Tables[0];

        }

        public int Ins_Supplier_Details_DL(string Updated_By, string Supplier_Name, string Address, string Country, string Email, string Phone, string Fax, string City, string Supplier_Code)
        {


            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Updated_By",Updated_By),
                                            new SqlParameter("@Supplier_Name",Supplier_Name),
                                            new SqlParameter("@Address",Address),
                                            new SqlParameter("@Country",Country),
                                            new SqlParameter("@Email",Email),
                                            new SqlParameter("@Phone",Phone),
                                            new SqlParameter("@Fax",Fax),
                                            new SqlParameter("@City",City),
                                            new SqlParameter("@Supplier_Code",Supplier_Code)
                                          
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "PURC_INS_SUPPLIERS_DETAILS", sqlprm);
        }

        public int Upd_Suppliers_Details_DL(int SUPPLIER_ID, string Updated_By, string Supplier_Name, string Address, string Country, string Email, string Phone, string Fax, string City)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SUPPLIER_ID",SUPPLIER_ID),
                                            new SqlParameter("@Updated_By",Updated_By),
                                            new SqlParameter("@Supplier_Name",Supplier_Name),
                                            new SqlParameter("@Address",Address),
                                            new SqlParameter("@Country",Country),
                                            new SqlParameter("@Email",Email),
                                            new SqlParameter("@Phone",Phone),
                                            new SqlParameter("@Fax",Fax),
                                            new SqlParameter("@City",City),
                                          
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "PURC_UPD_SUPPLIERS_DETAILS", sqlprm);

        }


        public int Del_Supplier_Details_DL(int SUPPLIER_ID, string Updated_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@SUPPLIER_ID", SUPPLIER_ID),
                                        new SqlParameter("@Updated_By", Updated_By),
                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "PURC_DEL_SUPPLIERS_DETAILS", sqlprm);

        }

        public string Get_Next_MakerCode_DL()
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@MAKER_CODE",SqlDbType.VarChar,50)
                                                                                      
                                        };
            sqlprm[0].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_GET_NEXT_MAKERCODE", sqlprm);
            return sqlprm[0].Value.ToString();

        }


    }
}
