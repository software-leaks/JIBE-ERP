using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PortageBill;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;

/// <summary>
/// Library Page to set Vessel wise, Nationality wise , Rank wise : Allowance
/// According to this setting,ship allowance will be auto populated in wages 
/// </summary>
public partial class PortageBill_VesselAllowance : System.Web.UI.Page
{
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    UserAccess objUA = new UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        lblMessage.Text = "";

        if (!IsPostBack)
        {
            try
            {
                Load_VesselList();
            }
            catch (Exception ex)
            {
                //ErrorLogHandler.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);

            }
        }
    }

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
        string PageURL = UDFLib.GetPageURL(Request.Path);

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

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
    }

    public void Load_VesselList()
    {        
        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
        DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

        lstVesselList.DataSource = dtVessel;
        lstVesselList.DataTextField = "Vessel_Name";
        lstVesselList.DataValueField = "Vessel_ID";
        lstVesselList.DataBind();
        lstVesselList.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void lstVesselList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["NationalityGroupId"] = null;
        lstCountryList1.Items.Clear();
        txteffdt.Text = "";
        DataTable dt = new DataTable();
        gvVesselAllowance.DataSource = dt;
        gvVesselAllowance.DataBind();

        if (lstVesselList.SelectedItem != null && lstVesselList.SelectedIndex > 0)
        {
            btnAddNationalityGroup.Enabled = true;
            LoadNationalityGroup(UDFLib.ConvertToInteger(lstVesselList.SelectedItem.Value));
        }
        else
        {
            btnAddNationalityGroup.Enabled = false;
            gvNationalityGroup.DataSource = dt;
            gvNationalityGroup.DataBind();
        }
    }
    private void LoadNationalityGroup(int VesselId)
    {
        DataTable dt = BLL_PB_VesselAllowance.Get_NationalityGroupForVesselAllowance(VesselId);
        gvNationalityGroup.DataSource = dt;
        gvNationalityGroup.DataBind();
    }
    public void GroupSelected(object sender, CommandEventArgs e)
    {
        string[] cmdargs = e.CommandArgument.ToString().Split(',');
        int RowIndex = int.Parse(cmdargs[0].ToString());
        int NationalityGroupId = int.Parse(cmdargs[1].ToString());
        ViewState["NationalityGroupId"] = NationalityGroupId;
        txteffdt.Enabled = true;

        for (int i = 0; i < gvNationalityGroup.Rows.Count; i++)
        {
            GridViewRow selectedRow = gvNationalityGroup.Rows[i];
            if ( i ==  RowIndex)
                selectedRow.BackColor = System.Drawing.Color.SkyBlue; 
            else
                selectedRow.BackColor = System.Drawing.Color.White; 
        }

        DataSet ds = BLL_PB_VesselAllowance.Get_RankWiseVesselAllowance(NationalityGroupId);

        lstCountryList1.Items.Clear();
        lstCountryList1.DataSource = ds.Tables[0];
        lstCountryList1.DataTextField = "Country_Name";
        lstCountryList1.DataValueField = "CountryID";
        lstCountryList1.DataBind();        

        gvVesselAllowance.DataSource = ds.Tables[1];
        gvVesselAllowance.DataBind();

        if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
        {
            txteffdt.Text = ds.Tables[2].Rows[0]["EffectiveDate"].ToString();
        }
   }
    protected void btnSaveNationalityGroup_OnClick(object sender, EventArgs e)
    {
        string js = "";
        int VesselId = UDFLib.ConvertToInteger(lstVesselList.SelectedItem.Value);
        string GroupName = txtGroupName.Text.Trim();
        
        lstCountryList1.Items.Clear();
        txteffdt.Text = "";
        DataTable dt2 = new DataTable();
        gvVesselAllowance.DataSource = dt2;
        gvVesselAllowance.DataBind();

        DataTable dt = new DataTable();
        dt.Columns.Add("NationalityGroupId", typeof(int));
        dt.Columns.Add("CountryId", typeof(int));

        DataTable dt1 = new DataTable();
        dt1.Columns.Add("Country_ID", typeof(int));
        dt1.Columns.Add("Country_Name", typeof(string));

        int NationalityGroupId = 0;
        if (ViewState["NationalityGroupId"] != null)
            NationalityGroupId = int.Parse(ViewState["NationalityGroupId"].ToString());

        List<int> lstCountryList = new List<int>();
        for (int i = 0; i < chkCountryList.Items.Count; i++)
        {
            if (chkCountryList.Items[i].Selected)
            {
                DataRow dr = dt.NewRow();
                dr["NationalityGroupId"] = NationalityGroupId;
                dr["CountryId"] = chkCountryList.Items[i].Value;
                dt.Rows.Add(dr);
                dt.AcceptChanges();

                DataRow dr1 = dt1.NewRow();
                dr1["Country_ID"] = chkCountryList.Items[i].Value;
                dr1["Country_Name"] = chkCountryList.Items[i].Text;
                dt1.Rows.Add(dr1);
                dt1.AcceptChanges();
            }
        }

        lstCountryList1.DataSource = dt1;
        lstCountryList1.DataTextField = "Country_Name";
        lstCountryList1.DataValueField = "Country_ID";
        lstCountryList1.DataBind();

        DataTable dtCheck = BLL_PB_VesselAllowance.Check_NationalityGroup(VesselId, NationalityGroupId, GroupName, dt);
        if (dtCheck != null && dtCheck.Rows.Count > 0)
        {
            if (int.Parse(dtCheck.Rows[0]["GroupNameExsist"].ToString()) > 0 )
                js = "Nationality Group already exsist";
            if (int.Parse(dtCheck.Rows[0]["CountryNameExsist"].ToString()) > 0)
                js = "One or more Countries belongs to other Nationality Group";
            if (js != "")
            {
                string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divCountryList',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            }
        }
        if (js == "")
        {
            int retVal = BLL_PB_VesselAllowance.Insert_NationalityGroup(VesselId, NationalityGroupId, GroupName, dt, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
            js = "Nationality Group Created";

            string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divCountryList',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

            LoadNationalityGroup(VesselId);
            UpdatePanel2.Update();
        }
        ViewState["NationalityGroupId"] = null;
    }
    protected void btnAddSelectedCountries(object sender, EventArgs e)
    {
        lstCountryList1.Items.Clear();

        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("COUNTRY", typeof(string));

        List<int> lstCountryList = new List<int>();
        for (int i = 0; i < chkCountryList.Items.Count; i++)
        {
            if (chkCountryList.Items[i].Selected)
            {
                DataRow dr = dt.NewRow();

                dr["ID"] = chkCountryList.Items[i].Value;
                dr["COUNTRY"] = chkCountryList.Items[i].Text;
                dt.Rows.Add(dr);
                dt.AcceptChanges();
            }
        }

        lstCountryList1.DataSource = dt;
        lstCountryList1.DataBind();

        string msgdivResponseShow = string.Format("hideModal('divCountryList',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

        UpdatePanel2.Update();
        pnlNationalityGroup.Visible = false;
    }
    protected void btnSaveAllowance_OnClick(object sender, EventArgs e)
    {    
        DataTable dt = new DataTable();
        dt.Columns.Add("CountryId", typeof(int));
        dt.Columns.Add("RankId", typeof(int));
        dt.Columns.Add("Amount", typeof(int));

        double amt = 0;

        for (int i = 0; i < lstCountryList1.Items.Count; i++)
        {
            foreach (GridViewRow gr in gvVesselAllowance.Rows)
            {
                if (Convert.ToString(((TextBox)gr.FindControl("txtAmount")).Text) != "")
                {
                    if (double.TryParse(((TextBox)gr.FindControl("txtAmount")).Text, out amt))
                    {
                        if (amt > 0)
                        {
                            int RankId = int.Parse(((Label)gr.FindControl("lblRankId")).Text);

                            DataRow dr = dt.NewRow();
                            dr["CountryId"] = lstCountryList1.Items[i].Value;
                            dr["RankId"] = RankId;
                            dr["Amount"] = amt;
                            dt.Rows.Add(dr);
                            dt.AcceptChanges();
                        }
                    }
                }
            }
        }
        IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
        DateTime EffectiveDate = DateTime.Parse(txteffdt.Text.Trim(), iFormatProvider);

        int VesselId = UDFLib.ConvertToInteger(lstVesselList.SelectedItem.Value);
        int retVal = BLL_PB_VesselAllowance.Insert_VesselAllowance(VesselId, dt,EffectiveDate, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
        string js = "Vessel Allowance Created";
        string msgdivResponseShow = string.Format("alert('" + js + "');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
    }
    protected void btnAddNationalityGroup_OnClick(object sender, EventArgs e)
    {
        ViewState["NationalityGroupId"] = null;
        txtGroupName.Text = "";
        DataTable dt = new DataTable();
        dt = BLL_PB_VesselAllowance.Get_NationalityForVesselAllowance(0);

        chkCountryList.DataSource = dt;
        chkCountryList.DataBind();

        string msgdivResponseShow = string.Format("showModal('divNationalityGroup',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

        pnlNationalityGroup.Update();
    }
    protected void DeleteNationality(object source, CommandEventArgs e)
    {
        int i = BLL_PB_VesselAllowance.DeleteNationalityGroup(int.Parse(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        string js = "Nationality deleted";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js + "');", true);

        if (lstVesselList.SelectedItem != null)
        {
            btnAddNationalityGroup.Enabled = true;
            LoadNationalityGroup(UDFLib.ConvertToInteger(lstVesselList.SelectedItem.Value));
        }
    }
    protected void AddNationality(object source, CommandEventArgs e)
    {     
           string[] cmdargs = e.CommandArgument.ToString().Split(',');
        
           int GroupId = UDFLib.ConvertToInteger(cmdargs[0].ToString());
           txtGroupName.Text = cmdargs[1].ToString();
            DataTable dt = new DataTable();
            dt = BLL_PB_VesselAllowance.Get_NationalityForVesselAllowance(GroupId);

            chkCountryList.DataSource = dt;
            chkCountryList.DataBind();
            int i = 0;
            foreach (ListItem chkitem in chkCountryList.Items)
            {
                if (dt.Rows[i]["Selected"].ToString() == "1")
                {
                    chkitem.Selected = true;
                }
                i++;
            }
            string msgdivResponseShow = string.Format("showModal('divNationalityGroup',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

            pnlNationalityGroup.Update();      
    }
}