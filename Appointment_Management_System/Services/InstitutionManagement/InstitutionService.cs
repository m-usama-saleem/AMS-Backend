using Appointment_Management_System.Models;
using Appointment_Management_System.ViewModels.UserManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.Services.InstitutionManagement
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InstitutionService : Controller, IInstitutionService
    {
        private readonly DatabaseContext _dbContext;
        public InstitutionService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        #region Get 
        public List<Institutions> GetAll()
        {
            return _dbContext.Institutions.Where(x => x.isDeleted == null).OrderByDescending(x => x.CreatedAt).ToList();
        }

        #endregion

        #region Create / Update / Delete

        [HttpPost]
        public JsonResult CreateInstitution([FromBody] InstitutionViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    var InstituteCount = _dbContext.Institutions.Where(x => x.Email == model.Email && x.isDeleted == null).Count();
                    if (InstituteCount == 0)
                    {
                        Institutions user = new Institutions()
                        {
                            Email = model.Email,
                            Name = model.Name,
                            Address = model.Address,
                            City = model.City,
                            Postcode = model.Postcode,
                            CreatedAt = DateTime.Now,
                            CreatedBy = model.CreatedBy, //get current user from sesssion
                        };

                        _dbContext.Institutions.Add(user);
                        _dbContext.SaveChanges();

                        return Json(new { user, success = true, message = "Institute created successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Institute name already exists" });
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
        public JsonResult EditInstitution([FromBody] InstitutionViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    var user = _dbContext.Institutions.Where(x => x.Id == model.Id && x.isDeleted == null).SingleOrDefault();
                    if (user is not null)
                    {
                        user.Email = model.Email;
                        user.Name = model.Name;
                        user.Address = model.Address;
                        user.Postcode = model.Postcode;
                        user.City = model.City;

                        _dbContext.Entry(user).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        return Json(new { success = true, message = "Institute updated successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No such Institute exists to update" });
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
        public JsonResult DeleteInstitution([FromBody] ParamsViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    var user = _dbContext.Institutions.Where(x => x.Id == model.id && x.isDeleted == null).SingleOrDefault();
                    if (user is not null)
                    {
                        user.isDeleted = "Y";
                        _dbContext.Entry(user).State = EntityState.Modified;

                        _dbContext.SaveChanges();

                        return Json(new { success = true, message = "Institute deleted successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No such Institute exists to delete" });
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
