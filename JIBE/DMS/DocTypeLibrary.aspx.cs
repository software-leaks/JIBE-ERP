using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Business.DMS;
using SMS.Properties;
using System.Data;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

public partial class DMS_DocTypeLibrary : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_DMS_Admin objDMS = new BLL_DMS_Admin();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    
    bool VesselConsidered = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataTable dtDocument = objCrewAdmin.GetDocumentSettings();
            if (dtDocument != null && dtDocument.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtDocument.Rows[0]["VesselFlagConsidered"]) == true)
                {
                    hdnVesselConsidered.Value = "VesselFlagConsidered";
                    VesselConsidered = false;
                    tdVesselFlagLabel.Visible = true;
                    tdVesselFlagData.Visible = true;
                    tdVesselFlagLabel1.Visible = true;
                    tdVesselFlagData1.Visible = true;

                    tdVesselLabel.Visible = false;
                    tdVesselData.Visible = false;
                    tdVesselLabel1.Visible = false;
                    tdVesselData1.Visible = false;

                    foreach (DataControlField col in GridViewDocType.Columns)
                    {
                        if (col.HeaderText == "Vessel")
                            col.Visible = false;
                        else if (col.HeaderText == "Vessel Flag")
                            col.Visible = true;
                    }
                }
                else if (Convert.ToBoolean(dtDocument.Rows[0]["VesselConsidered"]) == true)
                {
                    hdnVesselConsidered.Value = "VesselConsidered";
                    VesselConsidered = true;
                    tdVesselLabel.Visible = true;
                    tdVesselData.Visible = true;
                    tdVesselLabel1.Visible = true;
                    tdVesselData1.Visible = true;

                    tdVesselFlagLabel.Visible = false;
                    tdVesselFlagData.Visible = false;
                    tdVesselFlagLabel1.Visible = false;
                    tdVesselFlagData1.Visible = false;

                    foreach (DataControlField col in GridViewDocType.Columns)
                    {
                        if (col.HeaderText == "Vessel")
                            col.Visible = true;
                        else if (col.HeaderText == "Vessel Flag")
                            col.Visible = false;
                    }
                }

            }

            // Depending upon Crew Settings : Document section STCW_Deck & STCW_Engine will be enable or disabled
            if (Convert.ToBoolean(dtDocument.Rows[0]["STCW_Deck_Considered"]) == true)
            {
                trDeck1.Visible = true;
                hdnSTCW_Deck_Considered.Value = "1";
            }


            if (Convert.ToBoolean(dtDocument.Rows[0]["STCW_Engine_Considered"]) == true)
                trEngine1.Visible = true;
            else
                trEngine1.Visible = false;

            txtSearchText.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
            if (!IsPostBack)
            {
                Load_GroupList(ddlGroup);
                Load_RankList();
                Load_DocumentList();
                Load_VesselList();
                Load_VesselFlagList();
                Load_NationalityList();
                LoadDocumentList();
            }
            BindAssignedVesselAndVesselFlags();
            UserAccessValidation();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Bind Vessel Id and Vessel flagId for Administration Acceptance 
    /// </summary>
    protected void BindAssignedVesselAndVesselFlags()
    {
        objDMS = new BLL_DMS_Admin();
        DataTable dt = new DataTable();
        dt = objDMS.Get_VesselAndFlagIds_DocumentType("administrationacceptance");
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(dt.Rows[0]["VesselIds"]) != "0")
                hdnAssignedVesselForAdminAcceptance.Value = "|" + Convert.ToString(dt.Rows[0]["VesselIds"]).Replace("|", "||") + "|";
            else
                hdnAssignedVesselForAdminAcceptance.Value = "";
            if (Convert.ToString(dt.Rows[0]["Vessel_FlagIds"]) != "0")
                hdnAssignedVesselFlagsForAdminAcceptance.Value = "|" + Convert.ToString(dt.Rows[0]["Vessel_FlagIds"]).Replace("|", "||") + "|";
            else
                hdnAssignedVesselFlagsForAdminAcceptance.Value = "";
        }
    }

    protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewDocType.EditIndex = e.NewEditIndex;
        LoadDocumentList();
    }
    protected void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = UDFLib.ConvertToInteger(GridViewDocType.DataKeys[e.RowIndex].Value.ToString());

        int Voyage = 0;
        int isExpiryMandatory = 0;
        int isScannedDocMandatory = 0;
        int isDocCheckList = 0;

        string DocTypeName = e.NewValues["DocTypeName"].ToString();
        string Legend = e.NewValues["Legend"].ToString();
        string Deck = e.NewValues["Deck"].ToString();
        string Engine = e.NewValues["Engine"].ToString();
        int AlertDays = UDFLib.ConvertToInteger(e.NewValues["AlertDays"].ToString());
        if (e.NewValues["isDocCheckList"].ToString() == "True")
            isDocCheckList = 1;
        if (e.NewValues["Voyage"].ToString() == "True")
            Voyage = 1;
        if (e.NewValues["isExpiryMandatory"].ToString() == "True")
            isExpiryMandatory = 1;

        //******Added for DMS CR
        if (e.NewValues["isScannedDocMandatory"].ToString() == "True")
            isScannedDocMandatory = 1;
        //***********************
        int GroupId = UDFLib.ConvertToInteger(e.NewValues["GroupId"].ToString());

        objDMS.EditDocType(ID, GroupId, DocTypeName, Legend, Deck, Engine, AlertDays, isDocCheckList, Voyage, isExpiryMandatory, GetSessionUserID(), isScannedDocMandatory);
        GridViewDocType.EditIndex = -1;
        LoadDocumentList();
    }
    protected void GridView_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewDocType.EditIndex = -1;
        LoadDocumentList();
    }
    protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        BLL_Crew_Evaluation.DELETE_Category(UDFLib.ConvertToInteger(GridViewDocType.DataKeys[e.RowIndex].Value.ToString()), GetSessionUserID());
        LoadDocumentList();
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
        UserAccess objUA = new UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage( GetSessionUserID(), UDFLib.GetPageURL(Request.Path));

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            lnkNewDocument.Enabled = false;
            btnSelectReplacableDocuments.Enabled = false;
            btnSelectVesselFlags.Enabled = false;
            btnSelectVessels.Enabled = false;
            btnSelectRanks.Enabled = false;
            btnSelectCountry.Enabled = false;
        }
        else
        {
            lnkNewDocument.Enabled = true;
            btnSelectReplacableDocuments.Enabled = true;
            btnSelectVesselFlags.Enabled = true;
            btnSelectVessels.Enabled = true;
            btnSelectRanks.Enabled = true;
            btnSelectCountry.Enabled = true;
        }

        if (objUA.Edit == 0)
        {
            GridViewDocType.Columns[GridViewDocType.Columns.Count - 2].Visible = false;
            btnSelectReplacableDocuments.Enabled = false;
            btnSelectVesselFlags.Enabled = false;
            btnSelectVessels.Enabled = false;
            btnSelectRanks.Enabled = false;
            btnSelectCountry.Enabled = false;
        }
        else
        {
            GridViewDocType.Columns[GridViewDocType.Columns.Count - 2].Visible = true;
            btnSelectReplacableDocuments.Enabled = true;
            btnSelectVesselFlags.Enabled = true;
            btnSelectVessels.Enabled = true;
            btnSelectRanks.Enabled = true;
            btnSelectCountry.Enabled = true;
        }
        if (objUA.Delete == 0)
            GridViewDocType.Columns[GridViewDocType.Columns.Count - 1].Visible = false;
        else
            GridViewDocType.Columns[GridViewDocType.Columns.Count - 1].Visible = true;

    }
    public void Load_GroupList(DropDownList ddlGroup)
    {
        if (ddlGroup.Items.Count == 0)
        {
            ddlGroup.DataSource =objDMS.Get_GroupList();
            ddlGroup.DataTextField = "GroupName";
            ddlGroup.DataValueField = "GroupID";
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, new ListItem("-Select Group-", "0"));
        }
    }
    public void Load_DocumentList()
    {
        if (ddlDocument.Items.Count == 0)
        {
            ddlDocument.DataSource = objDMS.Get_DocumentList(0); 
            ddlDocument.DataTextField = "DocTypeName";
            ddlDocument.DataValueField = "DocTypeID";
            ddlDocument.DataBind();
            ddlDocument.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
    }
    public void Load_RankList()
    {
        if (ddlRank.Items.Count == 0)
        {
            BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
            DataTable dt = objCrewAdmin.Get_RankList();

            ddlRank.DataSource = dt;
            ddlRank.DataTextField = "Rank_Short_Name";
            ddlRank.DataValueField = "ID";
            ddlRank.DataBind();
            ddlRank.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
    }
    public void Load_VesselList()
    {
        if (ddlVessel.Items.Count == 0)
        {
            DataTable dt = objDMS.Get_VesselList(0);

            ddlVessel.DataSource = dt;
            ddlVessel.DataTextField = "Vessel_Name";
            ddlVessel.DataValueField = "Vessel_ID";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
    }
    public void Load_VesselFlagList()
    {
        if (ddlVesselFlag.Items.Count == 0)
        {
            ddlVesselFlag.DataSource = objDMS.Get_VesselFlagList(0);
            ddlVesselFlag.DataTextField = "Flag_Name";
            ddlVesselFlag.DataValueField = "ID";
            ddlVesselFlag.DataBind();
            ddlVesselFlag.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
    }
    public void Load_NationalityList()
    {
        if (ddlNationality.Items.Count == 0)
        {
            ddlNationality.DataSource = objDMS.Get_NationalityList(0);
            ddlNationality.DataTextField = "COUNTRY";
            ddlNationality.DataValueField = "ID";
            ddlNationality.DataBind();
            ddlNationality.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
    }
    protected void GridViewDocType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "VesselFlagList") != "" && DataBinder.Eval(e.Row.DataItem, "VesselFlagList") != null)
            {
                string input = DataBinder.Eval(e.Row.DataItem, "VesselFlagList").ToString();
                string[] stringArray = input.Split(',');

                CheckBoxList a = e.Row.FindControl("chkVesselFlagList") as CheckBoxList;
                if (a != null)
                {
                    foreach (ListItem chkitem in a.Items)
                        if (DataBinder.Eval(e.Row.DataItem, "VesselFlagList") != null && stringArray.Length > 0 && stringArray.Contains(chkitem.Value) == true)
                        {
                            chkitem.Selected = true;
                        }
                }
            }
        }
    }

    protected void SelectReplacableDocument(object source, CommandEventArgs e)
    {
        hdDocTypeId.Value = e.CommandArgument.ToString();
        DataTable dt = objDMS.Get_DocumentList(Convert.ToInt32(e.CommandArgument.ToString()));

        chkReplacableDocuments.DataSource = dt;
        chkReplacableDocuments.DataTextField = "DocTypeName";
        chkReplacableDocuments.DataValueField = "DocTypeId";
        chkReplacableDocuments.DataBind();

        int i = 0;
        foreach (ListItem chkitem in chkReplacableDocuments.Items)
        {
            if (dt.Rows[i]["Selected"].ToString() == "1")
                chkitem.Selected = true;
            i++;
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", "showModal('divReplacableDocumentList',false);", true);
        pnlReplacableDocuments.Update();
    }

    protected void SelectNationality(object source, CommandEventArgs e)
    {
        hdDocTypeId.Value = e.CommandArgument.ToString();
        DataTable dt = objDMS.Get_NationalityList(Convert.ToInt32(e.CommandArgument.ToString()));
        chkCountryList.DataSource = dt;
        chkCountryList.DataTextField = "COUNTRY";
        chkCountryList.DataValueField = "ID";
        chkCountryList.DataBind();

        int i = 0;
        chkSelectAllCountryList.Checked = true;
        foreach (ListItem chkitem in chkCountryList.Items)
        {
            if (dt.Rows[i]["Selected"].ToString() == "1")
                chkitem.Selected = true;
            else
                chkSelectAllCountryList.Checked = false;
            i++;
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", "showModal('divCountryList',false);", true);
        pnlCountries.Update();
    }

    protected void SelectRanks(object source, CommandEventArgs e)
    {
        hdDocTypeId.Value = e.CommandArgument.ToString();
        DataTable dt = objDMS.Get_RankList(Convert.ToInt32(e.CommandArgument.ToString()));
        chkRankList.DataSource = dt;
        chkRankList.DataTextField = "Rank_Name";
        chkRankList.DataValueField = "ID";
        chkRankList.DataBind();

        int i = 0;
        chkSelectAllRankList.Checked = true;
        foreach (ListItem chkitem in chkRankList.Items)
        {
            if (dt.Rows[i]["Selected"].ToString() == "1")
                chkitem.Selected = true;
            else
                chkSelectAllRankList.Checked = false;
            i++;
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", "showModal('divRankList',false);", true);
        pnlRanks.Update();
    }
    protected void SelectVessels(object source, CommandEventArgs e)
    {
        hdnVesselList.Value = "";
        hdDocTypeId.Value = e.CommandArgument.ToString();
        DataTable dt = objDMS.Get_VesselList(Convert.ToInt32(e.CommandArgument.ToString()));
        chkVesselList.DataSource = dt;
        chkVesselList.DataTextField = "Vessel_Name";
        chkVesselList.DataValueField = "Vessel_ID";
        chkVesselList.DataBind();

        int i = 0;
        chkSelectAllVesselList.Checked = true;
        foreach (ListItem chkitem in chkVesselList.Items)
        {
            if (dt.Rows[i]["Selected"].ToString() == "1")
            {
                chkitem.Selected = true;
                hdnVesselList.Value = hdnVesselList.Value.ToString() + i + ",";
            }
            else
            {
                chkSelectAllVesselList.Checked = false;
            }
            i++;
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", "showModal('divVesselList',false);", true);
        pnlVessels.Update();
    }
    protected void SelectVesselFlags(object source, CommandEventArgs e)
    {
        hdnVesselList.Value = "";
        hdDocTypeId.Value = e.CommandArgument.ToString();
        DataTable dt = objDMS.Get_VesselFlagList(Convert.ToInt32(e.CommandArgument.ToString()));
        chkVesselFlagList.DataSource = dt;
        chkVesselFlagList.DataTextField = "Flag_Name";
        chkVesselFlagList.DataValueField = "ID";
        chkVesselFlagList.DataBind();

        int i = 0;
        chkSelectAllVesselFlagList.Checked = true;
        foreach (ListItem chkitem in chkVesselFlagList.Items)
        {
            if (dt.Rows[i]["Selected"].ToString() == "1")
            {
                chkitem.Selected = true;
                hdnVesselList.Value = hdnVesselList.Value.ToString() + i + ",";
            }
            else
            {
                chkSelectAllVesselFlagList.Checked = false;
            }
            i++;
        }

        string msgdivResponseShow = string.Format("showModal('divVesselFlagList',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

        pnlVesselFlags.Update();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
            LoadDocumentList();
    }
    private void LoadDocumentList()
    {
        try
        {
            DataTable dt = objDMS.Get_DocTypeList_New(int.Parse(ddlDocument.SelectedValue), int.Parse(ddlGroup.SelectedValue), int.Parse(ddlVessel.SelectedValue), int.Parse(ddlVesselFlag.SelectedValue), int.Parse(ddlNationality.SelectedValue), int.Parse(ddlRank.SelectedValue), txtSearchText.Text.Trim());
            GridViewDocType.DataSource = dt;
            GridViewDocType.DataBind();
            Session["dtExportData"] = dt;
            UpdatePanel_Grid.Update();
            //hdnIsAdminAcceptance.Value;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "checkhdnSTCW_Deck_Considered", "checkhdnSTCW_Deck_Considered();", true);
        }
        catch(Exception ex) 
        {
            UDFLib.WriteExceptionLog(ex);
        }
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
            int DocTypeId1 = 0;
            if (hdDocTypeId.Value != null)
                DocTypeId1 = int.Parse(hdDocTypeId.Value);
            objDMS.INS_NationalityList(DocTypeId1, dt, UDFLib.ConvertToInteger(Session["UserID"].ToString()));

            LoadDocumentList();
            string msgdivResponseShow = string.Format("hideModal('divCountryList');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void SaveSelectedRanks(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PKID");
            dt.Columns.Add("ID");
            dt.Columns.Add("VALUE");

            foreach (ListItem chkitem in chkRankList.Items)
            {
                DataRow dr = dt.NewRow();
                dr["PKID"] = 0;
                dr["ID"] = chkitem.Value;
                dr["VALUE"] = chkitem.Selected == true ? 1 : 0;
                if (chkitem.Selected == true)
                    dt.Rows.Add(dr);
            }
            int DocTypeId1 = 0;
            if (hdDocTypeId.Value != null)
                DocTypeId1 = int.Parse(hdDocTypeId.Value);
            objDMS.INS_RankList(DocTypeId1, dt, UDFLib.ConvertToInteger(Session["UserID"].ToString()));

            LoadDocumentList();
            string msgdivResponseShow = string.Format("hideModal('divRankList');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void SaveSelectedVessels(object sender, EventArgs e)
    {
        try
        {
            int DocTypeId1 = 0;
            if (hdDocTypeId.Value != null)
                DocTypeId1 = int.Parse(hdDocTypeId.Value);

            #region Validate
            if (hdnIsAdminAcceptance.Value == "1")
            {
                bool chkResult = false;
                string VesselName = "";
                DataTable dtSelectedVessels = objDMS.Get_VesselList(DocTypeId1);
                foreach (ListItem chkitem in chkVesselList.Items)
                {
                    if (chkitem.Selected)
                    {
                        dtSelectedVessels.DefaultView.RowFilter = "";
                        dtSelectedVessels.DefaultView.RowFilter = "Vessel_ID=" + Convert.ToInt32(chkitem.Value) + " AND Selected=1";
                        if (dtSelectedVessels.DefaultView.Count == 0)
                        {
                            if (hdnAssignedVesselForAdminAcceptance.Value.Contains("|" + Convert.ToString(chkitem.Value) + "|"))
                            {
                                VesselName += chkitem.Text + ", ";
                                chkResult = true;
                            }
                        }
                    }
                }
                if (chkResult)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AnotherDocLink", "checkhdnSTCW_Deck_Considered();checkhdnSTCW_Deck_Considered();alert('Another document is linked to Administration Acceptance for " + VesselName.Trim().TrimEnd(',') + " vessel');showModal('divVesselList',false);", true);
                    return;
                }
            }
            #endregion

            DataTable dt = new DataTable();
            dt.Columns.Add("PKID");
            dt.Columns.Add("ID");
            dt.Columns.Add("VALUE");

            foreach (ListItem chkitem in chkVesselList.Items)
            {
                DataRow dr = dt.NewRow();
                dr["PKID"] = 0;
                dr["ID"] = chkitem.Value;
                dr["VALUE"] = chkitem.Selected == true ? 1 : 0;
                if (chkitem.Selected == true)
                    dt.Rows.Add(dr);
            }

            objDMS.INS_VesselList(DocTypeId1, dt, UDFLib.ConvertToInteger(Session["UserID"].ToString()));

            LoadDocumentList();
            string msgdivResponseShow = string.Format("hideModal('divVesselList');$('#hdnIsAdminAcceptance').val(0);checkhdnSTCW_Deck_Considered();checkhdnSTCW_Deck_Considered();alert('Saved successfully');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void SaveSelectedVesselFlags(object sender, EventArgs e)
    {
        try
        {
            int DocTypeId1 = 0;
            if (hdDocTypeId.Value != null)
                DocTypeId1 = int.Parse(hdDocTypeId.Value);

            #region Validate
            if (hdnIsAdminAcceptance.Value == "1")
            {
                bool chkResult = false;
                string VesselName = "";
                DataTable dtSelectedVessels = objDMS.Get_VesselFlagList(DocTypeId1);
                foreach (ListItem chkitem in chkVesselFlagList.Items)
                {
                    if (chkitem.Selected)
                    {
                        dtSelectedVessels.DefaultView.RowFilter = "";
                        dtSelectedVessels.DefaultView.RowFilter = "ID=" + Convert.ToInt32(chkitem.Value) + " AND Selected=1";
                        if (dtSelectedVessels.DefaultView.Count == 0)
                        {
                            if (hdnAssignedVesselFlagsForAdminAcceptance.Value.Contains("|" + Convert.ToString(chkitem.Value) + "|"))
                            {
                                VesselName += chkitem.Text + ", ";
                                chkResult = true;
                            }
                        }
                    }
                }
                if (chkResult)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AnotherDocLink", "checkhdnSTCW_Deck_Considered();checkhdnSTCW_Deck_Considered();alert('Another document is linked to Administration Acceptance for " + VesselName.Trim().TrimEnd(',') + " vessel flag');showModal('divVesselFlagList',false);", true);
                    return;
                }
            }
            #endregion

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

            objDMS.INS_VesselFlagList(DocTypeId1, dt, UDFLib.ConvertToInteger(Session["UserID"].ToString()));

            LoadDocumentList();
            string msgdivResponseShow = string.Format("hideModal('divVesselFlagList');$('#hdnIsAdminAcceptance').val(0);checkhdnSTCW_Deck_Considered();checkhdnSTCW_Deck_Considered();alert('Saved successfully');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    protected void SaveReplacableDocuments(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PKID");
            dt.Columns.Add("ID");
            dt.Columns.Add("VALUE");

            foreach (ListItem chkitem in chkReplacableDocuments.Items)
            {
                DataRow dr = dt.NewRow();
                dr["PKID"] = 0;
                dr["ID"] = chkitem.Value;
                dr["VALUE"] = chkitem.Selected == true ? 1 : 0;
                if (chkitem.Selected == true)
                    dt.Rows.Add(dr);
            }
            int DocTypeId1 = 0;
            if (hdDocTypeId.Value != null)
                DocTypeId1 = int.Parse(hdDocTypeId.Value);
            objDMS.INS_ReplacableDocumentList(DocTypeId1, dt, UDFLib.ConvertToInteger(Session["UserID"].ToString()));

            LoadDocumentList();
            string msgdivResponseShow = string.Format("hideModal('divVesselFlagList');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

        }
        catch { }
        {

        }
    }


    protected DataTable AddList(CheckBoxList chkList)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PID");

        foreach (ListItem chkitem in chkList.Items)
        {
            if (chkitem.Selected == true)
                dt.Rows.Add(int.Parse(chkitem.Value));
        }
        return dt;
    }

    protected DataTable SelectedList(CheckBoxList chkList)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PKID");
        dt.Columns.Add("ID");
        dt.Columns.Add("VALUE");

        foreach (ListItem chkitem in chkList.Items)
        {
            DataRow dr = dt.NewRow();
            dr["PKID"] = 0;
            dr["ID"] = chkitem.Value;
            dr["VALUE"] = chkitem.Selected == true ? 1 : 0;
            if (chkitem.Selected == true)
                dt.Rows.Add(dr);
        }
        return dt;
    }

    protected DataTable SelectedDDList(UserControl_ucCustomDropDownList ddlDocReplaceble)
    {
        DataTable dtDocRep = new DataTable();
        dtDocRep = (DataTable)(ddlDocReplaceble.SelectedValues);

        DataTable dt = new DataTable();
        dt.Columns.Add("PKID");
        dt.Columns.Add("ID");
        dt.Columns.Add("VALUE");

        foreach (DataRow drow in dtDocRep.Rows)
        {
            DataRow dr = dt.NewRow();
            dr["PKID"] = 0;
            dr["ID"] = drow["SelectedValue"];
            dr["VALUE"] = 1;
            dt.Rows.Add(dr);
        }
        return dt;
    }

    protected void LoadDocumentList(object sender, EventArgs e)
    {
        LoadDocumentList();
    }

    protected void btnSaveDocType_Click(object sender, EventArgs e)
    {
        try
        {
            #region Validate for administration acceptance
            bool chkResult = false;
            string VesselName = "";
            BindAssignedVesselAndVesselFlags();
            if (ddlDocmentType.SelectedValue.Trim() == "AdministrationAcceptance")
            {
                ///Check if Document type is Admin Acceptance and in Edit mode
                if (hdnIsAdminAcceptance.Value == "1" && hdDocTypeId.Value != "")
                {
                    if (VesselConsidered)
                    {
                        DataTable dtSelectedVessels = objDMS.Get_VesselList(int.Parse(hdDocTypeId.Value));
                        dtSelectedVessels.DefaultView.RowFilter = "";
                        dtSelectedVessels.DefaultView.RowFilter = "Selected=1 AND DocTypeId=" + Convert.ToInt32(hdDocTypeId.Value) + " AND Document_Type='AdministrationAcceptance'";

                        for (int i = 0; i < dtSelectedVessels.DefaultView.Count; i++)
                            hdnAssignedVesselForAdminAcceptance.Value = hdnAssignedVesselForAdminAcceptance.Value.Replace("|" + Convert.ToString(dtSelectedVessels.DefaultView[i]["Vessel_ID"]) + "|", "");

                        if (hdnAssignedVesselForAdminAcceptance.Value != "")
                        {
                            foreach (ListItem chkitem in chkVesselList1.Items)
                            {
                                if (chkitem.Selected)
                                {
                                    if (hdnAssignedVesselForAdminAcceptance.Value.Contains("|" + Convert.ToString(chkitem.Value) + "|"))
                                    {
                                        VesselName += chkitem.Text + ", ";
                                        chkResult = true;
                                        break;
                                    }
                                }
                            }
                        }

                        if (chkResult)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AnotherDocLinkVessel", "checkhdnSTCW_Deck_Considered();checkhdnSTCW_Deck_Considered();alert('Another document is linked to Administration Acceptance for " + VesselName.Trim().TrimEnd(',') + " vessel');showModal('dvAddDocType',false);", true);
                            return;
                        }

                    }
                    else
                    {
                        DataTable dtSelectedVessels = objDMS.Get_VesselFlagList(int.Parse(hdDocTypeId.Value));
                        dtSelectedVessels.DefaultView.RowFilter = "";
                        dtSelectedVessels.DefaultView.RowFilter = "Selected=1 AND DocTypeId=" + Convert.ToInt32(hdDocTypeId.Value) + " AND Document_Type='AdministrationAcceptance'";

                        for (int i = 0; i < dtSelectedVessels.DefaultView.Count; i++)
                            hdnAssignedVesselFlagsForAdminAcceptance.Value = hdnAssignedVesselFlagsForAdminAcceptance.Value.Replace("|" + Convert.ToString(dtSelectedVessels.DefaultView[i]["ID"]) + "|", "");

                        foreach (ListItem chkitem in chkVesselFlagList1.Items)
                        {
                            if (chkitem.Selected)
                            {
                                if (hdnAssignedVesselFlagsForAdminAcceptance.Value.Contains("|" + Convert.ToString(chkitem.Value) + "|"))
                                {
                                    VesselName += chkitem.Text + ", ";
                                    chkResult = true;
                                    break;
                                }
                            }
                        }

                        if (chkResult)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AnotherDocLinkVesselFlag", "checkhdnSTCW_Deck_Considered();checkhdnSTCW_Deck_Considered();alert('Another document is linked to Administration Acceptance for " + VesselName.Trim().TrimEnd(',') + " vessel flag');showModal('dvAddDocType',false);", true);
                            return;
                        }
                    }
                }
                else if (hdDocTypeId.Value == "")///While Adding new document
                {
                    if (VesselConsidered)
                    {
                        foreach (ListItem chkitem in chkVesselList1.Items)
                        {
                            if (chkitem.Selected)
                            {
                                if (hdnAssignedVesselForAdminAcceptance.Value.Contains("|" + Convert.ToString(chkitem.Value) + "|"))
                                {
                                    VesselName += chkitem.Text + ", ";
                                    chkResult = true;
                                }
                            }
                        }
                        if (chkResult)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AnotherDocLink", "checkhdnSTCW_Deck_Considered();checkhdnSTCW_Deck_Considered();alert('Another document is linked to Administration Acceptance for " + VesselName.Trim().TrimEnd(',') + " vessel');showModal('dvAddDocType',false);", true);
                            return;
                        }
                    }
                    else
                    {
                        foreach (ListItem chkitem in chkVesselFlagList1.Items)
                        {
                            if (chkitem.Selected)
                            {
                                if (hdnAssignedVesselFlagsForAdminAcceptance.Value.Contains("|" + Convert.ToString(chkitem.Value) + "|"))
                                {
                                    VesselName += chkitem.Text + ", ";
                                    chkResult = true;
                                }
                            }
                        }

                        if (chkResult)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AnotherDocLink", "checkhdnSTCW_Deck_Considered();checkhdnSTCW_Deck_Considered();alert('Another document is linked to Administration Acceptance for " + VesselName.Trim().TrimEnd(',') + " vessel flag');showModal('dvAddDocType',false);", true);
                            return;
                        }
                    }
                }
            }

            #endregion

            int AlertDays = 0;
            int AddToChecklist = 0;
            int VoyageDoc = 0;

            int ExpiryMandatory = 0;
            if (chkExpiryMandatory.Checked == true)
                ExpiryMandatory = 1;

            int ScannedDocMandatory = 0;
            if (ChkScannedDoc.Checked == true)
                ScannedDocMandatory = 1;

            if (txtAlertDays.Text.Trim().Length > 0)
                AlertDays = int.Parse(txtAlertDays.Text);

            if (chkCheckList.Checked == true)
                AddToChecklist = 1;

            if (chkVoyageDoc.Checked == true)
                VoyageDoc = 1;

            int GroupId = int.Parse(ddlGroup1.SelectedValue.ToString());
            int responseid;
            string s;

            //**************** Added for DMS CR****************
            if (GroupId == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMsg", "checkhdnSTCW_Deck_Considered();checkhdnSTCW_Deck_Considered();alert('Please select a Group');", true);
                return;
            }


            if (hdDocTypeId.Value != null && hdDocTypeId.Value != "")
            {
                DataTable dtVesselFlag1 = SelectedList(chkVesselFlagList1);
                DataTable dtVessel1 = SelectedList(chkVesselList1);
                DataTable dtRank1 = SelectedList(chkRankList1);
                DataTable dtCountry1 = SelectedList(chkCountryList1);

                DataTable dtReplacableDocument1 = SelectedDDList(ddlDocReplaceble);

                responseid = objDMS.EditDocType(int.Parse(hdDocTypeId.Value.ToString()), GroupId, txtDocType.Text.Trim(), txtLegend.Text.Trim(), txtDeck.Text.Trim(), txtEngine.Text.Trim(), AlertDays, AddToChecklist, VoyageDoc, ExpiryMandatory, dtVesselFlag1, dtVessel1, dtRank1, dtCountry1, dtReplacableDocument1, Convert.ToInt32(Session["USERID"]), ddlDocmentType.SelectedValue, ScannedDocMandatory);
                if (responseid == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "checkhdnSTCW_Deck_Considered();alert('Another document is already linked to " + ddlDocmentType.SelectedItem.Text.ToString() + ".Only one document can be linked to " + ddlDocmentType.SelectedItem.Text.ToString() + ".');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMsg", "checkhdnSTCW_Deck_Considered();alert('Document type updated successfully');", true);
                }
            }
            else
            {
                DataTable dtVesselFlag = AddList(chkVesselFlagList1);
                DataTable dtVessel = AddList(chkVesselList1);
                DataTable dtRank = AddList(chkRankList1);
                DataTable dtCountry = AddList(chkCountryList1);
                DataTable dtReplacableDocument = (DataTable)(ddlDocReplaceble.SelectedValues);
                responseid = objDMS.InsertDocType(txtDocType.Text.Trim(), GroupId, txtLegend.Text.Trim(), txtDeck.Text.Trim(), txtEngine.Text.Trim(), AlertDays, AddToChecklist, VoyageDoc, ExpiryMandatory, dtVesselFlag, dtVessel, dtRank, dtCountry, dtReplacableDocument, Convert.ToInt32(Session["USERID"]), ddlDocmentType.SelectedValue, ScannedDocMandatory);
                if (responseid == -1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "checkhdnSTCW_Deck_Considered();alert('Another document is already linked to " + ddlDocmentType.SelectedItem.Text.ToString() + ".Only one document can be linked to " + ddlDocmentType.SelectedItem.Text.ToString() + ".');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMsg", "checkhdnSTCW_Deck_Considered();alert('Document type saved successfully');", true);
                }
            }
            LoadDocumentList();
            Load_DocumentList();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "close", "closeDivAddDocType();", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void FillVesselFlagList(int DocTypeId)
    {
        DataTable dt = objDMS.Get_VesselFlagList(DocTypeId);
        chkVesselFlagList1.DataSource = dt;
        chkVesselFlagList1.DataTextField = "Flag_Name";
        chkVesselFlagList1.DataValueField = "ID";
        chkVesselFlagList1.DataBind();

        int i = 0;
        hdnVesselList.Value = "";
        chkSelectAllVesselFlagList1.Checked = true;
        foreach (ListItem chkitem in chkVesselFlagList1.Items)
        {
            chkitem.Attributes.Add("rel", dt.Rows[i]["ID"].ToString());
            if (dt.Rows[i]["Selected"].ToString() == "1")
            {
                chkitem.Selected = true;
                hdnVesselList.Value = hdnVesselList.Value.ToString() + i + ",";
            }
            else
            {
                chkSelectAllVesselFlagList1.Checked = false;
            }
            i++;
        }
    }
    protected void FillVesselList(int DocTypeId)
    {
        DataTable dt = objDMS.Get_VesselList(DocTypeId);
        chkVesselList1.DataSource = dt;
        chkVesselList1.DataTextField = "Vessel_Name";
        chkVesselList1.DataValueField = "Vessel_ID";
        chkVesselList1.DataBind();

        int i = 0;
        hdnVesselList.Value = "";
        chkSelectAllVesselList1.Checked = true;
        foreach (ListItem chkitem in chkVesselList1.Items)
        {
            chkitem.Attributes.Add("rel", dt.Rows[i]["Vessel_ID"].ToString());
            if (dt.Rows[i]["Selected"].ToString() == "1")
            {
                chkitem.Selected = true;
                hdnVesselList.Value = hdnVesselList.Value.ToString() + i + ",";
            }
            else
            {
                chkSelectAllVesselList1.Checked = false;
            }
            i++;
        }
    }
    protected void FillCountryList(int DocTypeId)
    {
        DataTable dt = objDMS.Get_NationalityList(DocTypeId);
        chkCountryList1.DataSource = dt;
        chkCountryList1.DataTextField = "COUNTRY";
        chkCountryList1.DataValueField = "ID";
        chkCountryList1.DataBind();

        int i = 0;
        chkSelectAllCountryList1.Checked = true;
        foreach (ListItem chkitem in chkCountryList1.Items)
        {
            if (dt.Rows[i]["Selected"].ToString() == "1")
            {
                chkitem.Selected = true;
            }
            else
            {
                chkSelectAllCountryList1.Checked = false;
            }
            i++;
        }
    }
    protected void FillRankList(int DocTypeId)
    {
        DataTable dt = objDMS.Get_RankList(DocTypeId);
        chkRankList1.DataSource = dt;
        chkRankList1.DataTextField = "Rank_Name";
        chkRankList1.DataValueField = "ID";
        chkRankList1.DataBind();

        int i = 0;

        chkSelectAllRankList1.Checked = true;
        foreach (ListItem chkitem in chkRankList1.Items)
        {
            if (dt.Rows[i]["Selected"].ToString() == "1")
            {
                chkitem.Selected = true;
            }
            else
            {
                chkSelectAllRankList1.Checked = false;
            }
            i++;
        }
    }
    protected void FillDocumentList(int DocTypeId)
    {
        DataTable dt = objDMS.Get_DocumentList(DocTypeId);


        ddlDocReplaceble.DataSource = dt;
        ddlDocReplaceble.DataTextField = "DocTypeName";
        ddlDocReplaceble.DataValueField = "DocTypeId";
        ddlDocReplaceble.DataBind();

        CheckBoxList chk = ddlDocReplaceble.FindControl("CheckBoxListItems") as CheckBoxList;
        int j = 0;
        foreach (ListItem chkitem in chk.Items)
        {
            if (dt.Rows[j]["Selected"].ToString() == "1")
            {
                chkitem.Selected = true;
            }
            j++;
        }

    }
    protected void AddNewDocument(object sender, EventArgs e)
    {
        try
        {
            btnSaveDocType.Text = "Save";
            hdDocTypeId.Value = null;
            chkVoyageDoc.Checked = false;
            chkCheckList.Checked = false;
            chkExpiryMandatory.Checked = false;
            ChkScannedDoc.Checked = true;
            txtDocType.Text = "";
            txtLegend.Text = "";
            txtDeck.Text = "";
            txtEngine.Text = "";
            txtAlertDays.Text = "";
            chkSelectAllVesselFlagList1.Checked = false;
            chkSelectAllVesselList1.Checked = false;
            chkSelectAllRankList1.Checked = false;
            chkSelectAllCountryList1.Checked = false;

            FillVesselFlagList(0);
            FillVesselList(0);
            FillCountryList(0);
            FillRankList(0);
            FillDocumentList(0);
            Load_GroupList(ddlGroup1);
            ddlGroup1.SelectedIndex = 0;

            ddlDocmentType.SelectedIndex = 0;
            string msgdivResponseShow = string.Format("showModal('dvAddDocType',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            hdDocName.Value = "";
            hdnDocumentType.Value = "";

            UpdatePanel1.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "checkhdnSTCW_Deck_Considered", "checkhdnSTCW_Deck_Considered();", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void EditDocument(object source, CommandEventArgs e)
    {
        try
        {
            BindAssignedVesselAndVesselFlags();
            btnSaveDocType.Text = "Update";
            hdDocTypeId.Value = e.CommandArgument.ToString();
            int DocTypeId = int.Parse(e.CommandArgument.ToString());

            chkSelectAllVesselFlagList1.Checked = false;
            chkSelectAllVesselList1.Checked = false;
            chkSelectAllRankList1.Checked = false;
            chkSelectAllCountryList1.Checked = false;

            DataTable dt = objDMS.Get_DocTypeList_New(DocTypeId, 0, 0, 0, 0, 0, "");
            hdnDocumentType.Value = Convert.ToString(dt.Rows[0]["Document_Type"]);

            FillVesselFlagList(DocTypeId);
            FillVesselList(DocTypeId);
            FillCountryList(DocTypeId);
            FillRankList(DocTypeId);
            FillDocumentList(DocTypeId);

            Load_GroupList(ddlGroup1);
            txtLegend.Text = dt.Rows[0]["Legend"].ToString();
            txtDeck.Text = dt.Rows[0]["Deck"].ToString();
            txtEngine.Text = dt.Rows[0]["Engine"].ToString();
            txtDocType.Text = dt.Rows[0]["DocTypeName"].ToString();
            txtAlertDays.Text = dt.Rows[0]["AlertDays"].ToString();
            hdDocName.Value = dt.Rows[0]["DocTypeName"].ToString();
            if (dt.Rows[0]["isDocCheckList"].ToString() == "true")
                chkCheckList.Checked = true;
            else
                chkCheckList.Checked = false;

            if (dt.Rows[0]["Voyage"].ToString() == "true")
                chkVoyageDoc.Checked = true;
            else
                chkVoyageDoc.Checked = false;

            if (dt.Rows[0]["isExpiryMandatory"].ToString() == "true")
                chkExpiryMandatory.Checked = true;
            else
                chkExpiryMandatory.Checked = false;

            //*********** Added for DMS CR
            if (dt.Rows[0]["isScannedDocMandatory"].ToString() == "true")
                ChkScannedDoc.Checked = true;
            else
                ChkScannedDoc.Checked = false;
            //***********

            if (dt.Rows[0]["GroupID"] != null && int.Parse(dt.Rows[0]["GroupID"].ToString()) > 0)
                ddlGroup1.SelectedValue = dt.Rows[0]["GroupID"].ToString();

            if (dt.Rows[0]["Document_Type"] != null)
                ddlDocmentType.SelectedValue = dt.Rows[0]["Document_Type"].ToString();


            string msgdivResponseShow = string.Format("document.getElementById('dvAddDocType').setAttribute('title', 'Edit Document Type');showModal('dvAddDocType',false);checkhdnSTCW_Deck_Considered();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);

            UpdatePanel1.Update();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void DeleteDocument(object source, CommandEventArgs e)
    {
        try
        {
            int i = objDMS.DeleteDocType(int.Parse(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
            string js = "Document deleted";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "checkhdnSTCW_Deck_Considered();alert('" + js + "');", true);
            LoadDocumentList();
            Load_DocumentList();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ImgExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            int Document = UDFLib.ConvertToInteger(ddlDocument.SelectedValue);
            int VesselID = UDFLib.ConvertToInteger(ddlVessel.SelectedValue);
            int Nationality = UDFLib.ConvertToInteger(ddlNationality.SelectedValue);
            int Rank_Category = UDFLib.ConvertToInteger(ddlRank.SelectedValue);
            int Group = UDFLib.ConvertToInteger(ddlGroup.SelectedValue);

            string SearchText = txtSearchText.Text;
            string strColname = string.Empty;
            string strColValue = string.Empty;

            DataTable dtDocument = objCrewAdmin.GetDocumentSettings();
            if (dtDocument != null && dtDocument.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtDocument.Rows[0]["VesselFlagConsidered"]) == true)
                {
                    strColname = "VesselFlag";
                    strColValue = "VesselFlagNameList";
                }
                else
                {
                    strColname = "Vessel";
                    strColValue = "VesselNames";
                }
            }

            string[] HeaderCaptions = { "Type Name", "Group", "Alert", "Required for Signon", "Voyage Specific", "Expiry Mandatory", "Scanned Required", strColname, "Ranks", "Nationality", "Link to Document Type", "Course Replace" };
            string[] DataColumnsName = { "DocTypeName", "GroupName", "AlertDays", "isDocChkLst", "isVoyage", "isExpMand", "isScanDocMand", strColValue, "RankListDetails", "CountryListDetails", "Document_Type", "ReplacableDocumentList" };

            DataTable dt = new DataTable();
            dt = (DataTable)(Session["dtExportData"]);

            GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, "DocumentTypeLibarary.xls", "Document Type Libarary Export");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void ClearFilter(object sender, EventArgs e)
    {
        try
        {
            ddlDocument.SelectedIndex = 0;
            ddlVessel.SelectedIndex = 0;
            ddlVessel.SelectedIndex = 0;
            ddlVesselFlag.SelectedIndex = 0;
            ddlNationality.SelectedIndex = 0;
            ddlRank.SelectedIndex = 0;
            ddlGroup.SelectedIndex = 0;
            txtSearchText.Text = "";
            LoadDocumentList();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "checkhdnSTCW_Deck_Considered", "checkhdnSTCW_Deck_Considered();", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }



}