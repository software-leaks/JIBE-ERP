using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.Inspection;
public partial class Technical_Worklist_ExecutiveSummary : System.Web.UI.Page
{
  //  BLL_Tec_Worklist objWl = new BLL_Tec_Worklist();
    BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
    DataSet ds = new DataSet();
    DataSet dsInspt = new DataSet();
    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["InspectionID"] = Request.QueryString["InspID"].ToString() ;

            if (ViewState["InspectionID"] != "" && ViewState["InspectionID"] != null)
            {
                BindSummary(Convert.ToInt32(ViewState["InspectionID"]));
                GetVesselInspectionDetails(Convert.ToInt32(ViewState["InspectionID"]));
               
                if (Request.QueryString["rnd"] != null)
                {
                    if (dlSummary.Items.Count > 0)
                    {
                        for (int i = 0; i <= dlSummary.Items.Count - 1; i++)
                        {
                            TextBox txttopic = ((TextBox)dlSummary.Items[i].FindControl("txtTopicDetails"));
                            if (txttopic.Text == "")
                            {
                                txttopic.Height = 20;
                            }

                        }
                    }
                }
            }

            UserAccessValidation();
        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

      
        ViewState["del"] = objUA.Delete;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public void GetVesselInspectionDetails(int InspectionID)
    {
        dsInspt = objInsp.GetVesselInspectionDetails(InspectionID);
        if (dsInspt.Tables[0].Rows.Count > 0)
        {
            lblVesselName.Text = dsInspt.Tables[0].Rows[0][0].ToString();
            lblFromToDate.Text = dsInspt.Tables[0].Rows[0][1].ToString();
        }
    }
    public void BindSummary(int InspectionID)
    {
        ds = objInsp.GetExecutiveSummary(InspectionID);

            if(ds.Tables[0].Rows.Count>0)
            {
                dlSummary.DataSource = ds;
                dlSummary.DataBind();
                //txtReportNo.Text = ds.Tables[0].Rows[0]["ReportNo"].ToString();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                txtFOLog.Text = ds.Tables[1].Rows[0][0].ToString();
                txtFOMeasured.Text = ds.Tables[1].Rows[0][1].ToString();
                txtMDOLog.Text = ds.Tables[1].Rows[0][2].ToString();
                txtMDOMeasured.Text = ds.Tables[1].Rows[0][3].ToString();
                txtMGOLog.Text = ds.Tables[1].Rows[0][4].ToString();
                txtMGOMeasured.Text = ds.Tables[1].Rows[0][5].ToString();
            }
    }

    protected void BtnSaveSummary_Click(object sender, EventArgs e)
    {
      

        if (dlSummary.Items.Count > 0)
        {

            for (int i = 0; i <= dlSummary.Items.Count-1; i++)
            {
              
                int TopicKey = Convert.ToInt32(((HiddenField)dlSummary.Items[i].FindControl("hdnTopicKey")).Value);
                string TopicDetails = ((TextBox)dlSummary.Items[i].FindControl("txtTopicDetails")).Text;
               
                int InspectionID = Convert.ToInt32(ViewState["InspectionID"]);
                int CreatedBy = GetSessionUserID();
                DateTime DateOfCreation = DateTime.Now;
                int ActiveStatus = 1;
                string ReportNo = ""; //txtReportNo.Text;

                if (TopicDetails.Length > 4000)
                {
                    string js2 = "alert('Character limit is exceeding above 4000 ');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                    ((TextBox)dlSummary.Items[i].FindControl("txtTopicDetails")).Focus();
                                       
                    //break;
                    return;
                }
                objInsp.InsertExecutiveSummary(TopicKey, TopicDetails, InspectionID, CreatedBy, DateOfCreation, ActiveStatus);
                


            }

            decimal parsedValue;
            string TotalFOLog=string.Empty;
            string TotalFOMeasured = string.Empty;
            string TotalMDOLog = string.Empty;
            string TotalMDOMeasured = string.Empty;
            string TotalMGOLog = string.Empty;
            string TotalMGOMeasured = string.Empty;
            int InspID = Convert.ToInt32(ViewState["InspectionID"]);
            int CreatdBy = GetSessionUserID();
            DateTime DateOfCreat = DateTime.Now;
            int ActStatus = 1;

            if (txtFOLog.Text != "")
            {
                if (!decimal.TryParse(txtFOLog.Text, out parsedValue))
                {
                    string js2 = "alert('Total FO As Per Log accept numeric value');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                    txtFOLog.Focus();
                    return;
                }
                else
                {
                      TotalFOLog =txtFOLog.Text;
                }
            }
            if (txtFOMeasured.Text != "")
            {
                if (!decimal.TryParse(txtFOMeasured.Text, out parsedValue))
                {
                    string js2 = "alert('Total FO Measured accept numeric value');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                    txtFOMeasured.Focus();
                    return;
                }
                else
                {
                    TotalFOMeasured = txtFOMeasured.Text;
                }
            }
            if (txtMDOLog.Text != "")
            {
                if (!decimal.TryParse(txtMDOLog.Text, out parsedValue))
                {
                    string js2 = "alert('Total MDO As Per Log accept numeric value');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                    txtMDOLog.Focus();
                    return;
                }
                else
                {
                    TotalMDOLog = txtMDOLog.Text;
                }
            }
            if (txtMDOMeasured.Text != "")
            {
                if (!decimal.TryParse(txtMDOMeasured.Text, out parsedValue))
                {
                    string js2 = "alert('Total MDO Measured accept numeric value');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                    txtMDOMeasured.Focus();
                    return;
                }
                else
                {
                    TotalMDOMeasured = txtMDOMeasured.Text;
                }
            }
            if (txtMGOLog.Text != "")
            {
                if (!decimal.TryParse(txtMGOLog.Text, out parsedValue))
                {
                    string js2 = "alert('Total MGO As Per Log accept numeric value');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                    txtMGOLog.Focus();
                    return;
                }
                else
                {
                    TotalMGOLog = txtMGOLog.Text;
                }
            }
            if (txtMGOMeasured.Text != "")
            {
                if (!decimal.TryParse(txtMGOMeasured.Text, out parsedValue))
                {
                    string js2 = "alert('Total MGO Measured accept numeric value');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js2, true);
                    txtMGOMeasured.Focus();
                    return;
                }
                else
                {
                    TotalMGOMeasured = txtMGOMeasured.Text;
                }
            }

           
           
            objInsp.InsertExecutiveSummaryBunkers(InspID,TotalFOLog,TotalFOMeasured,TotalMDOLog,TotalMDOMeasured,TotalMGOLog,TotalMGOMeasured,CreatdBy,DateOfCreat,ActStatus);
            string js3 = "alert('Summary Saved Successfully');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert9", js3, true);

        }
    }
}