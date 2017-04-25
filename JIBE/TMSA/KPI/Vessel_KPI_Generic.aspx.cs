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


public partial class TMSA_KPI_Vessel_KPI_Generic : System.Web.UI.Page
{
    BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
    BLL_TMSA_EEDI objEEDIBLL = new BLL_TMSA_EEDI();
    int KPI_ID = 1;
    DateTime? Startdate = null;
    DateTime? EndDate = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindVesselList();
            loadFormula();
            loadKPIs();
            //getGoal();
            BindInterval();
        }
      
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

    private void getGoal()
    {
        //string KPI_Code = "";
        //int KPI_ID;
        //if (ddlKPIList.SelectedValue == null || ddlKPIList.SelectedValue == "")
        //    KPI_ID = 0;
        //else
        //    KPI_ID = Convert.ToInt32(ddlKPIList.SelectedValue);
        //DataTable dt = objKPI.GetGoal(0, KPI_Code, KPI_ID);
        //if (dt.Rows.Count > 0)
        //{
        //    hiddenKPIID.Value = dt.Rows[0]["KPI_ID"].ToString();
        //    txtGoal.Text = Math.Round(Convert.ToDecimal(dt.Rows[0]["Goal"].ToString()), 2).ToString();
        //}
        //else
        //{
        //    lblGoal.Visible = false;
        //    txtGoal.Text = "0.00";
        //    txtGoal.Visible = false;
        //}
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
        if (exp != "")
        {
            lblFormula.Text = "KPI Formula : " + exp;
            lblFormula.Visible = true;
        }
    }
    protected void BindVesselList()
    {

    //    DataTable dt = objKPI.Get_KPI_DetailGoals(-1).Tables[0];
        BLL_Infra_VesselLib bll_Vessel = new BLL_Infra_VesselLib();

        DataTable dt = bll_Vessel.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
        dt.Columns.Remove("code");
        dt.Columns.Remove("name");
        dt.Columns.Remove("fleetname");
        dt.Columns.Remove("Super_MailID");
        dt.Columns.Remove("TechTeam_MailID");
        dt.Columns.Remove("Vessel_Owner");
        dt.Columns.Remove("Vessel_Manager");
        DataTable dtable = objKPI.Get_Fleet_Vessel_List(dt, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID());
        ddlvessel.DataSource = dtable;
        ddlvessel.DataTextField = "Vessel_Name";
        ddlvessel.DataValueField = "Vessel_Id";
        ddlvessel.DataBind();
        ddlvessel.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void btnportfilter_Click(object sender, ImageClickEventArgs e)
    {
        //txtGoal.Text = "";
        imgADD.Enabled = false;
        //imgComp.Visible = true;
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
        divVoyage.Visible=true;
        divGrid.Visible = true;
      
        listVoyage.Items.Clear();
   
        //txtGoal.Visible = true;
        //lblGoal.Visible = true;
        divChart.Visible = true;
        gvSearch.Visible = true;
       // btnVoyage.Visible = true;
        gvVoyage.Visible = false;
        if (checkVoyage.Checked)
        {
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
    protected void ddlvessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtGoal.Text = "";


        //divChart.Visible = false;
        //if (ddlvessel.SelectedIndex != 0)
        //{
        //    hiddenVessel.Value = ddlvessel.SelectedValue;
        //    gvSearch.Visible = false;
        //}
        //else
        //{
        //    hiddenVessel.Value = "-1";
        //}
    }


    protected void bindVoyage()
    {

        if (txtStartDate.Text != "" && txtEndDate.Text != "")
        {
            DateTime Startdate = Convert.ToDateTime(txtStartDate.Text);
            DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);
            int Vessel_Id = Convert.ToInt32(ddlvessel.SelectedValue);
            DataTable dt = objKPI.GetVoyageList(Vessel_Id, Startdate, EndDate);
            ddlVoyage.DataSource = dt;
            ddlVoyage.DataTextField = "VOYAGE";
            ddlVoyage.DataValueField = "TelID";

            ddlVoyage.DataBind();
            ddlVoyage.Items.Insert(0, new ListItem("-Select-", "0"));
        }

    }
    protected void checkVoyage_CheckedChanged(object sender, EventArgs e)
    {
        if (checkVoyage.Checked)
        {
            //txtEndDate.Text = "";
            //txtStartDate.Text = "";
            txtStartDate.Enabled = false;
            txtEndDate.Enabled = false;
            ddlVoyage.Enabled = true;
            listVoyage.Enabled = true;
            imgADD.Enabled = true;
            bindVoyage();
            btnVoyage.Visible = true;
            btnChart.Visible = false;
            gvSearch.Visible = false;
            rdListValue.Visible = false;
            lblinterval.Visible = false;
            ddlInterval.Visible = false;
        }
        else
        {
            rdListValue.Visible = true;
            lblinterval.Visible = true;
            ddlInterval.Visible = true;
            txtStartDate.Enabled = true;
            txtEndDate.Enabled = true;
            ddlVoyage.Enabled = false;
            listVoyage.Enabled = false;
            imgADD.Enabled = false;
            listVoyage.Items.Clear();
            btnVoyage.Visible = false;
            btnChart.Visible = true;
            gvSearch.Visible = true;
            gvVoyage.Visible = false;

        }
    }
    protected void imgADD_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlVoyage.SelectedItem != null)
        {
            if (ddlVoyage.SelectedIndex == 0)
            {
                if (!listVoyage.Items.Contains(ddlVoyage.SelectedItem))
                {
                    listVoyage.Items.Add(ddlVoyage.SelectedItem);
                }

                int Vessel_Id = Convert.ToInt32(ddlvessel.SelectedValue);

                DateTime Startdate = Convert.ToDateTime(txtStartDate.Text);
                DateTime EndDate = Convert.ToDateTime(txtEndDate.Text);

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
                ddlVoyage.DataTextField = "Voyage";
                ddlVoyage.DataValueField = "TelID";
                ddlVoyage.DataBind();
                hdnEnd.Value = "";
                hdnStart.Value = "";
                BindVoyageData();
            }
            else
            {
                string msg2 = String.Format("alert('Please select first item of voyage list')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            }
        }
       

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(Request.RawUrl);
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
                KPI_ID = Convert.ToInt32(hiddenKPIID.Value);
                DataTable dtq = BLL_TMSA_PI.GetTelDate(val.Trim(), Convert.ToInt32(ddlvessel.SelectedValue)).Tables[0];
                if (dtq.Rows[0][0].ToString() != "")
                {
                    Startdate = Convert.ToDateTime(dtq.Rows[0][0].ToString());
                }
                if (dtq.Rows[dtq.Rows.Count - 1][0].ToString() != "")
                {
                    EndDate = Convert.ToDateTime(dtq.Rows[dtq.Rows.Count - 1][0].ToString());
                }


                DataTable dt = objKPI.GetVoyageGenericData(listVoyage.Items[x].Value.Trim(), Convert.ToInt32(ddlvessel.SelectedValue), KPI_ID, Startdate, EndDate).Tables[0];

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
        }
    }



    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        if (checkVoyage.Checked == false)
        {


            if (txtStartDate.Text != "")
            {
                Startdate = Convert.ToDateTime(txtStartDate.Text);
            }
            if (txtEndDate.Text != "")
            {
                EndDate = Convert.ToDateTime(txtEndDate.Text);
            }
            string Value_Type = rdListValue.SelectedValue;
            KPI_ID = Convert.ToInt32(ddlKPIList.SelectedValue);
            DataTable dt = objKPI.Get_Vessel_KPI_Values(Convert.ToInt32(ddlvessel.SelectedValue), KPI_ID, ddlInterval.SelectedValue, Value_Type, Startdate, EndDate).Tables[0];

            string[] HeaderCaptions = { "Record Duration", "KPI Value" };
            string[] DataColumnsName = { "Record_Date_Str", "Value" };
            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, ddlvessel.SelectedItem.Text, ddlvessel.SelectedItem.Text);

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
                    if (dtq.Rows[0][0].ToString() != "")
                    {
                        Startdate = Convert.ToDateTime(dtq.Rows[0][0].ToString());
                    }
                    if (dtq.Rows[dtq.Rows.Count - 1][0].ToString() != "")
                    {
                        EndDate = Convert.ToDateTime(dtq.Rows[dtq.Rows.Count - 1][0].ToString());
                    }

                    DataTable dt = BLL_TMSA_PI.GetVoyageDataNOx(listVoyage.Items[x].Value.Trim(), Convert.ToInt32(ddlvessel.SelectedValue)).Tables[0];

                    liTable.Merge(dt);
                }
                string[] HeaderCaptions = { "Deperture Date", "Arrival Date", "From Port", "To Port", "Average" };
                string[] DataColumnsName = { "EffectiveFrom", "EffectiveTo", "FromPort", "ToPort", "Value" };
                GridViewExportUtil.ExportToExcel(liTable, HeaderCaptions, DataColumnsName, ddlvessel.SelectedItem.Text, ddlvessel.SelectedItem.Text);
            }
        }

    }

    protected void bindgvSearch()
    {
        if (txtStartDate.Text != "")
        {
            Startdate = Convert.ToDateTime(txtStartDate.Text);
        }
        if (txtEndDate.Text != "")
        {
            EndDate = Convert.ToDateTime(txtEndDate.Text);
        }

        string Value_Type = rdListValue.SelectedValue;
        KPI_ID = Convert.ToInt32(ddlKPIList.SelectedValue);
        DataSet ds = objKPI.Get_Vessel_KPI_Values(Convert.ToInt32(ddlvessel.SelectedValue), KPI_ID, ddlInterval.SelectedValue,Value_Type, Startdate, EndDate);

        gvSearch.DataSource = ds.Tables[0];
        gvSearch.DataBind();

        if (ds.Tables[0].Rows.Count != 0)
        {
            double AVG = Convert.ToDouble(ds.Tables[0].Compute("AVG(Value)", "").ToString());
            hiddenAVG.Value = Math.Round(AVG, 2).ToString();
            gvSearch.FooterRow.Cells[1].Text = "Average: " + Math.Round(AVG, 2).ToString();
            gvSearch.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            gvSearch.FooterRow.Cells[1].ForeColor = System.Drawing.Color.Black;
            gvSearch.FooterRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#abcdef");
            gvSearch.FooterRow.Cells[1].BorderColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
            gvSearch.FooterRow.Cells[0].BorderColor = System.Drawing.ColorTranslator.FromHtml("#EFEFEF");
            //  txtGoal.Text = ds.Tables[1].Rows[0]["Goal"].ToString();
            btnChart.Visible = true;
            hiddengdCount.Value = "1";
            btnExport.Visible = true;
        }
        else
        {
            hiddengdCount.Value = "0";
            btnChart.Visible = false;
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
    protected void ddlKPIList_SelectedIndexChanged(object sender, EventArgs e)
    {
        hiddenKPIID.Value = ddlKPIList.SelectedValue;
        loadFormula();
        
    }
    protected void ddlInterval_SelectedIndexChanged(object sender, EventArgs e)
    {
        hiddenInterval.Value = ddlInterval.SelectedValue;
    }
}