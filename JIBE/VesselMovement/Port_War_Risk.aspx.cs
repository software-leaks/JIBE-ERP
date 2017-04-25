using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.VM;
using SMS.Business.Infrastructure;
using System.Collections;
public partial class VesselMovement_Port_War_Risk_ : System.Web.UI.Page
{
    BLL_VM_PortCall objBLLPort = new BLL_VM_PortCall();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_PortList();
        }
    }


    ArrayList arraylist1 = new ArrayList();
    ArrayList arraylist2 = new ArrayList();
    public void Load_PortList()
    {

        DataTable dt = objBLLPort.Get_WarRisk_Ports().Tables[0];
        DataTable dt1 = objBLLPort.Get_WarRisk_Ports().Tables[1];
        listPort.DataSource = dt;
        listPort.DataTextField = "Port_Name";
        listPort.DataValueField = "Port_ID";
        listPort.DataBind();
        listWar.DataSource = dt1;
        listWar.DataTextField = "Port_Name";
        listWar.DataValueField = "Port_ID";
        listWar.DataBind();
      

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string val = "ADD";
        if (listPort.SelectedIndex >= 0)
        {
            
            DataTable dt = null;
            foreach (ListItem li in listPort.Items)
            {
                if (li.Selected)
                {
                    if (!arraylist1.Contains(li.Value))
                    {
                        arraylist1.Add(li.Value);
                        int i=objBLLPort.Get_WarRisk_PortsUpdate(Convert.ToInt32(li.Value),val);
                    }
                }


            }
            Load_PortList();
         
        }
      
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string val = "SUB";
        if (listWar.SelectedIndex >= 0)
        {
           
            DataTable dt = null;
            foreach (ListItem li in listWar.Items)
            {
                if (li.Selected)
                {
                    if (!arraylist1.Contains(li.Value))
                    {
                        arraylist1.Add(li.Value);
                        int i = objBLLPort.Get_WarRisk_PortsUpdate(Convert.ToInt32(li.Value), val);
                    }
                }


            }
            Load_PortList();
        }
        else
        {

        }
    }
}