using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneStoreWeb.Data.Models
{
    public class Comment
    {
        public Guid UserId { get; set; }
        public AppUser AppUser { set; get; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int Id { get; set; }
        [Required]
        public DateTime Created_At { set; get; }
        [Required]
        public string Content{ set; get; }   
    }
}
