using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace glory.BookStore.Data
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int LanguageId { get; set; } //ilişki

        public string CoverImageUrl { get; set; }
        public string BookPdfUrl { get; set; }
        public int TotalPages { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdateOn { get; set; }

        public Language Language { get; set; } //ilişki

        public ICollection<BookGallery> BookGallery { get; set; } //ilişki




    }
}
