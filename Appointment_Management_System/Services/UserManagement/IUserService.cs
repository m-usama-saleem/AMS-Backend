using Appointment_Management_System.Models;
using Appointment_Management_System.ViewModels.UserManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.Services.UserManagement
{
    public interface IUserService
    {
        public JsonResult Login(LoginViewModel model);
        public String LogOut();

        public List<AppUsers> GetAll();
        public JsonResult CreateUser(UserViewModel model);
        public JsonResult EditUser(UserViewModel model);
        public JsonResult DeleteUser(UserViewModel model);

    }
}
