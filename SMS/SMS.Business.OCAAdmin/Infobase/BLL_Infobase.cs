using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using SMS.Data.OCAAdmin;

namespace SMS.Business.OCAAdmin
{
    public class BLL_Infobase
    {
        DAL_Infobase objDAL = new DAL_Infobase();

        public DataTable Get_SavedQuery(string Command_Type)
        {
           return  objDAL.Get_SavedQuery(Command_Type);
        }
        public DataTable GET_Dept_FolderList(int DepartmentID, string SearchText, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.GET_Dept_FolderList(DepartmentID, SearchText, pagenumber, pagesize, ref isfetchcount); 
        }


        public int INS_Dept_Folder(int Folder_Id, int DepartmentId, string Folder_Name, string Table_Name,string Table_Query, string  List_Query ,  string Link_Value_Field, string Link_Text_Field, string Link_DisplayName, int Company_Id,  int Created_By)
        {
            return objDAL.INS_Dept_Folder(Folder_Id, DepartmentId, Folder_Name, Table_Name, Table_Query,List_Query, Link_Value_Field, Link_Text_Field, Link_DisplayName, Company_Id, Created_By);
        }

        public int Delete_DocFolder(int Folder_ID, int Deleted_By)
        {
            return objDAL.Delete_DocFolder(Folder_ID, Deleted_By);

        }

        public DataSet Get_AssignedAttributes(int Folder_Id)
        {
            return objDAL.Get_AssignedAttributes(Folder_Id);
        }



        public int InsertDocAttribute(int Folder_ID, string AttributeName, string AttributeDataType, bool IsRequired, string ListSource, string Value_Field, string Text_Field, int Created_By)
        {
            return objDAL.InsertDocAttribute(Folder_ID,  AttributeName,  AttributeDataType,  IsRequired,  ListSource,Value_Field, Text_Field, Created_By);
        }

        public int Update_DocAttribute(int Attribute_ID, string AttributeName, bool IsRequired,  int Created_By)
        {
            return objDAL.Update_DocAttribute(Attribute_ID, AttributeName, IsRequired, Created_By);
        }

        public int Delete_DocAttribute(int AttributeID, int Deleted_By)
        {
            return objDAL.Delete_DocAttribute(AttributeID, Deleted_By);

        }


        public DataTable Get_Dept_UserList(int Dept_Id, int Company_Id, int Folder_Id)
        {
            return objDAL.Get_Dept_UserList(Dept_Id, Company_Id, Folder_Id);

        }


        public int UPD_UserRights(int Folder_Id, string Folder_Access, DataTable dtUserRights, int Created_By)
        {
            return objDAL.UPD_UserRights(Folder_Id, Folder_Access, dtUserRights, Created_By);
        }



        public int INS_UserRights(int Folder_Id, string Folder_Access, DataTable dtUserRights, int Created_By)
        {
            return objDAL.INS_UserRights(Folder_Id, Folder_Access, dtUserRights, Created_By);
        }
        public DataSet GET_UserRightDetails(int Dept_Id, int Folder_Id)
        {
            return objDAL.GET_UserRightDetails(Dept_Id, Folder_Id);
        }

        public DataTable GET_UserDeptmentList(int User_Id)
        {
            return objDAL.GET_UserDeptmentList(User_Id);
        }

        public DataTable GET_UserFolderList(int User_Id, int Dept_Id)
        {
            return objDAL.GET_UserFolderList(User_Id, Dept_Id);
        }

        public int Delete_UserRight(int FolderId, int UserRightID, int Deleted_By)
        {
           return objDAL.Delete_UserRight(FolderId, UserRightID, Deleted_By);
        }
        public DataTable Get_Query(string QueryName)
        {
            return objDAL.Get_Query(QueryName);
        }


        public DataTable Info_ExecuteQuery(string Query)
        {
            return objDAL.Info_ExecuteQuery(Query);
        }


        public string Insert_UploadedFiles(int Folder_ID, string File_Name, string File_Extention, int File_Size, string Link_ID_Value, string Info_Content, string Info_Title, DataTable dtFileAttributes, int Created_By)
        {
            return objDAL.Insert_UploadedFiles(Folder_ID, File_Name, File_Extention, File_Size, Link_ID_Value, Info_Content, Info_Title, dtFileAttributes, Created_By);
        }
        public DataTable Get_Files(int? Folder_ID, string SearchText, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            return objDAL.Get_Files(Folder_ID, SearchText, sortby, sortdirection, pagenumber, pagesize, ref  isfetchcount);
        }
    }
}
