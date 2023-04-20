using Microsoft.EntityFrameworkCore;
using MySaleApp.Application.Repositories;
using MySaleApp.Domain.Entities;
using MySaleApp.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaleApp.Infrastructure.Repositories
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        private readonly SaledbV2Context _dbcontext;

        public SaleRepository(SaledbV2Context dbcontext) : base(dbcontext) 
        {
            _dbcontext = dbcontext;
        }

        public async Task<Sale> Register(Sale model)
        {
            Sale sale = new Sale();
            using (var transaction = _dbcontext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in model.SaleDetails)
                    {
                        var product = _dbcontext.Products.Where(x => x.ProductId == item.ProductId).First();

                        product.Stock = product.Stock - item.Quantity;
                        _dbcontext.Products.Update(product);
                    }
                    await _dbcontext.SaveChangesAsync();

                    var docNumber = _dbcontext.DocumentNumbers.First();

                    docNumber.LastNumber = docNumber.LastNumber + 1;
                    docNumber.CreateDate = DateTime.Now;

                    _dbcontext.DocumentNumbers.Update(docNumber);
                    await _dbcontext.SaveChangesAsync();

                    int digi = 4;
                    string test = string.Concat(Enumerable.Repeat("0", digi));
                    string test2 = test + docNumber.LastNumber.ToString();

                    //00001
                    test2 = test2.Substring(test2.Length - digi, digi);
                    model.DocumentNumber = test2;
                    await _dbcontext.Sales.AddAsync(model);
                    await _dbcontext.SaveChangesAsync();

                    sale = model;
                    transaction.Commit();

                }
                catch
                {
                    transaction.Rollback();
                    throw;

                }

                return sale;
            }
        }
    }
}
