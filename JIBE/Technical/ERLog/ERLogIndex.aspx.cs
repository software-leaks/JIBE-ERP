using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.Text;
using SMS.Business.Technical;
using System.Web.UI.HtmlControls;

public partial class Technical_ERLog_ERLogIndex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;
            ucCustomPagerItems.PageSize = 20;

            Session["sVesselCode"] = DDLVessel.SelectedValue;
            FillDDL();
            BindIndex();
        }
    }
    protected void gvERLogIndex_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton imgBtn;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
            LinkButton lbtnRequestNumber = (LinkButton)e.Row.FindControl("lbtVoyageNumberr");
            Label lblEngineLogDate = (Label)e.Row.FindControl("lblEngineLogDate");

            string loginid = lbtnRequestNumber.CommandArgument.ToString().Split(',')[0];
            string vesselid = lbtnRequestNumber.CommandArgument.ToString().Split(',')[1];
            // e.Row.Attributes.Add("onClick", "javascript:window.open('../ERLog/ERLogDetails.aspx?LOGID=" + loginid + "&VESSELID=" + vesselid + "'); return false;");
            lbtnRequestNumber.Attributes.Add("onclick", "javascript:window.open('../ERLog/ERLogDetails.aspx?LOGID=" + loginid + "&VESSELID=" + vesselid + "'); return false;");
            lblEngineLogDate.Attributes.Add("onclick", "javascript:window.open('../ERLog/ERLogDetails.aspx?LOGID=" + loginid + "&VESSELID=" + vesselid + "'); return false;");
            HiddenField hdfAnomalyValue = (HiddenField)e.Row.FindControl("hdfAnomalyValue");
            if (hdfAnomalyValue.Value.ToString() == "1")
            {
                e.Row.Cells[2].CssClass = "AnomalyCell";
            }
            imgBtn = (ImageButton)(e.Row.FindControl("ImgBtn"));
            imgBtn.CommandArgument = imgBtn.CommandArgument + ";" + e.Row.RowIndex.ToString();





            Image imgRemarks = (Image)e.Row.FindControl("imgRemarks");
            if (imgRemarks != null)
            {
                imgRemarks.Attributes.Add("onmouseover", "showFollowups(" + vesselid + "," + loginid + ",this)");
                imgRemarks.Attributes.Add("onmouseout", "closeDiv('dialog')");
            }

            //if (Session["LOG_ID"]!=null)
            //if (loginid.ToString() == Session["LOG_ID"].ToString())
            //{
            //    PlaceHolder objPH;
            //    objPH = (PlaceHolder)(e.Row.FindControl("objPHLHanomalies"));
            //    if (objPH != null)
            //        objPH.Visible = true;

            //    if (imgBtn.ImageUrl == "~/Images/plus.gif")
            //        imgBtn.ImageUrl = @"~/Images/minus.gif";
            //    else
            //        imgBtn.ImageUrl = @"~/Images/plus.gif";
            //}
        }



        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblCERemarks = (Label)e.Row.FindControl("lblCERemarks");
            Label lblCEFullRemarks = (Label)e.Row.FindControl("lblCEFullRemarks");

            if (lblCEFullRemarks.Text.Length > 20)
                lblCERemarks.Text = lblCERemarks.Text + "..";

            lblCERemarks.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[C/E Remarks] body=[" + lblCEFullRemarks.Text + "]");


        }

    }

    protected void grdLHanomalies_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            LinkButton lnkbGoToDetails = (LinkButton)e.Row.FindControl("lnkbGoToDetails");
            Label lblEngineLogDate = (Label)e.Row.FindControl("lblEngineLogDate");

            string LOG_WATCH = DataBinder.Eval(e.Row.DataItem, "LOG_WATCH").ToString();

            string Anomaly_Value = DataBinder.Eval(e.Row.DataItem, "Anomaly_Value").ToString();


            if (Anomaly_Value == "1")
            {
                e.Row.Cells[1].CssClass = "AnomalyCell";
            }
            else
            {
                e.Row.Cells[1].CssClass = "NoAnomaly";
            }


            string Log_date = Session["CurExDate"].ToString();
            string Vessel_Id = Session["CurExVessel_ID"].ToString();

            lnkbGoToDetails.Attributes.Add("onclick", "javascript:window.open('../ERLog/ERLogDetailsWatchHours.aspx?Log_date=" + Log_date + "&Vessel_Id=" + Vessel_Id + "&LOG_WATCH=" + LOG_WATCH + "'); return false;");



        }
    }



    protected void gvERLogIndex_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Expand")
        {
            ImageButton imgbtn;
            GridView gv = (GridView)(sender);
            Int32 rowIndex = Convert.ToInt32(e.CommandArgument.ToString().Split(';')[2]);
            UpdatePanel objPH = (UpdatePanel)(gv.Rows[rowIndex].FindControl("objPHLHanomalies"));
            PlaceHolder PlaceHolder1 = (PlaceHolder)(gv.Rows[rowIndex].FindControl("PlaceHolder1"));


            GridView objChildGrid = (GridView)(gv.Rows[rowIndex].FindControl("grdLHanomalies"));

            int Vessel_ID = Convert.ToInt32(e.CommandArgument.ToString().Split(';')[0]);
            DateTime LOG_DATE = Convert.ToDateTime(e.CommandArgument.ToString().Split(';')[1]);

            DataSet ds = BLL_Tec_ErLog.Get_Erlog_WatchHours_Anomaly(Vessel_ID, LOG_DATE);

            imgbtn = (ImageButton)(gv.Rows[rowIndex].FindControl("ImgBtn"));



            foreach (GridViewRow gvr in gvERLogIndex.Rows)
            {
                if (gvr.DataItemIndex != rowIndex)
                {

                    UpdatePanel objPH1 = (UpdatePanel)(gvr.FindControl("objPHLHanomalies"));
                    PlaceHolder PlaceHolder12 = (PlaceHolder)(gvr.FindControl("PlaceHolder1"));
                    if (objPH1 != null)
                    {
                        objPH1.Visible = false;
                        ImageButton imgbtn1 = (ImageButton)(gvr.FindControl("ImgBtn"));


                        imgbtn1.ImageUrl = @"~/Images/plus.gif";


                    }

                    if (PlaceHolder12 != null)
                    {
                        PlaceHolder12.Visible = false;
                    }


                }

            }



            if (imgbtn.ImageUrl == "~/Images/plus.gif")
            {
                Session["LOG_ID"] = gv.DataKeys[rowIndex][0].ToString();
                //  objDS.SelectParameters["LOG_ID"].DefaultValue = gv.DataKeys[rowIndex][0].ToString();
                Session["LOG_ID"] = gv.DataKeys[rowIndex][0].ToString();
                Session["CurExDate"] = LOG_DATE;
                Session["CurExVessel_ID"] = Vessel_ID;
                objChildGrid.DataSource = ds.Tables[0];
                objChildGrid.DataBind();

                imgbtn.ImageUrl = @"~/Images/minus.gif";
                if (objPH != null)
                    objPH.Visible = true;
                PlaceHolder1.Visible = true;
                objChildGrid.Visible = true;
            }
            else
            {

                imgbtn.ImageUrl = @"~/Images/plus.gif";
                if (objPH != null)
                    objPH.Visible = false;
                PlaceHolder1.Visible = false;
                objChildGrid.Visible = false;


                // BindIndex();
            }
            //  

        }
    }
    protected void gvERLogIndex_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["SORTBYCOLOUMN"] = e.SortExpression;
        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindIndex();
    }
    protected void OnViewRequest(object source, CommandEventArgs e)
    {
        //ResponseHelper.Redirect("ERLogDetails.aspx?LOGID=" + e.CommandArgument.ToString().Split(',')[0] + "&VESSELID=" + e.CommandArgument.ToString().Split(',')[1], "", "");

    }
    protected void UpdateStatus(object source, CommandEventArgs e)
    {
        //BLL_PB_PhoneCard.PhoneCord_Request_UpdateStatus(int.Parse(e.CommandArgument.ToString()), 1);
    }
    public void BindIndex()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = BLL_Tec_ErLog.ErLog_ME_00_Search(UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue), txtFromDate.Text, txtToDate.Text, sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvERLogIndex.DataSource = dt;
            gvERLogIndex.DataBind();
        }
        else
        {
            gvERLogIndex.DataSource = dt;
            gvERLogIndex.DataBind();
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
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLFleet.Items.Insert(0, li);

            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            DDLVessel.Items.Insert(0, new ListItem("--SELECT ALL--", null));
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindIndex();
    }

    protected void ImgExpExcel_Click(object sender, ImageClickEventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = BLL_Tec_ErLog.ErLog_ME_00_Search(UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue), txtFromDate.Text, txtToDate.Text, sortbycoloumn, sortdirection
            , 1, int.MaxValue, ref  rowcount);

        dt.Columns.Add("AML", typeof(string));
        foreach (DataRow item in dt.Rows)
        {
            if (item["Anomaly_Value"].ToString() == "1")
            {
                item["AML"] = "Yes";
            }
            else
            {
                item["AML"] = "No";
            }
        }

        string[] HeaderCaptions = { "Voyage Numbe", "Vessel Name", "From", "TO", "Date", "Remarks", "Anomaly Present" };
        string[] DataColumnsName = { "VOYAGE_NUM", "VESSEL_NAME", "FROMPORT", "TOPORT", "LOG_DATE", "CE_REMARKS", "AML" };

        GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, "EngineRoomLogBookIndex", "Engine Room Log Book Index ");
    }
    protected void DDLVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindIndex();
        // Session["sVesselCode"] = DDLVessel.SelectedValue;
    }



    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.Items.Clear();
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLVessel.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }


    }

}