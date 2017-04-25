using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SMS.Business.Technical;
using SMS.Business.Crew;
using System.IO;
using System.Collections.Generic;
using SMS.Properties;
using SMS.Business.Infrastructure;
public partial class Worklist_Crew_Involved : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();


    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.Page.Header.Controls.Add(SetUserStyle.AddThemeInHeader());
            base.OnInit(e);
        }
        catch { }
    }

    public int OFFICE_ID = 0;
    public int WORKLIST_ID = 0;
    public int VESSEL_ID = 0;

    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            OFFICE_ID = Convert.ToInt32(Request.QueryString["OFFID"]);
            WORKLIST_ID = Convert.ToInt32(Request.QueryString["WLID"]);
            VESSEL_ID = Convert.ToInt32(Request.QueryString["VID"]);

            //Load_RankList();
            //  LoadCrewListToAdd(VESSEL_ID);
            UserAccessValidation();
            if (dvWorkListCrewInvolved.Visible)
            {
                LoadWorkListCrewInvolved();
            }
        }
         
        string msg1 = String.Format("StaffInfo();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {

            lblMessage.Text = "you don't have sufficient previlege to access the requested page.";
            
            dvWorkListCrewInvolved.Visible = false;
        }
        
        if (objUA.Add == 0)
        {
            btnAdd.Enabled = false;

        }
        ViewState["del"] = objUA.Delete;


    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }




    //private void LoadCrewListToAdd(int VESSEL_ID)
    //{

    //  // lblCrewInvlabel.Text = dtsJobDetails.Tables[0].Rows[0]["Vessel_Name"].ToString();

    //    DataTable dtCrewList = objBLL.Get_Crew_Involved_To_Add_List(null, VESSEL_ID, null);

    //    grdCrewListToAdd.DataSource = dtCrewList;
    //    grdCrewListToAdd.DataBind();

    //}


    private void LoadWorkListCrewInvolved()
    {
        DataTable dtCrewInvolved = objBLL.Get_WorkList_Crew_Involved(Convert.ToInt32(Request.QueryString["OFFID"]), Convert.ToInt32(Request.QueryString["VID"]), Convert.ToInt32(Request.QueryString["WLID"]));

        grdCrewInvolved.DataSource = dtCrewInvolved;
        grdCrewInvolved.DataBind();

        BindCew();

    }
    protected void BindCew()
    {
        UpdateCrewList();
        int SelectRecordCount = 1;
        DataTable dtAllCrew = objBLL.Get_Crewlist_Index_By_Vessel(txtSearch.Text, null, UDFLib.ConvertIntegerToNull(rdbStatus.SelectedValue), null, ucCustomPagerctp.PageSize, ucCustomPagerctp.CurrentPageIndex, null, null, Convert.ToInt32(Request.QueryString["VID"]), ref SelectRecordCount);
        grdAddCrewInvolved.DataSource = dtAllCrew;
        grdAddCrewInvolved.DataBind();
        ucCustomPagerctp.CountTotalRec = SelectRecordCount.ToString();
        ucCustomPagerctp.BuildPager();
        if (dtAllCrew != null)
            if (dtAllCrew.Rows.Count > 0)
                lblVesselName.Text = dtAllCrew.Rows[0]["Vessel_Name"].ToString();
        DataTable dk = ((DataTable)ViewState["dtAllCrew"]);

        if (dk == null)
        {
            dk = new DataTable();
            dk = dtAllCrew.Clone();
            dk.PrimaryKey = new DataColumn[] { dk.Columns["Voyage_ID"] };
        }


        foreach (DataRow item in dtAllCrew.Rows)
        {
            if (!dk.Rows.Contains(item["Voyage_ID"]))
                dk.Rows.Add(item.ItemArray);
        }

        ViewState["dtAllCrew"] = dk;

    }


    //protected void btnSearchCrewInvolve_Click(object s, EventArgs e)
    //{

    //    DataTable dt = objBLL.Get_Crew_Involved_To_Add_List(txtCrewInvolveSearch.Text != "" ? txtCrewInvolveSearch.Text : null, Convert.ToInt32(Request.QueryString["VID"]), UDFLib.ConvertIntegerToNull(ddlCrewInvloveRank.SelectedValue));

    //    grdCrewListToAdd.DataSource = dt;
    //    grdCrewListToAdd.DataBind();


    //}

    //protected void btnAddCrewInvolve_Click(object s, EventArgs e)
    //{


    //}


    protected void onDelete(object sender, EventArgs e)
    {

        int retval = objBLL.WorkList_Crew_Involved_Delete(Convert.ToInt32(((ImageButton)sender).CommandArgument.ToString()), Convert.ToInt32(Request.QueryString["VID"]), Convert.ToInt32(Request.QueryString["OFFID"]), Convert.ToInt32(Session["USERID"]));
        LoadWorkListCrewInvolved();
    }

    protected bool SelectCheckbox(string Voyage_ID)
    {
        List<string> dtSelectedCrew = ((List<string>)ViewState["dtSelectedCrew"]);

        if (dtSelectedCrew != null)
            if (dtSelectedCrew.Count > 0)
                if (dtSelectedCrew.Contains(Voyage_ID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
        return false;

    }

    protected void UpdateCrewList()
    {



        List<string> dtSelectedCrew = ((List<string>)ViewState["dtSelectedCrew"]);

        if (dtSelectedCrew == null)
            dtSelectedCrew = new List<string>();





        foreach (GridViewRow item in grdAddCrewInvolved.Rows)
        {


            if (dtSelectedCrew.Count > 0)
            {

                if (((CheckBox)grdAddCrewInvolved.Rows[item.RowIndex].FindControl("checkRow")).Checked)
                {
                    if (!dtSelectedCrew.Contains(grdAddCrewInvolved.DataKeys[item.RowIndex][0].ToString()))
                        dtSelectedCrew.Add(grdAddCrewInvolved.DataKeys[item.RowIndex][0].ToString());
                }
                else
                {
                    dtSelectedCrew.Remove(grdAddCrewInvolved.DataKeys[item.RowIndex][0].ToString());
                }
            }
            else
            {
                if (((CheckBox)grdAddCrewInvolved.Rows[item.RowIndex].FindControl("checkRow")).Checked)
                {
                    dtSelectedCrew.Add(grdAddCrewInvolved.DataKeys[item.RowIndex][0].ToString());
                }

            }


        }

        if (ViewState["Status"] == null)
            ViewState["Status"] = rdbStatus.SelectedValue;
        if (ViewState["Status"].ToString() != rdbStatus.SelectedValue)
            dtSelectedCrew.Clear();
        ViewState["Status"] = rdbStatus.SelectedValue;
        ViewState["dtSelectedCrew"] = dtSelectedCrew;
    }
    protected void Search_Click(object sender, EventArgs e)
    {

        BindCew();

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        UpdateCrewList();
        DataTable dtAllCrew = ((DataTable)ViewState["dtAllCrew"]);
        List<string> dtSelectedCrew = ((List<string>)ViewState["dtSelectedCrew"]);

        if (dtSelectedCrew == null)
            dtSelectedCrew = new List<string>();

        foreach (string item in dtSelectedCrew)
        {


            int retval = objBLL.WorkList_Crew_Involved_Insert(Convert.ToInt32(Request.QueryString["OFFID"]), Convert.ToInt32(Request.QueryString["VID"]), Convert.ToInt32(Request.QueryString["WLID"]), UDFLib.ConvertIntegerToNull(item), Convert.ToInt32(Session["UserID"].ToString()));

        }

        LoadWorkListCrewInvolved();
    }
}