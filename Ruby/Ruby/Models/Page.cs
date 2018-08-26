using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ruby.Models
{
    public class Page
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PageContent { get; set; }
        public int? ParentId { get; set; }
        public virtual Page Parent { get;set; }
        public virtual List<Page> Children { get; set; }
    }
}