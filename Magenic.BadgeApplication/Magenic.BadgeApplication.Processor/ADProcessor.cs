using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace Magenic.BadgeApplication.Processor
{
    public partial class ADProcessor
    {
        public void Start()
        {
            while (true)
            {
                try
                {
	                adCycle();
                }
                catch (Exception ex)
                {
                    Logger.Error<ADProcessor>(ex.Message, ex);
                }
                finally
                {
                    Thread.Sleep(sleepInterval);
                }
            }
        }

	    public void adCycle()
	    {
			IAuthorizeLogOn adDal = IoC.Container.Resolve<IAuthorizeLogOn>();
			ICustomIdentityDAL dbDal = IoC.Container.Resolve<ICustomIdentityDAL>();

			IQueryable<string> employees = adDal.RetrieveActiveUsers().AsQueryable();

			insertEmployees(employees, adDal, dbDal);

			uploadPhotos(adDal, dbDal);

			markTermDateForMissingEmployees(adDal, dbDal);

			saveManagerInformation(employees, adDal, dbDal);

			foreach (string employeeADName in employees)
			{
				dbDal.SetManagerPermission(employeeADName);
			}
		}


        private void markTermDateForMissingEmployees(IAuthorizeLogOn adDal, ICustomIdentityDAL dal)
        {
			// ZISS: this code should be replaced by property injection.0
            var userCollectionDal = IoC.Container.Resolve<IUserCollectionDAL>();
            var userCollection = userCollectionDal.GetActiveAdUsers();

            foreach (var userName in userCollection)
            {
                if (adDal.RetrieveUserInformation(userName) == null)
                {
                    dal.SetTerminationDate(userName, DateTime.UtcNow);
                }
            }
        }

        private int sleepInterval
        {
            get { return int.Parse(ConfigurationManager.AppSettings["SleepIntervalInMilliseconds"]); }
        }

        private static void saveManagerInformation(IEnumerable<string> employees, IAuthorizeLogOn adDal, ICustomIdentityDAL dal)
        {
            foreach (var employeeADName in employees)
            {
                var employeeADInfo = adDal.RetrieveUserInformation(employeeADName);
                dal.SaveManagerInfo(employeeADInfo);
            }
        }

        private void uploadPhotos(IAuthorizeLogOn adDal, ICustomIdentityDAL dal)
        {
            var allEmployeePhotos = adDal.RetrieveUsersAndPhotos();
            foreach (var kvp in allEmployeePhotos)
            {
	            try
	            {
		            dal.SaveEmployeePhoto( kvp.Value, kvp.Key );
	            }
	            catch ( Exception ex )
	            {
		            break;
	            }
            }
        }

        private void insertEmployees(IEnumerable<string> employees, IAuthorizeLogOn adDal, ICustomIdentityDAL dal)
        {
            foreach (var employeeADName in employees)
            {
                insertUserInfoFromAD(adDal, dal, employeeADName);
            }
        }

        private void insertUserInfoFromAD(IAuthorizeLogOn adDal, ICustomIdentityDAL dal, string userName)
        {
            var userADInfo = adDal.RetrieveUserInformation(userName);
            if (userADInfo != null)
            {
                dal.SaveIdentity(userADInfo);
            }
        }
    }
}
