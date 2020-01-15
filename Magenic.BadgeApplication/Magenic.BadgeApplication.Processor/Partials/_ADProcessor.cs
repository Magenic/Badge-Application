using System.Collections.Generic;
using Magenic.BadgeApplication.Common.Interfaces;


namespace Magenic.BadgeApplication.Processor
{
#if DEBUG
    public partial class ADProcessor
    {
        internal void InsertEmployees( IEnumerable<string> employees, IAuthorizeLogOn adDal, ICustomIdentityDAL dbDal )
        {
            insertEmployees( employees, adDal, dbDal );
        }

        internal void UploadPhotos( IAuthorizeLogOn adDal, ICustomIdentityDAL dbDal )
        {
            uploadPhotos( adDal, dbDal );
        }

        internal static void SaveManagerInformation( IEnumerable<string> employees, IAuthorizeLogOn adDal, ICustomIdentityDAL dbDal )
        {
            saveManagerInformation( employees, adDal, dbDal );
        }

        internal void MarkTermDateForMissingEmployees( IAuthorizeLogOn adDal, ICustomIdentityDAL dbDal )
        {
            markTermDateForMissingEmployees( adDal, dbDal );
        }

        internal void InsertUserInfoFromAD( IAuthorizeLogOn adDal, ICustomIdentityDAL dbDal, string userName )
        {
            insertUserInfoFromAD( adDal, dbDal, userName );
        }
    }
#endif
}
