using System;
using System.Data;
using SMS.Business.QMSDB;
using System.Web;
public partial class RestHourDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                hdnVesselID.Value = Request.QueryString["Vessel_ID"].ToString();
                hdnLogBookID.Value = Request.QueryString["ID"].ToString();
                BindRestHoursDetails();
            }
        }
    }

    private void BindRestHoursDetails()
    {
        DataTable dt = BLL_QMS_RestHours.Get_RestHours_Details(int.Parse(Request.QueryString["ID"].ToString()), int.Parse(Request.QueryString["Vessel_ID"].ToString()));
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["ReverificationRemark"].ToString().Trim().Length > 0)
            {
                txtReverification.Text = dt.Rows[0]["ReverificationRemark"].ToString();
                txtReverification.Visible = true;
                lblRev.Visible = true;
            }
            lblStaffCode.Text = dt.Rows[0]["Staff_Code"].ToString();
            lblStaffName.Text = dt.Rows[0]["Staff_Name"].ToString();
            lblStaffRank.Text = dt.Rows[0]["Staff_rank_Code"].ToString();
            lblDateofjoing.Text = dt.Rows[0]["Joining_Date"].ToString() != "" ? Convert.ToDateTime(dt.Rows[0]["Joining_Date"]).ToString("dd/MMM/yyyy") : "";
            lblDateofsignoff.Text = dt.Rows[0]["Sign_Off_Date"].ToString() != "" ? Convert.ToDateTime(dt.Rows[0]["Sign_Off_Date"]).ToString("dd/MMM/yyyy") : "";
            lblSignOn.Text = dt.Rows[0]["Sign_On_Date"].ToString() != "" ? Convert.ToDateTime(dt.Rows[0]["Sign_On_Date"]).ToString("dd/MMM/yyyy") + " " + Convert.ToDateTime(dt.Rows[0]["Sign_On_Date"]).ToString("HH:MM") : "";
            txtSeafarerRemarks.Text = dt.Rows[0]["Seafarer_Remarks"].ToString();
            txtVerifierRemarks.Text = dt.Rows[0]["Verifier_Remarks"].ToString();
            lblManage.Text = dt.Rows[0]["Manager_Code"].ToString() + "-" + dt.Rows[0]["Manager_Rank"].ToString() + "-" + dt.Rows[0]["Manager_Name"].ToString();
            // lblManagerRank.Text = dt.Rows[0]["Manager_Rank"].ToString();
            lblRestHourDate.Text = "    Date :   " + dt.Rows[0]["REST_HOURS_DATE"].ToString();
            if (dt.Rows[0]["IsEmergency"].ToString() == "1")
                chkEmergency.Checked = true;
            if (dt.Rows[0]["IsArrival"].ToString() == "1")
                chkArrival.Checked = true;
            if (dt.Rows[0]["IsDeparture"].ToString() == "1")
                chkDeparture.Checked = true;
            if (dt.Rows[0]["IsDrill"].ToString() == "1")
                chkDrill.Checked = true;
            if (dt.Rows[0]["IsEmergencyVerify"].ToString() == "1")
                chkEmergencyVerify.Checked = true; 
            if (dt.Rows[0]["IsOthers"].ToString() == "1")
                chkOther.Checked = true;
            rpDeckLogBook01.DataSource = dt;
            rpDeckLogBook01.DataBind();

            lnkCreated.Text = dt.Rows[0]["Created_By_name"].ToString();
            lnkCreated.NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dt.Rows[0]["Created_By_Crew"].ToString();
            lnkCreated.Target = "_blank";
            imgCreated.ImageUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/uploads/CrewImages/" + dt.Rows[0]["PhotoUrl3"].ToString();
            lnkCreated.Visible = true;
            imgCreated.Visible = true;
         
            
                  
            

            lnkModif.Text = dt.Rows[0]["Modified_By_name"].ToString();
            lnkModif.NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dt.Rows[0]["Modified_By_Crew"].ToString();
            lnkModif.Target = "_blank";
            imgModif.ImageUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/uploads/CrewImages/" + dt.Rows[0]["PhotoURL4"].ToString();
            lnkModif.Visible = true;
            imgModif.Visible = true;

            lnkVerified.Text = dt.Rows[0]["Modified_By_name"].ToString();
            lnkVerified.NavigateUrl = "~/crew/crewdetails.aspx?ID=" + dt.Rows[0]["Modified_By_Crew"].ToString();
            lnkVerified.Target = "_blank";
            imgVerified.ImageUrl = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/uploads/CrewImages/" + dt.Rows[0]["PhotoURL4"].ToString();
            lnkVerified.Visible = true;
            imgVerified.Visible = true;
        }
    }

    protected void btnHiddenSubmit_Click(object sender, EventArgs e)
    {
        BindRestHoursDetails();
    }
}