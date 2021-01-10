using Microsoft.EntityFrameworkCore;
using ProjectSales.Data;
using ProjectSales.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSales.Services
{
    public class DepartmentService
    {
        private readonly ProjectSalesContext _context;
        public DepartmentService(ProjectSalesContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
