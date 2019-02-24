using System;
using System.ComponentModel.DataAnnotations;

namespace ToursSoft.Data.Models.Service
{
    public class ExcursionAddRequest
    {
        [Required(ErrorMessage = "Excursion ID is missed")]
        public Guid ExcursionId { get; set; }

        [Required(ErrorMessage = "User ID is missed")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Person data is missed")]
        public Person Person { get; set; }
    }
}