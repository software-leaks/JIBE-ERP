using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.Survey;
using SMS.Properties;
using System.Web.UI.HtmlControls;

public partial class Surveys_SurveyDocumentType : System.Web.UI.Page
{
    BLL_SURV_Survey objBLL = new BLL_SURV_Survey();
    UserAccess objUA = new UserAccess();

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (MainDiv.Visible)
        {
            if (!IsPostBack)
            {
                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                ucCustomPagerItems.PageSize = 20;

                BindSurveyDocType();
            }
        }
    }

    public void BindSurveyDocType()
    {
        try
        {


            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            DataTable dt = objBLL.Get_Survey_Document_Type_Search(txtfilter.Text != "" ? txtfilter.Text.Trim() : null, sortbycoloumn, sortdirection
                 , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            GridView_DocumentType.DataSource = dt;
            GridView_DocumentType.DataBind();

            if (dt.Rows.Count > 0)
                ImgExpExcel.Visible = true;
            else
                ImgExpExcel.Visible = false;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            MainDiv.Visible = false;
            AccessMsgDiv.Visible = true;
        }
        else
        {
            MainDiv.Visible = true;
            AccessMsgDiv.Visible = false;
        }

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

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtDocumentType");
        HiddenFlag.Value = "Add";
        OperationMode = "Add Document Type";
        ViewState["OperationMode"] = "Add Document Type";
        ClearField();
        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }

    protected void ClearField()
    {
        txtDocTypeID.Text = "";
        txtDocumentType.Text = "";
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            int Id = 0; string EditDocType = "";

            if (txtDocumentType.Text.Trim() == "")
            {
                if (ViewState["OperationMode"] != null)
                    OperationMode = ViewState["OperationMode"].ToString();
                EditDocType = String.Format("alert('Document Type is mandatory');showModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditDocumentTypemodal", EditDocType, true);
            }

            if (EditDocType == "")
            {
                int responseid;
                if (HiddenFlag.Value == "Add")
                {
                    responseid = objBLL.INSERT_Document_Type(Id, txtDocumentType.Text.Trim(), GetSessionUserID());
                }
                else
                {
                    responseid = objBLL.UPDATE_Document_Type(Convert.ToInt32(txtDocTypeID.Text.Trim()), txtDocumentType.Text.Trim(), GetSessionUserID());
                }
                if (responseid == -1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddEditDocumentTypemodal", String.Format("alert('Document Type is already exists.');showModal('divadd',false);"), true);
                    return;
                }
                txtfilter.Text = "";
                BindSurveyDocType();
                string hidemodal = String.Format("hideModal('divadd')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {
        try
        {
            HiddenFlag.Value = "Edit";
            OperationMode = "Edit Document Type";
            ViewState["OperationMode"] = "Document Type";
            DataTable dt = new DataTable();
            dt = objBLL.Get_Survey_Document_Type(Convert.ToInt32(e.CommandArgument.ToString()));

            txtDocTypeID.Text = dt.Rows[0]["ID"].ToString();
            txtDocumentType.Text = dt.Rows[0]["DocumentType"].ToString();

            string EditDocTypemodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "EditDocumentTypemodal", EditDocTypemodal, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DELETE_Document_Type(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));

        BindSurveyDocType();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindSurveyDocType();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        BindSurveyDocType();

    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            DataTable dt = objBLL.Get_Survey_Document_Type_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
                 , null, null, ref  rowcount);


            string[] HeaderCaptions = { "Document Type" };
            string[] DataColumnsName = { "DocumentType" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Survey Document Type", "Survey Document Type", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void GridView_DocumentType_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void GridView_DocumentType_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindSurveyDocType();
    }
}