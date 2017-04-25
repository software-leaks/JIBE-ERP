using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using SMS.Business.INFRA;
using System.Web.Services;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.QMSDB;
using System;
using SMS.Properties;
using System.IO;
using System.Globalization;
using System.Web.UI.WebControls;
 
public partial class JibeWebService
{

    [WebMethod]
    public string asyncGet_RestHourExceptions(int RestHourID, int Vessel_ID)
    {

     
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
       

        return UDFLib.CreateHtmlTableFromDataTable(BLL_QMS_RestHours.Get_RestHours_Exceptions(RestHourID, Vessel_ID),
           new string[] { },
           new string[] { "RULE_DESCRIPTION" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left" },
           "CreateHtmlTableFromDataTable-table",
           "CreateHtmlTableFromDataTable-DataHedaer",
           "CreateHtmlTableFromDataTable-Data");
    }
}