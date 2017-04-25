using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ucCustomPager : System.Web.UI.UserControl
{
    // get the isCountRecord's value from main page and set the value for current page index and current section( checking while setting the value for isCountRecord property )  
    protected void Page_Load(object sender, EventArgs e)
    {
        BuildPagers();
        if (!IsPostBack)
        {
            ListItem item = ddlPageSize.Items.FindByValue(hdfPageSize.Value);
            if (item != null)
                item.Selected = true;
        }
        if (IsPostBack)
        {

            BindPaging();
        }

        if (hdfcountTotalRec.Value == "0")
        {

            this.Visible = false;
        }
        else
        {

            this.Visible = true;
        }

        if (!AlwaysGetRecordsCount)
        {
            isCountRecord = 0;
        }
        else
        {
            isCountRecord = 1;
        }
    }

    private void BindPaging()
    {
        if (((CurrentPageIndex * 10)-9) > Convert.ToInt32(CountTotalRec) && CurrentPageIndex !=1)
        {
            CurrentPageIndex = 1;//Int32.Parse((Convert.ToDecimal(Convert.ToDecimal(hdfcountTotalRec.Value) / Convert.ToDecimal(hdfPageSize.Value)).ToString("0.00").Split(new char[] { '.' })[1])) > 0 ? ((Convert.ToInt32((Convert.ToInt32(hdfcountTotalRec.Value) / Convert.ToInt32(hdfPageSize.Value))) + 1)) : (Convert.ToInt32(hdfcountTotalRec.Value) / Convert.ToInt32(hdfPageSize.Value));
            CurrentPageSection = "1";//( Int32.Parse((Convert.ToDecimal(CurrentPageIndex / (Convert.ToDecimal(10))).ToString("0.00").Split(new char[] { '.' })[1])) > 0 ?(Convert.ToDecimal(CurrentPageIndex / (Convert.ToDecimal(10)))+1):(Convert.ToDecimal(CurrentPageIndex / (Convert.ToDecimal(10))))).ToString();
            BindDataItem();
        }

        lblPaging.Controls.Clear();
        int i = Convert.ToInt32(hdfCurrentPageSection.Value);
        lblTotalPages.Text = "";


        if (Convert.ToInt32(hdfcountPerRenderPage.Value) != 0 || Convert.ToInt32(hdfcountTotalRec.Value) != 0)
        {
            int TotalPages = Int32.Parse((Convert.ToDecimal(Convert.ToDecimal(hdfcountTotalRec.Value) / Convert.ToDecimal(hdfPageSize.Value)).ToString("0.00").Split(new char[] { '.' })[1])) > 0 ? ((Convert.ToInt32((Convert.ToInt32(hdfcountTotalRec.Value) / Convert.ToInt32(hdfPageSize.Value))) + 1)) : (Convert.ToInt32(hdfcountTotalRec.Value) / Convert.ToInt32(hdfPageSize.Value));
            int tillLoop = Convert.ToInt32(hdfCurrentPageSection.Value) + 10 > TotalPages ? TotalPages : Convert.ToInt32(hdfCurrentPageSection.Value) + 10;

            string strRecordCountCaption = "";
            if (hdfRecordCountCaption.Value.Trim() != "") //check if hdfRecordCountCaption.Value is not "" 
            {
                strRecordCountCaption = "&nbsp;&nbsp;" + hdfRecordCountCaption.Value + " : " + hdfcountTotalRec.Value;
            }

            lblTotalPages.Text = " [ Total Pages : " + TotalPages.ToString() + strRecordCountCaption + " ] ";

            for (int k = 0; k < 10; k++)
            {
                if (i <= tillLoop)
                {
                    string c = i.ToString(); ;
                    LinkButton lnk = new LinkButton();
                    lnk.ID = "lnk" + c;
                    lnk.CausesValidation = false;
                    lnk.CssClass = "Paging-Custom";
                    lnk.ForeColor = System.Drawing.Color.Black;
                    lnk.Text = c.ToString();
                    //lnk.BorderStyle = BorderStyle.Solid;
                    //lnk.BorderWidth = 1;
                    //lnk.BorderColor = System.Drawing.Color.LightGray;
                    lnk.CommandArgument = c.ToString();
                    lnk.Command += new CommandEventHandler(Page_List_lnk);
                    if (i == Convert.ToInt32(hdfPageIndex.Value))
                    {
                        //lnk.BackColor = System.Drawing.Color.LightGray;
                        lnk.CssClass = "Paging-Custom Paging-Selected";
                    }
                    lblPaging.Controls.Add(lnk);
                    i++;
                }
            }
        }

    }

    private void BuildPagers()
    {
        if (((int.Parse(hdfPageIndex.Value)) - 1) > 0 && Convert.ToInt32(hdfCurrentPageSection.Value) > 1)
        {
            prev.Visible = true;
            first.Visible = true;
        }
        else
        {
            prev.Visible = false;
            first.Visible = false;
        }

        if (int.Parse(hdfPageIndex.Value) * int.Parse(hdfPageSize.Value) > int.Parse(hdfcountTotalRec.Value) || (Convert.ToInt32(hdfCurrentPageSection.Value) + 9) * int.Parse(hdfPageSize.Value) > int.Parse(hdfcountTotalRec.Value))
        {
            next.Visible = false;
            last.Visible = false;
        }
        else
        {
            next.Visible = true;
            last.Visible = true;
        }
    }

    protected void next_Click(object sender, EventArgs e)
    {
        hdfCurrentPageSection.Value = (Convert.ToInt32(hdfCurrentPageSection.Value) + 10).ToString();
        Page_List(sender, e);
    }

    protected void prev_Click(object sender, EventArgs e)
    {
        hdfCurrentPageSection.Value = (Convert.ToInt32(hdfCurrentPageSection.Value) - 10).ToString();
        Page_List(sender, e);
    }

    protected void last_Click(object sender, EventArgs e)
    {
        int TotalPages = Int32.Parse((Convert.ToDecimal(Convert.ToDecimal(hdfcountTotalRec.Value) / Convert.ToDecimal(hdfPageSize.Value)).ToString("0.00").Split(new char[] { '.' })[1])) > 0 ? ((Convert.ToInt32((Convert.ToInt32(hdfcountTotalRec.Value) / Convert.ToInt32(hdfPageSize.Value))) + 1)) : (Convert.ToInt32(hdfcountTotalRec.Value) / Convert.ToInt32(hdfPageSize.Value));


        hdfCurrentPageSection.Value = (Int32.Parse((Convert.ToDecimal(TotalPages) / 10).ToString("0.00").Split(new char[] { '.' })[1]) > 0 ? ((Convert.ToInt32((TotalPages / 10)) + 1) * 10) - 9 : (TotalPages - 9)).ToString();
        hdfPageIndex.Value = TotalPages.ToString();
        Page_List(sender, e);
    }

    protected void first_Click(object sender, EventArgs e)
    {
        hdfCurrentPageSection.Value = "1";
        hdfPageIndex.Value = "1";
        Page_List(sender, e);
    }

    public void Page_List(object sender, EventArgs e)
    {
        if (((ImageButton)sender).ID == "prev")
        {
            if (!String.IsNullOrEmpty(hdfPageIndex.Value))
            {
                if (((int.Parse(hdfPageIndex.Value)) - 1) > 0)
                {
                    hdfPageIndex.Value = hdfCurrentPageSection.Value;
                }
            }
        }

        else if (((ImageButton)sender).ID == "next")
        {
            if (!String.IsNullOrEmpty(hdfPageIndex.Value))
            {
                if (int.Parse(hdfPageIndex.Value) * int.Parse(hdfPageSize.Value) < int.Parse(hdfcountTotalRec.Value))
                {
                    hdfPageIndex.Value = hdfCurrentPageSection.Value;
                }
            }
        }

        else if (((ImageButton)sender).ID == "last")
        {
            if (!String.IsNullOrEmpty(hdfPageIndex.Value))
            {
                if (int.Parse(hdfPageIndex.Value) * int.Parse(hdfPageSize.Value) < int.Parse(hdfcountTotalRec.Value))
                {
                    hdfPageIndex.Value = Convert.ToString((int.Parse(hdfcountTotalRec.Value) / int.Parse(hdfPageSize.Value)));
                }
            }
        }

        else if (((ImageButton)sender).ID == "first")
        {
            if (String.IsNullOrEmpty(hdfPageIndex.Value))
                hdfPageIndex.Value = "1";

            if (((int.Parse(hdfPageIndex.Value)) - 1) > 0)
                hdfPageIndex.Value = "1";
        }

        // BindItems
        //if (!AlwaysGetRecordsCount)
        //{
        //    isCountRecord = 0;
        //}

        BindDataItem();
        BuildPagers();
        BindPaging();
    }

    public void Page_List_lnk(object sender, EventArgs e)
    {
       

        string[] charval = { "lnk" };
        string[] strlId = (((LinkButton)sender).ID).Split(charval, StringSplitOptions.RemoveEmptyEntries);
        string pageIndex = (((LinkButton)sender).ID).Remove(0, 3);


        if (!String.IsNullOrEmpty(pageIndex))
        {
            CurrentPageIndex = int.Parse(pageIndex);
            //if (!AlwaysGetRecordsCount)
            //{
            //    isCountRecord = 0;
            //}
            // BindItems
            BindDataItem();
            BindPaging();
            BuildPagers();

        }

    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrentPageSection = "1";
        CurrentPageIndex = 1;
        //if (!AlwaysGetRecordsCount)
        //{
        //    isCountRecord = 0;
        //}
        PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        BindDataItem();
        BuildPagers();
        BindPaging();
    }

    public void BuildPager()
    {
        BuildPagers();

        BindPaging();

        if (hdfcountTotalRec.Value == "0")
        {

            this.Visible = false;
        }
        else
        {

            this.Visible = true;
        }
    }

    public delegate void BindDataItemEventHandler();

    public event BindDataItemEventHandler BindDataItem;

    #region property

    public string CurrentPageSection
    {
        get { return hdfCurrentPageSection.Value; }
        set { hdfCurrentPageSection.Value = value; }
    }

    public string CountPerRenderPage
    {
        get { return hdfcountPerRenderPage.Value; }
        set { hdfcountPerRenderPage.Value = value; }
    }

    public string CountTotalRec
    {
        get { return hdfcountTotalRec.Value; }
        set { hdfcountTotalRec.Value = value; }
    }

    public int PageSize
    {
        get { return Convert.ToInt32(hdfPageSize.Value); }
        set { hdfPageSize.Value = Convert.ToString(value); }
    }

    public int CurrentPageIndex
    {
        get { return Convert.ToInt32(hdfPageIndex.Value); }
        set { hdfPageIndex.Value = Convert.ToString(value); }
    }

    public int isCountRecord
    {
        get { return Convert.ToInt32(hdfisCountRecord.Value); }
        set
        {
            hdfisCountRecord.Value = Convert.ToString(value);

            //if (Convert.ToInt32(value) == 1)
            //{
            //    if (AlwaysGetRecordsCount)
            //    {
            //        //CurrentPageIndex = 1;
            //        //CurrentPageSection = "1";
            //    }
            //}
        }
    }

    public string RecordCountCaption
    {
        get { return hdfRecordCountCaption.Value; }
        set { hdfRecordCountCaption.Value = value; }
    }

    public bool AlwaysGetRecordsCount
    {
        get { return Convert.ToBoolean(hdfAlwaysGetRecordsCount.Value); }
        set { hdfAlwaysGetRecordsCount.Value = value.ToString(); }
    }

    private bool IsClickedOnPager
    {
        get { return Convert.ToBoolean(hdfIsClickedOnPager.Value); }
        set { hdfIsClickedOnPager.Value = value.ToString(); }
    }

    #endregion
}