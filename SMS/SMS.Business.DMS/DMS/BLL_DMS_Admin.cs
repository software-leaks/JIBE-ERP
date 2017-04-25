using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using SMS.Data.DMS;

namespace SMS.Business.DMS
{
    public class BLL_DMS_Admin
    {
        DAL_DMS_Admin objDAL = new DAL_DMS_Admin();


        #region -- DOCUMENT TYPE LIBRARY --
        public int Check_Document_Expiry(int DocTypeID)
        {
            try
            {
                return objDAL.Check_Document_Expiry_DL(DocTypeID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_DocTypeList()
        {
            try
            {
                return objDAL.Get_DocTypeList_DL();
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_DocTypeList(int Vessel_Flag, string SearchText,int RankID,int DocTypeId)
        {
            try
            {
                return objDAL.Get_DocTypeList_DL(Vessel_Flag, SearchText, RankID, DocTypeId);
            }
            catch
            {
                throw;
            }
        }
        public int EditDocType(int DocTypeID, string DocTypeName)
        {
            try
            {
                return objDAL.EditDocType_DL(DocTypeID, DocTypeName);
            }
            catch
            {
                throw;
            }
        }

        public int EditDocType(int DocTypeID, string DocTypeName, string Legend, string Deck, string Engine, int AlertDays, int isDocCheckList, int Voyage)
        {
            try
            {
                return objDAL.EditDocType_DL(DocTypeID, DocTypeName, Legend, Deck, Engine, AlertDays, isDocCheckList, Voyage);
            }
            catch
            {
                throw;
            }
        }
        public int EditDocType(int DocTypeID, string DocTypeName, string Legend, string Deck, string Engine, int AlertDays, int isDocCheckList, int Voyage, int Vessel_Flag, int isExpiryMandatory, DataTable VesselFlagList, int Modified_By)
        {
            try
            {
                return objDAL.EditDocType_DL(DocTypeID, DocTypeName, Legend, Deck, Engine, AlertDays, isDocCheckList, Voyage, Vessel_Flag, isExpiryMandatory, VesselFlagList, Modified_By);
            }
            catch
            {
                throw;
            }
        }
        public int EditDocType(int DocTypeID, int GroupId, string DocTypeName, string Legend, string Deck, string Engine, int AlertDays, int isDocCheckList, int Voyage, int isExpiryMandatory, int Modified_By)
        {
            try
            {
                return objDAL.EditDocType_DL(DocTypeID, GroupId, DocTypeName, Legend, Deck, Engine, AlertDays, isDocCheckList, Voyage, isExpiryMandatory, Modified_By);
            }
            catch
            {
                throw;
            }
        }

        //@ScannedDocMandatory is added for DMS CR 11292

        public int EditDocType(int DocTypeID, int GroupId, string DocTypeName, string Legend, string Deck, string Engine, int AlertDays, int isDocCheckList, int Voyage, int isExpiryMandatory, int Modified_By, int isScannedDocMandatory)
        {
            try
            {
                return objDAL.EditDocType_DL(DocTypeID, GroupId, DocTypeName, Legend, Deck, Engine, AlertDays, isDocCheckList, Voyage, isExpiryMandatory, Modified_By, isScannedDocMandatory);
            }
            catch
            {
                throw;
            }
        }

        public int EditDocType(int DocTypeID, int GroupId, string DocTypeName, string Legend, string Deck, string Engine, int AlertDays, int isDocCheckList, int Voyage, int isExpiryMandatory, DataTable VesselFlagList, DataTable VesselList, DataTable RankList, DataTable CountryList, DataTable ReplacableDocumentList, int Modified_By, string Document_Type)
        {
            try
            {
                return objDAL.EditDocType_DL(DocTypeID, GroupId, DocTypeName, Legend, Deck, Engine, AlertDays, isDocCheckList, Voyage, isExpiryMandatory, VesselFlagList, VesselList, RankList, CountryList, ReplacableDocumentList, Modified_By, Document_Type);
            }
            catch
            {
                throw;
            }
        }

        //@ScannedDocMandatory is added for DMS CR 11292

        public int EditDocType(int DocTypeID, int GroupId, string DocTypeName, string Legend, string Deck, string Engine, int AlertDays, int isDocCheckList, int Voyage, int isExpiryMandatory, DataTable VesselFlagList, DataTable VesselList, DataTable RankList, DataTable CountryList, DataTable ReplacableDocumentList, int Modified_By, string Document_Type, int isScannedDocMandatory)
        {
            try
            {
                return objDAL.EditDocType_DL(DocTypeID, GroupId, DocTypeName, Legend, Deck, Engine, AlertDays, isDocCheckList, Voyage, isExpiryMandatory, VesselFlagList, VesselList, RankList, CountryList, ReplacableDocumentList, Modified_By, Document_Type, isScannedDocMandatory);
            }
            catch
            {
                throw;
            }
        }
        public int InsertDocType(string DocTypeName, int GroupId, string Legend, string Deck, string Engine, int AlertDays, int isDocCheckList, int VoyageDoc, int ExpiryMandatory, DataTable VesselFlagList, DataTable VesselList, DataTable RankList, DataTable CountryList, DataTable ReplacableDocumentList, int CreatedBy, string Document_Type)
        {
            try
            {
                return objDAL.InsertDocType_DL(DocTypeName, GroupId, Legend, Deck, Engine, AlertDays, isDocCheckList, VoyageDoc, ExpiryMandatory, VesselFlagList, VesselList, RankList, CountryList, ReplacableDocumentList, CreatedBy, Document_Type);
            }
            catch
            {
                throw;
            }
        }

        //@ScannedDocMandatory is added for DMS CR 11292
        public int InsertDocType(string DocTypeName, int GroupId, string Legend, string Deck, string Engine, int AlertDays, int isDocCheckList, int VoyageDoc, int ExpiryMandatory, DataTable VesselFlagList, DataTable VesselList, DataTable RankList, DataTable CountryList, DataTable ReplacableDocumentList, int CreatedBy, string Document_Type, int ScannedDocMandatory)
        {
            try
            {
                return objDAL.InsertDocType_DL(DocTypeName, GroupId, Legend, Deck, Engine, AlertDays, isDocCheckList, VoyageDoc, ExpiryMandatory, VesselFlagList, VesselList, RankList, CountryList, ReplacableDocumentList, CreatedBy, Document_Type, ScannedDocMandatory);
            }
            catch
            {
                throw;
            }
        }
       public int InsertDocType(string DocTypeName, int AlertDays, int isDocCheckList, int VoyageDoc, int Vessel_Flag, int ExpiryMandatory,DataTable VesselFlagList,int CreatedBy)
        {
            try
            {
                return objDAL.InsertDocType_DL(DocTypeName, AlertDays, isDocCheckList, VoyageDoc, Vessel_Flag, ExpiryMandatory, VesselFlagList, CreatedBy);
            }
            catch
            {
                throw;
            }
        }
        public int DeleteDocType(int DocTypeID, int Deleted_By)
        {
            try
            {
                return objDAL.DeleteDocType_DL(DocTypeID,Deleted_By);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region -- DOCUMENT ATTRIBUTE LIBRARY --

        public DataTable Get_DocAttributeList()
        {
            try
            {
                return objDAL.Get_DocAttributeList_DL();
            }
            catch
            {
                throw;
            }
        }

        public int EditDocAttribute(int AttributeID, string AttributeName, string AttributeDataType)
        {
            try
            {
                return objDAL.EditDocAttribute_DL(AttributeID, AttributeName, AttributeDataType);
            }
            catch
            {
                throw;
            }
        }

        public int InsertDocAttribute(string AttributeName, string AttributeDataType)
        {
            try
            {
                return objDAL.InsertDocAttribute_DL(AttributeName, AttributeDataType);
            }
            catch
            {
                throw;
            }
        }
        public int InsertDocAttribute(string AttributeName, string AttributeDataType, string ListSource)
        {
            try
            {
                return objDAL.InsertDocAttribute_DL(AttributeName, AttributeDataType, ListSource);
            }
            catch
            {
                throw;
            }
        }

        public int DeleteDocAttribute(int AttributeID)
        {
            try
            {
                return objDAL.DeleteDocAttribute_DL(AttributeID);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region -- DOCTYPE and ATTRIBUTE LINKING --

        public DataTable Get_AssignedAttributesToTypeID(int DocTypeID)
        {
            try
            {
                return objDAL.Get_AssignedAttributesToTypeID_DL(DocTypeID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_UnAssignedAttributesToTypeID(int DocTypeID)
        {
            try
            {
                return objDAL.Get_UnAssignedAttributesToTypeID_DL(DocTypeID);
            }
            catch
            {
                throw;
            }
        }
                
        public int Remove_AttributeFromDocType(int ID)
        {
            try
            {
                return objDAL.Remove_AttributeFromDocType_DL(ID);
            }
            catch
            {
                throw;
            }
        }
        public int Add_AttributeToDocType(int DocTypeID, int AttributeID, int IsRequired)
        {
            try
            {
                return objDAL.Add_AttributeToDocType_DL(DocTypeID, AttributeID, IsRequired);
            }
            catch
            {
                throw;
            }
        }

        public int Update_DocType_Attribute(int ID, int IsRequired, int AlertDays)
        {
            try
            {
                return objDAL.Update_DocType_Attribute_DL(ID, IsRequired, AlertDays);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_AttributeListSource()
        {
            try
            {
                return objDAL.Get_AttributeListSource_DL();
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_ListItemsFromListSource(string ListSource)
        {
            return objDAL.Get_ListItemsFromListSource_DL(ListSource);
        }
        public DataTable Get_ListItemsFromListSource(string ListSource,string FilterString)
        {
            return objDAL.Get_ListItemsFromListSource_DL(ListSource, FilterString);
        }

        public DataTable Get_VesselAndFlagIds_DocumentType(string DocumentType)
        {
            return objDAL.Get_VesselAndFlagIds_DocumentType(DocumentType);
        }

        #endregion

        #region -- DOCTYPE and RANK LINKING --
        public DataTable Get_MandatoryRankList(int DocTypeID,int RankId)
        {
            try
            {
                return objDAL.Get_MandatoryRankList_DL(DocTypeID, RankId);
            }
            catch
            {
                throw;
            }
        }
        public int UPDATE_MandatoryRankList(int DocTypeID, string Rank_Name, int Selected, int UserID)
        {
            try
            {
                return objDAL.UPDATE_MandatoryRankList_DL(DocTypeID, Rank_Name, Selected,  UserID);
            }
            catch
            {
                throw;
            }
        }
        public int IsChecklist(int DocTypeID)
        {
            try
            {
                return objDAL.IsChecklist_DL(DocTypeID);
            }
            catch
            {
                throw;
            }
        }
        public int IsVoyageRelated(int DocTypeID)
        {
            try
            {
                return objDAL.IsVoyageRelated_DL(DocTypeID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_DocTypeList_New(int DocTypeId,int GroupId,int VesselId,int VesselFlagId,int CountryId,int RankId,string SearchText)
        {
            try
            {
                return objDAL.Get_DocTypeList_New_DL(DocTypeId, GroupId, VesselId, VesselFlagId, CountryId, RankId, SearchText);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_NationalityList(int DocTypeID)
        {
            try
            {
                return objDAL.Get_NationalityList_DL(DocTypeID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_RankList(int DocTypeID)
        {
            try
            {
                return objDAL.Get_RankList_DL(DocTypeID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_VesselList(int DocTypeID)
        {
            try
            {
                return objDAL.Get_VesselList_DL(DocTypeID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_VesselFlagList(int DocTypeID)
        {
            try
            {
                return objDAL.Get_VesselFlagList_DL(DocTypeID);
            }
            catch
            {
                throw;
            }
        }

        public int INS_NationalityList(int DocTypeID,DataTable dt,int UserId)
        {
            try
            {
                return objDAL.INS_NationalityList_DL(DocTypeID, dt, UserId);
            }
            catch
            {
                throw;
            }
        }
        public int INS_RankList(int DocTypeID, DataTable dt, int UserId)
        {
            try
            {
                return objDAL.INS_RankList_DL(DocTypeID, dt, UserId);
            }
            catch
            {
                throw;
            }
        }
        public int INS_VesselList(int DocTypeID, DataTable dt, int UserId)
        {
            try
            {
                return objDAL.INS_VesselList_DL(DocTypeID, dt, UserId);
            }
            catch
            {
                throw;
            }
        }
        public int INS_VesselFlagList(int DocTypeID, DataTable dt, int UserId)
        {
            try
            {
                return objDAL.INS_VesselFlagList_DL(DocTypeID, dt, UserId);
            }
            catch
            {
                throw;
            }
        }
        public int INS_ReplacableDocumentList(int DocTypeID, DataTable dt, int UserId)
        {
            try
            {
                return objDAL.INS_ReplacableDocumentList_DL(DocTypeID, dt, UserId);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_DocumentList(int DocTypeID)
        {
            try
            {
                return objDAL.Get_DocumentList_DL(DocTypeID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_GroupList()
        {
            try
            {
                return objDAL.Get_GroupList_DL();
            }
            catch
            {
                throw;
            }

        }
        public DataTable Get_GroupList(string SearchText)
        {
            try
            {
                return objDAL.Get_GroupList_DL(SearchText);
            }
            catch
            {
                throw;
            }

        }
        public int INSERT_Group(int GroupId,string Group_Name, int Created_By)
        {
            try
            {
                return objDAL.INSERT_Group_DL(GroupId,Group_Name, Created_By);
            }
            catch
            {
                throw;
            }

        }

        public int DELETE_Group(int ID, int Deleted_By)
        {
            try
            {
                return objDAL.DELETE_Group_DL(ID, Deleted_By);
            }
            catch
            {
                throw;
            }

        }
        public int Get_DocumentTypeId(string Document_Type)
        {
            try
            {
                return objDAL.Get_DocumentTypeId_DL(Document_Type);
            }
            catch
            {
                throw;
            }

        }
        #endregion
        
    }
}
