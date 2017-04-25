using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using SMS.Data.ASL;
using SMS.Data.INFRA;
using SMS.Properties;

namespace SMS.Business.ASL
{
    public  class BLL_TypeManagement
    {
        DAL_TypeManagement objDAL = new DAL_TypeManagement();

        public BLL_TypeManagement()
        {
            //
            // TODO: Add constructor logic here
            //
        }
       public DataTable Get_SysVariable_Type()
        {
            try
            {
                return objDAL.Get_SysVariable_Type_DL();
            }
            catch
            {
                throw;
            }
        }
       public DataTable Get_UserTypeAccess(string variable_Type, int userid, int LoginUser)
        {
            try
            {
                return objDAL.Get_UserTypeAccess_DL(variable_Type, userid, LoginUser);
            }
            catch
            {
                throw;
            }

        }
       public UserAccess Get_UserTypeAccess(int UserID, string Variable_Type, string Variable_Code, string Approver_Type)
       {
           try
           {
                UserAccess objUserAccess = new UserAccess();

                if (UserID == 1)
                {
                    objUserAccess.IsAdmin = 1;
                    objUserAccess.Id = 1;
                    objUserAccess.UserId = 1;
                    objUserAccess.Menu_Code = 1;
                    objUserAccess.View = 1;
                    objUserAccess.Add = 1;
                    objUserAccess.Edit = 1;
                    objUserAccess.Delete = 1;
                    objUserAccess.Approve = 1;
                    objUserAccess.Admin = 1;
                }
                else
                {

                    DataTable dtUserAccess = objDAL.Get_UserTypeAccess(UserID, Variable_Type, Variable_Code, Approver_Type);
                    if (dtUserAccess.Rows.Count > 0)
                    {
                        objUserAccess.IsAdmin = 0;
                        objUserAccess.Id = int.Parse(dtUserAccess.Rows[0]["id"].ToString());
                        objUserAccess.UserId = int.Parse(dtUserAccess.Rows[0]["UserID"].ToString());
                       // objUserAccess.Menu_Code = dtUserAccess.Rows[0]["VARIABLE_CODE"].ToString();
                        objUserAccess.View = int.Parse(dtUserAccess.Rows[0]["IsAccess"].ToString());
                        objUserAccess.Add = int.Parse(dtUserAccess.Rows[0]["Access_Add"].ToString());
                        objUserAccess.Edit = int.Parse(dtUserAccess.Rows[0]["Access_Edit"].ToString());
                        objUserAccess.Delete = int.Parse(dtUserAccess.Rows[0]["Access_Delete"].ToString());
                        objUserAccess.Approve = int.Parse(dtUserAccess.Rows[0]["Access_Approve"].ToString());
                        if (dtUserAccess.Rows[0]["Access_Admin"].ToString().Trim().Length == 0)
                            objUserAccess.Admin = 0;
                        else
                            objUserAccess.Admin = int.Parse(dtUserAccess.Rows[0]["Access_Admin"].ToString());
                    }
                }
                return objUserAccess;
           }
           catch
           {
               throw;
           }

       }
       public int Update_User_Type_Access(int UserID, string VCode, int View, int Add, int Edit, int Delete, int Approve, int Admin, string Variable_Type, int Created_By)
        {
            try
            {
                return objDAL.Update_User_Type_Access_DL(UserID, VCode, View,Add,Edit,Delete,Approve,Admin, Variable_Type, Created_By);
            }
            catch
            {
                throw;
            }

        }
        public int Copy_TypeAccessFromUser(int CopyFromUserID, int CopyToUserID, int AppendMode, string Selected_Mod_Code, int Created_By)
        {
            try
            {
                return objDAL.Copy_TypeAccessFromUser_DL(CopyFromUserID, CopyToUserID, AppendMode, Selected_Mod_Code, Created_By);
            }
            catch
            {
                throw;
            }

        }

    }
}
