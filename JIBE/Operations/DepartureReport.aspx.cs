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
using SMS.Business.Operation;

public partial class Operation_DepartureReport : System.Web.UI.Page
{
   
    public static DataTable dtCPROB;
    public static DataTable dtVesselInfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] != null)
            {            
                
                dtCPROB = BLL_OPS_VoyageReports.OPS_Get_RecentCPROB();
                BindReport(null);
            }
        }
        catch (Exception ex)
        {
        }

    }

    protected void BindReport(DataRow dr)
    {
       int PKID = dr != null ? Convert.ToInt32(dr["pkid"]) : Convert.ToInt32(Request.QueryString["id"]);

        if (PKID != 0)
        {
            DataTable dtDep = BLL_OPS_VoyageReports.Get_DepartureReport(PKID);
            ViewState["dtVesselROB"] = dtDep.Copy();
            fvdepature.DataSource = dtDep;
            fvdepature.DataBind();
            lblVessel.Text = Convert.ToString(dtDep.Rows[0]["vesselname"]);
            lbtncrwlist.CommandArgument = Convert.ToString(dtDep.Rows[0]["Vessel_Short_Name"]);

            if (dtDep.Rows.Count > 0)
            {
                dtVesselInfo = BLL_OPS_VoyageReports.OPS_Get_VesselInfo(UDFLib.ConvertToInteger(dtDep.Rows[0]["Vessel_ID"]));
                if (dtVesselInfo.Rows.Count > 0)
                {
                    lblIMONO.Text = Convert.ToString(dtVesselInfo.Rows[0]["Vessel_IMO_No"]);
                    lblCallSign.Text = Convert.ToString(dtVesselInfo.Rows[0]["Vessel_Call_sign"]);
                }
                if (dtDep.Rows[0]["LO_Offlanded"].ToString() == "YES")
                {
                    DataTable dtLOSamples = BLL_OPS_VoyageReports.Get_DepartureReport_LOSamples(Int32.Parse(Request.QueryString["id"].ToString()), UDFLib.ConvertToInteger(dtDep.Rows[0]["Vessel_ID"]));
                    Repeater rptLOSamples = (Repeater)fvdepature.FindControl("rptLOSamples");
                    if (rptLOSamples != null)
                    {
                        rptLOSamples.DataSource = dtLOSamples;
                        rptLOSamples.DataBind();
                    }
                }
            }

            Page.Title = dtDep.Rows[0]["Vessel_Short_Name"].ToString() + " / DEP / " + dtDep.Rows[0]["Report_Date"].ToString();
        }

    }

    protected void fvdepature_DataBound(object sender, EventArgs e)
    {
        #region check for low ROB




        string strVSLflt = "telegram_type= 'D'";

        DataRow[] drVSLROB = (ViewState["dtVesselROB"] as DataTable).Select(strVSLflt);

        string strCPFlt = "datatype like '%ROB%' and Vessel_id=" + drVSLROB[0]["vessel_id"].ToString();

        DataRow[] drCPROB = dtCPROB.Select(strCPFlt);

        foreach (DataRow dr in drCPROB)
        {
            string DataCode = dr["Data_Code"].ToString().ToUpper().Trim();
            decimal datavalue = decimal.Parse(dr["data_value"].ToString());

            if (DataCode == "HO_ROB")
            {
                if (decimal.Parse(drVSLROB[0]["HO_ROB"].ToString()) < datavalue)
                {
                    ((Label)fvdepature.FindControl("lblHO")).CssClass = "RedCell";
                }
            }
            else if (DataCode == "DO_ROB")
            {
                if (decimal.Parse(drVSLROB[0]["DO_ROB"].ToString()) < datavalue)
                {
                    ((Label)fvdepature.FindControl("lblDO")).CssClass = "RedCell";
                }

            }

            else if (DataCode == "AECC_ROB")
            {
                if (decimal.Parse(drVSLROB[0]["AECC_ROB"].ToString()) < datavalue)
                {
                    ((Label)fvdepature.FindControl("lblAECC")).CssClass = "RedCell";
                }

            }
            else if (DataCode == "MECC_ROB")
            {
                if (decimal.Parse(drVSLROB[0]["MECC_ROB"].ToString()) < datavalue)
                {
                    ((Label)fvdepature.FindControl("lblMECC")).CssClass = "RedCell";
                }

            }

            else if (DataCode == "MECYL_ROB")
            {
                if (decimal.Parse(drVSLROB[0]["MECYL_ROB"].ToString()) < datavalue)
                {
                    ((Label)fvdepature.FindControl("lblMECYL")).CssClass = "RedCell";
                }

            }
            else if (DataCode == "FW_ROB")
            {
                if (decimal.Parse(drVSLROB[0]["FW_ROB"].ToString()) < datavalue)
                {
                    ((Label)fvdepature.FindControl("lblFW")).CssClass = "RedCell";
                }

            }




        }



        #endregion
    }

    protected void lbtncrwlist_Click(object s, EventArgs e)
    {
        ResponseHelper.Redirect("~/crew/CrewList_Print.aspx?vcode=" + ((LinkButton)s).CommandArgument, "blank", "");
    }
}
