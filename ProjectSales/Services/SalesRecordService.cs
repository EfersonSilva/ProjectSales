using ProjectSales.Data;
using ProjectSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectSales.Services
{
    public class SalesRecordService
    {
        private readonly ProjectSalesContext _context;
        public SalesRecordService(ProjectSalesContext context)
        {
            _context = context;
        }
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resut = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                resut = resut.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                resut = resut.Where(x => x.Date <= maxDate.Value);
            }

            return await resut
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resut = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                resut = resut.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                resut = resut.Where(x => x.Date <= maxDate.Value);
            }

            return await resut
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }
    }
}
