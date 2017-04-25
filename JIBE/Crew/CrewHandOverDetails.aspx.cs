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

public partial class CrewHandOverDetails : System.Web.UI.Page
{
    BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                int CurrentUserID = GetSessionUserID();
                hdnUserID.Value = CurrentUserID.ToString();
                string tabid = TabSCM.ActiveTab.ID;
                string tabindex = TabSCM.ActiveTab.TabIndex.ToString();
                ucCustomPager1.PageSize = 20;
                ucCustomPager.PageSize = 20;
                BindHandOver();
                BindCheckList();
            }
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


    public void BindHandOver()
    {
        int PAGE_SIZE = ucCustomPager.PageSize;
        int PAGE_INDEX = ucCustomPager.CurrentPageIndex;

        int SelectRecordCount = ucCustomPager.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataSet ds = BLL_Crew_CrewList.Get_HandOverDetail_Search(int.Parse(Request.QueryString["ID"].ToString()), int.Parse(Request.QueryString["VESSELID"].ToString()), txtSearchText.Text, sortbycoloumn, sortdirection, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtVesselName.Text = dr["Vessel_Name"].ToString();
            txtStaffCode.Text = dr["STAFF_CODE"].ToString();
            txtStaffName.Text = dr["FULL_NAME"].ToString();
            txtStaffRank.Text = dr["Rank_Name"].ToString();
            txtRelievingMasterRemark.Text = dr["Sing_off_Remarks"].ToString();
            txtRelievedMasterRemark.Text = dr["Relieved_Master_Remark"].ToString();
            if (dr["Joining_Rank"].ToString() != "1")
                CheckList.Visible = false;
            if (dr["HANDOVER_DATE"] != null)
                txtHandOverDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dr["HANDOVER_DATE"]));
            lbltxtRelievingMasterRemark.Text = "Relieving " + txtStaffRank.Text + "'s Remarks :";
            lbltxtRelievedMasterRemark.Text = "Relieved " + txtStaffRank.Text + "'s Remarks :";
        }
        gvHandOver.DataSource = ds.Tables[1];
        gvHandOver.DataBind();

        if (ucCustomPager.isCountRecord == 1)
        {
            ucCustomPager.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager.BuildPager();
        }
    }

    public void BindCheckList()
    {

        int PAGE_SIZE = ucCustomPager1.PageSize;
        int PAGE_INDEX = ucCustomPager1.CurrentPageIndex;

        int SelectRecordCount = ucCustomPager.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataSet ds = BLL_Crew_CrewList.Get_ChechList_Search(int.Parse(Request.QueryString["ID"].ToString()), int.Parse(Request.QueryString["VESSELID"].ToString()), txtSearchText.Text, sortbycoloumn, sortdirection, PAGE_SIZE, PAGE_INDEX, ref SelectRecordCount);
        
        gvCheckList.DataSource = ds.Tables[0];
        gvCheckList.DataBind();

        if (ucCustomPager1.isCountRecord == 1)
        {
            ucCustomPager1.CountTotalRec = SelectRecordCount.ToString();
            ucCustomPager1.BuildPager();
        }
    }
    protected void gvHandOver_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
        ucCustomPager.isCountRecord = 1;
        BindHandOver();

        ucCustomPager1.isCountRecord = 1;
        BindCheckList();
    }

    protected void gvHandOver_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindHandOver();
    }

    protected void gvCheckList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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

    protected void gvCheckList_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindCheckList();
    }
    protected void TabSCM_ActiveTabChanged(object sender, EventArgs e)
    {
        BindTabs();
    }
    protected void BindTabs()
    {
        int p = 1;
        if (TabSCM.ActiveTab.TabIndex != 9)
        {
            switch (TabSCM.ActiveTab.TabIndex)
            {
                case 0:

                    BindHandOver();

                    break;
                case 1:

                    BindCheckList();

                    break;
                default: break;
            }
        }
    }
}