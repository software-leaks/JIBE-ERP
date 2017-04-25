using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using   SMS.Business.PURC ;
using Telerik.Web.UI;

using CrystalDecisions.Shared;

public partial class Technical_INV_ReportForConsolidatedPO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                

                Int32 i32Vessel_Code = Convert.ToInt32(Session["i32Vessel_Code"]);
                string strAgent_Code = Convert.ToString(Session["strAgent_Code"]);

                using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
                {
                    DataSet dtsPoItemsList = objTechService.GetPOItemListForReport(i32Vessel_Code, strAgent_Code);

                    ConnectionInfo cInfo = new ConnectionInfo();
                    TableLogOnInfo logOnInfo = new TableLogOnInfo();
                    string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["smsconn"].ToString();
                    string[] conn = connstring.ToString().Split(';');
                    string[] serverInfo = conn[0].ToString().Split('=');
                    string[] DbInfo = conn[1].ToString().Split('=');
                    string[] userInfo = conn[2].ToString().Split('=');
                    string[] passwordInfo = conn[3].ToString().Split('=');

                    cInfo.ServerName = serverInfo[1].ToString();
                    cInfo.DatabaseName = DbInfo[1].ToString();
                    cInfo.UserID = userInfo[1].ToString();
                    cInfo.Password = passwordInfo[1].ToString();

                    CrystalReports.RptConsolidatedPO objRptConsolidatedPO = new CrystalReports.RptConsolidatedPO();

                    foreach (CrystalDecisions.CrystalReports.Engine.Table reportTable in objRptConsolidatedPO.Database.Tables)
                    {
                        logOnInfo = reportTable.LogOnInfo;
                        logOnInfo.ConnectionInfo = cInfo;
                        reportTable.ApplyLogOnInfo(logOnInfo);
                    }
                    crvConsolidatedPO.ReportSource = objRptConsolidatedPO;
                    objRptConsolidatedPO.SetDataSource(dtsPoItemsList.Tables[0]);
                  
                    crvConsolidatedPO.DisplayToolbar = true;
                }

                //}
            }
            catch (Exception ex)
            {
                //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            }
            finally
            {
                Session.Remove("ArrListOfPO_ID");
                Session.Remove("StringArrayInfoPO");
            }
        }
    }
}
