using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magenic.BadgeApplication.Common.Interfaces;


namespace Magenic.BadgeApplication.Processor
{
#if DEBUG
	public partial class ADProcessor
    {
	    internal void _insertEmployees( IEnumerable<string> employees, IAuthorizeLogOn adDal, ICustomIdentityDAL dbDal )
	    {
		    insertEmployees( employees, adDal, dbDal );
	    }

	    internal void _uploadPhotos( IAuthorizeLogOn adDal, ICustomIdentityDAL dbDal )
	    {
		    uploadPhotos( adDal, dbDal );
	    }

	    internal static void _saveManagerInformation( IEnumerable<string> employees, IAuthorizeLogOn adDal, ICustomIdentityDAL dbDal )
	    {
		    saveManagerInformation(employees,adDal, dbDal);
	    }

	    internal void _markTermDateForMissingEmployees( IAuthorizeLogOn adDal, ICustomIdentityDAL dbDal )
	    {
		    markTermDateForMissingEmployees(adDal, dbDal);
	    }

	    internal void _insertUserInfoFromAD( IAuthorizeLogOn adDal, ICustomIdentityDAL dbDal, string userName )
	    {
		    insertUserInfoFromAD( adDal, dbDal, userName );
	    }
	}
#endif	
}
