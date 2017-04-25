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
using SMS.Business.PURC;

using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Globalization;

public partial class POList : System.Web.UI.Page
{
    // BLL_Infra_UploadFileSize objBLL = new BLL_Infra_UploadFileSize();
    BLL_PURC_LOG objBllPurc = new BLL_PURC_LOG();
    UserAccess objUA = new UserAccess();

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;
            ucCustomPagerItems.PageSize = 15;      
            BindPOList();
            BindVessels();
            //  BindSuppliers();
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

        if (objUA.Add == 0) ImgAdd.Visible = false;
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            btnsave.Visible = false;
        if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public void BindPOList()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBllPurc.SearchPOList(txtfilter.Text != "" ? txtfilter.Text : null, sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
           
        }


        if (dt.Rows.Count > 0)
        {
            gvPOList.DataSource = dt;
            gvPOList.DataBind();
        }
        else
        {
            gvPOList.DataSource = dt;
            gvPOList.DataBind();
        }

    }

    protected void ImgAdd_Click(object sender, EventArgs e)
    {

        this.SetFocus("ctl00_MainContent_txtPoNo");
        HiddenFlag.Value = "Add";
        OperationMode = "Add PO ";
        txtfilter.Text = "";
        txtPOAmt.Text = "";
        txtPoNo.Text = "";
        txtSupplier.Text = "";
        dtOrderDate.Text = "";
        ddlVessel.SelectedIndex = 0;

        string AddPoListmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPoListmodal", AddPoListmodal, true);
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        DateTime? dtOrder = null;// Convert.ToDateTime("9999-01-01");
        
        if (HiddenFlag.Value == "Add")
        {
            if (dtOrderDate.Text.Trim() != "")
            {
                try
                {
                    dtOrder = Convert.ToDateTime(dtOrderDate.Text.Trim());
                }
                catch (Exception)
                {
                    string js = " Please enter a valid Order Date !";
                    string msgdivResponseShow = string.Format("alert('" + js + "');showModal('divadd',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
                    return;
                }
               
            }
            int responseid = objBllPurc.InsertPONo(txtPoNo.Text.Trim(), Convert.ToDecimal(txtPOAmt.Text), Convert.ToInt32(ddlVessel.SelectedValue), txtSupplier.Text.Trim(), UDFLib.ConvertDateToNull(dtOrderDate.Text.Trim()), Convert.ToInt32(Session["USERID"]));
            if (responseid == 1)
            {
                string js = " Po Number added successfully...!";
                string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            }
            else
            {
                string js = " Po Number allready exists ...!";
                string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            }
        }
        else
        {
            int id = 0;
            if (HiddenFieldID.Value != "")
                id = Convert.ToInt32(HiddenFieldID.Value);
            int responseid = objBllPurc.EditPONo(id, txtPoNo.Text.Trim(), Convert.ToDecimal(txtPOAmt.Text), Convert.ToInt32(ddlVessel.SelectedValue), txtSupplier.Text.Trim(), UDFLib.ConvertDateToNull(dtOrderDate.Text.Trim()), Convert.ToInt32(Session["USERID"]));

            if (responseid == 1)
            {
                string js = " Updated successfully...!";
                string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            }
            else
            {
                string js = " Po Number allready exists ...!";
                string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            }

        }

        BindPOList();
        string hidemodal = String.Format("hideModal('divadd')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        

        pnlItems.Visible = false;
        if (e.CommandName == "OpenItems")
        {
           
            HiddenFieldID.Value = commandArgs[0];
            HiddenField_PONo.Value = commandArgs[1];
            lblPONO.Text = "PO Number : " + commandArgs[1].ToString();
            BindPOItems(Convert.ToInt32(HiddenFieldID.Value));
            string editSalStrmodal = String.Format("showModal('divItems',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editSalStrmodal", editSalStrmodal, true);
        }
        else if (e.CommandName == "EditItems")
        {
            string[] CommandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            HiddenFieldItemID.Value = CommandArgs[0];
            txtIem_Desc.Text = CommandArgs[1];
            txtItemPrice.Text = CommandArgs[2];
            txtItem_Qtn.Text = CommandArgs[3];
            txtItem_unit.Text = CommandArgs[4];
            pnlItems.Visible = true;
            HiddenFlag.Value = "EditItems";
            lblPONO.Text = "PO Number : " + HiddenField_PONo.Value;
            OperationMode = "Edit PO Items ";
            string editSalStrmodal = String.Format("showModal('divItems',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editSalStrmodal", editSalStrmodal, true);

        }
        else
        {
            HiddenFieldID.Value = commandArgs[0];
            HiddenField_PONo.Value = commandArgs[1];
            HiddenFlag.Value = "Edit";
            OperationMode = "Edit PO";
            BindVessels();
            DataTable dt = new DataTable();
            dt = objBllPurc.Get_POnumberByID(Convert.ToInt32(HiddenFieldID.Value));
            // dt.DefaultView.RowFilter = "ID= '" + e.CommandArgument.ToString() + "'";

            //  HiddenFieldID.Value = e.CommandArgument.ToString();
            if (dt.Rows.Count != 0)
            {
                txtPoNo.Text = dt.DefaultView[0]["PO_Number"].ToString();
                txtPOAmt.Text = dt.DefaultView[0]["PO_Amount"].ToString();
                txtSupplier.Text = dt.DefaultView[0]["Supplier_name"].ToString();
                dtOrderDate.Text = dt.DefaultView[0]["Order_Date"].ToString();
                ddlVessel.SelectedValue = dt.Rows[0]["Vessel_ID"].ToString() != "" ? dt.Rows[0]["Vessel_ID"].ToString() : "0"; ;

                string editSalStrmodal = String.Format("showModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editSalStrmodal", editSalStrmodal, true);
            }
        }

    }

    public void BindPOItems(int _pOID)
    {
       
        OperationMode = "PO Items ";
        lblPONO.Text = "PO Number : " + HiddenField_PONo.Value;
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBllPurc.GetPOItems(_pOID);
        //, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        //if (ucCustomPagerItems.isCountRecord == 1)
        //{
        //    ucCustomPagerItems.CountTotalRec = rowcount.ToString();
        //    ucCustomPagerItems.BuildPager();
        //}


        if (dt.Rows.Count > 0)
        {
            gvPoItems.DataSource = dt;
            gvPoItems.DataBind();
        }
        else
        {
            gvPoItems.DataSource = dt;
            gvPoItems.DataBind();
        }
        BindPOList();

    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        if (e.CommandName == "ItemsDelete")
        {
            int retval1 = objBllPurc.DeletePoItem(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
            BindPOItems(Convert.ToInt32(HiddenFieldID.Value));
            string AddPoListmodal = String.Format("showModal('divItems',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPoListmodal", AddPoListmodal, true);
        }
        else
        {
            int retval = objBllPurc.DeletePoNumbe(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
            BindPOList();
        }

    } 

    public void BindVessels()
    {
        try
        {
            DataTable dt = objBllPurc.get_Vessels();
            ddlVessel.Items.Clear();
            ddlVessel.DataSource = dt;
            ddlVessel.DataTextField = "VesselName";
            ddlVessel.DataValueField = "Vessel_ID";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
        catch (Exception)
        {
        }
    }

    protected void gvPOList_RowDataBound(object sender, GridViewRowEventArgs e)
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

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
        }

    }

    protected void gvPOList_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindPOList();
    }

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        BindPOList();
    }

    protected void btnsaveItem_Click(object sender, EventArgs e)
    {

        int qnt = 0;
        if (txtItem_Qtn.Text.Trim() != "")
            qnt = Convert.ToInt32(txtItem_Qtn.Text.Trim());
        if (HiddenFlag.Value == "EditItems")
        {
            int responseid = objBllPurc.UpdatePOItem(Convert.ToInt32(HiddenFieldItemID.Value), txtIem_Desc.Text.Trim(), Convert.ToDecimal(txtItemPrice.Text.Trim()), qnt, txtItem_unit.Text.Trim(), Convert.ToInt32(Session["USERID"]));
            if (responseid == 1)
            {
                string js = " Po Item updated successfully...!";
                string msgdivResponseShow = string.Format("alert('" + js + "');showModal('divItems',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            }
            else
            {
                string js = " Po Item allready exists ...!";
                string msgdivResponseShow = string.Format("alert('" + js + "');showModal('divItems',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            }

        }
        else
        {
            int responseid = objBllPurc.InsertPOItem(Convert.ToInt32(HiddenFieldID.Value), txtIem_Desc.Text.Trim(), Convert.ToDecimal(txtItemPrice.Text.Trim()), qnt, txtItem_unit.Text.Trim(), Convert.ToInt32(Session["USERID"]));
            if (responseid == 1)
            {
                string js = " Po Item added successfully...!";
                string msgdivResponseShow = string.Format("alert('" + js + "');showModal('divItems',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            }
            else
            {
                string js = " Po Item allready exists ...!";
                string msgdivResponseShow = string.Format("alert('" + js + "');showModal('divItems',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
            }
        }
          BindPOItems(Convert.ToInt32(HiddenFieldID.Value));


    }


    protected void btnItemAdd_Click(object sender, EventArgs e)
    {
        OperationMode = "Add PO Items ";
        lblPONO.Text = "PO Number : " + HiddenField_PONo.Value;
        this.SetFocus("ctl00_MainContent_txtPoNo");
        HiddenFlag.Value = "Add";
        //  OperationMode = "Edit Items of PO No : " + commandArgs[1].ToString(); 
        txtIem_Desc.Text = "";
        txtItem_Qtn.Text = "";
        txtItem_unit.Text = "";
        txtItemPrice.Text = "";
        pnlItems.Visible = true;

        string AddPoListmodal = String.Format("showModal('divItems',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPoListmodal", AddPoListmodal, true);
    }


    protected void btnItemRefresh_Click(object sender, EventArgs e)
    {
        txtIem_Desc.Text = "";
        txtItem_Qtn.Text = "";
        txtItem_unit.Text = "";
        txtItemPrice.Text = "";
        pnlItems.Visible = false;
        BindPOItems(Convert.ToInt32(HiddenFieldID.Value));
        string AddPoListmodal = String.Format("showModal('divItems',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddPoListmodal", AddPoListmodal, true);

    }


    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        txtfilter.Text = "";
        BindPOList();
    }
}