using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Activation;
using SMS.Business.Infrastructure;
using System.Data;


namespace JiBeMenuService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class JiBeMenuService : IJiBeMenuService
    {
        BLL_Infra_MenuManagement objBLL = new BLL_Infra_MenuManagement();
        public string SYNCMENU(DataTable dt)
        {

            int i = objBLL.SyncMenu(dt);
            if (i > 0)
                return "true";
            else
                return "false";

        }
        public string Publish_Role(DataTable dt)
        {

            int i = objBLL.Publish_Role(dt);
            if (i > 0)
                return "true";
            else
                return "false";

        }
        public string Publish_RoleMenuAcces(DataTable dt)
        {
            int i = objBLL.Publish_RoleMenuAcces(dt);
            if (i > 0)
                return "true";
            else
                return "false";

        }

        public string SYNCMENUACCESS(DataTable dt)
        {

            int i = objBLL.SyncMenuAccess(dt);
            if (i > 0)
                return "true";
            else
                return "false";

        }


















    }
}

