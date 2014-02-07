using System.Configuration;
using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
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

                    var employees = adDal.RetrieveActiveUsers();

                    InsertEmployees(employees, adDal, dal);

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

        private void InsertEmployees(IEnumerable<string> employees, IAuthorizeLogOn adDal, ICustomIdentityDAL dal)
        {
            try
            {
                foreach (var employeeADName in employees)
                {
                    var result = Task.Run(() => dal.RetrieveIdentityAsync(employeeADName)).Result ??
                                 InsertUserInfoFromAD(adDal, dal, employeeADName);
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private CustomIdentityDTO InsertUserInfoFromAD(IAuthorizeLogOn adDal, ICustomIdentityDAL dal, string userName)
        {
            var userADInfo = adDal.RetrieveUserInformation(userName);
            return dal.SaveIdentity(userADInfo);
        }
    }
}
