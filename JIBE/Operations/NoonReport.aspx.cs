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


public partial class Operation_NoonReport : System.Web.UI.Page
{

    public static DataTable dtCPROB;
    int PKID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {


                if (Request.QueryString["LastNoon"] != null)
                {
                    string Vessel_Code = Request.QueryString["LastNoon"].ToString();
                    DataTable dt = BLL_OPS_VoyageReports.Get_LatestNoonReport(Vessel_Code);

                    if (dt.Rows.Count > 0)
                        PKID = int.Parse(dt.Rows[0]["PKID"].ToString());

                }
                else
                {
                    PKID = UDFLib.ConvertToInteger(Request.QueryString["id"].ToString());
                    //string[] filters = Request.QueryString["filters"].Split('~');
                    //DataTable dtReports = BLL_OPS_VoyageReports.Get_VoyageReportIndex("N", Convert.ToInt32(filters[1]), Convert.ToInt32(filters[2]), filters[3], filters[4], Convert.ToInt32(filters[5]));
                    //dtReports.PrimaryKey = new DataColumn[] { dtReports.Columns["pkid"] };
                    //ctlRecordNavigationReport.InitRecords(dtReports);
                    //ctlRecordNavigationReport.CurrentIndex = dtReports.Rows.IndexOf(dtReports.Rows.Find(PKID));

                }
                dtCPROB = BLL_OPS_VoyageReports.OPS_Get_RecentCPROB();
                BindReport(null);
            }
        }
        catch
        {
        }
    }

    protected void BindReport(DataRow dr)
    {
        PKID = dr != null ? Convert.ToInt32(dr["pkid"]) : Convert.ToInt32(Request.QueryString["id"]);

        if (PKID != 0)
        {

            // = BLL_OPS_VoyageReports.Get_DailyNoonReport(PKID);
            
            DataTable dtNoon = BLL_OPS_VoyageReports.Get_DailyNoonReport(PKID);
           ViewState["dtVesselROB"]= dtNoon.Copy();
            lblVessel.Text = Convert.ToString(dtNoon.Rows[0]["vesselname"]);
            hplcrewlist.NavigateUrl = "~/crew/CrewList_Print.aspx?vcode="+Convert.ToString(dtNoon.Rows[0]["Vessel_Short_Name"]);

            fvnoonreport.DataSource = dtNoon;
            fvnoonreport.DataBind();
            Page.Title = dtNoon.Rows[0]["Vessel_Short_Name"].ToString() + " / NOON / " + dtNoon.Rows[0]["Report_Date"].ToString();
        }

    }

    protected void fvnoonreport_DataBound(object sender, EventArgs e)
    {
        FormView fv = (FormView)sender;
        try
        {
            string UTC_TYPE = ((DataTable)fv.DataSource).Rows[0]["UTC_TYPE"].ToString().Trim();
            string strUTC_hr = ((DataTable)fv.DataSource).Rows[0]["UTC_hr"].ToString().Trim();
            double UTC = 0;
            double UTC_hr = 0;
            if (strUTC_hr != "")
            {
                UTC_hr = double.Parse(strUTC_hr);
                UTC = UTC_hr;

            }

            if (UTC_TYPE == "+")
            {
                UTC_hr = UTC_hr < 1 ? 11 : 12 - UTC_hr;
                ((Label)fv.FindControl("lblUtchrs")).Text = UTC < 0 ? (UTC_hr.ToString().Length < 2 ? "0" + UTC_hr.ToString() : UTC_hr.ToString()) + "30 Hrs UTC" : (UTC_hr.ToString().Length < 2 ? "0" + UTC_hr.ToString() : UTC_hr.ToString()) + "00 Hrs UTC";
            }
            else if (UTC_TYPE == "-")
            {
                UTC_hr = UTC_hr < 1 ? 12 : 12 + UTC_hr;
                ((Label)fv.FindControl("lblUtchrs")).Text = UTC < 0 ? (UTC_hr.ToString().Length < 2 ? "0" + UTC_hr.ToString() : UTC_hr.ToString()) + "30 Hrs UTC" : (UTC_hr.ToString().Length < 2 ? "0" + UTC_hr.ToString() : UTC_hr.ToString()) + "00 Hrs UTC";
            }

            string wind = ((Label)fv.FindControl("lblwindforce")).Text;
            string stravgspeed = ((Label)fv.FindControl("lblaveragespeed")).Text;
            string strinsspeed = DataBinder.Eval(fv.DataItem, "INSTRUCTED_SPEED").ToString();

            double wforce = 0.0;
            if (double.TryParse(wind, out wforce))
            {
                if (wforce >= 5)
                {
                    ((Label)fv.FindControl("lblwindforce")).CssClass = "RedCell";
                }
            }


            double avgspeed = 0.0;
            double insspeed = 0.0;

            if (double.TryParse(stravgspeed, out avgspeed) && double.TryParse(strinsspeed, out insspeed))
            {

                if (avgspeed < insspeed)
                {
                    ((Label)fv.FindControl("lblaveragespeed")).CssClass = "RedCell";
                }

            }

        }
        catch (Exception ex)
        {
        }

        #region check for low ROB




        string strVSLflt = "telegram_type= 'N'";

        DataRow[] drVSLROB = (ViewState["dtVesselROB"] as DataTable).Select(strVSLflt);

        string strCPFlt = "datatype like '%ROB%' and Vessel_id=" + drVSLROB[0]["vessel_id"].ToString();

        DataRow[] drCPROB = dtCPROB.Select(strCPFlt);

        foreach (DataRow dr in drCPROB)
        {
            string DataCode = dr["Data_Code"].ToString().ToUpper().Trim();
            decimal datavalue = decimal.Parse(dr["data_value"].ToString());

            if (DataCode == "HO_ROB")
            {
                if (UDFLib.ConvertToDecimal(drVSLROB[0]["HO_ROB"].ToString()) < datavalue)
                {
                    ((Label)fvnoonreport.FindControl("lblHO")).CssClass = "RedCell";
                }
            }
            else if (DataCode == "DO_ROB")
            {
                if (UDFLib.ConvertToDecimal(drVSLROB[0]["DO_ROB"].ToString()) < datavalue)
                {
                    ((Label)fvnoonreport.FindControl("lblDO")).CssClass = "RedCell";
                }

            }

            else if (DataCode == "AECC_ROB")
            {
                if (UDFLib.ConvertToDecimal(drVSLROB[0]["AECC_ROB"].ToString()) < datavalue)
                {
                    ((Label)fvnoonreport.FindControl("lblAECC")).CssClass = "RedCell";
                }

            }
            else if (DataCode == "MECC_ROB")
            {
                if (UDFLib.ConvertToDecimal(drVSLROB[0]["MECC_ROB"].ToString()) < datavalue)
                {
                    ((Label)fvnoonreport.FindControl("lblMECC")).CssClass = "RedCell";
                }

            }

            else if (DataCode == "MECYL_ROB")
            {
                if (UDFLib.ConvertToDecimal(drVSLROB[0]["MECYL_ROB"].ToString()) < datavalue)
                {
                    /*JIT_2648 LOW_ROB COLOR NOT SETTING TO CONTROL due to wrong control name.*/
                    ((Label)fvnoonreport.FindControl("lblMECYL")).CssClass = "RedCell";
                    //((Label)fvnoonreport.FindControl("lblMECTL")).CssClass = "RedCell";
                    
                }

            }
            else if (DataCode == "FW_ROB")
            {
                if (UDFLib.ConvertToDecimal(drVSLROB[0]["FW_ROB"].ToString()) < datavalue)
                {
                    ((Label)fvnoonreport.FindControl("lblFW")).CssClass = "RedCell";
                }

            }




        }



        #endregion

        #region check for high consumption


        decimal CPHO = UDFLib.ConvertToDecimal(((DataTable)fv.DataSource).Rows[0]["calCPHO"].ToString().Trim());
        decimal MEHO = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblME_HO")).Text);
        decimal AEHO = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblAE_HO")).Text);
        decimal BlrHO = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblBlr_HO")).Text);

        decimal CPDO = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblCPDO")).Text);
        decimal MEDO = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblME_DO")).Text);
        decimal AEDO = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblAE_DO")).Text);
        decimal BlrDO = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblBlr_DO")).Text);

        decimal CPLSFO = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblCPLSFO")).Text);
        decimal MELSFO = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblME_LSFO")).Text);
        decimal AELSFO = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblAE_LSFO")).Text);
        decimal BlrLSFO = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblBlr_LSFO")).Text);

        decimal CPLSMGO = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblCPLSMGO")).Text);
        decimal MELSMGO = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblME_LSMGO")).Text);
        decimal AELSMGO = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblAE_LSMGO")).Text);
        decimal BlrLSMGO = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblBlr_LSMGO")).Text);

        decimal CPFW = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblCPFW")).Text);
        decimal FW_Cons = UDFLib.ConvertToDecimal(((Label)fv.FindControl("lblFW_Cons")).Text);

        if (CPHO < (MEHO + AEHO + BlrHO))
        {
            ((Label)fvnoonreport.FindControl("lblME_HO")).CssClass = "RedCell";
            ((Label)fvnoonreport.FindControl("lblAE_HO")).CssClass = "RedCell";
            ((Label)fvnoonreport.FindControl("lblBlr_HO")).CssClass = "RedCell";
        }
        if (CPDO < MEDO + AEDO + BlrDO)
        {
            ((Label)fvnoonreport.FindControl("lblME_DO")).CssClass = "RedCell";
            ((Label)fvnoonreport.FindControl("lblAE_DO")).CssClass = "RedCell";
            ((Label)fvnoonreport.FindControl("lblBlr_DO")).CssClass = "RedCell";
        }    

        if (CPLSFO < (MELSFO + AELSFO + BlrLSFO))
        {
            ((Label)fvnoonreport.FindControl("lblME_LSFO")).CssClass = "RedCell";
            ((Label)fvnoonreport.FindControl("lblAE_LSFO")).CssClass = "RedCell";
            ((Label)fvnoonreport.FindControl("lblBlr_LSFO")).CssClass = "RedCell";
        }
        if (CPLSMGO < MELSMGO + AELSMGO + BlrLSMGO)
        {
            ((Label)fvnoonreport.FindControl("lblME_LSMGO")).CssClass = "RedCell";
            ((Label)fvnoonreport.FindControl("lblAE_LSMGO")).CssClass = "RedCell";
            ((Label)fvnoonreport.FindControl("lblBlr_LSMGO")).CssClass = "RedCell";
        }
        if (CPFW < FW_Cons)
        {
            ((Label)fvnoonreport.FindControl("lblFW_Cons")).CssClass = "RedCell";
        }


        #endregion



    }

    //protected void lbtncrwlist_Click(object s, EventArgs e)
    //{
    //    ResponseHelper.Redirect("~/crew/CrewList_Print.aspx?vcode=" + ((LinkButton)s).CommandArgument, "blank", "");
    //}


}
