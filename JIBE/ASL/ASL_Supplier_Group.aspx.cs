using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.Technical;
using SMS.Business.ASL;


public partial class ASL_ASL_Group_Setting : System.Web.UI.Page
{
    BLL_ASL_Lib objBLL = new BLL_ASL_Lib();

    UserAccess objUA = new UserAccess();
    DataSet ds = new DataSet();
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
            BindSupplierGroup();
        }

    }



    public void BindSupplierGroup()
    {
        ds = objBLL.ASL_Supplier_ColumnGroup_List(GetSessionUserID());
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvSupplierGroup.DataSource = ds.Tables[0];
            gvSupplierGroup.DataBind();
        }
        else
        {
            gvSupplierGroup.DataSource = ds.Tables[0];
            gvSupplierGroup.DataBind();
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

        if (objUA.Add == 0)
        {
            ImgAdd.Visible = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        //else
        //    btnsave.Visible = false;

        if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindSupplierGroup();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        //txtfilter.Text = "";
        BindSupplierGroup();
    }

    protected void gvSupplierScope_RowDataBound(object sender, GridViewRowEventArgs e)
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
           
                DropDownList ddList = (DropDownList)e.Row.FindControl("ddlGroup");

                //return DataTable havinf department data
                
                ddList.DataSource = ds.Tables[1];
                ddList.DataTextField = "GROUP_NAME";
                ddList.DataValueField = "ID";
                ddList.DataBind();

            
                ddList.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Group_Name").ToString(); 
            

            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
        }

    }

    protected void gvSupplierScope_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindSupplierGroup();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("PKID");
        dt.Columns.Add("Fields_Name");
        dt.Columns.Add("Fields_Desc");
        dt.Columns.Add("Group_Name");
        dt.Columns.Add("Group_Desc");
        foreach (GridViewRow row in gvSupplierGroup.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lblFieldsName = (Label)row.FindControl("lblFieldsName");
                Label lblDesc = (Label)row.FindControl("lblDesc");
                DropDownList ddl =   (DropDownList)row.FindControl("ddlGroup");
                DataRow dr = dt.NewRow();
                dr["ID"] = 0;
                dr["PKID"] = UDFLib.ConvertIntegerToNull(gvSupplierGroup.DataKeys[row.RowIndex].Value.ToString());
                dr["Fields_Name"] = lblFieldsName.Text;
                dr["Fields_Desc"] = lblDesc.Text;
                dr["Group_Name"] = ddl.SelectedValue;
                dr["Group_Desc"] = ddl.SelectedItem.Text;
                dt.Rows.Add(dr);
            }

        }
       int retval =   objBLL.INS_ASL_Group_Item(dt,GetSessionUserID());
       if (retval >= 0)
       {
           string message = "alert('Changes Saved and Updated')";
           ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
       }
    }
}