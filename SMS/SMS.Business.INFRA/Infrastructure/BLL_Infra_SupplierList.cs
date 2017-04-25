using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Infrastructure;
using SMS.Properties;


namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_SupplierList
    {

        DAL_Infra_SupplierList objDAL = new DAL_Infra_SupplierList();

        public BLL_Infra_SupplierList()
        {
            //
            // TODO: Add constructor logic here
            //
        }

       

        public DataTable Get_Suppliers_List_Search(string searchText, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.Get_Suppliers_List_Search(searchText, sortby, sortdirection,  pagenumber,  pagesize, ref isfetchcount);
        }


        public DataTable Get_Suppliers_Details(int Supplier_ID)
        {
            return objDAL.Get_Suppliers_Details_DL(Supplier_ID);
        }

        public int Ins_Supplier_Details(string Updated_By, string Supplier_Name, string Address, string Address2, string Country
            , string Email, string Email2, string Phone, string Phone2, string Fax, string Fax2, string TELEX1, string TELEX2, string City, string Supplier_Code, string SupplierType, string SupplierScope, string Supplier_Currency,string Supp_Short_Name)
        {
            return objDAL.Ins_Supplier_Details_DL(Updated_By, Supplier_Name, Address, Address2, Country, Email, Email2, Phone, Phone2, Fax, Fax2, TELEX1, TELEX2, City, Supplier_Code, SupplierType, SupplierScope, Supplier_Currency,Supp_Short_Name);
        }

        public int Upd_Suppliers_Details(int SUPPLIER_ID, string Updated_By, string Supplier_Name, string Address, string Address2, string Country
            , string Email, string Email2, string Phone, string Phone2, string Fax, string Fax2, string TELEX1, string TELEX2, string City, string Supplier_Code, string SupplierType, string SupplierScope, string Supplier_Currency, string Supp_Short_Name)
        {
            return objDAL.Upd_Suppliers_Details_DL(SUPPLIER_ID, Updated_By, Supplier_Name, Address, Address2, Country, Email, Email2, Phone, Phone2, Fax, Fax2, TELEX1, TELEX2, City, Supplier_Code, SupplierType, SupplierScope, Supplier_Currency,Supp_Short_Name);
        }

        public int Del_Supplier_Details(int SUPPLIER_ID, string Updated_By)
        {

            return objDAL.Del_Supplier_Details_DL(SUPPLIER_ID, Updated_By);
        
        }



    }
}

