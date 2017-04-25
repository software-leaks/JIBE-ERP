using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;


namespace SMS.Data.OCAAdmin
{
    public class DAL_Infobase
    {

        IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
        private string connection = "";
        public DAL_Infobase(string ConnectionString)
        {
            connection = ConnectionString;
        }
        public DAL_Infobase()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        }


        public DataTable Get_DepartmentList(int? CompanyId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
			{   
				new SqlParameter("@CompanyID",CompanyId),
			};

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INFO_Get_DepartmentList", sqlprm).Tables[0];
        }





        #region --DOCUMENT ATTRIBUTE LIBRARY --
        public DataTable Get_DocAttributeList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INFO_Get_DocAttributeList").Tables[0];
        }

        public DataTable Get_AttributeDetails_DL(int AttributeID)
        {
            SqlParameter sqlprm = new SqlParameter("@AttributeID", AttributeID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_INFO_Get_AttributeDetails", sqlprm).Tables[0];
        }

        public int UpdDocAttribute(int AttributeID, string AttributeName, string AttributeDataType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@AttributeID",AttributeID),
                                            new SqlParameter("@AttributeName",AttributeName),
                                            new SqlParameter("@AttributeDataType",AttributeDataType)
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INFO_Update_DocAttribute", sqlprm);

        }

        public int InsertDocAttribute(int Folder_ID, string AttributeName, string AttributeDataType, bool IsRequired, string ListSource, string Value_Field, string Text_Field, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Folder_Id",Folder_ID),
                                            new SqlParameter("@AttributeName",AttributeName),
                                            new SqlParameter("@AttributeDataType",AttributeDataType),
                                            new SqlParameter("@Is_Required",IsRequired),
                                            new SqlParameter("@ListSource",ListSource),
                                            new SqlParameter("@Value_Field",Value_Field),
                                            new SqlParameter("@Text_Field",Text_Field),
                                            new SqlParameter("@Created_By",Created_By)

                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INFO_Insert_DocAttribute", sqlprm);
        }



        public int Update_DocAttribute(int Attribute_ID, string AttributeName,  bool IsRequired, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Attribute_Id",Attribute_ID),
                                            new SqlParameter("@AttributeName",AttributeName),
                                            new SqlParameter("@Is_Required",IsRequired),
                                            new SqlParameter("@Created_By",Modified_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_INFO_Update_DocAttribute", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


        /*! \brief 
         *  Function is used to get result of saved query
         *
         *  \details 
         *  This function is used to get  saved SQL query string
         *
         * \param[string] Query/SP name
         *
         * \retval[DataSet] 
         */
        public DataTable Get_Query(string QueryName)
        {
            SqlParameter sqlprm = new SqlParameter("@Query_Name", QueryName);

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "[INFO_SP_Get_Query]", sqlprm).Tables[0];
        }


        /*! \brief 
         *  Function is used to get result of saved query
         *
         *  \details 
         *  This function is used to get result of saved query
         *
         * \param[string] Query/SP name
         *
         * \retval[DataSet] 
         */
        public DataTable Info_ExecuteQuery(string Query)
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, Query).Tables[0];
        }

        public int Delete_DocAttribute(int AttributeID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@AttributeId",AttributeID),
                                            new SqlParameter("@Created_By",Deleted_By)
        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "[INFO_SP_Delete_DocAttribute]", sqlprm);
        }


        #endregion

        #region -- DOC Folder Section--


        public int Delete_DocFolder(int Folder_ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Folder_ID",Folder_ID),
                                            new SqlParameter("@Created_By",Deleted_By)
        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INFO_SP_Delete_DocFolder", sqlprm);
        }


        public DataSet Get_AssignedAttributes(int FolderID)
        {
            SqlParameter sqlprm = new SqlParameter("@Folder_Id", FolderID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INFO_SP_Get_AssignedAttributes", sqlprm);
        }

       
        /*! \brief 
         *  Function is used to get Saved Query  list
         *
         *  \details 
         *  This function is used to  get Saved Query  list for infobase
         *
         * \param[string] Command_Type  defines SP or table name
         *
         * \retval[Table] List
         */

        public DataTable Get_SavedQuery( string Command_Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Command_Type",Command_Type)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INFO_Get_QueryList", sqlprm).Tables[0];
        }

       


        public int INS_Dept_Folder(int Folder_Id,  int DepartmentId, string Folder_Name, string Table_Name,string Table_Query,  string List_Query, string Link_Value_Field, string Link_Text_Field ,string Link_DisplayName, int Company_Id, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@ID",Folder_Id),
                                            new SqlParameter("@Department_ID",DepartmentId),
                                            new SqlParameter("@Folder_Name",Folder_Name),
                                             new SqlParameter("@Table_Name",Table_Name),
                                            new SqlParameter("@Table_Query",Table_Query),
                                            new SqlParameter("@List_Query",List_Query),
                                            new SqlParameter("@Link_Value_Field",Link_Value_Field),
                                            new SqlParameter("@Link_Text_Field",Link_Text_Field),
                                            new SqlParameter("@Link_DisplayName", Link_DisplayName),
                                            new SqlParameter("@Company_ID",Company_Id),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INFO_SP_INS_Dept_Folder", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }



        /*
         * Method: To get the Folder list  for Infobase
         * Parameter: DepartmentID, earchtext , display page number, page records 
         *Return: Total no of records
         */

        public DataTable GET_Dept_FolderList(int DepartmentID, string SearchText, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@Department_ID",DepartmentID),
                   new System.Data.SqlClient.SqlParameter("@Search_Text",SearchText),
                   new System.Data.SqlClient.SqlParameter("@PAGE_INDEX",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGE_SIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INFO_SP_GET_Dept_FolderList", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }


        #endregion
        #region -- Folder Rights Section--

        /*! \brief 
         *  Function is used to get department user  list
         *
         *  \details 
         *  This function is used to user  list by department
         *
         * \param[int] DepartmentId, CompanyId
         *
         * \retval[Table] List
         */

        public DataTable Get_Dept_UserList(int Dept_Id, int Company_Id, int Folder_Id)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Dept_Id",Dept_Id),
                                            new SqlParameter("@Company_ID",Company_Id),
                                             new SqlParameter("@Folder_Id",Folder_Id)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INFO_SP_GET_Dept_UserList", sqlprm).Tables[0];
        }

        /*! \brief 
         *  Funtion is used to add or modify user rights
         *
         *  \details 
         *  This function is used to Update user rights
         *
         * \param[int] FolderId, Folder access type
         *
         * \param[string] Access type
         *  
         * \retval[int] Success
         */

        public int UPD_UserRights(int Folder_Id, string Folder_Access, DataTable  dtUserRights, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Folder_ID",Folder_Id),
                                            new SqlParameter("@Folder_Access",Folder_Access),
                                            new SqlParameter("@dtUserRights",dtUserRights),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INFO_SP_UPD_UserRights", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }




        /*! \brief 
         *  Funtion is used to add user rights
         *
         *  \details 
         *  This function is used to add or modify user rights
         *
         * \param[int] FolderId, Folder access type
         *
         * \param[string] Access type
         *  
         * \retval[int] Success
         */

        public int INS_UserRights(int Folder_Id, string Folder_Access, DataTable dtUserRights, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Folder_ID",Folder_Id),
                                            new SqlParameter("@Folder_Access",Folder_Access),
                                            new SqlParameter("@dtUserRights",dtUserRights),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("@Return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INFO_SP_INS_UserRights", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }





        /*! \brief 
         *  Function is used to get department user rights list
         *
         *  \details 
         *  This function is used to user rights list by folder & department
         *
         * \param[int] Folder_Id, Dept_Id
         *
         * \retval[DataSet] 
         */

        public DataSet GET_UserRightDetails(int Dept_Id, int Folder_Id)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Folder_ID",Folder_Id),
                                            new SqlParameter("@Department_ID",Dept_Id)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INFO_SP_GET_UserRightDetails", sqlprm);
        }

        

        #endregion

        #region -- File Uplaod Section--

        /*! \brief 
         *  Function is used to get departments by  user
         *
         *  \details 
         *  This function is used to departments by userid
         *
         * \param[int] User_Id
         *
         * \retval[DataSet] 
         */

        public DataTable GET_UserDeptmentList(int User_Id)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@User_ID",User_Id)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INFO_SP_GET_UserDeptmentList", sqlprm).Tables[0];
        }



        /*! \brief 
         *  Function is used to get department folders by  Department/user
         *
         *  \details 
         *  This function is used to department folders by userid and department id to be used to create tree view
         *
         * \param[int] User_Id, Dept_Id 
         *
         * \retval[DataSet] 
         */

        public DataTable GET_UserFolderList(int User_Id, int Dept_Id)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@User_ID",User_Id),
                                            new SqlParameter("@Dept_Id",Dept_Id)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INFO_SP_GET_UserFolderList", sqlprm).Tables[0];
        }




        public int Delete_UserRight(int FolderId, int UserRightID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Folder_Id",FolderId),
                                            new SqlParameter("@User_Right_ID",UserRightID),
                                            new SqlParameter("@Created_By",Deleted_By)
        };

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "[INFO_SP_Delete_UserRights]", sqlprm);
        }



        /*! \brief 
         *  Function is used to insert uploaded files
         *
         *  \details 
         *  This function is used to save uploaded files
         * \param[int] Folder_ID, File_Size 
         *
         * \retval[String] FileSeq 
         */

        public string Insert_UploadedFiles(int Folder_ID, string File_Name, string File_Extention, int File_Size, string Link_ID_Value, string Info_Content, string Info_Title, DataTable dtFileAttributes, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
            new SqlParameter("@Folder_ID",Folder_ID),
            new SqlParameter("@File_Name",File_Name),
            new SqlParameter("@File_Extention",File_Extention),
            new SqlParameter("@File_Size",File_Size),
            new SqlParameter("@Link_ID_Value",Link_ID_Value),
            new SqlParameter("@Info_Content",Info_Content),
            new SqlParameter("@Info_Title",Info_Title),
            new SqlParameter("@Created_By",Created_By),
            new SqlParameter("return",SqlDbType.VarChar),
            new SqlParameter("@dtFileAttributes",dtFileAttributes)
                                        };
            sqlprm[sqlprm.Length - 2].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "INFO_SP_Insert_UploadedFiles", sqlprm);
            return sqlprm[sqlprm.Length - 2].Value.ToString();
        }




        public DataTable Get_Files(int? Folder_ID, string SearchText, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            System.Data.SqlClient.SqlParameter[] obj = new System.Data.SqlClient.SqlParameter[] 
            { 
                   new System.Data.SqlClient.SqlParameter("@SearchText", SearchText),
                   new System.Data.SqlClient.SqlParameter("@Folder_ID", Folder_ID), 
                   new System.Data.SqlClient.SqlParameter("@SORTBY",sortby), 
                   new System.Data.SqlClient.SqlParameter("@SORTDIRECTION",sortdirection), 
                   new System.Data.SqlClient.SqlParameter("@PAGENUMBER",pagenumber),
                   new System.Data.SqlClient.SqlParameter("@PAGESIZE",pagesize),
                   new System.Data.SqlClient.SqlParameter("@ISFETCHCOUNT",isfetchcount)
            };
            obj[obj.Length - 1].Direction = ParameterDirection.InputOutput;
            DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "INFO_SP_Get_Files", obj);
            isfetchcount = Convert.ToInt32(obj[obj.Length - 1].Value);
            return ds.Tables[0];
        }





        #endregion

    }
}
