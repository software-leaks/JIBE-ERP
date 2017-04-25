using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using SMS.Data.Infrastructure;

/// <summary>
/// Summary description for BLL_Infra_SysParam
/// </summary>
/// 
namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_SysParam
    {

        DAL_Infra_SysParam objDAL = new DAL_Infra_SysParam();

        public BLL_Infra_SysParam()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //public DataTable GetModules(int status)
        //{
        //    try
        //    {
        //        return objDAL.GetModules(status);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public int InsertSysModule(string name, string TableName, int user, int active)
        {
            try
            {
                return objDAL.InsertSysModule_DL(name, TableName, user, active);
            }
            catch
            {
                throw;
            }
        }

        public DataSet GetModulesTable()
        {
            try
            {
                return objDAL.GetModulesTable_DL();
            }
            catch
            {
                throw;
            }
        }


        public DataTable GetListValues(string tablename, string parentcode, string status)
        {
            try
            {

                return objDAL.GetListValues_DL(tablename, parentcode, status);
            }
            catch
            {
                throw;
            }
        }


        public int InsertSysParameter(string query)
        {
            try
            {
                return objDAL.InsertSysParameter_DL(query);
            }
            catch
            {
                throw;
            }
        }


        public int DeleteSysParameter(string query)
        {
            try
            {
                return objDAL.DeleteSysParameter_DL(query);
            }
            catch
            {
                throw;
            }
        }


        public DataSet GetDetailOfSystemParameter(int code, string tablename)
        {
            try
            {
                return objDAL.GetDetailOfSystemParameter_DL(code, tablename);
            }
            catch
            {
                throw;
            }
        }


        public int UpdateSysParam(int ChildId, string Name, int Parent, string Description, int Active, string tablename, int user)
        {
            try
            {
                return objDAL.UpdateSysParam_DL(ChildId, Name, Parent, Description, Active, tablename, user);
            }
            catch
            {
                throw;
            }
        }

    }

}