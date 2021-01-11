using ProjectSales.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSales.Models
{
    public class SalesRecordViewModel
    {
        public SalesRecord SalesRecord { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();
        public ICollection<SaleStatus> SaleStatus { get; set; } = new List<SaleStatus>();

    }
}
