using Appointment_Management_System.Models;
using Appointment_Management_System.ViewModels.FinanceModule;
using Appointment_Management_System.ViewModels.UserManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.Services.FinanceModule
{
    public class FinanceService: Controller, IFinanceService
    {
        private readonly DatabaseContext _dbContext;

        public FinanceService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public List<FinanceViewModel> GetAll()
        {
            List <FinanceViewModel> model = new List<FinanceViewModel>();

            var finance = _dbContext.Finance.Where(x=>x.isDeleted == null).ToList();
            finance.ForEach(x =>
            {
                var appId = _dbContext.AppointmentInfo.FirstOrDefault((y => y.Id == x.AppointmentId)).AppointmentId;

                model.Add(new FinanceViewModel
                {
                    Id = x.Id,
                    AppointmentId_Fk = x.AppointmentId,
                    AppointmentId = appId,
                    Type = x.Type,
                    Tax = x.Tax,
                    WordCount = x.WordCount,
                    Rate = x.Rate,
                    Hours = x.Hours,
                    Discount = x.Discount,
                    NetPayment = x.NetPayment,
                    Status = x.Status,
                    RideCost = x.RideCost,
                    DailyAllowance = x.DailyAllowance,
                    TicketCost = x.TicketCost,
                    isDeleted = x.isDeleted,
                    CreatedBy = x.CreatedBy,
                    Attachments = x.Attachments
                });
            });
            return model;

        }

        public List<Finance> GetAllPayables()
        {
            return _dbContext.Finance.Where(x => x.isDeleted == null && x.Type == "P" && x.Status != "Completed").ToList();
        }

        public List<Finance> GetAllReceivables()
        {
            return _dbContext.Finance.Where(x => x.isDeleted == null && x.Type == "R" && x.Status != "Completed").ToList();
        }

        [HttpPost]
        public String Create([FromBody]FinanceViewModel model)
        {
            try
            {
                if(model is not null)
                {
                    Finance finance = new Finance()
                    {
                        AppointmentId = model.AppointmentId_Fk,
                        Status = model.Status,
                        Type = model.Type,
                        Attachments = model.Attachments,
                        WordCount = model.WordCount,
                        Rate = model.Rate,
                        Hours = model.Hours,
                        Discount = model.Discount,
                        RideCost = model.RideCost,
                        DailyAllowance = model.DailyAllowance,
                        Tax = model.Tax,
                        TicketCost = model.TicketCost,
                        NetPayment = model.NetPayment,
                        CreatedAt = DateTime.Now,
                        CreatedBy = model.CreatedBy
                    };

                    _dbContext.Finance.Add(finance);
                    _dbContext.SaveChanges();

                    return "OK";

                    //if(model.Type === "P")
                    //{
                    //    return Json(new { Finance = finance, success = true, message = "Payable created successfully" });
                    //}
                    //else if(model.Type === "R")
                    //{
                    //    return Json(new { Finance = finance, success = true, message = "Receivable created successfully" });
                    //}
                }
                else
                {
                    return "Error null";
                    //return Json(new { success = false, message = "form is null" });
                }
            }
            catch(Exception e)
            {
                return e.Message;
                //return Json(new { success = false, message = e.Message });
            }
        }
        [HttpPost]
        public JsonResult Approve([FromBody] ParamsViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    var fin = _dbContext.Finance.SingleOrDefault(x => x.Id == model.id && x.isDeleted == null);
                    if (fin is not null)
                    {
                        fin.Status = "Approved";

                        var app = _dbContext.Finance.Where(x => x.AppointmentId == fin.AppointmentId &&
                                            x.Status == "Approved" &&
                                            x.isDeleted == null).ToList();

                        if(app.Count() == 2) //both legs approved then mark status completed
                        {
                            foreach(var item in app)
                            {
                                item.Status = "Completed";
                                _dbContext.Entry(item).State = EntityState.Modified;
                            }

                            var appointment = _dbContext.AppointmentInfo.SingleOrDefault(x => x.Id == fin.AppointmentId && x.isDeleted == null);
                            appointment.Status = "Completed";

                            _dbContext.Entry(appointment).State = EntityState.Modified;
                        }

                        _dbContext.Entry(fin).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        return Json(new { success = true, message = "Approved successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No such appointment exists to approve" });
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
        public JsonResult Edit(FinanceViewModel model)
        {
            try
            {
                if(model is not null)
                {
                    var finance = _dbContext.Finance.Where(x => x.Id == model.Id && x.isDeleted == null).SingleOrDefault();
                    if(finance is not null)
                    {
                        finance.AppointmentId = model.AppointmentId_Fk;
                        finance.Status = model.Status;
                        finance.Type = model.Type;
                        finance.Attachments = model.Attachments;
                        finance.WordCount = model.WordCount;
                        finance.Rate = model.Rate;
                        finance.Hours = model.Hours;
                        finance.Discount = model.Discount;
                        finance.RideCost = model.RideCost;
                        finance.DailyAllowance = model.DailyAllowance;
                        finance.Tax = model.Tax;
                        finance.TicketCost = model.TicketCost;
                        finance.NetPayment = model.NetPayment;
                        finance.CreatedAt = DateTime.Now;
                        finance.CreatedBy = model.CreatedBy;

                        _dbContext.Entry(finance).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        if (model.Type == "P")
                        {
                            return Json(new { Finance = finance, success = true, message = "Payable updated successfully" });
                        }
                        else if (model.Type == "R")
                        {
                            return Json(new { Finance = finance, success = true, message = "Receivable updated successfully" });
                        }
                        else
                        {
                            return Json(new { success = false, message = "Finance type error" });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, message = "No such Payable/Receivable exists to update" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "form is null" });
                }
            }
            catch(Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult Delete([FromBody] ParamsViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    var fin = _dbContext.Finance.Where(x => x.Id == model.id && x.isDeleted == null).SingleOrDefault();
                    if (fin is not null)
                    {
                        fin.isDeleted = "Y";

                        _dbContext.Entry(fin).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        return Json(new { success = true, message = "Payable/Receivable deleted successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "No such Payable/Receivable exists to delete" });
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
    }
}
