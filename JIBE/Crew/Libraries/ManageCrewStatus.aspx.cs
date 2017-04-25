using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using SMS.Properties;
using SMS.Business.PortageBill;


public partial class Crew_Libraries__ManageCrewStatus : System.Web.UI.Page
{

    BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    public Boolean uaAddEntry = true;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserAccessValidation();
            if (!IsPostBack)
            {
                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;

                BindMainStatus();
                BindCalculatedStatus();
                BindStatusStructure();
                BindJoiningType();
            }
        }
        catch (Exception)
        {
        }

    }


    public void BindMainStatus()
    {
        try
        {
            DataTable dt = objCrewAdmin.get_MainStatus();
            ddlMainStatus.Items.Clear();
            ddlMainStatus.DataSource = dt;
            ddlMainStatus.DataTextField = "Name";
            ddlMainStatus.DataValueField = "Id";
            ddlMainStatus.DataBind();
            ddlMainStatus.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
        catch (Exception)
        {
        }
    }

    private void BindJoiningType()
    {
        try
        {
        DataTable dt = objCrewAdmin.get_JoiningTypeList();
        chkJoiningType.Items.Clear();
        chkJoiningType.DataSource = dt;
        chkJoiningType.DataTextField = "Joining_Type";
        chkJoiningType.DataValueField = "Id";
        chkJoiningType.DataBind();
        }
        catch (Exception)
        { }
    }


    private void BindCalculatedStatus()
    {
        try
        {
        DataTable dt = objCrewAdmin.get_Calc_StatuList();
        chkCalc_Status.Items.Clear();
        chkCalc_Status.DataSource = dt;
        chkCalc_Status.DataValueField = "Id";
        chkCalc_Status.DataTextField = "Name";
        chkCalc_Status.DataBind();
        }
        catch (Exception)
        {
        }
    }

    public void BindStatusStructure()
    {
        try
        {
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataSet ds = objCrewAdmin.SearchStatusStructure(txtfilter.Text.Trim());
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvStatusStructure.DataSource = ds.Tables[0];
            gvStatusStructure.DataBind();
        }
        else
        {
            gvStatusStructure.DataSource = ds.Tables[0];
            gvStatusStructure.DataBind();
        }
        }
        catch (Exception)
        {
        }

    }

    protected void UserAccessValidation()
    {
        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {

            ImgAdd.Visible = false;

        }
        if (objUA.Edit == 0)
        {
            uaEditFlag = false;
            btnsave.Visible = false;
        }
        else
        {
            uaEditFlag = true;
        }

        if (objUA.Delete == 0)
        {
            uaDeleteFlage = true;
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



    protected void ImgAdd_Click(object sender, EventArgs e)
    {
        try
        {       
        HiddenFlag.Value = "Add";
        OperationMode = "Add Status Structure";
        ddlMainStatus.Enabled = true;
        BindMainStatus();
        BindCalculatedStatus();
        BindJoiningType();
        string AddSalStrmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddSalStrmodal", AddSalStrmodal, true);
        }
        catch (Exception)
        {
        }
    }






    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int Add = 0;
            if (HiddenFlag.Value == "Add")
                Add = 1;
            else Add = 0;

            if (ddlMainStatus.SelectedIndex <= 0)
            {
                string js = " Please select status";
                string msgdivResponseShow = string.Format("alert('" + js + "');showModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
                return;
            }
            DataTable C_dt = new DataTable();
            DataTable J_dt = new DataTable();
            C_dt.Clear();
            C_dt.Columns.Add("C_ID");
            // C_dt.Columns.Add("C_Status");
            J_dt.Columns.Add("J_ID");
            // J_dt.Columns.Add("C_Joining_Type");

            for (int i = 0; i < chkCalc_Status.Items.Count; i++)
            {
                if (chkCalc_Status.Items[i].Selected)
                {
                    string Sid = ddlMainStatus.SelectedValue;
                    string Name = chkCalc_Status.Items[i].Text;
                    string id = chkCalc_Status.Items[i].Value;
                    DataRow _dr = C_dt.NewRow();
                    _dr["C_ID"] = id;
                    C_dt.Rows.Add(_dr);

                }
            }
            for (int i = 0; i < chkJoiningType.Items.Count; i++)
            {
                if (chkJoiningType.Items[i].Selected)
                {
                    string Name = chkJoiningType.Items[i].Text;
                    string id = chkJoiningType.Items[i].Value;
                    DataRow _dr = J_dt.NewRow();
                    _dr["J_ID"] = id;
                    J_dt.Rows.Add(_dr);
                }
            }

            int select = J_dt.Rows.Count + C_dt.Rows.Count;
            if (select <= 0)
            {
                string js = " Please select at least one option from Joining type or Sub status";
                string msgdivResponseShow = string.Format("alert('" + js + "');showModal('divadd',false);");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
                return;
            }
            string retval = objCrewAdmin.Insert_Update_StatusStructure(Convert.ToInt32(ddlMainStatus.SelectedValue), C_dt, J_dt, Convert.ToInt32(Session["USERID"]), Add);
            if (Add == 1)
            {
                if (retval != "0")
                {
                    string js = " Added Successfully...!";
                    string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divadd',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
                }
                else
                {
                    string js = "Already Exists with a Status";
                    string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divadd',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
                }
            }
            else
            {
                if (retval != "0")
                {
                    string js = " Updated Successfully...!";
                    string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divadd',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
                }
                else
                {
                    string js = " Already Exists with a Status";
                    string msgdivResponseShow = string.Format("alert('" + js + "');hideModal('divadd',false);");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgdivResponseShow", msgdivResponseShow, true);
                }
            }
            BindStatusStructure();
            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        }
        catch (Exception)
        {
        }
    }




    protected void onUpdate(object source, CommandEventArgs e)
    {
        try
        {
            HiddenFlag.Value = "Edit";
            OperationMode = "Edit Status Structure";
            ddlMainStatus.Enabled = false;
            BindCalculatedStatus();
            BindJoiningType();
            //  dt = objCrewAdmin.StatusStructure_Edit(Convert.ToInt32(e.CommandArgument.ToString().Split(';')[0]));
            LoadStatusByStatusID(Convert.ToInt32(e.CommandArgument.ToString().Split(';')[0]));
            string editSalStrmodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editSalStrmodal", editSalStrmodal, true);
        }
        catch (Exception)
        {
        }
    }


    private void LoadStatusByStatusID(int statusID)
    {
        try
        {
            chkCalc_Status.Items.Clear();
            chkJoiningType.Items.Clear();
            BindCalculatedStatus();
            BindJoiningType();
            DataTable dt = new DataTable();

            dt = objCrewAdmin.StatusStructure_Edit(statusID);

            int i = 0;
            string str1 = string.Empty;

            if (dt.Rows.Count != 0)
            {
                try
                {
                    ddlMainStatus.SelectedValue = dt.Rows[0]["ID"].ToString() != "" ? dt.Rows[0]["ID"].ToString() : "0";

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["C_ID"].ToString() != "0")
                        {
                            chkCalc_Status.Items.FindByValue(dr["C_ID"].ToString()).Selected = true;
                        }

                        if (dr["J_ID"].ToString() != "0")
                        {
                            chkJoiningType.Items.FindByValue(dr["J_ID"].ToString()).Selected = true;
                        }
                        i++;
                    }
                }
                catch (Exception)
                {
                    ddlMainStatus.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
            else
            {
                ddlMainStatus.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception)
        { }
    }


    protected void onDelete(object source, CommandEventArgs e)
    {
        BindMainStatus();
        BindStatusStructure();
    }


    protected void gvStatusStructure_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "MoveUp")
            {
                string[] arg = e.CommandArgument.ToString().Split(',');
                int Code = UDFLib.ConvertToInteger(arg[0]);
                int Parent_Code = UDFLib.ConvertToInteger(arg[1]);
                BindStatusStructure();
            }
            else if (e.CommandName == "MoveDown")
            {
                string[] arg = e.CommandArgument.ToString().Split(',');
                int Code = UDFLib.ConvertToInteger(arg[0]);
                int Parent_Code = UDFLib.ConvertToInteger(arg[1]);
                BindStatusStructure();
            }
        }
        catch (Exception)
        {
        }
    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            txtfilter.Text = "";
            BindStatusStructure();
        }
        catch (Exception)
        {
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            BindStatusStructure();
        }
        catch (Exception)
        {}
    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {

    }

    protected void gvStatusStructure_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;

            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;
            BindStatusStructure();

        }
        catch (Exception)
        {
        }
    }
    protected void ddlMainStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadStatusByStatusID(Convert.ToInt32(ddlMainStatus.SelectedValue));
            string editSalStrmodal = String.Format("showModal('divadd',false);");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editSalStrmodal", editSalStrmodal, true);
        }
        catch (Exception)
        {
        }

    }
}