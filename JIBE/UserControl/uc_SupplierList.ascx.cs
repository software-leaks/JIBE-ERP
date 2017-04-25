using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.PURC;
using System.ComponentModel;

public partial class UserControl_uc_SupplierList : System.Web.UI.UserControl
{
    #region property

    public string SelectedText
    {
        get { return hdn_SelectedText.Value; }
    }

    [BindableAttribute(true)]
    public string SelectedValue
    {

        get { return hdn_SelectedValue.Value; }

        set
        {
            try
            {
                hdn_SelectedValue.Value = value;

                if (hdn_SelectedValue.Value != "" && hdn_SelectedValue.Value != "0")
                {
                    DataTable dtSupplier = BLL_PURC_Common.Get_SupplierDetails_ByCode(hdn_SelectedValue.Value);
                    if (dtSupplier.Rows.Count > 0)
                    {
                        txtSelectedSupplier.Text = dtSupplier.Rows[0]["fullname"].ToString();
                       
                    }
                }
                else
                {
                    txtSelectedSupplier.Text = "-Select-";
                }
            }
            catch { }
        }
    }
    public string TargetControl
    {

        get { return hdn_TargetControlID.Value; }

        set { hdn_TargetControlID.Value = value; }
    }
    public string Width
    {

        get { return txtSelectedSupplier.Width.ToString(); }

        set
        {
            try
            {
                if (int.Parse(value.Replace("px", "")) > 30)
                {
                    txtSelectedSupplier.Width = int.Parse(value.Replace("px", ""));
                }
            }
            catch { }
        }
    }
    public bool Enable
    {
       
        set
        {
            pnlSearch.Enabled = value;
            Panel1.Enabled = value;
        }
    }
   
    public string Supplier_Category
    {
        get { return hdf_Supplier_Type.Value; }
        set { hdf_Supplier_Type.Value = value; }
    }

    #endregion

   

    public delegate void SelectedIndexChangedEventHandler(ListBox s);

    public event SelectedIndexChangedEventHandler SelectedIndexChanged;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (txtSelectedSupplier.Text.Trim() == "")
            txtSelectedSupplier.Text = "-Select Supplier-";
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            pnlSearch.Visible = false;

            if (lstSupplierList.SelectedItem != null)
            {
                hdn_SelectedText.Value = lstSupplierList.SelectedItem.Text;
                hdn_SelectedValue.Value = lstSupplierList.SelectedItem.Value;

                txtSelectedSupplier.Text = hdn_SelectedText.Value;

                if (SelectedIndexChanged != null)
                    SelectedIndexChanged(lstSupplierList);

                //txtSelectedSupplier.Text = hdn_SelectedText.Value;
                //txtSearchSupplierList.Focus();
            }
        }
        catch { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string SearchText = txtSearchSupplierList.Text;
        DataTable dt = BLL_PURC_Common.Get_SupplierList(Supplier_Category, SearchText);
        lstSupplierList.DataSource = dt;
        lstSupplierList.DataBind();
        lstSupplierList.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void lstSupplierList_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSearch.Visible = false;

        if (lstSupplierList.SelectedItem != null)
        {
            hdn_SelectedText.Value = lstSupplierList.SelectedItem.Text;
            hdn_SelectedValue.Value = lstSupplierList.SelectedItem.Value;

            txtSelectedSupplier.Text = hdn_SelectedText.Value;

            if (SelectedIndexChanged != null)
                SelectedIndexChanged(lstSupplierList);
        }
    }
    protected void btnSearchSupplier_Click(object sender, EventArgs e)
    {
        pnlSearch.Visible = true;
        DataTable dt = BLL_PURC_Common.Get_SupplierList(Supplier_Category, null);
        lstSupplierList.DataSource = dt;
        lstSupplierList.DataBind();
        lstSupplierList.Items.Insert(0, new ListItem("-Select-", "0"));


    }

    protected void txtSearchSupplierList_TextChanged(object sender, EventArgs e)
    {
        string SearchText = txtSearchSupplierList.Text;
        DataTable dt = BLL_PURC_Common.Get_SupplierList(Supplier_Category, SearchText); 
        lstSupplierList.DataSource = dt;
        lstSupplierList.DataBind();
        lstSupplierList.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        txtSearchSupplierList.Text = "";
        pnlSearch.Visible = false;
    }

   
}