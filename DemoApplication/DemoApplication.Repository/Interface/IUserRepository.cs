﻿using DemoApplication.BusinessEntity;

namespace DemoApplication.Repository.Interface
{
    public interface IUserRepository
    {
        List<Users> GetAllUsers();
        Users GetUserByUserId(int userId);
        Users ManageUsers(Users obj);
        List<Notification> GetNotificationByUserId(int userId);
        DashboardViewModel GetDashboardCountByUserId(int userId);
        bool IsReadNotification(int notificationId);
    }
}