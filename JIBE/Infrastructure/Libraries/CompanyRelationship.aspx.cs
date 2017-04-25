using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Data;

public partial class Infrastructure_Libraries_CompanyRelationship : System.Web.UI.Page
{
    BLL_Infra_Company objCompBLL = new BLL_Infra_Company();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserAccessValidation();

            BindCompanyRelationDLL();

            ddCompRelationshipType.SelectedValue = "1";
            Load_CompanyList();
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void UserAccessValidation()
    {
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        UserAccess objUA = new UserAccess();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            Response.Redirect("~/default.aspx?msgid=1");
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

    protected void BindCompanyRelationDLL()
    {
        ddCompRelationshipType.DataTextField = "Relationship_Name";
        ddCompRelationshipType.DataValueField = "ID";
        ddCompRelationshipType.DataSource = objCompBLL.Get_CompanyRelationType(UDFLib.ConvertToInteger(Session["USERID"]));
        ddCompRelationshipType.DataBind();
        ddCompRelationshipType.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }


    protected void Load_CompanyList()
    {
        try
        {

            int RelationshipType_id = UDFLib.ConvertToInteger(ddCompRelationshipType.SelectedValue);

            DataSet ds = objCompBLL.Get_Company_Parent_Child(RelationshipType_id, UDFLib.ConvertToInteger(lstCompany.SelectedValue), Convert.ToInt32(Session["USERID"].ToString()));

            if (ds.Tables.Count > 0)
            {
                lstCompany.DataSource = ds.Tables[0];
                lstCompany.DataTextField = "company_name";
                lstCompany.DataValueField = "ID";
                lstCompany.DataBind();
                lstCompany.Items.Insert(0, new ListItem("-Select-", "0"));

                lstCompany1.DataSource = ds.Tables[1];
                lstCompany1.DataTextField = "company_name";
                lstCompany1.DataValueField = "ID";
                lstCompany1.DataBind();
            }

        }
        catch (Exception ex)
        {

        }
    }


    protected void ddCompRelationshipType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_CompanyList();
    }



    protected void lstCompany_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable dt = objCompBLL.Get_CompanyRelationships(UDFLib.ConvertToInteger(lstCompany.SelectedValue), GetSessionUserID());
        foreach (ListItem li in lstCompany1.Items)
        {
            DataRow[] dr = dt.Select("child_company_id = " + li.Value + " and relation =" + ddCompRelationshipType.SelectedValue);
            if (dr.Length > 0)
                li.Selected = true;
            else
                li.Selected = false;
        }
    }

    protected void lstCompany1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = Company();
        foreach (ListItem li in lstCompany1.Items)
            dt = AddCompany(dt, UDFLib.ConvertToInteger(li.Value), li.Selected == true ? 1 : 0);

        ViewState["dt"] = dt;
    }

    protected DataTable Company()
    {
        DataTable dtCompany = new DataTable();
        dtCompany.Columns.Add("ID", typeof(int));
        dtCompany.Columns.Add("VALUE", typeof(string));
        return dtCompany;
    }

    protected DataTable AddCompany(DataTable dt, int CompanyID, int Selected)
    {
        DataRow dr = dt.NewRow();
        dr["ID"] = CompanyID;
        dr["VALUE"] = Selected;
        dt.Rows.Add(dr);
        return dt;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dt = Company();

        foreach (ListItem li in lstCompany1.Items)
            dt = AddCompany(dt, UDFLib.ConvertToInteger(li.Value), li.Selected == true ? 1 : 0);

       
        objCompBLL.UpdateCompanyReletionship(UDFLib.ConvertToInteger(lstCompany.SelectedValue),dt, Convert.ToInt32(ddCompRelationshipType.SelectedValue), GetSessionUserID());

    }

}