using ClinicSystem.Shared.CommonResult;
using ClinicSystem.Shared.DataTransferObject.MedicineDTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.ServicesAbstration.Contracts.Services
{
    public interface IMedicineService
    {
        Task<Result<MedicineDTO>> GetMedicineByIdAsync(Guid id);
        Task<Result<IEnumerable<MedicineDTO>>> GetAllMedicinesAsync();
        Task<Result<MedicineDTO>> CreateMedicineAsync(AddMedicineDTO addMedicineDTO);
        Task<Result<MedicineDTO>> UpdateMedicineAsync(Guid id, AddMedicineDTO addMedicineDTO);
        Task<Result> DeleteMedicineAsync(Guid id);
        Task<Result<IEnumerable<MedicineDTO>>> SearchMedicinesByNameAsync(string name);
    }
}