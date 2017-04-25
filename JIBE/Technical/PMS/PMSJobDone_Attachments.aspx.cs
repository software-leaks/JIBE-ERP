using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.PMS;
using System.IO;
using SMS.Business.PURC;

public partial class PMSJobDone_Attachments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindJobDoneAttachments();
        }
    }
    BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
    private void BindJobDoneAttachments()
    {
        BLL_PMS_Job_Status objJob = new BLL_PMS_Job_Status();

      //  DataTable dt = objJob.TecJobGetJobDoneAttachment(Convert.ToInt32(Request.QueryString["JobHistoryID"].ToString()), Convert.ToInt32(Request.QueryString["Vessel_ID"].ToString()));

        DataTable dt = objBLLPurc.GET_JOB_DONE_ATTACHMENT(Convert.ToInt32(Request.QueryString["Vessel_ID"].ToString())
               , null
               , Convert.ToInt32(Request.QueryString["JobHistoryID"].ToString()), UDFLib.ConvertToInteger(Request.QueryString["OFFICE_ID"]));

        if (dt.Rows.Count > 0)
        {
            //Bind the header

            txtVessel.Text = dt.Rows[0]["Vessel_Name"].ToString();
            txtJobTitle.Text = dt.Rows[0]["Job_Title"].ToString();
            txtJobDesc.Text = dt.Rows[0]["Job_Description"].ToString();

            txtFrequency.Text = dt.Rows[0]["FrequencyName"].ToString();
            txtDateDone.Text = dt.Rows[0]["JOB_DONE"].ToString();
            txtRHoursDone.Text = dt.Rows[0]["Rhrs_Done"].ToString();
            txtRemark.Text = dt.Rows[0]["Remarks"].ToString();

            DataView dvImage = dt.DefaultView;
            dvImage.RowFilter = "Is_Image='1' ";


            ListView1.DataSource = dvImage;
            ListView1.DataBind();
            ListView2.DataSource = dvImage;
            ListView2.DataBind();
            hidenTotalrecords.Value = dvImage.Count.ToString();
            HCurrentIndex.Value = "0";
            if (dvImage.Count == 0)
            {
                 tdg.Visible = false;
            }
            else
            {
                tdg.Visible = true;
            }

            dt.DefaultView.RowFilter = "Is_Image='0'  ";
            rptDrillImages.DataSource = dt.DefaultView;
            rptDrillImages.DataBind();


        }
    }

  

    
}