using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.CP;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class CP_Overdue_BreakDown : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public int Remarks_ID = 0;
    public int CPID = 0;
    public int PortId = 0;
    public string  PortName = "";
    public string OType = "";
    public Boolean uaEditFlag = true;//Test default true
    public Boolean uaDeleteFlage = true;
    protected void Page_Load(object sender, EventArgs e)
    {
       // UserAccessValidation();


            BindRemarks();

    }





    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void BindRemarks()
    {
        BLL_CP_CharterParty oBLL_CP = new BLL_CP_CharterParty();
        if (Request.QueryString["ID"] != null)
        {
            DataTable dt = oBLL_CP.Get_OutstandingRemarks(UDFLib.ConvertIntegerToNull(Request.QueryString["ID"]));

            if (dt.Rows.Count > 0)
                ltBreakdown.Text = dt.Rows[0]["Remarks"].ToString();

        }

    }

   
}
   
