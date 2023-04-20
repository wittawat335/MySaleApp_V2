using MySaleApp.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaleApp.Application.Services.Contract
{
    public interface ISaleService
    {
        Task<SaleDTO> Register(SaleDTO model);
        Task<List<SaleDTO>> Search(string search, string saleNumber, string startDate, string endDate);
        Task<List<ReportDTO>> Report(string startDate, string endDate);
    }
}
