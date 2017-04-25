using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Data;
using SMS.Data.Technical;

namespace SMS.Business.Technical
{
    public class BLL_Tec_WorklistType
    {
        public static void TEC_UPD_WORKLISTTYPE(string Worklist_Type, string Worklist_Type_Display)
        {
            DAL_Tec_WorklistType.TEC_UPD_WORKLISTTYPE(Worklist_Type, Worklist_Type_Display);

        }

        public static DataSet TEC_GET_WORKLISTYPE()
        {

            return DAL_Tec_WorklistType.TEC_GET_WORKLISTYPE();

        }
    }
}
