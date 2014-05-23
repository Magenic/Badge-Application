using Csla.Security;
using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Processor
{
    public class ActvityLoader
    {
        public async Task StartAsync()
        {
            while (true)
            {
                try
                {
                    var fileName = FileLocation;
                    if (System.IO.File.Exists(fileName))
                    {
                        var connectionString =
                            string.Format(
                                "Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;",
                                fileName);

                        using (var adapter = new OleDbDataAdapter("SELECT * FROM [Data$]", connectionString))
                        {
                            var ds = new DataSet();

                            adapter.Fill(ds, "ActivityData");

                            DataTable data = ds.Tables["ActivityData"];
                            foreach (DataRow row in data.Rows)
                            {
                                var activityId = row.Field<Double>("Activity Id");
                                var adName = row.Field<string>("Magenic Username");
                                var dateOccurred = row.Field<DateTime>("Date Occurred");
                                var comments = row.Field<string>("Comments");

                                ICslaPrincipal employee = null;
                                try
                                {
                                    employee = await BusinessLogic.Security.CustomPrincipal.LoadAsync(adName);
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error<ADProcessor>(ex.Message, ex);
                                }

                                if (employee != null)
                                {
                                    var activitySubmission = SubmitActivity.CreateActivitySubmission(((ICustomIdentity)employee.Identity).EmployeeId);
                                    activitySubmission.ActivityId = (int)activityId;
                                    activitySubmission.ActivitySubmissionDate = DateTime.UtcNow;
                                    activitySubmission.Notes = "Created by automatic feed.";
                                    await activitySubmission.SaveAsync();
                                }
                            }
                        }
                        System.IO.File.Delete(fileName);
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

        private string FileLocation
        {
            get { return ConfigurationManager.AppSettings["FileLocation"]; }
        }
    }
}
