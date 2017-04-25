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
using SMS.Business.eForms;
using System.Web.UI.HtmlControls;

public partial class eForms_Libraries_LashingGearInventory : System.Web.UI.Page
{
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_Infra_LashingGearInventoryLib objBLLOps = new BLL_Infra_LashingGearInventoryLib();
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
            BindLashingGearInventory();
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


    public void BindLashingGearInventory()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLLOps.Lashing_Gear_Inventory_Search(txtItemDescription.Text != "" ? txtItemDescription.Text : null, txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlVessel_Filter.SelectedValue)
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
        OperationMode = "Add Lashing Gear Inventory";

        ClearField();

        string AddHoldTank = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddHoldTank", AddHoldTank, true);
    }

    protected void ClearField()
    {
        txtLashingGearInventoryID.Text = "";
        txtcoargoMno.Text = "";
        txtItemDes.Text = "";
        txtitemModel.Text = "";
        //txtHoldTankName.Text = "";
        //ddlStructureType.SelectedValue = "0";
        DDLVessel.SelectedValue = "0";
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (HiddenFlag.Value == "Add")
        {
            int retval = objBLLOps.Insert_Lashing_Gear_Inventory(txtItemDes.Text, txtitemModel.Text, txtcoargoMno.Text, UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue), Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            int retval = objBLLOps.Edit_Lashing_Gear_Inventory(Convert.ToInt32(txtLashingGearInventoryID.Text.Trim()), Convert.ToString(txtItemDes.Text), Convert.ToString(txtitemModel.Text), Convert.ToString(txtcoargoMno.Text), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue), Convert.ToInt32(Session["USERID"]));
        }

        BindLashingGearInventory();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Lashing Gear Inventory";


        string[] cmdargs = e.CommandArgument.ToString().Split(',');

        string ID = cmdargs[0].ToString();
        string Vessel_ID = cmdargs[1].ToString();

        DataTable dt = new DataTable();
        dt = objBLLOps.Get_Lashing_Gear_Inventory(Convert.ToInt32(ID), Convert.ToInt32(Vessel_ID));


        txtLashingGearInventoryID.Text = dt.Rows[0]["ID"].ToString();
        txtItemDes.Text = dt.Rows[0]["Item_Description"].ToString();
        txtitemModel.Text = dt.Rows[0]["Model_No"].ToString();
        txtcoargoMno.Text = dt.Rows[0]["Carg_Securing_Mannual_No"].ToString();
        DDLVessel.SelectedValue = dt.Rows[0]["Vessel_ID"].ToString();

        string HoldTankEdit = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RankCategory", HoldTankEdit, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        string[] cmdargs = e.CommandArgument.ToString().Split(',');

        string ID = cmdargs[0].ToString();
        string Vessel_ID = cmdargs[1].ToString();

        int retval = objBLLOps.Delete_Lashing_Gear_Inventory(Convert.ToInt32(ID), Convert.ToInt32(Vessel_ID), Convert.ToInt32(Session["USERID"]));
        BindLashingGearInventory();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindLashingGearInventory();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        txtItemDescription.Text = "";
        txtfilter.Text = "";
        ddlVessel_Filter.SelectedValue = "0";
        //ddlStructureType_Filter.SelectedValue = "0";
        BindLashingGearInventory();
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;
        //string Carg_Securing_Mannual_No;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLLOps.Lashing_Gear_Inventory_Search(txtItemDescription.Text != "" ? txtItemDescription.Text : null, txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertIntegerToNull(ddlVessel_Filter.SelectedValue)
             , sortbycoloumn, sortdirection
              , null, null, ref  rowcount);


        string[] HeaderCaptions = { "Vessel", "Item Description", "Item Model", "Cargo Manual Non" };
        string[] DataColumnsName = { "VESSEL_NAME", "Item_Description", "Model_No", "Carg_Securing_Mannual_No" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Lashing Gear Inventory", "eForm Lashing Gear Inventory", "");

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

        BindLashingGearInventory();
    }
}