using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UserControl_ctlRecordNavigation : System.Web.UI.UserControl
{
    //public delegate void ListNavigator(int Move);
    public delegate void RowNavigator(DataRow dr);

    //public event ListNavigator NavigateList;
    public event RowNavigator NavigateRow;

    private  int CurrentID_  
    {
        get{return Convert.ToInt32(hdfCurrentID.Value) ;}
        set{hdfCurrentID.Value= Convert.ToString(value);}
    }
    private  int RecordCount_   
    {
         get{return Convert.ToInt32(hdfRecordCount.Value) ;}
        set { hdfRecordCount.Value = Convert.ToString(value); }
    }
    private int CurrentIndex_
    {
        get { return Convert.ToInt32(hdfCurrentIndex.Value); }
        set { hdfCurrentIndex.Value = Convert.ToString(value); }
    }




    //private static List<int> IDList = new List<int>();
    //private static DataTable dt;

    //public void InitRecords(List<int> IDList_)
    //{
    //    IDList          = IDList_;
    //    CurrentIndex_   = 0;
    //    RecordCount_    = IDList.Count;
    //}

    public void InitRecords(DataTable dt_)
    {
        ViewState["dt"]  = dt_;
        CurrentIndex_   = 0;
        RecordCount_    = dt_.Rows.Count;
       
    }

    protected void btnMoveFirst_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["dt"];

            if (dt != null)
            {
                CurrentIndex_ = 0;
                NavigateRow(dt.Rows[CurrentIndex_]);
                lblCurrentPosition.Text = (CurrentIndex_ + 1) + "/" + dt.Rows.Count;

                btnMovePrev.Enabled = false;
                btnMoveFirst.Enabled = false;
                btnMoveNext.Enabled = true;
                btnMoveLast.Enabled = true;

            }
        }
        catch { }

    }
    protected void btnMovePrev_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["dt"];
            if (dt != null)
            {
                if (CurrentIndex_ > 0)
                {
                    CurrentIndex_ -= 1;
                    NavigateRow(dt.Rows[CurrentIndex_]);
                    lblCurrentPosition.Text = (CurrentIndex_ + 1) + "/" + dt.Rows.Count;

                    if (CurrentIndex_ == 0)
                    {
                        btnMovePrev.Enabled = false;
                        btnMoveFirst.Enabled = false;
                    }
                    btnMoveNext.Enabled = true;
                    btnMoveLast.Enabled = true;
                }
            }
        }
        catch { }
    }
    protected void btnMoveNext_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["dt"];
            if (dt != null && dt.Rows.Count > 0)
            {
                if (CurrentIndex_ < dt.Rows.Count - 1)
                {
                    CurrentIndex_ += 1;
                    NavigateRow(dt.Rows[CurrentIndex_]);
                    lblCurrentPosition.Text = (CurrentIndex_ + 1) + "/" + dt.Rows.Count;
                    
                    if (CurrentIndex_ == dt.Rows.Count - 1)
                    {
                        btnMoveNext.Enabled = false;
                        btnMoveLast.Enabled = false;
                    }
                    btnMovePrev.Enabled = true;
                    btnMoveFirst.Enabled = true;
                }
            }
        }
        catch { }

    }
    protected void btnMoveLast_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["dt"];
            if (dt != null && dt.Rows.Count > 0)
            {
                CurrentIndex_ = dt.Rows.Count - 1;
                NavigateRow(dt.Rows[CurrentIndex_]);
                lblCurrentPosition.Text = (CurrentIndex_ + 1) + "/" + dt.Rows.Count;

                if (CurrentIndex_ == dt.Rows.Count - 1)
                {
                    btnMoveNext.Enabled = false;
                    btnMoveLast.Enabled = false;
                }
                btnMovePrev.Enabled = true;
                btnMoveFirst.Enabled = true;
            }
        }
        catch { }


    }

    public int CurrentIndex
    {
        get { return CurrentIndex_; }
        set
        {
            DataTable dt = (DataTable)ViewState["dt"];
            if (dt != null && dt.Rows.Count > 0)
            {
                if (value >= 0 && value < dt.Rows.Count)
                {
                    CurrentIndex_ = value;
                    lblCurrentPosition.Text = (CurrentIndex_ + 1) + "/" + dt.Rows.Count;
                }
            }
        }
    }
    public int RecordCount
    {
        get { return RecordCount_; }
        set { RecordCount_ = value; }
    }

    public void MoveFirst()
    {
        btnMoveFirst_Click(null, null);
    }
    public void MoveNext()
    {
        btnMoveNext_Click(null, null);
    }
    public void MovePrev()
    {
        btnMovePrev_Click(null, null);
    }
    public void MoveLast()
    {
        btnMoveLast_Click(null,null);
    }
    public void MoveToIndex(int NewIndex_)
    {
        DataTable dt = (DataTable)ViewState["dt"];
        if (dt != null)
        {
            if (NewIndex_ < dt.Rows.Count  && NewIndex_ >=0 )
            {
                CurrentIndex_ = NewIndex_;
                NavigateRow(dt.Rows[CurrentIndex_]);
                lblCurrentPosition.Text = (CurrentIndex_ + 1) + "/" + dt.Rows.Count;
            }
        }
    }
}