using ProjectSales.Data;
using ProjectSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectSales.Services.Exceptions;

namespace ProjectSales.Services
{
    public class SalesRecordService
    {
        private readonly ProjectSalesContext _context;
        public SalesRecordService(ProjectSalesContext context)
        {
            _context = context;
        }


        public async Task InsertAsync(SalesRecord obj)
        {
            _context.Add(obj);
           await _context.SaveChangesAsync();
        }

        public async Task<SalesRecord> FindByIdAsync(int id)
        {
            return await _context.SalesRecord.Include(obj => obj.Seller).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public List<Seller> FindAllSellers()
        {
            return  _context.Seller.ToList();
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

        public async Task UpdateAsync(SalesRecord obj)
        {
            bool hasAny = await _context.SalesRecord.AnyAsync(x => x.Id == obj.Id);

            if (!hasAny)
                throw new NotFoundException("Id Not Found");

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.SalesRecord.FindAsync(id);
                _context.SalesRecord.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
        }
    }
}
