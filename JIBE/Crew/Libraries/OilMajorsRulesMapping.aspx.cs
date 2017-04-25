using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Properties;
using SMS.Business.Infrastructure;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Crew_Libraries_OilMajorsRulesMapping : System.Web.UI.Page
{
    #region Declarations
    BLL_Crew_Admin objBLL;

    public string OperationMode = "Add New Rule";
    public int Result = 0;
    public DataTable dt, dtRanks;
    public bool editAccess = true, deleteAccess = true;
    public string JSONString = string.Empty;
    #endregion

    /// <summary>
    /// Page Load event 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (GetSessionUserID() == 0)
                Response.Redirect("~/account/login.aspx");
            else
                UserAccessValidation();

            BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
            dtRanks = objCrewAdmin.Get_RankList();

            if (!IsPostBack)
                BindRuleMappingPopup();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Check for access rights
    /// </summary>
    protected void UserAccessValidation()
    {
        try
        {
            UserAccess objUA = new UserAccess();
            int CurrentUserID = GetSessionUserID();
            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

            if (objUA.View == 0)
                Response.Redirect("~/default.aspx?msgid=1");

            if (objUA.Delete == 0)
                deleteAccess = false;
            else
                deleteAccess = true;

            if (objUA.Edit == 0)
            {
                chkOilMajors.Visible = false;
                chklstRanks.Enabled = editAccess = false;
            }
            else
            {
                editAccess = true;
                chkOilMajors.Visible = true;
            }

            if (objUA.Admin == 1)
            {
                chklstRanks.Enabled = btnCreateNewRule.Visible = true;
                btnCreateRuleAssign.Visible = true;
                chkOilMajors.Visible = true;
            }
            else
            {
                btnCreateNewRule.Visible = false;
                btnCreateRuleAssign.Visible = false;
                chklstRanks.Enabled = false;
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    /// <summary>
    /// Get UserId
    /// </summary>
    /// <returns></returns>
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    /// <summary>
    /// Bind OilMajors, Ranks, Rule Groups, Rules and grid
    /// </summary>
    private void BindRuleMappingPopup()
    {
        try
        {
            DataSet ds = new DataSet();
            objBLL = new BLL_Crew_Admin();
            ds = objBLL.Bind_Rule_Mapping_Popup("R", Convert.ToInt32(drpOilMajorsFilter.SelectedValue == "" ? 0 : Convert.ToInt32(drpOilMajorsFilter.SelectedValue)), 0);
            
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {

                    #region Bind Oil majors to popup and filter Dropdown
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (drpOilMajorsFilter.SelectedValue == "")
                        {
                            //Bind Oil Majors to Filter Dropdown
                            drpOilMajorsFilter.DataSource = ds.Tables[0];
                            drpOilMajorsFilter.DataTextField = "Oil_Major_Name";
                            drpOilMajorsFilter.DataValueField = "ID";
                            drpOilMajorsFilter.DataBind();
                            drpOilMajorsFilter.Items.Insert(0, new ListItem { Text = "-Select All-", Value = "0" });
                        }
                        rptOilMajorsValue.DataSource = ds.Tables[0];
                        rptOilMajorsValue.DataBind();

                        bindOilMajors(ds.Tables[0]);
                    }
                    #endregion

                    #region Bind oil major Group
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        drpOilMajorsGroup.DataSource = ds.Tables[1];
                        drpOilMajorsGroup.DataTextField = "Group_Name";
                        drpOilMajorsGroup.DataValueField = "ID";
                        drpOilMajorsGroup.DataBind();
                    }
                    #endregion

                    #region Bind All Ranks
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        chklstRanks.DataSource = ds.Tables[2];
                        chklstRanks.DataTextField = "Rank";
                        chklstRanks.DataValueField = "ID";
                        chklstRanks.DataBind();

                        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                        {
                            ListItem item = chklstRanks.Items[i];
                            item.Attributes["rankId"] = ds.Tables[2].Rows[i]["ID"].ToString();
                        }
                    }
                    #endregion

                    #region Bind Oil Major Rules
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        drpOilMajorRules.DataSource = ds.Tables[3];
                        drpOilMajorRules.DataTextField = "Rule_Name";
                        drpOilMajorRules.DataValueField = "ID";
                        drpOilMajorRules.DataBind();

                    }
                    #endregion

                    #region Bind Rule Repeater
                    tblNoRecord.Visible = false;

                    if (ds.Tables.Count > 4)
                    {
                        if (ds.Tables[1].Rows.Count > 0 && ds.Tables[4].Rows.Count > 0)
                        {
                            // Copy to bind rule as per rule group
                            dt = ds.Tables[4];
                            rptOilMajorRuleGroup.DataSource = ds.Tables[1];
                            rptOilMajorRuleGroup.DataBind();
                        }
                        else
                        {
                            rptOilMajorRuleGroup.DataSource = null;
                            rptOilMajorRuleGroup.DataBind();
                            tblNoRecord.Visible = true;
                        }
                    }
                    #endregion

                    #region Bind Rules &  data

                    bindRules();
                    bindRuleData(null, null);
                    #endregion

                }

                txtRuleMappingValue.Text = string.Empty;
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            string RankIDs = "", OilMajorIDs = "", OilMajorValues = "";
            int Result = 0;

            foreach (RepeaterItem item in rptOilMajorsValue.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkOilMajors");
                if (chk.Checked)
                {
                    TextBox txtOilMajorsValue = (TextBox)item.FindControl("txtOilMajorsValue");
                    OilMajorIDs += chk.Attributes["rel"] + "|";
                    OilMajorValues += txtOilMajorsValue.Text + "|";
                }
            }


            string ValidateRanks = "";
            if (Convert.ToInt32(hdnRuleMappingID.Value) > 0)
                ValidateRanks = " (";

            ///Get Selected RankIDs
            foreach (ListItem lstRanks in chklstRanks.Items)
            {
                if (lstRanks.Selected)
                {
                    RankIDs += lstRanks.Value + "|";
                    if (Convert.ToInt32(hdnRuleMappingID.Value) > 0)
                        ValidateRanks += " Ranks LIKE '%|" + lstRanks.Value + "|%' OR";
                }
            }

            if (Convert.ToInt32(hdnRuleMappingID.Value) > 0)
            {
                ValidateRanks = ValidateRanks.Remove(ValidateRanks.Length - 2, 2);
                ValidateRanks += ")";
            }

            ///vaildate if Rule Already Exists
            #region Validate for Duplicate rule
            if (Convert.ToInt32(hdnRuleMappingID.Value) > 0)
            {
                string ValidateOilMajors = "|" + OilMajorIDs;

                objBLL = new BLL_Crew_Admin();
                DataSet dsValidate = objBLL.Bind_Rule_Mapping_Popup("RR", 0, Convert.ToInt32(hdnRuleMappingID.Value));

                bool RuleExists = false;
                if (ValidateOilMajors != "")
                {
                    string[] Array = ValidateOilMajors.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < Array.Length; i++)
                    {
                        objBLL = new BLL_Crew_Admin();
                        DataSet ds = objBLL.Bind_Rule_Mapping_Popup("ROM", Convert.ToInt32(Array[i]), 0);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                ds.Tables[0].DefaultView.RowFilter = "RuleGroupID=" + Convert.ToInt32(drpOilMajorsGroup.SelectedValue) + " AND OMRuleId = " + Convert.ToInt32(drpOilMajorRules.SelectedValue) + " AND " + ValidateRanks + " AND ID<>" + Convert.ToInt32(hdnRuleMappingID.Value);
                                if (ds.Tables[0].DefaultView.Count > 0)
                                {
                                    RuleExists = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (RuleExists)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RuleExists", "$(\"#divadd\").attr(\"title\", \"Edit Rule\");showModal('divadd', true);alert('Rule already exists for either of the selected ranks');", true);
                    return;
                }
            }
            #endregion



            objBLL = new BLL_Crew_Admin();
            Result = objBLL.InsertUpdateOilMajorRuleMapping(OilMajorIDs.TrimEnd('|'), RankIDs.TrimEnd('|'), Convert.ToInt32(drpOilMajorsGroup.SelectedValue), Convert.ToInt32(drpOilMajorRules.SelectedValue), Convert.ToInt32(hdnRuleMappingID.Value), "0", GetSessionUserID(), OilMajorValues.TrimEnd('|'));

            BindRuleMappingPopup();
            if (Result == -1)
            {
                if (Convert.ToInt32(hdnRuleMappingID.Value) == 0)
                {
                    drpOilMajorsGroup.Enabled = true;
                    drpOilMajorRules.Enabled = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RuleExists", "$(\"#divadd\").attr(\"title\", \"Add New Rule\");alert('Rule already exists for either of the selected ranks');hideModal('divadd');", true);
                }
                else
                {
                    drpOilMajorsGroup.Enabled = false;
                    drpOilMajorRules.Enabled = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "RuleExists", " $(\"#divadd\").attr(\"title\", \"Edit Rule\");alert('Rule already exists for either of the selected ranks');hideModal('divadd');", true);
                }
            }
            else
            {
                ClearControls();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowPopup", "alert('" + UDFLib.GetException("SuccessMessage/SaveMessage") + "');$('#hdnRuleMappingID').val('0');hideModal('divadd');", true);
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "alert('" + UDFLib.GetException("SystemError/GeneralMessage") + "');", true); }
    }

    /// <summary>
    /// Apply Filter 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void drpOilMajorsFilter_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int selcetedValue = drpOilMajorsFilter.SelectedIndex;
            //Filter Rule according to rule group
            BindRuleMappingPopup();
            drpOilMajorsFilter.SelectedIndex = selcetedValue;
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptOilMajorRuleGroup_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HtmlTableRow trGroupHeading = (HtmlTableRow)e.Item.FindControl("trGroupHeading");
                Repeater rptRules = (Repeater)e.Item.FindControl("rptRules");
                dt.DefaultView.RowFilter = "";
                dt.DefaultView.RowFilter = "RuleGroupId=" + Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "ID"));

                if (dt.DefaultView.Count > 0)
                {
                    rptRules.DataSource = dt.DefaultView;
                    rptRules.DataBind();
                }
                else
                {
                    trGroupHeading.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Bind Rules
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptRules_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            Label lblOilMajors = (Label)e.Item.FindControl("lblOilMajors");
            if (drpOilMajorsFilter.SelectedIndex > 0)
                lblOilMajors.Text = drpOilMajorsFilter.SelectedItem.Text;
            else
                lblOilMajors.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "OilMajorName"));

            HtmlTableRow trGroupHeading = (HtmlTableRow)e.Item.FindControl("trRules");
            if (e.Item.ItemType == ListItemType.Item)
                trGroupHeading.Attributes.Add("class", "RowStyle-css");
            if (e.Item.ItemType == ListItemType.AlternatingItem)
                trGroupHeading.Attributes.Add("class", "AlternatingRowStyle-css");
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Image imgRecordInfo = (Image)e.Item.FindControl("imgRecordInfo");
                if (drpOilMajorsFilter.SelectedIndex == 0)
                    imgRecordInfo.Attributes.Add("onclick", "Get_Record_Information('CRW_LIB_CM_RulesOilMajors','OMRM_ID=" + Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "ID")) + " AND OM_ID=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "OilMajorId")) + "',event,this);");
                else
                    imgRecordInfo.Attributes.Add("onclick", "Get_Record_Information('CRW_LIB_CM_RulesOilMajors','OMRM_ID=" + Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "ID")) + " AND OM_ID=" + drpOilMajorsFilter.SelectedValue + "',event,this);");
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Delete Rule
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void ImgDelete_onDelete(object source, CommandEventArgs e)
    {
        try
        {
            objBLL = new BLL_Crew_Admin();
            ImageButton Imgbtmdelete = (ImageButton)source;
            int Result = objBLL.DeleteOilMajorRuleMapping(Convert.ToInt32(e.CommandArgument.ToString()), GetSessionUserID(), Convert.ToInt32(Imgbtmdelete.Attributes["rel"]));
            if (Result > 0)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowMessage", "alert('" + UDFLib.GetException("SuccessMessage/DeleteMessage") + "')", true);

            BindRuleMappingPopup();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ImgDelete_onDelete", "alert('" + UDFLib.GetException("SystemError/GeneralMessage") + "');", true);
        }
    }

    protected void Edit_OnEdit(object sender, CommandEventArgs e)
    {
        try
        {
            ImageButton Img = (ImageButton)sender;
            int OilRulemappingId = Convert.ToInt32(e.CommandArgument.ToString());
            hdnRuleMappingID.Value = e.CommandArgument.ToString();

            DataSet ds = new DataSet();
            objBLL = new BLL_Crew_Admin();

            /// Get Oil Majors and ranks
            ds = objBLL.Bind_Rule_Mapping_Popup("RR", 0, OilRulemappingId);
            if (ds != null)
            {
                try
                {
                    drpOilMajorsGroup.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["RuleGroup_ID"]);
                    drpOilMajorsGroup.Enabled = false;

                    drpOilMajorRules.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OilMajorRule_ID"]);
                    drpOilMajorRules.Enabled = false;
                }
                catch (Exception)
                {
                    drpOilMajorsGroup.Enabled = false;
                    drpOilMajorRules.Enabled = false;
                }


                #region Check and Bind Oil Majors
                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        foreach (RepeaterItem item in rptOilMajorsValue.Items)
                        {
                            CheckBox chkOilMajors = (CheckBox)item.FindControl("chkOilMajors");
                            if (Convert.ToInt32(chkOilMajors.Attributes["rel"]) == Convert.ToInt32(ds.Tables[1].Rows[i]["OM_ID"]))
                            {
                                chkOilMajors.Checked = true;
                                TextBox txtOilMajorsValue = (TextBox)item.FindControl("txtOilMajorsValue");
                                txtOilMajorsValue.Text = Convert.ToString(ds.Tables[1].Rows[i]["Value"]);
                                break;
                            }
                        }
                    }
                }
                #endregion
                #region check and bind ranks
                if (ds.Tables[2].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        foreach (ListItem item in chklstRanks.Items)
                        {
                            if (Convert.ToInt32(item.Value) == Convert.ToInt32(ds.Tables[2].Rows[i]["RankID"]))
                            {
                                item.Selected = true;
                                break;
                            }
                        }
                    }
                }
                #endregion

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showEditPopup", "$('#divadd').attr('title', 'Edit Rule');showModal('divadd', true);", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void chkIsactive_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chkIsactive = (CheckBox)sender;
            GridViewRow gvr = (GridViewRow)chkIsactive.NamingContainer;
            Label hdnRId = (Label)gvr.FindControl("lblRuleId");
            Label hdnOId = (Label)gvr.FindControl("lblOID");
            int ParentId = Convert.ToInt32(chkIsactive.Attributes["rel"]);

            BLL_Crew_Admin objBLL_Crew_Admin = new BLL_Crew_Admin();
            if (objBLL_Crew_Admin.ActiveUnactiveAdditionalRule(Convert.ToInt32(hdnRId.Text), Convert.ToInt32(hdnOId.Text), chkIsactive.Checked, ParentId) > 0)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SaveMessage", "alert('" + UDFLib.GetException("SuccessMessage/SaveMessage") + "');", true);
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SaveFailedMessage", "alert('" + UDFLib.GetException("FailureMessage/DataSaveMessage") + "');", true);
        }
        catch (Exception ex)
        { UDFLib.WriteExceptionLog(ex); }
    }

    /// <summary>
    /// Bind Rules 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void bindRules()
    {
        try
        {
            DataSet dsAdditionalRule = objBLL.getAdditionalRule();
            if (dsAdditionalRule.Tables.Count > 0)
            {
                if (dsAdditionalRule.Tables[0].Rows.Count > 0)
                {

                    gridRules.DataSource = dsAdditionalRule.Tables[0];
                    gridRules.DataValueField = "Param";
                    gridRules.DataTextField = "Rule";
                    gridRules.DataBind();
                    gridRules.Items.Insert(0, new ListItem("-Select-", "0"));

                    ddlRulesList.DataSource = dsAdditionalRule.Tables[0];
                    ddlRulesList.DataValueField = "Param";
                    ddlRulesList.DataTextField = "Rule";
                    ddlRulesList.DataBind();
                    ddlRulesList.Items.Insert(0, new ListItem("-Select-", "0"));

                    Session["Dt_AdditionalRule"] = dsAdditionalRule.Tables[0];

                    //Bind Addotional Rules JSON to validate in javascript
                    JSONString = DataTableToJsonObj(dsAdditionalRule.Tables[0]);
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Get JSON from datatable
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public string DataTableToJsonObj(DataTable dt)
    {
        DataSet ds = new DataSet();
        ds.Merge(dt);
        StringBuilder JsonString = new StringBuilder();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            JsonString.Append("[");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                JsonString.Append("{");
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    if (j < ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                    }
                    else if (j == ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == ds.Tables[0].Rows.Count - 1)
                {
                    JsonString.Append("}");
                }
                else
                {
                    JsonString.Append("},");
                }
            }
            JsonString.Append("]");
            return JsonString.ToString();
        }
        else
        {
            return null;
        }
    }

    public void gridRules_OnselectionIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "showModal('divParam',true)", true);
            if (gridRules.SelectedIndex > 0)
            {
                btnSaveRules.Visible = true;
                string hdn = gridRules.SelectedValue.Split('_')[1]; ;
                ViewState["Id"] = gridRules.SelectedValue.Split('_')[0];
                ViewState["hdn"] = gridRules.SelectedValue.Split('_')[1];

                if (Session["Dt_AdditionalRule"] != null)
                {
                    DataTable Dt_AdditionalRule = (DataTable)Session["Dt_AdditionalRule"];
                    Dt_AdditionalRule.DefaultView.RowFilter = "ID=" + gridRules.SelectedValue.Split('_')[0];
                    BindAddtionalRules(Convert.ToString(Dt_AdditionalRule.DefaultView[0]["RuleName"]));
                }

            }
            else
            {
                HideAllAdditionalRulesConfig();
            }
        }
        catch (Exception ex)
        {

            UDFLib.WriteExceptionLog(ex);
        }
    }

    private void HideAllAdditionalRulesConfig()
    {
        trAddtionalRule1.Visible = false;
        trAddtionalRule2.Visible = false;
        trAddtionalRule3.Visible = false;
        trAddtionalRule4.Visible = false;
        trAddtionalRule5.Visible = false;
        trAddtionalRule6.Visible = false;
        trAddtionalRule7.Visible = false;
        trAddtionalRule8.Visible = false;
        trAddtionalRule9.Visible = false;
        trAddtionalRule10.Visible = false;
        trAddtionalRule11.Visible = false;
        trAddtionalRule12.Visible = false;
        trAddtionalRule13.Visible = false;
        trAddtionalRule14.Visible = false;
        trAddtionalRule15.Visible = false;
        trAddtionalRule16.Visible = false;
        trAddtionalRule17.Visible = false;
        trAddtionalRule18.Visible = false;
        trAddtionalRule19.Visible = false;
    }

    /// <summary>
    /// Bind Rank On Pop Up
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BindAddtionalRules(string RuleName)
    {
        try
        {
            ClearControls();
            HideAllAdditionalRulesConfig();

            switch (RuleName.ToLower())
            {
                case "additionalrule1":
                    BLL_Crew_Admin objCrewBLL = new BLL_Crew_Admin();
                    DataTable dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule1.Visible = true;

                    ddlAdditionalRule1Rank1.DataSource = dt;
                    ddlAdditionalRule1Rank1.DataTextField = "Rank_Name";
                    ddlAdditionalRule1Rank1.DataValueField = "RankID";
                    ddlAdditionalRule1Rank1.DataBind();
                    ddlAdditionalRule1Rank1.Items.Insert(0, new ListItem("-Select-", "0"));

                    ddlAdditionalRule1Rank2.DataSource = dt;
                    ddlAdditionalRule1Rank2.DataTextField = "Rank_Name";
                    ddlAdditionalRule1Rank2.DataValueField = "RankID";
                    ddlAdditionalRule1Rank2.DataBind();
                    ddlAdditionalRule1Rank2.Items.Insert(0, new ListItem("-Select-", "0"));
                    break;
                case "additionalrule2":
                    trAddtionalRule2.Visible = true;
                    txtAdditionalRule2.Text = string.Empty;
                    break;
                case "additionalrule3":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule3.Visible = true;
                    chkAdditionalRule3Ranks.DataSource = dt;
                    chkAdditionalRule3Ranks.DataTextField = "Rank_Name";
                    chkAdditionalRule3Ranks.DataValueField = "RankID";
                    chkAdditionalRule3Ranks.DataBind();
                    break;
                case "additionalrule4":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule4.Visible = true;
                    chkAdditionalRule4Ranks.DataSource = dt;
                    chkAdditionalRule4Ranks.DataTextField = "Rank_Name";
                    chkAdditionalRule4Ranks.DataValueField = "RankID";
                    chkAdditionalRule4Ranks.DataBind();

                    txtAdditionalRule4Years.Text = string.Empty;
                    break;
                case "additionalrule5":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule5.Visible = true;
                    chkAdditionalRule5Ranks.DataSource = dt;
                    chkAdditionalRule5Ranks.DataTextField = "Rank_Name";
                    chkAdditionalRule5Ranks.DataValueField = "RankID";
                    chkAdditionalRule5Ranks.DataBind();

                    txtAdditionalRule5Years.Text = string.Empty;
                    break;
                case "additionalrule6":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule6.Visible = true;
                    chkAdditionalRule6Ranks.DataSource = dt;
                    chkAdditionalRule6Ranks.DataTextField = "Rank_Name";
                    chkAdditionalRule6Ranks.DataValueField = "RankID";
                    chkAdditionalRule6Ranks.DataBind();

                    txtAdditionalRule6Years.Text = string.Empty;
                    break;
                case "additionalrule7":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule7.Visible = true;
                    ddlAdditionalRule7Rank1.DataSource = dt;
                    ddlAdditionalRule7Rank1.DataTextField = "Rank_Name";
                    ddlAdditionalRule7Rank1.DataValueField = "RankID";
                    ddlAdditionalRule7Rank1.DataBind();
                    ddlAdditionalRule7Rank1.Items.Insert(0, new ListItem("-Select-", "0"));

                    txtAdditionalRule7Year1.Text = string.Empty;

                    trAddtionalRule7.Visible = true;
                    ddlAdditionalRule7Rank2.DataSource = dt;
                    ddlAdditionalRule7Rank2.DataTextField = "Rank_Name";
                    ddlAdditionalRule7Rank2.DataValueField = "RankID";
                    ddlAdditionalRule7Rank2.DataBind();
                    ddlAdditionalRule7Rank2.Items.Insert(0, new ListItem("-Select-", "0"));

                    txtAdditionalRule7Year2.Text = string.Empty;
                    break;
                case "additionalrule8":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule8.Visible = true;
                    ddlAdditionalRule8Rank1.DataSource = dt;
                    ddlAdditionalRule8Rank1.DataTextField = "Rank_Name";
                    ddlAdditionalRule8Rank1.DataValueField = "RankID";
                    ddlAdditionalRule8Rank1.DataBind();
                    ddlAdditionalRule8Rank1.Items.Insert(0, new ListItem("-Select-", "0"));

                    txtAdditionalRule8Year1.Text = string.Empty;

                    trAddtionalRule8.Visible = true;
                    ddlAdditionalRule8Rank2.DataSource = dt;
                    ddlAdditionalRule8Rank2.DataTextField = "Rank_Name";
                    ddlAdditionalRule8Rank2.DataValueField = "RankID";
                    ddlAdditionalRule8Rank2.DataBind();
                    ddlAdditionalRule8Rank2.Items.Insert(0, new ListItem("-Select-", "0"));

                    txtAdditionalRule8Year2.Text = string.Empty;
                    break;
                case "additionalrule9":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule9.Visible = true;
                    chkAdditionalRule9Ranks.DataSource = dt;
                    chkAdditionalRule9Ranks.DataTextField = "Rank_Name";
                    chkAdditionalRule9Ranks.DataValueField = "RankID";
                    chkAdditionalRule9Ranks.DataBind();

                    txtAdditionalRule9Years.Text = string.Empty;
                    break;
                case "additionalrule10":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule10.Visible = true;
                    chkAdditionalRule10Ranks.DataSource = dt;
                    chkAdditionalRule10Ranks.DataTextField = "Rank_Name";
                    chkAdditionalRule10Ranks.DataValueField = "RankID";
                    chkAdditionalRule10Ranks.DataBind();

                    txtAdditionalRule10Years.Text = string.Empty;
                    break;
                case "additionalrule11":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule11.Visible = true;
                    chkAdditionalRule11Ranks.DataSource = dt;
                    chkAdditionalRule11Ranks.DataTextField = "Rank_Name";
                    chkAdditionalRule11Ranks.DataValueField = "RankID";
                    chkAdditionalRule11Ranks.DataBind();

                    txtAdditionalRule11Years.Text = string.Empty;
                    break;
                case "additionalrule12":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule12.Visible = true;
                    chkAdditionalRule12Ranks.DataSource = dt;
                    chkAdditionalRule12Ranks.DataTextField = "Rank_Name";
                    chkAdditionalRule12Ranks.DataValueField = "RankID";
                    chkAdditionalRule12Ranks.DataBind();

                    txtAdditionalRule12Years.Text = string.Empty;
                    break;
                case "additionalrule13":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule13.Visible = true;
                    chkAdditionalRule13Ranks.DataSource = dt;
                    chkAdditionalRule13Ranks.DataTextField = "Rank_Name";
                    chkAdditionalRule13Ranks.DataValueField = "RankID";
                    chkAdditionalRule13Ranks.DataBind();

                    txtAdditionalRule13Years.Text = string.Empty;
                    break;
                case "additionalrule14":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule14.Visible = true;
                    ddlAdditionalRule14Rank1.DataSource = dt;
                    ddlAdditionalRule14Rank1.DataTextField = "Rank_Name";
                    ddlAdditionalRule14Rank1.DataValueField = "RankID";
                    ddlAdditionalRule14Rank1.DataBind();
                    ddlAdditionalRule14Rank1.Items.Insert(0, new ListItem("-Select-", "0"));

                    ddlAdditionalRule14Rank2.DataSource = dt;
                    ddlAdditionalRule14Rank2.DataTextField = "Rank_Name";
                    ddlAdditionalRule14Rank2.DataValueField = "RankID";
                    ddlAdditionalRule14Rank2.DataBind();
                    ddlAdditionalRule14Rank2.Items.Insert(0, new ListItem("-Select-", "0"));
                    break;
                case "additionalrule15":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule15.Visible = true;
                    ddlAdditionalRule15Rank1.DataSource = dt;
                    ddlAdditionalRule15Rank1.DataTextField = "Rank_Name";
                    ddlAdditionalRule15Rank1.DataValueField = "RankID";
                    ddlAdditionalRule15Rank1.DataBind();
                    ddlAdditionalRule15Rank1.Items.Insert(0, new ListItem("-Select-", "0"));

                    ddlAdditionalRule15Rank2.DataSource = dt;
                    ddlAdditionalRule15Rank2.DataTextField = "Rank_Name";
                    ddlAdditionalRule15Rank2.DataValueField = "RankID";
                    ddlAdditionalRule15Rank2.DataBind();
                    ddlAdditionalRule15Rank2.Items.Insert(0, new ListItem("-Select-", "0"));
                    break;
                case "additionalrule16":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule16.Visible = true;

                    txtAdditionalRule16Days.Text = string.Empty;

                    ddlAdditionalRule16Rank1.DataSource = dt;
                    ddlAdditionalRule16Rank1.DataTextField = "Rank_Name";
                    ddlAdditionalRule16Rank1.DataValueField = "RankID";
                    ddlAdditionalRule16Rank1.DataBind();
                    ddlAdditionalRule16Rank1.Items.Insert(0, new ListItem("-Select-", "0"));

                    ddlAdditionalRule16Rank2.DataSource = dt;
                    ddlAdditionalRule16Rank2.DataTextField = "Rank_Name";
                    ddlAdditionalRule16Rank2.DataValueField = "RankID";
                    ddlAdditionalRule16Rank2.DataBind();
                    ddlAdditionalRule16Rank2.Items.Insert(0, new ListItem("-Select-", "0"));
                    break;
                case "additionalrule17":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule17.Visible = true;

                    txtAdditionalRule17Days.Text = string.Empty;

                    ddlAdditionalRule17Rank1.DataSource = dt;
                    ddlAdditionalRule17Rank1.DataTextField = "Rank_Name";
                    ddlAdditionalRule17Rank1.DataValueField = "RankID";
                    ddlAdditionalRule17Rank1.DataBind();
                    ddlAdditionalRule17Rank1.Items.Insert(0, new ListItem("-Select-", "0"));

                    ddlAdditionalRule17Rank2.DataSource = dt;
                    ddlAdditionalRule17Rank2.DataTextField = "Rank_Name";
                    ddlAdditionalRule17Rank2.DataValueField = "RankID";
                    ddlAdditionalRule17Rank2.DataBind();
                    ddlAdditionalRule17Rank2.Items.Insert(0, new ListItem("-Select-", "0"));
                    break;
                case "additionalrule18":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule18.Visible = true;

                    txtAdditionalRule18Days.Text = string.Empty;

                    ddlAdditionalRule18Rank1.DataSource = dt;
                    ddlAdditionalRule18Rank1.DataTextField = "Rank_Name";
                    ddlAdditionalRule18Rank1.DataValueField = "RankID";
                    ddlAdditionalRule18Rank1.DataBind();
                    ddlAdditionalRule18Rank1.Items.Insert(0, new ListItem("-Select-", "0"));

                    ddlAdditionalRule18Rank2.DataSource = dt;
                    ddlAdditionalRule18Rank2.DataTextField = "Rank_Name";
                    ddlAdditionalRule18Rank2.DataValueField = "RankID";
                    ddlAdditionalRule18Rank2.DataBind();
                    ddlAdditionalRule18Rank2.Items.Insert(0, new ListItem("-Select-", "0"));
                    break;
                case "additionalrule19":
                    objCrewBLL = new BLL_Crew_Admin();
                    dt = objCrewBLL.GetCrewMatrixRankByGroup().Tables[0];

                    trAddtionalRule19.Visible = true;

                    txtAdditionalRule19Days.Text = string.Empty;

                    ddlAdditionalRule19Rank1.DataSource = dt;
                    ddlAdditionalRule19Rank1.DataTextField = "Rank_Name";
                    ddlAdditionalRule19Rank1.DataValueField = "RankID";
                    ddlAdditionalRule19Rank1.DataBind();
                    ddlAdditionalRule19Rank1.Items.Insert(0, new ListItem("-Select-", "0"));

                    ddlAdditionalRule19Rank2.DataSource = dt;
                    ddlAdditionalRule19Rank2.DataTextField = "Rank_Name";
                    ddlAdditionalRule19Rank2.DataValueField = "RankID";
                    ddlAdditionalRule19Rank2.DataBind();
                    ddlAdditionalRule19Rank2.Items.Insert(0, new ListItem("-Select-", "0"));
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Clear All Additional Rule controls
    /// </summary>
    private void ClearControls()
    {
        #region Rule 1
        txtAdditionalRule1Days.Text = string.Empty;
        ddlAdditionalRule1Rank1.ClearSelection();
        ddlAdditionalRule1Rank2.ClearSelection();
        #endregion
        #region Rule 2
        txtAdditionalRule2.Text = string.Empty;
        #endregion
        #region Rule 3
        chkAdditionalRule3Ranks.ClearSelection();
        #endregion
        #region Rule 4
        chkAdditionalRule4Ranks.ClearSelection();
        txtAdditionalRule4Years.Text = string.Empty;
        #endregion
        #region Rule 5
        chkAdditionalRule5Ranks.ClearSelection();
        txtAdditionalRule5Years.Text = string.Empty;
        #endregion
        #region Rule 6
        chkAdditionalRule6Ranks.ClearSelection();
        txtAdditionalRule6Years.Text = string.Empty;
        #endregion
        #region Rule 7
        ddlAdditionalRule7Rank1.ClearSelection();
        txtAdditionalRule7Year1.Text = string.Empty;
        ddlAdditionalRule7Rank2.ClearSelection();
        txtAdditionalRule7Year2.Text = string.Empty;
        #endregion
        #region Rule 8
        ddlAdditionalRule8Rank1.ClearSelection();
        txtAdditionalRule8Year1.Text = string.Empty;
        ddlAdditionalRule8Rank2.ClearSelection();
        txtAdditionalRule8Year2.Text = string.Empty;
        #endregion
        #region Rule 9
        chkAdditionalRule9Ranks.ClearSelection();
        txtAdditionalRule9Years.Text = string.Empty;
        #endregion
        #region Rule 10
        chkAdditionalRule10Ranks.ClearSelection();
        txtAdditionalRule10Years.Text = string.Empty;
        #endregion
        #region Rule 11
        chkAdditionalRule11Ranks.ClearSelection();
        txtAdditionalRule11Years.Text = string.Empty;
        #endregion
        #region Rule 12
        chkAdditionalRule12Ranks.ClearSelection();
        txtAdditionalRule12Years.Text = string.Empty;
        #endregion
        #region Rule 13
        chkAdditionalRule13Ranks.ClearSelection();
        txtAdditionalRule13Years.Text = string.Empty;
        #endregion
        #region Rule 14
        ddlAdditionalRule14Rank1.ClearSelection();
        ddlAdditionalRule14Rank2.ClearSelection();
        #endregion
        #region Rule 15
        ddlAdditionalRule15Rank1.ClearSelection();
        ddlAdditionalRule15Rank2.ClearSelection();
        #endregion
        #region Rule 16
        txtAdditionalRule16Days.Text = string.Empty;
        ddlAdditionalRule16Rank1.ClearSelection();
        ddlAdditionalRule16Rank2.ClearSelection();
        #endregion
        #region Rule 17
        txtAdditionalRule17Days.Text = string.Empty;
        ddlAdditionalRule17Rank1.ClearSelection();
        ddlAdditionalRule17Rank2.ClearSelection();
        #endregion
        #region Rule 18
        txtAdditionalRule18Days.Text = string.Empty;
        ddlAdditionalRule18Rank1.ClearSelection();
        ddlAdditionalRule18Rank2.ClearSelection();
        #endregion
        #region Rule 19
        txtAdditionalRule19Days.Text = string.Empty;
        ddlAdditionalRule19Rank1.ClearSelection();
        ddlAdditionalRule19Rank2.ClearSelection();
        #endregion

    }

    public void bindOilMajors(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            ddlOilMajors.DataSource = dt;
            ddlOilMajors.DataTextField = "Oil_Major_Name";
            ddlOilMajors.DataValueField = "ID";
            ddlOilMajors.DataBind();
            ddlOilMajors.Items.Insert(0, new ListItem("-Select-", "0"));

            chkOilMajors.DataSource = dt;
            chkOilMajors.DataTextField = "Oil_Major_Name";
            chkOilMajors.DataValueField = "ID";
            chkOilMajors.DataBind();
        }
    }


    /// <summary>
    /// Insert Additional rules Mapping
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnsaveRules_Click(object sender, EventArgs e)
    {
        try
        {
            string AdditionalRule = "";
            int ParameterCount = 0;

            if (Session["Dt_AdditionalRule"] != null)
            {
                DataTable Dt_AdditionalRule = (DataTable)Session["Dt_AdditionalRule"];
                Dt_AdditionalRule.DefaultView.RowFilter = "ID=" + gridRules.SelectedValue.Split('_')[0];
                AdditionalRule = Convert.ToString(Dt_AdditionalRule.DefaultView[0]["RuleName"]);
                ParameterCount = UDFLib.ConvertToInteger(Dt_AdditionalRule.DefaultView[0]["Parameters"]);
            }

            int len = 0;
            string OilMajorName = "", N1 = "", N2 = "", N3 = "", N4 = "", Values = "";
            string AlertMessage = "";

            if (AdditionalRule == "AdditionalRule1")
            {
                N1 = txtAdditionalRule1Days.Text;
                N2 = ddlAdditionalRule1Rank1.SelectedValue;
                N3 = ddlAdditionalRule1Rank2.SelectedValue;
                Values = txtAdditionalRule1Days.Text + "," + ddlAdditionalRule1Rank1.SelectedValue + "," + ddlAdditionalRule1Rank2.SelectedValue;


                for (int i = 0; i < chkOilMajors.Items.Count; i++)
                {
                    if (chkOilMajors.Items[i].Selected)
                    {
                        DataTable dtt = getPivotData(null, null);
                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND ISNULL(N1,'')='" + N1 + "' AND ISNULL(N2,'')='" + N2 + "' AND ISNULL(N3,'')='" + N3 + "'";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }

                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND ISNULL(N1,'')='" + N1 + "' AND ISNULL(N2,'')='" + N3 + "' AND ISNULL(N3,'')='" + N2 + "'";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }
                    }
                }
            }
            else if (AdditionalRule == "AdditionalRule2")
            {
                N1 = txtAdditionalRule2.Text;
                Values = txtAdditionalRule2.Text;

                for (int i = 0; i < chkOilMajors.Items.Count; i++)
                {
                    if (chkOilMajors.Items[i].Selected)
                    {
                        DataTable dtt = getPivotData(null, null);
                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND CONVERT(N1,'System.Decimal')=" + Convert.ToDecimal(N1) + "";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }
                    }
                }
            }
            else if (AdditionalRule == "AdditionalRule3")
            {
                for (int i = 0; i < chkAdditionalRule3Ranks.Items.Count; i++)
                {
                    if (chkAdditionalRule3Ranks.Items[i].Selected)
                        Values += chkAdditionalRule3Ranks.Items[i].Value + "|";
                }

                N1 = Values.TrimEnd('|');
                Values = Values.TrimEnd('|');

                for (int i = 0; i < chkOilMajors.Items.Count; i++)
                {
                    if (chkOilMajors.Items[i].Selected)
                    {
                        DataTable dtt = getPivotData(null, null);
                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "'";
                        if (dtt.DefaultView.Count > 0)
                        {
                            bool IsBreak = false;

                            for (int k = 0; k < Convert.ToString(dtt.DefaultView[0]["N1"]).Split('|').Length; k++)
                            {
                                if (IsBreak)
                                    break;
                                for (int j = 0; j < N1.Split('|').Length; j++)
                                {
                                    if (Convert.ToInt32(N1.Split('|')[j]) == Convert.ToInt32(Convert.ToString(dtt.DefaultView[0]["N1"]).Split('|')[k]))
                                    {
                                        OilMajorName = chkOilMajors.Items[i].Text.ToString();
                                        len++;
                                        IsBreak = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else if (AdditionalRule == "AdditionalRule4")
            {
                for (int i = 0; i < chkAdditionalRule4Ranks.Items.Count; i++)
                {
                    if (chkAdditionalRule4Ranks.Items[i].Selected)
                        Values += chkAdditionalRule4Ranks.Items[i].Value + "|";
                }

                N1 = Values.TrimEnd('|');
                N2 = txtAdditionalRule4Years.Text;

                Values = Values.TrimEnd('|') + ",";
                Values += txtAdditionalRule4Years.Text;


                for (int i = 0; i < chkOilMajors.Items.Count; i++)
                {
                    if (chkOilMajors.Items[i].Selected)
                    {
                        DataTable dtt = getPivotData(null, null);
                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND N1='" + N1 + "' AND CONVERT(N2,'System.Decimal')=" + N2 + "";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }
                    }
                }
            }
            else if (AdditionalRule == "AdditionalRule5")
            {
                for (int i = 0; i < chkAdditionalRule5Ranks.Items.Count; i++)
                {
                    if (chkAdditionalRule5Ranks.Items[i].Selected)
                        Values += chkAdditionalRule5Ranks.Items[i].Value + "|";
                }

                N1 = Values.TrimEnd('|');
                N2 = txtAdditionalRule5Years.Text;

                Values = Values.TrimEnd('|') + ",";
                Values += txtAdditionalRule5Years.Text;

                for (int i = 0; i < chkOilMajors.Items.Count; i++)
                {
                    if (chkOilMajors.Items[i].Selected)
                    {
                        DataTable dtt = getPivotData(null, null);
                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND N1='" + N1 + "' AND CONVERT(N2,'System.Decimal')=" + N2 + "";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }
                    }
                }
            }
            else if (AdditionalRule == "AdditionalRule6")
            {
                for (int i = 0; i < chkAdditionalRule6Ranks.Items.Count; i++)
                {
                    if (chkAdditionalRule6Ranks.Items[i].Selected)
                        Values += chkAdditionalRule6Ranks.Items[i].Value + "|";
                }

                N1 = Values.TrimEnd('|');
                N2 = txtAdditionalRule6Years.Text;

                Values = Values.TrimEnd('|') + ",";
                Values += txtAdditionalRule6Years.Text;

                for (int i = 0; i < chkOilMajors.Items.Count; i++)
                {
                    if (chkOilMajors.Items[i].Selected)
                    {
                        DataTable dtt = getPivotData(null, null);
                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND N1='" + N1 + "' AND CONVERT(N2,'System.Decimal')=" + N2 + "";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }
                    }
                }
            }
            else if (AdditionalRule == "AdditionalRule7")
            {
                N1 = ddlAdditionalRule7Rank1.SelectedValue;
                N2 = txtAdditionalRule7Year1.Text;
                N3 = ddlAdditionalRule7Rank2.SelectedValue;
                N4 = txtAdditionalRule7Year2.Text;

                Values += ddlAdditionalRule7Rank1.SelectedValue + "," + txtAdditionalRule7Year1.Text + ",";
                Values += ddlAdditionalRule7Rank2.SelectedValue + "," + txtAdditionalRule7Year2.Text;

                for (int i = 0; i < chkOilMajors.Items.Count; i++)
                {
                    if (chkOilMajors.Items[i].Selected)
                    {
                        DataTable dtt = getPivotData(null, null);
                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND N1='" + N1 + "' AND CONVERT(N2,'System.Decimal')=" + N2 + " AND N3='" + N3 + "' AND  CONVERT(N4,'System.Decimal')=" + N4 + "";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }
                    }
                }
            }
            else if (AdditionalRule == "AdditionalRule8")
            {
                Values += ddlAdditionalRule8Rank1.SelectedValue + "," + txtAdditionalRule8Year1.Text + ",";
                Values += ddlAdditionalRule8Rank2.SelectedValue + "," + txtAdditionalRule8Year2.Text;
            }
            else if (AdditionalRule == "AdditionalRule9")
            {
                for (int i = 0; i < chkAdditionalRule9Ranks.Items.Count; i++)
                {
                    if (chkAdditionalRule9Ranks.Items[i].Selected)
                        Values += chkAdditionalRule9Ranks.Items[i].Value + "|";
                }

                N1 = Values.TrimEnd('|');
                N2 = txtAdditionalRule9Years.Text;

                Values = Values.TrimEnd('|') + ",";
                Values += txtAdditionalRule9Years.Text;

                for (int i = 0; i < chkOilMajors.Items.Count; i++)
                {
                    if (chkOilMajors.Items[i].Selected)
                    {
                        DataTable dtt = getPivotData(null, null);
                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND N1='" + N1 + "' AND CONVERT(N2,'System.Decimal')=" + N2 + "";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }
                    }
                }
            }
            else if (AdditionalRule == "AdditionalRule10")
            {
                for (int i = 0; i < chkAdditionalRule10Ranks.Items.Count; i++)
                {
                    if (chkAdditionalRule10Ranks.Items[i].Selected)
                        Values += chkAdditionalRule10Ranks.Items[i].Value + "|";
                }

                N1 = Values.TrimEnd('|');
                N2 = txtAdditionalRule10Years.Text;

                Values = Values.TrimEnd('|') + ",";
                Values += txtAdditionalRule10Years.Text;

                for (int i = 0; i < chkOilMajors.Items.Count; i++)
                {
                    if (chkOilMajors.Items[i].Selected)
                    {
                        DataTable dtt = getPivotData(null, null);
                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND N1='" + N1 + "' AND CONVERT(N2,'System.Decimal')=" + N2 + "";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }
                    }
                }
            }
            else if (AdditionalRule == "AdditionalRule11")
            {
                for (int i = 0; i < chkAdditionalRule11Ranks.Items.Count; i++)
                {
                    if (chkAdditionalRule11Ranks.Items[i].Selected)
                        Values += chkAdditionalRule11Ranks.Items[i].Value + "|";
                }
                N1 = Values.TrimEnd('|');
                N2 = txtAdditionalRule11Years.Text;

                Values = Values.TrimEnd('|') + ",";
                Values += txtAdditionalRule11Years.Text;

                for (int i = 0; i < chkOilMajors.Items.Count; i++)
                {
                    if (chkOilMajors.Items[i].Selected)
                    {
                        DataTable dtt = getPivotData(null, null);
                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND N1='" + N1 + "' AND CONVERT(N2,'System.Decimal')=" + N2 + "";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }
                    }
                }
            }
            else if (AdditionalRule == "AdditionalRule12")
            {
                for (int i = 0; i < chkAdditionalRule12Ranks.Items.Count; i++)
                {
                    if (chkAdditionalRule12Ranks.Items[i].Selected)
                        Values += chkAdditionalRule12Ranks.Items[i].Value + "|";
                }

                N1 = Values.TrimEnd('|');
                N2 = txtAdditionalRule12Years.Text;

                Values = Values.TrimEnd('|') + ",";
                Values += txtAdditionalRule12Years.Text;

                for (int i = 0; i < chkOilMajors.Items.Count; i++)
                {
                    if (chkOilMajors.Items[i].Selected)
                    {
                        DataTable dtt = getPivotData(null, null);
                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND N1='" + N1 + "' AND CONVERT(N2,'System.Decimal')=" + N2 + "";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }
                    }
                }
            }
            else if (AdditionalRule == "AdditionalRule13")
            {
                for (int i = 0; i < chkAdditionalRule13Ranks.Items.Count; i++)
                {
                    if (chkAdditionalRule13Ranks.Items[i].Selected)
                        Values += chkAdditionalRule13Ranks.Items[i].Value + "|";
                }

                N1 = Values.TrimEnd('|');
                N2 = txtAdditionalRule13Years.Text;

                Values = Values.TrimEnd('|') + ",";
                Values += txtAdditionalRule13Years.Text;

                for (int i = 0; i < chkOilMajors.Items.Count; i++)
                {
                    if (chkOilMajors.Items[i].Selected)
                    {
                        DataTable dtt = getPivotData(null, null);
                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND N1='" + N1 + "' AND CONVERT(N2,'System.Decimal')=" + N2 + "";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }
                    }
                }
            }
            else if (AdditionalRule == "AdditionalRule14")
            {
                N1 = ddlAdditionalRule14Rank1.SelectedValue;
                N2 = ddlAdditionalRule14Rank2.SelectedValue;

                Values = ddlAdditionalRule14Rank1.SelectedValue + "," + ddlAdditionalRule14Rank2.SelectedValue;

                for (int i = 0; i < chkOilMajors.Items.Count; i++)
                {
                    if (chkOilMajors.Items[i].Selected)
                    {
                        DataTable dtt = getPivotData(null, null);
                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND ISNULL(N1,'')='" + N1 + "' AND ISNULL(N2,'')='" + N2 + "'";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }

                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND ISNULL(N1,'')='" + N2 + "' AND ISNULL(N2,'')='" + N1 + "'";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }
                    }
                }
            }
            else if (AdditionalRule == "AdditionalRule15")
            {
                N1 = ddlAdditionalRule15Rank1.SelectedValue;
                N2 = ddlAdditionalRule15Rank2.SelectedValue;

                Values = ddlAdditionalRule15Rank1.SelectedValue + "," + ddlAdditionalRule15Rank2.SelectedValue;

                for (int i = 0; i < chkOilMajors.Items.Count; i++)
                {
                    if (chkOilMajors.Items[i].Selected)
                    {
                        DataTable dtt = getPivotData(null, null);
                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND ISNULL(N1,'')='" + N1 + "' AND ISNULL(N2,'')='" + N2 + "' ";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }

                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND ISNULL(N1,'')='" + N2 + "' AND ISNULL(N2,'')='" + N1 + "'";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }
                    }
                }
            }
            else if (AdditionalRule == "AdditionalRule16")
            {
                N1 = txtAdditionalRule16Days.Text;
                N2 = ddlAdditionalRule16Rank1.SelectedValue;
                N3 = ddlAdditionalRule16Rank2.SelectedValue;

                Values = txtAdditionalRule16Days.Text + "," + ddlAdditionalRule16Rank1.SelectedValue + "," + ddlAdditionalRule16Rank2.SelectedValue;

                for (int i = 0; i < chkOilMajors.Items.Count; i++)
                {
                    if (chkOilMajors.Items[i].Selected)
                    {
                        DataTable dtt = getPivotData(null, null);
                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND ISNULL(N1,'')='" + N1 + "' AND ISNULL(N2,'')='" + N2 + "' AND ISNULL(N3,'')='" + N3 + "'";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }

                        dtt.DefaultView.RowFilter = "";
                        dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND ISNULL(N1,'')='" + N1 + "' AND ISNULL(N2,'')='" + N3 + "' AND ISNULL(N3,'')='" + N2 + "' ";
                        if (dtt.DefaultView.Count > 0)
                        {
                            OilMajorName = chkOilMajors.Items[i].Text.ToString();
                            len++;
                            break;
                        }
                    }
                }
            }
            else if (AdditionalRule == "AdditionalRule17")
            {
                Values = txtAdditionalRule17Days.Text + "," + ddlAdditionalRule17Rank1.SelectedValue + "," + ddlAdditionalRule17Rank2.SelectedValue;
            }
            else if (AdditionalRule == "AdditionalRule18")
            {
                Values = txtAdditionalRule18Days.Text + "," + ddlAdditionalRule18Rank1.SelectedValue + "," + ddlAdditionalRule18Rank2.SelectedValue;
            }
            else if (AdditionalRule == "AdditionalRule19")
            {
                Values = txtAdditionalRule19Days.Text + "," + ddlAdditionalRule19Rank1.SelectedValue + "," + ddlAdditionalRule19Rank2.SelectedValue;
            }


            for (int i = 0; i < chkOilMajors.Items.Count; i++)
            {
                if (chkOilMajors.Items[i].Selected)
                {
                    DataTable dtt = getPivotData(null, null);
                    dtt.DefaultView.RowFilter = "";
                    dtt.DefaultView.RowFilter = "OID='" + UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()) + "' AND ParentId<>'" + Convert.ToInt32(hdnParentID.Value) + "' AND RuleName='" + AdditionalRule + "' AND ISNULL(N1,'')='" + N1 + "' AND ISNULL(N2,'')='" + N2 + "' AND ISNULL(N3,'')='" + N3 + "' AND ISNULL(N4,'')='" + N4 + "' ";
                    if (dtt.DefaultView.Count > 0)
                    {
                        OilMajorName = chkOilMajors.Items[i].Text.ToString();
                        len++;
                        break;
                    }
                }
            }

            if (len == 0)
            {
                #region Save Additional rules value
                int Result = 0;
                int ParentID = 0;
                if (Values != "")
                {
                    for (int i = 0; i < chkOilMajors.Items.Count; i++)
                    {
                        if (chkOilMajors.Items[i].Selected)
                        {
                            ParentID = 0;
                            BLL_Crew_Admin objAdmin = new BLL_Crew_Admin();
                            Result++;
                            for (int j = 0; j < ParameterCount; j++)
                                objAdmin.InsertCRWAddtional_Rule_Mapping(Convert.ToInt32(hdnParentID.Value), UDFLib.ConvertToInteger(ViewState["Id"].ToString()), UDFLib.ConvertToInteger(chkOilMajors.Items[i].Value.ToString()), "N" + (j + 1), Values.Split(',')[j], GetSessionUserID(), Convert.ToInt32(chkActive.Checked), ref ParentID);

                            hdnParentID.Value = "0";
                        }
                    }
                }
                #endregion

                if (Result > 0)
                {
                    btnSearch_click(null, null);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", "alert('" + UDFLib.GetException("SuccessMessage/SaveMessage") + "');hideModal('divParam');", true);
                    ViewState["Edit"] = null;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertq1", "alert('Rule with same parameters already exists for " + OilMajorName + "');showModal('divParam',true)", true);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Bind Rules with Oil Major
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    public DataTable getPivotData(int? RuleId, int? OilMajorId)
    {
        DataTable dt = new DataTable();
        try
        {
            BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
            DataSet ds = objCrewAdmin.GetAddtionalRuleMapping(RuleId, OilMajorId);
            string[] Pkey_cols = new string[] { "RuleIdParentId", "Oil_Major_Name" };
            string[] Hide_cols = new string[] { };
            dt = PivotTable("Key", "Value", "Rule", Pkey_cols, Hide_cols, ds.Tables[0]);
            DataColumnCollection columns = dt.Columns;
            if (!columns.Contains("N1"))
            {
                dt.Columns.Add("N1");
            }
            if (!columns.Contains("N2"))
            {
                dt.Columns.Add("N2");
            }
            if (!columns.Contains("N3"))
            {
                dt.Columns.Add("N3");
            }
            if (!columns.Contains("N4"))
            {
                dt.Columns.Add("N4");
            }

        }
        catch (Exception ex)
        {
            dt = null;
            UDFLib.WriteExceptionLog(ex);
        }
        return dt;
    }
    public void bindRuleData(int? RuleId, int? OilMajorId)
    {
        try
        {
            gridOilMajorAssign.DataSource = getPivotData(RuleId, OilMajorId);
            gridOilMajorAssign.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected string BindRuleText(string Rule)
    {
        return Rule.Replace("N1", "<b>N1</b>").Replace("N2", "<b>N2</b>").Replace("N3", "<b>N3</b>").Replace("N4", "<b>N4</b>");
    }

    public void lblEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Edit"] = "Edit";
            // bindRuleData(null, null);
            string js1 = "showModal('divParam',true)";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", js1, true);
            ImageButton btn = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            Label hdnRId = (Label)gvr.FindControl("lblRuleId");
            int ParentID = 0;
            ParentID = Convert.ToInt32(btn.Attributes["rel"]);
            hdnParentID.Value = Convert.ToString(ParentID);
            Label hdnOId = (Label)gvr.FindControl("lblOID");

            ViewState["Id"] = hdnRId.Text;

            BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
            DataSet ds = objCrewAdmin.GetAddtionalRuleMapping(Convert.ToInt32(hdnRId.Text), Convert.ToInt32(hdnOId.Text));
            if (ParentID > 0)
                ds.Tables[0].DefaultView.RowFilter = "ParentId=" + ParentID;

            if (ds.Tables[0].DefaultView.Count > 0)
            {
                string AdditionalRule = Convert.ToString(ds.Tables[0].DefaultView[0]["RuleName"]);
                /// Bind AdditionalRules format 
                BindAddtionalRules(AdditionalRule);

                chkActive.Checked = Convert.ToBoolean(ds.Tables[0].DefaultView[0]["IsActive"]);
                chkOilMajors.ClearSelection();
                chkOilMajors.Enabled = false;
                chkOilMajors.Items.FindByValue(hdnOId.Text).Selected = true;

                gridRules.ClearSelection();
                gridRules.Enabled = false;
                gridRules.Items.FindByText(Convert.ToString(ds.Tables[0].DefaultView[0]["Rule"])).Selected = true;

                if (AdditionalRule == "AdditionalRule1")
                {
                    txtAdditionalRule1Days.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]);
                    ddlAdditionalRule1Rank1.SelectedValue = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6]);
                    ddlAdditionalRule1Rank2.SelectedValue = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N3'")[0].ItemArray[6]);
                }
                else if (AdditionalRule == "AdditionalRule2")
                {
                    txtAdditionalRule2.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]);
                }
                else if (AdditionalRule == "AdditionalRule3")
                {
                    for (int i = 0; i < Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|').Length; i++)
                        chkAdditionalRule3Ranks.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|')[i]).Selected = true;
                }
                else if (AdditionalRule == "AdditionalRule4")
                {
                    for (int i = 0; i < Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|').Length; i++)
                        chkAdditionalRule4Ranks.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|')[i]).Selected = true;

                    txtAdditionalRule4Years.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6]);
                }
                else if (AdditionalRule == "AdditionalRule5")
                {
                    for (int i = 0; i < Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|').Length; i++)
                        chkAdditionalRule5Ranks.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|')[i]).Selected = true;

                    txtAdditionalRule5Years.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6]);
                }
                else if (AdditionalRule == "AdditionalRule6")
                {
                    for (int i = 0; i < Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|').Length; i++)
                        chkAdditionalRule6Ranks.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|')[i]).Selected = true;

                    txtAdditionalRule6Years.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6]);
                }
                else if (AdditionalRule == "AdditionalRule7")
                {
                    ddlAdditionalRule7Rank1.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6])).Selected = true;
                    txtAdditionalRule7Year1.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6]);

                    ddlAdditionalRule7Rank2.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N3'")[0].ItemArray[6])).Selected = true;
                    txtAdditionalRule7Year2.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N4'")[0].ItemArray[6]);
                }
                else if (AdditionalRule == "AdditionalRule8")
                {
                    ddlAdditionalRule8Rank1.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6])).Selected = true;
                    txtAdditionalRule8Year1.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6]);

                    ddlAdditionalRule8Rank2.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N3'")[0].ItemArray[6])).Selected = true;
                    txtAdditionalRule8Year2.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N4'")[0].ItemArray[6]);
                }
                else if (AdditionalRule == "AdditionalRule9")
                {
                    for (int i = 0; i < Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|').Length; i++)
                        chkAdditionalRule9Ranks.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|')[i]).Selected = true;

                    txtAdditionalRule9Years.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6]);
                }
                else if (AdditionalRule == "AdditionalRule10")
                {
                    for (int i = 0; i < Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|').Length; i++)
                        chkAdditionalRule10Ranks.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|')[i]).Selected = true;

                    txtAdditionalRule10Years.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6]);
                }
                else if (AdditionalRule == "AdditionalRule11")
                {
                    for (int i = 0; i < Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|').Length; i++)
                        chkAdditionalRule11Ranks.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|')[i]).Selected = true;

                    txtAdditionalRule11Years.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6]);
                }
                else if (AdditionalRule == "AdditionalRule12")
                {
                    for (int i = 0; i < Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|').Length; i++)
                        chkAdditionalRule12Ranks.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|')[i]).Selected = true;

                    txtAdditionalRule12Years.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6]);
                }
                else if (AdditionalRule == "AdditionalRule13")
                {
                    for (int i = 0; i < Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|').Length; i++)
                        chkAdditionalRule13Ranks.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]).Split('|')[i]).Selected = true;

                    txtAdditionalRule13Years.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6]);
                }
                else if (AdditionalRule == "AdditionalRule14")
                {
                    ddlAdditionalRule14Rank1.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6])).Selected = true;
                    ddlAdditionalRule14Rank2.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6])).Selected = true;
                }
                else if (AdditionalRule == "AdditionalRule15")
                {
                    ddlAdditionalRule15Rank1.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6])).Selected = true;
                    ddlAdditionalRule15Rank2.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6])).Selected = true;
                }
                else if (AdditionalRule == "AdditionalRule16")
                {
                    txtAdditionalRule16Days.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]);
                    ddlAdditionalRule16Rank1.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6])).Selected = true;
                    ddlAdditionalRule16Rank2.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N3'")[0].ItemArray[6])).Selected = true;
                }
                else if (AdditionalRule == "AdditionalRule17")
                {
                    txtAdditionalRule17Days.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]);
                    ddlAdditionalRule17Rank1.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6])).Selected = true;
                    ddlAdditionalRule17Rank2.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N3'")[0].ItemArray[6])).Selected = true;
                }
                else if (AdditionalRule == "AdditionalRule18")
                {
                    txtAdditionalRule18Days.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]);
                    ddlAdditionalRule18Rank1.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6])).Selected = true;
                    ddlAdditionalRule18Rank2.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N3'")[0].ItemArray[6])).Selected = true;
                }
                else if (AdditionalRule == "AdditionalRule19")
                {
                    txtAdditionalRule19Days.Text = Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N1'")[0].ItemArray[6]);
                    ddlAdditionalRule19Rank1.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N2'")[0].ItemArray[6])).Selected = true;
                    ddlAdditionalRule19Rank2.Items.FindByValue(Convert.ToString(ds.Tables[0].DefaultView.ToTable().Select("Key='N3'")[0].ItemArray[6])).Selected = true;
                }

            }
            updivAddvewRule.Update();
            btnSaveRules.Visible = true;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    public void btnCreateAssign_click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Edit"] = null;
            hdnParentID.Value = "0";
            chkActive.Checked = true;
            gridRules.Enabled = true;
            gridRules.ClearSelection();
            gridRules_OnselectionIndexChanged(sender, e);
            chkOilMajors.Enabled = true;
            chkOilMajors.ClearSelection();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAssignPopup", "$('#divParam').attr(\"title\", \"Assign/Unassign Additional Rule\");showModal('divParam',true);", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }


    /// <summary>
    /// Search Rule Mapping along with Oil majors
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnSearch_click(object sender, EventArgs e)
    {
        string oilMajor = null;
        string rule = null;
        if (ddlOilMajors.SelectedIndex != 0)
        {

            oilMajor = ddlOilMajors.SelectedValue;
        }

        if (ddlRulesList.SelectedIndex != 0)
        {
            rule = ddlRulesList.SelectedValue.Split('_')[0];

        }
        bindRuleData(UDFLib.ConvertIntegerToNull(rule), UDFLib.ConvertIntegerToNull(oilMajor));
    }
    public void lblDelete_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        Label hdnOId = (Label)gvr.FindControl("lblOID");
        Label hdnRId = (Label)gvr.FindControl("lblRuleId");
        int ParentId = Convert.ToInt32(btn.Attributes["rel"]);
        BLL_Crew_Admin objCrewBLL = new BLL_Crew_Admin();
        int i = objCrewBLL.DeleteAdditionalRuleMapping(UDFLib.ConvertToInteger(hdnRId.Text), UDFLib.ConvertToInteger(hdnOId.Text), ParentId);
        if (i > 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ShowMessage", "alert('Record Deleted Successfully')", true);
        }
        btnSearch_click(sender, e);
    }

    public string ProcessMyDataItem(object myValue)
    {
        if (myValue == null)
        {
            return "0";
        }

        return myValue.ToString();
    }

    /// <summary>
    ///Grid View Pagination
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gridOilMajorAssign_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridOilMajorAssign.PageIndex = e.NewPageIndex;
        string oilMajor = null;
        string rule = null;
        if (ddlOilMajors.SelectedIndex != 0)
        {

            oilMajor = ddlOilMajors.SelectedValue;
        }

        if (ddlRulesList.SelectedIndex != 0)
        {
            rule = ddlRulesList.SelectedValue.Split('_')[0];

        }
        bindRuleData(UDFLib.ConvertIntegerToNull(rule), UDFLib.ConvertIntegerToNull(oilMajor));
        // gridOilMajorAssign.DataBind();
    }

    #region PivotTable

    public static DataTable PivotTable(string PivotColumnName, string PivotValueColumnName, string PivotColumnOrder, string[] PrimaryKeyColumns, string[] HideColumns, DataTable dtTableToPivot)
    {
        DataTable dtFinalResult = new DataTable();
        try
        {
            StringBuilder sbPKs = new StringBuilder();
            DataView dvPivotColumnNames = dtTableToPivot.DefaultView.ToTable(true, new string[] { PivotColumnName, PivotColumnOrder }).DefaultView;
            dvPivotColumnNames.Sort = PivotColumnOrder;

            DataTable dtPivotPrimaryKeys = dtTableToPivot.DefaultView.ToTable(true, PrimaryKeyColumns);


            foreach (DataColumn dcol in dtTableToPivot.Columns)
            {
                if (dcol.ColumnName != PivotColumnName && dcol.ColumnName != PivotValueColumnName && dcol.ColumnName != PivotColumnOrder)
                    dtFinalResult.Columns.Add(dcol.ColumnName);
            }
            string a = string.Empty;
            foreach (DataRow drCol in dvPivotColumnNames.ToTable().Rows)
            {

                if (!a.Contains(drCol[0].ToString()))
                {
                    dtFinalResult.Columns.Add(drCol[0].ToString());
                    a += drCol[0].ToString();
                }
            }


            foreach (DataRow drPK in dtPivotPrimaryKeys.Rows)
            {
                DataRow drNew = dtFinalResult.NewRow();

                foreach (DataColumn dcol in dtTableToPivot.Columns)
                {
                    if (dcol.ColumnName != PivotColumnName && dcol.ColumnName != PivotValueColumnName && dcol.ColumnName != PivotColumnOrder)
                    {
                        sbPKs.Clear();
                        foreach (string pk in PrimaryKeyColumns)
                        {
                            sbPKs.Append(pk + " = " + "'" + drPK[pk].ToString() + "'" + " and ");

                        }
                        sbPKs.Append(" 1=1  ");

                        DataRow[] drcoll = dtTableToPivot.Select(sbPKs.ToString());//[0][dcol.ColumnName];
                        drNew[dcol.ColumnName] = drcoll[0][dcol.ColumnName];
                    }
                }

                foreach (DataRow drCol in dvPivotColumnNames.ToTable().Rows)
                {
                    sbPKs.Clear();
                    foreach (string pk in PrimaryKeyColumns)
                    {
                        sbPKs.Append(pk + " = " + "'" + drPK[pk].ToString() + "'" + " and ");
                    }


                    DataRow[] drValue = dtTableToPivot.Select(sbPKs.ToString() + PivotColumnName + " = '" + drCol[0].ToString() + "' ");
                    if (drValue.Length > 0)
                        drNew[drCol[0].ToString()] = drValue[0][PivotValueColumnName];
                    else
                        drNew[drCol[0].ToString()] = null;
                }

                dtFinalResult.Rows.Add(drNew);
            }


            foreach (string strColToremove in HideColumns)
            {
                if (dtFinalResult.Columns.IndexOf(strColToremove) > -1)
                    dtFinalResult.Columns.Remove(strColToremove);
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        return dtFinalResult;
    }

    #endregion

    public void boldOilMaJor()
    {
        for (int x = 0; x < chkOilMajors.Items.Count; x++)
        {

            if (chkOilMajors.Items[x].Selected == true)
                chkOilMajors.Items[x].Attributes.Add("style", "font-weight:bold");

        }
    }
    protected void gridOilMajorAssign_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnRuleName = (HiddenField)e.Row.FindControl("hdnRuleName");
                if (hdnRuleName.Value.ToLower() == "additionalrule1")
                {
                    Label N2 = (Label)e.Row.FindControl("N2");
                    Label N3 = (Label)e.Row.FindControl("N3");

                    N2.Text = dtRanks.Select("ID='" + N2.Text + "'")[0].ItemArray[1].ToString();
                    N3.Text = dtRanks.Select("ID='" + N3.Text + "'")[0].ItemArray[1].ToString();
                }
                else if (hdnRuleName.Value.ToLower() == "additionalrule4" || hdnRuleName.Value.ToLower() == "additionalrule5" || hdnRuleName.Value.ToLower() == "additionalrule6" || hdnRuleName.Value.ToLower() == "additionalrule3")
                {
                    Label N1 = (Label)e.Row.FindControl("N1");
                    string ranks = "";
                    for (int i = 0; i < N1.Text.Split('|').Length; i++)
                        ranks += dtRanks.Select("ID='" + N1.Text.Split('|')[i] + "'")[0].ItemArray[1].ToString() + ", ";
                    N1.Text = ranks.Trim().TrimEnd(',');
                }
                else if (hdnRuleName.Value.ToLower() == "additionalrule7" || hdnRuleName.Value.ToLower() == "additionalrule8")
                {
                    Label N1 = (Label)e.Row.FindControl("N1");
                    Label N3 = (Label)e.Row.FindControl("N3");
                    N1.Text = dtRanks.Select("ID='" + N1.Text + "'")[0].ItemArray[1].ToString();
                    N3.Text = dtRanks.Select("ID='" + N3.Text + "'")[0].ItemArray[1].ToString();
                }
                else if (hdnRuleName.Value.ToLower() == "additionalrule9" || hdnRuleName.Value.ToLower() == "additionalrule10" || hdnRuleName.Value.ToLower() == "additionalrule11" || hdnRuleName.Value.ToLower() == "additionalrule12" || hdnRuleName.Value.ToLower() == "additionalrule13")
                {
                    Label N1 = (Label)e.Row.FindControl("N1");
                    string ranks = "";
                    for (int i = 0; i < N1.Text.Split('|').Length; i++)
                        ranks += dtRanks.Select("ID='" + N1.Text.Split('|')[i] + "'")[0].ItemArray[1].ToString() + ", ";
                    N1.Text = ranks.Trim().TrimEnd(',');
                }
                else if (hdnRuleName.Value.ToLower() == "additionalrule14" || hdnRuleName.Value.ToLower() == "additionalrule15")
                {
                    Label N1 = (Label)e.Row.FindControl("N1");
                    Label N2 = (Label)e.Row.FindControl("N2");
                    N1.Text = dtRanks.Select("ID='" + N1.Text + "'")[0].ItemArray[1].ToString();
                    N2.Text = dtRanks.Select("ID='" + N2.Text + "'")[0].ItemArray[1].ToString();
                }
                else if (hdnRuleName.Value.ToLower() == "additionalrule16" || hdnRuleName.Value.ToLower() == "additionalrule17" || hdnRuleName.Value.ToLower() == "additionalrule18" || hdnRuleName.Value.ToLower() == "additionalrule19")
                {
                    Label N2 = (Label)e.Row.FindControl("N2");
                    Label N3 = (Label)e.Row.FindControl("N3");
                    N3.Text = dtRanks.Select("ID='" + N3.Text + "'")[0].ItemArray[1].ToString();
                    N2.Text = dtRanks.Select("ID='" + N2.Text + "'")[0].ItemArray[1].ToString();
                }
            }
        }
        catch { }
    }

    /// <summary>
    /// Clear Additional rules filter
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClearAdditionalRules_click(object sender, EventArgs e)
    {
        ddlOilMajors.ClearSelection();
        ddlRulesList.ClearSelection();
        bindRuleData(null, null);
    }
}