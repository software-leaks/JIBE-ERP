using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Operation;
using System.Data;

public partial class Operations_TerminalInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int _Vessel_Id = 0;
        int _PortInfoReportId = 0;
        if (Request.QueryString["VesselId"] != null && Request.QueryString["PortInfoReportId"] != null)
        {
            _Vessel_Id = int.Parse(Request.QueryString["VesselId"].ToString());
            _PortInfoReportId = int.Parse(Request.QueryString["PortInfoReportId"].ToString());

            DataSet ds = BLL_OPS_VoyageReports.Get_TerminalInfoReport(_Vessel_Id, _PortInfoReportId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                CondOfBollards_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["CondOfBollards_Rat"]));
                CondOfApron_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["CondOfApron_Rat"]));
                CondOfShoreCranes_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["CondOfShoreCranes_Rat"]));
                BerthLighting_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["BerthLighting_Rat"]));
                TugsPerformance_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["TugsPerformance_Rat"]));
                CondOfTugsEqu_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["CondOfTugsEqu_Rat"]));
                PreTransConf_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["PreTransConf_Rat"]));
                SafetyAware_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["SafetyAware_Rat"]));
                EngSkill_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["EngSkill_Rat"]));
                Access_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["Access_Rat"]));
                Courtesy_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["Courtesy_Rat"]));
                EmerPreparedness_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["EmerPreparedness_Rat"]));
                EffOfMooringGang_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["EffOfMooringGang_Rat"]));
                Efficiency_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["Efficiency_Rat"]));
                Communication_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["Communication_Rat"]));
                CrewHandling_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["CrewHandling_Rat"]));
                Documentation_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["Documentation_Rat"]));
                CostEfficiency_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["CostEfficiency_Rat"]));
                CondOfBoats_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["CondOfBoats_Rat"]));
                SafetyAwareness_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["SafetyAwareness_Rat"]));
                SurveyorSafetyAware_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["SurveyorSafetyAware_Rat"]));
                Accessibility_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["Accessibility_Rat"]));
                Security_Rat.SetRating(Convert.ToInt16(ds.Tables[0].Rows[0]["Security_Rat"]));

                lblCondOfBollards_Comm.Text = ds.Tables[0].Rows[0]["CondOfBollards_Comm"].ToString();
                lblCondOfApron_Comm.Text = ds.Tables[0].Rows[0]["CondOfApron_Comm"].ToString();
                lblCondOfShoreCranes_Comm.Text = ds.Tables[0].Rows[0]["CondOfShoreCranes_Comm"].ToString();
                lblBerthLighting_Comm.Text = ds.Tables[0].Rows[0]["BerthLighting_Comm"].ToString();
                lblTugsPerformance_Comm.Text = ds.Tables[0].Rows[0]["TugsPerformance_Comm"].ToString();
                lblCondOfTugsEqu_Comm.Text = ds.Tables[0].Rows[0]["CondOfTugsEqu_Comm"].ToString();
                lblPreTransConf_Comm.Text = ds.Tables[0].Rows[0]["PreTransConf_Comm"].ToString();
                lblSafetyAware_Comm.Text = ds.Tables[0].Rows[0]["SafetyAware_Comm"].ToString();
                lblEngSkill_Comm.Text = ds.Tables[0].Rows[0]["EngSkill_Comm"].ToString();
                lblAccess_Comm.Text = ds.Tables[0].Rows[0]["Access_Comm"].ToString();
                lblCourtesy_Comm.Text = ds.Tables[0].Rows[0]["Courtesy_Comm"].ToString();
                lblEmerPreparedness_Comm.Text = ds.Tables[0].Rows[0]["EmerPreparedness_Comm"].ToString();
                lblEffOfMooringGang_Comm.Text = ds.Tables[0].Rows[0]["EffOfMooringGang_Comm"].ToString();
                lblEfficiency_Comm.Text = ds.Tables[0].Rows[0]["Efficiency_Comm"].ToString();
                lblCommunication_Comm.Text = ds.Tables[0].Rows[0]["Communication_Comm"].ToString();
                lblCrewHandling_Comm.Text = ds.Tables[0].Rows[0]["CrewHandling_Comm"].ToString();
                lblDocumentation_Comm.Text = ds.Tables[0].Rows[0]["Documentation_Comm"].ToString();
                lblCostEfficiency_Comm.Text = ds.Tables[0].Rows[0]["CostEfficiency_Comm"].ToString();
                lblCondOfBoats_Comm.Text = ds.Tables[0].Rows[0]["CondOfBoats_Comm"].ToString();
                lblSafetyAwareness_Comm.Text = ds.Tables[0].Rows[0]["SafetyAwareness_Comm"].ToString();
                lblSurveyorSafetyAware_Comm.Text = ds.Tables[0].Rows[0]["SurveyorSafetyAware_Comm"].ToString();
                lblAccessibility_Comm.Text = ds.Tables[0].Rows[0]["Accessibility_Comm"].ToString();
                lblSecurity_Comm.Text = ds.Tables[0].Rows[0]["Security_Comm"].ToString();

                lblCompanyName.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                lblSurveyorCompanyName.Text = ds.Tables[0].Rows[0]["SurveyorCompanyName"].ToString();
                lblBoatCompanyName.Text = ds.Tables[0].Rows[0]["BoatCompanyName"].ToString();

                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
            }
        }
    }
    protected void btnViewAttachments_Click(object sender, EventArgs e)
    {
        int Vessel_Id = 0;
        int Page_Type_Id = 0;
        int Report_Id = 0;

        Vessel_Id = int.Parse(Request.QueryString["VesselId"].ToString());
        Page_Type_Id = 2;
        Report_Id = int.Parse(Request.QueryString["PortInfoReportId"].ToString());

        DataTable dt = BLL_OPS_VoyageReports.Get_PRT_Attachment(Page_Type_Id, Report_Id, Vessel_Id);

        grdPTR_Attachment.DataSource = dt;
        grdPTR_Attachment.DataBind();

        string msgViewAttachments = string.Format("showModal('divViewAttachments1',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgViewAttachments", msgViewAttachments, true);
        UpdatePnl1.Update();
    }
}