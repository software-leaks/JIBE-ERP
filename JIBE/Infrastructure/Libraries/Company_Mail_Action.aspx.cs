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

public partial class Infrastructure_Libraries_Company_Mail_Action : System.Web.UI.Page
{
    BLL_Infra_CompMailAction objBLL = new BLL_Infra_CompMailAction();
    BLL_Infra_UserCredentials objBLLUser = new BLL_Infra_UserCredentials();
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

            ucCustomPagerItems.PageSize = 20;
            //Load_UserList();
            BindCompany_MailAction();
        }

    }

    public void BindCompany_MailAction()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;
        //int? isfavorite = null; if (ddlFavorite.SelectedValue != "2") isfavorite = Convert.ToInt32(ddlFavorite.SelectedValue.ToString());
        //int? countrycode = null; if (ddlSearchCountry.SelectedValue != "0") countrycode = Convert.ToInt32(ddlSearchCountry.SelectedValue.ToString());

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLL.SearchCompMailAction(sortbycoloumn, sortdirection
             , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvComMailAction.DataSource = dt;
            gvComMailAction.DataBind();
        }
        else
        {
            gvComMailAction.DataSource = dt;
            gvComMailAction.DataBind();
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

        //if (objUA.Add == 0) ImgAdd.Visible = false;
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

   

    

    protected void btnsave_Click(object sender, EventArgs e)
    {

        int responseid = 0;

        responseid = objBLL.EditCompMailAction(UDFLib.ConvertStringToNull(txtSubject.Text), UDFLib.ConvertStringToNull(txtMailTo.Text), UDFLib.ConvertStringToNull(txtMailCc.Text), UDFLib.ConvertStringToNull(txtemailbody.Text), int.Parse(txtMailID.Text), Convert.ToInt32(Session["USERID"]));

        
            BindCompany_MailAction();
            string hidemodal = String.Format("hideModal('divadd')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "hidemodal", hidemodal, true);
        

    }

    protected void onUpdate(object source, CommandEventArgs e)
    {

        HiddenFlag.Value = "Edit";
        OperationMode = "Edit Company Action Mail";
        txtActionType.Enabled = false;
        DataTable dt = new DataTable();
        dt = objBLL.Get_CompMailAction(Convert.ToInt32(e.CommandArgument.ToString()));
        txtMailID.Text = e.CommandArgument.ToString();
        txtActionType.Text = dt.Rows[0]["Action_Type"].ToString();
        txtSubject.Text = dt.Rows[0]["Subject"].ToString();
        txtMailTo.Text = dt.Rows[0]["Mail_To"].ToString();
        txtMailCc.Text = dt.Rows[0]["Mail_CC"].ToString();
        txtemailbody.Text = dt.Rows[0]["Body"].ToString();
       
        string Deptmodal = String.Format("showModal('divadd',false);");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Deptmodal", Deptmodal, true);

    }

   
    


    


    protected void gvComMailAction_RowDataBound(object sender, GridViewRowEventArgs e)
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


    protected void gvComMailAction_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindCompany_MailAction();
    }
}