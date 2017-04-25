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

public partial class Purchase_PURC_FunctionsLibrary : System.Web.UI.Page
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
            BindrgdFunc(); // Bind Functions Grid
            FillDDLreqsnType();
        }
    }
    // -- Set Session User As 0 If null
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
    /// Bind the Requisition Type Dropdown List 
    /// </summary>
    public void FillDDLreqsnType()                                                                         // Bind requisitionType Dropdown List
    {
        BLL_PURC_Purchase obj = new BLL_PURC_Purchase();
        DataTable dt = obj.Get_ReqsnType();
        try
        {
            cmbReqsnType.DataSource = dt;
            cmbReqsnType.DataTextField = "Reqsn_Name";
            cmbReqsnType.DataValueField = "ID";
            cmbReqsnType.DataBind();
            cmbReqsnType.Items.Insert(0, "SELECT");

            ddlREqsnType.DataSource = dt;
            ddlREqsnType.DataTextField = "Reqsn_Name";
            ddlREqsnType.DataValueField = "ID";
            ddlREqsnType.DataBind();
            ddlREqsnType.Items.Insert(0, "ALL");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }



    }

    /// <summary>
    /// Bind Function Grid View with sorting 
    /// </summary>
    public void BindrgdFunc()                                                                                          //Bind Functions Grid
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            BLL_PURC_Purchase obj = new BLL_PURC_Purchase();
            DataSet ds = obj.Get_Purc_LIB_Functions(txtSearchName.Text != "" ? txtSearchName.Text : null, UDFLib.ConvertStringToNull(ddlREqsnType.SelectedValue == "ALL" ? null : ddlREqsnType.SelectedValue), null, sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
            ViewState["table"] = ds.Tables[0];

            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }


            if (ds.Tables[0].Rows.Count > 0)
            {
                rgdFunc.DataSource = ds.Tables[0];
                rgdFunc.DataBind();
            }
            else
            {
                rgdFunc.DataSource = ds.Tables[0];
                rgdFunc.DataBind();
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    ///  On Add Button Click open "Add Functions" Pop Up
    /// </summary>
    protected void ImgAdd_Click(object sender, EventArgs e)                                                            //Add Button Click
    {
        try
        {
            this.SetFocus("ctl00_MainContent_TxtDept");
            HiddenFlag.Value = "Add";
            OperationMode = "Add Functions";
            TxtFuncName.Text = string.Empty;
            txt_Func_SName.Text = string.Empty;
            cmbReqsnType.SelectedIndex = 0;
            txt_Func_SName.Text = string.Empty;
            chkIsService.Checked = false;

            string AddDeptmodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddDeptmodal", AddDeptmodal, true);

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// On Update Button Click Fill the Controls on the Pop Up and Open the Update Functions Pop Up
    /// </summary>
    protected void onUpdate(object sender, ImageClickEventArgs e)                                                      // Update Click
    {
        DataTable dt = (DataTable)ViewState["table"];
        OperationMode = "Update Functions";
        HiddenFlag.Value = "Update";
        ImageButton ibtn = (ImageButton)sender;
        ViewState["selectedID"] = ibtn.CommandArgument.ToString();
        dt.DefaultView.RowFilter = "ID=" + ViewState["selectedID"].ToString() + "";
        TxtFuncName.Text = dt.DefaultView[0]["Function_Name"].ToString();
        cmbReqsnType.SelectedValue = dt.DefaultView[0]["ReqsnID"].ToString();
        txt_Func_SName.Text = dt.DefaultView[0]["Function_Short_Code"].ToString();
        chkIsService.Checked = Convert.ToBoolean(Convert.ToInt32(dt.DefaultView[0]["Function_Type"]));
        string Functionsmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Functions", Functionsmodal, true);

    }

    /// <summary>
    /// Delete the Function From the List
    /// </summary>
    protected void onDelete(object sender, EventArgs e)                                                                 //Delete Image Button Click
    {
        ImageButton ibtn = (ImageButton)sender;
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            int count = objTechService.Purc_INS_UPD_Functions(TxtFuncName.Text, "", cmbReqsnType.SelectedItem.Text, "", ibtn.CommandArgument.ToString(), GetSessionUserID().ToString(), "Delete");

            BindrgdFunc();
        }
    }                                                            

    /// <summary>
    ///  On Refresh Button Click Cleare the Filter and Bind the Functions Grid View
    /// </summary>
    protected void btnRefresh_Click(object sender, EventArgs e)                                                        //Refresh Button Click
    {

        ddlREqsnType.SelectedIndex = 0;
        txtSearchName.Text = "";
        BindrgdFunc();

    }

    /// <summary>
    /// On Search click Bind the Functions searched by user
    /// </summary>
    protected void btnFilter_Click(object sender, EventArgs e)                                                         //Search Button Click
    {
        BindrgdFunc();
        txtSearchName.Text = "";
    }

    /// <summary>
    /// Export The Gridview Data to Excel
    /// </summary>
    protected void ImgExpExcel_Click(object sender, EventArgs e)                                                       //Export The Gridview Data to Excel
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        BLL_PURC_Purchase obj = new BLL_PURC_Purchase();
        DataTable dt = obj.Get_Purc_LIB_Functions( null, null, null, sortbycoloumn, null
        , null, null, ref  rowcount).Tables[0];
        
        string[] HeaderCaptions = { "Function Name", "Short Name", "Reqsn Type", "Is Service" };
        string[] DataColumnsName = { "Function_Name", "Function_Short_Code", "Reqsn_Type", "Function_Type" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Functions", "Functions", "");

    }

    /// <summary>
    /// On save click Saves the New Function and Updates the Existing Functions
    /// </summary>
    protected void Save_Click(object sender, EventArgs e)                                                              // Save/Update Button Click
    {
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {

            if (HiddenFlag.Value == "Add")
            {
                int count = objTechService.Purc_INS_UPD_Functions(TxtFuncName.Text, txt_Func_SName.Text, cmbReqsnType.SelectedValue, chkIsService.Checked == true ? "1" : "0", null, GetSessionUserID().ToString(), "Add");
            }
            else
            {
                int count = objTechService.Purc_INS_UPD_Functions(TxtFuncName.Text, txt_Func_SName.Text, cmbReqsnType.SelectedValue, chkIsService.Checked == true ? "1" : "0", ViewState["selectedID"].ToString(), GetSessionUserID().ToString(), "Update");
            }

            BindrgdFunc();
            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
    }


    /// <summary>
    /// On Sorting Changes the Sorting Direction image
    /// </summary>
    protected void rgdFunc_RowDataBound(object sender, GridViewRowEventArgs e)                                         // Sorting Direction image Change
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
    protected void rgdFunc_Sorting(object sender, GridViewSortEventArgs se)                                            // Sorting
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindrgdFunc();

    }

}