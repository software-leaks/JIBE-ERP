using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.PURC;
using SMS.Properties;
using SMS.Business.Crew;
using System.IO;


public partial class PurchaseItemLocation : System.Web.UI.Page
{

    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();
    public string OperationMode = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["ID"] = null;
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;
            LoadPLocation();
            BindLocation();
        }

        //UserAccessValidation();

    }





    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);

        if (objUA.View == 0)
        {

            Response.Redirect("~/crew/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {

        }
    }



    public void BindLocation()
    {
        int rowcount = 1;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());



        DataTable dt = BLL_PURC_ItemLocation.GetLocation_Search(UDFLib.ConvertIntegerToNull(ddlPLocation.SelectedValue), txtSearchBy.Text, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        gvLocation.DataSource = dt;
        gvLocation.DataBind();

        ucCustomPagerItems.CountTotalRec = rowcount.ToString();
        ucCustomPagerItems.BuildPager();



    }

    public void LoadPLocation()
    {

        DataTable dt = BLL_PURC_ItemLocation.GetParentLocation();

        ddlPLocation.DataSource = dt;
        ddlPLocation.DataTextField = "Parent_Name";
        ddlPLocation.DataValueField = "Location_ID";
        ddlPLocation.DataBind();
        ddlPLocation.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlPLocation.SelectedIndex = 0;

        ddlAddLocation.DataSource = dt;
        ddlAddLocation.DataTextField = "Parent_Name";
        ddlAddLocation.DataValueField = "Location_ID";
        ddlAddLocation.DataBind();
        ddlAddLocation.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlAddLocation.SelectedIndex = 0;
    }



    protected void gvLocation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.color='blue';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";

        }
    }

    protected void gvLocation_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            ImageButton ImgFBMAtt = (ImageButton)e.Row.FindControl("ImgFBMAtt");
            Label lblUserID = (Label)e.Row.FindControl("lblUserID");


        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "../../purchase/Image/arrowUp.png";

                    else
                        img.Src = "../../purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }

    }



    protected void gvLocation_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindLocation();


    }




    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = BLL_PURC_ItemLocation.DeleteLocation(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"].ToString()));
        BindLocation();
        UpdPnlGrid.Update();

    }

    protected void btnRetrieve_Click(object sender, EventArgs e)
    {

        BindLocation();
        UpdPnlGrid.Update();
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {


        ViewState["SORTDIRECTION"] = null;
        ViewState["SORTBYCOLOUMN"] = null;

        txtSearchBy.Text = "";

        ddlPLocation.SelectedValue = "0";

        BindLocation();
        UpdPnlGrid.Update();
    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());



        DataTable dt = BLL_PURC_ItemLocation.GetLocation_Search(UDFLib.ConvertIntegerToNull(ddlPLocation.SelectedValue), txtSearchBy.Text, sortbycoloumn, sortdirection, null, null, ref  rowcount);



        string[] HeaderCaptions = { "Parent Name", "Location Name" };
        string[] DataColumnsName = { "Parent_Name", "Location_Name" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "Item Location", "Item Location");


    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {

        HiddenFlag.Value = "Add";
        OperationMode = "Add Location";

        txtLocation.Text = "";
        ddlAddLocation.SelectedIndex = 0;
        ddlAddLocation.Enabled = true;

        string AddCountrymodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddCountrymodal", AddCountrymodal, true);
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {


        if (HiddenFlag.Value == "Add")
        {
            int responseid = BLL_PURC_ItemLocation.InsertLocation(Convert.ToInt32(ddlPLocation.SelectedValue), txtLocation.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            int responseid = BLL_PURC_ItemLocation.UpdateLocation(Convert.ToInt32(txtLocationID.Text), Convert.ToInt32(ddlPLocation.SelectedValue), txtLocation.Text.Trim(), Convert.ToInt32(Session["USERID"]));

        }

        BindLocation();

        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }




    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Location";

        DataTable dt = new DataTable();
        dt = BLL_PURC_ItemLocation.EditLocation(Convert.ToInt32(e.CommandArgument.ToString()));
        if (dt.Rows.Count > 0)
        {

            txtLocationID.Text = dt.Rows[0]["Location_ID"].ToString();
            txtLocation.Text = dt.Rows[0]["Location_Name"].ToString();
            ddlAddLocation.SelectedValue = dt.Rows[0]["Parent_ID"].ToString();
            ddlAddLocation.Enabled = false;
        }

        string Countrymodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Countrymodal", Countrymodal, true);
    }



    protected void ddlPLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindLocation();
        UpdPnlGrid.Update();
    }
    protected void txtSearchBy_TextChanged(object sender, EventArgs e)
    {
        BindLocation();
        UpdPnlGrid.Update();
    }
}