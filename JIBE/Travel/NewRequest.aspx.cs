//System libararies
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//Custom libararies
using SMS.Business.TRAV;
using SMS.Properties;
using SMS.Business.Crew;
using System.Text;
using System.IO;

public partial class NewRequest : System.Web.UI.Page
{
    StringBuilder Changes_Remark = new StringBuilder("");
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserAccessValidation();
            if (!Page.IsPostBack)
            {
                DataTable dtPax = new DataTable();

                if (!string.IsNullOrEmpty(Request.QueryString["Request_ID"]))
                {
                    lblPageTitle.Text = "Travel Request : " + Request.QueryString["Request_ID"];
                    BLL_TRV_TravelRequest objreq = new BLL_TRV_TravelRequest();

                    //disable the save button if request has been approved

                    DataTable dtsts = objreq.Get_Travel_Request_Status(Convert.ToInt32(Request.QueryString["Request_ID"].ToString()), Convert.ToInt32(Session["userid"]));
                    int ReqSts = Convert.ToInt32(dtsts.Rows[0]["currentStatus"]);
                    if (ReqSts == 0)
                    {
                        objUA.Edit = 0;
                        cmdSaveRequest.Enabled = false;
                        GridView1.Enabled = false;
                        GridView2.Enabled = false;
                        GrdFlight.Enabled = false;
                        Vessel_List.Enabled = false;
                        txtSearchPax.Enabled = false;
                        GrdCrew.Enabled = false;

                    }

                    DataSet dspxReq = objreq.Get_RequestPax(UDFLib.ConvertToInteger(Request.QueryString["Request_ID"]));
                    dspxReq.Tables[0].PrimaryKey = new DataColumn[] { dspxReq.Tables[0].Columns["ID"] };
                    dtPax = dspxReq.Tables[0];
                    try
                    {
                        cmbTravelClass.Items.FindByValue(dspxReq.Tables[1].Rows[0]["classOfTravel"].ToString()).Selected = true;
                        chkSeaman.Checked = UDFLib.ConvertToInteger(dspxReq.Tables[1].Rows[0]["isSeaman"]) == 0 ? false : true;


                        Vessel_List.SelectedValue = UDFLib.ConvertToInteger(dspxReq.Tables[1].Rows[0]["vessel"]).ToString();
                    }
                    catch
                    { }


                    ViewState["DataTable"] = dtPax;
                    DataView view_On = new DataView(dtPax);
                    view_On.RowFilter = "ON_OFF=1";

                    GridView1.DataSource = view_On;
                    GridView1.DataBind();

                    DataView view_Off = new DataView(dtPax);
                    view_Off.RowFilter = "ON_OFF <> 1";

                    GridView2.DataSource = view_Off;
                    GridView2.DataBind();

                    BindFlightDetails();

                    ViewState["PaxDetails_Original"] = dtPax;


                }
                else
                {

                    MakePaxList();

                    rdoReturn_SelectedIndexChanged(null, null);
                    ViewState["IsClose"] = "N";


                    try
                    {
                        if (Request.QueryString["crewid"] != null)
                        {

                            AddToDataTable(Convert.ToInt32(Request.QueryString["crewid"].ToString()), 0, UDFLib.ConvertToInteger(Request.QueryString["VoyID"].ToString()));
                            Search_Pax(Convert.ToInt32(Request.QueryString["crewid"].ToString()), 0, 0, "");
                            //dvAddStaff.Visible = false;
                        }
                        else if (Request.QueryString["EventID"] != null)
                        {
                            AddToDataTable(0, Convert.ToInt32(Request.QueryString["EventID"].ToString()), 0);
                            Search_Pax(0, Convert.ToInt32(Request.QueryString["EventID"].ToString()), 0, "");
                            //dvAddStaff.Visible = false;
                        }
                        else
                        {
                            Search_Pax(0, 0, 0, "");
                        }

                    }
                    catch { }
                }
            }


        }
        catch { }
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
    private void BindFlightDetails()
    {
        BLL_TRV_TravelRequest objreq = new BLL_TRV_TravelRequest();
        DataTable dtFlight = objreq.Get_RequestFlight(UDFLib.ConvertToInteger(Request.QueryString["Request_ID"]));

        ViewState["SegmentDetails_Original"] = dtFlight;

        GrdFlight.DataSource = dtFlight;
        GrdFlight.DataBind();




    }

    private void CheckForChangesInRequest()
    {

        DataTable dtFlight = (DataTable)ViewState["SegmentDetails_Original"];

        int i = 0;
        string strNewSegment = "";
        foreach (GridViewRow gr in GrdFlight.Rows)
        {

            UserControl_ctlAirPortList txtFrom1 = (gr.FindControl("txtFrom1") as UserControl_ctlAirPortList);
            UserControl_ctlAirPortList txtTo1 = (gr.FindControl("txtTo1") as UserControl_ctlAirPortList);
            TextBox txtDepDate1 = (gr.FindControl("txtDepDate1") as TextBox);
            DropDownList cmbDepHours1 = (gr.FindControl("cmbDepHours1") as DropDownList);
            DropDownList cmbDepMins1 = (gr.FindControl("cmbDepMins1") as DropDownList);
            if (i < dtFlight.Rows.Count)
            {
                DataRow dr = dtFlight.Rows[i];

                StringBuilder sbEachSegment = new StringBuilder("");

                if (txtFrom1.Text.Trim() != dr["travelOrigin"].ToString().Trim())
                {
                    sbEachSegment.Append("&nbsp; Old From : " + dr["travelOrigin"].ToString() + "  ,  ");
                    sbEachSegment.Append("New From : " + txtFrom1.Text);
                }


                if (txtTo1.Text != dr["travelDestination"].ToString())
                {
                    sbEachSegment.Append("<br>&nbsp; Old To : " + dr["travelDestination"].ToString() + "  ,  ");
                    sbEachSegment.Append("New To : " + txtTo1.Text);
                }

                string Deptdt = "", DeptDT_old = "";
                string sMin = "", sMin_old = "";
                string sHour = "", sHour_old = "";
                if (txtDepDate1.Text != dr["departureDate"].ToString() || cmbDepMins1.SelectedValue != dr["PrefDepmins"].ToString() || cmbDepHours1.SelectedValue != dr["PrefDephours"].ToString())
                {

                    DeptDT_old = dr["departureDate"].ToString();
                    Deptdt = txtDepDate1.Text;
                    sMin_old = dr["PrefDepmins"].ToString();
                    sMin = cmbDepMins1.Text;
                    sHour_old = dr["PrefDephours"].ToString();
                    sHour = cmbDepHours1.Text;
                    sbEachSegment.Append("<br>&nbsp; Old Dept Date : " + DeptDT_old + " : " + sHour_old + " : " + sMin_old + "  ,  New Date : " + Deptdt + " : " + sHour + " : " + sMin);
                }

                if (sbEachSegment.ToString() != "")
                {
                    Changes_Remark.Append("<br><br><b>Following changes in segment : " + (i + 1) + "</b><br><br> " + sbEachSegment.ToString());
                }
            }
            else
            {
                strNewSegment += txtFrom1.Text + "&nbsp;&nbsp;&nbsp;" + txtTo1.Text + "&nbsp;&nbsp;&nbsp;" + txtDepDate1.Text + " : " + cmbDepHours1.Text + " : " + cmbDepMins1.Text + "<br>";
            }

            i++;
        }

        if (strNewSegment != "")
        {
            Changes_Remark.Append("<br><b>Segment added : </b><br><br>" + strNewSegment);
        }
        string RemovedSegment = "";
        if (dtFlight.Rows.Count > GrdFlight.Rows.Count)
        {
            for (int iFlt = 0; iFlt < dtFlight.Rows.Count; iFlt++)
            {
                if (iFlt >= GrdFlight.Rows.Count)
                {
                    RemovedSegment += dtFlight.Rows[iFlt]["travelOrigin"].ToString() + "&nbsp;&nbsp;&nbsp;" + dtFlight.Rows[iFlt]["travelDestination"].ToString() + "&nbsp;&nbsp;&nbsp;" + dtFlight.Rows[iFlt]["departureDate"].ToString() + " : " + dtFlight.Rows[iFlt]["PrefDephours"].ToString() + " : " + dtFlight.Rows[iFlt]["PrefDepmins"].ToString() + "<br>";

                }
            }
        }

        if (RemovedSegment != "")
        {
            Changes_Remark.Append("<br><b>Removed segment : </b><br><br>" + RemovedSegment);
        }

        #region ------- check fot pax -------------

        DataTable dtPax_Original = (DataTable)ViewState["PaxDetails_Original"];
        DataTable dtPax = (DataTable)ViewState["DataTable"];


        StringBuilder sbNewPax = new StringBuilder();
        foreach (DataRow drPax in dtPax.Rows)
        {

            if (!dtPax_Original.Rows.Contains(drPax["ID"]))
            {
                sbNewPax.Append("  " + drPax["Name"].ToString() + "<br>");
            }

        }
        if (sbNewPax.ToString().Trim().Length > 0)
        {
            Changes_Remark.Append("<br><br><b>New pax added :  </b><br><br> ");
            Changes_Remark.Append(sbNewPax.ToString());
        }



        StringBuilder sbDeletedpax = new StringBuilder();
        foreach (DataRow drPax in dtPax_Original.Rows)
        {

            if (!dtPax.Rows.Contains(drPax["ID"]))
            {
                sbDeletedpax.Append("  " + drPax["Name"].ToString() + "<br>");
            }

        }
        if (sbDeletedpax.ToString().Trim().Length > 0)
        {
            Changes_Remark.Append("<br><br><b>Pax removed :</b> <br><br> ");
            Changes_Remark.Append(sbDeletedpax.ToString());
        }

        #endregion check for pax

    }




    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void cmdClose_onClick(object source, EventArgs e)
    {
        //dvAddStaff.Visible = false;
    }

    protected void cmdSaveRequest_Click(object sender, EventArgs e)
    {
        if (UDFLib.ConvertToInteger(Vessel_List.SelectedValue) > 0)
        {





            if (!string.IsNullOrEmpty(Request.QueryString["Request_ID"]))
            {
                CheckForChangesInRequest();
            }
            string isReturn;
            int CreatedBy, isSeaman;
            int VoyageID = UDFLib.ConvertToInteger(Request.QueryString["VoyageID"]);
            int EventID = UDFLib.ConvertToInteger(Request.QueryString["EventID"]);


            if (chkSeaman.Checked)
                isSeaman = 1;
            else
                isSeaman = 0;

            if (chkReturn.Checked)
                isReturn = "RETURN";
            else
                isReturn = "ONEWAY";

            DataTable dtPax = (DataTable)ViewState["DataTable"];

            if (dtPax.Rows.Count > 0)
            {



                BLL_TRV_TravelRequest TReq = new BLL_TRV_TravelRequest();
                TRV_Request ReqProperties = new TRV_Request();
                try
                {
                    int i, requestid = 0; Boolean Issavingpax = true;
                    CreatedBy = Convert.ToInt32(Session["USERID"].ToString());


                    foreach (GridViewRow gr in GrdFlight.Rows)
                    {
                        UserControl_ctlAirPortList txtFrom1 = (gr.FindControl("txtFrom1") as UserControl_ctlAirPortList);
                        UserControl_ctlAirPortList txtTo1 = (gr.FindControl("txtTo1") as UserControl_ctlAirPortList);
                        TextBox txtDepDate1 = (gr.FindControl("txtDepDate1") as TextBox);
                        DropDownList cmbDepHours1 = (gr.FindControl("cmbDepHours1") as DropDownList);
                        DropDownList cmbDepMins1 = (gr.FindControl("cmbDepMins1") as DropDownList);

                        if (string.IsNullOrEmpty(txtFrom1.Text))
                        {
                            string js = "alert('Please select from !');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "txtFrom1", js, true);
                            
                            return;
                        }


                        if (string.IsNullOrEmpty(txtTo1.Text))
                        {
                            string js = "alert('Please select To !');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "txtTo1", js, true);
                           
                            return;
                        }




                        if (txtFrom1.Text.Trim() != "" && txtTo1.Text.Trim() != ""  && Issavingpax) // save pax and first segment
                        {
                            Issavingpax = false;

                            for (i = 0; i < dtPax.Rows.Count; i++)
                            {

                               

                                //to add all pax to the request after the default one, i.e. first / lead pax
                                if (i == 0)
                                {
                                    StringBuilder sbMainChanges_Remark = new StringBuilder("");
                                    if (Changes_Remark.ToString() != "")
                                    {
                                        sbMainChanges_Remark.Append("<div style='font-size:11px;font-family:Tahoma'>");
                                        sbMainChanges_Remark.Append(Changes_Remark.ToString());
                                        sbMainChanges_Remark.Append("</div>");
                                    }

                                    ReqProperties.StaffID = Convert.ToInt32(dtPax.Rows[i]["id"].ToString());

                                    ReqProperties.Travel_Class = cmbTravelClass.SelectedValue;
                                    ReqProperties.Is_Seaman_Ticket = isSeaman;
                                    ReqProperties.Travel_Type = isReturn;


                                    ReqProperties.isPersonal_Ticket = 0;

                                    ReqProperties.Travel_Origin = txtFrom1.Text;
                                    ReqProperties.Departure_Date = txtDepDate1.Text;
                                    ReqProperties.Travel_Destination = txtTo1.Text;
                                    //ReqProperties.Return_Date = txtArrDate1.Text;
                                    //ReqProperties.Preferred_Airline = txtPrefAirline1.Text;
                                    //ReqProperties.Preferred_Departure_Time = txtPrefTime1.Text;
                                    ReqProperties.Created_By = CreatedBy;
                                    ReqProperties.Remarks = txtRequestRemarks.Text;

                                    ReqProperties.PrefDepMin = cmbDepMins1.SelectedItem.Text;
                                    ReqProperties.PrefDepHrs = cmbDepHours1.SelectedItem.Text;

                                    requestid = TReq.CreateTravelRequest(ReqProperties, UDFLib.ConvertToInteger(Vessel_List.SelectedValue), UDFLib.ConvertToInteger(Request.QueryString["Request_ID"])
                                        , sbMainChanges_Remark.ToString());
                                }
                                else
                                {
                                    TReq.AddPaxToTravelRequest(requestid, Convert.ToInt32(dtPax.Rows[i]["id"].ToString()), CreatedBy, VoyageID, EventID);
                                }
                            }
                        }

                        else if (txtFrom1.Text.Trim() != "" && txtTo1.Text.Trim() != "")  //ADDING from  SECOND segment  DETAIL IF VALID 
                        {
                            ReqProperties.Travel_Class = cmbTravelClass.SelectedValue;
                            ReqProperties.Is_Seaman_Ticket = isSeaman;
                            ReqProperties.Travel_Type = isReturn;

                            ReqProperties.isPersonal_Ticket = 0;

                            ReqProperties.Travel_Origin = txtFrom1.Text;
                            ReqProperties.Departure_Date = txtDepDate1.Text;
                            ReqProperties.Travel_Destination = txtTo1.Text;
                            //ReqProperties.Return_Date = txtArrDate2.Text;
                            //ReqProperties.Preferred_Airline = txtPrefAirline2.Text;
                            //ReqProperties.Preferred_Departure_Time = txtPrefTime2.Text;
                            ReqProperties.Created_By = CreatedBy;
                            ReqProperties.Remarks = txtRequestRemarks.Text;
                            ReqProperties.PrefDepMin = cmbDepMins1.SelectedItem.Text;
                            ReqProperties.PrefDepHrs = cmbDepHours1.SelectedItem.Text;


                            //ONLY LEADPAX DETAIL HAS TO BE SAVED
                            ReqProperties.StaffID = Convert.ToInt32(dtPax.Rows[0]["id"].ToString());

                            TReq.AddFlightToTravelRequest(requestid, ReqProperties);
                        }

                    }


                    if (!string.IsNullOrEmpty(Request.QueryString["Request_ID"]))
                    {
                        string js = "window.opener.location.href='RequestList.aspx';window.open('','_self');window.close();";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg2", js, true);

                    }
                    else
                    {

                        Response.Redirect("RequestList.aspx");
                    }
                }
                catch { throw; }
                finally { TReq = null; ReqProperties = null; }



            }

            else
            {

                string msgPax = "alert('Add at least 1 Pax !');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msgPax", msgPax, true);

            }

        }
        else
        {
            string js = "alert('Please select vessel !');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgvsl", js, true);

        }
    }

    protected void cmdGet_Click(object sender, EventArgs e)
    {
        Search_Pax(0, 0, 0, txtSearch.Text);
    }

    protected void GrdCrew_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "PrevColor=this.style.backgroundColor; this.style.backgroundColor='#AFCFEE'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=PrevColor;");
        }
    }

    protected void Search_Pax(int CrewID, int EventID, int VoyageID, string SearchText)
    {
        BLL_TRV_TravelRequest obj = new BLL_TRV_TravelRequest();
        DataSet ds = obj.Get_SearchCrew(CrewID, EventID, VoyageID, Convert.ToInt32(Session["USERID"].ToString()), SearchText);
        GrdCrew.DataSource = ds.Tables[0];
        GrdCrew.DataBind();

        // if this page will be launched from event then only select the vessel.previously it was selecting from crew voyage also.
        if (Request.QueryString["EventID"] != null)
        {
            //Vessel_List.Enabled = false;
            if (ds.Tables[0].Rows.Count > 0)
                Vessel_List.SelectedValue = ds.Tables[0].Rows[0]["Vessel_ID"].ToString();
        }

        else
        {
            Vessel_List.Enabled = true;
        }

    }

    protected void GrdCrew_RowCommand(object source, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToUpper() == "ADDPAX")
            {
                ViewState["IsClose"] = "N";
                AddToDataTable(Convert.ToInt32(e.CommandArgument), 0, 0);
            }
            if (e.CommandName.ToUpper() == "ADDPAXANDCLOSE")
            {
                ViewState["IsClose"] = "Y";
                AddToDataTable(Convert.ToInt32(e.CommandArgument), 0, 0);
            }


        }
        catch { }
    }

    /// Datatable to add staff list

    protected void GridView1_RowDataBound(Object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string CrewID = DataBinder.Eval(e.Row.DataItem, "ID").ToString();

            e.Row.Cells[0].Attributes.Add("onclick", "window.open('/" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "/crew/crewdetails.aspx?id=" + CrewID + "')");
            e.Row.Cells[0].CssClass = "popup-link-button";
            //e.Row.Cells[1].Visible = false;
            if (DataBinder.Eval(e.Row.DataItem, "ON_OFF").ToString() == "-1")
            {
                ((Label)e.Row.FindControl("lbeventSts")).Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header) { }
    }

    protected void imgDelete_Click(object s, EventArgs e)
    {
        int id = ((GridViewRow)((ImageButton)s).Parent.Parent).RowIndex;
    }

    private void MakePaxList()
    {
        DataTable dtPax = new DataTable();

        DataColumn pkCol = new DataColumn("ID");
        dtPax.Columns.Add(pkCol);
        dtPax.Columns.Add("Code");
        dtPax.Columns.Add("Name");
        dtPax.Columns.Add("Rank");
        dtPax.Columns.Add("PPNo");
        dtPax.Columns.Add("PPExpiry");
        dtPax.Columns.Add("SBNo");
        dtPax.Columns.Add("SBExpiry");
        dtPax.Columns.Add("PlaceOfIssue");
        dtPax.Columns.Add("Nationality");
        dtPax.Columns.Add("DateOfBirth");
        dtPax.Columns.Add("ON_OFF");
        dtPax.Columns.Add("Vessel_ID");
        dtPax.PrimaryKey = new DataColumn[] { pkCol };

        ViewState["DataTable"] = dtPax;
    }

    private void AddToDataTable(int CrewID, int EventID, int VoyageID)
    {
        try
        {
            BLL_TRV_TravelRequest obj = new BLL_TRV_TravelRequest();
            DataTable dtPax = (DataTable)ViewState["DataTable"];

            if (!dtPax.Rows.Contains(CrewID))
            {
                DataSet ds = obj.Get_SearchCrew(CrewID, EventID, VoyageID, Convert.ToInt32(Session["USERID"].ToString()), txtSearch.Text);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drCrew in ds.Tables[0].Rows)
                    {
                        //DataRow drCrew = ds.Tables[0].Rows[0];
                        if (!dtPax.Rows.Contains(drCrew["id"]))
                        {
                            DataRow dr = dtPax.NewRow();
                            dr["ID"] = drCrew["id"].ToString();
                            dr["Code"] = drCrew["staff_code"].ToString();
                            dr["Name"] = drCrew["staff_fullName"].ToString();
                            dr["Rank"] = drCrew["CurrentRank"].ToString();
                            dr["PPNo"] = drCrew["Passport_Number"].ToString();
                            dr["PPExpiry"] = drCrew["Passport_Expiry_Date"].ToString();
                            dr["SBNo"] = drCrew["Seaman_Book_Number"].ToString();
                            dr["SBExpiry"] = drCrew["Seaman_Book_Expiry_Date"].ToString();
                            dr["PlaceOfIssue"] = drCrew["Passport_PlaceOf_Issue"].ToString();
                            dr["Nationality"] = drCrew["Staff_Nationality"].ToString();
                            dr["DateOfBirth"] = drCrew["staff_birth_date"].ToString();
                            dr["ON_OFF"] = drCrew["ON_OFF"].ToString();
                            dr["Vessel_ID"] = drCrew["Vessel_ID"].ToString();
                            dtPax.Rows.Add(dr);
                        }
                    }

                    // do this just one time

                    DataView view_On = new DataView(dtPax);
                    view_On.RowFilter = "ON_OFF=1";

                    GridView1.DataSource = view_On;
                    GridView1.DataBind();

                    DataView view_Off = new DataView(dtPax);
                    view_Off.RowFilter = "ON_OFF <> 1";

                    GridView2.DataSource = view_Off;
                    GridView2.DataBind();

                    ViewState["DataTable"] = dtPax;

                    hdf_StaffIds.Value = ",";


                    foreach (DataRow dr in dtPax.Rows)
                    {
                        hdf_StaffIds.Value = hdf_StaffIds.Value + dr["id"].ToString() + ",";
                    }


                    if (ViewState["IsClose"].ToString() == "Y")
                    {
                        string js = "hideModal('dvpopup');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg3", js, true);
                    }

                    if (EventID < 1)
                    {
                        //string js = "alert('Pax added successfully');";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg2", js, true);
                    }

                    ///select the vessel in ddl based on first person's vessel
                    //if (ds.Tables[0].Rows.Count > 0)
                    //    Vessel_List.SelectedValue = dtPax.Rows[0]["Vessel_ID"].ToString();

                }
            }
            else
            {
                string js = "alert('Pax already added.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg2", js, true);

            }
        }
        catch { }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        DataTable dtPax = (DataTable)ViewState["DataTable"];

        DataRow dr = dtPax.Rows.Find(e.Keys["id"].ToString());

        if (dr != null)
        {
            dtPax.Rows.Remove(dr);
        }

        ViewState["DataTable"] = dtPax;

        hdf_StaffIds.Value = ",";


        foreach (DataRow drp in dtPax.Rows)
        {
            hdf_StaffIds.Value = hdf_StaffIds.Value + drp["id"].ToString() + ",";
        }


        DataView view_On = new DataView(dtPax);
        view_On.RowFilter = "ON_OFF=1";

        GridView1.DataSource = view_On;
        GridView1.DataBind();

        DataView view_Off = new DataView(dtPax);
        view_Off.RowFilter = "ON_OFF <> 1";

        GridView2.DataSource = view_Off;
        GridView2.DataBind();

    }

    protected void GrdCrew_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdCrew.PageIndex = e.NewPageIndex;
        Search_Pax(0, 0, 0, "");
    }

    protected void txtSearchPax_TextChanged(object sender, EventArgs e)
    {

        BLL_TRV_TravelRequest obj = new BLL_TRV_TravelRequest();
        DataSet ds = obj.Get_SearchCrew(0, 0, 0, Convert.ToInt32(Session["USERID"].ToString()), txtSearchPax.Text);
        if (ds.Tables[0].Rows.Count == 1)
        {
            int CrewID = UDFLib.ConvertToInteger(ds.Tables[0].Rows[0]["ID"].ToString());
            int VoyageID = 0;
            AddToDataTable(CrewID, 0, VoyageID);

        }
        else
        {
            txtSearch.Text = txtSearchPax.Text;
            GrdCrew.DataSource = ds.Tables[0];
            GrdCrew.DataBind();
            string js = "showModal('dvpopup');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", js, true);
        }


    }

    protected void rdoReturn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GrdFlight.Rows.Count < 5)
        {
            RadioButtonList objsender = (RadioButtonList)sender;
            DataTable dtFlight = CreateDtFlight();
            string strFrom = "", strTo = "";

            DataRow dr = null;
            foreach (GridViewRow gr in GrdFlight.Rows)
            {
                UserControl_ctlAirPortList txtFrom1 = (gr.FindControl("txtFrom1") as UserControl_ctlAirPortList);
                UserControl_ctlAirPortList txtTo1 = (gr.FindControl("txtTo1") as UserControl_ctlAirPortList);
                dr = dtFlight.NewRow();
                dr["travelOrigin"] = txtFrom1.Text;
                dr["travelDestination"] = txtTo1.Text;
                dr["departureDate"] = (gr.FindControl("txtDepDate1") as TextBox).Text;
                dr["PrefDephours"] = (gr.FindControl("cmbDepHours1") as DropDownList).SelectedValue;
                dr["PrefDepmins"] = (gr.FindControl("cmbDepMins1") as DropDownList).SelectedValue;
                dtFlight.Rows.Add(dr);
                strFrom = txtFrom1.Text;
                strTo = txtTo1.Text;
            }

            dr = dtFlight.NewRow();
            if (objsender != null)
            {
                if (objsender.SelectedValue == "1")
                {
                    if (strTo != "")
                        dr["travelOrigin"] = strTo;
                    if (strFrom != "")
                        dr["travelDestination"] = strFrom;
                }

            }

            dtFlight.Rows.Add(dr);

            GrdFlight.DataSource = dtFlight;
            GrdFlight.DataBind();
        }

    }

    private DataTable CreateDtFlight()
    {
        DataTable dtFlight = new DataTable();



        dtFlight.Columns.Add("requestID");
        dtFlight.Columns.Add("travelOrigin");
        dtFlight.Columns.Add("travelDestination");
        dtFlight.Columns.Add("departureDate");
        dtFlight.Columns.Add("returnDate");
        dtFlight.Columns.Add("preferredDepartureTime");
        dtFlight.Columns.Add("travelClass");
        dtFlight.Columns.Add("PrefDephours");
        dtFlight.Columns.Add("PrefDepmins");
        return dtFlight;
    }

    protected void GrdFlight_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Request.QueryString["Port_Name"] != null)
            {
                BLL_TRV_Airport al = new BLL_TRV_Airport();
                DataTable dt = al.GetAirPort(Request.QueryString["Port_Name"]).Tables[0];

                if (dt.Rows.Count > 0)
                    (e.Row.FindControl("txtFrom1") as UserControl_ctlAirPortList).Text = dt.Rows[0]["iata_code"].ToString();

            }
            if (Request.QueryString["Event_Date"] != null)
            {
                (e.Row.FindControl("txtDepDate1") as TextBox).Text = Convert.ToDateTime(Request.QueryString["Event_Date"]).ToString("dd-MMMM-yyyy");
            }
            try
            {

                (e.Row.FindControl("cmbDepHours1") as DropDownList).Items.FindByValue(DataBinder.Eval(e.Row.DataItem, "PrefDephours").ToString()).Selected = true;
                (e.Row.FindControl("cmbDepMins1") as DropDownList).Items.FindByValue(DataBinder.Eval(e.Row.DataItem, "PrefDepmins").ToString()).Selected = true;
            }
            catch { }
        }


    }

    protected void imgbtndeleteFlight_Click(object s, EventArgs e)
    {
        GridViewRow grdlt = (GridViewRow)(s as ImageButton).Parent.Parent;


        DataTable dtFlight = CreateDtFlight();

        DataRow dr = null;
        if (GrdFlight.Rows.Count > 1)
        {
            foreach (GridViewRow gr in GrdFlight.Rows)
            {
                if (grdlt.RowIndex != gr.RowIndex)
                {
                    UserControl_ctlAirPortList txtFrom1 = (gr.FindControl("txtFrom1") as UserControl_ctlAirPortList);
                    UserControl_ctlAirPortList txtTo1 = (gr.FindControl("txtTo1") as UserControl_ctlAirPortList);
                    dr = dtFlight.NewRow();
                    dr["travelOrigin"] = txtFrom1.Text;
                    dr["travelDestination"] = txtTo1.Text;
                    dr["departureDate"] = (gr.FindControl("txtDepDate1") as TextBox).Text;
                    dr["PrefDephours"] = (gr.FindControl("cmbDepHours1") as DropDownList).SelectedValue;
                    dr["PrefDepmins"] = (gr.FindControl("cmbDepMins1") as DropDownList).SelectedValue;
                    dtFlight.Rows.Add(dr);

                }
                GrdFlight.DataSource = dtFlight;
                GrdFlight.DataBind();
            }
        }
    }
}