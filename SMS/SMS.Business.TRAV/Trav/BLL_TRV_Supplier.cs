using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using SMS.Properties;
using SMS.Data.TRAV;

namespace SMS.Business.TRAV
{
    public class BLL_TRV_Supplier
    {
        DAL_TRV_Supplier supplier = new DAL_TRV_Supplier();
        
        public DataTable GetSupplier(string Supplier_Category, string Supplier_Search)
        {            
            try
            {
                return supplier.GetSuppliers(Supplier_Category, Supplier_Search);
            }
            catch { throw; }
        }

        public static DataTable Get_SupplierList(string Supplier_Search)
        {
            return DAL_TRV_Supplier.Get_SupplierList_DL(Supplier_Search);
        }

        public static DataTable Get_SupplierList(string Supplier_Search, int RFQSent_REQ_ID)
        {
            return DAL_TRV_Supplier.Get_SupplierList_DL(Supplier_Search, RFQSent_REQ_ID);
        }
                
        public string GetSupplierEmail(int id)
        {
            DAL_TRV_Supplier supplier = new DAL_TRV_Supplier();
            try
            {
                return supplier.GetSupplier_Email(id);
            }
            catch { throw; }
        }
    }
}
