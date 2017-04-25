using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using SMS.Business.DMS;
using SMS.Business.OCAAdmin;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Info_Folder : System.Web.UI.Page
{
    BLL_DMS_Admin objBLL = new BLL_DMS_Admin();
    BLL_Infobase objINFO = new BLL_Infobase();
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    protected int Folder_Id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        if (!IsPostBack)
        {
            Session["Folder_Id"] = null;
            //UserAccessValidation();
            Load_Department();
            LoadTableList();
            LoadQueryList();
            BindGrid();
        }
    }

    protected void LoadQueryList()
    {
        ddlListSource1.DataSource = objINFO.Get_SavedQuery("SP");
        ddlListSource1.DataTextField = "Display_Name";
        ddlListSource1.DataValueField = "Query_Name";
        ddlListSource1.DataBind();
        ddlListSource1.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void UserAccessValidation()
    {
        UserAccess objUA = new UserAccess();
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {

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
    public void Load_Department()
    {
        if (ddlDepartment.Items.Count == 0)
        {
            BLL_Infra_Company objCompany = new BLL_Infra_Company();
            DataTable dt = objCompany.Get_CompanyDepartmentList(UDFLib.ConvertToInteger(Session["USERCOMPANYID"]));

            ddlDepartment.DataSource = dt;
            ddlDepartment.DataTextField = "Value";
            ddlDepartment.DataValueField = "ID";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("-SELECT-", "0"));


            //To Load departmennt for add folder 

            ddlAddDepartment.DataSource = dt;
            ddlAddDepartment.DataTextField = "Value";
            ddlAddDepartment.DataValueField = "ID";
            ddlAddDepartment.DataBind();
            ddlAddDepartment.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
    }

    public void BindGrid()
    {
        try
        {

            int rowcount = ucCustomPager1.isCountRecord;
            //string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            //int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            int DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
            string SearchText = txtSearchText.Text;

            DataTable dt = objINFO.GET_Dept_FolderList(DepartmentId,SearchText, ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount);


            if (ucCustomPager1.isCountRecord == 1)
            {
                ucCustomPager1.CountTotalRec = rowcount.ToString();
                ucCustomPager1.BuildPager();
            }

            if (dt.Rows.Count > 0)
            {
                GridViewDocType.DataSource = dt;
                GridViewDocType.DataBind();
            }
        }
        catch { }
    }





    public void LoadTableList()
    {

        DataTable dt = objINFO.Get_SavedQuery("TB");

            ddlTableList.DataSource = dt;
            ddlTableList.DataTextField = "Display_Name";
            ddlTableList.DataValueField = "Query_Name";
            ddlTableList.DataBind();
            ddlTableList.Items.Insert(0, new ListItem("-SELECT-", "0"));


    }


    protected void btnSaveFolder_Click(object sender, EventArgs e)
    {
        SaveFolder(Folder_Id);
    }


    protected void SaveFolder(int Folder_Id)
    {

        try
        {
            if (Session["Folder_Id"] != null)
            {
                Folder_Id = UDFLib.ConvertToInteger(Session["Folder_Id"]);
           }

            int userId = GetSessionUserID();
            string  TableQueryId = ddlTableList.SelectedValue;
            string ListQueryID = ddlListSource1.SelectedValue; ;
            string Link_Value_Field = ddlList1Value.SelectedValue;
            string Link_Text_Field = ddlList1Text.SelectedValue;


            int result = objINFO.INS_Dept_Folder(Folder_Id,Convert.ToInt32(ddlAddDepartment.SelectedValue),txtFolderName.Text, ddlTableList.SelectedValue, TableQueryId,ListQueryID, Link_Value_Field, Link_Text_Field, txtLinkDisplay.Text, UDFLib.ConvertToInteger(Session["USERCOMPANYID"]), userId);
            string hidemodal = String.Format("hideModal('dvAddFolder')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

            BindGrid();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
    protected void GridViewDocType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DocTypeID = int.Parse(GridViewDocType.SelectedValue.ToString());

    }



    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        BindGrid();
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        ddlDepartment.SelectedValue = "0";
        txtSearchText.Text = "";
        BindGrid();
    }



    protected void ibtnDeleteFolder_Click(object source, CommandEventArgs e)
    {
        try
        {

            Folder_Id = Convert.ToInt32(e.CommandArgument.ToString());
            int deleted = objINFO.Delete_DocFolder(Folder_Id, GetSessionUserID());
            BindGrid();
        }
        catch { }

    }

    protected void ddlListSource1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlList1Value.Items.Clear();
            ddlList1Text.Items.Clear();
            string QueryName = ddlListSource1.SelectedValue;

            DataTable dtQuery = objINFO.Get_Query(QueryName);
            if (dtQuery.Rows.Count > 0)
            {
                string SQLString = dtQuery.Rows[0]["Command_SQL"].ToString();

                DataTable dtResultset = objINFO.Info_ExecuteQuery(SQLString);
                if (dtResultset.Rows.Count > 0)
                {
                    foreach (DataColumn dc in dtResultset.Columns)
                    {
                        ListItem item = new ListItem(dc.ColumnName, dc.ColumnName);
                        ddlList1Text.Items.Add(item);
                        ddlList1Value.Items.Add(item);
                        ddlList1Text.Visible = true;
                        ddlList1Value.Visible = true;
                        lblLinkValue.Visible = true;
                        lblLinkText.Visible = true;

                    }
                }

            }



        }
        catch (Exception ex)
        {

        }

    }

    #region --Folder Attributes-- 

    protected void ibtnAttribute_Click(object source, CommandEventArgs e)
    {
      
       

        try
        {
            ibtnAddAttribute.Visible = true;
            int index = ((GridViewRow)(((ImageButton)source).NamingContainer)).RowIndex;
            GridViewDocType.SelectedIndex = index;
            Folder_Id =Convert.ToInt32(e.CommandArgument.ToString());
            Session["Folder_Id"] = Folder_Id;
            BindAttribuite();
        }
        catch { }

    }

    protected void btnSaveAttribute_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtAttribute.Text.Trim().Equals(""))
            {
                string js = "Document Name is mandatory!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js + "');", true);
                return;
            }
            else
            {
                string strListSource = "";
                string Value_Field = "";
                string Text_Field = "";
                Folder_Id = Convert.ToInt32(Session["Folder_Id"].ToString());
                if (rdoLstAttributeDataType.SelectedValue == "LIST")
                {
                    strListSource = ddlListSource.SelectedValue;
                    Value_Field = ddlValuefield.SelectedValue;
                    Text_Field = ddlTextfield.SelectedValue;
                }

                int responseid = objINFO.InsertDocAttribute(Folder_Id, txtAttribute.Text.Trim(), rdoLstAttributeDataType.SelectedValue.ToString(), chkIsRequired.Checked, strListSource,Value_Field,Text_Field, GetSessionUserID());

                BindAttribuite();


                string js = "closeDivAddAttribute();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "close", js, true);


            }
        }
        catch (Exception ex)
        {
            string Err = ex.ToString();
        }

    }


    protected void rdoLstAttributeDataType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rdoLstAttributeDataType.SelectedValue == "LIST")
        {
            ddlListSource.Visible = true;
            lblListSource.Visible = true;

            ddlListSource.DataSource = objINFO.Get_SavedQuery("SP");
            ddlListSource.DataTextField = "Display_Name";
            ddlListSource.DataValueField = "Query_Name";
            ddlListSource.DataBind();
            ddlListSource.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
    }
     protected void ddlListSource_SelectedIndexChanged(object sender, EventArgs e)

    {
        try
        {
            ddlTextfield.Items.Clear();
            ddlValuefield.Items.Clear();
            string QueryName = ddlListSource.SelectedValue;

            DataTable dtQuery = objINFO.Get_Query(QueryName);
            if (dtQuery.Rows.Count > 0)
            {
                string SQLString = dtQuery.Rows[0]["Command_SQL"].ToString();

                DataTable dtResultset = objINFO.Info_ExecuteQuery(SQLString);
                if (dtResultset.Rows.Count > 0)
                {
                    foreach (DataColumn dc in dtResultset.Columns)
                    {
                        ListItem item = new ListItem(dc.ColumnName, dc.ColumnName);
                        ddlTextfield.Items.Add(item);
                        ddlValuefield.Items.Add(item);
                        ddlTextfield.Visible = true;
                        ddlValuefield.Visible = true;
                        lblData.Visible = true;
                        lblText.Visible = true;

                    }
                }

            }



        }
        catch(Exception ex)
        {

        }

    }
    

    protected void EditAttribute(object sender, GridViewEditEventArgs e)
    {
        GridViewAttributes.EditIndex = e.NewEditIndex;
        BindAttribuite();
    }
    protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewAttributes.EditIndex = -1;
        BindAttribuite();
    }
    protected void UpdateAttribute(object sender, GridViewUpdateEventArgs e)
    {
        int Atrribute_Id = Convert.ToInt32(GridViewAttributes.DataKeys[e.RowIndex].Values[0]);
        int Folder_Id = Convert.ToInt32(GridViewAttributes.DataKeys[e.RowIndex].Values[1]);
        bool IsRequired = ((CheckBox)GridViewAttributes.Rows[e.RowIndex].FindControl("chkRequired")).Checked;

        string Attribute = ((TextBox)GridViewAttributes.Rows[GridViewAttributes.EditIndex].FindControl("txtAttributName")).Text;
        int res = objINFO.Update_DocAttribute(Atrribute_Id,Attribute,IsRequired,GetSessionUserID());
        Session["Folder_Id"] = Folder_Id;
        GridViewAttributes.EditIndex = -1;
        BindAttribuite();
    }


    protected void ibtnDeleteAttribute_Click(object source, CommandEventArgs e)
    {
        try
        {
            int Attribute_Id =Convert.ToInt32(e.CommandArgument.ToString());
            int deleted = objINFO.Delete_DocAttribute(Attribute_Id,GetSessionUserID());
            BindAttribuite();
        }
        catch { }

    }


    protected void ibtnEdit_Click(object source, CommandEventArgs e)
    {
        Folder_Id = Convert.ToInt32(e.CommandArgument.ToString());
        Session["Folder_Id"] = Folder_Id;
        DataTable dt = objINFO.Get_AssignedAttributes(Folder_Id).Tables[1];
        if (dt.Rows.Count > 0)
        {
            try
            {
                txtFolderName.Text = dt.Rows[0]["FOLDER_NAME"].ToString();
                ddlAddDepartment.SelectedValue = dt.Rows[0]["Department_ID"].ToString();
                ddlTableList.SelectedValue = dt.Rows[0]["TableQuery"].ToString();
                txtLinkDisplay.Text = dt.Rows[0]["Link_DisplayName"].ToString();
                ddlListSource1.SelectedValue = dt.Rows[0]["List_Query"].ToString();
                ddlAddDepartment.Enabled = false;
            }
            catch(Exception ex)
            {
                string Error = ex.ToString();
            }
        }
        string show = String.Format("showDivAddFolder('dvAddFolder')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showmodal", show, true);

    }

    private void BindAttribuite()
    {
        DataTable dt = new DataTable();
        if (Session["Folder_Id"] != null)
        {
            Folder_Id = Convert.ToInt32(Session["Folder_Id"].ToString());

            dt = objINFO.Get_AssignedAttributes(Folder_Id).Tables[0];

            GridViewAttributes.DataSource = dt;
            GridViewAttributes.DataBind();
           // Session["Folder_Id"] = null;
        }
    }

    #endregion

}