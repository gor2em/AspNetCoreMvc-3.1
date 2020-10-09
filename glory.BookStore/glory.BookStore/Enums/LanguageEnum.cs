using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace glory.BookStore.Enums
{
    public enum LanguageEnum
    {
        [Display(Name ="hindistanca")]
        Hindi,

        [Display(Name = "ingilizce")]
        English,

        [Display(Name = "hollandaca")]
        Dutch,

        [Display(Name = "türkçe")]
        Turkish,

        [Display(Name = "çince")]
        Chinese,

        [Display(Name = "almanca")]
        German
    }
}
