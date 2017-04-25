using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.VET;
using System.Data;

namespace SMS.Business.VET
{
    public class BLL_VET_Questionnaire
    {
        DAL_VET_Questioinnaire objVetDAL = new DAL_VET_Questioinnaire();

        /// <summary>
        /// To display Questionnaire in grid
        /// </summary>
        /// <param name="Module">Module ID</param>
        /// <param name="dtVesselType">List of selected vessel type</param>
        /// <param name="dtVettingType">List of selected vetting type</param>
        /// <param name="dtStatus">List of selected Questionnaire status</param>
        /// <param name="SearchQuest_Number">Search by question number </param>
        /// <param name="SearchQuest_Version">Search by version number</param>
        /// <param name="SearchQuestionnaire">Search by questionnaire</param>
        /// <param name="sortby">Column name by which data to be sorted  </param>
        /// <param name="sortdirection">Direction in which data to be sorted 'ASC' or 'DESC'</param>
        /// <param name="pagenumber">Page Number of displaying data</param>
        /// <param name="pagesize">Max data to be return</param>
        /// <param name="isfetchcount">Return Total Count</param>
        /// <returns></returns>
        public DataSet VET_Get_Questionnaire(int? Module, DataTable dtVesselType, DataTable dtVettingType, DataTable dtStatus, string SearchQuest_Number, string SearchQuest_Version, string SearchQuestionnaire, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            try
            {
                return objVetDAL.VET_Get_Questionnaire(Module, dtVesselType,dtVettingType, dtStatus, SearchQuest_Number, SearchQuest_Version, SearchQuestionnaire, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To Get records of selected Questionnaire Id
        /// </summary>
        /// <param name="Questionnaire_ID">Get records of selected Questionnaire Id</param>
        /// <returns></returns>
        public DataTable VET_Get_QuestionnaireDetailsByID(int? Questionnaire_ID)
        {
            try
            {
                return objVetDAL.VET_Get_QuestionnaireDetailsByID(Questionnaire_ID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// To display the Questionnaire details in grid
        /// </summary>
        /// <param name="Questionnaire_Id">Questionnaire ID</param>
        /// <param name="dtSectionNO">List of selected section no </param>
        /// <param name="dtQuestionNo">List of selected question no</param>
        /// <param name="SearchQuestion">Search by question</param>
        /// <param name="sortby">Column name by which data to be sorted </param>
        /// <param name="sortdirection">Direction in which data to be sorted 'ASC' or 'DESC'</param>
        /// <param name="pagenumber">Page Number of displaying data </param>
        /// <param name="pagesize">Max data to be return</param>
        /// <param name="isfetchcount">Return Total Count</param>
        /// <returns></returns>
        public DataSet VET_Get_QuestionnaireDetails(int? Questionnaire_Id, DataTable dtSectionNO, DataTable dtQuestionNo, string SearchQuestion, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            try
            {
                return objVetDAL.VET_Get_QuestionnaireDetails(Questionnaire_Id, dtSectionNO, dtQuestionNo,SearchQuestion, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// To Get version number against Questionnaire id
        /// </summary>
        /// <param name="Number">Number of questionnaire id</param>
        /// <param name="PreVersion">previous version number of questionnaire id</param>
        /// <returns></returns>
        public DataSet VET_Get_VersionNumber( string Number, string PreVersion)
        {
            try
            {
                return objVetDAL.VET_Get_VersionNumber(Number, PreVersion);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// To insert New Version with preious version Question Details
        /// </summary>
        /// <param name="Number">Number of questionnaire</param>
        /// <param name="PreVersion">Previous version of Questionnaire</param>
        /// <param name="NewVersion">New version of Questionnaire</param>
        /// <param name="Questionnaire_ID">Questionnaire Id</param>
        /// <param name="Status">Status of questionnaire</param>
        /// <param name="userID">User Id of crated by</param>
        /// <returns></returns>
        public DataTable VET_Ins_NewVersionQuestionDetails(string Number, string PreVersion, string NewVersion, int? Questionnaire_ID, string Status, int userID)
        {
            try
            {
                return objVetDAL.VET_Ins_NewVersionQuestionDetails(Number, PreVersion, NewVersion, Questionnaire_ID, Status, userID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// To Insert details of Questionnaire
        /// </summary>
        /// <param name="Module_ID">Module Id </param>
        /// <param name="VetType_ID">Vetting Type Id</param>
        /// <param name="VesselType_ID">Vessel Type Id </param>
        /// <param name="Questionnaire_Name">Questionnaire Name</param>
        /// <param name="Number">Unique Number for Questionnaire</param>
        /// <param name="Version">Version for Questionnaire</param>
        /// <param name="userID">ID of created by</param>
        /// <returns></returns>
        public DataTable VET_Ins_Questionnaire(int? Module_ID, int? VetType_ID, DataTable VesselType_ID, string Questionnaire_Name, string Number, string Version, int userID)
        {
            try
            {
                return objVetDAL.VET_Ins_Questionnaire(Module_ID, VetType_ID, VesselType_ID, Questionnaire_Name, Number, Version, userID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Insert Section / Question
        /// </summary>
        /// <param name="Questionnaire_ID">Questionnaire Id of question</param>
        /// <param name="Type">Type of Question :Question</param>
        /// <param name="Section_No">Section no </param>
        /// <param name="Level_1">Level_1</param>
        /// <param name="Level_2">Level_2</param>
        /// <param name="Level_3">Level_3</param>
        /// <param name="Question">Question text</param>
        /// <param name="Remarks">Remarks</param>
        /// <param name="userID">User ID of created by</param>
        /// <returns></returns>
        public int VET_Ins_SectionAndQuestion(int? Questionnaire_ID, string Type, int? Section_No, int? Level_1, int? Level_2, int? Level_3, string Question, string Remarks, int userID)
        {
            try
            {
                return objVetDAL.VET_Ins_SectionAndQuestion(Questionnaire_ID, Type, Section_No, Level_1, Level_2, Level_3, Question, Remarks, userID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// To Update Question/section
        /// </summary>
        /// <param name="Question_ID">Question id</param>
        /// <param name="Question">Question text</param>
        /// <param name="Remarks">Remarks</param>
        /// <param name="userID">User ID of modified by</param>
        /// <returns></returns>
        public int VET_Upd_QuestionAndSection(int? Question_ID, int? Section_No, int? Level_1, int? Level_2, int? Level_3, string Question, string Remarks, int userID)
        {
            try
            {
                return objVetDAL.VET_Upd_QuestionAndSection(Question_ID, Section_No, Level_1, Level_2, Level_3,Question, Remarks, userID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Insert Questionnaire attachment
        /// </summary>
        /// <param name="Questionnaire_ID">Questionnaire Id</param>
        /// <param name="Attachement_Name">Attachment name</param>
        /// <param name="Attachement_Path">Attachment path</param>
        /// <param name="userID">User id of created by</param>
        /// <returns></returns>
        public int VET_Ins_QuestionnaireAttachment(int? Questionnaire_ID, string Attachement_Name, string Attachement_Path, int userID)
        {
            try
            {
                return objVetDAL.VET_Ins_QuestionnaireAttachment(Questionnaire_ID, Attachement_Name, Attachement_Path, userID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Mantain log for Archived questionnaire and update status
        /// </summary>
        /// <param name="Questionnaire_ID">Questionnaire ID</param>
        /// <param name="userID">User id of deleted by</param>
        /// <returns></returns>
        public int VET_Del_Questionnaire(int? Questionnaire_ID, int userID)
        {
            try
            {
                return objVetDAL.VET_Del_Questionnaire(Questionnaire_ID, userID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// To Update Questionnaire if its published  by user
        /// </summary>
        /// <param name="NewVersion">New version no of questionnaire</param>
        /// <param name="Number">Number of questionnaire</param>
        /// <param name="userID">User id of updated by</param>
        /// <returns></returns>
        public int VET_Upd_QuestionnaireStatus(string NewVersion, string Number, int userID, int? Questionnaire_ID)
        {
            try
            {
                return objVetDAL.VET_Upd_QuestionnaireStatus(NewVersion,  Number, userID , Questionnaire_ID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// To bind Questionnaire Attachment
        /// </summary>
        /// <param name="Questionnaire_ID">Questionnaire id of questionnaire attachment</param>
        /// <returns></returns>
        public DataTable VET_Get_QuestionnaireAttachment(int? Questionnaire_ID)
        {
            try
            {
                return objVetDAL.VET_Get_QuestionnaireAttachment(Questionnaire_ID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// To Delete  Question
        /// </summary>
        /// <param name="Question_ID">Question id</param>
        /// <param name="userID">User id of deleted by</param>
        /// <returns></returns>
        public int VET_Del_Question(int? Question_ID, int userID)
        {
            try
            {
                return objVetDAL.VET_Del_Question(Question_ID, userID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// To check atleat one question is exist for questionnaire
        /// </summary>
        /// <param name="Questionnaire_Id">Questionnaire id</param>
        /// <returns></returns>
        public DataSet VET_Get_ExistQuestionForQuestionnaire(int? Questionnaire_Id)
        {
            try
            {
                return objVetDAL.VET_Get_ExistQuestionForQuestionnaire(Questionnaire_Id);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// To get existing Questionnire to validate questionnaire
        /// </summary>
        /// <param name="Module_ID">Module id </param>
        /// <param name="VetType_ID">vetting type id</param>
        /// <param name="VesselType_ID">vessel type id </param>
        /// <param name="Number">Number of questionnaire</param>
        /// <param name="Version">version number of questionnaire</param>
        /// <returns></returns>
        public DataSet VET_Get_ExistQuestinnaire(int? Module_ID, int? VetType_ID,DataTable VesselType_ID, string Number, string Version)
        {
            try
            {
                return objVetDAL.VET_Get_ExistQuestinnaire(Module_ID, VetType_ID, VesselType_ID, Number, Version);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// To get exist question number
        /// </summary>
        /// <param name="Questionnire_Id">Questionnaire id</param>
        /// <param name="Level_1">Level 1 of question</param>
        /// <param name="Level_2">Level 2 of question</param>
        /// <param name="Level_3">Level 3 of question</param>
        /// <returns></returns>
        public DataTable VET_Get_ExistQuestionNo(int? Questionnire_Id,int? Section, int? Level_1, int? Level_2, int? Level_3,string Mode, int? QuestionId)
        {
            try
            {
                return objVetDAL.VET_Get_ExistQuestionNo(Questionnire_Id, Section, Level_1, Level_2, Level_3, Mode, QuestionId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// To get Section list for Questionnaire details
        /// </summary>
        /// <param name="Questionnnire_Id">Questionnaire id</param>
        /// <returns></returns>
        public DataTable VET_Get_SectionList(int? Questionnnire_Id)
        {
            try
            {
                return objVetDAL.VET_Get_SectionList(Questionnnire_Id);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// To get Question list for Questionnaire details
        /// </summary>
        /// <param name="Questionnnire_Id">Questionnaire id</param>
        /// <returns></returns>
        public DataTable VET_Get_QuestionList(int? Questionnnire_Id)
        {
            try
            {
                return objVetDAL.VET_Get_QuestionList(Questionnnire_Id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Question no By section no
        /// </summary>
        /// <param name="dtSectionNo">List section No</param>
        /// <returns></returns>
        public DataTable VET_Get_QuestionNoBySectionNo(int? Questionnaire_Id,DataTable dtSectionNo)
        {
            try
            {
                return objVetDAL.VET_Get_QuestionNoBySectionNo(Questionnaire_Id, dtSectionNo);
            }
            catch
            {
                throw;
            }
        }
    }
}
