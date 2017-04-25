using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClsBLLTechnical;


public partial class UserControl_ucReqsncancelLog : System.Web.UI.UserControl
{
    /// <summary>
    /// Checking Sessoion Value
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {

        BindGrid();
       
    }
    /// <summary>
    /// Checking Sessoion Value
    /// </summary>
    public void BindGrid()
    {
        try
        {
            TechnicalBAL objLog = new TechnicalBAL();
            if (Session["ReqsnCancelLog"] != null)
            {
                gvCancelLog.DataSource = objLog.Get_ReqsnCancelLog(Session["ReqsnCancelLog"].ToString());
                gvCancelLog.DataBind();
                Session["ReqsnCancelLog"] = "0";
            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

}