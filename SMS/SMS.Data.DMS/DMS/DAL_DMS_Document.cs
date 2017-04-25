using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Configuration;
using SMS.Data;
using SMS.Properties;


namespace SMS.Data.DMS
{
    public class DAL_DMS_Document
    {
        private string connection = "";
        public DAL_DMS_Document(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_DMS_Document()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        # region GET - DOCUMENT - LIST
        /// <summary>
        /// Gets all active documents
        /// </summary>
        /// <returns>Datareader object</returns>
        public IDataReader GetActiveDocuments()
        {
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "SP_DMS_Get_ActiveDocuments");
        }

        /// <summary>
        /// Gets all active documents having file size between the limits
        /// </summary>
        /// <param name="SizeBiteFrom"></param>
        /// <param name="SizeBiteTo"></param>
        /// <returns>Datareader object</returns>
        public IDataReader GetDocumentsBySize(double SizeByteFrom, double SizeByteTo)
        {
            SqlParameter[] obj = new SqlParameter[]
            {   
                new SqlParameter("@SizeByteFrom",SizeByteFrom),
                new SqlParameter("@SizeByteTo",SizeByteTo)
            };
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocumentsBySize", obj);
        }

        /// <summary>
        /// Gets all active documents having creation date between the limits
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns>Datareader object</returns>
        public IDataReader GetDocumentsByCreateDate(DateTime StartDate, DateTime EndDate)
        {
            SqlParameter[] obj = new SqlParameter[]
            {   
                new SqlParameter("@StartDate",StartDate),
                new SqlParameter("@EndDate",EndDate)
            };
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocumentsByCreationDate", obj);
        }

        /// <summary>
        /// Gets all active documents having DOCNAME,DOCFILENAME like search param
        /// </summary>
        /// <param name="DocName"></param>
        /// <returns>Datareader object</returns>
        public IDataReader GetDocumentsByDocName(string DocName)
        {
            SqlParameter[] obj = new SqlParameter[]
            {   
                new SqlParameter("@DocName",DocName)
            };
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocumentsByDocName", obj);
        }

        /// <summary>
        /// Gets all active documents having DOC HEADER like search param
        /// </summary>
        /// <param name="TextContent"></param>
        /// <returns>Datareader object</returns>
        public IDataReader GetDocumentsByContent(string TextContent)
        {
            SqlParameter[] obj = new SqlParameter[]
            {   
                new SqlParameter("TextContent",TextContent)
            };
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocumentsByContent", obj);
        }

        /// <summary>
        /// Gets all active documents having Parent folder ID as param
        /// </summary>
        /// <param name="ParentFolderID"></param>
        /// <returns>Datareader object</returns>
        public IDataReader GetDocumentsByParentFolderID(int ParentFolderID)
        {
            SqlParameter[] obj = new SqlParameter[]
            {   
                new SqlParameter("ParentFolderID",ParentFolderID)
            };
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocumentsByParentFolderID", obj);
        }

        /// <summary>
        /// Gets all active documents last viewed in given period
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns>Datareader object</returns>
        public IDataReader GetDocumentsByLastViewDate(DateTime StartDate, DateTime EndDate)
        {
            SqlParameter[] obj = new SqlParameter[]
            {   
                new SqlParameter("@StartDate",StartDate),
                new SqlParameter("@EndDate",EndDate)
            };
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocumentsByLastViewDate", obj);
        }
        
        /// <summary>
        /// Gets the file name with extenssion
        /// </summary>
        /// <param name="DocID"></param>
        /// <returns>Datareader object</returns>
        public IDataReader Get_DocumentFileNameByDocID_DL(int DocID)
        {
            string SQLCommandText;
            SQLCommandText = "SELECT DocFileName FROM DMS_DTL_DOCUMENT WHERE DocID=@DocID";

            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocID",DocID)
                                        };
            return SqlHelper.ExecuteReader(connection, CommandType.Text, SQLCommandText, sqlprm);
        }

        #endregion

        public DataTable GetDocumentTypeList()
        {
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocumentTypeList").Tables[0];
            return dt;
        }
        public DataTable GetDocumentTypeList(int CheckList)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                new SqlParameter("@CheckList",CheckList)
            };
            
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocumentTypeList", obj).Tables[0];
            return dt;
        }
        public DataTable GetDocumentTypeList(int CheckList, int Voyage)
        {
            SqlParameter[] obj = new SqlParameter[]
            {
                new SqlParameter("@CheckList",CheckList),
                new SqlParameter("@Voyage",Voyage)
            };
            DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocumentTypeList", obj).Tables[0];
            return dt;
        }
        # region GET - DOCUMENT - DETAILS
        /// <summary>
        /// Gets document details by DocID
        /// </summary>
        /// <param name="DocID"></param>
        /// <returns>Datareader object</returns>
        public IDataReader GetDocumentDetailByDocID(int DocID)
        {
            SqlParameter[] obj = new SqlParameter[]
            {   
                new SqlParameter("@DocID",DocID)
            };

            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocumentDetailByDocID", obj);
        }

        #endregion

        # region -- CREW DOCUMENT FUNCTIONS --

        public int AddNewDocument_DL(int CrewID, string DocumentName, string DocFileName, int SizeByte, int DocTypeID, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {   
                new SqlParameter("@CrewID",CrewID),
                new SqlParameter("@DocumentName",DocumentName),
                new SqlParameter("@DocFileName",DocFileName),
                new SqlParameter("@SizeByte",SizeByte),
                new SqlParameter("@DocTypeID",DocTypeID),
                new SqlParameter("@Created_By",Created_By),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Insert_Crew_Document", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int AddNewDocument_DL(int CrewID, int DocTypeID, string DocumentName, string DocFileName, DateTime IssueDate, string IssuePlace, DateTime ExpiryDate, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {   
                new SqlParameter("@CrewID",CrewID),
                new SqlParameter("@DocTypeID",DocTypeID),
                new SqlParameter("@DocumentName",DocumentName),
                new SqlParameter("@DocFileName",DocFileName),
                new SqlParameter("@IssueDate",IssueDate),
                new SqlParameter("@IssuePlace",IssuePlace),
                new SqlParameter("@ExpiryDate",ExpiryDate),
                new SqlParameter("@Created_By",Created_By),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Insert_Crew_Document", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }

        public int UpdateAttributeValue_DL(int CrewID, int DocID, int AttributeID, int AttributeValue, int ModifiedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {   
                new SqlParameter("@CrewID",CrewID),
                new SqlParameter("@DocID",DocID),
                new SqlParameter("@AttributeID",AttributeID),
                new SqlParameter("@AttributeValue",AttributeValue),
                new SqlParameter("@ModifiedBy",ModifiedBy),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Update_DocAttributeValue_Int", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);

        }
        public int UpdateAttributeValue_DL(int CrewID, int DocID, int AttributeID, DateTime AttributeValue, int ModifiedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {   
                new SqlParameter("@CrewID",CrewID),
                new SqlParameter("@DocID",DocID),
                new SqlParameter("@AttributeID",AttributeID),
                new SqlParameter("@AttributeValue",AttributeValue),
                new SqlParameter("@ModifiedBy",ModifiedBy),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Update_DocAttributeValue_DateTime", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int UpdateAttributeValue_DL(int CrewID, int DocID, int AttributeID, string AttributeValue, int ModifiedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            {   
                new SqlParameter("@CrewID",CrewID),
                new SqlParameter("@DocID",DocID),
                new SqlParameter("@AttributeID",AttributeID),
                new SqlParameter("@AttributeValue",AttributeValue),
                new SqlParameter("@ModifiedBy",ModifiedBy),
                new SqlParameter("return",SqlDbType.Int)
            };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Update_DocAttributeValue_String", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public string Get_DocAttributeValue_DL(int DocID, int AttributeID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocID",DocID),
                                            new SqlParameter("@AttributeID",AttributeID),
                                        };
            return SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocAttributeValue", sqlprm).ToString();

        }
        public string Get_DocTypeID_DL(int DocID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocID",DocID)
                                        };
            return SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocTypeID", sqlprm).ToString();

        }

        public int Del_All_AttributeValuesForNewDoc_DL(int DocID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocID",DocID),
                                        };
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Del_All_DocAttributeValue", sqlprm);
            return 1;
        }
        public int Create_DocAttributesForNewDoc_DL(int DocID, int CrewID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocID",DocID),
                                            new SqlParameter("@CrewID",CrewID)
                                        };
             SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Insert_DocAttributesForNewDoc", sqlprm);
             return 1;
        }

        #endregion

        # region INSERT/UPDATE DOCUMENT DETAILS
        public int AddNewDoc(DocumentPoperties objDoc)
        {
            SqlParameter[] obj = new SqlParameter[]
            {   
                new SqlParameter("@DocNo",objDoc.DocNo),
                new SqlParameter("@DocName",objDoc.DocName),
                new SqlParameter("@DocFileName",objDoc.DocFileName),
                new SqlParameter("@DocFilePath",objDoc.DocFilePath),
                new SqlParameter("@DocTypeID",objDoc.DocTypeID),
                new SqlParameter("@DocHeader",objDoc.DocHeader),
                new SqlParameter("@DateOfIssue",objDoc.DateOfIssue),
                new SqlParameter("@PlaceOfIssue",objDoc.PlaceOfIssue),
                new SqlParameter("@IssuingAuthority",objDoc.IssuingAuthority),
                new SqlParameter("@CountryOfIssue",objDoc.CountryOfIssue),
                new SqlParameter("@DateOfExpiry",objDoc.DateOfExpiry),
                new SqlParameter("@ApproveStatus",objDoc.ApproveStatus),
                new SqlParameter("@ApprovedBy",objDoc.ApprovedBy),
                new SqlParameter("@Active_Status",objDoc.Active_Status),
                new SqlParameter("@SizeByte",objDoc.SizeByte),
                new SqlParameter("@CheckOutBy",objDoc.CheckOutBy),
                new SqlParameter("@CheckOutDate",objDoc.CheckOutDate),
                new SqlParameter("@ParentFolderId",objDoc.ParentFolderId),
                new SqlParameter("@CreatedBy",objDoc.CreatedBy),
                new SqlParameter("@return",SqlDbType.Int)
            };

            obj[4].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Sp_AddNewDoc", obj);

            //Return: new document id
            return Convert.ToInt32(obj[4].Value);

        }


        # endregion

        //# region OTHERS

        //public DataTable getAllDocumentVersion()
        //{
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_getAllDocumentVersion").Tables[0];
        //}

        //public DataTable getDocumentVersionByDocID(int DocID)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Doc_ID",SqlDbType.Int)
        //    };

        //    obj[0].Value = DocID;
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_getDocumentVersionByDocID", obj).Tables[0];
        //}

        //public IDataReader getDocumentByDocName(string Doc_Name, int ParentFolderId)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Doc_Name",SqlDbType.VarChar, 200),
        //        new SqlParameter("@ParentFolderId",SqlDbType.Int)
        //    };

        //    obj[0].Value = Doc_Name;
        //    obj[1].Value = ParentFolderId;

        //    return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "DMS_GetDocumentByDocNameParentID", obj);
        //}

        //public DataRow getDocumentStatus(int Doc_ID)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Doc_ID",SqlDbType.Int)
        //    };
        //    obj[0].Value = Doc_ID;

        //    DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_SP_getDocumentStatus", obj);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        return ds.Tables[0].Rows[0];
        //    }
        //    return null;
        //}

        //public int getSaveVersion(int Doc_ID)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Doc_ID",SqlDbType.Int)
        //    };
        //    obj[0].Value = Doc_ID;

        //    IDataReader ReaderSaveVersion;
        //    ReaderSaveVersion = SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "DMS_SP_GetDocSaveVersion", obj);
        //    if (ReaderSaveVersion.Read())
        //    {
        //        return Convert.ToInt32(ReaderSaveVersion["Save_version"]);
        //    }
        //    return 0;
        //}

        //public int getElementTypeId(int Doc_ID, int version, int save_Version, int ElementType_Id)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Doc_ID",SqlDbType.Int),
        //        new SqlParameter("@version",SqlDbType.Int),
        //        new SqlParameter("@save_Version",SqlDbType.Int),
        //        new SqlParameter("@ElementType_Id",SqlDbType.Int)
        //    };

        //    obj[0].Value = Doc_ID;
        //    obj[1].Value = version;
        //    obj[2].Value = save_Version;
        //    obj[3].Value = ElementType_Id;

        //    IDataReader readerElementTypeId;
        //    readerElementTypeId = SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "DMS_SP_getElementExists", obj);
        //    if (readerElementTypeId.Read())
        //    {
        //        return ElementType_Id;
        //    }
        //    return 0;
        //}

        //public bool CheckElementExist(int DocID, int DocElementID)
        //{
        //    SqlParameter[] obj = new SqlParameter[] 
        //    { 
        //        new SqlParameter("@Doc_ID",SqlDbType.Int),
        //        new SqlParameter("@Doc_Element_ID",SqlDbType.Int)
        //    };

        //    obj[0].Value = DocID;
        //    obj[1].Value = DocElementID;

        //    //return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "DMS_checkElementExist", obj);
        //    if (SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "DMS_checkElementExist", obj).Read())
        //    {
        //        return true;
        //    }
        //    return false;

        //}

        //public int getElementTypeId(int ID, int Doc_ID)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@ID",SqlDbType.Int),
        //        new SqlParameter("@Doc_ID",SqlDbType.Int),
        //    };

        //    obj[0].Value = ID;
        //    obj[1].Value = Doc_ID;

        //    //return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "DMS_SP_getElementType_ID", obj);

        //    IDataReader readerElementTypeId;
        //    readerElementTypeId = SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "DMS_SP_getElementType_ID", obj);
        //    if (readerElementTypeId.Read())
        //    {
        //        return Convert.ToInt32(readerElementTypeId["ElementType_Id"]);
        //    }
        //    return 0;
        //}


        //public bool checkFolderNameExist(int ParentFolderID, string FolderName)
        //{
        //    SqlParameter[] obj = new SqlParameter[] 
        //    { 
        //        new SqlParameter("@PID",SqlDbType.Int),
        //        new SqlParameter("@Node_Name",SqlDbType.VarChar, 50)                
        //    };

        //    obj[0].Value = ParentFolderID;
        //    obj[1].Value = FolderName;

        //    if (SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "DMS_CheckNameFolderExist", obj).Read())
        //    {
        //        return true;
        //    }

        //    return false;

        //}

        //public bool CheckAttributeExist(AttributeProperties objAtb)
        //{
        //    SqlParameter[] obj = new SqlParameter[] 
        //    { 
        //        new SqlParameter("@Doc_ID",SqlDbType.Int),
        //        new SqlParameter("@Version",SqlDbType.Int),
        //        new SqlParameter("@Save_Version",SqlDbType.Int),
        //        new SqlParameter("@Doc_Element_ID",SqlDbType.Int),
        //        new SqlParameter("@Attribute_ID",SqlDbType.Int)
        //    };

        //    obj[0].Value = objAtb.DocID;
        //    obj[1].Value = objAtb.version;
        //    obj[2].Value = objAtb.SaveVersion;
        //    obj[3].Value = objAtb.ElementDocID;
        //    obj[4].Value = objAtb.AttributeTypeID;

        //    if (SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "DMS_checkAttributeExist", obj).Read())
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public int GetFolderCount()
        //{
        //    //return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "DMS_getFolderCount");

        //    IDataReader readerFolderCnt = SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "DMS_getFolderCount");
        //    try
        //    {
        //        if (readerFolderCnt.Read())
        //        {
        //            return Convert.ToInt32(readerFolderCnt["Node_Count"]);
        //        }
        //    }
        //    finally
        //    {
        //        readerFolderCnt.Close();
        //    }
        //    return 0;
        //}

        //public DataTable GetAllDoc()
        //{
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_getAllDoc").Tables[0];
        //}

        //public DataTable getElementTypes()
        //{
        //    DataTable dt = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_SP_getElementTypes").Tables[0];
        //    return dt;
        //}

        //public DataTable getDocElementsByDocID(int Doc_Id)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Doc_ID",SqlDbType.Int)
        //    };
        //    obj[0].Value = Doc_Id;
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_SP_getDocumentElements", obj).Tables[0];

        //}

        //public DataTable GetAllFolder()
        //{
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_SP_getAllFolder").Tables[0];
        //}

        //public DataTable GetAllAttribute()
        //{
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_SP_getAllAttribute").Tables[0];
        //}

        //public bool CheckOut(int CheckOutBy, int Doc_ID, int DocVersion, string Remark)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        //new SqlParameter("@Operation_Type",SqlDbType.Int),
        //        new SqlParameter("@User_ID",SqlDbType.Int),
        //        new SqlParameter("@Doc_Id",SqlDbType.Int),
        //        new SqlParameter("@Version",SqlDbType.Int),
        //        new SqlParameter("@Remark",SqlDbType.VarChar,500)
        //    };

        //    //obj[0].Value = docObj.CurrentStatus;
        //    obj[0].Value = CheckOutBy;
        //    obj[1].Value = Doc_ID;
        //    obj[2].Value = DocVersion;
        //    obj[3].Value = Remark;

        //    if (Convert.ToInt32(SqlHelper.ExecuteNonQuery(connection, "DMS_Sp_InsDMSCheckout", obj)) > 0)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //public bool CheckIn(int CheckInBy, int Doc_ID, int DocVersion, string Remark)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Doc_ID",SqlDbType.Int),
        //        new SqlParameter("@User_ID",SqlDbType.Int),
        //        new SqlParameter("@Current_Version",SqlDbType.Int),
        //        new SqlParameter("@Remark",SqlDbType.VarChar,500)
        //    };

        //    obj[0].Value = Doc_ID;
        //    obj[1].Value = CheckInBy;
        //    obj[2].Value = DocVersion;
        //    obj[3].Value = Remark;

        //    if (Convert.ToInt32(SqlHelper.ExecuteNonQuery(connection, "DMS_Sp_InsDMSCheckin", obj)) > 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public int AddNewElement(ElementProperties objEle)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Doc_ID",SqlDbType.Int),
        //        new SqlParameter("@Version",SqlDbType.Int),
        //        new SqlParameter("@Save_Version",SqlDbType.Int),
        //        new SqlParameter("@ElementType_ID",SqlDbType.Int),
        //        new SqlParameter("@Parent_ID",SqlDbType.Int),
        //        new SqlParameter("@Created_By",SqlDbType.Int),
        //        new SqlParameter("@return",SqlDbType.Int)
        //    };

        //    obj[0].Value = objEle.DocID;
        //    obj[1].Value = objEle.version;
        //    obj[2].Value = objEle.SaveVersion;
        //    obj[3].Value = objEle.ElementTypeID;
        //    obj[4].Value = objEle.ParentID;
        //    obj[5].Value = objEle.CreatedBy;
        //    obj[6].Direction = ParameterDirection.ReturnValue;

        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Sp_AddNewElement", obj);
        //    return Convert.ToInt32(obj[6].Value);

        //}

        //public int DeleteElement(int DocumentElementID, int DeletedBy)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@ID",SqlDbType.Int),
        //        new SqlParameter("@Deleted_By",SqlDbType.Int) 
        //    };

        //    obj[0].Value = DocumentElementID;
        //    obj[1].Value = DeletedBy;

        //    return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Sp_DeleteElement", obj);

        //}

        //public int AddElementTextContaint(int DocId, int CurrentVersion, int SaveVersion, int DocElementID, string TextContent, int CreatedBy)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Doc_ID",SqlDbType.Int),
        //        new SqlParameter("@Version",SqlDbType.Int),
        //        new SqlParameter("@Save_Version",SqlDbType.Int),
        //        new SqlParameter("@Doc_Element_ID",SqlDbType.Int),
        //        new SqlParameter("@TextContent",SqlDbType.NVarChar,1000),
        //        new SqlParameter("@Created_By",SqlDbType.Int),
        //        new SqlParameter("@return",SqlDbType.Int)
        //    };

        //    obj[0].Value = DocId;
        //    obj[1].Value = CurrentVersion;
        //    obj[2].Value = SaveVersion;
        //    obj[3].Value = DocElementID;
        //    obj[4].Value = TextContent;
        //    obj[5].Value = CreatedBy;
        //    obj[6].Direction = ParameterDirection.ReturnValue;

        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Sp_AddElement_TextContent", obj);
        //    return Convert.ToInt32(obj[6].Value);

        //}

        //public int UpdateElementTextContaint(int DocID, int DocElementID, string TextContent, int ModifiedBy)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Doc_ID",SqlDbType.Int),
        //        new SqlParameter("@Doc_Element_ID",SqlDbType.Int),
        //        new SqlParameter("@TextContent",SqlDbType.NVarChar,1000),
        //        new SqlParameter("@Modified_By",SqlDbType.Int),
        //        new SqlParameter("@return",SqlDbType.Int)
        //    };

        //    obj[0].Value = DocID;
        //    obj[1].Value = DocElementID;
        //    obj[2].Value = TextContent;
        //    obj[3].Value = ModifiedBy;
        //    obj[4].Direction = ParameterDirection.ReturnValue;

        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Sp_UpdateElement_TextContent", obj);
        //    return Convert.ToInt32(obj[4].Value);
        //}

        //public int AddNewFolder(string FolderName, int ParentFolderId)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Node_Name",SqlDbType.VarChar,50),
        //        new SqlParameter("@PID",SqlDbType.Int),
        //        new SqlParameter("@return", SqlDbType.Int)
        //    };

        //    obj[0].Value = FolderName;
        //    obj[1].Value = ParentFolderId;
        //    obj[2].Direction = ParameterDirection.ReturnValue;

        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Sp_AddFolder", obj);
        //    return Convert.ToInt32(obj[2].Value);

        //}

        //public int RenameFolder(int FolderID, string FolderName)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@ID",SqlDbType.Int),
        //        new SqlParameter("@Node_Name",SqlDbType.VarChar,50)                
        //    };

        //    obj[0].Value = FolderID;
        //    obj[1].Value = FolderName;

        //    return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Sp_RenameFolder", obj);
        //}

        //public int DeleteFolder(int Deleted_By, string FolderID)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Deleted_By",SqlDbType.Int),
        //        new SqlParameter("@FolderID",SqlDbType.VarChar,50)                
        //    };

        //    obj[0].Value = Deleted_By;
        //    obj[1].Value = FolderID;


        //    return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Sp_DeleteFolder", obj);
        //}

        //public int AddNewAttribute(AttributeProperties objAtb)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Doc_ID",SqlDbType.Int),
        //        new SqlParameter("@Version",SqlDbType.Int),
        //        new SqlParameter("@Save_Version",SqlDbType.Int),
        //        new SqlParameter("@Doc_Element_ID",SqlDbType.Int),
        //        new SqlParameter("@Attribute_ID",SqlDbType.Int),
        //        new SqlParameter("@Attribute_Value",SqlDbType.VarChar,200),
        //        new SqlParameter("@Created_By",SqlDbType.Int),
        //        new SqlParameter("@return",SqlDbType.Int)
        //    };

        //    obj[0].Value = objAtb.DocID;
        //    obj[1].Value = objAtb.version;
        //    obj[2].Value = objAtb.SaveVersion;
        //    obj[3].Value = objAtb.ElementDocID;
        //    obj[4].Value = objAtb.AttributeTypeID;
        //    obj[5].Value = objAtb.AttributeValue;
        //    obj[6].Value = objAtb.CreatedBy;
        //    obj[7].Direction = ParameterDirection.ReturnValue;

        //    SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Sp_AddAttribute", obj);
        //    //Returns attribute auto id
        //    return Convert.ToInt32(obj[7].Value);
        //}

        //public int UpdateAttributeValue(AttributeProperties objAtb)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Doc_ID",SqlDbType.Int),
        //        new SqlParameter("@Version",SqlDbType.Int),
        //        new SqlParameter("@Save_Version",SqlDbType.Int),
        //        new SqlParameter("@Doc_Element_ID",SqlDbType.Int),
        //        new SqlParameter("@Attribute_ID",SqlDbType.Int),
        //        new SqlParameter("@Attribute_Value",SqlDbType.VarChar,200),
        //        new SqlParameter("@Modified_By",SqlDbType.Int)
        //    };

        //    obj[0].Value = objAtb.DocID;
        //    obj[1].Value = objAtb.version;
        //    obj[2].Value = objAtb.SaveVersion;
        //    obj[3].Value = objAtb.ElementDocID;
        //    obj[4].Value = objAtb.AttributeTypeID;
        //    obj[5].Value = objAtb.AttributeValue;
        //    obj[6].Value = objAtb.ModifiedBy;


        //    return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Sp_UpdateAttribute", obj);
        //}

        //public DataTable getDocPath()
        //{
        //    string sSQL = "select Doc_ID as ID, Doc_Name as Node_Name, ParentFolderId as PID, '0' as Type  from DMS_Document Where Deleted_By is null order by ParentFolderId";
        //    return SqlHelper.ExecuteDataset(connection, CommandType.Text, sSQL).Tables[0];
        //}

        //public IDataReader getAttributesByDocElementId(int DocElementID)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Doc_Element_ID",SqlDbType.Int)
        //    };

        //    obj[0].Value = DocElementID;
        //    return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "DMS_getAttributesByDocElementId", obj);
        //}

        //public IDataReader getElementTextByDocElementId(int DocElementID)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Doc_Element_ID",SqlDbType.Int)
        //    };

        //    obj[0].Value = DocElementID;
        //    return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "DMS_getElementTextByDocElementId", obj);
        //}

        //public bool AddViewLog(int DocID, int DocVersion, int ViewedBy, DateTime ViewedDate)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Doc_ID",SqlDbType.Int),
        //        new SqlParameter("@Version",SqlDbType.Int),
        //        new SqlParameter("@Viewed_By", SqlDbType.Int),
        //        new SqlParameter("@Viewed_Date", SqlDbType.DateTime)
        //    };

        //    obj[0].Value = DocID;
        //    obj[1].Value = DocVersion;
        //    obj[2].Value = ViewedBy;
        //    obj[3].Value = ViewedDate;

        //    if (SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_AddViewLog", obj) > 0)
        //    {
        //        return true;
        //    }
        //    return false;

        //}

        //public DataTable getAllViewLog()
        //{
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_getAllViewLog").Tables[0];
        //}

        //public DataTable getAllViewLogByDocId(int DocID)
        //{
        //    SqlParameter[] obj = new SqlParameter[]
        //    {   
        //        new SqlParameter("@Doc_ID",SqlDbType.Int)
        //    };

        //    obj[0].Value = DocID;
        //    return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_getAllViewLogByDocId", obj).Tables[0];
        //}
        //#endregion

        # region Document User

        public bool ChangePassword(int UserId, string OldPassword, string NewPassword)
        {
            SqlParameter[] obj = new SqlParameter[]
            {   
                new SqlParameter("@User_ID",SqlDbType.Int),
                new SqlParameter("@Old_Password",SqlDbType.NVarChar,50),
                new SqlParameter("@New_Password", SqlDbType.NVarChar,50)
            };

            obj[0].Value = UserId;
            obj[1].Value = OldPassword;
            obj[2].Value = NewPassword;

            if (SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_ChangePassword", obj) > 0)
            {
                return true;
            }
            return false;

        }

        public int AddNewUser(UserProperties objUser)
        {
            SqlParameter[] obj = new SqlParameter[]
            {   
                new SqlParameter("@User_Name",SqlDbType.VarChar,50),
                new SqlParameter("@Password",SqlDbType.VarChar,50),
                new SqlParameter("@First_Name",SqlDbType.VarChar,50),
                new SqlParameter("@Last_Name",SqlDbType.VarChar,50),
                new SqlParameter("@Address",SqlDbType.VarChar,500),
                new SqlParameter("@Email",SqlDbType.VarChar,250),
                new SqlParameter("@Phone",SqlDbType.VarChar,50),
                new SqlParameter("@Created_By",SqlDbType.Int),
                new SqlParameter("@return",SqlDbType.Int)
            };

            obj[0].Value = objUser.UserName;
            obj[1].Value = objUser.Password;
            obj[2].Value = objUser.First_Name;
            obj[3].Value = objUser.Last_Name;
            obj[4].Value = objUser.PresentAddress;
            obj[5].Value = objUser.Email;
            obj[6].Value = objUser.Phone;
            obj[7].Value = objUser.Created_By;
            obj[8].Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_AddUser", obj);
            return Convert.ToInt32(obj[8].Value);

        }

        public int UpdateUser(UserProperties objUser)
        {
            SqlParameter[] obj = new SqlParameter[]
            {   
                new SqlParameter("@User_ID",SqlDbType.Int),
                new SqlParameter("@Password",SqlDbType.VarChar,50),
                new SqlParameter("@First_Name",SqlDbType.VarChar,50),
                new SqlParameter("@Last_Name",SqlDbType.VarChar,50),
                new SqlParameter("@Address",SqlDbType.VarChar,500),
                new SqlParameter("@Email",SqlDbType.VarChar,250),
                new SqlParameter("@Phone",SqlDbType.VarChar,50),
                new SqlParameter("@Modified_By",SqlDbType.Int)
            };

            obj[0].Value = objUser.UserId;
            obj[1].Value = objUser.Password;
            obj[2].Value = objUser.First_Name;
            obj[3].Value = objUser.Last_Name;
            obj[4].Value = objUser.PresentAddress;
            obj[5].Value = objUser.Email;
            obj[6].Value = objUser.Phone;
            obj[7].Value = objUser.Modified_By;

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_UpdateUser", obj);
        }

        public int DeleteUser(UserProperties objUser)
        {
            SqlParameter[] obj = new SqlParameter[]
            {   
                new SqlParameter("@User_ID",SqlDbType.Int),
                new SqlParameter("@Deleted_By",SqlDbType.Int)
            };

            obj[0].Value = objUser.UserId;
            obj[1].Value = objUser.Deleted_By;

            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_DeleteUser", obj);

        }

        public bool CheckUserNameExist(string UserName)
        {
            SqlParameter[] obj = new SqlParameter[] 
            { 
                new SqlParameter("@User_Name",SqlDbType.VarChar,50)
            };

            obj[0].Value = UserName;

            if (SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "DMS_CheckUserNameExist", obj).Read())
            {
                return true;
            }
            return false;
        }

        public IDataReader getUSerDetailByID(int User_ID)
        {
            SqlParameter[] obj = new SqlParameter[]
            {   
                new SqlParameter("@User_ID",SqlDbType.Int)
            };
            obj[0].Value = User_ID;
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "DMS_getUserDetailsByID", obj);
        }

        public IDataReader getUSerDetailByUserName(string UserName)
        {
            SqlParameter[] obj = new SqlParameter[]
            {   
                new SqlParameter("@User_Name",SqlDbType.NVarChar,50)
            };
            obj[0].Value = UserName;
            return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, "DMS_getUserDetailsUserName", obj);
        }

        # endregion
    }
}
