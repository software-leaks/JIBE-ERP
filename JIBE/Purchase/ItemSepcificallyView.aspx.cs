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
using System.Text;
using ClsBLLTechnical;
using   SMS.Business.PURC ;

public partial class Technical_INV_ItemSepcificallyView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindRequisitionInfo();
        string Querry = Session["supplist"].ToString();
        //string QuerryPart = Session["SubQuerry"].ToString();
        //string[] SupplierList = Querry.Split(',');
        BindGridOnRetriveButtonClick(Querry);
    }
    protected void Page_Init()
    {

        string Querry = Session["supplist"].ToString();
        string[] SupplierList = Querry.Split(',');
        for (int i = 0; i < SupplierList.Length - 1; i++)
        {

            string Col_supp = SupplierList[i].ToString();
            GridBoundColumn boundColumn;
            boundColumn = new GridBoundColumn();
            boundColumn.HeaderText = Col_supp + " " + "Quoted Qty";
            boundColumn.DataField = Col_supp + "_OFFERED_QTY";
            boundColumn.UniqueName = Col_supp + "_OFFERED_QTY";
            boundColumn.DataFormatString = "{0:F2}";
            boundColumn.MaxLength = 100;
            boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            rgdItmSpecView.MasterTableView.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.HeaderText = Col_supp + " " + "Item Type";
            boundColumn.DataField = Col_supp + "_Item_Type";
            boundColumn.UniqueName = Col_supp + "_Item_Type";
            //boundColumn.DataFormatString = "{0:F2}";
            boundColumn.MaxLength = 100;
            boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            rgdItmSpecView.MasterTableView.Columns.Add(boundColumn);
                       
        }

    }

    protected void rgdItmSpecView_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {

    }
    //private void BindGridOnRetriveButtonClick(string Querry)
    //{
    //    string[] SupplierList = Querry.Split(',');
    //}
    private void BindGridOnRetriveButtonClick(string Querry)
    {
//        int suppSel = 0;
        string[] arrySuppName = new string[10];
        string[] arrSuppNameInShort = new string[10];
        string[] SupplierList = Querry.Split(',');

        //if (rgdItmSpecView.MasterTableView.Columns.Count > 10)
        //{
        //    //Remove all the programatically generated colomn from grid and 
        //    for (int i = rgdItmSpecView.MasterTableView.Columns.Count - 1; i >= 10; i--)
        //    {
        //        rgdItmSpecView.MasterTableView.Columns.RemoveAt(i);
        //    }
        //}
        //if (Request.QueryString["Requisitioncode"].ToString().Substring(0, 2) == "ST" || Request.QueryString["Requisitioncode"].ToString().Substring(0, 3) == "OST")
        //{
        //    rgdItmSpecView.MasterTableView.Columns[3].Display = false;
        //}
        StringBuilder strSql = new StringBuilder();
        StringBuilder strTables = new StringBuilder();
//        decimal MinValues = 0;
//        int col_id = 9;
//        int sel_Id = 0;
         strSql.Append("select distinct a.ITEM_SERIAL_NO,'False' chechstatus, a.QUOTATION_CODE,a.ITEM_REF_CODE, a.ITEM_SHORT_DESC,a.ITEM_FULL_DESC,a.QUOTED_QTY,item.Drawing_Number,Item.Part_Number,item.Unit_and_Packings,M.ROB_Qty,M.REQUESTED_QTY");
        // strTables.Append(" from PMS_Dtl_Quoted_Prices a ");
        strTables.Append("  from PURC_Dtl_Quoted_Prices a inner  join PURC_Lib_Items item On item.Id=A.Item_Ref_Code inner join PURC_Dtl_Supply_Items M on M.item_ref_code=A.item_ref_code and M.Document_Code=a.Document_Code and a.Vessel_Code=M.Vessel_Code   ");
        //foreach (GridDataItem dataItem in rgdSupplierInfo.MasterTableView.Items)
        //{
        for (int i = 0; i < SupplierList.Length-1; i++)
        {           

            string Col_supp = SupplierList[i].ToString();    
            strSql.Append(",");
            strTables.Append(" Inner Join ");
            string str = "(select ITEM_REF_CODE,QUOTATION_CODE,isnull(OFFERED_QTY,0) " + Col_supp + "_OFFERED_QTY,isnull((select top 1 Description from PURC_Lib_System_Parameters where Parent_Type='153' and Short_Code=isnull(PURC_Dtl_Quoted_Prices.Item_Type,'ORG')),'') " + Col_supp + "_Item_Type  from PURC_Dtl_Quoted_Prices where supplier_code='" + Col_supp + "' )  " + Col_supp + " on " + Col_supp + ".ITEM_REF_CODE = a.ITEM_REF_CODE and " + Col_supp + ".QUOTATION_CODE=a.QUOTATION_CODE ";
            strTables.Append(str);
            strSql.Append(Col_supp);
            strSql.Append(".* ");

            //GridBoundColumn boundColumn;
            //boundColumn = new GridBoundColumn();
            //boundColumn.HeaderText = Col_supp + " " + "QUOTED_QTY";
            //boundColumn.DataField = Col_supp + "_QUOTED_QTY";
            //boundColumn.UniqueName = Col_supp + "_QUOTED_QTY";
            //boundColumn.DataFormatString = "{0:F2}";
            //boundColumn.MaxLength = 100;
            //boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            //rgdItmSpecView.MasterTableView.Columns.Add(boundColumn);

            //boundColumn = new GridBoundColumn();
            //boundColumn.HeaderText = Col_supp + " " + "Item_Type";
            //boundColumn.DataField = Col_supp + "_Item_Type";
            //boundColumn.UniqueName = Col_supp + "_Item_Type";
            ////boundColumn.DataFormatString = "{0:F2}";
            //boundColumn.MaxLength = 100;
            //boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            //rgdItmSpecView.MasterTableView.Columns.Add(boundColumn);

        }
        strSql.Append(" ");
        strSql.Append(strTables);
        //Session["SubQuerry"] = strSql.ToString();
        string sqlstring = Session["SqlString"].ToString(); 
        string [] queryString=sqlstring.Split(',');
        strSql.Append("where  a.QUOTATION_CODE=(select top 1 QUOTATION_CODE from dbo.PURC_Dtl_Reqsn where REQUISITION_CODE='");
        strSql.Append(queryString[0].ToString());
        strSql.Append("'and Line_type='Q')");
        strSql.Append("and a.Vessel_code ='" + queryString[1].ToString());
        strSql.Append("'and a.Document_code ='" + queryString[2].ToString() + "'");

        TechnicalBAL objtechBAL = new TechnicalBAL();
        string FinalQuery = strSql.ToString();
        DataTable  dtQuatationInfo = objtechBAL.GetTable(FinalQuery);
        rgdItmSpecView.DataSource = dtQuatationInfo;
        rgdItmSpecView.DataBind();

    }
    private void BindRequisitionInfo()
    {
        string sqlstring = Session["SqlString"].ToString();
        string[] queryString = sqlstring.Split(',');

        try
        {
            DataTable dtReqInfo = new DataTable();
            using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
            {
                dtReqInfo = objTechService.SelectRequistionToSupplier(queryString[0].ToString(), queryString[2].ToString());
                lblReqNo.Text = dtReqInfo.DefaultView[0]["REQUISITION_CODE"].ToString();
                lblVessel.Text = dtReqInfo.DefaultView[0]["Vessel_Name"].ToString();
                lblCatalog.Text = dtReqInfo.DefaultView[0]["SYSTEM_Description"].ToString();
                lblToDate.Text = dtReqInfo.DefaultView[0]["requestion_Date"].ToString();
                lblTotalItem.Text = dtReqInfo.DefaultView[0]["TOTAL_ITEMS"].ToString();
                lblReqDate.Text = dtReqInfo.DefaultView[0]["RFQ_Date"].ToString();
                 
            }
            ViewState["dtRequistion"] = dtReqInfo;
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

    }
}
