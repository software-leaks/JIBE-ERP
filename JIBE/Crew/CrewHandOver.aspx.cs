using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using SMS.Business.Infrastructure;
using SMS.Business.Crew;
using SMS.Properties;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class CrewHandOver : System.Web.UI.Page
{
    BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            int CurrentUserID = GetSessionUserID();
            hdnUserID.Value = CurrentUserID.ToString();
            Load_FleetList();
            Load_VesselList();
            BindRank();
            ucCustomPager.PageSize = 30;
            BindMasterReview();
        }
        //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);   
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

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
    private string getSessionString(string SessionField)
    {
        try
        {
            if (Session[SessionField] != null && Session[SessionField].ToString() != "")
            {
                return Session[SessionField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }

    public void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }

    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));
        int Vessel_Manager = UDFLib.ConvertToInteger(getSessionString("USERCOMPANYID"));

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessel.SelectedIndex = 0;
    }

    protected void BindRank()
    {
        DataTable dt = objBLL.Get_RankList();

        ddlRankFilter.DataSource = dt;
        ddlRankFilter.DataTextField = "Rank_Short_Name";
        ddlRankFilter.DataValueField = "ID";
        ddlRankFilter.DataBind();
        ddlRankFilter.Items.Insert(0, new ListItem("-ALL-", "0"));

    }


    public void BindMasterReview()
    {
        int PAGE_SIZE = ucCustomPager.PageSize;
        int PAGE_INDEX = ucCustomPager.CurrentPageIndex;

        int SelectRecordCount = ucCustomPager.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = BLL_Crew_CrewList.Get_HandOver_Search(UDFLib.ConvertIntegerToNull(ddlFleet.SelectedValue.ToString())
            , UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue.ToString()), txtSearchText.Text, UDFLib.ConvertIntegerToNull(ddlRankFilter.SelectedValue.ToString()), sortbycoloumn, sortdirection, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);


        if (ucCustomPager.isCountRecord == 1)
        {
            ucCustomPager.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager.BuildPager();
        }

        gvMasterReview.DataSource = dt;
        gvMasterReview.DataBind();
    }

    protected void gvMasterReview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
            Label lblVesselID = (Label)(e.Row.FindControl("lblVesselID"));
            Label lblID = (Label)(e.Row.FindControl("lblID"));
            e.Row.Attributes.Add("onClick", "javascript:window.open('../CREW/CrewHandOverDetails.aspx?ID=" + lblID.Text.Trim() + "&VESSELID=" + lblVesselID.Text + "'); return false;");

        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/Images/arrowUp.png";
                    else
                        img.Src = "~/Images/arrowDown.png";

                    img.Visible = true;
                }
            }
        }
    }



    protected void BtnSearch_Click(object sender, EventArgs e)
    {
       BindMasterReview();
    }



    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
    }

    protected void gvMasterReview_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindMasterReview();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPager.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = BLL_Crew_CrewList.Get_HandOver_Search(UDFLib.ConvertIntegerToNull(ddlFleet.SelectedValue.ToString())
            , UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue.ToString()), txtSearchText.Text, UDFLib.ConvertIntegerToNull(ddlRankFilter.SelectedValue), sortbycoloumn, sortdirection, null, null, ref rowcount);

        string[] HeaderCaptions = { "Vessel Name", "Staff Code", "Staff Name", "Rank Name", "Hand Over Date", "Sign off Remark" };
        string[] DataColumnsName = { "Vessel_Name", "STAFF_CODE", "FULL_NAME", "Rank_Short_Name", "Handover_Date", "Sing_off_Remarks" };
        ChangeColumnDataType(dt, "Handover_Date", typeof(string));
        foreach (DataRow item in dt.Rows)
        {
            if (!string.IsNullOrEmpty(item["Handover_Date"].ToString()))
                item["Handover_Date"] = UDFLib.ConvertUserDateFormat(Convert.ToString(item["Handover_Date"]), UDFLib.GetDateFormat());
        }
        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "CrewHandOver.xls", "Crew Handover");

    }
    public static bool ChangeColumnDataType(DataTable table, string columnname, Type newtype)
    {
        if (table.Columns.Contains(columnname) == false)
            return false;

        DataColumn column = table.Columns[columnname];
        if (column.DataType == newtype)
            return true;

        try
        {
            DataColumn newcolumn = new DataColumn("temporary", newtype);
            table.Columns.Add(newcolumn);
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    row["temporary"] = Convert.ChangeType(row[columnname], newtype);
                }
                catch
                {
                }
            }
            table.Columns.Remove(columnname);
            newcolumn.ColumnName = columnname;
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
    protected void btn_Filter_Click(object sender, EventArgs e)
    {
        ddlFleet.SelectedValue = null;
        ddlVessel.SelectedValue = null;
        ddlRankFilter.SelectedValue = null;
        txtSearchText.Text = "";
        BindMasterReview();

    }
}