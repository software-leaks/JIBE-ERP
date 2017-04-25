using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Infrastructure;

namespace SMS.Business.Infrastructure
{
    public class BLL_Infra_Thesaurus
    {
        public static DataTable Thesaurus_Tags(string Project, string Module, int UserID)
        {
            return DAL_Infra_Thesaurus.Thesaurus_Tags_DL(Project, Module, UserID);
        }
        public static DataTable Thesaurus_Tag_Options(string Project, string Module, int UserID, string FindTag)
        {
            return DAL_Infra_Thesaurus.Thesaurus_Tag_Options_DL(Project, Module, UserID, FindTag);
        }
        public static DataTable Thesaurus_Desc(int UserID, int ID)
        {
            return DAL_Infra_Thesaurus.Thesaurus_Desc_DL(UserID, ID);
        }
    }
}
