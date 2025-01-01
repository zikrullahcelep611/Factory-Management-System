using FabrikaYonetimSistemi.Core.Repository;
using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace FabrikaYonetimSistemi.Service.Services.Concrete
{
    public class MaterialRequestService : IMaterialRequestService
    {
        private readonly IRepository<MaterialRequest> _repository;
        private readonly IStorageMaterialService _storageMaterialService;

        public MaterialRequestService(IRepository<MaterialRequest> repository, IStorageMaterialService storageMaterialService)
        {
            _repository = repository;
            _storageMaterialService = storageMaterialService;
        }

        public async Task AddMateriaRequestAsync(MaterialRequest materialRequest)
        {
            await _repository.AddAsync(materialRequest);
        }

        public async Task ApproveRequestAsync(int requestId)
        {
            var request = await _repository.GetByIdAsync(requestId);
            if (request == null || request.Status != RequestStatus.Pending)
                throw new InvalidOperationException("Geçersiz talep.");

            var storageMaterial = await _storageMaterialService.GetStorageMaterialByIdAsync(request.StorageMaterialId);
            if (storageMaterial == null)
                throw new InvalidOperationException("Depo malzemesi bulunamadı.");

            storageMaterial.Quantity += request.Quantity;

            await _storageMaterialService.UpdateStorageMaterialAsync(storageMaterial);

            request.Status = RequestStatus.Approved;
            request.ApprovedTime = DateTime.Now;
            _repository.Update(request);
        }


        public async Task CreateRequestAsync(MaterialRequest request)
        {
            request.Status = RequestStatus.Pending;
            request.RequestTime = DateTime.Now;
            await _repository.AddAsync(request);
        }

        public async Task<IEnumerable<MaterialRequest>> GetAllRequestsAsync()
        {
            return await _repository.GetAll()
                .Include(r => r.StorageMaterial)
                .Include(r => r.StorageMaterial.Material)
                .Include(r => r.StorageMaterial.Storage)
                .Include(r => r.Personnel)
                .ToListAsync();
        }

        public async Task<MaterialRequest> GetRequestByIdAsync(int id)
        {
             return await _repository.GetByIdAsync(id);
        }

        public async Task RejectRequestAsync(int requestId)
        {
            var request = await _repository.GetByIdAsync(requestId);
            if (request == null || request.Status != RequestStatus.Pending)
                throw new InvalidOperationException("Geçersiz talep.");

            request.Status = RequestStatus.Rejected;
            request.ApprovedTime = DateTime.Now;
            _repository.Update(request);
        }
    }
}
