using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_uc_Report_Issue : System.Web.UI.UserControl
{

    public string ModuleID
    {
        get { return hdf_Module_Id.Value; }
        set { hdf_Module_Id.Value = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
     
    protected void ImgReportBug_Click(object sender, EventArgs e)
    {

        //Acess from Local
        //String url = String.Format("OpenPopupWindowBtnID('POP__Task', '', 'http://localhost:63661/SPM/Task/ReportsBugToSPM.aspx?Module_ID=" + hdf_Module_Id.Value + "&USERCOMPANYID=" + Session["USERCOMPANYID"] + "&USERID=" + Session["USERID"] + "','popup',360,650,null,null,false,false,true,false,'');");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", url, true);

        //Acess from Server01
        String url = String.Format("OpenPopupWindowBtnID('POP__Task', '', 'http://seachange.dyndns.info/SPM/Task/ReportsBugToSPM.aspx?Module_ID=" + hdf_Module_Id.Value + "&USERCOMPANYID=" + Session["USERCOMPANYID"] + "&USERID=" + Session["USERID"] + "','popup',360,650,null,null,false,false,true,false,'');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", url, true);

    }
}