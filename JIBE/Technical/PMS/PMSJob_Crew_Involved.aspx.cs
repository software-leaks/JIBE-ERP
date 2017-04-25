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

public partial class PMSJob_Crew_Involved : System.Web.UI.Page
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

    public int JOB_ID = 0;
    public int JOB_HISTORY_ID = 0;
    public int VESSEL_ID = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

             JOB_ID = Convert.ToInt32(Request.QueryString["JobID"]);
             JOB_HISTORY_ID = Convert.ToInt32(Request.QueryString["JobHistoryID"]);
             VESSEL_ID = Convert.ToInt32(Request.QueryString["VID"]);

            Load_RankList();
            LoadCrewListToAdd(VESSEL_ID);
       
        }

        string msg1 = String.Format("StaffInfo();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
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
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_RankList();

        ddlCrewInvloveRank.DataSource = dt;
        ddlCrewInvloveRank.DataTextField = "Rank_Short_Name";
        ddlCrewInvloveRank.DataValueField = "ID";
        ddlCrewInvloveRank.DataBind();
        ddlCrewInvloveRank.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlCrewInvloveRank.SelectedIndex = 0;
    }



    private void LoadCrewListToAdd(int VESSEL_ID)
    {

      // lblCrewInvlabel.Text = dtsJobDetails.Tables[0].Rows[0]["Vessel_Name"].ToString();

        DataTable dtCrewList = objBLL.Get_Crew_Involved_To_Add_List(null, VESSEL_ID, null);

        grdCrewListToAdd.DataSource = dtCrewList;
        grdCrewListToAdd.DataBind();

    }


   



    protected void btnSearchCrewInvolve_Click(object s, EventArgs e)
    {

        DataTable dt = objBLL.Get_Crew_Involved_To_Add_List(txtCrewInvolveSearch.Text != "" ? txtCrewInvolveSearch.Text : null, Convert.ToInt32(Request.QueryString["VID"]), UDFLib.ConvertIntegerToNull(ddlCrewInvloveRank.SelectedValue));

        grdCrewListToAdd.DataSource = dt;
        grdCrewListToAdd.DataBind();


    }
  


   


     
}