using System.Collections.Generic;


namespace Magenic.BadgeApplication.DataAccess.EF
{
#if DEBUG
	public partial class SendMessageDAL
	{
		public IList<string> _GetEmailAddresses( IList<Employee> peopleToEmail, IList<Employee> employees ) 
			=> getEmailAddresses( peopleToEmail, employees );

		public void _AddEmailAddress( IList<Employee> employees, int? managerId, IList<string> emailAddresses ) 
			=> addEmailAddress( employees, managerId, emailAddresses );

		public IList<Employee> _GetEmployees( Entities context ) 
			=> getEmployees( context );

		public IList<Employee> _GetPeopleToEmail( Entities context ) 
			=> getPeopleToEmail( context );
	}
#endif
}