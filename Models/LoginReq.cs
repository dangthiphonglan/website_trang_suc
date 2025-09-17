using System.ComponentModel.DataAnnotations;

namespace TreasureL.Models
{
    public class LoginReq
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}
