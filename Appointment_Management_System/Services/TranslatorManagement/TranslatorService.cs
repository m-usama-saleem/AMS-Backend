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
            return _dbContext.Translators.Where(x => x.isDeleted == null).OrderByDescending(x => x.CreatedAt).ToList();
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
                    var userCount = _dbContext.Translators.Where(x => x.Email == model.Email && x.isDeleted == null).Count();
                    if (userCount == 0)
                    {
                        Translators user = new Translators()
                        {
                            Email = model.Email,

                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Contact = model.Contact,
                            Address = model.Address,
                            City = model.City,
                            PostCode = model.PostCode,
                            Country = model.Country,
                            Gender = model.Gender,

                            Language = model.Language,
                            CreatedAt = DateTime.Now,
                            CreatedBy = model.CreatedBy, //get current user from sesssion
                            Type = model.Type
                        };

                        _dbContext.Translators.Add(user);
                        _dbContext.SaveChanges();

                        return Json(new { user, success = true, message = "Translator created successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Translator name already exists" });
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
                        user.Type = model.Type;
                        user.Language = model.Language;
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.Contact = model.Contact;
                        user.Address = model.Address;
                        user.City = model.City;
                        user.PostCode = model.PostCode;
                        user.Country = model.Country;
                        user.Gender = model.Gender;
                        _dbContext.Entry(user).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        return Json(new { success = true, message = "Translator updated successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No such Translator exists to update" });
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

                        return Json(new { success = true, message = "Translator deleted successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No such Translator exists to delete" });
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
