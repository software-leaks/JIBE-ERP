using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Operations;

namespace SMS.Business.Operations
{
   public class BLL_OPS_Admin
    {
       
       DAL_OPS_Admin objDAL = new DAL_OPS_Admin();

       public DataTable LubeOilCategory_Search(string searchtext, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
       {
           return objDAL.LubeOilCategory_Search(searchtext, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
       }

       public DataTable Get_LubeOilCategory_List(int? ID)
       {
           return objDAL.Get_LubeOilCategory_List_DL(ID);
       }

       public int InsertLubeOilCategory(string Category, int CreatedBy)
       {
           return objDAL.InsertLubeOilCategory_DL(Category, CreatedBy);
       }

       public int EditLubeOilCategory(int ID, string Category, int CreatedBy)
       {
            return objDAL.EditLubeOilCategory_DL(ID, Category, CreatedBy);
       }

       public int DeleteLubeOilCategory(int ID, int CreatedBy)
       {
           return objDAL.DeleteLubeOilCategory_DL(ID, CreatedBy);
       }





       public DataTable Hold_Tank_Search(string searchtext, int? Structure_Type, int? Vessel_ID, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
       {
           return objDAL.Hold_Tank_Search(searchtext,Structure_Type,Vessel_ID,sortby,sortdirection,pagenumber,pagesize,ref isfetchcount);
       }

       public DataTable Get_Hold_Tank_List(int? ID, int Vessel_ID)
       {
           return objDAL.Get_Hold_Tank_List_DL(ID, Vessel_ID);
       }

       public int Insert_Hold_Tank(string Hold_Tank_Name, int? Structure_Type, int? Vessel_ID, int CreatedBy)
       {
           return objDAL.Insert_Hold_Tank_DL(Hold_Tank_Name, Structure_Type, Vessel_ID, CreatedBy);
       }

       public int Edit_Hold_Tank(int ID, string Hold_Tank_Name, int? Structure_Type, int? Vessel_ID, int CreatedBy)
       {
           return objDAL.Edit_Hold_Tank_DL(ID,Hold_Tank_Name, Structure_Type, Vessel_ID, CreatedBy);
       }

       public int Delete_Hold_Tank(int ID, int Vessel_ID, int CreatedBy)
       {
           return objDAL.Delete_Hold_Tank_DL(ID, Vessel_ID, CreatedBy);
       }


       public  DataTable Get_CategoryList(string SearchText)
       {
           try
           {
               return objDAL.Get_CategoryList_DL(SearchText);
           }
           catch
           {
               throw;
           }
       }
      // public int INSERT_Category(string Category_Name, int Category_Order_By, int Created_By)
       public int INSERT_Category(int? Category_ID, string Category_Name, int Category_Order_By, int? GradeType_Id, int Created_By)
       {
           try
           {
              // return objDAL.INSERT_Category_DL(Category_Name,Category_Order_By, Created_By);
               return objDAL.INSERT_Category_DL(Category_ID, Category_Name, Category_Order_By, GradeType_Id, Created_By);
           }
           catch
           {
               throw;
           }
       }

       public int UPDATE_Category(int ID, string Category_Name, int Category_Order_By, int GradeType_Id, int Updated_By)
       {
           try
           {
               return objDAL.UPDATE_Category_DL(ID, Category_Name,Category_Order_By,GradeType_Id, Updated_By);
           }
           catch
           {
               throw;
           }
       }

       public int DELETE_Category(int ID, int Deleted_By)
       {
           try
           {
               return objDAL.DELETE_Category_DL(ID, Deleted_By);
           }
           catch
           {
               throw;
           }
       }      
       public DataTable Get_GradingList()
       {
           try
           {
               return objDAL.Get_GradingList_DL();
           }
           catch
           {
               throw;
           }

       }

       public int INSERT_Grading(string Grading_Name, int Grade_Type, int Min, int Max, int Divisions, int Created_By)
       {
           try
           {
               return objDAL.INSERT_Grading_DL(Grading_Name, Grade_Type, Min, Max, Divisions, Created_By);
           }
           catch
           {
               throw;
           }

       }

       public int UPDATE_Grading(int ID, string Grading_Name, int Grading_Type, int Min, int Max, int Divisions, int Updated_By)
       {
           try
           {
               return objDAL.UPDATE_Grading_DL(ID, Grading_Name, Grading_Type, Min, Max, Divisions, Updated_By);
           }
           catch
           {
               throw;
           }

       }

       public int DELETE_Grading(int ID, int Deleted_By)
       {
           try
           {
               return objDAL.DELETE_Grading_DL(ID, Deleted_By);
           }
           catch
           {
               throw;
           }

       }

       public DataTable Get_GradingOptions(int Grade_ID)
       {
           try
           {
               return objDAL.Get_GradingOptions_DL(Grade_ID);
           }
           catch
           {
               throw;
           }
       }

       public int INSERT_GradingOption(int Grade_ID, string OptionText, decimal OptionValue, int Created_By)
       {
           try
           {
               return objDAL.INSERT_GradingOption_DL(Grade_ID, OptionText, OptionValue, Created_By);
           }
           catch
           {
               throw;
           }
       }
    public DataTable Get_CriteriaList(int Category_ID)
       {
           try
           {
               return objDAL.Get_CriteriaList_DL(Category_ID);
           }
           catch
           {
               throw;
           }
       }
       //public int INSERT_Criteria(int Category_ID, string Criteria_Name, int Criteria_Order_By, int GradeType_Id, int Created_By)
       //{
       //    try
       //    {
       //        return objDAL.INSERT_Criteria_DL(Category_ID, Criteria_Name, Criteria_Order_By, GradeType_Id, Created_By);
       //    }
       //    catch
       //    {
       //        throw;
       //    }
       //}

       //public int UPDATE_Criteria(int ID, string Criteria_Name, int Criteria_Order_By, int GradeType_Id, int Updated_By)
       //{
       //    try
       //    {
       //        return objDAL.UPDATE_Criteria_DL(ID, Criteria_Name, Criteria_Order_By, GradeType_Id, Updated_By);
       //    }
       //    catch
       //    {
       //        throw;
       //    }
       //}

       public int DELETE_Criteria(int ID, int Deleted_By)
       {
           try
           {
               return objDAL.DELETE_Category_DL(ID, Deleted_By);
           }
           catch
           {
               throw;
           }
       }
       public int INS_Rank_Voyage_Report(int MstRankID, int CERankID, string Mode, int UserID)
       {
           return objDAL.INS_Rank_Voyage_Report(MstRankID, CERankID, Mode, UserID);
       }
       public DataTable Get_Rank_Voyage_Config()
       {
           return objDAL.Get_Rank_Voyage_Config();
       }
           
    }
}
