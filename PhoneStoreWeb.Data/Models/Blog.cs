using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneStoreWeb.Data.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string Title{ get; set; }
        public int LikeCount{ get; set; }
        public string ImagePath { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime Created_At { set; get; }

        public Guid UserId { set; get; }
        public int CategoryId { set; get; }
        public AppUser AppUser { set; get; }
        public Category Categories { set; get; }
    }
}
