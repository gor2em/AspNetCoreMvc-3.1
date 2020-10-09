using glory.BookStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace glory.BookStore.Models
{
    public class GalleryModel
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        public Books Book { get; set; }
    }
}
