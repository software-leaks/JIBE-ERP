using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.Infrastructure;
using System.Data;

namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_Approval_Type
    {
        DAL_Infra_Approval_Type objApprType = new DAL_Infra_Approval_Type();
        public int INS_Approval_Type(string Type_Key,string Type_Value,int Amount_Applicable, int Created_By)
        {
            return objApprType.INS_Approval_Limit_DL(Type_Key, Type_Value,Amount_Applicable, Created_By);
        }
        public int UPD_Approval_Type(int ID, string TYPE_VALUE, int AMOUNT_APPLICABLE,int Modified_By)
        {
            return objApprType.UPD_Approval_Type_DL(ID, TYPE_VALUE,AMOUNT_APPLICABLE, Modified_By);
        }
        public DataTable Get_Approval_Type(string searchText)
        {
            return objApprType.Get_Approval_Type_DL(searchText);
        }
        public int DEL_Approval_Type(int ID, int Deleted_By)
        {
            return objApprType.DEL_Approval_Type_DL(ID, Deleted_By);
        }
    }
}
