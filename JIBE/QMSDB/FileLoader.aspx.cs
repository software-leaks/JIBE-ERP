using System;
using System.Data;
using SMS.Business.QMS;
using SMS.Business.QMSDB;
using System.Web;
using System.Text;
public partial class FileLoader : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document();
    string LatestFilePath = "";
    string OppStatus = "";
    string OppUserID = "";
    string DocVersion = "";
    string DocID = "";  
     
    protected void Page_Load(object sender, EventArgs e)
    {
        lblProcedureId.Attributes.Add("style", "visibility:hidden");
       

        if (!IsPostBack)
        {
            txtProcedureDetails.config.toolbar = new object[]
         {          
             new object[] { "Print"},    
            };
            txtProcedureDetails.BodyClass = "cke_show_borders";
            txtProcedureDetails.Enabled = false;
            txtProcedureDetails.ReadOnly = true;           
            txtProcedureDetails.FilebrowserImageUploadUrl = "true";
            txtProcedureDetails.BackColor = System.Drawing.Color.LightYellow;
            txtProcedureDetails.Width = 900;

            if (Convert.ToString(Session["userid"]) == "")
            {
                Response.Write("<center><br><br><h2><font color=gray>Session is lost. Please click on the Logout option and login again.</font></h2></center>");
                Response.End();
            }
            if (Request.QueryString["DocVer"] != null)
            {
                if (Request.QueryString["DocVer"].ToString() != "")
                {
                    string[] temp = Request.QueryString["DocVer"].ToString().Split('-');
                    DocID = temp[0];
                    //DocVersion = temp[1];
                }
            }
            else
            {
                DocID = Request.QueryString["DocID"];
                lblProcedureId.Text = Request.QueryString["DocID"];
            }
           System.Data.DataTable dsFileDetails = BLL_QMSDB_Procedures.QMSDBProcedures_Edit(int.Parse(lblProcedureId.Text));

            if (dsFileDetails.Rows.Count > 0)
            {
                DataRow dr = dsFileDetails.Rows[0];
                lblDocName.Text = dr["PROCEDURE_CODE"].ToString() + "/" + dr["PROCEDURES_NAME"].ToString();
                lblCurrentVersion.Text = dr["PUBLISH_VERSION"].ToString();
                lblProcedureStatus.Text = dr["PROCEDURE_STATUS"].ToString();
               //lblDetails.Text = dr["HeaderDetails"].ToString().Replace("@PROCEDURECODE", dr["PROCEDURE_CODE"].ToString()).Replace("@publishDate", dr["PUBLISHED_DATE"].ToString()).Replace("@ApproverName", dr["PUBLISHED_BY"].ToString()).Replace("@CreatedBy", dr["CREATED_BY_USER"].ToString()) + dr["DETAILS"].ToString();
                lblDetails.Text =  dr["DETAILS"].ToString();               
               // txtProcedureDetails.Text = dr["HeaderDetails"].ToString() + dr["DETAILS"].ToString();

                lblHeader.Text = dr["HeaderDetails"].ToString().Replace("@ProcedureCode", dr["PROCEDURE_CODE"].ToString()).Replace("@publishdate", dr["PUBLISHED_DATE"].ToString()).Replace("@ApproverName", dr["PUBLISHED_BY"].ToString()).Replace("@CreatedBy", dr["CREATED_BY_USER"].ToString()).Replace("@UserName", dr["CREATED_BY_USER"].ToString()).Replace("@FolderName", dr["FOLDER_NAME"].ToString()).Replace("@historiCode",dr["PUBLISH_VERSION"].ToString()) ;
                txtProcedureDetails.Text = lblHeader.Text + dr["DETAILS"].ToString();
            }
        }
    }

    /// <summary>
    /// this a method for the convert the date in givien format by the user.
    /// </summary>
    /// <param name="strDT"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public string ConvertDateToString(string strDT, string format)
    {
        string strRetDt = strDT;
        if (strDT != "")
        {
            DateTime dt = Convert.ToDateTime(strDT);
            strRetDt = dt.ToString(format);
        }
        return strRetDt;
    }

    /// <summary>
    /// get the latest file path for the particular file id.
    /// </summary>
    /// <returns></returns>
    public string GetDocPath()
    {
        return LatestFilePath;
    }

    /// <summary>
    /// get the latest file ID for the particular file id.
    /// </summary>
    /// <returns></returns>
    public string GetDocID()
    {
        return DocID; 
    }

    public string GeProcedureID()
    {
      return lblProcedureId.Text;
      
    }

    /// <summary>
    /// get the latest file status for the latest file id.
    /// </summary>
    /// <returns></returns>
    public string GetStatus()
    {
        return OppStatus;
    }

    /// <summary>
    /// get the latest User ID for the particular check out file id.
    /// </summary>
    /// <returns></returns>
    public string CheckOutUserID()
    {
        return OppUserID;
    }

    private void printdocument()
    {
        //IntPtr hPrinter= new System.IntPtr() ;
    
        //PrinterSettings a= new PrinterSettings()
        //a.PaperSizes=   
        //Document documents = new Document();
        //documents.Paragraphs.Add(txtProcedureDetails.Text);   
        //documents.PrintPreview();    
        //string strFileName = "GenerateDocument" + ".doc";
       
        //HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);
        //StringBuilder strHTMLContent  = new StringBuilder();
        //HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.Charset = "";
        //HttpContext.Current.Response.ContentType = "application/msword";
        //strHTMLContent.Append("<p align='Center'>MY CONTENT GOES HERE</p>".ToString());
        //HttpContext.Current.Response.Write(strHTMLContent);
        //HttpContext.Current.Response.End();
        //HttpContext.Current.Response.Flush();

        //object range = Microsoft.Office.Interop.Word.WdPrintOutRange.wdPrintCurrentPage;
        //object items = Microsoft.Office.Interop.Word.WdPrintOutItem.wdPrintDocumentContent;
        //object pageType = Microsoft.Office.Interop.Word.WdPrintOutPages.wdPrintAllPages;
        //object oTrue = true;
        //object oFalse = false;
        //Microsoft.Office.Interop.Word.Document document = new Microsoft.Office.Interop.Word.Document();
        //document.PrintOut(); 
     

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        printdocument();
    }
}
