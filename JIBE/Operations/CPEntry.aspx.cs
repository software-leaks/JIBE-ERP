using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SMS.Business.Operation;
using SMS.Business.Infrastructure;

public partial class Operation_CPEntry : System.Web.UI.Page
{
    int CurrentSTS;
    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();

    protected void Page_Load(object sender, EventArgs e)
    {
        txtdatavalue.Attributes.Add("onkeypress", "return isNumberKey(event)");
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");

        if (!IsPostBack)
        {
            //rbtnLatest.Checked = true;
            rdoCPEntryFlag.SelectedIndex = 1;
            BindFleetDLL();
            DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            BindVesselDDL();
            ViewState["SORTBYCOLOUMN"] = null;
            BindCPEntries();

            ddlDatatype.DataSource = BLL_OPS_VoyageReports.OPS_SP_Get_CPEntriesType();
            ddlDatatype.DataTextField = "DataType";
            ddlDatatype.DataValueField = "id";
            ddlDatatype.DataBind();
            ListItem alltype = new ListItem("SELECT ALL", "0");
            ddlDatatype.Items.Insert(0, alltype);

            //BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            //ddlUser.DataSource = objUser.Get_UserList();
            DataTable dt = objBLLUser.Get_UserList();
            string filter = "User_type ='OFFICE USER' ";


            dt.DefaultView.RowFilter = filter;

            ddlUser.DataSource = dt.DefaultView.ToTable();

            ddlUser.DataTextField = "USER_NAME";
            ddlUser.DataValueField = "userid";
            ddlUser.DataBind();
            ListItem alluser = new ListItem("SELECT ALL", "0");
            ddlUser.Items.Insert(0, alluser);


            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            ddlvesselCP.Items.Clear();
            ddlvesselCP.DataSource = dtVessel;
            ddlvesselCP.DataTextField = "Vessel_name";
            ddlvesselCP.DataValueField = "Vessel_id";
            ddlvesselCP.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            ddlvesselCP.Items.Insert(0, li);


            ddldatatypeCP.DataSource = BLL_OPS_VoyageReports.OPS_SP_Get_CPEntriesType();
            ddldatatypeCP.DataTextField = "DataType";
            ddldatatypeCP.DataValueField = "id";
            ddldatatypeCP.DataBind();
            ListItem lix = new ListItem("--SELECT ALL--", "0");
            ddldatatypeCP.Items.Insert(0, lix);

           

        }

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.Items.Clear();
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLFleet.Items.Insert(0, li);





        }
        catch (Exception ex)
        {

        }
    }

    public void BindVesselDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            ddlvessel.Items.Clear();
            ddlvessel.DataSource = dtVessel;
            ddlvessel.DataTextField = "Vessel_name";
            ddlvessel.DataValueField = "Vessel_id";
            ddlvessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            ddlvessel.Items.Insert(0, li);

        }
        catch (Exception ex)
        {

        }
    }
    protected void gvCPEntry_RowDataBound(object sender, GridViewRowEventArgs e)
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
        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label lblCalogueActiveSatus = (Label)e.Row.FindControl("lblCalogueActiveSatus");
        //    Label lblModel = (Label)e.Row.FindControl("lblModel");

        //    Label lblMaker = (Label)e.Row.FindControl("lblMaker");
        //    Label lblParticulars = (Label)e.Row.FindControl("lblParticulars");


        //    Label lblMakerFullDetails = (Label)e.Row.FindControl("lblMakerFullDetails");
        //    Label lblParticularsFullDetails = (Label)e.Row.FindControl("lblParticularsFullDetails");


        //    //if (lblMakerFullDetails.Text.Length > 20)
        //    //    lblMaker.Text = lblMaker.Text + "..";

        //    //if (lblParticularsFullDetails.Text.Length > 20)
        //    //    lblParticulars.Text = lblParticulars.Text + "..";



        //    //lblParticulars.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Particulars] body=[" + lblParticularsFullDetails.Text + "]");
        //    //lblMaker.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Maker] body=[" + lblMakerFullDetails.Text + "]");

        //    //Int64 result = 0;
        //    //if (Int64.TryParse(lblCalogueActiveSatus.Text, out result))
        //    //{
        //    //    e.Row.ForeColor = (result == 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        //    //    //lnkSystemName.ForeColor = (result == 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        //    //}
        //}
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    if (ViewState["SORTBYCOLOUMN"] != null)
        //    {
        //        HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
        //        if (img != null)
        //        {
        //            if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
        //                img.Src = "~/purchase/Image/arrowUp.png";
        //            else
        //                img.Src = "~/purchase/Image/arrowDown.png";

        //            img.Visible = true;
        //        }
        //    }
        //}

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ucCustomPagerItems.isCountRecord = 1;
        string vesselcode = ddlvessel.SelectedValue.ToString();

        ViewState["VesselCode"] = ddlvessel.SelectedValue.ToString();
        BindCPEntries();
    }
    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        ucCustomPagerItems.isCountRecord = 1;
        DDLFleet.SelectedValue = "0";
        BindVesselDDL();
        ddlvessel.SelectedValue = "0";
        ddlDatatype.SelectedValue = "0";
        ddlUser.SelectedValue = "0";
        rdoCPEntryFlag.SelectedIndex = 0;
        BindCPEntries();

    }
    public void BindCPEntries()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;
       
        //int CurrentSTS = 1;
        //if (rdoCPEntryFlag.SelectedValue == true) CurrentSTS = 1; else CurrentSTS = 0;
        if (rdoCPEntryFlag.SelectedItem.Value == "1")
       
             CurrentSTS = 1;
   
        else
            CurrentSTS = 0;

        //DataTable dt = BLL_OPS_VoyageReports.OPS_SP_Get_CPEntries(UDFLib.ConvertToInteger(ddlDatatype.SelectedValue), UDFLib.ConvertToInteger(ddlvessel.SelectedValue),
        //                                                            UDFLib.ConvertToInteger(ddlUser.SelectedValue), CurrentSTS, UDFLib.ConvertToInteger(DDLFleet.SelectedValue));

        //gvCPEntry.DataSource = dt;
        //gvCPEntry.DataBind();
        BLL_OPS_VoyageReports objTechService = new BLL_OPS_VoyageReports();
        DataTable dtCpEntry = new DataTable();
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        dtCpEntry = objTechService.OPS_SP_Get_CPEntries(UDFLib.ConvertIntegerToNull(ddlDatatype.SelectedValue), UDFLib.ConvertIntegerToNull(ddlvessel.SelectedValue),
         UDFLib.ConvertIntegerToNull(ddlUser.SelectedValue), CurrentSTS, UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue)
             , sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dtCpEntry.Rows.Count > 0)
        {
            gvCPEntry.DataSource = dtCpEntry;
            gvCPEntry.DataBind();
        }
        else
        {
            gvCPEntry.DataSource = dtCpEntry;
            gvCPEntry.DataBind();
        }
    }


    protected void btnclearall_Click(object sender, EventArgs e)
    {
        ddlDatatype.SelectedIndex = 0;

        BindFleetDLL();
        BindVesselDDL();

        ddlUser.SelectedIndex = 0;

        BindCPEntries();


    }
   

  
    //protected void ddlvessel_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    BindCPEntries();
    //}

    //protected void ddlDatatype_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindCPEntries();

    //}

    //protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindCPEntries();

    //}

    protected void gvCPEntry_Sorted(object sender, EventArgs e)
    {
        gvCPEntry.PageIndex = Convert.ToInt32(ViewState["currentpage"]);

       
    }

    protected void gvCPEntry_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["SORTBYCOLOUMN"] = e.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;
        BindCPEntries();
    
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            BLL_OPS_VoyageReports.OPS_SP_Ins_CPEntries(int.Parse(ddldatatypeCP.SelectedValue), int.Parse(ddlvesselCP.SelectedValue), GetSessionUserID(), decimal.Parse(txtdatavalue.Text.Trim()));
            BindCPEntries(); 
            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);               
        }
       catch { }
    }

    protected void rbtnLatest_CheckedChanged(object sender, EventArgs e)
    {
        BindCPEntries();
    }

    protected void rbtnHistory_CheckedChanged(object sender, EventArgs e)
    {
        BindCPEntries();
    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselDDL();
        //BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

        //ddlvessel.Items.Clear();
        //DataTable dtVessel = objVsl.GetVesselsByFleetID(int.Parse(DDLFleet.SelectedValue.ToString()));
        //ddlvessel.DataSource = dtVessel;
        //ddlvessel.DataTextField = "Vessel_name";
        //ddlvessel.DataValueField = "Vessel_ID";
        //ddlvessel.DataBind();
        //ListItem li = new ListItem("--SELECT ALL--", "0");
        //ddlvessel.Items.Insert(0, li);


        //BindCPEntries();

    }

    //protected void gvCPEntry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
       
    //    gvCPEntry.PageIndex = e.NewPageIndex;
    //    gvCPEntry.DataBind();
    //    BindCPEntries();
    //}



    public void ClearField()
    {
        ddlvesselCP.SelectedValue = "0";
        txtdatavalue.Text = "";
      // ddldatatypeCP.SelectedValue = "0";
    }


    protected void lbtnaddentry_Click(object sender, ImageClickEventArgs e)
    {
        this.SetFocus("ctl00_MainContent_txtdatavalue");

        OperationMode = "Add CP Entries";

        ClearField();

        string AddRankCategory = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCPEntries", AddRankCategory, true);
    }
    
}
