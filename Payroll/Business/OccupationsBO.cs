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
    public class OccupationsBO: GenericBO<Occupation>
    {
        public OccupationsBO(ApplicationDbContext context): base(context)
        {
        }

        public override async Task<int> Count(int page = 1, string filter = "")
        {
           return await _context.Occupation
                .Where(a => !a.Deleted)
                .Where(a => string.IsNullOrEmpty(filter) 
                || (a.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase) || a.CouncilName.Contains(filter, StringComparison.InvariantCultureIgnoreCase)))
                .CountAsync();
        }

        public override async Task<List<Occupation>> Search(int page = 1, string filter = "")
        {
            return await _context.Occupation
                .Where(a => !a.Deleted)
                .Where(a => string.IsNullOrEmpty(filter) || 
                (a.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase) || 
                (a.CouncilName != null && a.CouncilName.Contains(filter, StringComparison.InvariantCultureIgnoreCase))))
                .OrderBy(a => a.Name)
                .Skip((page - 1) * Constants.MAX_ITEMS_PER_PAGE)
                .Take(Constants.MAX_ITEMS_PER_PAGE)
                .ToListAsync();
        }

        public override async Task<Occupation> Details(Guid id)
        {
            return await _context.Occupation
                .Where(a => !a.Deleted)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public override async Task<Occupation> Find(Guid? id)
        {
            var occupation = await _context.Occupation
                .Where(a => !a.Deleted)
                .FirstOrDefaultAsync(m => m.Id == id);

            return occupation;
        }

        public override bool Exists(Guid id)
        {
            return _context.Occupation.Any(e => e.Id == id);
        }
    }
}
