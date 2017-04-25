using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.ASL;
using System.IO;
using System.Text;
using SMS.Properties;

public partial class ASL_ASL_Supplier_Statistics : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public Boolean uaEditFlag = true;
    public Boolean uaDeleteFlage = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserAccessValidation();
        if (!IsPostBack)
        {
            //Load_VesselList();
            BindGrid();
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

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            pnl.Visible = false;
            lblMsg.Text = "You don't have sufficient previlege to access the requested information.";
        }
        else
        {
            pnl.Visible = true;
        }

        if (objUA.Add == 0)
        {
            //ImgAdd.Visible = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        //else
        //    btnsave.Visible = false;

        if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private string GetSessionSupplierName()
    {
        try
        {
            if (Session["Register_Name"] != null)
            {
                return Session["Register_Name"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    protected void BindGrid()
    {
        lblSuppliername.Text = GetSessionSupplierName();
        lblSupplierCode.Text = GetSuppID();
        SearchBindGrid();

    }
    public string GetSuppID()
    {
        try
        {
            if (Request.QueryString["Supp_ID"] != null)
            {
                return Request.QueryString["Supp_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    protected void SearchBindGrid()
    {
        try
        {
            int Lastyear = 0;
            int year = 0;
            string SupplierID = GetSuppID();
            DataSet ds = BLL_ASL_Supplier.Get_Supplier_Statistics(UDFLib.ConvertStringToNull(SupplierID));
            if (ds.Tables[1].Rows.Count > 0)
            {
                //gvStatistics.DataSource = ds.Tables[0];
                //gvStatistics.DataBind();
                year = Convert.ToInt32(DateTime.Now.Year.ToString());
                Lastyear = Convert.ToInt16(ds.Tables[2].Rows[0]["Lastyear"].ToString());

                StringBuilder sb = new StringBuilder();
                sb.Append("<table Style=\"border: 1px solid #cccccc; font-family: Tahoma; font-size: 12px; color: Black;\" Cellpadding=5 Cellspacing=1 \">");
                sb.Append("<tr class='HeaderStyle-css' Style=\"Font-Size:14px;font-weight:bold;\" Bgcolor=\"E8E8E8\">" + "<td Style=\"Text-Align:center;\" Colspan=11>Statistics</td>");
                for (int J = Lastyear; J <= year; J++)
                {
                    sb.Append("<td Style=\"Text-Align:Center;\"  Colspan=11>" + J + "</td>");
                }
                sb.Append("<td Colspan=11>Total Value</td>");
                sb.Append("</tr>");

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    sb.Append("<tr class='AlternatingRowStyle-css' Style=\"Font-Size:11px;\">");
                    for (int j = 1; j < ds.Tables[1].Columns.Count; j++)
                    {
                        sb.Append("<td Style=\"Text-Align:Right;\" Colspan=11>" + ds.Tables[1].Rows[i][j].ToString() + "</td>");
                    }

                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                ltSupplierStatistics.Text = sb.ToString();

            }
        }
        catch { }
        {
        }

    }

    
}