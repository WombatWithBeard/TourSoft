using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToursSoft.Data.Models
{
    [Table("Excursion")]
    public class Excursion: Model
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateTime"></param>
        /// <param name="status"></param>
        /// <param name="tourId"></param>
        public Excursion(Guid id, DateTime dateTime, bool status, Guid tourId) : base(id)
        {
            DateTime = dateTime;
            Status = status;
            TourId = tourId;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Excursion()
        {
        }

        /// <summary>
        /// Date and time of excursion
        /// </summary>
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

        ////TODO: do it
        public bool GetCapacity()
        {
            return true;
        }
    }
}