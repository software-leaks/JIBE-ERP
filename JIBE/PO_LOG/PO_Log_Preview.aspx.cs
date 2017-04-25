using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.POLOG;
using System.IO;
using SMS.Properties;
using System.Text;
using EO.Pdf;
using System.Drawing;

public partial class PO_LOG_PO_Log_Preview : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtSupplyID.Text = GetSupplyID();
            BindPODetails();
        }
    }
    //check and Get PO Type from Request Query String
    public string GetPOType()
    {
        try
        {
            if (Request.QueryString["POType"] != null)
            {
                return Request.QueryString["POType"].ToString();
            }

            else
                return "";
        }
        catch { return ""; }
    }
    //check and Get UserID from Session
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    //check and Get PO Code from Request Query String
    public string GetSupplyID()
    {
        try
        {

            if (Request.QueryString["SUPPLY_ID"] != null)
            {
                return Request.QueryString["SUPPLY_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    //Bind Record for PO Preview
    protected void BindPODetails()
    {
        try
        {
            string Manager_Name = null;
            string Manager_Code = null;
            string Manager_Short_Name = null;
            DataSet ds = BLL_POLOG_Register.POLOG_Get_PO_Preview(UDFLib.ConvertIntegerToNull(txtSupplyID.Text.ToString()), null, UDFLib.ConvertIntegerToNull(GetSessionUserID()));



            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr["Req_Type"].ToString() == "OXP")
                {
                    lblPOMsg.Visible = true;
                    pnl1.Visible = false;
                }
                else
                {
                    lblPOMsg.Visible = false;
                    pnl1.Visible = true;
                }

                //Imgheader.ImageUrl = dr["ImagePath"].ToString();
                //lblCName.Text = dr["Company_Name"].ToString();
                //lblCAddress.Text = dr["Company_Address"].ToString();

                lblIssueToName.Text = dr["Supplier_Name"].ToString();
                lblIssueToAddress.Text = dr["Address"].ToString();

                lblIssueByname.Text = dr["Company_Name"].ToString();
                lblIssueByAddress.Text = dr["Company_Address"].ToString();

                Imgheader.ImageUrl = "~/Images/" + dr["ImagePath"].ToString();
                lblCName.Text = dr["Company_Name"].ToString();
                lblCAddress.Text = dr["Company_Address"].ToString();

                //if (dr["Req_Type"].ToString() == "JIB")
                //{
                //    Imgheader.ImageUrl = "~/Images/jibe_logo_Small.JPG";
                //    lblCName.Text = "JIBE Pte Ltd";
                //    lblCAddress.Text = "79, SOUTH BRIDGE ROAD, Singapore (058709)";
                //}
                //else if (dr["Req_Type"].ToString() == "DEC")
                //{
                //    Imgheader.ImageUrl = "~/Images/Decatur_Logo1.JPG";
                //    lblCName.Text = "DECATUR SECURITY LTD";
                //    lblCAddress.Text = "79, SOUTH BRIDGE ROAD, Singapore (058709)";
                //}
                //else if (dr["Req_Type"].ToString() == "SPJ")
                //{
                //    Imgheader.ImageUrl = "~/Images/SeaChange_Logo1.jpg";
                //    lblCName.Text = "SeaChange Projects LLC";
                //    lblCAddress.Text = "";
                //}
                //else
                //{
                //    Imgheader.ImageUrl = "~/Images/SeaChange_Logo1.jpg";
                //    lblCName.Text = "SEACHANGE Maritime (Singapore) Pte Ltd";
                //    lblCAddress.Text = "Reg. CR No.  200902692G 79 South Bridge Road Singapore 058709.<br>Mail: purchase@seachange-maritime.com";
                //}
                if (dr["Hide_PO_Value"].ToString() == "")
                {
                    lblHeader.Text = "PURCHASE ORDER";
                    lblPICName.Text = "";
                    lblPICMobile.Text = "";
                    ViewState["Hide_PO_Value"] = "No";
                }
                else
                {
                    lblHeader.Text = "SERVICE ORDER";
                    lblPICName.Text = dr["PICName"].ToString();
                    lblPICMobile.Text = dr["ContactNo"].ToString();
                    lbl1.Visible = true;
                    lbl2.Visible = true;
                    lbl3.Visible = true;
                    lblManagerCode5.Visible = true;
                    lblManagerCode6.Visible = true;
                    ViewState["Hide_PO_Value"] = "Yes";

                }

                Manager_Name = dr["Owner_Name"].ToString();
                Manager_Code = dr["Owner_Short_Code"].ToString();
                Manager_Short_Name = dr["Owner_Short_Name"].ToString();

                //if (dr["PO_Owner_Code"].ToString() == "001834")
                //{
                //    Manager_Name = "Decatur Security Ltd";
                //    Manager_Code = "Decatur";
                //    Manager_Short_Name = "Decatur";
                //}
                //else if (dr["PO_Owner_Code"].ToString() == "002054" || dr["PO_Owner_Code"].ToString() == "002065")
                //{
                //    Manager_Name = "JIBE PTE LTD";
                //    Manager_Code = "JIBE";
                //    Manager_Short_Name = "JIBE";
                //}
                //else
                //{
                //    Manager_Short_Name = "SeaChange";
                //    Manager_Code = "SEACHANGE";
                //    Manager_Name = "SeaChange Maritime (Singapore) Pte. Ltd.";
                //}




                lblManagerName.Text = Manager_Name;
                lblManagerCode.Text = Manager_Code;
                lblManagerCode1.Text = Manager_Code;
                lblManagerCode2.Text = Manager_Code;
                lblManagerCode3.Text = Manager_Code;
                lblManagerCode4.Text = Manager_Code;
                lblManagerCode5.Text = Manager_Code;
                lblManagerCode6.Text = Manager_Code;

                lblPaymentTermsDays.Text = dr["Payment_Terms"].ToString();
              
               
                lblPOCode.Text = dr["Office_Ref_Code"].ToString();
                lblCurrency.Text = dr["Line_Currency"].ToString();
                lblPODate.Text = dr["Line_Date"].ToString();
                lblShipRef.Text = dr["Ship_Ref_Code"].ToString();
                lblSupplierRef.Text = dr["Supplier_Ref_Code"].ToString();
                lblTypeExp.Text = dr["Office_Ref_Code"].ToString();
                lblDeliveryPort.Text = dr["Req_Port"].ToString();
                lblVesselETAETD.Text = dr["VESSEL_ETA_ETD"].ToString();
                lblUrgency.Text = dr["URGENCY"].ToString();
                lblVesselName.Text = dr["Vessel_Display_Name"].ToString();


                StringBuilder sb = new StringBuilder();
                sb.Append("<table Style=\"Font-Family:Tahoma;\" Cellpadding=5 Cellspacing=1 border=1 width= 100%\">");
                sb.Append("<tr Style=\"Font-Size:14px;font-weight:bold;\" Bgcolor=\"E8E8E8\">");
                sb.Append("<td Style=\"Text-Align:center;\">No</td>");
                sb.Append("<td Style=\"Text-Align:center;\">Item Description</td>");
                sb.Append("<td Style=\"Text-Align:center;\">Part Number</td>");
                if (ViewState["Hide_PO_Value"] == "No")
                {
                    sb.Append("<td Style=\"Text-Align:center;\">Unit</td>");
                    sb.Append("<td Style=\"Text-Align:center;\">Quantity</td>");
                    sb.Append("<td Style=\"Text-Align:center;\">Unit Price</td>");
                    sb.Append("<td Style=\"Text-Align:center;\">Discount</td>");
                    sb.Append("<td>Total ( " + ds.Tables[0].Rows[0]["Line_Currency"].ToString() + " )</td>");
                }
                sb.Append("</tr>");
               
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                        sb.Append("<tr Style=\"Font-Size:11px;\">");
                        sb.Append("<td Style=\"Text-Align:Center;width: 30px;\">" + ds.Tables[1].Rows[i]["ID"].ToString() + "</td>");
                        sb.Append("<td Style=\"Text-Align:Left;width: 300px;\">" + ds.Tables[1].Rows[i]["Item_Long_Desc"].ToString() + "</td>");
                        sb.Append("<td Style=\"Text-Align:Center;width: 200px;\">" + ds.Tables[1].Rows[i]["PartNumber"].ToString() + "</td>");
                        if (ViewState["Hide_PO_Value"] == "No")
                        {
                            sb.Append("<td Style=\"Text-Align:Center;width: 80px;\">" + ds.Tables[1].Rows[i]["Item_Unit"].ToString() + "</td>");
                            sb.Append("<td Style=\"Text-Align:Right;width: 80px;\">" + ds.Tables[1].Rows[i]["Order_Qty"].ToString() + "</td>");
                            sb.Append("<td Style=\"Text-Align:Right;width: 80px;\">" + ds.Tables[1].Rows[i]["Order_Price"].ToString() + "</td>");
                            sb.Append("<td Style=\"Text-Align:Right;width: 80px;\">" + ds.Tables[1].Rows[i]["Item_Discount"].ToString() + "</td>");
                            sb.Append("<td Style=\"Text-Align:Right;width: 100px;\">" + ds.Tables[1].Rows[i]["TotalAmount"].ToString() + "</td>");
                        }
                        sb.Append("</tr>");
                }
                if (ViewState["Hide_PO_Value"] == "No")
                {
                    sb.Append("<tr Style=\"Font-Size:14px;font-weight:bold;\" Bgcolor=\"E8E8E8\">");
                    sb.Append("<td Style=\"Text-Align:right;\" Colspan=7>Total Price ( " + ds.Tables[0].Rows[0]["Line_Currency"].ToString() + " ) </td>");
                    sb.Append("<td Style=\"Text-Align:right;\" >" + ds.Tables[0].Rows[0]["TotalAmount"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr Style=\"Font-Size:14px;font-weight:bold;\" Bgcolor=\"E8E8E8\">");
                    sb.Append("<td Style=\"Text-Align:right;\" Colspan=7>Less Discount : ( " + ds.Tables[0].Rows[0]["Discount_Amount"].ToString() + " " + ds.Tables[0].Rows[0]["Discount_Type"].ToString() + " ) on Total Price (" + ds.Tables[0].Rows[0]["Line_Currency"].ToString() + ")  </td>");
                    sb.Append("<td Style=\"Text-Align:right;\" >" + ds.Tables[0].Rows[0]["Discount_Total"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr Style=\"Font-Size:14px;font-weight:bold;\" Bgcolor=\"E8E8E8\">");
                    sb.Append("<td Style=\"Text-Align:right;\" Colspan=7>Add Logistic Cost </td>");
                    sb.Append("<td Style=\"Text-Align:right;\">" + ds.Tables[0].Rows[0]["Logistic_Cost"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr Style=\"Font-Size:14px;font-weight:bold;\" Bgcolor=\"E8E8E8\">");
                    sb.Append("<td Style=\"Text-Align:right;\" Colspan=7>Net Amount in " + ds.Tables[0].Rows[0]["Line_Currency"].ToString() + " </td>");
                    sb.Append("<td Style=\"Text-Align:right;\" >" + ds.Tables[0].Rows[0]["Net_Amount"].ToString() + "</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                ltPODetails.Text = sb.ToString();


            }
        }
        catch { }
        {
        }
    }

    
}