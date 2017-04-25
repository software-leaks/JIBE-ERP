using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Operation;
using SMS.Business.Technical;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class ERLogMainEngineThresHold : System.Web.UI.Page
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
        DataTable dt = BLL_Tec_ErLog.ErLog_ME_00_Get(int.Parse(lblLogId.Text),int.Parse(ViewState["VESSELID"].ToString()));
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
          
            Repeater fvDetails = (Repeater)FormView1.Row.Cells[0].FindControl("rpEngine2");
            if (fvDetails != null)
            {
                fvDetails.DataSource = BLL_Tec_ErLog.ErLog_ME_02_EDIT(int.Parse(Request.QueryString["VESSELID"].ToString()));
                fvDetails.DataBind();
            }           
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Repeater fvDetails = (Repeater)FormView1.Row.Cells[0].FindControl("rpEngine2");     

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
            TextBox tx20 = (TextBox)ritem.FindControl("txt20");
            TextBox tx21 = (TextBox)ritem.FindControl("txt21");
            TextBox tx22 = (TextBox)ritem.FindControl("txt22");
            TextBox tx23 = (TextBox)ritem.FindControl("txt23");
            TextBox tx24 = (TextBox)ritem.FindControl("txt24");
            TextBox tx25 = (TextBox)ritem.FindControl("txt25");
            TextBox tx26 = (TextBox)ritem.FindControl("txt26");
            TextBox tx27 = (TextBox)ritem.FindControl("txt27");
            TextBox tx28 = (TextBox)ritem.FindControl("txt28");
            TextBox tx29 = (TextBox)ritem.FindControl("txt29");
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
            TextBox tx020 = (TextBox)ritem.FindControl("txt020");
            TextBox tx021 = (TextBox)ritem.FindControl("txt021");
            TextBox tx022 = (TextBox)ritem.FindControl("txt022");
            TextBox tx023 = (TextBox)ritem.FindControl("txt023");
            TextBox tx024 = (TextBox)ritem.FindControl("txt024");
            TextBox tx025 = (TextBox)ritem.FindControl("txt025");
            TextBox tx026 = (TextBox)ritem.FindControl("txt026");
            TextBox tx027 = (TextBox)ritem.FindControl("txt027");
            TextBox tx028 = (TextBox)ritem.FindControl("txt028");
            TextBox tx029 = (TextBox)ritem.FindControl("txt029");

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
            if (valstatus)
            {

                int j = BLL_Tec_ErLog.ErLog_ME_02_THRESHOLD_Update(UDFLib.ConvertIntegerToNull(lblid.Text), UDFLib.ConvertIntegerToNull(ViewState["VESSELID"].ToString()), UDFLib.ConvertDecimalToNull(tx1.Text), UDFLib.ConvertDecimalToNull(tx01.Text),
                    UDFLib.ConvertDecimalToNull(tx2.Text), UDFLib.ConvertDecimalToNull(tx02.Text), UDFLib.ConvertDecimalToNull(tx3.Text), UDFLib.ConvertDecimalToNull(tx03.Text), UDFLib.ConvertDecimalToNull(tx4.Text),
                     UDFLib.ConvertDecimalToNull(tx04.Text), UDFLib.ConvertDecimalToNull(tx5.Text), UDFLib.ConvertDecimalToNull(tx05.Text), UDFLib.ConvertDecimalToNull(tx6.Text), UDFLib.ConvertDecimalToNull(tx06.Text),
                    UDFLib.ConvertDecimalToNull(tx7.Text), UDFLib.ConvertDecimalToNull(tx07.Text), UDFLib.ConvertDecimalToNull(tx8.Text), UDFLib.ConvertDecimalToNull(tx08.Text), UDFLib.ConvertDecimalToNull(tx9.Text),
                    UDFLib.ConvertDecimalToNull(tx09.Text), UDFLib.ConvertDecimalToNull(tx10.Text), UDFLib.ConvertDecimalToNull(tx010.Text), UDFLib.ConvertDecimalToNull(tx11.Text), UDFLib.ConvertDecimalToNull(tx011.Text),
                    UDFLib.ConvertDecimalToNull(tx12.Text), UDFLib.ConvertDecimalToNull(tx012.Text), UDFLib.ConvertDecimalToNull(tx13.Text), UDFLib.ConvertDecimalToNull(tx013.Text), UDFLib.ConvertDecimalToNull(tx14.Text),
                    UDFLib.ConvertDecimalToNull(tx014.Text), UDFLib.ConvertDecimalToNull(tx15.Text), UDFLib.ConvertDecimalToNull(tx015.Text), UDFLib.ConvertDecimalToNull(tx16.Text), UDFLib.ConvertDecimalToNull(tx016.Text),
                    UDFLib.ConvertDecimalToNull(tx17.Text), UDFLib.ConvertDecimalToNull(tx017.Text), UDFLib.ConvertDecimalToNull(tx18.Text), UDFLib.ConvertDecimalToNull(tx018.Text), UDFLib.ConvertDecimalToNull(tx19.Text),
                    UDFLib.ConvertDecimalToNull(tx019.Text), UDFLib.ConvertDecimalToNull(tx20.Text), UDFLib.ConvertDecimalToNull(tx020.Text), UDFLib.ConvertDecimalToNull(tx21.Text), UDFLib.ConvertDecimalToNull(tx021.Text),
                    UDFLib.ConvertDecimalToNull(tx22.Text), UDFLib.ConvertDecimalToNull(tx022.Text), UDFLib.ConvertDecimalToNull(tx23.Text), UDFLib.ConvertDecimalToNull(tx023.Text), UDFLib.ConvertDecimalToNull(tx24.Text),
                    UDFLib.ConvertDecimalToNull(tx024.Text), UDFLib.ConvertDecimalToNull(tx25.Text), UDFLib.ConvertDecimalToNull(tx025.Text), UDFLib.ConvertDecimalToNull(tx26.Text), UDFLib.ConvertDecimalToNull(tx026.Text),
                    UDFLib.ConvertDecimalToNull(tx27.Text), UDFLib.ConvertDecimalToNull(tx027.Text), UDFLib.ConvertDecimalToNull(tx28.Text), UDFLib.ConvertDecimalToNull(tx028.Text), UDFLib.ConvertDecimalToNull(tx29.Text),
                    UDFLib.ConvertDecimalToNull(tx029.Text), Convert.ToInt32(Session["USERID"]));
                string js = "alert('Changes are updated ');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
            }
            else
            {
                string js = "alert('Please check your data');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
            }
        }
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