using System;
using System.Threading.Tasks;
using Csla;
using Csla.Core;
using Csla.Serialization.Mobile;

namespace Magenic.BadgeApplication.BusinessLogic.Framework
{
    [Serializable]
    public sealed class ObjectFactory<T> : Common.Interfaces.IObjectFactory<T> where T : class, IMobileObject
    {
        public void BeginCreate(object criteria, object userState)
        {
            DataPortal.BeginCreate(criteria, CreateCompleted, userState);
        }

        public void BeginCreate(object criteria)
        {
            DataPortal.BeginCreate(criteria, CreateCompleted);
        }

        public void BeginCreate()
        {
            DataPortal.BeginCreate(CreateCompleted);
        }

        public void BeginDelete(object criteria, object userState)
        {
            DataPortal.BeginDelete(criteria, DeleteCompleted, userState);
        }

        public void BeginDelete(object criteria)
        {
            DataPortal.BeginDelete(criteria, DeleteCompleted);
        }

        public void BeginExecute(T command, object userState)
        {
            DataPortal.BeginExecute(command, ExecuteCompleted, userState);
        }

        public void BeginExecute(T command)
        {
            DataPortal.BeginExecute(command, ExecuteCompleted);
        }

        public void BeginFetch(object criteria, object userState)
        {
            DataPortal.BeginFetch(criteria, FetchCompleted, userState);
        }

        public void BeginFetch(object criteria)
        {
            DataPortal.BeginFetch(criteria, FetchCompleted);
        }

        public void BeginFetch()
        {
            DataPortal.BeginFetch(FetchCompleted);
        }

        public void BeginUpdate(T obj, object userState)
        {
            DataPortal.BeginUpdate(obj, UpdateCompleted, userState);
        }

        public void BeginUpdate(T obj)
        {
            DataPortal.BeginUpdate(obj, UpdateCompleted);
        }

        public T Create()
        {
            return DataPortal.Create<T>();
        }

        public TC CreateChild<TC>()
        {
            return DataPortal.CreateChild<TC>();
        }

        public TC CreateChild<TC>(params object[] parameters)
        {
            return DataPortal.CreateChild<TC>(parameters);
        }

        public T Create(object criteria)
        {
            return DataPortal.Create<T>(criteria);
        }

        public async Task<T> CreateAsync(object criteria)
        {
            return await DataPortal.CreateAsync<T>(criteria);
        }

        public async Task<T> CreateAsync()
        {
            return await DataPortal.CreateAsync<T>();
        }

        public event EventHandler<DataPortalResult<T>> CreateCompleted;

        public void Delete(object criteria)
        {
            DataPortal.Delete<T>(criteria);
        }

        public Task DeleteAsync(object criteria)
        {
            return DataPortal.DeleteAsync<T>(criteria);
        }

        public event EventHandler<DataPortalResult<T>> DeleteCompleted;

        public T Execute(T obj)
        {
            return DataPortal.Execute<T>(obj);
        }

        public async Task<T> ExecuteAsync(T command)
        {
            return await DataPortal.ExecuteAsync<T>(command);
        }

        public event EventHandler<DataPortalResult<T>> ExecuteCompleted;

        public T Fetch()
        {
            return DataPortal.Fetch<T>();
        }

        public T Fetch(object criteria)
        {
            return DataPortal.Fetch<T>(criteria);
        }

        public async Task<T> FetchAsync(object criteria)
        {
            return await DataPortal.FetchAsync<T>(criteria);
        }

        public async Task<T> FetchAsync()
        {
            return await DataPortal.FetchAsync<T>();
        }

        public TC FetchChild<TC>()
        {
            return DataPortal.FetchChild<TC>();
        }

        public TC FetchChild<TC>(params object[] parameters)
        {
            return DataPortal.FetchChild<TC>(parameters);
        }

        public event EventHandler<DataPortalResult<T>> FetchCompleted;

        public ContextDictionary GlobalContext
        {
            get { return ApplicationContext.GlobalContext; }
        }

        public T Update(T obj)
        {
            return DataPortal.Update<T>(obj);
        }

        public async Task<T> UpdateAsync(T obj)
        {
            return await DataPortal.UpdateAsync<T>(obj);
        }

        public event EventHandler<DataPortalResult<T>> UpdateCompleted;

        public void UpdateChild(object child)
        {
            DataPortal.UpdateChild(child);
        }

        public void UpdateChild(object child, params object[] parameters)
        {
            DataPortal.UpdateChild(child, parameters);
        }
    }
}