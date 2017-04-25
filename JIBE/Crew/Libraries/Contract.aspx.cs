using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;

public partial class Crew_Libraries_Contract : System.Web.UI.Page
{
    BLL_Crew_Contract objBLLCrewContract = new BLL_Crew_Contract();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_ContractList();
        }
    }
    protected void UserAccessValidation()
    {
        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            dvPageContent.Visible = false;
        }
        if (objUA.Add == 0)
        {
            lnkAddNewContract.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            gvContract.Columns[gvContract.Columns.Count - 2].Visible = false;

        }
        if (objUA.Delete == 0)
        {
            gvContract.Columns[gvContract.Columns.Count - 1].Visible = false;
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
    protected void Load_ContractList()
    {
        DataTable dt = objBLLCrewContract.Get_ContractList(0);
        gvContract.DataSource = dt;
        gvContract.DataBind();
    }
    protected void gvContract_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(gvContract.DataKeys[e.RowIndex].Value.ToString());

        objBLLCrewContract.DELETE_Contract(ID, GetSessionUserID());
        Load_ContractList();
    }
    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        int ContractId = 0;
        lblMsg.Text = "";
        lblMsg.Visible = false;
        int ID = 0;
        DataTable dt = new DataTable();
        if (hdContractId.Value != null && hdContractId.Value != "")
        {
            ContractId = int.Parse(hdContractId.Value.ToString());

            dt.Columns.Add("PKID");
            dt.Columns.Add("ID");
            dt.Columns.Add("VALUE");
            if (rdbContractType.SelectedValue.ToString() == "Nationality")
            {
                foreach (ListItem chkitem in chkCountryList1.Items)
                {
                    DataRow dr = dt.NewRow();
                    dr["PKID"] = 0;
                    dr["ID"] = chkitem.Value;
                    dr["VALUE"] = chkitem.Selected == true ? 1 : 0;
                    if (chkitem.Selected == true)
                        dt.Rows.Add(dr);
                }
            }
            else
            {
                foreach (ListItem chkitem in chkVesselFlagList1.Items)
                {
                    DataRow dr = dt.NewRow();
                    dr["PKID"] = 0;
                    dr["ID"] = chkitem.Value;
                    dr["VALUE"] = chkitem.Selected == true ? 1 : 0;
                    if (chkitem.Selected == true)
                        dt.Rows.Add(dr);
                }
            }
            ID = objBLLCrewContract.Update_Contract(ContractId, rdbContractType.SelectedValue.ToString(), dt, txtContractName.Text.Trim(), GetSessionUserID());
        }
        else
        {
            dt.Columns.Add("PID");

            if (rdbContractType.SelectedValue.ToString() == "Nationality")
            {
                foreach (ListItem chkitem in chkCountryList1.Items)
                {
                    if (chkitem.Selected == true)
                        dt.Rows.Add(int.Parse(chkitem.Value));
                }
            }
            else
            {
                foreach (ListItem chkitem in chkVesselFlagList1.Items)
                {
                    if (chkitem.Selected == true)
                        dt.Rows.Add(int.Parse(chkitem.Value));
                }
            }
            ID = objBLLCrewContract.Insert_Contract(rdbContractType.SelectedValue.ToString(), dt, txtContractName.Text.Trim(), GetSessionUserID());
            if (ID <= 0)
            {
                string js = "Contract Name already exsist!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js + "');", true);
            }
        }

        if (ID > 0)
        {
            txtContractName.Text = "";
            Load_ContractList();

            string msgdivResponseShow = string.Format("hideModal('dvAddNewContract');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
        }
    }

    protected void SelectNationality(object source, CommandEventArgs e)
    {
        hdContractId.Value = e.CommandArgument.ToString();
        DataTable dt = objBLLCrewContract.Get_NationalityList(Convert.ToInt32(e.CommandArgument.ToString()));
        chkCountryList.DataSource = dt;
        chkCountryList.DataTextField = "COUNTRY";
        chkCountryList.DataValueField = "ID";
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

        string msgdivResponseShow = string.Format("showModal('divCountryList',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

        pnlCountries.Update();
    }
    protected void SelectVesselFlags(object source, CommandEventArgs e)
    {
        hdContractId.Value = e.CommandArgument.ToString();
        DataTable dt = objBLLCrewContract.Get_VesselFlagList(Convert.ToInt32(e.CommandArgument.ToString()));
        chkVesselFlagList.DataSource = dt;
        chkVesselFlagList.DataTextField = "Flag_Name";
        chkVesselFlagList.DataValueField = "ID";
        chkVesselFlagList.DataBind();

        int i = 0;
        foreach (ListItem chkitem in chkVesselFlagList.Items)
        {
            if (dt.Rows[i]["Selected"].ToString() == "1")
            {
                chkitem.Selected = true;
            }
            i++;
        }

        string msgdivResponseShow = string.Format("showModal('divVesselFlagList',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

        pnlVesselFlags.Update();
    }
    protected void SaveSelectedCountries(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PKID");
            dt.Columns.Add("ID");
            dt.Columns.Add("VALUE");

            foreach (ListItem chkitem in chkCountryList.Items)
            {
                DataRow dr = dt.NewRow();
                dr["PKID"] = 0;
                dr["ID"] = chkitem.Value;
                dr["VALUE"] = chkitem.Selected == true ? 1 : 0;
                if (chkitem.Selected == true)
                    dt.Rows.Add(dr);
            }
            int ContractId = 0;
            if (hdContractId.Value != null)
                ContractId = int.Parse(hdContractId.Value);
            objBLLCrewContract.INS_NationalityList(ContractId, dt, UDFLib.ConvertToInteger(Session["UserID"].ToString()));

            Load_ContractList();
            string msgdivResponseShow = string.Format("hideModal('divCountryList');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            UpdatePanel1.Update();
        }
        catch { }
        {

        }
    }
    protected void SaveSelectedVesselFlags(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PKID");
            dt.Columns.Add("ID");
            dt.Columns.Add("VALUE");

            foreach (ListItem chkitem in chkVesselFlagList.Items)
            {
                DataRow dr = dt.NewRow();
                dr["PKID"] = 0;
                dr["ID"] = chkitem.Value;
                dr["VALUE"] = chkitem.Selected == true ? 1 : 0;
                if (chkitem.Selected == true)
                    dt.Rows.Add(dr);
            }
            int DocTypeId1 = 0;
            if (hdContractId.Value != null)
                DocTypeId1 = int.Parse(hdContractId.Value);
            objBLLCrewContract.INS_VesselFlagList(DocTypeId1, dt, UDFLib.ConvertToInteger(Session["UserID"].ToString()));

            Load_ContractList();
            string msgdivResponseShow = string.Format("hideModal('divVesselFlagList');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

        }
        catch { }
        {

        }
    }
    protected void AddNewContract(object sender, EventArgs e)
    {
        hdContractId.Value = null;
        txtContractName.Text = "";

        rdbContractType.SelectedValue = "Nationality";
        chkCountryList1.Enabled = true;
        chkVesselFlagList1.Enabled = false;

        FillCountryList(0);
        FillVesselFlagList(0);
        string msgdivResponseShow = string.Format("showModal('dvAddNewContract',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

        UpdatePanel1.Update();
    }
    protected void FillVesselFlagList(int ContractId)
    {
        DataTable dt = objBLLCrewContract.Get_VesselFlagList(ContractId);
        chkVesselFlagList1.DataSource = dt;
        chkVesselFlagList1.DataTextField = "Flag_Name";
        chkVesselFlagList1.DataValueField = "ID";
        chkVesselFlagList1.DataBind();

        int i = 0;
        foreach (ListItem chkitem in chkVesselFlagList1.Items)
        {
            if (dt.Rows[i]["Selected"].ToString() == "1")
            {
                chkitem.Selected = true;
            }
            i++;
        }
    }
    protected void FillCountryList(int ContractId)
    {
        DataTable dt = objBLLCrewContract.Get_NationalityList(ContractId);
        chkCountryList1.DataSource = dt;
        chkCountryList1.DataTextField = "COUNTRY";
        chkCountryList1.DataValueField = "ID";
        chkCountryList1.DataBind();

        int i = 0;
        foreach (ListItem chkitem in chkCountryList1.Items)
        {
            if (dt.Rows[i]["Selected"].ToString() == "1")
            {
                chkitem.Selected = true;
            }
            i++;
        }
    }
    protected void EditContract(object source, CommandEventArgs e)
    {
        hdContractId.Value = e.CommandArgument.ToString();
        int ContractId = int.Parse(e.CommandArgument.ToString());
        DataTable dt = objBLLCrewContract.Get_ContractList(ContractId);

        FillCountryList(ContractId);
        FillVesselFlagList(ContractId);
        txtContractName.Text = dt.Rows[0]["Contract_Name"].ToString();
        rdbContractType.SelectedValue = dt.Rows[0]["Contract_Type"].ToString();

        if (rdbContractType.SelectedValue.ToString() == "Nationality")
        {
            chkCountryList1.Enabled = true;
            chkVesselFlagList1.Enabled = false;
        }
        else
        {
            chkVesselFlagList1.Enabled = true;
            chkCountryList1.Enabled = false;
        }

        string msgdivResponseShow = string.Format("showModal('dvAddNewContract',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

        UpdatePanel1.Update();
    }
    protected void rdbContractType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbContractType.SelectedValue.ToString() == "Nationality")
        {
            chkCountryList1.Enabled = true;
            chkVesselFlagList1.Enabled = false;
        }
        else
        {
            chkVesselFlagList1.Enabled = true;
            chkCountryList1.Enabled = false;
        }
    }
}