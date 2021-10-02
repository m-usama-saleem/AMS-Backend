using Appointment_Management_System.Models;
using Appointment_Management_System.ViewModels.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.Services.TranslatorManagement
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TranslatorService : Controller, ITranslatorService
    {
        private readonly DatabaseContext _dbContext;
        public TranslatorService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        #region Get 
        public List<Translators> GetAll()
        {
            return _dbContext.Translators.Where(x => x.isDeleted == null).ToList();
        }

        #endregion


        #region Create / Update / Delete

        [HttpPost]
        public JsonResult CreateTranslator([FromBody] TranslatorViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    var userCount = _dbContext.Translators.Where(x => x.Name == model.Name).Count();
                    if (userCount == 0)
                    {
                        Translators user = new Translators()
                        {
                            Email = model.Email,
                            Name = model.Name,
                            Language = model.Language,
                            CreatedAt = DateTime.Now,
                            CreatedBy = "", //get current user from sesssion
                            Type = model.Type
                        };

                        _dbContext.Translators.Add(user);
                        _dbContext.SaveChanges();

                        return Json(new { user, success = true, message = "User created successfully" });
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
        public JsonResult EditTranslator([FromBody] TranslatorViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    var user = _dbContext.Translators.Where(x => x.Id == model.Id && x.isDeleted == null).SingleOrDefault();
                    if (user is not null)
                    {
                        user.Email = model.Email;
                        user.Name = model.Name;
                        user.Type = model.Type;
                        user.Language = model.Language;

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
        public JsonResult DeleteTranslator([FromBody] ParamsViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    var user = _dbContext.Translators.Where(x => x.Id == model.id && x.isDeleted == null).SingleOrDefault();
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
