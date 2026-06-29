using ClinicSystem.Domain.Entities;
using ClinicSystem.ServicesAbstration.Contracts.Repositories;
using ClinicSystem.ServicesAbstration.Contracts.Services;

using ClinicSystem.Shared.CommonResult;
using ClinicSystem.Shared.DataTransferObject.MedicineDTOS;

namespace ClinicSystem.Services.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MedicineDTO>> GetMedicineByIdAsync(Guid id)
        {
            var medicine = await _unitOfWork.Repository<Medicine>().GetByIdAsync(id);
            if (medicine == null)
                return Result<MedicineDTO>.Fail(Error.NotFound());

            return Result<MedicineDTO>.Ok(MapToMedicineDTO(medicine));
        }

        public async Task<Result<IEnumerable<MedicineDTO>>> GetAllMedicinesAsync()
        {
            var medicines = await _unitOfWork.Repository<Medicine>().GetAllAsync();
            var medicineDTOs = medicines.Select(MapToMedicineDTO);
            return Result<IEnumerable<MedicineDTO>>.Ok(medicineDTOs);
        }

        public async Task<Result<IEnumerable<MedicineDTO>>> SearchMedicinesByNameAsync(string name)
        {
            var medicines = await _unitOfWork.Repository<Medicine>().FindAsync(m => m.Name.Contains(name));
            var medicineDTOs = medicines.Select(MapToMedicineDTO);
            return Result<IEnumerable<MedicineDTO>>.Ok(medicineDTOs);
        }

        public async Task<Result<MedicineDTO>> CreateMedicineAsync(AddMedicineDTO addMedicineDTO)
        {
            var medicine = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = addMedicineDTO.Name,
                Description = addMedicineDTO.Description,
                Category = addMedicineDTO.Category,
                Manufacturer = addMedicineDTO.Manufacturer,
                DosageForm = addMedicineDTO.DosageForm,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Repository<Medicine>().AddAsync(medicine);
            await _unitOfWork.SaveChangesAsync();

            return Result<MedicineDTO>.Ok(MapToMedicineDTO(medicine));
        }

        public async Task<Result<MedicineDTO>> UpdateMedicineAsync(Guid id, AddMedicineDTO addMedicineDTO)
        {
            var medicine = await _unitOfWork.Repository<Medicine>().GetByIdAsync(id);
            if (medicine == null)
                return Result<MedicineDTO>.Fail(Error.NotFound());

            medicine.Name = addMedicineDTO.Name;
            medicine.Description = addMedicineDTO.Description;
            medicine.Category = addMedicineDTO.Category;
            medicine.Manufacturer = addMedicineDTO.Manufacturer;
            medicine.DosageForm = addMedicineDTO.DosageForm;
            medicine.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<Medicine>().Update(medicine);
            await _unitOfWork.SaveChangesAsync();

            return Result<MedicineDTO>.Ok(MapToMedicineDTO(medicine));
        }

        public async Task<Result> DeleteMedicineAsync(Guid id)
        {
            var medicine = await _unitOfWork.Repository<Medicine>().GetByIdAsync(id);
            if (medicine == null)
                return Result.Fail(Error.NotFound());

            _unitOfWork.Repository<Medicine>().Delete(medicine);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok();
        }

        private MedicineDTO MapToMedicineDTO(Medicine medicine)
        {
            return new MedicineDTO
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Description = medicine.Description,
                Category = medicine.Category,
                Manufacturer = medicine.Manufacturer,
                DosageForm = medicine.DosageForm,
                CreatedAt = medicine.CreatedAt
            };
        }
    }
}