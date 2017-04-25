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
using SMS.Business.ASL;
 

public partial class ASL_ASL_Evalution_Index : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;
            if (Request.QueryString["Supplier_Code"] != null)
            {
                txtfilter.Text = Request.QueryString["Supplier_Code"].ToString();
            }
            else
            {
                txtfilter.Text = "";
            }
            ucCustomPagerItems.PageSize = 20;
            BindGrid();
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
            //ImgAdd.Visible = false;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            // btnsave.Visible = false;

            if (objUA.Delete == 1) uaDeleteFlage = true;

    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void BindGrid()
    {
        try
        {
            DataTable dt = BLL_ASL_Supplier.Get_Supplier_Pending_Evaluation(txtfilter.Text != "" ? txtfilter.Text : null, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
            if (dt.Rows.Count > 0)
            {
                gvEvaluation.DataSource = dt;
                gvEvaluation.DataBind();
            }
            else
            {
                gvEvaluation.DataSource = dt;
                gvEvaluation.DataBind();
            }
        }
        catch { }
        {
        }


    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        txtfilter.Text = "";
        BindGrid();
    }
    protected void onDelete(object source, CommandEventArgs e)
    {
        //int retval = objBLLAirPort.Delete_AirPort(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["USERID"].ToString()));
        //BindGrid();
    }


    protected void gvEvaluation_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvEvaluation_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindGrid();
    }
    protected void btnApprove_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? Evaluation_ID = UDFLib.ConvertIntegerToNull(arg[0]);
        string Supplier_Code = UDFLib.ConvertStringToNull(arg[1]);
        string EvaluationStatus = "Approved";
       
        //int RetValue = BLL_ASL_Supplier.Pending_Evaluation_Approve_Reject(UDFLib.ConvertIntegerToNull(Evaluation_ID.ToString())
        //       , Supplier_Code, EvaluationStatus, UDFLib.ConvertIntegerToNull(GetSessionUserID()));

        BindGrid();

    }
    protected void btnReject_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? Evaluation_ID = UDFLib.ConvertIntegerToNull(arg[0]);
        string Supplier_Code = UDFLib.ConvertStringToNull(arg[1]);
        string EvaluationStatus = "Rejected";

        //int RetValue = BLL_ASL_Supplier.Pending_Evaluation_Approve_Reject(UDFLib.ConvertIntegerToNull(Evaluation_ID.ToString())
        //       , Supplier_Code, EvaluationStatus, UDFLib.ConvertIntegerToNull(GetSessionUserID()));
        BindGrid();

    }
}