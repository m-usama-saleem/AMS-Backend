using Appointment_Management_System.Models;
using Appointment_Management_System.ViewModels.Commons;
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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FinanceService : Controller, IFinanceService
    {
        private readonly DatabaseContext _dbContext;

        public FinanceService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public List<FinanceViewModel> GetAll()
        {
            List<FinanceViewModel> model = new List<FinanceViewModel>();

            var finance = _dbContext.Finance.Where(x => x.isDeleted == null).ToList();
            finance.ForEach(x =>
            {
                var appointment = _dbContext.AppointmentInfo.FirstOrDefault((y => y.Id == x.AppointmentId));
                var institute = _dbContext.Institutions.FirstOrDefault((y => y.Id == appointment.InstitutionId));
                var traName = _dbContext.Translators.FirstOrDefault((y => y.Id == appointment.TranslatorId));

                model.Add(new FinanceViewModel
                {
                    Id = x.Id,
                    AppointmentId_Fk = x.AppointmentId,
                    AppointmentId = appointment.AppointmentId,
                    AppointmentDate = appointment.AppointmentDate.ToShortDateString(),
                    AppointmentInstitute = institute.Name,
                    InstituteAddress = institute.Address + " \n " + institute.Postcode + " " + institute.City,
                    AppointmentTranslator = traName.FirstName + " " + traName.LastName,
                    AppointmentLanguage = appointment.Language,
                    AppointmentType = appointment.Type,
                    Type = x.Type,
                    Tax = x.Tax,
                    WordCount = x.WordCount,
                    Rate = x.Rate,
                    FlatRate = x.FlatRate,
                    Paragraph = x.Paragraph,
                    Postage = x.Postage,
                    Hours = x.Hours,
                    AppointmentStart = x.AppointmentStart,
                    StartOfTheTrip = x.StartOfTheTrip,
                    EndOfTheTrip = x.EndOfTheTrip,
                    EndOfTheAppointment = x.EndOfTheAppointment,
                    TotalHours = x.TotalHours,
                    Discount = x.Discount,
                    NetPayment = x.NetPayment,
                    Status = x.Status,
                    RideCost = x.RideCost,
                    DailyAllowance = x.DailyAllowance,
                    TicketCost = x.TicketCost,
                    isDeleted = x.isDeleted,
                    CreatedBy = x.CreatedBy,
                    Attachments = x.Attachments,
                    ApprovalBy = x.ApprovalBy,
                    ApprovalDate = x.ApprovalDate,
                    CompletionDate = x.CompletionDate,
                    CompletionBy = x.CompletionBy,
                    CreatedAt = x.CreatedAt
                });
            });
            return model;

        }

        public List<FinanceViewModel> GetAllPayables()
        {
            List<FinanceViewModel> model = new List<FinanceViewModel>();

            var finance = _dbContext.Finance.Where(x => x.isDeleted == null && x.Type == "P" && x.Status != "Completed")
                            .OrderByDescending(x => x.CreatedAt).ToList();

            finance.ForEach(x =>
            {
                var appointment = _dbContext.AppointmentInfo.FirstOrDefault((y => y.Id == x.AppointmentId));
                var institute = _dbContext.Institutions.FirstOrDefault((y => y.Id == appointment.InstitutionId));
                var traName = _dbContext.Translators.FirstOrDefault((y => y.Id == appointment.TranslatorId));

                model.Add(new FinanceViewModel
                {
                    Id = x.Id,
                    AppointmentId_Fk = x.AppointmentId,
                    AppointmentId = appointment.AppointmentId,
                    AppointmentDate = appointment.AppointmentDate.ToShortDateString(),
                    AppointmentInstitute = institute.Name,
                    InstituteAddress = institute.Address + " \n " + institute.Postcode + " " + institute.City,
                    AppointmentTranslator = traName.FirstName + " " + traName.LastName,
                    AppointmentLanguage = appointment.Language,
                    AppointmentType = appointment.Type,
                    Type = x.Type,
                    Tax = x.Tax,
                    WordCount = x.WordCount,
                    Rate = x.Rate,
                    FlatRate = x.FlatRate,
                    Paragraph = x.Paragraph,
                    Postage = x.Postage,
                    Hours = x.Hours,
                    Discount = x.Discount,
                    NetPayment = x.NetPayment,
                    Status = x.Status,
                    RideCost = x.RideCost,
                    AppointmentStart = x.AppointmentStart,
                    StartOfTheTrip = x.StartOfTheTrip,
                    EndOfTheTrip = x.EndOfTheTrip,
                    EndOfTheAppointment = x.EndOfTheAppointment,
                    TotalHours = x.TotalHours,
                    DailyAllowance = x.DailyAllowance,
                    TicketCost = x.TicketCost,
                    isDeleted = x.isDeleted,
                    CreatedBy = x.CreatedBy,
                    Attachments = x.Attachments,
                    ApprovalBy = x.ApprovalBy,
                    ApprovalDate = x.ApprovalDate,
                    CompletionDate = x.CompletionDate,
                    CompletionBy = x.CompletionBy,
                    CreatedAt = x.CreatedAt
                });
            });
            return model;
        }

        public List<FinanceViewModel> GetAllReceivables()
        {
            List<FinanceViewModel> model = new List<FinanceViewModel>();

            var finance = _dbContext.Finance.Where(x => x.isDeleted == null && x.Type == "R" && x.Status != "Completed")
                            .OrderByDescending(x => x.CreatedAt).ToList();
            finance.ForEach(x =>
            {
                var appointment = _dbContext.AppointmentInfo.FirstOrDefault((y => y.Id == x.AppointmentId));
                var institute = _dbContext.Institutions.FirstOrDefault((y => y.Id == appointment.InstitutionId));
                var traName = _dbContext.Translators.FirstOrDefault((y => y.Id == appointment.TranslatorId));

                model.Add(new FinanceViewModel
                {
                    Id = x.Id,
                    AppointmentId_Fk = x.AppointmentId,
                    AppointmentId = appointment.AppointmentId,
                    AppointmentDate = appointment.AppointmentDate.ToShortDateString(),
                    AppointmentInstitute = institute.Name,
                    InstituteAddress = institute.Address + " \n " + institute.Postcode + " " + institute.City,
                    AppointmentTranslator = traName.FirstName + " " + traName.LastName,
                    AppointmentLanguage = appointment.Language,
                    AppointmentType = appointment.Type,
                    Type = x.Type,
                    Tax = x.Tax,
                    WordCount = x.WordCount,
                    Rate = x.Rate,
                    FlatRate = x.FlatRate,
                    Paragraph = x.Paragraph,
                    Postage = x.Postage,
                    Hours = x.Hours,
                    AppointmentStart = x.AppointmentStart,
                    StartOfTheTrip = x.StartOfTheTrip,
                    EndOfTheTrip = x.EndOfTheTrip,
                    EndOfTheAppointment = x.EndOfTheAppointment,
                    TotalHours = x.TotalHours,
                    Discount = x.Discount,
                    NetPayment = x.NetPayment,
                    Status = x.Status,
                    RideCost = x.RideCost,
                    DailyAllowance = x.DailyAllowance,
                    TicketCost = x.TicketCost,
                    isDeleted = x.isDeleted,
                    CreatedBy = x.CreatedBy,
                    Attachments = x.Attachments,
                    ApprovalBy = x.ApprovalBy,
                    ApprovalDate = x.ApprovalDate,
                    CompletionDate = x.CompletionDate,
                    CompletionBy = x.CompletionBy,
                    CreatedAt = x.CreatedAt
                });
            });
            return model;
        }

        [HttpPost]
        public String Create([FromBody] FinanceViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    Finance finance = new Finance()
                    {
                        AppointmentId = model.AppointmentId_Fk,
                        Status = model.Status,
                        Type = model.Type,
                        Attachments = model.Attachments,
                        WordCount = model.WordCount,
                        FlatRate = model.FlatRate,
                        Paragraph = model.Paragraph,
                        Postage = model.Postage,
                        Rate = model.Rate,
                        Hours = model.Hours,
                        AppointmentStart = model.AppointmentStart,
                        StartOfTheTrip = model.StartOfTheTrip,
                        EndOfTheTrip = model.EndOfTheTrip,
                        EndOfTheAppointment = model.EndOfTheAppointment,
                        TotalHours = model.TotalHours,
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
            catch (Exception e)
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
                        fin.ApprovalDate = DateTime.Now;
                        fin.ApprovalBy = model.userId;

                        _dbContext.Entry(fin).State = EntityState.Modified;
                        var result = _dbContext.SaveChanges();
                        if (result > 0)
                        {
                            var app = _dbContext.Finance.Where(x => x.AppointmentId == fin.AppointmentId &&
                                               x.Status == "Approved" &&
                                               x.isDeleted == null).ToList();

                            if (app.Count() == 2) //both legs approved then mark status completed
                            {
                                foreach (var item in app)
                                {
                                    item.Status = "Completed";
                                    item.CompletionDate = DateTime.Now;
                                    item.CompletionBy = model.userId;
                                    _dbContext.Entry(item).State = EntityState.Modified;
                                }

                                var appointment = _dbContext.AppointmentInfo.SingleOrDefault(x => x.Id == fin.AppointmentId && x.isDeleted == null);
                                appointment.Status = "Completed";
                                appointment.CompletionDate = DateTime.Now;
                                appointment.CompletionBy = model.userId;

                                _dbContext.Entry(appointment).State = EntityState.Modified;
                                _dbContext.SaveChanges();
                            }
                            else
                            {
                                var appointment = _dbContext.AppointmentInfo.SingleOrDefault(x => x.Id == fin.AppointmentId && x.isDeleted == null);
                                appointment.Status = "PartiallyCompleted";

                                _dbContext.Entry(appointment).State = EntityState.Modified;
                                _dbContext.SaveChanges();
                            }
                            return Json(new { success = true, message = "Approved successfully" });
                        }

                        return Json(new { success = false, message = "Not Approved" });
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
        public JsonResult MultipleApprove([FromBody] List<ParamsViewModel> list)
        {
            try
            {
                if (list.Count > 0)
                {
                    var aprovedList = new List<long>();
                    foreach (var model in list)
                    {
                        var fin = _dbContext.Finance.SingleOrDefault(x => x.Id == model.id && x.isDeleted == null);
                        if (fin is not null)
                        {
                            fin.Status = "Approved";
                            fin.ApprovalDate = DateTime.Now;
                            fin.ApprovalBy = model.userId;

                            _dbContext.Entry(fin).State = EntityState.Modified;
                            var result = _dbContext.SaveChanges();
                            if (result > 0)
                            {
                                aprovedList.Add(model.id);
                                var app = _dbContext.Finance.Where(x => x.AppointmentId == fin.AppointmentId &&
                                                   x.Status == "Approved" &&
                                                   x.isDeleted == null).ToList();

                                if (app.Count() == 2) //both legs approved then mark status completed
                                {
                                    foreach (var item in app)
                                    {
                                        item.Status = "Completed";
                                        item.CompletionDate = DateTime.Now;
                                        item.CompletionBy = model.userId;

                                        _dbContext.Entry(item).State = EntityState.Modified;
                                    }

                                    var appointment = _dbContext.AppointmentInfo.SingleOrDefault(x => x.Id == fin.AppointmentId && x.isDeleted == null);
                                    appointment.Status = "Completed";
                                    appointment.CompletionDate = DateTime.Now;
                                    appointment.CompletionBy = model.userId;

                                    _dbContext.Entry(appointment).State = EntityState.Modified;
                                    _dbContext.SaveChanges();
                                }
                            }
                        }
                    }
                    if (aprovedList.Count == list.Count)
                    {
                        return Json(new { list, success = true, message = "Approved successfully" });
                    }
                    else if (aprovedList.Count < list.Count && aprovedList.Count != 0)
                    {
                        return Json(new { list, success = true, message = "Some Payable Approved successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Not Approved" });
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
                if (model is not null)
                {
                    var finance = _dbContext.Finance.Where(x => x.Id == model.Id && x.isDeleted == null).SingleOrDefault();
                    if (finance is not null)
                    {
                        finance.AppointmentId = model.AppointmentId_Fk;
                        finance.Status = model.Status;
                        finance.Type = model.Type;
                        finance.Attachments = model.Attachments;
                        finance.WordCount = model.WordCount;
                        finance.Rate = model.Rate;
                        finance.FlatRate = model.FlatRate;
                        finance.Paragraph = model.Paragraph;
                        finance.Postage = model.Postage;
                        finance.Hours = model.Hours;
                        finance.AppointmentStart = model.AppointmentStart;
                        finance.StartOfTheTrip = model.StartOfTheTrip;
                        finance.EndOfTheTrip = model.EndOfTheTrip;
                        finance.EndOfTheAppointment = model.EndOfTheAppointment;
                        finance.TotalHours = model.TotalHours;
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
                            return Json(new { Finance = model, success = true, message = "Payable updated successfully" });
                        }
                        else if (model.Type == "R")
                        {
                            return Json(new { Finance = model, success = true, message = "Receivable updated successfully" });
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
            catch (Exception e)
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

        public List<BarChartViewModel> GetCompletedPayableStats()
        {
            List<BarChartViewModel> model = new List<BarChartViewModel>();

            //var finance = _dbContext.Finance.Where(x => x.isDeleted == null && x.Type == "P").ToList();
            var finance = (from fin in _dbContext.Finance
                           join app in _dbContext.AppointmentInfo on fin.AppointmentId equals app.Id
                           where fin.isDeleted == null && fin.Status == "Completed" && fin.Type == "P"
                           select new
                           {
                               Appointment = app,
                               Payable = fin,
                           }).ToList().GroupBy(x => x.Payable.CompletionDate.Month).ToList();

            foreach (var pay in finance)
            {
                var completed_writing = pay.Where(x => x.Appointment.Type == "SCHREIBEN").ToList();
                var completed_speaking = pay.Where(x => x.Appointment.Type == "SPRACHEN").ToList();

                model.Add(new BarChartViewModel
                {
                    name = "Complete Payables",
                    type = new DateTime(2020, pay.Key, 1).ToString("MMMM"),
                    value1 = completed_writing.Count(),
                    value2 = completed_speaking.Count()
                });
            }

            return model;
        }
        public List<BarChartViewModel> GetInCompletedPayableStats()
        {
            List<BarChartViewModel> model = new List<BarChartViewModel>();

            //var finance = _dbContext.Finance.Where(x => x.isDeleted == null && x.Type == "P").ToList();
            var finance = (from fin in _dbContext.Finance
                           join app in _dbContext.AppointmentInfo on fin.AppointmentId equals app.Id
                           where fin.isDeleted == null && fin.Status != "Completed" && fin.Type == "P"
                           select new
                           {
                               Appointment = app,
                               Payable = fin,
                           }).ToList().GroupBy(x => x.Payable.CreatedAt.Month).ToList();

            foreach (var pay in finance)
            {
                var completed_writing = pay.Where(x => x.Appointment.Type == "SCHREIBEN").ToList();
                var completed_speaking = pay.Where(x => x.Appointment.Type == "SPRACHEN").ToList();

                model.Add(new BarChartViewModel
                {
                    name = "Incomplete Payables",
                    type = new DateTime(2020, pay.Key, 1).ToString("MMMM"),
                    value1 = completed_writing.Count(),
                    value2 = completed_speaking.Count()
                });
            }

            return model;
        }
        public List<BarChartViewModel> GetCompletedReceivableStats()
        {
            List<BarChartViewModel> model = new List<BarChartViewModel>();

            //var finance = _dbContext.Finance.Where(x => x.isDeleted == null && x.Type == "P").ToList();
            var finance = (from fin in _dbContext.Finance
                           join app in _dbContext.AppointmentInfo on fin.AppointmentId equals app.Id
                           where fin.isDeleted == null && fin.Status != "Completed" && fin.Type == "R"
                           select new
                           {
                               Appointment = app,
                               Payable = fin,
                           }).ToList().GroupBy(x => x.Payable.CompletionDate.Month).ToList();

            foreach (var pay in finance)
            {
                var completed_writing = pay.Where(x => x.Appointment.Type == "SCHREIBEN").ToList();
                var completed_speaking = pay.Where(x => x.Appointment.Type == "SPRACHEN").ToList();

                model.Add(new BarChartViewModel
                {
                    name = "Complete Receivable",
                    type = new DateTime(2020, pay.Key, 1).ToString("MMMM"),
                    value1 = completed_writing.Count(),
                    value2 = completed_speaking.Count()
                });
            }

            return model;
        }
         public List<BarChartViewModel> GetInCompletedReceivableStats()
        {
            List<BarChartViewModel> model = new List<BarChartViewModel>();

            //var finance = _dbContext.Finance.Where(x => x.isDeleted == null && x.Type == "P").ToList();
            var finance = (from fin in _dbContext.Finance
                           join app in _dbContext.AppointmentInfo on fin.AppointmentId equals app.Id
                           where fin.isDeleted == null && fin.Status != "Completed" && fin.Type == "R"
                           select new
                           {
                               Appointment = app,
                               Payable = fin,
                           }).ToList().GroupBy(x => x.Payable.CreatedAt.Month).ToList();

            foreach (var pay in finance)
            {
                var completed_writing = pay.Where(x => x.Appointment.Type == "SCHREIBEN").ToList();
                var completed_speaking = pay.Where(x => x.Appointment.Type == "SPRACHEN").ToList();

                model.Add(new BarChartViewModel
                {
                    name = "Incomplete Receivables",
                    type = new DateTime(2020, pay.Key, 1).ToString("MMMM"),
                    value1 = completed_writing.Count(),
                    value2 = completed_speaking.Count()
                });
            }

            return model;
        }

    }
}
