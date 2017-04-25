using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using SMS.Business.PURC;
using SMS.Business.ASL;
using SMS.Data.PURC;
using SMS.Properties.PURC;
using System.Web.UI.HtmlControls;


public partial class Purchase_PurchaseProcess_Config : System.Web.UI.Page
{
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    BLL_PURC_Config_PO objBLLconfig = new BLL_PURC_Config_PO();
    public string OperationMode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            BindPOGrid();
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx");
    }
    public void BindPOGrid()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLLconfig.PURC_POConfigSearch_BLL(txtSearchPOType.Text != "" ? txtSearchPOType.Text : null, sortbycoloumn, sortdirection
        , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            rgdConfig.DataSource = null;
            rgdConfig.DataBind();
            rgdConfig.DataSource = dt;
            rgdConfig.DataBind();
        }

    }

    string POtype = "";

    protected void updateClick(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('PURC_Config_View.aspx?OperationMode=" + "UPDATE" + "&id=" + btn.CommandArgument.ToString() + "');", true);
    }
    protected void deleteClick(object sender, ImageClickEventArgs e)
    {
        ImageButton im = (ImageButton)sender;
        POConfig POconfig = new POConfig();
        POconfig.ID = im.CommandArgument.ToString();
        POconfig.Currentuser = Session["userid"].ToString();
        int count = objBLLconfig.PURC_Delete_Config_BLL(POconfig);
        string script = "<script type=\"text/javascript\">alert('" + POconfig.POType + " Mandatary Feilds Deleted !');</script>";
        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
        BindPOGrid();
    }

    protected void imgclick(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "O  penWindow", "window.open('PURC_Config_View.aspx?OperationMode=" + "ADD');", true);

    }

    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtSearchPOType.Text = "";
        BindPOGrid();
    }

    protected void ImgExpExcel_Click(object sender, ImageClickEventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLLconfig.PURC_POConfigSearch_BLL(txtSearchPOType.Text != "" ? txtSearchPOType.Text : null, sortbycoloumn, sortdirection
        , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        string[] HeaderCaptions = { "ROWNUM", "Id", "typeid", "VARIABLE_NAME", "AutoOwnerSelection", "CopyToVessel", "AutoPOClosing" };
        string[] DataColumnsName = { "ROWNUM", "Id", "typeid", "VARIABLE_NAME", "AutoOwnerSelection", "CopyToVessel", "AutoPOClosing" };

        GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, "PurchaseConfig", "PurchaseConfig");
    }

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        BindPOGrid();
    }
}