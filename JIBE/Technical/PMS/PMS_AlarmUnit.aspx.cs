using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using SMS.Business.Infrastructure;
using SMS.Business.PMS;
using SMS.Properties;

public partial class Technical_PMS_PMS_AlarmUnit : System.Web.UI.Page
{
    BLL_PMS_Job_Status objBLL = new BLL_PMS_Job_Status();

    UserAccess objUA = new UserAccess();

    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();

        if (!IsPostBack)
        {
            //txtCurrencyDesc.Attributes.Add("maxlength", txtCurrencyDesc.MaxLength.ToString());
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            //Load_CountryList();
            BindUnit();
        }

        

    }

    public void BindUnit()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        int Record_Count = 0;
        //int? isfavorite = null; if (ddlFavorite.SelectedValue != "2") isfavorite = Convert.ToInt32(ddlFavorite.SelectedValue.ToString());
        //int? countrycode = null; if (ddlSearchCountry.SelectedValue != "0") countrycode = Convert.ToInt32(ddlSearchCountry.SelectedValue.ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataSet ds = objBLL.GetAlarmUnits(txtUnit.Text.Trim(), ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize);
        //     , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        //Commented by by anjali DT : 02-05-2016 JIT:9296
        //if (ucCustomPagerItems.isCountRecord == 1)
        //{
        //    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["Column1"])))
        //    {
        //        ucCustomPagerItems.CountTotalRec = ds.Tables[1].Rows[0]["Column1"].ToString();
        //        ucCustomPagerItems.BuildPager();
        //    }
        //}


        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    GridViewcurrency.DataSource = ds.Tables[0];
        //    GridViewcurrency.DataBind();
        //}
        //else
        //{
        //    GridViewcurrency.DataSource = ds.Tables[0];
        //    GridViewcurrency.DataBind();
        //}
        //Commented by by anjali DT : 02-05-2016 JIT:9296

        //Added by anjali DT : 02-05-2016 JIT:9296
        GridViewcurrency.DataSource = ds.Tables[0];
        GridViewcurrency.DataBind();

        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["Column1"])))
        {
            Record_Count = Convert.ToInt32(ds.Tables[1].Rows[0]["Column1"].ToString());
        }

        ucCustomPagerItems.CountTotalRec = Record_Count.ToString();
        ucCustomPagerItems.BuildPager();
        //Added by anjali DT : 02-05-2016 JIT:9296
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        //uaEditFlag = true;
        //uaDeleteFlage = true;


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
        this.SetFocus("ctl00_MainContent_txtModalUnit");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Unit";

        txtUnitID.Text = "";
        txtModalUnit.Text = "";

        string AddDeptmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddDeptmodal", AddDeptmodal, true);
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (HiddenFlag.Value == "Add")
        {
            int responseid = objBLL.AddUpdateUnit(0, txtModalUnit.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            int responseid = objBLL.AddUpdateUnit(int.Parse(txtUnitID.Text.Trim()), txtModalUnit.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        }

        BindUnit();
        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Unit";

        DataSet ds = new DataSet();

        ds = objBLL.GetAlarmUnits("", ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize);
        DataTable dt = ds.Tables[0];
        dt.DefaultView.RowFilter = "UnitID= '" + e.CommandArgument.ToString() + "'";
        txtModalUnit.Text = dt.DefaultView[0]["UnitName"].ToString();
        txtUnitID.Text = dt.DefaultView[0]["UnitID"].ToString();
        string Deptmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Deptmodal", Deptmodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DeleteAlarmUnit(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindUnit();


    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindUnit();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtUnit.Text = "";

        BindUnit();

    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        DataSet ds = objBLL.GetAlarmUnits(txtUnit.Text.Trim(), ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize);

        string[] HeaderCaptions = { "UnitName" };
        string[] DataColumnsName = { "UnitName" };

        GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "AlarmUnit", "AlarmUnit", "");

    }


    protected void GridViewcurrency_RowDataBound(object sender, GridViewRowEventArgs e)
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


    protected void GridViewcurrency_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindUnit();
    }
}