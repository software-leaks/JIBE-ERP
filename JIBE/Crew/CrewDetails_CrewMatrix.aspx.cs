using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;
using AjaxControlToolkit;
using System.IO;
using AjaxControlToolkit4;

public partial class Crew_CrewDetails_CrewMatrix : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Crew_Seniority objBLLCrewSeniority = new BLL_Crew_Seniority();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    UserAccess objUA = new UserAccess();
    public int CrewID = 0;
    string Host = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        
        Host = Request.Url.AbsoluteUri.ToString().Substring(0, Request.Url.AbsoluteUri.ToString().ToLower().IndexOf("/crew/")) + "/";

        if (GetSessionUserID() == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "RedirectToLogin", "RedirectToLogin('" + Host + "Account/Login.aspx');", true);
            return;
        }
        
        CrewID = UDFLib.ConvertToInteger(Request.QueryString["CrewID"]);
        if (!IsPostBack)
        {
            getEnglishProficiency();
            if (UserAccessValidation())
            {
                Load_Country();
                DefaultValues();
            }
        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected bool UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";
            divMain.Visible = false;
            return false;
        }
        if (objUA.Add == 0)
        {
            btnResetCrewMatrix.Visible = false;
            btnSave.Visible = false;
        }
        return true;
    }
    private void Load_Country()
    {
        try
        {
            BLL_Infra_Country objCountry = new BLL_Infra_Country();

            ddlCountry.DataSource = objCountry.Get_CountryList();
            ddlCountry.DataTextField = "COUNTRY";
            ddlCountry.DataValueField = "ID";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("-Select-", "0"));

        }
        catch { }
    }
    public int GetCrewID()
    {
        try
        {
            if (Request.QueryString["CrewID"] != null)
            {
                return int.Parse(Request.QueryString["CrewID"].ToString());
            }
            else
                return 0;
        }
        catch { return 0; }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int CrewID = GetCrewID();
        int UserID = GetSessionUserID();

        string TankerCert = "";
        for (int i = 0; i < chklstTankerCertification.Items.Count; i++)
        {
            if (chklstTankerCertification.Items[i].Selected)
                TankerCert += chklstTankerCertification.Items[i].Value + "|";
        }
        string AdminAccept = ddlAdminAccept.SelectedValue == "" ? "" : UDFLib.ConvertStringToNull(ddlAdminAccept.SelectedValue);

        string EnglishProf = ddlEnglishProf.SelectedValue == "0" ? "" : ddlEnglishProf.SelectedItem.Text;
        objBLLCrew.Insert_Crew_Matrix(CrewID, UDFLib.ConvertIntegerToNull(ddlCertOfComp.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCountry.SelectedValue), AdminAccept, TankerCert.TrimEnd('|'), UDFLib.ConvertStringToNull(ddlSTCW.SelectedValue.ToString()), UDFLib.ConvertStringToNull(ddlRadioQual.SelectedValue), EnglishProf, UserID);
            
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SavedCrewMatrix", "alert('" + UDFLib.GetException("SuccessMessage/SaveMessage") + "');", true);
        DefaultValues();
    }
    public void getEnglishProficiency()
    {
        DataSet ds = new DataSet();
        ds = objCrewAdmin.getEnglishProficiency(null);

        ddlEnglishProf.DataSource = ds.Tables[0];
        ddlEnglishProf.DataTextField = "EnglishProficiency";
        ddlEnglishProf.DataValueField = "EnglishProficiency";
        ddlEnglishProf.DataBind();
        ddlEnglishProf.Items.Insert(0, new ListItem("-Select-", "0"));

        DataTable dt = objCrewAdmin.getEnglishProficiency(GetCrewID()).Tables[0];
        if (dt.Rows.Count > 0)
        {
            ddlEnglishProf.SelectedValue = dt.Rows[0]["English_Proficiency"].ToString();
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        DefaultValues();
        DataSet ds = objCrewAdmin.Get_Crew_Matrix_Configuration();
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                for (int j = 0; j < chklstTankerCertification.Items.Count ; j++)
                {
                    for (int i = 0; i < dt.Rows.Count ; i++)
                    {
                        if (dt.Rows[i]["ID"].ToString() == Convert.ToString(chklstTankerCertification.Items[j].Value))
                        {
                            if (dt.Rows[i]["DefaultValue"].ToString() == "1")
                                chklstTankerCertification.Items[j].Selected = true;
                            else
                                chklstTankerCertification.Items[j].Selected = false;
                       }
                    }
                }
               DataTable dt1 = ds.Tables[1];
               dt1.DefaultView.RowFilter = "DefaultValue= 1";
               ddlSTCW.SelectedValue = dt1.DefaultView[0]["Parameters"].ToString();
            }
        }
    }
    protected void DefaultValues()
    {
        try
        {
            ddlSTCW.ClearSelection();
            DataSet dsCrewMatrix = new DataSet();
            dsCrewMatrix = objBLLCrew.Get_CrewMatix_DetailAndValue(CrewID);
            if (dsCrewMatrix != null)
            {
                ddlCertOfComp.Items.Clear();
                ddlSTCW.Items.Clear();
                if (dsCrewMatrix.Tables[0].Rows.Count > 0)
                {
                    ddlCertOfComp.DataSource = dsCrewMatrix.Tables[0];
                    ddlCertOfComp.DataTextField = "DocTypeName";
                    ddlCertOfComp.DataValueField = "DocTypeID";
                    ddlCertOfComp.DataBind();
                }
                for (int i = 0; i < dsCrewMatrix.Tables[0].Rows.Count; i++)
                {
                    ListItem lst = (ListItem)ddlCertOfComp.Items[i];
                    lst.Attributes["countryofissue"] = Convert.ToString(dsCrewMatrix.Tables[0].Rows[i]["countryofissue"]);

                    if (Convert.ToString(dsCrewMatrix.Tables[0].Rows[i]["DateOfExpiry"]) != "")
                    {
                        lst.Attributes["DateOfExpiry"] = Convert.ToString(dsCrewMatrix.Tables[0].Rows[i]["DateOfExpiry"]);
                        lst.Attributes["ConvertDate"] = UDFLib.ConvertUserDateFormat(Convert.ToString(dsCrewMatrix.Tables[0].Rows[i]["DateOfExpiry"]));
                    }

                    if (Convert.ToString(dsCrewMatrix.Tables[0].Rows[i]["DateOfIssue"]) != "")
                        lst.Text = dsCrewMatrix.Tables[0].Rows[i]["DocTypeName"] + " - " + UDFLib.ConvertUserDateFormat(Convert.ToDateTime(dsCrewMatrix.Tables[0].Rows[i]["DateOfIssue"]).ToShortDateString());
                }
                ddlCertOfComp.Items.Insert(0, new ListItem() { Value = "", Text = "-Select-" });

                if (dsCrewMatrix.Tables[2].Rows.Count > 0)
                {
                    if (Convert.ToString(dsCrewMatrix.Tables[2].Rows[0]["Certificate_Of_Competency"]) != "")
                    {
                        ddlCertOfComp.SelectedValue = dsCrewMatrix.Tables[2].Rows[0]["Certificate_Of_Competency"].ToString();
                        imgRecordInfoCC.Visible = true;
                    }
                    else
                    {
                        if (dsCrewMatrix.Tables[3].Rows[0]["Certificate_Of_Competency"].ToString().Equals("0"))
                            imgRecordInfoCC.Visible = false;
                        else
                            imgRecordInfoCC.Visible = true;
                    }
                    ddlCountry.SelectedValue = dsCrewMatrix.Tables[2].Rows[0]["Issuing_Country"].ToString().Equals("") ? "0" : dsCrewMatrix.Tables[2].Rows[0]["Issuing_Country"].ToString();
                    if (dsCrewMatrix.Tables[2].Rows[0]["Radio_Qualification"].ToString().Equals(""))
                    {
                        ddlRadioQual.SelectedValue = "";
                        if (dsCrewMatrix.Tables[3].Rows[0]["Radio_Qualification"].ToString().Equals("0"))
                            imgRecordInfoRadio.Visible = false;
                        else
                            imgRecordInfoRadio.Visible = true;
                    }
                    else
                    {
                        ddlRadioQual.SelectedValue = dsCrewMatrix.Tables[2].Rows[0]["Radio_Qualification"].ToString();
                        imgRecordInfoRadio.Visible = true;
                    }
                    if (dsCrewMatrix.Tables[2].Rows[0]["Administration_Acceptance"].ToString().Equals(""))
                    {
                        if (dsCrewMatrix.Tables[3].Rows[0]["Administration_Acceptance"].ToString().Equals("0"))
                            imgRecordInfoAA.Visible = false;
                        else
                            imgRecordInfoAA.Visible = true;
                        ddlAdminAccept.SelectedValue = "";
                    }
                    else
                    {
                        ddlAdminAccept.SelectedValue = dsCrewMatrix.Tables[2].Rows[0]["Administration_Acceptance"].ToString();
                        imgRecordInfoAA.Visible = true;
                    }
                }
                else
                {
                    imgRecordInfoCC.Visible = imgRecordInfoAA.Visible = imgRecordInfoRadio.Visible = false;
                }
                if (dsCrewMatrix.Tables[1].Rows.Count > 0)
                {
                    chklstTankerCertification.ClearSelection();
                    var TankerData = from tanker in dsCrewMatrix.Tables[1].AsEnumerable()
                                     where tanker.Field<string>("KEYCONFIGURATION").ToLower().Trim() == "tanker certification"
                                     select tanker;
                    if (TankerData != null)
                    {
                        chklstTankerCertification.DataSource = TankerData.CopyToDataTable();
                        chklstTankerCertification.DataTextField = "PARAMETERS";
                        chklstTankerCertification.DataValueField = "ID";
                        chklstTankerCertification.DataBind();
                    }

                    var STCW = from stcw in dsCrewMatrix.Tables[1].AsEnumerable()
                               where stcw.Field<string>("KEYCONFIGURATION").ToLower().Trim() == "stcw"
                               select stcw;
                    if (STCW != null)
                    {
                        ddlSTCW.DataSource = STCW.CopyToDataTable();
                        ddlSTCW.DataTextField = "PARAMETERS";
                        ddlSTCW.DataValueField = "PARAMETERS";
                        ddlSTCW.DataBind();
                    }
                  
                    if (dsCrewMatrix.Tables[2].Rows.Count > 0)
                    {
                        string[] Tanker_Certification = Convert.ToString(dsCrewMatrix.Tables[2].Rows[0]["Tanker_Certification"]).Split('|');

                        for (int j = 0; j < Tanker_Certification.Length; j++)
                        {
                            for (int i = 0; i < chklstTankerCertification.Items.Count; i++)
                            {
                                if (Tanker_Certification[j] == Convert.ToString(chklstTankerCertification.Items[i].Value))
                                    chklstTankerCertification.Items[i].Selected = true;
                            }
                        }
                        if (dsCrewMatrix.Tables[2].Rows[0]["STCWVPara"].ToString() != "")
                            ddlSTCW.SelectedValue = dsCrewMatrix.Tables[2].Rows[0]["STCWVPara"].ToString();
                    }
                    else //Default Values
                    {
                        #region Tanker Certification
                        for (int i = 0; i < TankerData.CopyToDataTable().Rows.Count; i++)
                        {
                            if (Convert.ToString(TankerData.CopyToDataTable().Rows[i]["DEFAULTVALUE"]) == "1")
                            {
                                chklstTankerCertification.Items[i].Selected = true;
                            }
                            else
                                chklstTankerCertification.Items[i].Selected = false;
                        }
                        #endregion
                        #region STCW

                        for (int i = 0; i < STCW.CopyToDataTable().Rows.Count; i++)
                        {
                            if (Convert.ToString(STCW.CopyToDataTable().Rows[i]["DEFAULTVALUE"]) == "1")
                                ddlSTCW.Items[i].Selected = true;
                        } 
                        #endregion
                    }
                    ddlSTCW.Items.Insert(0, new ListItem() { Value = "", Text = "-Select-" });
                }
                DataTable dt = objCrewAdmin.getEnglishProficiency(GetCrewID()).Tables[0];
                if (dt.Rows.Count > 0)
                    ddlEnglishProf.SelectedValue = dt.Rows[0]["English_Proficiency"].ToString().Equals("") ? "0" : dt.Rows[0]["English_Proficiency"].ToString();

                if (dsCrewMatrix.Tables[4].Rows.Count > 0)
                {
                    ltrYearsWithOpeartor.Text = dsCrewMatrix.Tables[4].Rows[0]["YearsWithOperator"].ToString();
                    ltrYearsinRank.Text = dsCrewMatrix.Tables[4].Rows[0]["YearsOnRank"].ToString();
                    ltrYearsAllTypeTanker.Text = dsCrewMatrix.Tables[4].Rows[0]["AllTypesOfTanker"].ToString();
                    ltrWatchkeepingYears.Text = dsCrewMatrix.Tables[4].Rows[0]["YearsWatch"].ToString();

                    if (dsCrewMatrix.Tables[4].Rows[0]["CrewStatus"].ToString() == "1")
                    {
                        ltrYearsthisTypeTanker.Text = dsCrewMatrix.Tables[4].Rows[0]["ThisTypeOfTanker"].ToString();
                        ltrTourOfDuty.Text = dsCrewMatrix.Tables[4].Rows[0]["MonthsOnVessel"].ToString();
                    }
                    else
                    {
                        ltrYearsthisTypeTanker.Text = "NA";
                        ltrTourOfDuty.Text = "NA";
                    }
                }
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }
}