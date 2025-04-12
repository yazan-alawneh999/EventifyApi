using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningHub.Core.Response
{
    public class Location
    {
        public int LocationID { get; set; } 
        public double Latitude { get; set; }  
        public double Longitude { get; set; } 
        public int EventID { get; set; } 
        public string ?Address { get; set; } 

       
    }
}
