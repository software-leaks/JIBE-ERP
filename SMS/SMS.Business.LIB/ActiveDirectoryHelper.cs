using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;

/// <summary>
/// Summary description for ActiveDirectoryHelper
/// </summary>
public class ActiveDirectoryHelper
{
	public ActiveDirectoryHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string[] getActiveUserDetails(string UserName)
    {
        string filter = string.Format("(&(ObjectClass={0})(sAMAccountName={1}))", "person", UserName);
        string domain = "192.168.0.100";
        string[] properties = new string[] { "fullname" };

        string displayName = "";
        string firstName = "";
        string lastName = "";
        string email = "";
        string jobtitle = "";
        string department = "";

        try
        {
            DirectoryEntry adRoot = new DirectoryEntry("LDAP://" + domain, null, null, AuthenticationTypes.Secure);
            DirectorySearcher searcher = new DirectorySearcher(adRoot); searcher.SearchScope = SearchScope.Subtree;
            searcher.ReferralChasing = ReferralChasingOption.All;
            searcher.PropertiesToLoad.AddRange(properties);
            searcher.Filter = filter;
            SearchResult result = searcher.FindOne();
            DirectoryEntry directoryEntry = result.GetDirectoryEntry();

            if (directoryEntry.Properties.Contains("displayName"))
                displayName = directoryEntry.Properties["displayName"][0].ToString();

            if (directoryEntry.Properties.Contains("givenName"))
                firstName = directoryEntry.Properties["givenName"][0].ToString();

            if (directoryEntry.Properties.Contains("sn"))
                lastName = directoryEntry.Properties["sn"][0].ToString();

            if (directoryEntry.Properties.Contains("mail"))
                email = directoryEntry.Properties["mail"][0].ToString();

            if (directoryEntry.Properties.Contains("title"))
                jobtitle = directoryEntry.Properties["title"][0].ToString();

            if (directoryEntry.Properties.Contains("department"))
                jobtitle = directoryEntry.Properties["department"][0].ToString();

        }
        catch
        {

        }
        return new string[] { displayName, firstName, lastName, email, jobtitle, department };
    }

}