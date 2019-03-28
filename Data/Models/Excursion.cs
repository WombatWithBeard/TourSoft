using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToursSoft.Data.Models.Users;

namespace ToursSoft.Data.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Excursion class: excursion is the "Tour" in specific date and time
    /// </summary>
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

        public string Type { get; set; }

        /// <summary>
        /// Tour link
        /// </summary>
        [Required]
        [ForeignKey("Tour")]
        public Guid TourId { get; set; }
        /// <summary>
        /// link on tour table
        /// </summary>
        public virtual Tour Tour { get; set; }
    }
}