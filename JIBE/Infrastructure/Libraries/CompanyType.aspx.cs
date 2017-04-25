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

using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class CompanyType : System.Web.UI.Page
{
   
    BLL_Infra_CompanyType objBLL = new BLL_Infra_CompanyType();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    UserAccess objUA = new UserAccess();

    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;

            HiddenFlag.Value = "Add";

            BindCompanyType();
        }

    }


    public void BindCompanyType()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.SearchCompanyType(null, sortbycoloumn, sortdirection
             , 1, 500, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvCompanyType.DataSource = dt;
            gvCompanyType.DataBind();
        }
        else
        {
            gvCompanyType.DataSource = dt;
            gvCompanyType.DataBind();
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
        this.SetFocus("ctl00_MainContent_txtUserType");
        HiddenFlag.Value = "Add";

        OperationMode = "Add User Type";

        //txtUserType.Text = "";


        string AddUserTypemodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);
    }

   

    protected void onUpdate(object source, CommandEventArgs e)
    {

        //HiddenFlag.Value = "Edit";
        //OperationMode = "Edit UserType";

        //DataTable dt = new DataTable();
        //dt = objBLL.Get_UserTypeList(Convert.ToInt32(e.CommandArgument.ToString()));
        //dt.DefaultView.RowFilter = "ID= '" + e.CommandArgument.ToString() + "'";

        //txtUserTypeID.Text = dt.DefaultView[0]["ID"].ToString();
        //txtUserType.Text = dt.DefaultView[0]["USER_TYPE"].ToString();

        //string AddUserTypemodal = String.Format("showModal('divadd',false);");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddUserTypemodal", AddUserTypemodal, true);

    }

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = objBLL.DeleteCompanyType(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"]));
        BindCompanyType();


    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindCompanyType();
    }


    public void ClearField()
    {
        txtCompanyTypeDesc.Text = "";
        txtCompanyTypeName.Text = "";
    
    }

 
    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (HiddenFlag.Value == "Add")
        {
            int responseid = objBLL.InsertCompanyType(txtCompanyTypeName.Text.Trim(), txtCompanyTypeDesc.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            //  int responseid = objBLL.EditCompanyType(Convert.ToInt32(txtCompanyTypeID.Text),txtCompanyTypeName.Text.Trim(),txtCompanyTypeDesc.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        }

        BindCompanyType();
        ClearField();
    }

    protected void btnSaveNClose_Click(object sender, EventArgs e)
    {

        if (HiddenFlag.Value == "Add")
        {
            int responseid = objBLL.InsertCompanyType(txtCompanyTypeName.Text.Trim(), txtCompanyTypeDesc.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        }
        else
        {
            //  int responseid = objBLL.EditCompanyType(Convert.ToInt32(txtCompanyTypeID.Text),txtCompanyTypeName.Text.Trim(),txtCompanyTypeDesc.Text.Trim(), Convert.ToInt32(Session["USERID"]));
        }

        BindCompanyType();
        ClearField();


        String script = String.Format("parent.RefreshMakerFromChild();parent.hideModal('dvIframe');");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);

    }

    





    protected void gvCompanyType_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvCompanyType_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindCompanyType();

    }
}