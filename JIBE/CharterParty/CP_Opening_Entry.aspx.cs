using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.VM;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;
using SMS.Business.CP;

public partial class CP_Opening_Entry : System.Web.UI.Page
{
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    private long lCurrentRecord = 0;
    private long lRecordsPerRow = 200;
    private int Progress_Id = 0;
    private int Opening_Id = 0;
    private int Vessel_Id = 0;
    private string  OpeningStatus = "";

    public UserAccess objUA = new UserAccess();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_CP_Openings oBLL_Openings = new BLL_CP_Openings();
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserAccessValidation();
        if (!IsPostBack)
        {
           
            BindVessels();

            if (Request.QueryString[1] != null)
                BindOpeningDetail();
 
        }
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
            //btnsave.Enabled = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            // btnsave.Visible = false;
            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    protected void ClearControls()
    {
        txtDescription.Text = "";
        txtName.Text = "";
        txtBroker.Text = "";
        txtCharter.Text = "";
        txtContactEmail.Text = "";
        txtContactName.Text = "";
        txtContactPhone.Text = "";



    }



    protected void BindVessels()
    {
        Session["dtVessel"] = null;
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        DataTable dt = oBLL_Openings.GetVesselListAll(UserCompanyID);

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-Select-", "0"));



    }


    protected void btnVesselAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (Session["dtVessel"] != null)
        {
            dt = (DataTable)Session["dtVessel"];

        }
        else
        {
            dt.Columns.Add("VesselId");
            dt.Columns.Add("Vessel_Name");
        }
        DataRow dr = dt.NewRow();
        dr["VesselId"] = ddlVessel.SelectedValue;
        dr["Vessel_Name"] = ddlVessel.SelectedItem.Text;
        dt.Rows.Add(dr);

        ddlVessel.Items.RemoveAt(ddlVessel.SelectedIndex);
        Session["dtVessel"] = dt;
        chkVessel.DataSource = dt;
        chkVessel.DataValueField = "VesselId";
        chkVessel.DataTextField = "Vessel_Name";
        chkVessel.DataBind();

        if (chkVessel.Items.Count > 0)
        {
            foreach (ListItem chkitem in chkVessel.Items)
            {
                chkitem.Selected = true;
            }

        }

    }


    protected void BindOpeningDetail()
    {
        try
        {

            Opening_Id = Convert.ToInt32(Request.QueryString[0]);
            Vessel_Id = Convert.ToInt32(Request.QueryString[1]);
            if(oBLL_Openings.Get_OpeningDetails(Opening_Id,Vessel_Id).Tables.Count > 0)
            {
            DataTable dtDetail = oBLL_Openings.Get_OpeningDetails(Opening_Id,Vessel_Id).Tables[0];
            txtName.Text = dtDetail.Rows[0]["Opening"].ToString();
            txtBroker.Text = dtDetail.Rows[0]["Charterer_Name"].ToString();
            txtCharter.Text = dtDetail.Rows[0]["Broker_Name"].ToString();
            txtDescription.Text = dtDetail.Rows[0]["Business_Description"].ToString();
            txtContactEmail.Text = dtDetail.Rows[0]["Contact_Email"].ToString();
            txtContactPhone.Text = dtDetail.Rows[0]["Contact_Mobile"].ToString();
            string sStatus = dtDetail.Rows[0]["Entry_Type"].ToString();
            btnVesselAdd.Visible = false;
            chkVessel.Visible = false;
            dvVesselList.Visible = false;
            if (ddlStatus.Items.FindByValue(sStatus) != null)
            {
                ddlStatus.SelectedValue = ddlStatus.Items.FindByValue(sStatus).Value;
            }

            if (ddlVessel.Items.FindByValue(Vessel_Id.ToString()) != null)
            {
                ddlVessel.SelectedValue = ddlVessel.Items.FindByValue(Vessel_Id.ToString()).Value;
            }
            DataTable dtHistory = oBLL_Openings.Get_OpeningDetails(Opening_Id, Vessel_Id).Tables[1];
            if(dtHistory!=  null)
            {
                gvOpenings.DataSource = dtHistory;
                gvOpenings.DataBind();
            
            } 
            }
        }
        catch (Exception ex)
        {
            string ERR = ex.ToString();
        }

    }



    protected void btnVesselRemove_Click(object sender, EventArgs e)
    {

        DataTable dt = (DataTable)Session["dtVessel"];
        BindVessels();

        dt.Clear();
        Session["dt"] = dt;
        chkVessel.DataSource = dt;

        chkVessel.DataBind();


    }
    protected void ibtnDelete_click(object sender, CommandEventArgs  e)
    {
            DataTable dt = new DataTable();
            try
            {
                Progress_Id = Convert.ToInt32(e.CommandArgument);
                oBLL_Openings.DEL_Opening_Update(Progress_Id, GetSessionUserID());
                BindOpeningDetail();
            }
            catch { }

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {


        string status = ddlStatus.SelectedItem.Value;
        if (Request.QueryString[0]!= null)
             Opening_Id = Convert.ToInt32(Request.QueryString[0]);
        OpeningStatus=ddlStatus.SelectedValue;


        if (Request.QueryString[1] != null && (Convert.ToInt32(Request.QueryString[1])) != 0)
        {
            Vessel_Id = Convert.ToInt32(ddlVessel.SelectedValue);
            Progress_Id = oBLL_Openings.Ins_Opening_Updates(Opening_Id, Vessel_Id, OpeningStatus, txtDescription.Text, txtCharter.Text, txtBroker.Text, txtContactEmail.Text,
            txtContactPhone.Text, txtContactName.Text, txtName.Text, GetSessionUserID());
        }
        else
        {
            if (chkVessel.Items.Count > 0)
            {
                foreach (ListItem chkitem in chkVessel.Items)
                {
                    Vessel_Id = Convert.ToInt32(chkitem.Value);
                    Progress_Id = oBLL_Openings.Ins_Opening_Updates(Opening_Id, Vessel_Id, OpeningStatus, txtDescription.Text, txtCharter.Text, txtBroker.Text, txtContactEmail.Text,
                        txtContactPhone.Text, txtContactName.Text, txtName.Text, GetSessionUserID());

                }
            }
            else
            {
                Vessel_Id = Convert.ToInt32(ddlVessel.SelectedValue);
                Progress_Id = oBLL_Openings.Ins_Opening_Updates(Opening_Id, Vessel_Id, OpeningStatus, txtDescription.Text, txtCharter.Text, txtBroker.Text, txtContactEmail.Text,
                    txtContactPhone.Text, txtContactName.Text, txtName.Text, GetSessionUserID());
            }
        }
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
    }


    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

}
