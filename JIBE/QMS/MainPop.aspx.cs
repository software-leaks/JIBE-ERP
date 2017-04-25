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
using System.Data.SqlClient;
using SMS.Business.QMS;
using System.Globalization;

public partial class QMS_Main_MainPop : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document();

    public string filename = "";
    public string filepath = "";
    public DateTime date = new DateTime();
    DataSet dsLogInfo = new DataSet();
    DataSet dsUserLog1 = new DataSet();

    static string sCulture = ConfigurationSettings.AppSettings["CultureToUse"];
    static IFormatProvider iFormatProvider = new System.Globalization.CultureInfo(sCulture, true);
    static string sCultureToCreateTmp = ConfigurationSettings.AppSettings["CultureToCreate"];
    public int UserDropDown;

    protected void Page_Load(object sender, EventArgs e)
    {
        string currentUser = "";
        if (Session["user"] != null)
        {
            currentUser = Session["user"].ToString();
        }
        else
        {
            //IF RUNNING IN LOCAL
            Session["user"] = "100028"; 
            currentUser = Session["user"].ToString();
        }

        if
            (!IsPostBack)
        {

            FillDDUser();
            filepath = Request.QueryString["path"].ToString();
            Session["FilePath"] = filepath;

            filename = getFileNameFromUrl(filepath);
            dsLogInfo = objQMS.getLogInfo(Session["user"].ToString(), filename);
            CultureInfo objCultureInfo = CultureInfo.CreateSpecificCulture(sCulture);

            if (dsLogInfo.Tables[0].Rows.Count > 0)
            {
                string userds = dsLogInfo.Tables[0].Rows[0]["UserID"].ToString();
                string fileds = dsLogInfo.Tables[0].Rows[0]["LogFileID"].ToString();
                if (userds == currentUser && fileds == filename)
                {

                    LBLM2.Text = dsLogInfo.Tables[0].Rows[0]["LOGManuals2"].ToString();
                    LBLM3.Text = dsLogInfo.Tables[0].Rows[0]["LOGManuals3"].ToString();
                    //path = dsLogInfo.Tables[0].Rows[0]["LOGManuals1"].ToString();
                    filename = dsLogInfo.Tables[0].Rows[0]["LogFileID"].ToString();
                    LBLFile.Text = dsLogInfo.Tables[0].Rows[0]["LogFileID"].ToString();
                    date = Convert.ToDateTime(dsLogInfo.Tables[0].Rows[0]["LogDate"].ToString(), iFormatProvider);
                    txtdate.Text = DateTime.Today.ToString("dd-MM-yy");

                    // date = DateTime.Parse(dsLogInfo.Tables[0].Rows[0]["LogDate"].ToString("dd-MM-YY"));

                    //lblMessage.Text = "You have already Read this File before on " + ((DateTime)(dsLogInfo.Tables[0].Rows[0]["LogDate"])).ToString("dd-MM-yyyy") + "";
                    //lblMessage2.Text = "if you want to change dates please press on save button ";
                }
            }
            else
                if (chkDocAllSelected.Checked == true)
                {

                }
                else
                {
                    string iDate = "";
                    string[] ManualText = filepath.Split('/');

                    LBLM2.Text = ManualText[1].ToString();
                    LBLM3.Text = ManualText[2].ToString();
                    LBLFile.Text = filename;
                    txtdate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    iDate = DateTime.Now.ToString();
                    date = Convert.ToDateTime(iDate, iFormatProvider);

                }

        }

    }

    /// <summary>
    /// get file name as string by splitting the file path string.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public string getFileNameFromUrl(string filePath)
    {
        string[] arPath = filePath.Split('/');
        return arPath[arPath.Length - 1];
    }

    public void checkInDB()
    {
    }


    /// <summary>
    /// it saves all the information in the database for further processes.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (DDUser.SelectedIndex == 0)
        {

            string js = "<script language='javascript' type='text/javascript'>CheckUser();</script>";
            Page.ClientScript.RegisterStartupScript(GetType(), "script", js);
        }
        else
            if (chkDocAllSelected.Checked == true)
            {
                string[] filethText = Session["FilePath"].ToString().Split('/');
                DataSet ds = objQMS.getAllFiles(filethText[1].ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    string folderName = filethText[filethText.Length - 2].ToString().ToUpper();
                    string[] filePathFromDB = ds.Tables[0].Rows[i]["FilePath"].ToString().Split('\\');
                    if (folderName.Equals(filePathFromDB[filePathFromDB.Length - 2].ToString().ToUpper()) == true)
                    {
                        objQMS.insertDataQmsLog(Convert.ToInt32(DDUser.SelectedValue), LBLM2.Text, LBLM3.Text, filePathFromDB[filePathFromDB.Length - 1].ToString(), txtdate.Text.ToString().Trim());
                    }

                }
                Response.Write("<script language='javascript'> { window.close();}</script>");
            }
            else
            {
                objQMS.insertDataQmsLog(Convert.ToInt32(DDUser.SelectedValue), LBLM2.Text, LBLM3.Text, LBLFile.Text, txtdate.Text.ToString().Trim());
                Response.Write("<script language='javascript'> { window.close();}</script>");
            }
    }


    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'> { window.close();}</script>");
    }

    protected void DDUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ReadFile = LBLFile.Text.ToString();
        UserDropDown = Convert.ToInt32(DDUser.SelectedValue);
        dsUserLog1 = objQMS.getUserLog(UserDropDown.ToString(), ReadFile);

        if (dsUserLog1.Tables[0].Rows.Count > 0)
        {
            lblMessage.Text = "You have already read this file before on " + ((DateTime)(dsUserLog1.Tables[0].Rows[0]["LogDate"])).ToString("dd-MM-yyyy") + "";
            lblMessage2.Text = "If you want to change dates please press on save button ";
        }
        else
        {
            lblMessage.Text = "";
            lblMessage2.Text = "";
        }

    }

    /// <summary>
    /// bind the combo box with the User name & User ID from the database.
    /// </summary>
    public void FillDDUser()
    {
        try
        {
            DataSet ds = objQMS.FillDDUserForOffice();
            DDUser.DataSource = ds.Tables[0];
            DDUser.DataTextField = "User_name";
            DDUser.DataValueField = "userid";
            DDUser.DataBind();
            DDUser.Items.Insert(0, new ListItem("Select"));
            DDUser.SelectedValue = Session["user"].ToString();
        }
        catch{}
    }


}
