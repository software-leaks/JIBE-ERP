using System;
using System.Collections.Generic;
using System.Web.UI;

using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.WebControls.CustomWebParts
{
    public class cutWebPartZone :  WebPartZone
    {
        protected override void OnCreateVerbs(System.Web.UI.WebControls.WebParts.WebPartVerbsEventArgs e)
        {
              
            System.Web.UI.WebControls.WebParts.WebPartVerb verbRefresh = new System.Web.UI.WebControls.WebParts.WebPartVerb("RefreshVerb", "refresh_snippet(this);return false;");
      
            verbRefresh.Text = "Refresh";
            

            verbRefresh.ImageUrl = "~/Images/dash-refresh-icon.png";
          
            WebPartVerb[] newVerbs = new WebPartVerb[] { verbRefresh };

            e.Verbs = new WebPartVerbCollection(e.Verbs, newVerbs);
            base.OnCreateVerbs(e);

        }


    }
}


