using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Properties;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.Operations;
using SMS.Business.QMS;
using System.Web.UI.HtmlControls;
using System.IO;
using SMS.Business.LMS;
using System.Reflection;
using System.Management;
using System.Diagnostics;

public partial class QMSSyncApproval : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document();
    BLL_Infra_Company objCompBLL = new BLL_Infra_Company();

    UserAccess objUA = new UserAccess();

    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
      UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            LoadCombo();
            BindGrid();
        }

    }
    protected void LoadCombo()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PID");
            dt.Columns.Add("SizeText");
            dt.Rows.Add(0, "0  to 20 KB");
            dt.Rows.Add(1, "21 to 30 KB");
            dt.Rows.Add(2, "31 to 50 KB");
            dt.Rows.Add(3, "51 to 100 KB");
            dt.Rows.Add(4, "101 KB to (Max Limit)");
            dt.Rows.Add(5, "Bigger Than (Max Limit)");
            ddlSizeRange.DataSource = dt;
            ddlSizeRange.DataTextField = "SizeText";
            ddlSizeRange.DataValueField = "PID";
            ddlSizeRange.DataBind();
            ddlSizeRange.Select("0");
            ddlSizeRange.Select("1");
            ddlSizeRange.Select("2");
            ddlSizeRange.Select("3");
            ddlSizeRange.Select("4");
            ddlSizeRange.Select("5");
        }
        catch (Exception)
        {
            
            throw;
        }
   

        
          
    }
    public void BindGrid()
    {

        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            int DownloadRequired = 0;
            if (chkRequireSync.Checked)
                DownloadRequired = 1;
            DataTable dt = objQMS.QMS_SP_Files_SyncApproval_Search(txtfilter.Text != "" ? txtfilter.Text : null, Convert.ToInt32(optApprove.SelectedValue), null
                , sortbycoloumn, sortdirection
                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, DownloadRequired, (DataTable)ddlSizeRange.SelectedValues, ref  rowcount);


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            if (dt.Rows.Count > 0)
            {
                gvQMSFile.DataSource = dt;
                gvQMSFile.DataBind();
            }
            else
            {
                gvQMSFile.DataSource = dt;
                gvQMSFile.DataBind();
            }
        }
        catch (Exception)
        {
            
            throw;
        }

      

       

    }

    protected void UserAccessValidation()
    {
        try
        {
            int CurrentUserID = GetSessionUserID();
            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

            if (objUA.View == 0)
                Response.Redirect("~/default.aspx?msgid=1");

            if (objUA.Edit == 1) uaEditFlag = true;

            if (objUA.Delete == 1) uaDeleteFlage = true;
        }
        catch (Exception)
        {
            
            throw;
        }
      


    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }



    

    protected void lblLogFileID_onClick(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            string filePath = gvQMSFile.DataKeys[row.RowIndex].Value.ToString();

            btn.PostBackUrl = "../QMS/" + filePath;
        }
        catch (Exception)
        {
            
            throw;
        }
     


    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {

        try
        {
            foreach (GridViewRow row in gvQMSFile.Rows)
            {
                if (((CheckBox)row.FindControl("chkStatus")).Checked == true && ((CheckBox)row.FindControl("chkStatus")).Enabled == true)
                {
                    Label lblID = (Label)row.FindControl("lblID");
                    Label lblFilePath = (Label)row.FindControl("lblFilePath");
                    Label lblVersion = (Label)row.FindControl("lblVersion");


                    int retval = objQMS.QMS_SP_File_Sync(UDFLib.ConvertToInteger(lblID.Text), UDFLib.ConvertToInteger(Session["USERID"].ToString()), null
                                        , UDFLib.ConvertToInteger(Session["USERID"]), UDFLib.ConvertToInteger(lblVersion.Text.Trim()));

                    string SourcePath = Server.MapPath(lblFilePath.Text);
                    string DestinationPath = Server.MapPath("~/uploads/QMS");
                    try
                    {
                        File.Copy(SourcePath, Path.Combine(DestinationPath, Path.GetFileName(SourcePath)));
                    }
                    catch (Exception)
                    {

                        continue;
                    }


                }
            }

            BindGrid();
        }
        catch (Exception)
        {
            
            throw;
        }
      

    }


    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            txtfilter.Text = "";
            optApprove.SelectedValue = "0";

            BindGrid();
        }
        catch (Exception)
        {
            
            throw;
        }
        
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            int DownloadRequired = 0;
            if (chkRequireSync.Checked)
                DownloadRequired = 1;

            DataTable dt = objQMS.QMS_SP_Files_SyncApproval_Search(txtfilter.Text != "" ? txtfilter.Text : null, Convert.ToInt32(optApprove.SelectedValue), null
                , sortbycoloumn, sortdirection
                 , null, null, DownloadRequired, (DataTable)ddlSizeRange.SelectedValues, ref  rowcount);


            string[] HeaderCaptions = { "File Name", "Created Date", "Version", };
            string[] DataColumnsName = { "LogFileID", "LogDate", "Version", };

            GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "QMSSyncApproval", "QMS Sync Status", "");
        }
        catch (Exception)
        {
            
            throw;
        }
       

    }

    protected void gvQMSFile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton lblLogFileID = (LinkButton)e.Row.FindControl("lblLogFileID");

                Label lblFilePath = (Label)e.Row.FindControl("lblFilePath");

                lblLogFileID.Attributes.Add("onclick", "DocOpen('" + lblFilePath.Text + "'); return false;");

            }


            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (ViewState["SORTBYCOLOUMN"] != null)
                {
                    HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                    if (img != null)
                    {
                        if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                            img.Src = "~/purchase/Image/arrowUp.png";
                        else
                            img.Src = "~/purchase/Image/arrowDown.png";

                        img.Visible = true;
                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
            }
        }
        catch (Exception)
        {
            
            throw;
        }
      
 

    }

    protected void gvQMSFile_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;

            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;

            BindGrid();
        }
        catch (Exception)
        {
            
            throw;
        }
      
    }
    protected void btnDownloadFiles_Click(object sender, EventArgs e)
    {
        try
        {
            if (File.Exists(Server.MapPath("~/QMS/" + "QMSFILEz.rar")))
            {
                File.Delete(Server.MapPath("~/QMS/" + "QMSFILEz.rar"));
            }


            int rowcount = ucCustomPagerItems.isCountRecord;

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            int DownloadRequired = 0;
            if (chkRequireSync.Checked)
                DownloadRequired = 1;
            DataTable dt = objQMS.QMS_SP_Files_SyncApproval_Search(txtfilter.Text != "" ? txtfilter.Text : null, Convert.ToInt32(optApprove.SelectedValue), null
                , sortbycoloumn, sortdirection
                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, DownloadRequired, (DataTable)ddlSizeRange.SelectedValues, ref  rowcount);
            int rowindex = 0;
            string zipFile = "";
            List<string> zips = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                //   string itempath = Server.MapPath("~/"+row["FilePath"].ToString());
                zips.Add(row["FilePath"].ToString());
                rowindex++;
            }

            if (zips.Count > 0)
            {
                var ac = Server.MapPath("~/QMS/");

                zipFile = RARQMS(Server.MapPath("~/QMS/"), zips);



                string DownloadFileName = "QmsAttachmentPage" + "-" + ucCustomPagerItems.Page + "_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "h" + DateTime.Now.Minute + "m" + DateTime.Now.Second + "s" + ".rar";
                if (File.Exists(Server.MapPath("~/QMS/" + zipFile)))
                {
                    File.Move(Server.MapPath("~/QMS/" + zipFile), Server.MapPath("~/Uploads/Temp/" + DownloadFileName));

                    ResponseHelper.Redirect("~/Uploads/Temp/" + DownloadFileName, "blank", "");

                }
                else
                {
                    string msgmodal = String.Format("alert('Files not present on the Server.')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", msgmodal, true);


                }
            }
        }
        catch (Exception)
        {
            
            throw;
        }

       

    }

    protected string RARQMS(string path, List<string> filenames)
    {
        //Create Bat file

        string rarFileName = "";
        if (!path.EndsWith(@"\"))
            path = path + @"\";
        string FileNameWithoutExtension = "QMSFILEz";
        string batfilename = path + "QMSFILEz.bat";




        try
        {
            TextWriter tw = new StreamWriter(batfilename);
            tw.WriteLine(batfilename.Substring(0, 1) + ":");
            tw.WriteLine(@"cd\");
            tw.WriteLine("cd " + path);


            if (filenames.Count > 0)
                for (int i = 0; i < filenames.Count; i++)
                {
                    if (File.Exists(path + filenames[i]))
                        tw.WriteLine("rar a -ep  " + FileNameWithoutExtension + @".rar   """ + filenames[i] + @"""");
                }
            //foreach (var filename in filenames)
            //{

            //}


            tw.Close();

            // create cmd process and execute bat file
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = true;
            proc.StartInfo.FileName = batfilename;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
            proc.Close();
            rarFileName = FileNameWithoutExtension + ".rar";
            return rarFileName;
        }
        catch (Exception ex)
        {
            string filePath = @"C:\Error.txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
            //Delete bat file
            throw ex;
        }
        finally
        {
            //Delete bat file
            if (File.Exists(batfilename))
                File.Delete(batfilename);
            //if (File.Exists(path + "\\" + filename + ".txt"))
            //    File.Delete(path + "\\" + filename + ".txt");
        }


    }
   

}