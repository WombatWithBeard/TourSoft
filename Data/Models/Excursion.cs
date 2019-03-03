using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToursSoft.Data.Models.Users;

namespace ToursSoft.Data.Models
{
    [Table("Excursion")]
    public class Excursion: Model
    {
        public Excursion(Guid id, DateTime dateTime, bool status, Guid tourId) : base(id)
        {
            DateTime = dateTime;
            Status = status;
            TourId = tourId;
        }

        public Excursion()
        {
        }

        [Required]
        public DateTime DateTime { get; set; }
        
        /// <summary>
        /// Status of this excursion: 0 - not active, 1 - is active
        /// </summary>
        [Required]
        public bool Status { get; set; }

        /// <summary>
        /// Tour link
        /// </summary>
        [Required]
        [ForeignKey("Tour")]
        public Guid TourId { get; set; }
        public virtual Tour Tour { get; set; }

        ////TO DO: do it
        public bool GetCapacity()
        {
            return true;
        }
    }
}