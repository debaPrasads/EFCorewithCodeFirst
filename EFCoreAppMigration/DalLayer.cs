using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreAppMigration
{
    public class DalLayer
    {
        CatPrdDbContext ctx;
        public DalLayer()
        {
            ctx = new CatPrdDbContext();
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await ctx.Categories.ToListAsync();
        }

        public async Task<Category> GetAsync(int id)
        {
            var cat = await ctx.Categories.FindAsync(id);
            return cat;
        }
        public async Task<Category> CreateAsync(Category cat)
        {
            var res = await ctx.Categories.AddAsync(cat);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Category> UpdateAsync(int id, Category cat)
        {
            var c = await ctx.Categories.FindAsync(id);
            if (c != null)
            {
                // perform upate

                // 1. Update each property separately
                //c.CategoryId = cat.CategoryId;
                //c.CategoryName = cat.CategoryName;
                //c.BasePrice = cat.BasePrice;

                // 2. Update using Cursor State
                ctx.Entry<Category>(cat).State = EntityState.Modified;

                await ctx.SaveChangesAsync();

            }
            return cat;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var c = await ctx.Categories.FindAsync(id);
            if (c != null)
            {
                ctx.Categories.Remove(c);
                await ctx.SaveChangesAsync();
                return true;
            }
            return false;
            
        }

    }
}
