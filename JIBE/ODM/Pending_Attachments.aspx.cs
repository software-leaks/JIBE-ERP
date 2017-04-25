using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.ODM;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class ODM_Pending_Attachments : System.Web.UI.Page
{
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    public UserAccess objUA = new UserAccess();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_ODM objODM = new BLL_ODM();
    protected void Page_Load(object sender, EventArgs e)
    {
       // UserAccessValidation();
        if (!IsPostBack)
        {


            BindAttachment();

        }
    }



    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        // if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            // btnsave.Visible = false;
            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void BindAttachment()
    {


        DataTable dt = objODM.GET_ODM_Attachments_PendingList();

        gvAttachment.DataSource = dt;
        gvAttachment.DataBind();
    }

    protected void btnODMMain_Click(object sender, EventArgs e)
    {
        Response.Redirect("../ODM/Daily_Message_Queue.aspx");
    }


}
   
