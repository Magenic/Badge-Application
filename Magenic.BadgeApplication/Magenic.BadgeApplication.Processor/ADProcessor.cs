using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Processor
{
    public class ADProcessor
    {
        public void Start()
        {
            while (true)
            {
                try
                {
                    var adDal = IoC.Container.Resolve<IAuthorizeLogOn>();
                    var dal = IoC.Container.Resolve<ICustomIdentityDAL>();

                    var employees = adDal.RetrieveActiveUsers().AsQueryable();

                    InsertEmployees(employees, adDal, dal);

                    UploadPhotos(employees, dal);

                    MarkTermDateForMissingEmployees(adDal, dal);

                    SaveManagerInformation(employees, adDal, dal);

                    foreach (var employeeADName in employees)
                    {
                        dal.SetManagerPermission(employeeADName);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error<ADProcessor>(ex.Message, ex);
                }
                finally
                {
                    Thread.Sleep(SleepInterval);
                }
            }
        }

        private void MarkTermDateForMissingEmployees(IAuthorizeLogOn adDal, ICustomIdentityDAL dal)
        {
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

        private int SleepInterval
        {
            get { return int.Parse(ConfigurationManager.AppSettings["SleepIntervalInMilliseconds"]); }
        }

        private static void SaveManagerInformation(IEnumerable<string> employees, IAuthorizeLogOn adDal, ICustomIdentityDAL dal)
        {
            foreach (var employeeADName in employees)
            {
                var employeeADInfo = adDal.RetrieveUserInformation(employeeADName);
                dal.SaveManagerInfo(employeeADInfo);
            }
        }

        private void UploadPhotos(IEnumerable<string> employees, ICustomIdentityDAL dal)
        {
            foreach (var employeeADName in employees)
            {
                var result = Task.Run(() => UploadConnectPhotoToAzure(employeeADName, dal, "Medium") && UploadConnectPhotoToAzure(employeeADName, dal, "Large"));
            }
        }

        private bool UploadConnectPhotoToAzure(string employeeADName, ICustomIdentityDAL dal, string size)
        {
            var urlFormat = ConfigurationManager.AppSettings[String.Format("{0}ProfilePhoto", size)];
            var uri = new Uri(String.Format(urlFormat, employeeADName));

            using (var webClient = new WebClient())
            {
                webClient.UseDefaultCredentials = true;
                var bytes = webClient.DownloadData(uri);
                dal.SaveEmployeePhoto(bytes, String.Format("{0}-{1}", size, employeeADName));
                return true;
            }
        }

        private void InsertEmployees(IEnumerable<string> employees, IAuthorizeLogOn adDal, ICustomIdentityDAL dal)
        {
            foreach (var employeeADName in employees)
            {
                var result = Task.Run(() => dal.RetrieveIdentityAsync(employeeADName)).Result ??
                                InsertUserInfoFromAD(adDal, dal, employeeADName);
            }
        }

        private CustomIdentityDTO InsertUserInfoFromAD(IAuthorizeLogOn adDal, ICustomIdentityDAL dal, string userName)
        {
            var userADInfo = adDal.RetrieveUserInformation(userName);
            return dal.SaveIdentity(userADInfo);
        }
    }
}
