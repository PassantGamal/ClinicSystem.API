using ClinicSystem.Services.Services;
using ClinicSystem.ServicesAbstration.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicSystem.Services.Dependencies
{
    public static class ServicesDependencies
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPrescriptionService, PrescriptionService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IMedicineService, MedicineService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPrescriptionPdfService, PrescriptionPdfService>();
            return services;
        }
    }
}