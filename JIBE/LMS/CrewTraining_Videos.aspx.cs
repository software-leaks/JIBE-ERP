using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.LMS;
using System.Data;

public partial class LMS_CrewTraining_Videos : System.Web.UI.Page
{
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {        
        VideosAccessValidation();
    }


    public void VideosAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();   
        try
        {
            if (CurrentUserID != 1)
            {
                string exist = BLL_LMS_Training.Validate_SeaStaff(Convert.ToInt32(Session["USERID"].ToString()));
                if (exist == "0")
                    UserAccessValidation();                   
                    
            }
        }
        catch (Exception)
        {
        }
    }

    protected void UserAccessValidation()
    {
        try
        {
            int CurrentUserID = GetSessionUserID();
            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

            SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

            if (objUA.View == 0)
                Response.Redirect("~/default.aspx?msgid=1");
        }
        catch (Exception)
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
}