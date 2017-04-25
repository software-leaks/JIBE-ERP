    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SMS.Business.Infrastructure;
using System.Drawing;
using SMS.Business.LMS;

public partial class Infrastructure_Menu_MenuHelpSetting : System.Web.UI.Page
{ 
    BLL_Infra_MenuManagement objMenuBLL = new BLL_Infra_MenuManagement();
    BLL_Infra_UserCredentials objBllUser = new BLL_Infra_UserCredentials();

    protected void Page_Load(object sender, EventArgs e)
    {
        string strConn = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        try

        {
            if (!IsPostBack)
            {

               // UserAccessValidation();
                LoadInitialResource();
                btnAddHelpFAQ.Enabled = false;
                btnAddHelpVideo.Enabled = false;
                

            }

        }
        catch (Exception ex)
        {
            string js = "alert('" + ex.Message + "')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
        }

    }
    protected void LoadInitialResource()
    {
        DataSet dsMen = BLL_LMS_Help.Get_Enabled_Menu();
        dsMen.Tables[0].TableName = "Menu";
       GenerateUL(dsMen);
       DataSet dsRes = BLL_LMS_Help.Get_Help_Resources(null);
       ddlHelpVideo.DataSource = dsRes.Tables[0];
       ddlHelpVideo.DataTextField = "ITEM_NAME";
       ddlHelpVideo.DataValueField = "ID";
       ddlHelpVideo.DataBind();
       ddlHelpVideo.Items.Insert(0, new ListItem("- Select -", "0"));

       //ddlHelpFAQ.DataSource = dsRes.Tables[1];
       //ddlHelpFAQ.DataTextField = "Question";
       //ddlHelpFAQ.DataValueField = "FAQ_ID";
       //ddlHelpFAQ.DataBind();
       //ddlHelpFAQ.Items.Insert(0, new ListItem("- Select -", "0"));
    }

    
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.IsAdmin == 0)
        {
            if (objUA.View == 0)
                Response.Redirect("~/default.aspx?msgid=1");

            if (objUA.Add == 0)
            {
                Response.Redirect("~/default.aspx?msgid=2");
            }
            if (objUA.Edit == 0)
            {
                Response.Redirect("~/default.aspx?msgid=3");
            }
            if (objUA.Delete == 0)
            {
                Response.Redirect("~/default.aspx?msgid=4");
            }
            if (objUA.Approve == 0)
            {
                Response.Redirect("~/default.aspx?msgid=5");
            }
        }
      
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    private void GenerateUL(DataSet ds)
    {

        DataRow[] drs = ds.Tables["Menu"].Select("Menu_Type is null");
        int meni = 0;
        int child = 0;

        foreach (DataRow dr in drs)
        {
            TreeNode mi = new TreeNode(dr["Menu_Short_Discription"].ToString().Trim(), dr["menu_type"].ToString() + ";" + dr["Menu_Code"].ToString() + ";" + dr["Menu_Link"].ToString());
            TreeView1.Nodes.Add(mi);

            DataRow[] drInners = ds.Tables["Menu"].Select("Menu_Type ='" + dr["Menu_Code"].ToString() + "' ");
            if (drInners.Length != 0)
            {
                child = 0;
                foreach (DataRow drInner in drInners)
                {
                    TreeNode miner;
                    miner = new TreeNode(drInner["Menu_Short_Discription"].ToString().Trim(), drInner["menu_type"].ToString() + ";" + drInner["Menu_Code"].ToString() + ";" + drInner["Menu_Link"].ToString());

                    TreeView1.Nodes[meni].ChildNodes.Add(miner);
                    TreeView1.Nodes[meni].CollapseAll();

                    string filter = "Menu_Type = '" + drInner["Menu_Code"].ToString() + "'";

                    ds.Tables["Menu"].AcceptChanges();
                    DataRow[] drInnerLinks = ds.Tables["Menu"].Select(filter);

                    if (drInnerLinks.Length != 0)
                    {
                        foreach (DataRow drInnerLink in drInnerLinks)
                        {
                            TreeNode milink = new TreeNode(drInnerLink["Menu_Short_Discription"].ToString().Trim(), drInnerLink["menu_type"].ToString() + ";" + drInnerLink["Menu_Code"].ToString() + ";" + drInnerLink["Menu_Link"].ToString());
                            TreeView1.Nodes[meni].ChildNodes[child].ChildNodes.Add(milink);
                            TreeView1.Nodes[meni].ChildNodes[child].CollapseAll();
                        }
                    }
                    child++;
                }
            }
            meni++;
        }




    }

     

   

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
       
        Load_Associated_Resource();
     
    }

     

 

    protected void imgbtnDeleteHelpVideo_Click(object sender, CommandEventArgs e)
    {
        BLL_LMS_Help.Delete_Help_Resource(UDFLib.ConvertIntegerToNull(e.CommandArgument), GetSessionUserID());
        Load_Associated_Resource();
    }
    protected void imgbtnDeleteHelpFAQ_Click(object sender, CommandEventArgs e)
    {
        BLL_LMS_Help.Delete_Help_Resource(UDFLib.ConvertIntegerToNull(e.CommandArgument), GetSessionUserID());
        Load_Associated_Resource();
    }
    protected void btnAddHelpFAQ_Click(object sender, EventArgs e)
    {
        if (ddlHelpFAQ.SelectedIndex <= 0)
        {
            string msg = String.Format("alert('FAQ is mandatory.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
            return;
        }
        BLL_LMS_Help.InsertUpdate_Help_Resource(UDFLib.ConvertToInteger(ViewState["Menu_Code"]), UDFLib.ConvertToInteger(ddlHelpFAQ.SelectedValue), "FAQ", GetSessionUserID());
        Load_Associated_Resource();
        ddlHelpFAQ.SelectedIndex = 0;
    }
     

    

    protected void Load_Associated_Resource()
    {

        if (TreeView1.SelectedNode != null)
        {

            
         

            string[] NodeValue = TreeView1.SelectedNode.Value.Split(';');


            int Menu_Code = UDFLib.ConvertToInteger(NodeValue[1]);
            ViewState["Menu_Code"] = Menu_Code;

            //if (NodeValue[2] == "" || (NodeValue[2] == "Infrastructure/DashBoard_Common.aspx" && TreeView1.SelectedNode.Text != "DashBoard"))
            if (NodeValue[2] == "" || (NodeValue[2] == "Infrastructure/DashBoard.aspx" && TreeView1.SelectedNode.Text != "DashBoard"))
            {
                
                dtlHelpVideo.DataSource = null;
                dtlHelpVideo.DataBind();
                dtlHelpFAQ.DataSource = null;
                dtlHelpFAQ.DataBind();
                btnAddHelpFAQ.Enabled = false;
                btnAddHelpVideo.Enabled = false;
               
            }
            else
            {
                DataSet ds = BLL_LMS_Help.Get_Help_Resources(Menu_Code);
                dtlHelpVideo.DataSource = ds.Tables[0];
                dtlHelpVideo.DataBind();
                //dtlHelpFAQ.DataSource = ds.Tables[1];
                //dtlHelpFAQ.DataBind();
                btnAddHelpFAQ.Enabled = true;
                btnAddHelpVideo.Enabled = true;
            } 

           
        }
           
          

    }

  
   
    
    


    protected void btnAddHelpVideo_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlHelpVideo.SelectedIndex <= 0)
            {
                string msg = String.Format("alert('Video is mandatory.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                return;
            }
            BLL_LMS_Help.InsertUpdate_Help_Resource(UDFLib.ConvertToInteger(ViewState["Menu_Code"]),UDFLib.ConvertToInteger(ddlHelpVideo.SelectedValue),"VIDEO",GetSessionUserID());
            Load_Associated_Resource();
            ddlHelpVideo.SelectedIndex = 0;
        }
        catch (Exception)
        {
            
            throw;
        }    
    }
}