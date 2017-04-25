using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Rating : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    public void SetRating(int rating)
    {
        if (rating > 0)
        {
            if (rating == 1 ) Radio1.Checked = true;
            else if (rating == 2 ) Radio2.Checked = true;
            else if (rating == 3 ) Radio3.Checked = true;
            else if (rating == 4 ) Radio4.Checked = true;
            else if (rating == 5) Radio5.Checked = true;
        }
    }
}