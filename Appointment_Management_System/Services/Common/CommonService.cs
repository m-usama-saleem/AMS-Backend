using Appointment_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appointment_Management_System.Services.Common
{
    public class CommonService: Controller, ICommonService
    {
        private readonly DatabaseContext _dbContext;

        public CommonService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public List<Translators> GetTranslators()
        {
            return _dbContext.Translators.Where(x => x.isDeleted == null).ToList();
        }

        public List<Institutions> GetInstitutions()
        {
            return _dbContext.Institutions.Where(x => x.isDeleted == null).ToList();
        }
    }
}
