using System;
using System.Web;
using System.Data;
using System.Web.Services;
using SMS.Data.Infrastructure;
using SMS.Business.CP;
using System.Collections.Generic;
using System.Text;



public partial class CPService
{
    [WebMethod]
    public string async_GetOutstandingRemarks(int Charter_ID)
    {
        BLL_CP_CharterParty oCharterParty = new BLL_CP_CharterParty();
        return oCharterParty.Get_OutstandingRemarks(Charter_ID).Rows[0][0].ToString();
    }

}
