using System;
using System.Collections.Generic;

using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


public class DataGridTemplate : ITemplate
{
    ListItemType templateType;
    string columnName;

    public DataGridTemplate(ListItemType type, string colname)
    {
        templateType = type;
        columnName = colname;
    }

    public void InstantiateIn(System.Web.UI.Control container)
    {
        Literal lc = new Literal();
        switch (templateType)
        {
            case ListItemType.Header:
                lc.Text = "<B>" + columnName + "</B>";
                container.Controls.Add(lc);
                break;
            case ListItemType.Item:
                lc.Text = "Item " + columnName;
                container.Controls.Add(lc);
                break;
            case ListItemType.EditItem:
                TextBox tb = new TextBox();
                tb.Text = "";
                container.Controls.Add(tb);
                break;
            case ListItemType.Footer:
                lc.Text = "<I>" + columnName + "</I>";
                container.Controls.Add(lc);
                break;
        }
    }
}


