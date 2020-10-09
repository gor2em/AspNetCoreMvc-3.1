using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using glory.BookStore.Enums;
using Microsoft.AspNetCore.Http;

namespace glory.BookStore.Models
{
    public class BookModel
    {

        public int Id { get; set; }

        [StringLength(100,MinimumLength =5,ErrorMessage ="minimum 5, maksimum 100 karakter olmalı")]
        [Required(ErrorMessage ="başlık boş geçilemez!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "yazar boş geçilemez!")]
        public string Author { get; set; }

        [StringLength(500, ErrorMessage = "maksimum 500 karakter olmalı")]

        public string Description { get; set; }
        public string Category { get; set; }

        //[Required(ErrorMessage ="bir dil seçin.")]
        public int LanguageId { get; set; }

        public string Language { get; set; }

        [Required(ErrorMessage = "sayfa sayısı boş geçilemez!")]
        [Display(Name ="Total pages of book")]
        public int? TotalPages { get; set; }
        //? = nullable


        [Display(Name ="kitap resmi seçin")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }


        [Display(Name = "galeri için kitap resimlerini seçin")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; } //iformfilecollection-list<>-ienumerable<> hangisini isteresen

        public List<GalleryModel> Gallery { get; set; }


        //for pdf
        [Display(Name = "pdf formatını yükleyin")]
        [Required]
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }

    }
}
