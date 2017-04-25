using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Inspection;
public partial class Technical_Worklist_InspectionWorklistReport : System.Web.UI.Page
{
    
    BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            if (Request.QueryString["InspID"] != null && Request.QueryString["ReportType"]!=null)
            {
                BindWorklistReport(Convert.ToInt32(Request.QueryString["InspID"]), 1, Request.QueryString["ReportType"].ToString());
            }
            

            
        }
    }


    public void BindWorklistReport(int InspectionID,int ShowImages,string ReportType)
    {
        ds = objInsp.INSP_Get_WorklistReport(InspectionID);

            if(ds.Tables[0].Rows.Count>0)
            {
              //  grdWorklistReport.DataSource=ds.Tables[0];
               // grdWorklistReport.DataBind();


                lblVesselName.Text = ds.Tables[0].Rows[0]["PORT_NAME"].ToString();
                lblPortName.Text = ds.Tables[0].Rows[0]["Vessel_Name"].ToString();
                lblFromDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["FromDate"].ToString()).ToString("dd MMM yyyy");
                lblToDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ToDate"].ToString()).ToString("dd MMM yyyy");
                lblDate.Text=DateTime.Now.ToString("dd MMM yyyy");
                lblInspectorName.Text = ds.Tables[0].Rows[0]["Inspector"].ToString();

            }


            DataTable dt = objInsp.INSP_Get_WorklistReportWithImages(UDFLib.ConvertToInteger(InspectionID), UDFLib.ConvertToInteger(ShowImages), ReportType);
                dvReport.InnerHtml = dt.Rows[0][0].ToString();
                if (dt.Rows[0][0].ToString() == "")
                {
                    tblFoot.Visible = false;
                }
                else
                {
                    tblFoot.Visible = true;
                }

          

    }
}