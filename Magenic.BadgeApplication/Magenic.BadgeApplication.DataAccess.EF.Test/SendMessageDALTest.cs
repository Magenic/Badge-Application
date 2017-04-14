using System;
using System.Collections.Generic;
using Magenic.BadgeApplication.DataAccess.EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magenic.BadgeApplication.DataAccess.EF.Test
{
	[TestClass]
	public class SendMessageDALTest
	{
		[TestMethod]
		public void SendActivityNotifications_Harness()
		{
			SendMessageDAL sm = new SendMessageDAL();
			sm.SendActivityNotifications();
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void getPeopleToEmail_Harness()
		{
			SendMessageDAL sm = new SendMessageDAL();

			using ( Entities context = new Entities() )
			{
				try
				{
					var peopleToEmail = sm._GetPeopleToEmail(context);
					Assert.IsTrue(true);
				}
				catch ( Exception e )
				{
					Assert.IsTrue( false );
				}
				
			}

				
			
		}

		[TestMethod]
		public void addEmailAddress_Test()
		{
			var emails = new List<string>();
			SendMessageDAL sm = new SendMessageDAL();

			IList<Employee> employees = new List<Employee>();
			int? managerId = 88;
			IList<string> emailAddress = new List<string>();
			sm._AddEmailAddress(employees, managerId, emailAddress);
		}

		[TestMethod]
		public void addEmailAddresses_Test()
		{
			SendMessageDAL sm = new SendMessageDAL();
			
		}

		[TestMethod]
		public void getEmailAddresses_Test()
		{
			SendMessageDAL sm = new SendMessageDAL();
			// sm._GetEmailAddressses()
		}

		[TestMethod]
		public void getEmployees_Test()
		{
			SendMessageDAL sm = new SendMessageDAL();
			// sm._GetEmployees()
		}
	}
}
