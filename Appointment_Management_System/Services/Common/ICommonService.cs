using Appointment_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.Services.Common
{
    public interface ICommonService
    {
        public List<Translators> GetTranslators();
        public List<Institutions> GetInstitutions();
    }
}
