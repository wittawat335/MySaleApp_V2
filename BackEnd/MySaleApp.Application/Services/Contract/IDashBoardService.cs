using MySaleApp.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaleApp.Application.Services.Contract
{
    public interface IDashBoardService
    {
        Task<DashBoardDTO> Summary();
    }
}
