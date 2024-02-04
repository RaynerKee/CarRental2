using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental2.Shared.Domain
{
    public class Saving : BaseDomainModel
    {
        public int UserID { get; set; }
        public string? Month { get; set; }
        public decimal Amount { get; set; }

    }
}
