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



public partial class Crew_Retantion : System.Web.UI.Page
{
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
    public UserAccess objUA = new UserAccess();
    public string[] Vessel_Ids ;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
      
        if (!IsPostBack)
        {
            Load_RankList();
            Load_Years();
            LoadData();
            loadPIDetails();
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
        ddlYear.Select ((DateTime.Now.AddYears(-1)).Year.ToString());

           
    }



    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        LoadData();

    }

    private void LoadData()
    {
        if (ddlYear.SelectedValues.Rows.Count > 2)
        {
            string msg2 = String.Format("alert('Maximum 2 years can be selected!')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        else
        {
            GenearteDiv();

            string funtion = "showChart();";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", funtion, true);
        }
    }

    /// <summary>
    /// Event Method to reinitialize the page
    /// @Author: Bhairab
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
    /// @author:Bhairab
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
            BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
            PlaceHolder1.Controls.Clear();
            int rowcount = 0;
            DataTable dt = objCrewAdmin.Get_RankCategories();
            rowcount = dt.Rows.Count;
            string shdnCategoryID = "0";
            string shdnCategory = "Selected Ranks";
            hdnCategoryID.Value = shdnCategoryID;
            hdnCategory.Value = shdnCategory;
            foreach (DataRow dr in dt.Rows)
            {

                hdnCategory.Value = hdnCategory.Value + "," + dr["Category_Name"].ToString();
                hdnCategoryID.Value = hdnCategoryID.Value + "," + dr["ID"].ToString();
            }
            hdnCategory.Value = hdnCategory.Value.Trim(',');
            hdnCategoryID.Value = hdnCategoryID.Value.Trim(',');
            int totalCategories = dt.Rows.Count + 1;
            int tblRows = 1;
            int tblCols = 2;//--do--
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

                        if (i == 0 && j == 0)
                        {
                            CategoryId = "0";
                            CategoyName = "Selected Ranks";
                        }
                        else
                        {
                            CategoryId = dt.Rows[iCount]["ID"].ToString();
                            CategoyName = dt.Rows[iCount]["Category_Name"].ToString();
                            catindex++;
                        }
                        HtmlGenericControl newControl = new HtmlGenericControl("div");
                        HiddenField hdnCatId = new HiddenField();
                        HiddenField hdnCatName = new HiddenField();
                        hdnCatId.ID = "hdnID_" + i + j;
                        hdnCatName.ID = "hdnName_" + i + j;

                        newControl.ID = sContainer + i + j;
                        newControl.Attributes.Add("Style", "Height:300px;width:700px;float:left");
                        newControl.Attributes.Add("onclick", "openDetail('" + CategoryId + "','" + CategoyName + "')");
                        newControl.InnerHtml = "";
                        tc.Controls.Add(newControl);

                        hdnCatId.Value = CategoryId;
                        hdnCatName.Value = CategoyName;
                        tc.Controls.Add(hdnCatId);
                        tc.Controls.Add(hdnCatName);
                        tr.Cells.Add(tc);
                        iCount++;
                    }
                }
                tbl.Rows.Add(tr);
            }

            hiddenCount.Value = tblRows.ToString();
            hiddenCount1.Value = tblCols.ToString();


        }
        catch (Exception ex)
        {

        }
    }

    
}