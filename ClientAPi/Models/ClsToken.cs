using System.ComponentModel.DataAnnotations;

namespace ClientAPi.Models
{
    public class ClsToken : ClsMessageTags
    {
        [Required]
        public string X_Signature { set; get; }
        [Required]
        public string X_Key { set; get; }
    }
}