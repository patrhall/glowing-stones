using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ruby.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public virtual IEnumerable<MenuItem> Children { get; set; }
    }
}