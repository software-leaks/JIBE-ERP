using System.Linq;
using System.Web;
using SMS.Data.PURC;
using System.Data;
using SMS.Properties;
using SMS.Data;
using System;


/// <summary>
/// Summary description for BondItem
/// </summary>
namespace SMS.Business.PURC
{
    public partial class BLL_PURC_Purchase
    {

        DAL_PURC_BondItem objBondItem = new DAL_PURC_BondItem();

        public DataTable getItemList(string SystemCode)
        {

            return objBondItem.getItemList(SystemCode);

        }

        public DataTable getStaffList()
        {

            return objBondItem.getStaffList();

        }

        public int getROb(string ItemRefCode)
        {

            return objBondItem.getROb(ItemRefCode);

        }

        public int ExequetString(string sqlQuery)
        {

            return objBondItem.ExequetString(sqlQuery);

        }

        public DataTable GetBondConsumption(string strFromdate, string strTodate, string VesselCode)
        {

            return objBondItem.GetBondConsumption(strFromdate, strTodate, VesselCode);

        }

    }
}
