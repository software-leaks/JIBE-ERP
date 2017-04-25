using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using SMS.Data.BDS;

namespace SMS.Business.BDS
{
    public class DES_Encrypt_Decrypt
    {

        /// <summary>
        /// Encrypt a string.
        /// </summary>
        /// <param name="originalString">The original string.</param>
        /// <returns>The encrypted string.</returns>
        /// <exception cref="ArgumentNullException">This exception will be 
        /// thrown when the original string is null or empty.</exception>
        public static string EncryptString(string Message, string Passphrase)
        {
            byte[] Results;
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            try
            {
                System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

                // Step 1. We hash the passphrase using MD5
                // We use the MD5 hash generator as the result is a 128 bit byte array
                // which is a valid length for the TripleDES encoder we use below


                byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

                // Step 2. Create a new TripleDESCryptoServiceProvider object


                // Step 3. Setup the encoder
                TDESAlgorithm.Key = TDESKey;
                TDESAlgorithm.Mode = CipherMode.ECB;
                TDESAlgorithm.Padding = PaddingMode.PKCS7;

                // Step 4. Convert the input string to a byte[]
                byte[] DataToEncrypt = UTF8.GetBytes(Message);

                // Step 5. Attempt to encrypt the string

                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);

            }

            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
            // Step 6. Return the encrypted string as a base64 encoded string
            //return Convert.ToBase64String(Results);
        }

        /// <summary>
        /// Decrypt a crypted string.
        /// </summary>
        /// <param name="cryptedString">The crypted string.</param>
        /// <returns>The decrypted string.</returns>
        /// <exception cref="ArgumentNullException">This exception will be thrown 
        /// when the crypted string is null or empty.</exception>
        public static string DecryptString(string Message, string Passphrase)
        {

            byte[] Results;
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            try
            {
                // Step 1. We hash the passphrase using MD5
                // We use the MD5 hash generator as the result is a 128 bit byte array
                // which is a valid length for the TripleDES encoder we use below


                byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

                // Step 2. Create a new TripleDESCryptoServiceProvider object


                // Step 3. Setup the decoder
                TDESAlgorithm.Key = TDESKey;
                TDESAlgorithm.Mode = CipherMode.ECB;
                TDESAlgorithm.Padding = PaddingMode.PKCS7;

                // Step 4. Convert the input string to a byte[]

                byte[] DataToDecrypt = Convert.FromBase64String(Message);


                // Step 5. Attempt to decrypt the string
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);

            }

            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();

            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }
    }

    public class BLL_Build_Service
    {
        DAL_Build_Service objDAL = new DAL_Build_Service();

        public BLL_Build_Service()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        ////public DataTable Get_VesselList()
        ////{
        ////    try
        ////    {
        ////        return objDAL.Get_VesselList_DL(0, 0, 0, "", 1, -1);
        ////    }
        ////    catch
        ////    {
        ////        throw;
        ////    }
        ////}


        public DataTable SearchVessel(string searchtext, int? fleet_id, int? vessel_id, int? vessel_manager, int? vessel_Flag, int? user_company_id, int? is_vessel, int? Vessel_Type
             , string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objDAL.SearchVessel(searchtext, fleet_id, vessel_id, vessel_manager, vessel_Flag, user_company_id, is_vessel, Vessel_Type
                                           , sortby, sortdirection, pagenumber, pagesize, ref  isfetchcount);

        }

        public DataTable Search_SURVEY_Vessel(string searchtext, int? fleet_id, int? vessel_id, int? vessel_manager, int? vessel_Flag, int? user_company_id, int? is_vessel, int? Vessel_Type
           , string sortby, int? sortdirection, int? pagenumber, int? pagesize, string CompanyID, ref int isfetchcount)
        {

            return objDAL.Search_SURVEY_Vessel(searchtext, fleet_id, vessel_id, vessel_manager, vessel_Flag, user_company_id, is_vessel, Vessel_Type
                                           , sortby, sortdirection, pagenumber, pagesize, CompanyID, ref  isfetchcount);

        }

        public DataTable SearchFleet(string searchtext, int? vessel_manager, int? vessel_owner, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {

            return objDAL.SearchFleet(searchtext, vessel_manager, vessel_owner, sortby, sortdirection, pagenumber, pagesize, ref  isfetchcount);

        }

        public DataTable Search_SURVEY_Fleet(string searchtext, int? vessel_manager, int? vessel_owner, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount, string companyID)
        {

            return objDAL.Search_SURVEY_Fleet(searchtext, vessel_manager, vessel_owner, sortby, sortdirection, pagenumber, pagesize, ref  isfetchcount, companyID);

        }

        public DataTable Get_VesselList(DataTable FleetIDList, int VesselID, int VesselManager, string SearchText, int UserCompanyID)
        {
            try
            {
                return objDAL.Get_VesselList_DL(FleetIDList, VesselID, VesselManager, SearchText, UserCompanyID, -1);
            }
            catch
            {
                throw;
            }
        }

        //public DataTable Get_VesselList(int? FleetID, int VesselID, int VesselManager, string SearchText, int UserCompanyID)
        //{
        //    try
        //    {
        //        return objDAL.Get_VesselList_DL(FleetID, VesselID, VesselManager, SearchText, UserCompanyID, -1);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        public DataTable Get_VesselList(int FleetID, int VesselID, int VesselManager, string SearchText, int UserCompanyID)
        {
            try
            {
                return objDAL.Get_VesselList_DL(FleetID, VesselID, VesselManager, SearchText, UserCompanyID, -1);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_SURVEY_VesselList(int FleetID, int VesselID, int VesselManager, string SearchText, int UserCompanyID)
        {
            try
            {
                return objDAL.Get_SURVEY_VesselList_DL(FleetID, VesselID, VesselManager, SearchText, UserCompanyID, -1);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_VesselListPreJoining(int UserCompanyID)
        {
            try
            {
                return objDAL.Get_VesselListPreJoining_DL(UserCompanyID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_VesselList(int FleetID, int VesselID, int VesselManager, string SearchText, int UserCompanyID, int IsVessel)
        {
            try
            {
                return objDAL.Get_VesselList_DL(FleetID, VesselID, VesselManager, SearchText, UserCompanyID, IsVessel);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_UserVesselList_DL(int FleetID, int VesselID, int VesselManager, string SearchText, int UserCompanyID, int? UserID)
        {
            try
            {
                return objDAL.Get_UserVesselList_DL(FleetID, VesselID, VesselManager, SearchText, UserCompanyID, -1, UserID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetVesselDetails_ByID(int Vessel_ID)
        {
            try
            {
                return objDAL.GetVesselDetails_ByID_DL(Vessel_ID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetSURVEYVesselDetails_ByID(int Vessel_ID)
        {
            try
            {
                return objDAL.Get_SURVEY_VesselDetails_ByID_DL(Vessel_ID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetFleetList()
        {
            try
            {
                return objDAL.GetFleetList_DL();
            }
            catch
            {
                throw;
            }
        }


        public DataTable GetFleetList_ByID(int? FleetID, int? CompanyID)
        {


            return objDAL.GetFleetList_ByID_DL(FleetID, CompanyID);

        }


        public DataTable GetFleetList(int UserCompanyID)
        {
            try
            {
                return objDAL.GetFleetList_DL(UserCompanyID);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetFleetList(int UserCompanyID, int VesselManager)
        {
            try
            {
                return objDAL.GetFleetList_DL(UserCompanyID, VesselManager);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetVesselsByFleetID(int FleetID)
        {
            try
            {
                return objDAL.Get_VesselList_DL(FleetID, 0, 1, "", 1, -1);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetVesselsByFleetID(int FleetID, int IsVessel)
        {
            try
            {
                return objDAL.Get_VesselList_DL(FleetID, 0, 1, "", 1, IsVessel);
            }
            catch
            {
                throw;
            }
        }
        //public DataTable GetVesselSearchData(int flag, int vessel, int Mngby, int Fleet, string VesselValue, string FleetCode, string MgdByValue)
        //{
        //    try
        //    {
        //        return objDAL.GetVesselSearchData_DL(flag, vessel, Mngby, Fleet, VesselValue, FleetCode, MgdByValue);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public DataSet ExecuteQuery(string strSQL)
        {
            try
            {

                return objDAL.ExecuteQuery_DL(strSQL);
            }
            catch
            {
                throw;
            }
        }

        public void DeleteVessel(int Vessel_ID)
        {
            try
            {
                objDAL.DeleteVessel_DL(Vessel_ID);
            }
            catch
            {
                throw;
            }
        }

        public void DeleteVessel(string Vessel_Code)
        {
            try
            {
                objDAL.DeleteVessel_DL(Vessel_Code);
            }
            catch
            {
                throw;
            }
        }

        public void DeleteVessel(int Vessel_ID, int Deleted_By)
        {
            try
            {
                objDAL.DeleteVessel_DL(Vessel_ID, Deleted_By);
            }
            catch
            {
                throw;
            }
        }

        public void AddVesselDetails(string ExName1, string ExName2, string ExName3, string ExName4, string ShortName, int VesselCode, string Owner, string Operator, string Flag, string Callsign, int imono, int OfcNo, string classname, string classno, float Servspeed, string vesseltype, string size,
                                  string KeelDt, string DlvryDt, string VesselYard, int hullno, string Hullltype, float LengthOA, float LengthBp, float Depth, float Breadth, float MastKeel, int MMISNo, float CargoTkCap, float SlopTkCap, float BallastTkCap, float Dw_Trop, float Dw_summ, float Dw_wint, float Dw_ballast,
                                  float Disp_trop, float Disp_summ, float Disp_wint, float Disp_Ballast, float Drft_Trop, float Drft_summ, float Drft_wint, float Drft_ballast, float grt_inter, float grt_suez, float grt_panama, float nrt_inter, float nrt_suez, float nrt_panam, float lwt_inter, float lwt_suez, float lwt_panama,
                                  string MEngine, string Aux_boiler, string Ablr_Cap, string ME_MCR, string ME_NCR, string Cop_Cap, string Aux_Engine, string Deck_Mech, string AE_Kw, string turb_Gent, string TG_Kw, string Dry_Last, string Dry_Next, string Dry_Latest, string Spl_Last, string Spl_Next, string Spl_Latest,
                                  string Tail_Last, string Tail_Next, string Tail_Latest, string shipImage, string TankImage, int flag, string Email)
        {
            try
            {

                objDAL.AddVesselDetails_DL(ExName1, ExName2, ExName3, ExName4, ShortName, VesselCode, Owner, Operator, Flag, Callsign, imono, OfcNo, classname, classno, Servspeed, vesseltype, size, KeelDt, DlvryDt, VesselYard, hullno, Hullltype, LengthOA, LengthBp, Depth, Breadth, MastKeel, MMISNo, CargoTkCap, SlopTkCap, BallastTkCap, Dw_Trop, Dw_summ, Dw_wint, Dw_ballast,
                               Disp_trop, Disp_summ, Disp_wint, Disp_Ballast, Drft_Trop, Drft_summ, Drft_wint, Drft_ballast, grt_inter, grt_suez, grt_panama, nrt_inter, nrt_suez, nrt_panam, lwt_inter, lwt_suez, lwt_panama, MEngine, Aux_boiler, Ablr_Cap, ME_MCR, ME_NCR, Cop_Cap, Aux_Engine, Deck_Mech, AE_Kw, turb_Gent, TG_Kw, Dry_Last, Dry_Next, Dry_Latest, Spl_Last, Spl_Next, Spl_Latest,
                               Tail_Last, Tail_Next, Tail_Latest, shipImage, TankImage, flag, Email);
            }
            catch
            {
                throw;
            }
        }

        public DataSet GetInmarsatValues_ForVesselID(int Vessel_ID)
        {
            try
            {
                return objDAL.GetInmarsatValues_ForVesselID_DL(Vessel_ID);
            }
            catch
            {
                throw;
            }

        }

        public DataTable GetVesselLocations()
        {
            return objDAL.GetVesselLocations_DL();
        }

        public DataTable Get_VesselTypeList()
        {
            return objDAL.Get_VesselTypeList_DL();
        }

        public string Get_VesselCode_ByID(int Vessel_ID)
        {
            try
            {
                return objDAL.Get_VesselCode_ByID_DL(Vessel_ID);
            }
            catch
            {
                throw;
            }
        }
        public int Get_VesselID_ByCode(string Vessel_Short_Code)
        {
            try
            {
                string Ret = objDAL.Get_VesselID_ByCode_DL(Vessel_Short_Code);
                if (Ret != "")
                    return int.Parse(Ret);
                else
                    return 0;
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_VesselType()
        {
            try
            {
                return objDAL.Get_VesselType_DL();
            }
            catch
            {
                throw;
            }
        }
        public int INSERT_New_Vessel(string Vessel_Code, string Vessel_Name, string Vessel_Short_Name, string MailID, int Fleet_Code, int VesselOwner
            , DateTime? Takeoverdate, DateTime? HandoverDate, int? VesselFlag, int CreatedBy, decimal? min_CTM, string Odm_Enabled, string FileName, int? vesselType_Add)
        {
            try
            {
                return objDAL.INSERT_New_Vessel_DL(Vessel_Code, Vessel_Name, Vessel_Short_Name, MailID, Fleet_Code, VesselOwner, Takeoverdate, HandoverDate, VesselFlag, CreatedBy, min_CTM, Odm_Enabled, FileName, vesselType_Add);
            }
            catch
            {
                throw;
            }

        }
        public int INSERT_New_Vessel_WithImageName(string Vessel_Code, string Vessel_Name, string Vessel_Short_Name, string MailID, int Fleet_Code, int VesselOwner
            , DateTime? Takeoverdate, DateTime? HandoverDate, int? VesselFlag, int CreatedBy, decimal? min_CTM, string Odm_Enabled, string FileName, string VeselImgPath, int? vesselType_Add, Boolean IsVessel, string IMO_NO, string Call_Sign)
        {
            try
            {
                return objDAL.INSERT_New_Vessel_DL_WithImageName(Vessel_Code, Vessel_Name, Vessel_Short_Name, MailID, Fleet_Code, VesselOwner, Takeoverdate, HandoverDate, VesselFlag, CreatedBy, min_CTM, Odm_Enabled, FileName, VeselImgPath, vesselType_Add, IsVessel, IMO_NO, Call_Sign);
            }
            catch
            {
                throw;
            }

        }

        public int INSERT_New_SURVEY_VesselDetails(string Vessel_ID, string Vessel_Call_sign, string Vessel_IMO_No, string Vessel_Length_OA, string Vessel_MMSI_No, DateTime? yearBuilt)
        {
            try
            {
                return objDAL.INSERT_New_SURVEY_VesselDetails_DL(Vessel_ID, Vessel_Call_sign, Vessel_IMO_No, Vessel_Length_OA, Vessel_MMSI_No, yearBuilt);
            }
            catch
            {
                throw;
            }

        }
        public int UPDATE_Vessel(int Vessel_ID, string Vessel_Code, string Vessel_Name, string Vessel_Short_Name, string MailID, int? Fleet_Code, int? VesselManager
           , DateTime? Takeoverdate, DateTime? HandoverDate, int? VesselFlag, int CreatedBy, decimal? min_CTM, string Odm_Enabled, string FileName, int? vesselType_Add)
        {
            try
            {
                return objDAL.UPDATE_Vessel_DL(Vessel_ID, Vessel_Code, Vessel_Name, Vessel_Short_Name, MailID, Fleet_Code, VesselManager, Takeoverdate, HandoverDate, VesselFlag, CreatedBy, min_CTM, Odm_Enabled, FileName, vesselType_Add);
            }
            catch
            {
                throw;
            }

        }

        public int UPDATE_Vessel_WithImageName(int Vessel_ID, string Vessel_Code, string Vessel_Name, string Vessel_Short_Name, string MailID, int? Fleet_Code, int? VesselManager
            , DateTime? Takeoverdate, DateTime? HandoverDate, int? VesselFlag, int CreatedBy, decimal? min_CTM, string Odm_Enabled, string FileName, string VesselImgName, int? vesselType_Add, Boolean IsVessel, string IMO_NO, string Call_Sign)
        {
            try
            {
                return objDAL.UPDATE_Vessel_WithImageName(Vessel_ID, Vessel_Code, Vessel_Name, Vessel_Short_Name, MailID, Fleet_Code, VesselManager, Takeoverdate, HandoverDate, VesselFlag, CreatedBy, min_CTM, Odm_Enabled, FileName, VesselImgName, vesselType_Add, IsVessel, IMO_NO, Call_Sign);
            }
            catch
            {
                throw;
            }

        }

        public int UPDATE_SURVEY_VesselDetails(string Vessel_ID, string Vessel_Call_sign, string Vessel_IMO_No, string Vessel_Length_OA, string Vessel_MMSI_No, DateTime? YearBuilt)
        {
            try
            {
                return objDAL.UPDATE_SURVEY_VesselDetails_DL(Vessel_ID, Vessel_Call_sign, Vessel_IMO_No, Vessel_Length_OA, Vessel_MMSI_No, YearBuilt);
            }
            catch
            {
                throw;
            }

        }


        public int Delete_MEPowerCurveAttachment(int Vessel_ID, int CreatedBy)
        {
            return objDAL.Delete_MEPowerCurveAttachment(Vessel_ID, CreatedBy);
        }



        public int INSERT_New_Fleet(string FleetName, int VesselManager, string suptdEmail, string TechTeamEmail, int Created_By)
        {
            try
            {
                return objDAL.INSERT_New_Fleet_DL(FleetName, VesselManager, suptdEmail, TechTeamEmail, Created_By);
            }
            catch
            {
                throw;
            }
        }

        public int Update_Fleet(int FleetID, string FleetName, int VesselManager, string suptdEmail, string TechTeamEmail, int Created_By)
        {
            return objDAL.Update_Fleet_DL(FleetID, FleetName, VesselManager, suptdEmail, TechTeamEmail, Created_By);
        }

        public int Delete_Fleet(int FleetID, int Created_By)
        {
            return objDAL.Delete_Fleet_DL(FleetID, Created_By);
        }

        public DataTable Get_VesselFlagList(int UserCompanyID)
        {
            return objDAL.Get_VesselFlagList_DL(UserCompanyID);
        }

        public DataTable Get_Survey_VesselFlagList()//int UserCompanyID)
        {
            return objDAL.Get_Survey_VesselFlagList_DL();
        }


        public DataTable Get_VesselFlagDetails(int Vessel_Flag, int UserCompanyID)
        {
            return objDAL.Get_VesselFlagDetails_DL(Vessel_Flag, UserCompanyID);
        }

        public DataTable Get_VesselManagerList()
        {
            try
            {
                return objDAL.Get_VesselManagerList_DL();
            }
            catch
            {
                throw;
            }
        }

        public DataTable Get_VesselInfo_VID(int Vessel_ID, string Vessel_Code, int UserID)
        {
            try
            {
                return objDAL.Get_VesselInfo_VID_DL(Vessel_ID, Vessel_Code, UserID);
            }
            catch
            {
                throw;
            }
        }


        public int Delete_VesselImageAttachment(int Vessel_ID, int Created_By)
        {
            try
            {
                return objDAL.Delete_VesselImageAttachment(Vessel_ID, Created_By);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Get_JiBePacketStatus_Search(int? VesselCode, int? Status, int CompanyID, DateTime? Start_Date, DateTime? End_Date, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
        {
            try
            {
                return objDAL.Get_JiBePacketStatus_Search_DL(VesselCode, Status, CompanyID, Start_Date, End_Date, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
            }
            catch
            {
                throw;
            }
        }
        public DataSet Get_JiBeBuildAssemblies()
        {
            try
            {
                return objDAL.Get_JiBeBuildAssemblies_DL();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Rertive Vessel list according to User Veseel Assigment
        /// </summary>
        /// 

        public DataTable Get_UserVesselList_Search(int FleetID, int VesselID, int VesselManager, string SearchText, int UserCompanyID, int UserID)
        {
            try
            {
                return objDAL.Get_UserVesselList_Search_DL(FleetID, VesselID, VesselManager, SearchText, UserCompanyID, -1, UserID);
            }
            catch (Exception ex)
            {
                 throw;
               
            }
        }

        public DataTable Get_JiBeDeltaLicenseKeyRequest(int Vessel_Owner)
        {
            try
            {
                return objDAL.Get_JiBeDeltaLicenseKeyRequest_DL(Vessel_Owner);
            }
            catch 
            {
                 throw;
               
            }
        }

        public int Upd_JiBeDeltaSyncFlag(int Vessel_ID)
        {
            try
            {
                return objDAL.Upd_JiBeDeltaSyncFlag_DL(Vessel_ID);
            }
            catch 
            {
                 throw;
               
            }
        }

        public int Upd_JiBeDeltaAutoSyncFlag(int Vessel_ID, string SyncConn_String)
        {
            try
            {
                return objDAL.Upd_JiBeDeltaAutoSyncFlag_DL(Vessel_ID, SyncConn_String);
            }
            catch
            {
                 throw;
               
            }
        }

        public string Upd_JiBeDeltaLicenseKey(int Vessel_ID, string AutorizedKey)
        {
            string LicenseKey = "";
            try
            {
                string Passphrase = "JiBEShip";
                if (AutorizedKey != null || AutorizedKey != string.Empty)
                    LicenseKey = DES_Encrypt_Decrypt.EncryptString(AutorizedKey, Passphrase);
                // insert in data_log table
                return objDAL.Upd_JiBeDeltaLicenseKey_DL(Vessel_ID, LicenseKey);
            }
            catch 
            {
                 throw;
               
            }
        }

        public DataTable Get_JibeDeltaDllAcknowledgement(int Vessel_ID)
        {
            try
            {
                return objDAL.Get_JibeDeltaDllAcknowledgement_DL(Vessel_ID);
            }
            catch 
            {
                 throw;
              
            }
        }

        
        // in case of table schema change re send data to vessel
        public Boolean Upd_JibeDeltaData(int Vessel_ID, DateTime SetupCreationDate)
        {
            try
            {
                if(objDAL.Upd_JibeDeltaData(Vessel_ID, SetupCreationDate)!=1)
                    return false;
                     else
                    return true;
            }
            catch
            {
                throw;

            }
        }

        //
        public Boolean Put_VesselReleaseFileToJibe(string FileName, byte[] fileData)
        {
            try
            {
                System.IO.FileInfo file = new System.IO.FileInfo(FileName);
                file.Directory.Create();
                System.IO.File.WriteAllBytes(FileName, fileData);
                return true;
            }
            catch 
            {
                 throw;
               
            }
        }

        //Description: Method user to Sync SQL to vessel after verification from JIT End
        public void SYNC_SQlQueryToVessels(string TableName, string PKID, string PKValue, int Vessel_ID, string strQuery, string PK_NAMES_PR, string PK_VALUES_PR)
        {
            try
            {
                objDAL.SYNC_SQlQueryToVessels_DL(TableName, PKID, PKValue, Vessel_ID, strQuery, PK_NAMES_PR, PK_VALUES_PR);
            }
            catch 
            {
                 throw;
            }
        }

        //Get incremental data for sql execution log 
        public DataTable GET_Delta_SQL_Script_Log(int Vessel_ID, int SqlVersion)
        {
            try
            {
                return objDAL.GET_Delta_SQL_Script_Log_DL(Vessel_ID,SqlVersion);
            }
            catch 
            {
                 throw;
            }
        }

        // Description Method used to send script details to the JIT        
        public DataTable GET_SQL_Script_Log()
        {
            try
            {
                return objDAL.GET_SQL_Script_Log_DL();
            }
            catch 
            {
                 throw;
                
            }
        }

        // Description Method used to send DLL log details to the CRM    
        public DataTable GET_Delta_DLL_Log(int Vessel_ID, int LastID)
        {
            try
            {
                return objDAL.GET_Delta_DLL_Log_DL(Vessel_ID, LastID);
            }
            catch 
            {
                 throw;
                
            }
        }
    }
}
