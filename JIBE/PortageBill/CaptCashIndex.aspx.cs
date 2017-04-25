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
using System.Collections.Specialized;
using System.Web.Configuration;
using SMS.Business.PortageBill;

public partial class Account_Portage_Bill_CaptCashIndex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            CaptCashBind();
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");
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

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public void CaptCashBind()
    {
        string arg = Request.QueryString["arg"].ToString();
        string[] arr;

        if (arg != null)
        {
            arr = arg.Split('~');

            string Vessel = arr[0].ToString();
            string Month = DateTime.Parse(arr[1]).Month.ToString();
            string Year = DateTime.Parse(arr[1]).Year.ToString();

            DataSet CaptCashDs = BLL_PB_PortageBill.Get_CapCashReport(Month, Year, Vessel);
            CaptCashGV.DataSource = CaptCashDs.Tables[0];
            CaptCashGV.DataBind();
        }
    }

    protected void CaptCashGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        CaptCashGV.PageIndex = e.NewPageIndex;
        CaptCashBind();
    }
    protected void CaptCashGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataSet RptDs = BLL_PB_PortageBill.LatestReportID();

        for (int i = 0; i < CaptCashGV.Rows.Count; i++)
        {
            string Id = ((Label)(CaptCashGV.Rows[i].Cells[CaptCashGV.Columns.Count - 2].FindControl("lblrptID"))).Text;
            string Vessel = ((Label)(CaptCashGV.Rows[i].Cells[CaptCashGV.Columns.Count - 1].FindControl("lblVesselCode"))).Text;

            for (int j = 0; j < RptDs.Tables[0].Rows.Count; j++)
            {
                if ((RptDs.Tables[0].Rows[j][0].ToString() == Id) && (RptDs.Tables[0].Rows[j][1].ToString() == Vessel))
                {
                    ImageButton imgbtn = (ImageButton)(CaptCashGV.Rows[i].Cells[CaptCashGV.Columns.Count - 3].FindControl("btnUnlcok"));
                    imgbtn.Enabled = true;
                }
                else
                {
                    //ImageButton imgbtn = (ImageButton)(CaptCashGV.Rows[i].Cells[CaptCashGV.Columns.Count - 3].FindControl("btnUnlcok"));
                    //imgbtn.ImageUrl = "~/images/lock.png";
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //ImageButton imgbtn = (ImageButton)(e.Row.Cells[CaptCashGV.Columns.Count - 3].FindControl("btnUnlcok"));

            //string Id = ((Label)(e.Row.Cells[CaptCashGV.Columns.Count - 2].FindControl("lblrptID"))).Text;
            //string Vessel = ((Label)(e.Row.Cells[CaptCashGV.Columns.Count - 1].FindControl("lblVesselCode"))).Text;

            //NameValueCollection queryStringsView = new NameValueCollection();
            //Session["Name"] = e.Row.Cells[0].Text.ToString();

            //queryStringsView.Add("ViewId", Id);
            //queryStringsView.Add("ViewId", Vessel);

            //string encryptedStringview = CryptoQueryStringHandler.EncryptQueryStrings(queryStringsView,
            //                            WebConfigurationManager.AppSettings["CryptoKey"]);

            //string popupScriptview = "ViewPopupId('" + queryStringsView + "')";

            //for (int k = 0; k < e.Row.Cells.Count - 1; k++)
            //{
            //    if (k != CaptCashGV.Columns.Count - 3)
            //    {

            //        e.Row.Attributes.Add("onMouseOver", "this.style.cursor='hand';");
            //        e.Row.Attributes.Add("onclick", popupScriptview);
            //    }
            //}
        }

    }
    protected void unlockreport(object sender, CommandEventArgs e)
    {
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        //BAL.UnlockCaptCash();
    }
   


    protected void CaptCashGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDetails")
        {
            string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
            ResponseHelper.Redirect("CaptRpt.aspx?Vessel_ID=" + Args[0] + "&ID=" + Args[1], "_blank", "");
        }
    }
}
