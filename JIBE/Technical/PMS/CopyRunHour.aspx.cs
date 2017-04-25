using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PMS;
using System.Data;
using SMS.Business.Infrastructure;
using System.Web.Script.Serialization;
using SMS.Business.PURC;
public partial class Technical_PMS_CopyRunHour : System.Web.UI.Page
{

    public BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
    public BLL_PMS_Job_Status objJobStatus = new BLL_PMS_Job_Status();
    //Page Events
    #region PageEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Methods to load Fleet and Vessel Dropdown
            BindFleets();
            BindVessels();
            btnSave.Visible = false;
            //btnSearch.Visible = false;

            //If page is call from another page
            if (Request.QueryString["sourceid"] != null)
            {
                DataTable dt = objJobs.PMS_Get_SystemSubsystemInfo(int.Parse(Convert.ToString(Request.QueryString["sourceid"])), Convert.ToString(Request.QueryString["systemtype"]));
                lblSourceName.Text = dt.Rows[0]["Title"].ToString();
                ddlFleet.Visible = false;
                lblFleet.Visible = false;
                ddlVessel.SelectedValue = dt.Rows[0]["Vessel_Code"].ToString();
                ddlVessel.Enabled = false;
                btnSearch.Visible = false;
                lblSystemSubsystem.Visible = true;
                lblSourceName.Visible = true;




                btnSave.Visible = true;
                btnSearch.Visible = false;

                hdfQueryStringEquipmentID.Value = Request.QueryString["sourceid"].ToString();
                if (Request.QueryString["systemtype"].ToString() == "S")
                {
                    hdfQueryStringEquipmentType.Value = "1";//To Represent a System
                    BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
                    string systemid = Request.QueryString["sourceid"].ToString();
                    DataSet ds = objBLLPurc.LibraryCatalogueList(systemid);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Run_Hour"].ToString() == "" || ds.Tables[0].Rows[0]["Run_Hour"].ToString() == "0")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "RunHourFailed", "alert('Selected System is not Running Hour based');", true);
                            btnSave.Enabled = false;
                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "bindTreeSource", "bindBasedOnQueryStringTreeSource();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "DisplayOnlySource", "DisplayOnlySource();", true);
                        }
                    }

                }

                if (Request.QueryString["systemtype"].ToString() == "SS")
                {
                    hdfQueryStringEquipmentType.Value = "2";//To Represent a Subsystem
                    BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
                    string subsystemid = Request.QueryString["sourceid"].ToString();
                    DataSet ds = objBLLPurc.LibrarySubCatalogueList(subsystemid);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Run_Hour"].ToString() == "" || ds.Tables[0].Rows[0]["Run_Hour"].ToString() == "0")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "RunHourFailed", "alert('Selected Sub-system is not Running Hour based');", true);
                            btnSave.Enabled = false;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "bindTreeSource", "bindBasedOnQueryStringTreeSource();", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "DisplayOnlySource", "DisplayOnlySource();", true);
                        }
                    }

                }




            }
            //Current page is the active Page
            else
            {
                lblSystemSubsystem.Visible = false;
                lblSourceName.Visible = false;
                btnSave.Visible = false;
            }

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        string EquipmentType = "SubSystem";
        if (!string.IsNullOrEmpty(hdf2.Value) && hdf2.Value == "System")
            EquipmentType = "System";
        int EquipmentID = 0;
        int ParentEquipment = 0;

        if (!hdfSourceID.Value.Contains(','))
            EquipmentID = Convert.ToInt32(hdfSourceID.Value);
        else
        {
            EquipmentID = Convert.ToInt32(hdfSourceID.Value.Split(',')[1]);
            ParentEquipment = Convert.ToInt32(hdfSourceID.Value.Split(',')[0]);
        }

        bool CheckResult = false;
        //Selected Equipment which does not have its own Run Hour, which inherits from a parent
        //DataTable dtInheritsRunHour = objJobs.PMS_GET_CheckIfEquipmentIsDirectlyRunHourBased(EquipmentID, EquipmentType);
        //if (dtInheritsRunHour != null && dtInheritsRunHour.Rows.Count > 0)
        //{
        //    CheckResult = true;
        //    if (EquipmentType == "System")
        //        CheckResult = false;
        //}

        //if (CheckResult == false)
        //{
        BindDestinationGrid();
        if (gvDestination.Rows.Count > 0)
            btnSave.Visible = true;
        //    else
        //        btnSave.Visible = false;
        //}
        //else
        //{
        //    btnSave.Visible = false;
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "NoRunningHours", "NoRunningHours();", true);
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "TreeSource", "bindTreeSource();", true);
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "DisplayPage", "DisplayPage();", true);
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "HideDestination", "HideDestination();", true);

        //}
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDestination.DataSource = null;
        gvDestination.DataBind();
        BindVessels();
        ddlVessel.SelectedValue = "0";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "DestroyTree", "DestroyTree();", true);

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("VesselID", typeof(int));
        dt.Columns.Add("EquipmentID", typeof(int));
        dt.Columns.Add("ParentEquipmentID", typeof(int));
        dt.Columns.Add("Dest_LocID", typeof(int));
        dt.Columns.Add("DestParent_LocID", typeof(int));
        dt.Columns.Add("EquipmentType", typeof(string));
        dt.Columns.Add("ParentEquipmentType", typeof(string));
        dt.Columns.Add("ActiveStatus", typeof(int));


        int EquipmentID = 0;
        int ParentEquipment = 0;

        if (!hdfSourceID.Value.Contains(','))
            EquipmentID = Convert.ToInt32(hdfSourceID.Value);
        else
        {
            EquipmentID = Convert.ToInt32(hdfSourceID.Value.Split(',')[1]);
            ParentEquipment = Convert.ToInt32(hdfSourceID.Value.Split(',')[0]);
        }




        if (string.IsNullOrEmpty(hdfSourceID.Value))
            hdfSourceID.Value = "0";

        //int SourceID = int.Parse(hdfSourceID.Value);
        ViewState["SourceID"] = hdfSourceID.Value;

        if (gvDestination != null && gvDestination.Rows.Count > 0)
        {
            for (int i = 0; i < gvDestination.Rows.Count; i++)
            {

                CheckBox chkboxIsActive = (CheckBox)gvDestination.Rows[i].FindControl("checkRow");
                Label lblID = (Label)gvDestination.Rows[i].FindControl("lblSystemID");
                Label lblLevel = (Label)gvDestination.Rows[i].FindControl("lblLevel");
                string ParentEquipmentType = "SubSystem";
                if (string.IsNullOrEmpty(lblLevel.Text))
                    ParentEquipmentType = "System";
                int DEquipmentID = 0;
                int DParentEquipment = 0;

                if (!lblID.Text.Contains(','))
                    DEquipmentID = Convert.ToInt32(lblID.Text);
                else
                {
                    DEquipmentID = Convert.ToInt32(lblID.Text.Split(',')[1]);
                    DParentEquipment = Convert.ToInt32(lblID.Text.Split(',')[0]);
                }
                int IsChecked = 0;
                if (chkboxIsActive.Checked == true)
                {
                    IsChecked = 1;
                    dt.Rows.Add(int.Parse(ddlVessel.SelectedValue), EquipmentID, ParentEquipment, DEquipmentID, DParentEquipment, "", ParentEquipmentType, IsChecked);
                }
                else
                {
                    IsChecked = 0;
                    dt.Rows.Add(int.Parse(ddlVessel.SelectedValue), EquipmentID, ParentEquipment, DEquipmentID, DParentEquipment, "", ParentEquipmentType, IsChecked);
                }
            }

        }
        //}
        int result = 0;
        int uncheckedall = 0;

        if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(hdf2.Value))
        {
            result = objJobs.PMS_INS_EquipmentRunHours(dt, UDFLib.ConvertToInteger(Session["USERID"].ToString()), hdf2.Value);
        }
        else
        {
            result = objJobs.PMS_INS_EquipmentRunHours(dt, UDFLib.ConvertToInteger(Session["USERID"].ToString()), hdf2.Value);
            if (result <= 0)
            {
                uncheckedall = 1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MessageFailureIfNoDestinationSelected", "MessageFailureIfNoDestinationSelected();", true);
            }
        }

        if (result > 0)
        {
            if (Request.QueryString["sourceid"] != null)
            {
                hdfSourceID.Value = Request.QueryString["sourceid"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "bindTreeSource", "bindBasedOnQueryStringTreeSource();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DisplayOnlySource", "DisplayOnlySource();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "SuccessMessage();", true);

            }
            else
            {
                BindDestinationGrid();
                //ddlFleet.SelectedIndex = 0;
                //ddlVessel.SelectedIndex = 0;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "DestroyTree", "DestroyTree();", true);

                //gvDestination.DataSource = null;
                //gvDestination.DataBind();

                //btnSave.Visible = false;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "DisplayOnlySource", "DisplayOnlySource();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "SuccessMessage", "SuccessMessage();", true);

            }
        }
        else
        {
            if (uncheckedall == 0)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FailureMessage", "FailureMessage();", true);
        }
        BindDestinationGrid();
        txtSearch.Text = "";
    }

    protected void btnFreeTextSearch_Click(object sender, EventArgs e)
    {
        GridFreeTextSearch();
    }
    protected void gvDestination_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDescription = (Label)e.Row.FindControl("lblSystemDescription");
            CheckBox chkbox = (CheckBox)e.Row.FindControl("checkRow");
            string SystemID = Convert.ToString(gvDestination.DataKeys[e.Row.RowIndex].Values[0]);
            string ParentSystemID = ViewState["SourceID"].ToString();

            string LinkedID = Convert.ToString(gvDestination.DataKeys[e.Row.RowIndex].Values[3]);

            if (LinkedID.Contains(","))
                LinkedID = LinkedID.Replace("0,", "");


            if (ParentSystemID == SystemID)
                chkbox.Enabled = false;


            if (hdfSourceID.Value == LinkedID)
            {
                chkbox.Enabled = true;

            }
            else
            {

                if (chkbox.Checked == true)
                {
                    chkbox.Checked = false;
                    chkbox.Enabled = false;
                }
                

            }



            lblDescription.Attributes.Add("onMouseOver", "javascript:js_ShowToolTip('" + Convert.ToString(gvDestination.DataKeys[e.Row.RowIndex].Values[2]) + "',event,this)");


        }
    }
    #endregion


    //Methods Declaration
    #region Page Methods

    private void BindFleets()
    {
        BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }
    private void BindVessels()
    {
        BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        DataTable dtVessel = objVessel.Get_VesselList(int.Parse(ddlFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
        ddlVessel.Items.Clear();
        ddlVessel.DataSource = dtVessel;
        ddlVessel.DataTextField = "Vessel_name";
        ddlVessel.DataValueField = "Vessel_id";
        ddlVessel.DataBind();
        ListItem li = new ListItem("--SELECT ALL--", "0");
        ddlVessel.Items.Insert(0, li);
    }

    private void BindDestinationGrid()
    {
        gvDestination.DataSource = null;
        gvDestination.DataBind();
        DataTable dt = objJobs.PMS_Get_DestinationSystemSubsystemTreeData(int.Parse(ddlVessel.SelectedValue));
        ViewState["DestinationGrid"] = dt;
        // int SourceID = int.Parse(hdfSourceID.Value);
        DataView view = new DataView(dt);
        view.Sort = "SystemCode ASC";
        dt = view.ToTable();
        if (!string.IsNullOrWhiteSpace(hdfSourceID.Value))
        {
            ViewState["SourceID"] = hdfSourceID.Value;

            gvDestination.DataSource = dt;
            gvDestination.DataBind();

            if (dt != null && dt.Rows.Count > 0)
                btnSave.Visible = true;


        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "TreeSelectionMsg", "TreeSelectionMsg();", true);
            btnSave.Visible = false;
        }
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "TreeSource", "bindTreeSource();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DisplayPage", "DisplayPage();", true);
        if (string.IsNullOrWhiteSpace(hdfSourceID.Value))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideDestination", "HideDestination();", true);
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SetFocusOnTreeSelectNode", "SetFocusOnTreeSelectNode();", true);
    }

    private void GridFreeTextSearch()
    {
        DataTable dt = (DataTable)ViewState["DestinationGrid"];
        DataTable dt1 = new DataTable();


        if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
        {

            DataRow[] result1 = dt.Select("SystemDescription LIKE '%" + txtSearch.Text.Trim() + "%' or Title LIKE '%" + txtSearch.Text.Trim() + "%'");
            //DataRow[] result = dt.Select();
            if (result1 != null && result1.Length > 0)
                dt1 = result1.CopyToDataTable();

        }
        else
        {
            dt1 = dt;
        }

        DataView view = new DataView(dt1);
        if (view.Table.Rows.Count > 0)      //Added in JIT : 11565 
        {
            view.Sort = "SystemCode ASC";
            dt1 = view.ToTable();
        }


        gvDestination.DataSource = dt1;
        gvDestination.DataBind();
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "TreeSource", "bindTreeSource();", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DisplayPage", "DisplayPage();", true);
        if (string.IsNullOrWhiteSpace(hdfSourceID.Value))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideDestination", "HideDestination();", true);
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SetFocusOnTreeSelectNode", "SetFocusOnTreeSelectNode();", true);
    }
    #endregion


}