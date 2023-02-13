using System.ComponentModel.DataAnnotations;

namespace JobsityChallengeAPI.DTOs
{
    public class UserDTO
    {

        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Name must ve at least {2}, and maximum {1} characters")]
        public string Name { get; set; }
    }
}
