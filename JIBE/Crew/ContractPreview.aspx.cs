using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;


public partial class Crew_ContractPreview : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USERID"] == null)
            Response.Redirect("~/account/Login.aspx");

        if (!IsPostBack)
        {
            int flag = UDFLib.ConvertToInteger(Request.QueryString["flag"].ToString());
            Load_ContractTemplate(flag);
        }
    }

    protected void Load_ContractTemplate(int flag)
    {
        // Updated Method by passing ContractId
        int ContractId = 0;
        DataTable dt = objCrew.Get_ContractTemplate(ContractId);
        if (dt.Rows.Count > 0)
        {
            lblContract.Text = dt.Rows[0]["template_text"].ToString();
        }
        else
        {
            lblContract.Text = "No contract found for the selected flag. <a href='ContractTemplateEdit.aspx?flag=" + Request.QueryString["flag"].ToString() + "' target='_blank'>Click to add a Contract Template</a>";
        }
        
    }
}