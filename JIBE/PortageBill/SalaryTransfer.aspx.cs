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
using SMS.Business.Infrastructure;
using SMS.Business.PortageBill;
using System.IO;
using AjaxControlToolkit4;
using SMS.Business.Crew;


public partial class Account_Portage_Bill_SalaryTransfer : System.Web.UI.Page
{

    BLL_Infra_VesselLib objVessel = new BLL_Infra_VesselLib();

    decimal TotalAmount = 0;
    int? CrewID = null;



    int? AllotmentID = null;


    protected void Page_Load(object sender, EventArgs e)
    {

        UserAccessValidation();

        btnReworkAllotmentToVessel.Visible = false;

        if (Session["USERID"] == null)
            Response.Redirect("~/account/login.aspx");

        if (AjaxFileUpload1.IsInFileUploadPostBack)
        {

        }
        else
        {
            if (!IsPostBack)
            {
                Session["dtFlagFiles"] = createDtFlagAttach();
                ViewState["PBMonth"] = "0";
                ViewState["PBYear"] = "0";
                ViewState["PBMonthPrev"] = "0";
                ViewState["PBYearPrev"] = "0";

                lblDlaguser.Text = Session["USERFULLNAME"].ToString();
                

                Load_FleetList();
                Load_VesselList();
                Load_Years();
                Load_BankNames();
                BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
                ddlCountry.DataSource = objCrew.Get_CrewNationality(GetSessionUserID());
                ddlCountry.DataTextField = "COUNTRY";
                ddlCountry.DataValueField = "ID";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
                ddlCountry.SelectedIndex = 0;

                if (Request.QueryString["arg"] != null)
                {
                    string arg = Request.QueryString["arg"].ToString();
                    string[] arr;

                    arr = arg.Split('~');

                    string Vessel = arr[0].ToString();
                    string Month = DateTime.Parse(arr[1]).Month.ToString();
                    string Year = DateTime.Parse(arr[1]).Year.ToString();
                    try
                    {
                        ddlVessel.Items.FindByValue(Vessel).Selected = true;
                        ddlYear.Items.FindByValue(Year).Selected = true;
                        ddlMonth.Items.FindByValue(Month).Selected = true;
                    }
                    catch { }
                }
                else
                {
                    string Month = DateTime.Today.Month.ToString();
                    string Year = DateTime.Today.Year.ToString();
                    try
                    {
                        ddlYear.Items.FindByValue(Year).Selected = true;
                        ddlMonth.Items.FindByValue(Month).Selected = true;
                    }
                    catch { }
                }
                Load_Allotments();
            }
        }   
    }


    protected DataTable createDtFlagAttach()
    {
        DataTable dtattach = new DataTable();
        dtattach.Columns.Add("PKID");
        dtattach.Columns.Add("Flag_Attach");
        dtattach.Columns.Add("Flag_Attach_Name");
        dtattach.PrimaryKey = new DataColumn[] { dtattach.Columns["PKID"] };
        return dtattach;
    }

    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {


        }
        if (objUA.Edit == 0)
        {
            gvAllotments.Columns[gvAllotments.Columns.Count - 3].Visible = false;
            btnSaveFlagRemark.Visible = false;
            btnMarkCompleted.Visible = false;
            btnReOpenFlag.Visible = false;

        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {
            gvAllotments.Columns[gvAllotments.Columns.Count - 2].Visible = false;

        }
        

    }


    public void Load_FleetList()
    {
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        ddlFleet.DataSource = objVessel.GetFleetList(UserCompanyID);
        ddlFleet.DataTextField = "NAME";
        ddlFleet.DataValueField = "CODE";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }
    public void Load_VesselList()
    {
        int Fleet_ID = int.Parse(ddlFleet.SelectedValue);
        int UserCompanyID = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());
        int Vessel_Manager = UDFLib.ConvertToInteger(Session["USERCOMPANYID"].ToString());

        ddlVessel.DataSource = objVessel.Get_VesselList(Fleet_ID, 0, Vessel_Manager, "", UserCompanyID);

        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }

    protected void Load_BankNames()
    {

    }
    protected void Load_Years()
    {
        int Y = DateTime.Today.Year;

        for (int i = 0; i < 10; i++)
        {
            ddlYear.Items.Add(new ListItem((Y - i).ToString(), (Y - i).ToString()));
        }
        ddlYear.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void Load_Allotments()
    {
        int? AmountValue = chkAmountIsGreaterthanZero.Checked == true ? UDFLib.ConvertIntegerToNull(1) : null;
        gvAllotments.PageSize = int.Parse(ddlPageSize.SelectedValue);

        DataTable dt = BLL_PB_PortageBill.Get_Allotments(int.Parse(ddlFleet.SelectedValue)
                                                         , int.Parse(ddlVessel.SelectedValue)
                                                         , ddlMonth.SelectedValue
                                                         , ddlYear.SelectedValue
                                                         , int.Parse(ddlApproval.SelectedValue)
                                                         , int.Parse(DDLBank.SelectedValue)
                                                         , int.Parse(ddlStatus.SelectedValue)
                                                         , txtSearch.Text
                                                         , CrewID
                                                         , UDFLib.ConvertIntegerToNull(rbtnVerificationStatus.SelectedValue)
                                                         , null
                                                         , AmountValue
                                                         , chkFlagedItems.Checked
                                                         , Convert.ToInt32(ddlCountry.SelectedValue));
        gvAllotments.DataSource = dt;
        gvAllotments.DataBind();

        DataTable dtIDs = new DataTable();
        dtIDs.Columns.Add("ID");
        dtIDs.Columns.Add("AllotmentID");
        dtIDs.Columns.Add("PBDate", typeof(DateTime));
        DataRow drID;
        foreach (DataRow dr in dt.Rows)
        {
            drID = dtIDs.NewRow();
            drID["ID"] = dr["id"].ToString();
            drID["AllotmentID"] = dr["AllotmentID"].ToString();
            drID["PBDate"] = dr["PBill_Date"];
            dtIDs.Rows.Add(drID);
        }

        ctlRecordNavigationAllotment.InitRecords(dtIDs);


        lblRecordCount.Text = dt.Rows.Count.ToString();
        if (gvAllotments.PageCount > 0)
            lblPageStatus.Text = (gvAllotments.PageIndex + 1).ToString() + " of " + gvAllotments.PageCount.ToString();
        else
            lblPageStatus.Text = "0 of 0";
        UpdatePanel2.Update();



        DateTime? pbilldate = BLL_PB_PortageBill.Get_Valid_Date_TO_Rework_Allotment(Convert.ToInt32(ddlVessel.SelectedValue));


        if (pbilldate != null && objUA.Admin == 1)
        {
            btnReworkAllotmentToVessel.Text = "Rework allotment [" + Convert.ToDateTime(pbilldate).ToString("MMM-yyyy")+"] to " + ddlVessel.SelectedItem.Text ;
            btnReworkAllotmentToVessel.CommandArgument = pbilldate.ToString();

            btnReworkAllotmentToVessel.Visible = true;
        }

    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_VesselList();
        Load_Allotments();
    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        int VesselID = int.Parse(ddlVessel.SelectedValue);
        Load_Allotments();

           }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Allotments();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Allotments();
    }
    protected void ddlApproval_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Allotments();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Allotments();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Load_Allotments();
    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow gr in gvAllotments.Rows)
        {
            int Vessel_ID = 0;

            HiddenField hdnAllotmentID = (HiddenField)gr.FindControl("hdnAllotmentID");
            HiddenField hdnVessel_ID = (HiddenField)gr.FindControl("hdnVessel_ID");
            if (hdnVessel_ID != null)
                Vessel_ID = UDFLib.ConvertToInteger(hdnVessel_ID.Value);

            CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
            if (chkSelect != null && chkSelect.Checked == true)
            {
                int AllotmentID = int.Parse(hdnAllotmentID.Value);
                BLL_PB_PortageBill.Verify_Allotment(Vessel_ID, AllotmentID, GetSessionUserID());
            }


        }

        int CurrentPage = gvAllotments.PageIndex;
        Load_Allotments();
        gvAllotments.PageIndex = CurrentPage;
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
        {
            Session.Abandon();
            Response.Redirect("~/Account/Login.aspx");
            return 0;
        }
    }
    public string GetText(string strAcc_NO)
    {
        string strReturn;
        if (strAcc_NO == "" || strAcc_NO == "0")
        {
            strReturn = "New Account";
        }
        else
        {
            strReturn = strAcc_NO;
        }
        return strReturn;
    }

    protected void gvAllotments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string approval_status = DataBinder.Eval(e.Row.DataItem, "approval_status").ToString();
            string AllotmentID = DataBinder.Eval(e.Row.DataItem, "AllotmentID").ToString();
            decimal Amount = UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount").ToString());


            if (((CheckBox)e.Row.FindControl("chkSelect")).Checked || objUA.Edit == 0)
            {
                e.Row.Cells[e.Row.Cells.Count - 3].Enabled = false;
            }
            if (((CheckBox)e.Row.FindControl("chkSelect")).Checked || objUA.Delete == 0 || approval_status == "1")
            {
                e.Row.Cells[e.Row.Cells.Count - 2].Text = "";
            }

            TotalAmount = TotalAmount + Amount;

            if (AllotmentID == "0")
                ((CheckBox)e.Row.FindControl("chkSelect")).Visible = false;

            // if (approval_status == "1" )
            //   ((CheckBox)e.Row.FindControl("chkSelect")).Enabled = false;

            if (!e.Row.RowState.ToString().Contains("Edit"))
            {
                DateTime PbillDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "PBill_Date"));

                ((Label)e.Row.FindControl("lblkAccountInfo")).Attributes.Add("onmousemove", "js_ShowToolTip('" + UDFLib.ReplaceSpecialCharacter(DataBinder.Eval(e.Row.DataItem, "Bank_Details").ToString()) + "',event,this)");
                ((Label)e.Row.FindControl("lblLeabeWage")).Attributes.Add("onmousemove", "js_ShowToolTip('" + UDFLib.ReplaceSpecialCharacter(DataBinder.Eval(e.Row.DataItem, "Bank_Details").ToString()) + "',event,this)");

                if (UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Is_Special_Alt")) == 2)
                {
                    (e.Row.FindControl("imgSideLetter") as Image).ImageUrl = @"~/images/SideLetterStar.png";
                    (e.Row.FindControl("imgSideLetter") as Image).ToolTip = "Side Letter Amount";
                    ((Label)e.Row.FindControl("lblSalary")).Text = "Side Letter";
                    ((Label)e.Row.FindControl("lblSalary")).Attributes.Add("onmouseover", "ASync_SideLetter('" + UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "Voyage_ID")) + "',event,this)");
                }
                else
                {
                    (e.Row.FindControl("imgSideLetter") as Image).Visible = false;
                    ((Label)e.Row.FindControl("lblSalary")).Attributes.Add("onmouseover", "ASync_Get_Wages('" + gvAllotments.DataKeys[e.Row.RowIndex].Values["id"].ToString() + "','" + PbillDate.Month.ToString() + "','" + PbillDate.Year.ToString() + "',event,this)");
                }

                if (Amount == 0)
                {
                    ((Label)e.Row.FindControl("lblAmount")).BackColor = System.Drawing.Color.Yellow;
                    ((CheckBox)e.Row.FindControl("chkSelect")).Enabled = false;

                }
            }

            // change the image url of flagbutton based on flag status
            if (string.Compare(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Flag_Status")), "OPEN", true) == 0) // it has been flagged
            {
                ((ImageButton)e.Row.FindControl("imgbtnAddFlagRemark")).ToolTip = "Click to add follow up";
                ((ImageButton)e.Row.FindControl("imgbtnAddFlagRemark")).ImageUrl = "~/Images/Allot-Flag-Active.png";
                ((ImageButton)e.Row.FindControl("imgbtnAddFlagRemark")).Attributes.Add("onmouseover", "ShowFlagRemarkasTooltip(" + DataBinder.Eval(e.Row.DataItem, "AllotmentID").ToString() + "," + DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString() + ",'" + DataBinder.Eval(e.Row.DataItem, "PBill_Date").ToString() + "',event,this);return false;");
            }
            else // it is new (ie status is null)
            {
                ((ImageButton)e.Row.FindControl("imgbtnAddFlagRemark")).ToolTip = "Click to Add Follow up";
                ((ImageButton)e.Row.FindControl("imgbtnAddFlagRemark")).ImageUrl = "~/Images/Allot-Flag-Inactive.png";
            }

            //call the js function based on status
            if (string.Compare(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Flag_Status")), "CLOSE", true) != 0) // not closed 
                ((ImageButton)e.Row.FindControl("imgbtnAddFlagRemark")).Attributes.Add("onclick", "ShowFlagRemark(" + DataBinder.Eval(e.Row.DataItem, "AllotmentID").ToString() + "," + DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString() + ",'" + DataBinder.Eval(e.Row.DataItem, "PBill_Date").ToString() + "'," + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + ");return false;");
            else
            {
                ((ImageButton)e.Row.FindControl("imgbtnAddFlagRemark")).Attributes.Add("onclick", "ShowFlagReOpen(" + DataBinder.Eval(e.Row.DataItem, "AllotmentID").ToString() + "," + DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString() + ",'" + DataBinder.Eval(e.Row.DataItem, "PBill_Date").ToString() + "');return false;");

                ((ImageButton)e.Row.FindControl("imgbtnAddFlagRemark")).ToolTip = " Click to re-open";
                ((ImageButton)e.Row.FindControl("imgbtnAddFlagRemark")).ImageUrl = "~/Images/Allot-Flag-Completed.png";
                ((ImageButton)e.Row.FindControl("imgbtnAddFlagRemark")).Attributes.Add("onmouseover", "ShowFlagRemarkasTooltip(" + DataBinder.Eval(e.Row.DataItem, "AllotmentID").ToString() + "," + DataBinder.Eval(e.Row.DataItem, "Vessel_ID").ToString() + ",'" + DataBinder.Eval(e.Row.DataItem, "PBill_Date").ToString() + "',event,this);return false;");
            }




        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblAmountTotal = (Label)e.Row.FindControl("lblAmountTotal");
            if (lblAmountTotal != null)
            {
                lblAmountTotal.Text = TotalAmount.ToString("0.00");
                TotalAmount = 0;
            }
        }

    }

    protected void gvAllotments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAllotments.PageIndex = e.NewPageIndex;
        Load_Allotments();

    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Allotments();
    }

    protected void gvAllotments_RowEditing(object sender, GridViewEditEventArgs e)
    {

        gvAllotments.EditIndex = e.NewEditIndex;
        Load_Allotments();

    }
    protected void gvAllotments_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        BLL_PB_PortageBill.Update_Allotment(Convert.ToInt32(e.Keys["Vessel_ID"]),
                                            Convert.ToInt32(e.Keys["AllotmentID"]),
                                            UDFLib.ConvertToDecimal(((TextBox)gvAllotments.Rows[e.RowIndex].FindControl("txtAllotmentAmount")).Text),
                                            Convert.ToInt32(((DropDownList)gvAllotments.Rows[e.RowIndex].FindControl("ddlAccount")).SelectedValue),
                                            Convert.ToInt32(Session["userid"]));

        gvAllotments.EditIndex = -1;
        Load_Allotments();
    }
    protected void gvAllotments_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvAllotments.EditIndex = -1;
        Load_Allotments();
    }


    protected void btnViewToVerify_Click(object s, EventArgs e)
    {
        try
        {
            string[] strArg = ((Button)s).CommandArgument.Split(',');

            CrewID = Int32.Parse(strArg[0]);
            ViewState["PBMonth"] = Convert.ToDateTime(strArg[1]).Month;
            ViewState["PBYear"] = Convert.ToDateTime(strArg[1]).Year;
            ViewState["PBMonthPrev"] = Convert.ToDateTime(strArg[1]).AddMonths(-1).Month;
            ViewState["PBYearPrev"] = Convert.ToDateTime(strArg[1]).AddMonths(-1).Year;
            AllotmentID = Int32.Parse(strArg[2]);

            BindAllotmentsToVerify(null);

            String msg1 = String.Format("showModal('dvVerifyAllotment',true,RefreshOnClose);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowverify", msg1, true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnVerifyAllot_Click(object s, EventArgs e)
    {
        try
        {
            if (BLL_PB_PortageBill.Get_Bank_Account_Status(int.Parse(((Button)s).CommandArgument.Split(',')[1]), int.Parse(((Button)s).CommandArgument.Split(',')[0])) != 0)
            {
                BLL_PB_PortageBill.Verify_Allotment(int.Parse(((Button)s).CommandArgument.Split(',')[0]), int.Parse(((Button)s).CommandArgument.Split(',')[1]), GetSessionUserID());
            }
            else
            {
                String msg1 = String.Format("alert('This seems to be manning office account.Please Select Crew Bank Account for Allotment.');");//You cannot verify this. To verify this, ask the staff to change the bank account or contact to IT Administrator.
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshobannk", msg1, true);
            }
            CrewID = int.Parse(((Button)s).CommandArgument.Split(',')[2]);
            AllotmentID = int.Parse(((Button)s).CommandArgument.Split(',')[1]);
            BindAllotmentsToVerify(null);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnDelete_Click(object s, EventArgs e)
    {
        try
        {
            string[] strArg = ((ImageButton)s).CommandArgument.Split(',');

            BLL_PB_PortageBill.Delete_Allotment(Int32.Parse(strArg[2]), Convert.ToInt32(strArg[0]), Convert.ToDateTime(strArg[1]), GetSessionUserID());
            Load_Allotments();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindAllotmentsToVerify(DataRow dr)
    {
        try
        {
            JSConfirmStatus.Value = "false";

            if (dr != null)
            {
                CrewID = int.Parse(dr["id"].ToString());
                ViewState["PBMonth"] = Convert.ToDateTime(dr["PBDate"]).Month;
                ViewState["PBYear"] = Convert.ToDateTime(dr["PBDate"]).Year;
                ViewState["PBYearPrev"] = Convert.ToDateTime(dr["PBDate"]).AddMonths(-1).Year;
                ViewState["PBMonthPrev"] = Convert.ToDateTime(dr["PBDate"]).AddMonths(-1).Month;
                AllotmentID = int.Parse(dr["AllotmentID"].ToString());

            }



            lblPortageBilldate.Text = "Portage Bill : " + ViewState["PBMonth"].ToString() + " / " + ViewState["PBYear"].ToString() + " [ Current ]";
            lblPrevMonthPBDate.Text = "Portage Bill : " + ViewState["PBMonthPrev"].ToString() + " / " + ViewState["PBYearPrev"].ToString() + " [ Previous ]";
            frmvCrewDetails.DataSource = BLL_PB_PortageBill.Get_Allotments(int.Parse(ddlFleet.SelectedValue), int.Parse(ddlVessel.SelectedValue), ViewState["PBMonth"].ToString(), ViewState["PBYear"].ToString(), int.Parse(ddlApproval.SelectedValue), int.Parse(DDLBank.SelectedValue), int.Parse(ddlStatus.SelectedValue), txtSearch.Text, CrewID, null, AllotmentID);
            frmvCrewDetails.DataBind();


            DataSet dtAllAllotment = BLL_PB_PortageBill.Get_Allotments_ByCrewID(int.Parse(CrewID.ToString()), Convert.ToInt32(ViewState["PBMonth"]), Convert.ToInt32(ViewState["PBYear"]));
            if (dtAllAllotment.Tables[0].Rows.Count > 1)
            {
                (frmvCrewDetails.FindControl("btnVerifyAllot") as Button).Attributes.Add("onclick", "AlertForMorethanOneAllotments('" + dtAllAllotment.Tables[1].Rows[0][0].ToString() + "','" + (frmvCrewDetails.FindControl("btnVerifyAllot") as Button).ClientID + "');return false;");
            }



            DataSet ds = BLL_PB_PortageBill.ACC_Get_CrewWages_ByMonth(int.Parse(CrewID.ToString()), Convert.ToInt32(ViewState["PBMonth"]), Convert.ToInt32(ViewState["PBYear"]));


         
                gvJoiningWages.DataSource = ds.Tables[1];
                gvJoiningWages.DataBind();

                if (ds.Tables[1].Rows.Count>1)
                gvJoiningWages.Rows[gvJoiningWages.Rows.Count - 1].Font.Bold = true;

                gvBortagebill.DataSource = ds.Tables[0];
                gvBortagebill.DataBind();

                if (ds.Tables[0].Rows.Count > 1)
                gvBortagebill.Rows[gvBortagebill.Rows.Count - 1].Font.Bold = true;

                gvPrevMonthPB.DataSource = ds.Tables[2];
                gvPrevMonthPB.DataBind();

                if (ds.Tables[2].Rows.Count > 1)
                gvPrevMonthPB.Rows[gvPrevMonthPB.Rows.Count - 1].Font.Bold = true;
           

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnAddNewAllotment_Click(object s, EventArgs e)
    {
        ResponseHelper.Redirect("NewAllotment.aspx", "_blank", "");
    }

    protected void btnSaveFlagRemark_Click(object s, EventArgs e)
    {
        try
        {

            BLL_PB_PortageBill.ACC_INS_Allotment_Flag(Convert.ToInt32(hdfAllotmentIDFlag.Value), Convert.ToInt32(hdfVesselIDFlag.Value), Convert.ToDateTime(hdfPBillDateFlag.Value),
                txtFlagRemark.Text, Convert.ToInt32(Session["userid"]), (DataTable)Session["dtFlagFiles"], "OPEN");
            SendMailOnFlagSave(Convert.ToInt32(hdfAllotmentIDFlag.Value), Convert.ToInt32(hdfVesselIDFlag.Value), Convert.ToDateTime(hdfPBillDateFlag.Value).ToString(), Convert.ToInt32(hdfCrewIDFlag.Value));
            Load_Allotments();
            String msg1 = String.Format("hideModal('dvFlagRemark');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowverifyrefresgclosehf", msg1, true);
        }
        catch (System.Exception ex)
        {

        }
        finally
        {
            Session["dtFlagFiles"] = createDtFlagAttach();
        }


    }
    protected void tbnMarkCompleted_Click(object s, EventArgs e)
    {
        try
        {
            BLL_PB_PortageBill.ACC_INS_Allotment_Flag(Convert.ToInt32(hdfAllotmentIDFlag.Value), Convert.ToInt32(hdfVesselIDFlag.Value), Convert.ToDateTime(hdfPBillDateFlag.Value),
               txtFlagRemark.Text, Convert.ToInt32(Session["userid"]), (DataTable)Session["dtFlagFiles"], "CLOSE");
            SendMailOnFlagSave(Convert.ToInt32(hdfAllotmentIDFlag.Value), Convert.ToInt32(hdfVesselIDFlag.Value), Convert.ToDateTime(hdfPBillDateFlag.Value).ToString(), Convert.ToInt32(hdfCrewIDFlag.Value));
            Load_Allotments();
            String msg1 = String.Format("hideModal('dvFlagRemark');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowverifyrefresg", msg1, true);
        }
        catch (Exception ex)
        {
            Session["dtFlagFiles"] = createDtFlagAttach();
        }
        finally
        {

        }
    }

    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        // User can save file to File System, database or in session state

        Byte[] fileBytes = file.GetContents();
        string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Allotments");
        Guid GUID = Guid.NewGuid();
        string FullFilename = Path.Combine(sPath, GUID.ToString() + Path.GetExtension(file.FileName));
        string Flag_Attach = GUID.ToString() + Path.GetExtension(file.FileName);


        FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
        fileStream.Write(fileBytes, 0, fileBytes.Length);
        fileStream.Close();

        DataTable dtattach = (DataTable)Session["dtFlagFiles"];
        if (dtattach.Rows.Count > 0)
        {
            DataRow drAttch = dtattach.NewRow();

            int pkid = Convert.ToInt32((dtattach.Rows[dtattach.Rows.Count - 1]["PKID"])) + 1;
            drAttch["Flag_Attach_Name"] = file.FileName;
            drAttch["Flag_Attach"] = "uploads/Allotments/" + Flag_Attach;
            drAttch["PKID"] = pkid;
            dtattach.Rows.Add(drAttch);

            Session["dtFlagFiles"] = dtattach;
        }
        else
        {
            DataRow drAttch = dtattach.NewRow();
            drAttch["Flag_Attach_Name"] = file.FileName;
            drAttch["Flag_Attach"] = "uploads/Allotments/" + Flag_Attach;
            drAttch["PKID"] = 1;
            dtattach.Rows.Add(drAttch);

            Session["dtFlagFiles"] = dtattach;

        }


    }

    protected void btnRefreshSessionAttachment_Click(object s, EventArgs e)
    {
        Session["dtFlagFiles"] = createDtFlagAttach();
    }

    protected void SendMailOnFlagSave(int Allotment_ID, int Vessel_ID, string PBill_Date, int CrewID)
    {
        Table tblFlag = new Table();
        TableRow R1flg = new TableRow();
        TableCell CL1flg = new TableCell();

        TableRow R2flg = new TableRow();
        TableCell CL2flg = new TableCell();
        R1flg.Cells.Add(CL1flg);
        R2flg.Cells.Add(CL2flg);

        tblFlag.Rows.Add(R1flg);
        tblFlag.Rows.Add(R2flg);

        DateTime dtDate = Convert.ToDateTime(PBill_Date);


        frmvCrewDetails.DataSource = BLL_PB_PortageBill.Get_Allotments(int.Parse(ddlFleet.SelectedValue), int.Parse(ddlVessel.SelectedValue), ViewState["PBMonth"].ToString(), ViewState["PBYear"].ToString(), int.Parse(ddlApproval.SelectedValue), int.Parse(DDLBank.SelectedValue), int.Parse(ddlStatus.SelectedValue), txtSearch.Text, Convert.ToInt32(hdfCrewIDFlag.Value), null, AllotmentID);
        frmvCrewDetails.DataBind();
        (frmvCrewDetails.FindControl("btnVerifyAllot") as Button).Visible = false;



        DataSet dsRmk = BLL_PB_PortageBill.ACC_GET_Allotment_Flag(Allotment_ID, Vessel_ID, dtDate);
        GridView gvFlag = new GridView();
        gvFlag.HeaderStyle.CssClass = "CreateHtmlTableFromDataTable-DataHedaer";
        gvFlag.RowStyle.CssClass = "CreateHtmlTableFromDataTable-Data";
        gvFlag.AutoGenerateColumns = false;
        gvFlag.DataKeyNames = new string[] { "Flag_ID" };
        gvFlag.BorderWidth = 1;
        gvFlag.Width = 498;

        BoundField User = new BoundField();
        User.DataField = "CreatedBy";
        User.HeaderText = "User";
        BoundField Date = new BoundField();
        Date.DataField = "CreatedOn";
        Date.HeaderText = "Date";
        BoundField Message = new BoundField();
        Message.DataField = "Remark";
        Message.HeaderText = "Message";

        BoundField Attachcol = new BoundField();
        Attachcol.DataField = "CreatedBy";
        Attachcol.HeaderText = "Attachments";

        gvFlag.Columns.Add(User);
        gvFlag.Columns.Add(Date);
        gvFlag.Columns.Add(Message);
        gvFlag.Columns.Add(Attachcol);


        gvFlag.DataSource = dsRmk.Tables[0];
        gvFlag.DataBind();

        foreach (GridViewRow gr in gvFlag.Rows)
        {
            GridView gvAttach = new GridView();
            gvAttach.AutoGenerateColumns = false;
            gvAttach.BorderWidth = 0;
            gvAttach.ShowHeader = false;
            gvAttach.RowStyle.CssClass = "CreateHtmlTableFromDataTable-Data-Attachment";

            HyperLinkField LinkAttch = new HyperLinkField();
            LinkAttch.DataNavigateUrlFields = new string[] { "Attachment_Path" };
            LinkAttch.DataTextField = "Attachment_Name";
            LinkAttch.Target = "blank";

            gvAttach.Columns.Add(LinkAttch);
            gvAttach.DataSource = dsRmk.Tables[1].AsEnumerable().Where(a => a["Flag_ID"].ToString().Equals(gvFlag.DataKeys[gr.RowIndex].Values["Flag_ID"].ToString())).AsDataView().ToTable();
            gvAttach.DataBind();

            gr.Cells[3].Text = "";
            gr.Cells[3].Controls.Add(gvAttach);
        }

        CL1flg.Controls.Add(frmvCrewDetails);
        CL2flg.Controls.Add(gvFlag);

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        tblFlag.RenderControl(htmlWrite);

        string mailContent = htmlWrite.InnerWriter.ToString();


        string mailBody = "<html><head> <style type='text/css'> body { font-size: 11px;font-family: Tahoma ,Tahoma, Sans-Serif;  }  .CreateHtmlTableFromDataTable-PageHeader  { background-color: #F6B680; }  .CreateHtmlTableFromDataTable-DataHedaer { background-color: #F2F2F2; border: 1px solid gray; text-align: center;  } .CreateHtmlTableFromDataTable-Data{ border: 1px solid gray; } </style>  </head>  <body > <table style='width:500px;background-color: #FFFFFF;border: 2px solid #FFB733;' border='1' ><tr><td>" + mailContent + "</td> </tr> </table> </body></html>";

        string sToEmailAddress = BLL_PB_PortageBill.ACC_Get_Allotment_Flag_Mail(Convert.ToInt32(Session["userid"]));

        BLL_Crew_CrewDetails objMail = new BLL_Crew_CrewDetails();
        objMail.Send_CrewNotification(0, 0, 0, 0, sToEmailAddress, "", "", "Allotment Flag", mailBody, "", "MAIL", "", UDFLib.ConvertToInteger(Session["USERID"].ToString()), "READY");


    }

    protected void btnReOpenFlag_Click(object s, EventArgs e)
    {
        try
        {

            BLL_PB_PortageBill.ACC_INS_Allotment_Flag(Convert.ToInt32(hdfFlagReOpenAllotmentID.Value), Convert.ToInt32(hdfFlagReOpenVesselID.Value), Convert.ToDateTime(hdfFlagReOpenPBillDate.Value),
                "Re-Open :" + Environment.NewLine + txtReasonForReopenFlag.Text, Convert.ToInt32(Session["userid"]), (DataTable)Session["dtFlagFiles"], "OPEN");
            Load_Allotments();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowReopen", "   hideModal('dvReOpenFlag');", true);
        }
        catch (System.Exception ex)
        {

        }
        finally
        {
            Session["dtFlagFiles"] = createDtFlagAttach();
        }



    }


    protected void btnReworkAllotmentToVessel_Click(object sender, EventArgs e)
    {
        try
        {
            int sts = BLL_PB_PortageBill.UPD_Rework_Allotment(Convert.ToInt32(ddlVessel.SelectedValue), Convert.ToInt32(Session["UserID"]), Convert.ToDateTime((sender as Button).CommandArgument));
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }

    }

    protected void btnUnverify_Click(object s, EventArgs e)
    {
        try
        { 
            BLL_PB_PortageBill.Unverify_Allotment(int.Parse(((Button)s).CommandArgument.Split(',')[0]), int.Parse(((Button)s).CommandArgument.Split(',')[1]), GetSessionUserID());
            Load_Allotments();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
