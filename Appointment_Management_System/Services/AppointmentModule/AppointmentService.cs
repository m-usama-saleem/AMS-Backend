using Appointment_Management_System.Models;
using Appointment_Management_System.ViewModels.AppointmentModule;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.Services.AppointmentModule
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentService: Controller, IAppointmentService
    {
        private readonly DatabaseContext _dbContext;

        public AppointmentService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        #region Get

        public List<AppointmentInfo> GetAll()
        {
            return _dbContext.AppointmentInfo.Where(x => x.isDeleted == null).ToList();
        }

        #endregion

        #region Create / Update / Delete

        [HttpPost]
        public JsonResult CreateAppointment(AppointmentViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    var appCount = _dbContext.AppointmentInfo.Where(x => x.AppointmentId == model.AppointmentId && x.isDeleted == null).Count();
                    if(appCount == 0)
                    {
                        AppointmentInfo appInfo = new AppointmentInfo()
                        {
                            AppointmentId = model.AppointmentId,
                            TranslatorName = model.TranslatorName,
                            InstitutionName = model.InstitutionName,
                            Type = model.Type,
                            EntryDate = model.EntryDate,
                            AppointmentDate = model.AppointmentDate,
                            Tax = model.Tax,
                            Rate = model.Rate,
                            Hours = model.Hours,
                            Discount = model.Discount,
                            NetPayment = ((model.Rate * model.Hours) + model.Tax - model.Discount)
                        };

                        _dbContext.AppointmentInfo.Add(appInfo);
                        _dbContext.SaveChanges();

                        return Json(new { success = true, message = "Appointment created successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Appointment already exists" });
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
        public JsonResult EditAppointment(AppointmentViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    var app = _dbContext.AppointmentInfo.Where(x => x.Id == model.Id && x.isDeleted == null).SingleOrDefault();
                    if (app is not null)
                    {
                        app.AppointmentId = model.AppointmentId;
                        app.TranslatorName = model.TranslatorName;
                        app.InstitutionName = model.InstitutionName;
                        app.Type = model.Type;
                        app.EntryDate = model.EntryDate;
                        app.AppointmentDate = model.AppointmentDate;
                        app.Tax = model.Tax;
                        app.Rate = model.Rate;
                        app.Hours = model.Hours;
                        app.Discount = model.Discount;
                        app.NetPayment = ((model.Rate * model.Hours) + model.Tax - model.Discount);

                        _dbContext.Entry(app).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        return Json(new { success = true, message = "Appointment updated successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No such appointment exists to update" });
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
        public JsonResult DeleteAppointment(AppointmentViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    var app = _dbContext.AppointmentInfo.Where(x => x.AppointmentId == model.AppointmentId && x.isDeleted == null).SingleOrDefault();
                    if (app is not null)
                    {
                        app.isDeleted = "Y";

                        _dbContext.Entry(app).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        return Json(new { success = true, message = "Appointment deleted successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No such appointment exists to delete" });
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
