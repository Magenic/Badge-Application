using Autofac;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.BusinessLogic.Rules;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{/// <summary>
 /// Used for to submit badge request into the system.  
 /// </summary>
    [Serializable]
    public sealed class SubmitBadgeRequest : BusinessBase<SubmitBadgeRequest>, ISubmitBadgeRequest
    {

        #region Properties
        /// <summary>
        /// The id of the badge request this submission is for.
        /// </summary>
        public static readonly PropertyInfo<int> BadgeRequestIdProperty = RegisterProperty<int>(c => c.BadgeRequestId);
        public int BadgeRequestId
        {
            get { return GetProperty(BadgeRequestIdProperty); }
            set { SetProperty(BadgeRequestIdProperty, value); }
        }

        /// <summary>
        /// The AD user id of the person who this badge submission is for.  
        /// This should be the same as the name of the identity.
        /// </summary>
        public static readonly PropertyInfo<int> EmployeeIdProperty = RegisterProperty<int>(c => c.EmployeeId);
        public int EmployeeId
        {
            get { return GetProperty(EmployeeIdProperty); }
            set { SetProperty(EmployeeIdProperty, value); }
        }

        /// <summary>
        /// The AD user id of the person who this badge submission is for.  
        /// This should be the same as the name of the identity.
        /// </summary>
        public static readonly PropertyInfo<string> EmployeeNameProperty = RegisterProperty<string>(c => c.EmployeeName);
        public string EmployeeName
        {
            get { return GetProperty(EmployeeNameProperty); }
            set { SetProperty(EmployeeNameProperty, value); }
        }

        /// <summary>
        /// The AD user id of the person who this badge submission is for.  
        /// This should be the same as the name of the identity.
        /// </summary>
        public static readonly PropertyInfo<string> EmployeeEmailProperty = RegisterProperty<string>(c => c.EmployeeEmail);
        public string EmployeeEmail
        {
            get { return GetProperty(EmployeeEmailProperty); }
            set { SetProperty(EmployeeEmailProperty, value); }
        }

        /// <summary>
        /// Any name associated with this submission.
        /// </summary>
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        /// <summary>
        /// Any description associated with this submission.
        /// </summary>
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }

        /// <summary>
        /// Display depends on save success with this submission.
        /// </summary>
        public static readonly PropertyInfo<bool> ShowNewBadgeProperty = RegisterProperty<bool>(c => c.ShowNewBadge);
        public bool ShowNewBadge
        {
            get { return GetProperty(ShowNewBadgeProperty); }
            set { SetProperty(ShowNewBadgeProperty, value); }
        }

        #endregion Properties

        #region Factory Methods

        public async static Task<ISubmitBadgeRequest> GetBadgeRequestSubmissionByIdAsync(int badgeRequestSubmissionId)
        {
            return await IoC.Container.Resolve<IObjectFactory<ISubmitBadgeRequest>>().FetchAsync(badgeRequestSubmissionId);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SubmitBadgeRequest" /> class.
        /// </summary>
        /// <param name="employeeId">The employee id that the badge request submission is for.</param>
        /// <returns>
        /// An instance of a class the implements <see cref="ISubmitBadgeRequest" />.
        /// </returns>
        public static ISubmitBadgeRequest CreateBadgeRequestSubmission(int employeeId)
        {
            return IoC.Container.Resolve<IObjectFactory<ISubmitBadgeRequest>>().Create(employeeId);
        }

        #endregion Factory Methods

        #region Rules

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            this.BusinessRules.AddRule(new MinValue<int>(EmployeeIdProperty, 1));
            this.BusinessRules.AddRule(new Required(NameProperty));
            this.BusinessRules.AddRule(new Required(DescriptionProperty));

            this.BusinessRules.AddRule(new CanCreateSubmission(AuthorizationActions.CreateObject));
        }

        #endregion Rules

        #region Data Access

        [RunLocal]
        private void DataPortal_Create(int employeeId)
        {
            base.DataPortal_Create();
            this.LoadProperty(EmployeeIdProperty, employeeId);
            this.LoadProperty(NameProperty, String.Empty);
            this.LoadProperty(DescriptionProperty, String.Empty);
            this.BusinessRules.CheckRules();
        }

        private async Task DataPortal_Fetch(int badgeId)
        {
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_Update()
        {
            if (IsDeleted)
            {
                if (!IsNew)
                {
                    this.DataPortal_DeleteSelf();
                }
                return;
            }

            if (IsNew)
            {
                this.DataPortal_Insert();
            }
            else if (IsDirty)
            {
                var dal = IoC.Container.Resolve<ISubmitBadgeRequestDAL>();
                this.LoadData(dal.Update(this.UnloadData()));
            }
            this.MarkClean();
            this.MarkOld();
        }

        private void SaveBadges(IQueryable<BadgeAwardDTO> badgesToCreate)
        {
            AwardBadges.SaveBadges(badgesToCreate);
        }
        
        private SubmitBadgeRequestDTO UnloadData()
        {
            using (this.BypassPropertyChecks)
            {
                var returnValue = new SubmitBadgeRequestDTO
                {
                    BadgeRequestId = this.BadgeRequestId,
                    EmployeeId = this.EmployeeId,
                    Description = this.Description,
                    Name = this.Name
                };
                return returnValue;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "data")]
        private void LoadData(SubmitBadgeRequestDTO data)
        {
            using (this.BypassPropertyChecks)
            {
                this.BadgeRequestId = data.BadgeRequestId;
                this.EmployeeId = data.EmployeeId;
                this.Name = data.Name;
                this.Description = data.Description;                
            }
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_DeleteSelf()
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void DeleteChildren()
        {
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_Insert()
        {
            base.DataPortal_Insert();
            var dal = IoC.Container.Resolve<ISubmitBadgeRequestDAL>();

            this.LoadData(dal.Insert(this.UnloadData()));
            FieldManager.UpdateChildren();
        }

        #endregion Data Access
    }
}
