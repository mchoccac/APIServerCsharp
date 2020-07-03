using System.ComponentModel.DataAnnotations;

namespace ClientAPi.Models 
{
    public class ClsMessage
    {
        [Required]
        public string Message { set; get; }
        [Required]
        public string Id { get; set; }
    }
}