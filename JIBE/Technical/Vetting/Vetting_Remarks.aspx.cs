using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.VET;
using System.Data;
public partial class Technical_Vetting_Vetting_Remarks : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.QueryString["Vetting_ID"] != null)
            {
                BindRemarks();
            }


        }
    }

    protected void BindRemarks()
    {
        BLL_VET_Planner objPlan = new BLL_VET_Planner();

        DataTable dt = objPlan.VET_Get_VettingRemarks(UDFLib.ConvertToInteger(Request.QueryString["Vetting_ID"].ToString()));

        gvRemarks.DataSource = dt;
        gvRemarks.DataBind();

    }
    protected void gvRemarks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblRemarkDate = (Label)e.Row.FindControl("lblRemarkDate");

            lblRemarkDate.Text = DateTime.Parse(lblRemarkDate.Text).ToString(UDFLib.GetDateFormat());

        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtRemarks.Text.Trim()))
            {
                BLL_VET_Planner objPlan = new BLL_VET_Planner();

                objPlan.VET_Ins_VettingRemark(UDFLib.ConvertToInteger(Request.QueryString["Vetting_ID"].ToString()), txtRemarks.Text.Trim(), GetSessionUserID());

                BindRemarks();
                txtRemarks.Text = "";
            }
            else
            {
                string js = "alert('Enter the Remark');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "remarkerror", js, true);

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
}