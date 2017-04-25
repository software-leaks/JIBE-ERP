using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

using SMS.Business.Crew;
using SMS.Business.DMS;
using SMS.Business.Infrastructure;
using SMS.Properties;

using AjaxControlToolkit;

public partial class DMS_Default : System.Web.UI.Page
{

    BLL_DMS_Document objDMSBLL = new BLL_DMS_Document();
    BLL_Infra_Country objInfraBLL = new BLL_Infra_Country();
    BLL_DMS_Admin objDMSAdminBLL = new BLL_DMS_Admin();
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
            Response.Redirect("~/account/login.aspx");
        else
            UserAccessValidation();


        if (!IsPostBack)
        {
            var CrewID = GetCrewID();
            var CurrentUserID = GetSessionUserID();

            if (CurrentUserID == 0)
                Response.Redirect("~/Account/Login.aspx");
            
            if (CrewID > 0)
            {
                lblCrewName.Text = "Selected Crew: " + objBLLCrew.Get_CrewPersonalDetailsByID(CrewID, "Staff_FullName");
                HiddenField_CrewID.Value = GetCrewID().ToString();
                HiddenField_UserID.Value = GetSessionUserID().ToString();
                HiddenField_DocumentUploadPath.Value = Server.MapPath("../Uploads/CrewDocuments");
                //imgUpload.Visible = true;

                LoadtreeView();

            }

            if (getQueryString("DocID") != "")
            {
                int DocID = int.Parse(getQueryString("DocID"));
                string docFile = objDMSBLL.getDocumentFileNameByID(DocID);
                if (docFile != "")
                {
                    string crewDocPath = "../Uploads/CrewDocuments/" + docFile;

                    if (File.Exists(Server.MapPath("../Uploads/CrewDocuments/") + docFile))
                    {
                        string js = "toggleLeftPanel(1);previewDocument('" + crewDocPath + "');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "loadfile", js, true);
                    }
                    else
                    {
                          string js = "toggleLeftPanel(1);previewDocument('../Images/FileNotFound.png');";
                          ScriptManager.RegisterStartupScript(this, this.GetType(), "loadfile", js, true);
                    }


                   

                }
            }
            else
            {
                LoadDocuments();
            }

        }
        //string msg1 = String.Format("$('.sailingInfo').SailingInfo();");
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgshowdetails", msg1, true);
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx");

        if (objUA.Add == 0)
        {
            //imgUpload.Visible = false;
        }

        if (objUA.Edit == 0)
        {
            
        }
        if (objUA.Delete == 0)
        {
            
        }

    }

    public int GetCrewID()
    {
        if (Request.QueryString["ID"] != null)
        {
            return int.Parse(Request.QueryString["ID"].ToString());
        }
        else
            return 0;
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    private string getSessionString(string SessionField)
    {
        try
        {
            if (Session[SessionField] != null)
                return Session[SessionField].ToString();
            else
                return "";
        }
        catch
        {
            return "";
        }
    }
    private string getQueryString(string QueryField)
    {
        try
        {
            if (Request.QueryString[QueryField] != null && Request.QueryString[QueryField].ToString() != "")
            {
                return Request.QueryString[QueryField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }
    protected void BrowseTreeView_SelectedNodeChanged(object sender, EventArgs e)
    {

        TreeNode selectedNode = BrowseTreeView.SelectedNode;
        if (selectedNode != null)
        {

            if (selectedNode.ChildNodes.Count > 0)
            {
                ObjectDataSource_DocumentList.SelectParameters["DocTypeName"].DefaultValue = selectedNode.Text;
                ObjectDataSource_DocumentList.SelectParameters["DocName"].DefaultValue = "";
                ObjectDataSource_DocAttributeValue.SelectParameters["DocID"].DefaultValue = "0";
            }
            else
            {
                if (selectedNode.Target != "")
                {
                    if (selectedNode.Parent != null)
                    {
                        ObjectDataSource_DocumentList.SelectParameters["DocTypeName"].DefaultValue = selectedNode.Parent.Text;
                        ObjectDataSource_DocumentList.SelectParameters["DocName"].DefaultValue = selectedNode.Text;
                        if (GetCrewID() > 0)
                        {
                            ObjectDataSource_DocAttributeValue.SelectParameters["DocID"].DefaultValue = selectedNode.Target;
                            GridView_DocAttributes.DataBind();
                            GridView_DocAttributes.Visible = true;
                        }
                        else
                            GridView_DocAttributes.Visible = false;

                        string docFile = objDMSBLL.getDocumentFileNameByID(int.Parse(selectedNode.Target));
                        string crewDocPath = "../Uploads/CrewDocuments/" + docFile;


                        if (File.Exists(Server.MapPath("../Uploads/CrewDocuments/") + docFile))
                        {
                            string js = "previewDocument('" + crewDocPath + "');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "loadfile", js, true);
                        }
                        else
                        {
                            string js = "previewDocument('../Images/FileNotFound.png');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "loadfile", js, true);
                        }
                     }
                }
            }
        }
    }


    //protected void CreateChildNode(string NodePath, string NodeName, int NodeType, string NodeValue, string ParentNodeValue, string DocFileExt)
    //{

    //    try
    //    {
    //        string ParentNodePath = NodePath.Replace("/" + NodeName, "");
    //        TreeNode ParentNode = BrowseTreeView.FindNode(ParentNodePath);

    //        if (ParentNode == null)
    //        {

    //            string[] sN = ParentNodePath.Split('/');
    //            CreateChildNode(ParentNodePath, sN[sN.Length - 1], 0, NodeValue, ParentNodeValue, DocFileExt);

    //            TreeNode ParentNode1 = BrowseTreeView.FindNode(ParentNodePath);
    //            if (ParentNode1 != null)
    //                CreateNode(ParentNode1, NodeName, NodeType, NodeValue, ParentNodeValue, DocFileExt);
    //        }
    //        else
    //        {
    //            CreateNode(ParentNode, NodeName, NodeType, NodeValue, ParentNodeValue, DocFileExt);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //protected void CreateNode(TreeNode ParentNode, string NodeName, int NodeType, string NodeValue, string ParentNodeValue, string DocFileExt)
    //{
    //    TreeNode NewNode = new TreeNode(NodeName, NodeName, getNodeImageURL(DocFileExt, NodeType));

    //    if (NodeType == 0)
    //        NewNode.Target = ParentNodeValue;
    //    else
    //        NewNode.Target = NodeValue;

    //    NewNode.SelectAction = TreeNodeSelectAction.SelectExpand;
    //    //NewNode.Expand();
    //    ParentNode.ChildNodes.Add(NewNode);

    //}

    protected void CreateChildNode(string NodePath, string NodeName, int NodeType, string NodeValue, string ParentNodeValue, string DocFileExt)
    {

        try
        {
            //string ParentNodePath = NodePath.Replace("/" + NodeName, "");
            string[] Temp = NodePath.Split('/');

            string ParentNodePath = "";
            for (int i = 0; i < Temp.Length - 1; i++)
            {
                if (ParentNodePath.Length > 0)
                    ParentNodePath += "/";
                ParentNodePath += Temp[i];
            }

            TreeNode ParentNode = BrowseTreeView.FindNode(ParentNodePath);

            if (ParentNode == null)
            {

                string[] sN = ParentNodePath.Split('/');
                CreateChildNode(ParentNodePath, sN[sN.Length - 1], 0, NodeValue, ParentNodeValue, DocFileExt);

                TreeNode ParentNode1 = BrowseTreeView.FindNode(ParentNodePath);
                if (ParentNode1 != null)
                    CreateNode(ParentNode1, NodeName, NodeType, NodeValue, ParentNodeValue, DocFileExt);
            }
            else
            {
                CreateNode(ParentNode, NodeName, NodeType, NodeValue, ParentNodeValue, DocFileExt);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void CreateNode(TreeNode ParentNode, string NodeName, int NodeType, string NodeValue, string ParentNodeValue, string DocFileExt)
    {
        if (NodeName != "")
        {
            string icon = (NodeType == 0) ? getNodeImageURL("") : getNodeImageURL(DocFileExt);

            TreeNode NewNode = new TreeNode(NodeName, NodeName, icon);

            if (NodeType == 0)
                NewNode.Target = ParentNodeValue;
            else
                NewNode.Target = NodeValue;

            NewNode.SelectAction = TreeNodeSelectAction.SelectExpand;
            NewNode.Collapse();
            ParentNode.ChildNodes.Add(NewNode);
        }
    }
    private string getNodeImageURL(string extenssion)
    {
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

    protected void LoadtreeView()
    {
        try
        {
            BrowseTreeView.Nodes.Clear();

            TreeNode parentNode = new TreeNode("DOCUMENTS", "DOCUMENTS", getNodeImageURL("Parent.FDC", 0));
            parentNode.SelectAction = TreeNodeSelectAction.Select;
            parentNode.Expand();
            BrowseTreeView.Nodes.Add(parentNode);

            if (GetCrewID() > 0)
            {
                DataTable dtTree = new DataTable();
                dtTree = objBLLCrew.Get_CrewDocumentList(GetCrewID(), int.Parse(getSessionString("USERCOMPANYID")), txtSearchDoc.Text);


                if (dtTree.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTree.Rows)
                    {
                        string sNodeName = dr["DocName"].ToString().Replace("/", "-");
                        string DocTypeName = dr["DocTypeName"].ToString();
                        string DocFileExt = dr["DocFileExt"].ToString();
                        string VoyID = dr["VoyageID"].ToString();
                        string Vessel_Short_Name = dr["Vessel_Short_Name"].ToString();
                        string Rank = dr["Rank_Short_Name"].ToString().Replace("/", "");
                        string Sign_On_Date = dr["Sign_On_Date"].ToString();

                        string sDate = Sign_On_Date.Replace("/", "-");

                        if (DocTypeName == "")
                            DocTypeName = "NEW";

                        string sNodePath = "";

                        if (VoyID != "")
                        {
                            sNodePath = "DOCUMENTS/Voyage Related/" + Vessel_Short_Name + "-" + Rank + "-" + sDate + "/" + DocTypeName + "/" + sNodeName;
                        }
                        else
                        {
                            sNodePath = "DOCUMENTS/Perpetual/" + DocTypeName + "/" + sNodeName;
                        }
                        string NodeValue = dr["DocID"].ToString();
                        string ParentNodeValue = dr["DocTypeID"].ToString();

                        CreateChildNode(sNodePath, sNodeName, 1, NodeValue, ParentNodeValue, DocFileExt);
                    }
                }
            }
        }
        catch { }

    }
    protected void LoadDocuments()
    {
        ObjectDataSource_DocumentList.SelectParameters["DocTypeName"].DefaultValue = "DOCUMENTS";
        ObjectDataSource_DocumentList.DataBind();
    }
    
    protected void LoadDocumentAttributes(string DocID)
    {
        ObjectDataSource_DocAttributeValue.SelectParameters["DocID"].DefaultValue = DocID;
        GridView_DocAttributes.DataBind();
    }

    protected void txtSearchDoc_TextChanged(object sender, EventArgs e)
    {
        LoadtreeView();
        LoadDocuments();
    }
    protected void ImgSearchDoc_Click(object sender, ImageClickEventArgs e)
    {
    }
    protected void ImgClearSearch_Click(object sender, ImageClickEventArgs e)
    {
        txtSearchDoc.Text = "";
        LoadtreeView();
        BrowseTreeView.Nodes[0].Select();
        LoadDocuments();

    }
    protected string getNodeImageURL(string NodeText, int NodeType)
    {
        string extenssion = Path.GetExtension(NodeText);
        if (NodeType == 0)
        {
            if (extenssion == ".FDC")
                return "~/images/DocTree/" + "/network.gif";
            else
                return "~/images/DocTree/folder.gif";
        }
        else
        {
            switch (extenssion)
            {
                case ".xls":
                case ".pdf":
                case ".htm":
                case ".html":
                case ".txt":
                case ".doc":
                case ".tiff":
                case ".tif":
                case ".zip":
                case ".csv":
                case ".gif":
                case ".jpg":
                case ".jpeg":
                case ".bmp":
                case ".rtf":
                    return "~/images/DocTree/" + extenssion.Replace(".", "") + ".gif";
                default:
                    return "~/images/DocTree/" + "page.gif";
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strRowId = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
            e.Row.Cells[1].Attributes.Add("onclick", "window.location.href='Default.aspx?ID=" + strRowId + "'");

        }
    }
    protected void GridView_Documents_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        try
        {
            //TreeNode selectedNode = BrowseTreeView.SelectedNode;
            //int iDocID = int.Parse(selectedNode.Target);

            //int iTypeID = int.Parse(e.NewValues["DocTypeID"].ToString());
            //string sDocName = e.NewValues["DocName"].ToString();

            //objBLLCrew.UPDATE_DocTypeAndCreateAttributeValues(iDocID, GetCrewID(), sDocName, iTypeID, GetSessionUserID());

            LoadDocumentAttributes("0");
            LoadtreeView();
            LoadDocuments();

            //if (BrowseTreeView.FindNode("DOCUMENTS/" + sDocTypeName + "/" + sDocName) != null)
            //    BrowseTreeView.FindNode("DOCUMENTS/" + sDocTypeName + "/" + sDocName).Selected = true;

        }
        catch (Exception ex)
        {

        }


    }
    protected void GridView_Documents_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            TreeNode selectedNode = BrowseTreeView.SelectedNode;
            int iDocID = int.Parse(selectedNode.Target);

            int iTypeID = int.Parse(e.NewValues["DocTypeID"].ToString());
            string sDocName = e.NewValues["DocName"].ToString();

            objBLLCrew.UPDATE_DocTypeAndCreateAttributeValues(iDocID, GetCrewID(), sDocName, iTypeID, GetSessionUserID());

            LoadDocumentAttributes(iDocID.ToString());
            LoadtreeView();
            LoadDocuments();

            //if (BrowseTreeView.FindNode("DOCUMENTS/" + sDocTypeName + "/" + sDocName) != null)
            //    BrowseTreeView.FindNode("DOCUMENTS/" + sDocTypeName + "/" + sDocName).Selected = true;

        }
        catch (Exception ex)
        {

        }

    }
    protected void GridView_Documents_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        LoadtreeView();
        LoadDocuments();
        LoadDocumentAttributes("0");
    }
    
    protected void GridView_DocAttributes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strRowId = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
            string AttributeDataType = DataBinder.Eval(e.Row.DataItem, "AttributeDataType").ToString();
            string sValue = DataBinder.Eval(e.Row.DataItem, "AttributeValue_String").ToString();

            if (AttributeDataType == "DATETIME")
            {                
                if(sValue!="")
                {
                    try
                    {
                        DateTime dt = Convert.ToDateTime(sValue);
                        ((TextBox)e.Row.FindControl("txtAttributeValue")).Text = dt.ToString("dd/MM/yyyy");
                    }
                    catch { }
                }
            }
        }
    }
    protected void GridView_DocAttributes_DataBound(object sender, EventArgs e)
    {
        Bind_Autocomplete_Script(GridView_DocAttributes);
    }

    protected void Bind_Autocomplete_Script(GridView grid)
    {
        foreach (GridViewRow row in grid.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string AttributeType = ((HiddenField)row.FindControl("HiddenField_Type")).Value;
                if (AttributeType == "LIST")
                {
                    string ListSource = ((HiddenField)row.FindControl("HiddenField_ListSource")).Value;

                    string ClientID = ((TextBox)row.FindControl("txtAttributeValue")).ClientID;

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClientID, "initAutoComplete('" + ClientID + "', '" + ListSource + "');", true);
                }
            }
        }

    }
    protected void txtAttributeValue_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataControlFieldCell cell = (DataControlFieldCell)((TextBox)(sender)).Parent;
            GridViewRow row = (GridViewRow)(cell.Parent);
            int RowID = row.RowIndex;
            int CrewID = GetCrewID();
            int iAttributeID = 0;

            string sID = ((HiddenField)row.FindControl("HiddenField_ID")).Value;
            string AttributeType = ((HiddenField)row.FindControl("HiddenField_Type")).Value;
            string AttributeID = ((HiddenField)row.FindControl("HiddenField_AttributeID")).Value;

            string sDocID = BrowseTreeView.SelectedNode.Target;
            string sValue = ((TextBox)(sender)).Text;

            int DocID = 0;
            if (sDocID != "")
                DocID = int.Parse(sDocID);
            if (AttributeID != "")
                iAttributeID = int.Parse(AttributeID);

            Bind_Autocomplete_Script(GridView_DocAttributes);

            //if (AttributeType == "LIST")
            //{
            //    string ListSource = ((HiddenField)row.FindControl("HiddenField_ListSource")).Value;
            //    string ClientID = ((TextBox)row.FindControl("txtAttributeValue")).ClientID;
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClientID, "initAutoComplete('" + ClientID + "', '" + ListSource + "');", true);
            //}


            if (Validate(sValue, AttributeType))
            {
                int responseid = objBLLCrew.UPDATE_DocumentAttributeValues(DocID, CrewID, iAttributeID, sValue, AttributeType, GetSessionUserID());
                ((TextBox)GridView_DocAttributes.Rows[RowID + 1].FindControl("txtAttributeValue")).Focus();
            }
            else
                ((TextBox)GridView_DocAttributes.Rows[RowID].FindControl("txtAttributeValue")).Focus();



        }
        catch { }
    }
    protected Boolean Validate(string sValue, string sDataType)
    {
        Boolean ret = true;
        string js = "";

        switch (sDataType)
        {
            case "DATETIME":
                try
                {
                    DateTime dt = DateTime.Parse(sValue, iFormatProvider);
                    ret = true;
                }
                catch
                {
                    ret = false;
                    js = "alert('Entered value is not a valid Date')";
                }

                break;
        }

        if (js != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
        return ret;
    }
}