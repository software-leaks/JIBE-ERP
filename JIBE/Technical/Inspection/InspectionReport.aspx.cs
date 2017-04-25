using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.Technical;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.Technical;
using System.Net;
using System.Web.Script.Serialization;
using SMS.Properties;
using System.Threading;

using EO.Pdf;
using System.Text;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using SMS.Business.Inspection;
public partial class Technical_Worklist_InspectionReport : System.Web.UI.Page
{
    public class CatRating
    {
        public string RNO { get; set; }
        public string Description { get; set; }
        public string LastReport { get; set; }
        public string ThisReport { get; set; }
        public string Rating { get; set; }
    }
    BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
    DataTable dt = new DataTable();
    DataSet dscon = new DataSet();
    //BLL_Tec_Worklist objWl = new BLL_Tec_Worklist();
    DataSet dsCat = new DataSet();
    BLL_Infra_InspectionReportConfig objCon = new BLL_Infra_InspectionReportConfig();
    BLL_INSP_Checklist objChecklist = new BLL_INSP_Checklist();
   CatRating item = new CatRating();
   public  List<CatRating> lstRate = new List<CatRating>();
   public UserAccess objUA = new UserAccess();
   BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
   int newtab = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //UserAccessValidation();
            if (Request.QueryString["InspID"] != null && Request.QueryString["InspectorID"] != null && Request.QueryString["ScheduleID"] != null  )
            {
                if (Request.QueryString["InspID"].ToString().Trim() != "" && Request.QueryString["InspectorID"].ToString() != "" && Request.QueryString["ScheduleID"] != "")
                {
                    int ScheduleID=UDFLib.ConvertToInteger(Request.QueryString["ScheduleID"].ToString());
                    string CheckListIDS = "";// GetChecklist(ScheduleID);
                   // PullRatingFromChecklistRating(UDFLib.ConvertToInteger(Request.QueryString["InspID"].ToString()), CheckListIDS);
                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                    imgLogo.ImageUrl = baseUrl+"Images/SB.png";
                    imgOT3Logo.ImageUrl = baseUrl + "Images/SB.png";
                    imgNonOT3Logo.ImageUrl = baseUrl + "Images/SB.png";
                    GetVesselAttendance(Convert.ToInt32(Request.QueryString["InspectorID"].ToString()), UDFLib.ConvertToInteger(Request.QueryString["InspID"].ToString()));
                    GetExecutiveSummary();
                    GetCategoryRating();
                    GetLegend();
                    BindCategoryRating(Request.QueryString["InspID"].ToString(), CheckListIDS);
                    GetSubCategoryRating();
                    BindSummary( UDFLib.ConvertToInteger(Request.QueryString["InspID"].ToString()));
                    GetConditionReportOT3();
                    GetDefectNonOT3();
                  // BindCategoryWiseJobs(UDFLib.ConvertToInteger(Request.QueryString["InspID"]), 1, Request.QueryString["ReportType"].ToString(), CheckListIDS);

                    BtnUpdateCompany.Style.Add("visibility", "hidden");
                    BtnUpdateReportNo.Style.Add("visibility", "hidden");
                    BtnCancelUpdCompany.Style.Add("visibility", "hidden");
                    BtnCancelUpdRptNo.Style.Add("visibility", "hidden");


                    string js3 = "fnSubCategoryLoadComplete();";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hideText", js3, true);
                    ViewState["newtabnumber"] = newtab;
                }
            }
           
        }
    }
    public void GetLegend()
    {
        DataSet ds = objInsp.INSP_Get_RatingLegends();

        if (ds.Tables.Count > 0)
        {
            grdRatingLegend.DataSource = ds.Tables[0];
            grdRatingLegend.DataBind();
            //string Legend = "Rating guideline : ";
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    Legend+=ds.Tables[0].Rows[i]["FromRating"].ToString()+"-"+ds.Tables[0].Rows[i]["ToRating"].ToString()+" "+ ds.Tables[0].Rows[i]["Rating"].ToString()+"  ";
            //}

            //lblLegend.Text=Legend;


            for (int i = 0; i < grdRatingLegend.Rows.Count; i++)
            {
                grdRatingLegend.Rows[i].Cells[2].BackColor = Color.FromName(grdRatingLegend.Rows[i].Cells[3].Text);
            }

            grdRatingLegend.Columns[3].Visible = false;
        }

    }
    public string GetChecklist(int ScheduleID)
    {
        DataTable ds = objChecklist.Get_CheckListName(ScheduleID);
         string ChecklistID=string.Empty;
        if (ds.Rows.Count > 0)
        {
           
            for (int i = 0; i < ds.Rows.Count; i++)
            {
                if (ChecklistID != "")
                {
                    ChecklistID = ChecklistID + ","+ds.Rows[i]["ChecklistID"].ToString();
                }
                else
                {
                    ChecklistID = ds.Rows[i]["ChecklistID"].ToString();
                }

                
            }
        }

        return ChecklistID;
    }
    public void PullRatingFromChecklistRating(int InspectionID,string CheckListIDs)
    {
        try
        {

            objInsp.INSP_Get_RatingsFromChecklistRating(InspectionID, CheckListIDs);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //public void GetConfiguration()
    //{
    //    dscon = objCon.INSP_Get_ReportConfig();
    //    if (dscon.Tables[0].Rows.Count > 0)
    //    {
    //      //  grdReportConfig.DataSource = dscon.Tables[0];
    //        //grdReportConfig.DataBind();

    //        //updReportConfig.Update();


    //    }
    //}
    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUA.View == 0)
        {
            Response.Redirect("~/crew/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {

        }
    }
    public void GetExecutiveSummary()
    {
        string js2 = "GetExecutiveSummary();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ExeSummary", js2, true);
    }
    public void GetCategoryRating()
    {
        string js3 = "GetCategoryRating();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CatRating", js3, true);
    }
    public void GetConditionReportOT3()
    {
        string js3 = "GetConditionReportOT3();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "worklist", js3, true);
    }
    public void GetDefectNonOT3()
    {
        string js3 = "GetDefectNonOT3();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "worklist1", js3, true);
    }
    public void ReplaceDropDownWithLabel()
    {
        string js3 = "ReplaceDropDownWithLabel();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "worklist", js3, true);
    }
    public void GetSubCategoryRating()
    {
        //DataTable dtSubCat = new DataTable();
       // dtSubCat=objWl.GetCategoryRating()
       
            if(dsCat.Tables[0].Rows.Count>0)
            {
                for (int i = 0; i < dsCat.Tables[0].Rows.Count; i++)
                {
                    string SystemCode = dsCat.Tables[0].Rows[i][1].ToString();
                    string js3 = "GetSubCategoryRating(" + SystemCode + ");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SubCatRating"+i, js3, true);
                }
                //string js4 = " setTimeout(ReplaceDropDownWithLabel, 10000);";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "test", js4, true);
               
            }
      
    }

    //public void BindCategoryWiseJobs(int InspectionID, int ShowImages, string ReportType,string ChecklistIDs)
    //{

    //    //char[] c = ReportType.ToCharArray() ;
    //    //string RptType = c[1].ToString();
    //    //DataTable dt = objWl.INSP_Get_WorklistReportWithCategoryGrouping(InspectionID, ShowImages, RptType);
    //    //dvCatWiseJobs.InnerHtml = dt.Rows[0][0].ToString();

    //}
    public void BindCategoryRating(string InspectionID, string CheckListIDs)
    {
        try
        {
            JavaScriptSerializer j = new JavaScriptSerializer();
            dsCat = objInsp.GetCategoryRating(InspectionID);

            if (dsCat.Tables[0].Rows.Count > 0)
            {
                // dsCat.Tables[0].

                for (int i = 0; i < dsCat.Tables[0].Rows.Count; i++)
                {
                    item = new CatRating();
                    item.RNO = dsCat.Tables[0].Rows[i][0].ToString();
                    item.Description = dsCat.Tables[0].Rows[i][2].ToString();
                    item.LastReport = dsCat.Tables[0].Rows[i][3].ToString();
                    item.ThisReport = dsCat.Tables[0].Rows[i][4].ToString();
                    item.Rating = dsCat.Tables[0].Rows[i][5].ToString();

                    lstRate.Add(item);
                }

                string js4 = "drawChartSummaryRating(" + j.Serialize(lstRate) + ");";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CatRatingChart", js4, true);
               
               
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public void GetVesselAttendance(int InspectorID, int InspectionID)
    {
        dt=objInsp.Get_VesselAttendence(InspectorID, InspectionID);

        if (dt.Rows.Count > 0)
        {
            lblShipName.Text = dt.Rows[0]["Vessel_Name"].ToString();
            lblWLShip.Text = dt.Rows[0]["Vessel_Name"].ToString();
            lblCWlShip.Text = dt.Rows[0]["Vessel_Name"].ToString(); 

            lblAttendendBy.Text = dt.Rows[0]["AttendedBy"].ToString();
            lblRank.Text = dt.Rows[0]["Rank_Name"].ToString();
            lblFrom.Text = Convert.ToDateTime(dt.Rows[0]["FromDate"].ToString()).ToString("ddMMMyy");
            lblWLFrom.Text = Convert.ToDateTime(dt.Rows[0]["FromDate"].ToString()).ToString("ddMMMyy");
            lblCwlFrom.Text = Convert.ToDateTime(dt.Rows[0]["FromDate"].ToString()).ToString("ddMMMyy");
            if (dt.Rows[0]["ToDate"].ToString() != "")
            {
                lblTo.Text = Convert.ToDateTime(dt.Rows[0]["ToDate"].ToString()).ToString("ddMMMyy");
                lblWlTo.Text = Convert.ToDateTime(dt.Rows[0]["ToDate"].ToString()).ToString("ddMMMyy");
                lblCWlTo.Text = Convert.ToDateTime(dt.Rows[0]["ToDate"].ToString()).ToString("ddMMMyy");
            }
            else
            {
                lblTo.Text = "";
                lblWlTo.Text = "";
                lblCWlTo.Text = "";
            }
            lblDur.Text = dt.Rows[0]["Duration"].ToString();
            lblWlDur.Text = dt.Rows[0]["Duration"].ToString();
            lblCwlDur.Text = dt.Rows[0]["Duration"].ToString();
            lblPorts.Text = dt.Rows[0]["Port"].ToString();
            lblWLPorts.Text = dt.Rows[0]["Port"].ToString();
            lblCWlPorts.Text = dt.Rows[0]["Port"].ToString();
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            if (dt.Rows[0]["Vessel_Image"].ToString() == "" || dt.Rows[0]["Vessel_Image"].ToString() == null)
            {
                imgVesselPhoto.ImageUrl = baseUrl + "Images/noVesselimage.png"; // + dt.Rows[0]["Vessel_Image"].ToString();
            }
            else
            {
                imgVesselPhoto.Visible = true;
                imgVesselPhoto.ImageUrl = baseUrl + "Uploads/VesselImage/" + dt.Rows[0]["Vessel_Image"].ToString();
            }

            
        }
        

    }
    public void BindSummary(int InspectionID)
    {
        DataSet ds = objInsp.INSP_Get_InspectionReportInfo(InspectionID);

        if (ds.Tables[0].Rows.Count > 0)
        {

            txtReportNo.Text = ds.Tables[0].Rows[0]["ReportNo"].ToString();
            txtCompanyName.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString(); 
        }

      
    }
    //protected void BtnConfig_Click(object sender, ImageClickEventArgs e)
    //{
    //    GetConfiguration();

    //    string js4 = "ShowModalPopup('dvConfig');";
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "rptConfig", js4, true);
               
    //}
    //protected void BtnSave_Click(object sender, EventArgs e)
    //{
    //    int KeyNo, ActiveStatus;

    //    for (int i = 0; i < grdReportConfig.Rows.Count; i++)
    //    {
    //        KeyNo = Convert.ToInt32(grdReportConfig.Rows[i].Cells[0].Text);
    //        ActiveStatus = Convert.ToInt32(((CheckBox)grdReportConfig.Rows[i].Cells[3].FindControl("chkActive")).Checked);
    //        objCon.INSP_Update_InspectionReportConfigStaus(KeyNo, ActiveStatus, UDFLib.ConvertToInteger(Session["USERID"]), DateTime.Now);
    //    }
    //    string js4 = "hideModal('dvConfig');";
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "rptConfigHide", js4, true);
    //}
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        string js4 = "hideModal('dvConfig');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "rptConfigHide", js4, true);
    }
    protected void BtnUpdateCompany_Click(object sender, EventArgs e)
    {
        string ReportNo = txtReportNo.Text;
        string CompanyName = txtCompanyName.Text;


        int res = objInsp.INSP_Update_ExecutiveSummary(ReportNo, CompanyName, UDFLib.ConvertToInteger(Request.QueryString["InspID"].ToString()), UDFLib.ConvertToInteger(Session["USERID"]));
    
        BtnUpdateCompany.Style.Add("visibility", "hidden");
        BtnCancelUpdCompany.Style.Add("visibility", "hidden");
        
       
    }
    protected void BtnUpdateReportNo_Click(object sender, EventArgs e)
    {
        string ReportNo = txtReportNo.Text;
        string CompanyName = txtCompanyName.Text;


        int res = objInsp.INSP_Update_ExecutiveSummary(ReportNo, CompanyName, UDFLib.ConvertToInteger(Request.QueryString["InspID"].ToString()), UDFLib.ConvertToInteger(Session["USERID"]));

      
        BtnUpdateReportNo.Style.Add("visibility", "hidden");
        BtnCancelUpdRptNo.Style.Add("visibility", "hidden");
    }
    protected void BtnCancelUpdCompany_Click(object sender, EventArgs e)
    {
        BindSummary(UDFLib.ConvertToInteger(Request.QueryString["InspID"].ToString()));
    }
    protected void BtnCancelUpdRptNo_Click(object sender, EventArgs e)
    {
        BindSummary(UDFLib.ConvertToInteger(Request.QueryString["InspID"].ToString()));
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    private void On_AfterRenderPage(object sender, EO.Pdf.PdfPageEventArgs e)
    {
        //EO.Pdf.PdfPage page = e.Page;
        //EO.Pdf.Acm.AcmRender render = new EO.Pdf.Acm.AcmRender(page, 0, new EO.Pdf.Acm.AcmPageLayout(new EO.Pdf.Acm.AcmPadding(0, 0, 0, 0)));
        ////render.SetDefPageSize(new SizeF(EO.Pdf.PdfPageSizes.A4.Width, EO.Pdf.PdfPageSizes.A4.Height));
        //EO.Pdf.Acm.AcmBlock footer = new EO.Pdf.Acm.AcmBlock(new EO.Pdf.Acm.AcmText(""));
        //footer.Style.Border.Top = new EO.Pdf.Acm.AcmLineInfo(EO.Pdf.Acm.AcmLineStyle.Solid, Color.LightGray, 0.01f);
        //footer.Style.Top = 10.4f;
        ////footer.Style.FontName = "Arial";        
        //footer.Style.FontSize = 10f;
        //footer.Style.HorizontalAlign = EO.Pdf.Acm.AcmHorizontalAlign.Right;
        //footer.Style.BackgroundColor = Color.Blue;
        //footer.Style.ForegroundColor = Color.White;
        
        //render.Render(footer);


    }
    protected void BtnPrintPDF_Click(object sender,  EventArgs e)
    {

        EO.Pdf.HtmlToPdf.Options.PageSize = new SizeF(8.27f, 11.69f);
        
        PdfDocument doc=new PdfDocument();

        string GUID = Guid.NewGuid().ToString();
        string filePath = Server.MapPath("~/Uploads/Reports/" + GUID + ".pdf");
        //string FileName = "~/Uploads/Reports/" + GUID + ".pdf";
    
        EO.Pdf.Runtime.AddLicense("p+R2mbbA3bNoqbTC4KFZ7ekDHuio5cGz4aFZpsKetZ9Zl6TNHuig5eUFIPGe" +
      "tcznH+du5PflEuCG49jjIfewwO/o9dB2tMDAHuig5eUFIPGetZGb566l4Of2" +
      "GfKetZGbdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW6yuCwb6y9xtyxdabw" +
      "+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzr" +
      "peb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mkBxDxrODz/+ihb6W0" +
      "s8uud4SOscufWbOz8hfrqO7CnrWfWZekzRrxndz22hnlqJfo8h8=");
       // HtmlToPdf.Options.AfterRenderPage = new EO.Pdf.PdfPageEventHandler(On_AfterRenderPage);
        EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = "<div style='text-align:right; font-family:Tahoma; font-size:12px'>Page {page_number} of {total_pages}</div>";

       HtmlToPdf.Options.AutoFitX = HtmlToPdfAutoFitMode.None;
        //HtmlToPdf.Options.AutoFitY = HtmlToPdfAutoFitMode.None;

        
       // HtmlToPdf.Options.AutoAdjustForDPI=true;
      //  HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.Letter;
        string TemplateText = hdnContent.Value ;

        HtmlToPdf.ConvertHtml(TemplateText, filePath);

        newtab = UDFLib.ConvertToInteger(ViewState["newtabnumber"]);
        newtab++;
        ViewState["newtabnumber"] = newtab;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideText", "window.open('../../Uploads/Reports/" + GUID + ".pdf','INSPRPT" + newtab + "');", true);  // (  this.GetType(), "OpenWindow", "window.open('../../Uploads/InspectionReport.pdf','_newtab');", true);
        //Response.Redirect("../../Uploads/Reports/" + GUID + ".pdf");
    
    }
    public string ReturnExtension(string fileExtension)
    {
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/ms-word";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".zip":
                return "application/zip";
            case ".xls":
            case ".csv":
                return "application/vnd.ms-excel";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".wav":
                return "audio/wav";
            case ".mp3":
                return "audio/mpeg3";
            case ".mpg":
            case "mpeg":
                return "video/mpeg";
            case ".rtf":
                return "application/rtf";
            case ".asp":
                return "text/asp";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".ppt":
                return "application/mspowerpoint";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
            case ".sdxl":
                return "application/xml";
            case ".xdp":
                return "application/vnd.adobe.xdp+xml";
            default:
                return "application/octet-stream";
        }
    }



   
}