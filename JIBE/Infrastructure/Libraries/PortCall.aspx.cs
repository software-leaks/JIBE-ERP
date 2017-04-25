using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class Infrastructure_Libraries_PortCall : System.Web.UI.Page
{
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    UserAccess objUA = new UserAccess();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_Infra_PortCall objPortCall = new BLL_Infra_PortCall();
    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();

    protected void Page_Load(object sender, EventArgs e)
    {

        UserAccessValidation();
        if (!IsPostBack)
        {
            Load_VesselList();
            Load_PortList();

            BindPortCall();
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

        if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnsave.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;

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
        DataTable dt = objBLL.Get_VesselList(0,0,0,"",1);


        DDLVesselFilter.DataSource = dt;
        DDLVesselFilter.DataTextField = "VESSEL_NAME";
        DDLVesselFilter.DataValueField = "VESSEL_ID";
        DDLVesselFilter.DataBind();
        DDLVesselFilter.Items.Insert(0, new ListItem("-ALL-", "0"));
        DDLVesselFilter.SelectedIndex = 0;

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-Select-", "0"));
        ddlVessel.SelectedIndex = 0;
    }


    public void Load_PortList()
    {

        DataTable dt = objBLLPort.Get_PortList_Mini();
        
        DDLPortFilter.DataSource = dt;
        DDLPortFilter.DataTextField = "Port_Name";
        DDLPortFilter.DataValueField = "Port_ID";
        DDLPortFilter.DataBind();
        DDLPortFilter.Items.Insert(0, new ListItem("-ALL-", "0"));
       
        DDLPort.DataSource = dt;
        DDLPort.DataTextField = "Port_Name";
        DDLPort.DataValueField = "Port_ID";
        DDLPort.DataBind();
        DDLPort.Items.Insert(0, new ListItem("-Select-", "0"));
       
    
    }



    public void BindPortCall()
    {

    

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objPortCall.Get_PortCall_Search(txtfilter.Text != "" ? txtfilter.Text : null,UDFLib.ConvertIntegerToNull(DDLVesselFilter.SelectedValue),UDFLib.ConvertIntegerToNull(DDLPortFilter.SelectedValue) ,sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvPortCall.DataSource = dt;
            gvPortCall.DataBind();
        }
        else
        {
            gvPortCall.DataSource = dt;
            gvPortCall.DataBind();
        }
    }

    protected void btnFilter_Click(object s, EventArgs e)
    {

        BindPortCall();

    }
   
    protected void btnRefresh_Click(object s, EventArgs e)
    {
        txtfilter.Text = "";
        DDLVesselFilter.SelectedValue = "0";
        DDLPort.SelectedValue = "0";
        BindPortCall();
    }

    protected void onUpdate(object s, CommandEventArgs e)
    {
        BLL_Infra_Supplier objSupp = new BLL_Infra_Supplier();
        HiddenFlag.Value = "Edit";

        OperationMode = "Edit Port Call";



        string[] arg = e.CommandArgument.ToString().Split(',');
        int Port_call_ID = UDFLib.ConvertToInteger(arg[0]);
        int Vessel_ID = UDFLib.ConvertToInteger(arg[1]);


        DataTable dtPortCall = objPortCall.Get_PortCall_List(Convert.ToInt32(Port_call_ID),Vessel_ID);

        txtPortCallID.Text = dtPortCall.Rows[0]["Port_Call_ID"].ToString();

        txtVesselCode.Text = dtPortCall.Rows[0]["Vessel_Code"].ToString();

        ddlVessel.SelectedValue = dtPortCall.Rows[0]["Vessel_ID"].ToString();
        
        DDLPort.SelectedValue = dtPortCall.Rows[0]["Port_ID"].ToString();
        dtpArrival.Text = dtPortCall.Rows[0]["Arrival"].ToString();
        dtpBerthing.Text = dtPortCall.Rows[0]["Berthing"].ToString();
        dtpDeparture.Text = dtPortCall.Rows[0]["Departure"].ToString();
        txtPortRemark.Text = dtPortCall.Rows[0]["Port_Remarks"].ToString();
       

        //ddlCountry_AV.ClearSelection();
        //ListItem list = ddlCountry_AV.Items.FindByValue(dtSuppDtl.Rows[0]["Country"].ToString());
        //if (list != null)
        //    list.Selected = true;

        //txtCreationDate_AV.Enabled = false;

        string AddMaker = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddMaker", AddMaker, true);
    }

    protected void lbtnDelete_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int Port_call_ID = UDFLib.ConvertToInteger(arg[0]);
        int Vessel_ID = UDFLib.ConvertToInteger(arg[1]);

        objPortCall.Del_PortCall_Details_DL(Convert.ToInt32(Port_call_ID), Vessel_ID,Convert.ToInt32(Session["USERID"].ToString()));
        BindPortCall();

    }

    protected void btnsave_Click(object s, EventArgs e)
    {
        
        if (HiddenFlag.Value == "Add")
        {
            //objPortCall.Ins_PortCall_Details(null,null,DDLPort.SelectedItem.ToString()
            //    ,UDFLib.ConvertDateToNull(dtpArrival.Text)
            //    ,UDFLib.ConvertDateToNull(dtpBerthing.Text)
            //    ,UDFLib.ConvertDateToNull(dtpDeparture.Text),null,null,txtPortRemark.Text,null,null
            //    ,UDFLib.ConvertToInteger(DDLPort.SelectedValue),null,UDFLib.ConvertToInteger(ddlVessel.SelectedValue),Convert.ToInt32(Session["USERID"].ToString()),Convert.ToInt32(chkWarRisk.Checked) ,Convert.ToInt32(chkShipCrane.Checked));
        }
        else 
        {
            //objPortCall.Upd_PortCall_Details(Convert.ToInt32(txtPortCallID.Text), txtVesselCode.Text, null, DDLPort.SelectedItem.ToString()
            //   , UDFLib.ConvertDateToNull(dtpArrival.Text)
            //   , UDFLib.ConvertDateToNull(dtpBerthing.Text)
            //   , UDFLib.ConvertDateToNull(dtpDeparture.Text), null, null, txtPortRemark.Text, null, null
            //   , UDFLib.ConvertToInteger(DDLPort.SelectedValue), null, UDFLib.ConvertToInteger(ddlVessel.SelectedValue), Convert.ToInt32(Session["USERID"].ToString()), Convert.ToInt32(chkWarRisk.Checked), Convert.ToInt32(chkShipCrane.Checked));
 
        }


        string hideMaker = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hideMaker", hideMaker, true);

        BindPortCall();
    }

    protected void lnkAddNew_Click(object s, EventArgs e)
    {
        HiddenFlag.Value = "Add";
        OperationMode = "Add Port Call";

        this.SetFocus("ctl00_MainContent_ddlVessel");


        DDLPort.SelectedValue = "0";
        ddlVessel.SelectedValue = "0";

        dtpArrival.Text = "";
        dtpBerthing.Text = "";
        dtpDeparture.Text = "";
        txtPortRemark.Text = "";


        string AddPortCall = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPortCall", AddPortCall, true);

    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

        BLL_Infra_Supplier objSupp = new BLL_Infra_Supplier();

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objSupp.Get_Suppliers_List_Search(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        string[] HeaderCaptions = { "Vessel", "Port", "Arrival", "Berthing","Departure", "Remaks"};
        string[] DataColumnsName = { "Vessel_Name", "Port_Name", "Arrival", "Berthing" ,"Departure", "Port_Remarks" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "PortCall", "Port Call", "");

    }

  
    protected void gvPortCall_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowUp.png";
                    else
                        img.Src = "~/purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
            
            
            Label lblAddress = (Label)e.Row.FindControl("lblAddress");
            Label lblCountry = (Label)e.Row.FindControl("lblCountry");


            if (DataBinder.Eval(e.Row.DataItem, "Port_Remarks").ToString().Length > 20)
            {
                lblAddress.Text = lblAddress.Text + "..";
                lblAddress.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Address] body=[" + DataBinder.Eval(e.Row.DataItem, "Port_Remarks").ToString() + "]");
            }

          
         
        }

    }

    protected void gvPortCall_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindPortCall();

    }

}