using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ToursSoft.Data.Models;

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

        /// <summary>
        /// Group of people, which managers add in this excursion
        /// </summary>
//        [ForeignKey("ManagersGroup")]
//        [NotMapped]
//        public ICollection<Guid> ManagersGroupId { get; set; }
//        public virtual ManagersGroup ManagersGroup { get; set; }
        //public virtual ICollection<ManagersGroup> ManagersGroups { get; set; }

        ////TO DO: do it
        public bool GetCapacity()
        {
            return true;
        }
    }
}