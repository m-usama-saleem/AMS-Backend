using Appointment_Management_System.Models;
using Appointment_Management_System.ViewModels.FinanceModule;
using Appointment_Management_System.ViewModels.UserManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.Services.FinanceModule
{
    public interface IFinanceService
    {
        public List<FinanceViewModel> GetAll();
        public List<FinanceViewModel> GetAllPayables();
        public List<FinanceViewModel> GetAllReceivables();
        public String Create(FinanceViewModel model);
        public JsonResult Edit(FinanceViewModel model);
        public JsonResult Delete(ParamsViewModel model);
    }
}
