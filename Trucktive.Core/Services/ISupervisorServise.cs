using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trucktive.Core.Contracts.Supervisor;
using Trucktive.Core.Entities;

namespace Trucktive.Core.Services
{
    public interface ISupervisorService
    {
       
        Task<int> AddSupervisorAsync(CreateSupervisorRequest request, CancellationToken cancellationToken = default);
        Task DeleteSupervisorAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<SupervisorResponse>> GetSupervisorsAsync(CancellationToken cancellationToken = default); // يجب أن تكون هنا
        Task<SupervisorResponse> GetSupervisorByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<SupervisorResponse> UpdateSupervisorAsync(int id, UpdateSupervisorRequest request, CancellationToken cancellationToken = default);
    }

}


