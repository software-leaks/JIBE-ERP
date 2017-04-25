using System;
using System.Data;
using System.Configuration;
using SMS.Data.Infrastructure;
using System.Collections.Generic;
using System.Text;

using System.Collections;


/// <summary>
/// Summary description for SysPar_BAL
/// </summary>
namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_SysParamater
    {
        public BLL_Infra_SysParamater()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataSet GetModules(int status)
        {
            DAL_Infra_SysParameter DBL = new DAL_Infra_SysParameter();
            return DBL.GetModules(status);
        }

        public DataSet GetModulesTable()
        {
            DAL_Infra_SysParameter DBL = new DAL_Infra_SysParameter();
            return DBL.GetModulesTable();
        }
        public DataSet GetListValues(string tableName, string parentcode, string status)
        {
            DAL_Infra_SysParameter DBL = new DAL_Infra_SysParameter();
            return DBL.GetListValues(tableName, parentcode,status);
        }

        public int insertSysparameter(string query)
        {
            DAL_Infra_SysParameter DBL = new DAL_Infra_SysParameter();
            return DBL.insertSysparameter(query);
        }

        public int deleteSysparameter(string query)
        {
            DAL_Infra_SysParameter DBL = new DAL_Infra_SysParameter();
            return DBL.deleteSysparameter(query);
        }

        public DataSet GetDetailOfSystemParameter(int code, string tablename)
        {
            DAL_Infra_SysParameter DBL = new DAL_Infra_SysParameter();
            return DBL.GetDetailOfSystemParameter(code, tablename);
        }

        public int updateSysParam(int ChildId, string Name, int Parent, string Description, int Active,string tablename,int user)
        {
            DAL_Infra_SysParameter DBL = new DAL_Infra_SysParameter();
            return DBL.updateSysParam(ChildId, Name, Parent, Description, Active, tablename,user);
        }

        public int insertSysModule(string name, string TableName,int user,int active)
        {
            DAL_Infra_SysParameter DBL = new DAL_Infra_SysParameter();
            return DBL.insertSysModule(name, TableName,user,active);
        }
    }
}
