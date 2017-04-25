using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Business.QMSDB;
using SMS.Properties;
using SMS.Business.Crew;
using System.IO;


public partial class ProcedureMandatoryRead : System.Web.UI.Page
{

    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["ID"] = null;
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;

            Load_RankList();
            Load_Rank_Category();
            BindFolder();
            BindProcedureRead();
            
        }

        //UserAccessValidation();

    }
    public void BindFolder()
    {
        try
        {

            DataTable dtFolders = BLL_QMSDB_Folders.QMSDBFoldes_List(null, null, null, null, 1);


            //ddlFolderName.DataSource = dtFolders;
            //ddlFolderName.DataTextField = "XNPATH";
            //ddlFolderName.DataValueField = "FOLDER_ID";
            //ddlFolderName.DataBind();

            if (dtFolders.Rows.Count > 0)
            {
                //ddlFolderName.SelectedValue = "1";
                BindTrvFolder(dtFolders);
                //GenerateUL(dtFolders);
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void trvFolder_SelectedNodeChanged(object sender, EventArgs e)
    {
        BindProcedureRead();
    }

    private void BindTrvFolder(DataTable dt)
    {

        DataRow[] drs = dt.Select("Folder_Id='1'");
        int meni = 0;
        int child = 0;
        foreach (DataRow dr in drs)
        {
            TreeNode mi = new TreeNode(dr["FOLDER_NAME"].ToString().Trim(), dr["FOLDER_ID"].ToString(), getNodeImageURL("Parent.FDC"));
            mi.SelectAction = TreeNodeSelectAction.Select;
            mi.Expand();
            trvFolder.Nodes.Add(mi);

            DataRow[] drInners = dt.Select("PARENT_FOLDER_ID ='" + dr["FOLDER_ID"].ToString() + "' ");
            if (drInners.Length != 0)
            {
                child = 0;
                foreach (DataRow drInner in drInners)
                {
                    TreeNode miner;
                    miner = new TreeNode(drInner["FOLDER_NAME"].ToString().Trim(), drInner["FOLDER_ID"].ToString(), getNodeImageURL(drInner["FOLDER_NAME"].ToString().Trim()));

                    trvFolder.Nodes[meni].ChildNodes.Add(miner);
                    trvFolder.Nodes[meni].CollapseAll();

                    string filter = "PARENT_FOLDER_ID ='" + drInner["FOLDER_ID"].ToString() + "'";

                    dt.AcceptChanges();
                    DataRow[] drInnerLinks = dt.Select(filter);

                    if (drInnerLinks.Length != 0)
                    {
                        foreach (DataRow drInnerLink in drInnerLinks)
                        {
                            TreeNode milink = new TreeNode(drInnerLink["FOLDER_NAME"].ToString().Trim(), drInnerLink["FOLDER_ID"].ToString(), getNodeImageURL(drInnerLink["FOLDER_NAME"].ToString().Trim()));
                            trvFolder.Nodes[meni].ChildNodes[child].ChildNodes.Add(milink);
                            trvFolder.Nodes[meni].ChildNodes[child].CollapseAll();
                        }
                    }
                    child++;
                }
            }
            meni++;
        }



    }
    private string getNodeImageURL(string NodeText)
    {
        string extenssion = Path.GetExtension(NodeText);

        extenssion = extenssion.ToLower();

        if (extenssion.IndexOf(".") >= 0)
        {
            switch (extenssion)
            {
                case ".xls":
                    return "~/images/DocTree/xls.gif";
                case ".xlsx":
                    return "~/images/DocTree/xls.gif";
                case ".pdf":
                    return "~/images/DocTree/pdf.gif";
                case ".htm":
                    return "~/images/DocTree/page.gif";
                case ".html":
                    return "~/images/DocTree/page.gif";
                case ".txt":
                    return "~/images/DocTree/txt.gif";
                case ".doc":
                    return "~/images/DocTree/doc.gif";
                case ".docx":
                    return "~/images/DocTree/doc.gif";
                case ".tiff":
                    return "~/images/DocTree/gif.gif";
                case ".tif":
                    return "~/images/DocTree/gif.gif";
                case ".zip":
                    return "~/images/DocTree/zip.gif";
                case ".csv":
                    return "~/images/DocTree/xls.gif";
                case ".gif":
                    return "~/images/DocTree/bmp.gif";
                case ".jpg":
                    return "~/images/DocTree/jpg.gif";
                case ".jpeg":
                    return "~/images/DocTree/jpg.gif";
                case ".bmp":
                    return "~/images/DocTree/bmp.gif";
                case ".png":
                    return "~/images/DocTree/bmp.gif";
                case ".rtf":
                    return "~/images/DocTree/page.gif";
                case ".fdc":
                    return "~/images/DocTree/network.gif";
                default:
                    return "~/images/DocTree/page.gif";
            }

        }
        else
        { return "~/images/DocTree/folder.gif"; }
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



    public void BindProcedureRead()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        string NodeValue="";
        if (trvFolder.SelectedNode != null)
        {
            NodeValue = trvFolder.SelectedNode.Value;
        }          


        DataTable dt = BLL_QMSDB_ProcedureSection.Get_RankList_Search(txtSearchBy.Text, UDFLib.ConvertIntegerToNull(ddlRank.SelectedValue), UDFLib.ConvertIntegerToNull(ddlRankCategory.SelectedValue),UDFLib.ConvertIntegerToNull(NodeValue)
            , sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            gvProcedureRead.DataSource = dt;
            gvProcedureRead.DataBind();
        }
        else
        {
            gvProcedureRead.DataSource = dt;
            gvProcedureRead.DataBind();
        }
    }

    public void Load_RankList()
    {
        BLL_Crew_Admin objCrewAdmin = new BLL_Crew_Admin();
        DataTable dt = objCrewAdmin.Get_RankList();

        ddlRank.DataSource = dt;
        ddlRank.DataTextField = "Rank_Short_Name";
        ddlRank.DataValueField = "ID";
        ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
        ddlRank.SelectedIndex = 0;
    }


    protected void Load_Rank_Category()
    {
        BLL_Crew_Admin objBLL = new BLL_Crew_Admin();
        DataTable dt = objBLL.Get_RankCategories();

        ddlRankCategory.DataSource = dt;
        ddlRankCategory.DataTextField = "category_name";
        ddlRankCategory.DataValueField = "id";
        ddlRankCategory.DataBind();
        ddlRankCategory.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));


    }

    protected void gvProcedureRead_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.color='blue';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';";
        
        }
    }

    protected void gvProcedureRead_RowDataBound(object sender, GridViewRowEventArgs e)
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

  

    protected void gvProcedureRead_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindProcedureRead();
     

    }


 

    protected void onDelete(object source, CommandEventArgs e)
    {

        int retval = BLL_QMSDB_ProcedureSection.DeleteReadMandatory(Convert.ToInt32(e.CommandArgument.ToString().Split(',')[1].ToString()), Convert.ToInt32(e.CommandArgument.ToString().Split(',')[0].ToString()), Convert.ToInt32(Session["USERID"].ToString()));
        BindProcedureRead();
        UpdPnlGrid.Update();
        
    }
 
    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
         
        BindProcedureRead();
        UpdPnlGrid.Update();
    }

  
    protected void btnClear_Click(object sender, EventArgs e)
    {
         

        ViewState["SORTDIRECTION"] = null;
        ViewState["SORTBYCOLOUMN"] = null;

        txtSearchBy.Text = "";

        ddlRank.SelectedValue = "0";
        ddlRankCategory.SelectedValue = "0"; 

        BindProcedureRead();
        UpdPnlGrid.Update();
    }

    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());



        DataTable dt = BLL_QMSDB_ProcedureSection.Get_RankList_Search(txtSearchBy.Text, UDFLib.ConvertIntegerToNull(ddlRank.SelectedValue), UDFLib.ConvertIntegerToNull(ddlRankCategory.SelectedValue),null
                 , sortbycoloumn, sortdirection, null, null, ref  rowcount);



        string[] HeaderCaptions = { "Rank Name", "Rank Short Name", "Read Access" };
        string[] DataColumnsName = { "Rank_Name", "Rank_Short_Name", "READ_ACCESS_FLAG" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "FBMReadAccess", "FBM Read Access");


    }

    protected void chkAccess_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox chkAccess = (sender as CheckBox);
        GridViewRow row = chkAccess.NamingContainer as GridViewRow;

        if (((CheckBox)row.FindControl("chkAccess")).Checked == true)
        {

            int retVal = BLL_QMSDB_ProcedureSection.InsertReadMandatory(UDFLib.ConvertIntegerToNull(((Label)row.FindControl("lblFolderID")).Text)
                    , UDFLib.ConvertIntegerToNull(((Label)row.FindControl("lblRankID")).Text)
                    , Convert.ToInt32(Session["USERID"])
                    , ((CheckBox)row.FindControl("chkAccess")).Checked == true ? 1 : 0);

            BindProcedureRead();

            UpdPnlGrid.Update();
        }
    }
}