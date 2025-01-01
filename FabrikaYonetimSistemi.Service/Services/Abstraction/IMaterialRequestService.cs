using FabrikaYonetimSistemi.Entity.Entities;

namespace FabrikaYonetimSistemi.Service.Services.Abstraction
{
    public interface IMaterialRequestService
    {
        Task<IEnumerable<MaterialRequest>> GetAllRequestsAsync();
        Task<MaterialRequest> GetRequestByIdAsync(int id);
        Task CreateRequestAsync(MaterialRequest request);
        Task ApproveRequestAsync(int requestId);
        Task RejectRequestAsync(int requestId);
    }
}
