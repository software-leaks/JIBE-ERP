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

public partial class Technical_PMS_PMS_AlarmEffect : System.Web.UI.Page
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
            BindEffect();

           
        }

    }

    public void BindEffect()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        int Record_Count = 0;
        //int? isfavorite = null; if (ddlFavorite.SelectedValue != "2") isfavorite = Convert.ToInt32(ddlFavorite.SelectedValue.ToString());
        //int? countrycode = null; if (ddlSearchCountry.SelectedValue != "0") countrycode = Convert.ToInt32(ddlSearchCountry.SelectedValue.ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataSet ds = objBLL.GetAlarmEffects(txtEffect.Text.Trim(), ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize);
        //     , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        //Commented by by anjali DT : 02-05-2016 JIT:9302 **** 
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
        //    GridViewEffect.DataSource = ds.Tables[0];
        //    GridViewEffect.DataBind();
        //}
        //else
        //{
        //    GridViewEffect.DataSource = ds.Tables[0];
        //    GridViewEffect.DataBind();
        //}

        //Commented by by anjali DT : 02-05-2016 JIT:9302 **** 
        GridViewEffect.DataSource = ds.Tables[0];
        GridViewEffect.DataBind();

        if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[1].Rows[0]["Column1"])))
        {
            Record_Count = Convert.ToInt32(ds.Tables[1].Rows[0]["Column1"].ToString());
        }

        ucCustomPagerItems.CountTotalRec = Record_Count.ToString();
        ucCustomPagerItems.BuildPager();
        //Added by anjali DT : 02-05-2016 JIT:9302

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
        this.SetFocus("ctl00_MainContent_txtModalEffect");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Effect";

        txtEffectID.Text = "";
        txtModalEffect.Text = "";

        string AddDeptmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddDeptmodal", AddDeptmodal, true);
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (HiddenFlag.Value == "Add")
        {
            int responseid = objBLL.AddUpdateAlarmEffect(0, txtModalEffect.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            int responseid = objBLL.AddUpdateAlarmEffect(int.Parse(txtEffectID.Text.Trim()), txtModalEffect.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        }

        BindEffect();
        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Effect";

        DataSet ds = new DataSet();

        ds = objBLL.GetAlarmEffects(txtEffect.Text.Trim(), ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize);
        DataTable dt = ds.Tables[0];
        dt.DefaultView.RowFilter = "EffectID= '" + e.CommandArgument.ToString() + "'";
        txtModalEffect.Text = dt.DefaultView[0]["EffectName"].ToString();
        txtEffectID.Text = dt.DefaultView[0]["EffectID"].ToString();
        string Deptmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Deptmodal", Deptmodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DeleteAlarmEffect(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindEffect();


    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindEffect();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtEffect.Text = "";

        BindEffect();

    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;
        DataSet ds = objBLL.GetAlarmEffects(txtEffect.Text.Trim(), ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize);

        string[] HeaderCaptions = { "EffectName" };
        string[] DataColumnsName = { "EffectName" };

        GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "AlarmEffect", "AlarmEffect", "");

    }


    //protected void GridViewEffect_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //if (e.Row.RowType == DataControlRowType.Header)
    //{
    //    if (ViewState["SORTBYCOLOUMN"] != null)
    //    {
    //        HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
    //        if (img != null)
    //        {
    //            if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
    //                img.Src = "~/purchase/Image/arrowUp.png";
    //            else
    //                img.Src = "~/purchase/Image/arrowDown.png";

    //            img.Visible = true;
    //        }
    //    }
    //}

    //if (e.Row.RowType == DataControlRowType.DataRow)
    //{
    //    e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
    //    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
    //}

    //}


    //protected void GridViewcurrency_Sorting(object sender, GridViewSortEventArgs se)
    //{
    //    ViewState["SORTBYCOLOUMN"] = se.SortExpression;

    //    if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
    //        ViewState["SORTDIRECTION"] = 1;
    //    else
    //        ViewState["SORTDIRECTION"] = 0;

    //    BindEffect();
    //}
}