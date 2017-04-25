using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.CP;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class CP_Delivery_Bunker : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public int Delivery_Bunker_ID = 0;
    public int CPID = 0;
    public int PortId = 0;
    public string  PortName = "";
    public string OType = "";
    public Boolean uaEditFlag = true;//Test default true
    public Boolean uaDeleteFlage = true;
    BLL_CP_CharterParty objCP = new BLL_CP_CharterParty();
    protected void Page_Load(object sender, EventArgs e)
    {
       // UserAccessValidation();
        if (!IsPostBack)
        {
            if (Request.QueryString["CPID"] != null  && Request.QueryString["PortId"] != null && Request.QueryString["Type"] != null)
            {
                CPID = Convert.ToInt32(Request.QueryString["CPID"]);
                ViewState["CPID"] = CPID.ToString();
                ViewState["PortId"] = Convert.ToInt32(Request.QueryString["PortId"]);
                if (Request.QueryString["PortName"] != null)
                    PortName = Request.QueryString["PortName"];
                OType = Request.QueryString["Type"];
                ViewState["OType"] = OType;
                if (OType == "R")
                    ltPageHeader.Text = "Port : [" + PortName + "]  ReDelivery Bunker ";
                else
                    ltPageHeader.Text = "Port : [" + PortName + "]  Delivery Bunker ";
            }
            BindFuelType();
            BindBunkerDetail();

        }
    }

    protected void BindFuelType()
    {
        DataTable dt = objCP.Get_FuelTypes();
        ddlFuelType.DataSource = dt;
        ddlFuelType.DataTextField = "Fuel_Name";
        ddlFuelType.DataValueField = "FuelType_ID";
        ddlFuelType.DataBind();
        dt.Dispose();
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnSave.Enabled = false;
            btnSaveClose.Enabled = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            // btnsave.Visible = false;
            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void BindBunkerDetail()
    {

        DataTable dt = objCP.Get_BunkerList(UDFLib.ConvertIntegerToNull(ViewState["CPID"]), "D");

        gvDeliveryBunker.DataSource = dt;
        gvDeliveryBunker.DataBind();
    }

    protected void ClearData()
    {
        txtPricePerUnit.Text = "";
        txtUnit.Text = "";
        ltmessage.Text = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
        BindBunkerDetail();
    }
    protected void btnSaveClose_Click(object sender, EventArgs e)
    {
        SaveData();
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close();", true);
    }

    protected void SaveData()
    {
        ltmessage.Text = "";
        int res = -1;
        if (ViewState["Bunker_Id"] != null)

          res = objCP.UPD_BunkerDetail(UDFLib.ConvertIntegerToNull(ViewState["Bunker_Id"]), UDFLib.ConvertIntegerToNull(Session["CPID"]),  UDFLib.ConvertIntegerToNull(ddlFuelType.SelectedValue),
              ddlFuelType.SelectedItem.Text,ViewState["OType"].ToString(), UDFLib.ConvertToDouble(txtUnit.Text), UDFLib.ConvertToDouble(txtPricePerUnit.Text), GetSessionUserID());
        else

            res = objCP.INS_BunkerDetail(UDFLib.ConvertIntegerToNull(Session["CPID"]), UDFLib.ConvertIntegerToNull(ddlFuelType.SelectedValue),ddlFuelType.SelectedItem.Text, ViewState["OType"].ToString(),
                UDFLib.ConvertToDouble(txtUnit.Text), UDFLib.ConvertToDouble(txtPricePerUnit.Text), GetSessionUserID());

        if (res == 0)
            ltmessage.Text = "Record already exist for same type ! Please check.";
        else
            ClearData();
    }

    protected void onUpdate(object source, CommandEventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            Delivery_Bunker_ID = Convert.ToInt32(e.CommandArgument);
            ViewState["Bunker_Id"] = Delivery_Bunker_ID;
            dt = objCP.Get_BunkerDetail(Delivery_Bunker_ID);

            txtPricePerUnit.Text = dt.DefaultView[0]["Unit_Price"].ToString();
            txtUnit.Text = dt.DefaultView[0]["Fuel_Amt"].ToString();
            ddlFuelType.SelectedValue = dt.DefaultView[0]["Fuel_Type_Id"].ToString();

        }
        catch { }

    }
    protected void lbtnDelete_Click(object source, CommandEventArgs e)
    {
        DataTable dt = new DataTable();
        ClearData();
        try
        {
            Delivery_Bunker_ID = Convert.ToInt32(e.CommandArgument);
            objCP.Delete_BunkerDetail(Delivery_Bunker_ID, GetSessionUserID());
            BindBunkerDetail();
        }
        catch { }

    }
    


}
   
