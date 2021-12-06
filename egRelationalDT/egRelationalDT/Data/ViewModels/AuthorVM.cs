using System.Collections.Generic;

namespace egRelationalDT.Data.ViewModels
{
    public class AuthorVM
    {
        public string FullName { get; set; }
    }

    public class AuthorWithBooksVM
    {
        public string FullName { get; set; }

        public List<string> BookTitles { get; set; }
    }
}
