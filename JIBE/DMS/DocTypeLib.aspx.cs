using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using SMS.Business.DMS;
using SMS.Business.Crew;


public partial class DMS_Admin_DocTypeLib : System.Web.UI.Page
{
    BLL_DMS_Admin objBLL = new BLL_DMS_Admin();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        if (!IsPostBack)
        {
            UserAccessValidation();
            Load_RankList();
        }
    }

    protected void ObjectDataSource1_Updating(object sender,ObjectDataSourceMethodEventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PID", typeof(int));
        foreach (GridViewRow row in GridViewDocType.Rows)
        {
            if ((row.RowState.ToString()).Contains("Edit"))
            {
                CheckBoxList a = row.FindControl("chkVesselFlagList") as CheckBoxList;
                foreach (ListItem chkitem in a.Items)
                if (chkitem.Selected.Equals(true))
                {
                    dt.Rows.Add(int.Parse(chkitem.Value));
                }
            }
        }
        e.InputParameters["VesselFlagList"] = dt;      
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
            GridViewDocType.Columns[GridViewDocType.Columns.Count - 1].Visible = false;
            btnSaveDocType.Enabled = false;
            btnSaveAttribute.Enabled = false;
            btnSaveMandatoryRank.Enabled = false;
        }
        if (objUA.Edit == 0)
        {
            GridViewDocType.Columns[GridViewDocType.Columns.Count - 3].Visible = false;
        }
        if (objUA.Delete == 0)
        {
            GridViewDocType.Columns[GridViewDocType.Columns.Count - 2].Visible = false;
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
    protected void btnSaveAttribute_Click(object sender, EventArgs e)
    {
        string strListSource = "";

        if (rdoLstAttributeDataType.SelectedValue == "LIST")
            strListSource = ddlListSource.SelectedValue;

        int responseid = objBLL.InsertDocAttribute(txtAttribute.Text.Trim(), rdoLstAttributeDataType.SelectedValue.ToString(), strListSource);

        GridViewAttributes.DataBind();
    }
    protected void GridViewAttributes_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DocTypeID = 0;
        int AttributeID = 0;
        int IsRequired = 0;

        if (GridViewDocType.SelectedIndex >= 0 && GridViewAttributes.SelectedIndex >= 0)
        {
            DocTypeID = int.Parse(GridViewDocType.SelectedValue.ToString());
            AttributeID = int.Parse(GridViewAttributes.SelectedValue.ToString());
            //IsRequired = int.Parse(GridViewAttributes.SelectedValue.ToString());

            int responseid = objBLL.Add_AttributeToDocType(DocTypeID, AttributeID, IsRequired);

            GridViewAttributes.DataBind();

            GridViewDocAttributeLinking.DataBind();
        }
    }
    protected void ObjectDataSource2_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        GridViewAttributes.DataBind();
    }
    protected void chkIsRequired_CheckedChanged(object sender, EventArgs e)
    {
        int ID = 0;
        int IsRequired = 0;
        CheckBox chk = (CheckBox)sender;

        //ID = int.Parse(GridViewDocAttributeLinking.SelectedValue.ToString());

        if (chk != null)
        {
            ID = int.Parse(chk.Text.ToString());
            IsRequired = (chk.Checked == true) ? 1 : 0;

            int responseid = objBLL.Update_DocType_Attribute(ID, IsRequired, -1);
        }


    }
    protected void txtAlertDays_TextChanged(object sender, EventArgs e)
    {
        DataControlFieldCell cell = (DataControlFieldCell)((TextBox)(sender)).Parent;
        GridViewRow row = (GridViewRow)(cell.Parent);
        string sID = ((CheckBox)row.FindControl("chkIsRequired")).Text;

        string AlertDays = ((TextBox)(sender)).Text;
        try
        {
            int iAlertDays = 0;
            if (AlertDays != "")
                iAlertDays = int.Parse(AlertDays);

            int ID = int.Parse(sID);
            int responseid = objBLL.Update_DocType_Attribute(ID, -1, iAlertDays);

        }
        catch { }

    }
    protected void rdoLstAttributeDataType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rdoLstAttributeDataType.SelectedValue == "LIST")
        {
            ddlListSource.Visible = true;
            lblListSource.Visible = true;

            ddlListSource.DataSource = objBLL.Get_AttributeListSource();
            ddlListSource.DataValueField = "VALUE";
            ddlListSource.DataTextField = "TEXT";
            ddlListSource.DataBind();
        }
    }
    protected void ddlRank_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkRankList.Items.Clear();    
    }
    protected void btnSaveDocType_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDocType.Text.Trim().Equals(""))
            {
                string js = "Document Name is mandatory!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoFile", "alert('" + js + "');", true);
                return;
            }
            else
            {
                int AlertDays = 0;
                int AddToChecklist = 0;
                int VoyageDoc = 0;
                int Vessel_Flag = 0;

                int ExpiryMandatory = 0;
                if (chkExpiryMandatory.Checked == true)
                    ExpiryMandatory = 1;

                if (txtAlertDays.Text.Trim().Length > 0)
                    AlertDays = int.Parse(txtAlertDays.Text);

                if (chkCheckList.Checked == true)
                    AddToChecklist = 1;

                if (chkVoyageDoc.Checked == true)
                    VoyageDoc = 1;
                Vessel_Flag = 0;
                DataTable dt = new DataTable();
                dt.Columns.Add("PID", typeof(int));
                foreach (ListItem chkitem in chkVesselFlagList.Items)
                    if (chkitem.Selected.Equals(true))
                    {
                        dt.Rows.Add(int.Parse(chkitem.Value));
                    }

                int responseid = objBLL.InsertDocType(txtDocType.Text.Trim(), AlertDays, AddToChecklist, VoyageDoc, Vessel_Flag, ExpiryMandatory, dt, Convert.ToInt32(Session["USERID"]));
                GridViewDocType.DataBind();

                string js = "closeDivAddDocType();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "close", js, true);
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
    protected void GridViewDocType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DocTypeID = int.Parse(GridViewDocType.SelectedValue.ToString());
        Load_MandatoryRankList(DocTypeID);
        chkSelectAll.Checked = false;
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
                Label lbl = e.Row.FindControl("lblVesselFlag") as Label;
                lbl.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Vessel Flag] body=[ " + DataBinder.Eval(e.Row.DataItem, "VesselFlagNameList") + "]");
                lbl.Visible = true;
            }            
        }
    }
    protected void Load_MandatoryRankList(int DocTypeID)
    {
        try
        {

            DataTable dt = objBLL.Get_MandatoryRankList(DocTypeID, int.Parse(ddlRank.SelectedValue));

            chkRankList.DataSource = dt;
            chkRankList.DataTextField = "Rank_Name";
            chkRankList.DataValueField = "Selected";

            chkRankList.DataBind();          


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Selected"].ToString() == "1")
                    chkRankList.Items[i].Selected = true;
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

    }
    protected void btnSaveMandatoryRank_Click(object sender, EventArgs e)
    {
        try
        {
            int DocTypeID = int.Parse(GridViewDocType.SelectedValue.ToString());

            foreach (ListItem li in chkRankList.Items)
            {

                if (li.Selected == true)
                {
                    objBLL.UPDATE_MandatoryRankList(DocTypeID, li.Text, 1, UDFLib.ConvertToInteger(Session["USERID"].ToString()));
                }
                else
                {
                    objBLL.UPDATE_MandatoryRankList(DocTypeID, li.Text, 0, UDFLib.ConvertToInteger(Session["USERID"].ToString()));
                }
            }
            lblMessage.Text = "Rank list saved !!";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }
    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        for (int i = 0; i < chkRankList.Items.Count; i++)
        {
            chkRankList.Items[i].Selected = chk.Checked;
        }
    }

    protected void RowCheckedChanged(object sender, System.EventArgs e)
    {
        try
        {
            DataControlFieldCell cell = (DataControlFieldCell)((CheckBox)(sender)).Parent;
            GridViewRow row = (GridViewRow)(cell.Parent);
            if (row != null)
            {
                string sID = ((CheckBox)row.FindControl("chkVoyage")).Checked.ToString();
                CheckBox chk = (CheckBox)row.FindControl("chkDocCheckList");
                if (sID == "True")
                {
                    chk.Checked = true;
                }
            }           
        }
        catch (Exception ex)
        {
            
        }
    }
}