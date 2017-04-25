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

public partial class UserType : System.Web.UI.Page
{
    BLL_Infra_UserType objBLL = new BLL_Infra_UserType();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
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
            
            BindUserType();
        }

    }

    public void BindUserType()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
      
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.SearchUserType(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
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
        this.SetFocus("ctl00_MainContent_txtUserType");
        HiddenFlag.Value = "Add";

        OperationMode = "Add User Type";

        txtUserType.Text = "";
       

        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        string js = "";
        if (HiddenFlag.Value == "Add")
        {
            int responseid = objBLL.InsertUserType(txtUserType.Text.Trim(),Convert.ToInt32(Session["USERID"]));
            if (responseid == -1)
                js = String.Format("alert('User Type already exist');showModal('divadd',false);");
        }
        else
        {
            int responseid = objBLL.EditUserType(Convert.ToInt32(txtUserTypeID.Text),txtUserType.Text,Convert.ToInt32(Session["USERID"]));
            if (responseid == -1)
                js = String.Format("alert('User Type already exist');showModal('divadd',false);");
        }
        if (js == "")
        {
            BindUserType();
            js = String.Format("hideModal('divadd')");
           
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "js", js, true);
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit UserType";

        DataTable dt = new DataTable();
        dt = objBLL.Get_UserTypeList(Convert.ToInt32(e.CommandArgument.ToString()));
        dt.DefaultView.RowFilter = "ID= '" + e.CommandArgument.ToString() + "'";

        txtUserTypeID.Text = dt.DefaultView[0]["ID"].ToString();
        txtUserType.Text = dt.DefaultView[0]["USER_TYPE"].ToString();



        string InfoDiv = "Get_Record_Information_Details('LIB_USER_TYPE','ID=" + txtUserTypeID.Text + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);
        
        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DeleteUserType(Convert.ToInt32(e.CommandArgument.ToString()),Convert.ToInt32(Session["USERID"]));
        BindUserType();


    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindUserType();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";

       
       BindUserType();

    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


         DataTable dt = objBLL.SearchUserType(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        string[] HeaderCaptions = {"ID", "User Type"};
        string[] DataColumnsName = { "ID", "User_Type" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "UserType", "UserType", "");

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

         BindUserType();
    }
}