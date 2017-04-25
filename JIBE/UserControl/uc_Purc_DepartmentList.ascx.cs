using System;
using System.Web.UI.WebControls;
using System.ComponentModel;
using SMS.Business.PURC;
using System.Data;

public partial class UserControl_uc_Purc_DepartmentList : System.Web.UI.UserControl
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
                    DataTable dtDepartment = Get_Department(Convert.ToInt32(hdn_SelectedValue.Value),null);
                    if (dtDepartment.Rows.Count > 0)
                    {
                        txtSelectedDepartment.Text = dtDepartment.Rows[0]["fullname"].ToString();
                    }
                }
                else
                {
                    txtSelectedDepartment.Text = "-Select-";
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

        get { return txtSelectedDepartment.Width.ToString(); }

        set
        {
            try
            {
                if (int.Parse(value.Replace("px", "")) > 30)
                {
                    txtSelectedDepartment.Width = int.Parse(value.Replace("px", ""));
                }
            }
            catch { }
        }
    }

    public string Department_Category
    {
        get { return hdf_Department_Type.Value; }
        set { hdf_Department_Type.Value = value; }
    }

    #endregion



    public delegate void SelectedIndexChangedEventHandler(ListBox s);

    public event SelectedIndexChangedEventHandler SelectedIndexChanged;


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            pnlSearch.Visible = false;

            if (lstDepartmentList.SelectedItem != null)
            {
                hdn_SelectedText.Value = lstDepartmentList.SelectedItem.Text;
                hdn_SelectedValue.Value = lstDepartmentList.SelectedItem.Value;

                txtSelectedDepartment.Text = hdn_SelectedText.Value;

                if (SelectedIndexChanged != null)
                    SelectedIndexChanged(lstDepartmentList);

                //txtSelectedDepartment.Text = hdn_SelectedText.Value;
                //txtSearchDepartmentList.Focus();
            }
        }
        catch { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string SearchText = txtSearchDepartmentList.Text;
        DataTable dt = Get_Department(null, UDFLib.ConvertStringToNull(SearchText));
        lstDepartmentList.DataSource = dt;
        lstDepartmentList.DataBind();
        lstDepartmentList.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void lstDepartmentList_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSearch.Visible = false;

        if (lstDepartmentList.SelectedItem != null)
        {
            hdn_SelectedText.Value = lstDepartmentList.SelectedItem.Text;
            hdn_SelectedValue.Value = lstDepartmentList.SelectedItem.Value;

            txtSelectedDepartment.Text = hdn_SelectedText.Value;

            if (SelectedIndexChanged != null)
                SelectedIndexChanged(lstDepartmentList);
        }
    }
    protected void btnSearchDepartment_Click(object sender, EventArgs e)
    {
        pnlSearch.Visible = true;
        DataTable dt = Get_Department(null, null);
        lstDepartmentList.DataSource = dt;
        lstDepartmentList.DataBind();
        lstDepartmentList.Items.Insert(0, new ListItem("-Select-", "0"));


    }

    protected void txtSearchDepartmentList_TextChanged(object sender, EventArgs e)
    {
        string SearchText = txtSearchDepartmentList.Text;
        DataTable dt = Get_Department(null,UDFLib.ConvertStringToNull(SearchText));
        lstDepartmentList.DataSource = dt;
        lstDepartmentList.DataBind();
        lstDepartmentList.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        txtSearchDepartmentList.Text = "";
        pnlSearch.Visible = false;
    }

    private DataTable Get_Department(int? Department_ID, string Dept_Name)
    {
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        DataTable dtDepartment = new DataTable();
        dtDepartment = objTechService.SelectDepartment();
        string rowFilter = "";
        if (Department_ID != null || Dept_Name != null)
        {
            rowFilter=(Department_ID != null)?"id='"+Department_ID+"'":"Name_Dept like '%"+Dept_Name+"%'";
            dtDepartment.DefaultView.RowFilter = rowFilter;
            
        }

        return dtDepartment;


    }
}