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
using System.Collections.Generic;
 

public partial class Operation_ArrivalReport : System.Web.UI.Page
{
  
    public static DataTable dtCPROB;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                dtCPROB = BLL_OPS_VoyageReports.OPS_Get_RecentCPROB();
                
            }
            if (Request.QueryString["id"] != null)
            {
                //string[] filters = Request.QueryString["filters"].Split('~');
                //DataTable dtReports = BLL_OPS_VoyageReports.Get_VoyageReportIndex("A", Convert.ToInt32(filters[1]), Convert.ToInt32(filters[2]), filters[3], filters[4], Convert.ToInt32(filters[5]));
                //dtReports.PrimaryKey = new DataColumn[] { dtReports.Columns["pkid"] };
                //ctlRecordNavigationReport.InitRecords(dtReports);
                //ctlRecordNavigationReport.CurrentIndex = dtReports.Rows.IndexOf(dtReports.Rows.Find(Int32.Parse(Request.QueryString["id"])));

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

          // ViewState["dtVesselROB"] = BLL_OPS_VoyageReports.Get_ArrivalReport(PKID);

           DataTable dtArrival = BLL_OPS_VoyageReports.Get_ArrivalReport(PKID);
           ViewState["dtVesselROB"] = dtArrival.Copy();
           lblVessel.Text = Convert.ToString(dtArrival.Rows[0]["vesselname"]);
           lbtncrwlist.CommandArgument = Convert.ToString(dtArrival.Rows[0]["Vessel_Short_Name"]);

           
            fvarrival.DataSource = dtArrival;
            fvarrival.DataBind();
            Page.Title = dtArrival.Rows[0]["Vessel_Short_Name"].ToString() + " / ARR / " + dtArrival.Rows[0]["Report_Date"].ToString();
        }

    }

    protected void fvarrival_DataBound(object sender, EventArgs e)
    {
        try
        {

            Label txtTotal_Cons_HSFO = (Label)fvarrival.FindControl("txtTotal_Cons_HSFO");
            Label txtAE_Cons_HSFO = (Label)fvarrival.FindControl("txtAE_Cons_HSFO");
            Label txtBoiler_Cons_HSFO = (Label)fvarrival.FindControl("txtBoiler_Cons_HSFO");
            Label txtME_Cons_HSFO = (Label)fvarrival.FindControl("txtME_Cons_HSFO");
            Label txtTotal_Cons_LSFO = (Label)fvarrival.FindControl("txtTotal_Cons_LSFO");
            Label txtAE_Cons_LSFO = (Label)fvarrival.FindControl("txtAE_Cons_LSFO");
            Label txtBoiler_Cons_LSFO = (Label)fvarrival.FindControl("txtBoiler_Cons_LSFO");
            Label txtME_Cons_LSFO = (Label)fvarrival.FindControl("txtME_Cons_LSFO");
            Label txtInc_Cons_HSFO = (Label)fvarrival.FindControl("txtInc_Cons_HSFO");
            Label txtInc_Cons_LSFO = (Label)fvarrival.FindControl("txtInc_Cons_LSFO");
            Label txtTotal_Cons_MGO_DO = (Label)fvarrival.FindControl("txtTotal_Cons_MGO_DO");
            Label txtAE_Cons_MGO_DO = (Label)fvarrival.FindControl("txtAE_Cons_MGO_DO");
            Label txtBoiler_Cons_MGO_DO = (Label)fvarrival.FindControl("txtBoiler_Cons_MGO_DO");
            Label txtME_Cons_MGO_DO = (Label)fvarrival.FindControl("txtME_Cons_MGO_DO");
            Label txtInc_Cons_MGO_DO = (Label)fvarrival.FindControl("txtInc_Cons_MGO_DO");













            Label txtTotal_Cons_LSMGO = (Label)fvarrival.FindControl("txtTotal_Cons_LSMGO");
            Label txtAE_Cons_LSMGO = (Label)fvarrival.FindControl("txtAE_Cons_LSMGO");
            Label txtBoiler_Cons_LSMGO = (Label)fvarrival.FindControl("txtBoiler_Cons_LSMGO");
            Label txtME_Cons_LSMGO = (Label)fvarrival.FindControl("txtME_Cons_LSMGO");
            Label txtInc_Cons_LSMGO = (Label)fvarrival.FindControl("txtInc_Cons_LSMGO");
            Label txtROB_DEP_Port_HSFO = (Label)fvarrival.FindControl("txtROB_DEP_Port_HSFO");
            Label txtROB_EOP_HSFO = (Label)fvarrival.FindControl("txtROB_EOP_HSFO");
            Label txtROB_DEP_Port_LSFO = (Label)fvarrival.FindControl("txtROB_DEP_Port_LSFO");
            Label txtROB_DEP_Port_LSMGO = (Label)fvarrival.FindControl("txtROB_DEP_Port_LSMGO");

            Label txtROB_DEP_Port_MGO_DO = (Label)fvarrival.FindControl("txtROB_DEP_Port_MGO_DO");
            Label txtROB_EOP_LSFO = (Label)fvarrival.FindControl("txtROB_EOP_LSFO");
            Label txtROB_EOP_MGO_DO = (Label)fvarrival.FindControl("txtROB_EOP_MGO_DO");
            Label txtROB_EOP_LSMGO = (Label)fvarrival.FindControl("txtROB_EOP_LSMGO");


            Label txtME_Cons_Per24_HSFO = (Label)fvarrival.FindControl("txtME_Cons_Per24_HSFO");
            Label txtME_Cons_Per24_LSFO = (Label)fvarrival.FindControl("txtME_Cons_Per24_LSFO");
            Label txtME_Cons_Per24_MGO_DO = (Label)fvarrival.FindControl("txtME_Cons_Per24_MGO_DO");
            Label txtME_Cons_Per24_LSMGO = (Label)fvarrival.FindControl("txtME_Cons_Per24_LSMGO");

            Label txtAE_Cons_Per24_HSFO = (Label)fvarrival.FindControl("txtAE_Cons_Per24_HSFO");
            Label txtAE_Cons_Per24_LSFO = (Label)fvarrival.FindControl("txtAE_Cons_Per24_LSFO");
            Label txtAE_Cons_Per24_MGO_DO = (Label)fvarrival.FindControl("txtAE_Cons_Per24_MGO_DO");
            Label txtAE_Cons_Per24_LSMGO = (Label)fvarrival.FindControl("txtAE_Cons_Per24_LSMGO");

            Label txtTotal_Manov_Cons_HSFO = (Label)fvarrival.FindControl("txtTotal_Manov_Cons_HSFO");
            Label txtTotal_Manov_Cons_LSFO = (Label)fvarrival.FindControl("txtTotal_Manov_Cons_LSFO");
            Label txtTotal_Manov_Cons_MGO_DO = (Label)fvarrival.FindControl("txtTotal_Manov_Cons_MGO_DO");
            Label txtTotal_Manov_Cons_LSMGO = (Label)fvarrival.FindControl("txtTotal_Manov_Cons_LSMGO");


            Label lblSteaming_hrs = (Label)fvarrival.FindControl("lblSteaming_hrs");
            Label lblTotal_Hrs_In_Day = (Label)fvarrival.FindControl("lblTotal_Hrs_In_Day");

            Label lblTtl_Steam_Hrs_Cop = (Label)fvarrival.FindControl("lblTtl_Steam_Hrs_Cop");
            Label lblSteaming_Dist_Till_COP = (Label)fvarrival.FindControl("lblSteaming_Dist_Till_COP");
            Label txtAvgVSpeed = (Label)fvarrival.FindControl("txtAvgVSpeed");


            if (UDFLib.ConvertToDecimal(lblSteaming_Dist_Till_COP.Text) > 0)
            {
                //txtAvgVSpeed.Text = (UDFLib.ConvertToDecimal(lblTtl_Steam_Hrs_Cop.Text) / UDFLib.ConvertToDecimal(lblSteaming_Dist_Till_COP.Text)).ToString("N3");
                txtAvgVSpeed.Text = (UDFLib.ConvertToDecimal(lblSteaming_Dist_Till_COP.Text) / UDFLib.ConvertToDecimal(lblTtl_Steam_Hrs_Cop.Text)).ToString("N3");
            }

            txtTotal_Cons_HSFO.Text = (UDFLib.ConvertToDecimal(txtAE_Cons_HSFO.Text) + UDFLib.ConvertToDecimal(txtBoiler_Cons_HSFO.Text) + UDFLib.ConvertToDecimal(txtME_Cons_HSFO.Text) + +UDFLib.ConvertToDecimal(txtInc_Cons_HSFO.Text)).ToString("#.##");
            txtTotal_Cons_LSFO.Text = (UDFLib.ConvertToDecimal(txtAE_Cons_LSFO.Text) + UDFLib.ConvertToDecimal(txtBoiler_Cons_LSFO.Text) + UDFLib.ConvertToDecimal(txtME_Cons_LSFO.Text) + +UDFLib.ConvertToDecimal(txtInc_Cons_LSFO.Text)).ToString("#.##");
            txtTotal_Cons_MGO_DO.Text = (UDFLib.ConvertToDecimal(txtAE_Cons_MGO_DO.Text) + UDFLib.ConvertToDecimal(txtBoiler_Cons_MGO_DO.Text) + UDFLib.ConvertToDecimal(txtME_Cons_MGO_DO.Text) + +UDFLib.ConvertToDecimal(txtInc_Cons_MGO_DO.Text)).ToString("#.##");
            txtTotal_Cons_LSMGO.Text = (UDFLib.ConvertToDecimal(txtAE_Cons_LSMGO.Text) + UDFLib.ConvertToDecimal(txtBoiler_Cons_LSMGO.Text) + UDFLib.ConvertToDecimal(txtME_Cons_LSMGO.Text) + +UDFLib.ConvertToDecimal(txtInc_Cons_LSMGO.Text)).ToString("#.##");

            if (UDFLib.ConvertToDecimal(lblSteaming_hrs.Text) > 0)
            {
                if ((!string.IsNullOrEmpty(txtME_Cons_HSFO.Text)) || (!string.IsNullOrEmpty(txtME_Cons_LSFO.Text)) || (!string.IsNullOrEmpty(txtME_Cons_MGO_DO.Text)) || (!string.IsNullOrEmpty(txtME_Cons_LSMGO.Text)))
                {
                    txtME_Cons_Per24_HSFO.Text = (UDFLib.ConvertToDecimal(txtME_Cons_HSFO.Text) / UDFLib.ConvertToDecimal(lblSteaming_hrs.Text) * 24).ToString("#.##");
                    txtME_Cons_Per24_LSFO.Text = (UDFLib.ConvertToDecimal(txtME_Cons_LSFO.Text) / UDFLib.ConvertToDecimal(lblSteaming_hrs.Text) * 24).ToString("#.##");
                    txtME_Cons_Per24_MGO_DO.Text = (UDFLib.ConvertToDecimal(txtME_Cons_MGO_DO.Text) / UDFLib.ConvertToDecimal(lblSteaming_hrs.Text) * 24).ToString("#.##");
                    txtME_Cons_Per24_LSMGO.Text = (UDFLib.ConvertToDecimal(txtME_Cons_LSMGO.Text) / UDFLib.ConvertToDecimal(lblSteaming_hrs.Text) * 24).ToString("#.##");
                }

                if(((!string.IsNullOrEmpty(lblTotal_Hrs_In_Day.Text)) && (UDFLib.ConvertToDecimal(lblTotal_Hrs_In_Day.Text) > 0)))
                {
                    txtAE_Cons_Per24_HSFO.Text = (UDFLib.ConvertToDecimal(txtAE_Cons_HSFO.Text) / UDFLib.ConvertToDecimal(lblTotal_Hrs_In_Day.Text) * 24).ToString("#.##");
                    txtAE_Cons_Per24_LSFO.Text = (UDFLib.ConvertToDecimal(txtAE_Cons_LSFO.Text) / UDFLib.ConvertToDecimal(lblTotal_Hrs_In_Day.Text) * 24).ToString("#.##");
                    txtAE_Cons_Per24_MGO_DO.Text = (UDFLib.ConvertToDecimal(txtAE_Cons_MGO_DO.Text) / UDFLib.ConvertToDecimal(lblTotal_Hrs_In_Day.Text) * 24).ToString("#.##");
                    txtAE_Cons_Per24_LSMGO.Text = (UDFLib.ConvertToDecimal(txtAE_Cons_LSMGO.Text) / UDFLib.ConvertToDecimal(lblTotal_Hrs_In_Day.Text) * 24).ToString("#.##");
                }
                
                txtTotal_Manov_Cons_HSFO.Text = (UDFLib.ConvertToDecimal(txtROB_DEP_Port_HSFO.Text) - UDFLib.ConvertToDecimal(txtROB_EOP_HSFO.Text)).ToString("#.##");
                txtTotal_Manov_Cons_LSFO.Text = (UDFLib.ConvertToDecimal(txtROB_DEP_Port_LSFO.Text) - UDFLib.ConvertToDecimal(txtROB_EOP_LSFO.Text)).ToString("#.##");
                txtTotal_Manov_Cons_MGO_DO.Text = (UDFLib.ConvertToDecimal(txtROB_DEP_Port_MGO_DO.Text) - UDFLib.ConvertToDecimal(txtROB_EOP_MGO_DO.Text)).ToString("#.##");
                txtTotal_Manov_Cons_LSMGO.Text = (UDFLib.ConvertToDecimal(txtROB_DEP_Port_LSMGO.Text) - UDFLib.ConvertToDecimal(txtROB_EOP_LSMGO.Text)).ToString("#.##");
                
            }

            #region conditional formatting

            List<Label> lTextTC = new List<Label>();

            lTextTC.Add(txtTotal_Cons_LSFO);
            lTextTC.Add(txtTotal_Cons_MGO_DO);
            lTextTC.Add(txtTotal_Cons_LSMGO);


            if (UDFLib.ConvertToDecimal(txtTotal_Cons_HSFO.Text) >= 20 && Convert.ToDouble(UDFLib.ConvertToDecimal(txtTotal_Cons_HSFO.Text)) <= 22.5)
            {
                txtTotal_Cons_HSFO.ForeColor = System.Drawing.Color.Yellow;
            }
            if (Convert.ToDouble(UDFLib.ConvertToDecimal(txtTotal_Cons_HSFO.Text)) > 22.5)
            {
                txtTotal_Cons_HSFO.ForeColor = System.Drawing.Color.Red;
            }
            if (UDFLib.ConvertToDecimal(txtTotal_Cons_HSFO.Text) > 24)
            {

                txtTotal_Cons_HSFO.BackColor = System.Drawing.Color.Pink;
            }

            foreach (var item in lTextTC)
            {
                if (UDFLib.ConvertToDecimal(item.Text) >= 20 && Convert.ToDouble(UDFLib.ConvertToDecimal(item.Text)) <= 22.5)
                {
                    item.ForeColor = System.Drawing.Color.Yellow;
                }
                if (Convert.ToDouble(UDFLib.ConvertToDecimal(item.Text)) > 22.5)
                {
                    item.ForeColor = System.Drawing.Color.Red;
                }
                if (UDFLib.ConvertToDecimal(item.Text) > 24)
                {
                    item.ForeColor = System.Drawing.Color.DarkRed;
                    item.BackColor = System.Drawing.Color.Pink;
                }
            }

            lTextTC.Clear();
            lTextTC.Add(txtME_Cons_Per24_HSFO);
            lTextTC.Add(txtME_Cons_Per24_LSFO);
            lTextTC.Add(txtME_Cons_Per24_MGO_DO);
            lTextTC.Add(txtME_Cons_Per24_LSMGO);





            foreach (var item in lTextTC)
            {
                if (UDFLib.ConvertToDecimal(item.Text) >= 20 && UDFLib.ConvertToDecimal(item.Text) <= 22)
                {
                    item.ForeColor = System.Drawing.Color.Yellow;
                }
                if (UDFLib.ConvertToDecimal(item.Text) > 22)
                {
                    item.ForeColor = System.Drawing.Color.Black;
                    item.BackColor = System.Drawing.Color.Red;
                }
            }


            lTextTC.Clear();
            lTextTC.Add(txtAE_Cons_Per24_HSFO);
            lTextTC.Add(txtAE_Cons_Per24_LSFO);
            lTextTC.Add(txtAE_Cons_Per24_MGO_DO);
            lTextTC.Add(txtAE_Cons_Per24_LSMGO);





            foreach (var item in lTextTC)
            {
                if (item.Text != "")
                    if (Convert.ToDouble(UDFLib.ConvertToDecimal(item.Text)) > 4.5)
                    {
                        item.ForeColor = System.Drawing.Color.White;
                        item.BackColor = System.Drawing.Color.Red;
                    }
            }

            #endregion



            #region check for low ROB




            string strVSLflt = "telegram_type= 'A'";

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
                        ((Label)fvarrival.FindControl("lblHO")).CssClass = "RedCell";
                    }
                }
                else if (DataCode == "DO_ROB")
                {
                    if (decimal.Parse(drVSLROB[0]["DO_ROB"].ToString()) < datavalue)
                    {
                        ((Label)fvarrival.FindControl("lblDO")).CssClass = "RedCell";
                    }

                }

                else if (DataCode == "AECC_ROB")
                {
                    if (decimal.Parse(drVSLROB[0]["AECC_ROB"].ToString()) < datavalue)
                    {
                        ((Label)fvarrival.FindControl("lblAECC")).CssClass = "RedCell";
                    }

                }
                else if (DataCode == "MECC_ROB")
                {
                    if (decimal.Parse(drVSLROB[0]["MECC_ROB"].ToString()) < datavalue)
                    {
                        ((Label)fvarrival.FindControl("lblMECC")).CssClass = "RedCell";
                    }

                }

                else if (DataCode == "MECYL_ROB")
                {
                    if (decimal.Parse(drVSLROB[0]["MECYL_ROB"].ToString()) < datavalue)
                    {
                        ((Label)fvarrival.FindControl("lblMECYL")).CssClass = "RedCell";
                    }

                }
                else if (DataCode == "FW_ROB")
                {
                    if (decimal.Parse(drVSLROB[0]["FW_ROB"].ToString()) < datavalue)
                    {
                        ((Label)fvarrival.FindControl("lblFW")).CssClass = "RedCell";
                    }

                }


            #endregion

            }
        }
        catch (Exception ex)
        {
            string js = "alert('" + ex.InnerException.ToString() + "')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

        }



        
       

       
    }

    protected void lbtncrwlist_Click(object s, EventArgs e)
    {
        ResponseHelper.Redirect("~/crew/CrewList_Print.aspx?vcode=" + ((LinkButton)s).CommandArgument, "blank", "");
    }

    protected void fvarrival_ItemCreated(object sender, EventArgs e)
    {
         
    }
}
