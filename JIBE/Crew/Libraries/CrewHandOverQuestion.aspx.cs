using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.Crew;


public partial class CrewHandOverQuestion : System.Web.UI.Page
{


    BLL_Crew_Admin objBLL = new BLL_Crew_Admin();

    UserAccess objUA = new UserAccess();

    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            BindRank();

            BindCrewContract();
        }

    }




    protected void BindRank()
    {

        DataTable dt = objBLL.Get_RankList();

        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "Rank_Short_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("-SELECT-", "0"));


        ddlRankFilter.DataSource = dt;
        ddlRankFilter.DataTextField = "Rank_Short_Name";
        ddlRankFilter.DataValueField = "ID";
        ddlRankFilter.DataBind();
        ddlRankFilter.Items.Insert(0, new ListItem("-ALL-", "0"));

    }



    public void BindCrewContract()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.Crew_HandOverQuestion_Search(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlRankFilter.SelectedValue), sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {

            gvCrewContract.DataSource = dt;
            gvCrewContract.DataBind();
        }
        else
        {
            gvCrewContract.DataSource = dt;
            gvCrewContract.DataBind();
        }

    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        //if (objUA.View == 0)
        //    Response.Redirect("~/default.aspx?msgid=1");

        //if (objUA.Add == 0)
        //{
        //   // ImgAdd.Visible = false;
        //}
        //if (objUA.Edit == 1)
        uaEditFlag = true;
        //else 
        //    btnsave.Visible = false;

        if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }



    protected void ClearField()
    {
        txtQuestion.Text = "";
        ddlRank.SelectedValue = "0";
        ddlRank.Enabled = true;

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int? strCheckList = null;
        string strDataType = null;
        if (chkCheckList.Checked)
        {
            strCheckList = 1;
            strDataType = ddlDataType.SelectedValue;
        }
        if (HiddenFlag.Value == "Add")
        {
            int retval = objBLL.Ins_Update_HandOverQuestion(null, UDFLib.ConvertToInteger(ddlRank.SelectedValue), txtQuestion.Text, strCheckList, strDataType, Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            int retval = objBLL.Ins_Update_HandOverQuestion(UDFLib.ConvertIntegerToNull(txtQuestionID.Text.Trim()), UDFLib.ConvertToInteger(ddlRank.SelectedValue), txtQuestion.Text, strCheckList, strDataType, Convert.ToInt32(Session["USERID"]));
        }

        BindCrewContract();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Question";

        DataTable dt = new DataTable();
        dt = objBLL.Get_Crew_HandOverQuestion(Convert.ToInt32(e.CommandArgument.ToString()));
        txtQuestionID.Text = dt.Rows[0]["ID"].ToString();
        txtQuestion.Text = dt.Rows[0]["HANDOVER_QUESTION"].ToString();
        ddlRank.SelectedValue = dt.DefaultView[0]["CREW_RANK"].ToString() != "" ? dt.DefaultView[0]["CREW_RANK"].ToString() : "0";
        if (dt.DefaultView[0]["IsCheckList"].ToString() == "1")
        {
            chkCheckList.Checked = true;
        }
        else
        {
            chkCheckList.Checked = false;
            ddlDataType.SelectedIndex = 1;
        }


        ddlDataType.SelectedValue = dt.DefaultView[0]["Data_Type"].ToString() != "" ? dt.DefaultView[0]["Data_Type"].ToString() : "0";
        ddlRank.Enabled = false;

        string AddSingoffReasonmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSingoffReasonmodal", AddSingoffReasonmodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.Delete_Crew_Contract_Period(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));

        BindCrewContract();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindCrewContract();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        ddlRankFilter.SelectedValue = "0";
        BindCrewContract();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;


        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.Crew_HandOverQuestion_Search(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlRankFilter.SelectedValue), sortbycoloumn, sortdirection
             , 1, null, ref  rowcount);


        string[] HeaderCaptions = { "Rank Name", "QUESTION" };
        string[] DataColumnsName = { "Rank_Short_Name", "HANDOVER_QUESTION" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Handover Question.xls", "Handover Question");

    }

    protected void gvCrewContract_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvCrewContract_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindCrewContract();
    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_ddlRank");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Question";

        txtQuestionID.Text = "";
        txtQuestion.Text = "";
        chkCheckList.Checked = false;
        ddlRank.SelectedIndex = 0;
        ddlRank.Enabled = true;


        string AddCountrymodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCountrymodal", AddCountrymodal, true);
    }
}