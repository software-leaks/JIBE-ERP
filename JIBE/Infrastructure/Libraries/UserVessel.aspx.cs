using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;

public partial class Infrastructure_Libraries_UserVessel : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_FleetList();
            Load_VesselList();
            Load_VesselList_Filter();
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnAssign.Enabled= false;
        }
        if (objUA.Edit == 0)
        {
            
        }
        if (objUA.Delete == 0)
        {
            GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
        }
        if (objUA.Approve == 0)
        {

        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }

    public void Load_VesselList()
    {

        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString()); 

        if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
            Vessel_Manager = UserCompanyID;

        lstVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        lstVessel.DataTextField = "VESSEL_NAME";
        lstVessel.DataValueField = "VESSEL_ID";
        lstVessel.DataBind();
        
    }

    public void Load_VesselList_Filter()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
            Vessel_Manager = UserCompanyID;

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));

    }
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        int UserID = 0;
        int VesselID =0;
        int Res = 0;

        if(lstUserList.SelectedIndex >= 0)
            UserID  = UDFLib.ConvertToInteger(lstUserList.SelectedValue);

        //if(lstVessel.SelectedIndex >= 0)
        //    VesselID  = UDFLib.ConvertToInteger(lstVessel.SelectedValue);

        if (UserID > 0 )
        {
            int[] Sel = lstVessel.GetSelectedIndices();
            if (Sel.Length > 0)
            {
                foreach (int i in Sel)
                {
                    VesselID = UDFLib.ConvertToInteger(lstVessel.Items[i].Value);
                    Res += objUser.INSERT_User_Vessel_Assignment(UserID, VesselID, GetSessionUserID());
                }


                if (Res > 0)
                {
                    GridView1.DataBind();

                    string js = "alert('" + Res + " Vessel(s) assigned to the user');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                }
                else
                {
                   
                        string js = "alert('Vessel is already assigned to the user!!');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
                    
                }
            }
            else //if no vessel is selected or assigned
            {
                string js = "alert('Select atleast one vessel');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", js, true);
            }
        }
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        if (ddlVessel.Items.Count > 0)
            ddlVessel.SelectedIndex = 0;
        
        txtUserName.Text = "";

    }

    protected void txtSearchUser_TextChanged(object sender, EventArgs e)
    {
        lstUserList.DataBind();
    }
    protected void txtUserName_TextChanged(object sender, EventArgs e)
    {
        GridView1.DataBind();
    }
}