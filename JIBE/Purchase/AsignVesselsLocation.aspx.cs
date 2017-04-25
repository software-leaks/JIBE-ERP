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
using Telerik.Web.UI;
using SMS.Business.PURC ;
using SMS.Properties;

public partial class AsignVesselsLocation : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //itemObject.Constring = _constring;
            bindVesselDDL();
            bindAssignAndUnAssignCatalog();
            
            bindassignlocation();
            bindUnsignLocation();
           
        }
        else if (Request.Params.Get("__EVENTTARGET").ToString() == "1")
        {
            testfunct();
        }
       
    }

    private void bindVesselDDL()
    {

        try
        {
            //using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            //{

            //    DataTable dtvessel = new DataTable();
            //    dtvessel = objTechService.SelectFleet();
            //    fleet.DataTextField = "Name";
            //    fleet.DataValueField = "Code";
            //    fleet.AppendDataBoundItems = true;
            //    fleet.DataSource = dtvessel;
            //    fleet.DataBind();

            //      
            //}


            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                DataSet FleetDS = objTechService.SelectFleet();
                fleet.DataSource = FleetDS.Tables[0];
                fleet.DataTextField = "Name";
                fleet.DataValueField = "Code";
                fleet.DataBind();
                 
            }



        }
        catch(Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally 
        {
        
        }

        //PMSClass.clsVessels vessel = new PMSClass.clsVessels();
        //vessel.Constring = _constring;
        //DataTable dtvessel = new DataTable();
        //dtvessel = vessel.GetFleet();
        //fleet.DataTextField = "Fleet";
        //fleet.DataValueField = "Fleet";
        //fleet.AppendDataBoundItems = true;
        //fleet.DataSource = dtvessel;
        //fleet.DataBind();
           
    }

    private bool testfunct()
    {
        //string a;
        //a = Hidden1.Value; 
        return true;
    }


    private void CreateTable()
    {


        try
        {
            DataTable dtVLC = new DataTable();
            DataColumn dc = new DataColumn("Vessel_code", typeof(string));
            dtVLC.Columns.Add(dc);
            DataColumn dc1 = new DataColumn("Location_code", typeof(string));
            dtVLC.Columns.Add(dc1);
            DataColumn dc2 = new DataColumn("System_code", typeof(string));
            dtVLC.Columns.Add(dc2);
            dtVLC.AcceptChanges();
            Session["dtVLC"] = dtVLC;


        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
       

    }

    private void bindUnsignLocation()
    {
        try
        {
            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                DataTable dtvessel = new DataTable();
                dtvessel = objTechService.UnassignLocation();
                Session["dtUnALoction"] = dtvessel;
                  
            }


        }
        catch (Exception ex)
        {

            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
    }

    private void bindassignlocation()
    {

        try
        {
            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                DataTable dtvessel = new DataTable();
                dtvessel = objTechService.assignLocation();
                Session["dtALoction"] = dtvessel;
                  
            }

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);

        }
        finally
        {

        }
    }

   
    private void bindVessel()
    {

        try
        {
            
            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
               
                DataTable dtvessel = new DataTable();
                dtvessel = objTechService.GetVesselName();
                grdVessel.DataSource = dtvessel;
                grdVessel.DataBind();

                   
            }

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        { 
        
        }

        //PMSClass.clsVessels vessel = new PMSClass.clsVessels();
        //vessel.Constring = _constring;
        //DataTable dtvessel = new DataTable();
        //dtvessel = vessel.GetVesselName();
        //grdVessel.DataSource = dtvessel;
        //grdVessel.DataBind();
    }

    private void bindAssignAndUnAssignCatalog()
    {


        try
        {
            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                
                DataTable dtItems = new DataTable();
                dtItems = objTechService.AssignCatalogs();
                Session["dtACatalog"] = dtItems;
                dtItems = objTechService.UnassignCatalogs();
                Session["dtUnACatalog"] = dtItems;
                  

            }
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        { 
        
        }
        
        //DataTable dtItems = new DataTable();
        //PMSClass.VLCatalog objVLC = new PMSClass.VLCatalog();
        //objVLC.Constring = _constring;
        //dtItems = objVLC.assignCatalogs();
        //Session["dtACatalog"] = dtItems;
        //dtItems = objVLC.UnassignCatalogs();
        //Session["dtUnACatalog"] = dtItems;

    }

  
    protected void onSelect(object source, CommandEventArgs e)
    {

        try
        {

            ResponseHelper.Redirect("Items.aspx?scode=" + e.CommandArgument.ToString(), "Blank", "");

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
      

    }

    protected void onVeselSelect(object source, CommandEventArgs e)
    {
        try
        {
            ResponseHelper.Redirect("~/Infrastructure/Vessel/VesselDetails.aspx?scode=" + e.CommandArgument.ToString(), "Blank", "");

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

        
    }


    protected void grdunAssignLocation_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        try
        {

            bindUnsignLocation();

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
      
    }


    protected void btnsave_Click(object sender, ImageClickEventArgs e)
    {
        //DataTable dt = new DataTable();
        //dt = (DataTable)Session["dtACatalog"];
        //PMSClass.VLCatalog cVlc = new PMSClass.VLCatalog();
        //cVlc.Constring = _constring;
        //cVlc.User = Session["user"].ToString();
        //foreach (DataRow dr in dt.Rows)
        //{
        //    cVlc.Vessel_code = dr[0].ToString();
        //    cVlc.Location = dr[1].ToString();
        //    cVlc.Systemcode = dr[2].ToString();
        //    int n = cVlc.assignLocation();

        //}
    }

 
    protected void ImgAssignLocation_Click(object sender, ImageClickEventArgs e)
    {


        try
        {
            string cell = "1";

            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                
                foreach (GridDataItem selectedItem in grdVessel.SelectedItems)
                {
                    GridDataItem row = (GridDataItem)selectedItem;// to access the selected row  
                    cell = row["Vessel_code"].Text;
                    foreach (GridDataItem item in grdunAssignLocation.SelectedItems)
                    {
                        AssignVesselData objAssignVesselDataDO = new AssignVesselData(); 

                        GridDataItem rowItem = (GridDataItem)item;// to access the selected row  
                        string cellItem = rowItem["Code"].Text;
                        objAssignVesselDataDO.Vessel_code = cell;
                        objAssignVesselDataDO.LocationCode = cellItem;
                        objAssignVesselDataDO.Systemcode = "0";
                        objAssignVesselDataDO.LocationComments = "";
                        objAssignVesselDataDO.CurrentUser = Session["user"].ToString();
                        int a = objTechService.VLassignLocation(objAssignVesselDataDO);

                    }
                }
                bindassignlocation1(cell);
                bindUnsignLocationVessel(cell);
               
                  
            }

           // AddToSyncronizer.AddToSyncroNizerData("PURC_Dtl_Vessel_Locations");
            

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        { 
        }
       
    }

    protected void ImgAssignCatalog_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string cell = "1";
            string cell1 = "1";

            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                
                foreach (GridDataItem selectedItem1 in grdVessel.SelectedItems)
                {
                    GridDataItem row1 = (GridDataItem)selectedItem1;// to access the selected row  
                    cell1 = row1["Vessel_code"].Text;

                    foreach (GridDataItem selectedItem in grdAssignLocation.SelectedItems)
                    {
                        GridDataItem row = (GridDataItem)selectedItem;// to access the selected row  
                        cell = row["code"].Text;
                        foreach (GridDataItem item in grdunAsigncatalog.SelectedItems)
                        {
                            AssignVesselData objAssignVesselDataDO = new AssignVesselData(); 

                            GridDataItem rowItem = (GridDataItem)item;// to access the selected row  
                            string cellItem = rowItem["system_Code"].Text;
                            objAssignVesselDataDO.Vessel_code = cell1;
                            objAssignVesselDataDO.LocationCode = cell;
                            objAssignVesselDataDO.Systemcode = cellItem;
                            objAssignVesselDataDO.LocationComments = "";
                            objAssignVesselDataDO.CurrentUser = Session["user"].ToString();
                            int a = objTechService.VLassignLocation(objAssignVesselDataDO);
                        }

                    }
                }
                bindCatalogLocation(cell, cell1);
                 
            }

            //AddToSyncronizer.AddToSyncroNizerData("PURC_Dtl_Vessel_Locations");

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);

        }
        finally
        { 
        
        }
     
    }



    protected void ImgUnassignLocation_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string cell = "1";
            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                foreach (GridDataItem selectedItem in grdVessel.SelectedItems)
                {
                    GridDataItem row = (GridDataItem)selectedItem;// to access the selected row  
                    cell = row["Vessel_code"].Text;
                    foreach (GridDataItem item in grdAssignLocation.SelectedItems)
                    {
                        GridDataItem rowItem = (GridDataItem)item;// to access the selected row  
                        string cellItem = rowItem["Code"].Text;

                        int a = objTechService.VDeleteLocation(cell, cellItem);
                    }

                }
                bindassignlocation1(cell);
                bindUnsignLocationVessel(cell);
                  

            }

            //AddToSyncronizer.AddToSyncroNizerData("PURC_Dtl_Vessel_Locations");

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        { 
        
        }
     
    }
  
    protected void ImgUnassignCatalog_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string cell1 = "1";
            string cell = "1";
            
            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                foreach (GridDataItem selectedItem1 in grdVessel.SelectedItems)
                {
                    GridDataItem row1 = (GridDataItem)selectedItem1;// to access the selected row  
                    cell1 = row1["Vessel_code"].Text;

                    foreach (GridDataItem selectedItem in grdAssignLocation.SelectedItems)
                    {
                        GridDataItem row = (GridDataItem)selectedItem;// to access the selected row  
                        cell = row["code"].Text;
                        foreach (GridDataItem item in grdAsigncatalog.SelectedItems)
                        {
                            GridDataItem rowItem = (GridDataItem)item;// to access the selected row  
                            string cellItem = rowItem["system_Code"].Text;

                            int a = objTechService.VDeletecatelog(cell1, cell, cellItem);
                        }
                    }
                }
                bindCatalogLocation(cell, cell1);
                 
            }

            //AddToSyncronizer.AddToSyncroNizerData("PURC_Dtl_Vessel_Locations");

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        { 
        
        }
 
    }


  
    protected void grdVessel_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {

            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {

                DataTable dtvessel = new DataTable();
                dtvessel = objTechService.GetVesselName();

                if (fleet.SelectedIndex == 0)
                {
                    grdVessel.DataSource = dtvessel;
                }
                else
                {
                    dtvessel.DefaultView.RowFilter = "Fleet like '" + fleet.SelectedItem.Text + "%' ";
                    grdVessel.DataSource = dtvessel.DefaultView;
                }
                grdVessel.DataSource = dtvessel;

                 

            }

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
        
        
        
        //PMSClass.clsVessels vessel = new PMSClass.clsVessels();
        //vessel.Constring = _constring;
        //DataTable dtvessel = new DataTable();
        //dtvessel = vessel.GetVesselName();
        //if (fleet.SelectedIndex == 0)
        //{
        //    grdVessel.DataSource = dtvessel;
        //}
        //else
        //{
        //    dtvessel.DefaultView.RowFilter = "Fleet like '" + fleet.SelectedItem.Text + "%' ";
        //    grdVessel.DataSource = dtvessel.DefaultView;
        //}
        //grdVessel.DataSource = dtvessel;
    }

    protected void grdAsigncatalog_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["dtACatalog"];
            grdAsigncatalog.DataSource = dt;
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

    }

    //protected void rgdSubSystem_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string SubCatalogId;
    //        lblMsg.Visible = false;
    //        lblErrorMsg.Visible = false;
    //        lblMainFormError.Text = "";


    //        if (Session["SUBSYSTEM_CODE"] != null)
    //        {
    //            SelectRowSubcatalog(Session["SUBSYSTEM_CODE"].ToString());
    //        }

    //        using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
    //        {
    //            foreach (GridDataItem dataItem in rgdSubSystem.MasterTableView.Items)
    //            {
    //                if (dataItem.Selected)
    //                {

    //                    SubCatalogId = dataItem["SUBSYSTEM_CODE"].Text;
    //                    Session["SUBSYSTEM_CODE"] = dataItem["SUBSYSTEM_CODE"].Text;
    //                    txtDivItemSubSytemCode.Text = SubCatalogId;
    //                    DataTable dtItems = new DataTable();
    //                    dtItems = objTechService.SelectItem(Request.QueryString["scode"].ToString(), Session["SUBSYSTEM_CODE"].ToString());
    //                    rgdItems.DataSource = dtItems;
    //                    rgdItems.DataBind();
    //                }
    //            }
    //             
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
    //        HiddenSubCateFlag.Value = "";
    //    }
    //    finally
    //    {
    //        HiddenSubCateFlag.Value = "";
    //    }

    //}


    protected void grdVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        string vessel_code = "";

        using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
        {
            foreach (GridDataItem dataItem in grdVessel.MasterTableView.Items)
            {
                if (dataItem.Selected)
                {
                    vessel_code = dataItem["Vessel_Code"].Text;

                bindassignlocation1((vessel_code));
                bindUnsignLocationVessel(vessel_code);
                bindcatalogs(vessel_code);
                
                }
            }
        }
    }

    protected void CheckedChanged(object sender, System.EventArgs e)
    {

        try
        {
            bool ststus;
            string vessel_code = "1";
            ststus = (sender as CheckBox).Checked;


            foreach (GridDataItem dataItem in grdVessel.MasterTableView.Items)
            {
                (dataItem.FindControl("CheckBox1") as CheckBox).Checked = false;
            }
            (sender as CheckBox).Checked = ststus;
            ((sender as CheckBox).Parent.Parent as GridItem).Selected = (sender as CheckBox).Checked;
            foreach (GridDataItem dataItem in grdVessel.MasterTableView.Items)
            {
                if ((dataItem.FindControl("CheckBox1") as CheckBox).Checked == true)
                {
                    vessel_code = dataItem["vessel_code"].Text;
                }
            }


            bindassignlocation1((vessel_code));
            bindUnsignLocationVessel(vessel_code);
            bindcatalogs(vessel_code);
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }


    }

    protected void checkchangAll(object sender, System.EventArgs e)
    {
        try
        {
            bool ststus;
            ststus = (sender as CheckBox).Checked;
            if (ststus)
            {
                ImgAssignCatalog.Enabled = false;
            }
            else
            {
                ImgAssignCatalog.Enabled = true;
            }

            foreach (GridDataItem dataItem in grdAssignLocation.MasterTableView.Items)
            {
                (dataItem.FindControl("ChecLocation") as CheckBox).Checked = ststus;
                dataItem.Selected = ststus;
            }
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
    
    }

    protected void RowCheckedChanged(object sender, System.EventArgs e)
    {
        try
        {

            bool ststus;
            string Code = "1";
            string vessel_code = "1";
            ststus = (sender as CheckBox).Checked;

            foreach (GridDataItem dataItem in grdAssignLocation.MasterTableView.Items)
            {
                (dataItem.FindControl("ChecLocation") as CheckBox).Checked = false;
                dataItem.Selected = false;
            }
            (sender as CheckBox).Checked = ststus;
            ((sender as CheckBox).Parent.Parent as GridItem).Selected = (sender as CheckBox).Checked;
            foreach (GridDataItem dataItem in grdAssignLocation.MasterTableView.Items)
            {
                if ((dataItem.FindControl("ChecLocation") as CheckBox).Checked == true)
                {
                    Code = dataItem["Code"].Text;
                }
            }
            foreach (GridDataItem selectedItem in grdVessel.SelectedItems)
            {
                GridDataItem row1 = (GridDataItem)selectedItem;// to access the selected row  
                vessel_code = row1["Vessel_code"].Text;
            }
            bindCatalogLocation(Code, vessel_code);
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
    }

    private void bindCatalogLocation(string Code,string vesel_code)
    {

    try
    {
        using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
        {
            DataTable dtItems = new DataTable();
            dtItems = objTechService.VLassignCatalogs(vesel_code, Code);
            Session["dtACatalog"] = dtItems;
            grdAsigncatalog.Rebind();
            dtItems = objTechService.VLUnassignCatalogs(vesel_code, Code);
            Session["dtUnACatalog"] = dtItems;
            grdunAsigncatalog.Rebind();
              
        }


    }
    catch (Exception ex)
    {

        //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
    }
    finally
    {

    }
    
    
    //DataTable dtItems = new DataTable();
    //PMSClass.VLCatalog objVLC = new PMSClass.VLCatalog();
    //objVLC.Constring = _constring;
    //dtItems = objVLC.VLassignCatalogs(vesel_code, Code);
    //Session["dtACatalog"] = dtItems;
    //grdAsigncatalog.Rebind();
    //dtItems = objVLC.VLUnassignCatalogs(vesel_code, Code);
    //Session["dtUnACatalog"] = dtItems;
    //grdunAsigncatalog.Rebind();
}

    private void bindcatalogs(string p)
    {

    try
    {
        using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
        {
            DataTable dtItems = new DataTable();
            dtItems = objTechService.VassignCatalogs(p);
            Session["dtACatalog"] = dtItems;
            grdAsigncatalog.Rebind();
            dtItems = objTechService.VUnassignCatalogs(p);
            Session["dtUnACatalog"] = dtItems;
            grdunAsigncatalog.Rebind();

              
        }

    }
    catch (Exception ex)
    {

        //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
    }
    finally
    {

    } 
    
    //DataTable dtItems = new DataTable();
    //PMSClass.VLCatalog objVLC = new PMSClass.VLCatalog();
    //objVLC.Constring = _constring;
    //dtItems = objVLC.VassignCatalogs(p);    
    //Session["dtACatalog"] = dtItems;
    //grdAsigncatalog.Rebind();
    //dtItems = objVLC.VUnassignCatalogs(p);
    //Session["dtUnACatalog"] = dtItems;
    //grdunAsigncatalog.Rebind();
}

    private void bindassignlocation1(string p)
    {
    try
    {
        using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
        {

            DataTable dtvessel = new DataTable();
            dtvessel = objTechService.VassignLocation(p);
            Session["dtALoction"] = dtvessel;
            grdAssignLocation.Rebind();

              
        }


    }
    catch (Exception ex)
    {
        //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);

    }
    finally
    {

    }
    
    
    //PMSClass.Location vesselLo = new PMSClass.Location();
    //vesselLo.Constring = _constring;
    //DataTable dtvessel = new DataTable();
    //dtvessel = vesselLo.VassignLocation(p);    
    //Session["dtALoction"] = dtvessel;
    //grdAssignLocation.Rebind();
   
}

    private void bindUnsignLocationVessel(string p)
    {
  
    try
        {
            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
               DataTable dtvessel = new DataTable();
                dtvessel = objTechService.VUnassignLocation(p);
                Session["dtUnALoction"] = dtvessel;
                grdunAssignLocation.Rebind();
                  
            }


        }
        catch (Exception ex)
        {

            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
    
    //PMSClass.Location vesselLo = new PMSClass.Location();
    //vesselLo.Constring = _constring;
    //DataTable dtvessel = new DataTable();
    //dtvessel = vesselLo.VUnassignLocation(p);
    //Session["dtUnALoction"] = dtvessel;
    //grdunAssignLocation.Rebind(); 
}

 protected void grdAssignLocation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        try
        {

            DataTable dt = new DataTable();
            dt = (DataTable)Session["dtALoction"];
            grdAssignLocation.DataSource = dt;

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
       
    }

    protected void grdunAssignLocation_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["dtUnALoction"];
            grdunAssignLocation.DataSource = dt;


        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
      
    }

    protected void grdunAsigncatalog_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {

        try
        {

            DataTable dt = new DataTable();
            dt = (DataTable)Session["dtUnACatalog"];
            grdunAsigncatalog.DataSource = dt;

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
       
    }

protected void fleet_SelectedIndexChanged(object sender, EventArgs e)
{
    try
    {

        grdVessel.Rebind(); 

    }
    catch (Exception ex)
    {
        //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
    }
    finally
    {

    }
    
}

protected void grdVessel_ItemDataBound(object sender, GridItemEventArgs e)
{

    if (e.Item is GridDataItem)
    {
        foreach (TableCell cell in e.Item.Cells)
        {
            cell.ToolTip = cell.Text != "&nbsp;" ? cell.Text : null;

        }
    }

}

protected void grdAssignLocation_ItemDataBound(object sender, GridItemEventArgs e)
{
    if (e.Item is GridDataItem)
    {
        foreach (TableCell cell in e.Item.Cells)
        {
            cell.ToolTip = cell.Text != "&nbsp;" ? cell.Text : null;

        }
    }
}
protected void grdAsigncatalog_ItemDataBound(object sender, GridItemEventArgs e)
{
    if (e.Item is GridDataItem)
    {
        foreach (TableCell cell in e.Item.Cells)
        {
            cell.ToolTip = cell.Text != "&nbsp;" ? cell.Text : null;

        }
    }
}
protected void grdunAssignLocation_ItemDataBound(object sender, GridItemEventArgs e)
{
    if (e.Item is GridDataItem)
    {
        foreach (TableCell cell in e.Item.Cells)
        {
            cell.ToolTip = cell.Text != "&nbsp;" ? cell.Text : null;

        }
    }
}
protected void grdunAsigncatalog_ItemDataBound(object sender, GridItemEventArgs e)
{
    if (e.Item is GridDataItem)
    {
        foreach (TableCell cell in e.Item.Cells)
        {
            cell.ToolTip = cell.Text != "&nbsp;" ? cell.Text : null;

        }
    }
}

#region dumpUnusedCode

//private void BindItems()
//{
//    DataTable dtItems = new DataTable();
//    dtItems = itemObject.GetItemId();
//    //cmbItems.DataValueField = "Item_Code";
//    //cmbItems.DataTextField = "Item_Name";
//    //cmbItems.DataSource = dtItems;
//    //cmbItems.DataBind();

//}

//private void BindSubCataLogs()
//{
//    DataTable dtItems = new DataTable();
//    SubsystemObject.Constring = _constring;
//    dtItems = SubsystemObject.GetSubCatalogid();
//    //cmbSubCatalog.DataValueField = "SubSystem_Code";
//    //cmbSubCatalog.DataTextField = "SUBSYSTEM_DESCRIPTION";
//    //cmbSubCatalog.DataSource = dtItems;
//    //cmbSubCatalog.DataBind();

//}

//private void BindCatalogs()
//{
//    DataTable dtItems = new DataTable();
//    SystemObject.Constring = _constring;
//    dtItems = SystemObject.GetCatalogid();
//    //cmbCataLogs.DataValueField = "System_Code";
//    //cmbCataLogs.DataTextField = "System_Name";
//    //cmbCataLogs.DataSource = dtItems;
//    //cmbCataLogs.DataBind();
//}

protected void cmbCataLogs_SelectedIndexChanged(object sender, EventArgs e)
{
    //DataTable dtItems = new DataTable();
    //dtItems = itemObject.GetItemId();
    //dtItems.DefaultView.RowFilter = "System_Code='" + cmbCataLogs.SelectedItem.Value + "'";
    //cmbSubCatalog.DataValueField = "SubSystem_Code";
    //cmbSubCatalog.DataTextField = "SubSystem_Name";
    //cmbSubCatalog.DataSource = dtItems.DefaultView;
    //cmbSubCatalog.DataBind();
}

protected void cmbSubCatalog_SelectedIndexChanged(object sender, EventArgs e)
{
    //DataTable dtItems = new DataTable();
    //dtItems = itemObject.GetItemId();
    //dtItems.DefaultView.RowFilter = "System_Code='" + cmbCataLogs.SelectedItem.Value + "' and subsystem_code ='" + cmbSubCatalog.SelectedItem.Value + "'";
    //cmbItems.DataValueField = "Item_Code";
    //cmbItems.DataTextField = "Item_Name";
    //cmbItems.DataSource = dtItems.DefaultView;
    //cmbItems.DataBind();
}

protected void RadGrid7_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
{

}

protected void RadGrid4_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
{

}

protected void grdunAssignLocation_SelectedIndexChanged(object sender, EventArgs e)
{

}





protected void grdVessel_ItemCommand(object source, GridCommandEventArgs e)
{

}

public void onfilter()
{

}

protected void text1_TextChanged(object sender, EventArgs e)
{

}

#endregion


}
