using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
public partial class Infrastructure_Snippets_Dash_Decklog_Anomalies : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds = BLL_Infra_DashBoard.Get_Decklog_Anomalies(Convert.ToInt32(Session["USERID"]));
        gvDecklogAnomalies.DataSource = ds;
        ViewState["rowcnt"] = ds.Tables[0].Rows.Count - 1;
        gvDecklogAnomalies.DataBind();

    }


   

    protected void gvDecklogAnomalies_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = DateTime.Now.ToString("dd/MMM/yyyy");
            e.Row.Cells[2].Text = DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy");
            e.Row.Cells[3].Text = DateTime.Now.AddDays(-2).ToString("dd/MMM/yyyy");
            e.Row.Cells[4].Text = DateTime.Now.AddDays(-3).ToString("dd/MMM/yyyy");
            e.Row.Cells[5].Text = DateTime.Now.AddDays(-4).ToString("dd/MMM/yyyy");
            e.Row.Cells[6].Text = DateTime.Now.AddDays(-5).ToString("dd/MMM/yyyy");
            e.Row.Cells[7].Text = DateTime.Now.AddDays(-6).ToString("dd/MMM/yyyy");


        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].BorderStyle = BorderStyle.Dashed;
            e.Row.Cells[1].BorderWidth = 1;
            e.Row.Cells[2].BorderStyle = BorderStyle.Dashed;
            e.Row.Cells[2].BorderWidth = 1;
            e.Row.Cells[3].BorderStyle = BorderStyle.Dashed;
            e.Row.Cells[3].BorderWidth = 1;
            e.Row.Cells[4].BorderStyle = BorderStyle.Dashed;
            e.Row.Cells[4].BorderWidth = 1;
            e.Row.Cells[5].BorderStyle = BorderStyle.Dashed;
            e.Row.Cells[5].BorderWidth = 1;
            e.Row.Cells[6].BorderStyle = BorderStyle.Dashed;
            e.Row.Cells[6].BorderWidth = 1;
            e.Row.Cells[7].BorderStyle = BorderStyle.Dashed;
            e.Row.Cells[7].BorderWidth = 1;
            try
            {
                if (Convert.ToInt32(e.Row.Cells[1].Text.ToString().Trim()) > 0)
                {

                    e.Row.Cells[1].CssClass = "AnomalyCell";


                }
                if (Convert.ToInt32(e.Row.Cells[1].Text.ToString().Trim()) == 0)
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Gray;
                }
            }
            catch (Exception)
            {


            }
            e.Row.Cells[1].Text = "   ";


            try
            {
                if (Convert.ToInt32(e.Row.Cells[2].Text.ToString().Trim()) > 0)
                {

                    e.Row.Cells[2].CssClass = "AnomalyCell";


                }
                if (Convert.ToInt32(e.Row.Cells[2].Text.ToString().Trim()) == 0)
                {
                    e.Row.Cells[2].CssClass = "NoAnomaly";
                }
            }
            catch (Exception)
            {


            }
            e.Row.Cells[2].Text = "   ";

            try
            {
                if (Convert.ToInt32(e.Row.Cells[3].Text.ToString().Trim()) > 0)
                {

                    e.Row.Cells[3].CssClass = "AnomalyCell";


                }
                if (Convert.ToInt32(e.Row.Cells[3].Text.ToString().Trim()) == 0)
                {
                    e.Row.Cells[3].CssClass = "NoAnomaly";
                }
            }
            catch (Exception)
            {


            }
            e.Row.Cells[3].Text = "   ";



            try
            {
                if (Convert.ToInt32(e.Row.Cells[4].Text.ToString().Trim()) > 0)
                {

                    e.Row.Cells[4].CssClass = "AnomalyCell";


                }
                if (Convert.ToInt32(e.Row.Cells[4].Text.ToString().Trim()) == 0)
                {
                    e.Row.Cells[4].CssClass = "NoAnomaly";
                }
            }
            catch (Exception)
            {


            }
            e.Row.Cells[4].Text = "   ";



            try
            {
                if (Convert.ToInt32(e.Row.Cells[5].Text.ToString().Trim()) > 0)
                {

                    e.Row.Cells[5].CssClass = "AnomalyCell";


                }
                if (Convert.ToInt32(e.Row.Cells[5].Text.ToString().Trim()) == 0)
                {
                    e.Row.Cells[5].CssClass = "NoAnomaly";
                }
            }
            catch (Exception)
            {


            }
            e.Row.Cells[5].Text = "   ";


            try
            {
                if (Convert.ToInt32(e.Row.Cells[6].Text.ToString().Trim()) > 0)
                {

                    e.Row.Cells[6].CssClass = "AnomalyCell";


                }
                if (Convert.ToInt32(e.Row.Cells[6].Text.ToString().Trim()) == 0)
                {
                    e.Row.Cells[6].CssClass = "NoAnomaly";
                }
            }
            catch (Exception)
            {


            }
            e.Row.Cells[6].Text = "   ";


            try
            {
                if (Convert.ToInt32(e.Row.Cells[7].Text.ToString().Trim()) > 0)
                {

                    e.Row.Cells[7].CssClass = "AnomalyCell";


                }
                if (Convert.ToInt32(e.Row.Cells[7].Text.ToString().Trim()) == 0)
                {
                    e.Row.Cells[7].CssClass = "NoAnomaly";
                }
            }
            catch (Exception)
            {


            }
            e.Row.Cells[7].Text = "   ";






 

        }
    }
}