using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace glory.BookStore.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage ="isminizi girin.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "soyisminizi girin.")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="email zorunludur.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "güçlü bir şifre giriniz.")]
        [Compare("ConfirmPassword",ErrorMessage ="şire boş geçilemez")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "şifreyi onaylayın.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
