using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SMS.Business.Infrastructure;
using SMS.Business.TMSA;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;
using System.Text;

public partial class PI_Details : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    protected DataTable dtGridItems;
    protected int PI_ID= 0;
    protected int Detail_ID = 0;
    protected string QtrMonth= null ;
    int IsExist = 0;
    public Boolean uaEditFlag = true;//Test default true
    public Boolean uaDeleteFlage = true;
    BLL_TMSA_PI objPI = new BLL_TMSA_PI();
    BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();
    protected void Page_Load(object sender, EventArgs e)
    {
       UserAccessValidation();
        if (!IsPostBack)
        {
            BindPIDetails();
            PopulateYear();
            BindGrid();
            BindVessel();
        }
    }

    protected void PopulateYear()
    {
        int CurrentYear = DateTime.Now.Year;
        int count= 0;
        for (count = CurrentYear; count >= CurrentYear - 10; count--)
        {
            ListItem li= new ListItem();
            li.Text=count.ToString();
            li.Value=count.ToString();
            ddlYear.Items.Add(li);
        }
    }

   /// <summary>
   /// Method to load fleet and vessels
   /// </summary>

    protected void BindVessel()
    {
        try
        {
            BLL_Infra_VesselLib bll_Vessel = new BLL_Infra_VesselLib();
            BLL_TMSA_KPI bll_KPI = new BLL_TMSA_KPI();
            DataTable dt = bll_Vessel.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            dt.Columns.Remove("code");
            dt.Columns.Remove("name");
            dt.Columns.Remove("fleetname");
            dt.Columns.Remove("Super_MailID");
            dt.Columns.Remove("TechTeam_MailID");
            dt.Columns.Remove("Vessel_Owner");
            dt.Columns.Remove("Vessel_Manager");
            DataTable dtable = bll_KPI.Get_Fleet_Vessel_List(dt, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID());
            ddlVessel.DataSource = dtable;
            ddlVessel.DataTextField = "Vessel_Name";
            ddlVessel.DataValueField = "Vessel_Id";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        
    }
    /// <summary>
    /// Description:Method to bind PI header information
    /// </summary>
    protected void BindPIDetails()
    {
        try
        {
            PI_ID = Convert.ToInt32(Request.QueryString[0]);
            DataTable dtDetail = BLL_TMSA_PI.Get_PI_Details(PI_ID).Tables[0];
            if (dtDetail.Rows.Count > 0)
            {
                string sPIName = dtDetail.Rows[0]["Name"].ToString();
                string sPICode = dtDetail.Rows[0]["Code"].ToString();

                ltPageHeader.Text = ltPageHeader.Text + " [" + sPIName + "("+ sPICode + ")" + " ]";

               
                ViewState["PI_Name"] = sPIName + "(" + sPICode + ")";
                string sInterval = dtDetail.Rows[0]["Interval"].ToString();
                int MeasuredForSBU = Convert.ToInt32(dtDetail.Rows[0]["MeasuredForSBU"]);
                if (MeasuredForSBU == 1)
                {
                    ViewState["MeasuredForSBU"] = MeasuredForSBU;
                    ltSBUValue.Visible = true;
                    txtSBU.Visible = true;
                    rgdItems.Visible = false;
                }
                else
                {
                    ltSBUValue.Visible = false ;
                    txtSBU.Visible = false;
                   // BindGrid();
                }
                if (sInterval.ToUpper() == "QUARTER")
                {
                    ltQtrMonth.Text = " For Quarter :";
                    ddlQuarter.Visible=true;
                    ddlMonths.Visible=false;
                }
                else if (sInterval.ToUpper() == "MONTH")
                {
                    ltQtrMonth.Text = " For Month :";
                    ddlQuarter.Visible = false;
                    ddlMonths.Visible = true;
                }
                else
                {
                    ltQtrMonth.Visible = false;
                    ddlQuarter.Visible = false;
                    ddlMonths.Visible = false;
                }

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }


    /// <summary>
    /// Description:Method to bind PI header information
    /// </summary>
    protected void BindHeadinfo()
    {
        //ClearData();

        PI_ID = Convert.ToInt32(Request.QueryString[0]);
        if (ViewState["Detail_ID"] != null)
        {

            Detail_ID = Convert.ToInt32(ViewState["Detail_ID"]);
            DataTable dt = BLL_TMSA_PI.Get_PI_Head_Details(PI_ID, Detail_ID).Tables[0];
            ddlYear.SelectedValue = dt.Rows[0]["ForYear"].ToString();
            txtEffectivedate.Text = dt.Rows[0]["Effective_From"].ToString();
            txtEfffectTo.Text = dt.Rows[0]["Effective_To"].ToString();
            string QtrMonth =  dt.Rows[0]["QtrMonth"].ToString();
            if (ddlMonths.Items.FindByValue(QtrMonth) != null)
                ddlMonths.SelectedValue = ddlMonths.Items.FindByValue(QtrMonth).Value;
            if (ddlQuarter.Items.FindByValue(QtrMonth) != null)
                ddlQuarter.SelectedValue = ddlQuarter.Items.FindByValue(QtrMonth).Value;
            if (Convert.ToInt16(dt.Rows[0]["Vessel_Id"]) != 0)
            {
                divEDIT.Visible = true;
                lblVesselName.Text = BLL_TMSA_PI.Get_PI_Head_Details(PI_ID, Detail_ID).Tables[1].Rows[0]["Vessel_Name"].ToString();
                txtSB.Text = dt.Rows[0]["Value"].ToString();
            }
            else
                txtSBU.Text = dt.Rows[0]["Value"].ToString();
            //BindPIValues();
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

  
    protected void BindGrid()
    {
        try
        {
            LoadSearchResults();
           if(Convert.ToBoolean(ViewState["MeasuredForSBU"])==false)
             BindPIValues();


        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Method to populate value grid for all vessels
    /// </summary>
    protected void BindPIValues()
    {
        try
        {
            DataTable dt = BLL_TMSA_PI.Get_All_Vessels(Convert.ToInt32(Session["USERCOMPANYID"].ToString())).Tables[0];
            rgdItems.DataSource = dt;
            rgdItems.DataBind();
            rgdItems.Visible = true;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Method to update PI header data
    /// </summary>

    protected void onUpdate(object source, CommandEventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            Detail_ID = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = (GridViewRow)((ImageButton)source).NamingContainer;
            HiddenField hdnVessel =(HiddenField) row.FindControl("hdnVessel_Id");
            if(hdnVessel.Value !="0")
                divEDIT.Visible = true;
            ViewState["Detail_ID"] = Detail_ID;
            gvPIList.SelectedIndex = row.RowIndex;

            BindHeadinfo();
            rgdItems.Visible = false;

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnSaveItem_Click(object sender, EventArgs e)
    {
            SaveData();
            BindGrid();
    }

    protected void ClearData()
    {

        ltmessage.Text = "";
        txtEffectivedate.Text = "";
        txtEfffectTo.Text = "";
        txtSBU.Text = "";
        ddlQuarter.SelectedIndex = -1;
        ddlYear.SelectedIndex = -1;
        ddlMonths.SelectedIndex = -1;
        divEDIT.Visible = false;
        ddlVessel.SelectedIndex = -1;
        txtSearchFrom.Text = "";
        txtSearchTo.Text = "";
        BindGrid();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtEfffectTo.Text != "")
        {
            if (Convert.ToDateTime(txtEffectivedate.Text) <= Convert.ToDateTime(txtEfffectTo.Text))
            {
                SaveData();

               

                if (IsExist == 0)
                {
                    ClearData();
                    ViewState["Detail_ID"] = null;
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    string msg2 = String.Format("alert('Value already exists!')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
                }
            }
            else
            {
                string msg2 = String.Format("alert('From Date  should not be greater than to date')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);

            }

        }
        else
        {
            SaveData();
            if (IsExist == 0)
            {
                ClearData();
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                string msg2 = String.Format("alert('Value already exists!')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            }

        }
      
    }


    protected void SaveData()
    {
        ltmessage.Text = "";
        try
        {
            
            DateTime? dtWEF = null;
            DateTime? dtWET = null;
            double? SBU_Value = null;
            double? Value = 0.00;
            if (txtEffectivedate.Text != "")
                dtWEF = Convert.ToDateTime(txtEffectivedate.Text);
            if (txtEfffectTo.Text != "")
                dtWET = Convert.ToDateTime(txtEfffectTo.Text);
            else
                dtWET = dtWEF;
            if (txtSBU.Text != "")
                SBU_Value = Convert.ToDouble(txtSBU.Text);
            PI_ID = Convert.ToInt32(Request.QueryString["PI_ID"]);
            if (ddlMonths.Visible == true)
                QtrMonth = ddlMonths.SelectedValue;
            else if (ddlQuarter.Visible == true)
                QtrMonth = ddlQuarter.SelectedValue;

            if (dtWEF <= dtWET)
            {
                if (ViewState["Detail_ID"] != null)
                {
                    Detail_ID = Convert.ToInt32(ViewState["Detail_ID"]);
                    Value = 1.00;
                }

                DataTable dt = getValueData();

                BLL_TMSA_PI.INSERT_PI_Detail(Detail_ID, Value, dt, PI_ID, ddlYear.SelectedValue, QtrMonth, dtWEF, dtWET, SBU_Value, UDFLib.ConvertToInteger(Session["UserID"].ToString()), ref IsExist);

            }

        }
        catch (Exception ex)
        {
            ltmessage.Text = ex.ToString();
        }
    }

    public DataTable getValueData()
    {
        DataTable dtGridItems = new DataTable();

        try
        {
            dtGridItems.Columns.Add("Vessel_ID");
            dtGridItems.Columns.Add("Value");
            foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
            {
                TextBox txtItem_Amount = (TextBox)(dataItem.FindControl("txtItem_Amount"));
                HiddenField hdnVessel_Id = (HiddenField)(dataItem.FindControl("hdnVessel_Id"));
                DataRow dritem = dtGridItems.NewRow();
                dritem["Value"] = txtItem_Amount.Text == "" ? Convert.DBNull : Convert.ToDecimal(txtItem_Amount.Text);
                dritem["Vessel_ID"] = hdnVessel_Id.Value;
                dtGridItems.Rows.Add(dritem);

            }
        }
        catch { }
        return dtGridItems;
    }

    protected void rgdItems_ItemDataBound(object sender, GridItemEventArgs e)
    {
        foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
        {

            TextBox txtItem_Amount = (TextBox)(dataItem.FindControl("txtItem_Amount"));
            txtItem_Amount.Attributes.Add("onkeypress", "return numbersonly(event)");

        }

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearData();
        rgdItems.Visible = true;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime? dtWEF = null;
            double? value = 0.00;
            DateTime? dtWET = null;

            if (txtEffectivedate.Text != "")
                dtWEF = Convert.ToDateTime(txtEffectivedate.Text);
            if (txtEfffectTo.Text != "")
                dtWET = Convert.ToDateTime(txtEfffectTo.Text);
            if (txtSB.Text != "")
                value = Convert.ToDouble(txtSB.Text);

            PI_ID = Convert.ToInt32(Request.QueryString["PI_ID"]);
            BLL_TMSA_PI.Update_PI_Head(Convert.ToInt32(ViewState["Detail_ID"].ToString()), Convert.ToInt32(Request.QueryString[0]), ddlYear.SelectedValue, null, dtWEF, dtWET, null, value, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
            LoadSearchResults();
            divEDIT.Visible = false;
            txtEffectivedate.Text = "";
            txtEfffectTo.Text = "";
        }
        catch (Exception ex)
        {
            ltmessage.Text = ex.ToString();
        }
    }


    protected void btnportfilter_Click(object sender, ImageClickEventArgs e)
    {

        LoadSearchResults();

    }

    protected void LoadSearchResults()
    {
        rgdItems.Visible = false;
        int rowcount = ucCustomPager1.isCountRecord;
        PI_ID = Convert.ToInt32(Request.QueryString["PI_ID"]);
        DateTime? dtWEF = null;
        DateTime? dtWET = null;
        if (txtSearchFrom.Text != "")

            dtWEF = Convert.ToDateTime(txtSearchFrom.Text);
        if (txtSearchTo.Text != "")
            dtWET = Convert.ToDateTime(txtSearchTo.Text);
        //DataTable dt = BLL_TMSA_PI.Get_Vessel_Values(Convert.ToInt32(ddlVessel.SelectedValue), dtWEF, dtWET).Tables[0];

        if (txtSearchFrom.Text != "" && txtSearchTo.Text != "")
        {
            if (dtWEF <= dtWET)
            {
                DataTable dt = BLL_TMSA_PI.Get_Vessel_Values(Convert.ToInt32(ddlVessel.SelectedValue), dtWEF, dtWET, PI_ID, ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount).Tables[0];


                if (ucCustomPager1.isCountRecord == 1)
                {
                    ucCustomPager1.CountTotalRec = rowcount.ToString();
                    ucCustomPager1.BuildPager();
                }
                dt.DefaultView.Sort = "Effective_From DESC";
                gvPIList.DataSource = dt;
                gvPIList.DataBind();
            }
            else
            {
                string msg2 = String.Format("alert('From Date  should not be greater than to date')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            }
        }
        else
        {
            DataTable dt = new DataTable();
            if (ddlVessel.SelectedValue != "0" && ddlVessel.SelectedValue !="")
                 dt = BLL_TMSA_PI.Get_Vessel_Values(Convert.ToInt32(ddlVessel.SelectedValue), dtWEF, dtWET, PI_ID, ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount).Tables[0];
            else
                dt = BLL_TMSA_PI.Get_Vessel_Values(null, dtWEF, dtWET, PI_ID, ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount).Tables[0];
            if (ucCustomPager1.isCountRecord == 1)
            {
                dt.DefaultView.Sort = "Effective_From DESC";
                ucCustomPager1.CountTotalRec = rowcount.ToString();
                ucCustomPager1.BuildPager();
            }

            gvPIList.DataSource = dt;
            gvPIList.DataBind();
        }
    }
    /// <summary>
    /// Method to export data to excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int rowcount = ucCustomPager1.isCountRecord;
            string PIName = "PI Name: ";
            PI_ID = Convert.ToInt32(Request.QueryString["PI_ID"]);
            DateTime? dtWEF = null;
            DateTime? dtWET = null;
            if (txtSearchFrom.Text != "")

                dtWEF = Convert.ToDateTime(txtSearchFrom.Text);
            if (txtSearchTo.Text != "")
                dtWET = Convert.ToDateTime(txtSearchTo.Text);
            if (ViewState["PI_Name"] != null)
                PIName = PIName + ViewState["PI_Name"].ToString();
            DataTable dt = BLL_TMSA_PI.Get_Vessel_Values(Convert.ToInt32(ddlVessel.SelectedValue), dtWEF, dtWET, PI_ID, null, null, ref  rowcount).Tables[0];
            string[] HeaderCaptions = { "Vessel", "Effect From", "Effect To", "PI Value", "Created ON" };
            string[] DataColumnsName = { "Vessel_Name", "Effective_From", "Effective_To", "Value", "Date_Of_Creation" };
            GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, PIName, PIName);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
}
   
