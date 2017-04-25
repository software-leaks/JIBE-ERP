using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.VM;
using SMS.Business.CP;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class VesselMovement_Port_Call_Report : System.Web.UI.Page
{
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    public string Vessel_ID,Port_CallID;
    public UserAccess objUA = new UserAccess();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_VM_PortCall objPortCall = new BLL_VM_PortCall();
    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
    BLL_CP_CharterParty oBLL_CP = new BLL_CP_CharterParty();
    protected void Page_Load(object sender, EventArgs e)
    {
       // UserAccessValidation();
        if (!IsPostBack)
        {

            if (Request.QueryString["PCID"] != null)
                Session["Port_call_ID"] = Request.QueryString["PCID"];
           
               BindCrewList();
        }
        string msg1 = String.Format("StaffInfo();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Edit == 1)
        {
            uaEditFlag = true;
          
        }
        else
        {
            uaEditFlag = false;
          
        }


        if (objUA.Delete == 1) uaDeleteFlage = true;

    }
 
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    
   
  

    protected void BindCrewList()
    {
        DataSet ds = objPortCall.Get_PortCall_CrewChange(UDFLib.ConvertIntegerToNull(Request.QueryString["PCID"]));

        if (ds.Tables[0].Rows.Count > 0)
        {
            gvCrewOn.DataSource = ds.Tables[0];
            gvCrewOn.DataBind();

        }
        else
        {
            gvCrewOn.DataSource = null;
            gvCrewOn.DataBind();
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            gvCrewOff.DataSource = ds.Tables[1];
            gvCrewOff.DataBind();

        }
        else
        {
            gvCrewOff.DataSource = null;
            gvCrewOff.DataBind();
        }
      

    }

}