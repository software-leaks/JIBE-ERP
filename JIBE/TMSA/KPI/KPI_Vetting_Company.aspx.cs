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
using AjaxControlToolkit;



public partial class KPI_Vetting_Company : System.Web.UI.Page
{
    BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
    public UserAccess objUA = new UserAccess();
    public string[] Vessel_Ids ;
    string start_date;
    string end_date;
    protected void Page_Load(object sender, EventArgs e)
    {
        CalStartDate.Format = UDFLib.GetDateFormat();   //Display the selected date from datepicker as in the same format which has been selected by the user.
        CalEndDate.Format = UDFLib.GetDateFormat();
       // UserAccessValidation();
        if (!IsPostBack)
        {
            txtStartDate.Text = DateTime.Now.AddYears(-1).ToString(UDFLib.GetDateFormat());
            txtEndDate.Text = DateTime.Now.ToString(UDFLib.GetDateFormat());
            hiddenStartDate.Value = UDFLib.ConvertToDefaultDt(txtStartDate.Text);
            hiddenEndDate.Value = UDFLib.ConvertToDefaultDt(txtEndDate.Text);
            //LoadKPIList();
            LoadData();
            IntializeControls();
            loadPIDetails();
            GenearteDiv();
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



    }


    private void ClearSearch()
    {
        txtStartDate.Text = DateTime.Now.AddYears(-1).ToString(UDFLib.GetDateFormat());
        txtEndDate.Text = DateTime.Now.ToString(UDFLib.GetDateFormat());



    }

    /// <summary>
    /// Method use to intialize the default Quarter and Default KPI for selected 
    /// </summary>
    protected void IntializeControls()
    {
        DataTable dt= objKPI.Get_Vetting_KPI_List();
        int firstKPI_Id=0;
        string Kpi_Name = "";
        if (dt.Rows.Count > 0)
        {
            firstKPI_Id = Convert.ToInt16(dt.Rows[0]["KPI_ID"]);
            Kpi_Name= dt.Rows[0]["Name"].ToString();
            ViewState["KPI_ID"] = firstKPI_Id;

            hdnKpi_ID.Value = firstKPI_Id.ToString();
            hdnKpi_Name.Value = Kpi_Name;

            DateTime EndDate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtEndDate.Text));
            
            int IMonth = 0;
            int Year = 2017;
            int Qtr=1;
            IMonth = EndDate.Month;
            Year = EndDate.Year;

            if (IMonth < 4)
                Qtr = 1;
            else if ((IMonth > 3 && IMonth <= 6))
                Qtr = 2;
            else if ((IMonth > 6 && IMonth <= 9))
                Qtr = 3;
            else
                Qtr = 4;

            hdnQtr.Value = "Q" + Qtr.ToString() + "-" + Year.ToString();

        }

    }


    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
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

    protected void LoadKPIList()
    {
      // DataTable dt= objKPI.Get_Vetting_KPI_List();
       //ddlKPIList.DataValueField = "KPI_ID";
       //ddlKPIList.DataTextFormatString = "Name";
       //ddlKPIList.DataSource = dt;

    }


    /// <summary>
    /// Description: Method to load data based on selected vessel and date range 
    /// </summary>
    private void LoadData()
    {
        try
        {


            hdnVessel_IDs.Value = null;
            string Qtr = "";
            string sStartDate = txtStartDate.Text;
            string sEndDate = txtEndDate.Text;
            if (sStartDate != "" && sEndDate != "")
            {
                DateTime StartDate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text));      //ConvertToDefaultDt() method is used to change the user selected date format to the default format, the format in which date is saved in database.
                DateTime EndDate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtEndDate.Text));

                //hdnLastMonth.Value = lastMonth;
                DataSet ds = objKPI.Get_Vetting_KPI_ByCompany(Qtr,0, StartDate, EndDate);
                DataTable dt = ds.Tables[0];
                string[] Pkey_cols = new string[] { "KPI_ID" };
                string[] Hide_cols = new string[] { "ID", "Qtr_Start", "Qtr_End" ,"Code"};
                DataTable dt1 = objKPI.PivotTable("Qtr", "KPI_Value", "", Pkey_cols, Hide_cols, dt);

                gvCompanyKPI.DataSource = dt1;
                gvCompanyKPI.DataBind();

                IntializeControls();
                string jsMethodName = null;
                jsMethodName = "$(document).ready(function () {searchInitialize();});";
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "uniqueKey", jsMethodName, true);


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
    protected void gvCompanyKPI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;

        if (e.Row.RowType == DataControlRowType.Header)
        {

            string Vessel_Id = hdnVessel_IDs.Value;

            for (int i = 0; i < e.Row.Cells.Count; i++)
            {

                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;


                if (i >3)
                {
                    string[] arrQtr = e.Row.Cells[i].Text.Split('-');
                    string lastQtr = arrQtr[0].ToString();
                    string lastYear = arrQtr[1].ToString();
                    lastYear = lastQtr + "-" + lastYear;
                    e.Row.Cells[i].Text = "";
                    HyperLink hp = new HyperLink();
                    hp.Attributes.Add("href", "#");
                    hp.Attributes.Add("Class", "chkheaderSelected");
                    hp.Text = lastYear;
                    e.Row.Cells[i].Controls.Add(hp);
                    e.Row.Cells[i].Attributes.Add("onclick", "$(document).ready(function () {showChart('" + lastYear + "')})");

                }
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i > 0)
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;

            }

           

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
            hdnVessel_IDs.Value = null;

        }
    }
    



    protected void loadPIDetails()
    {
        int iKPI_ID = 1;
        if (ViewState["KPI_ID"] != null)
            iKPI_ID = Convert.ToInt16(ViewState["KPI_ID"]);
        
        //DataSet ds = BLL_TMSA_PI.Get_KPI_Detail(iKPI_ID);
        //DataTable dt = new DataTable();
        //DataTable dtPIDtl = ds.Tables[1];
        //dt.Columns.Add(new DataColumn("sno", typeof(int)));
        //dt.Columns.Add(new DataColumn("PID", typeof(string)));
        //dt.Columns.Add(new DataColumn("value", typeof(string)));
        //dt.Columns.Add(new DataColumn("PIName", typeof(string)));
        //string exp = "";
        //foreach (DataRow row in dtPIDtl.Rows)
        //{
        //    dt.Rows.Add(new object[] { Convert.ToInt32(row["SerialNumber"].ToString()), row["PI_ID"].ToString(), row["value"].ToString(), row["Name"].ToString() });
        //    exp = exp + row["value"].ToString();
        //}
        //lblFormula.Text = "KPI Formula : " + exp;
        //DataTable dt_PI = dt;
        //dt_PI.DefaultView.RowFilter = "PID <> ''";
        //DataList1.DataSource = dt_PI.DefaultView;
        //DataList1.DataBind();

    }




    public void item_click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkVessel = (LinkButton)sender;
            GridViewRow gvrSelected = (GridViewRow)lnkVessel.NamingContainer;
            foreach (GridViewRow gvr in gvCompanyKPI.Rows)
            {
                gvr.Cells[0].BackColor = System.Drawing.Color.White;

            }
            gvrSelected.Cells[0].BackColor = System.Drawing.Color.Yellow;


            HiddenField hdf = (HiddenField)gvrSelected.FindControl("hdKPIID");
            HiddenField hdf2 = (HiddenField)gvrSelected.FindControl("hdnKpiName");



            start_date = UDFLib.ConvertToDefaultDt(txtStartDate.Text);
            end_date = UDFLib.ConvertToDefaultDt(txtEndDate.Text);
            string jsMethodName = null;

            jsMethodName = "showChart2('" + Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text)).ToString("yyyy-MM-dd") + "','" + Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtEndDate.Text)).ToString("yyyy-MM-dd") + "','" + hdf.Value + "','" + hdf2.Value + "')";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", jsMethodName, true);
        }

        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex); 
        }

    }


    /// <summary>
    /// Method to export values displayed on the grid applying same search criteria
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            hdnVessel_IDs.Value = null;
            string Qtr = "";
            string sStartDate = txtStartDate.Text;
            string sEndDate = txtEndDate.Text;
            if (sStartDate != "" && sEndDate != "")
            {
                DateTime StartDate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtStartDate.Text));      //ConvertToDefaultDt() method is used to change the user selected date format to the default format, the format in which date is saved in database.
                DateTime EndDate = Convert.ToDateTime(UDFLib.ConvertToDefaultDt(txtEndDate.Text));

                //hdnLastMonth.Value = lastMonth;
                DataSet ds = objKPI.Get_Vetting_KPI_ByCompany(Qtr, 0, StartDate, EndDate);
                DataTable dt = ds.Tables[0];
                string[] Pkey_cols = new string[] { "KPI_ID" };
                string[] Hide_cols = new string[] { "ID", "Qtr_Start", "Qtr_End", "Code" };
                DataTable dt1 = objKPI.PivotTable("Qtr", "KPI_Value", "", Pkey_cols, Hide_cols, dt);



                string[] HeaderCaptions = new string[dt1.Columns.Count-1];
                string[] DataColumnsName = new string[dt1.Columns.Count-1];
                string ColumnName;
                int i = 0;
                foreach (DataColumn column in dt1.Columns)
                {
       
                        ColumnName = column.ColumnName;
                        if (ColumnName != "KPI_ID")
                        {
                            if (ColumnName == "KPI_Name")
                                HeaderCaptions[i] = "KPI Name";
                            else
                                HeaderCaptions[i] = ColumnName;
                            DataColumnsName[i] = ColumnName;
                        }
                    
                    i++;
                }

                GridViewExportUtil.ExportToExcel(dt1, HeaderCaptions, DataColumnsName, "Vetting Company KPIs", "Vetting Company KPIs");
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


        }



    /// <summary>
    /// Description: Method to create the dynamic list of categorywise graph
    /// Baseed on Crew rank category, graph will be creted
    /// </summary>
    private void GenearteDiv()
    {
        try
        {
            string sContainer = "chartContainer_";
            string sGContainer = "gridContainer_";

            PlaceHolder1.Controls.Clear();
            int rowcount = 0;
            DataTable dt = objKPI.Get_Vetting_KPI_List();
          
            string sKPIId = "";
            string sKPIName = "";
            hdnKPI_IDList.Value = sKPIId;
            hdnKPI_NameList.Value = sKPIName;
            string sKpi_ID = hdnKpi_ID.Value;
            //if (sKpi_ID != null)
            //{
            //    DataRow[] drr = dt.Select("KPI_ID=' " + sKpi_ID + " ' ");
            //    for (int i = 0; i < drr.Length; i++)
            //        drr[i].Delete();
            //    dt.AcceptChanges();
            //}
            rowcount = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["KPI_ID"].ToString() != hdnKpi_ID.Value)
                {

                    if (hdnKPI_IDList.Value == "")
                    {
                        hdnKPI_NameList.Value = dr["Name"].ToString();
                        hdnKPI_IDList.Value = dr["KPI_ID"].ToString();
                    }
                    else
                    {
                        hdnKPI_NameList.Value = hdnKPI_NameList.Value + "," + dr["Name"].ToString();
                        hdnKPI_IDList.Value = hdnKPI_IDList.Value + "," + dr["KPI_ID"].ToString();
                    }
                }
            }
            hdnKPI_NameList.Value = hdnKPI_NameList.Value.Trim(',');
            hdnKPI_IDList.Value = hdnKPI_IDList.Value.Trim(',');
            int totalCategories = dt.Rows.Count;
            int tblRows = 1;
            int tblCols = 2;//--do--
            //int tblCols = 1;//--do--
            if (totalCategories > 1)
            {
                if (totalCategories < 3)
                {
                    tblRows = 1;


                }
                else if (totalCategories < 5)
                {
                    tblRows = 2;

                }
                else
                {
                    tblRows = (int)Math.Ceiling((double)totalCategories / 2);
                }

            }


            Table tbl = new Table();
            tbl.Attributes.Add("align", "center");
            int catindex = 0;
            string CategoryId = "";
            string CategoyName = "";
            PlaceHolder1.Controls.Add(tbl);
            string sURL;

            //TableRow tr = new TableRow();
            int iCount = 0;
            for (int i = 0; i < tblRows; i++)
            {
                TableRow tr = new TableRow();
                for (int j = 0; j < tblCols; j++)
                {

                    if (iCount < rowcount)
                    {
                        TableCell tc = new TableCell();

                        CategoryId = dt.Rows[iCount]["KPI_ID"].ToString();
                        CategoyName = dt.Rows[iCount]["Name"].ToString();
                        catindex++;
                        iCount++;

                        HtmlGenericControl newControl = new HtmlGenericControl("div");
                        HiddenField hdnCatId = new HiddenField();
                        HiddenField hdnCatName = new HiddenField();
                        //hdnCatId.ID = "hdnID_" + i + j;
                        //hdnCatName.ID = "hdnName_" + i + j;
                        hdnCatId.ID = "hdnID_" + i;
                        hdnCatName.ID = "hdnName_" + i;
                        newControl.ID = sContainer + i;
                        newControl.Attributes.Add("Style", "Height:300px;width:100%;float:left; display:inline-block; overflow:hidden;");
                        newControl.InnerHtml = "";
                        tc.Controls.Add(newControl);


                        HtmlGenericControl newGridControl = new HtmlGenericControl("div");

                        hdnCatId.ID = "hdnID_" + i + j;
                        hdnCatName.ID = "hdnName_" + i + j;

                        newControl.ID = sContainer + i + "_" + j;

                        newGridControl.Attributes.Add("Style", "Height:20px;width:700px;float:left;");

                        newGridControl.InnerHtml = "";
                        tc.Controls.Add(newGridControl);

                        hdnCatId.Value = CategoryId;
                        hdnCatName.Value = CategoyName;
                        tc.Controls.Add(hdnCatId);
                        tc.Controls.Add(hdnCatName);
                        tr.Cells.Add(tc);

                    }
                    TableRow tr1 = new TableRow();
                    TableCell td = new TableCell();
                    HtmlGenericControl labelControl1 = new HtmlGenericControl("div");
                    labelControl1.ID = "header_" + i;
                    labelControl1.InnerHtml = CategoyName;
                    labelControl1.Attributes.Add("Style", "Height:50px;width:850px;float:right; font-weight:bold;font-size: 14px;");
                    td.Controls.Add(labelControl1);
                    tr1.Cells.Add(td);
                }
            

                tbl.Rows.Add(tr);
            }

            hiddenCount.Value = tblRows.ToString();
            hiddenCount1.Value = tblCols.ToString();


        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }




}