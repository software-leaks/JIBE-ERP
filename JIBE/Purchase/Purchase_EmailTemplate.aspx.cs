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
using SMS.Business.Infrastructure;
using System.Text;


public partial class Purchase_Purchase_EmailTemplate : System.Web.UI.Page
{

    BLL_PURC_Purchase objBLLDept = new BLL_PURC_Purchase();
    UserAccess objUA = new UserAccess();
    public string OperationMode = "";
    public string Flag = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    protected void Page_Load(object sender, EventArgs e)
    {

            UserAccessValidation();
            if (!IsPostBack)
            {
                BindEmailStatus();
                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;
    
                BindGrid();
            }
        
       
    }
    /// <summary>
    /// This method is used to bind DDL of email template
    /// </summary>
    protected void BindEmailStatus()
    {
        try
        {
            DataSet dt = objBLLDept.Get_Email_System_Parameter("EMAIL", UDFLib.ConvertToInteger(Session["UserID"].ToString()));


            ddlEmailfilter.DataSource = dt;
            ddlEmailfilter.DataValueField = "Name";
            ddlEmailfilter.DataTextField = "Description";
            ddlEmailfilter.DataBind();
            ddlEmailfilter.Items.Insert(0, new ListItem("-All Template-", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;

    }
    protected void UserAccessValidation()
    {
        try
        {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            ImgAdd.Visible = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        //else
            // btnsave.Visible = false;

            if (objUA.Delete == 1) uaDeleteFlage = true;
        }
    catch (Exception ex)
    {
        UDFLib.WriteExceptionLog(ex);
    }

    }

    /// <summary>
    /// This method is used to bind Grid on search
    /// </summary>
    public void BindGrid()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            DataTable dt = objBLLDept.Search_Email_Template_List(UDFLib.ConvertStringToNull(ddlEmailfilter.SelectedValue), sortbycoloumn, sortdirection
                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }


            if (dt.Rows.Count > 0)
            {
                gvEmailTemplate.DataSource = dt;
                gvEmailTemplate.DataBind();
            }
            else
            {
                gvEmailTemplate.DataSource = dt;
                gvEmailTemplate.DataBind();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    /// <summary>
    /// This method is used to Add Email Template
    /// </summary>
    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        try
        {
            OperationMode = "Add Email Template";
            Flag = "Add";


            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Add_EmailTemplate.aspx?OperationMode=" + OperationMode.ToString() + "&Flag=" + Flag.ToString() +"');", true);


        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// This method is used to Edit Email Template
    /// </summary>
    protected void onUpdate(object source, CommandEventArgs e)
    {
        try
        {
        Flag  = "Edit";
        OperationMode = "Edit EmailTemplate";
        int command = Convert.ToInt32(e.CommandArgument.ToString());


       ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Add_EmailTemplate.aspx?OperationMode=" + OperationMode.ToString() + "&Flag=" + Flag.ToString() + "&command=" + command + "');", true);

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    /// <summary>
    /// This method is used to delete Email Template
    /// </summary>
    protected void onDelete(object source, CommandEventArgs e)
    {
        try
        {
        int retval = objBLLDept.PURC_Del_Email_Template(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindGrid();
                }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    /// <summary>
    /// This method is used to filter Email Template
    /// </summary>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
        BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// This method is used to refresh page
    /// </summary>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {

        ddlEmailfilter.SelectedIndex = 0;
        BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    /// <summary>
    /// This method is used to export all template Data
    /// </summary>
    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            DataTable dt = objBLLDept.Search_Email_Template_List(ddlEmailfilter.SelectedValue, sortbycoloumn, sortdirection
                        , null, null, ref  rowcount);

            string[] HeaderCaptions = { "ID", "Email Body", "Email Subject", "Email Type" };
            string[] DataColumnsName = { "ID", "Email_Body", "Email_Subject", "Email_Type" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "EmailTemplate", "Email Template", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void gvEmailTemplate_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void gvEmailTemplate_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {

            ViewState["SORTBYCOLOUMN"] = se.SortExpression;

            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;

            BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

}