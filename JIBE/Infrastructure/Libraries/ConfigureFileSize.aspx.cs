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

public partial class Infrastructure_Libraries_ConfigureFileSize : System.Web.UI.Page
{
    BLL_Infra_UploadFileSize objBLL = new BLL_Infra_UploadFileSize();
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
            //ucCustomPagerItems.PageSize = 20;      
            BindConfigureSize();
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
    public void BindConfigureSize()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.SearchConfigureFileSize(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvVesselType.DataSource = dt;
            gvVesselType.DataBind();
        }
        else
        {
            gvVesselType.DataSource = dt;
            gvVesselType.DataBind();
        }

    }
    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        chkVesselSyncable.Checked = false;
        this.SetFocus("ctl00_MainContent_txtUserType");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Configure File Size";

        txtUserType.Text = "";
        txtFileSize.Text = "";

        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        int VesselSyncable = 0;
      
        if (chkVesselSyncable.Checked == true)
        {
            VesselSyncable = 1;
        }
      

        if (HiddenFlag.Value == "Add")
        {
            int responseid = objBLL.InsertConfigureFileSize(txtUserType.Text.Trim(), Convert.ToInt32(txtFileSize.Text), Convert.ToInt32(Session["USERID"]),VesselSyncable);

        }
        else
        {
            int responseid = objBLL.EditConfigureFileSize(Convert.ToInt32(txtUserTypeID.Text), txtUserType.Text.Trim(), Convert.ToInt32(txtFileSize.Text), Convert.ToInt32(Session["USERID"]),VesselSyncable);

        }

        BindConfigureSize();
        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }
    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit FileUploadSize";

        DataTable dt = new DataTable();
        dt = objBLL.Get_ConfigurefilesizeList(Convert.ToInt32(e.CommandArgument.ToString()));
        dt.DefaultView.RowFilter = "Rule_ID= '" + e.CommandArgument.ToString() + "'";

        txtUserTypeID.Text = dt.DefaultView[0]["Rule_ID"].ToString();
        txtUserType.Text = dt.DefaultView[0]["Attach_Prefix"].ToString();
        txtFileSize.Text = dt.DefaultView[0]["Size_KB"].ToString();
        chkVesselSyncable.Checked = dt.DefaultView[0]["Syncable"].ToString() != "0" ? true : false;

        string InfoDiv = "Get_Record_Information_Details('INF_LIB_Upload_File_Size_Limit','Rule_ID=" + txtUserTypeID.Text + "')";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);

        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);

    }
    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DeleteConfigureFileSize(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindConfigureSize();
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindConfigureSize();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        txtFileSize.Text="";
        BindConfigureSize();

    }
    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        DataTable dt = objBLL.SearchConfigureFileSize(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
                    , null, null, ref  rowcount);

        string[] HeaderCaptions = { "AttachPrefix",  "Size_KB" };
        string[] DataColumnsName = { "AttachPrefix", "Size_KB" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "FileUploadConfigure", "FileUploadConfigure", "");

    }
    protected void gvVesselType_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void gvVesselType_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindConfigureSize();
    }
}