using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using System.IO;


public partial class Crew_CrewProfile : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();

    BLL_Crew_Admin objAdmin = new BLL_Crew_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        int CrewID = 0;
        if (Request.QueryString["ID"] != null)
            CrewID = UDFLib.ConvertToInteger(Request.QueryString["ID"].ToString());


        if (!IsPostBack)
        {
            if (CrewID > 0)
            {
                Load_CrewPersonalDetails(CrewID);
                Load_CrewPreviousContactDetails(CrewID);
                Load_PassportAndSeamanDetails(CrewID);
                Load_Next_Of_Kin(CrewID);
                Bind_ChangeEvent();
                getConfigurationDetails();
            }
        }
    }

    protected void Load_CrewPersonalDetails(int ID)
    {

        DataTable dt = objBLLCrew.Get_CrewPersonalDetailsByID(ID);
        if (dt.Rows.Count > 0)
        {

            lblSurname.Text = dt.Rows[0]["Staff_Surname"].ToString();
            lblGivenName.Text = dt.Rows[0]["Staff_Name"].ToString();
            lblAlias.Text = dt.Rows[0]["Alias"].ToString();

            if (dt.Rows[0]["Staff_Birth_Date"].ToString() != "")
                lblDateOfBirth.Text = DateTime.Parse(dt.Rows[0]["Staff_Birth_Date"].ToString()).ToString(UDFLib.GetDateFormat());

            lblPlaceOfBirth.Text = dt.Rows[0]["Staff_Born_Place"].ToString();
            lblNationality.Text = dt.Rows[0]["Country_Name"].ToString();
            lblMaritalStatus.Text = dt.Rows[0]["MaritalStatus"].ToString();
            lblTelephone.Text = dt.Rows[0]["Telephone"].ToString();
            lblMobile.Text = dt.Rows[0]["Mobile"].ToString();
            lblAddress.Text = dt.Rows[0]["Address"].ToString();
            lblFax.Text = dt.Rows[0]["Fax"].ToString();
            lblEMail.Text = dt.Rows[0]["EMail"].ToString();
            lblIntlAirport.Text = dt.Rows[0]["NearestAirport"].ToString();
            lblAL1.Text = dt.Rows[0]["AddressLine1"].ToString();
            lblAL2.Text = dt.Rows[0]["AddressLine2"].ToString();
            lblPassport_No.Text = dt.Rows[0]["Passport_Number"].ToString();
            lblPassport_Place.Text = dt.Rows[0]["Passport_PlaceOf_Issue"].ToString();
           
            if (dt.Rows[0]["Passport_Issue_Date"].ToString() != "")
                lblPassport_IssueDt.Text = DateTime.Parse(dt.Rows[0]["Passport_Issue_Date"].ToString()).ToString(UDFLib.GetDateFormat());

            if (dt.Rows[0]["Passport_Expiry_Date"].ToString() != "")
                lblPassport_ExpDt.Text = DateTime.Parse(dt.Rows[0]["Passport_Expiry_Date"].ToString()).ToString(UDFLib.GetDateFormat());


            lblSeamanBk_No.Text = dt.Rows[0]["Seaman_Book_Number"].ToString();
            lblSeamanBk_Place.Text = dt.Rows[0]["Seaman_Book_PlaceOf_Issue"].ToString();

            if (dt.Rows[0]["Seaman_Book_Issue_Date"].ToString() != "")
                lblSeamanBk_IssueDt.Text = DateTime.Parse(dt.Rows[0]["Seaman_Book_Issue_Date"].ToString()).ToString(UDFLib.GetDateFormat());

            if (dt.Rows[0]["Seaman_Book_Expiry_Date"].ToString() != "")
                lblSeamanBk_ExpDt.Text = DateTime.Parse(dt.Rows[0]["Seaman_Book_Expiry_Date"].ToString()).ToString(UDFLib.GetDateFormat());


            if (dt.Rows[0]["PhotoURL"].ToString() != "")
            {
                if (File.Exists(Server.MapPath("~/Uploads/CrewImages/" + dt.Rows[0]["PhotoURL"].ToString())))
                {
                    imgCrewPic.ImageUrl = "~/Uploads/CrewImages/" + dt.Rows[0]["PhotoURL"].ToString();
                    imgCrewPic.Visible = true;
                    imgNoPic.Visible = false;
                }
                else
                {
                    imgCrewPic.Visible = false;
                    imgNoPic.Visible = true;
                }
            }
            else
            {
                imgCrewPic.Visible = false;
                imgNoPic.Visible = true;
            }

            lblMultinationalcrew.Text = (dt.Rows[0]["Multinationalcrew"].ToString() == "1") ? "Yes" : "No";
            lblNationalities.Text = dt.Rows[0]["MultinationalcrewNationalities"].ToString();
            this.Title = "Staff Profile: " + dt.Rows[0]["Staff_Surname"].ToString() + " " + dt.Rows[0]["Staff_Name"].ToString();
            lblICountry.Text = dt.Rows[0]["Country_Name"].ToString();
        }

    }

    protected void Load_CrewPreviousContactDetails(int CrewID)
    {
        DataTable dt = objBLLCrew.Get_CrewPreviousContactDetails(CrewID);
        GridView_PreviousContacts.DataSource = dt;
        GridView_PreviousContacts.DataBind();

    }
    protected void Load_PassportAndSeamanDetails(int CrewID)
    {
        DataTable dt = objBLLCrew.Get_CrewPassportAndSeamanDetails(CrewID);
        if (dt.Rows.Count > 0)
        {
            lblPassport_No.Text = dt.Rows[0]["Passport_Number"].ToString();
            lblPassport_Place.Text = dt.Rows[0]["Passport_PlaceOf_Issue"].ToString();

            if (dt.Rows[0]["Passport_Issue_Date"].ToString() != "")
                lblPassport_IssueDt.Text = DateTime.Parse(dt.Rows[0]["Passport_Issue_Date"].ToString()).ToString(UDFLib.GetDateFormat());

            if (dt.Rows[0]["Passport_Expiry_Date"].ToString() != "")
                lblPassport_ExpDt.Text = DateTime.Parse(dt.Rows[0]["Passport_Expiry_Date"].ToString()).ToString(UDFLib.GetDateFormat());


            lblSeamanBk_No.Text = dt.Rows[0]["Seaman_Book_Number"].ToString();
            lblSeamanBk_Place.Text = dt.Rows[0]["Seaman_Book_PlaceOf_Issue"].ToString();

            if (dt.Rows[0]["Seaman_Book_Issue_Date"].ToString() != "")
                lblSeamanBk_IssueDt.Text = DateTime.Parse(dt.Rows[0]["Seaman_Book_Issue_Date"].ToString()).ToString(UDFLib.GetDateFormat());

            if (dt.Rows[0]["Seaman_Book_Expiry_Date"].ToString() != "")
                lblSeamanBk_ExpDt.Text = DateTime.Parse(dt.Rows[0]["Seaman_Book_Expiry_Date"].ToString()).ToString(UDFLib.GetDateFormat());
        }

    }
    protected void Load_Next_Of_Kin(int CrewID)
    {
        DataTable dtNOK = objBLLCrew.Get_Crew_DependentsByCrewID(CrewID, 1);
        GridView_Dependents.DataBind();
        if (dtNOK.Rows.Count > 0)
        {
            lblNOKName.Text = dtNOK.Rows[0]["FirstName"].ToString().ToUpper();
            lblNOKSurname.Text = dtNOK.Rows[0]["Surname"].ToString().ToUpper();
            lblNOKDOB.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dtNOK.Rows[0]["DOB"]));
            lblNOKrelationship.Text = dtNOK.Rows[0]["Relationship"].ToString();
            lblNOKAddress.Text = dtNOK.Rows[0]["Address"].ToString();
            lblNOKCountry.Text = dtNOK.Rows[0]["Country"].ToString().ToUpper();
            lblNOKPhone.Text = dtNOK.Rows[0]["Phone"].ToString();
            lblA1.Text = dtNOK.Rows[0]["Address1"].ToString();
            lblA2.Text = dtNOK.Rows[0]["Address2"].ToString();
            lblNOKSSN.Text =  dtNOK.Rows[0]["SSN"].ToString();
        }

        DataTable dtAddress = null;
        if (objAdmin.CRW_GetCDConfiguration("Addressformat").Tables.Count > 0)
        {
            dtAddress = objAdmin.CRW_GetCDConfiguration("Addressformat").Tables[0];
        }
        if (dtAddress.Rows.Count > 0)
        {
            if (dtAddress.Rows[0]["Value"].ToString() == "True")
            {
                tdA1.Visible = false;
                tdA2.Visible = false;
                tdA3.Visible = false;
                tdA4.Visible = false;
                tdIA1.Visible = true;
                tdIA2.Visible = true;
                trUSADDRESS.Visible = false;
                tdIPA1.Visible = true;
                tdIPA2.Visible = true;
                trCountry.Visible = false;
                tdCountry1.Visible = false;
                tdCountry2.Visible = false;
            }
            else
            {
                tdA1.Visible = true;
                tdA2.Visible = true;
                tdA3.Visible = true;
                tdA4.Visible = true;
                tdIA1.Visible = false;
                tdIA2.Visible = false;
                trUSADDRESS.Visible = true;
                tdIPA1.Visible = false;
                tdIPA2.Visible = false;
                trCountry.Visible = true;
                tdCountry1.Visible = true;
                tdCountry2.Visible = true;
            }
        }
    }
    protected void Bind_ChangeEvent()
    {
        int CrewID = GetCrewID();


        DataSet ds = objBLLCrew.Get_CrewChangeEvents_ByCrew(CrewID, GetSessionUserID());
        UDFLib.AddParentTable(ds.Tables[0], "Events", new string[] { "PKID" },
        new string[] { "Vessel_short_name", "Event_Date", "Port_Name" }, "EventMembers");


        rpt1.DataSource = ds;
        rpt1.DataMember = "Events";
        rpt1.DataBind();

    }
    public int GetCrewID()
    {
        try
        {
            if (Request.QueryString["ID"] != null)
            {
                return int.Parse(Request.QueryString["ID"].ToString());
            }
            else
                return 0;
        }
        catch { return 0; }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public void getConfigurationDetails()
    {
        string str = "";
        DataRow[] dr;
        DataTable dt = objAdmin.CRW_GetCDConfiguration(null).Tables[0];
        if (dt.Rows.Count > 0)
        {


            str = "SSN";
            dr = dt.Select("Key ='" + str + "'");
            if (dr.Length > 0)
            {
                if (dr[0].ItemArray[3].ToString() == "True" )
                {
                    tdSSN1.Visible = true;
                    tdSSN2.Visible = true;
                }
                else
                {
                    tdSSN1.Visible = false;
                    tdSSN2.Visible = false;
                }
            }

        }
    }
}