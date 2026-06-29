using ClinicSystem.Shared.DataTransferObject.PrescriptionDTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.ServicesAbstration.Contracts.Services
{
    public interface IPrescriptionPdfService
    {
        Task<byte[]> GeneratePrescriptionPdfAsync(PrescriptionDTO prescription);
    }
}
