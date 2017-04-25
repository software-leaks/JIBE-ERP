using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class Crew_CrewDetails_EditEOC : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    public string DFormat = "";
    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.Page.Header.Controls.Add(SetUserStyle.AddThemeInHeader());
            base.OnInit(e);
        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        DFormat = UDFLib.GetDateFormat();
        CalendarExtender14.Format = DFormat;

        if (!IsPostBack)
        {
            UserAccessValidation();
            int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
            int VoyID = UDFLib.ConvertToInteger(Request.QueryString["VoyID"]);

            if (objUA.View == 1)
                Load_EOC(CrewID, VoyID);

        }
    }


    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";

        if (objUA.Add == 0)
        {
            btnSaveEOCEdit.Enabled = false;
            btnSaveAndCloseEOCEdit.Enabled = false;
        }
        if (objUA.Edit == 0)
        {
            btnSaveEOCEdit.Enabled = false;
            btnSaveAndCloseEOCEdit.Enabled = false;
        }
        if (objUA.Delete == 0)
        {
        }
        if (objUA.Approve == 0)
        {
        }
        //-- MANNING OFFICE LOGIN --

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void Load_EOC(int CrewID, int VoyID)
    {
        DataTable dt = objBLLCrew.Get_CrewVoyages(CrewID, VoyID);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["COCDate"].ToString() != "")
            {
                //txtNewCOCDate.Text = DateTime.Parse(dt.Rows[0]["COCDate"].ToString()).ToString("dd/MM/yyyy");
                txtNewCOCDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["COCDate"]));
            }
            txtCOCRemark.Text = dt.Rows[0]["COCRemark"].ToString();
        }
    }
    protected void btnSaveEOCEdit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtNewCOCDate.Text != "")
            {
                if (!UDFLib.DateCheck(txtNewCOCDate.Text))
                {
                    string js = "alert('Enter valid EOC Date" + UDFLib.DateFormatMessage() + "');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                    return;
                }
            }

            int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
            int VoyID = UDFLib.ConvertToInteger(Request.QueryString["VoyID"]);

            if (VoyID != 0)
            {
                int RetVal = objBLLCrew.UPDATE_COC_Date(VoyID, UDFLib.ConvertToDefaultDt(Convert.ToString(txtNewCOCDate.Text)), txtCOCRemark.Text, GetSessionUserID());
                if (RetVal == 1)
                {
                    lblMsg.Text = "EOC Date Modified";
                    string js = "parent.GetCrewVoyages(" + CrewID.ToString() + ");";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnSaveAndCloseEOCEdit_Click(object sender, EventArgs e)
    {
        btnSaveEOCEdit_Click(null, null);
        int CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
        string js = "parent.hideModal('dvPopupFrame');parent.GetCrewVoyages(" + CrewID.ToString() + ");";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
    }
}