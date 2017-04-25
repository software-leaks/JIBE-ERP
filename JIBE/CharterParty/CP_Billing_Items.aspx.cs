using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;

using Telerik.Web.UI;

//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.CP;
using SMS.Properties;

public partial class CP_Billing_Items : System.Web.UI.Page
{
    protected DataTable dtGridItems;
    UserAccess objUA = new UserAccess();
    public int CPID = 0;
    public int PortId = 0;
    public string  PortName = "";
    public string OType = "";
    public Boolean uaEditFlag = true;//Test default true
    public Boolean uaDeleteFlage = true;
    BLL_CP_CharterParty objCP = new BLL_CP_CharterParty();
    BLL_CP_HireInvoice objHireInv = new BLL_CP_HireInvoice();
    BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();
    protected void Page_Load(object sender, EventArgs e)
    {
       // UserAccessValidation();
        if (!IsPostBack)
        {
            if (Request.QueryString["CPID"] != null)
            {
                CPID = Convert.ToInt32(Request.QueryString["CPID"]);
                ViewState["CPID"] = CPID.ToString();
            }
            BindItems();

        }
    }


    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");


        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            // btnsave.Visible = false;
            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }



   private void BindItems()
    {
        try
        {
            DataSet ds = objCP.GET_Billing_Items_Detail(UDFLib.ConvertIntegerToNull(Session["CPID"]));
            DataTable dt = ds.Tables[0];
            string[] Pkey_cols = new string[] { "Billing_Item_Id" };
            string[] Hide_cols = new string[] { "Trading_Range_Id", "CPID"};
            DataTable dt1 = PivotTable("WEF_Date", "Item_Amount", "Trading_Range_Id", Pkey_cols, Hide_cols, dt);
            if (dt1.Rows.Count > 0)
            {
                dtGridItems = dt1;
                rgdItems.DataSource = dt1;
                rgdItems.DataBind();
               
            }
            else
            {
                rgdItems.DataSource = dtGridItems;
                rgdItems.DataBind();
               
                //rgdItems.MasterTableView.Columns[9].Visible = false;

            }
            ViewState["dtGridItems"] = dtGridItems;

        }
        catch (Exception ex)
        {
            //lblError.Text = ex.ToString();
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

    }



    protected void btnSaveItem_Click(object sender, EventArgs e)
    {

        if (Session["CPID"] != null)
        {
            BindItems();

        }


    }



    protected void rgdItems_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[2].Visible=false;
        e.Row.Cells[1].Visible = false;
        
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Controls.Count; i++)
            {


                    var headerCell = e.Row.Controls[i] as DataControlFieldHeaderCell;
                      if (headerCell.Text.Contains("Item_Group"))
                        headerCell.Text = "Item Group";
                    else if (headerCell.Text.Contains("Item_Description"))
                        headerCell.Text = "Description";
                      else if (headerCell.Text.Contains("Interval_Unit"))
                          headerCell.Text = "Unit";
                      else if (headerCell.Text.Contains("Billing_Interval"))
                        headerCell.Text = "Billing Interval";
                     else if (headerCell.Text.Contains("Item_Rate"))
                        headerCell.Text = "Item Rate";
                        
                    else if (headerCell != null)
                    {
                        if (i > 5)
                           headerCell.Text = "WEF: " + headerCell.Text;
 
                    }
                
            }

        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Controls.Count; i++)
            {
                if(i<= 5)
                     e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Left;
                else
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }

        }
   }

    public static DataTable PivotTable(string PivotColumnName, string PivotValueColumnName, string PivotColumnOrder, string[] PrimaryKeyColumns, string[] HideColumns, DataTable dtTableToPivot)
    {
        StringBuilder sbPKs = new StringBuilder();

        DataTable dtFinalResult = new DataTable();
        DataView dvPivotColumnNames = dtTableToPivot.DefaultView.ToTable(true, new string[] { PivotColumnName, PivotColumnOrder }).DefaultView;
        dvPivotColumnNames.Sort = PivotColumnOrder;

        DataTable dtPivotPrimaryKeys = dtTableToPivot.DefaultView.ToTable(true, PrimaryKeyColumns);


        foreach (DataColumn dcol in dtTableToPivot.Columns)
        {
            if (dcol.ColumnName != PivotColumnName && dcol.ColumnName != PivotValueColumnName && dcol.ColumnName != PivotColumnOrder)
            {
               dtFinalResult.Columns.Add(dcol.ColumnName);

            }
        }

        foreach (DataRow drCol in dvPivotColumnNames.ToTable().Rows)
        {
            dtFinalResult.Columns.Add(drCol[0].ToString());
        }


        foreach (DataRow drPK in dtPivotPrimaryKeys.Rows)
        {
            DataRow drNew = dtFinalResult.NewRow();

            foreach (DataColumn dcol in dtTableToPivot.Columns)
            {
                if (dcol.ColumnName != PivotColumnName && dcol.ColumnName != PivotValueColumnName && dcol.ColumnName != PivotColumnOrder)
                {
                   
                    sbPKs.Clear();
                    foreach (string pk in PrimaryKeyColumns)
                    {
                        sbPKs.Append(pk + " = " + drPK[pk].ToString() + " and ");

                    }
                    sbPKs.Append(" 1=1  ");

                    DataRow[] drcoll = dtTableToPivot.Select(sbPKs.ToString());//[0][dcol.ColumnName];
                    drNew[dcol.ColumnName] = drcoll[0][dcol.ColumnName];
                }
            }

            foreach (DataRow drCol in dvPivotColumnNames.ToTable().Rows)
            {
               
                sbPKs.Clear();
                foreach (string pk in PrimaryKeyColumns)
                {
                    sbPKs.Append(pk + " = " + drPK[pk].ToString() + " and ");

                }




                DataRow[] drValue = dtTableToPivot.Select(sbPKs.ToString() + PivotColumnName + " = '" + drCol[0].ToString() + "' ");
                if (drValue.Length > 0)
                    drNew[drCol[0].ToString()] = drValue[0][PivotValueColumnName];
                else
                    drNew[drCol[0].ToString()] = null;
            }

            dtFinalResult.Rows.Add(drNew);
        }

       
        if (HideColumns != null)
        {
            foreach (string strColToremove in HideColumns)
            {
                if (dtFinalResult.Columns.IndexOf(strColToremove) > -1)
                    dtFinalResult.Columns.Remove(strColToremove);
            }
        }
        return dtFinalResult;
    }





    protected void onDelete(object source, CommandEventArgs e)
    {
        HiddenField hdnTPId = (rgdItems.FindControl("hdnTPId") as HiddenField);
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? ItemID = UDFLib.ConvertIntegerToNull(arg[0]);
        objCP.DEL_Billing_Item(ItemID, GetSessionUserID());
        BindItems();
    }


    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        BindItems();
    }
}
   
