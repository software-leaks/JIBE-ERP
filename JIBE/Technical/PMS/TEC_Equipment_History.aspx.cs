using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;

public partial class Technical_PMS_TEC_Equipment_History_ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucAsyncPager2.BindMethodName = "BindNextPageItems";
           // UserAccessValidation();
            BLL_Infra_VesselLib objBLLVessel = new BLL_Infra_VesselLib();
            DataTable dtvsl = objBLLVessel.Get_VesselList(0, 0, UDFLib.ConvertToInteger(Session["USERCOMPANYID"]), "", UDFLib.ConvertToInteger(Session["USERCOMPANYID"]));
            DDlVessel_List.DataSource = dtvsl;
            DDlVessel_List.DataBind();
            DDlVessel_List.Items.Insert(0, new ListItem("--SELECT VESSEL--", "0"));
        }
    }

    protected void UserAccessValidation()
    {
       
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(UDFLib.ConvertToInteger(Session["userid"]), PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {

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
}