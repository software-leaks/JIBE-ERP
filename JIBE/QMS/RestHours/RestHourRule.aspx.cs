using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.QMSDB;

public partial class RestHourRule : System.Web.UI.Page
{
   
    
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
            BindGrid();
        }

    }



    public void BindGrid()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        //int? isfavorite = null; if (ddlFavorite.SelectedValue != "2") isfavorite = Convert.ToInt32(ddlFavorite.SelectedValue.ToString());
        //int? Rulecode = null; if (ddlSearchRule.SelectedValue != "0") Rulecode = Convert.ToInt32(ddlSearchRule.SelectedValue.ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataSet ds = BLL_QMS_RestHours.GetRestHoursRulesSearch(null, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (ds.Tables[0].Rows.Count > 0)
        {
            GridViewRHRules.DataSource = ds.Tables[0];
            GridViewRHRules.DataBind();
        }
        else
        {
            GridViewRHRules.DataSource = ds.Tables[0];
            GridViewRHRules.DataBind();
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

   
        if (objUA.Add == 0)ImgAdd.Visible = false;
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
        this.SetFocus("ctl00_MainContent_txtDescription");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Rule";

        txtDescriptionID.Text = "";
        txtDescription.Text = "";
        txtValues.Text = "";
        DDLUnit.SelectedIndex = 0;

        string AddRulemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddRulemodal", AddRulemodal, true);
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {


        if (HiddenFlag.Value == "Add")
        {
            int responseid = BLL_QMS_RestHours.Insert_RestHours_Rules(txtDescription.Text.Trim(), UDFLib.ConvertIntegerToNull(txtValues.Text),UDFLib.ConvertIntegerToNull(txtPeriod.Text), DDLUnit.SelectedValue ,1, Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            int responseid = BLL_QMS_RestHours.Update_RestHours_Rules(int.Parse(txtDescriptionID.Text),txtDescription.Text.Trim(), UDFLib.ConvertIntegerToNull(txtValues.Text), UDFLib.ConvertIntegerToNull(txtPeriod.Text), DDLUnit.SelectedValue, 1, Convert.ToInt32(Session["USERID"]));
        
        }

        BindGrid();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }




    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Rule";

        DataTable dt = new DataTable();       
        //dt.DefaultView.RowFilter = "ID= '" + e.CommandArgument.ToString() + "'";
         dt = BLL_QMS_RestHours.Get_RestHours_Rules_Details(int.Parse(e.CommandArgument.ToString()));

        txtDescriptionID.Text = dt.Rows[0]["ID"].ToString();
        txtDescription.Text = dt.Rows[0]["RULE_DESCRIPTION"].ToString();
        txtValues.Text = dt.Rows[0]["Rule_Value"].ToString();
        txtPeriod.Text = dt.Rows[0]["Rule_Period"].ToString();
        DDLUnit.SelectedValue = dt.Rows[0]["Rule_Unit"].ToString();



        string InfoDiv = "Get_Record_Information_Details('LIB_Rule','ID=" + txtDescriptionID.Text + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);



        string Rulemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Rulemodal", Rulemodal, true);
    }


    protected void onDelete(object source, CommandEventArgs e)
    {
        int retval = BLL_QMS_RestHours.Delete_RestHours_Rules(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindGrid();
    
    
    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
       // txtfilter.Text = "";
        BindGrid();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {

        BindGrid();
    }



    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
       
        
        int rowcount = ucCustomPagerItems.isCountRecord;
        //int? isfavorite = null; if (ddlFavorite.SelectedValue != "2") isfavorite = Convert.ToInt32(ddlFavorite.SelectedValue.ToString());
        //int? Rulecode = null; if (ddlSearchRule.SelectedValue != "0") Rulecode = Convert.ToInt32(ddlSearchRule.SelectedValue.ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


         DataSet ds = BLL_QMS_RestHours.GetRestHoursRulesSearch(null, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


         string[] HeaderCaptions = { "Description", "Value", "Period" };
         string[] DataColumnsName = { "Rule_Description", "Rule_Value", "Rule_Period" };

        GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "Rest Hour rules", "Resthour rules", "");

    }

    protected void GridViewRHRules_RowDataBound(object sender, GridViewRowEventArgs e)
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


    protected void GridViewRHRules_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindGrid();
    }

}
