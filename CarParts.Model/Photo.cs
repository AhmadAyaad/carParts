using System;
using System.Collections.Generic;
using System.Text;

namespace CarParts.Model
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public bool IsMain { get; set; } = false;
        //for the id which generated to photo from cloudinary 
        public string PublicId { get; set; }
        public Product Product { get; set; }
        public int? ProductId { get; set; }
    }
}
