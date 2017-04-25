using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.DMS;
using SMS.Business.Infrastructure;
using System.IO;
using SMS.Business.TMSA;
using SMS.Business.QMS;
using SMS.Properties;


public partial class TMSA_Report_OverallReport : System.Web.UI.Page
{

    BLL_TMSA_REPORTS objTMSA = new BLL_TMSA_REPORTS();

    protected void Page_Load(object sender, EventArgs e)
    {

     

        try
        {

            if (!IsPostBack)
            {
                UserAccessValidation();

                
                

                if (Session["USERID"] != null)
                {
                    TreeView1.Nodes.Clear();

                    DataSet ds = objTMSA.FillTree(Convert.ToInt32(Session["USERID"].ToString()));
                    ds.Tables[0].TableName = "Menu";
                    GenerateUL(ds);
                    


                }
                BindGrid();

            }


        }
        catch (Exception ex)
        {
            string js = "alert('" + ex.Message + "')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
        }

    }


    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void UserAccessValidation()
    {
        try
        {
            int CurrentUserID = GetSessionUserID();
            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
            UserAccess objUA = new UserAccess();
            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);
            Session["Pageurl"] = PageURL;
            if (objUA.View == 0)
                Response.Redirect("~/default.aspx?msgid=1");

            //if (objUA.Admin == 0)
            //{
            //    hdnRole.Value = "View";
            //    btnCopy.Visible = false;
            //}
            //else
            //{
            //    hdnRole.Value = "Admin";
            //    btnCopy.Visible = true;
            //}
            if (objUA.View == 1)
            {
                hdnRole.Value = "View";
                btnCopy.Visible = false;

            }
            if ((objUA.Admin == 1) || (objUA.Edit == 1))
            {
                hdnRole.Value = "Admin";
            }

            if (objUA.Edit == 1)
            {
                btnCopy.Visible = false;
            }
            if (objUA.Admin == 1)
            {
                btnCopy.Visible = true;
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    /// <summary>
    /// Method Genrates User menu Tree
    /// </summary>
    /// <param name="ds"></param>
    private void GenerateUL(DataSet ds)
    {
        try
        {
            DataRow[] drs = ds.Tables["Menu"].Select("Menu_Type is null");
            int meni = 0;
            int child = 0;

            foreach (DataRow dr in drs)
            {
                TreeNode mi = new TreeNode(dr["Menu_Short_Discription"].ToString().Trim(), dr["menu_type"].ToString() + "_" + dr["Menu_Code"].ToString());
                TreeView1.Nodes.Add(mi);

                DataRow[] drInners = ds.Tables["Menu"].Select("Menu_Type ='" + dr["Menu_Code"].ToString() + "' ");
                if (drInners.Length != 0)
                {
                    child = 0;
                    foreach (DataRow drInner in drInners)
                    {
                        TreeNode miner;
                        miner = new TreeNode(drInner["Menu_Short_Discription"].ToString().Trim(), drInner["menu_type"].ToString() + "_" + drInner["Menu_Code"].ToString() );
                       
                        TreeView1.Nodes[meni].ChildNodes.Add(miner);
                        TreeView1.Nodes[meni].CollapseAll();
                        

                        string filter = "Menu_Type = '" + drInner["Menu_Code"].ToString() + "'";

                        ds.Tables["Menu"].AcceptChanges();
                        DataRow[] drInnerLinks = ds.Tables["Menu"].Select(filter);

                        if (drInnerLinks.Length != 0)
                        {
                            foreach (DataRow drInnerLink in drInnerLinks)
                            {
                                TreeNode milink = new TreeNode(drInnerLink["Menu_Short_Discription"].ToString().Trim(), drInnerLink["menu_type"].ToString() + "_" + drInnerLink["Menu_Code"].ToString());
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
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }




    }




    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {

        
        //write a method to get a link of a node

        TreeNode tn = ((TreeView)sender).SelectedNode;
       
        
        string menucode = tn.Value.Split('_')[1].ToString();
        string menuType = tn.Value.Split('_')[0].ToString();

        //Fix for when --Select Folder and Click on Save button ,Not showing popup message and Module add button and existing module links disappears
         if ((tn.ChildNodes.Count == 0) && (menuType != ""))
         {

            hdnModuleID.Value = menucode;
            
        }
        else
        {

            hdnModuleID.Value = "";
        }

       


    }


    protected void imgsearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void BindGrid()
    {
        try
        {
            string category = "";

            int rowcount = 1;
            DataTable dt = BLL_TMSA_PI.Get_KPI_List("", 1, 100, ref  rowcount, category).Tables[0];



            gvKPIList.DataSource = dt;
            gvKPIList.DataBind();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Method is used to Export report data to excel file
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportToExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
            string Elements = hdnElements.Value;
            string Stages = hdnStages.Value;
            string Levels = hdnLevels.Value;
            string version = hdnVersion.Value;
            int iVersionNo = Convert.ToInt16(version);
            DataTable dt = objTMSA.Search_ExportToExcelOverallReport(Elements, Stages, Levels, iVersionNo).Tables[0];

            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=DataTable.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);

            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Method is used to create new version of report
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCopy_Click(object sender, EventArgs e)
    {
        try
        {
            objTMSA.CopyNewVersion();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void save_Click(object sender, EventArgs e)
    {
        if (Session["USERID"] != null)
        {
            TreeView1.Nodes.Clear();

            DataSet ds = objTMSA.FillTree(Convert.ToInt32(Session["USERID"].ToString()));
            ds.Tables[0].TableName = "Menu";
            GenerateUL(ds);


        }
    }
    protected void saveKPI_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

}