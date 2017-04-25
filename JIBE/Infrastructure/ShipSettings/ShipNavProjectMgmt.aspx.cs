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


public partial class ShipNavProjectMgmt : System.Web.UI.Page
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
            BindProjectTemplete();
            BindModuleScreen();
        }

    }

    public void BindProjects()
    {
        string sortbycoloumn = (ViewState["PROJECTSORTBYCOLOUMN"] == null) ? null : (ViewState["PROJECTSORTBYCOLOUMN"].ToString());
        int? sortdirection = null;

        if (ViewState["PROJECTSORTDIRECTION"] != null)
            sortdirection = Int32.Parse(ViewState["PROJECTSORTDIRECTION"].ToString());

        DataTable dt = BLL_Infra_ShipSettings.Get_Nav_Project_Search(txtSearchProject.Text, sortbycoloumn, sortdirection);

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

        DataTable dt = BLL_Infra_ShipSettings.Get_Nav_Module_Search(txtSearchModule.Text, Project_ID, sortbycoloumn, sortdirection);

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

    public void BindProjectTemplete()
    {

        DataTable dt = BLL_Infra_ShipSettings.Get_Project_Templete();

        ddlProjectTemplete.DataTextField = "Class_Name";
        ddlProjectTemplete.DataValueField = "Screen_ID";
        ddlProjectTemplete.DataSource = dt;
        ddlProjectTemplete.DataBind();

        ddlProjectTemplete.Items.Insert(0, new ListItem("-SELECT-", "0"));

    }

    public void BindModuleScreen()
    {
        DataTable dt = BLL_Infra_ShipSettings.Get_Module_Screens();

        ddlModuleScreen.DataTextField = "Class_Name";
        ddlModuleScreen.DataValueField = "Screen_ID";
        ddlModuleScreen.DataSource = dt;
        ddlModuleScreen.DataBind();
        ddlModuleScreen.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }

    private void BindProjectList(int Project_ID)
    {

        DataTable dt = BLL_Infra_ShipSettings.Get_Nav_Projects(Project_ID);

        if (dt.Rows.Count > 0)
        {

            txtProject.Text = dt.Rows[0]["Name"].ToString();
            ddlProjectTemplete.SelectedValue = dt.Rows[0]["Screen_ID"].ToString();

            txtProjImagePath.Text = dt.Rows[0]["Image_Path"].ToString();

            lblProjectCreatedBy.Text = dt.Rows[0]["CREATEDBY"].ToString();
            lblProjectModifiedBy.Text = dt.Rows[0]["MODIFIEDBY"].ToString();
            lblProjectDeletedBy.Text = dt.Rows[0]["DELETEDBY"].ToString();
        }

        UpdProjectEntry.Update();

    }

    private void BindModuleList(int Module_ID)
    {

        DataTable dt = BLL_Infra_ShipSettings.Get_Nav_Modules(Module_ID);

        if (dt.Rows.Count > 0)
        {
            txtModule.Text = dt.Rows[0]["Name"].ToString();
            ddlModuleScreen.SelectedValue = dt.Rows[0]["Screen_ID"].ToString();
            txtModuleImagePath.Text = dt.Rows[0]["Image_Path"].ToString();

            lblModuleCreatedBy.Text = dt.Rows[0]["CREATEDBY"].ToString();
            lblModuleModifiedBy.Text = dt.Rows[0]["MODIFIEDBY"].ToString();
            lblModuleDeletedBy.Text = dt.Rows[0]["DELETEDBY"].ToString();
        }

        UpdModuleEntry.Update();

    }

    private void ClearProjectFields()
    {
        txtProject.Text = "";
        ddlProjectTemplete.SelectedValue = "0";
        txtProjImagePath.Text = "";

        lblProjectCreatedBy.Text = "";
        lblProjectModifiedBy.Text = "";
        lblProjectDeletedBy.Text = "";

        ViewState["ProjectID"] = null;
    }

    private void ClearModuleFields()
    {
        txtModule.Text = "";
        ddlModuleScreen.SelectedValue = "0";
        txtModuleImagePath.Text = "";

        lblModuleCreatedBy.Text = "";
        lblModuleModifiedBy.Text = "";
        lblModuleDeletedBy.Text = "";
        ViewState["ModuleID"] = null;
    }

    private void BindEmptyModule()
    {
        DataTable dt = BLL_Infra_ShipSettings.Get_Module_Search(null, null, null, null);
        gvModule.DataSource = dt;
        gvModule.DataBind();
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

    protected void ImgProjectDelete_Click(object sender, CommandEventArgs e)
    {
        int retval = BLL_Infra_ShipSettings.Delete_Nav_Projects(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Convert.ToInt32(Session["userid"].ToString())));
        BindProjects();
        BindEmptyModule();

        ClearProjectFields();
        ClearModuleFields();

        UpdProjectGrid.Update();
        UpdProjectEntry.Update();

        UpdModuleGrid.Update();
        UpdModuleEntry.Update();
    }

    protected void ImgModuleDelete_Click(object sender, CommandEventArgs e)
    {
        int retval = BLL_Infra_ShipSettings.Delete_Nav_Modules(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Convert.ToInt32(Session["userid"].ToString())));
        BindModules();

        ClearModuleFields();

        UpdModuleGrid.Update();
        UpdModuleEntry.Update();

    }


    protected void ImgDefaultModule_Click(object sender, CommandEventArgs e)
    {

        int retval = BLL_Infra_ShipSettings.Default_Nav_Module(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Convert.ToInt32(Session["userid"].ToString())));

        BindModules();

        ClearModuleFields();

        UpdModuleGrid.Update();
        UpdModuleEntry.Update();

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


        gvProject.SelectedIndex = se.NewSelectedIndex;

        ViewState["ProjectID"] = ((Label)gvProject.Rows[se.NewSelectedIndex].FindControl("lblProjectID")).Text;

        ViewState["ModuleID"] = null;
        ViewState["ScreenID"] = null;

        BindProjects();
        BindModules();


        ClearModuleFields();

        this.SetFocus("txtModule");

        UpdProjectGrid.Update();
        UpdProjectEntry.Update();

        UpdModuleGrid.Update();
        UpdModuleEntry.Update();
    }

    protected void gvModule_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {
        gvModule.SelectedIndex = se.NewSelectedIndex;

        ViewState["ModuleID"] = ((Label)gvModule.Rows[se.NewSelectedIndex].FindControl("lblModuleID")).Text;
        ViewState["ScreenID"] = null;

        BindModuleList(Convert.ToInt32(ViewState["ModuleID"].ToString()));

        BindModules();
        UpdModuleEntry.Update();
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


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ImgDefaultModule = (ImageButton)e.Row.FindControl("ImgDefaultModule");

            if (DataBinder.Eval(e.Row.DataItem, "Default_Module").ToString() == "0")
            {
                ImgDefaultModule.ImageUrl = "~/Images/NotDefault.gif";
                ImgDefaultModule.ToolTip = "Make Default";
            }
            else
            {
                ImgDefaultModule.ImageUrl = "~/Images/Default.png";
                ImgDefaultModule.ToolTip = "Default Module";
            }
        }
    }



    protected void imgProjectSearch_Click(object sender, ImageClickEventArgs e)
    {
        BindProjects();

        BindEmptyModule();


        ClearProjectFields();
        ClearModuleFields();

        UpdProjectGrid.Update();
        UpdProjectEntry.Update();

        UpdModuleGrid.Update();
        UpdModuleEntry.Update();

    }

    protected void imgModuleSearch_Click(object sender, ImageClickEventArgs e)
    {
        BindModules();
        ClearModuleFields();



        UpdModuleGrid.Update();
        UpdModuleEntry.Update();


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
        int retval = BLL_Infra_ShipSettings.Ins_Upd_Nav_Project(UDFLib.ConvertIntegerToNull(ViewState["ProjectID"])
             , Convert.ToInt32(ddlProjectTemplete.SelectedValue.ToString()), txtProject.Text
             , txtProjImagePath.Text, Convert.ToInt32(Session["USERID"].ToString()));

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
        int retval = BLL_Infra_ShipSettings.Ins_Upd_Nav_Modules(UDFLib.ConvertIntegerToNull(ViewState["ModuleID"]), Convert.ToInt32(ViewState["ProjectID"].ToString())
            , Convert.ToInt32(ddlModuleScreen.SelectedValue.ToString()), txtModule.Text, txtModuleImagePath.Text, null, Convert.ToInt32(Session["USERID"].ToString()));

        BindModules();
        UpdModuleGrid.Update();

    }

}