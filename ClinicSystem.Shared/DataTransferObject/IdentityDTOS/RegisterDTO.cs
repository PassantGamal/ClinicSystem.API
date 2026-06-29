using System.ComponentModel.DataAnnotations;

namespace ClinicSystem.Shared.DataTransferObject.IdentityDTOS
{
    public record RegisterDTO(
        [EmailAddress] string Email,
        string DisplayName,
        string UserName,
        string Password,
        [Phone] string PhoneNumber,
        string Role
    );
}