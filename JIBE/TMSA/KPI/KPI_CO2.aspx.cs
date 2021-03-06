﻿using System;
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
using AjaxControlToolkit;



public partial class KPI_CO2 : System.Web.UI.Page
{
    BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
    public UserAccess objUA = new UserAccess();
    public string[] Vessel_Ids ;
    protected void Page_Load(object sender, EventArgs e)
    {
        CalStartDate.Format = UDFLib.GetDateFormat();   //Display the selected date from datepicker as in the same format which has been selected by the user.
        CalEndDate.Format = UDFLib.GetDateFormat();
        UserAccessValidation();
        if (!IsPostBack)
        {
            txtStartDate.Text = DateTime.Now.AddDays(-30).ToString(UDFLib.GetDateFormat());
            txtEndDate.Text = DateTime.Now.ToString(UDFLib.GetDateFormat());
            //txtStartDate.Text = DateTime.Now.AddDays(-30).ToString("dd-MM-yyyy");
            //txtEndDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            BindFleetDLL();
            Load_VesselList();
            getGoal();
            LoadData();
            loadPIDetails();
        }
      
    }

    public string GetPortCallID()
    {
        try
        {

            if (Request.QueryString["Supp_ID"] != null)
            {
                return Request.QueryString["Supp_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }


    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        //if (objUA.Add == 0) //ImgAdd.Visible = false;
        //if (objUA.Edit == 1)
        //    uaEditFlag = true;
        //else
        //    btnsave.Visible = false;
        //if (objUA.Delete == 1) uaDeleteFlage = true;

    }


    private void ClearSearch()
    {
        txtStartDate.Text = DateTime.Now.AddDays(-30).ToString(UDFLib.GetDateFormat());
        txtEndDate.Text = DateTime.Now.ToString(UDFLib.GetDateFormat());

        //txtStartDate.Text = DateTime.Now.AddDays(-30).ToString("dd-MM-yyyy");
        //txtEndDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        CheckBox1.Checked = false;
        BindFleetDLL();
        Load_VesselList();
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
        //if (Convert.ToDateTime(txtStartDate.Text) <= Convert.ToDateTime(txtEndDate.Text))
        if(Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text)) <= Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtEndDate.Text))) 
        {
            LoadData();
        }
        else
        {
            string msg2 = String.Format("alert('Start Date should not be greater than End Date !')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
    }
    /// <summary>
    /// Description: Method to load data based on selected vessel and date range 
    /// </summary>
    private void LoadData()
    {
        try
        {

            DataTable dt = (DataTable)Session["Vessel_Id"];
            hdnVessel_IDs.Value = null;

            if (dt.Rows.Count != 0)
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

                if (Convert.ToInt32(ViewState["RecCount"]) > 0)
                {
                    divChart.Visible = true;
                    if (CheckBox1.Checked != true)
                    {
                        btnVoyageChart.Visible = false;
                        // btnChart.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "showChart();", true);
                    }
                    else
                    {
                        btnVoyageChart.Visible = true;
                        btnChart.Visible = false;

                    }
                }
                else
                {
                    btnVoyageChart.Visible = false;
                    btnChart.Visible = false;
                    divChart.Visible = false;
                }
            }
            else
            {

                string msg = String.Format("alert('Please select vessels.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        ClearSearch();
        LoadData();
    }
    /// <summary>
    /// Description: Method to bind the grid based on search criteria
    /// </summary>
    protected void BindKPI()
    {
        string sStartDate = UDFLib.ConvertToDefaultDt(txtStartDate.Text);
        string sEndDate = UDFLib.ConvertToDefaultDt(txtEndDate.Text);
       // string sStartDate = txtStartDate.Text;
       // string sEndDate = txtEndDate.Text;
        if (sStartDate != "" && sEndDate != "")
        {
            DateTime Startdate = Convert.ToDateTime(sStartDate);
            DateTime EndDate = Convert.ToDateTime(sEndDate);
            DataTable dt = objKPI.Get_CO2_Average((DataTable)Session["Vessel_Id"], Startdate, EndDate);
            ViewState["RecCount"] = dt.Rows.Count; 
            rgdItems.DataSource = dt;
            rgdItems.DataBind();
            hdnVessel_IDs.Value = null;

        }
    }
    

    protected void rgdItems_ItemDataBound(object sender, GridItemEventArgs e)
    {
        foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
        {
            try
            {
                DropDownList ddlVoyage = (DropDownList)dataItem.FindControl("ddlVoyage");
                HiddenField hdVoyage = (HiddenField)dataItem.FindControl("hdVoyage");
                HiddenField hdVesselID = (HiddenField)dataItem.FindControl("hdVesselID");
                LinkButton lnkVessel = (LinkButton)dataItem.FindControl("Item_Name");
                if (hdVesselID.Value != null && CheckBox1.Checked == true)
                {

                    ddlVoyage.Enabled = true;
                    Label Vessel_Average = (Label)dataItem.FindControl("Vessel_Average");
                    Vessel_Average.Text = "0";

                    ddlVoyage.Enabled = true;
                    int Vessel_Id = Convert.ToInt32(hdVesselID.Value);
                    if (txtStartDate.Text != "" && txtEndDate.Text != "")
                    {
                        DateTime Startdate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text));
                        DateTime EndDate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtEndDate.Text));

                        DataTable dt = objKPI.GetVoyageList(Vessel_Id, Startdate, EndDate);
                        ddlVoyage.DataSource = dt;
                        ddlVoyage.DataTextField = "VOYAGE";
                        ddlVoyage.DataValueField = "TelID";

                        ddlVoyage.DataBind();
                        ddlVoyage.Items.Insert(0, new ListItem("-Select-", "0"));
                    }
                }
                Label avg = (Label)dataItem.FindControl("Vessel_Average");
                Label Vessel_Goal = (Label)dataItem.FindControl("Vessel_Goal");
                Label eedi = (Label)dataItem.FindControl("eedi");
                if (Convert.ToDouble(avg.Text) == 0)
                    lnkVessel.Enabled = false;

                if (Convert.ToDouble(avg.Text) > 0 && Convert.ToDouble(eedi.Text) > 0)
                {
                    if ((Convert.ToDouble(avg.Text) < Convert.ToDouble(Vessel_Goal.Text)))
                    {
                        dataItem["Average"].BackColor = System.Drawing.Color.White;
                    }
                    if (Convert.ToDouble(avg.Text) > Convert.ToDouble(eedi.Text))
                    {
                        dataItem["Average"].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE");
                    }
                    else if ((Convert.ToDouble(avg.Text) > Convert.ToDouble(Vessel_Goal.Text)) && (Convert.ToDouble(avg.Text) < Convert.ToDouble(eedi.Text)))
                    {
                        dataItem["Average"].BackColor = System.Drawing.ColorTranslator.FromHtml("#FABF8F");
                    }
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
        }

    }

    /// <summary>
    /// Description: If by voyage is selected , for all vessels voyage ddl wil populate based on search date
    /// </summary>

    protected void ddlVoyage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           DateTime Startdate ;
           Startdate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text));
           //Startdate= Convert.ToDateTime(txtStartDate.Text);
           DateTime EndDate ;
           EndDate = Convert.ToDateTime(UDFLib.ConvertToDate(txtEndDate.Text));
           //EndDate = Convert.ToDateTime(txtEndDate.Text);
            int KPI_ID = 1;
            DropDownList ddlVoyage = (DropDownList)sender;
            GridDataItem item = (GridDataItem)ddlVoyage.NamingContainer;
            Label avg = (Label)item.FindControl("Vessel_Average");
            HiddenField hdf = (HiddenField)item.FindControl("hdVesselID");
            LinkButton lnkVessel = (LinkButton)item.FindControl("Item_Name"); 
            
            item["Average"].BackColor = System.Drawing.Color.White;
            avg.Text = "0";
            if (ddlVoyage.SelectedIndex != 0)
            {
                string val = ddlVoyage.SelectedValue.Trim().Split(':')[0] + ":" + ddlVoyage.SelectedValue.Trim().Split(':')[1];
                DataTable dtq = BLL_TMSA_PI.GetTelDate(val.Trim(), Convert.ToInt32(hdf.Value)).Tables[0];

                if (dtq.Rows[0][0].ToString() != "")
                {
                    Startdate = Convert.ToDateTime(dtq.Rows[0][1].ToString());
                  
                  // hiddenVesselStartDate.Value = dtq.Rows[0][0].ToString();
                   // hiddenStartDate.Text = dtq.Rows[0][0].ToString();
                    hiddenStartDate.Value = Startdate.ToString("dd-MM-yyyy");
                }
                if (dtq.Rows[dtq.Rows.Count - 1][0].ToString() != "")
                {
                    EndDate = Convert.ToDateTime(dtq.Rows[dtq.Rows.Count - 1][1].ToString());
                    //hiddenVesselEndDate.Value = dtq.Rows[dtq.Rows.Count - 1][0].ToString();
                    //hiddenEndDate.Text = dtq.Rows[dtq.Rows.Count - 1][0].ToString();
                    hiddenEndDate.Value = EndDate.ToString("dd-MM-yyyy");
                }
               
            }
       
            DataTable dt = BLL_TMSA_PI.GetVoyageData(ddlVoyage.SelectedValue.Trim(), Convert.ToInt32(hdf.Value), KPI_ID).Tables[0];
            if (dt.Rows.Count > 0)
            {
                avg.Text = Math.Round(Convert.ToDouble(dt.Rows[0]["Value"].ToString()), 2).ToString();
                if (Convert.ToDouble(avg.Text) == 0)
                {
                    lnkVessel.Enabled = false;
                    item["Vessel"].BackColor = System.Drawing.Color.White;
                }
                else
                    lnkVessel.Enabled = true;
            }
            else
            {
                lnkVessel.Enabled = false;
                item["Vessel"].BackColor = System.Drawing.Color.White;
            }

            Label Vessel_Goal = (Label)item.FindControl("Vessel_Goal");
            Label eedi = (Label)item.FindControl("eedi");
           
            if (Convert.ToDouble(avg.Text) > 0 && Convert.ToDouble(eedi.Text) > 0)
            {
                if ((Convert.ToDouble(avg.Text) < Convert.ToDouble(Vessel_Goal.Text)))
                {
                    item["Average"].BackColor = System.Drawing.Color.White;
                }
                if (Convert.ToDouble(avg.Text) > Convert.ToDouble(eedi.Text))
                {
                    item["Average"].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE");
                }
                else if ((Convert.ToDouble(avg.Text) > Convert.ToDouble(Vessel_Goal.Text)) && (Convert.ToDouble(avg.Text) < Convert.ToDouble(eedi.Text)))
                {
                    item["Average"].BackColor = System.Drawing.ColorTranslator.FromHtml("#FABF8F");
                }
            }
           
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void loadPIDetails()
    {

        DataSet ds = BLL_TMSA_PI.Get_KPI_Detail(UDFLib.ConvertIntegerToNull(hiddenKPIID.Value));
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

    public void item_click(object sender, EventArgs e)
    {
        try
        {

            GridDataItem item = (GridDataItem)((LinkButton)sender).NamingContainer;

            DropDownList ddlVoyage = (DropDownList)item.FindControl("ddlVoyage");
            foreach (GridDataItem gvr in rgdItems.Items)
            {
                gvr["Vessel"].BackColor = System.Drawing.Color.White;

            }

            if (CheckBox1.Checked)
            {
                if (ddlVoyage.SelectedIndex <= 0)
                {
                    string stralert = "alert('Please select a voyage ');";

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Alert", stralert, true);
                }
                else
                {
                    item["Vessel"].BackColor = System.Drawing.Color.Yellow;
                }
            }
            else
                item["Vessel"].BackColor = System.Drawing.Color.Yellow;



            LinkButton itemName = (LinkButton)item.FindControl("Item_Name");
           
            HiddenField hdf = (HiddenField)item.FindControl("hdVesselID");
            Label hiddenStartDate = (Label)item.FindControl("hdnVesselStartDate");
            Label hiddenEndDate = (Label)item.FindControl("hdnVesselEndDate");
            int index2 = item.ItemIndex;
            hiddenVesselStartDate.Value = UDFLib.ConvertToDefaultDt(txtStartDate.Text);
            hiddenVesselEndDate.Value = UDFLib.ConvertToDefaultDt(txtEndDate.Text);
           

            getGoal();
            string vesselname = itemName.Text;
            bool voyage = false;
            string jsMethodName = null;
            string val = "";
            string val1 = "";
            string val2="";
            if (CheckBox1.Checked)
            {
                if (ddlVoyage.SelectedIndex > 0)
                {
                    val = ddlVoyage.SelectedValue.Trim().Split(':')[0] + ":" + ddlVoyage.SelectedValue.Trim().Split(':')[1];
                    val1 = ddlVoyage.SelectedValue.Trim().Split(':')[0];
                    val2 = ddlVoyage.SelectedValue.Trim().Split(':')[1];
                    DataTable dtq = BLL_TMSA_PI.GetTelDate(val.Trim(), Convert.ToInt32(hdf.Value)).Tables[0];

                    if (dtq.Rows[0][1].ToString() != "")
                    {

                        hiddenStartDate.Text = dtq.Rows[0][1].ToString();

                    }
                    if (dtq.Rows[dtq.Rows.Count - 1][1].ToString() != "")
                    {

                        hiddenEndDate.Text = dtq.Rows[dtq.Rows.Count - 1][1].ToString();

                    }

                    voyage = true;

                    jsMethodName = "showChart2('" + val1 + "','" + val2 + "','" + hiddenStartDate.Text + "','" + hiddenEndDate.Text + "','" + vesselname + "','" + hdf.Value + "','" + voyage + "','" + ddlVoyage.SelectedValue.Trim() + "')";

                }

            }

            else

                jsMethodName = "showChart2('" + val1 + "','" + val2 + "','" + Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text)).ToString("yyyy-MM-dd") + "','" + Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtEndDate.Text)).ToString("yyyy-MM-dd") + "','" + vesselname + "','" + hdf.Value + "','" + voyage + "','" + ddlVoyage.SelectedValue.Trim() + "')";



            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "uniqueKey", jsMethodName, true);
 
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        LoadData();


    }
}