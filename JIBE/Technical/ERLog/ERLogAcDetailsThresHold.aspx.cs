using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Technical;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class ERLogAcDetailsThresHold : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    public UserAccess objUA = new UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        lblLogId.Attributes.Add("style", "visibility:hidden");
        if (!IsPostBack)
        {
            if (Request.QueryString["LOGID"] != null)
            {
              lblLogId.Text = Request.QueryString["LOGID"].ToString();             
            }
            if (Request.QueryString["VESSELID"] != null)
            {
                ViewState["VESSELID"] = Request.QueryString["VESSELID"].ToString();
            }
            BindViews();
        }
    }

    private void BindViews()
    {
        //DataTable dt = BLL_Tec_ErLog.ErLog_ME_00_Get( int.Parse(lblLogId.Text));
        DataTable dt = BLL_Tec_ErLog.ErLog_ThresHold_Main_EDIT(int.Parse(ViewState["VESSELID"].ToString()));
        if (dt.Rows.Count > 0)
        {
            FormView1.DataSource = dt;
            FormView1.DataBind();           
        }

    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            DataTable dt    =   BLL_Tec_ErLog.ErLog_AC_FM_MISC_EDIT(int.Parse(ViewState["VESSELID"].ToString()));
            Repeater  fvDetails = (Repeater)FormView1.Row.Cells[0].FindControl("rpEngine3");
            if (fvDetails != null)
            {
                fvDetails.DataSource = dt;
                fvDetails.DataBind();
            }
            fvDetails = (Repeater)FormView1.Row.Cells[0].FindControl("Repeater1");
            if (fvDetails != null)
            {
                fvDetails.DataSource = dt;
                fvDetails.DataBind();
            }
            fvDetails = (Repeater)FormView1.Row.Cells[0].FindControl("Repeater2");
            if (fvDetails != null)
            {
                fvDetails.DataSource = dt;
                fvDetails.DataBind();
            }
           
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Repeater fvDetails = (Repeater)FormView1.Row.Cells[0].FindControl("rpEngine3");
        Repeater fvDetails1 = (Repeater)FormView1.Row.Cells[0].FindControl("Repeater1");
        Repeater fvDetails2 = (Repeater)FormView1.Row.Cells[0].FindControl("Repeater2");

        int i = -1;

        foreach (RepeaterItem ritem in fvDetails.Items)
        {
             bool valstatus = true;
             i = i + 1; ;
            Label lblid = (Label)ritem.FindControl("lblid");
            Label lbllogid = (Label)ritem.FindControl("lblLogId");
            TextBox tx1 = (TextBox)ritem.FindControl("txt1");
            TextBox tx2 = (TextBox)ritem.FindControl("txt2");
            TextBox tx3 = (TextBox)ritem.FindControl("txt3");
            TextBox tx4 = (TextBox)ritem.FindControl("txt4");
            TextBox tx5 = (TextBox)ritem.FindControl("txt5");
            TextBox tx6 = (TextBox)ritem.FindControl("txt6");
            TextBox tx7 = (TextBox)ritem.FindControl("txt7");
            TextBox tx8 = (TextBox)ritem.FindControl("txt8");
            TextBox tx9 = (TextBox)ritem.FindControl("txt9");
            TextBox tx10 = (TextBox)ritem.FindControl("txt10");
            TextBox tx11 = (TextBox)ritem.FindControl("txt11");
            TextBox tx12 = (TextBox)ritem.FindControl("txt12");
            TextBox tx13 = (TextBox)ritem.FindControl("txt13");
            TextBox tx14 = (TextBox)ritem.FindControl("txt14");
            TextBox tx15 = (TextBox)ritem.FindControl("txt15");
            TextBox tx16 = (TextBox)ritem.FindControl("txt16");
            TextBox tx17 = (TextBox)ritem.FindControl("txt17");
            TextBox tx18 = (TextBox)ritem.FindControl("txt18");
            TextBox tx19 = (TextBox)ritem.FindControl("txt19");
            TextBox tx01 = (TextBox)ritem.FindControl("txt01");
            TextBox tx02 = (TextBox)ritem.FindControl("txt02");
            TextBox tx03 = (TextBox)ritem.FindControl("txt03");
            TextBox tx04 = (TextBox)ritem.FindControl("txt04");
            TextBox tx05 = (TextBox)ritem.FindControl("txt05");
            TextBox tx06 = (TextBox)ritem.FindControl("txt06");
            TextBox tx07 = (TextBox)ritem.FindControl("txt07");
            TextBox tx08 = (TextBox)ritem.FindControl("txt08");
            TextBox tx09 = (TextBox)ritem.FindControl("txt09");
            TextBox tx010 = (TextBox)ritem.FindControl("txt010");
            TextBox tx011 = (TextBox)ritem.FindControl("txt011");
            TextBox tx012 = (TextBox)ritem.FindControl("txt012");
            TextBox tx013 = (TextBox)ritem.FindControl("txt013");
            TextBox tx014 = (TextBox)ritem.FindControl("txt014");
            TextBox tx015 = (TextBox)ritem.FindControl("txt015");
            TextBox tx016 = (TextBox)ritem.FindControl("txt016");
            TextBox tx017 = (TextBox)ritem.FindControl("txt017");
            TextBox tx018 = (TextBox)ritem.FindControl("txt018");
            TextBox tx019 = (TextBox)ritem.FindControl("txt019");

            RepeaterItem ritem1 = fvDetails2.Items[i];

            TextBox tx20 = (TextBox)ritem1.FindControl("txt20");
            TextBox tx21 = (TextBox)ritem1.FindControl("txt21");
            TextBox tx22 = (TextBox)ritem1.FindControl("txt22");
            TextBox tx23 = (TextBox)ritem1.FindControl("txt23");
            TextBox tx24 = (TextBox)ritem1.FindControl("txt24");
            TextBox tx25 = (TextBox)ritem1.FindControl("txt25");
            TextBox tx26 = (TextBox)ritem1.FindControl("txt26");
            TextBox tx27 = (TextBox)ritem1.FindControl("txt27");
            TextBox tx28 = (TextBox)ritem1.FindControl("txt28");
            TextBox tx29 = (TextBox)ritem1.FindControl("txt29");
            TextBox tx30 = (TextBox)ritem1.FindControl("txt30");
            TextBox tx31 = (TextBox)ritem1.FindControl("txt31");
            TextBox tx32 = (TextBox)ritem1.FindControl("txt32");
            TextBox tx020 = (TextBox)ritem1.FindControl("txt020");
            TextBox tx021 = (TextBox)ritem1.FindControl("txt021");
            TextBox tx022 = (TextBox)ritem1.FindControl("txt022");
            TextBox tx023 = (TextBox)ritem1.FindControl("txt023");
            TextBox tx024 = (TextBox)ritem1.FindControl("txt024");
            TextBox tx025 = (TextBox)ritem1.FindControl("txt025");
            TextBox tx026 = (TextBox)ritem1.FindControl("txt026");
            TextBox tx027 = (TextBox)ritem1.FindControl("txt027");
            TextBox tx028 = (TextBox)ritem1.FindControl("txt028");
            TextBox tx029 = (TextBox)ritem1.FindControl("txt029");
            TextBox tx030 = (TextBox)ritem1.FindControl("txt030");
            TextBox tx031 = (TextBox)ritem1.FindControl("txt031");
            TextBox tx032 = (TextBox)ritem1.FindControl("txt032");
           

            RepeaterItem ritem2 = fvDetails1.Items[i];
            TextBox tx33 = (TextBox)ritem2.FindControl("txt33");
            TextBox tx34 = (TextBox)ritem2.FindControl("txt34");
            TextBox tx35 = (TextBox)ritem2.FindControl("txt35");
            TextBox tx36 = (TextBox)ritem2.FindControl("txt36");
            TextBox tx37 = (TextBox)ritem2.FindControl("txt37");
            TextBox tx38 = (TextBox)ritem2.FindControl("txt38");
            TextBox tx39 = (TextBox)ritem2.FindControl("txt39");
            TextBox tx40 = (TextBox)ritem2.FindControl("txt40");
            TextBox tx41 = (TextBox)ritem2.FindControl("txt41");
            TextBox tx42 = (TextBox)ritem2.FindControl("txt42");
            TextBox tx43 = (TextBox)ritem2.FindControl("txt43");
            TextBox tx44 = (TextBox)ritem2.FindControl("txt44");
            TextBox tx45 = (TextBox)ritem2.FindControl("txt45");
            TextBox tx46 = (TextBox)ritem2.FindControl("txt46");
            TextBox tx47 = (TextBox)ritem2.FindControl("txt47");
            TextBox tx48 = (TextBox)ritem2.FindControl("txt48");
            TextBox tx033 = (TextBox)ritem2.FindControl("txt33");
            TextBox tx034 = (TextBox)ritem2.FindControl("txt34");
            TextBox tx035 = (TextBox)ritem2.FindControl("txt35");
            TextBox tx036 = (TextBox)ritem2.FindControl("txt36");
            TextBox tx037 = (TextBox)ritem2.FindControl("txt37");
            TextBox tx038 = (TextBox)ritem2.FindControl("txt38");
            TextBox tx039 = (TextBox)ritem2.FindControl("txt39");
            TextBox tx040 = (TextBox)ritem2.FindControl("txt40");
            TextBox tx041 = (TextBox)ritem2.FindControl("txt41");
            TextBox tx042 = (TextBox)ritem2.FindControl("txt42");
            TextBox tx043 = (TextBox)ritem2.FindControl("txt43");
            TextBox tx044 = (TextBox)ritem2.FindControl("txt44");
            TextBox tx045 = (TextBox)ritem2.FindControl("txt45");
            TextBox tx046 = (TextBox)ritem2.FindControl("txt46");
            TextBox tx047 = (TextBox)ritem2.FindControl("txt47");
            TextBox tx048 = (TextBox)ritem2.FindControl("txt48");

            if ((UDFLib.ConvertDecimalToNull(tx1.Text) != null) && (UDFLib.ConvertDecimalToNull(tx01.Text) != null) && (decimal.Parse(tx1.Text) > decimal.Parse(tx01.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx2.Text) != null) && (UDFLib.ConvertDecimalToNull(tx02.Text) != null) && (decimal.Parse(tx2.Text) > decimal.Parse(tx02.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx3.Text) != null) && (UDFLib.ConvertDecimalToNull(tx03.Text) != null) && (decimal.Parse(tx3.Text) > decimal.Parse(tx03.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx4.Text) != null) && (UDFLib.ConvertDecimalToNull(tx04.Text) != null) && (decimal.Parse(tx4.Text) > decimal.Parse(tx04.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx5.Text) != null) && (UDFLib.ConvertDecimalToNull(tx05.Text) != null) && (decimal.Parse(tx5.Text) > decimal.Parse(tx05.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx6.Text) != null) && (UDFLib.ConvertDecimalToNull(tx06.Text) != null) && (decimal.Parse(tx6.Text) > decimal.Parse(tx06.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx7.Text) != null) && (UDFLib.ConvertDecimalToNull(tx07.Text) != null) && (decimal.Parse(tx7.Text) > decimal.Parse(tx07.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx8.Text) != null) && (UDFLib.ConvertDecimalToNull(tx08.Text) != null) && (decimal.Parse(tx8.Text) > decimal.Parse(tx08.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx9.Text) != null) && (UDFLib.ConvertDecimalToNull(tx09.Text) != null) && (decimal.Parse(tx9.Text) > decimal.Parse(tx09.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx10.Text) != null) && (UDFLib.ConvertDecimalToNull(tx010.Text) != null) && (decimal.Parse(tx10.Text) > decimal.Parse(tx010.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx11.Text) != null) && (UDFLib.ConvertDecimalToNull(tx011.Text) != null) && (decimal.Parse(tx11.Text) > decimal.Parse(tx011.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx12.Text) != null) && (UDFLib.ConvertDecimalToNull(tx012.Text) != null) && (decimal.Parse(tx12.Text) > decimal.Parse(tx012.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx13.Text) != null) && (UDFLib.ConvertDecimalToNull(tx013.Text) != null) && (decimal.Parse(tx13.Text) > decimal.Parse(tx013.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx14.Text) != null) && (UDFLib.ConvertDecimalToNull(tx014.Text) != null) && (decimal.Parse(tx14.Text) > decimal.Parse(tx014.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx15.Text) != null) && (UDFLib.ConvertDecimalToNull(tx015.Text) != null) && (decimal.Parse(tx15.Text) > decimal.Parse(tx015.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx16.Text) != null) && (UDFLib.ConvertDecimalToNull(tx016.Text) != null) && (decimal.Parse(tx16.Text) > decimal.Parse(tx016.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx17.Text) != null) && (UDFLib.ConvertDecimalToNull(tx017.Text) != null) && (decimal.Parse(tx17.Text) > decimal.Parse(tx017.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx18.Text) != null) && (UDFLib.ConvertDecimalToNull(tx018.Text) != null) && (decimal.Parse(tx18.Text) > decimal.Parse(tx018.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx19.Text) != null) && (UDFLib.ConvertDecimalToNull(tx019.Text) != null) && (decimal.Parse(tx19.Text) > decimal.Parse(tx019.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx20.Text) != null) && (UDFLib.ConvertDecimalToNull(tx020.Text) != null) && (decimal.Parse(tx20.Text) > decimal.Parse(tx020.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx21.Text) != null) && (UDFLib.ConvertDecimalToNull(tx021.Text) != null) && (decimal.Parse(tx21.Text) > decimal.Parse(tx021.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx22.Text) != null) && (UDFLib.ConvertDecimalToNull(tx022.Text) != null) && (decimal.Parse(tx22.Text) > decimal.Parse(tx022.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx23.Text) != null) && (UDFLib.ConvertDecimalToNull(tx023.Text) != null) && (decimal.Parse(tx23.Text) > decimal.Parse(tx023.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx24.Text) != null) && (UDFLib.ConvertDecimalToNull(tx024.Text) != null) && (decimal.Parse(tx24.Text) > decimal.Parse(tx024.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx25.Text) != null) && (UDFLib.ConvertDecimalToNull(tx025.Text) != null) && (decimal.Parse(tx25.Text) > decimal.Parse(tx025.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx26.Text) != null) && (UDFLib.ConvertDecimalToNull(tx026.Text) != null) && (decimal.Parse(tx26.Text) > decimal.Parse(tx026.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx27.Text) != null) && (UDFLib.ConvertDecimalToNull(tx027.Text) != null) && (decimal.Parse(tx27.Text) > decimal.Parse(tx027.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx28.Text) != null) && (UDFLib.ConvertDecimalToNull(tx028.Text) != null) && (decimal.Parse(tx28.Text) > decimal.Parse(tx028.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx29.Text) != null) && (UDFLib.ConvertDecimalToNull(tx029.Text) != null) && (decimal.Parse(tx29.Text) > decimal.Parse(tx029.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx30.Text) != null) && (UDFLib.ConvertDecimalToNull(tx030.Text) != null) && (decimal.Parse(tx30.Text) > decimal.Parse(tx030.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx31.Text) != null) && (UDFLib.ConvertDecimalToNull(tx031.Text) != null) && (decimal.Parse(tx31.Text) > decimal.Parse(tx031.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx32.Text) != null) && (UDFLib.ConvertDecimalToNull(tx032.Text) != null) && (decimal.Parse(tx32.Text) > decimal.Parse(tx032.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx33.Text) != null) && (UDFLib.ConvertDecimalToNull(tx033.Text) != null) && (decimal.Parse(tx33.Text) > decimal.Parse(tx033.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx34.Text) != null) && (UDFLib.ConvertDecimalToNull(tx034.Text) != null) && (decimal.Parse(tx34.Text) > decimal.Parse(tx034.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx35.Text) != null) && (UDFLib.ConvertDecimalToNull(tx035.Text) != null) && (decimal.Parse(tx35.Text) > decimal.Parse(tx035.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx36.Text) != null) && (UDFLib.ConvertDecimalToNull(tx036.Text) != null) && (decimal.Parse(tx36.Text) > decimal.Parse(tx036.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx37.Text) != null) && (UDFLib.ConvertDecimalToNull(tx037.Text) != null) && (decimal.Parse(tx37.Text) > decimal.Parse(tx037.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx38.Text) != null) && (UDFLib.ConvertDecimalToNull(tx038.Text) != null) && (decimal.Parse(tx38.Text) > decimal.Parse(tx038.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx39.Text) != null) && (UDFLib.ConvertDecimalToNull(tx039.Text) != null) && (decimal.Parse(tx39.Text) > decimal.Parse(tx039.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx40.Text) != null) && (UDFLib.ConvertDecimalToNull(tx040.Text) != null) && (decimal.Parse(tx40.Text) > decimal.Parse(tx040.Text))) valstatus = false; 
            if ((UDFLib.ConvertDecimalToNull(tx41.Text) != null) && (UDFLib.ConvertDecimalToNull(tx041.Text) != null) && (decimal.Parse(tx41.Text) > decimal.Parse(tx041.Text))) valstatus = false; 
            if ((UDFLib.ConvertDecimalToNull(tx42.Text) != null) && (UDFLib.ConvertDecimalToNull(tx042.Text) != null) && (decimal.Parse(tx42.Text) > decimal.Parse(tx042.Text))) valstatus = false; 
            if ((UDFLib.ConvertDecimalToNull(tx43.Text) != null) && (UDFLib.ConvertDecimalToNull(tx043.Text) != null) && (decimal.Parse(tx43.Text) > decimal.Parse(tx043.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx44.Text) != null) && (UDFLib.ConvertDecimalToNull(tx044.Text) != null) && (decimal.Parse(tx44.Text) > decimal.Parse(tx044.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx45.Text) != null) && (UDFLib.ConvertDecimalToNull(tx045.Text) != null) && (decimal.Parse(tx45.Text) > decimal.Parse(tx045.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx46.Text) != null) && (UDFLib.ConvertDecimalToNull(tx046.Text) != null) && (decimal.Parse(tx46.Text) > decimal.Parse(tx046.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx47.Text) != null) && (UDFLib.ConvertDecimalToNull(tx047.Text) != null) && (decimal.Parse(tx47.Text) > decimal.Parse(tx047.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx48.Text) != null) && (UDFLib.ConvertDecimalToNull(tx048.Text) != null) && (decimal.Parse(tx48.Text) > decimal.Parse(tx048.Text))) valstatus = false;

            if (valstatus)
            {

                int j = BLL_Tec_ErLog.ErLog_AC_FW_MISC_THRESHOLD_Update(UDFLib.ConvertIntegerToNull(lblid.Text), UDFLib.ConvertIntegerToNull(ViewState["VESSELID"].ToString()), UDFLib.ConvertDecimalToNull(tx1.Text), UDFLib.ConvertDecimalToNull(tx2.Text), UDFLib.ConvertDecimalToNull(tx3.Text),
                    UDFLib.ConvertDecimalToNull(tx4.Text), UDFLib.ConvertDecimalToNull(tx5.Text), UDFLib.ConvertDecimalToNull(tx6.Text), UDFLib.ConvertDecimalToNull(tx7.Text), UDFLib.ConvertDecimalToNull(tx8.Text),
                    UDFLib.ConvertDecimalToNull(tx9.Text), UDFLib.ConvertDecimalToNull(tx10.Text), UDFLib.ConvertDecimalToNull(tx11.Text), UDFLib.ConvertDecimalToNull(tx12.Text), UDFLib.ConvertDecimalToNull(tx13.Text),
                    UDFLib.ConvertDecimalToNull(tx14.Text), UDFLib.ConvertDecimalToNull(tx15.Text), UDFLib.ConvertDecimalToNull(tx16.Text), UDFLib.ConvertDecimalToNull(tx17.Text), UDFLib.ConvertDecimalToNull(tx18.Text),
                    UDFLib.ConvertDecimalToNull(tx19.Text), UDFLib.ConvertDecimalToNull(tx20.Text), UDFLib.ConvertDecimalToNull(tx21.Text), UDFLib.ConvertDecimalToNull(tx22.Text), UDFLib.ConvertDecimalToNull(tx23.Text),
                    UDFLib.ConvertDecimalToNull(tx24.Text), UDFLib.ConvertDecimalToNull(tx25.Text), UDFLib.ConvertDecimalToNull(tx26.Text), UDFLib.ConvertDecimalToNull(tx27.Text), UDFLib.ConvertDecimalToNull(tx28.Text),
                    UDFLib.ConvertDecimalToNull(tx29.Text), UDFLib.ConvertDecimalToNull(tx30.Text), UDFLib.ConvertDecimalToNull(tx31.Text), UDFLib.ConvertDecimalToNull(tx32.Text), UDFLib.ConvertDecimalToNull(tx33.Text),
                    UDFLib.ConvertDecimalToNull(tx34.Text), UDFLib.ConvertDecimalToNull(tx35.Text), UDFLib.ConvertDecimalToNull(tx36.Text), UDFLib.ConvertDecimalToNull(tx37.Text), UDFLib.ConvertDecimalToNull(tx38.Text),
                    UDFLib.ConvertDecimalToNull(tx39.Text), UDFLib.ConvertDecimalToNull(tx40.Text), UDFLib.ConvertDecimalToNull(tx41.Text), UDFLib.ConvertDecimalToNull(tx42.Text), UDFLib.ConvertDecimalToNull(tx43.Text),
                    UDFLib.ConvertDecimalToNull(tx44.Text), UDFLib.ConvertDecimalToNull(tx45.Text), UDFLib.ConvertDecimalToNull(tx46.Text), UDFLib.ConvertDecimalToNull(tx47.Text), UDFLib.ConvertDecimalToNull(tx48.Text),
                    UDFLib.ConvertDecimalToNull(tx01.Text), UDFLib.ConvertDecimalToNull(tx02.Text), UDFLib.ConvertDecimalToNull(tx03.Text), UDFLib.ConvertDecimalToNull(tx04.Text), UDFLib.ConvertDecimalToNull(tx05.Text),
                    UDFLib.ConvertDecimalToNull(tx06.Text), UDFLib.ConvertDecimalToNull(tx07.Text), UDFLib.ConvertDecimalToNull(tx08.Text), UDFLib.ConvertDecimalToNull(tx09.Text), UDFLib.ConvertDecimalToNull(tx010.Text),
                    UDFLib.ConvertDecimalToNull(tx011.Text), UDFLib.ConvertDecimalToNull(tx012.Text), UDFLib.ConvertDecimalToNull(tx013.Text), UDFLib.ConvertDecimalToNull(tx014.Text), UDFLib.ConvertDecimalToNull(tx015.Text),
                    UDFLib.ConvertDecimalToNull(tx016.Text), UDFLib.ConvertDecimalToNull(tx017.Text), UDFLib.ConvertDecimalToNull(tx018.Text), UDFLib.ConvertDecimalToNull(tx019.Text), UDFLib.ConvertDecimalToNull(tx020.Text),
                    UDFLib.ConvertDecimalToNull(tx021.Text), UDFLib.ConvertDecimalToNull(tx022.Text), UDFLib.ConvertDecimalToNull(tx023.Text), UDFLib.ConvertDecimalToNull(tx024.Text), UDFLib.ConvertDecimalToNull(tx025.Text),
                    UDFLib.ConvertDecimalToNull(tx026.Text), UDFLib.ConvertDecimalToNull(tx027.Text), UDFLib.ConvertDecimalToNull(tx028.Text), UDFLib.ConvertDecimalToNull(tx029.Text), UDFLib.ConvertDecimalToNull(tx030.Text),
                    UDFLib.ConvertDecimalToNull(tx031.Text), UDFLib.ConvertDecimalToNull(tx032.Text), UDFLib.ConvertDecimalToNull(tx033.Text), UDFLib.ConvertDecimalToNull(tx034.Text), UDFLib.ConvertDecimalToNull(tx05.Text),
                    UDFLib.ConvertDecimalToNull(tx036.Text), UDFLib.ConvertDecimalToNull(tx037.Text), UDFLib.ConvertDecimalToNull(tx038.Text), UDFLib.ConvertDecimalToNull(tx039.Text), UDFLib.ConvertDecimalToNull(tx040.Text),
                    UDFLib.ConvertDecimalToNull(tx041.Text), UDFLib.ConvertDecimalToNull(tx042.Text), UDFLib.ConvertDecimalToNull(tx043.Text), UDFLib.ConvertDecimalToNull(tx044.Text), UDFLib.ConvertDecimalToNull(tx045.Text),
                    UDFLib.ConvertDecimalToNull(tx046.Text), UDFLib.ConvertDecimalToNull(tx047.Text), UDFLib.ConvertDecimalToNull(tx048.Text), 1);
                string js = "alert('Changes are updated');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
            }
            else
            {
                string js = "alert('Please check your data');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
            }
        }

        TextBox txtBLR_CW_CHLORIDES_BLR = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_CHLORIDES_BLR");
        TextBox txtBLR_CW_CHLORIDES_MEJW = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_CHLORIDES_MEJW");
        TextBox txtBLR_CW_CHLORIDES_MEPW = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_CHLORIDES_MEPW");
        TextBox txtBLR_CW_CHLORIDES_AE = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_CHLORIDES_AE");
        TextBox txtBLR_CW_CHLORIDES_CMPR = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_CHLORIDES_CMPR");
        TextBox txtBLR_CW_ALKALINITY_BLR = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_ALKALINITY_BLR");
        TextBox txtBLR_CW_ALKALINITY_MEJW = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_ALKALINITY_MEJW");
        TextBox txtBLR_CW_ALKALINITY_MEPW = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_ALKALINITY_MEPW");
        TextBox txtBLR_CW_ALKALINITY_AE = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_ALKALINITY_AE");
        TextBox txtBLR_CW_ALKALINITY_CMPR = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_ALKALINITY_CMPR");
        TextBox txtBLR_CW_TALKALINITY_BLR = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_TALKALINITY_BLR");
        TextBox txtBLR_CW_TALKALINITY_MEJW = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_TALKALINITY_MEJW");
        TextBox txtBLR_CW_TALKALINITY_MEPW = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_TALKALINITY_MEPW");
        TextBox txtBLR_CW_TALKALINITY_AE = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_TALKALINITY_AE");
        TextBox txtBLR_CW_TALKALINITY_CMPR = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_TALKALINITY_CMPR");
        TextBox txtBLR_CW_PHOSPHATES_BLR = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_PHOSPHATES_BLR");
        TextBox txtBLR_CW_PHOSPHATES_MEJW = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_PHOSPHATES_MEJW");
        TextBox txtBLR_CW_PHOSPHATES_MEPW = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_PHOSPHATES_MEPW");
        TextBox txtBLR_CW_PHOSPHATES_AE = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_PHOSPHATES_AE");
        TextBox txtBLR_CW_PHOSPHATES_CMPR = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_PHOSPHATES_CMPR");
        TextBox txtBLR_CW_BLOWDOWN_BLR = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_BLOWDOWN_BLR");
        TextBox txtBLR_CW_BLOWDOWN_MEJW = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_BLOWDOWN_MEJW");
        TextBox txtBLR_CW_BLOWDOWN_MEPW = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_BLOWDOWN_MEPW");
        TextBox txtBLR_CW_BLOWDOWN_AE = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_BLOWDOWN_AE");
        TextBox txtBLR_CW_BLOWDOWN_CMPR = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_BLOWDOWN_CMPR");
        TextBox txtBLR_CW_NITRITES_BLR = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_NITRITES_BLR");
        TextBox txtBLR_CW_NITRITES_MEJW = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_NITRITES_MEJW");
        TextBox txtBLR_CW_NITRITES_MEPW = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_NITRITES_MEPW");
        TextBox txtBLR_CW_NITRITES_AE = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_NITRITES_AE");
        TextBox txtBLR_CW_NITRITES_CMPR = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_NITRITES_CMPR");
        TextBox txtBLR_CW_DOSAGE_BLR = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_DOSAGE_BLR");
        TextBox txtBLR_CW_DOSAGE_MEJW = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_DOSAGE_MEJW");
        TextBox txtBLR_CW_DOSAGE_MEPW = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_DOSAGE_MEPW");
        TextBox txtBLR_CW_DOSAGE_AE = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_DOSAGE_AE");
        TextBox txtBLR_CW_DOSAGE_CMPR = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_DOSAGE_CMPR");

        TextBox txtBLR_CW_CHLORIDES_BLR_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_CHLORIDES_BLR_Max");
        TextBox txtBLR_CW_CHLORIDES_MEJW_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_CHLORIDES_MEJW_Max");
        TextBox txtBLR_CW_CHLORIDES_MEPW_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_CHLORIDES_MEPW_Max");
        TextBox txtBLR_CW_CHLORIDES_AE_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_CHLORIDES_AE_Max");
        TextBox txtBLR_CW_CHLORIDES_CMPR_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_CHLORIDES_CMPR_Max");
        TextBox txtBLR_CW_ALKALINITY_BLR_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_ALKALINITY_BLR_Max");
        TextBox txtBLR_CW_ALKALINITY_MEJW_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_ALKALINITY_MEJW_Max");
        TextBox txtBLR_CW_ALKALINITY_MEPW_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_ALKALINITY_MEPW_Max");
        TextBox txtBLR_CW_ALKALINITY_AE_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_ALKALINITY_AE_Max");
        TextBox txtBLR_CW_ALKALINITY_CMPR_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_ALKALINITY_CMPR_Max");
        TextBox txtBLR_CW_TALKALINITY_BLR_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_TALKALINITY_BLR_Max");
        TextBox txtBLR_CW_TALKALINITY_MEJW_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_TALKALINITY_MEJW_Max");
        TextBox txtBLR_CW_TALKALINITY_MEPW_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_TALKALINITY_MEPW_Max");
        TextBox txtBLR_CW_TALKALINITY_AE_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_TALKALINITY_AE_Max");
        TextBox txtBLR_CW_TALKALINITY_CMPR_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_TALKALINITY_CMPR_Max");
        TextBox txtBLR_CW_PHOSPHATES_BLR_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_PHOSPHATES_BLR_Max");
        TextBox txtBLR_CW_PHOSPHATES_MEJW_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_PHOSPHATES_MEJW_Max");
        TextBox txtBLR_CW_PHOSPHATES_MEPW_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_PHOSPHATES_MEPW_Max");
        TextBox txtBLR_CW_PHOSPHATES_AE_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_PHOSPHATES_AE_Max");
        TextBox txtBLR_CW_PHOSPHATES_CMPR_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_PHOSPHATES_CMPR_Max");
        TextBox txtBLR_CW_BLOWDOWN_BLR_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_BLOWDOWN_BLR_Max");
        TextBox txtBLR_CW_BLOWDOWN_MEJW_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_BLOWDOWN_MEJW_Max");
        TextBox txtBLR_CW_BLOWDOWN_MEPW_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_BLOWDOWN_MEPW_Max");
        TextBox txtBLR_CW_BLOWDOWN_AE_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_BLOWDOWN_AE_Max");
        TextBox txtBLR_CW_BLOWDOWN_CMPR_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_BLOWDOWN_CMPR_Max");
        TextBox txtBLR_CW_NITRITES_BLR_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_NITRITES_BLR_Max");
        TextBox txtBLR_CW_NITRITES_MEJW_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_NITRITES_MEJW_Max");
        TextBox txtBLR_CW_NITRITES_MEPW_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_NITRITES_MEPW_Max");
        TextBox txtBLR_CW_NITRITES_AE_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_NITRITES_AE_Max");
        TextBox txtBLR_CW_NITRITES_CMPR_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_NITRITES_CMPR_Max");
        TextBox txtBLR_CW_DOSAGE_BLR_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_DOSAGE_BLR_Max");
        TextBox txtBLR_CW_DOSAGE_MEJW_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_DOSAGE_MEJW_Max");
        TextBox txtBLR_CW_DOSAGE_MEPW_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_DOSAGE_MEPW_Max");
        TextBox txtBLR_CW_DOSAGE_AE_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_DOSAGE_AE_Max");
        TextBox txtBLR_CW_DOSAGE_CMPR_Max = (TextBox)FormView1.Row.Cells[0].FindControl("txtBLR_CW_DOSAGE_CMPR_Max");

        int m = BLL_Tec_ErLog.ErLog_BLR_CW_THRESHOLD_Update(1, UDFLib.ConvertIntegerToNull(ViewState["VESSELID"].ToString()) , UDFLib.ConvertDecimalToNull(txtBLR_CW_CHLORIDES_BLR.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_CHLORIDES_BLR_Max.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_CHLORIDES_MEJW.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_CHLORIDES_MEJW_Max.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_CHLORIDES_MEPW.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_CHLORIDES_MEPW_Max.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_CHLORIDES_AE.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_CHLORIDES_AE_Max.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_CHLORIDES_CMPR.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_CHLORIDES_CMPR_Max.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_ALKALINITY_BLR.Text),UDFLib.ConvertDecimalToNull( txtBLR_CW_ALKALINITY_BLR_Max.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_ALKALINITY_MEJW.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_ALKALINITY_MEJW_Max.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_ALKALINITY_MEPW.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_ALKALINITY_MEPW_Max.Text),UDFLib.ConvertDecimalToNull(txtBLR_CW_ALKALINITY_AE.Text),UDFLib.ConvertDecimalToNull( txtBLR_CW_ALKALINITY_AE_Max.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_ALKALINITY_CMPR.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_ALKALINITY_CMPR_Max.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_TALKALINITY_BLR.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_TALKALINITY_BLR_Max.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_TALKALINITY_MEJW.Text),UDFLib.ConvertDecimalToNull( txtBLR_CW_TALKALINITY_MEJW_Max.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_TALKALINITY_MEPW.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_TALKALINITY_MEPW_Max.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_TALKALINITY_AE.Text),UDFLib.ConvertDecimalToNull( txtBLR_CW_TALKALINITY_AE_Max.Text),UDFLib.ConvertDecimalToNull(txtBLR_CW_TALKALINITY_CMPR.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_TALKALINITY_CMPR_Max.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_PHOSPHATES_BLR.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_PHOSPHATES_BLR_Max.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_PHOSPHATES_MEJW.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_PHOSPHATES_MEJW_Max.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_PHOSPHATES_MEPW.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_PHOSPHATES_MEPW_Max.Text),UDFLib.ConvertDecimalToNull(txtBLR_CW_PHOSPHATES_AE.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_PHOSPHATES_AE_Max.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_PHOSPHATES_CMPR.Text),UDFLib.ConvertDecimalToNull(txtBLR_CW_PHOSPHATES_CMPR_Max.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_BLOWDOWN_BLR.Text),UDFLib.ConvertDecimalToNull(txtBLR_CW_BLOWDOWN_BLR_Max.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_BLOWDOWN_MEJW.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_BLOWDOWN_MEJW_Max.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_BLOWDOWN_MEPW.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_BLOWDOWN_MEPW_Max.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_BLOWDOWN_AE.Text),UDFLib.ConvertDecimalToNull( txtBLR_CW_BLOWDOWN_AE_Max.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_BLOWDOWN_CMPR.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_BLOWDOWN_CMPR_Max.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_NITRITES_BLR.Text),UDFLib.ConvertDecimalToNull(txtBLR_CW_NITRITES_BLR_Max.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_NITRITES_MEJW.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_NITRITES_MEJW_Max.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_NITRITES_MEPW.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_NITRITES_MEPW_Max.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_NITRITES_AE.Text),UDFLib.ConvertDecimalToNull( txtBLR_CW_NITRITES_AE_Max.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_NITRITES_CMPR.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_NITRITES_CMPR_Max.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_DOSAGE_BLR.Text),UDFLib.ConvertDecimalToNull(txtBLR_CW_DOSAGE_BLR_Max.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_DOSAGE_MEJW.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_DOSAGE_MEJW_Max.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_DOSAGE_MEPW.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_DOSAGE_MEPW.Text),
                UDFLib.ConvertDecimalToNull(txtBLR_CW_DOSAGE_AE.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_DOSAGE_AE_Max.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_DOSAGE_CMPR.Text), UDFLib.ConvertDecimalToNull(txtBLR_CW_DOSAGE_CMPR_Max.Text), 1);
    }

    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);
        if (objUA.Edit == 0)
        {
            btnSave.Enabled = false;
            FormView1.Enabled = false;  
        }

        if (objUA.View == 0)
        {
            Response.Redirect("~/crew/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {

        }
    }
}