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
using   SMS.Business.PURC ;
using Telerik.Web.UI;
using System.Data.SqlClient;

public partial class PendingRequisitionPO : System.Web.UI.Page
{
    protected void Page_Init(object source, System.EventArgs e)
    {
           
    }
    DataTable dtRequistion = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
          
            BindRequisitionGrid();
            FillDDL();
            BindDepartmentByST_SP();
            string strQuerOpt = "0";
            if (Request.QueryString.Count > 0)
            {
                strQuerOpt = Request.QueryString["OptCode"].ToString();
            }

            if (strQuerOpt != "")
            {
                optRequiPendingType.SelectedIndex = 3;
            }
            else
            {
                optRequiPendingType.SelectedIndex = 3;
            }
       }
      
    }
 
    private DataTable BindRequisitionInHirarchy()
    {
      dtRequistion = new DataTable();
        try
        { 
              

            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                dtRequistion = objTechService.SelectRequisitionForHierarchy(); 
                 
                
            }
            return dtRequistion;
        }
        catch  
        {
          
            return dtRequistion = null; ;
        }
        finally
        {

        }

    }

    private DataTable BindItemsInHirarchy(string strRequistionCode,string strVesselCode,string strDocumnetCode )
    {
        DataTable dtItems = new DataTable();
        try
        {
            
            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {

                dtItems = objTechService.SelectSupplierToSendOrder(strRequistionCode, strVesselCode);
            }
            return dtItems;
        }
        catch 
        {
            
            return dtItems=null ;
        }
        finally
        {

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
                  
            }
        }
        catch  
        {
            
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
                    dtDepartment.DefaultView.RowFilter = "Form_Type='SP'";
                }
                else
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='ST'";

                }
                cmbDept.Items.Clear();
                cmbDept.DataSource = dtDepartment;
                cmbDept.AppendDataBoundItems = true;
                cmbDept.Items.Add("--Select--");
                cmbDept.DataTextField = "Name_Dept";
                cmbDept.DataValueField = "Code";
                cmbDept.DataBind();
                 
            }

        }
        catch  
        {
          
        }
        
 
    }

    protected void cmbDept_OnSelectedIndexChanged(object source, System.EventArgs e)
    {
        try
        {
            string filter;
            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                
                DataTable dtCatalog = objTechService.SelectCatalog();
                string department = cmbDept.SelectedValue.ToString();

                if (cmbDept.SelectedValue.ToString() != "--Select--")
                {

                    filter = "dept1='" + department + "'" + " or " + "dept2='" + department + "'" + " or " + "dept3='" + department + "'" + " or " + "dept4='" + department + "'" + " or " + "dept5='" + department + "'" + " or " + "dept6='" + department + "'" + " or " + "dept7='" + department + "'" + " or " + "dept8='" + department + "'" + " or " + "dept9='" + department + "'" + " or " + "dept10='" + department + "'" + " or " + "dept11='" + department + "'" + " or " + "dept12='" + department + "'" + " or " + "dept13='" + department + "'" + " or " + "dept14='" + department + "'" + " or " + "dept15='" + department + "'";
                    dtCatalog.DefaultView.RowFilter = filter;

                    if (dtCatalog.DefaultView.Count > 0)
                    {
                        cmbCatalog.Items.Clear();
                        cmbCatalog.DataSource = dtCatalog.DefaultView;
                        cmbCatalog.Items.Add("--Select--");
                        cmbCatalog.AppendDataBoundItems = true;

                        cmbCatalog.DataTextField = "SYSTEM_DESCRIPTION";
                        cmbCatalog.DataValueField = "SYSTEM_CODE";
                        cmbCatalog.DataBind();
                    }
                    else
                    {

                    }
                }
                else
                {
                    cmbCatalog.Items.Clear();
                    cmbCatalog.Items.Add("--Select--");
                }

                 
            }
        }
        catch  
        {
        
        }
        
    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            
            DDLVessel.Items.Clear();
            DDLVessel.AppendDataBoundItems = true;
            DDLVessel.Items.Add("--Select--");

            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                DataTable dtVessel = objTechService.SelectVessels();
                dtVessel.DefaultView.RowFilter = "Tech_Manager ='" + DDLFleet.SelectedItem.Text + "'";
                DDLVessel.DataSource = dtVessel;
                DDLVessel.DataTextField = "Vessels";
                DDLVessel.DataValueField = "Vessel_Code";
                DDLVessel.DataBind();
                 
            }
        }
        catch  
        {
        
        }
        
    }

    protected void optList_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            BindDepartmentByST_SP();
            cmbDept_OnSelectedIndexChanged(null,null);
             
        }
        catch 
        {
             
        }
        
    }

    protected void optRequiPendingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            /* ======Menu to Open the Forms==========

               1:Pending Requistion
               2:Quatation Received
               3:Purchased Order Raised
               4:Delivery Update
             */
            BindRequisitionGrid();
 
        }
        catch  
        {
          
           
        }
        finally
        { 
        
        }
    }

    private void BindRequisitionGrid()
    {
        try
        {
            string strAttFlag = "";

            //if (cmbCatalog.SelectedItem.ToString() != "--Select--")
            //{
//                string filter = "";
                string strOption = optRequiPendingType.SelectedValue.ToString();
                string strVesselCode = "";
                if (DDLVessel.SelectedValue.ToString() != "0")
                {
                   strVesselCode = DDLVessel.SelectedValue.ToString();
                }
                else 
                {
                    strVesselCode = "";
                }

                int value = Int32.Parse(strOption);

                using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
                {
                    switch (value)
                    {
                        case 1:
                            //rgdPending.Columns[4].Visible = true;
                            //rgdPending.Columns[4].HeaderText="Send RFQ";
                            //dtRequistion = objTechService.SelectPendingRequistion(strVesselCode);
                            ResponseHelper.Redirect("PendingRequisitionDetails.aspx?OptCode=0", "Blank", "");    
                            break;
                        case 2:
                            //rgdPending.Columns[4].Visible = true;
                            //rgdPending.Columns[4].HeaderText = "Upload Qtn";
                            //dtRequistion = objTechService.SelectPendingQuatationReceive(strVesselCode);
                            ResponseHelper.Redirect("PendingRequisitionDetails.aspx?OptCode=1", "Blank", "");    
                            break;
                        case 3:
                            //rgdPending.Columns[4].Visible = true;
                            //rgdPending.Columns[4].HeaderText = "Qtn Eval";
                            //dtRequistion = objTechService.SelectPendingQuatationEvalution(strVesselCode);  
                            ResponseHelper.Redirect("PendingRequisitionDetails.aspx?OptCode=2", "Blank", "");    
                            break;
                        case 4:
                            rgdPending.Columns[4].Visible = true;
                            rgdPending.Columns[4].HeaderText = "Raise PO";                             
                            //dtRequistion = objTechService.SelectPendingPurchasedOrderRaise(strVesselCode);                           
                            break;
                        case 5:
                            //rgdPending.Columns[4].Visible = true;
                            //rgdPending.Columns[4].HeaderText = "Req. Deliver";
                            //dtRequistion = objTechService.SelectPendingDeliveryUpdate(strVesselCode);  
                            ResponseHelper.Redirect("PendingRequisitionDetails.aspx?OptCode=4", "Blank", "");    
                            break;
                    }
                    if (cmbCatalog.SelectedValue.ToString() != "0")
                    {
                        dtRequistion.DefaultView.RowFilter = "SYSTEM_CODE ='" + cmbCatalog.SelectedValue.ToString() + "'";
                        if (dtRequistion.DefaultView.Count > 0)
                        {
                            rgdPending.DataSource = dtRequistion.DefaultView;
                        }
                        else
                        {
                            dtRequistion.Rows.Clear(); 
                            rgdPending.DataSource = dtRequistion;
                        }
                    }
                    else 
                    {
                        rgdPending.DataSource = dtRequistion; 
                    }

                    rgdPending.DataBind();
                     
                }


                //Changes the Attache Image on the basis of whethere file is Attached or not with Requisiton.

                foreach (GridDataItem dataItem in rgdPending.MasterTableView.Items)
                {
                    ImageButton ImgAttach = (ImageButton)(dataItem.FindControl("ImgAttachment") as ImageButton);

                    strAttFlag = dataItem["Attach_Status"].Text.ToString();
                    if (strAttFlag == "0")
                    {
                        ImgAttach.ImageUrl = "~/Technical/INV/Image/transparent.gif";  
                        ImgAttach.Enabled = false;
                        ImgAttach.ToolTip = "No Attachment(s)";
                        
                    }
                    else
                    {
                        ImgAttach.ImageUrl = "~/Technical/INV/Image/attach1.gif";
                        ImgAttach.ToolTip = "To view Attahment(s) click on image";
                    }
                }
            }


            //}
            catch //(Exception ex)
            {
            }
            finally 
            {
            }

        }
 
    public void cmbCatalog_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        try
        {

            BindRequisitionGrid();
         
        }
        catch 
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
             
        }
        finally
        {

        }
    }


    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            //RadGrid1.DataSource = GetDataTable("Select distinct  Req.REQUISITION_CODE,req.DOCUMENT_DATE requestion_Date,  req.REQUISITION_CODE,dep.Name_Dept,req.TOTAL_ITEMS,a.SYSTEM_Description,Req.Line_type,B.EVALUATION_OPTION,A.SYSTEM_CODE  from dbo.PURC_Dtl_Reqsn Req  LEFT OUTER JOIN dbo.PMS_Lib_Departments dep on req.DEPARTMENT=dep.code   inner join  (SELECT DISTINCT SITEM.REQUISITION_CODE,slib.SYSTEM_Description,Slib.System_Code  FROM dbo.PURC_Dtl_Supply_Items SITEM   INNER jOIN dbo.PMS_INV_Lib_Systems_Library Slib on SITEM.ITEM_SYSTEM_CODE=slib.SYSTEM_CODE) A on   A.REQUISITION_CODE=Req.REQUISITION_CODE   LEFT Outer Join   (select distinct QUOTATION_CODE,EVALUATION_OPTION from dbo.PURC_Dtl_Quoted_Prices where EVALUATION_OPTION=1) B on   Req.QUOTATION_CODE=B.QUOTATION_CODE");
            BindRequisitionGrid();
        }
    }

    protected void uploadToExcel(object source, CommandEventArgs e)
    { 
    
    
    }

    protected void onSelect(object source, CommandEventArgs e)
    {
        /* ======Menu to Open the Forms==========
         
                1:Pending Requistion
                2:Quatation Received
                3:Purchased Order Raised
                4:Delivery Update
         */
        
        string strOption =  optRequiPendingType.SelectedValue.ToString()   ;
        int value = Int32.Parse(strOption);
        switch (value)
         { 
         case 1:

             //ResponseHelper.Redirect("SelectSuppliers.aspx?Requisitioncode=" + e.CommandArgument.ToString() + "&Vessel_Code=" + DDLVessel.SelectedValue, "Blank", "");
             //ResponseHelper.Redirect("SendToSupplier.aspx?Requisitioncode=" + e.CommandArgument.ToString() + "&Vessel_Code=" + DDLVessel.SelectedValue, "Blank", "");
             ResponseHelper.Redirect("SelectSuppliers.aspx?Requisitioncode=" + e.CommandArgument.ToString(), "Blank", "");    
            
             break;
         case 2:

            // ResponseHelper.Redirect("QuatationEntry.aspx?Requisitioncode=" + e.CommandArgument.ToString() + "&Vessel_Code=" + DDLVessel.SelectedValue, "Blank", "");
             ResponseHelper.Redirect("QuatationEntry.aspx?Requisitioncode=" + e.CommandArgument.ToString(), "Blank", "");
             break;
         case 3:

             //ResponseHelper.Redirect("QuatationEvalution.aspx?Requisitioncode=" + e.CommandArgument.ToString() + "&Vessel_Code=" + DDLVessel.SelectedValue, "Blank", "");
             ResponseHelper.Redirect("QuatationEvalution.aspx?Requisitioncode=" + e.CommandArgument.ToString() , "Blank", "");
          
             break;
         case 4:

             //ResponseHelper.Redirect("PurchasedOrderRaised.aspx?Requisitioncode=" + e.CommandArgument.ToString() + "&Vessel_Code=" + DDLVessel.SelectedValue, "Blank", "");
             ResponseHelper.Redirect("PurchasedOrderRaised.aspx?Requisitioncode=" + e.CommandArgument.ToString(), "Blank", "");
         
             break;
         case 5:

             //ResponseHelper.Redirect("DeliveredItems.aspx?Requisitioncode=" + e.CommandArgument.ToString() + "&Vessel_Code=" + DDLVessel.SelectedValue + "&Dept_Code=" + cmbDept.SelectedValue, "Blank", "");
             ResponseHelper.Redirect("DeliveredItems.aspx?Requisitioncode=" + e.CommandArgument.ToString() + "&Dept_Code=" + cmbDept.SelectedValue, "Blank", "");
             break;
         }

    }

    private bool IsFileAttached(string ReqCode ,string FileType)
    {
        try
        {
            DataTable dtProItemsCons = new DataTable(); 

            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                dtProItemsCons = objTechService.GetAttachedFileInfo(DDLVessel.SelectedValue.ToString());
                dtProItemsCons.DefaultView.RowFilter = "Requisition_Code='" + ReqCode + "'";
                  
            }

            if (dtProItemsCons.DefaultView.Count > 0)
            {
                return true;
            }
            else 
            {
                return false;
            }


        }
        catch//(Exception ex)
        {
            return false;
        }
    
    }


    protected void onSelectAttachment(object source, CommandEventArgs e)
    {
        string strOption = optRequiPendingType.SelectedValue.ToString();
        int value = Int32.Parse(strOption);
        string strFileType = "";

        Session["Fleet"] = DDLFleet.SelectedValue.ToString();
        Session["VesselCode"] = DDLVessel.SelectedValue.ToString();   

        switch (value)
        {
            case 1:
                strFileType = "RFQ";

                if (IsFileAttached(e.CommandArgument.ToString(), strFileType))
                {
                    ResponseHelper.Redirect("FileAttachmentInfo.aspx?Requisitioncode=" + e.CommandArgument.ToString() + "&Vessel_Code=" + DDLVessel.SelectedValue + "&FileType=" + strFileType, "Blank", "");
                }
                else
                {
                    String msg = String.Format("alert(''There is no file attached with this Requisition.')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                }
                break;

            case 2:
                strFileType = "QUpld";
                if (IsFileAttached(e.CommandArgument.ToString(), strFileType))
                {
                    ResponseHelper.Redirect("FileAttachmentInfo.aspx?Requisitioncode=" + e.CommandArgument.ToString() + "&Vessel_Code=" + DDLVessel.SelectedValue + "&FileType=" + strFileType, "Blank", "");
                }
                else
                {

                }
                break;

            case 3:
                ResponseHelper.Redirect("FileAttachmentInfo.aspx?Requisitioncode=" + e.CommandArgument.ToString() + "&Vessel_Code=" + DDLVessel.SelectedValue + "&FileType=" + strFileType, "Blank", "");
                break;
            
            case 4:
               
                strFileType = "PO";
                if (IsFileAttached(e.CommandArgument.ToString(), strFileType))
                {
                    ResponseHelper.Redirect("FileAttachmentInfo.aspx?Requisitioncode=" + e.CommandArgument.ToString() + "&Vessel_Code=" + DDLVessel.SelectedValue + "&FileType=" + strFileType, "Blank", "");
                }
                else
                {

                }
                break;

            case 5:
                ResponseHelper.Redirect("FileAttachmentInfo.aspx?Requisitioncode=" + e.CommandArgument.ToString() + "&Vessel_Code=" + DDLVessel.SelectedValue + "&FileType=" + strFileType, "Blank", "");
                break;

        }
    
    }


     
    protected void RadGrid1_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        switch (e.DetailTableView.Name)
        {
            case "Items":
                {
                    string REQUISITION_CODE = dataItem.GetDataKeyValue("REQUISITION_CODE").ToString();
                    string DocumentCode = dataItem.GetDataKeyValue("document_code").ToString();
                    string Vessel_Code = dataItem.GetDataKeyValue("Vessel_Code").ToString();
                    e.DetailTableView.DataSource = BindItemsInHirarchy(REQUISITION_CODE, Vessel_Code, DocumentCode);   
                    break;
                }
        }
    }


    
    protected void onSelectApprovePO(object source, CommandEventArgs e)
    {
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindRequisitionGrid();
    }
}



 
