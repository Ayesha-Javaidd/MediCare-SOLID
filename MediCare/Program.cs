
using MediCare.Interfaces;
using MediCare.Models;
using MediCare.Repositories;
using MediCare.Services;
using MediCare.UI;

namespace MediCare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IUserRepository userRepository = new UserRepository();
            IProviderRepository providerRepository = new ProviderRepository();
            IMedicationRepository medicationRepository = new MedicationRepository();
            IAppointmentRepository appointmentRepository = new AppointmentRepository();

            IUserService userService = new UserService(userRepository);
            IMedicationService medicationService = new MedicationService(medicationRepository);
            IAppointmentService appointmentService = new AppointmentService(appointmentRepository);
            IAuthService authService = new AuthService(userRepository);

            AdminUI adminUI = new AdminUI(userService, medicationService);
            UserUI userUI = new UserUI(appointmentService, providerRepository, medicationService);
            LoginUI loginUI = new LoginUI(authService, adminUI, userUI);

            loginUI.Start();
        }
    }
}
