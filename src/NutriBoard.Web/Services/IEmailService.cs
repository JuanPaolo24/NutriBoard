using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutriBoard.Web.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
