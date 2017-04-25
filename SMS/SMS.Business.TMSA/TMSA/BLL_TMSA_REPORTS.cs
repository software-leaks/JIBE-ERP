using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.TMSA;

namespace SMS.Business.TMSA
{
    public class BLL_TMSA_REPORTS
    {
        DAL_TMSA_REPORTS objDAL = new DAL_TMSA_REPORTS();


        /// <summary>
        ///Description: Method to search TMSA Report
        /// Created By: Gargi
        /// Created On: 29/09/2016
        /// </summary>
        /// <param name="ElementIDs"></param>
        /// <param name="StageIDs"></param>
        /// <param name="LevelIDs"></param>
        /// <param name="Role"></param>
        /// <param name="iVersionNo"></param>
        /// <returns></returns>

        public DataSet Search_OverallReport(string ElementIDs, string StageIDs, string LevelIDs, string Role, int iVersionNo)
        {
            try
            {
                return objDAL.Search_OverallReport(ElementIDs, StageIDs, LevelIDs, Role, iVersionNo);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Description: Method to GET data in Excel Sheet 
        /// Created By: Gargi
        /// Created On: 29/09/2016
        /// </summary>
        /// <param name="ElementIDs"></param>
        /// <param name="StageIDs"></param>
        /// <param name="LevelIDs"></param>
        /// <param name="iVersionNo"></param>
        /// <returns></returns>
        public DataSet Search_ExportToExcelOverallReport(string ElementIDs, string StageIDs, string LevelIDs, int iVersionNo)
        {
            try
            {
                return objDAL.Search_ExportToExcelOverallReport(ElementIDs, StageIDs, LevelIDs, iVersionNo);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Description: Method to Get Element List
        /// Created By: Gargi
        /// Created On: 29/09/2016 
        /// </summary>
        /// <param name="iVersionNo"></param>
        /// <returns></returns>
        public DataTable Get_ElementList(int iVersionNo)
        {
            try
            {
                return objDAL.Get_ElementList_DL(iVersionNo);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Description: Method to Get Stage List
        /// Created By: Gargi
        /// Created On: 29/09/2016 
        /// </summary>
        /// <param name="iVersionNo"></param>
        /// <returns></returns>
        public DataTable Get_StageList(int iVersionNo)
        {
            try
            {
                return objDAL.Get_StageList_DL(iVersionNo);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Description: Method to Get Level List
        /// Created By: Gargi
        /// Created On: 29/09/2016 
        /// </summary>
        /// <param name="iVersionNo"></param>
        /// <returns></returns>
        public DataTable Get_LevelList(int iVersionNo)
        {
            try
            {
                return objDAL.Get_LevelList_DL(iVersionNo);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to Get Vesion List
        /// </summary>
        /// <returns></returns>
        public DataTable Get_VersionList()
        {
            try
            {
                return objDAL.Get_VersionList_DL();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// This Method is get to Count Of levels
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="LinkType"></param>
        /// <returns></returns>
        public DataSet GetCount(int ParentID, string LinkType)
        {
            try
            {
                return objDAL.GetCount(ParentID, LinkType);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// This method is used to get links if exists for Report
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="LinkType"></param>
        /// <param name="LinkID"></param>
        /// <param name="DPath"></param>
        /// <param name="Notes"></param>
        /// <returns></returns>

        public DataSet LinkExists(int ParentID, string LinkType, string LinkID, string DPath, string Notes)
        {
            try
            {
                return objDAL.LinkExists(ParentID, LinkType, LinkID, DPath, Notes);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// This method is used to Save data for Report
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="LinkType"></param>
        /// <param name="LinkID"></param>
        /// <param name="DPath"></param>
        /// <param name="Notes"></param>
        /// <returns></returns>

        public int SaveData(int ParentID, string LinkType, string LinkID, string DPath, string Notes)
        {
            try
            {
                return objDAL.SaveData(ParentID, LinkType, LinkID, DPath, Notes);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// This method is used to Save data for Report
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ParentID"></param>
        /// <param name="AuditedProcess"></param>
        /// <param name="Compliance"></param>
        /// <returns></returns>
        public int UpdateData(int ID,int ParentID, string AuditedProcess, int Compliance)
        {
            try
            {
                return objDAL.UpdateData(ID,ParentID, AuditedProcess, Compliance);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// This method is used to Save data for Report
        /// </summary>
        /// <param name="LinkID"></param>
        /// <returns></returns>
        public int DeleteData(int LinkID)
        {
            try
            {
                return objDAL.DeleteData(LinkID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to Create new Version
        /// </summary>
        /// <returns></returns>
        public int CopyNewVersion()
        {
            try
            {
                return objDAL.CopyNewVersion();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Used to Get Pie Chart Data
        /// </summary>
        /// <param name="LevelNO"></param>
        /// <param name="iVersionNo"></param>
        /// <returns></returns>

        public DataTable Get_PieChartData(int LevelNO, int iVersionNo)
        {
            try
            {
                return objDAL.Get_PieChartData_DL(LevelNO, iVersionNo);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// This method is used to get links if exists for Report
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="LinkType"></param>
        /// <param name="LinkID"></param>
        /// <param name="DPath"></param>
        /// <param name="Notes"></param>
        /// <returns></returns>

        public DataSet FillTree(int userid)
        {
            try
            {
                return objDAL.FillTree(userid);
            }
            catch
            {
                throw;
            }
        }

    }


}
