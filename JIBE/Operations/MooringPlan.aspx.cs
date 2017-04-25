using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Operation;

public partial class Operations_MooringPlan : System.Web.UI.Page
{
    int _Vessel_Id = 0;
    int _Office_Id = 0;
    int _PortInfoReportId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["VesselId"] != null && Request.QueryString["PortInfoReportId"] != null && Request.QueryString["OfficeId"] != null)
        {
            _Vessel_Id = int.Parse(Request.QueryString["VesselId"].ToString());
            _PortInfoReportId = int.Parse(Request.QueryString["PortInfoReportId"].ToString());
            _Office_Id = int.Parse(Request.QueryString["OfficeId"].ToString());  
            DataSet ds = BLL_OPS_VoyageReports.GET_MOOR_Attachment_List(_Vessel_Id, _PortInfoReportId,_Office_Id);
            DataRow[] imgrow = ds.Tables[0].Select("FileType = 'IMAGE'");

            if(imgrow.Length>0)
            imgMooringPlan.ImageUrl = "../Uploads/MOOR/" + imgrow[0]["File_Path"].ToString();
            if (ds.Tables[0].Rows.Count>0)
            txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
            int Vessel_Id = 0;
            int Page_Type_Id = 3;
            int Report_Id = 0; 
            Vessel_Id = int.Parse(Request.QueryString["VesselId"].ToString());
            Report_Id = int.Parse(Request.QueryString["PortInfoReportId"].ToString()); 
            DataTable dt = BLL_OPS_VoyageReports.Get_PRT_Attachment(Page_Type_Id, Report_Id, Vessel_Id); 
            dt.DefaultView.RowFilter = "MoorSide='L'";
            DataView dvLeft = dt.DefaultView;
            imgListLeft.DataSource = dvLeft;
            imgListLeft.DataBind(); 
            dt.DefaultView.RowFilter = "MoorSide='R'";
            DataView dvRight = dt.DefaultView;
            imgListRight.DataSource = dvRight;
            imgListRight.DataBind(); 
          
           
        }
    }
    protected string GetImage(string ImgName)
    {
        if (System.IO.Path.GetExtension(ImgName) == ".bmp" || System.IO.Path.GetExtension(ImgName) == ".png" || System.IO.Path.GetExtension(ImgName) == ".jpg" || System.IO.Path.GetExtension(ImgName) == ".gif")
        {
            return ImgName;

        }
        else
        {
            return "~/Images/DocTree/" + System.IO.Path.GetExtension(ImgName).Replace(".", "") + ".png";
        }
    }
    protected System.Web.UI.WebControls.Unit GetImageWidth(string ImgName)
    {
        if (System.IO.Path.GetExtension(ImgName) == ".bmp" || System.IO.Path.GetExtension(ImgName) == ".png" || System.IO.Path.GetExtension(ImgName) == ".jpg" || System.IO.Path.GetExtension(ImgName) == ".gif")
        {
            return System.Web.UI.WebControls.Unit.Pixel(240);

        }
        else
        {
            return System.Web.UI.WebControls.Unit.Pixel(40);
        }
    }
    protected void btnViewAttachments_Click(object sender, EventArgs e)
    {
        int Vessel_Id = 0;
        int Page_Type_Id = 3;
        int Report_Id = 0;

        Vessel_Id = int.Parse(Request.QueryString["VesselId"].ToString());
        Report_Id = int.Parse(Request.QueryString["PortInfoReportId"].ToString());

        DataTable dt = BLL_OPS_VoyageReports.Get_PRT_Attachment(Page_Type_Id, Report_Id, Vessel_Id);

        //grdPTR_Attachment.DataSource = dt;
        //grdPTR_Attachment.DataBind();

        string msgViewAttachments = string.Format("showModal('divViewAttachments',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgViewAttachments", msgViewAttachments, true);
        UpdatePnl.Update();
    }
}