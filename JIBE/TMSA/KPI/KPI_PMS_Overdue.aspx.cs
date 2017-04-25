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
using System.Web.Globalization;


public partial class KPI_PMS_Overdue : System.Web.UI.Page
{
    public UserAccess objUA = new UserAccess();
    BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
    public string[] Vessel_Ids ;
    protected void Page_Load(object sender, EventArgs e)
    {
        CalStartDate.Format = UDFLib.GetDateFormat();   //Display the selected date from datepicker as in the same format which has been selected by the user.
        CalEndDate.Format = UDFLib.GetDateFormat();
        
       // UserAccessValidation();
        if (!IsPostBack)
        {   
            //txtStartDate.Text = DateTime.Now.AddYears(-1).ToString("dd-MM-yyyy");
            //txtEndDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtStartDate.Text = DateTime.Now.AddYears(-1).ToString(UDFLib.GetDateFormat());
            txtEndDate.Text = DateTime.Now.ToString(UDFLib.GetDateFormat());
            hiddenStartDate.Value = UDFLib.ConvertToDefaultDt(txtStartDate.Text);
            hiddenEndDate.Value = UDFLib.ConvertToDefaultDt(txtEndDate.Text);
            BindFleetDLL();

            Load_VesselList();
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
            //UDFLib.WriteExceptionLog(ex);

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
                    if (dtVessel.Rows[i]["Vessel_id"] != null)
                        DDLVessel.SelectItems(new string[] { dtVessel.Rows[i]["Vessel_id"].ToString() });

                }

            }
            Session["Vessel_Id"] = DDLVessel.SelectedValues;

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);

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
        LoadData();

    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        ClearSearch();
        LoadData();
    }

    private void ClearSearch()
    {
        txtStartDate.Text = DateTime.Now.AddYears(-1).ToString(UDFLib.GetDateFormat());
        txtEndDate.Text = DateTime.Now.ToString(UDFLib.GetDateFormat());
        BindFleetDLL();
        Load_VesselList();
    }

    private void LoadData()
    {
        try
        {

            DataTable dt = (DataTable)Session["Vessel_Id"];
            hdnVessel_IDs.Value = null;
            if (dt.Rows.Count != 0)
            {
                BindKPI();
            }
            else
            {

                string msg = String.Format("alert('Please select vessels')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);

        }
        
    }


/// <summary>
/// Based on search criteria , method will search the different PMS KPIvalues and pivot the Monthwise values into columns
/// </summary>
    private void BindKPI()
    {
        try
        {
            string sStartDate = txtStartDate.Text;       
            string sEndDate = txtEndDate.Text;
            if (sStartDate != "" && sEndDate != "")
            {
                DateTime StartDate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text));      //ConvertToDefaultDt() method is used to change the user selected date format to the default format, the format in which date is saved in database.
                DateTime EndDate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtEndDate.Text));

                DateTime prevMonthLastDate= EndDate.AddDays(-EndDate.Day);
                string lastMonth = String.Format("{0:y}", prevMonthLastDate);
                hdnLastMonth.Value = lastMonth;
                DataSet ds = objKPI.Get_PM_Overdue((DataTable)Session["Vessel_Id"], StartDate, EndDate,null);
                DataTable dt = ds.Tables[0];
                string[] Pkey_cols = new string[] { "VesselID", "KPI_ID" };
                string[] Hide_cols = new string[] { "ID", "KPI_ID", "Effective_Date" };
                DataTable dt1 = objKPI.PivotTable("MonthYear", "Value", "", Pkey_cols, Hide_cols, dt);
                DataView dtVesselIDs = new DataView(dt1);
                DataTable distinctValues = dtVesselIDs.ToTable(true, "VesselID");
                string Vessel_Ids = "";

   
                foreach (DataRow dr in distinctValues.Rows)
                {

                    Vessel_Ids = Vessel_Ids + "," + dr["VesselID"].ToString();
                }
                Vessel_Ids = Vessel_Ids.Trim(',');
                hdnVessel_IDs.Value = Vessel_Ids;

                if (StartDate <= EndDate)
                {
                    if ((EndDate - StartDate).TotalDays >= 28 || EndDate.Month != StartDate.Month)
                    {
                        if (dt1.Rows.Count > 0)
                        {

                            gvPMSOverdue.DataSource = dt1;
                            gvPMSOverdue.DataBind();

                            gvPMSOverdue.Visible = true;
                            string jsMethodName = null;
                            jsMethodName = "$(document).ready(function () {showChart('" + Vessel_Ids + "','" + Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text)).ToString("yyyy-MM-dd") + "','" + lastMonth + "' )  });";
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "uniqueKey", jsMethodName, true);
                        }
                        else
                        {
                            gvPMSOverdue.Visible = false;
                            string msg2 = String.Format("showAlert('No record available! ')");

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "uniqueKey", msg2, true);

                        }
                    }
                    else
                    {
                        gvPMSOverdue.Visible = false;
                        string msg2 = String.Format("showAlert('No record available between this date range! ')");

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "uniqueKey", msg2, true);

                    }
                }
                else
                {
                    gvPMSOverdue.Visible = false;
                    string msg2 = String.Format("showAlert('Start Date should not be greater than End Date ')");

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "uniqueKey", msg2, true);
                }



            }
           
        }
        catch (Exception ex)
        {
           UDFLib.WriteExceptionLog(ex);

        }
        

    }

    /// <summary>
    /// Logic included to provide hyperlink for monthly graph for all vessels
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPMSOverdue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[3].Visible = false;


        if (e.Row.RowType == DataControlRowType.Header)
        {

            string Vessel_Id = hdnVessel_IDs.Value;

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
               
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("[", " ");
                e.Row.Cells[i].Text = e.Row.Cells[i].Text.Replace("]", " ");

                if (i > 3)
                {
                    string[] arrDate = e.Row.Cells[i].Text.Split('#');
                    string lastDateMonth = arrDate[1].ToString();
                    string lastMonth = arrDate[0].ToString();
                    e.Row.Cells[i].Text = "";
                    HyperLink hp = new HyperLink();
                    hp.Attributes.Add("href", "#");
                    hp.Attributes.Add("Class", "chkheaderSelected");
                    hp.Text = lastMonth;
                    e.Row.Cells[i].Controls.Add(hp);
                    e.Row.Cells[i].Attributes.Add("onclick", "$(document).ready(function () {showChart('" + Vessel_Id + "','" + lastDateMonth + "','" + lastMonth + "')})");
                 
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i > 1)
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
               
            }

            if (e.Row.RowIndex != 0 && e.Row.RowIndex % 2 != 0)
            {
                e.Row.Cells[0].Text = "";
            }

        }
    }

    /// <summary>
    /// Method to load PIs for PMS overdue KPI and display formula
    /// </summary>

    protected void loadPIDetails()
    {
        try
        {
            int KPI_ID1 = Convert.ToInt16(hdnKPI1.Value);
            int KPI_ID2 = Convert.ToInt16(hdnKPI2.Value);
            int KPI_ID3 = Convert.ToInt16(hdnKPI3.Value);
           
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("sno", typeof(int)));
            dt.Columns.Add(new DataColumn("PID", typeof(string)));
            dt.Columns.Add(new DataColumn("value", typeof(string)));
            dt.Columns.Add(new DataColumn("PIName", typeof(string)));

            DataSet ds = BLL_TMSA_PI.Get_KPI_Detail(KPI_ID1);

            DataTable dtPIDtl = ds.Tables[1];
            string exp = "";
            if (dtPIDtl.Rows.Count > 0)
            {
                foreach (DataRow row in dtPIDtl.Rows)
                {
                    dt.Rows.Add(new object[] { Convert.ToInt32(row["SerialNumber"].ToString()), row["PI_ID"].ToString(), row["value"].ToString(), row["Name"].ToString() });
                    exp = exp + row["value"].ToString();
                }
                lblFormula.Text = "Non Critical Rate: " + exp;
                DataTable dt_PI1 = dt;
                dt_PI1.DefaultView.RowFilter = "PID <> ''";
                dt_PI1.DefaultView.Sort = "PID asc";
                DataList1.DataSource = dt_PI1.DefaultView;
                DataList1.DataBind();
            }


            ds.Clear();
            dt.Clear();
            dtPIDtl.Clear();

            ds = BLL_TMSA_PI.Get_KPI_Detail(KPI_ID2);

            dtPIDtl = ds.Tables[1];
            exp = "";
            if (dtPIDtl.Rows.Count > 0)
            {
                foreach (DataRow row in dtPIDtl.Rows)
                {
                    dt.Rows.Add(new object[] { Convert.ToInt32(row["SerialNumber"].ToString()), row["PI_ID"].ToString(), row["value"].ToString(), row["Name"].ToString() });
                    exp = exp + row["value"].ToString();
                }
                lblFormula2.Text = "Critical Rate: " + exp;
                DataTable dt_PI1 = dt;
                dt_PI1.DefaultView.RowFilter = "PID <> ''";
                dt_PI1.DefaultView.Sort = "PID asc";
                DataList2.DataSource = dt_PI1.DefaultView;
                DataList2.DataBind();
            }



            ds.Clear();
            dt.Clear();
            dtPIDtl.Clear();
            ds = BLL_TMSA_PI.Get_KPI_Detail(KPI_ID3);

            dtPIDtl = ds.Tables[1];
            exp = "";
            if (dtPIDtl.Rows.Count > 0)
            {
                foreach (DataRow row in dtPIDtl.Rows)
                {
                    dt.Rows.Add(new object[] { Convert.ToInt32(row["SerialNumber"].ToString()), row["PI_ID"].ToString(), row["value"].ToString(), row["Name"].ToString() });
                    exp = exp + row["value"].ToString();
                }
                lblFormula3.Text = "KPI030 Formula : " + exp;
                DataTable dt_PI1 = dt;
                dt_PI1.DefaultView.RowFilter = "PID <> ''";
                dt_PI1.DefaultView.Sort = "PID asc";
                DataList3.DataSource = dt_PI1.DefaultView;
                DataList3.DataBind();
            }


        }

        catch (Exception ex)
        {
           //UDFLib.WriteExceptionLog(ex);

        }
        
        
    }

    public void item_click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkVessel = (LinkButton)sender;
            GridViewRow gvrSelected = (GridViewRow)lnkVessel.NamingContainer;
            foreach (GridViewRow gvr in gvPMSOverdue.Rows)
            {
                gvr.Cells[0].BackColor = System.Drawing.Color.White;

            }
            gvrSelected.Cells[0].BackColor = System.Drawing.Color.Yellow;


            HiddenField hdf = (HiddenField)gvrSelected.FindControl("hdVesselID");

            hiddenStartDate.Value =UDFLib.ConvertToDefaultDt(txtStartDate.Text);
            hiddenEndDate.Value = UDFLib.ConvertToDefaultDt(txtEndDate.Text);

            string vesselname = lnkVessel.Text;
            string jsMethodName = null;

            jsMethodName = "showChart2('" + Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text)).ToString("yyyy-MM-dd") + "','" + Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtEndDate.Text)).ToString("yyyy-MM-dd") + "','" + vesselname + "','" + hdf.Value + "')";
            //ScriptManager.RegisterClientScriptBlock(this, typeof(string), "uniqueKey", jsMethodName, true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", jsMethodName, true);
        }

        catch (Exception ex)
        {
            //UDFLib.WriteExceptionLog(ex);

        }
        
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        LoadData();
    }



}