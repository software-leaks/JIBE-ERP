using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Operation;
using System.Data;
using SMS.Business.Operations;

public partial class Operations_PortInfo : System.Web.UI.Page
{
    int _Vessel_Id = 0;
    int _PortInfoReportId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (Request.QueryString["VesselId"] != null && Request.QueryString["PortInfoReportId"] != null)
        {
            _Vessel_Id = int.Parse(Request.QueryString["VesselId"].ToString());
            _PortInfoReportId = int.Parse(Request.QueryString["PortInfoReportId"].ToString());

            DataSet ds = BLL_OPS_VoyageReports.Get_PortInfoReport(_Vessel_Id, _PortInfoReportId);
            fvPortInfoReport.DataSource = ds.Tables[0];
            fvPortInfoReport.DataBind();


            DataSet ds1 = BLL_OPS_VoyageReports.Get_PilotInfo(_Vessel_Id, _PortInfoReportId);
            gvPilotInfo.DataSource = ds1.Tables[0];
            gvPilotInfo.DataBind();

            DataTable dt = BLL_OPS_VoyageReports.Get_PRT_Attachment(1, _PortInfoReportId, _Vessel_Id); 
            imgListRight.DataSource = dt;
            imgListRight.DataBind(); 
        }
    }
    protected void onView(object source, CommandEventArgs e)
    {
        string[] cmdargs = e.CommandArgument.ToString().Split(',');

        int id = int.Parse(cmdargs[0].ToString());
        int Vessel_Id = int.Parse(cmdargs[1].ToString());

        DataSet ds = BLL_OPS_VoyageReports.Get_PilotDetailInfo(id, Vessel_Id);
        lblPilotName.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
        lblPilotremarks.Text = ds.Tables[0].Rows[0]["Pilot_Name"].ToString();

        dgPilotDetails.DataSource = ds.Tables[1];
        dgPilotDetails.DataBind();

        string msgdivResponseShow = string.Format("showModal('divPilotInfo',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdiv", msgdivResponseShow, true);
    
    }
    protected string GetImage(string ImgName)
    {
        if (System.IO.Path.GetExtension(ImgName) == ".bmp" || System.IO.Path.GetExtension(ImgName) == ".png" || System.IO.Path.GetExtension(ImgName) == ".jpg" || System.IO.Path.GetExtension(ImgName) == ".gif")
        {
            return ImgName;

        }
        else
        {
            return "~/Images/DocTree/" + System.IO.Path.GetExtension(ImgName).Replace(".", "") + ".png";
        }
    }
    protected void dgPilotDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int ID = UDFLib.ConvertToInteger(dgPilotDetails.DataKeys[e.Row.RowIndex].Value.ToString());

            RadioButtonList rdo = (RadioButtonList)e.Row.FindControl("rdoOptions");
            BLL_OPS_Admin OPS_Admin = new BLL_OPS_Admin();
            DataTable dt = OPS_Admin.Get_GradingOptions(ID);

            rdo.DataSource = dt;
            rdo.DataBind();

            int GRADINGOPTION_ID = UDFLib.ConvertToInteger(DataBinder.Eval(e.Row.DataItem, "GRADINGOPTION_ID").ToString());

            rdo.SelectedValue = GRADINGOPTION_ID.ToString();
        }
    }
 }