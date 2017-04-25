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

public partial class ERLogGeneratorEnginesThreshold : System.Web.UI.Page
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
            int rowcount = 0;
            Repeater fvMem = (Repeater)FormView1.Row.Cells[0].FindControl("rpTrainingDetails");
            if (fvMem != null)
            {
                fvMem.DataSource = BLL_Tec_ErLog.ERLOG_GENERATOR_ENGINES_EDIT(int.Parse(ViewState["VESSELID"].ToString()) );
                fvMem.DataBind();
            }
           
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Repeater fvDetails = (Repeater)FormView1.Row.Cells[0].FindControl("rpTrainingDetails");

        foreach (RepeaterItem ritem in fvDetails.Items)
        {
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
            TextBox tx30 = (TextBox)ritem.FindControl("txt30");
            TextBox tx31 = (TextBox)ritem.FindControl("txt31");
            TextBox tx32 = (TextBox)ritem.FindControl("txt32");
            TextBox tx33 = (TextBox)ritem.FindControl("txt33");
            TextBox tx34 = (TextBox)ritem.FindControl("txt34");
            TextBox tx35 = (TextBox)ritem.FindControl("txt35");
            TextBox tx36= (TextBox)ritem.FindControl("txt36");
            TextBox tx37 = (TextBox)ritem.FindControl("txt37");
            TextBox tx38= (TextBox)ritem.FindControl("txt38");
            TextBox tx39 = (TextBox)ritem.FindControl("txt39");
            TextBox tx40 = (TextBox)ritem.FindControl("txt40");

            bool valstatus = true;
            if ((UDFLib.ConvertDecimalToNull(tx1.Text) != null) && (UDFLib.ConvertDecimalToNull(tx21.Text) != null) && (decimal.Parse(tx1.Text) > decimal.Parse(tx21.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx2.Text) != null) && (UDFLib.ConvertDecimalToNull(tx22.Text) != null) && (decimal.Parse(tx2.Text) > decimal.Parse(tx22.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx3.Text) != null) && (UDFLib.ConvertDecimalToNull(tx23.Text) != null) && (decimal.Parse(tx3.Text) > decimal.Parse(tx23.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx4.Text) != null) && (UDFLib.ConvertDecimalToNull(tx24.Text) != null) && (decimal.Parse(tx4.Text) > decimal.Parse(tx24.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx5.Text) != null) && (UDFLib.ConvertDecimalToNull(tx25.Text) != null) && (decimal.Parse(tx5.Text) > decimal.Parse(tx25.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx6.Text) != null) && (UDFLib.ConvertDecimalToNull(tx26.Text) != null) && (decimal.Parse(tx6.Text) > decimal.Parse(tx26.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx7.Text) != null) && (UDFLib.ConvertDecimalToNull(tx27.Text) != null) && (decimal.Parse(tx7.Text) > decimal.Parse(tx27.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx8.Text) != null) && (UDFLib.ConvertDecimalToNull(tx28.Text) != null) && (decimal.Parse(tx8.Text) > decimal.Parse(tx28.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx9.Text) != null) && (UDFLib.ConvertDecimalToNull(tx29.Text) != null) && (decimal.Parse(tx9.Text) > decimal.Parse(tx29.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx10.Text) != null) && (UDFLib.ConvertDecimalToNull(tx30.Text) != null) && (decimal.Parse(tx10.Text) > decimal.Parse(tx30.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx11.Text) != null) && (UDFLib.ConvertDecimalToNull(tx31.Text) != null) && (decimal.Parse(tx11.Text) > decimal.Parse(tx31.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx12.Text) != null) && (UDFLib.ConvertDecimalToNull(tx32.Text) != null) && (decimal.Parse(tx12.Text) > decimal.Parse(tx32.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx13.Text) != null) && (UDFLib.ConvertDecimalToNull(tx33.Text) != null) && (decimal.Parse(tx13.Text) > decimal.Parse(tx33.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx14.Text) != null) && (UDFLib.ConvertDecimalToNull(tx34.Text) != null) && (decimal.Parse(tx14.Text) > decimal.Parse(tx34.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx15.Text) != null) && (UDFLib.ConvertDecimalToNull(tx35.Text) != null) && (decimal.Parse(tx15.Text) > decimal.Parse(tx35.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx16.Text) != null) && (UDFLib.ConvertDecimalToNull(tx36.Text) != null) && (decimal.Parse(tx16.Text) > decimal.Parse(tx36.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx17.Text) != null) && (UDFLib.ConvertDecimalToNull(tx37.Text) != null) && (decimal.Parse(tx17.Text) > decimal.Parse(tx37.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx18.Text) != null) && (UDFLib.ConvertDecimalToNull(tx38.Text) != null) && (decimal.Parse(tx18.Text) > decimal.Parse(tx38.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx19.Text) != null) && (UDFLib.ConvertDecimalToNull(tx39.Text) != null) && (decimal.Parse(tx19.Text) > decimal.Parse(tx39.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx20.Text) != null) && (UDFLib.ConvertDecimalToNull(tx40.Text) != null) && (decimal.Parse(tx20.Text) > decimal.Parse(tx40.Text))) valstatus = false;
           
            if (valstatus)
            {

                int i = BLL_Tec_ErLog.ErLog_GENERATOR_ENGINES_THRESHOLD_Update(UDFLib.ConvertIntegerToNull(lblid.Text), UDFLib.ConvertIntegerToNull(ViewState["VESSELID"].ToString()), 1, UDFLib.ConvertIntegerToNull(tx1.Text),
                    UDFLib.ConvertDecimalToNull(tx2.Text), UDFLib.ConvertDecimalToNull(tx3.Text), UDFLib.ConvertDecimalToNull(tx4.Text), UDFLib.ConvertDecimalToNull(tx5.Text), UDFLib.ConvertDecimalToNull(tx6.Text),
                    UDFLib.ConvertDecimalToNull(tx7.Text), UDFLib.ConvertDecimalToNull(tx8.Text), UDFLib.ConvertDecimalToNull(tx9.Text), UDFLib.ConvertDecimalToNull(tx10.Text), UDFLib.ConvertDecimalToNull(tx11.Text),
                    UDFLib.ConvertDecimalToNull(tx12.Text), UDFLib.ConvertDecimalToNull(tx13.Text), UDFLib.ConvertDecimalToNull(tx14.Text), UDFLib.ConvertDecimalToNull(tx15.Text), UDFLib.ConvertDecimalToNull(tx16.Text),
                    UDFLib.ConvertDecimalToNull(tx17.Text), UDFLib.ConvertDecimalToNull(tx18.Text), UDFLib.ConvertDecimalToNull(tx19.Text), UDFLib.ConvertDecimalToNull(tx20.Text), UDFLib.ConvertDecimalToNull(tx21.Text),
                    UDFLib.ConvertDecimalToNull(tx22.Text), UDFLib.ConvertDecimalToNull(tx23.Text), UDFLib.ConvertDecimalToNull(tx24.Text), UDFLib.ConvertDecimalToNull(tx25.Text), UDFLib.ConvertDecimalToNull(tx26.Text),
                    UDFLib.ConvertDecimalToNull(tx27.Text), UDFLib.ConvertDecimalToNull(tx28.Text), UDFLib.ConvertDecimalToNull(tx29.Text), UDFLib.ConvertDecimalToNull(tx30.Text), UDFLib.ConvertDecimalToNull(tx31.Text),
                    UDFLib.ConvertDecimalToNull(tx32.Text), UDFLib.ConvertDecimalToNull(tx33.Text), UDFLib.ConvertDecimalToNull(tx34.Text), UDFLib.ConvertDecimalToNull(tx35.Text), UDFLib.ConvertDecimalToNull(tx36.Text),
                    UDFLib.ConvertDecimalToNull(tx37.Text), UDFLib.ConvertDecimalToNull(tx38.Text), UDFLib.ConvertDecimalToNull(tx39.Text), UDFLib.ConvertDecimalToNull(tx40.Text), Convert.ToInt32(Session["USERID"]));
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