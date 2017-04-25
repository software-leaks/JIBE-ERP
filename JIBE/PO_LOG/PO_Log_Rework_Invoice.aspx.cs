using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SMS.Business.ASL;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.POLOG;
using System.IO;

public partial class PO_LOG_PO_Log_Rework_Invoice : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUser();

        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void BindUser()
    {
        try
        {
           
            DataTable dt = BLL_POLOG_Invoice.Get_Invoice_UserList(Convert.ToInt32(Session["USERID"].ToString())).Tables[0];
            ddlUserName.DataSource = dt;
            ddlUserName.DataTextField = "For_Action_By";
            ddlUserName.DataValueField = "UserID";
            ddlUserName.DataBind();

            if (ddlUserName.Items.FindByValue(Convert.ToString(GetSessionUserID())) != null)
                {
                    ddlUserName.SelectedValue = Convert.ToString(GetSessionUserID());
                }
            ViewState["UserID"] = ddlUserName.SelectedValue;
            BindInvoiceCount();
         
        }
        catch { }
        {
        }
    }
    protected void BindInvoiceCount()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        DataSet ds = BLL_POLOG_Invoice.POLOG_Get_Rework_Invoice(Convert.ToInt16(ddlUserName.SelectedValue), sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

       // objChangeReqstMerge.AddMergedColumns(new int[] { 3, 4 }, "PO", "HeaderStyle-center");
        objChangeReqstMerge.AddMergedColumns(new int[] {4, 5, 6, 7,8,9 }, "Invoice", "HeaderStyle-center");
        

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }
      if (ds.Tables[0].Rows.Count > 0)
        {
            gvInvoice.DataSource = ds.Tables[0];
            gvInvoice.DataBind();
        }

      if (ds.Tables[1].Rows.Count > 0)
      {
          gvInvoiceCount.DataSource = ds.Tables[1];
          gvInvoiceCount.DataBind();
      }

    }
    //protected void BindInvoiceCount()
    //{
    //    DataSet ds = BLL_POLOG_Invoice.POLOG_Get_Rework_Invoice(Convert.ToInt16(ddlUserName.SelectedValue));
     
    //    //if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        gvInvoiceCount.DataSource = ds.Tables[0];
    //        gvInvoiceCount.DataBind();
    //    }
       
    //    //if (ds.Tables[1].Rows.Count > 0)
    //    {
    //        gvInvoice.DataSource = ds.Tables[1];
    //        gvInvoice.DataBind();
    //    }
    //    objChangeReqstMerge.AddMergedColumns(new int[] { 3, 4 }, "PO", "HeaderStyle-center");
    //    objChangeReqstMerge.AddMergedColumns(new int[] { 5, 6, 7 }, "Invoice", "HeaderStyle-center");


    //}
    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        BindInvoiceCount();
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        BindUser();
    }
    double total = 0.00;
    protected void gvInvoiceCount_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblUSD = (Label)e.Row.FindControl("lblUSD");
            double qty = Convert.ToDouble(lblUSD.Text);
            total = total + qty;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total: "  ;
            e.Row.Cells[3].Text = total.ToString();
        }
    }



    double intotal = 0.00;
    protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objChangeReqstMerge);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
        }
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblUSD_Amount = (Label)e.Row.FindControl("lblUSD_Amount");
            double qty = Convert.ToDouble(lblUSD_Amount.Text);
            Label lblDispute = (Label)e.Row.FindControl("lblDispute_Flag");
            intotal = intotal + qty;
            if (DataBinder.Eval(e.Row.DataItem, "Dispute_Flag").ToString() == "YES")
            {
                e.Row.Cells[13].BackColor = System.Drawing.Color.Red;
                lblDispute.ForeColor = System.Drawing.Color.White;
               
            }
          

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = "Total: ";
            e.Row.Cells[10].Text =  intotal.ToString(); ;
        }
       
    }

    //protected void BindGrid()
    //{
    //    try
    //    {
    //        BindInvoiceCount(ddlUserName.SelectedValue);
    //        objChangeReqstMerge.AddMergedColumns(new int[] { 2, 3, 4 }, "PO", "HeaderStyle-center");
    //        objChangeReqstMerge.AddMergedColumns(new int[] { 5, 6, 7, 8, 9, 10 }, "Invoice", "HeaderStyle-center");
    //        objChangeReqstMerge.AddMergedColumns(new int[] { 11, 12 }, "Invoice Verified", "HeaderStyle-center");

    //        int rowcount = ucCustomPagerItems.isCountRecord;
          

    //        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
    //        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
    //        DataSet ds = BLL_POLOG_Register.POLOG_Get_Pending_Invoice_Search(null,
    //                               null, null, null, null, UDFLib.ConvertIntegerToNull(GetSessionUserID()), sortbycoloumn, sortdirection
    //                         , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

    //        if (ucCustomPagerItems.isCountRecord == 1)
    //        {
    //            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
    //            ucCustomPagerItems.BuildPager();
    //        }
          
    //    }
    //    catch { }
    //    {
    //    }
    //}
}