using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using System.IO;
using SMS.Business.PortageBill;
using System.Xml;
using System.Xml.Xsl;

using SMS.Business.Infrastructure;
using System.Globalization;
using SMS.Properties;


public partial class SurveyDashboard : System.Web.UI.Page
{
    IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");
    BLL_Infra_Company objBLL = new BLL_Infra_Company();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    BLL_Infra_Currency objBLLCurrency = new BLL_Infra_Currency();
    UserAccess objUA = new UserAccess();

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    string CompType = "VesselManager";
    //string CompType = "";
    string CompID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USERID"] == null)
            Response.Redirect("~/account/Login.aspx");
                
        UserAccessValidation();

        if (!IsPostBack)
        {
            BindCountryDLL();
            BindCurrencyDLL();

            BindCompanyDLL();
            BindCompanyTypeDLL();
            BindCompanyRelationDLL();
         
        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
        }

        if (objUA.Edit == 0)
        {
        }
        if (objUA.Delete == 0)
        {
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
    private string getQueryString(string QueryField)
    {
        try
        {
            if (Request.QueryString[QueryField] != null && Request.QueryString[QueryField].ToString() != "")
            {
                return Request.QueryString[QueryField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }


    protected void BindCountryDLL()
    {

        DataTable dt = objBLLCountry.Get_CountryList();

        ddlAddressCountry.DataSource = dt;
        ddlAddressCountry.DataTextField = "Country";
        ddlAddressCountry.DataValueField = "ID";
        ddlAddressCountry.DataBind();
        ddlAddressCountry.Items.Insert(0, new ListItem("-SELECT-", "0"));

        //ddlCountryFilter.DataSource = dt;
        //ddlCountryFilter.DataTextField = "Country";
        //ddlCountryFilter.DataValueField = "ID";
        //ddlCountryFilter.DataBind();
        //ddlCountryFilter.Items.Insert(0, new ListItem("-ALL-", "0"));


        ddlCountryIncorp.DataSource = dt;
        ddlCountryIncorp.DataTextField = "Country";
        ddlCountryIncorp.DataValueField = "ID";
        ddlCountryIncorp.DataBind();
        ddlCountryIncorp.Items.Insert(0, new ListItem("-SELECT-", "0"));


        //ddlCountryIncorpFilter.DataSource = dt;
        //ddlCountryIncorpFilter.DataTextField = "Country";
        //ddlCountryIncorpFilter.DataValueField = "ID";
        //ddlCountryIncorpFilter.DataBind();
        //ddlCountryIncorpFilter.Items.Insert(0, new ListItem("-ALL-", "0"));

    }

    protected void BindCurrencyDLL()
    {

        DataTable dt = objBLLCurrency.Get_CurrencyList();

        ddlCurrency.DataSource = dt;
        ddlCurrency.DataTextField = "Currency_Code";
        ddlCurrency.DataValueField = "Currency_ID";
        ddlCurrency.DataBind();
        ddlCurrency.Items.Insert(0, new ListItem("-SELECT-", "0"));


        //ddlCurrencyFilter.DataSource = dt;
        //ddlCurrencyFilter.DataTextField = "Currency_Code";
        //ddlCurrencyFilter.DataValueField = "Currency_ID";
        //ddlCurrencyFilter.DataBind();
        //ddlCurrencyFilter.Items.Insert(0, new ListItem("-ALL-", "0"));

    }

    protected void BindCompanyTypeDLL()
    {

        DataTable dt = objBLL.Get_CompanyTypeList();

        ddlCompanyType.Items.Clear();
        ddlCompanyType.DataSource = dt;
        ddlCompanyType.DataTextField = "Company_Type";
        ddlCompanyType.DataValueField = "ID";
        ddlCompanyType.DataBind();
        ddlCompanyType.Items.Insert(0, new ListItem("-SELECT-", "0"));

        //CompType = "VesselManager";
        if (CompType == "Surveyor")
        {
            DataTable dt1 = objBLL.Get_CompanyTypeList("Surveyor");
            if (dt1.Rows.Count > 0)
            {
                ddlCompanyType.SelectedValue = dt1.Rows[0]["ID"].ToString();
                ddlCompanyType.Enabled = false;
            }

        }
        else if (CompType == "VesselManager")
        {
            DataTable dt1 = objBLL.Get_CompanyTypeList("Vessel Manager");
            if (dt1.Rows.Count > 0)
            {
                ddlCompanyType.SelectedValue = dt1.Rows[0]["ID"].ToString();
                ddlCompanyType.Enabled = false;
            }
        }


        //ddlCompanyTypeFilter.Items.Clear();
        //ddlCompanyTypeFilter.DataSource = dt;
        //ddlCompanyTypeFilter.DataTextField = "Company_Type";
        //ddlCompanyTypeFilter.DataValueField = "ID";
        //ddlCompanyTypeFilter.DataBind();
        //ddlCompanyTypeFilter.Items.Insert(0, new ListItem("-ALL-", "0"));


    }

    protected void BindCompanyDLL()
    {
        ddlParentCompany.DataTextField = "Company_Name";
        ddlParentCompany.DataValueField = "ID";
        ddlParentCompany.DataSource = objBLL.Get_CompanyListMini(UDFLib.ConvertToInteger(Session["USERCOMPANYID"]));
        ddlParentCompany.DataBind();
        ddlParentCompany.Items.Insert(0, new ListItem("-SELECT-", "0"));

        if (CompType == "Surveyor")
        {
            ddlParentCompany.SelectedValue = "1";
            ddlParentCompany.Enabled = false;
        }
        else if (CompType == "VesselManager")
        {
            ddlParentCompany.SelectedValue = Session["USERCOMPANYID"].ToString();//CompID;
            ddlParentCompany.Enabled = false;
        }

    }

    protected void BindCompanyRelationDLL()
    {
        ddlRelation.DataTextField = "Relationship_Name";
        ddlRelation.DataValueField = "ID";
        ddlRelation.DataSource = objBLL.Get_CompanyRelationType(UDFLib.ConvertToInteger(Session["USERID"]));
        ddlRelation.DataBind();
        ddlRelation.Items.Insert(0, new ListItem("-SELECT-", "0"));

        if (CompType == "Surveyor")
        {
            ddlRelation.SelectedValue = "5";
            ddlRelation.Enabled = false;
        }
        else if (CompType == "VesselManager")
        {
            ddlRelation.SelectedValue = "5";
            ddlRelation.Enabled = false;
        }
    }
   



    

  
    protected void btnsave_Click(object sender, EventArgs e)
    {
        
        //if (HiddenFlag.Value == "Add")
        //{
        if(txtCompName.Text!="")
            txtShortName.Text = txtCompName.Text.Substring(0, 3);

        DataTable dt = objBLL.CheckCompanyExist(txtShortName.Text.Trim());

        if (dt.Rows.Count > 0)
        {
            string js1 = "alert('Company name already exists');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js1, true);
            //string AddCompmodal = String.Format("showModal('CompanyExist',false);");
            //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCompmodal", AddCompmodal, true);

        }
        else
        {
            int responseid = objBLL.InsertCompany(int.Parse(txtCompCode.Text.Trim()), int.Parse(ddlCompanyType.SelectedValue), txtCompName.Text.Trim(), txtRegNo.Text.Trim()
              , UDFLib.ConvertDateToNull(txtDtIncorp.Text.Trim()), UDFLib.ConvertToInteger(ddlCountryIncorp.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCurrency.SelectedValue), txtAddrerss.Text.Trim()
              , UDFLib.ConvertIntegerToNull(ddlAddressCountry.SelectedValue), txtEmail.Text.Trim(), txtPhone.Text.Trim()
              , UDFLib.ConvertToInteger(Session["UserID"]), txtShortName.Text, txtEmail2.Text, txtPhone2.Text, txtFax1.Text, txtFax2.Text);


            string emailid = "";
            string ContactDet = "";
            if (txtEmail2.Text != "")
            {
                emailid = txtEmail.Text.Trim() + "," + txtEmail2.Text;
            }
            else
            {
                emailid = txtEmail.Text.Trim();
            }
            if (txtPhone2.Text != "")
            {
                ContactDet = txtPhone.Text.Trim() + "," + txtPhone2.Text;
            }
            else
            {
                ContactDet = txtPhone.Text.Trim();
            }

            if (responseid != -1)
            {
                objBLL.InsertCompanyReletionship(UDFLib.ConvertToInteger(ddlParentCompany.SelectedValue), responseid, UDFLib.ConvertToInteger(ddlRelation.SelectedValue), UDFLib.ConvertToInteger(Session["UserID"]));

                string url = HttpContext.Current.Request.Url.AbsoluteUri;

                string[] segment = url.Split('/');
                string newstr = "";



                for (int i = 0; i < segment.Count(); i++)
                {
                    if (segment[i] == "JIBE")
                    {
                        newstr += segment[i].ToString() + "/";
                        break;
                    }
                    //if(i==0)
                    newstr += segment[i].ToString() + "/";
                    //else
                    //    newstr += segment[i].ToString() + "/";
                }

                newstr += "Account/Company.aspx?CompanyType=VesselManager&CompanyID="+responseid.ToString()+"";// + Session["USERCOMPANYID"] + "";

                btnsave.Enabled = false;

                string js1 = "alert('Saved Successfully');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js1, true);
               // objBLL.SendSurveyVesselORegistration(emailid, ContactDet, txtCompName.Text.Trim(), newstr);

                //divadd.Visible = false;
                //dvAddtd.Visible = false;
                //divadd.Visible = false;
                //SuccessId.Visible = true;
            }
            else
            {
                string js1 = "alert('Company name already exists');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js1, true);
            }
        }

        //}
        //else
        //{
        //    int responseid = objBLL.EditCompany(Convert.ToInt32(txtCompanyID.Text), Convert.ToInt32(txtCompCode.Text), Convert.ToInt32(ddlCompanyType.SelectedValue)
        //        , txtCompName.Text, txtShortName.Text, txtRegNo.Text, txtDtIncorp.Text, UDFLib.ConvertIntegerToNull(ddlCountryIncorp.SelectedValue), UDFLib.ConvertIntegerToNull(ddlCurrency.SelectedValue)
        //        , txtAddrerss.Text, UDFLib.ConvertIntegerToNull(ddlAddressCountry.SelectedValue), txtEmail.Text, txtPhone.Text, txtEmail2.Text, txtPhone2.Text, txtFax1.Text, txtFax2.Text, UDFLib.ConvertToInteger(Session["UserID"]));

        //}

        //BindCompany();

        //string hidemodal = String.Format("hideModal('divadd')");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

       
    }
    protected void hlSendInvitation_Click(object sender, EventArgs e)
    {
        //divadd.Visible = true;
        //dvAddtd.Visible = true;
    }
    //protected void hlRegisterdVessel_Click(object sender, EventArgs e)
    //{
    //    // Response.Redirect("~/Infrastructure/Libraries/Company.aspx");
    //    Response.Redirect("~/Infrastructure/Libraries/SearchVessel.aspx");
    //}

    //protected void hlVesselGA_Click(object sender, EventArgs e)
    //{        
    //    Response.Redirect("~/Infrastructure/Libraries/VesselGA.aspx");
    //}

    //protected void hlChecklistIndex_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Technical/Inspection/ChecklistIndex.aspx");
    //}
    //protected void hlAddInspector_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/infrastructure/libraries/userList.aspx");
    //}
    //protected void hlSheduleInspection_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Technical/Worklist/SuperintendentInspection.aspx");
    //}

    //protected void hlChecklistType_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Technical/Inspection/EvaluationLibrary.aspx");
    //}

    //protected void hlCriteria_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/Technical/Inspection/Criteria.aspx");
    //}
}