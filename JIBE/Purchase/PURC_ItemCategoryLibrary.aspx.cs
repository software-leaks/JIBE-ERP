using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SMS.Business.PURC;
using Telerik.Web.UI;
using SMS.Properties;

public partial class Purchase_PURC_ItemCategoryLibrary : System.Web.UI.Page
{

    string DeptID;
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
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


            BindrgdItmCat();
        }
    }
    

    /// <summary>
    /// Set Session User As 0 If null
    /// </summary>
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void UserAccessValidation()
    {
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(GetSessionUserID(), PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx");

        if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            ImgSave.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;


    }
 
    /// <summary>
    ///  Bind ItemCategory Grid View with sorting 
    /// </summary>
    public void BindrgdItmCat()                                                                                 // Bind Item Category Grid
    {
        DataSet ds = new DataSet();
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            BLL_PURC_Purchase obj = new BLL_PURC_Purchase();
            ds = obj.Get_Purc_LIB_ItemCategory(txtSearchName.Text != "" ? txtSearchName.Text : null, "ItemCategory", null, sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
            ViewState["table"] = ds.Tables[0];


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }
            rgdItmCat.DataSource = ds.Tables[0];
            rgdItmCat.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

       
    }

    /// <summary>
    ///  On Add Button Click open "Add Item Categories" Pop Up
    /// </summary>
    protected void ImgAdd_Click(object sender, EventArgs e)                                                     // Add Button Click
    {
        this.SetFocus("ctl00_MainContent_TxtDept"); 
        HiddenFlag.Value = "Add";
        OperationMode = "Add Department";
        TxtCatName.Text = string.Empty;
        TxtShrtName.Text = string.Empty;
        ddlcatType.SelectedIndex = 0;


        ddlcatType.SelectedIndex = -1;
        string Cat_modal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCategorymodal", Cat_modal, true);
    }

    /// <summary>
    /// On Update Button Click Fill the Controls on the Pop Up and Open the Update Category Pop Up
    /// </summary>
    protected void onUpdate(object sender, EventArgs e)                                                         // Update Button Click
    {
        DataTable dt = (DataTable)ViewState["table"];
        OperationMode="Update Item Category";
        HiddenFlag.Value = "Update";
        ImageButton ibtn = (ImageButton) sender;
        ViewState["selectedID"] = ibtn.CommandArgument.ToString();
        dt.DefaultView.RowFilter = "ID=" + ViewState["selectedID"].ToString() + "";
        TxtCatName.Text = dt.DefaultView[0]["Category_Name"].ToString();
        TxtShrtName.Text = dt.DefaultView[0]["Category_Short_Code"].ToString();
        ddlcatType.SelectedIndex = dt.DefaultView[0]["Category_Type"].ToString() == "UrgencyLevel" ? 2 : 1;
        string Cat_modal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "UpdateCategorymodal", Cat_modal, true);
    
    }

    /// <summary>
    /// Delete the Items Category From the List
    /// </summary>
    protected void onDelete(object sender, EventArgs e)                                                         // Delete Button Click
    {
         ImageButton ibtn = (ImageButton) sender;
         string[] deleteCmd = ibtn.CommandArgument.ToString().Split(',');
         if (Convert.ToBoolean(Convert.ToInt32(deleteCmd[1])))
         {
             ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "alert('This category is in use - are you sure you want to remove it?');", true);
         }
             using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
             {
                 int count = objTechService.PURC_INS_UPD_ItemCategory(TxtCatName.Text, ddlcatType.SelectedItem.Text, null, deleteCmd[0], GetSessionUserID().ToString(), 1);

                 BindrgdItmCat();
             }
         
    }

    /// <summary>
    ///  On Refresh Button Click Cleare the Filter and Bind the Item Category Grid View
    /// </summary>
    protected void btnRefresh_Click(object sender, EventArgs e)                                                 // Refresh Button Click
    {

        //ddlCategoryType.SelectedIndex = 0;
        txtSearchName.Text = "";
        BindrgdItmCat();

    }

    /// <summary>
    /// On Search click Bind the Item Category Grid searched by user
    /// </summary>
    protected void btnFilter_Click(object sender, EventArgs e)                                                  // Search Button Click
    {
        BindrgdItmCat();
    }

    /// <summary>
    /// Export The Gridview Data to Excel
    /// </summary>
    protected void ImgExpExcel_Click(object sender, EventArgs e)                                                //Export to Excel Clcik
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        BLL_PURC_Purchase obj = new BLL_PURC_Purchase();
        DataTable dt = obj.Get_Purc_LIB_ItemCategory(null, "ItemCategory", null, sortbycoloumn, sortdirection
        , null, null, ref  rowcount).Tables[0];

        string[] HeaderCaptions = { "Code", "Name", "Category Type" };
        string[] DataColumnsName = { "ID", "Category_Name", "Category_Type" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "CategoryItems", "CategoryItems", "");

    }

    public void Reset()
    {
        TxtCatName.Text = "";
        ddlcatType.SelectedIndex = 0;
    }                 

    /// <summary>
    /// On save click Saves the New Item Category and Updates the Existing Item Category
    /// </summary>
    protected void Save_Click(object sender, EventArgs e)                                                       // Save/Update Button Click
    {
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            
            if (HiddenFlag.Value == "Add")
            {
                int count = objTechService.PURC_INS_UPD_ItemCategory(TxtCatName.Text, TxtShrtName.Text, ddlcatType.SelectedItem.Text, null, GetSessionUserID().ToString(), 0);
            }
            else
            {
                int count = objTechService.PURC_INS_UPD_ItemCategory(TxtCatName.Text, TxtShrtName.Text, ddlcatType.SelectedItem.Text, ViewState["selectedID"].ToString(), GetSessionUserID().ToString(), 0);
            }

            BindrgdItmCat();

            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
        Reset();
    }

    /// <summary>
    /// On Sorting Changes the Sorting Direction image
    /// </summary>
    protected void rgdItmCat_RowDataBound(object sender, GridViewRowEventArgs e)                                // Sorting Direction image Change
    {
        try
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
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Sorting the Data on Column header Click
    /// </summary>
    protected void rgdItmCat_Sorting(object sender, GridViewSortEventArgs se)                                   // Sorting
    { 

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindrgdItmCat();

    }

   


}