using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class Infrastructure_Libraries_JiBePacketStatus : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;

            Load_VesselList();
            BindJiBePacketGrid();
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

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void BindJiBePacketGrid()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objVessel.Get_JiBePacketStatus_Search(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertIntegerToNull(ddlStatus.SelectedValue), UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString())
            , UDFLib.ConvertDateToNull(txtFromDate.Text), UDFLib.ConvertDateToNull(txtToDate.Text), sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        gvPktStat.DataSource = dt;
        gvPktStat.DataBind();

    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objVessel.Get_JiBePacketStatus_Search(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), UDFLib.ConvertIntegerToNull(ddlStatus.SelectedValue), UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString())
            , UDFLib.ConvertDateToNull(txtFromDate.Text), UDFLib.ConvertDateToNull(txtToDate.Text), sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        string[] HeaderCaptions = { "Vessel", "Packet Name", "Send Status", "Execute Status", "Send Date", "Date Of Execution" };
        string[] DataColumnsName = { "Vessel_Name", "Packet_Name", "Send_Status", "Execute_Status", "Send_Date", "Execute_Date" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "JiBePacketStatus", "JiBe Packet Status", "");
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {

        BindJiBePacketGrid();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ddlVessel.SelectedValue = "0";
        ddlStatus.SelectedValue = "0";
        txtFromDate.Text = "";
        txtToDate.Text = "";

        BindJiBePacketGrid();
    }

    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVessel.SelectedValue != "0")
            ddlStatus.Enabled = true;
        else
            ddlStatus.Enabled = false;
    }

    protected void Load_VesselList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        DataTable dt = objVessel.Get_VesselList(0, 0, 0, "", UserCompanyID);


        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-ALL-", "0"));

    }

    protected void gvPktStat_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowUp.png";
                    else
                        img.Src = "~/purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
        }

    }

    protected void gvPktStat_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindJiBePacketGrid();
    }
}