using MySaleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaleApp.Application.Repositories
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        Task<Sale> Register(Sale model);
    }
}
