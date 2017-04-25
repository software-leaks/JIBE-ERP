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
using SMS.Business.Crew;

using System.Data.SqlClient;
using System.Text;
using System.IO;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html;
//using iTextSharp.text.html.simpleparser;

public partial class TMSA_KPI_KPI_Crew_Retention_New : System.Web.UI.Page
{
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
    public UserAccess objUA = new UserAccess();
    public string[] Vessel_Ids;
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    private int Category_Id = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            Load_RankList();
            Load_Years();
            LoadData();
           // loadPIDetails();
            GenearteDiv();

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

    }


    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    /// <summary>
    /// Method to load list of ranks
    /// </summary>
    public void Load_RankList()
    {
        DataTable dt = objCrewAdmin.Get_RankList();
        lstRank.DataSource = dt;
        lstRank.DataTextField = "Rank_Short_Name";
        lstRank.DataValueField = "ID";
        lstRank.DataBind();


        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "Rank_Short_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();


        foreach (DataRow dr in dt.Rows)
        {
            ddlRank.SelectItems(new string[] { dr["ID"].ToString() });
        }


    }



    //protected void loadPIDetails()
    //{

    //    DataSet ds = BLL_TMSA_PI.Get_KPI_Detail(UDFLib.ConvertIntegerToNull(hiddenKPIID.Value));
    //    DataTable dt = new DataTable();
    //    DataTable dtPIDtl = ds.Tables[1];
    //    dt.Columns.Add(new DataColumn("sno", typeof(int)));
    //    dt.Columns.Add(new DataColumn("PID", typeof(string)));
    //    dt.Columns.Add(new DataColumn("value", typeof(string)));
    //    dt.Columns.Add(new DataColumn("PIName", typeof(string)));
    //    string exp = "";
    //    foreach (DataRow row in dtPIDtl.Rows)
    //    {
    //        dt.Rows.Add(new object[] { Convert.ToInt32(row["SerialNumber"].ToString()), row["PI_ID"].ToString(), row["value"].ToString(), row["Name"].ToString() });
    //        exp = exp + row["value"].ToString();
    //    }
    //    lblFormula.Text = "KPI Formula : " + exp;
    //    DataTable dt_PI = dt;
    //    dt_PI.DefaultView.RowFilter = "PID <> ''";
    //    DataList1.DataSource = dt_PI.DefaultView;
    //    DataList1.DataBind();

    //}

    protected void ddlRank_SelectedIndexChanged()
    {
        LoadData();
    }

    protected void ddlYear_SelectedIndexChanged()
    {
        LoadData();
    }

    /// <summary>
    /// Method to load list of last 10 years
    /// </summary>
    protected void Load_Years()
    {
        int CurrentYear = DateTime.Now.Year;
        int count = 0;
        DataTable dt = new DataTable();
        dt.Columns.Add("Year");
        for (count = CurrentYear; count >= CurrentYear - 10; count--)
        {

            DataRow dr = dt.NewRow();
            dr["Year"] = count.ToString();
            dt.Rows.Add(dr);
        }

        ddlYear.DataSource = dt;
        ddlYear.DataTextField = "Year";
        ddlYear.DataValueField = "Year";
        ddlYear.DataBind();
        ddlYear.Select(DateTime.Now.Year.ToString());
        ddlYear.Select((DateTime.Now.AddYears(-1)).Year.ToString());


    }



    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        LoadData();

    }
    /// <summary>
    /// Method to plot the graphs based on selection critera
    /// Search validation message added  
    /// </summary>
    private void LoadData()
    {
        

        if (ddlRank.SelectedValues.Rows.Count == 0)
        {
            string msg2 = String.Format("ValidateSearch('Rank1')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        else if (ddlYear.SelectedValues.Rows.Count == 0)
        {
            string msg2 = String.Format("ValidateSearch('Year1')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        else if (ddlYear.SelectedValues.Rows.Count > 2)
        {
            string msg2 = String.Format("ValidateSearch('Year2')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }

        else
        {
            GenearteDiv();
            lblFormulaQuarter.Text = "KPI Formula : 100 - (PI041- PI016)/PI006*100";
          //  string funtion = "$(document).ready(function () {showChart();showOverAllGrid();})";
          //  ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "msg", funtion, true);
        }
    }

    /// <summary>
    /// Event Method to reinitialize the page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        Load_RankList();
        Load_Years();
        LoadData();
    }


    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        LoadData();
    }

    /// <summary>
    /// Description: Method to create the dynamic list of categorywise graph
    /// Baseed on Crew rank category, graph will be creted
    /// </summary>
    private void GenearteDiv()
    {
        try
        {
            hdnRanks.Value = "";
            hdnYears.Value = "";
            DataTable dtRank = ddlRank.SelectedValues;
            DataTable dtYear = ddlYear.SelectedValues;

            foreach (DataRow dr in dtRank.Rows)
            {
                hdnRanks.Value = hdnRanks.Value + "," + dr["SelectedValue"].ToString();
            }
            hdnRanks.Value = hdnRanks.Value.Trim(',');
            foreach (DataRow dr in dtYear.Rows)
            {
                hdnYears.Value = hdnYears.Value + "," + dr["SelectedValue"].ToString(); ;
            }
            hdnYears.Value = hdnYears.Value.Trim(',');


            string sContainer = "chartContainer_";
            string sGContainer = "gridContainer_";
            BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
            PlaceHolder1.Controls.Clear();
            int rowcount = 0;
            DataTable dt = objCrewAdmin.Get_RankCategories();
            rowcount = dt.Rows.Count;
            string shdnCategoryID = "";
            string shdnCategory = "";
            hdnCategoryID.Value = shdnCategoryID;
            hdnCategory.Value = shdnCategory;
            foreach (DataRow dr in dt.Rows)
            {

                if (hdnCategoryID.Value == "")
                {
                    hdnCategory.Value = dr["Category_Name"].ToString();
                    hdnCategoryID.Value = dr["ID"].ToString();
                }
                else
                {
                    hdnCategory.Value = hdnCategory.Value + "," + dr["Category_Name"].ToString();
                    hdnCategoryID.Value = hdnCategoryID.Value + "," + dr["ID"].ToString();
                }
            }
            hdnCategory.Value = hdnCategory.Value.Trim(',');
            hdnCategoryID.Value = hdnCategoryID.Value.Trim(',');
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
            //for (int i = 0; i < tblRows; i++)
            for (int i = 0; i < totalCategories; i++)
            {
                TableRow tr = new TableRow();
                //for (int j = 0; j < tblCols; j++)
                //{

                    if (iCount < rowcount)
                    {
                        TableCell tc = new TableCell();

                        CategoryId = dt.Rows[iCount]["ID"].ToString();
                        CategoyName = dt.Rows[iCount]["Category_Name"].ToString();
                        catindex++;
                        iCount++;
                      
                        HtmlGenericControl newControl = new HtmlGenericControl("div");
                        HiddenField hdnCatId = new HiddenField();
                        HiddenField hdnCatName = new HiddenField();
                        //hdnCatId.ID = "hdnID_" + i + j;
                        //hdnCatName.ID = "hdnName_" + i + j;
                        hdnCatId.ID = "hdnID_" + i;
                        hdnCatName.ID = "hdnName_" + i;

                        //newControl.ID = sContainer + i + "_" + j;
                        newControl.ID = sContainer + i;
                        newControl.Attributes.Add("Style", "Height:300px;width:700px;float:left; padding-right:20px; padding-bottom:50px;");
                        //newControl.Attributes.Add("onclick", "openDetail('" + CategoryId + "','" + CategoyName + "')");
                        newControl.InnerHtml = "";
                        tc.Controls.Add(newControl);


                        HtmlGenericControl newGridControl = new HtmlGenericControl("div");
                        //hdnCatId.ID = "hdnID_" + i + j;
                        //hdnCatName.ID = "hdnName_" + i + j;

                        //newGridControl.ID = sGContainer + i + "_" + j;

                        hdnCatId.ID = "hdnID_" + i;
                        hdnCatName.ID = "hdnName_" + i;

                        newGridControl.ID = sGContainer + i;

                        newGridControl.Attributes.Add("Style", "Height:300px;width:700px;float:left;");
                        
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
                    labelControl1.InnerHtml = "Retention Rate - " + CategoyName;
                    labelControl1.Attributes.Add("Style", "Height:50px;width:850px;float:right; font-weight:bold;font-size: 14px;");
                    td.Controls.Add(labelControl1);
                    tr1.Cells.Add(td);
                //}
                    tbl.Rows.Add(tr1);
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
    /// <summary>
    /// Description: Event added to export the gridview data to excel.
    /// Created By: Krishnapriya
    /// </summary>
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Category_Id = 0;
            string RankIDs = hdnRanks.Value;
            string Year = hdnYears.Value;
            DataTable dt = objKPI.Search_CrewRetention(RankIDs, Year, Category_Id).Tables[0];

            string[] HeaderCaptions = { "Quarter", "Employed Crew(PI06)", "NTBR(PI16)", "Total Left(PI41)", "Retention Rate" };
            string[] DataColumnsName = { "Qtr", "AvgAvailable", "NTBR", "LeftAll", "KPI_Value" };
            GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, "Crew_Retention_Details", "Crew Retention Details:Selected Ranks");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Description: Event added to export the By Category gridview data to excel.
    /// Created By: Krishnapriya
    /// </summary>
    protected void btnTab2ExportToExcel_Click(object sender, EventArgs e)
    {   
        try
        {
            Category_Id = 0;
            DataTable dtRank = ddlRank.SelectedValues;
            foreach (DataRow dr in dtRank.Rows)     //Fetch the list of rank ids.
            {
                hdnRanks.Value = hdnRanks.Value + "," + dr["SelectedValue"].ToString();
            }
            hdnRanks.Value = hdnRanks.Value.Trim(',');

            DataTable dtCategory = objCrewAdmin.Get_RankCategories();   //Fetch the list of category name.
            string year = hdnYearTab2.Value;
            DataTable dt = new DataTable();

            string[] HeaderCaptions = { "Quarter", "Employed Crew(PI06)", "NTBR(PI16)", "Total Left(PI41)", "Retention Rate" };
            string[] DataColumnsName = { "Qtr", "AvgAvailable", "NTBR", "LeftAll", "KPI_Value" };

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=CrewRetentionDetails.xls");
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            //Loop through the category name to export individual grid to excel.
            foreach (DataRow drow in dtCategory.Rows)
            {
                dt = objKPI.Search_CrewRetention(hdnRanks.Value, year, Convert.ToInt32(drow["ID"])).Tables[0];
               
                HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='0' width='100%'>");
                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td style='text-align:center;' colspan='" + (DataColumnsName.Length).ToString() + "'><h3>Retention Rate - " + drow["Category_Name"].ToString() + "</h3></td>");
                HttpContext.Current.Response.Write("</tr>");
                HttpContext.Current.Response.Write("</TABLE>");
                HttpContext.Current.Response.Write("<br />");


                HttpContext.Current.Response.Write("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='2' width='100%'>");
                HttpContext.Current.Response.Write("<tr style='background-color: #F2F2F2;'>");
                for (int i = 0; i < HeaderCaptions.Length; i++)
                {
                    HttpContext.Current.Response.Write("<td width='20%'>");
                    HttpContext.Current.Response.Write("<b>" + HeaderCaptions[i] + "</b>");
                    HttpContext.Current.Response.Write("</td>");
                }
                HttpContext.Current.Response.Write("</tr>");
                foreach (DataRow dr in dt.Rows)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    for (int i = 0; i < DataColumnsName.Length; i++)
                    {
                        HttpContext.Current.Response.Write("<td>");
                        HttpContext.Current.Response.Write(dr[DataColumnsName[i]]);
                        HttpContext.Current.Response.Write("</td>");
                    }
                    HttpContext.Current.Response.Write("</tr>");
                }
                HttpContext.Current.Response.Write("</TABLE>");
            }

            HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    
}