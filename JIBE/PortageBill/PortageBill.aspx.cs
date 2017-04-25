using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.PortageBill;
using System.Collections;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;


public partial class PortageBill_PortageBill : System.Web.UI.Page
{
    public DataTable dtEntryType = new DataTable();
    public ArrayList typeamount = new ArrayList();
    public int startupdno = 5;
    public DataTable dtcomment;
    public DataTable dtfinalz;
    public DataTable dtdeduction;
    public DataSet ds;
    public DataTable dtdeclaredearn;
    public DataTable myDataTable;
    static int scodeID = 0;
    static int totalID = 0;
    static int joindateID = 0;
    static int BFid = 0;
    static int signofdateid = 0;
    static int totalEarnID = 0;
    static int totalDedID = 0;
    Int16 takeoversts = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // show office portage only if office portage bill has been considered for any joining type//



        string arg = Request.QueryString["arg"].ToString();
        string[] arr;
        if (arg != null && arg.Length > 0)
        {
            arr = arg.Split('~');

            ViewState["Vessel_ID"] = UDFLib.ConvertToInteger(arr[0]);
            ViewState["PBDt"] = DateTime.Parse(arr[1]);
            ViewState["Vessel_Name"] = arr[2];
        }

        if (UDFLib.ConvertToInteger(ViewState["Vessel_ID"]) > 0)
        {
            BLL_Crew_Admin objAdm = new BLL_Crew_Admin();
            DataTable dtJtype = objAdm.Get_JoiningType_List(null);

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable dtVsl = objVsl.GetVesselDetails_ByID(Convert.ToInt32(ViewState["Vessel_ID"]));

            if (dtJtype.Rows.Count > 0 && dtVsl.Rows.Count > 0)
            {
                // for office vessel only
                if (dtJtype.Select("OfficePortageBillConsidered=1").Length > 0 && UDFLib.ConvertToInteger(dtVsl.Rows[0]["ISVESSEL"]) == 0)
                {
                    btnGeneratePortageBill.Visible = true;
                    btnFinalizePortageBill.Visible = true;
                    DataTable dtOPPbill = BLL_PB_PortageBill.Get_Open_PortageBill();
                    if (dtOPPbill.Rows.Count > 0 && Convert.ToString(dtOPPbill.Rows[0]["GENERATE_PB"]).Trim().Length > 1)
                    {
                        ViewState["Open_PB_Month"] = Convert.ToDateTime(dtOPPbill.Rows[0]["PB_GEN_DATE"]).Month;
                        ViewState["Open_PB_Year"] = Convert.ToDateTime(dtOPPbill.Rows[0]["PB_GEN_DATE"]).Year;
                    }

                    // if open pbill is same as requested then generate pbill // 
                    if (int.Parse(ViewState["Open_PB_Month"].ToString()) == DateTime.Parse(ViewState["PBDt"].ToString()).Month && int.Parse(ViewState["Open_PB_Year"].ToString()) == DateTime.Parse(ViewState["PBDt"].ToString()).Year)
                    {
                        BLL_PB_PortageBill.Upd_Generate_PortageBill(ViewState["Open_PB_Month"].ToString(), ViewState["Open_PB_Year"].ToString(), Convert.ToInt32(ViewState["Vessel_ID"]), Convert.ToInt32(Session["USERID"]), null);
                    }
                    else
                    {
                        btnGeneratePortageBill.Visible = false;
                        btnFinalizePortageBill.Visible = false;
                    }


                }
                else
                {
                    btnGeneratePortageBill.Visible = false;
                    btnFinalizePortageBill.Visible = false;
                }
            }


            Load_PortageBill();
        }


        UserAccessValidation();
    }

    private void Load_PortageBill()
    {
        try
        {
            string _SortOrderColumns = "DOA_HomePort , RANK_SORT_ORDER ";

            Dictionary<string, UDCHyperLink> DicLink = new Dictionary<string, UDCHyperLink>();
            lblPageTitle.Text = "Portage Bill : " + Convert.ToString(ViewState["Vessel_Name"]) + "&nbsp-&nbsp" + DateTime.Parse(ViewState["PBDt"].ToString()).ToString("MMM-yyyy");

            UDCHyperLink alink = new UDCHyperLink();
            alink.Target = "_blank";
            alink.NaviagteURL = "../crew/CrewDetails.aspx";
            alink.QueryStringDataColumn = new string[] { "VoyageID", "CrewID" };
            alink.QueryStringText = new string[] { "VoyageID", "ID" };
            DicLink.Add("Code", alink);

            DataSet dsPb = BLL_PB_PortageBill.Get_PortageBill(Convert.ToInt32(DateTime.Parse(ViewState["PBDt"].ToString()).Month), Convert.ToInt32(DateTime.Parse(ViewState["PBDt"].ToString()).Year), Convert.ToInt32(ViewState["Vessel_ID"]), UDFLib.ConvertStringToNull(null), 0);
            dtcomment = dsPb.Tables["Comment"];
            dtEntryType = dsPb.Tables["EntryType"];

            DataTable dtpb = UDFLib.PivotTable("Description",
                                                  "Amount",
                                                  "Sort_Order",
                                                  new string[] { "VOYAGEID", "PBMONTH", "PBYEAR" },
                                                  new string[] { "VOYAGEID", "PBMONTH", "PBYEAR", "RANK_SORT_ORDER", "IsFinalized", "DOA_HomePort", "RANK_SORT_ORDER", "Amount", "Description", "Sort_Order", "CrewID" },
                                                  _SortOrderColumns,
                                                  DicLink,
                                                  dsPb.Tables["Data"]
                                                  );
            totalEarnID = dtpb.Columns.IndexOf("Total Earning");
            totalDedID = dtpb.Columns.IndexOf("Total Deduction");
            BFid = dtpb.Columns.IndexOf("balance");
            dtpb.DefaultView.RowFilter = "rank <> ''";
            DataTable dt = dtpb.DefaultView.ToTable();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["From"].ToString()))
                    {
                        dr["From"] = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(dr["From"].ToString()).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
                    }
                    if (!string.IsNullOrEmpty(dr["To"].ToString()))
                    {
                        dr["To"] = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(dr["To"].ToString()).ToString("dd/MM/yyyy"), UDFLib.GetDateFormat());
                    }
                }
            }
            GridViewPB.DataSource = dt;
            GridViewPB.DataBind();
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }

    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {

        }

        if (objUA.Edit == 0)
        {

        }
        if (objUA.Delete == 0)
        {

        }
        if (objUA.Approve == 0)
        {

        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }



    protected void GridViewPB_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string encoded = e.Row.Cells[0].Text;
            e.Row.Cells[0].Text = Context.Server.HtmlDecode(encoded);

        }


    }

    protected void GridViewPB_DataBound(object s, EventArgs e)
    {
        try
        {
            //for (int j = 0; j <= startupdno; j++)
            //{
            //    GridViewPB.HeaderRow.Cells[j].CssClass = "hd";
            //}
            GridViewPB.HeaderRow.Cells[1].Width = 160;

            int fixedcolid = 0;

            var header = GridViewPB.FooterRow;
            header.Cells[0].Text = "Total";
            header.BackColor = System.Drawing.Color.LightYellow;
            header.ForeColor = System.Drawing.Color.Blue;
            header.Font.Bold = true;

            foreach (GridViewRow gr in GridViewPB.Rows)
            {
                gr.Cells[1].Width = 160;
                gr.Cells[totalEarnID].ForeColor = System.Drawing.Color.Navy;
                gr.Cells[totalDedID].ForeColor = System.Drawing.Color.Navy;
                gr.Cells[BFid].ForeColor = System.Drawing.Color.Purple;
                gr.Cells[totalEarnID].Font.Bold = true;
                gr.Cells[totalDedID].Font.Bold = true;
                gr.Cells[BFid].Font.Bold = true;

                int SalTypeii = 0;
                for (int i = startupdno + 1; i < gr.Cells.Count; i++)
                {
                    if (fixedcolid <= startupdno)
                    {
                        gr.Cells[fixedcolid].CssClass = "PbillHeaderStyle-css-fixed";

                    }


                    if (i >= startupdno)
                    {
                        decimal val = 0;
                        if (decimal.TryParse(gr.Cells[i].Text, out val))
                        {
                            gr.Cells[i].Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                            header.Cells[i].Text = string.Format("{0:0,0.00}", (UDFLib.ConvertToDecimal(header.Cells[i].Text) + val));
                            header.Cells[i].Style.Add(HtmlTextWriterStyle.TextAlign, "right");

                        }
                    }


                    ////////////////


                    if (SalTypeii < dtEntryType.Rows.Count)
                    {
                        string VoyageID = gr.Cells[0].Text;
                        DataRow[] drcomment = dtcomment.Select("VoyageID=" + gr.Cells[0].Text.Split('?')[1].Split('&')[0].Split('=')[1].Trim() + " and entry_type=" + dtEntryType.Rows[SalTypeii]["code"].ToString());
                        if (drcomment.Length > 0)
                        {
                            gr.Cells[i].ForeColor = System.Drawing.Color.Red;

                            gr.Cells[i].Attributes.Add("onmouseover", "js_ShowToolTip('" + drcomment[0][2].ToString() + "', event,this)");
                        }
                    }
                    fixedcolid++;
                    SalTypeii++;
                }

                if (gr.Cells[1].Text.Length > 25)
                {
                    gr.Cells[1].Attributes.Add("onmousemove", "js_ShowToolTip('" + gr.Cells[1].Text + "',event,this)");
                    gr.Cells[1].Text = gr.Cells[1].Text.Substring(0, 24) + "..";
                }
                if (gr.Cells[2].Text.Length > 8)
                {
                    gr.Cells[2].Attributes.Add("onmousemove", "js_ShowToolTip('" + gr.Cells[2].Text + "',event,this)");
                    gr.Cells[2].Text = gr.Cells[2].Text.Substring(0, 8) + "..";
                }
                fixedcolid = 0;


            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }

    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string encoded = e.Row.Cells[0].Text;
            e.Row.Cells[0].Text = Context.Server.HtmlDecode(encoded);

        }


    }

    protected void GridView1_DataBound(object s, EventArgs e)
    {
        try
        {
            //for (int j = 0; j <= startupdno; j++)
            //{
            //    GridViewPB.HeaderRow.Cells[j].CssClass = "hd";
            //}
            GridViewPB.HeaderRow.Cells[1].Width = 160;

            int fixedcolid = 0;

            var header = GridViewPB.FooterRow;
            header.Cells[0].Text = "Total";
            header.BackColor = System.Drawing.Color.LightYellow;
            header.ForeColor = System.Drawing.Color.Blue;
            header.Font.Bold = true;

            foreach (GridViewRow gr in GridViewPB.Rows)
            {
                gr.Cells[1].Width = 160;
                gr.Cells[totalEarnID].ForeColor = System.Drawing.Color.Navy;
                gr.Cells[totalDedID].ForeColor = System.Drawing.Color.Navy;
                gr.Cells[BFid].ForeColor = System.Drawing.Color.Purple;
                gr.Cells[totalEarnID].Font.Bold = true;
                gr.Cells[totalDedID].Font.Bold = true;
                gr.Cells[BFid].Font.Bold = true;

                int SalTypeii = 0;
                for (int i = startupdno + 1; i < gr.Cells.Count; i++)
                {
                    if (fixedcolid <= startupdno)
                    {
                        gr.Cells[fixedcolid].CssClass = "PbillHeaderStyle-css-fixed";

                    }


                    if (i >= startupdno)
                    {
                        decimal val = 0;
                        if (decimal.TryParse(gr.Cells[i].Text, out val))
                        {
                            gr.Cells[i].Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                            header.Cells[i].Text = string.Format("{0:0,0.00}", (UDFLib.ConvertToDecimal(header.Cells[i].Text) + val));
                            header.Cells[i].Style.Add(HtmlTextWriterStyle.TextAlign, "right");

                        }
                    }


                    ////////////////


                    if (SalTypeii < dtEntryType.Rows.Count)
                    {
                        string VoyageID = gr.Cells[0].Text;
                        DataRow[] drcomment = dtcomment.Select("VoyageID=" + gr.Cells[0].Text.Split('?')[1].Split('&')[0].Split('=')[1].Trim() + " and entry_type=" + dtEntryType.Rows[SalTypeii]["code"].ToString());
                        if (drcomment.Length > 0)
                        {
                            gr.Cells[i].ForeColor = System.Drawing.Color.Red;

                            gr.Cells[i].Attributes.Add("onmouseover", "js_ShowToolTip('" + drcomment[0][2].ToString() + "', event,this)");
                        }
                    }
                    fixedcolid++;
                    SalTypeii++;
                }

                if (gr.Cells[1].Text.Length > 25)
                {
                    gr.Cells[1].Attributes.Add("onmousemove", "js_ShowToolTip('" + gr.Cells[1].Text + "',event,this)");
                    gr.Cells[1].Text = gr.Cells[1].Text.Substring(0, 24) + "..";
                }

                fixedcolid = 0;


            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }

    }

    protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {


            string _SortOrderColumns = "DOA_HomePort , RANK_SORT_ORDER ";
            Dictionary<string, UDCHyperLink> DicLink = new Dictionary<string, UDCHyperLink>();
            lblPageTitle.Text = "Portage Bill : " + Convert.ToString(ViewState["Vessel_Name"]) + "&nbsp-&nbsp" + DateTime.Parse(ViewState["PBDt"].ToString()).ToString("MMM-yyyy");

            UDCHyperLink alink = new UDCHyperLink();
            alink.Target = "_blank";
            alink.NaviagteURL = "../crew/CrewDetails.aspx";
            alink.QueryStringDataColumn = new string[] { "VoyageID", "CrewID" };
            alink.QueryStringText = new string[] { "VoyageID", "ID" };
            DicLink.Add("Code", alink);

            DataSet dsPb = BLL_PB_PortageBill.Get_PortageBill(Convert.ToInt32(DateTime.Parse(ViewState["PBDt"].ToString()).Month), Convert.ToInt32(DateTime.Parse(ViewState["PBDt"].ToString()).Year), Convert.ToInt32(ViewState["Vessel_ID"]), UDFLib.ConvertStringToNull(null), 0);
            dtcomment = dsPb.Tables["Comment"];
            dtEntryType = dsPb.Tables["EntryType"];

            DataTable dtpb = UDFLib.PivotTable("Description",
                                                  "Amount",
                                                  "Sort_Order",
                                                  new string[] { "VOYAGEID", "PBMONTH", "PBYEAR" },
                                                  new string[] { "VOYAGEID", "PBMONTH", "PBYEAR", "RANK_SORT_ORDER", "IsFinalized", "DOA_HomePort", "RANK_SORT_ORDER", "Amount", "Description", "Sort_Order", "CrewID" },
                                                  _SortOrderColumns,
                                                  DicLink,
                                                  dsPb.Tables["Data"]
                                                  );
            totalEarnID = dtpb.Columns.IndexOf("Total Earning");
            totalDedID = dtpb.Columns.IndexOf("Total Deduction");
            BFid = dtpb.Columns.IndexOf("balance");
            dtpb.DefaultView.RowFilter = "rank <> ''";


            DataTable dt = new DataTable();
            dt = dtpb.DefaultView.ToTable();

            string[] HeaderCaptions = { };
            string[] DataColumnsName = { };

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Array.Resize(ref HeaderCaptions, HeaderCaptions.Length + 1); HeaderCaptions[HeaderCaptions.Length - 1] = dt.Columns[i].ColumnName;
                Array.Resize(ref DataColumnsName, DataColumnsName.Length + 1); DataColumnsName[DataColumnsName.Length - 1] = dt.Columns[i].ColumnName;
            }

            foreach (DataRow row in dt.Rows)
            {
                string st = row["Code"].ToString();

                int start = st.IndexOf('>');
                int end = st.IndexOf('<', start);
                st = st.Substring(start + 1, end - start - 1);

                row["Code"] = st;

            }
            UDFLib.ChangeColumnDataType(dt, "To", typeof(string));
            UDFLib.ChangeColumnDataType(dt, "From", typeof(string));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["From"] = "&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[i]["From"].ToString()), UDFLib.GetDateFormat());
                    dt.Rows[i]["To"] = "&nbsp;" + UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[i]["To"].ToString()), UDFLib.GetDateFormat());
                }
            }

            //RemoveLinks(GridViewPB);
            GridViewExportUtil.ExportToExcel(dt, HeaderCaptions, DataColumnsName, "PortageBill-" + Request.QueryString["arg"].Split('~')[2] + "-" + DateTime.Parse(Request.QueryString["arg"].Split('~')[1]).ToString("MMM-yyyy") + ".xls", "PortageBill-" + Request.QueryString["arg"].Split('~')[2] + "-" + DateTime.Parse(Request.QueryString["arg"].Split('~')[1]).ToString("MMM-yyyy"));
            //GridViewExportUtil.Export("PortageBill-" + Request.QueryString["arg"].Split('~')[2] + "-" + DateTime.Parse(Request.QueryString["arg"].Split('~')[1]).ToString("MMM-yyyy") + ".xls", GridView1);

            // GridView1.GridLines = GridLines.None;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnGeneratePortageBill_Click(object sender, EventArgs e)
    {

    }

    private void RemoveLinks(Control grdView)
    {
        HyperLink lb = new HyperLink();
        Literal l = new Literal();

        //for (int i = 0; i < grdView.Controls.Count; i++)
        //{


        if (grdView.Controls[0].GetType() == typeof(HyperLink))  // or hyperlink
        {
            l.Text = (grdView.Controls[1] as HyperLink).Text;
            grdView.Controls.Remove(grdView.Controls[0]);
            grdView.Controls.AddAt(0, l);
        }
        //if (grdView.Controls[i].HasControls())
        //{
        //   // RemoveLinks(grdView.Controls[i]);
        //   // grdView.Controls[0].Visible = false;
        //}
        //}

    }

    protected void btnFinalizePortageBill_Click(object sender, EventArgs e)
    {
        if ((BLL_PB_PortageBill.Upd_Finalize_PortageBill(UDFLib.ConvertToInteger(ViewState["Open_PB_Month"]), UDFLib.ConvertToInteger(ViewState["Open_PB_Year"]), Convert.ToInt32(ViewState["Vessel_ID"]), Convert.ToInt32(Session["UserID"]))) == -2)
        {
            String msg1 = String.Format("alert('Future month portage bill can not be finalized.');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowverifyrefresgclosehf", msg1, true);
        }
        else
        {
            btnFinalizePortageBill.Enabled = false;
            btnGeneratePortageBill.Enabled = false;
        }

    }
}