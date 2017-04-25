using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Properties;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;

public partial class Purchase_AssignWorklist : System.Web.UI.Page
{
    BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();
    List<string> dtSelectedWL = null;
    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.Page.Header.Controls.Add(SetUserStyle.AddThemeInHeader());
            base.OnInit(e);
        }
        catch { }
    }

    public int? OFFICE_ID = 0;
    public int? WORKLIST_ID = 0;
    public int? VESSEL_ID = 0;
    public string DocCode="";

    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            OFFICE_ID = Convert.ToInt32(Request.QueryString["OFFID"]);
            WORKLIST_ID = Convert.ToInt32(Request.QueryString["WLID"]);
            VESSEL_ID = Convert.ToInt32(Request.QueryString["VID"]);
            DocCode = Convert.ToString((Request.QueryString["DocumentCode"]));

            btnAdd.Visible = false;
            grdAddWorklistInvolved.DataSource = null;
            grdAddWorklistInvolved.DataBind();
            LoadWorkListInvolved(true);
            
            
        }
        
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnAdd.Enabled = false;

        }
        ViewState["del"] = objUA.Delete;


    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    private void EnableDisableControls(bool Mode)
    {
        if (Request["Mode"] == "View")
        {
            grdAddWorklistInvolved.Visible=Mode;
        }
        
        grdWorklistInvolved.Visible = Mode;
    }

    private void LoadWorkListInvolved(bool IsBind)
    {
        int s = 0;
        DataTable dtWorklstInvolved = objBLLPurc.Get_Purc_Worklist(Convert.ToInt32(Request.QueryString["OFFID"]), Convert.ToInt32(Request.QueryString["VID"]), Convert.ToString(Request.QueryString["DocumentCode"]));

        List<string> lstID = dtWorklstInvolved.AsEnumerable()
                          .Select(r => Convert.ToString(r.Field<Int32>("WORKLIST_ID")))
                          .ToList();
        ViewState["tr"] = lstID;


        if (IsBind == true)
        {
            grdWorklistInvolved.DataSource = dtWorklstInvolved;
            grdWorklistInvolved.DataBind();
        }
      

        if (Request.QueryString["Mode"] == "View")
        {
            grdAddWorklistInvolved.Visible = false;
            txtSearch.Visible = false;
            
            tdPager.Visible = false;
            Search.Visible = false;
            lblhdr1.Visible = false;
            grdWorklistInvolved.Enabled = false;
            btnAdd.Visible = false;
        }
    }
    protected void BindWorklist()
    {
        UpdateWorkList();
        int SelectRecordCount = 1;
        DataSet dtWorklist = objBLLPurc.Get_WorkList_Search(txtSearch.Text.Trim(),Convert.ToInt32(Request.QueryString["VID"]),null, null,ucCustomPagerctp.CurrentPageIndex,ucCustomPagerctp.PageSize,ref SelectRecordCount);
        LoadWorkListInvolved(false);
        grdAddWorklistInvolved.DataSource = dtWorklist;
        grdAddWorklistInvolved.DataBind();
        ucCustomPagerctp.CountTotalRec = SelectRecordCount.ToString();
        ucCustomPagerctp.BuildPager();
        ViewState["dtWorklist"] = dtWorklist;

        if (dtWorklist.Tables[0].Rows.Count > 0)
        {
            btnAdd.Visible = true;
        }
        else
        {
            btnAdd.Visible = false;
        }
        UpdateWorkList();
    }


  


    protected void onDelete(object sender, EventArgs e)
    {
         ImageButton objImage = (ImageButton)sender;
        
        string[] commandArgs = objImage.CommandArgument.ToString().Split(new char[] { ',' });
        string item = commandArgs[0];
        string text = commandArgs[1];
        string offid = commandArgs[2];

        int retval = objBLLPurc.Insert_Purc_WorkList(UDFLib.ConvertIntegerToNull(item), Convert.ToInt32(Request.QueryString["WLID"]), Convert.ToInt32(offid),
                Convert.ToInt32(Request.QueryString["VID"]), DocCode, "D", Convert.ToInt32(Session["UserID"].ToString()));
            //(Convert.ToInt32(((ImageButton)sender).CommandArgument.ToString()), Convert.ToInt32(Request.QueryString["VID"]), Convert.ToInt32(Request.QueryString["OFFID"]), Convert.ToInt32(Session["USERID"]));

         List<string> lstdeleted1 = ((List<string>)ViewState["listWL"]);
        if (lstdeleted1 != null)
        {
            lstdeleted1.Remove(Convert.ToString(text));
        }
        List<string> lstdeleted = ((List<string>)ViewState["tr"]);
        if (lstdeleted != null)
        {
            lstdeleted.Remove(Convert.ToString(text));
        }
        
        unchkItems();
        LoadWorkListInvolved(true);

        if (txtSearch.Text.Trim() != "")
        {
            BindWorklist();
        }
        
    }

    protected bool SelectCheckbox(string WorklistID)
    {
        List<string> dtSelectedWorklist = ((List<string>)ViewState["dtSelectedWL"]);
        List<string> listWL = ((List<string>)ViewState["listWL"]);

        if (listWL != null)
            if (listWL.Count > 0)
                if (listWL.Contains(WorklistID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
        return false;

    }

    protected void UpdateWorkList()
    {
        List<string> dtSelectedWorklist = ((List<string>)ViewState["dtSelectedWL"]);
        List<string> listwl = ((List<string>)ViewState["listWL"]);
        if (dtSelectedWorklist == null)
            dtSelectedWorklist = new List<string>();
        if (listwl == null)
            listwl = new List<string>();

        
        foreach (GridViewRow item in grdAddWorklistInvolved.Rows)
        {
            if (dtSelectedWorklist.Count > 0)
            {
                if (((CheckBox)grdAddWorklistInvolved.Rows[item.RowIndex].FindControl("checkRow")).Checked)
                {
                    if (!dtSelectedWorklist.Contains(grdAddWorklistInvolved.DataKeys[item.RowIndex][0].ToString()))
                    {
                        Label lblOFFICE_ID = (Label)(item.FindControl("lblOFFICE_ID"));
                        dtSelectedWorklist.Add(grdAddWorklistInvolved.DataKeys[item.RowIndex][0].ToString() + "-" + lblOFFICE_ID.Text);

                        if (!listwl.Contains(grdAddWorklistInvolved.DataKeys[item.RowIndex][0].ToString()))
                        listwl.Add(grdAddWorklistInvolved.DataKeys[item.RowIndex][0].ToString());
                    }
                }
                else
                {
                    Label lblOFFICE_ID = (Label)(item.FindControl("lblOFFICE_ID"));
                    dtSelectedWorklist.Remove(grdAddWorklistInvolved.DataKeys[item.RowIndex][0].ToString() + "-" + lblOFFICE_ID.Text);
                    listwl.Remove(grdAddWorklistInvolved.DataKeys[item.RowIndex][0].ToString());
                }
            }
            else
            {
                if (((CheckBox)grdAddWorklistInvolved.Rows[item.RowIndex].FindControl("checkRow")).Checked)
                {
                    Label lblOFFICE_ID = (Label)(item.FindControl("lblOFFICE_ID"));
                    dtSelectedWorklist.Add(grdAddWorklistInvolved.DataKeys[item.RowIndex][0].ToString() + "-" + lblOFFICE_ID.Text);
                    listwl.Add(grdAddWorklistInvolved.DataKeys[item.RowIndex][0].ToString());

                }
            }
        }

        ViewState["dtSelectedWL"] = dtSelectedWorklist;
        ViewState["listWL"] = listwl;
  
    }
    protected void Search_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text.Trim() != "")
        {
            BindWorklist();
            
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string ReqsnCode = DocCode;
        UpdateWorkList();
        DataTable dtAllWorklist = ((DataTable)ViewState["dtAllWorklist"]);
        dtSelectedWL = ((List<string>)ViewState["dtSelectedWL"]);

        if (dtSelectedWL == null)
            dtSelectedWL = new List<string>();

        foreach (string worklistID in dtSelectedWL)
        {
            
            int retval = objBLLPurc.Insert_Purc_WorkList(0, UDFLib.ConvertIntegerToNull((worklistID.Split('-')[0])), Convert.ToInt32((worklistID.Split('-')[1])),
                Convert.ToInt32(Request.QueryString["VID"]), Convert.ToString(Request.QueryString["DocumentCode"]), "A", Convert.ToInt32(Session["UserID"].ToString()));
        }

        LoadWorkListInvolved(true);
        BindWorklist();
    }

    protected void grdAddWorklistInvolved_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblWrID = (Label)e.Row.FindControl("lblWrID");
            List<string> dtSelectedWL = ((List<string>)ViewState["dtSelectedWL"]);
            List<string> listwl = ((List<string>)ViewState["listWL"]);
            List<string> savedEL = ((List<string>)ViewState["tr"]);
            //if (savedEL.Count > 0 && savedEL != null && dtSelectedWL.Count > 0 && dtSelectedWL != null)
            //{
                if (dtSelectedWL != null || savedEL != null || listwl!= null)
                {

                    IEnumerable<string> aa = listwl.Except(savedEL, StringComparer.OrdinalIgnoreCase);


                    if (listwl.Contains(Convert.ToString(lblWrID.Text)) || savedEL.Contains(Convert.ToString(lblWrID.Text)))
                    {
                        CheckBox cb = (CheckBox)e.Row.FindControl("checkRow");
                        cb.Checked = true;
                        cb.Enabled = false;
                        if (aa.Contains(lblWrID.Text))
                        {
                            cb.Checked = true;
                            cb.Enabled = true;
                        }
                    }
                    else
                    {
                        CheckBox cb = (CheckBox)e.Row.FindControl("checkRow");
                        cb.Enabled = true;
                        cb.Checked = false;
                    }
                }
            //}
        }
    }
    
    
    private void unchkItems()
    {
        foreach (GridViewRow gridRow in grdAddWorklistInvolved.Rows)
        {
            Label lblWrID = (Label)gridRow.FindControl("lblWrID");
            List<string> dtSelectedWL = ((List<string>)ViewState["dtSelectedWL"]);
            List<string> listwl = ((List<string>)ViewState["listWL"]);
            List<string> savedEL = ((List<string>)ViewState["tr"]);
            if (dtSelectedWL != null || savedEL != null || listwl != null)
            {
                IEnumerable<string> aa = listwl.Except(savedEL, StringComparer.OrdinalIgnoreCase);
                if (listwl.Contains(Convert.ToString(lblWrID.Text)) || savedEL.Contains(Convert.ToString(lblWrID.Text)))
                {
                    CheckBox cb = (CheckBox)gridRow.FindControl("checkRow");
                    cb.Checked = true;
                    cb.Enabled = false;
                    if (aa.Contains(lblWrID.Text))
                    {
                        cb.Checked = true;
                        cb.Enabled = true;
                    }
                }
                else
                {
                    CheckBox cb = (CheckBox)gridRow.FindControl("checkRow");
                    cb.Enabled = true;
                    cb.Checked = false;
                }
            }
        }

    }
}