using System;
using System.Configuration;
using System.Globalization;


namespace Magenic.BadgeApplication.DataAccess.EF
{
	public static class Config
	{
		public static string SMTPAddress => ConfigurationManager.AppSettings["SMTPAddress"];

		public static int SMTPPort => Convert.ToInt32( ConfigurationManager.AppSettings["SMTPPort"], CultureInfo.InvariantCulture);

		public static bool EnableSslForSMTP => ConfigurationManager.AppSettings["EnableSSLforSMTP"].Equals("true", StringComparison.CurrentCultureIgnoreCase);
    }
}
