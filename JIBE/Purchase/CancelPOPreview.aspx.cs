using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.POLOG;
using System.IO;
using SMS.Properties;
using System.Text;
using EO.Pdf;
using System.Drawing;
using SMS.Business.PURC;
using EO.Pdf.Acm;
using ClsBLLTechnical;

public partial class Purchase_CancelPOPreview : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    TechnicalBAL objtechBAL = new TechnicalBAL();
    #region Variable 
    string QTORDCode = "";
    string DocCode = "";
    double TotalAmt = 0;
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            hdnHost.Value = Request.Url.AbsoluteUri.ToString().Substring(0, Request.Url.AbsoluteUri.ToString().ToLower().IndexOf("/purchase/cancelpopreview.aspx")) + "/";
            if (!IsPostBack)
            {
                if (Request.QueryString["Quotation_code"] != null)
                {
                    QTORDCode = Request.QueryString["Quotation_code"];
                    DocCode = "0";
                }
                else if (Request.QueryString["ORDER_CODE"] != null)
                {
                    QTORDCode = Request.QueryString["ORDER_CODE"];
                    DocCode = "1";
                }
                GridViewHelper helper = new GridViewHelper(this.gvReqsnItems);
                helper.RegisterGroup("Subsystem_Description", true, true);
                helper.GroupHeader += new GroupEvent(helper_GroupHeader);

                BindPODetails();
            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        
    }
    #endregion

    #region Group Header
    /// <summary>
    /// To Group Item Subsystem in header file.
    /// </summary>
    /// <param name="groupName"></param>
    /// <param name="values"></param>
    /// <param name="row"></param>
    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == "Subsystem_Description")
        {
            row.BackColor = System.Drawing.Color.WhiteSmoke;

            row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
            row.Cells[0].ForeColor = System.Drawing.Color.Black;
            row.Cells[0].Font.Bold = true;

        }

    }
    #endregion

    #region Generate PDF Format
    /// <summary>
    /// To Format and generate Pdf and save in specified location.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnrp_Click(object sender,EventArgs e)
    {
        try
        {
            string TemplateText = hdnReport.Value;


            EO.Pdf.HtmlToPdf.Options.PageSize = new SizeF(11.0f, 11.69f);
            string GUID = Guid.NewGuid().ToString();


            string filePath = Server.MapPath("~/Uploads/Purchase/" + GUID + ".pdf");

            string savefilepath = "Uploads/Purchase/" + GUID + ".pdf";

            EO.Pdf.Runtime.AddLicense("p+R2mbbA3bNoqbTC4KFZ7ekDHuio5cGz4aFZpsKetZ9Zl6TNHuig5eUFIPGe" +
          "tcznH+du5PflEuCG49jjIfewwO/o9dB2tMDAHuig5eUFIPGetZGb566l4Of2" +
          "GfKetZGbdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW6yuCwb6y9xtyxdabw" +
          "+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzr" +
          "peb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mkBxDxrODz/+ihb6W0" +
          "s8uud4SOscufWbOz8hfrqO7CnrWfWZekzRrxndz22hnlqJfo8h8=");
 
            HtmlToPdf.ConvertHtml(TemplateText, filePath);
            PdfDocument doc = new PdfDocument(filePath);
            
            AcmRender render = new AcmRender(doc);
            render.BeforeRenderPage += new AcmPageEventHandler(render_BeforeRenderPage);
            EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = "<div style='text-align:center; font-family:Tahoma; font-size:12px'>Page {page_number} of {total_pages}</div>";
            EO.Pdf.HtmlToPdf.Options.AutoFitX = HtmlToPdfAutoFitMode.None;
            AcmContent acmCon = new AcmContent();
            render.Render(acmCon);
            
            doc.Save(filePath);
            BLL_PURC_Common.PURC_UPD_CancelPO_FilePath(Convert.ToString(Request.QueryString["ORDER_CODE"]), "", savefilepath, "", Convert.ToInt32(Session["userid"].ToString()));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #endregion

    #region Eo Pdf render_BeforeRenderPage 
    /// <summary>
    /// To Add Watermark inside Pdf file.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	void render_BeforeRenderPage(object sender, AcmPageEventArgs e)
    {
        try
        {
            PdfDocument doc = e.Page.Document;
            foreach (PdfPage page in doc.Pages)
            {
                //Use a big font, light text color and also
                //rotate the text 45 degrees
                EO.Pdf.Contents.PdfTextLayer textLayer = new EO.Pdf.Contents.PdfTextLayer();
                textLayer.Font = new EO.Pdf.Drawing.PdfFont("Verdana", 30);
                textLayer.NonStrokingColor = Color.Red;
                textLayer.GfxMatrix.Rotate(28);

                //Create the text object
                EO.Pdf.Contents.PdfTextContent content1 = new EO.Pdf.Contents.PdfTextContent("CANCELLED");
                content1.PositionMode = EO.Pdf.Contents.PdfTextPositionMode.Offset;
                content1.Offset = new EO.Pdf.Drawing.PdfPoint(450, 150);

                //Add the text object into the text layer object
                textLayer.Contents.Add(content1);

                //Add the text layer into the page
                page.Contents.Add(textLayer);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        
        
    }
    #endregion

    #region Bind Data
    protected void BindPODetails()
    {
        DataSet ds = new DataSet();
        DocCode = "0";
        try
        {
          
            using (BLL_PURC_Purchase Obj = new BLL_PURC_Purchase())
            {
                ds = Obj.CancelPODetails(Request.QueryString["RFQCODE"].ToString(), QTORDCode, "", DocCode, Request.QueryString["Vessel_Code"].ToString());

            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                Imgheader.ImageUrl = Server.MapPath("~/Images/company_logo.jpg");
                lblCName.Text = Convert.ToString(Session["Company_Name_GL"]); //ds.Tables[0].Rows[0]["Company_Name"].ToString();
                lblCAddress.Text = Convert.ToString(Session["Company_Address_GL"]);//ds.Tables[0].Rows[0]["Company_Address"].ToString();

                lblAgentDtl.Text = Convert.ToString(ds.Tables[0].Rows[0]["Agent_Details"]);
                lblIssueToName.Text = Convert.ToString(ds.Tables[0].Rows[0]["SHORT_NAME"]);
                
                lblIssueByname.Text = Convert.ToString(ds.Tables[0].Rows[0]["VesselOwner"]);
                lblIssueByAddress.Text = Convert.ToString(ds.Tables[0].Rows[0]["ADDRESS"]);
                lblPICName.Text = Convert.ToString(ds.Tables[0].Rows[0]["UserName"]);
                lblPICMobile.Text = Convert.ToString(ds.Tables[0].Rows[0]["Mobile_Number"]);
                lblVesselName.Text = Convert.ToString(ds.Tables[0].Rows[0]["VesselName"]);
                lblReqsnNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["Requisition_code"]);
                lblPOCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["ORDER_CODE"]);
                lblPODate.Text = Convert.ToString(ds.Tables[0].Rows[0]["PODATE"]);
                lblQuCurrency.Text = Convert.ToString(ds.Tables[0].Rows[0]["Currency"]);
                lblSupplierRef.Text = Convert.ToString(ds.Tables[0].Rows[0]["Supplier_Quotation_Reference"]);
                lblTypeExp.Text = Convert.ToString(ds.Tables[0].Rows[0]["Budget_Name"]);
                lblDeliveryPort.Text = Convert.ToString(ds.Tables[0].Rows[0]["DELIVERY_PORT"]);
                lblVesselETA.Text = Convert.ToString(ds.Tables[0].Rows[0]["REQUISITION_ETA"]);
                lblVesselETD.Text = Convert.ToString(ds.Tables[0].Rows[0]["REQUISITION_ETD"]);
                lblmARKING.Text = Convert.ToString(ds.Tables[0].Rows[0]["Name_Dept"]);
                lbldeliveryInst.Text = Convert.ToString(ds.Tables[0].Rows[0]["Delivery_Instructions"]);
                lbldvTerm.Text = Convert.ToString(ds.Tables[0].Rows[0]["Supplier_DeliveryTerm"]);
                lblOrigin.Text = Convert.ToString(ds.Tables[0].Rows[0]["Delivery_Origin"]);

                lblBargeCost.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Barge_Workboat_Cost"]).ToString("N2");
                lblTruck.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Truck_Cost"]).ToString("N2");
                lblVat.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["VAT"]).ToString("N2");
                lblPkgHandling.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Packing_Handling_Charges"]).ToString("N2");
                lblOtherCost.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Other_Charges"]).ToString("N2");
                lblFrieght.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Freight_Cost"]).ToString("N2");
                lblDiscTotalPrice.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["DISCOUNT"]).ToString("N2");
                lblPOLegalTerm.Text = Convert.ToString(ds.Tables[0].Rows[0]["LegalTerm"]).Replace("\n", "<br/>");
                

                gvReqsnItems.DataSource = ds.Tables[0];
                gvReqsnItems.DataBind();

                var results =from row in ds.Tables[0].AsEnumerable()
                             group row by row.Field<string>("ITEM_SUBSYSTEM_CODE") into grp
                             select new
                             {
                                    key = grp.Key,
                                    TotalPrice = grp.Sum(r => r.Field<decimal>("Total_Price"))
                             };
                foreach (var res in results)
                {
                    lblTotalPrice.Text = res.TotalPrice.ToString("N2");
                }
                lblNet.Text =Convert.ToDouble(Convert.ToDouble(lblTotalPrice.Text) + Convert.ToDouble(lblBargeCost.Text) + Convert.ToDouble(lblTruck.Text) + Convert.ToDouble(lblVat.Text) + Convert.ToDouble(lblPkgHandling.Text)
                                +Convert.ToDouble(lblOtherCost.Text)+ Convert.ToDouble(lblFrieght.Text) + Convert.ToDouble(lblDiscTotalPrice.Text)).ToString("N2");

                String msg = "SavePDf();";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg16", msg, true);

            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    #endregion


  
}