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
using SMS.Business.QMS;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class FileLoader : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    BLL_QMS_Document objQMS = new BLL_QMS_Document();
    string LatestFilePath = "";
    string OppStatus = "";
    string OppUserID = "";
    string DocVersion = "0";
    string DocID = "";
    public Boolean uaEditFlag = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {      

        if (Convert.ToString(Session["USERID"]) == "")
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
                DocVersion = temp[1];
            }
        }
        else
        {
            DocID = Request.QueryString["DocID"];
        }



        DataSet dsFileDetails = objQMS.getFileDetailsByID(UDFLib.ConvertToInteger(DocID), UDFLib.ConvertToInteger(DocVersion));

        if (dsFileDetails.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsFileDetails.Tables[0].Rows[0];
           hfDocName.Value= lblDocName.Text = dr["LogFileID"].ToString();
            
            if (DocVersion == "0")
            {
                DocVersion = dr["Version"].ToString();
                if (dr["Created_By"].ToString() == "0")
                    lblOppStatus.Text = dr["Operation_Type"].ToString() == "" ? "CREATED by Office on " + ConvertDateToString(dr["Date_Of_Creatation"].ToString(), "dd-MMM-yy HH:mm") : dr["Operation_Type"].ToString() + " by " + dr["first_name"].ToString().ToUpper() + " on " + ConvertDateToString(dr["Operation_Date"].ToString(), "dd-MMM-yy HH:mm");
                else
                    lblOppStatus.Text = dr["Operation_Type"].ToString() == "" ? "CREATED by " + dr["CreatedBYFirstName"].ToString() + " on " + ConvertDateToString(dr["Date_Of_Creatation"].ToString(), "dd-MMM-yy HH:mm") : dr["Operation_Type"].ToString() + " by " + dr["first_name"].ToString().ToUpper() + " on " + ConvertDateToString(dr["Operation_Date"].ToString(), "dd-MMM-yy HH:mm");
            }
            else
            {
                if (DocVersion == dr["Version"].ToString())
                    lblOppStatus.Text = dr["Operation_Type"].ToString() == "" ? "CREATED by " + dr["first_name"].ToString() + " on " + ConvertDateToString(dr["Date_Of_Creatation"].ToString(), "dd-MMM-yy HH:mm") : dr["Operation_Type"].ToString() + " by " + dr["first_name"].ToString().ToUpper() + " on " + ConvertDateToString(dr["Operation_Date"].ToString(), "dd-MMM-yy HH:mm");
                else
                    lblOppStatus.Text = "<b>You are viewing older version of the file.</b>";

            }
            lblCurrentVersion.Text = DocVersion;

            OppStatus = dr["Operation_Type"].ToString();
            OppUserID = dr["userid"].ToString();
            string VersionFilePath = "";


             //for file Approval History
            int rowCount = 0;
            DataTable dtApproval_Level = objQMS.QMS_Check_FileApprovalExists(UDFLib.ConvertToInteger(DocID), null, null, null, null, null, null, ref rowCount);
            if (dtApproval_Level.Rows.Count > 0 && dtApproval_Level.Rows[dtApproval_Level.Rows.Count - 1]["Approval_Status"].ToString() == "0")
            {
                int index = 0;
                foreach (DataRow row in dtApproval_Level.Rows)
                {
                    if (row["Approval_Status"].ToString() == "0")
                    {
                        index = Convert.ToInt32(row["LevelID"].ToString());

                        break;

                    }

                }
                int User_ID=0;

                if (index > 0)
                {
                    User_ID = Convert.ToInt32(dtApproval_Level.Rows[index - 1]["ApproverID"].ToString());
                }
                else if(index==0)
               {
                     User_ID = Convert.ToInt32(dtApproval_Level.Rows[index ]["ApproverID"].ToString());
               }
                BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
                
                DataTable dtUser = objUser.Get_UserDetails(User_ID);
                lblUser.Text = dtUser.Rows[0]["User_name"].ToString();
                dvMain.Visible = false;
                divApprovalMessage.Visible = true;
                lblDocName1.Text = lblDocName.Text;
                lblCurrentVersion1.Text = lblCurrentVersion.Text;
                lblOppStatus1.Text = lblOppStatus.Text;
               
            }
            else
            {

                LatestFilePath = dr["FilePath"].ToString();
                dvMain.Visible = true;
                divApprovalMessage.Visible = false;
            }

        }
        UserAccessValidation();

        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
    protected bool UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, Session["Pageurl"].ToString());

        if (objUA.Edit == 1)
            uaEditFlag = true;
        if (objUA.Admin == 1)
        {
            btnDelete.Visible = true;

        }
        return uaEditFlag;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Convert.ToString(Session["USERID"]));
        else
            return 0;
    }
    private string getVirtualPath(string docPath)
    {
        string[] arPath = docPath.Split('\\');
        string url = "";

        for (int i = 3; i < arPath.Length; i++)
        {
            if (url != "")
                url += "/";

            url += arPath[i];
        }
        return url;
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
    protected void btnDeleteE_Click(object sender, EventArgs e)
    {
        objQMS.Delete_DMSFile_Folder(UDFLib.ConvertToInteger(Request.QueryString["DocID"]), GetSessionUserID());

        string jsFile = "alert('File deleted Successfully.');parent.ChildCallBackDelete();";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", jsFile, true);
    }
    protected void ImgViewIsRead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            DataSet ds = objQMS.getDocumentReadListbyDocument_ID(UDFLib.ConvertToInteger(Request.QueryString["DocID"]));
            lblRedDocName.Text = hfDocName.Value;
            if (ds.Tables[0].Rows.Count > 0)
            {                
                gvViewIsRead.DataSource = ds.Tables[0];
                gvViewIsRead.DataBind();
            }
            else
            {
                gvViewIsRead.DataSource = ds.Tables[0];
                gvViewIsRead.DataBind();
            }
            //OperationMode = "Add Salary Structure";
            string VeiwIsRead = String.Format("showModal('divViewIsRead',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "VeiwIsRead", VeiwIsRead, true);
        }
        catch (Exception)
        {
            
            throw;
        }
    }
}
