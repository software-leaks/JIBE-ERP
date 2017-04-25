using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.FMS;
using SMS.Business.Infrastructure;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Configuration;
using SMS.Properties;
using SMS.Business.PURC;
using System.Text;

public partial class Purchase_PURC_Permissions : System.Web.UI.Page
{
    BLL_Infra_Company objCompBLL = new BLL_Infra_Company();
    BLL_FMS_Document objFMS = new BLL_FMS_Document();
    BLL_PURC_Permissions objPermsn = new BLL_PURC_Permissions();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_DepartmentList();
            ViewState["DDL_DS"] = objPermsn.Get_Dep_Cat_SubCat(Session["userid"].ToString());
            BindFunctionDropDownlist();
        }
    }
    public void BindFunctionDropDownlist()         //-- Bind Dropdownlist
    {
        ds = (DataSet)ViewState["DDL_DS"];
        ddl_Function.DataSource = ds.Tables[0];
        ddl_Function.DataTextField = "Text";
        ddl_Function.DataValueField = "Value";
        ddl_Function.DataBind();
    }
    //--Events
    /// <summary>
    /// On Function Check Get the users list from the selected Function
    /// </summary>
    protected void chklstDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLL_PURC_Permissions objPermsn = new BLL_PURC_Permissions();
            string dep = GetDepartmentIDList();
            string accessid = string.Empty;
            DataSet dsuser = objPermsn.PURC_GetDepartmentUsers(dep, accessid);
            chklstUser.Items.Clear();

            chklstUser.DataSource = dsuser.Tables[0];
            chklstUser.DataTextField = "UserName";
            chklstUser.DataValueField = "UserID";
            chklstUser.DataBind();
            chklstUser.Items.Insert(0, new ListItem("-ALL-", "0"));

            for (int i = 0; i < chklstUser.Items.Count; i++)
            {
                DataRow[] foundUser = dsuser.Tables[1].Select("UserID = '" + chklstUser.Items[i].Value + "'");
                if (foundUser.Length != 0)
                {
                    chklstUser.Items[i].Selected = true;
                }
            }
            if (rbtAccessType.SelectedValue == "Department")
            {
                TDUserlist.Visible = false;
            }
            else
            {
                //DataTable dt_tosave = new DataTable();
                //dt_tosave.Merge(dt_Functions());
                //dt_tosave.Merge(dt_Catalogue());
                //dt_tosave.Merge(dt_SubCatalogue());

                //TDUserlist.Visible = true;
                //DataTable dt = objPermsn.PURC_GET_PermitedUsers(dt_tosave, GetAccessUserDepList("Department"), rbtAccessType.SelectedValue.ToUpper()).Tables[0];
                //foreach (DataRow row in dt.Rows)
                //{
                //    if (row["UserID"].ToString() != "0")
                //    {
                //        foreach (ListItem item in chklstUser.Items)
                //        {
                //            if (row["UserID"].ToString() == item.Value.ToString())
                //            {

                //                item.Selected = true;
                //                break;

                //            }
                //        }
                //    }
                //}
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// set the Access type and AccessID
    /// ( if user do not selects the Function ,Cataloge and Subcatalogue then AccessType="Function" and AccessID=0;
    /// if user selects Function and don't select Cataloge and Subcatalogue then AccessType="Function" and AccessID=(function dropdown selected Value ,ie FunctionID);
    /// if User selects Function and Catalogue but don't select Subcatalogue then AccessType = "CATALOGUE"  and AccessID=(Catalogue dropdown selected Value , ie Catalogue);
    /// if User selects Function , Catalogue and Subcatalogue then AccessType = "SUBCATALOGUE"  and AccessID=(Subcatalogue dropdown selected Value , ie SubcatalogueId);) and saves the Permissions for the selected Users
    /// </summary>
    public void btnSaveE_Click(object sender, EventArgs e)                                                            // Save Permissions 
    {
        string msg = "";
        if (rbtAccessType.SelectedValue == "Department")
        {
            if (chklstDept.SelectedValue.Count() > 0)
            {
                string accesstype = "";
                string accessid = "";
                DataTable dt_tosave = new DataTable();
                dt_tosave.Merge(dt_Functions());
                dt_tosave.Merge(dt_Catalogue());
                dt_tosave.Merge(dt_SubCatalogue());
                BLL_PURC_Permissions sp = new BLL_PURC_Permissions();
                string departmentId = GetDepartmentIDList();
                sp.SavePermissions(dt_tosave, GetAccessUserDepList(rbtAccessType.SelectedValue), accessid, accesstype, GetSessionUserID(), GetDepartmentIDList(), "DEPARTMENT");
                msg = "alert('Permission Added Succesfully');";
            }
            else { msg = "alert('Please Select the Department');"; }
        }
        else
        {
            if (chklstUser.SelectedValue.Count() > 0)
            {
                string accesstype = "";
                string accessid = "";
                DataTable dt_tosave = new DataTable();
                dt_tosave.Merge(dt_Functions());
                dt_tosave.Merge(dt_Catalogue());
                dt_tosave.Merge(dt_SubCatalogue());
                BLL_PURC_Permissions sp = new BLL_PURC_Permissions();
                string departmentId = GetDepartmentIDList();
                sp.SavePermissions(dt_tosave, GetAccessUserDepList(rbtAccessType.SelectedValue), accessid, accesstype, GetSessionUserID(), GetDepartmentIDList(), "User");
                msg = "alert('Permission Added Succesfully');";
            }
            else
            {
                msg = "alert('Please Select the User');";
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
    }
    public DataTable dt_Functions()
    {
        DataTable dt = ddl_Function.SelectedValues;
        System.Data.DataColumn newColumn = new System.Data.DataColumn("Type", typeof(System.String));
        newColumn.DefaultValue = "FUNCTION";
        dt.Columns.Add(newColumn);
        return dt;
    }
    public DataTable dt_Catalogue()
    {
        DataTable dt = ddl_Catlog.SelectedValues;
        System.Data.DataColumn newColumn = new System.Data.DataColumn("Type", typeof(System.String));
        newColumn.DefaultValue = "CATALOGUE";
        dt.Columns.Add(newColumn);
        return dt;
    }
    public DataTable dt_SubCatalogue()
    {
        DataTable dt = ddl_SubCatlog.SelectedValues;
        System.Data.DataColumn newColumn = new System.Data.DataColumn("Type", typeof(System.String));
        newColumn.DefaultValue = "SUBCATALOGUE";
        dt.Columns.Add(newColumn);
        return dt;
    }
    public string GetDepartmentIDList()
    {
        StringBuilder depList = new StringBuilder();
        if (chklstDept.Items.Count > 0)
        {
            for (int i = 0; i < chklstDept.Items.Count; i++)
            {
                if (chklstDept.Items[i].Selected == true)
                {
                    depList.Append("'" + chklstDept.Items[i].Value + "',");
                }
            }
        }
        return depList.ToString().Remove(depList.ToString().Length - 1);
    }
    /// <summary>
    /// if user has Checked ALL then All the Check box in the list will get selected and Vise Versa 
    /// </summary>
    protected void chklstUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        string result = Request.Form["__EVENTTARGET"];
        string[] checkedBox = result.Split('$'); ;
        int index = int.Parse(checkedBox[checkedBox.Length - 1]);
        if (index == 0 && chklstUser.Items[index].Selected)
        {
            for (int i = 1; i < chklstUser.Items.Count; i++)
            {
                chklstUser.Items[i].Selected = true;
            }
        }
        else if (index == 0 && !chklstUser.Items[index].Selected)
        {
            for (int i = 1; i < chklstUser.Items.Count; i++)
            {
                chklstUser.Items[i].Selected = false;
            }
        }
        else if (index != 0 && !chklstUser.Items[index].Selected)
        {
            chklstUser.Items[0].Selected = false;
        }

        
    }
    public void UncheckCheckbox(CheckBoxList chk)
    {
        for (int i = 0; i < chk.Items.Count; i++)
        {
            chk.Items[i].Selected = false;
        }
    }

    /// <summary>
    /// On Function Selection Bind Catalogue Dropdownlist
    /// </summary>
    protected void ddl_FunctionChange()
    {
        if (ddl_Function.SelectedValues.Rows.Count > 0)
        {
            ds = (DataSet)ViewState["DDL_DS"];
            StringBuilder SBFunctions = new StringBuilder();
            foreach (DataRow dr in ddl_Function.SelectedValues.Rows)
            {
                SBFunctions.Append(dr[0]);
                SBFunctions.Append(",");
            }
            DataSet dt = objPermsn.PURC_GET_CAT_SUBCAT(SBFunctions.ToString().Remove(SBFunctions.Length - 1), "CATALOGUE");
            ddl_Catlog.DataSource = dt.Tables[0];
            ddl_Catlog.DataTextField = "Text";
            ddl_Catlog.DataValueField = "Value";
            ddl_Catlog.DataBind();
            UncheckCheckbox(chklstDept);
            chklstUser.Items.Clear();
            UncheckCheckbox(chklstDept);
            chklstUser.Items.Clear();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "Please select the Functions", true);
        }
    }
    /// <summary>
    /// On Catalogue Selection Bind Subcatalogue DropdownList
    /// </summary>
    protected void Cataloge_Change()
    {
        if (ddl_Catlog.SelectedValues.Rows.Count > 0)
        {
            ds = (DataSet)ViewState["DDL_DS"];
            StringBuilder SBCatalogue = new StringBuilder();
            foreach (DataRow dr in ddl_Catlog.SelectedValues.Rows)
            {
                SBCatalogue.Append("'" + dr[0] + "'");
                SBCatalogue.Append(",");
            }
            DataSet dt = objPermsn.PURC_GET_CAT_SUBCAT(SBCatalogue.ToString().Remove(SBCatalogue.Length - 1), "SUBCATALOGUE");
            ddl_SubCatlog.DataSource = dt.Tables[0];
            ddl_SubCatlog.DataTextField = "Text";
            ddl_SubCatlog.DataValueField = "Value";
            ddl_SubCatlog.DataBind();
            UncheckCheckbox(chklstDept);
            chklstUser.Items.Clear();
            UncheckCheckbox(chklstDept);
            chklstUser.Items.Clear();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "Please select the Catalogue", true);
        }
    }
    protected void OnsubCatlogChange()
    {
        if (ddl_SubCatlog.SelectedValues.Rows.Count > 0)
        {
            UncheckCheckbox(chklstDept);
            chklstUser.Items.Clear();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "Please select the SubCatalogue", true);
        }
    }


    //-- Functions
    public void BindDropDownlist(DropDownList dll_selected, DropDownList ddl_TOfill, int table, string CType)         //-- Bind Dropdownlist
    {
        DataTable dt = new DataTable();
        ds = (DataSet)ViewState["DDL_DS"];
        if (CType != "") { dt = ds.Tables[table]; dt.DefaultView.RowFilter = "Filter='" + dll_selected.SelectedItem.Value + "'"; }
        else { dt = ds.Tables[table]; }
        ddl_TOfill.DataSource = dt.DefaultView;
        ddl_TOfill.DataTextField = "Text";
        ddl_TOfill.DataValueField = "Value";
        ddl_TOfill.DataBind();
    }
    /// <summary>
    /// Bind Function CheckList
    /// </summary>
    protected void Load_DepartmentList()                                                                             //Bind Function CheckList
    {
        if (getSessionString("USERCOMPANYID") != "")
        {
            int iCompID = int.Parse(getSessionString("USERCOMPANYID"));
            DataTable dt = objCompBLL.Get_CompanyDepartmentList(iCompID);


            chklstDept.DataSource = dt;
            chklstDept.DataTextField = "VALUE";
            chklstDept.DataValueField = "ID";
            chklstDept.DataBind();
            chklstDept.Items.Insert(0, new ListItem("- ALL -", "0"));
        }
    }
    /// <summary>
    /// Bind The User CheckList
    /// </summary>
    protected void Load_UserList()                                                                                  // Bind User CheckList
    {
        int CompanyID = 0;
        if (UDFLib.ConvertIntegerToNull(Session["USERCOMPANYID"]) != null)
        {
            CompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"]);
        }
        BLL_Infra_UserCredentials objBllUser = new BLL_Infra_UserCredentials();
        DataTable dt = objBllUser.Get_UserList(CompanyID, "");
        chklstUser.DataSource = dt;
        chklstUser.DataBind();
        chklstUser.Items.Insert(0, new ListItem("- ALL -", "0"));
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Convert.ToString(Session["USERID"]));
        else
            return 0;
    }
    /// <summary>
    /// get the Approved UserList on Save Permissions Click
    /// </summary>
    public DataTable GetAccessUserDepList(string selectcheck)
    {
        DataTable dtaccess = new DataTable();
        DataRow draccess;
        dtaccess.Columns.Add("Empty", typeof(string));
        dtaccess.Columns.Add("ID", typeof(int));

        if (selectcheck == "Department")
        {
            if (chklstDept.Items.Count > 0)
            {
                for (int i = 0; i < chklstDept.Items.Count; i++)
                {
                    if (chklstDept.Items[i].Selected == true && chklstDept.Items[i].Value != "0")
                    {
                        draccess = dtaccess.NewRow();
                        draccess["ID"] = Convert.ToInt32(chklstDept.Items[i].Value);
                        draccess["Empty"] = "";
                        dtaccess.Rows.Add(draccess);
                    }
                }
            }
        }
        else
        {
            if (chklstUser.Items.Count > 0)
            {
                for (int i = 0; i < chklstUser.Items.Count; i++)
                {
                    if (chklstUser.Items[i].Selected == true && chklstUser.Items[i].Value != "0")
                    {
                        draccess = dtaccess.NewRow();
                        draccess["ID"] = Convert.ToInt32(chklstUser.Items[i].Value);
                        draccess["Empty"] = "";
                        dtaccess.Rows.Add(draccess);
                    }
                }
            }
        }
        return dtaccess;
    }
    protected string getSessionString(string ID)                                                                  // Get CompanyID
    {
        string ret = "";
        if (Session[ID] != null)
        {
            ret = Convert.ToString(Session[ID]);
        }
        else
        {
            Response.Redirect("~/Account/Login.aspx?ReturnURL=~/Infrastructure/Libraries/User.aspx");
        }
        return ret;
    }

    /// <summary>
    /// On Function Check Get the users list from the selected Function
    /// </summary>    
    private DataTable GetUsersOnDepartmentSelection(string FunctionID, string CatalogID, string SubCatalogueId)
    {
        DataSet ds = (DataSet)ViewState["DDL_DS"];
        return ds.Tables[3];
    }
    public void GetFolderAccess(CheckBoxList UserList)
    {
        DataTable dt = objFMS.GET_FolderUser(UDFLib.ConvertToInteger(Request.QueryString["id"]));
        foreach (DataRow row in dt.Rows)
        {
            if (row["UserID"].ToString() != "0")
            {
                foreach (ListItem item in UserList.Items)
                {
                    if (row["UserID"].ToString() == item.Value.ToString())
                    {
                        item.Selected = true;
                        break;
                    }
                }
                // if (row["UserID"].ToString() == "0")
                //  break;
            }
        }
    }

    protected void OnTypeChange(object sender, EventArgs e)
    {
        if (rbtAccessType.SelectedValue == "Department")
        {
            TDUserlist.Visible = false;
        }
        else { TDUserlist.Visible = true; }
        DataTable dt_tosave = new DataTable();
        dt_tosave.Merge(dt_Functions());
        dt_tosave.Merge(dt_Catalogue());
        dt_tosave.Merge(dt_SubCatalogue());

       // TDUserlist.Visible = true;
        DataTable dt = objPermsn.PURC_GET_PermitedUsers(dt_tosave, GetAccessUserDepList(rbtAccessType.SelectedValue), rbtAccessType.SelectedValue.ToUpper()).Tables[0];


        foreach (DataRow row in dt.Rows)
        {
            if (rbtAccessType.SelectedValue != "Department")
            {
                if (row["UserID"].ToString() != "0")
                {
                    foreach (ListItem item in chklstUser.Items)
                    {
                        if (row["UserID"].ToString() == item.Value.ToString())
                        {

                            item.Selected = true;
                            break;

                        }
                    }
                }
            }
            else {
                if (row["DepartmentId"].ToString() != "0")
                {
                    foreach (ListItem item in chklstDept.Items)
                    {
                        if (row["DepartmentId"].ToString() == item.Value.ToString())
                        {

                            item.Selected = true;
                            break;

                        }
                    }
                }
            
            }
        }
    }
}