using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.VM;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;
using System.Globalization;

public partial class VesselMovement_Port_Cost : System.Web.UI.Page
{
    public UserAccess objUA = new UserAccess();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_VM_PortCall objPortCall = new BLL_VM_PortCall();
    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
    DataTable dtVessel;
    public string DateFormat = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //This change has been done to change the date format as per user selection
        CalStartDate.Format= Convert.ToString(Session["User_DateFormat"]);
        CalEndDate.Format = Convert.ToString(Session["User_DateFormat"]);
        //dateValidator.ValueToCompare = DateTime.Now.ToShortDateString();
        
        DateFormat = UDFLib.GetDateFormat();//Get User date format
        UserAccessValidation();
        if (!IsPostBack)
        {
            DateTime dt = System.DateTime.Now;
            DateTime Firstdaydate = dt.AddDays(-(dt.Day - 1));

            DateTime LastdayDate = dt.AddMonths(1);
            LastdayDate = LastdayDate.AddDays(-(LastdayDate.Day));
            //This change has been done to change the date format as per user selection
            txtStartDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(Firstdaydate));
            txtEndDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(LastdayDate));
            //txtStartDate.Text = Firstdaydate.ToString("dd-MM-yyyy");
            //txtEndDate.Text = LastdayDate.ToString("dd-MM-yyyy");
            Load_VesselList();
            Load_PortList();
            BindPortCostList();
                
        }
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
    public void Load_VesselList()
    {
        if (Session["sFleet"] == null)
        {
            DataTable FleetDT = objBLL.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            FleetDT.Columns.RemoveAt(1);
            FleetDT.Columns.RemoveAt(1);
            FleetDT.Columns.RemoveAt(1);
            FleetDT.Columns.RemoveAt(1);
            FleetDT.Columns.RemoveAt(1);
            FleetDT.Columns.RemoveAt(1);
            FleetDT.Columns.RemoveAt(1);
            Session["sFleet"] = FleetDT;
        }

        DataTable dt = objPortCall.Get_PortCall_VesselList((DataTable)Session["sFleet"], 0, 0, "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
       
        ddlvessel.DataSource = dt;
        ddlvessel.DataTextField = "VESSEL_NAME";
        ddlvessel.DataValueField = "VESSEL_ID";
        ddlvessel.DataBind();
        ddlvessel.Items.Insert(0, new ListItem("--Select--", "0"));

    }

    public int GetVesselID()
    {
        try
        {

            if (Session["Vessel_ID"] != null)
            {
                return int.Parse(Session["Vessel_ID"].ToString());
            }

            else
                return 0;
        }
        catch { return 0; }
    }
    public int GetPort_ID()
    {
        try
        {

            if (Session["Port_ID"] != null)
            {
                return int.Parse(Session["Port_ID"].ToString());
            }

            else
                return 0;
        }
        catch { return 0; }
    }
    public void Load_PortList()
    {

        DataTable dt = objPortCall.Get_PortCall_PortList(0, UDFLib.ConvertIntegerToNull(GetVesselID()),0);
        ddlPortCost.DataSource = dt;
        ddlPortCost.DataTextField = "Port_Name";
        ddlPortCost.DataValueField = "Port_ID";
        ddlPortCost.DataBind();
        if (ddlPortCost.Items.FindByText("SINGAPORE") != null)
            ddlPortCost.SelectedValue = ddlPortCost.Items.FindByText("SINGAPORE").Value;
    }
    protected void BindPortCostList()
    
    {
        try
        {
            string sVessel_Id = "";
            DateTime dtNow = System.DateTime.Now;
            dtVessel = new DataTable();
            dtVessel.Columns.Add("Vessel_Code");
            if (ddlvessel.SelectedIndex > 0)
            {

                DataRow dr = dtVessel.NewRow();
                dr[0] = ddlvessel.SelectedValue;
                dtVessel.Rows.Add(dr);
            }
            else
            {
                foreach (ListItem lt in ddlvessel.Items)
                {
                    if (lt.Value != "0")
                    {
                        DataRow dr = dtVessel.NewRow();
                        dr[0] = lt.Value;
                        dtVessel.Rows.Add(dr);
                    }
                }


            }

  DateTime dateTime;

  if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
  {
      //This change has been done to validate the date format as per user selection
      if (DateTime.TryParseExact(txtStartDate.Text, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime) && DateTime.TryParseExact(txtEndDate.Text, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
      {
          // Format your DateTime for valid date time error

          var from = UDFLib.ConvertToDefaultDt(txtStartDate.Text);
          var To = UDFLib.ConvertToDefaultDt(txtEndDate.Text);



          DateTime Startdate = Convert.ToDateTime(from);
          DateTime EndDate = Convert.ToDateTime(To);


          DataSet ds = objPortCall.Get_PortCall_PortCost(UDFLib.ConvertIntegerToNull(ddlPortCost.SelectedValue), dtVessel, Startdate, EndDate);
          //if (ds.Tables[0].Rows.Count > 0)
          //{
          //    gvPortCost.DataSource = ds;
          //    gvPortCost.DataBind();
          //}
          //else
          gvDAItem.DataSource = null;
          gvDAItem.DataBind();
          gvDAItem.EmptyDataText = "";
          {
              gvPortCost.DataSource = ds;
              gvPortCost.DataBind();

          }
      }
  }
        }
        catch(Exception ex)
        {
        }
    }
    protected void gvPortCost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnDAEdit = (ImageButton)e.Row.FindControl("lbtnDAEdit");
                Label lAgent = (Label)e.Row.FindControl("lblAgent");
                if (lAgent.Text != "")
                {
                    btnDAEdit.Visible = true;
                }
                else
                {
                    btnDAEdit.Visible = false;
                }
            }

        }
        catch { }
    }
    protected void lbtnDAEdit_Click(object s, CommandEventArgs e)
    {
        try
        {
            string[] arg = e.CommandArgument.ToString().Split(',');
            int? DA_ID = UDFLib.ConvertIntegerToNull(arg[0]);
            //ViewState["DA_ID"] = UDFLib.ConvertIntegerToNull(arg[0]);
            //ViewState["Agent_Code"] = UDFLib.ConvertIntegerToNull(arg[1]);
            DataSet ds = objPortCall.Get_PortCall_DAItem(arg[0], arg[1], UDFLib.ConvertStringToNull(ddlPortCost.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {

                gvDAItem.DataSource = ds;
                gvDAItem.DataBind();
            }
            
        }
        catch (Exception ex)
        {
            string err = ex.ToString();
        }
        //BindActivity();

    }
    protected void ImgPortCost_Click(object s, EventArgs e)
    {
        BindPortCostList();

    }
    double TotalPAmount = 0.00;
    double TotalFamount = 0.00;
    protected void gvDAItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
            // check row type
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPamount = (Label)e.Row.FindControl("lblPamount");
                Label lblFAmount = (Label)e.Row.FindControl("lblFAmount");
                TotalPAmount += Convert.ToDouble(lblPamount.Text);
                TotalFamount += Convert.ToDouble(lblFAmount.Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbtotalProformaAmt = (Label)e.Row.FindControl("lbtotalProformaAmt");
                Label lbtotalFinalAmt = (Label)e.Row.FindControl("lbtotalFinalAmt");
                lbtotalProformaAmt.Text = TotalPAmount.ToString("0.00");
                lbtotalFinalAmt.Text = TotalFamount.ToString("0.00");
            }

        
    }
    protected void DatesValidator_Validate(object source, ServerValidateEventArgs args)
    {
        DateTime dateTime;
        // Format your DateTime for valid date time error
        if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
        {
            if (System.DateTime.TryParseExact(txtStartDate.Text, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime) && DateTime.TryParseExact(txtEndDate.Text, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {

                var from = UDFLib.ConvertToDefaultDt(txtStartDate.Text);
                var To = UDFLib.ConvertToDefaultDt(txtEndDate.Text);

                DateTime Startdate = Convert.ToDateTime(from);
                DateTime EndDate = Convert.ToDateTime(To);

                if (EndDate < Startdate)
                {
                    args.IsValid = false;
                }
            }
        }
  

    }
}