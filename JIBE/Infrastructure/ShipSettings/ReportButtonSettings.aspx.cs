using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using SMS.Business.Crew;
using SMS.Business.PMS;
using System.Text;
using SMS.Properties;


public partial class ReportButtonSettings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ViewState["ProjectID"] = null;
            ViewState["ModuleID"] = null;
            ViewState["ScreenID"] = null;
            ViewState["ID"] = null;
            BindProjects();
            BindModules();
            BindScreen();
        }

    }
    public void BindProjects()
    {

        string sortbycoloumn = (ViewState["PROJECTSORTBYCOLOUMN"] == null) ? null : (ViewState["PROJECTSORTBYCOLOUMN"].ToString());
        int? sortdirection = null;

        if (ViewState["PROJECTSORTDIRECTION"] != null)
            sortdirection = Int32.Parse(ViewState["PROJECTSORTDIRECTION"].ToString());


        DataTable dt = BLL_Infra_ShipSettings.Get_Project_Search("", sortbycoloumn, sortdirection);

        if (dt.Rows.Count > 0)
        {
            gvProject.DataSource = dt;
            gvProject.DataBind();

            if (ViewState["ProjectID"] == null)
            {
                ViewState["ProjectID"] = dt.Rows[0]["Project_ID"].ToString();
                gvProject.SelectedIndex = 0;
            }

            SetProjectRowSelection();
        }
        else
        {
            gvProject.DataSource = dt;
            gvProject.DataBind();
            ViewState["ProjectID"] = null;
        }

        UpdProjectGrid.Update();
       

    }

    public void BindModules()
    {
        int? Project_ID = null;
        if (ViewState["ProjectID"] != null) Project_ID = UDFLib.ConvertToInteger(ViewState["ProjectID"].ToString());

        string sortbycoloumn = (ViewState["MODULESORTBYCOLOUMN"] == null) ? null : (ViewState["MODULESORTBYCOLOUMN"].ToString());
        int? sortdirection = null;

        if (ViewState["MODULESORTDIRECTION"] != null)
            sortdirection = Int32.Parse(ViewState["MODULESORTDIRECTION"].ToString());

        DataTable dt = BLL_Infra_ShipSettings.Get_Module_Search("", Project_ID, sortbycoloumn, sortdirection);

        if (dt.Rows.Count > 0)
        {


            if (ViewState["ModuleID"] == null)
            {
                ViewState["ModuleID"] = dt.Rows[0]["Module_ID"].ToString();
            }

        }
        else
        {
            ViewState["ModuleID"] = null;
        }

    }

    public void BindScreen()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        int? ModuleID = null;

        if (ViewState["ModuleID"] != null) ModuleID = UDFLib.ConvertToInteger(ViewState["ModuleID"].ToString());

        string sortbycoloumn = (ViewState["SCREENSORTBYCOLOUMN"] == null) ? null : (ViewState["SCREENSORTBYCOLOUMN"].ToString());
        int? sortdirection = null;

        if (ViewState["SCREENSORTDIRECTION"] != null)
            sortdirection = Int32.Parse(ViewState["SCREENSORTDIRECTION"].ToString());

        DataTable dt = BLL_Infra_ShipSettings.Get_Screen_Search("", ModuleID, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref rowcount);

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvScreens.DataSource = dt;
            gvScreens.DataBind();

            if (ViewState["ScreenID"] == null)
            {
                ViewState["ScreenID"] = dt.Rows[0]["Screen_ID"].ToString();

                gvScreens.SelectedIndex = 0;
                BindScreen_By_SrceenID();
            }
            SetScreenRowSelection();
        }
        else
        {
            gvScreens.DataSource = dt;
            gvScreens.DataBind();
            ViewState["ScreenID"] = null;
            BindScreen_By_SrceenID();
        }

        ucCustomPagerItems.Visible = false;
        UpdScreenGrid.Update();

    }


    private void BindScreen_By_SrceenID()
    {

        if ( ViewState["ScreenID"] != null)
        {
            try
            {
                DataTable dtdetails = new DataTable();
                gvDetails.DataSource = null;
                gvDetails.DataBind();
                dtdetails = BLL_Infra_ShipSettings.Get_Exist_Dtails_ByScreenID(UDFLib.ConvertToInteger(ViewState["ScreenID"]));
                if (dtdetails.Rows.Count > 0)
                {
                    gvDetails.DataSource = dtdetails;
                    gvDetails.DataBind();
                }
                ucCustomPagerItems.Visible = false;
                upDetails.Update();
            }
            catch (Exception)
            {

                throw;
            }
        }
        else
        {
            gvDetails.DataSource = null;
            gvDetails.DataBind();
            upDetails.Update();
        }

    }

    private void ClearProjectFields()
    {
        ViewState["ProjectID"] = null;
    }

    private void ClearModuleFields()
    {
        ViewState["ModuleID"] = null;
    }   



    private void SetProjectRowSelection()
    {
        gvProject.SelectedIndex = -1;
        for (int i = 0; i < gvProject.Rows.Count; i++)
        {
            if (gvProject.DataKeys[i].Value.ToString().Equals(ViewState["ProjectID"].ToString()))
            {
                gvProject.SelectedIndex = i;
            }
        }
    }


    private void SetScreenRowSelection()
    {
        gvScreens.SelectedIndex = -1;
        for (int i = 0; i < gvScreens.Rows.Count; i++)
        {
            if (gvScreens.DataKeys[i].Value.ToString().Equals(ViewState["ScreenID"].ToString()))
            {
                gvScreens.SelectedIndex = i;
            }
        }
    }

    
    protected void gvProject_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["PROJECTSORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["PROJECTSORTDIRECTION"] != null && ViewState["PROJECTSORTDIRECTION"].ToString() == "0")
            ViewState["PROJECTSORTDIRECTION"] = 1;
        else
            ViewState["PROJECTSORTDIRECTION"] = 0;

        BindProjects();

    }

    protected void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["PROJECTSORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["PROJECTSORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["PROJECTSORTDIRECTION"] == null || ViewState["PROJECTSORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowDown.png";
                    else
                        img.Src = "~/purchase/Image/arrowUp.png";

                    img.Visible = true;
                }
            }
        }
    }

    protected void gvProject_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {

        ucCustomPagerItems.isCountRecord = 1;
        gvProject.SelectedIndex = se.NewSelectedIndex;

        ViewState["ProjectID"] = ((Label)gvProject.Rows[se.NewSelectedIndex].FindControl("lblProjectID")).Text;

        ViewState["ModuleID"] = null;
        

        BindProjects();
        BindModules();
        BindScreen();

        ClearModuleFields();
      
        this.SetFocus("txtModule");
        ViewState["ScreenID"] = null;
        UpdProjectGrid.Update();

        UpdScreenGrid.Update();

    }


    protected void gvModule_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["MODULESORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["MODULESORTDIRECTION"] != null && ViewState["MODULESORTDIRECTION"].ToString() == "0")
            ViewState["MODULESORTDIRECTION"] = 1;
        else
            ViewState["MODULESORTDIRECTION"] = 0;

        BindModules();
    }

    protected void gvScreens_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SCREENSORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SCREENSORTDIRECTION"] != null && ViewState["SCREENSORTDIRECTION"].ToString() == "0")
            ViewState["SCREENSORTDIRECTION"] = 1;
        else
            ViewState["SCREENSORTDIRECTION"] = 0;

        BindScreen();

    }

    protected void gvScreens_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SCREENSORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SCREENSORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SCREENSORTDIRECTION"] == null || ViewState["SCREENSORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowDown.png";
                    else
                        img.Src = "~/purchase/Image/arrowUp.png";

                    img.Visible = true;
                }
            }
        }



    }

    protected void lnbHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/crew/default.aspx");
    }
    protected void onAddButton(object source, CommandEventArgs e)
    {

        ViewState["AddEdit"] = "Add";
        txtButtonName.Text = "";
        chkRanksList.Items.Clear();
        txtButtonName.Enabled = true;
        string[] cmdargs = e.CommandArgument.ToString().Split(',');

        ViewState["ScreenID"] = cmdargs[0].ToString();

        DataTable dt = BLL_Infra_ShipSettings.Get_RankList(0);
        chkRanksList.DataTextField = "Rank_Short_Name";
        chkRanksList.DataValueField = "ID";
        chkRanksList.DataSource = dt;

        chkRanksList.DataBind();

        string Ranklist = String.Format("showModal('dvAddButton',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Ranklist", Ranklist, true);
        UpdScreenEntry.Update();

    }   

    protected void SelectScreen(object source, CommandEventArgs e)
    {

        string[] cmdargs = e.CommandArgument.ToString().Split(',');
        ViewState["ScreenID"] = cmdargs[0].ToString();
        BindScreen_By_SrceenID();
        SetScreenRowSelection();

    }

    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsg.Visible = false;
        int REFF = 0;
        if (ViewState["AddEdit"].ToString() == "Add")
        {
            int MaxID = BLL_Infra_ShipSettings.Ins_RANK_SCREEN_Access(UDFLib.ConvertToInteger(ViewState["ScreenID"].ToString()), txtButtonName.Text.Trim(), UDFLib.ConvertToInteger(Session["UserID"].ToString()), ref REFF);
            DataTable dt = new DataTable();

            dt.Columns.Add("Rank_ID");
            dt.Columns.Add("RANK_SCREEN_Access_ID");
            dt.Columns.Add("Created_By");

            foreach (ListItem chkitem in chkRanksList.Items)
            {
                DataRow dr = dt.NewRow();
                dr["Rank_ID"] = UDFLib.ConvertToInteger(chkitem.Value);
                dr["RANK_SCREEN_Access_ID"] = MaxID;
                dr["Created_By"] = UDFLib.ConvertToInteger(Session["UserID"].ToString());

                if (chkitem.Selected == true)
                    dt.Rows.Add(dr);
            }
            if (dt.Rows.Count != 0)
            {
                int result = BLL_Infra_ShipSettings.Ins_RANK_SCREEN_Access_Details(dt);
                string Ranklist = String.Format("hideModal('dvAddButton',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Ranklist", Ranklist, true);
                BindScreen_By_SrceenID();
                upDetails.Update();
                ViewState["AddEdit"] = null;
            }
            else
            {
                string Ranklist = "Please select at least one rank..! ";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Ranklist", Ranklist, true);
                lblMsg.Text = "Please select at least one rank..! ";
                lblMsg.Visible = true;
            }

        }
        else
        {
            int MaxID = BLL_Infra_ShipSettings.Update_RANK_SCREEN_Access(UDFLib.ConvertToInteger(ViewState["ID"].ToString()), txtButtonName.Text.Trim(), UDFLib.ConvertToInteger(Session["UserID"].ToString()));
            DataTable dt = new DataTable();

            dt.Columns.Add("Rank_ID");
            dt.Columns.Add("RANK_SCREEN_Access_ID");
            dt.Columns.Add("Created_By");

            foreach (ListItem chkitem in chkRanksList.Items)
            {
                DataRow dr = dt.NewRow();
                dr["Rank_ID"] = UDFLib.ConvertToInteger(chkitem.Value);
                dr["RANK_SCREEN_Access_ID"] = UDFLib.ConvertToInteger(ViewState["ID"].ToString());
                dr["Created_By"] = UDFLib.ConvertToInteger(Session["UserID"].ToString());

                if (chkitem.Selected == true)
                    dt.Rows.Add(dr);
            }
            if (dt.Rows.Count != 0)
            {
                int result = BLL_Infra_ShipSettings.Update_RANK_SCREEN_Access_Details(dt, UDFLib.ConvertToInteger(ViewState["ID"].ToString()));
                string Ranklist = String.Format("hideModal('dvAddButton',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Ranklist", Ranklist, true);
                BindScreen_By_SrceenID();               
                upDetails.Update();
                ViewState["AddEdit"] = null;
            }
            else
            {
                string Ranklist = "Please select at least one rank..! ";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Ranklist", Ranklist, true);
                lblMsg.Text = "Please select at least one rank..! ";
                lblMsg.Visible = true;
            }

        }


    }

    protected void Edit(object source, CommandEventArgs e)
    {


        ViewState["AddEdit"] = "Edit";
        txtButtonName.Text = "";
        chkRanksList.Items.Clear();
        //int ScreenID = 0;
        string[] cmdargs = e.CommandArgument.ToString().Split(',');

        ViewState["ID"] = cmdargs[0].ToString();
        DataTable dt = BLL_Infra_ShipSettings.Get_RankList(0);
        chkRanksList.DataTextField = "Rank_Short_Name";
        chkRanksList.DataValueField = "ID";
        chkRanksList.DataSource = dt;
        chkRanksList.DataBind();

        DataTable dtDetails = BLL_Infra_ShipSettings.Get_RankList(UDFLib.ConvertToInteger(ViewState["ID"]));

        txtButtonName.Text = dtDetails.Rows[0]["Key"].ToString();
        txtButtonName.Enabled = false;
        int i = 0;
        foreach (ListItem chkitem in chkRanksList.Items)
        {
            if (dtDetails.Rows[i]["Selected"].ToString() == "1")
            {
                chkitem.Selected = true;
            }
            i++;
        }

        string msgdivResponseShow = string.Format("showModal('dvAddButton',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
        UpdScreenEntry.Update();

    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int Details_ID = UDFLib.ConvertToInteger(gvDetails.DataKeys[e.RowIndex].Values[1].ToString());
        ViewState["ScreenID"] = UDFLib.ConvertToInteger(gvDetails.DataKeys[e.RowIndex].Values[0].ToString());
        int result = BLL_Infra_ShipSettings.Delete_RANK_SCREEN_Access_Details(Details_ID, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
        BindScreen_By_SrceenID();
        upDetails.Update();

    }

    protected void chkRanksList_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
    }
}