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
using SMS.Properties;


public partial class ExchangeRate : System.Web.UI.Page
{


    BLL_Infra_VesselFlag objBLL = new BLL_Infra_VesselFlag();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    BLL_Infra_Company objBLLComp = new BLL_Infra_Company();
    BLL_Infra_Currency objBLLCurr = new BLL_Infra_Currency();



    BLL_Infra_Exch_Rate objBLLExchRate = new BLL_Infra_Exch_Rate();


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
            Load_Currency();

          


            BindExchRate();
        }

    }

    public void Load_Currency()
    {
      
        DataTable dt = objBLLCurr.Get_CurrencyList();

        ddlCurrency.DataSource = dt;
        ddlCurrency.DataTextField = "Currency_Code";
        ddlCurrency.DataValueField = "Currency_Code";
        ddlCurrency.DataBind();
        ddlCurrency.Items.Insert(0, new ListItem("-Select-", "0"));

    }

    public void BindExchRate()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        int? CurrentHistory = null;
        if (rbtCurrent.Checked == true)
        {
            CurrentHistory = 1;
        }

        DataTable dt = objBLLExchRate.SearchExchangeRate(txtfilter.Text != "" ? txtfilter.Text : null,CurrentHistory,sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvExchRate.DataSource = dt;
            gvExchRate.DataBind();
        }
        else
        {
            gvExchRate.DataSource = dt;
            gvExchRate.DataBind();
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
        this.SetFocus("ctl00_MainContent_ddlCurrency");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Exchange Rate";

        txtBaseCurr.Text = "USD";
        ddlCurrency.Enabled = true;
        dtpCurrentDate.Enabled = true;
        ClearField();
        caldtpCurrentDate.SelectedDate = DateTime.Now;
        dtpCurrentDate.Text = DateTime.Now.ToString("dd MMM yyyy");
        caldtpCurrentDate.Enabled = false;
        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }

    protected void ClearField()
    {
        ddlCurrency.SelectedValue = "0";
        txtExchangeRate.Text = "";
        dtpCurrentDate.Text = "";


    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
       

        if (HiddenFlag.Value == "Add")
        {
            //int responseid = objBLLExchRate.Insert_ExchangeRate(ddlCurrency.SelectedValue.ToString(), UDFLib.ConvertDecimalToNull(txtExchangeRate.Text)
            //    , UDFLib.ConvertDateToNull(dtpCurrentDate.Text), Convert.ToInt32(Session["USERID"]),txtBaseCurr.Text);
            if (!check(ddlCurrency.SelectedValue.ToString(), UDFLib.ConvertDateToNull(dtpCurrentDate.Text)))
            {
                string js = "alert('Todays Currency Code AllReady Exist!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js, true);
                string Countrymodal = String.Format("showModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Countrymodal", Countrymodal, true);
                ddlCurrency.Focus(); 
             
                return;
            }    
            int responseid = objBLLExchRate.Insert_ExchangeRate(ddlCurrency.SelectedValue.ToString(), UDFLib.ConvertDecimalToNull(txtExchangeRate.Text)
                , UDFLib.ConvertDateToNull(dtpCurrentDate.Text));

        }
        else
        {
            //int responseid = objBLLExchRate.Edit_ExchangeRate(ddlCurrency.SelectedValue.ToString(), UDFLib.ConvertDecimalToNull(txtExchangeRate.Text)
            //    , UDFLib.ConvertDateToNull(dtpCurrentDate.Text), Convert.ToInt32(Session["USERID"]));

            int responseid = objBLLExchRate.Edit_ExchangeRate(ddlCurrency.SelectedValue.ToString(), UDFLib.ConvertDecimalToNull(txtExchangeRate.Text)
                , UDFLib.ConvertDateToNull(dtpCurrentDate.Text));

        }

        BindExchRate();
        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Exchange Rate";

        ddlCurrency.Enabled = false;
        dtpCurrentDate.Enabled = false;
        string[] arg = e.CommandArgument.ToString().Split(',');
        string Currency = UDFLib.ConvertStringToNull(arg[0]);       
        DateTime? Curr_Date = UDFLib.ConvertDateToNull(arg[1]);


        DataTable dt = new DataTable();
        dt = objBLLExchRate.Get_ExchangeRate_List(Currency, Curr_Date);

        txtCurrency.Text = dt.Rows[0]["CURR_CODE"].ToString();
        //ddlCurrency.SelectedValue = dt.Rows[0]["Currency_Code"].ToString() != "" ? dt.Rows[0]["Currency_Code"].ToString() : "0";
        ddlCurrency.SelectedValue = dt.Rows[0]["CURR_CODE"].ToString();
        txtExchangeRate.Text = dt.Rows[0]["EXCH_RATE"].ToString();
        dtpCurrentDate.Text = dt.Rows[0]["DATE"].ToString();
        //txtBaseCurr.Text = dt.Rows[0]["BASE_CURRECY"].ToString();

 
        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        string[] arg = e.CommandArgument.ToString().Split(',');
        string Currency = UDFLib.ConvertStringToNull(arg[0]);      
        DateTime? Curr_Date = UDFLib.ConvertDateToNull(arg[1]);

        int retval = objBLLExchRate.Delete_ExchangeRate(Currency, Curr_Date);
        //int retval = objBLLExchRate.Delete_ExchangeRate(Currency,Curr_Date, Convert.ToInt32(Session["USERID"]));

        BindExchRate();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindExchRate();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
       
        BindExchRate();

    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        int? CurrentHistory = null;
        if (rbtCurrent.Checked == true)
        {
            CurrentHistory = 1;
        }

        DataTable dt = objBLLExchRate.SearchExchangeRate(txtfilter.Text != "" ? txtfilter.Text : null, CurrentHistory, sortbycoloumn, sortdirection
             , null, null, ref  rowcount);


        string[] HeaderCaptions = { "Flag Name", "Exchange Rate", "Date" };
        string[] DataColumnsName = { "CURR_CODE", "EXCH_RATE", "DATE" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "ExchangeRate", "Exchange Rate", "");

    }

    protected void gvExchRate_RowDataBound(object sender, GridViewRowEventArgs e)
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

            

            Image im = e.Row.FindControl("imgRecordInfo") as Image;
            if (im != null)
            {
                string str = "Curr_code='" + DataBinder.Eval(e.Row.DataItem, "Curr_code").ToString() + "' AND Date='" + DataBinder.Eval(e.Row.DataItem, "Date").ToString() + "'";
                string str1 = "Acc_Exch_Rate";
                //'" + DataBinder.Eval(e.Row.DataItem, "Curr_code").ToString() + "'
                im.Attributes.Add("onclick", "'<%# " + "Get_Record_Information(\"" + str1 + "\"," + "\"" + str + "\"" + ",event,this)" + " %>'");//AND Date='"+DataBinder.Eval(e.Row.DataItem,"Date").ToString()+"'
                
            }

        }

    }

    protected void gvExchRate_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindExchRate();
    }
    public bool check(string s, DateTime? a)
    {
        DataTable dt = objBLLExchRate.Check_Exchange_List(s, a);
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
