using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.QMS;
using SMS.Business.Crew;
using SMS.Business.QMSDB;

public partial class RestHourExceptionReports : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

           
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;
            BindGrid();
        }
        //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }


   
   

   

    public void BindGrid()
    {

        int rowcount = 0;

        string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = BLL_QMS_RestHours.Get_RestHours_Index(null,null,null,null
            , null,null, sortbycoloumn, sortdirection
            , 1, 100, ref  rowcount);

     

        if (dt.Rows.Count > 0)
        {
            
            gvDeckLogBook.DataSource = dt;
            gvDeckLogBook.DataBind();
        }
        else
        {
            gvDeckLogBook.DataSource = dt;
            gvDeckLogBook.DataBind();
        }
    }


    protected void txtfrom_TextChanged(object sender, EventArgs e)
    {
        BindGrid();
         
    }

    protected void txtCrew_TextChanged(object sender, EventArgs e)
    {
        
        BindGrid();

    }
    protected void ddlRank_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        BindGrid();

    }
    protected void txtto_TextChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void ddlvessel_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindGrid();
       
    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        BindGrid();
    }

    protected void gvDeckLogBook_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindGrid();
    }

    protected void gvDeckLogBook_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowUp.png";
                    else
                        img.Src = "~/purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }
    }

    protected void gvDeckLogBook_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';this.style.font='bold';";
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.font='bold';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["onclick"] = ClientScript.GetPostBackEventReference(this.gvDeckLogBook, "Select$" + e.Row.RowIndex);

        }

    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {

        int rowcount = 0;

        string vesselcode = (ViewState["VesselCode"] == null) ? null : (ViewState["VesselCode"].ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_QMS_RestHours.Get_RestHours_Index(null,null,null,null
           , null,null, sortbycoloumn, sortdirection
           ,1, 100, ref  rowcount);


        string[] HeaderCaptions = { "Vessel", "From Date", "To Date", "Remarks" };
        string[] DataColumnsName = { "Vessel_Name", "From_Date", "To_Date", "Remarks" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "DeckLogBook", "Deck Log Book", "");
    
    }

  

  
     

    protected void gvDeckLogBook_SelectedIndexChanging(object sender, GridViewSelectEventArgs se)
    {
        Label lblVesselID = (Label)gvDeckLogBook.Rows[se.NewSelectedIndex].FindControl("lblVesselID");
        Label lblDeckLogBookID = (Label)gvDeckLogBook.Rows[se.NewSelectedIndex].FindControl("lblDeckLogBookID");

        ResponseHelper.Redirect("../RestHours/RestHourDetails.aspx?ID=" + lblDeckLogBookID.Text.Trim() + "&Vessel_ID=" + lblVesselID.Text, "Blank", "");

    }
}