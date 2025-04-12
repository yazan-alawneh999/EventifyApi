using LearningHub.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Services
{
   public interface INotificationService
   {
        public List<Notification> getAllNotifications();
        public Notification getNotificationByID(int ID);
        public List<Notification> getNotificationByUserID(int IDuser);

        public void CreateNotification(Notification notification);
        public void UpdateNotification(Notification notification);
        public void deleteNotification(int ID);
    }
}
