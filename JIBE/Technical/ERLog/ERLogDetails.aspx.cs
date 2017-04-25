using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Operation;
using SMS.Business.Technical;
using System.IO;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Diagnostics;
using EO.Pdf;
public partial class Technical_ERLog_ERLogDetails : System.Web.UI.Page
{
    int newtab = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblLogId.Attributes.Add("style", "visibility:hidden");
        lblVesselId.Attributes.Add("style", "visibility:hidden");
        if (!IsPostBack)
        {
            if (Request.QueryString["LOGID"] != null)
            {
                lblLogId.Text = Request.QueryString["LOGID"].ToString();
                lblVesselId.Text = Request.QueryString["VESSELID"].ToString();
                BindViews();
                FillDDL();
            }
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            hdnBaseURL.Value = baseUrl;
            ViewState["newtabnumber"] = newtab;
        }
    }

    private void BindViews()
    {
        DataTable dt = BLL_Tec_ErLog.ErLog_ME_00_Get(int.Parse(lblLogId.Text), int.Parse(lblVesselId.Text));
        if (dt.Rows.Count > 0)
        {
            FormView1.DataSource = dt;
            FormView1.DataBind();
            lblMV.Text = dt.Rows[0]["VOYAGE_NUM"].ToString();
            lblfrom.Text = dt.Rows[0]["FROMPORT"].ToString();
            lblTo.Text = dt.Rows[0]["TOPORT"].ToString();
            lblDate.Text = dt.Rows[0]["LOG_DATE"].ToString();
            lblVesselName.Text = dt.Rows[0]["Vessel_Name"].ToString();

            if (dt.Rows[0]["Status"].ToString() == "UNFINALIZED")
            {
                btnRework.Visible = false;
            }
            else
            {
                btnRework.Visible = true;
            }
            if (dt.Rows[0]["Status"].ToString() == "REWORK")
            {
                btnRework.Enabled = false;
            }
            else
            {
                btnRework.Enabled = true;
            }
        }

    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            int rowcount = 0;
            Repeater fvMem = (Repeater)FormView1.Row.Cells[0].FindControl("rpEngine1");
            DataSet ds = BLL_Tec_ErLog.ErLog_Seach_All_Details(int.Parse(lblLogId.Text), int.Parse(lblVesselId.Text), 1, 1, ref  rowcount, 1, 6, null, null);


            if (fvMem != null)
            {
                fvMem.DataSource = ds.Tables[0];// BLL_Tec_ErLog.ErLog_MEngine_01_Search(int.Parse(lblLogId.Text), int.Parse(lblVesselId.Text), 1, 24, ref  rowcount);
                fvMem.DataBind();
            }
            Repeater fvDetails = (Repeater)FormView1.Row.Cells[0].FindControl("rpEngine2");
            if (fvDetails != null)
            {
                fvDetails.DataSource = ds.Tables[1];// BLL_Tec_ErLog.ErLog_ME_02_Search(int.Parse(lblLogId.Text), int.Parse(lblVesselId.Text), 1, 24, ref  rowcount);
                fvDetails.DataBind();
            }
            fvDetails = (Repeater)FormView1.Row.Cells[0].FindControl("rpEngine3");
            if (fvDetails != null)
            {
                fvDetails.DataSource = ds.Tables[2];// BLL_Tec_ErLog.ErLog_AC_FM_MISC_Search(int.Parse(lblLogId.Text), int.Parse(lblVesselId.Text),1, 24, ref  rowcount);
                fvDetails.DataBind();
            }
            fvMem = (Repeater)FormView1.Row.Cells[0].FindControl("rpTrainingDetails");
            if (fvMem != null)
            {
                fvMem.DataSource = ds.Tables[3];// BLL_Tec_ErLog.ErLog_Generator_Engine_Search(int.Parse(lblLogId.Text), int.Parse(lblVesselId.Text), 1, 24, ref  rowcount);
                fvMem.DataBind();
            }
            fvDetails = (Repeater)FormView1.Row.Cells[0].FindControl("Repeater1");
            if (fvDetails != null)
            {
                fvDetails.DataSource = ds.Tables[4];// BLL_Tec_ErLog.ErLog_Tank_Levels_Search(int.Parse(lblLogId.Text), int.Parse(lblVesselId.Text), 1, 24, ref  rowcount);
                fvDetails.DataBind();
            }
            fvDetails = (Repeater)FormView1.Row.Cells[0].FindControl("Repeater2");
            if (fvDetails != null)
            {
                fvDetails.DataSource = ds.Tables[5];// BLL_Tec_ErLog.ErLog_TASG_Search(int.Parse(lblLogId.Text), int.Parse(lblVesselId.Text),1, 24, ref  rowcount);
                fvDetails.DataBind();
            }
            fvDetails = (Repeater)FormView1.Row.Cells[0].FindControl("Repeater3");
            if (fvDetails != null)
            {
                fvDetails.DataSource = ds.Tables[6];// BLL_Tec_ErLog.ErLog_Engineer_Officer_Remarks_Search(int.Parse(lblLogId.Text), int.Parse(lblVesselId.Text), 1, 24, ref  rowcount);
                fvDetails.DataBind();
            }
        }
    }
    protected void Bexport_Click(object sender, EventArgs e)
    {
        //Clear the Response object
        Response.Clear();
        //set Response header to the Excel filename required (.xls for excel, .doc for word)
        Response.AddHeader("content-disposition", "attachment;filename=ReportOuput.doc");
        Response.Charset = "";
        // If you want the option to open the Excel 
        // file without the save option then un-comment out the line below
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //FOR EXCEL 
        //Response.ContentType = "application/vnd.xls";
        //FOR WORD
        Response.ContentType = "application/vnd.doc";
        //FOR PDF
        //Response.ContentType = "application/.pdf";
        //Declare new stringwriter and html writer
        StringWriter stringWrite = new System.IO.StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        //Strip out controls from FormView ?takes data too?
        PrepareFormViewForExport(FormView1);
        //render controls data as HTML
        FormView1.RenderControl(htmlWrite);
        //Clean out the Javascript postbacks etc.
        string html = stringWrite.ToString();
        html = Regex.Replace(html, "</?(a|A).*?>", "");
        StringReader reader = new StringReader(html);
        //Write xls document
        Response.Write(html);
        //end current Response
        HttpContext.Current.Response.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
    private void PrepareFormViewForExport(Control fv)
    {
        LinkButton lb = new LinkButton();
        Literal l = new Literal();
        string name = String.Empty;
        TextBox TB = new TextBox();

        for (int i = 0; i < fv.Controls.Count; i++)
        {
            if (fv.Controls[i].GetType() == typeof(LinkButton))
            {
                l.Text = (fv.Controls[i] as LinkButton).Text;
                fv.Controls.Remove(fv.Controls[i]);
                fv.Controls.AddAt(i, l);
            }
            else if (fv.Controls[i].GetType() == typeof(TextBox))
            {
                l.Text = (fv.Controls[i] as TextBox).Text;
                fv.Controls.Remove(fv.Controls[i]);
                fv.Controls.AddAt(i, l);
            }
            else if (fv.Controls[i].GetType() == typeof(DropDownList))
            {
                l.Text = (fv.Controls[i] as DropDownList).SelectedItem.Text;
                fv.Controls.Remove(fv.Controls[i]);
                fv.Controls.AddAt(i, l);
            }
            else if (fv.Controls[i].GetType() == typeof(CheckBox))
            {
                l.Text = (fv.Controls[i] as CheckBox).Checked ? "True" : "False";
                fv.Controls.Remove(fv.Controls[i]);
                fv.Controls.AddAt(i, l);
            }
            if (fv.Controls[i].HasControls())
            {
                PrepareFormViewForExport(fv.Controls[i]);
            }
        }
    }

    protected void btnRework_Click(object sender, EventArgs e)
    {

        String msgretv = String.Format("showModal('dvRework');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);


        ///int Result = BLL_Tec_ErLog.ErLog_Update_Status(int.Parse(lblLogId.Text), int.Parse(lblVesselId.Text),Convert.ToInt32(Session["USERID"]));
        //if (Result == 1)
        //{
        //    btnRework.Enabled = false;
        //}
        //else
        //{
        //    btnRework.Enabled = true;
        //}
    }
    protected void DDLVersion_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblLogId.Text = DDLVersion.SelectedValue;
        DataTable dt = BLL_Tec_ErLog.ErLog_ME_00_Get(int.Parse(DDLVersion.SelectedValue), int.Parse(lblVesselId.Text));
        if (dt.Rows.Count > 0)
        {
            FormView1.DataSource = dt;
            FormView1.DataBind();
            lblMV.Text = dt.Rows[0]["VOYAGE_NUM"].ToString();
            lblfrom.Text = dt.Rows[0]["FROMPORT"].ToString();
            lblTo.Text = dt.Rows[0]["TOPORT"].ToString();
            lblDate.Text = dt.Rows[0]["LOG_DATE"].ToString();
            lblVesselName.Text = dt.Rows[0]["Vessel_Name"].ToString();
            if (dt.Rows[0]["Status"].ToString() == "REWORK")
            {
                btnRework.Enabled = false;
            }
            else
            {
                btnRework.Enabled = true;
            }
        }

    }

    public void FillDDL()
    {
        try
        {


            DataTable dt = BLL_Tec_ErLog.GET_ERLOGVERSIONS(int.Parse(lblLogId.Text), int.Parse(lblVesselId.Text));
            DDLVersion.DataSource = dt;
            DDLVersion.DataTextField = "Version";
            DDLVersion.DataValueField = "LOG_ID";
            DDLVersion.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLVersion.SelectedValue = lblLogId.Text;
            if (dt.Rows.Count == 1)
            {
                versionrowlbl.Visible = false;
                versionrow.Visible = false;
                lblVersion.Visible = false;
                DDLVersion.Visible = false;
            }



            //DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            //DDLVessel.DataSource = dtVessel;
            //DDLVessel.DataTextField = "Vessel_name";
            //DDLVessel.DataValueField = "Vessel_id";
            //DDLVessel.DataBind();
            //DDLVessel.Items.Insert(0, new ListItem("--SELECT ALL--", null));
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }
    protected void btnSaveSaveRework_Click(object sender, EventArgs e)
    {

        if (txtDesc.Text.Trim().Length == 0)
        {
            txtDesc.Text = "";
            String msgretv = String.Format("showModal('dvRework');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);
            return;
        }
        string js = "closeReworkPopup();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
        int Result = BLL_Tec_ErLog.ErLog_Update_Status(int.Parse(lblLogId.Text), int.Parse(lblVesselId.Text), Convert.ToInt32(Session["USERID"]), txtDesc.Text);

        if (Result == 1)
        {
            btnRework.Enabled = false;
        }
        else
        {
            btnRework.Enabled = true;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        string js = "hideModal('dvRework');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModalJS", js, true);
    }

    protected void BtnPrintPDF_Click(object sender, EventArgs e)
    {

        EO.Pdf.HtmlToPdf.Options.PageSize = new SizeF(33.1f, 23.4f);

        PdfDocument doc = new PdfDocument();

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
        EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = "<div style='text-align:right; font-family:calibri; font-size:12px'>Page {page_number} of {total_pages}</div>";

        HtmlToPdf.Options.AutoFitX = HtmlToPdfAutoFitMode.None;
        //HtmlToPdf.Options.AutoFitY = HtmlToPdfAutoFitMode.None;


        // HtmlToPdf.Options.AutoAdjustForDPI=true;
        //  HtmlToPdf.Options.PageSize = EO.Pdf.PdfPageSizes.Letter;
        string TemplateText = hdnContent.Value;

        HtmlToPdf.ConvertHtml(TemplateText, filePath);

        newtab = UDFLib.ConvertToInteger(ViewState["newtabnumber"]);
        newtab++;
        ViewState["newtabnumber"] = newtab;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideText", "window.open('../../Uploads/Reports/" + GUID + ".pdf','INSPRPT" + newtab + "');", true);  // (  this.GetType(), "OpenWindow", "window.open('../../Uploads/InspectionReport.pdf','_newtab');", true);
        //Response.Redirect("../../Uploads/Reports/" + GUID + ".pdf");

    }
    
}