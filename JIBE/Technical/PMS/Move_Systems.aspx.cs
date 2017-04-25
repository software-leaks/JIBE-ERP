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

public partial class Technical_PMS_Move_Systems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.Items.Clear();
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLVessel.Items.Insert(0, li);




        }
    }
    protected void btnMove_Click(object sender, EventArgs e)
    {
        try
        {
            lblmsg.Text = "";

            BLL_PMS_Library_Jobs objBLL = new BLL_PMS_Library_Jobs();
            if (lstToMove.SelectedValue != "" && lstParent.SelectedValue != "")
            {
                objBLL.TEC_MOVE_SYSTEM_SUBSYSTEM(lstParent.SelectedValue, lstToMove.SelectedValue);
                lblmsg.Text = "System is moved to Selected Parent's Sub-System Level";
            }
            else
            {
                lblmsg.Text = "Select Parent System and System To Move";
            }
            Bindsystems();


        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
        }


    }


    private void Bindsystems()
    {
        if (Int32.Parse(DDLVessel.SelectedValue) > 0)
        {
            BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
            DataSet ds = objBLLPurc.LibraryCatalogueSearch_PMS(null, null, "SP", null, 0, Int32.Parse(DDLVessel.SelectedValue), "", null, null, null, 1, 500, 1);


            lstParent.DataSource = ds.Tables[0];
            lstParent.DataBind();

            lstToMove.DataSource = ds.Tables[0];
            lstToMove.DataBind();
        }
    }
    protected void DDLVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindsystems();
    }
}