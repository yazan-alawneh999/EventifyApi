


using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace LearningHub.Core.Dto;

public class ProfileDto
{
         public string USERID { get; set; }
    [Required]
        public string FirstName { get; set; }   
        [Required]
        public string LastName { get; set; }  
        [Required]
        public string City { get; set; }  
        [Required]
        public int Age { get; set; }  
        [Required]
        public string Email { get; set; } 
        
     
        [Required]
        public string PhoneNumber { get; set; }

        
        [JsonIgnore]
        public IFormFile ImageFile { get; set; }
    
}