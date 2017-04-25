using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.CP;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class CP_Hire_Invoice_Print : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public int Inv_ID = 0;
    public int CPID = 0;
    public double Address_Comm = 0.00;
    public double TotalDebit = 0.00;
    public double TotalCredit = 0.00;
    public bool Address_Comm_Display = false;
    public string Bank_Account_ID = "";
    public double Displaytotal = 0.00;
    

    public string Item_Group = "";
    public string HireInvNo = "";
    public double Address_Comm_Amount = 0.00;
    public int PortId = 0;
    public string VesselName = "";
    public string OType = "";
    public Boolean uaEditFlag = true;//Test default true
    public Boolean uaDeleteFlage = true;
    BLL_CP_CharterParty objCP = new BLL_CP_CharterParty();
    BLL_Infra_Company objCompany= new BLL_Infra_Company();
    BLL_CP_HireInvoice objHireInv = new BLL_CP_HireInvoice();
    BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();
    protected void Page_Load(object sender, EventArgs e)
    {
        // UserAccessValidation();
        if (!IsPostBack)
        {
            if (Session["CPID"] != null)
            {
                CPID = Convert.ToInt32(Session["CPID"]);
                BindInvoiceHead();
                BindCompanyDetails();
                BindCPdDetails();
                BindInvDetails();
            }


        }
    }


    protected void BindCPdDetails()
    {
        DataTable dt = objCP.GET_Charter_Party_Details(UDFLib.ConvertIntegerToNull(CPID));
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            lblOwner.Text = dr["Owner"].ToString();
            ltCharterer.Text = "TO :" + dr["Charterer"].ToString();
            VesselName = dr["Vessel_Name"].ToString();
            ltVesselCharterer.Text = dr["Vessel_Name"].ToString() + "/" + dr["ChartererShort"].ToString();
            DateTime dtCPDate = Convert.ToDateTime(dr["CP_Date"]);
            ltCPdate.Text = dtCPDate.ToString("dd-MMM-yyyy");
            Bank_Account_ID = dr["Bank_Account_ID"].ToString();
        }

    }
    protected void BindInvoiceHead()
    {
        DataTable dt = objHireInv.Get_Hire_InvDetail(UDFLib.ConvertIntegerToNull(Session["Inv_ID"]));
        if (dt!= null && dt.Rows.Count > 0)
        {
            Item_Group = "xxxx";
            ltInvRef.Text = dt.Rows[0]["InvoiceRef"].ToString();
            HireInvNo = dt.Rows[0]["Hire_Invoice_No"].ToString();
            ltDueDate.Text = dt.Rows[0]["Due_Date"].ToString();
            Address_Comm = Convert.ToDouble(dt.Rows[0]["Address_Comm"]);
            Address_Comm_Display = false;
        }
    }
    protected void BindCompanyDetails()
    {
        DataTable dt = new DataTable();
        dt = objCompany.Get_CompanyList();
        dt.DefaultView.RowFilter = "ID= '" + Session["USERCOMPANYID"].ToString() + "'";

        lblCompanyAddress.Text = dt.DefaultView[0]["Address"].ToString();
    }


    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void BindInvDetails()
    {
        StringBuilder sb = new StringBuilder();
        double AddCommBase = 0.0;

        try
        {
            DataTable dt = objHireInv.GET_Hire_Invoice_Items(UDFLib.ConvertIntegerToNull(Session["CPID"]), UDFLib.ConvertIntegerToNull(Session["Inv_ID"]));

            Address_Comm = Convert.ToDouble(dt.Rows[0]["Address_CommPerc"]);
   
            sb.Append("<table>");
            foreach (DataRow dr in dt.Rows)
            {

                if (Item_Group != dr["Item_Group"].ToString())
                {
                    sb.Append("<tr><td><font Size=3><b><u>" + dr["Item_Group"].ToString() + "</u></b></td></tr>");
                }

                sb.Append("<tr valign=\"top\"><td style=\"Width:460px;Text-Align:Left\">" + dr["Item_Name"].ToString() + "<br>" + dr["Item_Details"].ToString() + "</br></br></td> ");

                Displaytotal = Convert.ToDouble(dr["DisplayTotal"]);
                if (Convert.ToDouble(dr["Item_Total"]) > 0)
                {
                    TotalDebit = TotalDebit + Displaytotal;
                }
                else
                {
                    TotalCredit = TotalCredit + Displaytotal;
                }




                // Displaytotal = Convert.ToDouble(dr["DisplayTotal"]);
                if (Convert.ToDouble(dr["Item_Total"]) > 0)
                {
                    //TotalDebit = TotalDebit + Displaytotal;
                    sb.Append("<td style=\"Width:80px;Text-Align:Right;font-weight:bold\">" + Displaytotal.ToString("0.00") + "</td>");
                    sb.Append("<td style=\"Width:80px;Text-Align:Right;\">&nbsp;</td>");
                }
                else
                {
                    //TotalCredit = TotalCredit + Displaytotal;
                    sb.Append("<td style=\"Width:80px;Text-Align:Right;Text-Weight:Bold\">&nbsp;</td>");
                    sb.Append("<td style=\"Width:80px;Text-Align:Right;font-weight:bold\">" + Displaytotal.ToString("0.00") + "</td>");

                }


                if (Address_Comm_Display == false)
                {
                    if (Address_Comm > 0)
                    {
                        // DataTable dt1 = objHireInv.GET_Hire_Invoice_Items(UDFLib.ConvertIntegerToNull(Session["CPID"]), UDFLib.ConvertIntegerToNull(Session["Inv_ID"]));


                        AddCommBase = Convert.ToDouble(dr["AddComm"]);
                        if (AddCommBase != 0)
                        {
                            Address_Comm_Amount = AddCommBase * Address_Comm / 100;
                            TotalCredit = TotalCredit + Address_Comm_Amount;
                        }

                    }

                    else
                        Address_Comm_Amount = 0.00;

                    Address_Comm_Display = true;

                    if (Address_Comm_Amount != 0)
                    {

                        sb.Append("<tr><td><font size=\"3\"> <b><u> Address Commission </u></b></font></td></tr>");
                        sb.Append("<tr><td> Address Commission " + Address_Comm.ToString() + " % On US$ " + AddCommBase.ToString() + "</br></br</td>");
                        sb.Append("<td style=\"width:80px;Text-Align:Right;\">&nbsp;</td>");
                        sb.Append("<td style=\"width:80px;Text-Align:Right;Text-Weight:Bold\"> <b>" + Address_Comm_Amount.ToString("0.00") + " </b></td></tr>");
                    }
                }

            }


            sb.Append("</table>");


            ltItemcontents.Text = sb.ToString();

            if (TotalCredit > TotalDebit)
            {
                lblRemarks.Text = "Total Amount due in favor of Charterers US$";
                ltAmountDue_C.Text = Convert.ToDouble(TotalCredit - TotalDebit).ToString("0.00");
                ltAmountDue_O.Text = "";
            }
            else if (TotalDebit > TotalCredit)
            {
                lblRemarks.Text = "Total Amount due in favor of Owners US$";
                ltAmountDue_O.Text = Convert.ToDouble(TotalDebit - TotalCredit).ToString("0.00");
                ltAmountDue_C.Text = "";
            }
            else if (TotalDebit == TotalCredit)
            {
                lblRemarks.Text = "No outstanding due.";
                ltAmountDue_O.Text = "0.00";
                ltAmountDue_C.Text = "";
            }



            DataTable dtBankDetails = objHireInv.GetOwnerBank_Deatils(Bank_Account_ID);

            if (dtBankDetails.Rows.Count > 0)
            {
                ltBank.Text = dtBankDetails.Rows[0]["Bank_Name"].ToString();
                ltSwift.Text = dtBankDetails.Rows[0]["SWIFT"].ToString();
                ltCredit.Text = dtBankDetails.Rows[0]["BENEFICIARY_NAME"].ToString();
                ltAccNO.Text = dtBankDetails.Rows[0]["Account_No"].ToString();
                ltIBAN.Text = dtBankDetails.Rows[0]["IBAN_ABA"].ToString();
                ltVesselInv.Text = VesselName + " / " + HireInvNo;

            }

        }
        catch (Exception ex)
        {
            string Error = ex.ToString();
        }
    }




}

   
