﻿using Dapper;
using DemoApplication.BusinessEntity;
using DemoApplication.Repository.Interface;
using Microsoft.Extensions.Options;

namespace DemoApplication.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IOptions<ConnectionSettings> Configs) : base(Configs)
        {

        }

        #region Get User

        /// <summary>
        /// Get All User
        /// </summary>
        /// <returns></returns>
        public List<Users> GetAllUsers()
        {
           return Query<Users>("usp_getalluser", new DynamicParameters());
        }

        /// <summary>
        /// Get User By UserId
        /// </summary>
        /// <returns></returns>
        public Users GetUserByUserId(int userId)
        {
            var dynamicParameters = new DynamicParameters();
            var args = new Dictionary<string, object>()
            {
                ["userId"] = userId
            };
            dynamicParameters.AddDynamicParams(args);
            return QueryFirstOrDefault<Users>("usp_getuserbyuserid", dynamicParameters);
        }

        #endregion

        #region Manage User Add/Update/Delete

        public Users ManageUsers(Users obj)
        {
            var dynamicParameters = new DynamicParameters();
            var args = new Dictionary<string, object>()
            {
                ["id"] = obj.UserId,
                ["firstname"] = obj.FirstName,
                ["lastname"] = obj.LastName,
                ["email"] = obj.Email,
                ["address"] = obj.Address,
                ["encryptedpassword"] = obj.Encryptedpassword
            };
            dynamicParameters.AddDynamicParams(args);
            return QueryFirstOrDefault<Users>("usp_manageusers", dynamicParameters);
        }

        #endregion

        #region Get Notification By UserId

        public List<Notification> GetNotificationByUserId(int userId)
        {
            var dynamicParameters = new DynamicParameters();
            var args = new Dictionary<string, object>()
            {
                ["userId"] = userId
            };
            dynamicParameters.AddDynamicParams(args);
            return Query<Notification>("usp_getnotificationbyuserid", dynamicParameters);
        }

        #endregion

        #region ToDoCount

        public DashboardViewModel GetDashboardCountByUserId(int userId)
        {
            var dynamicParameters = new DynamicParameters();
            var args = new Dictionary<string, object>()
            {
                ["userId"] = userId
            };
            dynamicParameters.AddDynamicParams(args);
            return QueryFirstOrDefault<DashboardViewModel>("usp_getdashboardcountbyuserid", dynamicParameters);
        }

        #endregion

        #region IsReadNotification
        public bool IsReadNotification(int notificationId)
        {
            try
            {
                var dynamicParameters = new DynamicParameters();
                var args = new Dictionary<string, object>()
                {
                    ["notificationId"] = notificationId
                };
                dynamicParameters.AddDynamicParams(args);
                Execute("usp_readnotification", dynamicParameters);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
