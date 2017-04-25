using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Operation;

public partial class Operations_PortReport : System.Web.UI.Page
{
    public static DataTable dtCPROB;
    int PKID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                   
                    BindReport(UDFLib.ConvertToInteger(Request.QueryString["id"]), UDFLib.ConvertToInteger(Request.QueryString["VesselId"]));
                }
                int i = 0;
                DataTable dt = BLL_OPS_VoyageReports.Get_PortReportIndex(UDFLib.ConvertToInteger(Request.QueryString["VesselId"]), null, null,
                    UDFLib.ConvertStringToNull(ViewState["Sort_Column"]), UDFLib.ConvertStringToNull(ViewState["Sort_Direction"]),null,null, ref i);
                ctlRecordNavigationReport.InitRecords(dt);
                dt.PrimaryKey = new DataColumn[] { dt.Columns["PortReportId"] }; 
                DataRow dr = dt.Rows.Find(Int32.Parse(Request.QueryString["id"]));
                ctlRecordNavigationReport.CurrentIndex = dt.Rows.IndexOf(dr);
               
            }
        }
        catch
        {
        }
    }
    protected void BindReportDr(DataRow dr)
    {
        BindReport(UDFLib.ConvertToInteger(dr["PortReportId"]), UDFLib.ConvertToInteger(dr["Vessel_Id"]));
    }
  
    protected void BindReport(int PortReportId, int Vessel_Id)
    {
     
       
       


            DataSet ds = BLL_OPS_VoyageReports.GET_PORT_REPORT(PortReportId, Vessel_Id);
            DataTable dtport = ds.Tables[0];
            string strExpr = "StoppageType = 1";
            DataView dvNonRain = ds.Tables[1].DefaultView;
            dvNonRain.RowFilter = strExpr; 


            string strExpr2 = "StoppageType = 2";
            DataView dvRain = ds.Tables[1].Copy().DefaultView;
            dvRain.RowFilter = strExpr2; 

            Session["NonRainDt"]  = dvNonRain.ToTable().DefaultView.ToTable();
            Session["RainDt"]  = dvRain.ToTable().DefaultView.ToTable();
         

           

            fvportreport.DataSource = dtport;
            fvportreport.DataBind();
            
         

    }

    protected void fvportreport_ItemCreated(object sender, EventArgs e)
    {
        GridView gvNonRain = (GridView)fvportreport.FindControl("gvNonRain");
        GridView gvRain = (GridView)fvportreport.FindControl("gvRain");
       


        

        gvNonRain.DataSource = ((DataTable)Session["NonRainDt"]);
        gvNonRain.DataBind();
        gvRain.DataSource = ((DataTable)Session["RainDt"]);
        gvRain.DataBind();
        DataTable table = new DataTable();
        table.Columns.Add("Dosage", typeof(int));
        table.Columns.Add("Drug", typeof(string));
        table.Columns.Add("Patient", typeof(string));
        table.Columns.Add("Date", typeof(DateTime));

        // Here we add five DataRows.
        table.Rows.Add(25, "Indocin", "David", DateTime.Now);
        table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
        table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
        table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
        table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
       
        


         
    }
    protected void fvportreport_DataBound(object sender, EventArgs e)
    {
        try
        {
            Label Total_Consumption_HSFO = (Label)fvportreport.FindControl("Total_Consumption_HSFO");
            Label Total_Consumption_LSFO = (Label)fvportreport.FindControl("Total_Consumption_LSFO");
            Label Total_Consumption_MGODO = (Label)fvportreport.FindControl("Total_Consumption_MGODO");
            Label Total_Consumption_LSMGO = (Label)fvportreport.FindControl("Total_Consumption_LSMGO");

            Label Bunker_Received_HSFO = (Label)fvportreport.FindControl("Bunker_Received_HSFO");
            Label Bunker_Received_LSFO = (Label)fvportreport.FindControl("Bunker_Received_LSFO");
            Label Bunker_Received_MGODO = (Label)fvportreport.FindControl("Bunker_Received_MGODO");
            Label Bunker_Received_LSMGO = (Label)fvportreport.FindControl("Bunker_Received_LSMGO");

            Label ROB_HSFO = (Label)fvportreport.FindControl("ROB_HSFO");
            Label ROB_LSFO = (Label)fvportreport.FindControl("ROB_LSFO");
            Label ROB_MGODO = (Label)fvportreport.FindControl("ROB_MGODO");
            Label ROB_LSMGO = (Label)fvportreport.FindControl("ROB_LSMGO");

            if (Convert.ToDouble(UDFLib.ConvertToDecimal(Total_Consumption_HSFO.Text)) >= 2.2)
            {
                Total_Consumption_HSFO.ForeColor = System.Drawing.Color.Orange;
            }
            if (Convert.ToDouble(UDFLib.ConvertToDecimal(Total_Consumption_LSFO.Text)) >= 2.2)
            {
                Total_Consumption_LSFO.ForeColor = System.Drawing.Color.Orange;
            }


            if (Convert.ToDouble(UDFLib.ConvertToDecimal(Total_Consumption_HSFO.Text)) > 3.5)
            {
                Total_Consumption_HSFO.ForeColor = System.Drawing.Color.Red;
            }
            if (Convert.ToDouble(UDFLib.ConvertToDecimal(Total_Consumption_LSFO.Text)) > 3.5)
            {
                Total_Consumption_LSFO.ForeColor = System.Drawing.Color.Red;
            }


            if (Convert.ToDouble(UDFLib.ConvertToDecimal(Total_Consumption_HSFO.Text)) > 24)
            {
                Total_Consumption_HSFO.BackColor = System.Drawing.Color.Pink;
                Total_Consumption_HSFO.ForeColor = System.Drawing.Color.Red;
            }
            if (Convert.ToDouble(UDFLib.ConvertToDecimal(Total_Consumption_LSFO.Text)) > 24)
            {
                Total_Consumption_LSFO.BackColor = System.Drawing.Color.Pink;
                Total_Consumption_LSFO.ForeColor = System.Drawing.Color.Red;
            }
            if (Convert.ToDouble(UDFLib.ConvertToDecimal(Total_Consumption_MGODO.Text)) >= 0.7)
            {
                Total_Consumption_MGODO.BackColor = System.Drawing.Color.Pink;
                Total_Consumption_MGODO.ForeColor = System.Drawing.Color.Red;
            }
            if (Convert.ToDouble(UDFLib.ConvertToDecimal(Total_Consumption_LSMGO.Text)) >= 0.7)
            {
                Total_Consumption_LSMGO.BackColor = System.Drawing.Color.Pink;
                Total_Consumption_LSMGO.ForeColor = System.Drawing.Color.Red;
            }



            if (Convert.ToDouble(UDFLib.ConvertToDecimal(Bunker_Received_HSFO.Text)) > 4.5)
            {
                Bunker_Received_HSFO.BackColor = System.Drawing.Color.Red;
                Bunker_Received_HSFO.ForeColor = System.Drawing.Color.White;
            }

            if (Convert.ToDouble(UDFLib.ConvertToDecimal(Bunker_Received_LSFO.Text)) > 4.5)
            {
                Bunker_Received_LSFO.BackColor = System.Drawing.Color.Red;
                Bunker_Received_LSFO.ForeColor = System.Drawing.Color.White;
            }

            if (Convert.ToDouble(UDFLib.ConvertToDecimal(Bunker_Received_MGODO.Text)) > 4.5)
            {
                Bunker_Received_MGODO.BackColor = System.Drawing.Color.Red;
                Bunker_Received_MGODO.ForeColor = System.Drawing.Color.White;
            }

            if (Convert.ToDouble(UDFLib.ConvertToDecimal(Bunker_Received_LSMGO.Text)) > 4.5)
            {
                Bunker_Received_LSMGO.BackColor = System.Drawing.Color.Red;
                Bunker_Received_LSMGO.ForeColor = System.Drawing.Color.White;
            }

            if (Convert.ToDouble(UDFLib.ConvertToDecimal(ROB_HSFO.Text)) > 22)
            {
                ROB_HSFO.BackColor = System.Drawing.Color.Red;
                ROB_HSFO.ForeColor = System.Drawing.Color.Black;
            }

            if (Convert.ToDouble(UDFLib.ConvertToDecimal(ROB_LSFO.Text)) > 22)
            {
                ROB_LSFO.BackColor = System.Drawing.Color.Red;
                ROB_LSFO.ForeColor = System.Drawing.Color.Black;
            }

            if (Convert.ToDouble(UDFLib.ConvertToDecimal(ROB_MGODO.Text)) > 22)
            {
                ROB_MGODO.BackColor = System.Drawing.Color.Red;
                ROB_MGODO.ForeColor = System.Drawing.Color.Black;
            }

            if (Convert.ToDouble(UDFLib.ConvertToDecimal(ROB_LSMGO.Text)) > 22)
            {
                ROB_LSMGO.BackColor = System.Drawing.Color.Red;
                ROB_LSMGO.ForeColor = System.Drawing.Color.Black;
            }
        }
        catch (Exception ex)
        {
            string js = "alert('"+ex.InnerException.ToString()+"')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "initscript", js, true);
              
        }

     
       
        

    }

}