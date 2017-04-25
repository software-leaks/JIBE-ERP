using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.QMS;
using System.Globalization;

public partial class QMS_SCM_SCM_Absentees_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL();
            UserAccessValidation();
            BindGrid();
        }

        //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }
    BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);


        //if (objUA.View == 0)
        //    Response.Redirect("~/default.aspx?msgid=1");

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
        if (objUA.Admin == 1)
        {
            ViewState["WLADMIN"] = 1;
        }
        else
        {
            ViewState["WLADMIN"] = 0;
        }
    }
    public void FillDDL()
    {
        try
        {


            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLFleet.Items.Insert(0, li);




            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            DDLVessel.Items.Insert(0, new ListItem("--SELECT--", null));


        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }
    protected void BindGrid()
    {
        DateTime? iniDate = null;
        if(txtToDate.Text!="")
              iniDate = Convert.ToDateTime(txtToDate.Text);
        DateTime? tempdate = null;
         if(txtToDate.Text!="")
             tempdate = UDFLib.ConvertDateToNull(txtToDate.Text).Value.AddMonths(1);
         DataSet ds = BLL_SCM_Report.Get_Absentees_Report(tempdate, UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.Text));
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtToDate.Text = UDFLib.ConvertDateToNull(ds.Tables[0].Rows[0]["Meet_date"]).Value.ToString("dd/MM/yyyy");
            gvAbsentees.DataSource = ds.Tables[0];
            gvAbsentees.DataBind();
        }
        else
        {
            ds.Tables[0].Clear();
            gvAbsentees.DataSource = ds.Tables[0];
            gvAbsentees.DataBind();
        }
          if(iniDate!=null)
            if (Convert.ToDateTime(txtToDate.Text).Month != iniDate.Value.Month)
            {
                txtToDate.Text = iniDate.Value.ToString("dd/MM/yyyy");
                ds.Tables[0].Clear();
                gvAbsentees.DataSource = ds.Tables[0];
                gvAbsentees.DataBind();
            }
        
      



    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {

        Load_VesselList();

    }
    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(DDLFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = 1;

        DDLVessel.DataSource = objVsl.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        DDLVessel.DataTextField = "VESSEL_NAME";
        DDLVessel.DataValueField = "VESSEL_ID";
        DDLVessel.DataBind();
        DDLVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        DDLVessel.SelectedIndex = 0;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtToDate.Text = "";
        DDLVessel.SelectedIndex = 0;
        DDLFleet.SelectedIndex = 0;
        BindGrid();
    }
    protected void gvAbsentees_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (txtToDate.Text != "")
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ((Label)e.Row.Cells[5].FindControl("lblMonth2H")).Text = UDFLib.ConvertDateToNull(txtToDate.Text).Value.ToString("MMM/yyyy", CultureInfo.InvariantCulture);
                ((Label)e.Row.Cells[4].FindControl("lblMonth1H")).Text = (UDFLib.ConvertDateToNull(txtToDate.Text).Value.AddMonths(-1)).ToString("MMM/yyyy", CultureInfo.InvariantCulture);
                ((Label)e.Row.Cells[3].FindControl("lblMonth0H")).Text = (UDFLib.ConvertDateToNull(txtToDate.Text).Value.AddMonths(-2)).ToString("MMM/yyyy", CultureInfo.InvariantCulture);
            }
    }
}