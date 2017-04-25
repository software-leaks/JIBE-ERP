using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Xml;

using SMS.Data.DMS;


using System.Configuration;
using SMS.Properties;


namespace SMS.Business.DMS
{
    public class BLL_DMS_Document
    {

        DAL_DMS_Document objDBDoc = new DAL_DMS_Document();
        
        # region GET - DOCUMENT - LIST

        /// <summary>
        /// It returns all documents list with documentId, document name, create by and parent folder id.
        /// </summary>
        /// <returns>List</returns>
        public List<DocumentPoperties> GetActiveDocuments()
        {
            List<DocumentPoperties> DocList = new List<DocumentPoperties>();

            IDataReader reader = objDBDoc.GetActiveDocuments();
            try
            {
                while (reader.Read())
                {
                    DocumentPoperties objDoc = new DocumentPoperties();
                    FillDocumentProperties(objDoc, reader);
                    DocList.Add(objDoc);
                }
            }
            finally
            {
                reader.Close();
            }
            return DocList;
        }

        /// <summary>
        /// It returns all documents list with documentId, name, create by and parent folder id related to document name.
        /// </summary>
        /// <param name="Doc_Name">It is documnet name</param>
        /// <returns>List</returns>
        public List<DocumentPoperties> getDocumentsBYDocName(string Doc_Name)
        {
            List<DocumentPoperties> DocList = new List<DocumentPoperties>();

            IDataReader reader = objDBDoc.GetDocumentsByDocName(Doc_Name);
            try
            {
                while (reader.Read())
                {
                    DocumentPoperties objDoc = new DocumentPoperties();
                    FillDocumentProperties(objDoc, reader);
                    DocList.Add(objDoc);
                }
            }
            finally
            {
                reader.Close();
            }
            return DocList;
        }

        /// <summary>
        /// It returns all documents list with documentId, name, create by and parent folder id
        /// having size between sizeFrom and sizeto document size.
        /// </summary>
        /// <param name="sizeFrom">Minimum size of document</param>
        /// <param name="sizeTo">maximum size of document</param>
        /// <returns>list</returns>
        public List<DocumentPoperties> GetDocumentsBySize(double SizeFrom, double SizeTo)
        {
            List<DocumentPoperties> DocList = new List<DocumentPoperties>();

            IDataReader reader = objDBDoc.GetDocumentsBySize(SizeFrom, SizeTo);
            try
            {
                while (reader.Read())
                {
                    DocumentPoperties objDoc = new DocumentPoperties();
                    FillDocumentProperties(objDoc, reader);
                    DocList.Add(objDoc);
                }
            }
            finally
            {
                reader.Close();
            }
            return DocList;
        }

        /// <summary>
        /// It returns all documents list with documentId, name, create by and parent folder id
        /// having create date between startDate and endDate document date.
        /// </summary>
        /// <param name="startDate">It is create date form </param>
        /// <param name="endDate">It is create date to</param>
        /// <returns>List</returns>
        public List<DocumentPoperties> GetDocumentsByCreateDate(DateTime startDate, DateTime endDate)
        {
            List<DocumentPoperties> DocList = new List<DocumentPoperties>();

            IDataReader reader = objDBDoc.GetDocumentsByCreateDate(startDate, endDate);
            try
            {
                while (reader.Read())
                {
                    DocumentPoperties objDoc = new DocumentPoperties();
                    FillDocumentProperties(objDoc, reader);
                    DocList.Add(objDoc);
                }
            }
            finally
            {
                reader.Close();
            }
            return DocList;
        }

        ///// <summary>
        ///// It returns all documents list with documentId, name, create by and parent folder id
        ///// related to part of elelment text content and perent folder id.
        ///// </summary>
        ///// <param name="textContent">It is part of element text content</param>
        ///// <param name="ParentFolderId">Parent folder id</param>
        ///// <returns>List</returns>
        //public List<DocumentPoperties> getDocumentsByElementTextParentFolderID(string textContent, int ParentFolderId)
        //{
        //    List<DocumentPoperties> docList = new List<DocumentPoperties>();

        //    IDataReader reader = objDBDoc.getDocumentsByElementTextParentFolderID(textContent, ParentFolderId);
        //    try
        //    {
        //        while (reader.Read())
        //        {
        //            DocumentPoperties objDBDocCheck = new DocumentPoperties();
        //            objDBDocCheck.DocID = DataHelper.GetInt(reader, "Doc_ID");
        //            objDBDocCheck.DocName = DataHelper.GetString(reader, "Doc_Name");
        //            objDBDocCheck.CreatedBy = DataHelper.GetInt(reader, "Created_By");
        //            objDBDocCheck.ParentFolderId = DataHelper.GetInt(reader, "ParentFolderID");
        //            docList.Add(objDBDocCheck);
        //        }
        //    }
        //    finally
        //    {
        //        reader.Close();
        //    }
        //    return docList;
        //}

        /// <summary>
        /// It returns all documents list with documentId, name, create by and parent folder id
        /// related perent folder id.
        /// </summary>
        /// <param name="ParentFolderId">It is parent folder id</param>
        /// <returns>List</returns>
        public List<DocumentPoperties> getDocumentsByParentFolderID(int ParentFolderId)
        {
            List<DocumentPoperties> DocList = new List<DocumentPoperties>();

            IDataReader reader = objDBDoc.GetDocumentsByParentFolderID(ParentFolderId);
            try
            {
                while (reader.Read())
                {
                    DocumentPoperties objDoc = new DocumentPoperties();
                    FillDocumentProperties(objDoc, reader);
                    DocList.Add(objDoc);
                }
            }
            finally
            {
                reader.Close();
            }
            return DocList;
        }

        /// <summary>
        /// It returns all documents list with documentId, name, create by and parent folder id
        /// related to part of elelment text content.
        /// </summary>
        /// <param name="textContent">It is part of element text content</param>
        /// <returns>List</returns>
        public List<DocumentPoperties> getDocumentsByContent(string textContent)
        {
            List<DocumentPoperties> DocList = new List<DocumentPoperties>();

            IDataReader reader = objDBDoc.GetDocumentsByContent(textContent);
            try
            {
                while (reader.Read())
                {
                    DocumentPoperties objDoc = new DocumentPoperties();
                    FillDocumentProperties(objDoc, reader);
                    DocList.Add(objDoc);
                }
            }
            finally
            {
                reader.Close();
            }
            return DocList;
        }

        #endregion

        private void FillDocumentProperties(DocumentPoperties objDoc, IDataReader reader)
        {
            objDoc.DocID = DataHelper.GetInt(reader, "DocID");
            objDoc.CurrentVersion = DataHelper.GetInt(reader, "Version");
            objDoc.DocNo = DataHelper.GetString(reader, "DocNo");
            objDoc.DocName = DataHelper.GetString(reader, "DocName");
            objDoc.DocFileName = DataHelper.GetString(reader, "DocFileName");
            objDoc.DocFilePath = "Uploades\\CrewDocuments\\" + DataHelper.GetString(reader, "DocFileName");
            objDoc.DocTypeID = DataHelper.GetInt(reader, "DocTypeID");
            objDoc.DocTypeName = DataHelper.GetString(reader, "DocTypeName");
            objDoc.DocHeader = DataHelper.GetString(reader, "DocHeader");
            objDoc.DateOfIssue = DataHelper.GetDateTime(reader, "DateOfIssue");
            objDoc.PlaceOfIssue = DataHelper.GetString(reader, "PlaceOfIssue");
            objDoc.CountryOfIssue = DataHelper.GetString(reader, "Country");
            //objDoc.IssuingAuthority = DataHelper.GetString(reader, "IssuingAuthority");
            objDoc.DateOfExpiry = DataHelper.GetDateTime(reader, "DateOfExpiry");
            objDoc.ApproveStatus = DataHelper.GetInt(reader, "ApproveStatus");
            objDoc.ApprovedBy = DataHelper.GetInt(reader, "ApprovedBy");
            //objDoc.ApprovedUserName = DataHelper.GetString(reader, "ApprovedBy");
            //objDoc.SizeByte = DataHelper.GetDouble(reader, "sizebyte");
            objDoc.CheckOutBy = DataHelper.GetInt(reader, "CheckedOutBy");
            objDoc.CheckOutDate = DataHelper.GetDateTime(reader, "CheckOutDate");
            objDoc.ParentFolderId = DataHelper.GetInt(reader, "ParentFolderID");
            objDoc.CreatedBy = DataHelper.GetInt(reader, "Created_By");
            objDoc.DateOfCreation = DataHelper.GetDateTime(reader, "Date_Of_Creation");

        }

        public DataTable GetDocumentTypeList()
        {
            try
            {
                return objDBDoc.GetDocumentTypeList();
            }
            catch
            { return null; }
            
        }
        public DataTable GetDocumentTypeList(int CheckList)
        {
            try
            {
                return objDBDoc.GetDocumentTypeList(CheckList);
            }
            catch
            { return null; }
        }
        public DataTable GetDocumentTypeList(int CheckList, int Voyage)
        {
            try
            {
                return objDBDoc.GetDocumentTypeList( CheckList,  Voyage);
            }
            catch
            { return null; }
        }
        # region GET - DOCUMENT - DETAILS
        
        /// <summary>
        /// It gives document details by document id like Doc_ID, Doc_Name, Doc_Header, Version, ParentFolderID, 
        /// Approve_Status, Created_By, Date_Of_Creation, Deleted_By and size
        /// </summary>
        /// <param name="Doc_ID">This is document ID</param>
        /// <returns>DocumentPoperties</returns>
        public DocumentPoperties GetDocumentDetailsByID(int Doc_ID)
        {
            try
            {
                DocumentPoperties objDoc = new DocumentPoperties();

                IDataReader reader = objDBDoc.GetDocumentDetailByDocID(Doc_ID);
                if (reader.Read())
                {
                    FillDocumentProperties(objDoc, reader);
                }
                return objDoc;
            }
            catch
            { }
            return null;
        }

        public  string getDocumentFileNameByID(int DocID)
        {
            string sFileName = "";
            try
            {
                IDataReader reader = objDBDoc.Get_DocumentFileNameByDocID_DL(DocID);
                if (reader.Read())
                {
                    sFileName = reader["DocFileName"].ToString();
                }
            }
            catch
            { }
            return sFileName;
        }
        #endregion
        
        # region -- CREW DOCUMENT FUNCTIONS --

        public int AddNewDocument(int CrewID, string DocumentName, string DocFileName, int SizeByte, int DocTypeID, int Created_By)
        {
            try
            {
                return objDBDoc. AddNewDocument_DL(CrewID, DocumentName, DocFileName, SizeByte,DocTypeID, Created_By);
            }
            catch
            {
                throw;
            }
        }

        public int UpdateAttributeValue(int CrewID, int DocID, int AttributeID, int AttributeValue, int ModifiedBy)
        {
            try
            {
                return objDBDoc.UpdateAttributeValue_DL(CrewID, DocID, AttributeID, AttributeValue, ModifiedBy);
            }
            catch
            { return 0; }
        }
        public int UpdateAttributeValue(int CrewID, int DocID, int AttributeID, DateTime AttributeValue, int ModifiedBy)
        {
            try
            {
                return objDBDoc.UpdateAttributeValue_DL(CrewID, DocID, AttributeID, AttributeValue, ModifiedBy);
            }
            catch
            { return 0; }
        }
        public int UpdateAttributeValue(int CrewID, int DocID, int AttributeID, string AttributeValue, int ModifiedBy)
        {
            try
            {
                return objDBDoc.UpdateAttributeValue_DL(CrewID, DocID, AttributeID, AttributeValue, ModifiedBy);
            }
            catch
            { return 0; }
        }

        #endregion
        
        //# region OTHERS - NOT IN USE


        ///// <summary>
        ///// It gives document details by document name and parent folder id like Doc_ID, Doc_Name, 
        ///// Created_By, ParentFolderId
        ///// </summary>
        ///// <param name="Doc_Name">It is document name</param>
        ///// <param name="parentFolderId">It id parent folder id</param>
        ///// <returns>DocumentPoperties</returns>
        //public DocumentPoperties getDocumentBYDocName(string Doc_Name)
        //{
        //    try
        //    {
        //        DocumentPoperties objDoc = new DocumentPoperties();

        //        IDataReader reader = objDBDoc.GetDocumentsByDocName(Doc_Name);
        //        if (reader.Read())
        //        {
        //            objDoc.DocID = DataHelper.GetInt(reader, "Doc_ID");
        //            objDoc.DocName = DataHelper.GetString(reader, "Doc_Name");
        //            objDoc.ParentFolderId = DataHelper.GetInt(reader, "ParentFolderID");
        //            objDoc.CreatedBy = DataHelper.GetInt(reader, "Created_By");
        //        }
        //        return objDoc;
        //    }
        //    catch
        //    { }
        //    return null;
        //}

        ///// <summary>
        ///// It gives all element id and element types
        ///// </summary>
        ///// <returns>Datatable</returns>
        //public DataTable getElementTypes()
        //{
        //    try
        //    {

        //        return objDBDoc.getElementTypes();
        //    }
        //    catch
        //    { }
        //    return null;
        //}

        ///// <summary>
        ///// It gives all document elements and id by document name
        ///// </summary>
        ///// <param name="Doc_Id">It is document id</param>
        ///// <returns>Datatable</returns>
        //public DataTable getDocElementsByDocID(int Doc_Id)
        //{
        //    try
        //    {

        //        return objDBDoc.getDocElementsByDocID(Doc_Id);
        //    }
        //    catch
        //    { }
        //    return null;
        //}

        ///// <summary>
        ///// It gives all folder id, folder name and parent folder id
        ///// </summary>
        ///// <returns>Datatable</returns>
        //public DataTable GetAllFolder()
        //{
        //    try
        //    {

        //        return objDBDoc.GetAllFolder();
        //    }
        //    catch
        //    { }
        //    return null;
        //}

        ///// <summary>
        ///// It gives all document details like Doc_ID, Doc_Name, Created_By, ParentFolderId
        ///// </summary>
        ///// <returns>Datatable</returns>
        //public DataTable GetAllDoc()
        //{
        //    try
        //    {

        //        return objDBDoc.GetAllDoc();
        //    }
        //    catch
        //    { }
        //    return null;
        //}

        ///// <summary>
        ///// It gives all attributes with id
        ///// </summary>
        ///// <returns>Datatable</returns>
        //public DataTable GetAllAttribute()
        //{
        //    try
        //    {

        //        return objDBDoc.GetAllAttribute();
        //    }
        //    catch
        //    { }
        //    return null;
        //}

        ///// <summary>
        ///// It gives current save version of document. (-1 for falied to get save version)
        ///// </summary>
        ///// <param name="Doc_ID">It is document id</param>
        ///// <returns>Save Version</returns>
        //public int getSaveVersion(int Doc_ID)
        //{
        //    try
        //    {

        //        int SaveVersion = objDBDoc.getSaveVersion(Doc_ID);
        //        return SaveVersion;
        //    }
        //    catch
        //    { }
        //    return -1;
        //}

        ///// <summary>
        ///// It returns element type id, if element element exists with respects to Doc_ID, version, save_Version and ElementType_Id.
        ///// If 0 returns then element type id not found, if -1 returns then unable to find element type id
        ///// </summary>
        ///// <param name="Doc_ID">Document id</param>
        ///// <param name="version">Version of document</param>
        ///// <param name="save_Version">Save version of document</param>
        ///// <param name="ElementType_Id"></param>
        ///// <returns>Element Type Id</returns>
        //public int getElementTypeId(int Doc_ID, int version, int save_Version, int ElementType_Id)
        //{
        //    try
        //    {

        //        return objDBDoc.getElementTypeId(Doc_ID, version, save_Version, ElementType_Id);
        //    }
        //    catch
        //    { }
        //    return -1;
        //}

        //public DataRow getDocumentStatus(int Doc_ID)
        //{
        //    try
        //    {
        //        return objDBDoc.getDocumentStatus(Doc_ID);
        //    }
        //    catch
        //    { }
        //    return null;
        //}


        ///// <summary>
        ///// It adds record in DMS_Operation table as document is checked out
        ///// </summary>
        ///// <param name="documentObj">It contails check type, checkout by, document id, version</param>
        ///// <param name="Remark">It is checkout remark</param>
        ///// <returns>bool</returns>
        //public bool CheckOut(int CheckOutBy, int Doc_ID, int DocVersion, string Remark)
        //{
        //    try
        //    {

        //        return objDBDoc.CheckOut(CheckOutBy, Doc_ID, DocVersion, Remark);
        //    }
        //    catch
        //    { }
        //    return false;
        //}

        ///// <summary>
        ///// Update version in DMS_Document table
        ///// It adds record in DMS_Operation table as document is checked in
        ///// Add old version entry in version table
        ///// </summary>
        ///// <param name="documentObj">It contains document id, checkin by, version</param>
        ///// <param name="Remark">It is check in remark</param>
        ///// <returns>bool</returns>
        //public bool CheckIn(int CheckInBy, int Doc_ID, int DocVersion, string Remark)
        //{
        //    try
        //    {

        //        return objDBDoc.CheckIn(CheckInBy, Doc_ID, DocVersion, Remark);
        //    }
        //    catch
        //    { }
        //    return false;
        //}

        ///// <summary>
        ///// It add new document in DMS_Document
        ///// Add 1 entry in DMS_Document_Elements for document element add
        ///// </summary>
        ///// <param name="documentObj">It contains document name, header, parent folder id and create by</param>
        ///// <param name="ParentFolderId">Parent folder id</param>
        ///// <returns>int document id</returns>
        //public int AddNewDoc(DocumentPoperties documentObj)
        //{
        //    try
        //    {

        //        return objDBDoc.AddNewDoc(documentObj);
        //    }
        //    catch
        //    { }
        //    return 0;
        //}

        ///// <summary>
        ///// Add new elemnt in DMS_Document_Elements table
        ///// </summary>
        ///// <param name="ElementObj">It contains documents id, Element Type ID, version, save version, parent element id, created by</param>
        ///// <returns>Element ID</returns>
        //public int AddNewElement(ElementProperties objEle)
        //{
        //    try
        //    {

        //        return objDBDoc.AddNewElement(objEle);
        //    }
        //    catch
        //    { }
        //    return 0;
        //}

        ///// <summary>
        ///// It delete the elemnt added in document
        ///// </summary>
        ///// <param name="DocumentElementID">It is document element id</param>
        ///// <param name="DeletedBy">It is user id</param>
        ///// <returns>Number of rows affected</returns>
        //public int DeleteElement(int DocumentElementID, int DeletedBy)
        //{
        //    try
        //    {

        //        return objDBDoc.DeleteElement(DocumentElementID, DeletedBy);
        //    }
        //    catch
        //    { }
        //    return 0;
        //}

        ///// <summary>
        ///// It add the element text to DMS_Element_TextContent table
        ///// </summary>
        ///// <param name="documentObj">It contain document id, current version</param>
        ///// <param name="SaveVersion">Save version of document</param>
        ///// <param name="DocElementID">Element id of docuemnt</param>
        ///// <param name="TextContent">Element Text</param>
        ///// <param name="CreatedBy">User Id</param>
        ///// <returns>No of rows affected</returns>
        //public int AddElementTextContaint(int DocId, int CurrentVersion, int SaveVersion, int DocElementID, string TextContent, int CreatedBy)
        //{
        //    try
        //    {

        //        return objDBDoc.AddElementTextContaint(DocId, CurrentVersion, SaveVersion, DocElementID, TextContent, CreatedBy);
        //    }
        //    catch
        //    { }
        //    return 0;
        //}

        ///// <summary>
        ///// It update the element text to DMS_Element_TextContent table
        ///// </summary>
        ///// <param name="documentObj">It contains document id</param>
        ///// <param name="DocElementID">Docuemnt Element Id</param>
        ///// <param name="TextContent">Element text containt</param>
        ///// <param name="ModifiedBy">user id</param>
        ///// <returns>No of rows affected</returns>
        //public int UpdateElementTextContaint(int DocID, int DocElementID, string TextContent, int ModifiedBy)
        //{
        //    try
        //    {

        //        return objDBDoc.UpdateElementTextContaint(DocID, DocElementID, TextContent, ModifiedBy);
        //    }
        //    catch
        //    { }
        //    return 0;
        //}

        ///// <summary>
        ///// It gives the document element type Id related to auto document element ID and document id
        ///// It returns -1 if unable to find document element type id
        ///// </summary>
        ///// <param name="ID">Document element auto id</param>
        ///// <param name="Doc_ID">Document id</param>
        ///// <returns>return element type id</returns>
        //public int getElementTypeId(int ID, int Doc_ID)
        //{
        //    try
        //    {

        //        return objDBDoc.getElementTypeId(ID, Doc_ID);
        //    }
        //    catch
        //    { }
        //    return -1;
        //}

        ///// <summary>
        ///// It add new folder in DMS_Document_Folder_Path table.
        ///// if there no folder in DMS_Document_Folder_Path then default first folder is save 0 parent folder id.
        ///// </summary>
        ///// <param name="FolderName">Folder name</param>
        ///// <param name="ParentFolderId">It is parent folder id</param>
        ///// <returns>folder id</returns>
        //public int AddNewFolder(string FolderName, int ParentFolderId)
        //{
        //    try
        //    {

        //        return objDBDoc.AddNewFolder(FolderName, ParentFolderId);
        //    }
        //    catch
        //    { }
        //    return 0;
        //}

        ///// <summary>
        ///// It checks the folder name exists related to parent folder
        ///// </summary>
        ///// <param name="ParentFolderID">Parent folder id</param>
        ///// <param name="FolderName">Folder name</param>
        ///// <returns>bool</returns>
        //public bool checkFolderNameExist(int ParentFolderID, string FolderName)
        //{
        //    try
        //    {

        //        return objDBDoc.checkFolderNameExist(ParentFolderID, FolderName);
        //    }
        //    catch
        //    { }
        //    return true;
        //}

        ///// <summary>
        ///// It is rename the folder
        ///// </summary>
        ///// <param name="FolderID">Folder id</param>
        ///// <param name="FolderName">New folder name</param>
        ///// <returns>No of rows affected</returns>
        //public int RenameFolder(int FolderID, string FolderName)
        //{
        //    try
        //    {

        //        return objDBDoc.RenameFolder(FolderID, FolderName);
        //    }
        //    catch
        //    { }
        //    return 0;
        //}

        ///// <summary>
        ///// It delete folder sub folder and documents of related folder.
        ///// Here we have to pass multiple folder id in string separated by quama.
        ///// It is permanently delete the folder from DMS_Document_Folder_Path and enter deleted by and date of deletion
        ///// in DMS_Document table.
        ///// </summary>
        ///// <param name="Deleted_By">User Id</param>
        ///// <param name="FolderID">Folder which we have to delete</param>
        ///// <returns>No of rows affected</returns>
        //public int DeleteFolder(int Deleted_By, string FolderID)
        //{
        //    try
        //    {

        //        return objDBDoc.DeleteFolder(Deleted_By, FolderID);
        //    }
        //    catch
        //    { }
        //    return 0;
        //}

        ///// <summary>
        ///// It add the attribue and its value to related document element id
        ///// </summary>
        ///// <param name="ElementObj">It contains DocID, CurrentVersion, SaveVersion, DocElementID, AttributeID, AttributeValue, CreatedBy </param>
        ///// <returns>No of rows affected</returns>
        //public int AddNewAttribute(AttributeProperties objAtb)
        //{
        //    try
        //    {

        //        return objDBDoc.AddNewAttribute(objAtb);
        //    }
        //    catch
        //    { }
        //    return 0;
        //}

        ///// <summary>
        ///// It update attribute value related to document element id
        ///// </summary>
        ///// <param name="AttributeObj">It contains DocID, CurrentVersion, SaveVersion, DocElementID, AttributeID, AttributeValue, CreatedBy</param>
        ///// <returns>No of rows affected</returns>
        //public int UpdateAttributeValue(AttributeProperties objAtb)
        //{
        //    try
        //    {

        //        return objDBDoc.UpdateAttributeValue(objAtb);
        //    }
        //    catch
        //    { }
        //    return 0;
        //}

        ///// <summary>
        ///// It checks attribute exist or not to releated document and element id
        ///// </summary>
        ///// <param name="AttributeObj">It containts DocID, version, SaveVersion, ElementDocID, AttributeTypeID</param>
        ///// <returns>bool</returns>
        ////public bool CheckAttributeExist(int DocID, int CurrentVersion, int SaveVersion, int DocElementID, int AttributeID)
        //public bool CheckAttributeExist(AttributeProperties objAtb)
        //{
        //    try
        //    {
        //        return objDBDoc.CheckAttributeExist(objAtb);
        //    }
        //    catch
        //    { }
        //    return true;
        //}

        ///// <summary>
        ///// It check element exist or not to related document id
        ///// </summary>
        ///// <param name="DocID">It is document id</param>
        ///// <param name="DocElementID">It is document element id</param>
        ///// <returns>bool</returns>
        //public bool CheckElementExist(int DocID, int DocElementID)
        //{
        //    try
        //    {

        //        return objDBDoc.CheckElementExist(DocID, DocElementID);
        //    }
        //    catch
        //    { }
        //    return true;
        //}

        ///// <summary>
        ///// It return folder count. -1 for unable to get folder count
        ///// </summary>
        ///// <returns>int</returns>
        //public int GetFolderCount()
        //{
        //    try
        //    {

        //        return objDBDoc.GetFolderCount();
        //    }
        //    catch
        //    { }
        //    return -1;
        //}

        ///// <summary>
        ///// It returns all documents with document id, document name and parent folder id
        ///// </summary>
        ///// <returns>DataTable</returns>
        //public DataTable getDocPath()
        //{
        //    try
        //    {

        //        return objDBDoc.getDocPath();
        //    }
        //    catch
        //    { }
        //    return null;
        //}

        ///// <summary>
        ///// It Returns attribute_ID, attribute_Name, attribute_Value by document element id
        ///// </summary>
        ///// <param name="DocElementID">It is document element ID</param>
        ///// <returns>List</returns>
        //public List<AttributeProperties> getAttributesByDocElementId(int DocElementID)
        //{
        //    List<AttributeProperties> atbList = new List<AttributeProperties>();

        //    IDataReader reader = objDBDoc.getAttributesByDocElementId(DocElementID);

        //    try
        //    {

        //        while (reader.Read())
        //        {
        //            AttributeProperties objAtb = new AttributeProperties();
        //            objAtb.AttributeID = DataHelper.GetInt(reader, "Attribute_ID");
        //            objAtb.AttributeName = DataHelper.GetString(reader, "Attribute_Name");
        //            objAtb.AttributeValue = DataHelper.GetString(reader, "Attribute_Value");
        //            atbList.Add(objAtb);
        //        }
        //        return atbList;
        //    }
        //    catch
        //    { }
        //    finally
        //    {
        //        reader.Close();
        //    }
        //    return null;

        //}

        ///// <summary>
        ///// It returns element text by document elementID
        ///// </summary>
        ///// <param name="DocElementID">It is document element ID</param>
        ///// <returns>String</returns>
        //public string getElementTextByDocElementId(int DocElementID)
        //{
        //    try
        //    {

        //        IDataReader reader = objDBDoc.getElementTextByDocElementId(DocElementID);
        //        if (reader.Read())
        //        {
        //            reader["TextContent"].ToString();
        //        }
        //    }
        //    catch
        //    { }
        //    return "";
        //}

        ///// <summary>
        ///// It returns Doc_ID, Doc_name, version, created by, created date, remark of all documents
        ///// </summary>
        ///// <returns>Datatable</returns>
        //public DataTable getAllDocumentVersion()
        //{
        //    try
        //    {

        //        return objDBDoc.getAllDocumentVersion();
        //    }
        //    catch
        //    { }
        //    return null;
        //}

        ///// <summary>
        ///// It returns Doc_ID, Doc_name, version, created by, created date, remark by document ID
        ///// </summary>
        ///// <param name="DocID">It is document id</param>
        ///// <returns>DataTable</returns>
        //public DataTable getDocumentVersionByDocID(int DocID)
        //{
        //    try
        //    {

        //        return objDBDoc.getDocumentVersionByDocID(DocID);
        //    }
        //    catch
        //    { }
        //    return null;
        //}

        ///// <summary>
        ///// It stores details of document view by user
        ///// </summary>
        ///// <param name="DocID">It is document ID</param>
        ///// <param name="DocVersion">Document version</param>
        ///// <param name="ViewedBy">Document view by</param>
        ///// <param name="ViewedDate">Document view date</param>
        ///// <returns>Bool</returns>
        //public bool AddViewLog(int DocID, int DocVersion, int ViewedBy, DateTime ViewedDate)
        //{

        //    return objDBDoc.AddViewLog(DocID, DocVersion, ViewedBy, ViewedDate);
        //}

        ///// <summary>
        ///// It returns all view log of document
        ///// </summary>
        ///// <returns>Datable</returns>
        //public DataTable getAllViewLog()
        //{
        //    try
        //    {

        //        return objDBDoc.getAllViewLog();
        //    }
        //    catch
        //    { }
        //    return null;
        //}

        ///// <summary>
        ///// It returns document ID Wise view log
        ///// </summary>
        ///// <param name="DocID">It is document id</param>
        ///// <returns>Datatable</returns>
        //public DataTable getAllViewLogByDocId(int DocID)
        //{
        //    try
        //    {

        //        return objDBDoc.getAllViewLogByDocId(DocID);
        //    }
        //    catch
        //    { }
        //    return null;
        //}

        ///// <summary>
        ///// It returns last date of document view by user
        ///// </summary>
        ///// <param name="DocID">It is document id</param>
        ///// <param name="UserId">Viewed by</param>
        ///// <returns>Date</returns>
        //public IDataReader GetDocLastViewDate(DateTime StartDate, DateTime EndDate)
        //{
        //    return objDBDoc.GetDocumentsByLastViewDate(StartDate, EndDate);
        //}

        ///// <summary>
        ///// It add user details in DMS_User
        ///// </summary>
        ///// <param name="objUser">Object of user properties</param>
        ///// <returns>User ID</returns>
        //public int AddNewUser(UserProperties objUser)
        //{

        //    return objDBDoc.AddNewUser(objUser);
        //}

        ///// <summary>
        ///// It updates the details by user id
        ///// </summary>
        ///// <param name="objUser">Object of user properties</param>
        ///// <returns>No of rows affected</returns>
        //public int UpdateUser(UserProperties objUser)
        //{

        //    return objDBDoc.UpdateUser(objUser);
        //}

        ///// <summary>
        ///// It updates columns delete by and date of deletion in DMS_User
        ///// </summary>
        ///// <param name="objUser"></param>
        ///// <returns></returns>
        //public int DeleteUser(UserProperties objUser)
        //{

        //    return objDBDoc.DeleteUser(objUser);
        //}

        ///// <summary>
        ///// It checkes user name exist or not in user DMS_User table
        ///// </summary>
        ///// <param name="UserName">It is a user Name</param>
        ///// <returns>Bool</returns>
        //public bool CheckUserNameExist(string UserName)
        //{
        //    try
        //    {

        //        return objDBDoc.CheckUserNameExist(UserName);
        //    }
        //    catch
        //    { }
        //    return true;
        //}

        ///// <summary>
        ///// It returns user detail by user id
        ///// </summary>
        ///// <param name="UserID">It is user id</param>
        ///// <returns>User propertys object</returns>
        //public UserProperties getUSerDetailByID(int UserID)
        //{
        //    try
        //    {
        //        UserProperties objUser = new UserProperties();

        //        IDataReader reader = objDBDoc.getUSerDetailByID(UserID);
        //        if (reader.Read())
        //        {
        //            objUser.UserId = DataHelper.GetInt(reader, "User_ID");
        //            objUser.UserName = DataHelper.GetString(reader, "User_Name");
        //            objUser.Password = DataHelper.GetString(reader, "Password");
        //            objUser.First_Name = DataHelper.GetString(reader, "First_Name");
        //            objUser.Last_Name = DataHelper.GetString(reader, "Last_Name");
        //            objUser.Address = DataHelper.GetString(reader, "Address");
        //            objUser.Email = DataHelper.GetString(reader, "Email");
        //            objUser.Phone = DataHelper.GetString(reader, "Phone");
        //            objUser.Created_By = DataHelper.GetInt(reader, "Created_By");
        //            objUser.Date_Of_Creation = DataHelper.GetDateTime(reader, "Date_Of_Creation");
        //        }
        //        return objUser;
        //    }
        //    catch
        //    { }
        //    return null;
        //}

        //public UserProperties getUSerDetailByUserName(string UserName)
        //{
        //    try
        //    {
        //        UserProperties objUser = new UserProperties();

        //        IDataReader reader = objDBDoc.getUSerDetailByUserName(UserName);
        //        if (reader.Read())
        //        {
        //            objUser.UserId = DataHelper.GetInt(reader, "User_ID");
        //            objUser.UserName = DataHelper.GetString(reader, "User_Name");
        //            objUser.Password = DataHelper.GetString(reader, "Password");
        //            objUser.First_Name = DataHelper.GetString(reader, "First_Name");
        //            objUser.Last_Name = DataHelper.GetString(reader, "Last_Name");
        //            objUser.Address = DataHelper.GetString(reader, "Address");
        //            objUser.Email = DataHelper.GetString(reader, "Email");
        //            objUser.Phone = DataHelper.GetString(reader, "Phone");
        //            objUser.Created_By = DataHelper.GetInt(reader, "Created_By");
        //            objUser.Date_Of_Creation = DataHelper.GetDateTime(reader, "Date_Of_Creation");
        //        }
        //        return objUser;
        //    }
        //    catch
        //    { }
        //    return null;
        //}

        //#endregion

    }
}
