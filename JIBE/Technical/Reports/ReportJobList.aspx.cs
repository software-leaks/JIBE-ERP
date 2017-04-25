using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Technical;
using System.Data;


public partial class Technical_Reports_ReportJobList : System.Web.UI.Page
{

    BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string strCommand = "";

            if (Session["strQuery"]!= null)
            strCommand =Session["strQuery"].ToString();

            DataTable dtSearchResult = objBLL.Get_FilterWorklist(strCommand).Tables[0];

            grdJoblist.DataSource = dtSearchResult;
            grdJoblist.DataBind();

            if (Request.QueryString["Export"] != null)
            {                
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment; filename=JobList.xls;");
            }

            lblDt.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }
        catch (Exception ex)
        {
            string js = "alert('Error in loading data!! Error: " + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void Load_GridData()
    {

    }
}