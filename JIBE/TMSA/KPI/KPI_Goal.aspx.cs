using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.TMSA;
using Telerik.Web.UI;
using SMS.Business.Infrastructure;

public partial class TMSA_KPI_KPI_Goal : System.Web.UI.Page
{
    BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
    BLL_Infra_VesselLib bll_Vessel = new BLL_Infra_VesselLib();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string KPID = Request.QueryString["KPI_ID"];
            ViewState["KPID"] = KPID;

            if (!string.IsNullOrWhiteSpace(Request.QueryString["AppFor"]))
            {
                string KPI_ApplicableFor = Request.QueryString["AppFor"];
                ViewState["KPI_ApplicableFor"] = KPI_ApplicableFor;
            }

            if (!string.IsNullOrWhiteSpace(ViewState["KPI_ApplicableFor"].ToString()))
            {
                //Show/Hide the goal grids for Vessel/Vessel Type/Company/Fleet
                if (Convert.ToInt32(ViewState["KPI_ApplicableFor"]) == 1)       //Vessel
                {
                    rgdItems.Visible = true;
                    rgdFleetGoals.Visible = false;
                    rgdVesselTypeGoals.Visible = false;
                    companyDiv.Visible = false;
                    BindKPIGoals();
                }
                else if (Convert.ToInt32(ViewState["KPI_ApplicableFor"]) == 0)  //Fleet
                {
                    rgdItems.Visible = true;
                    rgdFleetGoals.Visible = false;
                    rgdVesselTypeGoals.Visible = false;
                    companyDiv.Visible = false;
                    BindFleetGoals();
                }
                else if (Convert.ToInt32(ViewState["KPI_ApplicableFor"]) == 2)   //Vessel Type
                {
                    rgdItems.Visible = false;
                    rgdFleetGoals.Visible = false;
                    rgdVesselTypeGoals.Visible = true;
                    companyDiv.Visible = false;
                    BindVesselTypeGoals();
                }
                else if (Convert.ToInt32(ViewState["KPI_ApplicableFor"]) == 3)   //Company
                {
                    rgdItems.Visible = false;
                    rgdFleetGoals.Visible = false;
                    rgdVesselTypeGoals.Visible = false;
                    companyDiv.Visible = true;
                    BindCompanyGoal();
                }
            }
        }
    }
    /// <summary>
    /// Description: This method returns the current Session ID. 
    /// <summary>
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    /// <summary>
    /// Description: This method binds the vessel list. 
    /// <summary>
    protected void BindKPIGoals()
    {
        try
        {
            DataTable dt = bll_Vessel.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            dt.Columns.Remove("code");
            dt.Columns.Remove("name");
            dt.Columns.Remove("fleetname");
            dt.Columns.Remove("Super_MailID");
            dt.Columns.Remove("TechTeam_MailID");
            dt.Columns.Remove("Vessel_Owner");
            dt.Columns.Remove("Vessel_Manager");
            //    DataTable dtable = bll_KPI.Get_Fleet_Vessel_List(dt, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID());

            DataTable dtable = objKPI.Get_KPI_DetailGoals(UDFLib.ConvertIntegerToNull(ViewState["KPID"].ToString()), dt, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID(), "VE").Tables[0];

            rgdItems.DataSource = dtable;
            rgdItems.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Description: This method creates a Datatable which will be used to save vessel/vessel type/fleet goal list to database. 
    /// <summary>
    public DataTable getGoalData(string txtTotal, string txtAverage, string hdnID, string gridID)
    {
        DataTable dtGridItems = new DataTable();

        try
        {
            dtGridItems.Columns.Add("Vessel_ID");
            dtGridItems.Columns.Add("Goal");
            dtGridItems.Columns.Add("Average_Goal");
            RadGrid RG = new RadGrid();
            RG = (RadGrid)(dvGrids.FindControl(gridID));
            if (RG != null)
            {
                foreach (GridDataItem dataItem in RG.MasterTableView.Items)
                {
                    TextBox txtItem_Amount = (TextBox)(dataItem.FindControl(txtTotal));
                    TextBox txtItem_Average = (TextBox)(dataItem.FindControl(txtAverage));
                    HiddenField hdnVessel_Id = (HiddenField)(dataItem.FindControl(hdnID));
                    DataRow dritem = dtGridItems.NewRow();
                    dritem["Goal"] = txtItem_Amount.Text == "" ? Convert.DBNull : Convert.ToDecimal(txtItem_Amount.Text);
                    dritem["Average_Goal"] = txtItem_Average.Text == "" ? Convert.DBNull : Convert.ToDecimal(txtItem_Average.Text);
                    dritem["Vessel_ID"] = hdnVessel_Id.Value;
                    dtGridItems.Rows.Add(dritem);

                }
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


        return dtGridItems;
    }
    /// <summary>
    /// Description: This method is used to save Goal
    /// <summary>
    protected void Save_Click(object sender, EventArgs e)
    {

        string txtTotal = "";
        string txtAverage = "";
        string hdnID = "";
        string rgdID = "";
        string Goal_Applicable_For = "";
        DataTable dt = new DataTable();
        
        try
        {
            //Pass seperate values for the above variables based on the value selected in "Applicable For" dropdown.
            if (Convert.ToInt32(ViewState["KPI_ApplicableFor"]) == 0)
            {
                txtTotal = "txtItemFleet_Amount";
                txtAverage = "txtAvgFleet_Amount";
                hdnID = "hdnFleet_Id";
                rgdID = "rgdFleetGoals";
                Goal_Applicable_For = "FT";
            }
            else if (Convert.ToInt32(ViewState["KPI_ApplicableFor"]) == 1)
            {
                txtTotal = "txtItem_Amount";
                txtAverage = "txtAverage";
                hdnID = "hdnVessel_Id";
                rgdID = "rgdItems";
                Goal_Applicable_For = "VE";
            }
            else if (Convert.ToInt32(ViewState["KPI_ApplicableFor"]) == 2)
            {
                txtTotal = "txtItemVesselType_Amount";
                txtAverage = "txtItemAvgVesselType_Amount";
                hdnID = "hdnVesselType_Id";
                rgdID = "rgdVesselTypeGoals";
                Goal_Applicable_For = "VT";
            }
            else if (Convert.ToInt32(ViewState["KPI_ApplicableFor"]) == 3)
            {
                txtTotal = "";
                txtAverage = "";
                hdnID = "";
                rgdID = "rgdCompanyGoals";
                Goal_Applicable_For = "CY";
            }


        
            if (Goal_Applicable_For != "CY")
                dt = getGoalData(txtTotal, txtAverage, hdnID, rgdID);
            else    //As the company goal is not populated in radgrid so create a datatable in codebehind to pass the INSERT_KPI_GoalDetail method.
            {
                dt.Columns.Add("Vessel_ID");
                dt.Columns.Add("Goal");
                dt.Columns.Add("Average_Goal");
                DataRow dritem = dt.NewRow();
                dritem["Goal"] = txtItemCompany_Amount.Text == "" ? Convert.DBNull : Convert.ToDecimal(txtItemCompany_Amount.Text);
                dritem["Average_Goal"] = txtAvgCompany.Text == "" ? Convert.DBNull : Convert.ToDecimal(txtAvgCompany.Text);
                dritem["Vessel_ID"] = 1;
                dt.Rows.Add(dritem);

            }


            if (dt.Rows.Count > 0 && Convert.ToInt32(ViewState["KPID"].ToString())!=0)  //Handle the save functionality of already created KPIs.
            {
                objKPI.INSERT_KPI_GoalDetail(Convert.ToInt32(ViewState["KPID"].ToString()), dt, UDFLib.ConvertToInteger(Session["UserID"].ToString()), Goal_Applicable_For);
                ltMessage.Text = "Updated successfully!";
            }
            //Display message when Goal button is clicked without creating a KPI.
            else
                ltMessage.Text = "Please create a KPI!";
        
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Description: This method is used to bind the fleet name and respective goals
    /// <summary>
    protected void BindFleetGoals()
    {
        try
        {
            DataTable dt = bll_Vessel.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            dt.Columns.Remove("code");
            dt.Columns.Remove("name");
            dt.Columns.Remove("fleetname");
            dt.Columns.Remove("Super_MailID");
            dt.Columns.Remove("TechTeam_MailID");
            dt.Columns.Remove("Vessel_Owner");
            dt.Columns.Remove("Vessel_Manager");
            //    DataTable dtable = bll_KPI.Get_Fleet_Vessel_List(dt, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID());

            DataTable dtable = objKPI.Get_KPI_DetailGoals(UDFLib.ConvertIntegerToNull(ViewState["KPID"].ToString()), dt, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID(), "FT").Tables[0];
            rgdFleetGoals.Visible = true;
            rgdFleetGoals.DataSource = dtable;
            rgdFleetGoals.DataBind();

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Description: This method is used to bind the Vessel Types and respective goals
    /// <summary>
    protected void BindVesselTypeGoals()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PID");
            DataTable dtable = objKPI.Get_KPI_DetailGoals(UDFLib.ConvertIntegerToNull(ViewState["KPID"].ToString()), dt, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID(), "VT").Tables[0];
            rgdVesselTypeGoals.Visible = true;
            rgdVesselTypeGoals.DataSource = dtable;
            rgdVesselTypeGoals.DataBind();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Description: This method is used to bind the Company and respective goal
    /// <summary>
    protected void BindCompanyGoal()
    {
        try {
            DataTable dt = new DataTable();
            dt.Columns.Add("CID");
            DataTable dtable = objKPI.Get_KPI_DetailGoals(UDFLib.ConvertIntegerToNull(ViewState["KPID"].ToString()), dt, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID(), "CY").Tables[0];
            if(dtable.Rows.Count>0)
            {
                companyDiv.Visible = true;
                txtItemCompany_Amount.Text = dtable.Rows[0]["GOAL"].ToString();
                txtAvgCompany.Text = dtable.Rows[0]["Average_Goal"].ToString();
            }
        }
        catch(Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Description: This method is used to close the KPI Goal page on click of Cancel button
    /// <summary>
    protected void Close(object sender, EventArgs e)
    {
        string msg2 = "CloseGoal()";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
    }

}