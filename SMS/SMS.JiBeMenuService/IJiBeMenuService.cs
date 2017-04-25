using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;



namespace JiBeMenuService
{
    [ServiceContract]
    public interface IJiBeMenuService
    {
        [OperationContract]
        string SYNCMENU(DataTable dt);

        [OperationContract]
        string Publish_Role(DataTable dt);

        [OperationContract]
        string Publish_RoleMenuAcces(DataTable dt);

        [OperationContract]
        string SYNCMENUACCESS(DataTable dt);









    }
}

