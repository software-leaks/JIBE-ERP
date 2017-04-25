using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;

public partial class Infrastructure_InspectionUserVessel : System.Web.UI.Page
{
    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

    DataTable dtvsl = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL();
            Load_VesselList();

        }
    }

    protected void FillDDL()
    {
        if (Session["USERCOMPANYID"] != null)
        {
            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));


            ddlFleet.DataSource = FleetDT;
            ddlFleet.DataTextField = "Name";
            ddlFleet.DataValueField = "code";
            ddlFleet.DataBind();
            ListItem li = new ListItem("--SELECT--", "0");
            ddlFleet.Items.Insert(0, li);

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
            btnAssign.Enabled = false;
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
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }



    public void Load_VesselList()
    {
        if (Session["USERCOMPANYID"] != null)
        {
            int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
            int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
            int Vessel_Manager = 1;

            if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
                Vessel_Manager = UserCompanyID;

            lstVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

            lstVessel.DataTextField = "VESSEL_NAME";
            lstVessel.DataValueField = "VESSEL_ID";
            lstVessel.DataBind();

        }
    }


    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();

        if (lstUserList.SelectedIndex != -1)
            lstUserList_SelectedIndexChanged(null, null);
    }

    /// <summary>
    /// Added by Anjali DT:20-06-2016 JIT:9496
    /// To assign one or more vessel to selected user.
    /// </summary>
    private void AssignVesseltoUser()
    {
        try
        {
            if (ValidateControls())
            {
                int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
                DataTable dtvslAllSelected = BLL_Infra_Common.Get_User_Vessel_Assignment(Convert.ToInt32(lstUserList.SelectedValue), null, UserCompanyID);

                if (dtvslAllSelected != null)
                {

                    if (dtvslAllSelected.Rows.Count > 0)
                    {
                        if (ddlFleet.SelectedIndex > 0)
                        {
                            foreach (DataRow item in dtvslAllSelected.Rows)
                            {
                                if (item["FleetCode"].ToString() != ddlFleet.SelectedValue && item["ASSIGNED"].ToString() != "-1")
                                {
                                    DataRow dr = dtvsl.NewRow();
                                    dr["ID"] = item["Vessel_ID"].ToString();
                                    dtvsl.Rows.Add(dr);
                                }
                            }
                        }

                        BLL_Infra_Common.Upd_User_Vessel_Assignment(Convert.ToInt32(lstUserList.SelectedValue), dtvsl, Convert.ToInt32(Session["USERID"]));
                        string js1 = "alert('Vessel assignment is saved successfully.');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js1, true);
                        dtvsl = null;

                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Added by Anjali DT:20-06-2016 JIT:9496
    /// To check validation of controls
    /// Validation 1.: User selection is mandatory.
    ///            2. Vessel need to be selected other than '--SELECT--'
    /// </summary>
    /// <returns>True : if above validation successed. || False : if validation fails.</returns>
    private bool ValidateControls()
    {
        try
        {
            if (lstUserList.SelectedValue == string.Empty)
            {
                string js1 = "alert('Please select user.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js1, true);
                lstUserList.Focus();
                return false;
            }

            dtvsl.Columns.Add("ID");
            foreach (ListItem litem in lstVessel.Items)
            {
                if (litem.Selected)
                {
                    DataRow dr = dtvsl.NewRow();
                    dr["ID"] = litem.Value;
                    dtvsl.Rows.Add(dr);
                }
            }

            if (dtvsl.Rows.Count <= 0)
            {
                string js1 = "alert('Please select vessel.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js1, true);
                lstVessel.Focus();
                return false;
            }
            if ((dtvsl.Rows.Count == 1) && (dtvsl.Rows[0][0].ToString() == "-1"))
            {
                string js2 = "alert('Select valid vessel.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", js2, true);
                lstVessel.Focus();
                return false;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return true;

    }
    /// <summary>
    /// Assign one or more vessel to selected user.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        try
        {
            AssignVesseltoUser();
        }
        catch (Exception ex)
        {
            string jsSqlError = "alert('" + ex.Message + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError", jsSqlError, true);
        }
    }


    protected void lstUserList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        DataTable dtvsl = BLL_Infra_Common.Get_User_Vessel_Assignment(Convert.ToInt32(lstUserList.SelectedValue), ddlFleet.SelectedIndex == 0 ? null : UDFLib.ConvertIntegerToNull(ddlFleet.SelectedValue), UserCompanyID);

        lstVessel.DataSource = dtvsl;
        lstVessel.DataBind();
        // lstVessel.Items.Insert(0, new ListItem("-SELECT-", "-1"));
        lstVessel.ClearSelection();
        chkSelectAll.Checked = false;
        foreach (DataRow dr in dtvsl.Rows)
        {
            ListItem li = lstVessel.Items.FindByValue(dr["ASSIGNED"].ToString());
            if (li != null)
                li.Selected = true;
        }

    }

    /// <summary>
    /// Checked or unchecked Vessel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < lstVessel.Items.Count; i++)
            lstVessel.Items[i].Selected = chkSelectAll.Checked;
    }
}