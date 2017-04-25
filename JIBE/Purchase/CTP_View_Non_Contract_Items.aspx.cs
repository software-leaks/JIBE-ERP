using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using System.Data;
using System.Data;




public partial class Purchase_CTP_View_Non_Contract_Items : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            txtReqsnDTFrom.Text = DateTime.Now.AddDays(-90).ToString("dd/MM/yyyy");
            btnAddItemToExistingContract.Enabled = false;
            btnAddToNewContract.Enabled = false;
            btnSaveToExistingCtp.Enabled = false;

            DataTable dtSelected = new DataTable();
            DataColumn column = new DataColumn("id", typeof(string));
            dtSelected.Columns.Add(column);
            dtSelected.PrimaryKey = new DataColumn[] { column };
            ViewState["vsdtSelected_Items"] = dtSelected;

            bindDeptCatalogue();
        }
        lblmsg.Text = "";
    }
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx");

        if (objUA.Add == 0)
        {

            btnAddItemToExistingContract.Visible = false;
            btnAddToNewContract.Visible = false;

        }
        if (objUA.Edit == 0)
        {
            btnAddItemToExistingContract.Visible = false;
            btnAddToNewContract.Visible = false;

        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {

            // You don't have sufficient previlege to access the requested page.
        }


    }
    protected void bindDeptCatalogue()
    {
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DataTable DeptDt = objTechService.GetDeptType();
            optList.DataSource = DeptDt;
            optList.DataTextField = "Description";
            optList.DataValueField = "Short_Code";
            optList.DataBind();
            optList.SelectedIndex = 0;

            DataTable dtDepartment = new DataTable();
            dtDepartment = objTechService.SelectDepartment();
            cmbDept.Items.Clear();
            cmbDept.DataTextField = "Name_Dept";
            cmbDept.DataValueField = "ID";
            cmbDept.DataSource = dtDepartment;
            cmbDept.DataBind();
            ListItem li = new ListItem("SELECT", "0");
            cmbDept.Items.Insert(0, li);

        }
    }

    protected void optList_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {

            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtDepartment = new DataTable();
                dtDepartment = objTechService.SelectDepartment();

                if (optList.SelectedItem.Text == "Spares")
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";


                }
                else if (optList.SelectedItem.Text == "Stores")
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";



                }
                else if (optList.SelectedItem.Text == "Repairs")
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";


                }
                cmbDept.Items.Clear();
                cmbDept.DataTextField = "Name_Dept";
                cmbDept.DataValueField = "ID";
                cmbDept.DataSource = dtDepartment;
                cmbDept.DataBind();
                ListItem li = new ListItem("SELECT ", "0");
                cmbDept.Items.Insert(0, li);

            }




        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }

    protected void cmbDept_OnSelectedIndexChanged(object source, System.EventArgs e)
    {
        try
        {
            string filter;

            ((DataTable)ViewState["vsdtSelected_Items"]).Clear();

            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtCatalog = objTechService.SelectCatalog();



                string department = cmbDept.SelectedValue.ToString();

                filter = "1=1";

                if (department != "0")
                    filter += "  and  id='" + department + "'";


                dtCatalog.DefaultView.RowFilter = filter;

                if (dtCatalog.DefaultView.Count > 0)
                {
                    ddlCatalogue.Items.Clear();
                    ddlCatalogue.DataTextField = "system_description";
                    ddlCatalogue.DataValueField = "system_code";
                    ddlCatalogue.DataSource = dtCatalog.DefaultView.ToTable();
                    ddlCatalogue.DataBind();
                    ListItem li = new ListItem("SELECT", "0");
                    ddlCatalogue.Items.Insert(0, li);

                }
                else
                {

                }


            }
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }

    }

    protected void btnSearch_Click(object s, EventArgs e)
    {
        BindItems();
        btnAddItemToExistingContract.Enabled = true;
        btnAddToNewContract.Enabled = true;
        
    }
    protected void btnClearFilter_Click(object s, EventArgs e)
    {
        btnAddItemToExistingContract.Enabled = false;
        btnAddToNewContract.Enabled = false;
        txtReqsnDTFrom.Text = "";
        txtReqsnDTTo.Text = "";
        cmbDept.ClearSelection();

        ddlCatalogue.Items.Clear();
        ddlSubCatalogue.Items.Clear();
        BindItems();
        updMain.Update();
    }

    public void BindItems()
    {
        if (IsPostBack)
            SaveItemsSelection();


        int is_fetch_count = ucCustomPager1.isCountRecord;
        gvNonContractItems.DataSource = BLL_PURC_CTP.Get_Ctp_Non_Contract_Items(UDFLib.ConvertDateToNull(txtReqsnDTFrom.Text)
                                                  , UDFLib.ConvertDateToNull(txtReqsnDTTo.Text)
                                                  , UDFLib.ConvertStringToNull(ddlSubCatalogue.SelectedValue)
                                                  , ddlCatalogue.SelectedValue
                                                  , ucCustomPager1.CurrentPageIndex
                                                  , ucCustomPager1.PageSize
                                                  , ref is_fetch_count
                                                  , null
                                                  , null
                                                  );
        gvNonContractItems.DataBind();
        if (ucCustomPager1.isCountRecord == 1)
        {
            ucCustomPager1.CountTotalRec = is_fetch_count.ToString();
            ucCustomPager1.BuildPager();

        }


    }

    protected void ddlCatalogue_SelectedIndexChanged(object sender, EventArgs e)
    {
        ((DataTable)ViewState["vsdtSelected_Items"]).Clear();
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DataTable dtSubSystem = new DataTable();
            string CatalogId = ddlCatalogue.SelectedValue;
            dtSubSystem = objTechService.SelectSubCatalogs();

            dtSubSystem.DefaultView.RowFilter = "System_Code ='" + CatalogId + "' or SubSystem_code='0'";
            ddlSubCatalogue.DataTextField = "Subsystem_Description";
            ddlSubCatalogue.DataValueField = "SubSystem_code";
            ddlSubCatalogue.DataSource = dtSubSystem.DefaultView;
            ddlSubCatalogue.DataBind();
            ddlSubCatalogue.Items.FindByText("ALL").Selected = true;

        }
    }
    protected void btnAddItemToExistingContract_Click(object sender, EventArgs e)
    {
        try
        {
            SaveItemsSelection();
            if (((DataTable)ViewState["vsdtSelected_Items"]).Rows.Count > 0)
            {

                mlvCTP.ActiveViewIndex = 2;
                BindContractList_Grid();
               
            }
            else
            {
                lblmsg.Text = "Please select Item !";
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void btnAddToNewContract_Click(object sender, EventArgs e)
    {
        try
        {

            SaveItemsSelection();
            if (((DataTable)ViewState["vsdtSelected_Items"]).Rows.Count > 0)
            {
                DataTable dtSelected = (DataTable)ViewState["vsdtSelected_Items"];

                DataTable dtSelected_SubCatalogue = new DataTable();
                dtSelected_SubCatalogue.Columns.Add("id");


                DataTable vsdtSelected_SubCatalogue_items = new DataTable();
                vsdtSelected_SubCatalogue_items.Columns.Add("id");


                int Contract_ID = BLL_PURC_CTP.Insert_Ctp_CreateNewContract(ddlCatalogue.SelectedValue, Convert.ToInt32(cmbDept.SelectedValue), dtSelected, dtSelected_SubCatalogue, vsdtSelected_SubCatalogue_items, Convert.ToInt32(Session["userid"].ToString()));
                if (Contract_ID > 0)
                {
                    btnAddItemToExistingContract.Enabled = false;
                    btnAddToNewContract.Enabled = false;
                    uc_Purc_Ctp_Send_RFQSupp.Contract_ID = Contract_ID;
                    mlvCTP.ActiveViewIndex = 1;
                   

                }
                else
                {
                    String msg1 = String.Format("alert('failed to create contract !')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg12865", msg1, true);

                }
            }
            else
            {
                lblmsg.Text = "Please select Item !";
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }

    }

    protected void SaveItemsSelection()
    {
        DataTable dtSelected = (DataTable)ViewState["vsdtSelected_Items"];
        bool chk;
        string id = "";
        DataRow drItem;
        foreach (GridViewRow gr in gvNonContractItems.Rows)
        {
            id = gvNonContractItems.DataKeys[gr.RowIndex].Values["ID"].ToString();
            chk = ((CheckBox)gr.FindControl("chkselect")).Checked;

            if (dtSelected.Rows.Contains(id))
            {
                if (!chk)
                {
                    dtSelected.Rows.Find(id).Delete();

                }
            }
            else
            {
                if (chk)
                {
                    drItem = dtSelected.NewRow();
                    drItem["id"] = id;
                    dtSelected.Rows.Add(drItem);
                }
            }
        }
        dtSelected.AcceptChanges();
        ViewState["vsdtSelected_Items"] = dtSelected;
    }
    protected void gvNonContractItems_DataBound(object sender, EventArgs e)
    {
        DataTable dtSelected = (DataTable)ViewState["vsdtSelected_Items"];

        string id = "";

        foreach (GridViewRow gr in gvNonContractItems.Rows)
        {
            id = gvNonContractItems.DataKeys[gr.RowIndex].Values["ID"].ToString();

            if (dtSelected.Rows.Contains(id))
            {
                ((CheckBox)gr.FindControl("chkselect")).Checked = true;
            }

        }
    }
    protected void btnSaveToExistingCtp_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtSelected = (DataTable)ViewState["vsdtSelected_Items"];

            DataTable dtSelected_SubCatalogue = new DataTable();
            dtSelected_SubCatalogue.Columns.Add("id");

            DataTable vsdtSelected_SubCatalogue_items = new DataTable();
            vsdtSelected_SubCatalogue_items.Columns.Add("id");

            int Quotation_ID = Convert.ToInt32(gvContractList_qtn.SelectedDataKey.Value.ToString());

            int sts = BLL_PURC_CTP.Insert_Ctp_QuotationItems(Quotation_ID, dtSelected, dtSelected_SubCatalogue, vsdtSelected_SubCatalogue_items, Convert.ToInt32(Session["userid"].ToString()));
            if (sts > 0)
            {
                String msg1 = String.Format("alert('Items added successfully.');window.open('','_self','');window.close();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg43476", msg1, true);
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }
    }
    protected void gvContractList_qtn_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSaveToExistingCtp.Enabled = true;
    }

    protected void btnSearchContract_Click(object s, EventArgs e)
    {
        BindContractList_Grid();
    }

    protected void BindContractList_Grid()
    {
        gvContractList_qtn.DataSource = BLL_PURC_CTP.Get_Ctp_Contract_List_ByCatalogue(ddlCatalogue.SelectedValue, UDFLib.ConvertIntegerToNull(ctlPortListCtp.SelectedValue), UDFLib.ConvertStringToNull(uc_SupplierListCtp.SelectedValue), UDFLib.ConvertStringToNull(txtContractCode.Text));
        gvContractList_qtn.DataBind();
    }

}