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
    DateTime? Startdate = null;
    DateTime? EndDate = null;
    int KPI_ID = 1;
    public UserAccess objUA = new UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        CalStartDate.Format = UDFLib.GetDateFormat();      //Display the selected date from datepicker as in the same format which has been selected by the user.
        CalEndDate.Format = UDFLib.GetDateFormat();
        UserAccessValidation();
        if (!IsPostBack)
        {
            txtStartDate.Text = DateTime.Now.AddDays(-30).ToString(UDFLib.GetDateFormat());
            txtEndDate.Text = DateTime.Now.ToString(UDFLib.GetDateFormat());
            //txtStartDate.Text = DateTime.Now.AddDays(-30).ToString("dd-MM-yyyy");
            //txtEndDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            hiddenStartDate.Value = UDFLib.ConvertToDefaultDt(txtStartDate.Text);
            hiddenEndDate.Value = UDFLib.ConvertToDefaultDt(txtEndDate.Text);
            BindVesselList();
            loadFormula();
            bindgvSearch();
            gvSearch.Visible = true;
        }
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


    protected void GetGoal()
    {
        DataTable dtKPI = objKPI.GetGoal(0, "KPI021", 0);
        string KPI_ID = dtKPI.Rows[0]["KPI_ID"].ToString();
        hiddenKPIID.Value = KPI_ID;
    }
    private void loadFormula()
    {
        GetGoal();

        DataSet ds = BLL_TMSA_PI.Get_KPI_Detail(UDFLib.ConvertIntegerToNull(KPI_ID));

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
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
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
        ddlvessel.DataTextField = "VESSEL_NAME";
        ddlvessel.DataValueField = "VESSEL_ID";
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
        ImgComp.Visible = false;
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

                if (checkVoyage.Checked)
                {
                    bindVoyage();
                    BindVoyageData();
                    gvSearch.Visible = false;
                    gvVoyage.Visible = true;
                    imgADD.Enabled = true;
                    divVoyage.Visible = true;


                }
                else
                {
                    bindgvSearch();
                    gvSearch.Visible = true;
                    gvVoyage.Visible = false;
                    imgADD.Enabled = false;
                }

                ImgComp.Visible = true;
                listVoyage.Items.Clear();
               
                int KPI_ID = 1;
            }
            else
            {
                clear();
                string msg2 = String.Format("alert('Start Date should not be greater than End Date  ')");
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
        //divChart.Visible = false;
        if (ddlvessel.SelectedIndex != 0)
        {
            hiddenVessel.Value = ddlvessel.SelectedValue;
            gvSearch.Visible = false;
            checkVoyage.Checked = false;
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
          
            ddlVoyage.Enabled = true;
            listVoyage.Enabled = true;
            imgADD.Enabled = true;
            divChart.Visible = true;
            bindVoyage();
            btnVoyage.Visible = true;
            btnChart.Visible = false;
            gvSearch.Visible = false;
            divVoyage.Visible = true;
            ddlVoyage.Enabled = true;
            //  gvVoyage.Visible = true;
        }
        else
        {

            txtStartDate.Enabled = true;
            txtEndDate.Enabled = true;
            ddlVoyage.Enabled = false;
            listVoyage.Enabled = false;
            imgADD.Enabled = false;
            listVoyage.Items.Clear();
            btnVoyage.Visible = false;
            //btnChart.Visible = true;
            bindgvSearch();
            gvSearch.Visible = true;
            gvVoyage.Visible = false;
            
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



                DataTable dt = BLL_TMSA_PI.GetVoyageDataNOx(listVoyage.Items[x].Value.Trim(), Convert.ToInt32(ddlvessel.SelectedValue)).Tables[0];

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
                //   txtGoal.Text = ds.Tables[1].Rows[0]["Goal"].ToString();
                gvVoyage.FooterRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#abcdef");
                hiddengVoyageCount.Value = "1";
            }
            else
            {
                hiddengVoyageCount.Value = "0";
            }

            for (int i = 0; i < gvVoyage.Rows.Count; i++)
            {

                Label lbl = (Label)gvVoyage.Rows[i].Cells[1].FindControl("lblTo4");
                if (Convert.ToDouble(lbl.Text) > Convert.ToDouble(hiddenAVG.Value))
                {
                    gvVoyage.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE");
                }
                else
                {
                    gvVoyage.Rows[i].BackColor = System.Drawing.Color.White;
                }

            }

        }
        GetGoal();
    }
    protected void gvSearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvVoyage_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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

        lblFormula.Text = "";
    }
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
                DataTable dt = BLL_TMSA_PI.Search_PI_ValuesNOX(Convert.ToInt32(ddlvessel.SelectedValue), Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtEndDate.Text).ToString("yyyy-MM-dd")).Tables[0];

                string[] HeaderCaptions = { "Record_Date", "Value" };
                string[] DataColumnsName = { "Record_Date", "Value" };
                GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "NOX Effeciency-" + ddlvessel.SelectedItem.Text, "NOX Effeciency-" + ddlvessel.SelectedItem.Text);

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



                        DataTable dt = BLL_TMSA_PI.GetVoyageDataNOx(listVoyage.Items[x].Value.Trim(), Convert.ToInt32(ddlvessel.SelectedValue)).Tables[0];

                        liTable.Merge(dt);
                    }
                    string[] HeaderCaptions = { "From Port", "To Port", "Effective From", "Effective To", "Efficiency" };
                    string[] DataColumnsName = { "FromPort", "ToPort", "EffectiveFrom", "EffectiveTo", "Value" };
                    GridViewExportUtil.ExportToExcel(liTable, HeaderCaptions, DataColumnsName,"NOX Effeciency-" + ddlvessel.SelectedItem.Text , "NOX Effeciency(Voyage)-" + ddlvessel.SelectedItem.Text);
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
            DataSet ds = BLL_TMSA_PI.Search_PI_ValuesNOX(Convert.ToInt32(ddlvessel.SelectedValue), Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text)).ToString("yyyy-MM-dd"), Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtEndDate.Text)).ToString("yyyy-MM-dd"));
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                btnChart.Visible = false;
                btnExport.Visible = false;
            }
            //else
            //    btnChart.Visible = true;
            gvSearch.DataSource = ds.Tables[0];
            gvSearch.DataBind();

            if (ds.Tables[0].Rows.Count != 0)
            {
                double AVG = Convert.ToDouble(ds.Tables[0].Compute("AVG(Value)", "").ToString());
                hiddenAVG.Value = Math.Round(AVG, 2).ToString();
                gvSearch.FooterRow.Cells[1].Text = "Average: " + Math.Round(AVG, 2).ToString();
                gvSearch.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                gvSearch.FooterRow.Cells[1].ForeColor = System.Drawing.Color.Blue;
                gvSearch.FooterRow.Cells[1].BorderColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                gvSearch.FooterRow.Cells[0].BorderColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
                hiddengdCount.Value = "1";
                btnExport.Visible = true;
                divChart.Visible = true;
                hiddenStartDate.Value = "";
                hiddenEndDate.Value = "";
                hiddenStartDate.Value = UDFLib.ConvertToDefaultDt(txtStartDate.Text);
                hiddenEndDate.Value = UDFLib.ConvertToDefaultDt(txtEndDate.Text);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "showChart();", true);
            }
            else
            {
                hiddengdCount.Value = "0";
            }
            for (int i = 0; i < gvSearch.Rows.Count; i++)
            {

                Label lbl = (Label)gvSearch.Rows[i].Cells[1].FindControl("lblPORT_NAME");
                if (Convert.ToDouble(lbl.Text) > Convert.ToDouble(hiddenAVG.Value))
                {
                    gvSearch.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC7CE");
                }
                else
                {
                    gvSearch.Rows[i].BackColor = System.Drawing.Color.White;
                }

            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void gvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSearch.PageIndex = e.NewPageIndex;
        bindgvSearch();
    }
    protected void gvVoyage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvVoyage.PageIndex = e.NewPageIndex;
        BindVoyageData();
    }
}