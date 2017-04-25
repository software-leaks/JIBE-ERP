using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class CustomControl_DisplaymodeUserControl : System.Web.UI.UserControl
{
    WebPartManager _manager;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    void Page_Init(object sender, EventArgs e)
    {
        Page.InitComplete += new EventHandler(InitComplete);
    }

    void InitComplete(object sender, System.EventArgs e)
    {
        _manager = WebPartManager.GetCurrentWebPartManager(Page);

        String browseModeName = WebPartManager.BrowseDisplayMode.Name;

        // Fill the drop-down list with the names of supported display modes.
        foreach (WebPartDisplayMode mode in
          _manager.SupportedDisplayModes)
        {
            String modeName = mode.Name;
            // Make sure a mode is enabled before adding it.
            if (mode.IsEnabled(_manager))
            {
                ListItem item = new ListItem(modeName, modeName);
                DisplayModeDropdown.Items.Add(item);
            }
        }

        // If Shared scope is allowed for this user, display the 
        // scope-switching UI and select the appropriate radio 
        // button for the current user scope.
        if (_manager.Personalization.CanEnterSharedScope)
        {
            Panel2.Visible = true;
            if (_manager.Personalization.Scope ==
              PersonalizationScope.User)
                RadioButton1.Checked = true;
            else
                RadioButton2.Checked = true;
        }
    }

    // Change the page to the selected display mode.
    protected void DisplayModeDropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        String selectedMode = DisplayModeDropdown.SelectedValue;

        WebPartDisplayMode mode =
         _manager.SupportedDisplayModes[selectedMode];
        if (mode != null)
            _manager.DisplayMode = mode;
    }

    // Set the selected item equal to the current display mode.
    void Page_PreRender(object sender, EventArgs e)
    {
        ListItemCollection items = DisplayModeDropdown.Items;
        int selectedIndex =
          items.IndexOf(items.FindByText(_manager.DisplayMode.Name));
        DisplayModeDropdown.SelectedIndex = selectedIndex;
    }

    // Reset all of a user's personalization data for the page.
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        _manager.Personalization.ResetPersonalizationState();
    }

    // If not in User personalization scope, toggle into it.
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (_manager.Personalization.Scope ==
          PersonalizationScope.Shared)
            _manager.Personalization.ToggleScope();
    }

    // If not in Shared scope, and if user has permission, toggle 
    // the scope.
    protected void RadioButton2_CheckedChanged(object sender,
      EventArgs e)
    {
        if (_manager.Personalization.CanEnterSharedScope &&
            _manager.Personalization.Scope ==
              PersonalizationScope.User)
            _manager.Personalization.ToggleScope();
    }

}
