using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SMS.Business.FMS;
using System.Management;
using SMS.Business.Infrastructure;
using System.IO;

public partial class CheckInForm : System.Web.UI.Page
{
    BLL_FMS_Document objFMS = new BLL_FMS_Document();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    int fileID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        fileID = Convert.ToInt32(Request.QueryString["FileID"].ToString());
        DataSet dsLastestFileAccessType = objFMS.getLatestFileOperationByUserID(fileID, Convert.ToInt32(Session["USERID"]));
        if (dsLastestFileAccessType.Tables[0].Rows.Count > 0)
        {
            getFolderName(dsLastestFileAccessType.Tables[0].Rows[0]["FilePath"].ToString(), dsLastestFileAccessType.Tables[0].Rows[0]["Orinal_FileName"].ToString(), dsLastestFileAccessType.Tables[0].Rows[0]["Form_Type"].ToString(), dsLastestFileAccessType.Tables[0].Rows[0]["Department"].ToString(), dsLastestFileAccessType.Tables[0].Rows[0]["Format"].ToString());
            if (dsLastestFileAccessType.Tables[0].Rows[0]["Operation_Type"].ToString() == "CHECKED IN")
            {
                pnlForFileUpload.Enabled = false;
                String msg = String.Format("ShowMessage('You have already Checked In the Form,Please Check Out the Form and then Check In')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

            }
            else if (dsLastestFileAccessType.Tables[0].Rows[0]["Operation_Type"].ToString() == "CREATED")
            {
                pnlForFileUpload.Enabled = false;
                String msg = String.Format("ShowMessage('Form is not Check Out,Please Check Out the Form and then Check In')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

            }

            else
                CheckInFileOpertion(fileID);

        }
        else
            CheckInFileOpertion(fileID);

    }

    /// <summary>
    /// get the folder name as a string by passing the file path.
    /// </summary>
    /// <param name="FilePathText"></param>
    public void getFolderName(string FilePathText, string original_fileName,string FormType,string Department,string Format)
    {
        string[] FilePath = FilePathText.Split('/');
        //this is showing the last parent folder name of the document
        txtFolderName.Text = FilePath[FilePath.Length - 2].ToString();
        //this is showing the document name
        txtFileName.Text = original_fileName;// FilePath[FilePath.Length - 1].ToString();

        txtFormType.Text = FormType;
        txtDepartment.Text = Department;
        txtFormat.Text = Format;
    }

    /// <summary>
    /// this is a method to do file operation by passing the file ID.
    /// </summary>
    /// <param name="fileID"></param>
    public void CheckInFileOpertion(int fileID)
    {
        DataSet dsOperationInfo = objFMS.getCheckedFileInfo(fileID);
        if (dsOperationInfo.Tables[0].Rows.Count > 0)
        {
            //get the last row of the table to know the current status of the file
            int dataRow = dsOperationInfo.Tables[0].Rows.Count - 1;
            string FileStatus = dsOperationInfo.Tables[0].Rows[dataRow]["Operation_Type"].ToString();
            int UserID = Convert.ToInt32(dsOperationInfo.Tables[0].Rows[dataRow]["UserID"].ToString());

            if (UserID != Convert.ToInt32(Session["USERID"]))
            {
                pnlForFileUpload.Enabled = false;
                string Message = "Form is already Check Out by " + dsOperationInfo.Tables[0].Rows[dataRow]["UserName"].ToString() + " " + ",\n currently this form is not availabe for Check In";
                String msg = String.Format("ShowMessage('" + Message + "')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            }

            if ((UserID == Convert.ToInt32(Session["USERID"])) && (FileStatus.Equals("CHECKED IN") == true))
            {
                MessageForCheckInFirst();
            }

        }
        else
            MessageForCheckInFirst();
    }


    /// <summary>
    /// this is use for the display the custom message.
    /// </summary>
    public void MessageForCheckInFirst()
    {
        pnlForFileUpload.Enabled = false;
        string Message = "Please Check Out form first for edit.";
        String msg = String.Format("ShowMessage('" + Message + "')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        DataTable dt = new DataTable();
        dt = objUploadFilesize.Get_Module_FileUpload("FMS_");
        if (dt.Rows.Count > 0)
        {
            string datasize = dt.Rows[0]["Size_KB"].ToString();
            if (CheckInfileUp.HasFile)
            {
                if (CheckInfileUp.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                {
                    string FileName = System.IO.Path.GetFileName(CheckInfileUp.PostedFile.FileName);

                    if (FileName.ToUpper().Equals(txtFileName.Text.ToUpper()) == true)
                    {
                        //get latest File details by the file ID
                        DataSet dsFileDetails = objFMS.getFileDetailsByID(fileID, 0);

                        if (dsFileDetails.Tables[0].Rows.Count > 0)
                        {
                            //string[] filePath = dsFileDetails.Tables[0].Rows[0]["FilePath"].ToString().Split('/');
                            string PhPath = "../"+dsFileDetails.Tables[0].Rows[0]["FilePath"].ToString().Replace("/", "\\");

                            string oldfileName = System.IO.Path.GetFileName(dsFileDetails.Tables[0].Rows[0]["FilePath"].ToString());


                            string DestinationPath = Server.MapPath(PhPath);
                            string sFileExt = System.IO.Path.GetExtension(FileName);
                            string sGuidFileName = "FMSL_" + System.Guid.NewGuid() + sFileExt;
                            DestinationPath = DestinationPath.Replace(oldfileName, sGuidFileName);

                            CheckInfileUp.PostedFile.SaveAs(DestinationPath);
                            objFMS.insertRecordAtCheckIN(fileID, sGuidFileName, Convert.ToInt32(Session["USERID"]));

                            //string[] createFolderPathText = DestinationPath.Split('\\');
                            //string TargetFolder = DestinationPath.Replace(createFolderPathText[createFolderPathText.Length - 1].ToString(), "") + FileInfoAfterUpdationDB.Tables[0].Rows[0]["ID"].ToString();
                            //System.IO.Directory.CreateDirectory(DestinationPath);                                        


                            txtRemarks.Text = "";
                            String msg = String.Format("ShowMessage('Form is checked-in successfully,\\nPlease browse the file from tree to see latest.');window.parent.location.href='FMSFileloader.aspx?docid=" + fileID + "';");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                        }
                    }
                    else
                    {
                        String msg2 = String.Format("ShowMessage('Sorry you have browsed diffrent form,\\n please select form as mentioned in Document Name.')");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg2", msg2, true);
                    }
                }
                else
                {
                    lblMessage.Text = datasize + " KB File size exceeds maximum limit";
                }
            }
            else
            {
                String msg = String.Format("ShowMessage('Please browse required document to upload into server')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            }
        }
        else
        {
            string js2 = "alert('Upload size not set!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
        }

    }
}
