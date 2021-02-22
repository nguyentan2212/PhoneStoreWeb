using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PhoneStoreWeb.Communication.Categories
{
    public class CreateCategoryRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile ThumbnailImage { get; set; }
    }
}
