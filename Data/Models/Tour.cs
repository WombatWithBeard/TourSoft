using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToursSoft.Data.Models
{
    [Table("Tour")]
    public class Tour: Model
    {
        public Tour(Guid id, string name, int capacity, string description) : base(id)
        {
            Name = name;
            Capacity = capacity;
            Description = description;
        }

        public Tour()
        {
        }

        /// <summary>
        /// Info about name of the tour
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Info about tour persons capacity
        /// </summary>
        [Required]
        public int Capacity { get; set; }

        /// <summary>
        /// Info about tour
        /// </summary>
        public string Description { get; set; }
    }
}