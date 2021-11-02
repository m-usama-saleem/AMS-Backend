using Appointment_Management_System.Models;
using Appointment_Management_System.Services.FinanceModule;
using Appointment_Management_System.ViewModels.AppointmentModule;
using Appointment_Management_System.ViewModels.Commons;
using Appointment_Management_System.ViewModels.FinanceModule;
using Appointment_Management_System.ViewModels.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Management_System.Services.AppointmentModule
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentService : Controller, IAppointmentService
    {
        private readonly DatabaseContext _dbContext;

        public AppointmentService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        #region Get

        public List<AppointmentViewModel> GetAllIncomplete()
        {
            List<AppointmentViewModel> model = new List<AppointmentViewModel>();
            var appointments = _dbContext.AppointmentInfo.Where(x => x.isDeleted == null && x.Status != "Completed").ToList();
            appointments.ForEach(x =>
            {
                var institute = _dbContext.Institutions.FirstOrDefault((y => y.Id == x.InstitutionId));
                var traName = _dbContext.Translators.FirstOrDefault((y => y.Id == x.TranslatorId));

                model.Add(new AppointmentViewModel
                {
                    Id = x.Id,
                    AppointmentId = x.AppointmentId,
                    TranslatorId = x.TranslatorId,
                    TranslatorName = traName.FirstName + " " + traName.LastName,
                    InstitutionId = x.InstitutionId,
                    InstitutionName = institute.Name,
                    InstitutionAddress = institute.Address + " \n " + institute.Postcode + " " + institute.City,
                    Type = x.Type,
                    EntryDate = x.EntryDate,
                    AppointmentDate = x.AppointmentDate,
                    Tax = x.Tax,
                    Rate = x.Rate,
                    Hours = x.Hours,
                    Discount = x.Discount,
                    NetPayment = x.NetPayment,
                    Status = x.Status,
                    IsDeleted = x.isDeleted,
                    Attachments = x.Attachments,
                    Language = x.Language,
                    ApprovalBy = x.ApprovalBy,
                    ApprovalDate = x.ApprovalDate,
                    CompletionBy = x.CompletionBy,
                    CompletionDate = x.CompletionDate,
                    CreatedBy = x.CreatedBy,
                    AppointmentTime = x.AppointmentTime,
                    RoomNumber = x.RoomNumber
                });
            });
            return model;
        }
        public List<AppointmentDetailViewModel> GetAll()
        {
            List<AppointmentDetailViewModel> model = new List<AppointmentDetailViewModel>();
            var appointments = _dbContext.AppointmentInfo.ToList();
            appointments.ForEach(x =>
            {
                var institute = _dbContext.Institutions.FirstOrDefault((y => y.Id == x.InstitutionId));
                var traName = _dbContext.Translators.FirstOrDefault((y => y.Id == x.TranslatorId));

                var pay = _dbContext.Finance.FirstOrDefault(y => y.AppointmentId == x.Id && y.Type == "P");
                var rec = _dbContext.Finance.FirstOrDefault(y => y.AppointmentId == x.Id && y.Type == "R");

                model.Add(new AppointmentDetailViewModel
                {
                    Appointment = new AppointmentViewModel
                    {
                        Id = x.Id,
                        AppointmentId = x.AppointmentId,
                        TranslatorId = x.TranslatorId,
                        TranslatorName = traName.FirstName + " " + traName.LastName,
                        InstitutionId = x.InstitutionId,
                        InstitutionName = institute.Name,
                        InstitutionAddress = institute.Address + " \n " + institute.Postcode + " " + institute.City,
                        Type = x.Type,
                        EntryDate = x.EntryDate,
                        AppointmentDate = x.AppointmentDate,
                        Tax = x.Tax,
                        Rate = x.Rate,
                        Hours = x.Hours,
                        Discount = x.Discount,
                        NetPayment = x.NetPayment,
                        Status = x.Status,
                        IsDeleted = x.isDeleted,
                        Attachments = x.Attachments,
                        Language = x.Language,
                        ApprovalBy = x.ApprovalBy,
                        ApprovalDate = x.ApprovalDate,
                        CompletionBy = x.CompletionBy,
                        CompletionDate = x.CompletionDate,
                        AppointmentTime = x.AppointmentTime,
                        RoomNumber = x.RoomNumber
                    },
                    Payable = pay == null ? new FinanceViewModel() : new FinanceViewModel
                    {
                        Id = x.Id,
                        AppointmentId_Fk = pay.AppointmentId,
                        AppointmentId = x.AppointmentId,
                        AppointmentDate = x.AppointmentDate.ToShortDateString(),
                        AppointmentInstitute = institute.Name,
                        InstituteAddress = institute.Address + " \n " + institute.Postcode + " " + institute.City,
                        AppointmentTranslator = traName.FirstName + " " + traName.LastName,
                        AppointmentLanguage = x.Language,
                        AppointmentType = x.Type,

                        Type = pay.Type,
                        Tax = pay.Tax,
                        WordCount = pay.WordCount,
                        Rate = pay.Rate,
                        FlatRate = pay.FlatRate,
                        Paragraph = pay.Paragraph,
                        Postage = pay.Postage,
                        Hours = pay.Hours,
                        AppointmentStart = pay.AppointmentStart,
                        StartOfTheTrip = pay.StartOfTheTrip,
                        EndOfTheTrip = pay.EndOfTheTrip,
                        EndOfTheAppointment = pay.EndOfTheAppointment,
                        TotalHours = pay.TotalHours,
                        Discount = pay.Discount,
                        NetPayment = pay.NetPayment,
                        Status = pay.Status,
                        RideCost = pay.RideCost,
                        DailyAllowance = pay.DailyAllowance,
                        TicketCost = pay.TicketCost,
                        isDeleted = pay.isDeleted,
                        CreatedBy = pay.CreatedBy,
                        ApprovalBy = pay.ApprovalBy,
                        ApprovalDate = pay.ApprovalDate,
                        CompletionBy = pay.CompletionBy,
                        CompletionDate = pay.CompletionDate
                    },
                    Receivable = rec == null ? new FinanceViewModel() : new FinanceViewModel
                    {
                        Id = x.Id,
                        AppointmentId_Fk = pay.AppointmentId,
                        AppointmentId = x.AppointmentId,
                        AppointmentDate = x.AppointmentDate.ToShortDateString(),
                        AppointmentInstitute = institute.Name,
                        InstituteAddress = institute.Address + " \n " + institute.Postcode + " " + institute.City,
                        AppointmentTranslator = traName.FirstName + " " + traName.LastName,
                        AppointmentLanguage = x.Language,
                        AppointmentType = x.Type,

                        ApprovalBy = rec.ApprovalBy,
                        ApprovalDate = rec.ApprovalDate,
                        CompletionBy = rec.CompletionBy,
                        CompletionDate = rec.CompletionDate,

                        Type = rec.Type,
                        Tax = rec.Tax,
                        WordCount = rec.WordCount,
                        Rate = rec.Rate,
                        Hours = rec.Hours,
                        Discount = rec.Discount,
                        NetPayment = rec.NetPayment,
                        Status = rec.Status,
                        isDeleted = rec.isDeleted,
                        CreatedBy = rec.CreatedBy,
                    }
                });
            });
            return model;
        }

        public List<PieChartViewModel> GetAppointmentStats()
        {
            List<PieChartViewModel> model = new List<PieChartViewModel>();
            var appointments = _dbContext.AppointmentInfo.ToList();

            var pending = appointments.Where(x => x.Status == "Pending");
            var completed = appointments.Where(x => x.Status == "Completed");
            var p_completed = appointments.Where(x => x.Status == "PartiallyCompleted");
            var approved = appointments.Where(x => x.Status == "Approved");

            model.Add(new PieChartViewModel
            {
                name = "Pending",
                value = pending.Count()
            });
            model.Add(new PieChartViewModel
            {
                name = "Completed",
                value = completed.Count()
            });
            model.Add(new PieChartViewModel
            {
                name = "Partially Completed",
                value = p_completed.Count()
            });
            model.Add(new PieChartViewModel
            {
                name = "Approved",
                value = approved.Count()
            });
            return model;
        }

        #endregion

        #region Create / Update / Delete

        [HttpPost]
        public JsonResult CreateAppointment(AppointmentViewModel model)
        {
            try
            {
                FinanceService finance = new FinanceService(_dbContext);

                if (model is not null)
                {
                    var appCount = _dbContext.AppointmentInfo.Where(x => x.AppointmentId == model.AppointmentId && x.isDeleted == null).Count();
                    if (appCount == 0)
                    {
                        AppointmentInfo appInfo = new AppointmentInfo()
                        {
                            AppointmentId = model.AppointmentId,
                            TranslatorId = model.TranslatorId,
                            InstitutionId = model.InstitutionId,
                            Type = model.Type,
                            EntryDate = model.EntryDate,
                            AppointmentDate = model.AppointmentDate,
                            Tax = model.Tax,
                            Rate = model.Rate,
                            Hours = model.Hours,
                            Discount = model.Discount,
                            Status = "Pending",
                            NetPayment = ((model.Rate * model.Hours) + model.Tax - model.Discount),
                            CreatedBy = model.CreatedBy,
                            Attachments = model.Attachments,
                            Language = model.Language,
                            AppointmentTime = model.AppointmentTime,
                            RoomNumber = model.RoomNumber
                        };

                        _dbContext.AppointmentInfo.Add(appInfo);
                        _dbContext.SaveChanges();

                        FinanceViewModel fin = new FinanceViewModel();

                        //Payable leg
                        fin.AppointmentId = model.Id;
                        fin.Status = "ACTIVE";
                        fin.Type = "P";
                        finance.Create(fin);

                        //Receivable leg
                        fin.AppointmentId = model.Id;
                        fin.Status = "ACTIVE";
                        fin.Type = "R";
                        finance.Create(fin);

                        //sendEmailoTranslator(model.TranslatorId, model.Attachments);

                        return Json(new { appointment = appInfo, success = true, message = "Appointment created successfully" });
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
                        app.TranslatorId = model.TranslatorId;
                        app.InstitutionId = model.InstitutionId;
                        app.Type = model.Type;
                        app.EntryDate = model.EntryDate;
                        app.AppointmentDate = model.AppointmentDate;
                        app.Tax = model.Tax;
                        app.Rate = model.Rate;
                        app.Hours = model.Hours;
                        app.Discount = model.Discount;
                        app.NetPayment = ((model.Rate * model.Hours) + model.Tax - model.Discount);
                        app.Language = model.Language;
                        app.AppointmentTime = model.AppointmentTime;
                        app.RoomNumber = model.RoomNumber;

                        if (!string.IsNullOrEmpty(model.Attachments))
                        {
                            if (!string.IsNullOrEmpty(app.Attachments))
                            {
                                app.Attachments += model.Attachments;
                            }
                        }

                        _dbContext.Entry(app).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        return Json(new { appointment = app, success = true, message = "Appointment updated successfully" });
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
        public JsonResult ApproveAppointment([FromBody] ParamsViewModel model)
        {
            try
            {
                FinanceService finance = new FinanceService(_dbContext);

                if (model is not null)
                {
                    var app = _dbContext.AppointmentInfo.SingleOrDefault(x => x.Id == model.id && x.isDeleted == null);
                    if (app is not null)
                    {
                        app.Status = "Approved";
                        app.ApprovalDate = DateTime.Now;
                        app.ApprovalBy = model.userId;

                        FinanceViewModel fin = new FinanceViewModel();

                        //Payable leg
                        fin.AppointmentId_Fk = app.Id;
                        fin.Status = "Pending";
                        fin.Type = "P";
                        fin.Tax = 19;
                        fin.CreatedBy = model.userId;

                        finance.Create(fin);

                        //Receivable leg
                        fin.AppointmentId_Fk = app.Id;
                        fin.Status = "Pending";
                        fin.Type = "R";
                        fin.Tax = 19;
                        fin.CreatedBy = model.userId;
                        finance.Create(fin);

                        _dbContext.Entry(app).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        return Json(new { success = true, message = "Appointment Approved successfully" });
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
        public JsonResult DeleteAppointment([FromBody] ParamsViewModel model)
        {
            try
            {
                if (model is not null)
                {
                    var app = _dbContext.AppointmentInfo.Where(x => x.Id == model.id && x.isDeleted == null).SingleOrDefault();
                    if (app is not null)
                    {
                        app.isDeleted = "Y";

                        var fin = _dbContext.Finance.Where(x => x.AppointmentId == app.Id && x.isDeleted == null).ToList();
                        foreach (var item in fin)
                        {
                            item.isDeleted = "Y";
                            _dbContext.Entry(item).State = EntityState.Modified;
                        }

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

        #region Emails
        private async void sendEmailToTranslator(long translatorId, string attachments)
        {
            try
            {
                var translator = _dbContext.Translators.FirstOrDefault(x => x.Id == translatorId);
                var listAttachments = attachments.Split(',');
                var files = new List<FileStreamResult>();
                foreach (var att in listAttachments)
                {
                    if (att.Length > 4)
                    {
                        files.Add(AttachFile(att));
                    }
                }
                await SendEmailAsync("Qureshi", "email_id", "email_password", translator.Email,
                    "New Appointment", "Hello! ", files, translator.FirstName);
            }
            catch (Exception ex)
            {

            }
        }
        public async Task SendEmailAsync(string fromName, string fromEmail, string fromEmailPassword, string toEmail, string subject, string body, List<FileStreamResult> streamList, string toName = null)
        {
            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    var ToUser = toName == null ? toEmail : toName;
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(fromName, fromEmail));
                    message.To.Add(new MailboxAddress(ToUser, toEmail));
                    message.Subject = subject;


                    var multipart = new Multipart("mixed");
                    multipart.Add(new TextPart("html")
                    {
                        Text = body
                    });

                    foreach (var stream in streamList)
                    {
                        // create an image attachment for the file located at path
                        var attachment = new MimePart(System.Net.Mime.MediaTypeNames.Application.Pdf)
                        {
                            Content = new MimeContent(stream.FileStream),
                            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                            ContentTransferEncoding = ContentEncoding.Base64,
                            FileName = Path.GetFileName(stream.FileDownloadName)
                        };
                        multipart.Add(attachment);
                    }

                    message.Body = multipart;

                    await client.ConnectAsync("smtp.gmail.com", 465, true);

                    await client.AuthenticateAsync(fromEmail, fromEmailPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        #endregion

        #region File Upload/Download

        [HttpPost]
        public async Task<IActionResult> Upload(IFormCollection files)
        {
            try
            {
                if (files.Files.Count > 0)
                {
                    var fileNames = "";
                    foreach (var file in files.Files)
                    {
                        if (file.Length > 0)
                        {
                            var g = Guid.NewGuid();
                            var official = String.Format("{0}_{1}", g, file.FileName);

                            string folderPath = AppDomain.CurrentDomain.BaseDirectory;
                            string path = Path.Combine(folderPath, "uploadFiles");
                            if (!Directory.Exists(path))
                            {
                                //If Directory (Folder) does not exists. Create it.
                                Directory.CreateDirectory(path);
                            }
                            using (var fs = new FileStream(Path.Combine(path, official), FileMode.Create))
                            {
                                await file.CopyToAsync(fs);
                            }
                            fileNames += official + ",";
                        }
                    }
                    return new ContentResult { StatusCode = (int)HttpStatusCode.OK, Content = fileNames };
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Unable to Upload File" + ex.Message };
            }
        }
        [HttpPost]
        public async Task<IActionResult> DownloadWord(IFormCollection files)
        {
            try
            {
                if (files.Files.Count > 0)
                {
                    var fileNames = "";
                    foreach (var file in files.Files)
                    {
                        if (file.Length > 0)
                        {
                            var g = Guid.NewGuid();
                            var official = String.Format("{0}_{1}", g, file.FileName);

                            string folderPath = AppDomain.CurrentDomain.BaseDirectory;
                            string path = Path.Combine(folderPath, "uploadFiles");
                            if (!Directory.Exists(path))
                            {
                                //If Directory (Folder) does not exists. Create it.
                                Directory.CreateDirectory(path);
                            }
                            using (var fs = new FileStream(Path.Combine(path, official), FileMode.OpenOrCreate))
                            {
                                await file.CopyToAsync(fs);
                                fs.Close();

                                PdfDocument pdf = new PdfDocument();

                                var allBytes = System.IO.File.ReadAllBytes(Path.Combine(path, official));

                                var stream = new MemoryStream(0);
                                stream.Write(allBytes, 0, allBytes.Length);
                                pdf.LoadFromStream(stream);
                                pdf.SaveToStream(stream, FileFormat.DOCX);
                                pdf.SaveToFile(Path.Combine(path, "newFile.docx"), FileFormat.DOCX);
                            }

                            path = Path.Combine(path, "newFile.docx");
                            var fileToDownload = new FileInfo(path);
                            if (fileToDownload.Exists)
                            {
                                var fileContentResult = new FileContentResult(System.IO.File.ReadAllBytes(path), GetContentType(path))
                                {
                                    FileDownloadName = "newFile.docx"
                                };
                                return fileContentResult;
                            }
                        }
                        return new ContentResult { StatusCode = (int)HttpStatusCode.OK, Content = fileNames };
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Unable to Upload File" + ex.Message };
            }
        }
        [HttpPost]
        public async Task<IActionResult> UploadAndEmail(IFormCollection files)
        {
            try
            {
                if (files.Files.Count > 0)
                {
                    Int64 appointmendId = 0;
                    var appId = files["AppointmentId"].ToString();
                    if (!String.IsNullOrEmpty(appId))
                    {
                        appointmendId = Convert.ToInt64(appId);
                    }
                    var fileNames = "";
                    foreach (var file in files.Files)
                    {
                        if (file.Length > 0)
                        {
                            var g = Guid.NewGuid();
                            var official = String.Format("{0}_{1}", g, file.FileName);

                            string folderPath = AppDomain.CurrentDomain.BaseDirectory;
                            string path = Path.Combine(folderPath, "uploadFiles");
                            if (!Directory.Exists(path))
                            {
                                //If Directory (Folder) does not exists. Create it.
                                Directory.CreateDirectory(path);
                            }
                            using (var fs = new FileStream(Path.Combine(path, official), FileMode.Create))
                            {
                                await file.CopyToAsync(fs);
                            }
                            fileNames += official + ",";
                        }
                    }
                    var app = _dbContext.AppointmentInfo.FirstOrDefault(x => x.Id == appointmendId);
                    if (app != null)
                    {
                        sendEmailToTranslator(app.TranslatorId, fileNames);
                    }
                    return new ContentResult { StatusCode = (int)HttpStatusCode.OK, Content = fileNames };
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return new ContentResult { StatusCode = (int)HttpStatusCode.InternalServerError, Content = "Unable to Upload File" + ex.Message };
            }
        }

        [HttpPost]
        public async Task<IActionResult> Download(FileData Data)
        {
            try
            {
                var filename = Data.Name;

                if (filename == null)
                {
                    return Content("filename not present");
                }

                var path = Path.Combine(
                           AppDomain.CurrentDomain.BaseDirectory,
                           "uploadFiles", filename);

                var file = new FileInfo(path);
                if (file.Exists)
                {
                    var fileContentResult = new FileContentResult(System.IO.File.ReadAllBytes(path), GetContentType(path))
                    {
                        FileDownloadName = filename
                    };
                    return fileContentResult;
                }

                return Content("filename not present");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public FileStreamResult AttachFile(string filename)
        {
            try
            {
                if (filename == null)
                {
                    return null;
                }

                var path = Path.Combine(
                           AppDomain.CurrentDomain.BaseDirectory,
                           "uploadFiles", filename);

                var file = new FileInfo(path);
                if (file.Exists)
                {
                    var fis = new FileStream(file.FullName, FileMode.Open, FileAccess.ReadWrite);

                    return new FileStreamResult(fis, GetContentType(path))
                    {
                        FileDownloadName = file.Name
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        #endregion
    }
    public class FileData
    {
        public string Name { get; set; }
    }
}
