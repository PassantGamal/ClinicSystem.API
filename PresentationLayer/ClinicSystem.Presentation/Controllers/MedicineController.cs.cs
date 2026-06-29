using ClinicSystem.ServicesAbstration.Contracts.Services;
using ClinicSystem.Shared.DataTransferObject.MedicineDTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSystem.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ApiBaseController
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicineDTO>>> GetAllMedicines()
        {
            var result = await _medicineService.GetAllMedicinesAsync();
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicineDTO>> GetMedicineById(Guid id)
        {
            var result = await _medicineService.GetMedicineByIdAsync(id);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<MedicineDTO>>> SearchMedicinesByName(string name)
        {
            var result = await _medicineService.SearchMedicinesByNameAsync(name);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpPost]
        public async Task<ActionResult<MedicineDTO>> CreateMedicine(AddMedicineDTO dto)
        {
            var result = await _medicineService.CreateMedicineAsync(dto);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin,Doctor")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MedicineDTO>> UpdateMedicine(Guid id, AddMedicineDTO dto)
        {
            var result = await _medicineService.UpdateMedicineAsync(id, dto);
            return HandleResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMedicine(Guid id)
        {
            var result = await _medicineService.DeleteMedicineAsync(id);
            return HandleResult(result);
        }
    }
}