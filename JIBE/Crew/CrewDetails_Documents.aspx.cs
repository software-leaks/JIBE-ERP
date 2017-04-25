using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.DMS;
using System.Text;

public partial class Crew_CrewDetails_Documents : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objBLLCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    BLL_DMS_Admin objDMSAdminBLL = new BLL_DMS_Admin();
    BLL_DMS_Document objDMSBLL = new BLL_DMS_Document();

    UserAccess objUA = new UserAccess();
    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);
    public string DFormat = "";
    Boolean blnTxtSearch = false;
    List<string> LstGreyoutName = new List<string>();

    protected override void OnInit(EventArgs e)
    {
        try
        {
            base.Page.Header.Controls.Add(SetUserStyle.AddThemeInHeader());
            base.OnInit(e);

        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CalendarExtender7.Format = Convert.ToString(Session["User_DateFormat"]);
            CalendarExtender8.Format = Convert.ToString(Session["User_DateFormat"]);
            Session["isGrey"] = "";
            DFormat = Convert.ToString(Session["User_DateFormat"]);
            if (Session["USERID"] == null)
            {
                lblMsg.Text = "Session Expired!! Log-out and log-in again.";
            }
            else
            {
                if (!IsPostBack)
                {
                    Session["dtgreyout"] = "";
                    Session["Document_SortExpression"] = null;
                    Session["Document_SortDirection"] = "DESC";
                    UserAccessValidation();
                    int CrewID = UDFLib.ConvertToInteger(Request.QueryString["ID"]);

                    if (objUA.View == 1)
                    {
                        LoadtreeView();
                        BindCountry();
                        pnlDocuments.Visible = true;
                        BindGrid();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    protected string BindVoyageField(string text)
    {
        string returnStr = "";
        if (text != "")
        {
            if (text.Split('-').Length == 3)
            {
                returnStr = text.Split('-')[0] + "-" + text.Split('-')[1] + "-" + UDFLib.ConvertUserDateFormat(text.Split('-')[2]);
            }
        }
        return returnStr;
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();

        if (CurrentUserID == 0)
        {
            Response.Write("Session Expired!! Please log-out and login again");
            Response.End();
        }

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            lblMsg.Text = "You don't have sufficient privilege to access the requested information.";
            pnlDocuments.Visible = false;
        }
        if (objUA.Add == 0)
        {
            ImgAddDocument.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            btnSaveDocType.Enabled = false;
            btnSaveAndReplaceDocType.Enabled = false;
        }
        if (objUA.Delete == 0)
        {
            GridView_Documents.Columns[GridView_Documents.Columns.Count - 2].Visible = false;
        }
        if (objUA.Approve == 0)
        {
        }
        //-- MANNING OFFICE LOGIN --

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    /// <summary>
    /// Loads the Documnets in TreeView structure
    /// </summary>
    protected void LoadtreeView()
    {
        BrowseTreeView.Nodes.Clear();

        DataTable dtTree = new DataTable();
        DataTable dtExp = new DataTable();
        dtTree = objBLLCrew.Get_CrewDocumentList(GetCrewID(), txtSearchDoc.Text, chkArchive.Checked ? 1 : 0);
        dtExp = objBLLCrew.Get_CrewDocumentList(GetCrewID(), txtSearchDoc.Text, 0);
        //dtTree.DefaultView.Sort = "VoyageID asc";  

        TreeNode parentNode = new TreeNode("DOCUMENTS", "DOCUMENTS", getNodeImageURL(".FDC"));
        parentNode.SelectAction = TreeNodeSelectAction.Select;
        parentNode.Expand();
        BrowseTreeView.Nodes.Add(parentNode);
        GetExpiredDocGroupName(dtExp);

        DataTable dtgreyout = GetGreyoutDocs(objBLLCrew.Get_CrewDocumentList(GetCrewID(), txtSearchDoc.Text, 1), objBLLCrew.Get_CrewDocumentList(GetCrewID(), txtSearchDoc.Text, 0));
        if (dtgreyout.Rows.Count > 0)
        {
            foreach (DataRow dr in dtgreyout.Rows)
            {
                LstGreyoutName.Add(Convert.ToString(dr["DocName"]));
            }
            Session["dtgreyout"] = LstGreyoutName;
        }

        if (dtTree.Rows.Count > 0)
        {
            foreach (DataRow dr in dtTree.Rows)
            {
                string sNodeName = dr["DocName"].ToString().Replace("/", "-");
                string DocTypeName = dr["DocTypeName"].ToString().Contains("/") ? dr["DocTypeName"].ToString().Replace("/", "--") : dr["DocTypeName"].ToString();
                string DocFileExt = dr["DocFileExt"].ToString();
                string VoyID = dr["VoyageID"].ToString();
                string Vessel_Short_Name = dr["Vessel_Short_Name"].ToString();
                string Rank = dr["Rank_Short_Name"].ToString().Replace("/", "-");
                string Joining_Date = dr["Joining_Date"].ToString();
                string strGroupName = dr["GroupName"].ToString();

                string sDate = UDFLib.ConvertUserDateFormat(Joining_Date).Replace("/", "-");
                if (DocTypeName == "")
                    DocTypeName = "NEW";

                string sNodePath = "";
                if (VoyID != "0")
                {
                    sNodePath = "DOCUMENTS/Voyage Related/" + Vessel_Short_Name + "-" + Rank + "-" + sDate + "/" + strGroupName + "/" + DocTypeName + "/" + sNodeName;
                }
                else
                {
                    sNodePath = "DOCUMENTS/Perpetual/" + strGroupName + "/" + DocTypeName + "/" + sNodeName;
                }

                string NodeValue = dr["DocID"].ToString();
                string ParentNodeValue = dr["DocTypeID"].ToString();

                CreateChildNode(sNodePath, sNodeName, 1, NodeValue, ParentNodeValue, DocFileExt, VoyID);
            }

        }


    }

    private void Load_DocumentTypes()
    {
        if (ddlDocType.Items.Count == 0)
        {
            DataTable dt = objDMSBLL.GetDocumentTypeList();
            ddlDocType.DataSource = dt;
            ddlDocType.DataValueField = "DocTypeID";
            ddlDocType.DataTextField = "DocTypeName";
            ddlDocType.DataBind();
            ddlDocType.Items.Insert(0, new ListItem("-Select-", "0"));
        }

    }
    protected void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iDocTypeID = int.Parse(ddlDocType.SelectedValue);
        int iVoyRelated = objDMSAdminBLL.IsVoyageRelated(iDocTypeID);

        int PassportDocTypeId = objDMSAdminBLL.Get_DocumentTypeId("PASSPORT");
        int SeamanDocTypeId = objDMSAdminBLL.Get_DocumentTypeId("SEAMAN");


        if (iDocTypeID == PassportDocTypeId || iDocTypeID == SeamanDocTypeId)
        {
            DataTable dt = objBLLCrew.Get_CrewPassportAndSeamanDetails(GetCrewID());
            if (dt.Rows.Count > 0)
            {
                if (iDocTypeID == PassportDocTypeId)
                {
                    txtDocNo.Text = dt.Rows[0]["Passport_Number"].ToString();
                    txtDocIssuePlace.Text = dt.Rows[0]["Passport_PlaceOf_Issue"].ToString();
                    txtDocIssueDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["Passport_Issue_Date"]));
                    txtDocExpiryDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["Passport_Expiry_Date"]));
                }
                if (iDocTypeID == SeamanDocTypeId)
                {
                    txtDocNo.Text = dt.Rows[0]["Seaman_Book_Number"].ToString();
                    txtDocIssuePlace.Text = dt.Rows[0]["Seaman_Book_PlaceOf_Issue"].ToString();
                    txtDocIssueDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["Seaman_Book_Issue_date"]));
                    txtDocExpiryDate.Text = UDFLib.ConvertUserDateFormat(Convert.ToString(dt.Rows[0]["Seaman_Book_Expiry_Date"]));
                }
            }
        }

        if (iVoyRelated == 1)
        {
            string js = "parent.ShowNotification('Alert','The current document is under PERPETUAL, you are changing the type to VOYAGE RELATED DOCUMENT.The document will be moved to the CURRENT VOYAGE of the crew.',true);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgVoyageRelated", js, true);
        }
    }

    protected void LoadDocumentAttributes(string DocID)
    {
        ObjectDataSource_DocAttributeValue.SelectParameters["DocID"].DefaultValue = DocID;
        //   ObjectDataSource_DocAttributeValue.DataBind();
        //   GridView_DocAttributes.DataBind();
        pnlView_Documents.Visible = false;
        pnlDoc_Attributes.Visible = true;
    }

    /// <summary>
    /// Created for Making GroupFolder in TreeView
    /// </summary>
    /// <param name="dTreeRow"></param>
    /// <param name="dtGroups"></param>
    /// <returns></returns>
    protected string[] MakeGroupNode(DataRow dTreeRow, DataTable dtGroups)
    {
        string strGroupName = string.Empty;
        string[] strNodes = new string[2];


        DataRow[] dr = dtGroups.Select("GroupId =" + dTreeRow["GroupId"]);
        if (dr.Length > 0)
        {
            if (strNodes[0] == Convert.ToString(dr[0]["GroupName"]))
            {
                strNodes[1] += dTreeRow["DocTypeName"] + "/";
            }
            else
            {
                strNodes[0] = Convert.ToString(dr[0]["GroupName"]);
                strNodes[1] += dTreeRow["DocTypeName"] + "/";

            }
        }

        strNodes[1] = strNodes[1].Substring(0, strNodes[1].Length - 1);

        return strNodes;
    }
    protected void CreateChildNode(string NodePath, string NodeName, int NodeType, string NodeValue, string ParentNodeValue, string DocFileExt, string VogId)
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

                CreateChildNode(ParentNodePath, sN[sN.Length - 1], 0, NodeValue, ParentNodeValue, DocFileExt, VogId);

                TreeNode ParentNode1 = BrowseTreeView.FindNode(ParentNodePath);
                if (ParentNode1 != null)
                    CreateNode(ParentNode1, NodeName, NodeType, NodeValue, ParentNodeValue, DocFileExt, VogId);
            }
            else
            {
                CreateNode(ParentNode, NodeName, NodeType, NodeValue, ParentNodeValue, DocFileExt, VogId);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void CreateNode(TreeNode ParentNode, string NodeName, int NodeType, string NodeValue, string ParentNodeValue, string DocFileExt, string VoyId)
    {
        string strClass = string.Empty;
        Session["SetExpiredFolder"] = "";
        string[] str;
        string strGroupName = "", strVoyId = "";
        Session["SetExpiredFolder"] = "";
        if (Session["GroupName"] != "")
        {
            List<string> Lst = new List<string>();
            Lst = (List<string>)(Session["GroupName"]);
            foreach (string value in Lst)
            {
                str = value.Split('|');
                strGroupName = Convert.ToString(str[0]);
                strVoyId = Convert.ToString(str[1]);
                if (strGroupName.Equals(NodeName) && VoyId.Equals(strVoyId))
                {
                    Session["SetExpiredFolder"] = "1";
                }
            }
        }
        string icon = (NodeType == 0) ? getNodeImageURL("") : getNodeImageURL(DocFileExt);

        TreeNode NewNode = new TreeNode(NodeName, NodeName, icon);
        //NewNode.ToolTip = VoyId;
        NewNode.ImageToolTip = VoyId;
        if (NodeType == 0)
        {
            //  NewNode.Value = ParentNodeValue;
            NewNode.Target = ParentNodeValue;
            Session["isGrey"] = "0";
        }
        else
        {
            if (Session["isGrey"] == "0")
            {
                Session["isGrey"] = "1";

            }
            else
            {
                NewNode.ImageToolTip = "Archived" + "|" + VoyId;
                strClass = "<span style=\"color: #A9A9A9\">" + NewNode.Value + "</span>";
                NewNode.Text = strClass;
                LstGreyoutName.Add(NewNode.Value);
                Session["dtgreyout"] = LstGreyoutName;
            }

            if (txtSearchDoc.Text != "")
            {
                DataTable dtTable = new DataTable();
                if (Session["dtgreyout"] != "")
                {
                    List<string> Lst = new List<string>();
                    Lst = (List<string>)(Session["dtgreyout"]);

                    if ((Lst.Count > 0) && (Lst.Contains(NewNode.Value)))
                    {
                        strClass = "";
                        NewNode.Text = "";
                        NewNode.ImageToolTip = "Archived" + "|" + VoyId;
                        strClass = "<span style=\"color: #A9A9A9\">" + NewNode.Value + "</span>";
                        NewNode.Text = strClass;
                    }

                }
            }

            NewNode.Value = NodeValue;
            NewNode.Target = NodeValue;
        }

        NewNode.SelectAction = TreeNodeSelectAction.SelectExpand;

        if (NodeName != "")
        {
            ParentNode.ChildNodes.Add(NewNode);

            string strParent = Convert.ToString(NewNode.Parent.Text);

            if ((NodeName == "Perpetual") || (NodeName == "Voyage Related") || (strParent == "Voyage Related"))
            {
                NewNode.Expand();
            }
            else
            {
                NewNode.Collapse();
            }
        }
    }

    /// <summary>
    /// Getting FolderImages for TreeNode 
    /// </summary>
    /// <param name="extenssion"></param>
    /// <returns></returns>
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
        {
            if (Session["SetExpiredFolder"].ToString() == "1")
            {

                return "~/images/DocTree/ExpiredFolder.png";

            }
            else
            { return "~/images/DocTree/folder.gif"; }

            //return "~/images/DocTree/folder.gif"; 

        }
    }

    protected void BrowseTreeView_SelectedNodeChanged(object sender, EventArgs e)
    {
        int result = 0;
        DocumentPoperties doc;
        try
        {
            TreeNode selectedNode = BrowseTreeView.SelectedNode;
            if (selectedNode != null)
            {
                if (selectedNode.ChildNodes.Count > 0)
                {
                    BindGrid();

                    pnlView_Documents.Visible = true;
                    pnlDoc_Attributes.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClientID, "FreezeGridView();", true);
                }
                else
                {
                    if (selectedNode.Value != "")
                    {
                        if (selectedNode.Parent != null)
                        {
                            if (int.TryParse(selectedNode.Value, out result) == true)
                            {
                                doc = objDMSBLL.GetDocumentDetailsByID(int.Parse(selectedNode.Value));
                                if (doc != null)
                                {
                                    txtDocumentName.Text = doc.DocName;
                                    txtDocIssuePlace.Text = doc.PlaceOfIssue;
                                    ddlCountry.SelectedValue = doc.CountryOfIssue;
                                    //  txtDocIssueDate.Text = doc.DateOfIssue.ToString("dd/MM/yyyy");
                                    if (doc.DateOfIssue.ToString() != "01/01/0001 00:00:00")
                                    {
                                        txtDocIssueDate.Text = doc.DateOfIssue.ToString(Convert.ToString(Session["User_DateFormat"])); ;
                                    }
                                    else
                                    {
                                        txtDocIssueDate.Text = "";
                                    }
                                    if (doc.DateOfExpiry.ToString() != "01/01/0001 00:00:00")
                                    {
                                        txtDocExpiryDate.Text = doc.DateOfExpiry.ToString(Convert.ToString(Session["User_DateFormat"])); ;
                                    }
                                    else
                                    {
                                        txtDocExpiryDate.Text = "";
                                    }

                                    txtDocNo.Text = doc.DocNo;
                                    lnkCrewDocument.NavigateUrl = "../DMS/Default.aspx?ID=" + GetCrewID() + "&DocID=" + doc.DocID.ToString();
                                    LoadDocumentAttributes(selectedNode.Target);
                                    hdnDocVoyageID.Value = doc.VoyageId.ToString();

                                    if (selectedNode.ImageToolTip.Contains("Archived"))
                                    {
                                        btnSaveDocType.Enabled = false;
                                        btnSaveAndReplaceDocType.Enabled = false;
                                    }
                                    else
                                    {
                                        btnSaveDocType.Enabled = true;
                                        btnSaveAndReplaceDocType.Enabled = true;
                                    }
                                }

                                Load_DocumentTypes();
                                if (selectedNode.Parent.Text == "NEW")
                                    ddlDocType.SelectedIndex = 0;
                                else
                                    ddlDocType.Text = selectedNode.Parent.Target;
                            }
                            else
                            {
                                BindGrid();//Document is not attached
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

        finally
        {

            doc = null;
        }
    }
    protected void txtSearchDoc_TextChanged(object sender, EventArgs e)
    {
        blnTxtSearch = true;
        Reload_Documents();
    }
    protected void ImgSearchDoc_Click(object sender, ImageClickEventArgs e)
    {
        //UpdatePanel_Documents.Update();
    }
    protected void ImgClearSearch_Click(object sender, ImageClickEventArgs e)
    {
        txtSearchDoc.Text = "";
        Reload_Documents();
    }
    protected void rptDocAttributes_ItemDataBound(object sender, EventArgs e)
    {
        Bind_Autocomplete_Script();
    }

    protected void btnSaveDocType_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        Boolean Valid = true;
        int VoyageID = 0;
        string sSuccessMessage = "Document details saved.";


        try
        {
            // -- Check if voyage related document,Move to current voyage if exists--------------------------
            int iDocTypeID = int.Parse(ddlDocType.SelectedValue);
            int iVoyRelated = objDMSAdminBLL.IsVoyageRelated(iDocTypeID);

            if (iVoyRelated == 1)
            {
                DataTable dtLastVoy = objBLLCrew.Get_CrewLastVoyage(GetCrewID());

                if (dtLastVoy.Rows.Count == 0)
                {
                    string js = "parent.ShowNotification('Alert','The document type selected is VOYAGE RELATED, but there is no voyage found for the Crew. Hence, document type can not be changed.',true);";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoVoyage", js, true);
                    Valid = false;
                }
                else
                {
                    VoyageID = int.Parse(dtLastVoy.Rows[0]["ID"].ToString());
                    if (hdnDocVoyageID.Value == "" || hdnDocVoyageID.Value == "0")
                    {
                        sSuccessMessage = "Document details Saved. Document is moved to Voyage-Document Checklist.";
                    }
                }
            }
            //---------------------------------------------------------------------------------------------
            int VogId = 0, iDocID = 0, iTypeID = 0, CrewID = 0;
            string sDocName = "", sDocTypeName = "";
            TreeNode selectedNode = BrowseTreeView.SelectedNode;
            if (selectedNode != null)
            {
                VogId = selectedNode.ImageToolTip == "" ? 0 : Convert.ToInt16(selectedNode.ImageToolTip);
                iDocID = int.Parse(selectedNode.Target);
                iTypeID = int.Parse(ddlDocType.SelectedValue);
                sDocName = txtDocumentName.Text;
                sDocTypeName = ddlDocType.SelectedItem.Text;
            }
            CrewID = GetCrewID();
            string IssueDate;
            string ExpiryDate;

            foreach (RepeaterItem row in rptDocAttributes.Items)
            {
                string AttributeType = ((HiddenField)row.FindControl("HiddenField_Type")).Value;
                int iAttributeID = int.Parse(((HiddenField)row.FindControl("HiddenField_AttributeID")).Value);
                string sValue = ((TextBox)row.FindControl("txtAttributeValue")).Text;

                IssueDate = UDFLib.ConvertUserDateFormat(Convert.ToString(txtDocIssueDate.Text));
                ExpiryDate = UDFLib.ConvertUserDateFormat(Convert.ToString(txtDocExpiryDate.Text));

                if (ValidateEntry(txtDocumentName.Text.Trim(), IssueDate, ExpiryDate) == false)
                    Valid = false;
                if (Validate(sValue, AttributeType) == false)
                    Valid = false;

            }
            if (Valid == true)
            {
                // -- SAVE standard attributes first --
                int RankID = 0;
                string Staff_Rank = objBLLCrew.Get_CrewPersonalDetailsByID(GetCrewID(), "Staff_Rank");
                if (Staff_Rank != "")
                    RankID = int.Parse(Staff_Rank);

                IssueDate = UDFLib.ConvertUserDateFormat(Convert.ToString(txtDocIssueDate.Text));
                ExpiryDate = UDFLib.ConvertUserDateFormat(Convert.ToString(txtDocExpiryDate.Text));

                if (ValidateEntry(txtDocumentName.Text.Trim(), IssueDate, ExpiryDate) == true)
                {
                    int expiryMandatory = objDMSAdminBLL.Check_Document_Expiry(iTypeID);
                    if (expiryMandatory == 1 && (ExpiryDate == "" || ExpiryDate == "1900/01/01"))
                    {
                        lblMsg.Text = "ExpiryDate date is mandatory";
                        return;
                    }
                    objBLLCrew.UPDATE_CrewDocument(CrewID, iDocID, iTypeID, txtDocumentName.Text, txtDocNo.Text, txtDocIssueDate.Text, txtDocIssuePlace.Text, txtDocExpiryDate.Text, GetSessionUserID(), UDFLib.ConvertToInteger(ddlCountry.SelectedValue));

                    objBLLCrew.UPDATE_DocumentChecklist(CrewID, iDocID, int.Parse(ddlDocType.SelectedValue), txtDocumentName.Text, 1, RankID, "", txtDocNo.Text, txtDocIssueDate.Text, txtDocIssuePlace.Text, txtDocExpiryDate.Text, GetSessionUserID(), "", VogId);

                    // -- SAVE - additional attributes
                    foreach (RepeaterItem row in rptDocAttributes.Items)
                    {
                        string AttributeType = ((HiddenField)row.FindControl("HiddenField_Type")).Value;
                        int iAttributeID = int.Parse(((HiddenField)row.FindControl("HiddenField_AttributeID")).Value);
                        string sValue = ((TextBox)row.FindControl("txtAttributeValue")).Text;
                        int responseid = objBLLCrew.UPDATE_DocumentAttributeValues(iDocID, CrewID, iAttributeID, sValue, AttributeType, GetSessionUserID());
                    }
                    lblMsg.Text = sSuccessMessage;

                    objBLLCrew.UPDATE_DocTypeAndCreateAttributeValues(iDocID, GetCrewID(), sDocName, iTypeID, GetSessionUserID());

                    Reload_Documents();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
            Bind_Autocomplete_Script();
        }
    }

    protected void btnSaveAndReplaceDocType_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        try
        {
            Boolean Valid = true;
            int VoyageID = 0;
            string sSuccessMessage = "Document details saved.";

            // -- Check if voyage related document,Move to current voyage if exists--------------------------
            int iDocTypeID = int.Parse(ddlDocType.SelectedValue);
            int iVoyRelated = objDMSAdminBLL.IsVoyageRelated(iDocTypeID);

            if (iVoyRelated == 1)
            {
                DataTable dtLastVoy = objBLLCrew.Get_CrewLastVoyage(GetCrewID());

                if (dtLastVoy.Rows.Count == 0)
                {
                    string js = "parent.ShowNotification('Alert','The document type selected is VOYAGE RELATED, but there is no voyage found for the Crew. Hence, document type can not be changed.', true);";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msgNoVoyage", js, true);
                    Valid = false;
                }
                else
                {
                    VoyageID = int.Parse(dtLastVoy.Rows[0]["ID"].ToString());
                    if (hdnDocVoyageID.Value == "" || hdnDocVoyageID.Value == "0")
                    {
                        sSuccessMessage = "Document details Saved. Document is moved to Voyage-Document Checklist.";
                    }
                }
            }
            //---------------------------------------------------------------------------------------------

            TreeNode selectedNode = BrowseTreeView.SelectedNode;
            int iDocID = int.Parse(selectedNode.Target);
            int iTypeID = int.Parse(ddlDocType.SelectedValue);
            string sDocName = txtDocumentName.Text;
            string sDocTypeName = ddlDocType.SelectedItem.Text;
            int CrewID = GetCrewID();

            string IssueDate;
            string ExpiryDate;

            foreach (RepeaterItem row in rptDocAttributes.Items)
            {
                string AttributeType = ((HiddenField)row.FindControl("HiddenField_Type")).Value;
                int iAttributeID = int.Parse(((HiddenField)row.FindControl("HiddenField_AttributeID")).Value);
                string sValue = ((TextBox)row.FindControl("txtAttributeValue")).Text;

                IssueDate = Convert.ToString(txtDocIssueDate.Text);
                ExpiryDate = Convert.ToString(txtDocExpiryDate.Text);

                if (ValidateEntry(txtDocumentName.Text.Trim(), IssueDate, ExpiryDate) == false)
                    Valid = false;
                if (Validate(sValue, AttributeType) == false)
                    Valid = false;
            }

            if (Valid == true)
            {
                // -- SAVE standard attributes first --
                int RankID = 0;
                string Staff_Rank = objBLLCrew.Get_CrewPersonalDetailsByID(GetCrewID(), "Staff_Rank");
                if (Staff_Rank != "")
                    RankID = int.Parse(Staff_Rank);

                IssueDate = Convert.ToString(txtDocIssueDate.Text);
                ExpiryDate = Convert.ToString(txtDocExpiryDate.Text);

                if (ValidateEntry(txtDocumentName.Text.Trim(), IssueDate, ExpiryDate) == true)
                {
                    int expiryMandatory = objDMSAdminBLL.Check_Document_Expiry(iTypeID);
                    if (expiryMandatory == 1 && (ExpiryDate == "" || ExpiryDate == "1900/01/01"))
                    {
                        lblMsg.Text = "ExpiryDate date is mandatory";
                        return;
                    }
                    objBLLCrew.UPDATE_CrewDocument(CrewID, iDocID, iTypeID, txtDocumentName.Text, txtDocNo.Text, txtDocIssueDate.Text, txtDocIssuePlace.Text, txtDocExpiryDate.Text, GetSessionUserID(), UDFLib.ConvertToInteger(ddlCountry.SelectedValue));
                    objBLLCrew.UPDATE_DocumentChecklist(CrewID, iDocID, int.Parse(ddlDocType.SelectedValue), txtDocumentName.Text, 1, RankID, "", txtDocNo.Text, txtDocIssueDate.Text, txtDocIssuePlace.Text, txtDocExpiryDate.Text, GetSessionUserID(), "", VoyageID, 1);
                    // -- SAVE - additional attributes
                    foreach (RepeaterItem row in rptDocAttributes.Items)
                    {
                        string AttributeType = ((HiddenField)row.FindControl("HiddenField_Type")).Value;
                        int iAttributeID = int.Parse(((HiddenField)row.FindControl("HiddenField_AttributeID")).Value);
                        string sValue = ((TextBox)row.FindControl("txtAttributeValue")).Text;
                        int responseid = objBLLCrew.UPDATE_DocumentAttributeValues(iDocID, CrewID, iAttributeID, sValue, AttributeType, GetSessionUserID());
                    }
                    lblMsg.Text = sSuccessMessage;
                    objBLLCrew.UPDATE_DocTypeAndCreateAttributeValues(iDocID, GetCrewID(), sDocName, iTypeID, GetSessionUserID());
                    Reload_Documents();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
            Bind_Autocomplete_Script();
        }
    }
    protected Boolean ValidateEntry(string DocName, string IssueDate, string ExpiryDate)
    {
        Boolean ret = true;
        string msg = "";

        if (DocName == "")
        {
            ret = false;
            msg = "Document Name is mandatory";
        }
        if (IssueDate == "")
        {
            ret = false;
            msg = "Issue Date is mandatory";
        }
        if (IssueDate != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(IssueDate);
            }
            catch
            {
                ret = false;
                msg = "Invalid entry in ISSUE DATE field.";
            }
        }
        if (ExpiryDate != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(ExpiryDate);
            }
            catch
            {
                ret = false;
                msg = "Invalid entry in EXPIRY  DATE field.";
            }
        }
        if (IssueDate != "" && ExpiryDate != "")
        {
            try
            {
                DateTime dt = DateTime.Parse(IssueDate);
                DateTime dt1 = DateTime.Parse(ExpiryDate);
                if (dt > dt1)
                {
                    ret = false;
                    msg = "Issue date cannot be greater than Expiry Date";
                }
            }
            catch
            {
                ret = false;
                msg = "Invalid entry in DATE field.";
            }
        }
        if (msg != "")
        {
            string js = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgValidation", js, true);
        }
        return ret;
    }

    protected void GridView_Documents_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int ID = UDFLib.ConvertToInteger(GridView_Documents.DataKeys[e.RowIndex].Value.ToString());
            objBLLCrew.DEL_Crew_DocumentByDocID(ID, GetSessionUserID());
            BindGrid();
            LoadtreeView();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void GridView_DocAttributes_DataBound(object sender, EventArgs e)
    {
        Bind_Autocomplete_Script();
    }
    protected void Bind_Autocomplete_Script()
    {

        foreach (RepeaterItem row in rptDocAttributes.Items)
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
                    js = "parent.ShowNotification('Alert','Entered value is not a valid Date', true)";
                }

                break;
        }

        if (js != "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", js, true);
        }
        return ret;
    }
    public int GetCrewID()
    {
        try
        {
            if (Request.QueryString["ID"] != null)
            {
                return int.Parse(Request.QueryString["ID"].ToString());
            }
            else
                return 0;
        }
        catch { return 0; }
    }

    protected void ImgReloadDocuments_Click(object sender, ImageClickEventArgs e)
    {
        txtSearchDoc.Text = "";
        Reload_Documents();
    }

    /// <summary>
    /// Loading Documents on Searchtxtbox enter
    /// </summary>
    protected void Reload_Documents()
    {
        try
        {
            LoadtreeView();
            BrowseTreeView.Nodes[0].Selected = true;
            BindGrid();
            pnlView_Documents.Visible = true;
            pnlDoc_Attributes.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClientID, "FreezeGridView();", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void BindGrid()
    {
        TreeNode selectedNode = BrowseTreeView.SelectedNode;
        if (selectedNode != null)
        {
            int VogId = selectedNode.ImageToolTip == "" ? 0 : Convert.ToInt16(selectedNode.ImageToolTip);
            DataTable dt = objBLLCrew.Get_CrewDocumentList(GetCrewID(), txtSearchDoc.Text, selectedNode.Value, VogId, chkArchive.Checked ? 1 : 0);
            GridView_Documents.DataSource = dt;
            GridView_Documents.DataBind();
        }
        else
        {
            DataTable dt = objBLLCrew.Get_CrewDocumentList(GetCrewID(), txtSearchDoc.Text, "", 0, chkArchive.Checked ? 1 : 0);
            GridView_Documents.DataSource = dt;
            GridView_Documents.DataBind();
        }
    }
    protected void BindCountry()
    {
        BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
        DataTable dt = objBLLCountry.Get_CountryList();
        ddlCountry.DataSource = dt;
        ddlCountry.DataTextField = "Country";
        ddlCountry.DataValueField = "ID";

        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void GridView_Documents_Sorting(object sender, GridViewSortEventArgs e)
    {
        string SortExpression = e.SortExpression;
        string SortDirection = e.SortDirection.ToString() == "Ascending" ? "ASC" : "DESC";

        if (SortExpression != Convert.ToString(Session["Document_SortExpression"]))
        {
            Session["Document_SortExpression"] = SortExpression;
            Session["Document_SortDirection"] = "DESC";
            SortDirection = "DESC";
        }
        else
        {
            if (Convert.ToString(Session["Document_SortDirection"]) == "ASC")
                SortDirection = "DESC";
            else if(Convert.ToString(Session["Document_SortDirection"]) == "DESC")
                SortDirection = "ASC";

            Session["Document_SortDirection"] = SortDirection;
        }
        
        SortExpression = SortExpression + " " + SortDirection;

        TreeNode selectedNode = BrowseTreeView.SelectedNode;
        DataTable dt = new DataTable();
        if (selectedNode != null)
        {
            int VogId = selectedNode.ImageToolTip == "" ? 0 : Convert.ToInt16(selectedNode.ImageToolTip);
            dt = objBLLCrew.Get_CrewDocumentList(GetCrewID(), txtSearchDoc.Text, selectedNode.Value, VogId, chkArchive.Checked ? 1 : 0);
        }
        else
        {
            dt = objBLLCrew.Get_CrewDocumentList(GetCrewID(), txtSearchDoc.Text, "", 0, chkArchive.Checked ? 1 : 0);
        }

        if (dt.Rows.Count > 0)
        {
            dt.DefaultView.Sort = SortExpression;
            GridView_Documents.DataSource = dt.DefaultView.ToTable();
            GridView_Documents.DataBind();
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClientID, "FreezeGridView();", true);
    }

    /// <summary>
    /// Getting Expired Document Group Name
    /// </summary>
    /// <param name="dtTree"></param>
    protected void GetExpiredDocGroupName(DataTable dtTree)
    {
        //+ "% OR ExpiryValidation = 1 "
        var filter = string.Format("[DateOfExpiry] < #{0:yyyy-MM-dd}#", DateTime.Now);
        DataRow[] drow = dtTree.Select(filter);
        StringBuilder str = new StringBuilder();
        List<string> LstGroupName = new List<string>();

        if (drow.Length > 0)
        {
            foreach (DataRow row in drow)
            {
                str = new StringBuilder();
                str.Append(Convert.ToString(row["GroupName"]));
                str.Append("|");
                str.Append(Convert.ToString(row["VoyageId"]) == "" ? "0" : Convert.ToString(row["VoyageId"]));
                LstGroupName.Add(str.ToString());
            }
        }
        Session["GroupName"] = LstGroupName;
    }
    protected void chkArchive_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //LoadtreeView();
            Reload_Documents();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClientID, "FreezeGridView();", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    private void txtSearchDoc_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
        try
        {
            blnTxtSearch = true;
            Reload_Documents();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void ImgAddDocument_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string js = "parent.showDialog('#dvCrewDocumentUploader');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClientID, js, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }

    private DataTable GetGreyoutDocs(DataTable dt, DataTable dtMax)
    {
        DataTable Dtable = new DataTable();
        Dtable = dt.Clone();

        DataView dv = dt.DefaultView;
        string str = string.Empty;

        foreach (DataRow drow in dtMax.Rows)
        {
            str += drow["DocID"] + ",";
        }

        if (str.Length > 0)
        {
            str = str.Substring(0) == "," ? str.Remove(0) : str;
            str = str.Substring(str.Length - 1) == "," ? str.Remove(str.Length - 1) : str;
            dv.RowFilter = "DocID Not in (" + str + ")";
        }

        Dtable = dv.ToTable();

        return Dtable;
    }

}