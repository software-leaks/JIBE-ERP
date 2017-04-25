using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.DMS;
using System.IO;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;

public partial class Crew_Crew_UploadDoc : System.Web.UI.Page
{
    BLL_DMS_Document objDMSBLL = new BLL_DMS_Document();
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
    protected void Page_Load(object sender, EventArgs e)
    {

        CalendarExtender1.Format = Convert.ToString(Session["User_DateFormat"]);
        CalendarExtender2.Format = Convert.ToString(Session["User_DateFormat"]);


        if (!IsPostBack)
        {
            Load_DocumentTypes();
        }
    }

    private void Load_DocumentTypes()
    {
        if (DropDownList1.Items.Count == 0)
        {
            DataTable dt = objDMSBLL.GetDocumentTypeList();
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("-Select-", "0"));

            DropDownList2.DataSource = dt;
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, new ListItem("-Select-", "0"));

            DropDownList3.DataSource = dt;
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, new ListItem("-Select-", "0"));

            DropDownList4.DataSource = dt;
            DropDownList4.DataBind();
            DropDownList4.Items.Insert(0, new ListItem("-Select-", "0"));

            DropDownList5.DataSource = dt;
            DropDownList5.DataBind();
            DropDownList5.Items.Insert(0, new ListItem("-Select-", "0"));
        }

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        lblMessage.Text="";
        lblmsg1.Text = "";
        lblmsg2.Text = "";
        lblmsg3.Text = "";
        lblmsg4.Text = "";
        lblmsg5.Text = "";
        int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
        int DocTypeID = 0;
        int DocID = 0;
       
        string DocName = "";
        string FileName = "";
        string FileExt = "";
        DataTable dt = new DataTable();
        dt = objUploadFilesize.Get_Module_FileUpload("CWF_");
        if (dt.Rows.Count > 0)
        {
            string datasize = dt.Rows[0]["Size_KB"].ToString();

            if (FileUpload1.HasFile)
            {
                if (FileUpload1.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                {
                    Guid GUID = Guid.NewGuid();
                    FileName = FileUpload1.FileName;
                    FileExt = Path.GetExtension(FileName).ToLower();
                    DocName = FileName.ToLower().Replace(FileExt, "");
                    FileName = GUID.ToString() + FileExt;
                    DocTypeID = UDFLib.ConvertToInteger(DropDownList1.SelectedValue);
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Uploads/CrewDocuments/" + FileName));

                    DocID += objCrew.INS_CrewDocuments(CrewID, DocName, FileName, FileExt, DocTypeID, GetSessionUserID(), txtDocNo1.Text, txtIssueDate1.Text, txtIssuePlace1.Text, txtExpDate1.Text);

                }
                else
                {
                    lblmsg1.Text = datasize + " KB File size exceeds maximum limit";
                   
                }
            }
                if (FileUpload2.HasFile)
                {
                    if (FileUpload2.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                    {
                        Guid GUID = Guid.NewGuid();
                        FileName = FileUpload2.FileName;
                        FileExt = Path.GetExtension(FileName).ToLower();
                        DocName = FileName.ToLower().Replace(FileExt, "");
                        FileName = GUID.ToString() + FileExt;
                        DocTypeID = UDFLib.ConvertToInteger(DropDownList2.SelectedValue);
                        FileUpload2.PostedFile.SaveAs(Server.MapPath("~/Uploads/CrewDocuments/" + FileName));

                        DocID += objCrew.INS_CrewDocuments(CrewID, DocName, FileName, FileExt, DocTypeID, GetSessionUserID(), txtDocNo1.Text, txtIssueDate1.Text, txtIssuePlace1.Text, txtExpDate1.Text);
                    }
                    else
                    {
                        lblmsg2.Text = datasize + " KB File size exceeds maximum limit";
                        
                    }
                }
                    if (FileUpload3.HasFile)
                    {
                        if (FileUpload3.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                        {
                            Guid GUID = Guid.NewGuid();
                            FileName = FileUpload3.FileName;
                            FileExt = Path.GetExtension(FileName).ToLower();
                            DocName = FileName.ToLower().Replace(FileExt, "");
                            FileName = GUID.ToString() + FileExt;
                            DocTypeID = UDFLib.ConvertToInteger(DropDownList3.SelectedValue);
                            FileUpload3.PostedFile.SaveAs(Server.MapPath("~/Uploads/CrewDocuments/" + FileName));

                            DocID += objCrew.INS_CrewDocuments(CrewID, DocName, FileName, FileExt, DocTypeID, GetSessionUserID(), txtDocNo1.Text, txtIssueDate1.Text, txtIssuePlace1.Text, txtExpDate1.Text);
                        }
                        else
                        {
                            lblmsg3.Text = datasize + " KB File size exceeds maximum limit";
                            
                        }
                    }
                        if (FileUpload4.HasFile)
                        {
                            if (FileUpload4.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                            {
                                Guid GUID = Guid.NewGuid();
                                FileName = FileUpload4.FileName;
                                FileExt = Path.GetExtension(FileName).ToLower();
                                DocName = FileName.ToLower().Replace(FileExt, "");
                                FileName = GUID.ToString() + FileExt;
                                DocTypeID = UDFLib.ConvertToInteger(DropDownList4.SelectedValue);
                                FileUpload4.PostedFile.SaveAs(Server.MapPath("~/Uploads/CrewDocuments/" + FileName));

                                DocID += objCrew.INS_CrewDocuments(CrewID, DocName, FileName, FileExt, DocTypeID, GetSessionUserID(), txtDocNo1.Text, txtIssueDate1.Text, txtIssuePlace1.Text, txtExpDate1.Text);
                            }
                            else
                            {
                                lblmsg4.Text = datasize + " KB File size exceeds maximum limit";
                               
                            }
                        }
                        if (FileUpload5.HasFile)
                        {
                            if (FileUpload4.PostedFile.ContentLength < Int32.Parse(dt.Rows[0]["Size_KB"].ToString()) * 1024)
                            {
                                Guid GUID = Guid.NewGuid();
                                FileName = FileUpload5.FileName;
                                FileExt = Path.GetExtension(FileName).ToLower();
                                DocName = FileName.ToLower().Replace(FileExt, "");
                                FileName = GUID.ToString() + FileExt;
                                DocTypeID = UDFLib.ConvertToInteger(DropDownList5.SelectedValue);
                                FileUpload5.PostedFile.SaveAs(Server.MapPath("~/Uploads/CrewDocuments/" + FileName));

                                DocID += objCrew.INS_CrewDocuments(CrewID, DocName, FileName, FileExt, DocTypeID, GetSessionUserID(), txtDocNo1.Text, txtIssueDate1.Text, txtIssuePlace1.Text, txtExpDate1.Text);
                            }
                            else
                            {
                                lblmsg5.Text = datasize + " KB File size exceeds maximum limit";
                                return;
                            }
                        }
                        if (DocID > 0)
                        {
                            lblMessage.Text = "Document(s) Uploaded.";
                            string js = "window.parent.ReloadDocuments();";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "reloadDoc", js, true);
                        }
        }


        else
        {
            string js2 = "alert('Upload size not set!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
        }
    }
}