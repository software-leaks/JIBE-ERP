using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.VM;
using SMS.Business.Infrastructure;
using SMS.Business.TMSA;
using SMS.Properties;


public partial class TMSA_KPI_Vessel_CO2_Efficiency : System.Web.UI.Page
{
    BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
    BLL_TMSA_EEDI objEEDIBLL = new BLL_TMSA_EEDI();
    int KPI_ID = 1;
    DateTime? Startdate = null;
    DateTime? EndDate = null;
    public UserAccess objUA = new UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        CalStartDate.Format = UDFLib.GetDateFormat();      //Display the selected date from datepicker as in the same format which has been selected by the user.
        CalEndDate.Format = UDFLib.GetDateFormat();
        UserAccessValidation();
        getGoal();
        if (!IsPostBack)
        {
            txtStartDate.Text = DateTime.Now.AddDays(-30).ToString(UDFLib.GetDateFormat());
            txtEndDate.Text = DateTime.Now.ToString(UDFLib.GetDateFormat());
            //txtStartDate.Text = DateTime.Now.AddDays(-30).ToString("dd-MM-yyyy");
            //txtEndDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            BindVesselList();
            loadFormula();
            bindgvSearch();
            gvSearch.Visible = true;

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

    public void getGoal()
    {
        DataTable dt = objKPI.GetGoal(0,"KPI005",0);
        if (dt.Rows.Count > 0)
            hiddenKPIID.Value = dt.Rows[0]["KPI_ID"].ToString();

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    private void loadFormula()
    {
        DataSet ds = BLL_TMSA_PI.Get_KPI_Detail(UDFLib.ConvertIntegerToNull(hiddenKPIID.Value));
       
        DataTable dtPIDtl = ds.Tables[1];

        string exp = "";
        foreach (DataRow row in dtPIDtl.Rows)
        {
           
            exp = exp + row["value"].ToString();
        }
        lblFormula.Text = "KPI Formula : " + exp;
        loadPIDetails();

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

    protected void BindVesselList()
    {

    //    DataTable dt = objKPI.Get_KPI_DetailGoals(-1).Tables[0];
        BLL_Infra_VesselLib bll_Vessel = new BLL_Infra_VesselLib();
        BLL_TMSA_KPI bll_KPI = new BLL_TMSA_KPI();
        DataTable dt = bll_Vessel.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
        dt.Columns.Remove("code");
        dt.Columns.Remove("name");
        dt.Columns.Remove("fleetname");
        dt.Columns.Remove("Super_MailID");
        dt.Columns.Remove("TechTeam_MailID");
        dt.Columns.Remove("Vessel_Owner");
        dt.Columns.Remove("Vessel_Manager");
        DataTable dtable = bll_KPI.Get_Fleet_Vessel_List(dt, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID());
        ddlvessel.DataSource = dtable;
        ddlvessel.DataTextField = "Vessel_Name";
        ddlvessel.DataValueField = "Vessel_Id";
        ddlvessel.DataBind();
        ddlvessel.Items.Insert(0, new ListItem("--Select--", "0"));
        if (dtable != null && dtable.Rows.Count > 0)
        {
            ddlvessel.SelectedIndex = 1;
            hiddenVessel.Value = "1";
        }
    }

    public void clear()
    {
        divVoyage.Visible = false;
        imgComp.Visible = false;
        divChart.Visible = false;
        gvSearch.Visible = false;

        checkVoyage.Checked = false;
        btnVoyage.Visible = false;
        gvVoyage.Visible = false;
        listVoyage.Items.Clear();
        imgADD.Enabled = false;
        btnChart.Visible = false;

        checkVoyage.Checked = false;
        listVoyage.Items.Clear();
        ddlVoyage.Enabled = false;
    }
    protected void btnportfilter_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlvessel.SelectedIndex != 0 && !string.IsNullOrEmpty(txtEndDate.Text) && !string.IsNullOrEmpty(txtStartDate.Text))
        {
            if (Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text)) <= Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtEndDate.Text)))
            {
                txtGoal.Text = "";
                txtEEDI.Text = "";
                imgADD.Enabled = false;
                imgComp.Visible = true;
                divChart.Visible = false;
                if (ddlvessel.SelectedIndex != 0)
                {
                    hiddenVessel.Value = ddlvessel.SelectedValue;
                    gvSearch.Visible = false;
                }
                else
                {
                    hiddenVessel.Value = "-1";
                }
                divVoyage.Visible = true;
                divGrid.Visible = true;

                listVoyage.Items.Clear();

                txtGoal.Visible = true;
                txtEEDI.Visible = true;
                lblGoal.Visible = true;
                lblEEDI.Visible = true;
                divChart.Visible = true;
                gvSearch.Visible = true;
                // btnVoyage.Visible = true;
                gvVoyage.Visible = false;
                if (checkVoyage.Checked)
                {
                    bindVoyage();
                    BindVoyageData();
                    gvSearch.Visible = false;
                    gvVoyage.Visible = true;
                    imgADD.Enabled = true;
                }
                else
                {
                    bindgvSearch();
                    gvSearch.Visible = true;
                    gvVoyage.Visible = false;
                }
            }
            else
            {
                clear();


                string msg2 = String.Format("alert('Start Date should not be greater than End Date ')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            }
        }
        else
        {
           clear();


            string msg2 = String.Format("alert('Vessel/StartDate/EndDate should not be blank')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);

        }
    }
    protected void ddlvessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtGoal.Text = "";
        if (ddlvessel.SelectedIndex != 0)
        {
            hiddenVessel.Value = ddlvessel.SelectedValue;
            checkVoyage.Checked = false;

            DataTable dtt = objEEDIBLL.Get_EEDI(Convert.ToInt32(ddlvessel.SelectedValue));
            if (dtt.Rows.Count != 0)
            {
                lblEEDI.Visible = true;
                txtEEDI.Text = Math.Round(Convert.ToDecimal(dtt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                txtEEDI.Visible = true;
            }
            else
            {
                txtEEDI.Text = "0";
            }

            DataTable dt = objKPI.GetGoal(Convert.ToInt32(ddlvessel.SelectedValue), "KPI005", 0);

            if (dt.Rows.Count > 0)
            {
                lblGoal.Visible = true;
                txtGoal.Text = Math.Round(Convert.ToDecimal(dt.Rows[0]["Goal"].ToString()), 2).ToString();
                txtGoal.Visible = true;
            }
            else
                txtGoal.Text = "0.00";
        }
        else
        {
            hiddenVessel.Value = "-1";
        }
    }


    protected void bindVoyage()
    {
      BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
    
      int Vessel_Id = Convert.ToInt32(ddlvessel.SelectedValue);
      if (txtStartDate.Text != "" && txtEndDate.Text != "")
      {
          DateTime Startdate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text));
          DateTime EndDate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtEndDate.Text));

          DataTable dt = objKPI.GetVoyageList(Vessel_Id, Startdate, EndDate);
          ddlVoyage.DataSource = dt;
          ddlVoyage.DataTextField = "VOYAGE";
          ddlVoyage.DataValueField = "TelID";

          ddlVoyage.DataBind();

      }

    }
    protected void checkVoyage_CheckedChanged(object sender, EventArgs e)
    {
        if (checkVoyage.Checked)
        {
            //txtEndDate.Text = "";
            //txtStartDate.Text = "";
            divChart.Visible = true;
            ddlVoyage.Enabled = true;
            listVoyage.Enabled = true;
            imgADD.Enabled = true;
            bindVoyage();
            btnVoyage.Visible = true;
            btnChart.Visible = false;
            gvSearch.Visible = false;
            lblGoal.Visible = true;
            txtGoal.Visible = true;
            lblEEDI.Visible = true;
            txtEEDI.Visible = true;
          //  gvVoyage.Visible = true;

        }
        else
        {
            //txtEndDate.Text = "";
            //txtStartDate.Text = "";
            txtStartDate.Enabled = true;
            txtEndDate.Enabled = true;
            ddlVoyage.Enabled = false;
            listVoyage.Enabled = false;
            imgADD.Enabled = false;
            listVoyage.Items.Clear();
            btnVoyage.Visible = false;
            //btnChart.Visible = true;
            gvSearch.Visible = true;
            gvVoyage.Visible = false;
            lblGoal.Visible = false;
            txtGoal.Visible = false;
            lblEEDI.Visible = false;
            txtEEDI.Visible = false;
        }
    }
    protected void imgADD_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlVoyage.SelectedItem != null)
        {

                if (!listVoyage.Items.Contains(ddlVoyage.SelectedItem))
                {
                    listVoyage.Items.Add(ddlVoyage.SelectedItem);
                }
                int Vessel_Id = Convert.ToInt32(ddlvessel.SelectedValue);
                if (txtStartDate.Text != "" && txtEndDate.Text != "")
                {
                    DateTime Startdate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text));
                    DateTime EndDate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtEndDate.Text));

                    DataTable dt = objKPI.GetVoyageList(Vessel_Id, Startdate, EndDate);
                    for (int i = 0; i < listVoyage.Items.Count; i++)
                    {
                        DataRow[] result = dt.Select("Voyage = '" + listVoyage.Items[i] + "'");
                        foreach (DataRow row in result)
                        {

                            dt.Rows.Remove(row);
                        }
                    }



                    ddlVoyage.DataSource = dt;
                    ddlVoyage.DataTextField = "VOYAGE";
                    ddlVoyage.DataValueField = "TelID";

                    ddlVoyage.DataBind();

                }
                hdnEnd.Value = "";
                hdnStart.Value = "";
                BindVoyageData();

        }
       

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        ClearSearch();
        Response.Redirect(Request.RawUrl);
    }

    protected void ClearSearch()
    {
        ddlvessel.SelectedIndex = -1;
        txtEndDate.Text = "";
        txtStartDate.Text = "";
        checkVoyage.Checked = false;
        txtGoal.Text = "";
        txtEEDI.Text = "";
        lblFormula.Text = "";
    }
 
    protected void BindVoyageData()
    {
      DataTable liTable = new DataTable();
        if (listVoyage.Items.Count > 0)
        {
            for (int x = 0; x < listVoyage.Items.Count; x++)
            {
                //string v1 = listVoyage.Items[0].Value.Split(':')[1].Trim();
                //string v2 = listVoyage.Items[listVoyage.Items.Count - 1].Value.Split(':')[0].Trim();
                string val = listVoyage.Items[x].Value.Split(':')[0] + ":" + listVoyage.Items[x].Value.Split(':')[1];

                DataTable dtq = BLL_TMSA_PI.GetTelDate(val.Trim(), Convert.ToInt32(ddlvessel.SelectedValue)).Tables[0];
                if (dtq.Rows[0]["Telegram_Date_DDMM"].ToString() != "")
                {
                    Startdate = Convert.ToDateTime(dtq.Rows[0]["Telegram_Date_DDMM"].ToString());
                }
                if (dtq.Rows[dtq.Rows.Count - 1]["Telegram_Date_DDMM"].ToString() != "")
                {
                    EndDate = Convert.ToDateTime(dtq.Rows[dtq.Rows.Count - 1]["Telegram_Date_DDMM"].ToString());
                }

              
               DataTable dt = BLL_TMSA_PI.GetVoyageData(listVoyage.Items[x].Value.Trim(), Convert.ToInt32(ddlvessel.SelectedValue), KPI_ID).Tables[0];

               liTable.Merge(dt);
            }
            gvVoyage.Visible = true;
            gvSearch.Visible = false;

            gvVoyage.DataSource = liTable;
            gvVoyage.DataBind();
            if (liTable.Rows.Count != 0)
            {
                double AVG = Convert.ToDouble(liTable.Compute("AVG(Value)", "").ToString());
                hiddenAVG.Value = Math.Round(AVG, 2).ToString();
                gvVoyage.FooterRow.Cells[4].Text = "Average: " + Math.Round(AVG, 2).ToString();
                gvVoyage.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                gvVoyage.FooterRow.Cells[4].ForeColor = System.Drawing.Color.Black;
                gvVoyage.FooterRow.Cells[4].BorderColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                gvVoyage.FooterRow.Cells[3].BorderColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                gvVoyage.FooterRow.Cells[2].BorderColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                gvVoyage.FooterRow.Cells[1].BorderColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                gvVoyage.FooterRow.Cells[0].BorderColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                //txtGoal.Text = ds.Tables[1].Rows[0]["Goal"].ToString();
                gvVoyage.FooterRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#abcdef");
                hiddengVoyageCount.Value = "1";
            }
            else
            {
                hiddengVoyageCount.Value = "0";
            }
            getGoal();
        }
    }

    /// <summary>
    /// Method used to changed the colure of row based on effecency values
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        try
        {
            var c = e.Row.FindControl("lblPORT_NAME") as Label;
            if (c != null)
            {
                if ((Convert.ToDouble(c.Text) < Convert.ToDouble(txtGoal.Text)))
                {
                    e.Row.BackColor = System.Drawing.Color.White;
                }
                if (Convert.ToDouble(c.Text) > Convert.ToDouble(txtEEDI.Text))
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE");
                }
                else if ((Convert.ToDouble(c.Text) > Convert.ToDouble(txtGoal.Text)) && (Convert.ToDouble(c.Text) < Convert.ToDouble(txtEEDI.Text)))
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FABF8F");
                }

                //else
                //    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE");
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Method used to changed the colure of row based on effecency values voyages
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvVoyage_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            var c = e.Row.FindControl("lblTo4") as Label;
            if (c != null && txtGoal.Text != "")
            {
                if (Convert.ToDouble(c.Text) > 0)
                {
                    if ((Convert.ToDouble(c.Text) < Convert.ToDouble(txtGoal.Text)))
                    {
                        e.Row.BackColor = System.Drawing.Color.White;
                    }
                    if (Convert.ToDouble(c.Text) > Convert.ToDouble(txtEEDI.Text))
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE");
                    }
                    else if ((Convert.ToDouble(c.Text) > Convert.ToDouble(txtGoal.Text)) && (Convert.ToDouble(c.Text) < Convert.ToDouble(txtEEDI.Text)))
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FABF8F");
                    }

                }


            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Method to export KPI values to Excel file 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (checkVoyage.Checked == false)
            {


                if (txtStartDate.Text != "")
                {
                    Startdate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text));
                }
                if (txtEndDate.Text != "")
                {
                    EndDate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtEndDate.Text));
                }

                DataTable dt = BLL_TMSA_PI.Search_PI_Values(Convert.ToInt32(ddlvessel.SelectedValue), KPI_ID, Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtEndDate.Text).ToString("yyyy-MM-dd")).Tables[0];

                string[] HeaderCaptions = { "Record Date", "EEOI Value" };
                string[] DataColumnsName = { "Record_Date_Str", "Value" };
                GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "CO2 Efficiency-" + ddlvessel.SelectedItem.Text, "CO2 Efficiency-" + ddlvessel.SelectedItem.Text);

            }
            else
            {
                DataTable liTable = new DataTable();
                if (listVoyage.Items.Count > 0)
                {
                    for (int x = 0; x < listVoyage.Items.Count; x++)
                    {
                        //string v1 = listVoyage.Items[0].Value.Split(':')[1].Trim();
                        //string v2 = listVoyage.Items[listVoyage.Items.Count - 1].Value.Split(':')[0].Trim();
                        string val = listVoyage.Items[x].Value.Split(':')[0] + ":" + listVoyage.Items[x].Value.Split(':')[1];

                        DataTable dtq = BLL_TMSA_PI.GetTelDate(val.Trim(), Convert.ToInt32(ddlvessel.SelectedValue)).Tables[0];
                        if (dtq.Rows[0]["Telegram_Date_DDMM"].ToString() != "")
                        {
                            Startdate = Convert.ToDateTime(dtq.Rows[0]["Telegram_Date_DDMM"].ToString());
                        }
                        if (dtq.Rows[dtq.Rows.Count - 1]["Telegram_Date_DDMM"].ToString() != "")
                        {
                            EndDate = Convert.ToDateTime(dtq.Rows[dtq.Rows.Count - 1]["Telegram_Date_DDMM"].ToString());
                        }


                        DataTable dt = BLL_TMSA_PI.GetVoyageData(listVoyage.Items[x].Value.Trim(), Convert.ToInt32(ddlvessel.SelectedValue),KPI_ID).Tables[0];

                        liTable.Merge(dt);
                    }
                    string[] HeaderCaptions = { "From Port", "To Port", "Departure Date", "Arrival Date", "Average" };
                    string[] DataColumnsName = { "FromPort", "ToPort", "EffectiveFrom", "EffectiveTo", "Value" };
                    GridViewExportUtil.ExportToExcel(liTable, HeaderCaptions, DataColumnsName, "CO2 Efficiency-" + ddlvessel.SelectedItem.Text, "CO2 Efficiency(Voyage)-" + ddlvessel.SelectedItem.Text);
                }
            }
        }
        catch (Exception ex)
        { }

    }

    protected void bindgvSearch()
    {
        try
        {
            if (txtStartDate.Text != "")
            {
                Startdate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text));
            }
            if (txtEndDate.Text != "")
            {
                EndDate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtEndDate.Text));
            }
            DataTable dtt = objEEDIBLL.Get_EEDI(Convert.ToInt32(ddlvessel.SelectedValue));
            if (dtt.Rows.Count != 0)
            {
                txtEEDI.Text = Math.Round(Convert.ToDecimal(dtt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
            }
            else
            {
                txtEEDI.Text = "0";
            }

            DataTable dt = objKPI.GetGoal(Convert.ToInt32(ddlvessel.SelectedValue), "KPI005", 0);

            if (dt.Rows.Count > 0)
                txtGoal.Text = Math.Round(Convert.ToDecimal(dt.Rows[0]["Goal"].ToString()), 2).ToString();
            else
                txtGoal.Text = "0.00";
            DataTable dt2 = BLL_TMSA_PI.Search_PI_Values(Convert.ToInt32(ddlvessel.SelectedValue), KPI_ID, Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text)).ToString("yyyy-MM-dd"), Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtEndDate.Text)).ToString("yyyy-MM-dd")).Tables[0];
            if (dt2 == null || dt2.Rows.Count == 0)
            {
                btnChart.Visible = false;
                btnExport.Visible = false;
            }
            gvSearch.DataSource = dt2;
            gvSearch.DataBind();

            if (dt2.Rows.Count != 0)
            {
                double AVG = Convert.ToDouble(dt2.Compute("AVG(Value)", "").ToString());
                hiddenAVG.Value = Math.Round(AVG, 2).ToString();
                gvSearch.FooterRow.Cells[1].Text = "Average: " + Math.Round(AVG, 2).ToString();
                gvSearch.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                gvSearch.FooterRow.Cells[1].ForeColor = System.Drawing.Color.Black;
                gvSearch.FooterRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#abcdef");
                gvSearch.FooterRow.Cells[1].BorderColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                gvSearch.FooterRow.Cells[0].BorderColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                //  txtGoal.Text = ds.Tables[1].Rows[0]["Goal"].ToString();
                //btnChart.Visible = true;
                hiddengdCount.Value = "1";
                btnExport.Visible = true;
                divChart.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "showChart();", true);
            }
            else
            {
                hiddengdCount.Value = "0";
                btnChart.Visible = false;
            }
        }
        catch(Exception ex) {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void gvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSearch.PageIndex = e.NewPageIndex;
        bindgvSearch();
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "showChart();", true);
    }
    protected void gvVoyage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvVoyage.PageIndex = e.NewPageIndex;
        BindVoyageData();
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "showChartVoyage();", true);
    }
}