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
using SMS.Business.Technical;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Infrastructure_Libraries_WorkList_Access : System.Web.UI.Page
{
    BLL_Worklist_Access objBLL = new BLL_Worklist_Access();
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();

    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    BLL_Tec_Worklist objBLLWL = new BLL_Tec_Worklist();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            Load_UserList();
            LoadCombo();
            BindWorkList_Access();
        }

    }

    /// <summary>
    /// Fill values in Combobox and Checkbox list
    /// </summary>
    protected void LoadCombo()
    {
        DataSet ds = objBLLWL.GetAllWorklistType();
        chkList.DataTextField = "Worklist_Type_Display";
        chkList.DataValueField = "Worklist_Type";
        chkList.DataSource = ds.Tables[0];
        chkList.DataBind();
    }

    public void BindWorkList_Access()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        //int? isfavorite = null; if (ddlFavorite.SelectedValue != "2") isfavorite = Convert.ToInt32(ddlFavorite.SelectedValue.ToString());
        //int? countrycode = null; if (ddlSearchCountry.SelectedValue != "0") countrycode = Convert.ToInt32(ddlSearchCountry.SelectedValue.ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.SearchWorklistAccess(txtSearch.Text.Trim(), UDFLib.ConvertStringToNull(ddlAction_Type.SelectedValue), sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvWorkListAccess.DataSource = dt;
            gvWorkListAccess.DataBind();
        }
        else
        {
            gvWorkListAccess.DataSource = dt;
            gvWorkListAccess.DataBind();
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

    protected void Load_UserList()
    {
        DataTable dt = objBLLUser.Get_UserList();
        string filter = "User_type ='OFFICE USER' or User_type like '%admin%' ";


        dt.DefaultView.RowFilter = filter;



        ddlUserName.DataSource = dt.DefaultView;
        ddlUserName.DataTextField = "USERNAME";
        ddlUserName.DataValueField = "USERID";
        ddlUserName.DataBind();
    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_ddlUserName");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Worklist Access";
        lblMsg.Text = "";
        ddlActionType.SelectedIndex = 0;
        ddlUserName.SelectedValue = "0";
        ddlActionType.Enabled = true;

        string AddDeptmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddDeptmodal", AddDeptmodal, true);
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        int responseid = 0;
        DataTable JobType = new DataTable();
        JobType.Columns.Add("ID", typeof(string));
        JobType.Columns.Add("VALUE", typeof(string));
        lblMsg.Text = "";

        foreach (ListItem item in chkList.Items)
        {
            if (item.Selected)
            {
                JobType.Rows.Add(null, item.Value);
            }
        }
        if (JobType.Rows.Count == 0)
        {
            lblMsg.Text = "Action Type is mandatory!";
            string AddDeptmodal = String.Format("showModal('divadd',true);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddDeptmodal", AddDeptmodal, true);
            return;
        }



        if (HiddenFlag.Value == "Add")
        {

            responseid = objBLL.InsertWorklistAccess(UDFLib.ConvertIntegerToNull(ddlUserName.SelectedValue), UDFLib.ConvertStringToNull(ddlActionType.SelectedValue), Convert.ToInt32(Session["USERID"]), JobType);

        }
        else
        {
            responseid = objBLL.EditWorklistAccess(UDFLib.ConvertIntegerToNull(ddlUserName.SelectedValue), UDFLib.ConvertStringToNull(ddlActionType.SelectedValue), int.Parse(txtAccessID.Text), Convert.ToInt32(Session["USERID"]), JobType);

        }
        if (responseid > 0)
        {
            BindWorkList_Access();
            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
        else
        {
            if (HiddenFlag.Value == "Add")
            {
                OperationMode = "Add Worklist Access";
            }
            else
            {
                OperationMode = "Edit Worklist Access";
            }
            lblMsg.Text = "User Name and Action Type already exists!";
            string AddDeptmodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddDeptmodal", AddDeptmodal, true);
        }

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {
        try
        {
            HiddenFlag.Value = "Edit";
            OperationMode = "Edit Worklist Access";
            lblMsg.Text = "";
            DataSet ds = new DataSet();
            ds = objBLL.Get_WorkListAccessList(Convert.ToInt32(e.CommandArgument.ToString()));
            txtAccessID.Text = e.CommandArgument.ToString();
            //ddlUserName.SelectedValue = dt.Rows[0]["ID"].ToString() != "" ? dt.Rows[0]["ID"].ToString() : "0";
            //alert(ds.Tables[0].Rows[0]["User_ID"].ToString());
          //  return;
          //  System.Diagnostics.Debug.WriteLine(ds.Tables[0].Rows[0]["User_ID"].ToString()); 
             ddlUserName.SelectedValue = ds.Tables[0].Rows[0]["User_ID"].ToString();
            ddlActionType.SelectedValue = ds.Tables[0].Rows[0]["Action_Type"].ToString();
            ddlActionType.Enabled = false;


            foreach (ListItem itemchk in chkList.Items)
            {
                DataRow[] dr = ds.Tables[1].Select("JOB_TYPE='" + itemchk.Value + "'");
                if (dr.Length > 0)
                {
                    itemchk.Selected = true;

                }
                else
                {
                    itemchk.Selected = false;
                }
            }

            chkList.DataBind();
            string Deptmodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Deptmodal", Deptmodal, true);
        }
        catch (Exception)
        {
            
            throw;
        }

      

    }

    protected void alert(string msg)
    {
        string message = String.Format("alert("+msg+");");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "message", message, true);
    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DeleteWorklistAccess(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindWorkList_Access();


    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ucCustomPagerItems.CurrentPageIndex = 1;
        BindWorkList_Access();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {

        // ddlUser_Name.SelectedValue = "0";
        txtSearch.Text = "";
        ddlAction_Type.SelectedValue = "0";

        BindWorkList_Access();

    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        //int? isfavorite = null; if (ddlFavorite.SelectedValue != "2") isfavorite = Convert.ToInt32(ddlFavorite.SelectedValue.ToString());
        //int? countrycode = null; if (ddlSearchCountry.SelectedValue != "0") countrycode = Convert.ToInt32(ddlSearchCountry.SelectedValue.ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.SearchWorklistAccess(txtSearch.Text.Trim(), UDFLib.ConvertStringToNull(ddlAction_Type.SelectedValue), sortbycoloumn, sortdirection
             , null, null, ref  rowcount);

        string[] HeaderCaptions = { "User Name", "Action Type", "Assigned Job Types" };
        string[] DataColumnsName = { "User_Name", "Action_Type", "Access_Details" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Worklis_Access", "Worklist Access");

    }


    protected void gvWorkListAccess_RowDataBound(object sender, GridViewRowEventArgs e)
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


    protected void gvWorkListAccess_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindWorkList_Access();
    }
}