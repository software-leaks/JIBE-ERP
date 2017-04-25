using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Data;

public partial class Infrastructure_Libraries_ApprovalType : System.Web.UI.Page
{
    BLL_Infra_Approval_Type objBLLAppType = new BLL_Infra_Approval_Type();

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        txtTypeKey.Text = "";
        txtTypeName.Text = "";
        string AddApprovalTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddApprovalTypemodal", AddApprovalTypemodal, true);
    }
  
    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string searchText = null;
        if (txtfilter.Text.Trim() != "")
            searchText = txtfilter.Text.Trim();
        DataTable dt = objBLLAppType.Get_Approval_Type(searchText);


        string[] HeaderCaptions = {  "Approval type", "Amount Applicable" };
        string[] DataColumnsName = { "TYPE_VALUE", "AMOUNT_APPLICABLE" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "ApprovalType", "Approval Type", "");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string js;
        int AmountApplicable = 0;
        if (chkAmountApplicable.Checked == true)
            AmountApplicable = 1;
        int responseid = objBLLAppType.INS_Approval_Type(txtTypeKey.Text.Trim(), txtTypeName.Text.Trim(), AmountApplicable, Convert.ToInt32(Session["USERID"]));
        if (responseid > 0)
        {
            js = "alert('Type Created');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
            BindApprovalType();
        }
        else
        {
            js = "alert('Type name already exsist');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BindApprovalType();
        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
    }
    protected void BindApprovalType()
    {
        string searchText = null;
        if (txtfilter.Text.Trim() != "" ) 
            searchText = txtfilter.Text.Trim();
        DataTable dt = objBLLAppType.Get_Approval_Type(searchText);
        gvApprovalType.DataBind();
    }
    protected void ObjectDataSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if ( e.InputParameters["TYPE_VALUE"] == null || e.InputParameters["TYPE_VALUE"].ToString() == "" )
        {
            string js = "Approval Name is mandatory";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + js + "');", true);
            e.Cancel = true;
            return;
        }
    }
}