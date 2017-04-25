using System.Web.Services;
using System.Collections.Generic;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.Operations;
using SMS.Business.Operation;


public partial class JibeWebService
{

    [WebMethod]
    public string Get_DeckLogBook_Values_Changes(string Vessel_Id, string LogBook_Dtl_ID, string LogHours_ID, string Column_Name)
    {

        DataTable dt = BLL_OPS_DeckLog.Get_DeckLogBook_Values_Changes(UDFLib.ConvertToInteger(Vessel_Id), UDFLib.ConvertToInteger(LogBook_Dtl_ID), UDFLib.ConvertToInteger(LogHours_ID), Column_Name);

        return UDFLib.CreateHtmlTableFromDataTable(dt
                            , new string[] { "Old Value", "New Value", "PC Name", "Modified By" }
                            , new string[] { "OLD_VALUE", "NEW_VAULE", "PC_Name", "Created_By" }, "");

    }



    [WebMethod]
    public string Get_DeckLogBook_Wheel_And_Look_Values_Changes(string Vessel_Id, string LookOut_Dtl_ID, string Log_WATCH_ID, string Column_Name)
    {

        DataTable dt = BLL_OPS_DeckLog.Get_DeckLogBook_Wheel_And_Look_Values_Changes(UDFLib.ConvertToInteger(Vessel_Id), UDFLib.ConvertToInteger(LookOut_Dtl_ID), UDFLib.ConvertToInteger(Log_WATCH_ID), Column_Name);

        return UDFLib.CreateHtmlTableFromDataTable(dt
                           , new string[] { "Old Value", "New Value", "PC Name", "Modified By" }
                            , new string[] { "OLD_VALUE", "NEW_VAULE", "PC_Name", "Created_By" }, "");

    }



    [WebMethod]
    public string Get_DeckLogBook_Water_In_Hold_Values_Changes(string Vessel_Id, string WaterInHold_Dtl_ID, string Hold_Tank_ID, string Column_Name)
    {

        DataTable dt = BLL_OPS_DeckLog.Get_DeckLogBook_Water_In_Hold_Values_Changes(UDFLib.ConvertToInteger(Vessel_Id), UDFLib.ConvertToInteger(WaterInHold_Dtl_ID), UDFLib.ConvertToInteger(Hold_Tank_ID), Column_Name);

        return UDFLib.CreateHtmlTableFromDataTable(dt
                            , new string[] { "Old Value", "New Value", "PC Name", "Modified By" }
                            , new string[] { "OLD_VALUE", "NEW_VAULE", "PC_Name", "Created_By" }, "");

    }



    [WebMethod]
    public string Get_DeckLogBook_Water_In_Tank_Values_Changes(string Vessel_Id, string WaterInTank_Dtl_ID, string Hold_Tank_ID, string Column_Name)
    {

        DataTable dt = BLL_OPS_DeckLog.Get_DeckLogBook_Water_In_Tank_Values_Changes(UDFLib.ConvertToInteger(Vessel_Id), UDFLib.ConvertToInteger(WaterInTank_Dtl_ID), UDFLib.ConvertToInteger(Hold_Tank_ID), Column_Name);

        return UDFLib.CreateHtmlTableFromDataTable(dt
                          , new string[] { "Old Value", "New Value", "PC Name", "Modified By" }
                            , new string[] { "OLD_VALUE", "NEW_VAULE", "PC_Name", "Created_By" }, "");

    }

    [WebMethod]
    public string Get_Piracy_Alarm_Change_Log(int Vessel_ID)
    {
        return UDFLib.CreateHtmlTableFromDataTable(BLL_OPS_DPL.Get_Piracy_Alarm_Change_Log(Vessel_ID),
             new string[] { "Date", "Updated By", "Reasons for Status Change" },
             new string[] { "Date_Of_Creation", "Updated_By", "Remarks" },

             new string[] { "left", "left", "left" },
            "");
    }
}
