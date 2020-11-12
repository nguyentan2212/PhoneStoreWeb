using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStoreWeb.Data.Models
{
    public class Like
    {
        public Guid UserId { set; get; }
        public AppUser AppUser { set; get; }
        public int BlogId{ set; get; }
        public Blog Blog { get; set; }
        public int Id { set; get; }
        
    }
}
