using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.DataAccess.EF;
using Moq;


namespace Magenic.BadgeApplication.Processor.Tests
{
	[TestClass]
	public class ADProcessorTest
	{
		[TestMethod]
		public void adCycle_Harness()
		{
			AutofacBootstrapper.Init();

			ADProcessor adProcessor = new ADProcessor();
			adProcessor.adCycle();
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void insertEmployees_Test()
		{
			var adp = new ADProcessor();

			IEnumerable<string> employees = new List<string> { "emp1", "emp2", "emp3" };
			var adDalMock = new Mock<IAuthorizeLogOn>();
			adDalMock.Setup( a => a.RetrieveUserInformation( "emp1", It.IsAny<string>() ) ).Returns( new AuthorizeLogOnDTO { UserName = "adEmp1" } );
			adDalMock.Setup( a => a.RetrieveUserInformation( "emp2", It.IsAny<string>() ) ).Returns( (AuthorizeLogOnDTO)null );
			adDalMock.Setup( a => a.RetrieveUserInformation( "emp3", It.IsAny<string>() ) ).Returns( new AuthorizeLogOnDTO { UserName = "adEmp3" } );

			var dbDalMock = new Mock<ICustomIdentityDAL>();
			dbDalMock.Setup( d => d.SaveIdentity( It.IsAny<AuthorizeLogOnDTO>() ) );
			dbDalMock.Setup( d => d.SaveIdentity( It.IsAny<AuthorizeLogOnDTO>() ) );
			adp._insertEmployees( employees, adDalMock.Object, dbDalMock.Object );

			adDalMock.VerifyAll();
			dbDalMock.VerifyAll();
		}


		[TestMethod]
		public void insertUserInfoFromAD_Test()
		{
			var adp = new ADProcessor();
			var adDalMock = new Mock<IAuthorizeLogOn>();
			adDalMock.Setup( a => a.RetrieveUserInformation( "emp1", It.IsAny<string>() ) ).Returns( new AuthorizeLogOnDTO { UserName = "adEmp1" } );
			adDalMock.Setup( a => a.RetrieveUserInformation( "emp2", It.IsAny<string>() ) ).Returns( (AuthorizeLogOnDTO)null );
			adDalMock.Setup( a => a.RetrieveUserInformation( "emp3", It.IsAny<string>() ) ).Returns( new AuthorizeLogOnDTO { UserName = "adEmp3" } );

			var dbDalMock = new Mock<ICustomIdentityDAL>();
			dbDalMock.Setup( d => d.SaveIdentity( It.IsAny<AuthorizeLogOnDTO>() ) );
			dbDalMock.Setup( d => d.SaveIdentity( It.IsAny<AuthorizeLogOnDTO>() ) );

			foreach ( var s in new List<string> { "emp1", "emp2", "emp3" } )
				adp._insertUserInfoFromAD( adDalMock.Object, dbDalMock.Object, s );

			adDalMock.VerifyAll();
			dbDalMock.VerifyAll();
		}



		[TestMethod]
		public void markTermDateForMissingEmployees_Test()
		{
			var builder = new ContainerBuilder();
			var ucDalMock = new Mock<IUserCollectionDAL>();
			ucDalMock.Setup( u => u.GetActiveAdUsers() ).Returns( new List<string> { "adUser1", "adUser2", "adUser3", "adUser4" } );

			builder.RegisterType<UserCollectionDAL>().As<IUserCollectionDAL>();
			builder.RegisterInstance( ucDalMock.Object );
			IoC.Container = builder.Build();

			var adp = new ADProcessor();
			var adDalMock = new Mock<IAuthorizeLogOn>();
			adDalMock.Setup( a => a.RetrieveUserInformation( "adUser1", It.IsAny<string>() ) ).Returns( new AuthorizeLogOnDTO { UserName = "adUser1" } );
			adDalMock.Setup( a => a.RetrieveUserInformation( "adUser2", It.IsAny<string>() ) ).Returns( (AuthorizeLogOnDTO)null );
			adDalMock.Setup( a => a.RetrieveUserInformation( "adUser3", It.IsAny<string>() ) ).Returns( new AuthorizeLogOnDTO { UserName = "adUser3" } );
			adDalMock.Setup( a => a.RetrieveUserInformation( "adUser4", It.IsAny<string>() ) ).Returns( (AuthorizeLogOnDTO)null );

			var dbDalMock = new Mock<ICustomIdentityDAL>();
			dbDalMock.Setup( d => d.SetTerminationDate( "adUser2", It.IsAny<DateTime>() ) );
			dbDalMock.Setup( d => d.SetTerminationDate( "adUser4", It.IsAny<DateTime>() ) );

			adp._markTermDateForMissingEmployees( adDalMock.Object, dbDalMock.Object );

			adDalMock.VerifyAll();
			dbDalMock.VerifyAll();
		}


		[TestMethod]
		public void updatePhotos_Test()
		{			
			var adDalMock = new Mock<IAuthorizeLogOn>();
			adDalMock.Setup( a => a.RetrieveUsersAndPhotos() ).Returns( new Dictionary<string, byte[]>
			{
				{ "emp1", new byte[10] },
				{ "emp2", new byte[10] },
				{ "emp3", new byte[10] },
				{ "emp4", new byte[10] },
				{ "emp5", new byte[10] }
			} );


			var dbDalMock = new Mock<ICustomIdentityDAL>();
			dbDalMock.Setup( d => d.SaveEmployeePhoto( It.IsAny<byte[]>(), "emp1" ) );
			dbDalMock.Setup( d => d.SaveEmployeePhoto( It.IsAny<byte[]>(), "emp2" ) );
			dbDalMock.Setup( d => d.SaveEmployeePhoto( It.IsAny<byte[]>(), "emp3" ) );
			dbDalMock.Setup( d => d.SaveEmployeePhoto( It.IsAny<byte[]>(), "emp4" ) );
			dbDalMock.Setup( d => d.SaveEmployeePhoto( It.IsAny<byte[]>(), "emp5" ) );

			new ADProcessor()._uploadPhotos(adDalMock.Object, dbDalMock.Object);

			adDalMock.VerifyAll();
			dbDalMock.VerifyAll();

		}

		[TestMethod]
		public void saveManagerInformation_Test()
		{
			IList<string> employees = new List<string>
												{
													"emp1","emp2","emp3","emp4","emp5"
												};

			var adDalMock = new Mock<IAuthorizeLogOn>();
			var dbDalMock = new Mock<ICustomIdentityDAL>();
			foreach ( var employee in employees )
			{
				adDalMock.Setup( a => a.RetrieveUserInformation( employee, It.IsAny<string>() ) ).Returns( new AuthorizeLogOnDTO { UserName = employee } );
				dbDalMock.Setup( d => d.SaveManagerInfo( It.IsAny<AuthorizeLogOnDTO>() ) );
			}

			ADProcessor._saveManagerInformation(employees, adDalMock.Object, dbDalMock.Object);
			adDalMock.VerifyAll();
			dbDalMock.VerifyAll();
		}
	}
}
