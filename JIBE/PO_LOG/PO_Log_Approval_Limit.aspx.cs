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

public partial class PO_LOG_PO_Log_Approval_Limit : System.Web.UI.Page
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

            Load_ApprovalGroup();
            //Load_UserList();

            BindUserApproval();
        }
        //objChangeReqstMerge.AddMergedColumns(new int[] { 2, 3 }, "Approvel Limit", "HeaderStyle-css");
        //objChangeReqstMerge.AddMergedColumns(new int[] { 4, 5 }, "Approver Type", "HeaderStyle-css");

    }



    public void Load_ApprovalGroup()
    {
        int Type = 0;
        DataSet ds2 = BLL_POLOG_Register.POLOG_Get_AccountClassification(UDFLib.ConvertIntegerToNull(GetSessionUserID()), null);


        DDLGroupFilter.DataSource = ds2.Tables[1];
        DDLGroupFilter.DataTextField = "Approval_Group_Name";
        DDLGroupFilter.DataValueField = "Approval_Group_Code";
        DDLGroupFilter.DataBind();
        DDLGroupFilter.Items.Insert(0, new ListItem("-ALL-", "0"));
    }


    public void Load_UserList()
    {

        DataTable dt = objBLLUser.Get_UserList_ApprovalLimit();

        //DDLUserFilter.DataSource = dt;
        //DDLUserFilter.DataTextField = "UserName";
        //DDLUserFilter.DataValueField = "UserID";
        //DDLUserFilter.DataBind();
        //DDLUserFilter.Items.Insert(0, new ListItem("-ALL-", "0"));

        //DDLUser.DataSource = dt;
        //DDLUser.DataTextField = "UserName";
        //DDLUser.DataValueField = "UserID";
        //DDLUser.DataBind();
        //DDLUser.Items.Insert(0, new ListItem("-Select-", "0"));

    }
    public void BindUserApproval()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_POLOG_Lib.POLOG_Get_Approval_Limit_Search(null, UDFLib.ConvertStringToNull(DDLGroupFilter.SelectedValue)
            , 0, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvUserType.DataSource = dt;
            gvUserType.DataBind();
        }
        else
        {
            gvUserType.DataSource = dt;
            gvUserType.DataBind();
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
    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_DDLGroup");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Approval Limit";
       


        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }
    
   

  

    protected void onDelete(object source, CommandEventArgs e)
    {
        try
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            int ID = 0;
            string Limit_ID = UDFLib.ConvertStringToNull(arg[0]);
            //decimal? Approval_Limit = UDFLib.ConvertDecimalToNull(arg[1]);
            int retval = BLL_POLOG_Lib.POLOG_Del_Approval_Limit(Limit_ID, Convert.ToInt32(Session["USERID"]));
            BindUserApproval();
        }
        catch { }
        {
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindUserApproval();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        //txtfilter.Text = "";
       
        DDLGroupFilter.SelectedValue = "0";
        //DDLUserFilter.SelectedValue = "0";


        BindUserApproval();
    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_POLOG_Lib.POLOG_Get_Approval_Limit_Search(null, UDFLib.ConvertStringToNull(DDLGroupFilter.SelectedValue)
           , 0, sortbycoloumn, sortdirection
            , null, null, ref  rowcount);

        string[] HeaderCaptions = { "Group Name", "Min Approval Limit", "Max Approval Limit","Advance Approver" };
        string[] DataColumnsName = { "Group_Name", "Min_Approval_Limit", "MAX_APPROVAL_LIMIT", "Advance_Approver" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "ApprovalLimit", "Approval Limit", "");

    }


    protected void gvUserType_RowDataBound(object sender, GridViewRowEventArgs e)
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
            MergeGridviewHeader.SetProperty(objChangeReqstMerge);

            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
        }

    }
    protected void gvUserType_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindUserApproval();
    }
   
  
}