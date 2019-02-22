using System;
using System.Net.Mail;

namespace ToursSoft.Data.Models
{
    public class ManagersPerson: Model
    {
        public ManagersPerson()
        {
        }

        public Guid PersonId { get; set; }

        public Guid ManagerId { get; set; }
    }
}