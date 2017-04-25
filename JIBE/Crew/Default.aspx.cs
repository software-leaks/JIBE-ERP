using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;


public partial class Crew_Default : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Redirect("~/Infrastructure/DashBoard_Common.aspx");
       
        Response.Redirect(ConfigurationManager.AppSettings["DeafaultURL"]);
        if (!IsPostBack)
        {
            UserAccessValidation();
            
            int CurrentUserID = GetSessionUserID();
            hdnUserID.Value = CurrentUserID.ToString();
            
            lblULMenu.Text = GenerateUL();

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
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

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

        //-- MANNING OFFICE LOGIN --
       
    }

    //private void GenerateUL()
    //{
    //    string strConn = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
    //    SqlConnection sCon = new SqlConnection(strConn);
    //    SqlDataAdapter sqlAdp = new SqlDataAdapter("SP_INF_MNU_Get_MenuLib", sCon);
    //    sqlAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
    //    sqlAdp.SelectCommand.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int));

    //    sqlAdp.SelectCommand.Parameters["@userid"].Value = UDFLib.ConvertToInteger(Session["USERID"]);
    //    DataSet ds = new DataSet();
    //    sqlAdp.Fill(ds, "Menu");
        
    //    DataRow[] drs = ds.Tables["Menu"].Select("Menu_Type is null");
    //    int meni = 0;
    //    int child = 0;

    //    foreach (DataRow dr in drs)
    //    {
    //        TreeNode mi = new TreeNode(dr["Menu_Short_Discription"].ToString().Trim(), dr["menu_type"].ToString() + "_" + dr["Menu_Code"].ToString());
    //        TreeView1.Nodes.Add(mi);

    //        DataRow[] drInners = ds.Tables["Menu"].Select("Menu_Type ='" + dr["Menu_Code"].ToString() + "' ");
    //        if (drInners.Length != 0)
    //        {
    //            child = 0;
    //            foreach (DataRow drInner in drInners)
    //            {
    //                TreeNode miner;
    //                miner = new TreeNode(drInner["Menu_Short_Discription"].ToString().Trim(), drInner["menu_type"].ToString() + "_" + drInner["Menu_Code"].ToString());
    //                miner.NavigateUrl ="../"+ drInner["Menu_Link"].ToString();
    //                miner.Target = "_blank";

    //                TreeView1.Nodes[meni].ChildNodes.Add(miner);
    //                TreeView1.Nodes[meni].CollapseAll();

    //                string filter = "Menu_Type = '" + drInner["Menu_Code"].ToString() + "'";

    //                ds.Tables["Menu"].AcceptChanges();
    //                DataRow[] drInnerLinks = ds.Tables["Menu"].Select(filter);

    //                if (drInnerLinks.Length != 0)
    //                {
    //                    foreach (DataRow drInnerLink in drInnerLinks)
    //                    {
    //                        TreeNode milink = new TreeNode(drInnerLink["Menu_Short_Discription"].ToString().Trim(), drInnerLink["menu_type"].ToString() + "_" + drInnerLink["Menu_Code"].ToString());                            
    //                        TreeView1.Nodes[meni].ChildNodes[child].ChildNodes.Add(milink);
    //                        TreeView1.Nodes[meni].ChildNodes[child].CollapseAll();

    //                    }
    //                }
    //                child++;
    //            }
    //        }
    //        meni++;
    //    }
    //}

    private string GenerateUL()
    {
        string strConn = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        SqlConnection sCon = new SqlConnection(strConn);
        SqlDataAdapter sqlAdp = new SqlDataAdapter("SP_INF_MNU_Get_MenuLib", sCon);
        sqlAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
        sqlAdp.SelectCommand.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int));

        sqlAdp.SelectCommand.Parameters["@userid"].Value = UDFLib.ConvertToInteger(Session["USERID"]);
        DataSet ds = new DataSet();
        sqlAdp.Fill(ds, "Menu");


        StringBuilder strMenu = new StringBuilder("<ul id='sitemap'>");
        DataRow[] drs = ds.Tables["Menu"].Select("Menu_Type is null");

        foreach (DataRow dr in drs)
        {
            strMenu.Append("<li>");
            //strMenu.Append("<a href='" + HttpContext.Current.Request.ApplicationPath + "/" + dr["Menu_Link"].ToString() + "'>");
            strMenu.Append("<a href='javascript:void(0)'>"); 
            strMenu.Append(dr["Menu_Short_Discription"].ToString().Trim());
            strMenu.Append("</a>");

            DataRow[] drInners = ds.Tables["Menu"].Select("Menu_Type ='" + dr["Menu_Code"].ToString() + "'");
            if (drInners.Length != 0)
            {
                strMenu.Append("<ul>");               

                foreach (DataRow drInner in drInners)
                {
                    if (drInner["Menu_Link"].ToString() == "")
                    {
                        strMenu.Append("<li>");
                        strMenu.Append("<a href='javascript:void(0)'>");
                        //strMenu.Append("<a href='" + HttpContext.Current.Request.ApplicationPath + "/" + drInner["Menu_Link"].ToString() + "'>");
                        strMenu.Append(drInner["Menu_Short_Discription"].ToString());
                        strMenu.Append("</a>");

                        string filter = "Menu_Type = '" + drInner["Menu_Code"].ToString() + "'";

                        ds.Tables["Menu"].AcceptChanges();
                        DataRow[] drInnerLinks = ds.Tables["Menu"].Select(filter);

                        if (drInnerLinks.Length != 0)
                        {
                            strMenu.Append("<ul>");
                            foreach (DataRow drInnerLink in drInnerLinks)
                            {
                                strMenu.Append("<li><a href='" + HttpContext.Current.Request.ApplicationPath + "/" + drInnerLink["Menu_Link"].ToString() + "' target='_blank'>");
                                strMenu.Append(drInnerLink["Menu_Short_Discription"].ToString());
                                strMenu.Append("</a></li>");
                            }
                            strMenu.Append("</ul>");
                        }
                        strMenu.Append("</li>");
                    }
                    else
                    {
                        strMenu.Append("<li><a href='" + HttpContext.Current.Request.ApplicationPath + "/" + drInner["Menu_Link"].ToString() + "' target='_blank'>");
                        strMenu.Append(drInner["Menu_Short_Discription"].ToString());
                        strMenu.Append("</a></li>");
                    }
                }

                //DataRow[] drLinks = ds.Tables["Menu"].Select("Menu_Type ='" + dr["Menu_Code"].ToString() + "' and Menu_Link is not null");
                //foreach (DataRow drLink in drLinks)
                //{
                //    strMenu.Append("<li><a href='" + HttpContext.Current.Request.ApplicationPath + "/" + drLink["Menu_Link"].ToString() + "' target='_blank'>");
                //    strMenu.Append(drLink["Menu_Short_Discription"].ToString());
                //    strMenu.Append("</a></li>");
                //}
                strMenu.Append("</ul>");
            }

            strMenu.Append("</li>");
        }

        strMenu.Append("</ul>");
        return strMenu.ToString();
    }
}