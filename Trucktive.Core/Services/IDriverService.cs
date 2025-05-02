using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trucktive.Core.Contracts.Drivers;
using Trucktive.Core.Entities;

namespace Trucktive.Core.Services
{
    public interface IDriverService
    {
        Task<int> AddDriverAsync(CreateDriverRequest request, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<DriverResponse>> GetDriversAsync(CancellationToken cancellationToken = default);
        Task<DriverResponse> GetDriverByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<DriverResponse> UpdateDriverAsync(int id, UpdateDriverRequest request, CancellationToken cancellationToken = default);
        Task DeleteDriverAsync(int id, CancellationToken cancellationToken = default);
    }

}
