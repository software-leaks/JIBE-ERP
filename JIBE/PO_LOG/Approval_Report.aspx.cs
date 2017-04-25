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
using SMS.Business.POLOG;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class PO_LOG_Approval_Report : System.Web.UI.Page
{
    BLL_Infra_UserType objBLL = new BLL_Infra_UserType();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();
    BLL_Infra_Approval_Limit objBLLAppLimit = new BLL_Infra_Approval_Limit();



    UserAccess objUA = new UserAccess();

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        //UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            BindDropDownlist();
            BindAcct();
            BindApproval();
        }
        
    }
    public void BindAcct()
    {
        DataSet ds1 = BLL_POLOG_Register.POLOG_Get_AccountClassification(UDFLib.ConvertIntegerToNull(GetSessionUserID()), ddlPOType.SelectedValue);
        ddlAccClassifictaion.DataSource = ds1.Tables[0];
        ddlAccClassifictaion.DataTextField = "VARIABLE_NAME";
        ddlAccClassifictaion.DataValueField = "VARIABLE_CODE";
        ddlAccClassifictaion.DataBind();
        ddlAccClassifictaion.Items.Insert(0, new ListItem("-All-", "0"));


        //DataSet ds2 = BLL_POLOG_Register.POLOG_Get_AccountClassification(UDFLib.ConvertIntegerToNull(GetSessionUserID()), ddlPOType.SelectedValue);
        ddlGroupName.DataSource = ds1.Tables[1];
        ddlGroupName.DataTextField = "Approval_Group_Name";
        ddlGroupName.DataValueField = "Approval_Group_Code";
        ddlGroupName.DataBind();
        ddlGroupName.Items.Insert(0, new ListItem("-All-", "0"));
    }
    public void BindDropDownlist()
    {
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(GetSessionUserID()), "PO_Type");
            ddlPOType.DataSource = ds.Tables[0];
            ddlPOType.DataTextField = "VARIABLE_NAME";
            ddlPOType.DataValueField = "VARIABLE_CODE";
            ddlPOType.DataBind();
            //ddlPOType.Items.Insert(0, new ListItem("-Select-", "0"));

            //DataSet ds1 = BLL_POLOG_Register.POLOG_Get_AccountClassification(UDFLib.ConvertIntegerToNull(GetSessionUserID()), ddlPOType.SelectedValue);
            //ddlAccClassifictaion.DataSource = ds1.Tables[0];
            //ddlAccClassifictaion.DataTextField = "VARIABLE_NAME";
            //ddlAccClassifictaion.DataValueField = "VARIABLE_CODE";
            //ddlAccClassifictaion.DataBind();
            //ddlAccClassifictaion.Items.Insert(0, new ListItem("-All-", "0"));


            //DataSet ds2 = BLL_POLOG_Register.POLOG_Get_AccountClassification(UDFLib.ConvertIntegerToNull(GetSessionUserID()), ddlPOType.SelectedValue);
            //ddlGroupName.DataSource = ds2.Tables[1];
            //ddlGroupName.DataTextField = "Approval_Group_Name";
            //ddlGroupName.DataValueField = "Approval_Group_Code";
            //ddlGroupName.DataBind();
            //ddlGroupName.Items.Insert(0, new ListItem("-All-", "0"));
        }
        catch { }
            {
            }
    }
    public void BindApproval()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        ViewState["VARIABLE_CODE"] = "";
        ViewState["Group_Code"] = "";
        DataTable dt = BLL_POLOG_Lib.POLOG_Get_Approval_Setting_Report(UDFLib.ConvertStringToNull(ddlPOType.SelectedValue)
            , UDFLib.ConvertStringToNull(ddlAccClassifictaion.SelectedValue),GetSessionUserID(),ddlGroupName.SelectedValue, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvApproval.DataSource = dt;
            gvApproval.DataBind();
        }
        else
        {
            gvApproval.DataSource = dt;
            gvApproval.DataBind();
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

        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            //btnsave.Visible = false;
            if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
   

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindApproval();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ddlAccClassifictaion.SelectedValue = "0";
        BindApproval();
    }
    protected void gvApproval_RowDataBound(object sender, GridViewRowEventArgs e)
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
            //MergeGridviewHeader.SetProperty(objChangeReqstMerge);

            //e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
        }
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblGroup_Name = (Label)e.Row.FindControl("lblApproval_Group_Name");
            Label lblvariable_Code = (Label)e.Row.FindControl("lblvariable_Code");
            Label lblVARIABLE_NAME = (Label)e.Row.FindControl("lblVARIABLE_NAME");
            string Group_Code = DataBinder.Eval(e.Row.DataItem, "Approval_Group_Code").ToString();
            if (Group_Code.ToString() == ViewState["Group_Code"].ToString())
            {
                lblGroup_Name.Visible = false;
            }
            else
            {
                ViewState["Group_Code"] = Group_Code.ToString();
            }
            if (lblvariable_Code.Text.ToString() == ViewState["VARIABLE_CODE"].ToString())
            {
                lblvariable_Code.Visible = false;
                lblVARIABLE_NAME.Visible = false;
            }
            else
            {
                ViewState["VARIABLE_CODE"] = lblvariable_Code.Text;
            }
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
        }

    }
   
    protected void gvApproval_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindApproval();
    }
    protected void ddlPOType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAcct();
    }
}