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

public partial class KPI_List : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    protected DataTable dtGridItems;
    public Boolean uaEditFlag = true;//Test default true
    public Boolean uaDeleteFlage = true;
    BLL_TMSA_KPI objBLL = new BLL_TMSA_KPI();
    BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();
    protected void Page_Load(object sender, EventArgs e)
    {
       // UserAccessValidation();
        if (!IsPostBack)
        {
            Session["dtPI"] = null;
            BindGrid();
            BindPI();
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
            string category=ddlCategory.SelectedValue;
            if (ddlCategory.SelectedIndex == 0)
            {
                category = "";
            }
            int rowcount = ucCustomPager1.isCountRecord;
            DataTable dt = BLL_TMSA_PI.Get_KPI_List(txtSearch.Text, ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount,category).Tables[0];
            if (dt.Rows.Count > 0)
                btnExport.Visible = true;

            if (ucCustomPager1.isCountRecord == 1)
            {
                ucCustomPager1.CountTotalRec = rowcount.ToString();
                ucCustomPager1.BuildPager();
            }

            gvKPIList.DataSource = dt;
            gvKPIList.DataBind();

        }
        catch (Exception ex)
        {

        }
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
        dt = BLL_TMSA_PI.Get_KPI_List(SearchText, null, null, ref  rowcount, Category).Tables[0];

        string[] HeaderCaptions = { "KPI Name", "Code", "Interval", "Description", "Status" };
        string[] DataColumnsName = { "Name", "Code", "Interval", "Description", "KPI_Status" };
        GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, "KPI List", "KPI List");
    }


    protected void btnAddPI_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtPI"];
        DataRow dr = dt.NewRow();
        dr["PI_ID"] = ddlPIList.SelectedValue;
        dt.Rows.Add(dr);

        ddlPIList.Items.RemoveAt(ddlPIList.SelectedIndex);

        Session["dtPI"] = dt;
        chkPI.DataSource = dt;
        chkPI.DataValueField = "PI_ID";
        chkPI.DataTextField = "Name";
        chkPI.DataBind();
        //chk1.SelectedItem.Selected = true;

        if (chkPI.Items.Count > 0)
        {
            foreach (ListItem chkitem in chkPI.Items)
            {
                chkitem.Selected = true;
            }

        }
    }
    protected void btnCountryRemove_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtPI"];
        BindPI();

        dt.Clear();
        Session["dtPI"] = dt;
        chkPI.DataSource = dt;

        chkPI.DataBind();

    }


    public void imgUpdate_Click(object sender, EventArgs e)
    {
        ImageButton ibtnUpdate = (ImageButton)sender;
        GridViewRow row = (GridViewRow)ibtnUpdate.NamingContainer;
        int KPI_ID = Convert.ToInt32(gvKPIList.DataKeys[row.RowIndex].Value);
        gvKPIList.SelectedIndex = row.RowIndex;
        ViewState["KPI_ID"] = KPI_ID;
        BindDetails();
    }


    private void BindDetails()
    {
        DataTable dt = BLL_TMSA_PI.Get_KPI_Detail (UDFLib.ConvertIntegerToNull(ViewState["KPI_ID"])).Tables[0];
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            if (dr["Interval"] != null)
             ddlIntervalUnit.SelectedValue = dr["Interval"].ToString();
            txtItemDescription.Text = dr["Description"].ToString();
            txtPIName.Text = dr["Name"].ToString();
            txtPICode.Text = dr["Code"].ToString();

        }
        BindPI();
    }


    protected void BindPI()
    {
        this.InitialCountryBind();

        int? KPI_ID = UDFLib.ConvertIntegerToNull(ViewState["KPI_ID"]);
        DataTable dt1 = BLL_TMSA_PI.Get_PI_ListByKPI(KPI_ID).Tables[0];//Load by notificationId
        Session["dtPI"] = dt1;
        chkPI.DataSource = dt1;
        chkPI.DataTextField = "Name";
        chkPI.DataValueField = "PI_ID";
        chkPI.DataBind();

        foreach (ListItem chkitem in chkPI.Items)
        {
            chkitem.Selected = true;
            if (ddlPIList.Items.FindByValue(chkitem.Value) != null)
            {
                ListItem itemToRemove = ddlPIList.Items.FindByValue(chkitem.Value);
                ddlPIList.Items.Remove(itemToRemove);

            }
        }
    }

    protected void InitialCountryBind()
    {
        int rowcount = 1;
        DataTable dt = BLL_TMSA_PI.Get_PI_List(null,"", null, null, ref  rowcount).Tables[0];
        ddlPIList.DataSource = dt;
        ddlPIList.DataTextField = "Name";
        ddlPIList.DataValueField = "PI_ID";
        ddlPIList.DataBind();
        ddlPIList.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void btnSaveItem_Click(object sender, EventArgs e)
    {

            SaveData();
            BindGrid();

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
            string Description =txtItemDescription.Text;
            string Context = null;

            DataTable dtPI = new DataTable();
            dtPI.Columns.Add("PI_ID");

             foreach (ListItem chkitem in chkPI.Items)
            {
                if (chkitem.Selected)
                {
                    DataRow dr = dtPI.NewRow();
                    dr["PI_ID"] = chkitem.Value;
                }
             }

             int result = BLL_TMSA_PI.INS_KPI_Details(PI_Name, PICode, Interval, Description, dtPI, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
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

    protected void imgsearch_Click(object sender, EventArgs e)
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

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}
   
