using Appointment_Management_System.Models;
using Appointment_Management_System.ViewModels.AppointmentModule;
using Appointment_Management_System.ViewModels.Commons;
using Appointment_Management_System.ViewModels.UserManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.Services.AppointmentModule
{
    public interface IAppointmentService
    {
        public List<AppointmentDetailViewModel> GetAll();
        public List<PieChartViewModel> GetAppointmentStats();
        public JsonResult CreateAppointment(AppointmentViewModel model);
        public JsonResult EditAppointment(AppointmentViewModel model);
        public JsonResult DeleteAppointment(ParamsViewModel model);
    }
}
