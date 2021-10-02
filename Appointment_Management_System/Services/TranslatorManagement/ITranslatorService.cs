using Appointment_Management_System.Models;
using Appointment_Management_System.ViewModels.UserManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.Services.TranslatorManagement
{
    public interface ITranslatorService
    {
        public List<Translators> GetAll();
        public JsonResult CreateTranslator(TranslatorViewModel model);
        public JsonResult EditTranslator(TranslatorViewModel model);
        public JsonResult DeleteTranslator(ParamsViewModel model);
    }
}
