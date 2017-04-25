using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SMS.Business.Infrastructure;
using SMS.Business.TMSA;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;
using System.Text;

public partial class PI_List : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    protected DataTable dtGridItems;
    public int CPID = 0;

    public Boolean uaEditFlag = true;//Test default true
    public Boolean uaDeleteFlage = true;
    BLL_TMSA_PI objCP = new BLL_TMSA_PI();
    BLL_TMSA_KPI objBLL = new BLL_TMSA_KPI();
    BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();
    protected void Page_Load(object sender, EventArgs e)
    {
        // UserAccessValidation();
        if (!IsPostBack)
        {

            ViewState["PI_Id"] = "0";
            if (Convert.ToInt32(ViewState["PI_Id"]) != 0)
            {
                btnSave.Text = "Update";
            }


            BindGrid();
            BindCategory();
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

        if (objUA.Add == 0)
        {
            btnSave.Enabled = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            // btnsave.Visible = false;
            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void BindGrid()
    {
        try
        
        {

            int rowcount = ucCustomPager1.isCountRecord;
            string SearchText = txtSearch.Text;
            string Category = null;
            if (ddlCategory.SelectedIndex == 0)
            {
                Category = "";
            }
            else
            {
                Category = ddlCategory.SelectedValue;
            }
            DataTable dt = null;
            DataSet DS = BLL_TMSA_PI.Get_PI_List(SearchText, Category, ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount);

            if (DS.Tables.Count != 0)
            {
                dt = DS.Tables[0];

                btnExport.Visible = true;
                if (ucCustomPager1.isCountRecord == 1)
                {
                    ucCustomPager1.CountTotalRec = rowcount.ToString();
                    ucCustomPager1.BuildPager();
                }

                if (dt.Rows.Count > 0)
                {
                    gvPIList.DataSource = dt;
                    gvPIList.DataBind();
                }
                else
                {
                    gvPIList.DataSource = null;
                    gvPIList.DataBind();
                    
                }
            }
            else
            {
                gvPIList.DataSource = null;
                gvPIList.DataBind();
            }



        }
        catch (Exception ex)
        {

        }
    }

    public void imgUpdate_Click(object sender, EventArgs e)
    {
        ImageButton ibtnUpdate = (ImageButton)sender;
        GridViewRow row = (GridViewRow)ibtnUpdate.NamingContainer;
        int PI_ID = Convert.ToInt32(gvPIList.DataKeys[row.RowIndex].Value);
        gvPIList.SelectedIndex = row.RowIndex;
        ViewState["PI_ID"] = PI_ID;
        BindGrid();
    }

    protected void btnSaveItem_Click(object sender, EventArgs e)
    {

        if (Session["CPID"] != null)
        {
            SaveData();
            BindGrid();

        }


    }

    protected void ClearData()
    {
        ltmessage.Text = "";
        txtPICode.Text = "";
        txtPIName.Text = "";
        txtItemDescription.Text = "";

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();

    }

    protected void SaveData()
    {
        ltmessage.Text = "";
        try
        {
            string Interval = ddlIntervalUnit.SelectedValue;
            string PI_Name = txtItemDescription.Text;
            string PICode = txtPICode.Text;
            string Description = txtItemDescription.Text;
            string Context = txtContext.Text;

            // int result=  BLL_TMSA_PI.INS_PI_Details(PI_Name, PICode ,Interval, Description,null,Context, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
            ClearData();
        }
        catch (Exception ex)
        {
            ltmessage.Text = ex.ToString();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {

        ClearData();

    }

    protected void imgsearh_Click(object sender, ImageClickEventArgs e)
    {
        BindGrid();
    }
    private void BindCategory()
    {

        ListItem liselect = new ListItem("--Select--", "0", true);
        DataTable dt = objBLL.Get_CategoryList("");
        ddlCategory.DataSource = dt;
        ddlCategory.DataTextField = "Category_Name";
        ddlCategory.DataValueField = "Category_Name";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, liselect);
        ddlCategory.SelectedValue = "0";
    }


    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        int rowcount = ucCustomPager1.isCountRecord;
        string SearchText = txtSearch.Text;
        string Category = null;
        if (ddlCategory.SelectedIndex == 0)
        {
            Category = "";
        }
        else
        {
            Category = ddlCategory.SelectedValue;
        }
        DataTable dt = null;
        dt = BLL_TMSA_PI.Get_PI_List(SearchText, Category, null, null, ref  rowcount).Tables[0];

        string[] HeaderCaptions = { "PI Name", "Code", "Interval", "Description", "Unit", "Status" };
        string[] DataColumnsName = { "Name", "Code", "Interval", "Description", "UOM", "Status" };
        GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, "PI List", "PI List");
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

}


   
