using Appointment_Management_System.Data;
using Appointment_Management_System.Models;
using Appointment_Management_System.Services.AppointmentModule;
using Appointment_Management_System.Services.FinanceModule;
using Appointment_Management_System.ViewModels.AppointmentModule;
using Appointment_Management_System.ViewModels.UserManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Appointment_Management_System.UnitTests.Services;

public class AppointmentServiceTests
{
    private readonly IAppointmentService _appointmentService;
    private readonly Mock<IFinanceService> _mockFinanceService;

    private Mock<IDatabaseContext> _mockDbContext;
    private Mock<DbSet<AppointmentInfo>> _mockAppointmentInfoDbSet;
    private Mock<DbSet<Finance>> _mockFinanceDbSet;
    
    public AppointmentServiceTests()
    {
        var serviceCollection = new ServiceCollection();

        SetupMockDbContext();
        SetupMockDbSets();
        SetupMockEntityEntries();
        SetupServiceCollection(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        _appointmentService = serviceProvider.GetService<IAppointmentService>();
    }

    [Fact]
    public void GetAppointments_ShouldReturnAppointments()
    {
        // Act
        var result = _appointmentService.GetAll();

        // Assert
        var appResponseModel = Assert.IsType<List<AppointmentDetailViewModel>>(result);
        Assert.NotNull(appResponseModel);
        Assert.Equal(2, appResponseModel.Count);
    }

    [Fact]
    public void GetAppointments_ShouldReturnEmpty_WhenNoAppointmentsFound()
    {
        // Arrange
        // Mock DbContext to return no appointments
        var appointments = new List<AppointmentInfo>().AsQueryable();

        _mockAppointmentInfoDbSet.As<IQueryable<AppointmentInfo>>()
                                  .Setup(m => m.Provider).Returns(appointments.Provider);
        _mockAppointmentInfoDbSet.As<IQueryable<AppointmentInfo>>()
                                  .Setup(m => m.Expression).Returns(appointments.Expression);
        _mockAppointmentInfoDbSet.As<IQueryable<AppointmentInfo>>()
                                  .Setup(m => m.ElementType).Returns(appointments.ElementType);
        _mockAppointmentInfoDbSet.As<IQueryable<AppointmentInfo>>()
                                  .Setup(m => m.GetEnumerator()).Returns(appointments.GetEnumerator());

        _mockDbContext.Setup(m => m.AppointmentInfo).Returns(_mockAppointmentInfoDbSet.Object);

        // Act
        var result = _appointmentService.GetAll();

        // Assert
        var appResponseModel = Assert.IsType<List<AppointmentDetailViewModel>>(result);
        Assert.Empty(appResponseModel);
    }
    
    [Fact]
    public void CreateAppointment_ShouldReturnSuccess_WhenAppointmentIsValid()
    {
        // Arrange
        var model = new AppointmentViewModel
        {
            AppointmentId = "3",
            TranslatorId = 1,
            InstitutionId = 1,
            Type = "Translation",
            AppointmentDate = DateTime.Now,
            Rate = 100,
            Hours = 2,
            Tax = 19,
            Discount = 10,
            CreatedBy = 1,
            Language = "English",
            RoomNumber = "101",
            Remarks = "Urgent",
            InvoiceID = "INV123"
        };

        // Act
        var result = _appointmentService.CreateAppointment(model);

        // Assert
        var jsonResponse = Assert.IsType<JsonResult>(result);
        Assert.Contains("True", jsonResponse.Value.GetType().GetProperty("success").GetValue(jsonResponse.Value, null).ToString());
        Assert.Contains("Appointment created successfully", jsonResponse.Value.ToString());
    }
    
    [Fact]
    public void UpdateAppointment_ShouldReturnSuccess_WhenAppointmentExists()
    {
        // Arrange
        var model = new AppointmentViewModel
        {
            Id = 1, // Existing Appointment
            AppointmentId = "1",
            TranslatorId = 1,
            InstitutionId = 1,
            Type = "Translation",
            AppointmentDate = DateTime.Now.AddDays(1),
            Rate = 100,
            Hours = 2,
            Tax = 19,
            Discount = 10,
            CreatedBy = 1,
            Language = "English",
            RoomNumber = "101",
            Remarks = "Updated Remarks",
            InvoiceID = "INV123"
        };

        // Act
        var result = _appointmentService.EditAppointment(model);

        // Assert
        var jsonResponse = Assert.IsType<JsonResult>(result);
        var appointmentUpdated = jsonResponse.Value.GetType().GetProperty("appointment").GetValue(jsonResponse.Value, null) as AppointmentInfo;
        
        Assert.Equal(model.Remarks, appointmentUpdated.Remarks);
        Assert.Contains("Appointment updated successfully", jsonResponse.Value.ToString());
        _mockDbContext.Verify(m => m.SaveChanges(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void UpdateAppointment_ShouldReturnFailure_WhenAppointmentDoesNotExist()
    {
        // Arrange
        var model = new AppointmentViewModel
        {
            AppointmentId = "999", // Non-existing AppointmentId
            TranslatorId = 1,
            InstitutionId = 1,
            Type = "Translation",
            AppointmentDate = DateTime.Now.AddDays(1),
            Rate = 100,
            Hours = 2,
            Tax = 19,
            Discount = 10,
            CreatedBy = 1,
            Language = "English",
            RoomNumber = "101",
            Remarks = "Updated Remarks",
            InvoiceID = "INV123"
        };

        // Act
        var result = _appointmentService.EditAppointment(model);

        // Assert
        var jsonResponse = Assert.IsType<JsonResult>(result);
        Assert.Contains("False", jsonResponse.Value.GetType().GetProperty("success").GetValue(jsonResponse.Value, null).ToString());
        Assert.Contains("No such appointment exists to update", jsonResponse.Value.ToString());
    }
    
    [Fact]
    public void DeleteAppointment_ShouldReturnSuccess_WhenAppointmentExists()
    {
        // Arrange
        var appointmentId = 1;
        var deleteAppointmentPayload = new ParamsViewModel()
        {
            id = appointmentId
        };

        // Act
        var result = _appointmentService.DeleteAppointment(deleteAppointmentPayload);

        // Assert
        var jsonResponse = Assert.IsType<JsonResult>(result);
        Assert.Contains("True", jsonResponse.Value.GetType().GetProperty("success").GetValue(jsonResponse.Value, null).ToString());
        Assert.Contains("Appointment deleted successfully", jsonResponse.Value.ToString());

        // Verify that SaveChanges was called
        _mockDbContext.Verify(m => m.SaveChanges(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void DeleteAppointment_ShouldReturnFailure_WhenAppointmentDoesNotExist()
    {
        // Arrange
        var appointmentId = 999; // Non-existing appointment
        var deleteAppointmentPayload = new ParamsViewModel()
        {
            id = appointmentId
        };
        
        // Act
        var result = _appointmentService.DeleteAppointment(deleteAppointmentPayload);

        // Assert
        var jsonResponse = Assert.IsType<JsonResult>(result);
        Assert.Contains("False", jsonResponse.Value.GetType().GetProperty("success").GetValue(jsonResponse.Value, null).ToString());
        Assert.Contains("No such appointment exists to delete", jsonResponse.Value.ToString());
    }
    
    private void SetupMockDbContext()
    {
        _mockDbContext = new Mock<IDatabaseContext>();
    }

    private void SetupMockDbSets()
    {
        var appointments = new List<AppointmentInfo>
        {
            new AppointmentInfo { AppointmentId = "1", Id = 1, TranslatorId = 1, InstitutionId = 1, Remarks = "Initial Remarks 1" },
            new AppointmentInfo { AppointmentId = "2", Id = 2, TranslatorId = 2, InstitutionId = 2, Remarks = "Initial Remarks 2" }
        }.AsQueryable();

        var finances = new List<Finance>
        {
            new Finance { Id = 1, AppointmentId = 1, NetPayment = 100, isDeleted = null },
            new Finance { Id = 2, AppointmentId = 2, NetPayment = 200, isDeleted = null }
        }.AsQueryable();

        _mockAppointmentInfoDbSet = SetupMockDbSet(appointments);
        _mockFinanceDbSet = SetupMockDbSet(finances);

        _mockDbContext.Setup(m => m.AppointmentInfo).Returns(_mockAppointmentInfoDbSet.Object);
        _mockDbContext.Setup(m => m.Finance).Returns(_mockFinanceDbSet.Object);
    }

    private Mock<DbSet<T>> SetupMockDbSet<T>(IQueryable<T> data) where T : class
    {
        var mockSet = new Mock<DbSet<T>>();
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        return mockSet;
    }

    private void SetupMockEntityEntries()
    {
        var mockAppointmentEntry = new Mock<EntityEntry<AppointmentInfo>>(MockBehavior.Default, It.IsAny<AppointmentInfo>());
        var mockFinanceEntry = new Mock<EntityEntry<Finance>>(MockBehavior.Default, It.IsAny<Finance>());

        _mockDbContext.Setup(db => db.Entry(It.IsAny<AppointmentInfo>())).Returns(mockAppointmentEntry.Object);
        _mockDbContext.Setup(db => db.Entry(It.IsAny<Finance>())).Returns(mockFinanceEntry.Object);

        _mockDbContext.Setup(m => m.SaveChanges(It.IsAny<string>())).Returns(1);
    }

    private void SetupServiceCollection(ServiceCollection services)
    {
        services.AddScoped<IDatabaseContext>(_ => _mockDbContext.Object);
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IFinanceService, FinanceService>();
    }
}