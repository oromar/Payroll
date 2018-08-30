using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll.Common;
using Payroll.Data;
using Payroll.Models;


namespace Payroll.Business
{
    public class OccupationsBO
    {
        private readonly ApplicationDbContext _context;

        public OccupationsBO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasMore(int page = 1, string filter = "")
        {
           return await _context.Occupation
                .Where(a => !a.Deleted)
                .Where(a => string.IsNullOrEmpty(filter) || a.Name.Contains(filter))
                .CountAsync() > (page * Constants.MAX_ITEMS_PER_PAGE);
        }

        public async Task<List<Occupation>> Search(int page = 1, string filter = "")
        {
            return await _context.Occupation
                .Where(a => !a.Deleted)
                .Where(a => string.IsNullOrEmpty(filter) || a.Name.Contains(filter))
                .OrderBy(a => a.Name)
                .Skip((page - 1) * Constants.MAX_ITEMS_PER_PAGE)
                .Take(Constants.MAX_ITEMS_PER_PAGE)
                .ToListAsync();
        }

        public async Task<Occupation> Find(Guid id)
        {
            var occupation = await _context.Occupation
                .Where(a => !a.Deleted)
                .FirstOrDefaultAsync(m => m.Id == id);

            return occupation;
        }

        public async Task<Occupation> Create(Occupation occupation, string userIdentity)
        {
            occupation.Id = Guid.NewGuid();
            occupation.CreationTime = DateTime.Now;
            occupation.CreationUser = userIdentity;
            occupation.Deleted = false;              
            _context.Add(occupation);
            await _context.SaveChangesAsync();                            
            return occupation;
        }

        public async Task<Occupation> Edit(Guid id, Occupation occupation, string userIdentity)
        {
            try
            {
                occupation.LastUpdateTime = DateTime.Now;
                occupation.LastUpdateUser = userIdentity;
                _context.Update(occupation);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }                
            return occupation;
        }

        public async Task<int> Delete(Guid id, string userIdentity)
        {
            var occupation = await _context.Occupation.FindAsync(id);
            occupation.Deleted = true;
            occupation.DeleteUser = userIdentity;
            occupation.DeleteTime = DateTime.Now;
            _context.Update(occupation);
            return await _context.SaveChangesAsync();            
        }

        public bool OccupationExists(Guid id)
        {
            return _context.Occupation.Any(e => e.Id == id);
        }
    }
}
