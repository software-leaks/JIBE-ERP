using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Properties;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.Operations;
using System.Web.UI.HtmlControls;


public partial class DeckLogBookLibHoldTank : System.Web.UI.Page
{
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_OPS_Admin objBLLOps = new BLL_OPS_Admin();
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

            Load_VesselList();
            BindHoldTank();
        }

    }


    public void Load_VesselList()
    {

        DataTable dt = objBLL.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

        DDLVessel.DataSource = dt;
        DDLVessel.DataTextField = "VESSEL_NAME";
        DDLVessel.DataValueField = "VESSEL_ID";
        DDLVessel.DataBind();
        DDLVessel.Items.Insert(0, new ListItem("-Select-", "0"));
        DDLVessel.SelectedIndex = 0;

        ddlVessel_Filter.DataSource = dt;
        ddlVessel_Filter.DataTextField = "VESSEL_NAME";
        ddlVessel_Filter.DataValueField = "VESSEL_ID";
        ddlVessel_Filter.DataBind();
        ddlVessel_Filter.Items.Insert(0, new ListItem("-ALL-", "0"));
        ddlVessel_Filter.SelectedIndex = 0;
        
    }


    public void BindHoldTank()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLLOps.Hold_Tank_Search(txtfilter.Text != "" ? txtfilter.Text : null,UDFLib.ConvertIntegerToNull(ddlStructureType_Filter.SelectedValue),UDFLib.ConvertIntegerToNull(ddlVessel_Filter.SelectedValue)
            , sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvLoCategory.DataSource = dt;
            gvLoCategory.DataBind();
        }
        else
        {
            gvLoCategory.DataSource = dt;
            gvLoCategory.DataBind();
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
        this.SetFocus("ctl00_MainContent_DDLVessel");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Hold Tank";

        ClearField();

        string AddHoldTank = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddHoldTank", AddHoldTank, true);
    }

    protected void ClearField()
    {
        txtHoldTankID.Text = "";
        txtHoldTankName.Text = "";
        ddlStructureType.SelectedValue = "0";
        DDLVessel.SelectedValue = "0";
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int retval = 0;
        if (HiddenFlag.Value == "Add")
        {
            retval = objBLLOps.Insert_Hold_Tank(txtHoldTankName.Text, UDFLib.ConvertIntegerToNull(ddlStructureType.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue), Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            retval = objBLLOps.Edit_Hold_Tank(Convert.ToInt32(txtHoldTankID.Text.Trim()), txtHoldTankName.Text, UDFLib.ConvertIntegerToNull(ddlStructureType.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue), Convert.ToInt32(Session["USERID"]));
        }
        if (retval > 0)
        {
            BindHoldTank();
        }
        else
        {
            string js = "";
            js = "alert('Vessel with same structure and same name already present. Please enter different name...');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "status", js, true);
        }
        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Hold Tank";


        string[] cmdargs = e.CommandArgument.ToString().Split(',');

        string ID = cmdargs[0].ToString();
        string Vessel_ID= cmdargs[1].ToString();

        DataTable dt = new DataTable();
        dt = objBLLOps.Get_Hold_Tank_List(Convert.ToInt32(ID), Convert.ToInt32(Vessel_ID));


        txtHoldTankID.Text = dt.Rows[0]["ID"].ToString();
        txtHoldTankName.Text = dt.Rows[0]["Hold_Tank_Name"].ToString();
        ddlStructureType.SelectedValue = dt.Rows[0]["Structure_Type"].ToString();
        DDLVessel.SelectedValue = dt.Rows[0]["Vessel_ID"].ToString();

        string HoldTankEdit = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RankCategory", HoldTankEdit, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        string[] cmdargs = e.CommandArgument.ToString().Split(',');

        string ID = cmdargs[0].ToString();
        string Vessel_ID = cmdargs[1].ToString();

        int retval = objBLLOps.Delete_Hold_Tank(Convert.ToInt32(ID), Convert.ToInt32(Vessel_ID), Convert.ToInt32(Session["USERID"]));
        BindHoldTank();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindHoldTank();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        ddlVessel_Filter.SelectedValue = "0";
        ddlStructureType_Filter.SelectedValue = "0";

        BindHoldTank();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLLOps.Hold_Tank_Search(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlStructureType_Filter.SelectedValue), UDFLib.ConvertIntegerToNull(ddlVessel_Filter.SelectedValue)
             , sortbycoloumn, sortdirection
             , null, null, ref  rowcount);


        string[] HeaderCaptions = { "Vessel" ,"Hold/Tank Name" ,"Structure Type" };
        string[] DataColumnsName = { "VESSEL_NAME", "HOLD_TANK_NAME", "STRUCTURE_TYPE_NAME" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "HoldTank", "Hold Tank Name", "");

    }

    protected void gvLoCategory_RowDataBound(object sender, GridViewRowEventArgs e)
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


        //ImageButton ImgUpdate = (ImageButton)e.Row.FindControl("ImgUpdate");


        //if (objUA.Delete == 1)
        //{ 
        
        //}

    }

    protected void gvLoCategory_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindHoldTank();
    }
}