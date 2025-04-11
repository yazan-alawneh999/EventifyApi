using LearningHub.Core.Response;
using LearningHub.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace LearningHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
       
        [HttpGet]
        [Route("getAllNotifications")]
        public List<Notification> getAllNotifications() 
        {
            return _notificationService.getAllNotifications(); 
        }
        [HttpGet]
        [Route("getNotificationByID/{ID}")]
        public Notification getNotificationByID(int ID)
        {
            return _notificationService.getNotificationByID(ID);    
        }
        [HttpGet]
        [Route("getNotificationByUserID/{IDuser}")]
        public List<Notification> getNotificationByUserID(int IDuser)
        {
            return _notificationService.getNotificationByUserID(IDuser);
        }

        [HttpPost]
        [Route("CreateNotification")]
        public void CreateNotification(Notification notification) 
        {
            _notificationService.CreateNotification(notification);
        }
        [HttpPut]
        [Route("UpdateNotification")]
        public void UpdateNotification(Notification notification) 
        {
            _notificationService.UpdateNotification(notification);
        }

        [HttpDelete]
        [Route("deleteNotification/{id}")]
        public void deleteNotification(int id)
        {
            _notificationService.deleteNotification(id);
        }
    }
}
