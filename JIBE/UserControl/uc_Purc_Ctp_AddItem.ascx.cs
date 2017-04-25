using System;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.PURC;
using System.Web.UI;

public partial class UserControl_Ctp_AddItem : System.Web.UI.UserControl
{
    #region Property

    public string Catalogue_code
    {
        get { return hdf_catalogue_Code.Value; }
        set { hdf_catalogue_Code.Value = value; }
    }

    public string Catalogue_Name
    {
        set { lblCatalogue.Text = value; }
    }

    public int Dept_ID
    {
        get { return UDFLib.ConvertToInteger(hdf_Dept_ID.Value); }
        set { hdf_Dept_ID.Value = value.ToString(); }
    }

    public string SubCatalogue
    {
        get { return hdf_SubCatalogue.Value; }
        set { hdf_SubCatalogue.Value = value; }
    }

    public int Contract_ID
    {
        get { return UDFLib.ConvertToInteger(hdf_Contract_ID.Value); }
        set { hdf_Contract_ID.Value = value.ToString(); }

    }
    public bool AddItems_Saved_Status
    {
        get { return bool.Parse(hdf_AddItems_Status.Value); }
        set { hdf_AddItems_Status.Value = value.ToString(); }
    }

    public string DepartmentName
    {
        set { lblDepartmentName.Text = value; }
    }

    public bool Is_Reset_Values
    {
        get { return bool.Parse(hdf_Is_Reset_Values.Value); }
        set { hdf_Is_Reset_Values.Value = value.ToString(); }
    }

    public int Quotation_ID
    {
        get { return Convert.ToInt32(hdf_Quotation_ID.Value); }
        set { hdf_Quotation_ID.Value = value.ToString(); }
    }





    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        txtItemSearch.Focus();
        if (!IsPostBack)
        {

            Create_StoreinViewStateDatatables();

        }

        if (Is_Reset_Values)
        {
            Reset_Values();

        }

        if (AddItems_Saved_Status)
        {
            btnSaveItems.Enabled = false;
            btnCancel.Enabled = false;
        }
        else
        {
            btnSaveItems.Enabled = true;
            btnCancel.Enabled = true;
        }

    }
    protected void txtItemSearch_TextChanged(object sender, EventArgs e)
    {
        ucCustomPagerItem_Selected.isCountRecord = 1;
        ucCustomPagerItem_UnSelected.isCountRecord = 1;
        BindData_Selected();
        BindData_UnSelected();
    }

    protected void rbtnselect_SelectedIndexChanged(object sender, CommandEventArgs e)
    {
        if (e.CommandArgument == "0")
        {
            DataTable dtSelected = (DataTable)ViewState["vsdtSelected_Items"];
            DataTable dtSelected_SubCatalogue_item = (DataTable)ViewState["vsdtSelected_SubCatalogue_items"];
            string item_ref_code = "";
            DataRow dr;
            DataRow ddr;
            foreach (GridViewRow gr in gvItem_UnSelected.Rows)
            {
                item_ref_code = gvItem_UnSelected.DataKeys[gr.RowIndex].Value.ToString();

                if (!dtSelected.Rows.Contains(item_ref_code))
                {
                    dr = dtSelected.NewRow();
                    dr[0] = item_ref_code;
                    dtSelected.Rows.Add(dr);
                }


                if (dtSelected_SubCatalogue_item.Rows.Contains(item_ref_code))
                {
                    ddr = dtSelected_SubCatalogue_item.Rows.Find(item_ref_code);
                    dtSelected_SubCatalogue_item.Rows.Remove(ddr);

                }
            }
            ViewState["vsdtSelected_Items"] = dtSelected;
            ViewState["vsdtSelected_SubCatalogue_items"] = dtSelected_SubCatalogue_item;
            BindData_UnSelected();
            BindData_Selected();
        }
    }
    protected void rbtn_UnSelect_SelectedIndexChanged(object sender, CommandEventArgs e)
    {
        if (e.CommandArgument == "0")
        {
            DataTable dtSelected = (DataTable)ViewState["vsdtSelected_Items"];
            DataTable dtSelected_SubCatalogue_item = (DataTable)ViewState["vsdtSelected_SubCatalogue_items"];

            string item_ref_code = "";
            DataRow dr;
            DataRow ddr;
            foreach (GridViewRow gr in gvItem_Selected.Rows)
            {
                item_ref_code = gvItem_Selected.DataKeys[gr.RowIndex].Value.ToString();

                if (dtSelected.Rows.Contains(item_ref_code))
                {
                    dr = dtSelected.Rows.Find(item_ref_code);
                    dtSelected.Rows.Remove(dr);
                }

                else
                {

                    ddr = dtSelected_SubCatalogue_item.NewRow();
                    ddr[0] = item_ref_code;
                    dtSelected_SubCatalogue_item.Rows.Add(ddr);
                }

            }
            ViewState["vsdtSelected_Items"] = dtSelected;
            ViewState["vsdtSelected_SubCatalogue_items"] = dtSelected_SubCatalogue_item;
            BindData_UnSelected();
            BindData_Selected();
        }
    }

    protected void btnSelect_Click(object s, CommandEventArgs e)
    {

        DataTable dtSelected = (DataTable)ViewState["vsdtSelected_Items"];
        if (!dtSelected.Rows.Contains(e.CommandArgument))
        {
            DataRow dr = dtSelected.NewRow();
            dr[0] = e.CommandArgument;
            dtSelected.Rows.Add(dr);
            ViewState["vsdtSelected_Items"] = dtSelected;
        }

        DataTable dtSelected_SubCatalogue_item = (DataTable)ViewState["vsdtSelected_SubCatalogue_items"];
        if (dtSelected_SubCatalogue_item.Rows.Contains(e.CommandArgument))
        {
            DataRow dr = dtSelected_SubCatalogue_item.Rows.Find(e.CommandArgument);
            dtSelected_SubCatalogue_item.Rows.Remove(dr);
            ViewState["vsdtSelected_SubCatalogue_items"] = dtSelected_SubCatalogue_item;

        }


        BindData_UnSelected();
        BindData_Selected();


    }
    protected void btnUnSelect_Click(object s, CommandEventArgs e)
    {
        DataTable dtSelected = (DataTable)ViewState["vsdtSelected_Items"];
        if (dtSelected.Rows.Contains(e.CommandArgument))
        {
            DataRow dr = dtSelected.Rows.Find(e.CommandArgument);
            dtSelected.Rows.Remove(dr);
            ViewState["vsdtSelected_Items"] = dtSelected;
        }
        else
        {
            DataTable dtSelected_SubCatalogue_item = (DataTable)ViewState["vsdtSelected_SubCatalogue_items"];
            DataRow dr = dtSelected_SubCatalogue_item.NewRow();
            dr[0] = e.CommandArgument;
            dtSelected_SubCatalogue_item.Rows.Add(dr);
            ViewState["vsdtSelected_SubCatalogue_items"] = dtSelected_SubCatalogue_item;
        }
        BindData_UnSelected();
        BindData_Selected();

    }

    public void BindSubCatalogue()
    {
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DataTable dtSubSystem = new DataTable();
            string CatalogId = Catalogue_code;
            dtSubSystem = objTechService.SelectSubCatalogs();
            DataRow dr = dtSubSystem.NewRow();

            dtSubSystem.DefaultView.RowFilter = "System_Code ='" + CatalogId + "' or SubSystem_code='0'";
            gvSubCatalogue.DataSource = dtSubSystem.DefaultView;
            gvSubCatalogue.DataBind();

        }
    }

    public void BindData_Selected()
    {


        int IsFetchCount = ucCustomPagerItem_Selected.isCountRecord;

        gvItem_Selected.DataSource = BLL_PURC_CTP.Get_Ctp_Items(Catalogue_code, UDFLib.ConvertStringToNull(SubCatalogue), UDFLib.ConvertStringToNull(txtItemSearch.Text), (DataTable)ViewState["vsdtSelected_Items"], (DataTable)ViewState["vsdtSelected_SubCatalogue"], (DataTable)ViewState["vsdtSelected_SubCatalogue_items"], 1,
                                      UDFLib.ConvertIntegerToNull(Quotation_ID), ucCustomPagerItem_Selected.CurrentPageIndex, ucCustomPagerItem_Selected.PageSize, ref IsFetchCount,Contract_ID);
        gvItem_Selected.DataBind();
        if (ucCustomPagerItem_Selected.isCountRecord == 1)
        {
            ucCustomPagerItem_Selected.CountTotalRec = IsFetchCount.ToString();
            ucCustomPagerItem_Selected.BuildPager();
        }

        btnDeSelectAll.Enabled = (IsFetchCount < 1) ? false : true;

        btnDeSElctAll_SubCatalogue.Enabled = (((DataTable)ViewState["vsdtSelected_SubCatalogue"]).Rows.Count > 0 && IsFetchCount > 0) ? true : false;

        Set_SelectedCatalogue();
    }
    public void BindData_UnSelected()
    {

        int IsFetchCount = ucCustomPagerItem_UnSelected.isCountRecord;

        gvItem_UnSelected.DataSource = BLL_PURC_CTP.Get_Ctp_Items(Catalogue_code, UDFLib.ConvertStringToNull(SubCatalogue), UDFLib.ConvertStringToNull(txtItemSearch.Text), (DataTable)ViewState["vsdtSelected_Items"], (DataTable)ViewState["vsdtSelected_SubCatalogue"], (DataTable)ViewState["vsdtSelected_SubCatalogue_items"], 0,
                                       UDFLib.ConvertIntegerToNull(Quotation_ID), ucCustomPagerItem_UnSelected.CurrentPageIndex, ucCustomPagerItem_UnSelected.PageSize, ref IsFetchCount,Contract_ID);
        gvItem_UnSelected.DataBind();

        if (ucCustomPagerItem_UnSelected.isCountRecord == 1)
        {
            ucCustomPagerItem_UnSelected.CountTotalRec = IsFetchCount.ToString();
            ucCustomPagerItem_UnSelected.BuildPager();
        }


        btnSelect_All.Enabled = (IsFetchCount < 1) ? false : true;
        btnselectAll_SubCatalogue.Enabled = (IsFetchCount < 1 || SubCatalogue.Trim() == "" || SubCatalogue.Trim() == "0") ? false : true;

        Set_SelectedCatalogue();
    }

    protected void gvSubCatalogue_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        ucCustomPagerItem_Selected.isCountRecord = 1;
        ucCustomPagerItem_UnSelected.isCountRecord = 1;
        SubCatalogue = gvSubCatalogue.DataKeys[e.NewSelectedIndex].Value.ToString();

        BindData_UnSelected();
        BindData_Selected();

    }

    protected void btnselectAll_SubCatalogue_Click(object sender, EventArgs e)
    {
        DataTable dtSelected_SubCatalogue = (DataTable)ViewState["vsdtSelected_SubCatalogue"];
        if (!dtSelected_SubCatalogue.Rows.Contains(SubCatalogue))
        {
            DataRow dr = dtSelected_SubCatalogue.NewRow();
            dr[0] = SubCatalogue;
            dtSelected_SubCatalogue.Rows.Add(dr);
            ViewState["vsdtSelected_SubCatalogue"] = dtSelected_SubCatalogue;
        }

        BindData_UnSelected();
        BindData_Selected();

    }
    protected void btnDeSElctAll_SubCatalogue_Click(object sender, EventArgs e)
    {

        DataTable dtSelected_SubCatalogue = (DataTable)ViewState["vsdtSelected_SubCatalogue"];
        DataRow dr = dtSelected_SubCatalogue.Rows.Find(SubCatalogue);
        if (dr != null)
        {
            dtSelected_SubCatalogue.Rows.Remove(dr);
            ViewState["vsdtSelected_SubCatalogue"] = dtSelected_SubCatalogue;
        }
        BindData_UnSelected();
        BindData_Selected();

    }

    protected void btnSaveItems_Click(object sender, EventArgs e)
    {
        try
        {
            if (Quotation_ID == -1 && Contract_ID < 1)
            {
                int sts = BLL_PURC_CTP.Insert_Ctp_CreateNewContract(Catalogue_code, Dept_ID, (DataTable)ViewState["vsdtSelected_Items"], (DataTable)ViewState["vsdtSelected_SubCatalogue"], (DataTable)ViewState["vsdtSelected_SubCatalogue_items"], Convert.ToInt32(Session["userid"].ToString()));
                if (sts > -1)
                {
                    btnSaveItems.Enabled = false;
                    AddItems_Saved_Status = true;
                    btnCancel.Enabled = false;

                    Contract_ID = sts;
                }
            }
            else if (Quotation_ID > 0 || Contract_ID >0)
            {
                int sts = BLL_PURC_CTP.Insert_Ctp_QuotationItems(Quotation_ID, (DataTable)ViewState["vsdtSelected_Items"], (DataTable)ViewState["vsdtSelected_SubCatalogue"], (DataTable)ViewState["vsdtSelected_SubCatalogue_items"], Convert.ToInt32(Session["userid"].ToString()),Contract_ID);
            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Is_Reset_Values = true;
        Reset_Values();
        BindData_Selected();
        BindData_UnSelected();
        Is_Reset_Values = false;
    }

    private void Reset_Values()
    {
        DataTable dtSelected = (DataTable)ViewState["vsdtSelected_Items"];
        DataTable dtSelected_SubCatalogue = (DataTable)ViewState["vsdtSelected_SubCatalogue"];
        DataTable dtSelected_SubCatalogue_Items = (DataTable)ViewState["vsdtSelected_SubCatalogue_items"];

        dtSelected.Clear();
        dtSelected_SubCatalogue.Clear();
        dtSelected_SubCatalogue_Items.Clear();

        ViewState["vsdtSelected_Items"] = dtSelected;
        ViewState["vsdtSelected_SubCatalogue"] = dtSelected_SubCatalogue;
        ViewState["vsdtSelected_SubCatalogue_items"] = dtSelected_SubCatalogue_Items;

        Contract_ID = -1;

        SubCatalogue = "";
    }

    private void Set_SelectedCatalogue()
    {
        DataTable dtSelected_SubCatalogue = (DataTable)ViewState["vsdtSelected_SubCatalogue"];

        lblSelectedSubCatalogue.Text = "";
        foreach (GridViewRow gr in gvSubCatalogue.Rows)
        {
            if (dtSelected_SubCatalogue.Rows.Contains(gvSubCatalogue.DataKeys[gr.RowIndex].Value.ToString()))
            {
                lblSelectedSubCatalogue.Text += " * " + ((LinkButton)gr.FindControl("lblSubCatalogueName")).Text;
            }
        }

    }

    public void Create_StoreinViewStateDatatables()
    {
        DataTable dtSelected = new DataTable();
        DataColumn column = new DataColumn("id", typeof(string));
        dtSelected.Columns.Add(column);
        dtSelected.PrimaryKey = new DataColumn[] { column };
        ViewState["vsdtSelected_Items"] = dtSelected;

        DataTable dtSelected_SubCatalogue = new DataTable();
        DataColumn column_SubCatalogue = new DataColumn("id", System.Type.GetType("System.String"));
        dtSelected_SubCatalogue.Columns.Add(column_SubCatalogue);
        dtSelected_SubCatalogue.PrimaryKey = new DataColumn[] { column_SubCatalogue };
        ViewState["vsdtSelected_SubCatalogue"] = dtSelected_SubCatalogue;


        DataTable dtSelected_SubCatalogue_Items = new DataTable();
        DataColumn column_SubCatalogue_items = new DataColumn("id", System.Type.GetType("System.String"));
        dtSelected_SubCatalogue_Items.Columns.Add(column_SubCatalogue_items);
        dtSelected_SubCatalogue_Items.PrimaryKey = new DataColumn[] { column_SubCatalogue_items };
        ViewState["vsdtSelected_SubCatalogue_items"] = dtSelected_SubCatalogue_Items;
    }

    public void SetAttribute_Refresh()
    {
       btnSaveItems.Attributes.Add("onclick","javascript:parent.ReloadParent_ByButtonID();");
        
    }
}