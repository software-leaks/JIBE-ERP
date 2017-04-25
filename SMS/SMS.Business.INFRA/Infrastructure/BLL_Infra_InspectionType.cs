using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.Infrastructure;
using System.Data;
 
namespace SMS.Business.Infrastructure
{
   public class BLL_Infra_InspectionType
    {

        DAL_Infra_InspectionType objDAL = new DAL_Infra_InspectionType();
        public BLL_Infra_InspectionType()
        {

        }  
        public DataTable SearchInspectionType(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objDAL.SearchInspectionType(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);

        }
        public int InsertInspectionType(string InpsectionTypeName, int CreatedBy,string color)
        {
            return objDAL.InsertInspectionType_DL(InpsectionTypeName, CreatedBy, color);
        }
        public int EditInspectionType(int InspectionTypeId, string InpsectionTypeName, int CreatedBy,string color)
        {
            return objDAL.EditInspectionType_DL(InspectionTypeId, InpsectionTypeName, CreatedBy, color);

        }
        public DataTable Get_InspectionTypeList()
        {
            return objDAL.Get_InspectionTypeList_DL();
        }
        public int DeleteInspectionType(int InspectionTypeId, int CreatedBy)
        {
            return objDAL.DeleteInspectionType_DL(InspectionTypeId, CreatedBy);

        }
         
        public DataTable Check_InspectionType(string InpsectionTypeName)
        {
            return objDAL.Check_InspectionType(InpsectionTypeName);
        }
    }
}
