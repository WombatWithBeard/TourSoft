using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Design;
using ToursSoft.Data.Models.Models.Users;

namespace ToursSoft.Data.Models.Models
{
    public class Excursion: Model
    {
        public Excursion(Guid id, DateTime dateTime, Dictionary<Guid, Person> managersGroup, bool status, Guid tourId) 
            : base(id)
        {
            DateTime = dateTime;
            ManagersGroup = managersGroup;
            Status = status;
            TourId = tourId;
        }

        public DateTime DateTime { get; set; }
        
        /// <summary>
        /// Group of people, which managers add in this excursion
        /// </summary>
        public Dictionary<Guid, Person> ManagersGroup { get; set; }
        
        /// <summary>
        /// Status of this excursion: 0 - not active, 1 - is active
        /// </summary>
        /// 
        [Required]
        public bool Status { get; set; }

        /// <summary>
        /// Tour link
        /// </summary>
        [Required]
        public Guid TourId { get; set; }
    }
}