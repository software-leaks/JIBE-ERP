using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.Technical;
using EO.Pdf;
using System.IO;
using SMS.Business.Infrastructure;
using System.Globalization;
using SMS.Business.Inspection;
public partial class Technical_Worklist_SuptAttendanceCalender : System.Web.UI.Page
{
    BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
    BLL_Infra_Company objCom = new BLL_Infra_Company();
    public static Dictionary<int, Color> InspColor;
    DataTable dt = new DataTable();
    DataTable dtTempRows = new DataTable();
    int newtab = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string StartDate = "01/01/2015";

            if (Session["COMPANYTYPE"].ToString() == "Surveyor")
            {

                DDLCompany.Visible = true;
                lblCompany.Visible = true;
            }
            else
            {

                DDLCompany.Visible = false;
                lblCompany.Visible = false;
            }
            ViewState["CurrentYear"] = DateTime.Now.Year.ToString();
            ViewState["CurrentMonth"] = DateTime.Now.Month.ToString();
            ViewState["CurrentDay"] = "01";
            ViewState["CurrentDate"] = DateTime.Now;
            dtTempRows.Columns.Add("RowCellCount");

            ViewState["dtTempRows"] = dtTempRows;

            BindSupAttGrid(Convert.ToDateTime("01/" + ViewState["CurrentMonth"].ToString() + "/" + ViewState["CurrentYear"].ToString()));

            lblMonthYear.Text = Convert.ToDateTime("01/" + ViewState["CurrentMonth"].ToString() + "/" + ViewState["CurrentYear"].ToString()).ToString("MMMM yyyy");
            lblDate.Text = DateTime.Now.ToString("dd MMM yy");
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            imgLogo.ImageUrl = baseUrl + "Images/company_logo.jpg";


            ViewState["newtabnumber"] = newtab;


            //string js = "LastLoad()";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ddd", js, true);
        }


    }


    public void BindSupAttGrid(DateTime StartDate)
    {

        DataSet dtCompany = objCom.Get_Company_Parent_Child(1, UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()), 0); ;
        DDLCompany.DataSource = dtCompany.Tables[0];
        DDLCompany.DataTextField = "Company_Name";
        DDLCompany.DataValueField = "ID";
        DDLCompany.DataBind();
        DDLCompany.Items.Insert(0, new ListItem("-- ALL --", null));

        dt = objInsp.TEC_Get_SupritendentAttendanceWithPort(StartDate, UDFLib.ConvertToInteger(DDLCompany.SelectedValue));

        grdSupAtt.DataSource = dt;
        grdSupAtt.DataBind();

        //string js = "LastLoad()";
        // ScriptManager.RegisterStartupScript(this, this.GetType(), "ddd", js, true);
        //string[] retval = new string[2];
        //retval = asncLoadCalendarBySupt("0", StartDate.ToString());

        //newt0.InnerHtml = retval[0];
        //newt.InnerHtml = retval[2];

        
    }

    protected void grdSupAtt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            int FixCellCount = 3;
            int DayCount = 0;
            //e.Row.Cells[0].Visible = false;

            if (e.Row.RowType == DataControlRowType.Header)
            {

                TableHeaderCell thc = new TableHeaderCell();
                thc.Attributes.Add("SR.NO.", "SR.NO.");
                thc.Text = "Sr.No.";
                e.Row.Cells.AddAt(0, thc);
                e.Row.Cells[2].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TableCell tc = new TableCell();
                tc.Attributes.Add("SR.NO.", "SR.NO.");
                tc.Text = Convert.ToString(e.Row.RowIndex + 1);
                tc.HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells.AddAt(0, tc);
                for (int i = 0; i < FixCellCount; i++)
                {
                    e.Row.Cells[i].CssClass = "Fixed";
                    //e.Row.Cells[i].Attributes.Add("onclick", "window.open('../Crew/CrewDetails.aspx?ID=" + CrewID + "');");
                }
                e.Row.Cells[2].Visible = false;

                e.Row.Cells[1].Width = 800;

                DateTime CurDateNew = Convert.ToDateTime(Convert.ToString(1) + "/" + ViewState["CurrentMonth"].ToString() + "/" + ViewState["CurrentYear"].ToString());
                DateTime PreDateNew1 = CurDateNew.AddMonths(-1);
                for (int i = 0; i < DateTime.DaysInMonth(PreDateNew1.Year, PreDateNew1.Month); i++)
                {
                    // DateTime CurDateNew = Convert.ToDateTime(Convert.ToString(i - 1) + "/" + ViewState["CurrentMonth"].ToString() + "/" + ViewState["CurrentYear"].ToString());
                    DateTime PreDate = PreDateNew1.AddDays(i);
                    if (e.Row.Cells[2].Text != "&nbsp;")
                    {
                        DataTable dtPrvMonthSch = objInsp.TEC_Get_SupInspectionDetailsByDate(PreDate, Convert.ToInt32(e.Row.Cells[2].Text), UDFLib.ConvertToInteger(DDLCompany.SelectedValue));

                        if (dtPrvMonthSch.Rows.Count > 0)
                        {
                            // if (dtPrvMonthSch.Rows[0]["ActualDate"].ToString() == "")
                            {
                                int startDay = Convert.ToDateTime(dtPrvMonthSch.Rows[0]["StartDate"].ToString()).Day;
                                //int endDay = Convert.ToDateTime(dtPrvMonthSch.Rows[0]["EndDate"].ToString()).Day;
                                //double diff = (Convert.ToDateTime(dtPrvMonthSch.Rows[0]["EndDate"].ToString()) - Convert.ToDateTime(dtPrvMonthSch.Rows[0]["StartDate"].ToString())).TotalDays;
                                int endDay = 0;
                                double diff = 0;                                
                                if (dtPrvMonthSch.Rows[0]["EndDate"].ToString() != "")
                                {
                                    endDay = Convert.ToDateTime(dtPrvMonthSch.Rows[0]["EndDate"].ToString()).Day;
                                    diff = (Convert.ToDateTime(dtPrvMonthSch.Rows[0]["EndDate"].ToString()) - Convert.ToDateTime(dtPrvMonthSch.Rows[0]["StartDate"].ToString())).TotalDays;
                                }


                                if ((startDay + Convert.ToInt32(diff)) > DateTime.DaysInMonth(PreDate.Year, PreDate.Month))
                                {
                                    int EndDay = (startDay + Convert.ToInt32(diff)) - DateTime.DaysInMonth(PreDate.Year, PreDate.Month);
                                    int StDay = 1;
                                    e.Row.Cells[StDay + 2].Text = dtPrvMonthSch.Rows[0]["LocName"].ToString();
                                    e.Row.Cells[StDay + 2].Attributes["ColSpan"] = ((EndDay)).ToString();
                                    e.Row.Cells[StDay + 2].CssClass = "CurrentNew";
                                    e.Row.Cells[1].Font.Bold = true;
                                    //for (int j = StDay + 1; j <= (StDay + EndDay); j++)
                                    //{
                                    //    if (j <= e.Row.Cells.Count - 2)
                                    //    {
                                    //        e.Row.Cells[j + 1].CssClass = "CurrentNew";
                                    //    }
                                    //}
                                    for (int j = StDay + 2; j <= (StDay + EndDay); j++)
                                    {
                                        if (j <= e.Row.Cells.Count - 2)
                                        {
                                            e.Row.Cells[j + 1].Visible = false;
                                            e.Row.Cells[j + 1].Controls.Clear();
                                        }
                                    }

                                }

                            }
                        }
                    }
                }
                for (int i = FixCellCount; i < e.Row.Cells.Count; i++)
                {
                    DayCount++;
                    e.Row.Cells[i].Width = 80;
                    DateTime CurDate1 = Convert.ToDateTime(Convert.ToString(i - 2) + "/" + ViewState["CurrentMonth"].ToString() + "/" + ViewState["CurrentYear"].ToString());
                    if (CurDate1.DayOfWeek.ToString() == "Sunday" || CurDate1.DayOfWeek.ToString() == "Saturday")
                    {

                        if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "")
                        {
                            e.Row.Cells[i].BackColor = ColorTranslator.FromHtml("#81BEF7");
                        }

                    }
                    if (e.Row.Cells[i].Text != "&nbsp;" && e.Row.Cells[i].Text != "")
                    {
                        DateTime CurDate = Convert.ToDateTime(Convert.ToString(i - 2) + "/" + ViewState["CurrentMonth"].ToString() + "/" + ViewState["CurrentYear"].ToString());

                        if (e.Row.Cells[2].Text != "")
                        {
                            DataTable dtSch = objInsp.TEC_Get_SupInspectionDetailsByDate(CurDate, Convert.ToInt32(e.Row.Cells[2].Text), UDFLib.ConvertToInteger(DDLCompany.SelectedValue));

                            if (dtSch.Rows.Count > 0)
                            {
                                int startDay = Convert.ToDateTime(dtSch.Rows[0]["StartDate"].ToString()).Day;
                                int endDay = 0;
                                double diff = 0;
                                //int endDay = Convert.ToDateTime(dtSch.Rows[0]["EndDate"].ToString()).Day;
                                //double diff = (Convert.ToDateTime(dtSch.Rows[0]["EndDate"].ToString()) - Convert.ToDateTime(dtSch.Rows[0]["StartDate"].ToString())).TotalDays;
                                if (dtSch.Rows[0]["EndDate"].ToString() != "")
                                {
                                    endDay = Convert.ToDateTime(dtSch.Rows[0]["EndDate"].ToString()).Day;
                                    diff = (Convert.ToDateTime(dtSch.Rows[0]["EndDate"].ToString()) - Convert.ToDateTime(dtSch.Rows[0]["StartDate"].ToString())).TotalDays;
                                }
                               
                                if ((startDay + Convert.ToInt32(diff)) > DateTime.DaysInMonth(CurDate.Year, CurDate.Month))
                                {
                                    int EndDay = (startDay + Convert.ToInt32(diff)) - DateTime.DaysInMonth(CurDate.Year, CurDate.Month);
                                    e.Row.Cells[startDay + 2].Text = dtSch.Rows[0]["LocName"].ToString();
                                    e.Row.Cells[startDay + 2].Attributes["ColSpan"] = ((Convert.ToInt32(diff) - EndDay) + 1).ToString();
                                    e.Row.Cells[startDay + 2].CssClass = "CurrentNew";
                                    e.Row.Cells[1].Font.Bold = true;
                                    //for (int j = startDay + 1; j <= (startDay + (Convert.ToInt32(diff) - EndDay)) + 1; j++)
                                    //{
                                    //    if (j <= e.Row.Cells.Count -2)
                                    //    {
                                    //        e.Row.Cells[j + 1].CssClass = "CurrentNew";
                                    //    }
                                    //}
                                    for (int j = startDay + 2; j <= (startDay + (Convert.ToInt32(diff) - EndDay)) + 1; j++)
                                    {
                                        if (j <= e.Row.Cells.Count - 2)
                                        {
                                            e.Row.Cells[j + 1].Visible = false;
                                            e.Row.Cells[j + 1].Controls.Clear();
                                        }
                                    }

                                }
                                else
                                {
                                    e.Row.Cells[startDay + 2].Text = dtSch.Rows[0]["LocName"].ToString();
                                    e.Row.Cells[startDay + 2].Attributes["ColSpan"] = ((endDay - startDay) + 1).ToString();
                                    e.Row.Cells[startDay + 2].CssClass = "CurrentNew";
                                    e.Row.Cells[1].Font.Bold = true;
                                    //for (int j = startDay + 1; j <= (startDay + diff) + 1; j++)
                                    //{
                                    //    if (j < e.Row.Cells.Count - 2)
                                    //    {
                                    //        e.Row.Cells[j + 1].CssClass = "CurrentNew";
                                    //    }
                                    //}
                                    for (int j = startDay + 2; j <= (startDay + diff) + 1; j++)
                                    {
                                        if (j <= e.Row.Cells.Count - 2)
                                        {
                                            e.Row.Cells[j + 1].Visible = false;
                                            e.Row.Cells[j + 1].Controls.Clear();
                                        }
                                    }

                                }

                            }
                        }
                    }



                }

                e.Row.Cells[1].Font.Size = 10;
                e.Row.Cells[1].Font.Name = "Tahoma";
                e.Row.Cells[0].Font.Size = 10;
                e.Row.Cells[0].Font.Name = "Tahoma";

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region "Async According To Web Sevice"
    //protected void BtnPrevious_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DateTime curDate = Convert.ToDateTime(ViewState["CurrentDate"].ToString()).AddMonths(-1);
    //        ViewState["CurrentYear"] = curDate.Year.ToString();
    //        ViewState["CurrentMonth"] = curDate.Month.ToString();
    //        ViewState["CurrentDay"] = "01";
    //        ViewState["CurrentDate"] = curDate;

    //          BindSupAttGrid(curDate);
    //        //string js = "Prev()";
    //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "ddd", js, true);
    //        lblMonthYear.Text = Convert.ToDateTime("01/" + ViewState["CurrentMonth"].ToString() + "/" + ViewState["CurrentYear"].ToString()).ToString("MMMM yyyy");
    //        lblDate.Text = DateTime.Now.ToString("dd MMM yy");
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }

    //}
    //protected void BtnCurrent_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DateTime curDate = DateTime.Now;
    //        ViewState["CurrentYear"] = curDate.Year.ToString();
    //        ViewState["CurrentMonth"] = curDate.Month.ToString();
    //        ViewState["CurrentDay"] = "01";
    //        ViewState["CurrentDate"] = curDate;

    //        BindSupAttGrid(curDate);
    //        //string js = "LastLoad()";
    //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "ddd", js, true);
    //        lblMonthYear.Text = Convert.ToDateTime("01/" + ViewState["CurrentMonth"].ToString() + "/" + ViewState["CurrentYear"].ToString()).ToString("MMMM yyyy");
    //        lblDate.Text = DateTime.Now.ToString("dd MMM yy");
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }

    //}
    //protected void BtnNext_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DateTime curDate = Convert.ToDateTime(ViewState["CurrentDate"].ToString()).AddMonths(1);
    //        ViewState["CurrentYear"] = curDate.Year.ToString();
    //        ViewState["CurrentMonth"] = curDate.Month.ToString();
    //        ViewState["CurrentDay"] = "01";
    //        ViewState["CurrentDate"] = curDate;

    //         BindSupAttGrid(curDate);
    //        //string js = "Next()";
    //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "ddd", js, true);
    //        lblMonthYear.Text = Convert.ToDateTime("01/" + ViewState["CurrentMonth"].ToString() + "/" + ViewState["CurrentYear"].ToString()).ToString("MMMM yyyy");
    //        lblDate.Text = DateTime.Now.ToString("dd MMM yy");
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    #endregion


    protected void BtnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime curDate = Convert.ToDateTime(ViewState["CurrentDate"].ToString()).AddMonths(-1);
            ViewState["CurrentYear"] = curDate.Year.ToString();
            ViewState["CurrentMonth"] = curDate.Month.ToString();
            ViewState["CurrentDay"] = "01";
            ViewState["CurrentDate"] = curDate;

            BindSupAttGrid(curDate);
            lblMonthYear.Text = Convert.ToDateTime("01/" + ViewState["CurrentMonth"].ToString() + "/" + ViewState["CurrentYear"].ToString()).ToString("MMMM yyyy");
            lblDate.Text = DateTime.Now.ToString("dd MMM yy");
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void BtnCurrent_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime curDate = DateTime.Now;
            ViewState["CurrentYear"] = curDate.Year.ToString();
            ViewState["CurrentMonth"] = curDate.Month.ToString();
            ViewState["CurrentDay"] = "01";
            ViewState["CurrentDate"] = curDate;

            BindSupAttGrid(curDate);
            lblMonthYear.Text = Convert.ToDateTime("01/" + ViewState["CurrentMonth"].ToString() + "/" + ViewState["CurrentYear"].ToString()).ToString("MMMM yyyy");
            lblDate.Text = DateTime.Now.ToString("dd MMM yy");
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void BtnNext_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime curDate = Convert.ToDateTime(ViewState["CurrentDate"].ToString()).AddMonths(1);
            ViewState["CurrentYear"] = curDate.Year.ToString();
            ViewState["CurrentMonth"] = curDate.Month.ToString();
            ViewState["CurrentDay"] = "01";
            ViewState["CurrentDate"] = curDate;

            BindSupAttGrid(curDate);
            lblMonthYear.Text = Convert.ToDateTime("01/" + ViewState["CurrentMonth"].ToString() + "/" + ViewState["CurrentYear"].ToString()).ToString("MMMM yyyy");
            lblDate.Text = DateTime.Now.ToString("dd MMM yy");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BtnPrintPDF_Click(object sender, ImageClickEventArgs e)
    {
        EO.Pdf.HtmlToPdf.Options.PageSize = new SizeF(11.69f, 8.27f);
        PdfDocument doc = new PdfDocument();


        string GUID = Guid.NewGuid().ToString();

        string filePath = Server.MapPath("~/Uploads/Reports/" + GUID + ".pdf");

        EO.Pdf.Runtime.AddLicense("p+R2mbbA3bNoqbTC4KFZ7ekDHuio5cGz4aFZpsKetZ9Zl6TNHuig5eUFIPGe" +
    "tcznH+du5PflEuCG49jjIfewwO/o9dB2tMDAHuig5eUFIPGetZGb566l4Of2" +
    "GfKetZGbdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW6yuCwb6y9xtyxdabw" +
    "+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzr" +
    "peb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mkBxDxrODz/+ihb6W0" +
    "s8uud4SOscufWbOz8hfrqO7CnrWfWZekzRrxndz22hnlqJfo8h8=");
        EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = "<div style='text-align:center; font-family:Tahoma; font-size:10px;'>Page {page_number} of {total_pages}</div>";
        HtmlToPdf.ConvertHtml(hdnContent.Value, filePath);


        newtab = UDFLib.ConvertToInteger(ViewState["newtabnumber"]);
        newtab++;
        ViewState["newtabnumber"] = newtab;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideText", "window.open('../../Uploads/Reports/" + GUID + ".pdf','ATT" + newtab + "');", true);  // (  this.GetType(), "OpenWindow", "window.open('../../Uploads/InspectionReport.pdf','_newtab');", true);

    }

    protected void DDLCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        DateTime StartDate = Convert.ToDateTime("01/" + ViewState["CurrentMonth"].ToString() + "/" + ViewState["CurrentYear"].ToString());
        dt = objInsp.TEC_Get_SupritendentAttendanceWithPort(StartDate, UDFLib.ConvertToInteger(DDLCompany.SelectedValue));

        grdSupAtt.DataSource = null;
        grdSupAtt.DataBind();

         grdSupAtt.DataSource = dt;
         grdSupAtt.DataBind();
    }

    #region "Async According To Web Sevice"
    //public string[] asncLoadCalendarBySupt(string pUserCompanyId, string pStartDate)
    //{
    //    bool flagfirstrow = true;

    //    DateTime lStartDate = Convert.ToDateTime(pStartDate);
    //    lStartDate = new DateTime(lStartDate.Year, lStartDate.Month, 1);
    //    DateTime lEndDate = lStartDate.AddMonths(0);
    //    lEndDate = new DateTime(lEndDate.Year, lEndDate.Month, DateTime.DaysInMonth(lEndDate.Year, lEndDate.Month));
    //    try
    //    {
    //        List<string> MonthName = new List<string>();
    //        List<int> MonthDays = new List<int>();
    //        for (int i = 1; i <= 12; i++)
    //        {
    //            DateTime lDate = new DateTime(lEndDate.Year, i, DateTime.DaysInMonth(lEndDate.Year, i));
    //            MonthName.Add(lDate.ToString("MMMM", new CultureInfo("en-GB")));
    //            MonthDays.Add(DateTime.DaysInMonth(lEndDate.Year, i));
    //        }
    //        DataTable tbDD = new DataTable();
    //        tbDD.Columns.Add("DateInt", typeof(int));
    //        tbDD.Columns.Add("DayOfWeek", typeof(string));
    //        tbDD.Columns.Add("tDate", typeof(DateTime));

    //        DataTable tbDDPrev = new DataTable();
    //        tbDDPrev.Columns.Add("DateInt", typeof(int));
    //        tbDDPrev.Columns.Add("DayOfWeek", typeof(string));
    //        tbDDPrev.Columns.Add("tDate", typeof(DateTime));

    //        string selectionText = lStartDate.ToString("MMM/yyyy") + " to " + lEndDate.ToString("MMM/yyyy");
    //        #region data


    //        BLL_Tec_Inspection lObj = new BLL_Tec_Inspection();
    //        DataSet ds = lObj.Get_CaledndarDataBySupt(lStartDate, lEndDate, UDFLib.ConvertIntegerToNull(pUserCompanyId));

    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            if (UDFLib.ConvertDateToNull(ds.Tables[0].Rows[i]["Schedule_Date"].ToString()).Value < lStartDate)
    //            {
    //                ds.Tables[0].Rows[i]["Schedule_Date"] = lStartDate;
               
                
    //            }
    //        }
    //        #endregion
    //        //if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            foreach (DataRow itemC in ds.Tables[0].Rows)
    //            {
    //                if (itemC["ActualDate"].ToString() == "")
    //                {
    //                    itemC["ActualInspectorId"] = itemC["InspectorId"];
    //                    itemC["ActualDate"] = UDFLib.ConvertDateToNull(itemC["Schedule_Date"]).Value.AddDays(UDFLib.ConvertToInteger(itemC["DurJobs"]) - 1);
    //                }
    //            }

           

    //            int? UserCompanyId = UDFLib.ConvertIntegerToNull(pUserCompanyId);




    //            DateTime tempdate = lStartDate;
    //         //   DateTime PrevTempDate = lStartDate.AddMonths(-1);
    //          //  DateTime PrevlEndDate = new DateTime(PrevTempDate.Year, PrevTempDate.Month, DateTime.DaysInMonth(PrevTempDate.Year, PrevTempDate.Month)); 
    //            while (true)
    //            {

    //                tbDD.Rows.Add(tempdate.Day, tempdate.DayOfWeek.ToString().ToCharArray()[0], tempdate);
    //                if (tempdate == lEndDate)
    //                    break;
    //                tempdate = tempdate.AddDays(1);
    //            }
    //            //while (true)
    //            //{

    //            //    tbDDPrev.Rows.Add(PrevTempDate.Day, PrevTempDate.DayOfWeek.ToString().ToCharArray()[0], PrevTempDate);
    //            //    if (PrevTempDate == PrevlEndDate)
    //            //        break;
    //            //    PrevTempDate = PrevTempDate.AddDays(1);
    //            //}

    //            Table InspTable = new Table();
    //            InspTable.CellPadding = 0;
    //            InspTable.CellSpacing = 0;
    //            Table InspTable0 = new Table();
    //            InspTable0.CellPadding = 0;
    //            InspTable0.CellSpacing = 0;
    //            TableRow dateRow;
    //            TableRow dayRow;
    //            TableRow VesslAttentRow = null;
    //            TableCell dateCell;
    //            TableCell dayCell;
    //            TableRow monthRow = new TableRow();
    //            monthRow.TableSection = TableRowSection.TableHeader;

    //            TableCell monthCell;
    //            TableCell tcSuperintendents = new TableCell();



    //            tcSuperintendents.Text = "Inspectors";
    //            tcSuperintendents.CssClass = "SupStyle";
    //            tcSuperintendents.RowSpan = 3;
    //            monthRow.Cells.Add(tcSuperintendents);




    //            DateTime l = lStartDate;

    //            while (true)
    //            {
    //                bool tobreak = false; ;
    //                if (l.Month == lEndDate.Month)
    //                {
    //                    tobreak = true;
    //                }
    //                monthCell = new TableCell();
    //                monthCell.Text = MonthName[l.Month - 1].ToString() + " " + l.Year;
    //                monthCell.ColumnSpan = DateTime.DaysInMonth(l.Year, l.Month);
    //                monthCell.CssClass = "MonthStyle";
    //                monthRow.Cells.Add(monthCell);
    //                l = l.AddMonths(1);
    //                if (tobreak)
    //                    break;
    //            }






    //            dateRow = new TableRow();
    //            dayRow = new TableRow();

    //            dateRow.TableSection = TableRowSection.TableHeader;
    //            dayRow.TableSection = TableRowSection.TableHeader;
    //            foreach (DataRow dataRow in tbDD.Rows)
    //            {

    //                dateCell = new TableCell();
    //                dateCell.CssClass = "DateStyle";
    //                dateCell.Text = dataRow["DateInt"].ToString();
    //                dayCell = new TableCell();
    //                dayCell.Text = dataRow["DayOfWeek"].ToString();
    //                dayCell.CssClass = "DayStyle";
    //                dateRow.Cells.Add(dateCell);
    //                dayRow.Cells.Add(dayCell);

    //            }

    //            InspTable0.Rows.Add(monthRow);
    //            InspTable0.Rows.Add(dateRow);
    //            InspTable0.Rows.Add(dayRow);





    //            if (InspColor == null)
    //            {
    //                InspColor = new Dictionary<int, Color>();
    //            }
    //            //else
    //            //{
    //            //    InspColor = (Dictionary<int, Color>)Session["InspColor"];
    //            //}


    //            InspColor = new Dictionary<int, Color>();
    //            // BLL_Infra_VesselLib lObjVessel = new BLL_Infra_VesselLib();
    //            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    //            //DataTable dtVessel = lObjVessel.Get_VesselList(0, 0, 0, "", 0);
    //            DataTable dtUsers = objUser.INSP_Get_InspectorList();

    //            //  for (int i = 0; i < 10; i++)
    //            //  {
    //            foreach (DataRow item in dtUsers.Rows)
    //            {
    //                if (UserCompanyId != null)
    //                    if (item["Vessel_Manager"].ToString() != UserCompanyId.ToString())
    //                        continue;
    //                TableCell VesselCell = new TableCell();


    //                List<DateTime> lDateList = new List<DateTime>();
    //                DataTable dx = null;
    //                DataTable dy = null;
    //                DataRow[] dtCount = ds.Tables[0].Select(" Inspctor=" + item["InspectorID"].ToString());

    //                if (dtCount.Length > 0)
    //                {
    //                    string[] ColumnNames = new string[1];
    //                    //ColumnNames[0] = "InspectionDetailId";
    //                    ColumnNames[0] = "VESSEL_ID";
    //                    //    ColumnNames[1] = "Schedule_date";
    //                    dx = dtCount.CopyToDataTable().DefaultView.ToTable(true, ColumnNames);

    //                }


    //                List<string[]> lb = new List<string[]>();

    //                if (dx != null)
    //                {
    //                    foreach (DataRow dxr in dx.Rows)
    //                    {
    //                        string[] Columnval = new string[1];
    //                        Columnval[0] = dxr[0].ToString();
    //                        // Columnval[1] = dxr[1].ToString();
    //                        if (!lb.Contains(Columnval))
    //                        {
    //                            lb.Add(Columnval);
    //                        }
    //                    }

    //                    dx.Rows.Clear();
    //                }

    //                foreach (string[] lbr in lb)
    //                {
    //                    dx.Rows.Add(lbr);
    //                }

    //                bool flag = true;
                  
    //                if (dtCount.Length > 0)
    //                {
    //                    Dictionary<string, TableRow> lTRow = new Dictionary<string, TableRow>();


    //                    TableRow VesslAttentRowD = new TableRow(); ;
    //                    foreach (DataRow dataRow in tbDD.Rows)
    //                    {

    //                        TableCell VesselAttCell = new TableCell();
    //                        VesselAttCell.Text = "";
    //                        VesselAttCell.CssClass = "NormStyle";
    //                        VesslAttentRowD.Cells.Add(VesselAttCell);
    //                    }

    //                    List<TableRow> lRowsTable = new List<TableRow>();
    //                    bool fmk = true;
    //                    lRowsTable.Add(VesslAttentRowD);
    //                    int st = 0;
    //                    foreach (DataRow insprow in dx.Rows)
    //                    {
    //                        if (!InspColor.ContainsKey(UDFLib.ConvertToInteger(item["InspectorID"].ToString())))
    //                        {
    //                            while (true)
    //                            {
    //                                Random randomGen = new Random();
    //                                KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
    //                                KnownColor randomColorName = names[randomGen.Next(names.Length)];
    //                                Color randomColor = Color.FromKnownColor(randomColorName);
    //                                bool flagcolr = true;
    //                                foreach (Color itemcolor in InspColor.Values)
    //                                {
    //                                    if (randomColor == itemcolor || randomColor == Color.White || randomColor.ToString().Contains("white") || randomColor.A < 5 || randomColor.R > 252 || randomColor.G > 252 || randomColor.B > 252 || randomColor.ToString().Contains("highlight"))
    //                                        flagcolr = false;

    //                                }
    //                                if (flagcolr)
    //                                {
    //                                    InspColor[UDFLib.ConvertToInteger(item["InspectorID"].ToString())] = randomColor;
    //                                    break;
    //                                }


    //                            }


    //                        }
    //                        int xk = 0;
    //                        Dictionary<int, bool> flist = new Dictionary<int, bool>();
    //                        foreach (DataRow dataRow in tbDD.Rows)
    //                        {

    //                            DataRow[] dtRows = ds.Tables[0].Select("Schedule_Date = '" + dataRow["tDate"].ToString() + "'   and VESSEL_ID=" + insprow["VESSEL_ID"] + "and ActualInspectorId=" + item["InspectorID"].ToString());
    //                            int iko = 0;
    //                            if (dtRows.Length > 0 && dtRows.Length <= 1)
    //                            {

                                  
    //                                    foreach (TableRow itemlRowsTable in lRowsTable)
    //                                    {
    //                                        if (itemlRowsTable.Cells[xk].Text.Trim().Length > 0 || itemlRowsTable.Cells[xk].Visible == false)
    //                                        {

    //                                            flist[iko] = false;

    //                                            fmk = false;


    //                                        }
                                    
    //                                        else
    //                                        {
    //                                            if (!flist.ContainsKey(iko))
    //                                            {
    //                                                flist[iko] = true;
    //                                            }
    //                                        }
    //                                        iko++;
    //                                    }
                                

    //                            }
                                
    //                            xk++;
    //                        }
    //                        try
    //                        {
    //                            if (flist.Values.Contains(true))
    //                            {
    //                                int trucnt = 0;
    //                                foreach (var itemx in flist.Values)
    //                                {
    //                                    if (itemx)
    //                                    {
    //                                        break;
    //                                    }
    //                                    trucnt++;
    //                                }

    //                                int nk = 0;
    //                                int Dur = 1;

                                   




    //                                nk = 0;
    //                                foreach (DataRow dataRow in tbDD.Rows)
    //                                {
    //                                    Dur = 1;
    //                                    DataRow[] dtRows = ds.Tables[0].Select("Schedule_Date = '" + dataRow["tDate"].ToString() + "'  and VESSEL_ID=" + insprow["VESSEL_ID"] + " and ActualInspectorId=" + item["InspectorID"]);
    //                                    if (dtRows.Length > 0)
    //                                    {


                                          
    //                                            int UserId = UDFLib.ConvertToInteger(dtRows[0]["Inspctor"]);
                                           

    //                                            lRowsTable[trucnt].Cells[nk].BackColor = System.Drawing.ColorTranslator.FromHtml("#" + dtRows[0]["ColorCodeS"]);
                                              
    //                                            Dur = UDFLib.ConvertToInteger(dtRows[0]["DurJobs"].ToString());

    //                                            DataTable dtSch = objInsp.TEC_Get_SupInspectionDetailsByDate(UDFLib.ConvertDateToNull(dtRows[0]["TempScheduleDate"]).Value, Convert.ToInt32(UserId), UDFLib.ConvertToInteger(DDLCompany.SelectedValue));

    //                                            if (dtSch.Rows.Count > 0)
    //                                            {
                                                 
    //                                                if (dtSch.Rows[0]["StartDate"].ToString() != dtSch.Rows[0]["EndDate"].ToString())
    //                                                {
    //                                                    lRowsTable[trucnt].Cells[nk].Text = dtSch.Rows[0]["LocName"].ToString() + "<br/>" + Convert.ToDateTime(dtSch.Rows[0]["StartDate"].ToString()).Date.ToString("dd/MM") + " - " + Convert.ToDateTime(dtSch.Rows[0]["EndDate"].ToString()).Date.ToString("dd/MM");
    //                                                }
    //                                                else
    //                                                {
    //                                                    lRowsTable[trucnt].Cells[nk].Text = dtSch.Rows[0]["LocName"].ToString() + "<br/>" + Convert.ToDateTime(dtSch.Rows[0]["StartDate"].ToString()).Date.ToString("dd/MM");
    //                                                }
    //                                            }

                                              


    //                                            int ColSpan = 0, endDay = 0;
    //                                            double  Diff = 0;
    //                                            Diff = (UDFLib.ConvertDateToNull(dataRow["tDate"]).Value - UDFLib.ConvertDateToNull(dtRows[0]["TempScheduleDate"]).Value).TotalDays;
    //                                            if ((nk + Dur) > tbDD.Rows.Count)
    //                                            {
    //                                                endDay = (UDFLib.ConvertToInteger( Diff) + nk + 1);
    //                                                Dur = tbDD.Rows.Count - (nk);
    //                                            }
    //                                            else
    //                                            {
    //                                                endDay = (Dur + nk);
    //                                                Dur = Dur - UDFLib.ConvertToInteger(Diff);
    //                                            }

    //                                            lRowsTable[trucnt].Cells[nk].ColumnSpan = Dur;
    //                                            for (int i = nk + 1; i < endDay; i++)
    //                                            {
    //                                                lRowsTable[trucnt].Cells[i].Visible = false;
    //                                            }

                                            

    //                                    }
    //                                    nk++;
                                    
    //                                }





    //                            }
    //                            else
    //                            {
    //                                TableRow VesslAttentRowNew = new TableRow(); ;
    //                                foreach (DataRow dataRow in tbDD.Rows)
    //                                {

    //                                    TableCell VesselAttCell = new TableCell();
    //                                    VesselAttCell.CssClass = "NormStyle";
    //                                    VesselAttCell.Text = "";

    //                                    VesslAttentRowNew.Cells.Add(VesselAttCell);
    //                                }
    //                                int nk = 0;
    //                                int Dur = 1;
    //                                foreach (DataRow dataRow in tbDD.Rows)
    //                                {
    //                                    Dur = 1;
    //                                    DataRow[] dtRows = ds.Tables[0].Select("Schedule_Date = '" + dataRow["tDate"].ToString() + "' and VESSEL_ID=" + insprow["VESSEL_ID"] + "  and ActualInspectorId=" + item["InspectorID"]);
    //                                    if (dtRows.Length > 0)
    //                                    {


                                           
    //                                            int UserId1 = UDFLib.ConvertToInteger(dtRows[0]["Inspctor"]);
                                                
    //                                            VesslAttentRowNew.Cells[nk].BackColor = System.Drawing.ColorTranslator.FromHtml("#" + dtRows[0]["ColorCodeS"]);
                                              
    //                                            VesslAttentRowNew.CssClass = "NormStyle";
    //                                            DataTable dtSch = objInsp.TEC_Get_SupInspectionDetailsByDate(UDFLib.ConvertDateToNull(dtRows[0]["TempScheduleDate"]).Value, Convert.ToInt32(UserId1), UDFLib.ConvertToInteger(DDLCompany.SelectedValue));

    //                                            if (dtSch.Rows.Count > 0)
    //                                            {
                                                  
    //                                                if (dtSch.Rows[0]["StartDate"].ToString() != dtSch.Rows[0]["EndDate"].ToString())
    //                                                {
    //                                                    VesslAttentRowNew.Cells[nk].Text = dtSch.Rows[0]["LocName"].ToString() + "<br/>" + Convert.ToDateTime(dtSch.Rows[0]["StartDate"].ToString()).Date.ToString("dd/MM") + " - " + Convert.ToDateTime(dtSch.Rows[0]["EndDate"].ToString()).Date.ToString("dd/MM");
                                                       
    //                                                }
    //                                                else
    //                                                {
    //                                                    VesslAttentRowNew.Cells[nk].Text = dtSch.Rows[0]["LocName"].ToString() + "<br/>" + Convert.ToDateTime(dtSch.Rows[0]["StartDate"].ToString()).Date.ToString("dd/MM");
    //                                                }
    //                                            }

    //                                            Dur = UDFLib.ConvertToInteger(dtRows[0]["DurJobs"].ToString());
    //                                            int ColSpan = 0,  endDay = 0;
    //                                            double Diff = 0;
    //                                            Diff = (UDFLib.ConvertDateToNull(dataRow["tDate"]).Value - UDFLib.ConvertDateToNull(dtRows[0]["TempScheduleDate"]).Value).TotalDays;
    //                                            if ((nk + Dur) > tbDD.Rows.Count)
    //                                            {
    //                                                // Diff = (nk + Dur) - tbDD.Rows.Count;
    //                                                endDay = (UDFLib.ConvertToInteger(Diff) + nk + 1);
    //                                                //Dur = Dur - Diff;

    //                                                Dur = tbDD.Rows.Count - (nk);
    //                                            }
    //                                            else
    //                                            {
    //                                                endDay = (Dur + nk);
    //                                                Dur = Dur - UDFLib.ConvertToInteger(Diff);
    //                                            }

    //                                            VesslAttentRowNew.Cells[nk].ColumnSpan = Dur;

    //                                            for (int i = nk + 1; i < endDay; i++)
    //                                            {

    //                                                VesslAttentRowNew.Cells[i].Visible = false;

    //                                            }
                                            
    //                                    }
    //                                    nk++;
    //                                    // nk += Dur;
    //                                }
    //                                lRowsTable.Add(VesslAttentRowNew);
    //                            }
    //                        }
    //                        catch (Exception)
    //                        {

    //                            throw;
    //                        }





    //                    }






    //                    int ik = 0;
    //                    foreach (TableRow itemTableRow in lRowsTable)
    //                    {
    //                        if (ik == 0)
    //                        {
    //                            VesslAttentRow = new TableRow();
    //                            VesselCell = new TableCell();
    //                            VesselCell.Text = item["Inspector"].ToString();

    //                            VesselCell.RowSpan = lRowsTable.Count;
    //                            VesselCell.CssClass = "VesselStyle";
    //                            VesslAttentRow.Cells.Add(VesselCell);
    //                            VesselCell.Style.Add("height", (15 * lRowsTable.Count) + "px");
    //                            int cellskip = 0;
    //                            foreach (TableCell itemDetails in itemTableRow.Cells)
    //                            {

    //                                TableCell dtC = new TableCell();

    //                                dtC.ToolTip = itemDetails.ToolTip;
    //                                dtC.Text = itemDetails.Text;
    //                                dtC.BackColor = itemDetails.BackColor;
    //                                dtC.ForeColor = itemDetails.ForeColor;
    //                                dtC.ColumnSpan = itemDetails.ColumnSpan;

    //                                dtC.CssClass = "NormStyle";
    //                                if (dtC.ColumnSpan > 0)
    //                                {
    //                                   cellskip = 0;
    //                                }
    //                                if (cellskip <= 0)
    //                                {
    //                                    cellskip = dtC.ColumnSpan - 1;
    //                                    if (dtC.Visible == true)
    //                                   {
    //                                        VesslAttentRow.Cells.Add(dtC);
    //                                   }
    //                                }
    //                                else
    //                                {
    //                                    cellskip--;
    //                                }



    //                            }
    //                            InspTable.Rows.Add(VesslAttentRow);
    //                        }
    //                        else
    //                        {

    //                            InspTable.Rows.Add(itemTableRow);
    //                        }

    //                        ik++;
    //                    }

    //                }
    //                else
    //                {
    //                    VesselCell.Text = item["Inspector"].ToString();
    //                    VesslAttentRow = new TableRow();
    //                    VesselCell.CssClass = "VesselStyle";
    //                    VesslAttentRow.Cells.Add(VesselCell);

    //                    foreach (DataRow dataRow in tbDD.Rows)
    //                    {
    //                        TableCell VesselAttCell = new TableCell();
    //                        if (flagfirstrow)
    //                            VesselAttCell.Text = "";
    //                        VesselAttCell.CssClass = "NormStyle";
    //                        VesslAttentRow.Cells.Add(VesselAttCell);
    //                    }
    //                    InspTable.Rows.Add(VesslAttentRow);
    //                    flagfirstrow = false;
    //                }


    //            }
    //            //    }





    //            string newt = "";
    //            string newt0 = "";

    //            InspTable.ID = "t01";
    //            using (StringWriter sw = new StringWriter())
    //            {

    //                InspTable.RenderControl(new HtmlTextWriter(sw));
    //                newt = sw.ToString();
    //            }
    //            InspTable0.ID = "t00";
    //            using (StringWriter swq = new StringWriter())
    //            {

    //                InspTable0.RenderControl(new HtmlTextWriter(swq));
    //                newt0 = swq.ToString();
    //            }


    //            String[] result = { newt, selectionText, newt0 };
    //            return result;
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        return new string[] { "", "", ex.Message };

    //    }

    //     String[] res = { "", "", "" };
    //     return res;

    //}
    #endregion

}