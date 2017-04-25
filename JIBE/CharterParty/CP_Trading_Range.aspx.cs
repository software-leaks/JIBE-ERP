using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;

using Telerik.Web.UI;

//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.CP;
using SMS.Properties;

public partial class CP_Trading_Range : System.Web.UI.Page
{
    protected DataTable dtGridItems;
    UserAccess objUA = new UserAccess();
    public int CPID = 0;
    public int PortId = 0;
    public string  PortName = "";
    public string OType = "";
    public Boolean uaEditFlag = true;//Test default true
    public Boolean uaDeleteFlage = true;
    BLL_CP_CharterParty objCP = new BLL_CP_CharterParty();
    BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();
    protected void Page_Load(object sender, EventArgs e)
    {
       // UserAccessValidation();
        if (!IsPostBack)
        {
            if (Session["CPID"] != null)
            {
                CPID = Convert.ToInt32(Request.QueryString["CPID"]);
                ViewState["CPID"] = CPID.ToString();
            }
            BindItems();

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

        if (objUA.Add == 0)
        {
            btnSave.Enabled = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            // btnsave.Visible = false;
            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void LoadHour(DropDownList ddlHH)
    {
        ListItemCollection lic = new ListItemCollection();
        int count = 0;
        while (count < 24)
        {
            ListItem li = new ListItem();
            if (count < 10)
            {
                li.Text = "0" + count.ToString();
                li.Value = "0" + count.ToString();
            }
            else
            {
                li.Text = count.ToString();
                li.Value = count.ToString();
            }
            lic.Add(li);
            count++;

        }
        ddlHH.DataSource = lic;
        ddlHH.DataBind();

    }

    public void LoadMin(DropDownList ddlMin)
    {
        ListItemCollection lic = new ListItemCollection();
        int count = 0;
        while (count < 60)
        {
            ListItem li = new ListItem();
            if (count < 10)
            {
                li.Text = "0" + count.ToString();
                li.Value = "0" + count.ToString();
            }
            else
            {
                li.Text = count.ToString();
                li.Value = count.ToString();
            }
            lic.Add(li);
            count++;

        }
        ddlMin.DataSource = lic;
        ddlMin.DataBind();
    }


    protected string AddHourMin(string dtText, string HH, string MIN)
    {
        DateTime dt;

        dt = Convert.ToDateTime(dtText);
        int TotalMin = Convert.ToInt16(HH) * 60 + Convert.ToInt16(MIN);
        dt = dt.AddMinutes(TotalMin);

        return dt.ToString();
    }


   private void BindItems()
    {
        try
        {
            DataTable dt = objCP.GET_Trading_Period(UDFLib.ConvertIntegerToNull(ViewState["CPID"]));
            if (dt.Rows.Count > 0)
            {
                dtGridItems = dt;
                rgdItems.DataSource = dt;
                rgdItems.DataBind();
                //rgdItems.ShowFooter = false;
            }
            else
            {
                dtGridItems = GetAddTable();
                rgdItems.DataSource = dtGridItems;
                rgdItems.DataBind();
               // rgdItems.MasterTableView.Columns[8].Visible = false;
                //rgdItems.MasterTableView.Columns[9].Visible = false;

            }
            ViewState["dtGridItems"] = dtGridItems;

        }
        catch (Exception ex)
        {
            lblError.Text = ex.ToString();
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

    }


    private DataTable GetAddTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Trading_Range_Id");
        dt.Columns.Add("Trading_Date");
        dt.Columns.Add("Trading_Range");
        dt.Columns.Add("Trading_Period");
        dt.Columns.Add("Min_Ext_Unit");
        dt.Columns.Add("Min_Ext_Value");
        dt.Columns.Add("Max_Ext_Unit");
        dt.Columns.Add("Max_Ext_Value");
       
        dt.AcceptChanges();
        return dt;
    }

    protected void btnSaveItem_Click(object sender, EventArgs e)
    {

        if (Session["CPID"] != null)
        {
            saveval();
            //BindItems();

        }


    }
    private void saveval()
    {
        try
        {
            int i = 0;

            DataTable dt = new DataTable();
            dt.Columns.Add("Trading_Range_Id");
            dt.Columns.Add("Trading_Date");
            dt.Columns.Add("Trading_Range");
            dt.Columns.Add("Trading_Period");
            dt.Columns.Add("Min_Ext_Unit");
            dt.Columns.Add("Min_Ext_Value");
            dt.Columns.Add("Max_Ext_Unit");
            dt.Columns.Add("Max_Ext_Value");


            int inc = 1;
            string WEF = "";
            
            foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
            {
                DropDownList ddlLTGMTP = (DropDownList)(dataItem.FindControl("ddlLTGMTP"));
                DropDownList ddlHoursWEFP = (DropDownList)(dataItem.FindControl("ddlHoursWEFP"));
                DropDownList ddlMinsWEFP = (DropDownList)(dataItem.FindControl("ddlMinsWEFP"));
                TextBox txtPWFF = (TextBox)(dataItem.FindControl("txtPWFF"));

                DropDownList ddlminUnit = (DropDownList)(dataItem.FindControl("ddlminUnit"));
                DropDownList ddlmaxUnit = (DropDownList)(dataItem.FindControl("ddlmaxUnit"));
                TextBox txtTradingRange = (TextBox)(dataItem.FindControl("txtTradingRange"));
                TextBox txtTradingPeriod = (TextBox)(dataItem.FindControl("txtTradingPeriod"));
                TextBox Min_Ext_Value = (TextBox)(dataItem.FindControl("Min_Ext_Value"));
                TextBox Max_Ext_Value = (TextBox)(dataItem.FindControl("Max_Ext_Value"));
                HiddenField hdnTPId= (HiddenField)(dataItem.FindControl("hdnTPId"));
                if (txtPWFF.Text != "")
                {
                    WEF = AddHourMin(txtPWFF.Text, ddlHoursWEFP.SelectedValue, ddlMinsWEFP.SelectedValue);
                   
                }
                if (txtTradingRange.Text.Length > 0 && txtTradingPeriod.Text.Length > 0)
                {
                    DataRow dritem = dt.NewRow();
                    if (hdnTPId.Value == null || hdnTPId.Value == "")
                        dritem["Trading_Range_Id"] = "0";
                    else
                        dritem["Trading_Range_Id"] = hdnTPId.Value;
                    if (WEF != "")
                    {
                        WEF = Convert.ToDateTime(WEF).ToString("yyyy-MM-dd hh:mm");
                        dritem["Trading_Date"] = WEF;
                    }
                    else
                    {
                        dritem["Trading_Date"] = WEF;
                    }
                    
                    dritem["Trading_Range"] = txtTradingRange.Text;
                    dritem["Trading_Period"] = txtTradingPeriod.Text;
                    dritem["Min_Ext_Unit"] = ddlminUnit.SelectedValue;
                    dritem["Min_Ext_Value"] = Min_Ext_Value.Text == "" ? Convert.DBNull : Convert.ToInt32(Min_Ext_Value.Text);
                    dritem["Max_Ext_Unit"] = ddlmaxUnit.SelectedValue;
                    dritem["Max_Ext_Value"] = Max_Ext_Value.Text == "" ? Convert.DBNull : Convert.ToDecimal(Max_Ext_Value.Text);
                    dt.Rows.Add(dritem);
                    inc++;

                }
            }

            int retval = 0;

            if (dt.Rows.Count > 0)
            {
                retval = objCP.INS_UPD_Trading_Range(UDFLib.ConvertIntegerToNull(ViewState["CPID"]), dt, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));

               if( retval==0)
                   lblError.Text = "With  effect from cannot be  duplicate! ";
               else
                BindItems();
            }
            else
            {
                lblError.Text = "Please provide Item description.";
            }
        }
        catch(Exception ex)
        {
            string err = ex.ToString();
        }

    }


    protected void rgdItems_ItemDataBound(object sender, GridItemEventArgs e)
    {
        string sMin_Ext_Unit="";
        string sMax_Ext_Unit="";
        foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
        {
            DropDownList ddlLTGMTP = (DropDownList)(dataItem.FindControl("ddlLTGMTP"));
            DropDownList ddlHoursWEFP = (DropDownList)(dataItem.FindControl("ddlHoursWEFP"));
            DropDownList ddlMinsWEFP = (DropDownList)(dataItem.FindControl("ddlMinsWEFP"));
            TextBox Min_Ext_Value = (TextBox)(dataItem.FindControl("Min_Ext_Value"));
            TextBox Max_Ext_Value = (TextBox)(dataItem.FindControl("Max_Ext_Value"));
            ImageButton ImgDelete = (ImageButton)(dataItem.FindControl("ImgDelete"));
            DropDownList ddlminUnit = (DropDownList)(dataItem.FindControl("ddlminUnit"));
            DropDownList ddlmaxUnit = (DropDownList)(dataItem.FindControl("ddlmaxUnit"));

             Image imgGreenArrow = (Image)(dataItem.FindControl("imgGreenArrow"));
             HiddenField hdnActivePeriod = (HiddenField)(dataItem.FindControl("hdnActivePeriod"));
             Label lblRedelivery = (Label)(dataItem.FindControl("lblRedelivery"));

            ddlminUnit.SelectedValue = ((Label)(dataItem.FindControl("Min_Ext_Unit"))).Text;
            ddlmaxUnit.SelectedValue = ((Label)(dataItem.FindControl("Max_Ext_Unit"))).Text;
            bool isActive = false;

            if (hdnActivePeriod.Value == "1")
                isActive = true;
            if (isActive)
            {
                imgGreenArrow.Visible = true;
                lblRedelivery.Visible = true;
            }


            TextBox txtPWFF = (TextBox)(dataItem.FindControl("txtPWFF"));
            LoadHour(ddlHoursWEFP);
            LoadMin(ddlMinsWEFP);
            HiddenField hdnDate = (HiddenField)(dataItem.FindControl("hdnDate"));
            if (hdnDate.Value != null && hdnDate.Value != "")
            {
                txtPWFF.Text = Convert.ToDateTime(hdnDate.Value).ToString("dd-MMM-yyyy");
                int hour = Convert.ToDateTime(hdnDate.Value).Hour;
                int Min = Convert.ToDateTime(hdnDate.Value).Minute;
                string strHour = "00";
                string strMin = "00";
                if (hour < 10)
                    strHour = "0" + hour.ToString();
                else
                    strHour = hour.ToString();
                if (Min < 10)
                    strMin = "0" + Min.ToString();
                else
                    strMin = Min.ToString();
                ddlHoursWEFP.SelectedValue = strHour;
                ddlMinsWEFP.SelectedValue = strMin;
            }


            HiddenField hdnTPId = (HiddenField)(dataItem.FindControl("hdnTPId"));
            Min_Ext_Value.Attributes.Add("onkeypress", "return numbersonly(event)");
            Max_Ext_Value.Attributes.Add("onkeypress", "return numbersonly(event)");

            if (hdnTPId.Value == "0")
            {
                ImgDelete.Visible = false;
            }

        }

    }


    protected void btnAddNewItem_Click(object s, EventArgs e)
    {
        try


        {

            dtGridItems = (DataTable)ViewState["dtGridItems"];
            int RowID = 0;
            string TradingRangeId = "0";
            foreach (GridDataItem item in rgdItems.MasterTableView.Items)
            {
                HiddenField hdnTradingPeriod= (HiddenField)item.FindControl("hdnTPId");
                if (hdnTradingPeriod.Value != null && hdnTradingPeriod.Value != "")
                    TradingRangeId = hdnTradingPeriod.Value;
                else
                    TradingRangeId = "0";
                dtGridItems.Rows[RowID]["Trading_Date"] = ((Label)item.FindControl("lblWEF")).Text;
                DropDownList ddlLTGMTP = (DropDownList)(item.FindControl("ddlLTGMTP"));
                DropDownList ddlHoursWEFP = (DropDownList)(item.FindControl("ddlHoursWEFP"));
                DropDownList ddlMinsWEFP = (DropDownList)(item.FindControl("ddlMinsWEFP"));
                TextBox txtPWFF = (TextBox)(item.FindControl("txtPWFF"));
                string WEF = "";

                if (txtPWFF.Text != "")
                {
                    WEF = AddHourMin(txtPWFF.Text, ddlHoursWEFP.SelectedValue, ddlMinsWEFP.SelectedValue);
                    dtGridItems.Rows[RowID]["Trading_Date"] = WEF;
                }

                dtGridItems.Rows[RowID]["Trading_Range_Id"] = TradingRangeId;
                dtGridItems.Rows[RowID]["Min_Ext_Unit"] = ((DropDownList)item.FindControl("ddlminUnit")).SelectedValue;
                dtGridItems.Rows[RowID]["Max_Ext_Unit"] = ((DropDownList)item.FindControl("ddlmaxUnit")).SelectedValue;
                dtGridItems.Rows[RowID]["Trading_Range"] = ((TextBox)item.FindControl("txtTradingRange")).Text;
                dtGridItems.Rows[RowID]["Trading_Period"] = ((TextBox)item.FindControl("txtTradingPeriod")).Text;

                dtGridItems.Rows[RowID]["Max_Ext_Value"] = ((TextBox)item.FindControl("Max_Ext_Value")).Text == "" ? Convert.DBNull : Convert.ToInt32(((TextBox)item.FindControl("Max_Ext_Value")).Text);
                dtGridItems.Rows[RowID]["Min_Ext_Value"] = ((TextBox)item.FindControl("Min_Ext_Value")).Text == "" ? Convert.DBNull : Convert.ToInt32(((TextBox)item.FindControl("Min_Ext_Value")).Text);
                RowID++;
            }

            DataRow dr = dtGridItems.NewRow();
            dtGridItems.Rows.Add(dr);
            rgdItems.DataSource = dtGridItems;
            rgdItems.DataBind();

            ViewState["dtGridItems"] = dtGridItems;
        }
        catch(Exception ex)
        {
            string sError = ex.ToString();
        }

    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        HiddenField lblgrdID = (rgdItems.FindControl("hdnTPId") as HiddenField);
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? ItemID = UDFLib.ConvertIntegerToNull(arg[0]);
    }


}
   
