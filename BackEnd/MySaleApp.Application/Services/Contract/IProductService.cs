using MySaleApp.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaleApp.Application.Services.Contract
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetList();
        Task<ProductDTO> Create(ProductDTO model);
        Task<bool> Update(ProductDTO model);
        Task<bool> Delete(int id);
    }
}
