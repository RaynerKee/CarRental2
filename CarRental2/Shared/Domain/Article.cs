using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental2.Shared.Domain
{
    public class Article : BaseDomainModel
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int AuthorID { get; set; }

    }
}
