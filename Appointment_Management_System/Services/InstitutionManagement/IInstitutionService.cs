using Appointment_Management_System.Models;
using Appointment_Management_System.ViewModels.UserManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.Services.InstitutionManagement
{
    public interface IInstitutionService
    {
        public List<Institutions> GetAll();
        public JsonResult CreateInstitution(InstitutionViewModel model);
        public JsonResult EditInstitution(InstitutionViewModel model);
        public JsonResult DeleteInstitution(ParamsViewModel model);
    }
}
