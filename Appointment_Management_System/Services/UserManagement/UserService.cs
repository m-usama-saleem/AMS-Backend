using Appointment_Management_System.Models;
using Appointment_Management_System.ViewModels.UserManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.Services.UserManagement
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserService: Controller, IUserService
    {
        private readonly DatabaseContext _dbContext;
        public UserService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        #region Get 
        public List<AppUsers> GetAll()
        {
            return _dbContext.AppUsers.Where(x=>x.isDeleted == null).ToList();
        }

        #endregion

        #region Login / Logout

        [HttpPost]
        public JsonResult Login([FromBody] LoginViewModel model)
        {
            if (model is not null)
            {
                var user = _dbContext.AppUsers.Where(x => x.Name == model.UserName).SingleOrDefault();
                if (user.Password == model.Password)
                {
                    return Json(new { success = true, message = "OK" });
                }
                else
                {
                    return Json(new { success = false, message = "Password incorrect" });
                }
            }
            else
            {
                return Json(new { success = false, message = "form is null" });
            }
        }

        public String LogOut()
        {

            return "OK";
        }

        #endregion

        #region Create / Update / Delete

        [HttpPost]
        public JsonResult CreateUser([FromBody] UserViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    var userCount = _dbContext.AppUsers.Where(x => x.Name == model.Name).Count();
                    if(userCount == 0)
                    {
                        AppUsers user = new AppUsers()
                        {
                            Email = model.Email,
                            Name = model.Name,
                            Password = model.Password,
                            Status = model.Status,
                            CreatedAt = DateTime.Now,
                            CreatedBy = "", //get current user from sesssion
                            Type = model.Type
                        };

                        _dbContext.AppUsers.Add(user);
                        _dbContext.SaveChanges();

                        return Json(new { success = true, message = "User created successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "User name already exists" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "form is null" });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult EditUser([FromBody] UserViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    var user = _dbContext.AppUsers.Where(x => x.Id == model.Id && x.isDeleted == null).SingleOrDefault();
                    if (user is not null)
                    {
                        user.Email = model.Email;
                        user.Name = model.Name;
                        user.Password = model.Password;
                        user.Status = model.Status;
                        user.Type = model.Type;

                        _dbContext.Entry(user).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        return Json(new { success = true, message = "User updated successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No such user exists to update" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "form is null" });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteUser([FromBody] UserViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    var user = _dbContext.AppUsers.Where(x => x.Id == model.Id && x.isDeleted == null).SingleOrDefault();
                    if (user is not null)
                    {
                        user.isDeleted = "Y";
                        _dbContext.Entry(user).State = EntityState.Modified;

                        _dbContext.SaveChanges();

                        return Json(new { success = true, message = "User deleted successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No such user exists to delete" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "form is null" });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        #endregion

    }
}
