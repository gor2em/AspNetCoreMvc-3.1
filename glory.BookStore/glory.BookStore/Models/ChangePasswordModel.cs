using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace glory.BookStore.Models
{
    public class ChangePasswordModel
    {
        [Required,DataType(DataType.Password),Display(Name ="mevcut şifre")]
        public string CurrentPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "yeni şifre")]
        public string NewPassword { get; set; }
            
        [Required, DataType(DataType.Password), Display(Name = "yeni şifre")]
        [Compare("NewPassword", ErrorMessage ="yeni şifre tekrarı girilmeli.")] //15 to 18
        public string ConfirmNewPassword { get; set; }

    }
}
