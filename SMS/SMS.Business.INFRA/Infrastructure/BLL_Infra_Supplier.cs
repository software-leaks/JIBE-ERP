using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using SMS.Data.Infrastructure;
using SMS.Properties;


namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_Supplier
    {

        DAL_Infra_Supplier objDAL = new DAL_Infra_Supplier();

        public BLL_Infra_Supplier()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable Get_SupplierList()
        {
            try
            {
                return objDAL.Get_SupplierList_DL();
            }
            catch
            {
                throw;
            }
        }

        public int UPDATE_Supplier(Supplier obj)
        {
            try
            {
                return objDAL.UPDATE_Supplier_DL(obj);
            }
            catch
            {
                throw;
            }
        }

        public int INS_Supplier(Supplier obj)
        {
            try
            {
                return objDAL.INS_Supplier_DL(obj);
            }
            catch
            {
                throw;
            }
        }

        public int DELETE_Supplier(int ID)
        {
            try
            {
                return objDAL.DEL_Supplier_DL(ID);
            }
            catch
            {
                throw;
            }
        }

        // new methods for maker

        public DataSet Get_Suppliers_List(string MakerName, string Address, string MakerCode, string Country)
        {
            return objDAL.Get_Suppliers_List_DL(MakerName, Address, MakerCode, Country);
        }


        public DataTable Get_Suppliers_List_Search(string searchText, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.Get_Suppliers_List_Search(searchText, sortby, sortdirection,  pagenumber,  pagesize, ref isfetchcount);
        }


        public DataTable Get_Suppliers_Details(int Supplier_ID)
        {
            return objDAL.Get_Suppliers_Details_DL(Supplier_ID);
        }

        public int Ins_Supplier_Details(string Updated_By, string Supplier_Name, string Address, string Country, string Email, string Phone, string Fax, string City, string Supplier_Code)
        {
            return objDAL.Ins_Supplier_Details_DL(Updated_By, Supplier_Name, Address, Country, Email, Phone, Fax, City, Supplier_Code);
        }

        public int Upd_Suppliers_Details(int SUPPLIER_ID, string Updated_By, string Supplier_Name, string Address, string Country, string Email, string Phone, string Fax, string City)
        {
            return objDAL.Upd_Suppliers_Details_DL(SUPPLIER_ID, Updated_By, Supplier_Name, Address, Country, Email, Phone, Fax, City);
        }


        public int Del_Supplier_Details(int SUPPLIER_ID, string Updated_By)
        {

            return objDAL.Del_Supplier_Details_DL(SUPPLIER_ID, Updated_By);

        }

        public string Get_Next_MakerCode()
        {
            return objDAL.Get_Next_MakerCode_DL();
        }

    }
}

