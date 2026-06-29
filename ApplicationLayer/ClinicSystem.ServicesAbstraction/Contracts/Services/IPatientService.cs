using ClinicSystem.Shared.CommonResult;
using ClinicSystem.Shared.DataTransferObject.PatientDTOS;
using ClinicSystem.Shared.DataTransferObject.AppointmentDTOS;
using ClinicSystem.Shared.DataTransferObject.PrescriptionDTOS;
using ClinicSystem.Shared.DataTransferObject.MedicalHistoryDTOS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicSystem.ServicesAbstration.Contracts.Services
{
    public interface IPatientService
    {
        Task<Result<PatientDTO>> GetPatientByIdAsync(Guid id);
        Task<Result<IEnumerable<PatientDTO>>> GetAllPatientsAsync();
        Task<Result<PatientDTO>> CreatePatientAsync(AddPatientDTO addPatientDTO);
        Task<Result<PatientDTO>> UpdatePatientAsync(Guid id, AddPatientDTO addPatientDTO);
        Task<Result> DeletePatientAsync(Guid id);
        Task<Result<MedicalHistoryDTO>> GetPatientMedicalHistoryAsync(Guid patientId);
        Task<Result<IEnumerable<AppointmentDTO>>> GetPatientAppointmentsAsync(Guid patientId);
        Task<Result<IEnumerable<PrescriptionDTO>>> GetPatientPrescriptionsAsync(Guid patientId);
    }
}