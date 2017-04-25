using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PortageBill;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;
using SMS.Business.Infrastructure;

public partial class SalaryStructure : System.Web.UI.Page
{
    BLL_PB_Admin objBLL = new BLL_PB_Admin();
    UserAccess objUA = new UserAccess();
    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    public Boolean uaAddEntry = true;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 100;
            BindParentList();
            BindSalaryStructure();
            BindddKeyValue();
        }

    }


    public void BindParentList()
    {

        
        DataTable dt = objBLL.Get_SalaryStructureParentList();

        DDLParentList.Items.Clear();
        DDLParentList.DataSource = dt;
        DDLParentList.DataTextField = "Name";
        DDLParentList.DataValueField = "Code";
        DDLParentList.DataBind();
        DDLParentList.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }

    public void BindddKeyValue()
    {
        DataTable dtkey = objBLL.Get_KeyValueList();
        ddlkeyvalue.Items.Clear();
        ddlkeyvalue.DataSource = dtkey;
        ddlkeyvalue.DataTextField = "Key_Value";
        ddlkeyvalue.DataValueField = "Key_Value";
        ddlkeyvalue.DataBind();
        ddlkeyvalue.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    public void BindSalaryStructure()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataSet ds = objBLL.SearchSalaryStructure(null, null, sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount, ddlParetType.SelectedIndex==0?null:UDFLib.ConvertIntegerToNull(ddlParetType.SelectedValue));


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (ds.Tables[0].Rows.Count > 0)
        {
            gvSalaryStructure.DataSource = ds.Tables[0];
            gvSalaryStructure.DataBind();
        }
        else
        {
            gvSalaryStructure.DataSource = ds.Tables[0];
            gvSalaryStructure.DataBind();
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
        if (objUA.Edit == 1) uaEditFlag = true; else btnsave.Visible = false;
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
        chkAutoPopulate.Checked = false;
        chkVesselSpecific.Checked = false;
        this.SetFocus("ctl00_MainContent_txtCountry");
        HiddenFlag.Value = "Add";

        OperationMode = "Add Salary Structure";
        ddlSalaryType.Items.Clear();
        ddlPaybleAt.Items.Clear();
        txtCode.Text = "";
        txtParentCode.Text = "";
        DDLParentList.SelectedValue = "0";
        ddlkeyvalue.SelectedValue = "0";
        DataTable dtSalaryTypeANDPayble = new DataTable();
        dtSalaryTypeANDPayble = objBLL.Get_SalaryTypeList(0);
        if (dtSalaryTypeANDPayble.Rows.Count != 0)
        {
            ddlSalaryType.DataSource = dtSalaryTypeANDPayble;
            ddlPaybleAt.DataSource = dtSalaryTypeANDPayble;
            ddlSalaryType.DataTextField = "Name";
            ddlPaybleAt.DataTextField = "Name";
            ddlSalaryType.DataValueField = "Code";
            ddlPaybleAt.DataValueField = "Code";
            ddlSalaryType.DataBind();
            ddlPaybleAt.DataBind();
            ddlSalaryType.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlPaybleAt.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            ddlSalaryType.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlPaybleAt.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        txtName.Text = "";
        txtDescription.Text = "";
        txtSortOrder.Text = "";
        string AddSalStrmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSalStrmodal", AddSalStrmodal, true);
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Error = "";
        int vesselspecific = 0;
        int autopopulate = 0;
        if (chkVesselSpecific.Checked == true)
        {
            vesselspecific = 1;
        }
        if (chkAutoPopulate.Checked == true)
        {
            autopopulate = 1;
        }
        if (HiddenFlag.Value == "Add")
        {   
            int retval = objBLL.InsertSalaryStructure(UDFLib.ConvertIntegerToNull(DDLParentList.SelectedValue), txtName.Text.Trim(), txtDescription.Text.Trim(), UDFLib.ConvertStringToNull(ddlkeyvalue.SelectedValue), UDFLib.ConvertIntegerToNull(ddlSalaryType.SelectedValue),
            UDFLib.ConvertIntegerToNull(txtSortOrder.Text.Trim()), null, Convert.ToInt32(Session["USERID"]), autopopulate, vesselspecific, UDFLib.ConvertIntegerToNull(ddlPaybleAt.SelectedValue));

            if (retval > 0)
            {
                string js =txtName.Text.Trim()+ " is Added Successfully";
                string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

            }
            else 
            {
                string js = txtName.Text.Trim() + " is Already Exists";
                string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            }
        
        }
        else
        {
            int retval = objBLL.EditSalaryStructure(Convert.ToInt32(txtCode.Text.Trim()), UDFLib.ConvertIntegerToNull(DDLParentList.SelectedValue), txtName.Text.Trim(), txtDescription.Text.Trim(), UDFLib.ConvertStringToNull(ddlkeyvalue.SelectedValue), UDFLib.ConvertIntegerToNull(ddlSalaryType.SelectedValue),
                UDFLib.ConvertIntegerToNull(txtSortOrder.Text.Trim()), null, Convert.ToInt32(Session["USERID"]), autopopulate, vesselspecific, UDFLib.ConvertIntegerToNull(ddlPaybleAt.SelectedValue));
            if (retval > 0)
            {
                string js = txtName.Text.Trim() + " is Updated Successfully";
                string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

            }
            else
            {
                string js = txtName.Text.Trim() + " is Not Updated";
                string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            }
        }
        BindddKeyValue();
        BindParentList();
        BindSalaryStructure();


        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }




    protected void onUpdate(object source, CommandEventArgs e)
    {
        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Salary Structure";
        ddlSalaryType.Items.Clear();
        ddlPaybleAt.Items.Clear();
        DataTable dt = new DataTable();
        DataTable dtSalaryType=new DataTable();
        dtSalaryType = objBLL.Get_SalaryTypeList(0);
        dt = objBLL.Get_SalaryStructureList(Convert.ToInt32(e.CommandArgument.ToString().Split(';')[0]));
       
        txtCode.Text = dt.Rows[0]["Code"].ToString();
        txtParentCode.Text = dt.Rows[0]["Parent_Code"].ToString();
        DDLParentList.SelectedValue = dt.Rows[0]["Parent_Code"].ToString() != "" ? dt.Rows[0]["Parent_Code"].ToString() : "0"; 
        txtName.Text = dt.Rows[0]["Name"].ToString();
        txtDescription.Text = dt.DefaultView[0]["Description"].ToString();
        ddlkeyvalue.SelectedValue = dt.Rows[0]["Key_Value"].ToString() != "" ? dt.Rows[0]["Key_Value"].ToString() : "0"; 
        ddlSalaryType.SelectedValue = dt.Rows[0]["Salary_Type"].ToString() != "" ? dt.Rows[0]["Salary_Type"].ToString() : "0";
        ddlPaybleAt.SelectedValue = dt.Rows[0]["Payable_At"].ToString() != "" ? dt.Rows[0]["Payable_At"].ToString() : "0";

        txtSortOrder.Text = dt.Rows[0]["Sort_Order"].ToString();
        if (dtSalaryType.Rows.Count!=0)
        {
            try
            {   ddlSalaryType.DataSource = dtSalaryType;              
                ddlSalaryType.DataTextField = "Name";           
                ddlSalaryType.DataValueField = "Code";             
                ddlSalaryType.DataBind();               
                ddlSalaryType.Items.Insert(0, new ListItem("--Select--", "0"));
              
            }
            catch (Exception)
            {

                ddlSalaryType.Items.Insert(0, new ListItem("--Select--", "0"));
             
            }

            try
            {              
                ddlPaybleAt.DataSource = dtSalaryType;              
                ddlPaybleAt.DataTextField = "Name";              
                ddlPaybleAt.DataValueField = "Code";               
                ddlPaybleAt.DataBind();               
                ddlPaybleAt.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception)
            {
                ddlPaybleAt.Items.Insert(0, new ListItem("--Select--", "0"));
            }
           
        }
        else
        {          
            ddlSalaryType.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlPaybleAt.Items.Insert(0, new ListItem("--Select--", "0"));
        }
      
        chkVesselSpecific.Checked = dt.Rows[0]["Vessel_Specific"].ToString() != "0" ? true : false;
        chkAutoPopulate.Checked = dt.Rows[0]["Auto_Populate"].ToString() != "0" ? true : false;
        hdfRowIndex.Value = e.CommandArgument.ToString().Split(';')[0];

        string editSalStrmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editSalStrmodal", editSalStrmodal, true);



    }


    protected void onDelete(object source, CommandEventArgs e)
    {
        int retval = objBLL.DeleteSalaryStructure(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindSalaryStructure();
        BindddKeyValue();
        BindParentList();

    }


    protected void gvSalaryStructure_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "MoveUp")
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            int Code = UDFLib.ConvertToInteger(arg[0]);
            int Parent_Code = UDFLib.ConvertToInteger(arg[1]);
            objBLL.Swap_Sort_Order(Code,Parent_Code, -1, Convert.ToInt32(Session["UserID"].ToString()));
            BindSalaryStructure();
        }
        else if (e.CommandName == "MoveDown")
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            int Code = UDFLib.ConvertToInteger(arg[0]);
            int Parent_Code = UDFLib.ConvertToInteger(arg[1]);
            objBLL.Swap_Sort_Order(Code, Parent_Code, 1, Convert.ToInt32(Session["UserID"].ToString()));      
            BindSalaryStructure();
        }


    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        // Refresh Should display All results. ddlparenttype selection should be 0
        ddlParetType.SelectedValue = "";
        ddlParetType.SelectedIndex = 0;
        BindSalaryStructure();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindSalaryStructure();
    }



    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {


        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataSet ds = objBLL.SearchSalaryStructure(null, null, sortbycoloumn, sortdirection
             , null, null, ref  rowcount, ddlParetType.SelectedIndex == 0 ? null : UDFLib.ConvertIntegerToNull(ddlParetType.SelectedValue));
        

        string[] HeaderCaptions = { "Parent Type ", "Name", "Description", "Payable At", "Key_Value", "Vessel Specific", "Auto Populate" };
        string[] DataColumnsName = { "Component_Type", "Name", "Description", "SalaryTypeDetails", "Key_Value", "Vessel_Specific", "Auto_Populate" };

        GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "SalaryStructure", "Salary Structure", "");

    }

    protected void gvSalaryStructure_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    if (ViewState["SORTBYCOLOUMN"] != null)
        //    {
        //        HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
        //        if (img != null)
        //        {
        //            if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
        //                img.Src = "~/purchase/Image/arrowUp.png";
        //            else
        //                img.Src = "~/purchase/Image/arrowDown.png";

        //            img.Visible = true;
        //        }
        //    }
        //}

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
        //    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
        //}



        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //    if (e.Row.DataItem == null) return;

        //    GridView GridViewDetails = (GridView)e.Row.FindControl("GridViewDetails");
        //    int rowcount = ucCustomPagerItems.isCountRecord;

        //    Label lblCode = (Label)e.Row.FindControl("lblCode");
           
        //    GridViewDetails.DataSource = objBLL.SearchSalaryStructure((UDFLib.ConvertIntegerToNull(lblCode.Text)), txtfilter.Text != "" ? txtfilter.Text : null, null, null
        //     , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount).Tables[1];


        //    GridViewDetails.DataBind();
        //}




    }


    protected void gvSalaryStructure_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindSalaryStructure();
    }
}