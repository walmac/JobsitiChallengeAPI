using System;
using System.ComponentModel.DataAnnotations;

namespace JobsityChallengeAPI.DTOs
{
    public class MessageDTO
    {
        [Required]
        public string From { get; set; }
        public string To { get; set; }
        [Required]
        public string Content { get; set; }
        public string TimeStamp { get; set; }
    }
}
