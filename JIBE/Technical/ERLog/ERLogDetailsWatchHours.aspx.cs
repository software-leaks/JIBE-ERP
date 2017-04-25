using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.Technical;

public partial class Technical_ERLog_ERLogDetailsWatchHours : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

            Dictionary<int, string> lDicHours = new Dictionary<int, string>();
            int i = 0;
            foreach (ListItem item in ddlWatchHours.Items)
            {
                lDicHours.Add(i++, item.Text);
            }
            DateTime FDATE = DateTime.Now.AddDays(-1).Date;
            Session["lDicHours"] = lDicHours;
            try
            {



                int lTempHour = lDicHours.FirstOrDefault(x => x.Value == Request.QueryString["LOG_WATCH"].ToString().ToString()).Key;
                if (lTempHour == 5)
                {
                    Session["DetLOG_WATCH"] = lDicHours[0];
                    Session["DetLog_date"] = Convert.ToDateTime(Request.QueryString["Log_date"]);
                }
                else
                {
                    Session["DetLOG_WATCH"] = lDicHours[lTempHour + 1];
                    Session["DetLog_date"] = Convert.ToDateTime(Request.QueryString["Log_date"]).AddDays(-1);
                }

                //  Session["DetLog_date"] = Convert.ToDateTime(Request.QueryString["Log_date"]);
                Session["DetVessel_Id"] = Request.QueryString["Vessel_Id"].ToString();
                // Session["DetLOG_WATCH"] = Request.QueryString["LOG_WATCH"].ToString();






                if (DateTime.Now.Hour < 4)
                {
                    Session["CurLOG_WATCH"] = "0000-0400";
                    FDATE.AddMinutes(1);

                }
                if (4 <= DateTime.Now.Hour && DateTime.Now.Hour < 8)
                {
                    Session["CurLOG_WATCH"] = "0400-0800";
                    FDATE.AddHours(4);
                }
                if (8 <= DateTime.Now.Hour && DateTime.Now.Hour < 12)
                {
                    Session["CurLOG_WATCH"] = "0800-1200";
                    FDATE.AddHours(8);

                }
                if (12 <= DateTime.Now.Hour && DateTime.Now.Hour < 16)
                {
                    Session["CurLOG_WATCH"] = "1200-1600";
                    FDATE.AddHours(12);

                }
                if (16 <= DateTime.Now.Hour && DateTime.Now.Hour < 20)
                {
                    Session["CurLOG_WATCH"] = "1600-2000";
                    FDATE.AddHours(16);

                }
                if (20 <= DateTime.Now.Hour)
                {

                    Session["CurLOG_WATCH"] = "2000-2400";
                    FDATE.AddHours(20);

                }

            }
            catch (Exception)
            {
                Session["DetLog_date"] = DateTime.Now.Date.AddDays(-1);



                if (DateTime.Now.Hour < 4)
                {
                    Session["DetLog_date"] = DateTime.Now.Date;
                    Session["DetLOG_WATCH"] = "0000-0400";
                    Session["CurLOG_WATCH"] = "0000-0400";
                    FDATE.AddMinutes(1);


                }
                if (4 <= DateTime.Now.Hour && DateTime.Now.Hour < 8)
                {
                    Session["DetLOG_WATCH"] = "0400-0800";
                    Session["CurLOG_WATCH"] = "0400-0800";
                    FDATE.AddHours(4);

                }
                if (8 <= DateTime.Now.Hour && DateTime.Now.Hour < 12)
                {
                    Session["DetLOG_WATCH"] = "0800-1200";
                    Session["CurLOG_WATCH"] = "0800-1200";
                    FDATE.AddHours(8);

                }
                if (12 <= DateTime.Now.Hour && DateTime.Now.Hour < 16)
                {
                    Session["DetLOG_WATCH"] = "1200-1600";
                    Session["CurLOG_WATCH"] = "1200-1600";
                    FDATE.AddHours(12);

                }
                if (16 <= DateTime.Now.Hour && DateTime.Now.Hour < 20)
                {
                    Session["DetLOG_WATCH"] = "1600-2000";
                    Session["CurLOG_WATCH"] = "1600-2000";
                    FDATE.AddHours(16);

                }
                if (20 <= DateTime.Now.Hour)
                {

                    Session["DetLOG_WATCH"] = "2000-2400";
                    Session["CurLOG_WATCH"] = "2000-2400";
                    FDATE.AddHours(20);

                }
                Session["DetVessel_Id"] = 0;
            }



            Session["SDate"] = Session["DetLog_date"];
            Session["SHour"] = lDicHours.FirstOrDefault(x => x.Value == Session["DetLOG_WATCH"].ToString()).Key;





            FillDDL();
            DDLVessel.SelectedValue = Session["DetVessel_Id"].ToString();
            ddlWatchHours.SelectedValue = (Convert.ToInt32(Session["SHour"]) + 1).ToString();
            txtFromDate.Text = Convert.ToDateTime(Session["DetLog_date"]).ToString("dd/MM/yyyy");

            //---------------------------------------------




            //---------------------------------------------

            Session["FDATE"] = FDATE;
            Session["FHOUR"] = lDicHours.FirstOrDefault(x => x.Value == Session["CurLOG_WATCH"].ToString()).Key;
            if (Convert.ToInt32(Session["FHOUR"]) == 0)
            {
                Session["XHOUR"] = "5";
            }
            else
            {
                Session["XHOUR"] = Convert.ToInt32(Session["FHOUR"]) - 1;
                Session["XDATE"] = FDATE.AddDays(1);
            }



            BindEW();
            BindSlider();
            if (txtFromDate.Text == txtDateTo.Text)
            {
                Session["DC"] = 0;
            }
            else
            {
                Session["DC"] = 1;
            }

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
    protected void DDLVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        // BindIndex();
        Session["DetVessel_Id"] = DDLVessel.SelectedValue;
        BindSlider();
    }

    protected void BindSlider()
    {
        //ddlWatchHours.SelectedValue = (Convert.ToInt32(Session["SHour"]) + 1).ToString();
        //txtFromDate.Text = Convert.ToDateTime(Session["SDate"]).ToString("dd/MM/yyyy");


        List<DateWatchHour> ListDateWatchHour = new List<DateWatchHour>();
        Dictionary<int, string> lDicHours = ((Dictionary<int, string>)Session["lDicHours"]);
        int k = Convert.ToInt32(Session["SHour"]);

        // LoadDetails(((DateTime)Session["SDate"]).Date, k);

        DateTime lSdate = ((DateTime)Session["SDate"]).Date;
        for (int i = 0; i < 6; i++)
        {
            DateWatchHour lObjDateWatchHour = new DateWatchHour();




            if (k == 6)
            {
                k = 0;
                lSdate = lSdate.Date.AddDays(1);
            }
            lObjDateWatchHour.WatchDate = lSdate;
            lObjDateWatchHour.WatchHours = lDicHours[k];

            ListDateWatchHour.Add(lObjDateWatchHour);
            k++;
        }

        rpSilider.DataSource = ListDateWatchHour;
        rpSilider.DataBind();

        txtFromDate.Text = Convert.ToDateTime(ListDateWatchHour[0].WatchDate).ToString("dd/MM/yyyy");
        txtDateTo.Text = Convert.ToDateTime(ListDateWatchHour[5].WatchDate).ToString("dd/MM/yyyy");





        ddlWatchHours.SelectedIndex = lDicHours.FirstOrDefault(x => x.Value == ListDateWatchHour[0].WatchHours).Key;
        ddlWatchHourT.SelectedIndex = lDicHours.FirstOrDefault(x => x.Value == ListDateWatchHour[5].WatchHours).Key;


        LoadDetails(((DateTime)Session["SDate"]).Date, Convert.ToInt32(Session["SHour"]));
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
            try
            {
                DDLVessel.SelectedValue = Request.QueryString["Vessel_Id"].ToString();
                Session["DetVessel_Id"] = Request.QueryString["Vessel_Id"].ToString();
            }
            catch (Exception)
            {
                DDLVessel.SelectedIndex = 1;
                Session["DetVessel_Id"] = DDLVessel.SelectedValue;
                BindSlider();
            }
            BindSlider();
        }
        catch (Exception ex)
        {

        }


    }
    protected void LoadDetails(DateTime lDate, int lLogW)
    {

        int rowcount = 0;
        //Repeater fvMem = (Repeater)FormView1.Row.Cells[0].FindControl("rpEngine1");



        int PageFrom = lLogW + 1;
        int PageTo = PageFrom + 5;

        DataSet ds = BLL_Tec_ErLog.ErLog_Seach_All_Details(0, Convert.ToInt32(Session["DetVessel_Id"]), 1, 6, ref  rowcount, PageFrom, PageTo, lDate, lDate.AddDays(1));

        for (int k = 0; k < 7; k++)
        {

            int i = 0;
            foreach (DataRow item in ds.Tables[k].Rows)
            {
                item["LOG_WATCH"] = Convert.ToDateTime(((Label)rpSilider.Items[i].FindControl("lblDateSlider")).Text).ToString("dd/MM/yy") + " " + item["LOG_WATCH"];
                i++;
            }
        }


        rpEngine1.DataSource = ds.Tables[0];
        rpEngine1.DataBind();
        rpEngine2.DataSource = ds.Tables[1];
        rpEngine2.DataBind();
        rpEngine3.DataSource = ds.Tables[2];
        rpEngine3.DataBind();
        rpTrainingDetails.DataSource = ds.Tables[3];
        rpTrainingDetails.DataBind();
        Repeater1.DataSource = ds.Tables[4];
        Repeater1.DataBind();
        Repeater2.DataSource = ds.Tables[5];
        Repeater2.DataBind();
        Repeater3.DataSource = ds.Tables[6];
        Repeater3.DataBind();


    }



    protected void rpSilider_ItemDataBound(object sender, DataListItemEventArgs e)
    {


        //DateWatchHour lObjCurItem = (DateWatchHour)e.Item.DataItem;
        //if (lObjCurItem.WatchDate.Date == DateTime.Now.Date)
        //{
        //    e.Item.CssClass = "CurCell";
        //}

    }
    protected void btnPrevHour_Click(object sender, ImageClickEventArgs e)
    {
        int k = Convert.ToInt32(Session["SHour"]);
        k--;
        if (k == -1)
        {
            k = 5;
            Session["SDate"] = ((DateTime)Session["SDate"]).Date.AddDays(-1);
        }
        Session["SHour"] = k;
        BindSlider();
    }
    protected void btnNextHour_Click(object sender, ImageClickEventArgs e)
    {
        List<DateWatchHour> ListDateWatchHour = new List<DateWatchHour>();
        Dictionary<int, string> lDicHours = ((Dictionary<int, string>)Session["lDicHours"]);






        Label lblDateSlider0 = rpSilider.Items[0].FindControl("lblDateSlider") as Label;
        Label lblDateWach0 = rpSilider.Items[0].FindControl("lblDateWach") as Label;

        Label lblDateSlider = rpSilider.Items[5].FindControl("lblDateSlider") as Label;
        Label lblDateWach = rpSilider.Items[5].FindControl("lblDateWach") as Label;


        //DateTime FDATE = Convert.ToDateTime(Session["FDATE"]);
        //DateTime lTPDATE = Convert.ToDateTime(lblDateSlider0.Text).Date;
        //int s = lDicHours.FirstOrDefault(x => x.Value == lblDateWach0.Text).Key * 4;

        //lTPDATE = lTPDATE.AddHours(s);

        //if (lTPDATE > FDATE || lTPDATE == FDATE)
        //{

        //    return;
        //}


        int testhour = lDicHours.FirstOrDefault(x => x.Value == lblDateWach.Text).Key;
        DateTime testDate = Convert.ToDateTime(lblDateSlider.Text);
        if (testhour == 5)
        {
            testhour = 0;
            testDate = Convert.ToDateTime(lblDateSlider.Text).Date.AddDays(1);

        }
        else
        {
            testhour = testhour + 1;
        }


        




        if (DDLVessel.SelectedIndex == 0)
        {



            

            string msgmodal = String.Format("alert('Please Select Vessel!.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", msgmodal, true);

            return;
        }
        if (testDate <= Convert.ToDateTime(Session["XDATE"]))
        {
            if (testDate < Convert.ToDateTime(Session["XDATE"]))
            {

            }
            else
            {
                int lDW = testhour;
                int lAR = Convert.ToInt32(Session["XHOUR"]);
                if (lDW > lAR)
                {
                    
                    return;
                }
            }

        }
        else
        {
            
            return;
        }

        




        int k = Convert.ToInt32(Session["SHour"]);
        k++;






        DateTime lDate = Convert.ToDateTime(lblDateSlider.Text);
        if (k == 6)
        {
            k = 0;


            if (lDate.Date < DateTime.Now.Date)
            {
                Session["SDate"] = ((DateTime)Session["SDate"]).Date.AddDays(1);
            }
            else if (lDate.Date == DateTime.Now.Date)
            {
                int Shr = lDicHours.FirstOrDefault(x => x.Value == lblDateWach.Text).Key;
                if (Shr <= k)
                {
                    Session["SDate"] = ((DateTime)Session["SDate"]).Date.AddDays(1);
                }
                else
                {
                    return;
                }
            }
            else if (lDate.Date > DateTime.Now.Date)
            {
                return;
            }

        }






        Session["SHour"] = k;
        BindSlider();

    }
    protected void ddlWatchHours_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dh = new DataSet();
        Dictionary<int, string> lDicHours = ((Dictionary<int, string>)Session["lDicHours"]);
            Label lblDateWach = rpSilider.Items[0].FindControl("lblDateWach") as Label;
            //DateTime FDATE = Convert.ToDateTime(Session["FDATE"]);
            //DateTime lTPDATE = Convert.ToDateTime(txtFromDate.Text).Date;
            //int s = lDicHours.FirstOrDefault(x => x.Value == ddlWatchHours.SelectedValue).Key * 4;
        // lTPDATE = lTPDATE.AddHours(lDicHours.FirstOrDefault(x => x.Value == ddlWatchHours.SelectedValue).Key * 4);
        //if (lTPDATE > FDATE || lTPDATE == FDATE)
        //{
        //    ddlWatchHours.SelectedIndex = lDicHours.FirstOrDefault(x => x.Value == lblDateWach.Text).Key;
        //    return;
        //}







        if (DDLVessel.SelectedIndex == 0)
        {



            ddlWatchHours.SelectedIndex = lDicHours.FirstOrDefault(x => x.Value == lblDateWach.Text).Key;

            string msgmodal = String.Format("alert('Please Select Vessel!.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", msgmodal, true);

            return;
        }
        if (Convert.ToDateTime(txtFromDate.Text) <= Convert.ToDateTime(Session["FDATE"]))
        {
            if (Convert.ToDateTime(txtFromDate.Text) < Convert.ToDateTime(Session["FDATE"]))
            {

            }
            else
            {
                int lDW = lDicHours.FirstOrDefault(x => x.Value == ddlWatchHours.SelectedItem.Text).Key;
                int lAR = Convert.ToInt32(Session["FHOUR"]);
                if (lDW > lAR)
                {
                    ddlWatchHours.SelectedIndex = lDicHours.FirstOrDefault(x => x.Value == lblDateWach.Text).Key;
                    return;
                }
            }

            
        }
        else
        {
            ddlWatchHours.SelectedIndex = lDicHours.FirstOrDefault(x => x.Value == lblDateWach.Text).Key;
            return;
        }






        Session["SHour"] = ddlWatchHours.SelectedIndex;


        BindEW();
        BindSlider();
        if (txtFromDate.Text == txtDateTo.Text)
        {
            Session["DC"] = 0;
        }
        else
        {
            Session["DC"] = 1;
        }
    }

    protected void BindEW()
    {
        int TempHourS = Convert.ToInt32(Session["SHour"]);
        TempHourS = TempHourS + 1;
        int TempHourE = TempHourS + 5;
        if (TempHourE > 6)
        {
            TempHourE = TempHourE - 6;
            //txtDateTo.Text = Convert.ToDateTime(Session["SDate"]).Date.AddDays(1).ToString("dd/MM/yyyy");
        }
        else
        {
            txtDateTo.Text = Convert.ToDateTime(Session["SDate"]).Date.ToString("dd/MM/yyyy");
        }
        ddlWatchHourT.SelectedValue = TempHourE.ToString();
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        Dictionary<int, string> lDicHours = ((Dictionary<int, string>)Session["lDicHours"]);

        Label lblDateSlider = rpSilider.Items[0].FindControl("lblDateSlider") as Label;


        //DateTime FDATE = Convert.ToDateTime(Session["FDATE"]);
        //DateTime lTPDATE = Convert.ToDateTime(txtFromDate.Text).Date;
        //int s = lDicHours.FirstOrDefault(x => x.Value == ddlWatchHours.SelectedValue).Key * 4;
        //lTPDATE = lTPDATE.AddHours(lDicHours.FirstOrDefault(x => x.Value == ddlWatchHours.SelectedValue).Key * 4);
        //if (lTPDATE > FDATE)
        //{
        //    txtFromDate.Text = Convert.ToDateTime(lblDateSlider.Text).Date.ToString("dd/MM/yyyy");
        //    return;
        //}

       

        if (DDLVessel.SelectedIndex == 0)
        {



            txtFromDate.Text = Convert.ToDateTime(lblDateSlider.Text).Date.ToString("dd/MM/yyyy");

            string msgmodal = String.Format("alert('Please Select Vessel!.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", msgmodal, true);

            return;
        }
        if (Convert.ToDateTime(txtFromDate.Text) <= Convert.ToDateTime(Session["FDATE"]))
        {
            if (Convert.ToDateTime(txtFromDate.Text) < Convert.ToDateTime(Session["FDATE"]))
            {

            }
            else
            {
                int lDW = lDicHours.FirstOrDefault(x => x.Value == ddlWatchHours.SelectedItem.Text).Key;
                int lAR = Convert.ToInt32(Session["FHOUR"]);
                if (lDW > lAR)
                {
                    txtFromDate.Text = Convert.ToDateTime(lblDateSlider.Text).Date.ToString("dd/MM/yyyy");
                    return;
                }
            }
        }
        else
        {
            txtFromDate.Text = Convert.ToDateTime(lblDateSlider.Text).Date.ToString("dd/MM/yyyy");
            return;
        }

        
        
        
        Session["SDate"] = Convert.ToDateTime(txtFromDate.Text);
        BindEW();
        BindSlider();
    }
    protected void txtDateTo_TextChanged(object sender, EventArgs e)
    {
        Dictionary<int, string> lDicHours = ((Dictionary<int, string>)Session["lDicHours"]);

        Label lblDateSlider = rpSilider.Items[5].FindControl("lblDateSlider") as Label;



        //DateTime FDATE = Convert.ToDateTime(Session["FDATE"]);
        //DateTime lTPDATE = Convert.ToDateTime(txtDateTo.Text).Date;
        //int s = lDicHours.FirstOrDefault(x => x.Value == ddlWatchHours.SelectedValue).Key * 4;
        //lTPDATE = lTPDATE.AddHours(lDicHours.FirstOrDefault(x => x.Value == ddlWatchHourT.SelectedValue).Key * 4);
        //lTPDATE = lTPDATE.AddDays(-1);
        //if (lTPDATE > FDATE || lTPDATE == FDATE)
        //{
        //    txtDateTo.Text = Convert.ToDateTime(lblDateSlider.Text).ToString("dd/MM/yyyy");
        //    return;
        //}



       // txtDateTo.Text = Convert.ToDateTime(lblDateSlider.Text).ToString("dd/MM/yyyy");



        //-------------------------------------------------------------







        if (DDLVessel.SelectedIndex == 0)
        {



            txtDateTo.Text = Convert.ToDateTime(lblDateSlider.Text).ToString("dd/MM/yyyy");

            string msgmodal = String.Format("alert('Please Select Vessel!.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", msgmodal, true);

            return;
        }
        if (Convert.ToDateTime(txtDateTo.Text) <= Convert.ToDateTime(Session["XDATE"]))
        {
            if (Convert.ToDateTime(txtDateTo.Text) < Convert.ToDateTime(Session["XDATE"]))
            {

            }
            else
            {
                int lDW = lDicHours.FirstOrDefault(x => x.Value == ddlWatchHourT.SelectedItem.Text).Key;
                int lAR = Convert.ToInt32(Session["XHOUR"]);
                if (lDW > lAR)
                {
                    txtDateTo.Text = Convert.ToDateTime(lblDateSlider.Text).ToString("dd/MM/yyyy");
                    return;
                }
            }

        }
        else
        {
            txtDateTo.Text = Convert.ToDateTime(lblDateSlider.Text).ToString("dd/MM/yyyy");
            return;
        }



        if (ddlWatchHourT.SelectedIndex == 5)
        {
            Session["SHour"] = 0;
            Session["SDATE"] = Convert.ToDateTime(txtDateTo.Text).Date;
        }
        else
        {
            Session["SHour"] = ddlWatchHourT.SelectedIndex + 1;
            Session["SDATE"] = Convert.ToDateTime(txtDateTo.Text).Date.AddDays(-1);
        }


        BindSlider();
        //if (Session["DC"].ToString() == "0")
        //{
        //    txtFromDate.Text = txtDateTo.Text;
        //}
        //else
        //{
        //    txtFromDate.Text = Convert.ToDateTime(txtDateTo.Text).Date.AddDays(-1).ToString("dd/MM/yyyy");
        //}
       // Session["SDate"] = Convert.ToDateTime(txtFromDate.Text);
      

    }
    protected void ddlWatchHourT_SelectedIndexChanged(object sender, EventArgs e)
    {




        Dictionary<int, string> lDicHours = ((Dictionary<int, string>)Session["lDicHours"]);

       // Label lblDateSlider = rpSilider.Items[5].FindControl("lblDateSlider") as Label;
        Label lblDateWach = rpSilider.Items[5].FindControl("lblDateWach") as Label;


        //DateTime FDATE = Convert.ToDateTime(Session["FDATE"]);
        //DateTime lTPDATE = Convert.ToDateTime(txtDateTo.Text).Date;
        //int s = lDicHours.FirstOrDefault(x => x.Value == ddlWatchHours.SelectedValue).Key * 4;
        //lTPDATE = lTPDATE.AddHours(lDicHours.FirstOrDefault(x => x.Value == ddlWatchHourT.SelectedValue).Key * 4);
        //lTPDATE = lTPDATE.AddDays(-1);
        //if (lTPDATE > FDATE)
        //{
        //    ddlWatchHourT.SelectedIndex = lDicHours.FirstOrDefault(x => x.Value == lblDateWach.Text).Key;
        //    return;
        //}



        //ddlWatchHourT.SelectedIndex = lDicHours.FirstOrDefault(x => x.Value == lblDateWach.Text).Key;



        if (DDLVessel.SelectedIndex == 0)
        {



            ddlWatchHourT.SelectedIndex = lDicHours.FirstOrDefault(x => x.Value == lblDateWach.Text).Key;

            string msgmodal = String.Format("alert('Please Select Vessel!.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", msgmodal, true);

            return;
        }
        if (Convert.ToDateTime(txtDateTo.Text) <= Convert.ToDateTime(Session["XDATE"]))
        {
            if (Convert.ToDateTime(txtDateTo.Text) < Convert.ToDateTime(Session["XDATE"]))
            {

            }
            else
            {
                int lDW = lDicHours.FirstOrDefault(x => x.Value == ddlWatchHourT.SelectedItem.Text).Key;
                int lAR = Convert.ToInt32(Session["XHOUR"]);
                if (lDW > lAR)
                {
                    ddlWatchHourT.SelectedIndex = lDicHours.FirstOrDefault(x => x.Value == lblDateWach.Text).Key;
                    return;
                }
            }
     
        }
        else
        {
            ddlWatchHourT.SelectedIndex = lDicHours.FirstOrDefault(x => x.Value == lblDateWach.Text).Key;
            return;
        }



        //int TempHourS = Convert.ToInt32(ddlWatchHourT.SelectedValue);
        //TempHourS = TempHourS - 5;
        //if (TempHourS < 1)
        //{
        //    TempHourS = TempHourS + 6;

        //}
        //Session["SHour"] = TempHourS - 1;

        if (ddlWatchHourT.SelectedIndex == 5)
        {
            Session["SHour"] = 0;
            Session["SDATE"] = Convert.ToDateTime(txtDateTo.Text).Date;
        }
        else
        {
            Session["SHour"] = ddlWatchHourT.SelectedIndex + 1;
            Session["SDATE"] = Convert.ToDateTime(txtDateTo.Text).Date.AddDays(-1);
        }

      





        BindSlider();
        //TempHourS = Convert.ToInt32(Session["SHour"]);
        //TempHourS = TempHourS + 1;
        //int TempHourE = TempHourS + 5;
        //if (TempHourE > 6)
        //{
        //    TempHourE = TempHourE - 6;
        //    txtDateTo.Text = Convert.ToDateTime(Session["SDate"]).Date.AddDays(1).ToString("dd/MM/yyyy");
        //}
        //else
        //{
        //    txtDateTo.Text = Convert.ToDateTime(Session["SDate"]).Date.ToString("dd/MM/yyyy");
        //}
        if (txtFromDate.Text == txtDateTo.Text)
        {
            Session["DC"] = 0;
        }
        else
        {
            Session["DC"] = 1;
        }
    }
}

public class DateWatchHour
{
    public DateTime WatchDate { get; set; }
    public String WatchHours { get; set; }

}