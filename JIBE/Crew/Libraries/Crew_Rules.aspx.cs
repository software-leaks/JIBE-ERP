using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.Technical;
using SMS.Business.Crew;


public partial class Crew_Rules : System.Web.UI.Page
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

            ViewState["Rule_ID"] = null;

            ucCustomPagerItems.PageSize = 20;

            BindCrewRule();

            BindVesselSpecificRule();
            BindRankSpecificRule();

        }

    }

    public void BindCrewRule()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.Crew_Rules_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvCrewRules.DataSource = dt;
            gvCrewRules.DataBind();
        }
        else
        {
            gvCrewRules.DataSource = dt;
            gvCrewRules.DataBind();
        }

    }

    public void BindVesselSpecificRule()
    {

        int? Rule_ID = null; if (ViewState["Rule_ID"] != null) Rule_ID = Int32.Parse(ViewState["Rule_ID"].ToString());


        DataTable dt = objBLL.Crew_Get_Vessel_Specific_Rules_Search(Rule_ID);

        if (dt.Rows.Count > 0)
        {
            gvVesselRule.DataSource = dt;
            gvVesselRule.DataBind();
        }
        else
        {

            gvVesselRule.DataSource = dt;
            gvVesselRule.DataBind();

        }


    }

    public void BindRankSpecificRule()
    {
        int? Rule_ID = null; if (ViewState["Rule_ID"] != null) Rule_ID = Int32.Parse(ViewState["Rule_ID"].ToString());

        DataTable dt = objBLL.Crew_Get_Rank_Specific_Rules_Search(Rule_ID);

        if (dt.Rows.Count > 0)
        {
            gvRankRules.DataSource = dt;
            gvRankRules.DataBind();
        }
        else
        {
            gvRankRules.DataSource = dt;
            gvRankRules.DataBind();
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

        if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnsave.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtRule");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Rule";

        ClearField();

        string AddTradeZone = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddTradeZone", AddTradeZone, true);
    }

    protected void ClearField()
    {
        txtRuleID.Text = "";
        txtRule.Text = "";
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (HiddenFlag.Value == "Add")
        {
            int retval = objBLL.Insert_Crew_Rules(txtRule.Text, Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            int retval = objBLL.Update_Crew_Rules(Convert.ToInt32(txtRuleID.Text.Trim()), txtRule.Text, Convert.ToInt32(Session["USERID"]));
        }

        BindCrewRule();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Rule";

        DataTable dt = new DataTable();
        dt = objBLL.Get_Crew_Rules_List(Convert.ToInt32(e.CommandArgument.ToString()));

        txtRuleID.Text = dt.Rows[0]["RULE_ID"].ToString();
        txtRule.Text = dt.Rows[0]["Description"].ToString();

        string CrewRule = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "CrewRule", CrewRule, true);

    }

    protected void onSelect(object source, CommandEventArgs e)
    {
        ViewState["Rule_ID"] = e.CommandArgument.ToString();

        BindVesselSpecificRule();
        BindRankSpecificRule();
    }


    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.Delete_Crew_Rules(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindCrewRule();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindCrewRule();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        BindCrewRule();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLL.Crew_Rules_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , null, null, ref  rowcount);


        string[] HeaderCaptions = { "Rule ID", "Rule" };
        string[] DataColumnsName = { "RULE_ID", "Description" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "CrewRule", "Crew Rule");

    }

    protected void gvCrewRules_RowDataBound(object sender, GridViewRowEventArgs e)
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

    }

    protected void gvCrewRules_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindCrewRule();
    }


    protected void btnSaveAssignment_Click(object sender, EventArgs e)
    {

        if (gvCrewRules.SelectedIndex != -1)
        {

            foreach (GridViewRow gr in gvVesselRule.Rows)
            {
                CheckBox chkVesselFlag = (CheckBox)gr.FindControl("chkVesselFlag");

                string lblVesselID = ((Label)gr.FindControl("lblVesselID")).Text;

                int retval = objBLL.Crew_Vessel_Specific_Rules_Assignment(Convert.ToInt32(lblVesselID), Convert.ToInt32(gvCrewRules.DataKeys[gvCrewRules.SelectedIndex].Values["RULE_ID"]), chkVesselFlag.Checked == true ? 1 : 0, Convert.ToInt32(Session["userid"].ToString()));
            }


            foreach (GridViewRow gr in gvRankRules.Rows)
            {
                CheckBox chkRankFlag = (CheckBox)gr.FindControl("chkRankFlag");

                string lblRankID = ((Label)gr.FindControl("lblRankID")).Text;

                int retval = objBLL.Crew_Rank_Specific_Rules_Assignment(Convert.ToInt32(lblRankID), Convert.ToInt32(gvCrewRules.DataKeys[gvCrewRules.SelectedIndex].Values["RULE_ID"]), chkRankFlag.Checked == true ? 1 : 0, Convert.ToInt32(Session["userid"].ToString()));
            }

         
        }
        else
        { 
        
        }

        BindVesselSpecificRule();
        BindRankSpecificRule();

        //if (!blnRecSel)
        //{

        //    lblErrMsg.Text = "Please select location/s to assign";

        //    string AssginLocmodal = String.Format("showModal('divAddLocation',false);");
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ApprmodalUser", AssginLocmodal, true);
        //}

    }

 
}