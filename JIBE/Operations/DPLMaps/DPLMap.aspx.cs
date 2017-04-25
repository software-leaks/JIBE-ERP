using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Operations_DPLMaps_DPLMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            hdfUserCompanyID.Value = Session["USERCOMPANYID"].ToString();
            hdfUserID.Value = GetSessionUserID().ToString();
        }
          
			
			
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return UDFLib.ConvertToInteger(Session["USERID"]);
        else
        {
            Response.Redirect("~/account/Login.aspx");
            return 0;
        }
    }
}