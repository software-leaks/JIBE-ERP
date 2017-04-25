using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.VM;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class VesselMovement_PortCallAlert : System.Web.UI.Page
{
    public UserAccess objUA = new UserAccess();

    BLL_VM_PortCall objPortCall = new BLL_VM_PortCall();

    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (!IsPostBack)
        {

            BindPortCallAlert(Convert.ToInt32(GetSessionUserID()));
        }
    }
    protected void BindPortCallAlert(int UserId)
    {

        DataTable dt = objPortCall.Get_PortCallAlertList(UserId ,"0");
        if (dt.Rows.Count > 0)
        {
            gvPortAlert.DataSource = dt;
            gvPortAlert.DataBind();
            gvPortAlert.Visible = true;
        }
        else
        {
            gvPortAlert.DataSource = dt;
            gvPortAlert.DataBind();
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void  gvPortAlert_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblRead = (Label)e.Row.FindControl("lblRead");
            Button btnMarkRead = (Button)e.Row.FindControl("btnMarkRead");
            if (DataBinder.Eval(e.Row.DataItem, "Read_Id").ToString() == "0")
            {
                btnMarkRead.Visible = true;
                lblRead.Visible = false;
            }
            else
            {
                btnMarkRead.Visible = false;
                lblRead.Text = "Read On : " + DataBinder.Eval(e.Row.DataItem, "Created_Date").ToString();
                lblRead.Visible = true;
            }
        }
    }

    protected void btnMarkRead_click(object source, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int Port_call_ID = UDFLib.ConvertToInteger(arg[0]);
        int Notification_ID = UDFLib.ConvertToInteger(arg[1]);
        int retval = objPortCall.Update_Port_Call_Alert(Notification_ID, Port_call_ID, GetSessionUserID());

        BindPortCallAlert(GetSessionUserID());
    }
}