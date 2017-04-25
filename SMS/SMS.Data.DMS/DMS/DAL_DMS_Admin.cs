using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Data.DMS
{
    public class DAL_DMS_Admin
    {
        private string connection = "";
        public DAL_DMS_Admin(string ConnectionString)
        {
            connection = ConnectionString;
        }

        public DAL_DMS_Admin()
        {
            connection = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        }

        #region -- DOCUMENT TYPE LIBRARY --
        public int Check_Document_Expiry_DL(int DocTypeID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
			{   
				new SqlParameter("@DocTypeID",DocTypeID),
		    	new SqlParameter("return",SqlDbType.Int)
			};
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Check_Document_Expiry", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public DataTable Get_DocTypeList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocTypeList").Tables[0];
        }
        public DataTable Get_DocTypeList_DL(int Vessel_Flag, string SearchText, int RankID, int DocTypeId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                { 
                                    new SqlParameter("@Vessel_Flag",Vessel_Flag),
                                    new SqlParameter("@SearchText",SearchText),
                                    new SqlParameter("@RankID",RankID)
                                };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocTypeList", sqlprm).Tables[0];

        }

        public int EditDocType_DL(int DocTypeID, string DocTypeName)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@DocTypeName",DocTypeName)
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Update_DocType", sqlprm);

        }
        public int EditDocType_DL(int DocTypeID, string DocTypeName, string Legend, string Deck, string Engine, int AlertDays, int isDocCheckList, int Voyage)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@DocTypeName",DocTypeName),
                                            new SqlParameter("@Legend",Legend),
                                            new SqlParameter("@Deck",Deck),
                                            new SqlParameter("@Engine",Engine),
                                            new SqlParameter("@AlertDays",AlertDays),
                                            new SqlParameter("@isDocCheckList",isDocCheckList),
                                            new SqlParameter("@Voyage",Voyage)                                            
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Update_DocType", sqlprm);

        }
        public int EditDocType_DL(int DocTypeID, string DocTypeName, string Legend, string Deck, string Engine, int AlertDays, int isDocCheckList, int Voyage, int Vessel_Flag, int isExpiryMandatory, DataTable VesselFlagList, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@DocTypeName",DocTypeName),
                                            new SqlParameter("@Legend",Legend),
                                            new SqlParameter("@Deck",Deck),
                                            new SqlParameter("@Engine",Engine),
                                            new SqlParameter("@AlertDays",AlertDays),
                                            new SqlParameter("@isDocCheckList",isDocCheckList),
                                            new SqlParameter("@Voyage",Voyage),
                                            new SqlParameter("@Vessel_Flag",Vessel_Flag),
                                            new SqlParameter("@isExpiryMandatory",isExpiryMandatory),
                                            new SqlParameter("@VesselFlagList",VesselFlagList),
                                             new SqlParameter("@ModifiedBy",Modified_By),
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Update_DocType", sqlprm);

        }

        public int EditDocType_DL(int DocTypeID, int GroupId, string DocTypeName, string Legend, string Deck, string Engine, int AlertDays, int isDocCheckList, int Voyage, int isExpiryMandatory, DataTable VesselFlagList, DataTable VesselList, DataTable RankList, DataTable CountryList, DataTable ReplacableDocumentList, int Modified_By, string Document_Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@DocTypeName",DocTypeName),
                                            new SqlParameter("@Legend",Legend),
                                            new SqlParameter("@Deck",Deck),
                                            new SqlParameter("@Engine",Engine),
                                            new SqlParameter("@AlertDays",AlertDays),
                                            new SqlParameter("@isDocCheckList",isDocCheckList),
                                            new SqlParameter("@Voyage",Voyage),
                                            new SqlParameter("@isExpiryMandatory",isExpiryMandatory),
                                            new SqlParameter("@GroupId",GroupId),
                                            new SqlParameter("@ModifiedBy",Modified_By),
                                            new SqlParameter("@VesselFlagList",VesselFlagList),
                                            new SqlParameter("@VesselList",VesselList),
                                            new SqlParameter("@RankList",RankList),
                                            new SqlParameter("@CountryList",CountryList),
                                            new SqlParameter("@ReplacableDocumentList",ReplacableDocumentList),
                                            new SqlParameter("@Document_Type",Document_Type),
                                            new SqlParameter("return",SqlDbType.Int)
                                          
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Update_DocType", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        /// <summary>
        /// @ScannedDocMandatory is added for DMS CR 11292
        /// </summary>
        /// <param name="DocTypeID"></param>
        /// <param name="GroupId"></param>
        /// <param name="DocTypeName"></param>
        /// <param name="Legend"></param>
        /// <param name="Deck"></param>
        /// <param name="Engine"></param>
        /// <param name="AlertDays"></param>
        /// <param name="isDocCheckList"></param>
        /// <param name="Voyage"></param>
        /// <param name="isExpiryMandatory"></param>
        /// <param name="VesselFlagList"></param>
        /// <param name="VesselList"></param>
        /// <param name="RankList"></param>
        /// <param name="CountryList"></param>
        /// <param name="ReplacableDocumentList"></param>
        /// <param name="Modified_By"></param>
        /// <param name="Document_Type"></param>
        /// <param name="isScannedDocMandatory"></param>
        /// <returns></returns>
        public int EditDocType_DL(int DocTypeID, int GroupId, string DocTypeName, string Legend, string Deck, string Engine, int AlertDays, int isDocCheckList, int Voyage, int isExpiryMandatory, DataTable VesselFlagList, DataTable VesselList, DataTable RankList, DataTable CountryList, DataTable ReplacableDocumentList, int Modified_By, string Document_Type, int isScannedDocMandatory)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@DocTypeName",DocTypeName),
                                            new SqlParameter("@Legend",Legend),
                                            new SqlParameter("@Deck",Deck),
                                            new SqlParameter("@Engine",Engine),
                                            new SqlParameter("@AlertDays",AlertDays),
                                            new SqlParameter("@isDocCheckList",isDocCheckList),
                                            new SqlParameter("@Voyage",Voyage),
                                            new SqlParameter("@isExpiryMandatory",isExpiryMandatory),
                                            new SqlParameter("@GroupId",GroupId),
                                            new SqlParameter("@ModifiedBy",Modified_By),
                                            new SqlParameter("@VesselFlagList",VesselFlagList),
                                            new SqlParameter("@VesselList",VesselList),
                                            new SqlParameter("@RankList",RankList),
                                            new SqlParameter("@CountryList",CountryList),
                                            new SqlParameter("@ReplacableDocumentList",ReplacableDocumentList),
                                            new SqlParameter("@Document_Type",Document_Type), 
                                            new SqlParameter("@isScannedDocMandatory",isScannedDocMandatory),
                                            new SqlParameter("return",SqlDbType.Int)
                                          
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Update_DocType", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int EditDocType_DL(int DocTypeID, int GroupId, string DocTypeName, string Legend, string Deck, string Engine, int AlertDays, int isDocCheckList, int Voyage, int isExpiryMandatory, int Modified_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@DocTypeName",DocTypeName),
                                            new SqlParameter("@Legend",Legend),
                                            new SqlParameter("@Deck",Deck),
                                            new SqlParameter("@Engine",Engine),
                                            new SqlParameter("@AlertDays",AlertDays),
                                            new SqlParameter("@isDocCheckList",isDocCheckList),
                                            new SqlParameter("@Voyage",Voyage),
                                            new SqlParameter("@isExpiryMandatory",isExpiryMandatory),
                                            new SqlParameter("@GroupId",GroupId),
                                            new SqlParameter("@ModifiedBy",Modified_By)
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Update_DocType", sqlprm);

        }

        /// <summary>
        /// @ScannedDocMandatory is added for DMS CR 11292
        /// </summary>
        /// <param name="DocTypeID"></param>
        /// <param name="GroupId"></param>
        /// <param name="DocTypeName"></param>
        /// <param name="Legend"></param>
        /// <param name="Deck"></param>
        /// <param name="Engine"></param>
        /// <param name="AlertDays"></param>
        /// <param name="isDocCheckList"></param>
        /// <param name="Voyage"></param>
        /// <param name="isExpiryMandatory"></param>
        /// <param name="Modified_By"></param>
        /// <param name="isScannedDocMandatory"></param>
        /// <returns></returns>
        public int EditDocType_DL(int DocTypeID, int GroupId, string DocTypeName, string Legend, string Deck, string Engine, int AlertDays, int isDocCheckList, int Voyage, int isExpiryMandatory, int Modified_By, int isScannedDocMandatory)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@DocTypeName",DocTypeName),
                                            new SqlParameter("@Legend",Legend),
                                            new SqlParameter("@Deck",Deck),
                                            new SqlParameter("@Engine",Engine),
                                            new SqlParameter("@AlertDays",AlertDays),
                                            new SqlParameter("@isDocCheckList",isDocCheckList),
                                            new SqlParameter("@Voyage",Voyage),
                                            new SqlParameter("@isExpiryMandatory",isExpiryMandatory),
                                            new SqlParameter("@GroupId",GroupId),
                                            new SqlParameter("@ModifiedBy",Modified_By),
                                           new SqlParameter("@isScannedDocMandatory",isScannedDocMandatory)
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Update_DocType", sqlprm);

        }


        public int InsertDocType_DL(string DocTypeName, int GroupId, string Legend, string Deck, string Engine, int AlertDays, int isDocCheckList, int VoyageDoc, int ExpiryMandatory, DataTable VesselFlagList, DataTable VesselList, DataTable RankList, DataTable CountryList, DataTable ReplacableDocumentList, int CreatedBy, string Document_Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeName",DocTypeName),
                                            new SqlParameter("@GroupId",GroupId),
                                            new SqlParameter("@Legend",Legend),
                                            new SqlParameter("@Deck",Deck),
                                            new SqlParameter("@Engine",Engine),
                                            new SqlParameter("@AlertDays",AlertDays),
                                            new SqlParameter("@isDocCheckList",isDocCheckList),
                                            new SqlParameter("@VoyageDoc",VoyageDoc),
                                            new SqlParameter("@ExpiryMandatory",ExpiryMandatory),
                                            new SqlParameter("@VesselFlagList",VesselFlagList),
                                            new SqlParameter("@VesselList",VesselList),
                                            new SqlParameter("@RankList",RankList),
                                            new SqlParameter("@CountryList",CountryList),
                                            new SqlParameter("@ReplacableDocumentList",ReplacableDocumentList),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            new SqlParameter("@Document_Type",Document_Type), 
                                            new SqlParameter("return",SqlDbType.Int)
                                            
                                         };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Insert_DocType", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        /// <summary>
        /// @ScannedDocMandatory is added for DMS CR 11292
        /// </summary>
        /// <param name="DocTypeName"></param>
        /// <param name="GroupId"></param>
        /// <param name="Legend"></param>
        /// <param name="Deck"></param>
        /// <param name="Engine"></param>
        /// <param name="AlertDays"></param>
        /// <param name="isDocCheckList"></param>
        /// <param name="VoyageDoc"></param>
        /// <param name="ExpiryMandatory"></param>
        /// <param name="VesselFlagList"></param>
        /// <param name="VesselList"></param>
        /// <param name="RankList"></param>
        /// <param name="CountryList"></param>
        /// <param name="ReplacableDocumentList"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="Document_Type"></param>
        /// <param name="ScannedDocMandatory"></param>
        /// <returns></returns>
        public int InsertDocType_DL(string DocTypeName, int GroupId, string Legend, string Deck, string Engine, int AlertDays, int isDocCheckList, int VoyageDoc, int ExpiryMandatory, DataTable VesselFlagList, DataTable VesselList, DataTable RankList, DataTable CountryList, DataTable ReplacableDocumentList, int CreatedBy, string Document_Type, int ScannedDocMandatory)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeName",DocTypeName),
                                            new SqlParameter("@GroupId",GroupId),
                                            new SqlParameter("@Legend",Legend),
                                            new SqlParameter("@Deck",Deck),
                                            new SqlParameter("@Engine",Engine),
                                            new SqlParameter("@AlertDays",AlertDays),
                                            new SqlParameter("@isDocCheckList",isDocCheckList),
                                            new SqlParameter("@VoyageDoc",VoyageDoc),
                                            new SqlParameter("@ExpiryMandatory",ExpiryMandatory),
                                            new SqlParameter("@VesselFlagList",VesselFlagList),
                                            new SqlParameter("@VesselList",VesselList),
                                            new SqlParameter("@RankList",RankList),
                                            new SqlParameter("@CountryList",CountryList),
                                            new SqlParameter("@ReplacableDocumentList",ReplacableDocumentList),
                                            new SqlParameter("@CreatedBy",CreatedBy),
                                            new SqlParameter("@Document_Type",Document_Type), 
                                            new SqlParameter("@ScannedDocMandatory",ScannedDocMandatory),
                                            new SqlParameter("return",SqlDbType.Int)
                                            
                                         };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Insert_DocType", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }


     
        public int InsertDocType_DL(string DocTypeName, int AlertDays, int isDocCheckList, int VoyageDoc, int Vessel_Flag, int ExpiryMandatory, DataTable VesselFlagList, int CreatedBy)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeName",DocTypeName),
                                            new SqlParameter("@AlertDays",AlertDays),
                                            new SqlParameter("@isDocCheckList",isDocCheckList),
                                            new SqlParameter("@VoyageDoc",VoyageDoc),
                                            new SqlParameter("@Vessel_Flag",Vessel_Flag),
                                             new SqlParameter("@ExpiryMandatory",ExpiryMandatory),
                                             new SqlParameter("@VesselFlagList",VesselFlagList),
                                              new SqlParameter("@CreatedBy",CreatedBy)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Insert_DocType", sqlprm);
        }



        public int DeleteDocType_DL(int DocTypeID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@DocTypeID",DocTypeID),
                new SqlParameter("@Deleted_By",Deleted_By)
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Del_DocType", sqlprm);
        }
        # endregion

        #region --DOCUMENT ATTRIBUTE LIBRARY --
        public DataTable Get_DocAttributeList_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_DocAttributeList").Tables[0];
        }

        public DataTable Get_AttributeDetails_DL(int AttributeID)
        {
            SqlParameter sqlprm = new SqlParameter("@AttributeID", AttributeID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_AttributeDetails", sqlprm).Tables[0];
        }

        public int EditDocAttribute_DL(int AttributeID, string AttributeName, string AttributeDataType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@AttributeID",AttributeID),
                                            new SqlParameter("@AttributeName",AttributeName),
                                            new SqlParameter("@AttributeDataType",AttributeDataType)
                                        };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Update_DocAttribute", sqlprm);

        }

        public int InsertDocAttribute_DL(string AttributeName, string AttributeDataType)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@AttributeName",AttributeName),
                                            new SqlParameter("@AttributeDataType",AttributeDataType),
                                            new SqlParameter("@ListSource","")
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Insert_DocAttribute", sqlprm);
        }
        public int InsertDocAttribute_DL(string AttributeName, string AttributeDataType, string ListSource)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@AttributeName",AttributeName),
                                            new SqlParameter("@AttributeDataType",AttributeDataType),
                                            new SqlParameter("@ListSource",ListSource)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Insert_DocAttribute", sqlprm);
        }
        public int DeleteDocAttribute_DL(int AttributeID)
        {
            SqlParameter sqlprm = new SqlParameter("@AttributeID", AttributeID);
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Del_DocAttribute", sqlprm);
        }

        #endregion

        #region -- DOCTYPE and ATTRIBUTE LINKING --


        public DataTable Get_AssignedAttributesToTypeID_DL(int DocTypeID)
        {
            SqlParameter sqlprm = new SqlParameter("@DocTypeID", DocTypeID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_AssignedAttributesToTypeID", sqlprm).Tables[0];
        }
        public DataTable Get_UnAssignedAttributesToTypeID_DL(int DocTypeID)
        {
            SqlParameter sqlprm = new SqlParameter("@DocTypeID", DocTypeID);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_UnAssignedAttributesToTypeID", sqlprm).Tables[0];
        }

        public int Add_AttributeToDocType_DL(int DocTypeID, int AttributeID, int IsRequired)
        {
            SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@DocTypeID", DocTypeID), 
                new SqlParameter("@AttributeID", AttributeID),
                new SqlParameter("@IsRequired", IsRequired) 
            };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Add_AttributeToDocType", sqlprm);
        }
        public int Remove_AttributeFromDocType_DL(int ID)
        {
            SqlParameter sqlprm = new SqlParameter("@ID", ID);
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Del_AttributeFromDocType", sqlprm);
        }

        public int Update_DocType_Attribute_DL(int ID, int IsRequired, int AlertDays)
        {
            int ret = 0;
            if (IsRequired != -1)
            {
                SqlParameter[] sqlprm = new SqlParameter[] { 
                new SqlParameter("@ID", ID), 
                new SqlParameter("@IsRequired", IsRequired)
                };
                ret = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Update_Attribute_IsRequired", sqlprm);
            }
            if (AlertDays != -1)
            {
                SqlParameter[] sqlprm1 = new SqlParameter[] { 
                new SqlParameter("@ID", ID), 
                new SqlParameter("@AlertDays", AlertDays)
                };
                ret = SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_Update_Attribute_AlertDays", sqlprm1);
            }
            return ret;
        }

        public DataTable Get_AttributeListSource_DL()
        {
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_AttributeListSource").Tables[0];
        }

        public DataTable Get_ListItemsFromListSource_DL(string ListSource)
        {
            string strSQL = "";
            strSQL = "SELECT top(100) * FROM " + ListSource + " ORDER BY TEXT";
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, strSQL).Tables[0];
        }
        public DataTable Get_ListItemsFromListSource_DL(string ListSource, string FilterString)
        {
            string strSQL = "";
            strSQL = "SELECT top(100) * FROM " + ListSource + " WHERE TEXT LIKE '%" + FilterString + "%' ORDER BY TEXT";
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, strSQL).Tables[0];
        }

        public DataTable Get_VesselAndFlagIds_DocumentType(string DocumentType)
        {
            SqlParameter sqlprm = new SqlParameter("@DocumentType", DocumentType);
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_Get_VesselAndFlagIds_DocumentType", sqlprm).Tables[0];
        }

        #endregion

        #region -- DOCTYPE and RANK LINKING --
        public DataTable Get_MandatoryRankList_DL(int DocTypeID, int RankId)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@RankId", RankId)
                                         };

            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "SP_DMS_Get_MandatoryRankList", sqlprm).Tables[0];
        }
        public int UPDATE_MandatoryRankList_DL(int DocTypeID, string Rank_Name, int Selected, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@Rank_Name",Rank_Name),
                                            new SqlParameter("@Selected",Selected),
                                            new SqlParameter("@UserID",UserID)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_DMS_UPDATE_MandatoryRankList", sqlprm);

        }
        public int IsChecklist_DL(int DocTypeID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_DocType_IsChecklist", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int IsVoyageRelated_DL(int DocTypeID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "SP_CRW_DocType_IsVoyageRelated", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        #endregion

        /// <summary>
        /// Changes done in SP for Multiple Ranks In DMS CR 11292
        /// </summary>
        /// <param name="DocTypeId"></param>
        /// <param name="GroupId"></param>
        /// <param name="VesselId"></param>
        /// <param name="VesselFlagId"></param>
        /// <param name="CountryId"></param>
        /// <param name="RankId"></param>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        public DataTable Get_DocTypeList_New_DL(int DocTypeId, int GroupId, int VesselId, int VesselFlagId, int CountryId, int RankId, string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                 new SqlParameter("@DocTypeId",DocTypeId),
                new SqlParameter("@GroupId",GroupId),
                new SqlParameter("@VesselId",VesselId),
                new SqlParameter("@VesselFlagId",VesselFlagId),
                new SqlParameter("@CountryId",CountryId),
                new SqlParameter("@RankId",RankId),
                new SqlParameter("@SearchText",SearchText)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_Get_DocTypeList", sqlprm).Tables[0];
        }

        public DataTable Get_NationalityList_DL(int DocTypeID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@DocTypeID",DocTypeID),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_Get_NationalityList", sqlprm).Tables[0];
        }
        public DataTable Get_RankList_DL(int DocTypeID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@DocTypeID",DocTypeID),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_Get_RankList", sqlprm).Tables[0];
        }
        public DataTable Get_VesselList_DL(int DocTypeID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@DocTypeID",DocTypeID),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_Get_VesselList", sqlprm).Tables[0];
        }
        public DataTable Get_VesselFlagList_DL(int DocTypeID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@DocTypeID",DocTypeID),
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_Get_VesselFlagList", sqlprm).Tables[0];
        }

        public int INS_NationalityList_DL(int DocTypeID, DataTable dt, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@dt",dt),
                                            new SqlParameter("@UserID",UserID)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_INS_Nationality", sqlprm);
        }
        public int INS_RankList_DL(int DocTypeID, DataTable dt, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@dt",dt),
                                            new SqlParameter("@UserID",UserID)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_INS_Ranks", sqlprm);
        }
        public int INS_VesselList_DL(int DocTypeID, DataTable dt, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@dt",dt),
                                            new SqlParameter("@UserID",UserID)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_INS_Vessels", sqlprm);
        }
        public int INS_VesselFlagList_DL(int DocTypeID, DataTable dt, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@dt",dt),
                                            new SqlParameter("@UserID",UserID)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_INS_VesselFlags", sqlprm);
        }
        public int INS_ReplacableDocumentList_DL(int DocTypeID, DataTable dt, int UserID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@DocTypeID",DocTypeID),
                                            new SqlParameter("@dt",dt),
                                            new SqlParameter("@UserID",UserID)
                                         };
            return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_INS_ReplacableDocuments", sqlprm);
        }
        public DataTable Get_DocumentList_DL(int DocTypeID)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
            { 
                new SqlParameter("@DocTypeID",DocTypeID)
            };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_Get_DocumentList", sqlprm).Tables[0];
        }
        public DataTable Get_GroupList_DL()
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText","")
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_Get_DocumentGroupList", sqlprm).Tables[0];
        }
        public DataTable Get_GroupList_DL(string SearchText)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@SearchText",SearchText)
                                        };
            return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "DMS_Get_DocumentGroupList", sqlprm).Tables[0];
        }

        public int INSERT_Group_DL(int GroupId, string Group_Name, int Created_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@GroupID",GroupId),
                                            new SqlParameter("@Group_Name",Group_Name),
                                            new SqlParameter("@Created_By",Created_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_INS_DocumentGroup", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
        public int DELETE_Group_DL(int ID, int Deleted_By)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@GroupId",ID),
                                            new SqlParameter("@Deleted_By",Deleted_By),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_Delete_DocumentGroup", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }

        public int Get_DocumentTypeId_DL(string Document_Type)
        {
            SqlParameter[] sqlprm = new SqlParameter[]
                                        { 
                                            new SqlParameter("@Document_Type",Document_Type),
                                            new SqlParameter("return",SqlDbType.Int)
                                        };
            sqlprm[sqlprm.Length - 1].Direction = ParameterDirection.ReturnValue;
            SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, "DMS_GET_DocumentTypeId", sqlprm);
            return Convert.ToInt32(sqlprm[sqlprm.Length - 1].Value);
        }
    }
}
