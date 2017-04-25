using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Technical;
using SMS.Business.Operations;
using SMS.Business.Infrastructure;
using SMS.Properties;
public partial class DeckLogBookThreshold : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    public UserAccess objUA = new UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();

        if (!IsPostBack)
        {
            Load_VesselList();

            if (Request.QueryString["LogBookID"] != null)
            {
                lblLOGBOOKID.Value = Request.QueryString["LogBookID"].ToString();
            }
            else
            {
                lblLOGBOOKID.Value = null;
            }
            if (Request.QueryString["Vessel_ID"] != null)
            {
                ViewState["VESSELID"] = Request.QueryString["Vessel_ID"].ToString();
            }
            else
            {
                ViewState["VESSELID"] = null;
            }

            if (ViewState["VESSELID"] != null)
            {
                BindDeckLogBookThrasholdList();
            }



        }

    }


    public void BindDeckLogBookThrasholdList(int? Threshold_Id = null)
    {
        DataSet ds = BLL_OPS_DeckLog.Get_DeckLogBook_Thrashold_List(int.Parse(ViewState["VESSELID"].ToString()), UDFLib.ConvertIntegerToNull(lblLOGBOOKID.Value), Threshold_Id);






        ds.Tables[0].Rows[0]["Vessel_Id"] = UDFLib.ConvertIntegerToNull(ViewState["VESSELID"]);

        if (Threshold_Id == null)
        {
            DDLVersion.DataSource = ds.Tables[1];
            DDLVersion.DataTextField = "Version";
            DDLVersion.DataValueField = "ID";
            DDLVersion.DataBind();

            if (ds.Tables[1].Rows.Count == 0)
            {
                DDLVersion.Enabled = false;
                DDLVersion.Items.Insert(0, new ListItem("-No Records found-", "0"));
            }
            else
            {
                DDLVersion.Enabled = true;
            }
        }
        if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
        DDLVersion.SelectedValue = ds.Tables[0].Rows[0]["ID"].ToString();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlVesselMain.SelectedValue = ds.Tables[0].Rows[0]["Vessel_Id"].ToString();
            FormView1.DataSource = ds.Tables[0];
            FormView1.DataBind();

            if (ds.Tables[0].Rows[0]["Active_Status"].ToString() == "0")
            {
                //btnAddTo.Enabled = false;
                btnCopy.Enabled = false;
                //  btnOK.Enabled = false;
                btnSave.Enabled = false;
            }
            else
            {
                //btnAddTo.Enabled = false;
                btnCopy.Enabled = true;
                //  btnOK.Enabled = false;
                btnSave.Enabled = true;
            }

        }
    }




    protected void btnSave_Click(object sender, EventArgs e)
    {

        int retVal = BLL_OPS_DeckLog.Insert_DeckLogBook_Thrashold(UDFLib.ConvertIntegerToNull(((Label)FormView1.Row.Cells[0].FindControl("lblthresHoldID")).Text), int.Parse(ViewState["VESSELID"].ToString())
            , UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtAirTempMin")).Text), UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtAirTempMax")).Text)
            , UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtBarometerMin")).Text), UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtBarometerMax")).Text)
            , UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtErrorGyroMin")).Text), UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtErrorGyroMax")).Text)
            , UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtErrorStandardMin")).Text), UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtErrorStandardMax")).Text)
            , UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtSeaMin")).Text), UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtSeaMax")).Text)
            , UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtSeaTempMin")).Text), UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtSeaTempMax")).Text)
            , UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtVisibilityMin")).Text), UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtVisibilityMax")).Text)
            , null, null
            , UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtWindsForceMin")).Text), UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtWindsForceMax")).Text),
              UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtCapacity100MinHold")).Text), UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtCapacity100MaxHold")).Text),
              UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtCapacity100MinTank")).Text), UDFLib.ConvertDecimalToNull(((TextBox)FormView1.Row.Cells[0].FindControl("txtCapacity100MaxTank")).Text),
        int.Parse(Session["UserID"].ToString()));



        string js = "alertmessage(1);";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
        lblLOGBOOKID.Value = null;
        BindDeckLogBookThrasholdList(null);

    }
    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);
        if (objUA.Edit == 0)
        {

            FormView1.Enabled = false;
            btnSave.Visible = false;
        }

        if (objUA.View == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {
            FormView1.Enabled = false;
            btnSave.Visible = false;

        }
    }
    public void Load_VesselList()
    {

        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()); 
        if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
            Vessel_Manager = UserCompanyID;

        ddlVessel.DataSource = objVessel.Get_VesselList(0, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));


        ddlVesselMain.DataSource = objVessel.Get_VesselList(0, 0, Vessel_Manager, "", UserCompanyID);

        ddlVesselMain.DataTextField = "VESSEL_NAME";
        ddlVesselMain.DataValueField = "VESSEL_ID";
        ddlVesselMain.DataBind();
        ddlVesselMain.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));


        ddlVessel.SelectedIndex = 0;
        ddlVesselMain.SelectedIndex = 0;
    }


    protected void ddlVesselMain_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVesselMain.SelectedIndex == 0)
            return;

        ViewState["VESSELID"] = ddlVesselMain.SelectedValue;
        lblLOGBOOKID.Value = null;
        BindDeckLogBookThrasholdList();
    }

    protected void DDLVersion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLVersion.SelectedIndex > 0)
        {
            btnSave.Enabled = false;
            btnCopy.Enabled = false;
        }
        else
        {
            btnSave.Enabled = true;
            btnCopy.Enabled = true;
        }
        BindDeckLogBookThrasholdList(UDFLib.ConvertIntegerToNull(DDLVersion.SelectedValue));
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        int copyfromvessel = int.Parse(ddlVessel.SelectedValue);
        string js = "";
        int i = BLL_OPS_DeckLog.CopyDeckLogBookThreshold(int.Parse(ViewState["VESSELID"].ToString()), copyfromvessel, int.Parse(Session["USERID"].ToString()));

        if (i <= 0)
        {
            js = "alertmessage(0);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
            return;
        }

        js = "alert('Threshold Values are copied successfully from " + ddlVessel.SelectedItem.Text + " to  " + ddlVesselMain.SelectedItem.Text + "!');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
        lblLOGBOOKID.Value = null;
        BindDeckLogBookThrasholdList(null);
    }
}