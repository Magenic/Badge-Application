using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Threading;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Processor
{
    public class FileLoader
    {
        public async Task StartAsync()
        {
            while (true)
            {
                try
                {
                    var fileName = FileLocation;
                    var connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", fileName);

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Data$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "AnniversaryData");

                    DataTable data = ds.Tables["AnniversaryData"];
                    var activityId = ActivityId;

                    foreach (DataRow row in data.Rows)
                    {
                        var adName = row.Field<string>("Magenic Username");
                        var years = int.Parse(row.Field<string>("Years"));

                        var submittedActivities = await BusinessLogic.Activity.SubmittedActivityCollection.GetSubmittedActivitiesByADNameAsync(adName, activityId);

                        if (submittedActivities.Count < years)
                        {
                            var employee = await BusinessLogic.Security.CustomPrincipal.LoadAsync(adName);
                            for (var i = 0; i < (years - submittedActivities.Count); i++)
                            {
                                var activitySubmission = BusinessLogic.Activity.SubmitActivity.CreateActivitySubmission(((ICustomIdentity)employee.Identity).EmployeeId);
                                activitySubmission.ActivityId = activityId;
                                activitySubmission.ActivitySubmissionDate = DateTime.UtcNow;
                                activitySubmission.Notes = "Created by automatic feed.";
                            }
                        }
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

        private int ActivityId
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ActivityId"]); }
        }

        private string FileLocation
        {
            get { return ConfigurationManager.AppSettings["FileLocation"]; }
        }
    }
}
