﻿using System.ComponentModel.DataAnnotations;

namespace ClientAPi.Models 
{
    public class ClsMessageTags : ClsCredencial
    {
        [Required]
        public string Message { set; get; }

        public string Tag { set; get; }
    }
}