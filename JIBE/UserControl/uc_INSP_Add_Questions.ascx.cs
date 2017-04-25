using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Technical;
using SMS.Business.Inspection;
public partial class UserControl_uc_INSP_Add_Questions : System.Web.UI.UserControl
{
    BLL_INSP_Checklist objBLL = new BLL_INSP_Checklist();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_CategoryList();
            Load_Gradings();
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
   
    protected void ddlGradingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int Grade_ID = UDFLib.ConvertToInteger(ddlGradingType.SelectedValue);

        //DataTable dt = objBLL.Get_GradingOptions(Grade_ID);        
        //rdoGradings.DataSource = dt;
        //rdoGradings.DataBind();
      
    }

    protected void Load_Gradings()
    {
        DataTable dt = objBLL.Get_Grades(null);
        ddlGradingType.DataSource = dt;
        ddlGradingType.DataBind();
        ddlGradingType.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void Load_CategoryList()
    {
        DataTable dt = objBLL.Get_Search_CheckListType("");
        // DataTable dt = BLL_Crew_Evaluation.Get_CategoryList(txtfilter.Text);
        //ddlCategory.DataSource = dt;
        //ddlCategory.DataBind();
        //ddlCategory.Items.Insert(0, new ListItem("-Select All-", "0"));


        ddlCatName.DataSource = dt;
        ddlCatName.DataBind();

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int ID = objBLL.INSERT_Question(txtCriteria.Text.Trim(), UDFLib.ConvertToInteger(ddlCatName.SelectedValue), UDFLib.ConvertToInteger(ddlGradingType.SelectedValue), GetSessionUserID());        
        txtCriteria.Text = "";
    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        int ID = objBLL.INSERT_Question(txtCriteria.Text.Trim(), UDFLib.ConvertToInteger(ddlCatName.SelectedValue), UDFLib.ConvertToInteger(ddlGradingType.SelectedValue), GetSessionUserID());
        txtCriteria.Text = "";



        string js = "return closeAddQuestion(),false;";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);

    }
}