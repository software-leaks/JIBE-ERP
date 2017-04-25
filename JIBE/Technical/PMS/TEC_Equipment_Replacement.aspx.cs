using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PMS;
using System.Data;

public partial class Technical_PMS_TEC_Equipment_Replacement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BLL_PMS_Library_Jobs objPms = new BLL_PMS_Library_Jobs();
            DataSet dsLoc = objPms.Get_Equipment_Location(UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"]), Convert.ToInt32(Request.QueryString["SystemID"]), UDFLib.ConvertIntegerToNull(Request.QueryString["SubSystemID"]), Convert.ToInt32(Request.QueryString["SystemLocation"]), UDFLib.ConvertIntegerToNull(Request.QueryString["SubSystemLocation"]));

            DViewActive.DataSource = dsLoc.Tables["ACTIVELOCATION"];
            DViewActive.DataBind();

            gvSpare.DataSource = dsLoc.Tables["SPARELOCATION"];
            gvSpare.DataBind();


        }
    }
    protected void btnReplaceEquipment_Click(object sender, ImageClickEventArgs e)
    {
        BLL_PMS_Library_Jobs objPms = new BLL_PMS_Library_Jobs();
        objPms.Upd_Equipment_Replacement(UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"]), Convert.ToInt32(Request.QueryString["SystemLocation"]), Convert.ToInt32(hdfSpareEQPID.Value), Convert.ToInt32(DViewActive.DataKey["ID"].ToString()), txtRemark.Text, Convert.ToInt32(Session["userid"]));

        string script = string.Format("parent.DDlVessel_selectionChange();parent.hideModal('dvEQPReplacement');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "close", script, true);

    }
}