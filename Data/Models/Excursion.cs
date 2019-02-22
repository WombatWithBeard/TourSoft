using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ToursSoft.Data.Models;

namespace ToursSoft.Data.Models
{
    [Table("Excursion")]
    public class Excursion: Model
    {
        public Excursion(Guid id, DateTime dateTime, Dictionary<Guid, Person> managersGroup, bool status, Guid tourId) 
            : base(id)
        {
            DateTime = dateTime;
//            ManagersGroup = managersGroup;
            Status = status;
            TourId = tourId;
        }

        public Excursion()
        {
        }

        public DateTime DateTime { get; set; }
        
        
        //TO DO: Lol
        /// <summary>
        /// Group of people, which managers add in this excursion
        /// </summary>
//        public Dictionary<Guid, Person> ManagersGroup { get; set; }
        
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
        [ForeignKey("Tour")]
        public Guid TourId { get; set; }
        public virtual Tour Tour { get; set; }
    }
}