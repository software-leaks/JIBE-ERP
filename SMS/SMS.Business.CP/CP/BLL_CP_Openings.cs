using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Web;


using SMS.Data.CP;


namespace SMS.Business.CP
{
    public class BLL_CP_Openings
    {

        DAL_CP_Openings objDALOpenings = new DAL_CP_Openings();

        public int Ins_Opening_Updates(int? Opening_ID, int? Vessel_ID, string Entry_Type, string Progress_Remarks, string Charterer_Name, string Broker_Name, string Contact_Email, string Contact_Mobile, string Contact_Name, string Opening, int Created_By)
        {
            return objDALOpenings.Ins_Opening_Updates(Opening_ID, Vessel_ID, Entry_Type, Progress_Remarks,Charterer_Name,  Broker_Name,  Contact_Email,  Contact_Mobile,  Contact_Name,  Opening, Created_By);
        }

        public int Ins_Opening(int Created_By)
        {
            return objDALOpenings.Ins_Opening(Created_By);
        }

        public DataSet Get_OpeningDetails(int? Opening_ID, int? Vessel_Id)
        {
            return objDALOpenings.Get_OpeningDetails(Opening_ID, Vessel_Id);
        }
      public DataTable GetOpeningList(DataTable StatusList, int? pagenumber, int? pagesize, int? Vessel_Id,  ref int isfetchcount)
      {
          return objDALOpenings.GetOpeningList(StatusList, pagenumber, pagesize, Vessel_Id, ref isfetchcount);
      }

      public int DEL_Opening_Update(int? Created_By, int? Progress_ID)
      {
          return objDALOpenings.DEL_Opening_Update(Progress_ID, Created_By);
      }
      public DataTable GetVesselListAll(int? UserCompanyID)
      {
          return objDALOpenings.GetVesselListAll(UserCompanyID);
      }

       
        
    }
}
