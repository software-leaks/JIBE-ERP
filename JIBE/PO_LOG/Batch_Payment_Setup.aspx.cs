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

public partial class PO_LOG_Batch_Payment_Setup : System.Web.UI.Page
{
    BLL_Infra_UserType objBLL = new BLL_Infra_UserType();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();

    //BLL_POLOG_Lib objBLLAppGroup = new BLL_POLOG_Lib();



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

            BindBatchPaymentSetup();
        }

    }



    public void BindBatchPaymentSetup()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_POLOG_Register.POLOG_Get_Batch_Payment_Setup_Search(txtfilter.Text != "" ? txtfilter.Text : null, GetSessionUserID(), sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvPayment.DataSource = dt;
            gvPayment.DataBind();
        }
        else
        {
            gvPayment.DataSource = dt;
            gvPayment.DataBind();
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



    

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = BLL_POLOG_Register.POLOG_DEL_Batch_Payment_Setup(UDFLib.ConvertStringToNull(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindBatchPaymentSetup();


    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindBatchPaymentSetup();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        //txtApprovalGroup.Text = "";

        BindBatchPaymentSetup();
    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_POLOG_Lib.Get_Approval_Group_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
          , null, null, ref  rowcount);


        string[] HeaderCaptions = { "Group Name", "PO Type" };
        string[] DataColumnsName = { "Group_Name", "VARIABLE_NAME" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "ApprovalGroup", "Approval Group", "");

    }


    protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
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


    protected void gvPayment_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindBatchPaymentSetup();
    }
}