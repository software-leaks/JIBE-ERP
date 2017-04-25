using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using SMS.Business.PortageBill;

public partial class PortageBill_PhoneCard_VesselRequestDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");

        if (!IsPostBack)
        {
            try
            {
                cmdHiddenSubmit.Attributes.Add("style", "visibility:hidden");
                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;
                ucCustomPagerItems.PageSize = 20;
                if (Request.QueryString["ID"] != null)
                {
                    ViewState["REQUESTID"] = Request.QueryString["ID"].ToString();
                    ViewState["VESSELID"] = Request.QueryString["VESSELID"].ToString();
                    BindRequestInfo(ViewState["REQUESTID"].ToString(), Request.QueryString["VESSELID"].ToString());
                    BindViews();
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    private void BindRequestInfo(string requestid, string vesselid)
    {

        DataTable dt = BLL_PB_PhoneCard.PhoneCord_Request_Edit(int.Parse(requestid), int.Parse(vesselid));
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            lblRequestNumber.Text = dr["REQUEST_NUMBER"].ToString();
            lblVesselname.Text = dr["VESSEL_NAME"].ToString();
            lblRequestDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(dr["DATE_OF_CREATION"].ToString()).ToShortDateString());
            lblPortageBillDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToDateTime(dr["PBILL_DATE"].ToString()).ToShortDateString());
            lblTotalRequest.Text = dr["TOTAL_REQUEST"].ToString();
            lblStatus.Text = dr["REQUEST_STATUS"].ToString();
        }


    }

    public void BindViews()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;
            string requestid = ViewState["REQUESTID"].ToString();
            string vesselid = ViewState["VESSELID"].ToString();

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = BLL_PB_PhoneCard.PhoneCord_RequestItem_Search(int.Parse(requestid), int.Parse(vesselid), sortbycoloumn, sortdirection
                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            if (dt.Rows.Count > 0)
            {
                gvPhoneCardRequest.DataSource = dt;
                gvPhoneCardRequest.DataBind();
            }
            else
            {
                gvPhoneCardRequest.DataSource = dt;
                gvPhoneCardRequest.DataBind();
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void gvPhoneCardRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/Images/arrowUp.png";
                    else
                        img.Src = "~/Images/arrowDown.png";

                    img.Visible = true;
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
            ImageButton imgb = (ImageButton)e.Row.FindControl("cmdUpload");
            if (imgb != null)
            {
                Label lblid = (Label)e.Row.FindControl("lblItemId");
                if (lblid != null)
                {
                    //imgb.Attributes.Add("onClick", "javascript:window.open('../PhoneCard/AssigCard.aspx?ID=" + lblid.Text + "','_blank');return false");
                    //  imgb.Attributes.Add("onclick", "document.getElementById('ifUploadCard').src='../PhoneCard/AssignCard.aspx?ID=" + lblid.Text + "';showModal('divUploadCard');return false;");

                    // imgb.Attributes.Add("onclick", "OpenPopupWindow('POP__Task', 'Upload Card', AssignCard.aspx?ID=" + lblid.Text + "','popup',410,700,null,null,false,false,true,false,'" + cmdHiddenSubmit.UniqueID + "');return false;");
                }
            }
        }
    }
    protected void gvPhoneCardRequest_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["SORTBYCOLOUMN"] = e.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindViews();
    }
    protected void btnSaveEventEdit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCard.SelectedIndex > 0)
            {
                int res = BLL_PB_PhoneCard.PhoneCard_RequestItem_Update(int.Parse(ViewState["CREWID"].ToString()), int.Parse(ddlCard.SelectedValue), int.Parse(ViewState["vesselid"].ToString()), int.Parse(ViewState["voyageid"].ToString()), 1);
                string Deptmodal = String.Format("hideModal('divUploadCard',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divUploadCard", Deptmodal, true);
                cmdHiddenSubmit_Click(null, null);
            }
        }
        catch (Exception ex)
        {
        }

    }
    protected void gvPhoneCardRequest_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnCloseEventEdit_Click(object sender, EventArgs e)
    {
        string Deptmodal = String.Format("hideModal('divUploadCard',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divUploadCard", Deptmodal, true);
    }
    protected void onUpdate(object source, CommandEventArgs e)
    {
        try
        {

            DataTable dt = BLL_PB_PhoneCard.PhoneCord_RequestItem_Edit(int.Parse(e.CommandArgument.ToString()));
            ViewState["CREWID"] = e.CommandArgument.ToString();
            txtCrewName.Text = dt.Rows[0]["STAFF_NAME"].ToString();
            txtRequestUnit.Text = dt.Rows[0]["CARD_UNITS"].ToString();
            ViewState["vesselid"] = dt.Rows[0]["VESSEL_ID"].ToString();
            ViewState["voyageid"] = dt.Rows[0]["VOYAGEID"].ToString();
            string Deptmodal = String.Format("showModal('divUploadCard',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "divUploadCard", Deptmodal, true);
        }
        catch (Exception ex)
        {
        }
    }

    protected void cmdHiddenSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            BindViews();
        }
        catch (Exception ex)
        {
        }

    }
}