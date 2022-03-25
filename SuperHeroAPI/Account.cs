using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI
{
    public class Account
    {
        [Key]
        [Required]       
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        
    }
}
