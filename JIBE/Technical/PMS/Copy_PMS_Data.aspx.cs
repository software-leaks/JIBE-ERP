using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.PMS;

public partial class Technical_Copy_PMS_Data : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));


            lstCopyFromVessel.DataSource = dtVessel;
            lstCopyFromVessel.DataBind();

            lstCopyToVessel.DataSource = dtVessel;
            lstCopyToVessel.DataBind();




        }
    }
    protected void btnCopyData_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstCopyFromVessel.SelectedIndex != -1 && lstCopyToVessel.SelectedIndex != -1)
            {

                BLL_PMS_Library_Jobs objCopy = new BLL_PMS_Library_Jobs();
                int sts = objCopy.Upd_Copy_PMS_Data(Convert.ToInt32(lstCopyFromVessel.SelectedValue), Convert.ToInt32(lstCopyToVessel.SelectedValue), Convert.ToInt32(Session["USERID"].ToString()), 0);
                if (sts == 1)
                    lblmsg.Text = "PMS data copied successfully.";

            }
            else
                lblmsg.Text = "Please select vessel";



        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }


    }


   
}