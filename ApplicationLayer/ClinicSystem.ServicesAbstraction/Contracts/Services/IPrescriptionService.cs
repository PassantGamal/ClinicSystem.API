using ClinicSystem.Shared.CommonResult;
using ClinicSystem.Shared.DataTransferObject.PrescriptionDTOS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicSystem.ServicesAbstration.Contracts.Services
{
    public interface IPrescriptionService
    {
        Task<Result<PrescriptionDTO>> GetPrescriptionByIdAsync(Guid id);
        Task<Result<IEnumerable<PrescriptionDTO>>> GetAllPrescriptionsAsync();
        Task<Result<IEnumerable<PrescriptionDTO>>> GetDoctorPrescriptionsAsync(Guid doctorId);
        Task<Result<IEnumerable<PrescriptionDTO>>> GetPatientPrescriptionsAsync(Guid patientId);
        Task<Result<PrescriptionDTO>> CreatePrescriptionAsync(CreatePrescriptionDTO createPrescriptionDTO);
        Task<Result<PrescriptionDTO>> UpdatePrescriptionAsync(Guid id, CreatePrescriptionDTO createPrescriptionDTO);
        Task<Result> DeletePrescriptionAsync(Guid id);
    }
}