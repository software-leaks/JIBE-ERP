using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Web.UI.HtmlControls;
using SMS.Business.PMS;
using SMS.Business.Crew;
using SMS.Business.VET;
using SMS.Properties;

public partial class Technical_Vetting_Vetting_AddSectionAndQuestion : System.Web.UI.Page
{
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_VET_VettingLib objBLLVetLib = new BLL_VET_VettingLib();
    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public void VET_Insert_SectionAndQuestion()
    {
        string strSectionNo = txtSection.Text.Trim();
        string strLevel2 = txtLevl2.Text.Trim();
        string strLevel3 = txtLevl3.Text.Trim();
        string strLevel4 = txtLevl4.Text.Trim();
        string strQuestion = txtQuestion.Text.Trim();
        string strRemarks = txtRemarks.Text.Trim();

       // objBLLVetLib.VET_Insert_SectionAndQuestion(strSectionNo,strLevel2,strLevel3,strLevel4,strQuestion,strRemarks, Convert.ToInt32(Session["USERID"]));

        string jsSqlError2 = "alert('Questionnaire saved successfully.');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "jsSqlError2", jsSqlError2, true);
    }

    

}