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
    public class DAL_Infra_SupplierList
    {
        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");

        private string connection = "";

        public DAL_Infra_SupplierList(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_Infra_SupplierList()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
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
            System.Data.DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_Get_Supplier_List_Search", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];

        }
        
        public DataTable Get_Suppliers_Details_DL(int Supplier_ID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            
                                            new SqlParameter("@Supplier_ID",Supplier_ID)
                                           
                                          
                                        };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INF_GET_SUPPLIERS_DETAILS", sqlprm).Tables[0];

        }

        public int Ins_Supplier_Details_DL(string Updated_By, string Supplier_Name, string Address, string Address2, string Country
            , string Email, string Email2, string Phone, string Phone2, string Fax, string Fax2, string TELEX1, string TELEX2, string City, string Supplier_Code, string SupplierType, string SupplierScope, string Supplier_Currency, string Supp_Short_Name)
        {


            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Updated_By",Updated_By),
                                            new SqlParameter("@Supplier_Name",Supplier_Name),

                                            new SqlParameter("@Address",Address),
                                            new SqlParameter("@Address2",Address2),

                                            new SqlParameter("@Country",Country),

                                            new SqlParameter("@Email",Email),
                                            new SqlParameter("@Email2",Email2),

                                            new SqlParameter("@Phone",Phone),
                                            new SqlParameter("@Phone2",Phone2),

                                            new SqlParameter("@Fax",Fax),
                                            new SqlParameter("@Fax2",Fax2),

                                            new SqlParameter("@TELEX1",TELEX1),
                                            new SqlParameter("@TELEX2",TELEX2),



                                            new SqlParameter("@City",City),
                                            new SqlParameter("@Supplier_Code",Supplier_Code),

                                            new SqlParameter("@supplierType",SupplierType),
                                            new SqlParameter("@SupplierScope",SupplierScope),
                                            new SqlParameter("@SupplierCurrency",Supplier_Currency),
                                            new SqlParameter("@Supp_short_Name",Supp_Short_Name)
                                          
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_INS_SUPPLIERS_DETAILS", sqlprm);
        }

        public int Upd_Suppliers_Details_DL(int SUPPLIER_ID, string Updated_By, string Supplier_Name, string Address, string Address2, string Country
            , string Email, string Email2, string Phone, string Phone2, string Fax, string Fax2, string TELEX1, string TELEX2, string City, string Supplier_Code, string SupplierType, string SupplierScope, string Supplier_Currency, string Supp_Short_Name)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SUPPLIER_ID",SUPPLIER_ID),
                                            new SqlParameter("@Updated_By",Updated_By),
                                            new SqlParameter("@Supplier_Name",Supplier_Name),
                                            new SqlParameter("@Address",Address),
                                            new SqlParameter("@Address2",Address2),
                                            new SqlParameter("@Country",Country),
                                            new SqlParameter("@Email",Email),
                                            new SqlParameter("@Email2",Email2),

                                            new SqlParameter("@Phone",Phone),
                                            new SqlParameter("@Phone2",Phone2),

                                            new SqlParameter("@Fax",Fax),
                                            new SqlParameter("@Fax2",Fax2),

                                            new SqlParameter("@TELEX1",TELEX1),
                                            new SqlParameter("@TELEX2",TELEX2),

                                            new SqlParameter("@City",City),
                                            new SqlParameter("@Supplier_Code",Supplier_Code),

                                            new SqlParameter("@supplierType",SupplierType),
                                            new SqlParameter("@SupplierScope",SupplierScope),
                                            new SqlParameter("@SupplierCurrency",Supplier_Currency),
                                            new SqlParameter("@Supp_short_Name",Supp_Short_Name)
                                          
                                        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INF_UPD_SUPPLIERS_DETAILS", sqlprm);
        }


        public int Del_Supplier_Details_DL(int SUPPLIER_ID, string Updated_By)
        {

            SqlParameter[] sqlprm = new SqlParameter[]
                                    { 
                                        new SqlParameter("@SUPPLIER_ID", SUPPLIER_ID),
                                        new SqlParameter("@Updated_By", Updated_By),
                                    };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "inf_DEL_SUPPLIERS_DETAILS", sqlprm);

        }




    }
}
