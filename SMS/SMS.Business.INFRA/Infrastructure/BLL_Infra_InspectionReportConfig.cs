using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.Infrastructure;
using System.Data;
namespace SMS.Business.Infrastructure
{
    
    public class BLL_Infra_InspectionReportConfig
    {
        DAL_Infra_InspectionReportConfig objRp = new DAL_Infra_InspectionReportConfig();
        public DataSet INSP_Get_ReportConfig()
        {

            return objRp.INSP_Get_ReportConfig();
        }
        public DataSet INSP_Get_ReportConfigByKeyNo(int KeyNo)
        {
            return objRp.INSP_Get_ReportConfigByKeyNo(KeyNo);
        }
        public int INSP_Insert_ReportConfig(string KeyDescription, string KeyValue, int CreatedBy, DateTime DateOfCreation)
        {
            return objRp.INSP_Insert_ReportConfig(KeyDescription, KeyValue, CreatedBy, DateOfCreation);
        }
        public int INSP_Update_ReportConfig(int KeyNo, string KeyDescription, string KeyValue, int ModifiedBy, DateTime DateOfModification)
        {
            return objRp.INSP_Update_ReportConfig(KeyNo,KeyDescription, KeyValue, ModifiedBy, DateOfModification);
        }
        public int INSP_DeleteRestore_ReportConfig(int KeyNo, int ActiveStatus, int ModifiedBy, DateTime DateOfModification)
        {
            return objRp.INSP_DeleteRestore_ReportConfig(KeyNo, ActiveStatus, ModifiedBy, DateOfModification);
        }

        public int INSP_Update_InspectionReportConfigStaus(int KeyNo, int ActiveStatus, int ModifiedBy, DateTime DateOfModification)
        {
            return objRp.INSP_Update_InspectionReportConfigStaus(KeyNo, ActiveStatus, ModifiedBy, DateOfModification);
        }

    }
}
