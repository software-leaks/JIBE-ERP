using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.PURC;
using System.Collections.Generic;
public partial class Purchase_LOG_Create_Logistic_PO : System.Web.UI.Page
{

   
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            Dictionary<string, string> DicPO = new Dictionary<string, string>();
            ViewState["vsDicPO"] = DicPO;
            FillDDL();
           
        }
        dvSelectedPo.InnerHtml = "";
        SavePOSelection();
       
    }

    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx");

        if (objUA.Add == 0)
        {

            btnCreateCPO.Visible = false;
        }
        if (objUA.Edit == 0)
        {


        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {


        }


    }
    public void FillDDL()
    {

        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();


            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            DDLVessel.Items.Insert(0, li);




        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
        if (DDLFleet.SelectedValue.ToString() == "0")
        {
            Session["sFleet"] = "0";
            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            DDLVessel.Items.Insert(0, li);
            Session["sVesselCode"] = "0";
        }
        else
        {
            Session["sFleet"] = DDLFleet.SelectedValue;
            DDLVessel.Items.Clear();
            DataTable dtVessel = objVsl.Get_VesselList(int.Parse(DDLFleet.SelectedValue.ToString()), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            //DataTable dtVessel = objVsl.GetVesselsByFleetID(int.Parse(DDLFleet.SelectedValue.ToString()));
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_ID";
            DDLVessel.DataBind();
            ListItem li = new ListItem("--Select--", "0");
            DDLVessel.Items.Insert(0, li);
        }



    }

    public void BindDataItems()
    {
        int is_Fetch_Count=ucCustomPagerPO.isCountRecord;
        gvOrderList.DataSource = BLL_PURC_LOG.Get_Log_PO_List(int.Parse(DDLVessel.SelectedValue), UDFLib.ConvertStringToNull(uc_SupplierList1.SelectedValue), UDFLib.ConvertStringToNull(txtPoNumber.Text),ucCustomPagerPO.CurrentPageIndex,ucCustomPagerPO.PageSize, ref is_Fetch_Count);
        gvOrderList.DataBind();

        ucCustomPagerPO.CountTotalRec = is_Fetch_Count.ToString();
        ucCustomPagerPO.BuildPager();

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindDataItems();
    }
    protected void btnCreateCPO_Click(object sender, EventArgs e)
    {
        DataTable dtPO = new DataTable();
        dtPO.Columns.Add("PID");
        dtPO.Columns.Add("Value");
        DataRow dr = null;
        Dictionary<string, string> DicPO = new Dictionary<string, string>();
        DicPO = (Dictionary<string, string>)ViewState["vsDicPO"];
        int PID=1;
        foreach (string PO in DicPO.Values)
        {
            dr = dtPO.NewRow();
            dr["PID"] = PID++;
            dr["Value"] = PO;
            dtPO.Rows.Add(dr);
        }

        int PO_ID=0;
        if (dtPO.Rows.Count > 0)
        {
            PO_ID = BLL_PURC_LOG.Ins_Log_Create_LogisticPO(Convert.ToInt32(DDLVessel.SelectedValue), dtPO, Convert.ToInt32(Session["userid"].ToString()));

            if (PO_ID > 0)
            {
                btnCreateCPO.Enabled = false;
                DicPO.Clear();
                ViewState["vsDicPO"] = DicPO;

                string URL = String.Format("OpenPopupWindow('POP__Logistic_PO_Details', 'Logistic PO Details', 'LOG_Logistic_PO_Details.aspx?LOG_ID=" + PO_ID + " ','popup',800,1020,null,null,false,false,true,redirecttoindexpage);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "k" + PO_ID.ToString(), URL, true);

               
               
            }
        }
        else
        {
            string URL = String.Format("alert('Please select PO !')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "k" + PO_ID.ToString(), URL, true);
            btnCreateCPO.Enabled = true;
        }
    }
    protected void gvOrderList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Dictionary<string, string> DicPO = new Dictionary<string, string>();
            DicPO = (Dictionary<string, string>)ViewState["vsDicPO"];
            CheckBox chkPO = (CheckBox)e.Row.FindControl("chkSelect");
            if (DicPO.ContainsKey(chkPO.Text.Trim()))
                chkPO.Checked = true;
        }
    }

    protected void SavePOSelection()
    {

        Dictionary<string, string> DicPO = new Dictionary<string, string>();
        DicPO = (Dictionary<string, string>)ViewState["vsDicPO"];

        CheckBox chkPO = new CheckBox();
        foreach (GridViewRow gr in gvOrderList.Rows)
        {
            chkPO = (CheckBox)gr.FindControl("chkSelect");
            if (chkPO.Checked)
            {
                if (!DicPO.ContainsKey(chkPO.Text.Trim()))
                {
                    DicPO.Add(chkPO.Text.Trim(), chkPO.Text.Trim());
                }

               
            }
            else
            {
                if (DicPO.ContainsKey(chkPO.Text.Trim()))
                {
                    DicPO.Remove(chkPO.Text.Trim());
                }
            }
        }

        int i = 1;
        foreach (string po in DicPO.Values)
        {
            if (i == 1)
                dvSelectedPo.InnerHtml = "<b style='color:black'> Selected PO(s) : </b>";

            dvSelectedPo.InnerHtml += "<b style='color:black'>"+(i++).ToString() +".</b> " +po+" ; ";
        }
   
        ViewState["vsDicPO"] = DicPO;



      
    }
    protected void DDLVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Dictionary<string, string> DicPO = new Dictionary<string, string>();
        //ViewState["vsDicPO"] = DicPO;
        btnCreateCPO.Enabled = true;
    }

    protected void btnClearselection_Click(object s, EventArgs e)
    {
        Dictionary<string, string> DicPO = new Dictionary<string, string>();
        ViewState["vsDicPO"] = DicPO;
        btnCreateCPO.Enabled = true;
        DDLFleet.SelectedIndex = 0;
        DDLVessel.SelectedIndex = 0;
        uc_SupplierList1.SelectedValue = "0";
        txtPoNumber.Text = "";


        BindDataItems();
        dvSelectedPo.InnerHtml = "";
    }
}