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
using System.Drawing;
using System.Collections.Generic;
using System.Reflection;
public partial class Infrastructure_Libraries_InspectionType : System.Web.UI.Page
{
    BLL_Infra_InspectionType objBLL = new BLL_Infra_InspectionType();
    UserAccess objUA = new UserAccess();
    DataSet dsColor = new DataSet();
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
            BindColor();
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


        DataTable dt = objBLL.SearchInspectionType(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
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
    //Method Binds colors to drop down list
    public void BindColor()
    {
        ListItem liselect = new ListItem("--Select--", "0", true);
        ddlColor.Items.Insert(0, liselect);
        ddlColor.Items.Add(new ListItem("Black", "Gainsboro"));
        ddlColor.Items.Add(new ListItem("Blue", "LightSkyBlue"));
        ddlColor.Items.Add(new ListItem("Green", "Palegreen"));
        ddlColor.Items.Add(new ListItem("Red", "Salmon"));
        ddlColor.Items.Add(new ListItem("Yellow", "Lemonchiffon"));
        ddlColor.Items.Add(new ListItem("Navy Blue", "Lightsteelblue"));
        ddlColor.Items.Add(new ListItem("Purple", "Thistle"));
        ddlColor.Items.Add(new ListItem("Cyan", "Lightcyan"));
        ddlColor.Items.Add(new ListItem("Pink", "LavenderBlush"));
        ddlColor.Items.Add(new ListItem("Brown", "Bisque"));
        ddlColor.DataBind();
    }
    private List<string> finalColorList()
    {
        string[] allColors = Enum.GetNames(typeof(System.Drawing.KnownColor));
        string[] systemEnvironmentColors = new string[(typeof(System.Drawing.SystemColors)).GetProperties().Length];
        int index = 0;
        foreach (MemberInfo member in (typeof(System.Drawing.SystemColors)).GetProperties())
        {
            systemEnvironmentColors[index++] = member.Name;
        }
        List<string> finalColorList = new List<string>();
        finalColorList.Clear();
        finalColorList.Add("--SELECT--");
        foreach (string color in allColors)
        {
            if (Array.IndexOf(systemEnvironmentColors, color) < 0)
            {
                finalColorList.Add(color);
            }
        }
        return finalColorList;
    }
    /// <summary>
    /// Method called When Add Image is clicked
    /// </summary>
    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtInspectionType");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Inspection Type";

        txtInspectionType.Text = "";
        ddlColor.SelectedValue = "0";

        string AddInpsectionTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddInpsectionTypemodal", AddInpsectionTypemodal, true);
    }
    //Method saves the colour for the Inspection Types.
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (HiddenFlag.Value == "Add")
        {
            if (!check(txtInspectionType.Text.ToString()))
            {
                string js = "alert('Inspection Type Already Exist!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js, true);
                txtInspectionType.Focus();
                string hideModal = String.Format("hideModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hideModal", hideModal, true);
                return;

            }
            int responseid = objBLL.InsertInspectionType(txtInspectionType.Text.Trim(), Convert.ToInt32(Session["USERID"]), ddlColor.SelectedValue);
        }
        else
        {
            int responseid = objBLL.EditInspectionType(Convert.ToInt32(txtInspectionTypeID.Text), txtInspectionType.Text.Trim(), Convert.ToInt32(Session["USERID"]), ddlColor.SelectedValue);
        }
        BindConfigureSize();
        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
    }
    /// <summary>
    /// Method Checks the Inspection Type
    /// </summary>
    public bool check(string s)
    {
        DataTable dt = objBLL.Check_InspectionType(s);
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    /// <summary>
    /// Method called When user wants to update Inspection Type.
    /// </summary>
    protected void onUpdate(object source, CommandEventArgs e)
    {
        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Inspection Type";

        DataTable dt = new DataTable();
        dt = objBLL.Get_InspectionTypeList();
        dt.DefaultView.RowFilter = "InspectionTypeId= '" + e.CommandArgument.ToString() + "'";

        txtInspectionTypeID.Text = dt.DefaultView[0]["InspectionTypeId"].ToString();
        txtInspectionType.Text = dt.DefaultView[0]["InspectionTypeName"].ToString();

        string ColorCode = "";
        if (dt.DefaultView[0]["ColorCode"] == null)
            ColorCode = "0";
        else
            ColorCode = dt.DefaultView[0]["ColorCode"].ToString();


        if (ddlColor.Items.FindByValue(ColorCode) != null)
        {
            ddlColor.SelectedValue = ColorCode;
        }
        else
            ddlColor.SelectedValue = "0";

        //string InfoDiv = "Get_Record_Information_Details('INF_LIB_Upload_File_Size_Limit','InspectionTypeId=" + txtInspectionTypeID.Text + "')";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "InfoDiv", InfoDiv, true);

        string AddInpsectionTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddInpsectionTypemodal", AddInpsectionTypemodal, true);
    }
    /// <summary>
    /// Method called When user wants to delete Inspection Type.
    /// </summary>
    protected void onDelete(object source, CommandEventArgs e)
    {
        int retval = objBLL.DeleteInspectionType(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindConfigureSize();
    }
    /// <summary>
    /// Method called when search button is clicked.
    /// </summary>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindConfigureSize();
    }
    /// <summary>
    /// Method called when refresh button is clicked.
    /// </summary>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";       
        BindConfigureSize();
    }
    /// <summary>
    /// Method called when export to excel button is clicked.
    /// </summary>
    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        DataTable dt = objBLL.SearchInspectionType(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
                    , null, null, ref  rowcount);

        string[] HeaderCaptions = { "InspectionType"};
        string[] DataColumnsName = { "InspectionTypeName" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "InspectionType", "InspectionType", "");
    }
    /// <summary>
    /// Method called at the time of binding data to teh row.
    /// </summary>
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
    /// <summary>
    /// Method called at the time of sorting data to teh row.
    /// </summary>
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