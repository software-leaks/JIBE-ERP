using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class WebUserControlLogin : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string strConn = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;

        try
        {
            if (!IsPostBack)
            {

                if (Session["USERID"] != null)
                {
                    SqlConnection sCon = new SqlConnection(strConn);
                    SqlDataAdapter sqlAdp = new SqlDataAdapter("SP_INF_MNU_Get_MenuLib", sCon);
                    sqlAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlAdp.SelectCommand.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int));
                    sqlAdp.SelectCommand.Parameters["@userid"].Value = Convert.ToInt32(Session["USERID"]);
                    DataSet ds = new DataSet();
                    sqlAdp.Fill(ds, "Menu");

                    string strMenu = GenerateUL(ds);
                    jQueryMenu.Text = strMenu;
                }

            }

        }
        catch (Exception ex)
        {
            string js = "alert('" + ex.Message + "')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
        }

    }

    private string GenerateUL(DataSet ds)
    {
        StringBuilder strMenu = new StringBuilder("<ul id='nav' class='menu'>");
        DataRow[] drs = ds.Tables["Menu"].Select("Menu_Type is null");

        foreach (DataRow dr in drs)
        {
            strMenu.Append("<li>");
            if (!string.IsNullOrWhiteSpace(Convert.ToString(dr["Menu_Link"])))
                strMenu.Append("<a href='" + HttpContext.Current.Request.ApplicationPath + "/" + Convert.ToString(dr["Menu_Link"]) + "?DepID=" + (Convert.ToString(dr["DepID"]) == "" ? "0" : Convert.ToString(dr["DepID"])) + "' class='parent'><span>" + dr["Menu_Short_Discription"].ToString().Trim() + "</span></a>");
            else
                strMenu.Append("<a href='javascript:void(0)' class='parent'><span>" + dr["Menu_Short_Discription"].ToString().Trim() + "</span></a>");

            DataRow[] drInners = ds.Tables["Menu"].Select("Menu_Type =" + dr["Menu_Code"].ToString());
            if (drInners.Length != 0)
            {
                strMenu.Append("<ul>");
                foreach (DataRow drInner in drInners)
                {
                    string filter = "Menu_Type = '" + drInner["Menu_Code"].ToString() + "'";

                    ds.Tables["Menu"].AcceptChanges();
                    DataRow[] drInnerLinks = ds.Tables["Menu"].Select(filter);

                    if (drInnerLinks.Length != 0)
                    {
                        //strMenu.Append("<li><a href='" + HttpContext.Current.Request.ApplicationPath + "/" + drInner["Menu_Link"].ToString() + "'><span>" + drInner["Menu_Short_Discription"].ToString().Trim() + "</span></a>");
                        ////if (drInner["Menu_Link"].ToString() != "")
                        ////{
                        ////    strMenu.Append("<li><a href='" + HttpContext.Current.Request.ApplicationPath + "/" + drInner["Menu_Link"].ToString() + "'><span>" + drInner["Menu_Short_Discription"].ToString().Trim() + "</span></a>");
                        ////}
                        ////else
                        ////{
                        strMenu.Append("<li><a href='javascript:void(0)' class='parent'><span>" + drInner["Menu_Short_Discription"].ToString().Trim() + "</span></a>");
                        ////}

                        strMenu.Append("<ul>");
                        foreach (DataRow drInnerLink in drInnerLinks)
                        {
                            strMenu.Append("<li><a href='" + HttpContext.Current.Request.ApplicationPath + "/" + drInnerLink["Menu_Link"].ToString()  + "'><span>");
                            strMenu.Append(drInnerLink["Menu_Short_Discription"].ToString());
                            strMenu.Append("</span></a></li>");
                        }
                        strMenu.Append("</ul>");

                        strMenu.Append("</li>");
                    }
                    else
                    {
                        if (drInner["Menu_Link"].ToString() != "")
                        {
                            strMenu.Append("<li><a href='" + HttpContext.Current.Request.ApplicationPath + "/" + drInner["Menu_Link"].ToString()  + "'><span>" + drInner["Menu_Short_Discription"].ToString().Trim() + "</span></a></li>");
                        }
                        else
                        {
                            strMenu.Append("<li><a href='javascript:void(0)'><span>" + drInner["Menu_Short_Discription"].ToString().Trim() + "</span></a></li>");
                        }
                    }
                }
                strMenu.Append("</ul>");
            }
            strMenu.Append("</li>");
        }

        strMenu.Append("</ul>");
        return strMenu.ToString();
    }
}
