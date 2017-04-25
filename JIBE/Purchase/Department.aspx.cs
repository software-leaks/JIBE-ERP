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
public partial class Department : System.Web.UI.Page
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


            BindrgdDept();
            FillDDLFormType();
            FillDDLAcClassification();
            FillApprovalGroup();
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
    /// <summary>
    /// Fill Form  Type
    /// </summary>
    public void FillDDLFormType()
    {

        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            cmbFType.DataSource = objTechService.SelectDepartmentType();
            cmbFType.DataTextField = "Form_Type";
            cmbFType.DataValueField = "Form_Type";

        }

    }
    /// <summary>
    /// Fill Approval Group 
    /// </summary>
    private void FillApprovalGroup()
    {
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        ddlApprovalGroup.DataSource =objTechService.GetApprovalCode();
        ddlApprovalGroup.DataTextField = "Approval_Group_Name";
        ddlApprovalGroup.DataValueField = "Approval_Group_Code";
        ddlApprovalGroup.DataBind();
    }
    /// <summary>
    /// Fill Account Classification
    /// </summary>
    public void FillDDLAcClassification()
    {

        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {

            ddlAcClassification.DataSource = objTechService.GetAccountClassification();
            ddlAcClassification.DataTextField = "Ac_Classification";
            ddlAcClassification.DataValueField = "variable_code";
            ddlAcClassification.DataBind();
        }
    }

    /// <summary>
    /// Bind Department Dropdown
    /// </summary>
    public void BindrgdDept()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());




        DataTable dt = objBLLDept.Department_Search(txtSearchName.Text != "" ? txtSearchName.Text : null, UDFLib.ConvertStringToNull(ddlFormType.SelectedValue),null, sortbycoloumn, sortdirection
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
    /// <summary>
    /// Add New Department event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        try
        {
            this.SetFocus("ctl00_MainContent_TxtDept");
            HiddenFlag.Value = "Add";
            OperationMode = "Add Department";


            txtCode.Enabled = true;
            cmbFType.Enabled = true;
            ddlAcClassification.Enabled = true;
            ddlApprovalGroup.Enabled = true;

            txtCode.Text = "";
            TxtDept.Text = "";
            cmbFType.SelectedIndex = -1;
            ddlAcClassification.SelectedIndex = 0;
            ddlApprovalGroup.SelectedIndex = 0;
            string AddDeptmodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddDeptmodal", AddDeptmodal, true);
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Gridview Edit Button Event.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void onUpdate(object source, CommandEventArgs e)
    {
        try
        {

            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {

                DepartmentData objDeptDo = new DepartmentData();
                HiddenFlag.Value = "Edit";

                OperationMode = "Edit Department";

                DeptID = e.CommandArgument.ToString();
                DataTable dtDept = new DataTable();
                dtDept = objTechService.GetDepartmentMaster();
                dtDept.DefaultView.RowFilter = "Id= '" + e.CommandArgument.ToString() + "'";
                txtDeptID.Text = dtDept.DefaultView[0]["ID"].ToString();
                TxtDept.Text = dtDept.DefaultView[0]["Name_Dept"].ToString();
                txtCode.Text = dtDept.DefaultView[0]["Code"].ToString();
                //ddlAcClassification.SelectedValue = dtDept.DefaultView[0]["Ac_Classi_Code"].ToString() != "" ? dtDept.DefaultView[0]["Ac_Classi_Code"].ToString() : "0";
                //JIT_3675_Pranali16072015_
                ddlAcClassification.SelectedValue = PreventUnlistedValueError(ddlAcClassification, Convert.ToString(dtDept.DefaultView[0]["Ac_Classi_Code"]));
                ddlApprovalGroup.SelectedValue = PreventUnlistedValueError(ddlApprovalGroup, Convert.ToString(dtDept.DefaultView[0]["Approval_Group_Code"]));
                cmbFType.ClearSelection();


                if (dtDept.DefaultView[0]["Form_Type"].ToString() == "Stores".ToUpper())
                {
                    cmbFType.SelectedValue = "ST";

                }

                if (dtDept.DefaultView[0]["Form_Type"].ToString() == "Spares".ToUpper())
                {
                    cmbFType.SelectedValue = "SP";

                }

                if (dtDept.DefaultView[0]["Form_Type"].ToString() == "repairs".ToUpper())
                {
                    cmbFType.SelectedValue = "RP";

                }

                objDeptDo.CurrentUser = Session["userid"].ToString();

                txtCode.Enabled = false;
                cmbFType.Enabled = false;
                ddlAcClassification.Enabled = false;
                ddlApprovalGroup.Enabled = false;

                string Deptmodal = String.Format("showModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Deptmodal", Deptmodal, true);

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    /// <summary>
    /// Gridview Delete Button Event.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void onDelete(object source, CommandEventArgs e)
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DeptID = e.CommandArgument.ToString();
                DepartmentData objDeptDo = new DepartmentData();
                objDeptDo.DeptID = e.CommandArgument.ToString();
                objDeptDo.CurrentUser = Session["userid"].ToString();
                int count = objTechService.DeleteDepartment(objDeptDo);

                BindrgdDept();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Gridview Refresh Button Event.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {

            ddlFormType.SelectedIndex = 0;
            txtSearchName.Text = "";
            BindrgdDept();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    /// <summary>
    /// Gridview Delete Button Event.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            BindrgdDept();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        try
        {


            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = objBLLDept.Department_Search(txtSearchName.Text != "" ? txtSearchName.Text : null, UDFLib.ConvertStringToNull(ddlFormType.SelectedValue), null, sortbycoloumn, sortdirection
              , null, null, ref  rowcount);

            string[] HeaderCaptions = { "Name", "Code", "Form Type", "A/c Classification Code", "Approval Group" };
            string[] DataColumnsName = { "name_dept", "code", "Form_Type", "Ac_Classi_Code", "Approval_Group_Name" };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Department", "Department", "");
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    /// <summary>
    /// To Save Department 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void Save_Click(object sender, EventArgs e)
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DepartmentData objDeptDo = new DepartmentData();

                objDeptDo.DeptID = txtDeptID.Text;
                objDeptDo.Dept_code = txtCode.Text;
                objDeptDo.Dept_name = TxtDept.Text;
                objDeptDo.FormType = cmbFType.SelectedValue;
                objDeptDo.CurrentUser = Session["userid"].ToString();
                objDeptDo.Ac_Clssification_Code = ddlAcClassification.SelectedValue;
                objDeptDo.Vessel_Code = "0";
                objDeptDo.Approval_Group_Code = ddlApprovalGroup.SelectedValue;
                if (HiddenFlag.Value == "Add")
                {
                    int count = objTechService.SaveDepartment(objDeptDo);
                }
                else
                {
                    int count = objTechService.EditDepartment(objDeptDo);
                }

                BindrgdDept();

                string hidemodal = String.Format("hideModal('divadd')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void rgdDept_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void rgdDept_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void rgdDept_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;

            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;

            BindrgdDept();
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    /// <summary>
    /// Following method to handled runtime exception if dropdown doesnt have saved department id.
    /// </summary>
    /// <param name="li">Dropdoen id to check </param>
    /// <param name="val">to Check value exists or not.</param>
    /// <returns>retruns 0 if value does not exists.</returns>
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
