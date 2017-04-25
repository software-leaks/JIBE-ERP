using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PMS;
using System.Data;
using SMS.Business.Infrastructure;

public partial class Technical_PMS_Functional_Tree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFleetDLL();
            BindVesselDDL();

        }
    }

    public void BindItemTreeView()
    {
        BLL_PMS_Library_Jobs objBl = new BLL_PMS_Library_Jobs();

        DataSet dsRecords = objBl.Get_Functional_Tree_Data(UDFLib.ConvertToInteger(DDLVessel.SelectedValue));

        if (dsRecords.Tables.Count == 5)
        {
            foreach (DataRow drParent in dsRecords.Tables["function"].Rows)
            {
                TreeNode parentNode = new TreeNode(drParent["CODE"].ToString() + " - " + drParent["DESCRIPTION"].ToString(), drParent["CODE"].ToString());
                parentNode.ImageUrl = "../../images/noneimg.png";

                /*  DataRow[] drChildList_System = dsRecords.Tables["system"].Select("Functions=" + drParent["code"].ToString());

                  foreach (DataRow drChild1 in drChildList_System)
                  {
                      TreeNode Child_System = new TreeNode(drParent["CODE"].ToString() + "." + drChild1["System_code"].ToString() +" - "+ drChild1["System_Description"].ToString(), drChild1["System_code"].ToString());
                      Child_System.ImageUrl = "../../images/noneimg.png";

                      parentNode.ChildNodes.Add(Child_System);

                      DataRow[] drChildList_Location = dsRecords.Tables["systemlocation"].Select("system_code='" + drChild1["system_code"].ToString() + "'");

                      foreach (DataRow drChild2 in drChildList_Location)
                      {
                          TreeNode Child_Location = new TreeNode(drChild2["Location_Name"].ToString());
                          Child_Location.ImageUrl = "../../images/noneimg.png";
                          Child_System.ChildNodes.Add(Child_Location);

                          DataRow[] drChildList_SubSystem = dsRecords.Tables["subsystem"].Select("system_code='" + drChild1["system_code"].ToString() + "'");

                          foreach (DataRow drChild3 in drChildList_SubSystem)
                          {
                              TreeNode Child_SubSystem = new TreeNode(drChild3["Subsystem_Description"].ToString(), drChild3["ID"].ToString());
                              Child_SubSystem.ImageUrl = "../../images/noneimg.png";
                              Child_Location.ChildNodes.Add(Child_SubSystem);

                              DataRow[] drChildList_SubSystem_location = dsRecords.Tables["subsystemlocation"].Select("system_code='" + drChild1["system_code"].ToString() + "' and SubSystem_Code='" + drChild3["SubSystem_Code"].ToString() + "'");

                              foreach (DataRow drChild4 in drChildList_SubSystem_location)
                              {
                                  TreeNode Child_SubSystem_Location = new TreeNode(drChild4["Location_Name"].ToString(), drChild4["systemid"].ToString() + "," + drChild4["id"].ToString());
                                  Child_SubSystem_Location.ImageUrl = "../../images/noneimg.png";
                                  Child_SubSystem.ChildNodes.Add(Child_SubSystem_Location);
                              }
                          }

                      }



                  }*/

                tvItemList.Nodes.Add(parentNode);
                //parentNode.ExpandAll();
            }


        }




    }


    private void Function_Click(TreeNode parentNode)
    {
        BLL_PMS_Library_Jobs objBl = new BLL_PMS_Library_Jobs();

        DataSet dsRecords = objBl.Get_Functional_Tree_Data(UDFLib.ConvertToInteger(DDLVessel.SelectedValue));

        if (dsRecords.Tables.Count == 5)
        {

            DataRow[] drChildList_System = dsRecords.Tables["system"].Select("Functions=" + parentNode.Value.ToString());

            foreach (DataRow drChild1 in drChildList_System)
            {
                TreeNode Child_System = new TreeNode(parentNode.Value.ToString() + "." + drChild1["System_code"].ToString() + " - " + drChild1["System_Description"].ToString(), drChild1["System_code"].ToString()+",");
                Child_System.ImageUrl = "../../images/noneimg.png";

                parentNode.ChildNodes.Add(Child_System);

                DataRow[] drChildList_Location = dsRecords.Tables["systemlocation"].Select("system_code='" + drChild1["system_code"].ToString() + "'");

                foreach (DataRow drChild2 in drChildList_Location)
                {
                    TreeNode Child_Location = new TreeNode(parentNode.Value.ToString() + "." + drChild1["System_code"].ToString() + "." + drChild2["Location_code"].ToString() + " - " + drChild2["Location_Name"].ToString());
                    Child_Location.ImageUrl = "../../images/noneimg.png";
                    Child_System.ChildNodes.Add(Child_Location);

                    DataRow[] drChildList_SubSystem = dsRecords.Tables["subsystem"].Select("system_code='" + drChild1["system_code"].ToString() + "'");

                    foreach (DataRow drChild3 in drChildList_SubSystem)
                    {
                        TreeNode Child_SubSystem = new TreeNode(parentNode.Value.ToString() + "." + drChild1["System_code"].ToString() + "." + drChild2["Location_code"].ToString() + "." + drChild3["id"].ToString() + " - " + drChild3["Subsystem_Description"].ToString(), drChild1["System_code"].ToString() + "," + drChild3["ID"].ToString());
                        Child_SubSystem.ImageUrl = "../../images/noneimg.png";
                        Child_Location.ChildNodes.Add(Child_SubSystem);

                        DataRow[] drChildList_SubSystem_location = dsRecords.Tables["subsystemlocation"].Select("system_code='" + drChild1["system_code"].ToString() + "' and SubSystem_Code='" + drChild3["SubSystem_Code"].ToString() + "'");

                        foreach (DataRow drChild4 in drChildList_SubSystem_location)
                        {
                            TreeNode Child_SubSystem_Location = new TreeNode(parentNode.Value.ToString() + "." + drChild1["System_code"].ToString() + "." + drChild2["Location_code"].ToString() + "." + drChild3["id"].ToString() + "." + drChild4["Location_Code"].ToString() + " - " + drChild4["Location_Name"].ToString(), drChild4["systemid"].ToString() + "," + drChild4["id"].ToString());
                            Child_SubSystem_Location.ImageUrl = "../../images/noneimg.png";
                            Child_SubSystem.ChildNodes.Add(Child_SubSystem_Location);
                        }
                    }

                }



            }

            parentNode.Expand();



        }



    }

    protected void tvItemList_SelectedNodeChanged(object sender, EventArgs e)
    {
        BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
        gvJobs.DataSource = null;
        gvJobs.DataBind();


        if (((TreeView)(sender)).SelectedNode.Depth == 1 || ((TreeView)(sender)).SelectedNode.Depth == 3)
        {
            int rowcount = 0;
            DataSet ds = objJobs.LibraryJobSearch(UDFLib.ConvertIntegerToNull(tvItemList.SelectedValue.Split(',')[0]), UDFLib.ConvertIntegerToNull(tvItemList.SelectedValue.Split(',')[1]), UDFLib.ConvertToInteger(DDLVessel.SelectedValue), null, null, null
               , 1, "id"
               , 1, 1, 200, ref rowcount);


            gvJobs.DataSource = ds.Tables[0];
            gvJobs.DataBind();
        }

        if (((TreeView)(sender)).SelectedNode.Depth == 0)
        {
            Function_Click((sender as TreeView).SelectedNode);
        }

    }


    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselDDL();
    }


    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.Items.Clear();
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLFleet.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }

    public void BindVesselDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLVessel.Items.Insert(0, li);
            DDLVessel.SelectedIndex = 0;


            if (Request.QueryString["Vessel_ID"] != null)
            {
                DDLVessel.SelectedValue = Request.QueryString["Vessel_ID"].ToString();

            }



        }
        catch (Exception ex)
        {

        }
    }
    protected void DDLVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        tvItemList.Nodes.Clear();
        BindItemTreeView();
    }
}