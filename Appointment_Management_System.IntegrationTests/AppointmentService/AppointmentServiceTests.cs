using Microsoft.AspNetCore.Mvc.Testing;

namespace Appointment_Management_System.IntegrationTests.AppointmentService;

public class AppointmentServiceTests : IntegrationTestFixture
{
    public AppointmentServiceTests(WebApplicationFactory<Program> factory) : base(factory)
    {
        
    }
    
    [Fact]
    public async Task Test_Endpoint_Returns_OK()
    {
        var response = await Client.GetAsync("/api/appointmentservice/GetAll");
        response.EnsureSuccessStatusCode();
    }
}