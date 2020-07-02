using System.ComponentModel.DataAnnotations;

namespace ClientAPi.Models
{
    public class ClsToken
    {
        [Required]
        public string X_Signature { set; get; }
    }
}