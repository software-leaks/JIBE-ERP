using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Odbc;
//using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using SMS.Business.PortageBill;
using SMS.Business.Infrastructure;
using System.Web.UI;


public partial class PortageBill_PhoneCard_KittyUpload : System.Web.UI.Page
{
    BLL_Infra_VesselLib objBLLVessel = new BLL_Infra_VesselLib();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;
            ucCustomPagerItems.PageSize = 20;           
            Load_VesselList();
            ViewState["kittyTable"] = null;           
        }
    }


    public void Import(String path)
    {
        Microsoft.Office.Interop.Excel.ApplicationClass app = new Microsoft.Office.Interop.Excel.ApplicationClass();
        Microsoft.Office.Interop.Excel.Workbook workBook = app.Workbooks.Open(path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

        Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;

        int index = 0;
        object rowIndex = 2;

        DataTable dt = new DataTable();
        dt.Columns.Add("cardnumber");
        dt.Columns.Add("pincode");
        dt.Columns.Add("units");
        dt.Columns.Add("title");
        dt.Columns.Add("subtitle");


        DataRow row;

        while (((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2 != null)
        {
            rowIndex = 2 + index;
            row = dt.NewRow();
            row[0] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2);
            row[1] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 2]).Value2);
            row[2] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 3]).Value2);
            row[3] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 4]).Value2);
            row[4] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 5]).Value2);
            index++;
            dt.Rows.Add(row);
        }
        app.Workbooks.Close();
      //  return dt;

        for (int i = dt.Rows.Count - 1; i >= 0; i--)
        {
            if ( string.IsNullOrEmpty(dt.Rows[i]["cardnumber"].ToString()))
            {
                dt.Rows[i].Delete();
            }
        }
        dt.AcceptChanges();

        if (dt.Rows.Count > 0)
        {
            int num;
            int num2;
            int dec = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if ((int.TryParse(dr[0].ToString(), out num)) && (int.TryParse(dr[2].ToString(), out num2)) && (dr[0].ToString().Length == 7) && (dr[1].ToString().Length == 4) && (num2 < 32766))
                {
                    
                }
                else
                {
                    dec++;
                }
            }
            if (dec == 0)
            {
                gvUploadKitty.DataSource = dt;
                gvUploadKitty.DataBind();
                ViewState["kittyTable"] = dt;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", "alert('Card Number/Units are not correct !!\\nCard Number should be a 7 digit number!!\\nCard PIN should be a 4 digit number!!')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", "alert('File contains no data')", true);
        }
        //Delete temporary Excel file from the Server path       
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = "";
            DataTable dt = new DataTable();
            dt = objUploadFilesize.Get_Module_FileUpload("CDOC_");
            //Check file is available in File upload Control
            if (dt.Rows.Count > 0)
            {
                string datasize = dt.Rows[0]["Size_KB"].ToString();
                if (FileUpload1.HasFile)
                {            //Store file name in the string variable 
                    if (FileUpload1.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                    {
                        string filename = FileUpload1.FileName;            //Save file upload file in to server path for temporary      
                        Session["filename"] = filename;
                        string fileExt = filename.Substring(filename.LastIndexOf('.')).ToLower();
                        string newFileName = DateTime.Now.ToString("yyyyMMddhhmm") + "_" + filename;
                        FileUpload1.SaveAs(Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\Uploads\\ChatCardKitty\\" + newFileName));           //Export excel data into Gridview using below method                
                        if (fileExt == ".xls" || fileExt == ".xlsx")
                        {

                            Import(Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\Uploads\\ChatCardKitty\\" + newFileName));

                        }
                    }
                    else
                    {

                        lblMessage.Text = datasize + " KB File size exceeds maximum limit";
                    }
                }
            }
            else
            {

                string js2 = "alert('Upload size not set!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);


            }
        }
        catch
        {
        }
    }
     public void ExportToGrid(String path)   
     {
         try
         {
             System.Data.DataTable dt = new System.Data.DataTable();
             OleDbConnection MyConnection = null;
             DataSet DtSet = null;
             OleDbDataAdapter MyCommand = null;        //Connection for MS Excel 2003 .xls format       
             string sheetname = "Sheet1";
             // MyConnection = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + path + "';Extended Properties=Excel 8.0;");              
             //Connection for .xslx 2007 format        //

             
             if (path.Substring(path.LastIndexOf('.')).ToLower() == ".xlsx")
             {
                 MyConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Extended Properties=Excel 12.0;");                           //Select your Excel file       
             }
             else
             {
                 MyConnection = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + path + "';Extended Properties= 'Excel 8.0;HDR=YES;IMEX=1;';");
             }
             MyConnection.Open();
             System.Data.DataTable schemaTable = MyConnection.GetOleDbSchemaTable(
                OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
             if (schemaTable.Rows.Count > 0)
                 sheetname = schemaTable.Rows[0]["TABLE_NAME"].ToString();
             MyConnection.Close();

        
             MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [" + sheetname + "]", MyConnection);
             DtSet = new System.Data.DataSet();        //Bind all excel data in to data set      
             MyCommand.Fill(DtSet, "[" + sheetname + "]");
             dt = DtSet.Tables[0];
             MyConnection.Close();        //Check datatable have records       
             if (dt.Rows.Count > 0)
             {
                 gvUploadKitty.DataSource = dt;
                 gvUploadKitty.DataBind();
                 ViewState["kittyTable"] = dt;
             }        //Delete temporary Excel file from the Server path       
             if (System.IO.File.Exists(path))
             {
                 System.IO.File.Delete(path);
             }
            
         }
         catch (Exception ex)
         {
             string js = "alert('The uploaded file is not in correct format !!');";  //"alert('" + ex + "');"; 
             ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
         }
     }
    


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string cardno = string.Empty;
        string Ucardno = string.Empty;
        string ecardno = string.Empty;
        try
        {
           
            if (ViewState["kittyTable"] != null)
            {
                string js = "";
                int j = 0;
                int k = 0;
                DataTable dt = (DataTable)ViewState["kittyTable"];
                if (dt.Rows.Count > 0)
                {


                    foreach (DataRow dr in dt.Rows)
                    {
                       
                         
                                int i = BLL_PB_PhoneCard.PhoneCard_Kitty_Insert(int.Parse(dr[0].ToString()), dr[1].ToString(), int.Parse(dr[2].ToString()), dr[3].ToString(), dr[4].ToString(), Session["filename"].ToString(), 1, UDFLib.ConvertToInteger(ddlVessels.SelectedValue), UDFLib.ConvertIntegerToNull(uc_SupplierListRFQ.SelectedValue));
                                if (i == -1)
                                {
                                    j = 1;
                                    cardno += dr[0].ToString() + ",";
                                }
                                else
                                {
                                    k = 1;
                                    Ucardno += dr[0].ToString() + ",";
                                }
                           
                       
                    }

                    if (j == 1 && k==0)
                    {
                        js = "alert( 'Card Number " + cardno.TrimEnd(',') + " already  present !!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
                    }
                    else
                        if (j == 1 & k==1)
                        {
                           
                            if (!string.IsNullOrEmpty(cardno))
                            {
                                js = "alert( 'Card Number " + Ucardno.TrimEnd(',') + " Uploaded !!\\nCard Number " + cardno.TrimEnd(',') + " already present !! ');";
                            }
                           
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
                        }
                        else
                            if (j == 0 & k == 1)
                        {
                            js = "alert('Kitty has been uploaded  !!'); window.close();";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", js, true);
                        }
                     


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", "alert('File contains no data')", true);
                }
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script2", "alert('Card Number " + ecardno.TrimEnd(',') + " is invalid')", true);
        }
    }

    public void Load_VesselList()
    {
        int Fleet_ID = 0;
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        
        ddlVessels.DataSource = objBLLVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessels.DataTextField = "VESSEL_NAME";
        ddlVessels.DataValueField = "VESSEL_ID";
        ddlVessels.DataBind();
        ddlVessels.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlVessels.SelectedIndex = 0;
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        string sFileName = "eChatCard_Template.xls";
        ResponseHelper.Redirect("../../Crew/DownloadFile.aspx?url=" + "../PortageBill/PhoneCard/" + sFileName, "_blank", "");        
    }
}
