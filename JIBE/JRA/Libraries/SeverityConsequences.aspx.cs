using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.JRA;
public partial class JRA_Libraries_SeverityConsequences : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            LoadCombo();
        }
    }

    protected void LoadCombo()
    {
        DataSet dsSev = BLL_JRA_Hazards.GET_TYPE("Severity");
        ddlSeverity.DataSource = dsSev.Tables[0];
        ddlSeverity.DataTextField = "Type_Display_Text";
        ddlSeverity.DataValueField = "Type_ID";
        ddlSeverity.DataBind();
        ddlSeverity.Items.Insert(0, new ListItem("-Select All-", "0"));

        DataSet dsCon = BLL_JRA_Hazards.GET_TYPE("Consequences");
        ddlCons.DataSource = dsCon.Tables[0];
        ddlCons.DataTextField = "Type_Display_Text";
        ddlCons.DataValueField = "Type_ID";
        ddlCons.DataBind();
        ddlCons.Items.Insert(0, new ListItem("-Select All-", "0"));
    }
    protected void ddlSeverity_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSevCon();
    }
    protected void ddlCons_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadSevCon();
    }

    protected void LoadSevCon()
    {
        if (ddlSeverity.SelectedIndex > 0 && ddlCons.SelectedIndex > 0)
        {
            DataTable dT = BLL_JRA_Hazards.GET_Sev_Cons(UDFLib.ConvertToInteger(ddlSeverity.SelectedValue), UDFLib.ConvertToInteger(ddlCons.SelectedValue));
            if (dT.Rows.Count > 0)
            {
                txtDesc.Text = dT.Rows[0]["SC_Description"].ToString();
            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlSeverity.SelectedIndex > 0 && ddlCons.SelectedIndex > 0)
        {
            BLL_JRA_Hazards.INSUPD_Sev_Cons(UDFLib.ConvertToInteger(ddlSeverity.SelectedValue), UDFLib.ConvertToInteger(ddlCons.SelectedValue), txtDesc.Text, UDFLib.ConvertToInteger(Session["UserID"]));
            string js0 = "alert('Description added Successfully, check the matrix to see effects!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsMsg", js0, true);
        }


    }
}