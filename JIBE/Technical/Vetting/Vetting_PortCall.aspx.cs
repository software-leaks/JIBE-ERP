using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Properties;

public partial class Technical_Vetting_Vetting_PortCall : System.Web.UI.Page
{
 PortCalls objPortCall;

    protected void Page_Load(object sender, EventArgs e)
    {
      try
      {
        if (!IsPostBack)
        {

            if (Request.QueryString["VesselID"] != null && Request.QueryString["VesselID"] != "")
            {
                ViewState["VesselID"] =  UDFLib.ConvertToInteger(Request.QueryString["VesselID"].ToString());               
            }
            else
            {
                ViewState["VesselID"] = 0;
            }
            ucPortCall.BindPortCalls(Convert.ToInt32(ViewState["VesselID"].ToString()));

            if (Request.QueryString["TypeID"] != null && Request.QueryString["TypeID"] != "")
            {
                ViewState["TypeID"] = UDFLib.ConvertToInteger(Request.QueryString["TypeID"].ToString());
            }
            else
            {
                ViewState["TypeID"] = 0;
            }
            if (Request.QueryString["Date"] != null && Request.QueryString["Date"] != "")
            {
                
                ViewState["Date"] = Request.QueryString["Date"].ToString();
            }
            else
            {
                ViewState["Date"] = "";
            }
            if (Request.QueryString["Questionnaire"] != null && Request.QueryString["Questionnaire"] != "")
            {
                ViewState["Questionnaire"] = UDFLib.ConvertToInteger(Request.QueryString["Questionnaire"].ToString());
            }
            else
            {
                ViewState["Questionnaire"] = 0;
            }
            if (Request.QueryString["OilMajor"] != null && Request.QueryString["OilMajor"] != "")
            {
                ViewState["OilMajor"] = UDFLib.ConvertToInteger(Request.QueryString["OilMajor"].ToString());
            }
            else
            {
                ViewState["OilMajor"] = 0;
            }
            if (Request.QueryString["Inspector"] != null && Request.QueryString["Inspector"] != "")
            {
                ViewState["Inspector"] = UDFLib.ConvertToInteger(Request.QueryString["Inspector"].ToString());
            }
            else
            {
                ViewState["Inspector"] = 0;
            }
            if (Request.QueryString["NoDays"] != null && Request.QueryString["NoDays"] != "")
            {
                ViewState["NoDays"] = UDFLib.ConvertToInteger(Request.QueryString["NoDays"].ToString());
            }
            else
            {
                ViewState["NoDays"] = 0;
            }
            if (Request.QueryString["RespNextDue"] != null && Request.QueryString["RespNextDue"] != "")
            {
              
                ViewState["RespNextDue"] = Request.QueryString["RespNextDue"].ToString();
            }
            else
            {
                ViewState["RespNextDue"] = "";
            }

        }
      }
      catch (Exception ex)
      {
          UDFLib.WriteExceptionLog(ex);
      }
                     
    }

     public void UserControl_ucPortCalls_Select(PortCalls objPortCall)     
     {
         try
         {

         string js = "parent.HidePortCall('" + ViewState["VesselID"].ToString() + "','" + ViewState["TypeID"].ToString() + "','" + ViewState["Date"].ToString() + "','" + ViewState["Questionnaire"].ToString() + "','" + ViewState["OilMajor"].ToString() + "','" + ViewState["Inspector"].ToString() + "','" + objPortCall.Port_ID + "','" + objPortCall.Port_Call_ID + "','" + ViewState["NoDays"].ToString() + "','" + ViewState["RespNextDue"].ToString()+ "');";
          ScriptManager.RegisterStartupScript(this, this.GetType(), "js1", js, true);
         }
         catch (Exception ex)
         {
             UDFLib.WriteExceptionLog(ex);
         }

     }
   
}