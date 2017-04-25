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
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using SMS.Business.QMS;
using SMS.Business.Infrastructure;
using SMS.Properties;


public partial class Main : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document();
    string FILE_SERVER_NAME = "";
    ArrayList ar = new ArrayList();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            UserAccessValidation();
    }
   
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    
    //protected void BrowseTreeView_SelectedNodeChanged(object sender, EventArgs e)
    //{
    //    TreeNode selectedNode = BrowseTreeView.SelectedNode;

    //    if (selectedNode.ChildNodes.Count > 0)
    //    {
    //        //node type is a folder
    //        // lblDocName.Text = "";

    //    }
    //    else
    //    {
    //        string fileName = selectedNode.Text;
    //        // lblDocName.Text = fileName;
    //        selectedNode.NavigateUrl = selectedNode.ValuePath;
    //        selectedNode.Target = "docPreview";

    //        string js = "<script language='javascript' type='text/javascript'>DocOpenOnIFrame('" + Convert.ToString(selectedNode.ValuePath) + "');</script>";
    //        Page.ClientScript.RegisterStartupScript(GetType(), "script", js);

    //    }
    //}

    protected void btnCreateFolder_Click(object sender, EventArgs e)
    {
        //  DivRemarks.Visible = true;
    }

    protected void btndivCancel_Click(object sender, EventArgs e)
    {
        // DivRemarks.Visible = false;
    }

    protected void btndivSave_Click(object sender, EventArgs e)
    {
        bool folderExists = Directory.Exists("");
        if (!folderExists)
        {
            System.IO.Directory.CreateDirectory("");
        }


        string js2 = "<script language='javascript' type='text/javascript'>myMessage('Folder created successfully.')</script>";
        Page.ClientScript.RegisterStartupScript(GetType(), "script", js2);
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        
        UserAccess objUA = new UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);
        Session["Pageurl"] = PageURL;
        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
           
        }
        if (objUA.Edit == 0)
        {

        }
        if (objUA.Delete == 0)
        {
        }

        if (objUA.Approve == 0)
        {

        }

    }

}