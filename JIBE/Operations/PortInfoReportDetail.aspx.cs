using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Operation;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Operations_PortInfoReportDetail : System.Web.UI.Page
{ 
    int _Vessel_Id = 0;
    int _Office_Id = 0;
    int _PortInfoReportId = 0;
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!IsPostBack)
        {
            if (Request.QueryString["PortInfoReportId"] != null)
            {
                UserAccessValidation();
                _Vessel_Id = int.Parse(Request.QueryString["VesselId"].ToString());
                _PortInfoReportId = int.Parse(Request.QueryString["PortInfoReportId"].ToString());
                _Office_Id = int.Parse(Request.QueryString["OfficeId"].ToString());
                DataSet ds = BLL_OPS_VoyageReports.Get_PortTerminalInfoReport(_Vessel_Id, _PortInfoReportId, _Office_Id); 
                BindReport(ds);
            }
        }
    }
    protected void UserAccessValidation()
    {
        UserAccess objUA = new UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            Response.Write("<center><h2>You do not have sufficient privilege to access to this page.</h2><br><br>Please contact " + Session["Company_Name_GL"] + " </center>");
            Response.End();
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
        {
            Response.Redirect("~/account/Login.aspx");
            return 0;
        }
    }
    public int GetVesselId()
    {
        return _Vessel_Id;
    }
    public int GetOfficeId()
    {
        return _Office_Id;
    }
    public int GetPortInfoReportId()
    {
        return _PortInfoReportId;
    }
    protected void BindReport(DataSet ds)
    {
        if (ds.Tables[0] != null)
        {
            fvPortInfoReport.DataSource = ds.Tables[0];
            fvPortInfoReport.DataBind();
        }  
    }
}