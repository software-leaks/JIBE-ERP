using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Properties;
public partial class Technical_PMS_PMSEquipmentStructure : System.Web.UI.Page
{
    public UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ucAsyncPager1.BindMethodName = "BindNextPageJobs";

            ucAsyncPager2.BindMethodName = "BindNextPageItems";
            //  ucAsyncLocPager.BindMethodName = "onGetLocation";
            // UserAccessValidation();
            BLL_Infra_VesselLib objBLLVessel = new BLL_Infra_VesselLib();
            DataTable dtvsl = objBLLVessel.Get_VesselList(0, 0, UDFLib.ConvertToInteger(Session["USERCOMPANYID"]), "", UDFLib.ConvertToInteger(Session["USERCOMPANYID"]));
            DDlVessel_List.DataSource = dtvsl;
            DDlVessel_List.DataBind();
            DDlVessel_List.Items.Insert(0, new ListItem("--SELECT VESSEL--", "0"));
            hdnUserID.Value = Session["userid"].ToString();
            
        }
    }
    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUA.View == 0)
        {
           // Response.Redirect("~/crew/default.aspx?msgid=1");
            lblError.Text = "You don't have Sufficient privilege To View This Page";
        }

        if (objUA.Add == 0)
        {

        }
    }
}