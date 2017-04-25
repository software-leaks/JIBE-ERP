using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using SMS.Properties;
using SMS.Business.Infrastructure;
//using System.DirectoryServices;


public partial class Infrastructure_Libraries_UserList : System.Web.UI.Page
{

    BLL_Infra_Company objCompBLL = new BLL_Infra_Company();
    BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    public int iUserCompanyID;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;

           // Load_CompanyList();

            Load_DepartmentList_filter();
            Load_ManagerList_Filter();
            Load_CountryList();
            Load_UserType();
            Load_UserTypeFilter();
            

            Load_Company_By_User_Type();
            Load_Company_By_User_Type_Filter();

            //ddlUserTypeFilter.SelectedValue = "OFFICE USER";
            BindUserGrid();
            //Load_NetworkUsers();
            //Bind_Custom_Filters();
        }

    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnSaveUserDetails.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;
    }



    protected void Load_Company_By_User_Type()
    {

        int iCompID = 0;
        if (getValueFromSession("UserCompanyID") != "")
        {
            iCompID = int.Parse(getValueFromSession("UserCompanyID"));
        }



        DataTable dt = objCompBLL.Get_Company_By_User_Type(iCompID, ddlUserType.SelectedValue.ToString());

        ddlCompany.Items.Clear();

        if (dt.Rows.Count > 0)
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataTextField = "company_name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        else
        {
            ddlCompany.Items.Insert(0, new ListItem("-Select-", "0"));
        }

    }

    protected void Load_CountryList()
    {
        BLL_Infra_Country objCountry = new BLL_Infra_Country();

        ddlNationality.DataSource = objCountry.Get_CountryList();
        ddlNationality.DataTextField = "COUNTRY";
        ddlNationality.DataValueField = "ID";
        ddlNationality.DataBind();
        ddlNationality.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void Load_Company_By_User_Type_Filter()
    {

        int iCompID = 0;
        if (getValueFromSession("UserCompanyID") != "")
        {
            iCompID = int.Parse(getValueFromSession("UserCompanyID"));
        }

        DataTable dt = objCompBLL.Get_Company_By_User_Type(iCompID, ddlUserTypeFilter.SelectedValue.ToString());

        ddlCompanyFilter.Items.Clear();
        if (dt.Rows.Count > 0)
        {
          
            ddlCompanyFilter.DataTextField = "company_name";
            ddlCompanyFilter.DataValueField = "ID";
            ddlCompanyFilter.DataSource = dt;
            ddlCompanyFilter.DataBind();
            ddlCompanyFilter.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        }
        else
        {
            ddlCompanyFilter.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        }

    }



    private void Bind_Custom_Filters()
    {
        if (!IsPostBack)
        {


            //Department            

            int iCompID = int.Parse(getValueFromSession("UserCompanyID"));
            DataTable dtfilter = objCompBLL.Get_CompanyDepartmentList(iCompID);

            ucf_DDLDepartment.DataSource = dtfilter;
            ucf_DDLDepartment.DataTextField = "VALUE";
            ucf_DDLDepartment.DataValueField = "ID";



            //Company


            ddlCompanyFilter.DataSource = objCompBLL.Get_CompanyListMini(iCompID);
            ddlCompanyFilter.DataTextField = "company_name";
            ddlCompanyFilter.DataValueField = "ID";
            ddlCompanyFilter.DataBind();
            ddlCompanyFilter.Items.Insert(0, new ListItem("-ALL-", "0"));

            //Manager




        }
    }

    protected void Load_DepartmentList()
    {
        DataTable dt = objCompBLL.Get_CompanyDepartmentList(Convert.ToInt32(ddlCompany.SelectedValue));

        ddlDepartment.Items.Clear();
        ddlDepartment.DataSource = dt;
        ddlDepartment.DataTextField = "VALUE";
        ddlDepartment.DataValueField = "ID";
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void Load_DepartmentList_filter()
    {
        int iCompID = int.Parse(getValueFromSession("UserCompanyID"));
        DataTable dtfilter = objCompBLL.Get_CompanyDepartmentList(iCompID);

        ddlDepartmentFilter.Items.Clear();
        ddlDepartmentFilter.DataSource = dtfilter;
        ddlDepartmentFilter.DataTextField = "VALUE";
        ddlDepartmentFilter.DataValueField = "ID";
        ddlDepartmentFilter.DataBind();
        ddlDepartmentFilter.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }

    protected void Load_CompanyList()
    {
        try
        {
            int iCompID = 0;
            if (getValueFromSession("UserCompanyID") != "")
            {
                iCompID = int.Parse(getValueFromSession("UserCompanyID"));
            }

            ddlCompanyFilter.DataSource = objCompBLL.Get_CompanyListMini(iCompID);
            ddlCompanyFilter.DataTextField = "company_name";
            ddlCompanyFilter.DataValueField = "ID";
            ddlCompanyFilter.DataBind();
            ddlCompanyFilter.Items.Insert(0, new ListItem("-ALL-", "0"));


            ddlCompany.DataSource = objCompBLL.Get_CompanyListMini(iCompID);
            ddlCompany.DataTextField = "company_name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem("-Select-", "0"));


        }
        catch
        { }
    }

    protected void Load_FleetList()
    {
        BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

        int iCompID = UDFLib.ConvertToInteger(ddlCompany.SelectedValue);
        ddlFleet.Items.Clear();
        ddlFleet.DataSource = objVessel.GetFleetList_ByID(null, iCompID);

        ddlFleet.DataTextField = "FleetName";
        ddlFleet.DataValueField = "FleetCode";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void Load_ManagerList_Filter()
    {
        int iCompID = UDFLib.ConvertToInteger(ddlCompanyFilter.SelectedValue);
        DataTable dtfilter = objUserBLL.Get_UserList(iCompID);

        ddlManagerFilter.Items.Clear();
        ddlManagerFilter.DataSource = dtfilter;
        ddlManagerFilter.DataTextField = "UserName";
        ddlManagerFilter.DataValueField = "UserID";
        ddlManagerFilter.DataBind();
        ddlManagerFilter.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }

    protected void Load_ManagerList()
    {

        DataTable dt = objUserBLL.Get_UserList(Convert.ToInt32(ddlCompany.SelectedValue));
        ddlManager.Items.Clear();
        ddlManager.DataSource = dt;
        ddlManager.DataTextField = "UserName";
        ddlManager.DataValueField = "UserID";
        ddlManager.DataBind();
        ddlManager.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void Load_UserType()
    {

        DataTable dt = objUserBLL.Get_UserTypeList();

        ddlUserType.DataSource = dt;
        ddlUserType.DataTextField = "user_type";
        ddlUserType.DataValueField = "user_type";
        ddlUserType.DataBind();
        ddlUserType.Items.Insert(0, new ListItem("-Select-", "0"));

    }

    protected void Load_UserTypeFilter()
    {
        DataTable dt = objUserBLL.Get_UserTypeList();
        ddlUserTypeFilter.DataSource = dt;
        ddlUserTypeFilter.DataTextField = "user_type";
        ddlUserTypeFilter.DataValueField = "user_type";
        ddlUserTypeFilter.DataBind();
        ddlUserTypeFilter.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

       
    }




    public void BindUserGrid()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objUserBLL.Get_UserList_Search(txtfilter.Text != "" ? txtfilter.Text : null
            , UDFLib.ConvertIntegerToNull(ddlCompanyFilter.SelectedValue)
            , UDFLib.ConvertIntegerToNull(ddlDepartmentFilter.SelectedValue)
            , UDFLib.ConvertIntegerToNull(ddlManagerFilter.SelectedValue), null, UDFLib.ConvertStringToNull(ddlUserTypeFilter.SelectedValue) , null, sortbycoloumn, sortdirection
          , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            GridViewUsers.DataSource = dt;
            GridViewUsers.DataBind();
        }
        else
        {
            GridViewUsers.DataSource = dt;
            GridViewUsers.DataBind();
        }
    }

    protected void btnSaveUserDetails_Click(object sender, EventArgs e)
    {

        int retval = objUserBLL.Update_UserDetails(Convert.ToInt32(txtUserID.Text), txtFirstName.Text, "", txtLastName.Text, txtEMail.Text
            , UDFLib.ConvertIntegerToNull(ddlDepartment.SelectedValue), UDFLib.ConvertIntegerToNull(ddlManager.SelectedValue)
            , UDFLib.ConvertDecimalToNull(txtApprovalLimit.Text), txtDesignation.Text, txtMobileNo.Text
            , UDFLib.ConvertDateToNull(txtDateOfBirth.Text), UDFLib.ConvertDateToNull(txtDateOfJoining.Text)
            , UDFLib.ConvertDateToNull(txtDateOfProbation.Text)
            , txtPermanentAddress.Text
            , txtPresentAddress.Text, UDFLib.ConvertStringToNull(ddlUserType.SelectedValue)
            , UDFLib.ConvertIntegerToNull(ddlCompany.SelectedValue), UDFLib.ConvertIntegerToNull(ddlFleet.SelectedValue)
            , null
            , null
            ,int.Parse(ddlNationality.SelectedValue));


        BindUserGrid();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void GridViewUsers_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void GridViewUsers_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindUserGrid();

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected string getValueFromSession(string ID)
    {
        string ret = "";
        if (Session[ID] != null)
        {
            ret = Session[ID].ToString();
        }
        else
        {
            Response.Redirect("~/Account/Login.aspx?ReturnURL=~/Infrastructure/Libraries/User.aspx");

        }

        return ret;
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {

        txtfilter.Text = "";

        ddlManagerFilter.SelectedValue = "0";
        ddlDepartmentFilter.SelectedValue = "0";

        ddlNationality.SelectedValue = "0";

        Load_UserTypeFilter();
        Load_Company_By_User_Type_Filter();

        BindUserGrid();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {

        BindUserGrid();

    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {

    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {


        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objUserBLL.Get_UserList_Search(txtfilter.Text != "" ? txtfilter.Text : null
            , UDFLib.ConvertIntegerToNull(ddlCompanyFilter.SelectedValue)
            , UDFLib.ConvertIntegerToNull(ddlDepartmentFilter.SelectedValue)
            , UDFLib.ConvertIntegerToNull(ddlManagerFilter.SelectedValue), null, null, null, sortbycoloumn, sortdirection
          , null, null, ref  rowcount);


        string[] HeaderCaptions = { "User ID", "First Name", "Last Name", "Designation", "Mobile No", "Email", "Department", "Manager" };
        string[] DataColumnsName = { "User_Name", "First_Name", "Last_Name", "Designation", "Mobile_Number", "MailID", "User_Dept", "ManagerFirstName" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "UserList", "User List", "");

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {
        OperationMode = "Edit User";

        DataTable dt = objUserBLL.Get_UserDetails(Convert.ToInt32(e.CommandArgument.ToString()));

        ddlUserType.SelectedValue = dt.Rows[0]["User_type"].ToString() != "" ? dt.Rows[0]["User_type"].ToString() : "0";
        if ((ddlCompany.Items.FindByValue(dt.Rows[0]["CompanyID"].ToString()) != null))
        {
            ddlCompany.SelectedValue = dt.Rows[0]["CompanyID"].ToString() != "" ? dt.Rows[0]["CompanyID"].ToString() : "0";
        }
        else
        {
            ddlCompany.SelectedValue = "0";
        }

        if (ddlCompany.SelectedValue != "0")
        {
            Load_DepartmentList();
            Load_FleetList();
            Load_ManagerList();
        }

        txtUserID.Text = dt.Rows[0]["UserID"].ToString();
        txtFirstName.Text = dt.Rows[0]["First_Name"].ToString();
        txtLastName.Text = dt.Rows[0]["Last_Name"].ToString();
        txtDateOfBirth.Text = dt.Rows[0]["DOB"].ToString();
        txtDesignation.Text = dt.Rows[0]["Designation"].ToString();
        txtPermanentAddress.Text = dt.Rows[0]["Permanent_Address"].ToString();
        txtPresentAddress.Text = dt.Rows[0]["Present_Address"].ToString();
        txtApprovalLimit.Text = dt.Rows[0]["Approval_Limit"].ToString();
        txtDateOfJoining.Text = dt.Rows[0]["Date_Of_Joining"].ToString();
        txtDateOfProbation.Text = dt.Rows[0]["Date_Of_Probation"].ToString();
        txtEMail.Text = dt.Rows[0]["MailID"].ToString();
        txtMobileNo.Text = dt.Rows[0]["Mobile_Number"].ToString();


        if (ddlUserType.Items.FindByValue(dt.Rows[0]["User_Type"].ToString()) != null)
        {
            ddlUserType.SelectedValue = dt.Rows[0]["User_Type"].ToString() != "" ? dt.Rows[0]["User_Type"].ToString() : "0";
        }
        if (ddlDepartment.Items.FindByValue(dt.Rows[0]["Dep_Code"].ToString()) != null)
        {
            ddlDepartment.SelectedValue = dt.Rows[0]["Dep_Code"].ToString() != "" ? dt.Rows[0]["Dep_Code"].ToString() : "0";
        }
        if (ddlFleet.Items.FindByValue(dt.Rows[0]["Tech_Manager"].ToString()) != null)
        {
            ddlFleet.SelectedValue = dt.Rows[0]["Tech_Manager"].ToString() != "" ? dt.Rows[0]["Tech_Manager"].ToString() : "0";
        }
        if (ddlManager.Items.FindByValue(dt.Rows[0]["ManagerID"].ToString()) != null)
        {
            ddlManager.SelectedValue = dt.Rows[0]["ManagerID"].ToString() != "" ? dt.Rows[0]["ManagerID"].ToString() : "0";
        }
        if (ddlNationality.Items.FindByValue(dt.Rows[0]["NationalityID"].ToString()) != null)
        {
            ddlNationality.SelectedValue = dt.Rows[0]["NationalityID"].ToString() != "" ? dt.Rows[0]["NationalityID"].ToString() : "0";
        }
        else
            ddlNationality.SelectedValue = "0";
        //ddlNetwkid.SelectedValue = dt.Rows[0]["NetWkId"].ToString() != "" ? dt.Rows[0]["NetWkId"].ToString() : "0";
        iUserCompanyID = Convert.ToInt32(ddlCompany.SelectedValue);


        string Usermodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Usermodal", Usermodal, true);

    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_ManagerList();
        Load_FleetList();
        Load_DepartmentList();

        string Usermodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Usermodal", Usermodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objUserBLL.Delete_User(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session["USERID"]));
        BindUserGrid();
    }

    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {

        Load_Company_By_User_Type();

        string Usermodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Usermodal23", Usermodal, true);
    }

    protected void ddlUserTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
    {

        Load_Company_By_User_Type_Filter();
         
    }
    
}