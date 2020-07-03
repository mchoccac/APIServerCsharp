using System.ComponentModel.DataAnnotations;

namespace ClientAPi.Models
{
    public class ClsCredencial
    {
        //[Required]
        public string Id { get; set; }

        [Required]
        public string Key { get; set; }
        [Required]
        public string Shared_Secret { get; set; }


    }
}