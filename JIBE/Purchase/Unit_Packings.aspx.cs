using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using SMS.Business.PURC;

public partial class Purchase_Unit_Packings : System.Web.UI.Page
{
    public string LocationID;
    public BLL_PURC_Purchase objBLLUnit = new BLL_PURC_Purchase();


    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();

        if (!IsPostBack)
        {
            HiddenFlag.Value = "Add";
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;

            BindrgdUnitPakings();
         

        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx");

        if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnSave.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;

    }


    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Unit";

        DataTable dt = objBLLUnit.UnitPackings_List(Convert.ToInt32(e.CommandArgument.ToString()));

        txtUnitPakingID.Text = dt.DefaultView[0]["ID"].ToString();
        txtMainPack.Text = dt.DefaultView[0]["MAIN_PACK"].ToString();
        txtAbreviation.Text = dt.DefaultView[0]["ABREVIATION"].ToString();

        string UnitPackingmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "UnitPackingmodal", UnitPackingmodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        int retval = objBLLUnit.DeleteUnitPackings(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindrgdUnitPakings();
    }



    public void BindrgdUnitPakings()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLLUnit.UnitPackings_Search(txtSearch.Text != "" ? txtSearch.Text : null, sortbycoloumn, sortdirection
        , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            rgdUnitPakings.DataSource = dt;
            rgdUnitPakings.DataBind();
        }
        else
        {
            rgdUnitPakings.DataSource = dt;
            rgdUnitPakings.DataBind();
        }

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string msg;
        int flag = 0;
        if (HiddenFlag.Value == "Add")
        {
            DataTable dt = new DataTable();
            dt = objBLLUnit.UnitPackings_List(null);
            dt.DefaultView.RowFilter = "MAIN_PACK= '" + txtMainPack.Text.Trim() + "' AND  ABREVIATION='" + txtAbreviation.Text.Trim() + "'";
            if (dt.DefaultView.Count > 0)
            {
                msg = String.Format("alert('Unit already exists!')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                flag = 1;
            }

            if (flag != 1)
            {
                {
                    int retVal = objBLLUnit.InsertUnitPackings(txtMainPack.Text, txtAbreviation.Text, Convert.ToInt32(Session["USERID"]));
                }
            }  
        }
        else
        {
            int retVal = objBLLUnit.EditUnitPackings(Convert.ToInt32(txtUnitPakingID.Text), txtMainPack.Text, txtAbreviation.Text, Convert.ToInt32(Session["USERID"]));
        }

        BindrgdUnitPakings();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }


    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindrgdUnitPakings();

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        BindrgdUnitPakings();
    }

    protected void ClearFields()
    {
        txtUnitPakingID.Text = "";
        txtMainPack.Text = "";
        txtAbreviation.Text = "";
    }


    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtMainPack");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Unit";

        ClearFields();

        string AddLocmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddLocmodal", AddLocmodal, true);

    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLLUnit.UnitPackings_Search(txtSearch.Text != "" ? txtSearch.Text : null, sortbycoloumn, sortdirection
        , null, null, ref  rowcount);


        string[] HeaderCaptions = { "Main Pack", "Abbreviation" };
        string[] DataColumnsName = { "MAIN_PACK", "ABREVIATION"};

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "UnitPacking", "UnitPacking", "");

    }


    protected void rgdUnitPakings_RowDataBound(object sender, GridViewRowEventArgs e)
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




    protected void rgdUnitPakings_Sorting(object sender, GridViewSortEventArgs se)
    {


        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindrgdUnitPakings();
    }



}