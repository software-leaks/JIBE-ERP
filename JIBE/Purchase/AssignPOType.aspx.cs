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

public partial class Purchase_AssignPOType : System.Web.UI.Page
{
    string DeptID;
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    BLL_PURC_Purchase objBLLDept = new BLL_PURC_Purchase();
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

            FillDropDownlist();
            BindrgdDept();
            
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

        if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            ImgSave.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;


    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public void FillDropDownlist()
    {

        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            
            DataSet ds = objTechService.GetPOType();

            
            ddlFilterType.DataSource = ds.Tables[0];
            ddlFilterType.DataTextField = "Short_Code";
            ddlFilterType.DataValueField = "Code";
            ddlFilterType.DataBind();
            ddlFilterType.Items.Insert(0, new ListItem("-All-", "0"));


            ddlReqType.DataSource = ds.Tables[0];
            ddlReqType.DataTextField = "Short_Code";
            ddlReqType.DataValueField = "Code";
            ddlReqType.DataBind();
            ddlReqType.Items.Insert(0, new ListItem("-Select-", "0"));

            ddlBudgetCode.DataSource = ds.Tables[1];
            ddlBudgetCode.DataTextField = "Budget_Name";
            ddlBudgetCode.DataValueField = "Budget_Code";
            ddlBudgetCode.DataBind();
            ddlBudgetCode.Items.Insert(0, new ListItem("-Select-", "0"));

            ddlPOType.DataSource = ds.Tables[2];
            ddlPOType.DataTextField = "PO_Type";
            ddlPOType.DataValueField = "variable_code";
            ddlPOType.DataBind();
            ddlPOType.Items.Insert(0, new ListItem("-Select-", "0"));

            ddlAccType.DataSource = ds.Tables[3];
            ddlAccType.DataTextField = "Account_Type";
            ddlAccType.DataValueField = "variable_code";
            ddlAccType.DataBind();
            ddlAccType.Items.Insert(0, new ListItem("-Select-", "0"));
        }
    }

    public void BindrgdDept()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

       


        DataTable dt = objBLLDept.POType_Search(UDFLib.ConvertIntegerToNull(ddlFilterType.SelectedValue), sortbycoloumn, sortdirection
        , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            rgdDept.DataSource = dt;
            rgdDept.DataBind();
        }
        else
        {
            rgdDept.DataSource = dt;
            rgdDept.DataBind();
        }


    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        this.SetFocus("ctl00_MainContent_ddlReqType");
        HiddenFlag.Value = "Add";
        OperationMode = "Add PO Type";

        ddlReqType.SelectedValue = "0";
        ddlBudgetCode.SelectedValue = "0";
        ddlPOType.SelectedValue = "0";
        ddlAccType.SelectedValue = "0";

        string AddDeptmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPOType", AddDeptmodal, true);
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {


        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {

            DepartmentData objDeptDo = new DepartmentData();
            HiddenFlag.Value = "Edit";

            OperationMode = "Edit PO Type";

            DataTable dtDept = new DataTable();
            dtDept = objTechService.Get_Req_POType(UDFLib.ConvertIntegerToNull(e.CommandArgument.ToString()));
            dtDept.DefaultView.RowFilter = "Id= '" + e.CommandArgument.ToString() + "'";

            ddlReqType.SelectedValue = dtDept.DefaultView[0]["Reqsn_Type"].ToString();
            ddlPOType.SelectedValue = dtDept.DefaultView[0]["PO_Type"].ToString();
            ddlBudgetCode.SelectedValue = dtDept.DefaultView[0]["Budget_Code"].ToString();
            ddlAccType.SelectedValue = dtDept.DefaultView[0]["Account_Type"].ToString();
            txtBudgetID.Text = dtDept.DefaultView[0]["ID"].ToString();
            objDeptDo.CurrentUser = Session["userid"].ToString();



            string Deptmodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "POType", Deptmodal, true);

        }

    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DeptID = e.CommandArgument.ToString();
            DepartmentData objDeptDo = new DepartmentData();
            //objDeptDo.DeptID = e.CommandArgument.ToString();
            objDeptDo.CurrentUser = Session["userid"].ToString();
            int count = objTechService.DeletePOType(UDFLib.ConvertIntegerToNull(e.CommandArgument.ToString()), GetSessionUserID());

            BindrgdDept();
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {

        ddlFilterType.SelectedIndex = 0;
        BindrgdDept();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindrgdDept();
    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLLDept.POType_Search(UDFLib.ConvertIntegerToNull(ddlFilterType.SelectedValue), sortbycoloumn, sortdirection
        , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        string[] HeaderCaptions = { "Name", "Code", "Form Type", "A/c Classification Code" };
        string[] DataColumnsName = { "name_dept", "code", "Form_Type", "Ac_Classi_Code" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Department", "Department", "");

    }



    protected void Save_Click(object sender, EventArgs e)
    {
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DepartmentData objDeptDo = new DepartmentData();

            //objDeptDo.DeptID = txtDeptID.Text;
            //objDeptDo.Dept_code = txtCode.Text;
            //objDeptDo.Dept_name = TxtDept.Text;
            //objDeptDo.FormType = cmbFType.SelectedValue;
            //objDeptDo.CurrentUser = Session["userid"].ToString();
            //objDeptDo.Ac_Clssification_Code = ddlAcClassification.SelectedValue;
            objDeptDo.Vessel_Code = "0";
            if (HiddenFlag.Value == "Add")
            {
                //int count = objTechService.SaveDepartment(objDeptDo);
                int count = objBLLDept.SavePOType(UDFLib.ConvertIntegerToNull(ddlReqType.SelectedValue),UDFLib.ConvertIntegerToNull(ddlBudgetCode.SelectedValue),
                    ddlPOType.SelectedValue,ddlAccType.SelectedValue,GetSessionUserID());
            }
            else
            {
                 int count = objBLLDept.UpdatePOType(UDFLib.ConvertIntegerToNull(txtBudgetID.Text),  UDFLib.ConvertIntegerToNull(ddlReqType.SelectedValue),UDFLib.ConvertIntegerToNull(ddlBudgetCode.SelectedValue),
                    ddlPOType.SelectedValue, ddlAccType.SelectedValue, GetSessionUserID());
            }

            BindrgdDept();

            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
    }

    protected void rgdDept_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void rgdDept_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void rgdDept_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindrgdDept();

    }

    protected string PreventUnlistedValueError(DropDownList li, string val)
    {
        if (li.Items.FindByValue(val) == null)
        {

            li.SelectedValue = "0";
            val = "0";

        }
        return val;
    }
}