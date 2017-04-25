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


public partial class ShipProjectMgmt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ViewState["ProjectID"] = null;
            ViewState["ModuleID"] = null;
            ViewState["ScreenID"] = null;

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
        
        
        DataTable dt = BLL_Infra_ShipSettings.Get_Project_Search(txtSearchProject.Text, sortbycoloumn, sortdirection);

        if (dt.Rows.Count > 0)
        {
            gvProject.DataSource = dt;
            gvProject.DataBind();

            if (ViewState["ProjectID"] == null)
            {
                ViewState["ProjectID"] = dt.Rows[0]["Project_ID"].ToString();
                gvProject.SelectedIndex = 0;
            }

            BindProjectList(Convert.ToInt32(ViewState["ProjectID"].ToString()));

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
        if (ViewState["ProjectID"] != null) Project_ID = Convert.ToInt32(ViewState["ProjectID"].ToString());

        string sortbycoloumn = (ViewState["MODULESORTBYCOLOUMN"] == null) ? null : (ViewState["MODULESORTBYCOLOUMN"].ToString());
        int? sortdirection = null;

        if (ViewState["MODULESORTDIRECTION"] != null)
            sortdirection = Int32.Parse(ViewState["MODULESORTDIRECTION"].ToString());

        DataTable dt = BLL_Infra_ShipSettings.Get_Module_Search(txtSearchModule.Text, Project_ID,sortbycoloumn,sortdirection);

        if (dt.Rows.Count > 0)
        {
            gvModule.DataSource = dt;
            gvModule.DataBind();

            if (ViewState["ModuleID"] == null)
            {
                ViewState["ModuleID"] = dt.Rows[0]["Module_ID"].ToString();

                BindModuleList(Convert.ToInt32(ViewState["ModuleID"].ToString()));

                gvModule.SelectedIndex = 0;
            }
            SetModuleRowSelection();
        }
        else
        {
            gvModule.DataSource = dt;
            gvModule.DataBind();
            ViewState["ModuleID"] = null;
        }

        UpdModuleGrid.Update();
    }

    public void BindScreen()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        int? ModuleID = null;

        if (ViewState["ModuleID"] != null) ModuleID = Convert.ToInt32(ViewState["ModuleID"].ToString());

        string sortbycoloumn = (ViewState["SCREENSORTBYCOLOUMN"] == null) ? null : (ViewState["SCREENSORTBYCOLOUMN"].ToString());
        int? sortdirection = null;

        if (ViewState["SCREENSORTDIRECTION"] != null)
            sortdirection = Int32.Parse(ViewState["SCREENSORTDIRECTION"].ToString());

        DataTable dt = BLL_Infra_ShipSettings.Get_Screen_Search(txtSearchScreenName.Text, ModuleID, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref rowcount);

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

                BindScreenList(Convert.ToInt32(ViewState["ScreenID"].ToString()));

                gvScreens.SelectedIndex = 0;
            }
            SetScreenRowSelection();
        }
        else
        {
            gvScreens.DataSource = dt;
            gvScreens.DataBind();
            ViewState["ScreenID"] = null;
        }


        UpdScreenGrid.Update();

    }

    private void BindProjectList(int Project_ID)
    {

        DataTable dt = BLL_Infra_ShipSettings.Get_Projects(Project_ID);

        if (dt.Rows.Count > 0)
        {

            txtProject.Text = dt.Rows[0]["Project_Name"].ToString();
            lblProjectCreatedBy.Text = dt.Rows[0]["CREATEDBY"].ToString();
            lblProjectModifiedBy.Text = dt.Rows[0]["MODIFIEDBY"].ToString();
            lblProjectDeletedBy.Text = dt.Rows[0]["DELETEDBY"].ToString();
        }

        UpdProjectEntry.Update();

    }

    private void BindModuleList(int Module_ID)
    {

        DataTable dt = BLL_Infra_ShipSettings.Get_Modules(Module_ID);

        if (dt.Rows.Count > 0)
        {
            txtModule.Text = dt.Rows[0]["Module_Name"].ToString();
            lblModuleCreatedBy.Text = dt.Rows[0]["CREATEDBY"].ToString();
            lblModuleModifiedBy.Text = dt.Rows[0]["MODIFIEDBY"].ToString();
            lblModuleDeletedBy.Text = dt.Rows[0]["DELETEDBY"].ToString();
        }

        UpdModuleEntry.Update();

    }

    private void BindScreenList(int Screen_ID)
    {

        DataTable dt = BLL_Infra_ShipSettings.Get_Screens(Screen_ID);

        if (dt.Rows.Count > 0)
        {
            txtScreen.Text = dt.Rows[0]["Screen_Name"].ToString();
            txtClass.Text = dt.Rows[0]["Class_Name"].ToString();
            txtAssembly.Text = dt.Rows[0]["Assembly_Name"].ToString();
            txtImagePath.Text = dt.Rows[0]["Image_Path"].ToString();
            ddlScreenType.SelectedValue = dt.Rows[0]["Screen_Type"].ToString();
            lblScreenCreatedBy.Text = dt.Rows[0]["CREATEDBY"].ToString();
            lblScreenModifiedby.Text = dt.Rows[0]["MODIFIEDBY"].ToString();
            lblScreenDeletedby.Text = dt.Rows[0]["DELETEDBY"].ToString();
        }
        UpdScreenEntry.Update();

    }

    private void ClearProjectFields()
    {
        txtProject.Text = "";
        lblProjectCreatedBy.Text = "";
        lblProjectModifiedBy.Text = "";
        lblProjectDeletedBy.Text = "";

        ViewState["ProjectID"] = null;
    }

    private void ClearModuleFields()
    {
        txtModule.Text = "";
        lblModuleCreatedBy.Text = "";
        lblModuleModifiedBy.Text = "";
        lblModuleDeletedBy.Text = "";
        ViewState["ModuleID"] = null;
    }

    private void ClearScreenFields()
    {
        txtScreen.Text = "";
        txtAssembly.Text = "";
        txtClass.Text = "";
        txtImagePath.Text = "";
        ddlScreenType.SelectedValue = "0";
        lblScreenCreatedBy.Text = "";
        lblScreenModifiedby.Text = "";
        lblScreenDeletedby.Text = "";

        ViewState["ScreenID"] = null;
    }

    private void BindEmptyModule()
    {
        DataTable dt = BLL_Infra_ShipSettings.Get_Module_Search(null, null, null, null);
        gvModule.DataSource = dt;
        gvModule.DataBind();
    }

    private void BindEmptyScreen()
    {

        int rowcount = 0;
        DataTable dt = BLL_Infra_ShipSettings.Get_Screen_Search(null, null, null, null, null, null, ref rowcount);

        gvScreens.DataSource = dt;
        gvScreens.DataBind();
        ucCustomPagerItems.Visible = false;
        ClearModuleFields();
        ClearScreenFields();
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

    private void SetModuleRowSelection()
    {
        gvModule.SelectedIndex = -1;
        for (int i = 0; i < gvModule.Rows.Count; i++)
        {
            if (gvModule.DataKeys[i].Value.ToString().Equals(ViewState["ModuleID"].ToString()))
            {
                gvModule.SelectedIndex = i;
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

    protected void ImgProjectDelete_Click(object sender, CommandEventArgs e)
    {
        int retval = BLL_Infra_ShipSettings.Delete_Projects(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Convert.ToInt32(Session["userid"].ToString())));
        BindProjects();


        BindEmptyModule();
        BindEmptyScreen();

        ClearProjectFields();
        ClearModuleFields();
        ClearScreenFields();

        UpdProjectGrid.Update();
        UpdProjectEntry.Update();

        UpdModuleGrid.Update();
        UpdModuleEntry.Update();

        UpdScreenGrid.Update();
        UpdScreenEntry.Update();

    }

    protected void ImgModuleDelete_Click(object sender, CommandEventArgs e)
    {
        int retval = BLL_Infra_ShipSettings.Delete_Modules(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Convert.ToInt32(Session["userid"].ToString())));
        BindModules();


        BindEmptyScreen();

        ClearModuleFields();
        ClearScreenFields();

        UpdModuleGrid.Update();
        UpdModuleEntry.Update();

        UpdScreenGrid.Update();
        UpdScreenEntry.Update();


    }

    protected void ImgScreenDelete_Click(object sender, CommandEventArgs e)
    {
        int retval = BLL_Infra_ShipSettings.Delete_Screen(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Convert.ToInt32(Session["userid"].ToString())));
        BindScreen();

        ClearScreenFields();

        UpdScreenGrid.Update();
        UpdScreenEntry.Update();
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
        ViewState["ScreenID"] = null;

        BindProjects();
        BindModules();
        BindScreen();

        ClearModuleFields();
        ClearScreenFields();
        this.SetFocus("txtModule");

        UpdProjectGrid.Update();
        UpdProjectEntry.Update();

        UpdModuleGrid.Update();
        UpdModuleEntry.Update();

        UpdScreenGrid.Update();
        UpdScreenEntry.Update();

    }

    protected void gvModule_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {
        ucCustomPagerItems.isCountRecord = 1;
        gvModule.SelectedIndex = se.NewSelectedIndex;

        ViewState["ModuleID"] = ((Label)gvModule.Rows[se.NewSelectedIndex].FindControl("lblModuleID")).Text;
        ViewState["ScreenID"] = null;

        BindModuleList(Convert.ToInt32(ViewState["ModuleID"].ToString()));

        BindModules();
        BindScreen();

        ClearScreenFields();

        this.SetFocus("txtScreen");

        UpdScreenGrid.Update();
        UpdScreenEntry.Update();

        UpdModuleEntry.Update();

    }

    protected void gvScreens_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {

        gvScreens.SelectedIndex = se.NewSelectedIndex;
        ViewState["ScreenID"] = ((Label)gvScreens.Rows[se.NewSelectedIndex].FindControl("lblScreenID")).Text;

        BindScreenList(Convert.ToInt32(ViewState["ScreenID"].ToString()));

        BindScreen();
        UpdScreenEntry.Update();

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

    protected void gvModule_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["MODULESORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["MODULESORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["MODULESORTDIRECTION"] == null || ViewState["MODULESORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowDown.png";
                    else
                        img.Src = "~/purchase/Image/arrowUp.png";

                    img.Visible = true;
                }
            }
        }
  
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

    protected void imgProjectSearch_Click(object sender, ImageClickEventArgs e)
    {
        BindProjects();

        BindEmptyModule();
        BindEmptyScreen();

        ClearProjectFields();
        ClearModuleFields();
        ClearScreenFields();

        UpdProjectGrid.Update();
        UpdProjectEntry.Update();

        UpdModuleGrid.Update();
        UpdModuleEntry.Update();

        UpdScreenGrid.Update();
        UpdScreenEntry.Update();
    }

    protected void imgModuleSearch_Click(object sender, ImageClickEventArgs e)
    {
        BindModules();
        ClearModuleFields();

        BindEmptyScreen();
        ClearScreenFields();

        UpdModuleGrid.Update();
        UpdModuleEntry.Update();

        UpdScreenGrid.Update();
        UpdScreenEntry.Update();

    }

    protected void imgScreenSearch_Click(object sender, ImageClickEventArgs e)
    {
        BindScreen();
        ClearScreenFields();

        UpdScreenGrid.Update();
        UpdScreenEntry.Update();
    }

    protected void lnbHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/crew/default.aspx");
    }

    protected void btnProjectAddNew_Click(object sender, EventArgs e)
    {

        ClearProjectFields();
        ViewState["ProjectID"] = null;
    }

    protected void btnProjectSave_Click(object sender, EventArgs e)
    {
        int retval = BLL_Infra_ShipSettings.Ins_Upd_Project(UDFLib.ConvertIntegerToNull(ViewState["ProjectID"])
            , txtProject.Text, Convert.ToInt32(Session["USERID"].ToString()));

        BindProjects();
        UpdProjectGrid.Update();

    }

    protected void btnModuleAddNew_Click(object sender, EventArgs e)
    {
        ClearModuleFields();
        ViewState["ModuleID"] = null;

    }

    protected void btnModuleSave_Click(object sender, EventArgs e)
    {
        int retval = BLL_Infra_ShipSettings.Ins_Upd_Modules(UDFLib.ConvertIntegerToNull(ViewState["ModuleID"])
            , Convert.ToInt32(ViewState["ProjectID"].ToString()), txtModule.Text, Convert.ToInt32(Session["USERID"].ToString()));


        BindModules();
        UpdModuleGrid.Update();

    }

    protected void btnScreenAddNew_Click(object sender, EventArgs e)
    {
        ClearScreenFields();
        ViewState["ScreenID"] = null;
    }

    protected void btnScreenSave_Click(object sender, EventArgs e)
    {
        int retval = BLL_Infra_ShipSettings.Ins_Upd_Screens(UDFLib.ConvertIntegerToNull(ViewState["ScreenID"])
            , Convert.ToInt32(ddlScreenType.SelectedValue), Convert.ToInt32(ViewState["ModuleID"].ToString())
            , txtScreen.Text, txtClass.Text, txtAssembly.Text, txtImagePath.Text, Convert.ToInt32(Session["USERID"].ToString()));

        BindScreen();
        UpdScreenGrid.Update();

    }

}