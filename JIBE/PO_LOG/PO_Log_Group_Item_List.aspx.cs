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


public partial class PO_LOG_PO_Log_Group_Item_List : System.Web.UI.Page
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

            BindApprovalGroup();
        }

    }



    public void BindApprovalGroup()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_POLOG_Lib.Get_Approval_Group_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvApprovalGroup.DataSource = dt;
            gvApprovalGroup.DataBind();
        }
        else
        {
            gvApprovalGroup.DataSource = dt;
            gvApprovalGroup.DataBind();
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



    //protected void ImgAdd_Click(object sender, EventArgs e)
    //{
    //    this.SetFocus("ctl00_MainContent_txtApprovalGroup");
    //    HiddenFlag.Value = "Add";

    //    OperationMode = "Add Approval Group";
    //    txtApprovalGroup.Text = "";

    //    string AddUserTypemodal = String.Format("showModal('divadd',false);");
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    //}

    //protected void btnsave_Click(object sender, EventArgs e)
    //{


    //    if (HiddenFlag.Value == "Add")
    //    {
    //        int responseid = objBLLAppGroup.INS_Approval_Group(txtApprovalGroup.Text, Convert.ToInt32(Session["USERID"]));

    //    }
    //    else
    //    {
    //        int responseid = objBLLAppGroup.Upd_Approval_Group(Convert.ToInt32(txtGroupID.Text), txtApprovalGroup.Text, Convert.ToInt32(Session["USERID"]));

    //    }

    //    BindApprovalGroup();
    //    string hidemodal = String.Format("hideModal('divadd')");
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    //}

    //protected void onUpdate(object source, CommandEventArgs e)
    //{

    //    HiddenFlag.Value = "Edit";
    //    OperationMode = "Edit Approval Group";

    //    DataTable dt = new DataTable();
    //    dt = objBLLAppGroup.Get_Approval_Group_List_DL(Convert.ToInt32(e.CommandArgument.ToString()));
    //    dt.DefaultView.RowFilter = "Group_ID= '" + e.CommandArgument.ToString() + "'";

    //    txtGroupID.Text = dt.DefaultView[0]["Group_ID"].ToString();
    //    txtApprovalGroup.Text = dt.DefaultView[0]["Group_Name"].ToString();

    //    string AddUserTypemodal = String.Format("showModal('divadd',false);");
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);

    //}

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = BLL_POLOG_Lib.Del_Approval_Group(UDFLib.ConvertStringToNull(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindApprovalGroup();


    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindApprovalGroup();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        //txtApprovalGroup.Text = "";

        BindApprovalGroup();
    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_POLOG_Lib.Get_Approval_Group_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
          , null, null, ref  rowcount);


        string[] HeaderCaptions = { "Group Name","PO Type" };
        string[] DataColumnsName = { "Group_Name", "VARIABLE_NAME" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "ApprovalGroup", "Approval Group", "");

    }


    protected void gvApprovalGroup_RowDataBound(object sender, GridViewRowEventArgs e)
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


    protected void gvApprovalGroup_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindApprovalGroup();
    }
}