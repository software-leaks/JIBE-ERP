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

public partial class VesselMovement_PortCallCrewList : System.Web.UI.Page
{
    public UserAccess objUA = new UserAccess();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_VM_PortCall objPortCall = new BLL_VM_PortCall();
    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
    public string DateFormat = "";
    bool bClosewindow = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        //This change has been done to change the date format as per user selection
        calAsOfDate.Format = Convert.ToString(Session["User_DateFormat"]);
        DateFormat = UDFLib.GetDateFormat();//Get User date format
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Vessel_ID"].ToString()))
            {
                Load_VesselList();
                GetVesselID();
                ddlCrewVessel.SelectedValue = Request.QueryString["Vessel_ID"].ToString();
                //This change has been done to change the date format as per user selection
                if (Session["Arrival"] == null)
                {
                    txtAsofDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(System.DateTime.Now));

                    
                }
                else
                {
                    txtAsofDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(Session["Arrival"]));
                    
                }
                ddlCrewStatus.SelectedValue = "2";
                BindcrewList();
            }
        }
        string msg1 = String.Format("StaffInfo();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }

    public void Load_VesselList()
    {
        DataTable dt = objBLL.Get_VesselList(0, 0, 0, "", 1);

        ddlCrewVessel.DataSource = dt;
        ddlCrewVessel.DataTextField = "VESSEL_NAME";
        ddlCrewVessel.DataValueField = "VESSEL_ID";
        ddlCrewVessel.DataBind();
        ddlCrewVessel.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public int GetVesselID()
    {
        try
        {

            if (Request.QueryString["Vessel_ID"] != null)
            {
                return int.Parse(Request.QueryString["Vessel_ID"].ToString());
            }

            else
                return 0;
        }
        catch { return 0; }
    }
    protected void BindcrewList()
    {

        DataTable dt = objPortCall.Get_PortCall_CrewList(Convert.ToInt32(ddlCrewVessel.SelectedValue), Convert.ToDateTime(txtAsofDate.Text), UDFLib.ConvertIntegerToNull(ddlCrewStatus.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            gvCrewList.DataSource = dt;
            gvCrewList.DataBind();
            gvCrewList.Visible = true;
        }
        else
        {
            gvCrewList.DataSource = dt;
            gvCrewList.DataBind();
            //gvCrewList.Visible = true;
        }
    }
    protected void imgCrewFilter_Click(object sender, ImageClickEventArgs e)
    {
        BindcrewList();
    }
    //protected void btnExit_Click(object sender, EventArgs e)
    //{
    //    ViewState["closewindow"] = true;
    //}
}