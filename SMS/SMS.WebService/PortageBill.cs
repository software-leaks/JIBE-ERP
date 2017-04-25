using System;
using System.Web.Services;
using System.Collections.Generic;
using SMS.Business.PortageBill;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;


public partial class JibeWebService
{

    [WebMethod]
    public string asyncGet_CaptCash_Items_Attachments(int Vessel_ID, int CaptCash_Details_ID)
    {
        UDCHyperLink aLink = new UDCHyperLink("DocURL", "../uploads/CrewAccount", new string[] { }, new string[] { }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("Doc_Name", aLink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_PB_PortageBill.Get_CaptCash_Items_Attachments(Vessel_ID, CaptCash_Details_ID),
           new string[] { },
           new string[] { "Doc_Name" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left" },
           "CreateHtmlTableFromDataTable-table",
           "CreateHtmlTableFromDataTable-DataHedaer",
           "CreateHtmlTableFromDataTable-Data");
    }

    [WebMethod]
    public string asyncGet_Portage_Bill_Attachments(int Vessel_ID, string month, string year, int Doc_type)
    {

        UDCHyperLink aLink = new UDCHyperLink("DocURL", "../uploads/CrewAccount", new string[] { }, new string[] { }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("Doc_Name", aLink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_PB_PortageBill.Get_Portage_Bill_Attachments(Vessel_ID, month, year, Doc_type),
           new string[] { },
           new string[] { "Doc_Name" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left" },
           "CreateHtmlTableFromDataTable-table",
           "CreateHtmlTableFromDataTable-DataHedaer",
           "CreateHtmlTableFromDataTable-Data");
    }



    [WebMethod]
    public string asyncACC_GET_Allotment_Flag(int Allotment_ID, int Vessel_ID, string PBill_Date)
    {
        DateTime dtDate = Convert.ToDateTime(PBill_Date);
       
        DataSet dsRmk = BLL_PB_PortageBill.ACC_GET_Allotment_Flag(Allotment_ID, Vessel_ID, dtDate);
                GridView gvFlag = new GridView();
        gvFlag.HeaderStyle.CssClass = "CreateHtmlTableFromDataTable-DataHedaer";
        gvFlag.RowStyle.CssClass = "CreateHtmlTableFromDataTable-Data";
        gvFlag.AutoGenerateColumns = false;
        gvFlag.DataKeyNames = new string[] { "Flag_ID" };
        gvFlag.BorderWidth = 0;

        
        BoundField User = new BoundField();
        User.DataField = "CreatedBy";
        User.HeaderText = "User";
        BoundField Date = new BoundField();
        Date.HeaderStyle.Width = 70;
        Date.DataField = "CreatedOn";
        Date.HeaderText = "Date";
        BoundField Message = new BoundField();
        Message.DataField = "Remark";
        Message.HeaderText = "Message";

        BoundField Attachcol = new BoundField();
        Attachcol.DataField = "CreatedBy";
        Attachcol.HeaderText = "Attachments";

        gvFlag.Columns.Add(User);
        gvFlag.Columns.Add(Date);
        gvFlag.Columns.Add(Message);
        gvFlag.Columns.Add(Attachcol);


        gvFlag.DataSource = dsRmk.Tables[0];
        gvFlag.DataBind();

        foreach (GridViewRow gr in gvFlag.Rows)
        {
            GridView gvAttach = new GridView();
            gvAttach.AutoGenerateColumns = false;
            gvAttach.BorderWidth = 0;
            gvAttach.ShowHeader = false;
            gvAttach.RowStyle.CssClass = "CreateHtmlTableFromDataTable-Data-Attachment";

            HyperLinkField LinkAttch = new HyperLinkField();
            LinkAttch.DataNavigateUrlFields = new string[] { "Attachment_Path" };
            LinkAttch.DataTextField = "Attachment_Name";
            LinkAttch.Target = "blank";

            gvAttach.Columns.Add(LinkAttch);
            gvAttach.DataSource = dsRmk.Tables[1].AsEnumerable().Where(a => a["Flag_ID"].ToString().Equals(gvFlag.DataKeys[gr.RowIndex].Values["Flag_ID"].ToString())).AsDataView().ToTable();
            gvAttach.DataBind();

            gr.Cells[3].Text = "";
            gr.Cells[3].Controls.Add(gvAttach);
        }




        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        gvFlag.RenderControl(htmlWrite);
               
        return htmlWrite.InnerWriter.ToString();

    }



    [WebMethod]
    public string asyncGet_Side_Letter(int Voyage_ID)
    {
        return UDFLib.CreateHtmlTableFromDataTable(BLL_PB_PortageBill.GET_Side_Letter(Voyage_ID),
            new string[] { "Amount(monthly)", "Total Days Served","Total Amount" },
            new string[] { "SD_AMT", "DAYS_SERVED", "TOTAL_AMT" },

            new string[] {"right","center","right"},"Side Letter" );

    }


    //[WebMethod]
    //public string asyncGet_Bank_Account_Status(int BankID)
    //{

    //    //return BLL_PB_PortageBill.Get_Bank_Account_Status(BankID).ToString();
    //}
    [WebMethod]
    public string asyncGet_CTM_Attachments(int CTM_ID,int Vessel_ID)
    {
        UDCHyperLink aLink = new UDCHyperLink("DocURL", "../uploads/CrewAccount", new string[] { }, new string[] { }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("Doc_Name", aLink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_PB_PortageBill.Get_CTM_Attachments(CTM_ID, Vessel_ID),
           new string[] {"Attachments" },
           new string[] { "Doc_Name" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "center" },
           "CreateHtmlTableFromDataTable-table",
           "CreateHtmlTableFromDataTable-DataHedaer",
           "CreateHtmlTableFromDataTable-Data");
    }
    [WebMethod]
    public string Get_RJBDetails(int ID)
    {
        DataTable dt = BLL_PortageBill.Get_RJBDetails(ID);
        return UDFLib.CreateHtmlTableFromDataTable(dt, new string[] { }, new string[] { "RJB_More_Info" }, "");
    }
}

