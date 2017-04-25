using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Technical_Inspection_SurveyMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
        {
            divLoggout.Visible = true;
            MainContent.Visible = false;
        }
        else
        {
            MainContent.Visible = true;
            divLoggout.Visible = false;
        }

        if (Session["User_DateFormat"] != null)
        {
            hdnDateFromatMasterPage.Value = UDFLib.GetDateFormat();
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
}
