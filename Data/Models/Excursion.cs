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
        public Excursion(Guid id, DateTime dateTime, List<ManagersGroup> managersGroup, bool status, Tour tour) : base(id)
        {
            DateTime = dateTime;
            ManagersGroup = managersGroup;
            Status = status;
            Tour = tour;
        }

        public Excursion()
        {
        }

        [Required]
        public DateTime DateTime { get; set; }
        
        /// <summary>
        /// Group of people, which managers add in this excursion
        /// </summary>
        public Guid? ManagersGroupId { get; set; }
        [ForeignKey("ManagersGroupId")]
        public List<ManagersGroup> ManagersGroup { get; set; }
        
        /// <summary>
        /// Status of this excursion: 0 - not active, 1 - is active
        /// </summary>
        /// 
        [Required]
        public bool Status { get; set; }

        /// <summary>
        /// Tour link
        /// </summary>
        public Guid? TourId { get; set; }
        [Required]
        [ForeignKey("TourId")]
        public Tour Tour { get; set; }

        //TO DO: do it
        public bool GetCapacity(Person persons)
        {
            return true;
        }
    }
}