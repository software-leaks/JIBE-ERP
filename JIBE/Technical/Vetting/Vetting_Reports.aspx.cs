using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.VET;
using System.Data;


public partial class Technical_Vetting_Vetting_Reports : System.Web.UI.Page
{
    BLL_VET_Index objVetBAL = new BLL_VET_Index();
    int VettingID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {           
             VettingID=Convert.ToInt32(Request.QueryString["Vetting_ID"].ToString());
             if (!Page.IsPostBack)
             {
                 DataTable dt = objVetBAL.VET_Get_Vetting_Report(VettingID, GetSessionUserID());
                 dvVettingReport.InnerHtml = dt.Rows[0][0].ToString();
             }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Method is used to get login user id
    /// </summary>
    /// <returns>retrun user id</returns>
    private int GetSessionUserID()
    {
       
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;

    }
    protected void ImgExportToExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt = objVetBAL.VET_Get_Vetting_Report(VettingID, GetSessionUserID());
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=Vetting_Report_" + DateTime.Now.ToString() + ".xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.Write(dvVettingReport.InnerHtml);         
            HttpContext.Current.Response.End();           
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
           
        }


    }

}