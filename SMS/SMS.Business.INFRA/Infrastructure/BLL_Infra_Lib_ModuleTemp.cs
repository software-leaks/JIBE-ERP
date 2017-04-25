using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Infrastructure;


namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_Lib_ModuleTemp
    {
        //DAL_Infra_Lib_ModuleTemp objDAL = new DAL_Infra_Lib_ModuleTemp();

        public BLL_Infra_Lib_ModuleTemp()
        {
            
        }
        public static  DataTable Get_ModuleList(int ModuleID)
        {
            return DAL_Infra_Lib_ModuleTemp.Get_ModuleList_DL(ModuleID);
        }

        public static int Update_ModuleName(int ModuleID, string ModuletName, int UserId)
        {
            return DAL_Infra_Lib_ModuleTemp.Update_ModuleName_DL(ModuleID, ModuletName, UserId);
        }
        public static int DELETE_Module(int ModuleID, string ModuletName, int UserId)
        {
            return DAL_Infra_Lib_ModuleTemp.Delete_Modul_DL(ModuleID, ModuletName, UserId);
        }


        public static DataTable Get_ModuleList()
        {
            return DAL_Infra_Lib_ModuleTemp.Get_ModuleList_DL();
        }

        public static DataTable Get_ModuleTemplateList(int TemplateID)
        {
            return DAL_Infra_Lib_ModuleTemp.Get_ModuleTemplateList_DL(TemplateID);
        }



        public static int Save_Module_Template(int ModuleID, string TemplateName, string TemplateText, int Created_By, int TemplateType)
        {
            try
            {

                return DAL_Infra_Lib_ModuleTemp.Save_Module_Template_DL(ModuleID, TemplateName, TemplateText, Created_By, TemplateType);
            }
            catch
            {
                throw;
            }
        }

        public static int Save_Module(string Module, int CreatedBy)
        {
            return DAL_Infra_Lib_ModuleTemp.Save_Module_DL(Module, CreatedBy);
        }
        /// <summary>
        /// get the Module Template by ModuleType Id
        /// </summary>
        /// <param name="ContractId"></param>
        /// <returns></returns>
        public static DataTable Get_ModuleTemplate(int moduletypeId)
        {
            try
            {
                return DAL_Infra_Lib_ModuleTemp.Get_ModuleTemplate_DL(moduletypeId);
            }
            catch
            {
                throw;
            }
        }
    }
}
