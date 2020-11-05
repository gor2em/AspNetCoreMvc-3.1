using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace glory.BookStore.Models
{
    public class ForgotPasswordModel
    {
        [Required,EmailAddress,Display(Name ="e mail adresiniz")]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }
}
