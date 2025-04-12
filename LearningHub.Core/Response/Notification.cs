using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Response
{
      public class Notification
      {
        public decimal NOTIFICATIONID { get; set; }
        public decimal userId { get; set; }
        public string message { get; set; } 
        public  DateTime createdAt{ get; set; }
      
    }
}
