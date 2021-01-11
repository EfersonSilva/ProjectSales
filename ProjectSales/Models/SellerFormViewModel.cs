using ProjectSales.Models.Enums;
using System.Collections.Generic;

namespace ProjectSales.Models
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
