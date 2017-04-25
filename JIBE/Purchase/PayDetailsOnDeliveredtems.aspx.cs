using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using   SMS.Business.PURC ;
using Telerik.Web.UI; 

public partial class Technical_INV_PayDetailsOnDeliveredtems : System.Web.UI.Page
{
//    double TotalPay;
//    double TotalItem;
//    double TotalQuatity;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL();
            BindDepartmentByST_SP();

           // txtfrom.Text = (DateTime.Now.AddDays(-(DateTime.Now.Day) + 1)).ToString("dd-MM-yyyy");
            txtfrom.Text = (DateTime.Now.AddDays(-(DateTime.Now.DayOfYear) + 1)).ToString("dd-MM-yyyy");
            txtto.Text = (DateTime.Now.AddMonths(1).AddDays(-(DateTime.Now.Day))).ToString("dd-MM-yyyy");
            BindPayDetailsGrid();

            CalTotalAmount();
       
        }
    }

    public void FillDDL()
    {
        try
        {
           using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                DataSet FleetDS = objTechService.SelectFleet();
                DDLFleet.DataSource = FleetDS.Tables[0];
                DDLFleet.DataTextField = "Name";
                DDLFleet.DataValueField = "Code";
                DDLFleet.DataBind();

                string Fleet = objTechService.SelectFleetByUser(Session["user"].ToString());
                switch (Fleet)
                {
                    case "Fleet A":
                        DDLFleet.SelectedIndex = 1;
                        break;
                    case "Fleet B":
                        DDLFleet.SelectedIndex = 2;
                        break;
                    case "Fleet C":
                        DDLFleet.SelectedIndex = 3;
                        break;
                    case "Fleet D":
                        DDLFleet.SelectedIndex = 4;
                        break;
                    case "Fleet Z":
                        DDLFleet.SelectedIndex = 5;
                        break;
                    case "Fleet E":
                        DDLFleet.SelectedIndex = 6;
                        break;
                }

                 DataTable Deptdt = objTechService.GetDeptType();
                 optList.DataSource = Deptdt;
                 optList.DataTextField = "Description";
                 optList.DataValueField ="Short_Code";
                 optList.DataBind();
                 optList.SelectedIndex=0;

                 
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

    private void BindDepartmentByST_SP()
    {
        try
        {
            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                DataTable dtDepartment = new DataTable();
                dtDepartment = objTechService.SelectDepartment();

                if (optList.SelectedItem.Text == "Spares")
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='"+optList.SelectedValue+"'";
                }
                else if(optList.SelectedItem.Text == "Stores")
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";
                }
                else if (optList.SelectedItem.Text == "Repairs")
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";
                }
                cmbDept.Items.Clear();
                cmbDept.DataSource = dtDepartment;
                cmbDept.AppendDataBoundItems = true;
                cmbDept.Items.Add("ALL");
                cmbDept.DataTextField = "Name_Dept";
                cmbDept.DataValueField = "Code";
                cmbDept.DataBind();
                 
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

    protected void cmbDept_OnSelectedIndexChanged(object source, System.EventArgs e)
    {
        try
        {
            StringBuilder filter = new StringBuilder();
            filter.Append("1=1");
            
            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                cmbCatalog.Items.Clear();
                DataTable dtCatalog = objTechService.SelectCatalog();
                //string department = cmbDept.SelectedValue.ToString();
                
                //filter = "dept1='" + department + "'" + " or " + "dept2='" + department + "'" + " or " + "dept3='" + department + "'" + " or " + "dept4='" + department + "'" + " or " + "dept5='" + department + "'" + " or " + "dept6='" + department + "'" + " or " + "dept7='" + department + "'" + " or " + "dept8='" + department + "'" + " or " + "dept9='" + department + "'" + " or " + "dept10='" + department + "'" + " or " + "dept11='" + department + "'" + " or " + "dept12='" + department + "'" + " or " + "dept13='" + department + "'" + " or " + "dept14='" + department + "'" + " or " + "dept15='" + department + "'";
                if (DDLVessel.SelectedIndex > 0)
                { 
                    filter.Append(" and Vessel_Code = '"+DDLVessel.SelectedValue.ToString()+"'");
                }

                if(cmbDept.SelectedIndex>0)
                {
                    filter.Append(" and code='" + cmbDept.SelectedValue.ToString() + "'");
                }
                dtCatalog.DefaultView.RowFilter = filter.ToString();

                if (dtCatalog.DefaultView.Count > 0)
                {
                    cmbCatalog.Items.Clear();
                    cmbCatalog.DataSource = dtCatalog.DefaultView;
                    cmbCatalog.AppendDataBoundItems = true;
                    cmbCatalog.Items.Add("ALL");                

                    cmbCatalog.DataTextField = "SYSTEM_DESCRIPTION";
                    cmbCatalog.DataValueField = "SYSTEM_CODE";
                    cmbCatalog.DataBind();
                }
                else
                {
                    cmbCatalog.Items.Clear();
                    cmbCatalog.Items.Add("ALL"); 
                }

                 
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

    protected void optList_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            BindDepartmentByST_SP();
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DDLVessel.Items.Clear();
            DDLVessel.AppendDataBoundItems = true;
            DDLVessel.Items.Add("ALL");

            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                DataTable dtVessel = objTechService.SelectVessels();
                dtVessel.DefaultView.RowFilter = "Tech_Manager ='" + DDLFleet.SelectedItem.Text + "'";
                DDLVessel.DataSource = dtVessel;
                DDLVessel.DataTextField = "Vessels";
                DDLVessel.DataValueField = "Vessel_Code";
                DDLVessel.DataBind();
                 
            }

            cmbCatalog.SelectedIndex = 0;
            cmbDept.SelectedIndex = 0;
            rgdPayDetails.Visible = false; 

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
    }
    
    public void BindPayDetailsGrid()
    {
        try
        {
            string catalogId="";
            string Vessel_Code = "";
            StringBuilder filter = new StringBuilder();
            filter.Append("1=1");

            filter.Append(" and Delivery_Date >='" + txtfrom.Text.Trim() + "'  and Delivery_Date <='" + txtto.Text.Trim() + "'");

            if (cmbDept.SelectedIndex > 0)
            {
                filter.Append(" and Department='" + cmbDept.SelectedValue.ToString() + "'");
            }
            if (optList.SelectedItem.Text != "ALL")
            {
                filter.Append(" and Form_Type='" + optList.SelectedItem.Value.ToString() + "'");
            }

            if (cmbCatalog.SelectedItem.Text != "ALL" && cmbCatalog.SelectedIndex > 0)
            {
                catalogId = cmbCatalog.SelectedValue.ToString();
            }
            if (DDLVessel.SelectedItem.Text != "ALL" && DDLVessel.SelectedIndex > 0)
            {
                Vessel_Code = DDLVessel.SelectedValue.ToString();
            }
            
            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                DataTable dtPayDetails = new DataTable();
                //need to pass the date as parameters..
                dtPayDetails = objTechService.SelectPayDetailsOnDeliveredItems(txtfrom.Text.Substring(6, 4) + "/" + txtfrom.Text.Substring(3, 2) + "/" + txtfrom.Text.Substring(0, 2), txtto.Text.Substring(6, 4) + "/" + txtto.Text.Substring(3, 2) + "/" + txtto.Text.Substring(0, 2), catalogId, Vessel_Code);
                dtPayDetails.DefaultView.RowFilter = filter.ToString();
                rgdPayDetails.DataSource = dtPayDetails.DefaultView;
                rgdPayDetails.DataBind();
                 
            }
            


        }
        catch //(Exception ex)
        {

        }
        finally
        { 
        
        }
    
    }

    private DataTable BindDeliveredItemsInHirarchy(string strReqCode,string strVesselCode ,string strDeliverCode ,string strCatalogId )
    {
        DataTable dtItems = new DataTable();
        try
        {
            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                dtItems = objTechService.BindDeliveredItemsInHirarchy(strReqCode, strDeliverCode, strCatalogId, strVesselCode );  
                 
            }
            return dtItems;
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            return dtItems = null;
        }
        finally
        {

        }

    }


    protected void rgdPayDetails_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            //RadGrid1.DataSource = GetDataTable("Select distinct  Req.REQUISITION_CODE,req.DOCUMENT_DATE requestion_Date,  req.REQUISITION_CODE,dep.Name_Dept,req.TOTAL_ITEMS,a.SYSTEM_Description,Req.Line_type,B.EVALUATION_OPTION,A.SYSTEM_CODE  from dbo.PURC_Dtl_Reqsn Req  LEFT OUTER JOIN dbo.PMS_Lib_Departments dep on req.DEPARTMENT=dep.code   inner join  (SELECT DISTINCT SITEM.REQUISITION_CODE,slib.SYSTEM_Description,Slib.System_Code  FROM dbo.PURC_Dtl_Supply_Items SITEM   INNER jOIN dbo.PMS_INV_Lib_Systems_Library Slib on SITEM.ITEM_SYSTEM_CODE=slib.SYSTEM_CODE) A on   A.REQUISITION_CODE=Req.REQUISITION_CODE   LEFT Outer Join   (select distinct QUOTATION_CODE,EVALUATION_OPTION from dbo.PURC_Dtl_Quoted_Prices where EVALUATION_OPTION=1) B on   Req.QUOTATION_CODE=B.QUOTATION_CODE");
         BindPayDetailsGrid();
            
        }
    }
    protected void rgdPayDetails_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;

        switch (e.DetailTableView.Name)
        {
            case "Items":
                {
                    DataTable dtDeliverItems = new DataTable(); 
                   
                    string strReqCode = dataItem.GetDataKeyValue("REQUISITION_CODE").ToString();
                    string strVesselCode = dataItem.GetDataKeyValue("Vessel_Code").ToString();
                    string strDeliverCode = dataItem.GetDataKeyValue("DELIVERY_CODE").ToString();
                    string strCatalogId = cmbCatalog.SelectedValue.ToString();
                    dtDeliverItems = BindDeliveredItemsInHirarchy(strReqCode, strVesselCode, strDeliverCode, strCatalogId);
                    //dtDeliverItems.DefaultView.RowFilter ="SYSTEM_CODE ='" + cmbCatalog.SelectedValue.ToString() + "'";
                    //e.DetailTableView.DataSource = dtDeliverItems.DefaultView; 

                    e.DetailTableView.DataSource = dtDeliverItems;
                    break;
                }

            //case "ItemsDetails": 
            //    {
            //        string REQUISITION_CODE = dataItem.GetDataKeyValue("REQUISITION_CODE").ToString();
            //        e.DetailTableView.DataSource = GetDataTable("SELECT DISTINCT itv.[ID],itv.[Part_Number],itv.[Short_Description],itv.[Unit_and_Packings], SITEM.[REQUESTED_QTY],SITEM.[ITEM_COMMENT], SITEM.REQUISITION_CODE,slib.SYSTEM_Description FROM dbo.PURC_Dtl_Supply_Items SITEM   INNER jOIN dbo.PMS_INV_Lib_Systems_Library Slib on SITEM.ITEM_SYSTEM_CODE=slib.SYSTEM_CODE INNER jOIN  dbo.PMS_INV_Lib_Items itv on itv.[ID]=SITEM.[ITEM_REF_CODE] where SITEM.REQUISITION_CODE ='" + REQUISITION_CODE + "'");
            //        break;
            //    }
        }
    }
     
    protected void rgdPayDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
//        string SubCatalogId;
        foreach (GridDataItem dataItem in rgdPayDetails.MasterTableView.Items)
        {
            if (dataItem.Selected)
            {
            }
        }
    }

    public void CalTotalAmount()
    {
        try
        {

            decimal Amount = 0;
            foreach (GridDataItem dataItem in rgdPayDetails.MasterTableView.Items)
            {
                Amount = Amount + Convert.ToDecimal(dataItem["Total_Pay_Amount"].Text);
            }
            txtTotalAmt.Text = Amount.ToString("####0.00");
        }
        catch //(Exception ex)
        {

        }
        finally
        { 
        
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {

        try
        {
            rgdPayDetails.Visible = true; 
            
            BindPayDetailsGrid();

            CalTotalAmount();
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        { 
        
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {

            cmbCatalog.SelectedIndex = 0;
            DDLFleet.SelectedIndex = 0;
            DDLVessel.SelectedIndex = 0;
            cmbDept.SelectedIndex = 0; 
            BindPayDetailsGrid();
            CalTotalAmount();
            rgdPayDetails.Visible = true ; 
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
       
    
    }



    protected void onMarkAsClose(object sender, CommandEventArgs e)
    {
        try
        {


        }
        catch //(Exception ex)
        { 
        
        
        }
    
    }
}


