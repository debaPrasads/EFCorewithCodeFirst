using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreAppMigration
{
    class DALLayerforList
    {
        CatPrdDbContext ctx;
        public DALLayerforList()
        {
            ctx = new CatPrdDbContext();
        }
        public async Task<List<Category>> CreateListAsync(List<Category> cat)
        {
            Category cobj = new Category();
            Product pobj = new Product();
            foreach (var items in cat)
            {
                cobj.CategoryId = items.CategoryId;
                cobj.BasePrice = items.BasePrice;
                cobj.CategoryName = items.CategoryName;               
                await ctx.Categories.AddAsync(cobj);
                foreach (var products in items.Products)
                {
                    if (products.Price >= items.BasePrice)
                    {
                        await ctx.Products.AddAsync(products);
                    }

                }

                await ctx.SaveChangesAsync();
            }
            return cat.ToList();
        }

        public async Task<IEnumerable<PrductViewModel>> GetListAsync(int? id)
        {
            
            var products =  await (from pd in ctx.Products
                               join od in ctx.Categories on pd.CategoryRowId equals od.CategoryRowId
                                 where od.CategoryRowId == id
                               select new PrductViewModel()
                               {
                                   ProductId= pd.ProductId,
                                  ProductName= pd.ProductName,
                                  Description=pd.Description,
                                  Price=  pd.Price,
                                  Manufacturer=pd.Manufacturer,
                                  CategoryName= od.CategoryName
                               }).ToListAsync(); 
            return products;
        }
    }
}
