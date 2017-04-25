using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Purchase_CTP_Copy_Contract : System.Web.UI.Page
{
    public UserAccess objUA = new UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
           

            BindContractInfo();
            bindDeptCatalogue();
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnCreateContract.Visible = false;
           

        }
        if (objUA.Edit == 0)
        {
           

        }
        if (objUA.Approve == 0)
        {
            btnCreateContract.Visible = false;
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

            ListItem LiDp= cmbDept.Items.FindByText(lblDepartment.Text.Trim());
            if (LiDp != null)
                LiDp.Enabled = false;

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
                ListItem LiDp = cmbDept.Items.FindByText(lblDepartment.Text.Trim());
                if (LiDp != null)
                    LiDp.Enabled = false;

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

    private void BindContractInfo()
    {
        DataTable dtInfo = BLL_PURC_CTP.Get_Ctp_Contract_Info(Convert.ToInt32(Request.QueryString["Quotation_ID"]), Convert.ToInt32(Request.QueryString["Contract_ID"]));
        if (dtInfo.Rows.Count > 0 && Convert.ToInt32(Request.QueryString["Quotation_ID"]) > 0)
        {
            lblApprovedBy.Text = dtInfo.Rows[0]["ApprovedBy"].ToString();

            lblCatalogue.Text = dtInfo.Rows[0]["System_Description"].ToString();

            lblDepartment.Text = dtInfo.Rows[0]["Dept_Name"].ToString();
            lblEffectiveDT.Text = dtInfo.Rows[0]["Effective_Date"].ToString();
            lblPort.Text = dtInfo.Rows[0]["PORT_NAME"].ToString();
            //lblRejectedDT.Text = dtInfo.Rows[0]["ApprovedBy"].ToString();
            lblSeachangeRef.Text = dtInfo.Rows[0]["QTN_Contract_Code"].ToString();


            lblSupplierName.Text = dtInfo.Rows[0]["Full_NAME"].ToString();
            lblSupplierRef.Text = dtInfo.Rows[0]["Supplier_Ref_Number"].ToString();
            imgApprovedByRmk.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[ ] body=[" + dtInfo.Rows[0]["approver_remark"].ToString() + "]");


        }

    }
    protected void btnDeSelect_Click(object sender, EventArgs e)
    {
        lstSelectedItems.Items.Remove(lstSelectedItems.SelectedItem);

    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        ListItem lstItems = new ListItem();
        lstItems.Text = cmbDept.SelectedItem.Text + " - " + ddlCatalogue.SelectedItem.Text;
        lstItems.Value = cmbDept.SelectedItem.Value + "~" + ddlCatalogue.SelectedItem.Value;
        if (lstSelectedItems.Items.IndexOf(lstItems) < 0)
        {
            lstSelectedItems.Items.Add(lstItems);
            lstSelectedItems.SelectedIndex = 0;
        }
    }
    protected void btnCreateContract_Click(object sender, EventArgs e)
    {
        if (lstSelectedItems.Items.Count > 0)
        {
           
            DataTable dtDeptCatalogue = new DataTable();
            dtDeptCatalogue.Columns.Add("ID");
            dtDeptCatalogue.Columns.Add("Value");
            foreach (ListItem li in lstSelectedItems.Items)
            {
                DataRow dritem = dtDeptCatalogue.NewRow();
                dritem["ID"] = li.Value.Split('~')[0];
                dritem["Value"] = li.Value.Split('~')[1];

                dtDeptCatalogue.Rows.Add(dritem);
            }
            
           int res= BLL_PURC_CTP.INS_CTP_Copy_Contract(Convert.ToInt32(Request.QueryString["Quotation_ID"]), Convert.ToInt32(Session["userid"]), txtCopyRemark.Text, dtDeptCatalogue);
           if (res == 1)
           {
               String msg = String.Format("alert(' Contract Created successfully.');window.opener.location.reload();window.open('','_self','');window.close();");
               ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgclosesucesc", msg, true);
           }
               else if(res ==2)
            {
                String msg = String.Format("alert('Contract already exists !');window.open('','_self','');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgclosesucesc", msg, true);
            }
           else if (res == 3)
           {
               String msg = String.Format("alert('Matching item not found !');window.open('','_self','');");
               ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgclosesucesc", msg, true);
           }
           else
           {
               String msg = String.Format("alert('Error while saving !');window.open('','_self','');");
               ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgclosesucesc", msg, true);
           }
        }
    }
}