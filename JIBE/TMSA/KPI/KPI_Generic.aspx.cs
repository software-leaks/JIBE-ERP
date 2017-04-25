using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SMS.Business.Infrastructure;
using SMS.Business.TMSA;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;
using System.Text;
using System.Web.Script.Serialization;


public partial class KPI_CO2 : System.Web.UI.Page
{
    BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
    public string[] Vessel_Ids ;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStartDate.Text = DateTime.Now.AddDays(-30).ToString("dd-MM-yyyy");
            txtEndDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            BindFleetDLL();
            Load_VesselList();
            getGoal();
           // LoadData();
            loadKPIs();
            BindInterval();
           
        }
    }

    public void getGoal()
    {
        DataTable dt = objKPI.GetGoal(0, "KPI005",0);
        if(dt.Rows.Count > 0)
            hiddenKPIID.Value = dt.Rows[0]["KPI_ID"].ToString();

    }
    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
            if (FleetDT.Rows.Count > 0)
            {
                if (Session["USERFLEETID"] != null && Session["USERFLEETID"].ToString() != "0")
                {
                    DDLFleet.SelectItems(new string[] { Session["USERFLEETID"].ToString() });
                }
                else
                {
                    foreach (DataRow dr in FleetDT.Rows)
                    {
                        DDLFleet.SelectItems(new string[] { dr["code"].ToString() });
                    }
                }

            }

            Session["sFleet"] = DDLFleet.SelectedValues;
        }
        catch (Exception ex)
        {

        }
    }



    public void Load_VesselList()
    {
        try
        {
            DataTable dtVessel = objKPI.Get_Fleet_Vessel_List((DataTable)Session["sFleet"], Convert.ToInt32(Session["USERCOMPANYID"].ToString()), Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID());
            foreach (DataRow dr in dtVessel.Rows)
            {
                if (dr["Vessel_Id"] == "11" || dr["Vessel_Id"] == "13")
                    dtVessel.Rows.Remove(dr);
            }

            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            if (dtVessel.Rows.Count > 0)
            {
                for (int i = 0; i < dtVessel.Rows.Count; i++)
                {
                   if( dtVessel.Rows[i]["Vessel_id"] != null)
                     DDLVessel.SelectItems(new string[] { dtVessel.Rows[i]["Vessel_id"].ToString() });

                }

            }
            Session["Vessel_Id"] = DDLVessel.SelectedValues;

        }
        catch (Exception ex)
        {

        }


    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    private void loadKPIs()
    {

        DataTable dt = objKPI.Get_KPIList();
        ddlKPIList.DataSource = dt;
        ddlKPIList.DataTextField = "KPIName";
        ddlKPIList.DataValueField = "KPI_ID";
        ddlKPIList.DataBind();
        ddlKPIList.Items.Insert(0, new ListItem("-SELECT-", "0"));

    }
    private void BindInterval()
    {

        DataTable dt = objKPI.Get_Intervals("");
        ddlInterval.DataSource = dt;
        ddlInterval.DataTextField = "Interval_Name";
        ddlInterval.DataValueField = "Interval_Name";
        ddlInterval.DataBind();
        ddlInterval.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }

    protected void DDLFleet_SelectedIndexChanged()
    {
        Session["sFleet"] = DDLFleet.SelectedValues;
        Load_VesselList();
        Session["Vessel_Id"] = null;
        Session["Vessel_Id"] = DDLVessel.SelectedValues;
        LoadData();
    }

    protected void DDLVessel_SelectedIndexChanged() 
    {

        Session["Vessel_Id"] = DDLVessel.SelectedValues;

        LoadData();
    }
    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        LoadData();
    }

    private void LoadData()
    {
       
        Button1.Visible = false;
        btnChart.Visible = true;
        DataTable dt = (DataTable)Session["Vessel_Id"];
        hdnVessel_IDs.Value = null;

        if ( dt.Rows.Count != 0)
        {
            BindKPI();
            Vessel_Ids = new string[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                Vessel_Ids[i] = dr[0].ToString();
                hdnVessel_IDs.Value = hdnVessel_IDs.Value + "," + dr[0].ToString();
                i++;
            }
            hdnVessel_IDs.Value = hdnVessel_IDs.Value.Trim(',');
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "showChart();", true);
        }
        else
        {

            string msg = String.Format("alert('Please select maximum 10 vessels at a time')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }


    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        LoadData();
    }

    protected void BindKPI()
    {
        string sStartDate = txtStartDate.Text;
        string sEndDate = txtEndDate.Text;
        int KPI_ID = 0;
        if (sStartDate != "" && sEndDate != "")
        {
            DateTime? Startdate = Convert.ToDateTime(sStartDate);
            DateTime? EndDate = Convert.ToDateTime(sEndDate);
            if (Request.QueryString["KPIID"] != null)
            {
                KPI_ID = Convert.ToInt32(Request.QueryString["KPIID"]);
                ddlKPIList.Enabled = false;
            }
            else
            {
                KPI_ID = Convert.ToInt32(ddlKPIList.SelectedValue);

            }
            DataTable dt = objKPI.Get_Multiple_Generic_Values((DataTable)Session["Vessel_Id"],  KPI_ID,  null,  null, Startdate,  EndDate).Tables[0];
          
            //if (dt.Rows.Count > 0)
            //{
                rgdItems.DataSource = dt;
                rgdItems.DataBind();
                dt = null;
            //}
        }
    }
    

    protected void rgdItems_ItemDataBound(object sender, GridItemEventArgs e)
    {
        foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
        {
            DropDownList ddlVoyage = (DropDownList)dataItem.FindControl("ddlVoyage");
            HiddenField hdVoyage = (HiddenField)dataItem.FindControl("hdVoyage");
            HiddenField hdVesselID = (HiddenField)dataItem.FindControl("hdVesselID");
            HiddenField hiddenStartDate = (HiddenField)dataItem.FindControl("hiddenStartDate");
            HiddenField hiddenEndDate = (HiddenField)dataItem.FindControl("hiddenEndDate");
            if (hdVesselID.Value != null)
            {
                int Vessel_Id = Convert.ToInt32(hdVesselID.Value);
                if (txtStartDate.Text != "" && txtEndDate.Text != "")
                {
                    DateTime Startdate = Convert.ToDateTime(txtStartDate.Text);
                    DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);

                    DataTable dt = objKPI.GetVoyageList(Vessel_Id, Startdate, EndDate);
                    ddlVoyage.DataSource = dt;
                    ddlVoyage.DataTextField = "VOYAGE";
                    ddlVoyage.DataValueField = "TelID";

                    ddlVoyage.DataBind();
                    ddlVoyage.Items.Insert(0, new ListItem("-Select-", "0"));
                }

            }

        }
       
    }


    protected void ddlVoyage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime? Startdate = null;
           Startdate= Convert.ToDateTime(txtStartDate.Text);
            DateTime? EndDate =null;
           EndDate = Convert.ToDateTime(txtEndDate.Text);
            int KPI_ID = 1;
            DropDownList ddlVoyage = (DropDownList)sender;
            GridDataItem item = (GridDataItem)ddlVoyage.NamingContainer;
            Label txtSrno = (Label)item.FindControl("Vessel_Average");
            HiddenField hdf = (HiddenField)item.FindControl("hdVesselID");
            if (ddlVoyage.SelectedIndex != 0)
            {
                string val = ddlVoyage.SelectedValue.Trim().Split(':')[0] + ":" + ddlVoyage.SelectedValue.Trim().Split(':')[1];
                DataTable dtq = BLL_TMSA_PI.GetTelDate(val.Trim(), Convert.ToInt32(hdf.Value)).Tables[0];

                if (dtq.Rows[0][0].ToString() != "")
                {
                    Startdate = Convert.ToDateTime(dtq.Rows[0][0].ToString());
                    hiddenStartDate.Value = Startdate.Value.Date.ToString("dd-MM-yyyy");
                }
                if (dtq.Rows[dtq.Rows.Count - 1][0].ToString() != "")
                {
                    EndDate = Convert.ToDateTime(dtq.Rows[dtq.Rows.Count - 1][0].ToString());
                    hiddenEndDate.Value = EndDate.Value.Date.ToString("dd-MM-yyyy");
                }
            }
            DataTable dt = BLL_TMSA_PI.GetVoyageData(ddlVoyage.SelectedValue.Trim(), Convert.ToInt32(hdf.Value), KPI_ID).Tables[0];
            txtSrno.Text = Math.Round(Convert.ToDouble(dt.Rows[0]["Value"].ToString()), 2).ToString();
            Button1.Visible = true;
            btnChart.Visible = false;
        }
        catch
        {
        }
    }


    protected void ddlKPIList_SelectedIndexChanged(object sender, EventArgs e)
    {
        hiddenKPIID.Value = ddlKPIList.SelectedValue;
        loadPIDetails();

    }
    protected void ddlInterval_SelectedIndexChanged(object sender, EventArgs e)
    {
        hiddenInterval.Value = ddlInterval.SelectedValue;
    }
    protected void loadPIDetails()
    {

        DataSet ds = BLL_TMSA_PI.Get_KPI_Detail(UDFLib.ConvertIntegerToNull(hiddenKPIID.Value));
        if (ds.Tables[0].Rows.Count > 0)
        {
            ltKPI.Text = ds.Tables[0].Rows[0]["Name"].ToString();
            hiddenKPIName.Value = ds.Tables[0].Rows[0]["Name"].ToString();
            DataTable dt = new DataTable();
            DataTable dtPIDtl = ds.Tables[1];
            dt.Columns.Add(new DataColumn("sno", typeof(int)));
            dt.Columns.Add(new DataColumn("PID", typeof(string)));
            dt.Columns.Add(new DataColumn("value", typeof(string)));
            dt.Columns.Add(new DataColumn("PIName", typeof(string)));
            string exp = "";
            foreach (DataRow row in dtPIDtl.Rows)
            {
                dt.Rows.Add(new object[] { Convert.ToInt32(row["SerialNumber"].ToString()), row["PI_ID"].ToString(), row["value"].ToString(), row["Name"].ToString() });
                exp = exp + row["value"].ToString();
            }
            lblFormula.Text = "KPI Formula : " + exp;
            DataTable dt_PI = dt;
            dt_PI.DefaultView.RowFilter = "PID <> ''";
            DataList1.DataSource = dt_PI.DefaultView;
            DataList1.DataBind();
        }

    }
    

}