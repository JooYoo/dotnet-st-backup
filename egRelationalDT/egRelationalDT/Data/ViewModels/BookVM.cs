using System;
using System.Collections.Generic;

namespace egRelationalDT.Data.ViewModels
{
    public class BookVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string CoverUrl { get; set; }

        // relations with Book_Author join table
        public int PublisherId { get; set; }
        public List<int> AuthorIds { get; set; }
    }

    public class BookWithAuthorVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string CoverUrl { get; set; }

        // relations with Book_Author join table
        public string PublishName { get; set; }
        public List<string> AuthorNames { get; set; }
    }
}
