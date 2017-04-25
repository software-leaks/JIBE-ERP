using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using SMS.Business.Infrastructure;
using SMS.Business.VM;
using SMS.Business.CP;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class VesselMovement_PortCallTemplate : System.Web.UI.Page
{
    public UserAccess objUA = new UserAccess();
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_VM_PortCall objPortCall = new BLL_VM_PortCall();
    BLL_CP_CharterParty oBLL_CP = new BLL_CP_CharterParty();
    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
    public string Vessel_ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Vessel_ID"].ToString()))
            {
                Vessel_ID = Request.QueryString["Vessel_ID"].ToString();
                ltHeader.Text = "TEMPLATE [" + Request.QueryString["VName"] + "]";
                BindPortTemplate();
            }
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

        if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public int GetVesselID()
    {
        try
        {
           
            if (Request.QueryString["Vessel_ID"] != null)
            {
                return int.Parse(Request.QueryString["Vessel_ID"].ToString());
            }

            else
                return 0;
        }
        catch { return 0; }
    }
    private void BindPortTemplate()
    {
        DataTable dt = objPortCall.Get_PortCallTemplate(UDFLib.ConvertIntegerToNull(Request.QueryString["Vessel_ID"].ToString()));

        if (dt.Rows.Count > 0)
        {

            gvTemplate.DataSource = dt;
            gvTemplate.DataBind();
            gvTemplate.Visible = true;

        }
        else
        {
            gvTemplate.DataSource = dt;
            gvTemplate.DataBind();
        }

    }
    protected void gvTemplate_RowEditing(object sender, GridViewEditEventArgs de)
    {
        GridView _gridView = (GridView)sender;

        _gridView.EditIndex = de.NewEditIndex;
        _gridView.SelectedIndex = de.NewEditIndex;
        BindPortTemplate();
        int VesselID = GetVesselID();
        GridViewRow row = gvTemplate.Rows[de.NewEditIndex];
        DropDownList ddlCAgent = (DropDownList)(row.FindControl("ddlCAgent"));
        DropDownList ddlOAgent = (DropDownList)(row.FindControl("ddlOAgent"));
        Label lblCagent = (Label)(row.FindControl("lblc"));
        Label lblOagent = (Label)(row.FindControl("lblo"));
        DataSet ds = objPortCall.Get_PortCall_AgentList(UDFLib.ConvertToInteger(VesselID));
        DataTable dtcharterer = oBLL_CP.GetAgent_List();
        ddlCAgent.DataSource = dtcharterer;
        ddlCAgent.DataTextField = "Supplier_Name";
        ddlCAgent.DataValueField = "SUPPLIER_CODE";
        ddlCAgent.DataBind();
        ddlCAgent.Items.Insert(0, new ListItem("-Select-", "0"));

        DataTable dtOwnerAgent = oBLL_CP.GetAgent_List();
        ddlOAgent.DataSource = dtOwnerAgent;
        ddlOAgent.DataTextField = "Supplier_Name";
        ddlOAgent.DataValueField = "SUPPLIER_CODE";
        ddlOAgent.DataBind();
        ddlOAgent.Items.Insert(0, new ListItem("-Select-", "0"));

        ddlCAgent.SelectedValue = lblCagent.Text.ToString();
        ddlOAgent.SelectedValue = lblOagent.Text.ToString();
        DropDownList ddlDD = (DropDownList)(row.FindControl("ddlDD"));
        DropDownList ddlHH = (DropDownList)(row.FindControl("ddlHH"));
        DropDownList ddlMM = (DropDownList)(row.FindControl("ddlMM"));
        DropDownList ddlDDI = (DropDownList)(row.FindControl("ddlDDI"));
        DropDownList ddlHHI = (DropDownList)(row.FindControl("ddlHHI"));
        DropDownList ddlMMI = (DropDownList)(row.FindControl("ddlMMI"));
        LoadDay(ddlDD);
        LoadHour(ddlHH);
        LoadMin(ddlMM);
        LoadDay(ddlDDI);
        LoadHour(ddlHHI);
        LoadMin(ddlMMI);
        HiddenField hdnf1 = (HiddenField)(row.FindControl("txtSeaTime"));
        HiddenField hdnf2 = (HiddenField)(row.FindControl("txtInportTime"));
        ddlDD.SelectedValue = hdnf1.Value.Split('/')[0].ToString();
        ddlHH.SelectedValue = hdnf1.Value.Split('/')[1].ToString().Substring(0, 2);
        ddlMM.SelectedValue = hdnf1.Value.Split('/')[1].ToString().Substring(2, 2);
        ddlDDI.SelectedValue = hdnf2.Value.Split('/')[0].ToString();
        ddlHHI.SelectedValue = hdnf2.Value.Split('/')[1].ToString().Substring(0, 2);
        ddlMMI.SelectedValue = hdnf2.Value.Split('/')[1].ToString().Substring(2, 2);
    }
    protected void gvTemplate_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView _gridView = (GridView)sender;
        _gridView.EditIndex = -1;
        BindPortTemplate();
    }
    protected void gvTemplate_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        GridView _gridView = (GridView)sender;
        int nCurrentRow = e.RowIndex;
        _gridView.EditIndex = -1;
        BindPortTemplate();
    }
    protected void gvTemplate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Vessel_ID = Request.QueryString["Vessel_ID"].ToString();
        try
        {
            GridView _gridView = (GridView)sender;
            int nCurrentRow = Int32.Parse(e.CommandArgument.ToString());
            int id = Convert.ToInt16(_gridView.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString());
            if (e.CommandName.ToUpper().Equals("UPDATE"))
            {
                //TextBox txtSeaTime = (TextBox)_gridView.Rows[nCurrentRow].FindControl("txtSeaTime");
             //   TextBox txtInportTime = (TextBox)_gridView.Rows[nCurrentRow].FindControl("txtInportTime");
                DropDownList ddlDD = (DropDownList)_gridView.Rows[nCurrentRow].FindControl("ddlDD");
                DropDownList ddlHH = (DropDownList)_gridView.Rows[nCurrentRow].FindControl("ddlHH");
                DropDownList ddlMM = (DropDownList)_gridView.Rows[nCurrentRow].FindControl("ddlMM");
                DropDownList ddlDDI = (DropDownList)_gridView.Rows[nCurrentRow].FindControl("ddlDDI");
                DropDownList ddlHHI = (DropDownList)_gridView.Rows[nCurrentRow].FindControl("ddlHHI");
                DropDownList ddlMMI = (DropDownList)_gridView.Rows[nCurrentRow].FindControl("ddlMMI");
                DropDownList ddlCAgent = (DropDownList)_gridView.Rows[nCurrentRow].FindControl("ddlCAgent");
                DropDownList ddlOAgent = (DropDownList)_gridView.Rows[nCurrentRow].FindControl("ddlOAgent");
             
                Label ToPort = (Label)(_gridView.Rows[nCurrentRow].FindControl("ToPort"));
                Label FromPort = (Label)(_gridView.Rows[nCurrentRow].FindControl("FromPort"));
                char[] delimiterChars = { '/' };

                string seatime = ddlDD.SelectedValue + "/" + ddlHH.SelectedValue + ddlMM.SelectedValue;
                string porttime = ddlDDI.SelectedValue + "/" + ddlHHI.SelectedValue + ddlMMI.SelectedValue;
                string[] Seatime1 = seatime.Split(delimiterChars);
                string[] porttime1 = porttime.Split(delimiterChars);
                objPortCall.Upd_PortCall_Template_Details(Convert.ToInt32(id), Convert.ToInt32(Vessel_ID), FromPort.Text, ToPort.Text, seatime, porttime, UDFLib.ConvertStringToNull(ddlCAgent.SelectedValue),
                    UDFLib.ConvertStringToNull(ddlOAgent.SelectedValue), Convert.ToInt32(GetSessionUserID()));

            }
            if (e.CommandName.ToUpper().Equals("Cancel"))

            {
              
                _gridView.EditIndex = -1;
                BindPortTemplate();
              
              
            }
            BindPortTemplate();

           
        }
        catch(Exception ex)
        {
            string err = ex.ToString();
        }
    }

    protected void gvTemplate_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32((int)gvTemplate.DataKeys[e.RowIndex].Value);
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            objPortCall.Del_Port_Call_Template(UDFLib.ConvertIntegerToNull(id), UDFLib.ConvertIntegerToNull(GetSessionUserID()));
            BindPortTemplate();
        }
        else
        {
            GridView _gridView = (GridView)sender;
            int nCurrentRow = e.RowIndex;
            _gridView.EditIndex = -1;
            BindPortTemplate();
        }

    }

    public void LoadDay(DropDownList ddlDD)
    {
        ListItemCollection lic = new ListItemCollection();
        int count = 0;
        while (count < 100)
        {
            ListItem li = new ListItem();
            if (count < 10)
            {
                li.Text = "0" + count.ToString();
                li.Value = "0" + count.ToString();
            }
            else
            {
                li.Text = count.ToString();
                li.Value = count.ToString();
            }
            lic.Add(li);
            count++;

        }
        ddlDD.DataSource = lic;
        ddlDD.DataBind();

    }

    public void LoadHour(DropDownList ddlHH)
    {
        ListItemCollection lic = new ListItemCollection();
        int count = 0;
        while (count < 24)
        {
            ListItem li = new ListItem();
            if (count < 10)
            {
                li.Text = "0" + count.ToString();
                li.Value = "0" + count.ToString();
            }
            else
            {
                li.Text = count.ToString();
                li.Value = count.ToString();
            }
            lic.Add(li);
            count++;

        }
        ddlHH.DataSource = lic;
        ddlHH.DataBind();

    }

    public void LoadMin(DropDownList ddlMin)
    {
        ListItemCollection lic = new ListItemCollection();
        int count = 0;
        while (count < 60)
        {
            ListItem li = new ListItem();
            if (count < 10)
            {
                li.Text = "0" + count.ToString();
                li.Value = "0" + count.ToString();
            }
            else
            {
                li.Text = count.ToString();
                li.Value = count.ToString();
            }
            lic.Add(li);
            count++;

        }
        ddlMin.DataSource = lic;
        ddlMin.DataBind();
    }

}