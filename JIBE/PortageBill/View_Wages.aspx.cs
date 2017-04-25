using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class PortageBill_View_Wages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["viewstaff"] != null)
            {
                lblstaffdtl.Text = "Salary details for:" + Request.QueryString["viewstaff"].ToString();
            }
         //GridViewViewwages.DataSource = Eern_Deduction.SP_ACC_GetCrewWages(System.Convert.ToInt32(Request.QueryString["VoyID"].ToString().Trim()), Convert.ToInt32(Session["vessel_codeadd"]));
            //GridViewViewwages.DataBind();
           // ObjectDataSource1.Select();
            //GridViewViewwages.DataBind();
            //int id=Convert.ToInt32(Request.QueryString["VoyID"].ToString().Trim());
            //int vc=Convert.ToInt32(Session["vessel_codeadd"].ToString().Trim());
            //Eern_Deduction.SP_ACC_GetCrewWages(id, vc);


        }
    }
    protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["voyid"] = System.Convert.ToInt32(Request.QueryString["VoyID"].ToString().Trim());
    }
    protected void ObjectDataSource1_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            e.InputParameters.Clear();

            double rulevalues = 0;
            if (double.TryParse(((TextBox)GridViewViewwages.Rows[GridViewViewwages.EditIndex].FindControl("TextBox5")).Text, out rulevalues))
            {
            }
            double amt = 0;
            if (double.TryParse(((TextBox)GridViewViewwages.Rows[GridViewViewwages.EditIndex].FindControl("TextBox3")).Text, out amt))
            {
            }
     

            string efdt = "1/1/1999";
            
            DateTime dt = DateTime.Parse(efdt);

            e.InputParameters["voyid"] = System.Convert.ToInt32(Request.QueryString["VoyID"].ToString().Trim());
            e.InputParameters["Entry_Type"] = Convert.ToInt32(GridViewViewwages.DataKeys[GridViewViewwages.EditIndex][1].ToString());
            e.InputParameters["Salary_Type"] = Convert.ToInt32(((DropDownList)GridViewViewwages.Rows[GridViewViewwages.EditIndex].FindControl("DDLSalaryType")).SelectedValue);
            e.InputParameters["PAYABLE_AT"] = Convert.ToInt32(((DropDownList)GridViewViewwages.Rows[GridViewViewwages.EditIndex].FindControl("ddlpayableat")).SelectedValue);
            e.InputParameters["Amount"] = amt;
            e.InputParameters["Currency_type"] = Convert.ToInt32(((DropDownList)GridViewViewwages.Rows[GridViewViewwages.EditIndex].FindControl("DDLCurrencyType")).SelectedValue);
            e.InputParameters["Rule_Values"] = rulevalues;
            e.InputParameters["Rule_Type"] = ((DropDownList)GridViewViewwages.Rows[GridViewViewwages.EditIndex].FindControl("ddlruletype")).SelectedValue; ;
            e.InputParameters["Rule_parent_code"] = Convert.ToInt32(((DropDownList)GridViewViewwages.Rows[GridViewViewwages.EditIndex].FindControl("DDLEntryType_Rpce")).SelectedValue); ;
            e.InputParameters["Vessel_Code"] = Convert.ToInt32(Request.QueryString["vcodeviewwages"].ToString());
            e.InputParameters["id"] = Convert.ToInt32(GridViewViewwages.DataKeys[GridViewViewwages.EditIndex][0].ToString());
            e.InputParameters["effdt"] = dt;
            e.InputParameters["updsts"] = 0;
            ViewState["idpk"] = Convert.ToInt32(GridViewViewwages.DataKeys[GridViewViewwages.EditIndex][0].ToString());


        }
        catch(Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
    }

    protected void GridViewViewwages_DataBound(object sender, EventArgs e)
    {
        try
        {
            if (GridViewViewwages.EditIndex != -1)
            {
                if (ViewState["currtype"].ToString() == "" || ViewState["currtype"].ToString() == "Select")
                {
                    ((DropDownList)GridViewViewwages.Rows[GridViewViewwages.EditIndex].FindControl("DDLCurrencyType")).Items.FindByText("USD").Selected = true;
                }
                ((DropDownList)GridViewViewwages.Rows[GridViewViewwages.EditIndex].FindControl("DDLSalaryType")).Items.FindByText(ViewState["saltype"].ToString()).Selected = true;
                ((DropDownList)GridViewViewwages.Rows[GridViewViewwages.EditIndex].FindControl("ddlpayableat")).Items.FindByText(ViewState["payat"].ToString()).Selected = true;
                ((DropDownList)GridViewViewwages.Rows[GridViewViewwages.EditIndex].FindControl("DDLCurrencyType")).Items.FindByText(ViewState["currtype"].ToString()).Selected = true;
                ((DropDownList)GridViewViewwages.Rows[GridViewViewwages.EditIndex].FindControl("ddlruletype")).Items.FindByText(ViewState["ruletype"].ToString()).Selected = true;
                ((DropDownList)GridViewViewwages.Rows[GridViewViewwages.EditIndex].FindControl("DDLEntryType_Rpce")).Items.FindByText(ViewState["parantcode"].ToString()).Selected = true;
               
            }
        }
        catch { }

    }
    protected void GridViewViewwages_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ViewState["saltype"] = ((Label)GridViewViewwages.Rows[e.NewEditIndex].FindControl("Label1")).Text;
        ViewState["payat"] = ((Label)GridViewViewwages.Rows[e.NewEditIndex].FindControl("Label2")).Text;
        ViewState["currtype"] = ((Label)GridViewViewwages.Rows[e.NewEditIndex].FindControl("Label4")).Text;
        ViewState["ruletype"] = ((Label)GridViewViewwages.Rows[e.NewEditIndex].FindControl("Label6")).Text;
        ViewState["parantcode"] = ((Label)GridViewViewwages.Rows[e.NewEditIndex].FindControl("Label7")).Text;
        
    }

    protected void ObjectDataSource1_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
       if(Convert.ToInt32(e.ReturnValue)>0)
       {
           try
           {

              
           }
           catch (Exception ex)
           {
               //.WriteError("viewwages.aspx", "btnsave_Click", ex);
           }
       }
    }
}
